using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;
using UObject = UnityEngine.Object;

namespace Game
{
    public class Util
    {
        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        public static string md5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; i++)
            {
                destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
            }
            destString = destString.PadLeft(32, '0');
            return destString;
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        public static string md5file(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }
        public static string StandardlizePath(string path)
        {
            return path.Replace(@"\", @"/");
        }
        public static void SetResolution(int resolution)
        {
#if UNITY_STANDALONE_WIN
            Screen.SetResolution(1280, 720, false);
#else
    if (resolution < 720)
            return;
        if (resolution > 1080)
            return;

        int w = Screen.width;
        int h = Screen.height;
        if (h > resolution)
        {
            int width = resolution * w / h;
            int height = resolution;
            Screen.SetResolution(width, height, true);
        }
#endif
        }

        /// <summary>
        /// 清除所有子节点
        /// </summary>
        public static void ClearChild(Transform go)
        {
            if (go == null) return;
            for (int i = go.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(go.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 热更后资源读取目录
        /// 取得数据存放目录-可读写目录(可包含美术资源,脚本,配置)
        /// </summary>
        public static string DataPath
        {
            get
            {
                string path = string.Empty;
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                    case RuntimePlatform.IPhonePlayer:
                        path = Application.persistentDataPath + "/";
                        break;
                    case RuntimePlatform.WindowsPlayer:
                        path = "file://" + Application.persistentDataPath + "/";
                        break;
                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.OSXEditor:
                        path = Application.dataPath + "/../../GamePlayer/";
                        break;
                }
                return path;
            }
        }
        /// <summary>
        /// 应用程序内容路径(包含首次所有美术资源)
        /// </summary>
        public static string StreamingPath
        {
            get
            {
                string path = string.Empty;
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        path = "jar:file://" + Application.dataPath + "!/assets/";
                        break;
                    case RuntimePlatform.IPhonePlayer:
                        path = Application.dataPath + "/Raw/";
                        break;
                    case RuntimePlatform.WindowsPlayer:
                        path = "file://" + Application.streamingAssetsPath + "/";
                        break;
                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.OSXEditor:
                        path = Application.dataPath + "/../../GamePlayer/";
                        break;
                }
                return path;
            }
        }


        /// <summary>
        /// 网络可用
        /// </summary>
        public static bool NetAvailable
        {
            get
            {
                return Application.internetReachability != NetworkReachability.NotReachable;
            }
        }

        /// <summary>
        /// 是否是无线
        /// </summary>
        public static bool IsWifi
        {
            get
            {
                return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
            }
        }

        public static GameObject Instantiate(UObject obj, string path)
        {
            var gameObj = UObject.Instantiate(obj) as GameObject;
            if (gameObj != null && path != null)
            {
                var mr = gameObj.SetDefaultComponent<ManagedResource>();
                mr.bundlename = path.Replace(@"\", @"/").ToLower();
                Game.ResourceManager.Instance.AddRefCount(mr.bundlename);

                var mc = gameObj.GetComponent<MecanimControl>();
                if (mc != null)
                {
                    var mcold = (obj as GameObject).GetComponent<MecanimControl>();
                    mc.Copy(mcold);
                }
            }
            return gameObj;
        }
        public static GameObject Copy(UObject obj)
        {
            var gameObj = UObject.Instantiate(obj) as GameObject;
            if (gameObj != null)
            {
                var mr = gameObj.GetComponent<ManagedResource>();
                if (mr != null)
                {
                    Game.ResourceManager.Instance.AddRefCount(mr.bundlename);
                }

                var mc = gameObj.GetComponent<MecanimControl>();
                if (mc != null)
                {
                    var mcold = (obj as GameObject).GetComponent<MecanimControl>();
                    mc.Copy(mcold);
                }
            }
            return gameObj;
        }

        public static void Log(string str)
        {
            Debug.Log(str);
        }
        public static void LogWarning(string str)
        {
            Debug.LogWarning(str);
        }
        public static void LogError(string str)
        {
            Debug.LogError(str);
        }

        #region 本地文件读写
        /// <summary>
        /// 解析成字典.格式:键值对用'='号分离
        /// </summary>
        public static Dictionary<string, string> ReadDict(string filePath)
        {
            var dict = new Dictionary<string, string>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] nodes = lines[i].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (nodes.Length == 2 && !dict.ContainsKey(nodes[0]))
                        dict.Add(nodes[0], nodes[1]);
                    else
                        Debug.LogErrorFormat("{0} 文件第{1}行格式错误!", filePath, i + 1);
                }
            }
            return dict;
        }
        /// <summary>
        /// 解析成列表.每行一个数据
        /// </summary>
        public static List<string> ReadList(string filePath)
        {
            var list = new List<string>();
            if (File.Exists(filePath))
                list.AddRange(File.ReadAllLines(filePath, Encoding.UTF8));
            return list;
        }
        public static void WriteDict(string filePath, Dictionary<string, string> dict)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in dict)
                builder.AppendFormat("{0}={1}\n", item.Key, item.Value);
            File.WriteAllText(filePath, builder.ToString(), Encoding.UTF8);
        }
        public static void WriteList(string filePath, List<string> list)
        {
            File.WriteAllLines(filePath, list.ToArray(), Encoding.UTF8);
        }
        #endregion
    }
}