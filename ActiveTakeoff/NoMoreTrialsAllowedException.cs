using System;

namespace QuoterPlan
{
	public class NoMoreTrialsAllowedException : TurboActivateException
	{
		public NoMoreTrialsAllowedException() : base("In the LimeLM account either the trial days is set to 0, OR the account is set to not auto-upgrade and thus no more verified trials can be made.")
		{
		}
	}
}
