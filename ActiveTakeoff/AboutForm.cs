using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class AboutForm : BaseForm
	{
		private void LoadResources()
		{
			this.Text = Utilities.À_propos_de_Quoter_Plan;
			this.lblWebSite.Text = Utilities.ApplicationWebsite;
			this.txtProductKey.Text = Resources.Aucune_clé_trouvée;
			this.lblProductKeyValue.Text = Resources.Aucune_clé_trouvée;
			this.picLogo.BackColor = Color.Transparent;
			this.picLogo.BorderStyle = BorderStyle.None;
			this.picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picLogo.Left = 15;
			this.picLogo.Top = 36;
			this.picLogo.Width = 358;
			this.picLogo.Height = 64;
			this.panelTop.Style.BackColor1.Color = Color.FromArgb(165, 206, 226);
			this.panelTop.Style.BackColor2.Color = Color.FromArgb(221, 229, 241);
		}

		private void InitializeFonts()
		{
			this.lblVersion.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.lblWebSite.Font = Utilities.GetDefaultFont(FontStyle.Bold | FontStyle.Underline);
			this.lblPleaseWait.Font = Utilities.GetDefaultFont(FontStyle.Bold);
		}

		private void LoadValues()
		{
			Version version = new Version(Application.ProductVersion);
			this.lblVersion.Text = string.Format(Resources.Version_Major_Minor_Build, version.Major, version.Minor, version.Build);
			try
			{
				if (this.initMode)
				{
					this.lblProductKeyValue.Text = this.productKey;
				}
				else
				{
					this.txtProductKey.Text = this.productKey;
				}
			}
			catch
			{
			}
		}

		public AboutForm(bool initMode, string productKey)
		{
			this.InitializeComponent();
			this.initMode = initMode;
			this.productKey = productKey;
			this.lblPleaseWait.Visible = initMode;
			this.lblProductKeyValue.Visible = initMode;
			base.ControlBox = !initMode;
			this.txtProductKey.Visible = !initMode;
			this.btOk.Visible = !initMode;
			this.InitializeFonts();
			this.LoadResources();
			this.LoadValues();
		}

		private void txtProductKey_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText(this.txtProductKey);
		}

		private void lblWebSite_Click(object sender, EventArgs e)
		{
			Utilities.OpenDocument(Utilities.ApplicationWebsite);
		}

		private readonly bool initMode;

		private string productKey;
	}
}
