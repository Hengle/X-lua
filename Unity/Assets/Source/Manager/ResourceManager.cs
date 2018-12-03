using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using Object = UnityEngine.Object;

public class ManagedResource : MonoBehaviour
{
    public string bundlename;
    public void OnDestroy()
    {
        //Util.LogColor("green", "~~~ OnDestroy : " + bundlename);
        Game.ResourceManager.Instance.RemoveRefCount(bundlename);
    }
}

namespace Game
{
    public enum ResourceLoadType
    {
        Default = 0,                          // 资源已加载,直接取资源
        Persistent = 1 << 0,                     // 永驻内存的资源
        Cache = 1 << 1,                     // Asset需要缓存

        UnLoad = 1 << 4,                     // 利用www加载并且处理后是否立即unload ab,如不卸载,则在指定时间后清理
        Immediate = 1 << 5,                     // 需要立即加载
        // 加载方式
        LoadBundleFromFile = 1 << 6,                     // 利用AssetBundle.LoadFromFile加载
        LoadBundleFromWWW = 1 << 7,                     // 利用WWW 异步加载 AssetBundle
        ReturnAssetBundle = 1 << 8,                     // 返回scene AssetBundle,默认unload ab
    }
    class CacheObject
    {
        public Object obj;
        public float lastUseTime;
    }
    class AssetLoadTask
    {
        public uint id;
        public List<uint> parentTaskIds;
        public int loadType;
        public string path;
        public Action<Object> actions;
        public List<string> dependencies;
        public int loadedDependenciesCount = 0;
        public void Reset()
        {
            id = 0;
            parentTaskIds = null;
            path = string.Empty;
            actions = null;
            dependencies = null;
            loadedDependenciesCount = 0;
        }
    }
    /// <summary>
    /// AssetBundle加载完毕,后续处理
    /// </summary>
    class LoadTask
    {
        public AssetLoadTask task;
        public AssetBundle bundle;
    }

    public class ResourceManager : IManager
    {
        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ResourceManager();
                return _instance;
            }
        }
        static ResourceManager _instance;
        protected ResourceManager() { }

        //资源存储策略
        private Dictionary<string, Object> persistantObjects = new Dictionary<string, Object>();       //key-依赖路径
        private Dictionary<string, CacheObject> cacheObjects = new Dictionary<string, CacheObject>();
        //加载的AB资源包
        private Dictionary<string, AssetBundle> dependenciesObj = new Dictionary<string, AssetBundle>();  //key-依赖路径
        //加载任务
        private Dictionary<string, AssetLoadTask> loadingFiles = new Dictionary<string, AssetLoadTask>();//key-文件名
        private Dictionary<uint, AssetLoadTask> loadingTasks = new Dictionary<uint, AssetLoadTask>();  //key-任务ID
        private ObjectPool<AssetLoadTask> assetLoadTaskPool = new ObjectPool<AssetLoadTask>();        //加载资源的相关信息
        private Queue<AssetLoadTask> delayAssetLoadTask = new Queue<AssetLoadTask>();
        private ObjectPool<LoadTask> loadTaskPool = new ObjectPool<LoadTask>();
        private Queue<LoadTask> delayLoadTask = new Queue<LoadTask>();

        private bool canStartCleanupMemeory = true;
        private const float cleanupMemInterval = 180;
        private const float cleanupCacheInterval = 120;
        private const float cleanupDepInterval = 30;

        private string preloadListPath = "config/preloadlist.txt";
        private List<string> preloadList = new List<string>();
        private int currentPreLoadCount = 0;

        private Dictionary<string, int> refCount = new Dictionary<string, int>();
        private Dictionary<string, float> refDelTime = new Dictionary<string, float>();
        private int currentTaskCount = 0;
        private const int defaultMaxTaskCount = 10;

        private float cleanupMemoryLastTime;
        private float cleanupCacheBundleLastTime;
        private float cleanupDependenciesLastTime;

        private AssetBundleManifest manifest;

        private static uint nextTaskId;

        public int MaxTaskCount { get; set; }

        [DoNotGen]
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            int memorySize = 1024 * 1024; //内存大小1GB
            if (memorySize <= 1024 * 1024)
            {
                MaxTaskCount = 5;
            }

            byte[] stream = null;
            string path = "";
