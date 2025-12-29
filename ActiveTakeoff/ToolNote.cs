using QuoterPlan.Properties;
using QuoterPlanControls;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    internal class ToolNote : ToolRectangle
    {
        private Point LastOrigin
        {
            get;
            set;
        }

        private Point LastPoint
        {
            get;
            set;
        }

        private DrawNote NewNote
        {
            get;
            set;
        }

        public ToolNote()
        {
        }

        public override void InitializeTool(Cursor cursor)
        {
            this.LoadCursor(cursor);
            string[] cliquezEtÉtirezLaPositionDeLaNote = new string[] { Resources.Cliquez_et_étirez_la_position_de_la_note, "  |  ", Resources.Cliquez_droit_et_déplacer, "  |  ", Resources.Esc_pour_annuler };
            string str = string.Concat(cliquezEtÉtirezLaPositionDeLaNote);
            base.DrawArea.UpdateStatusBar(str);
            this.ResetVariables();
        }

        public override void LoadCursor(Cursor cursor)
        {
            base.Cursor = cursor;
            base.DrawArea.Cursor = base.Cursor;
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.NewNote = null;
                base.CancelDrawing();
                this.ReleaseTool();
            }
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            base.BaseOnMouseDown(e);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (base.DrawArea.FindObjectByName(base.DrawArea.ToolSettings.Name, true) != null)
            {
                base.DrawArea.ToolSettings.Name = base.DrawArea.GetFreeObjectName(Resources.Note);
            }
            Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
            float x = (float)point.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            int num = (int)(x + origin.X);
            float y = (float)point.Y;
            PointF pointF = base.DrawArea.DrawingBoard.Origin;
            this.NewNote = new DrawNote(num, (int)(y + pointF.Y), base.DrawArea.ToolSettings.Name, base.DrawArea.ToolSettings.Comment, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.FillColor, base.DrawArea.ToolSettings.Opacity, base.DrawArea.ToolSettings.DrawFilled, base.DrawArea.ToolSettings.LineWidth, 32);
            base.AddNewObject(this.NewNote);
            base.StartDrawing(this.NewNote);
            base.DrawArea.CursorRestricted = true;
            this.LastPoint = new Point(point.X, point.Y);
            int x1 = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin1 = base.DrawArea.DrawingBoard.Origin;
            this.LastOrigin = new Point(x1, (int)origin1.Y);
            string[] déplacezLaPositionDeLaNote = new string[] { Resources.Déplacez_la_position_de_la_note, "  |  ", Resources.Relâcher_pour_compléter, "  |  ", Resources.Esc_pour_annuler };
            string str = string.Concat(déplacezLaPositionDeLaNote);
            base.DrawArea.UpdateStatusBar(str);
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            base.BaseOnMouseMove(e);
            this.TrackMouse(new Point(e.X, e.Y));
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            base.BaseOnMouseUp(e);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            this.ReleaseTool();
        }

        public override void ReleaseTool()
        {
            if (this.NewNote != null)
            {
                base.DrawArea.SaveObject(this.NewNote);
            }
            this.ResetVariables();
            base.StopDrawing();
        }

        private void ResetVariables()
        {
            this.NewNote = null;
        }

        public override void TrackMouse(Point location)
        {
            if (this.NewNote == null || base.Tracking)
            {
                return;
            }
            base.Tracking = true;
            Point point = base.DrawArea.BackTrackMouse(location);
            int x = point.X - this.LastPoint.X;
            int num = this.LastOrigin.X;
            PointF origin = base.DrawArea.DrawingBoard.Origin;
            int x1 = x - (num - (int)origin.X);
            int y = point.Y - this.LastPoint.Y;
            int y1 = this.LastOrigin.Y;
            PointF pointF = base.DrawArea.DrawingBoard.Origin;
            int num1 = y - (y1 - (int)pointF.Y);
            base.Move(this.NewNote, x1, num1);
            this.LastPoint = new Point(point.X, point.Y);
            int x2 = (int)base.DrawArea.DrawingBoard.Origin.X;
            PointF origin1 = base.DrawArea.DrawingBoard.Origin;
            this.LastOrigin = new Point(x2, (int)origin1.Y);
            base.Tracking = false;
        }
    }
}