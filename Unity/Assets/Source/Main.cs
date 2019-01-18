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

        List<IManager> _managers = new List<IManager>()
        {
             Manager.NetworkMgr,
             Manager.ResMgr,
             Manager.LuaMgr,
            //UpdateManager.Instance,
            //GameManager.Instance,
        };
        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FairyGUI.UIObjectFactory.SetLoaderExtension(typeof(MyLoader));

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
            yield return GameManager.Instance.LoadResource();

            yield return new WaitForEndOfFrame();
            GameManager.Instance.StartGame();
        }

        void Update()
        {
            Manager.ResMgr.Update();
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