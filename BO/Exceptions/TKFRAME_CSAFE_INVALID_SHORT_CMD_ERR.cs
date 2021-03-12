using System;

namespace BO.Exceptions
{
	public class TKFRAME_CSAFE_INVALID_SHORT_CMD_ERR : Exception
	{
		private const string _message = "An invalid CSAFE short command was received.";
		private const short Code = -164;

		public TKFRAME_CSAFE_INVALID_SHORT_CMD_ERR() : base(_message) { }

		public TKFRAME_CSAFE_INVALID_SHORT_CMD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
