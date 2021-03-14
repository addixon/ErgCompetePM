using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_LOGCARD_INVALID_ADDRESS : Exception
	{
		private const string _message = "Invalid Log Card address..";
		private const short Code = -10179;

		public TKCSAFE_LOGCARD_INVALID_ADDRESS() : base(_message) { }

		public TKCSAFE_LOGCARD_INVALID_ADDRESS(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
