using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class DBEstimatingItemForm : BaseForm
	{
		private void LoadTakeoffUnits()
		{
			if (UnitScale.DefaultUnitSystem() == UnitScale.UnitSystem.imperial)
			{
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.pi, Resources.pi));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.pi_2, Resources.pi_2));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.v_3, Resources.v_3));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData("m", "m"));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData("m²", "m²"));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData("m³", "m³"));
				return;
			}
			this.cbPurchaseUnit.Items.Add(new Utilities.ItemData("m", "m"));
			this.cbPurchaseUnit.Items.Add(new Utilities.ItemData("m²", "m²"));
			this.cbPurchaseUnit.Items.Add(new Utilities.ItemData("m³", "m³"));
			this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.pi, Resources.pi));
			this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.pi_2, Resources.pi_2));
			this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.v_3, Resources.v_3));
		}

		private void LoadResources()
		{
			this.cbPurchaseUnit.Items.Clear();
			switch (this.estimatingItem.ItemType)
			{
			case DBEstimatingItem.EstimatingItemType.MaterialItem:
				this.Text = (this.creationMode ? Resources.Nouvel_item_materiel : Resources.Modifier_item_d_estimation);
				this.LoadTakeoffUnits();
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Material_Chacun, Resources.Material_Chacun));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Material_Feuille, Resources.Material_Feuille));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Material_Paquet, Resources.Material_Paquet));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Material_Sac, Resources.Material_Sac));
				return;
			case DBEstimatingItem.EstimatingItemType.LaborItem:
				this.Text = (this.creationMode ? Resources.Nouvel_item_main_d_oeuvre : Resources.Modifier_item_d_estimation);
				this.LoadTakeoffUnits();
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Labor_Heure, Resources.Labor_Heure));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Labor_Jour, Resources.Labor_Jour));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Labor_Demie_journee, Resources.Labor_Demie_journee));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Labor_Semaine, Resources.Labor_Semaine));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Labor_Mois, Resources.Labor_Mois));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Labor_Projet, Resources.Labor_Projet));
				return;
			case DBEstimatingItem.EstimatingItemType.SubcontractItem:
				this.Text = (this.creationMode ? Resources.Nouvel_item_sous_contracteur : Resources.Modifier_item_d_estimation);
				this.LoadTakeoffUnits();
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Contractor_Heure, Resources.Contractor_Heure));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Contractor_Demie_journee, Resources.Contractor_Demie_journee));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Contractor_Jour, Resources.Contractor_Jour));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Contractor_Semaine, Resources.Contractor_Semaine));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Contractor_Mois, Resources.Contractor_Mois));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Contractor_Contrat, Resources.Contractor_Contrat));
				return;
			case DBEstimatingItem.EstimatingItemType.EquipmentItem:
				this.Text = (this.creationMode ? Resources.Nouvel_item_equipement : Resources.Modifier_item_d_estimation);
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Equipment_Unité, Resources.Equipment_Unité));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Equipment_Heure, Resources.Equipment_Heure));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Equipment_Jour, Resources.Equipment_Jour));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Equipment_Demie_journee, Resources.Equipment_Demie_journee));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Equipment_Semaine, Resources.Equipment_Semaine));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Equipment_Mois, Resources.Equipment_Mois));
				this.cbPurchaseUnit.Items.Add(new Utilities.ItemData(Resources.Equipment_Projet, Resources.Equipment_Projet));
				return;
			default:
				return;
			}
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		private void FillSections()
		{
			this.cbSection.Items.Clear();
			foreach (object obj in this.dbManagement.CSICodesA.Collection)
			{
				DBEstimatingSection dbestimatingSection = (DBEstimatingSection)((DictionaryEntry)obj).Value;
				this.cbSection.Items.Add(new Utilities.ItemData(dbestimatingSection.ID.ToString("00") + " - " + dbestimatingSection.Name, dbestimatingSection));
			}
			if (this.cbSection.Items.Count > 1)
			{
				this.cbSection.SelectedIndex = 0;
			}
		}

		private void FillSubSections(DBEstimatingSection section)
		{
			this.cbSubSection.Items.Clear();
			foreach (object obj in this.dbManagement.CSICodesB.Collection)
			{
				DBEstimatingSection dbestimatingSection = (DBEstimatingSection)((DictionaryEntry)obj).Value;
				if (dbestimatingSection.ParentID == section.ID)
				{
					this.cbSubSection.Items.Add(new Utilities.ItemData(dbestimatingSection.ID.ToString("0000") + " - " + dbestimatingSection.Name, dbestimatingSection));
				}
			}
			if (this.cbSubSection.Items.Count > 1)
			{
				this.cbSubSection.SelectedIndex = 1;
				return;
			}
			if (this.cbSubSection.Items.Count > 0)
			{
				this.cbSubSection.SelectedIndex = 0;
			}
		}

		private void FillMeasureValues()
		{
			this.cbUnitOfMeasure.Items.Clear();
			this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData(Resources.aucun, DBEstimatingItem.UnitMeasureType.none));
			if (UnitScale.DefaultUnitSystem() == UnitScale.UnitSystem.imperial)
			{
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData(Resources.pi, DBEstimatingItem.UnitMeasureType.lin_ft));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData(Resources.pi_2, DBEstimatingItem.UnitMeasureType.sq_ft));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData(Resources.v_3, DBEstimatingItem.UnitMeasureType.cu_yd));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData("m", DBEstimatingItem.UnitMeasureType.m));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData("m²", DBEstimatingItem.UnitMeasureType.sq_m));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData("m³", DBEstimatingItem.UnitMeasureType.cu_m));
			}
			else
			{
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData("m", DBEstimatingItem.UnitMeasureType.m));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData("m²", DBEstimatingItem.UnitMeasureType.sq_m));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData("m³", DBEstimatingItem.UnitMeasureType.cu_m));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData(Resources.pi, DBEstimatingItem.UnitMeasureType.lin_ft));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData(Resources.pi_2, DBEstimatingItem.UnitMeasureType.sq_ft));
				this.cbUnitOfMeasure.Items.Add(new Utilities.ItemData(Resources.v_3, DBEstimatingItem.UnitMeasureType.cu_yd));
			}
			this.cbUnitOfMeasure.SelectedIndex = 0;
		}

		private void LoadValues()
		{
			this.FillSections();
			this.FillMeasureValues();
			this.txtProductName.Text = this.estimatingItem.Description;
			this.txtPrice.Text = (this.txtPrice.Text = Utilities.FormatToCurrency(this.estimatingItem.PriceEach.ToString()));
			this.txtCoverageValue.Text = this.estimatingItem.CoverageValue.ToString();
			this.txtCoverateUnit.Text = this.estimatingItem.CoverageUnit.ToString();
			this.txtBidCode.Text = this.estimatingItem.BidCode;
			this.cbPurchaseUnit.Text = this.estimatingItem.PurchaseUnit;
			this.cbSection.SelectedItem = Utilities.SelectList(this.cbSection.Items, this.dbManagement.CSICodesA[this.estimatingItem.SectionID], true);
			this.cbSubSection.SelectedItem = Utilities.SelectList(this.cbSubSection.Items, this.dbManagement.CSICodesB[this.estimatingItem.SubSectionID], true);
			this.cbUnitOfMeasure.SelectedItem = Utilities.SelectList(this.cbUnitOfMeasure.Items, this.estimatingItem.UnitMeasure, true);
			this.txtItemID.Text = "itemID=\"" + this.estimatingItem.ID + "\"";
			this.ToggleCoverage();
		}

		public DBEstimatingItemForm(DBManagement dbManagement, DBEstimatingItem estimatingItem, bool creationMode)
		{
			this.InitializeComponent();
			this.creationMode = creationMode;
			this.dbManagement = dbManagement;
			this.estimatingItem = estimatingItem;
			this.LoadResources();
			this.InitializeFonts();
			this.LoadValues();
		}

		private void DBEstimatingItemForm_Activated(object sender, EventArgs e)
		{
			Utilities.SetObjectFocus(this.txtProductName);
		}

		private void txtField_Enter(object sender, EventArgs e)
		{
		}

		private void cbField_Enter(object sender, EventArgs e)
		{
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			DBEstimatingSection dbestimatingSection = (DBEstimatingSection)Utilities.GetItemData(this.cbSection.SelectedItem);
			if (dbestimatingSection == null)
			{
				return;
			}
			DBEstimatingSection dbestimatingSection2 = (DBEstimatingSection)Utilities.GetItemData(this.cbSubSection.SelectedItem);
			if (dbestimatingSection2 == null)
			{
				return;
			}
			this.txtProductName.Text = this.txtProductName.Text.Trim();
			if (this.txtProductName.Text == "")
			{
				Utilities.SetObjectFocus(this.txtProductName);
				return;
			}
			DBEstimatingItem.UnitMeasureType unitMeasureType = (DBEstimatingItem.UnitMeasureType)((Utilities.ItemData)this.cbUnitOfMeasure.SelectedItem).Data;
			double num = Utilities.ConvertToDouble(this.txtCoverageValue.Text, -1);
			double num2 = Utilities.ConvertToDouble(this.txtCoverateUnit.Text, -1);
			if (unitMeasureType != DBEstimatingItem.UnitMeasureType.none && (num == 0.0 || num2 == 0.0))
			{
				Utilities.SetObjectFocus((num == 0.0) ? this.txtCoverageValue : this.txtCoverateUnit);
				return;
			}
			double priceEach = Utilities.ConvertCurrency(this.txtPrice.Text, 0.0);
			this.estimatingItem.Description = this.txtProductName.Text;
			this.estimatingItem.PriceEach = priceEach;
			this.estimatingItem.UnitMeasure = unitMeasureType;
			this.estimatingItem.CoverageValue = num;
			this.estimatingItem.CoverageUnit = num2;
			this.estimatingItem.BidCode = this.txtBidCode.Text.Trim();
			this.estimatingItem.PurchaseUnit = this.cbPurchaseUnit.Text.Trim();
			this.estimatingItem.SectionID = dbestimatingSection.ID;
			this.estimatingItem.SubSectionID = dbestimatingSection2.ID;
			this.estimatingItem.Dirty = true;
			base.Close();
		}

		private void cbSection_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FillSubSections((DBEstimatingSection)Utilities.GetItemData(this.cbSection.SelectedItem));
		}

		private void cbUnitOfMeasure_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.cbUnitOfMeasure.SelectedIndex > 0;
			this.txtCoverageValue.Visible = flag;
			this.txtCoverateUnit.Visible = flag;
			this.lblCoverageMeasure.Visible = flag;
			this.lblCoverageUnit.Visible = flag;
			if (flag)
			{
				this.lblCoverageMeasure.Text = this.cbUnitOfMeasure.Text + " = ";
				this.lblCoverageUnit.Text = this.cbPurchaseUnit.Text;
				return;
			}
			this.txtCoverageValue.Text = "0";
		}

		private void ToggleCoverage()
		{
			string text = this.cbPurchaseUnit.Text;
			bool flag = text == "" || text == Resources.pi || text == Resources.pi_2 || text == Resources.v_3 || text == "m" || text == "m²" || text == "m³" || this.estimatingItem.ItemType == DBEstimatingItem.EstimatingItemType.LaborItem || this.estimatingItem.ItemType == DBEstimatingItem.EstimatingItemType.EquipmentItem || this.estimatingItem.ItemType == DBEstimatingItem.EstimatingItemType.SubcontractItem;
			this.lblUnitOfMeasure.Visible = !flag;
			this.cbUnitOfMeasure.Visible = !flag;
			if (flag)
			{
				this.cbUnitOfMeasure.SelectedIndex = 0;
				this.txtCoverageValue.Text = "0";
				this.txtCoverateUnit.Text = "1";
				return;
			}
			this.lblCoverageUnit.Text = text;
		}

		private void cbPurchaseUnit_Update(object sender, EventArgs e)
		{
			this.ToggleCoverage();
		}

		private void txtDouble_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '.' || e.KeyChar == ',')
			{
				e.KeyChar = Utilities.NumberDecimalSeparator()[0];
			}
			string text = "0123456789" + Utilities.NumberDecimalSeparator();
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\r' && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtDouble_Validating(object sender, CancelEventArgs e)
		{
			((TextBox)sender).Text = Utilities.ConvertToDouble(((TextBox)sender).Text, -1).ToString();
		}

		private void txtCurrency_Validating(object sender, CancelEventArgs e)
		{
			((TextBox)sender).Text = Utilities.FormatToCurrency(((TextBox)sender).Text);
		}

		private void txtText_Validating(object sender, CancelEventArgs e)
		{
			((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
		}

		private void txtField2_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private bool creationMode;

		private DBManagement dbManagement;

		private DBEstimatingItem estimatingItem;
	}
}
