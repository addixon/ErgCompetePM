using System;

namespace PM.BO.Exceptions
{
	public class TKSFILE_INVALID_RECORD_ERR : Exception
	{
		private const string _message = "An invalid S Record type was found.";
		private const short Code = -10305;

		public TKSFILE_INVALID_RECORD_ERR() : base(_message) { }

		public TKSFILE_INVALID_RECORD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
