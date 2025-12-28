using System;
using System.IO;
using System.Threading;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class MruManager
	{
		public event OnRecentProjectSelectedHandler OnRecentProjectSelected
		{
			add
			{
				OnRecentProjectSelectedHandler onRecentProjectSelectedHandler = this.OnRecentProjectSelected;
				OnRecentProjectSelectedHandler onRecentProjectSelectedHandler2;
				do
				{
					onRecentProjectSelectedHandler2 = onRecentProjectSelectedHandler;
					OnRecentProjectSelectedHandler value2 = (OnRecentProjectSelectedHandler)Delegate.Combine(onRecentProjectSelectedHandler2, value);
					onRecentProjectSelectedHandler = Interlocked.CompareExchange<OnRecentProjectSelectedHandler>(ref this.OnRecentProjectSelected, value2, onRecentProjectSelectedHandler2);
				}
				while (onRecentProjectSelectedHandler != onRecentProjectSelectedHandler2);
			}
			remove
			{
				OnRecentProjectSelectedHandler onRecentProjectSelectedHandler = this.OnRecentProjectSelected;
				OnRecentProjectSelectedHandler onRecentProjectSelectedHandler2;
				do
				{
					onRecentProjectSelectedHandler2 = onRecentProjectSelectedHandler;
					OnRecentProjectSelectedHandler value2 = (OnRecentProjectSelectedHandler)Delegate.Remove(onRecentProjectSelectedHandler2, value);
					onRecentProjectSelectedHandler = Interlocked.CompareExchange<OnRecentProjectSelectedHandler>(ref this.OnRecentProjectSelected, value2, onRecentProjectSelectedHandler2);
				}
				while (onRecentProjectSelectedHandler != onRecentProjectSelectedHandler2);
			}
		}

		public Variables RecentProjects
		{
			get
			{
				return this.recentProjects;
			}
		}

		public MruManager()
		{
			this.recentProjects = new Variables();
		}

		private void Clear()
		{
			this.recentProjects.Clear();
		}

		private void Add(string fileName)
		{
			this.recentProjects.Add(new Variable(fileName, null));
		}

		public void Insert(string fileName)
		{
			Variable variable = this.recentProjects.Find(fileName);
			if (variable != null)
			{
				this.recentProjects.Remove(variable);
			}
			this.recentProjects.Insert(0, new Variable(fileName, null));
			if (this.recentProjects.Count > 10)
			{
				for (int i = this.recentProjects.Count - 1; i >= 10; i--)
				{
					this.recentProjects.RemoveAt(i);
				}
			}
			this.Dump();
		}

		public void Load(string directory)
		{
			try
			{
				this.Clear();
				this.currentDirectory = directory;
				string mruList = Settings.Default.MruList;
				if (!(mruList == ""))
				{
					string[] fields = Utilities.GetFields(mruList, '|');
					foreach (string fileName in fields)
					{
						this.Add(fileName);
					}
				}
			}
			catch
			{
			}
		}

		public void Save()
		{
			try
			{
				string text = "";
				foreach (object obj in this.recentProjects.Collection)
				{
					Variable variable = (Variable)obj;
					text = text + variable.Name + "|";
				}
				Settings.Default.MruList = text;
			}
			catch
			{
			}
		}

		private void GalleryClearSubItems(GalleryContainer gallery)
		{
			if (gallery.SubItems.Count > 1)
			{
				for (int i = gallery.SubItems.Count - 1; i >= 1; i--)
				{
					gallery.SubItems.RemoveAt(i);
				}
			}
		}

		private void GalleryAddItem(GalleryContainer gallery, string displayName, string fullName)
		{
			ButtonItem buttonItem = new ButtonItem();
			buttonItem.Text = " " + displayName;
			buttonItem.ButtonStyle = eButtonStyle.Default;
			buttonItem.CanCustomize = false;
			buttonItem.Tag = fullName;
			buttonItem.Click += this.recentFileOpen_Click;
			gallery.SubItems.Add(buttonItem);
		}

		private void ListClearItems(AdvTree list)
		{
			list.Nodes.Clear();
		}

		private void ListAddItem(AdvTree list, string displayName, string fullName)
		{
			Node node = new Node();
			node.Cells.Clear();
			Cell cell = new Cell(" " + displayName);
			node.Cells.Add(cell);
			Cell cell2 = new Cell();
			node.Cells.Add(cell2);
			node.Tag = fullName;
			list.Nodes.Add(node);
			node.NodeClick += this.recentFileOpen_Click;
		}

		public void Populate(GalleryContainer galleryRecentProjects, AdvTree lstRecentProjects)
		{
			lstRecentProjects.BeginUpdate();
			this.GalleryClearSubItems(galleryRecentProjects);
			this.ListClearItems(lstRecentProjects);
			foreach (object obj in this.recentProjects.Collection)
			{
				Variable variable = (Variable)obj;
				string displayName = this.GetDisplayName(variable.Name);
				this.GalleryAddItem(galleryRecentProjects, displayName, variable.Name);
				this.ListAddItem(lstRecentProjects, displayName, variable.Name);
			}
			galleryRecentProjects.RecalcSize();
			lstRecentProjects.EndUpdate();
			if (lstRecentProjects.Nodes.Count > 0)
			{
				lstRecentProjects.SelectedIndex = 0;
			}
		}

		private string GetDisplayName(string fullName)
		{
			FileInfo fileInfo = new FileInfo(fullName);
			if (fileInfo.DirectoryName == this.currentDirectory)
			{
				return Utilities.TruncatePath(fileInfo.Name, 60);
			}
			if (Utilities.GetParentDirectory(fileInfo.DirectoryName) == this.currentDirectory)
			{
				return Utilities.TruncatePath(Utilities.GetFileName(fileInfo.DirectoryName, false), 60);
			}
			return Utilities.TruncatePath(fullName, 60);
		}

		public void PurgeProject(string fileName)
		{
			for (int i = this.recentProjects.Count - 1; i >= 0; i--)
			{
				if (this.recentProjects[i].Name == fileName)
				{
					this.recentProjects.RemoveAt(i);
				}
			}
		}

		public void Dump()
		{
			foreach (object obj in this.recentProjects.Collection)
			{
				Variable variable = (Variable)obj;
				Console.WriteLine(variable.Name);
			}
		}

		public void Open(AdvTree lstRecentProjects)
		{
			Node selectedNode = lstRecentProjects.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			string text = "";
			try
			{
				text = selectedNode.Tag.ToString();
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return;
			}
			if (!(text == string.Empty))
			{
				if (this.OnRecentProjectSelected != null)
				{
					this.OnRecentProjectSelected(text);
				}
				return;
			}
		}

		private void recentFileOpen_Click(object sender, EventArgs e)
		{
			string text = string.Empty;
			try
			{
				if (e.GetType().Name == "TreeNodeMouseEventArgs")
				{
					TreeNodeMouseEventArgs treeNodeMouseEventArgs = (TreeNodeMouseEventArgs)e;
					text = treeNodeMouseEventArgs.Node.Tag.ToString();
				}
				else if (e.GetType().Name == "EventSourceArgs")
				{
					ButtonItem buttonItem = (ButtonItem)sender;
					text = buttonItem.Tag.ToString();
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return;
			}
			if (!(text == string.Empty))
			{
				if (this.OnRecentProjectSelected != null)
				{
					this.OnRecentProjectSelected(text);
				}
				return;
			}
		}

		private const int RecentProjectsMax = 10;

		private const int DisplayLengthMax = 60;

		private OnRecentProjectSelectedHandler OnRecentProjectSelected;

		private Variables recentProjects;

		private string currentDirectory;
	}
}
