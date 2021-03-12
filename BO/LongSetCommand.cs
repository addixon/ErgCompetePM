namespace BO
{
    public abstract class LongSetCommand : SetCommand
    {
        public override PMCommandType CommandType => PMCommandType.Long;

        public LongSetCommand(uint[] data) : base(data)
        {

        }
    }
}
