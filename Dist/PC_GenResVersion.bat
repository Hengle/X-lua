@echo off
@SET currpath=..\GamePlayer
echo %currpath%
cd %currpath%

if exist resmd5.txt (
del/q resmd5.txt
)

..\Tool\CSharpTool\ToolFactory.exe -resversion .\Data .\
