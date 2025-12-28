using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using QuoterPlan.Properties;
using QuoterPlanControls;

namespace QuoterPlan
{
	[Serializable]
	public abstract class DrawObject : IComparable
	{
		public virtual Rectangle BoundingRectangle
		{
			get
			{
				return default(Rectangle);
			}
		}

		public virtual Point Center
		{
			get
			{
				return new Point(0, 0);
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

		public ArrayList TextArray
		{
			get
			{
				return this.textArray;
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

		public int Rotation
		{
			get
			{
				return this._rotation;
			}
			set
			{
				if (value > 360)
				{
					this._rotation = value - 360;
					return;
				}
				if (value < -360)
				{
					this._rotation = value + 360;
					return;
				}
				this._rotation = value;
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

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
				this.displayName.Caption = this.ObjectSortOrder + " - " + this.name;
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

		public string ObjectType
		{
			get
			{
				string text = base.GetType().Name;
				if (text == "DrawLine")
				{
					text = "Line";
				}
				if (text == "DrawRectangle")
				{
					text = "Rectangle";
				}
				if (text == "DrawPolyLine")
				{
					text = (this.Filled ? "Area" : "Perimeter");
				}
				if (text == "DrawCounter")
				{
					text = "Counter";
				}
				if (text == "DrawAngle")
				{
					text = "Angle";
				}
				if (text == "DrawNote")
				{
					text = "Note";
				}
				if (text == "DrawLegend")
				{
					text = "Legend";
				}
				return text;
			}
		}

		public int ObjectSortOrder
		{
			get
			{
				int result = 0;
				string a = base.GetType().Name;
				if (a == "DrawPolyLine")
				{
					result = (this.Filled ? 1 : 2);
				}
				if (a == "DrawLine")
				{
					result = 3;
				}
				if (a == "DrawCounter")
				{
					result = 4;
				}
				if (a == "DrawAngle")
				{
					result = 5;
				}
				if (a == "DrawRectangle")
				{
					result = 6;
				}
				if (a == "DrawNote")
				{
					result = 7;
				}
				if (a == "DrawLegend")
				{
					result = 8;
				}
				return result;
			}
		}

		public string ObjectTypeDisplayName
		{
			get
			{
				string text = base.GetType().Name;
				if (text == "DrawLine")
				{
					text = Resources.Distance;
				}
				if (text == "DrawRectangle")
				{
					text = Resources.Marqueur;
				}
				if (text == "DrawPolyLine")
				{
					text = (this.Filled ? Resources.Surface : Resources.Périmètre);
				}
				if (text == "DrawCounter")
				{
					text = Resources.Compteur;
				}
				if (text == "DrawAngle")
				{
					text = Resources.Angle;
				}
				if (text == "DrawNote")
				{
					text = Resources.Note;
				}
				if (text == "DrawLegend")
				{
					text = Resources.Légende;
				}
				return text;
			}
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

		public int Opacity
		{
			get
			{
				if (this.opacity + 30 <= 255)
				{
					return this.opacity;
				}
				return 255;
			}
			set
			{
				this.opacity = value;
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

		public virtual int ActiveHandleCount
		{
			get
			{
				return 0;
			}
		}

		public virtual int HandleCount
		{
			get
			{
				return 0;
			}
		}

		public virtual int ConnectionCount
		{
			get
			{
				return 0;
			}
		}

		public virtual SlopeFactor SlopeFactor
		{
			get
			{
				return null;
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

		public DrawingBoard DrawingBoard
		{
			get
			{
				return this.drawArea.DrawingBoard;
			}
		}

		public DrawObjectGroup Group
		{
			get
			{
				if (this.group == null)
				{
					this.group = this.drawArea.GetObjectGroup(this);
				}
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		public bool SuspendContentDrawing
		{
			[CompilerGenerated]
			get
			{
				return this.<SuspendContentDrawing>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SuspendContentDrawing>k__BackingField = value;
			}
		}

		protected DrawObject()
		{
			this.ID = this.GetHashCode();
			this.displayName = new Utilities.DisplayName(this, string.Empty);
			this.textArray = new ArrayList();
		}

		public abstract DrawObject Clone();

		public virtual Region Region(int offsetX, int offsetY, float zoomFactor)
		{
			return new Region();
		}

		public virtual bool AutoAdjust(Bitmap image, DrawPolyLine.AutoAdjustType autoAdjustType)
		{
			return false;
		}

		public bool IsPartOfGroup()
		{
			return this.GroupID != -1;
		}

		public bool IsDeduction()
		{
			return this.DeductionParentID != -1;
		}

		public bool IsDisplayed()
		{
			bool result;
			try
			{
				result = this.IntersectsWith(new Rectangle(0, 0, (int)((float)this.drawArea.ClientSize.Width / this.drawArea.ZoomFactor), (int)((float)this.drawArea.ClientSize.Height / this.drawArea.ZoomFactor)), (int)this.drawArea.DrawingBoard.Origin.X, (int)this.drawArea.DrawingBoard.Origin.Y);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public bool HasSameGroupOrID(DrawObject objectToCompare)
		{
			return objectToCompare != null && (this.ID == objectToCompare.ID || (this.GroupID == objectToCompare.GroupID && this.GroupID != -1));
		}

		public void ClearTextArray()
		{
			this.TextDirty = true;
			this.TextArray.Clear();
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

		public virtual void Draw(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh)
		{
		}

		public virtual void DrawText(Graphics g, int offsetX, int offsetY, bool printToScreen = true, MainForm.ImageQualityEnum imageQuality = MainForm.ImageQualityEnum.QualityHigh, float defaultFontSize = 12f)
		{
		}

		public virtual Point GetHandle(int handleNumber)
		{
			return new Point(0, 0);
		}

		public virtual int GetHandleSize()
		{
			return 4;
		}

		public virtual bool IsHandleResizable(int handleNumber)
		{
			return true;
		}

		public virtual Rectangle GetHandleRectangle(int handleNumber, int offsetX, int offsetY)
		{
			Point handle = this.GetHandle(handleNumber);
			int handleSize = this.GetHandleSize();
			return new Rectangle(handle.X - offsetX - handleSize, handle.Y - offsetY - handleSize, handleSize * 2, handleSize * 2);
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
				if (this.IsHandleResizable(i))
				{
					g.FillRectangle(new SolidBrush(Color.FromArgb(this.Opacity + 30, Color.Black)), handleRectangle);
				}
				else
				{
					g.FillRectangle(new SolidBrush(Color.FromArgb(this.Opacity + 30, Color.White)), handleRectangle);
					g.DrawRectangle(new Pen(Color.Black, 1f), handleRectangle);
				}
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

		public virtual int HitTest(Point point, int offsetX, int offsetY)
		{
			return -1;
		}

		protected virtual bool PointInObject(Point point, int offsetX, int offsetY)
		{
			return false;
		}

		public virtual Cursor GetHandleCursor(int handleNumber)
		{
			return Cursors.Default;
		}

		public virtual bool IntersectsWith(Rectangle rectangle, int offsetX, int offsetY)
		{
			return false;
		}

		public virtual void Move(int deltaX, int deltaY)
		{
		}

		public virtual void MoveHandleTo(Point point, int handleNumber)
		{
		}

		public virtual void Dump()
		{
			Trace.WriteLine("");
			Trace.WriteLine(base.GetType().Name);
			Trace.WriteLine("Selected = " + this.selected.ToString(CultureInfo.InvariantCulture));
		}

		public virtual void Normalize()
		{
		}

		protected void Initialize()
		{
		}

		public override string ToString()
		{
			return this.name;
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
				DrawText value = (DrawText)this.TextArray[i];
				drawObject.TextArray.Add(value);
			}
		}

		public int CompareTo(object obj)
		{
			DrawObject drawObject = obj as DrawObject;
			int result = 0;
			if (drawObject != null)
			{
				if (drawObject.ZOrder == this.ZOrder)
				{
					result = 0;
				}
				else if (drawObject.ZOrder > this.ZOrder)
				{
					result = -1;
				}
				else
				{
					result = 1;
				}
			}
			return result;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DrawObject()
		{
		}

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

		private int opacity = 255;

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

		private DrawObjectGroup group;

		private static Color lastUsedColor = Color.Black;

		private static int lastUsedPenWidth = 1;

		[CompilerGenerated]
		private bool <SuspendContentDrawing>k__BackingField;
	}
}
