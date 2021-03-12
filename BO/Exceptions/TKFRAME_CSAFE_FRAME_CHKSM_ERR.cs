using System;

namespace BO.Exceptions
{
	public class TKFRAME_CSAFE_FRAME_CHKSM_ERR : Exception
	{
		private const string _message = "CSAFE frame checksum error.";
		private const short Code = -161;

		public TKFRAME_CSAFE_FRAME_CHKSM_ERR() : base(_message) { }

		public TKFRAME_CSAFE_FRAME_CHKSM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
