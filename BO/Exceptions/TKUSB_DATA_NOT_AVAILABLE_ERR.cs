using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_DATA_NOT_AVAILABLE_ERR : Exception
	{
		private const string _message = "The required USB information is not available.";
		private const short Code = -10108;

		public TKUSB_DATA_NOT_AVAILABLE_ERR() : base(_message) { }

		public TKUSB_DATA_NOT_AVAILABLE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
