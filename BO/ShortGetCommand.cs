using System;

namespace BO
{
    public abstract class ShortGetCommand : GetCommand
    {
        protected override uint[]? Data => Array.Empty<uint>();
        public override PMCommandType CommandType => PMCommandType.Short;

        public ShortGetCommand(ushort? refreshRate) : base(refreshRate)
        {

        }
    }
}
