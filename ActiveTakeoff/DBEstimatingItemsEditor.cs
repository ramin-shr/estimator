using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraTreeList.Nodes;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace QuoterPlan
{
    public class DBEstimatingItemsEditor
    {
        private bool enabled;

        private DBManagement dbManagement;

        private TreeList tree;

        private ImageCollection imageCollection;

        private bool firstElementSelected;

        private Font titleFont;

        private Font defaultFont;

        private TreeListViewState treeListViewState;

        private TreeListNode hotTrackNode;

        private MainForm mainForm;

        [CompilerGenerated]
        // CS$<>9__CachedAnonymousMethodDelegate2
        private static EventHandler CSu0024u003cu003e9__CachedAnonymousMethodDelegate2;

        [CompilerGenerated]
        // CS$<>9__CachedAnonymousMethodDelegate3
        private static EventHandler CSu0024u003cu003e9__CachedAnonymousMethodDelegate3;

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

        public DBEstimatingItemsEditor(MainForm mainForm, DBManagement dbManagement, TreeList tree, ImageCollection imageCollection)
        {
            this.mainForm = mainForm;
            this.dbManagement = dbManagement;
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

        [CompilerGenerated]
        // <InitializeTreeView>b__1
        private static void u003cInitializeTreeViewu003eb__1(object sender, EventArgs args)
        {
            TextEdit textEdit = sender as TextEdit;
            if (textEdit != null)
            {
                textEdit.SelectAll();
            }
        }

        public void Add(DBEstimatingItem.EstimatingItemType itemType)
        {
            string str = "";
            string str1 = "";
            int num = 0;
            double num1 = 0;
            double num2 = 1;
            double num3 = 0;
            int d = 1;
            int d1 = 0x44c;
            string str2 = "";
            bool flag = false;
            DBEstimatingSection currentSection = this.GetCurrentSection();
            if (currentSection != null)
            {
                d = currentSection.ID;
            }
            DBEstimatingSection currentSubSection = this.GetCurrentSubSection();
            if (currentSubSection != null)
            {
                d1 = currentSubSection.ID;
            }
            int nextAvailableIndex = this.dbManagement.EstimatingItems.GetNextAvailableIndex();
            DBEstimatingItem dBEstimatingItem = new DBEstimatingItem(nextAvailableIndex, itemType, str, str1, (DBEstimatingItem.UnitMeasureType)num, num1, num2, num3, d, d1, str2, flag);
            if (this.ShowEstimatingItemForm(this.mainForm, dBEstimatingItem, true))
            {
                int num4 = this.dbManagement.AddEstimatingItem(dBEstimatingItem);
                this.tree.RefreshDataSource();
                this.SelectNodeByID(num4);
                if (this.OnDBItemCreated != null)
                {
                    this.dbManagement.EstimatingItems.NextAvailableIndex = nextAvailableIndex + 1;
                    this.OnDBItemCreated(dBEstimatingItem);
                }
            }
        }

        private DBEstimatingItem CastNodeToItem(object node)
        {
            DBEstimatingItem dBEstimatingItem;
            try
            {
                dBEstimatingItem = (DBEstimatingItem)node;
            }
            catch
            {
                dBEstimatingItem = null;
            }
            return dBEstimatingItem;
        }

        private DBEstimatingItem CastNodeToItem(TreeListNode node)
        {
            DBEstimatingItem value;
            try
            {
                value = (DBEstimatingItem)node.GetValue(0);
            }
            catch
            {
                value = null;
            }
            return value;
        }

        private DBEstimatingSection CastNodeToSection(int index)
        {
            DBEstimatingSection item;
            try
            {
                item = this.dbManagement.CSICodesA[index];
            }
            catch
            {
                item = null;
            }
            return item;
        }

        private DBEstimatingSection CastNodeToSection(TreeListNode node)
        {
            DBEstimatingSection item;
            try
            {
                item = this.dbManagement.CSICodesA[Utilities.ConvertToInt(node.GetValue(0))];
            }
            catch
            {
                item = null;
            }
            return item;
        }

        private DBEstimatingSection CastNodeToSubSection(int index)
        {
            DBEstimatingSection item;
            try
            {
                item = this.dbManagement.CSICodesB[index];
            }
            catch
            {
                item = null;
            }
            return item;
        }

        private DBEstimatingSection CastNodeToSubSection(TreeListNode node)
        {
            DBEstimatingSection item;
            try
            {
                item = this.dbManagement.CSICodesB[Utilities.ConvertToInt(node.GetValue(0))];
            }
            catch
            {
                item = null;
            }
            return item;
        }

        private void CleanUpSubSections(TreeListNodes nodes)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (nodes[i].Level == 1 && !nodes[i].HasChildren)
                {
                    this.dbManagement.TreeItems.Delete(this.CastNodeToSubSection(nodes[i]));
                    nodes.RemoveAt(i);
                }
            }
        }

        private void CleanUpTree()
        {
            for (int i = this.tree.Nodes.Count - 1; i >= 0; i--)
            {
                if (this.tree.Nodes[i].Level == 0)
                {
                    this.CleanUpSubSections(this.tree.Nodes[i].Nodes);
                    if (!this.tree.Nodes[i].HasChildren)
                    {
                        this.dbManagement.TreeItems.Delete(this.CastNodeToSubSection(this.tree.Nodes[i]));
                        this.tree.Nodes.RemoveAt(i);
                    }
                }
            }
        }

        public void Delete()
        {
            DBEstimatingItem currentEstimatingItem = this.GetCurrentEstimatingItem();
            if (currentEstimatingItem == null)
            {
                return;
            }
            if (currentEstimatingItem.IsSystemItem)
            {
                return;
            }
            if (Utilities.DisplayDeleteConfirmation(string.Concat(currentEstimatingItem.Description, Environment.NewLine, Resources.Supprimer_cet_item_), Resources.Si_vous_supprimez_cet_item_il_ne_sera_plus_disponible_dans_vos_projets) == DialogResult.Yes)
            {
                if (this.OnDBItemDeleted != null)
                {
                    this.OnDBItemDeleted(currentEstimatingItem);
                }
                this.dbManagement.TreeItems.Delete(currentEstimatingItem);
                this.dbManagement.EstimatingItems.Delete(currentEstimatingItem);
                this.tree.RefreshDataSource();
                this.CleanUpTree();
            }
        }

        public void Duplicate()
        {
            DBEstimatingItem currentEstimatingItem = this.GetCurrentEstimatingItem();
            if (currentEstimatingItem == null)
            {
                return;
            }
            int nextAvailableIndex = this.dbManagement.EstimatingItems.GetNextAvailableIndex();
            DBEstimatingItem dBEstimatingItem = currentEstimatingItem.Duplicate(nextAvailableIndex);
            dBEstimatingItem.IsSystemItem = false;
            if (this.ShowEstimatingItemForm(this.mainForm, dBEstimatingItem, true))
            {
                int num = this.dbManagement.AddEstimatingItem(dBEstimatingItem);
                this.tree.RefreshDataSource();
                this.SelectNodeByID(num);
                if (this.OnDBItemCreated != null)
                {
                    this.dbManagement.EstimatingItems.NextAvailableIndex = nextAvailableIndex + 1;
                    this.OnDBItemCreated(dBEstimatingItem);
                }
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

        private DBEstimatingItem GetCurrentEstimatingItem()
        {
            DBEstimatingItem item;
            try
            {
                if (this.tree.FocusedNode.Level <= 1)
                {
                    item = null;
                }
                else
                {
                    item = this.CastNodeToItem(this.tree.FocusedNode);
                }
            }
            catch
            {
                item = null;
            }
            return item;
        }

        private DBEstimatingSection GetCurrentSection()
        {
            DBEstimatingSection section;
            try
            {
                switch (this.tree.FocusedNode.Level)
                {
                    case 0:
                        {
                            section = this.CastNodeToSection(this.tree.FocusedNode);
                            return section;
                        }
                    case 1:
                        {
                            section = this.CastNodeToSection(this.tree.FocusedNode.ParentNode);
                            return section;
                        }
                    case 2:
                        {
                            section = this.CastNodeToSection(this.tree.FocusedNode.ParentNode.ParentNode);
                            return section;
                        }
                }
                section = null;
            }
            catch
            {
                section = null;
            }
            return section;
        }

        private DBEstimatingSection GetCurrentSubSection()
        {
            DBEstimatingSection subSection;
            try
            {
                switch (this.tree.FocusedNode.Level)
                {
                    case 0:
                        {
                            subSection = this.CastNodeToSubSection(this.tree.FocusedNode.Nodes[0]);
                            return subSection;
                        }
                    case 1:
                        {
                            subSection = this.CastNodeToSubSection(this.tree.FocusedNode);
                            return subSection;
                        }
                    case 2:
                        {
                            subSection = this.CastNodeToSubSection(this.tree.FocusedNode.ParentNode);
                            return subSection;
                        }
                }
                subSection = null;
            }
            catch
            {
                subSection = null;
            }
            return subSection;
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

        public DBEstimatingItemsEditor.NodeItemType GetSelectedNodeType()
        {
            DBEstimatingItemsEditor.NodeItemType nodeItemType;
            try
            {
                switch (this.tree.FocusedNode.Level)
                {
                    case 0:
                        {
                            nodeItemType = DBEstimatingItemsEditor.NodeItemType.Section;
                            return nodeItemType;
                        }
                    case 1:
                        {
                            nodeItemType = DBEstimatingItemsEditor.NodeItemType.SubSection;
                            return nodeItemType;
                        }
                }
                nodeItemType = (!this.CastNodeToItem(this.tree.FocusedNode).IsSystemItem ? DBEstimatingItemsEditor.NodeItemType.EstimatingItem : DBEstimatingItemsEditor.NodeItemType.EstimatingSystemItem);
            }
            catch
            {
                nodeItemType = DBEstimatingItemsEditor.NodeItemType.NotSelected;
            }
            return nodeItemType;
        }

        private void InitializeTreeView()
        {
            this.titleFont = Utilities.GetDefaultFont(11f, FontStyle.Bold);
            this.defaultFont = Utilities.GetDefaultFont(11f, FontStyle.Regular);
            this.treeListViewState = new TreeListViewState(this.tree);
            this.tree.DataSource = this.dbManagement.TreeItems.Collection;
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
            this.tree.Columns.Add();
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
            this.tree.Columns[2].Visible = true;
            this.tree.Columns[2].FieldName = "_UnitOfMeasure";
            this.tree.Columns[2].Caption = Resources.Recouvrement;
            this.tree.Columns[2].OptionsColumn.ReadOnly = true;
            this.tree.Columns[2].ColumnEdit = repositoryItemTextEdit;
            this.tree.Columns[2].UnboundType = UnboundColumnType.String;
            this.tree.Columns[2].Width = 90;
            this.tree.Columns[2].MinWidth = 90;
            this.tree.Columns[3].Visible = true;
            this.tree.Columns[3].FieldName = "_Price";
            this.tree.Columns[3].Caption = Resources.Prix;
            this.tree.Columns[3].Format.FormatType = FormatType.Numeric;
            this.tree.Columns[3].Format.FormatString = "c2";
            this.tree.Columns[3].OptionsColumn.ReadOnly = false;
            this.tree.Columns[3].UnboundType = UnboundColumnType.Decimal;
            this.tree.Columns[3].Width = 120;
            this.tree.Columns[3].MinWidth = 120;
            this.tree.Columns[4].Visible = true;
            this.tree.Columns[4].FieldName = "_PurchaseUnit";
            this.tree.Columns[4].Caption = Resources.Unité_d_achat;
            this.tree.Columns[4].OptionsColumn.ReadOnly = true;
            this.tree.Columns[4].ColumnEdit = repositoryItemTextEdit;
            this.tree.Columns[4].UnboundType = UnboundColumnType.String;
            this.tree.Columns[4].Width = 90;
            this.tree.Columns[4].MinWidth = 90;
            this.tree.Columns[5].Visible = true;
            this.tree.Columns[5].FieldName = "_BidCode";
            this.tree.Columns[5].Caption = Resources.Code_produit;
            this.tree.Columns[5].OptionsColumn.ReadOnly = true;
            this.tree.Columns[5].ColumnEdit = repositoryItemTextEdit;
            this.tree.Columns[5].UnboundType = UnboundColumnType.String;
            this.tree.Columns[5].Width = 150;
            this.tree.Columns[5].MinWidth = 150;
            this.tree.LookAndFeel.Style = LookAndFeelStyle.Skin;
            this.tree.LookAndFeel.SkinName = "Office 2010 Silver";
            this.tree.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tree.OptionsMenu.EnableColumnMenu = false;
            this.tree.StateImageList = this.imageCollection;
            this.tree.AfterFocusNode += new NodeEventHandler(this.tree_AfterFocusNode);
            this.tree.CellValueChanged += new CellValueChangedEventHandler(this.tree_CellValueChanged);
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
            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            repositoryItemTextEdit1.Mask.MaskType = MaskType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "c2";
            repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemTextEdit1.Click += new EventHandler((object sender, EventArgs args) => {
                TextEdit textEdit = sender as TextEdit;
                if (textEdit != null)
                {
                    textEdit.SelectAll();
                }
            });
            this.tree.Columns[3].ColumnEdit = repositoryItemTextEdit1;
            this.tree.ExpandAll();
        }

        private void LoadResources()
        {
        }

        public void Modify()
        {
            DBEstimatingItem currentEstimatingItem = this.GetCurrentEstimatingItem();
            if (currentEstimatingItem == null)
            {
                return;
            }
            int num = -1;
            int sectionID = currentEstimatingItem.SectionID;
            int subSectionID = currentEstimatingItem.SubSectionID;
            double priceEach = currentEstimatingItem.PriceEach;
            if (this.ShowEstimatingItemForm(this.mainForm, currentEstimatingItem, false))
            {
                if (currentEstimatingItem.SectionID != sectionID || currentEstimatingItem.SubSectionID != subSectionID)
                {
                    this.dbManagement.TreeItems.Delete(currentEstimatingItem);
                    num = this.dbManagement.InsertTreeNode(currentEstimatingItem);
                }
                this.tree.RefreshDataSource();
                if (num != -1)
                {
                    this.SelectNodeByID(num);
                }
                if (this.OnDBItemModified != null)
                {
                    this.OnDBItemModified(currentEstimatingItem, priceEach);
                }
            }
        }

        public void Refresh()
        {
            this.tree.RefreshDataSource();
        }

        private void SelectFirstElement()
        {
            if (this.firstElementSelected)
            {
                return;
            }
            if (this.tree.Nodes.Count > 0)
            {
                foreach (TreeListNode node in this.tree.Nodes)
                {
                    if (node.Level != 0)
                    {
                        continue;
                    }
                    try
                    {
                        if (node.GetValue(1).ToString().StartsWith("00001"))
                        {
                            node.Selected = true;
                            this.firstElementSelected = true;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
            }
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

        private bool ShowEstimatingItemForm(MainForm parentForm, DBEstimatingItem estimatingItem, bool creationMode)
        {
            estimatingItem.Dirty = false;
            try
            {
                using (DBEstimatingItemForm dBEstimatingItemForm = new DBEstimatingItemForm(this.dbManagement, estimatingItem, creationMode))
                {
                    dBEstimatingItemForm.HelpUtilities = parentForm.HelpUtilities;
                    dBEstimatingItemForm.HelpContextString = "DBEstimatingForm";
                    dBEstimatingItemForm.ShowDialog(parentForm);
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
            bool dirty = estimatingItem.Dirty;
            estimatingItem.Dirty = false;
            return dirty;
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

        private void tree_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            DBEstimatingItem item = this.CastNodeToItem(e.Node);
            if (item != null)
            {
                double priceEach = item.PriceEach;
                item.PriceEach = Utilities.ConvertToDouble(e.Value, -1);
                if (priceEach != item.PriceEach)
                {
                    if (this.OnDBItemModified != null)
                    {
                        this.OnDBItemModified(item, priceEach);
                    }
                    Application.DoEvents();
                    this.tree.Refresh();
                }
            }
        }

        private void tree_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
        }

        private void tree_CustomUnboundColumnData(object sender, TreeListCustomColumnDataEventArgs e)
        {
            TreeListNode node = e.Node;
            TreeViewNode row = (TreeViewNode)e.Row;
            if (e.Column.AbsoluteIndex != 1)
            {
                if (row.Tag.GetType() == typeof(DBEstimatingItem))
                {
                    DBEstimatingItem item = this.CastNodeToItem(row.Tag);
                    if (item != null)
                    {
                        switch (e.Column.AbsoluteIndex)
                        {
                            case 2:
                                {
                                    if (item.UnitMeasure != DBEstimatingItem.UnitMeasureType.none)
                                    {
                                        double coverageRate = item.CoverageRate;
                                        e.Value = string.Concat(coverageRate.ToString(), " ", item.UnitMeasureCaption);
                                        return;
                                    }
                                    e.Value = (item.CoverageRate == 1 ? "" : item.CoverageRate.ToString());
                                    return;
                                }
                            case 3:
                                {
                                    e.Value = item.PriceEach;
                                    return;
                                }
                            case 4:
                                {
                                    e.Value = (item.PurchaseUnit == "" ? "" : string.Concat("/ ", item.PurchaseUnit));
                                    return;
                                }
                            case 5:
                                {
                                    e.Value = item.BidCode;
                                    break;
                                }
                            default:
                                {
                                    return;
                                }
                        }
                    }
                }
            }
            else if (row.Tag.GetType() == typeof(int))
            {
                if (row.ParentID != 0)
                {
                    DBEstimatingSection subSection = this.CastNodeToSubSection(Utilities.ConvertToInt(row.Tag));
                    if (subSection != null)
                    {
                        int d = subSection.ID;
                        e.Value = string.Concat(d.ToString("D5"), " - ", subSection.Name);
                        return;
                    }
                }
                else
                {
                    DBEstimatingSection section = this.CastNodeToSection(Utilities.ConvertToInt(row.Tag));
                    if (section != null)
                    {
                        int num = section.ID;
                        e.Value = string.Concat(num.ToString("D5"), " - ", section.Name);
                        return;
                    }
                }
            }
            else if (row.Tag.GetType() == typeof(DBEstimatingItem))
            {
                DBEstimatingItem dBEstimatingItem = this.CastNodeToItem(row.Tag);
                if (dBEstimatingItem != null)
                {
                    e.Value = dBEstimatingItem.Description;
                    return;
                }
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
            try
            {
                if (e.Node.Level > 1)
                {
                    switch (this.CastNodeToItem(e.Node).ItemType)
                    {
                        case DBEstimatingItem.EstimatingItemType.MaterialItem:
                            {
                                e.NodeImageIndex = 2;
                                break;
                            }
                        case DBEstimatingItem.EstimatingItemType.LaborItem:
                            {
                                e.NodeImageIndex = 3;
                                break;
                            }
                        case DBEstimatingItem.EstimatingItemType.SubcontractItem:
                            {
                                e.NodeImageIndex = 5;
                                break;
                            }
                        case DBEstimatingItem.EstimatingItemType.EquipmentItem:
                            {
                                e.NodeImageIndex = 4;
                                break;
                            }
                    }
                }
            }
            catch
            {
            }
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
                                    this.Add(DBEstimatingItem.EstimatingItemType.MaterialItem);
                                }
                                else if (modifierKeys == Keys.Shift)
                                {
                                    this.Add(DBEstimatingItem.EstimatingItemType.LaborItem);
                                }
                            }
                            else if (modifierKeys == Keys.Control)
                            {
                                this.Add(DBEstimatingItem.EstimatingItemType.EquipmentItem);
                            }
                            else if (modifierKeys == Keys.Alt)
                            {
                                this.Add(DBEstimatingItem.EstimatingItemType.SubcontractItem);
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
            bool flag = (e.Node.Level == 0 ? true : e.Node.Level == 1);
            if (flag && e.Column.Caption == Resources.Description)
            {
                e.Appearance.ForeColor = Color.Black;
            }
            else if (flag || !(e.Column.Caption == Resources.Prix))
            {
                e.Appearance.ForeColor = Color.DarkSlateGray;
            }
            else
            {
                e.Appearance.ForeColor = Color.DarkSlateBlue;
                e.Appearance.BackColor = Color.AliceBlue;
            }
            e.Appearance.Font = (flag ? this.titleFont : this.defaultFont);
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
            bool flag;
            CancelEventArgs cancelEventArg = e;
            if (this.tree.FocusedNode.Level < 2)
            {
                flag = true;
            }
            else
            {
                flag = (this.tree.FocusedNode.Level != 2 ? false : this.tree.FocusedColumn.Caption != Resources.Prix);
            }
            cancelEventArg.Cancel = flag;
        }

        public event DBEstimatingItemEventHandler OnDBItemCreated;

        public event DBEstimatingItemEventHandler OnDBItemDeleted;

        public event DBEstimatingItemUpdateEventHandler OnDBItemModified;

        public event EventHandler OnSelected;

        public enum NodeItemType
        {
            NotSelected,
            Section,
            SubSection,
            EstimatingItem,
            EstimatingSystemItem
        }
    }
}