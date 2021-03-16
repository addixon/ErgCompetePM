using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_DSR_ERR : Exception
	{
		private const string _message = "Error determining the state of DSR.";
		private const short Code = -10715;

		public TKCOM_DSR_ERR() : base(_message) { }

		public TKCOM_DSR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
