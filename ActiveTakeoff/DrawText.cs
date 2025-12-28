using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace QuoterPlan
{
	public class DrawText
	{
		private void Initialize(string text, Point point, int angle, object tag)
		{
			this.text = text;
			this.point.X = point.X;
			this.point.Y = point.Y;
			this.angle = angle;
			this.tag = tag;
		}

		public DrawText(string text, Point point, int angle)
		{
			this.Initialize(text, point, angle, null);
		}

		public DrawText(string text, Point point, int angle, object tag)
		{
			this.Initialize(text, point, angle, tag);
		}

		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		public Point Point
		{
			get
			{
				return this.point;
			}
			set
			{
				this.point = value;
			}
		}

		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		public Rectangle Rectangle
		{
			get
			{
				return this.rectangle;
			}
		}

		public void Draw(Graphics g, int offsetX, int offsetY, float zoomFactor, int opacity, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh, float defaultFontSize = 12f)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = (printToScreen ? ((imageQuality == MainForm.ImageQualityEnum.QualityHigh) ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel) : TextRenderingHint.ClearTypeGridFit);
			float num = defaultFontSize;
			if (printToScreen)
			{
				if (zoomFactor < 0.35f)
				{
					num -= 2f;
				}
				else if (zoomFactor < 0.75f)
				{
					num -= 1f;
				}
				else if (zoomFactor >= 2f)
				{
					num += 2f;
				}
				else if (zoomFactor >= 1.25f)
				{
					num += 1f;
				}
				num /= Math.Abs(1f - (1f - zoomFactor));
			}
			Font font = Utilities.CreateFont("Tahoma", Utilities.FontSizeInPoints(num), FontStyle.Regular);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			SizeF sizeF = g.MeasureString(this.text, font, new PointF((float)(this.point.X - offsetX), (float)(this.point.Y - offsetY)), stringFormat);
			int num2 = sizeF.ToSize().Width + 6;
			int num3 = sizeF.ToSize().Height + 6;
			this.rectangle = new Rectangle(this.point.X - offsetX - num2 / 2, this.point.Y - offsetY - num3 / 2, num2, num3);
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddRectangle(this.rectangle);
			Matrix matrix = new Matrix();
			matrix.RotateAt((float)this.angle, new Point(this.point.X - offsetX, this.point.Y - offsetY), MatrixOrder.Append);
			graphicsPath.Transform(matrix);
			g.FillPath(new SolidBrush(Color.FromArgb(opacity + 30, Color.LightYellow)), graphicsPath);
			g.DrawPath(new Pen(Color.FromArgb(opacity + 30, Color.Black)), graphicsPath);
			graphicsPath.Dispose();
			if (this.angle != 0)
			{
				GraphicsPath graphicsPath2 = new GraphicsPath();
				graphicsPath2.AddString(this.text, font.FontFamily, (int)font.Style, num, new PointF((float)(this.point.X - offsetX), (float)(this.point.Y - offsetY)), stringFormat);
				graphicsPath2.Transform(matrix);
				if (printToScreen)
				{
					g.FillPath(new SolidBrush(Color.FromArgb(opacity + 30, Color.Black)), graphicsPath2);
				}
				else
				{
					g.FillPath(new SolidBrush(Color.Black), graphicsPath2);
				}
				graphicsPath2.Dispose();
			}
			else if (printToScreen)
			{
				g.DrawString(this.text, font, new SolidBrush(Color.FromArgb(opacity + 30, Color.Black)), new PointF((float)(this.point.X - offsetX), (float)(this.point.Y - offsetY)), stringFormat);
			}
			else
			{
				g.DrawString(this.text, font, Brushes.Black, new PointF((float)(this.point.X - offsetX), (float)(this.point.Y - offsetY)), stringFormat);
			}
			stringFormat.Dispose();
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		private string text;

		private Point point;

		private int angle;

		private object tag;

		private Rectangle rectangle;
	}
}
