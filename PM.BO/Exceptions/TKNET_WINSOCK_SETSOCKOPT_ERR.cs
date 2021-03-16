using System;

namespace PM.BO.Exceptions
{
	public class TKNET_WINSOCK_SETSOCKOPT_ERR : Exception
	{
		private const string _message = "Set socket option error (setsockopt).";
		private const short Code = -10814;

		public TKNET_WINSOCK_SETSOCKOPT_ERR() : base(_message) { }

		public TKNET_WINSOCK_SETSOCKOPT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
