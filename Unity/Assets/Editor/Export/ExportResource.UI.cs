using System.Collections.Generic;
using UnityEditor;

public partial class ExportResource
{
    //冗余资源:Texture,Audio,VFX:图集[RGB|A]
    //上述资源,在每次打包UI时,需要重新打包
    //Android 平台特效有待继续处理

    //-----问题
    //1.界面图集
    //2.界面描述
    //3.界面字体(艺术字和动态字体)
    //4.音频                              -另作处理
    //5.动态加载图片(大图)                -另作处理
    //6.2D特效/3D特效?麻烦!!

    static string guiFolder = "Assets/Interface/GUI";
    static string atlasFolder = "Assets/Interface/Atlas";
    static Dictionary<string, string> assetFonts = new Dictionary<string, string>()
    { {"Assets/Interface/Font/HYQiHei-55JW.otf", "ui/atlas/dlgfont.ui"} };//Unity资源路径 -> Bundle资源路径

    //rgb:atlas0.png        -RGB通道贴图
    //a:atlas0!a.png        -A通道贴图
    //des:fui.bytes         -UI描述文件
    static void SetAtlasPackage(string srcFolder, string dstFolder)
    {
        Dictionary<string, string> assets = new Dictionary<string, string>();
        GetAssetsRecursively(srcFolder, "*_fui.bytes", dstFolder, null, ref assets);
        GetAssetsRecursively(srcFolder, "*_atlas0.png", dstFolder, null, ref assets);
#if UNITY_ANDROID
        GetAssetsRecursively(srcFolder, "*_atlas0!a.png", dstFolder, null, ref assets);
#endif
        SetAssetBundleName(assets, new string[] { EXT_PNG, EXT_TGA }, DEPTEX_FOLDER);
    }
    static Dictionary<string, string> GetGUIPackage(string srcFolder, string dstFolder)
    {
        Dictionary<string, string> assets = new Dictionary<string, string>();
        GetAssetsRecursively(srcFolder, "*_fui.bytes", dstFolder, null, ref assets);
        return assets;
    }

    static void ExportSelectedUIs(BuildTarget target)
    {
        UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selection.Length > 0)
        {
            SetAtlasPackage(atlasFolder, "ui/atlas/");

            var assetUIs = GetGUIPackage(guiFolder, "ui/");
            var assets = GetSelectedAssets(assetUIs, selection);

            CombineAssets(new Dictionary<string, string>[] { assetFonts }, ref assets);
            SetAssetBundleName(assets, new string[] { EXT_PNG, EXT_TGA }, DEPTEX_FOLDER);

            BuildAssetBundles(target);
        }
    }
    static void ExportAllUIs(BuildTarget target)
    {
        SetAtlasPackage(atlasFolder, "ui/atlas/");
        var assets = GetGUIPackage(guiFolder, "ui/");
        CombineAssets(new Dictionary<string, string>[] { assetFonts }, ref assets);
        SetAssetBundleName(assets, new string[] { EXT_PNG, EXT_TGA }, DEPTEX_FOLDER);

        BuildAssetBundles(target);
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
