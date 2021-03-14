using System;

namespace PM.BO.Exceptions
{
	public class TKLOGCARD_FILE_OPEN_ERROR : Exception
	{
		private const string _message = "Error opening LogCard Output file. Make sure file is not open in another application..";
		private const short Code = -10195;

		public TKLOGCARD_FILE_OPEN_ERROR() : base(_message) { }

		public TKLOGCARD_FILE_OPEN_ERROR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
