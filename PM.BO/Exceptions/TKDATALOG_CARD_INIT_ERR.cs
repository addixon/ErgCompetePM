using System;

namespace PM.BO.Exceptions
{
	public class TKDATALOG_CARD_INIT_ERR : Exception
	{
		private const string _message = "The SmartCard could not be initialized.";
		private const short Code = -132;

		public TKDATALOG_CARD_INIT_ERR() : base(_message) { }

		public TKDATALOG_CARD_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
