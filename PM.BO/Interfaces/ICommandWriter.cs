using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface ICommandWriter
    {
        void WriteByte(uint value);
        void WriteBytes(uint[] value);
        void WriteBytes(IEnumerable<uint> value);
    }
}
