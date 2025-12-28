using System;
using System.Drawing;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolRectangle : ToolObject
	{
		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		private void ResetVariables()
		{
			this.newRectangle = null;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
			string status = string.Concat(new string[]
			{
				Resources.Cliquez_et_étirez,
				"  |  ",
				Resources.Cliquez_droit_et_déplacer,
				"  |  ",
				Resources.Esc_pour_annuler
			});
			base.DrawArea.UpdateStatusBar(status);
			this.ResetVariables();
		}

		public override void ReleaseTool()
		{
			if (this.newRectangle != null)
			{
				base.DrawArea.SaveObject(this.newRectangle);
			}
			this.ResetVariables();
			base.StopDrawing();
		}

		protected void BaseOnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		protected void BaseOnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
		}

		protected void BaseOnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			if (base.DrawArea.FindObjectByName(base.DrawArea.ToolSettings.Name, true) != null)
			{
				base.DrawArea.ToolSettings.Name = base.DrawArea.GetFreeObjectName(Resources.Marqueur);
			}
			Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
			this.newRectangle = new DrawRectangle((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y), 1, 1, new PointF(0f, 0f), base.DrawArea.ToolSettings.Name, base.DrawArea.ToolSettings.Comment, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.FillColor, base.DrawArea.ToolSettings.Opacity, base.DrawArea.ToolSettings.DrawFilled, base.DrawArea.ToolSettings.LineWidth);
			base.AddNewObject(this.newRectangle);
			base.StartDrawing(this.newRectangle);
			base.DrawArea.CursorRestricted = true;
			string status = string.Concat(new string[]
			{
				Resources.Cliquez_et_étirez,
				"  |  ",
				Resources.Relâcher_pour_compléter,
				"  |  ",
				Resources.Esc_pour_annuler
			});
			base.DrawArea.UpdateStatusBar(status);
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.TrackMouse(new Point(e.X, e.Y));
		}

		public override void TrackMouse(Point location)
		{
			if (this.newRectangle == null || base.Tracking)
			{
				return;
			}
			base.Tracking = true;
			base.MoveHandleTo(this.newRectangle, base.DrawArea.BackTrackMouse(location), 5);
			base.Tracking = false;
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			this.ReleaseTool();
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.newRectangle = null;
				base.CancelDrawing();
				this.ReleaseTool();
			}
		}

		public ToolRectangle()
		{
		}

		private DrawRectangle newRectangle;
	}
}
