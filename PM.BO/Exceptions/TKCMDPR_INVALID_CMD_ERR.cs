using System;

namespace PM.BO.Exceptions
{
	public class TKCMDPR_INVALID_CMD_ERR : Exception
	{
		private const string _message = "The command is not recognized by the Command Processor.";
		private const short Code = -121;

		public TKCMDPR_INVALID_CMD_ERR() : base(_message) { }

		public TKCMDPR_INVALID_CMD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
