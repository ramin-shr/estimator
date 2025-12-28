using System;

namespace QuoterPlan
{
	public class InvalidHandleException : TurboActivateException
	{
		public InvalidHandleException() : base("The handle is not valid. You must set a valid VersionGUID when constructing TurboActivate object.")
		{
		}
	}
}
