using System;

namespace PM.BO.Exceptions
{
	public class TKMEM_INVALID_START_ADDR_ERR : Exception
	{
		private const string _message = "The start address is invalid for this type of memory.";
		private const short Code = -201;

		public TKMEM_INVALID_START_ADDR_ERR() : base(_message) { }

		public TKMEM_INVALID_START_ADDR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
