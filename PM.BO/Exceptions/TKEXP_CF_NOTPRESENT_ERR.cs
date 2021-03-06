using System;

namespace PM.BO.Exceptions
{
	public class TKEXP_CF_NOTPRESENT_ERR : Exception
	{
		private const string _message = "Compact flash card not present.";
		private const short Code = -1001;

		public TKEXP_CF_NOTPRESENT_ERR() : base(_message) { }

		public TKEXP_CF_NOTPRESENT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
