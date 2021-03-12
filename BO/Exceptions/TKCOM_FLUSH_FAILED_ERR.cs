using System;

namespace BO.Exceptions
{
	public class TKCOM_FLUSH_FAILED_ERR : Exception
	{
		private const string _message = "Flush operation failed.";
		private const short Code = -10710;

		public TKCOM_FLUSH_FAILED_ERR() : base(_message) { }

		public TKCOM_FLUSH_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
