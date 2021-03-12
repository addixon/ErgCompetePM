using System;

namespace BO.Exceptions
{
	public class TKDATALOG_RECORDIDENTIFIER_ERR : Exception
	{
		private const string _message = "Data log record identifer error.";
		private const short Code = -135;

		public TKDATALOG_RECORDIDENTIFIER_ERR() : base(_message) { }

		public TKDATALOG_RECORDIDENTIFIER_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
