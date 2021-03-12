namespace BO
{
    public abstract class ShortSetCommand : SetCommand
    {
        public override PMCommandType CommandType => PMCommandType.Short;

        public ShortSetCommand(uint[] data) : base(data)
        {

        }
    }
}
