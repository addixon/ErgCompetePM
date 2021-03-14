using PM.BO;
using PM.BO.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Communication
{
    public abstract class CommunicationBuffer<TBufferType> : List<TBufferType>, ICommunicationBuffer<TBufferType>
    {
        public int Size => Count;
        public int Length => Count;
        public int Position { get; protected set; }

        protected int MaxSize { get; }
        protected abstract BufferType BufferType { get; }

        public CommunicationBuffer(int maxSize = 96)
        {
            MaxSize = maxSize;
            Position = 0;
        }

        public void Reset()
        {   
            Clear();
            Position = 0;
        }

        public TBufferType Peek()
        {
            return this[Position];
        }

        protected int PositionsRemaining()
        {
            return MaxSize - Position;
        }

        public void PrependRange(IEnumerable<TBufferType> range)
        {
            InsertRange(0, range);
            Position += range.Count();
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            Position--;
        }
        
        public new void InsertRange(int index, IEnumerable<TBufferType> range)
        {
            base.InsertRange(index, range);
            Position += range.Count();
        }

        public void AppendRange(IEnumerable<TBufferType> range)
        {
            base.AddRange(range);
            Position += range.Count();
        }

        public new void Insert(int index, TBufferType value)
        {
            base.Insert(index, value);
            Position++;
        }
    }
}
