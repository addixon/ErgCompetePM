using PM.BO;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL.Communication
{
    /// <summary>
    /// A response reader
    /// </summary>
    public class ResponseReader : CommunicationBuffer<uint>, IResponseReader
    {
        /// <inheritdoc />
        protected override BufferType BufferType => BufferType.Read;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bytes">The bytes to read into the buffer</param>
        public ResponseReader(IEnumerable<uint> bytes) : base()
        {
            AddRange(bytes);
        }
        
        /// <inheritdoc />
        public uint ReadByte()
        {
            if (PositionsRemaining() <= 0)
            {
                throw new InvalidOperationException("Can not read byte. Position is past end of buffer.");
            }

            return this[Position++];
        }

        /// <inheritdoc />
        public uint ReadUShort()
        {
            return (ReadByte() << 0) + (ReadByte() << 8);
        }

        /// <inheritdoc />
        public uint ReadUInt()
        {
            return (ReadByte() << 0) + (ReadByte() << 8) + (ReadByte() << 16) + (ReadByte() << 24);
        }

        /// <inheritdoc />
        public uint ReadBytes(int totalBytes)
        {
            uint value = 0;

            for (int i = 0; i < totalBytes; i++)
            {
                value += ReadByte() << i * 8;
            }

            return value;
        }

        /// <inheritdoc />
        public TReturnType ReadBytes<TReturnType>(int totalBytes) where TReturnType : struct
        {
            ulong value = 0;

            for (int i = 0; i < totalBytes; i++)
            {
                value += ReadByte() << i * 8;
            }

            return (TReturnType)(object)value;
        }

        /// <inheritdoc />
        public void Truncate(int index)
        {
            if (index > Count)
            {
                throw new IndexOutOfRangeException("Index specified to truncate was higher than Count");
            }

            RemoveRange(index, Count - index);
        }
    }
}
