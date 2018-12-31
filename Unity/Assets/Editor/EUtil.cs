using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EUtil
{
    public static string FilePath2UnityPath(string path)
    {
        return path.Replace(Application.dataPath, "Assets");
    }
    public static string UnityPath2FilePath(string path)
    {
        if (path.IndexOf("Assets") != 0)
        {
            Debug.LogError("非UnityPath - " + path);
            return path;
        }
        return Application.dataPath + path.Substring(0, 6);
    }
    /// <summary>
    /// \ 转换 /
    /// </summary>
    public static string StandardlizePath(string path)
    {
        return path.Replace(@"\", @"/");
    }

    /// <summary>
    /// 子资源不可重名! key-文件名    value-路径
    /// </summary>
    public static void GetAssetsInSubFolderRecursively(string srcFolder, string searchPattern, ref Dictionary<string, string> fileName2Path, List<string> sb = null)
    {
        string searchFolder = StandardlizePath(srcFolder);
        if (!Directory.Exists(searchFolder))
            return;

        string[] files = Directory.GetFiles(searchFolder, searchPattern);
        foreach (string oneFile in files)
        {
            string srcFile = StandardlizePath(oneFile);
            if (!File.Exists(srcFile))
                continue;
            string srcFileName = Path.GetFileNameWithoutExtension(srcFile);
            if (!fileName2Path.ContainsKey(srcFileName))
            {
                fileName2Path.Add(srcFileName, srcFile);
            }
            else
            {
                string error = "资源名重复 filename:" + srcFileName;
                UnityEngine.Debug.LogError(error);
                if (sb != null && !sb.Contains(error))
                {
                    sb.Add(error);
                }

            }

        }

        string[] dirs = Directory.GetDirectories(searchFolder);
        foreach (string oneDir in dirs)
        {
            GetAssetsInSubFolderRecursively(oneDir, searchPattern, ref fileName2Path, sb);
        }
    }

    /// <summary>
    /// 按扩展名筛选文件
    /// </summary>
    /// <param name="path">目录路径</param>
    /// <param name="exts">需要过滤的扩展名</param>
    /// <param name="searchOption">查询模式</param>
    /// <returns></returns>
    public static string[] GetFiles(string path, string[] exts, SearchOption searchOption)
    {
        List<string> list = new List<string>();
        string[] fs = Directory.GetFiles(path, "*.*", searchOption);
        HashSet<string> hash = new HashSet<string>(exts);
        for (int i = 0; i < fs.Length; i++)
        {
            string ext = Path.GetExtension(fs[i]);
            if (!hash.Contains(ext))
                list.Add(fs[i]);
        }
        return list.ToArray();
    }

}
