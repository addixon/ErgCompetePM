using System;

namespace PM.BO.Exceptions
{
	public class TKSFILE_FILE_NOT_FOUND_ERR : Exception
	{
		private const string _message = "The S Record file cannot be found.";
		private const short Code = -10307;

		public TKSFILE_FILE_NOT_FOUND_ERR() : base(_message) { }

		public TKSFILE_FILE_NOT_FOUND_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
