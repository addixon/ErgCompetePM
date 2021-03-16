using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_SYNC_ALL_DID_NOT_RESPOND : Exception
	{
		private const string _message = "Unable to sync race time.  Could not get at least one PM within tolerance on initial broadcast..";
		private const short Code = -10913;

		public TKRACE_SYNC_ALL_DID_NOT_RESPOND() : base(_message) { }

		public TKRACE_SYNC_ALL_DID_NOT_RESPOND(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
