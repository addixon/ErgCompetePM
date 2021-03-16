using System;

namespace PM.BO.Exceptions
{
	public class TKCMDSET_DEVICE_NOT_FOUND_ERR : Exception
	{
		private const string _message = "A USB connection could not be established with the PM3..";
		private const short Code = -10201;

		public TKCMDSET_DEVICE_NOT_FOUND_ERR() : base(_message) { }

		public TKCMDSET_DEVICE_NOT_FOUND_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
