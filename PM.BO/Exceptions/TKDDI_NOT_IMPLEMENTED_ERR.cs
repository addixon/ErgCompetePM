using System;

namespace PM.BO.Exceptions
{
	public class TKDDI_NOT_IMPLEMENTED_ERR : Exception
	{
		private const string _message = "Feature not implemented.";
		private const short Code = -10151;

		public TKDDI_NOT_IMPLEMENTED_ERR() : base(_message) { }

		public TKDDI_NOT_IMPLEMENTED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
