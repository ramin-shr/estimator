using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace QuoterPlan
{
	public partial class PlanDuplicateForm : BaseForm
	{
		public event OnPlanDuplicateHandler OnDuplicate;

		public Plan Plan
		{
			get
			{
				return this.plan;
			}
		}

		private void LoadData()
		{
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		public PlanDuplicateForm(Plan plan, DrawingArea drawArea)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.slider1.Text = 1.ToString();
			this.plan = plan;
		}

		private void btDuplicate_Click(object sender, EventArgs e)
		{
			if (this.OnDuplicate != null)
			{
				this.OnDuplicate(this.Plan, this.slider1.Value, this.opYes.Checked);
			}
			base.Close();
		}

		private void slider1_ValueChanging(object sender, CancelIntValueEventArgs e)
		{
			this.slider1.Text = this.slider1.Value.ToString();
		}

		private void slider1_ValueChanged(object sender, EventArgs e)
		{
			this.slider1.Text = this.slider1.Value.ToString();
		}

		private Plan plan;

		private DrawingArea drawArea;
	}
}
