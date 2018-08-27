using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AppConst
    {
        public const bool DebugMode     = false;        //是否为调试模式
        public const bool EnableProfile = false;        //是否激活Profile功能
        public const int FrameRate      = 30;           //游戏帧率

#if UNITY_IPHONE
        public const int Resolution     = 1080;         //分辨率配置
#else
        public const int Resolution     = 810;          //分辨率配置
#endif
    }
}

