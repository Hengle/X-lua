using System.IO;

namespace Game
{
    /// <summary>
    /// 网络消息包头接口
    /// |PacketLength|ProtocolType|ProtocolMsg
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

        public NetworkChannelHelper() { }

        public virtual void DecodeHeader(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            PacketLength = reader.ReadUInt16();
        }
        /// <summary>
        /// 打包协议
        /// </summary>
        public virtual Packet Decode(MemoryStream stream)
        {
            Packet packet = new Packet(stream.ToArray());
            return packet;
        }
    }
}
