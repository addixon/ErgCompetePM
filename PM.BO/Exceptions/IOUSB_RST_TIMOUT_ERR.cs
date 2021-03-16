using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_RST_TIMOUT_ERR : Exception
	{
		private const string _message = "USB reset timeout error.";
		private const short Code = -930;

		public IOUSB_RST_TIMOUT_ERR() : base(_message) { }

		public IOUSB_RST_TIMOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
