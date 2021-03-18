using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetHardwareSerialNumberCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_SERIAL;
        public override ushort? ResponseSize => 9;
        
        

        public GetHardwareSerialNumberCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            byte[] serialBytes = new byte[9];

            for (int i = 0; i < 9; i++)
            {
                serialBytes[i] = (byte) responseReader.ReadByte();
            }

            Value = (string?) System.Text.Encoding.ASCII.GetString(serialBytes);
        }
    }
}
