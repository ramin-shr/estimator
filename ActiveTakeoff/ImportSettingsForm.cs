using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class ImportSettingsForm : BaseForm
	{
		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		private void LoadValues()
		{
			this.exitNow = true;
			this.txtFileToImport.Text = Settings.Default.ImportDBPricesFileName;
			this.txtProductCodePosition.Text = Settings.Default.ImportDBPricesSkuPosition.ToString();
			this.txtProductPricePosition.Text = Settings.Default.ImportDBPricesPricePosition.ToString();
			this.txtFieldSeparator.Text = Settings.Default.ImportDBPricesSeparator.ToString();
			this.exitNow = false;
		}

		public ImportSettingsForm()
		{
			this.InitializeComponent();
			this.InitializeFonts();
			this.LoadValues();
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			this.txtFileToImport.Text = this.txtFileToImport.Text.Trim();
			if (this.txtFileToImport.Text == "")
			{
				Utilities.SetObjectFocus(this.txtFileToImport);
				return;
			}
			this.txtProductCodePosition.Text = Utilities.ConvertToInt(this.txtProductCodePosition.Text).ToString();
			if (this.txtProductCodePosition.Text == "" || this.txtProductCodePosition.Text == "0")
			{
				Utilities.SetObjectFocus(this.txtProductCodePosition);
				return;
			}
			this.txtProductPricePosition.Text = Utilities.ConvertToInt(this.txtProductPricePosition.Text).ToString();
			if (this.txtProductPricePosition.Text == "" || this.txtProductPricePosition.Text == "0")
			{
				Utilities.SetObjectFocus(this.txtProductPricePosition);
				return;
			}
			this.txtFieldSeparator.Text = this.txtFieldSeparator.Text.Trim();
			if (this.txtFieldSeparator.Text == "")
			{
				Utilities.SetObjectFocus(this.txtFieldSeparator);
				return;
			}
			Settings.Default.ImportDBPricesFileName = this.txtFileToImport.Text;
			Settings.Default.ImportDBPricesSkuPosition = Utilities.ConvertToInt(this.txtProductCodePosition.Text);
			Settings.Default.ImportDBPricesPricePosition = Utilities.ConvertToInt(this.txtProductPricePosition.Text);
			Settings.Default.ImportDBPricesSeparator = this.txtFieldSeparator.Text[0];
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void SelectFileName()
		{
			string text = Utilities.OpenFileDialog(Resources.Fichier_à_importer, Utilities.GetDBFolder(""), Resources.Tous_les_fichiers + " (*.*)|*.*");
			if (text != string.Empty)
			{
				this.txtFileToImport.Text = text;
			}
		}

		private void btFileToImport_Click(object sender, EventArgs e)
		{
			this.SelectFileName();
			Utilities.SetObjectFocus(this.txtFileToImport);
		}

		private void ImportSettingsForm_Shown(object sender, EventArgs e)
		{
			Utilities.SetObjectFocus(this.txtFileToImport);
		}

		private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			string text = "0123456789";
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\r' && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtNumeric_Validating(object sender, CancelEventArgs e)
		{
			((TextBox)sender).Text = Utilities.ConvertToInt(((TextBox)sender).Text).ToString();
		}

		private void txtField_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private void btHelp_Click(object sender, EventArgs e)
		{
			base.HelpUtilities.ShowContextID(86);
		}

		private bool exitNow;
	}
}
