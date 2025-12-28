using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class FilterElement
	{
		public string PlanName
		{
			[CompilerGenerated]
			get
			{
				return this.<PlanName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PlanName>k__BackingField = value;
			}
		}

		public string LayerName
		{
			[CompilerGenerated]
			get
			{
				return this.<LayerName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LayerName>k__BackingField = value;
			}
		}

		public int GroupID
		{
			[CompilerGenerated]
			get
			{
				return this.<GroupID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GroupID>k__BackingField = value;
			}
		}

		public FilterElement(string planName, string layerName, int groupID)
		{
			this.PlanName = planName;
			this.LayerName = layerName;
			this.GroupID = groupID;
		}

		[CompilerGenerated]
		private string <PlanName>k__BackingField;

		[CompilerGenerated]
		private string <LayerName>k__BackingField;

		[CompilerGenerated]
		private int <GroupID>k__BackingField;
	}
}
