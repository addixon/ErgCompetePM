using PM.BO.Interfaces;
using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    [Obsolete("Not supported by Performance Monitors")]
    public class GetDisplayTypeCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_DISPLAYTYPE;
        public override ushort? ResponseSize => 1;
        
        public override bool IsProprietary => true;

        public GetDisplayTypeCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (DisplayType?) responseReader.ReadByte();
        }
    }
}
