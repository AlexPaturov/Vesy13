<#
.SYNOPSIS
    Installs the published Vesy13 application for SCCM deployment.

.DESCRIPTION
    Run as SYSTEM or an administrator. The published application is copied to
    Program Files, state is stored in ProgramData, and the Users group receives
    Modify rights only to the state directory. SCCM can detect the application
    through HKLM:\SOFTWARE\Vesy13\Application, value Version.
#>
[CmdletBinding()]
param(
    [string] $SourcePath,
    [string] $InstallPath = (Join-Path $env:ProgramFiles "Vesy13"),
    [string] $StatePath = (Join-Path $env:ProgramData "Vesy13")
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$ScriptDir = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path -Parent $MyInvocation.MyCommand.Definition }
if (-not $SourcePath) { $SourcePath = Join-Path $ScriptDir "publish" }

$MarkerKey = "HKLM:\SOFTWARE\Vesy13\Application"
$ExecutableName = "Vesy13.exe"

function Copy-ApplicationFiles {
    param([string] $Source, [string] $Destination)

    $robocopyArguments = @($Source, $Destination, "/E", "/COPY:DAT", "/DCOPY:DAT", "/R:2", "/W:2", "/NFL", "/NDL", "/NP")
    & robocopy.exe @robocopyArguments | Out-Host
    if ($LASTEXITCODE -gt 7) {
        throw "robocopy failed with exit code $LASTEXITCODE."
    }
}

if (-not (Test-Path -LiteralPath $SourcePath -PathType Container)) {
    throw "Publish directory is missing: $SourcePath"
}

$sourceExecutable = Join-Path $SourcePath $ExecutableName
if (-not (Test-Path -LiteralPath $sourceExecutable -PathType Leaf)) {
    throw "Published executable is missing: $sourceExecutable"
}

New-Item -ItemType Directory -Path $InstallPath -Force | Out-Null
Copy-ApplicationFiles -Source $SourcePath -Destination $InstallPath

New-Item -ItemType Directory -Path $StatePath -Force | Out-Null
& icacls.exe $StatePath "/grant" "*S-1-5-32-545:(OI)(CI)M" | Out-Host
if ($LASTEXITCODE -ne 0) {
    throw "icacls failed with exit code $LASTEXITCODE."
}

$startMenuPath = Join-Path $env:ProgramData "Microsoft\Windows\Start Menu\Programs"
New-Item -ItemType Directory -Path $startMenuPath -Force | Out-Null
$shortcutPath = Join-Path $startMenuPath "Vesy13.lnk"
$shell = New-Object -ComObject WScript.Shell
try {
    $shortcut = $shell.CreateShortcut($shortcutPath)
    $shortcut.TargetPath = Join-Path $InstallPath $ExecutableName
    $shortcut.WorkingDirectory = $InstallPath
    $shortcut.Description = "Vesy13"
    $shortcut.Save()
}
finally {
    [Runtime.InteropServices.Marshal]::ReleaseComObject($shell) | Out-Null
}

$version = (Get-Item -LiteralPath (Join-Path $InstallPath $ExecutableName)).VersionInfo.ProductVersion
if ([string]::IsNullOrWhiteSpace($version)) { $version = "0.0.0.0" }
New-Item -Path $MarkerKey -Force | Out-Null
New-ItemProperty -Path $MarkerKey -Name "Version" -Value $version -PropertyType String -Force | Out-Null
New-ItemProperty -Path $MarkerKey -Name "InstallPath" -Value $InstallPath -PropertyType String -Force | Out-Null

Write-Host "Vesy13 $version installed to $InstallPath."
