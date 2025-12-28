using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using ClipperLib;

namespace QuoterPlan
{
	public class CustomRenderingGenericTiles : CustomRendering
	{
		public CustomRenderingGenericTiles(Preset preset)
		{
			base.Preset = preset;
		}

		private static Point IntPointToPoint(IntPoint intPoint)
		{
			return new Point((int)intPoint.X, (int)intPoint.Y);
		}

		private int EstimatedTilesCount(double netArea, double wasteFactor, double tileWidth, double tileLength, double jointThickness)
		{
			return (int)Math.Round(netArea * (1.0 + wasteFactor / 100.0) / ((tileWidth + jointThickness) * (tileLength + jointThickness)));
		}

		private CustomRenderingGenericTiles.Tile GetTile(DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
		{
			CustomRenderingGenericTiles.Tile tile = new CustomRenderingGenericTiles.Tile();
			tile.Width = Utilities.ConvertToDouble(base.Preset.FieldValue("tileWidth", 0), -1);
			tile.Height = Utilities.ConvertToDouble(base.Preset.FieldValue("tileLength", 0), -1);
			tile.Thickness = 0.0;
			if (tile.Width == 0.0 || tile.Height == 0.0)
			{
				return null;
			}
			tile.WidthInPixels = ((base.Preset.ScaleSystemType == UnitScale.UnitSystem.imperial) ? ((int)unitScale.ToPixelsFromFeet(tile.Width)) : ((int)unitScale.ToPixelsFromFeet(UnitScale.FromMetersToFeet(tile.Width))));
			tile.WidthInPixels = ((tile.WidthInPixels == 0) ? 1 : tile.WidthInPixels);
			tile.HeightInPixels = ((base.Preset.ScaleSystemType == UnitScale.UnitSystem.imperial) ? ((int)unitScale.ToPixelsFromFeet(tile.Height)) : ((int)unitScale.ToPixelsFromFeet(UnitScale.FromMetersToFeet(tile.Height))));
			tile.HeightInPixels = ((tile.HeightInPixels == 0) ? 1 : tile.HeightInPixels);
			tile.ThicknessInPixels = 1;
			tile.OffsetXInPixels = (int)((float)tile.WidthInPixels * ((float)renderingProperties.OffsetX / 100f));
			tile.OffsetYInPixels = (int)((float)tile.HeightInPixels * ((float)renderingProperties.OffsetY / 100f));
			tile.Angle = renderingProperties.Angle;
			tile.JointPenWidth = 1.6f;
			tile.DrawPolyLine = drawPolyLine;
			tile.EstimatedTilesCount = this.EstimatedTilesCount(unitScale.ToArea((int)drawPolyLine.Stats.AreaMinusDeduction), 0.0, tile.Width, tile.Height, 0.0);
			return tile;
		}

		private HashSet<Segment> GetSegments(CustomRenderingGenericTiles.Tile tile)
		{
			HashSet<Segment> hashSet = new HashSet<Segment>();
			DrawPolyLine drawPolyLine = tile.DrawPolyLine;
			Rectangle boundingRectangle = drawPolyLine.BoundingRectangle;
			int num = Math.Max(tile.WidthInPixels, tile.HeightInPixels);
			boundingRectangle.Inflate(num * 2, num * 2);
			int num2 = 32565;
			new List<List<IntPoint>>();
			new List<List<IntPoint>>();
			for (int i = 0; i < 2; i++)
			{
				Point point = new Point(((Point)drawPolyLine.PointArray[0]).X, ((Point)drawPolyLine.PointArray[0]).Y);
				Segment segmentAtAngle = base.GetSegmentAtAngle(point, point, tile.Angle + 90 * i, (double)num2, CustomRendering.SegmentDirection.SegmentDirectionBoth, 0);
				segmentAtAngle.StartPoint = new Point(segmentAtAngle.StartPoint.X + tile.OffsetXInPixels, segmentAtAngle.StartPoint.Y + tile.OffsetYInPixels);
				segmentAtAngle.EndPoint = new Point(segmentAtAngle.EndPoint.X + tile.OffsetXInPixels, segmentAtAngle.EndPoint.Y + tile.OffsetYInPixels);
				float num3 = (float)segmentAtAngle.StartPoint.X;
				float num4 = (float)segmentAtAngle.StartPoint.Y;
				float num5 = (float)segmentAtAngle.EndPoint.X;
				float num6 = (float)segmentAtAngle.EndPoint.Y;
				float num7 = (float)Math.Sqrt((double)((num3 - num5) * (num3 - num5) + (num4 - num6) * (num4 - num6)));
				for (int j = 0; j < 3; j++)
				{
					int num8 = 0;
					bool flag = false;
					do
					{
						switch (j)
						{
						case 1:
							num8 -= ((i == 0) ? tile.HeightInPixels : tile.WidthInPixels) + tile.ThicknessInPixels;
							break;
						case 2:
							num8 += ((i == 0) ? tile.HeightInPixels : tile.WidthInPixels) + tile.ThicknessInPixels;
							break;
						}
						float num9 = num3 + (float)num8 * (num6 - num4) / num7;
						float num10 = num5 + (float)num8 * (num6 - num4) / num7;
						float num11 = num4 + (float)num8 * (num3 - num5) / num7;
						float num12 = num6 + (float)num8 * (num3 - num5) / num7;
						Segment segment = base.GetSegment(new Point((int)num9, (int)num11), new Point((int)num10, (int)num12), boundingRectangle);
						if (segment.IsValid())
						{
							segment.Tag = ((i == 0) ? CustomRenderingGenericTiles.SegmentsAxis.SegmentsAxisX : CustomRenderingGenericTiles.SegmentsAxis.SegmentsAxisY);
							hashSet.Add(segment);
						}
						else
						{
							flag = true;
						}
						flag = (flag || j == 0);
					}
					while (!flag);
				}
			}
			return hashSet;
		}

		private HashSet<Segment> GetBreakSegments(HashSet<Segment> segments, DrawPolyLine drawPolyLine)
		{
			HashSet<Segment> hashSet = new HashSet<Segment>();
			List<List<IntPoint>> list = new List<List<IntPoint>>(1 + drawPolyLine.DeductionArray.Count);
			List<IntPoint> list2 = new List<IntPoint>();
			foreach (object obj in drawPolyLine.PointArray)
			{
				Point point = (Point)obj;
				list2.Add(new IntPoint((long)point.X, (long)point.Y));
			}
			list.Add(list2);
			if (drawPolyLine.DeductionArray.Count > 0)
			{
				foreach (object obj2 in drawPolyLine.DeductionArray)
				{
					DrawPolyLine drawPolyLine2 = (DrawPolyLine)obj2;
					List<IntPoint> list3 = new List<IntPoint>();
					foreach (object obj3 in drawPolyLine2.PointArray)
					{
						Point point2 = (Point)obj3;
						list3.Add(new IntPoint((long)point2.X, (long)point2.Y));
					}
					list.Add(list3);
				}
			}
			List<List<IntPoint>> list4 = new List<List<IntPoint>>();
			foreach (Segment segment in segments)
			{
				list4.Add(new List<IntPoint>(2)
				{
					new IntPoint((long)segment.StartPoint.X, (long)segment.StartPoint.Y),
					new IntPoint((long)segment.EndPoint.X, (long)segment.EndPoint.Y)
				});
			}
			PolyTree polyTree = new PolyTree();
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(list4, PolyType.ptSubject, false);
			clipper.AddPaths(list, PolyType.ptClip, true);
			clipper.Execute(ClipType.ctIntersection, polyTree);
			HashSet<string> hashSet2 = new HashSet<string>();
			foreach (PolyNode polyNode in polyTree.Childs)
			{
				Segment segment2 = new Segment();
				try
				{
					segment2 = new Segment(new Point((int)polyNode.Contour[0].X, (int)polyNode.Contour[0].Y), new Point((int)polyNode.Contour[1].X, (int)polyNode.Contour[1].Y));
				}
				catch
				{
				}
				if (segment2.IsValid())
				{
					if (DrawLine.LineIsReverse(segment2.StartPoint, segment2.EndPoint))
					{
						Point point3 = new Point(segment2.StartPoint.X, segment2.StartPoint.Y);
						segment2.StartPoint = new Point(segment2.EndPoint.X, segment2.EndPoint.Y);
						segment2.EndPoint = new Point(point3.X, point3.Y);
					}
					if (!drawPolyLine.MatchSegment(segment2, drawPolyLine.PenWidth))
					{
						string item = string.Concat(new object[]
						{
							segment2.StartPoint.X,
							",",
							segment2.StartPoint.Y,
							",",
							segment2.EndPoint.X,
							",",
							segment2.EndPoint.Y
						});
						if (!hashSet2.Contains(item))
						{
							hashSet.Add(segment2);
							hashSet2.Add(item);
						}
					}
				}
			}
			return hashSet;
		}

		private HashSet<Segment> GetJoints(CustomRenderingGenericTiles.Tile tile)
		{
			HashSet<Segment> segments = this.GetSegments(tile);
			if (segments.Count > 0)
			{
				return this.GetBreakSegments(segments, tile.DrawPolyLine);
			}
			return segments;
		}

		private int ComputeOutlineJoints(DrawPolyLine drawPolyLine)
		{
			int num = 0;
			for (int i = 0; i < drawPolyLine.PointArray.Count - 1; i++)
			{
				num += DrawLine.GetDistance((Point)drawPolyLine.PointArray[i], (Point)drawPolyLine.PointArray[i + 1]);
			}
			return num + DrawLine.GetDistance((Point)drawPolyLine.PointArray[drawPolyLine.PointArray.Count - 1], (Point)drawPolyLine.PointArray[0]);
		}

		private int ComputeJoints(GraphicsPath gp, CustomRenderingGenericTiles.Tile tile, int offsetX, int offsetY)
		{
			int num = 0;
			DrawPolyLine drawPolyLine = tile.DrawPolyLine;
			HashSet<Segment> joints = this.GetJoints(tile);
			int num2 = 0;
			foreach (Segment segment in joints)
			{
				if (gp != null)
				{
					gp.AddLine(new Point(segment.StartPoint.X - offsetX, segment.StartPoint.Y - offsetY), new Point(segment.EndPoint.X - offsetX, segment.EndPoint.Y - offsetY));
					gp.CloseFigure();
				}
				int distance = DrawLine.GetDistance(Point.Truncate(segment.StartPoint), Point.Truncate(segment.EndPoint));
				num += distance;
				num2++;
			}
			num += this.ComputeOutlineJoints(drawPolyLine);
			if (drawPolyLine.DeductionArray.Count > 0)
			{
				foreach (object obj in drawPolyLine.DeductionArray)
				{
					DrawPolyLine drawPolyLine2 = (DrawPolyLine)obj;
					num += this.ComputeOutlineJoints(drawPolyLine2);
				}
			}
			return num;
		}

		private void DrawTiles(Graphics g, CustomRenderingGenericTiles.Tile tile, int offsetX, int offsetY)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				if (this.ComputeJoints(graphicsPath, tile, offsetX, offsetY) > 0)
				{
					Pen pen = new Pen(Color.FromArgb(tile.DrawPolyLine.Opacity + 30, Utilities.IdealTextColor(tile.DrawPolyLine.FillColor, 180)), tile.JointPenWidth);
					pen.StartCap = LineCap.Square;
					pen.Alignment = PenAlignment.Center;
					g.DrawPath(pen, graphicsPath);
					pen.Dispose();
				}
			}
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
			if (tile != null && tile.EstimatedTilesCount <= 7500)
			{
				this.DrawTiles(g, tile, offsetX, offsetY);
			}
		}

