using System;
using System.Collections.Generic;
using System.Globalization;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class UnitScale
	{
		public UnitScale()
		{
			this.SetScale(0f, UnitScale.UnitSystem.imperial, UnitScale.DefaultUnitPrecision(), false);
		}

		public UnitScale(float scale, UnitScale.UnitSystem scaleSystemType, UnitScale.UnitPrecision precision, bool setManually)
		{
			this.SetScale(scale, scaleSystemType, precision, setManually);
		}

		public void SetScale(float scale, UnitScale.UnitSystem scaleSystemType, UnitScale.UnitPrecision precision, bool setManually)
		{
			this.dirty = false;
			this.scale = scale;
			this.scaleSystemType = scaleSystemType;
			this.currentSystemType = scaleSystemType;
			this.precision = precision;
			this.setManually = setManually;
		}

		public bool Engineering
		{
			get
			{
				return this.engineering && this.scaleSystemType == UnitScale.UnitSystem.imperial;
			}
			set
			{
				this.engineering = value;
			}
		}

		public bool Dirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		public static UnitScale.UnitSystem CastUnitSystem(int value)
		{
			if (value < 0 || value > 1)
			{
				return UnitScale.UnitSystem.imperial;
			}
			return (UnitScale.UnitSystem)value;
		}

		public static UnitScale.UnitPrecision CastUnitPrecision(int value)
		{
			if (value < 0 || value > 3)
			{
				return UnitScale.UnitPrecision.precision16;
			}
			return (UnitScale.UnitPrecision)value;
		}

		public static UnitScale.UnitSystem DefaultUnitSystem()
		{
			string a = CultureInfo.CurrentCulture.Name.ToLower();
			if (Settings.Default.DefaultSystemType >= 0 && Settings.Default.DefaultSystemType <= 1)
			{
				return (UnitScale.UnitSystem)Settings.Default.DefaultSystemType;
			}
			if (!(a == "en-us") && !(a == "en-ca") && !(a == "fr-ca"))
			{
				return UnitScale.UnitSystem.metric;
			}
			return UnitScale.UnitSystem.imperial;
		}

		public static UnitScale.UnitPrecision DefaultUnitPrecision()
		{
			if (Settings.Default.DefaultPrecision >= 0 && Settings.Default.DefaultPrecision <= 3)
			{
				return (UnitScale.UnitPrecision)Settings.Default.DefaultPrecision;
			}
			return UnitScale.UnitPrecision.precision32;
		}

		public float Scale
		{
			get
			{
				return this.scale;
			}
		}

		public float ScaleFactor
		{
			get
			{
				if (this.setManually)
				{
					return this.scale;
				}
				if (this.scaleSystemType != UnitScale.UnitSystem.imperial)
				{
					return this.referenceDpiX / (this.scale * 2.54f);
				}
				if (this.Engineering)
				{
					return this.referenceDpiX / (this.scale * 12f);
				}
				return this.referenceDpiX / (12f / this.scale);
			}
		}

		public UnitScale.UnitSystem CurrentSystemType
		{
			get
			{
				return this.currentSystemType;
			}
			set
			{
				this.currentSystemType = value;
			}
		}

		public UnitScale.UnitSystem ScaleSystemType
		{
			get
			{
				return this.scaleSystemType;
			}
		}

		public UnitScale.UnitPrecision Precision
		{
			get
			{
				return this.precision;
			}
			set
			{
				this.precision = value;
			}
		}

		public bool SetManually
		{
			get
			{
				return this.setManually;
			}
			set
			{
				this.setManually = value;
			}
		}

		public bool MustSetManually
		{
			get
			{
				return this.mustSetManually;
			}
			set
			{
				this.mustSetManually = value;
			}
		}

		public int ReferenceDistance
		{
			get
			{
				return this.referenceDistance;
			}
			set
			{
				this.referenceDistance = value;
			}
		}

		public float ReferenceDpiX
		{
			get
			{
				return this.referenceDpiX;
			}
			set
			{
				this.referenceDpiX = ((value <= 0f) ? 96f : value);
			}
		}

		public float ReferenceDpiY
		{
			get
			{
				return this.referenceDpiY;
			}
			set
			{
				this.referenceDpiY = ((value <= 0f) ? 96f : value);
			}
		}

		public bool StandardScaleDisable
		{
			get
			{
				return this.standardScaleDisable;
			}
			set
			{
				this.standardScaleDisable = value;
			}
		}

		public UnitScale Duplicate()
		{
			return new UnitScale(this.scale, this.scaleSystemType, this.precision, this.setManually)
			{
				CurrentSystemType = this.CurrentSystemType,
				MustSetManually = this.MustSetManually,
				Precision = this.Precision,
				ReferenceDistance = this.ReferenceDistance,
				ReferenceDpiX = this.ReferenceDpiX,
				ReferenceDpiY = this.ReferenceDpiY,
				StandardScaleDisable = this.StandardScaleDisable
			};
		}

		public double Round(double value)
		{
			double num = Math.Round(value, (int)this.Precision);
			if (value <= 0.0 || num != 0.0)
			{
				return num;
			}
			return Math.Round((value - Math.Truncate(value)) * 10.0, 0) / 10.0;
		}

		private float ToCentimeters(float value)
		{
			return value / this.ScaleFactor * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 2.54f : 1f);
		}

		private float ToMeters(int value)
		{
			return (float)value / this.ScaleFactor * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 0.0254f : 0.01f);
		}

		private float ToSquareCentimeters(int value)
		{
			return (float)value / (this.ScaleFactor * this.ScaleFactor) * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 6.4516f : 1f);
		}

		private float ToSquareMeters(int value)
		{
			return (float)value / (this.ScaleFactor * this.ScaleFactor) * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 0.00064516f : 0.0001f);
		}

		private float ToSquareInches(int value)
		{
			return (float)value / (this.ScaleFactor * this.ScaleFactor) * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 1f : 0.15500031f);
		}

		private float ToSquareFeet(int value)
		{
			return (float)value / (this.ScaleFactor * this.ScaleFactor) * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 0.0069444445f : 0.0010763911f);
		}

		private float ToInches(int value)
		{
			return (float)value / this.ScaleFactor * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 1f : 0.39370078f);
		}

		private float ToFeet(int value)
		{
			return (float)value / this.ScaleFactor * ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 0.083333336f : 0.0328084f);
		}

		public float ToScale(int value)
		{
			return (float)value / this.ScaleFactor;
		}

		public static double FromMillimetersToInches(double value)
		{
			return value * 0.0393700787;
		}

		public static double FromInchesToMillimeters(double value)
		{
			return Math.Round(value * 25.4, 0);
		}

		public static double FromMetersToFeet(double value)
		{
			return value * 3.2808399;
		}

		public static double FromFeetToMeters(double value)
		{
			return value * 0.3048;
		}

		public static double FromInchesToFeet(double value)
		{
			return value * 0.0833333333;
		}

		public static double FromFeetToInches(double value)
		{
			return value * 12.0;
		}

		public static double FromMillimetersToMeters(double value)
		{
			return value * 0.001;
		}

		public static double FromCentimetersToMeters(double value)
		{
			return value * 0.01;
		}

		public static double FromMetersToMillimeters(double value)
		{
			return value * 1000.0;
		}

		public static double FromMetersToInches(double value)
		{
			return value * 39.3700787;
		}

		public static double FromFeetToMillimeters(double value)
		{
			return value * 304.8;
		}

		public static double FromSquareFeetToSquareMeters(double value)
		{
			return value / 10.764;
		}

		public static double FromSquareMetersToSquareFeet(double value)
		{
			return value * 10.764;
		}

		public static double FromCubicFeetToCubicMeters(double value)
		{
			return value / 35.315;
		}

		public static double FromCubicMetersToCubicFeet(double value)
		{
			return value * 35.315;
		}

		public static string ToFractionString(float value, UnitScale.UnitPrecision precision)
		{
			switch (precision)
			{
			case UnitScale.UnitPrecision.precision8:
				if (value > 0f && (double)value <= 0.125)
				{
					return "1/8";
				}
				if ((double)value > 0.125 && (double)value <= 0.25)
				{
					return "1/4";
				}
				if ((double)value > 0.25 && (double)value <= 0.375)
				{
					return "3/8";
				}
				if ((double)value > 0.375 && (double)value <= 0.5)
				{
					return "1/2";
				}
				if ((double)value > 0.5 && (double)value <= 0.625)
				{
					return "5/8";
				}
				if ((double)value > 0.625 && (double)value <= 0.75)
				{
					return "3/4";
				}
				if ((double)value > 0.75 && (double)value <= 0.875)
				{
					return "7/8";
				}
				if ((double)value > 0.875)
				{
					return "1";
				}
				if (value == 0f)
				{
					return "";
				}
				break;
			case UnitScale.UnitPrecision.precision16:
				if (value > 0f && (double)value <= 0.0625)
				{
					return "1/16";
				}
				if ((double)value > 0.0625 && (double)value <= 0.125)
				{
					return "1/8";
				}
				if ((double)value > 0.125 && (double)value <= 0.1875)
				{
					return "3/16";
				}
				if ((double)value > 0.1875 && (double)value <= 0.25)
				{
					return "1/4";
				}
				if ((double)value > 0.25 && (double)value <= 0.3125)
				{
					return "5/16";
				}
				if ((double)value > 0.3125 && (double)value <= 0.375)
				{
					return "3/8";
				}
				if ((double)value > 0.375 && (double)value <= 0.4375)
				{
					return "7/16";
				}
				if ((double)value > 0.4375 && (double)value <= 0.5)
				{
					return "1/2";
				}
				if ((double)value > 0.5 && (double)value <= 0.5625)
				{
					return "9/16";
				}
				if ((double)value > 0.5625 && (double)value <= 0.625)
				{
					return "5/8";
				}
				if ((double)value > 0.625 && (double)value <= 0.6875)
				{
					return "11/16";
				}
				if ((double)value > 0.6875 && (double)value <= 0.75)
				{
					return "3/4";
				}
				if ((double)value > 0.75 && (double)value <= 0.8125)
				{
					return "13/16";
				}
				if ((double)value > 0.8125 && (double)value <= 0.875)
				{
					return "7/8";
				}
				if ((double)value > 0.875 && (double)value <= 0.9375)
				{
					return "15/16";
				}
				if ((double)value > 0.9375)
				{
					return "1";
				}
				if (value == 0f)
				{
					return "";
				}
				break;
			case UnitScale.UnitPrecision.precision32:
				if (value > 0f && (double)value <= 0.03125)
				{
					return "1/32";
				}
				if ((double)value > 0.03125 && (double)value <= 0.0625)
				{
					return "1/16";
				}
				if ((double)value > 0.0625 && (double)value <= 0.09375)
				{
					return "3/32";
				}
				if ((double)value > 0.09375 && (double)value <= 0.125)
				{
					return "1/8";
				}
				if ((double)value > 0.125 && (double)value <= 0.15625)
				{
					return "5/32";
				}
				if ((double)value > 0.15625 && (double)value <= 0.1875)
				{
					return "3/16";
				}
				if ((double)value > 0.1875 && (double)value <= 0.21875)
				{
					return "7/32";
				}
				if ((double)value > 0.21875 && (double)value <= 0.25)
				{
					return "1/4";
				}
				if ((double)value > 0.25 && (double)value <= 0.28125)
				{
					return "9/32";
				}
				if ((double)value > 0.28125 && (double)value <= 0.3125)
				{
					return "5/16";
				}
				if ((double)value > 0.3125 && (double)value <= 0.34375)
				{
					return "11/32";
				}
				if ((double)value > 0.34375 && (double)value <= 0.375)
				{
					return "3/8";
				}
				if ((double)value > 0.375 && (double)value <= 0.40625)
				{
					return "13/32";
				}
				if ((double)value > 0.40625 && (double)value <= 0.4375)
				{
					return "7/16";
				}
				if ((double)value > 0.4375 && (double)value <= 0.46875)
				{
					return "15/32";
				}
				if ((double)value > 0.46875 && (double)value <= 0.5)
				{
					return "1/2";
				}
				if ((double)value > 0.5 && (double)value <= 0.53125)
				{
					return "17/32";
				}
				if ((double)value > 0.53125 && (double)value <= 0.5625)
				{
					return "9/16";
				}
				if ((double)value > 0.5625 && (double)value <= 0.59375)
				{
					return "19/32";
				}
				if ((double)value > 0.59375 && (double)value <= 0.625)
				{
					return "5/8";
				}
				if ((double)value > 0.625 && (double)value <= 0.65625)
				{
					return "21/32";
				}
				if ((double)value > 0.65625 && (double)value <= 0.6875)
				{
					return "11/16";
				}
				if ((double)value > 0.6875 && (double)value <= 0.71875)
				{
					return "23/32";
				}
				if ((double)value > 0.71875 && (double)value <= 0.75)
				{
					return "3/4";
				}
				if ((double)value > 0.75 && (double)value <= 0.78125)
				{
					return "25/32";
				}
				if ((double)value > 0.78125 && (double)value <= 0.8125)
				{
					return "13/16";
				}
				if ((double)value > 0.8125 && (double)value <= 0.84375)
				{
					return "27/32";
				}
				if ((double)value > 0.84375 && (double)value <= 0.875)
				{
					return "7/8";
				}
				if ((double)value > 0.875 && (double)value <= 0.90625)
				{
					return "29/32";
				}
				if ((double)value > 0.90625 && (double)value <= 0.9375)
				{
					return "15/16";
				}
				if ((double)value > 0.9375 && (double)value <= 0.96875)
				{
					return "31/32";
				}
				if ((double)value > 0.96875)
				{
					return "1";
				}
				if (value == 0f)
				{
					return "";
				}
				break;
			case UnitScale.UnitPrecision.precision64:
				if (value > 0f && (double)value <= 0.015625)
				{
					return "1/64";
				}
				if ((double)value > 0.015625 && (double)value <= 0.03125)
				{
					return "1/32";
				}
				if ((double)value > 0.03125 && (double)value <= 0.046875)
				{
					return "3/64";
				}
				if ((double)value > 0.046875 && (double)value <= 0.0625)
				{
					return "1/16";
				}
				if ((double)value > 0.0625 && (double)value <= 0.078125)
				{
					return "5/64";
				}
				if ((double)value > 0.078125 && (double)value <= 0.09375)
				{
					return "3/32";
				}
				if ((double)value > 0.09375 && (double)value <= 0.109375)
				{
					return "7/64";
				}
				if ((double)value > 0.109375 && (double)value <= 0.125)
				{
					return "1/8";
				}
				if ((double)value > 0.125 && (double)value <= 0.140625)
				{
					return "9/64";
				}
				if ((double)value > 0.140625 && (double)value <= 0.15625)
				{
					return "5/32";
				}
				if ((double)value > 0.15625 && (double)value <= 0.171875)
				{
					return "11/64";
				}
				if ((double)value > 0.171875 && (double)value <= 0.1875)
				{
					return "3/16";
				}
				if ((double)value > 0.1875 && (double)value <= 0.203125)
				{
					return "13/64";
				}
				if ((double)value > 0.203125 && (double)value <= 0.21875)
				{
					return "7/32";
				}
				if ((double)value > 0.21875 && (double)value <= 0.234375)
				{
					return "15/64";
				}
				if ((double)value > 0.234375 && (double)value <= 0.25)
				{
					return "1/4";
				}
				if ((double)value > 0.25 && (double)value <= 0.265625)
				{
					return "17/64";
				}
				if ((double)value > 0.265625 && (double)value <= 0.28125)
				{
					return "9/32";
				}
				if ((double)value > 0.28125 && (double)value <= 0.296875)
				{
					return "19/64";
				}
				if ((double)value > 0.296875 && (double)value <= 0.3125)
				{
					return "5/16";
				}
				if ((double)value > 0.3125 && (double)value <= 0.328125)
				{
					return "21/64";
				}
				if ((double)value > 0.328125 && (double)value <= 0.34375)
				{
					return "11/32";
				}
				if ((double)value > 0.34375 && (double)value <= 0.359375)
				{
					return "23/64";
				}
				if ((double)value > 0.359375 && (double)value <= 0.375)
				{
					return "3/8";
				}
				if ((double)value > 0.375 && (double)value <= 0.390625)
				{
					return "25/64";
				}
				if ((double)value > 0.390625 && (double)value <= 0.40625)
				{
					return "13/32";
				}
				if ((double)value > 0.40625 && (double)value <= 0.421875)
				{
					return "27/64";
				}
				if ((double)value > 0.421875 && (double)value <= 0.4375)
				{
					return "7/16";
				}
				if ((double)value > 0.4375 && (double)value <= 0.453125)
				{
					return "29/64";
				}
				if ((double)value > 0.453125 && (double)value <= 0.46875)
				{
					return "15/32";
				}
				if ((double)value > 0.46875 && (double)value <= 0.484375)
				{
					return "31/64";
				}
				if ((double)value > 0.484375 && (double)value <= 0.5)
				{
					return "1/2";
				}
				if ((double)value > 0.5 && (double)value <= 0.515625)
				{
					return "33/64";
				}
				if ((double)value > 0.515625 && (double)value <= 0.53125)
				{
					return "17/32";
				}
				if ((double)value > 0.53125 && (double)value <= 0.546875)
				{
					return "35/64";
				}
				if ((double)value > 0.546875 && (double)value <= 0.5625)
				{
					return "9/16";
				}
				if ((double)value > 0.5625 && (double)value <= 0.578125)
				{
					return "37/64";
				}
				if ((double)value > 0.578125 && (double)value <= 0.59375)
				{
					return "19/32";
				}
				if ((double)value > 0.59375 && (double)value <= 0.609375)
				{
					return "39/64";
				}
				if ((double)value > 0.609375 && (double)value <= 0.625)
				{
					return "5/8";
				}
				if ((double)value > 0.625 && (double)value <= 0.640625)
				{
					return "41/64";
				}
				if ((double)value > 0.640625 && (double)value <= 0.65625)
				{
					return "21/32";
				}
				if ((double)value > 0.65625 && (double)value <= 0.671875)
				{
					return "43/64";
				}
				if ((double)value > 0.671875 && (double)value <= 0.6875)
				{
					return "11/16";
				}
				if ((double)value > 0.6875 && (double)value <= 0.703125)
				{
					return "45/64";
				}
				if ((double)value > 0.703125 && (double)value <= 0.71875)
				{
					return "23/32";
				}
				if ((double)value > 0.71875 && (double)value <= 0.734375)
				{
					return "47/64";
				}
				if ((double)value > 0.734375 && (double)value <= 0.75)
				{
					return "3/4";
				}
				if ((double)value > 0.75 && (double)value <= 0.765625)
				{
					return "49/64";
				}
				if ((double)value > 0.765625 && (double)value <= 0.78125)
				{
					return "25/32";
				}
				if ((double)value > 0.78125 && (double)value <= 0.796875)
				{
					return "51/64";
				}
				if ((double)value > 0.796875 && (double)value <= 0.8125)
				{
					return "13/16";
				}
				if ((double)value > 0.8125 && (double)value <= 0.828125)
				{
					return "53/64";
				}
				if ((double)value > 0.828125 && (double)value <= 0.84375)
				{
					return "27/32";
				}
				if ((double)value > 0.84375 && (double)value <= 0.859375)
				{
					return "55/64";
				}
				if ((double)value > 0.859375 && (double)value <= 0.875)
				{
					return "7/8";
				}
				if ((double)value > 0.875 && (double)value <= 0.890625)
				{
					return "57/64";
				}
				if ((double)value > 0.890625 && (double)value <= 0.90625)
				{
					return "29/32";
				}
				if ((double)value > 0.90625 && (double)value <= 0.921875)
				{
					return "59/64";
				}
				if ((double)value > 0.921875 && (double)value <= 0.9375)
				{
					return "15/16";
				}
				if ((double)value > 0.9375 && (double)value <= 0.953125)
				{
					return "61/64";
				}
				if ((double)value > 0.953125 && (double)value <= 0.96875)
				{
					return "31/32";
				}
				if ((double)value > 0.96875 && (double)value <= 0.984375)
				{
					return "63/64";
				}
				if ((double)value > 0.984375)
				{
					return "1";
				}
				if (value == 0f)
				{
					return "";
				}
				break;
			}
			return "";
		}

		public static KeyValuePair<int, float> ToFeetInches(float inches)
		{
			return new KeyValuePair<int, float>((int)inches / 12, inches % 12f);
		}

		private string ToMetricLength(float value, bool roundResult = true)
		{
			float num = value;
			return (roundResult ? this.Round((double)num).ToString() : num.ToString()) + " m";
		}

		private string ToMetricArea(float value, bool roundResult = true)
		{
			float num = value;
			return (roundResult ? this.Round((double)num).ToString() : num.ToString()) + " m²";
		}

		private string ToMetricVolume(float value, bool roundResult = true)
		{
			float num = value;
			return (roundResult ? this.Round((double)num).ToString() : num.ToString()) + " m³";
		}

		private string ToImperialLength(float value, bool shortFormat = false)
		{
			KeyValuePair<int, float> keyValuePair = UnitScale.ToFeetInches(value);
			int num = keyValuePair.Key;
			int num2 = (int)Math.Truncate((double)keyValuePair.Value);
			float value2 = keyValuePair.Value - (float)num2;
			string text = UnitScale.ToFractionString(value2, shortFormat ? UnitScale.UnitPrecision.precision8 : this.Precision);
			if (text == "1")
			{
				num2++;
				if (num2 == 12)
				{
					num2 = 0;
					num++;
				}
				text = "";
			}
			string text2;
			if (shortFormat)
			{
				text2 = ((num * 12 + num2 == 0) ? "" : (num * 12 + num2).ToString());
			}
			else
			{
				text2 = ((num > 0) ? (num.ToString() + "' ") : "");
				text2 += num2.ToString();
			}
			text2 += ((text == "") ? "" : ("-" + text));
			text2 += "\"";
			if (!shortFormat)
			{
				return text2;
			}
			return text2.Replace(" ", string.Empty);
		}

		private string ToImperialArea(float value, bool roundResult = true)
		{
			float num = value;
			return (roundResult ? this.Round((double)num).ToString() : num.ToString()) + " " + Resources.pi_2;
		}

		private string ToImperialVolume(float value, bool roundResult = true)
		{
			float num = value;
			return (roundResult ? this.Round((double)num).ToString() : num.ToString()) + " " + Resources.pi_3;
		}

		public static double ConvertDimensionToValue(string dimension, UnitScale.UnitSystem currentSystem, ref UnitScale.UnitSystem returnedSystem)
		{
			returnedSystem = UnitScale.UnitSystem.undefined;
			string[] array = new string[]
			{
				"mm",
				"cm",
				"m"
			};
			double num2;
			foreach (string text in array)
			{
				int num = dimension.ToLower().IndexOf(text);
				if (num > 0)
				{
					num2 = Utilities.ConvertToDouble(dimension.Substring(0, num).Trim(), -1);
					string a;
					if ((a = text) != null)
					{
						if (!(a == "mm"))
						{
							if (!(a == "cm"))
							{
								if (!(a == "m"))
								{
								}
							}
							else
							{
								num2 = UnitScale.FromCentimetersToMeters(num2);
							}
						}
						else
						{
							num2 = UnitScale.FromMillimetersToMeters(num2);
						}
					}
					returnedSystem = UnitScale.UnitSystem.metric;
					return num2;
				}
			}
			int num3 = dimension.IndexOf("'");
			int num4 = dimension.IndexOf("\"");
			double num5 = 0.0;
			double num6 = 0.0;
			bool flag = num3 > 0 || (num4 > 0 && num4 > num3);
			if (flag)
			{
				if (num3 > 0)
				{
					string[] array3 = dimension.Split(new string[]
					{
						"'"
					}, StringSplitOptions.RemoveEmptyEntries);
					if (array3.GetLength(0) > 0)
					{
						num5 = Utilities.ConvertToDouble(array3[0], -1);
					}
				}
				if (num4 > 0)
				{
					string[] array4 = dimension.Split(new string[]
					{
						"'",
						"\""
					}, StringSplitOptions.RemoveEmptyEntries);
					if (array4.GetLength(0) > ((num3 > 0) ? 1 : 0))
					{
						num6 = Utilities.ConvertToDouble(array4[(num3 > 0) ? 1 : 0], -1);
					}
				}
				returnedSystem = UnitScale.UnitSystem.imperial;
				return num5 + num6 / 12.0;
			}
			num2 = Utilities.ConvertToDouble(dimension, -1);
			if (currentSystem == UnitScale.UnitSystem.metric)
			{
				num2 = UnitScale.FromMillimetersToMeters(num2);
			}
			returnedSystem = currentSystem;
			return num2;
		}

		public string ToLengthStringFromUnitSystem(double value, bool shortFormat = false, bool roundResult = true, bool checkSystemType = true)
		{
			if (this.scale == 0f)
			{
				return Resources.Échelle_non_configurée;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return this.ToMetricLength((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)UnitScale.FromFeetToMeters(value)) : ((float)value), roundResult);
			}
			return this.ToImperialLength((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)UnitScale.FromFeetToInches(value)) : ((float)UnitScale.FromMetersToInches(value)), shortFormat);
		}

		public string ToAreaStringFromUnitSystem(double value, bool roundResult = true)
		{
			if (this.scale == 0f)
			{
				return Resources.Échelle_non_configurée;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return this.ToMetricArea((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value * 0.09290304f) : ((float)value), roundResult);
			}
			return this.ToImperialArea((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value) : ((float)value * 10.76391f), roundResult);
		}

		public string ToCubicStringFromUnitSystem(double value, bool roundResult = true)
		{
			if (this.scale == 0f)
			{
				return Resources.Échelle_non_configurée;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return this.ToMetricVolume((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value * 0.028316846f) : ((float)value), roundResult);
			}
			return this.ToImperialVolume((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value) : ((float)value * 35.314667f), roundResult);
		}

		public double ToLengthFromUnitSystem(double value)
		{
			if (this.scale == 0f)
			{
				return 0.0;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return Utilities.ConvertToDouble((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)UnitScale.FromFeetToMeters(value)) : ((float)value), -1);
			}
			return Utilities.ConvertToDouble((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value) : ((float)UnitScale.FromMetersToFeet(value)), -1);
		}

		public double ToAreaFromUnitSystem(double value)
		{
			if (this.scale == 0f)
			{
				return 0.0;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return Utilities.ConvertToDouble((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value * 0.09290304f) : ((float)value), -1);
			}
			return Utilities.ConvertToDouble((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value) : ((float)value * 10.76391f), -1);
		}

		public double ToCubicFromUnitSystem(double value)
		{
			if (this.scale == 0f)
			{
				return 0.0;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return Utilities.ConvertToDouble((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value * 0.028316846f) : ((float)value), -1);
			}
			return Utilities.ConvertToDouble((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? ((float)value) : ((float)value * 35.314667f), -1);
		}

		public double ToPixelsFromFeet(double value)
		{
			return (double)((float)((double)this.ScaleFactor * value) / ((this.scaleSystemType == UnitScale.UnitSystem.imperial) ? 0.083333336f : 0.0328084f));
		}

		public double ToLength(int value)
		{
			if (this.scale == 0f)
			{
				return 0.0;
			}
			if (this.scaleSystemType != UnitScale.UnitSystem.imperial)
			{
				return Utilities.ConvertToDouble(this.ToMeters(value), -1);
			}
			return Utilities.ConvertToDouble(this.ToFeet(value), -1);
		}

		public double ToArea(int value)
		{
			if (this.scale == 0f)
			{
				return 0.0;
			}
			if (this.scaleSystemType != UnitScale.UnitSystem.imperial)
			{
				return Utilities.ConvertToDouble(this.ToSquareMeters(value), -1);
			}
			return Utilities.ConvertToDouble(this.ToSquareFeet(value), -1);
		}

		public string ToAngleString(double value, DrawAngle.AngleTypeEnum angleType)
		{
			string result = "";
			if (angleType == DrawAngle.AngleTypeEnum.AngleSlope)
			{
				try
				{
					value = Math.Tan(value * 3.141592653589793 / 180.0) * 12.0;
					return this.Round(value).ToString() + " / 12";
				}
				catch
				{
					return Resources.Aucune_pente;
				}
			}
			result = this.Round(value).ToString() + " °";
			return result;
		}

		public string ToLengthString(int value, bool shortFormat = false)
		{
			if (this.scale == 0f)
			{
				return Resources.Échelle_non_configurée;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return this.ToMetricLength(this.ToMeters(value), true);
			}
			return this.ToImperialLength(this.ToInches(value), shortFormat);
		}

		public string ToAreaString(int value)
		{
			if (this.scale == 0f)
			{
				return Resources.Échelle_non_configurée;
			}
			if (this.currentSystemType != UnitScale.UnitSystem.imperial)
			{
				return this.ToMetricArea(this.ToSquareMeters(value), true);
			}
			return this.ToImperialArea(this.ToSquareFeet(value), true);
		}

		public void ConvertStatsToUnitSystem(GroupStats stats, UnitScale.UnitSystem systemType)
		{
			stats.Area = this.ToArea((int)stats.Area);
			stats.Area = ((systemType == this.scaleSystemType) ? stats.Area : ((systemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(stats.Area) : UnitScale.FromSquareFeetToSquareMeters(stats.Area)));
			stats.DeductionArea = this.ToArea((int)stats.DeductionArea);
			stats.DeductionArea = ((systemType == this.scaleSystemType) ? stats.DeductionArea : ((systemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(stats.DeductionArea) : UnitScale.FromSquareFeetToSquareMeters(stats.DeductionArea)));
			stats.Perimeter = this.ToLength((int)stats.Perimeter);
			stats.Perimeter = ((systemType == this.scaleSystemType) ? stats.Perimeter : ((systemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(stats.Perimeter) : UnitScale.FromFeetToMeters(stats.Perimeter)));
			stats.DeductionPerimeter = this.ToLength((int)stats.DeductionPerimeter);
			stats.DeductionPerimeter = ((systemType == this.scaleSystemType) ? stats.DeductionPerimeter : ((systemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(stats.DeductionPerimeter) : UnitScale.FromFeetToMeters(stats.DeductionPerimeter)));
		}

		private bool dirty;

		private float scale;

		private bool setManually;

		private UnitScale.UnitSystem scaleSystemType;

		private UnitScale.UnitSystem currentSystemType;

		private UnitScale.UnitPrecision precision;

		private bool engineering;

		private bool mustSetManually;

		private int referenceDistance;

		private float referenceDpiX = 96f;

		private float referenceDpiY = 96f;

		private bool standardScaleDisable;

		public enum UnitSystem
		{
			metric,
			imperial,
			undefined
		}

		public enum UnitPrecision
		{
			precision8,
			precision16,
			precision32,
			precision64
		}
	}
}
