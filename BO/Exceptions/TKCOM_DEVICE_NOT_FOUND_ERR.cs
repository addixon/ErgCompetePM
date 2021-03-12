using System;

namespace BO.Exceptions
{
	public class TKCOM_DEVICE_NOT_FOUND_ERR : Exception
	{
		private const string _message = "The specified serial port cannot be located.";
		private const short Code = -10712;

		public TKCOM_DEVICE_NOT_FOUND_ERR() : base(_message) { }

		public TKCOM_DEVICE_NOT_FOUND_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
