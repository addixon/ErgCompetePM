using System;

namespace BO.Exceptions
{
	public class CRC_TOOLKIT_ERR : Exception
	{
		private const string _message = "A CRC Toolkit error occurred.";
		private const short Code = -10500;

		public CRC_TOOLKIT_ERR() : base(_message) { }

		public CRC_TOOLKIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
