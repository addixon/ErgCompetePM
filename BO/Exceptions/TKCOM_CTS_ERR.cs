using System;

namespace BO.Exceptions
{
	public class TKCOM_CTS_ERR : Exception
	{
		private const string _message = "Error determining the state of CTS.";
		private const short Code = -10716;

		public TKCOM_CTS_ERR() : base(_message) { }

		public TKCOM_CTS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
