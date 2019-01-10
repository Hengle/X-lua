using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 对象缓存.无对象实例化操作,仅作存储
    /// </summary>
    public class SimplePool<T> where T : class
    {
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public int Count { get { return _cache.Count; } }


        private Queue<T> _cache = new Queue<T>();

        public SimplePool() : this("SimplePool") { }
        public SimplePool(string name) : this(name, 10) { }
        public SimplePool(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public T Get()
        {
            if (_cache.Count > 0)
            {
                T obj = _cache.Dequeue();
                return obj;
            }
            return null;
        }
        public bool Release(T obj)
        {
            if (obj == null)
                throw new Exception(string.Format("{0} SimplePool无法存储Null对象", Name));

            if (_cache.Count <= Capacity)
            {
#if UNITY_EDITOR
                foreach (var item in _cache)
                {
                    if (ReferenceEquals(item, obj))
                        throw new Exception(string.Format("{0} SimplePool中禁止存储相同对象", Name));
                }
#endif
                _cache.Enqueue(obj);
                return true;
            }
            
            //如果不做处理,可能会出现资源无法释放的问题;
            //如果系统能处理,也可能出现系统集中释放资源,导致卡顿.
            //To Dispose
            Debug.LogWarningFormat("{0} SimplePool已达上限[Capacity:{1}]", Name, Capacity);
            return false;
        }
        public T[] ToArray()
        {
            return _cache.ToArray();
        }
    }
}