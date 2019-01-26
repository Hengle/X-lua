using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;
using UObject = UnityEngine.Object;

namespace Game
{
    public partial class Util
    {
        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        public static string ComputeMD5(string source)
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
        public static string ComputeMD5File(string file)
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
                Game.Manager.ResMgr.AddRefCount(mr.bundlename);

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
                    Game.Manager.ResMgr.AddRefCount(mr.bundlename);
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
    }
}