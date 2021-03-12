using BO.Enums;
using BO.Interfaces;

namespace BO.Commands
{
    public class GetWorkoutStateCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_WORKOUTSTATE;
        public override ushort? ResponseSize => 1;
        
        public override bool IsProprietary => true;
        private const ushort _refreshRate = 2;

        public GetWorkoutStateCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (WorkoutState?)responseReader.ReadByte();
        }
    }
}
