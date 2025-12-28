using System;
using System.Drawing;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolScale : ToolObject
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
			this.mustResetOrtho = !base.DrawArea.OrthoEnabled;
			if (this.mustResetOrtho)
			{
				base.DrawArea.Owner.EnableOrtho(true, true);
			}
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
			int num = 0;
			if (this.newLine != null)
			{
				num = this.newLine.Distance2D(false);
			}
			base.CancelDrawing();
			this.ResetVariables();
			base.StopDrawing();
			base.DrawArea.SelectPointerTool();
			if (num > 0)
			{
				base.DrawArea.ScaleWasSetManually(num);
			}
			if (this.mustResetOrtho)
			{
				base.DrawArea.Owner.EnableOrtho(false, true);
			}
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.newLine == null)
			{
				if (e.Button != MouseButtons.Left)
				{
					return;
				}
				Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
				this.newLine = new DrawLine((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y), (int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X + 1f), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y + 1f), new PointF(0f, 0f), "Scale", -1, "", false, Color.Lime, 225, 10)
				{
					DisplayInPixels = true,
					ShowMeasure = false
				};
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
			else if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				this.endPointClicked = true;
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
			if (this.newLine == null || base.Tracking)
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
			this.checkForMode = false;
			if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && (this.endPointClicked || !this.pointMode) && this.newLine != null && !base.DrawArea.PanningInProgress)
			{
				if (this.newLine.Distance2D(false) < 250)
				{
					string référence_linéaire_trop_petite = Resources.Référence_linéaire_trop_petite;
					string pour_obtenir_une_échelle_précise = Resources.Pour_obtenir_une_échelle_précise;
					DialogResult dialogResult = Utilities.DisplayWarningQuestionYesNoCancel(référence_linéaire_trop_petite, pour_obtenir_une_échelle_précise);
					DialogResult dialogResult2 = dialogResult;
					if (dialogResult2 == DialogResult.Cancel)
					{
						this.newLine = null;
						this.ReleaseTool();
						return;
					}
					switch (dialogResult2)
					{
					case DialogResult.Yes:
						this.newLine.FigureCompleted = true;
						break;
					case DialogResult.No:
						this.pointMode = true;
						break;
					}
				}
				else
				{
					this.newLine.FigureCompleted = true;
				}
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
				this.ReleaseTool();
			}
		}

		public ToolScale()
		{
		}

		private DrawLine newLine;

		private bool mustResetOrtho;

		private bool endPointClicked;

		private bool pointMode = true;

		private bool checkForMode = true;
	}
}
