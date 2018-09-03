@set cfg=%~dp0..\Tool\CfgGen\

%cfg%ConfigGen.exe -optMode all ^
-configDir ..\Csv ^
-exportCsv ..\Code\Config ^
-exportLua ..\Code\Scripts\Cfg ^
-exportCSharp ..\Unity\Assets\Editor\Config ^
-exportCsLson ..\Unity\Assets\Editor\Marsh ^

@pause