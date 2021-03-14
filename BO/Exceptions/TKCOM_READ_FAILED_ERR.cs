using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_READ_FAILED_ERR : Exception
	{
		private const string _message = "Read operation failed.";
		private const short Code = -10705;

		public TKCOM_READ_FAILED_ERR() : base(_message) { }

		public TKCOM_READ_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
