using System;

namespace BO.Exceptions
{
	public class TKTACH_INVALID_NUM_MEAS_ERR : Exception
	{
		private const string _message = "Invalid number of measurements.";
		private const short Code = -250;

		public TKTACH_INVALID_NUM_MEAS_ERR() : base(_message) { }

		public TKTACH_INVALID_NUM_MEAS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
