using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace Game
{
    public class LuaManager : IManager
    {
        public static LuaManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LuaManager();
                return _instance;
            }
        }
        static LuaManager _instance;
        protected LuaManager() { }

        //lua逻辑代码目录
#if UNITY_EDITOR
        string LUA_DIR { get { return Util.DataPath + "../Code/Scripts"; } }
#else
        string LUA_DIR { get { return Util.DataPath + "Scripts"; } }
#endif

        const string _luaMain = "Main";
        LuaEnv _luaEnv;

        public void Init()
        {
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader(CustomLoader);
        }
        public void Dispose()
        {
            if (_luaEnv != null)
            {
                try
                {
                    _luaEnv.Dispose();
                    _luaEnv = null;
                    _instance = null;
                }
                catch (Exception e)
                {
                    Debug.LogError(string.Format("xLua exception : {0}\n {1}", e.Message, e.StackTrace));
                }
            }
        }

        private byte[] CustomLoader(ref string filePath)
        {
            string fullPath = string.Format("{0}/{1}.lua", LUA_DIR, filePath.Replace(".", "/"));
            if (File.Exists(fullPath))
                return File.ReadAllBytes(fullPath);
            return null;
        }
        public void Start()
        {
            _luaEnv.DoString(string.Format("require '{0}'", _luaMain), _luaMain);
            _luaEnv.DoString(string.Format("{0}.Init()", _luaMain), _luaMain);
            Main.Instance.Updater += _luaEnv.Global.Get<Action<float, float>>("Update");
            Main.Instance.LateUpdater += _luaEnv.Global.Get<Action>("LateUpdate");
            Main.Instance.FixedUpdater += _luaEnv.Global.Get<Action<float>>("FixedUpdate");
        }

        public void Tick()
        {
            if (_luaEnv != null && Time.frameCount % 100 == 0)
            {
                _luaEnv.Tick();
                _luaEnv.FullGc();
            }
        }
    }
}
