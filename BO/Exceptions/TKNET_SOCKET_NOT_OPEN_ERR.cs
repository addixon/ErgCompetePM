using System;

namespace BO.Exceptions
{
	public class TKNET_SOCKET_NOT_OPEN_ERR : Exception
	{
		private const string _message = "Socket not open.";
		private const short Code = -10813;

		public TKNET_SOCKET_NOT_OPEN_ERR() : base(_message) { }

		public TKNET_SOCKET_NOT_OPEN_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
