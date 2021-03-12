using System;

namespace BO.Exceptions
{
	public class IOHDW_MEM_INVALID_CS_ERR : Exception
	{
		private const string _message = "Invalid memory chip select.";
		private const short Code = -830;

		public IOHDW_MEM_INVALID_CS_ERR() : base(_message) { }

		public IOHDW_MEM_INVALID_CS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
