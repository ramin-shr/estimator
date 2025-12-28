using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using QuoterPlan.Properties;
using QuoterPlanControls;

namespace QuoterPlan
{
	public class DrawingArea
	{
		private int ColorIndex
		{
			get
			{
				return this.colorIndex;
			}
			set
			{
				this.colorIndex = value;
			}
		}

		private int TranslateColorIndex(string objectType)
		{
			if (objectType != null)
			{
				if (objectType == "Line")
				{
					return (int)this.PerimeterColors.GetValue(this.ColorIndex);
				}
				if (objectType == "Area")
				{
					return (int)this.AreaColors.GetValue(this.ColorIndex);
				}
				if (objectType == "Perimeter")
				{
					return (int)this.PerimeterColors.GetValue(this.ColorIndex);
				}
				if (objectType == "Counter")
				{
					return (int)this.CounterColors.GetValue(this.ColorIndex);
				}
			}
			return this.ColorIndex;
		}

		public Color GetAngleDefaultColor()
		{
			return Color.FromKnownColor(KnownColor.DarkRed);
		}

		public Color GetMarkerDefaultColor()
		{
			return Color.FromKnownColor(KnownColor.Yellow);
		}

		public Color GetNoteDefaultColor()
		{
			return Color.FromKnownColor(KnownColor.Khaki);
		}

		private Color GetNextColorEx(string objectType)
		{
			this.ColorIndex++;
			if (this.ColorIndex >= 27 || this.ColorIndex < 0)
			{
				this.ColorIndex = 0;
			}
			if (this.TranslateColorIndex(objectType) == 3)
			{
				return this.GetNextColorEx(objectType);
			}
			return ColorPicker.GetStandardColor((ColorPicker.StandardColorEnum)this.TranslateColorIndex(objectType));
		}

