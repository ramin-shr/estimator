namespace QuoterPlan
{
	public partial class MessageBoxEx : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.MessageBoxEx));
			this.btCancel = new global::System.Windows.Forms.Button();
			this.panelBottom = new global::DevComponents.DotNetBar.PanelEx();
			this.btCustomYes = new global::System.Windows.Forms.Button();
			this.btOk = new global::System.Windows.Forms.Button();
			this.btYes = new global::System.Windows.Forms.Button();
			this.btDelete = new global::System.Windows.Forms.Button();
			this.btNo = new global::System.Windows.Forms.Button();
			this.btCustomQuestion2 = new global::System.Windows.Forms.Button();
			this.btCustomQuestion1 = new global::System.Windows.Forms.Button();
			this.panelTop = new global::DevComponents.DotNetBar.PanelEx();
			this.lblTitle = new global::QuoterPlan.LabelEx();
			this.lblMessage = new global::QuoterPlan.LabelEx();
			this.txtMessage = new global::System.Windows.Forms.TextBox();
			this.picIcon = new global::QuoterPlan.TransparentPictureBox();
			this.panelBottom.SuspendLayout();
			this.panelTop.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btButton_Click);
			this.panelBottom.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelBottom.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelBottom.Controls.Add(this.btCustomYes);
			this.panelBottom.Controls.Add(this.btOk);
			this.panelBottom.Controls.Add(this.btCancel);
			this.panelBottom.Controls.Add(this.btYes);
			this.panelBottom.Controls.Add(this.btDelete);
			this.panelBottom.Controls.Add(this.btNo);
			this.panelBottom.Controls.Add(this.btCustomQuestion2);
			this.panelBottom.Controls.Add(this.btCustomQuestion1);
			componentResourceManager.ApplyResources(this.panelBottom, "panelBottom");
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelBottom.Style.BackColor1.Color = global::System.Drawing.SystemColors.ButtonFace;
			this.panelBottom.Style.BackColor2.Color = global::System.Drawing.Color.WhiteSmoke;
			this.panelBottom.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelBottom.Style.BorderColor.Color = global::System.Drawing.Color.LightGray;
			this.panelBottom.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Top;
			this.panelBottom.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelBottom.Style.GradientAngle = 90;
			componentResourceManager.ApplyResources(this.btCustomYes, "btCustomYes");
			this.btCustomYes.DialogResult = global::System.Windows.Forms.DialogResult.Yes;
			this.btCustomYes.Name = "btCustomYes";
			this.btCustomYes.Text = global::QuoterPlan.Properties.Resources.Rendre_visible;
			this.btCustomYes.UseVisualStyleBackColor = true;
			this.btCustomYes.Click += new global::System.EventHandler(this.btButton_Click);
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btButton_Click);
			componentResourceManager.ApplyResources(this.btYes, "btYes");
			this.btYes.DialogResult = global::System.Windows.Forms.DialogResult.Yes;
			this.btYes.Name = "btYes";
			this.btYes.Text = global::QuoterPlan.Properties.Resources.Oui;
			this.btYes.UseVisualStyleBackColor = true;
			this.btYes.Click += new global::System.EventHandler(this.btButton_Click);
			this.btDelete.DialogResult = global::System.Windows.Forms.DialogResult.Yes;
			componentResourceManager.ApplyResources(this.btDelete, "btDelete");
			this.btDelete.Name = "btDelete";
			this.btDelete.UseVisualStyleBackColor = true;
			this.btDelete.Click += new global::System.EventHandler(this.btButton_Click);
			componentResourceManager.ApplyResources(this.btNo, "btNo");
			this.btNo.DialogResult = global::System.Windows.Forms.DialogResult.No;
			this.btNo.Name = "btNo";
			this.btNo.Text = global::QuoterPlan.Properties.Resources.Non;
			this.btNo.UseVisualStyleBackColor = true;
			this.btNo.Click += new global::System.EventHandler(this.btButton_Click);
			componentResourceManager.ApplyResources(this.btCustomQuestion2, "btCustomQuestion2");
			this.btCustomQuestion2.DialogResult = global::System.Windows.Forms.DialogResult.No;
			this.btCustomQuestion2.Name = "btCustomQuestion2";
			this.btCustomQuestion2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btCustomQuestion1, "btCustomQuestion1");
			this.btCustomQuestion1.DialogResult = global::System.Windows.Forms.DialogResult.Yes;
			this.btCustomQuestion1.Name = "btCustomQuestion1";
			this.btCustomQuestion1.UseVisualStyleBackColor = true;
			this.panelTop.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelTop.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelTop.Controls.Add(this.lblTitle);
			componentResourceManager.ApplyResources(this.panelTop, "panelTop");
			this.panelTop.Name = "panelTop";
			this.panelTop.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelTop.Style.BackColor1.Color = global::System.Drawing.Color.WhiteSmoke;
			this.panelTop.Style.BackColor2.Color = global::System.Drawing.SystemColors.ButtonFace;
			this.panelTop.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelTop.Style.BorderColor.Color = global::System.Drawing.Color.LightGray;
			this.panelTop.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Bottom;
			this.panelTop.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelTop.Style.GradientAngle = 90;
			componentResourceManager.ApplyResources(this.lblTitle, "lblTitle");
			this.lblTitle.ForeColor = global::System.Drawing.Color.Black;
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblTitle.UseMnemonic = false;
			this.lblMessage.BackColor = global::System.Drawing.Color.Transparent;
			this.lblMessage.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.lblMessage, "lblMessage");
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.lblMessage.UseMnemonic = false;
			componentResourceManager.ApplyResources(this.txtMessage, "txtMessage");
			this.txtMessage.Name = "txtMessage";
			this.picIcon.BackColor = global::System.Drawing.SystemColors.ButtonFace;
			this.picIcon.Image = null;
			componentResourceManager.ApplyResources(this.picIcon, "picIcon");
			this.picIcon.Name = "picIcon";
			this.picIcon.TabStop = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.Snow;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.picIcon);
			base.Controls.Add(this.panelBottom);
			base.Controls.Add(this.panelTop);
			base.Controls.Add(this.lblMessage);
			base.Controls.Add(this.txtMessage);
			this.ForeColor = global::System.Drawing.Color.Black;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MessageBoxEx";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MessageBoxEx_FormClosing);
			this.panelBottom.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::QuoterPlan.LabelEx lblMessage;

		private global::DevComponents.DotNetBar.PanelEx panelTop;

		private global::QuoterPlan.LabelEx lblTitle;

		private global::DevComponents.DotNetBar.PanelEx panelBottom;

		private global::System.Windows.Forms.Button btYes;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btNo;

		private global::System.Windows.Forms.Button btDelete;

		private global::System.Windows.Forms.Button btCancel;

		private global::System.Windows.Forms.TextBox txtMessage;

		private global::QuoterPlan.TransparentPictureBox picIcon;

		private global::System.Windows.Forms.Button btCustomYes;

		private global::System.Windows.Forms.Button btCustomQuestion2;

		private global::System.Windows.Forms.Button btCustomQuestion1;
	}
}
