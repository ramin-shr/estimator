using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class CustomRendering
    {
        public const int MaxLength = 0x7f35;

        public int Count
        {
            get;
            set;
        }

        public Preset Preset
        {
            get;
            set;
        }

        public CustomRendering()
        {
        }

        public void Clear()
        {
        }

        public virtual void ComputeResults(Graphics g, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
        }

        public virtual void Draw(Graphics g, Region region, int offsetX, int offsetY, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
        }

        public void Dump()
        {
        }

        public double FindDistanceBetweenSegments(PointF p1, PointF p2, PointF p3, PointF p4, out PointF close1, out PointF close2)
        {
            bool flag;
            bool flag1;
            PointF pointF;
            PointF pointF1;
            this.FindIntersection(p1, p2, p3, p4, out flag, out flag1, out pointF, out close1, out close2);
            if (flag1)
            {
                close1 = pointF;
                close2 = pointF;
                return 0;
            }
            double num = double.MaxValue;
            double segment = this.FindDistanceToSegment(p1, p3, p4, out pointF1);
            if (segment < num)
            {
                num = segment;
                close1 = p1;
                close2 = pointF1;
            }
            segment = this.FindDistanceToSegment(p2, p3, p4, out pointF1);
            if (segment < num)
            {
                num = segment;
                close1 = p2;
                close2 = pointF1;
            }
            segment = this.FindDistanceToSegment(p3, p1, p2, out pointF1);
            if (segment < num)
            {
                num = segment;
                close1 = pointF1;
                close2 = p3;
            }
            segment = this.FindDistanceToSegment(p4, p1, p2, out pointF1);
            if (segment < num)
            {
                num = segment;
                close1 = pointF1;
                close2 = p4;
            }
            return num;
        }

        private double FindDistanceToSegment(PointF pt, PointF p1, PointF p2, out PointF closest)
        {
            float x = p2.X - p1.X;
            float y = p2.Y - p1.Y;
            if (x == 0f && y == 0f)
            {
                closest = p1;
                x = pt.X - p1.X;
                y = pt.Y - p1.Y;
                return Math.Sqrt((double)(x * x + y * y));
            }
            float single = ((pt.X - p1.X) * x + (pt.Y - p1.Y) * y) / (x * x + y * y);
            if (single < 0f)
            {
                closest = new PointF(p1.X, p1.Y);
                x = pt.X - p1.X;
                y = pt.Y - p1.Y;
            }
            else if (single <= 1f)
            {
                closest = new PointF(p1.X + single * x, p1.Y + single * y);
                x = pt.X - closest.X;
                y = pt.Y - closest.Y;
            }
            else
            {
                closest = new PointF(p2.X, p2.Y);
                x = pt.X - p2.X;
                y = pt.Y - p2.Y;
            }
            return Math.Sqrt((double)(x * x + y * y));
        }

        private void FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4, out bool lines_intersect, out bool segments_intersect, out PointF intersection, out PointF close_p1, out PointF close_p2)
        {
            float x;
            float single = p2.X - p1.X;
            float y = p2.Y - p1.Y;
            float x1 = p4.X - p3.X;
            float y1 = p4.Y - p3.Y;
            float single1 = y * x1 - single * y1;
            try
            {
                x = ((p1.X - p3.X) * y1 + (p3.Y - p1.Y) * x1) / single1;
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
            float x2 = ((p3.X - p1.X) * y + (p1.Y - p3.Y) * single) / -single1;
            intersection = new PointF(p1.X + single * x, p1.Y + y * x);
            segments_intersect = (x < 0f || x > 1f || x2 < 0f ? false : x2 <= 1f);
            if (x < 0f)
            {
                x = 0f;
            }
            else if (x > 1f)
            {
                x = 1f;
            }
            if (x2 < 0f)
            {
                x2 = 0f;
            }
            else if (x2 > 1f)
            {
                x2 = 1f;
            }
            close_p1 = new PointF(p1.X + single * x, p1.Y + y * x);
            close_p2 = new PointF(p3.X + x1 * x2, p3.Y + y1 * x2);
        }

        public virtual string GetAdvancedInfo(object data)
        {
            return string.Empty;
        }

        public virtual string GetBasicInfo(object data)
        {
            return string.Empty;
        }

        public Segment GetParallelSegment(Segment referenceSegment, int distance)
        {
            float x = (float)referenceSegment.StartPoint.X;
            float y = (float)referenceSegment.StartPoint.Y;
            float single = (float)referenceSegment.EndPoint.X;
            float y1 = (float)referenceSegment.EndPoint.Y;
            float single1 = (float)Math.Sqrt((double)((x - single) * (x - single) + (y - y1) * (y - y1)));
            float single2 = x + (float)distance * (y1 - y) / single1;
            float single3 = single + (float)distance * (y1 - y) / single1;
            float single4 = y + (float)distance * (x - single) / single1;
            float single5 = y1 + (float)distance * (x - single) / single1;
            return new Segment(new Point((int)single2, (int)single4), new Point((int)single3, (int)single5));
        }

        public SegmentF GetParallelSegment(SegmentF referenceSegment, int distance)
        {
            float x = referenceSegment.StartPoint.X;
            float y = referenceSegment.StartPoint.Y;
            float single = referenceSegment.EndPoint.X;
            float y1 = referenceSegment.EndPoint.Y;
            float single1 = (float)Math.Sqrt((double)((x - single) * (x - single) + (y - y1) * (y - y1)));
            float single2 = x + (float)distance * (y1 - y) / single1;
            float single3 = single + (float)distance * (y1 - y) / single1;
            float single4 = y + (float)distance * (x - single) / single1;
            float single5 = y1 + (float)distance * (x - single) / single1;
            return new SegmentF(new PointF(single2, single4), new PointF(single3, single5));
        }

        public Segment GetSegment(Point startPoint, Point endPoint, Rectangle boundingRectangle)
        {
            Segment segment = new Segment(startPoint, endPoint);
            Segment point = new Segment();
            if (DrawLine.LineIntersectsRectangle(boundingRectangle, segment.StartPoint, segment.EndPoint))
            {
                point = new Segment(segment.StartPoint, segment.EndPoint);
            }
            if (!point.IsValid())
            {
                point.Reset();
                return point;
            }
            if (DrawLine.LineIsReverse(point.StartPoint, point.EndPoint))
            {
                Point point1 = new Point(point.StartPoint.X, point.StartPoint.Y);
                point.StartPoint = new Point(point.EndPoint.X, point.EndPoint.Y);
                point.EndPoint = new Point(point1.X, point1.Y);
            }
            return point;
        }

        public Segment GetSegmentAtAngle(Point startPoint, Point endPoint, int angle, double distance = 32565, CustomRendering.SegmentDirection segmentDirection = SegmentDirection.SegmentDirectionBoth, int offsetX = 0)
        {
            switch (segmentDirection)
            {
                case CustomRendering.SegmentDirection.SegmentDirectionLeft:
                    {
                        return new Segment(DrawLine.GetPointAtAngle(startPoint, (double)angle, -(distance + (double)offsetX)), DrawLine.GetPointAtAngle(endPoint, (double)angle, (double)offsetX));
                    }
                case CustomRendering.SegmentDirection.SegmentDirectionRight:
                    {
                        return new Segment(DrawLine.GetPointAtAngle(startPoint, (double)angle, (double)offsetX), DrawLine.GetPointAtAngle(endPoint, (double)angle, distance + (double)offsetX));
                    }
            }
            return new Segment(DrawLine.GetPointAtAngle(startPoint, (double)angle, -distance), DrawLine.GetPointAtAngle(endPoint, (double)angle, distance));
        }

        public Segment[] GetSegments(Point[] startPoint, Point[] endPoint, DrawPolyLine drawPolyLine)
        {
            Segment[] segment = new Segment[2];
            Segment[] point = new Segment[2];
            segment[0] = new Segment(startPoint[0], endPoint[0]);
            point[0] = new Segment();
            segment[1] = new Segment(startPoint[1], endPoint[1]);
            point[1] = new Segment();
            RectangleF boundingRectangle = drawPolyLine.BoundingRectangle;
            if (DrawLine.LineIntersectsRectangle(new Rectangle((int)boundingRectangle.X, (int)boundingRectangle.Y, (int)boundingRectangle.Width, (int)boundingRectangle.Height), segment[0].StartPoint, segment[0].EndPoint) || DrawLine.LineIntersectsRectangle(new Rectangle((int)boundingRectangle.X, (int)boundingRectangle.Y, (int)boundingRectangle.Width, (int)boundingRectangle.Height), segment[1].StartPoint, segment[1].EndPoint))
            {
                point[0] = new Segment(segment[0].StartPoint, segment[0].EndPoint);
                point[1] = new Segment(segment[1].StartPoint, segment[1].EndPoint);
            }
            if (!point[0].IsValid())
            {
                point[0].Reset();
                point[1].Reset();
                return point;
            }
            for (int i = 0; i < 2; i++)
            {
                if (DrawLine.LineIsReverse(point[i].StartPoint, point[i].EndPoint))
                {
                    int x = point[i].StartPoint.X;
                    Point point1 = point[i].StartPoint;
                    Point point2 = new Point(x, point1.Y);
                    Segment segment1 = point[i];
                    int num = point[i].EndPoint.X;
                    Point point3 = point[i].EndPoint;
                    segment1.StartPoint = new Point(num, point3.Y);
                    point[i].EndPoint = new Point(point2.X, point2.Y);
                }
            }
            return point;
        }

        public SegmentF[] GetSegments(PointF[] startPoint, PointF[] endPoint, DrawPolyLine drawPolyLine)
        {
            SegmentF[] segmentF = new SegmentF[2];
            SegmentF[] pointF = new SegmentF[2];
            segmentF[0] = new SegmentF(startPoint[0], endPoint[0]);
            pointF[0] = new SegmentF();
            segmentF[1] = new SegmentF(startPoint[1], endPoint[1]);
            pointF[1] = new SegmentF();
            RectangleF boundingRectangle = drawPolyLine.BoundingRectangle;
            if (DrawLine.LineIntersectsRectangle(new Rectangle((int)boundingRectangle.X, (int)boundingRectangle.Y, (int)boundingRectangle.Width, (int)boundingRectangle.Height), new Point((int)segmentF[0].StartPoint.X, (int)segmentF[0].StartPoint.Y), new Point((int)segmentF[0].EndPoint.X, (int)segmentF[0].EndPoint.Y)) || DrawLine.LineIntersectsRectangle(new Rectangle((int)boundingRectangle.X, (int)boundingRectangle.Y, (int)boundingRectangle.Width, (int)boundingRectangle.Height), new Point((int)segmentF[0].StartPoint.X, (int)segmentF[0].StartPoint.Y), new Point((int)segmentF[0].EndPoint.X, (int)segmentF[0].EndPoint.Y)))
            {
                pointF[0] = new SegmentF(segmentF[0].StartPoint, segmentF[0].EndPoint);
                pointF[1] = new SegmentF(segmentF[1].StartPoint, segmentF[1].EndPoint);
            }
            if (!pointF[0].IsValid())
            {
                pointF[0].Reset();
                pointF[1].Reset();
                return pointF;
            }
            for (int i = 0; i < 2; i++)
            {
                if (DrawLine.LineIsReverse(pointF[i].StartPoint, pointF[i].EndPoint))
                {
                    float x = pointF[i].StartPoint.X;
                    PointF pointF1 = pointF[i].StartPoint;
                    PointF pointF2 = new PointF(x, pointF1.Y);
                    SegmentF segmentF1 = pointF[i];
                    float single = pointF[i].EndPoint.X;
                    PointF pointF3 = pointF[i].EndPoint;
                    segmentF1.StartPoint = new PointF(single, pointF3.Y);
                    pointF[i].EndPoint = new PointF(pointF2.X, pointF2.Y);
                }
            }
            return pointF;
        }

        public Point LineIntersectionPoint(Point ps1, Point pe1, Point ps2, Point pe2)
        {
            float y = (float)(pe1.Y - ps1.Y);
            float x = (float)(ps1.X - pe1.X);
            float single = y * (float)ps1.X + x * (float)ps1.Y;
            float y1 = (float)(pe2.Y - ps2.Y);
            float x1 = (float)(ps2.X - pe2.X);
            float single1 = y1 * (float)ps2.X + x1 * (float)ps2.Y;
            float single2 = y * x1 - y1 * x;
            if (single2 == 0f)
            {
                return Point.Empty;
            }
            return new Point((int)((x1 * single - x * single1) / single2), (int)((y * single1 - y1 * single) / single2));
        }

        public PointF LineIntersectionPoint(PointF ps1, PointF pe1, PointF ps2, PointF pe2)
        {
            float y = pe1.Y - ps1.Y;
            float x = ps1.X - pe1.X;
            float single = y * ps1.X + x * ps1.Y;
            float y1 = pe2.Y - ps2.Y;
            float x1 = ps2.X - pe2.X;
            float single1 = y1 * ps2.X + x1 * ps2.Y;
            float single2 = y * x1 - y1 * x;
            if (single2 == 0f)
            {
                return PointF.Empty;
            }
            return new PointF((x1 * single - x * single1) / single2, (y * single1 - y1 * single) / single2);
        }

        public class PointsRectangle
        {
            private Point[] points = new Point[5];

            public Point[] Points
            {
                get
                {
                    return this.points;
                }
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

            private void Initialize(int left, int right, int top, int bottom)
            {
                this.points[0] = new Point(left, top);
                this.points[1] = new Point(right, top);
                this.points[2] = new Point(right, bottom);
                this.points[3] = new Point(left, bottom);
                this.points[4] = new Point(left, top);
            }

            public bool IsVisible(Graphics g, Region region)
            {
                GraphicsPath graphicsPath = new GraphicsPath();
                graphicsPath.AddPolygon(this.points);
                Region region1 = new Region(graphicsPath);
                region1.Intersect(region);
                bool flag = !region1.IsEmpty(g);
                region1.Dispose();
                graphicsPath.Dispose();
                return flag;
            }

            public void RotateAt(int angle, Point point)
            {
                Matrix matrix = new Matrix();
                matrix.RotateAt((float)angle, point);
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

            public void Translate(float offsetX, float offsetY)
            {
                Matrix matrix = new Matrix();
                matrix.Translate(offsetX, offsetY);
                matrix.TransformPoints(this.points);
            }
        }

        public enum SegmentDirection
        {
            SegmentDirectionLeft,
            SegmentDirectionRight,
            SegmentDirectionBoth
        }
    }
}