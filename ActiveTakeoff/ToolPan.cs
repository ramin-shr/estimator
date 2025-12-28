using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuoterPlan
{
	internal class ToolPan : ToolObject
	{
		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
		}

		public override void ReleaseTool()
		{
			base.DrawArea.PanningInProgress = false;
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
			this.lastPoint = new Point(point.X, point.Y);
			this.lastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
			base.DrawArea.PanningInProgress = true;
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			if (base.DrawArea.PanningInProgress)
			{
				this.TrackMouse(new Point(e.X, e.Y));
			}
		}

		public override void TrackMouse(Point location)
		{
			if (base.Tracking)
			{
				return;
			}
			base.Tracking = true;
			Point point = base.DrawArea.BackTrackMouse(location);
			Point p = new Point(this.lastOrigin.X, this.lastOrigin.Y);
			p.X += this.lastPoint.X - point.X;
			p.Y += this.lastPoint.Y - point.Y;
			if (!p.Equals(this.lastOrigin))
			{
				base.DrawArea.DrawingBoard.Origin = p;
				base.DrawArea.Refresh();
			}
			this.lastPoint = new Point(point.X, point.Y);
			this.lastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
			base.Tracking = false;
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			this.ReleaseTool();
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.ReleaseTool();
			}
		}

		public ToolPan()
		{
		}

		private Point lastPoint;

		private Point lastOrigin;
	}
}
