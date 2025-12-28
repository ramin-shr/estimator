using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class CustomRenderingProperties
	{
		public int Angle
		{
			[CompilerGenerated]
			get
			{
				return this.<Angle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Angle>k__BackingField = value;
			}
		}

		public int OffsetX
		{
			[CompilerGenerated]
			get
			{
				return this.<OffsetX>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OffsetX>k__BackingField = value;
			}
		}

		public int OffsetY
		{
			[CompilerGenerated]
			get
			{
				return this.<OffsetY>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OffsetY>k__BackingField = value;
			}
		}

		public Dictionary<string, CustomRenderingResult> ResultsArray
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultsArray>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ResultsArray>k__BackingField = value;
			}
		}

		public CustomRenderingProperties()
		{
			this.Angle = 0;
			this.OffsetX = 0;
			this.OffsetY = 0;
			this.ResultsArray = new Dictionary<string, CustomRenderingResult>();
		}

		public CustomRenderingProperties(int angle, int offsetX, int offsetY)
		{
			this.Angle = angle;
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
			this.ResultsArray = new Dictionary<string, CustomRenderingResult>();
		}

		public CustomRenderingProperties Clone()
		{
			CustomRenderingProperties customRenderingProperties = new CustomRenderingProperties(this.Angle, this.OffsetX, this.OffsetY);
			foreach (KeyValuePair<string, CustomRenderingResult> keyValuePair in customRenderingProperties.ResultsArray)
			{
				customRenderingProperties.ResultsArray.Add(keyValuePair.Key, keyValuePair.Value.Clone());
			}
			return customRenderingProperties;
		}

		[CompilerGenerated]
		private int <Angle>k__BackingField;

		[CompilerGenerated]
		private int <OffsetX>k__BackingField;

		[CompilerGenerated]
		private int <OffsetY>k__BackingField;

		[CompilerGenerated]
		private Dictionary<string, CustomRenderingResult> <ResultsArray>k__BackingField;
	}
}
