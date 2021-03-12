using BO.Interfaces;
using System;
using System.Linq;

namespace BO
{
    public abstract class SetCommand : ICommand
    {
        public string Name
        {
            get
            {
                const string prefix = "Set";
                const string suffix = "Command";
                int minimumLength = prefix.Length + suffix.Length;

                string className = GetType().Name;

                if (!className.Contains(prefix) && !className.Contains(suffix) || className.Length <= minimumLength)
                {
                    Exception e = new InvalidOperationException("Invalid set command name");
                    throw e;
                }

                return className.Substring(prefix.Length, className.Length - minimumLength);
            }
        }

        protected static uint[] EmptyByteArray => Enumerable.Empty<uint>().ToArray();

        public abstract byte Code { get; }

        public abstract PMCommandType CommandType { get; }

        public ushort? ResponseSize => 0;

        public abstract ushort Size { get; }

        public abstract bool IsProprietary { get; }

        private uint[]? Data { get; }

        public string? Units { get; }

        public string? Resolution { get; }

        public ushort? Order { get; set; }

        public bool IsShortCommand => CommandType == PMCommandType.Short;

        public bool IsLongCommand => CommandType == PMCommandType.Long;

        public dynamic Value => throw new InvalidOperationException("Set commands do not have a value.");

        public SetCommand(uint[] data)
        {
            Data = data;
        }

        public void Write(ICommandWriter commandWriter)
        {
            commandWriter.WriteByte(Code);

            if (Data != null) 
            { 
                commandWriter.WriteBytes(Data);
            }
        }

        public void Read(IResponseReader responseReader)
        {
            throw new NotSupportedException("Read is not supported on Set commands");
        }
    }
}