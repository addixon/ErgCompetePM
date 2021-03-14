using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_INVALID_DEVICE_NAME_ERR : Exception
	{
		private const string _message = "A device with the specified name cannot be located.";
		private const short Code = -10701;

		public TKCOM_INVALID_DEVICE_NAME_ERR() : base(_message) { }

		public TKCOM_INVALID_DEVICE_NAME_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
