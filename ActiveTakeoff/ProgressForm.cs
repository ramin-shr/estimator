using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace QuoterPlan
{
	public partial class ProgressForm : BaseForm
	{
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
		}

		private void ShowCancel(bool bShow)
		{
			this.btCancel.Visible = bShow;
			base.Height = (bShow ? 165 : 130);
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.lblDescription.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.lblDescription2.Font = Utilities.GetDefaultFont();
			this.btCancel.Font = Utilities.GetDefaultFont();
		}

		public ProgressForm(string description1, string description2, bool showCancel)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.lblDescription.Text = description1;
			this.lblDescription2.Text = description2;
			this.ShowCancel(showCancel);
		}

		private void fmProgress_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			this.cancel = true;
		}

		private bool cancel;
	}
}
