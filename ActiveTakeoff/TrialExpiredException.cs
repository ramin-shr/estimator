using System;

namespace QuoterPlan
{
	public class TrialExpiredException : TurboActivateException
	{
		public TrialExpiredException() : base("The verified trial has expired. You must request a trial extension from the company.")
		{
		}
	}
}
