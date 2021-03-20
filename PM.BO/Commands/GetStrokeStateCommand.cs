using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetStrokeStateCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_STROKESTATE;
        public override ushort? ResponseSize => 1;
        
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;
        private const ushort _refreshRate = 100;

        public GetStrokeStateCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (StrokeState?)responseReader.ReadByte();
        }
    }
}
