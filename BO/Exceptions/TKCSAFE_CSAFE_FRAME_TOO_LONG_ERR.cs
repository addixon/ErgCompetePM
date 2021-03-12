using System;

namespace BO.Exceptions
{
	public class TKCSAFE_CSAFE_FRAME_TOO_LONG_ERR : Exception
	{
		private const string _message = "CSAFE command frame too long..";
		private const short Code = -10184;

		public TKCSAFE_CSAFE_FRAME_TOO_LONG_ERR() : base(_message) { }

		public TKCSAFE_CSAFE_FRAME_TOO_LONG_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
