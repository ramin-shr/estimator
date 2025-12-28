using System;
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

		private int EstimatedRowsCount(double netArea, double wasteFactor, double rowWidth, double rowLength, double jointThickness)
		{
			return 0;
		}

		private CustomRenderingGenericRows.Row GetRow(DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
		{
			CustomRenderingGenericRows.Row row = new CustomRenderingGenericRows.Row();
			row.Height = Utilities.ConvertToDouble(base.Preset.FieldValue("rowWidth", 0), -1);
			if (row.Height == 0.0)
			{
				return null;
			}
			row.WidthInPixels = 1000;
			row.WidthInPixels = ((row.WidthInPixels == 0) ? 1 : row.WidthInPixels);
			row.HeightInPixels = (int)unitScale.ToPixelsFromFeet(row.Height);
			row.HeightInPixels = ((row.HeightInPixels == 0) ? 1 : row.HeightInPixels);
			row.ThicknessInPixels = (int)unitScale.ToPixelsFromFeet(row.Thickness);
			row.ThicknessInPixels = ((row.ThicknessInPixels == 0) ? 1 : row.ThicknessInPixels);
			row.OffsetXInPixels = (int)((float)row.WidthInPixels * ((float)renderingProperties.OffsetX / 100f));
			row.OffsetYInPixels = (int)((float)row.HeightInPixels * ((float)renderingProperties.OffsetY / 100f));
			row.Angle = renderingProperties.Angle;
			row.DrawPolyline = drawPolyLine;
			return row;
		}

		private List<CustomRendering.PointsRectangle> GetRows(Graphics g, Region region, CustomRenderingGenericRows.Row row)
		{
			List<CustomRendering.PointsRectangle> list = new List<CustomRendering.PointsRectangle>();
			int num = (row.Angle == 0) ? 1 : 2;
			RectangleF bounds = region.GetBounds(g);
			RectangleF bounds2 = new RectangleF(bounds.X - (float)((row.WidthInPixels + row.ColumnOffsetInPixels) * num), bounds.Y - (float)(row.HeightInPixels * num), bounds.Width + (float)((row.WidthInPixels + row.ColumnOffsetInPixels) * num * 2), bounds.Height + (float)(row.HeightInPixels * num * 2));
			if (row.Angle > 0)
			{
				CustomRendering.PointsRectangle pointsRectangle = new CustomRendering.PointsRectangle(bounds2);
				pointsRectangle.RotateAt(row.Angle, bounds2);
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddPolygon(pointsRectangle.Points);
					Region region2 = region.Clone();
					region2.Union(graphicsPath);
					bounds2 = region2.GetBounds(g);
					region2.Dispose();
				}
			}
			float top = bounds2.Top;
			int thicknessInPixels = row.ThicknessInPixels;
			float bottom = bounds2.Bottom;
			float left = bounds2.Left;
			int thicknessInPixels2 = row.ThicknessInPixels;
			float right = bounds2.Right;
			list.Add(new CustomRendering.PointsRectangle((int)bounds.Left, (int)bounds.Right, (int)bounds.Top + (int)(bounds.Height / 2f), (int)bounds.Top + (int)(bounds.Height / 2f) + 2));
			if (row.Angle > 0)
			{
				foreach (CustomRendering.PointsRectangle pointsRectangle2 in list)
				{
					pointsRectangle2.RotateAt(row.Angle, bounds);
					pointsRectangle2.Translate((float)row.OffsetXInPixels, (float)row.OffsetYInPixels);
				}
			}
			return list;
		}

		private double DistanceSquared(PointF p1, PointF p2)
		{
			return (double)((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
		}

		private PointF NearestToTopLeft(GraphicsPath gPath)
		{
			PointF result = Point.Empty;
			double num = double.MaxValue;
			foreach (PointF pointF in gPath.PathPoints)
			{
				double num2 = this.DistanceSquared(pointF, gPath.GetBounds().Location);
				if (num2 < num)
				{
					result = pointF;
					num = num2;
				}
			}
			return result;
		}

		private int CountRows(Graphics g, Region region, CustomRenderingGenericRows.Row row, GraphicsPath outlinePath, GraphicsPath gp)
		{
			int num = 0;
			RectangleF bounds = region.GetBounds(g);
			int angle = row.Angle;
			float num2 = bounds.Left;
			float num3 = bounds.Top + bounds.Height / 2f;
			float num4 = bounds.Right;
			float num5 = bounds.Top + bounds.Height / 2f;
			PointF[] array = new PointF[]
			{
				new PointF(num2, num3 + (float)row.OffsetYInPixels - (float)(row.HeightInPixels / 2)),
				new PointF(num4, num5 + (float)row.OffsetYInPixels - (float)(row.HeightInPixels / 2))
			};
			if (row.Angle != 0)
			{
				Matrix matrix = new Matrix();
				matrix.RotateAt((float)row.Angle, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
				matrix.TransformPoints(array);
			}
			num2 = array[0].X;
			num3 = array[0].Y;
			num4 = array[1].X;
			num5 = array[1].Y;
			double num6 = Math.Sqrt((double)((num2 - num4) * (num2 - num4) + (num3 - num5) * (num3 - num5)));
			Console.WriteLine("-----------------------------");
			Segment segment = new Segment();
			DrawPolyLine drawPolyline = row.DrawPolyline;
			int num7 = 1;
			for (int i = 0; i < 3; i++)
			{
				int num8 = 0;
				bool flag = false;
				Segment segment2 = new Segment();
				do
				{
					if (i != 0)
					{
						if (i == 1)
						{
							num8 -= row.HeightInPixels;
						}
						else if (i == 2)
						{
							num8 += row.HeightInPixels;
						}
					}
					int x = (int)(num2 + (float)num8 * (num5 - num3) / (float)num6);
					int x2 = (int)(num4 + (float)num8 * (num5 - num3) / (float)num6);
					int y = (int)(num3 + (float)num8 * (num2 - num4) / (float)num6);
					int y2 = (int)(num5 + (float)num8 * (num2 - num4) / (float)num6);
					GraphicsPath graphicsPath = new GraphicsPath();
					graphicsPath.AddRectangle(bounds);
					graphicsPath.CloseAllFigures();
					Segment segment3 = base.GetSegment(new Point(x, y), new Point(x2, y2), new Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height));
					graphicsPath.Dispose();
					if (segment3.IsValid())
					{
						if (segment2.IsValid() || segment.IsValid())
						{
							using (GraphicsPath graphicsPath2 = new GraphicsPath())
							{
								Point[] array2 = new Point[5];
								if (segment2.IsValid())
								{
									array2[0] = segment2.StartPoint;
									array2[1] = segment2.EndPoint;
								}
								else
								{
									array2[0] = segment.StartPoint;
									array2[1] = segment.EndPoint;
								}
								array2[2] = segment3.EndPoint;
								array2[3] = segment3.StartPoint;
								if (segment2.IsValid())
								{
									array2[4] = segment2.StartPoint;
								}
								else
								{
									array2[4] = segment.StartPoint;
								}
								graphicsPath2.AddPolygon(array2);
								g.FillPath(new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.FromArgb(drawPolyline.Opacity, drawPolyline.FillColor)), graphicsPath2);
								g.DrawPath(new Pen(Color.FromArgb(drawPolyline.Opacity + 30, Color.Black), (float)num7), graphicsPath2);
							}
						}
						if (i == 0)
						{
							segment = new Segment(segment3.StartPoint, segment3.EndPoint);
						}
						else
						{
							segment2 = new Segment(segment3.StartPoint, segment3.EndPoint);
						}
						gp.AddLine(segment3.StartPoint, segment3.EndPoint);
						gp.CloseFigure();
						int num9 = (int)drawPolyline.DrawArea.DrawingBoard.Origin.X;
						int num10 = (int)drawPolyline.DrawArea.DrawingBoard.Origin.Y;
						int distance = DrawLine.GetDistance(segment3.StartPoint, segment3.EndPoint);
						DrawLine drawLine = new DrawLine(segment3.StartPoint.X + num9, segment3.StartPoint.Y + num10, segment3.EndPoint.X + num9, segment3.EndPoint.Y + num10, new PointF(0f, 0f), "", 0, "");
						string text = drawPolyline.DisplayInPixels ? (distance.ToString() + " pixels") : drawPolyline.DrawArea.ToLengthString(distance, false);
						DrawText value = new DrawText(text, drawLine.Center, drawPolyline.TextAngle(segment3.StartPoint, segment3.EndPoint), new Segment(drawLine.StartPoint, drawLine.EndPoint));
						drawPolyline.TextArray.Add(value);
						num++;
					}
					else
					{
						flag = true;
					}
					flag = (flag || i == 0);
				}
				while (!flag);
			}
			return num;
		}

		public override string GetBasicInfo(object data)
		{
			return string.Empty;
		}

		public override void ComputeResults(Graphics g, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
		{
		}

		private void DrawManually(Graphics g, Region region, GraphicsPath outlinePath, CustomRenderingGenericRows.Row row, DrawPolyLine drawPolyLine)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				int num = this.CountRows(g, region, row, outlinePath, graphicsPath);
				if (num > 0)
				{
					int num2 = 4;
					g.DrawPath(new Pen(Color.FromArgb(drawPolyLine.Opacity + 30, Color.Black), (float)num2), graphicsPath);
				}
			}
		}

		public override void Draw(Graphics g, Region region, int offsetX, int offsetY, DrawPolyLine drawPolyLine, CustomRenderingProperties renderingProperties, UnitScale unitScale)
		{
			CustomRenderingGenericRows.Row row = this.GetRow(drawPolyLine, renderingProperties, unitScale);
		}

		private class Row
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

			public DrawPolyLine DrawPolyline
			{
				[CompilerGenerated]
				get
				{
					return this.<DrawPolyline>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DrawPolyline>k__BackingField = value;
				}
			}

			public Row()
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
			private DrawPolyLine <DrawPolyline>k__BackingField;
		}
	}
}
