using System;

namespace BO.Exceptions
{
	public class IOADCONV_INVALID_CHAN_ERR : Exception
	{
		private const string _message = "An invalid A to D Converter channel was specified.";
		private const short Code = -812;

		public IOADCONV_INVALID_CHAN_ERR() : base(_message) { }

		public IOADCONV_INVALID_CHAN_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
