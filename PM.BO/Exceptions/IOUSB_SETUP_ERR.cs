using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_SETUP_ERR : Exception
	{
		private const string _message = "USB Setup error.";
		private const short Code = -933;

		public IOUSB_SETUP_ERR() : base(_message) { }

		public IOUSB_SETUP_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
