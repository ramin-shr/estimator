using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
	[Serializable]
	public class DrawNote : DrawRectangle
	{
		public Point StartPoint
		{
			get
			{
				return this.startPoint;
			}
			set
			{
				this.startPoint = value;
			}
		}

		public Point EndPoint
		{
			get
			{
				return this.endPoint;
			}
			set
			{
				this.endPoint = value;
			}
		}

		public bool ResizeContent
		{
			get
			{
				return this.resizeContent;
			}
			set
			{
				this.resizeContent = value;
			}
		}

		private int ValidateFontSize(int fontSize)
		{
			if (fontSize < 8)
			{
				return 52;
			}
			if (fontSize <= 96)
			{
				return fontSize;
			}
			return 96;
		}

		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = this.ValidateFontSize(value);
			}
		}

		public override DrawObject Clone()
		{
			DrawNote drawNote = new DrawNote();
			drawNote.Rectangle = new Rectangle(base.Rectangle.Location, base.Rectangle.Size);
			drawNote.StartPoint = new Point(this.StartPoint.X, this.StartPoint.Y);
			drawNote.EndPoint = Point.Empty;
			drawNote.DisplayName = new Utilities.DisplayName(drawNote, "");
			drawNote.FontSize = this.FontSize;
			drawNote.ResizeContent = true;
			base.FillDrawObjectFields(drawNote);
			return drawNote;
		}

		public override Region Region(int offsetX, int offsetY, float zoomFactor)
		{
			Region region = new Region();
			offsetX -= (int)((float)base.DrawArea.DrawingBoard.HorizontalOffset / zoomFactor);
			offsetY -= (int)((float)base.DrawArea.DrawingBoard.VerticalOffset / zoomFactor);
			Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
			normalizedRectangle.X -= offsetX;
			normalizedRectangle.Y -= offsetY;
			normalizedRectangle.X = (int)((float)normalizedRectangle.X * zoomFactor);
			normalizedRectangle.Y = (int)((float)normalizedRectangle.Y * zoomFactor);
			normalizedRectangle.Width = (int)((float)normalizedRectangle.Width * zoomFactor);
			normalizedRectangle.Height = (int)((float)normalizedRectangle.Height * zoomFactor);
			normalizedRectangle.Inflate(new Size(20 + base.PenWidth, 20 + base.PenWidth));
			region.MakeEmpty();
			region.Union(normalizedRectangle);
			Rectangle normalizedRectangle2 = this.GetNormalizedRectangle();
			normalizedRectangle2.X -= offsetX;
			normalizedRectangle2.Y -= offsetY;
			normalizedRectangle2.X = (int)((float)normalizedRectangle2.X * zoomFactor);
			normalizedRectangle2.Y = (int)((float)normalizedRectangle2.Y * zoomFactor);
			normalizedRectangle2.Width = (int)((float)normalizedRectangle2.Width * zoomFactor);
			normalizedRectangle2.Height = (int)((float)normalizedRectangle2.Height * zoomFactor);
			normalizedRectangle2.Inflate(new Size(10 + base.PenWidth, 10 + base.PenWidth));
			region.Union(normalizedRectangle2);
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

		private Rectangle GetNormalizedRectangle()
		{
			int x;
			int x2;
			if (this.startPoint.X <= this.endPoint.X)
			{
				x = this.startPoint.X;
				x2 = this.endPoint.X;
			}
			else
			{
				x = this.endPoint.X;
				x2 = this.startPoint.X;
			}
			int y;
			int y2;
			if (this.startPoint.Y <= this.endPoint.Y)
			{
				y = this.startPoint.Y;
				y2 = this.endPoint.Y;
			}
			else
			{
				y = this.endPoint.Y;
				y2 = this.startPoint.Y;
			}
			int num = x2 - x;
			int num2 = y2 - y;
			return new Rectangle(x, y, num + 1, num2 + 1);
		}

		public override Rectangle BoundingRectangle
		{
			get
			{
				Point point = Point.Empty;
				Point left = Point.Empty;
				Point empty = Point.Empty;
				ArrayList arrayList = new ArrayList();
				arrayList.Add(new Point(this.StartPoint.X, this.StartPoint.Y));
				arrayList.Add(new Point(base.Rectangle.X, base.Rectangle.Y));
				arrayList.Add(new Point(base.Rectangle.Right, base.Rectangle.Y));
				arrayList.Add(new Point(base.Rectangle.Right, base.Rectangle.Bottom));
				arrayList.Add(new Point(base.Rectangle.X, base.Rectangle.Bottom));
				for (int i = 0; i < arrayList.Count; i++)
				{
					point = (Point)arrayList[i];
					if (left == Point.Empty)
					{
						left = point;
					}
					else
					{
						if (point.X < left.X)
						{
							left.X = point.X;
						}
						if (point.Y < left.Y)
						{
							left.Y = point.Y;
						}
					}
					if (point.X > empty.X)
					{
						empty.X = point.X;
					}
					if (point.Y > empty.Y)
					{
						empty.Y = point.Y;
					}
				}
				return new Rectangle(left.X, left.Y, Math.Abs(empty.X - left.X), Math.Abs(empty.Y - left.Y));
			}
		}

		public DrawNote()
		{
			base.SetRectangle(0, 0, 1, 1);
			base.Initialize();
		}

		public DrawNote(int x, int y, string name, string comment, Color lineColor, Color fillColor, int opacity, bool filled, int lineWidth, int fontSize)
		{
			base.Rectangle = new Rectangle(x, y, 1, 1);
			base.Color = lineColor;
			base.FillColor = fillColor;
			base.Filled = filled;
			base.Opacity = opacity;
			base.PenWidth = lineWidth;
			this.FontSize = fontSize;
			base.Name = name;
			base.GroupID = -1;
			base.Text = "";
			base.Comment = comment;
			this.resizeContent = true;
			base.Initialize();
		}

		public DrawNote(int x, int y, int width, int height, int anchorX, int anchorY, string name, Color lineColor, Color fillColor, int opacity, bool filled, int lineWidth, int fontSize)
		{
			base.Rectangle = new Rectangle(x, y, width, height);
			this.StartPoint = new Point(anchorX, anchorY);
			this.EndPoint = this.GetAnchorHandle();
			base.Color = lineColor;
			base.FillColor = fillColor;
			base.Filled = filled;
			base.Opacity = opacity;
			base.PenWidth = lineWidth;
			this.FontSize = fontSize;
			base.Name = name;
			base.GroupID = -1;
			base.Text = "";
			base.Comment = "";
			this.resizeContent = false;
			base.Initialize();
		}

		public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = (printToScreen ? ((imageQuality == MainForm.ImageQualityEnum.QualityHigh) ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel) : TextRenderingHint.ClearTypeGridFit);
			Brush brush = new SolidBrush(Color.FromArgb(base.Opacity, base.FillColor));
			Pen pen;
			if (base.DrawPen == null)
			{
				pen = new Pen(Color.FromArgb(base.Opacity + 30, Color.Black), (float)base.PenWidth);
			}
			else
			{
				pen = (Pen)base.DrawPen.Clone();
			}
			pen.DashStyle = DashStyle.Solid;
			GraphicsPath graphicsPath = new GraphicsPath();
			StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
			stringFormat.Alignment = StringAlignment.Near;
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.Trimming = StringTrimming.None;
			stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
			if (this.resizeContent)
			{
				Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
				int num = 0;
				int num2 = 0;
				string[] fields = Utilities.GetFields(base.Comment, '\n');
				foreach (string text in fields)
				{
					string text2 = text.Replace("\r", " ");
					text2 = ((text2 == " ") ? text2 : text.Replace("\r", ""));
					SizeF sizeF = g.MeasureString(text2, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), -1, stringFormat);
					num = ((sizeF.ToSize().Width > num) ? sizeF.ToSize().Width : num);
					num2 += sizeF.ToSize().Height;
				}
				if (this.StartPoint.IsEmpty)
				{
					normalizedRectangle.X -= num / 2 - 4;
					normalizedRectangle.Y += 4;
				}
				base.SetRectangle(normalizedRectangle.X, normalizedRectangle.Y, num + base.PenWidth + this.padding * 2, num2 + base.PenWidth + this.padding * 2);
				Point handle = this.GetHandle(2);
				if (this.StartPoint.IsEmpty)
				{
					this.StartPoint = new Point(handle.X, handle.Y - 1);
				}
				if (this.EndPoint.IsEmpty)
				{
					this.EndPoint = this.GetAnchorHandle();
				}
				this.resizeContent = false;
			}
			Rectangle normalizedRectangle2 = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
			normalizedRectangle2.X -= offsetX;
			normalizedRectangle2.Y -= offsetY;
			graphicsPath.AddRectangle(normalizedRectangle2);
			if (base.Rotation != 0)
			{
				RectangleF bounds = graphicsPath.GetBounds();
				Matrix matrix = new Matrix();
				matrix.RotateAt((float)base.Rotation, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
				graphicsPath.Transform(matrix);
			}
			if (base.Filled)
			{
				g.FillPath(brush, graphicsPath);
			}
			g.DrawPath(pen, graphicsPath);
			Point pt = new Point(this.startPoint.X, this.startPoint.Y);
			Point pt2 = new Point(this.endPoint.X, this.endPoint.Y);
			pt.X -= offsetX;
			pt.Y -= offsetY;
			pt2.X -= offsetX;
			pt2.Y -= offsetY;
			pen.DashStyle = DashStyle.Solid;
			pen.StartCap = LineCap.ArrowAnchor;
			pen.EndCap = LineCap.Flat;
			g.DrawLine(pen, pt, pt2);
			g.DrawString(base.Comment, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), Brushes.Black, new RectangleF((float)(normalizedRectangle2.X + base.PenWidth / 2 + this.padding), (float)normalizedRectangle2.Y, (float)(normalizedRectangle2.Width - (base.PenWidth + this.padding)), (float)normalizedRectangle2.Height), stringFormat);
			stringFormat.Dispose();
			graphicsPath.Dispose();
			pen.Dispose();
			brush.Dispose();
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		public void RecalcLayout()
		{
			this.resizeContent = true;
			this.EndPoint = Point.Empty;
			base.DrawArea.DrawingBoard.Refresh();
		}

		public void UpdateFontSize(int fontSize)
		{
			this.FontSize = fontSize;
			this.RecalcLayout();
		}

		public override int ActiveHandleCount
		{
			get
			{
				return 9;
			}
		}

		public override int HandleCount
		{
			get
			{
				return 9;
			}
		}

		public override int GetHandleSize()
		{
			return 4;
		}

		public override Point GetHandle(int handleNumber)
		{
			if (handleNumber == 9)
			{
				return this.StartPoint;
			}
			return base.GetHandle(handleNumber);
		}

		private Point GetAnchorHandle()
		{
			Point result = base.GetHandle(2);
			int num = DrawLine.GetDistance(this.StartPoint, result);
			for (int i = 4; i < 10; i += 2)
			{
				Point handle = base.GetHandle(i);
				int distance = DrawLine.GetDistance(this.StartPoint, handle);
				if (distance < num)
				{
					num = distance;
					result = handle;
				}
			}
			return result;
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
			if (!base.PointInObject(point, offsetX, offsetY))
			{
				double num = DrawLine.FindDistanceToSegment(new Point(point.X + offsetX, point.Y + offsetY), this.startPoint, this.endPoint);
				return num >= 0.0 && num <= (double)(base.PenWidth / 2);
			}
			return true;
		}

		public override bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
		{
			return DrawLine.LineIntersectsRectangle(new Rectangle(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height), this.startPoint, this.endPoint) || base.IntersectsWith(rectangle, offsetX, offsetY);
		}

		public override Cursor GetHandleCursor(int handleNumber)
		{
			if (handleNumber == 9)
			{
				return Cursors.SizeAll;
			}
			return base.GetHandleCursor(handleNumber);
		}

		public override void MoveHandleTo(Point point, int handleNumber)
		{
			if (handleNumber == 9)
			{
				this.StartPoint = new Point(point.X + (int)base.DrawingBoard.Origin.X, point.Y + (int)base.DrawingBoard.Origin.Y);
			}
			else
			{
				base.MoveHandleTo(point, handleNumber);
			}
			this.EndPoint = this.GetAnchorHandle();
		}

		public override void Move(int deltaX, int deltaY)
		{
			base.Move(deltaX, deltaY);
			this.EndPoint = this.GetAnchorHandle();
		}

		private Point startPoint;

		private Point endPoint;

		private int padding = 20;

		private int fontSize = 52;

		private bool resizeContent;
	}
}
