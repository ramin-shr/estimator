namespace QuoterPlan
{
	public partial class ProgressForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.ProgressForm));
			this.btCancel = new global::System.Windows.Forms.Button();
			this.lblDescription = new global::System.Windows.Forms.Label();
			this.progressBar = new global::DevComponents.DotNetBar.Controls.ProgressBarX();
			this.lblDescription2 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			componentResourceManager.ApplyResources(this.lblDescription, "lblDescription");
			this.lblDescription.Name = "lblDescription";
			this.progressBar.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.progressBar.BackgroundStyle.TextAlignment = global::DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.progressBar.BackgroundStyle.TextColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarCaptionText;
			this.progressBar.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.progressBar, "progressBar");
			this.progressBar.Name = "progressBar";
			this.progressBar.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.progressBar.TabStop = false;
			this.progressBar.TextVisible = true;
			componentResourceManager.ApplyResources(this.lblDescription2, "lblDescription2");
			this.lblDescription2.Name = "lblDescription2";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.CancelButton = this.btCancel;
			base.ControlBox = false;
			base.Controls.Add(this.lblDescription2);
			base.Controls.Add(this.progressBar);
			base.Controls.Add(this.lblDescription);
			base.Controls.Add(this.btCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProgressForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btCancel;

		public global::System.Windows.Forms.Label lblDescription;

		public global::System.Windows.Forms.Label lblDescription2;

		public global::DevComponents.DotNetBar.Controls.ProgressBarX progressBar;
	}
}
