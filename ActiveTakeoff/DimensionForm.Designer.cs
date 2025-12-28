namespace QuoterPlan
{
	public partial class DimensionForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.DimensionForm));
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.btOk.Image = global::QuoterPlan.Properties.Resources.accept_16x16;
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
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

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Panel panel1;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;
	}
}
