using BO.Interfaces;
using System;

namespace BO
{
    public abstract class GetCommand : ICommand
    {
        public string Name { 
            get 
            {
                const string prefix = "Get";
                const string suffix = "Command";
                int minimumLength = prefix.Length + suffix.Length;

                string className = GetType().Name;

                if (!className.Contains(prefix) && !className.Contains(suffix) || className.Length <= minimumLength)
                {
                    Exception e = new InvalidOperationException("Invalid get command name");
                    throw e;
                }

                return className.Substring(prefix.Length, className.Length - minimumLength);
            } 
        }

        public abstract byte Code { get; }

        public abstract PMCommandType CommandType { get; }

        public abstract ushort? ResponseSize { get; }

        public ushort Size => (ushort)(Data != null ? (Data.Length + 1) : 1);

        public abstract bool IsProprietary { get; }

        public ushort? RefreshRate { get; }

        public string Units { get; } = string.Empty;

        public string Resolution { get; } = string.Empty;

        public ushort? Order { get; set; }

        public bool IsShortCommand => CommandType == PMCommandType.Short;

        public bool IsLongCommand => CommandType == PMCommandType.Long;

        protected abstract uint[]? Data { get; }

        public dynamic? Value { get; protected set; }

        public GetCommand(ushort? refreshRate)
        {
            RefreshRate = refreshRate;
        }

        public void Write(ICommandWriter commandWriter)
        {
            commandWriter.WriteByte(Code);

            if (Data == null)
            {
                return;
            }

            for (int i = 0; i < Data.Length; i++)
            {
                commandWriter.WriteByte(Data[i]);
            }
        }

        public void Read(IResponseReader responseReader)
        {
            byte code = (byte) responseReader.ReadByte();
            ushort size = (ushort) responseReader.ReadByte();

            if (code == Code && size == ResponseSize)
            {
                ReadImplementation(responseReader, size);
            }
            else
            {
                throw new System.Exception("Unexpected code or size");
            }
        }

        protected abstract void ReadImplementation(IResponseReader responseReader, ushort size);
    }
}