using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPool<T> where T : class, new()
    {
        private int maxNum = 5;
        private bool createWhenIsFull = true;
        private readonly Queue<T> objects = new Queue<T>();


        public int Count { get { return objects == null ? 0 : objects.Count; } }
        public bool IsFull { get { return objects.Count >= maxNum; } }


        public ObjectPool()
        {
            Queue<T> queue = new Queue<T>(maxNum);
            createWhenIsFull = true;
        }
        public ObjectPool(int maxNum, bool createWhenIsFull = true)
        {
            Queue<T> queue = new Queue<T>(maxNum);
            this.maxNum = maxNum;
            this.createWhenIsFull = createWhenIsFull;
        }

        public T Get()
        {
            if (objects.Count > 0)
            {
                T item = objects.Dequeue();
                if (item != null)
                    return item;
            }
            if (createWhenIsFull)
            {
                return new T();
            }
            return null;
        }

        public void Put(T item)
        {
            if (item == null)
            {
                Debug.LogError("Trying to release null to pool");
                return;
            }
            if (objects.Count > 0 && ReferenceEquals(objects.Peek(), item))
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");           
            objects.Enqueue(item);
            if (objects.Count > maxNum)
                Debug.LogWarningFormat("Warning! Class {0}'s pool is full.", typeof(T).ToString());
        }
    }
}
