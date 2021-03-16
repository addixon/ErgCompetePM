using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_WRITE_FAILED_ERR : Exception
	{
		private const string _message = "Write operation failed.";
		private const short Code = -10702;

		public TKCOM_WRITE_FAILED_ERR() : base(_message) { }

		public TKCOM_WRITE_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
