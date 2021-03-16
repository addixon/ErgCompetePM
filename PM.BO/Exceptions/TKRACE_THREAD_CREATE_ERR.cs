using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_THREAD_CREATE_ERR : Exception
	{
		private const string _message = "Error creating communications thread.";
		private const short Code = -10900;

		public TKRACE_THREAD_CREATE_ERR() : base(_message) { }

		public TKRACE_THREAD_CREATE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
