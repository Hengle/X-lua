@echo off
@SET currpath=%~dp0 
echo %currpath%

if exist .\Data (
rd/s/q .\Data
)
mkdir .\Data
xcopy ..\Code\Scripts .\Data\scripts\ /e/s/Y/q
xcopy ..\GamePlayer\Data\config .\Data\config\ /e/s/Y/q

xcopy ..\GamePlayer\hasdownload.txt .\Data\ /Y/q
xcopy ..\GamePlayer\resmd5.txt .\Data\ /Y/q
xcopy ..\GamePlayer\version.txt .\Data\ /Y/q


if exist data.zip (
del/q data.zip
)

..\Tool\WinRAR.exe a -r data.zip .\Data
xcopy data.zip ..\Unity\Assets\StreamingAssets\ /Y/q

pause