using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class ExportResource
{
    const string EXT_SHADER = ".shader";
    const string EXT_PNG = ".png";
    const string EXT_TGA = ".tga";
    const string EXT_MP3 = ".mp3";
    const string EXT_OGG = ".ogg";

    const string UI_FOLDER = "ui/";
    const string SHADER_FOLDER = "shaders/";
    const string DEPTEX_FOLDER = "texture/t_";
    const string AUDIO_FOLDER = "audio/a_";

    static BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle
        | BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle;

    /// <summary>
    /// 平台文件目录名
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    static string GetPlatfomrPath(BuildTarget target)
    {
        string platformPath = string.Empty;
        switch (target)
        {
            case BuildTarget.Android:
                platformPath = "Dist/Android";
                break;
            case BuildTarget.iOS:
                platformPath = "Dist/IOS";
                break;
            default:
                {
                    platformPath = "GamePlayer";
                }
                break;
        }
        return platformPath;
    }
    /// <summary>
    /// AB包路径
    /// </summary>
    /// <param name="target">平台</param>
    /// <param name="relativePath">平台内部相对路径</param>
    /// <returns></returns>
    public static string GetBundleSavePath(BuildTarget target, string relativePath)
    {
        string path;
        switch (target)
        {
            case BuildTarget.Android:
            case BuildTarget.iOS:
                path = string.Format("{0}/../../{1}/{2}", Application.dataPath, GetPlatfomrPath(target), relativePath);
                break;
            default:
                path = string.Format("{0}/../../{1}/Data/{2}", Application.dataPath, GetPlatfomrPath(target), relativePath);
                break;
        }
        return path;
    }
    /// <summary>
    /// 对应分类资源目录
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static string GetBundleSaveDir(BuildTarget target)
    {
        return string.Format("{0}/../../{1}/", Application.dataPath, GetPlatfomrPath(target));
    }

    /// <summary>
    /// 设置主体资源AB包名及依赖资源包名;[分离依赖单独打包]
    /// </summary>
    /// <param name="assets">资源</param>
    /// <param name="depFormats">依赖资源格式,此格式单独打AB包</param>
    /// <param name="depPath">依赖存储相对路径</param>
    /// <param name="bPrefix">是否修正前缀</param>
    static void SetAssetBundleName(Dictionary<string, string> assets, string[] depFormats, string depPath)
    {
        AssetImporter importer = null;
        foreach (KeyValuePair<string, string> pair in assets)
        {
            string[] dependencies = AssetDatabase.GetDependencies(pair.Key);
            foreach (string sdep in dependencies)
            {
                string dep = sdep.ToLower();
                foreach (string format in depFormats)
                {
                    if (dep.EndsWith(format))
                    {
                        importer = AssetImporter.GetAtPath(dep);

                        string newName = string.Format("{0}{1}.bundle", depPath, Path.GetFileNameWithoutExtension(dep));
                        newName = newName.Replace(" ", "");
                        newName = newName.ToLower();
                        if (importer.assetBundleName == null || importer.assetBundleName != newName)
                        {
                            importer.assetBundleName = newName;
                        }
                    }
                }
            }
            importer = AssetImporter.GetAtPath(pair.Key);
            if (importer == null)
            {
                Debug.LogError("未发现资源: " + pair.Key);
            }
            if (importer.assetBundleName == null || importer.assetBundleName != pair.Value)
            {
                importer.assetBundleName = pair.Value;
            }
        }
    }
    /// <summary>
    /// 设置AB包名,无依赖资源
    /// </summary>
    /// <param name="assets">资源</param>
    static void SetAssetBundleName(Dictionary<string, string> assets)
    {
        AssetImporter importer = null;
        foreach (KeyValuePair<string, string> pair in assets)
        {
            importer = AssetImporter.GetAtPath(pair.Key);
            if (importer.assetBundleName == null || importer.assetBundleName != pair.Value)
            {
                importer.assetBundleName = pair.Value;
            }
        }
    }
    /// <summary>
    /// 构建AB包
    /// </summary>
    static void BuildAssetBundles(BuildTarget target)
    {
        string dir = GetBundleSaveDir(target);
        Directory.CreateDirectory(Path.GetDirectoryName(dir));

        BuildPipeline.BuildAssetBundles(dir, options, target);

        string[] allNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < allNames.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(allNames[i], true);
        }
    }
    /// <summary>
    /// 标准化路径,'\'转化'/'
    /// </summary>
    /// <param name="obj">资源对象</param>
    /// <returns></returns>
    static string StandardlizePath(UnityEngine.Object obj)
    {
        return StandardlizePath(AssetDatabase.GetAssetPath(obj));
    }
    /// <summary>
    /// 标准化路径,'\'转化'/'
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <returns></returns>
    public static string StandardlizePath(string path)
    {
        string pathReplace = path.Replace(@"\", @"/");
        string pathLower = pathReplace.ToLower();
        return pathLower;
    }
    /// <summary>
    /// 限定选择的资源必须在指定范围内
    /// </summary>
    /// <param name="allassets">所有资源</param>
    /// <param name="selection">选择的资源</param>
    /// <returns></returns>
    static Dictionary<string, string> GetSelectedAssets(Dictionary<string, string> allassets, Object[] selection)
    {
        Dictionary<string, string> assets = new Dictionary<string, string>();
        for (int i = 0; i < selection.Length; ++i)
        {
            var assetpath = AssetDatabase.GetAssetPath(selection[i]);
            var assetstandardlizepath = StandardlizePath(selection[i]);
            if (allassets.ContainsKey(assetpath))
            {
                assets[assetpath] = allassets[assetpath];
            }
            else if (allassets.ContainsKey(assetstandardlizepath))
            {
                assets[assetstandardlizepath] = allassets[assetstandardlizepath];
            }
        }
        return assets;
    }
    /// <summary>
    /// 获取目录下指定格式资源,包含子目录
    /// </summary>
    /// <param name="srcFolder"></param>
    /// <param name="searchPattern"></param>
    /// <param name="dstFolder"></param>
    /// <param name="prefix"></param>
    /// <param name="assets"></param>
    static void GetAssetsRecursively(string srcFolder, string searchPattern, string dstFolder, string prefix, ref Dictionary<string, string> assets)
    {
        string searchFolder = StandardlizePath(srcFolder);
        if (!Directory.Exists(searchFolder))
            return;

        string suffix = "bundle";
        string srcDir = searchFolder;
        string[] files = Directory.GetFiles(srcDir, searchPattern);
        foreach (string oneFile in files)
        {
            string srcFile = StandardlizePath(oneFile);
            if (!File.Exists(srcFile))
                continue;

            string dstFile;
            if (string.IsNullOrEmpty(prefix))
            {
                dstFile = Path.Combine(dstFolder, string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(srcFile), suffix));
            }
            else
            {
                dstFile = Path.Combine(dstFolder, string.Format("{0}_{1}.{2}", prefix, Path.GetFileNameWithoutExtension(srcFile), suffix));
            }
            dstFile = StandardlizePath(dstFile);
            assets[srcFile] = dstFile;

            //UnityEngine.Debug.Log("srcFile: " + srcFile + " => dstFile: " + dstFile);
        }

        string[] dirs = Directory.GetDirectories(searchFolder);
        foreach (string oneDir in dirs)
        {
            GetAssetsRecursively(oneDir, searchPattern, dstFolder, prefix, ref assets);
        }
    }

    /// <summary>
    /// Android平台时,图集更换材质
    /// </summary>
    static void ReplaceDefaultMatAndShaders()
    {

    }
}
