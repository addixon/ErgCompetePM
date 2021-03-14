using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_WAIT_OBJECT_ERR : Exception
	{
		private const string _message = "Race communications thread - wait on event failed.";
		private const short Code = -10904;

		public TKRACE_WAIT_OBJECT_ERR() : base(_message) { }

		public TKRACE_WAIT_OBJECT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
