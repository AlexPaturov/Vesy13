@echo off
setlocal EnableExtensions
cd /d "%~dp0.."
echo Vesy13 DeepClean
echo Root: %CD%
echo.
for /d /r "src" %%D in (bin obj) do (
    if exist "%%D" (
        echo Removing: %%D
        rmdir /s /q "%%D"
        if exist "%%D" (
            echo FAILED: %%D
        ) else (
            echo Removed.
        )
    )
)
echo.
echo Remaining bin/obj directories:
for /d /r "src" %%D in (bin obj) do if exist "%%D" echo %%D
echo.
echo DeepClean finished.
pause
