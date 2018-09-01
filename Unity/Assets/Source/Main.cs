using Game.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using XLua;

namespace Game
{
    [CSharpCallLua]
    public delegate void UpdateFunc(float deltaTime, float unscaledDeltaTime);
    [CSharpCallLua]
    public delegate void LateUpdateFunc();
    [CSharpCallLua]
    public delegate void FixedUpdateFunc(float fixedDeltaTime);

    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        public UpdateFunc Updater;
        public LateUpdateFunc LateUpdater;
        public FixedUpdateFunc FixedUpdater;


        List<IManager> _managers = new List<IManager>()
        {
            LuaManager.Instance,
            ResourceManager.Instance,
            GameManager.Instance,
        };

        void Awake()
        {
            Instance = this;
            var uiRoot = GameObject.Find("/UIRoot");
            var eSystem = GameObject.Find("/EventSystem");
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(uiRoot);
            DontDestroyOnLoad(eSystem);

            for (int i = 0; i < _managers.Count; i++)
                _managers[i].Init();

            Util.SetResolution(AppConst.Resolution);
            Application.targetFrameRate = AppConst.FrameRate;
            QualitySettings.blendWeights = BlendWeights.TwoBones;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Interface.Create();
            Interface.Instance.Init();
        }

        IEnumerator Start()
        {
            yield return GameManager.Instance.InitSDK();
            yield return GameManager.Instance.DowmloadUnZip();
            yield return GameManager.Instance.UnZipFile();
            yield return GameManager.Instance.UpdateAppVersion();
            yield return GameManager.Instance.UpdateResVersion();
            yield return GameManager.Instance.GetServerList();
            yield return GameManager.Instance.LoadResource();

            GameManager.Instance.StartGame();
        }

        void Update()
        {
            LuaManager.Instance.Tick();

            if (AppConst.EnableProfile)
                Profiler.BeginSample("Client.Updater");
            if (Updater != null)
                Updater(Time.deltaTime, Time.unscaledDeltaTime);
            if (AppConst.EnableProfile)
                Profiler.EndSample();
        }
        void LateUpdate()
        {
            if (AppConst.EnableProfile)
                Profiler.BeginSample("Client.LateUpdater");
            if (Updater != null)
                LateUpdater();
            if (AppConst.EnableProfile)
                Profiler.EndSample();
        }
        void FixedUpdate()
        {
            if (AppConst.EnableProfile)
                Profiler.BeginSample("Client.FixedUpdater");
            if (Updater != null)
                FixedUpdater(Time.fixedDeltaTime);
            if (AppConst.EnableProfile)
                Profiler.EndSample();
        }
        void OnApplicationQuit()
        {
            Updater = null;
            LateUpdater = null;
            FixedUpdater = null;

            for (int i = 0; i < _managers.Count; i++)
                _managers[i].Dispose();
            _managers.Clear();
            Instance = null;
        }
    }
}