using PM.BO.Enums;

namespace PM.BO
{
    public abstract class LongSetCommand : SetCommand
    {
        public override PMCommandType CommandType => PMCommandType.Long;

        public LongSetCommand() : base()
        {

        }
        public LongSetCommand(uint[] data) : base(data)
        {

        }
    }
}
