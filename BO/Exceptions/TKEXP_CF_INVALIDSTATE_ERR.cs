using System;

namespace BO.Exceptions
{
	public class TKEXP_CF_INVALIDSTATE_ERR : Exception
	{
		private const string _message = "The Compact flash is in an invalid state.";
		private const short Code = -1005;

		public TKEXP_CF_INVALIDSTATE_ERR() : base(_message) { }

		public TKEXP_CF_INVALIDSTATE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