		public bool ColorAvailable(string objectType, Color color)
		{
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (objectType == drawObject.ObjectType && color.R == drawObject.Color.R && color.G == drawObject.Color.G && color.B == drawObject.Color.B)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		public Color GetNextColor(string objectType)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.GetMarkerDefaultColor());
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (ColorPicker.IsStandardColor(drawObject.Color))
						{
							string text = string.Concat(new object[]
							{
								drawObject.Color.R,
								".",
								drawObject.Color.G,
								".",
								drawObject.Color.B
							});
							if (!arrayList.Contains(text))
							{
								arrayList.Add(text);
							}
						}
					}
				}
			}
			bool flag = arrayList.Count < 27;
			arrayList.Clear();
			arrayList = null;
			if (flag)
			{
				this.ColorIndex = -1;
				bool flag2;
				Color nextColorEx;
				do
				{
					flag2 = false;
					nextColorEx = this.GetNextColorEx(objectType);
					foreach (object obj4 in this.project.Plans.Collection)
					{
						Plan plan2 = (Plan)obj4;
						foreach (object obj5 in plan2.Layers.Collection)
						{
							Layer layer2 = (Layer)obj5;
							foreach (object obj6 in layer2.DrawingObjects.Collection)
							{
								DrawObject drawObject2 = (DrawObject)obj6;
								if (nextColorEx.R == drawObject2.Color.R && nextColorEx.G == drawObject2.Color.G && nextColorEx.B == drawObject2.Color.B)
								{
									flag2 = true;
									break;
								}
							}
						}
					}
				}
				while (flag2);
				return nextColorEx;
			}
			return this.GetNextColorEx(objectType);
		}

		private void DrawAreaIcon(DrawObject drawObject, Graphics g)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLine(new Point(3, 0), new Point(11, 0));
				graphicsPath.AddLine(new Point(11, 0), new Point(11, 4));
				graphicsPath.AddLine(new Point(11, 4), new Point(14, 4));
				graphicsPath.AddLine(new Point(14, 4), new Point(14, 8));
				graphicsPath.AddLine(new Point(14, 8), new Point(17, 8));
				graphicsPath.AddLine(new Point(17, 8), new Point(17, 15));
				graphicsPath.AddLine(new Point(17, 15), new Point(0, 15));
				graphicsPath.AddLine(new Point(0, 15), new Point(0, 4));
				graphicsPath.AddLine(new Point(0, 4), new Point(3, 4));
				graphicsPath.CloseFigure();
				DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
				if (drawPolyLine.Pattern == HatchStylePickerCombo.HatchStylePickerEnum.Solid)
				{
					g.FillPath(new SolidBrush(Color.FromArgb(170, drawPolyLine.FillColor)), graphicsPath);
				}
				else
				{
					g.FillPath(new HatchBrush((HatchStyle)drawPolyLine.Pattern, Color.FromArgb(170, drawPolyLine.FillColor), Color.White), graphicsPath);
				}
				using (Pen pen = new Pen(Color.FromArgb(255, drawObject.Color), 2f))
				{
					pen.StartCap = LineCap.Square;
					pen.Alignment = PenAlignment.Inset;
					g.DrawPath(pen, graphicsPath);
				}
			}
		}

		private void DrawPerimeterIcon(DrawObject drawObject, Graphics g)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLine(new Point(1, 4), new Point(1, 11));
				graphicsPath.AddLine(new Point(2, 4), new Point(2, 11));
				graphicsPath.CloseFigure();
				graphicsPath.AddLine(new Point(9, 5), new Point(12, 5));
				graphicsPath.AddLine(new Point(9, 6), new Point(12, 6));
				graphicsPath.CloseFigure();
				graphicsPath.AddLine(new Point(14, 8), new Point(14, 11));
				graphicsPath.AddLine(new Point(15, 8), new Point(15, 11));
				graphicsPath.CloseFigure();
				graphicsPath.AddLine(new Point(4, 13), new Point(12, 13));
				graphicsPath.AddLine(new Point(4, 14), new Point(12, 14));
				graphicsPath.CloseFigure();
				g.DrawPath(new Pen(Color.FromArgb(240, drawObject.Color), 1f), graphicsPath);
			}
			using (GraphicsPath graphicsPath2 = new GraphicsPath())
			{
				graphicsPath2.AddRectangle(new Rectangle(0, 0, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(5, 4, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(13, 4, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(0, 12, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(13, 12, 4, 4));
				g.FillPath(new SolidBrush(Color.FromArgb(200, Color.Black)), graphicsPath2);
			}
		}

		private void DrawCounterIcon(DrawObject drawObject, Graphics g)
		{
			int num = 15;
			PointF point = new PointF(8f, 7f);
			DrawCounter.CounterShapeTypeEnum shape = ((DrawCounter)drawObject).Shape;
			RectangleF rectangleF = new RectangleF(point.X - (float)(num / 2), point.Y - (float)(num / 2), (float)num, (float)num);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				switch (shape)
				{
				case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
					graphicsPath.AddRectangle(rectangleF);
					point = new PointF(point.X + 1f, point.Y + 1f);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
					rectangleF = new RectangleF(point.X - 7.75f, point.Y - 7.75f, 15.5f, 15.5f);
					graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(Rectangle.Truncate(rectangleF)));
					point = new PointF(point.X, point.Y + 1f);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
					graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Up));
					point = new PointF(point.X + 1f, point.Y + 3f);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
					graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Down));
					point = new PointF(point.X + 1f, point.Y - 2f);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
					graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Up));
					point = new PointF(point.X + 1f, point.Y + 1f);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
					graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Down));
					point = new PointF(point.X + 1f, point.Y);
					break;
				default:
					graphicsPath.AddEllipse(rectangleF);
					point = new PointF(point.X + 0.5f, point.Y + 1f);
					break;
				}
				g.FillPath(new SolidBrush(Color.FromArgb(220, drawObject.FillColor)), graphicsPath);
				g.DrawPath(new Pen(Color.FromArgb(250, Color.Black), 1f), graphicsPath);
			}
			string text = drawObject.Text;
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.Character;
				stringFormat.LineAlignment = StringAlignment.Center;
				g.DrawString(text, new Font("Tahoma", Utilities.FontSizeInPoints(6f), FontStyle.Bold), new SolidBrush(Color.FromArgb(250, Color.Black)), point, stringFormat);
			}
		}

		private void DrawDistanceIcon(DrawObject drawObject, Graphics g)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLine(new Point(4, 7), new Point(12, 7));
				graphicsPath.AddLine(new Point(4, 8), new Point(12, 8));
				g.DrawPath(new Pen(Color.FromArgb(240, drawObject.Color), 1f), graphicsPath);
			}
			using (GraphicsPath graphicsPath2 = new GraphicsPath())
			{
				graphicsPath2.AddRectangle(new Rectangle(0, 6, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(13, 6, 4, 4));
				g.FillPath(new SolidBrush(Color.FromArgb(200, Color.Black)), graphicsPath2);
			}
		}

		public void DrawObjectIcon(DrawObject drawObject, Graphics g)
		{
			string objectType;
			switch (objectType = drawObject.ObjectType)
			{
			case "Line":
				g.SmoothingMode = SmoothingMode.None;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				this.DrawDistanceIcon(drawObject, g);
				return;
			case "Rectangle":
				g.DrawImage(Resources.marker_small, Point.Empty);
				return;
			case "Area":
				g.SmoothingMode = SmoothingMode.None;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				this.DrawAreaIcon(drawObject, g);
				return;
			case "Perimeter":
				g.SmoothingMode = SmoothingMode.None;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				this.DrawPerimeterIcon(drawObject, g);
				return;
			case "Counter":
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				this.DrawCounterIcon(drawObject, g);
				return;
			case "Angle":
				g.DrawImage(Resources.angle_small, Point.Empty);
				return;
			case "Note":
				g.DrawImage(Resources.note_small, Point.Empty);
				return;
			case "Legend":
				g.DrawImage(Resources.legend_small, Point.Empty);
				break;

				return;
			}
		}

		public Image GetDrawObjectIcon(DrawObject drawObject)
		{
			Image result = null;
			string objectType;
			switch (objectType = drawObject.ObjectType)
			{
			case "Line":
				result = Resources.distance_small;
				break;
			case "Rectangle":
				result = Resources.marker_small;
				break;
			case "Area":
				result = Resources.area_small;
				break;
			case "Perimeter":
				result = Resources.perimeter_small;
				break;
			case "Counter":
				result = Resources.counter_small;
				break;
			case "Angle":
				result = Resources.angle_small;
				break;
			case "Note":
				result = Resources.note_small;
				break;
			case "Legend":
				result = Resources.legend_small;
				break;
			}
			return result;
		}

		public DrawObject FindObjectFromGroupID(DrawingObjects drawingObjects, int groupID)
		{
			for (int i = drawingObjects.Collection.Count - 1; i >= 0; i--)
			{
				DrawObject drawObject = (DrawObject)drawingObjects.Collection[i];
				if (drawObject.GroupID == groupID && !drawObject.IsDeduction())
				{
					return drawObject;
				}
			}
			return null;
		}

		public DrawObject FindObjectFromGroupID(Layer layer, int groupID)
		{
			return this.FindObjectFromGroupID(layer.DrawingObjects, groupID);
		}

		public DrawObject FindObjectFromGroupID(int groupID)
		{
			foreach (object obj in this.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				DrawObject drawObject = this.FindObjectFromGroupID(layer, groupID);
				if (drawObject != null)
				{
					return drawObject;
				}
			}
			return null;
		}

		public DrawObject FindObjectFromGroupID(Plan plan, int groupID)
		{
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				DrawObject drawObject = this.FindObjectFromGroupID(layer, groupID);
				if (drawObject != null)
				{
					return drawObject;
				}
			}
			return null;
		}

		public DrawObject FindObjectFromGroupID(Project project, int groupID)
		{
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				DrawObject drawObject = this.FindObjectFromGroupID(plan, groupID);
				if (drawObject != null)
				{
					return drawObject;
				}
			}
			return null;
		}

		public DrawObject FindObjectFromID(DrawingObjects drawingObjects, int ID)
		{
			for (int i = drawingObjects.Collection.Count - 1; i >= 0; i--)
			{
				DrawObject drawObject = (DrawObject)drawingObjects.Collection[i];
				if (drawObject.ID == ID)
				{
					return drawObject;
				}
			}
			return null;
		}

		public DrawObject FindObjectFromID(Layer layer, int ID)
		{
			return this.FindObjectFromID(layer.DrawingObjects, ID);
		}

		public DrawObject FindObjectFromID(int ID)
		{
			foreach (object obj in this.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				DrawObject drawObject = this.FindObjectFromID(layer, ID);
				if (drawObject != null)
				{
					return drawObject;
				}
			}
			return null;
		}

		public DrawObject FindObjectFromID(Plan plan, int ID)
		{
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				DrawObject drawObject = this.FindObjectFromID(layer, ID);
				if (drawObject != null)
				{
					return drawObject;
				}
			}
			return null;
		}

		public DrawObject FindObjectFromID(Project project, int ID)
		{
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				DrawObject drawObject = this.FindObjectFromID(plan, ID);
				if (drawObject != null)
				{
					return drawObject;
				}
			}
			return null;
		}

		public int FindGroupLayer(int groupID)
		{
			for (int i = 0; i < this.Layers.Count; i++)
			{
				DrawObject drawObject = this.FindObjectFromGroupID(this.Layers[i], groupID);
				if (drawObject != null)
				{
					return i;
				}
			}
			return -1;
		}

		public Layer FindGroupLayer(Plan plan, int groupID)
		{
			for (int i = 0; i < plan.Layers.Count; i++)
			{
				DrawObject drawObject = this.FindObjectFromGroupID(plan.Layers[i], groupID);
				if (drawObject != null)
				{
					return plan.Layers[i];
				}
			}
			return null;
		}

		public Layer FindLayerByName(string layerName)
		{
			int num = -1;
			return this.FindLayerByName(layerName, ref num);
		}

		public Layer FindLayerByName(string layerName, ref int layerIndex)
		{
			layerIndex = -1;
			if (this.Layers == null)
			{
				return null;
			}
			layerIndex = this.Layers.FindLayerIndex(layerName);
			if (layerIndex == -1)
			{
				return null;
			}
			return this.Layers[layerIndex];
		}

		public DrawObject FindObjectByName(string objectName, bool lookAlsoInClipboard)
		{
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (drawObject.Name == objectName && drawObject.DeductionParentID == -1)
						{
							return drawObject;
						}
					}
				}
			}
			if (lookAlsoInClipboard)
			{
				foreach (object obj4 in this.Clipboard.Objects.Collection)
				{
					DrawObject drawObject2 = (DrawObject)obj4;
					if (drawObject2.Name == objectName)
					{
						return drawObject2;
					}
				}
			}
			return null;
		}

		public DrawObject GetObjectByID(Plan plan, int objectID)
		{
			if (plan != null && objectID != -1)
			{
				foreach (object obj in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj;
					DrawObject objectByID = layer.DrawingObjects.GetObjectByID(objectID);
					if (objectByID != null)
					{
						return objectByID;
					}
				}
			}
			return null;
		}

		public string GetFreeTemplateObjectName(string prefix)
		{
			string text = "";
			return this.GetFreeTemplateObjectName(prefix, ref text);
		}

		public string GetFreeTemplateObjectName(string prefix, ref string suffix)
		{
			int num = 0;
			string arg = Utilities.StripIndexFromString(prefix) + " ";
			do
			{
				num++;
			}
			while (this.FindObjectByName(arg + num, true) != null);
			suffix = num.ToString();
			return arg + num;
		}

		public string GetFreeObjectName(string prefix)
		{
			string text = "";
			return this.GetFreeObjectName(prefix, ref text);
		}

		public string GetFreeObjectName(string prefix, ref string suffix)
		{
			int num = 0;
			string arg = prefix + " ";
			do
			{
				num++;
			}
			while (this.FindObjectByName(arg + num, true) != null);
			suffix = num.ToString();
			return arg + num;
		}

		public string GetFreeObjectName(DrawObject drawObject, ref string text)
		{
			string result = "";
			string objectType;
			switch (objectType = drawObject.ObjectType)
			{
			case "Line":
				result = this.GetFreeObjectName(Resources.Distance);
				break;
			case "Area":
				result = this.GetFreeObjectName(Resources.Surface);
				break;
			case "Perimeter":
				result = this.GetFreeObjectName(Resources.Périmètre);
				break;
			case "Counter":
				result = this.GetFreeObjectName(Resources.Compteur, ref text);
				break;
			case "Rectangle":
				result = this.GetFreeObjectName(Resources.Marqueur);
				break;
			case "Angle":
				result = this.GetFreeObjectName(Resources.Angle);
				break;
			case "Note":
				result = this.GetFreeObjectName(Resources.Note);
				break;
			case "Legend":
				result = this.GetFreeObjectName(Resources.Légende);
				break;
			}
			return result;
		}

		public bool UnitScaleIsImperial()
		{
			bool result;
			try
			{
				result = (this.ActivePlan.UnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public string ToAreaString(int value)
		{
			string result;
			try
			{
				result = this.ActivePlan.UnitScale.ToAreaString(value);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public string ToAreaStringFromUnitSystem(double value)
		{
			string result;
			try
			{
				result = this.ActivePlan.UnitScale.ToAreaStringFromUnitSystem(value, true);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public string ToLengthString(int value, bool shortFormat = false)
		{
			string result;
			try
			{
				result = this.ActivePlan.UnitScale.ToLengthString(value, shortFormat);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public string ToLengthStringFromUnitSystem(double value, bool shortFormat = false)
		{
			string result;
			try
			{
				result = this.ActivePlan.UnitScale.ToLengthStringFromUnitSystem(value, shortFormat, true, true);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public string ToAngleString(double value, DrawAngle.AngleTypeEnum angleType)
		{
			string result;
			try
			{
				result = this.ActivePlan.UnitScale.ToAngleString(value, angleType);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public string ToUnitString(int value)
		{
			return value.ToString() + " " + ((value > 1) ? Resources.Unités : Resources.Unité);
		}

		public string ToCurrency(double value)
		{
			return string.Format("{0:C}", value);
		}

		private void ScaleCheckWarnings()
		{
			if (this.DrawingBoard.OriginalDpiX != this.DrawingBoard.OriginalDpiY)
			{
				string title = Resources.Inconsistance_entre_le_DPI_horitonzal_et_le_DPI_vertical_détectée;
				string text = string.Format(Resources.Fonction_de_sélection_d_échelle_standard_désactivée, this.UnitScale.ReferenceDpiX, this.UnitScale.ReferenceDpiY);
				if (this.UnitScale.Scale == 0f)
				{
					text = text + "\n" + Resources.Veuillez_utiliser_l_ajustement_manuel_pour_spécifier_l_échelle_appropriée;
				}
				Utilities.DisplayError(title, text);
				this.UnitScale.StandardScaleDisable = true;
			}
			if (this.DrawingBoard.OriginalDpiX == 0f)
			{
				string title = Resources.DPI_horitonzal_invalide;
				string text = Resources.Valeur_de_référence_du_DPI_horitantal_égale_à_0;
				if (this.UnitScale.Scale == 0f)
				{
					text = text + "\n" + Resources.Veuillez_utiliser_l_ajustement_manuel_pour_spécifier_l_échelle_appropriée;
				}
				Utilities.DisplayError(title, text);
				this.UnitScale.StandardScaleDisable = true;
			}
			if (Math.Ceiling((double)this.DrawingBoard.OriginalDpiX) < 96.0)
			{
				if (this.UnitScale.Scale == 0f)
				{
					string title = Resources.DPI_horitonzal_invalide;
					string text = Resources.Valeur_de_référence_du_DPI_est_trop_petite;
					text = text + "\n" + Resources.Veuillez_utiliser_l_ajustement_manuel_pour_spécifier_l_échelle_appropriée;
					Utilities.DisplayError(title, text);
				}
				this.UnitScale.StandardScaleDisable = true;
			}
			if (this.UnitScale.StandardScaleDisable && !this.UnitScale.SetManually)
			{
				this.UnitScale.SetScale(0f, this.UnitScale.CurrentSystemType, this.UnitScale.Precision, false);
			}
		}

		public void ScaleValidate()
		{
			if (this.UnitScale.Scale == 0f)
			{
				string échelle_non_configurée = Resources.Échelle_non_configurée;
				string l_échelle_pour_ce_plan_n_est_pas_encore_configurée = Resources.L_échelle_pour_ce_plan_n_est_pas_encore_configurée;
				Utilities.DisplayWarning(échelle_non_configurée, l_échelle_pour_ce_plan_n_est_pas_encore_configurée);
			}
			this.ScaleCheckWarnings();
		}

		public void ScaleWasSetManually(int referenceDistance)
		{
			this.UnitScale.ReferenceDistance = referenceDistance;
			this.ScaleSet(true);
		}

		public void ScaleSetManually()
		{
			string pour_ajuster_l_échelle_manuellement = Resources.Pour_ajuster_l_échelle_manuellement;
			string identifiez_une_mesure_linéaire_sur_le_plan = Resources.Identifiez_une_mesure_linéaire_sur_le_plan;
			Utilities.DisplayMessage(pour_ajuster_l_échelle_manuellement, identifiez_une_mesure_linéaire_sur_le_plan);
			this.UnitScale.MustSetManually = true;
			this.SelectScaleTool();
		}

		public void ScaleSet(bool bMustSetManually)
		{
			this.UnitScale.MustSetManually = bMustSetManually;
			try
			{
				using (ScaleForm scaleForm = new ScaleForm(this.UnitScale))
				{
					scaleForm.HelpUtilities = this.Owner.HelpUtilities;
					scaleForm.HelpContextString = "ScaleForm";
					scaleForm.ShowDialog(this.Owner);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return;
			}
			if (this.UnitScale.Dirty)
			{
				if (this.UnitScale.MustSetManually && !bMustSetManually)
				{
					this.ScaleSetManually();
					return;
				}
				this.Owner.UpdateScaleSystemInGUI(true);
				this.Owner.SetModified();
				if (!this.UnitScale.MustSetManually)
				{
					string note_importante = Resources.Note_importante;
					string certains_plans_peuvent_avoir_perdu_leur_intégrité = Utilities.Certains_plans_peuvent_avoir_perdu_leur_intégrité;
					Utilities.DisplayMessageCustom(note_importante, certains_plans_peuvent_avoir_perdu_leur_intégrité, null, Resources.Je_comprends);
				}
			}
		}

		public bool ValidateName(ref string name, ref string title, ref string errorMessage, int maxLength)
		{
			name = Utilities.Substring(name, 0, maxLength);
			if (!Utilities.ValidateVariableName(name, ""))
			{
				if (name != "")
				{
					title = Resources.Nom_invalide;
					errorMessage = Resources.Les_caractères_suivants_sont_invalides + "\n" + Utilities.InvalidCharacters();
				}
				return false;
			}
			return true;
		}

		public bool ValidateDrawObjectName(ref string name, ref string title, ref string errorMessage)
		{
			if (!this.ValidateName(ref name, ref title, ref errorMessage, 50))
			{
				return false;
			}
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (drawObject.Name == name)
						{
							title = Resources.Nom_déjà_utilisé;
							errorMessage = Resources.Ce_nom_est_déjà_utilisé_par_un_autre_objet;
							return false;
						}
					}
				}
			}
			return true;
		}

		public bool ValidateLayerName(ref string name, ref string title, ref string errorMessage)
		{
			if (!this.ValidateName(ref name, ref title, ref errorMessage, 50))
			{
				return false;
			}
			if (this.ActivePlan.Layers.NameExists(name))
			{
				title = Resources.Nom_déjà_utilisé;
				errorMessage = Resources.Ce_nom_est_déjà_utilisé_par_un_autre_calque;
				return false;
			}
			return true;
		}

		public bool ValidatePlanName(ref string name, ref string title, ref string errorMessage)
		{
			if (!this.ValidateName(ref name, ref title, ref errorMessage, 50))
			{
				return false;
			}
			if (this.project.Plans.FindPlan(name) != null)
			{
				title = Resources.Nom_déjà_utilisé;
				errorMessage = Resources.Ce_nom_est_déjà_utilisé_par_un_autre_plan;
				return false;
			}
			return true;
		}

		public void MapSelectedObjectsToGroup(DrawingObjects drawingObjects, DrawObject groupObject, int groupID, bool changeGroup)
		{
			string name = "";
			Color color = groupObject.Color;
			int groupID2 = groupObject.GroupID;
			if (changeGroup)
			{
				name = this.GetFreeTemplateObjectName(groupObject.Name);
				color = this.GetNextColor(groupObject.ObjectType);
			}
			bool flag = true;
			for (int i = 0; i < drawingObjects.SelectionCount; i++)
			{
				DrawObject selectedObject = drawingObjects.GetSelectedObject(i);
				if (selectedObject.GroupID == groupID2)
				{
					selectedObject.GroupID = groupID;
					if (changeGroup)
					{
						selectedObject.Name = name;
						selectedObject.Color = color;
						selectedObject.FillColor = color;
						selectedObject.Group = null;
						DrawObjectGroup group = selectedObject.Group;
						DrawObjectGroup drawObjectGroup = this.Project.Groups.FindFromGroupID((groupID2 < -1) ? ((groupID2 + 2) * -1) : groupID2);
						if (drawObjectGroup != null && flag)
						{
							foreach (object obj in drawObjectGroup.Presets.Collection)
							{
								Preset preset = (Preset)obj;
								Preset preset2 = preset.Clone(false);
								preset2.ID = Guid.NewGuid().ToString();
								preset2.SynchWithTemplate(this.Project.ExtensionsSupport);
								group.Presets.Add(preset2);
							}
							foreach (CEstimatingItem cestimatingItem in drawObjectGroup.EstimatingItems.Collection)
							{
								group.EstimatingItems.Add(cestimatingItem.Clone(true));
							}
							foreach (CEstimatingItem cestimatingItem2 in drawObjectGroup.COfficeProducts.Collection)
							{
								group.EstimatingItems.Add(cestimatingItem2.Clone(false));
							}
							this.project.EstimatingItems.CloneEstimatingItemsPrices(drawObjectGroup, group);
							flag = false;
						}
					}
					if (selectedObject.ObjectType == "Area" || selectedObject.ObjectType == "Perimeter")
					{
						DrawPolyLine drawPolyLine = (DrawPolyLine)selectedObject;
						foreach (object obj2 in drawPolyLine.DeductionArray)
						{
							DrawObject drawObject = (DrawObject)obj2;
							drawObject.GroupID = selectedObject.GroupID;
							drawObject.Opacity = selectedObject.Opacity;
							drawObject.DeductionParentID = selectedObject.ID;
							drawObject.SetSlopeFactor(selectedObject.SlopeFactor);
						}
					}
				}
			}
		}

		public void ToggleMeasures()
		{
			bool flag = true;
			bool flag2 = false;
			if (this.ActivePlan == null)
			{
				return;
			}
			foreach (object obj in this.ActivePlan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj2;
					if (drawObject.IsPartOfGroup() && drawObject.ShowMeasure)
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				using (IEnumerator enumerator3 = this.ActivePlan.Layers.Collection.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						object obj3 = enumerator3.Current;
						Layer layer2 = (Layer)obj3;
						foreach (object obj4 in layer2.DrawingObjects.Collection)
						{
							DrawObject drawObject2 = (DrawObject)obj4;
							if (drawObject2.IsPartOfGroup())
							{
								drawObject2.MeasureWasInvisible = !drawObject2.ShowMeasure;
							}
						}
					}
					goto IL_1F9;
				}
			}
			foreach (object obj5 in this.ActivePlan.Layers.Collection)
			{
				Layer layer3 = (Layer)obj5;
				foreach (object obj6 in layer3.DrawingObjects.Collection)
				{
					DrawObject drawObject3 = (DrawObject)obj6;
					if (drawObject3.IsPartOfGroup())
					{
						flag2 = (flag2 || !drawObject3.MeasureWasInvisible);
					}
				}
			}
			IL_1F9:
			foreach (object obj7 in this.ActivePlan.Layers.Collection)
			{
				Layer layer4 = (Layer)obj7;
				foreach (object obj8 in layer4.DrawingObjects.Collection)
				{
					DrawObject drawObject4 = (DrawObject)obj8;
					if (drawObject4.IsPartOfGroup())
					{
						if (!flag)
						{
							drawObject4.ShowMeasure = false;
						}
						else
						{
							drawObject4.ShowMeasure = (!flag2 || !drawObject4.MeasureWasInvisible);
						}
						if (drawObject4.GroupID == this.toolSettings.GroupID)
						{
							this.toolSettings.ShowMeasure = drawObject4.ShowMeasure;
						}
					}
				}
			}
		}

		public event OnObjectsSelectedHandler OnObjectsSelected
		{
			add
			{
				OnObjectsSelectedHandler onObjectsSelectedHandler = this.OnObjectsSelected;
				OnObjectsSelectedHandler onObjectsSelectedHandler2;
				do
				{
					onObjectsSelectedHandler2 = onObjectsSelectedHandler;
					OnObjectsSelectedHandler value2 = (OnObjectsSelectedHandler)Delegate.Combine(onObjectsSelectedHandler2, value);
					onObjectsSelectedHandler = Interlocked.CompareExchange<OnObjectsSelectedHandler>(ref this.OnObjectsSelected, value2, onObjectsSelectedHandler2);
				}
				while (onObjectsSelectedHandler != onObjectsSelectedHandler2);
			}
			remove
			{
				OnObjectsSelectedHandler onObjectsSelectedHandler = this.OnObjectsSelected;
				OnObjectsSelectedHandler onObjectsSelectedHandler2;
				do
				{
					onObjectsSelectedHandler2 = onObjectsSelectedHandler;
					OnObjectsSelectedHandler value2 = (OnObjectsSelectedHandler)Delegate.Remove(onObjectsSelectedHandler2, value);
					onObjectsSelectedHandler = Interlocked.CompareExchange<OnObjectsSelectedHandler>(ref this.OnObjectsSelected, value2, onObjectsSelectedHandler2);
				}
				while (onObjectsSelectedHandler != onObjectsSelectedHandler2);
			}
		}

		public event OnDeductionsParentSetHandler OnDeductionsParentSet
		{
			add
			{
				OnDeductionsParentSetHandler onDeductionsParentSetHandler = this.OnDeductionsParentSet;
				OnDeductionsParentSetHandler onDeductionsParentSetHandler2;
				do
				{
					onDeductionsParentSetHandler2 = onDeductionsParentSetHandler;
					OnDeductionsParentSetHandler value2 = (OnDeductionsParentSetHandler)Delegate.Combine(onDeductionsParentSetHandler2, value);
					onDeductionsParentSetHandler = Interlocked.CompareExchange<OnDeductionsParentSetHandler>(ref this.OnDeductionsParentSet, value2, onDeductionsParentSetHandler2);
				}
				while (onDeductionsParentSetHandler != onDeductionsParentSetHandler2);
			}
			remove
			{
				OnDeductionsParentSetHandler onDeductionsParentSetHandler = this.OnDeductionsParentSet;
				OnDeductionsParentSetHandler onDeductionsParentSetHandler2;
				do
				{
					onDeductionsParentSetHandler2 = onDeductionsParentSetHandler;
					OnDeductionsParentSetHandler value2 = (OnDeductionsParentSetHandler)Delegate.Remove(onDeductionsParentSetHandler2, value);
					onDeductionsParentSetHandler = Interlocked.CompareExchange<OnDeductionsParentSetHandler>(ref this.OnDeductionsParentSet, value2, onDeductionsParentSetHandler2);
				}
				while (onDeductionsParentSetHandler != onDeductionsParentSetHandler2);
			}
		}

		public event OnDeductionsParentReleaseHandler OnDeductionsParentRelease
		{
			add
			{
				OnDeductionsParentReleaseHandler onDeductionsParentReleaseHandler = this.OnDeductionsParentRelease;
				OnDeductionsParentReleaseHandler onDeductionsParentReleaseHandler2;
				do
				{
					onDeductionsParentReleaseHandler2 = onDeductionsParentReleaseHandler;
					OnDeductionsParentReleaseHandler value2 = (OnDeductionsParentReleaseHandler)Delegate.Combine(onDeductionsParentReleaseHandler2, value);
					onDeductionsParentReleaseHandler = Interlocked.CompareExchange<OnDeductionsParentReleaseHandler>(ref this.OnDeductionsParentRelease, value2, onDeductionsParentReleaseHandler2);
				}
				while (onDeductionsParentReleaseHandler != onDeductionsParentReleaseHandler2);
			}
			remove
			{
				OnDeductionsParentReleaseHandler onDeductionsParentReleaseHandler = this.OnDeductionsParentRelease;
				OnDeductionsParentReleaseHandler onDeductionsParentReleaseHandler2;
				do
				{
					onDeductionsParentReleaseHandler2 = onDeductionsParentReleaseHandler;
					OnDeductionsParentReleaseHandler value2 = (OnDeductionsParentReleaseHandler)Delegate.Remove(onDeductionsParentReleaseHandler2, value);
					onDeductionsParentReleaseHandler = Interlocked.CompareExchange<OnDeductionsParentReleaseHandler>(ref this.OnDeductionsParentRelease, value2, onDeductionsParentReleaseHandler2);
				}
				while (onDeductionsParentReleaseHandler != onDeductionsParentReleaseHandler2);
			}
		}

		public DrawingArea()
		{
		}

		public ToolSettings ToolSettings
		{
			get
			{
				return this.toolSettings;
			}
		}

		public bool DrawingInProgress
		{
			get
			{
				return this._drawingInProgress;
			}
			set
			{
				this._drawingInProgress = value;
			}
		}

		public bool PointerInProgress
		{
			get
			{
				return this._pointerInProgress;
			}
			set
			{
				this._pointerInProgress = value;
			}
		}

		public bool PanningInProgress
		{
			get
			{
				return this._panningInProgress;
			}
			set
			{
				this._panningInProgress = value;
			}
		}

		public bool PanningFromPanTool
		{
			get
			{
				return this._panningInProgress && this.ActiveTool == DrawingArea.DrawToolType.Pan;
			}
		}

		public bool NetSelectionInProgress
		{
			get
			{
				return this._netSelectionInProgress;
			}
			set
			{
				this._netSelectionInProgress = value;
			}
		}

		public DrawObject CurrentlyCreatedObject
		{
			get
			{
				return this._currentlyCreatedObject;
			}
			set
			{
				this._currentlyCreatedObject = value;
			}
		}

		public DrawObject CurrentlyResizedObject
		{
			get
			{
				return this._currentlyResizedObject;
			}
			set
			{
				this._currentlyResizedObject = value;
			}
		}

		public FillBrushes.BrushType BrushType
		{
			get
			{
				return this._brushType;
			}
			set
			{
				this._brushType = value;
			}
		}

		public Brush CurrentBrush
		{
			get
			{
				return this._currentBrush;
			}
			set
			{
				this._currentBrush = value;
			}
		}

		public DrawingPens.PenType PenType
		{
			get
			{
				return this._penType;
			}
			set
			{
				this._penType = value;
			}
		}

		public Pen CurrentPen
		{
			get
			{
				return this._currentPen;
			}
			set
			{
				this._currentPen = value;
			}
		}

		public float Rotation
		{
			get
			{
				return this._rotation;
			}
			set
			{
				this._rotation = value;
			}
		}

		public float ZoomFactor
		{
			get
			{
				return (float)this.DrawingBoard.ZoomFactor;
			}
		}

		public int Zoom
		{
			get
			{
				return this.DrawingBoard.Zoom;
			}
			set
			{
				this.DrawingBoard.Zoom = value;
			}
		}

		public Rectangle NetRectangle
		{
			get
			{
				return this.netRectangle;
			}
			set
			{
				this.netRectangle = value;
			}
		}

		public bool DrawNetRectangle
		{
			get
			{
				return this.drawNetRectangle;
			}
			set
			{
				this.drawNetRectangle = value;
			}
		}

		public MainForm Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		public DrawingBoard DrawingBoard
		{
			get
			{
				return this.drawingBoard;
			}
			set
			{
				this.drawingBoard = value;
			}
		}

		public DrawingArea.DrawToolType ActiveTool
		{
			get
			{
				return this.activeTool;
			}
			set
			{
				this.activeTool = value;
			}
		}

		public bool SuspendScrolling
		{
			get
			{
				return this.suspendScrolling;
			}
			set
			{
				this.suspendScrolling = value;
			}
		}

		public CommandChangeState ActionCommand
		{
			get
			{
				return this.actionCommand;
			}
			set
			{
				this.actionCommand = value;
			}
		}

		public bool DeductionWasChanged
		{
			get
			{
				return this.deductionWasChanged;
			}
			set
			{
				this.deductionWasChanged = value;
			}
		}

		public DrawPolyLine DeductionParent
		{
			get
			{
				return this.deductionParent;
			}
		}

		public UnitScale UnitScale
		{
			get
			{
				UnitScale result;
				try
				{
					result = this.ActivePlan.UnitScale;
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public bool Capture
		{
			get
			{
				return this.DrawingBoard.Capture;
			}
			set
			{
				this.DrawingBoard.Capture = value;
			}
		}

		public Cursor Cursor
		{
			get
			{
				return this.DrawingBoard.Cursor;
			}
			set
			{
				this.DrawingBoard.Cursor = value;
			}
		}

		public Rectangle ClientRectangle
		{
			get
			{
				return this.DrawingBoard.ClientRectangle;
			}
		}

		public Size ClientSize
		{
			get
			{
				return this.DrawingBoard.ClientSize;
			}
		}

		public Clipboard Clipboard
		{
			get
			{
				return this.clipboard;
			}
		}

		public Project Project
		{
			get
			{
				return this.project;
			}
			set
			{
				this.project = value;
			}
		}

		public Plan ActivePlan
		{
			get
			{
				return this.currentPlan;
			}
			set
			{
				this.currentPlan = value;
			}
		}

		public void Initialize(MainForm owner, Project project, DrawingBoard drawingBoard)
		{
			this.Project = project;
			this.Owner = owner;
			this.DrawingBoard = drawingBoard;
			this.activeTool = DrawingArea.DrawToolType.Pointer;
			this.tools = new Tool[12];
			this.tools[0] = new ToolPointer();
			this.tools[1] = new ToolRectangle();
			this.tools[2] = new ToolLine();
			this.tools[3] = new ToolPolyLine();
			this.tools[4] = new ToolDeduction();
			this.tools[5] = new ToolOpening();
			this.tools[6] = new ToolCounter();
			this.tools[7] = new ToolAngle();
			this.tools[8] = new ToolNote();
			this.tools[9] = new ToolPan();
			this.tools[10] = new ToolScale();
			this.tools[11] = new ToolRectangle();
			this.tools[0].DrawArea = this;
			this.tools[1].DrawArea = this;
			this.tools[2].DrawArea = this;
			this.tools[3].DrawArea = this;
			this.tools[4].DrawArea = this;
			this.tools[5].DrawArea = this;
			this.tools[6].DrawArea = this;
			this.tools[7].DrawArea = this;
			this.tools[8].DrawArea = this;
			this.tools[9].DrawArea = this;
			this.tools[10].DrawArea = this;
			this.tools[11].DrawArea = this;
		}

		public int GetNextObjectID()
		{
			this.project.ObjectCounter++;
			return this.project.ObjectCounter;
		}

		public int GetNextGroupID()
		{
			this.project.GroupCounter++;
			return this.project.GroupCounter;
		}

		public Layers Layers
		{
			get
			{
				Layers result;
				try
				{
					result = this.ActivePlan.Layers;
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public Layer ActiveLayer
		{
			get
			{
				Layer result;
				try
				{
					result = this.Layers[this.Layers.ActiveLayerIndex];
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public int ActiveLayerIndex
		{
			get
			{
				int result;
				try
				{
					result = this.Layers.ActiveLayerIndex;
				}
				catch
				{
					result = 0;
				}
				return result;
			}
			set
			{
				try
				{
					this.Layers.SetActiveLayer(value);
				}
				catch
				{
				}
			}
		}

		public string ActiveLayerName
		{
			get
			{
				string result;
				try
				{
					result = this.ActiveLayer.Name;
				}
				catch
				{
					result = string.Empty;
				}
				return result;
			}
		}

		public Layer GetLayer(int index)
		{
			Layer result;
			try
			{
				result = this.Layers[index];
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DrawingObjects ActiveDrawingObjects
		{
			get
			{
				DrawingObjects result;
				try
				{
					result = this.Layers[this.Layers.ActiveLayerIndex].DrawingObjects;
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public int ActiveLayerOpacity
		{
			get
			{
				int result;
				try
				{
					result = this.Layers[this.Layers.ActiveLayerIndex].Opacity;
				}
				catch
				{
					result = 150;
				}
				return result;
			}
		}

		public int SelectionCount
		{
			get
			{
				int result;
				try
				{
					result = this.ActiveDrawingObjects.SelectionCount;
				}
				catch
				{
					result = 0;
				}
				return result;
			}
		}

		public DrawObject GetSelectedObject(int index)
		{
			DrawObject result;
			try
			{
				result = this.ActiveDrawingObjects.GetSelectedObject(index);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DrawObject SelectedObject
		{
			get
			{
				DrawObject result;
				try
				{
					result = this.ActiveDrawingObjects.GetSelectedObject(0);
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public DrawLegend SelectedLegend
		{
			get
			{
				DrawLegend result;
				try
				{
					DrawObject selectedObject = this.SelectedObject;
					if (selectedObject.ObjectType == "Legend")
					{
						result = (DrawLegend)selectedObject;
					}
					else
					{
						result = null;
					}
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public DrawPolyLine SelectedPolyLine
		{
			get
			{
				DrawPolyLine result;
				try
				{
					DrawObject selectedObject = this.SelectedObject;
					if (selectedObject.ObjectType == "Area" || selectedObject.ObjectType == "Perimeter")
					{
						result = (DrawPolyLine)selectedObject;
					}
					else
					{
						result = null;
					}
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public DrawPolyLine FirstSelectedArea
		{
			get
			{
				DrawPolyLine result;
				try
				{
					DrawObject drawObject = null;
					for (int i = 0; i < this.SelectionCount; i++)
					{
						if (this.GetSelectedObject(i).ObjectType == "Area")
						{
							drawObject = this.GetSelectedObject(i);
						}
					}
					result = (DrawPolyLine)drawObject;
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public DrawPolyLine SelectedArea
		{
			get
			{
				DrawPolyLine result;
				try
				{
					DrawObject selectedObject = this.SelectedObject;
					if (selectedObject.ObjectType == "Area")
					{
						result = (DrawPolyLine)selectedObject;
					}
					else
					{
						result = null;
					}
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public DrawPolyLine SelectedPerimeter
		{
			get
			{
				DrawPolyLine result;
				try
				{
					DrawObject selectedObject = this.SelectedObject;
					if (selectedObject.ObjectType == "Perimeter")
					{
						result = (DrawPolyLine)selectedObject;
					}
					else
					{
						result = null;
					}
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public Layer GetObjectLayer(DrawObject drawObject)
		{
			Layer result;
			try
			{
				foreach (object obj in this.Layers.Collection)
				{
					Layer layer = (Layer)obj;
					foreach (object obj2 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject2 = (DrawObject)obj2;
						if (drawObject2.HasSameGroupOrID(drawObject))
						{
							return layer;
						}
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DrawObjectGroup GetObjectGroup(DrawObject drawObject)
		{
			if (drawObject.GroupID < 0)
			{
				return null;
			}
			foreach (object obj in this.project.Groups.Collection)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				if (drawObjectGroup.ID == drawObject.GroupID)
				{
					return drawObjectGroup;
				}
			}
			DrawObjectGroup drawObjectGroup2 = new DrawObjectGroup(drawObject.GroupID, drawObject.Name, drawObject.ObjectType, "");
			this.project.Groups.Add(drawObjectGroup2);
			return drawObjectGroup2;
		}

		public Preset GetGroupSelectedPreset(DrawObject drawObject)
		{
			if (this.project.GroupCounter < 1)
			{
				return null;
			}
			DrawObjectGroup group = drawObject.Group;
			if (group == null)
			{
				return null;
			}
			return group.SelectedPreset;
		}

		public void SetGroupSelectedPreset(DrawObject drawObject, Preset preset)
		{
			if (this.project.GroupCounter < 1)
			{
				return;
			}
			DrawObjectGroup group = drawObject.Group;
			if (group == null)
			{
				return;
			}
			group.SelectedPreset = preset;
		}

		public void DrawLayers(object sender, Graphics g, Size clientSize, int offsetX, int offsetY, float zoom, bool drawText, bool printToScreen, MainForm.ImageQualityEnum imageQuality, float defaultFontSize = 12f)
		{
			if (this.Layers == null)
			{
				return;
			}
			this.DrawLayers(sender, this.ActivePlan, g, clientSize, offsetX, offsetY, zoom, drawText, printToScreen, imageQuality, defaultFontSize);
		}

		public void DrawLayers(object sender, Plan plan, Graphics g, Size clientSize, int offsetX, int offsetY, float zoom, bool drawText, bool printToScreen, MainForm.ImageQualityEnum imageQuality, float defaultFontSize = 12f)
		{
			if (plan == null)
			{
				return;
			}
			Plan activePlan = this.ActivePlan;
			this.ActivePlan = plan;
			Matrix transform = g.Transform;
			Matrix matrix = new Matrix();
			matrix.Translate((float)(-(float)clientSize.Width) / 2f, (float)(-(float)clientSize.Height) / 2f, MatrixOrder.Append);
			matrix.Rotate(this._rotation, MatrixOrder.Append);
			matrix.Translate((float)clientSize.Width / 2f, (float)clientSize.Height / 2f, MatrixOrder.Append);
			matrix.Scale(zoom, zoom, MatrixOrder.Append);
			if (sender.GetType().Name == "MainControl")
			{
				matrix.Translate((float)this.drawingBoard.HorizontalOffset, (float)this.drawingBoard.VerticalOffset, MatrixOrder.Append);
			}
			g.Transform = matrix;
			if (plan.Layers != null)
			{
				int count = plan.Layers.Count;
				for (int i = 0; i < count; i++)
				{
					if (plan.Layers[i].DrawingObjects != null)
					{
						plan.Layers[i].DrawingObjects.Draw(sender, g, offsetX, offsetY, plan.Layers[i].Visible, printToScreen, imageQuality);
						if (plan.Layers[i].Visible && drawText)
						{
							plan.Layers[i].DrawingObjects.DrawText(sender, g, offsetX, offsetY, printToScreen, imageQuality, defaultFontSize);
						}
					}
				}
			}
			this.DrawNetSelection(g, offsetX, offsetY);
			this.ActivePlan = activePlan;
			g.Transform = transform;
		}

		private void DrawNetSelection(Graphics g, int offsetX, int offsetY)
		{
			if (!this.DrawNetRectangle)
			{
				return;
			}
			Rectangle rect = this.NetRectangle;
			rect.X += (int)this.DrawingBoard.Origin.X - offsetX;
			rect.Y += (int)this.DrawingBoard.Origin.Y - offsetY;
			Pen pen = new Pen(Color.FromArgb(255, Color.Orange), 1f);
			Brush brush = new SolidBrush(Color.FromArgb(50, Color.Orange));
			g.DrawRectangle(pen, rect);
			g.FillRectangle(brush, rect);
			brush.Dispose();
			pen.Dispose();
		}

		public void Clear()
		{
			this.colorIndex = 26;
			this.clipboard.Clear();
			this.clipboard.SelectedOpening = null;
		}

		public void UpdateSelectedObjects()
		{
			if (this.Layers == null)
			{
				return;
			}
			if (this.OnObjectsSelected != null)
			{
				this.OnObjectsSelected();
			}
		}

		public void SaveObject(DrawObject drawObject)
		{
			drawObject.Normalize();
			if (drawObject.DeductionParentID == -1)
			{
				this.AddCommandToHistory(new CommandAdd(this, this.ActiveLayerName, drawObject));
				this.Owner.AddObjectToGUI(this.ActiveLayerIndex, drawObject, true);
				this.Owner.RefreshObject(drawObject);
			}
			else
			{
				this.Owner.SetModified();
				this.Owner.EnableEditCommands(true);
			}
			if (this.activeTool == DrawingArea.DrawToolType.Deduction || this.activeTool == DrawingArea.DrawToolType.Opening || this.activeTool == DrawingArea.DrawToolType.Note)
			{
				this.SelectPointerTool();
			}
			this.Refresh();
		}

		public void SetObjectPropertiesFromGroup(DrawObject drawObject)
		{
			if (drawObject.IsPartOfGroup())
			{
				if (!drawObject.IsDeduction())
				{
					DrawObject drawObject2 = this.FindObjectFromGroupID(this.project, drawObject.GroupID);
					if (drawObject2 != null)
					{
						drawObject.Name = drawObject2.Name;
						drawObject.Color = drawObject2.Color;
						drawObject.FillColor = drawObject2.FillColor;
						drawObject.Comment = drawObject2.Comment;
						drawObject.Opacity = drawObject2.Opacity;
						drawObject.PenWidth = drawObject2.PenWidth;
						drawObject.ShowMeasure = drawObject2.ShowMeasure;
						drawObject.Visible = drawObject2.Visible;
						drawObject.SetSlopeFactor(drawObject2.SlopeFactor);
					}
					else if (this.toolSettings.GroupID == drawObject.GroupID && !this.toolSettings.IsDeduction)
					{
						drawObject.Name = this.toolSettings.Name;
						drawObject.Color = this.toolSettings.LineColor;
						drawObject.FillColor = this.toolSettings.FillColor;
						drawObject.Comment = this.toolSettings.Comment;
						drawObject.Opacity = this.toolSettings.Opacity;
						drawObject.PenWidth = this.toolSettings.LineWidth;
						drawObject.ShowMeasure = this.toolSettings.ShowMeasure;
						drawObject.Visible = this.toolSettings.Visible;
						drawObject.SetSlopeFactor(this.toolSettings.SlopeFactor);
					}
					if (!(drawObject.ObjectType == "Area") && !(drawObject.ObjectType == "Perimeter"))
					{
						return;
					}
					using (IEnumerator enumerator = ((DrawPolyLine)drawObject).DeductionArray.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DrawObject drawObject3 = (DrawObject)obj;
							drawObject3.GroupID = drawObject.GroupID;
							drawObject3.Opacity = drawObject.Opacity;
							drawObject3.DeductionParentID = drawObject.ID;
							drawObject3.SetSlopeFactor(drawObject.SlopeFactor);
						}
						return;
					}
				}
				Console.WriteLine("Deduction");
			}
		}

		public void FlagDeletedGroups()
		{
			this.project.FlagDeletedGroups();
		}

		public void Refresh()
		{
			this.DrawingBoard.Invalidate();
		}

		public void UpdateStatusBar(string status)
		{
			this.Owner.UpdateStatusBar(status);
		}

		public void UnselectLegend()
		{
			if (this.ActivePlan == null)
			{
				return;
			}
			foreach (object obj in this.ActivePlan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj2;
					if (drawObject.ObjectType == "Legend")
					{
						((DrawLegend)drawObject).Selected = false;
						return;
					}
				}
			}
		}

		public void UpdateLegend()
		{
			if (this.ActivePlan == null)
			{
				return;
			}
			foreach (object obj in this.ActivePlan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj2;
					if (drawObject.ObjectType == "Legend")
					{
						((DrawLegend)drawObject).RecalcLayout();
						return;
					}
				}
			}
		}

		public void ValidateLegend(ExtensionsSupport extensionsSupport)
		{
			if (this.ActivePlan == null)
			{
				return;
			}
			try
			{
				foreach (object obj in this.ActivePlan.Layers.Collection)
				{
					Layer layer = (Layer)obj;
					foreach (object obj2 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj2;
						if (drawObject.ObjectType == "Legend")
						{
							return;
						}
					}
				}
				DrawLegend o = new DrawLegend(this.ActivePlan, extensionsSupport, 0, 0, Resources.Légende, Color.WhiteSmoke, 6, DrawLegend.DefaultFontSize, DrawLegend.DefaultMaxRows)
				{
					MustSetLocation = true,
					ShowMeasure = true,
					Visible = true
				};
				this.InsertObject(o, 0, false, false);
			}
			catch
			{
			}
		}

		public void ValidateLegends(ExtensionsSupport extensionsSupport)
		{
			Plan activePlan = this.ActivePlan;
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				this.ActivePlan = plan;
				if (plan.Layers.Count == 0)
				{
					plan.CreateDefaultLayers();
				}
				this.ValidateLegend(extensionsSupport);
			}
			this.ActivePlan = activePlan;
		}

		public bool CanRedo
		{
			get
			{
				bool result;
				try
				{
					result = this.ActivePlan.CanRedo;
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		public bool CanUndo
		{
			get
			{
				bool result;
				try
				{
					result = this.ActivePlan.CanUndo;
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		public void ClearHistory()
		{
			if (this.ActivePlan != null)
			{
				this.ActivePlan.ClearHistory();
			}
		}

		public void AddCommandToHistory(Command command)
		{
			if (this.ActivePlan != null)
			{
				this.ActivePlan.AddCommandToHistory(command);
			}
		}

		public void RenameLayerFromUndoManager(string oldName, string newName)
		{
			if (this.ActivePlan != null)
			{
				this.ActivePlan.RenameLayerFromUndoManager(oldName, newName);
			}
		}

		public void Undo()
		{
			if (this.ActivePlan != null)
			{
				this.ActivePlan.Undo();
				this.Refresh();
			}
		}

		public void Redo()
		{
			if (this.ActivePlan != null)
			{
				this.ActivePlan.Redo();
				this.Refresh();
			}
		}

		private void InitializeTool(DrawingArea.DrawToolType toolType, Cursor cursor)
		{
			this.SuspendScrolling = false;
			this.DrawingInProgress = false;
			this.PanningInProgress = false;
			this.NetSelectionInProgress = false;
			this.CursorRestricted = false;
			this.CurrentlyCreatedObject = null;
			this.CurrentlyResizedObject = null;
			this.ActiveTool = toolType;
			this.UpdateStatusBar(string.Empty);
			this.tools[(int)this.ActiveTool].InitializeTool(cursor);
		}

		public void SelectPointerTool()
		{
			this.InitializeTool(DrawingArea.DrawToolType.Pointer, Cursors.Default);
			((ToolPointer)this.tools[0]).ZoomSelection = false;
		}

		public void SelectZoomSelectionTool()
		{
			this.InitializeTool(DrawingArea.DrawToolType.Pointer, Utilities.LoadCursor("Zoom.cur", Cursors.Default));
			((ToolPointer)this.tools[0]).ZoomSelection = true;
		}

		public void SelectPanTool()
		{
			this.InitializeTool(DrawingArea.DrawToolType.Pan, Utilities.LoadCursor("Pan.cur", Cursors.Hand));
		}

		private void DrawCursorCrossHair(Graphics g)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLine(new Point(4, 1), new Point(6, 1));
				graphicsPath.AddLine(new Point(6, 1), new Point(6, 4));
				graphicsPath.AddLine(new Point(6, 4), new Point(9, 4));
				graphicsPath.AddLine(new Point(9, 4), new Point(9, 6));
				graphicsPath.AddLine(new Point(9, 6), new Point(6, 6));
				graphicsPath.AddLine(new Point(6, 6), new Point(6, 9));
				graphicsPath.AddLine(new Point(6, 9), new Point(4, 9));
				graphicsPath.AddLine(new Point(4, 9), new Point(4, 6));
				graphicsPath.AddLine(new Point(4, 6), new Point(1, 6));
				graphicsPath.AddLine(new Point(1, 6), new Point(1, 4));
				graphicsPath.AddLine(new Point(1, 4), new Point(4, 4));
				graphicsPath.CloseFigure();
				g.DrawPath(new Pen(Color.White), graphicsPath);
			}
			using (GraphicsPath graphicsPath2 = new GraphicsPath())
			{
				graphicsPath2.AddLine(new Point(5, 2), new Point(5, 4));
				graphicsPath2.CloseFigure();
				graphicsPath2.AddLine(new Point(6, 5), new Point(8, 5));
				graphicsPath2.CloseFigure();
				graphicsPath2.AddLine(new Point(5, 6), new Point(5, 8));
				graphicsPath2.CloseFigure();
				graphicsPath2.AddLine(new Point(2, 5), new Point(4, 5));
				graphicsPath2.CloseFigure();
				g.DrawPath(new Pen(Color.Black), graphicsPath2);
			}
		}

		private void DrawAreaCursor(DrawObject drawObject, Graphics g)
		{
			this.DrawCursorCrossHair(g);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLine(new Point(11, 11), new Point(22, 11));
				graphicsPath.AddLine(new Point(22, 11), new Point(22, 17));
				graphicsPath.AddLine(new Point(22, 17), new Point(26, 17));
				graphicsPath.AddLine(new Point(26, 17), new Point(26, 22));
				graphicsPath.AddLine(new Point(26, 22), new Point(31, 22));
				graphicsPath.AddLine(new Point(31, 22), new Point(31, 31));
				graphicsPath.AddLine(new Point(31, 31), new Point(5, 31));
				graphicsPath.AddLine(new Point(5, 31), new Point(5, 17));
				graphicsPath.AddLine(new Point(5, 17), new Point(11, 17));
				graphicsPath.CloseFigure();
				DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
				if (drawPolyLine.Pattern == HatchStylePickerCombo.HatchStylePickerEnum.Solid)
				{
					g.FillPath(new SolidBrush(Color.FromArgb(220, drawPolyLine.FillColor)), graphicsPath);
				}
				else
				{
					g.FillPath(new HatchBrush((HatchStyle)drawPolyLine.Pattern, Color.FromArgb(250, drawPolyLine.FillColor), Color.FromArgb(220, Color.White)), graphicsPath);
				}
				g.DrawPath(new Pen(Color.FromArgb(255, drawObject.Color), 2f), graphicsPath);
			}
		}

		private void DrawPerimeterCursor(DrawObject drawObject, Graphics g)
		{
			this.DrawCursorCrossHair(g);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLine(new Point(12, 15), new Point(12, 27));
				graphicsPath.AddLine(new Point(13, 15), new Point(13, 27));
				graphicsPath.CloseFigure();
				graphicsPath.AddLine(new Point(15, 29), new Point(27, 29));
				graphicsPath.AddLine(new Point(15, 30), new Point(27, 30));
				graphicsPath.CloseFigure();
				graphicsPath.AddLine(new Point(29, 21), new Point(29, 27));
				graphicsPath.AddLine(new Point(30, 21), new Point(30, 27));
				graphicsPath.CloseFigure();
				graphicsPath.AddLine(new Point(22, 18), new Point(27, 18));
				graphicsPath.AddLine(new Point(22, 19), new Point(27, 19));
				graphicsPath.CloseFigure();
				g.DrawPath(new Pen(Color.FromArgb(220, drawObject.Color), 1f), graphicsPath);
			}
			using (GraphicsPath graphicsPath2 = new GraphicsPath())
			{
				graphicsPath2.AddRectangle(new Rectangle(11, 11, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(11, 28, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(28, 28, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(28, 17, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(18, 17, 4, 4));
				g.FillPath(new SolidBrush(Color.FromArgb(240, 58, 58, 58)), graphicsPath2);
			}
		}

		private void DrawCounterCursor(DrawObject drawObject, Graphics g)
		{
			this.DrawCursorCrossHair(g);
			int num = 20;
			Point p = new Point(19, 19);
			DrawCounter.CounterShapeTypeEnum shape = ((DrawCounter)drawObject).Shape;
			Rectangle rect = new Rectangle(p.X - num / 2, p.Y - num / 2, num, num);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				switch (shape)
				{
				case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
					graphicsPath.AddRectangle(rect);
					p = new Point(p.X, p.Y + 1);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
					graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(rect));
					p = new Point(p.X, p.Y + 1);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
					graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rect, DrawCounter.Direction.Up));
					p = new Point(p.X, p.Y + 3);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
					graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rect, DrawCounter.Direction.Down));
					p = new Point(p.X, p.Y - 2);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
					graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rect, DrawCounter.Direction.Up));
					p = new Point(p.X, p.Y + 1);
					break;
				case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
					graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rect, DrawCounter.Direction.Down));
					break;
				default:
					graphicsPath.AddEllipse(rect);
					p = new Point(p.X, p.Y + 1);
					break;
				}
				g.FillPath(new SolidBrush(Color.FromArgb(220, drawObject.FillColor)), graphicsPath);
				g.DrawPath(new Pen(Color.FromArgb(250, Color.Black), 1f), graphicsPath);
			}
			string text = drawObject.Text;
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.Character;
				stringFormat.LineAlignment = StringAlignment.Center;
				g.DrawString(text, new Font("Tahoma", Utilities.FontSizeInPoints(7f), FontStyle.Bold), new SolidBrush(Color.FromArgb(250, Color.Black)), p, stringFormat);
			}
		}

		private void DrawDistanceCursor(DrawObject drawObject, Graphics g)
		{
			this.DrawCursorCrossHair(g);
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLine(new Point(10, 24), new Point(24, 24));
				graphicsPath.AddLine(new Point(10, 25), new Point(24, 25));
				g.DrawPath(new Pen(Color.FromArgb(220, drawObject.Color), 1f), graphicsPath);
			}
			using (GraphicsPath graphicsPath2 = new GraphicsPath())
			{
				graphicsPath2.AddRectangle(new Rectangle(6, 23, 4, 4));
				graphicsPath2.AddRectangle(new Rectangle(24, 23, 4, 4));
				g.FillPath(new SolidBrush(Color.FromArgb(240, 58, 58, 58)), graphicsPath2);
			}
		}

		private void DrawObjectTypeCursor(DrawObject drawObject, Graphics g)
		{
			string objectType;
			if ((objectType = drawObject.ObjectType) != null)
			{
				if (objectType == "Area")
				{
					g.SmoothingMode = SmoothingMode.None;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					this.DrawAreaCursor(drawObject, g);
					return;
				}
				if (objectType == "Perimeter")
				{
					g.SmoothingMode = SmoothingMode.None;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					this.DrawPerimeterCursor(drawObject, g);
					return;
				}
				if (objectType == "Counter")
				{
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					this.DrawCounterCursor(drawObject, g);
					return;
				}
				if (!(objectType == "Line"))
				{
					return;
				}
				g.SmoothingMode = SmoothingMode.None;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				this.DrawDistanceCursor(drawObject, g);
			}
		}

		private Cursor LoadCursor(DrawObject drawObject, int xHotSpot, int yHotSpot, string defaultCursorName)
		{
			Cursor cursor = null;
			string objectType;
			if ((objectType = drawObject.ObjectType) != null)
			{
				if (objectType == "Rectangle")
				{
					cursor = Utilities.LoadCursor("Marker.cur", new Cursor(base.GetType(), "Rectangle.cur"));
					goto IL_D9;
				}
				if (objectType == "Angle")
				{
					cursor = Utilities.LoadCursor("Angle.cur", new Cursor(base.GetType(), "Pencil.cur"));
					goto IL_D9;
				}
				if (objectType == "Note")
				{
					cursor = Utilities.LoadCursor("Note.cur", new Cursor(base.GetType(), "Rectangle.cur"));
					goto IL_D9;
				}
			}
			try
			{
				using (Bitmap bitmap = new Bitmap(32, 32))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						this.DrawObjectTypeCursor(drawObject, graphics);
						cursor = Utilities.LoadCursor(bitmap, xHotSpot, yHotSpot, ref this.cursorHandlePtr);
					}
				}
			}
			catch
			{
				cursor = null;
			}
			IL_D9:
			return cursor ?? new Cursor(base.GetType(), defaultCursorName);
		}

		public void UpdateCursor(DrawObject drawObject)
		{
			if (this.ActiveTool == DrawingArea.DrawToolType.Counter || this.ActiveTool == DrawingArea.DrawToolType.Line || this.ActiveTool == DrawingArea.DrawToolType.PolyLine)
			{
				string defaultCursorName = "Pencil.cur";
				string objectType;
				if ((objectType = drawObject.ObjectType) != null)
				{
					if (!(objectType == "Line"))
					{
						if (objectType == "Counter")
						{
							defaultCursorName = "Ellipse.cur";
						}
					}
					else
					{
						defaultCursorName = "Line.cur";
					}
				}
				this.tools[(int)this.ActiveTool].LoadCursor(this.LoadCursor(drawObject, 5, 5, defaultCursorName));
			}
		}

		public void SelectAreaTool(DrawPolyLine drawPolyLine, string name, int groupID, string comment, Color color, HatchStylePickerCombo.HatchStylePickerEnum pattern, int opacity, SlopeFactor slopeFactor, bool showMeasure, bool visible)
		{
			this.toolSettings.Name = name;
			this.toolSettings.GroupID = groupID;
			this.toolSettings.Text = "";
			this.toolSettings.Comment = comment;
			this.toolSettings.LineColor = color;
			this.toolSettings.FillColor = color;
			this.toolSettings.Pattern = pattern;
			this.toolSettings.Opacity = opacity;
			this.toolSettings.LineWidth = 4;
			this.toolSettings.DrawFilled = true;
			this.toolSettings.CloseFigure = true;
			this.toolSettings.SlopeFactor.SetValues(slopeFactor);
			this.toolSettings.ShowMeasure = showMeasure;
			this.toolSettings.Visible = visible;
			this.toolSettings.IsDeduction = false;
			this.InitializeTool(DrawingArea.DrawToolType.PolyLine, this.LoadCursor(drawPolyLine, 5, 5, "Pencil.cur"));
		}

		public void SelectPerimeterTool(DrawPolyLine drawPolyLine, string name, int groupID, string comment, Color color, int opacity, int lineWidth, SlopeFactor slopeFactor, bool showMeasure, bool visible)
		{
			this.toolSettings.Name = name;
			this.toolSettings.GroupID = groupID;
			this.toolSettings.Text = "";
			this.toolSettings.Comment = comment;
			this.toolSettings.LineColor = color;
			this.toolSettings.FillColor = color;
			this.toolSettings.Opacity = opacity;
			this.toolSettings.LineWidth = lineWidth;
			this.toolSettings.DrawFilled = false;
			this.toolSettings.CloseFigure = false;
			this.toolSettings.SlopeFactor.SetValues(slopeFactor);
			this.toolSettings.ShowMeasure = showMeasure;
			this.toolSettings.Visible = visible;
			this.toolSettings.IsDeduction = false;
			this.InitializeTool(DrawingArea.DrawToolType.PolyLine, this.LoadCursor(drawPolyLine, 5, 5, "Pencil.cur"));
		}

		public void SelectCounterTool(DrawCounter drawCounter, string name, int groupID, string text, int defaultSize, DrawCounter.CounterShapeTypeEnum shape, string comment, Color color, int opacity, bool visible)
		{
			this.toolSettings.Name = name;
			this.toolSettings.GroupID = groupID;
			this.toolSettings.Text = text;
			this.toolSettings.Comment = comment;
			this.toolSettings.LineColor = color;
			this.toolSettings.FillColor = color;
			this.toolSettings.Opacity = opacity;
			this.toolSettings.LineWidth = 2;
			this.toolSettings.CounterSize = defaultSize;
			this.toolSettings.Shape = shape;
			this.toolSettings.DrawFilled = true;
			this.toolSettings.CloseFigure = false;
			this.toolSettings.ShowMeasure = false;
			this.toolSettings.Visible = visible;
			this.toolSettings.IsDeduction = false;
			this.InitializeTool(DrawingArea.DrawToolType.Counter, this.LoadCursor(drawCounter, 5, 5, "Ellipse.cur"));
		}

		public void SelectDistanceTool(DrawLine drawLine, string name, int groupID, string comment, Color color, int opacity, int lineWidth, SlopeFactor slopeFactor, bool showMeasure, bool visible)
		{
			this.toolSettings.Name = name;
			this.toolSettings.GroupID = groupID;
			this.toolSettings.Text = "";
			this.toolSettings.Comment = comment;
			this.toolSettings.LineColor = color;
			this.toolSettings.FillColor = color;
			this.toolSettings.Opacity = opacity;
			this.toolSettings.LineWidth = lineWidth;
			this.toolSettings.DrawFilled = false;
			this.toolSettings.CloseFigure = false;
			this.toolSettings.SlopeFactor.SetValues(slopeFactor);
			this.toolSettings.ShowMeasure = showMeasure;
			this.toolSettings.Visible = visible;
			this.toolSettings.IsDeduction = false;
			this.InitializeTool(DrawingArea.DrawToolType.Line, this.LoadCursor(drawLine, 5, 5, "Line.cur"));
		}

		public void SelectAngleTool(string name, string comment, Color color, int opacity, bool visible)
		{
			this.toolSettings.Name = name;
			this.toolSettings.GroupID = -1;
			this.toolSettings.Text = "";
			this.toolSettings.Comment = comment;
			this.toolSettings.LineColor = color;
			this.toolSettings.FillColor = color;
			this.toolSettings.Opacity = opacity;
			this.toolSettings.LineWidth = 6;
			this.toolSettings.DrawFilled = false;
			this.toolSettings.CloseFigure = false;
			this.toolSettings.ShowMeasure = true;
			this.toolSettings.Visible = visible;
			this.toolSettings.IsDeduction = false;
			this.InitializeTool(DrawingArea.DrawToolType.Angle, Utilities.LoadCursor("Angle.cur", new Cursor(base.GetType(), "Pencil.cur")));
		}

		public void SelectMarkerTool(string name, string comment, Color color, int opacity, bool visible)
		{
			this.toolSettings.Name = name;
			this.toolSettings.GroupID = -1;
			this.toolSettings.Text = "";
			this.toolSettings.Comment = comment;
			this.toolSettings.LineColor = color;
			this.toolSettings.FillColor = color;
			this.toolSettings.Opacity = opacity;
			this.toolSettings.LineWidth = 3;
			this.toolSettings.DrawFilled = true;
			this.toolSettings.CloseFigure = false;
			this.toolSettings.ShowMeasure = false;
			this.toolSettings.Visible = visible;
			this.toolSettings.IsDeduction = false;
			this.InitializeTool(DrawingArea.DrawToolType.Rectangle, Utilities.LoadCursor("Marker.cur", new Cursor(base.GetType(), "Rectangle.cur")));
		}

		public void SelectNoteTool(string name, string note, Color color, int opacity, bool visible)
		{
			this.toolSettings.Name = name;
			this.toolSettings.GroupID = -1;
			this.toolSettings.Text = "";
			this.toolSettings.Comment = note;
			this.toolSettings.LineColor = color;
			this.toolSettings.FillColor = color;
			this.toolSettings.Opacity = opacity;
			this.toolSettings.LineWidth = 3;
			this.toolSettings.DrawFilled = true;
			this.toolSettings.CloseFigure = false;
			this.toolSettings.ShowMeasure = false;
			this.toolSettings.Visible = visible;
			this.toolSettings.IsDeduction = false;
			this.InitializeTool(DrawingArea.DrawToolType.Note, Utilities.LoadCursor("Note.cur", new Cursor(base.GetType(), "Rectangle.cur")));
		}

		public void SelectScaleTool()
		{
			this.InitializeTool(DrawingArea.DrawToolType.Scale, Utilities.LoadCursor("Distance.cur", new Cursor(base.GetType(), "Line.cur")));
		}

		public void InsertObject(DrawObject o, int activeLayer, bool bringToFront, bool insertDeductionToList = false)
		{
			this.InsertObject(o, this.GetNextObjectID(), activeLayer, bringToFront, insertDeductionToList);
		}

		public void InsertObject(DrawObject o, int objectID, int activeLayer, bool bringToFront, bool insertDeductionToList = false)
		{
			string objectType;
			if ((objectType = o.ObjectType) != null)
			{
				if (<PrivateImplementationDetails>{E0438CCF-7425-4F3B-BA1B-66DC2567D6E9}.$$method0x60013e5-1 == null)
				{
					<PrivateImplementationDetails>{E0438CCF-7425-4F3B-BA1B-66DC2567D6E9}.$$method0x60013e5-1 = new Dictionary<string, int>(8)
					{
						{
							"Line",
							0
						},
						{
							"Rectangle",
							1
						},
						{
							"Area",
							2
						},
						{
							"Perimeter",
							3
						},
						{
							"Counter",
							4
						},
						{
							"Angle",
							5
						},
						{
							"Note",
							6
						},
						{
							"Legend",
							7
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{E0438CCF-7425-4F3B-BA1B-66DC2567D6E9}.$$method0x60013e5-1.TryGetValue(objectType, out num))
				{
					ToolObject toolObject;
					switch (num)
					{
					case 0:
						toolObject = (ToolLine)this.tools[2];
						break;
					case 1:
						toolObject = (ToolRectangle)this.tools[1];
						break;
					case 2:
					case 3:
						toolObject = (ToolPolyLine)this.tools[3];
						break;
					case 4:
						toolObject = (ToolCounter)this.tools[6];
						break;
					case 5:
						toolObject = (ToolAngle)this.tools[7];
						break;
					case 6:
						toolObject = (ToolNote)this.tools[8];
						break;
					case 7:
						toolObject = (ToolRectangle)this.tools[11];
						break;
					default:
						return;
					}
					toolObject.InsertObject(o, objectID, activeLayer, bringToFront, insertDeductionToList);
					return;
				}
			}
		}

		public void Delete(bool logCommand)
		{
			if (this.Layers == null)
			{
				return;
			}
			Command command = null;
			if (logCommand && this.deductionParent == null)
			{
				command = new CommandDelete(this, this.ActiveLayerName);
			}
			if (this.ActiveDrawingObjects.DeleteSelection())
			{
				if (command != null)
				{
					this.AddCommandToHistory(command);
					return;
				}
				if (this.deductionParent != null)
				{
					this.deductionParent.Selected = true;
					this.DeductionSaveCommand();
					this.DeductionParentRelease();
				}
			}
		}

		public int CreateNewLayer(string layerName)
		{
			if (this.Layers == null)
			{
				return -1;
			}
			int result = this.Layers.CreateNewLayer(layerName, 150);
			this.AddCommandToHistory(new CommandAddLayer(this, layerName));
			return result;
		}

		public bool RemoveLayer(int layerIndex)
		{
			if (this.Layers == null)
			{
				return false;
			}
			Layer layer = this.GetLayer(layerIndex);
			if (layer == null)
			{
				return false;
			}
			this.AddCommandToHistory(new CommandDeleteLayer(this, layer.Name));
			return this.Layers.RemoveLayer(layerIndex);
		}

		public int LayerMoveUp(int layerIndex)
		{
			if (this.Layers == null)
			{
				return -1;
			}
			if (this.GetLayer(layerIndex) == null)
			{
				return -1;
			}
			return this.Layers.MoveUp(layerIndex);
		}

		public int LayerMoveDown(int layerIndex)
		{
			if (this.Layers == null)
			{
				return -1;
			}
			if (this.GetLayer(layerIndex) == null)
			{
				return -1;
			}
			return this.Layers.MoveDown(layerIndex);
		}

		public void SelectAll()
		{
			try
			{
				this.ActiveLayer.DrawingObjects.SelectAll();
				this.Refresh();
			}
			catch
			{
			}
		}

		public void UnselectAll()
		{
			try
			{
				this.ActiveLayer.DrawingObjects.UnselectAll();
				this.Refresh();
			}
			catch
			{
			}
		}

		public void SelectObjectType(string objectType)
		{
			try
			{
				this.ActiveDrawingObjects.UnselectAll();
				foreach (object obj in this.ActiveDrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj;
					if (drawObject.ObjectType == objectType && !drawObject.IsDeduction())
					{
						drawObject.Selected = true;
					}
				}
				this.Refresh();
			}
			catch
			{
			}
		}

		public void SelectThisGroup(int groupID)
		{
			try
			{
				this.ActiveDrawingObjects.UnselectAll();
				foreach (object obj in this.ActiveDrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj;
					if (drawObject.GroupID == groupID && !drawObject.IsDeduction())
					{
						drawObject.Selected = true;
					}
				}
				this.Refresh();
			}
			catch
			{
			}
		}

		public int ObjectsCount()
		{
			int result;
			try
			{
				int num = 0;
				for (int i = 0; i < this.ActivePlan.Layers.Count; i++)
				{
					num += this.ObjectsCount(i);
				}
				result = num;
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		public int ObjectsCount(int layerIndex)
		{
			int result;
			try
			{
				int num = 0;
				foreach (object obj in this.ActivePlan.Layers[layerIndex].DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj;
					if (drawObject.ObjectType != "Legend" && !drawObject.IsDeduction())
					{
						num++;
					}
				}
				result = num;
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		public DrawObject GetNextOrPreviousObject(bool byObjectType, bool getPrevious)
		{
			DrawObject selectedObject = this.SelectedObject;
			if (selectedObject == null)
			{
				return null;
			}
			if (this.ActiveDrawingObjects.Count < 2)
			{
				return selectedObject;
			}
			int num = -1;
			for (int i = 0; i < this.ActiveDrawingObjects.Count; i++)
			{
				if (this.ActiveDrawingObjects[i].ID == selectedObject.ID)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return selectedObject;
			}
			int num2 = num;
			for (;;)
			{
				if (getPrevious)
				{
					if (num2 + 1 == this.ActiveDrawingObjects.Count)
					{
						num2 = 0;
					}
					else
					{
						num2++;
					}
				}
				else if (num2 - 1 < 0)
				{
					num2 = this.ActiveDrawingObjects.Count - 1;
				}
				else
				{
					num2--;
				}
				if (num2 == num)
				{
					return selectedObject;
				}
				if (!this.ActiveDrawingObjects[num2].IsDeduction())
				{
					if (byObjectType)
					{
						if (this.ActiveDrawingObjects[num2].ObjectType == selectedObject.ObjectType)
						{
							break;
						}
					}
					else if (selectedObject.IsPartOfGroup() && this.ActiveDrawingObjects[num2].GroupID == selectedObject.GroupID)
					{
						goto Block_13;
					}
				}
			}
			return this.ActiveDrawingObjects[num2];
			Block_13:
			return this.ActiveDrawingObjects[num2];
		}

		public void SelectPreviousObject(bool byObjectType)
		{
			DrawObject nextOrPreviousObject = this.GetNextOrPreviousObject(byObjectType, true);
			if (nextOrPreviousObject != null)
			{
				this.ActiveDrawingObjects.UnselectAll();
				nextOrPreviousObject.Selected = true;
				this.Refresh();
			}
		}

		public void SelectNextObject(bool byObjectType)
		{
			DrawObject nextOrPreviousObject = this.GetNextOrPreviousObject(byObjectType, false);
			if (nextOrPreviousObject != null)
			{
				this.ActiveDrawingObjects.UnselectAll();
				nextOrPreviousObject.Selected = true;
				this.Refresh();
			}
		}

		public void AutoAdjust(DrawPolyLine.AutoAdjustType autoAdjustType)
		{
			if (this.Layers == null)
			{
				return;
			}
			if (this.activeTool != DrawingArea.DrawToolType.Pointer)
			{
				return;
			}
			int activeLayerIndex = this.Layers.ActiveLayerIndex;
			if (this.Layers[activeLayerIndex].DrawingObjects.AutoAdjust((Bitmap)this.drawingBoard.Image, autoAdjustType))
			{
				this.Refresh();
			}
		}

		public void OpeningCreate(DrawObject drawObject)
		{
			if (drawObject == null)
			{
				return;
			}
			if (drawObject.ObjectType != "Perimeter")
			{
				return;
			}
			this.DeductionSetParent(drawObject);
			this.toolSettings.Name = drawObject.Name;
			this.toolSettings.GroupID = drawObject.GroupID;
			this.toolSettings.Text = drawObject.Text;
			this.toolSettings.Comment = drawObject.Comment;
			this.toolSettings.LineColor = Color.Black;
			this.toolSettings.FillColor = Color.Black;
			this.toolSettings.Opacity = drawObject.Opacity;
			this.toolSettings.LineWidth = drawObject.PenWidth;
			this.toolSettings.DrawFilled = false;
			this.toolSettings.CloseFigure = false;
			this.toolSettings.SlopeFactor.SetValues(drawObject.SlopeFactor);
			this.toolSettings.ShowMeasure = true;
			this.toolSettings.Visible = true;
			this.toolSettings.IsDeduction = true;
			this.InitializeTool(DrawingArea.DrawToolType.Opening, new Cursor(base.GetType(), "Pencil.cur"));
		}

		public void DeductionSaveCommand()
		{
			if (this.actionCommand != null)
			{
				this.actionCommand.NewState();
				this.AddCommandToHistory(this.actionCommand);
				this.actionCommand = null;
			}
		}

		public void DeductionParentRelease()
		{
			if (this.deductionParent != null)
			{
				this.RemoveAllDeductionsFromList();
				this.deductionParent.EditDeductions = false;
				this.Owner.RefreshObject(this.deductionParent);
				ArrayList deductionArray = this.deductionParent.DeductionArray;
				foreach (object obj in deductionArray)
				{
					DrawObject drawObject = (DrawObject)obj;
					drawObject.Visible = false;
					drawObject.Selected = false;
				}
				if (this.actionCommand != null && this.deductionWasChanged)
				{
					bool selected = this.deductionParent.Selected;
					this.deductionParent.Selected = true;
					this.DeductionSaveCommand();
					this.deductionParent.Selected = selected;
				}
				this.actionCommand = null;
				this.deductionParent = null;
				this.deductionWasChanged = false;
				if (this.OnDeductionsParentRelease != null)
				{
					this.OnDeductionsParentRelease();
				}
			}
			this.CursorRestricted = false;
		}

		private void RemoveAllDeductionsFromList()
		{
			for (int i = this.ActiveDrawingObjects.Count - 1; i >= 0; i--)
			{
				if (this.ActiveDrawingObjects[i].IsDeduction())
				{
					this.ActiveDrawingObjects.RemoveAt(i);
				}
			}
		}

		private void AddObjectDeductionsToList(DrawPolyLine drawPolyLine)
		{
			foreach (object obj in drawPolyLine.DeductionArray)
			{
				DrawObject o = (DrawObject)obj;
				this.InsertObject(o, this.ActiveLayerIndex, true, true);
			}
		}

		private void DeductionSetParent(DrawObject drawObject)
		{
			this.RemoveAllDeductionsFromList();
			this.deductionParent = (DrawPolyLine)drawObject;
			this.deductionParent.EditDeductions = true;
			this.deductionParent.Selected = true;
			this.actionCommand = new CommandChangeState(this, this.ActiveLayerName);
			this.deductionParent.Selected = false;
			this.deductionWasChanged = false;
			this.AddObjectDeductionsToList(this.deductionParent);
			this.Owner.UpdateObject(this.deductionParent);
			ArrayList deductionArray = this.deductionParent.DeductionArray;
			if (deductionArray.Count > 0)
			{
				this.ActiveDrawingObjects.UnselectAll();
				foreach (object obj in deductionArray)
				{
					DrawObject drawObject2 = (DrawObject)obj;
					drawObject2.ShowMeasure = this.deductionParent.ShowMeasure;
					drawObject2.Visible = true;
					drawObject2.Selected = false;
				}
			}
			if (this.OnDeductionsParentSet != null)
			{
				this.OnDeductionsParentSet();
			}
		}

		public void DeductionCreate(DrawObject drawObject)
		{
			if (drawObject == null)
			{
				return;
			}
			if (drawObject.ObjectType != "Area")
			{
				return;
			}
			this.DeductionSetParent(drawObject);
			this.toolSettings.Name = drawObject.Name;
			this.toolSettings.GroupID = drawObject.GroupID;
			this.toolSettings.Text = drawObject.Text;
			this.toolSettings.Comment = drawObject.Comment;
			this.toolSettings.LineColor = Color.LightGray;
			this.toolSettings.FillColor = Color.LightGray;
			this.toolSettings.Opacity = drawObject.Opacity;
			this.toolSettings.LineWidth = 3;
			this.toolSettings.DrawFilled = true;
			this.toolSettings.CloseFigure = true;
			this.toolSettings.SlopeFactor.SetValues(drawObject.SlopeFactor);
			this.toolSettings.ShowMeasure = true;
			this.toolSettings.Visible = true;
			this.toolSettings.IsDeduction = true;
			this.InitializeTool(DrawingArea.DrawToolType.Deduction, Utilities.LoadCursor("Deduction.cur", new Cursor(base.GetType(), "Rectangle.cur")));
		}

		public void DeductionsEdit(DrawObject drawObject)
		{
			if (drawObject == null)
			{
				return;
			}
			if (drawObject.ObjectType != "Area")
			{
				return;
			}
			if (((DrawPolyLine)drawObject).DeductionArray.Count == 0)
			{
				return;
			}
			this.DeductionSetParent(drawObject);
			DrawPolyLine drawPolyLine = (DrawPolyLine)((DrawPolyLine)drawObject).DeductionArray[0];
			drawPolyLine.Selected = true;
			this.SelectPointerTool();
			this.CursorRestricted = true;
			this.Refresh();
		}

		public void RestrictCursor(Point point, Size size)
		{
			Cursor.Clip = new Rectangle(point, size);
			this.cursorRestricted = true;
		}

		private void RestrictCursor(bool value)
		{
			if (value)
			{
				Point location = this.DrawingBoard.PointToScreen(new Point(0, 0));
				Size size = new Size(this.DrawingBoard.Width, this.DrawingBoard.Height);
				Cursor.Clip = new Rectangle(location, size);
				return;
			}
			Cursor.Clip = Rectangle.Empty;
		}

		public bool CursorRestricted
		{
			get
			{
				return this.cursorRestricted;
			}
			set
			{
				this.RestrictCursor(value);
				this.cursorRestricted = value;
			}
		}

		public Point MouseLocation
		{
			get
			{
				Point result;
				try
				{
					result = this.BackTrackMouse(this.DrawingBoard.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));
				}
				catch
				{
					result = new Point(0, 0);
				}
				return result;
			}
		}

		public Point NativePoint(Point point)
		{
			return new Point(point.X + (int)this.DrawingBoard.Origin.X, point.Y + (int)this.DrawingBoard.Origin.Y);
		}

		public Point IsPartOfDrop(DrawPolyLine drawPolyLine, Point point)
		{
			Point result;
			try
			{
				result = drawPolyLine.IsPartOfDrop(point, (int)this.DrawingBoard.Origin.X, (int)this.DrawingBoard.Origin.Y);
			}
			catch
			{
				result = Point.Empty;
			}
			return result;
		}

		public int SegmentHitTest(DrawPolyLine drawPolyLine, Point point)
		{
			int result;
			try
			{
				int num = drawPolyLine.SegmentHitTest(point, (int)this.DrawingBoard.Origin.X, (int)this.DrawingBoard.Origin.Y);
				if (num == -3 || num == -2)
				{
					this.ToolSettings.SelectedSegment.StartPoint = new Point(drawPolyLine.SelectedSegment.StartPoint.X, drawPolyLine.SelectedSegment.StartPoint.Y);
					this.ToolSettings.SelectedSegment.EndPoint = new Point(drawPolyLine.SelectedSegment.EndPoint.X, drawPolyLine.SelectedSegment.EndPoint.Y);
				}
				result = num;
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		public bool OrthoEnabled
		{
			get
			{
				return this.orthoEnabled;
			}
			set
			{
				this.orthoEnabled = value;
			}
		}

		public bool OrthoModeIsOn
		{
			get
			{
				if (!this.orthoEnabled)
				{
					return (Control.ModifierKeys & Keys.Control) == Keys.Control;
				}
				return (Control.ModifierKeys & Keys.Control) != Keys.Control;
			}
		}

		public Point BackTrackMouse(Point p)
		{
			Point[] array = new Point[]
			{
				p
			};
			Matrix matrix = new Matrix();
			matrix.Translate((float)(-(float)this.ClientSize.Width) / 2f, (float)(-(float)this.ClientSize.Height) / 2f, MatrixOrder.Append);
			matrix.Rotate(this._rotation, MatrixOrder.Append);
			matrix.Translate((float)this.ClientSize.Width / 2f, (float)this.ClientSize.Height / 2f, MatrixOrder.Append);
			matrix.Scale((float)this.DrawingBoard.ZoomFactor, (float)this.DrawingBoard.ZoomFactor, MatrixOrder.Append);
			matrix.Translate((float)this.drawingBoard.HorizontalOffset, (float)this.drawingBoard.VerticalOffset, MatrixOrder.Append);
			matrix.Invert();
			matrix.TransformPoints(array);
			return array[0];
		}

		public void OnContextMenu(MouseEventArgs e)
		{
			if (this.Layers == null)
			{
				return;
			}
			Console.WriteLine("OnContextMenu");
			Point point = this.BackTrackMouse(new Point(e.X, e.Y));
			new Point(e.X, e.Y);
			int activeLayerIndex = this.Layers.ActiveLayerIndex;
			int count = this.Layers[activeLayerIndex].DrawingObjects.Count;
			DrawObject drawObject = null;
			for (int i = 0; i < count; i++)
			{
				if (this.Layers[activeLayerIndex].DrawingObjects[i].Visible && this.Layers[activeLayerIndex].DrawingObjects[i].HitTest(point, (int)this.DrawingBoard.Origin.X, (int)this.DrawingBoard.Origin.Y) >= 0)
				{
					drawObject = this.Layers[activeLayerIndex].DrawingObjects[i];
					break;
				}
			}
			if (drawObject != null)
			{
				if (!drawObject.Selected)
				{
					this.Layers[activeLayerIndex].DrawingObjects.UnselectAll();
				}
				if (this.deductionParent != null)
				{
					bool flag = true;
					if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
					{
						flag = (((DrawPolyLine)drawObject).DeductionParentID == -1);
					}
					if (flag)
					{
						this.DeductionParentRelease();
					}
				}
				drawObject.Selected = true;
			}
			else
			{
				this.Layers[activeLayerIndex].DrawingObjects.UnselectAll();
				if (this.deductionParent != null)
				{
					this.DeductionParentRelease();
				}
			}
			Console.WriteLine("SelectionCount = " + this.Layers[activeLayerIndex].DrawingObjects.SelectionCount);
			this.DrawingBoard.Refresh();
			this.UpdateSelectedObjects();
			this.Owner.EditContextMenu(e);
		}

		public void TrackMouse(Point location)
		{
			this.tools[(int)this.activeTool].TrackMouse(location);
		}

		public void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (this.Layers == null)
			{
				return;
			}
			if (this.ActiveLayer == null)
			{
				return;
			}
			this.lastPoint = this.BackTrackMouse(e.Location);
			if (!this.ActiveLayer.Visible && this.activeTool != DrawingArea.DrawToolType.Pan)
			{
				string ce_calque_est_invisible = Resources.Ce_calque_est_invisible;
				string impossible_d_effectuer_l_opération_désirée = Resources.Impossible_d_effectuer_l_opération_désirée;
				Utilities.DisplayError(ce_calque_est_invisible, impossible_d_effectuer_l_opération_désirée);
				return;
			}
			MouseButtons button = e.Button;
			if (button != MouseButtons.Left)
			{
				if (button != MouseButtons.Right)
				{
					return;
				}
				this.tools[(int)this.activeTool].OnMouseDown(e);
			}
			else
			{
				this.tools[(int)this.activeTool].OnMouseDown(e);
				if (this.DrawingInProgress)
				{
					return;
				}
			}
		}

		public void OnMouseMove(object sender, MouseEventArgs e)
		{
			this.BackTrackMouse(e.Location);
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None || e.Button == MouseButtons.Right)
			{
				this.tools[(int)this.activeTool].OnMouseMove(e);
			}
			this.lastPoint = this.BackTrackMouse(e.Location);
		}

		public void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (this.Layers == null)
			{
				return;
			}
			this.tools[(int)this.activeTool].OnMouseUp(e);
			if (this.DrawingInProgress)
			{
				return;
			}
			if (this.activeTool == DrawingArea.DrawToolType.Pointer && e.Button != MouseButtons.Right)
			{
				this.UpdateSelectedObjects();
			}
			this.Capture = false;
			this.Refresh();
		}

		public void OnKeyDown(object sender, KeyEventArgs e)
		{
			this.tools[(int)this.activeTool].OnKeyDown(e);
		}

		private int colorIndex = 26;

		private Array AreaColors = new ColorPicker.StandardColorEnum[]
		{
			ColorPicker.StandardColorEnum.ColorLime,
			ColorPicker.StandardColorEnum.ColorSteelBlue,
			ColorPicker.StandardColorEnum.ColorMagenta,
			ColorPicker.StandardColorEnum.ColorYellow,
			ColorPicker.StandardColorEnum.ColorRed,
			ColorPicker.StandardColorEnum.ColorTurquoise,
			ColorPicker.StandardColorEnum.ColorOrange,
			ColorPicker.StandardColorEnum.ColorPeru,
			ColorPicker.StandardColorEnum.ColorSilver,
			ColorPicker.StandardColorEnum.ColorChartreuse,
			ColorPicker.StandardColorEnum.ColorBlue,
			ColorPicker.StandardColorEnum.ColorHotPink,
			ColorPicker.StandardColorEnum.ColorKhaki,
			ColorPicker.StandardColorEnum.ColorViolet,
			ColorPicker.StandardColorEnum.ColorPaleTurquoise,
			ColorPicker.StandardColorEnum.ColorLightSalmon,
			ColorPicker.StandardColorEnum.ColorTan,
			ColorPicker.StandardColorEnum.ColorGainsboro,
			ColorPicker.StandardColorEnum.ColorGreen,
			ColorPicker.StandardColorEnum.ColorDarkBlue,
			ColorPicker.StandardColorEnum.ColorDarkRed,
			ColorPicker.StandardColorEnum.ColorGold,
			ColorPicker.StandardColorEnum.ColorPurple,
			ColorPicker.StandardColorEnum.ColorTeal,
			ColorPicker.StandardColorEnum.ColorOrangeRed,
			ColorPicker.StandardColorEnum.ColorSaddleBrown,
			ColorPicker.StandardColorEnum.ColorSlateGray
		};

		private Array PerimeterColors = new ColorPicker.StandardColorEnum[]
		{
			ColorPicker.StandardColorEnum.ColorBlue,
			ColorPicker.StandardColorEnum.ColorDarkRed,
			ColorPicker.StandardColorEnum.ColorGreen,
			ColorPicker.StandardColorEnum.ColorPurple,
			ColorPicker.StandardColorEnum.ColorTeal,
			ColorPicker.StandardColorEnum.ColorOrangeRed,
			ColorPicker.StandardColorEnum.ColorSaddleBrown,
			ColorPicker.StandardColorEnum.ColorGold,
			ColorPicker.StandardColorEnum.ColorSlateGray,
			ColorPicker.StandardColorEnum.ColorDarkBlue,
			ColorPicker.StandardColorEnum.ColorRed,
			ColorPicker.StandardColorEnum.ColorLime,
			ColorPicker.StandardColorEnum.ColorMagenta,
			ColorPicker.StandardColorEnum.ColorTurquoise,
			ColorPicker.StandardColorEnum.ColorOrange,
			ColorPicker.StandardColorEnum.ColorPeru,
			ColorPicker.StandardColorEnum.ColorYellow,
			ColorPicker.StandardColorEnum.ColorSilver,
			ColorPicker.StandardColorEnum.ColorSteelBlue,
			ColorPicker.StandardColorEnum.ColorHotPink,
			ColorPicker.StandardColorEnum.ColorChartreuse,
			ColorPicker.StandardColorEnum.ColorViolet,
			ColorPicker.StandardColorEnum.ColorPaleTurquoise,
			ColorPicker.StandardColorEnum.ColorLightSalmon,
			ColorPicker.StandardColorEnum.ColorTan,
			ColorPicker.StandardColorEnum.ColorKhaki,
			ColorPicker.StandardColorEnum.ColorGainsboro
		};

		private Array CounterColors = new ColorPicker.StandardColorEnum[]
		{
			ColorPicker.StandardColorEnum.ColorRed,
			ColorPicker.StandardColorEnum.ColorTurquoise,
			ColorPicker.StandardColorEnum.ColorChartreuse,
			ColorPicker.StandardColorEnum.ColorKhaki,
			ColorPicker.StandardColorEnum.ColorViolet,
			ColorPicker.StandardColorEnum.ColorPaleTurquoise,
			ColorPicker.StandardColorEnum.ColorLightSalmon,
			ColorPicker.StandardColorEnum.ColorTan,
			ColorPicker.StandardColorEnum.ColorGainsboro,
			ColorPicker.StandardColorEnum.ColorHotPink,
			ColorPicker.StandardColorEnum.ColorBlue,
			ColorPicker.StandardColorEnum.ColorLime,
			ColorPicker.StandardColorEnum.ColorYellow,
			ColorPicker.StandardColorEnum.ColorMagenta,
			ColorPicker.StandardColorEnum.ColorSteelBlue,
			ColorPicker.StandardColorEnum.ColorOrange,
			ColorPicker.StandardColorEnum.ColorPeru,
			ColorPicker.StandardColorEnum.ColorSilver,
			ColorPicker.StandardColorEnum.ColorDarkRed,
			ColorPicker.StandardColorEnum.ColorDarkBlue,
			ColorPicker.StandardColorEnum.ColorGreen,
			ColorPicker.StandardColorEnum.ColorGold,
			ColorPicker.StandardColorEnum.ColorPurple,
			ColorPicker.StandardColorEnum.ColorTeal,
			ColorPicker.StandardColorEnum.ColorOrangeRed,
			ColorPicker.StandardColorEnum.ColorSaddleBrown,
			ColorPicker.StandardColorEnum.ColorSlateGray
		};

		private OnObjectsSelectedHandler OnObjectsSelected;

		private OnDeductionsParentSetHandler OnDeductionsParentSet;

		private OnDeductionsParentReleaseHandler OnDeductionsParentRelease;

		private float _rotation;

		private bool _drawingInProgress;

		private bool _pointerInProgress;

		private bool _panningInProgress;

		private bool _netSelectionInProgress;

		private DrawObject _currentlyCreatedObject;

		private DrawObject _currentlyResizedObject;

		private Point lastPoint;

		private Pen _currentPen;

		private DrawingPens.PenType _penType;

		private Brush _currentBrush;

		private FillBrushes.BrushType _brushType;

		private bool suspendScrolling;

		private DrawingArea.DrawToolType activeTool;

		private Tool[] tools;

		private ToolSettings toolSettings = new ToolSettings();

		private MainForm owner;

		private DrawingBoard drawingBoard;

		private Project project;

		private Plan currentPlan;

		private Clipboard clipboard = new Clipboard();

		private Rectangle netRectangle;

		private bool drawNetRectangle;

		private CommandChangeState actionCommand;

		private bool deductionWasChanged;

		private DrawPolyLine deductionParent;

		private bool cursorRestricted;

		private bool orthoEnabled;

		private IntPtr cursorHandlePtr = IntPtr.Zero;

		public enum DrawToolType
		{
			Pointer,
			Rectangle,
			Line,
			PolyLine,
			Deduction,
			Opening,
			Counter,
			Angle,
			Note,
			Pan,
			Scale,
			Legend,
			NumberOfDrawTools
		}
	}
}
