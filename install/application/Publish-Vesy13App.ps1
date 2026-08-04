<#
.SYNOPSIS
    Publishes a self-contained win-x64 build into the application package.
#>
[CmdletBinding()]
param(
    [string] $ProjectPath,
    [string] $OutputPath
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# Windows PowerShell 5.1 initializes $PSScriptRoot only after param is parsed.
$ScriptDir = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path -Parent $MyInvocation.MyCommand.Definition }
if (-not $ProjectPath) { $ProjectPath = Join-Path $ScriptDir "..\..\Vesy13.csproj" }
if (-not $OutputPath)  { $OutputPath = Join-Path $ScriptDir "publish" }

& dotnet publish $ProjectPath -c Release -r win-x64 --self-contained true -o $OutputPath
if ($LASTEXITCODE -ne 0) {
    throw "dotnet publish failed with exit code $LASTEXITCODE."
}
