using System;

namespace PM.BO.Exceptions
{
	public class TKEEPROM_STOP_ERR : Exception
	{
		private const string _message = "I2C Stop error communicating with EEPROM.";
		private const short Code = -152;

		public TKEEPROM_STOP_ERR() : base(_message) { }

		public TKEEPROM_STOP_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
