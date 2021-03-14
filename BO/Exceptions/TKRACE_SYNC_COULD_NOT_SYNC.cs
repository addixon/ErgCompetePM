using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_SYNC_COULD_NOT_SYNC : Exception
	{
		private const string _message = "Unable to sync race time.  One or more PMs out of tolerance..";
		private const short Code = -10915;

		public TKRACE_SYNC_COULD_NOT_SYNC() : base(_message) { }

		public TKRACE_SYNC_COULD_NOT_SYNC(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
