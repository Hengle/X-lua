﻿

using System;
using System.Collections.Generic;

namespace GameFramework.ObjectPool
{
    internal partial class ObjectPoolManager
    {
        /// <summary>
        /// 对象池。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        private sealed class ObjectPool<T> : ObjectPoolBase, IObjectPool<T> where T : ObjectBase
        {
            private readonly LinkedList<Object<T>> m_Objects;
            private readonly List<T> m_CachedCanReleaseObjects;
            private readonly List<T> m_CachedToReleaseObjects;
            private readonly bool m_AllowMultiSpawn;
            private float m_AutoReleaseInterval;
            private int m_Capacity;
            private float m_ExpireTime;
            private int m_Priority;
            private float m_AutoReleaseTime;

            /// <summary>
            /// 初始化对象池的新实例。
            /// </summary>
            /// <param name="name">对象池名称。</param>
            /// <param name="allowMultiSpawn">是否允许对象被多次获取。</param>
            /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。</param>
            /// <param name="capacity">对象池的容量。</param>
            /// <param name="expireTime">对象池对象过期秒数。</param>
            /// <param name="priority">对象池的优先级。</param>
            public ObjectPool(string name, bool allowMultiSpawn, float autoReleaseInterval, int capacity, float expireTime, int priority)
                : base(name)
            {
                m_Objects = new LinkedList<Object<T>>();
                m_CachedCanReleaseObjects = new List<T>();
                m_CachedToReleaseObjects = new List<T>();
                m_AllowMultiSpawn = allowMultiSpawn;
                m_AutoReleaseInterval = autoReleaseInterval;
                Capacity = capacity;
                ExpireTime = expireTime;
                m_Priority = priority;
                m_AutoReleaseTime = 0f;
            }

            /// <summary>
            /// 获取对象池对象类型。
            /// </summary>
            public override Type ObjectType
            {
                get
                {
                    return typeof(T);
                }
            }

            /// <summary>
            /// 获取对象池中对象的数量。
            /// </summary>
            public override int Count
            {
                get
                {
                    return m_Objects.Count;
                }
            }

            /// <summary>
            /// 获取对象池中能被释放的对象的数量。
            /// </summary>
            public override int CanReleaseCount
            {
                get
                {
                    return m_CachedCanReleaseObjects.Count;
                }
            }

            /// <summary>
            /// 获取是否允许对象被多次获取。
            /// </summary>
            public override bool AllowMultiSpawn
            {
                get
                {
                    return m_AllowMultiSpawn;
                }
            }

            /// <summary>
            /// 获取或设置对象池自动释放可释放对象的间隔秒数。
            /// </summary>
            public override float AutoReleaseInterval
            {
                get
                {
                    return m_AutoReleaseInterval;
                }
                set
                {
                    m_AutoReleaseInterval = value;
                }
            }

            /// <summary>
            /// 获取或设置对象池的容量。
            /// </summary>
            public override int Capacity
            {
                get
                {
                    return m_Capacity;
                }
                set
                {
                    if (value < 0)
                    {
                        throw new Exception("Capacity is invalid.");
                    }

                    if (m_Capacity == value)
                    {
                        return;
                    }

                    //GameFrameworkLog.Debug("Object pool '{0}' capacity changed from '{1}' to '{2}'.", Utility.Text.GetFullName<T>(Name), m_Capacity.ToString(), value.ToString());
                    m_Capacity = value;
                    Release();
                }
            }

            /// <summary>
            /// 获取或设置对象池对象过期秒数。
            /// </summary>
            public override float ExpireTime
            {
                get
                {
                    return m_ExpireTime;
                }

                set
                {
                    if (value < 0f)
                    {
                        throw new Exception("ExpireTime is invalid.");
                    }

                    if (ExpireTime == value)
                    {
                        return;
                    }

                    //GameFrameworkLog.Debug("Object pool '{0}' expire time changed from '{1}' to '{2}'.", Utility.Text.GetFullName<T>(Name), m_ExpireTime.ToString(), value.ToString());
                    m_ExpireTime = value;
                    Release();
                }
            }

            /// <summary>
            /// 获取或设置对象池的优先级。
            /// </summary>
            public override int Priority
            {
                get
                {
                    return m_Priority;
                }
                set
                {
                    m_Priority = value;
                }
            }

            /// <summary>
            /// 创建对象。
            /// </summary>
            /// <param name="obj">对象。</param>
            /// <param name="spawned">对象是否已被获取。</param>
            public void Register(T obj, bool spawned)
            {
                if (obj == null)
                {
                    throw new Exception("Object is invalid.");
                }

                //GameFrameworkLog.Debug(spawned ? "Object pool '{0}' create and spawned '{1}'." : "Object pool '{0}' create '{1}'.", Utility.Text.GetFullName<T>(Name), obj.Name);
                m_Objects.AddLast(new Object<T>(obj, spawned));

                Release();
            }

            /// <summary>
            /// 检查对象。
            /// </summary>
            /// <returns>要检查的对象是否存在。</returns>
            public bool CanSpawn()
            {
                return CanSpawn(string.Empty);
            }

            /// <summary>
            /// 检查对象。
            /// </summary>
            /// <param name="name">对象名称。</param>
            /// <returns>要检查的对象是否存在。</returns>
            public bool CanSpawn(string name)
            {
                foreach (Object<T> obj in m_Objects)
                {
                    if (obj.Name != name)
                    {
                        continue;
                    }

                    if (m_AllowMultiSpawn || !obj.IsInUse)
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// 获取对象。
            /// </summary>
            /// <returns>要获取的对象。</returns>
            public T Spawn()
            {
                return Spawn(string.Empty);
            }

            /// <summary>
            /// 获取对象。
            /// </summary>
            /// <param name="name">对象名称。</param>
            /// <returns>要获取的对象。</returns>
            public T Spawn(string name)
            {
                foreach (Object<T> obj in m_Objects)
                {
                    if (obj.Name != name)
                    {
                        continue;
                    }

                    if (m_AllowMultiSpawn || !obj.IsInUse)
                    {
                        //GameFrameworkLog.Debug("Object pool '{0}' spawn '{1}'.", Utility.Text.GetFullName<T>(Name), obj.Peek().Name);
                        return obj.Spawn();
                    }
                }

                return null;
            }

            /// <summary>
            /// 回收对象。
            /// </summary>
            /// <param name="obj">要回收的内部对象。</param>
            public void Unspawn(T obj)
            {
                if (obj == null)
                {
                    throw new Exception("Object is invalid.");
                }

                Unspawn(obj.Target);
            }

            /// <summary>
            /// 回收对象。
            /// </summary>
            /// <param name="target">要回收的对象。</param>
            public void Unspawn(object target)
            {
                if (target == null)
                {
                    throw new Exception("Target is invalid.");
                }

                foreach (Object<T> obj in m_Objects)
                {
                    if (obj.Peek().Target == target)
                    {
                        //GameFrameworkLog.Debug("Object pool '{0}' unspawn '{1}'.", Utility.Text.GetFullName<T>(Name), obj.Peek().Name);
                        obj.Unspawn();
                        Release();
                        return;
                    }
                }

                throw new Exception(string.Format("Can not find target in object pool '{0}'.", Utility.Text.GetFullName<T>(Name)));
            }

            /// <summary>
            /// 设置对象是否被加锁。
            /// </summary>
            /// <param name="obj">要设置被加锁的内部对象。</param>
            /// <param name="locked">是否被加锁。</param>
            public void SetLocked(T obj, bool locked)
            {
                if (obj == null)
                {
                    throw new Exception("Object is invalid.");
                }

                SetLocked(obj.Target, locked);
            }

            /// <summary>
            /// 设置对象是否被加锁。
            /// </summary>
            /// <param name="target">要设置被加锁的对象。</param>
            /// <param name="locked">是否被加锁。</param>
            public void SetLocked(object target, bool locked)
            {
                if (target == null)
                {
                    throw new Exception("Target is invalid.");
                }

                foreach (Object<T> obj in m_Objects)
                {
                    if (obj.Peek().Target == target)
                    {
                        //GameFrameworkLog.Debug("Object pool '{0}' set locked '{1}' to '{2}.", Utility.Text.GetFullName<T>(Name), obj.Peek().Name, locked.ToString());
                        obj.Locked = locked;
                        return;
                    }
                }

                throw new Exception(string.Format("Can not find target in object pool '{0}'.", Utility.Text.GetFullName<T>(Name)));
            }

            /// <summary>
            /// 设置对象的优先级。
            /// </summary>
            /// <param name="obj">要设置优先级的内部对象。</param>
            /// <param name="priority">优先级。</param>
            public void SetPriority(T obj, int priority)
            {
                if (obj == null)
                {
                    throw new Exception("Object is invalid.");
                }

                SetPriority(obj.Target, priority);
            }

            /// <summary>
            /// 设置对象的优先级。
            /// </summary>
            /// <param name="target">要设置优先级的对象。</param>
            /// <param name="priority">优先级。</param>
            public void SetPriority(object target, int priority)
            {
                if (target == null)
                {
                    throw new Exception("Target is invalid.");
                }

                foreach (Object<T> obj in m_Objects)
                {
                    if (obj.Peek().Target == target)
                    {
                        //GameFrameworkLog.Debug("Object pool '{0}' set priority '{1}' to '{2}.", Utility.Text.GetFullName<T>(Name), obj.Peek().Name, priority.ToString());
                        obj.Priority = priority;
                        return;
                    }
                }

                throw new Exception(string.Format("Can not find target in object pool '{0}'.", Utility.Text.GetFullName<T>(Name)));
            }

            /// <summary>
            /// 释放对象池中的可释放对象。
            /// </summary>
            public override void Release()
            {
                Release(m_Objects.Count - m_Capacity, DefaultReleaseObjectFilterCallback);
            }

            /// <summary>
            /// 释放对象池中的可释放对象。
            /// </summary>
            /// <param name="toReleaseCount">尝试释放对象数量。</param>
            public override void Release(int toReleaseCount)
            {
                Release(toReleaseCount, DefaultReleaseObjectFilterCallback);
            }

            /// <summary>
            /// 释放对象池中的可释放对象。
            /// </summary>
            /// <param name="releaseObjectFilterCallback">释放对象筛选函数。</param>
            public void Release(ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
            {
                Release(m_Objects.Count - m_Capacity, releaseObjectFilterCallback);
            }

            /// <summary>
            /// 释放对象池中的可释放对象。
            /// </summary>
            /// <param name="toReleaseCount">尝试释放对象数量。</param>
            /// <param name="releaseObjectFilterCallback">释放对象筛选函数。</param>
            public void Release(int toReleaseCount, ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
            {
                if (releaseObjectFilterCallback == null)
                {
                    throw new Exception("Release object filter callback is invalid.");
                }

                m_AutoReleaseTime = 0f;
                if (toReleaseCount < 0)
                {
                    toReleaseCount = 0;
                }

                DateTime expireTime = DateTime.MinValue;
                if (m_ExpireTime < float.MaxValue)
                {
                    expireTime = DateTime.Now.AddSeconds(-m_ExpireTime);
                }

                GetCanReleaseObjects(m_CachedCanReleaseObjects);
                List<T> toReleaseObjects = releaseObjectFilterCallback(m_CachedCanReleaseObjects, toReleaseCount, expireTime);
                if (toReleaseObjects == null || toReleaseObjects.Count <= 0)
                {
                    return;
                }

                foreach (ObjectBase toReleaseObject in toReleaseObjects)
                {
                    if (toReleaseObject == null)
                    {
                        throw new Exception("Can not release null object.");
                    }

                    bool found = false;
                    foreach (Object<T> obj in m_Objects)
                    {
                        if (obj.Peek() != toReleaseObject)
                        {
                            continue;
                        }

                        m_Objects.Remove(obj);
                        obj.Release(false);
                        //GameFrameworkLog.Debug("Object pool '{0}' release '{1}'.", Utility.Text.GetFullName<T>(Name), toReleaseObject.Name);
                        found = true;
                        break;
                    }

                    if (!found)
                    {
                        throw new Exception("Can not release object which is not found.");
                    }
                }
            }

            /// <summary>
            /// 释放对象池中的所有未使用对象。
            /// </summary>
            public override void ReleaseAllUnused()
            {
                LinkedListNode<Object<T>> current = m_Objects.First;
                while (current != null)
                {
                    if (current.Value.IsInUse || current.Value.Locked || !current.Value.CustomCanReleaseFlag)
                    {
                        current = current.Next;
                        continue;
                    }

                    LinkedListNode<Object<T>> next = current.Next;
                    m_Objects.Remove(current);
                    current.Value.Release(false);
                    //GameFrameworkLog.Debug("Object pool '{0}' release '{1}'.", Utility.Text.GetFullName<T>(Name), current.Value.Name);
                    current = next;
                }
            }

            /// <summary>
            /// 获取所有对象信息。
            /// </summary>
            /// <returns>所有对象信息。</returns>
            public override ObjectInfo[] GetAllObjectInfos()
            {
                int index = 0;
                ObjectInfo[] results = new ObjectInfo[m_Objects.Count];
                foreach (Object<T> obj in m_Objects)
                {
                    results[index++] = new ObjectInfo(obj.Name, obj.Locked, obj.CustomCanReleaseFlag, obj.Priority, obj.LastUseTime, obj.SpawnCount);
                }

                return results;
            }

            internal override void Update(float elapseSeconds, float realElapseSeconds)
            {
                m_AutoReleaseTime += realElapseSeconds;
                if (m_AutoReleaseTime < m_AutoReleaseInterval)
                {
                    return;
                }

                //GameFrameworkLog.Debug("Object pool '{0}' auto release start.", Utility.Text.GetFullName<T>(Name));
                Release();
                //GameFrameworkLog.Debug("Object pool '{0}' auto release complete.", Utility.Text.GetFullName<T>(Name));
            }

            internal override void Shutdown()
            {
                LinkedListNode<Object<T>> current = m_Objects.First;
                while (current != null)
                {
                    LinkedListNode<Object<T>> next = current.Next;
                    m_Objects.Remove(current);
                    current.Value.Release(true);
                    //GameFrameworkLog.Debug("Object pool '{0}' release '{1}'.", Utility.Text.GetFullName<T>(Name), current.Value.Name);
                    current = next;
                }
            }

            private void GetCanReleaseObjects(List<T> results)
            {
                if (results == null)
                {
                    throw new Exception("Results is invalid.");
                }

                results.Clear();
                foreach (Object<T> obj in m_Objects)
                {
                    if (obj.IsInUse || obj.Locked || !obj.CustomCanReleaseFlag)
                    {
                        continue;
                    }

                    results.Add(obj.Peek());
                }
            }

            private List<T> DefaultReleaseObjectFilterCallback(List<T> candidateObjects, int toReleaseCount, DateTime expireTime)
            {
                m_CachedToReleaseObjects.Clear();

                if (expireTime > DateTime.MinValue)
                {
                    for (int i = candidateObjects.Count - 1; i >= 0; i--)
                    {
                        if (candidateObjects[i].LastUseTime <= expireTime)
                        {
                            m_CachedToReleaseObjects.Add(candidateObjects[i]);
                            candidateObjects.RemoveAt(i);
                            continue;
                        }
                    }

                    toReleaseCount -= m_CachedToReleaseObjects.Count;
                }

                for (int i = 0; toReleaseCount > 0 && i < candidateObjects.Count; i++)
                {
                    for (int j = i + 1; j < candidateObjects.Count; j++)
                    {
                        if (candidateObjects[i].Priority > candidateObjects[j].Priority
                            || candidateObjects[i].Priority == candidateObjects[j].Priority && candidateObjects[i].LastUseTime > candidateObjects[j].LastUseTime)
                        {
                            T temp = candidateObjects[i];
                            candidateObjects[i] = candidateObjects[j];
                            candidateObjects[j] = temp;
                        }
                    }

                    m_CachedToReleaseObjects.Add(candidateObjects[i]);
                    toReleaseCount--;
                }

                return m_CachedToReleaseObjects;
            }
        }
    }
}
