namespace QuoterPlan
{
	public partial class TaxesConfiguration : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.TaxesConfiguration));
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.chkTaxOnTax = new global::System.Windows.Forms.CheckBox();
			this.txtTax2Rate = new global::QuoterPlan.TextBoxEx();
			this.lblTax2Rate = new global::QuoterPlan.LabelEx();
			this.lblTax1Label = new global::QuoterPlan.LabelEx();
			this.lblTax1Rate = new global::QuoterPlan.LabelEx();
			this.lblTax2Label = new global::QuoterPlan.LabelEx();
			this.txtTax1Label = new global::QuoterPlan.TextBoxEx();
			this.txtTax1Rate = new global::QuoterPlan.TextBoxEx();
			this.txtTax2Label = new global::QuoterPlan.TextBoxEx();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			base.SuspendLayout();
			this.btOk.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
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
			this.panelGroup.Controls.Add(this.chkTaxOnTax);
			this.panelGroup.Controls.Add(this.txtTax2Rate);
			this.panelGroup.Controls.Add(this.lblTax1Label);
			this.panelGroup.Controls.Add(this.lblTax2Rate);
			this.panelGroup.Controls.Add(this.lblTax1Rate);
			this.panelGroup.Controls.Add(this.lblTax2Label);
			this.panelGroup.Controls.Add(this.txtTax1Label);
			this.panelGroup.Controls.Add(this.txtTax1Rate);
			this.panelGroup.Controls.Add(this.txtTax2Label);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.chkTaxOnTax, "chkTaxOnTax");
			this.chkTaxOnTax.Name = "chkTaxOnTax";
			this.chkTaxOnTax.UseVisualStyleBackColor = true;
			this.txtTax2Rate.AssociatedLabel = this.lblTax2Rate;
			componentResourceManager.ApplyResources(this.txtTax2Rate, "txtTax2Rate");
			this.txtTax2Rate.Name = "txtTax2Rate";
			this.txtTax2Rate.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtTax2Rate.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax2Rate.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax2Rate.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtTaxRate_Validating);
			componentResourceManager.ApplyResources(this.lblTax2Rate, "lblTax2Rate");
			this.lblTax2Rate.ForeColor = global::System.Drawing.Color.Black;
			this.lblTax2Rate.Name = "lblTax2Rate";
			this.lblTax2Rate.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblTax1Label, "lblTax1Label");
			this.lblTax1Label.ForeColor = global::System.Drawing.Color.Black;
			this.lblTax1Label.Name = "lblTax1Label";
			this.lblTax1Label.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblTax1Rate, "lblTax1Rate");
			this.lblTax1Rate.ForeColor = global::System.Drawing.Color.Black;
			this.lblTax1Rate.Name = "lblTax1Rate";
			this.lblTax1Rate.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblTax2Label, "lblTax2Label");
			this.lblTax2Label.ForeColor = global::System.Drawing.Color.Black;
			this.lblTax2Label.Name = "lblTax2Label";
			this.lblTax2Label.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtTax1Label.AssociatedLabel = this.lblTax1Label;
			componentResourceManager.ApplyResources(this.txtTax1Label, "txtTax1Label");
			this.txtTax1Label.Name = "txtTax1Label";
			this.txtTax1Label.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtTax1Label.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax1Label.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax1Label.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtTaxLabel_Validating);
			this.txtTax1Rate.AssociatedLabel = this.lblTax1Rate;
			componentResourceManager.ApplyResources(this.txtTax1Rate, "txtTax1Rate");
			this.txtTax1Rate.Name = "txtTax1Rate";
			this.txtTax1Rate.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtTax1Rate.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax1Rate.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax1Rate.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtTaxRate_Validating);
			this.txtTax2Label.AssociatedLabel = this.lblTax2Label;
			componentResourceManager.ApplyResources(this.txtTax2Label, "txtTax2Label");
			this.txtTax2Label.Name = "txtTax2Label";
			this.txtTax2Label.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtTax2Label.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax2Label.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtTax2Label.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtTaxLabel_Validating);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TaxesConfiguration";
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

		private global::QuoterPlan.LabelEx lblTax1Label;

		private global::QuoterPlan.LabelEx lblTax2Rate;

		private global::QuoterPlan.LabelEx lblTax1Rate;

		private global::QuoterPlan.TextBoxEx txtTax2Label;

		private global::QuoterPlan.LabelEx lblTax2Label;

		private global::QuoterPlan.TextBoxEx txtTax1Label;

		private global::QuoterPlan.TextBoxEx txtTax2Rate;

		private global::QuoterPlan.TextBoxEx txtTax1Rate;

		private global::System.Windows.Forms.CheckBox chkTaxOnTax;
	}
}
