using PM.BO.Enums;

namespace PM.BO.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        byte Code { get; }

        PMCommandType CommandType { get; }

        ushort Size { get; }

        ushort? ResponseSize { get; }

        uint? ProprietaryWrapper { get; }

        bool IsShortCommand { get; }

        bool IsLongCommand { get; }

        string? Units { get; }

        string? Resolution { get; }

        ushort? Order { get; set; }

        dynamic? Value { get; }

        void Write(ICommandWriter commandWriter);

        void Read(IResponseReader responseReader);

    }
}
