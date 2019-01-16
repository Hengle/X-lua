﻿using System;
using System.Net;

namespace Game
{
    /// <summary>
    /// 网络频道接口。
    /// </summary>
    public interface INetworkChannel
    {
        /// <summary>
        /// 获取网络频道名称。
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 获取是否已连接。
        /// </summary>
        bool Connected
        {
            get;
        }

        /// <summary>
        /// 获取网络类型。
        /// </summary>
        NetworkType NetworkType
        {
            get;
        }

        /// <summary>
        /// 获取本地终结点的 IP 地址。
        /// </summary>
        IPAddress LocalIPAddress
        {
            get;
        }

        /// <summary>
        /// 获取本地终结点的端口号。
        /// </summary>
        int LocalPort
        {
            get;
        }

        /// <summary>
        /// 获取远程终结点的 IP 地址。
        /// </summary>
        IPAddress RemoteIPAddress
        {
            get;
        }

        /// <summary>
        /// 获取远程终结点的端口号。
        /// </summary>
        int RemotePort
        {
            get;
        }

        /// <summary>
        /// 获取要发送的消息包数量。
        /// </summary>
        int SendPacketCount
        {
            get;
        }

        /// <summary>
        /// 获取累计发送的消息包数量。
        /// </summary>
        int SentPacketCount
        {
            get;
        }

        /// <summary>
        /// 获取已接收未处理的消息包数量。
        /// </summary>
        int ReceivePacketCount
        {
            get;
        }

        /// <summary>
        /// 获取累计已接收的消息包数量。
        /// </summary>
        int ReceivedPacketCount
        {
            get;
        }

        /// <summary>
        /// 获取或设置当收到消息包时是否重置心跳流逝时间。
        /// </summary>
        bool ResetHeartBeatElapseSecondsWhenReceivePacket
        {
            get;
            set;
        }

        /// <summary>
        /// 获取丢失心跳的次数。
        /// </summary>
        int MissHeartBeatCount
        {
            get;
        }

        /// <summary>
        /// 获取或设置心跳间隔时长，以秒为单位。
        /// </summary>
        float HeartBeatInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 获取心跳等待时长，以秒为单位。
        /// </summary>
        float HeartBeatElapseSeconds
        {
            get;
        }

        /// <summary>
        /// 获取或设置接收缓冲区字节数。
        /// </summary>
        int ReceiveBufferSize
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置发送缓冲区字节数。
        /// </summary>
        int SendBufferSize
        {
            get;
            set;
        }

        /// <summary>
        /// 连接到远程主机。
        /// </summary>
        /// <param name="ip">远程主机的 IP 地址。</param>
        /// <param name="port">远程主机的端口号。</param>
        void Connect(string ip, int port);

        /// <summary>
        /// 连接到远程主机。
        /// </summary>
        /// <param name="ip">远程主机的 IP 地址。</param>
        /// <param name="port">远程主机的端口号。</param>
        /// <param name="userData">用户自定义数据。</param>
        void Connect(string ip, int port, int sendBuffer, int receiveBuffer, object userData);

        /// <summary>
        /// 关闭网络频道。
        /// </summary>
        void Close();

        /// <summary>
        /// 向远程主机发送消息包。
        /// </summary>
        /// <param name="packet">要发送的消息包。</param>
        void Send(int type, byte[] msg);
    }
}
