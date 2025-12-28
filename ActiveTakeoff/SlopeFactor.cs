using System;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class SlopeFactor
	{
		public SlopeFactor.HipValleyEnum HipValley
		{
			get
			{
				return this.hipValley;
			}
			set
			{
				this.hipValley = value;
			}
		}

		public double InternalValue
		{
			get
			{
				return this.internalValue;
			}
			set
			{
				this.internalValue = value;
			}
		}

		private SlopeFactor.SlopeApplyTypeEnum ValidateSlopeApplyType(SlopeFactor.SlopeApplyTypeEnum slopeApplyType)
		{
			if (slopeApplyType < SlopeFactor.SlopeApplyTypeEnum.applyOnPlan)
			{
				slopeApplyType = SlopeFactor.SlopeApplyTypeEnum.applyOnPlan;
			}
			else if (slopeApplyType > SlopeFactor.SlopeApplyTypeEnum.applyOnElevation)
			{
				slopeApplyType = SlopeFactor.SlopeApplyTypeEnum.applyOnElevation;
			}
			return slopeApplyType;
		}

		public SlopeFactor.SlopeApplyTypeEnum SlopeApplyType
		{
			get
			{
				return this.ValidateSlopeApplyType(this.slopeApplyType);
			}
			set
			{
				this.slopeApplyType = this.ValidateSlopeApplyType(value);
			}
		}

		private SlopeFactor.SlopeTypeEnum ValidateSlopeType(SlopeFactor.SlopeTypeEnum slopeType)
		{
			if (slopeType < SlopeFactor.SlopeTypeEnum.slopeTypePercentage)
			{
				slopeType = SlopeFactor.SlopeTypeEnum.slopeTypePercentage;
			}
			else if (slopeType > SlopeFactor.SlopeTypeEnum.slopeType12)
			{
				slopeType = SlopeFactor.SlopeTypeEnum.slopeType12;
			}
			return slopeType;
		}

		public SlopeFactor.SlopeTypeEnum SlopeType
		{
			get
			{
				return this.ValidateSlopeType(this.slopeType);
			}
			set
			{
				this.slopeType = this.ValidateSlopeType(value);
			}
		}

		public void SetSlope(int value)
		{
			switch (this.SlopeType)
			{
			case SlopeFactor.SlopeTypeEnum.slopeTypePercentage:
				this.InternalValue = 5729.5779513082325 * Math.Atan((double)value / 100.0) / 100.0;
				return;
			case SlopeFactor.SlopeTypeEnum.slopeTypeDegree:
				this.InternalValue = (double)value;
				return;
			case SlopeFactor.SlopeTypeEnum.slopeType12:
				this.InternalValue = 5729.5779513082325 * Math.Atan((double)value / 12.0) / 100.0;
				return;
			default:
				return;
			}
		}

		public int GetSlope()
		{
			if (this.InternalValue <= 0.0)
			{
				return 0;
			}
			switch (this.SlopeType)
			{
			case SlopeFactor.SlopeTypeEnum.slopeTypePercentage:
				return (int)Math.Round(Math.Tan(this.InternalValue * 0.017453292519943295) * 10000.0) / 100;
			case SlopeFactor.SlopeTypeEnum.slopeTypeDegree:
				return (int)Math.Round(this.InternalValue);
			case SlopeFactor.SlopeTypeEnum.slopeType12:
				return (int)Math.Round(Math.Tan(this.InternalValue * 3.141592653589793 / 180.0) * 12.0);
			default:
				return 0;
			}
		}

		public string GetSlopeString()
		{
			if (this.InternalValue == 0.0)
			{
				return Resources.Aucune_pente;
			}
			int slope = this.GetSlope();
			switch (this.SlopeType)
			{
			case SlopeFactor.SlopeTypeEnum.slopeTypePercentage:
				return slope + " %";
			case SlopeFactor.SlopeTypeEnum.slopeTypeDegree:
				return slope + " °";
			case SlopeFactor.SlopeTypeEnum.slopeType12:
				return slope + " / 12";
			default:
				return Resources.Pente_non_définie;
			}
		}

		public string GetSlopeLongString()
		{
			if (this.InternalValue == 0.0)
			{
				return Resources.Aucune_pente;
			}
			string text = string.Empty;
			switch (this.SlopeApplyType)
			{
			case SlopeFactor.SlopeApplyTypeEnum.applyOnElevation:
				text = Resources.Vue_d_élévation;
				break;
			case SlopeFactor.SlopeApplyTypeEnum.applyOnPlan:
				text = Resources.Vue_de_plan;
				break;
			}
			if (this.SlopeType == SlopeFactor.SlopeTypeEnum.slopeType12)
			{
				text = text + " " + this.GetSlopeString();
			}
			else
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" ",
					Resources.à,
					" ",
					this.GetSlopeString()
				});
			}
			return text;
		}

		public void SetValues(SlopeFactor slopeFactor)
		{
			try
			{
				this.InternalValue = slopeFactor.InternalValue;
				this.SlopeApplyType = slopeFactor.SlopeApplyType;
				this.SlopeType = slopeFactor.SlopeType;
				this.HipValley = slopeFactor.hipValley;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		public void SetValues(double internalValue, SlopeFactor.SlopeTypeEnum slopeType, SlopeFactor.SlopeApplyTypeEnum slopeApplyType, SlopeFactor.HipValleyEnum hipValley)
		{
			this.InternalValue = internalValue;
			this.SlopeType = slopeType;
			this.SlopeApplyType = slopeApplyType;
			this.HipValley = hipValley;
		}

		public double Apply(double value, double deltaX = 0.0, double deltaY = 0.0)
		{
			if (this.InternalValue > 0.0)
			{
				switch (this.SlopeApplyType)
				{
				case SlopeFactor.SlopeApplyTypeEnum.applyOnElevation:
					if (this.HipValley != SlopeFactor.HipValleyEnum.hipValleyEnabled)
					{
						double num = Math.Abs(Math.Sin(Utilities.DegreeToRadian(this.InternalValue)));
						double num2 = 1.0 / num;
						value = num2 * value;
					}
					else
					{
						int slope = this.GetSlope();
						double a = 1.0;
						switch (this.SlopeType)
						{
						case SlopeFactor.SlopeTypeEnum.slopeTypePercentage:
						{
							double num3 = (double)slope;
							a = Math.Atan((double)slope / num3);
							break;
						}
						case SlopeFactor.SlopeTypeEnum.slopeTypeDegree:
							a = Math.Abs(Utilities.DegreeToRadian(this.InternalValue));
							break;
						case SlopeFactor.SlopeTypeEnum.slopeType12:
						{
							double num3 = 12.0;
							a = Math.Atan((double)slope / num3);
							break;
						}
						}
						double x = 1.0 / Math.Sin(a) * deltaY;
						double num4 = Math.Pow(x, 2.0);
						double num5 = Math.Pow(deltaX, 2.0);
						value = Math.Sqrt(num4 + num5);
					}
					break;
				case SlopeFactor.SlopeApplyTypeEnum.applyOnPlan:
					if (this.HipValley != SlopeFactor.HipValleyEnum.hipValleyEnabled)
					{
						double num6 = Math.Abs(Math.Cos(Utilities.DegreeToRadian(this.InternalValue)));
						double num2 = 1.0 / num6;
						value = num2 * value;
					}
					else
					{
						int slope2 = this.GetSlope();
						double num7 = Math.Pow(value, 2.0);
						double x2 = 1.0;
						switch (this.SlopeType)
						{
						case SlopeFactor.SlopeTypeEnum.slopeTypePercentage:
						{
							double num8 = 141.4;
							x2 = (double)slope2 / num8 * value;
							break;
						}
						case SlopeFactor.SlopeTypeEnum.slopeTypeDegree:
						{
							double num8 = Math.Abs(Math.Tan(Utilities.DegreeToRadian(this.InternalValue)) / 1.41214);
							x2 = value * num8;
							break;
						}
						case SlopeFactor.SlopeTypeEnum.slopeType12:
						{
							double num8 = 16.97;
							x2 = (double)slope2 / num8 * value;
							break;
						}
						}
						double num9 = Math.Pow(x2, 2.0);
						value = Math.Sqrt(num7 + num9);
					}
					break;
				}
			}
			return value;
		}

		public SlopeFactor(SlopeFactor.HipValleyEnum hipValley = SlopeFactor.HipValleyEnum.hipValleyUnavailable)
		{
			this.hipValley = hipValley;
			this.slopeType = (SlopeFactor.SlopeTypeEnum)Settings.Default.SlopeType;
		}

		public override string ToString()
		{
			return this.GetSlopeLongString();
		}

		private double internalValue;

		private SlopeFactor.SlopeApplyTypeEnum slopeApplyType = SlopeFactor.SlopeApplyTypeEnum.applyOnPlan;

		private SlopeFactor.SlopeTypeEnum slopeType = SlopeFactor.SlopeTypeEnum.slopeTypeDegree;

		private SlopeFactor.HipValleyEnum hipValley = SlopeFactor.HipValleyEnum.hipValleyUnavailable;

		public enum SlopeApplyTypeEnum
		{
			applyOnElevation,
			applyOnPlan,
			slopeApplyTypeEnumCount
		}

		public enum SlopeTypeEnum
		{
			slopeTypePercentage,
			slopeTypeDegree,
			slopeType12,
			slopeTypeEnumCount
		}

		public enum HipValleyEnum
		{
			hipValleyDisabled,
			hipValleyEnabled,
			hipValleyUnavailable,
			hipValleyEnumCount
		}
	}
}
