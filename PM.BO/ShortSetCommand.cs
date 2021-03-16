using PM.BO.Enums;

namespace PM.BO
{
    public abstract class ShortSetCommand : SetCommand
    {
        public override PMCommandType CommandType => PMCommandType.Short;

        public ShortSetCommand() : base()
        {

        }

        public ShortSetCommand(uint[] data) : base(data)
        {

        }
    }
}
