using System;

namespace PM.BO.Exceptions
{
	public class TKTACH_TOO_FEW_MEAS_ERR : Exception
	{
		private const string _message = "Too few measurements.";
		private const short Code = -251;

		public TKTACH_TOO_FEW_MEAS_ERR() : base(_message) { }

		public TKTACH_TOO_FEW_MEAS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
