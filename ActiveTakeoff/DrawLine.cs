using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class DrawLine : DrawObject
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

		public double Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		public override SlopeFactor SlopeFactor
		{
			get
			{
				return this.slopeFactor;
			}
		}

		public override Rectangle BoundingRectangle
		{
			get
			{
				return this.GetNormalizedRectangle();
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
			Rectangle normalizedRectangle = this.GetNormalizedRectangle();
			return new Point(normalizedRectangle.X + normalizedRectangle.Width / 2, normalizedRectangle.Y + normalizedRectangle.Height / 2);
		}

		public bool FigureCompleted
		{
			get
			{
				return this.figureCompleted;
			}
			set
			{
				this.figureCompleted = value;
			}
		}

		public override void SetSlopeFactor(SlopeFactor slopeFactor)
		{
			this.SlopeFactor.SetValues(slopeFactor);
		}

		public override void SetSlopeFactor(double internalValue, SlopeFactor.SlopeTypeEnum slopeType, SlopeFactor.SlopeApplyTypeEnum slopeApplyType, SlopeFactor.HipValleyEnum hipValley)
		{
			SlopeFactor slopeFactor = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyDisabled);
			slopeFactor.SetValues(internalValue, slopeType, slopeApplyType, hipValley);
			this.SetSlopeFactor(slopeFactor);
		}

		public DrawLine()
		{
			this.startPoint.X = 0;
			this.startPoint.Y = 0;
			this.endPoint.X = 1;
			this.endPoint.Y = 1;
			base.ZOrder = 0;
			this.FigureCompleted = false;
			base.Initialize();
		}

		public DrawLine(int x1, int y1, int x2, int y2, PointF offset, string name, int groupID, string comment)
		{
			this.startPoint.X = x1;
			this.startPoint.Y = y1;
			this.endPoint.X = x2;
			this.endPoint.Y = y2;
			base.Offset = offset;
			base.ZOrder = 0;
			base.Name = name;
			base.GroupID = groupID;
			base.Comment = comment;
			this.FigureCompleted = false;
			base.Initialize();
		}

		public DrawLine(int x1, int y1, int x2, int y2, PointF offset, string name, int groupID, string comment, bool figureCompleted, Color lineColor, int opacity, int lineWidth)
		{
			this.startPoint.X = x1;
			this.startPoint.Y = y1;
			this.endPoint.X = x2;
			this.endPoint.Y = y2;
			base.Offset = offset;
			base.Color = lineColor;
			base.Opacity = opacity;
			base.PenWidth = lineWidth;
			base.ZOrder = 0;
			base.Name = name;
			base.GroupID = groupID;
			base.Comment = comment;
			this.FigureCompleted = figureCompleted;
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
			base.ClearTextArray();
			int num = 0;
			int value = this.Distance2D(this.StartPoint.X, this.StartPoint.Y, this.EndPoint.X, this.EndPoint.Y, true, ref num);
			if ((double)num * base.DrawingBoard.ZoomFactor >= (double)(this.toolTipMinThreshold + (base.DrawArea.UnitScaleIsImperial() ? 10 : 0)))
			{
				string text = base.DisplayInPixels ? (value.ToString() + " pixels") : base.DrawArea.ToLengthString(value, (double)num * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold);
				base.TextArray.Add(new DrawText(text, this.Center, this.TextAngle(this.startPoint, this.endPoint)));
			}
			this.lastDistance = (double)num;
			GraphicsPath graphicsPath = new GraphicsPath();
			Point pt = new Point(this.startPoint.X, this.startPoint.Y);
			Point pt2 = new Point(this.endPoint.X, this.endPoint.Y);
			pt.X -= offsetX;
			pt.Y -= offsetY;
			pt2.X -= offsetX;
			pt2.Y -= offsetY;
			graphicsPath.AddLine(pt, pt2);
			if (base.Rotation != 0)
			{
				RectangleF bounds = graphicsPath.GetBounds();
				Matrix matrix = new Matrix();
				matrix.RotateAt((float)base.Rotation, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
				graphicsPath.Transform(matrix);
			}
			g.DrawPath(pen, graphicsPath);
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

		public override DrawObject Clone()
		{
			DrawLine drawLine = new DrawLine();
			drawLine.StartPoint = new Point(this.StartPoint.X, this.StartPoint.Y);
			drawLine.EndPoint = new Point(this.EndPoint.X, this.EndPoint.Y);
			drawLine.Height = this.Height;
			drawLine.FigureCompleted = true;
			drawLine.SetSlopeFactor(this.slopeFactor);
			drawLine.DisplayName = new Utilities.DisplayName(drawLine, "");
			base.FillDrawObjectFields(drawLine);
			return drawLine;
		}

		public override Region Region(int offsetX, int offsetY, float zoomFactor)
		{
			Region region = new Region();
			offsetX -= (int)((float)base.DrawArea.DrawingBoard.HorizontalOffset / zoomFactor);
			offsetY -= (int)((float)base.DrawArea.DrawingBoard.VerticalOffset / zoomFactor);
			Rectangle normalizedRectangle = this.GetNormalizedRectangle();
			normalizedRectangle.X -= offsetX;
			normalizedRectangle.Y -= offsetY;
			normalizedRectangle.X = (int)((float)normalizedRectangle.X * zoomFactor);
			normalizedRectangle.Y = (int)((float)normalizedRectangle.Y * zoomFactor);
			normalizedRectangle.Width = (int)((float)normalizedRectangle.Width * zoomFactor);
			normalizedRectangle.Height = (int)((float)normalizedRectangle.Height * zoomFactor);
			normalizedRectangle.Inflate(new Size(10 + base.PenWidth, 10 + base.PenWidth));
			region.MakeEmpty();
			region.Union(normalizedRectangle);
			if (base.TextArray.Count > 0)
			{
				DrawText drawText = (DrawText)base.TextArray[0];
				Rectangle rect = new Rectangle(drawText.Point.X - offsetX - drawText.Rectangle.Width / 2, drawText.Point.Y - offsetY - drawText.Rectangle.Height / 2, drawText.Rectangle.Width, drawText.Rectangle.Height);
				rect.X = (int)((float)rect.X * zoomFactor);
				rect.Y = (int)((float)rect.Y * zoomFactor);
				rect.Width = (int)((float)rect.Width * zoomFactor);
				rect.Height = (int)((float)rect.Height * zoomFactor);
				rect.Inflate(new Size(base.PenWidth, base.PenWidth));
				region.Union(rect);
			}
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

		public override int ActiveHandleCount
		{
			get
			{
				if (!this.FigureCompleted)
				{
					return 1;
				}
				return 2;
			}
		}

		public override int HandleCount
		{
			get
			{
				return 2;
			}
		}

		public override Point GetHandle(int handleNumber)
		{
			if (handleNumber < 1)
			{
				handleNumber = 1;
			}
			if (handleNumber > 2)
			{
				handleNumber = 2;
			}
			if (handleNumber != 1)
			{
				return this.EndPoint;
			}
			return this.StartPoint;
		}

		public override int GetHandleSize()
		{
			return 4;
		}

		public override int HitTest(Point point, int offsetX, int offsetY)
		{
			if (this.Selected)
			{
				for (int i = 1; i <= this.HandleCount; i++)
				{
					GraphicsPath graphicsPath = new GraphicsPath();
					graphicsPath.AddRectangle(this.GetHandleRectangle(i, offsetX, offsetY));
					bool flag = graphicsPath.IsVisible(point);
					graphicsPath.Dispose();
					if (flag)
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

		public override bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
		{
			return DrawLine.LineIntersectsRectangle(new Rectangle(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height), this.startPoint, this.endPoint);
		}

		protected override bool PointInObject(Point point, int offsetX, int offsetY)
		{
			double num = DrawLine.FindDistanceToSegment(new Point(point.X + offsetX, point.Y + offsetY), this.startPoint, this.endPoint);
			return num >= 0.0 && num <= (double)(base.PenWidth / 2);
		}

		public override Cursor GetHandleCursor(int handleNumber)
		{
			switch (handleNumber)
			{
			case 1:
			case 2:
				return Cursors.SizeAll;
			default:
				return Cursors.Default;
			}
		}

		public override void MoveHandleTo(Point point, int handleNumber)
		{
			if (handleNumber == 1)
			{
				this.startPoint = point;
				this.startPoint.X = this.startPoint.X + (int)base.DrawingBoard.Origin.X;
				this.startPoint.Y = this.startPoint.Y + (int)base.DrawingBoard.Origin.Y;
			}
			else
			{
				this.endPoint = point;
				this.endPoint.X = this.endPoint.X + (int)base.DrawingBoard.Origin.X;
				this.endPoint.Y = this.endPoint.Y + (int)base.DrawingBoard.Origin.Y;
			}
			base.Dirty = true;
			this.Invalidate();
		}

		public override void Move(int deltaX, int deltaY)
		{
			this.startPoint.X = this.startPoint.X + deltaX;
			this.startPoint.Y = this.startPoint.Y + deltaY;
			this.endPoint.X = this.endPoint.X + deltaX;
			this.endPoint.Y = this.endPoint.Y + deltaY;
			base.Dirty = true;
			this.Invalidate();
		}

		public static bool LineIsReverse(Point startPoint, Point endPoint)
		{
			bool result;
			if (endPoint.X == startPoint.X && endPoint.Y != startPoint.Y)
			{
				result = (endPoint.Y < startPoint.Y);
			}
			else if (endPoint.X != startPoint.X && endPoint.Y == startPoint.Y)
			{
				result = (endPoint.X < startPoint.X);
			}
			else if (Math.Abs(endPoint.X - startPoint.X) > Math.Abs(endPoint.Y - startPoint.Y))
			{
				result = (endPoint.X < startPoint.X);
			}
			else
			{
				result = (endPoint.Y < startPoint.Y);
			}
			return result;
		}

		public static bool LineIsReverse(PointF startPoint, PointF endPoint)
		{
			bool result;
			if (endPoint.X == startPoint.X && endPoint.Y != startPoint.Y)
			{
				result = (endPoint.Y < startPoint.Y);
			}
			else if (endPoint.X != startPoint.X && endPoint.Y == startPoint.Y)
			{
				result = (endPoint.X < startPoint.X);
			}
			else if (Math.Abs(endPoint.X - startPoint.X) > Math.Abs(endPoint.Y - startPoint.Y))
			{
				result = (endPoint.X < startPoint.X);
			}
			else
			{
				result = (endPoint.Y < startPoint.Y);
			}
			return result;
		}

		public void NormalizeLine()
		{
			if (DrawLine.LineIsReverse(this.startPoint, this.endPoint))
			{
				Point point = new Point(this.startPoint.X, this.startPoint.Y);
				this.startPoint = new Point(this.endPoint.X, this.endPoint.Y);
				this.endPoint = new Point(point.X, point.Y);
			}
		}

		public double RoundAngle(double angle, int precision)
		{
			if (angle == 0.0 || angle == 90.0 || angle == 180.0 || angle == 270.0 || angle == 360.0)
			{
				if (angle != 360.0)
				{
					return angle;
				}
				return 0.0;
			}
			else
			{
				int[] array = new int[5];
				double[] array2 = new double[5];
				array[0] = 0;
				array[1] = 90;
				array[2] = 180;
				array[3] = 270;
				array[4] = 0;
				array2[0] = Math.Abs(angle - 0.0);
				array2[1] = Math.Abs(angle - 90.0);
				array2[2] = Math.Abs(angle - 180.0);
				array2[3] = Math.Abs(angle - 270.0);
				array2[4] = Math.Abs(angle - 360.0);
				int num = 0;
				for (int i = 1; i < array2.Length; i++)
				{
					if (array2[i] < array2[num])
					{
						num = i;
					}
				}
				if (array2[num] <= (double)precision)
				{
					return (double)array[num];
				}
				return angle;
			}
		}

		public static double Angle(double px1, double py1, double px2, double py2)
		{
			double num = px2 - px1;
			double num2 = py2 - py1;
			double num3;
			if (num == 0.0)
			{
				if (num2 >= 0.0)
				{
					num3 = 1.5707963267948966;
				}
				else
				{
					num3 = 4.71238898038469;
				}
			}
			else if (num2 == 0.0)
			{
				num3 = ((num > 0.0) ? 0.0 : 3.141592653589793);
			}
			else if (num < 0.0)
			{
				num3 = Math.Atan(num2 / num) + 3.141592653589793;
			}
			else if (num2 < 0.0)
			{
				num3 = Math.Atan(num2 / num) + 6.283185307179586;
			}
			else
			{
				num3 = Math.Atan(num2 / num);
			}
			return num3 * 180.0 / 3.141592653589793;
		}

		public int TextAngle(Point startPoint, Point endPoint)
		{
			int value = 0;
			int num = (int)DrawLine.Angle((double)startPoint.X, (double)startPoint.Y, (double)endPoint.X, (double)endPoint.Y);
			if (num >= 0 && num <= 90)
			{
				value = num;
			}
			if (num > 90 && num < 180)
			{
				value = 270 + (num - 90);
			}
			if (num >= 180 && num < 270)
			{
				value = num - 180;
			}
			if (num >= 270)
			{
				value = 270 + (num - 270);
			}
			return Math.Abs(value);
		}

		public static int GetDistance(Point startPoint, Point endPoint)
		{
			int x = startPoint.X;
			int y = startPoint.Y;
			int x2 = endPoint.X;
			int y2 = endPoint.Y;
			double num = Math.Pow((double)(x2 - x), 2.0);
			double num2 = Math.Pow((double)(y2 - y), 2.0);
			double d = num + num2;
			return (int)Math.Sqrt(d);
		}

		public int Distance2D(Point startPoint, Point endPoint)
		{
			return DrawLine.GetDistance(startPoint, endPoint);
		}

		public int Distance2D(bool applySlope)
		{
			int num = 0;
			if (applySlope)
			{
				return this.Distance2D(this.StartPoint.X, this.StartPoint.Y, this.EndPoint.X, this.EndPoint.Y, true, ref num);
			}
			return this.Distance2D(this.StartPoint, this.EndPoint);
		}

		public int Distance2D(int x1, int y1, int x2, int y2, bool applySlopeOnPlan, ref int distanceBeforeSlope)
		{
			int num = this.Distance2D(new Point(x1, y1), new Point(x2, y2));
			distanceBeforeSlope = num;
			if (this.SlopeFactor.InternalValue > 0.0 && (this.SlopeFactor.SlopeApplyType != SlopeFactor.SlopeApplyTypeEnum.applyOnPlan || (this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnPlan && applySlopeOnPlan)))
			{
				double num2 = DrawLine.Angle((double)x1, (double)y1, (double)x2, (double)y2);
				Console.WriteLine("angle = " + num2);
				if (this.SlopeFactor.HipValley == SlopeFactor.HipValleyEnum.hipValleyEnabled && this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnElevation)
				{
					num = (int)this.SlopeFactor.Apply((double)num, (double)Math.Abs(x2 - x1), (double)Math.Abs(y2 - y1));
				}
				else if ((num2 >= 89.0 && num2 <= 91.0) || (num2 >= 269.0 && num2 <= 271.0) || this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnPlan)
				{
					num = (int)this.SlopeFactor.Apply((double)num, 0.0, 0.0);
				}
			}
			return num;
		}

		public float Slope(PointF point1, PointF point2)
		{
			return (point2.Y - point1.Y) / (point2.X - point1.X);
		}

		public static double FindDistanceToSegment(Point pt, Point p1, Point p2)
		{
			PointF pointF;
			return DrawLine.FindDistanceToSegment(pt, p1, p2, out pointF);
		}

		public static double FindDistanceToSegment(Point pt, Point p1, Point p2, out PointF closest)
		{
			float num = (float)(p2.X - p1.X);
			float num2 = (float)(p2.Y - p1.Y);
			if (num == 0f && num2 == 0f)
			{
				closest = p1;
				num = (float)(pt.X - p1.X);
				num2 = (float)(pt.Y - p1.Y);
				return Math.Sqrt((double)(num * num + num2 * num2));
			}
			float num3 = ((float)(pt.X - p1.X) * num + (float)(pt.Y - p1.Y) * num2) / (num * num + num2 * num2);
			if (num3 < 0f)
			{
				closest = new Point(p1.X, p1.Y);
				num = (float)(pt.X - p1.X);
				num2 = (float)(pt.Y - p1.Y);
			}
			else if (num3 > 1f)
			{
				closest = new Point(p2.X, p2.Y);
				num = (float)(pt.X - p2.X);
				num2 = (float)(pt.Y - p2.Y);
			}
			else
			{
				closest = new PointF((float)p1.X + num3 * num, (float)p1.Y + num3 * num2);
				num = (float)pt.X - closest.X;
				num2 = (float)pt.Y - closest.Y;
			}
			return Math.Sqrt((double)(num * num + num2 * num2));
		}

		public static IEnumerable<Point> GetPointsOnLine(int x0, int y0, int x1, int y1)
		{
			bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (steep)
			{
				int num = x0;
				x0 = y0;
				y0 = num;
				num = x1;
				x1 = y1;
				y1 = num;
			}
			if (x0 > x1)
			{
				int num2 = x0;
				x0 = x1;
				x1 = num2;
				num2 = y0;
				y0 = y1;
				y1 = num2;
			}
			int dx = x1 - x0;
			int dy = Math.Abs(y1 - y0);
			int error = dx / 2;
			int ystep = (y0 < y1) ? 1 : -1;
			int y2 = y0;
			for (int x2 = x0; x2 <= x1; x2++)
			{
				yield return new Point(steep ? y2 : x2, steep ? x2 : y2);
				error -= dy;
				if (error < 0)
				{
					y2 += ystep;
					error += dx;
				}
			}
			yield break;
		}

		public static Point GetPointAtAngle(Point startPoint, double angle, double distance)
		{
			Point result = new Point((int)Math.Ceiling((double)startPoint.X + distance * Math.Cos(Utilities.DegreeToRadian(angle))), (int)Math.Ceiling((double)startPoint.Y + distance * Math.Sin(Utilities.DegreeToRadian(angle))));
			return result;
		}

		public static PointF GetPointAtAngle(PointF startPoint, double angle, double distance)
		{
			PointF result = new PointF((float)Math.Ceiling((double)startPoint.X + distance * Math.Cos(Utilities.DegreeToRadian(angle))), (float)Math.Ceiling((double)startPoint.Y + distance * Math.Sin(Utilities.DegreeToRadian(angle))));
			return result;
		}

		public static bool LineIntersectsRectangle(Rectangle rectangle, Point p1, Point p2)
		{
			return DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y)) || DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X + rectangle.Width, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)) || DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), new Point(rectangle.X, rectangle.Y + rectangle.Height)) || DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X, rectangle.Y + rectangle.Height), new Point(rectangle.X, rectangle.Y)) || (rectangle.Contains(p1) && rectangle.Contains(p2));
		}

		public static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
		{
			float num = (float)((l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y));
			float num2 = (float)((l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X));
			if (num2 == 0f)
			{
				return false;
			}
			float num3 = num / num2;
			num = (float)((l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y));
			float num4 = num / num2;
			return num3 >= 0f && num3 <= 1f && num4 >= 0f && num4 <= 1f;
		}

		public static bool LineIntersectsLine(PointF l1p1, PointF l1p2, PointF l2p1, PointF l2p2)
		{
			float num = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
			float num2 = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);
			if (num2 == 0f)
			{
				return false;
			}
			float num3 = num / num2;
			num = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
			float num4 = num / num2;
			return num3 >= 0f && num3 <= 1f && num4 >= 0f && num4 <= 1f;
		}

		public Point LocatePointOnLine(Point pt, Point p1, Point p2)
		{
			PointF pointF;
			DrawLine.FindDistanceToSegment(pt, p1, p2, out pointF);
			return new Point((int)pointF.X, (int)pointF.Y);
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

		public override void Scale(float scaleX, float scaleY)
		{
			this.startPoint.X = (int)((float)this.startPoint.X * scaleX);
			this.startPoint.Y = (int)((float)this.startPoint.Y * scaleY);
			this.endPoint.X = (int)((float)this.endPoint.X * scaleX);
			this.endPoint.Y = (int)((float)this.endPoint.Y * scaleY);
		}

		protected void Invalidate()
		{
		}

		private Point startPoint;

		private Point endPoint;

		private double height;

		private double lastDistance;

		private SlopeFactor slopeFactor = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyDisabled);

		private bool figureCompleted;

		private int toolTipMinThreshold = 50;

		private int toolTipMaxThreshold = 100;

		[CompilerGenerated]
		private sealed class <GetPointsOnLine>d__0 : IEnumerable<Point>, IEnumerable, IEnumerator<Point>, IEnumerator, IDisposable
		{
			[DebuggerHidden]
			IEnumerator<Point> IEnumerable<Point>.GetEnumerator()
			{
				DrawLine.<GetPointsOnLine>d__0 <GetPointsOnLine>d__;
				if (Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId && this.<>1__state == -2)
				{
					this.<>1__state = 0;
					<GetPointsOnLine>d__ = this;
				}
				else
				{
					<GetPointsOnLine>d__ = new DrawLine.<GetPointsOnLine>d__0(0);
				}
				<GetPointsOnLine>d__.x0 = x0;
				<GetPointsOnLine>d__.y0 = y0;
				<GetPointsOnLine>d__.x1 = x1;
				<GetPointsOnLine>d__.y1 = y1;
				return <GetPointsOnLine>d__;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Drawing.Point>.GetEnumerator();
			}

			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					steep = (Math.Abs(y1 - y0) > Math.Abs(x1 - x0));
					if (steep)
					{
						int num = x0;
						x0 = y0;
						y0 = num;
						num = x1;
						x1 = y1;
						y1 = num;
					}
					if (x0 > x1)
					{
						int num2 = x0;
						x0 = x1;
						x1 = num2;
						num2 = y0;
						y0 = y1;
						y1 = num2;
					}
					dx = x1 - x0;
					dy = Math.Abs(y1 - y0);
					error = dx / 2;
					ystep = ((y0 < y1) ? 1 : -1);
					y2 = y0;
					x2 = x0;
					break;
				case 1:
					this.<>1__state = -1;
					error -= dy;
					if (error < 0)
					{
						y2 += ystep;
						error += dx;
					}
					x2++;
					break;
				default:
					return false;
				}
				if (x2 <= x1)
				{
					this.<>2__current = new Point(steep ? y2 : x2, steep ? x2 : y2);
					this.<>1__state = 1;
					return true;
				}
				return false;
			}

			Point IEnumerator<Point>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			void IDisposable.Dispose()
			{
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			[DebuggerHidden]
			public <GetPointsOnLine>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			private Point <>2__current;

			private int <>1__state;

			private int <>l__initialThreadId;

			public int x0;

			public int <>3__x0;

			public int y0;

			public int <>3__y0;

			public int x1;

			public int <>3__x1;

			public int y1;

			public int <>3__y1;

			public bool <steep>5__1;

			public int <dx>5__2;

			public int <dy>5__3;

			public int <error>5__4;

			public int <ystep>5__5;

			public int <y>5__6;

			public int <x>5__7;
		}
	}
}
