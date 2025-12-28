using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class PlanPropertiesForm : BaseForm
	{
		public event OnPlanSelectedHandler OnPlanSelected;

		public event OnPlanSelectedHandler OnSave;

		public Plan Plan
		{
			get
			{
				return this.plans[this.planIndex];
			}
		}

		private void UpdateNavigation()
		{
			this.btNext.Enabled = (this.planIndex + 1 < this.plans.Count);
			this.btPrevious.Enabled = (this.planIndex > 0);
		}

		private void LoadData()
		{
			this.thumbnailPanel.Plan = this.Plan;
			this.txtPlanName.Text = this.Plan.Name;
			this.txtPlanSource.Text = this.Plan.FullFileName;
			this.txtComment.Text = this.Plan.Comment;
			Utilities.SetObjectFocus(this.txtPlanName);
			this.UpdateNavigation();
			if (this.OnPlanSelected != null)
			{
				this.OnPlanSelected(this.Plan);
			}
		}

		private bool SaveData()
		{
			string text = string.Empty;
			string title = string.Empty;
			string text2 = string.Empty;
			this.txtPlanName.Text = this.txtPlanName.Text.Trim();
			this.txtPlanSource.Text = this.txtPlanSource.Text.Trim();
			this.txtComment.Text = this.txtComment.Text.Trim();
			if (this.txtPlanName.Text == "")
			{
				title = Resources.Nom_de_plan_invalide;
				text2 = Resources.Vous_devez_spécifier_un_nom;
			}
			if (text2 == "" && this.Plan.Name != this.txtPlanName.Text)
			{
				text = this.txtPlanName.Text;
				this.drawArea.ValidatePlanName(ref text, ref title, ref text2);
				this.txtPlanName.Text = text;
			}
			if (text2 != "")
			{
				Utilities.DisplayError(title, text2);
				Utilities.SetObjectFocus(this.txtPlanName);
				return false;
			}
			this.Plan.PrevName = this.Plan.Name;
			this.Plan.Name = this.txtPlanName.Text;
			this.Plan.Comment = this.txtComment.Text;
			if (this.OnSave != null)
			{
				this.OnSave(this.Plan);
			}
			return true;
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		public PlanPropertiesForm(int planIndex, Plans plans, DrawingArea drawArea)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.thumbnailPanel.DrawArea = drawArea;
			this.thumbnailPanel.ThumbnailMarging = new Padding(10, 3, 0, 0);
			this.thumbnailPanel.ThumbnailPadding = new Padding(15, 38, 0, 0);
			this.thumbnailPanel.ThumbnailSize = new Size(256, 192);
			this.thumbnailPanel.DisplayName = false;
			this.thumbnailPanel.RecalcLayout();
			this.planIndex = planIndex;
			this.plans = plans;
			this.drawArea = drawArea;
			this.LoadData();
		}

		private void btPrevious_Click(object sender, EventArgs e)
		{
			if (!this.SaveData())
			{
				return;
			}
			this.planIndex--;
			this.planIndex = ((this.planIndex < 0) ? 0 : this.planIndex);
			this.LoadData();
		}

		private void btNext_Click(object sender, EventArgs e)
		{
			if (!this.SaveData())
			{
				return;
			}
			this.planIndex++;
			this.planIndex = ((this.planIndex == this.plans.Count) ? (this.plans.Count - 1) : this.planIndex);
			this.LoadData();
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			if (!this.SaveData())
			{
				return;
			}
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

		private void txtPlanName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.btNext_Click(sender, new EventArgs());
				Utilities.SelectText((TextBox)sender);
				e.Handled = true;
			}
		}

		private void lblPlanName_Click(object sender, EventArgs e)
		{
		}

		private void lblPlanSource_Click(object sender, EventArgs e)
		{
		}

		private int planIndex;

		private Plans plans;

		private DrawingArea drawArea;
	}
}
