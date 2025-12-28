using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class EstimatingItemPrice
	{
		public string Key
		{
			get
			{
				return string.Concat(new string[]
				{
					this.GroupID.ToString(),
					";",
					this.ExtensionID,
					";",
					this.ResultID,
					";"
				});
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

		public UnitScale.UnitSystem SystemType
		{
			[CompilerGenerated]
			get
			{
				return this.<SystemType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SystemType>k__BackingField = value;
			}
		}

		public EstimatingItemPrice(string key, double costEach, double markupEach, UnitScale.UnitSystem systemType)
		{
			try
			{
				string[] fields = Utilities.GetFields(key, ';', StringSplitOptions.None);
				this.GroupID = Utilities.ConvertToInt(fields[0]);
				this.ExtensionID = fields[1].ToString();
				this.ResultID = fields[2].ToString();
				this.CostEach = costEach;
				this.MarkupEach = markupEach;
				this.SystemType = systemType;
			}
			catch
			{
			}
		}

		public EstimatingItemPrice(EstimatingItem estimatingItem, UnitScale.UnitSystem systemType)
		{
			this.GroupID = estimatingItem.GroupID;
			this.ExtensionID = estimatingItem.ExtensionID;
			this.ResultID = estimatingItem.ResultID;
			this.CostEach = estimatingItem.CostEach;
			this.MarkupEach = estimatingItem.MarkupEach;
			this.SystemType = systemType;
		}

		public EstimatingItemPrice(int groupID, string extensionID, string resultID, double costEach, double markupEach, UnitScale.UnitSystem systemType)
		{
			this.GroupID = groupID;
			this.ExtensionID = extensionID;
			this.ResultID = resultID;
			this.CostEach = costEach;
			this.MarkupEach = markupEach;
			this.SystemType = systemType;
		}

		public static string GenerateKey(EstimatingItem estimatingItem)
		{
			return string.Concat(new string[]
			{
				estimatingItem.GroupID.ToString(),
				";",
				estimatingItem.ExtensionID,
				";",
				estimatingItem.ResultID,
				";"
			});
		}

		[CompilerGenerated]
		private int <GroupID>k__BackingField;

		[CompilerGenerated]
		private string <ExtensionID>k__BackingField;

		[CompilerGenerated]
		private string <ResultID>k__BackingField;

		[CompilerGenerated]
		private double <CostEach>k__BackingField;

		[CompilerGenerated]
		private double <MarkupEach>k__BackingField;

		[CompilerGenerated]
		private UnitScale.UnitSystem <SystemType>k__BackingField;
	}
}
