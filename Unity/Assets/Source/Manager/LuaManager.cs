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

        private LuaEnv _luaEnv;
        private string _luaDir = "?";
        private string _luaMain = "?";
        private XLuaDelegate _luaDelegate;

        public LuaEnv LuaEnv { get { return _luaEnv; } }
        public void Init()
        {
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader(CustomLoader);
            LuaHelper.Init();
        }
        public void Dispose()
        {
            if (_luaEnv != null)
            {
                try
                {
                    _luaDelegate.Dispose();
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
        public void SetLuaDirAndMain(string dir, string main)
        {
            _luaDir = dir;
            _luaMain = main;
        }
        private byte[] CustomLoader(ref string filePath)
        {
            string fullPath = string.Format("{0}/{1}.lua", _luaDir, filePath.Replace(".", "/"));
            if (File.Exists(fullPath))
                return File.ReadAllBytes(fullPath);
            return null;
        }

        public void Tick()
        {
            if (_luaEnv != null && Time.frameCount % 100 == 0)
            {
                _luaEnv.GC();
                _luaEnv.FullGc();
            }
        }
        public void StartGame()
        {
            _luaEnv.DoString(string.Format("require '{0}'", _luaMain), _luaMain);
            _luaEnv.DoString(string.Format("{0}.Init()", _luaMain), _luaMain);
            
            var main = Main.Instance.gameObject;
            _luaDelegate = main.GetComponent<XLuaDelegate>();
            if (_luaDelegate == null)
                _luaDelegate = main.AddComponent<XLuaDelegate>();
            _luaDelegate.Init(_luaEnv);          
        }
    }
}
