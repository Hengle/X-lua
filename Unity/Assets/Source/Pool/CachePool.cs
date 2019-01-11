using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 创建一个对象的Clone集合
    /// </summary>
    public class CachePool<T> : IPoolItem<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// 池最大上限,实际大小为 Limit + MaxSize
        /// </summary>
        readonly int Capacity = 50;

        public string Name { get { return _name; } }
        public int MaxSize { get { return _maxSize; } }
        public float LastUseTime { get { return _lastUseTime; } }
        public float CacheTime { get { return _cacheTime; } }
        /// <summary>
        /// Clone对象
        /// </summary>
        public T Original { get { return _original; } }

        private string _name = "Null";
        private int _maxSize = 10;
        private float _lastUseTime = 0;
        /// <summary>
        /// time < 0时,不做记时处理;反之,计时处理
        /// </summary>
        private float _cacheTime = -1;
        private T _original;
        private SimplePool<T> _cache;
        private HashSet<T> _usingObjects;
        private Action<T> _onGet;
        private Action<T> _onRelease;

        public CachePool(string name, T original, int maxSize, float cacheTime = -1)
            : this(name, original, maxSize, cacheTime, null, null) { }
        public CachePool(string name, T original, int maxSize, Action<T> onGet, Action<T> onRelease)
             : this(name, original, maxSize, -1, onGet, onRelease) { }
        public CachePool(string name, T original, int maxSize, float cacheTime,
            Action<T> onGet, Action<T> onRelease)
        {
            _name = name;
            _original = original;
            _maxSize = maxSize;
            _lastUseTime = cacheTime < 0 ? 0 : Time.time;
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
        public virtual void ReleaseAllUsing()
        {
            List<T> usingObjs = new List<T>(_usingObjects);
            for (int i = 0; i < usingObjs.Count; i++)
            {
                var obj = usingObjs[i];
                _usingObjects.Remove(obj);
                _cache.Release(obj);
                if (_onRelease != null)
                    _onRelease(obj);
            }
            _lastUseTime = Time.time;
        }
    }
}
