using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_WRITE_TIMEOUT_ERR : Exception
	{
		private const string _message = "A timeout occurred during a write operation.";
		private const short Code = -10704;

		public TKCOM_WRITE_TIMEOUT_ERR() : base(_message) { }

		public TKCOM_WRITE_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
