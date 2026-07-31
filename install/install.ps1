<#
.SYNOPSIS
    Installs the Vesy13 local database on a weighing station.

.DESCRIPTION
    Deploys PostgreSQL, the scale_db schema, the trust rules and the purge task
    in the Windows scheduler. Runs unattended, so the SCCM agent can execute it
    as SYSTEM.

    Every step checks its own state first, so a repeated run on an already
    configured machine passes through and reports success.

    Exit code 0 means the installation is complete, anything else makes SCCM
    report a failure. Progress goes to C:\ProgramData\Vesy13\install-db.log.

    Text in this file stays ASCII: Windows PowerShell 5.1 reads a script without
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
    [string] $PgRoot            = 'C:\Program Files\PostgreSQL\17',
    [string] $DataDir           = 'C:\Program Files\PostgreSQL\17\data',
    [int]    $Port              = 5432,
    [string] $ServiceName       = 'postgresql-x64-17',
    [string] $StateDir          = 'C:\ProgramData\Vesy13'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Windows PowerShell 5.1 fills $PSScriptRoot after the param block is parsed,
# so the script directory is resolved here and defaults are applied below.
$ScriptDir = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path -Parent $MyInvocation.MyCommand.Definition }

if (-not $PostgresInstaller) {
    $found = Get-ChildItem -LiteralPath $ScriptDir -Filter 'postgresql-*windows-x64.exe' -File -ErrorAction SilentlyContinue |
             Sort-Object Name | Select-Object -First 1
    if ($found) { $PostgresInstaller = $found.FullName }
}

$MarkerKey     = 'HKLM:\SOFTWARE\Vesy13\Database'
$SchemaVersion = '1'
$PasswordFile  = Join-Path $StateDir 'postgres_password.txt'
$LogFile       = Join-Path $StateDir 'install-db.log'
$PurgeSql      = Join-Path $StateDir 'purge.sql'
$PurgeCmd      = Join-Path $StateDir 'purge.cmd'
$PurgeLog      = Join-Path $StateDir 'purge.log'
$Psql          = Join-Path $PgRoot 'bin\psql.exe'

$TempTrustTag  = '# Vesy13 temporary superuser trust'
$ScaleTrustTag = '# Vesy13 application access'

# -- Helpers ------------------------------------------------------------------

function Write-Log {
    param([string] $Message)
    $line = '{0:yyyy-MM-dd HH:mm:ss}  {1}' -f (Get-Date), $Message
    Add-Content -LiteralPath $LogFile -Value $line -Encoding UTF8
    Write-Host $line
}

function New-Password {
    # 32 characters from an alphabet that is safe on a command line, in files and
    # inside a single-quoted SQL literal.
    # Create()/GetBytes() exists on .NET Framework, which Windows PowerShell 5.1
    # runs on; the static Fill() helper arrived only with .NET Core.
    $alphabet = 'abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789'
    $bytes    = New-Object byte[] 32
    $rng      = [System.Security.Cryptography.RandomNumberGenerator]::Create()
    try     { $rng.GetBytes($bytes) }
    finally { $rng.Dispose() }
    -join ($bytes | ForEach-Object { $alphabet[$_ % $alphabet.Length] })
}

function Protect-File {
    param([string] $Path)
    # Access is kept for SYSTEM and administrators, inheritance is turned off.
    # Well-known SIDs are used because account names are localized: on a Russian
    # Windows "NT AUTHORITY\SYSTEM" and "BUILTIN\Administrators" carry different
    # names and fail to translate.
    $system = New-Object System.Security.Principal.SecurityIdentifier(
        [System.Security.Principal.WellKnownSidType]::LocalSystemSid, $null)
    $admins = New-Object System.Security.Principal.SecurityIdentifier(
        [System.Security.Principal.WellKnownSidType]::BuiltinAdministratorsSid, $null)

    $acl = Get-Acl -LiteralPath $Path
    $acl.SetAccessRuleProtection($true, $false)
    foreach ($rule in @($acl.Access)) { $acl.RemoveAccessRule($rule) | Out-Null }
    foreach ($sid in @($system, $admins)) {
        $acl.AddAccessRule((New-Object System.Security.AccessControl.FileSystemAccessRule(
            $sid, 'FullControl', 'Allow')))
    }
    Set-Acl -LiteralPath $Path -AclObject $acl
}

