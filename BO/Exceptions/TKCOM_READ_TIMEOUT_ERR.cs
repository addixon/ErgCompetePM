using System;

namespace BO.Exceptions
{
	public class TKCOM_READ_TIMEOUT_ERR : Exception
	{
		private const string _message = "A timeout occurred during a read operation.";
		private const short Code = -10707;

		public TKCOM_READ_TIMEOUT_ERR() : base(_message) { }

		public TKCOM_READ_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
