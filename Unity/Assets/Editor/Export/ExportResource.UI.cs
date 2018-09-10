using System.Collections.Generic;
using UnityEditor;

public partial class ExportResource
{
    //冗余资源:Texture,Audio,VFX:图集[RGB|A]
    //上述资源,在每次打包UI时,需要重新打包
    //Android 平台特效有待继续处理

    static string uiPrefabsFolder = "Assets/Interface/Prefabs";
    static string atlasAssetFolder = "Assets/Interface/Atlas";
    static Dictionary<string, string> assetAtlas = new Dictionary<string, string>();
    static Dictionary<string, string> assetFonts = new Dictionary<string, string>();
    static Dictionary<string, string> assetUIs = new Dictionary<string, string>();

    static void ExportSelectedUIs(BuildTarget target)
    {
        UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selection.Length > 0)
        {
            assetAtlas.Clear();
            GetAssetsRecursively(atlasAssetFolder, "*.spriteatlas", "ui/atlas/", null, ref assetAtlas);
            SetAssetBundleName(assetAtlas);

            GetUIPrefabAssets();
            var assets = GetSelectedAssets(assetUIs, selection);

            CombineAssets(new Dictionary<string, string>[] { assetFonts, assetUIs }, ref assets);
            SetAssetBundleName(assets, new string[] { EXT_PNG, EXT_TGA, }, DEPTEX_FOLDER);
            SetAssetBundleName(assets, new string[] { EXT_SHADER }, SHADER_FOLDER);
            SetAssetBundleName(assets, new string[] { EXT_OGG, EXT_MP3 }, AUDIO_FOLDER);

            BuildAssetBundles(target);
        }
    }
    static void ExportAllUIs(BuildTarget target)
    {
        assetAtlas.Clear();
        GetAssetsRecursively(atlasAssetFolder, "*.spriteatlas", "ui/atlas/", null, ref assetAtlas);
        SetAssetBundleName(assetAtlas);

        GetUIPrefabAssets();

        CombineAssets(new Dictionary<string, string>[] { assetFonts }, ref assetUIs);
        SetAssetBundleName(assetUIs, new string[] { EXT_PNG, EXT_TGA, }, DEPTEX_FOLDER);        
        SetAssetBundleName(assetUIs, new string[] { EXT_SHADER }, SHADER_FOLDER);
        SetAssetBundleName(assetUIs, new string[] { EXT_OGG, EXT_MP3 }, AUDIO_FOLDER);

        BuildAssetBundles(target);
    }
    static void GetUIPrefabAssets()
    {
        assetUIs.Clear();
        GetAssetsRecursively(uiPrefabsFolder, "*.prefab", "ui/", null, ref assetUIs);
    }
    static void CombineAssets(Dictionary<string, string>[] dics, ref Dictionary<string, string> assets)
    {
        for (int i = 0; i < dics.Length; ++i)
        {
            Dictionary<string, string> dic = dics[i];
            foreach (KeyValuePair<string, string> pair in dic)
            {
                assets[pair.Key] = pair.Value;
            }
        }
    }


    [MenuItem("Assets/Build Selected for Windows/UI")]
    static void ExportSelectedUIsForWindows()
    {
        ExportSelectedUIs(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Assets/Build Selected for Android/UI")]
    static void ExportSelectedUIsForAndroid()
    {
        //ForAndroidSfxProcessBug(BuildTarget.Android);
        ExportSelectedUIs(BuildTarget.Android);
    }
    [MenuItem("Assets/Build Selected for iOS/UI")]
    static void ExportSelectedUIsForiOS()
    {
        ExportSelectedUIs(BuildTarget.iOS);
    }
    [MenuItem("Build Windows/Build UIs for Windows")]
    static void ExportAllUIsForWindows()
    {
        ExportAllUIs(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Build Android/Build UIs for Android")]
    static void ExportAllUIsForAndroid()
    {
        //ForAndroidSfxProcessBug(BuildTarget.Android);
        ExportAllUIs(BuildTarget.Android);
    }
    [MenuItem("Build iOS/Build UIs for iOS")]
    static void ExportAllUIsForiOS()
    {
        ExportAllUIs(BuildTarget.iOS);
    }
}
