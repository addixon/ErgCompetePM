using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetErrorCodeCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_ERRORCODE;
        public override ushort? ResponseSize => 3;
        
        

        public GetErrorCodeCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (uint?)responseReader.ReadBytes(3);
        }
    }
}
