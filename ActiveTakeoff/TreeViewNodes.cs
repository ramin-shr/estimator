using System;
using System.Collections.Generic;

namespace QuoterPlan
{
	public class TreeViewNodes
	{
		public TreeViewNodes()
		{
			this.itemsList = new List<TreeViewNode>();
		}

		public TreeViewNode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.itemsList.Count)
				{
					return null;
				}
				return this.itemsList[index];
			}
		}

		public List<TreeViewNode> Collection
		{
			get
			{
				return this.itemsList;
			}
		}

		public int Add(int parentID, object tag)
		{
			int num = ++this.indexID;
			this.itemsList.Add(new TreeViewNode(num, parentID, tag));
			return num;
		}

		public int Add(object selectedTag, object newTag)
		{
			int num = -1;
			TreeViewNode treeViewNode = this.FindByTag(selectedTag);
			if (treeViewNode != null)
			{
				num = treeViewNode.ParentID;
			}
			if (num == -1)
			{
				return -1;
			}
			int num2 = ++this.indexID;
			this.itemsList.Add(new TreeViewNode(num2, num, newTag));
			return num2;
		}

		public int Add(int id, int parentID, object tag)
		{
			this.itemsList.Add(new TreeViewNode(id, parentID, tag));
			return id;
		}

		public TreeViewNode FindByTag(object tag)
		{
			foreach (TreeViewNode treeViewNode in this.itemsList)
			{
				if (treeViewNode.Tag.Equals(tag))
				{
					return treeViewNode;
				}
			}
			return null;
		}

		public void Delete(object tag)
		{
			for (int i = 0; i < this.itemsList.Count; i++)
			{
				if (this.itemsList[i].Tag.Equals(tag))
				{
					this.itemsList.RemoveAt(i);
					return;
				}
			}
		}

		public int Count
		{
			get
			{
				return this.itemsList.Count;
			}
		}

		public void Clear()
		{
			this.indexID = 0;
			this.itemsList.Clear();
		}

		public void Dump()
		{
			foreach (TreeViewNode treeViewNode in this.itemsList)
			{
				treeViewNode.Dump();
			}
		}

		private int indexID;

		protected List<TreeViewNode> itemsList;
	}
}
