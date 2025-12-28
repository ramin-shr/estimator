using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	[Serializable]
	public class DrawCounter : DrawRectangle
	{
		private int ValidateDefaultSize(int defaultSize)
		{
			if (defaultSize < 10)
			{
				return 80;
			}
			if (defaultSize <= 150)
			{
				return defaultSize;
			}
			return 150;
		}

		public int DefaultSize
		{
			get
			{
				return this.ValidateDefaultSize(this.defaultSize);
			}
			set
			{
				this.defaultSize = this.ValidateDefaultSize(value);
			}
		}

		private DrawCounter.CounterShapeTypeEnum ValidateShape(DrawCounter.CounterShapeTypeEnum shape)
		{
			if (shape < DrawCounter.CounterShapeTypeEnum.CounterShapeCircle)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
			}
			if (shape <= DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage)
			{
				return shape;
			}
			return DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
		}

		public DrawCounter.CounterShapeTypeEnum Shape
		{
			get
			{
				return this.ValidateShape(this.shape);
			}
			set
			{
				this.shape = this.ValidateShape(value);
			}
		}

		public string ImageFileName
		{
			get
			{
				return this.imageFileName;
			}
			set
			{
				this.imageFileName = value;
			}
		}

		public Image CustomImage
		{
			get
			{
				return this.customImage;
			}
		}

		public bool LoadCustomImage()
		{
			if (this.customImage != null)
			{
				this.customImage.Dispose();
			}
			if (this.imageFileName == string.Empty)
			{
				return false;
			}
			string text = Path.Combine(Utilities.GetCountersFolder(), this.imageFileName);
			if (!Utilities.FileExists(text))
			{
				return false;
			}
			bool result;
			try
			{
				this.customImage = new Bitmap(Image.FromFile(text));
				result = (this.customImage != null);
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileOpenError(text, exception);
				result = false;
			}
			return result;
		}

		public static string ShapeToString(DrawCounter.CounterShapeTypeEnum shape)
		{
			switch (shape)
			{
			case DrawCounter.CounterShapeTypeEnum.CounterShapeCircle:
				return Resources.Cercle;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
				return Resources.Carré;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
				return Resources.Losange;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
				return Resources.Triangle;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
				return Resources.Triangle_inversé;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
				return Resources.Trapèze;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
				return Resources.Trapèze_inversé;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage:
				return Resources.Symbole_personnalisé;
			default:
				return string.Empty;
			}
		}

		public static DrawCounter.CounterShapeTypeEnum StringToShape(string shape)
		{
			if (shape == Resources.Cercle)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
			}
			if (shape == Resources.Carré)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeSquare;
			}
			if (shape == Resources.Losange)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond;
			}
			if (shape == Resources.Triangle)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle;
			}
			if (shape == Resources.Triangle_inversé)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed;
			}
			if (shape == Resources.Trapèze)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze;
			}
			if (shape == Resources.Trapèze_inversé)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed;
			}
			if (shape == Resources.Symbole_personnalisé)
			{
				return DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage;
			}
			return DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
		}

		public DrawCounter()
		{
			base.SetRectangle(0, 0, 1, 1);
			base.Initialize();
		}

		public override DrawObject Clone()
		{
			DrawCounter drawCounter = new DrawCounter();
			drawCounter.DefaultSize = this.DefaultSize;
			drawCounter.Shape = this.Shape;
			drawCounter.ImageFileName = this.ImageFileName;
			if (drawCounter.Shape == DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage)
			{
				drawCounter.LoadCustomImage();
			}
			drawCounter.Rectangle = base.Rectangle;
			drawCounter.DisplayName = new Utilities.DisplayName(drawCounter, "");
			base.FillDrawObjectFields(drawCounter);
			return drawCounter;
		}

		public DrawCounter(int x, int y, int width, int height, string name, int groupID, string text, string comment)
		{
			base.Rectangle = new Rectangle(x, y, width, height);
			base.Name = name;
			this.DefaultSize = 80;
			this.Shape = DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
			base.GroupID = groupID;
			base.Text = text;
			base.Comment = comment;
			base.Initialize();
		}

		public DrawCounter(int x, int y, int width, int height, string name, int groupID, string text, string comment, Color lineColor, Color fillColor, int opacity, bool filled)
		{
			base.Rectangle = new Rectangle(x, y, width, height);
			base.Color = lineColor;
			base.FillColor = fillColor;
			base.Opacity = opacity;
			base.Filled = filled;
			base.Name = name;
			this.DefaultSize = 80;
			this.Shape = DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
			base.GroupID = groupID;
			base.Text = text;
			base.Comment = comment;
			base.Initialize();
		}

		public DrawCounter(int x, int y, int width, int height, string name, int groupID, string text, string comment, DrawingPens.PenType pType, Color fillColor, int opacity, bool filled)
		{
			base.Rectangle = new Rectangle(x, y, width, height);
			base.DrawPen = DrawingPens.SetCurrentPen(pType);
			base.PenType = pType;
			base.FillColor = fillColor;
			base.Opacity = opacity;
			base.Filled = filled;
			base.Name = name;
			this.DefaultSize = 80;
			this.Shape = DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
			base.GroupID = groupID;
			base.Text = text;
			base.Comment = comment;
			base.Initialize();
		}

		public DrawCounter(int x, int y, int width, int height, int counterSize, DrawCounter.CounterShapeTypeEnum shape, string name, int groupID, string text, string comment, Color lineColor, Color fillColor, int opacity, bool filled, int lineWidth)
		{
			base.Rectangle = new Rectangle(x, y, width, height);
			base.Color = lineColor;
			base.FillColor = fillColor;
			base.Filled = filled;
			base.Opacity = opacity;
			base.PenWidth = lineWidth;
			base.Name = name;
			this.DefaultSize = counterSize;
			this.Shape = shape;
			base.GroupID = groupID;
			base.Text = text;
			base.Comment = comment;
			base.Initialize();
		}

		public void UpdateDefaultSize(int defaultSize)
		{
			Point center = this.Center;
			this.DefaultSize = defaultSize;
			base.Rectangle = new Rectangle(center.X - this.DefaultSize / 2, center.Y - this.DefaultSize / 2, this.DefaultSize, this.DefaultSize);
		}

		public static Point[] GetTrianglePoints(Rectangle rect, DrawCounter.Direction direction)
		{
			int num = rect.Width / 2;
			int num2 = rect.Height / 2;
			Point empty = Point.Empty;
			Point empty2 = Point.Empty;
			Point empty3 = Point.Empty;
			switch (direction)
			{
			case DrawCounter.Direction.Up:
				empty = new Point(rect.Left + num, rect.Top);
				empty2 = new Point(rect.Left, rect.Bottom);
				empty3 = new Point(rect.Right, rect.Bottom);
				break;
			case DrawCounter.Direction.Down:
				empty = new Point(rect.Left + num, rect.Bottom);
				empty2 = new Point(rect.Left, rect.Top);
				empty3 = new Point(rect.Right, rect.Top);
				break;
			case DrawCounter.Direction.Left:
				empty = new Point(rect.Left, rect.Top + num2);
				empty2 = new Point(rect.Right, rect.Top);
				empty3 = new Point(rect.Right, rect.Bottom);
				break;
			case DrawCounter.Direction.Right:
				empty = new Point(rect.Right, rect.Top + num2);
				empty2 = new Point(rect.Left, rect.Bottom);
				empty3 = new Point(rect.Left, rect.Top);
				break;
			}
			return new Point[]
			{
				empty,
				empty2,
				empty3
			};
		}

		public static Point[] GetTapezePoints(Rectangle rect, DrawCounter.Direction direction)
		{
			int num = rect.Width / 2;
			int num2 = rect.Height / 2;
			Point empty = Point.Empty;
			Point empty2 = Point.Empty;
			Point empty3 = Point.Empty;
			Point empty4 = Point.Empty;
			switch (direction)
			{
			case DrawCounter.Direction.Up:
				empty = new Point(rect.Left + num / 2, rect.Top);
				empty2 = new Point(rect.Right - num / 2, rect.Top);
				empty3 = new Point(rect.Right, rect.Bottom);
				empty4 = new Point(rect.Left, rect.Bottom);
				break;
			case DrawCounter.Direction.Down:
				empty = new Point(rect.Left, rect.Top);
				empty2 = new Point(rect.Right, rect.Top);
				empty3 = new Point(rect.Right - num / 2, rect.Bottom);
				empty4 = new Point(rect.Left + num / 2, rect.Bottom);
				break;
			}
			return new Point[]
			{
				empty,
				empty2,
				empty3,
				empty4
			};
		}

		public static Point[] GetDiamondPoints(Rectangle rect)
		{
			int num = rect.Width / 2;
			int num2 = rect.Height / 2;
			Point point = new Point(rect.Left + num, rect.Top);
			Point point2 = new Point(rect.Right, rect.Top + num2);
			Point point3 = new Point(rect.Left + num, rect.Bottom);
			Point point4 = new Point(rect.Left, rect.Top + num2);
			return new Point[]
			{
				point,
				point2,
				point3,
				point4
			};
		}

		public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = (printToScreen ? ((imageQuality == MainForm.ImageQualityEnum.QualityHigh) ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel) : TextRenderingHint.ClearTypeGridFit);
			if (this.Shape == DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage && this.CustomImage == null)
			{
				this.Shape = DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
			}
			if (this.Shape != DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage)
			{
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
				GraphicsPath graphicsPath = new GraphicsPath();
				Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
				normalizedRectangle.X -= offsetX;
				normalizedRectangle.Y -= offsetY;
				base.LastArea = base.ComputeArea();
				int num = 0;
				switch (this.Shape)
				{
				case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
					graphicsPath.AddRectangle(normalizedRectangle);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
					graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(normalizedRectangle));
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
					graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(normalizedRectangle, DrawCounter.Direction.Up));
					num = (int)((float)normalizedRectangle.Height / 6f);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
					graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(normalizedRectangle, DrawCounter.Direction.Down));
					num = -(int)((float)normalizedRectangle.Height / 6f);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
					graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(normalizedRectangle, DrawCounter.Direction.Up));
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
					graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(normalizedRectangle, DrawCounter.Direction.Down));
					break;
				default:
					graphicsPath.AddEllipse(normalizedRectangle);
					break;
				}
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
				if (!base.SuspendContentDrawing && base.DrawingBoard.ZoomFactor >= 0.25)
				{
					using (StringFormat stringFormat = new StringFormat())
					{
						stringFormat.Alignment = StringAlignment.Center;
						stringFormat.LineAlignment = StringAlignment.Center;
						Point center = this.Center;
						this.fontSize = (float)this.DefaultSize * 0.15f;
						Font font = Utilities.CreateFont("Tahoma", Utilities.FontSizeInPoints(this.fontSize), FontStyle.Bold);
						g.DrawString(base.Text + "\n", font, Brushes.Black, new PointF((float)(center.X - offsetX), (float)(center.Y - offsetY + num)), stringFormat);
					}
				}
				graphicsPath.Dispose();
				pen.Dispose();
				brush.Dispose();
			}
			else
			{
				Rectangle normalizedRectangle2 = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
				normalizedRectangle2.X -= offsetX;
				normalizedRectangle2.Y -= offsetY;
				base.LastArea = base.ComputeArea();
				float opacity = (float)base.Opacity / 255f;
				using (Image image = Utilities.SetImageOpacity(this.CustomImage, opacity))
				{
					g.DrawImage(image, normalizedRectangle2, new Rectangle(Point.Empty, image.Size), GraphicsUnit.Pixel);
				}
			}
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		private float fontSize = 12f;

		private int defaultSize = 80;

		private DrawCounter.CounterShapeTypeEnum shape;

		private string imageFileName;

		private Image customImage;

		public enum CounterShapeTypeEnum
		{
			CounterShapeCircle,
			CounterShapeSquare,
			CounterShapeDiamond,
			CounterShapeTriangle,
			CounterShapeTriangleReversed,
			CounterShapeTrapeze,
			CounterShapeTrapezeReversed,
			CounterShapeCustomImage
		}

		public enum Direction
		{
			Up,
			Down,
			Left,
			Right
		}
	}
}
