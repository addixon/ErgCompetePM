using System;

namespace BO.Exceptions
{
	public class IOUSB_CFG_ENDPT_ERR : Exception
	{
		private const string _message = "USB endpoint configuration timeout error.";
		private const short Code = -932;

		public IOUSB_CFG_ENDPT_ERR() : base(_message) { }

		public IOUSB_CFG_ENDPT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
