using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetDragFactorCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_DRAGFACTOR;
        public override ushort? ResponseSize => 1;
        
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;

        private const ushort _refreshRate = 2;

        public new string Units = "N-M-Sec";


        public GetDragFactorCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (byte?) responseReader.ReadByte();
        }
    }
}
