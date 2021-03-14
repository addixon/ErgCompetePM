using System;

namespace PM.BO.Exceptions
{
	public class TKSFILE_INVALID_FILE_ERR : Exception
	{
		private const string _message = "The S Record file is invalid.";
		private const short Code = -10306;

		public TKSFILE_INVALID_FILE_ERR() : base(_message) { }

		public TKSFILE_INVALID_FILE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
