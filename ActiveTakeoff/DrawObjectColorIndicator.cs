using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DrawObjectColorIndicator : UserControl
    {
        private IContainer components;

        public DrawObject ParentObject
        {
            get;
            set;
        }

        public DrawObjectColorIndicator(DrawObject parentObject)
        {
            this.InitializeComponent();
            Utilities.SetDoubleBuffered(this);
            this.ParentObject = parentObject;
            this.Font = Utilities.GetDefaultFont();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawObjectColorIndicator_Paint(object sender, PaintEventArgs e)
        {
            if (this.ParentObject != null)
            {
                Rectangle rectangle = new Rectangle(1, 1, 10, 10);
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb((this.ParentObject.Visible ? 0xff : 35), Color.Black), 2f), rectangle);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb((this.ParentObject.Visible ? 0xff : 135), this.ParentObject.Color)), rectangle);
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "DrawObjectColorIndicator";
            base.Paint += new PaintEventHandler(this.DrawObjectColorIndicator_Paint);
            base.ResumeLayout(false);
        }
    }
}