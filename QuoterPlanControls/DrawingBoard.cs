using QuoterPlanImaging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlanControls
{
    [ComVisible(false)]
    public class DrawingBoard : UserControl
    {
        private const int WM_SETREDRAW = 11;

        private Bitmap m_OriginalImage;

        private Bitmap[] m_ZoomedImage = new Bitmap[3];

        private Bitmap m_VisibleImage;

        private IdleMonitor m_idleMonitor;

        private InterpolationModes m_InterpolationModes = new InterpolationModes();

        private Size m_CanvasSize;

        private float m_OriginalDpiX;

        private float m_OriginalDpiY;

        private bool m_MustUpdateVisibleImage = true;

        private PointF m_Origin;

        private int m_ZoomValue = 100;

        private bool m_ZoomRestricted;

        private PointF m_ZoomToPointerLocation = PointF.Empty;

        private int m_LastAdaptiveZoomValue;

        private PointF m_LastAdaptiveOrigin = PointF.Empty;

        private bool m_CanUseAdaptiveCache;

        private int m_brightness;

        private int m_contrast;

        private bool m_useDynamicAdjustments;

        private int m_HorizontalOffset;

        private int m_VerticalOffset;

        private int m_ZoomInterval = 6;

        private IContainer components;

        public int Brightness
        {
            get
            {
                return this.m_brightness;
            }
            set
            {
                int num;
                if (value < -255)
                {
                    num = -255;
                }
                else
                {
                    num = (value > 0xff ? 0xff : value);
                }
                this.m_brightness = num;
                if (this.m_OriginalImage != null && this.m_useDynamicAdjustments)
                {
                    this.m_MustUpdateVisibleImage = true;
                    base.Invalidate();
                }
            }
        }

        public bool CacheInUsed
        {
            get
            {
                if (this.m_OriginalImage == null || this.m_ZoomValue != this.m_LastAdaptiveZoomValue && this.m_ZoomValue != 25 && this.m_ZoomValue != 50 && this.m_ZoomValue != 100)
                {
                    return false;
                }
                return !this.m_useDynamicAdjustments;
            }
        }

        public bool CanUseAdaptiveCache
        {
            get
            {
                this.CheckIfCanUseAdaptiveCache();
                return this.m_CanUseAdaptiveCache;
            }
        }

        public Size CanvasSize
        {
            get
            {
                return new Size(base.ClientSize.Width, base.ClientSize.Height);
            }
        }

        public int Contrast
        {
            get
            {
                return this.m_contrast;
            }
            set
            {
                int num;
                if (value < -100)
                {
                    num = -100;
                }
                else
                {
                    num = (value > 100 ? 100 : value);
                }
                this.m_contrast = num;
                if (this.m_OriginalImage != null && this.m_useDynamicAdjustments)
                {
                    this.m_MustUpdateVisibleImage = true;
                    base.Invalidate();
                }
            }
        }

        public int FitToScreenZoom
        {
            get
            {
                Size canvasSize = this.CanvasSize;
                float width = (float)canvasSize.Width / (float)this.m_OriginalImage.Width;
                Size size = this.CanvasSize;
                float single = Math.Min(width, (float)size.Height / (float)this.m_OriginalImage.Height);
                return (int)(single * 100f);
            }
        }

        public int HorizontalOffset
        {
            get
            {
                return this.m_HorizontalOffset;
            }
            set
            {
                this.m_HorizontalOffset = value;
            }
        }

        public Image Image
        {
            get
            {
                return this.m_OriginalImage;
            }
        }

        public InterpolationModes ImageQuality
        {
            get
            {
                return this.m_InterpolationModes;
            }
        }

        public PointF Origin
        {
            get
            {
                return this.m_Origin;
            }
            set
            {
                this.SetOrigin(value);
                base.Invalidate();
            }
        }

        public float OriginalDpiX
        {
            get
            {
                return this.m_OriginalDpiX;
            }
        }

        public float OriginalDpiY
        {
            get
            {
                return this.m_OriginalDpiY;
            }
        }

        public PointF TempOffset
        {
            set
            {
                this.m_Origin = value;
            }
        }

        public bool UseDynamicAdjustments
        {
            get
            {
                return this.m_useDynamicAdjustments;
            }
            set
            {
                this.m_useDynamicAdjustments = value;
            }
        }

        public int VerticalOffset
        {
            get
            {
                return this.m_VerticalOffset;
            }
            set
            {
                this.m_VerticalOffset = value;
            }
        }

        public int Zoom
        {
            get
            {
                return this.m_ZoomValue;
            }
            set
            {
                this.SetZoom(value);
                base.Invalidate();
            }
        }

        public double ZoomFactor
        {
            get
            {
                return (double)this.m_ZoomValue / 100;
            }
        }

        public bool ZoomRestricted
        {
            get
            {
                return this.m_ZoomRestricted;
            }
            set
            {
                this.m_ZoomRestricted = value;
            }
        }

        public DrawingBoard()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.MouseWheel += new MouseEventHandler(this.DrawingBoard_MouseWheel);
            base.Location = Point.Empty;
        }

        public void ApplyBrightnessAndContrast(int brightness, int contrast)
        {
            if (this.m_OriginalImage == null)
            {
                return;
            }
            if (brightness == 0 && contrast == 0)
            {
                return;
            }
            this.m_useDynamicAdjustments = false;
            if (brightness != 0)
            {
                this.SetBrightness(this.m_OriginalImage, brightness);
            }
            if (contrast != 0)
            {
                this.SetContrast(this.m_OriginalImage, contrast);
            }
            this.ReloadZoomedImages();
            this.m_MustUpdateVisibleImage = true;
            base.Invalidate();
        }

        private void ApplyZoom(int zoom)
        {
            PointF empty = Point.Empty;
            if (this.m_ZoomToPointerLocation != Point.Empty)
            {
                PointF pointF = new PointF((float)((int)((double)this.m_ZoomToPointerLocation.X / this.ZoomFactor)), (float)((int)((double)this.m_ZoomToPointerLocation.Y / this.ZoomFactor)));
                PointF pointF1 = new PointF((float)((int)((double)this.m_ZoomToPointerLocation.X / ((double)zoom / 100))), (float)((int)((double)this.m_ZoomToPointerLocation.Y / ((double)zoom / 100))));
                this.m_ZoomToPointerLocation = new PointF(pointF.X - pointF1.X - (float)((int)((double)this.HorizontalOffset / this.ZoomFactor)), pointF.Y - pointF1.Y - (float)((int)((double)this.VerticalOffset / this.ZoomFactor)));
            }
            else
            {
                float x = this.m_Origin.X;
                Size clientSize = base.ClientSize;
                float width = x + (float)((int)((double)clientSize.Width / this.ZoomFactor / 2));
                float y = this.m_Origin.Y;
                Size size = base.ClientSize;
                empty = new PointF(width, y + (float)((int)((double)size.Height / this.ZoomFactor / 2)));
            }
            this.m_ZoomValue = zoom;
            if (this.m_ZoomValue > 0x12c)
            {
                this.m_ZoomValue = 0x12c;
            }
            if (this.m_ZoomValue < 10)
            {
                this.m_ZoomValue = 10;
            }
            this.m_LastAdaptiveZoomValue = 0;
            this.ComputeDrawingArea();
            if (this.m_ZoomToPointerLocation == Point.Empty)
            {
                float single = empty.X;
                Size clientSize1 = base.ClientSize;
                float width1 = single - (float)((int)((double)clientSize1.Width / this.ZoomFactor / 2));
                float y1 = empty.Y;
                Size size1 = base.ClientSize;
                this.m_Origin = new PointF(width1, y1 - (float)((int)((double)size1.Height / this.ZoomFactor / 2)));
            }
            else if (this.m_ZoomValue >= 10 && this.m_ZoomValue < 0x12c)
            {
                this.m_Origin = new PointF(this.m_Origin.X + this.m_ZoomToPointerLocation.X, this.m_Origin.Y + this.m_ZoomToPointerLocation.Y);
            }
            this.CheckBounds();
            if (this.OnZoomChange != null)
            {
                this.OnZoomChange(this.m_ZoomValue);
            }
        }

        public void CenterToLocation(Point location)
        {
            this.ZoomImage(true, -1);
        }

        private void CheckBounds()
        {
            if (this.m_OriginalImage == null)
            {
                return;
            }
            if (this.m_Origin.X < 0f)
            {
                this.m_Origin.X = 0f;
            }
            if (this.m_Origin.Y < 0f)
            {
                this.m_Origin.Y = 0f;
            }
            if ((double)this.m_Origin.X > (double)this.m_OriginalImage.Width - (double)base.ClientSize.Width / this.ZoomFactor)
            {
                double width = (double)this.m_OriginalImage.Width;
                Size clientSize = base.ClientSize;
                this.m_Origin.X = (float)(width - (double)clientSize.Width / this.ZoomFactor);
            }
            if ((double)this.m_Origin.Y > (double)this.m_OriginalImage.Height - (double)base.ClientSize.Height / this.ZoomFactor)
            {
                double height = (double)this.m_OriginalImage.Height;
                Size size = base.ClientSize;
                this.m_Origin.Y = (float)(height - (double)size.Height / this.ZoomFactor);
            }
            if (this.m_Origin.X < 0f)
            {
                this.m_Origin.X = 0f;
            }
            if (this.m_Origin.Y < 0f)
            {
                this.m_Origin.Y = 0f;
            }
        }

        private void CheckIfCanUseAdaptiveCache()
        {
            try
            {
                this.m_CanUseAdaptiveCache = this.Image.Width * this.Image.Height <= 0x2255100;
            }
            catch
            {
            }
        }

        public void Clear()
        {
            this.DisposeValues(false);
            base.Invalidate();
        }

        public void ClearCache()
        {
            this.DisposeValues(true);
        }

        public void ComputeDrawingArea()
        {
            DrawingBoard.SuspendDrawing(this);
            if (this.m_OriginalImage != null)
            {
                int width = (int)((double)this.m_OriginalImage.Width * this.ZoomFactor);
                int height = (int)((double)this.m_OriginalImage.Height * this.ZoomFactor);
                this.HorizontalOffset = (width < this.CanvasSize.Width ? (this.CanvasSize.Width - width) / 2 : 0);
                this.VerticalOffset = (height < this.CanvasSize.Height ? (this.CanvasSize.Height - height) / 2 : 0);
            }
            DrawingBoard.ResumeDrawing(this);
            this.m_MustUpdateVisibleImage = true;
        }

        private bool CreateZoomedImage(Bitmap originalImage, ref Bitmap zoomedImage, InterpolationMode interpolationMode)
        {
            bool flag;
            try
            {
                using (Graphics graphic = Graphics.FromImage(zoomedImage))
                {
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    graphic.InterpolationMode = interpolationMode;
                    graphic.DrawImage(originalImage, new Rectangle(new Point(0, 0), zoomedImage.Size), new Rectangle(new Point(0, 0), originalImage.Size), GraphicsUnit.Pixel);
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Console.WriteLine("The image could not be zoomed:");
                Console.WriteLine(exception.Message);
                return false;
            }
            return flag;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DisposeIdleTimer()
        {
            if (this.m_idleMonitor != null)
            {
                this.m_idleMonitor.IdleStateChanged -= new EventHandler(this.idleMonitor_IdleStateChanged);
                this.m_idleMonitor.Dispose();
                this.m_idleMonitor = null;
            }
        }

        private void DisposeValues(bool preserveOriginalImage = false)
        {
            if (this.m_OriginalImage != null && !preserveOriginalImage)
            {
                this.m_OriginalImage.Dispose();
                this.m_OriginalImage = null;
            }
            for (int i = 0; i < 3; i++)
            {
                if (this.m_ZoomedImage[i] != null)
                {
                    this.m_ZoomedImage[i].Dispose();
                    this.m_ZoomedImage[i] = null;
                }
            }
            this.DisposeIdleTimer();
            this.m_CanUseAdaptiveCache = false;
            GC.Collect();
        }

        private void DrawImage(ref Graphics g)
        {
            RectangleF rectangleF;
            int num;
            double num1;
            if (this.m_OriginalImage == null)
            {
                g.Clear(this.BackColor);
                return;
            }
            RectangleF rectangle = new Rectangle(this.HorizontalOffset, this.VerticalOffset, base.ClientSize.Width, base.ClientSize.Height);
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            if (this.ZoomFactor == 1 && !this.m_useDynamicAdjustments)
            {
                float x = (float)((int)((double)this.m_Origin.X * this.ZoomFactor));
                float y = (float)((int)((double)this.m_Origin.Y * this.ZoomFactor));
                float width = (float)base.ClientSize.Width;
                Size clientSize = base.ClientSize;
                rectangleF = new RectangleF(x, y, width, (float)clientSize.Height);
                g.DrawImage(this.m_OriginalImage, rectangle, rectangleF, GraphicsUnit.Pixel);
            }
            else if (this.ZoomFactor != 0.5 && this.ZoomFactor != 0.25 && (this.ZoomFactor >= 1 || this.m_ZoomValue != this.m_LastAdaptiveZoomValue || this.m_LastAdaptiveOrigin.Equals(this.m_Origin)) || this.m_useDynamicAdjustments)
            {
                if (this.m_MustUpdateVisibleImage || this.m_VisibleImage == null)
                {
                    if (this.m_VisibleImage != null)
                    {
                        this.m_VisibleImage.Dispose();
                        this.m_VisibleImage = null;
                    }
                    int width1 = base.ClientSize.Width;
                    Size size = base.ClientSize;
                    this.m_VisibleImage = new Bitmap(width1, size.Height, PixelFormat.Format32bppPArgb);
                    using (Graphics item = Graphics.FromImage(this.m_VisibleImage))
                    {
                        item.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        item.SmoothingMode = SmoothingMode.HighQuality;
                        if (this.ZoomFactor <= 0.5)
                        {
                            if (this.ZoomFactor <= 0.25 || this.ZoomFactor > 0.5)
                            {
                                item.InterpolationMode = this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Below25Percent];
                                num = 1;
                                num1 = 0.25;
                            }
                            else
                            {
                                item.InterpolationMode = this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Between25and50Percents];
                                num = 0;
                                num1 = 0.5;
                            }
                            if (this.ZoomedImage(num) != null)
                            {
                                float single = (float)((int)((double)this.m_Origin.X * num1));
                                float y1 = (float)((int)((double)this.m_Origin.Y * num1));
                                Size clientSize1 = base.ClientSize;
                                float single1 = (float)((float)clientSize1.Width / (float)(this.ZoomFactor / num1));
                                Size size1 = base.ClientSize;
                                rectangleF = new RectangleF(single, y1, single1, (float)((float)size1.Height / (float)(this.ZoomFactor / num1)));
                                item.DrawImage(this.ZoomedImage(num), rectangle, rectangleF, GraphicsUnit.Pixel);
                            }
                            else
                            {
                                g.Clear(this.BackColor);
                                return;
                            }
                        }
                        else
                        {
                            item.InterpolationMode = this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.MoreThan50Percent];
                            float x1 = this.m_Origin.X;
                            float y2 = this.m_Origin.Y;
                            Size clientSize2 = base.ClientSize;
                            float width2 = (float)((double)clientSize2.Width / this.ZoomFactor);
                            Size size2 = base.ClientSize;
                            rectangleF = new RectangleF(x1, y2, width2, (float)((double)size2.Height / this.ZoomFactor));
                            item.DrawImage(this.m_OriginalImage, rectangle, rectangleF, GraphicsUnit.Pixel);
                        }
                        if (this.m_useDynamicAdjustments)
                        {
                            if (this.Brightness != 0)
                            {
                                this.SetBrightness(this.m_VisibleImage, this.Brightness);
                            }
                            if (this.Contrast != 0)
                            {
                                this.SetContrast(this.m_VisibleImage, this.Contrast);
                            }
                        }
                    }
                }
                int mZoomValue = this.m_ZoomValue;
                int mLastAdaptiveZoomValue = this.m_LastAdaptiveZoomValue;
                g.DrawImageUnscaled(this.m_VisibleImage, Point.Empty);
            }
            else if (this.ZoomFactor == 0.5 || this.ZoomFactor == 0.25)
            {
                if (this.ZoomedImage((this.ZoomFactor == 0.5 ? 0 : 1)) == null)
                {
                    g.Clear(this.BackColor);
                    return;
                }
                float x2 = (float)((int)((double)this.m_Origin.X * this.ZoomFactor));
                float single2 = (float)((int)((double)this.m_Origin.Y * this.ZoomFactor));
                float width3 = (float)base.ClientSize.Width;
                Size clientSize3 = base.ClientSize;
                rectangleF = new RectangleF(x2, single2, width3, (float)clientSize3.Height);
                g.DrawImage(this.ZoomedImage((this.ZoomFactor == 0.5 ? 0 : 1)), rectangle, rectangleF, GraphicsUnit.Pixel);
            }
            else
            {
                if (this.ZoomedImage(2) == null)
                {
                    g.Clear(this.BackColor);
                    return;
                }
                float x3 = (float)((int)((double)this.m_Origin.X * this.ZoomFactor));
                float y3 = (float)((int)((double)this.m_Origin.Y * this.ZoomFactor));
                float single3 = (float)base.ClientSize.Width;
                Size size3 = base.ClientSize;
                rectangleF = new RectangleF(x3, y3, single3, (float)size3.Height);
                g.DrawImage(this.ZoomedImage(2), rectangle, rectangleF, GraphicsUnit.Pixel);
            }
            this.SetScrollPositions(this, new EventArgs());
            this.m_MustUpdateVisibleImage = false;
        }

        private void DrawingBoard_Load(object sender, EventArgs e)
        {
        }

        public void DrawingBoard_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.Image == null)
            {
                return;
            }
            if (e.Delta > 0)
            {
                this.ZoomIn(e.Location);
            }
            else if (e.Delta < 0)
            {
                this.ZoomOut(e.Location);
            }
            if (this.OnMouseWheel != null)
            {
                this.OnMouseWheel(this.m_ZoomValue);
            }
        }

        public void FitHorizontally()
        {
            this.Origin = new Point(0, 0);
            if (this.m_OriginalImage != null)
            {
                Size canvasSize = this.CanvasSize;
                float width = (float)canvasSize.Width / (float)this.m_OriginalImage.Width;
                this.Zoom = (int)(width * 100f);
            }
        }

        public void FitToScreen()
        {
            this.Origin = new Point(0, 0);
            if (this.m_OriginalImage != null)
            {
                this.Zoom = this.FitToScreenZoom;
            }
        }

        public void FitVertically()
        {
            this.Origin = new Point(0, 0);
            if (this.m_OriginalImage != null)
            {
                Size canvasSize = this.CanvasSize;
                float height = (float)canvasSize.Height / (float)this.m_OriginalImage.Height;
                this.Zoom = (int)(height * 100f);
            }
        }

        private void idleMonitor_IdleStateChanged(object sender, EventArgs e)
        {
            Console.WriteLine("idleMonitor_IdleStateChanged");
            this.m_idleMonitor.Enabled = false;
            if (this.m_ZoomedImage[0] == null)
            {
                this.LoadZoomedImage(0);
            }
            if (this.m_ZoomedImage[1] == null)
            {
                this.LoadZoomedImage(1);
            }
            if (this.m_CanUseAdaptiveCache && this.m_ZoomValue != 25 && this.m_ZoomValue != 50 && this.m_ZoomValue < 100 && this.m_ZoomValue != this.m_LastAdaptiveZoomValue)
            {
                this.LoadAdaptiveZoomedImage(this.Zoom);
            }
            this.DisposeIdleTimer();
        }

        private void InitialiseValues()
        {
            this.DisposeValues(false);
            this.m_Origin = Point.Empty;
            this.m_ZoomValue = 100;
            this.m_ZoomToPointerLocation = Point.Empty;
            this.m_HorizontalOffset = 0;
            this.m_VerticalOffset = 0;
            this.m_LastAdaptiveZoomValue = 0;
            this.m_LastAdaptiveOrigin = Point.Empty;
            this.m_brightness = 0;
            this.m_contrast = 0;
            this.m_useDynamicAdjustments = false;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CausesValidation = false;
            base.Margin = new Padding(0);
            base.Name = "DrawingBoard";
            base.Size = new Size(233, 247);
            base.Load += new EventHandler(this.DrawingBoard_Load);
            base.ResumeLayout(false);
        }

        private void InitializeIdleTimer()
        {
            if (this.ZoomRestricted)
            {
                return;
            }
            if (this.m_OriginalImage == null)
            {
                return;
            }
            if ((!this.m_CanUseAdaptiveCache || this.m_ZoomValue == 25 || this.m_ZoomValue == 50 || this.m_ZoomValue >= 100 || this.m_ZoomValue == this.m_LastAdaptiveZoomValue) && this.m_ZoomedImage[0] != null && this.m_ZoomedImage[1] != null)
            {
                return;
            }
            if (this.m_idleMonitor == null)
            {
                this.m_idleMonitor = new IdleMonitor(0x28a, 25);
                this.m_idleMonitor.IdleStateChanged += new EventHandler(this.idleMonitor_IdleStateChanged);
            }
        }

        private bool LoadAdaptiveZoomedImage(int zoom)
        {
            if (!this.m_CanUseAdaptiveCache)
            {
                return false;
            }
            if (zoom == this.m_LastAdaptiveZoomValue || zoom == 25 || zoom == 50 || zoom >= 100)
            {
                return true;
            }
            if (this.m_ZoomedImage[2] != null)
            {
                this.m_ZoomedImage[2].Dispose();
                this.m_ZoomedImage[2] = null;
                GC.Collect();
            }
            bool flag = false;
            this.m_ZoomedImage[2] = new Bitmap((int)((float)this.m_OriginalImage.Width * (float)((float)zoom / 100f)), (int)((float)this.m_OriginalImage.Height * (float)((float)zoom / 100f)), PixelFormat.Format32bppPArgb);
            if (this.ZoomFactor <= 0.5)
            {
                flag = (this.ZoomFactor <= 0.25 || this.ZoomFactor > 0.5 ? this.CreateZoomedImage(this.m_ZoomedImage[1], ref this.m_ZoomedImage[2], this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Below25Percent]) : this.CreateZoomedImage(this.m_ZoomedImage[0], ref this.m_ZoomedImage[2], this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Between25and50Percents]));
            }
            else
            {
                flag = this.CreateZoomedImage(this.m_OriginalImage, ref this.m_ZoomedImage[2], this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.MoreThan50Percent]);
            }
            if (!flag)
            {
                this.m_LastAdaptiveOrigin = Point.Empty;
                this.m_LastAdaptiveZoomValue = 0;
                return false;
            }
            this.m_LastAdaptiveOrigin = new PointF(this.m_Origin.X, this.m_Origin.Y);
            this.m_LastAdaptiveZoomValue = zoom;
            if (this.OnCacheLoaded != null)
            {
                this.OnCacheLoaded(zoom);
            }
            return true;
        }

        public bool LoadImageFromStream(string fileName, ref Bitmap sourceImage, ref Exception exception, ref float imageDpiX, ref float imageDpiY)
        {
            bool flag = false;
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (Image image = Image.FromStream(fileStream))
                    {
                        imageDpiX = image.HorizontalResolution;
                        imageDpiY = image.VerticalResolution;
                        PixelFormat pixelFormat = image.PixelFormat;
                        if (pixelFormat <= PixelFormat.Format8bppIndexed)
                        {
                            if (pixelFormat == PixelFormat.Format32bppRgb)
                            {
                                sourceImage = Imaging.CloneBitmap((Bitmap)image, PixelFormat.Format32bppPArgb, ref exception);
                                if (sourceImage != null)
                                {
                                    sourceImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                                    flag = true;
                                }
                                if (!flag && sourceImage != null)
                                {
                                    sourceImage.Dispose();
                                }
                                GC.Collect();
                                return flag;
                            }
                            if (pixelFormat != PixelFormat.Format8bppIndexed)
                            {
                                sourceImage = ((Bitmap)image).Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format32bppPArgb);
                                if (sourceImage != null)
                                {
                                    sourceImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                                    flag = true;
                                }
                                if (!flag && sourceImage != null)
                                {
                                    sourceImage.Dispose();
                                }
                                GC.Collect();
                                return flag;
                            }
                            sourceImage = Imaging.GrayscaleToColor((Bitmap)image, ref exception);
                            if (sourceImage != null)
                            {
                                sourceImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                                flag = true;
                            }
                            if (!flag && sourceImage != null)
                            {
                                sourceImage.Dispose();
                            }
                            GC.Collect();
                            return flag;
                        }
                        else if (pixelFormat != PixelFormat.Format32bppPArgb && pixelFormat != PixelFormat.Format32bppArgb)
                        {
                            sourceImage = ((Bitmap)image).Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format32bppPArgb);
                            if (sourceImage != null)
                            {
                                sourceImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                                flag = true;
                            }
                            if (!flag && sourceImage != null)
                            {
                                sourceImage.Dispose();
                            }
                            GC.Collect();
                            return flag;
                        }
                        sourceImage = Imaging.CloneBitmap((Bitmap)image, PixelFormat.Format32bppPArgb, ref exception);
                        if (sourceImage != null)
                        {
                            sourceImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                            flag = true;
                        }
                        if (!flag && sourceImage != null)
                        {
                            sourceImage.Dispose();
                        }
                        GC.Collect();
                        return flag;
                    }
                }
            }
            catch (Exception exception1)
            {
                exception = exception1;
            }
            if (!flag && sourceImage != null)
            {
                sourceImage.Dispose();
            }
            GC.Collect();
            return flag;
        }

        private bool LoadImageFromStream(string fileName, ref Exception exception)
        {
            return this.LoadImageFromStream(fileName, ref this.m_OriginalImage, ref exception, ref this.m_OriginalDpiX, ref this.m_OriginalDpiY);
        }

        private bool LoadZoomedImage(int index)
        {
            index = (index < 0 || index > 1 ? 0 : index);
            if (this.m_ZoomedImage[index] != null)
            {
                this.m_ZoomedImage[index].Dispose();
                this.m_ZoomedImage[index] = null;
                GC.Collect();
            }
            switch (index)
            {
                case 0:
                    {
                        this.m_ZoomedImage[0] = new Bitmap((int)((double)this.m_OriginalImage.Width * 0.5), (int)((double)this.m_OriginalImage.Height * 0.5), PixelFormat.Format32bppPArgb);
                        return this.CreateZoomedImage(this.m_OriginalImage, ref this.m_ZoomedImage[0], InterpolationMode.HighQualityBicubic);
                    }
                case 1:
                    {
                        this.m_ZoomedImage[1] = new Bitmap((int)((double)this.m_OriginalImage.Width * 0.25), (int)((double)this.m_OriginalImage.Height * 0.25), PixelFormat.Format32bppPArgb);
                        return this.CreateZoomedImage(this.m_OriginalImage, ref this.m_ZoomedImage[1], InterpolationMode.HighQualityBicubic);
                    }
            }
            return false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            Graphics graphics = e.Graphics;
            this.DrawImage(ref graphics);
            base.OnPaint(e);
        }

        public bool OpenImageFile(string fileName, PointF origin, int zoom, ref Exception exception)
        {
            this.InitialiseValues();
            if (!this.LoadImageFromStream(fileName, ref exception))
            {
                this.DisposeValues(false);
                return false;
            }
            this.Brightness = 0;
            this.Contrast = 0;
            this.CheckIfCanUseAdaptiveCache();
            this.SetZoom(zoom);
            this.SetOrigin(new PointF(origin.X, origin.Y));
            return true;
        }

        public void ReloadAdaptiveZoomedImage()
        {
            if (!this.m_CanUseAdaptiveCache)
            {
                return;
            }
            this.m_LastAdaptiveZoomValue = 0;
            this.LoadAdaptiveZoomedImage(this.Zoom);
            base.Invalidate();
        }

        public void ReloadImage(bool reloadZoomedImages)
        {
            this.ReloadImage(reloadZoomedImages, true);
        }

        private void ReloadImage(bool reloadZoomedImages, bool disposeValues)
        {
            if (disposeValues)
            {
                this.DisposeValues(true);
            }
            if (this.m_OriginalImage == null)
            {
                return;
            }
            this.CheckIfCanUseAdaptiveCache();
            this.CheckBounds();
            if (reloadZoomedImages)
            {
                this.ReloadZoomedImages();
            }
            this.m_MustUpdateVisibleImage = true;
        }

        public bool ReloadImage(string fileName, bool reloadZoomedImages, ref Exception exception)
        {
            this.DisposeValues(false);
            if (!this.LoadImageFromStream(fileName, ref exception))
            {
                return false;
            }
            this.ReloadImage(reloadZoomedImages, false);
            return true;
        }

        public void ReloadZoomedImages()
        {
            this.LoadZoomedImage(0);
            this.LoadZoomedImage(1);
            this.m_LastAdaptiveZoomValue = 0;
            if (this.m_CanUseAdaptiveCache)
            {
                this.LoadAdaptiveZoomedImage(this.Zoom);
            }
        }

        public static bool ResizeImage(Image srcImage, ref Bitmap destImage, int maxWidth, int maxHeight, PixelFormat pixelFormat, InterpolationMode interpolationMode, ref Exception exception)
        {
            bool flag;
            int width = srcImage.Width;
            int height = srcImage.Height;
            if (maxWidth != -1 && maxHeight != -1)
            {
                if (width > maxWidth)
                {
                    height = height * maxWidth / width;
                    width = maxWidth;
                }
                if (height > maxHeight)
                {
                    width = width * maxHeight / height;
                    height = maxHeight;
                }
            }
            try
            {
                destImage = (pixelFormat == PixelFormat.Undefined ? new Bitmap(width, height) : new Bitmap(width, height, pixelFormat));
                using (Graphics graphic = Graphics.FromImage(destImage))
                {
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    graphic.InterpolationMode = interpolationMode;
                    graphic.DrawImage(srcImage, 0, 0, destImage.Width, destImage.Height);
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                exception = exception1;
                if (destImage != null)
                {
                    destImage.Dispose();
                    GC.Collect();
                }
                return false;
            }
            return flag;
        }

        private static void ResumeDrawing(Control parent)
        {
            DrawingBoard.SendMessage(parent.Handle, 11, true, 0);
            parent.Invalidate();
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        private void SetBrightness(Bitmap image, int brightness)
        {
            Imaging.ApplyBrightness(image, (double)brightness);
        }

        private void SetContrast(Bitmap image, int contrast)
        {
            Imaging.ApplyContrast(image, (double)contrast);
        }

        public void SetImageQuality(InterpolationModes.ZoomedImagesEnum index, InterpolationMode interpolationMode)
        {
            this.m_InterpolationModes[index] = interpolationMode;
            if (this.m_VisibleImage != null)
            {
                this.m_VisibleImage.Dispose();
                this.m_VisibleImage = null;
            }
            this.CheckBounds();
            this.m_MustUpdateVisibleImage = true;
            base.Invalidate();
        }

        private void SetOrigin(PointF origin)
        {
            this.m_Origin = origin;
            this.CheckBounds();
            this.m_MustUpdateVisibleImage = true;
            if (this.OnOriginChange != null)
            {
                this.OnOriginChange(this.m_Origin);
            }
        }

        private void SetZoom(int zoom)
        {
            if (this.m_ZoomRestricted)
            {
                return;
            }
            if (zoom == -1)
            {
                zoom = this.FitToScreenZoom;
            }
            this.ApplyZoom(zoom);
            this.InitializeIdleTimer();
        }

        private static void SuspendDrawing(Control parent)
        {
            DrawingBoard.SendMessage(parent.Handle, 11, false, 0);
        }

        public Image ZoomedImage(int index)
        {
            if (this.m_ZoomedImage[index] == null)
            {
                this.LoadZoomedImage(index);
            }
            return this.m_ZoomedImage[index];
        }

        private void ZoomImage(bool zoomIn, int minZoom = -1)
        {
            int mZoomInterval = this.m_ZoomInterval;
            if (zoomIn)
            {
                DrawingBoard zoom = this;
                zoom.Zoom = zoom.Zoom + mZoomInterval;
                return;
            }
            if (this.Zoom - mZoomInterval < minZoom && minZoom != -1)
            {
                this.Zoom = minZoom;
                return;
            }
            DrawingBoard drawingBoard = this;
            drawingBoard.Zoom = drawingBoard.Zoom - mZoomInterval;
        }

        public void ZoomIn()
        {
            this.ZoomImage(true, -1);
        }

        public void ZoomIn(PointF point)
        {
            this.m_ZoomToPointerLocation = new PointF(point.X, point.Y);
            this.ZoomImage(true, -1);
            this.m_ZoomToPointerLocation = Point.Empty;
        }

        public void ZoomLock(int zoom)
        {
            this.ApplyZoom(zoom);
            this.ZoomRestricted = true;
            base.Invalidate();
        }

        public void ZoomOut()
        {
            this.ZoomImage(false, -1);
        }

        public void ZoomOut(PointF point)
        {
            this.m_ZoomToPointerLocation = new PointF(point.X, point.Y);
            this.ZoomImage(false, this.FitToScreenZoom);
            this.m_ZoomToPointerLocation = Point.Empty;
        }

        public void ZoomSelection(Rectangle selectedRectangle)
        {
            if (this.m_OriginalImage != null)
            {
                PointF origin = this.Origin;
                int x = (int)(origin.X + (float)selectedRectangle.X);
                PointF pointF = this.Origin;
                Point point = new Point(x, (int)(pointF.Y + (float)selectedRectangle.Y));
                Size canvasSize = this.CanvasSize;
                float width = (float)canvasSize.Width / (float)selectedRectangle.Width;
                Size size = this.CanvasSize;
                float single = Math.Min(width, (float)size.Height / (float)selectedRectangle.Height);
                this.Zoom = (int)(single * 100f);
                this.Origin = new Point(point.X, point.Y);
            }
        }

        public event ZoomChangeEventHandler OnCacheLoaded;

        public event ZoomChangeEventHandler OnMouseWheel;

        public event OriginChangeEventHandler OnOriginChange;

        public event ZoomChangeEventHandler OnZoomChange;

        public event EventHandler SetScrollPositions;
    }
}