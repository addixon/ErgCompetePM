using System;

namespace BO.Exceptions
{
	public class TKTSTGEN_NO_UNUSED_FILE_ERR : Exception
	{
		private const string _message = "The Unused file could not be found..";
		private const short Code = -3000;

		public TKTSTGEN_NO_UNUSED_FILE_ERR() : base(_message) { }

		public TKTSTGEN_NO_UNUSED_FILE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
