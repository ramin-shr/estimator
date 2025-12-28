using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class DimensionInputEx : BaseUserControl
	{
		public double Value
		{
			get
			{
				return this.openingHeightDimensionInput.Value;
			}
		}

		private void InitializeDimensionControl()
		{
			this.openingHeightDimensionInput = new DimensionInput(this.uniScale.ScaleSystemType, this.uniScale.CurrentSystemType, this.height);
			this.openingHeightDimensionInput.AutoScaleMode = AutoScaleMode.Inherit;
			this.openingHeightDimensionInput.Font = Utilities.GetDefaultFont();
			this.openingHeightDimensionInput.Width = 150;
			this.panel1.Controls.Add(this.openingHeightDimensionInput);
		}

		public DimensionInputEx(string caption, UnitScale uniScale, double height)
		{
			this.InitializeComponent();
			this.label1.Text = caption;
			this.uniScale = uniScale;
			this.height = height;
			this.InitializeDimensionControl();
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.panel1 = new Panel();
			this.label2 = new Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(1, 4);
			this.label1.Name = "label1";
			this.label1.Size = new Size(38, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			this.panel1.Location = new Point(101, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(150, 25);
			this.panel1.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(258, 4);
			this.label2.Name = "label2";
			this.label2.Size = new Size(16, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "H";
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label1);
			base.Name = "DimensionInputEx";
			base.Size = new Size(280, 26);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private UnitScale uniScale;

		private double height;

		private Label label1;

		private Panel panel1;

		private Label label2;

		private DimensionInput openingHeightDimensionInput;
	}
}
