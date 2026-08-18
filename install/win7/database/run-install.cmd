@echo off
chcp 65001 >nul

rem Vesy13 - manual run of the station database installer.
rem
rem Text in this file stays ASCII on purpose: cmd.exe tracks its read position
rem in bytes, so a codepage switch inside a file that holds multibyte characters
rem makes it resume mid-character and try to execute the fragments.
rem
rem Arguments are forwarded to install.ps1, for example:
rem     run-install.cmd -PostgresInstaller postgresql-10.6-1-windows-x64.exe
rem
rem SCCM calls install.ps1 directly, with the command line from docs/configuration.md.

net session >nul 2>&1
if errorlevel 1 (
    echo Run this file as administrator.
    pause
    exit /b 1
)

powershell.exe -ExecutionPolicy Bypass -NoProfile -File "%~dp0install.ps1" %*
set RC=%ERRORLEVEL%

echo.
if "%RC%"=="0" (
    echo Done. Log: C:\ProgramData\Vesy13\install-db.log
) else (
    echo Failed with code %RC%. Log: C:\ProgramData\Vesy13\install-db.log
)

pause
exit /b %RC%
