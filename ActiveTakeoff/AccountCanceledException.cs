using System;

namespace QuoterPlan
{
	public class AccountCanceledException : TurboActivateException
	{
		public AccountCanceledException() : base("Can't activate because the LimeLM account is cancelled.")
		{
		}
	}
}
