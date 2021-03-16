using System;

namespace PM.BO.Exceptions
{
	public class TKCMDSET_MUTEX_TIMEOUT_ERR : Exception
	{
		private const string _message = "Communication in use by another process for too long.";
		private const short Code = -10202;

		public TKCMDSET_MUTEX_TIMEOUT_ERR() : base(_message) { }

		public TKCMDSET_MUTEX_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
