using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal abstract class ToolObject : Tool
	{
		protected Cursor Cursor
		{
			get
			{
				return this.cursor;
			}
			set
			{
				this.cursor = value;
			}
		}

		protected bool Tracking
		{
			[CompilerGenerated]
			get
			{
				return this.<Tracking>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tracking>k__BackingField = value;
			}
		}

		private void InsertObject(DrawObject drawObject, int activeLayer, bool bringToFront, bool insertDeductionToList = false)
		{
			this.InsertObject(drawObject, base.DrawArea.GetNextObjectID(), activeLayer, bringToFront, insertDeductionToList);
		}

		public void InsertObject(DrawObject drawObject, int objectID, int activeLayer, bool bringToFront, bool insertDeductionToList = false)
		{
			drawObject.ID = objectID;
			drawObject.DrawArea = base.DrawArea;
			if (drawObject.Name == "")
			{
				drawObject.Name = base.DrawArea.GetFreeObjectName(Resources.Untitled);
			}
			if (!drawObject.IsDeduction() || insertDeductionToList)
			{
				if (bringToFront)
				{
					base.DrawArea.Layers[activeLayer].DrawingObjects.Insert(drawObject, 0);
					return;
				}
				base.DrawArea.Layers[activeLayer].DrawingObjects.Add(drawObject);
			}
		}

		protected void AddNewObject(DrawObject drawObject)
		{
			base.DrawArea.ActiveDrawingObjects.UnselectAll();
			drawObject.Selected = true;
			drawObject.Dirty = true;
			DrawObject drawObject2 = base.DrawArea.FindObjectFromGroupID(base.DrawArea.ActivePlan, drawObject.GroupID);
			if (drawObject2 != null)
			{
				drawObject.MeasureWasInvisible = drawObject2.MeasureWasInvisible;
			}
			else
			{
				drawObject.MeasureWasInvisible = false;
			}
			this.InsertObject(drawObject, base.DrawArea.ActiveLayerIndex, true, true);
			base.DrawArea.Capture = true;
			base.DrawArea.Refresh();
		}

		public Point SnapPoint(Point startPoint, Point endPoint, Func<Point, Point, int> getDistance, int angle, int degreeThresold)
		{
			if (angle >= 360 - degreeThresold || angle <= degreeThresold || (angle >= 180 - degreeThresold && angle <= 180 + degreeThresold))
			{
				return new Point(endPoint.X, startPoint.Y);
			}
			if ((angle >= 90 - degreeThresold && angle <= 90 + degreeThresold) || (angle >= 270 - degreeThresold && angle <= 270 + degreeThresold))
			{
				return new Point(startPoint.X, endPoint.Y);
			}
			int num = getDistance(startPoint, endPoint);
			if (angle >= 45 - degreeThresold && angle <= 45 + degreeThresold)
			{
				return new Point((int)((double)startPoint.X + (double)num * Math.Cos(Utilities.DegreeToRadian(45.0))), (int)((double)startPoint.Y + (double)num * Math.Sin(Utilities.DegreeToRadian(45.0))));
			}
			if (angle >= 135 - degreeThresold && angle <= 135 + degreeThresold)
			{
				return new Point((int)((double)startPoint.X - (double)num * Math.Cos(Utilities.DegreeToRadian(45.0))), (int)((double)startPoint.Y + (double)num * Math.Sin(Utilities.DegreeToRadian(45.0))));
			}
			if (angle >= 225 - degreeThresold && angle <= 225 + degreeThresold)
			{
				return new Point((int)((double)startPoint.X - (double)num * Math.Cos(Utilities.DegreeToRadian(45.0))), (int)((double)startPoint.Y - (double)num * Math.Sin(Utilities.DegreeToRadian(45.0))));
			}
			if (angle >= 315 - degreeThresold && angle <= 315 + degreeThresold)
			{
				return new Point((int)((double)startPoint.X + (double)num * Math.Cos(Utilities.DegreeToRadian(45.0))), (int)((double)startPoint.Y - (double)num * Math.Sin(Utilities.DegreeToRadian(45.0))));
			}
			return endPoint;
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
			{
				return;
			}
			Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
			this.lastPoint = new Point(point.X, point.Y);
			this.lastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.TrackPanning(new Point(e.X, e.Y));
			}
		}

		private void TrackPanning(Point location)
		{
			if (this.Tracking)
			{
				return;
			}
			this.Tracking = true;
			Point point = base.DrawArea.BackTrackMouse(location);
			Point p = new Point(this.lastOrigin.X, this.lastOrigin.Y);
			p.X += this.lastPoint.X - point.X;
			p.Y += this.lastPoint.Y - point.Y;
			if (!p.Equals(this.lastOrigin))
			{
				base.DrawArea.PanningInProgress = true;
				base.DrawArea.DrawingBoard.Origin = p;
				base.DrawArea.Refresh();
			}
			this.lastPoint = new Point(point.X, point.Y);
			this.lastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
			this.Tracking = false;
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && !base.DrawArea.DrawingInProgress && !base.DrawArea.PanningInProgress)
			{
				base.DrawArea.OnContextMenu(e);
			}
			base.DrawArea.PanningInProgress = false;
		}

		protected void Move(DrawObject drawObject, int deltaX, int deltaY)
		{
			base.DrawArea.DrawingBoard.Invalidate(drawObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
			drawObject.Move(deltaX, deltaY);
			base.DrawArea.DrawingBoard.Invalidate(drawObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
			base.DrawArea.DrawingBoard.Update();
		}

		protected void MoveHandleTo(DrawObject drawObject, Point point, int handleNumber)
		{
			base.DrawArea.DrawingBoard.Invalidate(drawObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
			drawObject.MoveHandleTo(point, handleNumber);
			base.DrawArea.DrawingBoard.Invalidate(drawObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
			base.DrawArea.DrawingBoard.Update();
		}

		protected void StartDrawing(DrawObject drawObject)
		{
			base.DrawArea.DrawingInProgress = true;
			base.DrawArea.CurrentlyCreatedObject = drawObject;
		}

		protected void StopDrawing()
		{
			base.DrawArea.DrawingInProgress = false;
			base.DrawArea.CursorRestricted = false;
			base.DrawArea.CurrentlyCreatedObject = null;
			base.DrawArea.UpdateStatusBar(string.Empty);
		}

		protected void CancelDrawing()
		{
			if (!base.DrawArea.DrawingInProgress)
			{
				return;
			}
			try
			{
				base.DrawArea.Layers[base.DrawArea.Layers.ActiveLayerIndex].DrawingObjects.RemoveAt(0);
				base.DrawArea.Refresh();
			}
			catch
			{
			}
		}

		protected ToolObject()
		{
		}

		private Point lastPoint;

		private Point lastOrigin;

		private Cursor cursor;

		[CompilerGenerated]
		private bool <Tracking>k__BackingField;
	}
}
