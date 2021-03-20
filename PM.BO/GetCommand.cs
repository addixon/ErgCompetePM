using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO
{
    public abstract class GetCommand : Command
    {
        public override string Name { 
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

        public ushort? RefreshRate { get; }


        public override ushort TotalSize => (ushort)(Size + 1);

        public GetCommand(ushort? refreshRate)
        {
            RefreshRate = refreshRate;
        }
    }
}