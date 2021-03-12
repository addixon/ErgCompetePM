using System;

namespace BO.Exceptions
{
	public class TKEXP_CF_CARDNOTREADY_ERR : Exception
	{
		private const string _message = "Compact flash card not ready.";
		private const short Code = -1003;

		public TKEXP_CF_CARDNOTREADY_ERR() : base(_message) { }

		public TKEXP_CF_CARDNOTREADY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
