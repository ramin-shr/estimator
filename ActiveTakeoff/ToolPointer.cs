using QuoterPlanControls;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    internal class ToolPointer : Tool
    {
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

        protected bool Tracking
        {
            get;
            set;
        }

        public bool ZoomSelection
        {
            private get;
            set;
        }

        public ToolPointer()
        {
            this.selectMode = ToolPointer.SelectionMode.None;
            this.lastPoint = Point.Empty;
            this.lastOrigin = Point.Empty;
            this.startPoint = Point.Empty;
            this.lastRectangle = new Rectangle();
            this.hoverSegment = new Segment();
            this.polyLineCursor = new Cursor(base.GetType(), "Pencil.cur");
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

        public override void InitializeTool(Cursor cursor)
        {
            this.LoadCursor(cursor);
            this.InitializeVariables();
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

        public override void LoadCursor(Cursor cursor)
        {
            base.DrawArea.Cursor = cursor;
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

        public override void OnMouseDown(MouseEventArgs e)
        {
            this.InitializeVariables();
            Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
            int x = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            this.lastOrigin = new Point(x, (int)origin.Y);
            if (e.Button == MouseButtons.Left)
            {
                if (this.ZoomSelection)
                {
                    this.SetNetSelection();
                }
                else
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
            }
            this.lastPoint = new Point(point.X, point.Y);
            this.startPoint = new Point(point.X, point.Y);
            int num = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF pointF = base.DrawArea.DrawingBoard.Origin;
            this.lastOrigin = new Point(num, (int)pointF.Y);
            this.lastRectangle = DrawRectangle.GetNormalizedRectangle(this.startPoint, this.lastPoint);
            base.DrawArea.NetRectangle = this.lastRectangle;
            base.DrawArea.Capture = true;
            base.DrawArea.Refresh();
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            this.mouseEventArgs = e;
            Point point = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                this.TrackPanning(point);
                return;
            }
            this.TrackMouse(point);
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
                if (this.ZoomSelection)
                {
                    base.DrawArea.DrawingBoard.ZoomSelection(base.DrawArea.NetRectangle);
                }
                else if (base.DrawArea.ActiveLayer.Visible)
                {
                    Rectangle netRectangle = base.DrawArea.NetRectangle;
                    int x = (int)base.DrawArea.DrawingBoard.Origin.X;
                    PointF origin = base.DrawArea.DrawingBoard.Origin;
                    activeDrawingObjects.SelectInRectangle(netRectangle, x, (int)origin.Y);
                    base.DrawArea.UpdateSelectedObjects();
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
                    flag = (flag ? true : activeDrawingObjects.GetSelectedObject(i).Moving);
                    activeDrawingObjects.GetSelectedObject(i).Moving = false;
                }
                if (flag)
                {
                    if (base.DrawArea.DeductionParent != null)
                    {
                        base.DrawArea.DeductionWasChanged = true;
                    }
                    else
                    {
                        this.commandChangeState.NewState();
                        base.DrawArea.AddCommandToHistory(this.commandChangeState);
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

        private void ReleaseParentIfNecessary(DrawObject drawObject)
        {
            if (base.DrawArea.DeductionParent != null)
            {
                bool deductionParentID = true;
                if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
                {
                    deductionParentID = drawObject.DeductionParentID == -1;
                }
                if (deductionParentID)
                {
                    base.DrawArea.DeductionParentRelease();
                }
            }
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

        private void ResetSelectedSegment(DrawPolyLine drawPolyLine)
        {
            if (drawPolyLine.SelectedSegment.IsValid())
            {
                drawPolyLine.SelectedSegment.Reset();
                DrawingBoard drawingBoard = base.DrawArea.DrawingBoard;
                int x = (int)base.DrawArea.DrawingBoard.Origin.X;
                PointF origin = base.DrawArea.DrawingBoard.Origin;
                drawingBoard.Invalidate(drawPolyLine.Region(x, (int)origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
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

        private void TestForMove(Point point)
        {
            DrawObject drawObject = null;
            DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
            foreach (DrawObject collection in activeDrawingObjects.Collection)
            {
                if (!collection.Visible || collection.HitTest(point, (int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y) != 0)
                {
                    continue;
                }
                drawObject = collection;
                base.DrawArea.PointerInProgress = true;
                break;
            }
            if (drawObject == null)
            {
                return;
            }
            this.selectMode = ToolPointer.SelectionMode.Move;
            base.DrawArea.Cursor = Cursors.SizeAll;
            if ((Control.ModifierKeys & Keys.Control) == Keys.None && !drawObject.Selected || base.DrawArea.DeductionParent != null)
            {
                activeDrawingObjects.UnselectAll();
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.None || !drawObject.Selected)
            {
                drawObject.Selected = true;
            }
            else
            {
                drawObject.Selected = false;
            }
            this.ReleaseParentIfNecessary(drawObject);
            if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
            {
                this.ResetSelectedSegment((DrawPolyLine)drawObject);
            }
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
                Point item = (Point)drawPolyLine.PointArray[num];
                Point[] pointArray = new Point[2];
                if (num != 0 && num != drawPolyLine.PointArray.Count - 1)
                {
                    pointArray[0] = (Point)drawPolyLine.PointArray[num - 1];
                    pointArray[1] = (Point)drawPolyLine.PointArray[num + 1];
                }
                else if (!drawPolyLine.CloseFigure)
                {
                    flag = true;
                }
                else
                {
                    ArrayList arrayLists = drawPolyLine.PointArray;
                    pointArray[0] = (Point)arrayLists[(num == 0 ? drawPolyLine.PointArray.Count - 1 : 0)];
                    ArrayList pointArray1 = drawPolyLine.PointArray;
                    pointArray[1] = (Point)pointArray1[(num == 0 ? 1 : drawPolyLine.PointArray.Count - 2)];
                }
                if (!flag && DrawLine.FindDistanceToSegment(item, pointArray[0], pointArray[1]) <= 2)
                {
                    this.hoverSegment.StartPoint = pointArray[0];
                    this.hoverSegment.EndPoint = pointArray[1];
                    if (this.resizedObject.ObjectType == "Perimeter")
                    {
                        this.orthoEnable = drawPolyLine.IsHandlePartOfOpening(item);
                    }
                    if (!this.orthoEnable)
                    {
                        this.orthoEnable = (Control.ModifierKeys & Keys.Control) != Keys.None;
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
                    int x = (int)base.DrawArea.DrawingBoard.Origin.X;
                    PointF origin = base.DrawArea.DrawingBoard.Origin;
                    int num = selectedObject.HitTest(point, x, (int)origin.Y);
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
                        if (!(this.resizedObject.ObjectType == "Line") && !(this.resizedObject.ObjectType == "Area") && !(this.resizedObject.ObjectType == "Perimeter") && !(this.resizedObject.ObjectType == "Angle"))
                        {
                            break;
                        }
                        base.DrawArea.Cursor = Cursors.Cross;
                        return;
                    }
                }
            }
        }

        public override void TrackMouse(Point location)
        {
            int x;
            int y;
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
                int num = point.X - this.lastPoint.X;
                int x1 = this.lastOrigin.X;
                PointF origin = base.DrawArea.DrawingBoard.Origin;
                int num1 = num - (x1 - (int)origin.X);
                int y1 = point.Y - this.lastPoint.Y;
                int y2 = this.lastOrigin.Y;
                PointF pointF = base.DrawArea.DrawingBoard.Origin;
                int num2 = y1 - (y2 - (int)pointF.Y);
                this.TrackMouseResize(point, num1, num2);
            }
            if (this.selectMode == ToolPointer.SelectionMode.Move)
            {
                if (!this.moving)
                {
                    int x2 = point.X - this.startPoint.X;
                    int x3 = this.lastOrigin.X;
                    PointF origin1 = base.DrawArea.DrawingBoard.Origin;
                    x = x2 - (x3 - (int)origin1.X);
                    int y3 = point.Y - this.startPoint.Y;
                    int num3 = this.lastOrigin.Y;
                    PointF pointF1 = base.DrawArea.DrawingBoard.Origin;
                    y = y3 - (num3 - (int)pointF1.Y);
                    this.moving = (Math.Abs(x) > 4 ? true : Math.Abs(y) > 4);
                    if (this.moving)
                    {
                        this.commandChangeState = new CommandChangeState(base.DrawArea, base.DrawArea.ActiveLayerName);
                    }
                }
                if (this.moving)
                {
                    int x4 = point.X - this.lastPoint.X;
                    int num4 = this.lastOrigin.X;
                    PointF origin2 = base.DrawArea.DrawingBoard.Origin;
                    x = x4 - (num4 - (int)origin2.X);
                    int y4 = point.Y - this.lastPoint.Y;
                    int y5 = this.lastOrigin.Y;
                    PointF pointF2 = base.DrawArea.DrawingBoard.Origin;
                    y = y4 - (y5 - (int)pointF2.Y);
                    this.TrackMouseMove(point, x, y);
                }
            }
            this.lastPoint = new Point(point.X, point.Y);
            if (this.selectMode == ToolPointer.SelectionMode.NetSelection)
            {
                this.TrackMouseNetSelection();
                return;
            }
            int x5 = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin3 = base.DrawArea.DrawingBoard.Origin;
            this.lastOrigin = new Point(x5, (int)origin3.Y);
        }

        private void TrackMouseMove(Point point, int deltaX, int deltaY)
        {
            DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
            for (int i = 0; i < activeDrawingObjects.SelectionCount; i++)
            {
                DrawObject selectedObject = activeDrawingObjects.GetSelectedObject(i);
                DrawingBoard drawingBoard = base.DrawArea.DrawingBoard;
                int x = (int)base.DrawArea.DrawingBoard.Origin.X;
                PointF origin = base.DrawArea.DrawingBoard.Origin;
                drawingBoard.Invalidate(selectedObject.Region(x, (int)origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
                selectedObject.Move(deltaX, deltaY);
                selectedObject.Moving = true;
                DrawingBoard drawingBoard1 = base.DrawArea.DrawingBoard;
                int num = (int)base.DrawArea.DrawingBoard.Origin.X;
                PointF pointF = base.DrawArea.DrawingBoard.Origin;
                drawingBoard1.Invalidate(selectedObject.Region(num, (int)pointF.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
            }
            base.DrawArea.DrawingBoard.Update();
            base.DrawArea.Cursor = Cursors.SizeAll;
        }

        private void TrackMouseNetSelection()
        {
            int x = this.startPoint.X;
            int num = this.lastOrigin.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            int x1 = x + (num - (int)origin.X);
            int y = this.startPoint.Y;
            int y1 = this.lastOrigin.Y;
            PointF pointF = base.DrawArea.DrawingBoard.Origin;
            this.lastRectangle = DrawRectangle.GetNormalizedRectangle(x1, y + (y1 - (int)pointF.Y), this.lastPoint.X, this.lastPoint.Y);
            base.DrawArea.NetRectangle = this.lastRectangle;
            base.DrawArea.Refresh();
        }

        private void TrackMouseNoButton(Point point)
        {
            Cursor handleCursor = null;
            DrawingObjects activeDrawingObjects = base.DrawArea.ActiveDrawingObjects;
            if (activeDrawingObjects == null)
            {
                base.DrawArea.Cursor = Cursors.Default;
                return;
            }
            int num = 0;
            while (num < activeDrawingObjects.SelectionCount)
            {
                DrawObject selectedObject = activeDrawingObjects.GetSelectedObject(num);
                int x = (int)base.DrawArea.DrawingBoard.Origin.X;
                PointF origin = base.DrawArea.DrawingBoard.Origin;
                int num1 = selectedObject.HitTest(point, x, (int)origin.Y);
                if (num1 <= 0)
                {
                    if ((selectedObject.ObjectType == "Area" || selectedObject.ObjectType == "Perimeter") && activeDrawingObjects.SelectionCount == 1)
                    {
                        DrawPolyLine item = (DrawPolyLine)selectedObject;
                        bool flag = item.SelectedSegment.IsValid();
                        if (item.PointIsOnOutline)
                        {
                            int x1 = point.X;
                            PointF pointF = base.DrawArea.DrawingBoard.Origin;
                            int x2 = x1 + (int)pointF.X;
                            int y = point.Y;
                            PointF origin1 = base.DrawArea.DrawingBoard.Origin;
                            int nearestPointIndex = item.GetNearestPointIndex(new Point(x2, y + (int)origin1.Y));
                            if (nearestPointIndex > -1)
                            {
                                handleCursor = this.polyLineCursor;
                                item.SelectedSegment.StartPoint = (Point)item.PointArray[nearestPointIndex];
                                item.SelectedSegment.EndPoint = (Point)item.PointArray[(nearestPointIndex + 1 == item.PointArray.Count ? 0 : nearestPointIndex + 1)];
                                DrawingBoard drawingBoard = base.DrawArea.DrawingBoard;
                                int num2 = (int)base.DrawArea.DrawingBoard.Origin.X;
                                PointF pointF1 = base.DrawArea.DrawingBoard.Origin;
                                drawingBoard.Invalidate(selectedObject.Region(num2, (int)pointF1.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            this.ResetSelectedSegment(item);
                        }
                        base.DrawArea.Refresh();
                    }
                    num++;
                }
                else
                {
                    handleCursor = selectedObject.GetHandleCursor(num1);
                    if (!(selectedObject.ObjectType == "Area") && !(selectedObject.ObjectType == "Perimeter"))
                    {
                        break;
                    }
                    this.ResetSelectedSegment((DrawPolyLine)selectedObject);
                    break;
                }
            }
            base.DrawArea.Cursor = handleCursor ?? Cursors.Default;
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
                DrawPolyLine drawPolyLine = (DrawPolyLine)this.resizedObject;
                int x = point.X;
                PointF origin = base.DrawArea.DrawingBoard.Origin;
                int num = x + (int)origin.X;
                int y = point.Y;
                PointF pointF = base.DrawArea.DrawingBoard.Origin;
                Point point1 = drawPolyLine.LocatePointOnLine(new Point(num, y + (int)pointF.Y), this.hoverSegment.StartPoint, this.hoverSegment.EndPoint);
                DrawPolyLine drawPolyLine1 = (DrawPolyLine)this.resizedObject;
                int num1 = 0;
                while (num1 < drawPolyLine1.PointArray.Count)
                {
                    if (num1 == this.resizedObjectHandle - 1 || !((Point)drawPolyLine1.PointArray[num1]).Equals(point1))
                    {
                        num1++;
                    }
                    else
                    {
                        point.X = this.lastPoint.X;
                        point.Y = this.lastPoint.Y;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    point.X = point1.X;
                    point.Y = point1.Y;
                    int x1 = point.X;
                    PointF origin1 = base.DrawArea.DrawingBoard.Origin;
                    point.X = x1 - (int)origin1.X;
                    int y1 = point.Y;
                    PointF pointF1 = base.DrawArea.DrawingBoard.Origin;
                    point.Y = y1 - (int)pointF1.Y;
                }
            }
            if (!flag)
            {
                DrawingBoard drawingBoard = base.DrawArea.DrawingBoard;
                DrawObject drawObject = this.resizedObject;
                int x2 = (int)base.DrawArea.DrawingBoard.Origin.X;
                PointF origin2 = base.DrawArea.DrawingBoard.Origin;
                drawingBoard.Invalidate(drawObject.Region(x2, (int)origin2.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
                this.resizedObject.MoveHandleTo(point, this.resizedObjectHandle);
                DrawingBoard drawingBoard1 = base.DrawArea.DrawingBoard;
                DrawObject drawObject1 = this.resizedObject;
                int num2 = (int)base.DrawArea.DrawingBoard.Origin.X;
                PointF pointF2 = base.DrawArea.DrawingBoard.Origin;
                drawingBoard1.Invalidate(drawObject1.Region(num2, (int)pointF2.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
                base.DrawArea.DrawingBoard.Update();
            }
            if (this.resizedObject.ObjectType == "Line" || this.resizedObject.ObjectType == "Area" || this.resizedObject.ObjectType == "Perimeter" || this.resizedObject.ObjectType == "Angle")
            {
                base.DrawArea.Cursor = Cursors.Cross;
            }
        }

        private void TrackPanning(Point location)
        {
            if (this.Tracking)
                return;

            this.Tracking = true;

            Point currentPoint = base.DrawArea.BackTrackMouse(location);

            int newOriginX = this.lastOrigin.X + (this.lastPoint.X - currentPoint.X);
            int newOriginY = this.lastOrigin.Y + (this.lastPoint.Y - currentPoint.Y);
            Point newOrigin = new Point(newOriginX, newOriginY);

            if (!newOrigin.Equals(this.lastOrigin))
            {
                base.DrawArea.PanningInProgress = true;
                base.DrawArea.DrawingBoard.Origin = newOrigin;
                base.DrawArea.Refresh();
            }

            this.lastPoint = new Point(currentPoint.X, currentPoint.Y);

            PointF origin = base.DrawArea.DrawingBoard.Origin;
            this.lastOrigin = new Point((int)origin.X, (int)origin.Y);

            this.Tracking = false;
        }

        private enum SelectionMode
        {
            None,
            NetSelection,
            Move,
            Size
        }
    }
}