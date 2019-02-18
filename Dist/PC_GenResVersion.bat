@echo off
set currpath=%~dp0PC
echo %currpath%
cd %currpath%

if exist resmd5.txt (
del/q resmd5.txt
)

rem xcopy ..\..\Code\Scripts .\Data\scripts\ /e/s/Y/q

REM 当前未对Scripts进行md5记录
set tool=..\..\Tool\CSharpTool\ToolFactory.exe
%tool% -resversion .\Data .\


pause