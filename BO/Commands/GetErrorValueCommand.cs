using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetErrorValueCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_ERRORVALUE;
        public override ushort? ResponseSize => 2;
        
        public override bool IsProprietary => true;

        private const ushort _refreshRate = 2;

        public GetErrorValueCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (ErrorValue?)responseReader.ReadUShort();
        }
    }
}
