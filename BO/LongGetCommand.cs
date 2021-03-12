namespace BO
{
    public abstract class LongGetCommand : GetCommand
    {
        public override PMCommandType CommandType => PMCommandType.Long;

        public LongGetCommand(ushort? refreshRate) : base(refreshRate)
        {

        }
    }
}
