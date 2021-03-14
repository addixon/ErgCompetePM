using System;

namespace PM.BO.Exceptions
{
	public class IOADCONV_INIT_ADC_ERR : Exception
	{
		private const string _message = "A to D Converter initialization error.";
		private const short Code = -815;

		public IOADCONV_INIT_ADC_ERR() : base(_message) { }

		public IOADCONV_INIT_ADC_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
