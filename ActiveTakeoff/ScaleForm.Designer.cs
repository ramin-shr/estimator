namespace QuoterPlan
{
	public partial class ScaleForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.ScaleForm));
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.lblScale_1 = new global::QuoterPlan.LabelEx();
			this.lblScale = new global::QuoterPlan.LabelEx();
			this.lblScaleReference = new global::QuoterPlan.LabelEx();
			this.lblMeasureType = new global::QuoterPlan.LabelEx();
			this.cbScale = new global::QuoterPlan.ComboBoxEx();
			this.lblManual_2 = new global::QuoterPlan.LabelEx();
			this.txtManualInches = new global::QuoterPlan.TextBoxEx();
			this.lblManual = new global::QuoterPlan.LabelEx();
			this.lblManual_1 = new global::QuoterPlan.LabelEx();
			this.txtManualFeet = new global::QuoterPlan.TextBoxEx();
			this.btManual = new global::System.Windows.Forms.Button();
			this.txtManualMetric = new global::QuoterPlan.TextBoxEx();
			this.txtScale = new global::QuoterPlan.TextBoxEx();
			this.cbImperialScale = new global::QuoterPlan.ComboBoxEx();
			this.btModify = new global::System.Windows.Forms.Button();
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
			this.groupBox.BackColor = global::System.Drawing.Color.Transparent;
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.lblScale_1);
			this.panelGroup.Controls.Add(this.lblScale);
			this.panelGroup.Controls.Add(this.lblScaleReference);
			this.panelGroup.Controls.Add(this.lblMeasureType);
			this.panelGroup.Controls.Add(this.cbScale);
			this.panelGroup.Controls.Add(this.lblManual_2);
			this.panelGroup.Controls.Add(this.txtManualInches);
			this.panelGroup.Controls.Add(this.lblManual_1);
			this.panelGroup.Controls.Add(this.txtManualFeet);
			this.panelGroup.Controls.Add(this.btManual);
			this.panelGroup.Controls.Add(this.txtManualMetric);
			this.panelGroup.Controls.Add(this.lblManual);
			this.panelGroup.Controls.Add(this.txtScale);
			this.panelGroup.Controls.Add(this.cbImperialScale);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			this.lblScale_1.BackColor = global::System.Drawing.Color.Transparent;
			componentResourceManager.ApplyResources(this.lblScale_1, "lblScale_1");
			this.lblScale_1.Name = "lblScale_1";
			this.lblScale_1.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblScale, "lblScale");
			this.lblScale.BackColor = global::System.Drawing.Color.Transparent;
			this.lblScale.Name = "lblScale";
			this.lblScale.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblScaleReference.BackColor = global::System.Drawing.Color.Transparent;
			componentResourceManager.ApplyResources(this.lblScaleReference, "lblScaleReference");
			this.lblScaleReference.Name = "lblScaleReference";
			this.lblScaleReference.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblMeasureType, "lblMeasureType");
			this.lblMeasureType.BackColor = global::System.Drawing.Color.Transparent;
			this.lblMeasureType.Name = "lblMeasureType";
			this.lblMeasureType.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.cbScale.AssociatedLabel = this.lblMeasureType;
			this.cbScale.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScale.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbScale, "cbScale");
			this.cbScale.Name = "cbScale";
			this.cbScale.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbScale.SelectedIndexChanged += new global::System.EventHandler(this.cbScale_SelectedIndexChanged);
			this.lblManual_2.BackColor = global::System.Drawing.Color.Transparent;
			componentResourceManager.ApplyResources(this.lblManual_2, "lblManual_2");
			this.lblManual_2.Name = "lblManual_2";
			this.lblManual_2.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtManualInches.AssociatedLabel = this.lblManual;
			componentResourceManager.ApplyResources(this.txtManualInches, "txtManualInches");
			this.txtManualInches.Name = "txtManualInches";
			this.txtManualInches.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtManualInches.TextChanged += new global::System.EventHandler(this.txtManual_TextChanged);
			this.txtManualInches.Enter += new global::System.EventHandler(this.txtNumeric_Enter);
			this.txtManualInches.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtManualInches.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			this.lblManual.BackColor = global::System.Drawing.Color.Transparent;
			componentResourceManager.ApplyResources(this.lblManual, "lblManual");
			this.lblManual.Name = "lblManual";
			this.lblManual.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblManual_1.BackColor = global::System.Drawing.Color.Transparent;
			componentResourceManager.ApplyResources(this.lblManual_1, "lblManual_1");
			this.lblManual_1.Name = "lblManual_1";
			this.lblManual_1.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtManualFeet.AssociatedLabel = this.lblManual;
			componentResourceManager.ApplyResources(this.txtManualFeet, "txtManualFeet");
			this.txtManualFeet.Name = "txtManualFeet";
			this.txtManualFeet.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtManualFeet.TextChanged += new global::System.EventHandler(this.txtManual_TextChanged);
			this.txtManualFeet.Enter += new global::System.EventHandler(this.txtNumeric_Enter);
			this.txtManualFeet.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtManualFeet.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			componentResourceManager.ApplyResources(this.btManual, "btManual");
			this.btManual.Name = "btManual";
			this.btManual.Text = global::QuoterPlan.Properties.Resources.Ajustement_manuel;
			this.btManual.UseVisualStyleBackColor = true;
			this.btManual.Click += new global::System.EventHandler(this.btManual_Click);
			this.txtManualMetric.AssociatedLabel = null;
			componentResourceManager.ApplyResources(this.txtManualMetric, "txtManualMetric");
			this.txtManualMetric.Name = "txtManualMetric";
			this.txtManualMetric.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtManualMetric.TextChanged += new global::System.EventHandler(this.txtManual_TextChanged);
			this.txtManualMetric.Enter += new global::System.EventHandler(this.txtNumeric_Enter);
			this.txtManualMetric.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtManualMetric.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			this.txtScale.AssociatedLabel = this.lblScale;
			componentResourceManager.ApplyResources(this.txtScale, "txtScale");
			this.txtScale.Name = "txtScale";
			this.txtScale.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtScale.Enter += new global::System.EventHandler(this.txtNumeric_Enter);
			this.txtScale.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			this.txtScale.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtNumeric_Validating);
			this.cbImperialScale.AssociatedLabel = this.lblScale;
			this.cbImperialScale.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbImperialScale.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbImperialScale, "cbImperialScale");
			this.cbImperialScale.Name = "cbImperialScale";
			this.cbImperialScale.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			componentResourceManager.ApplyResources(this.btModify, "btModify");
			this.btModify.Name = "btModify";
			this.btModify.UseVisualStyleBackColor = true;
			this.btModify.Click += new global::System.EventHandler(this.btModify_Click);
			base.AcceptButton = this.btOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.groupBox);
			base.Controls.Add(this.btModify);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.btOk);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ScaleForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.Shown += new global::System.EventHandler(this.ScaleForm_Shown);
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.Button btModify;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::QuoterPlan.LabelEx lblScale;

		private global::QuoterPlan.LabelEx lblScaleReference;

		private global::QuoterPlan.ComboBoxEx cbImperialScale;

		private global::QuoterPlan.ComboBoxEx cbScale;

		private global::QuoterPlan.TextBoxEx txtScale;

		private global::System.Windows.Forms.Button btManual;

		private global::QuoterPlan.LabelEx lblManual;

		private global::QuoterPlan.LabelEx lblManual_2;

		private global::QuoterPlan.TextBoxEx txtManualInches;

		private global::QuoterPlan.LabelEx lblManual_1;

		private global::QuoterPlan.TextBoxEx txtManualFeet;

		private global::QuoterPlan.TextBoxEx txtManualMetric;

		private global::QuoterPlan.LabelEx lblMeasureType;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::QuoterPlan.LabelEx lblScale_1;
	}
}
