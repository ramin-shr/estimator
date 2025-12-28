using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class DrawPolyLine : DrawLine
	{
		private HatchStylePickerCombo.HatchStylePickerEnum ValidateHatchStyle(HatchStylePickerCombo.HatchStylePickerEnum hatchStyle)
		{
			if (hatchStyle < HatchStylePickerCombo.HatchStylePickerEnum.Solid)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Solid;
			}
			if (hatchStyle < HatchStylePickerCombo.HatchStylePickerEnum.SolidDiamond)
			{
				return hatchStyle;
			}
			return HatchStylePickerCombo.HatchStylePickerEnum.SolidDiamond;
		}

		public HatchStylePickerCombo.HatchStylePickerEnum Pattern
		{
			get
			{
				return this.ValidateHatchStyle(this.pattern);
			}
			set
			{
				this.pattern = this.ValidateHatchStyle(value);
			}
		}

		public ArrayList PointArray
		{
			get
			{
				return this.pointArray;
			}
			set
			{
				this.pointArray = value;
			}
		}

		public ArrayList DeductionArray
		{
			get
			{
				return this.deductionArray;
			}
			set
			{
				this.deductionArray = value;
			}
		}

		public ArrayList DropArray
		{
			get
			{
				return this.dropArray;
			}
			set
			{
				this.dropArray = value;
			}
		}

		public Dictionary<string, CustomRenderingProperties> CustomRenderingArray
		{
			get
			{
				return this.customRenderingArray;
			}
		}

		public UnitScale UnitScale
		{
			get
			{
				if (this.unitScale != null)
				{
					return this.unitScale;
				}
				return base.DrawArea.UnitScale;
			}
			set
			{
				this.unitScale = value;
			}
		}

		public bool ComputingCustomRendering
		{
			get
			{
				return this.unitScale != null;
			}
		}

		public string CustomRenderingTooltip
		{
			get
			{
				return this.customRenderingTooltip;
			}
			set
			{
				this.customRenderingTooltip = value;
			}
		}

		public Segment SelectedSegment
		{
			get
			{
				return this.selectedSegment;
			}
			set
			{
				this.selectedSegment = value;
			}
		}

		public bool CloseFigure
		{
			get
			{
				return this.closeFigure;
			}
			set
			{
				this.closeFigure = value;
			}
		}

		public new bool FigureCompleted
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

		public override bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				if (!value)
				{
					this.SelectedSegment.Reset();
				}
				this.selected = value;
			}
		}

		public override void SetSlopeFactor(SlopeFactor slopeFactor)
		{
			this.SlopeFactor.SetValues(slopeFactor);
			foreach (object obj in this.DeductionArray)
			{
				DrawObject drawObject = (DrawObject)obj;
				drawObject.SetSlopeFactor(slopeFactor);
			}
		}

		private int GetPointIndex(Point point)
		{
			for (int i = 0; i < this.pointArray.Count; i++)
			{
				if (((Point)this.pointArray[i]).X == point.X && ((Point)this.pointArray[i]).Y == point.Y)
				{
					return i;
				}
			}
			Console.WriteLine("GetPointIndex::Unable to find point index:" + point.ToString());
			return -1;
		}

		public DrawLine GetOpeningFromHandle(Point handlePoint)
		{
			foreach (object obj in this.deductionArray)
			{
				DrawLine drawLine = (DrawLine)obj;
				if (handlePoint == drawLine.StartPoint || handlePoint == drawLine.EndPoint)
				{
					return drawLine;
				}
			}
			return null;
		}

		public bool IsHandlePartOfOpening(Point handlePoint)
		{
			bool result;
			try
			{
				foreach (object obj in this.deductionArray)
				{
					DrawLine drawLine = (DrawLine)obj;
					if (handlePoint == drawLine.StartPoint || handlePoint == drawLine.EndPoint)
					{
						return true;
					}
				}
				result = false;
			}
			catch (Exception ex)
			{
				Console.WriteLine("HandleIsPartOfOpening failed.");
				Console.WriteLine(ex.Message);
				result = false;
			}
			return result;
		}

		public DrawLine GetOpening(Point startPoint, Point endPoint)
		{
			DrawLine result;
			try
			{
				foreach (object obj in this.deductionArray)
				{
					DrawLine drawLine = (DrawLine)obj;
					if ((startPoint == drawLine.StartPoint && endPoint == drawLine.EndPoint) || (startPoint == drawLine.EndPoint && endPoint == drawLine.StartPoint))
					{
						return drawLine;
					}
				}
				result = null;
			}
			catch (Exception ex)
			{
				Console.WriteLine("GetOpening failed.");
				Console.WriteLine(ex.Message);
				result = null;
			}
			return result;
		}

		public bool IsOpening(Point startPoint, Point endPoint)
		{
			return !base.Filled && this.GetOpening(startPoint, endPoint) != null;
		}

		public void CreateOpening(DrawLine drawLine, Segment segment, bool selectOpening, bool createFromSegment = false)
		{
			int nearestPointIndex = this.GetNearestPointIndex(drawLine.StartPoint);
			if (nearestPointIndex == -1)
			{
				Console.WriteLine("CreateOpening::GetNearestPointIndex:failed");
				return;
			}
			if (!createFromSegment)
			{
				Point point = new Point(drawLine.StartPoint.X, drawLine.StartPoint.Y);
				Point point2 = new Point(drawLine.EndPoint.X, drawLine.EndPoint.Y);
				bool flag = DrawLine.LineIsReverse(point, point2);
				if (!DrawLine.LineIsReverse(segment.StartPoint, segment.EndPoint))
				{
					this.InsertPoint((!flag) ? point : point2, nearestPointIndex + 1);
					this.InsertPoint((!flag) ? point2 : point, nearestPointIndex + 2);
				}
				else
				{
					this.InsertPoint((!flag) ? point2 : point, nearestPointIndex + 1);
					this.InsertPoint((!flag) ? point : point2, nearestPointIndex + 2);
				}
			}
			this.CreateDeduction(drawLine);
			if (selectOpening)
			{
				base.DrawArea.Clipboard.SelectedOpening = new Clipboard.Opening(drawLine, base.DrawArea.UnitScale);
			}
		}

		public void DeleteOpening(Point startPoint, Point endPoint)
		{
			try
			{
				DrawLine opening = this.GetOpening(startPoint, endPoint);
				if (opening != null)
				{
					opening.ID = -1;
					this.deductionArray.Remove(opening);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("DeleteOpening failed.");
				Console.WriteLine(ex.Message);
			}
		}

		public double GetOpeningHeight(Point startPoint, Point endPoint)
		{
			double result = 0.0;
			try
			{
				DrawLine opening = this.GetOpening(startPoint, endPoint);
				if (opening != null)
				{
					result = opening.Height;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("GetOpeningHeight failed.");
				Console.WriteLine(ex.Message);
			}
			return result;
		}

		public void SetOpeningHeight(Point startPoint, Point endPoint, double height)
		{
			try
			{
				DrawLine opening = this.GetOpening(startPoint, endPoint);
				if (opening != null)
				{
					opening.Height = height;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("SetOpeningHeight failed.");
				Console.WriteLine(ex.Message);
			}
		}

		public int CreateDeduction(DrawObject deduction)
		{
			deduction.DeductionParentID = base.ID;
			deduction.SetSlopeFactor(this.SlopeFactor);
			return this.deductionArray.Add(deduction);
		}

		public void DeleteDeduction(DrawObject deduction)
		{
			for (int i = this.deductionArray.Count - 1; i >= 0; i--)
			{
				if (((DrawObject)this.deductionArray[i]).ID == deduction.ID)
				{
					((DrawObject)this.deductionArray[i]).ID = -1;
					this.deductionArray.RemoveAt(i);
					return;
				}
			}
		}

		public bool EditDeductions
		{
			get
			{
				return this.editDeductions;
			}
			set
			{
				this.editDeductions = value;
			}
		}

		public Point IsPartOfDrop(Point point, int offsetX, int offsetY)
		{
			foreach (object obj in this.dropArray)
			{
				DrawLine drawLine = (DrawLine)obj;
				if (DrawRectangle.GetNormalizedRectangle(new Rectangle(drawLine.StartPoint.X - 10, drawLine.StartPoint.Y - 10, 20, 20)).Contains(point.X + offsetX, point.Y + offsetY))
				{
					return drawLine.StartPoint;
				}
			}
			return Point.Empty;
		}

		public void ResetDrops()
		{
			foreach (object obj in this.dropArray)
			{
				DrawLine drawLine = (DrawLine)obj;
				PointF empty = PointF.Empty;
				double num = double.PositiveInfinity;
				for (int i = 0; i < this.pointArray.Count - (this.closeFigure ? 0 : 1); i++)
				{
					Point[] array = new Point[2];
					if (this.closeFigure && i + 1 == this.pointArray.Count)
					{
						array[0] = (Point)this.pointArray[this.pointArray.Count - 1];
						array[1] = (Point)this.pointArray[0];
					}
					else
					{
						array[0] = (Point)this.pointArray[i];
						array[1] = (Point)this.pointArray[i + 1];
					}
					PointF pointF;
					double num2 = DrawLine.FindDistanceToSegment(drawLine.StartPoint, array[0], array[1], out pointF);
					if (num2 < num)
					{
						num = num2;
						empty = new PointF(pointF.X, pointF.Y);
					}
				}
				if (empty != PointF.Empty)
				{
					drawLine.StartPoint = new Point((int)Math.Round((double)empty.X, MidpointRounding.ToEven), (int)Math.Round((double)empty.Y, MidpointRounding.ToEven));
				}
			}
		}

		public DrawLine FindDrop(Point p)
		{
			DrawLine result = null;
			for (int i = this.dropArray.Count - 1; i >= 0; i--)
			{
				if (((DrawLine)this.dropArray[i]).StartPoint.Equals(p))
				{
					result = (DrawLine)this.dropArray[i];
					break;
				}
			}
			return result;
		}

		public double GetDropLength(Point dropPoint)
		{
			double result = 0.0;
			try
			{
				DrawLine drawLine = this.FindDrop(dropPoint);
				if (drawLine != null)
				{
					result = drawLine.Height;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("GetDropLength failed.");
				Console.WriteLine(ex.Message);
			}
			return result;
		}

		public void SetDropLength(Point dropPoint, double length)
		{
			try
			{
				DrawLine drawLine = this.FindDrop(dropPoint);
				if (drawLine != null)
				{
					drawLine.Height = length;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("SetDropLength failed.");
				Console.WriteLine(ex.Message);
			}
		}

		public DrawLine CreateDrop(Point p, double length)
		{
			int nearestPointIndex = this.GetNearestPointIndex(p);
			if (nearestPointIndex == -1)
			{
				Console.WriteLine("CreateDrop::GetNearestPointIndex:failed");
				return null;
			}
			DrawLine drawLine = new DrawLine();
			drawLine.StartPoint = new Point(p.X, p.Y);
			drawLine.Height = length;
			this.dropArray.Add(drawLine);
			return drawLine;
		}

		public void DeleteDrop(Point p)
		{
			DrawLine drawLine = this.FindDrop(p);
			if (drawLine == null)
			{
				Console.WriteLine("DeleteDrop::FindDrop:failed");
				return;
			}
			this.dropArray.Remove(drawLine);
		}

		public bool PointIsOnOutline
		{
			get
			{
				return this.pointIsOnOutline;
			}
		}

		private int CornersCount()
		{
			int num = 0;
			for (int i = 0; i < this.pointArray.Count - (this.closeFigure ? 0 : 2); i++)
			{
				Point handlePoint = (Point)this.pointArray[i];
				if (!this.IsHandlePartOfOpening(handlePoint))
				{
					num++;
				}
			}
			return num;
		}

		public GroupStats Stats
		{
			get
			{
				GroupStats groupStats = new GroupStats(base.ObjectType);
				groupStats.GroupCount = 1;
				groupStats.Perimeter = this.ComputePerimeter();
				groupStats.DeductionsCount = this.deductionArray.Count;
				foreach (object obj in this.deductionArray)
				{
					DrawObject drawObject = (DrawObject)obj;
					if (drawObject.ObjectType == "Area")
					{
						groupStats.DeductionArea += ((DrawPolyLine)drawObject).ComputeArea(true);
						groupStats.DeductionPerimeter += ((DrawPolyLine)drawObject).ComputePerimeter();
					}
					else if (drawObject.ObjectType == "Line")
					{
						groupStats.DeductionPerimeter += (double)((DrawLine)drawObject).Distance2D(true);
					}
				}
				if (this.closeFigure)
				{
					groupStats.Area = this.ComputeArea(true);
					groupStats.EndsCount = this.deductionArray.Count * 2;
					groupStats.SegmentsCount = this.pointArray.Count - this.deductionArray.Count;
					groupStats.CornersCount = this.CornersCount();
				}
				else
				{
					groupStats.EndsCount = 2 + this.deductionArray.Count * 2;
					groupStats.SegmentsCount = this.pointArray.Count - (1 + this.deductionArray.Count);
					groupStats.CornersCount = this.CornersCount();
				}
				foreach (KeyValuePair<string, CustomRenderingProperties> keyValuePair in this.CustomRenderingArray)
				{
					CustomRenderingProperties value = keyValuePair.Value;
					Dictionary<string, CustomRenderingResult> dictionary = new Dictionary<string, CustomRenderingResult>();
					foreach (KeyValuePair<string, CustomRenderingResult> keyValuePair2 in value.ResultsArray)
					{
						dictionary.Add(keyValuePair2.Key, keyValuePair2.Value.Clone());
					}
					groupStats.CustomRenderingArray.Add(keyValuePair.Key, dictionary);
				}
				return groupStats;
			}
		}

		public override DrawObject Clone()
		{
			DrawPolyLine drawPolyLine = new DrawPolyLine();
			drawPolyLine.StartPoint = new Point(base.StartPoint.X, base.StartPoint.Y);
			drawPolyLine.EndPoint = new Point(base.EndPoint.X, base.EndPoint.Y);
			drawPolyLine.pointArray = new ArrayList();
			for (int i = 0; i < this.pointArray.Count; i++)
			{
				Point point = (Point)this.pointArray[i];
				drawPolyLine.pointArray.Add(new Point(point.X, point.Y));
			}
			for (int j = 0; j < this.deductionArray.Count; j++)
			{
				if (base.Filled)
				{
					drawPolyLine.deductionArray.Add(((DrawPolyLine)this.deductionArray[j]).Clone());
				}
				else
				{
					drawPolyLine.deductionArray.Add(((DrawLine)this.deductionArray[j]).Clone());
				}
			}
			for (int k = 0; k < this.dropArray.Count; k++)
			{
				drawPolyLine.dropArray.Add(((DrawLine)this.dropArray[k]).Clone());
			}
			foreach (KeyValuePair<string, CustomRenderingProperties> keyValuePair in this.CustomRenderingArray)
			{
				drawPolyLine.customRenderingArray.Add(keyValuePair.Key, keyValuePair.Value.Clone());
			}
			drawPolyLine.Pattern = this.pattern;
			drawPolyLine.CloseFigure = this.closeFigure;
			drawPolyLine.FigureCompleted = true;
			drawPolyLine.DisplayName = new Utilities.DisplayName(drawPolyLine, "");
			drawPolyLine.SetSlopeFactor(this.SlopeFactor.InternalValue, this.SlopeFactor.SlopeType, this.SlopeFactor.SlopeApplyType, this.SlopeFactor.HipValley);
			base.FillDrawObjectFields(drawPolyLine);
			return drawPolyLine;
		}

		public override Rectangle BoundingRectangle
		{
			get
			{
				Point point = Point.Empty;
				Point left = Point.Empty;
				Point empty = Point.Empty;
				for (int i = 0; i < this.pointArray.Count; i++)
				{
					point = (Point)this.pointArray[i];
					if (left == Point.Empty)
					{
						left = point;
					}
					else
					{
						if (point.X < left.X)
						{
							left.X = point.X;
						}
						if (point.Y < left.Y)
						{
							left.Y = point.Y;
						}
					}
					if (point.X > empty.X)
					{
						empty.X = point.X;
					}
					if (point.Y > empty.Y)
					{
						empty.Y = point.Y;
					}
				}
				return new Rectangle(left.X, left.Y, Math.Abs(empty.X - left.X), Math.Abs(empty.Y - left.Y));
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
			if (this.pointArray.Count < 2)
			{
				return new Point(0, 0);
			}
			if (this.pointArray.Count == 2)
			{
				return new DrawLine
				{
					StartPoint = (Point)this.pointArray[0],
					EndPoint = (Point)this.pointArray[1]
				}.Center;
			}
			double num = this.ComputeArea(false);
			if (num > 0.0)
			{
				return this.FindCentroid(num);
			}
			return new Point(0, 0);
		}

		public override Region Region(int offsetX, int offsetY, float zoomFactor)
		{
			Region region = new Region();
			GraphicsPath graphicsPath = new GraphicsPath();
			offsetX -= (int)((float)base.DrawArea.DrawingBoard.HorizontalOffset / zoomFactor);
			offsetY -= (int)((float)base.DrawArea.DrawingBoard.VerticalOffset / zoomFactor);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			IEnumerator enumerator = this.pointArray.GetEnumerator();
			if (enumerator.MoveNext())
			{
				Point point = (Point)enumerator.Current;
				num2 = point.X;
				Point point2 = (Point)enumerator.Current;
				num3 = point2.Y;
				num++;
			}
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				int x = ((Point)obj).X;
				Point point3 = (Point)enumerator.Current;
				int y = point3.Y;
				num++;
				graphicsPath.AddLine((float)(num2 - offsetX) * zoomFactor, (float)(num3 - offsetY) * zoomFactor, (float)(x - offsetX) * zoomFactor, (float)(y - offsetY) * zoomFactor);
				num2 = x;
				num3 = y;
			}
			graphicsPath.CloseFigure();
			RectangleF bounds = graphicsPath.GetBounds();
			bounds.Inflate(new Size(20 + base.PenWidth, 20 + base.PenWidth));
			region.MakeEmpty();
			region.Union(bounds);
			if (base.ShowMeasure)
			{
				foreach (object obj2 in base.TextArray)
				{
					DrawText drawText = (DrawText)obj2;
					Rectangle rect = new Rectangle(drawText.Point.X - offsetX - drawText.Rectangle.Width / 2, drawText.Point.Y - offsetY - drawText.Rectangle.Height / 2, drawText.Rectangle.Width, drawText.Rectangle.Height);
					rect.X = (int)((float)rect.X * zoomFactor);
					rect.Y = (int)((float)rect.Y * zoomFactor);
					rect.Width = (int)((float)rect.Width * zoomFactor);
					rect.Height = (int)((float)rect.Height * zoomFactor);
					rect.Inflate(new Size(base.PenWidth, base.PenWidth));
					region.Union(rect);
				}
			}
			for (int i = 1; i <= this.pointArray.Count; i++)
			{
				Rectangle handleRectangle = this.GetHandleRectangle(i, offsetX, offsetY);
				handleRectangle.X = (int)((float)handleRectangle.X * zoomFactor);
				handleRectangle.Y = (int)((float)handleRectangle.Y * zoomFactor);
				handleRectangle.Width = (int)((float)handleRectangle.Width * zoomFactor);
				handleRectangle.Height = (int)((float)handleRectangle.Height * zoomFactor);
				region.Union(handleRectangle);
			}
			graphicsPath.Dispose();
			return region;
		}

		public DrawPolyLine()
		{
			this.pointArray = new ArrayList();
			this.deductionArray = new ArrayList();
			this.dropArray = new ArrayList();
			this.FigureCompleted = false;
			base.Initialize();
		}

		public DrawPolyLine(PointF offset, string name, int groupID, string comment, Color lineColor, int opacity, int lineWidth)
		{
			this.pointArray = new ArrayList();
			this.deductionArray = new ArrayList();
			this.dropArray = new ArrayList();
			base.Offset = offset;
			base.Color = lineColor;
			base.Opacity = opacity;
			base.Filled = false;
			this.CloseFigure = false;
			base.PenWidth = lineWidth;
			this.FigureCompleted = true;
			base.Name = name;
			base.GroupID = groupID;
			base.Comment = comment;
			base.Initialize();
		}

		public DrawPolyLine(PointF offset, string name, int groupID, string comment, Color lineColor, Color fillColor, HatchStylePickerCombo.HatchStylePickerEnum pattern, int opacity, int lineWidth)
		{
			this.pointArray = new ArrayList();
			this.deductionArray = new ArrayList();
			this.dropArray = new ArrayList();
			base.Offset = offset;
			base.Color = lineColor;
			base.FillColor = fillColor;
			this.Pattern = pattern;
			base.Opacity = opacity;
			base.Filled = true;
			this.CloseFigure = true;
			base.PenWidth = lineWidth;
			this.FigureCompleted = true;
			base.Name = name;
			base.GroupID = groupID;
			base.Comment = comment;
			base.Initialize();
		}

		public DrawPolyLine(int x1, int y1, int x2, int y2, PointF offset, string name, int groupID, string comment, Color lineColor, Color fillColor, HatchStylePickerCombo.HatchStylePickerEnum pattern, int opacity, bool filled, bool closeFigure, int lineWidth)
		{
			this.pointArray = new ArrayList();
			this.pointArray.Add(new Point(x1, y1));
			this.pointArray.Add(new Point(x2, y2));
			this.deductionArray = new ArrayList();
			this.dropArray = new ArrayList();
			base.Offset = offset;
			base.Color = lineColor;
			base.FillColor = fillColor;
			this.Pattern = pattern;
			base.Opacity = opacity;
			base.Filled = filled;
			this.CloseFigure = closeFigure;
			base.PenWidth = lineWidth;
			base.Name = name;
			base.GroupID = groupID;
			base.Comment = comment;
			this.FigureCompleted = false;
			base.Initialize();
		}

		~DrawPolyLine()
		{
		}

		public Point LocatePointOnSegment(Point point)
		{
			return base.LocatePointOnLine(point, this.SelectedSegment.StartPoint, this.SelectedSegment.EndPoint);
		}

		public int GetNearestPointIndexOnSegment(Point point)
		{
			Point point2 = this.LocatePointOnSegment(point);
			return this.GetNearestPointIndex(point2);
		}

		public int GetNearestPointIndex(Point point)
		{
			int result = -1;
			try
			{
				result = 0;
				PointF pointF;
				double num = DrawLine.FindDistanceToSegment(point, (Point)this.PointArray[0], (Point)this.PointArray[1], out pointF);
				for (int i = 0; i < this.PointArray.Count - 1; i++)
				{
					double num2 = DrawLine.FindDistanceToSegment(point, (Point)this.PointArray[i], (Point)this.PointArray[i + 1], out pointF);
					if (num2 <= num)
					{
						num = num2;
						result = i;
					}
				}
				if (this.CloseFigure)
				{
					double num3 = DrawLine.FindDistanceToSegment(point, (Point)this.PointArray[this.PointArray.Count - 1], (Point)this.PointArray[0], out pointF);
					if (num3 <= num)
					{
						result = this.PointArray.Count - 1;
					}
				}
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		public double Angle(Point a, Point b, Point c)
		{
			PointF pointF = new PointF((float)(b.X - a.X), (float)(b.Y - a.Y));
			PointF pointF2 = new PointF((float)(b.X - c.X), (float)(b.Y - c.Y));
			double x = (double)(pointF.X * pointF2.X + pointF.Y * pointF2.Y);
			double y = (double)(pointF.X * pointF2.Y - pointF.Y * pointF2.X);
			double num = Math.Atan2(y, x);
			return Math.Abs(num * 180.0 / 3.141592653589793 + 0.5);
		}

		public Point FindCentroid(double area)
		{
			int count = this.pointArray.Count;
			Point[] array = new Point[count + 1];
			this.pointArray.CopyTo(array, 0);
			array[count] = (Point)this.pointArray[0];
			double num = 0.0;
			double num2 = 0.0;
			for (int i = 0; i < count; i++)
			{
				Point[] array2 = new Point[]
				{
					array[i],
					array[i + 1]
				};
				double num3 = (double)(array2[0].X * array2[1].Y - array2[1].X * array2[0].Y);
				num += (double)(array2[0].X + array2[1].X) * num3;
				num2 += (double)(array2[0].Y + array2[1].Y) * num3;
			}
			num /= 6.0 * area;
			num2 /= 6.0 * area;
			if (num < 0.0)
			{
				num = -num;
				num2 = -num2;
			}
			return new Point((int)num, (int)num2);
		}

		private double GetDeterminant(double x1, double y1, double x2, double y2)
		{
			return x1 * y2 - x2 * y1;
		}

		private double ComputeArea(bool applySlopFactor)
		{
			if (this.pointArray.Count < 3)
			{
				return 0.0;
			}
			Point[] array = new Point[]
			{
				(Point)this.pointArray[0],
				(Point)this.pointArray[this.pointArray.Count - 1]
			};
			double num = this.GetDeterminant((double)array[1].X, (double)array[1].Y, (double)array[0].X, (double)array[0].Y);
			for (int i = 1; i < this.pointArray.Count; i++)
			{
				array[0] = (Point)this.pointArray[i];
				array[1] = (Point)this.pointArray[i - 1];
				num += this.GetDeterminant((double)array[1].X, (double)array[1].Y, (double)array[0].X, (double)array[0].Y);
			}
			num /= 2.0;
			num = Math.Abs(num);
			if (applySlopFactor && this.SlopeFactor.InternalValue > 0.0)
			{
				num = this.SlopeFactor.Apply(num, 0.0, 0.0);
			}
			return num;
		}

		private double ComputePerimeter()
		{
			if (this.pointArray.Count < 2)
			{
				return 0.0;
			}
			int num = 0;
			double num2 = 0.0;
			Point[] array = new Point[2];
			for (int i = 0; i < this.pointArray.Count - 1; i++)
			{
				array[0] = (Point)this.pointArray[i];
				array[1] = (Point)this.pointArray[i + 1];
				num2 += (double)base.Distance2D(array[0].X, array[0].Y, array[1].X, array[1].Y, !base.Filled, ref num);
			}
			if (this.CloseFigure)
			{
				array[0] = (Point)this.pointArray[this.pointArray.Count - 1];
				array[1] = (Point)this.pointArray[0];
				num2 += (double)base.Distance2D(array[0].X, array[0].Y, array[1].X, array[1].Y, !base.Filled, ref num);
			}
			return num2;
		}

		public DrawPolyLine.WindingDirection DetermineWindingDirection()
		{
			Point[] array = new Point[this.pointArray.Count + 1];
			this.pointArray.CopyTo(array, 0);
			array[this.pointArray.Count] = (Point)this.pointArray[0];
			float x = (float)((int)array.Average((Point p) => p.X));
			float y = (float)((int)array.Average((Point p) => p.Y));
			PointF pointInPolygon = new PointF(x, y);
			double num = 0.0;
			List<PointF> list = array.Select(delegate(Point point)
			{
				PointF result = new PointF((float)point.X - pointInPolygon.X, (float)point.Y - pointInPolygon.Y);
				return result;
			}).ToList<PointF>();
			for (int i = 0; i < list.Count; i++)
			{
				int index = (i + 1 == list.Count) ? 0 : (i + 1);
				double num2 = (double)list[i].X;
				double num3 = (double)list[index].X;
				double num4 = (double)list[i].Y;
				double num5 = (double)list[index].Y;
				if (num4 * num5 < 0.0)
				{
					double num6 = num2 + num4 * (num3 - num2) / (num4 - num5);
					if (num6 > 0.0)
					{
						if (num4 < 0.0)
						{
							num += 1.0;
						}
						else
						{
							num -= 1.0;
						}
					}
				}
				else if (num4 == 0.0 && num2 > 0.0)
				{
					if (num5 > 0.0)
					{
						num += 0.5;
					}
					else
					{
						num -= 0.5;
					}
				}
				else if (num5 == 0.0 && num3 > 0.0)
				{
					if (num4 < 0.0)
					{
						num += 0.5;
					}
					else
					{
						num -= 0.5;
					}
				}
			}
			if (num <= 0.0)
			{
				return DrawPolyLine.WindingDirection.CounterClockWise;
			}
			return DrawPolyLine.WindingDirection.Clockwise;
		}

		private Point MatchPointToColor(Bitmap image, Point point, int angle, Color colorToMatch, bool directionOutward, int maxOffset)
		{
			bool flag = false;
			int num = 0;
			Point result = new Point(point.X, point.Y);
			int x = point.X;
			int y = point.Y;
			while (result.X >= 0 && result.Y >= 0 && result.X < image.Width && result.Y < image.Height)
			{
				Color pixel = image.GetPixel(result.X, result.Y);
				if (colorToMatch.R == 0 && colorToMatch.G == 0 && colorToMatch.B == 0)
				{
					if (pixel.R != 255 && pixel.G != 255 && pixel.B != 255)
					{
						return result;
					}
				}
				else if (pixel.R == colorToMatch.R && pixel.G == colorToMatch.G && pixel.B == colorToMatch.B)
				{
					return result;
				}
				int x2 = result.X;
				int y2 = result.Y;
				if (angle <= 90)
				{
					if (angle != 0)
					{
						if (angle != 90)
						{
							goto IL_1F4;
						}
						if (directionOutward)
						{
							result.X++;
						}
						else
						{
							result.X--;
						}
					}
					else if (directionOutward)
					{
						result.Y--;
					}
					else
					{
						result.Y++;
					}
				}
				else if (angle != 180)
				{
					if (angle != 270)
					{
						goto IL_1F4;
					}
					if (directionOutward)
					{
						result.X--;
					}
					else
					{
						result.X++;
					}
				}
				else if (directionOutward)
				{
					result.Y++;
				}
				else
				{
					result.Y--;
				}
				IL_1F6:
				num++;
				if (flag)
				{
					return result;
				}
				continue;
				IL_1F4:
				flag = true;
				goto IL_1F6;
			}
			if (result.X < 0)
			{
				result.X = 0;
			}
			if (result.Y < 0)
			{
				result.Y = 0;
				return result;
			}
			return result;
		}

		private int MatchSegmentToColor(Bitmap image, ArrayList inputArray, int angle, int normalizedAngle, Color colorToMatch, bool directionOutward, int maxOffset, ref Point pointA, ref Point pointB, ref int resultOffset)
		{
			int num = 0;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in inputArray)
			{
				Point point = (Point)obj;
				Point point2 = this.MatchPointToColor(image, point, normalizedAngle, colorToMatch, directionOutward, maxOffset);
				if (point2.X > 0 || point2.Y > 0)
				{
					int num2;
					int otherValue;
					int num3;
					if (normalizedAngle == 0 || normalizedAngle == 180)
					{
						num2 = point2.Y;
						otherValue = point2.X;
						num3 = Math.Abs(point.Y - point2.Y);
					}
					else
					{
						num2 = point2.X;
						otherValue = point2.Y;
						num3 = Math.Abs(point.X - point2.X);
					}
					num += num3;
					bool flag = false;
					foreach (object obj2 in arrayList)
					{
						DrawPolyLine.ValueCount valueCount = (DrawPolyLine.ValueCount)obj2;
						if (valueCount.value == num2)
						{
							valueCount.count++;
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						arrayList.Add(new DrawPolyLine.ValueCount(num2, otherValue, num3, 1));
					}
				}
			}
			int result = 0;
			int num4 = 0;
			int num5 = 0;
			foreach (object obj3 in arrayList)
			{
				DrawPolyLine.ValueCount valueCount2 = (DrawPolyLine.ValueCount)obj3;
				if (valueCount2.count > num5)
				{
					result = valueCount2.value;
					num4 = valueCount2.offset;
					num5 = valueCount2.count;
				}
			}
			resultOffset = num4;
			Console.WriteLine("Offset moyen = " + resultOffset);
			return result;
		}

		private Point AdjustSegmentToImage(Bitmap image, Point point1, Point point2, int angle, int normalizedAngle, int maxOffset)
		{
			Point result = default(Point);
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			for (int i = 1; i <= 100; i++)
			{
				float num = (float)i / 100f;
				result.X = (int)((float)point1.X + num * (float)(point2.X - point1.X));
				result.Y = (int)((float)point1.Y + num * (float)(point2.Y - point1.Y));
				Color pixel = image.GetPixel(result.X, result.Y);
				if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
				{
					arrayList2.Add(new Point(result.X, result.Y));
				}
				else
				{
					arrayList.Add(new Point(result.X, result.Y));
				}
			}
			Console.WriteLine("normalizedAngle = " + normalizedAngle);
			if (arrayList.Count >= arrayList2.Count)
			{
				Console.WriteLine("hit % = " + (float)arrayList.Count / 100f * 100f);
				int num2 = 0;
				Point[] array = new Point[2];
				int num3 = this.MatchSegmentToColor(image, arrayList, angle, normalizedAngle, Color.White, false, maxOffset, ref array[0], ref array[1], ref num2);
				if (normalizedAngle == 0 || normalizedAngle == 270)
				{
					num3 -= 2;
				}
				else
				{
					num3 += 2;
				}
				if (normalizedAngle == 0 || normalizedAngle == 180)
				{
					result = new Point(point1.X, num3);
				}
				else
				{
					result = new Point(num3, point1.Y);
				}
				return result;
			}
			Console.WriteLine("miss % = " + (float)arrayList2.Count / 100f * 100f);
			int[] array2 = new int[2];
			int[] array3 = new int[2];
			Point[] array4 = new Point[2];
			Point[] array5 = new Point[2];
			array2[0] = this.MatchSegmentToColor(image, arrayList2, angle, normalizedAngle, Color.Black, false, maxOffset, ref array4[0], ref array5[0], ref array3[0]);
			array2[1] = this.MatchSegmentToColor(image, arrayList2, angle, normalizedAngle, Color.Black, true, maxOffset, ref array4[1], ref array5[1], ref array3[1]);
			int num4 = (array3[0] <= array3[1]) ? 0 : 1;
			Console.WriteLine((num4 == 0) ? "Compress" : "Expand");
			if (array3[num4] <= 50)
			{
				for (int j = 0; j < arrayList2.Count; j++)
				{
					if (normalizedAngle == 0 || normalizedAngle == 180)
					{
						arrayList2[j] = new Point(((Point)arrayList2[j]).X, array2[num4]);
					}
					else
					{
						arrayList2[j] = new Point(array2[num4], ((Point)arrayList2[j]).Y);
					}
				}
				array2[num4] = this.MatchSegmentToColor(image, arrayList2, angle, normalizedAngle, Color.White, false, maxOffset, ref array4[0], ref array5[0], ref array3[num4]);
				if (normalizedAngle == 0 || normalizedAngle == 270)
				{
					array2[num4] -= 2;
				}
				else
				{
					array2[num4] += 2;
				}
				if (normalizedAngle == 0 || normalizedAngle == 180)
				{
					result = new Point(point1.X, array2[num4]);
				}
				else
				{
					result = new Point(array2[num4], point1.Y);
				}
			}
			else
			{
				result = new Point(0, 0);
			}
			return result;
		}

		public override bool AutoAdjust(Bitmap image, DrawPolyLine.AutoAdjustType autoAdjustType)
		{
			if (this.pointArray.Count < 4 || !this.FigureCompleted || !this.CloseFigure)
			{
				return false;
			}
			for (int i = 0; i < this.pointArray.Count; i++)
			{
				if (((Point)this.pointArray[i]).X < 0 || ((Point)this.pointArray[i]).Y < 0)
				{
					string objet_hors_canvenas = Resources.Objet_hors_canvenas;
					string message = Resources.Impossible_de_compléter_l_opération + "\n" + Resources.Veuillez_déplacer_l_objet_sur_le_canvenas_et_recommencer;
					Utilities.DisplayError(objet_hors_canvenas, message);
					return false;
				}
			}
			Console.WriteLine("");
			Console.WriteLine("DrawPolyLine::AutoAdjust ENTER");
			bool flag = this.DetermineWindingDirection() == DrawPolyLine.WindingDirection.CounterClockWise;
			Point[] array = new Point[this.pointArray.Count + 1];
			this.pointArray.CopyTo(array, 0);
			array[this.pointArray.Count] = (Point)this.pointArray[0];
			for (int j = 0; j < this.pointArray.Count; j++)
			{
				Point[] array2 = new Point[]
				{
					array[j],
					array[j + 1]
				};
				string.Concat(new object[]
				{
					"(",
					array2[0].X,
					",",
					array2[0].Y,
					") - (",
					array2[1].X,
					",",
					array2[1].Y,
					")"
				});
				double num = DrawLine.Angle((double)array2[0].X, (double)array2[0].Y, (double)array2[1].X, (double)array2[1].Y);
				double num2 = base.RoundAngle(num, 10);
				if (num2 == 0.0 || num2 == 90.0 || num2 == 180.0 || num2 == 270.0 || autoAdjustType != DrawPolyLine.AutoAdjustType.ToZone)
				{
					int num3 = (int)num2;
					if (flag)
					{
						int num4 = (int)num2;
						if (num4 <= 90)
						{
							if (num4 != 0)
							{
								if (num4 == 90)
								{
									num3 = 270;
								}
							}
							else
							{
								num3 = 180;
							}
						}
						else if (num4 != 180)
						{
							if (num4 == 270)
							{
								num3 = 90;
							}
						}
						else
						{
							num3 = 0;
						}
					}
					Point point;
					if (autoAdjustType == DrawPolyLine.AutoAdjustType.ToZone)
					{
						point = this.AdjustSegmentToImage(image, array2[0], array2[1], (int)num, num3, 50);
						this.compressLeft = true;
						this.compressTop = true;
						this.expandLeft = true;
						this.expandTop = true;
					}
					else
					{
						point = new Point(array2[0].X, array2[0].Y);
					}
					if (point.X > 0 && point.Y > 0)
					{
						int num5 = num3;
						if (num5 <= 90)
						{
							if (num5 != 0)
							{
								if (num5 == 90)
								{
									switch (autoAdjustType)
									{
									case DrawPolyLine.AutoAdjustType.Compress:
										point.X--;
										break;
									case DrawPolyLine.AutoAdjustType.CompressLeftRight:
										if (!this.compressLeft)
										{
											point.X--;
										}
										break;
									case DrawPolyLine.AutoAdjustType.Expand:
										point.X++;
										break;
									case DrawPolyLine.AutoAdjustType.ExpandLeftRight:
										if (!this.expandLeft)
										{
											point.X++;
										}
										break;
									}
									this.pointArray[j] = new Point(point.X, ((Point)this.pointArray[j]).Y);
									if (j + 1 < this.pointArray.Count)
									{
										this.pointArray[j + 1] = new Point(point.X, ((Point)this.pointArray[j + 1]).Y);
									}
									else
									{
										this.pointArray[0] = new Point(point.X, ((Point)this.pointArray[0]).Y);
									}
								}
							}
							else
							{
								switch (autoAdjustType)
								{
								case DrawPolyLine.AutoAdjustType.Compress:
									point.Y++;
									break;
								case DrawPolyLine.AutoAdjustType.CompressTopBottom:
									if (this.compressTop)
									{
										point.Y++;
									}
									break;
								case DrawPolyLine.AutoAdjustType.Expand:
									point.Y--;
									break;
								case DrawPolyLine.AutoAdjustType.ExpandTopBottom:
									if (this.expandTop)
									{
										point.Y--;
									}
									break;
								}
								this.pointArray[j] = new Point(((Point)this.pointArray[j]).X, point.Y);
								if (j + 1 < this.pointArray.Count)
								{
									this.pointArray[j + 1] = new Point(((Point)this.pointArray[j + 1]).X, point.Y);
								}
								else
								{
									this.pointArray[0] = new Point(((Point)this.pointArray[0]).X, point.Y);
								}
							}
						}
						else if (num5 != 180)
						{
							if (num5 == 270)
							{
								switch (autoAdjustType)
								{
								case DrawPolyLine.AutoAdjustType.Compress:
									point.X++;
									break;
								case DrawPolyLine.AutoAdjustType.CompressLeftRight:
									if (this.compressLeft)
									{
										point.X++;
									}
									break;
								case DrawPolyLine.AutoAdjustType.Expand:
									point.X--;
									break;
								case DrawPolyLine.AutoAdjustType.ExpandLeftRight:
									if (this.expandLeft)
									{
										point.X--;
									}
									break;
								}
								this.pointArray[j] = new Point(point.X, ((Point)this.pointArray[j]).Y);
								if (j + 1 < this.pointArray.Count)
								{
									this.pointArray[j + 1] = new Point(point.X, ((Point)this.pointArray[j + 1]).Y);
								}
								else
								{
									this.pointArray[0] = new Point(point.X, ((Point)this.pointArray[0]).Y);
								}
							}
						}
						else
						{
							switch (autoAdjustType)
							{
							case DrawPolyLine.AutoAdjustType.Compress:
								point.Y--;
								break;
							case DrawPolyLine.AutoAdjustType.CompressTopBottom:
								if (!this.compressTop)
								{
									point.Y--;
								}
								break;
							case DrawPolyLine.AutoAdjustType.Expand:
								point.Y++;
								break;
							case DrawPolyLine.AutoAdjustType.ExpandTopBottom:
								if (!this.expandTop)
								{
									point.Y++;
								}
								break;
							}
							this.pointArray[j] = new Point(((Point)this.pointArray[j]).X, point.Y);
							if (j + 1 < this.pointArray.Count)
							{
								this.pointArray[j + 1] = new Point(((Point)this.pointArray[j + 1]).X, point.Y);
							}
							else
							{
								this.pointArray[0] = new Point(((Point)this.pointArray[0]).X, point.Y);
							}
						}
					}
				}
			}
			if (autoAdjustType == DrawPolyLine.AutoAdjustType.CompressLeftRight)
			{
				this.compressLeft = !this.compressLeft;
			}
			if (autoAdjustType == DrawPolyLine.AutoAdjustType.CompressTopBottom)
			{
				this.compressTop = !this.compressTop;
			}
			if (autoAdjustType == DrawPolyLine.AutoAdjustType.ExpandLeftRight)
			{
				this.expandLeft = !this.expandLeft;
			}
			if (autoAdjustType == DrawPolyLine.AutoAdjustType.ExpandTopBottom)
			{
				this.expandTop = !this.expandTop;
			}
			if (!base.Filled && this.deductionArray.Count > 0)
			{
				for (int k = 0; k < this.pointArray.Count; k++)
				{
					Point[] array3 = new Point[]
					{
						array[k],
						array[k + 1]
					};
					DrawLine opening = this.GetOpening(array3[0], array3[1]);
					if (opening != null)
					{
						if (array3[0] == opening.StartPoint && array3[1] == opening.EndPoint)
						{
							opening.StartPoint = new Point(((Point)this.pointArray[k]).X, ((Point)this.pointArray[k]).Y);
							opening.EndPoint = new Point(((Point)this.pointArray[k + 1]).X, ((Point)this.pointArray[k + 1]).Y);
						}
						else
						{
							opening.EndPoint = new Point(((Point)this.pointArray[k]).X, ((Point)this.pointArray[k]).Y);
							opening.StartPoint = new Point(((Point)this.pointArray[k + 1]).X, ((Point)this.pointArray[k + 1]).Y);
						}
					}
				}
			}
			Console.WriteLine("DrawPolyLine::AutoAdjust EXIT");
			return true;
		}

		public CustomRenderingProperties GetCustomRenderingProperties(string extensionID)
		{
			if (this.customRenderingArray.ContainsKey(extensionID))
			{
				return this.customRenderingArray[extensionID];
			}
			CustomRenderingProperties customRenderingProperties = new CustomRenderingProperties();
			this.SetCustomRenderingProperties(extensionID, customRenderingProperties);
			return customRenderingProperties;
		}

		public void SetCustomRenderingAngle(string extensionID, int angle)
		{
			CustomRenderingProperties customRenderingProperties = this.GetCustomRenderingProperties(extensionID);
			customRenderingProperties.Angle = angle;
			this.SetCustomRenderingProperties(extensionID, customRenderingProperties);
		}

		public void SetCustomRenderingOffsetX(string extensionID, int offsetX)
		{
			CustomRenderingProperties customRenderingProperties = this.GetCustomRenderingProperties(extensionID);
			customRenderingProperties.OffsetX = offsetX;
			this.SetCustomRenderingProperties(extensionID, customRenderingProperties);
		}

		public void SetCustomRenderingOffsetY(string extensionID, int offsetY)
		{
			CustomRenderingProperties customRenderingProperties = this.GetCustomRenderingProperties(extensionID);
			customRenderingProperties.OffsetY = offsetY;
			this.SetCustomRenderingProperties(extensionID, customRenderingProperties);
		}

		public void SetCustomRenderingProperties(string extensionID, CustomRenderingProperties renderingProperties)
		{
			if (this.customRenderingArray.ContainsKey(extensionID))
			{
				this.customRenderingArray[extensionID].Angle = renderingProperties.Angle;
				this.customRenderingArray[extensionID].OffsetX = renderingProperties.OffsetX;
				this.customRenderingArray[extensionID].OffsetY = renderingProperties.OffsetY;
				return;
			}
			this.customRenderingArray.Add(extensionID, renderingProperties);
		}

		private CustomRendering SelectCurrentRendering()
		{
			if (base.Group.SelectedPreset != null && base.Group.SelectedPreset.CustomRendering != null)
			{
				base.Group.SelectedRenderingPreset = base.Group.SelectedPreset;
				return base.Group.SelectedPreset.CustomRendering;
			}
			if (base.Group.SelectedRenderingPreset != null && base.Group.SelectedRenderingPreset.CustomRendering != null)
			{
				return base.Group.SelectedRenderingPreset.CustomRendering;
			}
			foreach (object obj in base.Group.Presets.Collection)
			{
				Preset preset = (Preset)obj;
				if (preset.CustomRendering != null)
				{
					base.Group.SelectedRenderingPreset = preset;
					return preset.CustomRendering;
				}
			}
			base.Group.SelectedRenderingPreset = null;
			return null;
		}

		private void DrawCustomRendering(Graphics g, Region region, int offsetX, int offsetY)
		{
			DrawObjectGroup group = base.Group;
			if (group != null && group.Presets.Count >= 0)
			{
				CustomRendering customRendering = this.SelectCurrentRendering();
				if (customRendering != null)
				{
					Region clip = g.Clip;
					g.Clip = region;
					customRendering.Draw(g, region, offsetX, offsetY, this, this.GetCustomRenderingProperties(customRendering.Preset.ID), this.UnitScale);
					g.Clip = clip;
				}
			}
		}

		public void ComputeCustomRendering(Plan plan)
		{
			this.UnitScale = plan.UnitScale;
			Preset selectedPreset = base.Group.SelectedPreset;
			Preset selectedRenderingPreset = base.Group.SelectedRenderingPreset;
			foreach (object obj in base.Group.Presets.Collection)
			{
				Preset preset = (Preset)obj;
				if (preset.CustomRendering != null)
				{
					base.Group.SelectedPreset = preset;
					preset.CustomRendering.ComputeResults(null, this, this.GetCustomRenderingProperties(preset.ID), plan.UnitScale);
				}
			}
			base.Group.SelectedPreset = selectedPreset;
			base.Group.SelectedRenderingPreset = selectedRenderingPreset;
			this.UnitScale = null;
		}

		private Pen SelectPen()
		{
			Pen pen;
			if (base.DrawPen == null)
			{
				pen = new Pen(Color.FromArgb(base.Opacity + 30, base.Color), (float)base.PenWidth);
				pen.StartCap = LineCap.Square;
			}
			else
			{
				pen = (base.DrawPen.Clone() as Pen);
			}
			return pen;
		}

		private Brush SelectBrush()
		{
			Brush result;
			if (base.DeductionParentID == -1)
			{
				if (this.Pattern == HatchStylePickerCombo.HatchStylePickerEnum.Solid)
				{
					result = new SolidBrush(Color.FromArgb(base.Opacity, base.FillColor));
				}
				else
				{
					result = new HatchBrush((HatchStyle)this.Pattern, Color.FromArgb(base.Opacity, base.FillColor), Color.FromArgb(base.Opacity, Color.White));
				}
			}
			else
			{
				result = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(30, base.FillColor));
			}
			return result;
		}

		private GraphicsPath OutlinePath(int offsetX, int offsetY)
		{
			Point[] array = new Point[(this.pointArray.Count <= 1 || !this.FigureCompleted || !this.CloseFigure) ? this.pointArray.Count : (this.pointArray.Count + 1)];
			for (int i = 0; i < this.pointArray.Count; i++)
			{
				Point point = (Point)this.pointArray[i];
				array[i] = point;
				Point[] array2 = array;
				int num = i;
				array2[num].X = array2[num].X - offsetX;
				Point[] array3 = array;
				int num2 = i;
				array3[num2].Y = array3[num2].Y - offsetY;
				if (!this.ComputingCustomRendering && i + 1 < this.pointArray.Count)
				{
					DrawLine drawLine = new DrawLine(((Point)this.pointArray[i]).X, ((Point)this.pointArray[i]).Y, ((Point)this.pointArray[i + 1]).X, ((Point)this.pointArray[i + 1]).Y, new PointF(0f, 0f), "", 0, "");
					drawLine.SetSlopeFactor(this.SlopeFactor);
					int num3 = 0;
					int value = drawLine.Distance2D(drawLine.StartPoint.X, drawLine.StartPoint.Y, drawLine.EndPoint.X, drawLine.EndPoint.Y, !base.Filled, ref num3);
					if ((double)num3 * base.DrawingBoard.ZoomFactor >= (double)(this.toolTipMinThreshold + (base.DrawArea.UnitScaleIsImperial() ? 10 : 0)))
					{
						string text = base.DisplayInPixels ? (value.ToString() + " pixels") : base.DrawArea.ToLengthString(value, (double)num3 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold);
						if (this.IsOpening((Point)this.pointArray[i], (Point)this.pointArray[i + 1]))
						{
							double openingHeight = this.GetOpeningHeight((Point)this.pointArray[i], (Point)this.pointArray[i + 1]);
							if (openingHeight > 0.0)
							{
								string text2 = text;
								text = string.Concat(new string[]
								{
									text2,
									"\n",
									Resources.Hauteur_court,
									" ",
									base.DrawArea.ToLengthStringFromUnitSystem(openingHeight, (double)num3 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold)
								});
							}
						}
						DrawText value2 = new DrawText(text, drawLine.Center, base.TextAngle((Point)this.pointArray[i], (Point)this.pointArray[i + 1]), new Segment((Point)this.pointArray[i], (Point)this.pointArray[i + 1]));
						base.TextArray.Add(value2);
					}
				}
			}
			if (this.pointArray.Count > 1 && this.CloseFigure)
			{
				Point point2 = (Point)this.pointArray[0];
				if (!this.ComputingCustomRendering && this.pointArray.Count > 2)
				{
					DrawLine drawLine2 = new DrawLine(((Point)this.pointArray[this.pointArray.Count - 1]).X, ((Point)this.pointArray[this.pointArray.Count - 1]).Y, ((Point)this.pointArray[0]).X, ((Point)this.pointArray[0]).Y, new PointF(0f, 0f), "", 0, "");
					drawLine2.SetSlopeFactor(this.SlopeFactor);
					int num4 = 0;
					int value3 = drawLine2.Distance2D(drawLine2.StartPoint.X, drawLine2.StartPoint.Y, drawLine2.EndPoint.X, drawLine2.EndPoint.Y, !base.Filled, ref num4);
					if ((double)num4 * base.DrawingBoard.ZoomFactor >= (double)(this.toolTipMinThreshold + (base.DrawArea.UnitScaleIsImperial() ? 10 : 0)))
					{
						string text = base.DisplayInPixels ? (value3.ToString() + " pixels") : base.DrawArea.ToLengthString(value3, (double)num4 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold);
						if (this.IsOpening((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]))
						{
							double openingHeight2 = this.GetOpeningHeight((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]);
							if (openingHeight2 > 0.0)
							{
								string text3 = text;
								text = string.Concat(new string[]
								{
									text3,
									"\n",
									Resources.Hauteur_court,
									" ",
									base.DrawArea.ToLengthStringFromUnitSystem(openingHeight2, (double)num4 * base.DrawingBoard.ZoomFactor <= (double)this.toolTipMaxThreshold)
								});
							}
						}
						DrawText value4 = new DrawText(text, drawLine2.Center, base.TextAngle((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]), new Segment((Point)this.pointArray[this.pointArray.Count - 1], (Point)this.pointArray[0]));
						base.TextArray.Add(value4);
					}
				}
				if (this.FigureCompleted)
				{
					array[this.pointArray.Count] = point2;
					Point[] array4 = array;
					int count = this.pointArray.Count;
					array4[count].X = array4[count].X - offsetX;
					Point[] array5 = array;
					int count2 = this.pointArray.Count;
					array5[count2].Y = array5[count2].Y - offsetY;
				}
			}
			byte[] array6 = new byte[(this.pointArray.Count <= 1 || !this.FigureCompleted || !this.CloseFigure) ? this.pointArray.Count : (this.pointArray.Count + 1)];
			for (int j = 0; j < ((this.pointArray.Count <= 1 || !this.FigureCompleted || !this.CloseFigure) ? this.pointArray.Count : (this.pointArray.Count + 1)); j++)
			{
				array6[j] = 1;
			}
			return new GraphicsPath(array, array6);
		}

		private Region FilledRegion(Graphics g, Pen pen, GraphicsPath outlinePath, int offsetX, int offsetY)
		{
			Region region = new Region(outlinePath);
			if (this.deductionArray.Count > 0)
			{
				foreach (object obj in this.deductionArray)
				{
					DrawPolyLine drawPolyLine = (DrawPolyLine)obj;
					if (drawPolyLine.PointArray.Count > 0)
					{
						Point[] array = new Point[drawPolyLine.PointArray.Count];
						for (int i = 0; i < drawPolyLine.PointArray.Count; i++)
						{
							array[i] = (Point)drawPolyLine.PointArray[i];
							Point[] array2 = array;
							int num = i;
							array2[num].X = array2[num].X - offsetX;
							Point[] array3 = array;
							int num2 = i;
							array3[num2].Y = array3[num2].Y - offsetY;
						}
						GraphicsPath graphicsPath = new GraphicsPath();
						graphicsPath.AddLines(array);
						graphicsPath.CloseFigure();
						region.Exclude(graphicsPath);
						if (g != null && pen != null)
						{
							g.DrawPath(pen, graphicsPath);
						}
						graphicsPath.Dispose();
					}
				}
			}
			return region;
		}

		private void DrawOpeningOrSegment(Graphics g, Pen pen, int offsetX, int offsetY)
		{
			for (int i = 0; i < this.pointArray.Count - (this.closeFigure ? 0 : 1); i++)
			{
				Point[] array = new Point[2];
				if (this.closeFigure && i + 1 == this.pointArray.Count)
				{
					array[0] = (Point)this.pointArray[this.pointArray.Count - 1];
					array[1] = (Point)this.pointArray[0];
				}
				else
				{
					array[0] = (Point)this.pointArray[i];
					array[1] = (Point)this.pointArray[i + 1];
				}
				bool flag = this.IsOpening(array[0], array[1]);
				if (!this.selectedSegment.IsEqualTo(array[0], array[1]))
				{
					if (flag)
					{
						Pen pen2 = new Pen(Color.FromArgb((base.Opacity > 50) ? 50 : base.Opacity, base.Color), (float)base.PenWidth);
						pen2.DashStyle = DashStyle.Dot;
						g.DrawLine(pen2, new Point(array[0].X - offsetX, array[0].Y - offsetY), new Point(array[1].X - offsetX, array[1].Y - offsetY));
						pen2.Dispose();
					}
					else
					{
						g.DrawLine(pen, new Point(array[0].X - offsetX, array[0].Y - offsetY), new Point(array[1].X - offsetX, array[1].Y - offsetY));
					}
				}
				else
				{
					Pen pen3 = new Pen(Color.FromArgb(base.Opacity, Color.Black), (float)base.PenWidth);
					pen3.DashStyle = (flag ? DashStyle.Dot : DashStyle.DashDot);
					g.DrawLine(pen3, new Point(array[0].X - offsetX, array[0].Y - offsetY), new Point(array[1].X - offsetX, array[1].Y - offsetY));
					pen3.Dispose();
				}
			}
		}

		private void DrawDrops(Graphics g, int offsetX, int offsetY)
		{
			foreach (object obj in this.dropArray)
			{
				DrawLine drawLine = (DrawLine)obj;
				Pen pen = new Pen(Color.White, 1f);
				g.DrawEllipse(pen, drawLine.StartPoint.X - offsetX - 10, drawLine.StartPoint.Y - offsetY - 10, 20, 20);
				g.FillEllipse(new SolidBrush(Color.FromArgb(base.Opacity + 30, 33, 33, 33)), drawLine.StartPoint.X - offsetX - 10, drawLine.StartPoint.Y - offsetY - 10, 20, 20);
				g.DrawLine(pen, drawLine.StartPoint.X - offsetX, drawLine.StartPoint.Y - offsetY - 4, drawLine.StartPoint.X - offsetX, drawLine.StartPoint.Y - offsetY + 4);
				g.DrawLine(pen, drawLine.StartPoint.X - offsetX - 4, drawLine.StartPoint.Y - offsetY - 4, drawLine.StartPoint.X - offsetX + 4, drawLine.StartPoint.Y - offsetY - 4);
				g.DrawLine(pen, drawLine.StartPoint.X - offsetX - 4, drawLine.StartPoint.Y - offsetY + 4, drawLine.StartPoint.X - offsetX + 4, drawLine.StartPoint.Y - offsetY + 4);
				pen.Dispose();
				Point empty = Point.Empty;
				Point empty2 = Point.Empty;
				double num = double.PositiveInfinity;
				for (int i = 0; i < this.pointArray.Count - (this.closeFigure ? 0 : 1); i++)
				{
					Point[] array = new Point[2];
					if (this.closeFigure && i + 1 == this.pointArray.Count)
					{
						array[0] = (Point)this.pointArray[this.pointArray.Count - 1];
						array[1] = (Point)this.pointArray[0];
					}
					else
					{
						array[0] = (Point)this.pointArray[i];
						array[1] = (Point)this.pointArray[i + 1];
					}
					PointF pointF;
					double num2 = DrawLine.FindDistanceToSegment(drawLine.StartPoint, array[0], array[1], out pointF);
					if (num2 < num)
					{
						num = num2;
						empty = new Point(array[0].X, array[0].Y);
						empty2 = new Point(array[1].X, array[1].Y);
					}
				}
				if (empty != Point.Empty && empty2 != Point.Empty)
				{
					string text = base.DrawArea.ToLengthStringFromUnitSystem(drawLine.Height, true);
					DrawText value = new DrawText(text, drawLine.StartPoint, base.TextAngle(empty, empty2), new Segment(empty, empty2));
					base.TextArray.Add(value);
				}
			}
		}

		private void DrawAngleMarkers(Graphics g, int offsetX, int offsetY)
		{
			if (this.pointArray.Count > 2)
			{
				for (int i = 0; i < this.pointArray.Count; i++)
				{
					Point[] array = new Point[3];
					if (i == 0)
					{
						array[0] = (Point)this.pointArray[this.pointArray.Count - 1];
						array[1] = (Point)this.pointArray[0];
						array[2] = (Point)this.pointArray[1];
					}
					else if (i == this.pointArray.Count - 1)
					{
						array[0] = (Point)this.pointArray[this.pointArray.Count - 2];
						array[1] = (Point)this.pointArray[this.pointArray.Count - 1];
						array[2] = (Point)this.pointArray[0];
					}
					else
					{
						array[0] = (Point)this.pointArray[i - 1];
						array[1] = (Point)this.pointArray[i];
						array[2] = (Point)this.pointArray[i + 1];
					}
					double num = this.Angle(array[0], array[1], array[2]);
					if (num >= 89.5 && num <= 90.5)
					{
						int num2 = 50;
						int num3 = 4;
						DrawLine drawLine = new DrawLine();
						array[0] = new Point(array[0].X - offsetX, array[0].Y - offsetY);
						array[1] = new Point(array[1].X - offsetX, array[1].Y - offsetY);
						array[2] = new Point(array[2].X - offsetX, array[2].Y - offsetY);
						if (drawLine.Distance2D(array[0], array[1]) > num2 + 10 && drawLine.Distance2D(array[2], array[1]) > num2 + 10)
						{
							Point[] array2 = new Point[3];
							double[] array3 = new double[]
							{
								DrawLine.Angle((double)array[1].X, (double)array[1].Y, (double)array[0].X, (double)array[0].Y),
								DrawLine.Angle((double)array[1].X, (double)array[1].Y, (double)array[2].X, (double)array[2].Y)
							};
							array2[1] = new Point(array[1].X, array[1].Y);
							array2[0] = DrawLine.GetPointAtAngle(array2[1], array3[0], (double)((float)num2 * 0.8f));
							array2[2] = DrawLine.GetPointAtAngle(array2[1], array3[1], (double)((float)num2 * 0.8f));
							array[0] = DrawLine.GetPointAtAngle(array[1], array3[0], (double)num2);
							array[2] = DrawLine.GetPointAtAngle(array[1], array3[1], (double)num2);
							Pen pen = new Pen(Color.FromArgb(255, (base.Color == Color.Red) ? Color.DarkRed : Color.Red), (float)num3);
							pen.DashStyle = DashStyle.Solid;
							pen.EndCap = LineCap.Square;
							pen.Alignment = PenAlignment.Center;
							g.DrawLine(pen, array[0], array[1]);
							g.DrawLine(pen, array[2], array[1]);
							g.FillPolygon(new SolidBrush(Color.FromArgb(100, pen.Color)), array2);
							pen.Dispose();
						}
					}
				}
			}
		}

		private void ComputeCenterTooltip()
		{
			if (this.pointArray.Count > 2)
			{
				GroupStats stats = this.Stats;
				string text = string.Concat(new string[]
				{
					Resources.Surface,
					" = ",
					base.DisplayInPixels ? (stats.AreaMinusDeduction.ToString() + " pixels ²") : base.DrawArea.ToAreaString((int)stats.AreaMinusDeduction),
					"\n",
					Resources.Périmètre,
					" = ",
					base.DisplayInPixels ? (stats.Perimeter.ToString() + " pixels") : base.DrawArea.ToLengthString((int)stats.Perimeter, false)
				});
				text += ((this.customRenderingTooltip != string.Empty) ? ("\n" + this.customRenderingTooltip) : string.Empty);
				DrawText value = new DrawText(text, this.Center, 0);
				base.TextArray.Insert(0, value);
				return;
			}
			base.TextArray.Insert(0, new DrawText("", Point.Empty, 0));
		}

		public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = (printToScreen ? ((imageQuality == MainForm.ImageQualityEnum.QualityHigh) ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel) : TextRenderingHint.ClearTypeGridFit);
			Pen pen = this.SelectPen();
			Brush brush = this.SelectBrush();
			string empty = string.Empty;
			base.ClearTextArray();
			GraphicsPath graphicsPath = this.OutlinePath(offsetX, offsetY);
			if (base.Rotation != 0)
			{
				RectangleF bounds = graphicsPath.GetBounds();
				Matrix matrix = new Matrix();
				matrix.RotateAt((float)base.Rotation, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
				graphicsPath.Transform(matrix);
			}
			Region region = null;
			if (base.Filled)
			{
				region = this.FilledRegion(g, pen, graphicsPath, offsetX, offsetY);
				g.FillRegion(brush, region);
			}
			if (!this.FigureCompleted || (this.FigureCompleted && !this.selectedSegment.IsValid() && this.deductionArray.Count == 0))
			{
				g.DrawPath(pen, graphicsPath);
			}
			else
			{
				this.DrawOpeningOrSegment(g, pen, offsetX, offsetY);
			}
			if (this.DropArray.Count > 0)
			{
				this.DrawDrops(g, offsetX, offsetY);
			}
			if (this.Equals(base.DrawArea.CurrentlyCreatedObject) || this.Equals(base.DrawArea.CurrentlyResizedObject))
			{
				this.DrawAngleMarkers(g, offsetX, offsetY);
			}
			if (region != null)
			{
				if (this.UnitScale.Scale > 0f && !base.IsDeduction())
				{
					this.DrawCustomRendering(g, region, offsetX, offsetY);
				}
				region.Dispose();
			}
			if (base.Filled)
			{
				this.ComputeCenterTooltip();
			}
			graphicsPath.Dispose();
			if (pen != null)
			{
				pen.Dispose();
			}
			brush.Dispose();
			base.Invalidate();
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		public override void DrawText(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh, float defaultFontSize = 12f)
		{
			if (!base.ShowMeasure || base.TextArray.Count == 0 || this.EditDeductions)
			{
				return;
			}
			double num = this.ComputeArea(false);
			int num2 = (base.DrawingBoard.ZoomFactor < 0.15) ? (base.Filled ? ((base.DrawingBoard.ZoomFactor >= 0.12) ? 1 : 0) : 0) : ((this.Selected || !base.Filled) ? base.TextArray.Count : 1);
			for (int i = 0; i < num2; i++)
			{
				DrawText drawText = (DrawText)base.TextArray[i];
				if ((i == 0 && (num * base.DrawingBoard.ZoomFactor >= 30000.0 || this.figureCompleted)) || (i > 0 && (num * base.DrawingBoard.ZoomFactor >= 30000.0 || !this.figureCompleted || !base.Filled)) || !this.closeFigure)
				{
					bool flag = (i == 0 && base.Filled) || (!this.selectedSegment.IsValid() && !base.Filled);
					if (!flag)
					{
						Segment segment = (Segment)drawText.Tag;
						flag = !this.selectedSegment.IsEqualTo(segment.StartPoint, segment.EndPoint);
					}
					if (flag)
					{
						drawText.Draw(g, offsetX, offsetY, (float)base.DrawingBoard.ZoomFactor, (!base.IsDeduction()) ? base.Opacity : 225, printToScreen, imageQuality, defaultFontSize);
					}
				}
			}
			base.TextDirty = false;
		}

		public void AddPoint(Point point)
		{
			this.pointArray.Add(point);
		}

		public void InsertPoint(Point point, int index)
		{
			this.pointArray.Insert(index, point);
		}

		public void RemovePoint(int index)
		{
			this.pointArray.RemoveAt(index);
		}

		public void RemovePoint(Point point)
		{
			if (!base.Filled && this.deductionArray.Count > 0)
			{
				for (int i = this.deductionArray.Count - 1; i >= 0; i--)
				{
					DrawLine drawLine = (DrawLine)this.deductionArray[i];
					if (drawLine.StartPoint.Equals(point) || drawLine.EndPoint.Equals(point))
					{
						this.deductionArray.RemoveAt(i);
					}
				}
			}
			this.pointArray.Remove(point);
			this.ResetDrops();
		}

		public override int ActiveHandleCount
		{
			get
			{
				if (!this.FigureCompleted)
				{
					return this.pointArray.Count - 1;
				}
				return this.pointArray.Count;
			}
		}

		public override int HandleCount
		{
			get
			{
				return this.pointArray.Count;
			}
		}

		public override int GetHandleSize()
		{
			return 4;
		}

		public override Point GetHandle(int handleNumber)
		{
			if (handleNumber < 1)
			{
				handleNumber = 1;
			}
			if (handleNumber > this.pointArray.Count)
			{
				handleNumber = this.pointArray.Count;
			}
			return (Point)this.pointArray[handleNumber - 1];
		}

		public override Cursor GetHandleCursor(int handleNumber)
		{
			return Cursors.SizeAll;
		}

		public override void MoveHandleTo(Point point, int handleNumber)
		{
			if (handleNumber < 1)
			{
				handleNumber = 1;
			}
			if (handleNumber > this.pointArray.Count)
			{
				handleNumber = this.pointArray.Count;
			}
			Point left = (Point)this.pointArray[handleNumber - 1];
			Point point2 = new Point(point.X, point.Y);
			point2.X += (int)base.DrawingBoard.Origin.X;
			point2.Y += (int)base.DrawingBoard.Origin.Y;
			this.pointArray[handleNumber - 1] = point2;
			if (!base.Filled && this.deductionArray.Count > 0)
			{
				foreach (object obj in this.deductionArray)
				{
					DrawLine drawLine = (DrawLine)obj;
					if (left == drawLine.StartPoint)
					{
						drawLine.StartPoint = new Point(point2.X, point2.Y);
					}
					else if (left == drawLine.EndPoint)
					{
						drawLine.EndPoint = new Point(point2.X, point2.Y);
					}
				}
			}
			this.ResetDrops();
			base.Dirty = true;
			base.Invalidate();
		}

		public override void Move(int deltaX, int deltaY)
		{
			int count = this.pointArray.Count;
			for (int i = 0; i < count; i++)
			{
				Point point = new Point(((Point)this.pointArray[i]).X + deltaX, ((Point)this.pointArray[i]).Y + deltaY);
				this.pointArray[i] = point;
			}
			foreach (object obj in this.deductionArray)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (base.Filled)
				{
					DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
					for (int j = 0; j < drawPolyLine.PointArray.Count; j++)
					{
						Point point2 = new Point(((Point)drawPolyLine.PointArray[j]).X + deltaX, ((Point)drawPolyLine.PointArray[j]).Y + deltaY);
						drawPolyLine.PointArray[j] = point2;
					}
				}
				else
				{
					DrawLine drawLine = (DrawLine)drawObject;
					drawLine.StartPoint = new Point(drawLine.StartPoint.X + deltaX, drawLine.StartPoint.Y + deltaY);
					drawLine.EndPoint = new Point(drawLine.EndPoint.X + deltaX, drawLine.EndPoint.Y + deltaY);
				}
			}
			foreach (object obj2 in this.dropArray)
			{
				DrawLine drawLine2 = (DrawLine)obj2;
				drawLine2.StartPoint = new Point(drawLine2.StartPoint.X + deltaX, drawLine2.StartPoint.Y + deltaY);
			}
			base.Dirty = true;
			base.Invalidate();
		}

		public int SegmentHitTest(Point point, int offsetX, int offsetY)
		{
			if (this.IsPartOfDrop(point, offsetX, offsetY) != Point.Empty)
			{
				return -4;
			}
			int num = this.HitTest(point, offsetX, offsetY);
			if (num != 0)
			{
				return num;
			}
			if (!this.PointIsOnOutline)
			{
				return 0;
			}
			this.SetSelectedSegment(new Point(point.X + offsetX, point.Y + offsetY));
			if (!this.SelectedSegment.IsValid())
			{
				return 0;
			}
			if (this.IsOpening(this.SelectedSegment.StartPoint, this.SelectedSegment.EndPoint))
			{
				return -3;
			}
			return -2;
		}

		public void SetSelectedSegment(Point point)
		{
			int nearestPointIndex = this.GetNearestPointIndex(point);
			if (nearestPointIndex > -1)
			{
				this.SelectedSegment.StartPoint = (Point)this.PointArray[nearestPointIndex];
				this.SelectedSegment.EndPoint = (Point)this.PointArray[(nearestPointIndex + 1 == this.PointArray.Count) ? 0 : (nearestPointIndex + 1)];
			}
		}

		public override void Normalize()
		{
		}

		private bool IntersectWithLine(Rectangle rectangle, int offsetX, int offsetY)
		{
			for (int i = 0; i < this.PointArray.Count - 1; i++)
			{
				if (DrawLine.LineIntersectsRectangle(new Rectangle(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height), (Point)this.PointArray[i], (Point)this.PointArray[i + 1]))
				{
					return true;
				}
			}
			return this.CloseFigure && DrawLine.LineIntersectsRectangle(new Rectangle(rectangle.X + offsetX, rectangle.Y + offsetY, rectangle.Width, rectangle.Height), (Point)this.PointArray[this.PointArray.Count - 1], (Point)this.PointArray[0]);
		}

		public override bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
		{
			return this.IntersectWithLine(rectangle, offsetX, offsetY) || (base.Filled && this.PointInPolygon(new Point(rectangle.X + offsetX, rectangle.Y + offsetY)));
		}

		public bool PointInPolygon(Point p)
		{
			bool flag = false;
			if (this.PointArray.Count < 3)
			{
				return flag;
			}
			Point point = new Point(((Point)this.PointArray[this.PointArray.Count - 1]).X, ((Point)this.PointArray[this.PointArray.Count - 1]).Y);
			foreach (object obj in this.PointArray)
			{
				Point point2 = (Point)obj;
				Point point3 = new Point(point2.X, point2.Y);
				Point point4;
				Point point5;
				if (point3.X > point.X)
				{
					point4 = point;
					point5 = point3;
				}
				else
				{
					point4 = point3;
					point5 = point;
				}
				if (point3.X < p.X == p.X <= point.X && ((long)p.Y - (long)point4.Y) * (long)(point5.X - point4.X) < ((long)point5.Y - (long)point4.Y) * (long)(p.X - point4.X))
				{
					flag = !flag;
				}
				point = point3;
			}
			return flag;
		}

		public bool MatchSegment(Segment segment, int tolerance)
		{
			Point[] array = new Point[2];
			for (int i = 0; i < this.pointArray.Count - 1; i++)
			{
				array[0] = (Point)this.pointArray[i];
				array[1] = (Point)this.pointArray[i + 1];
				if ((Math.Abs(segment.StartPoint.X - array[0].X) <= tolerance && Math.Abs(segment.StartPoint.Y - array[0].Y) <= tolerance && Math.Abs(segment.EndPoint.X - array[1].X) <= tolerance && Math.Abs(segment.EndPoint.Y - array[1].Y) <= tolerance) || (Math.Abs(segment.StartPoint.X - array[1].X) <= tolerance && Math.Abs(segment.StartPoint.Y - array[1].Y) <= tolerance && Math.Abs(segment.EndPoint.X - array[0].X) <= tolerance && Math.Abs(segment.EndPoint.Y - array[0].Y) <= tolerance))
				{
					return true;
				}
			}
			if (this.CloseFigure)
			{
				array[0] = (Point)this.pointArray[this.pointArray.Count - 1];
				array[1] = (Point)this.pointArray[0];
				if ((Math.Abs(segment.StartPoint.X - array[0].X) <= tolerance && Math.Abs(segment.StartPoint.Y - array[0].Y) <= tolerance && Math.Abs(segment.EndPoint.X - array[1].X) <= tolerance && Math.Abs(segment.EndPoint.Y - array[1].Y) <= tolerance) || (Math.Abs(segment.StartPoint.X - array[1].X) <= tolerance && Math.Abs(segment.StartPoint.Y - array[1].Y) <= tolerance && Math.Abs(segment.EndPoint.X - array[0].X) <= tolerance && Math.Abs(segment.EndPoint.Y - array[0].Y) <= tolerance))
				{
					return true;
				}
			}
			return false;
		}

		public bool PointInOutline(Point point, int offsetX, int offsetY)
		{
			for (int i = 0; i < this.PointArray.Count - 1; i++)
			{
				double num = DrawLine.FindDistanceToSegment(new Point(point.X + offsetX, point.Y + offsetY), (Point)this.PointArray[i], (Point)this.PointArray[i + 1]);
				if (num >= 0.0 && num <= (double)(base.PenWidth / 2))
				{
					return true;
				}
			}
			if (this.CloseFigure)
			{
				double num2 = DrawLine.FindDistanceToSegment(new Point(point.X + offsetX, point.Y + offsetY), (Point)this.PointArray[this.PointArray.Count - 1], (Point)this.PointArray[0]);
				if (num2 >= 0.0 && num2 <= (double)(base.PenWidth / 2))
				{
					return true;
				}
			}
			return false;
		}

		protected override bool PointInObject(Point point, int offsetX, int offsetY)
		{
			this.pointIsOnOutline = this.PointInOutline(point, offsetX, offsetY);
			if (this.pointIsOnOutline)
			{
				this.pointInObject = true;
			}
			else
			{
				this.pointInObject = (base.Filled && this.PointInPolygon(new Point(point.X + offsetX, point.Y + offsetY)));
			}
			return this.pointInObject;
		}

		public override void Scale(float scaleX, float scaleY)
		{
			int count = this.pointArray.Count;
			for (int i = 0; i < count; i++)
			{
				Point point = new Point((int)((float)((Point)this.pointArray[i]).X * scaleX), (int)((float)((Point)this.pointArray[i]).Y * scaleY));
				this.pointArray[i] = point;
			}
			foreach (object obj in this.deductionArray)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (base.Filled)
				{
					DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
					for (int j = 0; j < drawPolyLine.PointArray.Count; j++)
					{
						Point point2 = new Point((int)((float)((Point)drawPolyLine.PointArray[j]).X * scaleX), (int)((float)((Point)drawPolyLine.PointArray[j]).Y * scaleY));
						drawPolyLine.PointArray[j] = point2;
					}
				}
				else
				{
					DrawLine drawLine = (DrawLine)drawObject;
					drawLine.StartPoint = new Point((int)((float)drawLine.StartPoint.X * scaleX), (int)((float)drawLine.StartPoint.Y * scaleY));
					drawLine.EndPoint = new Point((int)((float)drawLine.EndPoint.X * scaleX), (int)((float)drawLine.EndPoint.Y * scaleY));
				}
			}
		}

		[CompilerGenerated]
		private static int <DetermineWindingDirection>b__a(Point p)
		{
			return p.X;
		}

		[CompilerGenerated]
		private static int <DetermineWindingDirection>b__b(Point p)
		{
			return p.Y;
		}

		private ArrayList pointArray;

		private ArrayList deductionArray;

		private ArrayList dropArray;

		private Dictionary<string, CustomRenderingProperties> customRenderingArray = new Dictionary<string, CustomRenderingProperties>();

		private HatchStylePickerCombo.HatchStylePickerEnum pattern = HatchStylePickerCombo.HatchStylePickerEnum.Solid;

		private Segment selectedSegment = new Segment();

		private bool closeFigure;

		private bool figureCompleted;

		private bool editDeductions;

		private bool pointInObject;

		private bool pointIsOnOutline;

		private bool compressLeft;

		private bool compressTop;

		private bool expandLeft;

		private bool expandTop;

		private int toolTipMinThreshold = 50;

		private int toolTipMaxThreshold = 100;

		private string customRenderingTooltip = string.Empty;

		private UnitScale unitScale;

		[CompilerGenerated]
		private static Func<Point, int> CS$<>9__CachedAnonymousMethodDelegated;

		[CompilerGenerated]
		private static Func<Point, int> CS$<>9__CachedAnonymousMethodDelegatee;

		public enum AutoAdjustType
		{
			ToZone,
			Compress,
			CompressTopBottom,
			CompressLeftRight,
			Expand,
			ExpandTopBottom,
			ExpandLeftRight
		}

		public enum HitTestEnum
		{
			NoHit = -1,
			HitOnSegment = -2,
			HitOnOpening = -3,
			HitOnDrop = -4,
			HitAnywhere = 0
		}

		public enum WindingDirection
		{
			Clockwise,
			CounterClockWise
		}

		private class ValueCount
		{
			public ValueCount(int value, int otherValue, int offset, int count)
			{
				this.value = value;
				this.otherValue = otherValue;
				this.offset = offset;
				this.count = count;
			}

			public int value;

			public int offset;

			public int otherValue;

			public int count;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClassf
		{
			public <>c__DisplayClassf()
			{
			}

			public PointF <DetermineWindingDirection>b__c(Point point)
			{
				PointF result = new PointF((float)point.X - this.pointInPolygon.X, (float)point.Y - this.pointInPolygon.Y);
				return result;
			}

			public PointF pointInPolygon;
		}
	}
}
