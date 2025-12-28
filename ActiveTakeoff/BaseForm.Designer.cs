namespace QuoterPlan
{
	public partial class BaseForm : global::DevComponents.DotNetBar.Office2007Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.BaseForm));
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.KeyPreview = true;
			base.Name = "BaseForm";
			base.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.BaseForm_KeyDown);
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;
	}
}
