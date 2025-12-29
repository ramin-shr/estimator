using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class LayersNavigator
    {
        private bool exitNow;

        private bool enabled;

        private Project project;

        private DrawingArea drawArea;

        private AdvTree list;

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

        public LayersNavigator(Project project, DrawingArea drawArea, AdvTree list)
        {
            this.project = project;
            this.drawArea = drawArea;
            this.list = list;
            this.enabled = true;
            this.InitializeTreeView();
            this.LoadResources();
        }

        public void Add(Layer layer, int layerIndex, bool selectLayer)
        {
            int count = this.list.Nodes.Count - layerIndex;
            count = (count < 0 ? 0 : count);
            Node node = this.FormatLayerNode(layer, layerIndex);
            this.list.Nodes.Insert(count, node);
            this.ResetLayers();
            if (selectLayer)
            {
                this.SetActiveLayer(layerIndex, true);
            }
        }

        private int CastNodeToLayerIndex(Node node)
        {
            int tag;
            try
            {
                tag = (int)node.Tag;
            }
            catch
            {
                tag = -1;
            }
            return tag;
        }

        public void Clear()
        {
            this.exitNow = true;
            this.list.Nodes.Clear();
            this.list.Refresh();
            this.exitNow = false;
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

        private Node FindLayerNode(int layerIndex)
        {
            Node node;
            IEnumerator enumerator = this.list.Nodes.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Node current = (Node)enumerator.Current;
                    if (this.CastNodeToLayerIndex(current) != layerIndex)
                    {
                        continue;
                    }
                    node = current;
                    return node;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return node;
        }

        public void ForceSelection(int layerIndex)
        {
            this.SetActiveLayer(layerIndex, true);
        }

        private Node FormatLayerNode(Layer layer, int layerIndex)
        {
            Node node = new Node();
            node.Cells.Clear();
            Cell cell = new Cell();
            CheckBoxItem checkBoxItem = new CheckBoxItem(string.Concat("chk", layer.Name))
            {
                TextVisible = false,
                CheckState = (layer.Visible ? CheckState.Checked : CheckState.Unchecked),
                Tag = layerIndex
            };
            cell.HostedItem = checkBoxItem;
            cell.Editable = false;
            checkBoxItem.CheckedChanged += new CheckBoxChangeEventHandler(this.list_VisibleEvent);
            node.Cells.Add(cell);
            Cell cell1 = new Cell(layer.Name)
            {
                Editable = true,
                EditorType = eCellEditorType.Default,
                TextMarkupEnabled = false
            };
            node.Cells.Add(cell1);
            Cell cell2 = new Cell();
            SliderItem sliderItem = new SliderItem(string.Concat("slider", layer.Name))
            {
                LabelVisible = false,
                TrackMarker = false,
                Minimum = 0,
                Maximum = 225,
                Tag = layerIndex
            };
            layer.Opacity = (layer.Opacity < 0 || layer.Opacity > 225 ? 150 : layer.Opacity);
            sliderItem.Value = layer.Opacity;
            cell2.HostedItem = sliderItem;
            cell2.Editable = false;
            sliderItem.ValueChanged += new EventHandler(this.list_SliderEvent);
            node.Cells.Add(cell2);
            node.Tag = layerIndex;
            return node;
        }

        private void InitializeTreeView()
        {
            this.list.CellEditEnding += new CellEditEventHandler(this.list_CellEditEnding);
            this.list.AfterNodeSelect += new AdvTreeNodeEventHandler(this.list_AfterNodeSelect);
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
            int layerIndex = this.CastNodeToLayerIndex(e.Node);
            if (layerIndex == -1)
            {
                this.RevertToDefaultLayer();
                return;
            }
            this.SetActiveLayer(layerIndex, false);
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
            string str = string.Empty;
            e.Cancel = !this.drawArea.ValidateLayerName(ref e.NewText, ref empty, ref str);
            if (e.Cancel)
            {
                Utilities.DisplayError(empty, str);
                return;
            }
            int layerIndex = this.CastNodeToLayerIndex(e.Cell.Parent);
            if (layerIndex != -1)
            {
                this.UpdateProperty(layerIndex, "Name", e.NewText);
            }
        }

        private void list_SliderEvent(object sender, EventArgs e)
        {
            if (this.exitNow)
            {
                return;
            }
            SliderItem sliderItem = (SliderItem)sender;
            int tag = (int)sliderItem.Tag;
            this.UpdateProperty(tag, "Opacity", sliderItem.Value);
        }

        private void list_VisibleEvent(object sender, CheckBoxChangeEventArgs e)
        {
            if (this.exitNow)
            {
                return;
            }
            int tag = (int)((CheckBoxItem)sender).Tag;
            bool checkState = e.NewChecked.CheckState == CheckState.Checked;
            this.UpdateProperty(tag, "Visible", checkState);
        }

        public void Load(string fileName)
        {
            if (this.drawArea.ActivePlan != null)
            {
                this.drawArea.ActivePlan.LoadLayers(fileName);
            }
        }

        private void LoadResources()
        {
        }

        public void Refresh(int selectedLayerIndex)
        {
            this.exitNow = true;
            this.list.BeginUpdate();
            int count = this.list.Nodes.Count - 1;
            int num = 0;
            while (count >= 0)
            {
                this.list.Nodes[count].Tag = num;
                this.list.Nodes[count].Cells[0].HostedItem.Tag = num;
                this.list.Nodes[count].Cells[2].HostedItem.Tag = num;
                Layer layer = this.drawArea.GetLayer(num);
                try
                {
                    if (this.list.Nodes[count].Cells[1].Text != layer.Name)
                    {
                        this.list.Nodes[count].Cells[1].Text = layer.Name;
                        ((CheckBoxItem)this.list.Nodes[count].Cells[0].HostedItem).Checked = layer.Visible;
                        ((SliderItem)this.list.Nodes[count].Cells[2].HostedItem).Value = layer.Opacity;
                    }
                }
                catch
                {
                }
                count--;
                num++;
            }
            this.SetActiveLayer(selectedLayerIndex, true);
            this.list.EndUpdate();
            this.exitNow = false;
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

        private void ResetLayers()
        {
            int count = this.list.Nodes.Count - 1;
            int num = 0;
            while (count >= 0)
            {
                this.list.Nodes[count].Tag = num;
                this.list.Nodes[count].Cells[0].HostedItem.Tag = num;
                this.list.Nodes[count].Cells[2].HostedItem.Tag = num;
                count--;
                num++;
            }
        }

        private void RevertToDefaultLayer()
        {
            Utilities.DisplayError(Resources.Calque_invalide, Resources.Bascule_vers_le_calque_par_défaut);
            this.ResetLayers();
            this.SetActiveLayer(0, true);
        }

        public bool SaveAs(string fileName)
        {
            if (this.drawArea.ActivePlan == null)
            {
                return false;
            }
            return this.drawArea.ActivePlan.SaveLayers(fileName);
        }

        public bool SaveAsDefault()
        {
            if (this.drawArea.ActivePlan == null)
            {
                return false;
            }
            string defaultLayersFileName = Utilities.GetDefaultLayersFileName();
            return this.drawArea.ActivePlan.SaveLayers(defaultLayersFileName);
        }

        public int SelectedLayerIndex()
        {
            if (this.list.SelectedNode == null)
            {
                return -1;
            }
            return this.CastNodeToLayerIndex(this.list.SelectedNode);
        }

        private void SetActiveLayer(int layerIndex, bool forceNodeSelection)
        {
            this.drawArea.ActiveLayerIndex = layerIndex;
            Console.WriteLine(string.Concat("Setting ActiveLayerIndex to [", layerIndex, "]"));
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

        public void ToggleAllVisibility(bool visible)
        {
            Utilities.SuspendDrawing(this.drawArea.Owner);
            foreach (Node node in this.list.Nodes)
            {
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

        private void UpdateProperty(int layerIndex, string propertyName, object value)
        {
            Layer layer = this.drawArea.GetLayer(layerIndex);
            if (layer == null)
            {
                return;
            }
            string str = propertyName;
            string str1 = str;
            if (str == null)
            {
                return;
            }
            if (str1 == "Name")
            {
                layer.PrevName = layer.Name;
                layer.Name = value.ToString();
                this.Rename(layerIndex, value.ToString());
            }
            else if (str1 == "Visible")
            {
                layer.Visible = Utilities.ConvertToBoolean(value, true);
            }
            else
            {
                if (str1 != "Opacity")
                {
                    return;
                }
                layer.Opacity = Utilities.ConvertToInt(value);
                foreach (DrawObject collection in layer.DrawingObjects.Collection)
                {
                    collection.Opacity = layer.Opacity;
                }
            }
            if (this.OnLayerChanged != null)
            {
                this.OnLayerChanged(layerIndex, propertyName, value);
            }
        }

        public event OnLayerChangedHandler OnLayerChanged;

        public event OnLayerSelectedHandler OnLayerSelected;
    }
}