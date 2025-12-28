using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class DrawObjectColorIndicator : UserControl
	{
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

		public DrawObjectColorIndicator(DrawObject parentObject)
		{
			this.InitializeComponent();
			Utilities.SetDoubleBuffered(this);
			this.ParentObject = parentObject;
			this.Font = Utilities.GetDefaultFont();
		}

		private void DrawObjectColorIndicator_Paint(object sender, PaintEventArgs e)
		{
			if (this.ParentObject != null)
			{
				Rectangle rect = new Rectangle(1, 1, 10, 10);
				e.Graphics.DrawRectangle(new Pen(Color.FromArgb(this.ParentObject.Visible ? 255 : 35, Color.Black), 2f), rect);
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(this.ParentObject.Visible ? 255 : 135, this.ParentObject.Color)), rect);
			}
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
			base.Name = "DrawObjectColorIndicator";
			base.Paint += this.DrawObjectColorIndicator_Paint;
			base.ResumeLayout(false);
		}

		private IContainer components;

		[CompilerGenerated]
		private DrawObject <ParentObject>k__BackingField;
	}
}
