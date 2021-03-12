using BO.Enums;
using BO.Interfaces;
using System;

namespace BO.Commands
{
    public class GetErrorCodeCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_ERRORCODE;
        public override ushort? ResponseSize => 3;
        
        public override bool IsProprietary => false;

        public GetErrorCodeCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (uint?)responseReader.ReadBytes(3);
        }
    }
}
