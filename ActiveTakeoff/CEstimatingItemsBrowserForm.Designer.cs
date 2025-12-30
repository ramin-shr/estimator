namespace QuoterPlan
{
	public partial class CEstimatingItemsBrowserForm : global::QuoterPlan.BaseForm
	{
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(CEstimatingItemsBrowserForm));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");
            this.panelEx1 = new global::DevComponents.DotNetBar.PanelEx();
			this.treeSections = new global::DevExpress.XtraTreeList.TreeList();
			this.barCOffice = new global::DevComponents.DotNetBar.Bar();
			this.btItemsExpandAll = new global::DevComponents.DotNetBar.ButtonItem();
			this.btItemsCollapseAll = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblSpacer = new global::DevComponents.DotNetBar.LabelItem();
			this.lblSelect = new global::DevComponents.DotNetBar.LabelItem();
			this.btResidentialDatabase = new global::DevComponents.DotNetBar.ButtonItem();
			this.btCommercialDatabase = new global::DevComponents.DotNetBar.ButtonItem();
			this.panelEx2 = new global::DevComponents.DotNetBar.PanelEx();
			this.panelEx4 = new global::DevComponents.DotNetBar.PanelEx();
			this.btSelect = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.panelEx3 = new global::DevComponents.DotNetBar.PanelEx();
			this.listItems = new global::DevExpress.XtraTreeList.TreeList();
			this.expandableSplitter1 = new global::DevComponents.DotNetBar.ExpandableSplitter();
			this.panelEx1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.treeSections).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.barCOffice).BeginInit();
			this.panelEx2.SuspendLayout();
			this.panelEx4.SuspendLayout();
			this.panelEx3.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.listItems).BeginInit();
			base.SuspendLayout();
			this.panelEx1.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.treeSections);
			resources.ApplyResources(this.panelEx1, "panelEx1");
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			resources.ApplyResources(this.treeSections, "treeSections");
			this.treeSections.LookAndFeel.SkinName = "Office 2010 Silver";
			this.treeSections.Name = "treeSections";
			this.treeSections.OptionsView.FocusRectStyle = global::DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
			this.treeSections.OptionsView.ShowHorzLines = false;
			this.treeSections.OptionsView.ShowIndicator = false;
			this.treeSections.OptionsView.ShowVertLines = false;
			this.barCOffice.CanAutoHide = false;
			this.barCOffice.CanCustomize = false;
			this.barCOffice.CanDockBottom = false;
			this.barCOffice.CanDockLeft = false;
			this.barCOffice.CanDockRight = false;
			this.barCOffice.CanDockTab = false;
			this.barCOffice.CanDockTop = false;
			this.barCOffice.CanMove = false;
			this.barCOffice.CanReorderTabs = false;
			this.barCOffice.CanUndock = false;
			this.barCOffice.ColorScheme.PredefinedColorScheme = global::DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
			resources.ApplyResources(this.barCOffice, "barCOffice");
			this.barCOffice.DockTabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Left;
			this.barCOffice.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btItemsExpandAll,
				this.btItemsCollapseAll,
				this.lblSpacer,
				this.lblSelect,
				this.btResidentialDatabase,
				this.btCommercialDatabase
			});
			this.barCOffice.Name = "barCOffice";
			this.barCOffice.RoundCorners = false;
			this.barCOffice.SaveLayoutChanges = false;
			this.barCOffice.Stretch = true;
			this.barCOffice.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barCOffice.TabStop = false;
			this.btItemsExpandAll.Image = (global::System.Drawing.Image)resources.GetObject("btItemsExpandAll.Image");
			this.btItemsExpandAll.Name = "btItemsExpandAll";
			this.btItemsExpandAll.Click += new global::System.EventHandler(this.btItemsExpandAll_Click);
			this.btItemsCollapseAll.Image = (global::System.Drawing.Image)resources.GetObject("btItemsCollapseAll.Image");
			this.btItemsCollapseAll.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btItemsCollapseAll.Name = "btItemsCollapseAll";
			this.btItemsCollapseAll.Click += new global::System.EventHandler(this.btItemsCollapseAll_Click);
			this.lblSpacer.BeginGroup = true;
			this.lblSpacer.Name = "lblSpacer";
			this.lblSpacer.Width = 6;
			this.lblSelect.Name = "lblSelect";
			resources.ApplyResources(this.lblSelect, "lblSelect");
			resources.ApplyResources(this.btResidentialDatabase, "btResidentialDatabase");
			this.btResidentialDatabase.Checked = true;
			this.btResidentialDatabase.Name = "btResidentialDatabase";
			this.btResidentialDatabase.Click += new global::System.EventHandler(this.btResidentialDatabase_Click);
			resources.ApplyResources(this.btCommercialDatabase, "btCommercialDatabase");
			this.btCommercialDatabase.Name = "btCommercialDatabase";
			this.btCommercialDatabase.Click += new global::System.EventHandler(this.btCommercialDatabase_Click);
			this.panelEx2.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.panelEx4);
			this.panelEx2.Controls.Add(this.panelEx3);
			resources.ApplyResources(this.panelEx2, "panelEx2");
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.BackColor2.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx2.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx2.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx4.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelEx4.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx4.Controls.Add(this.btSelect);
			this.panelEx4.Controls.Add(this.btCancel);
			resources.ApplyResources(this.panelEx4, "panelEx4");
			this.panelEx4.Name = "panelEx4";
			this.panelEx4.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelEx4.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx4.Style.BackColor2.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx4.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx4.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx4.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx4.Style.GradientAngle = 90;
			resources.ApplyResources(this.btSelect, "btSelect");
			this.btSelect.Name = "btSelect";
			this.btSelect.UseVisualStyleBackColor = true;
			this.btSelect.Click += new global::System.EventHandler(this.btSelect_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			this.panelEx3.CanvasColor = global::System.Drawing.Color.Transparent;
			this.panelEx3.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx3.Controls.Add(this.listItems);
			resources.ApplyResources(this.panelEx3, "panelEx3");
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.BackColor2.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx3.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx3.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			resources.ApplyResources(this.listItems, "listItems");
			this.listItems.LookAndFeel.SkinName = "Office 2010 Silver";
			this.listItems.Name = "listItems";
			this.listItems.OptionsView.FocusRectStyle = global::DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
			this.listItems.OptionsView.ShowHorzLines = false;
			this.listItems.OptionsView.ShowIndicator = false;
			this.listItems.OptionsView.ShowVertLines = false;
			this.expandableSplitter1.BackColor2 = global::System.Drawing.Color.FromArgb(101, 147, 207);
			this.expandableSplitter1.BackColor2SchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.BackColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter1.Cursor = global::System.Windows.Forms.Cursors.HSplit;
			resources.ApplyResources(this.expandableSplitter1, "expandableSplitter1");
			this.expandableSplitter1.ExpandFillColor = global::System.Drawing.Color.FromArgb(101, 147, 207);
			this.expandableSplitter1.ExpandFillColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.ExpandLineColor = global::System.Drawing.Color.FromArgb(0, 0, 0);
			this.expandableSplitter1.ExpandLineColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripDarkColor = global::System.Drawing.Color.FromArgb(0, 0, 0);
			this.expandableSplitter1.GripDarkColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripLightColor = global::System.Drawing.Color.FromArgb(227, 239, 255);
			this.expandableSplitter1.GripLightColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.HotBackColor = global::System.Drawing.Color.FromArgb(252, 151, 61);
			this.expandableSplitter1.HotBackColor2 = global::System.Drawing.Color.FromArgb(255, 184, 94);
			this.expandableSplitter1.HotBackColor2SchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter1.HotBackColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter1.HotExpandFillColor = global::System.Drawing.Color.FromArgb(101, 147, 207);
			this.expandableSplitter1.HotExpandFillColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotExpandLineColor = global::System.Drawing.Color.FromArgb(0, 0, 0);
			this.expandableSplitter1.HotExpandLineColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.HotGripDarkColor = global::System.Drawing.Color.FromArgb(101, 147, 207);
			this.expandableSplitter1.HotGripDarkColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotGripLightColor = global::System.Drawing.Color.FromArgb(227, 239, 255);
			this.expandableSplitter1.HotGripLightColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Style = global::DevComponents.DotNetBar.eSplitterStyle.Office2007;
			this.expandableSplitter1.TabStop = false;
			this.expandableSplitter1.SplitterMoving += new global::System.Windows.Forms.SplitterEventHandler(this.expandableSplitter1_SplitterMoving);
			this.expandableSplitter1.SplitterMoved += new global::System.Windows.Forms.SplitterEventHandler(this.expandableSplitter1_SplitterMoved);
			base.AcceptButton = this.btSelect;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.panelEx2);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.panelEx1);
			base.Controls.Add(this.barCOffice);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CEstimatingItemsBrowserForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.Activated += new global::System.EventHandler(this.CEstimatingItemsBrowserForm_Activated);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.CEstimatingItemsBrowserForm_FormClosing);
			base.Resize += new global::System.EventHandler(this.CEstimatingItemsBrowserForm_Resize);
			this.panelEx1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.treeSections).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.barCOffice).EndInit();
			this.panelEx2.ResumeLayout(false);
			this.panelEx4.ResumeLayout(false);
			this.panelEx3.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.listItems).EndInit();
			base.ResumeLayout(false);
		}

		private DevComponents.DotNetBar.PanelEx panelEx1;

		private DevComponents.DotNetBar.PanelEx panelEx2;

		private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;

		private DevExpress.XtraTreeList.TreeList treeSections;

		private DevExpress.XtraTreeList.TreeList listItems;

		private DevComponents.DotNetBar.Bar barCOffice;

		private DevComponents.DotNetBar.ButtonItem btItemsExpandAll;

		private DevComponents.DotNetBar.ButtonItem btItemsCollapseAll;

		private System.Windows.Forms.Button btSelect;

		private System.Windows.Forms.Button btCancel;

		private DevComponents.DotNetBar.PanelEx panelEx3;

		private DevComponents.DotNetBar.PanelEx panelEx4;

		private DevComponents.DotNetBar.ButtonItem btResidentialDatabase;

		private DevComponents.DotNetBar.ButtonItem btCommercialDatabase;

		private DevComponents.DotNetBar.LabelItem lblSpacer;

		private DevComponents.DotNetBar.LabelItem lblSelect;
	}
}
