using System;

namespace BO.Exceptions
{
	public class TKSFILE_INVALID_LENGTH_ERR : Exception
	{
		private const string _message = "The S Record length is incorrect.";
		private const short Code = -10304;

		public TKSFILE_INVALID_LENGTH_ERR() : base(_message) { }

		public TKSFILE_INVALID_LENGTH_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
