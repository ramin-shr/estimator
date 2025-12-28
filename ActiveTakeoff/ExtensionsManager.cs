using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class ExtensionsManager : BaseUserControl
	{
		public event EventHandler OnShowModalWindow
		{
			add
			{
				EventHandler eventHandler = this.OnShowModalWindow;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnShowModalWindow, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnShowModalWindow;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnShowModalWindow, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
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

		public event PresetRenameEventHandler OnPresetRenamed
		{
			add
			{
				PresetRenameEventHandler presetRenameEventHandler = this.OnPresetRenamed;
				PresetRenameEventHandler presetRenameEventHandler2;
				do
				{
					presetRenameEventHandler2 = presetRenameEventHandler;
					PresetRenameEventHandler value2 = (PresetRenameEventHandler)Delegate.Combine(presetRenameEventHandler2, value);
					presetRenameEventHandler = Interlocked.CompareExchange<PresetRenameEventHandler>(ref this.OnPresetRenamed, value2, presetRenameEventHandler2);
				}
				while (presetRenameEventHandler != presetRenameEventHandler2);
			}
			remove
			{
				PresetRenameEventHandler presetRenameEventHandler = this.OnPresetRenamed;
				PresetRenameEventHandler presetRenameEventHandler2;
				do
				{
					presetRenameEventHandler2 = presetRenameEventHandler;
					PresetRenameEventHandler value2 = (PresetRenameEventHandler)Delegate.Remove(presetRenameEventHandler2, value);
					presetRenameEventHandler = Interlocked.CompareExchange<PresetRenameEventHandler>(ref this.OnPresetRenamed, value2, presetRenameEventHandler2);
				}
				while (presetRenameEventHandler != presetRenameEventHandler2);
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

		public event EventHandler OnPresetAfterDeleted
		{
			add
			{
				EventHandler eventHandler = this.OnPresetAfterDeleted;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnPresetAfterDeleted, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnPresetAfterDeleted;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnPresetAfterDeleted, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		private double Opacity
		{
			get
			{
				return base.FindForm().Opacity;
			}
		}

		public HelpUtilities HelpUtilities
		{
			[CompilerGenerated]
			get
			{
				return this.<HelpUtilities>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HelpUtilities>k__BackingField = value;
			}
		}

		private void LoadResources()
		{
		}

		private void InitializeList()
		{
			ElementStyle elementStyle = new ElementStyle();
			elementStyle.Name = "nodeStyle";
			elementStyle.TextAlignment = eStyleTextAlignment.Far;
			elementStyle.TextLineAlignment = eStyleTextAlignment.Center;
			elementStyle.WordWrap = false;
			elementStyle.TextTrimming = DevComponents.DotNetBar.eStyleTextTrimming.EllipsisWord;
			this.lstTemplates.NodeStyle = elementStyle;
			this.lstTemplates.Styles.Add(elementStyle);
			ElementStyle elementStyle2 = new ElementStyle();
			elementStyle2.TextColor = Color.DarkSlateGray;
			elementStyle2.Name = "categoryStyle";
			elementStyle2.MarginTop = 1;
			elementStyle2.MarginLeft = 4;
			elementStyle2.WordWrap = false;
			this.lstTemplates.Styles.Add(elementStyle2);
			ElementStyle elementStyle3 = new ElementStyle();
			elementStyle3.MarginTop = 2;
			elementStyle3.MarginLeft = 4;
			elementStyle3.TextColor = Color.Black;
			elementStyle3.WordWrap = true;
			elementStyle3.Name = "templateStyle";
			this.lstTemplates.Styles.Add(elementStyle3);
			this.lstTemplates.NodeDoubleClick += this.lstTemplates_NodeDoubleClick;
		}

		private void InitializeGroup()
		{
			this.lstTemplates.BeginUpdate();
			this.lstTemplates.Nodes.Clear();
			if (this.group != null)
			{
				foreach (object obj in this.group.Presets.Collection)
				{
					Preset preset = (Preset)obj;
					this.InsertNode(preset, false);
				}
			}
			if (this.lstTemplates.Nodes.Count > 0)
			{
				this.lstTemplates.SelectedIndex = 0;
			}
			this.lstTemplates.EndUpdate();
			this.EnableGUI();
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

		public ExtensionsManager()
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.InitializeList();
			this.EnableGUI();
		}

		private void EnableGUI()
		{
			bool flag = false;
			bool flag2 = false;
			if (this.group != null)
			{
				flag = (this.group.ObjectType != "Rectangle");
				flag2 = (flag && this.Group.Presets.Count > 0);
			}
			this.lblNoTemplate.ForeColor = (flag ? Color.Black : Color.LightGray);
			this.btSelectTemplate.Enabled = flag;
			this.lblNoTemplate.Visible = !flag2;
			this.btSelectTemplate.Visible = !flag2;
			this.lstTemplates.Visible = flag2;
			this.btAddTemplate.Visible = flag2;
			this.btModifyTemplate.Visible = flag2;
			this.btModifyTemplate.Enabled = (flag2 && this.lstTemplates.SelectedNode != null);
			this.btRemoveTemplate.Visible = flag2;
			this.btRemoveTemplate.Enabled = (flag2 && this.lstTemplates.SelectedNode != null);
		}

		private Preset CastNodeToObject(Node node)
		{
			Preset result;
			try
			{
				result = (Preset)node.Tag;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private Node FormatNode(Preset preset)
		{
			Node node = new Node();
			node.Cells.Clear();
			this.lstTemplates.Styles["categoryStyle"].TextTrimming = DevComponents.DotNetBar.eStyleTextTrimming.EllipsisWord;
			string text = preset.DisplayName;
			text = Utilities.ShortenString(text, this.lstTemplates.Font, this.lstTemplates.Width - 5, TextFormatFlags.WordEllipsis);
			Cell cell = new Cell(text, this.lstTemplates.Styles["categoryStyle"]);
			node.Cells.Add(cell);
			string text2 = "";
			int num = 0;
			for (int i = 0; i < preset.Choices.Count; i++)
			{
				text = preset.Choices[i].ChoiceCaption + " = " + preset.Choices[i].ChoiceElementCaption;
				text = Utilities.ShortenString(text, this.lstTemplates.Font, this.lstTemplates.Width - 5, TextFormatFlags.WordEllipsis);
				text2 = text2 + ((num == 0) ? "" : "\n") + text;
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
					text = preset.Fields[j].Value.ToString();
					if (preset.Fields[j].FieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension)
					{
						double value = Utilities.ConvertToDouble(text, -1);
						if (preset.ScaleSystemType != this.unitScale.ScaleSystemType)
						{
							value = ((this.unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(value) : UnitScale.FromFeetToMeters(value));
						}
						text = this.unitScale.ToLengthStringFromUnitSystem(value, false, false, true);
					}
					text = preset.Fields[j].Caption + " = " + text;
					if (num + 1 == 4 && j < preset.Fields.Count)
					{
						text += " { ... }";
					}
					text = Utilities.ShortenString(text, this.lstTemplates.Font, this.lstTemplates.Width - 5, TextFormatFlags.WordEllipsis);
					text2 = text2 + ((num == 0) ? "" : "\n") + text;
					num++;
					if (num == 4 && j < preset.Fields.Count)
					{
						break;
					}
				}
			}
			this.lstTemplates.Styles["templateStyle"].TextTrimming = DevComponents.DotNetBar.eStyleTextTrimming.EllipsisWord;
			Cell cell2 = new Cell(text2, this.lstTemplates.Styles["templateStyle"]);
			node.Cells.Add(cell2);
			node.Tag = preset;
			return node;
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
				using (PresetEditForm presetEditForm = new PresetEditForm(this.group, preset, this.extensionSupport, this.unitScale, true))
				{
					presetEditForm.HelpUtilities = this.HelpUtilities;
					presetEditForm.HelpContextString = "PresetEditForm";
					presetEditForm.Opacity = ((this.Opacity != 1.0) ? 1.0 : presetEditForm.Opacity);
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
				this.group.Presets.Add(preset);
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
			string displayName2 = preset.DisplayName;
			try
			{
				using (PresetEditForm presetEditForm = new PresetEditForm(this.group, preset, this.extensionSupport, this.unitScale, false))
				{
					presetEditForm.HelpUtilities = this.HelpUtilities;
					presetEditForm.HelpContextString = "PresetEditForm";
					presetEditForm.Opacity = ((this.Opacity != 1.0) ? 1.0 : presetEditForm.Opacity);
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
				displayName2 = preset.DisplayName;
				if (this.OnPresetRenamed != null && displayName != displayName2)
				{
					this.OnPresetRenamed(preset, displayName, displayName2);
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
			string supprimer_la_configuration_d_extension = Resources.Supprimer_la_configuration_d_extension;
			string cette_configuration_d_extension_sera_supprimée_du_groupe = Resources.Cette_configuration_d_extension_sera_supprimée_du_groupe;
			if (Utilities.DisplayDeleteConfirmation(supprimer_la_configuration_d_extension, cette_configuration_d_extension_sera_supprimée_du_groupe) == DialogResult.No)
			{
				return;
			}
			if (this.OnPresetDeleted != null)
			{
				this.OnPresetDeleted(preset);
			}
			this.RemoveNode(this.lstTemplates.SelectedNode, true);
			this.group.Presets.Remove(preset);
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

		private void UpdateNode(Node node, Preset preset)
		{
			Node node2 = this.FormatNode(preset);
			node.Cells[0].Text = node2.Cells[0].Text;
			node.Cells[1].Text = node2.Cells[1].Text;
		}

		private void RemoveNode(Node node, bool selectNode)
		{
			this.lstTemplates.Nodes.Remove(node);
			if (selectNode && this.lstTemplates.Nodes.Count > 0)
			{
				this.lstTemplates.SelectedIndex = 0;
			}
		}

		public void Disable()
		{
			this.group = null;
			this.InitializeGroup();
		}

		public void SelectGroup(DrawObjectGroup group, ExtensionsSupport extensionSupport, UnitScale unitScale)
		{
			this.group = group;
			this.extensionSupport = extensionSupport;
			this.unitScale = unitScale;
			this.InitializeGroup();
		}

		public DrawObjectGroup Group
		{
			get
			{
				return this.group;
			}
		}

		private void btSelectTemplate_Click(object sender, EventArgs e)
		{
			this.PresetAdd();
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

		private void lstTemplates_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
		{
			this.PresetModify(this.CastNodeToObject(e.Node));
		}

		private void lstTemplates_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Delete)
			{
				return;
			}
			this.PresetRemove(this.CastNodeToObject(this.lstTemplates.SelectedNode));
		}

		public void RecalcLayout()
		{
			this.lblNoTemplate.Location = new Point((base.ClientSize.Width - this.lblNoTemplate.Size.Width) / 2, (base.ClientSize.Height - this.lblNoTemplate.Size.Height) / 2 - this.btSelectTemplate.Height);
			this.btSelectTemplate.Location = new Point((base.ClientSize.Width - this.btSelectTemplate.Size.Width) / 2, this.lblNoTemplate.Top + this.lblNoTemplate.Height + 2);
			this.lstTemplates.Location = new Point(8, 10);
			this.lstTemplates.Size = new Size(base.ClientSize.Width - this.btAddTemplate.Width - 25, base.ClientSize.Height - 16);
			this.btAddTemplate.Location = new Point(base.ClientSize.Width - this.btAddTemplate.Width - 8, 8);
			this.btModifyTemplate.Location = new Point(base.ClientSize.Width - this.btModifyTemplate.Width - 8, this.btAddTemplate.Height + 16);
			this.btRemoveTemplate.Location = new Point(base.ClientSize.Width - this.btRemoveTemplate.Width - 8, this.btAddTemplate.Height + this.btModifyTemplate.Height + 24);
			using (Graphics graphics = base.CreateGraphics())
			{
				int num = graphics.MeasureString(new string('\n', 5), this.lstTemplates.Font, this.lstTemplates.Width - 5).ToSize().Height;
				num++;
				num += 2;
				num += 4;
				this.lstTemplates.TileSize = new Size(this.lstTemplates.Width - 5, num);
			}
			foreach (object obj in this.lstTemplates.Nodes)
			{
				Node node = (Node)obj;
				if (node.Tag != null)
				{
					this.UpdateNode(node, this.CastNodeToObject(node));
				}
			}
		}

		private void TemplatesManager_Resize(object sender, EventArgs e)
		{
			this.RecalcLayout();
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
			this.btAddTemplate.Click += this.btAddTemplate_Click;
			this.btModifyTemplate.AccessibleRole = AccessibleRole.PushButton;
			this.btModifyTemplate.ColorTable = eButtonColor.OrangeWithBackground;
			this.btModifyTemplate.Image = (Image)componentResourceManager.GetObject("btModifyTemplate.Image");
			this.btModifyTemplate.ImagePosition = eImagePosition.Top;
			componentResourceManager.ApplyResources(this.btModifyTemplate, "btModifyTemplate");
			this.btModifyTemplate.Name = "btModifyTemplate";
			this.btModifyTemplate.Style = eDotNetBarStyle.StyleManagerControlled;
			this.btModifyTemplate.Click += this.btModifyTemplate_Click;
			this.btRemoveTemplate.AccessibleRole = AccessibleRole.PushButton;
			this.btRemoveTemplate.ColorTable = eButtonColor.OrangeWithBackground;
			this.btRemoveTemplate.Image = (Image)componentResourceManager.GetObject("btRemoveTemplate.Image");
			this.btRemoveTemplate.ImagePosition = eImagePosition.Top;
			componentResourceManager.ApplyResources(this.btRemoveTemplate, "btRemoveTemplate");
			this.btRemoveTemplate.Name = "btRemoveTemplate";
			this.btRemoveTemplate.Style = eDotNetBarStyle.StyleManagerControlled;
			this.btRemoveTemplate.Click += this.btRemoveTemplate_Click;
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
			this.lstTemplates.Nodes.AddRange(new Node[]
			{
				this.node1
			});
			this.lstTemplates.NodesConnector = this.nodeConnector3;
			this.lstTemplates.NodeSpacing = 0;
			this.lstTemplates.PathSeparator = ";";
			this.lstTemplates.Styles.Add(this.elementStyle8);
			this.lstTemplates.View = eView.Tile;
			this.lstTemplates.KeyDown += this.lstTemplates_KeyDown;
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
			this.btSelectTemplate.Click += this.btSelectTemplate_Click;
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
			base.Resize += this.TemplatesManager_Resize;
			((ISupportInitialize)this.lstTemplates).EndInit();
			base.ResumeLayout(false);
		}

		private const int maxPropertiesInResume = 4;

		private const int categoryMarginTop = 1;

		private const int categoryMarginBottom = 4;

		private const int templateMarginTop = 2;

		private EventHandler OnShowModalWindow;

		private PresetEventHandler OnPresetCreated;

		private PresetRenameEventHandler OnPresetRenamed;

		private PresetEventHandler OnPresetModified;

		private PresetEventHandler OnPresetDeleted;

		private EventHandler OnPresetAfterDeleted;

		private DrawObjectGroup group;

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

		[CompilerGenerated]
		private HelpUtilities <HelpUtilities>k__BackingField;
	}
}
