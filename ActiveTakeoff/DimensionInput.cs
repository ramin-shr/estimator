using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class DimensionInput : BaseUserControl
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(DimensionInput));
			this.txtFeet = new TextBoxEx();
			this.lblFeet = new Label();
			this.txtInches = new TextBoxEx();
			this.lblInches = new Label();
			this.btSwitch = new Button();
			this.txtMetric = new TextBoxEx();
			this.lblMetric = new Label();
			base.SuspendLayout();
			this.txtFeet.AssociatedLabel = null;
			this.txtFeet.BackColor = SystemColors.Window;
			this.txtFeet.Location = new Point(0, 0);
			this.txtFeet.MaxLength = 6;
			this.txtFeet.Name = "txtFeet";
			this.txtFeet.Size = new Size(62, 23);
			this.txtFeet.TabIndex = 0;
			this.txtFeet.Text = "0";
			this.txtFeet.TextAlign = HorizontalAlignment.Center;
			this.txtFeet.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			this.txtFeet.Click += this.txtNumeric_Enter;
			this.txtFeet.Enter += this.txtNumeric_Enter;
			this.txtFeet.KeyPress += this.txtNumeric_KeyPress;
			this.txtFeet.Validating += new CancelEventHandler(this.txtNumeric_Validating);
			this.lblFeet.AutoSize = true;
			this.lblFeet.BackColor = Color.Transparent;
			this.lblFeet.Location = new Point(62, 0);
			this.lblFeet.Name = "lblFeet";
			this.lblFeet.Padding = new Padding(0, 0, 2, 0);
			this.lblFeet.Size = new Size(12, 15);
			this.lblFeet.TabIndex = 82;
			this.lblFeet.Text = "'";
			this.lblFeet.TextAlign = ContentAlignment.MiddleCenter;
			this.txtInches.AssociatedLabel = null;
			this.txtInches.Location = new Point(74, 0);
			this.txtInches.MaxLength = 5;
			this.txtInches.Name = "txtInches";
			this.txtInches.Size = new Size(40, 23);
			this.txtInches.TabIndex = 1;
			this.txtInches.Text = "0";
			this.txtInches.TextAlign = HorizontalAlignment.Center;
			this.txtInches.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			this.txtInches.Click += this.txtNumeric_Enter;
			this.txtInches.Enter += this.txtNumeric_Enter;
			this.txtInches.KeyPress += this.txtNumeric_KeyPress;
			this.txtInches.Validating += new CancelEventHandler(this.txtNumeric_Validating);
			this.lblInches.AutoSize = true;
			this.lblInches.BackColor = Color.Transparent;
			this.lblInches.Location = new Point(116, 0);
			this.lblInches.Name = "lblInches";
			this.lblInches.Size = new Size(12, 15);
			this.lblInches.TabIndex = 84;
			this.lblInches.Text = "\"";
			this.lblInches.TextAlign = ContentAlignment.MiddleCenter;
			this.btSwitch.Dock = DockStyle.Right;
			this.btSwitch.Image = (Image)componentResourceManager.GetObject("btSwitch.Image");
			this.btSwitch.Location = new Point(137, 0);
			this.btSwitch.Name = "btSwitch";
			this.btSwitch.Size = new Size(31, 24);
			this.btSwitch.TabIndex = 3;
			this.btSwitch.UseVisualStyleBackColor = true;
			this.btSwitch.Click += this.btSwitch_Click;
			this.txtMetric.AssociatedLabel = null;
			this.txtMetric.Location = new Point(0, 0);
			this.txtMetric.MaxLength = 9;
			this.txtMetric.Name = "txtMetric";
			this.txtMetric.Size = new Size(111, 23);
			this.txtMetric.TabIndex = 2;
			this.txtMetric.Text = "0";
			this.txtMetric.TextAlign = HorizontalAlignment.Center;
			this.txtMetric.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			this.txtMetric.Visible = false;
			this.txtMetric.Click += this.txtNumeric_Enter;
			this.txtMetric.Enter += this.txtNumeric_Enter;
			this.txtMetric.KeyPress += this.txtNumeric_KeyPress;
			this.txtMetric.Validating += new CancelEventHandler(this.txtNumeric_Validating);
			this.lblMetric.BackColor = Color.Transparent;
			this.lblMetric.Location = new Point(112, 0);
			this.lblMetric.Name = "lblMetric";
			this.lblMetric.Size = new Size(32, 15);
			this.lblMetric.TabIndex = 87;
			this.lblMetric.Text = "mm";
			this.lblMetric.TextAlign = ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.btSwitch);
			base.Controls.Add(this.lblInches);
			base.Controls.Add(this.txtInches);
			base.Controls.Add(this.lblFeet);
			base.Controls.Add(this.lblMetric);
			base.Controls.Add(this.txtFeet);
			base.Controls.Add(this.txtMetric);
			base.Name = "DimensionInput";
			base.Size = new Size(168, 24);
			base.Resize += this.DimensionInput_Resize;
			base.ResumeLayout(false);
			base.PerformLayout();
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

		public UnitScale.UnitSystem CurrentSystemType
		{
			get
			{
				return this.currentSystemType;
			}
		}

		private double ToCentimeters(double value)
		{
			return value * 2.54;
		}

		private double ToInches(double value)
		{
			return value * 0.393700787;
		}

		public double CurrentSystemValue
		{
			get
			{
				if (this.currentSystemType == UnitScale.UnitSystem.imperial)
				{
					double num = Utilities.ConvertToDouble(this.txtFeet.Text, -1);
					double num2 = Utilities.ConvertToDouble(this.txtInches.Text, -1);
					return num * 12.0 + num2;
				}
				return Utilities.ConvertToDouble(this.txtMetric.Text, -1);
			}
		}

		public double Value
		{
			get
			{
				if (this.systemType == this.currentSystemType)
				{
					return this.CurrentSystemValue;
				}
				if (this.systemType != UnitScale.UnitSystem.imperial)
				{
					return this.ToCentimeters(this.CurrentSystemValue) * 10.0;
				}
				return this.ToInches(this.CurrentSystemValue / 10.0);
			}
			set
			{
				if (this.currentSystemType == UnitScale.UnitSystem.imperial)
				{
					KeyValuePair<int, float> keyValuePair = UnitScale.ToFeetInches((float)value);
					int key = keyValuePair.Key;
					double num = Math.Round((double)keyValuePair.Value, 3);
					this.txtFeet.Text = key.ToString();
					this.txtInches.Text = num.ToString();
					return;
				}
				this.txtMetric.Text = value.ToString();
			}
		}

		private void SetSystemType(UnitScale.UnitSystem systemType)
		{
			if (this.currentSystemType == systemType && this.currentSystemType != UnitScale.UnitSystem.undefined)
			{
				return;
			}
			if (this.currentSystemType == UnitScale.UnitSystem.imperial)
			{
				double a = this.ToCentimeters(this.CurrentSystemValue) * 10.0;
				int num = (int)Math.Ceiling(a);
				this.txtMetric.Text = num.ToString();
			}
			else
			{
				double num2 = this.ToInches(this.CurrentSystemValue / 10.0);
				KeyValuePair<int, float> keyValuePair = UnitScale.ToFeetInches((float)num2);
				int key = keyValuePair.Key;
				double num3 = Math.Round((double)keyValuePair.Value, 3);
				this.txtFeet.Text = key.ToString();
				this.txtInches.Text = num3.ToString();
			}
			this.buttonToolTip.SetToolTip(this.btSwitch, (systemType == UnitScale.UnitSystem.imperial) ? Resources.Basculer_vers_métrique : Resources.Basculer_vers_impérial);
			this.lblFeet.Visible = (systemType == UnitScale.UnitSystem.imperial);
			this.txtFeet.Visible = (systemType == UnitScale.UnitSystem.imperial);
			this.lblInches.Visible = (systemType == UnitScale.UnitSystem.imperial);
			this.txtInches.Visible = (systemType == UnitScale.UnitSystem.imperial);
			this.lblMetric.Visible = (systemType == UnitScale.UnitSystem.metric);
			this.txtMetric.Visible = (systemType == UnitScale.UnitSystem.metric);
			this.currentSystemType = systemType;
			Utilities.SetObjectFocus((this.currentSystemType == UnitScale.UnitSystem.imperial) ? this.txtFeet : this.txtMetric);
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
		}

		private void InitializeGui()
		{
			this.buttonToolTip.AutoPopDelay = 5000;
			this.buttonToolTip.InitialDelay = 1000;
			this.buttonToolTip.ReshowDelay = 500;
			this.buttonToolTip.ShowAlways = true;
		}

		public DimensionInput(UnitScale.UnitSystem systemType, UnitScale.UnitSystem currentSystemType, double defaultValue)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.InitializeGui();
			this.systemType = systemType;
			this.SetSystemType(currentSystemType);
			this.Value = Math.Round(defaultValue, 3);
		}

		private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			string text = "0123456789" + ((textBox.Name == "txtFeet") ? "" : ",.");
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\r' && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtNumeric_Validating(object sender, CancelEventArgs e)
		{
			((TextBox)sender).Text = Utilities.ConvertToDouble(((TextBox)sender).Text, -1).ToString();
		}

		private void txtNumeric_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private void btSwitch_Click(object sender, EventArgs e)
		{
			this.SetSystemType((this.currentSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial);
		}

		private void DimensionInput_Resize(object sender, EventArgs e)
		{
			this.txtFeet.Left = 0;
			this.txtFeet.Width = base.Width - (this.lblFeet.Width + this.txtInches.Width + this.lblInches.Width + this.btSwitch.Width + 1);
			this.lblFeet.Left = this.txtFeet.Left + this.txtFeet.Width;
			this.txtInches.Left = this.lblFeet.Left + this.lblFeet.Width + 1;
			this.lblInches.Left = this.txtInches.Left + this.txtInches.Width;
			this.txtMetric.Width = base.Width - (this.lblMetric.Width + this.btSwitch.Width + 1);
			this.lblMetric.Left = this.txtMetric.Left + this.txtMetric.Width + 1;
			this.btSwitch.Left = base.Width - this.btSwitch.Width;
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

		private TextBoxEx txtFeet;

		private Label lblFeet;

		private TextBoxEx txtInches;

		private Label lblInches;

		private Button btSwitch;

		private TextBoxEx txtMetric;

		private Label lblMetric;

		private Color originalBackColor = Color.Empty;

		private Color originalAssociatedLabelForeColor = Color.Empty;

		private UnitScale.UnitSystem systemType = UnitScale.UnitSystem.undefined;

		private UnitScale.UnitSystem currentSystemType = UnitScale.UnitSystem.undefined;

		private ToolTip buttonToolTip = new ToolTip();

		private Control _associatedLabel;
	}
}
