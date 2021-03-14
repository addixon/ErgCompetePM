using System;

namespace PM.BO.Exceptions
{
	public class IOADCONV_INVALID_REF_ERR : Exception
	{
		private const string _message = "The reference for the A to D Converter is invalid.";
		private const short Code = -814;

		public IOADCONV_INVALID_REF_ERR() : base(_message) { }

		public IOADCONV_INVALID_REF_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
