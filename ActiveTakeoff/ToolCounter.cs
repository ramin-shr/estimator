using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolCounter : ToolRectangle
	{
		private DrawCounter NewCounter
		{
			[CompilerGenerated]
			get
			{
				return this.<NewCounter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NewCounter>k__BackingField = value;
			}
		}

		private Point LastPoint
		{
			[CompilerGenerated]
			get
			{
				return this.<LastPoint>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LastPoint>k__BackingField = value;
			}
		}

		private Point LastOrigin
		{
			[CompilerGenerated]
			get
			{
				return this.<LastOrigin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LastOrigin>k__BackingField = value;
			}
		}

		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		private void ResetVariables()
		{
			this.NewCounter = null;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
			string status = string.Concat(new string[]
			{
				Resources.Cliquez_la_position_du_compteur,
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
			if (this.NewCounter != null)
			{
				base.DrawArea.SaveObject(this.NewCounter);
			}
			this.ResetVariables();
			base.StopDrawing();
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			base.BaseOnMouseDown(e);
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
			this.NewCounter = new DrawCounter((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X) - base.DrawArea.ToolSettings.CounterSize / 2, (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y) - base.DrawArea.ToolSettings.CounterSize / 2, base.DrawArea.ToolSettings.CounterSize, base.DrawArea.ToolSettings.CounterSize, base.DrawArea.ToolSettings.CounterSize, base.DrawArea.ToolSettings.Shape, base.DrawArea.ToolSettings.Name, base.DrawArea.ToolSettings.GroupID, base.DrawArea.ToolSettings.Text, base.DrawArea.ToolSettings.Comment, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.FillColor, base.DrawArea.ToolSettings.Opacity, base.DrawArea.ToolSettings.DrawFilled, base.DrawArea.ToolSettings.LineWidth);
			base.AddNewObject(this.NewCounter);
			base.StartDrawing(this.NewCounter);
			base.DrawArea.CursorRestricted = true;
			this.LastPoint = new Point(point.X, point.Y);
			this.LastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
			string status = string.Concat(new string[]
			{
				Resources.Déplacez_la_position_du_compteur,
				"  |  ",
				Resources.Relâcher_pour_compléter,
				"  |  ",
				Resources.Esc_pour_annuler
			});
			base.DrawArea.UpdateStatusBar(status);
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			base.BaseOnMouseMove(e);
			this.TrackMouse(new Point(e.X, e.Y));
		}

		public override void TrackMouse(Point location)
		{
			if (this.NewCounter == null || base.Tracking)
			{
				return;
			}
			base.Tracking = true;
			Point point = base.DrawArea.BackTrackMouse(location);
			int deltaX = point.X - this.LastPoint.X - (this.LastOrigin.X - (int)base.DrawArea.DrawingBoard.Origin.X);
			int deltaY = point.Y - this.LastPoint.Y - (this.LastOrigin.Y - (int)base.DrawArea.DrawingBoard.Origin.Y);
			base.Move(this.NewCounter, deltaX, deltaY);
			this.LastPoint = new Point(point.X, point.Y);
			this.LastOrigin = new Point((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
			base.Tracking = false;
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

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.NewCounter = null;
				base.CancelDrawing();
				this.ReleaseTool();
			}
		}

		public ToolCounter()
		{
		}

		[CompilerGenerated]
		private DrawCounter <NewCounter>k__BackingField;

		[CompilerGenerated]
		private Point <LastPoint>k__BackingField;

		[CompilerGenerated]
		private Point <LastOrigin>k__BackingField;
	}
}
