using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace QuoterPlan
{
	public class FillBrushes
	{
		public static Brush SetCurrentBrush(FillBrushes.BrushType _bType)
		{
			Brush result = null;
			switch (_bType)
			{
			case FillBrushes.BrushType.Brown:
				result = FillBrushes.BrownBrush();
				break;
			case FillBrushes.BrushType.Aqua:
				result = FillBrushes.AquaBrush();
				break;
			case FillBrushes.BrushType.GrayDivot:
				result = FillBrushes.GrayDivotBrush();
				break;
			case FillBrushes.BrushType.RedDiag:
				result = FillBrushes.RedDiagBrush();
				break;
			case FillBrushes.BrushType.ConfettiGreen:
				result = FillBrushes.ConfettiBrush();
				break;
			}
			return result;
		}

		private static Brush BrownBrush()
		{
			return new SolidBrush(Color.Brown);
		}

		private static Brush AquaBrush()
		{
			return new SolidBrush(Color.Aqua);
		}

		private static Brush GrayDivotBrush()
		{
			return new HatchBrush(HatchStyle.Divot, Color.Gray, Color.Gainsboro);
		}

		private static Brush RedDiagBrush()
		{
			return new HatchBrush(HatchStyle.ForwardDiagonal, Color.Red, Color.Yellow);
		}

		private static Brush ConfettiBrush()
		{
			return new HatchBrush(HatchStyle.LargeConfetti, Color.Green, Color.White);
		}

		public FillBrushes()
		{
		}

		public enum BrushType
		{
			Brown,
			Aqua,
			GrayDivot,
			RedDiag,
			ConfettiGreen,
			NoBrush,
			NumberOfBrushes
		}
	}
}
