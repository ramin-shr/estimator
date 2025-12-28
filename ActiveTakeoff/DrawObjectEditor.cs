using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class DrawObjectEditor
	{
		public event OnObjectSelectedHandler OnObjectSelected
		{
			add
			{
				OnObjectSelectedHandler onObjectSelectedHandler = this.OnObjectSelected;
				OnObjectSelectedHandler onObjectSelectedHandler2;
				do
				{
					onObjectSelectedHandler2 = onObjectSelectedHandler;
					OnObjectSelectedHandler value2 = (OnObjectSelectedHandler)Delegate.Combine(onObjectSelectedHandler2, value);
					onObjectSelectedHandler = Interlocked.CompareExchange<OnObjectSelectedHandler>(ref this.OnObjectSelected, value2, onObjectSelectedHandler2);
				}
				while (onObjectSelectedHandler != onObjectSelectedHandler2);
			}
			remove
			{
				OnObjectSelectedHandler onObjectSelectedHandler = this.OnObjectSelected;
				OnObjectSelectedHandler onObjectSelectedHandler2;
				do
				{
					onObjectSelectedHandler2 = onObjectSelectedHandler;
					OnObjectSelectedHandler value2 = (OnObjectSelectedHandler)Delegate.Remove(onObjectSelectedHandler2, value);
					onObjectSelectedHandler = Interlocked.CompareExchange<OnObjectSelectedHandler>(ref this.OnObjectSelected, value2, onObjectSelectedHandler2);
				}
				while (onObjectSelectedHandler != onObjectSelectedHandler2);
			}
		}

		public event OnObjectChangedHandler OnObjectChanged
		{
			add
			{
				OnObjectChangedHandler onObjectChangedHandler = this.OnObjectChanged;
				OnObjectChangedHandler onObjectChangedHandler2;
				do
				{
					onObjectChangedHandler2 = onObjectChangedHandler;
					OnObjectChangedHandler value2 = (OnObjectChangedHandler)Delegate.Combine(onObjectChangedHandler2, value);
					onObjectChangedHandler = Interlocked.CompareExchange<OnObjectChangedHandler>(ref this.OnObjectChanged, value2, onObjectChangedHandler2);
				}
				while (onObjectChangedHandler != onObjectChangedHandler2);
			}
			remove
			{
				OnObjectChangedHandler onObjectChangedHandler = this.OnObjectChanged;
				OnObjectChangedHandler onObjectChangedHandler2;
				do
				{
					onObjectChangedHandler2 = onObjectChangedHandler;
					OnObjectChangedHandler value2 = (OnObjectChangedHandler)Delegate.Remove(onObjectChangedHandler2, value);
					onObjectChangedHandler = Interlocked.CompareExchange<OnObjectChangedHandler>(ref this.OnObjectChanged, value2, onObjectChangedHandler2);
				}
				while (onObjectChangedHandler != onObjectChangedHandler2);
			}
		}

		public event OnObjectSelectedHandler OnDisplayCalculationsForAllPlans
		{
			add
			{
				OnObjectSelectedHandler onObjectSelectedHandler = this.OnDisplayCalculationsForAllPlans;
				OnObjectSelectedHandler onObjectSelectedHandler2;
				do
				{
					onObjectSelectedHandler2 = onObjectSelectedHandler;
					OnObjectSelectedHandler value2 = (OnObjectSelectedHandler)Delegate.Combine(onObjectSelectedHandler2, value);
					onObjectSelectedHandler = Interlocked.CompareExchange<OnObjectSelectedHandler>(ref this.OnDisplayCalculationsForAllPlans, value2, onObjectSelectedHandler2);
				}
				while (onObjectSelectedHandler != onObjectSelectedHandler2);
			}
			remove
			{
				OnObjectSelectedHandler onObjectSelectedHandler = this.OnDisplayCalculationsForAllPlans;
				OnObjectSelectedHandler onObjectSelectedHandler2;
				do
				{
					onObjectSelectedHandler2 = onObjectSelectedHandler;
					OnObjectSelectedHandler value2 = (OnObjectSelectedHandler)Delegate.Remove(onObjectSelectedHandler2, value);
					onObjectSelectedHandler = Interlocked.CompareExchange<OnObjectSelectedHandler>(ref this.OnDisplayCalculationsForAllPlans, value2, onObjectSelectedHandler2);
				}
				while (onObjectSelectedHandler != onObjectSelectedHandler2);
			}
		}

		public event PresetEventHandler OnPresetCreated
		{
			add
			{
				PresetEventHandler presetEventHandler = this.OnPresetCreated;
				PresetEventHandler presetEventHandler2;
				do
				{
					presetEventHandler2 = presetEventHandler;
					PresetEventHandler value2 = (PresetEventHandler)Delegate.Combine(presetEventHandler2, value);
					presetEventHandler = Interlocked.CompareExchange<PresetEventHandler>(ref this.OnPresetCreated, value2, presetEventHandler2);
				}
				while (presetEventHandler != presetEventHandler2);
			}
			remove
			{
				PresetEventHandler presetEventHandler = this.OnPresetCreated;
				PresetEventHandler presetEventHandler2;
				do
				{
					presetEventHandler2 = presetEventHandler;
					PresetEventHandler value2 = (PresetEventHandler)Delegate.Remove(presetEventHandler2, value);
					presetEventHandler = Interlocked.CompareExchange<PresetEventHandler>(ref this.OnPresetCreated, value2, presetEventHandler2);
				}
				while (presetEventHandler != presetEventHandler2);
			}
		}

		public event PresetEventHandler OnPresetModified
		{
			add
			{
				PresetEventHandler presetEventHandler = this.OnPresetModified;
				PresetEventHandler presetEventHandler2;
				do
				{
					presetEventHandler2 = presetEventHandler;
					PresetEventHandler value2 = (PresetEventHandler)Delegate.Combine(presetEventHandler2, value);
					presetEventHandler = Interlocked.CompareExchange<PresetEventHandler>(ref this.OnPresetModified, value2, presetEventHandler2);
				}
				while (presetEventHandler != presetEventHandler2);
			}
			remove
			{
				PresetEventHandler presetEventHandler = this.OnPresetModified;
				PresetEventHandler presetEventHandler2;
				do
				{
					presetEventHandler2 = presetEventHandler;
					PresetEventHandler value2 = (PresetEventHandler)Delegate.Remove(presetEventHandler2, value);
					presetEventHandler = Interlocked.CompareExchange<PresetEventHandler>(ref this.OnPresetModified, value2, presetEventHandler2);
				}
				while (presetEventHandler != presetEventHandler2);
			}
		}

		public event PresetEventHandler OnPresetDeleted
		{
			add
			{
				PresetEventHandler presetEventHandler = this.OnPresetDeleted;
				PresetEventHandler presetEventHandler2;
				do
				{
					presetEventHandler2 = presetEventHandler;
					PresetEventHandler value2 = (PresetEventHandler)Delegate.Combine(presetEventHandler2, value);
					presetEventHandler = Interlocked.CompareExchange<PresetEventHandler>(ref this.OnPresetDeleted, value2, presetEventHandler2);
				}
				while (presetEventHandler != presetEventHandler2);
			}
			remove
			{
				PresetEventHandler presetEventHandler = this.OnPresetDeleted;
				PresetEventHandler presetEventHandler2;
				do
				{
					presetEventHandler2 = presetEventHandler;
					PresetEventHandler value2 = (PresetEventHandler)Delegate.Remove(presetEventHandler2, value);
					presetEventHandler = Interlocked.CompareExchange<PresetEventHandler>(ref this.OnPresetDeleted, value2, presetEventHandler2);
				}
				while (presetEventHandler != presetEventHandler2);
			}
		}

		public event ProductEventHandler OnProductCreated
		{
			add
			{
				ProductEventHandler productEventHandler = this.OnProductCreated;
				ProductEventHandler productEventHandler2;
				do
				{
					productEventHandler2 = productEventHandler;
					ProductEventHandler value2 = (ProductEventHandler)Delegate.Combine(productEventHandler2, value);
					productEventHandler = Interlocked.CompareExchange<ProductEventHandler>(ref this.OnProductCreated, value2, productEventHandler2);
				}
				while (productEventHandler != productEventHandler2);
			}
			remove
			{
				ProductEventHandler productEventHandler = this.OnProductCreated;
				ProductEventHandler productEventHandler2;
				do
				{
					productEventHandler2 = productEventHandler;
					ProductEventHandler value2 = (ProductEventHandler)Delegate.Remove(productEventHandler2, value);
					productEventHandler = Interlocked.CompareExchange<ProductEventHandler>(ref this.OnProductCreated, value2, productEventHandler2);
				}
				while (productEventHandler != productEventHandler2);
			}
		}

		public event ProductEventHandler OnProductModified
		{
			add
			{
				ProductEventHandler productEventHandler = this.OnProductModified;
				ProductEventHandler productEventHandler2;
				do
				{
					productEventHandler2 = productEventHandler;
					ProductEventHandler value2 = (ProductEventHandler)Delegate.Combine(productEventHandler2, value);
					productEventHandler = Interlocked.CompareExchange<ProductEventHandler>(ref this.OnProductModified, value2, productEventHandler2);
				}
				while (productEventHandler != productEventHandler2);
			}
			remove
			{
				ProductEventHandler productEventHandler = this.OnProductModified;
				ProductEventHandler productEventHandler2;
				do
				{
					productEventHandler2 = productEventHandler;
					ProductEventHandler value2 = (ProductEventHandler)Delegate.Remove(productEventHandler2, value);
					productEventHandler = Interlocked.CompareExchange<ProductEventHandler>(ref this.OnProductModified, value2, productEventHandler2);
				}
				while (productEventHandler != productEventHandler2);
			}
		}

		public event ProductEventHandler OnProductDeleted
		{
			add
			{
				ProductEventHandler productEventHandler = this.OnProductDeleted;
				ProductEventHandler productEventHandler2;
				do
				{
					productEventHandler2 = productEventHandler;
					ProductEventHandler value2 = (ProductEventHandler)Delegate.Combine(productEventHandler2, value);
					productEventHandler = Interlocked.CompareExchange<ProductEventHandler>(ref this.OnProductDeleted, value2, productEventHandler2);
				}
				while (productEventHandler != productEventHandler2);
			}
			remove
			{
				ProductEventHandler productEventHandler = this.OnProductDeleted;
				ProductEventHandler productEventHandler2;
				do
				{
					productEventHandler2 = productEventHandler;
					ProductEventHandler value2 = (ProductEventHandler)Delegate.Remove(productEventHandler2, value);
					productEventHandler = Interlocked.CompareExchange<ProductEventHandler>(ref this.OnProductDeleted, value2, productEventHandler2);
				}
				while (productEventHandler != productEventHandler2);
			}
		}

		private void UpdateProductsGUI(CEstimatingsItemsControl itemsControl, CEstimatingItems estimatingItems)
		{
			DrawObjectGroup drawObjectGroup = null;
			if (this.selectedObject != null)
			{
				drawObjectGroup = this.selectedObject.Group;
			}
			if (drawObjectGroup != null)
			{
				itemsControl.UpdateGUI(estimatingItems, this.selectedObject);
				return;
			}
			itemsControl.Disable();
		}

		private void GetDefaultFormula(CEstimatingItemsBrowserInterface browserInterface, DrawObject selectedObject, CEstimatingItem product, string coUnit)
		{
			GroupStats stats = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, selectedObject, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, true, "");
			browserInterface.GetDefaultFormula(selectedObject, stats, product, coUnit);
		}

		private void ProductAdd(CEstimatingItemsBrowserInterface browserInterface, CEstimatingsItemsControl itemsControl, CEstimatingItems products, double value = 0.0)
		{
			Utilities.EnableInterface(this.drawArea.Owner, false);
			browserInterface.ShowBrowser(this.drawArea.Owner);
			Utilities.EnableInterface(this.drawArea.Owner, true);
			CEstimatingItem selectedProduct = browserInterface.Browser.SelectedProduct;
			if (selectedProduct == null)
			{
				return;
			}
			CEstimatingItem cestimatingItem = new CEstimatingItem(selectedProduct.ItemID, selectedProduct.Description, selectedProduct.Value, selectedProduct.Unit, selectedProduct.ItemType, selectedProduct.UnitMeasure, selectedProduct.CoverageValue, selectedProduct.CoverageUnit, selectedProduct.SectionID, selectedProduct.SubSectionID, selectedProduct.BidCode, "");
			cestimatingItem.Tag = cestimatingItem;
			if (value != 0.0)
			{
				cestimatingItem.Value = value;
			}
			this.GetDefaultFormula(browserInterface, this.selectedObject, cestimatingItem, selectedProduct.Unit);
			if (cestimatingItem.Formula == "")
			{
				GroupStats stats = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, this.selectedObject, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, true, "");
				FormulaResults formulaResults = new FormulaResults();
				formulaResults.Refresh(this.selectedObject, stats, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, this.drawArea.ActivePlan.UnitScale, this.selectedObject.Group.Presets, this.drawArea.Project.ExtensionsSupport);
				using (FormulaBuilderForm formulaBuilderForm = new FormulaBuilderForm(cestimatingItem, formulaResults, this.selectedObject, this.drawArea.Owner.ImageCollection))
				{
					formulaBuilderForm.ShowDialog(this.drawArea.Owner);
				}
			}
			if (cestimatingItem.Formula == "")
			{
				return;
			}
			products.Add(cestimatingItem);
			itemsControl.RefreshList();
			itemsControl.SelectItem(cestimatingItem);
			Console.WriteLine("product=" + cestimatingItem.Description);
			if (this.OnProductCreated != null)
			{
				this.OnProductCreated(selectedProduct);
			}
		}

		private void ProductRemove(CEstimatingsItemsControl itemsControl, CEstimatingItems products)
		{
			CEstimatingItem selectedProduct = itemsControl.SelectedProduct;
			if (selectedProduct == null)
			{
				return;
			}
			string supprimer_ce_produit_ = Resources.Supprimer_ce_produit_;
			string ce_produit_sera_supprimé_de_ce_groupe = Resources.Ce_produit_sera_supprimé_de_ce_groupe;
			if (Utilities.DisplayDeleteConfirmation(supprimer_ce_produit_, ce_produit_sera_supprimé_de_ce_groupe) == DialogResult.No)
			{
				return;
			}
			products.Remove(selectedProduct);
			if (this.OnProductDeleted != null)
			{
				this.OnProductDeleted(selectedProduct);
			}
			itemsControl.RefreshList();
		}

		private void ProductEdit(CEstimatingsItemsControl itemsControl)
		{
			CEstimatingItem selectedProduct = itemsControl.SelectedProduct;
			if (selectedProduct == null)
			{
				return;
			}
			GroupStats stats = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, this.selectedObject, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, true, "");
			FormulaResults formulaResults = new FormulaResults();
			formulaResults.Refresh(this.selectedObject, stats, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, this.drawArea.ActivePlan.UnitScale, this.selectedObject.Group.Presets, this.drawArea.Project.ExtensionsSupport);
			using (FormulaBuilderForm formulaBuilderForm = new FormulaBuilderForm(selectedProduct, formulaResults, this.selectedObject, this.drawArea.Owner.ImageCollection))
			{
				formulaBuilderForm.ShowDialog(this.drawArea.Owner);
			}
			if (itemsControl.Name == "EstimatingItems")
			{
				this.UpdateProductsGUI(itemsControl, this.selectedObject.Group.EstimatingItems);
			}
			else
			{
				itemsControl.RefreshList();
			}
			if (this.OnProductModified != null)
			{
				this.OnProductModified(selectedProduct);
			}
		}

		private void InitializeEstimatingItemsGUI()
		{
			this.project.DBManagement.InitializeBrowserInterface(this.drawArea.Owner.ImageCollection);
			this.estimatingItemsControl.OnProductAdd += this.estimatingItemsControl_OnProductAdd;
			this.estimatingItemsControl.OnProductRemove += this.estimatingItemsControl_OnProductRemove;
			this.estimatingItemsControl.OnFormulaEdit += this.estimatingItemsControl_OnFormulaEdit;
		}

		private void estimatingItemsControl_OnProductAdd(object sender, EventArgs e)
		{
			if (this.selectedObject == null)
			{
				return;
			}
			if (this.selectedObject.Group == null)
			{
				return;
			}
			DrawObjectGroup group = this.selectedObject.Group;
			this.ProductAdd(this.project.DBManagement.BrowserInterface, this.estimatingItemsControl, group.EstimatingItems, -1.0);
		}

		private void estimatingItemsControl_OnProductRemove(object sender, EventArgs e)
		{
			if (this.selectedObject == null)
			{
				return;
			}
			if (this.selectedObject.Group == null)
			{
				return;
			}
			DrawObjectGroup group = this.selectedObject.Group;
			this.ProductRemove(this.estimatingItemsControl, group.EstimatingItems);
		}

		private void estimatingItemsControl_OnFormulaEdit(object sender, EventArgs e)
		{
			if (this.selectedObject == null)
			{
				return;
			}
			if (this.selectedObject.Group == null)
			{
				return;
			}
			this.ProductEdit(this.estimatingItemsControl);
		}

		private void InitializeCOfficeGUI()
		{
			this.coInterface.InitializeBrowserInterface(this.drawArea.Owner.ImageCollection, this.coInterface.COSections[1] != null);
			this.coControl.OnProductAdd += this.coControl_OnProductAdd;
			this.coControl.OnProductRemove += this.coControl_OnProductRemove;
			this.coControl.OnFormulaEdit += this.coControl_OnFormulaEdit;
		}

		private void coControl_OnProductAdd(object sender, EventArgs e)
		{
			if (this.selectedObject == null)
			{
				return;
			}
			if (this.selectedObject.Group == null)
			{
				return;
			}
			DrawObjectGroup group = this.selectedObject.Group;
			this.ProductAdd(this.coInterface.BrowserInterface, this.coControl, group.COfficeProducts, 0.0);
		}

		private void coControl_OnProductRemove(object sender, EventArgs e)
		{
			if (this.selectedObject == null)
			{
				return;
			}
			if (this.selectedObject.Group == null)
			{
				return;
			}
			DrawObjectGroup group = this.selectedObject.Group;
			this.ProductRemove(this.coControl, group.COfficeProducts);
		}

		private void coControl_OnFormulaEdit(object sender, EventArgs e)
		{
			if (this.selectedObject == null)
			{
				return;
			}
			if (this.selectedObject.Group == null)
			{
				return;
			}
			this.ProductEdit(this.coControl);
		}

		private PropertySettings SetGridPropertyDisplayName(string propertyName, string displayName)
		{
			PropertySettings propertySettings = new PropertySettings(propertyName);
			propertySettings.DisplayName = displayName;
			for (int i = this.propertyGrid.PropertySettings.Count - 1; i >= 0; i--)
			{
				if (this.propertyGrid.PropertySettings[i].PropertyName == propertyName)
				{
					this.propertyGrid.PropertySettings.RemoveAt(i);
					break;
				}
			}
			this.propertyGrid.PropertySettings.Add(propertySettings);
			return propertySettings;
		}

		private void LoadResources()
		{
			this.SetGridPropertyDisplayName("Name", Resources.Nom);
			this.SetGridPropertyDisplayName("Comment", Resources.Commentaire);
			this.SetGridPropertyDisplayName("Text", Resources.Texte);
			this.SetGridPropertyDisplayName("Pattern", Resources.Motif);
			this.SetGridPropertyDisplayName("PenWidth", Resources.Épaisseur);
			this.SetGridPropertyDisplayName("FontSize", Resources.Taille);
			this.SetGridPropertyDisplayName("MaxRows", Resources.Items_par_colonne);
			this.SetGridPropertyDisplayName("Shape", Resources.Forme);
			this.SetGridPropertyDisplayName("RowsPerColumn", Resources.Items_par_colonne);
			this.SetGridPropertyDisplayName("ShowMeasure", Resources.Afficher_mesure);
			this.SetGridPropertyDisplayName("Visible", Resources.Visibilité);
			this.SetGridPropertyDisplayName("Label", Resources.Étiquette);
			this.SetGridPropertyDisplayName("CounterSize", Resources.Taille);
			this.SetGridPropertyDisplayName("IncludeAllPlans", "Inclure tous les plans");
			this.SetGridPropertyDisplayName("GroupCount", Resources.Nombre_d_objets);
			this.SetGridPropertyDisplayName("Length", Resources.Longueur);
			this.SetGridPropertyDisplayName("Area", Resources.Surface);
			this.SetGridPropertyDisplayName("Deduction", Resources.Déduction);
			this.SetGridPropertyDisplayName("AreaMinusDeduction", Resources.Surface_déductions);
			this.SetGridPropertyDisplayName("Perimeter", Resources.Périmètre);
			this.SetGridPropertyDisplayName("PerimeterPlusDeduction", Resources.Périmètre_déductions);
			this.SetGridPropertyDisplayName("DeductionsCount", Resources.Nombre_de_déductions);
			this.SetGridPropertyDisplayName("PerimeterMinusOpening", Resources.Longueur_sans_ouvertures);
			this.SetGridPropertyDisplayName("DropLength", Resources.Drop_length);
			this.SetGridPropertyDisplayName("NetLength", Resources.Longueur_nette);
			this.SetGridPropertyDisplayName("DropsCount", Resources.Drops_count);
			this.SetGridPropertyDisplayName("Openings", Resources.Longueur_des_ouvertures);
			this.SetGridPropertyDisplayName("OpeningsCount", Resources.Nombre_d_ouvertures);
			this.SetGridPropertyDisplayName("CornersCount", Resources.Nombre_de_coins);
			this.SetGridPropertyDisplayName("EndsCount", Resources.Nombre_de_bouts);
			this.SetGridPropertyDisplayName("SegmentsCount", Resources.Nombre_de_segments);
			this.SetGridPropertyDisplayName("TeesCount", Resources.Nombre_de_tés);
			this.SetGridPropertyDisplayName("Angle", Resources.Angle);
			this.SetGridPropertyDisplayName("RenderingAngle", Resources.Angle);
			this.SetGridPropertyDisplayName("RenderingOffsetX", Resources.Décalage_horizontal);
			this.SetGridPropertyDisplayName("RenderingOffsetY", Resources.Décalage_vertical);
		}

		private void InitializeCombo()
		{
			this.comboBox.DrawItem += this.combo_DrawItem;
			this.comboBox.SelectedIndexChanged += this.combo_SelectedIndexChanged;
			this.comboBox.Sorted = true;
		}

		private void InitializeColorPicker()
		{
			PropertySettings propertySettings = new PropertySettings("Color");
			this.colorPickerEditor = new UITypeEditorColorPicker();
			this.colorPickerEditor.OnColorHover += this.colorPickerEditor_OnColorHover;
			propertySettings.DisplayName = Resources.Couleur;
			propertySettings.UITypeEditor = this.colorPickerEditor;
			this.propertyGrid.PropertySettings.Add(propertySettings);
			PropertySettings propertySettings2 = new PropertySettings("PenWidth");
			propertySettings2.DisplayName = Resources.Épaisseur;
			propertySettings2.ValueEditor = new PropertySliderEditor(1, 100);
			this.propertyGrid.PropertySettings.Add(propertySettings2);
			PropertySettings propertySettings3 = new PropertySettings("PresetResult1");
			propertySettings3.ValueEditor = new PropertySliderEditor(1, 100);
			this.propertyGrid.PropertySettings.Add(propertySettings3);
		}

		private void InitializeSlopeFactorControl()
		{
			PropertySettings propertySettings = new PropertySettings("SlopeFactor");
			this.slopeFactorEditor = new UITypeEditorSlopeControl();
			this.slopeFactorEditor.OnSave += this.slopeFactorEditor_OnSave;
			propertySettings.ConvertFromStringToPropertyValue += this.SlopeFactorUITypeEditorConvertFromStringToPropertyValue;
			propertySettings.ConvertPropertyValueToString += this.SlopeFactorUITypeEditorConvertPropertyValueToString;
			propertySettings.DisplayName = Resources.Facteur_de_pente;
			propertySettings.UITypeEditor = this.slopeFactorEditor;
			this.propertyGrid.PropertySettings.Add(propertySettings);
		}

		private void SlopeFactorUITypeEditorConvertFromStringToPropertyValue(object sender, ConvertValueEventArgs e)
		{
			if (e.TypedValue == null)
			{
				e.StringValue = "";
			}
			else
			{
				SlopeFactor slopeFactor = (SlopeFactor)e.TypedValue;
				e.StringValue = ((slopeFactor == null) ? "" : slopeFactor.ToString());
			}
			e.IsConverted = true;
		}

		private void SlopeFactorUITypeEditorConvertPropertyValueToString(object sender, ConvertValueEventArgs e)
		{
			e.IsConverted = true;
		}

		private void InitializeGrid()
		{
			this.properties = new DrawObjectProperties();
			this.propertyGrid.SelectedObject = this.properties;
			this.propertyGrid.PropertySort = ePropertySort.Categorized;
			this.propertyGrid.SearchBoxVisible = false;
			this.propertyGrid.ToolbarVisible = false;
			this.propertyGrid.Enabled = false;
			this.propertyGrid.PropertyTree.GridColumnLineResizeEnabled = false;
			this.propertyGrid.PropertyTree.AllowUserToResizeColumns = false;
			this.propertyGrid.Appearance.CategoryStyle = new ElementStyle(Color.DarkSlateGray);
			this.propertyGrid.Appearance.ReadOnlyPropertyStyle = new ElementStyle(Color.Black);
			this.propertyGrid.PropertyTree.SelectionFocusAware = true;
			this.propertyGrid.Localize += this.grid_Localize;
			this.propertyGrid.ConvertPropertyValueToString += this.grid_ConvertPropertyValueToString;
			this.propertyGrid.ConvertFromStringToPropertyValue += this.grid_ConvertFromStringToPropertyValue;
			this.propertyGrid.PropertyValueChanging += this.grid_PropertyValueChanging;
			this.propertyGrid.ValidatePropertyValue += this.grid_ValidatePropertyValue;
			this.propertyGrid.ProvidePropertyValueList += this.propertyGrid_ProvidePropertyValueList;
			this.propertyGrid.PropertyTree.KeyDown += this.propertyTree_KeyDown;
			((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForThisPlan"]).CheckedChanged += this.DisplayCalculations_CheckedChanged;
			((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForAllPlans"]).CheckedChanged += this.DisplayCalculations_CheckedChanged;
		}

		private void InitializeTabs()
		{
			this.tabProperties.SelectedTabChanged += this.tabProperties_SelectedTabChanged;
			this.superTabProperties.SelectedTabChanged += this.superTabProperties_SelectedTabChanged;
		}

		private void InitializeTemplatesManager()
		{
			this.extensionsManager.OnPresetCreated += this.extensionsManager_OnPresetCreated;
			this.extensionsManager.OnPresetRenamed += this.extensionsManager_OnPresetRenamed;
			this.extensionsManager.OnPresetModified += this.extensionsManager_OnPresetModified;
			this.extensionsManager.OnPresetDeleted += this.extensionsManager_OnPresetDeleted;
			this.extensionsManager.OnPresetAfterDeleted += this.extensionsManager_OnPresetAfterDeleted;
		}

		public DrawObjectEditor(Project project, DrawingArea drawArea, ExtensionsSupport extensionSupport, ComboBoxEx comboBox, AdvPropertyGrid propertyGrid, ExtensionsManager extensionsManager, DevComponents.DotNetBar.TabControl tabProperties, SuperTabControl superTabProperties, Bar barDisplayCalculations, CEstimatingsItemsControl estimatingItemsControl, CEstimatingsItemsControl coControl, COfficeInterface coInterface)
		{
			this.project = project;
			this.drawArea = drawArea;
			this.extensionSupport = extensionSupport;
			this.comboBox = comboBox;
			this.propertyGrid = propertyGrid;
			this.extensionsManager = extensionsManager;
			this.tabProperties = tabProperties;
			this.superTabProperties = superTabProperties;
			this.barDisplayCalculations = barDisplayCalculations;
			this.enabled = true;
			this.estimatingItemsControl = estimatingItemsControl;
			this.InitializeEstimatingItemsGUI();
			this.coInterface = coInterface;
			this.coControl = coControl;
			if (coInterface.IsReady)
			{
				this.InitializeCOfficeGUI();
			}
			this.InitializeCombo();
			this.InitializeGrid();
			this.InitializeTabs();
			this.InitializeTemplatesManager();
			this.InitializeColorPicker();
			this.InitializeSlopeFactorControl();
			this.LoadResources();
		}

		private PropertyNode GetPropertyNode(string propertyName)
		{
			PropertyNode result;
			try
			{
				result = this.propertyGrid.GetPropertyNode(propertyName);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private bool CustomRenderingEnabled(Preset preset)
		{
			return this.selectedObject != null && preset != null && this.selectedObject.ObjectType == "Area" && preset.CustomRendering != null;
		}

		private List<string> GetRenderingSameProperties(Preset preset, CustomRenderingProperties propertiesToCompare)
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			List<string> list = new List<string>();
			if (preset != null)
			{
				for (int i = 0; i < this.drawArea.SelectionCount; i++)
				{
					DrawObject drawObject = this.drawArea.GetSelectedObject(i);
					if (drawObject.ObjectType == "Area" && drawObject.HasSameGroupOrID(this.selectedObject))
					{
						CustomRenderingProperties customRenderingProperties = ((DrawPolyLine)drawObject).GetCustomRenderingProperties(preset.ID);
						flag = (flag && customRenderingProperties.Angle == propertiesToCompare.Angle);
						flag2 = (flag2 && customRenderingProperties.OffsetX == propertiesToCompare.OffsetX);
						flag3 = (flag3 && customRenderingProperties.OffsetY == propertiesToCompare.OffsetY);
					}
					if (!flag && !flag2 && !flag3)
					{
						break;
					}
				}
			}
			if (flag)
			{
				list.Add("RenderingAngle");
			}
			if (flag2)
			{
				list.Add("RenderingOffsetX");
			}
			if (flag3)
			{
				list.Add("RenderingOffsetY");
			}
			if (flag2 || flag3)
			{
				list.Add("RenderingOffsetScale");
			}
			return list;
		}

		private DrawObject ActiveObject
		{
			set
			{
				this.selectedObject = value;
				this.RefreshProperties();
			}
		}

		private void combo_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			e.DrawBackground();
			DrawObject drawObject = this[e.Index];
			if (drawObject == null)
			{
				return;
			}
			using (Image image = new Bitmap(17, 17))
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					this.drawArea.DrawObjectIcon(drawObject, graphics);
					ColorMatrix colorMatrix = new ColorMatrix();
					colorMatrix.Matrix33 = (drawObject.Visible ? 1f : 0.35f);
					ImageAttributes imageAttributes = new ImageAttributes();
					imageAttributes.SetColorMatrix(colorMatrix);
					e.Graphics.DrawImage(image, new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 2, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
				}
			}
			Rectangle rect = new Rectangle(e.Bounds.Width - 18, e.Bounds.Y + 5, 10, 10);
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(drawObject.Visible ? 255 : 35, Color.Black), 2f), rect);
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(drawObject.Visible ? 255 : 135, drawObject.Color)), rect);
			string name = ((e.State & DrawItemState.Selected) != DrawItemState.None) ? "HighlightText" : "WindowText";
			e.Graphics.DrawString(drawObject.Name, this.comboBox.Font, new SolidBrush(Color.FromArgb(drawObject.Visible ? 255 : 150, Color.FromName(name))), new RectangleF(27f, (float)e.Bounds.Y, (float)(e.Bounds.Width - 40), (float)e.Bounds.Height));
		}

		private void combo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			Utilities.DisplayName displayName = (Utilities.DisplayName)this.comboBox.SelectedItem;
			if (displayName != null)
			{
				if (this.selectedObject != null && (((DrawObject)displayName.Owner).ID == this.selectedObject.ID || (((DrawObject)displayName.Owner).GroupID == this.selectedObject.GroupID && this.selectedObject.GroupID != -1)))
				{
					return;
				}
				this.ActiveObject = (DrawObject)displayName.Owner;
			}
			else
			{
				this.ActiveObject = null;
			}
			if (this.OnObjectSelected != null)
			{
				this.OnObjectSelected(this.selectedObject);
			}
		}

		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				this.comboBox.Enabled = this.enabled;
				this.propertyGrid.Enabled = this.enabled;
				this.exitNow = true;
				((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForThisPlan"]).Checked = !this.project.DisplayResultsForAllPlans;
				((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForAllPlans"]).Checked = this.project.DisplayResultsForAllPlans;
				this.barDisplayCalculations.Enabled = this.enabled;
				this.exitNow = false;
				if (!this.enabled)
				{
					this.Clear();
				}
			}
		}

		public bool IsEditing
		{
			get
			{
				bool result;
				try
				{
					string[] array = this.editableProperties.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						PropertyNode propertyNode = this.GetPropertyNode(array[i]);
						if (propertyNode != null && propertyNode.IsEditing)
						{
							return true;
						}
					}
					result = false;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					result = false;
				}
				return result;
			}
		}

		public DrawObject SelectedObject
		{
			get
			{
				return this.selectedObject;
			}
		}

		public DrawObject this[int index]
		{
			get
			{
				DrawObject result;
				try
				{
					Utilities.DisplayName displayName = (Utilities.DisplayName)this.comboBox.Items[index];
					result = (DrawObject)displayName.Owner;
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public int Count
		{
			get
			{
				return this.comboBox.Items.Count;
			}
		}

		public void Clear()
		{
			for (int i = this.comboBox.Items.Count - 1; i >= 0; i--)
			{
			}
			this.comboBox.Items.Clear();
			this.ActiveObject = null;
		}

		public void Add(DrawObject drawObject, bool selectObject)
		{
			int selectedIndex = this.comboBox.Items.Add(drawObject.DisplayName);
			if (selectObject)
			{
				this.exitNow = true;
				this.comboBox.SelectedIndex = selectedIndex;
				this.ActiveObject = drawObject;
				this.exitNow = false;
			}
		}

		public void Insert(int index, DrawObject drawObject)
		{
			this.comboBox.Items.Insert(index, drawObject.DisplayName);
		}

		public void Rename(DrawObject drawObject)
		{
			for (int i = 0; i < this.comboBox.Items.Count; i++)
			{
				if (this[i] != null && this[i].HasSameGroupOrID(drawObject))
				{
					this.exitNow = true;
					this[i].Name = drawObject.Name;
					this.comboBox.Items[i] = drawObject.DisplayName;
					this.exitNow = false;
					return;
				}
			}
		}

		public void RemoveDeletedObjects()
		{
			if (this.selectedObject != null)
			{
				if (this.selectedObject.IsPartOfGroup())
				{
					if (!GroupUtilities.GroupExists(this.drawArea.ActivePlan, this.selectedObject.GroupID))
					{
						this.ActiveObject = null;
					}
				}
				else if (this.selectedObject.ID == -1)
				{
					this.ActiveObject = null;
				}
			}
			for (int i = this.comboBox.Items.Count - 1; i >= 0; i--)
			{
				bool flag;
				if (this[i].IsPartOfGroup())
				{
					flag = !GroupUtilities.GroupExists(this.drawArea.ActivePlan, this[i].GroupID);
				}
				else
				{
					flag = (this.drawArea.GetObjectByID(this.drawArea.ActivePlan, this[i].ID) == null);
				}
				if (flag)
				{
					this.comboBox.Items.RemoveAt(i);
				}
			}
		}

		public void ForceSelection(DrawObject drawObject)
		{
			int num = -1;
			bool flag = false;
			if (drawObject != null && drawObject.DeductionParentID != -1)
			{
				drawObject = this.drawArea.ActiveDrawingObjects.GetObjectByID(drawObject.DeductionParentID);
			}
			if (drawObject != null)
			{
				Utilities.DisplayName displayName = (Utilities.DisplayName)this.comboBox.SelectedItem;
				if (displayName != null)
				{
					flag = ((DrawObject)displayName.Owner).HasSameGroupOrID(drawObject);
				}
				if (flag)
				{
					this.ActiveObject = drawObject;
					return;
				}
				for (int i = 0; i < this.comboBox.Items.Count; i++)
				{
					if (this[i] != null)
					{
						if (this[i].HasSameGroupOrID(drawObject))
						{
							num = i;
						}
						if (num != -1)
						{
							this.exitNow = true;
							this.comboBox.SelectedIndex = i;
							this.ActiveObject = drawObject;
							this.exitNow = false;
							break;
						}
					}
				}
				if (num == -1)
				{
					this.DisableEditor();
					this.exitNow = false;
					return;
				}
			}
			else
			{
				Console.WriteLine("DrawObject was null");
				this.DisableEditor();
				this.exitNow = false;
			}
		}

		public void ResetAllocatedGroups(int groupID)
		{
			bool flag = false;
			for (int i = 0; i < this.Count; i++)
			{
				DrawObject drawObject = this[i];
				if (drawObject.GroupID == groupID)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				DrawObject drawObject2 = this.drawArea.FindObjectFromGroupID(groupID);
				if (drawObject2 != null)
				{
					this.Add(drawObject2, false);
					return;
				}
				Console.WriteLine("ResetAllocatedGroups::FindObjectFromGroupID null");
			}
		}

		public void ResetItems()
		{
			for (int i = 0; i < this.comboBox.Items.Count; i++)
			{
				Utilities.DisplayName value = (Utilities.DisplayName)this.comboBox.Items[i];
				this.exitNow = true;
				this.comboBox.Items[i] = value;
				this.exitNow = false;
			}
		}

		public void ReplaceObject(DrawObject drawObject)
		{
			if (drawObject != null)
			{
				for (int i = 0; i < this.comboBox.Items.Count; i++)
				{
					Utilities.DisplayName displayName = (Utilities.DisplayName)this.comboBox.Items[i];
					DrawObject drawObject2 = (DrawObject)displayName.Owner;
					if (drawObject2.HasSameGroupOrID(drawObject))
					{
						this.exitNow = true;
						this.comboBox.Items[i] = drawObject.DisplayName;
						this.exitNow = false;
					}
				}
			}
		}

		public void Refresh()
		{
			this.RefreshProperties();
		}

		private void UpdateProperty(string propertyName, object value)
		{
			GroupUtilities.UpdateGroupProperty(this.project, this.drawArea, this.selectedObject, propertyName, value);
			if (this.OnObjectChanged != null)
			{
				this.OnObjectChanged(this.selectedObject, propertyName, value);
			}
		}

		private void ValidatePropertyValue(ValidatePropertyValueEventArgs e)
		{
			string propertyName;
			switch (propertyName = e.PropertyName)
			{
			case "Name":
			{
				string text = ((string)e.NewValue).Trim();
				e.Cancel = (text == string.Empty);
				if (e.Cancel)
				{
					e.Message = Resources.Vous_devez_spécifier_un_nom;
					return;
				}
				if (!(this.selectedObject.Name != text))
				{
					return;
				}
				string empty = string.Empty;
				e.Cancel = !this.drawArea.ValidateDrawObjectName(ref text, ref empty, ref e.Message);
				if (e.Cancel)
				{
					e.Message = empty + "\n" + e.Message;
					return;
				}
				this.selectedObject.Name = text;
				this.Rename(this.selectedObject);
				this.UpdateProperty(e.PropertyName, text);
				return;
			}
			case "Label":
			{
				string text2 = ((string)e.NewValue).Trim();
				if (this.selectedObject.Label != text2)
				{
					this.UpdateProperty(e.PropertyName, text2);
					return;
				}
				return;
			}
			case "Text":
			{
				string text3 = ((string)e.NewValue).Trim();
				if (this.selectedObject.ObjectType == "Counter")
				{
					text3 = Utilities.Substring(text3, 0, 5);
				}
				this.UpdateProperty(e.PropertyName, text3);
				return;
			}
			case "Comment":
			{
				string value = ((string)e.NewValue).Trim();
				this.UpdateProperty(e.PropertyName, value);
				return;
			}
			case "Color":
			{
				Color color = (Color)e.NewValue;
				this.UpdateProperty(e.PropertyName, color);
				return;
			}
			case "Pattern":
			{
				HatchStylePickerCombo.HatchStylePickerEnum hatchStylePickerEnum = (HatchStylePickerCombo.HatchStylePickerEnum)e.NewValue;
				this.UpdateProperty(e.PropertyName, hatchStylePickerEnum);
				return;
			}
			case "PenWidth":
			{
				int num2 = (int)e.NewValue;
				this.UpdateProperty(e.PropertyName, num2);
				return;
			}
			case "FontSize":
			{
				int num3 = (int)e.NewValue;
				this.UpdateProperty(e.PropertyName, num3);
				return;
			}
			case "MaxRows":
			{
				int num4 = (int)e.NewValue;
				this.UpdateProperty(e.PropertyName, num4);
				return;
			}
			case "Shape":
			{
				DrawCounter.CounterShapeTypeEnum counterShapeTypeEnum = (DrawCounter.CounterShapeTypeEnum)e.NewValue;
				this.UpdateProperty(e.PropertyName, counterShapeTypeEnum);
				return;
			}
			case "CounterSize":
			{
				int num5 = (int)e.NewValue;
				this.UpdateProperty(e.PropertyName, num5);
				return;
			}
			case "SlopeFactor":
			{
				SlopeFactor value2 = (SlopeFactor)e.NewValue;
				this.UpdateProperty(e.PropertyName, value2);
				return;
			}
			case "Visible":
			{
				bool flag = (bool)e.NewValue;
				this.UpdateProperty(e.PropertyName, flag);
				return;
			}
			case "IncludeAllPlans":
			{
				bool flag2 = (bool)e.NewValue;
				this.UpdateProperty(e.PropertyName, flag2);
				this.RefreshProperties();
				return;
			}
			case "ShowMeasure":
			{
				bool flag3 = (bool)e.NewValue;
				this.UpdateProperty(e.PropertyName, flag3);
				return;
			}
			case "RenderingAngle":
			case "RenderingOffsetX":
			case "RenderingOffsetY":
			{
				int num6 = (int)e.NewValue;
				this.UpdateProperty(e.PropertyName, num6);
				this.drawArea.Owner.SetModified();
				this.RefreshRenderingResults();
				return;
			}
			}
			if (e.NewValue.GetType().Name == "ExtensionChoiceElement")
			{
				ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)e.NewValue;
				if (extensionChoiceElement == null)
				{
					e.Cancel = true;
					e.Message = Resources.Choix_invalide;
					return;
				}
				Preset selectedPreset = this.GetSelectedPreset(false);
				if (selectedPreset == null)
				{
					e.Cancel = true;
					e.Message = Resources.Choix_invalide;
					return;
				}
				PresetChoice presetChoice = selectedPreset.Choices.Find(extensionChoiceElement.Parent.Name);
				if (presetChoice == null)
				{
					e.Cancel = true;
					e.Message = Resources.Choix_invalide;
					return;
				}
				presetChoice.ChoiceElementName = extensionChoiceElement.Name;
				presetChoice.ChoiceElementCaption = extensionChoiceElement.Caption;
				presetChoice.Variables.Clear();
				foreach (object obj in extensionChoiceElement.Variables.Collection)
				{
					Variable variable = (Variable)obj;
					presetChoice.Variables.Add(new Variable(variable.Name, variable.Value));
				}
				this.lastRefreshedObject = null;
				selectedPreset.SynchWithTemplate(this.extensionSupport);
				this.RefreshProperties();
				if (this.OnPresetModified != null)
				{
					this.OnPresetModified(selectedPreset);
					return;
				}
			}
			else if (e.PropertyName.IndexOf("PresetField") != -1)
			{
				this.lastRefreshedObject = null;
				this.RefreshProperties();
				if (this.OnPresetModified != null)
				{
					this.OnPresetModified(this.GetSelectedPreset(false));
				}
			}
		}

		private void DisableEditor()
		{
			this.selectedObject = null;
			this.lastRefreshedObject = null;
			this.exitNow = true;
			this.comboBox.SelectedIndex = -1;
			this.exitNow = false;
			this.editableProperties.Clear();
			this.propertyGrid.IgnoredCategories = new string[]
			{
				"Properties",
				"SelectedObjectsProperties",
				"PresetProperties",
				"PresetCustomRendering",
				"Values",
				"PresetResults"
			};
			this.extensionsManager.Disable();
			this.tabProperties.SelectedTabIndex = 0;
			this.estimatingItemsControl.Disable();
			if (this.coInterface.IsReady)
			{
				this.coControl.Disable();
			}
			this.DisableSuperTab();
		}

		private void DisableSuperTab()
		{
			this.superTabProperties.TabsVisible = false;
			this.ClearSuperTab();
		}

		private void RefreshSelectedGroup()
		{
			bool flag = true;
			int num = -1;
			if (this.extensionsManager.Group != null)
			{
				num = this.extensionsManager.Group.ID;
			}
			DrawObjectGroup group = this.selectedObject.Group;
			this.extensionsManager.SelectGroup(group, this.extensionSupport, this.drawArea.UnitScale);
			if (group != null)
			{
				this.UpdateProductsGUI(this.estimatingItemsControl, group.EstimatingItems);
			}
			else
			{
				this.estimatingItemsControl.Disable();
			}
			if (this.coInterface.IsReady)
			{
				if (group != null)
				{
					this.UpdateProductsGUI(this.coControl, group.COfficeProducts);
				}
				else
				{
					this.coControl.Disable();
				}
			}
			if (group != null)
			{
				flag = (group.Presets.Count == 0);
			}
			if (!flag)
			{
				if (num != group.ID)
				{
					this.RefreshSuperTab();
					return;
				}
			}
			else
			{
				this.DisableSuperTab();
			}
		}

		private Preset GetSelectedPreset(bool setTabIndex)
		{
			Preset groupSelectedPreset = this.drawArea.GetGroupSelectedPreset(this.selectedObject);
			if (groupSelectedPreset == null || this.superTabProperties.Tabs.Count < 2)
			{
				this.drawArea.Refresh();
				return null;
			}
			if (setTabIndex)
			{
				this.exitNow = true;
				this.superTabProperties.SelectedTabIndex = this.LocatePresetSuperTab(groupSelectedPreset);
				this.exitNow = false;
			}
			this.drawArea.Refresh();
			return groupSelectedPreset;
		}

		private string[] GetIgnoredPresetProperties(Preset preset)
		{
			List<string> list = new List<string>();
			if (preset != null)
			{
				for (int i = preset.Choices.Count; i < 25; i++)
				{
					list.Add("PresetChoice" + (i + 1).ToString());
				}
				for (int j = preset.Fields.Count; j < 25; j++)
				{
					list.Add("PresetField" + (j + 1).ToString());
				}
				list.Add("RenderingAngle");
				list.Add("RenderingOffsetX");
				list.Add("RenderingOffsetY");
				int num = 0;
				foreach (object obj in preset.Results.Collection)
				{
					PresetResult presetResult = (PresetResult)obj;
					if (presetResult.ConditionMet)
					{
						num++;
					}
				}
				for (int k = num; k < 50; k++)
				{
					list.Add("PresetResult" + (k + 1).ToString());
				}
			}
			return list.ToArray();
		}

		private string[] RemoveFromIgnoredPresetProperties(string[] currentList, string[] elementsToRemove)
		{
			List<string> list = new List<string>(currentList);
			foreach (string item in elementsToRemove)
			{
				list.Remove(item);
			}
			return list.ToArray();
		}

		private void InitializeChoiceElementsUITypeEditor(PropertySettings propertySetting, ExtensionChoice choice)
		{
			if (choice == null)
			{
				return;
			}
			propertySetting.UITypeEditor = new UITypeEditorChoiceElements();
			((UITypeEditorChoiceElements)propertySetting.UITypeEditor).PropertyGrid = this.propertyGrid;
			((UITypeEditorChoiceElements)propertySetting.UITypeEditor).Choice = choice;
			propertySetting.ConvertFromStringToPropertyValue += this.ChoiceElementsUITypeEditorConvertFromStringToPropertyValue;
			propertySetting.ConvertPropertyValueToString += this.ChoiceElementsUITypeEditorConvertPropertyValueToString;
		}

		private void ChoiceElementsUITypeEditorConvertPropertyValueToString(object sender, ConvertValueEventArgs e)
		{
			if (e.TypedValue == null)
			{
				e.StringValue = "";
			}
			else
			{
				ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)e.TypedValue;
				e.StringValue = ((extensionChoiceElement == null) ? "" : extensionChoiceElement.Caption);
			}
			e.IsConverted = true;
		}

		private void ChoiceElementsUITypeEditorConvertFromStringToPropertyValue(object sender, ConvertValueEventArgs e)
		{
			string b = (e.StringValue ?? "").ToLower();
			PropertySettings propertySettings = (PropertySettings)sender;
			ExtensionChoice choice = ((UITypeEditorChoiceElements)propertySettings.UITypeEditor).Choice;
			if (choice != null)
			{
				foreach (object obj in choice.Elements.Collection)
				{
					ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)obj;
					if (extensionChoiceElement.Caption.ToLower() == b)
					{
						e.TypedValue = extensionChoiceElement;
						e.IsConverted = true;
						return;
					}
				}
			}
			e.IsConverted = false;
		}

		private void SetPresetChoiceProperty(int index, object value)
		{
			switch (index)
			{
			case 0:
				this.properties.PresetChoice1 = value;
				return;
			case 1:
				this.properties.PresetChoice2 = value;
				return;
			case 2:
				this.properties.PresetChoice3 = value;
				return;
			case 3:
				this.properties.PresetChoice4 = value;
				return;
			case 4:
				this.properties.PresetChoice5 = value;
				return;
			case 5:
				this.properties.PresetChoice6 = value;
				return;
			case 6:
				this.properties.PresetChoice7 = value;
				return;
			case 7:
				this.properties.PresetChoice8 = value;
				return;
			case 8:
				this.properties.PresetChoice9 = value;
				return;
			case 9:
				this.properties.PresetChoice10 = value;
				return;
			default:
				return;
			}
		}

		private void SetPresetFieldProperty(int index, string value)
		{
			switch (index)
			{
			case 0:
				this.properties.PresetField1 = value;
				return;
			case 1:
				this.properties.PresetField2 = value;
				return;
			case 2:
				this.properties.PresetField3 = value;
				return;
			case 3:
				this.properties.PresetField4 = value;
				return;
			case 4:
				this.properties.PresetField5 = value;
				return;
			case 5:
				this.properties.PresetField6 = value;
				return;
			case 6:
				this.properties.PresetField7 = value;
				return;
			case 7:
				this.properties.PresetField8 = value;
				return;
			case 8:
				this.properties.PresetField9 = value;
				return;
			case 9:
				this.properties.PresetField10 = value;
				return;
			default:
				return;
			}
		}

		private string FormatPresetResult(string value, ExtensionResult.ExtensionResultTypeEnum resultType)
		{
			if (resultType == ExtensionResult.ExtensionResultTypeEnum.ResultTypeCustom)
			{
				return value;
			}
			double value2 = Utilities.ConvertToDouble(value, -1);
			Preset selectedPreset = this.GetSelectedPreset(false);
			switch (resultType)
			{
			case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
				return this.drawArea.UnitScale.ToLengthStringFromUnitSystem(value2, false, true, true);
			case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
				return this.drawArea.UnitScale.ToAreaStringFromUnitSystem(value2, true);
			case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
				return this.drawArea.UnitScale.ToCubicStringFromUnitSystem(value2, true);
			case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
				return this.drawArea.ToCurrency(value2);
			default:
				return this.drawArea.UnitScale.Round(value2).ToString();
			}
		}

		private void SetPresetResult(int index, string value, string unit, ExtensionResult.ExtensionResultTypeEnum resultType)
		{
			switch (index)
			{
			case 0:
				this.properties.PresetResult1 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 1:
				this.properties.PresetResult2 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 2:
				this.properties.PresetResult3 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 3:
				this.properties.PresetResult4 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 4:
				this.properties.PresetResult5 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 5:
				this.properties.PresetResult6 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 6:
				this.properties.PresetResult7 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 7:
				this.properties.PresetResult8 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 8:
				this.properties.PresetResult9 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 9:
				this.properties.PresetResult10 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 10:
				this.properties.PresetResult11 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 11:
				this.properties.PresetResult12 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 12:
				this.properties.PresetResult13 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 13:
				this.properties.PresetResult14 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 14:
				this.properties.PresetResult15 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 15:
				this.properties.PresetResult16 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 16:
				this.properties.PresetResult17 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 17:
				this.properties.PresetResult18 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 18:
				this.properties.PresetResult19 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 19:
				this.properties.PresetResult20 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 20:
				this.properties.PresetResult21 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 21:
				this.properties.PresetResult22 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 22:
				this.properties.PresetResult23 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 23:
				this.properties.PresetResult24 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 24:
				this.properties.PresetResult25 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 25:
				this.properties.PresetResult26 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 26:
				this.properties.PresetResult27 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 27:
				this.properties.PresetResult28 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 28:
				this.properties.PresetResult29 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 29:
				this.properties.PresetResult30 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 30:
				this.properties.PresetResult31 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 31:
				this.properties.PresetResult32 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 32:
				this.properties.PresetResult33 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 33:
				this.properties.PresetResult34 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 34:
				this.properties.PresetResult35 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 35:
				this.properties.PresetResult36 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 36:
				this.properties.PresetResult37 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 37:
				this.properties.PresetResult38 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 38:
				this.properties.PresetResult39 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 39:
				this.properties.PresetResult40 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 40:
				this.properties.PresetResult41 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 41:
				this.properties.PresetResult42 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 42:
				this.properties.PresetResult43 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 43:
				this.properties.PresetResult44 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 44:
				this.properties.PresetResult45 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 45:
				this.properties.PresetResult46 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 46:
				this.properties.PresetResult47 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 47:
				this.properties.PresetResult48 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 48:
				this.properties.PresetResult49 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			case 49:
				this.properties.PresetResult50 = this.FormatPresetResult(value, resultType) + ((unit != string.Empty) ? (" " + unit) : "");
				return;
			default:
				return;
			}
		}

		private string FormatField(string value, ExtensionField.ExtensionFieldTypeEnum fieldType)
		{
			double value2 = Utilities.ConvertToDouble(value, -1);
			if (fieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension)
			{
				return this.drawArea.UnitScale.ToLengthStringFromUnitSystem(value2, false, false, true);
			}
			return value2.ToString();
		}

		private void RefreshRenderingResults()
		{
			Plan activePlan = this.drawArea.ActivePlan;
			Preset selectedPreset = this.GetSelectedPreset(false);
			if (this.drawArea.ActivePlan == null || this.selectedObject == null || selectedPreset == null)
			{
				return;
			}
			GroupStats objectStats;
			if (!this.project.DisplayResultsForAllPlans)
			{
				objectStats = GroupUtilities.ComputeGroupStats(activePlan, this.selectedObject, activePlan.UnitScale.ScaleSystemType, true, "");
			}
			else
			{
				objectStats = GroupUtilities.ComputeGroupStats(this.project, this.selectedObject, null, activePlan.UnitScale.ScaleSystemType, true, "");
			}
			this.extensionSupport.QueryPresetResults(selectedPreset, objectStats, activePlan.UnitScale);
			this.UpdatePresetProperties(selectedPreset, activePlan.UnitScale, true);
		}

		private void ClearProperties(string prefix)
		{
			for (int i = this.propertyGrid.PropertySettings.Count - 1; i >= 0; i--)
			{
				if (this.propertyGrid.PropertySettings[i].PropertyName.IndexOf(prefix) != -1)
				{
					this.propertyGrid.PropertySettings.RemoveAt(i);
				}
			}
		}

		private void UpdatePresetProperties(Preset preset, UnitScale unitScale, bool updateResultsOnly = false)
		{
			int num = 0;
			if (!updateResultsOnly)
			{
				this.editableProperties.Clear();
				for (int i = 0; i < 25; i++)
				{
					this.propertyFields[i] = null;
				}
				string[] array = this.GetIgnoredPresetProperties(preset);
				if (this.CustomRenderingEnabled(preset))
				{
					CustomRenderingProperties customRenderingProperties = ((DrawPolyLine)this.selectedObject).GetCustomRenderingProperties(preset.ID);
					List<string> renderingSameProperties = this.GetRenderingSameProperties(preset, customRenderingProperties);
					if (renderingSameProperties.Count > 0)
					{
						array = this.RemoveFromIgnoredPresetProperties(array, renderingSameProperties.ToArray());
						if (renderingSameProperties.Contains("RenderingAngle"))
						{
							this.properties.RenderingAngle = customRenderingProperties.Angle;
						}
						if (renderingSameProperties.Contains("RenderingOffsetX"))
						{
							this.properties.RenderingOffsetX = customRenderingProperties.OffsetX;
						}
						if (renderingSameProperties.Contains("RenderingOffsetY"))
						{
							this.properties.RenderingOffsetY = customRenderingProperties.OffsetY;
						}
					}
				}
				this.propertyGrid.IgnoredProperties = array;
				foreach (object obj in preset.Choices.Collection)
				{
					PresetChoice presetChoice = (PresetChoice)obj;
					if (num == 25)
					{
						break;
					}
					try
					{
						ExtensionChoice extensionChoice = this.extensionSupport.FindChoice(preset.CategoryName, preset.ExtensionName, presetChoice.ChoiceName);
						ExtensionChoiceElement value = extensionChoice.FindElement(presetChoice.ChoiceElementName);
						this.editableProperties.Add(presetChoice.ChoiceCaption);
						PropertySettings propertySettings = this.SetGridPropertyDisplayName("PresetChoice" + (num + 1).ToString(), presetChoice.ChoiceCaption);
						if (propertySettings != null)
						{
							this.InitializeChoiceElementsUITypeEditor(propertySettings, extensionChoice);
						}
						this.SetPresetChoiceProperty(num, value);
					}
					catch
					{
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in preset.Fields.Collection)
				{
					PresetField presetField = (PresetField)obj2;
					if (num == 25)
					{
						break;
					}
					this.editableProperties.Add(presetField.Caption);
					this.SetGridPropertyDisplayName("PresetField" + (num + 1).ToString(), presetField.Caption);
					this.propertyFields[num] = presetField;
					int decimals = (presetField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency) ? 2 : -1;
					double value2 = Utilities.ConvertToDouble(presetField.Value.ToString(), decimals);
					ExtensionField.ExtensionFieldTypeEnum fieldType = presetField.FieldType;
					if (fieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension && preset.ScaleSystemType != unitScale.ScaleSystemType)
					{
						value2 = ((unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(value2) : UnitScale.FromFeetToMeters(value2));
					}
					this.SetPresetFieldProperty(num, this.FormatField(value2.ToString(), presetField.FieldType));
					num++;
				}
			}
			num = 0;
			foreach (object obj3 in preset.Results.Collection)
			{
				PresetResult presetResult = (PresetResult)obj3;
				if (num == 50)
				{
					break;
				}
				double num2 = Utilities.ConvertToDouble(presetResult.Result.ToString(), -1);
				string text = presetResult.Caption;
				string unit = presetResult.Unit;
				string value3 = presetResult.Result.ToString();
				ExtensionResult.ExtensionResultTypeEnum resultType = presetResult.ResultType;
				Console.WriteLine(presetResult.Caption + " == " + num2);
				DBEstimatingItem dbestimatingItem = null;
				if (presetResult.ItemID != -1)
				{
					dbestimatingItem = this.drawArea.Project.DBManagement.GetEstimatingItem(presetResult.ItemID);
				}
				bool flag = false;
				if (dbestimatingItem != null)
				{
					flag = dbestimatingItem.MatchResultType(presetResult.ResultType);
				}
				if (flag)
				{
					UnitScale unitScale2 = new UnitScale(1f, (dbestimatingItem.PurchaseUnit == "m" || dbestimatingItem.PurchaseUnit == "m²" || dbestimatingItem.PurchaseUnit == "m³") ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial, unitScale.Precision, false);
					UnitScale.UnitSystem scaleSystemType = unitScale2.ScaleSystemType;
					text = dbestimatingItem.Description;
					unit = dbestimatingItem.PurchaseUnit;
					switch (presetResult.ResultType)
					{
					case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
						num2 = ((unitScale.ScaleSystemType == scaleSystemType) ? num2 : ((scaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num2) : UnitScale.FromFeetToMeters(num2)));
						value3 = unitScale2.ToLengthStringFromUnitSystem(num2, false, true, true);
						unit = "";
						break;
					case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
						num2 = ((unitScale.ScaleSystemType == scaleSystemType) ? num2 : ((scaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(num2) : UnitScale.FromSquareFeetToSquareMeters(num2)));
						value3 = unitScale2.ToAreaStringFromUnitSystem(num2, true);
						unit = "";
						break;
					case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
						num2 = ((unitScale.ScaleSystemType == scaleSystemType) ? num2 : ((scaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromCubicMetersToCubicFeet(num2) : UnitScale.FromCubicFeetToCubicMeters(num2)));
						value3 = unitScale2.ToCubicStringFromUnitSystem(num2, true);
						unit = "";
						break;
					}
					resultType = ExtensionResult.ExtensionResultTypeEnum.ResultTypeCustom;
				}
				if (presetResult.ConditionMet)
				{
					if (!updateResultsOnly)
					{
						this.editableProperties.Add(text);
					}
					this.SetGridPropertyDisplayName("PresetResult" + (num + 1).ToString(), text);
					this.SetPresetResult(num, value3, unit, resultType);
					if (updateResultsOnly)
					{
						this.propertyGrid.UpdatePropertyValue("PresetResult" + (num + 1).ToString());
					}
					num++;
				}
			}
		}

		private void AddIgnoredProperty(string property)
		{
			string[] ignoredProperties = this.propertyGrid.IgnoredProperties;
			Array.Resize<string>(ref ignoredProperties, this.propertyGrid.IgnoredProperties.Length + 1);
			this.propertyGrid.IgnoredProperties = ignoredProperties;
			this.propertyGrid.IgnoredProperties[this.propertyGrid.IgnoredProperties.Length - 1] = property;
		}

		private void UpdateDefaultProperties(DrawObject drawObject, GroupStats groupStats)
		{
			this.editableProperties.Clear();
			if (this.drawArea.ActivePlan == null)
			{
				return;
			}
			string objectType;
			switch (objectType = drawObject.ObjectType)
			{
			case "Line":
				this.propertyGrid.IgnoredProperties = new string[]
				{
					"FontSize",
					"Text",
					"FillColor",
					"Pattern",
					"Shape",
					"CounterSize",
					"MaxRows",
					"Area",
					"AreaMinusDeduction",
					"Deduction",
					"DeductionsCount",
					"DropLength",
					"NetLength",
					"DropsCount",
					"PerimeterPlusDeduction",
					"CornersCount",
					"EndsCount",
					"Perimeter",
					"SegmentsCount",
					"TeesCount",
					"Openings",
					"OpeningsCount",
					"PerimeterMinusOpening",
					"Angle"
				};
				this.editableProperties.AddRange(new string[]
				{
					Resources.Nom,
					Resources.Couleur,
					Resources.Facteur_de_pente,
					Resources.Commentaire,
					Resources.Longueur
				});
				this.properties.Name = drawObject.Name;
				this.properties.Comment = drawObject.Comment;
				this.properties.Color = drawObject.Color;
				this.properties.PenWidth = drawObject.PenWidth;
				this.properties.SlopeFactor = drawObject.SlopeFactor;
				this.properties.ShowMeasure = drawObject.ShowMeasure;
				this.properties.Visible = drawObject.Visible;
				if (GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
				{
					this.properties.Label = drawObject.Label;
					this.editableProperties.Add(Resources.Étiquette);
				}
				else
				{
					this.AddIgnoredProperty("Label");
				}
				this.properties.GroupCount = this.drawArea.ToUnitString(groupStats.GroupCount);
				this.properties.Length = this.drawArea.ToLengthStringFromUnitSystem(groupStats.Perimeter, false);
				return;
			case "Rectangle":
				this.propertyGrid.IgnoredProperties = new string[]
				{
					"FontSize",
					"Label",
					"Text",
					"MaxRows",
					"GroupCount",
					"FillColor",
					"Pattern",
					"Shape",
					"PenWidth",
					"SlopeFactor",
					"ShowMeasure",
					"CounterSize",
					"Area",
					"AreaMinusDeduction",
					"Deduction",
					"DeductionsCount",
					"Perimeter",
					"DropLength",
					"NetLength",
					"DropsCount",
					"PerimeterPlusDeduction",
					"CornersCount",
					"EndsCount",
					"Length",
					"SegmentsCount",
					"TeesCount",
					"Openings",
					"OpeningsCount",
					"PerimeterMinusOpening",
					"Angle"
				};
				this.editableProperties.AddRange(new string[]
				{
					Resources.Nom,
					Resources.Couleur,
					Resources.Commentaire
				});
				this.properties.Name = drawObject.Name;
				this.properties.Comment = drawObject.Comment;
				this.properties.Color = drawObject.Color;
				this.properties.Visible = drawObject.Visible;
				return;
			case "Angle":
				this.propertyGrid.IgnoredProperties = new string[]
				{
					"FontSize",
					"Label",
					"Text",
					"MaxRows",
					"GroupCount",
					"FillColor",
					"Pattern",
					"Shape",
					"SlopeFactor",
					"CounterSize",
					"Area",
					"AreaMinusDeduction",
					"Deduction",
					"DeductionsCount",
					"Perimeter",
					"DropLength",
					"NetLength",
					"DropsCount",
					"PerimeterPlusDeduction",
					"CornersCount",
					"EndsCount",
					"Length",
					"SegmentsCount",
					"TeesCount",
					"Openings",
					"OpeningsCount",
					"PerimeterMinusOpening"
				};
				this.editableProperties.AddRange(new string[]
				{
					Resources.Nom,
					Resources.Couleur,
					Resources.Commentaire
				});
				this.properties.Name = drawObject.Name;
				this.properties.Comment = drawObject.Comment;
				this.properties.Color = drawObject.Color;
				this.properties.PenWidth = drawObject.PenWidth;
				this.properties.ShowMeasure = drawObject.ShowMeasure;
				this.properties.Visible = drawObject.Visible;
				this.properties.Angle = this.drawArea.ToAngleString(((DrawAngle)drawObject).Angle, ((DrawAngle)drawObject).AngleType);
				return;
			case "Area":
				if (!((DrawPolyLine)drawObject).EditDeductions)
				{
					string[] ignoredProperties = new string[]
					{
						"FontSize",
						"Text",
						"MaxRows",
						"FillColor",
						"PenWidth",
						"Shape",
						"CounterSize",
						"CornersCount",
						"EndsCount",
						"Length",
						"SegmentsCount",
						"TeesCount",
						"Openings",
						"OpeningsCount",
						"DropLength",
						"NetLength",
						"DropsCount",
						"PerimeterMinusOpening",
						"Angle"
					};
					if (groupStats.Area - groupStats.AreaMinusDeduction == 0.0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "AreaMinusDeduction");
					}
					if (groupStats.DeductionArea == 0.0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "Deduction");
					}
					if (groupStats.DeductionsCount == 0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "DeductionsCount");
					}
					if (groupStats.Perimeter - groupStats.PerimeterPlusDeduction == 0.0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "PerimeterPlusDeduction");
					}
					this.propertyGrid.IgnoredProperties = ignoredProperties;
					this.editableProperties.AddRange(new string[]
					{
						Resources.Nom,
						Resources.Étiquette,
						Resources.Couleur,
						Resources.Motif,
						Resources.Facteur_de_pente,
						Resources.Commentaire,
						Resources.Surface,
						Resources.Déduction,
						Resources.Surface_déductions,
						Resources.Périmètre,
						Resources.Périmètre_déductions,
						Resources.Nombre_de_déductions
					});
					this.properties.Name = drawObject.Name;
					this.properties.Comment = drawObject.Comment;
					this.properties.Color = drawObject.Color;
					this.properties.Pattern = ((DrawPolyLine)drawObject).Pattern;
					this.properties.SlopeFactor = drawObject.SlopeFactor;
					this.properties.ShowMeasure = drawObject.ShowMeasure;
					this.properties.Visible = drawObject.Visible;
					if (GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
					{
						this.properties.Label = drawObject.Label;
						this.editableProperties.Add(Resources.Étiquette);
					}
					else
					{
						this.AddIgnoredProperty("Label");
					}
				}
				else
				{
					string[] ignoredProperties = new string[]
					{
						"Name",
						"FontSize",
						"Label",
						"Comment",
						"Color",
						"Pattern",
						"Shape",
						"SlopeFactor",
						"ShowMeasure",
						"Visible",
						"Text",
						"CounterSize",
						"GroupCount",
						"FillColor",
						"PenWidth",
						"CornersCount",
						"EndsCount",
						"Length",
						"SegmentsCount",
						"TeesCount",
						"Openings",
						"OpeningsCount",
						"DropLength",
						"DropsCount",
						"PerimeterMinusOpening",
						"Angle"
					};
					if (groupStats.Area - groupStats.AreaMinusDeduction == 0.0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "AreaMinusDeduction");
					}
					if (groupStats.DeductionArea == 0.0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "Deduction");
					}
					if (groupStats.DeductionsCount == 0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "DeductionsCount");
					}
					if (groupStats.Perimeter - groupStats.PerimeterPlusDeduction == 0.0)
					{
						Utilities.AddToStringArray(ref ignoredProperties, "PerimeterPlusDeduction");
					}
					this.propertyGrid.IgnoredProperties = ignoredProperties;
					this.editableProperties.AddRange(new string[]
					{
						Resources.Surface,
						Resources.Déduction,
						Resources.Surface_déductions,
						Resources.Périmètre,
						Resources.Périmètre_déductions,
						Resources.Nombre_de_déductions
					});
				}
				this.properties.GroupCount = this.drawArea.ToUnitString(groupStats.GroupCount);
				this.properties.Area = this.drawArea.ToAreaStringFromUnitSystem(groupStats.Area);
				this.properties.Perimeter = this.drawArea.ToLengthStringFromUnitSystem(groupStats.Perimeter, false);
				this.properties.AreaMinusDeduction = this.drawArea.ToAreaStringFromUnitSystem(groupStats.AreaMinusDeduction);
				this.properties.Deduction = this.drawArea.ToAreaStringFromUnitSystem(groupStats.DeductionArea);
				this.properties.DeductionsCount = this.drawArea.ToUnitString(groupStats.DeductionsCount);
				this.properties.PerimeterPlusDeduction = this.drawArea.ToLengthStringFromUnitSystem(groupStats.PerimeterPlusDeduction, false);
				return;
			case "Perimeter":
			{
				string[] ignoredProperties = new string[]
				{
					"FontSize",
					"Text",
					"MaxRows",
					"FillColor",
					"Pattern",
					"Shape",
					"CounterSize",
					"Area",
					"AreaMinusDeduction",
					"Deduction",
					"DeductionsCount",
					"Perimeter",
					"PerimeterPlusDeduction",
					"TeesCount",
					"Angle"
				};
				if (groupStats.DropLength == 0.0)
				{
					Utilities.AddToStringArray(ref ignoredProperties, "DropLength");
					Utilities.AddToStringArray(ref ignoredProperties, "NetLength");
				}
				if (groupStats.DeductionPerimeter == 0.0)
				{
					Utilities.AddToStringArray(ref ignoredProperties, "Openings");
				}
				if (groupStats.Perimeter - groupStats.PerimeterMinusOpening == 0.0)
				{
					Utilities.AddToStringArray(ref ignoredProperties, "PerimeterMinusOpening");
				}
				if (groupStats.DropsCount == 0)
				{
					Utilities.AddToStringArray(ref ignoredProperties, "DropsCount");
				}
				if (groupStats.DeductionsCount == 0)
				{
					Utilities.AddToStringArray(ref ignoredProperties, "OpeningsCount");
				}
				if (groupStats.CornersCount == 0)
				{
					Utilities.AddToStringArray(ref ignoredProperties, "CornersCount");
				}
				if (groupStats.EndsCount == 0)
				{
					Utilities.AddToStringArray(ref ignoredProperties, "EndsCount");
				}
				this.propertyGrid.IgnoredProperties = ignoredProperties;
				this.editableProperties.AddRange(new string[]
				{
					Resources.Nom,
					Resources.Étiquette,
					Resources.Couleur,
					Resources.Facteur_de_pente,
					Resources.Commentaire,
					Resources.Longueur,
					Resources.Drop_length,
					Resources.Longueur_nette,
					Resources.Drops_count,
					Resources.Longueur_des_ouvertures,
					Resources.Longueur_sans_ouvertures,
					Resources.Nombre_d_ouvertures,
					Resources.Nombre_de_coins,
					Resources.Nombre_de_bouts,
					Resources.Nombre_de_segments
				});
				this.properties.Name = drawObject.Name;
				this.properties.Comment = drawObject.Comment;
				this.properties.Color = drawObject.Color;
				this.properties.PenWidth = drawObject.PenWidth;
				this.properties.SlopeFactor = drawObject.SlopeFactor;
				this.properties.ShowMeasure = drawObject.ShowMeasure;
				this.properties.Visible = drawObject.Visible;
				if (GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
				{
					this.properties.Label = drawObject.Label;
					this.editableProperties.Add(Resources.Étiquette);
				}
				else
				{
					this.AddIgnoredProperty("Label");
				}
				this.properties.GroupCount = this.drawArea.ToUnitString(groupStats.GroupCount);
				this.properties.Length = this.drawArea.ToLengthStringFromUnitSystem(groupStats.Perimeter, false);
				this.properties.Openings = this.drawArea.ToLengthStringFromUnitSystem(groupStats.DeductionPerimeter, false);
				this.properties.PerimeterMinusOpening = this.drawArea.ToLengthStringFromUnitSystem(groupStats.PerimeterMinusOpening, false);
				this.properties.DropLength = this.drawArea.ToLengthStringFromUnitSystem(groupStats.DropLength, false);
				this.properties.NetLength = this.drawArea.ToLengthStringFromUnitSystem(groupStats.NetLength, false);
				this.properties.DropsCount = this.drawArea.ToUnitString(groupStats.DropsCount);
				this.properties.OpeningsCount = this.drawArea.ToUnitString(groupStats.DeductionsCount);
				this.properties.CornersCount = this.drawArea.ToUnitString(groupStats.CornersCount);
				this.properties.EndsCount = this.drawArea.ToUnitString(groupStats.EndsCount);
				this.properties.SegmentsCount = this.drawArea.ToUnitString(groupStats.SegmentsCount);
				return;
			}
			case "Counter":
				this.propertyGrid.IgnoredProperties = new string[]
				{
					"FontSize",
					"MaxRows",
					"FillColor",
					"Pattern",
					"PenWidth",
					"SlopeFactor",
					"ShowMeasure",
					"Area",
					"AreaMinusDeduction",
					"Deduction",
					"DeductionsCount",
					"Perimeter",
					"DropLength",
					"NetLength",
					"DropsCount",
					"PerimeterPlusDeduction",
					"CornersCount",
					"EndsCount",
					"Length",
					"SegmentsCount",
					"TeesCount",
					"Openings",
					"OpeningsCount",
					"PerimeterMinusOpening",
					"Angle"
				};
				this.editableProperties.AddRange(new string[]
				{
					Resources.Nom,
					Resources.Étiquette,
					Resources.Texte,
					Resources.Couleur,
					Resources.Forme,
					Resources.Commentaire,
					Resources.Nombre_d_objets
				});
				this.properties.Name = drawObject.Name;
				this.properties.Comment = drawObject.Comment;
				this.properties.Text = ((DrawCounter)drawObject).Text;
				this.properties.Color = drawObject.Color;
				this.properties.Shape = ((DrawCounter)drawObject).Shape;
				this.properties.CounterSize = ((DrawCounter)drawObject).DefaultSize;
				this.properties.Visible = drawObject.Visible;
				if (GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
				{
					this.properties.Label = drawObject.Label;
					this.editableProperties.Add(Resources.Étiquette);
				}
				else
				{
					this.AddIgnoredProperty("Label");
				}
				this.properties.GroupCount = this.drawArea.ToUnitString(groupStats.GroupCount);
				return;
			case "Note":
				this.propertyGrid.IgnoredProperties = new string[]
				{
					"Label",
					"Text",
					"CounterSize",
					"MaxRows",
					"GroupCount",
					"FillColor",
					"Pattern",
					"Shape",
					"SlopeFactor",
					"ShowMeasure",
					"Area",
					"AreaMinusDeduction",
					"Deduction",
					"DeductionsCount",
					"Perimeter",
					"DropLength",
					"NetLength",
					"DropsCount",
					"PerimeterPlusDeduction",
					"CornersCount",
					"EndsCount",
					"Length",
					"SegmentsCount",
					"TeesCount",
					"Openings",
					"OpeningsCount",
					"PerimeterMinusOpening",
					"Angle"
				};
				this.editableProperties.AddRange(new string[]
				{
					Resources.Nom,
					Resources.Couleur,
					Resources.Commentaire
				});
				this.properties.Name = drawObject.Name;
				this.properties.Comment = drawObject.Comment;
				this.properties.Color = drawObject.Color;
				this.properties.PenWidth = drawObject.PenWidth;
				this.properties.FontSize = ((DrawNote)drawObject).FontSize;
				this.properties.Visible = drawObject.Visible;
				return;
			case "Legend":
				this.propertyGrid.IgnoredProperties = new string[]
				{
					"Name",
					"Label",
					"Text",
					"Color",
					"Comment",
					"CounterSize",
					"GroupCount",
					"FillColor",
					"Pattern",
					"Shape",
					"SlopeFactor",
					"Area",
					"AreaMinusDeduction",
					"Deduction",
					"DeductionsCount",
					"Perimeter",
					"DropLength",
					"NetLength",
					"DropsCount",
					"PerimeterPlusDeduction",
					"CornersCount",
					"EndsCount",
					"Length",
					"SegmentsCount",
					"TeesCount",
					"Openings",
					"OpeningsCount",
					"PerimeterMinusOpening",
					"Angle"
				};
				this.editableProperties.AddRange(new string[0]);
				this.properties.FontSize = ((DrawLegend)drawObject).FontSize;
				this.properties.PenWidth = drawObject.PenWidth;
				this.properties.MaxRows = ((DrawLegend)drawObject).MaxRows;
				this.properties.ShowMeasure = drawObject.ShowMeasure;
				this.properties.Visible = drawObject.Visible;
				break;

				return;
			}
		}

		private void RefreshProperties()
		{
			if (this.selectedObject == null)
			{
				this.DisableEditor();
				return;
			}
			if (this.selectedObject.DeductionParentID != -1)
			{
				this.selectedObject = this.drawArea.ActiveDrawingObjects.GetObjectByID(this.selectedObject.DeductionParentID);
				if (this.selectedObject == null)
				{
					this.DisableEditor();
					return;
				}
			}
			Plan activePlan = this.drawArea.ActivePlan;
			if (activePlan == null)
			{
				return;
			}
			this.exitNow = true;
			((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForThisPlan"]).Checked = !this.project.DisplayResultsForAllPlans;
			((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForAllPlans"]).Checked = this.project.DisplayResultsForAllPlans;
			this.exitNow = false;
			this.RefreshSelectedGroup();
			Preset selectedPreset = this.GetSelectedPreset(true);
			GroupStats groupStats;
			if (!this.project.DisplayResultsForAllPlans)
			{
				groupStats = GroupUtilities.ComputeGroupStats(activePlan, this.selectedObject, activePlan.UnitScale.ScaleSystemType, true, "");
			}
			else
			{
				groupStats = GroupUtilities.ComputeGroupStats(this.project, this.selectedObject, null, activePlan.UnitScale.ScaleSystemType, true, "");
			}
			bool flag = false;
			if (this.lastRefreshedObject != null)
			{
				flag = (this.selectedObject.IsPartOfGroup() ? this.selectedObject.HasSameGroupOrID(this.lastRefreshedObject) : (this.selectedObject.ID == this.lastRefreshedObject.ID));
			}
			if (!flag)
			{
				this.propertyGrid.IgnoredCategories = new string[]
				{
					"PresetProperties",
					"PresetCustomRendering",
					"PresetResults",
					"Properties",
					"SelectedObjectsProperties",
					"Values"
				};
			}
			this.lastRefreshedObject = this.selectedObject;
			if (selectedPreset != null)
			{
				this.extensionSupport.QueryPresetResults(selectedPreset, groupStats, activePlan.UnitScale);
				this.UpdatePresetProperties(selectedPreset, activePlan.UnitScale, false);
			}
			else
			{
				this.UpdateDefaultProperties(this.selectedObject, groupStats);
			}
			eColorSchemeStyle colorSchemeStyle = eColorSchemeStyle.Office2007;
			this.propertyGrid.PropertyTree.ColorSchemeStyle = colorSchemeStyle;
			this.propertyGrid.PropertyTree.SelectionBoxStyle = eSelectionStyle.HighlightCells;
			this.propertyGrid.PropertyTree.HotTracking = true;
			this.propertyGrid.PropertyTree.Font = Utilities.GetDefaultFont();
			this.propertyGrid.PropertyTree.Zoom = 1.000001f;
			this.propertyGrid.IgnoredCategories = ((selectedPreset == null) ? new string[]
			{
				"PresetProperties",
				"PresetCustomRendering",
				"PresetResults"
			} : new string[]
			{
				"Properties",
				"SelectedObjectsProperties",
				"Values"
			});
			this.propertyGrid.RefreshProperties();
			if (this.propertyGrid.PropertyTree.Nodes.Count > 0)
			{
				this.propertyGrid.PropertyTree.SelectedIndex = 0;
			}
		}

		private void grid_ValidatePropertyValue(object sender, ValidatePropertyValueEventArgs e)
		{
			this.ValidatePropertyValue(e);
		}

		private void grid_PropertyValueChanging(object sender, PropertyValueChangingEventArgs e)
		{
			if (e.NewValue == null)
			{
				e.Handled = true;
				return;
			}
			string propertyName;
			if ((propertyName = e.PropertyName) != null)
			{
				if (!(propertyName == "Color"))
				{
					return;
				}
				if ((Color)e.NewValue == Color.Empty)
				{
					e.Handled = true;
				}
			}
		}

		private void grid_ConvertFromStringToPropertyValue(object sender, ConvertValueEventArgs e)
		{
			if (e.StringValue == null)
			{
				return;
			}
			string propertyName;
			if ((propertyName = e.PropertyName) != null)
			{
				if (!(propertyName == "Color"))
				{
					if (propertyName == "Pattern")
					{
						string stringValue = e.StringValue;
						HatchStylePickerCombo.HatchStylePickerEnum hatchStylePickerEnum = HatchStylePickerCombo.StringToPattern(stringValue);
						e.TypedValue = hatchStylePickerEnum;
						e.IsConverted = true;
						return;
					}
					if (propertyName == "Shape")
					{
						string stringValue2 = e.StringValue;
						DrawCounter.CounterShapeTypeEnum counterShapeTypeEnum = DrawCounter.StringToShape(stringValue2);
						e.TypedValue = counterShapeTypeEnum;
						e.IsConverted = true;
						return;
					}
				}
				else
				{
					Color color = Color.Empty;
					string stringValue3 = e.StringValue;
					if (ColorPicker.ColorNameExists(stringValue3))
					{
						color = ColorPicker.StringToColor(stringValue3);
					}
					string[] array = stringValue3.Split(new string[]
					{
						",",
						" ",
						"\t"
					}, StringSplitOptions.RemoveEmptyEntries);
					if (array.GetLength(0) == 3)
					{
						try
						{
							color = Color.FromArgb(Utilities.ConvertToInt(array.GetValue(0)), Utilities.ConvertToInt(array.GetValue(1)), Utilities.ConvertToInt(array.GetValue(2)));
						}
						catch
						{
							color = Color.Empty;
						}
					}
					if (color == Color.Empty)
					{
						try
						{
							if (Color.FromName(stringValue3).ToArgb() != Color.Empty.ToArgb())
							{
								color = this.drawArea.GetNextColor(this.selectedObject.ObjectType);
							}
						}
						catch
						{
						}
					}
					if (color != Color.Empty)
					{
						e.TypedValue = color;
						e.IsConverted = true;
						return;
					}
					return;
				}
			}
			if (e.PropertyName.IndexOf("PresetField") != -1)
			{
				int num = Utilities.ConvertToInt(e.PropertyName.Substring(11, e.PropertyName.Length - 11));
				PresetField presetField = this.propertyFields[num - 1];
				if (presetField != null)
				{
					switch (presetField.FieldType)
					{
					case ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension:
					{
						UnitScale.UnitSystem unitSystem = UnitScale.UnitSystem.undefined;
						double num2 = UnitScale.ConvertDimensionToValue(e.StringValue, this.drawArea.UnitScale.CurrentSystemType, ref unitSystem);
						if (unitSystem != this.drawArea.UnitScale.ScaleSystemType)
						{
							switch (unitSystem)
							{
							case UnitScale.UnitSystem.metric:
								num2 = UnitScale.FromMetersToFeet(num2);
								break;
							case UnitScale.UnitSystem.imperial:
								num2 = UnitScale.FromFeetToMeters(num2);
								break;
							}
						}
						e.TypedValue = this.FormatField(num2.ToString(), presetField.FieldType);
						Preset selectedPreset = this.GetSelectedPreset(false);
						if (selectedPreset != null && selectedPreset.ScaleSystemType != this.drawArea.UnitScale.ScaleSystemType)
						{
							num2 = ((selectedPreset.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num2) : UnitScale.FromFeetToMeters(num2));
						}
						presetField.Value = num2;
						break;
					}
					case ExtensionField.ExtensionFieldTypeEnum.FieldTypeInteger:
						e.TypedValue = Utilities.ConvertToInt(e.StringValue);
						presetField.Value = e.TypedValue;
						break;
					case ExtensionField.ExtensionFieldTypeEnum.FieldTypeDouble:
					case ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency:
						e.TypedValue = Utilities.ConvertToDouble(e.StringValue, -1);
						presetField.Value = e.TypedValue;
						break;
					}
					e.IsConverted = true;
				}
			}
		}

		private void grid_ConvertPropertyValueToString(object sender, ConvertValueEventArgs e)
		{
			if (e.TypedValue == null)
			{
				e.StringValue = "";
				e.IsConverted = true;
				return;
			}
			string propertyName;
			switch (propertyName = e.PropertyName)
			{
			case "Color":
			{
				Color color = (Color)e.TypedValue;
				e.StringValue = ColorPicker.ColorToString(color);
				e.IsConverted = true;
				return;
			}
			case "Pattern":
			{
				HatchStylePickerCombo.HatchStylePickerEnum pattern = (HatchStylePickerCombo.HatchStylePickerEnum)e.TypedValue;
				e.StringValue = HatchStylePickerCombo.PatternToString(pattern);
				e.IsConverted = true;
				return;
			}
			case "Shape":
			{
				DrawCounter.CounterShapeTypeEnum shape = (DrawCounter.CounterShapeTypeEnum)e.TypedValue;
				e.StringValue = DrawCounter.ShapeToString(shape);
				e.IsConverted = true;
				return;
			}
			case "ShowMeasure":
				e.StringValue = (((bool)e.TypedValue) ? Resources.Oui : Resources.Non);
				e.IsConverted = true;
				return;
			case "Visible":
				e.StringValue = (((bool)e.TypedValue) ? Resources.Visible : Resources.Invisible);
				e.IsConverted = true;
				return;
			case "IncludeAllPlans":
				e.StringValue = (((bool)e.TypedValue) ? Resources.Oui : Resources.Non);
				e.IsConverted = true;
				break;

				return;
			}
		}

		private void propertyGrid_ProvidePropertyValueList(object sender, PropertyValueListEventArgs e)
		{
			string propertyName;
			if ((propertyName = e.PropertyName) != null)
			{
				if (propertyName == "Pattern")
				{
					e.ValueList = new List<string>(new string[]
					{
						Resources.Solide,
						Resources.Horizontal,
						Resources.Vertical,
						Resources.ForwardDiagonal,
						Resources.BackwardDiagonal,
						Resources.DiagonalCross,
						Resources.Percent05,
						Resources.Percent10,
						Resources.Percent20,
						Resources.Percent25,
						Resources.Percent30,
						Resources.Percent40,
						Resources.Percent50,
						Resources.Percent60,
						Resources.Percent70,
						Resources.Percent75,
						Resources.Percent80,
						Resources.Percent90,
						Resources.LightDownwardDiagonal,
						Resources.LightUpwardDiagonal,
						Resources.DarkDownwardDiagonal,
						Resources.DarkUpwardDiagonal,
						Resources.WideDownwardDiagonal,
						Resources.WideUpwardDiagonal,
						Resources.LightVertical,
						Resources.LightHorizontal,
						Resources.NarrowVertical,
						Resources.NarrowHorizontal,
						Resources.DarkVertical,
						Resources.DarkHorizontal,
						Resources.DashedDownwardDiagonal,
						Resources.DashedUpwardDiagonal,
						Resources.DashedHorizontal,
						Resources.DashedVertical,
						Resources.SmallConfetti,
						Resources.LargeConfetti,
						Resources.ZigZag,
						Resources.Wave,
						Resources.DiagonalBrick,
						Resources.HorizontalBrick,
						Resources.Weave,
						Resources.Plaid,
						Resources.Divot,
						Resources.DottedGrid,
						Resources.DottedDiamond,
						Resources.Shingle,
						Resources.Trellis,
						Resources.Sphere,
						Resources.SmallGrid,
						Resources.SmallCheckerBoard,
						Resources.LargeCheckerBoard,
						Resources.OutlinedDiamond,
						Resources.SolidDiamond
					});
					e.IsListValid = true;
					return;
				}
				if (!(propertyName == "Shape"))
				{
					return;
				}
				e.ValueList = new List<string>(new string[]
				{
					Resources.Cercle,
					Resources.Carré,
					Resources.Losange,
					Resources.Triangle,
					Resources.Triangle_inversé,
					Resources.Trapèze,
					Resources.Trapèze_inversé
				});
				e.IsListValid = true;
			}
		}

		private void grid_Localize(object sender, AdvPropertyGridLocalizeEventArgs e)
		{
			ePropertyGridLocalizationType localizationType = e.LocalizationType;
			if (localizationType != ePropertyGridLocalizationType.Category)
			{
				return;
			}
			string key;
			if ((key = e.Key) != null)
			{
				if (key == "Properties")
				{
					e.LocalizedValue = (GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, this.drawArea.SelectedObject) ? Resources.Propriétés_du_groupe : Resources.Propriétés);
					return;
				}
				if (key == "SelectedObjectsProperties" || key == "PresetCustomRendering")
				{
					e.LocalizedValue = Resources.Propriétés_de_la_sélection;
					return;
				}
				if (key == "PresetProperties")
				{
					e.LocalizedValue = Resources.Propriétés_de_l_extension;
					return;
				}
				if (key == "Values")
				{
					e.LocalizedValue = (GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, this.drawArea.SelectedObject) ? Resources.Résultats_du_groupe : Resources.Résultats);
					return;
				}
				if (!(key == "PresetResults"))
				{
					return;
				}
				e.LocalizedValue = Resources.Résultats_de_l_extension;
			}
		}

		private void propertyTree_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.C)
			{
				return;
			}
			if (Control.ModifierKeys == Keys.Control)
			{
				e.Handled = true;
			}
		}

		private void DisplayCalculations_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.exitNow)
			{
				this.project.DisplayResultsForAllPlans = ((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForAllPlans"]).Checked;
				this.RefreshProperties();
				if (this.OnDisplayCalculationsForAllPlans != null)
				{
					this.OnDisplayCalculationsForAllPlans(this.selectedObject);
				}
			}
		}

		private void colorPickerEditor_OnColorHover(Color color)
		{
			if (this.selectedObject != null)
			{
				if (this.selectedObject.GroupID > -1)
				{
					using (IEnumerator enumerator = this.drawArea.ActiveDrawingObjects.Collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DrawObject drawObject = (DrawObject)obj;
							if (drawObject.GroupID == this.selectedObject.GroupID)
							{
								drawObject.Color = color;
								drawObject.FillColor = color;
							}
						}
						goto IL_91;
					}
				}
				this.selectedObject.Color = color;
				this.selectedObject.FillColor = color;
				IL_91:
				this.drawArea.DrawingBoard.Invalidate();
			}
		}

		private void slopeFactorEditor_OnSave(SlopeFactor slopeFactor)
		{
			if (this.selectedObject != null)
			{
				this.UpdateProperty("SlopeFactor", slopeFactor);
			}
			this.Refresh();
		}

		private void ClearSuperTab()
		{
			this.exitNow = true;
			this.superTabProperties.SelectedTabIndex = 0;
			this.exitNow = false;
			if (this.superTabProperties.Tabs.Count > 1)
			{
				for (int i = this.superTabProperties.Tabs.Count - 1; i >= 1; i--)
				{
					this.RemoveSuperTab(i);
				}
				this.superTabProperties.RecalcLayout();
			}
		}

		private int LocatePresetSuperTab(Preset preset)
		{
			int result = 0;
			for (int i = 1; i < this.superTabProperties.Tabs.Count; i++)
			{
				if (preset.Equals((Preset)((SuperTabItem)this.superTabProperties.Tabs[i]).Tag))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		private string GetSuperTabCaption(Preset preset)
		{
			string[] words = Utilities.GetWords(preset.DisplayName);
			int num = 0;
			string text = "";
			foreach (string str in words)
			{
				string str2 = (num == 0) ? "" : " ";
				if (num > -1)
				{
					if ((double)num == Math.Floor((double)(words.Length / 2)) && num > 0)
					{
						str2 = "\n";
						num = -1;
					}
					else
					{
						num++;
					}
				}
				text = text + str2 + str;
			}
			return text;
		}

		private void CreateSuperTab(Preset preset)
		{
			SuperTabItem superTabItem = this.superTabProperties.CreateTab(this.GetSuperTabCaption(preset));
			superTabItem.Tag = preset;
			superTabItem.TextAlignment = new eItemAlignment?(eItemAlignment.Center);
		}

		private void RenameSuperTab(Preset preset)
		{
			try
			{
				int num = this.LocatePresetSuperTab(preset);
				if (num > 0)
				{
					SuperTabItem superTabItem = (SuperTabItem)this.superTabProperties.Tabs[num];
					superTabItem.Text = this.GetSuperTabCaption(preset);
				}
			}
			catch
			{
			}
		}

		private void RemoveSuperTab(int index)
		{
			int selectedTabIndex = this.superTabProperties.SelectedTabIndex;
			try
			{
				SuperTabItem superTabItem = (SuperTabItem)this.superTabProperties.Tabs[index];
				superTabItem.Tag = null;
				this.superTabProperties.Tabs.Remove(superTabItem);
				this.superTabProperties.Controls.Remove(superTabItem.AttachedControl);
			}
			catch
			{
			}
			if (selectedTabIndex == index)
			{
				this.superTabProperties.SelectedTabIndex = 0;
			}
		}

		private void RefreshSuperTab()
		{
			this.ClearSuperTab();
			foreach (object obj in this.extensionsManager.Group.Presets.Collection)
			{
				Preset preset = (Preset)obj;
				this.CreateSuperTab(preset);
			}
			this.superTabProperties.RecalcLayout();
			this.superTabProperties.SelectedTabIndex = 0;
			this.superTabProperties.TabsVisible = true;
		}

		private void tabProperties_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
		{
			switch (this.tabProperties.SelectedTabIndex)
			{
			case 0:
				this.RefreshProperties();
				Utilities.SetObjectFocus(this.propertyGrid.PropertyTree);
				return;
			case 1:
				Utilities.SetObjectFocus(this.extensionsManager);
				return;
			case 2:
				if (this.coInterface.IsReady)
				{
					Utilities.SetObjectFocus(this.coControl);
				}
				return;
			default:
				return;
			}
		}

		private void superTabProperties_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
		{
			this.propertyGrid.Parent = this.superTabProperties.SelectedTab.AttachedControl;
			if (this.exitNow)
			{
				return;
			}
			if (this.selectedObject != null)
			{
				Preset preset = null;
				if (this.superTabProperties.SelectedTabIndex > 0)
				{
					preset = (Preset)this.superTabProperties.SelectedTab.Tag;
					if (preset != null)
					{
						this.drawArea.SetGroupSelectedPreset(this.selectedObject, preset);
					}
				}
				this.drawArea.SetGroupSelectedPreset(this.selectedObject, preset);
			}
			this.lastRefreshedObject = null;
			this.RefreshProperties();
			Utilities.SetObjectFocus(this.propertyGrid.PropertyTree);
		}

		private void extensionsManager_OnPresetCreated(Preset preset)
		{
			this.CreateSuperTab(preset);
			this.superTabProperties.TabsVisible = true;
			this.superTabProperties.RecalcLayout();
			this.drawArea.Owner.SetModified();
			this.drawArea.Refresh();
			if (this.OnPresetCreated != null)
			{
				this.OnPresetCreated(preset);
			}
		}

		private void extensionsManager_OnPresetRenamed(Preset preset, string oldDisplayName, string newDisplayName)
		{
			if (this.coInterface.IsReady && this.selectedObject != null)
			{
				FormulaUtilities.RenameExtension(this.selectedObject.Group, oldDisplayName, newDisplayName);
			}
		}

		private void extensionsManager_OnPresetModified(Preset preset)
		{
			this.drawArea.Owner.SetModified();
			this.RenameSuperTab(preset);
			if (this.OnPresetModified != null)
			{
				this.OnPresetModified(preset);
			}
		}

		private void extensionsManager_OnPresetDeleted(Preset preset)
		{
			if (this.selectedObject != null && this.CustomRenderingEnabled(preset))
			{
				if (this.selectedObject.Group.SelectedRenderingPreset != null && this.selectedObject.Group.SelectedRenderingPreset.Equals(preset))
				{
					this.selectedObject.Group.SelectedRenderingPreset = null;
				}
				GroupUtilities.CleanUpCustomRendering(this.project, this.selectedObject.Group.ID, preset.ID);
			}
			int num = this.LocatePresetSuperTab(preset);
			if (num > 0)
			{
				this.RemoveSuperTab(num);
			}
		}

		private void extensionsManager_OnPresetAfterDeleted(object sender, EventArgs e)
		{
			this.drawArea.Owner.SetModified();
			this.drawArea.Refresh();
			if (this.OnPresetDeleted != null)
			{
				this.OnPresetDeleted(null);
			}
		}

		private OnObjectSelectedHandler OnObjectSelected;

		private OnObjectChangedHandler OnObjectChanged;

		private OnObjectSelectedHandler OnDisplayCalculationsForAllPlans;

		private PresetEventHandler OnPresetCreated;

		private PresetEventHandler OnPresetModified;

		private PresetEventHandler OnPresetDeleted;

		private ProductEventHandler OnProductCreated;

		private ProductEventHandler OnProductModified;

		private ProductEventHandler OnProductDeleted;

		private bool enabled;

		private bool exitNow;

		private Project project;

		private DrawingArea drawArea;

		private ExtensionsSupport extensionSupport;

		private DrawObject selectedObject;

		private DrawObject lastRefreshedObject;

		private DrawObjectProperties properties;

		private ComboBoxEx comboBox;

		private AdvPropertyGrid propertyGrid;

		private Bar barDisplayCalculations;

		private ExtensionsManager extensionsManager;

		private DevComponents.DotNetBar.TabControl tabProperties;

		private SuperTabControl superTabProperties;

		private List<string> editableProperties = new List<string>();

		private PresetField[] propertyFields = new PresetField[25];

		private UITypeEditorColorPicker colorPickerEditor;

		private UITypeEditorSlopeControl slopeFactorEditor;

		private CEstimatingsItemsControl estimatingItemsControl;

		private CEstimatingsItemsControl coControl;

		private COfficeInterface coInterface;
	}
}
