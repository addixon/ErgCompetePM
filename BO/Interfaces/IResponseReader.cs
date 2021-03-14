namespace PM.BO.Interfaces
{
    public interface IResponseReader : ICommunicationBuffer<uint>
    {
        uint ReadByte();

        uint ReadUShort();

        uint ReadUInt();

        uint ReadBytes(int totalBytes);

        TReturnType ReadBytes<TReturnType>(int totalBytes) where TReturnType : struct;

        void Truncate(int index);
    }
}
