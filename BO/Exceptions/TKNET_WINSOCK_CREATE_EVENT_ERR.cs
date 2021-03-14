using System;

namespace PM.BO.Exceptions
{
	public class TKNET_WINSOCK_CREATE_EVENT_ERR : Exception
	{
		private const string _message = "Error creating event object (WSACreateEvent error).";
		private const short Code = -10805;

		public TKNET_WINSOCK_CREATE_EVENT_ERR() : base(_message) { }

		public TKNET_WINSOCK_CREATE_EVENT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
