using System;

namespace BO.Exceptions
{
	public class TKCMDSET_NO_ECODE_FILE_ERR : Exception
	{
		private const string _message = "The Error Code Description File is not available, full error information will not be available..";
		private const short Code = -10200;

		public TKCMDSET_NO_ECODE_FILE_ERR() : base(_message) { }

		public TKCMDSET_NO_ECODE_FILE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
