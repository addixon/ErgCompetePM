using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_SYNC_ERROR_NO_PM3 : Exception
	{
		private const string _message = "Unable to sync race time.  First PM3 is offline..";
		private const short Code = -10912;

		public TKRACE_SYNC_ERROR_NO_PM3() : base(_message) { }

		public TKRACE_SYNC_ERROR_NO_PM3(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
