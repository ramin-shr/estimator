using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class PresetResult : ExtensionResult
	{
		public double Result
		{
			[CompilerGenerated]
			get
			{
				return this.<Result>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Result>k__BackingField = value;
			}
		}

		public bool ConditionMet
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionMet>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConditionMet>k__BackingField = value;
			}
		}

		public PresetResult(string name, string caption, string unit, string condition, string formula, ExtensionResult.ExtensionResultTypeEnum resultType, bool showInLegend = false, bool isEstimatingItem = true, int itemID = -1, int sectionID = -1, int subSectionID = -1)
		{
			base.Name = name;
			base.Caption = caption;
			base.Unit = unit;
			base.Condition = condition;
			base.Formula = formula;
			this.Result = 0.0;
			base.ResultType = resultType;
			base.ShowInLegend = showInLegend;
			base.IsEstimatingItem = isEstimatingItem;
			base.ItemID = itemID;
			base.SectionID = sectionID;
			base.SubSectionID = subSectionID;
			this.ConditionMet = false;
		}

		public new void Clear()
		{
			base.Clear();
			this.Result = 0.0;
			this.ConditionMet = false;
		}

		public new void Dump()
		{
			base.Dump();
			Console.WriteLine("Result = " + this.Result);
		}

		[CompilerGenerated]
		private double <Result>k__BackingField;

		[CompilerGenerated]
		private bool <ConditionMet>k__BackingField;
	}
}
