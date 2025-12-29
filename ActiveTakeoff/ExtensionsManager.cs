using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using eStyleTextTrimming = DevComponents.DotNetBar.eStyleTextTrimming;

namespace QuoterPlan
{
    public class ExtensionsManager : BaseUserControl
    {
        private const int maxPropertiesInResume = 4;

        private const int categoryMarginTop = 1;

        private const int categoryMarginBottom = 4;

        private const int templateMarginTop = 2;

        private DrawObjectGroup @group;

        private ExtensionsSupport extensionSupport;

        private UnitScale unitScale;

        private IContainer components;

        private LabelEx lblNoTemplate;

        private ButtonX btAddTemplate;

        private ButtonX btModifyTemplate;

        private ButtonX btRemoveTemplate;

        private AdvTree lstTemplates;

        private NodeConnector nodeConnector3;

        private ElementStyle elementStyle8;

        private Node node1;

        private ButtonX btSelectTemplate;

        public DrawObjectGroup Group
        {
            get
            {
                return this.@group;
            }
        }

        public HelpUtilities HelpUtilities
        {
            get;
            set;
        }

        private double Opacity
        {
            get
            {
                return base.FindForm().Opacity;
            }
        }

        public ExtensionsManager()
        {
            this.InitializeComponent();
            this.LoadResources();
            this.InitializeFonts();
            this.InitializeList();
            this.EnableGUI();
        }

        private void btAddTemplate_Click(object sender, EventArgs e)
        {
            this.PresetAdd();
        }

        private void btModifyTemplate_Click(object sender, EventArgs e)
        {
            this.PresetModify(this.CastNodeToObject(this.lstTemplates.SelectedNode));
        }

        private void btRemoveTemplate_Click(object sender, EventArgs e)
        {
            this.PresetRemove(this.CastNodeToObject(this.lstTemplates.SelectedNode));
        }

        private void btSelectTemplate_Click(object sender, EventArgs e)
        {
            this.PresetAdd();
        }

        private Preset CastNodeToObject(Node node)
        {
            Preset tag;
            try
            {
                tag = (Preset)node.Tag;
            }
            catch
            {
                tag = null;
            }
            return tag;
        }

