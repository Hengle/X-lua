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

            yield return CheckDevice();
            yield return CheckUnzipData();//解压zip中script和config到持续化目录
            yield return GetServerList();//加载本地Url配置信息
            yield return CheckVersion();//检查资源版本,判断是否需要更新
            yield return PreloadAssets();//开始初始化lua
            yield return InitLua();//开始初始化lua
            yield return StartGame();//进入游戏登入场景,并且销毁资源更新场景
        }

        IEnumerator CheckDevice()
        {
            Launcher.Ins.SetLaunchState(LaunchState.CheckDevice, 0.4f);
            while (!Interface.Instance.IsSDKFinished())
                yield return null;
            //获取设备内存大小,如果不足,则提示内存不足.
            long memorysize = Interface.Instance.GetMemInfo();
#if UNITY_ANDROID || UNITY_IPHONE
            if (memorysize < 1073741824)
            {
                Launcher.Ins.RefreshLaunch(LaunchState.CheckDevice, 0.8f);
                yield return null;
            }
#endif
            Launcher.Ins.SetLaunchState(LaunchState.CheckDevice, 1f);
        }
        IEnumerator CheckUnzipData()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            yield return Client.ResMgr.CheckUnzipData();
#endif
            Launcher.Ins.SetLaunchState(LaunchState.CheckUnzipData, 1f);
            yield break;
        }
        IEnumerator GetServerList()
        {
            Launcher.Ins.SetLaunchState(LaunchState.GetServerList, 0.4f);
            yield return Client.ServerMgr.GetServerList();
            Launcher.Ins.SetLaunchState(LaunchState.GetServerList, 1f);
        }
        IEnumerator CheckVersion()
        {
            yield return Client.UpdateMgr.CheckVersion();
        }
        IEnumerator PreloadAssets()
        {
            Client.ResMgr.Init();
            while (!Client.ResMgr.IsPreLoadDone)
            {
                yield return null;
                Launcher.Ins.SetLaunchState(LaunchState.PreloadAssets, Client.ResMgr.PreloadPrograss);
            }
        }
        IEnumerator InitLua()
        {
            Client.LuaMgr.AddSearchPath(ConstSetting.LuaDir);
            Client.LuaMgr.InitScripts();
            var modules = Client.LuaMgr.LuaEnv.Global.GetInPath<XLua.LuaTable>("Modules");
            var GetInitedNum = Client.LuaMgr.LuaEnv.Global.GetInPath<System.Func<int>>("GetInitedNum");
            while (GetInitedNum() != modules.Length)
            {
                yield return null;
                Launcher.Ins.SetLaunchState(LaunchState.InitScripts, GetInitedNum() * 1f / modules.Length);
            }

            GetInitedNum = null;
            modules.Dispose();
            modules = null;
        }
        IEnumerator StartGame()
        {
            //加载登入场景
            Launcher.Ins.SetLaunchState(LaunchState.StartGame, 1);
            yield break;
        }

        public void Dispose()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}
