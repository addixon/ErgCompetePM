using System;

namespace BO.Exceptions
{
	public class TKUSB_INIT_EPNUM_ERR : Exception
	{
		private const string _message = "Error initializing the USB endpoint.";
		private const short Code = -334;

		public TKUSB_INIT_EPNUM_ERR() : base(_message) { }

		public TKUSB_INIT_EPNUM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
