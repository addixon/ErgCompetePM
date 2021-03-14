using PM.BO.Enums;
using PM.BO.Interfaces;
using PM.BO;

namespace PM.BO.Commands
{
    public class GetUserInfoCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_USERINFO;
        public override ushort? ResponseSize => 5;
        public override bool IsProprietary => false;

        private const ushort _refreshRate = 2;

        public new string Units = "N-M-Sec";


        public GetUserInfoCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = new UserInfo
            {
                Weight = responseReader.ReadUShort(),
                Units = (UnitType) responseReader.ReadByte(),
                Age = responseReader.ReadByte(),
                Gender = (Gender) responseReader.ReadByte()
            };
        }
    }
}
