using System;

namespace PM.BO.Exceptions
{
	public class TKEXP_CF_MEMTEST_ERR : Exception
	{
		private const string _message = "Compact flash memory test failed.";
		private const short Code = -1004;

		public TKEXP_CF_MEMTEST_ERR() : base(_message) { }

		public TKEXP_CF_MEMTEST_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
