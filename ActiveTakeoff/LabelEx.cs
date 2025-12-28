using System;
using System.ComponentModel;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class LabelEx : Label
	{
		public LabelEx()
		{
			this.InitializeComponent();
		}

		public TextRenderingHint TextRenderingHint
		{
			get
			{
				return this._hint;
			}
			set
			{
				this._hint = value;
			}
		}

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
		}

		private TextRenderingHint _hint = TextRenderingHint.ClearTypeGridFit;

		private IContainer components;
	}
}
