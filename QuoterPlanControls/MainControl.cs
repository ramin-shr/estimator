using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlanControls
{
	public class MainControl : UserControl
	{
		public new event KeyEventHandler KeyDown
		{
			add
			{
				KeyEventHandler keyEventHandler = this.KeyDown;
				KeyEventHandler keyEventHandler2;
				do
				{
					keyEventHandler2 = keyEventHandler;
					KeyEventHandler value2 = (KeyEventHandler)Delegate.Combine(keyEventHandler2, value);
					keyEventHandler = Interlocked.CompareExchange<KeyEventHandler>(ref this.KeyDown, value2, keyEventHandler2);
				}
				while (keyEventHandler != keyEventHandler2);
			}
			remove
			{
				KeyEventHandler keyEventHandler = this.KeyDown;
				KeyEventHandler keyEventHandler2;
				do
				{
					keyEventHandler2 = keyEventHandler;
					KeyEventHandler value2 = (KeyEventHandler)Delegate.Remove(keyEventHandler2, value);
					keyEventHandler = Interlocked.CompareExchange<KeyEventHandler>(ref this.KeyDown, value2, keyEventHandler2);
				}
				while (keyEventHandler != keyEventHandler2);
			}
		}

		public new event MouseEventHandler MouseDown
		{
			add
			{
				MouseEventHandler mouseEventHandler = this.MouseDown;
				MouseEventHandler mouseEventHandler2;
				do
				{
					mouseEventHandler2 = mouseEventHandler;
					MouseEventHandler value2 = (MouseEventHandler)Delegate.Combine(mouseEventHandler2, value);
					mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.MouseDown, value2, mouseEventHandler2);
				}
				while (mouseEventHandler != mouseEventHandler2);
			}
			remove
			{
				MouseEventHandler mouseEventHandler = this.MouseDown;
				MouseEventHandler mouseEventHandler2;
				do
				{
					mouseEventHandler2 = mouseEventHandler;
					MouseEventHandler value2 = (MouseEventHandler)Delegate.Remove(mouseEventHandler2, value);
					mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.MouseDown, value2, mouseEventHandler2);
				}
				while (mouseEventHandler != mouseEventHandler2);
			}
		}

		public new event MouseEventHandler MouseMove
		{
			add
			{
				MouseEventHandler mouseEventHandler = this.MouseMove;
				MouseEventHandler mouseEventHandler2;
				do
				{
					mouseEventHandler2 = mouseEventHandler;
					MouseEventHandler value2 = (MouseEventHandler)Delegate.Combine(mouseEventHandler2, value);
					mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.MouseMove, value2, mouseEventHandler2);
				}
				while (mouseEventHandler != mouseEventHandler2);
			}
			remove
			{
				MouseEventHandler mouseEventHandler = this.MouseMove;
				MouseEventHandler mouseEventHandler2;
				do
				{
					mouseEventHandler2 = mouseEventHandler;
					MouseEventHandler value2 = (MouseEventHandler)Delegate.Remove(mouseEventHandler2, value);
					mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.MouseMove, value2, mouseEventHandler2);
				}
				while (mouseEventHandler != mouseEventHandler2);
			}
		}

		public new event MouseEventHandler MouseUp
		{
			add
			{
				MouseEventHandler mouseEventHandler = this.MouseUp;
				MouseEventHandler mouseEventHandler2;
				do
				{
					mouseEventHandler2 = mouseEventHandler;
					MouseEventHandler value2 = (MouseEventHandler)Delegate.Combine(mouseEventHandler2, value);
					mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.MouseUp, value2, mouseEventHandler2);
				}
				while (mouseEventHandler != mouseEventHandler2);
			}
			remove
			{
				MouseEventHandler mouseEventHandler = this.MouseUp;
				MouseEventHandler mouseEventHandler2;
				do
				{
					mouseEventHandler2 = mouseEventHandler;
					MouseEventHandler value2 = (MouseEventHandler)Delegate.Remove(mouseEventHandler2, value);
					mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.MouseUp, value2, mouseEventHandler2);
				}
				while (mouseEventHandler != mouseEventHandler2);
			}
		}

		public new event EventHandler MouseEnter
		{
			add
			{
				EventHandler eventHandler = this.MouseEnter;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.MouseEnter, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.MouseEnter;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.MouseEnter, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public new event EventHandler MouseLeave
		{
			add
			{
				EventHandler eventHandler = this.MouseLeave;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.MouseLeave, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.MouseLeave;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.MouseLeave, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public new event PaintEventHandler OnPaint
		{
			add
			{
				PaintEventHandler paintEventHandler = this.OnPaint;
				PaintEventHandler paintEventHandler2;
				do
				{
					paintEventHandler2 = paintEventHandler;
					PaintEventHandler value2 = (PaintEventHandler)Delegate.Combine(paintEventHandler2, value);
					paintEventHandler = Interlocked.CompareExchange<PaintEventHandler>(ref this.OnPaint, value2, paintEventHandler2);
				}
				while (paintEventHandler != paintEventHandler2);
			}
			remove
			{
				PaintEventHandler paintEventHandler = this.OnPaint;
				PaintEventHandler paintEventHandler2;
				do
				{
					paintEventHandler2 = paintEventHandler;
					PaintEventHandler value2 = (PaintEventHandler)Delegate.Remove(paintEventHandler2, value);
					paintEventHandler = Interlocked.CompareExchange<PaintEventHandler>(ref this.OnPaint, value2, paintEventHandler2);
				}
				while (paintEventHandler != paintEventHandler2);
			}
		}

		public event ZoomChangeEventHandler OnZoomChange
		{
			add
			{
				ZoomChangeEventHandler zoomChangeEventHandler = this.OnZoomChange;
				ZoomChangeEventHandler zoomChangeEventHandler2;
				do
				{
					zoomChangeEventHandler2 = zoomChangeEventHandler;
					ZoomChangeEventHandler value2 = (ZoomChangeEventHandler)Delegate.Combine(zoomChangeEventHandler2, value);
					zoomChangeEventHandler = Interlocked.CompareExchange<ZoomChangeEventHandler>(ref this.OnZoomChange, value2, zoomChangeEventHandler2);
				}
				while (zoomChangeEventHandler != zoomChangeEventHandler2);
			}
			remove
			{
				ZoomChangeEventHandler zoomChangeEventHandler = this.OnZoomChange;
				ZoomChangeEventHandler zoomChangeEventHandler2;
				do
				{
					zoomChangeEventHandler2 = zoomChangeEventHandler;
					ZoomChangeEventHandler value2 = (ZoomChangeEventHandler)Delegate.Remove(zoomChangeEventHandler2, value);
					zoomChangeEventHandler = Interlocked.CompareExchange<ZoomChangeEventHandler>(ref this.OnZoomChange, value2, zoomChangeEventHandler2);
				}
				while (zoomChangeEventHandler != zoomChangeEventHandler2);
			}
		}

		public event ZoomChangeEventHandler OnCacheLoaded
		{
			add
			{
				ZoomChangeEventHandler zoomChangeEventHandler = this.OnCacheLoaded;
				ZoomChangeEventHandler zoomChangeEventHandler2;
				do
				{
					zoomChangeEventHandler2 = zoomChangeEventHandler;
					ZoomChangeEventHandler value2 = (ZoomChangeEventHandler)Delegate.Combine(zoomChangeEventHandler2, value);
					zoomChangeEventHandler = Interlocked.CompareExchange<ZoomChangeEventHandler>(ref this.OnCacheLoaded, value2, zoomChangeEventHandler2);
				}
				while (zoomChangeEventHandler != zoomChangeEventHandler2);
			}
			remove
			{
				ZoomChangeEventHandler zoomChangeEventHandler = this.OnCacheLoaded;
				ZoomChangeEventHandler zoomChangeEventHandler2;
				do
				{
					zoomChangeEventHandler2 = zoomChangeEventHandler;
					ZoomChangeEventHandler value2 = (ZoomChangeEventHandler)Delegate.Remove(zoomChangeEventHandler2, value);
					zoomChangeEventHandler = Interlocked.CompareExchange<ZoomChangeEventHandler>(ref this.OnCacheLoaded, value2, zoomChangeEventHandler2);
				}
				while (zoomChangeEventHandler != zoomChangeEventHandler2);
			}
		}

		public new event ZoomChangeEventHandler OnMouseWheel
		{
			add
			{
				ZoomChangeEventHandler zoomChangeEventHandler = this.OnMouseWheel;
				ZoomChangeEventHandler zoomChangeEventHandler2;
				do
				{
					zoomChangeEventHandler2 = zoomChangeEventHandler;
					ZoomChangeEventHandler value2 = (ZoomChangeEventHandler)Delegate.Combine(zoomChangeEventHandler2, value);
					zoomChangeEventHandler = Interlocked.CompareExchange<ZoomChangeEventHandler>(ref this.OnMouseWheel, value2, zoomChangeEventHandler2);
				}
				while (zoomChangeEventHandler != zoomChangeEventHandler2);
			}
			remove
			{
				ZoomChangeEventHandler zoomChangeEventHandler = this.OnMouseWheel;
				ZoomChangeEventHandler zoomChangeEventHandler2;
				do
				{
					zoomChangeEventHandler2 = zoomChangeEventHandler;
					ZoomChangeEventHandler value2 = (ZoomChangeEventHandler)Delegate.Remove(zoomChangeEventHandler2, value);
					zoomChangeEventHandler = Interlocked.CompareExchange<ZoomChangeEventHandler>(ref this.OnMouseWheel, value2, zoomChangeEventHandler2);
				}
				while (zoomChangeEventHandler != zoomChangeEventHandler2);
			}
		}

		public event OriginChangeEventHandler OnOriginChange
		{
			add
			{
				OriginChangeEventHandler originChangeEventHandler = this.OnOriginChange;
				OriginChangeEventHandler originChangeEventHandler2;
				do
				{
					originChangeEventHandler2 = originChangeEventHandler;
					OriginChangeEventHandler value2 = (OriginChangeEventHandler)Delegate.Combine(originChangeEventHandler2, value);
					originChangeEventHandler = Interlocked.CompareExchange<OriginChangeEventHandler>(ref this.OnOriginChange, value2, originChangeEventHandler2);
				}
				while (originChangeEventHandler != originChangeEventHandler2);
			}
			remove
			{
				OriginChangeEventHandler originChangeEventHandler = this.OnOriginChange;
				OriginChangeEventHandler originChangeEventHandler2;
				do
				{
					originChangeEventHandler2 = originChangeEventHandler;
					OriginChangeEventHandler value2 = (OriginChangeEventHandler)Delegate.Remove(originChangeEventHandler2, value);
					originChangeEventHandler = Interlocked.CompareExchange<OriginChangeEventHandler>(ref this.OnOriginChange, value2, originChangeEventHandler2);
				}
				while (originChangeEventHandler != originChangeEventHandler2);
			}
		}

		public MainControl()
		{
			this.InitializeComponent();
			base.BorderStyle = BorderStyle.None;
			this.drawingBoard1.SetScrollPositions += this.drawingBoard1_SetScrollPositions;
			this.drawingBoard1.OnZoomChange += this.drawingBoard1_OnZoomChange;
			this.drawingBoard1.OnCacheLoaded += this.drawingBoard1_OnCacheLoaded;
			this.drawingBoard1.OnMouseWheel += this.drawingBoard1_OnMouseWheel;
			this.drawingBoard1.OnOriginChange += this.drawingBoard1_OnOriginChange;
			this.vScrollBar.ValueChanged += this.ScrollBar_ValueChanged;
			this.hScrollBar.ValueChanged += this.ScrollBar_ValueChanged;
		}

		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		public DrawingBoard DrawingBoard
		{
			get
			{
				return this.drawingBoard1;
			}
		}

		public int Zoom
		{
			get
			{
				return this.drawingBoard1.Zoom;
			}
			set
			{
				if (!this.ZoomRestricted)
				{
					this.drawingBoard1.Zoom = value;
				}
			}
		}

		public void ZoomLock(int zoom)
		{
			this.drawingBoard1.ZoomLock(zoom);
		}

		public int FitToScreenZoom
		{
			get
			{
				return this.drawingBoard1.FitToScreenZoom;
			}
		}

		public double ZoomFactor
		{
			get
			{
				return this.drawingBoard1.ZoomFactor;
			}
		}

		public bool ZoomRestricted
		{
			get
			{
				return this.drawingBoard1.ZoomRestricted;
			}
			set
			{
				this.drawingBoard1.ZoomRestricted = value;
			}
		}

		public PointF Origin
		{
			get
			{
				return this.drawingBoard1.Origin;
			}
			set
			{
				this.drawingBoard1.Origin = value;
			}
		}

		public void ApplyBrightnessAndContrast(int brightness, int contrast)
		{
			this.drawingBoard1.ApplyBrightnessAndContrast(brightness, contrast);
		}

		public int Brightness
		{
			get
			{
				return this.drawingBoard1.Brightness;
			}
			set
			{
				this.drawingBoard1.Brightness = value;
			}
		}

		public int Contrast
		{
			get
			{
				return this.drawingBoard1.Contrast;
			}
			set
			{
				this.drawingBoard1.Contrast = value;
			}
		}

		public bool UseDynamicAdjustments
		{
			get
			{
				return this.drawingBoard1.UseDynamicAdjustments;
			}
			set
			{
				this.drawingBoard1.UseDynamicAdjustments = value;
			}
		}

		public bool CacheInUsed
		{
			get
			{
				return this.drawingBoard1.CacheInUsed;
			}
		}

		public void ReloadImage(bool reloadZoomedImages)
		{
			this.drawingBoard1.ReloadImage(reloadZoomedImages);
		}

		public bool ReloadImage(string fileName, bool reloadZoomedImages, ref Exception exception)
		{
			return this.drawingBoard1.ReloadImage(fileName, reloadZoomedImages, ref exception);
		}

		public void ReloadZoomedImages()
		{
			this.drawingBoard1.ReloadZoomedImages();
		}

		public void ReloadAdaptiveZoomedImage()
		{
			this.drawingBoard1.ReloadAdaptiveZoomedImage();
		}

		public void SetImageQuality(InterpolationModes.ZoomedImagesEnum index, InterpolationMode interpolationMode)
		{
			this.drawingBoard1.SetImageQuality(index, interpolationMode);
		}

		public InterpolationModes ImageQuality
		{
			get
			{
				return this.drawingBoard1.ImageQuality;
			}
		}

		public bool HScrollBarEnabled
		{
			get
			{
				return this.hScrollBar.Enabled;
			}
		}

		public bool VScrollBarEnabled
		{
			get
			{
				return this.vScrollBar.Enabled;
			}
		}

		public bool ScrollbarsVisible
		{
			get
			{
				return this.scrollVisible;
			}
			set
			{
				this.scrollVisible = value;
				this.hScrollBar.Visible = value;
				this.vScrollBar.Visible = value;
				if (!value)
				{
					this.drawingBoard1.Dock = DockStyle.Fill;
					return;
				}
				this.drawingBoard1.Dock = DockStyle.None;
				this.vScrollBar.Location = new Point(base.ClientSize.Width - this.vScrollBar.Width, 0);
				this.vScrollBar.Height = base.ClientSize.Height - this.hScrollBar.Height;
				this.hScrollBar.Location = new Point(-1, base.ClientSize.Height - this.hScrollBar.Height);
				this.hScrollBar.Width = base.ClientSize.Width - this.vScrollBar.Width;
			}
		}

		public void Clear()
		{
			this.drawingBoard1.Clear();
			this.hScrollBar.Value = 0;
			this.hScrollBar.Enabled = false;
			this.vScrollBar.Value = 0;
			this.vScrollBar.Enabled = false;
		}

		public void ClearCache()
		{
			this.drawingBoard1.ClearCache();
		}

		public bool LoadImageFromStream(string fileName, ref Bitmap sourceImage, ref Exception exception)
		{
			float num = 0f;
			float num2 = 0f;
			return this.drawingBoard1.LoadImageFromStream(fileName, ref sourceImage, ref exception, ref num, ref num2);
		}

		public bool OpenImageFile(string fileName, PointF origin, int zoom, ref Exception exception)
		{
			return this.drawingBoard1.OpenImageFile(fileName, origin, zoom, ref exception);
		}

		public override void Refresh()
		{
			this.drawingBoard1.Refresh();
			base.Refresh();
		}

		public void ZoomIn()
		{
			if (!this.ZoomRestricted)
			{
				this.drawingBoard1.ZoomIn();
			}
		}

		public void ZoomOut()
		{
			if (!this.ZoomRestricted)
			{
				this.drawingBoard1.ZoomOut();
			}
		}

		public void FitToScreen()
		{
			if (!this.ZoomRestricted)
			{
				this.drawingBoard1.FitToScreen();
			}
		}

		public void FitHorizontally()
		{
			if (!this.ZoomRestricted)
			{
				this.drawingBoard1.FitHorizontally();
			}
		}

		public void FitVertically()
		{
			if (!this.ZoomRestricted)
			{
				this.drawingBoard1.FitVertically();
			}
		}

		public void ScrollLeft()
		{
			this.exitNow = true;
			this.Origin = ((this.Origin.X - (float)this.hScrollBar.SmallChange > 0f) ? new PointF(this.Origin.X - (float)this.hScrollBar.SmallChange, this.Origin.Y) : new PointF(0f, this.Origin.Y));
		}

		public void ScrollRight()
		{
			this.exitNow = true;
			this.Origin = (((double)(this.Origin.X + (float)this.hScrollBar.SmallChange) + (double)this.DrawingBoard.Width / this.ZoomFactor < (double)this.hScrollBar.Maximum) ? new PointF(this.Origin.X + (float)this.hScrollBar.SmallChange, this.Origin.Y) : new PointF((float)(this.drawingBoard1.Image.Width - (int)((double)this.drawingBoard1.Width / this.drawingBoard1.ZoomFactor)), this.Origin.Y));
		}

		public void ScrollUp()
		{
			this.exitNow = true;
			this.Origin = ((this.Origin.Y - (float)this.vScrollBar.SmallChange > 0f) ? new PointF(this.Origin.X, this.Origin.Y - (float)this.vScrollBar.SmallChange) : new PointF(this.Origin.X, 0f));
		}

		public void ScrollDown()
		{
			this.exitNow = true;
			this.Origin = (((double)(this.Origin.Y + (float)this.vScrollBar.SmallChange) + (double)this.DrawingBoard.Height / this.ZoomFactor < (double)this.vScrollBar.Maximum) ? new PointF(this.Origin.X, this.Origin.Y + (float)this.vScrollBar.SmallChange) : new PointF(this.Origin.X, (float)(this.drawingBoard1.Image.Height - (int)((double)this.drawingBoard1.Height / this.drawingBoard1.ZoomFactor))));
		}

		public void ScrollHome()
		{
			this.exitNow = true;
			this.Origin = new PointF(0f, 0f);
		}

		public void ScrollEnd()
		{
			this.exitNow = true;
			this.Origin = new PointF((float)(this.drawingBoard1.Image.Width - (int)((double)this.drawingBoard1.Width / this.drawingBoard1.ZoomFactor)), (float)(this.drawingBoard1.Image.Height - (int)((double)this.drawingBoard1.Height / this.drawingBoard1.ZoomFactor)));
		}

		public void ScrollPageUp()
		{
			this.exitNow = true;
			this.Origin = ((this.Origin.Y - (float)this.vScrollBar.LargeChange > 0f) ? new PointF(this.Origin.X, this.Origin.Y - (float)this.vScrollBar.LargeChange) : new PointF(this.Origin.X, 0f));
		}

		public void ScrollPageDown()
		{
			this.exitNow = true;
			this.Origin = (((double)(this.Origin.Y + (float)this.vScrollBar.LargeChange) + (double)this.DrawingBoard.Height / this.ZoomFactor < (double)this.vScrollBar.Maximum) ? new PointF(this.Origin.X, this.Origin.Y + (float)this.vScrollBar.LargeChange) : new PointF(this.Origin.X, (float)(this.drawingBoard1.Image.Height - (int)((double)this.drawingBoard1.Height / this.drawingBoard1.ZoomFactor))));
		}

		public int ScrollX()
		{
			return this.hScrollBar.SmallChange;
		}

		public int ScrollY()
		{
			return this.vScrollBar.SmallChange;
		}

		public Image Image
		{
			get
			{
				return this.drawingBoard1.Image;
			}
		}

		private void drawingBoard1_SetScrollPositions(object sender, EventArgs e)
		{
			try
			{
				int num = (int)this.drawingBoard1.Origin.X;
				int num2 = (int)this.drawingBoard1.Origin.Y;
				int num3 = (int)((double)this.drawingBoard1.CanvasSize.Width / this.drawingBoard1.ZoomFactor);
				int num4 = (int)((double)this.drawingBoard1.CanvasSize.Height / this.drawingBoard1.ZoomFactor);
				this.hScrollBar.Maximum = this.drawingBoard1.Image.Width;
				this.vScrollBar.Maximum = this.drawingBoard1.Image.Height;
				if (num3 >= this.drawingBoard1.Image.Width)
				{
					this.hScrollBar.Enabled = false;
					this.hScrollBar.Value = 0;
				}
				else
				{
					this.hScrollBar.SmallChange = (int)((double)num3 * 0.05);
					this.hScrollBar.LargeChange = num3;
					this.hScrollBar.Enabled = true;
					this.hScrollBar.Value = ((num < this.hScrollBar.Minimum) ? this.hScrollBar.Minimum : ((num > this.hScrollBar.Maximum) ? this.hScrollBar.Maximum : num));
				}
				if (num4 >= this.drawingBoard1.Image.Height)
				{
					this.vScrollBar.Enabled = false;
					this.vScrollBar.Value = 0;
				}
				else
				{
					this.vScrollBar.Enabled = true;
					this.vScrollBar.SmallChange = (int)((double)num3 * 0.05);
					this.vScrollBar.LargeChange = num4;
					this.vScrollBar.Value = ((num2 < this.vScrollBar.Minimum) ? this.vScrollBar.Minimum : ((num2 > this.vScrollBar.Maximum) ? this.vScrollBar.Maximum : num2));
				}
			}
			catch
			{
			}
		}

		private void ScrollBar_ValueChanged(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				this.exitNow = false;
				return;
			}
			this.drawingBoard1.Origin = new PointF((float)this.hScrollBar.Value, (float)this.vScrollBar.Value);
		}

		private void drawingBoard1_OnOriginChange(PointF origin)
		{
			if (this.OnOriginChange != null)
			{
				this.OnOriginChange(origin);
			}
		}

		private void drawingBoard1_OnZoomChange(int zoom)
		{
			base.Invalidate();
			if (this.OnZoomChange != null)
			{
				this.OnZoomChange(zoom);
			}
		}

		private void drawingBoard1_OnCacheLoaded(int zoom)
		{
			if (this.OnCacheLoaded != null)
			{
				this.OnCacheLoaded(zoom);
			}
		}

		private void drawingBoard1_OnMouseWheel(int zoom)
		{
			if (this.OnMouseWheel != null)
			{
				this.OnMouseWheel(zoom);
			}
		}

		private void drawingBoard1_Paint(object sender, PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.OnPaint != null)
			{
				this.OnPaint(e, this.drawingBoard1.Origin);
			}
		}

		private void MainControl_KeyDown(object sender, KeyEventArgs e)
		{
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((keyData == Keys.Right || keyData == Keys.Left || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Prior || keyData == Keys.Next || keyData == Keys.Home || keyData == Keys.End) && (Control.ModifierKeys & Keys.Control) == Keys.None && (Control.ModifierKeys & Keys.Shift) == Keys.None && (Control.ModifierKeys & Keys.Alt) == Keys.None)
			{
				this.ProcessDeplacementKeys(keyData);
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void ProcessDeplacementKeys(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Prior:
				this.ScrollPageUp();
				return;
			case Keys.Next:
				this.ScrollPageDown();
				return;
			case Keys.End:
				this.ScrollEnd();
				return;
			case Keys.Home:
				this.ScrollHome();
				return;
			case Keys.Left:
				this.ScrollLeft();
				return;
			case Keys.Up:
				this.ScrollUp();
				return;
			case Keys.Right:
				this.ScrollRight();
				return;
			case Keys.Down:
				this.ScrollDown();
				return;
			default:
				return;
			}
		}

		private void drawingBoard1_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.KeyDown != null)
			{
				this.KeyDown(this, e);
			}
			bool handled = e.Handled;
		}

		private void drawingBoard1_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.MouseDown != null)
			{
				this.MouseDown(this, e);
			}
		}

		private void drawingBoard1_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.MouseMove != null)
			{
				this.MouseMove(this, e);
			}
		}

		private void drawingBoard1_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.MouseUp != null)
			{
				this.MouseUp(this, e);
			}
		}

		private void MainControl_Resize(object sender, EventArgs e)
		{
			this.drawingBoard1.ClientSize = new Size(base.ClientSize.Width - this.vScrollBar.Width, base.ClientSize.Height - this.hScrollBar.Height);
			this.drawingBoard1.ComputeDrawingArea();
			this.ScrollbarsVisible = this.scrollVisible;
			base.Invalidate();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainControl));
			this.hScrollBar = new HScrollBar();
			this.vScrollBar = new VScrollBar();
			this.drawingBoard1 = new DrawingBoard();
			base.SuspendLayout();
			this.hScrollBar.Anchor = AnchorStyles.None;
			this.hScrollBar.Enabled = false;
			this.hScrollBar.LargeChange = 20;
			this.hScrollBar.Location = new Point(-1, 433);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new Size(510, 17);
			this.hScrollBar.TabIndex = 4;
			this.hScrollBar.KeyDown += this.drawingBoard1_KeyDown;
			this.vScrollBar.Anchor = AnchorStyles.None;
			this.vScrollBar.Enabled = false;
			this.vScrollBar.LargeChange = 20;
			this.vScrollBar.Location = new Point(509, 8);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new Size(17, 425);
			this.vScrollBar.TabIndex = 3;
			this.vScrollBar.KeyDown += this.drawingBoard1_KeyDown;
			this.drawingBoard1.BackColor = SystemColors.Control;
			this.drawingBoard1.Brightness = 0;
			this.drawingBoard1.CausesValidation = false;
			this.drawingBoard1.Contrast = 0;
			this.drawingBoard1.Location = new Point(0, 0);
			this.drawingBoard1.Margin = new Padding(0);
			this.drawingBoard1.Name = "drawingBoard1";
			this.drawingBoard1.Origin = (PointF)componentResourceManager.GetObject("drawingBoard1.Origin");
			this.drawingBoard1.Size = new Size(0, 0);
			this.drawingBoard1.TabIndex = 0;
			this.drawingBoard1.UseDynamicAdjustments = false;
			this.drawingBoard1.Zoom = 100;
			this.drawingBoard1.ZoomRestricted = false;
			this.drawingBoard1.Paint += this.drawingBoard1_Paint;
			this.drawingBoard1.KeyDown += this.drawingBoard1_KeyDown;
			this.drawingBoard1.MouseDown += this.drawingBoard1_MouseDown;
			this.drawingBoard1.MouseMove += this.drawingBoard1_MouseMove;
			this.drawingBoard1.MouseUp += this.drawingBoard1_MouseUp;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.Fixed3D;
			base.Controls.Add(this.vScrollBar);
			base.Controls.Add(this.drawingBoard1);
			base.Controls.Add(this.hScrollBar);
			base.Margin = new Padding(0);
			base.Name = "MainControl";
			base.Size = new Size(526, 454);
			this.KeyDown += this.MainControl_KeyDown;
			base.Resize += this.MainControl_Resize;
			base.ResumeLayout(false);
		}

		private new KeyEventHandler KeyDown;

		private new MouseEventHandler MouseDown;

		private new MouseEventHandler MouseMove;

		private new MouseEventHandler MouseUp;

		private new EventHandler MouseEnter;

		private new EventHandler MouseLeave;

		private new PaintEventHandler OnPaint;

		private ZoomChangeEventHandler OnZoomChange;

		private ZoomChangeEventHandler OnCacheLoaded;

		private new ZoomChangeEventHandler OnMouseWheel;

		private OriginChangeEventHandler OnOriginChange;

		private bool exitNow;

		private bool scrollVisible = true;

		private IContainer components;

		internal HScrollBar hScrollBar;

		internal VScrollBar vScrollBar;

		private DrawingBoard drawingBoard1;
	}
}
