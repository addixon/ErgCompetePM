namespace BO.Interfaces
{
    public interface ICommandWriter
    {
        void WriteByte(uint value);
        void WriteBytes(uint[] value);
    }
}