function Get-PgDataDir {
    # The service command line carries the real data directory, which survives an
    # install that used different paths than this script's defaults.
    $svc = Get-CimInstance -ClassName Win32_Service -Filter "Name='$ServiceName'" -ErrorAction SilentlyContinue
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
    $hba  = Get-Content -LiteralPath $path
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

    if (-not (Test-Path "$path.vesy13.bak")) { Copy-Item -LiteralPath $path -Destination "$path.vesy13.bak" -Force }
    Set-Content -LiteralPath $path -Value $result -Encoding ASCII
    return $true
}

function Remove-PgHbaRules {
    param([string] $Tag)

    $path  = Get-PgHbaPath
    $hba   = Get-Content -LiteralPath $path
    $start = ($hba | Select-String -Pattern ([regex]::Escape("$Tag BEGIN")) | Select-Object -First 1)
    $stop  = ($hba | Select-String -Pattern ([regex]::Escape("$Tag END"))   | Select-Object -First 1)
    if (-not $start -or -not $stop) { return $false }

    $first = $start.LineNumber - 1
    $last  = $stop.LineNumber - 1
    if ($last -lt $first) { throw "Malformed block '$Tag' in $path." }

    $result = @()
    if ($first -gt 0)           { $result += $hba[0..($first - 1)] }
    if ($last + 1 -lt $hba.Count) { $result += $hba[($last + 1)..($hba.Count - 1)] }
    Set-Content -LiteralPath $path -Value $result -Encoding ASCII
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

if (Test-Path $MarkerKey) {
    $installed = (Get-ItemProperty -Path $MarkerKey).SchemaVersion
    if ($installed -eq $SchemaVersion) {
        Write-Log "Database already installed, schema version $installed. Nothing to do."
        exit 0
    }
    Write-Log "Found schema version $installed, expected $SchemaVersion. Continuing."
}

# -- 1. PostgreSQL ------------------------------------------------------------

if (Get-Service -Name $ServiceName -ErrorAction SilentlyContinue) {
    Write-Log "Service $ServiceName found, skipping the PostgreSQL installer."
}
else {
    Write-Log 'Installing PostgreSQL.'
    if (-not $PostgresInstaller) {
        throw "PostgreSQL installer not found in $ScriptDir. Put postgresql-*windows-x64.exe next to this script or pass -PostgresInstaller."
    }
    if (-not (Test-Path $PostgresInstaller)) { throw "PostgreSQL installer is missing: $PostgresInstaller" }

    # Paths holding a space carry their own quotes: Start-Process joins the
    # array with spaces and quotes nothing.
    $installArgs = @(
        '--mode', 'unattended',
        '--unattendedmodeui', 'none',
        '--prefix', "`"$PgRoot`"",
        '--datadir', "`"$DataDir`"",
        '--serverport', $Port,
        '--disable-components', 'stackbuilder'
    )
    $proc = Start-Process -FilePath $PostgresInstaller -ArgumentList $installArgs -Wait -PassThru
    if ($proc.ExitCode -ne 0) {
        $installerLogs = Get-ChildItem -Path $env:TEMP -Filter '*install*.log' -File -ErrorAction SilentlyContinue |
                         Where-Object { $_.LastWriteTime -gt (Get-Date).AddMinutes(-30) }
        foreach ($log in $installerLogs) {
            $copy = Join-Path $StateDir "postgres-$($log.Name)"
            Copy-Item -LiteralPath $log.FullName -Destination $copy -Force -ErrorAction SilentlyContinue
            Write-Log "Installer log copied to $copy"
        }
        throw "PostgreSQL installer exited with code $($proc.ExitCode)."
    }
    Write-Log 'PostgreSQL installed.'
}

if (-not (Test-Path $Psql)) { throw "psql is missing: $Psql" }

# -- 2. Superuser password ----------------------------------------------------

# The password is set from here rather than taken from the installer, so the
# stored value and the server always agree.

$superPassword = $null
if (Test-Path $PasswordFile) {
    $stored = (Get-Content -LiteralPath $PasswordFile -Raw).Trim()
    if (Test-PgConnection -Password $stored) {
        $superPassword = $stored
        Write-Log 'Stored superuser password accepted.'
    }
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

    Set-Content -LiteralPath $PasswordFile -Value $superPassword -NoNewline -Encoding ASCII
    Protect-File -Path $PasswordFile
    Write-Log "Superuser password stored in $PasswordFile, readable by SYSTEM and administrators."
}

# -- 3. The scale_db schema ---------------------------------------------------

$dbExists = Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword `
    -Command "SELECT 1 FROM pg_database WHERE datname = 'scale_db'"

if ($dbExists -eq '1') {
    Write-Log 'Database scale_db exists, skipping the schema script.'
}
else {
    Write-Log 'Creating database scale_db.'
    Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword `
        -File (Join-Path $ScriptDir 'scale_db.sql') | Out-Null
    Write-Log 'Database scale_db created.'
}

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

