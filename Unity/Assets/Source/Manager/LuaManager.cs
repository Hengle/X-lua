using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace Game
{
    public class LuaManager : Manager
    {
        public LuaManager() { }

        private LuaEnv _luaEnv;
        private List<string> _searchPaths = new List<string>();

        private Action<float, float> _luaUpdate;
        private Action _luaLateUpdate;
        private Action<float> _luaFixedUpdate;
        private Action _luaDestroy;

        public LuaEnv LuaEnv { get { return _luaEnv; } }
        public override void Init()
        {
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader(CustomLoader);
            LuaHelper.Init();
        }
        public override void Dispose()
        {
            if (_luaEnv != null)
            {
                try
                {
                    if (_luaDestroy != null)
                        _luaDestroy();

                    _luaUpdate = null;
                    _luaLateUpdate = null;
                    _luaFixedUpdate = null;
                    _luaDestroy = null;

                    _searchPaths.Clear();
                    _luaEnv.Dispose();
                    _luaEnv = null;
                }
                catch (Exception e)
                {
                    Debug.LogError(string.Format("xlua exception : {0}\n {1}", e.Message, e.StackTrace));
                }
            }
        }
        public void AddSearchPath(string path)
        {
            _searchPaths.Add(path);
        }

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
        private void Tick()
        {
            if (_luaEnv != null && Time.frameCount % 100 == 0)
            {
                _luaEnv.GC();
                _luaEnv.FullGc();
            }
        }

        public void InitScripts()
        {
            var luaMain = GetTable("Main");
            luaMain.Get<Action>("Init")();

            _luaUpdate += luaMain.Get<Action<float, float>>("Update");
            _luaLateUpdate += luaMain.Get<Action>("LateUpdate");
            _luaFixedUpdate += luaMain.Get<Action<float>>("FixedUpdate");
            _luaDestroy += luaMain.Get<Action>("OnDestroy");

            luaMain.Dispose();
        }
        public LuaTable GetTable(string name)
        {
            string require = string.Format("require '{0}'", name);
            _luaEnv.DoString(require, name, _luaEnv.Global);
            return _luaEnv.Global.GetInPath<LuaTable>(name);
        }
        public T GetLuaFunc<T>(string name)
        {
            return _luaEnv.Global.Get<T>(name);
        }

        public void Update()
        {
            Tick();

            if (_luaUpdate != null)
                _luaUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
        public void FixedUpdate()
        {
            if (_luaUpdate != null)
                _luaFixedUpdate(Time.fixedDeltaTime);
        }
        public void LateUpdate()
        {
            if (_luaUpdate != null)
                _luaLateUpdate();
        }
    }

}
