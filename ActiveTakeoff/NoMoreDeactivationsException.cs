using System;

namespace QuoterPlan
{
	public class NoMoreDeactivationsException : TurboActivateException
	{
		public NoMoreDeactivationsException() : base("No more deactivations are allowed for the product key. This product is still activated on this computer.")
		{
		}
	}
}
