<#
.SYNOPSIS
    Установка локальной базы Vesy13 на весовую станцию.

.DESCRIPTION
    Разворачивает PostgreSQL, схему scale_db, правила trust и задание очистки
    в планировщике Windows. Рассчитан на запуск агентом SCCM от имени SYSTEM,
    без интерактива.

    Каждый шаг проверяет своё состояние перед выполнением, поэтому повторный
    запуск на уже настроенной машине проходит вхолостую и завершается успехом.

    Код возврата 0 — установка завершена, отличный от нуля — SCCM показывает
    сбой. Ход работы пишется в C:\ProgramData\Vesy13\install-db.log.

.PARAMETER PostgresInstaller
    Инсталлятор PostgreSQL от EDB, лежит рядом со скриптом в составе пакета.

.EXAMPLE
    powershell.exe -ExecutionPolicy Bypass -NoProfile -File install.ps1
#>

[CmdletBinding()]
param(
    [string] $PostgresInstaller = (Join-Path $PSScriptRoot 'postgresql-17.10-1-windows-x64.exe'),
    [string] $PgRoot            = 'C:\Program Files\PostgreSQL\17',
    [string] $DataDir           = 'C:\Program Files\PostgreSQL\17\data',
    [int]    $Port              = 5432,
    [string] $ServiceName       = 'postgresql-x64-17',
    [string] $StateDir          = 'C:\ProgramData\Vesy13'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$MarkerKey     = 'HKLM:\SOFTWARE\Vesy13\Database'
$SchemaVersion = '1'
$PasswordFile  = Join-Path $StateDir 'postgres_password.txt'
$LogFile       = Join-Path $StateDir 'install-db.log'
$PurgeSql      = Join-Path $StateDir 'purge.sql'
$PurgeCmd      = Join-Path $StateDir 'purge.cmd'
$PurgeLog      = Join-Path $StateDir 'purge.log'
$Psql          = Join-Path $PgRoot 'bin\psql.exe'

# ── Вспомогательное ───────────────────────────────────────────────────────────

function Write-Log {
    param([string] $Message)
    $line = '{0:yyyy-MM-dd HH:mm:ss}  {1}' -f (Get-Date), $Message
    Add-Content -LiteralPath $LogFile -Value $line -Encoding UTF8
    Write-Host $line
}

function New-Password {
    # 32 символа из алфавита, безопасного для командной строки и файлов конфигурации.
    $alphabet = 'abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789'
    $bytes    = [byte[]]::new(32)
    [System.Security.Cryptography.RandomNumberGenerator]::Fill($bytes)
    -join ($bytes | ForEach-Object { $alphabet[$_ % $alphabet.Length] })
}

function Protect-File {
    param([string] $Path)
    # Доступ остаётся у SYSTEM и администраторов, наследование отключается.
    $acl = New-Object System.Security.AccessControl.FileSecurity
    $acl.SetAccessRuleProtection($true, $false)
    foreach ($account in 'NT AUTHORITY\SYSTEM', 'BUILTIN\Administrators') {
        $acl.AddAccessRule((New-Object System.Security.AccessControl.FileSystemAccessRule(
            $account, 'FullControl', 'Allow')))
    }
    Set-Acl -LiteralPath $Path -AclObject $acl
}

function Invoke-Psql {
    param(
        [string] $Database,
        [string] $User,
        [string] $File,
        [string] $Command,
        [string] $Password
    )
    $args = @('-v', 'ON_ERROR_STOP=1', '-X', '-q', '-h', '127.0.0.1', '-p', $Port, '-U', $User, '-d', $Database)
    if ($File)    { $args += @('-f', $File) }
    if ($Command) { $args += @('-t', '-A', '-c', $Command) }

    # Пароль передаётся окружением процесса: в командной строке он был бы виден
    # в списке процессов и в логах SCCM.
    $previous = $env:PGPASSWORD
    $env:PGPASSWORD       = $Password
    $env:PGCLIENTENCODING = 'UTF8'
    try {
        $output = & $Psql @args 2>&1
        if ($LASTEXITCODE -ne 0) {
            throw "psql завершился с кодом ${LASTEXITCODE}: $output"
        }
        return ($output | Out-String).Trim()
    }
    finally {
        $env:PGPASSWORD = $previous
    }
}

# ── Подготовка ────────────────────────────────────────────────────────────────

New-Item -ItemType Directory -Path $StateDir -Force | Out-Null
Write-Log '=== Установка базы Vesy13 ==='

if (Test-Path $MarkerKey) {
    $installed = (Get-ItemProperty -Path $MarkerKey).SchemaVersion
    if ($installed -eq $SchemaVersion) {
        Write-Log "База уже установлена, версия схемы $installed. Установка завершена."
        exit 0
    }
    Write-Log "Найдена версия схемы $installed, ожидается $SchemaVersion. Продолжаю."
}

# ── 1. PostgreSQL ─────────────────────────────────────────────────────────────

if (Get-Service -Name $ServiceName -ErrorAction SilentlyContinue) {
    Write-Log "Служба $ServiceName найдена, установка PostgreSQL пропущена."
    if (-not (Test-Path $PasswordFile)) {
        throw "Служба $ServiceName есть, а $PasswordFile отсутствует — пароль суперпользователя неизвестен. Задайте его вручную и повторите."
    }
    $superPassword = Get-Content -LiteralPath $PasswordFile -Raw
}
else {
    Write-Log 'Устанавливаю PostgreSQL.'
    if (-not (Test-Path $PostgresInstaller)) { throw "Инсталлятор PostgreSQL отсутствует: $PostgresInstaller" }

    $superPassword = New-Password
    Set-Content -LiteralPath $PasswordFile -Value $superPassword -NoNewline -Encoding ASCII
    Protect-File -Path $PasswordFile
    Write-Log "Пароль суперпользователя сохранён в $PasswordFile, доступ у SYSTEM и администраторов."

    $installArgs = @(
        '--mode', 'unattended',
        '--unattendedmodeui', 'none',
        '--superpassword', $superPassword,
        '--prefix', $PgRoot,
        '--datadir', $DataDir,
        '--serverport', $Port,
        '--disable-components', 'stackbuilder'
    )
    $proc = Start-Process -FilePath $PostgresInstaller -ArgumentList $installArgs -Wait -PassThru
    if ($proc.ExitCode -ne 0) { throw "Инсталлятор PostgreSQL завершился с кодом $($proc.ExitCode)." }
    Write-Log 'PostgreSQL установлен.'
}

if (-not (Test-Path $Psql)) { throw "psql отсутствует: $Psql" }

# ── 2. Схема scale_db ─────────────────────────────────────────────────────────

$dbExists = Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword `
    -Command "SELECT 1 FROM pg_database WHERE datname = 'scale_db'"

if ($dbExists -eq '1') {
    Write-Log 'База scale_db существует, скрипт схемы пропущен.'
}
else {
    Write-Log 'Создаю базу scale_db.'
    Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword `
        -File (Join-Path $PSScriptRoot 'scale_db.sql') | Out-Null
    Write-Log 'База scale_db создана.'
}

