using System;

namespace PM.BO.Exceptions
{
	public class TKEEPROM_INIT_ERR : Exception
	{
		private const string _message = "The EEPROM toolkit could not initialize.";
		private const short Code = -150;

		public TKEEPROM_INIT_ERR() : base(_message) { }

		public TKEEPROM_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
