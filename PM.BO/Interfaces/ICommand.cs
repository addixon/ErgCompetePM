using PM.BO.Enums;
using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        byte Code { get; }

        PMCommandType CommandType { get; }

        ushort Size { get; }

        uint? Wrapper { get; }

        IEnumerable<uint>? ParentTo { get; }

        int? MaximumOccurencesInParent { get; }

        ushort? ResponseSize { get; }

        bool IsShortCommand { get; }

        bool IsLongCommand { get; }

        string? Units { get; }

        string? Resolution { get; }

        ushort TotalSize { get; }

        dynamic? Value { get; }

        uint[] GetBytes();

        void Write(ICommandWriter commandWriter);

        void Read(IResponseReader responseReader);

    }
}
