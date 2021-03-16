using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_INCORRECT_CMD_PARAMS : Exception
	{
		private const string _message = "PM3 command has not enough parameters..";
		private const short Code = -10176;

		public TKCSAFE_INCORRECT_CMD_PARAMS() : base(_message) { }

		public TKCSAFE_INCORRECT_CMD_PARAMS(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
