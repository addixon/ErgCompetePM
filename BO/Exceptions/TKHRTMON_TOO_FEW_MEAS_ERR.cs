using System;

namespace BO.Exceptions
{
	public class TKHRTMON_TOO_FEW_MEAS_ERR : Exception
	{
		private const string _message = "Not enough measurements have been taken.";
		private const short Code = -181;

		public TKHRTMON_TOO_FEW_MEAS_ERR() : base(_message) { }

		public TKHRTMON_TOO_FEW_MEAS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
