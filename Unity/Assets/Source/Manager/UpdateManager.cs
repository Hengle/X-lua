using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 游戏热更新管理器
    /// 注:必须在资源管理器初始化完毕,确保能加载AB资源
    /// </summary>
    public class UpdateManager : IManager
    {
        public string LocalResVersion { get { return _localResVersion; } }
        public string LocalAppVersion { get { return _localAppVersion; } }
        public string RemoteResVersion { get { return _remoteResVersion; } }
        public string RemoteAppVersion { get { return _remoteAppVersion; } }


        private string _localResVersion;
        private string _localAppVersion;
        private string _remoteResVersion;
        private string _remoteAppVersion;
        private long _downloadSize = 0;
        private long _totalDownloadSize = 0;

        private Dictionary<string, AssetInfo> _localMD5Table = new Dictionary<string, AssetInfo>();
        private Dictionary<string, AssetInfo> _remoteMD5Table = new Dictionary<string, AssetInfo>();
        private HashSet<string> _hasDownload = new HashSet<string>();

        /// <summary>
        /// 格式:a.r.t;
        /// a:app版本号
        /// r:资源版本号
        /// t:当前版本资源构建时间,yymmddhh年月日时
        /// </summary>
        private string _resVersionFile = "version.txt";
        /// <summary>
        /// 资源版本信息记录格式:path,md5,pathtype,size
        /// path:资源相对路径
        /// md5:文件MD5信息
        /// pathtype:路径类型.r:流目录[只读];rw:持续化目录[读写]
        /// size:文件大小,单位字节
        /// </summary>
        private string _resMD5File = "resmd5.txt";
        /// <summary>
        /// 当前已下载资源名称,中断后不重复下载
        /// </summary>
        private string _hasDownloadFile = "hasdownload.txt";
        private string _urlRoot = "http://localhost:8086/";

        private bool _downloadOK = true;


        public void Init()
        {
        }

        public void Dispose()
        {

        }

        public void Update()
        {

        }

        public bool HasdownloadFile { get { return File.Exists(Util.DataPath + _hasDownloadFile); } }
        public IEnumerator CheckVersion()
        {
            string path = Util.DataPath + _resVersionFile;

            string version = File.ReadAllText(path);
            string[] nodes = version.Split(",".ToCharArray());
            _localAppVersion = nodes[0];
            _localResVersion = nodes[1];
            yield return DwonloadRemoteVersion("");
        }
        IEnumerator BeginDownloadApplication()
        {
            yield return null;
        }
        IEnumerator BeginDownloadAssets()
        {
            ServicePointManager.DefaultConnectionLimit = 50;

            yield return null;
        }


        string LoadLocalFile(string localFile)
        {
            string file = Util.DataPath + localFile;
            string content = null;
            try
            {
                content = File.ReadAllText(file);
                if (!string.IsNullOrEmpty(content))
                    return content.Trim();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return "";
            }
            return content;
        }
        IEnumerator DwonloadRemoteVersion(string remoteFile)
        {
            string url = _urlRoot + remoteFile;
            using (WWW www = new WWW(url))
            {
                yield return www;
                if (www.error != null)
                {
                    _downloadOK = false;
                    yield break;
                }
            }
        }


        void SaveHasDownloadInfo()
        {

        }
        void SaveMD5Table()
        {

        }
    }
}