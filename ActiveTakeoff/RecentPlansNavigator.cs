using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Padding = System.Windows.Forms.Padding;

namespace QuoterPlan
{
    public class RecentPlansNavigator
    {
        private const int RecentPlansMax = 10;

        private bool exitNow;

        private bool enabled;

        private Project project;

        private DrawingArea drawArea;

        private AdvTree list;

        private ThumbnailPanel previewPanel;

        private Control parentControl;

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

        private int UnpinnedPlansCount
        {
            get
            {
                int num = 0;
                foreach (Node node in this.list.Nodes)
                {
                    Plan plan = this.CastNodeToPlan(node);
                    if (plan == null || plan.Pinned)
                    {
                        continue;
                    }
                    num++;
                }
                return num;
            }
        }

        public RecentPlansNavigator(Project project, DrawingArea drawArea, AdvTree list, ThumbnailPanel previewPanel, Control parentControl)
        {
            this.project = project;
            this.drawArea = drawArea;
            this.list = list;
            this.previewPanel = previewPanel;
            this.parentControl = parentControl;
            this.enabled = true;
            this.InitializeTreeView();
            this.LoadResources();
        }

        public void Add(Plan plan, bool selectNode)
        {
            Node node = this.FindPlanNode(plan);
            if (node == null)
            {
                node = this.FormatPlanNode(plan);
                if (plan.Pinned || this.list.Nodes.Count == 0)
                {
                    this.list.Nodes.Insert(0, node);
                }
                else
                {
                    if (this.UnpinnedPlansCount == 10)
                    {
                        this.RemoveLastNode();
                    }
                    Node node1 = this.FindFirstUnpinnedPlanNode();
                    if (node1 == null)
                    {
                        this.list.Nodes.Add(node);
                    }
                    else
                    {
                        this.list.Nodes.Insert(node1.Index, node);
                    }
                }
            }
            if (selectNode)
            {
                this.list.SelectedNode = node;
            }
        }

        private Plan CastNodeToPlan(Node node)
        {
            Plan tag;
            try
            {
                tag = (Plan)node.Tag;
            }
            catch
            {
                tag = null;
            }
            return tag;
        }

        public void Clear()
        {
            this.exitNow = true;
            this.list.Nodes.Clear();
            this.exitNow = false;
        }

        public void Edit()
        {
            Node selectedNode = this.list.SelectedNode;
            if (selectedNode != null)
            {
                try
                {
                    selectedNode.BeginEdit(1);
                }
                catch
                {
                }
            }
        }

