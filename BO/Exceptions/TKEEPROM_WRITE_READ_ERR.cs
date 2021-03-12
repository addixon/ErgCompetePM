using System;

namespace BO.Exceptions
{
	public class TKEEPROM_WRITE_READ_ERR : Exception
	{
		private const string _message = "A read error occurred aftr writing the EEPROM.";
		private const short Code = -155;

		public TKEEPROM_WRITE_READ_ERR() : base(_message) { }

		public TKEEPROM_WRITE_READ_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
