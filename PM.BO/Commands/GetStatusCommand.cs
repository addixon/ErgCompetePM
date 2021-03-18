using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetStatusCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_STATUS;
        public override ushort? ResponseSize => 1;
        
        

        public GetStatusCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (byte?) responseReader.ReadByte();
        }
    }
}
