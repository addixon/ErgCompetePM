using System;

namespace BO.Exceptions
{
	public class TKCSAFE_INVALID_ID_IN_RSP_ERR : Exception
	{
		private const string _message = "CSAFE invalid command ID in response..";
		private const short Code = -10186;

		public TKCSAFE_INVALID_ID_IN_RSP_ERR() : base(_message) { }

		public TKCSAFE_INVALID_ID_IN_RSP_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
