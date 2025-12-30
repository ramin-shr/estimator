// BaseForm.Designer.cs
namespace QuoterPlan
{
    partial class BaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.KeyPreview = true;
            this.Name = "BaseForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BaseForm_KeyDown);

            this.ResumeLayout(false);
        }
    }
}