#if UNITY_EDITOR || UNITY_EDITOR_WIN
            path = GetResPath("GamePlayer");
#elif UNITY_ANDROID
            path = GetResPath("Android");
#elif UNITY_IOS
            path = GetResPath("IOS");
#endif
            if (!File.Exists(path)) return;
            stream = File.ReadAllBytes(path);
            var assetbundle = AssetBundle.LoadFromMemory(stream);
            manifest = assetbundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            LoadPreLoadList();
            PreLoadResource();
        }

        private void LoadPreLoadList()
        {
            preloadList.Clear();
            string dataPath = GetResPath(preloadListPath);

            if (!File.Exists(dataPath)) return;

            foreach (string p in File.ReadAllLines(dataPath))
                preloadList.Add(p);
        }
        private void PreLoadResource()
        {
            foreach (string path in preloadList)
            {
                if (path.Contains("_atlas0!a") && Application.platform != UnityEngine.RuntimePlatform.Android)
                    currentPreLoadCount++;
                else
                    AddTask(path, PreLoadEventHandler, (int)(ResourceLoadType.LoadBundleFromFile | ResourceLoadType.Persistent));
            }
        }
        private void PreLoadEventHandler(UnityEngine.Object obj)
        {
            currentPreLoadCount++;
        }
        /// <summary>
        /// 判断预加载资源是否已经加载结束
        /// </summary>
        public bool IsPreLoadDone { get { return currentPreLoadCount >= preloadList.Count; } }


        public bool IsLoading(uint taskId)
        {
            return loadingTasks.ContainsKey(taskId);
        }
        public void RemoveTask(uint taskId, Action<Object> action)
        {
            if (IsLoading(taskId))
            {
                AssetLoadTask oldTask = null;
                if (loadingTasks.TryGetValue(taskId, out oldTask))
                {
                    if (null != action)
                    {
                        oldTask.actions -= action;
                    }
                }
            }
        }

        public uint AddTask(string file, LuaFunction action, int loadType = (int)ResourceLoadType.LoadBundleFromFile)
        {
            return AddTask(file, o => action.Call(o), loadType, 0);
        }
        public uint AddTask(string file, Action<Object> action, int loadType = (int)ResourceLoadType.LoadBundleFromFile)
        {
            return AddTask(file, action, loadType, 0);
        }
        private uint AddTask(string file, Action<Object> action, int loadType, uint parentTaskId)
        {
            if (string.IsNullOrEmpty(file))
            {
                return 0;
            }
            string fileReplace = file.Replace(@"\", @" / ");

            string lowerFile = fileReplace.ToLower();
            Object obj;
            if (persistantObjects.TryGetValue(lowerFile, out obj))
            {
                action(obj);
                return 0;
            }

            CacheObject cacheObject;
            if (cacheObjects.TryGetValue(lowerFile, out cacheObject))
            {
                cacheObject.lastUseTime = Time.realtimeSinceStartup;

                action(cacheObject.obj);
                return 0;
            }

            AssetLoadTask oldTask;
            if (loadingFiles.TryGetValue(lowerFile, out oldTask))
            {//资源加载任务-正在进行中
                if (action != null)
                {
                    oldTask.actions += action;
                }

                if ((loadType & (int)ResourceLoadType.Persistent) > 0)
                {
                    oldTask.loadType |= (int)ResourceLoadType.Persistent;
                }

                if (parentTaskId != 0)
                {
                    if (oldTask.parentTaskIds == null)
                    {
                        oldTask.parentTaskIds = new List<uint>();
                        Debug.LogErrorFormat("resource path {0} type is mixed, dependency resource or not ", oldTask.path);
                    }
                    oldTask.parentTaskIds.Add(parentTaskId);
                }

                return 0;
            }

            //加载资源到内存
            uint id = ++nextTaskId;
            List<uint> ptList = null;
            if (parentTaskId != 0)
            {
                ptList = new List<uint>();
                ptList.Add(parentTaskId);
            }
            string[] deps = manifest.GetAllDependencies(lowerFile);
            var task = assetLoadTaskPool.Get();
            {
                task.id = id;
                task.parentTaskIds = ptList;
                task.path = lowerFile;
                task.loadType = loadType;
                task.actions = action;
                task.dependencies = deps == null ? null : new List<string>(deps);
                task.loadedDependenciesCount = 0;
            };
            loadingFiles[lowerFile] = task;
            loadingTasks[id] = task;
            if (dependenciesObj.ContainsKey(task.path))
            {//依赖资源已被加载
                AddRefCount(task.path);
            }
            //任务数量达最大时,延迟加载资源[需要排队]
            if (currentTaskCount < MaxTaskCount)
                DoTask(task);
            else
                delayAssetLoadTask.Enqueue(task);

            return id;
        }
        public void AddRefCount(string bundlename)
        {
            string[] dependencies = manifest.GetAllDependencies(bundlename);
            if (dependencies != null && dependencies.Length > 0)
            {
                for (int i = 0; i < dependencies.Length; i++)
                {
                    string depname = dependencies[i];
                    if (!persistantObjects.ContainsKey(depname))
                    {
                        if (!refCount.ContainsKey(depname))
                        {
                            refCount[depname] = 0;
                        }
                        refCount[depname]++;
                    }
                }
            }
        }
        public void RemoveRefCount(string bundlename)
        {
            string[] dependencies = manifest.GetAllDependencies(bundlename);
            if (dependencies != null && dependencies.Length > 0)
            {
                for (int i = 0; i < dependencies.Length; i++)
                {
                    string depname = dependencies[i];
                    if (refCount.ContainsKey(depname))
                    {
                        refCount[depname]--;
                        if (refCount[depname] <= 0)
                        {
                            refDelTime[depname] = Time.realtimeSinceStartup;
                        }
                    }
                }
            }
        }
        private void DoTask(AssetLoadTask task)
        {
            if (task.dependencies == null)
            {
                DoImmediateTask(task);
            }
            else
            {
                if (task.loadedDependenciesCount >= task.dependencies.Count)
                {
                    DoImmediateTask(task);
                }
                else
                {
                    int i = task.loadedDependenciesCount;
                    for (; i < task.dependencies.Count; ++i)
                    {
                        if (dependenciesObj.ContainsKey(task.dependencies[i]) || persistantObjects.ContainsKey(task.dependencies[i]))
                        {
                            task.loadedDependenciesCount += 1;
                            if (task.loadedDependenciesCount >= task.dependencies.Count)
                            {
                                DoImmediateTask(task);
                                return;
                            }
                        }
                        else
                        {
                            AddTask(task.dependencies[i], null, task.loadType, task.id);
                        }
                    }
                }
            }
        }
        private void DoImmediateTask(AssetLoadTask task)
        {
            currentTaskCount += 1;
            if ((task.loadType & (int)ResourceLoadType.LoadBundleFromWWW) != 0)
            {
                Main.Instance.StartCoroutine(LoadBundleFromWWW(task));
            }
            else if ((task.loadType & (int)ResourceLoadType.LoadBundleFromFile) != 0)
            {
                LoadBundleFromFile(task);
            }
            else
            {
                currentTaskCount -= 1;
                Debug.LogErrorFormat("Unknown task loadtype:{0} path:{1}", task.loadType, task.path);
            }
        }
        private IEnumerator LoadBundleFromWWW(AssetLoadTask task)
        {
            string path = GetResPath(task.path);
            var www = new WWW(path);
            yield return www;
            if (null != www.error)
                Debug.LogErrorFormat("LoadBundleAsync: {0} failed! www error:{1}", task.path, www.error);
            OnBundleLoaded(task, www.assetBundle);
            www.Dispose();
        }
        private void LoadBundleFromFile(AssetLoadTask task)
        {
            string path = GetResPath(task.path);
            AssetBundle ab = AssetBundle.LoadFromFile(path);
            OnBundleLoaded(task, ab);
        }
        //AssetBundle加载完毕
        private void OnBundleLoaded(AssetLoadTask task, AssetBundle ab)
        {
            currentTaskCount -= 1;
            Object obj = null;
            if (ab == null)
            {
                Debug.LogErrorFormat("LoadBundle: {0} failed! assetBundle == NULL!", task.path);
                OnAseetsLoaded(task, ab, obj);
            }
            else
            {
                var loadTask = loadTaskPool.Get();
                loadTask.task = task;
                loadTask.bundle = ab;
                delayLoadTask.Enqueue(loadTask);
            }
        }
        [DoNotGen]
        public void Update()
        {
            CleanupCacheBundle();
            CleanupMemoryInterval();
            CleanupDependenciesInterval();
            //执行延迟加载任务
            DoDelayTasks();
        }
        private void DoDelayTasks()
        {
            if (delayAssetLoadTask.Count > 0)
            {
                while (delayAssetLoadTask.Count > 0 && currentTaskCount < MaxTaskCount)
                {
                    AssetLoadTask task = delayAssetLoadTask.Dequeue();
                    DoTask(task);
                }
            }
            if (delayLoadTask.Count > 0)
            {
                var maxLoadTime = 0.02f;
                var startTime = Time.realtimeSinceStartup;
                while (delayLoadTask.Count > 0 && Time.realtimeSinceStartup - startTime < maxLoadTime)
                {
                    LoadTask loadTask = delayLoadTask.Dequeue();
                    var task = loadTask.task;
                    var bundle = loadTask.bundle;

                    Object obj = null;
                    if (bundle != null)
                    {
                        if (!bundle.isStreamedSceneAssetBundle)
                        {
                            var objs = bundle.LoadAllAssets();
                            if (objs.Length > 0)
                                obj = objs[0];
                            if (obj == null)
                            {
                                Debug.LogErrorFormat("LoadBundle: {0} ! No Assets in Bundle!", task.path);
                            }
                        }
                    }
                    OnAseetsLoaded(task, bundle, obj);
                    loadTaskPool.Put(loadTask);
                }
            }
        }
        //Asset加载完毕,可能是依赖资源,也可能是主资源
        private void OnAseetsLoaded(AssetLoadTask task, AssetBundle ab, Object obj)
        {
            string[] dependencies = manifest.GetAllDependencies(task.path);
            if (dependencies == null || dependencies.Length == 0)
            {
                RemoveRefCount(task.path);
            }

            loadingFiles.Remove(task.path);
            loadingTasks.Remove(task.id);

            //主资源加载完毕
            if (task.actions != null && task.parentTaskIds == null)
            {
                Delegate[] delegates = task.actions.GetInvocationList();
                foreach (var dele in delegates)
                {
                    var action = (Action<Object>)dele;
                    try
                    {
                        if ((task.loadType & (int)ResourceLoadType.ReturnAssetBundle) > 0)
                        {
                            action(ab);
                        }
                        else
                        {
                            action(obj);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogErrorFormat("Load Bundle {0} DoAction Exception: {1}", task.path, e);
                    }
                }
            }

            //缓存加载的资源对象[非AB]
            if (ab != null && task.parentTaskIds == null)
            {
                if ((task.loadType & (int)ResourceLoadType.Persistent) > 0)
                {
                    persistantObjects[task.path] = obj;
                    if ((task.loadType & (int)ResourceLoadType.UnLoad) > 0)
                    {
                        ab.Unload(false);
                    }
                }
                else
                {
                    if ((task.loadType & (int)ResourceLoadType.Cache) > 0)
                    {
                        var cacheObject = new CacheObject
                        {
                            lastUseTime = Time.realtimeSinceStartup,
                            obj = obj
                        };

                        cacheObjects[task.path] = cacheObject;
                    }
                    if ((task.loadType & (int)ResourceLoadType.ReturnAssetBundle) == 0)
                    {
                        ab.Unload(false);
                    }
                }
            }

            //依赖资源加载完毕
            if (task.parentTaskIds != null)
            {
                dependenciesObj[task.path] = ab;
                for (int i = 0; i < task.parentTaskIds.Count; ++i)
                {
                    uint taskid = task.parentTaskIds[i];
                    AssetLoadTask pt = null;
                    if (loadingTasks.TryGetValue(taskid, out pt))
                    {
                        pt.loadedDependenciesCount += 1;
                        if (pt.loadedDependenciesCount >= pt.dependencies.Count)
                        {
                            DoTask(pt);
                        }
                    }
                }
            }

            task.Reset();
            assetLoadTaskPool.Put(task);
        }
        public string GetResPath(string relative)
        {
            relative = Util.StandardlizePath(relative);
            string fullPath = Util.DataPath + relative;
            if (!File.Exists(fullPath))
                fullPath = Util.StreamingPath + relative;
            return fullPath;
        }

        //定时清理功能       
        //-内存清理 
        public void CleanupMemoryInterval()
        {
            if (Time.realtimeSinceStartup > cleanupMemoryLastTime + cleanupMemInterval)
            {
                CleanupMemoryNow();
            }
        }
        public void CleanupMemoryNow()
        {
            if (canStartCleanupMemeory)
            {
                canStartCleanupMemeory = false;
                cleanupMemoryLastTime = Time.realtimeSinceStartup;
                Main.Instance.StartCoroutine(CleanupMemoryAsync());
            }
        }
        private IEnumerator CleanupMemoryAsync()
        {
            yield return Resources.UnloadUnusedAssets();
            GC.Collect();
            canStartCleanupMemeory = true;
            cleanupMemoryLastTime = Time.realtimeSinceStartup;
        }
        //-缓存包清理
        private void CleanupCacheBundle()
        {
            if (cacheObjects.Count <= 0) return;
            if (!(Time.realtimeSinceStartup > cleanupCacheBundleLastTime + 10)) return;

            var now = cleanupCacheBundleLastTime = Time.realtimeSinceStartup;

            var tempList = new List<string>();
            foreach (var pair in cacheObjects)
            {
                if (now > pair.Value.lastUseTime + cleanupCacheInterval)
                {
                    tempList.Add(pair.Key);
                    if (null != pair.Value.obj)
                    {
                        //Debug.LogError(" try to destroy object: " + pair.Key);
                        if (pair.Value.obj is GameObject)
                        {
                            UnityEngine.Object.DestroyImmediate(pair.Value.obj, true);
                        }
                        else
                        {
                            Resources.UnloadAsset(pair.Value.obj);
                        }
                    }
                }
            }
            foreach (var bundle in tempList)
            {
                cacheObjects.Remove(bundle);
            }
        }
        //-依赖包清理
        public void CleanupDependenciesInterval()
        {
            if (Time.realtimeSinceStartup > cleanupDependenciesLastTime + cleanupDepInterval)
            {
                CleanupDependenciesNow();
            }
        }
        public void CleanupDependenciesNow()
        {
            if (refCount == null || refDelTime == null || dependenciesObj == null)
            {
                return;
            }
            cleanupDependenciesLastTime = Time.realtimeSinceStartup;
            List<string> refCountToRemove = new List<string>();
            foreach (var pairs in refCount)
            {
                if (pairs.Value <= 0)
                {
                    if (dependenciesObj.ContainsKey(pairs.Key) &&
                        refDelTime.ContainsKey(pairs.Key) &&
                        Time.realtimeSinceStartup - refDelTime[pairs.Key] > 60)
                    {
                        if (dependenciesObj.ContainsKey(pairs.Key))
                        {
                            if (dependenciesObj[pairs.Key] != null)
                                dependenciesObj[pairs.Key].Unload(false);
                            dependenciesObj[pairs.Key] = null;
                            dependenciesObj.Remove(pairs.Key);
                        }
                        refDelTime.Remove(pairs.Key);
                        refCountToRemove.Add(pairs.Key);
                    }
                }
            }
            foreach (var remove in refCountToRemove)
            {
                refCount.Remove(remove);
            }
        }

        //其他操作        
        public void Dispose()
        {
            if (manifest != null) manifest = null;
            foreach (KeyValuePair<string, Object> pair in persistantObjects)
            {
                if (pair.Value != null)
                {
                    if (pair.Value is GameObject)
                    {
                        UnityEngine.Object.DestroyImmediate(pair.Value, true);
                    }
                    else
                    {
                        Resources.UnloadAsset(pair.Value);
                    }

                }
            }

            persistantObjects.Clear();

            foreach (var pair in dependenciesObj)
            {
                if (pair.Value != null)
                {
                    pair.Value.Unload(true);
                }
            }
            dependenciesObj.Clear();

            foreach (var pair in cacheObjects)
            {
                if (pair.Value != null && pair.Value.obj != null)
                {
                    if (pair.Value.obj is GameObject)
                    {
                        UnityEngine.Object.DestroyImmediate(pair.Value.obj, true);
                    }
                    else
                    {
                        Resources.UnloadAsset(pair.Value.obj);
                    }
                }
            }
            cacheObjects.Clear();
            Debug.Log("~ResourceManager was destroy!");
        }
    }
}
