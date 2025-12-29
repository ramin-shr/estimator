using QuoterPlan.Properties;
using QuoterPlanControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DrawingArea
    {
        private int colorIndex = 26;

        private Array AreaColors = new ColorPicker.StandardColorEnum[] { ColorPicker.StandardColorEnum.ColorLime, ColorPicker.StandardColorEnum.ColorSteelBlue, ColorPicker.StandardColorEnum.ColorMagenta, ColorPicker.StandardColorEnum.ColorYellow, ColorPicker.StandardColorEnum.ColorRed, ColorPicker.StandardColorEnum.ColorTurquoise, ColorPicker.StandardColorEnum.ColorOrange, ColorPicker.StandardColorEnum.ColorPeru, ColorPicker.StandardColorEnum.ColorSilver, ColorPicker.StandardColorEnum.ColorChartreuse, ColorPicker.StandardColorEnum.ColorBlue, ColorPicker.StandardColorEnum.ColorHotPink, ColorPicker.StandardColorEnum.ColorKhaki, ColorPicker.StandardColorEnum.ColorViolet, ColorPicker.StandardColorEnum.ColorPaleTurquoise, ColorPicker.StandardColorEnum.ColorLightSalmon, ColorPicker.StandardColorEnum.ColorTan, ColorPicker.StandardColorEnum.ColorGainsboro, ColorPicker.StandardColorEnum.ColorGreen, ColorPicker.StandardColorEnum.ColorDarkBlue, ColorPicker.StandardColorEnum.ColorDarkRed, ColorPicker.StandardColorEnum.ColorGold, ColorPicker.StandardColorEnum.ColorPurple, ColorPicker.StandardColorEnum.ColorTeal, ColorPicker.StandardColorEnum.ColorOrangeRed, ColorPicker.StandardColorEnum.ColorSaddleBrown, ColorPicker.StandardColorEnum.ColorSlateGray };

        private Array PerimeterColors = new ColorPicker.StandardColorEnum[] { ColorPicker.StandardColorEnum.ColorBlue, ColorPicker.StandardColorEnum.ColorDarkRed, ColorPicker.StandardColorEnum.ColorGreen, ColorPicker.StandardColorEnum.ColorPurple, ColorPicker.StandardColorEnum.ColorTeal, ColorPicker.StandardColorEnum.ColorOrangeRed, ColorPicker.StandardColorEnum.ColorSaddleBrown, ColorPicker.StandardColorEnum.ColorGold, ColorPicker.StandardColorEnum.ColorSlateGray, ColorPicker.StandardColorEnum.ColorDarkBlue, ColorPicker.StandardColorEnum.ColorRed, ColorPicker.StandardColorEnum.ColorLime, ColorPicker.StandardColorEnum.ColorMagenta, ColorPicker.StandardColorEnum.ColorTurquoise, ColorPicker.StandardColorEnum.ColorOrange, ColorPicker.StandardColorEnum.ColorPeru, ColorPicker.StandardColorEnum.ColorYellow, ColorPicker.StandardColorEnum.ColorSilver, ColorPicker.StandardColorEnum.ColorSteelBlue, ColorPicker.StandardColorEnum.ColorHotPink, ColorPicker.StandardColorEnum.ColorChartreuse, ColorPicker.StandardColorEnum.ColorViolet, ColorPicker.StandardColorEnum.ColorPaleTurquoise, ColorPicker.StandardColorEnum.ColorLightSalmon, ColorPicker.StandardColorEnum.ColorTan, ColorPicker.StandardColorEnum.ColorKhaki, ColorPicker.StandardColorEnum.ColorGainsboro };

        private Array CounterColors = new ColorPicker.StandardColorEnum[] { ColorPicker.StandardColorEnum.ColorRed, ColorPicker.StandardColorEnum.ColorTurquoise, ColorPicker.StandardColorEnum.ColorChartreuse, ColorPicker.StandardColorEnum.ColorKhaki, ColorPicker.StandardColorEnum.ColorViolet, ColorPicker.StandardColorEnum.ColorPaleTurquoise, ColorPicker.StandardColorEnum.ColorLightSalmon, ColorPicker.StandardColorEnum.ColorTan, ColorPicker.StandardColorEnum.ColorGainsboro, ColorPicker.StandardColorEnum.ColorHotPink, ColorPicker.StandardColorEnum.ColorBlue, ColorPicker.StandardColorEnum.ColorLime, ColorPicker.StandardColorEnum.ColorYellow, ColorPicker.StandardColorEnum.ColorMagenta, ColorPicker.StandardColorEnum.ColorSteelBlue, ColorPicker.StandardColorEnum.ColorOrange, ColorPicker.StandardColorEnum.ColorPeru, ColorPicker.StandardColorEnum.ColorSilver, ColorPicker.StandardColorEnum.ColorDarkRed, ColorPicker.StandardColorEnum.ColorDarkBlue, ColorPicker.StandardColorEnum.ColorGreen, ColorPicker.StandardColorEnum.ColorGold, ColorPicker.StandardColorEnum.ColorPurple, ColorPicker.StandardColorEnum.ColorTeal, ColorPicker.StandardColorEnum.ColorOrangeRed, ColorPicker.StandardColorEnum.ColorSaddleBrown, ColorPicker.StandardColorEnum.ColorSlateGray };

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

        public DrawingObjects ActiveDrawingObjects
        {
            get
            {
                DrawingObjects drawingObjects;
                try
                {
                    drawingObjects = this.Layers[this.Layers.ActiveLayerIndex].DrawingObjects;
                }
                catch
                {
                    drawingObjects = null;
                }
                return drawingObjects;
            }
        }

        public Layer ActiveLayer
        {
            get
            {
                Layer item;
                try
                {
                    item = this.Layers[this.Layers.ActiveLayerIndex];
                }
                catch
                {
                    item = null;
                }
                return item;
            }
        }

        public int ActiveLayerIndex
        {
            get
            {
                int activeLayerIndex;
                try
                {
                    activeLayerIndex = this.Layers.ActiveLayerIndex;
                }
                catch
                {
                    activeLayerIndex = 0;
                }
                return activeLayerIndex;
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
                string name;
                try
                {
                    name = this.ActiveLayer.Name;
                }
                catch
                {
                    name = string.Empty;
                }
                return name;
            }
        }

        public int ActiveLayerOpacity
        {
            get
            {
                int opacity;
                try
                {
                    opacity = this.Layers[this.Layers.ActiveLayerIndex].Opacity;
                }
                catch
                {
                    opacity = 150;
                }
                return opacity;
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

        public bool CanRedo
        {
            get
            {
                bool canRedo;
                try
                {
                    canRedo = this.ActivePlan.CanRedo;
                }
                catch
                {
                    canRedo = false;
                }
                return canRedo;
            }
        }

        public bool CanUndo
        {
            get
            {
                bool canUndo;
                try
                {
                    canUndo = this.ActivePlan.CanUndo;
                }
                catch
                {
                    canUndo = false;
                }
                return canUndo;
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

        public DrawPolyLine DeductionParent
        {
            get
            {
                return this.deductionParent;
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

        public DrawPolyLine FirstSelectedArea
        {
            get
            {
                DrawPolyLine drawPolyLine;
                try
                {
                    DrawObject selectedObject = null;
                    for (int i = 0; i < this.SelectionCount; i++)
                    {
                        if (this.GetSelectedObject(i).ObjectType == "Area")
                        {
                            selectedObject = this.GetSelectedObject(i);
                        }
                    }
                    drawPolyLine = (DrawPolyLine)selectedObject;
                }
                catch
                {
                    drawPolyLine = null;
                }
                return drawPolyLine;
            }
        }

        public Layers Layers
        {
            get
            {
                Layers layers;
                try
                {
                    layers = this.ActivePlan.Layers;
                }
                catch
                {
                    layers = null;
                }
                return layers;
            }
        }

        public Point MouseLocation
        {
            get
            {
                Point point;
                try
                {
                    DrawingBoard drawingBoard = this.DrawingBoard;
                    int x = Cursor.Position.X;
                    Point position = Cursor.Position;
                    point = this.BackTrackMouse(drawingBoard.PointToClient(new Point(x, position.Y)));
                }
                catch
                {
                    point = new Point(0, 0);
                }
                return point;
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

        public bool PanningFromPanTool
        {
            get
            {
                if (!this._panningInProgress)
                {
                    return false;
                }
                return this.ActiveTool == DrawingArea.DrawToolType.Pan;
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

        public DrawPolyLine SelectedArea
        {
            get
            {
                DrawPolyLine drawPolyLine;
                try
                {
                    DrawObject selectedObject = this.SelectedObject;
                    if (selectedObject.ObjectType != "Area")
                    {
                        drawPolyLine = null;
                    }
                    else
                    {
                        drawPolyLine = (DrawPolyLine)selectedObject;
                    }
                }
                catch
                {
                    drawPolyLine = null;
                }
                return drawPolyLine;
            }
        }

        public DrawLegend SelectedLegend
        {
            get
            {
                DrawLegend drawLegend;
                try
                {
                    DrawObject selectedObject = this.SelectedObject;
                    if (selectedObject.ObjectType != "Legend")
                    {
                        drawLegend = null;
                    }
                    else
                    {
                        drawLegend = (DrawLegend)selectedObject;
                    }
                }
                catch
                {
                    drawLegend = null;
                }
                return drawLegend;
            }
        }

        public DrawObject SelectedObject
        {
            get
            {
                DrawObject selectedObject;
                try
                {
                    selectedObject = this.ActiveDrawingObjects.GetSelectedObject(0);
                }
                catch
                {
                    selectedObject = null;
                }
                return selectedObject;
            }
        }

        public DrawPolyLine SelectedPerimeter
        {
            get
            {
                DrawPolyLine drawPolyLine;
                try
                {
                    DrawObject selectedObject = this.SelectedObject;
                    if (selectedObject.ObjectType != "Perimeter")
                    {
                        drawPolyLine = null;
                    }
                    else
                    {
                        drawPolyLine = (DrawPolyLine)selectedObject;
                    }
                }
                catch
                {
                    drawPolyLine = null;
                }
                return drawPolyLine;
            }
        }

        public DrawPolyLine SelectedPolyLine
        {
            get
            {
                DrawPolyLine drawPolyLine;
                try
                {
                    DrawObject selectedObject = this.SelectedObject;
                    if (selectedObject.ObjectType == "Area" || selectedObject.ObjectType == "Perimeter")
                    {
                        drawPolyLine = (DrawPolyLine)selectedObject;
                    }
                    else
                    {
                        drawPolyLine = null;
                    }
                }
                catch
                {
                    drawPolyLine = null;
                }
                return drawPolyLine;
            }
        }

        public int SelectionCount
        {
            get
            {
                int selectionCount;
                try
                {
                    selectionCount = this.ActiveDrawingObjects.SelectionCount;
                }
                catch
                {
                    selectionCount = 0;
                }
                return selectionCount;
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

        public ToolSettings ToolSettings
        {
            get
            {
                return this.toolSettings;
            }
        }

        public UnitScale UnitScale
        {
            get
            {
                UnitScale unitScale;
                try
                {
                    unitScale = this.ActivePlan.UnitScale;
                }
                catch
                {
                    unitScale = null;
                }
                return unitScale;
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

        public float ZoomFactor
        {
            get
            {
                return (float)this.DrawingBoard.ZoomFactor;
            }
        }

        public DrawingArea()
        {
        }

        public void AddCommandToHistory(Command command)
        {
            if (this.ActivePlan != null)
            {
                this.ActivePlan.AddCommandToHistory(command);
            }
        }

        private void AddObjectDeductionsToList(DrawPolyLine drawPolyLine)
        {
            foreach (DrawObject deductionArray in drawPolyLine.DeductionArray)
            {
                this.InsertObject(deductionArray, this.ActiveLayerIndex, true, true);
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

        public Point BackTrackMouse(Point p)
        {
            Point[] pointArray = new Point[] { p };
            Matrix matrix = new Matrix();
            float width = (float)(-this.ClientSize.Width) / 2f;
            Size clientSize = this.ClientSize;
            matrix.Translate(width, (float)(-clientSize.Height) / 2f, MatrixOrder.Append);
            matrix.Rotate(this._rotation, MatrixOrder.Append);
            float single = (float)this.ClientSize.Width / 2f;
            Size size = this.ClientSize;
            matrix.Translate(single, (float)size.Height / 2f, MatrixOrder.Append);
            matrix.Scale((float)this.DrawingBoard.ZoomFactor, (float)this.DrawingBoard.ZoomFactor, MatrixOrder.Append);
            matrix.Translate((float)this.drawingBoard.HorizontalOffset, (float)this.drawingBoard.VerticalOffset, MatrixOrder.Append);
            matrix.Invert();
            matrix.TransformPoints(pointArray);
            return pointArray[0];
        }

        public void Clear()
        {
            this.colorIndex = 26;
            this.clipboard.Clear();
            this.clipboard.SelectedOpening = null;
        }

        public void ClearHistory()
        {
            if (this.ActivePlan != null)
            {
                this.ActivePlan.ClearHistory();
            }
        }

        public bool ColorAvailable(string objectType, Color color)
        {
            bool flag;
            IEnumerator enumerator = this.project.Plans.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    IEnumerator enumerator1 = ((Plan)enumerator.Current).Layers.Collection.GetEnumerator();
                    try
                    {
                        while (enumerator1.MoveNext())
                        {
                            IEnumerator enumerator2 = ((Layer)enumerator1.Current).DrawingObjects.Collection.GetEnumerator();
                            try
                            {
                                while (enumerator2.MoveNext())
                                {
                                    DrawObject current = (DrawObject)enumerator2.Current;
                                    if (!(objectType == current.ObjectType) || color.R != current.Color.R || color.G != current.Color.G || color.B != current.Color.B)
                                    {
                                        continue;
                                    }
                                    flag = false;
                                    return flag;
                                }
                            }
                            finally
                            {
                                IDisposable disposable = enumerator2 as IDisposable;
                                if (disposable != null)
                                {
                                    disposable.Dispose();
                                }
                            }
                        }
                    }
                    finally
                    {
                        IDisposable disposable1 = enumerator1 as IDisposable;
                        if (disposable1 != null)
                        {
                            disposable1.Dispose();
                        }
                    }
                }
                return true;
            }
            finally
            {
                IDisposable disposable2 = enumerator as IDisposable;
                if (disposable2 != null)
                {
                    disposable2.Dispose();
                }
            }
            return flag;
        }

        public int CreateNewLayer(string layerName)
        {
            if (this.Layers == null)
            {
                return -1;
            }
            int num = this.Layers.CreateNewLayer(layerName, 150);
            this.AddCommandToHistory(new CommandAddLayer(this, layerName));
            return num;
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
            this.InitializeTool(DrawingArea.DrawToolType.Deduction, Utilities.LoadCursor("Deduction.cur", new Cursor(this.GetType(), "Rectangle.cur")));
        }

        public void DeductionParentRelease()
        {
            if (this.deductionParent != null)
            {
                this.RemoveAllDeductionsFromList();
                this.deductionParent.EditDeductions = false;
                this.Owner.RefreshObject(this.deductionParent);
                foreach (DrawObject deductionArray in this.deductionParent.DeductionArray)
                {
                    deductionArray.Visible = false;
                    deductionArray.Selected = false;
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

        public void DeductionSaveCommand()
        {
            if (this.actionCommand != null)
            {
                this.actionCommand.NewState();
                this.AddCommandToHistory(this.actionCommand);
                this.actionCommand = null;
            }
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
            ((DrawPolyLine)((DrawPolyLine)drawObject).DeductionArray[0]).Selected = true;
            this.SelectPointerTool();
            this.CursorRestricted = true;
            this.Refresh();
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
                foreach (DrawObject showMeasure in deductionArray)
                {
                    showMeasure.ShowMeasure = this.deductionParent.ShowMeasure;
                    showMeasure.Visible = true;
                    showMeasure.Selected = false;
                }
            }
            if (this.OnDeductionsParentSet != null)
            {
                this.OnDeductionsParentSet();
            }
        }

        public void Delete(bool logCommand)
        {
            if (this.Layers == null)
            {
                return;
            }
            Command commandDelete = null;
            if (logCommand && this.deductionParent == null)
            {
                commandDelete = new CommandDelete(this, this.ActiveLayerName);
            }
            if (this.ActiveDrawingObjects.DeleteSelection())
            {
                if (commandDelete != null)
                {
                    this.AddCommandToHistory(commandDelete);
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
                if (drawPolyLine.Pattern != HatchStylePickerCombo.HatchStylePickerEnum.Solid)
                {
                    g.FillPath(new HatchBrush((HatchStyle)drawPolyLine.Pattern, Color.FromArgb(250, drawPolyLine.FillColor), Color.FromArgb(220, Color.White)), graphicsPath);
                }
                else
                {
                    g.FillPath(new SolidBrush(Color.FromArgb(220, drawPolyLine.FillColor)), graphicsPath);
                }
                g.DrawPath(new Pen(Color.FromArgb(0xff, drawObject.Color), 2f), graphicsPath);
            }
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
                if (drawPolyLine.Pattern != HatchStylePickerCombo.HatchStylePickerEnum.Solid)
                {
                    g.FillPath(new HatchBrush((HatchStyle)drawPolyLine.Pattern, Color.FromArgb(170, drawPolyLine.FillColor), Color.White), graphicsPath);
                }
                else
                {
                    g.FillPath(new SolidBrush(Color.FromArgb(170, drawPolyLine.FillColor)), graphicsPath);
                }
                using (Pen pen = new Pen(Color.FromArgb(0xff, drawObject.Color), 2f))
                {
                    pen.StartCap = LineCap.Square;
                    pen.Alignment = PenAlignment.Inset;
                    g.DrawPath(pen, graphicsPath);
                }
            }
        }

        private void DrawCounterCursor(DrawObject drawObject, Graphics g)
        {
            this.DrawCursorCrossHair(g);
            int num = 20;
            Point point = new Point(19, 19);
            DrawCounter.CounterShapeTypeEnum shape = ((DrawCounter)drawObject).Shape;
            Rectangle rectangle = new Rectangle(point.X - num / 2, point.Y - num / 2, num, num);
            using (GraphicsPath graphicsPath = new GraphicsPath())
            {
                switch (shape)
                {
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
                        {
                            graphicsPath.AddRectangle(rectangle);
                            point = new Point(point.X, point.Y + 1);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(rectangle));
                            point = new Point(point.X, point.Y + 1);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rectangle, DrawCounter.Direction.Up));
                            point = new Point(point.X, point.Y + 3);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rectangle, DrawCounter.Direction.Down));
                            point = new Point(point.X, point.Y - 2);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rectangle, DrawCounter.Direction.Up));
                            point = new Point(point.X, point.Y + 1);
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
                            point = new Point(point.X, point.Y + 1);
                            break;
                        }
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
                g.DrawString(text, new Font("Tahoma", Utilities.FontSizeInPoints(7f), FontStyle.Bold), new SolidBrush(Color.FromArgb(250, Color.Black)), point, stringFormat);
            }
        }

        private void DrawCounterIcon(DrawObject drawObject, Graphics g)
        {
            int num = 15;
            PointF pointF = new PointF(8f, 7f);
            DrawCounter.CounterShapeTypeEnum shape = ((DrawCounter)drawObject).Shape;
            RectangleF rectangleF = new RectangleF(pointF.X - (float)(num / 2), pointF.Y - (float)(num / 2), (float)num, (float)num);
            using (GraphicsPath graphicsPath = new GraphicsPath())
            {
                switch (shape)
                {
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
                        {
                            graphicsPath.AddRectangle(rectangleF);
                            pointF = new PointF(pointF.X + 1f, pointF.Y + 1f);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
                        {
                            rectangleF = new RectangleF(pointF.X - 7.75f, pointF.Y - 7.75f, 15.5f, 15.5f);
                            graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(Rectangle.Truncate(rectangleF)));
                            pointF = new PointF(pointF.X, pointF.Y + 1f);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Up));
                            pointF = new PointF(pointF.X + 1f, pointF.Y + 3f);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Down));
                            pointF = new PointF(pointF.X + 1f, pointF.Y - 2f);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Up));
                            pointF = new PointF(pointF.X + 1f, pointF.Y + 1f);
                            break;
                        }
                    case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
                        {
                            graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(Rectangle.Truncate(rectangleF), DrawCounter.Direction.Down));
                            pointF = new PointF(pointF.X + 1f, pointF.Y);
                            break;
                        }
                    default:
                        {
                            graphicsPath.AddEllipse(rectangleF);
                            pointF = new PointF(pointF.X + 0.5f, pointF.Y + 1f);
                            break;
                        }
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
                g.DrawString(text, new Font("Tahoma", Utilities.FontSizeInPoints(6f), FontStyle.Bold), new SolidBrush(Color.FromArgb(250, Color.Black)), pointF, stringFormat);
            }
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
            using (GraphicsPath graphicsPath1 = new GraphicsPath())
            {
                graphicsPath1.AddLine(new Point(5, 2), new Point(5, 4));
                graphicsPath1.CloseFigure();
                graphicsPath1.AddLine(new Point(6, 5), new Point(8, 5));
                graphicsPath1.CloseFigure();
                graphicsPath1.AddLine(new Point(5, 6), new Point(5, 8));
                graphicsPath1.CloseFigure();
                graphicsPath1.AddLine(new Point(2, 5), new Point(4, 5));
                graphicsPath1.CloseFigure();
                g.DrawPath(new Pen(Color.Black), graphicsPath1);
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
            using (GraphicsPath graphicsPath1 = new GraphicsPath())
            {
                graphicsPath1.AddRectangle(new Rectangle(6, 23, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(24, 23, 4, 4));
                g.FillPath(new SolidBrush(Color.FromArgb(240, 58, 58, 58)), graphicsPath1);
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
            using (GraphicsPath graphicsPath1 = new GraphicsPath())
            {
                graphicsPath1.AddRectangle(new Rectangle(0, 6, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(13, 6, 4, 4));
                g.FillPath(new SolidBrush(Color.FromArgb(200, Color.Black)), graphicsPath1);
            }
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
            matrix.Translate((float)(-clientSize.Width) / 2f, (float)(-clientSize.Height) / 2f, MatrixOrder.Append);
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
            Rectangle netRectangle = this.NetRectangle;
            int x = netRectangle.X;
            PointF origin = this.DrawingBoard.Origin;
            netRectangle.X = x + ((int)origin.X - offsetX);
            int y = netRectangle.Y;
            PointF pointF = this.DrawingBoard.Origin;
            netRectangle.Y = y + ((int)pointF.Y - offsetY);
            Pen pen = new Pen(Color.FromArgb(0xff, Color.Orange), 1f);
            Brush solidBrush = new SolidBrush(Color.FromArgb(50, Color.Orange));
            g.DrawRectangle(pen, netRectangle);
            g.FillRectangle(solidBrush, netRectangle);
            solidBrush.Dispose();
            pen.Dispose();
        }

        public void DrawObjectIcon(DrawObject drawObject, Graphics g)
        {
            string objectType = drawObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                switch (str)
                {
                    case "Line":
                        {
                            g.SmoothingMode = SmoothingMode.None;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            this.DrawDistanceIcon(drawObject, g);
                            return;
                        }
                    case "Rectangle":
                        {
                            g.DrawImage(Resources.marker_small, Point.Empty);
                            return;
                        }
                    case "Area":
                        {
                            g.SmoothingMode = SmoothingMode.None;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            this.DrawAreaIcon(drawObject, g);
                            return;
                        }
                    case "Perimeter":
                        {
                            g.SmoothingMode = SmoothingMode.None;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            this.DrawPerimeterIcon(drawObject, g);
                            return;
                        }
                    case "Counter":
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            this.DrawCounterIcon(drawObject, g);
                            return;
                        }
                    case "Angle":
                        {
                            g.DrawImage(Resources.angle_small, Point.Empty);
                            return;
                        }
                    case "Note":
                        {
                            g.DrawImage(Resources.note_small, Point.Empty);
                            return;
                        }
                    case "Legend":
                        {
                            g.DrawImage(Resources.legend_small, Point.Empty);
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }

        private void DrawObjectTypeCursor(DrawObject drawObject, Graphics g)
        {
            string objectType = drawObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                if (str == "Area")
                {
                    g.SmoothingMode = SmoothingMode.None;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    this.DrawAreaCursor(drawObject, g);
                    return;
                }
                if (str == "Perimeter")
                {
                    g.SmoothingMode = SmoothingMode.None;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    this.DrawPerimeterCursor(drawObject, g);
                    return;
                }
                if (str == "Counter")
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    this.DrawCounterCursor(drawObject, g);
                    return;
                }
                if (str != "Line")
                {
                    return;
                }
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                this.DrawDistanceCursor(drawObject, g);
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
            using (GraphicsPath graphicsPath1 = new GraphicsPath())
            {
                graphicsPath1.AddRectangle(new Rectangle(11, 11, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(11, 28, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(28, 28, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(28, 17, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(18, 17, 4, 4));
                g.FillPath(new SolidBrush(Color.FromArgb(240, 58, 58, 58)), graphicsPath1);
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
            using (GraphicsPath graphicsPath1 = new GraphicsPath())
            {
                graphicsPath1.AddRectangle(new Rectangle(0, 0, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(5, 4, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(13, 4, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(0, 12, 4, 4));
                graphicsPath1.AddRectangle(new Rectangle(13, 12, 4, 4));
                g.FillPath(new SolidBrush(Color.FromArgb(200, Color.Black)), graphicsPath1);
            }
        }

        public int FindGroupLayer(int groupID)
        {
            for (int i = 0; i < this.Layers.Count; i++)
            {
                if (this.FindObjectFromGroupID(this.Layers[i], groupID) != null)
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
                if (this.FindObjectFromGroupID(plan.Layers[i], groupID) != null)
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
            DrawObject drawObject;
            foreach (Plan collection in this.project.Plans.Collection)
            {
                foreach (Layer layer in collection.Layers.Collection)
                {
                    foreach (DrawObject collection1 in layer.DrawingObjects.Collection)
                    {
                        if (!(collection1.Name == objectName) || collection1.DeductionParentID != -1)
                        {
                            continue;
                        }
                        drawObject = collection1;
                        return drawObject;
                    }
                }
            }
            if (lookAlsoInClipboard)
            {
                foreach (DrawObject drawObject1 in this.Clipboard.Objects.Collection)
                {
                    if (drawObject1.Name != objectName)
                    {
                        continue;
                    }
                    drawObject = drawObject1;
                    return drawObject;
                }
            }
            return null;
        }

        public DrawObject FindObjectFromGroupID(DrawingObjects drawingObjects, int groupID)
        {
            for (int i = drawingObjects.Collection.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)drawingObjects.Collection[i];
                if (item.GroupID == groupID && !item.IsDeduction())
                {
                    return item;
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
            DrawObject drawObject;
            IEnumerator enumerator = this.Layers.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject drawObject1 = this.FindObjectFromGroupID((Layer)enumerator.Current, groupID);
                    if (drawObject1 == null)
                    {
                        continue;
                    }
                    drawObject = drawObject1;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public DrawObject FindObjectFromGroupID(Plan plan, int groupID)
        {
            DrawObject drawObject;
            IEnumerator enumerator = plan.Layers.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject drawObject1 = this.FindObjectFromGroupID((Layer)enumerator.Current, groupID);
                    if (drawObject1 == null)
                    {
                        continue;
                    }
                    drawObject = drawObject1;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public DrawObject FindObjectFromGroupID(Project project, int groupID)
        {
            DrawObject drawObject;
            IEnumerator enumerator = project.Plans.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject drawObject1 = this.FindObjectFromGroupID((Plan)enumerator.Current, groupID);
                    if (drawObject1 == null)
                    {
                        continue;
                    }
                    drawObject = drawObject1;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public DrawObject FindObjectFromID(DrawingObjects drawingObjects, int ID)
        {
            for (int i = drawingObjects.Collection.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)drawingObjects.Collection[i];
                if (item.ID == ID)
                {
                    return item;
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
            DrawObject drawObject;
            IEnumerator enumerator = this.Layers.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject drawObject1 = this.FindObjectFromID((Layer)enumerator.Current, ID);
                    if (drawObject1 == null)
                    {
                        continue;
                    }
                    drawObject = drawObject1;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public DrawObject FindObjectFromID(Plan plan, int ID)
        {
            DrawObject drawObject;
            IEnumerator enumerator = plan.Layers.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject drawObject1 = this.FindObjectFromID((Layer)enumerator.Current, ID);
                    if (drawObject1 == null)
                    {
                        continue;
                    }
                    drawObject = drawObject1;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public DrawObject FindObjectFromID(Project project, int ID)
        {
            DrawObject drawObject;
            IEnumerator enumerator = project.Plans.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject drawObject1 = this.FindObjectFromID((Plan)enumerator.Current, ID);
                    if (drawObject1 == null)
                    {
                        continue;
                    }
                    drawObject = drawObject1;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public void FlagDeletedGroups()
        {
            this.project.FlagDeletedGroups();
        }

        public Color GetAngleDefaultColor()
        {
            return Color.FromKnownColor(KnownColor.DarkRed);
        }

        public Image GetDrawObjectIcon(DrawObject drawObject)
        {
            Image distanceSmall = null;
            string objectType = drawObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                switch (str)
                {
                    case "Line":
                        {
                            distanceSmall = Resources.distance_small;
                            break;
                        }
                    case "Rectangle":
                        {
                            distanceSmall = Resources.marker_small;
                            break;
                        }
                    case "Area":
                        {
                            distanceSmall = Resources.area_small;
                            break;
                        }
                    case "Perimeter":
                        {
                            distanceSmall = Resources.perimeter_small;
                            break;
                        }
                    case "Counter":
                        {
                            distanceSmall = Resources.counter_small;
                            break;
                        }
                    case "Angle":
                        {
                            distanceSmall = Resources.angle_small;
                            break;
                        }
                    case "Note":
                        {
                            distanceSmall = Resources.note_small;
                            break;
                        }
                    case "Legend":
                        {
                            distanceSmall = Resources.legend_small;
                            break;
                        }
                }
            }
            return distanceSmall;
        }

        public string GetFreeObjectName(string prefix)
        {
            string str = "";
            return this.GetFreeObjectName(prefix, ref str);
        }

        public string GetFreeObjectName(string prefix, ref string suffix)
        {
            int num = 0;
            string str = string.Concat(prefix, " ");
            do
            {
                num++;
            }
            while (this.FindObjectByName(string.Concat(str, num), true) != null);
            suffix = num.ToString();
            return string.Concat(str, num);
        }

        public string GetFreeObjectName(DrawObject drawObject, ref string text)
        {
            string freeObjectName = "";
            string objectType = drawObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                switch (str)
                {
                    case "Line":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Distance);
                            break;
                        }
                    case "Area":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Surface);
                            break;
                        }
                    case "Perimeter":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Périmètre);
                            break;
                        }
                    case "Counter":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Compteur, ref text);
                            break;
                        }
                    case "Rectangle":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Marqueur);
                            break;
                        }
                    case "Angle":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Angle);
                            break;
                        }
                    case "Note":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Note);
                            break;
                        }
                    case "Legend":
                        {
                            freeObjectName = this.GetFreeObjectName(Resources.Légende);
                            break;
                        }
                }
            }
            return freeObjectName;
        }

        public string GetFreeTemplateObjectName(string prefix)
        {
            string str = "";
            return this.GetFreeTemplateObjectName(prefix, ref str);
        }

        public string GetFreeTemplateObjectName(string prefix, ref string suffix)
        {
            int num = 0;
            string str = string.Concat(Utilities.StripIndexFromString(prefix), " ");
            do
            {
                num++;
            }
            while (this.FindObjectByName(string.Concat(str, num), true) != null);
            suffix = num.ToString();
            return string.Concat(str, num);
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

        public Layer GetLayer(int index)
        {
            Layer item;
            try
            {
                item = this.Layers[index];
            }
            catch
            {
                item = null;
            }
            return item;
        }

        public Color GetMarkerDefaultColor()
        {
            return Color.FromKnownColor(KnownColor.Yellow);
        }

        public Color GetNextColor(string objectType)
        {
            Color nextColorEx;
            bool flag;
            bool count = true;
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(this.GetMarkerDefaultColor());
            foreach (Plan collection in this.project.Plans.Collection)
            {
                foreach (Layer layer in collection.Layers.Collection)
                {
                    foreach (DrawObject drawObject in layer.DrawingObjects.Collection)
                    {
                        if (!ColorPicker.IsStandardColor(drawObject.Color))
                        {
                            continue;
                        }
                        object[] r = new object[] { drawObject.Color.R, ".", drawObject.Color.G, ".", drawObject.Color.B };
                        string str = string.Concat(r);
                        if (arrayLists.Contains(str))
                        {
                            continue;
                        }
                        arrayLists.Add(str);
                    }
                }
            }
            count = arrayLists.Count < 27;
            arrayLists.Clear();
            arrayLists = null;
            if (!count)
            {
                return this.GetNextColorEx(objectType);
            }
            this.ColorIndex = -1;
            do
            {
                flag = false;
                nextColorEx = this.GetNextColorEx(objectType);
                foreach (Plan plan in this.project.Plans.Collection)
                {
                    foreach (Layer collection1 in plan.Layers.Collection)
                    {
                        foreach (DrawObject drawObject1 in collection1.DrawingObjects.Collection)
                        {
                            if (nextColorEx.R != drawObject1.Color.R || nextColorEx.G != drawObject1.Color.G || nextColorEx.B != drawObject1.Color.B)
                            {
                                continue;
                            }
                            flag = true;
                            break;
                        }
                    }
                }
            }
            while (flag);
            return nextColorEx;
        }

        private Color GetNextColorEx(string objectType)
        {
            DrawingArea colorIndex = this;
            colorIndex.ColorIndex = colorIndex.ColorIndex + 1;
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

        public int GetNextGroupID()
        {
            Project groupCounter = this.project;
            groupCounter.GroupCounter = groupCounter.GroupCounter + 1;
            return this.project.GroupCounter;
        }

        public int GetNextObjectID()
        {
            Project objectCounter = this.project;
            objectCounter.ObjectCounter = objectCounter.ObjectCounter + 1;
            return this.project.ObjectCounter;
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
            int num1 = 0;
            while (num1 < this.ActiveDrawingObjects.Count)
            {
                if (this.ActiveDrawingObjects[num1].ID != selectedObject.ID)
                {
                    num1++;
                }
                else
                {
                    num = num1;
                    break;
                }
            }
            if (num == -1)
            {
                return selectedObject;
            }
            int count = num;
            do
            {
                if (getPrevious)
                {
                    if (count + 1 != this.ActiveDrawingObjects.Count)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                else if (count - 1 >= 0)
                {
                    count--;
                }
                else
                {
                    count = this.ActiveDrawingObjects.Count - 1;
                }
                if (count == num)
                {
                    return selectedObject;
                }
                if (!this.ActiveDrawingObjects[count].IsDeduction())
                {
                    if (!byObjectType)
                    {
                        continue;
                    }
                    if (this.ActiveDrawingObjects[count].ObjectType == selectedObject.ObjectType)
                    {
                        return this.ActiveDrawingObjects[count];
                    }
                    else
                    {
                        goto Label0;
                    }
                }
                else
                {
                    goto Label0;
                }
            }
            while (!selectedObject.IsPartOfGroup() || this.ActiveDrawingObjects[count].GroupID != selectedObject.GroupID);
            return this.ActiveDrawingObjects[count];
        }

        public Color GetNoteDefaultColor()
        {
            return Color.FromKnownColor(KnownColor.Khaki);
        }

        public DrawObject GetObjectByID(Plan plan, int objectID)
        {
            DrawObject drawObject;
            if (plan != null && objectID != -1)
            {
                IEnumerator enumerator = plan.Layers.Collection.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        DrawObject objectByID = ((Layer)enumerator.Current).DrawingObjects.GetObjectByID(objectID);
                        if (objectByID == null)
                        {
                            continue;
                        }
                        drawObject = objectByID;
                        return drawObject;
                    }
                    return null;
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                return drawObject;
            }
            return null;
        }

        public DrawObjectGroup GetObjectGroup(DrawObject drawObject)
        {
            DrawObjectGroup drawObjectGroup;
            if (drawObject.GroupID < 0)
            {
                return null;
            }
            IEnumerator enumerator = this.project.Groups.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObjectGroup current = (DrawObjectGroup)enumerator.Current;
                    if (current.ID != drawObject.GroupID)
                    {
                        continue;
                    }
                    drawObjectGroup = current;
                    return drawObjectGroup;
                }
                DrawObjectGroup drawObjectGroup1 = new DrawObjectGroup(drawObject.GroupID, drawObject.Name, drawObject.ObjectType, "");
                this.project.Groups.Add(drawObjectGroup1);
                return drawObjectGroup1;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObjectGroup;
        }

        public Layer GetObjectLayer(DrawObject drawObject)
        {
            Layer layer;
            try
            {
                foreach (Layer collection in this.Layers.Collection)
                {
                    foreach (DrawObject collection1 in collection.DrawingObjects.Collection)
                    {
                        if (!collection1.HasSameGroupOrID(drawObject))
                        {
                            continue;
                        }
                        layer = collection;
                        return layer;
                    }
                }
                layer = null;
            }
            catch
            {
                layer = null;
            }
            return layer;
        }

        public DrawObject GetSelectedObject(int index)
        {
            DrawObject selectedObject;
            try
            {
                selectedObject = this.ActiveDrawingObjects.GetSelectedObject(index);
            }
            catch
            {
                selectedObject = null;
            }
            return selectedObject;
        }

        public void Initialize(MainForm owner, Project project, DrawingBoard drawingBoard)
        {
            this.Project = project;
            this.Owner = owner;
            this.DrawingBoard = drawingBoard;
            this.activeTool = DrawingArea.DrawToolType.Pointer;
            this.tools = new Tool[] { new ToolPointer(), new ToolRectangle(), new ToolLine(), new ToolPolyLine(), new ToolDeduction(), new ToolOpening(), new ToolCounter(), new ToolAngle(), new ToolNote(), new ToolPan(), new ToolScale(), new ToolRectangle() };
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

        public void InsertObject(DrawObject o, int activeLayer, bool bringToFront, bool insertDeductionToList = false)
        {
            this.InsertObject(o, this.GetNextObjectID(), activeLayer, bringToFront, insertDeductionToList);
        }

        public void InsertObject(DrawObject o, int objectID, int activeLayer, bool bringToFront, bool insertDeductionToList = false)
        {
            ToolObject toolObject;
            string objectType = o.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                switch (str)
                {
                    case "Line":
                        {
                            toolObject = (ToolLine)this.tools[2];
                            break;
                        }
                    case "Rectangle":
                        {
                            toolObject = (ToolRectangle)this.tools[1];
                            break;
                        }
                    case "Area":
                    case "Perimeter":
                        {
                            toolObject = (ToolPolyLine)this.tools[3];
                            break;
                        }
                    case "Counter":
                        {
                            toolObject = (ToolCounter)this.tools[6];
                            break;
                        }
                    case "Angle":
                        {
                            toolObject = (ToolAngle)this.tools[7];
                            break;
                        }
                    case "Note":
                        {
                            toolObject = (ToolNote)this.tools[8];
                            break;
                        }
                    case "Legend":
                        {
                            toolObject = (ToolRectangle)this.tools[11];
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
                toolObject.InsertObject(o, objectID, activeLayer, bringToFront, insertDeductionToList);
                return;
            }
        }

        public Point IsPartOfDrop(DrawPolyLine drawPolyLine, Point point)
        {
            Point empty;
            try
            {
                int x = (int)this.DrawingBoard.Origin.X;
                PointF origin = this.DrawingBoard.Origin;
                empty = drawPolyLine.IsPartOfDrop(point, x, (int)origin.Y);
            }
            catch
            {
                empty = Point.Empty;
            }
            return empty;
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

        private Cursor LoadCursor(DrawObject drawObject, int xHotSpot, int yHotSpot, string defaultCursorName)
        {
            Cursor cursor;
            Cursor cursor1 = null;
            string objectType = drawObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                if (str == "Rectangle")
                {
                    cursor1 = Utilities.LoadCursor("Marker.cur", new Cursor(this.GetType(), "Rectangle.cur"));
                    cursor = cursor1 ?? new Cursor(this.GetType(), defaultCursorName);
                    return cursor;
                }
                else if (str == "Angle")
                {
                    cursor1 = Utilities.LoadCursor("Angle.cur", new Cursor(this.GetType(), "Pencil.cur"));
                    cursor = cursor1 ?? new Cursor(this.GetType(), defaultCursorName);
                    return cursor;
                }
                else
                {
                    if (str != "Note")
                    {
                        goto Label2;
                    }
                    cursor1 = Utilities.LoadCursor("Note.cur", new Cursor(this.GetType(), "Rectangle.cur"));
                    cursor = cursor1 ?? new Cursor(this.GetType(), defaultCursorName);
                    return cursor;
                }
            }
        Label2:
            try
            {
                using (Bitmap bitmap = new Bitmap(32, 32))
                {
                    using (Graphics graphic = Graphics.FromImage(bitmap))
                    {
                        this.DrawObjectTypeCursor(drawObject, graphic);
                        cursor1 = Utilities.LoadCursor(bitmap, xHotSpot, yHotSpot, ref this.cursorHandlePtr);
                    }
                }
            }
            catch
            {
                cursor1 = null;
            }
            cursor = cursor1 ?? new Cursor(this.GetType(), defaultCursorName);
            return cursor;
        }

        public void MapSelectedObjectsToGroup(DrawingObjects drawingObjects, DrawObject groupObject, int groupID, bool changeGroup)
        {
            string freeTemplateObjectName = "";
            Color color = groupObject.Color;
            int num = groupObject.GroupID;
            if (changeGroup)
            {
                freeTemplateObjectName = this.GetFreeTemplateObjectName(groupObject.Name);
                color = this.GetNextColor(groupObject.ObjectType);
            }
            bool flag = true;
            for (int i = 0; i < drawingObjects.SelectionCount; i++)
            {
                DrawObject selectedObject = drawingObjects.GetSelectedObject(i);
                if (selectedObject.GroupID == num)
                {
                    selectedObject.GroupID = groupID;
                    if (changeGroup)
                    {
                        selectedObject.Name = freeTemplateObjectName;
                        selectedObject.Color = color;
                        selectedObject.FillColor = color;
                        selectedObject.Group = null;
                        DrawObjectGroup group = selectedObject.Group;
                        DrawObjectGroup drawObjectGroup = this.Project.Groups.FindFromGroupID((num < -1 ? (num + 2) * -1 : num));
                        if (drawObjectGroup != null && flag)
                        {
                            foreach (Preset collection in drawObjectGroup.Presets.Collection)
                            {
                                Preset str = collection.Clone(false);
                                str.ID = Guid.NewGuid().ToString();
                                str.SynchWithTemplate(this.Project.ExtensionsSupport);
                                group.Presets.Add(str);
                            }
                            foreach (CEstimatingItem cEstimatingItem in drawObjectGroup.EstimatingItems.Collection)
                            {
                                group.EstimatingItems.Add(cEstimatingItem.Clone(true));
                            }
                            foreach (CEstimatingItem collection1 in drawObjectGroup.COfficeProducts.Collection)
                            {
                                group.EstimatingItems.Add(collection1.Clone(false));
                            }
                            this.project.EstimatingItems.CloneEstimatingItemsPrices(drawObjectGroup, group);
                            flag = false;
                        }
                    }
                    if (selectedObject.ObjectType == "Area" || selectedObject.ObjectType == "Perimeter")
                    {
                        foreach (DrawObject deductionArray in ((DrawPolyLine)selectedObject).DeductionArray)
                        {
                            deductionArray.GroupID = selectedObject.GroupID;
                            deductionArray.Opacity = selectedObject.Opacity;
                            deductionArray.DeductionParentID = selectedObject.ID;
                            deductionArray.SetSlopeFactor(selectedObject.SlopeFactor);
                        }
                    }
                }
            }
        }

        public Point NativePoint(Point point)
        {
            int x = point.X + (int)this.DrawingBoard.Origin.X;
            int y = point.Y;
            PointF origin = this.DrawingBoard.Origin;
            return new Point(x, y + (int)origin.Y);
        }

        public int ObjectsCount()
        {
            int num;
            try
            {
                int num1 = 0;
                for (int i = 0; i < this.ActivePlan.Layers.Count; i++)
                {
                    num1 += this.ObjectsCount(i);
                }
                num = num1;
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public int ObjectsCount(int layerIndex)
        {
            int num;
            try
            {
                int num1 = 0;
                foreach (DrawObject collection in this.ActivePlan.Layers[layerIndex].DrawingObjects.Collection)
                {
                    if (!(collection.ObjectType != "Legend") || collection.IsDeduction())
                    {
                        continue;
                    }
                    num1++;
                }
                num = num1;
            }
            catch
            {
                num = 0;
            }
            return num;
        }

        public void OnContextMenu(MouseEventArgs e)
        {
            if (this.Layers == null)
            {
                return;
            }
            Console.WriteLine("OnContextMenu");
            Point point = this.BackTrackMouse(new Point(e.X, e.Y));
            Point point1 = new Point(e.X, e.Y);
            int activeLayerIndex = this.Layers.ActiveLayerIndex;
            int count = this.Layers[activeLayerIndex].DrawingObjects.Count;
            DrawObject item = null;
            int num = 0;
            while (num < count)
            {
                if (!this.Layers[activeLayerIndex].DrawingObjects[num].Visible || this.Layers[activeLayerIndex].DrawingObjects[num].HitTest(point, (int)this.DrawingBoard.Origin.X, (int)this.DrawingBoard.Origin.Y) < 0)
                {
                    num++;
                }
                else
                {
                    item = this.Layers[activeLayerIndex].DrawingObjects[num];
                    break;
                }
            }
            if (item == null)
            {
                this.Layers[activeLayerIndex].DrawingObjects.UnselectAll();
                if (this.deductionParent != null)
                {
                    this.DeductionParentRelease();
                }
            }
            else
            {
                if (!item.Selected)
                {
                    this.Layers[activeLayerIndex].DrawingObjects.UnselectAll();
                }
                if (this.deductionParent != null)
                {
                    bool deductionParentID = true;
                    if (item.ObjectType == "Area" || item.ObjectType == "Perimeter")
                    {
                        deductionParentID = ((DrawPolyLine)item).DeductionParentID == -1;
                    }
                    if (deductionParentID)
                    {
                        this.DeductionParentRelease();
                    }
                }
                item.Selected = true;
            }
            Console.WriteLine(string.Concat("SelectionCount = ", this.Layers[activeLayerIndex].DrawingObjects.SelectionCount));
            this.DrawingBoard.Refresh();
            this.UpdateSelectedObjects();
            this.Owner.EditContextMenu(e);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            this.tools[(int)this.activeTool].OnKeyDown(e);
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
                Utilities.DisplayError(Resources.Ce_calque_est_invisible, Resources.Impossible_d_effectuer_l_opération_désirée);
                return;
            }
            MouseButtons button = e.Button;
            if (button == MouseButtons.Left)
            {
                this.tools[(int)this.activeTool].OnMouseDown(e);
                if (this.DrawingInProgress)
                {
                    return;
                }
            }
            else
            {
                if (button != MouseButtons.Right)
                {
                    return;
                }
                this.tools[(int)this.activeTool].OnMouseDown(e);
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
            this.InitializeTool(DrawingArea.DrawToolType.Opening, new Cursor(this.GetType(), "Pencil.cur"));
        }

        public void Redo()
        {
            if (this.ActivePlan != null)
            {
                this.ActivePlan.Redo();
                this.Refresh();
            }
        }

        public void Refresh()
        {
            this.DrawingBoard.Invalidate();
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

        public void RenameLayerFromUndoManager(string oldName, string newName)
        {
            if (this.ActivePlan != null)
            {
                this.ActivePlan.RenameLayerFromUndoManager(oldName, newName);
            }
        }

        public void RestrictCursor(Point point, Size size)
        {
            Cursor.Clip = new Rectangle(point, size);
            this.cursorRestricted = true;
        }

        private void RestrictCursor(bool value)
        {
            if (!value)
            {
                Cursor.Clip = Rectangle.Empty;
                return;
            }
            Point screen = this.DrawingBoard.PointToScreen(new Point(0, 0));
            Size size = new Size(this.DrawingBoard.Width, this.DrawingBoard.Height);
            Cursor.Clip = new Rectangle(screen, size);
        }

        public void SaveObject(DrawObject drawObject)
        {
            drawObject.Normalize();
            if (drawObject.DeductionParentID != -1)
            {
                this.Owner.SetModified();
                this.Owner.EnableEditCommands(true);
            }
            else
            {
                this.AddCommandToHistory(new CommandAdd(this, this.ActiveLayerName, drawObject));
                this.Owner.AddObjectToGUI(this.ActiveLayerIndex, drawObject, true);
                this.Owner.RefreshObject(drawObject);
            }
            if (this.activeTool == DrawingArea.DrawToolType.Deduction || this.activeTool == DrawingArea.DrawToolType.Opening || this.activeTool == DrawingArea.DrawToolType.Note)
            {
                this.SelectPointerTool();
            }
            this.Refresh();
        }

        private void ScaleCheckWarnings()
        {
            string inconsistanceEntreLeDPIHoritonzalEtLeDPIVerticalDétectée;
            string valeurDeRéférenceDuDPIHoritantalÉgaleÀ0;
            if (this.DrawingBoard.OriginalDpiX != this.DrawingBoard.OriginalDpiY)
            {
                inconsistanceEntreLeDPIHoritonzalEtLeDPIVerticalDétectée = Resources.Inconsistance_entre_le_DPI_horitonzal_et_le_DPI_vertical_détectée;
                valeurDeRéférenceDuDPIHoritantalÉgaleÀ0 = string.Format(Resources.Fonction_de_sélection_d_échelle_standard_désactivée, this.UnitScale.ReferenceDpiX, this.UnitScale.ReferenceDpiY);
                if (this.UnitScale.Scale == 0f)
                {
                    valeurDeRéférenceDuDPIHoritantalÉgaleÀ0 = string.Concat(valeurDeRéférenceDuDPIHoritantalÉgaleÀ0, "\n", Resources.Veuillez_utiliser_l_ajustement_manuel_pour_spécifier_l_échelle_appropriée);
                }
                Utilities.DisplayError(inconsistanceEntreLeDPIHoritonzalEtLeDPIVerticalDétectée, valeurDeRéférenceDuDPIHoritantalÉgaleÀ0);
                this.UnitScale.StandardScaleDisable = true;
            }
            if (this.DrawingBoard.OriginalDpiX == 0f)
            {
                inconsistanceEntreLeDPIHoritonzalEtLeDPIVerticalDétectée = Resources.DPI_horitonzal_invalide;
                valeurDeRéférenceDuDPIHoritantalÉgaleÀ0 = Resources.Valeur_de_référence_du_DPI_horitantal_égale_à_0;
                if (this.UnitScale.Scale == 0f)
                {
                    valeurDeRéférenceDuDPIHoritantalÉgaleÀ0 = string.Concat(valeurDeRéférenceDuDPIHoritantalÉgaleÀ0, "\n", Resources.Veuillez_utiliser_l_ajustement_manuel_pour_spécifier_l_échelle_appropriée);
                }
                Utilities.DisplayError(inconsistanceEntreLeDPIHoritonzalEtLeDPIVerticalDétectée, valeurDeRéférenceDuDPIHoritantalÉgaleÀ0);
                this.UnitScale.StandardScaleDisable = true;
            }
            if (Math.Ceiling((double)this.DrawingBoard.OriginalDpiX) < 96)
            {
                if (this.UnitScale.Scale == 0f)
                {
                    inconsistanceEntreLeDPIHoritonzalEtLeDPIVerticalDétectée = Resources.DPI_horitonzal_invalide;
                    valeurDeRéférenceDuDPIHoritantalÉgaleÀ0 = Resources.Valeur_de_référence_du_DPI_est_trop_petite;
                    valeurDeRéférenceDuDPIHoritantalÉgaleÀ0 = string.Concat(valeurDeRéférenceDuDPIHoritantalÉgaleÀ0, "\n", Resources.Veuillez_utiliser_l_ajustement_manuel_pour_spécifier_l_échelle_appropriée);
                    Utilities.DisplayError(inconsistanceEntreLeDPIHoritonzalEtLeDPIVerticalDétectée, valeurDeRéférenceDuDPIHoritantalÉgaleÀ0);
                }
                this.UnitScale.StandardScaleDisable = true;
            }
            if (this.UnitScale.StandardScaleDisable && !this.UnitScale.SetManually)
            {
                this.UnitScale.SetScale(0f, this.UnitScale.CurrentSystemType, this.UnitScale.Precision, false);
            }
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
                    string noteImportante = Resources.Note_importante;
                    Utilities.DisplayMessageCustom(noteImportante, Utilities.Certains_plans_peuvent_avoir_perdu_leur_intégrité, null, Resources.Je_comprends);
                }
            }
        }

        public void ScaleSetManually()
        {
            Utilities.DisplayMessage(Resources.Pour_ajuster_l_échelle_manuellement, Resources.Identifiez_une_mesure_linéaire_sur_le_plan);
            this.UnitScale.MustSetManually = true;
            this.SelectScaleTool();
        }

        public void ScaleValidate()
        {
            if (this.UnitScale.Scale == 0f)
            {
                Utilities.DisplayWarning(Resources.Échelle_non_configurée, Resources.L_échelle_pour_ce_plan_n_est_pas_encore_configurée);
            }
            this.ScaleCheckWarnings();
        }

        public void ScaleWasSetManually(int referenceDistance)
        {
            this.UnitScale.ReferenceDistance = referenceDistance;
            this.ScaleSet(true);
        }

        public int SegmentHitTest(DrawPolyLine drawPolyLine, Point point)
        {
            int num;
            try
            {
                int x = (int)this.DrawingBoard.Origin.X;
                PointF origin = this.DrawingBoard.Origin;
                int num1 = drawPolyLine.SegmentHitTest(point, x, (int)origin.Y);
                if (num1 == -3 || num1 == -2)
                {
                    Segment selectedSegment = this.ToolSettings.SelectedSegment;
                    int x1 = drawPolyLine.SelectedSegment.StartPoint.X;
                    Point startPoint = drawPolyLine.SelectedSegment.StartPoint;
                    selectedSegment.StartPoint = new Point(x1, startPoint.Y);
                    Segment segment = this.ToolSettings.SelectedSegment;
                    int x2 = drawPolyLine.SelectedSegment.EndPoint.X;
                    Point endPoint = drawPolyLine.SelectedSegment.EndPoint;
                    segment.EndPoint = new Point(x2, endPoint.Y);
                }
                num = num1;
            }
            catch
            {
                num = -1;
            }
            return num;
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
            this.InitializeTool(DrawingArea.DrawToolType.Angle, Utilities.LoadCursor("Angle.cur", new Cursor(this.GetType(), "Pencil.cur")));
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
            this.InitializeTool(DrawingArea.DrawToolType.Rectangle, Utilities.LoadCursor("Marker.cur", new Cursor(this.GetType(), "Rectangle.cur")));
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
            this.InitializeTool(DrawingArea.DrawToolType.Note, Utilities.LoadCursor("Note.cur", new Cursor(this.GetType(), "Rectangle.cur")));
        }

        public void SelectObjectType(string objectType)
        {
            try
            {
                this.ActiveDrawingObjects.UnselectAll();
                foreach (DrawObject collection in this.ActiveDrawingObjects.Collection)
                {
                    if (!(collection.ObjectType == objectType) || collection.IsDeduction())
                    {
                        continue;
                    }
                    collection.Selected = true;
                }
                this.Refresh();
            }
            catch
            {
            }
        }

        public void SelectPanTool()
        {
            this.InitializeTool(DrawingArea.DrawToolType.Pan, Utilities.LoadCursor("Pan.cur", Cursors.Hand));
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

        public void SelectPointerTool()
        {
            this.InitializeTool(DrawingArea.DrawToolType.Pointer, Cursors.Default);
            ((ToolPointer)this.tools[0]).ZoomSelection = false;
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

        public void SelectScaleTool()
        {
            this.InitializeTool(DrawingArea.DrawToolType.Scale, Utilities.LoadCursor("Distance.cur", new Cursor(this.GetType(), "Line.cur")));
        }

        public void SelectThisGroup(int groupID)
        {
            try
            {
                this.ActiveDrawingObjects.UnselectAll();
                foreach (DrawObject collection in this.ActiveDrawingObjects.Collection)
                {
                    if (collection.GroupID != groupID || collection.IsDeduction())
                    {
                        continue;
                    }
                    collection.Selected = true;
                }
                this.Refresh();
            }
            catch
            {
            }
        }

        public void SelectZoomSelectionTool()
        {
            this.InitializeTool(DrawingArea.DrawToolType.Pointer, Utilities.LoadCursor("Zoom.cur", Cursors.Default));
            ((ToolPointer)this.tools[0]).ZoomSelection = true;
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

        public void SetObjectPropertiesFromGroup(DrawObject drawObject)
        {
            if (drawObject.IsPartOfGroup())
            {
                if (drawObject.IsDeduction())
                {
                    Console.WriteLine("Deduction");
                }
                else
                {
                    DrawObject drawObject1 = this.FindObjectFromGroupID(this.project, drawObject.GroupID);
                    if (drawObject1 != null)
                    {
                        drawObject.Name = drawObject1.Name;
                        drawObject.Color = drawObject1.Color;
                        drawObject.FillColor = drawObject1.FillColor;
                        drawObject.Comment = drawObject1.Comment;
                        drawObject.Opacity = drawObject1.Opacity;
                        drawObject.PenWidth = drawObject1.PenWidth;
                        drawObject.ShowMeasure = drawObject1.ShowMeasure;
                        drawObject.Visible = drawObject1.Visible;
                        drawObject.SetSlopeFactor(drawObject1.SlopeFactor);
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
                    if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
                    {
                        foreach (DrawObject deductionArray in ((DrawPolyLine)drawObject).DeductionArray)
                        {
                            deductionArray.GroupID = drawObject.GroupID;
                            deductionArray.Opacity = drawObject.Opacity;
                            deductionArray.DeductionParentID = drawObject.ID;
                            deductionArray.SetSlopeFactor(drawObject.SlopeFactor);
                        }
                    }
                }
            }
        }

        public string ToAngleString(double value, DrawAngle.AngleTypeEnum angleType)
        {
            string angleString;
            try
            {
                angleString = this.ActivePlan.UnitScale.ToAngleString(value, angleType);
            }
            catch
            {
                angleString = "";
            }
            return angleString;
        }

        public string ToAreaString(int value)
        {
            string areaString;
            try
            {
                areaString = this.ActivePlan.UnitScale.ToAreaString(value);
            }
            catch
            {
                areaString = "";
            }
            return areaString;
        }

        public string ToAreaStringFromUnitSystem(double value)
        {
            string areaStringFromUnitSystem;
            try
            {
                areaStringFromUnitSystem = this.ActivePlan.UnitScale.ToAreaStringFromUnitSystem(value, true);
            }
            catch
            {
                areaStringFromUnitSystem = "";
            }
            return areaStringFromUnitSystem;
        }

        public string ToCurrency(double value)
        {
            return string.Format("{0:C}", value);
        }

        public void ToggleMeasures()
        {
            bool flag = true;
            bool flag1 = false;
            if (this.ActivePlan == null)
            {
                return;
            }
            foreach (Layer collection in this.ActivePlan.Layers.Collection)
            {
                foreach (DrawObject drawObject in collection.DrawingObjects.Collection)
                {
                    if (!drawObject.IsPartOfGroup() || !drawObject.ShowMeasure)
                    {
                        continue;
                    }
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                foreach (Layer layer in this.ActivePlan.Layers.Collection)
                {
                    foreach (DrawObject collection1 in layer.DrawingObjects.Collection)
                    {
                        if (!collection1.IsPartOfGroup())
                        {
                            continue;
                        }
                        flag1 = (flag1 ? true : !collection1.MeasureWasInvisible);
                    }
                }
            }
            else
            {
                foreach (Layer layer1 in this.ActivePlan.Layers.Collection)
                {
                    foreach (DrawObject showMeasure in layer1.DrawingObjects.Collection)
                    {
                        if (!showMeasure.IsPartOfGroup())
                        {
                            continue;
                        }
                        showMeasure.MeasureWasInvisible = !showMeasure.ShowMeasure;
                    }
                }
            }
            foreach (Layer collection2 in this.ActivePlan.Layers.Collection)
            {
                foreach (DrawObject drawObject1 in collection2.DrawingObjects.Collection)
                {
                    if (!drawObject1.IsPartOfGroup())
                    {
                        continue;
                    }
                    if (flag)
                    {
                        drawObject1.ShowMeasure = (!flag1 ? true : !drawObject1.MeasureWasInvisible);
                    }
                    else
                    {
                        drawObject1.ShowMeasure = false;
                    }
                    if (drawObject1.GroupID != this.toolSettings.GroupID)
                    {
                        continue;
                    }
                    this.toolSettings.ShowMeasure = drawObject1.ShowMeasure;
                }
            }
        }

        public string ToLengthString(int value, bool shortFormat = false)
        {
            string lengthString;
            try
            {
                lengthString = this.ActivePlan.UnitScale.ToLengthString(value, shortFormat);
            }
            catch
            {
                lengthString = "";
            }
            return lengthString;
        }

        public string ToLengthStringFromUnitSystem(double value, bool shortFormat = false)
        {
            string lengthStringFromUnitSystem;
            try
            {
                lengthStringFromUnitSystem = this.ActivePlan.UnitScale.ToLengthStringFromUnitSystem(value, shortFormat, true, true);
            }
            catch
            {
                lengthStringFromUnitSystem = "";
            }
            return lengthStringFromUnitSystem;
        }

        public string ToUnitString(int value)
        {
            return string.Concat(value.ToString(), " ", (value > 1 ? Resources.Unités : Resources.Unité));
        }

        public void TrackMouse(Point location)
        {
            this.tools[(int)this.activeTool].TrackMouse(location);
        }

        private int TranslateColorIndex(string objectType)
        {
            string str = objectType;
            string str1 = str;
            if (str != null)
            {
                if (str1 == "Line")
                {
                    return (int)this.PerimeterColors.GetValue(this.ColorIndex);
                }
                if (str1 == "Area")
                {
                    return (int)this.AreaColors.GetValue(this.ColorIndex);
                }
                if (str1 == "Perimeter")
                {
                    return (int)this.PerimeterColors.GetValue(this.ColorIndex);
                }
                if (str1 == "Counter")
                {
                    return (int)this.CounterColors.GetValue(this.ColorIndex);
                }
            }
            return this.ColorIndex;
        }

        public void Undo()
        {
            if (this.ActivePlan != null)
            {
                this.ActivePlan.Undo();
                this.Refresh();
            }
        }

        public bool UnitScaleIsImperial()
        {
            bool currentSystemType;
            try
            {
                currentSystemType = this.ActivePlan.UnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial;
            }
            catch
            {
                currentSystemType = false;
            }
            return currentSystemType;
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

        public void UnselectLegend()
        {
            if (this.ActivePlan == null)
            {
                return;
            }
            foreach (Layer collection in this.ActivePlan.Layers.Collection)
            {
                foreach (DrawObject drawObject in collection.DrawingObjects.Collection)
                {
                    if (drawObject.ObjectType != "Legend")
                    {
                        continue;
                    }
                    ((DrawLegend)drawObject).Selected = false;
                    return;
                }
            }
        }

        public void UpdateCursor(DrawObject drawObject)
        {
            if (this.ActiveTool == DrawingArea.DrawToolType.Counter || this.ActiveTool == DrawingArea.DrawToolType.Line || this.ActiveTool == DrawingArea.DrawToolType.PolyLine)
            {
                string str = "Pencil.cur";
                string objectType = drawObject.ObjectType;
                string str1 = objectType;
                if (objectType != null)
                {
                    if (str1 == "Line")
                    {
                        str = "Line.cur";
                    }
                    else if (str1 == "Counter")
                    {
                        str = "Ellipse.cur";
                    }
                }
                this.tools[(int)this.ActiveTool].LoadCursor(this.LoadCursor(drawObject, 5, 5, str));
            }
        }

        public void UpdateLegend()
        {
            if (this.ActivePlan == null)
            {
                return;
            }
            foreach (Layer collection in this.ActivePlan.Layers.Collection)
            {
                foreach (DrawObject drawObject in collection.DrawingObjects.Collection)
                {
                    if (drawObject.ObjectType != "Legend")
                    {
                        continue;
                    }
                    ((DrawLegend)drawObject).RecalcLayout();
                    return;
                }
            }
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

        public void UpdateStatusBar(string status)
        {
            this.Owner.UpdateStatusBar(status);
        }

        public bool ValidateDrawObjectName(ref string name, ref string title, ref string errorMessage)
        {
            bool flag;
            if (!this.ValidateName(ref name, ref title, ref errorMessage, 50))
            {
                return false;
            }
            IEnumerator enumerator = this.project.Plans.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    IEnumerator enumerator1 = ((Plan)enumerator.Current).Layers.Collection.GetEnumerator();
                    try
                    {
                        while (enumerator1.MoveNext())
                        {
                            IEnumerator enumerator2 = ((Layer)enumerator1.Current).DrawingObjects.Collection.GetEnumerator();
                            try
                            {
                                while (enumerator2.MoveNext())
                                {
                                    if (((DrawObject)enumerator2.Current).Name != name)
                                    {
                                        continue;
                                    }
                                    title = Resources.Nom_déjà_utilisé;
                                    errorMessage = Resources.Ce_nom_est_déjà_utilisé_par_un_autre_objet;
                                    flag = false;
                                    return flag;
                                }
                            }
                            finally
                            {
                                IDisposable disposable = enumerator2 as IDisposable;
                                if (disposable != null)
                                {
                                    disposable.Dispose();
                                }
                            }
                        }
                    }
                    finally
                    {
                        IDisposable disposable1 = enumerator1 as IDisposable;
                        if (disposable1 != null)
                        {
                            disposable1.Dispose();
                        }
                    }
                }
                return true;
            }
            finally
            {
                IDisposable disposable2 = enumerator as IDisposable;
                if (disposable2 != null)
                {
                    disposable2.Dispose();
                }
            }
            return flag;
        }

        public bool ValidateLayerName(ref string name, ref string title, ref string errorMessage)
        {
            if (!this.ValidateName(ref name, ref title, ref errorMessage, 50))
            {
                return false;
            }
            if (!this.ActivePlan.Layers.NameExists(name))
            {
                return true;
            }
            title = Resources.Nom_déjà_utilisé;
            errorMessage = Resources.Ce_nom_est_déjà_utilisé_par_un_autre_calque;
            return false;
        }

        public void ValidateLegend(ExtensionsSupport extensionsSupport)
        {
            if (this.ActivePlan == null)
            {
                return;
            }
            try
            {
                foreach (Layer collection in this.ActivePlan.Layers.Collection)
                {
                    foreach (DrawObject drawObject in collection.DrawingObjects.Collection)
                    {
                        if (drawObject.ObjectType != "Legend")
                        {
                            continue;
                        }
                        return;
                    }
                }
                DrawLegend drawLegend = new DrawLegend(this.ActivePlan, extensionsSupport, 0, 0, Resources.Légende, Color.WhiteSmoke, 6, DrawLegend.DefaultFontSize, DrawLegend.DefaultMaxRows)
                {
                    MustSetLocation = true,
                    ShowMeasure = true,
                    Visible = true
                };
                this.InsertObject(drawLegend, 0, false, false);
            }
            catch
            {
            }
        }

        public void ValidateLegends(ExtensionsSupport extensionsSupport)
        {
            Plan activePlan = this.ActivePlan;
            foreach (Plan collection in this.project.Plans.Collection)
            {
                this.ActivePlan = collection;
                if (collection.Layers.Count == 0)
                {
                    collection.CreateDefaultLayers();
                }
                this.ValidateLegend(extensionsSupport);
            }
            this.ActivePlan = activePlan;
        }

        public bool ValidateName(ref string name, ref string title, ref string errorMessage, int maxLength)
        {
            name = Utilities.Substring(name, 0, maxLength);
            if (Utilities.ValidateVariableName(name, ""))
            {
                return true;
            }
            if (name != "")
            {
                title = Resources.Nom_invalide;
                errorMessage = string.Concat(Resources.Les_caractères_suivants_sont_invalides, "\n", Utilities.InvalidCharacters());
            }
            return false;
        }

        public bool ValidatePlanName(ref string name, ref string title, ref string errorMessage)
        {
            if (!this.ValidateName(ref name, ref title, ref errorMessage, 50))
            {
                return false;
            }
            if (this.project.Plans.FindPlan(name) == null)
            {
                return true;
            }
            title = Resources.Nom_déjà_utilisé;
            errorMessage = Resources.Ce_nom_est_déjà_utilisé_par_un_autre_plan;
            return false;
        }

        public event OnDeductionsParentReleaseHandler OnDeductionsParentRelease;

        public event OnDeductionsParentSetHandler OnDeductionsParentSet;

        public event OnObjectsSelectedHandler OnObjectsSelected;

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