using System;

namespace PM.BO.Exceptions
{
	public class TKSMCD_WRITE_TIMEOUT_ERR : Exception
	{
		private const string _message = "Timeout during a write operation to the SmartCard.";
		private const short Code = -243;

		public TKSMCD_WRITE_TIMEOUT_ERR() : base(_message) { }

		public TKSMCD_WRITE_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
