using System.IO;
using System.Text;
using UnityEngine;

namespace Game
{
    public class Manager : MonoBehaviour
    {
        public static NetworkManager NetworkMgr = new NetworkManager();
        public static UpdateManager UpdateMgr = new UpdateManager();
        public static ResourceManager ResMgr = new ResourceManager();
        public static GameManager GameMgr = new GameManager();
        public static PoolManager PoolMgr = new PoolManager();
        public static LuaManager LuaMgr = new LuaManager();     

        private void Awake()
        {
            //TCPAwake();
        }
        private void Update()
        {
            //TCPUpdate();
        }
        private void OnGUI()
        {
            //TCPOnGUI();
        }



        #region TCP 通讯测试
        string _text = "";
        NetworkChannel Channel;
        private void TCPAwake()
        {
            string ip = "192.168.50.90";
            int port = 8686;
            NetworkMgr.OnNetworkConnected = onChannelConnected;
            NetworkMgr.OnNetworkClosed = onChannelClosed;

            NetworkChannel channel = NetworkMgr.CreateNetworkChannel("NetworkChannel") as NetworkChannel;
            //channel.NetworkReceive = Receive;
            channel.Connect(ip, port);
        }
        void TCPUpdate()
        {
            NetworkMgr.Update();
        }
        void TCPOnGUI()
        {
            //GUILayout.BeginHorizontal();
            ////_text = GUILayout.TextArea(_text, GUILayout.Width(500), GUILayout.Height(500));
            //if (GUILayout.Button("Send", GUILayout.Width(200), GUILayout.Height(200)))
            //{
            //    if (Channel == null)
            //        Channel = Manager.NetworkMgr.GetNetworkChannel("NetworkChannel") as NetworkChannel;
            //    int n = 11;// random.Next(1, );
            //    StringBuilder sb = new StringBuilder();
            //    for (int i = 0; i < n; i++)
            //    {
            //        if (n == i + 1)
            //            sb.Append("X");
            //        else
            //            sb.Append(i);
            //    }
            //    Channel.Send(n, System.Text.Encoding.ASCII.GetBytes(sb.ToString()));
            //}
            //if (GUILayout.Button("Clear", GUILayout.Width(200), GUILayout.Height(200)))
            //    _text = "";
            //GUILayout.EndHorizontal();
        }
        void onChannelConnected(NetworkChannel channel)
        {
            Debug.LogError(channel.Name + "-onChannelConnected");
        }
        void onChannelClosed(NetworkChannel channel)
        {
            Debug.LogError(channel.Name + "-onChannelClosed");
        }
        void onMissHeartBeat(NetworkChannel channel, int count)
        {

        }
        void Receive(int type, byte[] msg)
        {
            string content = System.Text.Encoding.UTF8.GetString(msg, 0, msg.Length);
            Debug.LogFormat("{2}:{0}-{1}", type, content, System.DateTime.Now);
            Debug.LogFormat("msgLenght:{0}   stringLength:{1}", msg.Length, content.Length);
            File.WriteAllText(@"C:\Users\Administrator\Desktop\txt\msg.txt", content);
        }
        #endregion
    }
}

