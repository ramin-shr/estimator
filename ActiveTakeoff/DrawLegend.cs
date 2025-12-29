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
        public static int DefaultFontSize;

        public static int DefaultMaxRows;

        private Point startPoint;

        private Point endPoint;

        private int padding;

        private int fontSize = DrawLegend.DefaultFontSize;

        private int maxRows = DrawLegend.DefaultMaxRows;

        private ArrayList columnMarginLeft;

        private ArrayList columnWidth;

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

        public ExtensionsSupport ExtensionsSupport
        {
            get;
            set;
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

        public DrawObjectGroups Groups
        {
            get;
            set;
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

        public bool MustSetLocation
        {
            get;
            set;
        }

        public Plan Plan
        {
            get;
            set;
        }

        public bool UpdateContent
        {
            get;
            set;
        }

        static DrawLegend()
        {
            DrawLegend.DefaultFontSize = 45;
            DrawLegend.DefaultMaxRows = 25;
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

        public override DrawObject Clone()
        {
            DrawLegend drawLegend = new DrawLegend(this.Plan, this.ExtensionsSupport)
            {
                Rectangle = new Rectangle(base.Rectangle.Location, base.Rectangle.Size),
                DisplayName = new Utilities.DisplayName(drawLegend, "")
            };
            base.FillDrawObjectFields(drawLegend);
            this.Groups = new DrawObjectGroups();
            this.ColumnMarginLeft = new ArrayList();
            this.ColumnWidth = new ArrayList();
            this.UpdateContent = true;
            this.MustSetLocation = false;
            return drawLegend;
        }

        public override void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = 1)
        {
            Pen pen;
            TextRenderingHint textRenderingHint;
            SmoothingMode smoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            TextRenderingHint textRenderingHint1 = g.TextRenderingHint;
            Graphics graphic = g;
            if (printToScreen)
            {
                textRenderingHint = (imageQuality == MainForm.ImageQualityEnum.QualityHigh ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.SingleBitPerPixel);
            }
            else
            {
                textRenderingHint = TextRenderingHint.ClearTypeGridFit;
            }
            graphic.TextRenderingHint = textRenderingHint;
            Brush solidBrush = new SolidBrush(Color.FromArgb(base.Opacity, base.FillColor));
            pen = (base.DrawPen != null ? (Pen)base.DrawPen.Clone() : new Pen(Color.FromArgb(base.Opacity + 30, Color.Black), (float)base.PenWidth));
            pen.DashStyle = DashStyle.Solid;
            GraphicsPath graphicsPath = new GraphicsPath();
            this.padding = this.fontSize / 2;
            Rectangle normalizedRectangle = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
            if (this.UpdateContent)
            {
                this.UpdateGroups(g, offsetX, offsetY, normalizedRectangle);
                this.UpdateContent = false;
            }
            Rectangle x = DrawRectangle.GetNormalizedRectangle(base.Rectangle);
            x.X = x.X - offsetX;
            x.Y = x.Y - offsetY;
            graphicsPath.AddRectangle(x);
            if (base.Rotation != 0)
            {
                RectangleF bounds = graphicsPath.GetBounds();
                Matrix matrix = new Matrix();
                matrix.RotateAt((float)base.Rotation, new PointF(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f), MatrixOrder.Append);
                graphicsPath.Transform(matrix);
            }
            if (base.Filled)
            {
                g.FillPath(solidBrush, graphicsPath);
            }
            g.DrawPath(pen, graphicsPath);
            this.DrawContent(g, x);
            graphicsPath.Dispose();
            pen.Dispose();
            solidBrush.Dispose();
            g.TextRenderingHint = textRenderingHint1;
            g.SmoothingMode = smoothingMode;
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
            if (drawPolyLine.Pattern != HatchStylePickerCombo.HatchStylePickerEnum.Solid)
            {
                g.FillPath(new HatchBrush((HatchStyle)drawPolyLine.Pattern, Color.FromArgb(150, drawPolyLine.FillColor), Color.FromArgb(150, Color.White)), graphicsPath);
            }
            else
            {
                g.FillPath(new SolidBrush(Color.FromArgb(150, drawPolyLine.FillColor)), graphicsPath);
            }
            g.DrawPath(new Pen(Color.FromArgb(0xff, drawObject.Color), 3f), graphicsPath);
            graphicsPath.Dispose();
        }

        private void DrawColorIndicator(Graphics g, DrawObject drawObject, int x, int y, int size)
        {
            string objectType = drawObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                if (str == "Area")
                {
                    this.DrawAreaIcon(g, drawObject, x - 3, y - 3);
                    return;
                }
                if (str == "Perimeter")
                {
                    this.DrawPerimeterIcon(g, drawObject, x - 3, y - 3);
                    return;
                }
                if (str == "Counter")
                {
                    this.DrawCounterIcon(g, drawObject, x - 3, y - 3);
                    return;
                }
                if (str != "Line")
                {
                    return;
                }
                this.DrawDistanceIcon(g, drawObject, x - 3, y - 3);
            }
        }

        private void DrawContent(Graphics g, Rectangle screenRectangle)
        {
            int fontSize = this.padding;
            int num = 0;
            int num1 = 0;
            int marginLeft = this.GetMarginLeft(num1, screenRectangle);
            int y = screenRectangle.Y + base.PenWidth + this.padding;
            using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic))
            {
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Trimming = StringTrimming.EllipsisWord;
                stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                if (base.SuspendContentDrawing)
                {
                    g.FillRectangle(new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.Gray, Color.White), screenRectangle);
                    stringFormat.Alignment = StringAlignment.Center;
                    g.DrawString(base.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize), FontStyle.Bold), new SolidBrush(Color.FromArgb(0xff, 64, 64, 64)), new PointF((float)(marginLeft + screenRectangle.Width / 2 - (base.PenWidth + this.padding)), (float)(y + screenRectangle.Height / 2 - (base.PenWidth + this.padding))), stringFormat);
                }
                else
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    g.DrawString(base.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize), FontStyle.Bold), new SolidBrush(Color.FromArgb(0xff, 64, 64, 64)), new PointF((float)(marginLeft + screenRectangle.Width / 2 - (base.PenWidth + this.padding)), (float)(y + this.padding / 2)), stringFormat);
                    foreach (DrawObjectGroup collection in this.Groups.Collection)
                    {
                        int fontSize1 = (int)((float)this.FontSize * 0.85f);
                        int columnWidth = marginLeft + this.GetColumnWidth(num1, screenRectangle);
                        int fontSize2 = y + fontSize + this.FontSize;
                        string name = collection.Name;
                        string basicInfo = collection.BasicInfo;
                        DrawObject tag = (DrawObject)collection.Tag;
                        this.DrawColorIndicator(g, tag, marginLeft + this.padding / 3, fontSize2 - fontSize1 / 2, fontSize1);
                        stringFormat.Alignment = StringAlignment.Near;
                        g.DrawString(name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), Brushes.Black, new PointF((float)(marginLeft + fontSize1 + this.padding), (float)fontSize2), stringFormat);
                        stringFormat.Alignment = StringAlignment.Far;
                        g.DrawString(basicInfo, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), Brushes.DarkBlue, new PointF((float)columnWidth, (float)fontSize2), stringFormat);
                        fontSize += (int)((float)this.FontSize * 0.9f);
                        foreach (Preset preset in collection.Presets.Collection)
                        {
                            if (preset.Fields.Collection.Count > 0)
                            {
                                stringFormat.Alignment = StringAlignment.Near;
                                g.DrawString(preset.DisplayName, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.75f)), Brushes.DarkRed, new PointF((float)(marginLeft + fontSize1 + this.padding), (float)(y + fontSize) + (float)this.FontSize * 1.05f), stringFormat);
                                fontSize += (int)((float)this.FontSize * 0.96f);
                            }
                            foreach (PresetField presetField in preset.Fields.Collection)
                            {
                                stringFormat.Alignment = StringAlignment.Near;
                                g.DrawString(presetField.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), Brushes.Black, new PointF((float)(marginLeft + fontSize1 + this.padding), (float)(y + fontSize + this.FontSize)), stringFormat);
                                stringFormat.Alignment = StringAlignment.Far;
                                g.DrawString(presetField.Value.ToString(), new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), Brushes.DarkBlue, new PointF((float)columnWidth, (float)(y + fontSize + this.FontSize)), stringFormat);
                                fontSize += (int)((float)this.FontSize * 0.9f);
                            }
                        }
                        if (collection.Presets.Count > 0)
                        {
                            fontSize = fontSize + this.padding / 2;
                        }
                        num++;
                        if (num != this.MaxRows)
                        {
                            continue;
                        }
                        num1++;
                        columnWidth = marginLeft + this.GetColumnWidth(num1, screenRectangle);
                        marginLeft = this.GetMarginLeft(num1, screenRectangle);
                        y = screenRectangle.Y + base.PenWidth + this.padding;
                        fontSize = this.padding;
                        num = 0;
                    }
                }
            }
        }

        private void DrawCounterIcon(Graphics g, DrawObject drawObject, int offsetX, int offsetY)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            int num = 45;
            Point point = new Point(offsetX + 43, offsetY + 32);
            DrawCounter.CounterShapeTypeEnum shape = ((DrawCounter)drawObject).Shape;
            Rectangle rectangle = new Rectangle(point.X - num / 2, point.Y - num / 2, num, num);
            switch (shape)
            {
                case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
                    {
                        graphicsPath.AddRectangle(rectangle);
                        break;
                    }
                case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
                    {
                        graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(rectangle));
                        break;
                    }
                case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
                    {
                        graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rectangle, DrawCounter.Direction.Up));
                        point = new Point(point.X, point.Y + (int)((float)rectangle.Height / 6f));
                        break;
                    }
                case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
                    {
                        graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rectangle, DrawCounter.Direction.Down));
                        point = new Point(point.X, point.Y - (int)((float)rectangle.Height / 6f));
                        break;
                    }
                case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
                    {
                        graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rectangle, DrawCounter.Direction.Up));
                        break;
                    }
                case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
                    {
                        graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rectangle, DrawCounter.Direction.Down));
                        break;
                    }
                default:
                    {
                        graphicsPath.AddEllipse(rectangle);
                        break;
                    }
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
            Pen pen = new Pen(Color.FromArgb(200, drawObject.Color), (float)this.FontSize * 0.1f)
            {
                StartCap = LineCap.Square
            };
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

        private int GetColumnWidth(int columnIndex, Rectangle screenRectangle)
        {
            int item;
            try
            {
                item = (int)this.ColumnWidth[columnIndex];
            }
            catch
            {
                item = 250;
            }
            return item;
        }

        private string GetDrawObjectBasicInfo(DrawObject drawObject, GroupStats groupStats)
        {
            string empty;
            string lengthStringFromUnitSystem = string.Empty;
            try
            {
                string objectType = drawObject.ObjectType;
                string str = objectType;
                if (objectType != null)
                {
                    if (str == "Line")
                    {
                        lengthStringFromUnitSystem = this.Plan.UnitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true);
                    }
                    else if (str == "Area")
                    {
                        lengthStringFromUnitSystem = this.Plan.UnitScale.ToAreaStringFromUnitSystem(groupStats.AreaMinusDeduction, true);
                    }
                    else if (str == "Perimeter")
                    {
                        lengthStringFromUnitSystem = this.Plan.UnitScale.ToLengthStringFromUnitSystem(groupStats.NetLength, false, true, true);
                    }
                    else if (str == "Counter")
                    {
                        lengthStringFromUnitSystem = base.DrawArea.ToUnitString(groupStats.GroupCount);
                    }
                }
                empty = lengthStringFromUnitSystem;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                empty = string.Empty;
            }
            return empty;
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        private int GetMarginLeft(int columnIndex, Rectangle screenRectangle)
        {
            int x;
            try
            {
                x = screenRectangle.X + (int)this.ColumnMarginLeft[columnIndex];
            }
            catch
            {
                x = screenRectangle.X + base.PenWidth / 2 + this.padding;
            }
            return x;
        }

        public override bool IsHandleResizable(int handleNumber)
        {
            return false;
        }

        private void QueryExtensionsResults(DrawObject groupObject, DrawObjectGroup drawObjectGroup, GroupStats planStats, UnitScale unitScale)
        {
            double num;
            double num1;
            double num2;
            DrawObjectGroup group = groupObject.Group;
            if (group != null)
            {
                foreach (Preset collection in group.Presets.Collection)
                {
                    Preset preset = new Preset(collection.ID, collection.DisplayName, collection.CategoryName, collection.ExtensionName, unitScale.ScaleSystemType);
                    drawObjectGroup.Presets.Add(preset);
                    this.ExtensionsSupport.QueryPresetResults(collection, planStats, unitScale);
                    foreach (PresetResult presetResult in collection.Results.Collection)
                    {
                        if (!presetResult.ConditionMet || !presetResult.ShowInLegend)
                        {
                            continue;
                        }
                        double result = presetResult.Result;
                        double num3 = Utilities.ConvertToDouble(result.ToString(), -1);
                        string empty = string.Empty;
                        string lengthStringFromUnitSystem = string.Empty;
                        DBEstimatingItem estimatingItem = null;
                        if (presetResult.ItemID != -1)
                        {
                            estimatingItem = base.DrawArea.Project.DBManagement.GetEstimatingItem(presetResult.ItemID);
                        }
                        bool flag = false;
                        if (estimatingItem != null)
                        {
                            flag = estimatingItem.MatchResultType(presetResult.ResultType);
                        }
                        if (!flag)
                        {
                            empty = presetResult.Caption;
                            switch (presetResult.ResultType)
                            {
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                                    {
                                        lengthStringFromUnitSystem = unitScale.ToLengthStringFromUnitSystem(num3, false, true, true);
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                                    {
                                        lengthStringFromUnitSystem = unitScale.ToAreaStringFromUnitSystem(num3, true);
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                                    {
                                        lengthStringFromUnitSystem = unitScale.ToCubicStringFromUnitSystem(num3, true);
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
                                    {
                                        lengthStringFromUnitSystem = base.DrawArea.ToCurrency(num3);
                                        break;
                                    }
                                default:
                                    {
                                        lengthStringFromUnitSystem = unitScale.Round(num3).ToString();
                                        break;
                                    }
                            }
                            lengthStringFromUnitSystem = string.Concat(lengthStringFromUnitSystem, (presetResult.Unit != string.Empty ? string.Concat(" ", presetResult.Unit) : ""));
                        }
                        else
                        {
                            UnitScale unitScale1 = new UnitScale(1f, (estimatingItem.PurchaseUnit == "m" || estimatingItem.PurchaseUnit == "m²" || estimatingItem.PurchaseUnit == "m³" ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial), unitScale.Precision, false);
                            UnitScale.UnitSystem scaleSystemType = unitScale1.ScaleSystemType;
                            empty = estimatingItem.Description;
                            switch (presetResult.ResultType)
                            {
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                                    {
                                        if (unitScale.ScaleSystemType == scaleSystemType)
                                        {
                                            num = num3;
                                        }
                                        else
                                        {
                                            num = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num3) : UnitScale.FromFeetToMeters(num3));
                                        }
                                        num3 = num;
                                        lengthStringFromUnitSystem = unitScale1.ToLengthStringFromUnitSystem(num3, false, true, true);
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                                    {
                                        if (unitScale.ScaleSystemType == scaleSystemType)
                                        {
                                            num1 = num3;
                                        }
                                        else
                                        {
                                            num1 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet(num3) : UnitScale.FromSquareFeetToSquareMeters(num3));
                                        }
                                        num3 = num1;
                                        lengthStringFromUnitSystem = unitScale1.ToAreaStringFromUnitSystem(num3, true);
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                                    {
                                        if (unitScale.ScaleSystemType == scaleSystemType)
                                        {
                                            num2 = num3;
                                        }
                                        else
                                        {
                                            num2 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromCubicMetersToCubicFeet(num3) : UnitScale.FromCubicFeetToCubicMeters(num3));
                                        }
                                        num3 = num2;
                                        lengthStringFromUnitSystem = unitScale1.ToCubicStringFromUnitSystem(num3, true);
                                        break;
                                    }
                                default:
                                    {
                                        lengthStringFromUnitSystem = unitScale1.Round(num3).ToString();
                                        lengthStringFromUnitSystem = string.Concat(lengthStringFromUnitSystem, (estimatingItem.PurchaseUnit != string.Empty ? string.Concat(" ", estimatingItem.PurchaseUnit) : ""));
                                        break;
                                    }
                            }
                        }
                        preset.Fields.Add(new PresetField(empty, lengthStringFromUnitSystem));
                    }
                }
            }
        }

        public void RecalcLayout()
        {
            this.UpdateContent = true;
            base.DrawArea.DrawingBoard.Refresh();
        }

        private void TransformIcon(GraphicsPath gp, int offsetX, int offsetY)
        {
            float single = (float)this.fontSize / 64f;
            Matrix matrix = new Matrix();
            matrix.Scale(single, single, MatrixOrder.Append);
            matrix.Translate((float)(offsetX - (int)((float)offsetX * single)), (float)(offsetY - (int)((float)offsetY * single)), MatrixOrder.Append);
            gp.Transform(matrix);
        }

        public void UpdateFontSize(int fontSize)
        {
            this.FontSize = fontSize;
            this.RecalcLayout();
        }

        private void UpdateGroups(Graphics g, int offsetX, int offsetY, Rectangle rectangle)
        {
            int width;
            int num = 0;
            int fontSize = this.padding;
            int penWidth = 0;
            int penWidth1 = 0;
            int num1 = 0;
            int x = rectangle.X;
            int penWidth2 = base.PenWidth / 2;
            int y = rectangle.Y;
            int num2 = base.PenWidth;
            int num3 = (base.ShowMeasure ? this.FontSize * 3 : this.FontSize);
            int fontSize1 = (int)((float)this.FontSize * 0.85f);
            this.ClearGroups();
            this.ColumnMarginLeft.Clear();
            this.ColumnWidth.Clear();
            using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic))
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Trimming = StringTrimming.EllipsisWord;
                stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                ArrayList arrayLists = new ArrayList();
                SizeF sizeF = g.MeasureString(base.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize), FontStyle.Bold), -1, stringFormat);
                num = sizeF.ToSize().Width;
                foreach (Layer collection in this.Plan.Layers.Collection)
                {
                    if (!collection.Visible)
                    {
                        continue;
                    }
                    foreach (DrawObject drawObject in collection.DrawingObjects.Collection)
                    {
                        if (!drawObject.Visible || !drawObject.IsPartOfGroup() || arrayLists.Contains(drawObject.GroupID))
                        {
                            continue;
                        }
                        GroupStats groupStat = GroupUtilities.ComputeGroupStats(this.Plan, drawObject, this.Plan.UnitScale.ScaleSystemType, true, "");
                        arrayLists.Add(drawObject.GroupID);
                        string str = (base.ShowMeasure ? this.GetDrawObjectBasicInfo(drawObject, groupStat) : string.Empty);
                        DrawObjectGroup drawObjectGroup = new DrawObjectGroup(drawObject.GroupID, drawObject.Name, drawObject.ObjectType, drawObject.Color, str, drawObject);
                        this.Groups.Add(drawObjectGroup);
                    }
                }
                this.Groups.Collection.Sort(new DrawLegend.GroupSorter());
                foreach (DrawObjectGroup collection1 in this.Groups.Collection)
                {
                    DrawObject tag = (DrawObject)collection1.Tag;
                    GroupStats groupStat1 = GroupUtilities.ComputeGroupStats(this.Plan, tag, this.Plan.UnitScale.ScaleSystemType, true, "");
                    arrayLists.Add(tag.GroupID);
                    if (base.ShowMeasure)
                    {
                        this.GetDrawObjectBasicInfo(tag, groupStat1);
                    }
                    else
                    {
                        string empty = string.Empty;
                    }
                    sizeF = g.MeasureString(collection1.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), -1, stringFormat);
                    int width1 = sizeF.ToSize().Width;
                    if (!base.ShowMeasure)
                    {
                        width = 0;
                    }
                    else
                    {
                        sizeF = g.MeasureString(collection1.BasicInfo, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize)), -1, stringFormat);
                        width = sizeF.ToSize().Width;
                    }
                    int num4 = fontSize1 + width1 + width + num3;
                    num = (num4 > num ? num4 : num);
                    fontSize += (int)((float)this.FontSize * 0.9f);
                    if (base.ShowMeasure)
                    {
                        this.QueryExtensionsResults(tag, collection1, groupStat1, this.Plan.UnitScale);
                        foreach (Preset preset in collection1.Presets.Collection)
                        {
                            if (preset.Fields.Collection.Count > 0)
                            {
                                fontSize += (int)((float)this.FontSize * 0.96f);
                            }
                            foreach (PresetField presetField in preset.Fields.Collection)
                            {
                                sizeF = g.MeasureString(presetField.Name, new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), -1, stringFormat);
                                width1 = sizeF.ToSize().Width;
                                sizeF = g.MeasureString(presetField.Value.ToString(), new Font("Tahoma", Utilities.FontSizeInPoints((float)this.FontSize * 0.9f)), -1, stringFormat);
                                width = sizeF.ToSize().Width;
                                num4 = fontSize1 + width1 + width + num3;
                                num = (num4 > num ? num4 : num);
                                fontSize += (int)((float)this.FontSize * 0.9f);
                            }
                        }
                    }
                    if (collection1.Presets.Count > 0)
                    {
                        fontSize = fontSize + this.padding / 2;
                    }
                    num1++;
                    if (num1 != this.MaxRows)
                    {
                        continue;
                    }
                    this.ColumnMarginLeft.Add(penWidth1 + this.padding);
                    this.ColumnWidth.Add(num);
                    penWidth1 = penWidth1 + num + this.padding;
                    fontSize += this.padding;
                    penWidth = (fontSize > penWidth ? fontSize : penWidth);
                    num = 0;
                    fontSize = this.padding;
                    num1 = 0;
                }
                fontSize += this.padding;
                arrayLists.Clear();
                arrayLists = null;
            }
            if (num1 > 0)
            {
                this.ColumnMarginLeft.Add(penWidth1 + this.padding);
                if (this.MaxRows > this.Groups.Count)
                {
                    this.ColumnWidth.Add(num);
                }
                else
                {
                    this.ColumnWidth.Add(num + this.padding);
                }
                penWidth1 = penWidth1 + num + this.padding;
            }
            penWidth = (penWidth < fontSize ? fontSize : penWidth);
            penWidth1 = penWidth1 + base.PenWidth + this.padding * 2;
            penWidth = penWidth + base.PenWidth * 2 + this.padding * 2;
            if (!this.MustSetLocation)
            {
                base.SetRectangle(rectangle.X, rectangle.Y, penWidth1, penWidth);
                return;
            }
            base.SetRectangle(100, 100, penWidth1, penWidth);
            this.MustSetLocation = false;
        }

        public void UpdateMaxRows(int maxRows)
        {
            this.MaxRows = maxRows;
            this.RecalcLayout();
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

        private class GroupSorter : IComparer
        {
            public GroupSorter()
            {
            }

            public int Compare(object x, object y)
            {
                int num;
                try
                {
                    DrawObjectGroup drawObjectGroup = x as DrawObjectGroup;
                    DrawObjectGroup drawObjectGroup1 = y as DrawObjectGroup;
                    DrawObject tag = (DrawObject)drawObjectGroup.Tag;
                    DrawObject drawObject = (DrawObject)drawObjectGroup1.Tag;
                    if (tag.ObjectType == drawObject.ObjectType)
                    {
                        num = StringLogicalComparer.Compare(tag.Name, drawObject.Name);
                    }
                    else
                    {
                        string str = tag.ObjectSortOrder.ToString();
                        int objectSortOrder = drawObject.ObjectSortOrder;
                        num = StringLogicalComparer.Compare(str, objectSortOrder.ToString());
                    }
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    num = -1;
                }
                return num;
            }
        }
    }
}