using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_INVALID_ADDRESS_IN_RSP_ERR : Exception
	{
		private const string _message = "CSAFE invalid address in response..";
		private const short Code = -10185;

		public TKCSAFE_INVALID_ADDRESS_IN_RSP_ERR() : base(_message) { }

		public TKCSAFE_INVALID_ADDRESS_IN_RSP_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
