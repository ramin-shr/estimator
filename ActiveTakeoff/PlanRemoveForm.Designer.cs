namespace QuoterPlan
{
	public partial class PlanRemoveForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.PlanRemoveForm));
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.btRemoveWithData = new global::System.Windows.Forms.Button();
			this.btRemove = new global::System.Windows.Forms.Button();
			this.txtPlanSource = new global::QuoterPlan.TextBoxEx();
			this.thumbnailPanel = new global::QuoterPlan.ThumbnailPanel();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			base.SuspendLayout();
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.btRemoveWithData);
			this.panelGroup.Controls.Add(this.btRemove);
			this.panelGroup.Controls.Add(this.btCancel);
			this.panelGroup.Controls.Add(this.txtPlanSource);
			this.panelGroup.Controls.Add(this.thumbnailPanel);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.btRemoveWithData, "btRemoveWithData");
			this.btRemoveWithData.Name = "btRemoveWithData";
			this.btRemoveWithData.UseVisualStyleBackColor = true;
			this.btRemoveWithData.Click += new global::System.EventHandler(this.btRemoveWithData_Click);
			componentResourceManager.ApplyResources(this.btRemove, "btRemove");
			this.btRemove.Name = "btRemove";
			this.btRemove.UseVisualStyleBackColor = true;
			this.btRemove.Click += new global::System.EventHandler(this.btRemove_Click);
			this.txtPlanSource.AssociatedLabel = null;
			componentResourceManager.ApplyResources(this.txtPlanSource, "txtPlanSource");
			this.txtPlanSource.Name = "txtPlanSource";
			this.txtPlanSource.ReadOnly = true;
			this.txtPlanSource.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.txtPlanSource.Enter += new global::System.EventHandler(this.txtField_Enter);
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
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PlanRemoveForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::QuoterPlan.TextBoxEx txtPlanSource;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::QuoterPlan.ThumbnailPanel thumbnailPanel;

		private global::System.Windows.Forms.Button btRemoveWithData;

		private global::System.Windows.Forms.Button btRemove;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.Panel panelGroup;
	}
}
