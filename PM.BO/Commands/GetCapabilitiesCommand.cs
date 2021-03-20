using PM.BO.Enums;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;

namespace PM.BO.Commands
{
    public class GetCapabilitiesCommand: LongGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_CAPS;
        public override ushort? ResponseSize { get; } = 0;
        
        public GetCapabilitiesCommand(byte capabilityCode) : base(null)
        {
            Data = new uint[] { capabilityCode };

            ResponseSize = capabilityCode switch
            {
                0x00 => 3,
                0x01 => 2,
                0x02 => 11,
                _ => throw new NotSupportedException("Capability Code Not Supported"),
            };
        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort size)
        {
            if (size == 3)
            {
                Value = (Capabilities) new Capabilities
                {
                    MaxRxFrame = (byte) responseReader.ReadByte(),
                    MaxTxFrame = (byte) responseReader.ReadByte(),
                    MinInterframe = (byte) responseReader.ReadByte()
                };
            }
            else if (size == 2)
            {
                Value = (Capabilities)new Capabilities();

                for (int i = 0; i < 2; i++)
                {
                    Value.Code0x01[i] = (byte) responseReader.ReadByte();
                }
            }
            else if (size == 11)
            {
                Value = (Capabilities)new Capabilities();

                for (int i = 0; i < 11; i++)
                {
                    Value.Code0x02[i] = (byte) responseReader.ReadByte();
                }
            }

            Value = Value;
        }
    }
}
