using QuoterPlan.Properties;
using QuoterPlanControls;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    internal abstract class ToolObject : Tool
    {
        private Point lastPoint;

        private Point lastOrigin;

        private Cursor cursor;

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
            get;
            set;
        }

        protected ToolObject()
        {
        }

        protected void AddNewObject(DrawObject drawObject)
        {
            base.DrawArea.ActiveDrawingObjects.UnselectAll();
            drawObject.Selected = true;
            drawObject.Dirty = true;
            DrawObject drawObject1 = base.DrawArea.FindObjectFromGroupID(base.DrawArea.ActivePlan, drawObject.GroupID);
            if (drawObject1 == null)
            {
                drawObject.MeasureWasInvisible = false;
            }
            else
            {
                drawObject.MeasureWasInvisible = drawObject1.MeasureWasInvisible;
            }
            this.InsertObject(drawObject, base.DrawArea.ActiveLayerIndex, true, true);
            base.DrawArea.Capture = true;
            base.DrawArea.Refresh();
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

        protected void Move(DrawObject drawObject, int deltaX, int deltaY)
        {
            DrawingBoard drawingBoard = base.DrawArea.DrawingBoard;
            int x = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            drawingBoard.Invalidate(drawObject.Region(x, (int)origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
            drawObject.Move(deltaX, deltaY);
            DrawingBoard drawingBoard1 = base.DrawArea.DrawingBoard;
            int num = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF pointF = base.DrawArea.DrawingBoard.Origin;
            drawingBoard1.Invalidate(drawObject.Region(num, (int)pointF.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
            base.DrawArea.DrawingBoard.Update();
        }

        protected void MoveHandleTo(DrawObject drawObject, Point point, int handleNumber)
        {
            DrawingBoard drawingBoard = base.DrawArea.DrawingBoard;
            int x = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            drawingBoard.Invalidate(drawObject.Region(x, (int)origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
            drawObject.MoveHandleTo(point, handleNumber);
            DrawingBoard drawingBoard1 = base.DrawArea.DrawingBoard;
            int num = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF pointF = base.DrawArea.DrawingBoard.Origin;
            drawingBoard1.Invalidate(drawObject.Region(num, (int)pointF.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
            base.DrawArea.DrawingBoard.Update();
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
            this.lastPoint = new Point(point.X, point.Y);
            int x = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            this.lastOrigin = new Point(x, (int)origin.Y);
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.TrackPanning(new Point(e.X, e.Y));
            }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !base.DrawArea.DrawingInProgress && !base.DrawArea.PanningInProgress)
            {
                base.DrawArea.OnContextMenu(e);
            }
            base.DrawArea.PanningInProgress = false;
        }

        public Point SnapPoint(Point startPoint, Point endPoint, Func<Point, Point, int> getDistance, int angle, int degreeThresold)
        {
            if (angle >= 0x168 - degreeThresold || angle <= degreeThresold || angle >= 180 - degreeThresold && angle <= 180 + degreeThresold)
            {
                return new Point(endPoint.X, startPoint.Y);
            }
            if (angle >= 90 - degreeThresold && angle <= 90 + degreeThresold || angle >= 0x10e - degreeThresold && angle <= 0x10e + degreeThresold)
            {
                return new Point(startPoint.X, endPoint.Y);
            }
            int num = getDistance(startPoint, endPoint);
            if (angle >= 45 - degreeThresold && angle <= 45 + degreeThresold)
            {
                return new Point((int)((double)startPoint.X + (double)num * Math.Cos(Utilities.DegreeToRadian(45))), (int)((double)startPoint.Y + (double)num * Math.Sin(Utilities.DegreeToRadian(45))));
            }
            if (angle >= 135 - degreeThresold && angle <= 135 + degreeThresold)
            {
                return new Point((int)((double)startPoint.X - (double)num * Math.Cos(Utilities.DegreeToRadian(45))), (int)((double)startPoint.Y + (double)num * Math.Sin(Utilities.DegreeToRadian(45))));
            }
            if (angle >= 225 - degreeThresold && angle <= 225 + degreeThresold)
            {
                return new Point((int)((double)startPoint.X - (double)num * Math.Cos(Utilities.DegreeToRadian(45))), (int)((double)startPoint.Y - (double)num * Math.Sin(Utilities.DegreeToRadian(45))));
            }
            if (angle < 0x13b - degreeThresold || angle > 0x13b + degreeThresold)
            {
                return endPoint;
            }
            return new Point((int)((double)startPoint.X + (double)num * Math.Cos(Utilities.DegreeToRadian(45))), (int)((double)startPoint.Y - (double)num * Math.Sin(Utilities.DegreeToRadian(45))));
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

        private void TrackPanning(Point location)
        {
            if (this.Tracking)
            {
                return;
            }
            this.Tracking = true;
            Point point = base.DrawArea.BackTrackMouse(location);
            Point point1 = new Point(this.lastOrigin.X, this.lastOrigin.Y)
            {
                X = point1.X + (this.lastPoint.X - point.X),
                Y = point1.Y + (this.lastPoint.Y - point.Y)
            };
            if (!point1.Equals(this.lastOrigin))
            {
                base.DrawArea.PanningInProgress = true;
                base.DrawArea.DrawingBoard.Origin = point1;
                base.DrawArea.Refresh();
            }
            this.lastPoint = new Point(point.X, point.Y);
            int x = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            this.lastOrigin = new Point(x, (int)origin.Y);
            this.Tracking = false;
        }
    }
}