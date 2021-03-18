using PM.BO.Enums;
using System;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the number of user id digits to accept (2-5)
    /// </summary>
    public class SetUserIdDigitsCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.IDDIGITS;

        public SetUserIdDigitsCommand(uint[] data) : base(data)
        {

        }

        public SetUserIdDigitsCommand(int digits)
        {
            if (digits < 2 || digits > 5)
            {
                throw new ArgumentException("Value must be between 2 and 5, inclusively.");
            }

            Data = BitConverter.GetBytes(digits).Reverse().Take(1).Select(b => (uint) b);
        }
    }
}
