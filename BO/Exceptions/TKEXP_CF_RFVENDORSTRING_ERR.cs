using System;

namespace BO.Exceptions
{
	public class TKEXP_CF_RFVENDORSTRING_ERR : Exception
	{
		private const string _message = "The RF vendor string identifier is invalid.";
		private const short Code = -1006;

		public TKEXP_CF_RFVENDORSTRING_ERR() : base(_message) { }

		public TKEXP_CF_RFVENDORSTRING_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
