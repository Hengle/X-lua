@cd %~dp0..\Tool\CfgGen\
@set rootPath=..\..

ConfigGen.exe -optMode part ^
-configDir %rootPath%\Csv ^
-dataDir %rootPath%\Code\Config ^
-luaDir %rootPath%\Code\Scripts\Config ^
-codeDir %rootPath%\Unity\Assets\Editor\Code ^
-xmlCodeDir %rootPath%\Unity\Assets\Editor\XmlCode


