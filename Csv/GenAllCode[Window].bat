@cd %~dp0..\Tool\CfgGen\
@set rootPath=..\..

ConfigGen.exe -optMode all ^
-configXml %rootPath%\Csv\Cfg.xml ^
-dataDir %rootPath%\GamePlayer\config\csv ^
-luaDir %rootPath%\Code\Scripts\Cfg ^
-codeDir %rootPath%\Unity\Assets\Source\Config\Csv



