using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BundleRefInfo : CacheObject<AssetBundle>
    {
        public int RefCount { get { return _refCount; } }
        public float RefDeleteTime { get { return _cacheTime; } }

        private int _refCount;

        public BundleRefInfo(string name, AssetBundle ab) : base(name, ab)
        {
        }

        public void AddCount(string name)
        {

        }
        public void RemoveCount(string name)
        {

        }

    }

}