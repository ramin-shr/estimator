using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using QuoterPlanImaging;

namespace QuoterPlanControls
{
	[ComVisible(false)]
	public class DrawingBoard : UserControl
	{
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

		public event EventHandler SetScrollPositions
		{
			add
			{
				EventHandler eventHandler = this.SetScrollPositions;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.SetScrollPositions, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.SetScrollPositions;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.SetScrollPositions, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
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

		public DrawingBoard()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.MouseWheel += this.DrawingBoard_MouseWheel;
			base.Location = Point.Empty;
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
				this.m_idleMonitor = new IdleMonitor(650, 25);
				this.m_idleMonitor.IdleStateChanged += this.idleMonitor_IdleStateChanged;
			}
		}

		private void DisposeIdleTimer()
		{
			if (this.m_idleMonitor != null)
			{
				this.m_idleMonitor.IdleStateChanged -= this.idleMonitor_IdleStateChanged;
				this.m_idleMonitor.Dispose();
				this.m_idleMonitor = null;
			}
		}

		public Image Image
		{
			get
			{
				return this.m_OriginalImage;
			}
		}

		public Image ZoomedImage(int index)
		{
			if (this.m_ZoomedImage[index] == null)
			{
				this.LoadZoomedImage(index);
			}
			return this.m_ZoomedImage[index];
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
							if (pixelFormat != PixelFormat.Format32bppRgb)
							{
								if (pixelFormat != PixelFormat.Format8bppIndexed)
								{
									goto IL_80;
								}
								sourceImage = Imaging.GrayscaleToColor((Bitmap)image, ref exception);
								goto IL_A5;
							}
						}
						else if (pixelFormat != PixelFormat.Format32bppPArgb && pixelFormat != PixelFormat.Format32bppArgb)
						{
							goto IL_80;
						}
						sourceImage = Imaging.CloneBitmap((Bitmap)image, PixelFormat.Format32bppPArgb, ref exception);
						goto IL_A5;
						IL_80:
						sourceImage = ((Bitmap)image).Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format32bppPArgb);
						IL_A5:
						if (sourceImage != null)
						{
							sourceImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
							flag = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				exception = ex;
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

		public static bool ResizeImage(Image srcImage, ref Bitmap destImage, int maxWidth, int maxHeight, PixelFormat pixelFormat, InterpolationMode interpolationMode, ref Exception exception)
		{
			int num = srcImage.Width;
			int num2 = srcImage.Height;
			if (maxWidth != -1 && maxHeight != -1)
			{
				if (num > maxWidth)
				{
					num2 = num2 * maxWidth / num;
					num = maxWidth;
				}
				if (num2 > maxHeight)
				{
					num = num * maxHeight / num2;
					num2 = maxHeight;
				}
			}
			try
			{
				destImage = ((pixelFormat == PixelFormat.Undefined) ? new Bitmap(num, num2) : new Bitmap(num, num2, pixelFormat));
				using (Graphics graphics = Graphics.FromImage(destImage))
				{
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.InterpolationMode = interpolationMode;
					graphics.DrawImage(srcImage, 0, 0, destImage.Width, destImage.Height);
				}
				return true;
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			if (destImage != null)
			{
				destImage.Dispose();
				GC.Collect();
			}
			return false;
		}

		private bool CreateZoomedImage(Bitmap originalImage, ref Bitmap zoomedImage, InterpolationMode interpolationMode)
		{
			try
			{
				using (Graphics graphics = Graphics.FromImage(zoomedImage))
				{
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.InterpolationMode = interpolationMode;
					graphics.DrawImage(originalImage, new Rectangle(new Point(0, 0), zoomedImage.Size), new Rectangle(new Point(0, 0), originalImage.Size), GraphicsUnit.Pixel);
				}
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("The image could not be zoomed:");
				Console.WriteLine(ex.Message);
			}
			return false;
		}

		private void CheckIfCanUseAdaptiveCache()
		{
			try
			{
				this.m_CanUseAdaptiveCache = (this.Image.Width * this.Image.Height <= 36000000);
			}
			catch
			{
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
			if (this.LoadImageFromStream(fileName, ref exception))
			{
				this.ReloadImage(reloadZoomedImages, false);
				return true;
			}
			return false;
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
			this.m_ZoomedImage[2] = new Bitmap((int)((float)this.m_OriginalImage.Width * ((float)zoom / 100f)), (int)((float)this.m_OriginalImage.Height * ((float)zoom / 100f)), PixelFormat.Format32bppPArgb);
			bool flag;
			if (this.ZoomFactor > 0.5)
			{
				flag = this.CreateZoomedImage(this.m_OriginalImage, ref this.m_ZoomedImage[2], this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.MoreThan50Percent]);
			}
			else if (this.ZoomFactor > 0.25 && this.ZoomFactor <= 0.5)
			{
				flag = this.CreateZoomedImage(this.m_ZoomedImage[0], ref this.m_ZoomedImage[2], this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Between25and50Percents]);
			}
			else
			{
				flag = this.CreateZoomedImage(this.m_ZoomedImage[1], ref this.m_ZoomedImage[2], this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Below25Percent]);
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

		private bool LoadZoomedImage(int index)
		{
			index = ((index < 0 || index > 1) ? 0 : index);
			if (this.m_ZoomedImage[index] != null)
			{
				this.m_ZoomedImage[index].Dispose();
				this.m_ZoomedImage[index] = null;
				GC.Collect();
			}
			switch (index)
			{
			case 0:
				this.m_ZoomedImage[0] = new Bitmap((int)((double)this.m_OriginalImage.Width * 0.5), (int)((double)this.m_OriginalImage.Height * 0.5), PixelFormat.Format32bppPArgb);
				return this.CreateZoomedImage(this.m_OriginalImage, ref this.m_ZoomedImage[0], InterpolationMode.HighQualityBicubic);
			case 1:
				this.m_ZoomedImage[1] = new Bitmap((int)((double)this.m_OriginalImage.Width * 0.25), (int)((double)this.m_OriginalImage.Height * 0.25), PixelFormat.Format32bppPArgb);
				return this.CreateZoomedImage(this.m_OriginalImage, ref this.m_ZoomedImage[1], InterpolationMode.HighQualityBicubic);
			default:
				return false;
			}
		}

		private void DrawImage(ref Graphics g)
		{
			if (this.m_OriginalImage == null)
			{
				g.Clear(this.BackColor);
				return;
			}
			RectangleF destRect = new Rectangle(this.HorizontalOffset, this.VerticalOffset, base.ClientSize.Width, base.ClientSize.Height);
			g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
			g.SmoothingMode = SmoothingMode.HighSpeed;
			g.InterpolationMode = InterpolationMode.NearestNeighbor;
			if (this.ZoomFactor == 1.0 && !this.m_useDynamicAdjustments)
			{
				RectangleF srcRect = new RectangleF((float)((int)((double)this.m_Origin.X * this.ZoomFactor)), (float)((int)((double)this.m_Origin.Y * this.ZoomFactor)), (float)base.ClientSize.Width, (float)base.ClientSize.Height);
				g.DrawImage(this.m_OriginalImage, destRect, srcRect, GraphicsUnit.Pixel);
			}
			else if ((this.ZoomFactor == 0.5 || this.ZoomFactor == 0.25 || (this.ZoomFactor < 1.0 && this.m_ZoomValue == this.m_LastAdaptiveZoomValue && !this.m_LastAdaptiveOrigin.Equals(this.m_Origin))) && !this.m_useDynamicAdjustments)
			{
				if (this.ZoomFactor == 0.5 || this.ZoomFactor == 0.25)
				{
					if (this.ZoomedImage((this.ZoomFactor == 0.5) ? 0 : 1) == null)
					{
						g.Clear(this.BackColor);
						return;
					}
					RectangleF srcRect = new RectangleF((float)((int)((double)this.m_Origin.X * this.ZoomFactor)), (float)((int)((double)this.m_Origin.Y * this.ZoomFactor)), (float)base.ClientSize.Width, (float)base.ClientSize.Height);
					g.DrawImage(this.ZoomedImage((this.ZoomFactor == 0.5) ? 0 : 1), destRect, srcRect, GraphicsUnit.Pixel);
				}
				else
				{
					if (this.ZoomedImage(2) == null)
					{
						g.Clear(this.BackColor);
						return;
					}
					RectangleF srcRect = new RectangleF((float)((int)((double)this.m_Origin.X * this.ZoomFactor)), (float)((int)((double)this.m_Origin.Y * this.ZoomFactor)), (float)base.ClientSize.Width, (float)base.ClientSize.Height);
					g.DrawImage(this.ZoomedImage(2), destRect, srcRect, GraphicsUnit.Pixel);
				}
			}
			else
			{
				if (this.m_MustUpdateVisibleImage || this.m_VisibleImage == null)
				{
					if (this.m_VisibleImage != null)
					{
						this.m_VisibleImage.Dispose();
						this.m_VisibleImage = null;
					}
					this.m_VisibleImage = new Bitmap(base.ClientSize.Width, base.ClientSize.Height, PixelFormat.Format32bppPArgb);
					using (Graphics graphics = Graphics.FromImage(this.m_VisibleImage))
					{
						graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
						graphics.SmoothingMode = SmoothingMode.HighQuality;
						if (this.ZoomFactor > 0.5)
						{
							graphics.InterpolationMode = this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.MoreThan50Percent];
							RectangleF srcRect = new RectangleF(this.m_Origin.X, this.m_Origin.Y, (float)((double)base.ClientSize.Width / this.ZoomFactor), (float)((double)base.ClientSize.Height / this.ZoomFactor));
							graphics.DrawImage(this.m_OriginalImage, destRect, srcRect, GraphicsUnit.Pixel);
						}
						else
						{
							int index;
							double num;
							if (this.ZoomFactor > 0.25 && this.ZoomFactor <= 0.5)
							{
								graphics.InterpolationMode = this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Between25and50Percents];
								index = 0;
								num = 0.5;
							}
							else
							{
								graphics.InterpolationMode = this.m_InterpolationModes[InterpolationModes.ZoomedImagesEnum.Below25Percent];
								index = 1;
								num = 0.25;
							}
							if (this.ZoomedImage(index) == null)
							{
								g.Clear(this.BackColor);
								return;
							}
							RectangleF srcRect = new RectangleF((float)((int)((double)this.m_Origin.X * num)), (float)((int)((double)this.m_Origin.Y * num)), (float)base.ClientSize.Width / (float)(this.ZoomFactor / num), (float)base.ClientSize.Height / (float)(this.ZoomFactor / num));
							graphics.DrawImage(this.ZoomedImage(index), destRect, srcRect, GraphicsUnit.Pixel);
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
				int zoomValue = this.m_ZoomValue;
				int lastAdaptiveZoomValue = this.m_LastAdaptiveZoomValue;
				g.DrawImageUnscaled(this.m_VisibleImage, Point.Empty);
			}
			this.SetScrollPositions(this, new EventArgs());
			this.m_MustUpdateVisibleImage = false;
		}

		private static void SuspendDrawing(Control parent)
		{
			DrawingBoard.SendMessage(parent.Handle, 11, false, 0);
		}

		private static void ResumeDrawing(Control parent)
		{
			DrawingBoard.SendMessage(parent.Handle, 11, true, 0);
			parent.Invalidate();
		}

		public void ComputeDrawingArea()
		{
			DrawingBoard.SuspendDrawing(this);
			if (this.m_OriginalImage != null)
			{
				int num = (int)((double)this.m_OriginalImage.Width * this.ZoomFactor);
				int num2 = (int)((double)this.m_OriginalImage.Height * this.ZoomFactor);
				this.HorizontalOffset = ((num < this.CanvasSize.Width) ? ((this.CanvasSize.Width - num) / 2) : 0);
				this.VerticalOffset = ((num2 < this.CanvasSize.Height) ? ((this.CanvasSize.Height - num2) / 2) : 0);
			}
			DrawingBoard.ResumeDrawing(this);
			this.m_MustUpdateVisibleImage = true;
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
				this.m_Origin.X = (float)((double)this.m_OriginalImage.Width - (double)base.ClientSize.Width / this.ZoomFactor);
			}
			if ((double)this.m_Origin.Y > (double)this.m_OriginalImage.Height - (double)base.ClientSize.Height / this.ZoomFactor)
			{
				this.m_Origin.Y = (float)((double)this.m_OriginalImage.Height - (double)base.ClientSize.Height / this.ZoomFactor);
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

		public double ZoomFactor
		{
			get
			{
				return (double)this.m_ZoomValue / 100.0;
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

		public void ZoomLock(int zoom)
		{
			this.ApplyZoom(zoom);
			this.ZoomRestricted = true;
			base.Invalidate();
		}

		private void ApplyZoom(int zoom)
		{
			PointF pointF = Point.Empty;
			if (this.m_ZoomToPointerLocation == Point.Empty)
			{
				pointF = new PointF(this.m_Origin.X + (float)((int)((double)base.ClientSize.Width / this.ZoomFactor / 2.0)), this.m_Origin.Y + (float)((int)((double)base.ClientSize.Height / this.ZoomFactor / 2.0)));
			}
			else
			{
				PointF pointF2 = new PointF((float)((int)((double)this.m_ZoomToPointerLocation.X / this.ZoomFactor)), (float)((int)((double)this.m_ZoomToPointerLocation.Y / this.ZoomFactor)));
				PointF pointF3 = new PointF((float)((int)((double)this.m_ZoomToPointerLocation.X / ((double)zoom / 100.0))), (float)((int)((double)this.m_ZoomToPointerLocation.Y / ((double)zoom / 100.0))));
				this.m_ZoomToPointerLocation = new PointF(pointF2.X - pointF3.X - (float)((int)((double)this.HorizontalOffset / this.ZoomFactor)), pointF2.Y - pointF3.Y - (float)((int)((double)this.VerticalOffset / this.ZoomFactor)));
			}
			this.m_ZoomValue = zoom;
			if (this.m_ZoomValue > 300)
			{
				this.m_ZoomValue = 300;
			}
			if (this.m_ZoomValue < 10)
			{
				this.m_ZoomValue = 10;
			}
			this.m_LastAdaptiveZoomValue = 0;
			this.ComputeDrawingArea();
			if (this.m_ZoomToPointerLocation == Point.Empty)
			{
				this.m_Origin = new PointF(pointF.X - (float)((int)((double)base.ClientSize.Width / this.ZoomFactor / 2.0)), pointF.Y - (float)((int)((double)base.ClientSize.Height / this.ZoomFactor / 2.0)));
			}
			else if (this.m_ZoomValue >= 10 && this.m_ZoomValue < 300)
			{
				this.m_Origin = new PointF(this.m_Origin.X + this.m_ZoomToPointerLocation.X, this.m_Origin.Y + this.m_ZoomToPointerLocation.Y);
			}
			this.CheckBounds();
			if (this.OnZoomChange != null)
			{
				this.OnZoomChange(this.m_ZoomValue);
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

		private void ZoomImage(bool zoomIn, int minZoom = -1)
		{
			int zoomInterval = this.m_ZoomInterval;
			if (zoomIn)
			{
				this.Zoom += zoomInterval;
				return;
			}
			if (this.Zoom - zoomInterval < minZoom && minZoom != -1)
			{
				this.Zoom = minZoom;
				return;
			}
			this.Zoom -= zoomInterval;
		}

		public int FitToScreenZoom
		{
			get
			{
				float num = Math.Min((float)this.CanvasSize.Width / (float)this.m_OriginalImage.Width, (float)this.CanvasSize.Height / (float)this.m_OriginalImage.Height);
				return (int)(num * 100f);
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

		public void FitHorizontally()
		{
			this.Origin = new Point(0, 0);
			if (this.m_OriginalImage != null)
			{
				float num = (float)this.CanvasSize.Width / (float)this.m_OriginalImage.Width;
				this.Zoom = (int)(num * 100f);
			}
		}

		public void FitVertically()
		{
			this.Origin = new Point(0, 0);
			if (this.m_OriginalImage != null)
			{
				float num = (float)this.CanvasSize.Height / (float)this.m_OriginalImage.Height;
				this.Zoom = (int)(num * 100f);
			}
		}

		public void ZoomSelection(Rectangle selectedRectangle)
		{
			if (this.m_OriginalImage != null)
			{
				Point point = new Point((int)(this.Origin.X + (float)selectedRectangle.X), (int)(this.Origin.Y + (float)selectedRectangle.Y));
				float num = Math.Min((float)this.CanvasSize.Width / (float)selectedRectangle.Width, (float)this.CanvasSize.Height / (float)selectedRectangle.Height);
				this.Zoom = (int)(num * 100f);
				this.Origin = new Point(point.X, point.Y);
			}
		}

		public void CenterToLocation(Point location)
		{
			this.ZoomImage(true, -1);
		}

		public PointF TempOffset
		{
			set
			{
				this.m_Origin = value;
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

		public Size CanvasSize
		{
			get
			{
				return new Size(base.ClientSize.Width, base.ClientSize.Height);
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

		public bool CacheInUsed
		{
			get
			{
				return this.m_OriginalImage != null && (this.m_ZoomValue == this.m_LastAdaptiveZoomValue || this.m_ZoomValue == 25 || this.m_ZoomValue == 50 || this.m_ZoomValue == 100) && !this.m_useDynamicAdjustments;
			}
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

		public InterpolationModes ImageQuality
		{
			get
			{
				return this.m_InterpolationModes;
			}
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

		public int Brightness
		{
			get
			{
				return this.m_brightness;
			}
			set
			{
				this.m_brightness = ((value < -255) ? -255 : ((value > 255) ? 255 : value));
				if (this.m_OriginalImage != null && this.m_useDynamicAdjustments)
				{
					this.m_MustUpdateVisibleImage = true;
					base.Invalidate();
				}
			}
		}

		private void SetBrightness(Bitmap image, int brightness)
		{
			Imaging.ApplyBrightness(image, (double)brightness);
		}

		public int Contrast
		{
			get
			{
				return this.m_contrast;
			}
			set
			{
				this.m_contrast = ((value < -100) ? -100 : ((value > 100) ? 100 : value));
				if (this.m_OriginalImage != null && this.m_useDynamicAdjustments)
				{
					this.m_MustUpdateVisibleImage = true;
					base.Invalidate();
				}
			}
		}

		private void SetContrast(Bitmap image, int contrast)
		{
			Imaging.ApplyContrast(image, (double)contrast);
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

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(this.BackColor);
			Graphics graphics = e.Graphics;
			this.DrawImage(ref graphics);
			base.OnPaint(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
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

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CausesValidation = false;
			base.Margin = new Padding(0);
			base.Name = "DrawingBoard";
			base.Size = new Size(233, 247);
			base.Load += this.DrawingBoard_Load;
			base.ResumeLayout(false);
		}

		private const int WM_SETREDRAW = 11;

		private EventHandler SetScrollPositions;

		private ZoomChangeEventHandler OnZoomChange;

		private ZoomChangeEventHandler OnCacheLoaded;

		private new ZoomChangeEventHandler OnMouseWheel;

		private OriginChangeEventHandler OnOriginChange;

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
	}
}
