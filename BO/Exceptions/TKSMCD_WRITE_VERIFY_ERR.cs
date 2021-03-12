using System;

namespace BO.Exceptions
{
	public class TKSMCD_WRITE_VERIFY_ERR : Exception
	{
		private const string _message = "Error verifying write operation to SmartCard.";
		private const short Code = -245;

		public TKSMCD_WRITE_VERIFY_ERR() : base(_message) { }

		public TKSMCD_WRITE_VERIFY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
