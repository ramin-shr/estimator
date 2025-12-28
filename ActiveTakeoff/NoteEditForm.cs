using System;
using System.ComponentModel;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class NoteEditForm : BaseForm
	{
		public NoteEditForm(DrawNote drawNote, bool creationMode)
		{
			this.InitializeComponent();
			this.Text = (creationMode ? Resources.Insérer_une_nouvelle_note : Resources.Modifier_la_note);
			this.drawNote = drawNote;
			this.txtComment.Text = drawNote.Comment;
			this.txtComment.SelectionStart = this.txtComment.TextLength + 1;
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			this.txtComment.Text = this.txtComment.Text.Trim();
			if (this.txtComment.Text == string.Empty)
			{
				Utilities.SetObjectFocus(this.txtComment);
				return;
			}
			this.drawNote.Comment = this.txtComment.Text;
			this.drawNote.Dirty = true;
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void txtComment_MouseEnter(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.SelectionStart = textBox.TextLength + 1;
		}

		private DrawNote drawNote;
	}
}
