namespace QuoterPlan
{
	public partial class PlanPropertiesForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.PlanPropertiesForm));
			this.btCancel = new global::System.Windows.Forms.Button();
			this.btOk = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.btNext = new global::System.Windows.Forms.Button();
			this.txtComment = new global::QuoterPlan.TextBoxEx();
			this.lblComment = new global::QuoterPlan.LabelEx();
			this.btPrevious = new global::System.Windows.Forms.Button();
			this.txtPlanSource = new global::QuoterPlan.TextBoxEx();
			this.lblPlanSource = new global::QuoterPlan.LabelEx();
			this.txtPlanName = new global::QuoterPlan.TextBoxEx();
			this.lblPlanName = new global::QuoterPlan.LabelEx();
			this.thumbnailPanel = new global::QuoterPlan.ThumbnailPanel();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			base.SuspendLayout();
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.btNext);
			this.panelGroup.Controls.Add(this.txtComment);
			this.panelGroup.Controls.Add(this.lblComment);
			this.panelGroup.Controls.Add(this.btPrevious);
			this.panelGroup.Controls.Add(this.txtPlanSource);
			this.panelGroup.Controls.Add(this.lblPlanSource);
			this.panelGroup.Controls.Add(this.txtPlanName);
			this.panelGroup.Controls.Add(this.lblPlanName);
			this.panelGroup.Controls.Add(this.thumbnailPanel);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.btNext, "btNext");
			this.btNext.Name = "btNext";
			this.btNext.UseVisualStyleBackColor = true;
			this.btNext.Click += new global::System.EventHandler(this.btNext_Click);
			this.txtComment.AssociatedLabel = this.lblComment;
			componentResourceManager.ApplyResources(this.txtComment, "txtComment");
			this.txtComment.Name = "txtComment";
			this.txtComment.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.txtComment.Enter += new global::System.EventHandler(this.txtMultiLine_Enter);
			componentResourceManager.ApplyResources(this.lblComment, "lblComment");
			this.lblComment.ForeColor = global::System.Drawing.Color.Black;
			this.lblComment.Name = "lblComment";
			this.lblComment.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.btPrevious, "btPrevious");
			this.btPrevious.Name = "btPrevious";
			this.btPrevious.UseVisualStyleBackColor = true;
			this.btPrevious.Click += new global::System.EventHandler(this.btPrevious_Click);
			this.txtPlanSource.AssociatedLabel = this.lblPlanSource;
			componentResourceManager.ApplyResources(this.txtPlanSource, "txtPlanSource");
			this.txtPlanSource.Name = "txtPlanSource";
			this.txtPlanSource.ReadOnly = true;
			this.txtPlanSource.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.txtPlanSource.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtPlanSource.Enter += new global::System.EventHandler(this.txtField_Enter);
			componentResourceManager.ApplyResources(this.lblPlanSource, "lblPlanSource");
			this.lblPlanSource.ForeColor = global::System.Drawing.Color.Black;
			this.lblPlanSource.Name = "lblPlanSource";
			this.lblPlanSource.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblPlanSource.Click += new global::System.EventHandler(this.lblPlanSource_Click);
			this.txtPlanName.AssociatedLabel = this.lblPlanName;
			componentResourceManager.ApplyResources(this.txtPlanName, "txtPlanName");
			this.txtPlanName.Name = "txtPlanName";
			this.txtPlanName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.txtPlanName.Click += new global::System.EventHandler(this.txtField_Enter);
			this.txtPlanName.Enter += new global::System.EventHandler(this.txtField_Enter);
			this.txtPlanName.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.txtPlanName_KeyPress);
			componentResourceManager.ApplyResources(this.lblPlanName, "lblPlanName");
			this.lblPlanName.ForeColor = global::System.Drawing.Color.Black;
			this.lblPlanName.Name = "lblPlanName";
			this.lblPlanName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblPlanName.Click += new global::System.EventHandler(this.lblPlanName_Click);
			componentResourceManager.ApplyResources(this.thumbnailPanel, "thumbnailPanel");
			this.thumbnailPanel.BackColor = global::System.Drawing.Color.Transparent;
			this.thumbnailPanel.DisplayLayers = true;
			this.thumbnailPanel.DisplayName = true;
			this.thumbnailPanel.DisplayShadow = true;
			this.thumbnailPanel.DrawArea = null;
			this.thumbnailPanel.FlatAppearance.BorderSize = 0;
			this.thumbnailPanel.FlatAppearance.CheckedBackColor = global::System.Drawing.Color.Transparent;
			this.thumbnailPanel.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Transparent;
			this.thumbnailPanel.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.thumbnailPanel.Name = "thumbnailPanel";
			this.thumbnailPanel.Plan = null;
			this.thumbnailPanel.Selected = false;
			this.thumbnailPanel.TabStop = false;
			this.thumbnailPanel.ThumbnailMarging = new global::System.Windows.Forms.Padding(0);
			this.thumbnailPanel.ThumbnailPadding = new global::System.Windows.Forms.Padding(0);
			this.thumbnailPanel.ThumbnailSize = new global::System.Drawing.Size(0, 0);
			this.thumbnailPanel.UseVisualStyleBackColor = false;
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
			base.Name = "PlanPropertiesForm";
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

		private global::QuoterPlan.TextBoxEx txtComment;

		private global::QuoterPlan.LabelEx lblComment;

		private global::QuoterPlan.TextBoxEx txtPlanSource;

		private global::QuoterPlan.LabelEx lblPlanSource;

		private global::QuoterPlan.TextBoxEx txtPlanName;

		private global::QuoterPlan.LabelEx lblPlanName;

		private global::System.Windows.Forms.Button btNext;

		private global::System.Windows.Forms.Button btPrevious;

		private global::QuoterPlan.ThumbnailPanel thumbnailPanel;

		private global::System.Windows.Forms.Panel panelGroup;
	}
}
