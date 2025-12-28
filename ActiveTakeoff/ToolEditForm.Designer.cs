namespace QuoterPlan
{
	public partial class ToolEditForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.ToolEditForm));
			this.galleryContainer2 = new global::DevComponents.DotNetBar.GalleryContainer();
			this.labelItem1 = new global::DevComponents.DotNetBar.LabelItem();
			this.superTabItem2 = new global::DevComponents.DotNetBar.SuperTabItem();
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.btDeleteTemplate = new global::System.Windows.Forms.Button();
			this.chkSaveAsTemplate = new global::System.Windows.Forms.CheckBox();
			this.panelColor = new global::System.Windows.Forms.Panel();
			this.btMoreColors = new global::System.Windows.Forms.Button();
			this.cbColor = new global::QuoterPlan.ColorPickerCombo();
			this.lblToolColor = new global::QuoterPlan.LabelEx();
			this.lblToolName = new global::QuoterPlan.LabelEx();
			this.sliderPenWidth = new global::DevComponents.DotNetBar.Controls.Slider();
			this.lblPenWidth = new global::QuoterPlan.LabelEx();
			this.lblSlope = new global::QuoterPlan.LabelEx();
			this.tabControl = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.btEERevokeLink = new global::DevComponents.DotNetBar.ButtonX();
			this.btEELink = new global::DevComponents.DotNetBar.ButtonX();
			this.panelPreview = new global::System.Windows.Forms.Panel();
			this.picPreview = new global::System.Windows.Forms.PictureBox();
			this.flowLayoutPanel1 = new global::System.Windows.Forms.FlowLayoutPanel();
			this.tabPage2 = new global::System.Windows.Forms.TabPage();
			this.extensionsManager = new global::QuoterPlan.ExtensionsManager();
			this.tabPage3 = new global::System.Windows.Forms.TabPage();
			this.txtComment = new global::QuoterPlan.TextBoxEx();
			this.txtToolName = new global::QuoterPlan.TextBoxEx();
			this.chkApplyChangesToTemplate = new global::System.Windows.Forms.CheckBox();
			this.panelSlope = new global::System.Windows.Forms.Panel();
			this.txtSlope = new global::QuoterPlan.TextBoxEx();
			this.btSlope = new global::System.Windows.Forms.Button();
			this.txtText = new global::QuoterPlan.TextBoxEx();
			this.lblText = new global::QuoterPlan.LabelEx();
			this.sliderDefaultSize = new global::DevComponents.DotNetBar.Controls.Slider();
			this.lblDefaultSize = new global::QuoterPlan.LabelEx();
			this.lblShape = new global::QuoterPlan.LabelEx();
			this.cbShape = new global::QuoterPlan.ComboBoxEx();
			this.cbHatchStyle = new global::QuoterPlan.HatchStylePickerCombo();
			this.lblHatchStyle = new global::QuoterPlan.LabelEx();
			this.panelColor.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox.SuspendLayout();
			this.panelPreview.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picPreview).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.panelSlope.SuspendLayout();
			base.SuspendLayout();
			this.galleryContainer2.BackgroundStyle.Class = "RibbonFileMenuColumnTwoContainer";
			this.galleryContainer2.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryContainer2.EnableGalleryPopup = false;
			this.galleryContainer2.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.galleryContainer2.MinimumSize = new global::System.Drawing.Size(180, 240);
			this.galleryContainer2.MultiLine = false;
			this.galleryContainer2.Name = "galleryContainer2";
			this.galleryContainer2.PopupUsesStandardScrollbars = false;
			this.galleryContainer2.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.labelItem1.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Bottom;
			this.labelItem1.BorderType = global::DevComponents.DotNetBar.eBorderType.Etched;
			this.labelItem1.CanCustomize = false;
			this.labelItem1.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.labelItem1.Name = "labelItem1";
			this.superTabItem2.GlobalItem = false;
			this.superTabItem2.Name = "superTabItem2";
			componentResourceManager.ApplyResources(this.superTabItem2, "superTabItem2");
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			componentResourceManager.ApplyResources(this.btDeleteTemplate, "btDeleteTemplate");
			this.btDeleteTemplate.Name = "btDeleteTemplate";
			this.btDeleteTemplate.UseVisualStyleBackColor = true;
			this.btDeleteTemplate.Click += new global::System.EventHandler(this.btDeleteTemplate_Click);
			componentResourceManager.ApplyResources(this.chkSaveAsTemplate, "chkSaveAsTemplate");
			this.chkSaveAsTemplate.Name = "chkSaveAsTemplate";
			this.chkSaveAsTemplate.UseVisualStyleBackColor = true;
			this.panelColor.Controls.Add(this.btMoreColors);
			this.panelColor.Controls.Add(this.cbColor);
			componentResourceManager.ApplyResources(this.panelColor, "panelColor");
			this.panelColor.Name = "panelColor";
			componentResourceManager.ApplyResources(this.btMoreColors, "btMoreColors");
			this.btMoreColors.Name = "btMoreColors";
			this.btMoreColors.UseVisualStyleBackColor = true;
			this.btMoreColors.Click += new global::System.EventHandler(this.btMoreColors_Click);
			this.btMoreColors.Enter += new global::System.EventHandler(this.btControl_Enter);
			this.btMoreColors.Leave += new global::System.EventHandler(this.btControl_Leave);
			this.cbColor.DisableInternalDrawing = true;
			this.cbColor.DisplayMember = "Text";
			this.cbColor.DrawMode = global::System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbColor.DropDownHeight = 200;
			this.cbColor.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbColor, "cbColor");
			this.cbColor.FormattingEnabled = true;
			this.cbColor.Name = "cbColor";
			this.cbColor.SelectedValue = global::System.Drawing.Color.White;
			this.cbColor.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbColor.SelectedValueChanged += new global::System.EventHandler(this.cbColor_SelectedValueChanged);
			this.cbColor.Enter += new global::System.EventHandler(this.btControl_Enter);
			this.cbColor.Leave += new global::System.EventHandler(this.btControl_Leave);
			componentResourceManager.ApplyResources(this.lblToolColor, "lblToolColor");
			this.lblToolColor.BackColor = global::System.Drawing.Color.Transparent;
			this.lblToolColor.ForeColor = global::System.Drawing.Color.Black;
			this.lblToolColor.Name = "lblToolColor";
			this.lblToolColor.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblToolName, "lblToolName");
			this.lblToolName.BackColor = global::System.Drawing.Color.Transparent;
			this.lblToolName.ForeColor = global::System.Drawing.Color.Black;
			this.lblToolName.Name = "lblToolName";
			this.lblToolName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.sliderPenWidth.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.sliderPenWidth.LabelWidth = 20;
			componentResourceManager.ApplyResources(this.sliderPenWidth, "sliderPenWidth");
			this.sliderPenWidth.Maximum = 32;
			this.sliderPenWidth.Minimum = 1;
			this.sliderPenWidth.Name = "sliderPenWidth";
			this.sliderPenWidth.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.sliderPenWidth.Value = 6;
			this.sliderPenWidth.ValueChanged += new global::System.EventHandler(this.sliderPenWidth_ValueChanged);
			this.sliderPenWidth.Enter += new global::System.EventHandler(this.btControl_Enter);
			this.sliderPenWidth.Leave += new global::System.EventHandler(this.btControl_Leave);
			componentResourceManager.ApplyResources(this.lblPenWidth, "lblPenWidth");
			this.lblPenWidth.BackColor = global::System.Drawing.Color.Transparent;
			this.lblPenWidth.ForeColor = global::System.Drawing.Color.Black;
			this.lblPenWidth.Name = "lblPenWidth";
			this.lblPenWidth.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblSlope, "lblSlope");
			this.lblSlope.BackColor = global::System.Drawing.Color.Transparent;
			this.lblSlope.ForeColor = global::System.Drawing.Color.Black;
			this.lblSlope.Name = "lblSlope";
			this.lblSlope.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.HotTrack = true;
			componentResourceManager.ApplyResources(this.tabControl, "tabControl");
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.SelectedIndexChanged += new global::System.EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabPage1.Controls.Add(this.groupBox);
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Text = global::QuoterPlan.Properties.Resources.Propriétés;
			this.tabPage1.UseVisualStyleBackColor = true;
			this.groupBox.BackColor = global::System.Drawing.Color.Transparent;
			this.groupBox.Controls.Add(this.btEERevokeLink);
			this.groupBox.Controls.Add(this.btEELink);
			this.groupBox.Controls.Add(this.panelPreview);
			this.groupBox.Controls.Add(this.flowLayoutPanel1);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.btEERevokeLink.AccessibleRole = global::System.Windows.Forms.AccessibleRole.PushButton;
			this.btEERevokeLink.ColorTable = global::DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btEERevokeLink.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			componentResourceManager.ApplyResources(this.btEERevokeLink, "btEERevokeLink");
			this.btEERevokeLink.Name = "btEERevokeLink";
			this.btEERevokeLink.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btEELink.AccessibleRole = global::System.Windows.Forms.AccessibleRole.PushButton;
			this.btEELink.ColorTable = global::DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btEELink.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btEELink.Image");
			componentResourceManager.ApplyResources(this.btEELink, "btEELink");
			this.btEELink.Name = "btEELink";
			this.btEELink.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelPreview.BackColor = global::System.Drawing.Color.White;
			this.panelPreview.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelPreview.Controls.Add(this.picPreview);
			componentResourceManager.ApplyResources(this.panelPreview, "panelPreview");
			this.panelPreview.Name = "panelPreview";
			this.picPreview.BackColor = global::System.Drawing.Color.White;
			componentResourceManager.ApplyResources(this.picPreview, "picPreview");
			this.picPreview.Name = "picPreview";
			this.picPreview.TabStop = false;
			this.picPreview.Paint += new global::System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
			componentResourceManager.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.tabPage2.Controls.Add(this.extensionsManager);
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.extensionsManager.BackColor = global::System.Drawing.SystemColors.Window;
			componentResourceManager.ApplyResources(this.extensionsManager, "extensionsManager");
			this.extensionsManager.HelpUtilities = null;
			this.extensionsManager.Name = "extensionsManager";
			this.extensionsManager.OnShowModalWindow += new global::System.EventHandler(this.extensionsManager_OnShowModalWindow);
			this.tabPage3.Controls.Add(this.txtComment);
			componentResourceManager.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.txtComment.AssociatedLabel = null;
			this.txtComment.BackColor = global::System.Drawing.Color.AliceBlue;
			componentResourceManager.ApplyResources(this.txtComment, "txtComment");
			this.txtComment.Name = "txtComment";
			this.txtComment.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.txtComment.Enter += new global::System.EventHandler(this.txtComment_Enter);
			this.txtToolName.AssociatedLabel = this.lblToolName;
			componentResourceManager.ApplyResources(this.txtToolName, "txtToolName");
			this.txtToolName.Name = "txtToolName";
			this.txtToolName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.txtToolName.TextChanged += new global::System.EventHandler(this.txtToolName_TextChanged);
			this.txtToolName.Enter += new global::System.EventHandler(this.textBox_Enter);
			componentResourceManager.ApplyResources(this.chkApplyChangesToTemplate, "chkApplyChangesToTemplate");
			this.chkApplyChangesToTemplate.Name = "chkApplyChangesToTemplate";
			this.chkApplyChangesToTemplate.UseVisualStyleBackColor = true;
			this.panelSlope.Controls.Add(this.txtSlope);
			this.panelSlope.Controls.Add(this.btSlope);
			componentResourceManager.ApplyResources(this.panelSlope, "panelSlope");
			this.panelSlope.Name = "panelSlope";
			this.txtSlope.AssociatedLabel = this.lblSlope;
			this.txtSlope.BackColor = global::System.Drawing.SystemColors.Window;
			componentResourceManager.ApplyResources(this.txtSlope, "txtSlope");
			this.txtSlope.Name = "txtSlope";
			this.txtSlope.ReadOnly = true;
			this.txtSlope.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.txtSlope.Click += new global::System.EventHandler(this.textBox_Enter);
			this.txtSlope.Enter += new global::System.EventHandler(this.textBox_Enter);
			componentResourceManager.ApplyResources(this.btSlope, "btSlope");
			this.btSlope.Name = "btSlope";
			this.btSlope.UseVisualStyleBackColor = true;
			this.btSlope.Click += new global::System.EventHandler(this.btSlope_Click);
			this.btSlope.Enter += new global::System.EventHandler(this.btControl_Enter);
			this.btSlope.Leave += new global::System.EventHandler(this.btControl_Leave);
			this.txtText.AssociatedLabel = this.lblText;
			componentResourceManager.ApplyResources(this.txtText, "txtText");
			this.txtText.Name = "txtText";
			this.txtText.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtText.Click += new global::System.EventHandler(this.textBox_Enter);
			this.txtText.TextChanged += new global::System.EventHandler(this.txtText_TextChanged);
			this.txtText.Enter += new global::System.EventHandler(this.textBox_Enter);
			componentResourceManager.ApplyResources(this.lblText, "lblText");
			this.lblText.BackColor = global::System.Drawing.Color.Transparent;
			this.lblText.Name = "lblText";
			this.lblText.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.sliderDefaultSize.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.sliderDefaultSize.LabelWidth = 25;
			componentResourceManager.ApplyResources(this.sliderDefaultSize, "sliderDefaultSize");
			this.sliderDefaultSize.Maximum = 150;
			this.sliderDefaultSize.Minimum = 10;
			this.sliderDefaultSize.Name = "sliderDefaultSize";
			this.sliderDefaultSize.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.sliderDefaultSize.Value = 80;
			this.sliderDefaultSize.ValueChanged += new global::System.EventHandler(this.sliderDefaultSize_ValueChanged);
			this.sliderDefaultSize.Enter += new global::System.EventHandler(this.btControl_Enter);
			this.sliderDefaultSize.Leave += new global::System.EventHandler(this.btControl_Leave);
			componentResourceManager.ApplyResources(this.lblDefaultSize, "lblDefaultSize");
			this.lblDefaultSize.BackColor = global::System.Drawing.Color.Transparent;
			this.lblDefaultSize.ForeColor = global::System.Drawing.Color.Black;
			this.lblDefaultSize.Name = "lblDefaultSize";
			this.lblDefaultSize.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblShape, "lblShape");
			this.lblShape.BackColor = global::System.Drawing.Color.Transparent;
			this.lblShape.ForeColor = global::System.Drawing.Color.Black;
			this.lblShape.Name = "lblShape";
			this.lblShape.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.cbShape.AssociatedLabel = this.lblShape;
			this.cbShape.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbShape.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cbShape, "cbShape");
			this.cbShape.Name = "cbShape";
			this.cbShape.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbShape.SelectedIndexChanged += new global::System.EventHandler(this.cbShape_SelectedIndexChanged);
			this.cbHatchStyle.DisableInternalDrawing = true;
			this.cbHatchStyle.DisplayMember = "Text";
			this.cbHatchStyle.DrawMode = global::System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbHatchStyle.DropDownHeight = 200;
			this.cbHatchStyle.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cbHatchStyle, "cbHatchStyle");
			this.cbHatchStyle.FormattingEnabled = true;
			this.cbHatchStyle.Name = "cbHatchStyle";
			this.cbHatchStyle.SelectedValue = global::QuoterPlan.HatchStylePickerCombo.HatchStylePickerEnum.Solid;
			this.cbHatchStyle.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbHatchStyle.SelectedIndexChanged += new global::System.EventHandler(this.cbHatchStyle_SelectedIndexChanged);
			this.cbHatchStyle.Enter += new global::System.EventHandler(this.btControl_Enter);
			this.cbHatchStyle.Leave += new global::System.EventHandler(this.btControl_Leave);
			componentResourceManager.ApplyResources(this.lblHatchStyle, "lblHatchStyle");
			this.lblHatchStyle.BackColor = global::System.Drawing.Color.Transparent;
			this.lblHatchStyle.ForeColor = global::System.Drawing.Color.Black;
			this.lblHatchStyle.Name = "lblHatchStyle";
			this.lblHatchStyle.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			base.AcceptButton = this.btOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.lblHatchStyle);
			base.Controls.Add(this.cbHatchStyle);
			base.Controls.Add(this.cbShape);
			base.Controls.Add(this.lblShape);
			base.Controls.Add(this.sliderDefaultSize);
			base.Controls.Add(this.lblDefaultSize);
			base.Controls.Add(this.txtText);
			base.Controls.Add(this.lblText);
			base.Controls.Add(this.chkApplyChangesToTemplate);
			base.Controls.Add(this.panelSlope);
			base.Controls.Add(this.btDeleteTemplate);
			base.Controls.Add(this.chkSaveAsTemplate);
			base.Controls.Add(this.panelColor);
			base.Controls.Add(this.lblToolColor);
			base.Controls.Add(this.lblToolName);
			base.Controls.Add(this.sliderPenWidth);
			base.Controls.Add(this.lblPenWidth);
			base.Controls.Add(this.lblSlope);
			base.Controls.Add(this.tabControl);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.txtToolName);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ToolEditForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.Activated += new global::System.EventHandler(this.ToolEditForm_Activated);
			base.Shown += new global::System.EventHandler(this.ToolEditForm_Shown);
			this.panelColor.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox.ResumeLayout(false);
			this.panelPreview.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.picPreview).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.panelSlope.ResumeLayout(false);
			this.panelSlope.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.TabControl tabControl;

		private global::System.Windows.Forms.TabPage tabPage2;

		private global::System.Windows.Forms.TabPage tabPage1;

		private global::DevComponents.DotNetBar.GalleryContainer galleryContainer2;

		private global::DevComponents.DotNetBar.LabelItem labelItem1;

		private global::DevComponents.DotNetBar.SuperTabItem superTabItem2;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.TabPage tabPage3;

		private global::QuoterPlan.TextBoxEx txtComment;

		private global::System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

		private global::QuoterPlan.LabelEx lblSlope;

		private global::QuoterPlan.LabelEx lblPenWidth;

		private global::DevComponents.DotNetBar.Controls.Slider sliderPenWidth;

		private global::QuoterPlan.LabelEx lblToolColor;

		private global::QuoterPlan.LabelEx lblToolName;

		private global::QuoterPlan.TextBoxEx txtToolName;

		private global::System.Windows.Forms.Panel panelColor;

		private global::System.Windows.Forms.Button btMoreColors;

		private global::QuoterPlan.ColorPickerCombo cbColor;

		private global::System.Windows.Forms.Panel panelPreview;

		private global::System.Windows.Forms.PictureBox picPreview;

		private global::QuoterPlan.ExtensionsManager extensionsManager;

		private global::System.Windows.Forms.CheckBox chkSaveAsTemplate;

		private global::System.Windows.Forms.CheckBox chkApplyChangesToTemplate;

		private global::System.Windows.Forms.Button btDeleteTemplate;

		private global::System.Windows.Forms.Panel panelSlope;

		private global::System.Windows.Forms.Button btSlope;

		private global::QuoterPlan.TextBoxEx txtSlope;

		private global::QuoterPlan.TextBoxEx txtText;

		private global::QuoterPlan.LabelEx lblText;

		private global::DevComponents.DotNetBar.Controls.Slider sliderDefaultSize;

		private global::QuoterPlan.LabelEx lblDefaultSize;

		private global::QuoterPlan.LabelEx lblShape;

		private global::QuoterPlan.ComboBoxEx cbShape;

		private global::QuoterPlan.HatchStylePickerCombo cbHatchStyle;

		private global::QuoterPlan.LabelEx lblHatchStyle;

		private global::DevComponents.DotNetBar.ButtonX btEELink;

		private global::DevComponents.DotNetBar.ButtonX btEERevokeLink;
	}
}
