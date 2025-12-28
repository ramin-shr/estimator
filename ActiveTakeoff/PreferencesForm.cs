using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class PreferencesForm : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		public PreferencesForm(bool enableCancel)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.txtCompanyName.Text = Settings.Default.CompanyName;
			this.txtCompanyAddress.Text = Settings.Default.CompanyFullAddress;
			this.txtRepresentativeName.Text = Settings.Default.CompanyRepresentative;
			this.cbDefaultSystemType.Items.Clear();
			this.cbDefaultSystemType.Items.Add(new Utilities.ItemData(Resources.Metric, UnitScale.UnitSystem.metric));
			this.cbDefaultSystemType.Items.Add(new Utilities.ItemData(Resources.Imperial_US, UnitScale.UnitSystem.imperial));
			UnitScale.UnitSystem unitSystem = UnitScale.DefaultUnitSystem();
			this.cbDefaultSystemType.SelectedItem = Utilities.SelectList(this.cbDefaultSystemType.Items, unitSystem, true);
			this.cbDefaultPrecision.Items.Clear();
			this.cbDefaultPrecision.Items.Add(new Utilities.ItemData("1/64 | 0.001", UnitScale.UnitPrecision.precision64));
			this.cbDefaultPrecision.Items.Add(new Utilities.ItemData("1/32 | 0.01", UnitScale.UnitPrecision.precision32));
			this.cbDefaultPrecision.Items.Add(new Utilities.ItemData("1/16 | 0.1", UnitScale.UnitPrecision.precision16));
			this.cbDefaultPrecision.Items.Add(new Utilities.ItemData("1/8 | 1", UnitScale.UnitPrecision.precision8));
			UnitScale.UnitPrecision unitPrecision = UnitScale.DefaultUnitPrecision();
			this.cbDefaultPrecision.SelectedItem = Utilities.SelectList(this.cbDefaultPrecision.Items, unitPrecision, true);
			this.enableCancel = enableCancel;
			this.btCancel.Enabled = enableCancel;
			base.CloseEnabled = false;
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			string title = string.Empty;
			string message = string.Empty;
			this.txtCompanyName.Text = this.txtCompanyName.Text.Trim();
			this.txtCompanyAddress.Text = this.txtCompanyAddress.Text.Trim();
			this.txtRepresentativeName.Text = this.txtRepresentativeName.Text.Trim();
			if (this.txtCompanyName.Text == "")
			{
				title = Resources.Valeur_invalide;
				message = Resources.Vous_devez_specifier_une_valeur;
				Utilities.DisplayError(title, message);
				Utilities.SetObjectFocus(this.txtCompanyName);
				return;
			}
			if (this.txtRepresentativeName.Text == "")
			{
				title = Resources.Valeur_invalide;
				message = Resources.Vous_devez_specifier_une_valeur;
				Utilities.DisplayError(title, message);
				Utilities.SetObjectFocus(this.txtRepresentativeName);
				return;
			}
			Settings.Default.CompanyName = this.txtCompanyName.Text;
			Settings.Default.CompanyFullAddress = this.txtCompanyAddress.Text;
			Settings.Default.CompanyRepresentative = this.txtRepresentativeName.Text;
			Settings.Default.DefaultSystemType = (int)((Utilities.ItemData)this.cbDefaultSystemType.SelectedItem).Data;
			Settings.Default.DefaultPrecision = (int)((Utilities.ItemData)this.cbDefaultPrecision.SelectedItem).Data;
			base.Close();
		}

		private void txtField_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private void txtMultiLine_Enter(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.SelectionStart = textBox.TextLength + 1;
		}

		private bool enableCancel;
	}
}
