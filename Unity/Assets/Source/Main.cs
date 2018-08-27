using Game.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Game
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        public Action<float, float> Updater;
        public Action LateUpdater;
        public Action<float> FixedUpdater;


        List<IManager> _managers = new List<IManager>()
        {
            LuaManager.Instance,
            ResourceManager.Instance,
            GameManger.Instance,
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
            yield return GameManger.Instance.InitSDK();
            yield return GameManger.Instance.DowmloadUnZip();
            yield return GameManger.Instance.UnZipFile();
            yield return GameManger.Instance.UpdateAppVersion();
            yield return GameManger.Instance.UpdateResVersion();
            yield return GameManger.Instance.GetServerList();
            yield return GameManger.Instance.LoadResource();

            GameManger.Instance.StartGame();
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