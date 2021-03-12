using System;

namespace BO.Exceptions
{
	public class TKCOM_RTS_ERR : Exception
	{
		private const string _message = "Error controlling the RTS signal.";
		private const short Code = -10714;

		public TKCOM_RTS_ERR() : base(_message) { }

		public TKCOM_RTS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
