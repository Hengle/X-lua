using System.IO;

namespace Game
{
    /// <summary>
    /// 网络消息包头接口。---------------可重复利用
    /// </summary>
    public class NetworkChannelHelper
    {
        public virtual int PacketHeaderLength
        {
            get { return 4; }
        }
        /// <summary>
        /// 获取网络消息包长度。
        /// </summary>
        public ushort PacketLength
        {
            get;
            private set;
        }

        /// <summary>
        /// 包协议类型
        /// </summary>
        public ushort PacketType
        {
            get;
            private set;
        }

        public NetworkChannelHelper() { }
   
        public virtual void DecodeHeader(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            PacketType = reader.ReadUInt16();
            PacketLength = reader.ReadUInt16();
        }
        /// <summary>
        /// 发送数据到Lua层解析数据
        /// </summary>
        public virtual void DecodeBody()
        {

        }
    }
}
