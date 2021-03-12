using System;

namespace BO.Exceptions
{
	public class TKEEPROM_WRITE_TIMEOUT_ERR : Exception
	{
		private const string _message = "A timeout occurred during the write cycle to the EEPROM.";
		private const short Code = -154;

		public TKEEPROM_WRITE_TIMEOUT_ERR() : base(_message) { }

		public TKEEPROM_WRITE_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
