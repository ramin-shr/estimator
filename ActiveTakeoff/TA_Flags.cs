using System;

namespace QuoterPlan
{
	[Flags]
	public enum TA_Flags : uint
	{
		TA_SYSTEM = 1U,
		TA_USER = 2U,
		TA_DISALLOW_VM = 4U,
		TA_UNVERIFIED_TRIAL = 16U,
		TA_VERIFIED_TRIAL = 32U
	}
}
