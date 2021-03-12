using System;

namespace BO.Exceptions
{
	public class IOSCI_INVALID_CNT_ERR : Exception
	{
		private const string _message = "Invalid SCI count.";
		private const short Code = -892;

		public IOSCI_INVALID_CNT_ERR() : base(_message) { }

		public IOSCI_INVALID_CNT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
