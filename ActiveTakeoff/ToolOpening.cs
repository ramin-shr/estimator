using System;
using System.Drawing;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolOpening : ToolObject
	{
		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		private Point AdjustOpening(Point location)
		{
			Point result = base.DrawArea.BackTrackMouse(location);
			Point point = this.newOpening.LocatePointOnLine(new Point(result.X + (int)base.DrawArea.DrawingBoard.Origin.X, result.Y + (int)base.DrawArea.DrawingBoard.Origin.Y), base.DrawArea.ToolSettings.SelectedSegment.StartPoint, base.DrawArea.ToolSettings.SelectedSegment.EndPoint);
			if (base.DrawArea.DeductionParent.HitTest(new Point(point.X - (int)base.DrawArea.DrawingBoard.Origin.X, point.Y - (int)base.DrawArea.DrawingBoard.Origin.Y), (int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y) == -1)
			{
				return Point.Empty;
			}
			DrawPolyLine deductionParent = base.DrawArea.DeductionParent;
			for (int i = 0; i < deductionParent.PointArray.Count; i++)
			{
				if (((Point)deductionParent.PointArray[i]).Equals(point))
				{
					base.DrawArea.SuspendScrolling = true;
					return Point.Empty;
				}
			}
			result.X = point.X;
			result.Y = point.Y;
			result.X -= (int)base.DrawArea.DrawingBoard.Origin.X;
			result.Y -= (int)base.DrawArea.DrawingBoard.Origin.Y;
			return result;
		}

		private void ResetVariables()
		{
			this.newOpening = null;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
			this.newOpening = new DrawLine((int)((float)base.DrawArea.ToolSettings.StartPoint.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)base.DrawArea.ToolSettings.StartPoint.Y + base.DrawArea.DrawingBoard.Origin.Y), (int)((float)base.DrawArea.ToolSettings.StartPoint.X + base.DrawArea.DrawingBoard.Origin.X + 1f), (int)((float)base.DrawArea.ToolSettings.StartPoint.Y + base.DrawArea.DrawingBoard.Origin.Y + 1f), new PointF(0f, 0f), base.DrawArea.ToolSettings.Name, base.DrawArea.ToolSettings.GroupID, base.DrawArea.ToolSettings.Comment, false, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.Opacity, base.DrawArea.ToolSettings.LineWidth)
			{
				DeductionParentID = base.DrawArea.DeductionParent.ID
			};
			this.newOpening.SetSlopeFactor(base.DrawArea.ToolSettings.SlopeFactor);
			base.AddNewObject(this.newOpening);
			this.lastPoint.X = this.newOpening.StartPoint.X;
			this.lastPoint.Y = this.newOpening.StartPoint.Y;
			this.lastPoint.X = this.lastPoint.X - (int)base.DrawArea.DrawingBoard.Origin.X;
			this.lastPoint.Y = this.lastPoint.Y - (int)base.DrawArea.DrawingBoard.Origin.Y;
			base.StartDrawing(this.newOpening);
			base.DrawArea.CursorRestricted = true;
			string status = string.Concat(new string[]
			{
				Resources.Cliquez_le_point_de_fin,
				" (",
				Resources.Ctrl_pour_saisir_la_hauteur,
				")  |  ",
				Resources.Esc_pour_annuler
			});
			base.DrawArea.UpdateStatusBar(status);
			base.DrawArea.Refresh();
		}

		public override void ReleaseTool()
		{
			bool flag = false;
			DrawPolyLine drawPolyLine = null;
			Segment segment = new Segment();
			if (this.newOpening != null)
			{
				if ((Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					flag = true;
					drawPolyLine = base.DrawArea.DeductionParent;
					segment.StartPoint = new Point(this.newOpening.StartPoint.X, this.newOpening.StartPoint.Y);
					segment.EndPoint = new Point(this.newOpening.EndPoint.X, this.newOpening.EndPoint.Y);
				}
				this.newOpening.Visible = false;
				base.DrawArea.DeductionParent.CreateOpening(this.newOpening, base.DrawArea.ToolSettings.SelectedSegment, true, false);
				base.DrawArea.DeductionParent.Selected = true;
				base.DrawArea.DeductionSaveCommand();
				base.DrawArea.DeductionParentRelease();
				base.DrawArea.SaveObject(this.newOpening);
			}
			this.ResetVariables();
			base.StopDrawing();
			if (flag)
			{
				base.DrawArea.Owner.OpeningSetHeight(drawPolyLine, segment);
			}
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.TrackMouse(new Point(e.X, e.Y));
		}

		public override void TrackMouse(Point location)
		{
			if (this.newOpening == null || base.Tracking || base.DrawArea.PanningInProgress)
			{
				return;
			}
			base.Tracking = true;
			Point point = this.AdjustOpening(location);
			if (!point.IsEmpty)
			{
				base.MoveHandleTo(this.newOpening, point, 2);
				this.lastPoint = point;
			}
			base.Tracking = false;
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			if (this.newOpening != null && !base.DrawArea.PanningInProgress)
			{
				this.newOpening.FigureCompleted = true;
			}
			base.OnMouseUp(e);
			if (this.newOpening == null)
			{
				return;
			}
			if (this.newOpening.FigureCompleted)
			{
				this.ReleaseTool();
			}
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.newOpening = null;
				if (base.DrawArea.DeductionParent != null)
				{
					base.DrawArea.DeductionParent.Selected = true;
					base.DrawArea.DeductionParentRelease();
				}
				base.CancelDrawing();
				this.ReleaseTool();
			}
		}

		public ToolOpening()
		{
		}

		private DrawLine newOpening;

		private Point lastPoint;
	}
}
