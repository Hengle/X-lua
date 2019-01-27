using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    internal enum LaunchState
    {
        Launcher,                    //启动界面,动态字体加载后再显示界面

        InitSDK,                      //SDK初始化完成
        CheckUnzipData,               //并解压脚本和配置文件
        GetServerList,               //加载服务器列表
        CheckVersion,                 //检查版本,更新App和资源
        PreloadAssets,                //开始预加载资源,重新加载字体
        InitLua,                      //初始化lua,并开始监听login场景是否加载成功
        StartGame,                    //进入游戏登入场景

        //--错误提示
        MemeryNotEnough,    //检查设备内存大小
        Offline,            //网络断线
        ServerListFailed,   //获取服务器列表失败
    }

    class Launcher
    {
        public static Launcher Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new Launcher();
                return _ins;
            }
        }

        private static Launcher _ins;
        private Dictionary<LaunchState, string> _launcher = new Dictionary<LaunchState, string>()
        {
            {LaunchState.InitSDK              ,  "初始化SDK"},
            {LaunchState.CheckUnzipData       ,  "检查解压数据"},
            {LaunchState.GetServerList        ,  "加载服务器列表"},
            {LaunchState.CheckVersion         ,  "检查版本"},
            {LaunchState.PreloadAssets        ,  "预加载资源"},
            {LaunchState.InitLua              ,  "初始化脚本"},
            {LaunchState.StartGame            ,  "开始游戏"},

            {LaunchState.MemeryNotEnough      ,  "内存不足"},
            {LaunchState.Offline              ,  "网络断线"},
            {LaunchState.ServerListFailed     ,  "服务器列表加载失败"},//区服列表信息
        };

        public IEnumerator StartLaunch()
        {
            yield return null;
            //--加载字体
            //--加载界面
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// 刷新启动器状态
        /// </summary>
        /// <param name="state">启动器状态</param>
        /// <param name="value">启动进度百分比,保留小数点后2位</param>
        public void RefreshLaunch(LaunchState state, float value)
        {
            string name = "";
            if (_launcher.TryGetValue(state, out name))
            {
                Debug.LogErrorFormat("_launcher[{0}] = null ~", state);
                return;
            }
            //--设置状态标题
            //--设置百分比
        }
     
    }
}
