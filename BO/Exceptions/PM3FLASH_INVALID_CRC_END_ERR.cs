using System;

namespace BO.Exceptions
{
	public class PM3FLASH_INVALID_CRC_END_ERR : Exception
	{
		private const string _message = "The end address for a CRC block is invalid.";
		private const short Code = -20101;

		public PM3FLASH_INVALID_CRC_END_ERR() : base(_message) { }

		public PM3FLASH_INVALID_CRC_END_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
