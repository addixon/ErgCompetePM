using PM.BO;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Communication
{
    public class CommandWriter : CommunicationBuffer<uint>, ICommandWriter
    {
        protected override BufferType BufferType => BufferType.Write;

        public CommandWriter() : base()
        {

        }

        public void WriteByte(uint value)
        {
            EnsureAvailableSpace(0);

            Add(value);
            Position++;
        }

        public void WriteBytes(uint[] value)
        {
            EnsureAvailableSpace(value.Length);

            AddRange(value);
            Position += value.Length;
        }

        public void WriteBytes(IEnumerable<uint> value)
        {
            int size = value.Count();
            EnsureAvailableSpace(size);

            AddRange(value);
            Position += size;
        }

        private void EnsureAvailableSpace(int spaceRequired)
        {
            if (PositionsRemaining() - spaceRequired <= 0)
            {
                throw new InvalidOperationException("Can not write bytes. Buffer will be exceeded.");
            }
        }
    }
}
