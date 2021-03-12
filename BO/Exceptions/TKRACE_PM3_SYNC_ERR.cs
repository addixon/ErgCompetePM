using System;

namespace BO.Exceptions
{
	public class TKRACE_PM3_SYNC_ERR : Exception
	{
		private const string _message = "PM3 time sync out of tolerance..";
		private const short Code = -10911;

		public TKRACE_PM3_SYNC_ERR() : base(_message) { }

		public TKRACE_PM3_SYNC_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
