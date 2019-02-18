@echo off
set target=%~dp0PC
echo %target%

if exist .\Data (
rd/s/q .\Data
)
mkdir .\Data
xcopy %target%\Data\scripts .\Data\scripts\ /e/s/Y/q
xcopy %target%\Data\config .\Data\config\ /e/s/Y/q

xcopy %target%\hasdownload.txt .\Data\ /Y/q
xcopy %target%\resmd5.txt .\Data\ /Y/q
xcopy %target%\version.txt .\Data\ /Y/q


if exist data.zip (
del/q data.zip
)

..\Tool\WinRAR.exe a -r data.zip .\Data
xcopy data.zip ..\GamePlayer\ /Y/q

pause