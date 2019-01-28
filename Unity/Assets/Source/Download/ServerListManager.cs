using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class ServerListManager : IManager
    {
        /// <summary>
        /// 各渠道App下载链接配置文件
        /// </summary>
        public string UpdateAppUrl { get { return _config.UpdateAppUrl; } }
        /// <summary>
        /// 资源更新列表
        /// </summary>
        public List<string> UpdateResUrls { get { return _config.UpdateResUrls; } }
        /// <summary>
        /// 服务器区服列表
        /// </summary>
        public string ServerList { get { return _serverList; } }

        internal class UrlConfig
        {
            public string UpdateAppUrl;
            public List<string> UpdateResUrls;

            public string AndroidServerListUrl;
            public string IosServerListUrl;
        }
        private UrlConfig _config;
        private string _serverList;

        public void Init() { }
        public void Dispose() { _config = null; }
        public IEnumerator GetServerList()
        {
            string file = string.Format("{0}config/{1}", Util.DataPath, ConstSetting.UrlConfig);
            string json = File.ReadAllText(file);
            _config = JsonUtility.FromJson<UrlConfig>(json);
            if (_config == null)
            {
                Debug.LogErrorFormat("本地未配置{0}文件.", ConstSetting.UrlConfig);
                yield break;
            }

            string url = Application.platform == RuntimePlatform.Android ?
              _config.AndroidServerListUrl : _config.IosServerListUrl;
            WWW www = new WWW(url);
            yield return www;
            if (www.error != null)
            {
                Debug.LogErrorFormat("服务无{0}配置文件.", url);
                yield break;
            }
            _serverList = www.text;
        }
    }
}

