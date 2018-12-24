using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 游戏热更新管理器
    /// 注:必须在资源管理器初始化完毕,确保能加载AB资源
    /// </summary>
    public class UpdateManager : IManager
    {
        public static UpdateManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UpdateManager();
                return _instance;
            }
        }
        static UpdateManager _instance;

        public void Init()
        {
            
        }

        public void Dispose()
        {

        }

        public void SetProgressValue(string name, float value)
        {

        }
    }
}