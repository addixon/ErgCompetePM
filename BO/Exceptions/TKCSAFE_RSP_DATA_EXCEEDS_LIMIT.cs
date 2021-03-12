using System;

namespace BO.Exceptions
{
	public class TKCSAFE_RSP_DATA_EXCEEDS_LIMIT : Exception
	{
		private const string _message = "CSAFE response data exceeded frame limit..";
		private const short Code = -10175;

		public TKCSAFE_RSP_DATA_EXCEEDS_LIMIT() : base(_message) { }

		public TKCSAFE_RSP_DATA_EXCEEDS_LIMIT(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
