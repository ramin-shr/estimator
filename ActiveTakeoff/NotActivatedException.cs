using System;

namespace QuoterPlan
{
	public class NotActivatedException : TurboActivateException
	{
		public NotActivatedException() : base("The product needs to be activated.")
		{
		}
	}
}
