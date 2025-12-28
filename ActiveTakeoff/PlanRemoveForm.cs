using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
	public partial class PlanRemoveForm : BaseForm
	{
		public event OnPlanSelectedHandler OnPlanSelected;

		public event OnPlanSelectedHandler OnRemove;

		public event OnPlanSelectedHandler OnRemoveWithData;

		public Plan Plan
		{
			get
			{
				return this.plan;
			}
		}

		private void LoadData()
		{
			this.thumbnailPanel.Plan = this.Plan;
			this.txtPlanSource.Text = this.Plan.FullFileName;
			if (this.OnPlanSelected != null)
			{
				this.OnPlanSelected(this.Plan);
			}
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		public PlanRemoveForm(Plan plan, DrawingArea drawArea)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.thumbnailPanel.DrawArea = drawArea;
			this.thumbnailPanel.ThumbnailMarging = new Padding(10, 3, 0, 0);
			this.thumbnailPanel.ThumbnailPadding = new Padding(15, 38, 0, 0);
			this.thumbnailPanel.ThumbnailSize = new Size(256, 192);
			this.thumbnailPanel.DisplayName = true;
			this.thumbnailPanel.RecalcLayout();
			this.plan = plan;
			this.LoadData();
		}

		private void btRemove_Click(object sender, EventArgs e)
		{
			if (this.OnRemove != null)
			{
				this.OnRemove(this.Plan);
			}
			base.Close();
		}

		private void btRemoveWithData_Click(object sender, EventArgs e)
		{
			if (this.OnRemoveWithData != null)
			{
				this.OnRemoveWithData(this.Plan);
			}
			base.Close();
		}

		private void txtField_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private Plan plan;

		private DrawingArea drawArea;
	}
}
