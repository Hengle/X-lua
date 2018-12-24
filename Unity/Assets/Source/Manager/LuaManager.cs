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
        private List<string> _searchPaths = new List<string>();
        private XLuaDelegate _luaDelegate;
        public LuaEnv LuaEnv { get { return _luaEnv; } }
        [DoNotGen]
        public void Init()
        {
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader(CustomLoader);
            LuaHelper.Init();
        }
        [DoNotGen]
        public void Dispose()
        {
            if (_luaEnv != null)
            {
                try
                {
                    FairyGUI.UIPackage.RemoveAllPackages();
                    if (_luaDelegate != null)
                        _luaDelegate.Dispose();
                    _searchPaths.Clear();
                    _luaEnv.Dispose();
                    _luaEnv = null;
                    _instance = null;
                }
                catch (Exception e)
                {
                    Debug.LogError(string.Format("xlua exception : {0}\n {1}", e.Message, e.StackTrace));
                }
            }
        }
        [DoNotGen]
        public void AddLuaSearchPath(string path)
        {
            _searchPaths.Add(path);
        }
        [DoNotGen]
        public bool HasScript(string viewName)
        {
            return _searchPaths.Exists(s => s.Equals(viewName));
        }
        private byte[] CustomLoader(ref string filePath)
        {
            for (int i = 0; i < _searchPaths.Count; i++)
            {
                string fullPath = string.Format("{0}/{1}.lua", _searchPaths[i], filePath.Replace(".", "/")).ToLower();
                if (File.Exists(fullPath))
                    return File.ReadAllBytes(fullPath);
            }

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
            _luaEnv.DoString("require 'Main'", "Main", _luaEnv.Global);
            LuaTable luaMain = _luaEnv.Global.GetInPath<LuaTable>("Main");
            luaMain.Get<Action>("Init")();

            var main = Main.Instance.gameObject;
            _luaDelegate = main.GetComponent<XLuaDelegate>();
            if (_luaDelegate == null)
                _luaDelegate = main.AddComponent<XLuaDelegate>();
            _luaDelegate.Init(luaMain);
            luaMain.Dispose();
        }
    }

}
