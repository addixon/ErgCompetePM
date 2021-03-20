using PM.BO;
using PM.BO.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Communication
{
    /// <summary>
    /// A generic communication buffer
    /// </summary>
    /// <typeparam name="TBufferType">The type of values that the buffer holds</typeparam>
    public abstract class CommunicationBuffer<TBufferType> : List<TBufferType>, ICommunicationBuffer<TBufferType>
    {
        /// <inheritdoc/>
        public int Size => Count;

        /// <inheritdoc/>
        public int Length => Count;

        /// <inheritdoc/>
        public int Position { get; protected set; }

        /// <inheritdoc/>
        public int MaxSize { get; }

        /// <summary>
        /// The type of buffer
        /// </summary>
        protected abstract BufferType BufferType { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxSize">The max size of the buffer</param>
        public CommunicationBuffer(int maxSize = 121)
        {
            MaxSize = maxSize;
            Position = 0;
        }

        /// <inheritdoc/>
        public void Reset()
        {   
            Clear();
            Position = 0;
        }

        /// <inheritdoc/>
        public TBufferType Peek()
        {
            return this[Position];
        }

        /// <inheritdoc/>
        public int PositionsRemaining()
        {
            return MaxSize - Position;
        }

        /// <inheritdoc/>
        public void PrependRange(IEnumerable<TBufferType> range)
        {
            InsertRange(0, range);
            Position += range.Count();
        }

        /// <inheritdoc/>
        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            if (index < Position) 
            { 
                Position--;
            }
        }

        /// <inheritdoc/>
        public new void InsertRange(int index, IEnumerable<TBufferType> range)
        {
            base.InsertRange(index, range);
            Position += range.Count();
        }

        /// <inheritdoc/>
        public void AppendRange(IEnumerable<TBufferType> range)
        {
            base.AddRange(range);
            Position += range.Count();
        }

        /// <inheritdoc/>
        public new void Insert(int index, TBufferType value)
        {
            base.Insert(index, value);
            Position++;
        }
    }
}
