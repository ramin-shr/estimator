using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TabControl = DevComponents.DotNetBar.TabControl;

namespace QuoterPlan
{
    public class DrawObjectEditor
    {
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

        private TabControl tabProperties;

        private SuperTabControl superTabProperties;

        private List<string> editableProperties = new List<string>();

        private PresetField[] propertyFields = new PresetField[25];

        private UITypeEditorColorPicker colorPickerEditor;

        private UITypeEditorSlopeControl slopeFactorEditor;

        private CEstimatingsItemsControl estimatingItemsControl;

        private CEstimatingsItemsControl coControl;

        private COfficeInterface coInterface;

        private DrawObject ActiveObject
        {
            set
            {
                this.selectedObject = value;
                this.RefreshProperties();
            }
        }

        public int Count
        {
            get
            {
                return this.comboBox.Items.Count;
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
                bool flag;
                try
                {
                    string[] array = this.editableProperties.ToArray();
                    int num = 0;
                    while (num < (int)array.Length)
                    {
                        PropertyNode propertyNode = this.GetPropertyNode(array[num]);
                        if (propertyNode == null || !propertyNode.IsEditing)
                        {
                            num++;
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                    flag = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    flag = false;
                }
                return flag;
            }
        }

        public DrawObject this[int index]
        {
            get
            {
                DrawObject owner;
                try
                {
                    Utilities.DisplayName item = (Utilities.DisplayName)this.comboBox.Items[index];
                    owner = (DrawObject)item.Owner;
                }
                catch
                {
                    owner = null;
                }
                return owner;
            }
        }

        public DrawObject SelectedObject
        {
            get
            {
                return this.selectedObject;
            }
        }

        public DrawObjectEditor(Project project, DrawingArea drawArea, ExtensionsSupport extensionSupport, ComboBoxEx comboBox, AdvPropertyGrid propertyGrid, ExtensionsManager extensionsManager, TabControl tabProperties, SuperTabControl superTabProperties, Bar barDisplayCalculations, CEstimatingsItemsControl estimatingItemsControl, CEstimatingsItemsControl coControl, COfficeInterface coInterface)
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

        public void Add(DrawObject drawObject, bool selectObject)
        {
            int num = this.comboBox.Items.Add(drawObject.DisplayName);
            if (selectObject)
            {
                this.exitNow = true;
                this.comboBox.SelectedIndex = num;
                this.ActiveObject = drawObject;
                this.exitNow = false;
            }
        }

        private void AddIgnoredProperty(string property)
        {
            string[] ignoredProperties = this.propertyGrid.IgnoredProperties;
            Array.Resize<string>(ref ignoredProperties, (int)this.propertyGrid.IgnoredProperties.Length + 1);
            this.propertyGrid.IgnoredProperties = ignoredProperties;
            this.propertyGrid.IgnoredProperties[(int)this.propertyGrid.IgnoredProperties.Length - 1] = property;
        }

        private void ChoiceElementsUITypeEditorConvertFromStringToPropertyValue(object sender, ConvertValueEventArgs e)
        {
            string lower = (e.StringValue ?? "").ToLower();
            ExtensionChoice choice = ((UITypeEditorChoiceElements)((PropertySettings)sender).UITypeEditor).Choice;
            if (choice != null)
            {
                foreach (ExtensionChoiceElement collection in choice.Elements.Collection)
                {
                    if (collection.Caption.ToLower() != lower)
                    {
                        continue;
                    }
                    e.TypedValue = collection;
                    e.IsConverted = true;
                    return;
                }
            }
            e.IsConverted = false;
        }

        private void ChoiceElementsUITypeEditorConvertPropertyValueToString(object sender, ConvertValueEventArgs e)
        {
            if (e.TypedValue != null)
            {
                ExtensionChoiceElement typedValue = (ExtensionChoiceElement)e.TypedValue;
                e.StringValue = (typedValue == null ? "" : typedValue.Caption);
            }
            else
            {
                e.StringValue = "";
            }
            e.IsConverted = true;
        }

        public void Clear()
        {
            int count = this.comboBox.Items.Count - 1;
            while (count >= 0)
            {
                count--;
            }
            this.comboBox.Items.Clear();
            this.ActiveObject = null;
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
            this.ProductAdd(this.coInterface.BrowserInterface, this.coControl, group.COfficeProducts, 0);
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

        private void colorPickerEditor_OnColorHover(Color color)
        {
            if (this.selectedObject != null)
            {
                if (this.selectedObject.GroupID <= -1)
                {
                    this.selectedObject.Color = color;
                    this.selectedObject.FillColor = color;
                }
                else
                {
                    foreach (DrawObject collection in this.drawArea.ActiveDrawingObjects.Collection)
                    {
                        if (collection.GroupID != this.selectedObject.GroupID)
                        {
                            continue;
                        }
                        collection.Color = color;
                        collection.FillColor = color;
                    }
                }
                this.drawArea.DrawingBoard.Invalidate();
            }
        }

        private void combo_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                return;
            }
            e.DrawBackground();
            DrawObject item = this[e.Index];
            if (item == null)
            {
                return;
            }
            using (Image bitmap = new Bitmap(17, 17))
            {
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    this.drawArea.DrawObjectIcon(item, graphic);
                    ColorMatrix colorMatrix = new ColorMatrix()
                    {
                        Matrix33 = (item.Visible ? 1f : 0.35f)
                    };
                    ImageAttributes imageAttribute = new ImageAttributes();
                    imageAttribute.SetColorMatrix(colorMatrix);
                    Graphics graphics = e.Graphics;
                    Rectangle bounds = e.Bounds;
                    Rectangle rectangle = e.Bounds;
                    graphics.DrawImage(bitmap, new Rectangle(bounds.X + 4, rectangle.Y + 2, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttribute);
                }
            }
            Rectangle bounds1 = e.Bounds;
            Rectangle rectangle1 = e.Bounds;
            Rectangle rectangle2 = new Rectangle(bounds1.Width - 18, rectangle1.Y + 5, 10, 10);
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb((item.Visible ? 0xff : 35), Color.Black), 2f), rectangle2);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb((item.Visible ? 0xff : 135), item.Color)), rectangle2);
            string str = ((e.State & DrawItemState.Selected) != DrawItemState.None ? "HighlightText" : "WindowText");
            Graphics graphics1 = e.Graphics;
            string name = item.Name;
            Font font = this.comboBox.Font;
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb((item.Visible ? 0xff : 150), Color.FromName(str)));
            float y = (float)e.Bounds.Y;
            float width = (float)(e.Bounds.Width - 40);
            Rectangle bounds2 = e.Bounds;
            graphics1.DrawString(name, font, solidBrush, new RectangleF(27f, y, width, (float)bounds2.Height));
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.exitNow)
            {
                return;
            }
            Utilities.DisplayName selectedItem = (Utilities.DisplayName)this.comboBox.SelectedItem;
            if (selectedItem == null)
            {
                this.ActiveObject = null;
            }
            else
            {
                if (this.selectedObject != null && (((DrawObject)selectedItem.Owner).ID == this.selectedObject.ID || ((DrawObject)selectedItem.Owner).GroupID == this.selectedObject.GroupID && this.selectedObject.GroupID != -1))
                {
                    return;
                }
                this.ActiveObject = (DrawObject)selectedItem.Owner;
            }
            if (this.OnObjectSelected != null)
            {
                this.OnObjectSelected(this.selectedObject);
            }
        }

        private void CreateSuperTab(Preset preset)
        {
            SuperTabItem nullable = this.superTabProperties.CreateTab(this.GetSuperTabCaption(preset));
            nullable.Tag = preset;
            nullable.TextAlignment = new eItemAlignment?(eItemAlignment.Center);
        }

        private bool CustomRenderingEnabled(Preset preset)
        {
            if (this.selectedObject == null || preset == null)
            {
                return false;
            }
            if (this.selectedObject.ObjectType != "Area")
            {
                return false;
            }
            return preset.CustomRendering != null;
        }

        private void DisableEditor()
        {
            this.selectedObject = null;
            this.lastRefreshedObject = null;
            this.exitNow = true;
            this.comboBox.SelectedIndex = -1;
            this.exitNow = false;
            this.editableProperties.Clear();
            AdvPropertyGrid advPropertyGrid = this.propertyGrid;
            string[] strArrays = new string[] { "Properties", "SelectedObjectsProperties", "PresetProperties", "PresetCustomRendering", "Values", "PresetResults" };
            advPropertyGrid.IgnoredCategories = strArrays;
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
            this.ProductAdd(this.project.DBManagement.BrowserInterface, this.estimatingItemsControl, group.EstimatingItems, -1);
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

        private void extensionsManager_OnPresetAfterDeleted(object sender, EventArgs e)
        {
            this.drawArea.Owner.SetModified();
            this.drawArea.Refresh();
            if (this.OnPresetDeleted != null)
            {
                this.OnPresetDeleted(null);
            }
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

        private void extensionsManager_OnPresetModified(Preset preset)
        {
            this.drawArea.Owner.SetModified();
            this.RenameSuperTab(preset);
            if (this.OnPresetModified != null)
            {
                this.OnPresetModified(preset);
            }
        }

        private void extensionsManager_OnPresetRenamed(Preset preset, string oldDisplayName, string newDisplayName)
        {
            if (this.coInterface.IsReady && this.selectedObject != null)
            {
                FormulaUtilities.RenameExtension(this.selectedObject.Group, oldDisplayName, newDisplayName);
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
            if (drawObject == null)
            {
                Console.WriteLine("DrawObject was null");
                this.DisableEditor();
                this.exitNow = false;
            }
            else
            {
                Utilities.DisplayName selectedItem = (Utilities.DisplayName)this.comboBox.SelectedItem;
                if (selectedItem != null)
                {
                    flag = ((DrawObject)selectedItem.Owner).HasSameGroupOrID(drawObject);
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
        }

        private string FormatField(string value, ExtensionField.ExtensionFieldTypeEnum fieldType)
        {
            double num = Utilities.ConvertToDouble(value, -1);
            if (fieldType != ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension)
            {
                return num.ToString();
            }
            string lengthStringFromUnitSystem = this.drawArea.UnitScale.ToLengthStringFromUnitSystem(num, false, false, true);
            return lengthStringFromUnitSystem;
        }

        private string FormatPresetResult(string value, ExtensionResult.ExtensionResultTypeEnum resultType)
        {
            if (resultType == ExtensionResult.ExtensionResultTypeEnum.ResultTypeCustom)
            {
                return value;
            }
            double num = Utilities.ConvertToDouble(value, -1);
            this.GetSelectedPreset(false);
            switch (resultType)
            {
                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                    {
                        return this.drawArea.UnitScale.ToLengthStringFromUnitSystem(num, false, true, true);
                    }
                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                    {
                        return this.drawArea.UnitScale.ToAreaStringFromUnitSystem(num, true);
                    }
                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                    {
                        return this.drawArea.UnitScale.ToCubicStringFromUnitSystem(num, true);
                    }
                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
                    {
                        return this.drawArea.ToCurrency(num);
                    }
            }
            return this.drawArea.UnitScale.Round(num).ToString();
        }

        private void GetDefaultFormula(CEstimatingItemsBrowserInterface browserInterface, DrawObject selectedObject, CEstimatingItem product, string coUnit)
        {
            GroupStats groupStat = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, selectedObject, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, true, "");
            browserInterface.GetDefaultFormula(selectedObject, groupStat, product, coUnit);
        }

        private string[] GetIgnoredPresetProperties(Preset preset)
        {
            List<string> strs = new List<string>();
            if (preset != null)
            {
                for (int i = preset.Choices.Count; i < 25; i++)
                {
                    int num = i + 1;
                    strs.Add(string.Concat("PresetChoice", num.ToString()));
                }
                for (int j = preset.Fields.Count; j < 25; j++)
                {
                    int num1 = j + 1;
                    strs.Add(string.Concat("PresetField", num1.ToString()));
                }
                strs.Add("RenderingAngle");
                strs.Add("RenderingOffsetX");
                strs.Add("RenderingOffsetY");
                int num2 = 0;
                foreach (PresetResult collection in preset.Results.Collection)
                {
                    if (!collection.ConditionMet)
                    {
                        continue;
                    }
                    num2++;
                }
                for (int k = num2; k < 50; k++)
                {
                    int num3 = k + 1;
                    strs.Add(string.Concat("PresetResult", num3.ToString()));
                }
            }
            return strs.ToArray();
        }

        private PropertyNode GetPropertyNode(string propertyName)
        {
            PropertyNode propertyNode;
            try
            {
                propertyNode = this.propertyGrid.GetPropertyNode(propertyName);
            }
            catch
            {
                propertyNode = null;
            }
            return propertyNode;
        }

        private List<string> GetRenderingSameProperties(Preset preset, CustomRenderingProperties propertiesToCompare)
        {
            bool flag = true;
            bool flag1 = true;
            bool flag2 = true;
            List<string> strs = new List<string>();
            if (preset != null)
            {
                for (int i = 0; i < this.drawArea.SelectionCount; i++)
                {
                    DrawObject selectedObject = this.drawArea.GetSelectedObject(i);
                    if (selectedObject.ObjectType == "Area" && selectedObject.HasSameGroupOrID(this.selectedObject))
                    {
                        CustomRenderingProperties customRenderingProperties = ((DrawPolyLine)selectedObject).GetCustomRenderingProperties(preset.ID);
                        flag = (flag ? customRenderingProperties.Angle == propertiesToCompare.Angle : false);
                        flag1 = (flag1 ? customRenderingProperties.OffsetX == propertiesToCompare.OffsetX : false);
                        flag2 = (flag2 ? customRenderingProperties.OffsetY == propertiesToCompare.OffsetY : false);
                    }
                    if (!flag && !flag1 && !flag2)
                    {
                        break;
                    }
                }
            }
            if (flag)
            {
                strs.Add("RenderingAngle");
            }
            if (flag1)
            {
                strs.Add("RenderingOffsetX");
            }
            if (flag2)
            {
                strs.Add("RenderingOffsetY");
            }
            if (flag1 || flag2)
            {
                strs.Add("RenderingOffsetScale");
            }
            return strs;
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

        private string GetSuperTabCaption(Preset preset)
        {
            string[] words = Utilities.GetWords(preset.DisplayName);
            int num = 0;
            string str = "";
            string[] strArrays = words;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str1 = strArrays[i];
                string str2 = (num == 0 ? "" : " ");
                if (num > -1)
                {
                    if ((double)num != Math.Floor((double)((int)words.Length / 2)) || num <= 0)
                    {
                        num++;
                    }
                    else
                    {
                        str2 = "\n";
                        num = -1;
                    }
                }
                str = string.Concat(str, str2, str1);
            }
            return str;
        }

        private void grid_ConvertFromStringToPropertyValue(object sender, ConvertValueEventArgs e)
        {
            if (e.StringValue == null)
            {
                return;
            }
            string propertyName = e.PropertyName;
            string str = propertyName;
            if (propertyName != null)
            {
                if (str != "Color")
                {
                    goto Label1;
                }
                Color empty = Color.Empty;
                string stringValue = e.StringValue;
                if (ColorPicker.ColorNameExists(stringValue))
                {
                    empty = ColorPicker.StringToColor(stringValue);
                }
                string[] strArrays = new string[] { ",", " ", "\t" };
                string[] strArrays1 = stringValue.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
                if (strArrays1.GetLength(0) == 3)
                {
                    try
                    {
                        empty = Color.FromArgb(Utilities.ConvertToInt(strArrays1.GetValue(0)), Utilities.ConvertToInt(strArrays1.GetValue(1)), Utilities.ConvertToInt(strArrays1.GetValue(2)));
                    }
                    catch
                    {
                        empty = Color.Empty;
                    }
                }
                if (empty == Color.Empty)
                {
                    try
                    {
                        if (Color.FromName(stringValue).ToArgb() != Color.Empty.ToArgb())
                        {
                            empty = this.drawArea.GetNextColor(this.selectedObject.ObjectType);
                        }
                    }
                    catch
                    {
                    }
                }
                if (empty != Color.Empty)
                {
                    e.TypedValue = empty;
                    e.IsConverted = true;
                    return;
                }
                else
                {
                    return;
                }
            }
        Label2:
            if (e.PropertyName.IndexOf("PresetField") != -1)
            {
                int num = Utilities.ConvertToInt(e.PropertyName.Substring(11, e.PropertyName.Length - 11));
                PresetField typedValue = this.propertyFields[num - 1];
                if (typedValue != null)
                {
                    switch (typedValue.FieldType)
                    {
                        case ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension:
                            {
                                UnitScale.UnitSystem unitSystem = UnitScale.UnitSystem.undefined;
                                double value = UnitScale.ConvertDimensionToValue(e.StringValue, this.drawArea.UnitScale.CurrentSystemType, ref unitSystem);
                                if (unitSystem != this.drawArea.UnitScale.ScaleSystemType)
                                {
                                    switch (unitSystem)
                                    {
                                        case UnitScale.UnitSystem.metric:
                                            {
                                                value = UnitScale.FromMetersToFeet(value);
                                                break;
                                            }
                                        case UnitScale.UnitSystem.imperial:
                                            {
                                                value = UnitScale.FromFeetToMeters(value);
                                                break;
                                            }
                                    }
                                }
                                e.TypedValue = this.FormatField(value.ToString(), typedValue.FieldType);
                                Preset selectedPreset = this.GetSelectedPreset(false);
                                if (selectedPreset != null && selectedPreset.ScaleSystemType != this.drawArea.UnitScale.ScaleSystemType)
                                {
                                    value = (selectedPreset.ScaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(value) : UnitScale.FromFeetToMeters(value));
                                }
                                typedValue.Value = value;
                                break;
                            }
                        case ExtensionField.ExtensionFieldTypeEnum.FieldTypeInteger:
                            {
                                e.TypedValue = Utilities.ConvertToInt(e.StringValue);
                                typedValue.Value = e.TypedValue;
                                break;
                            }
                        case ExtensionField.ExtensionFieldTypeEnum.FieldTypeDouble:
                        case ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency:
                            {
                                e.TypedValue = Utilities.ConvertToDouble(e.StringValue, -1);
                                typedValue.Value = e.TypedValue;
                                break;
                            }
                    }
                    e.IsConverted = true;
                }
            }
            return;
        Label1:
            if (str == "Pattern")
            {
                e.TypedValue = HatchStylePickerCombo.StringToPattern(e.StringValue);
                e.IsConverted = true;
                return;
            }
            if (str == "Shape")
            {
                e.TypedValue = DrawCounter.StringToShape(e.StringValue);
                e.IsConverted = true;
                return;
            }
            goto Label2;
        }

        private void grid_ConvertPropertyValueToString(object sender, ConvertValueEventArgs e)
        {
            if (e.TypedValue == null)
            {
                e.StringValue = "";
                e.IsConverted = true;
                return;
            }
            string propertyName = e.PropertyName;
            string str = propertyName;
            if (propertyName != null)
            {
                switch (str)
                {
                    case "Color":
                        {
                            e.StringValue = ColorPicker.ColorToString((Color)e.TypedValue);
                            e.IsConverted = true;
                            return;
                        }
                    case "Pattern":
                        {
                            e.StringValue = HatchStylePickerCombo.PatternToString((HatchStylePickerCombo.HatchStylePickerEnum)e.TypedValue);
                            e.IsConverted = true;
                            return;
                        }
                    case "Shape":
                        {
                            e.StringValue = DrawCounter.ShapeToString((DrawCounter.CounterShapeTypeEnum)e.TypedValue);
                            e.IsConverted = true;
                            return;
                        }
                    case "ShowMeasure":
                        {
                            e.StringValue = ((bool)e.TypedValue ? Resources.Oui : Resources.Non);
                            e.IsConverted = true;
                            return;
                        }
                    case "Visible":
                        {
                            e.StringValue = ((bool)e.TypedValue ? Resources.Visible : Resources.Invisible);
                            e.IsConverted = true;
                            return;
                        }
                    case "IncludeAllPlans":
                        {
                            e.StringValue = ((bool)e.TypedValue ? Resources.Oui : Resources.Non);
                            e.IsConverted = true;
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }

        private void grid_Localize(object sender, AdvPropertyGridLocalizeEventArgs e)
        {
            if (e.LocalizationType != ePropertyGridLocalizationType.Category)
            {
                return;
            }
            string key = e.Key;
            string str = key;
            if (key != null)
            {
                if (str == "Properties")
                {
                    e.LocalizedValue = (GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, this.drawArea.SelectedObject) ? Resources.Propriétés_du_groupe : Resources.Propriétés);
                    return;
                }
                if (str == "SelectedObjectsProperties" || str == "PresetCustomRendering")
                {
                    e.LocalizedValue = Resources.Propriétés_de_la_sélection;
                    return;
                }
                if (str == "PresetProperties")
                {
                    e.LocalizedValue = Resources.Propriétés_de_l_extension;
                    return;
                }
                if (str == "Values")
                {
                    e.LocalizedValue = (GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, this.drawArea.SelectedObject) ? Resources.Résultats_du_groupe : Resources.Résultats);
                    return;
                }
                if (str != "PresetResults")
                {
                    return;
                }
                e.LocalizedValue = Resources.Résultats_de_l_extension;
            }
        }

        private void grid_PropertyValueChanging(object sender, PropertyValueChangingEventArgs e)
        {
            if (e.NewValue == null)
            {
                e.Handled = true;
                return;
            }
            string propertyName = e.PropertyName;
            string str = propertyName;
            if (propertyName != null)
            {
                if (str != "Color")
                {
                    return;
                }
                if ((Color)e.NewValue == Color.Empty)
                {
                    e.Handled = true;
                }
            }
        }

        private void grid_ValidatePropertyValue(object sender, ValidatePropertyValueEventArgs e)
        {
            this.ValidatePropertyValue(e);
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
            propertySetting.ConvertFromStringToPropertyValue += new ConvertValueEventHandler(this.ChoiceElementsUITypeEditorConvertFromStringToPropertyValue);
            propertySetting.ConvertPropertyValueToString += new ConvertValueEventHandler(this.ChoiceElementsUITypeEditorConvertPropertyValueToString);
        }

        private void InitializeCOfficeGUI()
        {
            this.coInterface.InitializeBrowserInterface(this.drawArea.Owner.ImageCollection, this.coInterface.COSections[1] != null);
            this.coControl.OnProductAdd += new EventHandler(this.coControl_OnProductAdd);
            this.coControl.OnProductRemove += new EventHandler(this.coControl_OnProductRemove);
            this.coControl.OnFormulaEdit += new EventHandler(this.coControl_OnFormulaEdit);
        }

        private void InitializeColorPicker()
        {
            PropertySettings propertySetting = new PropertySettings("Color");
            this.colorPickerEditor = new UITypeEditorColorPicker();
            this.colorPickerEditor.OnColorHover += new OnColorHoverHandler(this.colorPickerEditor_OnColorHover);
            propertySetting.DisplayName = Resources.Couleur;
            propertySetting.UITypeEditor = this.colorPickerEditor;
            this.propertyGrid.PropertySettings.Add(propertySetting);
            PropertySettings propertySetting1 = new PropertySettings("PenWidth")
            {
                DisplayName = Resources.Épaisseur,
                ValueEditor = new PropertySliderEditor(1, 100)
            };
            this.propertyGrid.PropertySettings.Add(propertySetting1);
            PropertySettings propertySetting2 = new PropertySettings("PresetResult1")
            {
                ValueEditor = new PropertySliderEditor(1, 100)
            };
            this.propertyGrid.PropertySettings.Add(propertySetting2);
        }

        private void InitializeCombo()
        {
            this.comboBox.DrawItem += new DrawItemEventHandler(this.combo_DrawItem);
            this.comboBox.SelectedIndexChanged += new EventHandler(this.combo_SelectedIndexChanged);
            this.comboBox.Sorted = true;
        }

        private void InitializeEstimatingItemsGUI()
        {
            this.project.DBManagement.InitializeBrowserInterface(this.drawArea.Owner.ImageCollection);
            this.estimatingItemsControl.OnProductAdd += new EventHandler(this.estimatingItemsControl_OnProductAdd);
            this.estimatingItemsControl.OnProductRemove += new EventHandler(this.estimatingItemsControl_OnProductRemove);
            this.estimatingItemsControl.OnFormulaEdit += new EventHandler(this.estimatingItemsControl_OnFormulaEdit);
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
            this.propertyGrid.Localize += new LocalizeEventHandler(this.grid_Localize);
            this.propertyGrid.ConvertPropertyValueToString += new ConvertValueEventHandler(this.grid_ConvertPropertyValueToString);
            this.propertyGrid.ConvertFromStringToPropertyValue += new ConvertValueEventHandler(this.grid_ConvertFromStringToPropertyValue);
            this.propertyGrid.PropertyValueChanging += new PropertyValueChangingEventHandler(this.grid_PropertyValueChanging);
            this.propertyGrid.ValidatePropertyValue += new ValidatePropertyValueEventHandler(this.grid_ValidatePropertyValue);
            this.propertyGrid.ProvidePropertyValueList += new PropertyValueListEventHandler(this.propertyGrid_ProvidePropertyValueList);
            this.propertyGrid.PropertyTree.KeyDown += new KeyEventHandler(this.propertyTree_KeyDown);
            ((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForThisPlan"]).CheckedChanged += new EventHandler(this.DisplayCalculations_CheckedChanged);
            ((ButtonItem)this.barDisplayCalculations.Items["btDisplayResultsForAllPlans"]).CheckedChanged += new EventHandler(this.DisplayCalculations_CheckedChanged);
        }

        private void InitializeSlopeFactorControl()
        {
            PropertySettings propertySetting = new PropertySettings("SlopeFactor");
            this.slopeFactorEditor = new UITypeEditorSlopeControl();
            this.slopeFactorEditor.OnSave += new OnSlopeFactorSaveHandler(this.slopeFactorEditor_OnSave);
            propertySetting.ConvertFromStringToPropertyValue += new ConvertValueEventHandler(this.SlopeFactorUITypeEditorConvertFromStringToPropertyValue);
            propertySetting.ConvertPropertyValueToString += new ConvertValueEventHandler(this.SlopeFactorUITypeEditorConvertPropertyValueToString);
            propertySetting.DisplayName = Resources.Facteur_de_pente;
            propertySetting.UITypeEditor = this.slopeFactorEditor;
            this.propertyGrid.PropertySettings.Add(propertySetting);
        }

        private void InitializeTabs()
        {
            this.tabProperties.SelectedTabChanged += new TabStrip.SelectedTabChangedEventHandler(this.tabProperties_SelectedTabChanged);
            this.superTabProperties.SelectedTabChanged += new EventHandler<SuperTabStripSelectedTabChangedEventArgs>(this.superTabProperties_SelectedTabChanged);
        }

        private void InitializeTemplatesManager()
        {
            this.extensionsManager.OnPresetCreated += new PresetEventHandler(this.extensionsManager_OnPresetCreated);
            this.extensionsManager.OnPresetRenamed += new PresetRenameEventHandler(this.extensionsManager_OnPresetRenamed);
            this.extensionsManager.OnPresetModified += new PresetEventHandler(this.extensionsManager_OnPresetModified);
            this.extensionsManager.OnPresetDeleted += new PresetEventHandler(this.extensionsManager_OnPresetDeleted);
            this.extensionsManager.OnPresetAfterDeleted += new EventHandler(this.extensionsManager_OnPresetAfterDeleted);
        }

        public void Insert(int index, DrawObject drawObject)
        {
            this.comboBox.Items.Insert(index, drawObject.DisplayName);
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

        private int LocatePresetSuperTab(Preset preset)
        {
            int num = 0;
            int num1 = 1;
            while (num1 < this.superTabProperties.Tabs.Count)
            {
                if (!preset.Equals((Preset)((SuperTabItem)this.superTabProperties.Tabs[num1]).Tag))
                {
                    num1++;
                }
                else
                {
                    num = num1;
                    break;
                }
            }
            return num;
        }

        private void ProductAdd(CEstimatingItemsBrowserInterface browserInterface, CEstimatingsItemsControl itemsControl, CEstimatingItems products, double value = 0)
        {
            Utilities.EnableInterface(this.drawArea.Owner, false);
            browserInterface.ShowBrowser(this.drawArea.Owner);
            Utilities.EnableInterface(this.drawArea.Owner, true);
            CEstimatingItem selectedProduct = browserInterface.Browser.SelectedProduct;
            if (selectedProduct == null)
            {
                return;
            }
            CEstimatingItem cEstimatingItem = new CEstimatingItem(
                selectedProduct.ItemID,
                selectedProduct.Description,
                selectedProduct.Value,
                selectedProduct.Unit,
                selectedProduct.ItemType,
                selectedProduct.UnitMeasure,
                selectedProduct.CoverageValue,
                selectedProduct.CoverageUnit,
                selectedProduct.SectionID,
                selectedProduct.SubSectionID,
                selectedProduct.BidCode,
                "");

            cEstimatingItem.Tag = cEstimatingItem;

            if (value != 0)
            {
                cEstimatingItem.Value = value;
            }
            this.GetDefaultFormula(browserInterface, this.selectedObject, cEstimatingItem, selectedProduct.Unit);
            if (cEstimatingItem.Formula == "")
            {
                GroupStats groupStat = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, this.selectedObject, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, true, "");
                FormulaResults formulaResult = new FormulaResults();
                formulaResult.Refresh(this.selectedObject, groupStat, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, this.drawArea.ActivePlan.UnitScale, this.selectedObject.Group.Presets, this.drawArea.Project.ExtensionsSupport);
                using (FormulaBuilderForm formulaBuilderForm = new FormulaBuilderForm(cEstimatingItem, formulaResult, this.selectedObject, this.drawArea.Owner.ImageCollection))
                {
                    formulaBuilderForm.ShowDialog(this.drawArea.Owner);
                }
            }
            if (cEstimatingItem.Formula == "")
            {
                return;
            }
            products.Add(cEstimatingItem);
            itemsControl.RefreshList();
            itemsControl.SelectItem(cEstimatingItem);
            Console.WriteLine(string.Concat("product=", cEstimatingItem.Description));
            if (this.OnProductCreated != null)
            {
                this.OnProductCreated(selectedProduct);
            }
        }

        private void ProductEdit(CEstimatingsItemsControl itemsControl)
        {
            CEstimatingItem selectedProduct = itemsControl.SelectedProduct;
            if (selectedProduct == null)
            {
                return;
            }
            GroupStats groupStat = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, this.selectedObject, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, true, "");
            FormulaResults formulaResult = new FormulaResults();
            formulaResult.Refresh(this.selectedObject, groupStat, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, this.drawArea.ActivePlan.UnitScale, this.selectedObject.Group.Presets, this.drawArea.Project.ExtensionsSupport);
            using (FormulaBuilderForm formulaBuilderForm = new FormulaBuilderForm(selectedProduct, formulaResult, this.selectedObject, this.drawArea.Owner.ImageCollection))
            {
                formulaBuilderForm.ShowDialog(this.drawArea.Owner);
            }
            if (itemsControl.Name != "EstimatingItems")
            {
                itemsControl.RefreshList();
            }
            else
            {
                this.UpdateProductsGUI(itemsControl, this.selectedObject.Group.EstimatingItems);
            }
            if (this.OnProductModified != null)
            {
                this.OnProductModified(selectedProduct);
            }
        }

        private void ProductRemove(CEstimatingsItemsControl itemsControl, CEstimatingItems products)
        {
            CEstimatingItem selectedProduct = itemsControl.SelectedProduct;
            if (selectedProduct == null)
            {
                return;
            }
            if (Utilities.DisplayDeleteConfirmation(Resources.Supprimer_ce_produit_, Resources.Ce_produit_sera_supprimé_de_ce_groupe) == DialogResult.No)
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

        private void propertyGrid_ProvidePropertyValueList(object sender, PropertyValueListEventArgs e)
        {
            string propertyName = e.PropertyName;
            string str = propertyName;
            if (propertyName != null)
            {
                if (str == "Pattern")
                {
                    string[] solide = new string[] { Resources.Solide, Resources.Horizontal, Resources.Vertical, Resources.ForwardDiagonal, Resources.BackwardDiagonal, Resources.DiagonalCross, Resources.Percent05, Resources.Percent10, Resources.Percent20, Resources.Percent25, Resources.Percent30, Resources.Percent40, Resources.Percent50, Resources.Percent60, Resources.Percent70, Resources.Percent75, Resources.Percent80, Resources.Percent90, Resources.LightDownwardDiagonal, Resources.LightUpwardDiagonal, Resources.DarkDownwardDiagonal, Resources.DarkUpwardDiagonal, Resources.WideDownwardDiagonal, Resources.WideUpwardDiagonal, Resources.LightVertical, Resources.LightHorizontal, Resources.NarrowVertical, Resources.NarrowHorizontal, Resources.DarkVertical, Resources.DarkHorizontal, Resources.DashedDownwardDiagonal, Resources.DashedUpwardDiagonal, Resources.DashedHorizontal, Resources.DashedVertical, Resources.SmallConfetti, Resources.LargeConfetti, Resources.ZigZag, Resources.Wave, Resources.DiagonalBrick, Resources.HorizontalBrick, Resources.Weave, Resources.Plaid, Resources.Divot, Resources.DottedGrid, Resources.DottedDiamond, Resources.Shingle, Resources.Trellis, Resources.Sphere, Resources.SmallGrid, Resources.SmallCheckerBoard, Resources.LargeCheckerBoard, Resources.OutlinedDiamond, Resources.SolidDiamond };
                    e.ValueList = new List<string>(solide);
                    e.IsListValid = true;
                    return;
                }
                if (str != "Shape")
                {
                    return;
                }
                string[] cercle = new string[] { Resources.Cercle, Resources.Carré, Resources.Losange, Resources.Triangle, Resources.Triangle_inversé, Resources.Trapèze, Resources.Trapèze_inversé };
                e.ValueList = new List<string>(cercle);
                e.IsListValid = true;
            }
        }

        private void propertyTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.C)
            {
                return;
            }
            if (Control.ModifierKeys == Keys.Control)
            {
                e.Handled = true;
            }
        }

        public void Refresh()
        {
            this.RefreshProperties();
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
            GroupStats groupStat = null;
            groupStat = (this.project.DisplayResultsForAllPlans ? GroupUtilities.ComputeGroupStats(this.project, this.selectedObject, null, activePlan.UnitScale.ScaleSystemType, true, "") : GroupUtilities.ComputeGroupStats(activePlan, this.selectedObject, activePlan.UnitScale.ScaleSystemType, true, ""));
            bool flag = false;
            if (this.lastRefreshedObject != null)
            {
                flag = (this.selectedObject.IsPartOfGroup() ? this.selectedObject.HasSameGroupOrID(this.lastRefreshedObject) : this.selectedObject.ID == this.lastRefreshedObject.ID);
            }
            if (!flag)
            {
                AdvPropertyGrid advPropertyGrid = this.propertyGrid;
                string[] strArrays = new string[] { "PresetProperties", "PresetCustomRendering", "PresetResults", "Properties", "SelectedObjectsProperties", "Values" };
                advPropertyGrid.IgnoredCategories = strArrays;
            }
            this.lastRefreshedObject = this.selectedObject;
            if (selectedPreset == null)
            {
                this.UpdateDefaultProperties(this.selectedObject, groupStat);
            }
            else
            {
                this.extensionSupport.QueryPresetResults(selectedPreset, groupStat, activePlan.UnitScale);
                this.UpdatePresetProperties(selectedPreset, activePlan.UnitScale, false);
            }
            this.propertyGrid.PropertyTree.ColorSchemeStyle = eColorSchemeStyle.Office2007;
            this.propertyGrid.PropertyTree.SelectionBoxStyle = eSelectionStyle.HighlightCells;
            this.propertyGrid.PropertyTree.HotTracking = true;
            this.propertyGrid.PropertyTree.Font = Utilities.GetDefaultFont();
            this.propertyGrid.PropertyTree.Zoom = 1.000001f;
            this.propertyGrid.IgnoredCategories = (selectedPreset == null ? new string[] { "PresetProperties", "PresetCustomRendering", "PresetResults" } : new string[] { "Properties", "SelectedObjectsProperties", "Values" });
            this.propertyGrid.RefreshProperties();
            if (this.propertyGrid.PropertyTree.Nodes.Count > 0)
            {
                this.propertyGrid.PropertyTree.SelectedIndex = 0;
            }
        }

        private void RefreshRenderingResults()
        {
            Plan activePlan = this.drawArea.ActivePlan;
            Preset selectedPreset = this.GetSelectedPreset(false);
            if (this.drawArea.ActivePlan == null || this.selectedObject == null || selectedPreset == null)
            {
                return;
            }
            GroupStats groupStat = null;
            groupStat = (this.project.DisplayResultsForAllPlans ? GroupUtilities.ComputeGroupStats(this.project, this.selectedObject, null, activePlan.UnitScale.ScaleSystemType, true, "") : GroupUtilities.ComputeGroupStats(activePlan, this.selectedObject, activePlan.UnitScale.ScaleSystemType, true, ""));
            this.extensionSupport.QueryPresetResults(selectedPreset, groupStat, activePlan.UnitScale);
            this.UpdatePresetProperties(selectedPreset, activePlan.UnitScale, true);
        }

        private void RefreshSelectedGroup()
        {
            bool count = true;
            int d = -1;
            DrawObjectGroup group = null;
            if (this.extensionsManager.Group != null)
            {
                d = this.extensionsManager.Group.ID;
            }
            group = this.selectedObject.Group;
            this.extensionsManager.SelectGroup(group, this.extensionSupport, this.drawArea.UnitScale);
            if (group == null)
            {
                this.estimatingItemsControl.Disable();
            }
            else
            {
                this.UpdateProductsGUI(this.estimatingItemsControl, group.EstimatingItems);
            }
            if (this.coInterface.IsReady)
            {
                if (group == null)
                {
                    this.coControl.Disable();
                }
                else
                {
                    this.UpdateProductsGUI(this.coControl, group.COfficeProducts);
                }
            }
            if (group != null)
            {
                count = group.Presets.Count == 0;
            }
            if (count)
            {
                this.DisableSuperTab();
            }
            else if (d != group.ID)
            {
                this.RefreshSuperTab();
                return;
            }
        }

        private void RefreshSuperTab()
        {
            this.ClearSuperTab();
            foreach (Preset collection in this.extensionsManager.Group.Presets.Collection)
            {
                this.CreateSuperTab(collection);
            }
            this.superTabProperties.RecalcLayout();
            this.superTabProperties.SelectedTabIndex = 0;
            this.superTabProperties.TabsVisible = true;
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
                bool flag = false;
                flag = (!this[i].IsPartOfGroup() ? this.drawArea.GetObjectByID(this.drawArea.ActivePlan, this[i].ID) == null : !GroupUtilities.GroupExists(this.drawArea.ActivePlan, this[i].GroupID));
                if (flag)
                {
                    this.comboBox.Items.RemoveAt(i);
                }
            }
        }

        private string[] RemoveFromIgnoredPresetProperties(string[] currentList, string[] elementsToRemove)
        {
            List<string> strs = new List<string>(currentList);
            string[] strArrays = elementsToRemove;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                strs.Remove(strArrays[i]);
            }
            return strs.ToArray();
        }

        private void RemoveSuperTab(int index)
        {
            int selectedTabIndex = this.superTabProperties.SelectedTabIndex;
            try
            {
                SuperTabItem item = (SuperTabItem)this.superTabProperties.Tabs[index];
                item.Tag = null;
                this.superTabProperties.Tabs.Remove(item);
                this.superTabProperties.Controls.Remove(item.AttachedControl);
            }
            catch
            {
            }
            if (selectedTabIndex == index)
            {
                this.superTabProperties.SelectedTabIndex = 0;
            }
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

        private void RenameSuperTab(Preset preset)
        {
            try
            {
                int num = this.LocatePresetSuperTab(preset);
                if (num > 0)
                {
                    SuperTabItem item = (SuperTabItem)this.superTabProperties.Tabs[num];
                    item.Text = this.GetSuperTabCaption(preset);
                }
            }
            catch
            {
            }
        }

        public void ReplaceObject(DrawObject drawObject)
        {
            if (drawObject != null)
            {
                for (int i = 0; i < this.comboBox.Items.Count; i++)
                {
                    if (((DrawObject)((Utilities.DisplayName)this.comboBox.Items[i]).Owner).HasSameGroupOrID(drawObject))
                    {
                        this.exitNow = true;
                        this.comboBox.Items[i] = drawObject.DisplayName;
                        this.exitNow = false;
                    }
                }
            }
        }

        public void ResetAllocatedGroups(int groupID)
        {
            bool flag = false;
            int num = 0;
            while (num < this.Count)
            {
                if (this[num].GroupID != groupID)
                {
                    num++;
                }
                else
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                DrawObject drawObject = this.drawArea.FindObjectFromGroupID(groupID);
                if (drawObject != null)
                {
                    this.Add(drawObject, false);
                    return;
                }
                Console.WriteLine("ResetAllocatedGroups::FindObjectFromGroupID null");
            }
        }

        public void ResetItems()
        {
            for (int i = 0; i < this.comboBox.Items.Count; i++)
            {
                Utilities.DisplayName item = (Utilities.DisplayName)this.comboBox.Items[i];
                this.exitNow = true;
                this.comboBox.Items[i] = item;
                this.exitNow = false;
            }
        }

        private PropertySettings SetGridPropertyDisplayName(string propertyName, string displayName)
        {
            PropertySettings propertySetting = new PropertySettings(propertyName)
            {
                DisplayName = displayName
            };
            int count = this.propertyGrid.PropertySettings.Count - 1;
            while (count >= 0)
            {
                if (this.propertyGrid.PropertySettings[count].PropertyName != propertyName)
                {
                    count--;
                }
                else
                {
                    this.propertyGrid.PropertySettings.RemoveAt(count);
                    break;
                }
            }
            this.propertyGrid.PropertySettings.Add(propertySetting);
            return propertySetting;
        }

        private void SetPresetChoiceProperty(int index, object value)
        {
            switch (index)
            {
                case 0:
                    {
                        this.properties.PresetChoice1 = value;
                        return;
                    }
                case 1:
                    {
                        this.properties.PresetChoice2 = value;
                        return;
                    }
                case 2:
                    {
                        this.properties.PresetChoice3 = value;
                        return;
                    }
                case 3:
                    {
                        this.properties.PresetChoice4 = value;
                        return;
                    }
                case 4:
                    {
                        this.properties.PresetChoice5 = value;
                        return;
                    }
                case 5:
                    {
                        this.properties.PresetChoice6 = value;
                        return;
                    }
                case 6:
                    {
                        this.properties.PresetChoice7 = value;
                        return;
                    }
                case 7:
                    {
                        this.properties.PresetChoice8 = value;
                        return;
                    }
                case 8:
                    {
                        this.properties.PresetChoice9 = value;
                        return;
                    }
                case 9:
                    {
                        this.properties.PresetChoice10 = value;
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void SetPresetFieldProperty(int index, string value)
        {
            switch (index)
            {
                case 0:
                    {
                        this.properties.PresetField1 = value;
                        return;
                    }
                case 1:
                    {
                        this.properties.PresetField2 = value;
                        return;
                    }
                case 2:
                    {
                        this.properties.PresetField3 = value;
                        return;
                    }
                case 3:
                    {
                        this.properties.PresetField4 = value;
                        return;
                    }
                case 4:
                    {
                        this.properties.PresetField5 = value;
                        return;
                    }
                case 5:
                    {
                        this.properties.PresetField6 = value;
                        return;
                    }
                case 6:
                    {
                        this.properties.PresetField7 = value;
                        return;
                    }
                case 7:
                    {
                        this.properties.PresetField8 = value;
                        return;
                    }
                case 8:
                    {
                        this.properties.PresetField9 = value;
                        return;
                    }
                case 9:
                    {
                        this.properties.PresetField10 = value;
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void SetPresetResult(int index, string value, string unit, ExtensionResult.ExtensionResultTypeEnum resultType)
        {
            switch (index)
            {
                case 0:
                    {
                        this.properties.PresetResult1 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 1:
                    {
                        this.properties.PresetResult2 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 2:
                    {
                        this.properties.PresetResult3 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 3:
                    {
                        this.properties.PresetResult4 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 4:
                    {
                        this.properties.PresetResult5 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 5:
                    {
                        this.properties.PresetResult6 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 6:
                    {
                        this.properties.PresetResult7 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 7:
                    {
                        this.properties.PresetResult8 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 8:
                    {
                        this.properties.PresetResult9 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 9:
                    {
                        this.properties.PresetResult10 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 10:
                    {
                        this.properties.PresetResult11 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 11:
                    {
                        this.properties.PresetResult12 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 12:
                    {
                        this.properties.PresetResult13 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 13:
                    {
                        this.properties.PresetResult14 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 14:
                    {
                        this.properties.PresetResult15 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 15:
                    {
                        this.properties.PresetResult16 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 16:
                    {
                        this.properties.PresetResult17 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 17:
                    {
                        this.properties.PresetResult18 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 18:
                    {
                        this.properties.PresetResult19 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 19:
                    {
                        this.properties.PresetResult20 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 20:
                    {
                        this.properties.PresetResult21 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 21:
                    {
                        this.properties.PresetResult22 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 22:
                    {
                        this.properties.PresetResult23 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 23:
                    {
                        this.properties.PresetResult24 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 24:
                    {
                        this.properties.PresetResult25 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 25:
                    {
                        this.properties.PresetResult26 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 26:
                    {
                        this.properties.PresetResult27 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 27:
                    {
                        this.properties.PresetResult28 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 28:
                    {
                        this.properties.PresetResult29 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 29:
                    {
                        this.properties.PresetResult30 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 30:
                    {
                        this.properties.PresetResult31 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 31:
                    {
                        this.properties.PresetResult32 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 32:
                    {
                        this.properties.PresetResult33 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 33:
                    {
                        this.properties.PresetResult34 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 34:
                    {
                        this.properties.PresetResult35 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 35:
                    {
                        this.properties.PresetResult36 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 36:
                    {
                        this.properties.PresetResult37 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 37:
                    {
                        this.properties.PresetResult38 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 38:
                    {
                        this.properties.PresetResult39 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 39:
                    {
                        this.properties.PresetResult40 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 40:
                    {
                        this.properties.PresetResult41 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 41:
                    {
                        this.properties.PresetResult42 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 42:
                    {
                        this.properties.PresetResult43 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 43:
                    {
                        this.properties.PresetResult44 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 44:
                    {
                        this.properties.PresetResult45 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 45:
                    {
                        this.properties.PresetResult46 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 46:
                    {
                        this.properties.PresetResult47 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 47:
                    {
                        this.properties.PresetResult48 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 48:
                    {
                        this.properties.PresetResult49 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                case 49:
                    {
                        this.properties.PresetResult50 = string.Concat(this.FormatPresetResult(value, resultType), (unit != string.Empty ? string.Concat(" ", unit) : ""));
                        return;
                    }
                default:
                    {
                        return;
                    }
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

        private void SlopeFactorUITypeEditorConvertFromStringToPropertyValue(object sender, ConvertValueEventArgs e)
        {
            if (e.TypedValue != null)
            {
                SlopeFactor typedValue = (SlopeFactor)e.TypedValue;
                e.StringValue = (typedValue == null ? "" : typedValue.ToString());
            }
            else
            {
                e.StringValue = "";
            }
            e.IsConverted = true;
        }

        private void SlopeFactorUITypeEditorConvertPropertyValueToString(object sender, ConvertValueEventArgs e)
        {
            e.IsConverted = true;
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
                Preset tag = null;
                if (this.superTabProperties.SelectedTabIndex > 0)
                {
                    tag = (Preset)this.superTabProperties.SelectedTab.Tag;
                    if (tag != null)
                    {
                        this.drawArea.SetGroupSelectedPreset(this.selectedObject, tag);
                    }
                }
                this.drawArea.SetGroupSelectedPreset(this.selectedObject, tag);
            }
            this.lastRefreshedObject = null;
            this.RefreshProperties();
            Utilities.SetObjectFocus(this.propertyGrid.PropertyTree);
        }

        private void tabProperties_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            switch (this.tabProperties.SelectedTabIndex)
            {
                case 0:
                    {
                        this.RefreshProperties();
                        Utilities.SetObjectFocus(this.propertyGrid.PropertyTree);
                        return;
                    }
                case 1:
                    {
                        Utilities.SetObjectFocus(this.extensionsManager);
                        return;
                    }
                case 2:
                    {
                        if (this.coInterface.IsReady)
                        {
                            Utilities.SetObjectFocus(this.coControl);
                        }
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void UpdateDefaultProperties(DrawObject drawObject, GroupStats groupStats)
        {
            string[] strArrays;
            this.editableProperties.Clear();
            if (this.drawArea.ActivePlan == null)
            {
                return;
            }
            string objectType = drawObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                switch (str)
                {
                    case "Line":
                        {
                            AdvPropertyGrid advPropertyGrid = this.propertyGrid;
                            string[] strArrays1 = new string[] { "FontSize", "Text", "FillColor", "Pattern", "Shape", "CounterSize", "MaxRows", "Area", "AreaMinusDeduction", "Deduction", "DeductionsCount", "DropLength", "NetLength", "DropsCount", "PerimeterPlusDeduction", "CornersCount", "EndsCount", "Perimeter", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "PerimeterMinusOpening", "Angle" };
                            advPropertyGrid.IgnoredProperties = strArrays1;
                            List<string> strs = this.editableProperties;
                            string[] nom = new string[] { Resources.Nom, Resources.Couleur, Resources.Facteur_de_pente, Resources.Commentaire, Resources.Longueur };
                            strs.AddRange(nom);
                            this.properties.Name = drawObject.Name;
                            this.properties.Comment = drawObject.Comment;
                            this.properties.Color = drawObject.Color;
                            this.properties.PenWidth = drawObject.PenWidth;
                            this.properties.SlopeFactor = drawObject.SlopeFactor;
                            this.properties.ShowMeasure = drawObject.ShowMeasure;
                            this.properties.Visible = drawObject.Visible;
                            if (!GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
                            {
                                this.AddIgnoredProperty("Label");
                            }
                            else
                            {
                                this.properties.Label = drawObject.Label;
                                this.editableProperties.Add(Resources.Étiquette);
                            }
                            this.properties.GroupCount = this.drawArea.ToUnitString(groupStats.GroupCount);
                            this.properties.Length = this.drawArea.ToLengthStringFromUnitSystem(groupStats.Perimeter, false);
                            return;
                        }
                    case "Rectangle":
                        {
                            AdvPropertyGrid advPropertyGrid1 = this.propertyGrid;
                            string[] strArrays2 = new string[] { "FontSize", "Label", "Text", "MaxRows", "GroupCount", "FillColor", "Pattern", "Shape", "PenWidth", "SlopeFactor", "ShowMeasure", "CounterSize", "Area", "AreaMinusDeduction", "Deduction", "DeductionsCount", "Perimeter", "DropLength", "NetLength", "DropsCount", "PerimeterPlusDeduction", "CornersCount", "EndsCount", "Length", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "PerimeterMinusOpening", "Angle" };
                            advPropertyGrid1.IgnoredProperties = strArrays2;
                            List<string> strs1 = this.editableProperties;
                            string[] nom1 = new string[] { Resources.Nom, Resources.Couleur, Resources.Commentaire };
                            strs1.AddRange(nom1);
                            this.properties.Name = drawObject.Name;
                            this.properties.Comment = drawObject.Comment;
                            this.properties.Color = drawObject.Color;
                            this.properties.Visible = drawObject.Visible;
                            return;
                        }
                    case "Angle":
                        {
                            AdvPropertyGrid advPropertyGrid2 = this.propertyGrid;
                            string[] strArrays3 = new string[] { "FontSize", "Label", "Text", "MaxRows", "GroupCount", "FillColor", "Pattern", "Shape", "SlopeFactor", "CounterSize", "Area", "AreaMinusDeduction", "Deduction", "DeductionsCount", "Perimeter", "DropLength", "NetLength", "DropsCount", "PerimeterPlusDeduction", "CornersCount", "EndsCount", "Length", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "PerimeterMinusOpening" };
                            advPropertyGrid2.IgnoredProperties = strArrays3;
                            List<string> strs2 = this.editableProperties;
                            string[] nom2 = new string[] { Resources.Nom, Resources.Couleur, Resources.Commentaire };
                            strs2.AddRange(nom2);
                            this.properties.Name = drawObject.Name;
                            this.properties.Comment = drawObject.Comment;
                            this.properties.Color = drawObject.Color;
                            this.properties.PenWidth = drawObject.PenWidth;
                            this.properties.ShowMeasure = drawObject.ShowMeasure;
                            this.properties.Visible = drawObject.Visible;
                            this.properties.Angle = this.drawArea.ToAngleString(((DrawAngle)drawObject).Angle, ((DrawAngle)drawObject).AngleType);
                            return;
                        }
                    case "Area":
                        {
                            if (((DrawPolyLine)drawObject).EditDeductions)
                            {
                                string[] strArrays4 = new string[] { "Name", "FontSize", "Label", "Comment", "Color", "Pattern", "Shape", "SlopeFactor", "ShowMeasure", "Visible", "Text", "CounterSize", "GroupCount", "FillColor", "PenWidth", "CornersCount", "EndsCount", "Length", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "DropLength", "DropsCount", "PerimeterMinusOpening", "Angle" };
                                strArrays = strArrays4;
                                if (groupStats.Area - groupStats.AreaMinusDeduction == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "AreaMinusDeduction");
                                }
                                if (groupStats.DeductionArea == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "Deduction");
                                }
                                if (groupStats.DeductionsCount == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "DeductionsCount");
                                }
                                if (groupStats.Perimeter - groupStats.PerimeterPlusDeduction == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "PerimeterPlusDeduction");
                                }
                                this.propertyGrid.IgnoredProperties = strArrays;
                                List<string> strs3 = this.editableProperties;
                                string[] surface = new string[] { Resources.Surface, Resources.Déduction, Resources.Surface_déductions, Resources.Périmètre, Resources.Périmètre_déductions, Resources.Nombre_de_déductions };
                                strs3.AddRange(surface);
                            }
                            else
                            {
                                string[] strArrays5 = new string[] { "FontSize", "Text", "MaxRows", "FillColor", "PenWidth", "Shape", "CounterSize", "CornersCount", "EndsCount", "Length", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "DropLength", "NetLength", "DropsCount", "PerimeterMinusOpening", "Angle" };
                                strArrays = strArrays5;
                                if (groupStats.Area - groupStats.AreaMinusDeduction == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "AreaMinusDeduction");
                                }
                                if (groupStats.DeductionArea == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "Deduction");
                                }
                                if (groupStats.DeductionsCount == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "DeductionsCount");
                                }
                                if (groupStats.Perimeter - groupStats.PerimeterPlusDeduction == 0)
                                {
                                    Utilities.AddToStringArray(ref strArrays, "PerimeterPlusDeduction");
                                }
                                this.propertyGrid.IgnoredProperties = strArrays;
                                List<string> strs4 = this.editableProperties;
                                string[] nom3 = new string[] { Resources.Nom, Resources.Étiquette, Resources.Couleur, Resources.Motif, Resources.Facteur_de_pente, Resources.Commentaire, Resources.Surface, Resources.Déduction, Resources.Surface_déductions, Resources.Périmètre, Resources.Périmètre_déductions, Resources.Nombre_de_déductions };
                                strs4.AddRange(nom3);
                                this.properties.Name = drawObject.Name;
                                this.properties.Comment = drawObject.Comment;
                                this.properties.Color = drawObject.Color;
                                this.properties.Pattern = ((DrawPolyLine)drawObject).Pattern;
                                this.properties.SlopeFactor = drawObject.SlopeFactor;
                                this.properties.ShowMeasure = drawObject.ShowMeasure;
                                this.properties.Visible = drawObject.Visible;
                                if (!GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
                                {
                                    this.AddIgnoredProperty("Label");
                                }
                                else
                                {
                                    this.properties.Label = drawObject.Label;
                                    this.editableProperties.Add(Resources.Étiquette);
                                }
                            }
                            this.properties.GroupCount = this.drawArea.ToUnitString(groupStats.GroupCount);
                            this.properties.Area = this.drawArea.ToAreaStringFromUnitSystem(groupStats.Area);
                            this.properties.Perimeter = this.drawArea.ToLengthStringFromUnitSystem(groupStats.Perimeter, false);
                            this.properties.AreaMinusDeduction = this.drawArea.ToAreaStringFromUnitSystem(groupStats.AreaMinusDeduction);
                            this.properties.Deduction = this.drawArea.ToAreaStringFromUnitSystem(groupStats.DeductionArea);
                            this.properties.DeductionsCount = this.drawArea.ToUnitString(groupStats.DeductionsCount);
                            this.properties.PerimeterPlusDeduction = this.drawArea.ToLengthStringFromUnitSystem(groupStats.PerimeterPlusDeduction, false);
                            return;
                        }
                    case "Perimeter":
                        {
                            string[] strArrays6 = new string[] { "FontSize", "Text", "MaxRows", "FillColor", "Pattern", "Shape", "CounterSize", "Area", "AreaMinusDeduction", "Deduction", "DeductionsCount", "Perimeter", "PerimeterPlusDeduction", "TeesCount", "Angle" };
                            strArrays = strArrays6;
                            if (groupStats.DropLength == 0)
                            {
                                Utilities.AddToStringArray(ref strArrays, "DropLength");
                                Utilities.AddToStringArray(ref strArrays, "NetLength");
                            }
                            if (groupStats.DeductionPerimeter == 0)
                            {
                                Utilities.AddToStringArray(ref strArrays, "Openings");
                            }
                            if (groupStats.Perimeter - groupStats.PerimeterMinusOpening == 0)
                            {
                                Utilities.AddToStringArray(ref strArrays, "PerimeterMinusOpening");
                            }
                            if (groupStats.DropsCount == 0)
                            {
                                Utilities.AddToStringArray(ref strArrays, "DropsCount");
                            }
                            if (groupStats.DeductionsCount == 0)
                            {
                                Utilities.AddToStringArray(ref strArrays, "OpeningsCount");
                            }
                            if (groupStats.CornersCount == 0)
                            {
                                Utilities.AddToStringArray(ref strArrays, "CornersCount");
                            }
                            if (groupStats.EndsCount == 0)
                            {
                                Utilities.AddToStringArray(ref strArrays, "EndsCount");
                            }
                            this.propertyGrid.IgnoredProperties = strArrays;
                            List<string> strs5 = this.editableProperties;
                            string[] nom4 = new string[] { Resources.Nom, Resources.Étiquette, Resources.Couleur, Resources.Facteur_de_pente, Resources.Commentaire, Resources.Longueur, Resources.Drop_length, Resources.Longueur_nette, Resources.Drops_count, Resources.Longueur_des_ouvertures, Resources.Longueur_sans_ouvertures, Resources.Nombre_d_ouvertures, Resources.Nombre_de_coins, Resources.Nombre_de_bouts, Resources.Nombre_de_segments };
                            strs5.AddRange(nom4);
                            this.properties.Name = drawObject.Name;
                            this.properties.Comment = drawObject.Comment;
                            this.properties.Color = drawObject.Color;
                            this.properties.PenWidth = drawObject.PenWidth;
                            this.properties.SlopeFactor = drawObject.SlopeFactor;
                            this.properties.ShowMeasure = drawObject.ShowMeasure;
                            this.properties.Visible = drawObject.Visible;
                            if (!GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
                            {
                                this.AddIgnoredProperty("Label");
                            }
                            else
                            {
                                this.properties.Label = drawObject.Label;
                                this.editableProperties.Add(Resources.Étiquette);
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
                        {
                            AdvPropertyGrid advPropertyGrid3 = this.propertyGrid;
                            string[] strArrays7 = new string[] { "FontSize", "MaxRows", "FillColor", "Pattern", "PenWidth", "SlopeFactor", "ShowMeasure", "Area", "AreaMinusDeduction", "Deduction", "DeductionsCount", "Perimeter", "DropLength", "NetLength", "DropsCount", "PerimeterPlusDeduction", "CornersCount", "EndsCount", "Length", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "PerimeterMinusOpening", "Angle" };
                            advPropertyGrid3.IgnoredProperties = strArrays7;
                            List<string> strs6 = this.editableProperties;
                            string[] nom5 = new string[] { Resources.Nom, Resources.Étiquette, Resources.Texte, Resources.Couleur, Resources.Forme, Resources.Commentaire, Resources.Nombre_d_objets };
                            strs6.AddRange(nom5);
                            this.properties.Name = drawObject.Name;
                            this.properties.Comment = drawObject.Comment;
                            this.properties.Text = ((DrawCounter)drawObject).Text;
                            this.properties.Color = drawObject.Color;
                            this.properties.Shape = ((DrawCounter)drawObject).Shape;
                            this.properties.CounterSize = ((DrawCounter)drawObject).DefaultSize;
                            this.properties.Visible = drawObject.Visible;
                            if (!GroupUtilities.SelectedObjectsHaveSameLabel(this.drawArea.ActiveLayer, drawObject))
                            {
                                this.AddIgnoredProperty("Label");
                            }
                            else
                            {
                                this.properties.Label = drawObject.Label;
                                this.editableProperties.Add(Resources.Étiquette);
                            }
                            this.properties.GroupCount = this.drawArea.ToUnitString(groupStats.GroupCount);
                            return;
                        }
                    case "Note":
                        {
                            AdvPropertyGrid advPropertyGrid4 = this.propertyGrid;
                            string[] strArrays8 = new string[] { "Label", "Text", "CounterSize", "MaxRows", "GroupCount", "FillColor", "Pattern", "Shape", "SlopeFactor", "ShowMeasure", "Area", "AreaMinusDeduction", "Deduction", "DeductionsCount", "Perimeter", "DropLength", "NetLength", "DropsCount", "PerimeterPlusDeduction", "CornersCount", "EndsCount", "Length", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "PerimeterMinusOpening", "Angle" };
                            advPropertyGrid4.IgnoredProperties = strArrays8;
                            List<string> strs7 = this.editableProperties;
                            string[] nom6 = new string[] { Resources.Nom, Resources.Couleur, Resources.Commentaire };
                            strs7.AddRange(nom6);
                            this.properties.Name = drawObject.Name;
                            this.properties.Comment = drawObject.Comment;
                            this.properties.Color = drawObject.Color;
                            this.properties.PenWidth = drawObject.PenWidth;
                            this.properties.FontSize = ((DrawNote)drawObject).FontSize;
                            this.properties.Visible = drawObject.Visible;
                            return;
                        }
                    case "Legend":
                        {
                            AdvPropertyGrid advPropertyGrid5 = this.propertyGrid;
                            string[] strArrays9 = new string[] { "Name", "Label", "Text", "Color", "Comment", "CounterSize", "GroupCount", "FillColor", "Pattern", "Shape", "SlopeFactor", "Area", "AreaMinusDeduction", "Deduction", "DeductionsCount", "Perimeter", "DropLength", "NetLength", "DropsCount", "PerimeterPlusDeduction", "CornersCount", "EndsCount", "Length", "SegmentsCount", "TeesCount", "Openings", "OpeningsCount", "PerimeterMinusOpening", "Angle" };
                            advPropertyGrid5.IgnoredProperties = strArrays9;
                            this.editableProperties.AddRange(new string[0]);
                            this.properties.FontSize = ((DrawLegend)drawObject).FontSize;
                            this.properties.PenWidth = drawObject.PenWidth;
                            this.properties.MaxRows = ((DrawLegend)drawObject).MaxRows;
                            this.properties.ShowMeasure = drawObject.ShowMeasure;
                            this.properties.Visible = drawObject.Visible;
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }

        private void UpdatePresetProperties(Preset preset, UnitScale unitScale, bool updateResultsOnly = false)
        {
            double num;
            double num1;
            double num2;
            int num3 = 0;
            if (!updateResultsOnly)
            {
                this.editableProperties.Clear();
                for (int i = 0; i < 25; i++)
                {
                    this.propertyFields[i] = null;
                }
                string[] ignoredPresetProperties = this.GetIgnoredPresetProperties(preset);
                if (this.CustomRenderingEnabled(preset))
                {
                    CustomRenderingProperties customRenderingProperties = ((DrawPolyLine)this.selectedObject).GetCustomRenderingProperties(preset.ID);
                    List<string> renderingSameProperties = this.GetRenderingSameProperties(preset, customRenderingProperties);
                    if (renderingSameProperties.Count > 0)
                    {
                        ignoredPresetProperties = this.RemoveFromIgnoredPresetProperties(ignoredPresetProperties, renderingSameProperties.ToArray());
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
                this.propertyGrid.IgnoredProperties = ignoredPresetProperties;
                foreach (PresetChoice collection in preset.Choices.Collection)
                {
                    if (num3 == 25)
                    {
                        break;
                    }
                    try
                    {
                        ExtensionChoice extensionChoice = this.extensionSupport.FindChoice(preset.CategoryName, preset.ExtensionName, collection.ChoiceName);
                        ExtensionChoiceElement extensionChoiceElement = extensionChoice.FindElement(collection.ChoiceElementName);
                        this.editableProperties.Add(collection.ChoiceCaption);
                        int num4 = num3 + 1;
                        PropertySettings propertySetting = this.SetGridPropertyDisplayName(string.Concat("PresetChoice", num4.ToString()), collection.ChoiceCaption);
                        if (propertySetting != null)
                        {
                            this.InitializeChoiceElementsUITypeEditor(propertySetting, extensionChoice);
                        }
                        this.SetPresetChoiceProperty(num3, extensionChoiceElement);
                    }
                    catch
                    {
                    }
                    num3++;
                }
                num3 = 0;
                foreach (PresetField presetField in preset.Fields.Collection)
                {
                    if (num3 == 25)
                    {
                        break;
                    }
                    this.editableProperties.Add(presetField.Caption);
                    int num5 = num3 + 1;
                    this.SetGridPropertyDisplayName(string.Concat("PresetField", num5.ToString()), presetField.Caption);
                    this.propertyFields[num3] = presetField;
                    int num6 = (presetField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency ? 2 : -1);
                    double num7 = Utilities.ConvertToDouble(presetField.Value.ToString(), num6);
                    if (presetField.FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension && preset.ScaleSystemType != unitScale.ScaleSystemType)
                    {
                        num7 = (unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num7) : UnitScale.FromFeetToMeters(num7));
                    }
                    this.SetPresetFieldProperty(num3, this.FormatField(num7.ToString(), presetField.FieldType));
                    num3++;
                }
            }
            num3 = 0;
            foreach (PresetResult presetResult in preset.Results.Collection)
            {
                if (num3 == 50)
                {
                    break;
                }
                double result = presetResult.Result;
                double num8 = Utilities.ConvertToDouble(result.ToString(), -1);
                string caption = presetResult.Caption;
                string unit = presetResult.Unit;
                string str = presetResult.Result.ToString();
                ExtensionResult.ExtensionResultTypeEnum resultType = presetResult.ResultType;
                Console.WriteLine(string.Concat(presetResult.Caption, " == ", num8));
                DBEstimatingItem estimatingItem = null;
                if (presetResult.ItemID != -1)
                {
                    estimatingItem = this.drawArea.Project.DBManagement.GetEstimatingItem(presetResult.ItemID);
                }
                bool flag = false;
                if (estimatingItem != null)
                {
                    flag = estimatingItem.MatchResultType(presetResult.ResultType);
                }
                if (flag)
                {
                    UnitScale unitScale1 = new UnitScale(1f, (estimatingItem.PurchaseUnit == "m" || estimatingItem.PurchaseUnit == "m²" || estimatingItem.PurchaseUnit == "m³" ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial), unitScale.Precision, false);
                    UnitScale.UnitSystem scaleSystemType = unitScale1.ScaleSystemType;
                    caption = estimatingItem.Description;
                    unit = estimatingItem.PurchaseUnit;
                    switch (presetResult.ResultType)
                    {
                        case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                            {
                                if (unitScale.ScaleSystemType == scaleSystemType)
                                {
                                    num = num8;
                                }
                                else
                                {
                                    num = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num8) : UnitScale.FromFeetToMeters(num8));
                                }
                                num8 = num;
                                str = unitScale1.ToLengthStringFromUnitSystem(num8, false, true, true);
                                unit = "";
                                break;
                            }
                        case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                            {
                                if (unitScale.ScaleSystemType == scaleSystemType)
                                {
                                    num1 = num8;
                                }
                                else
                                {
                                    num1 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet(num8) : UnitScale.FromSquareFeetToSquareMeters(num8));
                                }
                                num8 = num1;
                                str = unitScale1.ToAreaStringFromUnitSystem(num8, true);
                                unit = "";
                                break;
                            }
                        case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                            {
                                if (unitScale.ScaleSystemType == scaleSystemType)
                                {
                                    num2 = num8;
                                }
                                else
                                {
                                    num2 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromCubicMetersToCubicFeet(num8) : UnitScale.FromCubicFeetToCubicMeters(num8));
                                }
                                num8 = num2;
                                str = unitScale1.ToCubicStringFromUnitSystem(num8, true);
                                unit = "";
                                break;
                            }
                    }
                    resultType = ExtensionResult.ExtensionResultTypeEnum.ResultTypeCustom;
                }
                if (!presetResult.ConditionMet)
                {
                    continue;
                }
                if (!updateResultsOnly)
                {
                    this.editableProperties.Add(caption);
                }
                int num9 = num3 + 1;
                this.SetGridPropertyDisplayName(string.Concat("PresetResult", num9.ToString()), caption);
                this.SetPresetResult(num3, str, unit, resultType);
                if (updateResultsOnly)
                {
                    int num10 = num3 + 1;
                    this.propertyGrid.UpdatePropertyValue(string.Concat("PresetResult", num10.ToString()));
                }
                num3++;
            }
        }

        private void UpdateProductsGUI(CEstimatingsItemsControl itemsControl, CEstimatingItems estimatingItems)
        {
            DrawObjectGroup group = null;
            if (this.selectedObject != null)
            {
                group = this.selectedObject.Group;
            }
            if (group == null)
            {
                itemsControl.Disable();
                return;
            }
            itemsControl.UpdateGUI(estimatingItems, this.selectedObject);
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
            string propertyName = e.PropertyName;
            string str = propertyName;
            if (propertyName != null)
            {
                switch (str)
                {
                    case "Name":
                        {
                            string str1 = ((string)e.NewValue).Trim();
                            e.Cancel = str1 == string.Empty;
                            if (e.Cancel)
                            {
                                e.Message = Resources.Vous_devez_spécifier_un_nom;
                                return;
                            }
                            if (this.selectedObject.Name == str1)
                            {
                                return;
                            }
                            string empty = string.Empty;
                            e.Cancel = !this.drawArea.ValidateDrawObjectName(ref str1, ref empty, ref e.Message);
                            if (e.Cancel)
                            {
                                e.Message = string.Concat(empty, "\n", e.Message);
                                return;
                            }
                            this.selectedObject.Name = str1;
                            this.Rename(this.selectedObject);
                            this.UpdateProperty(e.PropertyName, str1);
                            return;
                        }
                    case "Label":
                        {
                            string str2 = ((string)e.NewValue).Trim();
                            if (this.selectedObject.Label == str2)
                            {
                                return;
                            }
                            this.UpdateProperty(e.PropertyName, str2);
                            return;
                        }
                    case "Text":
                        {
                            string str3 = ((string)e.NewValue).Trim();
                            if (this.selectedObject.ObjectType == "Counter")
                            {
                                str3 = Utilities.Substring(str3, 0, 5);
                            }
                            this.UpdateProperty(e.PropertyName, str3);
                            return;
                        }
                    case "Comment":
                        {
                            string str4 = ((string)e.NewValue).Trim();
                            this.UpdateProperty(e.PropertyName, str4);
                            return;
                        }
                    case "Color":
                        {
                            Color newValue = (Color)e.NewValue;
                            this.UpdateProperty(e.PropertyName, newValue);
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
                            int num = (int)e.NewValue;
                            this.UpdateProperty(e.PropertyName, num);
                            return;
                        }
                    case "FontSize":
                        {
                            int newValue1 = (int)e.NewValue;
                            this.UpdateProperty(e.PropertyName, newValue1);
                            return;
                        }
                    case "MaxRows":
                        {
                            int num1 = (int)e.NewValue;
                            this.UpdateProperty(e.PropertyName, num1);
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
                            int newValue2 = (int)e.NewValue;
                            this.UpdateProperty(e.PropertyName, newValue2);
                            return;
                        }
                    case "SlopeFactor":
                        {
                            SlopeFactor slopeFactor = (SlopeFactor)e.NewValue;
                            this.UpdateProperty(e.PropertyName, slopeFactor);
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
                            bool flag1 = (bool)e.NewValue;
                            this.UpdateProperty(e.PropertyName, flag1);
                            this.RefreshProperties();
                            return;
                        }
                    case "ShowMeasure":
                        {
                            bool flag2 = (bool)e.NewValue;
                            this.UpdateProperty(e.PropertyName, flag2);
                            return;
                        }
                    case "RenderingAngle":
                    case "RenderingOffsetX":
                    case "RenderingOffsetY":
                        {
                            int num2 = (int)e.NewValue;
                            this.UpdateProperty(e.PropertyName, num2);
                            this.drawArea.Owner.SetModified();
                            this.RefreshRenderingResults();
                            return;
                        }
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
                PresetChoice name = selectedPreset.Choices.Find(extensionChoiceElement.Parent.Name);
                if (name == null)
                {
                    e.Cancel = true;
                    e.Message = Resources.Choix_invalide;
                    return;
                }
                name.ChoiceElementName = extensionChoiceElement.Name;
                name.ChoiceElementCaption = extensionChoiceElement.Caption;
                name.Variables.Clear();
                foreach (Variable collection in extensionChoiceElement.Variables.Collection)
                {
                    name.Variables.Add(new Variable(collection.Name, collection.Value));
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

        public event OnObjectSelectedHandler OnDisplayCalculationsForAllPlans;

        public event OnObjectChangedHandler OnObjectChanged;

        public event OnObjectSelectedHandler OnObjectSelected;

        public event PresetEventHandler OnPresetCreated;

        public event PresetEventHandler OnPresetDeleted;

        public event PresetEventHandler OnPresetModified;

        public event ProductEventHandler OnProductCreated;

        public event ProductEventHandler OnProductDeleted;

        public event ProductEventHandler OnProductModified;
    }
}