using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_AUTHENTICATION_FAILED : Exception
	{
		private const string _message = "PM3 authentication failed..";
		private const short Code = -10188;

		public TKCSAFE_AUTHENTICATION_FAILED() : base(_message) { }

		public TKCSAFE_AUTHENTICATION_FAILED(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
