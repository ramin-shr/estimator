using System;
using System.Collections.Generic;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class GroupStats
	{
		public int GroupCount
		{
			get
			{
				return this.groupCount;
			}
			set
			{
				this.groupCount = value;
			}
		}

		public double Area
		{
			get
			{
				return this.area;
			}
			set
			{
				this.area = value;
			}
		}

		public double DeductionArea
		{
			get
			{
				return this.deductionArea;
			}
			set
			{
				this.deductionArea = value;
			}
		}

		public double AreaMinusDeduction
		{
			get
			{
				return this.area - this.deductionArea;
			}
		}

		public int DeductionsCount
		{
			get
			{
				return this.deductionsCount;
			}
			set
			{
				this.deductionsCount = value;
			}
		}

		public double Perimeter
		{
			get
			{
				return this.perimeter;
			}
			set
			{
				this.perimeter = value;
			}
		}

		public double DeductionPerimeter
		{
			get
			{
				return this.deductionPerimeter;
			}
			set
			{
				this.deductionPerimeter = value;
			}
		}

		public double PerimeterPlusDeduction
		{
			get
			{
				return this.Perimeter + this.deductionPerimeter;
			}
		}

		public double PerimeterMinusOpening
		{
			get
			{
				return this.Perimeter - this.deductionPerimeter;
			}
		}

		public double DropLength
		{
			get
			{
				return this.dropLength;
			}
			set
			{
				this.dropLength = value;
			}
		}

		public double PerimeterPlusDrop
		{
			get
			{
				return this.Perimeter + this.dropLength;
			}
		}

		public double NetLength
		{
			get
			{
				return this.PerimeterMinusOpening + this.dropLength;
			}
		}

		public int DropsCount
		{
			get
			{
				return this.dropsCount;
			}
			set
			{
				this.dropsCount = value;
			}
		}

		public int CornersCount
		{
			get
			{
				return this.cornersCount;
			}
			set
			{
				this.cornersCount = value;
			}
		}

		public int EndsCount
		{
			get
			{
				return this.endsCount;
			}
			set
			{
				this.endsCount = value;
			}
		}

		public int SegmentsCount
		{
			get
			{
				return this.segmentsCount;
			}
			set
			{
				this.segmentsCount = value;
			}
		}

		public string ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		public Dictionary<string, Dictionary<string, CustomRenderingResult>> CustomRenderingArray
		{
			get
			{
				return this.customRenderingArray;
			}
		}

		public Dictionary<string, CustomRenderingResult> RenderingResults(string extensionID)
		{
			if (this.CustomRenderingArray.ContainsKey(extensionID))
			{
				return this.CustomRenderingArray[extensionID];
			}
			return null;
		}

		public CustomRenderingResult RenderingResult(string extensionID, string propertyName)
		{
			Dictionary<string, CustomRenderingResult> dictionary = this.RenderingResults(extensionID);
			if (dictionary != null && dictionary.ContainsKey(propertyName))
			{
				return dictionary[propertyName];
			}
			return null;
		}

		private static void ComputeRenderingResults(GroupStats resultStats, GroupStats drawObjectStats1, GroupStats drawObjectStats2)
		{
			foreach (KeyValuePair<string, Dictionary<string, CustomRenderingResult>> keyValuePair in drawObjectStats2.CustomRenderingArray)
			{
				Dictionary<string, CustomRenderingResult> value = keyValuePair.Value;
				Dictionary<string, CustomRenderingResult> dictionary = new Dictionary<string, CustomRenderingResult>();
				foreach (KeyValuePair<string, CustomRenderingResult> keyValuePair2 in value)
				{
					dictionary.Add(keyValuePair2.Key, keyValuePair2.Value.Clone());
				}
				resultStats.CustomRenderingArray.Add(keyValuePair.Key, dictionary);
			}
			foreach (KeyValuePair<string, Dictionary<string, CustomRenderingResult>> keyValuePair3 in drawObjectStats1.CustomRenderingArray)
			{
				if (!resultStats.CustomRenderingArray.ContainsKey(keyValuePair3.Key))
				{
					Dictionary<string, CustomRenderingResult> value2 = keyValuePair3.Value;
					Dictionary<string, CustomRenderingResult> dictionary2 = new Dictionary<string, CustomRenderingResult>();
					foreach (KeyValuePair<string, CustomRenderingResult> keyValuePair4 in value2)
					{
						dictionary2.Add(keyValuePair4.Key, keyValuePair4.Value.Clone());
					}
					resultStats.CustomRenderingArray.Add(keyValuePair3.Key, dictionary2);
				}
			}
			foreach (KeyValuePair<string, Dictionary<string, CustomRenderingResult>> keyValuePair5 in resultStats.CustomRenderingArray)
			{
				Dictionary<string, CustomRenderingResult> value3 = keyValuePair5.Value;
				Dictionary<string, CustomRenderingResult> dictionary3 = null;
				Dictionary<string, CustomRenderingResult> dictionary4 = null;
				if (drawObjectStats1.CustomRenderingArray.ContainsKey(keyValuePair5.Key))
				{
					dictionary3 = drawObjectStats1.CustomRenderingArray[keyValuePair5.Key];
				}
				if (drawObjectStats2.CustomRenderingArray.ContainsKey(keyValuePair5.Key))
				{
					dictionary4 = drawObjectStats2.CustomRenderingArray[keyValuePair5.Key];
				}
				foreach (KeyValuePair<string, CustomRenderingResult> keyValuePair6 in value3)
				{
					double num = 0.0;
					double num2 = 0.0;
					if (dictionary3 != null && dictionary3.ContainsKey(keyValuePair6.Key))
					{
						num = dictionary3[keyValuePair6.Key].Result;
					}
					if (dictionary4 != null && dictionary4.ContainsKey(keyValuePair6.Key))
					{
						num2 = dictionary4[keyValuePair6.Key].Result;
					}
					keyValuePair6.Value.Result = num + num2;
				}
			}
		}

		public GroupStats(string objectType)
		{
			this.objectType = objectType;
		}

		public static GroupStats operator +(GroupStats drawObjectStats1, GroupStats drawObjectStats2)
		{
			GroupStats groupStats = new GroupStats(drawObjectStats1.ObjectType);
			if (drawObjectStats1.ObjectType != drawObjectStats2.ObjectType)
			{
				Console.WriteLine(Resources.Groupe_incompatible);
				return groupStats;
			}
			groupStats.GroupCount = drawObjectStats1.GroupCount + drawObjectStats2.GroupCount;
			groupStats.Area = drawObjectStats1.Area + drawObjectStats2.Area;
			groupStats.DeductionArea = drawObjectStats1.DeductionArea + drawObjectStats2.DeductionArea;
			groupStats.DeductionsCount = drawObjectStats1.DeductionsCount + drawObjectStats2.DeductionsCount;
			groupStats.Perimeter = drawObjectStats1.Perimeter + drawObjectStats2.Perimeter;
			groupStats.DropLength = drawObjectStats1.DropLength + drawObjectStats2.DropLength;
			groupStats.DeductionPerimeter = drawObjectStats1.DeductionPerimeter + drawObjectStats2.DeductionPerimeter;
			groupStats.DropsCount = drawObjectStats1.DropsCount + drawObjectStats2.DropsCount;
			groupStats.EndsCount = drawObjectStats1.EndsCount + drawObjectStats2.EndsCount;
			groupStats.SegmentsCount = drawObjectStats1.SegmentsCount + drawObjectStats2.SegmentsCount;
			groupStats.CornersCount = drawObjectStats1.CornersCount + drawObjectStats2.CornersCount;
			if (drawObjectStats1.ObjectType == "Area")
			{
				GroupStats.ComputeRenderingResults(groupStats, drawObjectStats1, drawObjectStats2);
			}
			return groupStats;
		}

		private string objectType;

		private int groupCount;

		private double area;

		private double deductionArea;

		private int deductionsCount;

		private double perimeter;

		private double deductionPerimeter;

		private double dropLength;

		private int cornersCount;

		private int endsCount;

		private int segmentsCount;

		private int dropsCount;

		private Dictionary<string, Dictionary<string, CustomRenderingResult>> customRenderingArray = new Dictionary<string, Dictionary<string, CustomRenderingResult>>();

		private Dictionary<string, CustomRenderingResult> renderingResultsArray = new Dictionary<string, CustomRenderingResult>();
	}
}
