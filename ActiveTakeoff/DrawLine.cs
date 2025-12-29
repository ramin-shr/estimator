using QuoterPlanControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DrawLine : DrawObject
    {
        private Point startPoint;
        private Point endPoint;

        private double height;
        private double lastDistance;

        private readonly SlopeFactor slopeFactor = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyDisabled);

        private bool figureCompleted;

        private int toolTipMinThreshold = 50;
        private int toolTipMaxThreshold = 100;

        public override int ActiveHandleCount => this.FigureCompleted ? 2 : 1;

        public override Rectangle BoundingRectangle => this.GetNormalizedRectangle();

        public override Point Center => this.ComputeCenter();

        public Point EndPoint
        {
            get => this.endPoint;
            set => this.endPoint = value;
        }

        public bool FigureCompleted
        {
            get => this.figureCompleted;
            set => this.figureCompleted = value;
        }

        public override int HandleCount => 2;

        public double Height
        {
            get => this.height;
            set => this.height = value;
        }

        public override SlopeFactor SlopeFactor => this.slopeFactor;

        public Point StartPoint
        {
            get => this.startPoint;
            set => this.startPoint = value;
        }

        public DrawLine()
        {
            this.startPoint = new Point(0, 0);
            this.endPoint = new Point(1, 1);

            base.ZOrder = 0;
            this.FigureCompleted = false;
            base.Initialize();
        }

        public DrawLine(int x1, int y1, int x2, int y2, PointF offset, string name, int groupID, string comment)
        {
            this.startPoint = new Point(x1, y1);
            this.endPoint = new Point(x2, y2);

            base.Offset = offset;
            base.ZOrder = 0;
            base.Name = name;
            base.GroupID = groupID;
            base.Comment = comment;

            this.FigureCompleted = false;
            base.Initialize();
        }

        public DrawLine(
            int x1,
            int y1,
            int x2,
            int y2,
            PointF offset,
            string name,
            int groupID,
            string comment,
            bool figureCompleted,
            Color lineColor,
            int opacity,
            int lineWidth)
        {
            this.startPoint = new Point(x1, y1);
            this.endPoint = new Point(x2, y2);

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

        public static double Angle(double px1, double py1, double px2, double py2)
        {
            double dx = px2 - px1;
            double dy = py2 - py1;

            double angleRad;

            if (dx == 0)
            {
                angleRad = (dy < 0) ? 4.71238898038469 : 1.5707963267949;
            }
            else if (dy == 0)
            {
                angleRad = (dx > 0) ? 0 : 3.14159265358979;
            }
            else if (dx >= 0)
            {
                angleRad = (dy >= 0) ? Math.Atan(dy / dx) : Math.Atan(dy / dx) + 6.28318530717959;
            }
            else
            {
                angleRad = Math.Atan(dy / dx) + 3.14159265358979;
            }

            return angleRad * 180 / 3.14159265358979;
        }

        public override DrawObject Clone()
        {
            DrawLine copy = new DrawLine
            {
                StartPoint = new Point(this.StartPoint.X, this.StartPoint.Y),
                EndPoint = new Point(this.EndPoint.X, this.EndPoint.Y),
                Height = this.Height,
                FigureCompleted = true
            };

            copy.SetSlopeFactor(this.slopeFactor);
            copy.DisplayName = new Utilities.DisplayName(copy, "");
            base.FillDrawObjectFields(copy);

            return copy;
        }

        private Point ComputeCenter()
        {
            Rectangle bounds = this.GetNormalizedRectangle();
            return new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        }

        public int Distance2D(Point startPoint, Point endPoint)
        {
            return GetDistance(startPoint, endPoint);
        }

        public int Distance2D(bool applySlope)
        {
            if (!applySlope)
                return this.Distance2D(this.StartPoint, this.EndPoint);

            int distanceBeforeSlope = 0;
            return this.Distance2D(
                this.StartPoint.X,
                this.StartPoint.Y,
                this.EndPoint.X,
                this.EndPoint.Y,
                true,
                ref distanceBeforeSlope);
        }

        public int Distance2D(int x1, int y1, int x2, int y2, bool applySlopeOnPlan, ref int distanceBeforeSlope)
        {
            int distance = this.Distance2D(new Point(x1, y1), new Point(x2, y2));
            distanceBeforeSlope = distance;

            if (this.SlopeFactor.InternalValue > 0 &&
                (this.SlopeFactor.SlopeApplyType != SlopeFactor.SlopeApplyTypeEnum.applyOnPlan ||
                 (this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnPlan && applySlopeOnPlan)))
            {
                double angleDeg = Angle(x1, y1, x2, y2);
                Console.WriteLine("angle = " + angleDeg);

                if (this.SlopeFactor.HipValley == SlopeFactor.HipValleyEnum.hipValleyEnabled &&
                    this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnElevation)
                {
                    distance = (int)this.SlopeFactor.Apply(distance, Math.Abs(x2 - x1), Math.Abs(y2 - y1));
                }
                else if ((angleDeg >= 89 && angleDeg <= 91) ||
                         (angleDeg >= 269 && angleDeg <= 271) ||
                         this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnPlan)
                {
                    distance = (int)this.SlopeFactor.Apply(distance, 0, 0);
                }
            }

            return distance;
        }

        public override void Draw(
            Graphics g,
            int offsetX,
            int offsetY,
            bool printToScreen = true,
            MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
        {
            SmoothingMode previousSmoothing = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;

            TextRenderingHint previousTextHint = g.TextRenderingHint;
            g.TextRenderingHint = printToScreen
                ? (imageQuality == MainForm.ImageQualityEnum.QualityHigh ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel)
                : TextRenderingHint.ClearTypeGridFit;

            Pen pen = base.DrawPen != null
                ? (Pen)base.DrawPen.Clone()
                : new Pen(Color.FromArgb(base.Opacity + 30, base.Color), (float)base.PenWidth);

            base.ClearTextArray();

            int distanceBeforeSlope = 0;
            int distance = this.Distance2D(
                this.StartPoint.X,
                this.StartPoint.Y,
                this.EndPoint.X,
                this.EndPoint.Y,
                true,
                ref distanceBeforeSlope);

            double scaledBaseDistance = distanceBeforeSlope * base.DrawingBoard.ZoomFactor;
            int thresholdBonus = base.DrawArea.UnitScaleIsImperial() ? 10 : 0;

            if (scaledBaseDistance >= (this.toolTipMinThreshold + thresholdBonus))
            {
                bool compact = scaledBaseDistance <= this.toolTipMaxThreshold;

                string text = base.DisplayInPixels
                    ? distance + " pixels"
                    : base.DrawArea.ToLengthString(distance, compact);

                base.TextArray.Add(new DrawText(text, this.Center, this.TextAngle(this.startPoint, this.endPoint)));
            }

            this.lastDistance = distanceBeforeSlope;

            using (GraphicsPath path = new GraphicsPath())
            {
                Point p1 = new Point(this.startPoint.X - offsetX, this.startPoint.Y - offsetY);
                Point p2 = new Point(this.endPoint.X - offsetX, this.endPoint.Y - offsetY);

                path.AddLine(p1, p2);

                if (base.Rotation != 0)
                {
                    RectangleF bounds = path.GetBounds();
                    using (Matrix transform = new Matrix())
                    {
                        transform.RotateAt(
                            (float)base.Rotation,
                            new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f),
                            MatrixOrder.Append);

                        path.Transform(transform);
                    }
                }

                g.DrawPath(pen, path);
            }

            pen.Dispose();

            g.TextRenderingHint = previousTextHint;
            g.SmoothingMode = previousSmoothing;
        }

        public override void DrawText(
            Graphics g,
            int offsetX,
            int offsetY,
            bool printToScreen = true,
            MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh,
            float defaultFontSize = 12f)
        {
            if (!base.ShowMeasure || base.TextArray.Count == 0)
                return;

            if (base.DrawingBoard.ZoomFactor >= 0.15)
            {
                ((DrawText)base.TextArray[0]).Draw(
                    g,
                    offsetX,
                    offsetY,
                    (float)base.DrawingBoard.ZoomFactor,
                    base.Opacity,
                    printToScreen,
                    imageQuality,
                    defaultFontSize);
            }

            base.TextDirty = false;
        }

        public static double FindDistanceToSegment(Point pt, Point p1, Point p2)
        {
            PointF closest;
            return FindDistanceToSegment(pt, p1, p2, out closest);
        }

        public static double FindDistanceToSegment(Point pt, Point p1, Point p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            if (dx == 0f && dy == 0f)
            {
                closest = p1;
                float ax = pt.X - p1.X;
                float ay = pt.Y - p1.Y;
                return Math.Sqrt(ax * ax + ay * ay);
            }

            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            if (t < 0f)
            {
                closest = new Point(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t <= 1f)
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }
            else
            {
                closest = new Point(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static int GetDistance(Point startPoint, Point endPoint)
        {
            int dx = endPoint.X - startPoint.X;
            int dy = endPoint.Y - startPoint.Y;

            double dx2 = Math.Pow(dx, 2);
            double dy2 = Math.Pow(dy, 2);

            return (int)Math.Sqrt(dx2 + dy2);
        }

        public override Point GetHandle(int handleNumber)
        {
            if (handleNumber < 1) handleNumber = 1;
            if (handleNumber > 2) handleNumber = 2;

            return handleNumber == 1 ? this.StartPoint : this.EndPoint;
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

        public override int GetHandleSize()
        {
            return 4;
        }

        private Rectangle GetNormalizedRectangle()
        {
            int left, right;
            int top, bottom;

            if (this.startPoint.X > this.endPoint.X)
            {
                left = this.endPoint.X;
                right = this.startPoint.X;
            }
            else
            {
                left = this.startPoint.X;
                right = this.endPoint.X;
            }

            if (this.startPoint.Y > this.endPoint.Y)
            {
                top = this.endPoint.Y;
                bottom = this.startPoint.Y;
            }
            else
            {
                top = this.startPoint.Y;
                bottom = this.endPoint.Y;
            }

            int width = right - left;
            int height = bottom - top;

            return new Rectangle(left, top, width + 1, height + 1);
        }

        public static Point GetPointAtAngle(Point startPoint, double angle, double distance)
        {
            return new Point(
                (int)Math.Ceiling(startPoint.X + distance * Math.Cos(Utilities.DegreeToRadian(angle))),
                (int)Math.Ceiling(startPoint.Y + distance * Math.Sin(Utilities.DegreeToRadian(angle))));
        }

        public static PointF GetPointAtAngle(PointF startPoint, double angle, double distance)
        {
            return new PointF(
                (float)Math.Ceiling(startPoint.X + distance * Math.Cos(Utilities.DegreeToRadian(angle))),
                (float)Math.Ceiling(startPoint.Y + distance * Math.Sin(Utilities.DegreeToRadian(angle))));
        }

        // Clean replacement for the decompiler-generated iterator state machine.
        // Logic is unchanged (Bresenham-style line stepping, including steep/swap behavior).
        public static IEnumerable<Point> GetPointsOnLine(int x0, int y0, int x1, int y1)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                int tmp = x0; x0 = y0; y0 = tmp;
                tmp = x1; x1 = y1; y1 = tmp;
            }

            if (x0 > x1)
            {
                int tmp = x0; x0 = x1; x1 = tmp;
                tmp = y0; y0 = y1; y1 = tmp;
            }

            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2;
            int ystep = (y0 < y1) ? 1 : -1;
            int y = y0;

            for (int x = x0; x <= x1; x++)
            {
                yield return new Point(steep ? y : x, steep ? x : y);

                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }

        public override int HitTest(Point point, int offsetX, int offsetY)
        {
            if (this.Selected)
            {
                for (int handle = 1; handle <= this.HandleCount; handle++)
                {
                    using (GraphicsPath handlePath = new GraphicsPath())
                    {
                        handlePath.AddRectangle(this.GetHandleRectangle(handle, offsetX, offsetY));
                        if (handlePath.IsVisible(point))
                            return handle;
                    }
                }
            }

            return this.PointInObject(point, offsetX, offsetY) ? 0 : -1;
        }

        public override bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
        {
            Rectangle shifted = new Rectangle(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height);
            return LineIntersectsRectangle(shifted, this.startPoint, this.endPoint);
        }

        protected void Invalidate()
        {
        }

        public static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float numerator = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float denominator = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (denominator == 0f)
                return false;

            float ua = numerator / denominator;

            numerator = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float ub = numerator / denominator;

            return ua >= 0f && ua <= 1f && ub >= 0f && ub <= 1f;
        }

        public static bool LineIntersectsLine(PointF l1p1, PointF l1p2, PointF l2p1, PointF l2p2)
        {
            float numerator = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float denominator = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (denominator == 0f)
                return false;

            float ua = numerator / denominator;

            numerator = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float ub = numerator / denominator;

            return ua >= 0f && ua <= 1f && ub >= 0f && ub <= 1f;
        }

        public static bool LineIntersectsRectangle(Rectangle rectangle, Point p1, Point p2)
        {
            if (LineIntersectsLine(p1, p2, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y)) ||
                LineIntersectsLine(p1, p2, new Point(rectangle.X + rectangle.Width, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)) ||
                LineIntersectsLine(p1, p2, new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), new Point(rectangle.X, rectangle.Y + rectangle.Height)) ||
                LineIntersectsLine(p1, p2, new Point(rectangle.X, rectangle.Y + rectangle.Height), new Point(rectangle.X, rectangle.Y)))
            {
                return true;
            }

            if (!rectangle.Contains(p1))
                return false;

            return rectangle.Contains(p2);
        }

        public static bool LineIsReverse(Point startPoint, Point endPoint)
        {
            bool reverse;

            if (endPoint.X == startPoint.X && endPoint.Y != startPoint.Y)
            {
                reverse = endPoint.Y < startPoint.Y;
            }
            else if (endPoint.X == startPoint.X || endPoint.Y != startPoint.Y)
            {
                reverse = (Math.Abs(endPoint.X - startPoint.X) <= Math.Abs(endPoint.Y - startPoint.Y))
                    ? endPoint.Y < startPoint.Y
                    : endPoint.X < startPoint.X;
            }
            else
            {
                reverse = endPoint.X < startPoint.X;
            }

            return reverse;
        }

        public static bool LineIsReverse(PointF startPoint, PointF endPoint)
        {
            bool reverse;

            if (endPoint.X == startPoint.X && endPoint.Y != startPoint.Y)
            {
                reverse = endPoint.Y < startPoint.Y;
            }
            else if (endPoint.X == startPoint.X || endPoint.Y != startPoint.Y)
            {
                reverse = (Math.Abs(endPoint.X - startPoint.X) <= Math.Abs(endPoint.Y - startPoint.Y))
                    ? endPoint.Y < startPoint.Y
                    : endPoint.X < startPoint.X;
            }
            else
            {
                reverse = endPoint.X < startPoint.X;
            }

            return reverse;
        }

        public Point LocatePointOnLine(Point pt, Point p1, Point p2)
        {
            PointF closest;
            FindDistanceToSegment(pt, p1, p2, out closest);
            return new Point((int)closest.X, (int)closest.Y);
        }

        public override void Move(int deltaX, int deltaY)
        {
            this.startPoint = new Point(this.startPoint.X + deltaX, this.startPoint.Y + deltaY);
            this.endPoint = new Point(this.endPoint.X + deltaX, this.endPoint.Y + deltaY);

            base.Dirty = true;
            this.Invalidate();
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            PointF origin = base.DrawingBoard.Origin;

            if (handleNumber != 1)
            {
                this.endPoint = new Point(point.X + (int)origin.X, point.Y + (int)origin.Y);
            }
            else
            {
                this.startPoint = new Point(point.X + (int)origin.X, point.Y + (int)origin.Y);
            }

            base.Dirty = true;
            this.Invalidate();
        }

        public void NormalizeLine()
        {
            if (LineIsReverse(this.startPoint, this.endPoint))
            {
                Point temp = new Point(this.startPoint.X, this.startPoint.Y);
                this.startPoint = new Point(this.endPoint.X, this.endPoint.Y);
                this.endPoint = new Point(temp.X, temp.Y);
            }
        }

        protected override bool PointInObject(Point point, int offsetX, int offsetY)
        {
            double distanceToSegment = FindDistanceToSegment(
                new Point(point.X + offsetX, point.Y + offsetY),
                this.startPoint,
                this.endPoint);

            if (distanceToSegment < 0)
                return false;

            return distanceToSegment <= (double)(base.PenWidth / 2);
        }

        public override Region Region(int offsetX, int offsetY, float zoomFactor)
        {
            Region region = new Region();

            offsetX -= (int)((float)base.DrawArea.DrawingBoard.HorizontalOffset / zoomFactor);
            offsetY -= (int)((float)base.DrawArea.DrawingBoard.VerticalOffset / zoomFactor);

            Rectangle bounds = this.GetNormalizedRectangle();

            bounds.X -= offsetX;
            bounds.Y -= offsetY;

            bounds.X = (int)(bounds.X * zoomFactor);
            bounds.Y = (int)(bounds.Y * zoomFactor);
            bounds.Width = (int)(bounds.Width * zoomFactor);
            bounds.Height = (int)(bounds.Height * zoomFactor);

            bounds.Inflate(new Size(10 + base.PenWidth, 10 + base.PenWidth));

            region.MakeEmpty();
            region.Union(bounds);

            if (base.TextArray.Count > 0)
            {
                DrawText label = (DrawText)base.TextArray[0];

                int labelX = label.Point.X - offsetX - label.Rectangle.Width / 2;
                int labelY = label.Point.Y - offsetY - label.Rectangle.Height / 2;

                Rectangle labelRect = new Rectangle(labelX, labelY, label.Rectangle.Width, label.Rectangle.Height)
                {
                    X = (int)(labelX * zoomFactor),
                    Y = (int)(labelY * zoomFactor),
                    Width = (int)(label.Rectangle.Width * zoomFactor),
                    Height = (int)(label.Rectangle.Height * zoomFactor)
                };

                labelRect.Inflate(new Size(base.PenWidth, base.PenWidth));
                region.Union(labelRect);
            }

            for (int handle = 1; handle <= this.HandleCount; handle++)
            {
                Rectangle handleRect = this.GetHandleRectangle(handle, offsetX, offsetY);

                handleRect.X = (int)(handleRect.X * zoomFactor);
                handleRect.Y = (int)(handleRect.Y * zoomFactor);
                handleRect.Width = (int)(handleRect.Width * zoomFactor);
                handleRect.Height = (int)(handleRect.Height * zoomFactor);

                handleRect.Inflate(new Size(base.PenWidth, base.PenWidth));
                region.Union(handleRect);
            }

            return region;
        }

        public double RoundAngle(double angle, int precision)
        {
            if (angle == 0 || angle == 90 || angle == 180 || angle == 270 || angle == 360)
                return angle == 360 ? 0 : angle;

            int[] snapAngles = { 0, 90, 180, 270, 360 };
            double[] diffs =
            {
                Math.Abs(angle - 0),
                Math.Abs(angle - 90),
                Math.Abs(angle - 180),
                Math.Abs(angle - 270),
                Math.Abs(angle - 360)
            };

            int bestIndex = 0;
            for (int i = 1; i < diffs.Length; i++)
            {
                if (diffs[i] < diffs[bestIndex])
                    bestIndex = i;
            }

            if (diffs[bestIndex] > precision)
                return angle;

            return snapAngles[bestIndex];
        }

        public override void Scale(float scaleX, float scaleY)
        {
            this.startPoint = new Point((int)(this.startPoint.X * scaleX), (int)(this.startPoint.Y * scaleY));
            this.endPoint = new Point((int)(this.endPoint.X * scaleX), (int)(this.endPoint.Y * scaleY));
        }

        public override void SetSlopeFactor(SlopeFactor slopeFactor)
        {
            this.SlopeFactor.SetValues(slopeFactor);
        }

        public override void SetSlopeFactor(double internalValue, SlopeFactor.SlopeTypeEnum slopeType, SlopeFactor.SlopeApplyTypeEnum slopeApplyType, SlopeFactor.HipValleyEnum hipValley)
        {
            SlopeFactor sf = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyDisabled);
            sf.SetValues(internalValue, slopeType, slopeApplyType, hipValley);
            this.SetSlopeFactor(sf);
        }

        public float Slope(PointF point1, PointF point2)
        {
            return (point2.Y - point1.Y) / (point2.X - point1.X);
        }

        public int TextAngle(Point startPoint, Point endPoint)
        {
            int result = 0;

            int angleDeg = (int)Angle(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);

            if (angleDeg >= 0 && angleDeg <= 90)
                result = angleDeg;

            if (angleDeg > 90 && angleDeg < 180)
                result = 0x10e + (angleDeg - 90);

            if (angleDeg >= 180 && angleDeg < 0x10e)
                result = angleDeg - 180;

            if (angleDeg >= 0x10e)
                result = 0x10e + (angleDeg - 0x10e);

            return Math.Abs(result);
        }
    }
}