        private Node FindFirstUnpinnedPlanNode()
        {
            Node node;
            IEnumerator enumerator = this.list.Nodes.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Node current = (Node)enumerator.Current;
                    Plan plan = this.CastNodeToPlan(current);
                    if (plan == null || plan.Pinned)
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

        private Node FindPlanNode(Plan plan)
        {
            Node node;
            IEnumerator enumerator = this.list.Nodes.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Node current = (Node)enumerator.Current;
                    Plan plan1 = this.CastNodeToPlan(current);
                    if (plan1 == null || !plan1.Equals(plan))
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

        private Node FormatPlanNode(Plan plan)
        {
            Node node = new Node();
            node.Cells.Clear();
            Cell cell = new Cell();
            cell.Images.Image = (plan.Pinned ? Resources.pin_small : Resources.unpin_small);
            cell.ImageAlignment = eCellPartAlignment.FarCenter;
            cell.Editable = false;
            node.Cells.Add(cell);
            Cell cell1 = new Cell(plan.Name)
            {
                Editable = true,
                EditorType = eCellEditorType.Default
            };
            node.Cells.Add(cell1);
            Cell cell2 = new Cell();
            cell.Editable = false;
            node.Cells.Add(cell2);
            node.Tag = plan;
            return node;
        }

        private void InitializeTreeView()
        {
            this.list.CellEditEnding += new CellEditEventHandler(this.list_CellEditEnding);
            this.list.MouseClick += new MouseEventHandler(this.list_MouseClick);
            this.list.NodeDoubleClick += new TreeNodeMouseEventHandler(this.list_NodeDoubleClick);
            this.list.MouseMove += new MouseEventHandler(this.list_MouseMove);
            this.list.MouseLeave += new EventHandler(this.list_MouseLeave);
            this.list.NodeMouseHover += new TreeNodeMouseEventHandler(this.list_NodeMouseHover);
        }

        private void list_CellEditEnding(object sender, CellEditEventArgs e)
        {
            Plan plan = this.CastNodeToPlan(this.list.SelectedNode);
            if (plan == null)
            {
                return;
            }
            e.NewText = e.NewText.Trim();
            if (e.NewText == e.Cell.Text)
            {
                return;
            }
            if (e.NewText == string.Empty)
            {
                e.NewText = e.Cell.Text;
                return;
            }
            string empty = string.Empty;
            string str = string.Empty;
            string newText = e.NewText;
            e.Cancel = !this.drawArea.ValidatePlanName(ref newText, ref empty, ref str);
            if (e.Cancel)
            {
                Utilities.DisplayError(empty, str);
                return;
            }
            if (this.OnPlanRename != null)
            {
                this.OnPlanRename(plan, newText);
            }
        }

        private void list_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 22)
            {
                return;
            }
            Node selectedNode = this.list.SelectedNode;
            Plan plan = this.CastNodeToPlan(selectedNode);
            if (plan == null)
            {
                return;
            }
            plan.Pinned = !plan.Pinned;
            selectedNode.Cells[0].Images.Image = (plan.Pinned ? Resources.pin_small : Resources.unpin_small);
            this.list.Nodes.Remove(selectedNode);
            this.Add(plan, true);
        }

        private void list_MouseLeave(object sender, EventArgs e)
        {
            this.previewPanel.Visible = false;
        }

        private void list_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.list.IsCellEditing)
            {
                return;
            }
            if (!this.list.Focused)
            {
                this.list.Focus();
            }
        }

        private void list_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            Plan plan = this.CastNodeToPlan(e.Node);
            if (plan == null)
            {
                return;
            }
            if (this.OnPlanLoad != null)
            {
                this.OnPlanLoad(plan);
            }
        }

        private void list_NodeMouseHover(object sender, TreeNodeMouseEventArgs e)
        {
            if (this.list.IsCellEditing)
            {
                return;
            }
            Plan plan = this.CastNodeToPlan(e.Node);
            if (plan == null)
            {
                return;
            }
            if (!plan.Thumbnail.IsValid())
            {
                plan.CreateThumbnail(true);
            }
            if (!plan.Thumbnail.IsValid())
            {
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

        public void LoadFromProject()
        {
            this.list.BeginUpdate();
            this.list.Nodes.Clear();
            for (int i = this.project.Workspace.RecentPlans.Count - 1; i >= 0; i--)
            {
                Plan plan = this.project.Plans.FindPlan(this.project.Workspace.RecentPlans[i].Name);
                if (plan != null)
                {
                    this.Add(plan, false);
                }
            }
            this.list.EndUpdate();
        }

        private void LoadResources()
        {
        }

        public void Remove(Plan plan)
        {
            Node node = this.FindPlanNode(plan);
            if (node != null)
            {
                this.RemoveNode(node);
            }
        }

        private void RemoveLastNode()
        {
            try
            {
                this.list.Nodes.RemoveAt(this.list.Nodes.Count - 1);
            }
            catch
            {
            }
        }

        private void RemoveNode(Node node)
        {
            try
            {
                this.list.Nodes.Remove(node);
            }
            catch
            {
            }
        }

        public void Rename(Plan plan, string newName)
        {
            Node node = this.FindPlanNode(plan);
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

        public void SaveToProject()
        {
            this.project.Workspace.RecentPlans.Clear();
            foreach (Node node in this.list.Nodes)
            {
                Plan plan = this.CastNodeToPlan(node);
                if (plan == null)
                {
                    continue;
                }
                this.project.Workspace.RecentPlans.Add(new Variable(plan.Name, null));
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

        public event OnPlanSelectedHandler OnPlanLoad;

        public event OnPlanRenameHandler OnPlanRename;
    }
}