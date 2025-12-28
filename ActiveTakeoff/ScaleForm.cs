using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class ScaleForm : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
			this.lblScaleReference.Font = Utilities.GetDefaultFont(FontStyle.Bold);
		}

		public ScaleForm(UnitScale unitScale)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.setManually = unitScale.SetManually;
			this.Initialize(unitScale);
		}

		private void SetReadOnly(bool readOnly)
		{
			this.cbScale.Enabled = (this.unitScale.MustSetManually || !readOnly);
			this.cbImperialScale.Enabled = !readOnly;
			this.txtScale.Enabled = (!this.unitScale.MustSetManually && !this.setManually && !readOnly);
			this.btOk.Enabled = (this.unitScale.MustSetManually || !readOnly);
			this.btModify.Enabled = readOnly;
			this.btManual.Enabled = (!readOnly || this.setManually);
		}

		private void Initialize(UnitScale unitScale)
		{
			bool flag = unitScale.Scale == 0f && !unitScale.MustSetManually;
			this.unitScale = unitScale;
			this.txtScale.Text = "";
			this.lblManual_1.Visible = false;
			this.txtManualFeet.Visible = false;
			this.lblManual_2.Visible = false;
			this.txtManualInches.Visible = false;
			this.txtManualMetric.Visible = false;
			this.btManual.Visible = !unitScale.MustSetManually;
			this.btModify.Visible = !unitScale.MustSetManually;
			this.cbScale.Items.Clear();
			this.cbScale.Items.Add(new Utilities.ItemData(Resources.Impérial, UnitScale.UnitSystem.imperial));
			this.cbScale.Items.Add(new Utilities.ItemData(Resources.Métrique, UnitScale.UnitSystem.metric));
			if (!unitScale.MustSetManually)
			{
				this.cbScale.Items.Add(new Utilities.ItemData(Resources.Ingénierie, UnitScale.UnitSystem.imperial));
			}
			if (flag)
			{
				unitScale.SetScale(unitScale.Scale, UnitScale.DefaultUnitSystem(), unitScale.Precision, unitScale.SetManually);
			}
			if (!unitScale.Engineering || unitScale.MustSetManually)
			{
				this.cbScale.SelectedItem = Utilities.SelectList(this.cbScale.Items, unitScale.ScaleSystemType, false);
			}
			else
			{
				this.cbScale.SelectedIndex = 2;
			}
			if (unitScale.MustSetManually)
			{
				this.lblManual.Text = Resources.Veuillez_indiquer_la_longueur_de_la_référence_linéaire_prise_sur_le_plan;
			}
			else if (!this.setManually)
			{
				this.lblManual.Text = Resources.Pour_ajuster_manuellement_l_échelle_en_utilisant_une_référence_linéaire_sur_le_plan_cliquez_ici;
				this.btManual.Text = Resources.Ajustement_manuel;
				this.cbImperialScale.Items.Clear();
				this.cbImperialScale.Items.Add(new Utilities.ItemData("1/32 \"", 0.03125f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("1/16 \"", 0.0625f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("3/32 \"", 0.09375f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("1/8 \"", 0.125f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("5/32 \"", 0.15625f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("3/16 \"", 0.1875f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("7/32 \"", 0.21875f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("1/4 \"", 0.25f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("9/32 \"", 0.28125f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("5/16 \"", 0.3125f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("11/32 \"", 0.34375f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("3/8 \"", 0.375f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("13/32 \"", 0.40625f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("7/16 \"", 0.4375f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("15/32 \"", 0.46875f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("1/2 \"", 0.5f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("17/32 \"", 0.53125f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("9/16 \"", 0.5625f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("19/32 \"", 0.59375f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("5/8 \"", 0.625f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("21/32 \"", 0.65625f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("11/16 \"", 0.6875f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("23/32 \"", 0.71875f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("3/4 \"", 0.75f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("25/32 \"", 0.78125f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("13/16 \"", 0.8125f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("27/32 \"", 0.84375f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("7/8 \"", 0.875f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("29/32 \"", 0.90625f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("15/16 \"", 0.9375f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("31/32 \"", 0.96875f));
				this.cbImperialScale.Items.Add(new Utilities.ItemData("1 \"", 1f));
				if (unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial && !unitScale.Engineering)
				{
					this.cbImperialScale.SelectedItem = Utilities.SelectList(this.cbImperialScale.Items, unitScale.Scale, false);
				}
				else
				{
					this.txtScale.Text = ((int)unitScale.Scale).ToString();
				}
			}
			else if (!unitScale.StandardScaleDisable)
			{
				this.lblManual.Text = Resources.Pour_utiliser_une_échelle_standard_pré_définie_cliquez_ici;
				this.btManual.Text = Resources.Échelle_standard;
			}
			else
			{
				this.lblManual.Visible = false;
				this.btManual.Visible = false;
			}
			this.SetReadOnly(unitScale.Scale != 0f);
			this.unitScale.Dirty = false;
		}

		private void cbScale_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.cbScale.SelectedIndex == 0 || this.cbScale.SelectedIndex == 2;
			bool flag2 = this.cbScale.SelectedIndex == 2;
			this.lblScale_1.Visible = flag2;
			if (!this.unitScale.MustSetManually && !this.setManually)
			{
				this.cbImperialScale.Visible = (flag && !flag2);
				this.lblScaleReference.Text = ((flag && !flag2) ? "1' 0\" =" : ((!flag2) ? "1 :" : "1\" ="));
				this.txtScale.Visible = (!flag || flag2);
				if (!flag || flag2)
				{
					this.txtScale.Text = ((this.txtScale.Text == "") ? "0" : this.txtScale.Text);
				}
				Utilities.SetObjectFocus((flag && !flag2) ? this.cbImperialScale : this.txtScale);
				return;
			}
			if (this.unitScale.MustSetManually)
			{
				this.lblManual_1.Visible = flag;
				this.txtManualFeet.Visible = flag;
				this.lblManual_2.Visible = true;
				this.txtManualInches.Visible = flag;
				this.txtManualMetric.Visible = !flag;
				this.lblManual_2.Text = (flag ? "\"" : "mm");
			}
			this.lblScaleReference.Text = (flag ? "1\" =" : "10 mm =");
			this.ComputeManualScale();
			Utilities.SetObjectFocus(this.unitScale.MustSetManually ? (flag ? this.txtManualFeet : this.txtManualMetric) : this.btModify);
		}

		private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			string text = "0123456789";
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\r' && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtNumeric_Validating(object sender, CancelEventArgs e)
		{
			((TextBox)sender).Text = Utilities.ConvertToInt(((TextBox)sender).Text).ToString();
		}

		private void txtNumeric_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private float ComputeManualScale()
		{
			float num = 0f;
			if (this.setManually && !this.unitScale.MustSetManually)
			{
				num = this.unitScale.ScaleFactor;
			}
			else
			{
				UnitScale.UnitSystem unitSystem = (UnitScale.UnitSystem)((Utilities.ItemData)this.cbScale.SelectedItem).Data;
				if (unitSystem == UnitScale.UnitSystem.imperial)
				{
					int num2 = Utilities.ConvertToInt(this.txtManualFeet.Text) * 12 + Utilities.ConvertToInt(this.txtManualInches.Text);
					if (num2 > 0)
					{
						num = (float)this.unitScale.ReferenceDistance / (float)num2;
					}
				}
				else
				{
					int num3 = Utilities.ConvertToInt(this.txtManualMetric.Text) / 10;
					if (num3 > 0)
					{
						num = (float)this.unitScale.ReferenceDistance / (float)num3;
					}
				}
			}
			this.txtScale.Text = Math.Round((double)num, 3).ToString() + " pixels";
			return num;
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			float num = 0f;
			bool flag = false;
			UnitScale.UnitSystem unitSystem = UnitScale.UnitSystem.undefined;
			bool flag2 = this.cbScale.SelectedIndex == 2;
			if (this.cbScale.SelectedItem != null)
			{
				unitSystem = (UnitScale.UnitSystem)((Utilities.ItemData)this.cbScale.SelectedItem).Data;
			}
			if (!this.unitScale.MustSetManually)
			{
				if (unitSystem != UnitScale.UnitSystem.undefined)
				{
					if (unitSystem == UnitScale.UnitSystem.imperial)
					{
						if (!flag2)
						{
							flag = (this.cbImperialScale.SelectedItem != null);
							if (flag)
							{
								num = (float)((Utilities.ItemData)this.cbImperialScale.SelectedItem).Data;
							}
						}
						else
						{
							num = (float)Utilities.ConvertToInt(this.txtScale.Text);
							flag = (num > 0f);
						}
					}
					else
					{
						num = (float)Utilities.ConvertToInt(this.txtScale.Text);
						flag = (num > 0f);
					}
				}
			}
			else
			{
				num = this.ComputeManualScale();
				flag = (num > 0f);
			}
			if (flag)
			{
				this.unitScale.SetScale(num, unitSystem, this.unitScale.Precision, this.unitScale.MustSetManually);
				this.unitScale.Engineering = flag2;
				this.unitScale.Dirty = true;
				base.Close();
				return;
			}
			Utilities.DisplayError(Resources.Échelle_invalide, Resources.Veuillez_spécifier_une_échelle_valide);
			if (!this.unitScale.MustSetManually)
			{
				Utilities.SetObjectFocus((unitSystem == UnitScale.UnitSystem.imperial && !flag2) ? this.cbImperialScale : this.txtScale);
				return;
			}
			Utilities.SetObjectFocus((unitSystem == UnitScale.UnitSystem.imperial) ? this.txtManualFeet : this.txtManualMetric);
		}

		private void Modify()
		{
			this.SetReadOnly(false);
			UnitScale.UnitSystem unitSystem = (UnitScale.UnitSystem)((Utilities.ItemData)this.cbScale.SelectedItem).Data;
			if (!this.unitScale.MustSetManually)
			{
				Utilities.SetObjectFocus((unitSystem == UnitScale.UnitSystem.imperial) ? this.cbImperialScale : this.txtScale);
				return;
			}
			Utilities.SetObjectFocus(this.txtManualFeet);
		}

		private void btModify_Click(object sender, EventArgs e)
		{
			if (this.setManually)
			{
				this.unitScale.MustSetManually = true;
				this.unitScale.Dirty = true;
				base.Close();
				return;
			}
			this.Modify();
		}

		private void btManual_Click(object sender, EventArgs e)
		{
			if (this.setManually)
			{
				this.setManually = false;
				this.Initialize(this.unitScale);
				this.Modify();
				this.txtScale.Text = "0";
				Utilities.SelectText(this.txtScale);
				return;
			}
			this.unitScale.MustSetManually = true;
			this.unitScale.Dirty = true;
			base.Close();
		}

		private void txtManual_TextChanged(object sender, EventArgs e)
		{
			this.ComputeManualScale();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void ScaleForm_Shown(object sender, EventArgs e)
		{
			bool flag = this.unitScale.Scale == 0f && !this.unitScale.MustSetManually;
			if (flag)
			{
				Utilities.SetObjectFocus((this.unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? this.cbImperialScale : this.txtScale);
				return;
			}
			if (!this.unitScale.MustSetManually)
			{
				this.cbScale.Focus();
				return;
			}
			Utilities.SetObjectFocus((this.unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? this.txtManualFeet : this.txtManualMetric);
		}

		private bool setManually;

		private UnitScale unitScale;
	}
}
