using PM.BO.Enums;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO
{
    public abstract class SetCommand : Command, ICommand
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

        public override ushort? ResponseSize => 0;

        public virtual uint? ProprietaryWrapper => null;

        public string? Units { get; }

        public string? Resolution { get; }

        public ushort? Order { get; set; }

        public bool IsShortCommand => CommandType == PMCommandType.Short;

        public bool IsLongCommand => CommandType == PMCommandType.Long;

        public dynamic? Value { get; set; }

        public SetCommand()
        {
            Data = Enumerable.Empty<uint>();
        }

        public SetCommand(uint[]? data = null)
        {
            Data = data ?? Enumerable.Empty<uint>();
        }
    }
}