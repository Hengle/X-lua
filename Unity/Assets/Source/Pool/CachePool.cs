using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CachePool<T> : IPoolItem<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// 池最大上限,实际大小为 Limit + MaxSize
        /// </summary>
        readonly int Capacity = 50;

        public string Name { get { return _original != null ? _original.name : "Null"; } }
        public int MaxSize { get { return _maxSize; } }
        public float LastUseTime { get { return _lastUseTime; } }
        public float CacheTime { get { return _cacheTime; } }
        public T Original { get { return _original; } }

        private int _maxSize = 10;
        private float _lastUseTime;
        private float _cacheTime;
        private T _original;
        private SimplePool<T> _cache;
        private HashSet<T> _usingObjects;
        private Action<T> _onGet;
        private Action<T> _onRelease;

        public CachePool(T original, int maxSize, float lastUseTime = 0, float cacheTime = 0)
            : this(original, maxSize, lastUseTime, cacheTime, null, null) { }
        public CachePool(T original, int maxSize, Action<T> onGet, Action<T> onRelease)
             : this(original, maxSize, 0, 0, onGet, onRelease) { }
        public CachePool(T original, int maxSize, float lastUseTime, float cacheTime,
            Action<T> onGet, Action<T> onRelease)
        {
            _original = original;
            _maxSize = maxSize;
            _lastUseTime = lastUseTime;
            _cacheTime = cacheTime;
            _cache = new SimplePool<T>(original.name, maxSize);
            _usingObjects = new HashSet<T>();
            _onGet = onGet;
            _onRelease = onRelease;

            Capacity += _maxSize;
        }

        public virtual bool CanCleanup()
        {
            return _usingObjects.Count == 0 && Time.time - _lastUseTime > _cacheTime;
        }
        public virtual void Cleanup()
        {
            T del = _cache.Get();
            while (del != null)
            {
                if (del is GameObject)
                    UnityEngine.Object.DestroyImmediate(del, true);
                else
                    Resources.UnloadAsset(del);
            }
            if (_original is GameObject)
                UnityEngine.Object.DestroyImmediate(_original, true);
            else
                Resources.UnloadAsset(_original);
        }

        public virtual T Get()
        {
            if (_cache.Count + _usingObjects.Count >= Capacity)
                throw new Exception(string.Format("{0} CachePool存储数量达到极限[Capacity={1}]", Name, Capacity));

            T obj = null;
            if (_cache.Count == 0)
                obj = UnityEngine.Object.Instantiate<T>(_original);
            else
                obj = _cache.Get();
            _usingObjects.Add(obj);
            if (_onGet != null)
                _onGet(obj);
            return obj;
        }
        public virtual bool Release(T obj)
        {
            if (!_usingObjects.Contains(obj))
                throw new Exception(string.Format("{0} CachePool中不包含实例{1}对象", Name, obj.name));

            _usingObjects.Remove(obj);
            _cache.Release(obj);
            _lastUseTime = Time.time;
            if (_onRelease != null)
                _onRelease(obj);
            return true;
        }
    }
}
