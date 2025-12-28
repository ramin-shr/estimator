namespace QuoterPlan
{
	public partial class ReportEditForm : global::QuoterPlan.BaseForm
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
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.ReportEditForm));
			this.smallImages = new global::System.Windows.Forms.ImageList(this.components);
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.btSelectNone = new global::QuoterPlan.ButtonEx();
			this.btSelectAll = new global::QuoterPlan.ButtonEx();
			this.lblSeparator = new global::System.Windows.Forms.Label();
			this.triStateTreeView = new global::QuoterPlan.TriStateTreeViewComponent();
			this.lblOrder = new global::QuoterPlan.LabelEx();
			this.opOrderByObjects = new global::System.Windows.Forms.RadioButton();
			this.opOrderByPlans = new global::System.Windows.Forms.RadioButton();
			this.opOrderBySections = new global::System.Windows.Forms.RadioButton();
			this.opOrderByItemTypes = new global::System.Windows.Forms.RadioButton();
			this.opOrderByLayers = new global::System.Windows.Forms.RadioButton();
			this.opOrderByList = new global::System.Windows.Forms.RadioButton();
			this.panFilterDisabled = new global::System.Windows.Forms.Panel();
			this.lblFilterDisabled = new global::QuoterPlan.LabelEx();
			this.btEnableFilters = new global::QuoterPlan.ButtonEx();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			this.panFilterDisabled.SuspendLayout();
			base.SuspendLayout();
			this.smallImages.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("smallImages.ImageStream");
			this.smallImages.TransparentColor = global::System.Drawing.Color.Transparent;
			this.smallImages.Images.SetKeyName(0, "Plan_wide.png");
			this.smallImages.Images.SetKeyName(1, "Layer_wide.png");
			this.smallImages.Images.SetKeyName(2, "Area_wide.png");
			this.smallImages.Images.SetKeyName(3, "Perimeter_wide.png");
			this.smallImages.Images.SetKeyName(4, "Counter_wide.png");
			this.smallImages.Images.SetKeyName(5, "Distance_wide.png");
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Controls.Add(this.btSelectNone);
			this.panelGroup.Controls.Add(this.btSelectAll);
			this.panelGroup.Controls.Add(this.lblSeparator);
			this.panelGroup.Controls.Add(this.triStateTreeView);
			this.panelGroup.Controls.Add(this.lblOrder);
			this.panelGroup.Controls.Add(this.opOrderByObjects);
			this.panelGroup.Controls.Add(this.opOrderByPlans);
			this.panelGroup.Controls.Add(this.opOrderBySections);
			this.panelGroup.Controls.Add(this.opOrderByItemTypes);
			this.panelGroup.Controls.Add(this.opOrderByLayers);
			this.panelGroup.Controls.Add(this.opOrderByList);
			this.panelGroup.Name = "panelGroup";
			componentResourceManager.ApplyResources(this.btSelectNone, "btSelectNone");
			this.btSelectNone.Name = "btSelectNone";
			this.btSelectNone.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btSelectNone.UseVisualStyleBackColor = true;
			this.btSelectNone.Click += new global::System.EventHandler(this.btSelectNone_Click);
			componentResourceManager.ApplyResources(this.btSelectAll, "btSelectAll");
			this.btSelectAll.Name = "btSelectAll";
			this.btSelectAll.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btSelectAll.UseVisualStyleBackColor = true;
			this.btSelectAll.Click += new global::System.EventHandler(this.btSelectAll_Click);
			this.lblSeparator.BackColor = global::System.Drawing.SystemColors.Control;
			this.lblSeparator.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			componentResourceManager.ApplyResources(this.lblSeparator, "lblSeparator");
			this.lblSeparator.Name = "lblSeparator";
			componentResourceManager.ApplyResources(this.triStateTreeView, "triStateTreeView");
			this.triStateTreeView.Name = "triStateTreeView";
			this.triStateTreeView.TriStateStyleProperty = global::QuoterPlan.TriStateTreeViewComponent.TriStateStyles.Standard;
			this.triStateTreeView.AfterCheck += new global::System.Windows.Forms.TreeViewEventHandler(this.triStateTreeView_AfterCheck);
			componentResourceManager.ApplyResources(this.lblOrder, "lblOrder");
			this.lblOrder.ForeColor = global::System.Drawing.Color.Black;
			this.lblOrder.Name = "lblOrder";
			this.lblOrder.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.opOrderByObjects, "opOrderByObjects");
			this.opOrderByObjects.Name = "opOrderByObjects";
			this.opOrderByObjects.TabStop = true;
			this.opOrderByObjects.UseVisualStyleBackColor = true;
			this.opOrderByObjects.CheckedChanged += new global::System.EventHandler(this.opOrderByObjects_CheckedChanged);
			componentResourceManager.ApplyResources(this.opOrderByPlans, "opOrderByPlans");
			this.opOrderByPlans.Name = "opOrderByPlans";
			this.opOrderByPlans.TabStop = true;
			this.opOrderByPlans.UseVisualStyleBackColor = true;
			this.opOrderByPlans.CheckedChanged += new global::System.EventHandler(this.opOrderByPlans_CheckedChanged);
			componentResourceManager.ApplyResources(this.opOrderBySections, "opOrderBySections");
			this.opOrderBySections.Name = "opOrderBySections";
			this.opOrderBySections.TabStop = true;
			this.opOrderBySections.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.opOrderByItemTypes, "opOrderByItemTypes");
			this.opOrderByItemTypes.Name = "opOrderByItemTypes";
			this.opOrderByItemTypes.TabStop = true;
			this.opOrderByItemTypes.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.opOrderByLayers, "opOrderByLayers");
			this.opOrderByLayers.Name = "opOrderByLayers";
			this.opOrderByLayers.TabStop = true;
			this.opOrderByLayers.UseVisualStyleBackColor = true;
			this.opOrderByLayers.CheckedChanged += new global::System.EventHandler(this.opOrderByPlans_CheckedChanged);
			componentResourceManager.ApplyResources(this.opOrderByList, "opOrderByList");
			this.opOrderByList.Name = "opOrderByList";
			this.opOrderByList.TabStop = true;
			this.opOrderByList.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.panFilterDisabled, "panFilterDisabled");
			this.panFilterDisabled.Controls.Add(this.lblFilterDisabled);
			this.panFilterDisabled.Controls.Add(this.btEnableFilters);
			this.panFilterDisabled.Name = "panFilterDisabled";
			this.lblFilterDisabled.BackColor = global::System.Drawing.SystemColors.Window;
			componentResourceManager.ApplyResources(this.lblFilterDisabled, "lblFilterDisabled");
			this.lblFilterDisabled.Name = "lblFilterDisabled";
			this.lblFilterDisabled.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.btEnableFilters, "btEnableFilters");
			this.btEnableFilters.Name = "btEnableFilters";
			this.btEnableFilters.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btEnableFilters.UseVisualStyleBackColor = true;
			this.btEnableFilters.Click += new global::System.EventHandler(this.btEnableFilter_Click);
			base.AcceptButton = this.btOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.groupBox);
			base.Controls.Add(this.panFilterDisabled);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ReportEditForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.Activated += new global::System.EventHandler(this.ReportEditForm_Activated);
			base.Resize += new global::System.EventHandler(this.ReportEditForm_Resize);
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panFilterDisabled.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::QuoterPlan.TriStateTreeViewComponent triStateTreeView;

		private global::System.Windows.Forms.ImageList smallImages;

		private global::System.Windows.Forms.RadioButton opOrderByObjects;

		private global::System.Windows.Forms.RadioButton opOrderByPlans;

		private global::QuoterPlan.LabelEx lblOrder;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::System.Windows.Forms.RadioButton opOrderByLayers;

		private global::System.Windows.Forms.RadioButton opOrderByList;

		private global::System.Windows.Forms.RadioButton opOrderByItemTypes;

		private global::System.Windows.Forms.RadioButton opOrderBySections;

		private global::System.Windows.Forms.Label lblSeparator;

		private global::QuoterPlan.ButtonEx btSelectNone;

		private global::QuoterPlan.ButtonEx btSelectAll;

		private global::System.Windows.Forms.Panel panFilterDisabled;

		private global::QuoterPlan.LabelEx lblFilterDisabled;

		private global::QuoterPlan.ButtonEx btEnableFilters;
	}
}
