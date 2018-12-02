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
        Action<float> luaSecondUpdate;
        Action luaLateUpdate;
        Action<float> luaFixedUpdate;
        Action luaDestroy;

        float time = -1;

        [DoNotGen]
        public void Init(LuaTable luaMain)
        {
            luaUpdate += luaMain.Get<Action<float, float>>("Update");
            luaSecondUpdate += luaMain.Get<Action<float>>("SecondUpdate");
            luaLateUpdate += luaMain.Get<Action>("LateUpdate");
            luaFixedUpdate += luaMain.Get<Action<float>>("FixedUpdate");
            luaDestroy += luaMain.Get<Action>("OnDestroy");
        }

        [DoNotGen]
        public void Dispose()
        {
            if (luaDestroy != null)
                luaDestroy();

            luaUpdate = null;
            luaLateUpdate = null;
            luaFixedUpdate = null;
            luaDestroy = null;
            luaSecondUpdate = null;
        }

        void Update()
        {
            LuaManager.Instance.Tick();

            if (luaUpdate != null)
                luaUpdate(Time.deltaTime, Time.unscaledDeltaTime);

            if (luaSecondUpdate != null && time < Time.time)
            {
                time = Time.time + 1;
                luaSecondUpdate(Time.time);
            }
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
    }
}