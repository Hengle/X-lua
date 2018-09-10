@cd %~dp0..\Tool\CfgGen\
@set rootPath=..\..

ConfigGen.exe -optMode all ^
-configDir %rootPath%\Csv ^
-dataDir %rootPath%\GamePlayer\Config ^
-luaDir %rootPath%\Code\Scripts\Cfg ^
-codeDir %rootPath%\Unity\Assets\Editor\Code ^
-xmlCodeDir %rootPath%\Unity\Assets\Editor\XmlCode


