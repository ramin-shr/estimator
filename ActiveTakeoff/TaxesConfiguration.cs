using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class TaxesConfiguration : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		public TaxesConfiguration()
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.txtTax1Label.Text = Settings.Default.Tax1Label;
			this.txtTax1Rate.Text = (Settings.Default.Tax1Rate * 100.0).ToString();
			this.txtTax2Label.Text = Settings.Default.Tax2Label;
			this.txtTax2Rate.Text = (Settings.Default.Tax2Rate * 100.0).ToString();
			this.chkTaxOnTax.Checked = Settings.Default.TaxOnTax;
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			this.txtTax1Label.Text = this.txtTax1Label.Text.Trim();
			this.txtTax1Rate.Text = this.txtTax1Rate.Text.Trim();
			this.txtTax2Label.Text = this.txtTax2Label.Text.Trim();
			this.txtTax2Rate.Text = this.txtTax2Rate.Text.Trim();
			Settings.Default.Tax1Label = this.txtTax1Label.Text;
			Settings.Default.Tax1Rate = Utilities.ConvertToDouble(this.txtTax1Rate.Text, -1) / 100.0;
			Settings.Default.Tax2Label = this.txtTax2Label.Text;
			Settings.Default.Tax2Rate = Utilities.ConvertToDouble(this.txtTax2Rate.Text, -1) / 100.0;
			Settings.Default.TaxOnTax = this.chkTaxOnTax.Checked;
			base.Close();
		}

		private void txtField_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBoxEx)sender);
		}

		private void txtTaxLabel_Validating(object sender, CancelEventArgs e)
		{
			TextBoxEx textBoxEx = (TextBoxEx)sender;
			textBoxEx.Text = textBoxEx.Text.Trim();
			if (textBoxEx.Text == "")
			{
				textBoxEx.Text = ((textBoxEx.Name == "txtTax1Label") ? Resources.Taxe_1 : Resources.Taxe_2);
			}
		}

		private void txtTaxRate_Validating(object sender, CancelEventArgs e)
		{
			TextBoxEx textBoxEx = (TextBoxEx)sender;
			double num = Utilities.ConvertToDouble(textBoxEx.Text, -1);
			if (num < 1.0)
			{
				num *= 100.0;
			}
			textBoxEx.Text = num.ToString();
		}
	}
}
