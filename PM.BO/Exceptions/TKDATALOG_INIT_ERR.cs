using System;

namespace PM.BO.Exceptions
{
	public class TKDATALOG_INIT_ERR : Exception
	{
		private const string _message = "The data logging toolkit could not initialize.";
		private const short Code = -130;

		public TKDATALOG_INIT_ERR() : base(_message) { }

		public TKDATALOG_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
