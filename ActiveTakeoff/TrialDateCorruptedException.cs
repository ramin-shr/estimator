using System;

namespace QuoterPlan
{
	public class TrialDateCorruptedException : TurboActivateException
	{
		public TrialDateCorruptedException() : base("The trial data has been corrupted, using the oldest date possible.")
		{
		}
	}
}
