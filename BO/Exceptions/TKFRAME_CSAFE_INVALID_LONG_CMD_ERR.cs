using System;

namespace PM.BO.Exceptions
{
	public class TKFRAME_CSAFE_INVALID_LONG_CMD_ERR : Exception
	{
		private const string _message = "An invalid CSAFE long command was received.";
		private const short Code = -165;

		public TKFRAME_CSAFE_INVALID_LONG_CMD_ERR() : base(_message) { }

		public TKFRAME_CSAFE_INVALID_LONG_CMD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
