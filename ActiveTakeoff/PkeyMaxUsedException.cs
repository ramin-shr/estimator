using System;

namespace QuoterPlan
{
	public class PkeyMaxUsedException : TurboActivateException
	{
		public PkeyMaxUsedException() : base("The product key has already been activated with the maximum number of computers.")
		{
		}
	}
}
