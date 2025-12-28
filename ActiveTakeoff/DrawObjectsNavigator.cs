using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class DrawObjectsNavigator
	{
		public event OnNodeSelectedHandler OnNodeSelected
		{
			add
			{
				OnNodeSelectedHandler onNodeSelectedHandler = this.OnNodeSelected;
				OnNodeSelectedHandler onNodeSelectedHandler2;
				do
				{
					onNodeSelectedHandler2 = onNodeSelectedHandler;
					OnNodeSelectedHandler value2 = (OnNodeSelectedHandler)Delegate.Combine(onNodeSelectedHandler2, value);
					onNodeSelectedHandler = Interlocked.CompareExchange<OnNodeSelectedHandler>(ref this.OnNodeSelected, value2, onNodeSelectedHandler2);
				}
				while (onNodeSelectedHandler != onNodeSelectedHandler2);
			}
			remove
			{
				OnNodeSelectedHandler onNodeSelectedHandler = this.OnNodeSelected;
				OnNodeSelectedHandler onNodeSelectedHandler2;
				do
				{
					onNodeSelectedHandler2 = onNodeSelectedHandler;
					OnNodeSelectedHandler value2 = (OnNodeSelectedHandler)Delegate.Remove(onNodeSelectedHandler2, value);
					onNodeSelectedHandler = Interlocked.CompareExchange<OnNodeSelectedHandler>(ref this.OnNodeSelected, value2, onNodeSelectedHandler2);
				}
				while (onNodeSelectedHandler != onNodeSelectedHandler2);
			}
		}

		public event OnLayerChangedHandler OnLayerChanged
		{
			add
			{
				OnLayerChangedHandler onLayerChangedHandler = this.OnLayerChanged;
				OnLayerChangedHandler onLayerChangedHandler2;
				do
				{
					onLayerChangedHandler2 = onLayerChangedHandler;
					OnLayerChangedHandler value2 = (OnLayerChangedHandler)Delegate.Combine(onLayerChangedHandler2, value);
					onLayerChangedHandler = Interlocked.CompareExchange<OnLayerChangedHandler>(ref this.OnLayerChanged, value2, onLayerChangedHandler2);
				}
				while (onLayerChangedHandler != onLayerChangedHandler2);
			}
			remove
			{
				OnLayerChangedHandler onLayerChangedHandler = this.OnLayerChanged;
				OnLayerChangedHandler onLayerChangedHandler2;
				do
				{
					onLayerChangedHandler2 = onLayerChangedHandler;
					OnLayerChangedHandler value2 = (OnLayerChangedHandler)Delegate.Remove(onLayerChangedHandler2, value);
					onLayerChangedHandler = Interlocked.CompareExchange<OnLayerChangedHandler>(ref this.OnLayerChanged, value2, onLayerChangedHandler2);
				}
				while (onLayerChangedHandler != onLayerChangedHandler2);
			}
		}

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

		public event OnPlanSelectedHandler OnPlanSelected
		{
			add
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanSelected;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Combine(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanSelected, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
			remove
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanSelected;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Remove(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanSelected, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
		}

		public event OnPlanRenameHandler OnPlanRename
		{
			add
			{
				OnPlanRenameHandler onPlanRenameHandler = this.OnPlanRename;
				OnPlanRenameHandler onPlanRenameHandler2;
				do
				{
					onPlanRenameHandler2 = onPlanRenameHandler;
					OnPlanRenameHandler value2 = (OnPlanRenameHandler)Delegate.Combine(onPlanRenameHandler2, value);
					onPlanRenameHandler = Interlocked.CompareExchange<OnPlanRenameHandler>(ref this.OnPlanRename, value2, onPlanRenameHandler2);
				}
				while (onPlanRenameHandler != onPlanRenameHandler2);
			}
			remove
			{
				OnPlanRenameHandler onPlanRenameHandler = this.OnPlanRename;
				OnPlanRenameHandler onPlanRenameHandler2;
				do
				{
					onPlanRenameHandler2 = onPlanRenameHandler;
					OnPlanRenameHandler value2 = (OnPlanRenameHandler)Delegate.Remove(onPlanRenameHandler2, value);
					onPlanRenameHandler = Interlocked.CompareExchange<OnPlanRenameHandler>(ref this.OnPlanRename, value2, onPlanRenameHandler2);
				}
				while (onPlanRenameHandler != onPlanRenameHandler2);
			}
		}

		public event OnGroupChangeLayerHandler OnGroupChangeLayer
		{
			add
			{
				OnGroupChangeLayerHandler onGroupChangeLayerHandler = this.OnGroupChangeLayer;
				OnGroupChangeLayerHandler onGroupChangeLayerHandler2;
				do
				{
					onGroupChangeLayerHandler2 = onGroupChangeLayerHandler;
					OnGroupChangeLayerHandler value2 = (OnGroupChangeLayerHandler)Delegate.Combine(onGroupChangeLayerHandler2, value);
					onGroupChangeLayerHandler = Interlocked.CompareExchange<OnGroupChangeLayerHandler>(ref this.OnGroupChangeLayer, value2, onGroupChangeLayerHandler2);
				}
				while (onGroupChangeLayerHandler != onGroupChangeLayerHandler2);
			}
			remove
			{
				OnGroupChangeLayerHandler onGroupChangeLayerHandler = this.OnGroupChangeLayer;
				OnGroupChangeLayerHandler onGroupChangeLayerHandler2;
				do
				{
					onGroupChangeLayerHandler2 = onGroupChangeLayerHandler;
					OnGroupChangeLayerHandler value2 = (OnGroupChangeLayerHandler)Delegate.Remove(onGroupChangeLayerHandler2, value);
					onGroupChangeLayerHandler = Interlocked.CompareExchange<OnGroupChangeLayerHandler>(ref this.OnGroupChangeLayer, value2, onGroupChangeLayerHandler2);
				}
				while (onGroupChangeLayerHandler != onGroupChangeLayerHandler2);
			}
		}

		private void LoadResources()
		{
		}

		private void InitializeCombo()
		{
			this.cbPlans.DrawItem += this.combo_DrawItem;
			this.cbPlans.SelectedIndexChanged += this.combo_SelectedIndexChanged;
			this.cbPlans.DropDownClosed += this.comboBox_DropDownClosed;
			this.cbPlans.DropDown += this.comboBox_DropDown;
			this.cbPlans.KeyDown += this.cbPlans_KeyDown;
		}

		private void InitializeTextBox()
		{
			this.txtPlanName.Enter += this.txtPlanName_Enter;
			this.txtPlanName.KeyDown += this.txtPlanName_KeyDown;
			this.txtPlanName.Validating += new CancelEventHandler(this.txtPlanName_Validating);
		}

		private void InitializeTreeView()
		{
			this.tree.AfterNodeSelect += this.tree_AfterNodeSelect;
			this.tree.DoubleClick += this.tree_DoubleClick;
			this.tree.AfterCheck += this.tree_AfterCheck;
			this.tree.BeforeCellEdit += this.tree_BeforeCellEdit;
			this.tree.CellEditEnding += this.tree_CellEditEnding;
			this.tree.NodeDragFeedback += this.tree_NodeDragFeedback;
			this.tree.BeforeNodeDrop += this.tree_BeforeNodeDrop;
			this.tree.AfterNodeDrop += this.tree_AfterNodeDrop;
		}

		public DrawObjectsNavigator(Project project, DrawingArea drawArea, ComboBoxEx cbPlans, TextBoxEx txtPlanName, AdvTree tree, ThumbnailPanel previewPanel, Control parentControl)
		{
			this.project = project;
			this.drawArea = drawArea;
			this.cbPlans = cbPlans;
			this.txtPlanName = txtPlanName;
			this.tree = tree;
			this.previewPanel = previewPanel;
			this.parentControl = parentControl;
			this.enabled = true;
			this.InitializeCombo();
			this.InitializeTextBox();
			this.InitializeTreeView();
			this.LoadResources();
		}

		private void ResetLayers()
		{
			for (int i = 0; i < this.tree.Nodes.Count; i++)
			{
				this.tree.Nodes[i].Tag = i;
			}
		}

		private Image GetDrawObjectImage(DrawObject drawObject)
		{
			return this.drawArea.GetDrawObjectIcon(drawObject);
		}

		private string GetDrawObjectBasicInfo(DrawObject drawObject)
		{
			string result = string.Empty;
			Plan activePlan = this.drawArea.ActivePlan;
			if (activePlan != null)
			{
				GroupStats groupStats = GroupUtilities.ComputeGroupStats(activePlan, drawObject, activePlan.UnitScale.ScaleSystemType, true, "");
				string objectType;
				switch (objectType = drawObject.ObjectType)
				{
				case "Line":
					result = this.drawArea.ToLengthStringFromUnitSystem(groupStats.Perimeter, false);
					break;
				case "Rectangle":
					result = "";
					break;
				case "Area":
					result = this.drawArea.ToAreaStringFromUnitSystem(groupStats.AreaMinusDeduction);
					break;
				case "Perimeter":
					result = this.drawArea.ToLengthStringFromUnitSystem(groupStats.NetLength, false);
					break;
				case "Counter":
					result = this.drawArea.ToUnitString(groupStats.GroupCount);
					break;
				case "Angle":
					result = this.drawArea.ToAngleString(((DrawAngle)drawObject).Angle, ((DrawAngle)drawObject).AngleType);
					break;
				case "Note":
					result = "";
					break;
				}
			}
			return result;
		}

		private static Plan CastItemToPlan(object item)
		{
			Plan result;
			try
			{
				result = (Plan)((Utilities.ItemData)item).Data;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static DrawObject CastNodeToObject(Node node)
		{
			DrawObject result;
			try
			{
				result = (DrawObject)node.Tag;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static int CastNodeToLayerIndex(Node node)
		{
			int result;
			try
			{
				result = (int)node.Tag;
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		private void UpdateObjectNode(Node node, DrawObject drawObject)
		{
			try
			{
				((DrawObjectIconIndicator)node.Cells[0].HostedControl).ParentObject = drawObject;
				node.Cells[1].Text = drawObject.Name;
				node.Cells[2].Text = this.GetDrawObjectBasicInfo(drawObject);
				node.Cells[4].Checked = drawObject.Visible;
			}
			catch
			{
			}
		}

		private Node FormatLayerNode(Layer layer, int layerIndex)
		{
			Node node = new Node();
			node.Cells.Clear();
			Cell cell = new Cell(layer.Name);
			cell.Editable = false;
			cell.Images.Image = Resources.layer_small;
			cell.ImageAlignment = eCellPartAlignment.Default;
			cell.TextMarkupEnabled = false;
			node.Cells.Add(cell);
			node.Tag = layerIndex;
			node.DragDropEnabled = false;
			return node;
		}

		private Node FormatObjectNode(DrawObject drawObject)
		{
			Node node = new Node();
			node.Cells.Clear();
			Cell cell = new Cell();
			DrawObjectIconIndicator drawObjectIconIndicator = new DrawObjectIconIndicator(drawObject, this.drawArea);
			drawObjectIconIndicator.DoubleClick += this.tree_DoubleClick;
			drawObjectIconIndicator.Size = new Size(12, 18);
			cell.HostedControl = drawObjectIconIndicator;
			cell.Editable = false;
			node.Cells.Add(cell);
			cell = new Cell(drawObject.Name);
			cell.Editable = (drawObject.ObjectType != "Legend");
			cell.EditorType = eCellEditorType.Default;
			cell.TextMarkupEnabled = false;
			node.Cells.Add(cell);
			cell = new Cell(this.GetDrawObjectBasicInfo(drawObject));
			cell.StyleNormal = new ElementStyle(Color.DarkSlateGray);
			cell.Editable = false;
			node.Cells.Add(cell);
			cell = new Cell();
			node.Cells.Add(cell);
			cell = new Cell();
			cell.CheckBoxVisible = true;
			cell.Checked = drawObject.Visible;
			cell.Editable = false;
			node.Cells.Add(cell);
			node.Tag = drawObject;
			node.DragDropEnabled = (drawObject.ObjectType != "Legend");
			return node;
		}

		private void RemoveDeletedObjectsInTree(Node parentNode)
		{
			for (int i = parentNode.Nodes.Count - 1; i >= 0; i--)
			{
				this.RemoveDeletedObjectsInTree(parentNode.Nodes[i]);
				DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(parentNode.Nodes[i]);
				if (drawObject != null)
				{
					bool flag;
					if (drawObject.IsPartOfGroup())
					{
						flag = !GroupUtilities.GroupExists(this.drawArea.ActivePlan, drawObject.GroupID);
					}
					else
					{
						flag = (this.drawArea.GetObjectByID(this.drawArea.ActivePlan, drawObject.ID) == null);
					}
					if (flag)
					{
						this.FreeHostedControl(parentNode.Nodes[i]);
						parentNode.Nodes.RemoveAt(i);
					}
				}
			}
		}

		private void SelectDefaultNode()
		{
			if (this.tree.Nodes.Count > 0)
			{
				this.tree.SelectedIndex = 0;
			}
		}

		private void UpdateLayerProperty(int layerIndex, string propertyName, object value)
		{
			if (this.OnLayerChanged != null)
			{
				this.OnLayerChanged(layerIndex, propertyName, value);
			}
		}

		private void UpdateProperty(DrawObject drawObject, string propertyName, object value)
		{
			GroupUtilities.UpdateGroupProperty(this.project, this.drawArea, drawObject, propertyName, value);
			if (this.OnObjectChanged != null)
			{
				this.OnObjectChanged(drawObject, propertyName, value);
			}
		}

		private Node FindObjectNode(Node parentNode, DrawObject drawObject)
		{
			foreach (object obj in parentNode.Nodes)
			{
				Node node = (Node)obj;
				DrawObject drawObject2 = DrawObjectsNavigator.CastNodeToObject(node);
				if (drawObject2 != null && drawObject2.HasSameGroupOrID(drawObject))
				{
					return node;
				}
			}
			return null;
		}

		private Node FindObjectNode(DrawObject drawObject)
		{
			for (int i = 0; i < this.tree.Nodes.Count; i++)
			{
				Node node = this.FindObjectNode(this.tree.Nodes[i], drawObject);
				if (node != null)
				{
					return node;
				}
			}
			return null;
		}

		private Node FindLayerNode(int layerIndex)
		{
			foreach (object obj in this.tree.Nodes)
			{
				Node node = (Node)obj;
				if ((int)node.Tag == layerIndex)
				{
					return node;
				}
			}
			return null;
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
				this.tree.Enabled = this.enabled;
				this.cbPlans.Enabled = (this.Enabled || this.project.Plans.Count > 0);
				this.EditingPlanName = false;
				this.thumbnailActive = false;
				this.previewPanel.Visible = false;
				if (!this.enabled)
				{
					this.Clear();
					this.cbPlans.Items.Clear();
				}
			}
		}

		public bool EditingPlanName
		{
			get
			{
				return this.editingPlanName;
			}
			set
			{
				this.exitNow = true;
				this.editingPlanName = value;
				this.cbPlans.Visible = !this.editingPlanName;
				this.txtPlanName.Visible = this.editingPlanName;
				if (this.editingPlanName)
				{
					this.txtPlanName.Text = this.drawArea.ActivePlan.Name;
				}
				Utilities.SetObjectFocus(this.editingPlanName ? this.txtPlanName : this.cbPlans);
				this.exitNow = false;
			}
		}

		public void RefreshListOfPlans(Plan selectedPlan)
		{
			this.exitNow = true;
			int num = 0;
			this.cbPlans.Items.Clear();
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				this.cbPlans.Items.Add(new Utilities.ItemData(plan.Name, plan));
				if (selectedPlan != null && plan.Equals(selectedPlan))
				{
					this.cbPlans.SelectedIndex = num;
				}
				num++;
			}
			this.exitNow = false;
		}

		public int SelectedLayerIndex
		{
			get
			{
				int result;
				try
				{
					result = (int)this.tree.SelectedNode.Parent.Tag;
				}
				catch
				{
					result = -1;
				}
				return result;
			}
		}

		public Layer SelectedLayer
		{
			get
			{
				Layer result;
				try
				{
					result = this.drawArea.Layers[this.SelectedLayerIndex];
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public DrawObject SelectedObject
		{
			get
			{
				DrawObject result;
				try
				{
					result = (DrawObject)this.tree.SelectedNode.Tag;
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		private void FreeHostedControl(Node node)
		{
			try
			{
				((DrawObjectIconIndicator)node.Cells[0].HostedControl).ParentObject = null;
				((DrawObjectIconIndicator)node.Cells[0].HostedControl).DrawArea = null;
			}
			catch
			{
			}
		}

		private void FreeHostedControls()
		{
			for (int i = this.tree.Nodes.Count - 1; i >= 0; i--)
			{
				for (int j = this.tree.Nodes[i].Nodes.Count - 1; j >= 0; j--)
				{
					Node node = this.tree.Nodes[i].Nodes[j];
					this.FreeHostedControl(node);
				}
			}
		}

		public void Clear()
		{
			try
			{
				this.FreeHostedControls();
				for (int i = this.tree.Nodes.Count - 1; i >= 0; i--)
				{
					this.tree.Nodes.RemoveAt(i);
				}
				this.tree.Nodes.Clear();
				this.tree.Refresh();
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		public void ExpandAll()
		{
			this.tree.ExpandAll();
			this.SelectDefaultNode();
			this.tree.RecalcLayout();
		}

		public void Reload(Plan plan)
		{
			int num = 0;
			ArrayList arrayList = new ArrayList();
			this.tree.BeginUpdate();
			this.Clear();
			if (plan == null)
			{
				this.tree.EndUpdate();
				return;
			}
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				this.AddLayer(layer, num);
				for (int i = layer.DrawingObjects.Count - 1; i >= 0; i--)
				{
					DrawObject drawObject = layer.DrawingObjects[i];
					bool flag = drawObject.DeductionParentID != -1;
					if (!flag)
					{
						if (drawObject.IsPartOfGroup())
						{
							if (!arrayList.Contains(drawObject.GroupID))
							{
								arrayList.Add(drawObject.GroupID);
							}
							else
							{
								flag = true;
							}
						}
						if (!flag)
						{
							this.AddObject(num, drawObject, false);
						}
					}
				}
				num++;
			}
			this.ExpandAll();
			this.tree.Refresh();
			this.tree.EndUpdate();
			this.tree.RecalcLayout();
			arrayList.Clear();
			arrayList = null;
		}

		public void AddLayer(Layer layer, int layerIndex)
		{
			int index = (layerIndex > this.tree.Nodes.Count) ? this.tree.Nodes.Count : layerIndex;
			Node node = this.FormatLayerNode(layer, layerIndex);
			this.tree.Nodes.Insert(index, node);
			node.Expand();
			this.ResetLayers();
		}

		public void RenameLayer(int layerIndex, string newName, ref string oldName)
		{
			Node node = this.FindLayerNode(layerIndex);
			if (node != null)
			{
				oldName = node.Text;
				node.Text = newName;
			}
		}

		public void RemoveLayer(int layerIndex)
		{
			Node node = this.FindLayerNode(layerIndex);
			if (node != null)
			{
				this.tree.Nodes.Remove(node);
				this.SelectDefaultNode();
				this.tree.RecalcLayout();
				this.ResetLayers();
			}
		}

		public void AddObject(int layerIndex, DrawObject drawObject, bool selectObject)
		{
			Node node = this.FindLayerNode(layerIndex);
			if (node != null)
			{
				Node node2 = this.FormatObjectNode(drawObject);
				node.Nodes.Add(node2);
				if (selectObject)
				{
					this.tree.SelectedNode = node2;
				}
				node.Nodes.Sort(new DrawObjectsNavigator.NodeSorter());
				this.tree.RecalcLayout();
			}
		}

		public void RemoveDeletedObjects()
		{
			for (int i = this.tree.Nodes.Count - 1; i >= 0; i--)
			{
				this.RemoveDeletedObjectsInTree(this.tree.Nodes[i]);
			}
			this.SelectDefaultNode();
			this.tree.RecalcLayout();
		}

		public void InsertObject(Node parentNode, int index, DrawObject drawObject)
		{
			parentNode.Nodes.Insert(index, this.FormatObjectNode(drawObject));
		}

		public void Edit(DrawObject drawObject)
		{
			Node node = this.FindObjectNode(drawObject);
			if (node != null)
			{
				try
				{
					node.BeginEdit(1);
				}
				catch
				{
				}
			}
		}

		public void Refresh(DrawObject drawObject)
		{
			this.UpdateObjectNode(this.FindObjectNode(drawObject), drawObject);
		}

		public void RefreshAll()
		{
			for (int i = this.tree.Nodes.Count - 1; i >= 0; i--)
			{
				for (int j = this.tree.Nodes[i].Nodes.Count - 1; j >= 0; j--)
				{
					Node node = this.tree.Nodes[i].Nodes[j];
					DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(node);
					if (drawObject != null)
					{
						this.UpdateObjectNode(node, drawObject);
					}
					else
					{
						this.FreeHostedControl(this.tree.Nodes[j]);
						this.tree.Nodes.RemoveAt(j);
					}
				}
			}
			this.tree.RecalcLayout();
		}

		public void ResetItems()
		{
			for (int i = 0; i < this.tree.Nodes.Count; i++)
			{
				Node node = this.tree.Nodes[i];
				for (int j = 0; j < node.Nodes.Count; j++)
				{
					Node node2 = node.Nodes[j];
					DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(node2);
					if (drawObject != null)
					{
						this.UpdateObjectNode(node2, DrawObjectsNavigator.CastNodeToObject(node2));
						node2.Tag = drawObject;
					}
				}
			}
		}

		public void ReplaceObject(DrawObject drawObject)
		{
			for (int i = 0; i < this.tree.Nodes.Count; i++)
			{
				Node node = this.tree.Nodes[i];
				for (int j = 0; j < node.Nodes.Count; j++)
				{
					Node node2 = node.Nodes[j];
					DrawObject drawObject2 = DrawObjectsNavigator.CastNodeToObject(node2);
					if (drawObject2 != null && drawObject2.ID == drawObject.ID)
					{
						node2.Tag = drawObject;
						this.UpdateObjectNode(node2, DrawObjectsNavigator.CastNodeToObject(node2));
					}
				}
			}
		}

		public void ToggleAllVisibility(bool visible)
		{
			Utilities.SuspendDrawing(this.drawArea.Owner);
			foreach (object obj in this.tree.Nodes)
			{
				Node node = (Node)obj;
				foreach (object obj2 in node.Nodes)
				{
					Node node2 = (Node)obj2;
					DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(node2);
					if (drawObject != null)
					{
						try
						{
							node2.Cells[4].Checked = visible;
						}
						catch
						{
						}
						this.UpdateProperty(drawObject, "Visible", visible);
					}
				}
			}
			Utilities.ResumeDrawing(this.drawArea.Owner);
			this.drawArea.Refresh();
		}

		public void ToggleVisibility(DrawObject drawObject)
		{
			if (drawObject == null)
			{
				return;
			}
			Node node = this.FindObjectNode(drawObject);
			if (node == null)
			{
				return;
			}
			int num = -1;
			Layer layer = null;
			try
			{
				num = (int)node.Parent.Tag;
				layer = this.drawArea.GetLayer(num);
			}
			catch
			{
			}
			if (layer == null)
			{
				return;
			}
			bool flag = !layer.Visible;
			bool flag2 = !drawObject.Visible;
			if (flag || flag2)
			{
				string objet_invisible = Resources.Objet_invisible;
				string l_objet_que_vous_avez_sélectionné_est_présentement_invisible = Resources.L_objet_que_vous_avez_sélectionné_est_présentement_invisible;
				string rendre_visible = Resources.Rendre_visible;
				if (Utilities.DisplayWarningQuestionCustom(objet_invisible, l_objet_que_vous_avez_sélectionné_est_présentement_invisible, null, rendre_visible) == DialogResult.Yes)
				{
					if (flag)
					{
						layer.Visible = true;
						this.UpdateLayerProperty(num, "Visible", true);
					}
					if (flag2)
					{
						try
						{
							this.tree.SelectedNode.Cells[4].Checked = true;
						}
						catch
						{
						}
						this.UpdateProperty(drawObject, "Visible", true);
					}
				}
			}
		}

		private void SetThumbnailPosition()
		{
			try
			{
				if (((Bar)this.parentControl).Docked)
				{
					if (this.parentControl.Parent.Right - this.parentControl.Parent.Width - this.previewPanel.Size.Width > 0)
					{
						this.previewPanel.Location = new Point(this.parentControl.Parent.Right - this.parentControl.Parent.Width - this.previewPanel.Size.Width, this.parentControl.Top + this.previewPanel.Size.Height + 20);
					}
					else
					{
						this.previewPanel.Location = new Point(this.parentControl.Parent.Left + this.parentControl.Parent.Width, this.parentControl.Top + this.previewPanel.Size.Height + 20);
					}
				}
				else if (this.parentControl.PointToScreen(this.parentControl.Location).X - this.previewPanel.Size.Width > 0)
				{
					this.previewPanel.Location = new Point(this.parentControl.PointToScreen(this.parentControl.Location).X - this.previewPanel.Size.Width, this.parentControl.PointToScreen(this.parentControl.Location).Y + 52);
				}
				else
				{
					this.previewPanel.Location = new Point(this.parentControl.PointToScreen(this.parentControl.Location).X + this.parentControl.Width + 6, this.parentControl.PointToScreen(this.parentControl.Location).Y + 52);
				}
			}
			catch
			{
			}
		}

		private void UpdateThumbnail(Plan plan)
		{
			if (plan == null)
			{
				this.previewPanel.Visible = false;
				return;
			}
			if (!plan.Thumbnail.IsValid())
			{
				plan.CreateThumbnail(true);
			}
			if (!plan.Thumbnail.IsValid())
			{
				this.previewPanel.Visible = false;
				return;
			}
			this.previewPanel.Plan = plan;
			this.previewPanel.FlatStyle = FlatStyle.Flat;
			this.previewPanel.FlatAppearance.BorderSize = 1;
			this.previewPanel.ThumbnailMarging = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.previewPanel.ThumbnailPadding = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.previewPanel.DisplayShadow = true;
			this.previewPanel.ThumbnailSize = new Size(this.previewPanel.Plan.Thumbnail.ThumbImage.Width, this.previewPanel.Plan.Thumbnail.ThumbImage.Height);
			this.previewPanel.RecalcLayout();
			this.previewPanel.BringToFront();
			this.SetThumbnailPosition();
			this.previewPanel.Visible = true;
		}

		private void comboBox_DropDown(object sender, EventArgs e)
		{
			this.thumbnailActive = true;
		}

		private void comboBox_DropDownClosed(object sender, EventArgs e)
		{
			this.thumbnailActive = false;
			this.previewPanel.Visible = false;
		}

		private void combo_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			e.DrawBackground();
			Plan plan = DrawObjectsNavigator.CastItemToPlan(this.cbPlans.Items[e.Index]);
			if (plan == null)
			{
				return;
			}
			if (this.thumbnailActive && (e.State & DrawItemState.Selected) == DrawItemState.Selected && (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.None)
			{
				this.UpdateThumbnail(plan);
			}
			Image plan_16x = Resources.plan_16x16;
			e.Graphics.DrawImage(plan_16x, new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 2, plan_16x.Width, plan_16x.Height));
			string name = ((e.State & DrawItemState.Selected) != DrawItemState.None) ? "HighlightText" : "WindowText";
			e.Graphics.DrawString(plan.Name, this.cbPlans.Font, new SolidBrush(Color.FromArgb(255, Color.FromName(name))), new RectangleF(26f, (float)e.Bounds.Y, (float)(e.Bounds.Width - 26), (float)e.Bounds.Height));
		}

		private void combo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			Plan plan = DrawObjectsNavigator.CastItemToPlan(this.cbPlans.SelectedItem);
			if (plan == null)
			{
				return;
			}
			Application.DoEvents();
			if (this.OnPlanSelected != null)
			{
				this.OnPlanSelected(plan);
			}
		}

		private void cbPlans_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F2)
			{
				this.EditingPlanName = true;
				e.Handled = true;
			}
		}

		private bool RenamePlan(Plan plan)
		{
			this.txtPlanName.Text = this.txtPlanName.Text.Trim();
			if (this.txtPlanName.Text == plan.Name)
			{
				return true;
			}
			if (this.txtPlanName.Text == string.Empty)
			{
				this.txtPlanName.Text = plan.Name;
				return true;
			}
			string empty = string.Empty;
			string empty2 = string.Empty;
			string text = this.txtPlanName.Text;
			if (!this.drawArea.ValidatePlanName(ref text, ref empty, ref empty2))
			{
				Utilities.DisplayError(empty, empty2);
				return false;
			}
			if (this.OnPlanRename != null)
			{
				this.OnPlanRename(plan, text);
			}
			return true;
		}

		private void txtPlanName_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText(this.txtPlanName);
		}

		private void txtPlanName_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return)
			{
				Plan plan = DrawObjectsNavigator.CastItemToPlan(this.cbPlans.SelectedItem);
				if (plan != null && this.RenamePlan(plan))
				{
					this.EditingPlanName = false;
				}
				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}
			if (keyCode != Keys.Escape)
			{
				return;
			}
			this.txtPlanName.Text = string.Empty;
			this.EditingPlanName = false;
			e.Handled = true;
			e.SuppressKeyPress = true;
		}

		private void txtPlanName_Validating(object sender, CancelEventArgs e)
		{
			if (!this.Enabled || this.exitNow)
			{
				return;
			}
			Plan plan = DrawObjectsNavigator.CastItemToPlan(this.cbPlans.SelectedItem);
			if (plan != null)
			{
				e.Cancel = !this.RenamePlan(plan);
				if (!e.Cancel)
				{
					this.EditingPlanName = false;
					return;
				}
				Utilities.SetObjectFocus(this.txtPlanName);
			}
		}

		private void tree_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
		{
			if (this.OnNodeSelected != null)
			{
				if (this.SelectedObject != null)
				{
					this.OnNodeSelected(this.SelectedObject);
					return;
				}
				if (this.SelectedLayer != null)
				{
					this.OnNodeSelected(this.SelectedLayer);
					return;
				}
				this.OnNodeSelected(null);
			}
		}

		private void tree_BeforeCellEdit(object sender, CellEditEventArgs e)
		{
			DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(e.Cell.Parent);
			e.Cancel = (drawObject == null);
		}

		private void tree_CellEditEnding(object sender, CellEditEventArgs e)
		{
			DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(e.Cell.Parent);
			e.Cancel = (drawObject == null);
			if (e.Cancel)
			{
				return;
			}
			e.NewText = e.NewText.Trim();
			if (e.NewText == e.Cell.Text)
			{
				return;
			}
			if (e.NewText == "")
			{
				e.NewText = e.Cell.Text;
				return;
			}
			string empty = string.Empty;
			string empty2 = string.Empty;
			e.Cancel = !this.drawArea.ValidateDrawObjectName(ref e.NewText, ref empty, ref empty2);
			if (e.Cancel)
			{
				Utilities.DisplayError(empty, empty2);
				return;
			}
			this.UpdateProperty(drawObject, "Name", e.NewText);
		}

		private void tree_AfterCheck(object sender, AdvTreeCellEventArgs e)
		{
			DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(e.Cell.Parent);
			if (drawObject == null)
			{
				return;
			}
			this.UpdateProperty(drawObject, "Visible", e.Cell.Checked);
		}

		private void tree_DoubleClick(object sender, EventArgs e)
		{
			if (this.OnObjectSelected != null && this.SelectedObject != null)
			{
				this.ToggleVisibility(this.SelectedObject);
				this.OnObjectSelected(this.SelectedObject);
			}
		}

		private int QueryNodeLayer(Node node)
		{
			int result;
			try
			{
				int num = DrawObjectsNavigator.CastNodeToLayerIndex(node);
				if (num != -1)
				{
					result = num;
				}
				else
				{
					num = DrawObjectsNavigator.CastNodeToLayerIndex(node.Parent);
					if (num != -1)
					{
						result = num;
					}
					else
					{
						num = DrawObjectsNavigator.CastNodeToLayerIndex(node.Parent.Parent);
						if (num != -1)
						{
							result = num;
						}
						else
						{
							result = -1;
						}
					}
				}
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		private void tree_NodeDragFeedback(object sender, TreeDragFeedbackEventArgs e)
		{
			try
			{
				if (DrawObjectsNavigator.CastNodeToObject(e.DragNode) == null)
				{
					e.AllowDrop = false;
				}
				else
				{
					int num = this.QueryNodeLayer(e.DragNode.Parent);
					if (num == -1)
					{
						e.AllowDrop = false;
					}
					else
					{
						int num2 = this.QueryNodeLayer(e.ParentNode);
						if (num2 == -1)
						{
							e.AllowDrop = false;
						}
						else if (num == num2)
						{
							e.AllowDrop = false;
						}
						else
						{
							DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(e.ParentNode);
							if (drawObject != null)
							{
								e.ParentNode = e.ParentNode.Parent;
							}
						}
					}
				}
			}
			catch
			{
				e.AllowDrop = false;
			}
		}

		private void tree_BeforeNodeDrop(object sender, TreeDragDropEventArgs e)
		{
		}

		private void tree_AfterNodeDrop(object sender, TreeDragDropEventArgs e)
		{
			DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(e.Node);
			if (drawObject == null)
			{
				e.Cancel = true;
				return;
			}
			int num = DrawObjectsNavigator.CastNodeToLayerIndex(e.OldParentNode);
			if (num == -1)
			{
				e.Cancel = true;
				return;
			}
			int num2 = DrawObjectsNavigator.CastNodeToLayerIndex(e.NewParentNode);
			if (num2 == -1)
			{
				e.Cancel = true;
				return;
			}
			if (num == num2)
			{
				e.Cancel = true;
				return;
			}
			e.InsertPosition = 0;
			if (this.OnGroupChangeLayer != null)
			{
				this.OnGroupChangeLayer(drawObject, num, num2);
			}
		}

		private OnNodeSelectedHandler OnNodeSelected;

		private OnLayerChangedHandler OnLayerChanged;

		private OnObjectSelectedHandler OnObjectSelected;

		private OnObjectChangedHandler OnObjectChanged;

		private OnPlanSelectedHandler OnPlanSelected;

		private OnPlanRenameHandler OnPlanRename;

		private OnGroupChangeLayerHandler OnGroupChangeLayer;

		private bool enabled;

		private bool editingPlanName;

		private bool thumbnailActive;

		private Project project;

		private DrawingArea drawArea;

		private ComboBoxEx cbPlans;

		private TextBoxEx txtPlanName;

		private AdvTree tree;

		private ThumbnailPanel previewPanel;

		private Control parentControl;

		private bool exitNow;

		private class NodeSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				int result;
				try
				{
					Node node = x as Node;
					Node node2 = y as Node;
					DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(node);
					DrawObject drawObject2 = DrawObjectsNavigator.CastNodeToObject(node2);
					if (drawObject.ObjectType != drawObject2.ObjectType)
					{
						result = StringLogicalComparer.Compare(drawObject.ObjectSortOrder.ToString(), drawObject2.ObjectSortOrder.ToString());
					}
					else
					{
						result = StringLogicalComparer.Compare(drawObject.Name, drawObject2.Name);
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}

			public NodeSorter()
			{
			}
		}
	}
}
