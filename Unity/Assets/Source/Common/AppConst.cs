using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AppConst
    {
        public const int FrameRate      = 30;           //游戏帧率

#if UNITY_IPHONE
        public const int Resolution     = 1080;         //分辨率配置
#else
        public const int Resolution     = 810;          //分辨率配置
#endif

#if UNITY_EDITOR
        public static string LuaDir { get { return Util.DataPath + "../Code/Scripts"; } }
#else
        public static string LuaDir { get { return Util.DataPath + "Scripts"; } }
#endif
        public const string LuaMain = "Main";
    }
}

