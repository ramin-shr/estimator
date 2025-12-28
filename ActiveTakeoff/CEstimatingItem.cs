using System;
using System.Runtime.CompilerServices;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class CEstimatingItem
	{
		public string ItemID
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemID>k__BackingField = value;
			}
		}

		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		public double Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
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
			set
			{
				this.<Unit>k__BackingField = value;
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
			set
			{
				this.<Formula>k__BackingField = value;
			}
		}

		public object Tag
		{
			[CompilerGenerated]
			get
			{
				return this.<Tag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tag>k__BackingField = value;
			}
		}

		public string InternalKey
		{
			[CompilerGenerated]
			get
			{
				return this.<InternalKey>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InternalKey>k__BackingField = value;
			}
		}

		public DBEstimatingItem.EstimatingItemType ItemType
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemType>k__BackingField = value;
			}
		}

		public DBEstimatingItem.UnitMeasureType UnitMeasure
		{
			[CompilerGenerated]
			get
			{
				return this.<UnitMeasure>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnitMeasure>k__BackingField = value;
			}
		}

		public double CoverageValue
		{
			[CompilerGenerated]
			get
			{
				return this.<CoverageValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CoverageValue>k__BackingField = value;
			}
		}

		public double CoverageUnit
		{
			[CompilerGenerated]
			get
			{
				return this.<CoverageUnit>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CoverageUnit>k__BackingField = value;
			}
		}

		public double CoverageRate
		{
			get
			{
				return this.CoverageValue / ((this.CoverageUnit <= 0.0) ? 1.0 : this.CoverageUnit);
			}
		}

		public string ItemTypeCaption
		{
			get
			{
				return DBEstimatingItem.GetItemTypeCaption(this.ItemType);
			}
		}

		public string UnitMeasureCaption
		{
			get
			{
				return DBEstimatingItem.GetUnitMeasureCaption(this.UnitMeasure);
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
			set
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
			set
			{
				this.<SubSectionID>k__BackingField = value;
			}
		}

		public string BidCode
		{
			[CompilerGenerated]
			get
			{
				return this.<BidCode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BidCode>k__BackingField = value;
			}
		}

		public CEstimatingItem()
		{
			this.Clear();
		}

		public CEstimatingItem(string itemID, string description, double value, string unit, DBEstimatingItem.EstimatingItemType itemType, DBEstimatingItem.UnitMeasureType unitMeasure, double coverageValue, double coverageUnit, int sectionID, int subSectionID, string bidCode, string formula)
		{
			this.ItemID = itemID;
			this.Description = description;
			this.Value = value;
			this.Unit = unit;
			this.ItemType = itemType;
			this.UnitMeasure = unitMeasure;
			this.CoverageValue = coverageValue;
			this.CoverageUnit = coverageUnit;
			this.SectionID = sectionID;
			this.SubSectionID = subSectionID;
			this.BidCode = bidCode;
			this.Formula = formula;
			this.InternalKey = Guid.NewGuid().ToString();
		}

		public void Clear()
		{
			this.ItemID = "";
			this.Description = "";
			this.Value = 0.0;
			this.Unit = "";
			this.UnitMeasure = DBEstimatingItem.UnitMeasureType.none;
			this.CoverageValue = 0.0;
			this.CoverageUnit = 1.0;
			this.SectionID = 0;
			this.SubSectionID = 0;
			this.BidCode = "";
			this.Formula = "";
			this.Tag = null;
			this.InternalKey = Guid.NewGuid().ToString();
		}

		public UnitScale.UnitSystem ResultSystemType(UnitScale.UnitSystem defaultSystemType)
		{
			if (this.UnitMeasure == DBEstimatingItem.UnitMeasureType.m || this.UnitMeasure == DBEstimatingItem.UnitMeasureType.sq_m || this.UnitMeasure == DBEstimatingItem.UnitMeasureType.cu_m || this.Unit.ToUpper() == "M" || this.Unit.ToUpper() == "M²" || this.Unit.ToUpper() == "M³")
			{
				return UnitScale.UnitSystem.metric;
			}
			if (this.UnitMeasure == DBEstimatingItem.UnitMeasureType.lin_ft || this.UnitMeasure == DBEstimatingItem.UnitMeasureType.sq_ft || this.UnitMeasure == DBEstimatingItem.UnitMeasureType.cu_yd || this.Unit.ToUpper() == Resources.pi.ToUpper() || this.Unit.ToUpper() == Resources.pi_2.ToUpper() || this.Unit.ToUpper() == Resources.pi_3.ToUpper() || this.Unit.ToUpper() == Resources.v_3.ToUpper())
			{
				return UnitScale.UnitSystem.imperial;
			}
			return defaultSystemType;
		}

		public CEstimatingItem Clone(bool preserveInternalKey = false)
		{
			CEstimatingItem cestimatingItem = new CEstimatingItem(this.ItemID, this.Description, this.Value, this.Unit, this.ItemType, this.UnitMeasure, this.CoverageValue, this.CoverageUnit, this.SectionID, this.SubSectionID, this.BidCode, this.Formula);
			cestimatingItem.Tag = cestimatingItem;
			if (preserveInternalKey)
			{
				cestimatingItem.InternalKey = this.InternalKey;
			}
			return cestimatingItem;
		}

		public void Dump()
		{
			Console.WriteLine("SectionID = " + this.ItemID);
			Console.WriteLine("Name = " + this.Description);
			Console.WriteLine("Value = " + this.Value);
			Console.WriteLine("Unit = " + this.Unit);
			Console.WriteLine("UnitMeasure = " + this.UnitMeasure);
			Console.WriteLine("CoverageValue = " + this.CoverageValue);
			Console.WriteLine("CoverageUnit = " + this.CoverageUnit);
			Console.WriteLine("SectionID = " + this.SectionID);
			Console.WriteLine("SubSectionID = " + this.SubSectionID);
			Console.WriteLine("BidCode = " + this.BidCode);
			Console.WriteLine("Formula = " + this.Formula);
			Console.WriteLine("InternalKey = " + this.InternalKey);
		}

		[CompilerGenerated]
		private string <ItemID>k__BackingField;

		[CompilerGenerated]
		private string <Description>k__BackingField;

		[CompilerGenerated]
		private double <Value>k__BackingField;

		[CompilerGenerated]
		private string <Unit>k__BackingField;

		[CompilerGenerated]
		private string <Formula>k__BackingField;

		[CompilerGenerated]
		private object <Tag>k__BackingField;

		[CompilerGenerated]
		private string <InternalKey>k__BackingField;

		[CompilerGenerated]
		private DBEstimatingItem.EstimatingItemType <ItemType>k__BackingField;

		[CompilerGenerated]
		private DBEstimatingItem.UnitMeasureType <UnitMeasure>k__BackingField;

		[CompilerGenerated]
		private double <CoverageValue>k__BackingField;

		[CompilerGenerated]
		private double <CoverageUnit>k__BackingField;

		[CompilerGenerated]
		private int <SectionID>k__BackingField;

		[CompilerGenerated]
		private int <SubSectionID>k__BackingField;

		[CompilerGenerated]
		private string <BidCode>k__BackingField;
	}
}
