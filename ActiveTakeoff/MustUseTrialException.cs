using System;

namespace QuoterPlan
{
	public class MustUseTrialException : TurboActivateException
	{
		public MustUseTrialException() : base("You must call TA_UseTrial() before you can get the number of trial days remaining.")
		{
		}
	}
}
