using System;

namespace BO.Exceptions
{
	public class TKCMDPR_ROUTE_TABLE_FULL_ERR : Exception
	{
		private const string _message = "The routing table size has been exceeded.";
		private const short Code = -126;

		public TKCMDPR_ROUTE_TABLE_FULL_ERR() : base(_message) { }

		public TKCMDPR_ROUTE_TABLE_FULL_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
