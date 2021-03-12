using System;

namespace BO.Exceptions
{
	public class TKRACE_MUTEX_FAILED_ERR : Exception
	{
		private const string _message = "Mutex failed in race DLL.";
		private const short Code = -10903;

		public TKRACE_MUTEX_FAILED_ERR() : base(_message) { }

		public TKRACE_MUTEX_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
