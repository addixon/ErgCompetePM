using PM.BO.Enums;

namespace PM.BO
{
    public abstract class ShortGetCommand : GetCommand
    {
        public override PMCommandType CommandType => PMCommandType.Short;

        public ShortGetCommand(ushort? refreshRate) : base(refreshRate)
        {

        }
    }
}
