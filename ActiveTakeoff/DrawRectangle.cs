using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace QuoterPlan
{
	[Serializable]
	public class DrawRectangle : DrawObject
	{
		public Point Location
		{
			get
			{
				return this.Rectangle.Location;
			}
		}

		public Size Size
		{
			get
			{
				return this.Rectangle.Size;
			}
		}

		protected Rectangle Rectangle
		{
			get
			{
				return this.rectangle;
			}
			set
			{
				this.rectangle = value;
			}
		}

		protected double LastArea
		{
			get
			{
				return this.lastArea;
			}
			set
			{
				this.lastArea = value;
			}
		}

		public override DrawObject Clone()
		{
			DrawRectangle drawRectangle = new DrawRectangle();
			drawRectangle.Rectangle = new Rectangle(this.Rectangle.Location, this.Rectangle.Size);
			drawRectangle.DisplayName = new Utilities.DisplayName(drawRectangle, "");
			base.FillDrawObjectFields(drawRectangle);
			return drawRectangle;
		}

		public override Region Region(int offsetX, int offsetY, float zoomFactor)
		{
			Region region = new Region();
			offsetX -= (int)((float)base.DrawArea.DrawingBoard.HorizontalOffset / zoomFactor);
			offsetY -= (int)((float)base.DrawArea.DrawingBoard.VerticalOffset / zoomFactor);
			Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(this.Rectangle);
			normalizedRectangle.X -= offsetX;
			normalizedRectangle.Y -= offsetY;
			normalizedRectangle.X = (int)((float)normalizedRectangle.X * zoomFactor);
			normalizedRectangle.Y = (int)((float)normalizedRectangle.Y * zoomFactor);
			normalizedRectangle.Width = (int)((float)normalizedRectangle.Width * zoomFactor);
			normalizedRectangle.Height = (int)((float)normalizedRectangle.Height * zoomFactor);
			normalizedRectangle.Inflate(new Size(10 + base.PenWidth, 10 + base.PenWidth));
			region.MakeEmpty();
			region.Union(normalizedRectangle);
			for (int i = 1; i <= this.HandleCount; i++)
			{
				Rectangle handleRectangle = this.GetHandleRectangle(i, offsetX, offsetY);
				handleRectangle.X = (int)((float)handleRectangle.X * zoomFactor);
				handleRectangle.Y = (int)((float)handleRectangle.Y * zoomFactor);
				handleRectangle.Width = (int)((float)handleRectangle.Width * zoomFactor);
				handleRectangle.Height = (int)((float)handleRectangle.Height * zoomFactor);
				handleRectangle.Inflate(new Size(base.PenWidth, base.PenWidth));
				region.Union(handleRectangle);
			}
			return region;
		}

		public DrawRectangle()
		{
			this.SetRectangle(0, 0, 1, 1);
		}

		public override Rectangle BoundingRectangle
		{
			get
			{
				return DrawRectangle.GetNormalizedRectangle(this.Rectangle);
			}
		}

		public override Point Center
		{
			get
			{
				return this.ComputeCenter();
			}
		}

		private Point ComputeCenter()
		{
			return new Point(this.rectangle.X + this.rectangle.Width / 2, this.rectangle.Y + this.rectangle.Height / 2);
		}

		protected double ComputeArea()
		{
			return (double)(this.rectangle.Width * this.rectangle.Height);
		}

		public DrawRectangle(int x, int y, int width, int height, PointF offset, string name, string comment)
		{
			base.Offset = offset;
			this.rectangle.X = x;
			this.rectangle.Y = y;
			this.rectangle.Width = width;
			this.rectangle.Height = height;
			base.Name = name;
			base.Comment = comment;
		}

		public DrawRectangle(int x, int y, int width, int height, PointF offset, string name, string comment, Color lineColor, Color fillColor, int opacity)
		{
			base.Offset = offset;
			this.rectangle.X = x;
			this.rectangle.Y = y;
			this.rectangle.Width = width;
			this.rectangle.Height = height;
			base.Color = lineColor;
			base.FillColor = fillColor;
			base.Opacity = opacity;
			base.PenWidth = -1;
			base.Name = name;
			base.Comment = comment;
		}

		public DrawRectangle(int x, int y, int width, int height, PointF offset, string name, string comment, Color lineColor, Color fillColor, int opacity, bool filled)
		{
			base.Offset = offset;
			this.rectangle.X = x;
			this.rectangle.Y = y;
			this.rectangle.Width = width;
			this.rectangle.Height = height;
			base.Color = lineColor;
			base.FillColor = fillColor;
			base.Opacity = opacity;
			base.Filled = filled;
			base.PenWidth = -1;
			base.Name = name;
			base.Comment = comment;
		}

		public DrawRectangle(int x, int y, int width, int height, PointF offset, string name, string comment, DrawingPens.PenType pType, Color fillColor, int opacity, bool filled)
		{
			base.Offset = offset;
			this.rectangle.X = x;
			this.rectangle.Y = y;
			this.rectangle.Width = width;
			this.rectangle.Height = height;
			base.DrawPen = DrawingPens.SetCurrentPen(pType);
			base.PenType = pType;
			base.FillColor = fillColor;
			base.Opacity = opacity;
			base.Filled = filled;
			base.Name = name;
			base.Comment = comment;
		}

		public DrawRectangle(int x, int y, int width, int height, PointF offset, string name, string comment, Color lineColor, Color fillColor, int opacity, bool filled, int lineWidth)
		{
			base.Offset = offset;
			this.rectangle.X = x;
			this.rectangle.Y = y;
			this.rectangle.Width = width;
			this.rectangle.Height = height;
			base.Color = lineColor;
			base.FillColor = fillColor;
			base.Opacity = opacity;
			base.Filled = filled;
			base.PenWidth = lineWidth;
			base.Name = name;
			base.Comment = comment;
		}

		public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
		{
			Brush brush = new SolidBrush(Color.FromArgb(base.Opacity, base.FillColor));
			Pen pen;
			if (base.DrawPen == null)
			{
				pen = new Pen(Color.FromArgb(base.Opacity + 30, base.Color), (float)base.PenWidth);
			}
			else
			{
				pen = (Pen)base.DrawPen.Clone();
			}
			GraphicsPath graphicsPath = new GraphicsPath();
			Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(this.Rectangle);
			normalizedRectangle.X -= offsetX;
			normalizedRectangle.Y -= offsetY;
			this.lastArea = this.ComputeArea();
			graphicsPath.AddRectangle(normalizedRectangle);
			if (base.Rotation != 0)
			{
				RectangleF bounds = graphicsPath.GetBounds();
				Matrix matrix = new Matrix();
				matrix.RotateAt((float)base.Rotation, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
				graphicsPath.Transform(matrix);
			}
			g.DrawPath(pen, graphicsPath);
			if (base.Filled)
			{
				g.FillPath(brush, graphicsPath);
			}
			graphicsPath.Dispose();
			pen.Dispose();
			brush.Dispose();
		}

		protected void SetRectangle(int x, int y, int width, int height)
		{
			this.rectangle.X = x;
			this.rectangle.Y = y;
			this.rectangle.Width = width;
			this.rectangle.Height = height;
		}

		public override int ActiveHandleCount
		{
			get
			{
				return 8;
			}
		}

		public override int HandleCount
		{
			get
			{
				return 8;
			}
		}

		public override int ConnectionCount
		{
			get
			{
				return this.HandleCount;
			}
		}

		public override Point GetConnection(int connectionNumber)
		{
			return this.GetHandle(connectionNumber);
		}

		public override Point GetHandle(int handleNumber)
		{
			int num = this.rectangle.X + this.rectangle.Width / 2;
			int num2 = this.rectangle.Y + this.rectangle.Height / 2;
			int x = this.rectangle.X;
			int y = this.rectangle.Y;
			switch (handleNumber)
			{
			case 1:
				x = this.rectangle.X;
				y = this.rectangle.Y;
				break;
			case 2:
				x = num;
				y = this.rectangle.Y;
				break;
			case 3:
				x = this.rectangle.Right;
				y = this.rectangle.Y;
				break;
			case 4:
				x = this.rectangle.Right;
				y = num2;
				break;
			case 5:
				x = this.rectangle.Right;
				y = this.rectangle.Bottom;
				break;
			case 6:
				x = num;
				y = this.rectangle.Bottom;
				break;
			case 7:
				x = this.rectangle.X;
				y = this.rectangle.Bottom;
				break;
			case 8:
				x = this.rectangle.X;
				y = num2;
				break;
			}
			return new Point(x, y);
		}

		public override int GetHandleSize()
		{
			if (this.lastArea >= 1600.0)
			{
				return 4;
			}
			return 2;
		}

		public override int HitTest(Point point, int offsetX, int offsetY)
		{
			if (this.Selected)
			{
				for (int i = 1; i <= this.HandleCount; i++)
				{
					if (this.GetHandleRectangle(i, offsetX, offsetY).Contains(point))
					{
						return i;
					}
				}
			}
			if (this.PointInObject(point, offsetX, offsetY))
			{
				return 0;
			}
			return -1;
		}

		protected override bool PointInObject(Point point, int offsetX, int offsetY)
		{
			Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(this.Rectangle);
			normalizedRectangle.X -= offsetX;
			normalizedRectangle.Y -= offsetY;
			return normalizedRectangle.Contains(point);
		}

		public override Cursor GetHandleCursor(int handleNumber)
		{
			switch (handleNumber)
			{
			case 1:
				return Cursors.SizeNWSE;
			case 2:
				return Cursors.SizeNS;
			case 3:
				return Cursors.SizeNESW;
			case 4:
				return Cursors.SizeWE;
			case 5:
				return Cursors.SizeNWSE;
			case 6:
				return Cursors.SizeNS;
			case 7:
				return Cursors.SizeNESW;
			case 8:
				return Cursors.SizeWE;
			default:
				return Cursors.Default;
			}
		}

		public override void MoveHandleTo(Point point, int handleNumber)
		{
			int num = this.Rectangle.Left;
			int num2 = this.Rectangle.Top;
			int num3 = this.Rectangle.Right;
			int num4 = this.Rectangle.Bottom;
			switch (handleNumber)
			{
			case 1:
				num = point.X + (int)base.DrawingBoard.Origin.X;
				num2 = point.Y + (int)base.DrawingBoard.Origin.Y;
				break;
			case 2:
				num2 = point.Y + (int)base.DrawingBoard.Origin.Y;
				break;
			case 3:
				num3 = point.X + (int)base.DrawingBoard.Origin.X;
				num2 = point.Y + (int)base.DrawingBoard.Origin.Y;
				break;
			case 4:
				num3 = point.X + (int)base.DrawingBoard.Origin.X;
				break;
			case 5:
				num3 = point.X + (int)base.DrawingBoard.Origin.X;
				num4 = point.Y + (int)base.DrawingBoard.Origin.Y;
				break;
			case 6:
				num4 = point.Y + (int)base.DrawingBoard.Origin.Y;
				break;
			case 7:
				num = point.X + (int)base.DrawingBoard.Origin.X;
				num4 = point.Y + (int)base.DrawingBoard.Origin.Y;
				break;
			case 8:
				num = point.X + (int)base.DrawingBoard.Origin.X;
				break;
			}
			base.Dirty = true;
			this.SetRectangle(num, num2, num3 - num, num4 - num2);
		}

		public override bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
		{
			Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(this.Rectangle);
			normalizedRectangle.X -= offsetX;
			normalizedRectangle.Y -= offsetY;
			normalizedRectangle.Inflate(new Size(base.PenWidth, base.PenWidth));
			return normalizedRectangle.IntersectsWith(rectangle);
		}

		public override void Move(int deltaX, int deltaY)
		{
			this.rectangle.X = this.rectangle.X + deltaX;
			this.rectangle.Y = this.rectangle.Y + deltaY;
			base.Dirty = true;
		}

		public override void Dump()
		{
			base.Dump();
			Trace.WriteLine("rectangle.X = " + this.rectangle.X.ToString(CultureInfo.InvariantCulture));
			Trace.WriteLine("rectangle.Y = " + this.rectangle.Y.ToString(CultureInfo.InvariantCulture));
			Trace.WriteLine("rectangle.Width = " + this.rectangle.Width.ToString(CultureInfo.InvariantCulture));
			Trace.WriteLine("rectangle.Height = " + this.rectangle.Height.ToString(CultureInfo.InvariantCulture));
		}

		public override void Normalize()
		{
			this.rectangle = DrawRectangle.GetNormalizedRectangle(this.rectangle);
		}

		public static Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
		{
			if (x2 < x1)
			{
				int num = x2;
				x2 = x1;
				x1 = num;
			}
			if (y2 < y1)
			{
				int num2 = y2;
				y2 = y1;
				y1 = num2;
			}
			return new Rectangle(x1, y1, x2 - x1, y2 - y1);
		}

		public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
		{
			return DrawRectangle.GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
		}

		public static Rectangle GetNormalizedRectangle(Rectangle r)
		{
			return DrawRectangle.GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
		}

		public override void Scale(float scaleX, float scaleY)
		{
			this.rectangle.X = (int)((float)this.rectangle.X * scaleX);
			this.rectangle.Y = (int)((float)this.rectangle.Y * scaleY);
			this.rectangle.Width = (int)((float)this.rectangle.Width * scaleX);
			this.rectangle.Height = (int)((float)this.rectangle.Height * scaleY);
		}

		private Rectangle rectangle;

		private double lastArea;
	}
}
