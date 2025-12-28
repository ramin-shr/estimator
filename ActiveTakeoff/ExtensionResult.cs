using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ExtensionResult
	{
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Name>k__BackingField = value;
			}
		}

		public string Caption
		{
			[CompilerGenerated]
			get
			{
				return this.<Caption>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Caption>k__BackingField = value;
			}
		}

		public string Unit
		{
			[CompilerGenerated]
			get
			{
				return this.<Unit>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Unit>k__BackingField = value;
			}
		}

		public int ItemID
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemID>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ItemID>k__BackingField = value;
			}
		}

		public int SectionID
		{
			[CompilerGenerated]
			get
			{
				return this.<SectionID>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<SectionID>k__BackingField = value;
			}
		}

		public int SubSectionID
		{
			[CompilerGenerated]
			get
			{
				return this.<SubSectionID>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<SubSectionID>k__BackingField = value;
			}
		}

		public string Formula
		{
			[CompilerGenerated]
			get
			{
				return this.<Formula>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Formula>k__BackingField = value;
			}
		}

		public string Condition
		{
			[CompilerGenerated]
			get
			{
				return this.<Condition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Condition>k__BackingField = value;
			}
		}

		public ExtensionResult.ExtensionResultTypeEnum ResultType
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ResultType>k__BackingField = value;
			}
		}

		public bool ShowInLegend
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowInLegend>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ShowInLegend>k__BackingField = value;
			}
		}

		public bool IsEstimatingItem
		{
			[CompilerGenerated]
			get
			{
				return this.<IsEstimatingItem>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsEstimatingItem>k__BackingField = value;
			}
		}

		public ExtensionResult()
		{
		}

		public ExtensionResult(string name, string caption, string unit, int itemID, int sectionID, int subSectionID, string formula, string condition, ExtensionResult.ExtensionResultTypeEnum resultType, bool showInLegend, bool isEstimatingItem)
		{
			this.Name = name;
			this.Caption = caption;
			this.Unit = unit;
			this.ItemID = itemID;
			this.SectionID = sectionID;
			this.SubSectionID = subSectionID;
			this.Formula = formula;
			this.Condition = condition;
			this.ResultType = resultType;
			this.ShowInLegend = showInLegend;
			this.IsEstimatingItem = (isEstimatingItem || itemID != -1);
		}

		public void Clear()
		{
			this.Name = "";
			this.Caption = "";
			this.Unit = "";
			this.ItemID = -1;
			this.SectionID = -1;
			this.SubSectionID = -1;
			this.Formula = "";
			this.Condition = "";
		}

		public void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("Caption = " + this.Caption);
			Console.WriteLine("Unit = " + this.Unit);
			Console.WriteLine("ItemID = " + this.ItemID);
			Console.WriteLine("SectionID = " + this.SectionID);
			Console.WriteLine("SubSectionID = " + this.SubSectionID);
			Console.WriteLine("Formula= " + this.Formula);
			Console.WriteLine("Condition= " + this.Condition);
			Console.WriteLine("ResultType = " + this.ResultType);
			Console.WriteLine("ShowInLegend = " + this.ShowInLegend);
			Console.WriteLine("IsEstimatingItem = " + this.IsEstimatingItem);
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private string <Unit>k__BackingField;

		[CompilerGenerated]
		private int <ItemID>k__BackingField;

		[CompilerGenerated]
		private int <SectionID>k__BackingField;

		[CompilerGenerated]
		private int <SubSectionID>k__BackingField;

		[CompilerGenerated]
		private string <Formula>k__BackingField;

		[CompilerGenerated]
		private string <Condition>k__BackingField;

		[CompilerGenerated]
		private ExtensionResult.ExtensionResultTypeEnum <ResultType>k__BackingField;

		[CompilerGenerated]
		private bool <ShowInLegend>k__BackingField;

		[CompilerGenerated]
		private bool <IsEstimatingItem>k__BackingField;

		public enum ExtensionResultTypeEnum
		{
			ResultTypeUnit,
			ResultTypeLength,
			ResultTypeArea,
			ResultTypeVolume,
			ResultTypeCurrency,
			ResultTypeCustom,
			ResultTypeEnumCount
		}
	}
}
