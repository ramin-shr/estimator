using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DrawObjectsNavigator
    {
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

        public bool EditingPlanName
        {
            get
            {
                return this.editingPlanName;
            }
            set
            {
                Control control;
                this.exitNow = true;
                this.editingPlanName = value;
                this.cbPlans.Visible = !this.editingPlanName;
                this.txtPlanName.Visible = this.editingPlanName;
                if (this.editingPlanName)
                {
                    this.txtPlanName.Text = this.drawArea.ActivePlan.Name;
                }
                if (this.editingPlanName)
                {
                    control = this.txtPlanName;
                }
                else
                {
                    control = this.cbPlans;
                }
                Utilities.SetObjectFocus(control);
                this.exitNow = false;
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
                this.tree.Enabled = this.enabled;
                this.cbPlans.Enabled = (this.Enabled ? true : this.project.Plans.Count > 0);
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

        public Layer SelectedLayer
        {
            get
            {
                Layer item;
                try
                {
                    item = this.drawArea.Layers[this.SelectedLayerIndex];
                }
                catch
                {
                    item = null;
                }
                return item;
            }
        }

        public int SelectedLayerIndex
        {
            get
            {
                int tag;
                try
                {
                    tag = (int)this.tree.SelectedNode.Parent.Tag;
                }
                catch
                {
                    tag = -1;
                }
                return tag;
            }
        }

        public DrawObject SelectedObject
        {
            get
            {
                DrawObject tag;
                try
                {
                    tag = (DrawObject)this.tree.SelectedNode.Tag;
                }
                catch
                {
                    tag = null;
                }
                return tag;
            }
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

        public void AddLayer(Layer layer, int layerIndex)
        {
            int num = layerIndex;
            num = (num > this.tree.Nodes.Count ? this.tree.Nodes.Count : num);
            Node node = this.FormatLayerNode(layer, layerIndex);
            this.tree.Nodes.Insert(num, node);
            node.Expand();
            this.ResetLayers();
        }

        public void AddObject(int layerIndex, DrawObject drawObject, bool selectObject)
        {
            Node node = this.FindLayerNode(layerIndex);
            if (node != null)
            {
                Node node1 = this.FormatObjectNode(drawObject);
                node.Nodes.Add(node1);
                if (selectObject)
                {
                    this.tree.SelectedNode = node1;
                }
                node.Nodes.Sort(new DrawObjectsNavigator.NodeSorter());
                this.tree.RecalcLayout();
            }
        }

        private static Plan CastItemToPlan(object item)
        {
            Plan data;
            try
            {
                data = (Plan)((Utilities.ItemData)item).Data;
            }
            catch
            {
                data = null;
            }
            return data;
        }

        private static int CastNodeToLayerIndex(Node node)
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

        private static DrawObject CastNodeToObject(Node node)
        {
            DrawObject tag;
            try
            {
                tag = (DrawObject)node.Tag;
            }
            catch
            {
                tag = null;
            }
            return tag;
        }

        private void cbPlans_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.EditingPlanName = true;
                e.Handled = true;
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
            Image plan16x16 = Resources.plan_16x16;
            Graphics graphics = e.Graphics;
            Rectangle bounds = e.Bounds;
            Rectangle rectangle = e.Bounds;
            graphics.DrawImage(plan16x16, new Rectangle(bounds.X + 4, rectangle.Y + 2, plan16x16.Width, plan16x16.Height));
            string str = ((e.State & DrawItemState.Selected) != DrawItemState.None ? "HighlightText" : "WindowText");
            Graphics graphic = e.Graphics;
            string name = plan.Name;
            Font font = this.cbPlans.Font;
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(0xff, Color.FromName(str)));
            float y = (float)e.Bounds.Y;
            float width = (float)(e.Bounds.Width - 26);
            Rectangle bounds1 = e.Bounds;
            graphic.DrawString(name, font, solidBrush, new RectangleF(26f, y, width, (float)bounds1.Height));
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

        private void comboBox_DropDown(object sender, EventArgs e)
        {
            this.thumbnailActive = true;
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.thumbnailActive = false;
            this.previewPanel.Visible = false;
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

        public void ExpandAll()
        {
            this.tree.ExpandAll();
            this.SelectDefaultNode();
            this.tree.RecalcLayout();
        }

        private Node FindLayerNode(int layerIndex)
        {
            Node node;
            IEnumerator enumerator = this.tree.Nodes.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Node current = (Node)enumerator.Current;
                    if ((int)current.Tag != layerIndex)
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

        private Node FindObjectNode(Node parentNode, DrawObject drawObject)
        {
            Node node;
            IEnumerator enumerator = parentNode.Nodes.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Node current = (Node)enumerator.Current;
                    DrawObject obj = DrawObjectsNavigator.CastNodeToObject(current);
                    if (obj == null || !obj.HasSameGroupOrID(drawObject))
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

        private Node FormatLayerNode(Layer layer, int layerIndex)
        {
            Node node = new Node();
            node.Cells.Clear();
            Cell cell = new Cell(layer.Name)
            {
                Editable = false
            };
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
            drawObjectIconIndicator.DoubleClick += new EventHandler(this.tree_DoubleClick);
            drawObjectIconIndicator.Size = new Size(12, 18);
            cell.HostedControl = drawObjectIconIndicator;
            cell.Editable = false;
            node.Cells.Add(cell);
            cell = new Cell(drawObject.Name)
            {
                Editable = drawObject.ObjectType != "Legend",
                EditorType = eCellEditorType.Default,
                TextMarkupEnabled = false
            };
            node.Cells.Add(cell);
            cell = new Cell(this.GetDrawObjectBasicInfo(drawObject))
            {
                StyleNormal = new ElementStyle(Color.DarkSlateGray),
                Editable = false
            };
            node.Cells.Add(cell);
            cell = new Cell();
            node.Cells.Add(cell);
            cell = new Cell()
            {
                CheckBoxVisible = true,
                Checked = drawObject.Visible,
                Editable = false
            };
            node.Cells.Add(cell);
            node.Tag = drawObject;
            node.DragDropEnabled = drawObject.ObjectType != "Legend";
            return node;
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
                    Node item = this.tree.Nodes[i].Nodes[j];
                    this.FreeHostedControl(item);
                }
            }
        }

        private string GetDrawObjectBasicInfo(DrawObject drawObject)
        {
            string empty = string.Empty;
            Plan activePlan = this.drawArea.ActivePlan;
            if (activePlan != null)
            {
                GroupStats groupStat = null;
                groupStat = GroupUtilities.ComputeGroupStats(activePlan, drawObject, activePlan.UnitScale.ScaleSystemType, true, "");
                string objectType = drawObject.ObjectType;
                string str = objectType;
                if (objectType != null)
                {
                    switch (str)
                    {
                        case "Line":
                            {
                                empty = this.drawArea.ToLengthStringFromUnitSystem(groupStat.Perimeter, false);
                                break;
                            }
                        case "Rectangle":
                            {
                                empty = "";
                                break;
                            }
                        case "Area":
                            {
                                empty = this.drawArea.ToAreaStringFromUnitSystem(groupStat.AreaMinusDeduction);
                                break;
                            }
                        case "Perimeter":
                            {
                                empty = this.drawArea.ToLengthStringFromUnitSystem(groupStat.NetLength, false);
                                break;
                            }
                        case "Counter":
                            {
                                empty = this.drawArea.ToUnitString(groupStat.GroupCount);
                                break;
                            }
                        case "Angle":
                            {
                                empty = this.drawArea.ToAngleString(((DrawAngle)drawObject).Angle, ((DrawAngle)drawObject).AngleType);
                                break;
                            }
                        case "Note":
                            {
                                empty = "";
                                break;
                            }
                    }
                }
            }
            return empty;
        }

        private Image GetDrawObjectImage(DrawObject drawObject)
        {
            return this.drawArea.GetDrawObjectIcon(drawObject);
        }

        private void InitializeCombo()
        {
            this.cbPlans.DrawItem += new DrawItemEventHandler(this.combo_DrawItem);
            this.cbPlans.SelectedIndexChanged += new EventHandler(this.combo_SelectedIndexChanged);
            this.cbPlans.DropDownClosed += new EventHandler(this.comboBox_DropDownClosed);
            this.cbPlans.DropDown += new EventHandler(this.comboBox_DropDown);
            this.cbPlans.KeyDown += new KeyEventHandler(this.cbPlans_KeyDown);
        }

        private void InitializeTextBox()
        {
            this.txtPlanName.Enter += new EventHandler(this.txtPlanName_Enter);
            this.txtPlanName.KeyDown += new KeyEventHandler(this.txtPlanName_KeyDown);
            this.txtPlanName.Validating += new CancelEventHandler(this.txtPlanName_Validating);
        }

        private void InitializeTreeView()
        {
            this.tree.AfterNodeSelect += new AdvTreeNodeEventHandler(this.tree_AfterNodeSelect);
            this.tree.DoubleClick += new EventHandler(this.tree_DoubleClick);
            this.tree.AfterCheck += new AdvTreeCellEventHandler(this.tree_AfterCheck);
            this.tree.BeforeCellEdit += new CellEditEventHandler(this.tree_BeforeCellEdit);
            this.tree.CellEditEnding += new CellEditEventHandler(this.tree_CellEditEnding);
            this.tree.NodeDragFeedback += new TreeDragFeedbackEventHander(this.tree_NodeDragFeedback);
            this.tree.BeforeNodeDrop += new TreeDragDropEventHandler(this.tree_BeforeNodeDrop);
            this.tree.AfterNodeDrop += new TreeDragDropEventHandler(this.tree_AfterNodeDrop);
        }

        public void InsertObject(Node parentNode, int index, DrawObject drawObject)
        {
            parentNode.Nodes.Insert(index, this.FormatObjectNode(drawObject));
        }

        private void LoadResources()
        {
        }

        private int QueryNodeLayer(Node node)
        {
            int num;
            try
            {
                int layerIndex = DrawObjectsNavigator.CastNodeToLayerIndex(node);
                if (layerIndex == -1)
                {
                    layerIndex = DrawObjectsNavigator.CastNodeToLayerIndex(node.Parent);
                    if (layerIndex == -1)
                    {
                        layerIndex = DrawObjectsNavigator.CastNodeToLayerIndex(node.Parent.Parent);
                        num = (layerIndex == -1 ? -1 : layerIndex);
                    }
                    else
                    {
                        num = layerIndex;
                    }
                }
                else
                {
                    num = layerIndex;
                }
            }
            catch
            {
                num = -1;
            }
            return num;
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
                    Node item = this.tree.Nodes[i].Nodes[j];
                    DrawObject obj = DrawObjectsNavigator.CastNodeToObject(item);
                    if (obj == null)
                    {
                        this.FreeHostedControl(this.tree.Nodes[j]);
                        this.tree.Nodes.RemoveAt(j);
                    }
                    else
                    {
                        this.UpdateObjectNode(item, obj);
                    }
                }
            }
            this.tree.RecalcLayout();
        }

        public void RefreshListOfPlans(Plan selectedPlan)
        {
            this.exitNow = true;
            int num = 0;
            this.cbPlans.Items.Clear();
            foreach (Plan collection in this.project.Plans.Collection)
            {
                this.cbPlans.Items.Add(new Utilities.ItemData(collection.Name, collection));
                if (selectedPlan != null && collection.Equals(selectedPlan))
                {
                    this.cbPlans.SelectedIndex = num;
                }
                num++;
            }
            this.exitNow = false;
        }

        public void Reload(Plan plan)
        {
            int num = 0;
            ArrayList arrayLists = new ArrayList();
            this.tree.BeginUpdate();
            this.Clear();
            if (plan == null)
            {
                this.tree.EndUpdate();
                return;
            }
            foreach (Layer collection in plan.Layers.Collection)
            {
                this.AddLayer(collection, num);
                for (int i = collection.DrawingObjects.Count - 1; i >= 0; i--)
                {
                    DrawObject item = collection.DrawingObjects[i];
                    bool deductionParentID = false;
                    deductionParentID = item.DeductionParentID != -1;
                    if (!deductionParentID)
                    {
                        if (item.IsPartOfGroup())
                        {
                            if (arrayLists.Contains(item.GroupID))
                            {
                                deductionParentID = true;
                            }
                            else
                            {
                                arrayLists.Add(item.GroupID);
                            }
                        }
                        if (!deductionParentID)
                        {
                            this.AddObject(num, item, false);
                        }
                    }
                }
                num++;
            }
            this.ExpandAll();
            this.tree.Refresh();
            this.tree.EndUpdate();
            this.tree.RecalcLayout();
            arrayLists.Clear();
            arrayLists = null;
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

        private void RemoveDeletedObjectsInTree(Node parentNode)
        {
            for (int i = parentNode.Nodes.Count - 1; i >= 0; i--)
            {
                this.RemoveDeletedObjectsInTree(parentNode.Nodes[i]);
                bool flag = false;
                DrawObject obj = DrawObjectsNavigator.CastNodeToObject(parentNode.Nodes[i]);
                if (obj != null)
                {
                    flag = (!obj.IsPartOfGroup() ? this.drawArea.GetObjectByID(this.drawArea.ActivePlan, obj.ID) == null : !GroupUtilities.GroupExists(this.drawArea.ActivePlan, obj.GroupID));
                    if (flag)
                    {
                        this.FreeHostedControl(parentNode.Nodes[i]);
                        parentNode.Nodes.RemoveAt(i);
                    }
                }
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

        public void RenameLayer(int layerIndex, string newName, ref string oldName)
        {
            Node node = this.FindLayerNode(layerIndex);
            if (node != null)
            {
                oldName = node.Text;
                node.Text = newName;
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
            string str = string.Empty;
            string text = this.txtPlanName.Text;
            if (!this.drawArea.ValidatePlanName(ref text, ref empty, ref str))
            {
                Utilities.DisplayError(empty, str);
                return false;
            }
            if (this.OnPlanRename != null)
            {
                this.OnPlanRename(plan, text);
            }
            return true;
        }

        public void ReplaceObject(DrawObject drawObject)
        {
            for (int i = 0; i < this.tree.Nodes.Count; i++)
            {
                Node item = this.tree.Nodes[i];
                for (int j = 0; j < item.Nodes.Count; j++)
                {
                    Node node = item.Nodes[j];
                    DrawObject obj = DrawObjectsNavigator.CastNodeToObject(node);
                    if (obj != null && obj.ID == drawObject.ID)
                    {
                        node.Tag = drawObject;
                        this.UpdateObjectNode(node, DrawObjectsNavigator.CastNodeToObject(node));
                    }
                }
            }
        }

        public void ResetItems()
        {
            for (int i = 0; i < this.tree.Nodes.Count; i++)
            {
                Node item = this.tree.Nodes[i];
                for (int j = 0; j < item.Nodes.Count; j++)
                {
                    Node node = item.Nodes[j];
                    DrawObject obj = DrawObjectsNavigator.CastNodeToObject(node);
                    if (obj != null)
                    {
                        this.UpdateObjectNode(node, DrawObjectsNavigator.CastNodeToObject(node));
                        node.Tag = obj;
                    }
                }
            }
        }

        private void ResetLayers()
        {
            for (int i = 0; i < this.tree.Nodes.Count; i++)
            {
                this.tree.Nodes[i].Tag = i;
            }
        }

        private void SelectDefaultNode()
        {
            if (this.tree.Nodes.Count > 0)
            {
                this.tree.SelectedIndex = 0;
            }
        }

        private void SetThumbnailPosition()
        {
            try
            {
                if (((Bar)this.parentControl).Docked)
                {
                    if (this.parentControl.Parent.Right - this.parentControl.Parent.Width - this.previewPanel.Size.Width <= 0)
                    {
                        ThumbnailPanel point = this.previewPanel;
                        int left = this.parentControl.Parent.Left + this.parentControl.Parent.Width;
                        int top = this.parentControl.Top;
                        Size size = this.previewPanel.Size;
                        point.Location = new Point(left, top + size.Height + 20);
                    }
                    else
                    {
                        ThumbnailPanel thumbnailPanel = this.previewPanel;
                        int right = this.parentControl.Parent.Right - this.parentControl.Parent.Width - this.previewPanel.Size.Width;
                        int num = this.parentControl.Top;
                        Size size1 = this.previewPanel.Size;
                        thumbnailPanel.Location = new Point(right, num + size1.Height + 20);
                    }
                }
                else if (this.parentControl.PointToScreen(this.parentControl.Location).X - this.previewPanel.Size.Width <= 0)
                {
                    ThumbnailPanel point1 = this.previewPanel;
                    Point screen = this.parentControl.PointToScreen(this.parentControl.Location);
                    Point screen1 = this.parentControl.PointToScreen(this.parentControl.Location);
                    point1.Location = new Point(screen.X + this.parentControl.Width + 6, screen1.Y + 52);
                }
                else
                {
                    ThumbnailPanel thumbnailPanel1 = this.previewPanel;
                    Point screen2 = this.parentControl.PointToScreen(this.parentControl.Location);
                    int x = screen2.X - this.previewPanel.Size.Width;
                    Point point2 = this.parentControl.PointToScreen(this.parentControl.Location);
                    thumbnailPanel1.Location = new Point(x, point2.Y + 52);
                }
            }
            catch
            {
            }
        }

        public void ToggleAllVisibility(bool visible)
        {
            Utilities.SuspendDrawing(this.drawArea.Owner);
            foreach (Node node in this.tree.Nodes)
            {
                foreach (Node node1 in node.Nodes)
                {
                    DrawObject obj = DrawObjectsNavigator.CastNodeToObject(node1);
                    if (obj == null)
                    {
                        continue;
                    }
                    try
                    {
                        node1.Cells[4].Checked = visible;
                    }
                    catch
                    {
                    }
                    this.UpdateProperty(obj, "Visible", visible);
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
            int tag = -1;
            Layer layer = null;
            try
            {
                tag = (int)node.Parent.Tag;
                layer = this.drawArea.GetLayer(tag);
            }
            catch
            {
            }
            if (layer == null)
            {
                return;
            }
            bool visible = false;
            bool flag = false;
            visible = !layer.Visible;
            flag = !drawObject.Visible;
            if ((visible || flag) && Utilities.DisplayWarningQuestionCustom(Resources.Objet_invisible, Resources.L_objet_que_vous_avez_sélectionné_est_présentement_invisible, null, Resources.Rendre_visible) == DialogResult.Yes)
            {
                if (visible)
                {
                    layer.Visible = true;
                    this.UpdateLayerProperty(tag, "Visible", true);
                }
                if (flag)
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

        private void tree_AfterCheck(object sender, AdvTreeCellEventArgs e)
        {
            DrawObject obj = DrawObjectsNavigator.CastNodeToObject(e.Cell.Parent);
            if (obj == null)
            {
                return;
            }
            this.UpdateProperty(obj, "Visible", e.Cell.Checked);
        }

        private void tree_AfterNodeDrop(object sender, TreeDragDropEventArgs e)
        {
            DrawObject obj = DrawObjectsNavigator.CastNodeToObject(e.Node);
            if (obj == null)
            {
                e.Cancel = true;
                return;
            }
            int layerIndex = DrawObjectsNavigator.CastNodeToLayerIndex(e.OldParentNode);
            if (layerIndex == -1)
            {
                e.Cancel = true;
                return;
            }
            int num = DrawObjectsNavigator.CastNodeToLayerIndex(e.NewParentNode);
            if (num == -1)
            {
                e.Cancel = true;
                return;
            }
            if (layerIndex == num)
            {
                e.Cancel = true;
                return;
            }
            e.InsertPosition = 0;
            if (this.OnGroupChangeLayer != null)
            {
                this.OnGroupChangeLayer(obj, layerIndex, num);
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
            DrawObject obj = DrawObjectsNavigator.CastNodeToObject(e.Cell.Parent);
            e.Cancel = obj == null;
        }

        private void tree_BeforeNodeDrop(object sender, TreeDragDropEventArgs e)
        {
        }

        private void tree_CellEditEnding(object sender, CellEditEventArgs e)
        {
            DrawObject obj = DrawObjectsNavigator.CastNodeToObject(e.Cell.Parent);
            e.Cancel = obj == null;
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
            string str = string.Empty;
            e.Cancel = !this.drawArea.ValidateDrawObjectName(ref e.NewText, ref empty, ref str);
            if (e.Cancel)
            {
                Utilities.DisplayError(empty, str);
                return;
            }
            this.UpdateProperty(obj, "Name", e.NewText);
        }

        private void tree_DoubleClick(object sender, EventArgs e)
        {
            if (this.OnObjectSelected != null && this.SelectedObject != null)
            {
                this.ToggleVisibility(this.SelectedObject);
                this.OnObjectSelected(this.SelectedObject);
            }
        }

        private void tree_NodeDragFeedback(object sender, TreeDragFeedbackEventArgs e)
        {
            try
            {
                if (DrawObjectsNavigator.CastNodeToObject(e.DragNode) != null)
                {
                    int num = this.QueryNodeLayer(e.DragNode.Parent);
                    if (num != -1)
                    {
                        int num1 = this.QueryNodeLayer(e.ParentNode);
                        if (num1 == -1)
                        {
                            e.AllowDrop = false;
                        }
                        else if (num == num1)
                        {
                            e.AllowDrop = false;
                        }
                        else if (DrawObjectsNavigator.CastNodeToObject(e.ParentNode) != null)
                        {
                            e.ParentNode = e.ParentNode.Parent;
                        }
                    }
                    else
                    {
                        e.AllowDrop = false;
                    }
                }
                else
                {
                    e.AllowDrop = false;
                }
            }
            catch
            {
                e.AllowDrop = false;
            }
        }

        private void txtPlanName_Enter(object sender, EventArgs e)
        {
            Utilities.SelectText(this.txtPlanName);
        }

        private void txtPlanName_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode != Keys.Return)
            {
                if (keyCode != Keys.Escape)
                {
                    return;
                }
                this.txtPlanName.Text = string.Empty;
                this.EditingPlanName = false;
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            Plan plan = DrawObjectsNavigator.CastItemToPlan(this.cbPlans.SelectedItem);
            if (plan != null && this.RenamePlan(plan))
            {
                this.EditingPlanName = false;
            }
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

        private void UpdateLayerProperty(int layerIndex, string propertyName, object value)
        {
            if (this.OnLayerChanged != null)
            {
                this.OnLayerChanged(layerIndex, propertyName, value);
            }
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

        private void UpdateProperty(DrawObject drawObject, string propertyName, object value)
        {
            GroupUtilities.UpdateGroupProperty(this.project, this.drawArea, drawObject, propertyName, value);
            if (this.OnObjectChanged != null)
            {
                this.OnObjectChanged(drawObject, propertyName, value);
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
            this.previewPanel.ThumbnailMarging = new Padding(6, 6, 0, 0);
            this.previewPanel.ThumbnailPadding = new Padding(15, 15, 0, 0);
            this.previewPanel.DisplayShadow = true;
            this.previewPanel.ThumbnailSize = new Size(this.previewPanel.Plan.Thumbnail.ThumbImage.Width, this.previewPanel.Plan.Thumbnail.ThumbImage.Height);
            this.previewPanel.RecalcLayout();
            this.previewPanel.BringToFront();
            this.SetThumbnailPosition();
            this.previewPanel.Visible = true;
        }

        public event OnGroupChangeLayerHandler OnGroupChangeLayer;

        public event OnLayerChangedHandler OnLayerChanged;

        public event OnNodeSelectedHandler OnNodeSelected;

        public event OnObjectChangedHandler OnObjectChanged;

        public event OnObjectSelectedHandler OnObjectSelected;

        public event OnPlanRenameHandler OnPlanRename;

        public event OnPlanSelectedHandler OnPlanSelected;

        private class NodeSorter : IComparer
        {
            public NodeSorter()
            {
            }

            public int Compare(object x, object y)
            {
                int num;
                try
                {
                    Node node = x as Node;
                    Node node1 = y as Node;
                    DrawObject obj = DrawObjectsNavigator.CastNodeToObject(node);
                    DrawObject drawObject = DrawObjectsNavigator.CastNodeToObject(node1);
                    if (obj.ObjectType == drawObject.ObjectType)
                    {
                        num = StringLogicalComparer.Compare(obj.Name, drawObject.Name);
                    }
                    else
                    {
                        string str = obj.ObjectSortOrder.ToString();
                        int objectSortOrder = drawObject.ObjectSortOrder;
                        num = StringLogicalComparer.Compare(str, objectSortOrder.ToString());
                    }
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    num = -1;
                }
                return num;
            }
        }
    }
}