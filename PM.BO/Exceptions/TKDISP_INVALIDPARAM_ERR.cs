using System;

namespace PM.BO.Exceptions
{
	public class TKDISP_INVALIDPARAM_ERR : Exception
	{
		private const string _message = "Invalid parameter.";
		private const short Code = -141;

		public TKDISP_INVALIDPARAM_ERR() : base(_message) { }

		public TKDISP_INVALIDPARAM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
