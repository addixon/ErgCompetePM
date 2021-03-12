using System;

namespace BO.Exceptions
{
	public class TKDATALOG_WRITE_ERR : Exception
	{
		private const string _message = "Cannot write to data log.";
		private const short Code = -134;

		public TKDATALOG_WRITE_ERR() : base(_message) { }

		public TKDATALOG_WRITE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
