namespace QuoterPlan
{
	public partial class ImportSettingsForm : global::QuoterPlan.BaseForm
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
                new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");

            this.SuspendLayout();
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
			resources.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			resources.ApplyResources(this.groupBox, "groupBox");
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
			resources.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			resources.ApplyResources(this.lblFieldSeparator, "lblFieldSeparator");
			this.lblFieldSeparator.ForeColor = global::System.Drawing.Color.Black;
			this.lblFieldSeparator.Name = "lblFieldSeparator";
			this.lblFieldSeparator.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtFieldSeparator.AssociatedLabel = this.lblFieldSeparator;
			resources.ApplyResources(this.txtFieldSeparator, "txtFieldSeparator");
			this.txtFieldSeparator.Name = "txtFieldSeparator";
			this.txtFieldSeparator.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtFieldSeparator.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtFieldSeparator.Enter += new global::System.EventHandler(this.txtField_Enter);
			resources.ApplyResources(this.lblProductPricePosition, "lblProductPricePosition");
			this.lblProductPricePosition.ForeColor = global::System.Drawing.Color.Black;
			this.lblProductPricePosition.Name = "lblProductPricePosition";
			this.lblProductPricePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductPricePosition.AssociatedLabel = this.lblProductPricePosition;
			resources.ApplyResources(this.txtProductPricePosition, "txtProductPricePosition");
			this.txtProductPricePosition.Name = "txtProductPricePosition";
			this.txtProductPricePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductPricePosition.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtProductPricePosition.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtProductPricePosition.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			resources.ApplyResources(this.lblProductCodePosition, "lblProductCodePosition");
			this.lblProductCodePosition.ForeColor = global::System.Drawing.Color.Black;
			this.lblProductCodePosition.Name = "lblProductCodePosition";
			this.lblProductCodePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductCodePosition.AssociatedLabel = this.lblProductCodePosition;
			resources.ApplyResources(this.txtProductCodePosition, "txtProductCodePosition");
			this.txtProductCodePosition.Name = "txtProductCodePosition";
			this.txtProductCodePosition.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductCodePosition.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtProductCodePosition.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtProductCodePosition.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			resources.ApplyResources(this.btFileToImport, "btFileToImport");
			this.btFileToImport.Name = "btFileToImport";
			this.btFileToImport.UseVisualStyleBackColor = true;
			this.btFileToImport.Click += new global::System.EventHandler(this.btFileToImport_Click);
			this.txtFileToImport.AssociatedLabel = this.txtFileToImport;
			resources.ApplyResources(this.txtFileToImport, "txtFileToImport");
			this.txtFileToImport.Name = "txtFileToImport";
			this.txtFileToImport.ReadOnly = true;
			this.txtFileToImport.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtFileToImport.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtFileToImport.Enter += new global::System.EventHandler(this.txtField_Enter);
			resources.ApplyResources(this.lblFileToImport, "lblFileToImport");
			this.lblFileToImport.ForeColor = global::System.Drawing.Color.Black;
			this.lblFileToImport.Name = "lblFileToImport";
			this.lblFileToImport.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			resources.ApplyResources(this.btHelp, "btHelp");
			this.btHelp.Name = "btHelp";
			this.btHelp.UseVisualStyleBackColor = true;
			this.btHelp.Click += new global::System.EventHandler(this.btHelp_Click);
			resources.ApplyResources(this, "$this");
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