		private const int MaxTilesCount = 7500;

		private enum SegmentsAxis
		{
			SegmentsAxisX,
			SegmentsAxisY
		}

		private class Tile
		{
			public double Width
			{
				[CompilerGenerated]
				get
				{
					return this.<Width>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Width>k__BackingField = value;
				}
			}

			public double Height
			{
				[CompilerGenerated]
				get
				{
					return this.<Height>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Height>k__BackingField = value;
				}
			}

			public double Thickness
			{
				[CompilerGenerated]
				get
				{
					return this.<Thickness>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Thickness>k__BackingField = value;
				}
			}

			public double OffsetX
			{
				[CompilerGenerated]
				get
				{
					return this.<OffsetX>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<OffsetX>k__BackingField = value;
				}
			}

			public double OffsetY
			{
				[CompilerGenerated]
				get
				{
					return this.<OffsetY>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<OffsetY>k__BackingField = value;
				}
			}

			public double ColumnOffset
			{
				[CompilerGenerated]
				get
				{
					return this.<ColumnOffset>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ColumnOffset>k__BackingField = value;
				}
			}

			public int WidthInPixels
			{
				[CompilerGenerated]
				get
				{
					return this.<WidthInPixels>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<WidthInPixels>k__BackingField = value;
				}
			}

