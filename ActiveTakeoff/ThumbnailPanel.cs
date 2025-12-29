using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class ThumbnailPanel : Button
    {
        private bool selected;

        private bool displayName;

        private bool displayShadow;

        private bool displayLayers;

        private Rectangle boundingRectangle;

        private Size thumbnailSize;

        private Padding thumbnailMarging;

        private Padding thumbnailPadding;

        private Plan plan;

        private DrawingArea drawArea;

        private Control container;

        private int dragRadius = 40;

        private int dragPosX;

        private int dragPosY;

        private bool isDragging;

        private IContainer components;

        public bool AllowDrag
        {
            get;
            set;
        }

        public bool DisplayLayers
        {
            get
            {
                return this.displayLayers;
            }
            set
            {
                this.displayLayers = value;
            }
        }

        public bool DisplayName
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

        public bool DisplayShadow
        {
            get
            {
                return this.displayShadow;
            }
            set
            {
                this.displayShadow = value;
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

        private bool IsDragging
        {
            get
            {
                return this.isDragging;
            }
            set
            {
                this.isDragging = value;
            }
        }

        public Plan Plan
        {
            get
            {
                return this.plan;
            }
            set
            {
                this.plan = value;
                base.Invalidate();
            }
        }

        public bool Selected
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

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        public Padding ThumbnailMarging
        {
            get
            {
                return this.thumbnailMarging;
            }
            set
            {
                this.thumbnailMarging = value;
            }
        }

        public Padding ThumbnailPadding
        {
            get
            {
                return this.thumbnailPadding;
            }
            set
            {
                this.thumbnailPadding = value;
            }
        }

        public Size ThumbnailSize
        {
            get
            {
                return this.thumbnailSize;
            }
            set
            {
                this.thumbnailSize = value;
            }
        }

        public ThumbnailPanel()
        {
            this.Initialize(null);
        }

        public ThumbnailPanel(Control container)
        {
            this.Initialize(container);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawImage(Graphics g, Rectangle rectangle)
        {
            g.DrawImage(this.plan.Thumbnail.ThumbImage, rectangle.X, rectangle.Y);
        }

        private void DrawLayers(Graphics g, Rectangle rectangle)
        {
            if (this.drawArea != null && this.plan.ImageWidth > 0)
            {
                float width = (float)this.plan.Thumbnail.ThumbImage.Width / (float)this.plan.ImageWidth;
                if (width > 0f)
                {
                    this.drawArea.DrawLayers(this, this.plan, g, base.ClientSize, -(int)((float)rectangle.X / width), -(int)((float)rectangle.Y / width), width, false, true, MainForm.ImageQualityEnum.QualityHigh, 12f);
                }
            }
        }

        private void DrawMissingImage(Graphics g, Rectangle rectangle)
        {
            g.DrawLine(new Pen(Color.DarkRed, 1f), new Point(rectangle.X, rectangle.Y), new Point(rectangle.Width, rectangle.Y + rectangle.Height));
            g.DrawLine(new Pen(Color.DarkRed, 1f), new Point(rectangle.X + rectangle.Width, rectangle.Y), new Point(rectangle.X, rectangle.Y + rectangle.Height));
        }

        private void DrawOutline(Graphics g, Rectangle rectangle)
        {
            g.DrawRectangle(new Pen(Color.Gray), rectangle);
        }

        private void DrawPlanName(Graphics g, Rectangle rectangle)
        {
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.Trimming = StringTrimming.EllipsisPath;
                stringFormat.LineAlignment = StringAlignment.Near;
                g.DrawString(this.plan.Name, Utilities.GetDefaultFont(), Brushes.Black, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height + 6, rectangle.Width, 30), stringFormat);
            }
        }

        private void DrawSelection(Graphics g, Rectangle rectangle)
        {
            g.DrawRectangle(new Pen(Color.White, 1f), rectangle);
            Pen pen = new Pen(Color.FromArgb(200, Color.FromKnownColor(KnownColor.Highlight)), 2f);
            Brush solidBrush = new SolidBrush(Color.FromArgb(80, Color.FromKnownColor(KnownColor.Highlight)));
            g.DrawRectangle(pen, rectangle.X - 2, rectangle.Y - 2, rectangle.Width + 4, rectangle.Height + 4);
            g.FillRectangle(solidBrush, rectangle.X - 2, rectangle.Y - 2, rectangle.Width + 4, rectangle.Height + 4);
            pen.Dispose();
            solidBrush.Dispose();
        }

        private void DrawShadow(Graphics g, Rectangle rectangle)
        {
            for (int i = 0; i < 3; i++)
            {
                g.DrawLine(new Pen(Color.DarkGray), new Point(rectangle.X + 3, rectangle.Y + rectangle.Height + 1 + i), new Point(rectangle.X + rectangle.Width + 3, rectangle.Y + rectangle.Height + 1 + i));
                g.DrawLine(new Pen(Color.DarkGray), new Point(rectangle.X + rectangle.Width + 1 + i, rectangle.Y + 3), new Point(rectangle.X + rectangle.Width + 1 + i, rectangle.Y + rectangle.Height + 3));
            }
        }

        public void EnableDoubleClick(bool enable)
        {
            base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, enable);
        }

        private void Initialize(Control container)
        {
            this.container = container;
            this.Anchor = AnchorStyles.None;
            this.BackColor = Color.Transparent;
            base.Margin = new Padding(0, 0, 0, 0);
            base.FlatAppearance.BorderSize = 0;
            base.FlatStyle = FlatStyle.Flat;
            base.FlatAppearance.MouseDownBackColor = Color.Transparent;
            base.FlatAppearance.MouseOverBackColor = Color.Transparent;
            base.FlatAppearance.CheckedBackColor = Color.Transparent;
            Utilities.SetDoubleBuffered(this);
            this.EnableDoubleClick(true);
            this.displayName = true;
            this.displayShadow = true;
            this.displayLayers = true;
            this.AllowDrag = false;
            this.thumbnailSize = new Size();
            this.thumbnailMarging = new Padding();
            this.thumbnailPadding = new Padding();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.Focus();
            base.OnMouseDown(e);
            this.dragPosX = e.X;
            this.dragPosY = e.Y;
            this.IsDragging = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!this.IsDragging)
            {
                if (e.Button == MouseButtons.Left && this.dragRadius > 0 && this.AllowDrag)
                {
                    int x = this.dragPosX - e.X;
                    int y = this.dragPosY - e.Y;
                    if (x * x + y * y > this.dragRadius)
                    {
                        base.DoDragDrop(this, DragDropEffects.Move);
                        this.IsDragging = true;
                        return;
                    }
                }
                base.OnMouseMove(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.IsDragging = false;
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            try
            {
                base.OnPaint(pevent);
                bool thumbImage = this.plan == null;
                if (!thumbImage)
                {
                    thumbImage = this.plan.Thumbnail.ThumbImage == null;
                }
                Size size = new Size(this.thumbnailSize.Width - 10, this.thumbnailSize.Height - 20);
                if (!thumbImage)
                {
                    size.Width = this.plan.Thumbnail.ThumbImage.Width;
                    size.Height = this.plan.Thumbnail.ThumbImage.Height;
                }
                int left = (this.thumbnailPadding.Left > 0 ? (this.thumbnailSize.Width - size.Width) / 2 : 0) + this.thumbnailMarging.Left;
                int top = (this.thumbnailPadding.Top > 0 ? (this.thumbnailSize.Height - size.Height) / 2 : 0) + this.thumbnailMarging.Top;
                int width = size.Width - 1;
                int height = size.Height - 1;
                this.boundingRectangle = new Rectangle(left, top, width, height);
                if (thumbImage)
                {
                    this.DrawMissingImage(pevent.Graphics, this.boundingRectangle);
                }
                else
                {
                    this.DrawImage(pevent.Graphics, this.boundingRectangle);
                }
                this.DrawOutline(pevent.Graphics, this.boundingRectangle);
                if (this.displayShadow)
                {
                    this.DrawShadow(pevent.Graphics, this.boundingRectangle);
                }
                if (this.displayName && this.plan != null)
                {
                    this.DrawPlanName(pevent.Graphics, this.boundingRectangle);
                }
                if (this.displayLayers && !thumbImage)
                {
                    this.DrawLayers(pevent.Graphics, this.boundingRectangle);
                }
                if (this.selected)
                {
                    this.DrawSelection(pevent.Graphics, this.boundingRectangle);
                }
            }
            catch
            {
            }
        }

        public void RecalcLayout()
        {
            base.Size = new Size(this.thumbnailSize.Width + this.thumbnailPadding.Left, this.thumbnailSize.Height + this.thumbnailPadding.Top);
        }
    }
}