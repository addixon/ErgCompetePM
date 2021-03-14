using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface ICommunicationBuffer<TBufferType> : IList<TBufferType>
    {
        int Size { get; }
        int Position { get; }
        void Reset();
    }
}
