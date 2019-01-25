namespace Game
{
    public class AssetInfo
    {
        public string Path
        {
            get;
            private set;
        }

        public string MD5
        {
            get;
            private set;
        }

        public long Size
        {
            get;
            private set;
        }
    }

    public class DownloadTask
    {
        public static long ChunkSize = 4096L;
        private string _uri;
        private AssetInfo _localInfo;
        private AssetInfo _remoteInfo;
        private UpdateManager _updater;

        public DownloadTask(string uri, AssetInfo localInfo, AssetInfo remoteInfo, UpdateManager updater)
        {
            _uri = uri;
            _localInfo = localInfo;
            _remoteInfo = remoteInfo;
            _updater = updater;
        }

        public void BeginDownload()
        {

        }
    }
}

