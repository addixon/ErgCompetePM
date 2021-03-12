using System;

namespace BO.Exceptions
{
	public class TKTIME_ABORT_ERR : Exception
	{
		private const string _message = "The delay terminated prematurely due to a Windows quit message..";
		private const short Code = -10001;

		public TKTIME_ABORT_ERR() : base(_message) { }

		public TKTIME_ABORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
