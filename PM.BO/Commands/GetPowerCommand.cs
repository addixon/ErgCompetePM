using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetPowerCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_POWER;
        public override ushort? ResponseSize => 3;
        
        

        public new string Units = "Watts";
        public new string Resolution = "1w";

        public GetPowerCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (ushort?) responseReader.ReadUShort();
            responseReader.ReadByte(); // TODO: Units
        }
    }
}
