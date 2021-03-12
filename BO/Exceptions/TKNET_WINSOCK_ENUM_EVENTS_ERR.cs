using System;

namespace BO.Exceptions
{
	public class TKNET_WINSOCK_ENUM_EVENTS_ERR : Exception
	{
		private const string _message = "Error discovering network events (WSAEnumNetworkEvents error).";
		private const short Code = -10804;

		public TKNET_WINSOCK_ENUM_EVENTS_ERR() : base(_message) { }

		public TKNET_WINSOCK_ENUM_EVENTS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
