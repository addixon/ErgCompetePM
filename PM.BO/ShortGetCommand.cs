using PM.BO.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO
{
    public abstract class ShortGetCommand : GetCommand
    {
        protected override IEnumerable<uint>? Data => Enumerable.Empty<uint>();
        public override PMCommandType CommandType => PMCommandType.Short;

        public ShortGetCommand(ushort? refreshRate) : base(refreshRate)
        {

        }
    }
}
