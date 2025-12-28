using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class CustomRendering
	{
		public Preset Preset
		{
			[CompilerGenerated]
			get
			{
				return this.<Preset>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Preset>k__BackingField = value;
			}
		}

		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this.<Count>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Count>k__BackingField = value;
			}
		}

		public double FindDistanceBetweenSegments(PointF p1, PointF p2, PointF p3, PointF p4, out PointF close1, out PointF close2)
		{
			bool flag;
			bool flag2;
			PointF pointF;
			this.FindIntersection(p1, p2, p3, p4, out flag, out flag2, out pointF, out close1, out close2);
			if (flag2)
			{
				close1 = pointF;
				close2 = pointF;
				return 0.0;
			}
			double num = double.MaxValue;
			PointF pointF2;
			double num2 = this.FindDistanceToSegment(p1, p3, p4, out pointF2);
			if (num2 < num)
			{
				num = num2;
				close1 = p1;
				close2 = pointF2;
			}
			num2 = this.FindDistanceToSegment(p2, p3, p4, out pointF2);
			if (num2 < num)
			{
				num = num2;
				close1 = p2;
				close2 = pointF2;
			}
			num2 = this.FindDistanceToSegment(p3, p1, p2, out pointF2);
			if (num2 < num)
			{
				num = num2;
				close1 = pointF2;
				close2 = p3;
			}
			num2 = this.FindDistanceToSegment(p4, p1, p2, out pointF2);
			if (num2 < num)
			{
				num = num2;
				close1 = pointF2;
				close2 = p4;
			}
			return num;
		}

		private void FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4, out bool lines_intersect, out bool segments_intersect, out PointF intersection, out PointF close_p1, out PointF close_p2)
		{
			float num = p2.X - p1.X;
			float num2 = p2.Y - p1.Y;
			float num3 = p4.X - p3.X;
			float num4 = p4.Y - p3.Y;
			float num5 = num2 * num3 - num * num4;
			float num6;
			try
			{
				num6 = ((p1.X - p3.X) * num4 + (p3.Y - p1.Y) * num3) / num5;
			}
			catch
			{
				lines_intersect = false;
				segments_intersect = false;
				intersection = new PointF(float.NaN, float.NaN);
				close_p1 = new PointF(float.NaN, float.NaN);
				close_p2 = new PointF(float.NaN, float.NaN);
				return;
			}
			lines_intersect = true;
			float num7 = ((p3.X - p1.X) * num2 + (p1.Y - p3.Y) * num) / -num5;
			intersection = new PointF(p1.X + num * num6, p1.Y + num2 * num6);
			segments_intersect = (num6 >= 0f && num6 <= 1f && num7 >= 0f && num7 <= 1f);
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			else if (num6 > 1f)
			{
				num6 = 1f;
			}
			if (num7 < 0f)
			{
				num7 = 0f;
			}
			else if (num7 > 1f)
			{
				num7 = 1f;
			}
			close_p1 = new PointF(p1.X + num * num6, p1.Y + num2 * num6);
			close_p2 = new PointF(p3.X + num3 * num7, p3.Y + num4 * num7);
		}

		private double FindDistanceToSegment(PointF pt, PointF p1, PointF p2, out PointF closest)
		{
			float num = p2.X - p1.X;
			float num2 = p2.Y - p1.Y;
			if (num == 0f && num2 == 0f)
			{
				closest = p1;
				num = pt.X - p1.X;
				num2 = pt.Y - p1.Y;
				return Math.Sqrt((double)(num * num + num2 * num2));
			}
			float num3 = ((pt.X - p1.X) * num + (pt.Y - p1.Y) * num2) / (num * num + num2 * num2);
			if (num3 < 0f)
			{
				closest = new PointF(p1.X, p1.Y);
				num = pt.X - p1.X;
				num2 = pt.Y - p1.Y;
			}
			else if (num3 > 1f)
			{
				closest = new PointF(p2.X, p2.Y);
				num = pt.X - p2.X;
				num2 = pt.Y - p2.Y;
			}
			else
			{
				closest = new PointF(p1.X + num3 * num, p1.Y + num3 * num2);
				num = pt.X - closest.X;
				num2 = pt.Y - closest.Y;
			}
			return Math.Sqrt((double)(num * num + num2 * num2));
		}

		public Point LineIntersectionPoint(Point ps1, Point pe1, Point ps2, Point pe2)
		{
			float num = (float)(pe1.Y - ps1.Y);
			float num2 = (float)(ps1.X - pe1.X);
			float num3 = num * (float)ps1.X + num2 * (float)ps1.Y;
			float num4 = (float)(pe2.Y - ps2.Y);
			float num5 = (float)(ps2.X - pe2.X);
			float num6 = num4 * (float)ps2.X + num5 * (float)ps2.Y;
			float num7 = num * num5 - num4 * num2;
			if (num7 == 0f)
			{
				return Point.Empty;
			}
			return new Point((int)((num5 * num3 - num2 * num6) / num7), (int)((num * num6 - num4 * num3) / num7));
		}

		public PointF LineIntersectionPoint(PointF ps1, PointF pe1, PointF ps2, PointF pe2)
		{
			float num = pe1.Y - ps1.Y;
			float num2 = ps1.X - pe1.X;
			float num3 = num * ps1.X + num2 * ps1.Y;
			float num4 = pe2.Y - ps2.Y;
			float num5 = ps2.X - pe2.X;
			float num6 = num4 * ps2.X + num5 * ps2.Y;
			float num7 = num * num5 - num4 * num2;
			if (num7 == 0f)
			{
				return PointF.Empty;
			}
			return new PointF((num5 * num3 - num2 * num6) / num7, (num * num6 - num4 * num3) / num7);
		}

		public Segment GetSegmentAtAngle(Point startPoint, Point endPoint, int angle, double distance = 32565.0, CustomRendering.SegmentDirection segmentDirection = CustomRendering.SegmentDirection.SegmentDirectionBoth, int offsetX = 0)
		{
			switch (segmentDirection)
			{
			case CustomRendering.SegmentDirection.SegmentDirectionLeft:
				return new Segment(DrawLine.GetPointAtAngle(startPoint, (double)angle, -(distance + (double)offsetX)), DrawLine.GetPointAtAngle(endPoint, (double)angle, (double)offsetX));
			case CustomRendering.SegmentDirection.SegmentDirectionRight:
				return new Segment(DrawLine.GetPointAtAngle(startPoint, (double)angle, (double)offsetX), DrawLine.GetPointAtAngle(endPoint, (double)angle, distance + (double)offsetX));
			default:
				return new Segment(DrawLine.GetPointAtAngle(startPoint, (double)angle, -distance), DrawLine.GetPointAtAngle(endPoint, (double)angle, distance));
			}
		}

		public Segment GetSegment(Point startPoint, Point endPoint, Rectangle boundingRectangle)
		{
			Segment segment = new Segment(startPoint, endPoint);
			Segment segment2 = new Segment();
			if (DrawLine.LineIntersectsRectangle(boundingRectangle, segment.StartPoint, segment.EndPoint))
			{
				segment2 = new Segment(segment.StartPoint, segment.EndPoint);
			}
			if (!segment2.IsValid())
			{
				segment2.Reset();
				return segment2;
			}
			if (DrawLine.LineIsReverse(segment2.StartPoint, segment2.EndPoint))
			{
				Point point = new Point(segment2.StartPoint.X, segment2.StartPoint.Y);
				segment2.StartPoint = new Point(segment2.EndPoint.X, segment2.EndPoint.Y);
				segment2.EndPoint = new Point(point.X, point.Y);
			}
			return segment2;
		}

		public Segment GetParallelSegment(Segment referenceSegment, int distance)
		{
			float num = (float)referenceSegment.StartPoint.X;
			float num2 = (float)referenceSegment.StartPoint.Y;
			float num3 = (float)referenceSegment.EndPoint.X;
			float num4 = (float)referenceSegment.EndPoint.Y;
			float num5 = (float)Math.Sqrt((double)((num - num3) * (num - num3) + (num2 - num4) * (num2 - num4)));
			float num6 = num + (float)distance * (num4 - num2) / num5;
			float num7 = num3 + (float)distance * (num4 - num2) / num5;
			float num8 = num2 + (float)distance * (num - num3) / num5;
			float num9 = num4 + (float)distance * (num - num3) / num5;
			return new Segment(new Point((int)num6, (int)num8), new Point((int)num7, (int)num9));
		}

		public SegmentF GetParallelSegment(SegmentF referenceSegment, int distance)
		{
			float x = referenceSegment.StartPoint.X;
			float y = referenceSegment.StartPoint.Y;
			float x2 = referenceSegment.EndPoint.X;
			float y2 = referenceSegment.EndPoint.Y;
			float num = (float)Math.Sqrt((double)((x - x2) * (x - x2) + (y - y2) * (y - y2)));
			float x3 = x + (float)distance * (y2 - y) / num;
			float x4 = x2 + (float)distance * (y2 - y) / num;
			float y3 = y + (float)distance * (x - x2) / num;
			float y4 = y2 + (float)distance * (x - x2) / num;
			return new SegmentF(new PointF(x3, y3), new PointF(x4, y4));
		}

		public Segment[] GetSegments(Point[] startPoint, Point[] endPoint, DrawPolyLine drawPolyLine)
		{
			Segment[] array = new Segment[2];
			Segment[] array2 = new Segment[2];
			array[0] = new Segment(startPoint[0], endPoint[0]);
			array2[0] = new Segment();
			array[1] = new Segment(startPoint[1], endPoint[1]);
			array2[1] = new Segment();
			RectangleF rectangleF = drawPolyLine.BoundingRectangle;
			if (DrawLine.LineIntersectsRectangle(new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), array[0].StartPoint, array[0].EndPoint) || DrawLine.LineIntersectsRectangle(new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), array[1].StartPoint, array[1].EndPoint))
			{
				array2[0] = new Segment(array[0].StartPoint, array[0].EndPoint);
				array2[1] = new Segment(array[1].StartPoint, array[1].EndPoint);
			}
			if (!array2[0].IsValid())
			{
				array2[0].Reset();
				array2[1].Reset();
				return array2;
			}
			for (int i = 0; i < 2; i++)
			{
				if (DrawLine.LineIsReverse(array2[i].StartPoint, array2[i].EndPoint))
				{
					Point point = new Point(array2[i].StartPoint.X, array2[i].StartPoint.Y);
					array2[i].StartPoint = new Point(array2[i].EndPoint.X, array2[i].EndPoint.Y);
					array2[i].EndPoint = new Point(point.X, point.Y);
				}
			}
			return array2;
		}

		public SegmentF[] GetSegments(PointF[] startPoint, PointF[] endPoint, DrawPolyLine drawPolyLine)
		{
			SegmentF[] array = new SegmentF[2];
			SegmentF[] array2 = new SegmentF[2];
			array[0] = new SegmentF(startPoint[0], endPoint[0]);
			array2[0] = new SegmentF();
			array[1] = new SegmentF(startPoint[1], endPoint[1]);
			array2[1] = new SegmentF();
			RectangleF rectangleF = drawPolyLine.BoundingRectangle;
			if (DrawLine.LineIntersectsRectangle(new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), new Point((int)array[0].StartPoint.X, (int)array[0].StartPoint.Y), new Point((int)array[0].EndPoint.X, (int)array[0].EndPoint.Y)) || DrawLine.LineIntersectsRectangle(new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), new Point((int)array[0].StartPoint.X, (int)array[0].StartPoint.Y), new Point((int)array[0].EndPoint.X, (int)array[0].EndPoint.Y)))
			{
				array2[0] = new SegmentF(array[0].StartPoint, array[0].EndPoint);
				array2[1] = new SegmentF(array[1].StartPoint, array[1].EndPoint);
			}
			if (!array2[0].IsValid())
			{
				array2[0].Reset();
				array2[1].Reset();
				return array2;
			}
			for (int i = 0; i < 2; i++)
			{
				if (DrawLine.LineIsReverse(array2[i].StartPoint, array2[i].EndPoint))
				{
					PointF pointF = new PointF(array2[i].StartPoint.X, array2[i].StartPoint.Y);
					array2[i].StartPoint = new PointF(array2[i].EndPoint.X, array2[i].EndPoint.Y);
					array2[i].EndPoint = new PointF(pointF.X, pointF.Y);
				}
			}
			return array2;
		}

		public virtual void ComputeResults(Graphics g, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
		{
		}

		public virtual void Draw(Graphics g, Region region, int offsetX, int offsetY, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
		{
		}

		public virtual string GetBasicInfo(object data)
		{
			return string.Empty;
		}

		public virtual string GetAdvancedInfo(object data)
		{
			return string.Empty;
		}

		public void Clear()
		{
		}

		public void Dump()
		{
		}

		public CustomRendering()
		{
		}

		public const int MaxLength = 32565;

		[CompilerGenerated]
		private Preset <Preset>k__BackingField;

		[CompilerGenerated]
		private int <Count>k__BackingField;

		public enum SegmentDirection
		{
			SegmentDirectionLeft,
			SegmentDirectionRight,
			SegmentDirectionBoth
		}

		public class PointsRectangle
		{
			public Point[] Points
			{
				get
				{
					return this.points;
				}
			}

			private void Initialize(int left, int right, int top, int bottom)
			{
				this.points[0] = new Point(left, top);
				this.points[1] = new Point(right, top);
				this.points[2] = new Point(right, bottom);
				this.points[3] = new Point(left, bottom);
				this.points[4] = new Point(left, top);
			}

			public PointsRectangle(Rectangle rectangle)
			{
				this.Initialize(rectangle.Left, rectangle.Right, rectangle.Top, rectangle.Bottom);
			}

			public PointsRectangle(RectangleF rectangle)
			{
				this.Initialize((int)rectangle.Left, (int)rectangle.Right, (int)rectangle.Top, (int)rectangle.Bottom);
			}

			public PointsRectangle(int left, int right, int top, int bottom)
			{
				this.Initialize(left, right, top, bottom);
			}

			public void RotateAt(int angle, Point point)
			{
				Matrix matrix = new Matrix();
				matrix.RotateAt((float)angle, point);
				matrix.TransformPoints(this.points);
			}

			public void Translate(float offsetX, float offsetY)
			{
				Matrix matrix = new Matrix();
				matrix.Translate(offsetX, offsetY);
				matrix.TransformPoints(this.points);
			}

			public void RotateAt(int angle, Rectangle boundingRectangle)
			{
				this.RotateAt(angle, new Point(boundingRectangle.X + boundingRectangle.Width / 2, boundingRectangle.Y + boundingRectangle.Height / 2));
			}

			public void RotateAt(int angle, RectangleF boundingRectangle)
			{
				this.RotateAt(angle, new Point((int)boundingRectangle.X + (int)boundingRectangle.Width / 2, (int)boundingRectangle.Y + (int)boundingRectangle.Height / 2));
			}

			public bool IsVisible(Graphics g, Region region)
			{
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddPolygon(this.points);
				Region region2 = new Region(graphicsPath);
				region2.Intersect(region);
				bool result = !region2.IsEmpty(g);
				region2.Dispose();
				graphicsPath.Dispose();
				return result;
			}

			private Point[] points = new Point[5];
		}
	}
}
