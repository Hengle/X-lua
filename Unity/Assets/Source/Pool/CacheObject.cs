using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CacheObject<T> : IPoolItem<T> where T : UnityEngine.Object
    {
        public string Name { get { return _name; } }
        public int MaxSize { get { return 1; } }
        public float LastUseTime { get { return _lastUseTime; } }
        public float CacheTime { get { return _cacheTime; } }
        public T Original { get { return _original; } }

        private string _name = "Null";
        private float _lastUseTime = 0;
        /// <summary>
        /// time < 0时,不做记时处理;反之,计时处理
        /// </summary>
        private float _cacheTime = -1;
        private T _original;
        private Action<T> _onGet;
        private Action<T> _onRelease;

        public CacheObject(string name, T original, float cacheTime = -1)
            : this(name, original, cacheTime, null, null) { }
        public CacheObject(string name, T original, Action<T> onGet, Action<T> onRelease)
             : this(name, original, -1, onGet, onRelease) { }
        public CacheObject(string name, T original, float cacheTime,
            Action<T> onGet, Action<T> onRelease)
        {
            _name = name;
            _original = original;
            _lastUseTime = cacheTime < 0 ? 0 : Time.time;
            _cacheTime = cacheTime;
            _onGet = onGet;
            _onRelease = onRelease;
        }

        public virtual bool CanCleanup()
        {
            return Time.time - _lastUseTime > _cacheTime;
        }
        public virtual void Cleanup()
        {
            if (_original is GameObject)
                UnityEngine.Object.DestroyImmediate(_original, true);
            else
                Resources.UnloadAsset(_original);
        }

        public virtual T Get()
        {
            if (_onGet != null)
                _onGet(_original);
            return _original;
        }
        public virtual bool Release(T obj)
        {
            _lastUseTime = Time.time;
            if (_onRelease != null)
                _onRelease(_original);
            return true;
        }
    }
}
