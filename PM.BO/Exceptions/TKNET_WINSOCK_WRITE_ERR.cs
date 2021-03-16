using System;

namespace PM.BO.Exceptions
{
	public class TKNET_WINSOCK_WRITE_ERR : Exception
	{
		private const string _message = "Error writing network packet (sendto error).";
		private const short Code = -10806;

		public TKNET_WINSOCK_WRITE_ERR() : base(_message) { }

		public TKNET_WINSOCK_WRITE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
