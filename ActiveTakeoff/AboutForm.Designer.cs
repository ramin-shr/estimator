namespace QuoterPlan
{
	public partial class AboutForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.AboutForm));
			this.panelTop = new global::DevComponents.DotNetBar.PanelEx();
			this.picLogo = new global::System.Windows.Forms.PictureBox();
			this.panelBottom = new global::DevComponents.DotNetBar.PanelEx();
			this.lblVersion = new global::DevComponents.DotNetBar.LabelX();
			this.btOk = new global::DevComponents.DotNetBar.ButtonX();
			this.lblWebSite = new global::DevComponents.DotNetBar.LabelX();
			this.lblCopyright = new global::DevComponents.DotNetBar.LabelX();
			this.lblCopyrightNotice = new global::DevComponents.DotNetBar.LabelX();
			this.lblProductKey = new global::DevComponents.DotNetBar.LabelX();
			this.lblPleaseWait = new global::DevComponents.DotNetBar.LabelX();
			this.txtProductKey = new global::DevComponents.DotNetBar.Controls.TextBoxX();
			this.lblProductKeyValue = new global::DevComponents.DotNetBar.LabelX();
			this.panelTop.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picLogo).BeginInit();
			this.panelBottom.SuspendLayout();
			base.SuspendLayout();
			this.panelTop.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelTop.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelTop.Controls.Add(this.picLogo);
			componentResourceManager.ApplyResources(this.panelTop, "panelTop");
			this.panelTop.Name = "panelTop";
			this.panelTop.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelTop.Style.BackColor1.Color = global::System.Drawing.Color.FromArgb(14, 70, 126);
			this.panelTop.Style.BackColor2.Color = global::System.Drawing.Color.FromArgb(14, 70, 126);
			this.panelTop.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelTop.Style.BorderColor.Color = global::System.Drawing.Color.DimGray;
			this.panelTop.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Bottom;
			this.panelTop.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelTop.Style.GradientAngle = 90;
			this.picLogo.BackColor = global::System.Drawing.Color.White;
			this.picLogo.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			componentResourceManager.ApplyResources(this.picLogo, "picLogo");
			this.picLogo.Name = "picLogo";
			this.picLogo.TabStop = false;
			this.panelBottom.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelBottom.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelBottom.Controls.Add(this.lblVersion);
			this.panelBottom.Controls.Add(this.btOk);
			this.panelBottom.Controls.Add(this.lblWebSite);
			this.panelBottom.Controls.Add(this.lblCopyright);
			this.panelBottom.Controls.Add(this.lblCopyrightNotice);
			this.panelBottom.Controls.Add(this.lblProductKey);
			this.panelBottom.Controls.Add(this.lblPleaseWait);
			this.panelBottom.Controls.Add(this.txtProductKey);
			this.panelBottom.Controls.Add(this.lblProductKeyValue);
			componentResourceManager.ApplyResources(this.panelBottom, "panelBottom");
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelBottom.Style.BackColor1.Color = global::System.Drawing.Color.White;
			this.panelBottom.Style.BackColor2.Alpha = 100;
			this.panelBottom.Style.BackColor2.Color = global::System.Drawing.Color.WhiteSmoke;
			this.panelBottom.Style.BackgroundImageAlpha = 200;
			this.panelBottom.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelBottom.Style.BorderColor.Color = global::System.Drawing.Color.DimGray;
			this.panelBottom.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.panelBottom.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelBottom.Style.GradientAngle = 270;
			this.lblVersion.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblVersion.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.lblVersion, "lblVersion");
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.lblVersion.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.btOk.AccessibleRole = global::System.Windows.Forms.AccessibleRole.PushButton;
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.ColorTable = global::DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btOk.FocusCuesEnabled = false;
			this.btOk.Name = "btOk";
			this.btOk.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			componentResourceManager.ApplyResources(this.lblWebSite, "lblWebSite");
			this.lblWebSite.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblWebSite.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.lblWebSite.ForeColor = global::System.Drawing.Color.FromArgb(14, 70, 126);
			this.lblWebSite.Name = "lblWebSite";
			this.lblWebSite.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.lblWebSite.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblWebSite.Click += new global::System.EventHandler(this.lblWebSite_Click);
			this.lblCopyright.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblCopyright.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.lblCopyright, "lblCopyright");
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.lblCopyright.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblCopyrightNotice.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			componentResourceManager.ApplyResources(this.lblCopyrightNotice, "lblCopyrightNotice");
			this.lblCopyrightNotice.ForeColor = global::System.Drawing.Color.Black;
			this.lblCopyrightNotice.Name = "lblCopyrightNotice";
			this.lblCopyrightNotice.PaddingBottom = 10;
			this.lblCopyrightNotice.PaddingLeft = 20;
			this.lblCopyrightNotice.PaddingRight = 20;
			this.lblCopyrightNotice.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.lblCopyrightNotice.TextAlignment = global::System.Drawing.StringAlignment.Far;
			this.lblCopyrightNotice.WordWrap = true;
			componentResourceManager.ApplyResources(this.lblProductKey, "lblProductKey");
			this.lblProductKey.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblProductKey.ForeColor = global::System.Drawing.Color.Black;
			this.lblProductKey.Name = "lblProductKey";
			this.lblProductKey.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.lblPleaseWait.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblPleaseWait.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.lblPleaseWait.ForeColor = global::System.Drawing.Color.DarkRed;
			componentResourceManager.ApplyResources(this.lblPleaseWait, "lblPleaseWait");
			this.lblPleaseWait.Name = "lblPleaseWait";
			this.lblPleaseWait.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.lblPleaseWait.TextAlignment = global::System.Drawing.StringAlignment.Center;
			componentResourceManager.ApplyResources(this.txtProductKey, "txtProductKey");
			this.txtProductKey.Border.Class = "TextBoxBorder";
			this.txtProductKey.Border.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.txtProductKey.Name = "txtProductKey";
			this.txtProductKey.ReadOnly = true;
			this.txtProductKey.Click += new global::System.EventHandler(this.txtProductKey_Enter);
			this.txtProductKey.Enter += new global::System.EventHandler(this.txtProductKey_Enter);
			this.lblProductKeyValue.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblProductKeyValue.ForeColor = global::System.Drawing.Color.Black;
			componentResourceManager.ApplyResources(this.lblProductKeyValue, "lblProductKeyValue");
			this.lblProductKeyValue.Name = "lblProductKeyValue";
			this.lblProductKeyValue.SingleLineColor = global::System.Drawing.Color.Black;
			this.lblProductKeyValue.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.lblProductKeyValue.Click += new global::System.EventHandler(this.txtProductKey_Enter);
			this.lblProductKeyValue.Enter += new global::System.EventHandler(this.txtProductKey_Enter);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.Controls.Add(this.panelTop);
			base.Controls.Add(this.panelBottom);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.picLogo).EndInit();
			this.panelBottom.ResumeLayout(false);
			this.panelBottom.PerformLayout();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.PictureBox picLogo;

		private global::DevComponents.DotNetBar.PanelEx panelTop;

		private global::DevComponents.DotNetBar.PanelEx panelBottom;

		private global::DevComponents.DotNetBar.LabelX lblVersion;

		private global::DevComponents.DotNetBar.ButtonX btOk;

		private global::DevComponents.DotNetBar.LabelX lblWebSite;

		private global::DevComponents.DotNetBar.LabelX lblCopyright;

		private global::DevComponents.DotNetBar.LabelX lblCopyrightNotice;

		private global::DevComponents.DotNetBar.Controls.TextBoxX txtProductKey;

		private global::DevComponents.DotNetBar.LabelX lblProductKey;

		private global::DevComponents.DotNetBar.LabelX lblPleaseWait;

		private global::DevComponents.DotNetBar.LabelX lblProductKeyValue;
	}
}
