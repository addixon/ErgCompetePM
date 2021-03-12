using BO;
using BO.Interfaces;
using System;

namespace BLL.Communication
{
    public abstract class CommunicationBuffer<TBufferType> : ICommunicationBuffer<TBufferType>
    {
        protected abstract BufferType BufferType { get; }

        public TBufferType[] Buffer { get; private set; }

        public ushort Position { get; protected set; }

        public ushort Size => (ushort)(Buffer?.Length ?? 0);

        public CommunicationBuffer(ushort bufferSize)
        {
            Buffer = new TBufferType[bufferSize];
            Position = 0;
        }

        public void Reset(int? bufferSize = null)
        {
            Buffer = new TBufferType[bufferSize ?? 0];
            Position = 0;
        }

        public void Resize(int bufferSize)
        {
            TBufferType[] bufferCopy = new TBufferType[bufferSize];
            int sizeToCopy = Math.Min(Buffer?.Length ?? 0, bufferSize);
            Array.Copy(Buffer ?? Array.Empty<TBufferType>(), bufferCopy, sizeToCopy);
            Buffer = bufferCopy;
            Position = 0;
        }

        protected int PositionsRemaining()
        {
            return Size - Position;
        }
    }
}
