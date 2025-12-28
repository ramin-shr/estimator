namespace QuoterPlan
{
	public partial class NoteEditForm : global::QuoterPlan.BaseForm
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.NoteEditForm));
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.txtComment = new global::QuoterPlan.TextBoxEx();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOk_Click);
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			componentResourceManager.ApplyResources(this.txtComment, "txtComment");
			this.txtComment.AssociatedLabel = null;
			this.txtComment.Name = "txtComment";
			this.txtComment.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.txtComment.MouseEnter += new global::System.EventHandler(this.txtComment_MouseEnter);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btCancel;
			base.Controls.Add(this.txtComment);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "NoteEditForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::QuoterPlan.TextBoxEx txtComment;
	}
}
