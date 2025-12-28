using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class DataFolderForm : BaseForm
	{
		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
			this.lblWarning.Font = Utilities.GetDefaultFont(FontStyle.Bold);
		}

		private void LoadValues()
		{
			this.exitNow = true;
			this.opDefaultFolder.Checked = (Settings.Default.DataFolder == "");
			this.opAlternateFolder.Checked = (Settings.Default.DataFolder != "");
			this.txtAlternateFolder.Text = Settings.Default.DataFolder;
			if (this.txtAlternateFolder.Text != "" && !this.txtAlternateFolder.Text.EndsWith(Utilities.ApplicationName, StringComparison.InvariantCultureIgnoreCase))
			{
				this.txtAlternateFolder.Text = Path.Combine(this.txtAlternateFolder.Text, Utilities.ApplicationName);
			}
			this.exitNow = false;
		}

		private bool SaveValues()
		{
			string text = this.opDefaultFolder.Checked ? "" : this.txtAlternateFolder.Text;
			if (text != "")
			{
				if (text.EndsWith(Utilities.ApplicationName, StringComparison.InvariantCultureIgnoreCase))
				{
					text = text.Remove(text.ToLower().IndexOf(Utilities.ApplicationName.ToLower()), Utilities.ApplicationName.ToLower().Length);
				}
				if (Path.Combine(text, Utilities.ApplicationName) == Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName))
				{
					text = "";
				}
			}
			bool flag = Settings.Default.DataFolder != text;
			if (flag)
			{
				Settings.Default.DataFolder = text;
			}
			return flag;
		}

		public DataFolderForm()
		{
			this.InitializeComponent();
			this.InitializeFonts();
			this.LoadValues();
		}

		private void txtAlternateFolder_MouseEnter(object sender, EventArgs e)
		{
			Utilities.SelectText(this.txtAlternateFolder);
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			string dbfolder = Utilities.GetDBFolder("");
			bool flag = this.SaveValues();
			if (flag)
			{
				if (!Utilities.FileExists(Path.Combine(Utilities.GetDBFolder(""), Utilities.GetDefaultDBName())))
				{
					Utilities.FileCopy(Path.Combine(dbfolder, Utilities.GetDefaultDBName()), Path.Combine(Utilities.GetDBFolder(""), Utilities.GetDefaultDBName()));
				}
				string redémarrage_requis = Resources.Redémarrage_requis;
				string vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif = Utilities.Vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif;
				Utilities.DisplayMessage(redémarrage_requis, vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif);
			}
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void opAlternateFolder_CheckedChanged(object sender, EventArgs e)
		{
			this.txtAlternateFolder.Enabled = this.opAlternateFolder.Checked;
			if (this.opAlternateFolder.Checked && !this.exitNow)
			{
				Utilities.SetObjectFocus(this.txtAlternateFolder);
				if (this.txtAlternateFolder.Text == string.Empty)
				{
					this.SelectFolder();
				}
			}
		}

		private void SelectFolder()
		{
			string text = Utilities.SelectFolder("");
			if (text != "")
			{
				if (!text.EndsWith(Utilities.ApplicationName, StringComparison.InvariantCultureIgnoreCase))
				{
					text = Path.Combine(text, Utilities.ApplicationName);
				}
				this.txtAlternateFolder.Text = text;
				Utilities.SetObjectFocus(this.txtAlternateFolder);
				return;
			}
			if (this.txtAlternateFolder.Text == "")
			{
				this.exitNow = true;
				this.opDefaultFolder.Checked = true;
				this.opAlternateFolder.Checked = false;
				Utilities.SetObjectFocus(this.opDefaultFolder);
				this.exitNow = false;
			}
		}

		private void btSelectAlternateFolder_Click(object sender, EventArgs e)
		{
			this.SelectFolder();
		}

		private void DataFolderForm_Shown(object sender, EventArgs e)
		{
			if (this.opAlternateFolder.Checked)
			{
				Utilities.SetObjectFocus(this.txtAlternateFolder);
			}
		}

		private bool exitNow;
	}
}
