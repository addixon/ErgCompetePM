using System;

namespace PM.BO.Exceptions
{
	public class TKNET_INVALID_PORT_ERR : Exception
	{
		private const string _message = "Invalid IP address in command.";
		private const short Code = -10812;

		public TKNET_INVALID_PORT_ERR() : base(_message) { }

		public TKNET_INVALID_PORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
