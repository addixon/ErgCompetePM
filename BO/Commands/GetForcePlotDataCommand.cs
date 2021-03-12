using BO.Enums;
using BO.Interfaces;
using System.Collections.Generic;

namespace BO.Commands
{
    public class GetForcePlotDataCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_FORCEPLOTDATA;
        public override ushort? ResponseSize => 33;
        
        public override bool IsProprietary => true;

        public GetForcePlotDataCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            byte size = (byte) responseReader.ReadByte();
            List<ushort> plots = new List<ushort>();

            for (int i = 0; i < size; i += 2)
            {
                plots.Add((ushort) responseReader.ReadUShort());
            }

            Value = (ushort[]?) plots.ToArray();
        }
    }
}
