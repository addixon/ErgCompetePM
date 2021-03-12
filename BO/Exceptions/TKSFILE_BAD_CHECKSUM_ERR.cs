using System;

namespace BO.Exceptions
{
	public class TKSFILE_BAD_CHECKSUM_ERR : Exception
	{
		private const string _message = "The S Record checksum is incorrect.";
		private const short Code = -10303;

		public TKSFILE_BAD_CHECKSUM_ERR() : base(_message) { }

		public TKSFILE_BAD_CHECKSUM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
