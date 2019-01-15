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
            get { return 16; }
        }
        /// <summary>
        /// 获取网络消息包长度。
        /// 注:值为-1时,包头无效
        /// </summary>
        public int PacketLength
        {
            get;
            private set;
        }

        public NetworkChannelHelper() { }

        /// <summary>
        /// 包头编码
        /// </summary>
        /// <returns></returns>
        public virtual byte[] EncodeHeader()
        {
            Packet packet = new Packet();
            packet.WriteInt(PacketLength);
            return packet.ReadBytes();
        }
        public virtual void DecodeHeader(Stream stream)
        {
            PacketLength = stream.Length == 0 ? -1 : stream.ReadByte();
        }
        /// <summary>
        /// 发送数据到Lua层解析数据
        /// </summary>
        public virtual void DecodeBody()
        {

        }
    }
}
