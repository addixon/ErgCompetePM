using System;

namespace BO.Exceptions
{
	public class TKNET_WINSOCK_WRONG_EVENT_ERR : Exception
	{
		private const string _message = "Read network packet error (wrong event signalled).";
		private const short Code = -10809;

		public TKNET_WINSOCK_WRONG_EVENT_ERR() : base(_message) { }

		public TKNET_WINSOCK_WRONG_EVENT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
