using System;
using System.Drawing;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal class ToolPolyLine : ToolObject
	{
		public override void LoadCursor(Cursor cursor)
		{
			base.Cursor = cursor;
			base.DrawArea.Cursor = base.Cursor;
		}

		private DrawPolyLine CreateObject(int x1, int y1, int x2, int y2)
		{
			DrawPolyLine drawPolyLine = new DrawPolyLine(x1, y1, x2, y2, base.DrawArea.DrawingBoard.Origin, base.DrawArea.ToolSettings.Name, base.DrawArea.ToolSettings.GroupID, base.DrawArea.ToolSettings.Comment, base.DrawArea.ToolSettings.LineColor, base.DrawArea.ToolSettings.FillColor, base.DrawArea.ToolSettings.Pattern, base.DrawArea.ToolSettings.Opacity, base.DrawArea.ToolSettings.DrawFilled, base.DrawArea.ToolSettings.CloseFigure, base.DrawArea.ToolSettings.LineWidth)
			{
				ShowMeasure = base.DrawArea.ToolSettings.ShowMeasure,
				EndPoint = new Point(x2, y2)
			};
			drawPolyLine.SetSlopeFactor(base.DrawArea.ToolSettings.SlopeFactor);
			return drawPolyLine;
		}

		private Point AdjustLine(Point location)
		{
			if (base.DrawArea.OrthoModeIsOn && this.newPolyLine.HandleCount > 1)
			{
				Point point = (Point)this.newPolyLine.PointArray[this.newPolyLine.HandleCount - 2];
				Point startPoint = new Point(point.X - (int)base.DrawArea.DrawingBoard.Origin.X, point.Y - (int)base.DrawArea.DrawingBoard.Origin.Y);
				double num = DrawLine.Angle((double)startPoint.X, (double)startPoint.Y, (double)location.X, (double)location.Y);
				location = base.SnapPoint(startPoint, location, new Func<Point, Point, int>(this.newPolyLine.Distance2D), (int)num, 5);
			}
			return location;
		}

		private void ComputeRectangleZone()
		{
			if (this.newPolyLine.PointArray.Count == 0)
			{
				return;
			}
			Point point = new Point(((Point)this.newPolyLine.PointArray[0]).X, ((Point)this.newPolyLine.PointArray[0]).Y);
			Point point2 = new Point(this.newPolyLine.EndPoint.X, this.newPolyLine.EndPoint.Y);
			for (int i = this.newPolyLine.PointArray.Count - 1; i > 0; i--)
			{
				this.newPolyLine.PointArray.RemoveAt(i);
			}
			if (point2.X > point.X && point2.Y < point.Y)
			{
				this.newPolyLine.AddPoint(new Point(point2.X, point.Y));
				this.newPolyLine.AddPoint(new Point(point2.X, point2.Y));
				this.newPolyLine.AddPoint(new Point(point.X, point2.Y));
			}
			if (point2.X > point.X && point2.Y > point.Y)
			{
				this.newPolyLine.AddPoint(new Point(point2.X, point.Y));
				this.newPolyLine.AddPoint(new Point(point2.X, point2.Y));
				this.newPolyLine.AddPoint(new Point(point.X, point2.Y));
			}
			if (point2.X < point.X && point2.Y > point.Y)
			{
				this.newPolyLine.AddPoint(new Point(point2.X, point.Y));
				this.newPolyLine.AddPoint(new Point(point2.X, point2.Y));
				this.newPolyLine.AddPoint(new Point(point.X, point2.Y));
			}
			if (point2.X < point.X && point2.Y < point.Y)
			{
				this.newPolyLine.AddPoint(new Point(point2.X, point.Y));
				this.newPolyLine.AddPoint(new Point(point2.X, point2.Y));
				this.newPolyLine.AddPoint(new Point(point.X, point2.Y));
			}
		}

		private void ResetVariables()
		{
			this.newPolyLine = null;
			this.pointMode = true;
			this.checkForMode = true;
		}

		public override void InitializeTool(Cursor cursor)
		{
			this.LoadCursor(cursor);
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
			if (this.newPolyLine != null)
			{
				if (this.newPolyLine.PointArray.Count > 2 && (Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					this.newPolyLine.CloseFigure = true;
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						this.newPolyLine.AutoAdjust((Bitmap)base.DrawArea.DrawingBoard.Image, DrawPolyLine.AutoAdjustType.ToZone);
					}
				}
				base.DrawArea.SaveObject(this.newPolyLine);
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
				if (this.newPolyLine == null)
				{
					this.newPolyLine = this.CreateObject((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y), (int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X + 1f), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y + 1f));
					base.AddNewObject(this.newPolyLine);
					base.StartDrawing(this.newPolyLine);
					base.DrawArea.CursorRestricted = true;
					this.checkForMode = true;
					text += (base.DrawArea.ToolSettings.DrawFilled ? string.Empty : ("  |  " + Resources.Cliquez_droit_pour_compléter));
				}
				else if (this.pointMode)
				{
					Point point2 = new Point((int)((float)point.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)point.Y + base.DrawArea.DrawingBoard.Origin.Y));
					this.newPolyLine.AddPoint(point2);
					this.newPolyLine.EndPoint = point2;
					if (base.DrawArea.ToolSettings.DrawFilled)
					{
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
					else
					{
						string text3 = text;
						text = string.Concat(new string[]
						{
							text3,
							"  |  ",
							Resources.Cliquez_droit_pour_compléter,
							" (",
							Resources.Ctrl_pour_fermer_périmètre,
							" ",
							Resources.ou,
							" ",
							Resources.Ctrl_Shift_pour_fermer_et_auto_ajuster,
							")"
						});
					}
				}
				if (this.pointMode)
				{
					string text4 = text;
					text = string.Concat(new string[]
					{
						text4,
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

		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.TrackMouse(new Point(e.X, e.Y));
		}

		public override void TrackMouse(Point location)
		{
			if (this.newPolyLine == null || base.Tracking || base.DrawArea.PanningInProgress)
			{
				return;
			}
			base.Tracking = true;
			Point location2 = base.DrawArea.BackTrackMouse(location);
			if (this.checkForMode && Math.Abs(((Point)this.newPolyLine.PointArray[0]).X - (location2.X + (int)base.DrawArea.DrawingBoard.Origin.X)) > 15 && Math.Abs(((Point)this.newPolyLine.PointArray[0]).Y - (location2.Y + (int)base.DrawArea.DrawingBoard.Origin.Y)) > 15)
			{
				this.pointMode = false;
				this.checkForMode = false;
				this.newPolyLine.CloseFigure = true;
				this.newPolyLine.FigureCompleted = true;
			}
			if (this.pointMode)
			{
				Point point = this.AdjustLine(location2);
				if (!point.IsEmpty)
				{
					base.MoveHandleTo(this.newPolyLine, point, this.newPolyLine.HandleCount);
				}
			}
			else
			{
				this.newPolyLine.EndPoint = new Point((int)((float)location2.X + base.DrawArea.DrawingBoard.Origin.X), (int)((float)location2.Y + base.DrawArea.DrawingBoard.Origin.Y));
				base.DrawArea.DrawingBoard.Invalidate(this.newPolyLine.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
				this.ComputeRectangleZone();
				base.DrawArea.DrawingBoard.Invalidate(this.newPolyLine.Region((int)base.DrawArea.DrawingBoard.Origin.X, (int)base.DrawArea.DrawingBoard.Origin.Y, (float)base.DrawArea.DrawingBoard.ZoomFactor));
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
			if (this.pointMode && e.Button == MouseButtons.Right && this.newPolyLine != null && !base.DrawArea.PanningInProgress && this.newPolyLine.PointArray.Count > ((this.newPolyLine.ObjectType == "Area") ? 2 : 1))
			{
				this.newPolyLine.FigureCompleted = true;
			}
			base.OnMouseUp(e);
			if (this.pointMode && e.Button != MouseButtons.Right)
			{
				return;
			}
			if (this.newPolyLine == null)
			{
				return;
			}
			if (this.newPolyLine.FigureCompleted)
			{
				this.ReleaseTool();
			}
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				bool flag = this.newPolyLine == null;
				if (!flag)
				{
					flag = (this.newPolyLine.PointArray.Count <= 2);
				}
				if (flag)
				{
					this.newPolyLine = null;
					base.CancelDrawing();
					this.ReleaseTool();
					return;
				}
				this.newPolyLine.PointArray.RemoveAt(this.newPolyLine.PointArray.Count - 1);
				base.DrawArea.Refresh();
			}
		}

		public ToolPolyLine()
		{
		}

		private DrawPolyLine newPolyLine;

		private bool pointMode = true;

		private bool checkForMode = true;
	}
}
