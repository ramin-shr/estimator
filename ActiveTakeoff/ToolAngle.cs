using System;
using System.Drawing;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolAngle : ToolObject
	{
		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		private DrawAngle CreateObject(int x1, int y1, int x2, int y2)
		{
			if (base.DrawArea.FindObjectByName(base.DrawArea.ToolSettings.Name, true) != null)
			{
				base.DrawArea.ToolSettings.Name = base.DrawArea.GetFreeObjectName(Resources.Angle);
			}
			return new DrawAngle(x1, y1, x2, y2, base.DrawArea.DrawingBoard.Origin, base.DrawArea.ToolSettings.Name, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.Opacity, base.DrawArea.ToolSettings.LineWidth, (DrawAngle.AngleTypeEnum)Settings.Default.AngleType)
			{
				ShowMeasure = base.DrawArea.ToolSettings.ShowMeasure,
				EndPoint = new Point(x2, y2)
			};
		}

		private void ResetVariables()
		{
			this.newAngle = null;
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
			if (this.newAngle != null)
			{
				base.DrawArea.SaveObject(this.newAngle);
			}
			this.ResetVariables();
			base.StopDrawing();
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Right)
			{
				if (this.newAngle == null)
				{
					return;
				}
				if (this.newAngle.PointArray.Count < 3)
				{
					return;
				}
			}
			Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
			if (this.newAngle == null)
			{
				this.newAngle = this.CreateObject((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y), (int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X + 1f), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y + 1f));
				base.AddNewObject(this.newAngle);
				base.StartDrawing(this.newAngle);
				base.DrawArea.CursorRestricted = true;
				string status = string.Concat(new string[]
				{
					Resources.Cliquez_le_point_du_milieu,
					"  |  ",
					Resources.Cliquez_droit_et_déplacer,
					"  |  ",
					Resources.Esc_pour_annuler
				});
				base.DrawArea.UpdateStatusBar(status);
			}
			base.DrawArea.Refresh();
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.TrackMouse(new Point(e.X, e.Y));
		}

		public override void TrackMouse(Point location)
		{
			if (this.newAngle == null || base.Tracking)
			{
				return;
			}
			base.Tracking = true;
			base.MoveHandleTo(this.newAngle, base.DrawArea.BackTrackMouse(location), this.newAngle.HandleCount);
			base.Tracking = false;
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			if (this.newAngle != null && !base.DrawArea.PanningInProgress)
			{
				Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
				Point point2 = new Point((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y));
				bool flag = true;
				for (int i = 0; i < this.newAngle.PointArray.Count - 1; i++)
				{
					Point point3 = (Point)this.newAngle.PointArray[i];
					if (point2.Equals(point3))
					{
						flag = false;
						break;
					}
					DrawLine drawLine = new DrawLine(point3.X, point3.Y, point2.X, point2.Y, PointF.Empty, "", -1, "");
					if (drawLine.Distance2D(false) < 20)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.newAngle.AddPoint(point2);
					this.newAngle.EndPoint = point2;
					if (this.newAngle.PointArray.Count == 4)
					{
						this.newAngle.PointArray.RemoveAt(3);
						this.newAngle.FigureCompleted = true;
					}
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
			}
			base.OnMouseUp(e);
			if (this.newAngle == null)
			{
				return;
			}
			if (this.newAngle.FigureCompleted)
			{
				this.ReleaseTool();
			}
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.newAngle = null;
				base.CancelDrawing();
				this.ReleaseTool();
			}
		}

		public ToolAngle()
		{
		}

		private DrawAngle newAngle;
	}
}
