using System;

namespace BO.Exceptions
{
	public class TKUSB_BUS_DISABLE_ERR : Exception
	{
		private const string _message = "Cannot disable the USB bus.";
		private const short Code = -336;

		public TKUSB_BUS_DISABLE_ERR() : base(_message) { }

		public TKUSB_BUS_DISABLE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
