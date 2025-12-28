using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class StatusArgs : EventArgs
	{
		public TA_TrialStatus Status
		{
			[CompilerGenerated]
			get
			{
				return this.<Status>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Status>k__BackingField = value;
			}
		}

		public StatusArgs()
		{
		}

		[CompilerGenerated]
		private TA_TrialStatus <Status>k__BackingField;
	}
}
