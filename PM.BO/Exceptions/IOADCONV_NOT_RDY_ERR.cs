using System;

namespace PM.BO.Exceptions
{
	public class IOADCONV_NOT_RDY_ERR : Exception
	{
		private const string _message = "A to D Converter not ready.";
		private const short Code = -813;

		public IOADCONV_NOT_RDY_ERR() : base(_message) { }

		public IOADCONV_NOT_RDY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
