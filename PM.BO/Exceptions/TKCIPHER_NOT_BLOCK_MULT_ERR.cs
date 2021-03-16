using System;

namespace PM.BO.Exceptions
{
	public class TKCIPHER_NOT_BLOCK_MULT_ERR : Exception
	{
		private const string _message = "The size of the block to decipher is not an even multiple of the minimum cipher block size.";
		private const short Code = -10400;

		public TKCIPHER_NOT_BLOCK_MULT_ERR() : base(_message) { }

		public TKCIPHER_NOT_BLOCK_MULT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
