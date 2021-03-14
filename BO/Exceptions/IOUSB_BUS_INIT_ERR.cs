using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_BUS_INIT_ERR : Exception
	{
		private const string _message = "USB initialization error.";
		private const short Code = -936;

		public IOUSB_BUS_INIT_ERR() : base(_message) { }

		public IOUSB_BUS_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
