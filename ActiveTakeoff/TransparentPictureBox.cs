using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class TransparentPictureBox : Control
	{
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		public TransparentPictureBox()
		{
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.Opaque, true);
			this.BackColor = Color.Transparent;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 32;
				return createParams;
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (this.image != null)
			{
				graphics.DrawImage(this.image, new Rectangle(0, 0, this.image.Width, this.image.Height));
			}
			else
			{
				graphics.Clear(this.BackColor);
			}
			base.OnPaint(e);
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			if (base.Parent != null)
			{
				base.Parent.Invalidate(base.Bounds, true);
			}
			base.OnBackColorChanged(e);
		}

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.Invalidate();
			base.OnParentBackColorChanged(e);
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
			this.components = new Container();
		}

		private Image image;

		private IContainer components;
	}
}
