@cd %~dp0..\Tool\CfgGen\
@set rootPath=..\..

ConfigGen.exe -optMode all ^
-configDir %rootPath%\Csv ^
-xmlCodeDir %rootPath%\Unity\Assets\ModelEditor\Editor\XmlCode ^
-export %rootPath%\Csv\skill.exp


