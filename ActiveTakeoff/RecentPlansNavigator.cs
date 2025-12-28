using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class RecentPlansNavigator
	{
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

		public event OnPlanSelectedHandler OnPlanLoad
		{
			add
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanLoad;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Combine(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanLoad, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
			remove
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanLoad;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Remove(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanLoad, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
		}

		private void LoadResources()
		{
		}

		private void InitializeTreeView()
		{
			this.list.CellEditEnding += this.list_CellEditEnding;
			this.list.MouseClick += this.list_MouseClick;
			this.list.NodeDoubleClick += this.list_NodeDoubleClick;
			this.list.MouseMove += this.list_MouseMove;
			this.list.MouseLeave += this.list_MouseLeave;
			this.list.NodeMouseHover += this.list_NodeMouseHover;
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

		private int UnpinnedPlansCount
		{
			get
			{
				int num = 0;
				foreach (object obj in this.list.Nodes)
				{
					Node node = (Node)obj;
					Plan plan = this.CastNodeToPlan(node);
					if (plan != null && !plan.Pinned)
					{
						num++;
					}
				}
				return num;
			}
		}

		private Plan CastNodeToPlan(Node node)
		{
			Plan result;
			try
			{
				result = (Plan)node.Tag;
			}
			catch
			{
				result = null;
			}
			return result;
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
			Cell cell2 = new Cell(plan.Name);
			cell2.Editable = true;
			cell2.EditorType = eCellEditorType.Default;
			node.Cells.Add(cell2);
			Cell cell3 = new Cell();
			cell.Editable = false;
			node.Cells.Add(cell3);
			node.Tag = plan;
			return node;
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

		private Node FindPlanNode(Plan plan)
		{
			foreach (object obj in this.list.Nodes)
			{
				Node node = (Node)obj;
				Plan plan2 = this.CastNodeToPlan(node);
				if (plan2 != null && plan2.Equals(plan))
				{
					return node;
				}
			}
			return null;
		}

		private Node FindFirstUnpinnedPlanNode()
		{
			foreach (object obj in this.list.Nodes)
			{
				Node node = (Node)obj;
				Plan plan = this.CastNodeToPlan(node);
				if (plan != null && !plan.Pinned)
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
			this.exitNow = false;
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
					Node node2 = this.FindFirstUnpinnedPlanNode();
					if (node2 != null)
					{
						this.list.Nodes.Insert(node2.Index, node);
					}
					else
					{
						this.list.Nodes.Add(node);
					}
				}
			}
			if (selectNode)
			{
				this.list.SelectedNode = node;
			}
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

		public void Remove(Plan plan)
		{
			Node node = this.FindPlanNode(plan);
			if (node != null)
			{
				this.RemoveNode(node);
			}
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

		public void SaveToProject()
		{
			this.project.Workspace.RecentPlans.Clear();
			foreach (object obj in this.list.Nodes)
			{
				Node node = (Node)obj;
				Plan plan = this.CastNodeToPlan(node);
				if (plan != null)
				{
					this.project.Workspace.RecentPlans.Add(new Variable(plan.Name, null));
				}
			}
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
			string empty2 = string.Empty;
			string newText = e.NewText;
			e.Cancel = !this.drawArea.ValidatePlanName(ref newText, ref empty, ref empty2);
			if (e.Cancel)
			{
				Utilities.DisplayError(empty, empty2);
				return;
			}
			if (this.OnPlanRename != null)
			{
				this.OnPlanRename(plan, newText);
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
			this.previewPanel.ThumbnailMarging = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.previewPanel.ThumbnailPadding = new System.Windows.Forms.Padding(15, 15, 0, 0);
			this.previewPanel.DisplayShadow = true;
			this.previewPanel.ThumbnailSize = new Size(this.previewPanel.Plan.Thumbnail.ThumbImage.Width, this.previewPanel.Plan.Thumbnail.ThumbImage.Height);
			this.previewPanel.RecalcLayout();
			this.previewPanel.BringToFront();
			this.SetThumbnailPosition();
			this.previewPanel.Visible = true;
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

		private const int RecentPlansMax = 10;

		private OnPlanRenameHandler OnPlanRename;

		private OnPlanSelectedHandler OnPlanLoad;

		private bool exitNow;

		private bool enabled;

		private Project project;

		private DrawingArea drawArea;

		private AdvTree list;

		private ThumbnailPanel previewPanel;

		private Control parentControl;
	}
}
