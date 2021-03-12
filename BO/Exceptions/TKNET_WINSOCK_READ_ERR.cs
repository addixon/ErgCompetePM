using System;

namespace BO.Exceptions
{
	public class TKNET_WINSOCK_READ_ERR : Exception
	{
		private const string _message = "Read network packet time out.";
		private const short Code = -10808;

		public TKNET_WINSOCK_READ_ERR() : base(_message) { }

		public TKNET_WINSOCK_READ_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
