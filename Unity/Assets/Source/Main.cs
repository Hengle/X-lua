using Game.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using XLua;

namespace Game
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        List<IManager> _managers = new List<IManager>()
        {
            LuaManager.Instance,
            //UpdateManager.Instance,
            //GameManager.Instance,
            //ResourceManager.Instance,
        };

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

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
            //yield return GameManager.Instance.InitSDK();
            //yield return GameManager.Instance.DowmloadUnZip();
            //yield return GameManager.Instance.UnZipFile();
            //yield return GameManager.Instance.UpdateAppVersion();
            //yield return GameManager.Instance.UpdateResVersion();
            //yield return GameManager.Instance.GetServerList();
            //yield return GameManager.Instance.LoadResource();

            yield return new WaitForEndOfFrame();
            GameManager.Instance.StartGame();
        }

        void OnDestroy()
        {
            for (int i = 0; i < _managers.Count; i++)
                _managers[i].Dispose();
            _managers.Clear();
            Instance = null;
        }
    }
}