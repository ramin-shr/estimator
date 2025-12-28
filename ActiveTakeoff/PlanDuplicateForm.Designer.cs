namespace QuoterPlan
{
	public partial class PlanDuplicateForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.PlanDuplicateForm));
			this.groupBox = new global::System.Windows.Forms.GroupBox();
			this.panelGroup = new global::System.Windows.Forms.Panel();
			this.slider1 = new global::DevComponents.DotNetBar.Controls.Slider();
			this.labelEx1 = new global::QuoterPlan.LabelEx();
			this.opNo = new global::System.Windows.Forms.RadioButton();
			this.lblCopyObjects = new global::QuoterPlan.LabelEx();
			this.opYes = new global::System.Windows.Forms.RadioButton();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.btDuplicate = new global::System.Windows.Forms.Button();
			this.groupBox.SuspendLayout();
			this.panelGroup.SuspendLayout();
			base.SuspendLayout();
			this.groupBox.Controls.Add(this.panelGroup);
			this.groupBox.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.groupBox, "groupBox");
			this.groupBox.Name = "groupBox";
			this.groupBox.TabStop = false;
			this.panelGroup.Controls.Add(this.slider1);
			this.panelGroup.Controls.Add(this.labelEx1);
			this.panelGroup.Controls.Add(this.opNo);
			this.panelGroup.Controls.Add(this.lblCopyObjects);
			this.panelGroup.Controls.Add(this.opYes);
			componentResourceManager.ApplyResources(this.panelGroup, "panelGroup");
			this.panelGroup.Name = "panelGroup";
			this.slider1.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.slider1.LabelPosition = global::DevComponents.DotNetBar.eSliderLabelPosition.Bottom;
			componentResourceManager.ApplyResources(this.slider1, "slider1");
			this.slider1.Maximum = 50;
			this.slider1.Minimum = 1;
			this.slider1.Name = "slider1";
			this.slider1.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.slider1.TrackMarker = false;
			this.slider1.Value = 1;
			this.slider1.ValueChanged += new global::System.EventHandler(this.slider1_ValueChanged);
			this.slider1.ValueChanging += new global::DevComponents.DotNetBar.CancelIntValueEventHandler(this.slider1_ValueChanging);
			componentResourceManager.ApplyResources(this.labelEx1, "labelEx1");
			this.labelEx1.BackColor = global::System.Drawing.Color.Transparent;
			this.labelEx1.ForeColor = global::System.Drawing.Color.Black;
			this.labelEx1.Name = "labelEx1";
			this.labelEx1.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.opNo, "opNo");
			this.opNo.Name = "opNo";
			this.opNo.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.lblCopyObjects, "lblCopyObjects");
			this.lblCopyObjects.BackColor = global::System.Drawing.Color.Transparent;
			this.lblCopyObjects.ForeColor = global::System.Drawing.Color.Black;
			this.lblCopyObjects.Name = "lblCopyObjects";
			this.lblCopyObjects.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.opYes, "opYes");
			this.opYes.Checked = true;
			this.opYes.Name = "opYes";
			this.opYes.TabStop = true;
			this.opYes.UseVisualStyleBackColor = true;
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btDuplicate, "btDuplicate");
			this.btDuplicate.Name = "btDuplicate";
			this.btDuplicate.UseVisualStyleBackColor = true;
			this.btDuplicate.Click += new global::System.EventHandler(this.btDuplicate_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.btDuplicate);
			base.Controls.Add(this.groupBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PlanDuplicateForm";
			base.ShowInTaskbar = false;
			this.groupBox.ResumeLayout(false);
			this.panelGroup.ResumeLayout(false);
			this.panelGroup.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.GroupBox groupBox;

		private global::System.Windows.Forms.Panel panelGroup;

		private global::System.Windows.Forms.RadioButton opYes;

		private global::QuoterPlan.LabelEx lblCopyObjects;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.Button btDuplicate;

		private global::QuoterPlan.LabelEx labelEx1;

		private global::System.Windows.Forms.RadioButton opNo;

		private global::DevComponents.DotNetBar.Controls.Slider slider1;
	}
}
