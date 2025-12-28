using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class ProjectForm : BaseForm
	{
		private void LoadResources()
		{
			this.Text = (this.creationMode ? Resources.Nouveau_projet : Resources.Propriétés_du_projet);
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		public ProjectForm(Project project, bool creationMode)
		{
			this.InitializeComponent();
			this.creationMode = creationMode;
			this.project = project;
			if (project.CreationDate == "")
			{
				project.CreationDate = Utilities.FormatDate(DateTime.Now);
			}
			if (project.LastModified == "")
			{
				project.LastModified = Utilities.FormatDate(DateTime.Now);
			}
			this.creationParentFolder = Utilities.GetProjectsFolder();
			this.LoadResources();
			this.InitializeFonts();
			this.txtProjectName.Text = project.Name;
			this.txtProjectDescription.Text = project.Description;
			this.txtJobNumber.Text = project.JobNumber;
			this.txtContactName.Text = project.ContactName;
			this.txtContactInfo.Text = project.ContactInfo;
			this.txtComment.Text = project.Comment;
			this.txtCreationDate.Text = Utilities.GetDateString(project.CreationDate, Utilities.GetCurrentValidUICultureShort());
			this.txtLastModified.Text = Utilities.GetDateString(project.LastModified, Utilities.GetCurrentValidUICultureShort());
			this.txtProjectFolder.Text = (creationMode ? this.creationParentFolder : project.FolderName);
			this.btProjectFolder.Enabled = creationMode;
			project.Dirty = false;
		}

		private string GetProjectFolder()
		{
			string text = this.txtProjectName.Text.Trim();
			if (text != "")
			{
				string path = text + " (" + Utilities.GetDateString(this.project.CreationDate, Utilities.GetCurrentValidUICultureShort()).Replace(",", "").Replace(" ", "-") + ")";
				string folder = Path.Combine(this.creationParentFolder, path);
				return Utilities.MakeUniqueFolderName(folder);
			}
			return this.creationParentFolder;
		}

		private void SelectFolder()
		{
			string a = Utilities.SelectFolder(this.creationParentFolder);
			if (a != "")
			{
				this.creationParentFolder = a;
				this.txtProjectFolder.Text = Utilities.TruncatePath(this.GetProjectFolder(), 80);
				Utilities.SetObjectFocus(this.txtProjectFolder);
			}
		}

		private void btProjectFolder_Click(object sender, EventArgs e)
		{
			this.SelectFolder();
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			string title = string.Empty;
			string text = string.Empty;
			this.txtProjectName.Text = this.txtProjectName.Text.Trim();
			this.txtProjectDescription.Text = this.txtProjectDescription.Text.Trim();
			this.txtContactName.Text = this.txtContactName.Text.Trim();
			this.txtContactInfo.Text = this.txtContactInfo.Text.Trim();
			this.txtComment.Text = this.txtComment.Text.Trim();
			if (this.txtProjectName.Text == "")
			{
				title = Resources.Nom_de_projet_invalide;
				text = Resources.Vous_devez_spécifier_un_nom_de_projet;
			}
			if (text == "" && !Utilities.ValidateVariableName(this.txtProjectName.Text, "/"))
			{
				title = Resources.Nom_de_projet_invalide;
				text = Resources.Les_caractères_suivants_sont_invalides + "\n/ " + Utilities.InvalidCharacters();
			}
			if (text != "")
			{
				Utilities.DisplayError(title, text);
				Utilities.SetObjectFocus(this.txtProjectName);
				return;
			}
			this.project.Name = this.txtProjectName.Text;
			this.project.Description = this.txtProjectDescription.Text;
			this.project.ContactName = this.txtContactName.Text;
			this.project.ContactInfo = this.txtContactInfo.Text;
			this.project.JobNumber = this.txtJobNumber.Text;
			this.project.Comment = this.txtComment.Text;
			if (this.creationMode)
			{
				this.project.CreationParentFolder = this.creationParentFolder;
			}
			this.project.Dirty = true;
			base.Close();
		}

		private void txtField_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private void txtMultiLine_Enter(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.SelectionStart = textBox.TextLength + 1;
		}

		private void txtProjectName_TextChanged(object sender, EventArgs e)
		{
			if (this.creationMode)
			{
				this.txtProjectFolder.Text = Utilities.TruncatePath(this.GetProjectFolder(), 80);
			}
		}

		private bool creationMode;

		private Project project;

		private string creationParentFolder;
	}
}
