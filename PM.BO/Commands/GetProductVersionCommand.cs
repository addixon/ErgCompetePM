using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetProductVersionCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_VERSION;
        public override ushort? ResponseSize => 7;
        
        

        public GetProductVersionCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (ProductVersion?) new ProductVersion
            {
                ManufacturerId = (byte) responseReader.ReadByte(),
                CID = (byte) responseReader.ReadByte(),
                Model = (byte) responseReader.ReadByte(),
                HardwareVersion = (ushort) responseReader.ReadUShort(),
                SoftwareVersion = (ushort) responseReader.ReadUShort()
            };
        }
    }
}
