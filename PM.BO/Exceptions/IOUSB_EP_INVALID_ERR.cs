using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_EP_INVALID_ERR : Exception
	{
		private const string _message = "Invalid USB endpoint specified.";
		private const short Code = -939;

		public IOUSB_EP_INVALID_ERR() : base(_message) { }

		public IOUSB_EP_INVALID_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
