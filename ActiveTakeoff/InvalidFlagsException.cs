using System;

namespace QuoterPlan
{
	public class InvalidFlagsException : TurboActivateException
	{
		public InvalidFlagsException() : base("The flags you passed to the function were invalid (or missing). Flags like \"TA_SYSTEM\" and \"TA_USER\" are mutually exclusive -- you can only use one or the other.")
		{
		}
	}
}
