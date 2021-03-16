using System;

namespace PM.BO.Exceptions
{
	public class TKSMCD_ACK_ERR : Exception
	{
		private const string _message = "SmartCard acknowledge error.";
		private const short Code = -240;

		public TKSMCD_ACK_ERR() : base(_message) { }

		public TKSMCD_ACK_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