# ── 3. Правила trust ──────────────────────────────────────────────────────────

$hbaPath = Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword -Command 'SHOW hba_file'
$hba     = Get-Content -LiteralPath $hbaPath

if ($hba -match 'scale_db\s+scale_user') {
    Write-Log 'Правила trust уже в pg_hba.conf.'
}
else {
    Write-Log "Дописываю правила trust в $hbaPath."
    $rules = @(
        '# Vesy13: приложение подключается к локальной базе под ролью без пароля.',
        'host    scale_db    scale_user    127.0.0.1/32    trust',
        'host    scale_db    scale_user    ::1/128         trust'
    )

    # Правила ставятся выше общих: pg_hba.conf применяет первое совпавшее.
    $anchor = ($hba | Select-String -Pattern '^\s*host\s+all\s+all' | Select-Object -First 1).LineNumber
    if ($anchor) {
        $index  = $anchor - 1
        $result = @($hba[0..($index - 1)]) + $rules + @($hba[$index..($hba.Count - 1)])
    }
    else {
        $result = $rules + $hba
    }

    Copy-Item -LiteralPath $hbaPath -Destination "$hbaPath.vesy13.bak" -Force
    Set-Content -LiteralPath $hbaPath -Value $result -Encoding ASCII
    Invoke-Psql -Database 'postgres' -User 'postgres' -Password $superPassword -Command 'SELECT pg_reload_conf()' | Out-Null
    Write-Log 'Правила добавлены, конфигурация перечитана.'
}

# Подключение под scale_user без пароля подтверждает, что правило действует.
$who = Invoke-Psql -Database 'scale_db' -User 'scale_user' -Command 'SELECT current_user'
if ($who -ne 'scale_user') { throw "Подключение под scale_user вернуло '$who'." }
Write-Log 'Подключение под scale_user без пароля работает.'

# ── 4. Задание очистки ────────────────────────────────────────────────────────

# Скрипт очистки и обёртка к нему живут в ProgramData: папка пакета SCCM
# существует только на время установки.
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'purge.sql') -Destination $PurgeSql -Force

# Обёртка задаёт кодировку клиента: консоль Windows работает не в UTF-8,
# а в purge.sql есть кириллица.
$wrapper = @"
@echo off
set PGCLIENTENCODING=UTF8
"$Psql" -X -q -v ON_ERROR_STOP=1 -h 127.0.0.1 -p $Port -U scale_user -d scale_db -f "$PurgeSql" >> "$PurgeLog" 2>&1
"@
Set-Content -LiteralPath $PurgeCmd -Value $wrapper -Encoding ASCII

$taskName = 'Vesy13 purge'
if (Get-ScheduledTask -TaskName $taskName -ErrorAction SilentlyContinue) {
    Unregister-ScheduledTask -TaskName $taskName -Confirm:$false
    Write-Log 'Прежнее задание очистки снято с регистрации.'
}

Write-Log 'Регистрирую задание очистки.'
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
if ($LASTEXITCODE -ne 0) { throw "Регистрация задания завершилась с кодом $LASTEXITCODE." }

$nextRun = (Get-ScheduledTaskInfo -TaskName $taskName).NextRunTime
Write-Log "Задание «$taskName» зарегистрировано, следующий запуск — $nextRun."

# ── 6. Метка установки ────────────────────────────────────────────────────────

New-Item -Path $MarkerKey -Force | Out-Null
Set-ItemProperty -Path $MarkerKey -Name 'SchemaVersion' -Value $SchemaVersion
Set-ItemProperty -Path $MarkerKey -Name 'InstalledOn'   -Value (Get-Date -Format 's')
Set-ItemProperty -Path $MarkerKey -Name 'DataDir'       -Value $DataDir

Write-Log '=== Установка завершена ==='
exit 0
