using System;

namespace PM.BO.Exceptions
{
	public class TKDDI_INVALID_PORT_ERR : Exception
	{
		private const string _message = "An invalid port number was specified.";
		private const short Code = -10150;

		public TKDDI_INVALID_PORT_ERR() : base(_message) { }

		public TKDDI_INVALID_PORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