# -- 5. Purge task ------------------------------------------------------------

# The purge script and its wrapper live in ProgramData: the SCCM package folder
# exists only while the installation runs.
Copy-Item -LiteralPath (Join-Path $ScriptDir 'purge.sql') -Destination $PurgeSql -Force

$wrapper = @"
@echo off
"$Psql" -X -q -v ON_ERROR_STOP=1 -h 127.0.0.1 -p $Port -U scale_user -d scale_db -f "$PurgeSql" >> "$PurgeLog" 2>&1
"@
Set-Content -LiteralPath $PurgeCmd -Value $wrapper -Encoding ASCII

$taskName = 'Vesy13 purge'
if (Get-ScheduledTask -TaskName $taskName -ErrorAction SilentlyContinue) {
    Unregister-ScheduledTask -TaskName $taskName -Confirm:$false
    Write-Log 'Previous purge task unregistered.'
}

Write-Log 'Registering the purge task.'

# schtasks.exe creates the trigger: New-ScheduledTaskTrigger covers once, daily
# and weekly only, and a monthly schedule expressed as "every four weeks" would
# drift away from the first day of the month.
$schtasks = @(
    '/Create',
    '/TN', $taskName,
    '/TR', "`"$PurgeCmd`"",
    '/SC', 'MONTHLY',
    '/D',  '1',
    '/ST', '03:00',
    '/RU', 'SYSTEM',
    '/RL', 'HIGHEST',
    '/F'
)
& schtasks.exe @schtasks
if ($LASTEXITCODE -ne 0) { throw "Registering the task exited with code $LASTEXITCODE." }

# The defaults schtasks applies skip a run on battery power and drop a run that
# was missed while the station was off, which on a monthly schedule means
# waiting another month.
$settings = New-ScheduledTaskSettingsSet -StartWhenAvailable -AllowStartIfOnBatteries `
                                         -DontStopIfGoingOnBatteries -ExecutionTimeLimit (New-TimeSpan -Hours 1)
Set-ScheduledTask -TaskName $taskName -Settings $settings | Out-Null
Write-Log 'Task settings adjusted: runs when available, battery state ignored.'

$nextRun = (Get-ScheduledTaskInfo -TaskName $taskName).NextRunTime
Write-Log "Task '$taskName' registered, next run $nextRun."

# -- 6. Installation marker ---------------------------------------------------

New-Item -Path $MarkerKey -Force | Out-Null
Set-ItemProperty -Path $MarkerKey -Name 'SchemaVersion' -Value $SchemaVersion
Set-ItemProperty -Path $MarkerKey -Name 'InstalledOn'   -Value (Get-Date -Format 's')
Set-ItemProperty -Path $MarkerKey -Name 'DataDir'       -Value (Get-PgDataDir)

Write-Log '=== Installation complete ==='
exit 0
