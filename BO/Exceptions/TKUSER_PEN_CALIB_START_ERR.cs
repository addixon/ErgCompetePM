using System;

namespace BO.Exceptions
{
	public class TKUSER_PEN_CALIB_START_ERR : Exception
	{
		private const string _message = "Pen calibration start error.";
		private const short Code = -283;

		public TKUSER_PEN_CALIB_START_ERR() : base(_message) { }

		public TKUSER_PEN_CALIB_START_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
