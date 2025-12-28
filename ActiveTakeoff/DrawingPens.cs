using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace QuoterPlan
{
	public class DrawingPens
	{
		public static Pen SetCurrentPen(DrawingPens.PenType _penType)
		{
			Pen result;
			switch (_penType)
			{
			case DrawingPens.PenType.Generic:
				result = null;
				break;
			case DrawingPens.PenType.RedPen:
				result = DrawingPens.RedPen();
				break;
			case DrawingPens.PenType.BluePen:
				result = DrawingPens.BluePen();
				break;
			case DrawingPens.PenType.GreenPen:
				result = DrawingPens.GreenPen();
				break;
			case DrawingPens.PenType.RedDottedPen:
				result = DrawingPens.RedDottedPen();
				break;
			case DrawingPens.PenType.RedDotDashPen:
				result = DrawingPens.RedDotDashPen();
				break;
			case DrawingPens.PenType.DoubleLinePen:
				result = DrawingPens.DoubleLinePen();
				break;
			case DrawingPens.PenType.DashedArrowPen:
				result = DrawingPens.DashedArrowLinePen();
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		private static Pen RedPen()
		{
			return new Pen(Color.Red)
			{
				LineJoin = LineJoin.Round,
				Width = 5f
			};
		}

		private static Pen BluePen()
		{
			return new Pen(Color.Blue)
			{
				LineJoin = LineJoin.Round,
				Width = 3f
			};
		}

		private static Pen GreenPen()
		{
			return new Pen(Color.Green)
			{
				LineJoin = LineJoin.Round,
				Width = 7f
			};
		}

		private static Pen RedDottedPen()
		{
			return new Pen(Color.Red)
			{
				LineJoin = LineJoin.Round,
				DashStyle = DashStyle.Dot,
				Width = 3f
			};
		}

		private static Pen RedDotDashPen()
		{
			return new Pen(Color.Red)
			{
				LineJoin = LineJoin.Round,
				Width = 5f,
				DashStyle = DashStyle.DashDot,
				DashCap = DashCap.Round
			};
		}

		private static Pen DoubleLinePen()
		{
			return new Pen(Color.Black)
			{
				CompoundArray = new float[]
				{
					0f,
					0.1f,
					0.2f,
					0.3f,
					0.7f,
					0.8f,
					0.9f,
					1f
				},
				LineJoin = LineJoin.Round,
				Width = 7f
			};
		}

		private static Pen DashedArrowLinePen()
		{
			return new Pen(Color.Red)
			{
				LineJoin = LineJoin.Round,
				Width = 10f,
				DashStyle = DashStyle.Dash,
				EndCap = LineCap.ArrowAnchor,
				DashCap = DashCap.Flat
			};
		}

		public DrawingPens()
		{
		}

		public enum PenType
		{
			Generic,
			RedPen,
			BluePen,
			GreenPen,
			RedDottedPen,
			RedDotDashPen,
			DoubleLinePen,
			DashedArrowPen,
			NumberOfPens
		}
	}
}
