using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_NO_FEATURE_REPORT_ERR : Exception
	{
		private const string _message = "No USB feature report is available.";
		private const short Code = -338;

		public TKUSB_NO_FEATURE_REPORT_ERR() : base(_message) { }

		public TKUSB_NO_FEATURE_REPORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
