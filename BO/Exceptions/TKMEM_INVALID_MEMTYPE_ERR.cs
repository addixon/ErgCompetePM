using System;

namespace PM.BO.Exceptions
{
	public class TKMEM_INVALID_MEMTYPE_ERR : Exception
	{
		private const string _message = "An invalid type of memory was specified.";
		private const short Code = -200;

		public TKMEM_INVALID_MEMTYPE_ERR() : base(_message) { }

		public TKMEM_INVALID_MEMTYPE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