        public void Disable()
        {
            this.@group = null;
            this.InitializeGroup();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EnableGUI()
        {
            bool objectType = false;
            bool flag = false;
            if (this.@group != null)
            {
                objectType = this.@group.ObjectType != "Rectangle";
                flag = (!objectType ? false : this.Group.Presets.Count > 0);
            }
            this.lblNoTemplate.ForeColor = (objectType ? Color.Black : Color.LightGray);
            this.btSelectTemplate.Enabled = objectType;
            this.lblNoTemplate.Visible = !flag;
            this.btSelectTemplate.Visible = !flag;
            this.lstTemplates.Visible = flag;
            this.btAddTemplate.Visible = flag;
            this.btModifyTemplate.Visible = flag;
            this.btModifyTemplate.Enabled = (!flag ? false : this.lstTemplates.SelectedNode != null);
            this.btRemoveTemplate.Visible = flag;
            this.btRemoveTemplate.Enabled = (!flag ? false : this.lstTemplates.SelectedNode != null);
        }

        private Node FormatNode(Preset preset)
        {
            Node node = new Node();
            node.Cells.Clear();
            this.lstTemplates.Styles["categoryStyle"].TextTrimming = eStyleTextTrimming.EllipsisWord;
            string displayName = preset.DisplayName;
            displayName = Utilities.ShortenString(displayName, this.lstTemplates.Font, this.lstTemplates.Width - 5, TextFormatFlags.WordEllipsis);
            Cell cell = new Cell(displayName, this.lstTemplates.Styles["categoryStyle"]);
            node.Cells.Add(cell);
            string str = "";
            int num = 0;
            for (int i = 0; i < preset.Choices.Count; i++)
            {
                displayName = string.Concat(preset.Choices[i].ChoiceCaption, " = ", preset.Choices[i].ChoiceElementCaption);
                displayName = Utilities.ShortenString(displayName, this.lstTemplates.Font, this.lstTemplates.Width - 5, TextFormatFlags.WordEllipsis);
                str = string.Concat(str, (num == 0 ? "" : "\n"), displayName);
                num++;
                if (num == 4)
                {
                    break;
                }
            }
            if (num < 4)
            {
                for (int j = 0; j < preset.Fields.Count; j++)
                {
                    displayName = preset.Fields[j].Value.ToString();
                    if (preset.Fields[j].FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension)
                    {
                        double num1 = Utilities.ConvertToDouble(displayName, -1);
                        if (preset.ScaleSystemType != this.unitScale.ScaleSystemType)
                        {
                            num1 = (this.unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num1) : UnitScale.FromFeetToMeters(num1));
                        }
                        displayName = this.unitScale.ToLengthStringFromUnitSystem(num1, false, false, true);
                    }
                    displayName = string.Concat(preset.Fields[j].Caption, " = ", displayName);
                    if (num + 1 == 4 && j < preset.Fields.Count)
                    {
                        displayName = string.Concat(displayName, " { ... }");
                    }
                    displayName = Utilities.ShortenString(displayName, this.lstTemplates.Font, this.lstTemplates.Width - 5, TextFormatFlags.WordEllipsis);
                    str = string.Concat(str, (num == 0 ? "" : "\n"), displayName);
                    num++;
                    if (num == 4 && j < preset.Fields.Count)
                    {
                        break;
                    }
                }
            }
            this.lstTemplates.Styles["templateStyle"].TextTrimming = eStyleTextTrimming.EllipsisWord;
            Cell cell1 = new Cell(str, this.lstTemplates.Styles["templateStyle"]);
            node.Cells.Add(cell1);
            node.Tag = preset;
            return node;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ExtensionsManager));
            this.btAddTemplate = new ButtonX();
            this.btModifyTemplate = new ButtonX();
            this.btRemoveTemplate = new ButtonX();
            this.lstTemplates = new AdvTree();
            this.node1 = new Node();
            this.nodeConnector3 = new NodeConnector();
            this.elementStyle8 = new ElementStyle();
            this.btSelectTemplate = new ButtonX();
            this.lblNoTemplate = new LabelEx();
            ((ISupportInitialize)this.lstTemplates).BeginInit();
            base.SuspendLayout();
            this.btAddTemplate.AccessibleRole = AccessibleRole.PushButton;
            this.btAddTemplate.ColorTable = eButtonColor.OrangeWithBackground;
            this.btAddTemplate.Image = (Image)componentResourceManager.GetObject("btAddTemplate.Image");
            this.btAddTemplate.ImagePosition = eImagePosition.Top;
            componentResourceManager.ApplyResources(this.btAddTemplate, "btAddTemplate");
            this.btAddTemplate.Name = "btAddTemplate";
            this.btAddTemplate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btAddTemplate.Click += new EventHandler(this.btAddTemplate_Click);
            this.btModifyTemplate.AccessibleRole = AccessibleRole.PushButton;
            this.btModifyTemplate.ColorTable = eButtonColor.OrangeWithBackground;
            this.btModifyTemplate.Image = (Image)componentResourceManager.GetObject("btModifyTemplate.Image");
            this.btModifyTemplate.ImagePosition = eImagePosition.Top;
            componentResourceManager.ApplyResources(this.btModifyTemplate, "btModifyTemplate");
            this.btModifyTemplate.Name = "btModifyTemplate";
            this.btModifyTemplate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btModifyTemplate.Click += new EventHandler(this.btModifyTemplate_Click);
            this.btRemoveTemplate.AccessibleRole = AccessibleRole.PushButton;
            this.btRemoveTemplate.ColorTable = eButtonColor.OrangeWithBackground;
            this.btRemoveTemplate.Image = (Image)componentResourceManager.GetObject("btRemoveTemplate.Image");
            this.btRemoveTemplate.ImagePosition = eImagePosition.Top;
            componentResourceManager.ApplyResources(this.btRemoveTemplate, "btRemoveTemplate");
            this.btRemoveTemplate.Name = "btRemoveTemplate";
            this.btRemoveTemplate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btRemoveTemplate.Click += new EventHandler(this.btRemoveTemplate_Click);
            this.lstTemplates.AccessibleRole = AccessibleRole.Outline;
            this.lstTemplates.AllowDrop = true;
            this.lstTemplates.BackColor = SystemColors.Window;
            this.lstTemplates.BackgroundStyle.Class = "TreeBorderKey";
            this.lstTemplates.BackgroundStyle.CornerType = eCornerType.Square;
            this.lstTemplates.DoubleClickTogglesNode = false;
            this.lstTemplates.DragDropEnabled = false;
            this.lstTemplates.DragDropNodeCopyEnabled = false;
            this.lstTemplates.ExpandButtonSize = new Size(0, 0);
            this.lstTemplates.ExpandButtonType = eExpandButtonType.Triangle;
            this.lstTemplates.ExpandWidth = 0;
            this.lstTemplates.HotTracking = true;
            this.lstTemplates.HScrollBarVisible = false;
            this.lstTemplates.Indent = 0;
            this.lstTemplates.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            componentResourceManager.ApplyResources(this.lstTemplates, "lstTemplates");
            this.lstTemplates.MultiNodeDragCountVisible = false;
            this.lstTemplates.MultiNodeDragDropAllowed = false;
            this.lstTemplates.Name = "lstTemplates";
            this.lstTemplates.NodeHorizontalSpacing = 0;
            this.lstTemplates.Nodes.AddRange(new Node[] { this.node1 });
            this.lstTemplates.NodesConnector = this.nodeConnector3;
            this.lstTemplates.NodeSpacing = 0;
            this.lstTemplates.PathSeparator = ";";
            this.lstTemplates.Styles.Add(this.elementStyle8);
            this.lstTemplates.View = eView.Tile;
            this.lstTemplates.KeyDown += new KeyEventHandler(this.lstTemplates_KeyDown);
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            componentResourceManager.ApplyResources(this.node1, "node1");
            this.nodeConnector3.LineColor = SystemColors.ControlText;
            this.elementStyle8.CornerType = eCornerType.Square;
            this.elementStyle8.Name = "elementStyle8";
            this.elementStyle8.TextColor = SystemColors.ControlText;
            this.btSelectTemplate.AccessibleRole = AccessibleRole.PushButton;
            this.btSelectTemplate.ColorTable = eButtonColor.OrangeWithBackground;
            componentResourceManager.ApplyResources(this.btSelectTemplate, "btSelectTemplate");
            this.btSelectTemplate.Name = "btSelectTemplate";
            this.btSelectTemplate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btSelectTemplate.Click += new EventHandler(this.btSelectTemplate_Click);
            this.lblNoTemplate.BackColor = Color.Transparent;
            this.lblNoTemplate.ForeColor = Color.Black;
            componentResourceManager.ApplyResources(this.lblNoTemplate, "lblNoTemplate");
            this.lblNoTemplate.Name = "lblNoTemplate";
            this.lblNoTemplate.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            componentResourceManager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Window;
            base.Controls.Add(this.btSelectTemplate);
            base.Controls.Add(this.lblNoTemplate);
            base.Controls.Add(this.btModifyTemplate);
            base.Controls.Add(this.lstTemplates);
            base.Controls.Add(this.btRemoveTemplate);
            base.Controls.Add(this.btAddTemplate);
            base.Name = "ExtensionsManager";
            base.Resize += new EventHandler(this.TemplatesManager_Resize);
            ((ISupportInitialize)this.lstTemplates).EndInit();
            base.ResumeLayout(false);
        }

        private void InitializeFonts()
        {
            this.Font = Utilities.GetDefaultFont();
            this.lstTemplates.Font = Utilities.GetDefaultFont();
            this.lblNoTemplate.Font = Utilities.GetDefaultFont();
            this.btSelectTemplate.Font = Utilities.GetDefaultFont();
            this.btAddTemplate.Font = Utilities.GetDefaultFont();
            this.btModifyTemplate.Font = Utilities.GetDefaultFont();
            this.btRemoveTemplate.Font = Utilities.GetDefaultFont();
        }

        private void InitializeGroup()
        {
            this.lstTemplates.BeginUpdate();
            this.lstTemplates.Nodes.Clear();
            if (this.@group != null)
            {
                foreach (Preset collection in this.@group.Presets.Collection)
                {
                    this.InsertNode(collection, false);
                }
            }
            if (this.lstTemplates.Nodes.Count > 0)
            {
                this.lstTemplates.SelectedIndex = 0;
            }
            this.lstTemplates.EndUpdate();
            this.EnableGUI();
        }

        private void InitializeList()
        {
            ElementStyle elementStyle = new ElementStyle()
            {
                Name = "nodeStyle",
                TextAlignment = eStyleTextAlignment.Far,
                TextLineAlignment = eStyleTextAlignment.Center,
                WordWrap = false,
                TextTrimming = eStyleTextTrimming.EllipsisWord
            };
            this.lstTemplates.NodeStyle = elementStyle;
            this.lstTemplates.Styles.Add(elementStyle);
            ElementStyle elementStyle1 = new ElementStyle()
            {
                TextColor = Color.DarkSlateGray,
                Name = "categoryStyle",
                MarginTop = 1,
                MarginLeft = 4,
                WordWrap = false
            };
            this.lstTemplates.Styles.Add(elementStyle1);
            ElementStyle elementStyle2 = new ElementStyle()
            {
                MarginTop = 2,
                MarginLeft = 4,
                TextColor = Color.Black,
                WordWrap = true,
                Name = "templateStyle"
            };
            this.lstTemplates.Styles.Add(elementStyle2);
            this.lstTemplates.NodeDoubleClick += new TreeNodeMouseEventHandler(this.lstTemplates_NodeDoubleClick);
        }

        private void InsertNode(Preset preset, bool selectNode)
        {
            Node node = this.FormatNode(preset);
            this.lstTemplates.Nodes.Add(node);
            if (selectNode)
            {
                node.EnsureVisible();
                this.lstTemplates.RecalcLayout();
                this.lstTemplates.SelectedNode = node;
            }
        }

        private void LoadResources()
        {
        }

        private void lstTemplates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }
            this.PresetRemove(this.CastNodeToObject(this.lstTemplates.SelectedNode));
        }

        private void lstTemplates_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            this.PresetModify(this.CastNodeToObject(e.Node));
        }

        private void PresetAdd()
        {
            if (this.OnShowModalWindow != null)
            {
                this.OnShowModalWindow(this, new EventArgs());
            }
            Preset preset = new Preset(this.unitScale.ScaleSystemType);
            try
            {
                using (PresetEditForm presetEditForm = new PresetEditForm(this.@group, preset, this.extensionSupport, this.unitScale, true))
                {
                    presetEditForm.HelpUtilities = this.HelpUtilities;
                    presetEditForm.HelpContextString = "PresetEditForm";
                    presetEditForm.Opacity = (this.Opacity != 1 ? 1 : presetEditForm.Opacity);
                    presetEditForm.ShowDialog(this);
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                return;
            }
            if (preset.Dirty)
            {
                preset.SynchWithTemplate(this.extensionSupport);
                preset.SetCustomRendering();
                preset.Dirty = false;
                this.@group.Presets.Add(preset);
                this.InsertNode(preset, true);
                if (this.OnPresetCreated != null)
                {
                    this.OnPresetCreated(preset);
                }
                this.EnableGUI();
                Utilities.SetObjectFocus(this.lstTemplates);
                return;
            }
        }

        private void PresetModify(Preset preset)
        {
            if (preset == null)
            {
                return;
            }
            if (this.OnShowModalWindow != null)
            {
                this.OnShowModalWindow(this, new EventArgs());
            }
            string displayName = preset.DisplayName;
            string str = preset.DisplayName;
            try
            {
                using (PresetEditForm presetEditForm = new PresetEditForm(this.@group, preset, this.extensionSupport, this.unitScale, false))
                {
                    presetEditForm.HelpUtilities = this.HelpUtilities;
                    presetEditForm.HelpContextString = "PresetEditForm";
                    presetEditForm.Opacity = (this.Opacity != 1 ? 1 : presetEditForm.Opacity);
                    presetEditForm.ShowDialog(this);
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                return;
            }
            if (preset.Dirty)
            {
                preset.SynchWithTemplate(this.extensionSupport);
                preset.Dirty = false;
                str = preset.DisplayName;
                if (this.OnPresetRenamed != null && displayName != str)
                {
                    this.OnPresetRenamed(preset, displayName, str);
                }
                if (this.OnPresetModified != null)
                {
                    this.OnPresetModified(preset);
                }
                this.UpdateNode(this.lstTemplates.SelectedNode, preset);
                Utilities.SetObjectFocus(this.lstTemplates);
                return;
            }
        }

        private void PresetRemove(Preset preset)
        {
            if (preset == null)
            {
                return;
            }
            if (this.OnShowModalWindow != null)
            {
                this.OnShowModalWindow(this, new EventArgs());
            }
            if (Utilities.DisplayDeleteConfirmation(Resources.Supprimer_la_configuration_d_extension, Resources.Cette_configuration_d_extension_sera_supprimée_du_groupe) == DialogResult.No)
            {
                return;
            }
            if (this.OnPresetDeleted != null)
            {
                this.OnPresetDeleted(preset);
            }
            this.RemoveNode(this.lstTemplates.SelectedNode, true);
            this.@group.Presets.Remove(preset);
            this.EnableGUI();
            if (this.lstTemplates.Nodes.Count > 0)
            {
                Utilities.SetObjectFocus(this.lstTemplates);
            }
            if (this.OnPresetAfterDeleted != null)
            {
                this.OnPresetAfterDeleted(this, new EventArgs());
            }
        }

        public void RecalcLayout()
        {
            LabelEx point = this.lblNoTemplate;
            int width = base.ClientSize.Width;
            Size size = this.lblNoTemplate.Size;
            int num = (width - size.Width) / 2;
            int height = base.ClientSize.Height;
            Size size1 = this.lblNoTemplate.Size;
            point.Location = new Point(num, (height - size1.Height) / 2 - this.btSelectTemplate.Height);
            ButtonX buttonX = this.btSelectTemplate;
            int width1 = base.ClientSize.Width;
            Size size2 = this.btSelectTemplate.Size;
            buttonX.Location = new Point((width1 - size2.Width) / 2, this.lblNoTemplate.Top + this.lblNoTemplate.Height + 2);
            this.lstTemplates.Location = new Point(8, 10);
            AdvTree advTree = this.lstTemplates;
            Size clientSize = base.ClientSize;
            Size clientSize1 = base.ClientSize;
            advTree.Size = new Size(clientSize.Width - this.btAddTemplate.Width - 25, clientSize1.Height - 16);
            ButtonX point1 = this.btAddTemplate;
            Size clientSize2 = base.ClientSize;
            point1.Location = new Point(clientSize2.Width - this.btAddTemplate.Width - 8, 8);
            ButtonX buttonX1 = this.btModifyTemplate;
            Size clientSize3 = base.ClientSize;
            buttonX1.Location = new Point(clientSize3.Width - this.btModifyTemplate.Width - 8, this.btAddTemplate.Height + 16);
            ButtonX point2 = this.btRemoveTemplate;
            Size size3 = base.ClientSize;
            point2.Location = new Point(size3.Width - this.btRemoveTemplate.Width - 8, this.btAddTemplate.Height + this.btModifyTemplate.Height + 24);
            using (Graphics graphic = base.CreateGraphics())
            {
                SizeF sizeF = graphic.MeasureString(new string('\n', 5), this.lstTemplates.Font, this.lstTemplates.Width - 5);
                int height1 = sizeF.ToSize().Height;
                height1 = height1 + 1 + 2 + 4;
                this.lstTemplates.TileSize = new Size(this.lstTemplates.Width - 5, height1);
            }
            foreach (Node node in this.lstTemplates.Nodes)
            {
                if (node.Tag == null)
                {
                    continue;
                }
                this.UpdateNode(node, this.CastNodeToObject(node));
            }
        }

        private void RemoveNode(Node node, bool selectNode)
        {
            this.lstTemplates.Nodes.Remove(node);
            if (selectNode && this.lstTemplates.Nodes.Count > 0)
            {
                this.lstTemplates.SelectedIndex = 0;
            }
        }

        public void SelectGroup(DrawObjectGroup group, ExtensionsSupport extensionSupport, UnitScale unitScale)
        {
            this.@group = group;
            this.extensionSupport = extensionSupport;
            this.unitScale = unitScale;
            this.InitializeGroup();
        }

        private void TemplatesManager_Resize(object sender, EventArgs e)
        {
            this.RecalcLayout();
        }

        private void UpdateNode(Node node, Preset preset)
        {
            Node node1 = this.FormatNode(preset);
            node.Cells[0].Text = node1.Cells[0].Text;
            node.Cells[1].Text = node1.Cells[1].Text;
        }

        public event EventHandler OnPresetAfterDeleted;

        public event PresetEventHandler OnPresetCreated;

        public event PresetEventHandler OnPresetDeleted;

        public event PresetEventHandler OnPresetModified;

        public event PresetRenameEventHandler OnPresetRenamed;

        public event EventHandler OnShowModalWindow;
    }
}