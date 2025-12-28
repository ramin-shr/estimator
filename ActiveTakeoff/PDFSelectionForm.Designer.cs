namespace QuoterPlan
{
	public partial class PDFSelectionForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.PDFSelectionForm));
			this.btValidate = new global::System.Windows.Forms.Button();
			this.btOpen = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.lblExampleCaption = new global::QuoterPlan.LabelEx();
			this.opImportSelection = new global::System.Windows.Forms.RadioButton();
			this.lblExampleText = new global::QuoterPlan.LabelEx();
			this.txtSelection = new global::QuoterPlan.TextBoxEx();
			this.opImportAll = new global::System.Windows.Forms.RadioButton();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btValidate, "btValidate");
			this.btValidate.Name = "btValidate";
			this.btValidate.UseVisualStyleBackColor = true;
			this.btValidate.Click += new global::System.EventHandler(this.btValidate_Click);
			componentResourceManager.ApplyResources(this.btOpen, "btOpen");
			this.btOpen.Name = "btOpen";
			this.btOpen.UseVisualStyleBackColor = true;
			this.btOpen.Click += new global::System.EventHandler(this.btOpen_Click);
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.lblExampleCaption);
			this.panelGroup.Controls.Add(this.opImportSelection);
			this.panelGroup.Controls.Add(this.lblExampleText);
			this.panelGroup.Controls.Add(this.txtSelection);
			this.panelGroup.Controls.Add(this.opImportAll);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.lblExampleCaption, "lblExampleCaption");
			this.lblExampleCaption.Name = "lblExampleCaption";
			this.lblExampleCaption.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.opImportSelection, "opImportSelection");
			this.opImportSelection.Name = "opImportSelection";
			this.opImportSelection.UseVisualStyleBackColor = true;
			this.opImportSelection.CheckedChanged += new global::System.EventHandler(this.opImportSelection_CheckedChanged);
			componentResourceManager.ApplyResources(this.lblExampleText, "lblExampleText");
			this.lblExampleText.Name = "lblExampleText";
			this.lblExampleText.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtSelection.AssociatedLabel = this.opImportSelection;
			componentResourceManager.ApplyResources(this.txtSelection, "txtSelection");
			this.txtSelection.Name = "txtSelection";
			this.txtSelection.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtSelection.Enter += new global::System.EventHandler(this.txtSelection_Enter);
			this.txtSelection.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
			componentResourceManager.ApplyResources(this.opImportAll, "opImportAll");
			this.opImportAll.Checked = true;
			this.opImportAll.Name = "opImportAll";
			this.opImportAll.TabStop = true;
			this.opImportAll.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.ControlBox = false;
			base.Controls.Add(this.btValidate);
			base.Controls.Add(this.btOpen);
			base.Controls.Add(this.groupBox);
			base.Controls.Add(this.btCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PDFSelectionForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btValidate;

		private global::System.Windows.Forms.Button btOpen;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::QuoterPlan.LabelEx lblExampleText;

		private global::QuoterPlan.TextBoxEx txtSelection;

		private global::QuoterPlan.LabelEx lblExampleCaption;

		private global::System.Windows.Forms.RadioButton opImportSelection;

		private global::System.Windows.Forms.RadioButton opImportAll;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::System.Windows.Forms.Button btCancel;
	}
}
