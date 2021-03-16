using System;

namespace PM.BO.Exceptions
{
	public class TKNET_INCORRECT_PORT_IN_RSP_ERR : Exception
	{
		private const string _message = "Incorrect port in response packet.";
		private const short Code = -10810;

		public TKNET_INCORRECT_PORT_IN_RSP_ERR() : base(_message) { }

		public TKNET_INCORRECT_PORT_IN_RSP_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
