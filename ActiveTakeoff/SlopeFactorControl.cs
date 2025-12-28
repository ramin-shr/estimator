using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class SlopeFactorControl : UserControl
	{
		public event OnSlopeFactorSaveHandler OnSave
		{
			add
			{
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler = this.OnSave;
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler2;
				do
				{
					onSlopeFactorSaveHandler2 = onSlopeFactorSaveHandler;
					OnSlopeFactorSaveHandler value2 = (OnSlopeFactorSaveHandler)Delegate.Combine(onSlopeFactorSaveHandler2, value);
					onSlopeFactorSaveHandler = Interlocked.CompareExchange<OnSlopeFactorSaveHandler>(ref this.OnSave, value2, onSlopeFactorSaveHandler2);
				}
				while (onSlopeFactorSaveHandler != onSlopeFactorSaveHandler2);
			}
			remove
			{
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler = this.OnSave;
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler2;
				do
				{
					onSlopeFactorSaveHandler2 = onSlopeFactorSaveHandler;
					OnSlopeFactorSaveHandler value2 = (OnSlopeFactorSaveHandler)Delegate.Remove(onSlopeFactorSaveHandler2, value);
					onSlopeFactorSaveHandler = Interlocked.CompareExchange<OnSlopeFactorSaveHandler>(ref this.OnSave, value2, onSlopeFactorSaveHandler2);
				}
				while (onSlopeFactorSaveHandler != onSlopeFactorSaveHandler2);
			}
		}

		public event OnSlopeFactorCancelHandler OnCancel
		{
			add
			{
				OnSlopeFactorCancelHandler onSlopeFactorCancelHandler = this.OnCancel;
				OnSlopeFactorCancelHandler onSlopeFactorCancelHandler2;
				do
				{
					onSlopeFactorCancelHandler2 = onSlopeFactorCancelHandler;
					OnSlopeFactorCancelHandler value2 = (OnSlopeFactorCancelHandler)Delegate.Combine(onSlopeFactorCancelHandler2, value);
					onSlopeFactorCancelHandler = Interlocked.CompareExchange<OnSlopeFactorCancelHandler>(ref this.OnCancel, value2, onSlopeFactorCancelHandler2);
				}
				while (onSlopeFactorCancelHandler != onSlopeFactorCancelHandler2);
			}
			remove
			{
				OnSlopeFactorCancelHandler onSlopeFactorCancelHandler = this.OnCancel;
				OnSlopeFactorCancelHandler onSlopeFactorCancelHandler2;
				do
				{
					onSlopeFactorCancelHandler2 = onSlopeFactorCancelHandler;
					OnSlopeFactorCancelHandler value2 = (OnSlopeFactorCancelHandler)Delegate.Remove(onSlopeFactorCancelHandler2, value);
					onSlopeFactorCancelHandler = Interlocked.CompareExchange<OnSlopeFactorCancelHandler>(ref this.OnCancel, value2, onSlopeFactorCancelHandler2);
				}
				while (onSlopeFactorCancelHandler != onSlopeFactorCancelHandler2);
			}
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.Font = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.cbSlopeType.Font = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.opSlope12.Font = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.opSlopeDegree.Font = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.opSlopePercentage.Font = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.sliderSlope.Font = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.btOk.Font = Utilities.GetDefaultFont(FontStyle.Regular);
			this.btCancel.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		private void InitializeGui()
		{
			this.cbSlopeType.Items.Clear();
			this.cbSlopeType.Items.Add(Resources.Appliquer_en_vue_d_élévation);
			this.cbSlopeType.Items.Add(Resources.Appliquer_en_vue_de_plan);
			this.chkHipsValleys.Checked = (this.slopeFactor.HipValley == SlopeFactor.HipValleyEnum.hipValleyEnabled);
			this.cbSlopeType.SelectedIndex = (int)this.slopeFactor.SlopeApplyType;
			this.opSlopePercentage.Checked = (this.slopeFactor.SlopeType == SlopeFactor.SlopeTypeEnum.slopeTypePercentage);
			this.opSlopeDegree.Checked = (this.slopeFactor.SlopeType == SlopeFactor.SlopeTypeEnum.slopeTypeDegree);
			this.opSlope12.Checked = (this.slopeFactor.SlopeType == SlopeFactor.SlopeTypeEnum.slopeType12);
			this.UpdateSlope();
			this.UpdateSlopeType(this.slopeFactor.SlopeType);
		}

		public SlopeFactorControl(SlopeFactor slopeFactor)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.slopeFactor.SetValues(slopeFactor);
			this.InitializeGui();
		}

		private void cbSlopeType_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.slopeFactor.SlopeApplyType = (SlopeFactor.SlopeApplyTypeEnum)this.cbSlopeType.SelectedIndex;
		}

		private void UpdateSlopeType(SlopeFactor.SlopeTypeEnum slopeType)
		{
			this.slopeFactor.SlopeType = slopeType;
			int num = this.slopeFactor.GetSlope();
			switch (slopeType)
			{
			case SlopeFactor.SlopeTypeEnum.slopeTypePercentage:
				if (num > 400)
				{
					num = 0;
				}
				this.sliderSlope.Maximum = 400;
				break;
			case SlopeFactor.SlopeTypeEnum.slopeTypeDegree:
				if (num > 89)
				{
					num = 0;
				}
				this.sliderSlope.Maximum = 89;
				break;
			case SlopeFactor.SlopeTypeEnum.slopeType12:
				if (num > 50)
				{
					num = 0;
				}
				this.sliderSlope.Maximum = 50;
				break;
			}
			try
			{
				this.sliderSlope.Value = num;
			}
			catch
			{
				this.sliderSlope.Value = 0;
			}
		}

		private void opSlope_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == null)
			{
				return;
			}
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Checked)
			{
				return;
			}
			string name;
			if ((name = radioButton.Name) != null)
			{
				if (name == "opSlopePercentage")
				{
					this.UpdateSlopeType(SlopeFactor.SlopeTypeEnum.slopeTypePercentage);
					return;
				}
				if (name == "opSlopeDegree")
				{
					this.UpdateSlopeType(SlopeFactor.SlopeTypeEnum.slopeTypeDegree);
					return;
				}
				if (!(name == "opSlope12"))
				{
					return;
				}
				this.UpdateSlopeType(SlopeFactor.SlopeTypeEnum.slopeType12);
			}
		}

		private void UpdateSlope()
		{
			bool flag = this.sliderSlope.Value > 0;
			this.cbSlopeType.Enabled = flag;
			this.chkHipsValleys.Enabled = (flag && this.slopeFactor.HipValley != SlopeFactor.HipValleyEnum.hipValleyUnavailable);
			this.sliderSlope.Text = this.slopeFactor.GetSlopeString();
		}

		private void chkHipsValleys_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkHipsValleys.Checked)
			{
				this.slopeFactor.HipValley = SlopeFactor.HipValleyEnum.hipValleyEnabled;
				return;
			}
			this.slopeFactor.HipValley = SlopeFactor.HipValleyEnum.hipValleyDisabled;
		}

		private void sliderSlope_ValueChanged(object sender, EventArgs e)
		{
			this.slopeFactor.SetSlope(this.sliderSlope.Value);
			this.UpdateSlope();
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			if (this.OnSave != null)
			{
				Settings.Default.SlopeType = (int)this.slopeFactor.SlopeType;
				this.OnSave(this.slopeFactor);
			}
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			if (this.OnCancel != null)
			{
				this.OnCancel();
			}
		}

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SlopeFactorControl));
			this.opSlope12 = new RadioButton();
			this.opSlopeDegree = new RadioButton();
			this.opSlopePercentage = new RadioButton();
			this.sliderSlope = new Slider();
			this.cbSlopeType = new ComboBox();
			this.btOk = new Button();
			this.btCancel = new Button();
			this.chkHipsValleys = new CheckBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.opSlope12, "opSlope12");
			this.opSlope12.Name = "opSlope12";
			this.opSlope12.UseVisualStyleBackColor = true;
			this.opSlope12.CheckedChanged += this.opSlope_CheckedChanged;
			componentResourceManager.ApplyResources(this.opSlopeDegree, "opSlopeDegree");
			this.opSlopeDegree.Checked = true;
			this.opSlopeDegree.Name = "opSlopeDegree";
			this.opSlopeDegree.TabStop = true;
			this.opSlopeDegree.UseVisualStyleBackColor = true;
			this.opSlopeDegree.CheckedChanged += this.opSlope_CheckedChanged;
			componentResourceManager.ApplyResources(this.opSlopePercentage, "opSlopePercentage");
			this.opSlopePercentage.Name = "opSlopePercentage";
			this.opSlopePercentage.UseVisualStyleBackColor = true;
			this.opSlopePercentage.CheckedChanged += this.opSlope_CheckedChanged;
			this.sliderSlope.BackgroundStyle.CornerType = eCornerType.Square;
			this.sliderSlope.LabelPosition = eSliderLabelPosition.Bottom;
			componentResourceManager.ApplyResources(this.sliderSlope, "sliderSlope");
			this.sliderSlope.Name = "sliderSlope";
			this.sliderSlope.Style = eDotNetBarStyle.StyleManagerControlled;
			this.sliderSlope.TrackMarker = false;
			this.sliderSlope.Value = 0;
			this.sliderSlope.ValueChanged += this.sliderSlope_ValueChanged;
			this.cbSlopeType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbSlopeType.FormattingEnabled = true;
			this.cbSlopeType.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cbSlopeType.Items"),
				componentResourceManager.GetString("cbSlopeType.Items1")
			});
			componentResourceManager.ApplyResources(this.cbSlopeType, "cbSlopeType");
			this.cbSlopeType.Name = "cbSlopeType";
			this.cbSlopeType.SelectedIndexChanged += this.cbSlopeType_SelectedIndexChanged;
			componentResourceManager.ApplyResources(this.btOk, "btOk");
			this.btOk.Name = "btOk";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += this.btOk_Click;
			componentResourceManager.ApplyResources(this.btCancel, "btCancel");
			this.btCancel.DialogResult = DialogResult.Cancel;
			this.btCancel.Name = "btCancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += this.btCancel_Click;
			componentResourceManager.ApplyResources(this.chkHipsValleys, "chkHipsValleys");
			this.chkHipsValleys.Name = "chkHipsValleys";
			this.chkHipsValleys.UseVisualStyleBackColor = true;
			this.chkHipsValleys.CheckedChanged += this.chkHipsValleys_CheckedChanged;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.WhiteSmoke;
			base.Controls.Add(this.chkHipsValleys);
			base.Controls.Add(this.opSlopePercentage);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.cbSlopeType);
			base.Controls.Add(this.sliderSlope);
			base.Controls.Add(this.opSlopeDegree);
			base.Controls.Add(this.opSlope12);
			base.Name = "SlopeFactorControl";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private OnSlopeFactorSaveHandler OnSave;

		private OnSlopeFactorCancelHandler OnCancel;

		private SlopeFactor slopeFactor = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyUnavailable);

		private IContainer components;

		private RadioButton opSlope12;

		private RadioButton opSlopeDegree;

		private RadioButton opSlopePercentage;

		private Slider sliderSlope;

		private ComboBox cbSlopeType;

		private Button btOk;

		private Button btCancel;

		private CheckBox chkHipsValleys;
	}
}
