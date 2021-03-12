namespace BO.Interfaces
{
    public interface ICommunicationBuffer<TBufferType>
    {
        TBufferType[]? Buffer { get; }
        ushort Position { get; }
        ushort Size { get; }

        void Reset(int? bufferSize = null);
    }
}
