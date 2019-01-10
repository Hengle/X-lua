using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CacheObject<T> : IPoolItem<T> where T : UnityEngine.Object
    {
        public string Name { get { return _original != null ? _original.name : "Null"; } }
        public int MaxSize { get { return 1; } }
        public float LastUseTime { get { return _lastUseTime; } }
        public float CacheTime { get { return _cacheTime; } }
        public T Original { get { return _original; } }

        private float _lastUseTime;
        private float _cacheTime;
        private T _original;
        private Action<T> _onGet;
        private Action<T> _onRelease;

        public CacheObject(T original, float lastUseTime = 0, float cacheTime = 0)
            : this(original, lastUseTime, cacheTime, null, null) { }
        public CacheObject(T original, Action<T> onGet, Action<T> onRelease)
             : this(original, 0, 0, onGet, onRelease) { }
        public CacheObject(T original, float lastUseTime, float cacheTime,
            Action<T> onGet, Action<T> onRelease)
        {
            _original = original;
            _lastUseTime = lastUseTime;
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
