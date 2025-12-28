using System;
using System.Drawing;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolDeduction : ToolObject
	{
		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		private bool ParentHitTest(Point point)
		{
			int num = 0;
			for (int i = -10; i < 10; i++)
			{
				for (int j = -10; j < 10; j++)
				{
					num = base.DrawArea.DeductionParent.HitTest(new Point(point.X + i, point.Y + j), (int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y);
					if (num == 0)
					{
						break;
					}
				}
				if (num == 0)
				{
					break;
				}
			}
			return num == 0;
		}

		private Point AdjustLine(Point location)
		{
			if (base.DrawArea.OrthoModeIsOn && this.newDeduction.HandleCount > 1)
			{
				Point point = (Point)this.newDeduction.PointArray[this.newDeduction.HandleCount - 2];
				Point startPoint = new Point(point.X - (int)base.DrawArea.DrawingBoard.Origin.X, point.Y - (int)base.DrawArea.DrawingBoard.Origin.Y);
				double num = DrawLine.Angle((double)startPoint.X, (double)startPoint.Y, (double)location.X, (double)location.Y);
				location = base.SnapPoint(startPoint, location, new Func<Point, Point, int>(this.newDeduction.Distance2D), (int)num, 5);
			}
			return location;
		}

		private void ComputeRectangleZone()
		{
			if (this.newDeduction.PointArray.Count == 0)
			{
				return;
			}
			Point point = new Point(((Point)this.newDeduction.PointArray[0]).X, ((Point)this.newDeduction.PointArray[0]).Y);
			Point point2 = new Point(this.newDeduction.EndPoint.X, this.newDeduction.EndPoint.Y);
			for (int i = this.newDeduction.PointArray.Count - 1; i > 0; i--)
			{
				this.newDeduction.PointArray.RemoveAt(i);
			}
			if (point2.X > point.X && point2.Y < point.Y)
			{
				this.newDeduction.AddPoint(new Point(point2.X, point.Y));
				this.newDeduction.AddPoint(new Point(point2.X, point2.Y));
				this.newDeduction.AddPoint(new Point(point.X, point2.Y));
			}
			if (point2.X > point.X && point2.Y > point.Y)
			{
				this.newDeduction.AddPoint(new Point(point2.X, point.Y));
				this.newDeduction.AddPoint(new Point(point2.X, point2.Y));
				this.newDeduction.AddPoint(new Point(point.X, point2.Y));
			}
			if (point2.X < point.X && point2.Y > point.Y)
			{
				this.newDeduction.AddPoint(new Point(point2.X, point.Y));
				this.newDeduction.AddPoint(new Point(point2.X, point2.Y));
				this.newDeduction.AddPoint(new Point(point.X, point2.Y));
			}
			if (point2.X < point.X && point2.Y < point.Y)
			{
				this.newDeduction.AddPoint(new Point(point2.X, point.Y));
				this.newDeduction.AddPoint(new Point(point2.X, point2.Y));
				this.newDeduction.AddPoint(new Point(point.X, point2.Y));
			}
		}

		private void ResetVariables()
		{
			this.newDeduction = null;
			this.pointMode = true;
			this.checkForMode = true;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
			base.DrawArea.CursorRestricted = true;
			string status = string.Concat(new string[]
			{
				Resources.Cliquez_premier_point,
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
			if (this.newDeduction != null)
			{
				this.newDeduction.Visible = false;
				if ((Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					this.newDeduction.AutoAdjust((Bitmap)base.DrawArea.DrawingBoard.Image, DrawPolyLine.AutoAdjustType.ToZone);
				}
				base.DrawArea.DeductionParent.CreateDeduction(this.newDeduction);
				base.DrawArea.DeductionParent.Selected = true;
				base.DrawArea.DeductionSaveCommand();
				base.DrawArea.DeductionParentRelease();
				base.DrawArea.SaveObject(this.newDeduction);
			}
			this.ResetVariables();
			base.StopDrawing();
		}

		public override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				string text = Resources.Cliquez_un_autre_point + (this.checkForMode ? (" " + Resources.ou + " " + Resources.Étirez_pour_zone_rectangulaire) : string.Empty);
				Point point = base.DrawArea.BackTrackMouse(new Point(e.X, e.Y));
				if (this.ParentHitTest(point))
				{
					if (this.newDeduction == null)
					{
						this.newDeduction = new DrawPolyLine((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y), (int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X + 1f), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y + 1f), base.DrawArea.DrawingBoard.Origin, base.DrawArea.DeductionParent.Name, base.DrawArea.DeductionParent.GroupID, base.DrawArea.DeductionParent.Comment, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.FillColor, HatchStylePickerCombo.HatchStylePickerEnum.DarkDownwardDiagonal, base.DrawArea.ToolSettings.Opacity, true, true, base.DrawArea.ToolSettings.LineWidth)
						{
							ShowMeasure = base.DrawArea.DeductionParent.ShowMeasure,
							DeductionParentID = base.DrawArea.DeductionParent.ID,
							EndPoint = new Point((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X + 1f), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y + 1f))
						};
						this.newDeduction.SetSlopeFactor(base.DrawArea.ToolSettings.SlopeFactor);
						base.AddNewObject(this.newDeduction);
						this.checkForMode = true;
						base.StartDrawing(this.newDeduction);
						base.DrawArea.CursorRestricted = true;
					}
					else if (this.pointMode)
					{
						Point point2 = new Point((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y));
						this.newDeduction.AddPoint(point2);
						this.newDeduction.EndPoint = point2;
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							"  |  ",
							Resources.Cliquez_droit_pour_compléter,
							" (",
							Resources.Ctrl_Shift_pour_auto_ajuster,
							")"
						});
					}
					if (this.pointMode)
					{
						string text3 = text;
						text = string.Concat(new string[]
						{
							text3,
							"  |  ",
							Resources.Cliquez_droit_et_déplacer,
							"  |  ",
							Resources.Esc_pour_annuler
						});
						base.DrawArea.UpdateStatusBar(text);
					}
					base.DrawArea.Refresh();
				}
			}
		}

		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.TrackMouse(new Point(e.X, e.Y));
		}

		public override void TrackMouse(Point location)
		{
			if (this.newDeduction == null || base.Tracking || base.DrawArea.PanningInProgress)
			{
				return;
			}
			base.Tracking = true;
			Point location2 = base.DrawArea.BackTrackMouse(location);
			if (this.checkForMode && Math.Abs(((Point)this.newDeduction.PointArray[0]).X - (location2.X + (int)base.DrawArea.DrawingBoard.Origin.X)) > 15 && Math.Abs(((Point)this.newDeduction.PointArray[0]).Y - (location2.Y + (int)base.DrawArea.DrawingBoard.Origin.Y)) > 15)
			{
				this.pointMode = false;
				this.checkForMode = false;
				this.newDeduction.FigureCompleted = true;
			}
			if (this.pointMode)
			{
				Point point = this.AdjustLine(location2);
				if (!point.IsEmpty)
				{
					base.MoveHandleTo(this.newDeduction, point, this.newDeduction.HandleCount);
				}
			}
			else
			{
				this.newDeduction.EndPoint = new Point((int)((float)location2.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)location2.Y + base.DrawArea.DrawingBoard.Origin.Y));
				base.DrawArea.DrawingBoard.Invalidate(this.newDeduction.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
				this.ComputeRectangleZone();
				base.DrawArea.DrawingBoard.Invalidate(this.newDeduction.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
				base.DrawArea.DrawingBoard.Update();
				string status = string.Concat(new string[]
				{
					Resources.Relâcher_pour_compléter,
					" (",
					Resources.Ctrl_Shift_pour_auto_ajuster,
					")  |  ",
					Resources.Esc_pour_annuler
				});
				base.DrawArea.UpdateStatusBar(status);
			}
			base.Tracking = false;
		}

		public override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && this.checkForMode)
			{
				if (this.pointMode)
				{
					string text = Resources.Cliquez_un_autre_point;
					text += (base.DrawArea.ToolSettings.DrawFilled ? string.Empty : ("  |  " + Resources.Cliquez_droit_pour_compléter));
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"  |  ",
						Resources.Cliquez_droit_et_déplacer,
						"  |  ",
						Resources.Esc_pour_annuler
					});
					base.DrawArea.UpdateStatusBar(text);
				}
				this.checkForMode = false;
			}
			if (this.pointMode && e.Button == MouseButtons.Right && this.newDeduction != null && !base.DrawArea.PanningInProgress && this.newDeduction.PointArray.Count > 2)
			{
				this.newDeduction.FigureCompleted = this.ParentHitTest(base.DrawArea.BackTrackMouse(new Point(e.X, e.Y)));
			}
			base.OnMouseUp(e);
			if (this.pointMode && e.Button != MouseButtons.Right)
			{
				return;
			}
			if (this.newDeduction == null)
			{
				return;
			}
			if (this.newDeduction.FigureCompleted)
			{
				this.ReleaseTool();
			}
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.newDeduction = null;
				if (base.DrawArea.DeductionParent != null)
				{
					base.DrawArea.DeductionParent.Selected = true;
					base.DrawArea.DeductionParentRelease();
					base.DrawArea.Refresh();
				}
				this.ReleaseTool();
			}
		}

		public ToolDeduction()
		{
		}

		private DrawPolyLine newDeduction;

		private bool pointMode = true;

		private bool checkForMode = true;
	}
}
