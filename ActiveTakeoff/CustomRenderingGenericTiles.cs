using ClipperLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class CustomRenderingGenericTiles : CustomRendering
    {
        private const int MaxTilesCount = 0x1d4c;

        public CustomRenderingGenericTiles(Preset preset)
        {
            base.Preset = preset;
        }

        private int ComputeJoints(GraphicsPath gp, CustomRenderingGenericTiles.Tile tile, int offsetX, int offsetY)
        {
            int num = 0;
            DrawPolyLine drawPolyLine = tile.DrawPolyLine;
            HashSet<Segment> joints = this.GetJoints(tile);
            int num1 = 0;
            foreach (Segment joint in joints)
            {
                if (gp != null)
                {
                    Point startPoint = joint.StartPoint;
                    Point point = joint.StartPoint;
                    Point point1 = new Point(startPoint.X - offsetX, point.Y - offsetY);
                    Point endPoint = joint.EndPoint;
                    Point endPoint1 = joint.EndPoint;
                    gp.AddLine(point1, new Point(endPoint.X - offsetX, endPoint1.Y - offsetY));
                    gp.CloseFigure();
                }
                int distance = DrawLine.GetDistance(Point.Truncate(joint.StartPoint), Point.Truncate(joint.EndPoint));
                num += distance;
                num1++;
            }
            num += this.ComputeOutlineJoints(drawPolyLine);
            if (drawPolyLine.DeductionArray.Count > 0)
            {
                foreach (DrawPolyLine deductionArray in drawPolyLine.DeductionArray)
                {
                    num += this.ComputeOutlineJoints(deductionArray);
                }
            }
            return num;
        }

        private int ComputeOutlineJoints(DrawPolyLine drawPolyLine)
        {
            int distance = 0;
            for (int i = 0; i < drawPolyLine.PointArray.Count - 1; i++)
            {
                distance += DrawLine.GetDistance((Point)drawPolyLine.PointArray[i], (Point)drawPolyLine.PointArray[i + 1]);
            }
            distance += DrawLine.GetDistance((Point)drawPolyLine.PointArray[drawPolyLine.PointArray.Count - 1], (Point)drawPolyLine.PointArray[0]);
            return distance;
        }

        public override void ComputeResults(Graphics g, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
            renderingProperties.ResultsArray.Clear();
            drawPolyLine.CustomRenderingTooltip = string.Empty;
            CustomRenderingGenericTiles.Tile tile = this.GetTile(drawPolyLine, renderingProperties, unitScale);
            if (tile != null)
            {
                tile.JointLength = this.ComputeJoints(null, tile, 0, 0);
                renderingProperties.ResultsArray.Add("jointLength1", new CustomRenderingResult("jointLength1", unitScale.ToLength(tile.JointLength), ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength));
            }
        }

        public override void Draw(Graphics g, Region region, int offsetX, int offsetY, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
            CustomRenderingGenericTiles.Tile tile = this.GetTile(drawPolyLine, renderingProperties, unitScale);
            if (tile != null && tile.EstimatedTilesCount <= 0x1d4c)
            {
                this.DrawTiles(g, tile, offsetX, offsetY);
            }
        }

        private void DrawTiles(Graphics g, CustomRenderingGenericTiles.Tile tile, int offsetX, int offsetY)
        {
            using (GraphicsPath graphicsPath = new GraphicsPath())
            {
                if (this.ComputeJoints(graphicsPath, tile, offsetX, offsetY) > 0)
                {
                    Pen pen = new Pen(Color.FromArgb(tile.DrawPolyLine.Opacity + 30, Utilities.IdealTextColor(tile.DrawPolyLine.FillColor, 180)), tile.JointPenWidth)
                    {
                        StartCap = LineCap.Square,
                        Alignment = PenAlignment.Center
                    };
                    g.DrawPath(pen, graphicsPath);
                    pen.Dispose();
                }
            }
        }

        private int EstimatedTilesCount(double netArea, double wasteFactor, double tileWidth, double tileLength, double jointThickness)
        {
            return (int)Math.Round(netArea * (1 + wasteFactor / 100) / ((tileWidth + jointThickness) * (tileLength + jointThickness)));
        }

        private HashSet<Segment> GetBreakSegments(HashSet<Segment> segments, DrawPolyLine drawPolyLine)
        {
            HashSet<Segment> segments1 = new HashSet<Segment>();
            List<List<IntPoint>> lists = new List<List<IntPoint>>(1 + drawPolyLine.DeductionArray.Count);
            List<IntPoint> intPoints = new List<IntPoint>();
            foreach (Point pointArray in drawPolyLine.PointArray)
            {
                intPoints.Add(new IntPoint((long)pointArray.X, (long)pointArray.Y));
            }
            lists.Add(intPoints);
            if (drawPolyLine.DeductionArray.Count > 0)
            {
                foreach (DrawPolyLine deductionArray in drawPolyLine.DeductionArray)
                {
                    List<IntPoint> intPoints1 = new List<IntPoint>();
                    foreach (Point point in deductionArray.PointArray)
                    {
                        intPoints1.Add(new IntPoint((long)point.X, (long)point.Y));
                    }
                    lists.Add(intPoints1);
                }
            }
            List<List<IntPoint>> lists1 = new List<List<IntPoint>>();
            foreach (Segment segment in segments)
            {
                List<IntPoint> intPoints2 = new List<IntPoint>(2);
                long x = (long)segment.StartPoint.X;
                Point startPoint = segment.StartPoint;
                intPoints2.Add(new IntPoint(x, (long)startPoint.Y));
                long num = (long)segment.EndPoint.X;
                Point endPoint = segment.EndPoint;
                intPoints2.Add(new IntPoint(num, (long)endPoint.Y));
                lists1.Add(intPoints2);
            }
            PolyTree polyTree = new PolyTree();
            Clipper clipper = new Clipper(0);
            clipper.AddPaths(lists1, PolyType.ptSubject, false);
            clipper.AddPaths(lists, PolyType.ptClip, true);
            clipper.Execute(ClipType.ctIntersection, polyTree);
            HashSet<string> strs = new HashSet<string>();
            foreach (PolyNode child in polyTree.Childs)
            {
                Segment segment1 = new Segment();
                try
                {
                    segment1 = new Segment(new Point((int)child.Contour[0].X, (int)child.Contour[0].Y), new Point((int)child.Contour[1].X, (int)child.Contour[1].Y));
                }
                catch
                {
                }
                if (!segment1.IsValid())
                {
                    continue;
                }
                if (DrawLine.LineIsReverse(segment1.StartPoint, segment1.EndPoint))
                {
                    Point point1 = new Point(segment1.StartPoint.X, segment1.StartPoint.Y);
                    segment1.StartPoint = new Point(segment1.EndPoint.X, segment1.EndPoint.Y);
                    segment1.EndPoint = new Point(point1.X, point1.Y);
                }
                if (drawPolyLine.MatchSegment(segment1, drawPolyLine.PenWidth))
                {
                    continue;
                }
                object[] objArray = new object[] { segment1.StartPoint.X, ",", segment1.StartPoint.Y, ",", segment1.EndPoint.X, ",", segment1.EndPoint.Y };
                string str = string.Concat(objArray);
                if (strs.Contains(str))
                {
                    continue;
                }
                segments1.Add(segment1);
                strs.Add(str);
            }
            return segments1;
        }

        private HashSet<Segment> GetJoints(CustomRenderingGenericTiles.Tile tile)
        {
            HashSet<Segment> segments = this.GetSegments(tile);
            if (segments.Count <= 0)
            {
                return segments;
            }
            return this.GetBreakSegments(segments, tile.DrawPolyLine);
        }

        private HashSet<Segment> GetSegments(CustomRenderingGenericTiles.Tile tile)
        {
            HashSet<Segment> segments = new HashSet<Segment>();
            DrawPolyLine drawPolyLine = tile.DrawPolyLine;
            Rectangle boundingRectangle = drawPolyLine.BoundingRectangle;
            int num = Math.Max(tile.WidthInPixels, tile.HeightInPixels);
            boundingRectangle.Inflate(num * 2, num * 2);
            int num1 = 0x7f35;
            List<List<IntPoint>> lists = new List<List<IntPoint>>();
            List<List<IntPoint>> lists1 = new List<List<IntPoint>>();
            for (int i = 0; i < 2; i++)
            {
                int x = ((Point)drawPolyLine.PointArray[0]).X;
                Point item = (Point)drawPolyLine.PointArray[0];
                Point point = new Point(x, item.Y);
                Segment segmentAtAngle = base.GetSegmentAtAngle(point, point, tile.Angle + 90 * i, (double)num1, CustomRendering.SegmentDirection.SegmentDirectionBoth, 0);
                int x1 = segmentAtAngle.StartPoint.X + tile.OffsetXInPixels;
                Point startPoint = segmentAtAngle.StartPoint;
                segmentAtAngle.StartPoint = new Point(x1, startPoint.Y + tile.OffsetYInPixels);
                int x2 = segmentAtAngle.EndPoint.X + tile.OffsetXInPixels;
                Point endPoint = segmentAtAngle.EndPoint;
                segmentAtAngle.EndPoint = new Point(x2, endPoint.Y + tile.OffsetYInPixels);
                float single = (float)segmentAtAngle.StartPoint.X;
                float y = (float)segmentAtAngle.StartPoint.Y;
                float single1 = (float)segmentAtAngle.EndPoint.X;
                float y1 = (float)segmentAtAngle.EndPoint.Y;
                float single2 = (float)Math.Sqrt((double)((single - single1) * (single - single1) + (y - y1) * (y - y1)));
                for (int j = 0; j < 3; j++)
                {
                    int thicknessInPixels = 0;
                    bool flag = false;
                    do
                    {
                        switch (j)
                        {
                            case 1:
                                {
                                    thicknessInPixels = thicknessInPixels - ((i == 0 ? tile.HeightInPixels : tile.WidthInPixels) + tile.ThicknessInPixels);
                                    break;
                                }
                            case 2:
                                {
                                    thicknessInPixels = thicknessInPixels + (i == 0 ? tile.HeightInPixels : tile.WidthInPixels) + tile.ThicknessInPixels;
                                    break;
                                }
                        }
                        float single3 = single + (float)thicknessInPixels * (y1 - y) / single2;
                        float single4 = single1 + (float)thicknessInPixels * (y1 - y) / single2;
                        float single5 = y + (float)thicknessInPixels * (single - single1) / single2;
                        float single6 = y1 + (float)thicknessInPixels * (single - single1) / single2;
                        Segment segment = base.GetSegment(new Point((int)single3, (int)single5), new Point((int)single4, (int)single6), boundingRectangle);
                        if (!segment.IsValid())
                        {
                            flag = true;
                        }
                        else
                        {
                            segment.Tag = (i == 0 ? CustomRenderingGenericTiles.SegmentsAxis.SegmentsAxisX : CustomRenderingGenericTiles.SegmentsAxis.SegmentsAxisY);
                            segments.Add(segment);
                        }
                        flag = (flag ? true : j == 0);
                    }
                    while (!flag);
                }
            }
            return segments;
        }

        private CustomRenderingGenericTiles.Tile GetTile(DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
        {
            CustomRenderingGenericTiles.Tile tile = new CustomRenderingGenericTiles.Tile()
            {
                Width = Utilities.ConvertToDouble(base.Preset.FieldValue("tileWidth", 0), -1),
                Height = Utilities.ConvertToDouble(base.Preset.FieldValue("tileLength", 0), -1),
                Thickness = 0
            };
            if (tile.Width == 0 || tile.Height == 0)
            {
                return null;
            }
            tile.WidthInPixels = (base.Preset.ScaleSystemType == UnitScale.UnitSystem.imperial ? (int)unitScale.ToPixelsFromFeet(tile.Width) : (int)unitScale.ToPixelsFromFeet(UnitScale.FromMetersToFeet(tile.Width)));
            tile.WidthInPixels = (tile.WidthInPixels == 0 ? 1 : tile.WidthInPixels);
            tile.HeightInPixels = (base.Preset.ScaleSystemType == UnitScale.UnitSystem.imperial ? (int)unitScale.ToPixelsFromFeet(tile.Height) : (int)unitScale.ToPixelsFromFeet(UnitScale.FromMetersToFeet(tile.Height)));
            tile.HeightInPixels = (tile.HeightInPixels == 0 ? 1 : tile.HeightInPixels);
            tile.ThicknessInPixels = 1;
            tile.OffsetXInPixels = (int)((float)tile.WidthInPixels * ((float)renderingProperties.OffsetX / 100f));
            tile.OffsetYInPixels = (int)((float)tile.HeightInPixels * ((float)renderingProperties.OffsetY / 100f));
            tile.Angle = renderingProperties.Angle;
            tile.JointPenWidth = 1.6f;
            tile.DrawPolyLine = drawPolyLine;
            tile.EstimatedTilesCount = this.EstimatedTilesCount(unitScale.ToArea((int)drawPolyLine.Stats.AreaMinusDeduction), 0, tile.Width, tile.Height, 0);
            return tile;
        }

        private static Point IntPointToPoint(IntPoint intPoint)
        {
            return new Point((int)intPoint.X, (int)intPoint.Y);
        }

        private enum SegmentsAxis
        {
            SegmentsAxisX,
            SegmentsAxisY
        }

        private class Tile
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

            public DrawPolyLine DrawPolyLine
            {
                get;
                set;
            }

            public int EstimatedTilesCount
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

            public int JointLength
            {
                get;
                set;
            }

            public float JointPenWidth
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

            public Tile()
            {
            }
        }
    }
}