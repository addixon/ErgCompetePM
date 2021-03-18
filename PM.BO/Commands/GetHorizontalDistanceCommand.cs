using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetHorizontalDistanceCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_HORIZONTAL;
        public override ushort? ResponseSize => 3;
        
        

        private const ushort _refreshRate = 10;

        public new string Units = "Meters";
        public new string Resolution = "1m";

        public GetHorizontalDistanceCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (ushort?) responseReader.ReadUShort();
            responseReader.ReadByte(); // TODO: Units
        }
    }
}
