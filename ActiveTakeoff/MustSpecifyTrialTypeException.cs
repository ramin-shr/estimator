using System;

namespace QuoterPlan
{
	public class MustSpecifyTrialTypeException : TurboActivateException
	{
		public MustSpecifyTrialTypeException() : base("You must specify the trial type (TA_UNVERIFIED_TRIAL or TA_VERIFIED_TRIAL). And you can't use both flags. Choose one or the other. We recommend TA_VERIFIED_TRIAL.")
		{
		}
	}
}
