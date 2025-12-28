namespace QuoterPlan
{
	public partial class DBEstimatingItemForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.DBEstimatingItemForm));
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.lblBidCode = new global::QuoterPlan.LabelEx();
			this.txtBidCode = new global::QuoterPlan.TextBoxEx();
			this.cbUnitOfMeasure = new global::QuoterPlan.ComboBoxEx();
			this.lblUnitOfMeasure = new global::QuoterPlan.LabelEx();
			this.lblSubSection = new global::QuoterPlan.LabelEx();
			this.cbSubSection = new global::QuoterPlan.ComboBoxEx();
			this.cbSection = new global::QuoterPlan.ComboBoxEx();
			this.lblSection = new global::QuoterPlan.LabelEx();
			this.lblCoverageUnit = new global::QuoterPlan.LabelEx();
			this.txtCoverateUnit = new global::QuoterPlan.TextBoxEx();
			this.lblPrice = new global::QuoterPlan.LabelEx();
			this.txtPrice = new global::QuoterPlan.TextBoxEx();
			this.lblCoverageMeasure = new global::QuoterPlan.LabelEx();
			this.txtCoverageValue = new global::QuoterPlan.TextBoxEx();
			this.lblPurchaseUnit = new global::QuoterPlan.LabelEx();
			this.lblProductName = new global::QuoterPlan.LabelEx();
			this.txtProductName = new global::QuoterPlan.TextBoxEx();
			this.cbPurchaseUnit = new global::QuoterPlan.ComboBoxEx();
			this.txtItemID = new global::QuoterPlan.TextBoxEx();
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
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.lblBidCode);
			this.panelGroup.Controls.Add(this.txtBidCode);
			this.panelGroup.Controls.Add(this.cbUnitOfMeasure);
			this.panelGroup.Controls.Add(this.lblSubSection);
			this.panelGroup.Controls.Add(this.cbSubSection);
			this.panelGroup.Controls.Add(this.cbSection);
			this.panelGroup.Controls.Add(this.lblSection);
			this.panelGroup.Controls.Add(this.lblCoverageUnit);
			this.panelGroup.Controls.Add(this.txtCoverateUnit);
			this.panelGroup.Controls.Add(this.lblPrice);
			this.panelGroup.Controls.Add(this.txtPrice);
			this.panelGroup.Controls.Add(this.lblCoverageMeasure);
			this.panelGroup.Controls.Add(this.txtCoverageValue);
			this.panelGroup.Controls.Add(this.lblPurchaseUnit);
			this.panelGroup.Controls.Add(this.lblUnitOfMeasure);
			this.panelGroup.Controls.Add(this.lblProductName);
			this.panelGroup.Controls.Add(this.txtProductName);
			this.panelGroup.Controls.Add(this.cbPurchaseUnit);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.lblBidCode, "lblBidCode");
			this.lblBidCode.ForeColor = global::System.Drawing.Color.Black;
			this.lblBidCode.Name = "lblBidCode";
			this.lblBidCode.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtBidCode.AssociatedLabel = this.lblBidCode;
			componentResourceManager.ApplyResources(this.txtBidCode, "txtBidCode");
			this.txtBidCode.Name = "txtBidCode";
			this.txtBidCode.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtBidCode.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtBidCode.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtBidCode.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtText_Validating);
			this.cbUnitOfMeasure.AssociatedLabel = this.lblUnitOfMeasure;
			this.cbUnitOfMeasure.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbUnitOfMeasure.FormattingEnabled = true;
			this.cbUnitOfMeasure.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cbUnitOfMeasure.Items")
			});
			componentResourceManager.ApplyResources(this.cbUnitOfMeasure, "cbUnitOfMeasure");
			this.cbUnitOfMeasure.Name = "cbUnitOfMeasure";
			this.cbUnitOfMeasure.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbUnitOfMeasure.SelectedIndexChanged += new global::System.EventHandler(this.cbUnitOfMeasure_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.lblUnitOfMeasure, "lblUnitOfMeasure");
			this.lblUnitOfMeasure.ForeColor = global::System.Drawing.Color.Black;
			this.lblUnitOfMeasure.Name = "lblUnitOfMeasure";
			this.lblUnitOfMeasure.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblSubSection, "lblSubSection");
			this.lblSubSection.BackColor = global::System.Drawing.Color.Transparent;
			this.lblSubSection.ForeColor = global::System.Drawing.Color.Black;
			this.lblSubSection.Name = "lblSubSection";
			this.lblSubSection.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.cbSubSection.AssociatedLabel = this.lblSubSection;
			this.cbSubSection.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSubSection.FormattingEnabled = true;
			this.cbSubSection.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cbSubSection.Items")
			});
			componentResourceManager.ApplyResources(this.cbSubSection, "cbSubSection");
			this.cbSubSection.Name = "cbSubSection";
			this.cbSubSection.Sorted = true;
			this.cbSubSection.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbSection.AssociatedLabel = this.lblSection;
			this.cbSection.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSection.FormattingEnabled = true;
			this.cbSection.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cbSection.Items"),
				componentResourceManager.GetString("cbSection.Items1"),
				componentResourceManager.GetString("cbSection.Items2")
			});
			componentResourceManager.ApplyResources(this.cbSection, "cbSection");
			this.cbSection.Name = "cbSection";
			this.cbSection.Sorted = true;
			this.cbSection.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbSection.SelectedIndexChanged += new global::System.EventHandler(this.cbSection_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.lblSection, "lblSection");
			this.lblSection.BackColor = global::System.Drawing.Color.Transparent;
			this.lblSection.ForeColor = global::System.Drawing.Color.Black;
			this.lblSection.Name = "lblSection";
			this.lblSection.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblCoverageUnit.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.lblCoverageUnit, "lblCoverageUnit");
			this.lblCoverageUnit.Name = "lblCoverageUnit";
			this.lblCoverageUnit.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCoverateUnit.AssociatedLabel = null;
			componentResourceManager.ApplyResources(this.txtCoverateUnit, "txtCoverateUnit");
			this.txtCoverateUnit.Name = "txtCoverateUnit";
			this.txtCoverateUnit.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCoverateUnit.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtCoverateUnit.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtCoverateUnit.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtDouble_KeyPress);
			this.txtCoverateUnit.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtDouble_Validating);
			componentResourceManager.ApplyResources(this.lblPrice, "lblPrice");
			this.lblPrice.ForeColor = global::System.Drawing.Color.Black;
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtPrice.AssociatedLabel = this.lblPrice;
			componentResourceManager.ApplyResources(this.txtPrice, "txtPrice");
			this.txtPrice.Name = "txtPrice";
			this.txtPrice.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtPrice.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtPrice.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtPrice.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtDouble_KeyPress);
			this.txtPrice.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtCurrency_Validating);
			this.lblCoverageMeasure.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.lblCoverageMeasure, "lblCoverageMeasure");
			this.lblCoverageMeasure.Name = "lblCoverageMeasure";
			this.lblCoverageMeasure.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCoverageValue.AssociatedLabel = null;
			componentResourceManager.ApplyResources(this.txtCoverageValue, "txtCoverageValue");
			this.txtCoverageValue.Name = "txtCoverageValue";
			this.txtCoverageValue.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCoverageValue.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtCoverageValue.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtCoverageValue.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtDouble_KeyPress);
			this.txtCoverageValue.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtDouble_Validating);
			componentResourceManager.ApplyResources(this.lblPurchaseUnit, "lblPurchaseUnit");
			this.lblPurchaseUnit.ForeColor = global::System.Drawing.Color.Black;
			this.lblPurchaseUnit.Name = "lblPurchaseUnit";
			this.lblPurchaseUnit.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblProductName, "lblProductName");
			this.lblProductName.ForeColor = global::System.Drawing.Color.Black;
			this.lblProductName.Name = "lblProductName";
			this.lblProductName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductName.AssociatedLabel = this.lblProductName;
			componentResourceManager.ApplyResources(this.txtProductName, "txtProductName");
			this.txtProductName.Name = "txtProductName";
			this.txtProductName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProductName.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtProductName.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtProductName.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtText_Validating);
			this.cbPurchaseUnit.AssociatedLabel = this.lblPurchaseUnit;
			this.cbPurchaseUnit.FormattingEnabled = true;
			this.cbPurchaseUnit.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cbPurchaseUnit.Items")
			});
			componentResourceManager.ApplyResources(this.cbPurchaseUnit, "cbPurchaseUnit");
			this.cbPurchaseUnit.Name = "cbPurchaseUnit";
			this.cbPurchaseUnit.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbPurchaseUnit.SelectedIndexChanged += new global::System.EventHandler(this.cbPurchaseUnit_Update);
			this.cbPurchaseUnit.TextUpdate += new global::System.EventHandler(this.cbPurchaseUnit_Update);
			this.cbPurchaseUnit.Enter += new global::System.EventHandler(this.cbField_Enter);
			this.txtItemID.AssociatedLabel = null;
			componentResourceManager.ApplyResources(this.txtItemID, "txtItemID");
			this.txtItemID.Name = "txtItemID";
			this.txtItemID.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtItemID.Click += new global::System.EventHandler(this.txtField2_Enter);
			this.txtItemID.Enter += new global::System.EventHandler(this.txtField2_Enter);
			base.AcceptButton = this.btOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.txtItemID);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DBEstimatingItemForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.Activated += new global::System.EventHandler(this.DBEstimatingItemForm_Activated);
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::QuoterPlan.LabelEx lblProductName;

		private global::QuoterPlan.TextBoxEx txtProductName;

		private global::QuoterPlan.LabelEx lblPurchaseUnit;

		private global::QuoterPlan.LabelEx lblUnitOfMeasure;

		private global::QuoterPlan.LabelEx lblPrice;

		private global::QuoterPlan.TextBoxEx txtPrice;

		private global::QuoterPlan.TextBoxEx txtCoverageValue;

		private global::QuoterPlan.ComboBoxEx cbSubSection;

		private global::QuoterPlan.ComboBoxEx cbSection;

		private global::QuoterPlan.LabelEx lblSection;

		private global::QuoterPlan.LabelEx lblSubSection;

		private global::QuoterPlan.ComboBoxEx cbUnitOfMeasure;

		private global::QuoterPlan.ComboBoxEx cbPurchaseUnit;

		private global::QuoterPlan.LabelEx lblBidCode;

		private global::QuoterPlan.TextBoxEx txtBidCode;

		private global::QuoterPlan.LabelEx lblCoverageUnit;

		private global::QuoterPlan.TextBoxEx txtCoverateUnit;

		private global::QuoterPlan.LabelEx lblCoverageMeasure;

		private global::QuoterPlan.TextBoxEx txtItemID;
	}
}
