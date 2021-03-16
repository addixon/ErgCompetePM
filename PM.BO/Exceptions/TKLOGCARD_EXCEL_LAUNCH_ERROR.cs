using System;

namespace PM.BO.Exceptions
{
	public class TKLOGCARD_EXCEL_LAUNCH_ERROR : Exception
	{
		private const string _message = "Error opening file in Excel.  Unable to run Excel..";
		private const short Code = -10196;

		public TKLOGCARD_EXCEL_LAUNCH_ERROR() : base(_message) { }

		public TKLOGCARD_EXCEL_LAUNCH_ERROR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
