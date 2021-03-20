using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface ICommunicationBuffer<TBufferType> : IList<TBufferType>
    {
        /// <summary>
        /// The size
        /// </summary>
        int Size { get; }
        
        /// <summary>
        /// The current position
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Max buffer size
        /// </summary>
        int MaxSize { get; }

        /// <summary>
        /// The positions remaining
        /// </summary>
        /// <returns>the positions remaining in the buffer</returns>
        int PositionsRemaining();

        /// <summary>
        /// Resets and clears the buffer
        /// </summary>
        void Reset();

        /// <summary>
        /// Peeks at the value in the current position
        /// </summary>
        /// <returns>The value</returns>
        TBufferType Peek();

        /// <summary>
        /// Prepends a range of values to the front of the buffer
        /// </summary>
        /// <param name="range">The range of values</param>
        void PrependRange(IEnumerable<TBufferType> range);

        /// <summary>
        /// Removes the element at the specified index
        /// </summary>
        /// <param name="index">The index</param>
        new void RemoveAt(int index);

        /// <summary>
        /// Inserts a range of values at the specified index
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="range">The range of values</param>
        void InsertRange(int index, IEnumerable<TBufferType> range);

        /// <summary>
        /// Appends a range of values to the end of the buffer
        /// </summary>
        /// <param name="range">The range of values</param>
        void AppendRange(IEnumerable<TBufferType> range);

        /// <summary>
        /// Inserts a value at the specified index
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="value">The value</param>
        new void Insert(int index, TBufferType value);
    }
}
