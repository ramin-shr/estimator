namespace QuoterPlan
{
	public partial class DataFolderForm : global::QuoterPlan.BaseForm
	{
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(DataFolderForm));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");
            this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.lblWarning = new global::QuoterPlan.LabelEx();
			this.btSelectAlternateFolder = new global::System.Windows.Forms.Button();
			this.lblNote = new global::QuoterPlan.LabelEx();
			this.opAlternateFolder = new global::System.Windows.Forms.RadioButton();
			this.lblNoteText = new global::QuoterPlan.LabelEx();
			this.txtAlternateFolder = new global::QuoterPlan.TextBoxEx();
			this.opDefaultFolder = new global::System.Windows.Forms.RadioButton();
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
			this.panelGroup.Controls.Add(this.lblWarning);
			this.panelGroup.Controls.Add(this.btSelectAlternateFolder);
			this.panelGroup.Controls.Add(this.lblNote);
			this.panelGroup.Controls.Add(this.opAlternateFolder);
			this.panelGroup.Controls.Add(this.lblNoteText);
			this.panelGroup.Controls.Add(this.txtAlternateFolder);
			this.panelGroup.Controls.Add(this.opDefaultFolder);
			resources.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			resources.ApplyResources(this.lblWarning, "lblWarning");
			this.lblWarning.Name = "lblWarning";
			this.lblWarning.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			resources.ApplyResources(this.btSelectAlternateFolder, "btSelectAlternateFolder");
			this.btSelectAlternateFolder.Name = "btSelectAlternateFolder";
			this.btSelectAlternateFolder.UseVisualStyleBackColor = true;
			this.btSelectAlternateFolder.Click += new global::System.EventHandler(this.btSelectAlternateFolder_Click);
			resources.ApplyResources(this.lblNote, "lblNote");
			this.lblNote.Name = "lblNote";
			this.lblNote.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			resources.ApplyResources(this.opAlternateFolder, "opAlternateFolder");
			this.opAlternateFolder.Name = "opAlternateFolder";
			this.opAlternateFolder.UseVisualStyleBackColor = true;
			this.opAlternateFolder.CheckedChanged += new global::System.EventHandler(this.opAlternateFolder_CheckedChanged);
			resources.ApplyResources(this.lblNoteText, "lblNoteText");
			this.lblNoteText.Name = "lblNoteText";
			this.lblNoteText.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtAlternateFolder.AssociatedLabel = this.opAlternateFolder;
			resources.ApplyResources(this.txtAlternateFolder, "txtAlternateFolder");
			this.txtAlternateFolder.Name = "txtAlternateFolder";
			this.txtAlternateFolder.ReadOnly = true;
			this.txtAlternateFolder.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtAlternateFolder.Click += new global::System.EventHandler(this.txtAlternateFolder_MouseEnter);
			this.txtAlternateFolder.MouseEnter += new global::System.EventHandler(this.txtAlternateFolder_MouseEnter);
			resources.ApplyResources(this.opDefaultFolder, "opDefaultFolder");
			this.opDefaultFolder.Checked = true;
			this.opDefaultFolder.Name = "opDefaultFolder";
			this.opDefaultFolder.TabStop = true;
			this.opDefaultFolder.UseVisualStyleBackColor = true;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DataFolderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.Shown += new global::System.EventHandler(this.DataFolderForm_Shown);
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::QuoterPlan.LabelEx lblNote;

		private global::System.Windows.Forms.RadioButton opAlternateFolder;

		private global::QuoterPlan.LabelEx lblNoteText;

		private global::QuoterPlan.TextBoxEx txtAlternateFolder;

		private global::System.Windows.Forms.RadioButton opDefaultFolder;

		private global::System.Windows.Forms.Button btSelectAlternateFolder;

		private global::QuoterPlan.LabelEx lblWarning;
	}
}
