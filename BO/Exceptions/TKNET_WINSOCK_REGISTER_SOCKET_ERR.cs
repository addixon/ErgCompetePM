using System;

namespace BO.Exceptions
{
	public class TKNET_WINSOCK_REGISTER_SOCKET_ERR : Exception
	{
		private const string _message = "Error registering Winsock socket (WSAEventSelect error).";
		private const short Code = -10803;

		public TKNET_WINSOCK_REGISTER_SOCKET_ERR() : base(_message) { }

		public TKNET_WINSOCK_REGISTER_SOCKET_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
