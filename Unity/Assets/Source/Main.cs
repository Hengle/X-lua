using Game.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Profiling;

namespace Game
{
    public class Main : MonoBehaviour
    {
        public string file = "hotfix.txt";

        public static Main Instance { get; private set; }

        List<IManager> _managers = new List<IManager>()
        {
             Manager.ResMgr,
             Manager.LuaMgr,
             //Manager.UpdateMgr,
             Manager.NetworkMgr,
            //GameManager.Instance,
        };
        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FairyGUI.UIObjectFactory.SetLoaderExtension(typeof(MyLoader));

            for (int i = 0; i < _managers.Count; i++)
                _managers[i].Init();

            Util.SetResolution(AppConst.Resolution);
            Application.targetFrameRate = AppConst.FrameRate;
            QualitySettings.blendWeights = BlendWeights.TwoBones;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Interface.Create();
            Interface.Instance.Init();

            //StartDownload();
            Debug.LogError(Application.persistentDataPath);
        }

        IEnumerator Start()
        {
            //yield return GameManager.Instance.InitSDK();
            //yield return GameManager.Instance.CheckUnzipData();
            //yield return GameManager.Instance.CheckVersion();
            //yield return GameManager.Instance.GetServerList();
            yield return GameManager.Instance.LoadResource();

            yield return new WaitForEndOfFrame();
            GameManager.Instance.StartGame();
        }

        void Update()
        {
            Manager.ResMgr.Update();
            Manager.UpdateMgr.Update();
            Manager.NetworkMgr.Update();
        }

        void OnDestroy()
        {
            for (int i = 0; i < _managers.Count; i++)
                _managers[i].Dispose();
            _managers.Clear();
            Instance = null;
        }

        void StartDownload()
        {
            string uri = "http://localhost:8081/";
            string save = Application.dataPath + "/../" + file;
            Download(uri + file, Application.dataPath + "/../" + file);
            var builder = new System.Text.StringBuilder();
            for (int i = 0; i < 1024 * 1024 * 20; i++)
            {
                builder.AppendLine("0");
            }
            //File.WriteAllText(Application.dataPath + "/../" + "TEST.txt", builder.ToString());
        }
        void Download(string uri, string file)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(new System.Uri(uri));
            httpWebRequest.Timeout = 2 * 60 * 1000;
            httpWebRequest.KeepAlive = false;
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            byte[] buffer = new byte[4096];
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (Stream stream = httpWebResponse.GetResponseStream())
            {
                int readTotalSize = 0;
                int size = stream.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    //只将读出的字节写入文件
                    fs.Write(buffer, 0, size);
                    readTotalSize += size;
                    size = stream.Read(buffer, 0, buffer.Length);
                }
            }
            httpWebRequest.Abort();
            httpWebRequest = null;
            httpWebResponse.Close();
            httpWebResponse = null;

            if (File.Exists(file))
            {
                string txt = File.ReadAllText(file);
                Debug.LogError(txt);
            }
            else
            {
                Debug.LogError("Not Found File " + file);
            }
        }
    }
}