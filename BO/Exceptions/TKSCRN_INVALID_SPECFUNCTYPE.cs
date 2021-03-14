using System;

namespace PM.BO.Exceptions
{
	public class TKSCRN_INVALID_SPECFUNCTYPE : Exception
	{
		private const string _message = "Invalid special function type.";
		private const short Code = -230;

		public TKSCRN_INVALID_SPECFUNCTYPE() : base(_message) { }

		public TKSCRN_INVALID_SPECFUNCTYPE(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
