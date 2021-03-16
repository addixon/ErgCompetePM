using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_MUTEX_CREATE_ERR : Exception
	{
		private const string _message = "Error creating race DLL mutex.";
		private const short Code = -10901;

		public TKRACE_MUTEX_CREATE_ERR() : base(_message) { }

		public TKRACE_MUTEX_CREATE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
