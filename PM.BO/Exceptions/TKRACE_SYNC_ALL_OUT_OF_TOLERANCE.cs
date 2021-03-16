using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_SYNC_ALL_OUT_OF_TOLERANCE : Exception
	{
		private const string _message = "Unable to sync race time.  None of the PMs are within tolerance..";
		private const short Code = -10914;

		public TKRACE_SYNC_ALL_OUT_OF_TOLERANCE() : base(_message) { }

		public TKRACE_SYNC_ALL_OUT_OF_TOLERANCE(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
