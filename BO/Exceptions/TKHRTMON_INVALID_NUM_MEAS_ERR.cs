using System;

namespace BO.Exceptions
{
	public class TKHRTMON_INVALID_NUM_MEAS_ERR : Exception
	{
		private const string _message = "An invalid number of measurements was requested from the heart rate monitor.";
		private const short Code = -180;

		public TKHRTMON_INVALID_NUM_MEAS_ERR() : base(_message) { }

		public TKHRTMON_INVALID_NUM_MEAS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
