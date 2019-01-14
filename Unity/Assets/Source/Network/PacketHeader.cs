using System.IO;

namespace Game
{
    /// <summary>
    /// 网络消息包头接口。---------------可重复利用
    /// </summary>
    public class PacketHeader
    {
        /// <summary>
        /// 获取网络消息包长度。
        /// </summary>
        public int PacketLength
        {
            get;
            private set;
        }

        public static PacketHeader Deserialize(Stream stream)
        {
            PacketHeader header = new PacketHeader();
            header.PacketLength = stream.ReadByte();
            return header;
        }
        //public static Stream Serialize(PacketHeader header)
        //{

        //}
    }
}
