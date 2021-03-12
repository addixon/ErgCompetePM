using System;

namespace BO.Exceptions
{
	public class TKDATALOG_READ_ERR : Exception
	{
		private const string _message = "The data log could not be read.";
		private const short Code = -131;

		public TKDATALOG_READ_ERR() : base(_message) { }

		public TKDATALOG_READ_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
