using System.Collections;
using Game.Platform;
using UnityEngine;

namespace Game
{
    public class GameManager : IManager
    {
        public GameManager() { }

        public void Init()
        {
            Interface.Create();
            Interface.Instance.Init();

            Client.Ins.StartCoroutine(Start());
        }

        IEnumerator Start()
        {
            yield return Launcher.Ins.StartLaunch();

            yield return InitSDK();
            yield return CheckUnzipData();//解压zip中script和config到持续化目录
            yield return GetServerList();//加载本地Url配置信息
            yield return CheckVersion();//检查资源版本,判断是否需要更新
            yield return PreloadAssets();//开始初始化lua
            yield return InitLua();//开始初始化lua
            yield return StartGame();//进入游戏登入场景,并且销毁资源更新场景
        }

        IEnumerator InitSDK()
        {
            Launcher.Ins.RefreshLaunch(LaunchState.InitSDK, 0.4f);
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
        IEnumerator CheckUnzipData()
        {
            Launcher.Ins.RefreshLaunch(LaunchState.CheckUnzipData, 1f);
            yield return null;
        }
        IEnumerator GetServerList()
        {
            yield return new WaitForEndOfFrame();
        }
        IEnumerator CheckVersion()
        {
            Launcher.Ins.RefreshLaunch(LaunchState.CheckVersion, 0.2f);
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
            Launcher.Ins.RefreshLaunch(LaunchState.CheckVersion, 0.2f);
        }
        IEnumerator PreloadAssets()
        {
            Debug.Log("Is PreLoad Done?");
            while (!Client.ResMgr.IsPreLoadDone)
                yield return new WaitForEndOfFrame();
            Debug.Log("Load Done!");
        }
        IEnumerator InitLua()
        {
            Client.LuaMgr.AddSearchPath(ConstSetting.LuaDir);
            Client.LuaMgr.InitScripts();
            yield break;
        }
        IEnumerator StartGame()
        {
            //GameObject.Destroy(_dlgRes);

            Launcher.Ins.RefreshLaunch(LaunchState.StartGame, 1);
            yield break;
        }
 
        public void Dispose()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}
