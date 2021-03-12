using System;

namespace BO.Exceptions
{
	public class IOUSB_CFG_DEV_ERR : Exception
	{
		private const string _message = "USB device configuration error.";
		private const short Code = -942;

		public IOUSB_CFG_DEV_ERR() : base(_message) { }

		public IOUSB_CFG_DEV_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
