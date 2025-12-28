namespace QuoterPlan
{
	public partial class ProjectForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.ProjectForm));
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.lblLastModified = new global::QuoterPlan.LabelEx();
			this.txtLastModified = new global::QuoterPlan.TextBoxEx();
			this.lblCreationDate = new global::QuoterPlan.LabelEx();
			this.txtCreationDate = new global::QuoterPlan.TextBoxEx();
			this.lbJobNumber = new global::QuoterPlan.LabelEx();
			this.txtJobNumber = new global::QuoterPlan.TextBoxEx();
			this.btProjectFolder = new global::System.Windows.Forms.Button();
			this.txtProjectFolder = new global::QuoterPlan.TextBoxEx();
			this.lblProjectFolder = new global::QuoterPlan.LabelEx();
			this.txtComment = new global::QuoterPlan.TextBoxEx();
			this.lblComment = new global::QuoterPlan.LabelEx();
			this.lblProjectName = new global::QuoterPlan.LabelEx();
			this.txtContactInfo = new global::QuoterPlan.TextBoxEx();
			this.lblContactInfo = new global::QuoterPlan.LabelEx();
			this.txtProjectDescription = new global::QuoterPlan.TextBoxEx();
			this.lblProjectDescription = new global::QuoterPlan.LabelEx();
			this.txtContactName = new global::QuoterPlan.TextBoxEx();
			this.lblContactName = new global::QuoterPlan.LabelEx();
			this.txtProjectName = new global::QuoterPlan.TextBoxEx();
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
			this.panelGroup.Controls.Add(this.lblLastModified);
			this.panelGroup.Controls.Add(this.txtLastModified);
			this.panelGroup.Controls.Add(this.lblCreationDate);
			this.panelGroup.Controls.Add(this.txtCreationDate);
			this.panelGroup.Controls.Add(this.lbJobNumber);
			this.panelGroup.Controls.Add(this.txtJobNumber);
			this.panelGroup.Controls.Add(this.btProjectFolder);
			this.panelGroup.Controls.Add(this.txtProjectFolder);
			this.panelGroup.Controls.Add(this.lblProjectFolder);
			this.panelGroup.Controls.Add(this.txtComment);
			this.panelGroup.Controls.Add(this.lblProjectName);
			this.panelGroup.Controls.Add(this.lblComment);
			this.panelGroup.Controls.Add(this.txtContactInfo);
			this.panelGroup.Controls.Add(this.lblContactInfo);
			this.panelGroup.Controls.Add(this.txtProjectDescription);
			this.panelGroup.Controls.Add(this.lblProjectDescription);
			this.panelGroup.Controls.Add(this.txtContactName);
			this.panelGroup.Controls.Add(this.lblContactName);
			this.panelGroup.Controls.Add(this.txtProjectName);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.lblLastModified, "lblLastModified");
			this.lblLastModified.ForeColor = global::System.Drawing.Color.Black;
			this.lblLastModified.Name = "lblLastModified";
			this.lblLastModified.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtLastModified.AssociatedLabel = this.lblLastModified;
			componentResourceManager.ApplyResources(this.txtLastModified, "txtLastModified");
			this.txtLastModified.Name = "txtLastModified";
			this.txtLastModified.ReadOnly = true;
			this.txtLastModified.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtLastModified.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lblCreationDate, "lblCreationDate");
			this.lblCreationDate.ForeColor = global::System.Drawing.Color.Black;
			this.lblCreationDate.Name = "lblCreationDate";
			this.lblCreationDate.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCreationDate.AssociatedLabel = this.lblCreationDate;
			componentResourceManager.ApplyResources(this.txtCreationDate, "txtCreationDate");
			this.txtCreationDate.Name = "txtCreationDate";
			this.txtCreationDate.ReadOnly = true;
			this.txtCreationDate.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtCreationDate.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lbJobNumber, "lbJobNumber");
			this.lbJobNumber.ForeColor = global::System.Drawing.Color.Black;
			this.lbJobNumber.Name = "lbJobNumber";
			this.lbJobNumber.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtJobNumber.AssociatedLabel = this.lbJobNumber;
			componentResourceManager.ApplyResources(this.txtJobNumber, "txtJobNumber");
			this.txtJobNumber.Name = "txtJobNumber";
			this.txtJobNumber.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtJobNumber.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.btProjectFolder, "btProjectFolder");
			this.btProjectFolder.Name = "btProjectFolder";
			this.btProjectFolder.UseVisualStyleBackColor = true;
			this.btProjectFolder.Click += new global::System.EventHandler(this.btProjectFolder_Click);
			this.txtProjectFolder.AssociatedLabel = this.lblProjectFolder;
			componentResourceManager.ApplyResources(this.txtProjectFolder, "txtProjectFolder");
			this.txtProjectFolder.Name = "txtProjectFolder";
			this.txtProjectFolder.ReadOnly = true;
			this.txtProjectFolder.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProjectFolder.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lblProjectFolder, "lblProjectFolder");
			this.lblProjectFolder.ForeColor = global::System.Drawing.Color.Black;
			this.lblProjectFolder.Name = "lblProjectFolder";
			this.lblProjectFolder.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtComment.AssociatedLabel = this.lblComment;
			componentResourceManager.ApplyResources(this.txtComment, "txtComment");
			this.txtComment.Name = "txtComment";
			this.txtComment.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtComment.Enter += new global::System.EventHandler(this.txtMultiLine_Enter);
			componentResourceManager.ApplyResources(this.lblComment, "lblComment");
			this.lblComment.ForeColor = global::System.Drawing.Color.Black;
			this.lblComment.Name = "lblComment";
			this.lblComment.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblProjectName, "lblProjectName");
			this.lblProjectName.ForeColor = global::System.Drawing.Color.Black;
			this.lblProjectName.Name = "lblProjectName";
			this.lblProjectName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtContactInfo.AssociatedLabel = this.lblContactInfo;
			componentResourceManager.ApplyResources(this.txtContactInfo, "txtContactInfo");
			this.txtContactInfo.Name = "txtContactInfo";
			this.txtContactInfo.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtContactInfo.Enter += new global::System.EventHandler(this.txtMultiLine_Enter);
			componentResourceManager.ApplyResources(this.lblContactInfo, "lblContactInfo");
			this.lblContactInfo.ForeColor = global::System.Drawing.Color.Black;
			this.lblContactInfo.Name = "lblContactInfo";
			this.lblContactInfo.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProjectDescription.AssociatedLabel = this.lblProjectDescription;
			componentResourceManager.ApplyResources(this.txtProjectDescription, "txtProjectDescription");
			this.txtProjectDescription.Name = "txtProjectDescription";
			this.txtProjectDescription.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProjectDescription.Enter += new global::System.EventHandler(this.txtMultiLine_Enter);
			componentResourceManager.ApplyResources(this.lblProjectDescription, "lblProjectDescription");
			this.lblProjectDescription.ForeColor = global::System.Drawing.Color.Black;
			this.lblProjectDescription.Name = "lblProjectDescription";
			this.lblProjectDescription.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtContactName.AssociatedLabel = this.lblContactName;
			componentResourceManager.ApplyResources(this.txtContactName, "txtContactName");
			this.txtContactName.Name = "txtContactName";
			this.txtContactName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtContactName.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lblContactName, "lblContactName");
			this.lblContactName.ForeColor = global::System.Drawing.Color.Black;
			this.lblContactName.Name = "lblContactName";
			this.lblContactName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProjectName.AssociatedLabel = this.lblProjectName;
			componentResourceManager.ApplyResources(this.txtProjectName, "txtProjectName");
			this.txtProjectName.Name = "txtProjectName";
			this.txtProjectName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtProjectName.TextChanged += new global::System.EventHandler(this.txtProjectName_TextChanged);
			this.txtProjectName.Enter += new global::System.EventHandler(this.txtField_Enter);
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
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProjectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::QuoterPlan.TextBoxEx txtComment;

		private global::QuoterPlan.LabelEx lblComment;

		private global::QuoterPlan.TextBoxEx txtContactInfo;

		private global::QuoterPlan.LabelEx lblContactInfo;

		private global::QuoterPlan.TextBoxEx txtProjectDescription;

		private global::QuoterPlan.LabelEx lblProjectDescription;

		private global::QuoterPlan.TextBoxEx txtContactName;

		private global::QuoterPlan.LabelEx lblContactName;

		private global::QuoterPlan.TextBoxEx txtProjectName;

		private global::QuoterPlan.LabelEx lblProjectName;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::QuoterPlan.TextBoxEx txtProjectFolder;

		private global::QuoterPlan.LabelEx lblProjectFolder;

		private global::QuoterPlan.LabelEx lblCreationDate;

		private global::QuoterPlan.TextBoxEx txtCreationDate;

		private global::QuoterPlan.LabelEx lbJobNumber;

		private global::QuoterPlan.TextBoxEx txtJobNumber;

		private global::System.Windows.Forms.Button btProjectFolder;

		private global::QuoterPlan.LabelEx lblLastModified;

		private global::QuoterPlan.TextBoxEx txtLastModified;
	}
}
