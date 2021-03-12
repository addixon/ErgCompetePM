using System;

namespace BO.Exceptions
{
	public class TKRACE_PARTICIPANT_LIST_SET_FAIL : Exception
	{
		private const string _message = "Unable to set race participant list on one or more PMs..";
		private const short Code = -10916;

		public TKRACE_PARTICIPANT_LIST_SET_FAIL() : base(_message) { }

		public TKRACE_PARTICIPANT_LIST_SET_FAIL(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
