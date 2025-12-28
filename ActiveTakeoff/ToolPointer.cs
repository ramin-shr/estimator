using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
	internal class ToolPointer : Tool
	{
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

		public bool ZoomSelection
		{
			[CompilerGenerated]
			private get
			{
				return this.<ZoomSelection>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ZoomSelection>k__BackingField = value;
			}
		}

		public ToolPointer()
		{
			this.selectMode = ToolPointer.SelectionMode.None;
			this.lastPoint = Point.Empty;
			this.lastOrigin = Point.Empty;
			this.startPoint = Point.Empty;
			this.lastRectangle = default(Rectangle);
			this.hoverSegment = new Segment();
			this.polyLineCursor = new Cursor(base.GetType(), "Pencil.cur");
		}

		public override void LoadCursor(Cursor cursor)
		{
			base.DrawArea.Cursor = cursor;
		}

		private void CancelTool()
		{
			if (base.DrawArea.DeductionParent != null)
			{
				base.DrawArea.DeductionParent.Selected = true;
				base.DrawArea.DeductionParentRelease();
				base.DrawArea.Refresh();
			}
		}

		private void InitializeVariables()
		{
			this.moving = false;
			this.orthoEnable = false;
			this.resizedObject = null;
			this.resizedObjectHandle = 0;
			this.commandChangeState = null;
			this.hoverSegment.Reset();
			this.selectMode = ToolPointer.SelectionMode.None;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
			this.InitializeVariables();
		}

		public override void ReleaseTool()
		{
			this.ZoomSelection = false;
			base.DrawArea.PointerInProgress = false;
			base.DrawArea.CurrentlyResizedObject = null;
			base.DrawArea.NetSelectionInProgress = false;
			base.DrawArea.Cursor = Cursors.Default;
			if (base.DrawArea.DeductionParent == null)
			{
				base.DrawArea.CursorRestricted = false;
			}
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			this.InitializeVariables();
			Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
			this.lastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
			if (e.Button == MouseButtons.Left)
			{
				if (!this.ZoomSelection)
				{
					this.TestForResize(point);
					if (this.selectMode == ToolPointer.SelectionMode.None)
					{
						this.TestForMove(point);
					}
					if (this.selectMode == ToolPointer.SelectionMode.None)
					{
						this.ClickOnBackground();
					}
				}
				else
				{
					this.SetNetSelection();
				}
			}
			this.lastPoint = new Point(point.X, point.Y);
			this.startPoint = new Point(point.X, point.Y);
			this.lastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
			this.lastRectangle = DrawRectangle.GetNormalizedRectangle(this.startPoint, this.lastPoint);
			base.DrawArea.NetRectangle = this.lastRectangle;
			base.DrawArea.Capture = true;
			base.DrawArea.Refresh();
		}

		private void TestForOrtho(DrawObject drawObject, int handleNumber)
		{
			if (drawObject.ObjectType != "Area" && drawObject.ObjectType != "Perimeter")
			{
				return;
			}
			DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
			if (drawPolyLine.PointArray.Count > 2)
			{
				bool flag = false;
				int num = handleNumber - 1;
				Point point = (Point)drawPolyLine.PointArray[num];
				Point[] array = new Point[2];
				if (num == 0 || num == drawPolyLine.PointArray.Count - 1)
				{
					if (drawPolyLine.CloseFigure)
					{
						array[0] = (Point)drawPolyLine.PointArray[(num == 0) ? (drawPolyLine.PointArray.Count - 1) : 0];
						array[1] = (Point)drawPolyLine.PointArray[(num == 0) ? 1 : (drawPolyLine.PointArray.Count - 2)];
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					array[0] = (Point)drawPolyLine.PointArray[num - 1];
					array[1] = (Point)drawPolyLine.PointArray[num + 1];
				}
				if (!flag)
				{
					double num2 = DrawLine.FindDistanceToSegment(point, array[0], array[1]);
					if (num2 <= 2.0)
					{
						this.hoverSegment.StartPoint = array[0];
						this.hoverSegment.EndPoint = array[1];
						if (this.resizedObject.ObjectType == "Perimeter")
						{
							this.orthoEnable = drawPolyLine.IsHandlePartOfOpening(point);
						}
						if (!this.orthoEnable)
						{
							this.orthoEnable = ((Control.ModifierKeys & Keys.Control) != Keys.None);
						}
					}
				}
			}
		}

		private void TestForResize(Point point)
		{
			DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
			for (int i = 0; i < activeDrawingObjects.SelectionCount; i++)
			{
				DrawObject selectedObject = activeDrawingObjects.GetSelectedObject(i);
				if (selectedObject.Visible)
				{
					int num = selectedObject.HitTest(point, (int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
					if (num > 0)
					{
						if (!selectedObject.IsHandleResizable(num))
						{
							break;
						}
						this.selectMode = ToolPointer.SelectionMode.Size;
						this.resizedObject = selectedObject;
						this.resizedObjectHandle = num;
						activeDrawingObjects.UnselectAll();
						this.resizedObject.Selected = true;
						this.resizedObject.Resizing = true;
						this.commandChangeState = new CommandChangeState(base.DrawArea, base.DrawArea.ActiveLayerName);
						this.ReleaseParentIfNecessary(this.resizedObject);
						this.TestForOrtho(this.resizedObject, this.resizedObjectHandle);
						base.DrawArea.PointerInProgress = true;
						base.DrawArea.CurrentlyResizedObject = selectedObject;
						if (this.resizedObject.ObjectType == "Line" || this.resizedObject.ObjectType == "Area" || this.resizedObject.ObjectType == "Perimeter" || this.resizedObject.ObjectType == "Angle")
						{
							base.DrawArea.Cursor = Cursors.Cross;
							return;
						}
						break;
					}
				}
			}
		}

		private void TestForMove(Point point)
		{
			DrawObject drawObject = null;
			DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
			foreach (object obj in activeDrawingObjects.Collection)
			{
				DrawObject drawObject2 = (DrawObject)obj;
				if (drawObject2.Visible && drawObject2.HitTest(point, (int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y) == 0)
				{
					drawObject = drawObject2;
					base.DrawArea.PointerInProgress = true;
					break;
				}
			}
			if (drawObject == null)
			{
				return;
			}
			this.selectMode = ToolPointer.SelectionMode.Move;
			base.DrawArea.Cursor = Cursors.SizeAll;
			if (((Control.ModifierKeys & Keys.Control) == Keys.None && !drawObject.Selected) || base.DrawArea.DeductionParent != null)
			{
				activeDrawingObjects.UnselectAll();
			}
			if ((Control.ModifierKeys & Keys.Control) != Keys.None && drawObject.Selected)
			{
				drawObject.Selected = false;
			}
			else
			{
				drawObject.Selected = true;
			}
			this.ReleaseParentIfNecessary(drawObject);
			if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
			{
				this.ResetSelectedSegment((DrawPolyLine)drawObject);
			}
		}

		private void SetNetSelection()
		{
			this.selectMode = ToolPointer.SelectionMode.NetSelection;
			base.DrawArea.DrawNetRectangle = true;
			base.DrawArea.PointerInProgress = true;
			base.DrawArea.NetSelectionInProgress = true;
			base.DrawArea.CursorRestricted = true;
		}

		private void ClickOnBackground()
		{
			if (base.DrawArea.DeductionParent != null)
			{
				base.DrawArea.DeductionParentRelease();
				return;
			}
			DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
			if ((Control.ModifierKeys & Keys.Control) == Keys.None)
			{
				activeDrawingObjects.UnselectAll();
			}
			this.SetNetSelection();
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			this.mouseEventArgs = e;
			Point location = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Right)
			{
				this.TrackPanning(location);
				return;
			}
			this.TrackMouse(location);
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

		public override void TrackMouse(Point location)
		{
			Point point = base.DrawArea.BackTrackMouse(location);
			if (!this.ZoomSelection && this.mouseEventArgs.Button == MouseButtons.None)
			{
				this.TrackMouseNoButton(point);
				return;
			}
			if (this.mouseEventArgs.Button != MouseButtons.Left)
			{
				return;
			}
			if (this.selectMode == ToolPointer.SelectionMode.Size)
			{
				int deltaX = point.X - this.lastPoint.X - (this.lastOrigin.X - (int)base.DrawArea.DrawingBoard.Origin.X);
				int deltaY = point.Y - this.lastPoint.Y - (this.lastOrigin.Y - (int)base.DrawArea.DrawingBoard.Origin.Y);
				this.TrackMouseResize(point, deltaX, deltaY);
			}
			if (this.selectMode == ToolPointer.SelectionMode.Move)
			{
				if (!this.moving)
				{
					int num = point.X - this.startPoint.X - (this.lastOrigin.X - (int)base.DrawArea.DrawingBoard.Origin.X);
					int num2 = point.Y - this.startPoint.Y - (this.lastOrigin.Y - (int)base.DrawArea.DrawingBoard.Origin.Y);
					this.moving = (Math.Abs(num) > 4 || Math.Abs(num2) > 4);
					if (this.moving)
					{
						this.commandChangeState = new CommandChangeState(base.DrawArea, base.DrawArea.ActiveLayerName);
					}
				}
				if (this.moving)
				{
					int num = point.X - this.lastPoint.X - (this.lastOrigin.X - (int)base.DrawArea.DrawingBoard.Origin.X);
					int num2 = point.Y - this.lastPoint.Y - (this.lastOrigin.Y - (int)base.DrawArea.DrawingBoard.Origin.Y);
					this.TrackMouseMove(point, num, num2);
				}
			}
			this.lastPoint = new Point(point.X, point.Y);
			if (this.selectMode == ToolPointer.SelectionMode.NetSelection)
			{
				this.TrackMouseNetSelection();
				return;
			}
			this.lastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
		}

		private void TrackMouseNoButton(Point point)
		{
			Cursor cursor = null;
			DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
			if (activeDrawingObjects == null)
			{
				base.DrawArea.Cursor = Cursors.Default;
				return;
			}
			int i = 0;
			while (i < activeDrawingObjects.SelectionCount)
			{
				DrawObject selectedObject = activeDrawingObjects.GetSelectedObject(i);
				int num = selectedObject.HitTest(point, (int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
				if (num > 0)
				{
					cursor = selectedObject.GetHandleCursor(num);
					if (selectedObject.ObjectType == "Area" || selectedObject.ObjectType == "Perimeter")
					{
						this.ResetSelectedSegment((DrawPolyLine)selectedObject);
						break;
					}
					break;
				}
				else
				{
					if ((selectedObject.ObjectType == "Area" || selectedObject.ObjectType == "Perimeter") && activeDrawingObjects.SelectionCount == 1)
					{
						DrawPolyLine drawPolyLine = (DrawPolyLine)selectedObject;
						bool flag = drawPolyLine.SelectedSegment.IsValid();
						if (drawPolyLine.PointIsOnOutline)
						{
							int nearestPointIndex = drawPolyLine.GetNearestPointIndex(new Point(point.X + (int)base.DrawArea.DrawingBoard.Origin.X, point.Y + (int)base.DrawArea.DrawingBoard.Origin.Y));
							if (nearestPointIndex > -1)
							{
								cursor = this.polyLineCursor;
								drawPolyLine.SelectedSegment.StartPoint = (Point)drawPolyLine.PointArray[nearestPointIndex];
								drawPolyLine.SelectedSegment.EndPoint = (Point)drawPolyLine.PointArray[(nearestPointIndex + 1 == drawPolyLine.PointArray.Count) ? 0 : (nearestPointIndex + 1)];
								base.DrawArea.DrawingBoard.Invalidate(selectedObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
								break;
							}
						}
						if (flag)
						{
							this.ResetSelectedSegment(drawPolyLine);
						}
						base.DrawArea.Refresh();
					}
					i++;
				}
			}
			base.DrawArea.Cursor = (cursor ?? Cursors.Default);
		}

		private void TrackMouseResize(Point point, int deltaX, int deltaY)
		{
			if (this.resizedObject == null)
			{
				return;
			}
			bool flag = false;
			if (this.orthoEnable)
			{
				Point point2 = ((DrawPolyLine)this.resizedObject).LocatePointOnLine(new Point(point.X + (int)base.DrawArea.DrawingBoard.Origin.X, point.Y + (int)base.DrawArea.DrawingBoard.Origin.Y), this.hoverSegment.StartPoint, this.hoverSegment.EndPoint);
				DrawPolyLine drawPolyLine = (DrawPolyLine)this.resizedObject;
				for (int i = 0; i < drawPolyLine.PointArray.Count; i++)
				{
					if (i != this.resizedObjectHandle - 1 && ((Point)drawPolyLine.PointArray[i]).Equals(point2))
					{
						point.X = this.lastPoint.X;
						point.Y = this.lastPoint.Y;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					point.X = point2.X;
					point.Y = point2.Y;
					point.X -= (int)base.DrawArea.DrawingBoard.Origin.X;
					point.Y -= (int)base.DrawArea.DrawingBoard.Origin.Y;
				}
			}
			if (!flag)
			{
				base.DrawArea.DrawingBoard.Invalidate(this.resizedObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
				this.resizedObject.MoveHandleTo(point, this.resizedObjectHandle);
				base.DrawArea.DrawingBoard.Invalidate(this.resizedObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
				base.DrawArea.DrawingBoard.Update();
			}
			if (this.resizedObject.ObjectType == "Line" || this.resizedObject.ObjectType == "Area" || this.resizedObject.ObjectType == "Perimeter" || this.resizedObject.ObjectType == "Angle")
			{
				base.DrawArea.Cursor = Cursors.Cross;
			}
		}

		private void TrackMouseMove(Point point, int deltaX, int deltaY)
		{
			DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
			for (int i = 0; i < activeDrawingObjects.SelectionCount; i++)
			{
				DrawObject selectedObject = activeDrawingObjects.GetSelectedObject(i);
				base.DrawArea.DrawingBoard.Invalidate(selectedObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
				selectedObject.Move(deltaX, deltaY);
				selectedObject.Moving = true;
				base.DrawArea.DrawingBoard.Invalidate(selectedObject.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
			}
			base.DrawArea.DrawingBoard.Update();
			base.DrawArea.Cursor = Cursors.SizeAll;
		}

		private void TrackMouseNetSelection()
		{
			this.lastRectangle = DrawRectangle.GetNormalizedRectangle(this.startPoint.X + (this.lastOrigin.X - (int)base.DrawArea.DrawingBoard.Origin.X), this.startPoint.Y + (this.lastOrigin.Y - (int)base.DrawArea.DrawingBoard.Origin.Y), this.lastPoint.X, this.lastPoint.Y);
			base.DrawArea.NetRectangle = this.lastRectangle;
			base.DrawArea.Refresh();
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && !base.DrawArea.PanningInProgress)
			{
				base.DrawArea.OnContextMenu(e);
			}
			base.DrawArea.PanningInProgress = false;
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
			if (this.selectMode == ToolPointer.SelectionMode.NetSelection)
			{
				if (!this.ZoomSelection)
				{
					if (base.DrawArea.ActiveLayer.Visible)
					{
						activeDrawingObjects.SelectInRectangle(base.DrawArea.NetRectangle, (int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
						base.DrawArea.UpdateSelectedObjects();
					}
				}
				else
				{
					base.DrawArea.DrawingBoard.ZoomSelection(base.DrawArea.NetRectangle);
				}
				this.selectMode = ToolPointer.SelectionMode.None;
				base.DrawArea.DrawNetRectangle = false;
			}
			else if (this.commandChangeState != null)
			{
				bool flag = false;
				if (this.resizedObject != null)
				{
					this.resizedObject.Normalize();
					this.resizedObject.Resizing = false;
					this.resizedObject = null;
					flag = true;
				}
				for (int i = 0; i < activeDrawingObjects.SelectionCount; i++)
				{
					flag = (flag || activeDrawingObjects.GetSelectedObject(i).Moving);
					activeDrawingObjects.GetSelectedObject(i).Moving = false;
				}
				if (flag)
				{
					if (base.DrawArea.DeductionParent == null)
					{
						this.commandChangeState.NewState();
						base.DrawArea.AddCommandToHistory(this.commandChangeState);
					}
					else
					{
						base.DrawArea.DeductionWasChanged = true;
					}
					if (activeDrawingObjects.SelectionCount == 1)
					{
						base.DrawArea.Owner.UpdateObject(activeDrawingObjects.GetSelectedObject(0));
					}
					base.DrawArea.Owner.SetModified();
				}
				this.commandChangeState = null;
			}
			this.lastPoint = base.DrawArea.BackTrackMouse(e.Location);
			base.DrawArea.Capture = false;
			base.DrawArea.Refresh();
			this.ReleaseTool();
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (!base.DrawArea.PointerInProgress)
				{
					this.CancelTool();
					this.ReleaseTool();
				}
				e.Handled = true;
			}
		}

		private void ResetSelectedSegment(DrawPolyLine drawPolyLine)
		{
			if (drawPolyLine.SelectedSegment.IsValid())
			{
				drawPolyLine.SelectedSegment.Reset();
				base.DrawArea.DrawingBoard.Invalidate(drawPolyLine.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
			}
		}

		private void ReleaseParentIfNecessary(DrawObject drawObject)
		{
			if (base.DrawArea.DeductionParent != null)
			{
				bool flag = true;
				if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
				{
					flag = (drawObject.DeductionParentID == -1);
				}
				if (flag)
				{
					base.DrawArea.DeductionParentRelease();
				}
			}
		}

		private bool moving;

		private bool orthoEnable;

		private int resizedObjectHandle;

		private DrawObject resizedObject;

		private Point lastPoint;

		private Point lastOrigin;

		private Point startPoint;

		private Rectangle lastRectangle;

		private ToolPointer.SelectionMode selectMode;

		private MouseEventArgs mouseEventArgs;

		private CommandChangeState commandChangeState;

		private readonly Segment hoverSegment;

		private readonly Cursor polyLineCursor;

		[CompilerGenerated]
		private bool <Tracking>k__BackingField;

		[CompilerGenerated]
		private bool <ZoomSelection>k__BackingField;

		private enum SelectionMode
		{
			None,
			NetSelection,
			Move,
			Size
		}
	}
}
