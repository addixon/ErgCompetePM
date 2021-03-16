using System;

namespace PM.BO.Exceptions
{
	public class IOLCD_INVALIDPARAM_ERR : Exception
	{
		private const string _message = "Invalid parameter.";
		private const short Code = -861;

		public IOLCD_INVALIDPARAM_ERR() : base(_message) { }

		public IOLCD_INVALIDPARAM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
