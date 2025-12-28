using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraTreeList.Nodes;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class TemplatesLibraryEditor
	{
		public event EventHandler OnCreateArea
		{
			add
			{
				EventHandler eventHandler = this.OnCreateArea;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreateArea, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnCreateArea;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreateArea, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnCreatePerimeter
		{
			add
			{
				EventHandler eventHandler = this.OnCreatePerimeter;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreatePerimeter, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnCreatePerimeter;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreatePerimeter, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnCreateLength
		{
			add
			{
				EventHandler eventHandler = this.OnCreateLength;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreateLength, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnCreateLength;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreateLength, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnCreateCounter
		{
			add
			{
				EventHandler eventHandler = this.OnCreateCounter;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreateCounter, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnCreateCounter;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnCreateCounter, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnSelected
		{
			add
			{
				EventHandler eventHandler = this.OnSelected;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnSelected, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnSelected;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnSelected, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnModify
		{
			add
			{
				EventHandler eventHandler = this.OnModify;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnModify, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnModify;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnModify, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnDuplicate
		{
			add
			{
				EventHandler eventHandler = this.OnDuplicate;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnDuplicate, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnDuplicate;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnDuplicate, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

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
					TreeListNode node = this.hotTrackNode;
					this.hotTrackNode = value;
					if (this.tree.ActiveEditor != null)
					{
						this.tree.PostEditor();
					}
					this.tree.RefreshNode(node);
					this.tree.RefreshNode(this.hotTrackNode);
				}
			}
		}

		private void LoadResources()
		{
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

		private void InitializeTreeView()
		{
			this.titleFont = Utilities.GetDefaultFont(11f, FontStyle.Bold);
			this.defaultFont = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.treeListViewState = new TreeListViewState(this.tree);
			this.tree.DataSource = this.templatesSupport.TreeItems.Collection;
			this.tree.PopulateColumns();
			RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
			repositoryItemTextEdit.Click += delegate(object sender, EventArgs args)
			{
				TextEdit textEdit = sender as TextEdit;
				if (textEdit != null)
				{
					textEdit.SelectAll();
				}
			};
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
			this.tree.Columns[1].Width = 1000;
			this.tree.Columns[1].SortOrder = SortOrder.Ascending;
			this.tree.LookAndFeel.Style = LookAndFeelStyle.Skin;
			this.tree.LookAndFeel.SkinName = "Office 2010 Silver";
			this.tree.LookAndFeel.UseDefaultLookAndFeel = false;
			this.tree.OptionsMenu.EnableColumnMenu = false;
			this.tree.StateImageList = this.imageCollection;
			this.tree.AfterFocusNode += this.tree_AfterFocusNode;
			this.tree.CellValueChanging += this.tree_CellValueChanging;
			this.tree.GetStateImage += this.tree_GetStateImage;
			this.tree.NodeCellStyle += this.tree_NodeCellStyle;
			this.tree.MouseMove += this.tree_MouseMove;
			this.tree.MouseLeave += this.tree_MouseLeave;
			this.tree.CustomUnboundColumnData += this.tree_CustomUnboundColumnData;
			this.tree.ShowingEditor += new CancelEventHandler(this.tree_ShowingEditor);
			this.tree.AfterExpand += this.tree_AfterExpand;
			this.tree.AfterCollapse += this.tree_AfterCollapse;
			this.tree.DoubleClick += this.tree_DoubleClick;
			this.tree.KeyDown += this.tree_KeyDown;
			this.tree.FilterNode += this.tree_OnFilterNode;
			this.tree.EndSorting += this.tree_EndSorting;
			this.tree.OptionsBehavior.EnableFiltering = true;
			this.tree.OptionsFilter.FilterMode = FilterMode.Extended;
			this.tree.ExpandAll();
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

		private string CastNodeToObjectType(TreeListNode node)
		{
			string result;
			try
			{
				result = Utilities.ConvertToString(node.GetValue(0), "");
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private Template CastNodeToTemplate(TreeListNode node)
		{
			Template result;
			try
			{
				result = (Template)node.GetValue(0);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private int GetNodeParentID()
		{
			int result;
			try
			{
				result = this.tree.FocusedNode.ParentNode.Id;
			}
			catch
			{
				result = -1;
			}
			return result;
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

		public Template GetCurrentTemplate()
		{
			Template result;
			try
			{
				if (this.tree.FocusedNode.Level > 0)
				{
					result = this.CastNodeToTemplate(this.tree.FocusedNode);
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
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

		public void Delete()
		{
			Template currentTemplate = this.GetCurrentTemplate();
			if (currentTemplate == null)
			{
				return;
			}
			string title = currentTemplate.DrawObject.Name + Environment.NewLine + Resources.Supprimer_ce_modèle;
			string si_vous_supprimez_ce_modèle_il_ne_sera_plus_disponible_pour_vos_prochains_projets = Resources.Si_vous_supprimez_ce_modèle_il_ne_sera_plus_disponible_pour_vos_prochains_projets;
			if (Utilities.DisplayDeleteConfirmation(title, si_vous_supprimez_ce_modèle_il_ne_sera_plus_disponible_pour_vos_prochains_projets) == DialogResult.Yes)
			{
				Utilities.FileDelete(currentTemplate.FullFileName, true);
				this.templatesSupport.RemoveTemplate(currentTemplate);
				this.tree.RefreshDataSource();
				this.CleanUpTree();
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

		public TemplatesLibraryEditor.NodeItemType GetSelectedNodeType()
		{
			TemplatesLibraryEditor.NodeItemType result;
			try
			{
				int level = this.tree.FocusedNode.Level;
				if (level == 0)
				{
					result = TemplatesLibraryEditor.NodeItemType.Section;
				}
				else
				{
					result = TemplatesLibraryEditor.NodeItemType.Item;
				}
			}
			catch
			{
				result = TemplatesLibraryEditor.NodeItemType.NotSelected;
			}
			return result;
		}

		private void tree_OnFilterNode(object sender, FilterNodeEventArgs e)
		{
			this.Filtering(sender, e);
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
			Type type = this.tree.GetType();
			MethodInfo method = type.GetMethod("FilterNodeOnFilterCriteria", BindingFlags.Instance | BindingFlags.NonPublic);
			while (parentNode != null)
			{
				object[] parameters = new object[]
				{
					parentNode
				};
				if ((bool)method.Invoke(sender, parameters))
				{
					e.Node.Visible = true;
					e.Handled = true;
					return;
				}
				parentNode = parentNode.ParentNode;
			}
		}

		private void tree_EndSorting(object sender, EventArgs e)
		{
			this.SelectFirstElement();
		}

		private void tree_AfterFocusNode(object sender, NodeEventArgs e)
		{
			if (this.OnSelected != null)
			{
				this.OnSelected(this, new EventArgs());
			}
		}

		private void tree_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
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

		private void tree_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
		}

		private void tree_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			bool flag = e.Node.Level == 0;
			if (flag && e.Column.Caption == Resources.Description)
			{
				e.Appearance.ForeColor = Color.Black;
			}
			else
			{
				e.Appearance.ForeColor = Color.DarkSlateGray;
			}
			e.Appearance.Font = (flag ? this.titleFont : this.defaultFont);
			if (e.Node == this.HotTrackNode || e.Node.Selected)
			{
				e.Appearance.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
				e.Appearance.BackColor = Color.FromArgb(252, 229, 126);
			}
		}

		private void tree_MouseMove(object sender, MouseEventArgs e)
		{
			TreeList treeList = sender as TreeList;
			TreeListHitInfo treeListHitInfo = treeList.CalcHitInfo(new Point(e.X, e.Y));
			this.HotTrackNode = ((treeListHitInfo.HitInfoType == HitInfoType.Cell) ? treeListHitInfo.Node : null);
		}

		private void tree_MouseLeave(object sender, EventArgs e)
		{
			this.HotTrackNode = null;
		}

		private void tree_GetStateImage(object sender, GetStateImageEventArgs e)
		{
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

		private void tree_DoubleClick(object sender, EventArgs e)
		{
			this.Modify();
		}

		private void tree_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Insert:
					if (Control.ModifierKeys == Keys.None || Control.ModifierKeys == Keys.Shift || Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Alt)
					{
						Keys modifierKeys = Control.ModifierKeys;
						if (modifierKeys <= Keys.Shift)
						{
							if (modifierKeys != Keys.None)
							{
								if (modifierKeys == Keys.Shift)
								{
									this.Add("Perimeter");
								}
							}
							else
							{
								this.Add("Area");
							}
						}
						else if (modifierKeys != Keys.Control)
						{
							if (modifierKeys == Keys.Alt)
							{
								this.Add("Counter");
							}
						}
						else
						{
							this.Add("Line");
						}
						e.Handled = true;
						return;
					}
					break;
				case Keys.Delete:
					if (Control.ModifierKeys == Keys.None)
					{
						this.Delete();
						e.Handled = true;
					}
					break;
				default:
					if (keyCode != Keys.D)
					{
						return;
					}
					if (Control.ModifierKeys == Keys.Control)
					{
						this.Duplicate();
						e.Handled = true;
						return;
					}
					break;
				}
			}
			else if (Control.ModifierKeys == Keys.None)
			{
				this.Modify();
				e.Handled = true;
				return;
			}
		}

		[CompilerGenerated]
		private static void <InitializeTreeView>b__0(object sender, EventArgs args)
		{
			TextEdit textEdit = sender as TextEdit;
			if (textEdit != null)
			{
				textEdit.SelectAll();
			}
		}

		private bool enabled;

		private TemplatesSupport templatesSupport;

		private TreeList tree;

		private ImageCollection imageCollection;

		private bool firstElementSelected;

		private Font titleFont;

		private Font defaultFont;

		private TreeListViewState treeListViewState;

		private EventHandler OnCreateArea;

		private EventHandler OnCreatePerimeter;

		private EventHandler OnCreateLength;

		private EventHandler OnCreateCounter;

		private EventHandler OnSelected;

		private EventHandler OnModify;

		private EventHandler OnDuplicate;

		private TreeListNode hotTrackNode;

		private MainForm mainForm;

		[CompilerGenerated]
		private static EventHandler CS$<>9__CachedAnonymousMethodDelegate1;

		public enum NodeItemType
		{
			NotSelected,
			Section,
			Item
		}
	}
}
