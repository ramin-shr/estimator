namespace QuoterPlan
{
	public partial class PresetEditForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.PresetEditForm));
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.txtDisplayName = new global::QuoterPlan.TextBoxEx();
			this.lblDisplayName = new global::QuoterPlan.LabelEx();
			this.hiddenLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.lblSelection = new global::QuoterPlan.LabelEx();
			this.lblSeparator = new global::System.Windows.Forms.Label();
			this.cbExtensions = new global::QuoterPlan.ComboBoxEx();
			this.lblUsage = new global::QuoterPlan.LabelEx();
			this.cbCategories = new global::QuoterPlan.ComboBoxEx();
			this.lblCategory = new global::QuoterPlan.LabelEx();
			this.tableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
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
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.txtDisplayName);
			this.panelGroup.Controls.Add(this.lblDisplayName);
			this.panelGroup.Controls.Add(this.hiddenLayoutPanel);
			this.panelGroup.Controls.Add(this.lblSelection);
			this.panelGroup.Controls.Add(this.lblSeparator);
			this.panelGroup.Controls.Add(this.cbExtensions);
			this.panelGroup.Controls.Add(this.lblUsage);
			this.panelGroup.Controls.Add(this.cbCategories);
			this.panelGroup.Controls.Add(this.lblCategory);
			this.panelGroup.Controls.Add(this.tableLayoutPanel);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			this.txtDisplayName.AssociatedLabel = null;
			componentResourceManager.ApplyResources(this.txtDisplayName, "txtDisplayName");
			this.txtDisplayName.Name = "txtDisplayName";
			this.txtDisplayName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtDisplayName.Enter += new global::System.EventHandler(this.txtDisplayName_Enter);
			this.txtDisplayName.Validating += new global::System.ComponentModel.CancelEventHandler(this.txtDisplayName_Validating);
			componentResourceManager.ApplyResources(this.lblDisplayName, "lblDisplayName");
			this.lblDisplayName.BackColor = global::System.Drawing.Color.Transparent;
			this.lblDisplayName.ForeColor = global::System.Drawing.Color.Black;
			this.lblDisplayName.Name = "lblDisplayName";
			this.lblDisplayName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.hiddenLayoutPanel, "hiddenLayoutPanel");
			this.hiddenLayoutPanel.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.hiddenLayoutPanel.Name = "hiddenLayoutPanel";
			this.lblSelection.BackColor = global::System.Drawing.Color.Transparent;
			componentResourceManager.ApplyResources(this.lblSelection, "lblSelection");
			this.lblSelection.Name = "lblSelection";
			this.lblSelection.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblSeparator.BackColor = global::System.Drawing.SystemColors.Control;
			this.lblSeparator.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			componentResourceManager.ApplyResources(this.lblSeparator, "lblSeparator");
			this.lblSeparator.Name = "lblSeparator";
			this.cbExtensions.AssociatedLabel = this.lblUsage;
			this.cbExtensions.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbExtensions.FormattingEnabled = true;
			this.cbExtensions.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cbExtensions.Items")
			});
			componentResourceManager.ApplyResources(this.cbExtensions, "cbExtensions");
			this.cbExtensions.Name = "cbExtensions";
			this.cbExtensions.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbExtensions.SelectedIndexChanged += new global::System.EventHandler(this.cbTemplates_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.lblUsage, "lblUsage");
			this.lblUsage.BackColor = global::System.Drawing.Color.Transparent;
			this.lblUsage.ForeColor = global::System.Drawing.Color.Black;
			this.lblUsage.Name = "lblUsage";
			this.lblUsage.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.cbCategories.AssociatedLabel = this.lblCategory;
			this.cbCategories.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCategories.FormattingEnabled = true;
			this.cbCategories.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cbCategories.Items"),
				componentResourceManager.GetString("cbCategories.Items1"),
				componentResourceManager.GetString("cbCategories.Items2")
			});
			componentResourceManager.ApplyResources(this.cbCategories, "cbCategories");
			this.cbCategories.Name = "cbCategories";
			this.cbCategories.Sorted = true;
			this.cbCategories.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbCategories.SelectedIndexChanged += new global::System.EventHandler(this.cbCategories_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.lblCategory, "lblCategory");
			this.lblCategory.BackColor = global::System.Drawing.Color.Transparent;
			this.lblCategory.ForeColor = global::System.Drawing.Color.Black;
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
			this.tableLayoutPanel.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			base.AcceptButton = this.btOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PresetEditForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.Label lblSeparator;

		private global::QuoterPlan.ComboBoxEx cbExtensions;

		private global::QuoterPlan.LabelEx lblUsage;

		private global::QuoterPlan.ComboBoxEx cbCategories;

		private global::QuoterPlan.LabelEx lblCategory;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.Button btOk;

		private global::QuoterPlan.LabelEx lblSelection;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

		private global::System.Windows.Forms.TableLayoutPanel hiddenLayoutPanel;

		private global::QuoterPlan.LabelEx lblDisplayName;

		private global::QuoterPlan.TextBoxEx txtDisplayName;
	}
}
