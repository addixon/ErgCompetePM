using System;

namespace PM.BO.Exceptions
{
	public class TKEXP_CF_CIRQINVALID_ERR : Exception
	{
		private const string _message = "Invalid Compact flash interrupt.";
		private const short Code = -1002;

		public TKEXP_CF_CIRQINVALID_ERR() : base(_message) { }

		public TKEXP_CF_CIRQINVALID_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
