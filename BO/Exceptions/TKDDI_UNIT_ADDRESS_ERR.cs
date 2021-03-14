using System;

namespace PM.BO.Exceptions
{
	public class TKDDI_UNIT_ADDRESS_ERR : Exception
	{
		private const string _message = "Unit address out of range.";
		private const short Code = -10152;

		public TKDDI_UNIT_ADDRESS_ERR() : base(_message) { }

		public TKDDI_UNIT_ADDRESS_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
