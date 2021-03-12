using BO.Enums;
using BO.Interfaces;
using System;

namespace BO.Commands
{
    public class GetAccumulatedCaloriesCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_CALORIES;
        public override ushort? ResponseSize => 2;
        
        public override bool IsProprietary => false;

        private const ushort _refreshRate = 2;

        public new string Units = "Calories";
        public new string Resolution = "1 cal";

        public GetAccumulatedCaloriesCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (ushort?) responseReader.ReadUShort();
        }
    }
}
