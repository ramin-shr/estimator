using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraTreeList.Nodes;
using QuoterPlan.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace QuoterPlan
{
    public class TemplatesLibraryEditor
    {
        private bool enabled;

        private TemplatesSupport templatesSupport;

        private TreeList tree;

        private ImageCollection imageCollection;

        private bool firstElementSelected;

        private Font titleFont;

        private Font defaultFont;

        private TreeListViewState treeListViewState;

        private TreeListNode hotTrackNode;

        private MainForm mainForm;

        [CompilerGenerated]
        // CS$<>9__CachedAnonymousMethodDelegate1
        private static EventHandler CSu0024u003cu003e9__CachedAnonymousMethodDelegate1;

        private TreeListNode HotTrackNode
        {
            get
            {
                return this.hotTrackNode;
            }
            set
            {
                if (this.hotTrackNode != value)
                {
                    TreeListNode treeListNode = this.hotTrackNode;
                    this.hotTrackNode = value;
                    if (this.tree.ActiveEditor != null)
                    {
                        this.tree.PostEditor();
                    }
                    this.tree.RefreshNode(treeListNode);
                    this.tree.RefreshNode(this.hotTrackNode);
                }
            }
        }

        public TemplatesLibraryEditor(MainForm mainForm, TemplatesSupport templatesSupport, TreeList tree, ImageCollection imageCollection)
        {
            this.mainForm = mainForm;
            this.templatesSupport = templatesSupport;
            this.tree = tree;
            this.imageCollection = imageCollection;
            this.enabled = true;
            this.InitializeTreeView();
            this.LoadResources();
        }

        [CompilerGenerated]
        // <InitializeTreeView>b__0
        private static void u003cInitializeTreeViewu003eb__0(object sender, EventArgs args)
        {
            TextEdit textEdit = sender as TextEdit;
            if (textEdit != null)
            {
                textEdit.SelectAll();
            }
        }

        public void Add(string objectType)
        {
            if (objectType == "Area" && this.OnCreateArea != null)
            {
                this.OnCreateArea(this, new EventArgs());
            }
            if (objectType == "Perimeter" && this.OnCreatePerimeter != null)
            {
                this.OnCreatePerimeter(this, new EventArgs());
            }
            if (objectType == "Line" && this.OnCreateLength != null)
            {
                this.OnCreateLength(this, new EventArgs());
            }
            if (objectType == "Counter" && this.OnCreateCounter != null)
            {
                this.OnCreateCounter(this, new EventArgs());
            }
        }

        private string CastNodeToObjectType(TreeListNode node)
        {
            string str;
            try
            {
                str = Utilities.ConvertToString(node.GetValue(0), "");
            }
            catch
            {
                str = "";
            }
            return str;
        }

        private Template CastNodeToTemplate(TreeListNode node)
        {
            Template value;
            try
            {
                value = (Template)node.GetValue(0);
            }
            catch
            {
                value = null;
            }
            return value;
        }

        private void CleanUpTree()
        {
            for (int i = this.tree.Nodes.Count - 1; i >= 0; i--)
            {
                if (this.tree.Nodes[i].Level == 0 && !this.tree.Nodes[i].HasChildren)
                {
                    this.tree.Nodes.RemoveAt(i);
                }
            }
        }

        public void Delete()
        {
            Template currentTemplate = this.GetCurrentTemplate();
            if (currentTemplate == null)
            {
                return;
            }
            if (Utilities.DisplayDeleteConfirmation(string.Concat(currentTemplate.DrawObject.Name, Environment.NewLine, Resources.Supprimer_ce_modèle), Resources.Si_vous_supprimez_ce_modèle_il_ne_sera_plus_disponible_pour_vos_prochains_projets) == DialogResult.Yes)
            {
                Utilities.FileDelete(currentTemplate.FullFileName, true);
                this.templatesSupport.RemoveTemplate(currentTemplate);
                this.tree.RefreshDataSource();
                this.CleanUpTree();
            }
        }

        public void Duplicate()
        {
            if (this.GetCurrentTemplate() == null)
            {
                return;
            }
            if (this.OnDuplicate != null)
            {
                this.OnDuplicate(this, new EventArgs());
            }
        }

        private void Filtering(object sender, FilterNodeEventArgs e)
        {
            if (e.IsFitDefaultFilter)
            {
                return;
            }
            TreeListNode parentNode = e.Node.ParentNode;
            if (parentNode == null)
            {
                return;
            }
            MethodInfo method = this.tree.GetType().GetMethod("FilterNodeOnFilterCriteria", BindingFlags.Instance | BindingFlags.NonPublic);
            while (parentNode != null)
            {
                object[] objArray = new object[] { parentNode };
                if ((bool)method.Invoke(sender, objArray))
                {
                    e.Node.Visible = true;
                    e.Handled = true;
                    return;
                }
                parentNode = parentNode.ParentNode;
            }
        }

        public Template GetCurrentTemplate()
        {
            Template template;
            try
            {
                if (this.tree.FocusedNode.Level <= 0)
                {
                    template = null;
                }
                else
                {
                    template = this.CastNodeToTemplate(this.tree.FocusedNode);
                }
            }
            catch
            {
                template = null;
            }
            return template;
        }

        private int GetNodeParentID()
        {
            int id;
            try
            {
                id = this.tree.FocusedNode.ParentNode.Id;
            }
            catch
            {
                id = -1;
            }
            return id;
        }

        public TemplatesLibraryEditor.NodeItemType GetSelectedNodeType()
        {
            TemplatesLibraryEditor.NodeItemType nodeItemType;
            try
            {
                nodeItemType = (this.tree.FocusedNode.Level != 0 ? TemplatesLibraryEditor.NodeItemType.Item : TemplatesLibraryEditor.NodeItemType.Section);
            }
            catch
            {
                nodeItemType = TemplatesLibraryEditor.NodeItemType.NotSelected;
            }
            return nodeItemType;
        }

        private void InitializeTreeView()
        {
            this.titleFont = Utilities.GetDefaultFont(11f, FontStyle.Bold);
            this.defaultFont = Utilities.GetDefaultFont(11f, FontStyle.Regular);
            this.treeListViewState = new TreeListViewState(this.tree);
            this.tree.DataSource = this.templatesSupport.TreeItems.Collection;
            this.tree.PopulateColumns();
            RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
            repositoryItemTextEdit.Click += new EventHandler((object sender, EventArgs args) => {
                TextEdit textEdit = sender as TextEdit;
                if (textEdit != null)
                {
                    textEdit.SelectAll();
                }
            });
            this.tree.RepositoryItems.Add(repositoryItemTextEdit);
            this.tree.Columns.Add();
            this.tree.Columns.Add();
            this.tree.Columns[0].Visible = false;
            this.tree.Columns[1].Visible = true;
            this.tree.Columns[1].FieldName = "_Description";
            this.tree.Columns[1].Caption = Resources.Description;
            this.tree.Columns[1].OptionsColumn.ReadOnly = true;
            this.tree.Columns[1].ColumnEdit = repositoryItemTextEdit;
            this.tree.Columns[1].UnboundType = UnboundColumnType.String;
            this.tree.Columns[1].Width = 0x3e8;
            this.tree.Columns[1].SortOrder = SortOrder.Ascending;
            this.tree.LookAndFeel.Style = LookAndFeelStyle.Skin;
            this.tree.LookAndFeel.SkinName = "Office 2010 Silver";
            this.tree.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tree.OptionsMenu.EnableColumnMenu = false;
            this.tree.StateImageList = this.imageCollection;
            this.tree.AfterFocusNode += new NodeEventHandler(this.tree_AfterFocusNode);
            this.tree.CellValueChanging += new CellValueChangedEventHandler(this.tree_CellValueChanging);
            this.tree.GetStateImage += new GetStateImageEventHandler(this.tree_GetStateImage);
            this.tree.NodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.tree_NodeCellStyle);
            this.tree.MouseMove += new MouseEventHandler(this.tree_MouseMove);
            this.tree.MouseLeave += new EventHandler(this.tree_MouseLeave);
            this.tree.CustomUnboundColumnData += new CustomColumnDataEventHandler(this.tree_CustomUnboundColumnData);
            this.tree.ShowingEditor += new CancelEventHandler(this.tree_ShowingEditor);
            this.tree.AfterExpand += new NodeEventHandler(this.tree_AfterExpand);
            this.tree.AfterCollapse += new NodeEventHandler(this.tree_AfterCollapse);
            this.tree.DoubleClick += new EventHandler(this.tree_DoubleClick);
            this.tree.KeyDown += new KeyEventHandler(this.tree_KeyDown);
            this.tree.FilterNode += new FilterNodeEventHandler(this.tree_OnFilterNode);
            this.tree.EndSorting += new EventHandler(this.tree_EndSorting);
            this.tree.OptionsBehavior.EnableFiltering = true;
            this.tree.OptionsFilter.FilterMode = FilterMode.Extended;
            this.tree.ExpandAll();
        }

        private void LoadResources()
        {
        }

        public void Modify()
        {
            if (this.GetCurrentTemplate() == null)
            {
                return;
            }
            if (this.OnModify != null)
            {
                this.OnModify(this, new EventArgs());
            }
        }

        public void RefreshSource(int selectedNodeID = -1)
        {
            this.tree.RefreshDataSource();
            if (selectedNodeID != -1)
            {
                this.SelectNodeByID(selectedNodeID);
            }
        }

        private void SelectFirstElement()
        {
            if (this.firstElementSelected)
            {
                return;
            }
            if (this.tree.Nodes.Count > 0)
            {
                this.tree.Nodes[0].Selected = true;
            }
            this.firstElementSelected = true;
        }

        private void SelectNodeByID(int nodeID)
        {
            try
            {
                TreeListNode treeListNode = this.tree.FindNodeByKeyID(nodeID);
                if (treeListNode != null)
                {
                    treeListNode.Selected = true;
                    treeListNode.ExpandAll();
                }
            }
            catch
            {
            }
        }

        private bool ShowTemplateForm(MainForm parentForm, Template template, bool creationMode)
        {
            return true;
        }

        private void tree_AfterCollapse(object sender, NodeEventArgs e)
        {
            if (e.Node.Level <= 1)
            {
                e.Node.StateImageIndex = 7;
            }
        }

        private void tree_AfterExpand(object sender, NodeEventArgs e)
        {
            if (e.Node.Level <= 1)
            {
                e.Node.StateImageIndex = 8;
            }
        }

        private void tree_AfterFocusNode(object sender, NodeEventArgs e)
        {
            if (this.OnSelected != null)
            {
                this.OnSelected(this, new EventArgs());
            }
        }

        private void tree_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
        }

        private void tree_CustomUnboundColumnData(object sender, TreeListCustomColumnDataEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Value = this.CastNodeToObjectType(e.Node);
                return;
            }
            Template template = this.CastNodeToTemplate(e.Node);
            if (template != null)
            {
                e.Value = template.DrawObject.Name;
            }
        }

        private void tree_DoubleClick(object sender, EventArgs e)
        {
            this.Modify();
        }

        private void tree_EndSorting(object sender, EventArgs e)
        {
            this.SelectFirstElement();
        }

        private void tree_GetStateImage(object sender, GetStateImageEventArgs e)
        {
        }

        private void tree_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode != Keys.Return)
            {
                switch (keyCode)
                {
                    case Keys.Insert:
                        {
                            if (Control.ModifierKeys != Keys.None && Control.ModifierKeys != Keys.Shift && Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Alt)
                            {
                                break;
                            }
                            Keys modifierKeys = Control.ModifierKeys;
                            if (modifierKeys <= Keys.Shift)
                            {
                                if (modifierKeys == Keys.None)
                                {
                                    this.Add("Area");
                                }
                                else if (modifierKeys == Keys.Shift)
                                {
                                    this.Add("Perimeter");
                                }
                            }
                            else if (modifierKeys == Keys.Control)
                            {
                                this.Add("Line");
                            }
                            else if (modifierKeys == Keys.Alt)
                            {
                                this.Add("Counter");
                            }
                            e.Handled = true;
                            return;
                        }
                    case Keys.Delete:
                        {
                            if (Control.ModifierKeys != Keys.None)
                            {
                                break;
                            }
                            this.Delete();
                            e.Handled = true;
                            break;
                        }
                    default:
                        {
                            if (keyCode != Keys.D)
                            {
                                return;
                            }
                            if (Control.ModifierKeys != Keys.Control)
                            {
                                break;
                            }
                            this.Duplicate();
                            e.Handled = true;
                            return;
                        }
                }
            }
            else if (Control.ModifierKeys == Keys.None)
            {
                this.Modify();
                e.Handled = true;
                return;
            }
        }

        private void tree_MouseLeave(object sender, EventArgs e)
        {
            this.HotTrackNode = null;
        }

        private void tree_MouseMove(object sender, MouseEventArgs e)
        {
            TreeListNode node;
            TreeList treeList = sender as TreeList;
            TreeListHitInfo treeListHitInfo = treeList.CalcHitInfo(new Point(e.X, e.Y));
            if (treeListHitInfo.HitInfoType == HitInfoType.Cell)
            {
                node = treeListHitInfo.Node;
            }
            else
            {
                node = null;
            }
            this.HotTrackNode = node;
        }

        private void tree_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            bool level = e.Node.Level == 0;
            if (!level || !(e.Column.Caption == Resources.Description))
            {
                e.Appearance.ForeColor = Color.DarkSlateGray;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
            e.Appearance.Font = (level ? this.titleFont : this.defaultFont);
            if (e.Node == this.HotTrackNode || e.Node.Selected)
            {
                e.Appearance.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                e.Appearance.BackColor = Color.FromArgb(252, 229, 126);
            }
        }

        private void tree_OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            this.Filtering(sender, e);
        }

        private void tree_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public event EventHandler OnCreateArea;

        public event EventHandler OnCreateCounter;

        public event EventHandler OnCreateLength;

        public event EventHandler OnCreatePerimeter;

        public event EventHandler OnDuplicate;

        public event EventHandler OnModify;

        public event EventHandler OnSelected;

        public enum NodeItemType
        {
            NotSelected,
            Section,
            Item
        }
    }
}