using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;

namespace QuoterPlan
{
    public class MruManager
    {
        private const int RecentProjectsMax = 10;

        private const int DisplayLengthMax = 60;

        private Variables recentProjects;

        private string currentDirectory;

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

        private void Add(string fileName)
        {
            this.recentProjects.Add(new Variable(fileName, null));
        }

        private void Clear()
        {
            this.recentProjects.Clear();
        }

        public void Dump()
        {
            foreach (Variable collection in this.recentProjects.Collection)
            {
                Console.WriteLine(collection.Name);
            }
        }

        private void GalleryAddItem(GalleryContainer gallery, string displayName, string fullName)
        {
            ButtonItem buttonItem = new ButtonItem()
            {
                Text = string.Concat(" ", displayName),
                ButtonStyle = eButtonStyle.Default,
                CanCustomize = false,
                Tag = fullName
            };
            buttonItem.Click += new EventHandler(this.recentFileOpen_Click);
            gallery.SubItems.Add(buttonItem);
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

        private string GetDisplayName(string fullName)
        {
            FileInfo fileInfo = new FileInfo(fullName);
            if (fileInfo.DirectoryName == this.currentDirectory)
            {
                return Utilities.TruncatePath(fileInfo.Name, 60);
            }
            if (Utilities.GetParentDirectory(fileInfo.DirectoryName) != this.currentDirectory)
            {
                return Utilities.TruncatePath(fullName, 60);
            }
            return Utilities.TruncatePath(Utilities.GetFileName(fileInfo.DirectoryName, false), 60);
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

        private void ListAddItem(AdvTree list, string displayName, string fullName)
        {
            Node node = new Node();
            node.Cells.Clear();
            Cell cell = new Cell(string.Concat(" ", displayName));
            node.Cells.Add(cell);
            Cell cell1 = new Cell();
            node.Cells.Add(cell1);
            node.Tag = fullName;
            list.Nodes.Add(node);
            node.NodeClick += new EventHandler(this.recentFileOpen_Click);
        }

        private void ListClearItems(AdvTree list)
        {
            list.Nodes.Clear();
        }

        public void Load(string directory)
        {
            try
            {
                this.Clear();
                this.currentDirectory = directory;
                string mruList = Settings.Default.MruList;
                if (mruList != "")
                {
                    string[] fields = Utilities.GetFields(mruList, '|');
                    for (int i = 0; i < (int)fields.Length; i++)
                    {
                        this.Add(fields[i]);
                    }
                }
            }
            catch
            {
            }
        }

        public void Open(AdvTree lstRecentProjects)
        {
            Node selectedNode = lstRecentProjects.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            string str = "";
            try
            {
                str = selectedNode.Tag.ToString();
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                return;
            }
            if (str != string.Empty)
            {
                if (this.OnRecentProjectSelected != null)
                {
                    this.OnRecentProjectSelected(str);
                }
                return;
            }
        }

        public void Populate(GalleryContainer galleryRecentProjects, AdvTree lstRecentProjects)
        {
            lstRecentProjects.BeginUpdate();
            this.GalleryClearSubItems(galleryRecentProjects);
            this.ListClearItems(lstRecentProjects);
            foreach (Variable collection in this.recentProjects.Collection)
            {
                string displayName = this.GetDisplayName(collection.Name);
                this.GalleryAddItem(galleryRecentProjects, displayName, collection.Name);
                this.ListAddItem(lstRecentProjects, displayName, collection.Name);
            }
            galleryRecentProjects.RecalcSize();
            lstRecentProjects.EndUpdate();
            if (lstRecentProjects.Nodes.Count > 0)
            {
                lstRecentProjects.SelectedIndex = 0;
            }
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

        private void recentFileOpen_Click(object sender, EventArgs e)
        {
            string empty = string.Empty;
            try
            {
                if (e.GetType().Name == "TreeNodeMouseEventArgs")
                {
                    empty = ((TreeNodeMouseEventArgs)e).Node.Tag.ToString();
                }
                else if (e.GetType().Name == "EventSourceArgs")
                {
                    empty = ((ButtonItem)sender).Tag.ToString();
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                return;
            }
            if (empty != string.Empty)
            {
                if (this.OnRecentProjectSelected != null)
                {
                    this.OnRecentProjectSelected(empty);
                }
                return;
            }
        }

        public void Save()
        {
            try
            {
                string str = "";
                foreach (Variable collection in this.recentProjects.Collection)
                {
                    str = string.Concat(str, collection.Name, "|");
                }
                Settings.Default.MruList = str;
            }
            catch
            {
            }
        }

        public event OnRecentProjectSelectedHandler OnRecentProjectSelected;
    }
}