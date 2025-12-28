using System;
using System.Runtime.CompilerServices;
using DevExpress.XtraEditors.DXErrorProvider;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class EstimatingItem : IDXDataErrorInfo
	{
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

		public int ParentID
		{
			[CompilerGenerated]
			get
			{
				return this.<ParentID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ParentID>k__BackingField = value;
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

		public string ExtensionID
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ExtensionID>k__BackingField = value;
			}
		}

		public string ResultID
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResultID>k__BackingField = value;
			}
		}

		public string ObjectType
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ObjectType>k__BackingField = value;
			}
		}

		public string ResultName
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ResultName>k__BackingField = value;
			}
		}

		public double ResultValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ResultValue>k__BackingField = value;
			}
		}

		public string ResultUnit
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultUnit>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ResultUnit>k__BackingField = value;
			}
		}

		public double CostEach
		{
			[CompilerGenerated]
			get
			{
				return this.<CostEach>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CostEach>k__BackingField = value;
			}
		}

		public double MarkupEach
		{
			[CompilerGenerated]
			get
			{
				return this.<MarkupEach>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MarkupEach>k__BackingField = value;
			}
		}

		public double PriceEach
		{
			get
			{
				return Math.Round(this.CostEach * (1.0 + this.MarkupEach / 100.0), 2);
			}
		}

		public double PriceTotal
		{
			get
			{
				return Math.Round(this.ResultValue * this.PriceEach, 2);
			}
		}

		public string InternalUnit
		{
			[CompilerGenerated]
			get
			{
				return this.<InternalUnit>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InternalUnit>k__BackingField = value;
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
			set
			{
				this.<ItemID>k__BackingField = value;
			}
		}

		public EstimatingItem(int id, int parentID, int groupID, string extensionID, string resultID, string objectType, string resultName, double resultValue, string resultUnit, double costEach, double markupEach, int itemID = -1)
		{
			this.ID = id;
			this.ParentID = parentID;
			this.GroupID = groupID;
			this.ExtensionID = extensionID;
			this.ResultID = resultID;
			this.ObjectType = objectType;
			this.ResultName = resultName;
			if (resultUnit == Utilities.GetCurrencySymbol())
			{
				this.ResultValue = 1.0;
				this.ResultUnit = Resources.chaque;
				this.CostEach = resultValue;
			}
			else
			{
				this.ResultValue = resultValue;
				this.ResultUnit = resultUnit;
				this.CostEach = costEach;
			}
			this.MarkupEach = markupEach;
			this.InternalUnit = resultUnit;
			this.ItemID = itemID;
		}

		public void Clear()
		{
			this.ID = 0;
			this.ParentID = 0;
			this.GroupID = -1;
			this.ExtensionID = "";
			this.ResultID = "";
			this.ObjectType = "";
			this.ResultName = "";
			this.ResultValue = 0.0;
			this.ResultUnit = "";
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("ParentID = " + this.ParentID);
			Console.WriteLine("GroupID = " + this.GroupID);
			Console.WriteLine("ExtensionID = " + this.ExtensionID);
			Console.WriteLine("ResultID = " + this.ResultID);
			Console.WriteLine("ObjectType = " + this.ObjectType);
			Console.WriteLine("ResultName = " + this.ResultName);
			Console.WriteLine("ResultValue = " + this.ResultValue);
			Console.WriteLine("ResultUnit = " + this.ResultUnit);
			Console.WriteLine("CostEach = " + this.CostEach);
			Console.WriteLine("MarkupEach = " + this.MarkupEach);
			Console.WriteLine("PriceEach = " + this.PriceEach);
			Console.WriteLine("PriceTotal = " + this.PriceTotal);
		}

		void IDXDataErrorInfo.GetError(ErrorInfo info)
		{
			if (this.IsStringEmpty(this.ObjectType))
			{
				this.SetErrorInfo(info, "Vous devez fournir une valeur.", ErrorType.Critical);
			}
		}

		void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
		{
			if (propertyName != null)
			{
				if (!(propertyName == "Name"))
				{
					return;
				}
				if (this.IsStringEmpty(this.ObjectType))
				{
					this.SetErrorInfo(info, "Vous devez fournir une valeur", ErrorType.Information);
				}
			}
		}

		private void SetErrorInfo(ErrorInfo info, string errorText, ErrorType errorType)
		{
			info.ErrorText = errorText;
			info.ErrorType = errorType;
		}

		private bool IsStringEmpty(string str)
		{
			return str != null && str.Trim().Length == 0;
		}

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private int <ParentID>k__BackingField;

		[CompilerGenerated]
		private int <GroupID>k__BackingField;

		[CompilerGenerated]
		private string <ExtensionID>k__BackingField;

		[CompilerGenerated]
		private string <ResultID>k__BackingField;

		[CompilerGenerated]
		private string <ObjectType>k__BackingField;

		[CompilerGenerated]
		private string <ResultName>k__BackingField;

		[CompilerGenerated]
		private double <ResultValue>k__BackingField;

		[CompilerGenerated]
		private string <ResultUnit>k__BackingField;

		[CompilerGenerated]
		private double <CostEach>k__BackingField;

		[CompilerGenerated]
		private double <MarkupEach>k__BackingField;

		[CompilerGenerated]
		private string <InternalUnit>k__BackingField;

		[CompilerGenerated]
		private int <ItemID>k__BackingField;
	}
}
