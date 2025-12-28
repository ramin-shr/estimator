using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class Workspace
	{
		public Plan ActivePlan
		{
			[CompilerGenerated]
			get
			{
				return this.<ActivePlan>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ActivePlan>k__BackingField = value;
			}
		}

		public Variables RecentPlans
		{
			[CompilerGenerated]
			get
			{
				return this.<RecentPlans>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RecentPlans>k__BackingField = value;
			}
		}

		public Workspace()
		{
			this.ActivePlan = null;
			this.RecentPlans = new Variables();
		}

		public void Clear()
		{
			this.ActivePlan = null;
			this.RecentPlans.Clear();
		}

		[CompilerGenerated]
		private Plan <ActivePlan>k__BackingField;

		[CompilerGenerated]
		private Variables <RecentPlans>k__BackingField;
	}
}
