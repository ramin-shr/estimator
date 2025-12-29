using DevComponents.DotNetBar;
using QuoterPlan.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class EELinkManager : UserControl
    {
        private IContainer components;

        private LabelEx lblNoLink;

        private ButtonX btLink;

        private ButtonX btRevokeLink;

        private TextBoxEx txtItemID;

        private LabelEx lblItemID;

        private TextBoxEx txtKey;

        private LabelEx lblKey;

        private TextBoxEx txtPersonalKey;

        private LabelEx lblPersonalKey;

        private TextBoxEx txtDescription;

        private LabelEx lblDescription;

        public EEExchangeData EEExchangeData
        {
            get;
            set;
        }

        public EEInterface EEInterface
        {
            get;
            set;
        }

        private double Opacity
        {
            get
            {
                return base.FindForm().Opacity;
            }
        }

        public EELinkManager()
        {
            this.InitializeComponent();
            this.LoadResources();
            this.InitializeFonts();
        }

        private void btLink_Click(object sender, EventArgs e)
        {
            if (this.EEInterface.EnsureRunning(true))
            {
                this.EEInterface.SendItemSelectRequest(base.FindForm().Handle.ToInt32());
            }
        }

        private void btRevokeLink_Click(object sender, EventArgs e)
        {
            if (this.OnRevokeLink != null)
            {
                this.OnRevokeLink(this, new EventArgs());
            }
            this.UpdateGUI();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EEManager_Resize(object sender, EventArgs e)
        {
            this.RecalcLayout();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EELinkManager));
            this.btLink = new ButtonX();
            this.btRevokeLink = new ButtonX();
            this.txtDescription = new TextBoxEx();
            this.lblDescription = new LabelEx();
            this.txtPersonalKey = new TextBoxEx();
            this.lblPersonalKey = new LabelEx();
            this.txtKey = new TextBoxEx();
            this.lblKey = new LabelEx();
            this.txtItemID = new TextBoxEx();
            this.lblItemID = new LabelEx();
            this.lblNoLink = new LabelEx();
            base.SuspendLayout();
            this.btLink.AccessibleRole = AccessibleRole.PushButton;
            this.btLink.ColorTable = eButtonColor.OrangeWithBackground;
            this.btLink.Image = (Image)componentResourceManager.GetObject("btLink.Image");
            componentResourceManager.ApplyResources(this.btLink, "btLink");
            this.btLink.Name = "btLink";
            this.btLink.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btLink.Click += new EventHandler(this.btLink_Click);
            this.btRevokeLink.AccessibleRole = AccessibleRole.PushButton;
            this.btRevokeLink.ColorTable = eButtonColor.OrangeWithBackground;
            this.btRevokeLink.Image = Resources.delete_16x16;
            componentResourceManager.ApplyResources(this.btRevokeLink, "btRevokeLink");
            this.btRevokeLink.Name = "btRevokeLink";
            this.btRevokeLink.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btRevokeLink.Click += new EventHandler(this.btRevokeLink_Click);
            this.txtDescription.AssociatedLabel = this.lblDescription;
            componentResourceManager.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            this.txtDescription.Click += new EventHandler(this.txt_Enter);
            this.txtDescription.Enter += new EventHandler(this.txt_Enter);
            componentResourceManager.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.ForeColor = Color.Black;
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            this.txtPersonalKey.AssociatedLabel = this.lblPersonalKey;
            componentResourceManager.ApplyResources(this.txtPersonalKey, "txtPersonalKey");
            this.txtPersonalKey.Name = "txtPersonalKey";
            this.txtPersonalKey.ReadOnly = true;
            this.txtPersonalKey.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            this.txtPersonalKey.Click += new EventHandler(this.txt_Enter);
            this.txtPersonalKey.Enter += new EventHandler(this.txt_Enter);
            componentResourceManager.ApplyResources(this.lblPersonalKey, "lblPersonalKey");
            this.lblPersonalKey.ForeColor = Color.Black;
            this.lblPersonalKey.Name = "lblPersonalKey";
            this.lblPersonalKey.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            this.txtKey.AssociatedLabel = this.lblKey;
            componentResourceManager.ApplyResources(this.txtKey, "txtKey");
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            this.txtKey.Click += new EventHandler(this.txt_Enter);
            this.txtKey.Enter += new EventHandler(this.txt_Enter);
            componentResourceManager.ApplyResources(this.lblKey, "lblKey");
            this.lblKey.ForeColor = Color.Black;
            this.lblKey.Name = "lblKey";
            this.lblKey.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            this.txtItemID.AssociatedLabel = this.lblItemID;
            componentResourceManager.ApplyResources(this.txtItemID, "txtItemID");
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.ReadOnly = true;
            this.txtItemID.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            this.txtItemID.Click += new EventHandler(this.txt_Enter);
            this.txtItemID.Enter += new EventHandler(this.txt_Enter);
            componentResourceManager.ApplyResources(this.lblItemID, "lblItemID");
            this.lblItemID.ForeColor = Color.Black;
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            this.lblNoLink.BackColor = Color.Transparent;
            this.lblNoLink.ForeColor = Color.Black;
            componentResourceManager.ApplyResources(this.lblNoLink, "lblNoLink");
            this.lblNoLink.Name = "lblNoLink";
            this.lblNoLink.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            componentResourceManager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            base.Controls.Add(this.btRevokeLink);
            base.Controls.Add(this.btLink);
            base.Controls.Add(this.lblNoLink);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.lblDescription);
            base.Controls.Add(this.txtPersonalKey);
            base.Controls.Add(this.lblPersonalKey);
            base.Controls.Add(this.txtKey);
            base.Controls.Add(this.lblKey);
            base.Controls.Add(this.txtItemID);
            base.Controls.Add(this.lblItemID);
            base.Name = "EELinkManager";
            base.Resize += new EventHandler(this.EEManager_Resize);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeFonts()
        {
            this.Font = Utilities.GetDefaultFont();
            this.lblNoLink.Font = Utilities.GetDefaultFont();
            this.btLink.Font = Utilities.GetDefaultFont();
            this.lblItemID.Font = Utilities.GetDefaultFont();
            this.txtItemID.Font = Utilities.GetDefaultFont();
            this.lblKey.Font = Utilities.GetDefaultFont();
            this.txtKey.Font = Utilities.GetDefaultFont();
            this.lblPersonalKey.Font = Utilities.GetDefaultFont();
            this.txtPersonalKey.Font = Utilities.GetDefaultFont();
            this.lblDescription.Font = Utilities.GetDefaultFont();
            this.txtDescription.Font = Utilities.GetDefaultFont();
        }

        private void LoadResources()
        {
        }

        public void RecalcLayout()
        {
            LabelEx point = this.lblNoLink;
            int width = base.ClientSize.Width;
            Size size = this.lblNoLink.Size;
            int num = (width - size.Width) / 2;
            int height = base.ClientSize.Height;
            Size size1 = this.lblNoLink.Size;
            point.Location = new Point(num, (height - size1.Height) / 2 - this.btLink.Height);
            ButtonX buttonX = this.btLink;
            int width1 = base.ClientSize.Width;
            Size size2 = this.btLink.Size;
            buttonX.Location = new Point((width1 - size2.Width) / 2, this.lblNoLink.Top + this.lblNoLink.Height + 2);
            ButtonX point1 = this.btRevokeLink;
            int num1 = base.ClientSize.Width;
            Size size3 = this.btRevokeLink.Size;
            int width2 = (num1 - size3.Width) / 2;
            Size clientSize = base.ClientSize;
            point1.Location = new Point(width2, clientSize.Height - (this.btRevokeLink.Height + 12));
            this.txtItemID.Width = base.ClientSize.Width - 12;
            this.txtKey.Width = base.ClientSize.Width - 12;
            this.txtPersonalKey.Width = base.ClientSize.Width - 12;
            this.txtDescription.Width = base.ClientSize.Width - 12;
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            Utilities.SelectText((TextBox)sender);
        }

        private void UpdateData()
        {
            if (this.EEExchangeData != null && this.EEExchangeData.IsValid())
            {
                this.txtItemID.Text = string.Concat("(", (this.EEExchangeData.ItemType.ToUpper() == "A" ? Resources.Ensemble : Resources.Produit), ") ", this.EEExchangeData.ItemID);
                this.txtKey.Text = this.EEExchangeData.Key;
                this.txtPersonalKey.Text = this.EEExchangeData.PersonalKey;
                this.txtDescription.Text = this.EEExchangeData.Description;
                Utilities.SetObjectFocus(this.txtItemID);
            }
        }

        public void UpdateGUI()
        {
            bool flag = false;
            bool flag1 = false;
            if (this.EEExchangeData != null)
            {
                flag = true;
                flag1 = this.EEExchangeData.IsValid();
            }
            this.lblNoLink.ForeColor = (flag ? Color.Black : Color.LightGray);
            this.btLink.Enabled = flag;
            this.lblNoLink.Visible = !flag1;
            this.btLink.Visible = !flag1;
            this.btRevokeLink.Visible = flag1;
            this.lblItemID.Visible = flag1;
            this.lblKey.Visible = flag1;
            this.lblPersonalKey.Visible = flag1;
            this.lblDescription.Visible = flag1;
            this.txtItemID.Visible = flag1;
            this.txtKey.Visible = flag1;
            this.txtPersonalKey.Visible = flag1;
            this.txtDescription.Visible = flag1;
            if (flag1)
            {
                this.UpdateData();
            }
        }

        public event EventHandler OnRevokeLink;
    }
}