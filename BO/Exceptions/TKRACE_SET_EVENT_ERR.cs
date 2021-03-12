using System;

namespace BO.Exceptions
{
	public class TKRACE_SET_EVENT_ERR : Exception
	{
		private const string _message = "Error signaling race event object.";
		private const short Code = -10906;

		public TKRACE_SET_EVENT_ERR() : base(_message) { }

		public TKRACE_SET_EVENT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
