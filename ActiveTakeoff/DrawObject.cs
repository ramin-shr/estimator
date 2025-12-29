using QuoterPlan.Properties;
using QuoterPlanControls;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    [Serializable]
    public abstract class DrawObject : IComparable
    {
        private const string entryColor = "Color";

        private const string entryPenWidth = "PenWidth";

        private const string entryPen = "DrawPen";

        private const string entryBrush = "DrawBrush";

        private const string entryFillColor = "FillColor";

        private const string entryFilled = "Filled";

        private const string entryZOrder = "ZOrder";

        private const string entryRotation = "Rotation";

        private const string entryTipText = "TipText";

        private string name = string.Empty;

        private string label = string.Empty;

        private string text = string.Empty;

        private string comment = string.Empty;

        protected bool selected;

        private Color color;

        private Color fillColor;

        private int opacity = 0xff;

        private bool filled;

        private int penWidth;

        private Pen drawpen;

        private Brush drawBrush;

        private DrawingPens.PenType _penType;

        private FillBrushes.BrushType _brushType;

        private PointF _offset;

        private DrawingArea drawArea;

        private Utilities.DisplayName displayName;

        private int deductionParentID = -1;

        private string tipText;

        private bool dirty;

        private bool visible = true;

        private bool showMeasure = true;

        private bool measureWasInvisible = true;

        private int _id;

        private int _altID;

        private int _groupID = -1;

        private int _zOrder;

        private int _rotation;

        private bool resizing;

        private bool moving;

        private bool textDirty;

        private ArrayList textArray;

        private bool displayInPixels;

        private DrawObjectGroup @group;

        private static Color lastUsedColor;

        private static int lastUsedPenWidth;

        public virtual int ActiveHandleCount
        {
            get
            {
                return 0;
            }
        }

        public virtual Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle();
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

        public virtual Point Center
        {
            get
            {
                return new Point(0, 0);
            }
        }

        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        public virtual int ConnectionCount
        {
            get
            {
                return 0;
            }
        }

        public int DeductionParentID
        {
            get
            {
                return this.deductionParentID;
            }
            set
            {
                this.deductionParentID = value;
            }
        }

        public bool Dirty
        {
            get
            {
                return this.dirty;
            }
            set
            {
                this.dirty = value;
            }
        }

        public bool DisplayInPixels
        {
            get
            {
                return this.displayInPixels;
            }
            set
            {
                this.displayInPixels = value;
            }
        }

        public Utilities.DisplayName DisplayName
        {
            get
            {
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }

        public DrawingArea DrawArea
        {
            get
            {
                return this.drawArea;
            }
            set
            {
                this.drawArea = value;
            }
        }

        public Brush DrawBrush
        {
            get
            {
                return this.drawBrush;
            }
            set
            {
                this.drawBrush = value;
            }
        }

        public DrawingBoard DrawingBoard
        {
            get
            {
                return this.drawArea.DrawingBoard;
            }
        }

        public Pen DrawPen
        {
            get
            {
                return this.drawpen;
            }
            set
            {
                this.drawpen = value;
            }
        }

        public Color FillColor
        {
            get
            {
                return this.fillColor;
            }
            set
            {
                this.fillColor = value;
            }
        }

        public bool Filled
        {
            get
            {
                return this.filled;
            }
            set
            {
                this.filled = value;
            }
        }

        public DrawObjectGroup Group
        {
            get
            {
                if (this.@group == null)
                {
                    this.@group = this.drawArea.GetObjectGroup(this);
                }
                return this.@group;
            }
            set
            {
                this.@group = value;
            }
        }

        public int GroupID
        {
            get
            {
                return this._groupID;
            }
            set
            {
                this._groupID = value;
            }
        }

        public virtual int HandleCount
        {
            get
            {
                return 0;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label = value;
            }
        }

        public static Color LastUsedColor
        {
            get
            {
                return DrawObject.lastUsedColor;
            }
            set
            {
                DrawObject.lastUsedColor = value;
            }
        }

        public static int LastUsedPenWidth
        {
            get
            {
                return DrawObject.lastUsedPenWidth;
            }
            set
            {
                DrawObject.lastUsedPenWidth = value;
            }
        }

        public bool MeasureWasInvisible
        {
            get
            {
                return this.measureWasInvisible;
            }
            set
            {
                this.measureWasInvisible = value;
            }
        }

        public bool Moving
        {
            get
            {
                return this.moving;
            }
            set
            {
                this.moving = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.displayName.Caption = string.Concat(this.ObjectSortOrder, " - ", this.name);
            }
        }

        public int ObjectSortOrder
        {
            get
            {
                int num = 0;
                string name = this.GetType().Name;
                if (name == "DrawPolyLine")
                {
                    num = (this.Filled ? 1 : 2);
                }
                if (name == "DrawLine")
                {
                    num = 3;
                }
                if (name == "DrawCounter")
                {
                    num = 4;
                }
                if (name == "DrawAngle")
                {
                    num = 5;
                }
                if (name == "DrawRectangle")
                {
                    num = 6;
                }
                if (name == "DrawNote")
                {
                    num = 7;
                }
                if (name == "DrawLegend")
                {
                    num = 8;
                }
                return num;
            }
        }

        public string ObjectType
        {
            get
            {
                string name = this.GetType().Name;
                if (name == "DrawLine")
                {
                    name = "Line";
                }
                if (name == "DrawRectangle")
                {
                    name = "Rectangle";
                }
                if (name == "DrawPolyLine")
                {
                    name = (this.Filled ? "Area" : "Perimeter");
                }
                if (name == "DrawCounter")
                {
                    name = "Counter";
                }
                if (name == "DrawAngle")
                {
                    name = "Angle";
                }
                if (name == "DrawNote")
                {
                    name = "Note";
                }
                if (name == "DrawLegend")
                {
                    name = "Legend";
                }
                return name;
            }
        }

        public string ObjectTypeDisplayName
        {
            get
            {
                string name = this.GetType().Name;
                if (name == "DrawLine")
                {
                    name = Resources.Distance;
                }
                if (name == "DrawRectangle")
                {
                    name = Resources.Marqueur;
                }
                if (name == "DrawPolyLine")
                {
                    name = (this.Filled ? Resources.Surface : Resources.Périmètre);
                }
                if (name == "DrawCounter")
                {
                    name = Resources.Compteur;
                }
                if (name == "DrawAngle")
                {
                    name = Resources.Angle;
                }
                if (name == "DrawNote")
                {
                    name = Resources.Note;
                }
                if (name == "DrawLegend")
                {
                    name = Resources.Légende;
                }
                return name;
            }
        }

        public PointF Offset
        {
            get
            {
                return this._offset;
            }
            set
            {
                this._offset = value;
            }
        }

        public int Opacity
        {
            get
            {
                if (this.opacity + 30 > 0xff)
                {
                    return 0xff;
                }
                return this.opacity;
            }
            set
            {
                this.opacity = value;
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

        public int PenWidth
        {
            get
            {
                return this.penWidth;
            }
            set
            {
                this.penWidth = value;
            }
        }

        public bool Resizing
        {
            get
            {
                return this.resizing;
            }
            set
            {
                this.resizing = value;
            }
        }

        public int Rotation
        {
            get
            {
                return this._rotation;
            }
            set
            {
                if (value > 0x168)
                {
                    this._rotation = value - 0x168;
                    return;
                }
                if (value >= -360)
                {
                    this._rotation = value;
                    return;
                }
                this._rotation = value + 0x168;
            }
        }

        public virtual bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        public bool ShowMeasure
        {
            get
            {
                return this.showMeasure;
            }
            set
            {
                this.showMeasure = value;
            }
        }

        public virtual SlopeFactor SlopeFactor
        {
            get
            {
                return null;
            }
        }

        public bool SuspendContentDrawing
        {
            get;
            set;
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public ArrayList TextArray
        {
            get
            {
                return this.textArray;
            }
        }

        public bool TextDirty
        {
            get
            {
                return this.textDirty;
            }
            set
            {
                this.textDirty = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
            }
        }

        public int ZOrder
        {
            get
            {
                return this._zOrder;
            }
            set
            {
                this._zOrder = value;
            }
        }

        static DrawObject()
        {
            DrawObject.lastUsedColor = Color.Black;
            DrawObject.lastUsedPenWidth = 1;
        }

        protected DrawObject()
        {
            this.ID = this.GetHashCode();
            this.displayName = new Utilities.DisplayName(this, string.Empty);
            this.textArray = new ArrayList();
        }

        public virtual bool AutoAdjust(Bitmap image, DrawPolyLine.AutoAdjustType autoAdjustType)
        {
            return false;
        }

        public void ClearTextArray()
        {
            this.TextDirty = true;
            this.TextArray.Clear();
        }

        public abstract DrawObject Clone();

        public int CompareTo(object obj)
        {
            DrawObject drawObject = obj as DrawObject;
            int num = 0;
            if (drawObject != null)
            {
                if (drawObject.ZOrder != this.ZOrder)
                {
                    num = (drawObject.ZOrder <= this.ZOrder ? 1 : -1);
                }
                else
                {
                    num = 0;
                }
            }
            return num;
        }

        public virtual void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = 1)
        {
        }

        public virtual void DrawConnection(Graphics g, int connectionNumber)
        {
            SolidBrush solidBrush = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Red, -1f);
            g.DrawEllipse(pen, this.GetConnectionEllipse(connectionNumber));
            g.FillEllipse(solidBrush, this.GetConnectionEllipse(connectionNumber));
            pen.Dispose();
            solidBrush.Dispose();
        }

        public virtual void DrawConnections(Graphics g)
        {
            if (!this.Selected)
            {
                return;
            }
            SolidBrush solidBrush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.Black, -1f);
            for (int i = 0; i < this.ConnectionCount; i++)
            {
                g.DrawEllipse(pen, this.GetConnectionEllipse(i));
                g.FillEllipse(solidBrush, this.GetConnectionEllipse(i));
            }
            pen.Dispose();
            solidBrush.Dispose();
        }

        public virtual void DrawText(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = 1, float defaultFontSize = 12f)
        {
        }

        public virtual void DrawTracker(Graphics g, int offsetX, int offsetY)
        {
            if (!this.Selected || this.Resizing || this.Moving)
            {
                return;
            }
            for (int i = 1; i <= this.ActiveHandleCount; i++)
            {
                Rectangle handleRectangle = this.GetHandleRectangle(i, offsetX, offsetY);
                if (!this.IsHandleResizable(i))
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(this.Opacity + 30, Color.White)), handleRectangle);
                    g.DrawRectangle(new Pen(Color.Black, 1f), handleRectangle);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(this.Opacity + 30, Color.Black)), handleRectangle);
                }
            }
        }

        public virtual void Dump()
        {
            Trace.WriteLine("");
            Trace.WriteLine(this.GetType().Name);
            Trace.WriteLine(string.Concat("Selected = ", this.selected.ToString(CultureInfo.InvariantCulture)));
        }

        protected void FillDrawObjectFields(DrawObject drawObject)
        {
            drawObject.ID = this.ID;
            drawObject.GroupID = this.GroupID;
            drawObject.Name = this.name;
            drawObject.Label = this.label;
            drawObject.Text = this.text;
            drawObject.Comment = this.Comment;
            drawObject.Selected = false;
            drawObject.Color = this.color;
            drawObject.FillColor = this.fillColor;
            drawObject.Filled = this.filled;
            drawObject.Visible = this.visible;
            drawObject.ShowMeasure = this.showMeasure;
            drawObject.Opacity = this.opacity;
            drawObject.PenWidth = this.penWidth;
            drawObject.DrawPen = this.drawpen;
            drawObject.DrawBrush = this.drawBrush;
            drawObject.PenType = this._penType;
            drawObject.BrushType = this._brushType;
            drawObject.ZOrder = this._zOrder;
            drawObject.Rotation = this._rotation;
            drawObject.Offset = this._offset;
            drawObject.DeductionParentID = this.DeductionParentID;
            drawObject.drawArea = this.drawArea;
            for (int i = 0; i < this.TextArray.Count; i++)
            {
                DrawText item = (DrawText)this.TextArray[i];
                drawObject.TextArray.Add(item);
            }
        }

        public virtual Point GetConnection(int connectionNumber)
        {
            return new Point(0, 0);
        }

        public virtual Rectangle GetConnectionEllipse(int connectionNumber)
        {
            Point connection = this.GetConnection(connectionNumber);
            return new Rectangle(connection.X - (this.penWidth + 3), connection.Y - (this.penWidth + 3), 7 + this.penWidth, 7 + this.penWidth);
        }

        public virtual Point GetHandle(int handleNumber)
        {
            return new Point(0, 0);
        }

        public virtual Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        public virtual Rectangle GetHandleRectangle(int handleNumber, int offsetX, int offsetY)
        {
            Point handle = this.GetHandle(handleNumber);
            int handleSize = this.GetHandleSize();
            return new Rectangle(handle.X - offsetX - handleSize, handle.Y - offsetY - handleSize, handleSize * 2, handleSize * 2);
        }

        public virtual int GetHandleSize()
        {
            return 4;
        }

        public bool HasSameGroupOrID(DrawObject objectToCompare)
        {
            if (objectToCompare == null)
            {
                return false;
            }
            if (this.ID == objectToCompare.ID)
            {
                return true;
            }
            if (this.GroupID != objectToCompare.GroupID)
            {
                return false;
            }
            return this.GroupID != -1;
        }

        public virtual int HitTest(Point point, int offsetX, int offsetY)
        {
            return -1;
        }

        protected void Initialize()
        {
        }

        public virtual bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
        {
            return false;
        }

        public bool IsDeduction()
        {
            return this.DeductionParentID != -1;
        }

        public bool IsDisplayed()
        {
            bool flag;
            try
            {
                Size clientSize = this.drawArea.ClientSize;
                int width = (int)((float)clientSize.Width / this.drawArea.ZoomFactor);
                Size size = this.drawArea.ClientSize;
                Rectangle rectangle = new Rectangle(0, 0, width, (int)((float)size.Height / this.drawArea.ZoomFactor));
                int x = (int)this.drawArea.DrawingBoard.Origin.X;
                PointF origin = this.drawArea.DrawingBoard.Origin;
                flag = this.IntersectsWith(rectangle, x, (int)origin.Y);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public virtual bool IsHandleResizable(int handleNumber)
        {
            return true;
        }

        public bool IsPartOfGroup()
        {
            return this.GroupID != -1;
        }

        public virtual void Move(int deltaX, int deltaY)
        {
        }

        public virtual void MoveHandleTo(Point point, int handleNumber)
        {
        }

        public virtual void Normalize()
        {
        }

        protected virtual bool PointInObject(Point point, int offsetX, int offsetY)
        {
            return false;
        }

        public virtual Region Region(int offsetX, int offsetY, float zoomFactor)
        {
            return new Region();
        }

        public virtual void Scale(float scaleX, float scaleY)
        {
        }

        public virtual void SetSlopeFactor(SlopeFactor slopeFactor)
        {
        }

        public virtual void SetSlopeFactor(double internalValue, SlopeFactor.SlopeTypeEnum slopeType, SlopeFactor.SlopeApplyTypeEnum slopeApplyType, SlopeFactor.HipValleyEnum hipValley)
        {
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}