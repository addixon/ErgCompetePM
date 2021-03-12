using BO.Enums;
using BO.Interfaces;

namespace BO.Commands
{
    public class GetStatusCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_STATUS;
        public override ushort? ResponseSize => 1;
        
        public override bool IsProprietary => false;

        public GetStatusCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (byte?) responseReader.ReadByte();
        }
    }
}
