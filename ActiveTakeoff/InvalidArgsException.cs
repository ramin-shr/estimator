using System;

namespace QuoterPlan
{
	public class InvalidArgsException : TurboActivateException
	{
		public InvalidArgsException() : base("The arguments passed to the function are invalid. Double check your logic.")
		{
		}
	}
}
