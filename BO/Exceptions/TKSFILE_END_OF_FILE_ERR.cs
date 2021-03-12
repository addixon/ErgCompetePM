using System;

namespace BO.Exceptions
{
	public class TKSFILE_END_OF_FILE_ERR : Exception
	{
		private const string _message = "The end of the S Record file was found prematurely.";
		private const short Code = -10300;

		public TKSFILE_END_OF_FILE_ERR() : base(_message) { }

		public TKSFILE_END_OF_FILE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
