using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_LOGCARD_INVALID : Exception
	{
		private const string _message = "Invalid Log Card type..";
		private const short Code = -10180;

		public TKCSAFE_LOGCARD_INVALID() : base(_message) { }

		public TKCSAFE_LOGCARD_INVALID(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
