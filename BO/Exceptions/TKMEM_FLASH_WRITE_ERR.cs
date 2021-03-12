using System;

namespace BO.Exceptions
{
	public class TKMEM_FLASH_WRITE_ERR : Exception
	{
		private const string _message = "The FLASH memory could not be written.";
		private const short Code = -203;

		public TKMEM_FLASH_WRITE_ERR() : base(_message) { }

		public TKMEM_FLASH_WRITE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
