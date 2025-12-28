using System;
using System.Collections;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace QuoterPlan
{
	public class TreeListViewState
	{
		public TreeListViewState() : this(null)
		{
		}

		public TreeListViewState(TreeList treeList)
		{
			this.treeList = treeList;
			this.expanded = new ArrayList();
			this.selected = new ArrayList();
		}

		public void Clear()
		{
			this.expanded.Clear();
			this.selected.Clear();
			this.focused = null;
			this.topIndex = 0;
		}

		private ArrayList GetExpanded()
		{
			TreeListViewState.OperationSaveExpanded operationSaveExpanded = new TreeListViewState.OperationSaveExpanded();
			this.TreeList.NodesIterator.DoOperation(operationSaveExpanded);
			return operationSaveExpanded.Nodes;
		}

		private ArrayList GetSelected()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.TreeList.Selection)
			{
				TreeListNode treeListNode = (TreeListNode)obj;
				arrayList.Add(treeListNode.GetValue(this.TreeList.KeyFieldName));
			}
			return arrayList;
		}

		public void LoadState()
		{
			this.TreeList.BeginUpdate();
			try
			{
				this.TreeList.CollapseAll();
				foreach (object keyID in this.expanded)
				{
					TreeListNode treeListNode = this.TreeList.FindNodeByKeyID(keyID);
					if (treeListNode != null)
					{
						treeListNode.Expanded = true;
					}
				}
				foreach (object keyID2 in this.selected)
				{
					TreeListNode treeListNode = this.TreeList.FindNodeByKeyID(keyID2);
					if (treeListNode != null)
					{
						this.TreeList.Selection.Add(treeListNode);
					}
				}
				this.TreeList.FocusedNode = this.TreeList.FindNodeByKeyID(this.focused);
			}
			finally
			{
				this.TreeList.EndUpdate();
				this.TreeList.TopVisibleNodeIndex = this.TreeList.GetVisibleIndexByNode(this.TreeList.FocusedNode) - this.topIndex;
			}
		}

		public void SaveState()
		{
			if (this.TreeList.FocusedNode != null)
			{
				this.expanded = this.GetExpanded();
				this.selected = this.GetSelected();
				this.focused = this.TreeList.FocusedNode[this.TreeList.KeyFieldName];
				this.topIndex = this.TreeList.GetVisibleIndexByNode(this.TreeList.FocusedNode) - this.TreeList.TopVisibleNodeIndex;
				return;
			}
			this.Clear();
		}

		public TreeList TreeList
		{
			get
			{
				return this.treeList;
			}
			set
			{
				this.treeList = value;
				this.Clear();
			}
		}

		private ArrayList expanded;

		private ArrayList selected;

		private object focused;

		private int topIndex;

		private TreeList treeList;

		private class OperationSaveExpanded : TreeListOperation
		{
			public override void Execute(TreeListNode node)
			{
				if (node.HasChildren && node.Expanded)
				{
					this.al.Add(node.GetValue(node.TreeList.KeyFieldName));
				}
			}

			public ArrayList Nodes
			{
				get
				{
					return this.al;
				}
			}

			public OperationSaveExpanded()
			{
			}

			private ArrayList al = new ArrayList();
		}
	}
}
