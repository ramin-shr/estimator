using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ReportSummary
	{
		public string Caption
		{
			[CompilerGenerated]
			get
			{
				return this.<Caption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Caption>k__BackingField = value;
			}
		}

		public double CostSubTotal
		{
			[CompilerGenerated]
			get
			{
				return this.<CostSubTotal>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CostSubTotal>k__BackingField = value;
			}
		}

		public double PriceSubTotal
		{
			[CompilerGenerated]
			get
			{
				return this.<PriceSubTotal>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PriceSubTotal>k__BackingField = value;
			}
		}

		public Plan Plan
		{
			[CompilerGenerated]
			get
			{
				return this.<Plan>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Plan>k__BackingField = value;
			}
		}

		public DrawObject GroupObject
		{
			[CompilerGenerated]
			get
			{
				return this.<GroupObject>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GroupObject>k__BackingField = value;
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

		public string LayerName
		{
			[CompilerGenerated]
			get
			{
				return this.<LayerName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LayerName>k__BackingField = value;
			}
		}

		public ReportSummary(string caption, double costSubTotal, double priceSubTotal, Plan plan, DrawObject groupObject, int groupID, string layerName)
		{
			this.Caption = caption;
			this.CostSubTotal = costSubTotal;
			this.PriceSubTotal = priceSubTotal;
			this.Plan = plan;
			this.GroupObject = groupObject;
			this.GroupID = groupID;
			this.LayerName = layerName;
		}

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private double <CostSubTotal>k__BackingField;

		[CompilerGenerated]
		private double <PriceSubTotal>k__BackingField;

		[CompilerGenerated]
		private Plan <Plan>k__BackingField;

		[CompilerGenerated]
		private DrawObject <GroupObject>k__BackingField;

		[CompilerGenerated]
		private int <GroupID>k__BackingField;

		[CompilerGenerated]
		private string <LayerName>k__BackingField;
	}
}
