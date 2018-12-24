using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class NetworkManager : IManager
    {
        public static NetworkManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NetworkManager();
                return _instance;
            }
        }
        static NetworkManager _instance;
        protected NetworkManager() { }

        public void Init()
        {

        }
        public void Dispose()
        {

        }
    }

}


