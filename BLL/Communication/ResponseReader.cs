using BO;
using BO.Interfaces;
using System;

namespace BLL.Communication
{
    public class ResponseReader : CommunicationBuffer<uint>, IResponseReader
    {
        protected override BufferType BufferType => BufferType.Read;

        public ResponseReader(ushort bufferSize = 0) : base(bufferSize)
        {
        }

        public uint ReadByte()
        {
            if (PositionsRemaining() <= 0)
            {
                throw new InvalidOperationException("Can not read byte. Position is past end of buffer.");
            }

            return Buffer[Position++];
        }

        public uint ReadUShort()
        {
            return (ReadByte() << 0) + (ReadByte() << 8);
        }

        public uint ReadUInt()
        {
            return (ReadByte() << 0) + (ReadByte() << 8) + (ReadByte() << 16) + (ReadByte() << 24);
        }

        public uint ReadBytes(int totalBytes)
        {
            uint value = 0;

            for (int i = 0; i < totalBytes; i++)
            {
                value += ReadByte() << i * 8;
            }

            return value;
        }

        public TReturnType ReadBytes<TReturnType>(int totalBytes) where TReturnType : struct
        {
            ulong value = 0;

            for (int i = 0; i < totalBytes; i++)
            {
                value += (ulong)(ReadByte() << i * 8);
            }

            return (TReturnType)(object)value;
        }
    }
}
