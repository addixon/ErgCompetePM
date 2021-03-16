using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_FIFO_RD_ERR : Exception
	{
		private const string _message = "USB FIFO read error.";
		private const short Code = -934;

		public IOUSB_FIFO_RD_ERR() : base(_message) { }

		public IOUSB_FIFO_RD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
