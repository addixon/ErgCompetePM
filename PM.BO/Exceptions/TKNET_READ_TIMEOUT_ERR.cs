using System;

namespace PM.BO.Exceptions
{
	public class TKNET_READ_TIMEOUT_ERR : Exception
	{
		private const string _message = "Read network packet time out.";
		private const short Code = -10807;

		public TKNET_READ_TIMEOUT_ERR() : base(_message) { }

		public TKNET_READ_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
