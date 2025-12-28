using System;
using System.ComponentModel;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class DimensionForm : BaseForm
	{
		private void InitializeDimensionControl(UnitScale unitScale, double value)
		{
			this.dimensionInput = new DimensionInput(unitScale.ScaleSystemType, unitScale.CurrentSystemType, value);
			this.dimensionInput.AutoScaleMode = AutoScaleMode.Inherit;
			this.dimensionInput.Font = Utilities.GetDefaultFont();
			this.dimensionInput.Width = 200;
			this.dimensionInput.AssociatedLabel = this.label1;
			this.panel1.Controls.Add(this.dimensionInput);
		}

		public DimensionForm(string caption, UnitScale uniScale, GenericValue dimension)
		{
			this.InitializeComponent();
			this.label1.Text = caption;
			this.dimension = dimension;
			this.dimension.Dirty = false;
			this.InitializeDimensionControl(uniScale, Utilities.ConvertToDouble(dimension.Value, -1));
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			if (this.dimensionInput.Value == 0.0)
			{
				return;
			}
			this.dimension.Dirty = true;
			this.dimension.Value = this.dimensionInput.Value;
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void DimensionForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.btOk_Click(this, new EventArgs());
			}
		}

		private GenericValue dimension;

		private DimensionInput dimensionInput;
	}
}
