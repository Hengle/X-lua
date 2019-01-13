using UnityEngine;

namespace Game
{
    public class Manager : MonoBehaviour
    {
        public static ResourceManager ResMgr = new ResourceManager();
        public static GameManager GameMgr = new GameManager();
        public static PoolManager PoolMgr = new PoolManager();
        public static LuaManager LuaMgr = new LuaManager();
    }
}

