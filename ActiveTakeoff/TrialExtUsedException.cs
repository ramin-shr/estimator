using System;

namespace QuoterPlan
{
	public class TrialExtUsedException : TurboActivateException
	{
		public TrialExtUsedException() : base("The trial extension has already been used.")
		{
		}
	}
}
