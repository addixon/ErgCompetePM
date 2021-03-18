using PM.BO.Enums;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO
{
    public abstract class GetCommand : Command, ICommand
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

        public virtual uint? ProprietaryWrapper => null;

        public ushort? RefreshRate { get; }

        public string Units { get; } = string.Empty;

        public string Resolution { get; } = string.Empty;

        public ushort? Order { get; set; }

        public bool IsShortCommand => CommandType == PMCommandType.Short;

        public bool IsLongCommand => CommandType == PMCommandType.Long;

        protected abstract IEnumerable<uint>? Data { get; }

        public dynamic? Value { get; protected set; }

        public GetCommand(ushort? refreshRate)
        {
            RefreshRate = refreshRate;
        }
    }
}