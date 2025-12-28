using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class PDFSelectionForm : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		private int CountSelectedPages()
		{
			int num = 0;
			for (int i = 0; i < this.pages.Count; i++)
			{
				if (Utilities.ConvertToBoolean(this.pages[i].Value, false))
				{
					num++;
				}
			}
			return num;
		}

		private bool SelectPages(int startPage, int endPage, bool selected)
		{
			bool result;
			try
			{
				for (int i = startPage - 1; i < endPage; i++)
				{
					this.pages[i].Value = selected;
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private bool ExtractPages()
		{
			bool result;
			try
			{
				this.SelectPages(1, this.pages.Count, false);
				string text = this.txtSelection.Text;
				string[] array = text.Split(new string[]
				{
					","
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text2 in array)
				{
					string[] array3 = text2.Split(new string[]
					{
						"-"
					}, StringSplitOptions.RemoveEmptyEntries);
					int num = Utilities.ConvertToInt(array3.GetValue(0));
					int num2 = num;
					if (array3.GetUpperBound(0) > 0)
					{
						num2 = Utilities.ConvertToInt(array3.GetValue(1));
					}
					if (num <= 0 || num2 > this.pages.Count || num2 < num)
					{
						return false;
					}
					if (!this.SelectPages(num, num2, true))
					{
						return false;
					}
				}
				result = (this.CountSelectedPages() > 0);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private bool ValidateSelection()
		{
			if (this.opImportAll.Checked)
			{
				this.SelectPages(1, this.pages.Count, true);
				return true;
			}
			this.txtSelection.Text = this.txtSelection.Text.Trim();
			if (this.ExtractPages())
			{
				return true;
			}
			string sélection_invalide = Resources.Sélection_invalide;
			string veuillez_choisir_une_plage_de_sélection_valide = Resources.Veuillez_choisir_une_plage_de_sélection_valide;
			Utilities.DisplayError(sélection_invalide, veuillez_choisir_une_plage_de_sélection_valide);
			Utilities.SetObjectFocus(this.txtSelection);
			return false;
		}

		public PDFSelectionForm(string fileName, Variables pages)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.Text = Utilities.GetFileName(fileName, false);
			this.fileName = fileName;
			this.pages = pages;
			this.opImportAll.Checked = true;
			this.txtSelection.Enabled = false;
			this.txtSelection.Text = "1 - " + pages.Count;
		}

		private void btOpen_Click(object sender, EventArgs e)
		{
			Utilities.OpenDocument(this.fileName);
		}

		private void btValidate_Click(object sender, EventArgs e)
		{
			if (!this.ValidateSelection())
			{
				return;
			}
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			this.pages.Clear();
			base.Close();
		}

		private void opImportSelection_CheckedChanged(object sender, EventArgs e)
		{
			this.txtSelection.Enabled = this.opImportSelection.Checked;
			if (this.opImportSelection.Checked)
			{
				Utilities.SetObjectFocus(this.txtSelection);
			}
		}

		private void txtSelection_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			string text = "0123456789,- ";
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\r' && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private string fileName;

		private Variables pages;
	}
}
