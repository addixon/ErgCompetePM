using System;

namespace BO.Exceptions
{
	public class IOUSB_EP_BUSY_ERR : Exception
	{
		private const string _message = "USB endpoint busy error.";
		private const short Code = -938;

		public IOUSB_EP_BUSY_ERR() : base(_message) { }

		public IOUSB_EP_BUSY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
