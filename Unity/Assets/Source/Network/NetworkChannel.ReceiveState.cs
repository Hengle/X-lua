using System;
using System.IO;

namespace Game
{
    public partial class NetworkChannel
    {
        private sealed class ReceiveState : IDisposable
        {
            private MemoryStream m_Stream;
            private NetworkChannelHelper m_PacketHeader;
            private bool m_Disposed;

            public ReceiveState()
            {
                m_Stream = new MemoryStream(DefaultBufferLength);
                m_PacketHeader = null;
                m_Disposed = false;
            }

            public MemoryStream Stream
            {
                get
                {
                    return m_Stream;
                }
            }

            public NetworkChannelHelper PacketHeader
            {
                get
                {
                    return m_PacketHeader;
                }
            }

            public void PrepareForPacketHeader(int packetHeaderLength)
            {
                Reset(packetHeaderLength, null);
            }

            public void PrepareForPacket(NetworkChannelHelper packetHeader)
            {
                if (packetHeader == null)
                {
                    throw new Exception("Packet header is invalid.");
                }

                Reset(packetHeader.PacketLength, packetHeader);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (m_Disposed)
                {
                    return;
                }

                if (disposing)
                {
                    if (m_Stream != null)
                    {
                        m_Stream.Dispose();
                        m_Stream = null;
                    }
                }

                m_Disposed = true;
            }

            private void Reset(int targetLength, NetworkChannelHelper packetHeader)
            {
                if (targetLength < 0)
                {
                    throw new Exception("Target length is invalid.");
                }

                m_Stream.Position = 0L;
                m_Stream.SetLength(targetLength);
                m_PacketHeader = packetHeader;
            }
        }
    }
}
