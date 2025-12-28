using System;

namespace QuoterPlan
{
	public class TurboFloatKeyException : TurboActivateException
	{
		public TurboFloatKeyException() : base("The product key used is for TurboFloat Server, not TurboActivate.")
		{
		}
	}
}
