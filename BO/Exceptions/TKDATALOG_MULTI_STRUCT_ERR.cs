using System;

namespace BO.Exceptions
{
	public class TKDATALOG_MULTI_STRUCT_ERR : Exception
	{
		private const string _message = "Mulitple structure error.";
		private const short Code = -133;

		public TKDATALOG_MULTI_STRUCT_ERR() : base(_message) { }

		public TKDATALOG_MULTI_STRUCT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
