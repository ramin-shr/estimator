namespace QuoterPlan
{
	public partial class DimensionForm : BaseForm
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
            this.panel1 = new global::System.Windows.Forms.Panel();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.btOk.Image = Properties.Resources.accept_16x16;
			resources.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Image = Properties.Resources.delete_16x16;
			resources.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DimensionForm";
			base.ShowInTaskbar = false;
			base.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.DimensionForm_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.Windows.Forms.Panel panel1;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;
	}
}
