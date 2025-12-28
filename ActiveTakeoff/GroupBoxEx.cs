using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class GroupBoxEx : GroupBox
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

		public GroupBoxEx()
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

		protected override void OnPaint(PaintEventArgs pe)
		{
			pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			pe.Graphics.TextRenderingHint = this.TextRenderingHint;
			base.OnPaint(pe);
		}

		private IContainer components;

		private TextRenderingHint _hint = TextRenderingHint.ClearTypeGridFit;
	}
}
