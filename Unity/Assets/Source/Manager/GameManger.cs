<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;
using Game.Platform;
using XLua;

namespace Game
{
    public class GameManager : IManager
    {
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }
        static GameManager _instance;
        protected GameManager() { }

        enum LaunchState
        {
            InitSDK,         //SDK初始化完成
            DowmloadZip,     //下载脚本和配置文件
            UnZipFile,       //并解压脚本和配置文件
            CheckAppVersion, //检查App版本
            DowmloadApp,     //安装App
            CheckResVersion, //检查资源版本
            DowmloadRes,     //热更资源
            GetServerList,   //更新服务器地址列表
            LoadResource,    //加载资源,加载完毕进入登入界面
            //--错误提示
            MemeryNotEnough, //检查设备内存大小
            OffLine,         //网络断线
            ServerListFailed,//获取服务器列表失败
        }


        Dictionary<string, string> _launcher = new Dictionary<string, string>();
        public void Init()
        {
            TextAsset text = (TextAsset)Resources.Load("launcher", typeof(TextAsset));
            if (null != text)
            {
                using (MemoryStream stream = new MemoryStream(text.bytes))
                using (StreamReader r = new StreamReader(stream))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        string[] pair = line.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (pair != null && pair.Length == 2 && !_launcher.ContainsKey(pair[0]))
                            _launcher.Add(pair[0], pair[1]);
                    }
                }
            }
        }
        public void Dispose()
        {
            _launcher.Clear();
            _instance = null;
            Resources.UnloadUnusedAssets();
        }

        public IEnumerator InitSDK()
        {
            RefreshLaunch(LaunchState.InitSDK, 0.4f);
            while (!Interface.Instance.IsSDKFinished())
            {
                yield return new WaitForEndOfFrame();

            }
            //获取设备内存大小,如果不足,则提示内存不足.
            long memorysize = Interface.Instance.GetMemInfo();

#if UNITY_ANDROID
            if (memorysize < 1500000)
            {
                _status.text = _launcher[LaunchState.CheckMemerySize.ToString()];
                yield return new WaitForSeconds(1);
            }
#elif UNITY_IPHONE
            if (memorysize < 1000000)
            {
                _status.text = _launcher[LaunchState.CheckMemerySize.ToString()];
                yield return new WaitForSeconds(1);
            }
#elif UNITY_STANDALONE_WIN
            yield break;
#else
            yield return new WaitForSeconds(1);
#endif
        }
        public IEnumerator DowmloadUnZip()
        {
            RefreshLaunch(LaunchState.DowmloadZip, 1f);
            yield return null;
        }
        public IEnumerator UnZipFile()
        {
            RefreshLaunch(LaunchState.UnZipFile, 1f);
            yield return null;
        }
        public IEnumerator UpdateAppVersion()
        {
            RefreshLaunch(LaunchState.CheckAppVersion, 0.2f);
            //与服务器版本号进行对比
            //TODO
            yield return new WaitForEndOfFrame();
            if (true)
            {
                //下载资源-安装包
                //TODO
            }
            RefreshLaunch(LaunchState.CheckAppVersion, 1f);
        }
        public IEnumerator UpdateResVersion()
        {
            RefreshLaunch(LaunchState.CheckResVersion, 0.2f);
            //与服务器版本号进行对比
            //TODO
            yield return null;
            if (true)
            {
                //下载资源-数据资源
                //TODO
            }
            //资源更新列表加快资源存在性检查速度;初始化资源管理器
            //TODO;在读写路径下存一个文件
            RefreshLaunch(LaunchState.CheckResVersion, 0.2f);
        }
        public IEnumerator GetServerList()
        {
            //下载服务器url列表
            //App地址
            //Res地址
            //公告地址
            //Android/IOS区服列表地址
            //TODO
            yield return null;
        }
        public IEnumerator LoadResource()
        {
            Debug.Log("Is PreLoad Done?");
            while (!ResourceManager.Instance.IsPreLoadDone)
                yield return new WaitForEndOfFrame();
            Debug.Log("Load Done!");
            //场景加载
            //TODO
            RefreshLaunch(LaunchState.LoadResource, 1f);
        }
        public void StartGame()
        {
            //GameObject.Destroy(_dlgRes);
            LuaManager.Instance.AddLuaSearchPath(AppConst.LuaDir);
            LuaManager.Instance.StartGame();

            UpdateManager.Instance.SetProgressValue("开始游戏", 1f);
        }

        void RefreshLaunch(LaunchState state, float value)
        {
            string name = "";
            if (_launcher.TryGetValue(state.ToString(), out name))
            {
                Debug.LogErrorFormat("_launcher[{0}] = null ~", state.ToString());
                return;
            }
            UpdateManager.Instance.SetProgressValue(name, value);
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;
using Game.Platform;
using XLua;

namespace Game
{
    public class GameManager : IManager
    {
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }
        static GameManager _instance;
        protected GameManager() { }

        enum LaunchState
        {
            InitSDK,         //SDK初始化完成
            DowmloadZip,     //下载脚本和配置文件
            UnZipFile,       //并解压脚本和配置文件
            CheckAppVersion, //检查App版本
            DowmloadApp,     //安装App
            CheckResVersion, //检查资源版本
            DowmloadRes,     //热更资源
            GetServerList,   //更新服务器地址列表
            LoadResource,    //加载资源,加载完毕进入登入界面
            //--错误提示
            MemeryNotEnough, //检查设备内存大小
            OffLine,         //网络断线
            ServerListFailed,//获取服务器列表失败
        }


        Dictionary<string, string> _launcher = new Dictionary<string, string>();
        [DoNotGen]
        public void Init()
        {
            TextAsset text = (TextAsset)Resources.Load("launcher", typeof(TextAsset));
            if (null != text)
            {
                using (MemoryStream stream = new MemoryStream(text.bytes))
                using (StreamReader r = new StreamReader(stream))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        string[] pair = line.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (pair != null && pair.Length == 2 && !_launcher.ContainsKey(pair[0]))
                            _launcher.Add(pair[0], pair[1]);
                    }
                }
            }
        }
        [DoNotGen]
        public void Dispose()
        {
            _launcher.Clear();
            _instance = null;
            Resources.UnloadUnusedAssets();
        }

        public IEnumerator InitSDK()
        {
            RefreshLaunch(LaunchState.InitSDK, 0.4f);
            while (!Interface.Instance.IsSDKFinished())
            {
                yield return new WaitForEndOfFrame();
                
            }
            //获取设备内存大小,如果不足,则提示内存不足.
            long memorysize = Interface.Instance.GetMemInfo();
         
#if UNITY_ANDROID
            if (memorysize < 1500000)
            {
                _status.text = _launcher[LaunchState.CheckMemerySize.ToString()];
                yield return new WaitForSeconds(1);
            }
#elif UNITY_IPHONE
            if (memorysize < 1000000)
            {
                _status.text = _launcher[LaunchState.CheckMemerySize.ToString()];
                yield return new WaitForSeconds(1);
            }
#elif UNITY_STANDALONE_WIN
            yield break;
#else
            yield return new WaitForSeconds(1);
#endif
        }
        public IEnumerator DowmloadUnZip()
        {
            RefreshLaunch(LaunchState.DowmloadZip, 1f);
            yield return null;
        }
        public IEnumerator UnZipFile()
        {
            RefreshLaunch(LaunchState.UnZipFile, 1f);
            yield return null;
        }
        public IEnumerator UpdateAppVersion()
        {
            RefreshLaunch(LaunchState.CheckAppVersion, 0.2f);
            //与服务器版本号进行对比
            //TODO
            yield return new WaitForEndOfFrame();
            if (true)
            {
                //下载资源-安装包
                //TODO
            }
            RefreshLaunch(LaunchState.CheckAppVersion, 1f);
        }
        public IEnumerator UpdateResVersion()
        {
            RefreshLaunch(LaunchState.CheckResVersion, 0.2f);
            //与服务器版本号进行对比
            //TODO
            yield return null;
            if (true)
            {
                //下载资源-数据资源
                //TODO
            }
            //资源更新列表加快资源存在性检查速度;初始化资源管理器
            //TODO;在读写路径下存一个文件
            RefreshLaunch(LaunchState.CheckResVersion, 0.2f);
        }
        public IEnumerator GetServerList()
        {
            //下载服务器url列表
            //App地址
            //Res地址
            //公告地址
            //Android/IOS区服列表地址
            //TODO
            yield return null;
        }
        public IEnumerator LoadResource()
        {
            yield return new WaitForEndOfFrame();
            //ResourceManager.Instance.Start();
            yield return new WaitForEndOfFrame();
            //场景加载
            //TODO
            RefreshLaunch(LaunchState.LoadResource, 1f);
        }
        public void StartGame()
        {
            //GameObject.Destroy(_dlgRes);
            LuaManager.Instance.AddLuaSearchPath(AppConst.LuaDir);
            LuaManager.Instance.StartGame();

            UpdateManager.Instance.SetProgressValue("开始游戏", 1f);
        }

        void RefreshLaunch(LaunchState state, float value)
        {
            UpdateManager.Instance.SetProgressValue(_launcher[state.ToString()], value);
        }
    }
}
>>>>>>> 1d8f3cd81a2e6b28b65e750d857fc04cfa6a59b9
