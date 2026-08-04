<#
.SYNOPSIS
    Removes the Vesy13 application deployed by Install-Vesy13App.ps1.

.DESCRIPTION
    Settings in ProgramData are retained by default so an application upgrade or
    reinstall does not lose the administrator password or calibration cache.
#>
[CmdletBinding()]
param(
    [string] $InstallPath = (Join-Path $env:ProgramFiles "Vesy13"),
    [string] $StatePath = (Join-Path $env:ProgramData "Vesy13"),
    [switch] $RemoveState
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$process = Get-Process -Name "Vesy13" -ErrorAction SilentlyContinue
if ($process) {
    throw "Vesy13 is running. Close the application before uninstalling it."
}

$shortcutPath = Join-Path $env:ProgramData "Microsoft\Windows\Start Menu\Programs\Vesy13.lnk"
Remove-Item -LiteralPath $shortcutPath -Force -ErrorAction SilentlyContinue
Remove-Item -LiteralPath $InstallPath -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -LiteralPath "HKLM:\SOFTWARE\Vesy13\Application" -Recurse -Force -ErrorAction SilentlyContinue

if ($RemoveState) {
    Remove-Item -LiteralPath $StatePath -Recurse -Force -ErrorAction SilentlyContinue
}

Write-Host "Vesy13 application removed."
