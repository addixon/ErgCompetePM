using System;

namespace BO.Exceptions
{
	public class TKRACE_RESET_EVENT_ERR : Exception
	{
		private const string _message = "Error resetting race event object.";
		private const short Code = -10907;

		public TKRACE_RESET_EVENT_ERR() : base(_message) { }

		public TKRACE_RESET_EVENT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
