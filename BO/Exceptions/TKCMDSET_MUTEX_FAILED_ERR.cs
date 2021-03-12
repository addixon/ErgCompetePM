using System;

namespace BO.Exceptions
{
	public class TKCMDSET_MUTEX_FAILED_ERR : Exception
	{
		private const string _message = "Synchronization mechanism failure.";
		private const short Code = -10203;

		public TKCMDSET_MUTEX_FAILED_ERR() : base(_message) { }

		public TKCMDSET_MUTEX_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
