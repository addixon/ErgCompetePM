using System;

namespace BO.Exceptions
{
	public class TKRACE_MUTEX_TIMEOUT_ERR : Exception
	{
		private const string _message = "Mutex timeout in race DLL.";
		private const short Code = -10902;

		public TKRACE_MUTEX_TIMEOUT_ERR() : base(_message) { }

		public TKRACE_MUTEX_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
