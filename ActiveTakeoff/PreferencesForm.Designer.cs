namespace QuoterPlan
{
	public partial class PreferencesForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.PreferencesForm));
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.cbDefaultSystemType = new global::QuoterPlan.ComboBoxEx();
			this.lblDefaultSystemType = new global::QuoterPlan.LabelEx();
			this.cbDefaultPrecision = new global::QuoterPlan.ComboBoxEx();
			this.lblDefaultPrecision = new global::QuoterPlan.LabelEx();
			this.lblRepresentativeName = new global::QuoterPlan.LabelEx();
			this.txtRepresentativeName = new global::QuoterPlan.TextBoxEx();
			this.lblCompanyName = new global::QuoterPlan.LabelEx();
			this.txtCompanyAddress = new global::QuoterPlan.TextBoxEx();
			this.lblCompanyAddress = new global::QuoterPlan.LabelEx();
			this.txtCompanyName = new global::QuoterPlan.TextBoxEx();
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			base.SuspendLayout();
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.cbDefaultSystemType);
			this.panelGroup.Controls.Add(this.cbDefaultPrecision);
			this.panelGroup.Controls.Add(this.lblRepresentativeName);
			this.panelGroup.Controls.Add(this.txtRepresentativeName);
			this.panelGroup.Controls.Add(this.lblDefaultPrecision);
			this.panelGroup.Controls.Add(this.lblCompanyName);
			this.panelGroup.Controls.Add(this.lblDefaultSystemType);
			this.panelGroup.Controls.Add(this.txtCompanyAddress);
			this.panelGroup.Controls.Add(this.lblCompanyAddress);
			this.panelGroup.Controls.Add(this.txtCompanyName);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			this.cbDefaultSystemType.AssociatedLabel = this.lblDefaultSystemType;
			this.cbDefaultSystemType.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDefaultSystemType.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbDefaultSystemType, "cbDefaultSystemType");
			this.cbDefaultSystemType.Name = "cbDefaultSystemType";
			this.cbDefaultSystemType.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			componentResourceManager.ApplyResources(this.lblDefaultSystemType, "lblDefaultSystemType");
			this.lblDefaultSystemType.ForeColor = global::System.Drawing.Color.Black;
			this.lblDefaultSystemType.Name = "lblDefaultSystemType";
			this.lblDefaultSystemType.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.cbDefaultPrecision.AssociatedLabel = this.lblDefaultPrecision;
			this.cbDefaultPrecision.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDefaultPrecision.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbDefaultPrecision, "cbDefaultPrecision");
			this.cbDefaultPrecision.Name = "cbDefaultPrecision";
			this.cbDefaultPrecision.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			componentResourceManager.ApplyResources(this.lblDefaultPrecision, "lblDefaultPrecision");
			this.lblDefaultPrecision.ForeColor = global::System.Drawing.Color.Black;
			this.lblDefaultPrecision.Name = "lblDefaultPrecision";
			this.lblDefaultPrecision.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblRepresentativeName, "lblRepresentativeName");
			this.lblRepresentativeName.ForeColor = global::System.Drawing.Color.Black;
			this.lblRepresentativeName.Name = "lblRepresentativeName";
			this.lblRepresentativeName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtRepresentativeName.AssociatedLabel = this.lblRepresentativeName;
			componentResourceManager.ApplyResources(this.txtRepresentativeName, "txtRepresentativeName");
			this.txtRepresentativeName.Name = "txtRepresentativeName";
			this.txtRepresentativeName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtRepresentativeName.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lblCompanyName, "lblCompanyName");
			this.lblCompanyName.ForeColor = global::System.Drawing.Color.Black;
			this.lblCompanyName.Name = "lblCompanyName";
			this.lblCompanyName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCompanyAddress.AssociatedLabel = this.lblCompanyAddress;
			componentResourceManager.ApplyResources(this.txtCompanyAddress, "txtCompanyAddress");
			this.txtCompanyAddress.Name = "txtCompanyAddress";
			this.txtCompanyAddress.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCompanyAddress.Enter += new global::System.EventHandler(this.txtMultiLine_Enter);
			componentResourceManager.ApplyResources(this.lblCompanyAddress, "lblCompanyAddress");
			this.lblCompanyAddress.ForeColor = global::System.Drawing.Color.Black;
			this.lblCompanyAddress.Name = "lblCompanyAddress";
			this.lblCompanyAddress.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCompanyName.AssociatedLabel = this.lblCompanyName;
			componentResourceManager.ApplyResources(this.txtCompanyName, "txtCompanyName");
			this.txtCompanyName.Name = "txtCompanyName";
			this.txtCompanyName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCompanyName.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.groupBox);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PreferencesForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::QuoterPlan.LabelEx lblDefaultSystemType;

		private global::QuoterPlan.LabelEx lblCompanyName;

		private global::QuoterPlan.TextBoxEx txtCompanyAddress;

		private global::QuoterPlan.LabelEx lblCompanyAddress;

		private global::QuoterPlan.TextBoxEx txtCompanyName;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::QuoterPlan.LabelEx lblDefaultPrecision;

		private global::QuoterPlan.LabelEx lblRepresentativeName;

		private global::QuoterPlan.TextBoxEx txtRepresentativeName;

		private global::QuoterPlan.ComboBoxEx cbDefaultPrecision;

		private global::QuoterPlan.ComboBoxEx cbDefaultSystemType;
	}
}
