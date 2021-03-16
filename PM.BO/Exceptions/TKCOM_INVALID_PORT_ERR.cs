using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_INVALID_PORT_ERR : Exception
	{
		private const string _message = "The identifier for the port is invalid.";
		private const short Code = -10700;

		public TKCOM_INVALID_PORT_ERR() : base(_message) { }

		public TKCOM_INVALID_PORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
