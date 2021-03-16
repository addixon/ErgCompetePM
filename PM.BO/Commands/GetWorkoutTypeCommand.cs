using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetWorkoutTypeCommand : ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_WORKOUTTYPE;
        public override ushort? ResponseSize => 1;
        
        public override bool IsProprietary => true;
        private const ushort _refreshRate = 2;


        public GetWorkoutTypeCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (WorkoutType?)responseReader.ReadByte();
        }
    }
}
