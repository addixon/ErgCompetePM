using BO.Enums;
using BO.Interfaces;

namespace BO.Commands
{
    public class GetDisplayUnitsTypeCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_DISPLAYUNITS;
        public override ushort? ResponseSize => 1;
        
        public override bool IsProprietary => true;

        public GetDisplayUnitsTypeCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (DisplayUnitsType?)responseReader.ReadByte();
        }
    }
}
