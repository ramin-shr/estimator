using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
	[Serializable]
	public class DrawLegend : DrawRectangle
	{
		public Plan Plan
		{
			[CompilerGenerated]
			get
			{
				return this.<Plan>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Plan>k__BackingField = value;
			}
		}

		public ExtensionsSupport ExtensionsSupport
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionsSupport>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExtensionsSupport>k__BackingField = value;
			}
		}

		public DrawObjectGroups Groups
		{
			[CompilerGenerated]
			get
			{
				return this.<Groups>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Groups>k__BackingField = value;
			}
		}

		public bool UpdateContent
		{
			[CompilerGenerated]
			get
			{
				return this.<UpdateContent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UpdateContent>k__BackingField = value;
			}
		}

		public bool MustSetLocation
		{
			[CompilerGenerated]
			get
			{
				return this.<MustSetLocation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MustSetLocation>k__BackingField = value;
			}
		}

		private int ValidateFontSize(int fontSize)
		{
			if (fontSize < 8)
			{
				return DrawLegend.DefaultFontSize;
			}
			if (fontSize <= 96)
			{
				return fontSize;
			}
			return 96;
		}

		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = this.ValidateFontSize(value);
			}
		}

		private int ValidateMaxRows(int maxRows)
		{
			if (maxRows < 1)
			{
				return DrawLegend.DefaultMaxRows;
			}
			if (maxRows <= 96)
			{
				return maxRows;
			}
			return 96;
		}

		public int MaxRows
		{
			get
			{
				return this.maxRows;
			}
			set
			{
				this.maxRows = this.ValidateMaxRows(value);
			}
		}

		public ArrayList ColumnMarginLeft
		{
			get
			{
				return this.columnMarginLeft;
			}
			set
			{
				this.columnMarginLeft = value;
			}
		}

		public ArrayList ColumnWidth
		{
			get
			{
				return this.columnWidth;
			}
			set
			{
				this.columnWidth = value;
			}
		}

		public override DrawObject Clone()
		{
			DrawLegend drawLegend = new DrawLegend(this.Plan, this.ExtensionsSupport);
			drawLegend.Rectangle = new Rectangle(base.Rectangle.Location, base.Rectangle.Size);
			drawLegend.DisplayName = new Utilities.DisplayName(drawLegend, "");
			base.FillDrawObjectFields(drawLegend);
			this.Groups = new DrawObjectGroups();
			this.ColumnMarginLeft = new ArrayList();
			this.ColumnWidth = new ArrayList();
			this.UpdateContent = true;
			this.MustSetLocation = false;
			return drawLegend;
		}

		public DrawLegend(Plan plan, ExtensionsSupport extensionsSupport)
		{
			this.Plan = plan;
			this.ExtensionsSupport = extensionsSupport;
			this.Groups = new DrawObjectGroups();
			this.ColumnMarginLeft = new ArrayList();
			this.ColumnWidth = new ArrayList();
			this.UpdateContent = true;
			base.SetRectangle(0, 0, 1, 1);
			base.Initialize();
		}

		public DrawLegend(Plan plan, ExtensionsSupport extensionsSupport, int x, int y, string name, Color color, int lineWidth, int fontSize, int maxRows)
		{
			this.Plan = plan;
			this.ExtensionsSupport = extensionsSupport;
			this.Groups = new DrawObjectGroups();
			this.ColumnMarginLeft = new ArrayList();
			this.ColumnWidth = new ArrayList();
			this.UpdateContent = true;
			base.Rectangle = new Rectangle(x, y, 1, 1);
			base.Color = color;
			base.FillColor = color;
			base.Filled = true;
			base.Opacity = 225;
			base.PenWidth = lineWidth;
			this.FontSize = fontSize;
			base.Name = name;
			base.GroupID = -1;
			base.Text = "";
			base.Comment = "";
			this.MaxRows = maxRows;
			base.Initialize();
		}

		private void ClearGroups()
		{
			this.Groups.Clear();
		}

		private void QueryExtensionsResults(DrawObject groupObject, DrawObjectGroup drawObjectGroup, GroupStats planStats, UnitScale unitScale)
		{
			DrawObjectGroup group = groupObject.Group;
			if (group != null)
			{
				foreach (object obj in group.Presets.Collection)
				{
					Preset preset = (Preset)obj;
					Preset preset2 = new Preset(preset.ID, preset.DisplayName, preset.CategoryName, preset.ExtensionName, unitScale.ScaleSystemType);
					drawObjectGroup.Presets.Add(preset2);
					this.ExtensionsSupport.QueryPresetResults(preset, planStats, unitScale);
					foreach (object obj2 in preset.Results.Collection)
					{
						PresetResult presetResult = (PresetResult)obj2;
						if (presetResult.ConditionMet && presetResult.ShowInLegend)
						{
							double num = Utilities.ConvertToDouble(presetResult.Result.ToString(), -1);
							string name = string.Empty;
							string text = string.Empty;
							DBEstimatingItem dbestimatingItem = null;
							if (presetResult.ItemID != -1)
							{
								dbestimatingItem = base.DrawArea.Project.DBManagement.GetEstimatingItem(presetResult.ItemID);
							}
							bool flag = false;
							if (dbestimatingItem != null)
							{
								flag = dbestimatingItem.MatchResultType(presetResult.ResultType);
							}
							if (flag)
							{
								UnitScale unitScale2 = new UnitScale(1f, (dbestimatingItem.PurchaseUnit == "m" || dbestimatingItem.PurchaseUnit == "m²" || dbestimatingItem.PurchaseUnit == "m³") ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial, unitScale.Precision, false);
								UnitScale.UnitSystem scaleSystemType = unitScale2.ScaleSystemType;
								name = dbestimatingItem.Description;
								switch (presetResult.ResultType)
								{
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
									num = ((unitScale.ScaleSystemType == scaleSystemType) ? num : ((scaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num) : UnitScale.FromFeetToMeters(num)));
									text = unitScale2.ToLengthStringFromUnitSystem(num, false, true, true);
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
									num = ((unitScale.ScaleSystemType == scaleSystemType) ? num : ((scaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(num) : UnitScale.FromSquareFeetToSquareMeters(num)));
									text = unitScale2.ToAreaStringFromUnitSystem(num, true);
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
									num = ((unitScale.ScaleSystemType == scaleSystemType) ? num : ((scaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromCubicMetersToCubicFeet(num) : UnitScale.FromCubicFeetToCubicMeters(num)));
									text = unitScale2.ToCubicStringFromUnitSystem(num, true);
									break;
								default:
									text = unitScale2.Round(num).ToString();
									text += ((dbestimatingItem.PurchaseUnit != string.Empty) ? (" " + dbestimatingItem.PurchaseUnit) : "");
									break;
								}
							}
							else
							{
								name = presetResult.Caption;
								switch (presetResult.ResultType)
								{
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
									text = unitScale.ToLengthStringFromUnitSystem(num, false, true, true);
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
									text = unitScale.ToAreaStringFromUnitSystem(num, true);
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
									text = unitScale.ToCubicStringFromUnitSystem(num, true);
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
									text = base.DrawArea.ToCurrency(num);
									break;
								default:
									text = unitScale.Round(num).ToString();
									break;
								}
								text += ((presetResult.Unit != string.Empty) ? (" " + presetResult.Unit) : "");
							}
							preset2.Fields.Add(new PresetField(name, text));
						}
					}
				}
			}
		}

		private string GetDrawObjectBasicInfo(DrawObject drawObject, GroupStats groupStats)
		{
			string text = string.Empty;
			string result;
			try
			{
				string objectType;
				if ((objectType = drawObject.ObjectType) != null)
				{
					if (!(objectType == "Line"))
					{
						if (!(objectType == "Area"))
						{
							if (!(objectType == "Perimeter"))
							{
								if (objectType == "Counter")
								{
									text = base.DrawArea.ToUnitString(groupStats.GroupCount);
								}
							}
							else
							{
								text = this.Plan.UnitScale.ToLengthStringFromUnitSystem(groupStats.NetLength, false, true, true);
							}
						}
						else
						{
							text = this.Plan.UnitScale.ToAreaStringFromUnitSystem(groupStats.AreaMinusDeduction, true);
						}
					}
					else
					{
						text = this.Plan.UnitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true);
					}
				}
				result = text;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = string.Empty;
			}
			return result;
		}

		private void UpdateGroups(Graphics g, int offsetX, int offsetY, Rectangle rectangle)
		{
			int num = 0;
			int num2 = this.padding;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int x = rectangle.X;
			int num6 = base.PenWidth / 2;
			int y = rectangle.Y;
			int penWidth = base.PenWidth;
			int num7 = base.ShowMeasure ? (this.FontSize * 3) : this.FontSize;
			int num8 = (int)((float)this.FontSize * 0.85f);
			this.ClearGroups();
			this.ColumnMarginLeft.Clear();
			this.ColumnWidth.Clear();
			using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic))
			{
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.EllipsisWord;
				stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
				ArrayList arrayList = new ArrayList();
				num = g.MeasureString(base.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize), FontStyle.Bold), -1, stringFormat).ToSize().Width;
				foreach (object obj in this.Plan.Layers.Collection)
				{
					Layer layer = (Layer)obj;
					if (layer.Visible)
					{
						foreach (object obj2 in layer.DrawingObjects.Collection)
						{
							DrawObject drawObject = (DrawObject)obj2;
							if (drawObject.Visible && drawObject.IsPartOfGroup() && !arrayList.Contains(drawObject.GroupID))
							{
								GroupStats groupStats = GroupUtilities.ComputeGroupStats(this.Plan, drawObject, this.Plan.UnitScale.ScaleSystemType, true, "");
								arrayList.Add(drawObject.GroupID);
								string basicInfo = base.ShowMeasure ? this.GetDrawObjectBasicInfo(drawObject, groupStats) : string.Empty;
								DrawObjectGroup group = new DrawObjectGroup(drawObject.GroupID, drawObject.Name, drawObject.ObjectType, drawObject.Color, basicInfo, drawObject);
								this.Groups.Add(group);
							}
						}
					}
				}
				this.Groups.Collection.Sort(new DrawLegend.GroupSorter());
				foreach (object obj3 in this.Groups.Collection)
				{
					DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj3;
					DrawObject drawObject2 = (DrawObject)drawObjectGroup.Tag;
					GroupStats groupStats2 = GroupUtilities.ComputeGroupStats(this.Plan, drawObject2, this.Plan.UnitScale.ScaleSystemType, true, "");
					arrayList.Add(drawObject2.GroupID);
					if (!base.ShowMeasure)
					{
						string empty = string.Empty;
					}
					else
					{
						this.GetDrawObjectBasicInfo(drawObject2, groupStats2);
					}
					int width = g.MeasureString(drawObjectGroup.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), -1, stringFormat).ToSize().Width;
					int num9;
					if (base.ShowMeasure)
					{
						num9 = g.MeasureString(drawObjectGroup.BasicInfo, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), -1, stringFormat).ToSize().Width;
					}
					else
					{
						num9 = 0;
					}
					int num10 = num8 + width + num9 + num7;
					num = ((num10 > num) ? num10 : num);
					num2 += (int)((float)this.FontSize * 0.9f);
					if (base.ShowMeasure)
					{
						this.QueryExtensionsResults(drawObject2, drawObjectGroup, groupStats2, this.Plan.UnitScale);
						foreach (object obj4 in drawObjectGroup.Presets.Collection)
						{
							Preset preset = (Preset)obj4;
							if (preset.Fields.Collection.Count > 0)
							{
								num2 += (int)((float)this.FontSize * 0.96f);
							}
							foreach (object obj5 in preset.Fields.Collection)
							{
								PresetField presetField = (PresetField)obj5;
								width = g.MeasureString(presetField.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), -1, stringFormat).ToSize().Width;
								num9 = g.MeasureString(presetField.Value.ToString(), new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), -1, stringFormat).ToSize().Width;
								num10 = num8 + width + num9 + num7;
								num = ((num10 > num) ? num10 : num);
								num2 += (int)((float)this.FontSize * 0.9f);
							}
						}
					}
					if (drawObjectGroup.Presets.Count > 0)
					{
						num2 += this.padding / 2;
					}
					num5++;
					if (num5 == this.MaxRows)
					{
						this.ColumnMarginLeft.Add(num4 + this.padding);
						this.ColumnWidth.Add(num);
						num4 += num + this.padding;
						num2 += this.padding;
						num3 = ((num2 > num3) ? num2 : num3);
						num = 0;
						num2 = this.padding;
						num5 = 0;
					}
				}
				num2 += this.padding;
				arrayList.Clear();
				arrayList = null;
			}
			if (num5 > 0)
			{
				this.ColumnMarginLeft.Add(num4 + this.padding);
				if (this.MaxRows <= this.Groups.Count)
				{
					this.ColumnWidth.Add(num + this.padding);
				}
				else
				{
					this.ColumnWidth.Add(num);
				}
				num4 += num + this.padding;
			}
			num3 = ((num3 < num2) ? num2 : num3);
			num4 += base.PenWidth + this.padding * 2;
			num3 += base.PenWidth * 2 + this.padding * 2;
			if (this.MustSetLocation)
			{
				base.SetRectangle(100, 100, num4, num3);
				this.MustSetLocation = false;
				return;
			}
			base.SetRectangle(rectangle.X, rectangle.Y, num4, num3);
		}

		private void TransformIcon(GraphicsPath gp, int offsetX, int offsetY)
		{
			float num = (float)this.fontSize / 64f;
			Matrix matrix = new Matrix();
			matrix.Scale(num, num, MatrixOrder.Append);
			matrix.Translate((float)(offsetX - (int)((float)offsetX * num)), (float)(offsetY - (int)((float)offsetY * num)), MatrixOrder.Append);
			gp.Transform(matrix);
		}

		private void DrawAreaIcon(Graphics g, DrawObject drawObject, int offsetX, int offsetY)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddLine(new Point(offsetX + 20, offsetY + 25), new Point(offsetX + 27, offsetY + 25));
			graphicsPath.AddLine(new Point(offsetX + 27, offsetY + 25), new Point(offsetX + 27, offsetY + 12));
			graphicsPath.AddLine(new Point(offsetX + 27, offsetY + 12), new Point(offsetX + 50, offsetY + 12));
			graphicsPath.AddLine(new Point(offsetX + 50, offsetY + 12), new Point(offsetX + 50, offsetY + 25));
			graphicsPath.AddLine(new Point(offsetX + 50, offsetY + 25), new Point(offsetX + 58, offsetY + 25));
			graphicsPath.AddLine(new Point(offsetX + 58, offsetY + 25), new Point(offsetX + 58, offsetY + 32));
			graphicsPath.AddLine(new Point(offsetX + 58, offsetY + 32), new Point(offsetX + 68, offsetY + 32));
			graphicsPath.AddLine(new Point(offsetX + 68, offsetY + 32), new Point(offsetX + 68, offsetY + 52));
			graphicsPath.AddLine(new Point(offsetX + 68, offsetY + 52), new Point(offsetX + 20, offsetY + 52));
			graphicsPath.AddLine(new Point(offsetX + 20, offsetY + 52), new Point(offsetX + 20, offsetY + 25));
			graphicsPath.CloseFigure();
			this.TransformIcon(graphicsPath, offsetX, offsetY);
			DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
			if (drawPolyLine.Pattern == HatchStylePickerCombo.HatchStylePickerEnum.Solid)
			{
				g.FillPath(new SolidBrush(Color.FromArgb(150, drawPolyLine.FillColor)), graphicsPath);
			}
			else
			{
				g.FillPath(new HatchBrush((HatchStyle)drawPolyLine.Pattern, Color.FromArgb(150, drawPolyLine.FillColor), Color.FromArgb(150, Color.White)), graphicsPath);
			}
			g.DrawPath(new Pen(Color.FromArgb(255, drawObject.Color), 3f), graphicsPath);
			graphicsPath.Dispose();
		}

		private void DrawPerimeterHandles(Graphics g, DrawObject drawObject, int offsetX, int offsetY)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddRectangle(new Rectangle(new Point(offsetX + 18, offsetY + 9), new Size(10, 10)));
			graphicsPath.AddRectangle(new Rectangle(new Point(offsetX + 18, offsetY + 48), new Size(10, 10)));
			graphicsPath.AddRectangle(new Rectangle(new Point(offsetX + 34, offsetY + 23), new Size(10, 10)));
			graphicsPath.AddRectangle(new Rectangle(new Point(offsetX + 61, offsetY + 23), new Size(10, 10)));
			graphicsPath.AddRectangle(new Rectangle(new Point(offsetX + 61, offsetY + 48), new Size(10, 10)));
			this.TransformIcon(graphicsPath, offsetX, offsetY);
			g.FillPath(new SolidBrush(Color.Black), graphicsPath);
			graphicsPath.Dispose();
		}

		private void DrawPerimeterIcon(Graphics g, DrawObject drawObject, int offsetX, int offsetY)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			Pen pen = new Pen(Color.FromArgb(200, drawObject.Color), (float)this.FontSize * 0.1f);
			pen.StartCap = LineCap.Square;
			graphicsPath.AddLine(new Point(offsetX + 23, offsetY + 14), new Point(offsetX + 23, offsetY + 53));
			graphicsPath.AddLine(new Point(offsetX + 23, offsetY + 53), new Point(offsetX + 66, offsetY + 53));
			graphicsPath.AddLine(new Point(offsetX + 66, offsetY + 53), new Point(offsetX + 66, offsetY + 28));
			graphicsPath.AddLine(new Point(offsetX + 66, offsetY + 28), new Point(offsetX + 38, offsetY + 28));
			this.TransformIcon(graphicsPath, offsetX, offsetY);
			g.DrawPath(pen, graphicsPath);
			pen.Dispose();
			graphicsPath.Dispose();
			this.DrawPerimeterHandles(g, drawObject, offsetX, offsetY);
		}

		private void DrawCounterIcon(Graphics g, DrawObject drawObject, int offsetX, int offsetY)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = 45;
			Point point = new Point(offsetX + 43, offsetY + 32);
			DrawCounter.CounterShapeTypeEnum shape = ((DrawCounter)drawObject).Shape;
			Rectangle rect = new Rectangle(point.X - num / 2, point.Y - num / 2, num, num);
			switch (shape)
			{
			case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
				graphicsPath.AddRectangle(rect);
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
				graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(rect));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
				graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rect, DrawCounter.Direction.Up));
				point = new Point(point.X, point.Y + (int)((float)rect.Height / 6f));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
				graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rect, DrawCounter.Direction.Down));
				point = new Point(point.X, point.Y - (int)((float)rect.Height / 6f));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
				graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rect, DrawCounter.Direction.Up));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
				graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rect, DrawCounter.Direction.Down));
				break;
			default:
				graphicsPath.AddEllipse(rect);
				break;
			}
			this.TransformIcon(graphicsPath, offsetX, offsetY);
			g.FillPath(new SolidBrush(Color.FromArgb(200, drawObject.FillColor)), graphicsPath);
			g.DrawPath(new Pen(Color.FromArgb(230, Color.Black), 1.75f), graphicsPath);
			graphicsPath.Dispose();
		}

		private void DrawDistanceHandles(Graphics g, DrawObject drawObject, int offsetX, int offsetY)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddRectangle(new Rectangle(new Point(offsetX + 18, offsetY + 29), new Size(10, 10)));
			graphicsPath.AddRectangle(new Rectangle(new Point(offsetX + 61, offsetY + 29), new Size(10, 10)));
			this.TransformIcon(graphicsPath, offsetX, offsetY);
			g.FillPath(new SolidBrush(Color.Black), graphicsPath);
			graphicsPath.Dispose();
		}

		private void DrawDistanceIcon(Graphics g, DrawObject drawObject, int offsetX, int offsetY)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			Pen pen = new Pen(Color.FromArgb(200, drawObject.Color), (float)this.FontSize * 0.1f);
			graphicsPath.AddLine(new Point(offsetX + 22, offsetY + 34), new Point(offsetX + 65, offsetY + 34));
			this.TransformIcon(graphicsPath, offsetX, offsetY);
			g.DrawPath(pen, graphicsPath);
			pen.Dispose();
			graphicsPath.Dispose();
			this.DrawDistanceHandles(g, drawObject, offsetX, offsetY);
		}

		private void DrawColorIndicator(Graphics g, DrawObject drawObject, int x, int y, int size)
		{
			string objectType;
			if ((objectType = drawObject.ObjectType) != null)
			{
				if (objectType == "Area")
				{
					this.DrawAreaIcon(g, drawObject, x - 3, y - 3);
					return;
				}
				if (objectType == "Perimeter")
				{
					this.DrawPerimeterIcon(g, drawObject, x - 3, y - 3);
					return;
				}
				if (objectType == "Counter")
				{
					this.DrawCounterIcon(g, drawObject, x - 3, y - 3);
					return;
				}
				if (!(objectType == "Line"))
				{
					return;
				}
				this.DrawDistanceIcon(g, drawObject, x - 3, y - 3);
			}
		}

		private int GetMarginLeft(int columnIndex, Rectangle screenRectangle)
		{
			int result;
			try
			{
				result = screenRectangle.X + (int)this.ColumnMarginLeft[columnIndex];
			}
			catch
			{
				result = screenRectangle.X + base.PenWidth / 2 + this.padding;
			}
			return result;
		}

		private int GetColumnWidth(int columnIndex, Rectangle screenRectangle)
		{
			int result;
			try
			{
				result = (int)this.ColumnWidth[columnIndex];
			}
			catch
			{
				result = 250;
			}
			return result;
		}

		private void DrawContent(Graphics g, Rectangle screenRectangle)
		{
			int num = this.padding;
			int num2 = 0;
			int num3 = 0;
			int marginLeft = this.GetMarginLeft(num3, screenRectangle);
			int num4 = screenRectangle.Y + base.PenWidth + this.padding;
			using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic))
			{
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.EllipsisWord;
				stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
				if (!base.SuspendContentDrawing)
				{
					stringFormat.Alignment = StringAlignment.Center;
					g.DrawString(base.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize), FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 64, 64, 64)), new PointF((float)(marginLeft + screenRectangle.Width / 2 - (base.PenWidth + this.padding)), (float)(num4 + this.padding / 2)), stringFormat);
					using (IEnumerator enumerator = this.Groups.Collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
							int num5 = (int)((float)this.FontSize * 0.85f);
							int num6 = marginLeft + this.GetColumnWidth(num3, screenRectangle);
							int num7 = num4 + num + this.FontSize;
							string name = drawObjectGroup.Name;
							string basicInfo = drawObjectGroup.BasicInfo;
							DrawObject drawObject = (DrawObject)drawObjectGroup.Tag;
							this.DrawColorIndicator(g, drawObject, marginLeft + this.padding / 3, num7 - num5 / 2, num5);
							stringFormat.Alignment = StringAlignment.Near;
							g.DrawString(name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), Brushes.Black, new PointF((float)(marginLeft + num5 + this.padding), (float)num7), stringFormat);
							stringFormat.Alignment = StringAlignment.Far;
							g.DrawString(basicInfo, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), Brushes.DarkBlue, new PointF((float)num6, (float)num7), stringFormat);
							num += (int)((float)this.FontSize * 0.9f);
							foreach (object obj2 in drawObjectGroup.Presets.Collection)
							{
								Preset preset = (Preset)obj2;
								if (preset.Fields.Collection.Count > 0)
								{
									stringFormat.Alignment = StringAlignment.Near;
									g.DrawString(preset.DisplayName, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.75f)), Brushes.DarkRed, new PointF((float)(marginLeft + num5 + this.padding), (float)(num4 + num) + (float)this.FontSize * 1.05f), stringFormat);
									num += (int)((float)this.FontSize * 0.96f);
								}
								foreach (object obj3 in preset.Fields.Collection)
								{
									PresetField presetField = (PresetField)obj3;
									stringFormat.Alignment = StringAlignment.Near;
									g.DrawString(presetField.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), Brushes.Black, new PointF((float)(marginLeft + num5 + this.padding), (float)(num4 + num + this.FontSize)), stringFormat);
									stringFormat.Alignment = StringAlignment.Far;
									g.DrawString(presetField.Value.ToString(), new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), Brushes.DarkBlue, new PointF((float)num6, (float)(num4 + num + this.FontSize)), stringFormat);
									num += (int)((float)this.FontSize * 0.9f);
								}
							}
							if (drawObjectGroup.Presets.Count > 0)
							{
								num += this.padding / 2;
							}
							num2++;
							if (num2 == this.MaxRows)
							{
								num3++;
								num6 = marginLeft + this.GetColumnWidth(num3, screenRectangle);
								marginLeft = this.GetMarginLeft(num3, screenRectangle);
								num4 = screenRectangle.Y + base.PenWidth + this.padding;
								num = this.padding;
								num2 = 0;
							}
						}
						goto IL_4C3;
					}
				}
				g.FillRectangle(new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.Gray, Color.White), screenRectangle);
				stringFormat.Alignment = StringAlignment.Center;
				g.DrawString(base.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize), FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 64, 64, 64)), new PointF((float)(marginLeft + screenRectangle.Width / 2 - (base.PenWidth + this.padding)), (float)(num4 + screenRectangle.Height / 2 - (base.PenWidth + this.padding))), stringFormat);
				IL_4C3:;
			}
		}

		public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = (printToScreen ? ((imageQuality == MainForm.ImageQualityEnum.QualityHigh) ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel) : TextRenderingHint.ClearTypeGridFit);
			Brush brush = new SolidBrush(Color.FromArgb(base.Opacity, base.FillColor));
			Pen pen;
			if (base.DrawPen == null)
			{
				pen = new Pen(Color.FromArgb(base.Opacity + 30, Color.Black), (float)base.PenWidth);
			}
			else
			{
				pen = (Pen)base.DrawPen.Clone();
			}
			pen.DashStyle = DashStyle.Solid;
			GraphicsPath graphicsPath = new GraphicsPath();
			this.padding = this.fontSize / 2;
			Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
			if (this.UpdateContent)
			{
				this.UpdateGroups(g, offsetX, offsetY, normalizedRectangle);
				this.UpdateContent = false;
			}
			Rectangle normalizedRectangle2 = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
			normalizedRectangle2.X -= offsetX;
			normalizedRectangle2.Y -= offsetY;
			graphicsPath.AddRectangle(normalizedRectangle2);
			if (base.Rotation != 0)
			{
				RectangleF bounds = graphicsPath.GetBounds();
				Matrix matrix = new Matrix();
				matrix.RotateAt((float)base.Rotation, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
				graphicsPath.Transform(matrix);
			}
			if (base.Filled)
			{
				g.FillPath(brush, graphicsPath);
			}
			g.DrawPath(pen, graphicsPath);
			this.DrawContent(g, normalizedRectangle2);
			graphicsPath.Dispose();
			pen.Dispose();
			brush.Dispose();
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		public override bool IsHandleResizable(int handleNumber)
		{
			return false;
		}

		public override Cursor GetHandleCursor(int handleNumber)
		{
			return Cursors.Default;
		}

		public void RecalcLayout()
		{
			this.UpdateContent = true;
			base.DrawArea.DrawingBoard.Refresh();
		}

		public void UpdateFontSize(int fontSize)
		{
			this.FontSize = fontSize;
			this.RecalcLayout();
		}

		public void UpdateMaxRows(int maxRows)
		{
			this.MaxRows = maxRows;
			this.RecalcLayout();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DrawLegend()
		{
		}

		public static int DefaultFontSize = 45;

		public static int DefaultMaxRows = 25;

		private Point startPoint;

		private Point endPoint;

		private int padding;

		private int fontSize = DrawLegend.DefaultFontSize;

		private int maxRows = DrawLegend.DefaultMaxRows;

		private ArrayList columnMarginLeft;

		private ArrayList columnWidth;

		[CompilerGenerated]
		private Plan <Plan>k__BackingField;

		[CompilerGenerated]
		private ExtensionsSupport <ExtensionsSupport>k__BackingField;

		[CompilerGenerated]
		private DrawObjectGroups <Groups>k__BackingField;

		[CompilerGenerated]
		private bool <UpdateContent>k__BackingField;

		[CompilerGenerated]
		private bool <MustSetLocation>k__BackingField;

		private class GroupSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				int result;
				try
				{
					DrawObjectGroup drawObjectGroup = x as DrawObjectGroup;
					DrawObjectGroup drawObjectGroup2 = y as DrawObjectGroup;
					DrawObject drawObject = (DrawObject)drawObjectGroup.Tag;
					DrawObject drawObject2 = (DrawObject)drawObjectGroup2.Tag;
					if (drawObject.ObjectType != drawObject2.ObjectType)
					{
						result = StringLogicalComparer.Compare(drawObject.ObjectSortOrder.ToString(), drawObject2.ObjectSortOrder.ToString());
					}
					else
					{
						result = StringLogicalComparer.Compare(drawObject.Name, drawObject2.Name);
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}

			public GroupSorter()
			{
			}
		}
	}
}
