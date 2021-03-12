using BO;
using BO.Interfaces;
using System;

namespace BLL.Communication
{
    public class CommandWriter : CommunicationBuffer<uint>, ICommandWriter
    {
        protected override BufferType BufferType => BufferType.Write;

        public CommandWriter(ushort bufferSize) : base(bufferSize)
        {

        }

        public void WriteByte(uint value)
        {
            if (PositionsRemaining() <= 0)
            {
                throw new InvalidOperationException("Can not write byte. Buffer has been exceeded.");
            }

            Buffer[Position++] = value;
        }

        public void WriteBytes(uint[] value)
        {
            if (PositionsRemaining() - value.Length <= 0)
            {
                throw new InvalidOperationException("Can not write bytes. Buffer will be exceeded.");
            }

            for (int i = 0; i < value.Length; i++)
            {
                Buffer[Position++] = value[i];
            }
        }
    }
}
