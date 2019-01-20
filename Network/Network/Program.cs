using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.Text;
using System.Collections.Generic;
using LitJson;

namespace Network
{
    class Program
    {
        private static byte[] result = new byte[1024 * 64];
        private static int myProt = 8686;   //端口 
        static Socket serverSocket;
        static Random random = new Random(321561);
        static void Main(string[] args)
        {
            //服务器IP地址 
            IPAddress ip = IPAddress.Parse("192.168.50.90");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口 
            serverSocket.Listen(10);    //设定最多10个排队连接请求      
            serverSocket.ReceiveBufferSize = 1024 * 16;
            serverSocket.NoDelay = true;
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过Clientsoket发送数据 
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();

            ThreadPool.QueueUserWorkItem(ReceiveMessage);
            ThreadPool.QueueUserWorkItem(SendMessage);
            Console.ReadLine();
        }

        /// <summary> 
        /// 监听客户端连接 
        /// </summary> 
        private static void ListenClientConnect(object state)
        {
            Console.WriteLine("ClientConnectOK");
            serverSocket = serverSocket.Accept();

            //Protocol sendMsg = new Protocol();
            //int count = 65536;// random.Next(1, );
            //sendMsg.WriteInt(count);
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < count; i++)
            //{
            //    if (count == i + 1)
            //        sb.Append("X");
            //    else
            //        sb.Append("1");
            //}
            //byte[] msg = Encoding.ASCII.GetBytes(sb.ToString());
            //sendMsg.WriteBytes(msg);
            //clientSocket.Send(sendMsg.ToBytes());
        }

        private static void SendMessage(object state)
        {
            Console.WriteLine("SendMessageOK");
            while (true)
            {
                if (!serverSocket.Connected) continue;

                Thread.Sleep(1000);
                try
                {
                    //Protocol sendMsg = new Protocol();
                    //int type = random.Next(1, 100);
                    //JsonData data = RandomTable();
                    //string json = data.ToJson();
                    //byte[] msg = Encoding.UTF8.GetBytes(json);
                    //sendMsg.WriteInt(type);
                    //sendMsg.WriteBytes(msg);
                    //serverSocket.Send(sendMsg.ToBytes());
                    //Console.WriteLine("[{0}]{1} {2}", DateTime.Now, type, json);
                }
                catch (ObjectDisposedException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    serverSocket.Shutdown(SocketShutdown.Both);
                    serverSocket.Close();
                    break;
                }
            }
        }

        /// <summary> 
        /// 接收消息 
        /// </summary> 
        /// <param name="clientSocket"></param> 
        private static void ReceiveMessage(object state)
        {
            Console.WriteLine("ReceiveMessageOK");
            while (true)
            {
                if (!serverSocket.Connected) continue;
                Thread.Sleep(100);
                try
                {
                    //通过clientSocket接收数据 
                    int receiveNumber = serverSocket.Receive(result);
                    if (receiveNumber > 0)
                    {
                        Console.WriteLine("Send:{0}\nReceive:{1}", serverSocket.SendBufferSize, serverSocket.ReceiveBufferSize);
                        Protocol protocol = new Protocol(result);
                        int type = protocol.ReadInt();
                        int length = protocol.ReadInt();
                        byte[] msg = protocol.ReadBytes(length);
                        Console.WriteLine("{0}-{1} + msg:{2} - real:{3}", type, length, msg.Length, receiveNumber);
                        string content = Encoding.UTF8.GetString(msg, 0, length);
                        Console.WriteLine("[Receive:{0}]{1}-{2}", serverSocket.RemoteEndPoint.ToString(), type, content);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    serverSocket.Shutdown(SocketShutdown.Both);
                    serverSocket.Close();
                    break;
                }
            }
        }

        public static JsonData RandomTable()
        {
            HashSet<string> hash = new HashSet<string>();
            JsonData table = new JsonData();
            int length = random.Next(3, 10);
            for (int i = 0; i < length; i++)
            {
                var k = RandomValue();
                if (!hash.Contains(k))
                {
                    hash.Add(k);
                    switch (random.Next(0, 4))
                    {
                        case 0:
                            table[k] = new JsonData(random.Next(-100, 100));
                            break;
                        case 1:
                            table[k] = new JsonData(random.Next(-100, 100) > 0);
                            break;
                        case 2:
                            table[k] = new JsonData(random.NextDouble());
                            break;
                        case 3:
                            table[k] = new JsonData(RandomValue());
                            break;
                    }
                }
            }
            return table;
        }
        public static string RandomValue()
        {
            string field = "";
            int length = random.Next(1, 6);
            for (int i = 0; i < length; i++)
                field += Convert.ToChar(random.Next(65, 91));
            return field;
        }
    }
}

