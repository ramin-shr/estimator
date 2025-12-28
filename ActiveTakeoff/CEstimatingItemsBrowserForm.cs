using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraTreeList.Nodes;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class CEstimatingItemsBrowserForm : BaseForm
	{
		public event OnSelectDatabaseHandler OnSelectDatabase;

		public event OnLoadEstimatingSectionsHandler OnLoadEstimatingSections;

		public event OnLoadEstimatingItemsHandler OnLoadEstimatingItems;

		private TreeListNode TreeTrackNode
		{
			get
			{
				return this.treeTrackNode;
			}
			set
			{
				if (this.treeTrackNode != value)
				{
					TreeListNode node = this.treeTrackNode;
					this.treeTrackNode = value;
					if (this.treeSections.ActiveEditor != null)
					{
						this.treeSections.PostEditor();
					}
					this.treeSections.RefreshNode(node);
					this.treeSections.RefreshNode(this.treeTrackNode);
				}
			}
		}

		private TreeListNode ListTrackNode
		{
			get
			{
				return this.listTrackNode;
			}
			set
			{
				if (this.listTrackNode != value)
				{
					TreeListNode node = this.listTrackNode;
					this.listTrackNode = value;
					if (this.listItems.ActiveEditor != null)
					{
						this.listItems.PostEditor();
					}
					this.listItems.RefreshNode(node);
					this.listItems.RefreshNode(this.listTrackNode);
				}
			}
		}

		public CEstimatingItem SelectedProduct { get; set; }

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
		}

		private void InitializeTreeView(bool isCOffice = false)
		{
			this.activated = false;
			this.currentNode = null;
			this.titleFont = Utilities.GetDefaultFont(11f, FontStyle.Bold);
			this.defaultFont = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			if (this.sections == null)
			{
				return;
			}
			this.treeSections.DataSource = this.sections.Collection;
			this.treeSections.PopulateColumns();
			this.treeSections.Columns.Add();
			this.treeSections.Columns[0].Visible = false;
			this.treeSections.Columns[1].Visible = true;
			this.treeSections.Columns[1].FieldName = "_Description";
			this.treeSections.Columns[1].Caption = Resources.Description;
			this.treeSections.Columns[1].UnboundType = UnboundColumnType.String;
			this.treeSections.Columns[1].OptionsColumn.ReadOnly = true;
			this.treeSections.Columns[1].OptionsColumn.AllowEdit = false;
			this.treeSections.Columns[1].OptionsColumn.AllowSort = false;
			this.treeSections.Columns[1].SortOrder = (isCOffice ? SortOrder.None : SortOrder.Ascending);
			this.treeSections.LookAndFeel.Style = LookAndFeelStyle.Skin;
			this.treeSections.LookAndFeel.SkinName = "Office 2010 Silver";
			this.treeSections.LookAndFeel.UseDefaultLookAndFeel = false;
			this.treeSections.OptionsMenu.EnableColumnMenu = false;
			this.treeSections.StateImageList = this.imageCollection;
			this.treeSections.GetStateImage += this.tree_GetStateImage;
			this.treeSections.NodeCellStyle += this.tree_NodeCellStyle;
			this.treeSections.MouseMove += this.tree_MouseMove;
			this.treeSections.MouseLeave += this.tree_MouseLeave;
			this.treeSections.FocusedNodeChanged += this.treeSections_FocusedNodeChanged;
			this.treeSections.CustomUnboundColumnData += this.tree_CustomUnboundColumnData;
			this.treeSections.RefreshDataSource();
			base.Focus();
		}

		private void InitializeList(CEstimatingItems products)
		{
			if (products == null)
			{
				return;
			}
			this.listItems.BeginUpdate();
			try
			{
				this.listItems.Columns.Clear();
				this.listItems.DataSource = products.Collection;
				this.listItems.PopulateColumns();
			}
			catch (Exception)
			{
			}
			this.listItems.Columns[0].Visible = false;
			this.listItems.Columns[1].Caption = Resources.Description;
			this.listItems.Columns[1].OptionsColumn.ReadOnly = true;
			this.listItems.Columns[1].OptionsColumn.AllowEdit = false;
			this.listItems.Columns[1].OptionsColumn.AllowSort = false;
			this.listItems.Columns[1].SortOrder = SortOrder.Ascending;
			this.listItems.Columns[2].Caption = Resources.Coûtant;
			this.listItems.Columns[2].Format.FormatType = FormatType.Numeric;
			this.listItems.Columns[2].Format.FormatString = "c2";
			this.listItems.Columns[2].OptionsColumn.ReadOnly = true;
			this.listItems.Columns[2].OptionsColumn.AllowEdit = false;
			this.listItems.Columns[2].OptionsColumn.AllowSort = false;
			this.listItems.Columns[3].Caption = Resources.Unité;
			this.listItems.Columns[3].OptionsColumn.ReadOnly = true;
			this.listItems.Columns[3].OptionsColumn.AllowEdit = false;
			this.listItems.Columns[3].OptionsColumn.AllowSort = false;
			this.listItems.Columns[4].Visible = false;
			this.listItems.Columns[5].Visible = false;
			this.listItems.Columns[6].Visible = false;
			this.listItems.Columns[7].Visible = false;
			this.listItems.Columns[8].Visible = false;
			this.listItems.Columns[9].Visible = false;
			this.listItems.Columns[10].Visible = false;
			this.listItems.Columns[11].Visible = false;
			this.listItems.Columns[12].Visible = false;
			this.listItems.Columns[13].Visible = false;
			this.listItems.Columns[14].Visible = false;
			this.listItems.Columns[15].Visible = false;
			this.listItems.Columns[16].Visible = false;
			this.listItems.LookAndFeel.Style = LookAndFeelStyle.Skin;
			this.listItems.LookAndFeel.SkinName = "Office 2010 Silver";
			this.listItems.LookAndFeel.UseDefaultLookAndFeel = false;
			this.listItems.OptionsMenu.EnableColumnMenu = false;
			this.listItems.NodeCellStyle += this.list_NodeCellStyle;
			this.listItems.MouseMove += this.list_MouseMove;
			this.listItems.MouseLeave += this.list_MouseLeave;
			this.listItems.DoubleClick += this.listItems_DoubleClick;
			this.listItems.BestFitColumns();
			if (this.listItems.Nodes.Count > 0)
			{
				this.listItems.Nodes[0].Selected = true;
			}
			this.listItems.RefreshDataSource();
			this.listItems.EndUpdate();
		}

		public CEstimatingItemsBrowserForm(ImageCollection imageCollection)
		{
			this.InitializeComponent();
			this.InitializeFonts();
			this.LoadResources();
			this.imageCollection = imageCollection;
		}

		private CEstimatingSection CastNodeToSection(TreeListNode node)
		{
			CEstimatingSection result;
			try
			{
				result = (CEstimatingSection)node.GetValue(0);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private CEstimatingItem CastListToProduct(TreeListNode node)
		{
			CEstimatingItem result;
			try
			{
				result = (CEstimatingItem)node.GetValue(5);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private void SelectFirstNode()
		{
			if (this.treeSections.Nodes.Count > 0 && this.mustSelectNode)
			{
				this.treeSections.Nodes[0].Selected = true;
				this.mustSelectNode = false;
			}
		}

		public void EnableCOCommercialDatabase(bool enable)
		{
			this.lblSelect.Visible = enable;
			this.btCommercialDatabase.Visible = enable;
			this.btResidentialDatabase.Visible = enable;
			this.Text = Resources.Items_d_estimation;
		}

		public void RefreshSections(bool isCOffice = false)
		{
			this.listItems.DataSource = null;
			this.treeSections.DataSource = null;
			if (this.OnLoadEstimatingSections != null)
			{
				this.OnLoadEstimatingSections(ref this.sections);
			}
			Application.DoEvents();
			this.InitializeTreeView(isCOffice);
			this.activated = true;
			if (this.treeSections.Selection.Count > 0)
			{
				this.currentNode = this.treeSections.Selection[0];
				this.RefreshProducts(this.currentNode);
				return;
			}
			this.SelectFirstNode();
		}

		private void RefreshProducts(TreeListNode node)
		{
			this.listItems.DataSource = null;
			if (node.Level == 0)
			{
				this.listItems.RefreshDataSource();
				return;
			}
			CEstimatingSection cestimatingSection = this.CastNodeToSection(node);
			if (cestimatingSection == null)
			{
				this.listItems.RefreshDataSource();
				return;
			}
			Console.WriteLine("section.Name = " + cestimatingSection.Name);
			if (this.OnLoadEstimatingItems != null)
			{
				this.OnLoadEstimatingItems(cestimatingSection, ref this.products);
			}
			this.InitializeList(this.products);
		}

		private void btItemsExpandAll_Click(object sender, EventArgs e)
		{
			this.treeSections.ExpandAll();
		}

		private void btItemsCollapseAll_Click(object sender, EventArgs e)
		{
			this.treeSections.CollapseAll();
		}

		private void btResidentialDatabase_Click(object sender, EventArgs e)
		{
			this.btResidentialDatabase.Checked = true;
			this.btCommercialDatabase.Checked = false;
			if (this.OnSelectDatabase != null)
			{
				bool flag = false;
				this.OnSelectDatabase("COResidentialDatabase", ref flag);
				if (flag)
				{
					return;
				}
				this.RefreshSections(true);
			}
		}

		private void btCommercialDatabase_Click(object sender, EventArgs e)
		{
			this.btCommercialDatabase.Checked = true;
			this.btResidentialDatabase.Checked = false;
			if (this.OnSelectDatabase != null)
			{
				bool flag = false;
				this.OnSelectDatabase("COCommercialDatabase", ref flag);
				if (flag)
				{
					return;
				}
				this.RefreshSections(true);
			}
		}

		private void btSelect_Click(object sender, EventArgs e)
		{
			if (this.listItems.Selection.Count > 0)
			{
				this.SelectedProduct = this.CastListToProduct(this.listItems.Selection[0]);
			}
			else
			{
				this.SelectedProduct = null;
			}
			if (this.SelectedProduct == null)
			{
				return;
			}
			base.Hide();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			this.SelectedProduct = null;
			base.Hide();
		}

		private void DoResize()
		{
			this.listItems.Height = this.panelEx3.Height - this.panelEx4.Height;
			this.btSelect.Left = base.Width / 2 - (this.btSelect.Width / 2 + this.btCancel.Width / 2 + 10);
			this.btCancel.Left = this.btSelect.Left + this.btSelect.Width + 5;
		}

		private void CEstimatingItemsBrowserForm_Resize(object sender, EventArgs e)
		{
			this.DoResize();
		}

		private void expandableSplitter1_SplitterMoving(object sender, SplitterEventArgs e)
		{
			this.DoResize();
		}

		private void expandableSplitter1_SplitterMoved(object sender, SplitterEventArgs e)
		{
			this.DoResize();
		}

		private void tree_CustomUnboundColumnData(object sender, TreeListCustomColumnDataEventArgs e)
		{
			if (e.Column.AbsoluteIndex == 1)
			{
				CEstimatingSection cestimatingSection = this.CastNodeToSection(e.Node);
				if (cestimatingSection != null)
				{
					e.Value = cestimatingSection.Name;
				}
			}
		}

		private void tree_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			bool flag = e.Node.Level == 0;
			e.Appearance.Font = (flag ? this.titleFont : this.defaultFont);
			if (e.Node == this.TreeTrackNode || e.Node.Selected)
			{
				e.Appearance.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
				e.Appearance.BackColor = Color.FromArgb(252, 229, 126);
			}
		}

		private void tree_MouseMove(object sender, MouseEventArgs e)
		{
			TreeList treeList = sender as TreeList;
			TreeListHitInfo treeListHitInfo = treeList.CalcHitInfo(new Point(e.X, e.Y));
			this.TreeTrackNode = ((treeListHitInfo.HitInfoType == HitInfoType.Cell) ? treeListHitInfo.Node : null);
		}

		private void tree_MouseLeave(object sender, EventArgs e)
		{
			this.TreeTrackNode = null;
		}

		private void tree_GetStateImage(object sender, GetStateImageEventArgs e)
		{
			e.NodeImageIndex = 7;
		}

		private void list_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			if (e.Node == this.ListTrackNode || e.Node.Selected)
			{
				e.Appearance.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
				e.Appearance.BackColor = Color.FromArgb(252, 229, 126);
			}
		}

		private void list_MouseMove(object sender, MouseEventArgs e)
		{
			TreeListHitInfo treeListHitInfo = this.listItems.CalcHitInfo(new Point(e.X, e.Y));
			this.ListTrackNode = ((treeListHitInfo.HitInfoType == HitInfoType.Cell) ? treeListHitInfo.Node : null);
		}

		private void list_MouseLeave(object sender, EventArgs e)
		{
			this.ListTrackNode = null;
		}

		private void list_GetStateImage(object sender, GetStateImageEventArgs e)
		{
			e.NodeImageIndex = 7;
		}

		private void listItems_DoubleClick(object sender, EventArgs e)
		{
			TreeListHitInfo treeListHitInfo = this.listItems.CalcHitInfo(this.listItems.PointToClient(Control.MousePosition));
			if (treeListHitInfo.Node != null)
			{
				this.SelectedProduct = this.CastListToProduct(treeListHitInfo.Node);
				if (this.SelectedProduct != null)
				{
					base.Hide();
				}
			}
		}

		private void treeSections_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			if (!this.activated)
			{
				return;
			}
			if (this.currentNode != null && object.Equals(this.currentNode, e.Node))
			{
				return;
			}
			this.RefreshProducts(e.Node);
			this.currentNode = e.Node;
		}

		private void CEstimatingItemsBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				this.SelectedProduct = null;
				e.Cancel = true;
				base.Hide();
			}
		}

		private void CEstimatingItemsBrowserForm_Activated(object sender, EventArgs e)
		{
			this.activated = true;
			this.SelectFirstNode();
		}

		private bool exitNow;

		private bool activated;

		private TreeViewNodes sections;

		private CEstimatingItems products;

		private Font titleFont;

		private Font defaultFont;

		private ImageCollection imageCollection;

		private bool mustSelectNode = true;

		private TreeListNode treeTrackNode;

		private TreeListNode listTrackNode;

		private TreeListNode currentNode;
	}
}
