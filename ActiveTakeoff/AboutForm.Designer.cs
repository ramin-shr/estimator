using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace QuoterPlan
{
	public partial class AboutForm : QuoterPlan.BaseForm
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
                new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");

            this.panelTop = new PanelEx();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.panelBottom = new PanelEx();
			this.lblVersion = new LabelX();
			this.btOk = new ButtonX();
			this.lblWebSite = new LabelX();
			this.lblCopyright = new LabelX();
			this.lblCopyrightNotice = new LabelX();
			this.lblProductKey = new LabelX();
			this.lblPleaseWait = new LabelX();
			this.txtProductKey = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.lblProductKeyValue = new LabelX();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.picLogo).BeginInit();
			this.panelBottom.SuspendLayout();
			base.SuspendLayout();
			this.panelTop.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelTop.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
			this.panelTop.Controls.Add(this.picLogo);
			resources.ApplyResources(this.panelTop, "panelTop");
			this.panelTop.Name = "panelTop";
			this.panelTop.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelTop.Style.BackColor1.Color = System.Drawing.Color.FromArgb(14, 70, 126);
			this.panelTop.Style.BackColor2.Color = System.Drawing.Color.FromArgb(14, 70, 126);
			this.panelTop.Style.Border = eBorderType.SingleLine;
			this.panelTop.Style.BorderColor.Color = System.Drawing.Color.DimGray;
			this.panelTop.Style.BorderSide = eBorderSide.Bottom;
			this.panelTop.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
			this.panelTop.Style.GradientAngle = 90;
			this.picLogo.BackColor = System.Drawing.Color.White;
			this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.picLogo, "picLogo");
			this.picLogo.Name = "picLogo";
			this.picLogo.TabStop = false;
			this.panelBottom.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelBottom.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
			this.panelBottom.Controls.Add(this.lblVersion);
			this.panelBottom.Controls.Add(this.btOk);
			this.panelBottom.Controls.Add(this.lblWebSite);
			this.panelBottom.Controls.Add(this.lblCopyright);
			this.panelBottom.Controls.Add(this.lblCopyrightNotice);
			this.panelBottom.Controls.Add(this.lblProductKey);
			this.panelBottom.Controls.Add(this.lblPleaseWait);
			this.panelBottom.Controls.Add(this.txtProductKey);
			this.panelBottom.Controls.Add(this.lblProductKeyValue);
			resources.ApplyResources(this.panelBottom, "panelBottom");
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelBottom.Style.BackColor1.Color = System.Drawing.Color.White;
			this.panelBottom.Style.BackColor2.Alpha = 100;
			this.panelBottom.Style.BackColor2.Color = System.Drawing.Color.WhiteSmoke;
			this.panelBottom.Style.BackgroundImageAlpha = 200;
			this.panelBottom.Style.Border = eBorderType.SingleLine;
			this.panelBottom.Style.BorderColor.Color = System.Drawing.Color.DimGray;
			this.panelBottom.Style.BorderSide = eBorderSide.None;
			this.panelBottom.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
			this.panelBottom.Style.GradientAngle = 270;
			this.lblVersion.BackgroundStyle.CornerType = eCornerType.Square;
			this.lblVersion.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.lblVersion, "lblVersion");
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Style = eDotNetBarStyle.StyleManagerControlled;
			this.lblVersion.TextAlignment = System.Drawing.StringAlignment.Center;
			this.btOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			resources.ApplyResources(this.btOk, "btOk");
			this.btOk.ColorTable = eButtonColor.OrangeWithBackground;
			this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOk.FocusCuesEnabled = false;
			this.btOk.Name = "btOk";
			this.btOk.Style = eDotNetBarStyle.StyleManagerControlled;
			resources.ApplyResources(this.lblWebSite, "lblWebSite");
			this.lblWebSite.BackgroundStyle.CornerType = eCornerType.Square;
			this.lblWebSite.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblWebSite.ForeColor = System.Drawing.Color.FromArgb(14, 70, 126);
			this.lblWebSite.Name = "lblWebSite";
			this.lblWebSite.Style = eDotNetBarStyle.StyleManagerControlled;
			this.lblWebSite.TextAlignment = System.Drawing.StringAlignment.Center;
			this.lblWebSite.Click += new System.EventHandler(this.lblWebSite_Click);
			this.lblCopyright.BackgroundStyle.CornerType = eCornerType.Square;
			this.lblCopyright.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.lblCopyright, "lblCopyright");
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Style = eDotNetBarStyle.StyleManagerControlled;
			this.lblCopyright.TextAlignment = System.Drawing.StringAlignment.Center;
			this.lblCopyrightNotice.BackgroundStyle.CornerType = eCornerType.Square;
			resources.ApplyResources(this.lblCopyrightNotice, "lblCopyrightNotice");
			this.lblCopyrightNotice.ForeColor = System.Drawing.Color.Black;
			this.lblCopyrightNotice.Name = "lblCopyrightNotice";
			this.lblCopyrightNotice.PaddingBottom = 10;
			this.lblCopyrightNotice.PaddingLeft = 20;
			this.lblCopyrightNotice.PaddingRight = 20;
			this.lblCopyrightNotice.Style = eDotNetBarStyle.StyleManagerControlled;
			this.lblCopyrightNotice.TextAlignment = System.Drawing.StringAlignment.Far;
			this.lblCopyrightNotice.WordWrap = true;
			resources.ApplyResources(this.lblProductKey, "lblProductKey");
			this.lblProductKey.BackgroundStyle.CornerType = eCornerType.Square;
			this.lblProductKey.ForeColor = System.Drawing.Color.Black;
			this.lblProductKey.Name = "lblProductKey";
			this.lblProductKey.Style = eDotNetBarStyle.StyleManagerControlled;
			this.lblPleaseWait.BackgroundStyle.CornerType = eCornerType.Square;
			this.lblPleaseWait.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblPleaseWait.ForeColor = System.Drawing.Color.DarkRed;
			resources.ApplyResources(this.lblPleaseWait, "lblPleaseWait");
			this.lblPleaseWait.Name = "lblPleaseWait";
			this.lblPleaseWait.Style = eDotNetBarStyle.StyleManagerControlled;
			this.lblPleaseWait.TextAlignment = System.Drawing.StringAlignment.Center;
			resources.ApplyResources(this.txtProductKey, "txtProductKey");
			this.txtProductKey.Border.Class = "TextBoxBorder";
			this.txtProductKey.Border.CornerType = eCornerType.Square;
			this.txtProductKey.Name = "txtProductKey";
			this.txtProductKey.ReadOnly = true;
			this.txtProductKey.Click += new System.EventHandler(this.txtProductKey_Enter);
			this.txtProductKey.Enter += new System.EventHandler(this.txtProductKey_Enter);
			this.lblProductKeyValue.BackgroundStyle.CornerType = eCornerType.Square;
			this.lblProductKeyValue.ForeColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.lblProductKeyValue, "lblProductKeyValue");
			this.lblProductKeyValue.Name = "lblProductKeyValue";
			this.lblProductKeyValue.SingleLineColor = System.Drawing.Color.Black;
			this.lblProductKeyValue.Style = eDotNetBarStyle.StyleManagerControlled;
			this.lblProductKeyValue.Click += new System.EventHandler(this.txtProductKey_Enter);
			this.lblProductKeyValue.Enter += new System.EventHandler(this.txtProductKey_Enter);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			base.Controls.Add(this.panelTop);
			base.Controls.Add(this.panelBottom);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.picLogo).EndInit();
			this.panelBottom.ResumeLayout(false);
			this.panelBottom.PerformLayout();
			base.ResumeLayout(false);
		}

		System.Windows.Forms.PictureBox picLogo;

		PanelEx panelTop;

		PanelEx panelBottom;

		LabelX lblVersion;

		ButtonX btOk;

		LabelX lblWebSite;

		LabelX lblCopyright;

		LabelX lblCopyrightNotice;

		TextBoxX txtProductKey;

		LabelX lblProductKey;

		LabelX lblPleaseWait;

		LabelX lblProductKeyValue;
	}
}
