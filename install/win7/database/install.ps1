<#
.SYNOPSIS
    Installs the Vesy13 local database on Windows 7 SP1 x64.

.DESCRIPTION
    Deploys PostgreSQL 10.6, the scale_db schema, the trust rules and the purge task
    in the Windows scheduler. Runs unattended, so the SCCM agent can execute it
    as SYSTEM.

    Every run removes an existing scale_db database and creates the final schema
    again. PostgreSQL itself, access rules and the purge task are then configured.

    Exit code 0 means the installation is complete, anything else makes SCCM
    report a failure. Progress goes to C:\ProgramData\Vesy13\install-db.log.

    Text in this file stays ASCII: Windows PowerShell 2.0 reads a script without
    a byte order mark in the system ANSI codepage, which corrupts multibyte
    characters and can break parsing.

.PARAMETER PostgresInstaller
    EDB PostgreSQL installer. Defaults to postgresql-*windows-x64.exe found next
    to this script inside the package.

.EXAMPLE
    powershell.exe -ExecutionPolicy Bypass -NoProfile -File install.ps1
#>

[CmdletBinding()]
param(
    [string] $PostgresInstaller,
    [string] $PgRoot            = 'C:\Program Files\PostgreSQL\10',
    [string] $DataDir           = 'C:\Program Files\PostgreSQL\10\data',
    [int]    $Port              = 5432,
    [string] $ServiceName       = 'postgresql-x64-10',
    [string] $StateDir          = 'C:\ProgramData\Vesy13'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Windows PowerShell 2.0 has no $PSScriptRoot.
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

if (-not $PostgresInstaller) {
    $found = Get-ChildItem -Path $ScriptDir -Filter 'postgresql-10.6-1-windows-x64.exe' -ErrorAction SilentlyContinue | Where-Object { -not $_.PSIsContainer } | Select-Object -First 1
    if ($found) { $PostgresInstaller = $found.FullName }
}

$PasswordFile  = Join-Path $StateDir 'postgres_password.txt'
$InstallerOptionFile = Join-Path $StateDir 'postgres-install-options.txt'
$LogFile       = Join-Path $StateDir 'install-db.log'
$Psql          = Join-Path $PgRoot 'bin\psql.exe'

$TempTrustTag  = '# Vesy13 temporary superuser trust'
$ScaleTrustTag = '# Vesy13 application access'

# -- Helpers ------------------------------------------------------------------

function Write-Log {
    param([string] $Message)
    $line = '{0:yyyy-MM-dd HH:mm:ss}  {1}' -f (Get-Date), $Message
    Add-Content -Path $LogFile -Value $line -Encoding UTF8
    Write-Host $line
}

function New-Password {
    # 32 characters from an alphabet that is safe on a command line, in files and
    # inside a single-quoted SQL literal.
    # Create()/GetBytes() exists on .NET Framework, which Windows PowerShell 2.0
    # runs on; the static Fill() helper arrived only with .NET Core.
    $alphabet = 'abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789'
    $bytes    = New-Object byte[] 32
    $rng      = [System.Security.Cryptography.RandomNumberGenerator]::Create()
    try     { $rng.GetBytes($bytes) }
    finally 
    {
        if ($rng -is [System.IDisposable]) {
            ([System.IDisposable]$rng).Dispose()
        }
    }
    -join ($bytes | ForEach-Object { $alphabet[$_ % $alphabet.Length] })
}

function Protect-File {
    param([string] $Path)
    # Access is kept for SYSTEM and administrators, inheritance is turned off.
    #
    # The rights are written as SDDL so no account name is ever translated:
    # account names are localized, and reading or building rules through
    # FileSystemAccessRule raises IdentityNotMappedException when a name cannot
    # be resolved. In SDDL, SY is the local system and BA the builtin
    # administrators, and both are locale independent.
    #   D:P        - the list is protected, inherited entries are dropped
    #   (A;;FA;;;) - allow full access
    $acl = Get-Acl -Path $Path
    $acl.SetSecurityDescriptorSddlForm('D:P(A;;FA;;;SY)(A;;FA;;;BA)')
    Set-Acl -Path $Path -AclObject $acl
}

function Get-PgDataDir {
    # The service command line carries the real data directory, which survives an
    # install that used different paths than this script's defaults.
    $svc = Get-WmiObject -Class Win32_Service -Filter "Name='$ServiceName'" -ErrorAction SilentlyContinue
    if ($svc -and $svc.PathName -match '-D\s+"([^"]+)"') { return $Matches[1] }
    return $DataDir
}

function Get-PgHbaPath { Join-Path (Get-PgDataDir) 'pg_hba.conf' }

function Add-PgHbaRules {
    # Rules go above the general ones: pg_hba.conf applies the first match.
    # The block is fenced by BEGIN/END comments so removal takes exactly the
    # lines it added and leaves neighbouring records alone.
    param([string] $Tag, [string[]] $Rules)

    $path = Get-PgHbaPath
    $hba  = Get-Content -Path $path
    if ($hba -match [regex]::Escape("$Tag BEGIN")) { return $false }

    $block  = @("$Tag BEGIN") + $Rules + @("$Tag END")
    $anchor = ($hba | Select-String -Pattern '^\s*host\s+all\s+all' | Select-Object -First 1)
    if ($anchor) {
        $index  = $anchor.LineNumber - 1
        $result = @($hba[0..($index - 1)]) + $block + @($hba[$index..($hba.Count - 1)])
    }
    else {
        $result = $block + $hba
    }

    if (-not (Test-Path "$path.vesy13.bak")) { Copy-Item -Path $path -Destination "$path.vesy13.bak" -Force }
    Set-Content -Path $path -Value $result -Encoding ASCII
    return $true
}

function Remove-PgHbaRules {
    param([string] $Tag)

    $path  = Get-PgHbaPath
    $hba   = Get-Content -Path $path
    $start = ($hba | Select-String -Pattern ([regex]::Escape("$Tag BEGIN")) | Select-Object -First 1)
    $stop  = ($hba | Select-String -Pattern ([regex]::Escape("$Tag END"))   | Select-Object -First 1)
    if (-not $start -or -not $stop) { return $false }

    $first = $start.LineNumber - 1
    $last  = $stop.LineNumber - 1
    if ($last -lt $first) { throw "Malformed block '$Tag' in $path." }

    $result = @()
    if ($first -gt 0)           { $result += $hba[0..($first - 1)] }
    if ($last + 1 -lt $hba.Count) { $result += $hba[($last + 1)..($hba.Count - 1)] }
    Set-Content -Path $path -Value $result -Encoding ASCII
    return $true
}

function Test-PgConnection {
    # Probes the server and reports success as a value. With
    # $ErrorActionPreference = 'Stop' PowerShell turns anything a native command
    # writes to stderr into a terminating error before the exit code can be
    # read, so the preference is relaxed for the duration of the call.
    param([string] $Password)

    $prevPreference        = $ErrorActionPreference
    $prevPassword          = $env:PGPASSWORD
    $ErrorActionPreference = 'Continue'
    if ($Password) { $env:PGPASSWORD = $Password }
    else           { Remove-Item Env:PGPASSWORD -ErrorAction SilentlyContinue }
    try {
        & $Psql -X -q -h 127.0.0.1 -p $Port -U postgres -d postgres -c 'SELECT 1' 2>&1 | Out-Null
        return ($LASTEXITCODE -eq 0)
    }
    finally {
        $ErrorActionPreference = $prevPreference
        if ($prevPassword) { $env:PGPASSWORD = $prevPassword }
        else               { Remove-Item Env:PGPASSWORD -ErrorAction SilentlyContinue }
    }
}

function Restart-PgService {
    param([string] $Password)
    Restart-Service -Name $ServiceName -Force
    # The service reports running before the postmaster accepts connections.
    for ($i = 0; $i -lt 30; $i++) {
        Start-Sleep -Seconds 1
        if (Test-PgConnection -Password $Password) { return $true }
    }
    return $false
}

function Invoke-Psql {
    param(
        [string] $Database,
        [string] $User,
        [string] $File,
        [string] $Command,
        [string] $Password
    )
    # $args is an automatic PowerShell variable, hence the explicit name.
    $psqlArgs = @('-v', 'ON_ERROR_STOP=1', '-X', '-q', '-h', '127.0.0.1', '-p', $Port, '-U', $User, '-d', $Database)
    if ($File)    { $psqlArgs += @('-f', $File) }
    if ($Command) { $psqlArgs += @('-t', '-A', '-c', $Command) }

    # The password travels in the process environment: on a command line it
    # would show up in the process list and in SCCM logs.
    $previous             = $env:PGPASSWORD
    $prevPreference       = $ErrorActionPreference
    $env:PGPASSWORD       = $Password
    $env:PGCLIENTENCODING = 'UTF8'
    # Relaxed for the same reason as in Test-PgConnection: psql writing to stderr
    # would otherwise abort the script before its exit code is examined.
    $ErrorActionPreference = 'Continue'
    try {
        $output = & $Psql @psqlArgs 2>&1
        if ($LASTEXITCODE -ne 0) {
            throw "psql exited with code ${LASTEXITCODE}: $output"
        }
        return ($output | Out-String).Trim()
    }
    finally {
        $ErrorActionPreference = $prevPreference
        $env:PGPASSWORD        = $previous
    }
}

# -- Preparation --------------------------------------------------------------

New-Item -ItemType Directory -Path $StateDir -Force | Out-Null
Write-Log '=== Vesy13 database installation ==='

# -- 1. PostgreSQL ------------------------------------------------------------

$existingService = Get-Service -Name $ServiceName -ErrorAction SilentlyContinue
$installerPassword = $null
if ($existingService) {
    Write-Log "Service $ServiceName found, skipping the PostgreSQL installer."
}
else {
    Write-Log 'Installing PostgreSQL 10.6.'
    if (-not $PostgresInstaller) {
        throw "PostgreSQL installer not found in $ScriptDir. Put postgresql-10.6-1-windows-x64.exe next to this script or pass -PostgresInstaller."
    }
    if (-not (Test-Path $PostgresInstaller)) { throw "PostgreSQL installer is missing: $PostgresInstaller" }
    $installerPassword = New-Password

    # PostgreSQL 10.6 reads unattended settings from a protected temporary file.
    # The generated password is therefore never exposed in the process list.
    $installerOptions = @(
        'mode=unattended',
        'unattendedmodeui=none',
        "prefix=$PgRoot",
        "datadir=$DataDir",
        "serverport=$Port",
        "servicename=$ServiceName",
        "superpassword=$installerPassword",
        "servicepassword=$installerPassword",
        'disable-components=stackbuilder'
    )
    Set-Content -Path $InstallerOptionFile -Value $installerOptions -Encoding ASCII
    Protect-File -Path $InstallerOptionFile
    $proc = $null
    try {
        $proc = Start-Process -FilePath $PostgresInstaller -ArgumentList @('--optionfile', "`"$InstallerOptionFile`"") -Wait -PassThru
    }
    finally {
        Remove-Item -Path $InstallerOptionFile -Force -ErrorAction SilentlyContinue
    }
    if ($proc.ExitCode -ne 0) {
        $installerLogs = Get-ChildItem -Path $env:TEMP -Filter '*install*.log' -ErrorAction SilentlyContinue |
                         Where-Object { $_.LastWriteTime -gt (Get-Date).AddMinutes(-30) }
        foreach ($log in $installerLogs) {
            $copy = Join-Path $StateDir "postgres-$($log.Name)"
            Copy-Item -Path $log.FullName -Destination $copy -Force -ErrorAction SilentlyContinue
            Write-Log "Installer log copied to $copy"
        }
        throw "PostgreSQL installer exited with code $($proc.ExitCode)."
    }
    Write-Log 'PostgreSQL 10.6 installed.'
}

if (-not (Test-Path $Psql)) { throw "psql is missing: $Psql" }

# -- 2. Superuser password ----------------------------------------------------

# The password is set from here rather than taken from the installer, so the
# stored value and the server always agree.

$superPassword = $null
if (Test-Path $PasswordFile) {
    $stored = (Get-Content -Path $PasswordFile | Select-Object -First 1).Trim()
    if (Test-PgConnection -Password $stored) {
        $superPassword = $stored
        Write-Log 'Stored superuser password accepted.'
    }
}

if (-not $superPassword -and $installerPassword -and (Test-PgConnection -Password $installerPassword)) {
    $superPassword = $installerPassword
    Set-Content -Path $PasswordFile -Value $superPassword -Encoding ASCII
    Protect-File -Path $PasswordFile
    Write-Log 'Generated superuser password stored.'
}

if (-not $superPassword) {
    Write-Log 'Setting the superuser password through a temporary trust rule.'
    $superPassword = New-Password

    Add-PgHbaRules -Tag $TempTrustTag -Rules @(
        "host    all    postgres    127.0.0.1/32    trust",
        "host    all    postgres    ::1/128         trust"
    ) | Out-Null
    if (-not (Restart-PgService)) {
        throw "The temporary trust rule did not take effect: connecting as postgres still fails. Check $(Get-PgHbaPath)."
    }

    try {
        Invoke-Psql -Database 'postgres' -User 'postgres' `
            -Command "ALTER ROLE postgres PASSWORD '$superPassword'" | Out-Null
    }
    finally {
        Remove-PgHbaRules -Tag $TempTrustTag | Out-Null
        Restart-PgService -Password $superPassword | Out-Null
    }

    Set-Content -Path $PasswordFile -Value $superPassword -Encoding ASCII
    Protect-File -Path $PasswordFile
    Write-Log "Superuser password stored in $PasswordFile, readable by SYSTEM and administrators."
}

# -- 3. The scale_db schema ---------------------------------------------------

$dbExists = Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword `
    -Command "SELECT 1 FROM pg_database WHERE datname = 'scale_db'"

if ($dbExists -eq '1') {
    Write-Log 'Removing existing database scale_db.'
    Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword `
        -Command "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = 'scale_db' AND pid <> pg_backend_pid(); DROP DATABASE scale_db" | Out-Null
}

Write-Log 'Creating database scale_db.'
Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword `
    -File (Join-Path $ScriptDir 'scale_db.sql') | Out-Null
Write-Log 'Database scale_db created.'
# -- 4. Trust rules for the application ---------------------------------------

$added = Add-PgHbaRules -Tag $ScaleTrustTag -Rules @(
    'host    scale_db    scale_user    127.0.0.1/32    trust',
    'host    scale_db    scale_user    ::1/128         trust'
)
if ($added) {
    Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword -Command 'SELECT pg_reload_conf()' | Out-Null
    Write-Log 'Trust rules added, configuration reloaded.'
}
else {
    Write-Log 'Trust rules already present in pg_hba.conf.'
}

# Connecting as scale_user without a password confirms the rule is in effect.
$who = Invoke-Psql -Database 'scale_db' -User 'scale_user' -Command 'SELECT current_user'
if ($who -ne 'scale_user') { throw "Connecting as scale_user returned '$who'." }
Write-Log 'Passwordless connection as scale_user works.'
Write-Log '=== Installation complete ==='
exit 0
