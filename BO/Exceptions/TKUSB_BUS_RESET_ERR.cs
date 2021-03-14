using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_BUS_RESET_ERR : Exception
	{
		private const string _message = "USB Reset error.";
		private const short Code = -337;

		public TKUSB_BUS_RESET_ERR() : base(_message) { }

		public TKUSB_BUS_RESET_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
