using PM.BO;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Communication
{
    /// <summary>
    /// Handles writing a command into the buffer
    /// </summary>
    public class CommandWriter : CommunicationBuffer<uint>, ICommandWriter
    {
        /// <inheritdoc />
        protected override BufferType BufferType => BufferType.Write;

        /// <inheritdoc/>
        public void WriteByte(uint value)
        {
            EnsureAvailableSpace(0);

            Add(value);
            Position++;
        }

        /// <inheritdoc/>
        public void WriteBytes(uint[] value)
        {
            EnsureAvailableSpace(value.Length);

            AddRange(value);
            Position += value.Length;
        }

        /// <inheritdoc/>
        public void WriteBytes(IEnumerable<uint> value)
        {
            int size = value.Count();
            EnsureAvailableSpace(size);

            AddRange(value);
            Position += size;
        }

        /// <summary>
        /// Ensures that space in the buffer is available
        /// </summary>
        /// <param name="spaceRequired">The space required</param>
        private void EnsureAvailableSpace(int spaceRequired)
        {
            if (PositionsRemaining() - spaceRequired <= 0)
            {
                throw new InvalidOperationException("Can not write bytes. Buffer will be exceeded.");
            }
        }
    }
}
