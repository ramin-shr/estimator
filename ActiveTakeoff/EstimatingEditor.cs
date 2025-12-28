using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using QuoterPlan.Properties;
using QuoterPlanControls;

namespace QuoterPlan
{
	public class EstimatingEditor
	{
		public event EventHandler OnEnable
		{
			add
			{
				EventHandler eventHandler = this.OnEnable;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnEnable, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnEnable;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnEnable, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnDisable
		{
			add
			{
				EventHandler eventHandler = this.OnDisable;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnDisable, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnDisable;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnDisable, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnModified
		{
			add
			{
				EventHandler eventHandler = this.OnModified;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnModified, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnModified;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnModified, value2, eventHandler2);
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

		private void InitializeTreeView()
		{
			this.titleFont = Utilities.GetDefaultFont(11f, FontStyle.Bold);
			this.defaultFont = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.treeListViewState = new TreeListViewState(this.tree);
			this.tree.DataSource = this.project.EstimatingItems.Collection;
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
			this.tree.Columns[0].Visible = false;
			this.tree.Columns[1].Visible = false;
			this.tree.Columns[2].Visible = false;
			this.tree.Columns[3].Visible = false;
			this.tree.Columns[4].Caption = Resources.Nom;
			this.tree.Columns[4].OptionsColumn.ReadOnly = true;
			this.tree.Columns[4].OptionsColumn.AllowEdit = false;
			this.tree.Columns[4].OptionsColumn.AllowSort = false;
			this.tree.Columns[5].Caption = Resources.Valeur;
			this.tree.Columns[5].OptionsColumn.ReadOnly = true;
			this.tree.Columns[5].OptionsColumn.AllowEdit = false;
			this.tree.Columns[5].OptionsColumn.AllowSort = false;
			this.tree.Columns[6].Caption = Resources.Unité;
			this.tree.Columns[6].OptionsColumn.ReadOnly = true;
			this.tree.Columns[6].OptionsColumn.AllowEdit = false;
			this.tree.Columns[6].OptionsColumn.AllowSort = false;
			this.tree.Columns[7].Caption = Resources.Coûtant;
			this.tree.Columns[7].Format.FormatType = FormatType.Numeric;
			this.tree.Columns[7].ColumnEdit = repositoryItemTextEdit;
			this.tree.Columns[7].AppearanceCell.BackColor = Color.AliceBlue;
			this.tree.Columns[7].OptionsColumn.AllowSort = false;
			this.tree.Columns[8].Caption = Resources.Markup;
			this.tree.Columns[8].Format.FormatType = FormatType.Numeric;
			this.tree.Columns[8].ColumnEdit = repositoryItemTextEdit;
			this.tree.Columns[8].AppearanceCell.BackColor = Color.AliceBlue;
			this.tree.Columns[8].OptionsColumn.AllowSort = false;
			this.tree.Columns[9].Caption = Resources.Prix;
			this.tree.Columns[9].Format.FormatType = FormatType.Numeric;
			this.tree.Columns[9].Format.FormatString = "c2";
			this.tree.Columns[9].OptionsColumn.ReadOnly = true;
			this.tree.Columns[9].OptionsColumn.AllowEdit = false;
			this.tree.Columns[9].OptionsColumn.AllowSort = false;
			this.tree.Columns[10].Caption = Resources.Total;
			this.tree.Columns[10].Format.FormatType = FormatType.Numeric;
			this.tree.Columns[10].Format.FormatString = "c2";
			this.tree.Columns[10].OptionsColumn.ReadOnly = true;
			this.tree.Columns[10].OptionsColumn.AllowEdit = false;
			this.tree.Columns[10].OptionsColumn.AllowSort = false;
			this.tree.Columns[11].Visible = false;
			this.tree.Columns[12].Visible = false;
			this.tree.LookAndFeel.Style = LookAndFeelStyle.Skin;
			this.tree.LookAndFeel.SkinName = "Office 2010 Silver";
			this.tree.LookAndFeel.UseDefaultLookAndFeel = false;
			this.tree.OptionsMenu.EnableColumnMenu = false;
			this.tree.CellValueChanged += this.tree_CellValueChanged;
			this.tree.GetStateImage += this.tree_GetStateImage;
			this.tree.NodeCellStyle += this.tree_NodeCellStyle;
			this.tree.MouseMove += this.tree_MouseMove;
			this.tree.MouseLeave += this.tree_MouseLeave;
			this.tree.ShowingEditor += new CancelEventHandler(this.tree_ShowingEditor);
			this.tree.ShownEditor += this.tree_ShownEditor;
			RepositoryItemTextEdit repositoryItemTextEdit2 = new RepositoryItemTextEdit();
			repositoryItemTextEdit2.Mask.MaskType = MaskType.Numeric;
			repositoryItemTextEdit2.Mask.EditMask = "c2";
			repositoryItemTextEdit2.Mask.UseMaskAsDisplayFormat = true;
			repositoryItemTextEdit2.Click += delegate(object sender, EventArgs args)
			{
				TextEdit textEdit = sender as TextEdit;
				if (textEdit != null)
				{
					textEdit.SelectAll();
				}
			};
			this.tree.Columns[7].ColumnEdit = repositoryItemTextEdit2;
			RepositoryItemTextEdit repositoryItemTextEdit3 = new RepositoryItemTextEdit();
			repositoryItemTextEdit3.Mask.MaskType = MaskType.Numeric;
			repositoryItemTextEdit3.Mask.EditMask = "P1";
			repositoryItemTextEdit3.Mask.UseMaskAsDisplayFormat = true;
			repositoryItemTextEdit3.Click += delegate(object sender, EventArgs args)
			{
				TextEdit textEdit = sender as TextEdit;
				if (textEdit != null)
				{
					textEdit.SelectAll();
				}
			};
			this.tree.Columns[8].ColumnEdit = repositoryItemTextEdit3;
		}

		public EstimatingEditor(Project project, DrawingArea drawingArea, TreeList tree, MainControl mainControl, ImageCollection imageCollection)
		{
			this.project = project;
			this.drawingArea = drawingArea;
			this.tree = tree;
			this.mainControl = mainControl;
			this.imageCollection = imageCollection;
			this.enabled = true;
			this.InitializeTreeView();
			this.LoadResources();
		}

		private DrawObject CastNodeToObject(TreeListNode node)
		{
			DrawObject result;
			try
			{
				result = this.drawingArea.FindObjectFromGroupID(this.project, (int)node.GetValue(0));
			}
			catch
			{
				result = null;
			}
			return result;
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
				if (!this.enabled)
				{
					if (this.OnDisable != null)
					{
						this.OnDisable(this, new EventArgs());
						return;
					}
				}
				else if (this.tree.Nodes.Count == 0)
				{
					if (this.OnDisable != null)
					{
						this.OnDisable(this, new EventArgs());
						return;
					}
				}
				else if (this.OnEnable != null)
				{
					this.OnEnable(this, new EventArgs());
				}
			}
		}

		public void SaveState()
		{
			this.treeListViewState.SaveState();
		}

		public void RestoreState()
		{
			this.treeListViewState.SaveState();
		}

		public void Rename(DrawObject groupObject)
		{
			this.project.EstimatingItems.Rename(groupObject);
			this.tree.RefreshDataSource();
		}

		public void Refresh(DrawObject groupObject)
		{
			this.project.EstimatingItems.Refresh(groupObject);
			this.tree.RefreshDataSource();
		}

		public void RefreshAll()
		{
			this.project.EstimatingItems.RefreshAll();
			this.tree.RefreshDataSource();
			this.tree.BestFitColumns();
			this.tree.ExpandAll();
			if (this.tree.Nodes.Count == 0)
			{
				if (this.OnDisable != null)
				{
					this.OnDisable(this, new EventArgs());
					return;
				}
			}
			else if (this.OnEnable != null)
			{
				this.OnEnable(this, new EventArgs());
			}
		}

		public void Clear()
		{
			try
			{
				this.tree.ClearNodes();
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void tree_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			try
			{
				int groupID = Utilities.ConvertToInt(e.Node.GetValue(0));
				string extensionID = e.Node.GetValue(1).ToString();
				string resultID = e.Node.GetValue(2).ToString();
				double num = Utilities.ConvertToDouble(e.Value, -1);
				if (e.Column.FieldName == "CostEach")
				{
					this.project.EstimatingItems.SaveEstimatingItemCost(groupID, extensionID, resultID, num, this.drawingArea.ActivePlan.UnitScale.CurrentSystemType);
				}
				else if (e.Column.FieldName == "MarkupEach")
				{
					this.project.EstimatingItems.SaveEstimatingItemMarkup(groupID, extensionID, resultID, num);
				}
				Console.WriteLine("Value = " + e.Value);
				if (this.OnModified != null)
				{
					this.OnModified(this, new EventArgs());
				}
			}
			catch
			{
			}
		}

		private void tree_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			bool flag = e.Node.Level == 0 && e.Column.Caption == Resources.Nom;
			if (e.Column.Caption == Resources.Nom)
			{
				e.Appearance.ForeColor = Color.Black;
			}
			else
			{
				if (e.Column.Caption == Resources.Coûtant)
				{
					e.Appearance.ForeColor = Color.DarkSlateBlue;
					try
					{
						e.Appearance.BackColor = ((e.Node.GetValue(11).ToString() == Utilities.GetCurrencySymbol()) ? Color.White : Color.AliceBlue);
						goto IL_FA;
					}
					catch
					{
						goto IL_FA;
					}
				}
				if (e.Column.Caption == Resources.Markup)
				{
					e.Appearance.ForeColor = Color.DarkSlateBlue;
				}
				else
				{
					e.Appearance.ForeColor = Color.DarkSlateGray;
				}
			}
			IL_FA:
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

		private void tree_ShowingEditor(object sender, CancelEventArgs e)
		{
		}

		private void tree_ShownEditor(object sender, EventArgs e)
		{
			try
			{
				if (this.tree.FocusedColumn.Caption == Resources.Coûtant && this.tree.FocusedNode.GetValue(11).ToString() == Utilities.GetCurrencySymbol())
				{
					this.tree.ActiveEditor.ReadOnly = true;
				}
			}
			catch
			{
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

		[CompilerGenerated]
		private static void <InitializeTreeView>b__1(object sender, EventArgs args)
		{
			TextEdit textEdit = sender as TextEdit;
			if (textEdit != null)
			{
				textEdit.SelectAll();
			}
		}

		[CompilerGenerated]
		private static void <InitializeTreeView>b__2(object sender, EventArgs args)
		{
			TextEdit textEdit = sender as TextEdit;
			if (textEdit != null)
			{
				textEdit.SelectAll();
			}
		}

		private bool enabled;

		private bool exitNow;

		private Project project;

		private TreeList tree;

		private MainControl mainControl;

		private DrawingArea drawingArea;

		private ImageCollection imageCollection;

		private Font titleFont;

		private Font defaultFont;

		private TreeListViewState treeListViewState;

		private EventHandler OnEnable;

		private EventHandler OnDisable;

		private EventHandler OnModified;

		private TreeListNode hotTrackNode;

		[CompilerGenerated]
		private static EventHandler CS$<>9__CachedAnonymousMethodDelegate3;

		[CompilerGenerated]
		private static EventHandler CS$<>9__CachedAnonymousMethodDelegate4;

		[CompilerGenerated]
		private static EventHandler CS$<>9__CachedAnonymousMethodDelegate5;
	}
}
