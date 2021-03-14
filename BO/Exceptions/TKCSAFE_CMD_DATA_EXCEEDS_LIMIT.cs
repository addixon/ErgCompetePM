using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_CMD_DATA_EXCEEDS_LIMIT : Exception
	{
		private const string _message = "CSAFE command data exceeds frame limit..";
		private const short Code = -10174;

		public TKCSAFE_CMD_DATA_EXCEEDS_LIMIT() : base(_message) { }

		public TKCSAFE_CMD_DATA_EXCEEDS_LIMIT(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
