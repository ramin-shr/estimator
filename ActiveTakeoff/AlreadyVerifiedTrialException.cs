using System;

namespace QuoterPlan
{
	public class AlreadyVerifiedTrialException : TurboActivateException
	{
		public AlreadyVerifiedTrialException() : base("The trial is already a verified trial. You need to use the \"TA_VERIFIED_TRIAL\" flag. Can't \"downgrade\" a verified trial to an unverified trial.")
		{
		}
	}
}
