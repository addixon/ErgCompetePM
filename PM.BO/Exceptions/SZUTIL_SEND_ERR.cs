using System;

namespace PM.BO.Exceptions
{
	public class SZUTIL_SEND_ERR : Exception
	{
		private const string _message = "Could not send a B Record to the SZ.";
		private const short Code = -20000;

		public SZUTIL_SEND_ERR() : base(_message) { }

		public SZUTIL_SEND_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
