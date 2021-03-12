using System;

namespace BO.Exceptions
{
	public class TKCSAFE_INCORRECT_RSP_PARAMS : Exception
	{
		private const string _message = "PM3 response had unexpected number of data bytes..";
		private const short Code = -10177;

		public TKCSAFE_INCORRECT_RSP_PARAMS() : base(_message) { }

		public TKCSAFE_INCORRECT_RSP_PARAMS(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
