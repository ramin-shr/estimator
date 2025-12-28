using System;
using System.Drawing;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolLine : ToolObject
	{
		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		private Point AdjustLine(Point location)
		{
			if (base.DrawArea.OrthoModeIsOn)
			{
				Point startPoint = new Point(this.newLine.StartPoint.X - (int)base.DrawArea.DrawingBoard.Origin.X, this.newLine.StartPoint.Y - (int)base.DrawArea.DrawingBoard.Origin.Y);
				double num = DrawLine.Angle((double)startPoint.X, (double)startPoint.Y, (double)location.X, (double)location.Y);
				location = base.SnapPoint(startPoint, location, new Func<Point, Point, int>(this.newLine.Distance2D), (int)num, 5);
			}
			return location;
		}

		private void ResetVariables()
		{
			this.newLine = null;
			this.endPointClicked = false;
			this.pointMode = true;
			this.checkForMode = true;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
			string status = string.Concat(new string[]
			{
				Resources.Cliquez_le_point_de_départ,
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
			if (this.newLine != null)
			{
				base.DrawArea.SaveObject(this.newLine);
			}
			this.ResetVariables();
			base.StopDrawing();
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.newLine != null)
			{
				if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
				{
					this.endPointClicked = true;
				}
				return;
			}
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
			this.newLine = new DrawLine((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y), (int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X + 1f), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y + 1f), new PointF(0f, 0f), base.DrawArea.ToolSettings.Name, base.DrawArea.ToolSettings.GroupID, base.DrawArea.ToolSettings.Comment, false, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.Opacity, base.DrawArea.ToolSettings.LineWidth)
			{
				ShowMeasure = base.DrawArea.ToolSettings.ShowMeasure
			};
			this.newLine.SetSlopeFactor(base.DrawArea.ToolSettings.SlopeFactor);
			base.AddNewObject(this.newLine);
			base.StartDrawing(this.newLine);
			base.DrawArea.CursorRestricted = true;
			string status = string.Concat(new string[]
			{
				Resources.Cliquez_le_point_de_fin,
				" ",
				Resources.ou,
				" ",
				Resources.Étirez_distance,
				"  |  ",
				Resources.Cliquez_droit_et_déplacer,
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
			if (this.newLine == null || base.Tracking || base.DrawArea.PanningInProgress)
			{
				return;
			}
			base.Tracking = true;
			Point point = base.DrawArea.BackTrackMouse(location);
			if (this.checkForMode && (Math.Abs(this.newLine.StartPoint.X - (point.X + (int)base.DrawArea.DrawingBoard.Origin.X)) >= 50 || Math.Abs(this.newLine.StartPoint.Y - (point.Y + (int)base.DrawArea.DrawingBoard.Origin.Y)) >= 50))
			{
				this.pointMode = false;
				this.checkForMode = false;
				string status = Resources.Relâcher_pour_compléter + "  |  " + Resources.Esc_pour_annuler;
				base.DrawArea.UpdateStatusBar(status);
			}
			Point point2 = this.AdjustLine(base.DrawArea.BackTrackMouse(location));
			if (!point2.IsEmpty)
			{
				base.MoveHandleTo(this.newLine, point2, 2);
			}
			base.Tracking = false;
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			if (this.newLine != null)
			{
				this.checkForMode = false;
			}
			if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && (this.endPointClicked || !this.pointMode) && this.newLine != null && !base.DrawArea.PanningInProgress && this.newLine.Distance2D(false) > 10)
			{
				this.newLine.FigureCompleted = true;
			}
			base.OnMouseUp(e);
			if (this.newLine == null)
			{
				return;
			}
			if (this.pointMode && !this.newLine.FigureCompleted)
			{
				string status = string.Concat(new string[]
				{
					Resources.Cliquez_le_point_de_fin,
					"  |  ",
					Resources.Cliquez_droit_et_déplacer,
					"  |  ",
					Resources.Esc_pour_annuler
				});
				base.DrawArea.UpdateStatusBar(status);
			}
			if (this.newLine.FigureCompleted)
			{
				this.ReleaseTool();
			}
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.newLine = null;
				base.CancelDrawing();
				this.ReleaseTool();
			}
		}

		public ToolLine()
		{
		}

		private DrawLine newLine;

		private bool endPointClicked;

		private bool pointMode = true;

		private bool checkForMode = true;
	}
}
