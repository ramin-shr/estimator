namespace QuoterPlan
{
	public partial class ImportSettingsForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.ImportSettingsForm));
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.lblFieldSeparator = new global::QuoterPlan.LabelEx();
			this.txtFieldSeparator = new global::QuoterPlan.TextBoxEx();
			this.lblProductPricePosition = new global::QuoterPlan.LabelEx();
			this.txtProductPricePosition = new global::QuoterPlan.TextBoxEx();
			this.lblProductCodePosition = new global::QuoterPlan.LabelEx();
			this.txtProductCodePosition = new global::QuoterPlan.TextBoxEx();
			this.btFileToImport = new global::System.Windows.Forms.Button();
			this.txtFileToImport = new global::QuoterPlan.TextBoxEx();
			this.lblFileToImport = new global::QuoterPlan.LabelEx();
			this.btHelp = new global::System.Windows.Forms.Button();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.lblFieldSeparator);
			this.panelGroup.Controls.Add(this.txtFieldSeparator);
			this.panelGroup.Controls.Add(this.lblProductPricePosition);
			this.panelGroup.Controls.Add(this.txtProductPricePosition);
			this.panelGroup.Controls.Add(this.lblProductCodePosition);
			this.panelGroup.Controls.Add(this.txtProductCodePosition);
			this.panelGroup.Controls.Add(this.btFileToImport);
			this.panelGroup.Controls.Add(this.txtFileToImport);
			this.panelGroup.Controls.Add(this.lblFileToImport);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.lblFieldSeparator, "lblFieldSeparator");
			this.lblFieldSeparator.ForeColor = global::System.Drawing.Color.Black;
			this.lblFieldSeparator.Name = "lblFieldSeparator";
			this.lblFieldSeparator.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtFieldSeparator.AssociatedLabel = this.lblFieldSeparator;
			componentResourceManager.ApplyResources(this.txtFieldSeparator, "txtFieldSeparator");
			this.txtFieldSeparator.Name = "txtFieldSeparator";
			this.txtFieldSeparator.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtFieldSeparator.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtFieldSeparator.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lblProductPricePosition, "lblProductPricePosition");
			this.lblProductPricePosition.ForeColor = global::System.Drawing.Color.Black;
			this.lblProductPricePosition.Name = "lblProductPricePosition";
			this.lblProductPricePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductPricePosition.AssociatedLabel = this.lblProductPricePosition;
			componentResourceManager.ApplyResources(this.txtProductPricePosition, "txtProductPricePosition");
			this.txtProductPricePosition.Name = "txtProductPricePosition";
			this.txtProductPricePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductPricePosition.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtProductPricePosition.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtProductPricePosition.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			componentResourceManager.ApplyResources(this.lblProductCodePosition, "lblProductCodePosition");
			this.lblProductCodePosition.ForeColor = global::System.Drawing.Color.Black;
			this.lblProductCodePosition.Name = "lblProductCodePosition";
			this.lblProductCodePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductCodePosition.AssociatedLabel = this.lblProductCodePosition;
			componentResourceManager.ApplyResources(this.txtProductCodePosition, "txtProductCodePosition");
			this.txtProductCodePosition.Name = "txtProductCodePosition";
			this.txtProductCodePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductCodePosition.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtProductCodePosition.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtProductCodePosition.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			componentResourceManager.ApplyResources(this.btFileToImport, "btFileToImport");
			this.btFileToImport.Name = "btFileToImport";
			this.btFileToImport.UseVisualStyleBackColor = true;
			this.btFileToImport.Click += new global::System.EventHandler(this.btFileToImport_Click);
			this.txtFileToImport.AssociatedLabel = this.txtFileToImport;
			componentResourceManager.ApplyResources(this.txtFileToImport, "txtFileToImport");
			this.txtFileToImport.Name = "txtFileToImport";
			this.txtFileToImport.ReadOnly = true;
			this.txtFileToImport.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtFileToImport.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtFileToImport.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lblFileToImport, "lblFileToImport");
			this.lblFileToImport.ForeColor = global::System.Drawing.Color.Black;
			this.lblFileToImport.Name = "lblFileToImport";
			this.lblFileToImport.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.btHelp, "btHelp");
			this.btHelp.Name = "btHelp";
			this.btHelp.UseVisualStyleBackColor = true;
			this.btHelp.Click += new global::System.EventHandler(this.btHelp_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.btHelp);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ImportSettingsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::System.Windows.Forms.Button btFileToImport;

		private global::QuoterPlan.TextBoxEx txtFileToImport;

		private global::QuoterPlan.LabelEx lblProductCodePosition;

		private global::QuoterPlan.TextBoxEx txtProductCodePosition;

		private global::QuoterPlan.LabelEx lblFileToImport;

		private global::QuoterPlan.LabelEx lblFieldSeparator;

		private global::QuoterPlan.TextBoxEx txtFieldSeparator;

		private global::QuoterPlan.LabelEx lblProductPricePosition;

		private global::QuoterPlan.TextBoxEx txtProductPricePosition;

		private global::System.Windows.Forms.Button btHelp;
	}
}
