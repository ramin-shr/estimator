using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace QuoterPlan
{
	public partial class BaseForm : Office2007Form
	{
		public string HelpContextString { get; set; }

		public HelpUtilities HelpUtilities { get; set; }

		public BaseForm()
		{
			this.InitializeComponent();
			base.AutoScaleMode = Utilities.GetAutoScaleMode();
			this.Font = Utilities.GetDefaultFont();
			base.CaptionFont = Utilities.GetDefaultFont();
			base.Opacity = 0.94;
			this.HelpContextString = string.Empty;
		}

		private void BaseForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1 && this.HelpUtilities != null)
			{
				this.HelpUtilities.ShowHelp(this.HelpContextString);
			}
		}
	}
}
