using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetOperationalStateCommand : ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_OPERATIONALSTATE;
        public override ushort? ResponseSize => 1;
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;
        private const ushort _refreshRate = 2;

        public GetOperationalStateCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (OperationalState?)responseReader.ReadByte();
        }
    }
}
