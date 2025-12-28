using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class ComboBoxEx : ComboBox
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
		}

		public ComboBoxEx()
		{
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

		public Control AssociatedLabel
		{
			get
			{
				return this._associatedLabel;
			}
			set
			{
				this._associatedLabel = value;
			}
		}

		protected override void OnEnter(EventArgs e)
		{
			if (this.AssociatedLabel != null)
			{
				this.originalAssociatedLabelForeColor = this.AssociatedLabel.ForeColor;
				this.AssociatedLabel.Font = Utilities.GetDefaultFont(FontStyle.Bold);
				this.AssociatedLabel.ForeColor = Color.SteelBlue;
			}
			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			if (this.AssociatedLabel != null)
			{
				this.AssociatedLabel.Font = Utilities.GetDefaultFont();
				this.AssociatedLabel.ForeColor = this.originalAssociatedLabelForeColor;
			}
			base.OnLeave(e);
		}

		private IContainer components;

		private Color originalAssociatedLabelForeColor = Color.Empty;

		private TextRenderingHint _hint = TextRenderingHint.AntiAliasGridFit;

		private Control _associatedLabel;
	}
}
