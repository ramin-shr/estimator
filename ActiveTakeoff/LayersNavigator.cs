using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class LayersNavigator
	{
		public event OnLayerSelectedHandler OnLayerSelected
		{
			add
			{
				OnLayerSelectedHandler onLayerSelectedHandler = this.OnLayerSelected;
				OnLayerSelectedHandler onLayerSelectedHandler2;
				do
				{
					onLayerSelectedHandler2 = onLayerSelectedHandler;
					OnLayerSelectedHandler value2 = (OnLayerSelectedHandler)Delegate.Combine(onLayerSelectedHandler2, value);
					onLayerSelectedHandler = Interlocked.CompareExchange<OnLayerSelectedHandler>(ref this.OnLayerSelected, value2, onLayerSelectedHandler2);
				}
				while (onLayerSelectedHandler != onLayerSelectedHandler2);
			}
			remove
			{
				OnLayerSelectedHandler onLayerSelectedHandler = this.OnLayerSelected;
				OnLayerSelectedHandler onLayerSelectedHandler2;
				do
				{
					onLayerSelectedHandler2 = onLayerSelectedHandler;
					OnLayerSelectedHandler value2 = (OnLayerSelectedHandler)Delegate.Remove(onLayerSelectedHandler2, value);
					onLayerSelectedHandler = Interlocked.CompareExchange<OnLayerSelectedHandler>(ref this.OnLayerSelected, value2, onLayerSelectedHandler2);
				}
				while (onLayerSelectedHandler != onLayerSelectedHandler2);
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

		private void LoadResources()
		{
		}

		private void InitializeTreeView()
		{
			this.list.CellEditEnding += this.list_CellEditEnding;
			this.list.AfterNodeSelect += this.list_AfterNodeSelect;
		}

		public LayersNavigator(Project project, DrawingArea drawArea, AdvTree list)
		{
			this.project = project;
			this.drawArea = drawArea;
			this.list = list;
			this.enabled = true;
			this.InitializeTreeView();
			this.LoadResources();
		}

		private void SetActiveLayer(int layerIndex, bool forceNodeSelection)
		{
			this.drawArea.ActiveLayerIndex = layerIndex;
			Console.WriteLine("Setting ActiveLayerIndex to [" + layerIndex + "]");
			if (forceNodeSelection)
			{
				Node node = this.FindLayerNode(layerIndex);
				if (node != null)
				{
					this.exitNow = true;
					this.list.SelectedNode = node;
					this.exitNow = false;
				}
			}
			if (this.OnLayerSelected != null)
			{
				this.OnLayerSelected(layerIndex);
			}
		}

		private void RevertToDefaultLayer()
		{
			string calque_invalide = Resources.Calque_invalide;
			string bascule_vers_le_calque_par_défaut = Resources.Bascule_vers_le_calque_par_défaut;
			Utilities.DisplayError(calque_invalide, bascule_vers_le_calque_par_défaut);
			this.ResetLayers();
			this.SetActiveLayer(0, true);
		}

		private int CastNodeToLayerIndex(Node node)
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

		private void ResetLayers()
		{
			int i = this.list.Nodes.Count - 1;
			int num = 0;
			while (i >= 0)
			{
				this.list.Nodes[i].Tag = num;
				this.list.Nodes[i].Cells[0].HostedItem.Tag = num;
				this.list.Nodes[i].Cells[2].HostedItem.Tag = num;
				i--;
				num++;
			}
		}

		private Node FormatLayerNode(Layer layer, int layerIndex)
		{
			Node node = new Node();
			node.Cells.Clear();
			Cell cell = new Cell();
			CheckBoxItem checkBoxItem = new CheckBoxItem("chk" + layer.Name);
			checkBoxItem.TextVisible = false;
			checkBoxItem.CheckState = (layer.Visible ? CheckState.Checked : CheckState.Unchecked);
			checkBoxItem.Tag = layerIndex;
			cell.HostedItem = checkBoxItem;
			cell.Editable = false;
			checkBoxItem.CheckedChanged += this.list_VisibleEvent;
			node.Cells.Add(cell);
			Cell cell2 = new Cell(layer.Name);
			cell2.Editable = true;
			cell2.EditorType = eCellEditorType.Default;
			cell2.TextMarkupEnabled = false;
			node.Cells.Add(cell2);
			Cell cell3 = new Cell();
			SliderItem sliderItem = new SliderItem("slider" + layer.Name);
			sliderItem.LabelVisible = false;
			sliderItem.TrackMarker = false;
			sliderItem.Minimum = 0;
			sliderItem.Maximum = 225;
			sliderItem.Tag = layerIndex;
			layer.Opacity = ((layer.Opacity < 0 || layer.Opacity > 225) ? 150 : layer.Opacity);
			sliderItem.Value = layer.Opacity;
			cell3.HostedItem = sliderItem;
			cell3.Editable = false;
			sliderItem.ValueChanged += this.list_SliderEvent;
			node.Cells.Add(cell3);
			node.Tag = layerIndex;
			return node;
		}

		private void UpdateProperty(int layerIndex, string propertyName, object value)
		{
			Layer layer = this.drawArea.GetLayer(layerIndex);
			if (layer == null)
			{
				return;
			}
			if (propertyName != null)
			{
				if (!(propertyName == "Name"))
				{
					if (!(propertyName == "Visible"))
					{
						if (!(propertyName == "Opacity"))
						{
							return;
						}
						layer.Opacity = Utilities.ConvertToInt(value);
						using (IEnumerator enumerator = layer.DrawingObjects.Collection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								DrawObject drawObject = (DrawObject)obj;
								drawObject.Opacity = layer.Opacity;
							}
							goto IL_CD;
						}
						return;
					}
					else
					{
						layer.Visible = Utilities.ConvertToBoolean(value, true);
					}
				}
				else
				{
					layer.PrevName = layer.Name;
					layer.Name = value.ToString();
					this.Rename(layerIndex, value.ToString());
				}
				IL_CD:
				if (this.OnLayerChanged != null)
				{
					this.OnLayerChanged(layerIndex, propertyName, value);
				}
				return;
			}
		}

		private Node FindLayerNode(int layerIndex)
		{
			foreach (object obj in this.list.Nodes)
			{
				Node node = (Node)obj;
				if (this.CastNodeToLayerIndex(node) == layerIndex)
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
				this.list.Enabled = this.enabled;
				if (!this.enabled)
				{
					this.Clear();
				}
			}
		}

		public void Clear()
		{
			this.exitNow = true;
			this.list.Nodes.Clear();
			this.list.Refresh();
			this.exitNow = false;
		}

		public int SelectedLayerIndex()
		{
			if (this.list.SelectedNode != null)
			{
				return this.CastNodeToLayerIndex(this.list.SelectedNode);
			}
			return -1;
		}

		public void Add(Layer layer, int layerIndex, bool selectLayer)
		{
			int num = this.list.Nodes.Count - layerIndex;
			num = ((num < 0) ? 0 : num);
			Node value = this.FormatLayerNode(layer, layerIndex);
			this.list.Nodes.Insert(num, value);
			this.ResetLayers();
			if (selectLayer)
			{
				this.SetActiveLayer(layerIndex, true);
			}
		}

		public void Edit(int layerIndex)
		{
			Node node = this.FindLayerNode(layerIndex);
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

		public void Rename(int layerIndex, string newName)
		{
			Node node = this.FindLayerNode(layerIndex);
			if (node != null)
			{
				try
				{
					node.Cells[1].Text = newName;
				}
				catch
				{
				}
			}
		}

		public void Remove(int layerIndex)
		{
			Node node = this.FindLayerNode(layerIndex);
			if (node != null)
			{
				this.exitNow = true;
				this.list.Nodes.Remove(node);
				this.exitNow = false;
				this.ResetLayers();
				this.SetActiveLayer(0, true);
			}
		}

		public bool SaveAsDefault()
		{
			if (this.drawArea.ActivePlan != null)
			{
				string defaultLayersFileName = Utilities.GetDefaultLayersFileName();
				return this.drawArea.ActivePlan.SaveLayers(defaultLayersFileName);
			}
			return false;
		}

		public bool SaveAs(string fileName)
		{
			return this.drawArea.ActivePlan != null && this.drawArea.ActivePlan.SaveLayers(fileName);
		}

		public void Load(string fileName)
		{
			if (this.drawArea.ActivePlan != null)
			{
				this.drawArea.ActivePlan.LoadLayers(fileName);
			}
		}

		public void ForceSelection(int layerIndex)
		{
			this.SetActiveLayer(layerIndex, true);
		}

		public void Refresh(int selectedLayerIndex)
		{
			this.exitNow = true;
			this.list.BeginUpdate();
			int i = this.list.Nodes.Count - 1;
			int num = 0;
			while (i >= 0)
			{
				this.list.Nodes[i].Tag = num;
				this.list.Nodes[i].Cells[0].HostedItem.Tag = num;
				this.list.Nodes[i].Cells[2].HostedItem.Tag = num;
				Layer layer = this.drawArea.GetLayer(num);
				try
				{
					if (this.list.Nodes[i].Cells[1].Text != layer.Name)
					{
						this.list.Nodes[i].Cells[1].Text = layer.Name;
						((CheckBoxItem)this.list.Nodes[i].Cells[0].HostedItem).Checked = layer.Visible;
						((SliderItem)this.list.Nodes[i].Cells[2].HostedItem).Value = layer.Opacity;
					}
				}
				catch
				{
				}
				i--;
				num++;
			}
			this.SetActiveLayer(selectedLayerIndex, true);
			this.list.EndUpdate();
			this.exitNow = false;
		}

		public void ToggleAllVisibility(bool visible)
		{
			Utilities.SuspendDrawing(this.drawArea.Owner);
			foreach (object obj in this.list.Nodes)
			{
				Node node = (Node)obj;
				((CheckBoxItem)node.Cells[0].HostedItem).Checked = visible;
			}
			Utilities.ResumeDrawing(this.drawArea.Owner);
		}

		public void ToggleVisibility(int layerIndex, bool visible)
		{
			Node node = this.FindLayerNode(layerIndex);
			if (node != null)
			{
				try
				{
					((CheckBoxItem)node.Cells[0].HostedItem).Checked = visible;
				}
				catch
				{
				}
			}
		}

		private void list_CellEditEnding(object sender, CellEditEventArgs e)
		{
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
			e.Cancel = !this.drawArea.ValidateLayerName(ref e.NewText, ref empty, ref empty2);
			if (e.Cancel)
			{
				Utilities.DisplayError(empty, empty2);
				return;
			}
			int num = this.CastNodeToLayerIndex(e.Cell.Parent);
			if (num != -1)
			{
				this.UpdateProperty(num, "Name", e.NewText);
			}
		}

		private void list_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			if (e.Node == null)
			{
				this.RevertToDefaultLayer();
				return;
			}
			int num = this.CastNodeToLayerIndex(e.Node);
			if (num == -1)
			{
				this.RevertToDefaultLayer();
				return;
			}
			this.SetActiveLayer(num, false);
		}

		private void list_VisibleEvent(object sender, CheckBoxChangeEventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			CheckBoxItem checkBoxItem = (CheckBoxItem)sender;
			int layerIndex = (int)checkBoxItem.Tag;
			bool flag = e.NewChecked.CheckState == CheckState.Checked;
			this.UpdateProperty(layerIndex, "Visible", flag);
		}

		private void list_SliderEvent(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			SliderItem sliderItem = (SliderItem)sender;
			int layerIndex = (int)sliderItem.Tag;
			int value = sliderItem.Value;
			this.UpdateProperty(layerIndex, "Opacity", value);
		}

		private OnLayerSelectedHandler OnLayerSelected;

		private OnLayerChangedHandler OnLayerChanged;

		private bool exitNow;

		private bool enabled;

		private Project project;

		private DrawingArea drawArea;

		private AdvTree list;
	}
}
