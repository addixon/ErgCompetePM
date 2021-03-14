using System;

namespace PM.BO.Exceptions
{
	public class TKCMDPR_INVALID_MSG_TYPE : Exception
	{
		private const string _message = "An invalid message was received by the Command Processor.";
		private const short Code = -120;

		public TKCMDPR_INVALID_MSG_TYPE() : base(_message) { }

		public TKCMDPR_INVALID_MSG_TYPE(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
