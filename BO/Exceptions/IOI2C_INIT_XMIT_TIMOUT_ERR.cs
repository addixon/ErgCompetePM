using System;

namespace BO.Exceptions
{
	public class IOI2C_INIT_XMIT_TIMOUT_ERR : Exception
	{
		private const string _message = "Transmit timeout during initialization.";
		private const short Code = -842;

		public IOI2C_INIT_XMIT_TIMOUT_ERR() : base(_message) { }

		public IOI2C_INIT_XMIT_TIMOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
