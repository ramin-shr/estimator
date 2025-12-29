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
        private IContainer components;

        public DrawingArea DrawArea
        {
            get;
            set;
        }

        public DrawObject ParentObject
        {
            get;
            set;
        }

        public DrawObjectIconIndicator(DrawObject parentObject, DrawingArea drawArea)
        {
            this.InitializeComponent();
            Utilities.SetDoubleBuffered(this);
            this.BackColor = Color.Transparent;
            this.ParentObject = parentObject;
            this.DrawArea = drawArea;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawObjectIconIndicator_Paint(object sender, PaintEventArgs e)
        {
            if (this.ParentObject != null && this.DrawArea != null)
            {
                using (Image bitmap = new Bitmap(17, 17))
                {
                    using (Graphics graphic = Graphics.FromImage(bitmap))
                    {
                        this.DrawArea.DrawObjectIcon(this.ParentObject, graphic);
                        ColorMatrix colorMatrix = new ColorMatrix()
                        {
                            Matrix33 = (this.ParentObject.Visible ? 1f : 0.35f)
                        };
                        ImageAttributes imageAttribute = new ImageAttributes();
                        imageAttribute.SetColorMatrix(colorMatrix);
                        e.Graphics.DrawImage(bitmap, new Rectangle(1, 1, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttribute);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "DrawObjectIconIndicator";
            base.Paint += new PaintEventHandler(this.DrawObjectIconIndicator_Paint);
            base.ResumeLayout(false);
        }
    }
}