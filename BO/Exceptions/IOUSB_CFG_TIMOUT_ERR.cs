using System;

namespace BO.Exceptions
{
	public class IOUSB_CFG_TIMOUT_ERR : Exception
	{
		private const string _message = "USB configuration timeout error.";
		private const short Code = -931;

		public IOUSB_CFG_TIMOUT_ERR() : base(_message) { }

		public IOUSB_CFG_TIMOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
