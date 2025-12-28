using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlanControls
{
	public class PanControl : UserControl
	{
		public event PanningEventHandler Panning
		{
			add
			{
				PanningEventHandler panningEventHandler = this.Panning;
				PanningEventHandler panningEventHandler2;
				do
				{
					panningEventHandler2 = panningEventHandler;
					PanningEventHandler value2 = (PanningEventHandler)Delegate.Combine(panningEventHandler2, value);
					panningEventHandler = Interlocked.CompareExchange<PanningEventHandler>(ref this.Panning, value2, panningEventHandler2);
				}
				while (panningEventHandler != panningEventHandler2);
			}
			remove
			{
				PanningEventHandler panningEventHandler = this.Panning;
				PanningEventHandler panningEventHandler2;
				do
				{
					panningEventHandler2 = panningEventHandler;
					PanningEventHandler value2 = (PanningEventHandler)Delegate.Remove(panningEventHandler2, value);
					panningEventHandler = Interlocked.CompareExchange<PanningEventHandler>(ref this.Panning, value2, panningEventHandler2);
				}
				while (panningEventHandler != panningEventHandler2);
			}
		}

		public PanControl()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
		}

		public void Initialize(DrawingBoard drawingBoard)
		{
			this.DisposeImage();
			this.drawingBoard = drawingBoard;
			base.MouseWheel += drawingBoard.DrawingBoard_MouseWheel;
			this.ComputeDrawingArea();
		}

		public PointF Offset
		{
			get
			{
				return this.offset;
			}
		}

		public float ZoomFactor
		{
			get
			{
				return this.zoomFactor;
			}
		}

		public Rectangle PanRectangle
		{
			get
			{
				return this.panRectangle;
			}
			set
			{
				this.panRectangle = value;
			}
		}

		public bool PanFromScrolling
		{
			get
			{
				return this.panFromScrolling;
			}
			set
			{
				this.panFromScrolling = value;
			}
		}

		public new bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		private void DrawImage(ref Graphics g)
		{
			if (this.m_OriginalImage == null)
			{
				return;
			}
			this.offset = new Point((base.ClientSize.Width - this.m_OriginalImage.Width) / 2, (base.ClientSize.Height - this.m_OriginalImage.Height) / 2);
			g.DrawImageUnscaled(this.m_OriginalImage, (int)this.offset.X, (int)this.offset.Y);
		}

		private void DisposeImage()
		{
			if (this.m_OriginalImage != null)
			{
				this.m_OriginalImage.Dispose();
				this.m_OriginalImage = null;
			}
		}

		public void Clear()
		{
			this.DisposeImage();
			this.Refresh();
		}

		public void Reload()
		{
			this.DisposeImage();
			this.ComputeDrawingArea();
			base.Invalidate();
		}

		public void DrawPanRectangle(Graphics g)
		{
			if (this.m_OriginalImage == null)
			{
				return;
			}
			this.panRectangle.Width = ((this.panRectangle.Width > this.m_OriginalImage.Width) ? this.m_OriginalImage.Width : this.panRectangle.Width);
			this.panRectangle.Height = ((this.panRectangle.Height > this.m_OriginalImage.Height) ? this.m_OriginalImage.Height : this.panRectangle.Height);
			g.FillRectangle(new SolidBrush(Color.FromArgb(150, 255, 0, 0)), new Rectangle(this.panRectangle.X + (int)this.offset.X, this.panRectangle.Y + (int)this.offset.Y, this.panRectangle.Width, this.panRectangle.Height));
		}

		private void ComputeDrawingArea()
		{
			if (this.drawingBoard == null || this.drawingBoard.Image == null)
			{
				return;
			}
			Image image = this.drawingBoard.Image;
			Exception ex = null;
			DrawingBoard.ResizeImage(image, ref this.m_OriginalImage, base.ClientSize.Width, base.ClientSize.Height, PixelFormat.Format24bppRgb, InterpolationMode.HighQualityBicubic, ref ex);
			this.zoomFactor = (float)this.m_OriginalImage.Width / (float)image.Width;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(this.BackColor);
			if (this.enabled)
			{
				Graphics graphics = e.Graphics;
				this.DrawImage(ref graphics);
				base.OnPaint(e);
			}
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			if (this.m_OriginalImage == null)
			{
				return;
			}
			this.ComputeDrawingArea();
			base.OnSizeChanged(e);
		}

		private void PanControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.m_OriginalImage == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				this.Cursor = Cursors.Hand;
				if (!this.panRectangle.Contains(e.Location))
				{
					this.panRectangle.Location = new Point(e.Location.X - this.panRectangle.Width / 2 - (int)this.offset.X, e.Location.Y - this.panRectangle.Height / 2 - (int)this.offset.Y);
					if (this.Panning != null)
					{
						this.Panning(this.panRectangle.Location);
					}
				}
				this.origPanPoint = this.panRectangle.Location;
				this.origMousePoint = e.Location;
				this.panning = true;
			}
		}

		private void PanControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.m_OriginalImage == null)
			{
				return;
			}
			if (!this.panning)
			{
				this.Cursor = (this.panRectangle.Contains(e.Location) ? Cursors.Hand : Cursors.Default);
				return;
			}
			if (e.Button == MouseButtons.Left && this.panning)
			{
				this.panRectangle.Location = new Point(this.origPanPoint.X + (e.X - this.origMousePoint.X), this.origPanPoint.Y + (e.Y - this.origMousePoint.Y));
				if (this.Panning != null)
				{
					this.Panning(this.panRectangle.Location);
				}
			}
		}

		private void PanControl_MouseUp(object sender, MouseEventArgs e)
		{
			this.panning = false;
		}

		private void PanControl_Load(object sender, EventArgs e)
		{
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
			this.BackColor = SystemColors.Window;
			base.Name = "PanControl";
			base.Load += this.PanControl_Load;
			base.MouseDown += this.PanControl_MouseDown;
			base.MouseMove += this.PanControl_MouseMove;
			base.MouseUp += this.PanControl_MouseUp;
			base.ResumeLayout(false);
		}

		private PanningEventHandler Panning;

		private Point origPanPoint;

		private Point origMousePoint;

		private bool panning;

		private bool enabled = true;

		private bool panFromScrolling;

		private Rectangle panRectangle = new Rectangle(0, 0, 1, 1);

		private Bitmap m_OriginalImage;

		private DrawingBoard drawingBoard;

		private PointF offset;

		private float zoomFactor = -1f;

		private IContainer components;
	}
}
