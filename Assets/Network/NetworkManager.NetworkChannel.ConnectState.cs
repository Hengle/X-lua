﻿

using System.Net.Sockets;

namespace GameFramework.Network
{
    internal partial class NetworkManager
    {
        private partial class NetworkChannel
        {
            private sealed class ConnectState
            {
                private readonly Socket m_Socket;
                private readonly object m_UserData;

                public ConnectState(Socket socket, object userData)
                {
                    m_Socket = socket;
                    m_UserData = userData;
                }

                public Socket Socket
                {
                    get
                    {
                        return m_Socket;
                    }
                }

                public object UserData
                {
                    get
                    {
                        return m_UserData;
                    }
                }
            }
        }
    }
}
