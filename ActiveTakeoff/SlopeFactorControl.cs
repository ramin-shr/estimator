using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class SlopeFactorControl : UserControl
    {
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

        public SlopeFactorControl(SlopeFactor slopeFactor)
        {
            this.InitializeComponent();
            this.LoadResources();
            this.InitializeFonts();
            this.slopeFactor.SetValues(slopeFactor);
            this.InitializeGui();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (this.OnCancel != null)
            {
                this.OnCancel();
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (this.OnSave != null)
            {
                Settings.Default.SlopeType = (int)this.slopeFactor.SlopeType;
                this.OnSave(this.slopeFactor);
            }
        }

        private void cbSlopeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.slopeFactor.SlopeApplyType = (SlopeFactor.SlopeApplyTypeEnum)this.cbSlopeType.SelectedIndex;
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
            this.opSlope12.CheckedChanged += new EventHandler(this.opSlope_CheckedChanged);
            componentResourceManager.ApplyResources(this.opSlopeDegree, "opSlopeDegree");
            this.opSlopeDegree.Checked = true;
            this.opSlopeDegree.Name = "opSlopeDegree";
            this.opSlopeDegree.TabStop = true;
            this.opSlopeDegree.UseVisualStyleBackColor = true;
            this.opSlopeDegree.CheckedChanged += new EventHandler(this.opSlope_CheckedChanged);
            componentResourceManager.ApplyResources(this.opSlopePercentage, "opSlopePercentage");
            this.opSlopePercentage.Name = "opSlopePercentage";
            this.opSlopePercentage.UseVisualStyleBackColor = true;
            this.opSlopePercentage.CheckedChanged += new EventHandler(this.opSlope_CheckedChanged);
            this.sliderSlope.BackgroundStyle.CornerType = eCornerType.Square;
            this.sliderSlope.LabelPosition = eSliderLabelPosition.Bottom;
            componentResourceManager.ApplyResources(this.sliderSlope, "sliderSlope");
            this.sliderSlope.Name = "sliderSlope";
            this.sliderSlope.Style = eDotNetBarStyle.StyleManagerControlled;
            this.sliderSlope.TrackMarker = false;
            this.sliderSlope.Value = 0;
            this.sliderSlope.ValueChanged += new EventHandler(this.sliderSlope_ValueChanged);
            this.cbSlopeType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbSlopeType.FormattingEnabled = true;
            ComboBox.ObjectCollection items = this.cbSlopeType.Items;
            object[] str = new object[] { componentResourceManager.GetString("cbSlopeType.Items"), componentResourceManager.GetString("cbSlopeType.Items1") };
            items.AddRange(str);
            componentResourceManager.ApplyResources(this.cbSlopeType, "cbSlopeType");
            this.cbSlopeType.Name = "cbSlopeType";
            this.cbSlopeType.SelectedIndexChanged += new EventHandler(this.cbSlopeType_SelectedIndexChanged);
            componentResourceManager.ApplyResources(this.btOk, "btOk");
            this.btOk.Name = "btOk";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new EventHandler(this.btOk_Click);
            componentResourceManager.ApplyResources(this.btCancel, "btCancel");
            this.btCancel.DialogResult = DialogResult.Cancel;
            this.btCancel.Name = "btCancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new EventHandler(this.btCancel_Click);
            componentResourceManager.ApplyResources(this.chkHipsValleys, "chkHipsValleys");
            this.chkHipsValleys.Name = "chkHipsValleys";
            this.chkHipsValleys.UseVisualStyleBackColor = true;
            this.chkHipsValleys.CheckedChanged += new EventHandler(this.chkHipsValleys_CheckedChanged);
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
            this.chkHipsValleys.Checked = this.slopeFactor.HipValley == SlopeFactor.HipValleyEnum.hipValleyEnabled;
            this.cbSlopeType.SelectedIndex = (int)this.slopeFactor.SlopeApplyType;
            this.opSlopePercentage.Checked = this.slopeFactor.SlopeType == SlopeFactor.SlopeTypeEnum.slopeTypePercentage;
            this.opSlopeDegree.Checked = this.slopeFactor.SlopeType == SlopeFactor.SlopeTypeEnum.slopeTypeDegree;
            this.opSlope12.Checked = this.slopeFactor.SlopeType == SlopeFactor.SlopeTypeEnum.slopeType12;
            this.UpdateSlope();
            this.UpdateSlopeType(this.slopeFactor.SlopeType);
        }

        private void LoadResources()
        {
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
            string name = radioButton.Name;
            string str = name;
            if (name != null)
            {
                if (str == "opSlopePercentage")
                {
                    this.UpdateSlopeType(SlopeFactor.SlopeTypeEnum.slopeTypePercentage);
                    return;
                }
                if (str == "opSlopeDegree")
                {
                    this.UpdateSlopeType(SlopeFactor.SlopeTypeEnum.slopeTypeDegree);
                    return;
                }
                if (str != "opSlope12")
                {
                    return;
                }
                this.UpdateSlopeType(SlopeFactor.SlopeTypeEnum.slopeType12);
            }
        }

        private void sliderSlope_ValueChanged(object sender, EventArgs e)
        {
            this.slopeFactor.SetSlope(this.sliderSlope.Value);
            this.UpdateSlope();
        }

        private void UpdateSlope()
        {
            bool value = this.sliderSlope.Value > 0;
            this.cbSlopeType.Enabled = value;
            this.chkHipsValleys.Enabled = (!value ? false : this.slopeFactor.HipValley != SlopeFactor.HipValleyEnum.hipValleyUnavailable);
            this.sliderSlope.Text = this.slopeFactor.GetSlopeString();
        }

        private void UpdateSlopeType(SlopeFactor.SlopeTypeEnum slopeType)
        {
            this.slopeFactor.SlopeType = slopeType;
            int slope = this.slopeFactor.GetSlope();
            switch (slopeType)
            {
                case SlopeFactor.SlopeTypeEnum.slopeTypePercentage:
                    {
                        if (slope > 0x190)
                        {
                            slope = 0;
                        }
                        this.sliderSlope.Maximum = 0x190;
                        break;
                    }
                case SlopeFactor.SlopeTypeEnum.slopeTypeDegree:
                    {
                        if (slope > 89)
                        {
                            slope = 0;
                        }
                        this.sliderSlope.Maximum = 89;
                        break;
                    }
                case SlopeFactor.SlopeTypeEnum.slopeType12:
                    {
                        if (slope > 50)
                        {
                            slope = 0;
                        }
                        this.sliderSlope.Maximum = 50;
                        break;
                    }
            }
            try
            {
                this.sliderSlope.Value = slope;
            }
            catch
            {
                this.sliderSlope.Value = 0;
            }
        }

        public event OnSlopeFactorCancelHandler OnCancel;

        public event OnSlopeFactorSaveHandler OnSave;
    }
}