using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlanControls
{
    public class MainControl : UserControl
    {
        private bool exitNow;

        private bool scrollVisible = true;

        private IContainer components;

        internal HScrollBar hScrollBar;

        internal VScrollBar vScrollBar;

        private DrawingBoard drawingBoard1;

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

        public bool CacheInUsed
        {
            get
            {
                return this.drawingBoard1.CacheInUsed;
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

        public DrawingBoard DrawingBoard
        {
            get
            {
                return this.drawingBoard1;
            }
        }

        public int FitToScreenZoom
        {
            get
            {
                return this.drawingBoard1.FitToScreenZoom;
            }
        }

        public bool HScrollBarEnabled
        {
            get
            {
                return this.hScrollBar.Enabled;
            }
        }

        public Image Image
        {
            get
            {
                return this.drawingBoard1.Image;
            }
        }

        public InterpolationModes ImageQuality
        {
            get
            {
                return this.drawingBoard1.ImageQuality;
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
                VScrollBar point = this.vScrollBar;
                Size clientSize = base.ClientSize;
                point.Location = new Point(clientSize.Width - this.vScrollBar.Width, 0);
                VScrollBar height = this.vScrollBar;
                Size size = base.ClientSize;
                height.Height = size.Height - this.hScrollBar.Height;
                HScrollBar hScrollBar = this.hScrollBar;
                Size clientSize1 = base.ClientSize;
                hScrollBar.Location = new Point(-1, clientSize1.Height - this.hScrollBar.Height);
                HScrollBar width = this.hScrollBar;
                Size size1 = base.ClientSize;
                width.Width = size1.Width - this.vScrollBar.Width;
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

        public bool VScrollBarEnabled
        {
            get
            {
                return this.vScrollBar.Enabled;
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

        public MainControl()
        {
            this.InitializeComponent();
            base.BorderStyle = BorderStyle.None;
            this.drawingBoard1.SetScrollPositions += new EventHandler(this.drawingBoard1_SetScrollPositions);
            this.drawingBoard1.OnZoomChange += new ZoomChangeEventHandler(this.drawingBoard1_OnZoomChange);
            this.drawingBoard1.OnCacheLoaded += new ZoomChangeEventHandler(this.drawingBoard1_OnCacheLoaded);
            this.drawingBoard1.OnMouseWheel += new ZoomChangeEventHandler(this.drawingBoard1_OnMouseWheel);
            this.drawingBoard1.OnOriginChange += new OriginChangeEventHandler(this.drawingBoard1_OnOriginChange);
            this.vScrollBar.ValueChanged += new EventHandler(this.ScrollBar_ValueChanged);
            this.hScrollBar.ValueChanged += new EventHandler(this.ScrollBar_ValueChanged);
        }

        public void ApplyBrightnessAndContrast(int brightness, int contrast)
        {
            this.drawingBoard1.ApplyBrightnessAndContrast(brightness, contrast);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void drawingBoard1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.OnPaint != null)
            {
                this.OnPaint(e, this.drawingBoard1.Origin);
            }
        }

        private void drawingBoard1_SetScrollPositions(object sender, EventArgs e)
        {
            int minimum;
            int num;
            try
            {
                int x = (int)this.drawingBoard1.Origin.X;
                int y = (int)this.drawingBoard1.Origin.Y;
                Size canvasSize = this.drawingBoard1.CanvasSize;
                int width = (int)((double)canvasSize.Width / this.drawingBoard1.ZoomFactor);
                Size size = this.drawingBoard1.CanvasSize;
                int height = (int)((double)size.Height / this.drawingBoard1.ZoomFactor);
                this.hScrollBar.Maximum = this.drawingBoard1.Image.Width;
                this.vScrollBar.Maximum = this.drawingBoard1.Image.Height;
                if (width < this.drawingBoard1.Image.Width)
                {
                    this.hScrollBar.SmallChange = (int)((double)width * 0.05);
                    this.hScrollBar.LargeChange = width;
                    this.hScrollBar.Enabled = true;
                    HScrollBar hScrollBar = this.hScrollBar;
                    if (x < this.hScrollBar.Minimum)
                    {
                        minimum = this.hScrollBar.Minimum;
                    }
                    else
                    {
                        minimum = (x > this.hScrollBar.Maximum ? this.hScrollBar.Maximum : x);
                    }
                    hScrollBar.Value = minimum;
                }
                else
                {
                    this.hScrollBar.Enabled = false;
                    this.hScrollBar.Value = 0;
                }
                if (height < this.drawingBoard1.Image.Height)
                {
                    this.vScrollBar.Enabled = true;
                    this.vScrollBar.SmallChange = (int)((double)width * 0.05);
                    this.vScrollBar.LargeChange = height;
                    VScrollBar vScrollBar = this.vScrollBar;
                    if (y < this.vScrollBar.Minimum)
                    {
                        num = this.vScrollBar.Minimum;
                    }
                    else
                    {
                        num = (y > this.vScrollBar.Maximum ? this.vScrollBar.Maximum : y);
                    }
                    vScrollBar.Value = num;
                }
                else
                {
                    this.vScrollBar.Enabled = false;
                    this.vScrollBar.Value = 0;
                }
            }
            catch
            {
            }
        }

        public void FitHorizontally()
        {
            if (!this.ZoomRestricted)
            {
                this.drawingBoard1.FitHorizontally();
            }
        }

        public void FitToScreen()
        {
            if (!this.ZoomRestricted)
            {
                this.drawingBoard1.FitToScreen();
            }
        }

        public void FitVertically()
        {
            if (!this.ZoomRestricted)
            {
                this.drawingBoard1.FitVertically();
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(MainControl));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");
            this.hScrollBar = new HScrollBar();
            this.vScrollBar = new VScrollBar();
            this.drawingBoard1 = new DrawingBoard();
            base.SuspendLayout();
            this.hScrollBar.Anchor = AnchorStyles.None;
            this.hScrollBar.Enabled = false;
            this.hScrollBar.LargeChange = 20;
            this.hScrollBar.Location = new Point(-1, 0x1b1);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new Size(0x1fe, 17);
            this.hScrollBar.TabIndex = 4;
            this.hScrollBar.KeyDown += new KeyEventHandler(this.drawingBoard1_KeyDown);
            this.vScrollBar.Anchor = AnchorStyles.None;
            this.vScrollBar.Enabled = false;
            this.vScrollBar.LargeChange = 20;
            this.vScrollBar.Location = new Point(0x1fd, 8);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new Size(17, 0x1a9);
            this.vScrollBar.TabIndex = 3;
            this.vScrollBar.KeyDown += new KeyEventHandler(this.drawingBoard1_KeyDown);
            this.drawingBoard1.BackColor = SystemColors.Control;
            this.drawingBoard1.Brightness = 0;
            this.drawingBoard1.CausesValidation = false;
            this.drawingBoard1.Contrast = 0;
            this.drawingBoard1.Location = new Point(0, 0);
            this.drawingBoard1.Margin = new Padding(0);
            this.drawingBoard1.Name = "drawingBoard1";
            this.drawingBoard1.Origin = (PointF)resources.GetObject("drawingBoard1.Origin");
            this.drawingBoard1.Size = new Size(0, 0);
            this.drawingBoard1.TabIndex = 0;
            this.drawingBoard1.UseDynamicAdjustments = false;
            this.drawingBoard1.Zoom = 100;
            this.drawingBoard1.ZoomRestricted = false;
            this.drawingBoard1.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingBoard1_Paint);
            this.drawingBoard1.KeyDown += new KeyEventHandler(this.drawingBoard1_KeyDown);
            this.drawingBoard1.MouseDown += new MouseEventHandler(this.drawingBoard1_MouseDown);
            this.drawingBoard1.MouseMove += new MouseEventHandler(this.drawingBoard1_MouseMove);
            this.drawingBoard1.MouseUp += new MouseEventHandler(this.drawingBoard1_MouseUp);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.BorderStyle = BorderStyle.Fixed3D;
            base.Controls.Add(this.vScrollBar);
            base.Controls.Add(this.drawingBoard1);
            base.Controls.Add(this.hScrollBar);
            base.Margin = new Padding(0);
            base.Name = "MainControl";
            base.Size = new Size(0x20e, 0x1c6);
            this.KeyDown += new KeyEventHandler(this.MainControl_KeyDown);
            base.Resize += new EventHandler(this.MainControl_Resize);
            base.ResumeLayout(false);
        }


        public bool LoadImageFromStream(string fileName, ref Bitmap sourceImage, ref Exception exception)
        {
            float single = 0f;
            float single1 = 0f;
            return this.drawingBoard1.LoadImageFromStream(fileName, ref sourceImage, ref exception, ref single, ref single1);
        }

        private void MainControl_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void MainControl_Resize(object sender, EventArgs e)
        {
            DrawingBoard size = this.drawingBoard1;
            int width = base.ClientSize.Width - this.vScrollBar.Width;
            Size clientSize = base.ClientSize;
            size.ClientSize = new Size(width, clientSize.Height - this.hScrollBar.Height);
            this.drawingBoard1.ComputeDrawingArea();
            this.ScrollbarsVisible = this.scrollVisible;
            base.Invalidate();
        }

        public bool OpenImageFile(string fileName, PointF origin, int zoom, ref Exception exception)
        {
            return this.drawingBoard1.OpenImageFile(fileName, origin, zoom, ref exception);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Right && keyData != Keys.Left && keyData != Keys.Up && keyData != Keys.Down && keyData != Keys.Prior && keyData != Keys.Next && keyData != Keys.Home && keyData != Keys.End || (Control.ModifierKeys & Keys.Control) != Keys.None || (Control.ModifierKeys & Keys.Shift) != Keys.None || (Control.ModifierKeys & Keys.Alt) != Keys.None)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            this.ProcessDeplacementKeys(keyData);
            return true;
        }

        private void ProcessDeplacementKeys(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Prior:
                    {
                        this.ScrollPageUp();
                        return;
                    }
                case Keys.Next:
                    {
                        this.ScrollPageDown();
                        return;
                    }
                case Keys.End:
                    {
                        this.ScrollEnd();
                        return;
                    }
                case Keys.Home:
                    {
                        this.ScrollHome();
                        return;
                    }
                case Keys.Left:
                    {
                        this.ScrollLeft();
                        return;
                    }
                case Keys.Up:
                    {
                        this.ScrollUp();
                        return;
                    }
                case Keys.Right:
                    {
                        this.ScrollRight();
                        return;
                    }
                case Keys.Down:
                    {
                        this.ScrollDown();
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        public override void Refresh()
        {
            this.drawingBoard1.Refresh();
            base.Refresh();
        }

        public void ReloadAdaptiveZoomedImage()
        {
            this.drawingBoard1.ReloadAdaptiveZoomedImage();
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

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (this.exitNow)
            {
                this.exitNow = false;
                return;
            }
            this.drawingBoard1.Origin = new PointF((float)this.hScrollBar.Value, (float)this.vScrollBar.Value);
        }

        public void ScrollDown()
        {
            PointF pointF;
            this.exitNow = true;
            if ((double)(this.Origin.Y + (float)this.vScrollBar.SmallChange) + (double)this.DrawingBoard.Height / this.ZoomFactor < (double)this.vScrollBar.Maximum)
            {
                float x = this.Origin.X;
                PointF origin = this.Origin;
                pointF = new PointF(x, origin.Y + (float)this.vScrollBar.SmallChange);
            }
            else
            {
                PointF origin1 = this.Origin;
                pointF = new PointF(origin1.X, (float)(this.drawingBoard1.Image.Height - (int)((double)this.drawingBoard1.Height / this.drawingBoard1.ZoomFactor)));
            }
            this.Origin = pointF;
        }

        public void ScrollEnd()
        {
            this.exitNow = true;
            this.Origin = new PointF((float)(this.drawingBoard1.Image.Width - (int)((double)this.drawingBoard1.Width / this.drawingBoard1.ZoomFactor)), (float)(this.drawingBoard1.Image.Height - (int)((double)this.drawingBoard1.Height / this.drawingBoard1.ZoomFactor)));
        }

        public void ScrollHome()
        {
            this.exitNow = true;
            this.Origin = new PointF(0f, 0f);
        }

        public void ScrollLeft()
        {
            PointF pointF;
            this.exitNow = true;
            if (this.Origin.X - (float)this.hScrollBar.SmallChange > 0f)
            {
                PointF origin = this.Origin;
                pointF = new PointF(origin.X - (float)this.hScrollBar.SmallChange, this.Origin.Y);
            }
            else
            {
                pointF = new PointF(0f, this.Origin.Y);
            }
            this.Origin = pointF;
        }

        public void ScrollPageDown()
        {
            PointF pointF;
            this.exitNow = true;
            if ((double)(this.Origin.Y + (float)this.vScrollBar.LargeChange) + (double)this.DrawingBoard.Height / this.ZoomFactor < (double)this.vScrollBar.Maximum)
            {
                float x = this.Origin.X;
                PointF origin = this.Origin;
                pointF = new PointF(x, origin.Y + (float)this.vScrollBar.LargeChange);
            }
            else
            {
                PointF origin1 = this.Origin;
                pointF = new PointF(origin1.X, (float)(this.drawingBoard1.Image.Height - (int)((double)this.drawingBoard1.Height / this.drawingBoard1.ZoomFactor)));
            }
            this.Origin = pointF;
        }

        public void ScrollPageUp()
        {
            PointF pointF;
            this.exitNow = true;
            if (this.Origin.Y - (float)this.vScrollBar.LargeChange > 0f)
            {
                float x = this.Origin.X;
                PointF origin = this.Origin;
                pointF = new PointF(x, origin.Y - (float)this.vScrollBar.LargeChange);
            }
            else
            {
                pointF = new PointF(this.Origin.X, 0f);
            }
            this.Origin = pointF;
        }

        public void ScrollRight()
        {
            PointF pointF;
            this.exitNow = true;
            if ((double)(this.Origin.X + (float)this.hScrollBar.SmallChange) + (double)this.DrawingBoard.Width / this.ZoomFactor < (double)this.hScrollBar.Maximum)
            {
                PointF origin = this.Origin;
                pointF = new PointF(origin.X + (float)this.hScrollBar.SmallChange, this.Origin.Y);
            }
            else
            {
                pointF = new PointF((float)(this.drawingBoard1.Image.Width - (int)((double)this.drawingBoard1.Width / this.drawingBoard1.ZoomFactor)), this.Origin.Y);
            }
            this.Origin = pointF;
        }

        public void ScrollUp()
        {
            PointF pointF;
            this.exitNow = true;
            if (this.Origin.Y - (float)this.vScrollBar.SmallChange > 0f)
            {
                float x = this.Origin.X;
                PointF origin = this.Origin;
                pointF = new PointF(x, origin.Y - (float)this.vScrollBar.SmallChange);
            }
            else
            {
                pointF = new PointF(this.Origin.X, 0f);
            }
            this.Origin = pointF;
        }

        public int ScrollX()
        {
            return this.hScrollBar.SmallChange;
        }

        public int ScrollY()
        {
            return this.vScrollBar.SmallChange;
        }

        public void SetImageQuality(InterpolationModes.ZoomedImagesEnum index, InterpolationMode interpolationMode)
        {
            this.drawingBoard1.SetImageQuality(index, interpolationMode);
        }

        public void ZoomIn()
        {
            if (!this.ZoomRestricted)
            {
                this.drawingBoard1.ZoomIn();
            }
        }

        public void ZoomLock(int zoom)
        {
            this.drawingBoard1.ZoomLock(zoom);
        }

        public void ZoomOut()
        {
            if (!this.ZoomRestricted)
            {
                this.drawingBoard1.ZoomOut();
            }
        }

        public event KeyEventHandler KeyDown;

        public event MouseEventHandler MouseDown;

        public event EventHandler MouseEnter;

        public event EventHandler MouseLeave;

        public event MouseEventHandler MouseMove;

        public event MouseEventHandler MouseUp;

        public event ZoomChangeEventHandler OnCacheLoaded;

        public event ZoomChangeEventHandler OnMouseWheel;

        public event OriginChangeEventHandler OnOriginChange;

        public event QuoterPlanControls.PaintEventHandler OnPaint;

        public event ZoomChangeEventHandler OnZoomChange;
    }
}