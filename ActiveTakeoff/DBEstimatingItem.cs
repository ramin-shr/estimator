using System;
using System.Runtime.CompilerServices;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class DBEstimatingItem
	{
		public static string GetItemTypeCaption(DBEstimatingItem.EstimatingItemType itemType)
		{
			if (itemType < DBEstimatingItem.EstimatingItemType.MaterialItem || itemType >= DBEstimatingItem.EstimatingItemType.count)
			{
				return "";
			}
			return DBEstimatingItem.itemTypeString[(int)itemType];
		}

		public static string GetUnitMeasureCaption(DBEstimatingItem.UnitMeasureType unitMeasure)
		{
			if (unitMeasure < DBEstimatingItem.UnitMeasureType.none || unitMeasure >= DBEstimatingItem.UnitMeasureType.count)
			{
				return "";
			}
			return DBEstimatingItem.unitMeasureString[(int)unitMeasure];
		}

		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ID>k__BackingField = value;
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
			private set
			{
				this.<ItemType>k__BackingField = value;
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

		public string PurchaseUnit
		{
			get
			{
				string text = "";
				if (this.purchaseUnit.Length > 0)
				{
					if (this.purchaseUnit[0] == '~')
					{
						text = this.purchaseUnit.Substring(1, this.purchaseUnit.Length - 1);
						if (UnitScale.CastUnitSystem(Settings.Default.DefaultSystemType) == UnitScale.UnitSystem.metric)
						{
							if (text == Resources.pi)
							{
								text = "m";
							}
							else if (text == Resources.pi_2)
							{
								text = "m²";
							}
							else if (text == Resources.pi_3)
							{
								text = "m³";
							}
						}
					}
					else
					{
						text = this.purchaseUnit;
					}
				}
				return text;
			}
			set
			{
				this.purchaseUnit = value;
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

		public string UnitMeasureCaption
		{
			get
			{
				return DBEstimatingItem.unitMeasureString[(int)this.UnitMeasure];
			}
		}

		public double PriceEach
		{
			[CompilerGenerated]
			get
			{
				return this.<PriceEach>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PriceEach>k__BackingField = value;
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

		public bool IsSystemItem
		{
			[CompilerGenerated]
			get
			{
				return this.<IsSystemItem>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsSystemItem>k__BackingField = value;
			}
		}

		public bool Dirty
		{
			[CompilerGenerated]
			get
			{
				return this.<Dirty>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Dirty>k__BackingField = value;
			}
		}

		public DBEstimatingItem(int id, DBEstimatingItem.EstimatingItemType itemType, string description, string purchaseUnit, DBEstimatingItem.UnitMeasureType unitMeasure, double coverageValue, double coverageUnit, double priceEach, int sectionID, int subSectionID, string bidCode, bool isSystemItem)
		{
			this.ID = id;
			this.ItemType = ((itemType < DBEstimatingItem.EstimatingItemType.MaterialItem || itemType > DBEstimatingItem.EstimatingItemType.count) ? DBEstimatingItem.EstimatingItemType.MaterialItem : itemType);
			this.Description = description;
			this.PurchaseUnit = purchaseUnit;
			this.UnitMeasure = ((unitMeasure < DBEstimatingItem.UnitMeasureType.none || unitMeasure >= DBEstimatingItem.UnitMeasureType.count) ? DBEstimatingItem.UnitMeasureType.none : unitMeasure);
			this.CoverageValue = coverageValue;
			this.CoverageUnit = ((coverageUnit <= 0.0) ? 1.0 : coverageUnit);
			this.PriceEach = priceEach;
			this.SectionID = sectionID;
			this.SubSectionID = subSectionID;
			this.BidCode = bidCode;
			this.IsSystemItem = isSystemItem;
			this.Dirty = false;
		}

		public DBEstimatingItem Duplicate(int id)
		{
			return new DBEstimatingItem(id, this.ItemType, this.Description, this.PurchaseUnit, this.UnitMeasure, this.CoverageValue, this.CoverageUnit, this.PriceEach, this.SectionID, this.SubSectionID, this.BidCode, this.IsSystemItem);
		}

		public bool MatchResultType(ExtensionResult.ExtensionResultTypeEnum resultType)
		{
			switch (resultType)
			{
			case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
				return this.PurchaseUnit == "m" || this.PurchaseUnit == Resources.pi;
			case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
				return this.PurchaseUnit == "m²" || this.PurchaseUnit == Resources.pi_2;
			case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
				return this.PurchaseUnit == "m³" || this.PurchaseUnit == Resources.pi_3;
			default:
				return this.PurchaseUnit != "m" && this.PurchaseUnit != Resources.pi && this.PurchaseUnit != "m²" && this.PurchaseUnit != Resources.pi_2 && this.PurchaseUnit != "m³" && this.PurchaseUnit != Resources.pi_3;
			}
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("ItemType = " + this.ItemType);
			Console.WriteLine("Description = " + this.Description);
			Console.WriteLine("PriceEach = " + this.PriceEach);
			Console.WriteLine("SectionID = " + this.SectionID);
			Console.WriteLine("SubSectionID = " + this.SubSectionID);
			Console.WriteLine("BidCode = " + this.BidCode);
			Console.WriteLine("IsSystemItem = " + this.IsSystemItem);
			Console.WriteLine("IsDirty = " + this.Dirty);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DBEstimatingItem()
		{
		}

		private static string[] itemTypeString = new string[]
		{
			Resources.Matériel,
			Resources.Main_d_oeuvre,
			Resources.Sous_contracteur,
			Resources.Équipement
		};

		private static string[] unitMeasureString = new string[]
		{
			Resources.aucun,
			Resources.pi,
			Resources.pi_2,
			Resources.v_3,
			"m",
			"m²",
			"m³"
		};

		public string purchaseUnit;

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private DBEstimatingItem.EstimatingItemType <ItemType>k__BackingField;

		[CompilerGenerated]
		private string <Description>k__BackingField;

		[CompilerGenerated]
		private DBEstimatingItem.UnitMeasureType <UnitMeasure>k__BackingField;

		[CompilerGenerated]
		private double <CoverageValue>k__BackingField;

		[CompilerGenerated]
		private double <CoverageUnit>k__BackingField;

		[CompilerGenerated]
		private double <PriceEach>k__BackingField;

		[CompilerGenerated]
		private int <SectionID>k__BackingField;

		[CompilerGenerated]
		private int <SubSectionID>k__BackingField;

		[CompilerGenerated]
		private string <BidCode>k__BackingField;

		[CompilerGenerated]
		private bool <IsSystemItem>k__BackingField;

		[CompilerGenerated]
		private bool <Dirty>k__BackingField;

		public enum EstimatingItemType
		{
			MaterialItem,
			LaborItem,
			SubcontractItem,
			EquipmentItem,
			count
		}

		public enum UnitMeasureType
		{
			none,
			lin_ft,
			sq_ft,
			cu_yd,
			m,
			sq_m,
			cu_m,
			count
		}
	}
}
