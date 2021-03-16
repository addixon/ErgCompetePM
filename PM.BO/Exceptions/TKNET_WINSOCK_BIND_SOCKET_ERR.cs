using System;

namespace PM.BO.Exceptions
{
	public class TKNET_WINSOCK_BIND_SOCKET_ERR : Exception
	{
		private const string _message = "Error binding Winsock socket.";
		private const short Code = -10802;

		public TKNET_WINSOCK_BIND_SOCKET_ERR() : base(_message) { }

		public TKNET_WINSOCK_BIND_SOCKET_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