			public int HeightInPixels
			{
				[CompilerGenerated]
				get
				{
					return this.<HeightInPixels>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<HeightInPixels>k__BackingField = value;
				}
			}

			public int ThicknessInPixels
			{
				[CompilerGenerated]
				get
				{
					return this.<ThicknessInPixels>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ThicknessInPixels>k__BackingField = value;
				}
			}

			public int OffsetXInPixels
			{
				[CompilerGenerated]
				get
				{
					return this.<OffsetXInPixels>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<OffsetXInPixels>k__BackingField = value;
				}
			}

			public int OffsetYInPixels
			{
				[CompilerGenerated]
				get
				{
					return this.<OffsetYInPixels>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<OffsetYInPixels>k__BackingField = value;
				}
			}

			public int ColumnOffsetInPixels
			{
				[CompilerGenerated]
				get
				{
					return this.<ColumnOffsetInPixels>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ColumnOffsetInPixels>k__BackingField = value;
				}
			}

			public int Angle
			{
				[CompilerGenerated]
				get
				{
					return this.<Angle>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Angle>k__BackingField = value;
				}
			}

			public int EstimatedTilesCount
			{
				[CompilerGenerated]
				get
				{
					return this.<EstimatedTilesCount>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<EstimatedTilesCount>k__BackingField = value;
				}
			}

