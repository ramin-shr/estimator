using QuoterPlanControls;
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
        private Point startPoint;

        private Point endPoint;

        private double height;

        private double lastDistance;

        private SlopeFactor slopeFactor = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyDisabled);

        private bool figureCompleted;

        private int toolTipMinThreshold = 50;

        private int toolTipMaxThreshold = 100;

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

        public override int HandleCount
        {
            get
            {
                return 2;
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

        public static double Angle(double px1, double py1, double px2, double py2)
        {
            double num = px2 - px1;
            double num1 = py2 - py1;
            double num2 = 0;
            if (num == 0)
            {
                num2 = (num1 < 0 ? 4.71238898038469 : 1.5707963267949);
            }
            else if (num1 == 0)
            {
                num2 = (num > 0 ? 0 : 3.14159265358979);
            }
            else if (num >= 0)
            {
                num2 = (num1 >= 0 ? Math.Atan(num1 / num) : Math.Atan(num1 / num) + 6.28318530717959);
            }
            else
            {
                num2 = Math.Atan(num1 / num) + 3.14159265358979;
            }
            num2 = num2 * 180 / 3.14159265358979;
            return num2;
        }

        public override DrawObject Clone()
        {
            DrawLine drawLine = new DrawLine()
            {
                StartPoint = new Point(this.StartPoint.X, this.StartPoint.Y),
                EndPoint = new Point(this.EndPoint.X, this.EndPoint.Y),
                Height = this.Height,
                FigureCompleted = true
            };
            drawLine.SetSlopeFactor(this.slopeFactor);
            drawLine.DisplayName = new Utilities.DisplayName(drawLine, "");
            base.FillDrawObjectFields(drawLine);
            return drawLine;
        }

        private Point ComputeCenter()
        {
            Rectangle normalizedRectangle = this.GetNormalizedRectangle();
            return new Point(normalizedRectangle.X + normalizedRectangle.Width / 2, normalizedRectangle.Y + normalizedRectangle.Height / 2);
        }

        public int Distance2D(Point startPoint, Point endPoint)
        {
            return DrawLine.GetDistance(startPoint, endPoint);
        }

        public int Distance2D(bool applySlope)
        {
            int num = 0;
            if (!applySlope)
            {
                return this.Distance2D(this.StartPoint, this.EndPoint);
            }
            int x = this.StartPoint.X;
            int y = this.StartPoint.Y;
            int x1 = this.EndPoint.X;
            Point endPoint = this.EndPoint;
            return this.Distance2D(x, y, x1, endPoint.Y, true, ref num);
        }

        public int Distance2D(int x1, int y1, int x2, int y2, bool applySlopeOnPlan, ref int distanceBeforeSlope)
        {
            int num = this.Distance2D(new Point(x1, y1), new Point(x2, y2));
            distanceBeforeSlope = num;
            if (this.SlopeFactor.InternalValue > 0 && (this.SlopeFactor.SlopeApplyType != SlopeFactor.SlopeApplyTypeEnum.applyOnPlan || this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnPlan && applySlopeOnPlan))
            {
                double num1 = DrawLine.Angle((double)x1, (double)y1, (double)x2, (double)y2);
                Console.WriteLine(string.Concat("angle = ", num1));
                if (this.SlopeFactor.HipValley == SlopeFactor.HipValleyEnum.hipValleyEnabled && this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnElevation)
                {
                    num = (int)this.SlopeFactor.Apply((double)num, (double)Math.Abs(x2 - x1), (double)Math.Abs(y2 - y1));
                }
                else if (num1 >= 89 && num1 <= 91 || num1 >= 269 && num1 <= 271 || this.SlopeFactor.SlopeApplyType == SlopeFactor.SlopeApplyTypeEnum.applyOnPlan)
                {
                    num = (int)this.SlopeFactor.Apply((double)num, 0, 0);
                }
            }
            return num;
        }

        public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = 1)
        {
            Pen pen;
            TextRenderingHint textRenderingHint;
            SmoothingMode smoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            TextRenderingHint textRenderingHint1 = g.TextRenderingHint;
            Graphics graphic = g;
            if (printToScreen)
            {
                textRenderingHint = (imageQuality == MainForm.ImageQualityEnum.QualityHigh ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel);
            }
            else
            {
                textRenderingHint = TextRenderingHint.ClearTypeGridFit;
            }
            graphic.TextRenderingHint = textRenderingHint;
            pen = (base.DrawPen != null ? (Pen)base.DrawPen.Clone() : new Pen(Color.FromArgb(base.Opacity + 30, base.Color), (float)base.PenWidth));
            base.ClearTextArray();
            int num = 0;
            int x = this.StartPoint.X;
            int y = this.StartPoint.Y;
            int x1 = this.EndPoint.X;
            Point endPoint = this.EndPoint;
            int num1 = this.Distance2D(x, y, x1, endPoint.Y, true, ref num);
            if ((double)num * base.DrawingBoard.ZoomFactor >= (double)(this.toolTipMinThreshold + (base.DrawArea.UnitScaleIsImperial() ? 10 : 0)))
            {
                string str = (base.DisplayInPixels ? string.Concat(num1.ToString(), " pixels") : base.DrawArea.ToLengthString(num1, (double)num * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold));
                base.TextArray.Add(new DrawText(str, this.Center, this.TextAngle(this.startPoint, this.endPoint)));
            }
            this.lastDistance = (double)num;
            GraphicsPath graphicsPath = new GraphicsPath();
            Point point = new Point(this.startPoint.X, this.startPoint.Y);
            Point point1 = new Point(this.endPoint.X, this.endPoint.Y);
            point.X = point.X - offsetX;
            point.Y = point.Y - offsetY;
            point1.X = point1.X - offsetX;
            point1.Y = point1.Y - offsetY;
            graphicsPath.AddLine(point, point1);
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
            g.TextRenderingHint = textRenderingHint1;
            g.SmoothingMode = smoothingMode;
        }

        public override void DrawText(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = 1, float defaultFontSize = 12f)
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

        public static double FindDistanceToSegment(Point pt, Point p1, Point p2)
        {
            PointF pointF;
            return DrawLine.FindDistanceToSegment(pt, p1, p2, out pointF);
        }

        public static double FindDistanceToSegment(Point pt, Point p1, Point p2, out PointF closest)
        {
            float x = (float)(p2.X - p1.X);
            float y = (float)(p2.Y - p1.Y);
            if (x == 0f && y == 0f)
            {
                closest = p1;
                x = (float)(pt.X - p1.X);
                y = (float)(pt.Y - p1.Y);
                return Math.Sqrt((double)(x * x + y * y));
            }
            float single = ((float)(pt.X - p1.X) * x + (float)(pt.Y - p1.Y) * y) / (x * x + y * y);
            if (single < 0f)
            {
                closest = new Point(p1.X, p1.Y);
                x = (float)(pt.X - p1.X);
                y = (float)(pt.Y - p1.Y);
            }
            else if (single <= 1f)
            {
                closest = new PointF((float)p1.X + single * x, (float)p1.Y + single * y);
                x = (float)pt.X - closest.X;
                y = (float)pt.Y - closest.Y;
            }
            else
            {
                closest = new Point(p2.X, p2.Y);
                x = (float)(pt.X - p2.X);
                y = (float)(pt.Y - p2.Y);
            }
            return Math.Sqrt((double)(x * x + y * y));
        }

        public static int GetDistance(Point startPoint, Point endPoint)
        {
            int x = startPoint.X;
            int y = startPoint.Y;
            int num = endPoint.X;
            int y1 = endPoint.Y;
            double num1 = Math.Pow((double)(num - x), 2);
            double num2 = Math.Pow((double)(y1 - y), 2);
            return (int)Math.Sqrt(num1 + num2);
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

        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                case 2:
                    {
                        return Cursors.SizeAll;
                    }
            }
            return Cursors.Default;
        }

        public override int GetHandleSize()
        {
            return 4;
        }

        private Rectangle GetNormalizedRectangle()
        {
            int x;
            int y;
            int num;
            int y1;
            if (this.startPoint.X > this.endPoint.X)
            {
                x = this.endPoint.X;
                num = this.startPoint.X;
            }
            else
            {
                x = this.startPoint.X;
                num = this.endPoint.X;
            }
            if (this.startPoint.Y > this.endPoint.Y)
            {
                y = this.endPoint.Y;
                y1 = this.startPoint.Y;
            }
            else
            {
                y = this.startPoint.Y;
                y1 = this.endPoint.Y;
            }
            int num1 = num - x;
            int num2 = y1 - y;
            return new Rectangle(x, y, num1 + 1, num2 + 1);
        }

        public static Point GetPointAtAngle(Point startPoint, double angle, double distance)
        {
            Point point = new Point((int)Math.Ceiling((double)startPoint.X + distance * Math.Cos(Utilities.DegreeToRadian(angle))), (int)Math.Ceiling((double)startPoint.Y + distance * Math.Sin(Utilities.DegreeToRadian(angle))));
            return point;
        }

        public static PointF GetPointAtAngle(PointF startPoint, double angle, double distance)
        {
            PointF pointF = new PointF((float)Math.Ceiling((double)startPoint.X + distance * Math.Cos(Utilities.DegreeToRadian(angle))), (float)Math.Ceiling((double)startPoint.Y + distance * Math.Sin(Utilities.DegreeToRadian(angle))));
            return pointF;
        }

        public static IEnumerable<Point> GetPointsOnLine(int x0, int y0, int x1, int y1)
        {
            int num;
            bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (flag)
            {
                int num1 = x0;
                x0 = y0;
                y0 = num1;
                num1 = x1;
                x1 = y1;
                y1 = num1;
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
            int num3 = x1 - x0;
            int num4 = Math.Abs(y1 - y0);
            int num5 = num3 / 2;
            int num6 = (y0 < y1 ? 1 : -1);
            int num7 = y0;
            for (int i = x0; i <= x1; i++)
            {
                num = (flag ? num7 : i);
                yield return new Point(num, (flag ? i : num7));
                num5 -= num4;
                if (num5 < 0)
                {
                    num7 += num6;
                    num5 += num3;
                }
            }
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

        protected void Invalidate()
        {
        }

        public static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float y = (float)((l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y));
            float x = (float)((l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X));
            if (x == 0f)
            {
                return false;
            }
            float single = y / x;
            y = (float)((l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y));
            float single1 = y / x;
            if (single >= 0f && single <= 1f && single1 >= 0f && single1 <= 1f)
            {
                return true;
            }
            return false;
        }

        public static bool LineIntersectsLine(PointF l1p1, PointF l1p2, PointF l2p1, PointF l2p2)
        {
            float y = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float x = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);
            if (x == 0f)
            {
                return false;
            }
            float single = y / x;
            y = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float single1 = y / x;
            if (single >= 0f && single <= 1f && single1 >= 0f && single1 <= 1f)
            {
                return true;
            }
            return false;
        }

        public static bool LineIntersectsRectangle(Rectangle rectangle, Point p1, Point p2)
        {
            if (DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y)) || DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X + rectangle.Width, rectangle.Y), new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)) || DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), new Point(rectangle.X, rectangle.Y + rectangle.Height)) || DrawLine.LineIntersectsLine(p1, p2, new Point(rectangle.X, rectangle.Y + rectangle.Height), new Point(rectangle.X, rectangle.Y)))
            {
                return true;
            }
            if (!rectangle.Contains(p1))
            {
                return false;
            }
            return rectangle.Contains(p2);
        }

        public static bool LineIsReverse(Point startPoint, Point endPoint)
        {
            bool y = false;
            if (endPoint.X == startPoint.X && endPoint.Y != startPoint.Y)
            {
                y = endPoint.Y < startPoint.Y;
            }
            else if (endPoint.X == startPoint.X || endPoint.Y != startPoint.Y)
            {
                y = (Math.Abs(endPoint.X - startPoint.X) <= Math.Abs(endPoint.Y - startPoint.Y) ? endPoint.Y < startPoint.Y : endPoint.X < startPoint.X);
            }
            else
            {
                y = endPoint.X < startPoint.X;
            }
            return y;
        }

        public static bool LineIsReverse(PointF startPoint, PointF endPoint)
        {
            bool y = false;
            if (endPoint.X == startPoint.X && endPoint.Y != startPoint.Y)
            {
                y = endPoint.Y < startPoint.Y;
            }
            else if (endPoint.X == startPoint.X || endPoint.Y != startPoint.Y)
            {
                y = (Math.Abs(endPoint.X - startPoint.X) <= Math.Abs(endPoint.Y - startPoint.Y) ? endPoint.Y < startPoint.Y : endPoint.X < startPoint.X);
            }
            else
            {
                y = endPoint.X < startPoint.X;
            }
            return y;
        }

        public Point LocatePointOnLine(Point pt, Point p1, Point p2)
        {
            PointF pointF;
            DrawLine.FindDistanceToSegment(pt, p1, p2, out pointF);
            return new Point((int)pointF.X, (int)pointF.Y);
        }

        public override void Move(int deltaX, int deltaY)
        {
            ref Point x = ref this.startPoint;
            x.X = x.X + deltaX;
            ref Point y = ref this.startPoint;
            y.Y = y.Y + deltaY;
            ref Point pointPointer = ref this.endPoint;
            pointPointer.X = pointPointer.X + deltaX;
            ref Point y1 = ref this.endPoint;
            y1.Y = y1.Y + deltaY;
            base.Dirty = true;
            this.Invalidate();
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber != 1)
            {
                this.endPoint = point;
                ref Point x = ref this.endPoint;
                int num = x.X;
                PointF origin = base.DrawingBoard.Origin;
                x.X = num + (int)origin.X;
                ref Point y = ref this.endPoint;
                int y1 = y.Y;
                PointF pointF = base.DrawingBoard.Origin;
                y.Y = y1 + (int)pointF.Y;
            }
            else
            {
                this.startPoint = point;
                ref Point pointPointer = ref this.startPoint;
                int x1 = pointPointer.X;
                PointF origin1 = base.DrawingBoard.Origin;
                pointPointer.X = x1 + (int)origin1.X;
                ref Point pointPointer1 = ref this.startPoint;
                int num1 = pointPointer1.Y;
                PointF pointF1 = base.DrawingBoard.Origin;
                pointPointer1.Y = num1 + (int)pointF1.Y;
            }
            base.Dirty = true;
            this.Invalidate();
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

        protected override bool PointInObject(Point point, int offsetX, int offsetY)
        {
            double segment = DrawLine.FindDistanceToSegment(new Point(point.X + offsetX, point.Y + offsetY), this.startPoint, this.endPoint);
            if (segment < 0)
            {
                return false;
            }
            return segment <= (double)(base.PenWidth / 2);
        }

        public override Region Region(int offsetX, int offsetY, float zoomFactor)
        {
            Region region = new Region();
            offsetX -= (int)((float)base.DrawArea.DrawingBoard.HorizontalOffset / zoomFactor);
            offsetY -= (int)((float)base.DrawArea.DrawingBoard.VerticalOffset / zoomFactor);
            Rectangle normalizedRectangle = this.GetNormalizedRectangle();
            normalizedRectangle.X = normalizedRectangle.X - offsetX;
            normalizedRectangle.Y = normalizedRectangle.Y - offsetY;
            normalizedRectangle.X = (int)((float)normalizedRectangle.X * zoomFactor);
            normalizedRectangle.Y = (int)((float)normalizedRectangle.Y * zoomFactor);
            normalizedRectangle.Width = (int)((float)normalizedRectangle.Width * zoomFactor);
            normalizedRectangle.Height = (int)((float)normalizedRectangle.Height * zoomFactor);
            normalizedRectangle.Inflate(new Size(10 + base.PenWidth, 10 + base.PenWidth));
            region.MakeEmpty();
            region.Union(normalizedRectangle);
            if (base.TextArray.Count > 0)
            {
                DrawText item = (DrawText)base.TextArray[0];
                Point point = item.Point;
                Rectangle rectangle = item.Rectangle;
                int x = point.X - offsetX - rectangle.Width / 2;
                Point point1 = item.Point;
                Rectangle rectangle1 = item.Rectangle;
                int y = point1.Y - offsetY - rectangle1.Height / 2;
                int width = item.Rectangle.Width;
                Rectangle rectangle2 = item.Rectangle;
                Rectangle rectangle3 = new Rectangle(x, y, width, rectangle2.Height)
                {
                    X = (int)((float)rectangle3.X * zoomFactor),
                    Y = (int)((float)rectangle3.Y * zoomFactor),
                    Width = (int)((float)rectangle3.Width * zoomFactor),
                    Height = (int)((float)rectangle3.Height * zoomFactor)
                };
                rectangle3.Inflate(new Size(base.PenWidth, base.PenWidth));
                region.Union(rectangle3);
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

        public double RoundAngle(double angle, int precision)
        {
            if (angle == 0 || angle == 90 || angle == 180 || angle == 270 || angle == 360)
            {
                if (angle != 360)
                {
                    return angle;
                }
                return 0;
            }
            int[] numArray = new int[5];
            double[] numArray1 = new double[5];
            numArray[0] = 0;
            numArray[1] = 90;
            numArray[2] = 180;
            numArray[3] = 0x10e;
            numArray[4] = 0;
            numArray1[0] = Math.Abs(angle - 0);
            numArray1[1] = Math.Abs(angle - 90);
            numArray1[2] = Math.Abs(angle - 180);
            numArray1[3] = Math.Abs(angle - 270);
            numArray1[4] = Math.Abs(angle - 360);
            int num = 0;
            for (int i = 1; i < (int)numArray1.Length; i++)
            {
                if (numArray1[i] < numArray1[num])
                {
                    num = i;
                }
            }
            if (numArray1[num] > (double)precision)
            {
                return angle;
            }
            return (double)numArray[num];
        }

        public override void Scale(float scaleX, float scaleY)
        {
            this.startPoint.X = (int)((float)this.startPoint.X * scaleX);
            this.startPoint.Y = (int)((float)this.startPoint.Y * scaleY);
            this.endPoint.X = (int)((float)this.endPoint.X * scaleX);
            this.endPoint.Y = (int)((float)this.endPoint.Y * scaleY);
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

        public float Slope(PointF point1, PointF point2)
        {
            return (point2.Y - point1.Y) / (point2.X - point1.X);
        }

        public int TextAngle(Point startPoint, Point endPoint)
        {
            int num = 0;
            int num1 = (int)DrawLine.Angle((double)startPoint.X, (double)startPoint.Y, (double)endPoint.X, (double)endPoint.Y);
            if (num1 >= 0 && num1 <= 90)
            {
                num = num1;
            }
            if (num1 > 90 && num1 < 180)
            {
                num = 0x10e + (num1 - 90);
            }
            if (num1 >= 180 && num1 < 0x10e)
            {
                num = num1 - 180;
            }
            if (num1 >= 0x10e)
            {
                num = 0x10e + (num1 - 0x10e);
            }
            return Math.Abs(num);
        }

        [CompilerGenerated]
        // <GetPointsOnLine>d__0
        private sealed class u003cGetPointsOnLineu003ed__0 : IEnumerable<Point>, IEnumerable, IEnumerator<Point>, IEnumerator, IDisposable
        {
            // <>2__current
            private Point u003cu003e2__current;

            // <>1__state
            private int u003cu003e1__state;

            // <>l__initialThreadId
            private int u003cu003el__initialThreadId;

            public int x0;

            // <>3__x0
            public int u003cu003e3__x0;

            public int y0;

            // <>3__y0
            public int u003cu003e3__y0;

            public int x1;

            // <>3__x1
            public int u003cu003e3__x1;

            public int y1;

            // <>3__y1
            public int u003cu003e3__y1;

            // <steep>5__1
            public bool u003csteepu003e5__1;

            // <dx>5__2
            public int u003cdxu003e5__2;

            // <dy>5__3
            public int u003cdyu003e5__3;

            // <error>5__4
            public int u003cerroru003e5__4;

            // <ystep>5__5
            public int u003cystepu003e5__5;

            // <y>5__6
            public int u003cyu003e5__6;

            // <x>5__7
            public int u003cxu003e5__7;

            Point System.Collections.Generic.IEnumerator<System.Drawing.Point>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.u003cu003e2__current;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.u003cu003e2__current;
                }
            }

            [DebuggerHidden]
            public u003cGetPointsOnLineu003ed__0(int u003cu003e1__state)
            {
                this.u003cu003e1__state = u003cu003e1__state;
                this.u003cu003el__initialThreadId = Thread.CurrentThread.ManagedThreadId;
            }

            bool MoveNext()
            {
                switch (this.u003cu003e1__state)
                {
                    case 0:
                        {
                            this.u003cu003e1__state = -1;
                            this.u003csteepu003e5__1 = Math.Abs(this.y1 - this.y0) > Math.Abs(this.x1 - this.x0);
                            if (this.u003csteepu003e5__1)
                            {
                                int num = this.x0;
                                this.x0 = this.y0;
                                this.y0 = num;
                                num = this.x1;
                                this.x1 = this.y1;
                                this.y1 = num;
                            }
                            if (this.x0 > this.x1)
                            {
                                int num1 = this.x0;
                                this.x0 = this.x1;
                                this.x1 = num1;
                                num1 = this.y0;
                                this.y0 = this.y1;
                                this.y1 = num1;
                            }
                            this.u003cdxu003e5__2 = this.x1 - this.x0;
                            this.u003cdyu003e5__3 = Math.Abs(this.y1 - this.y0);
                            this.u003cerroru003e5__4 = this.u003cdxu003e5__2 / 2;
                            this.u003cystepu003e5__5 = (this.y0 < this.y1 ? 1 : -1);
                            this.u003cyu003e5__6 = this.y0;
                            this.u003cxu003e5__7 = this.x0;
                            break;
                        }
                    case 1:
                        {
                            this.u003cu003e1__state = -1;
                            this.u003cerroru003e5__4 -= this.u003cdyu003e5__3;
                            if (this.u003cerroru003e5__4 < 0)
                            {
                                this.u003cyu003e5__6 += this.u003cystepu003e5__5;
                                this.u003cerroru003e5__4 += this.u003cdxu003e5__2;
                            }
                            this.u003cxu003e5__7++;
                            break;
                        }
                    default:
                        {
                            return false;
                        }
                }
                if (this.u003cxu003e5__7 <= this.x1)
                {
                    this.u003cu003e2__current = new Point((this.u003csteepu003e5__1 ? this.u003cyu003e5__6 : this.u003cxu003e5__7), (this.u003csteepu003e5__1 ? this.u003cxu003e5__7 : this.u003cyu003e5__6));
                    this.u003cu003e1__state = 1;
                    return true;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Point> System.Collections.Generic.IEnumerable<System.Drawing.Point>.GetEnumerator()
            {
                DrawLine.u003cGetPointsOnLineu003ed__0 variable;
                if (Thread.CurrentThread.ManagedThreadId != this.u003cu003el__initialThreadId || this.u003cu003e1__state != -2)
                {
                    variable = new DrawLine.u003cGetPointsOnLineu003ed__0(0);
                }
                else
                {
                    this.u003cu003e1__state = 0;
                    variable = this;
                }
                variable.x0 = this.u003cu003e3__x0;
                variable.y0 = this.u003cu003e3__y0;
                variable.x1 = this.u003cu003e3__x1;
                variable.y1 = this.u003cu003e3__y1;
                return variable;
            }

            [DebuggerHidden]
            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.System.Collections.Generic.IEnumerable<System.Drawing.Point>.GetEnumerator();
            }

            [DebuggerHidden]
            void System.Collections.IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void System.IDisposable.Dispose()
            {
            }
        }
    }
}