using System;

namespace PM.BO.Exceptions
{
	public class SZUTIL_SYNC_ERR : Exception
	{
		private const string _message = "Cannot synchronize with SZ PM.BOotloader and switch to high baud rate.";
		private const short Code = -20001;

		public SZUTIL_SYNC_ERR() : base(_message) { }

		public SZUTIL_SYNC_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
