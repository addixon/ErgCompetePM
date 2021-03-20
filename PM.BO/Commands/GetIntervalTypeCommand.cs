using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetIntervalTypeCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_INTERVALTYPE;
        public override ushort? ResponseSize => 1;
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;

        public GetIntervalTypeCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (IntervalType?)responseReader.ReadByte();
        }
    }
}
