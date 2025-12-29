using QuoterPlanControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class CustomRenderingGenericRows : CustomRendering
    {
        public CustomRenderingGenericRows(Preset preset)
        {
            base.Preset = preset;
        }

        public override void ComputeResults(Graphics g, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
        }

        private int CountRows(Graphics g, Region region, CustomRenderingGenericRows.Row row, GraphicsPath outlinePath, GraphicsPath gp)
        {
            int num = 0;
            RectangleF bounds = region.GetBounds(g);
            int angle = row.Angle;
            float left = bounds.Left;
            float top = bounds.Top + bounds.Height / 2f;
            float right = bounds.Right;
            float y = bounds.Top + bounds.Height / 2f;
            PointF[] pointF = new PointF[] { new PointF(left, top + (float)row.OffsetYInPixels - (float)(row.HeightInPixels / 2)), new PointF(right, y + (float)row.OffsetYInPixels - (float)(row.HeightInPixels / 2)) };
            if (row.Angle != 0)
            {
                Matrix matrix = new Matrix();
                matrix.RotateAt((float)row.Angle, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
                matrix.TransformPoints(pointF);
            }
            left = pointF[0].X;
            top = pointF[0].Y;
            right = pointF[1].X;
            y = pointF[1].Y;
            double num1 = Math.Sqrt((double)((left - right) * (left - right) + (top - y) * (top - y)));
            Console.WriteLine("-----------------------------");
            Segment segment = new Segment();
            DrawPolyLine drawPolyline = row.DrawPolyline;
            int num2 = 1;
            for (int i = 0; i < 3; i++)
            {
                int heightInPixels = 0;
                bool flag = false;
                Segment segment1 = new Segment();
                do
                {
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            heightInPixels -= row.HeightInPixels;
                        }
                        else if (i == 2)
                        {
                            heightInPixels += row.HeightInPixels;
                        }
                    }
                    int num3 = (int)(left + (float)heightInPixels * (y - top) / (float)num1);
                    int num4 = (int)(right + (float)heightInPixels * (y - top) / (float)num1);
                    int num5 = (int)(top + (float)heightInPixels * (left - right) / (float)num1);
                    int num6 = (int)(y + (float)heightInPixels * (left - right) / (float)num1);
                    GraphicsPath graphicsPath = new GraphicsPath();
                    graphicsPath.AddRectangle(bounds);
                    graphicsPath.CloseAllFigures();
                    Segment segment2 = base.GetSegment(new Point(num3, num5), new Point(num4, num6), new Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height));
                    graphicsPath.Dispose();
                    if (!segment2.IsValid())
                    {
                        flag = true;
                    }
                    else
                    {
                        if (segment1.IsValid() || segment.IsValid())
                        {
                            using (GraphicsPath graphicsPath1 = new GraphicsPath())
                            {
                                Point[] startPoint = new Point[5];
                                if (!segment1.IsValid())
                                {
                                    startPoint[0] = segment.StartPoint;
                                    startPoint[1] = segment.EndPoint;
                                }
                                else
                                {
                                    startPoint[0] = segment1.StartPoint;
                                    startPoint[1] = segment1.EndPoint;
                                }
                                startPoint[2] = segment2.EndPoint;
                                startPoint[3] = segment2.StartPoint;
                                if (!segment1.IsValid())
                                {
                                    startPoint[4] = segment.StartPoint;
                                }
                                else
                                {
                                    startPoint[4] = segment1.StartPoint;
                                }
                                graphicsPath1.AddPolygon(startPoint);
                                g.FillPath(new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.FromArgb(drawPolyline.Opacity, drawPolyline.FillColor)), graphicsPath1);
                                g.DrawPath(new Pen(Color.FromArgb(drawPolyline.Opacity + 30, Color.Black), (float)num2), graphicsPath1);
                            }
                        }
                        if (i != 0)
                        {
                            segment1 = new Segment(segment2.StartPoint, segment2.EndPoint);
                        }
                        else
                        {
                            segment = new Segment(segment2.StartPoint, segment2.EndPoint);
                        }
                        gp.AddLine(segment2.StartPoint, segment2.EndPoint);
                        gp.CloseFigure();
                        int x = (int)drawPolyline.DrawArea.DrawingBoard.Origin.X;
                        int y1 = (int)drawPolyline.DrawArea.DrawingBoard.Origin.Y;
                        int distance = DrawLine.GetDistance(segment2.StartPoint, segment2.EndPoint);
                        Point point = segment2.StartPoint;
                        Point startPoint1 = segment2.StartPoint;
                        Point endPoint = segment2.EndPoint;
                        Point endPoint1 = segment2.EndPoint;
                        DrawLine drawLine = new DrawLine(point.X + x, startPoint1.Y + y1, endPoint.X + x, endPoint1.Y + y1, new PointF(0f, 0f), "", 0, "");
                        string str = (drawPolyline.DisplayInPixels ? string.Concat(distance.ToString(), " pixels") : drawPolyline.DrawArea.ToLengthString(distance, false));
                        DrawText drawText = new DrawText(str, drawLine.Center, drawPolyline.TextAngle(segment2.StartPoint, segment2.EndPoint), new Segment(drawLine.StartPoint, drawLine.EndPoint));
                        drawPolyline.TextArray.Add(drawText);
                        num++;
                    }
                    flag = (flag ? true : i == 0);
                }
                while (!flag);
            }
            return num;
        }

        private double DistanceSquared(PointF p1, PointF p2)
        {
            return (double)((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public override void Draw(Graphics g, Region region, int offsetX, int offsetY, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
            this.GetRow(drawPolyLine, renderingProperties, unitScale);
        }

        private void DrawManually(Graphics g, Region region, GraphicsPath outlinePath, CustomRenderingGenericRows.Row row, DrawPolyLine drawPolyLine)
        {
            using (GraphicsPath graphicsPath = new GraphicsPath())
            {
                if (this.CountRows(g, region, row, outlinePath, graphicsPath) > 0)
                {
                    int num = 4;
                    g.DrawPath(new Pen(Color.FromArgb(drawPolyLine.Opacity + 30, Color.Black), (float)num), graphicsPath);
                }
            }
        }

        private int EstimatedRowsCount(double netArea, double wasteFactor, double rowWidth, double rowLength, double jointThickness)
        {
            return 0;
        }

        public override string GetBasicInfo(object data)
        {
            return string.Empty;
        }

        private CustomRenderingGenericRows.Row GetRow(DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
            CustomRenderingGenericRows.Row row = new CustomRenderingGenericRows.Row()
            {
                Height = Utilities.ConvertToDouble(base.Preset.FieldValue("rowWidth", 0), -1)
            };
            if (row.Height == 0)
            {
                return null;
            }
            row.WidthInPixels = 0x3e8;
            row.WidthInPixels = (row.WidthInPixels == 0 ? 1 : row.WidthInPixels);
            row.HeightInPixels = (int)unitScale.ToPixelsFromFeet(row.Height);
            row.HeightInPixels = (row.HeightInPixels == 0 ? 1 : row.HeightInPixels);
            row.ThicknessInPixels = (int)unitScale.ToPixelsFromFeet(row.Thickness);
            row.ThicknessInPixels = (row.ThicknessInPixels == 0 ? 1 : row.ThicknessInPixels);
            row.OffsetXInPixels = (int)((float)row.WidthInPixels * ((float)renderingProperties.OffsetX / 100f));
            row.OffsetYInPixels = (int)((float)row.HeightInPixels * ((float)renderingProperties.OffsetY / 100f));
            row.Angle = renderingProperties.Angle;
            row.DrawPolyline = drawPolyLine;
            return row;
        }

        private List<CustomRendering.PointsRectangle> GetRows(Graphics g, Region region, CustomRenderingGenericRows.Row row)
        {
            List<CustomRendering.PointsRectangle> pointsRectangles = new List<CustomRendering.PointsRectangle>();
            int num = (row.Angle == 0 ? 1 : 2);
            RectangleF bounds = region.GetBounds(g);
            RectangleF rectangleF = new RectangleF(bounds.X - (float)((row.WidthInPixels + row.ColumnOffsetInPixels) * num), bounds.Y - (float)(row.HeightInPixels * num), bounds.Width + (float)((row.WidthInPixels + row.ColumnOffsetInPixels) * num * 2), bounds.Height + (float)(row.HeightInPixels * num * 2));
            if (row.Angle > 0)
            {
                CustomRendering.PointsRectangle pointsRectangle = new CustomRendering.PointsRectangle(rectangleF);
                pointsRectangle.RotateAt(row.Angle, rectangleF);
                using (GraphicsPath graphicsPath = new GraphicsPath())
                {
                    graphicsPath.AddPolygon(pointsRectangle.Points);
                    Region region1 = region.Clone();
                    region1.Union(graphicsPath);
                    rectangleF = region1.GetBounds(g);
                    region1.Dispose();
                }
            }
            float top = rectangleF.Top;
            int thicknessInPixels = row.ThicknessInPixels;
            float bottom = rectangleF.Bottom;
            float left = rectangleF.Left;
            int thicknessInPixels1 = row.ThicknessInPixels;
            float right = rectangleF.Right;
            pointsRectangles.Add(new CustomRendering.PointsRectangle((int)bounds.Left, (int)bounds.Right, (int)bounds.Top + (int)(bounds.Height / 2f), (int)bounds.Top + (int)(bounds.Height / 2f) + 2));
            if (row.Angle > 0)
            {
                foreach (CustomRendering.PointsRectangle pointsRectangle1 in pointsRectangles)
                {
                    pointsRectangle1.RotateAt(row.Angle, bounds);
                    pointsRectangle1.Translate((float)row.OffsetXInPixels, (float)row.OffsetYInPixels);
                }
            }
            return pointsRectangles;
        }

        private PointF NearestToTopLeft(GraphicsPath gPath)
        {
            PointF empty = Point.Empty;
            double num = double.MaxValue;
            PointF[] pathPoints = gPath.PathPoints;
            for (int i = 0; i < (int)pathPoints.Length; i++)
            {
                PointF pointF = pathPoints[i];
                RectangleF bounds = gPath.GetBounds();
                double num1 = this.DistanceSquared(pointF, bounds.Location);
                if (num1 < num)
                {
                    empty = pointF;
                    num = num1;
                }
            }
            return empty;
        }

        private class Row
        {
            public int Angle
            {
                get;
                set;
            }

            public double ColumnOffset
            {
                get;
                set;
            }

            public int ColumnOffsetInPixels
            {
                get;
                set;
            }

            public DrawPolyLine DrawPolyline
            {
                get;
                set;
            }

            public double Height
            {
                get;
                set;
            }

            public int HeightInPixels
            {
                get;
                set;
            }

            public double OffsetX
            {
                get;
                set;
            }

            public int OffsetXInPixels
            {
                get;
                set;
            }

            public double OffsetY
            {
                get;
                set;
            }

            public int OffsetYInPixels
            {
                get;
                set;
            }

            public double Thickness
            {
                get;
                set;
            }

            public int ThicknessInPixels
            {
                get;
                set;
            }

            public double Width
            {
                get;
                set;
            }

            public int WidthInPixels
            {
                get;
                set;
            }

            public Row()
            {
            }
        }
    }
}