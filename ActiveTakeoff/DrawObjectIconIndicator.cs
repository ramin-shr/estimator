using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class DrawObjectIconIndicator : UserControl
	{
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
			base.Name = "DrawObjectIconIndicator";
			base.Paint += this.DrawObjectIconIndicator_Paint;
			base.ResumeLayout(false);
		}

		public DrawObject ParentObject
		{
			[CompilerGenerated]
			get
			{
				return this.<ParentObject>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ParentObject>k__BackingField = value;
			}
		}

		public DrawingArea DrawArea
		{
			[CompilerGenerated]
			get
			{
				return this.<DrawArea>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DrawArea>k__BackingField = value;
			}
		}

		public DrawObjectIconIndicator(DrawObject parentObject, DrawingArea drawArea)
		{
			this.InitializeComponent();
			Utilities.SetDoubleBuffered(this);
			this.BackColor = Color.Transparent;
			this.ParentObject = parentObject;
			this.DrawArea = drawArea;
		}

		private void DrawObjectIconIndicator_Paint(object sender, PaintEventArgs e)
		{
			if (this.ParentObject != null && this.DrawArea != null)
			{
				using (Image image = new Bitmap(17, 17))
				{
					using (Graphics graphics = Graphics.FromImage(image))
					{
						this.DrawArea.DrawObjectIcon(this.ParentObject, graphics);
						ColorMatrix colorMatrix = new ColorMatrix();
						colorMatrix.Matrix33 = (this.ParentObject.Visible ? 1f : 0.35f);
						ImageAttributes imageAttributes = new ImageAttributes();
						imageAttributes.SetColorMatrix(colorMatrix);
						e.Graphics.DrawImage(image, new Rectangle(1, 1, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
					}
				}
			}
		}

		private IContainer components;

		[CompilerGenerated]
		private DrawObject <ParentObject>k__BackingField;

		[CompilerGenerated]
		private DrawingArea <DrawArea>k__BackingField;
	}
}
