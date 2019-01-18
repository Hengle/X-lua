using System;
using System.IO;

namespace Game
{
    public partial class NetworkChannel
    {
        private sealed class ReceiveState : IDisposable
        {
            private MemoryStream m_Stream;
            private bool m_Disposed;
            private bool m_HasHeader;

            public ReceiveState()
            {
                m_Stream = new MemoryStream(DefaultBufferLength);
                m_HasHeader = false;
                m_Disposed = false;
            }

            public MemoryStream Stream
            {
                get
                {
                    return m_Stream;
                }
            }

            public bool HasHeader
            {
                get
                {
                    return m_HasHeader;
                }
            }

            public void PrepareForPacketHeader(int packetHeaderLength)
            {
                m_HasHeader = true;
                Reset(packetHeaderLength);
            }

            public void PrepareForPacket(int packetBodyLength)
            {
                m_HasHeader = false; 
                Reset(packetBodyLength);
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

            private void Reset(int targetLength)
            {
                if (targetLength < 0)
                {
                    throw new Exception("Target length is invalid.");
                }

                m_Stream.Position = 0L;
                m_Stream.SetLength(targetLength);
            }
        }
    }
}
