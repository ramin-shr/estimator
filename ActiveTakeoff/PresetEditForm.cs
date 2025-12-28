using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class PresetEditForm : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		private void InitializeGui()
		{
			int top = 5;
			this.tableLayoutPanel.Margin = new Padding(0, 0, 0, 0);
			this.tableLayoutPanel.Padding = new Padding(0, top, 0, 0);
			if (this.creationMode)
			{
				this.FillTemplateCategories();
			}
			else
			{
				this.FillPresetValues();
			}
			if (!this.creationMode)
			{
				this.txtDisplayName.Text = this.preset.DisplayName;
			}
			Utilities.SetObjectFocus(this.cbCategories);
		}

		public PresetEditForm(DrawObjectGroup group, Preset preset, ExtensionsSupport extensionSupport, UnitScale unitScale, bool creationMode)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.group = group;
			this.preset = preset;
			this.extensionSupport = extensionSupport;
			this.unitScale = unitScale;
			this.creationMode = creationMode;
			preset.Dirty = false;
			this.InitializeGui();
		}

		private void AddToPanel(TableLayoutPanel tableLayoutPanel, Control control)
		{
			control.Font = Utilities.GetDefaultFont();
			control.Width = 150;
			control.Margin = new Padding((control.GetType().Name == "LabelEx") ? 20 : 23, 0, 0, (control.GetType().Name == "LabelEx") ? 4 : 10);
			control.Visible = true;
			tableLayoutPanel.Controls.Add(control, this.columnIndex, this.rowIndex);
			this.rowIndex++;
			if (this.rowIndex == 8)
			{
				this.rowIndex = 0;
				this.columnIndex++;
				tableLayoutPanel.ColumnCount++;
			}
		}

		private void FillPresetValues()
		{
			ExtensionCategory extensionCategory = null;
			Extension extension = this.extensionSupport.FindExtension(ref extensionCategory, this.preset.ExtensionName);
			if (extension == null)
			{
				this.btOk.Enabled = false;
				return;
			}
			this.cbCategories.Items.Clear();
			this.cbCategories.Items.Add(new Utilities.ItemData(extensionCategory.Caption, extensionCategory));
			this.cbCategories.Enabled = false;
			this.cbCategories.SelectedIndex = 0;
			this.cbExtensions.Items.Clear();
			this.cbExtensions.Items.Add(new Utilities.ItemData(extension.Caption, extension));
			this.cbExtensions.Enabled = false;
			this.cbExtensions.SelectedIndex = 0;
			this.LoadTemplateControls(extension);
			this.btOk.Enabled = true;
		}

		private void FillTemplateCategories()
		{
			this.cbCategories.Items.Clear();
			foreach (object obj in this.extensionSupport.Categories.Collection)
			{
				ExtensionCategory extensionCategory = (ExtensionCategory)obj;
				bool flag = false;
				foreach (object obj2 in extensionCategory.Templates.Collection)
				{
					Extension extension = (Extension)obj2;
					if (extension.ObjectType == this.group.ObjectType)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					this.cbCategories.Items.Add(new Utilities.ItemData(extensionCategory.Division.ToString("00") + " - " + extensionCategory.Caption, extensionCategory));
				}
			}
			if (this.cbCategories.Items.Count > 0)
			{
				this.cbCategories.SelectedIndex = 0;
			}
		}

		private void FillTemplates(ExtensionCategory category)
		{
			this.cbExtensions.Items.Clear();
			if (category == null)
			{
				return;
			}
			foreach (object obj in category.Templates.Collection)
			{
				Extension extension = (Extension)obj;
				if (!extension.Hidden && extension.ObjectType == this.group.ObjectType)
				{
					this.cbExtensions.Items.Add(new Utilities.ItemData(extension.Caption, extension));
				}
			}
			if (this.cbExtensions.Items.Count > 0)
			{
				this.cbExtensions.SelectedIndex = 0;
			}
		}

		private void LoadTemplateControls(Extension extension)
		{
			this.lblSelection.Visible = true;
			this.lblDisplayName.Visible = false;
			this.txtDisplayName.Visible = false;
			this.extensionControls.Clear();
			if (this.presetDefaultValues != null)
			{
				this.presetDefaultValues.Clear();
				this.presetDefaultValues = null;
			}
			for (int i = this.tableLayoutPanel.Controls.Count - 1; i >= 0; i--)
			{
				this.tableLayoutPanel.Controls[i].Dispose();
			}
			if (extension == null)
			{
				return;
			}
			int num = 3;
			foreach (object obj in extension.Choices.Collection)
			{
				ExtensionChoice extensionChoice = (ExtensionChoice)obj;
				LabelEx labelEx = new LabelEx();
				labelEx.Name = "lblChoice" + extensionChoice.Name;
				labelEx.Text = extensionChoice.Caption;
				labelEx.AutoSize = true;
				ComboBoxEx comboBoxEx = new ComboBoxEx();
				comboBoxEx.Name = "cbChoice" + extensionChoice.Name;
				comboBoxEx.Tag = extensionChoice;
				comboBoxEx.DropDownStyle = ComboBoxStyle.DropDownList;
				comboBoxEx.AssociatedLabel = labelEx;
				ExtensionChoiceElement extensionChoiceElement = null;
				foreach (object obj2 in extensionChoice.Elements.Collection)
				{
					ExtensionChoiceElement extensionChoiceElement2 = (ExtensionChoiceElement)obj2;
					if (extensionChoiceElement2.Name == extensionChoice.DefaultChoice)
					{
						extensionChoiceElement = extensionChoiceElement2;
					}
					comboBoxEx.Items.Add(new Utilities.ItemData(extensionChoiceElement2.Caption, extensionChoiceElement2));
				}
				comboBoxEx.SelectedItem = Utilities.SelectList(comboBoxEx.Items, this.creationMode ? extensionChoiceElement : extensionChoice.FindElement(this.preset.ChoiceElement(extensionChoice.Name)), true);
				comboBoxEx.SelectedIndexChanged += this.cbChoice_SelectedIndexChanged;
				comboBoxEx.TabIndex = num++;
				this.visibleControls.Add(extensionChoice.Name);
				this.extensionControls.Add(new Variable(extensionChoice.Name, comboBoxEx, labelEx));
			}
			foreach (object obj3 in extension.Fields.Collection)
			{
				ExtensionField extensionField = (ExtensionField)obj3;
				LabelEx labelEx2 = new LabelEx();
				labelEx2.Name = "lblField" + extensionField.Name;
				labelEx2.Text = extensionField.Caption;
				if (extensionField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency)
				{
					LabelEx labelEx3 = labelEx2;
					labelEx3.Text = labelEx3.Text + " (" + Utilities.GetCurrencySymbol() + ")";
				}
				labelEx2.AutoSize = true;
				Control control;
				if (extensionField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension)
				{
					double num2 = 0.0;
					if (!this.creationMode)
					{
						num2 = Utilities.ConvertToDouble(this.preset.FieldValue(extensionField.Name), -1);
						if (this.preset.ScaleSystemType != this.unitScale.ScaleSystemType)
						{
							num2 = ((this.unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num2) : UnitScale.FromFeetToMeters(num2));
						}
						if (this.unitScale.ScaleSystemType == UnitScale.UnitSystem.metric)
						{
							num2 = ((this.unitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToInches(num2) : UnitScale.FromMetersToMillimeters(num2));
						}
						else
						{
							num2 = ((this.unitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromFeetToInches(num2) : UnitScale.FromFeetToMillimeters(num2));
						}
					}
					control = new DimensionInput(this.unitScale.ScaleSystemType, this.unitScale.CurrentSystemType, num2);
					((DimensionInput)control).AutoScaleMode = AutoScaleMode.Inherit;
					((DimensionInput)control).AssociatedLabel = labelEx2;
				}
				else
				{
					control = new TextBoxEx();
					control.Text = (this.creationMode ? "0" : this.preset.FieldValue(extensionField.Name));
					((TextBoxEx)control).TextAlign = HorizontalAlignment.Center;
					((TextBoxEx)control).MaxLength = 9;
					((TextBoxEx)control).AssociatedLabel = labelEx2;
					control.KeyPress += this.txtNumeric_KeyPress;
					control.Validating += new CancelEventHandler(this.txtNumeric_Validating);
					control.Enter += this.txtNumeric_Enter;
					control.Click += this.txtNumeric_Enter;
				}
				control.Name = "txtField" + extensionField.Name;
				control.Tag = extensionField;
				control.TabIndex = num++;
				this.visibleControls.Add(extensionField.Name);
				this.extensionControls.Add(new Variable(extensionField.Name, control, labelEx2));
			}
			this.GeneratePresetDefaultValues();
			this.lblSelection.Visible = (this.tableLayoutPanel.Controls.Count == 0);
			this.lblDisplayName.Visible = (this.tableLayoutPanel.Controls.Count > 0);
			this.txtDisplayName.Visible = (this.tableLayoutPanel.Controls.Count > 0);
		}

		private void FillControls(TableLayoutPanel layoutPanel, List<string> listOfControls)
		{
			layoutPanel.Controls.Clear();
			layoutPanel.ColumnStyles.Clear();
			layoutPanel.RowStyles.Clear();
			layoutPanel.RowCount = 8;
			layoutPanel.ColumnCount = 1;
			this.rowIndex = 0;
			this.columnIndex = 0;
			foreach (string name in listOfControls)
			{
				Variable variable = this.extensionControls.Find(name);
				if (variable != null)
				{
					LabelEx control = (LabelEx)variable.Tag;
					Control control2 = (Control)variable.Value;
					this.AddToPanel(layoutPanel, control);
					this.AddToPanel(layoutPanel, control2);
				}
			}
		}

		private void QueryVisibleControls(Preset presetObject, List<string> listOfControls)
		{
			listOfControls.Clear();
			foreach (object obj in this.extensionControls.Collection)
			{
				Variable variable = (Variable)obj;
				bool flag = false;
				Control control = (Control)variable.Value;
				if (control.GetType().Name == "ComboBoxEx")
				{
					ExtensionChoice extensionChoice = (ExtensionChoice)control.Tag;
					flag = (presetObject.Choices.Find(extensionChoice.Name) != null);
				}
				else if (control.GetType().Name == "TextBoxEx" || control.GetType().Name == "DimensionInput")
				{
					ExtensionField extensionField = (ExtensionField)control.Tag;
					flag = (presetObject.Fields.Find(extensionField.Name) != null);
				}
				if (flag)
				{
					listOfControls.Add(variable.Name);
				}
			}
		}

		private void GeneratePresetDefaultValues()
		{
			if (this.cbCategories.SelectedIndex == 0 && this.cbExtensions.SelectedIndex == 0 && this.creationMode)
			{
				return;
			}
			ExtensionCategory extensionCategory = (ExtensionCategory)Utilities.GetItemData(this.cbCategories.SelectedItem);
			if (extensionCategory == null)
			{
				return;
			}
			Extension extension = (Extension)Utilities.GetItemData(this.cbExtensions.SelectedItem);
			if (extension == null)
			{
				return;
			}
			this.FillControls(this.hiddenLayoutPanel, this.visibleControls);
			this.presetDefaultValues = new Preset(this.unitScale.ScaleSystemType);
			this.SaveControlsValues(this.hiddenLayoutPanel, this.presetDefaultValues, extensionCategory.Name, extension.Name);
			this.presetDefaultValues.SynchWithTemplate(this.extensionSupport);
			this.QueryVisibleControls(this.presetDefaultValues, this.visibleControls);
			this.FillControls(this.tableLayoutPanel, this.visibleControls);
		}

		private bool AreEqual<T>(List<T> x, List<T> y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Count != y.Count)
			{
				return false;
			}
			for (int i = 0; i < x.Count; i++)
			{
				T t = x[i];
				if (!t.Equals(y[i]))
				{
					return false;
				}
			}
			return true;
		}

		private void UpdateTemplateControlsVisibility()
		{
			if (this.presetDefaultValues != null)
			{
				bool flag = false;
				do
				{
					List<string> list = new List<string>();
					this.SaveControlsValues(this.tableLayoutPanel, this.presetDefaultValues, this.presetDefaultValues.CategoryName, this.presetDefaultValues.ExtensionName);
					this.presetDefaultValues.SynchWithTemplate(this.extensionSupport);
					this.QueryVisibleControls(this.presetDefaultValues, list);
					if (!this.AreEqual<string>(list, this.visibleControls))
					{
						this.visibleControls.Clear();
						this.visibleControls = list.Clone<string>();
						this.FillControls(this.tableLayoutPanel, this.visibleControls);
					}
					else
					{
						flag = true;
					}
				}
				while (!flag);
			}
		}

		private void cbChoice_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.exitNow)
			{
				this.exitNow = true;
				this.UpdateTemplateControlsVisibility();
				Utilities.SetObjectFocus((Control)sender);
				this.exitNow = false;
			}
		}

		private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.creationMode)
			{
				return;
			}
			this.FillTemplates((ExtensionCategory)Utilities.GetItemData(this.cbCategories.SelectedItem));
			this.btOk.Enabled = (this.cbCategories.SelectedIndex > 0 || (this.cbCategories.SelectedIndex == 0 && this.cbExtensions.SelectedIndex > 0));
		}

		private void cbTemplates_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.creationMode)
			{
				return;
			}
			this.LoadTemplateControls((Extension)Utilities.GetItemData(this.cbExtensions.SelectedItem));
			if (this.creationMode)
			{
				this.txtDisplayName.Text = this.group.Presets.GetFreeDisplayName(this.cbExtensions.Text, "");
			}
			this.btOk.Enabled = (this.cbCategories.SelectedIndex > 0 || (this.cbCategories.SelectedIndex == 0 && this.cbExtensions.SelectedIndex > 0));
		}

		private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			string text = "0123456789";
			if (((TextBoxEx)sender).Text.IndexOf('.') == -1)
			{
				ExtensionField extensionField = (ExtensionField)((TextBoxEx)sender).Tag;
				if (extensionField != null && (extensionField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDouble || extensionField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency))
				{
					text += ".";
				}
			}
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\r' && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtNumeric_Validating(object sender, CancelEventArgs e)
		{
			bool flag = false;
			ExtensionField extensionField = (ExtensionField)((TextBoxEx)sender).Tag;
			int decimals = (extensionField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency) ? 2 : -1;
			if (extensionField != null)
			{
				flag = (extensionField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDouble || extensionField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency);
			}
			((TextBoxEx)sender).Text = (flag ? Utilities.ConvertToDouble(((TextBoxEx)sender).Text, decimals).ToString() : Utilities.ConvertToInt(((TextBoxEx)sender).Text).ToString());
		}

		private void txtNumeric_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBoxEx)sender);
		}

		private void txtDisplayName_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBoxEx)sender);
		}

		private void txtDisplayName_Validating(object sender, CancelEventArgs e)
		{
			((TextBoxEx)sender).Text = ((TextBoxEx)sender).Text.Trim();
		}

		private void SaveControlsValues(TableLayoutPanel layoutPanel, Preset presetObject, string categoryName, string extensionName)
		{
			string displayName = this.txtDisplayName.Text.Trim();
			presetObject.Reset(displayName, categoryName, extensionName);
			foreach (object obj in layoutPanel.Controls)
			{
				Control control = (Control)obj;
				if (control.GetType().Name == "ComboBoxEx")
				{
					ComboBoxEx comboBoxEx = (ComboBoxEx)control;
					ExtensionChoice extensionChoice = (ExtensionChoice)control.Tag;
					ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)Utilities.GetItemData(comboBoxEx.SelectedItem);
					if (extensionChoice != null && extensionChoiceElement != null)
					{
						presetObject.Choices.Add(new PresetChoice(extensionChoice.Name, extensionChoiceElement.Name));
					}
				}
				else if (control.GetType().Name == "TextBoxEx" || control.GetType().Name == "DimensionInput")
				{
					ExtensionField extensionField = (ExtensionField)control.Tag;
					if (extensionField != null)
					{
						switch (extensionField.FieldType)
						{
						case ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension:
						{
							double num = ((DimensionInput)control).Value;
							num = ((this.unitScale.ScaleSystemType == UnitScale.UnitSystem.metric) ? UnitScale.FromMillimetersToMeters(num) : UnitScale.FromInchesToFeet(num));
							if (presetObject.ScaleSystemType != this.unitScale.ScaleSystemType)
							{
								num = ((presetObject.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num) : UnitScale.FromFeetToMeters(num));
							}
							presetObject.Fields.Add(new PresetField(extensionField.Name, num));
							break;
						}
						case ExtensionField.ExtensionFieldTypeEnum.FieldTypeInteger:
							presetObject.Fields.Add(new PresetField(extensionField.Name, Utilities.ConvertToInt(((TextBoxEx)control).Text)));
							break;
						case ExtensionField.ExtensionFieldTypeEnum.FieldTypeDouble:
						case ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency:
							presetObject.Fields.Add(new PresetField(extensionField.Name, Utilities.ConvertToDouble(((TextBoxEx)control).Text, -1)));
							break;
						}
					}
				}
			}
			presetObject.Dirty = true;
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			if (this.cbCategories.SelectedIndex == 0 && this.cbExtensions.SelectedIndex == 0 && this.creationMode)
			{
				return;
			}
			ExtensionCategory extensionCategory = (ExtensionCategory)Utilities.GetItemData(this.cbCategories.SelectedItem);
			if (extensionCategory == null)
			{
				return;
			}
			Extension extension = (Extension)Utilities.GetItemData(this.cbExtensions.SelectedItem);
			if (extension == null)
			{
				return;
			}
			this.txtDisplayName.Text = this.txtDisplayName.Text.Trim();
			if (this.txtDisplayName.Text != "" && !Utilities.ValidateVariableName(this.txtDisplayName.Text, "[]."))
			{
				Utilities.DisplayError(Resources.Nom_invalide, Resources.Les_caractères_suivants_sont_invalides + "\n" + Utilities.InvalidCharacters() + " [ ] .");
				Utilities.SetObjectFocus(this.txtDisplayName);
				return;
			}
			if (this.creationMode)
			{
				this.txtDisplayName.Text = this.group.Presets.GetFreeDisplayName((this.txtDisplayName.Text == "") ? this.cbExtensions.Text : this.txtDisplayName.Text, "");
			}
			else
			{
				this.txtDisplayName.Text = this.group.Presets.GetFreeDisplayName((this.txtDisplayName.Text == "") ? this.cbExtensions.Text : this.txtDisplayName.Text, this.preset.ID);
			}
			this.SaveControlsValues(this.tableLayoutPanel, this.preset, extensionCategory.Name, extension.Name);
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private Preset preset;

		private Preset presetDefaultValues;

		private DrawObjectGroup group;

		private UnitScale unitScale;

		private ExtensionsSupport extensionSupport;

		private Variables extensionControls = new Variables();

		private List<string> visibleControls = new List<string>();

		private int rowIndex;

		private int columnIndex;

		private bool creationMode;

		private bool exitNow;
	}
}
