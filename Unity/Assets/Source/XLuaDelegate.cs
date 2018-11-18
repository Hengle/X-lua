using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using XLua;

namespace Game
{
    public class XLuaDelegate : MonoBehaviour
    {
        Action<float, float> luaUpdate;
        Action luaLateUpdate;
        Action<float> luaFixedUpdate;
        Action luaDestroy;

        public void Init(LuaEnv luaEnv)
        {
            luaUpdate += luaEnv.Global.Get<Action<float, float>>("Update");
            luaLateUpdate += luaEnv.Global.Get<Action>("LateUpdate");
            luaFixedUpdate += luaEnv.Global.Get<Action<float>>("FixedUpdate");
            luaDestroy += luaEnv.Global.Get<Action>("OnDestroy");
        }

        public void Dispose()
        {
            luaUpdate = null;
            luaLateUpdate = null;
            luaFixedUpdate = null;
            luaDestroy = null;
            Destroy(this);
        }

        void Update()
        {
            LuaManager.Instance.Tick();

            if (luaUpdate != null)
                luaUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        void LateUpdate()
        {
            if (luaUpdate != null)
                luaLateUpdate();
        }

        void FixedUpdate()
        {
            if (luaUpdate != null)
                luaFixedUpdate(Time.fixedDeltaTime);
        }

        void OnDestroy()
        {
            if (luaDestroy != null)
                luaDestroy();
        }
    }
}