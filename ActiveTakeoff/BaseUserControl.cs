using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class BaseUserControl : UserControl
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			base.AutoScaleMode = AutoScaleMode.Font;
		}

		public BaseUserControl()
		{
			this.InitializeComponent();
			base.AutoScaleMode = Utilities.GetAutoScaleMode();
			this.Font = Utilities.GetDefaultFont();
		}

		private IContainer components;
	}
}
