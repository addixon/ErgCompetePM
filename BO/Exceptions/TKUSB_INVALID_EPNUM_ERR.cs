using System;

namespace BO.Exceptions
{
	public class TKUSB_INVALID_EPNUM_ERR : Exception
	{
		private const string _message = "Invalid endpoint number.";
		private const short Code = -331;

		public TKUSB_INVALID_EPNUM_ERR() : base(_message) { }

		public TKUSB_INVALID_EPNUM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
