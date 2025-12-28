using System;

namespace QuoterPlan
{
	public class TrialExtExpiredException : TurboActivateException
	{
		public TrialExtExpiredException() : base("The trial extension has expired.")
		{
		}
	}
}
