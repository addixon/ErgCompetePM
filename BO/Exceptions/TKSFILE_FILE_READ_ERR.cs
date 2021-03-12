using System;

namespace BO.Exceptions
{
	public class TKSFILE_FILE_READ_ERR : Exception
	{
		private const string _message = "Cannot read data in S Record file.";
		private const short Code = -10301;

		public TKSFILE_FILE_READ_ERR() : base(_message) { }

		public TKSFILE_FILE_READ_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