			public int JointLength
			{
				[CompilerGenerated]
				get
				{
					return this.<JointLength>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<JointLength>k__BackingField = value;
				}
			}

			public float JointPenWidth
			{
				[CompilerGenerated]
				get
				{
					return this.<JointPenWidth>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<JointPenWidth>k__BackingField = value;
				}
			}

			public DrawPolyLine DrawPolyLine
			{
				[CompilerGenerated]
				get
				{
					return this.<DrawPolyLine>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DrawPolyLine>k__BackingField = value;
				}
			}

			public Tile()
			{
			}

			[CompilerGenerated]
			private double <Width>k__BackingField;

			[CompilerGenerated]
			private double <Height>k__BackingField;

			[CompilerGenerated]
			private double <Thickness>k__BackingField;

			[CompilerGenerated]
			private double <OffsetX>k__BackingField;

			[CompilerGenerated]
			private double <OffsetY>k__BackingField;

			[CompilerGenerated]
			private double <ColumnOffset>k__BackingField;

			[CompilerGenerated]
			private int <WidthInPixels>k__BackingField;

			[CompilerGenerated]
			private int <HeightInPixels>k__BackingField;

			[CompilerGenerated]
			private int <ThicknessInPixels>k__BackingField;

			[CompilerGenerated]
			private int <OffsetXInPixels>k__BackingField;

			[CompilerGenerated]
			private int <OffsetYInPixels>k__BackingField;

			[CompilerGenerated]
			private int <ColumnOffsetInPixels>k__BackingField;

			[CompilerGenerated]
			private int <Angle>k__BackingField;

			[CompilerGenerated]
			private int <EstimatedTilesCount>k__BackingField;

			[CompilerGenerated]
			private int <JointLength>k__BackingField;

			[CompilerGenerated]
			private float <JointPenWidth>k__BackingField;

			[CompilerGenerated]
			private DrawPolyLine <DrawPolyLine>k__BackingField;
		}
	}
}
