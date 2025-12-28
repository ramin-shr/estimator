using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class DrawAngle : DrawPolyLine
	{
		public DrawAngle.AngleTypeEnum AngleType
		{
			[CompilerGenerated]
			get
			{
				return this.<AngleType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AngleType>k__BackingField = value;
			}
		}

		public new double Angle
		{
			get
			{
				double result = 0.0;
				if (base.PointArray.Count >= 3)
				{
					Point a = (Point)base.PointArray[0];
					Point b = (Point)base.PointArray[1];
					Point c = (Point)base.PointArray[2];
					result = base.Angle(a, b, c);
				}
				return result;
			}
		}

		public DrawAngle()
		{
			base.PointArray = new ArrayList();
			base.ZOrder = 0;
			base.FigureCompleted = false;
			base.Initialize();
		}

		public DrawAngle(int x1, int y1, int x2, int y2, PointF offset, string name, Color lineColor, int opacity, int lineWidth, DrawAngle.AngleTypeEnum angleType)
		{
			base.PointArray = new ArrayList();
			base.PointArray.Add(new Point(x1, y1));
			base.PointArray.Add(new Point(x2, y2));
			base.Offset = offset;
			base.Color = lineColor;
			base.Opacity = opacity;
			base.PenWidth = lineWidth;
			base.ZOrder = 0;
			base.Name = name;
			base.FigureCompleted = false;
			this.AngleType = angleType;
			base.Initialize();
		}

		public DrawAngle(int x1, int y1, int x2, int y2, int x3, int y3, PointF offset, string name, Color lineColor, int opacity, int lineWidth, DrawAngle.AngleTypeEnum angleType)
		{
			base.PointArray = new ArrayList();
			base.PointArray.Add(new Point(x1, y1));
			base.PointArray.Add(new Point(x2, y2));
			base.PointArray.Add(new Point(x3, y3));
			base.Offset = offset;
			base.Color = lineColor;
			base.Opacity = opacity;
			base.PenWidth = lineWidth;
			base.ZOrder = 0;
			base.Name = name;
			base.FigureCompleted = true;
			this.AngleType = angleType;
			base.Initialize();
		}

		public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = (printToScreen ? ((imageQuality == MainForm.ImageQualityEnum.QualityHigh) ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel) : TextRenderingHint.ClearTypeGridFit);
			Pen pen;
			if (base.DrawPen == null)
			{
				pen = new Pen(Color.FromArgb(base.Opacity + 30, base.Color), (float)base.PenWidth);
			}
			else
			{
				pen = (Pen)base.DrawPen.Clone();
			}
			pen.DashStyle = DashStyle.Dash;
			pen.StartCap = LineCap.ArrowAnchor;
			pen.EndCap = LineCap.ArrowAnchor;
			base.ClearTextArray();
			if (base.PointArray.Count >= 3)
			{
				string text = base.DrawArea.ToAngleString(this.Angle, this.AngleType);
				DrawText value = new DrawText(text, this.Center, 0);
				base.TextArray.Insert(0, value);
			}
			GraphicsPath graphicsPath = new GraphicsPath();
			for (int i = 0; i < base.PointArray.Count - 1; i++)
			{
				Point pt = (Point)base.PointArray[i];
				Point pt2 = (Point)base.PointArray[i + 1];
				pt.X -= offsetX;
				pt.Y -= offsetY;
				pt2.X -= offsetX;
				pt2.Y -= offsetY;
				graphicsPath.AddLine(pt, pt2);
			}
			try
			{
				g.DrawPath(pen, graphicsPath);
			}
			catch
			{
			}
			graphicsPath.Dispose();
			pen.Dispose();
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		public override void DrawText(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh, float defaultFontSize = 12f)
		{
			if (!base.ShowMeasure || base.TextArray.Count == 0)
			{
				return;
			}
			if (base.DrawingBoard.ZoomFactor >= 0.15)
			{
				((DrawText)base.TextArray[0]).Draw(g, offsetX, offsetY, (float)base.DrawingBoard.ZoomFactor, base.Opacity, printToScreen, imageQuality, defaultFontSize);
			}
			base.TextDirty = false;
		}

		public override int GetHandleSize()
		{
			return 4;
		}

		public override DrawObject Clone()
		{
			DrawAngle drawAngle = new DrawAngle();
			drawAngle.PointArray = new ArrayList();
			for (int i = 0; i < base.PointArray.Count; i++)
			{
				Point point = (Point)base.PointArray[i];
				drawAngle.PointArray.Add(new Point(point.X, point.Y));
			}
			drawAngle.CloseFigure = false;
			drawAngle.FigureCompleted = true;
			drawAngle.AngleType = this.AngleType;
			drawAngle.DisplayName = new Utilities.DisplayName(drawAngle, "");
			base.FillDrawObjectFields(drawAngle);
			return drawAngle;
		}

		[CompilerGenerated]
		private DrawAngle.AngleTypeEnum <AngleType>k__BackingField;

		public enum AngleTypeEnum
		{
			AngleDegree,
			AngleSlope,
			AngleEnumCount
		}
	}
}
