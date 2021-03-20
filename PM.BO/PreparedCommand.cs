using PM.BO.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO
{
    public record PreparedCommand
    {
        private readonly ICommand _command;

        public uint Code => _command.Code;
        public ushort Size => _command.Size;
        public ushort TotalSize { get; }
        public uint? Wrapper => _command.Wrapper;

        public IEnumerable<ICommand>? Children { get; }

        public void Write(ICommandWriter writer) => _command.Write(writer);

        public PreparedCommand(ICommand command, IEnumerable<ICommand>? children = null)
        {
            _command = command;
            Children = children;

            TotalSize = (ushort)(_command.TotalSize + (Children?.Sum(command => command.TotalSize) ?? 0));
        }

        public uint[] GetBytes()
        {
            List<uint> bytes = new();
            bytes.AddRange(_command.GetBytes());

            if (Children == null)
            {
                return bytes.ToArray();
            }

            foreach(ICommand childCommand in Children)
            {
                bytes.AddRange(childCommand.GetBytes());
            }

            return bytes.ToArray();
        }
    }
}
