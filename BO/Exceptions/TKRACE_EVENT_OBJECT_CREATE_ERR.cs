using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_EVENT_OBJECT_CREATE_ERR : Exception
	{
		private const string _message = "Error creating race event object.";
		private const short Code = -10905;

		public TKRACE_EVENT_OBJECT_CREATE_ERR() : base(_message) { }

		public TKRACE_EVENT_OBJECT_CREATE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
