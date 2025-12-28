using System;

namespace QuoterPlan
{
	public class PkeyRevokedException : TurboActivateException
	{
		public PkeyRevokedException() : base("The product key has been revoked.")
		{
		}
	}
}
