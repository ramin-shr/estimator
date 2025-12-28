using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
	public partial class ReportEditForm : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.panelGroup.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		private TriStateTreeViewComponent TreeView
		{
			get
			{
				return this.triStateTreeView;
			}
		}

		private static string NodeType(TreeNode node)
		{
			string result;
			try
			{
				object tag = node.Tag;
				result = tag.GetType().Name;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private static Plan CastNodeToPlan(TreeNode node)
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

		private static Layer CastNodeToLayer(TreeNode node)
		{
			Layer result;
			try
			{
				result = (Layer)node.Tag;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static DrawObject CastNodeToObject(TreeNode node)
		{
			DrawObject result;
			try
			{
				result = (DrawObject)node.Tag;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private void SaveFilterForAllNodes(TreeNodeCollection nodes, ref string filter)
		{
			foreach (object obj in nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				string text = ReportEditForm.NodeType(treeNode);
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Plan"))
					{
						if (a == "Layer")
						{
							Layer layer = ReportEditForm.CastNodeToLayer(treeNode);
							if (layer == null)
							{
								continue;
							}
							if (treeNode.StateImageIndex == 0)
							{
								filter = filter + "#LAYER:" + layer.Name + ":*;";
								continue;
							}
							if (treeNode.StateImageIndex == 2 && treeNode.Nodes.Count > 0)
							{
								filter = filter + "#LAYER:" + layer.Name + ":~;";
								this.SaveFilterForAllNodes(treeNode.Nodes, ref filter);
								continue;
							}
							continue;
						}
					}
					else
					{
						Plan plan = ReportEditForm.CastNodeToPlan(treeNode);
						if (plan == null)
						{
							continue;
						}
						if (treeNode.StateImageIndex == 0)
						{
							filter = filter + "#PLAN:" + plan.Name + ((treeNode.Level == 0) ? ":*;|" : ":*;");
							continue;
						}
						if (treeNode.StateImageIndex == 2 && treeNode.Nodes.Count > 0)
						{
							filter = filter + "#PLAN:" + plan.Name + ":~;";
							this.SaveFilterForAllNodes(treeNode.Nodes, ref filter);
							filter += ((treeNode.Level == 0) ? "|" : "");
							continue;
						}
						continue;
					}
				}
				DrawObject drawObject = ReportEditForm.CastNodeToObject(treeNode);
				if (drawObject != null)
				{
					if (treeNode.StateImageIndex == 0)
					{
						object obj2 = filter;
						filter = string.Concat(new object[]
						{
							obj2,
							"#GROUP:",
							drawObject.GroupID,
							(treeNode.Level == 0) ? ":*;|" : ":*;"
						});
					}
					else if (treeNode.StateImageIndex == 2 && treeNode.Nodes.Count > 0)
					{
						object obj3 = filter;
						filter = string.Concat(new object[]
						{
							obj3,
							"#GROUP:",
							drawObject.GroupID,
							":~;"
						});
						this.SaveFilterForAllNodes(treeNode.Nodes, ref filter);
						filter += ((treeNode.Level == 0) ? "|" : "");
					}
				}
			}
		}

		private void SaveFilter()
		{
			string text = "";
			this.SaveFilterForAllNodes(this.TreeView.Nodes, ref text);
			if (this.report.Order == Report.ReportOrderEnum.ReportOrderByPlans)
			{
				this.report.OrderByPlansFilter = text;
			}
			else
			{
				this.report.OrderByObjectsFilter = text;
			}
			Console.WriteLine(text);
		}

		private void ApplyFilterToNode(TreeNode node)
		{
			Plan plan = null;
			Layer layer = null;
			DrawObject drawObject = null;
			string text = ReportEditForm.NodeType(node);
			string a;
			if ((a = text) != null)
			{
				if (!(a == "Plan"))
				{
					if (a == "Layer")
					{
						layer = ReportEditForm.CastNodeToLayer(node);
						if (layer == null || this.report.Order != Report.ReportOrderEnum.ReportOrderByPlans)
						{
							return;
						}
						try
						{
							plan = ReportEditForm.CastNodeToPlan(node.Parent);
						}
						catch
						{
						}
						if (plan != null && this.filter.QueryFilter(plan.Name, layer.Name, -1, false))
						{
							node.Checked = false;
							return;
						}
						return;
					}
				}
				else
				{
					plan = ReportEditForm.CastNodeToPlan(node);
					if (plan == null)
					{
						return;
					}
					if (this.report.Order == Report.ReportOrderEnum.ReportOrderByPlans)
					{
						if (this.filter.QueryFilter(plan.Name, "", -1, false))
						{
							node.Checked = false;
							return;
						}
						return;
					}
					else
					{
						try
						{
							drawObject = ReportEditForm.CastNodeToObject(node.Parent);
						}
						catch
						{
						}
						if (drawObject != null && this.filter.QueryFilter(plan.Name, "", drawObject.GroupID, false))
						{
							node.Checked = false;
							return;
						}
						return;
					}
				}
			}
			drawObject = ReportEditForm.CastNodeToObject(node);
			if (drawObject != null)
			{
				if (this.report.Order == Report.ReportOrderEnum.ReportOrderByPlans)
				{
					try
					{
						layer = ReportEditForm.CastNodeToLayer(node.Parent);
					}
					catch
					{
					}
					if (layer != null)
					{
						try
						{
							plan = ReportEditForm.CastNodeToPlan(node.Parent.Parent);
						}
						catch
						{
						}
						if (plan != null && this.filter.QueryFilter(plan.Name, layer.Name, drawObject.GroupID, false))
						{
							node.Checked = false;
							return;
						}
					}
				}
				else if (this.filter.QueryFilter("", "", drawObject.GroupID, false))
				{
					node.Checked = false;
				}
			}
		}

		private void SetFilterForAllNodes(TreeNodeCollection nodes, bool value)
		{
			foreach (object obj in nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				treeNode.Checked = value;
				if (treeNode.Nodes.Count > 0)
				{
					this.SetFilterForAllNodes(treeNode.Nodes, value);
				}
			}
		}

		private void SetFilterForAllNodes(TreeNodeCollection nodes)
		{
			foreach (object obj in nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				this.ApplyFilterToNode(treeNode);
				if (treeNode.Nodes.Count > 0)
				{
					this.SetFilterForAllNodes(treeNode.Nodes);
				}
			}
		}

		private void LoadFilter()
		{
			this.filter.LoadFromString((this.report.Order == Report.ReportOrderEnum.ReportOrderByPlans) ? this.report.OrderByPlansFilter : this.report.OrderByObjectsFilter, this.report.Order);
			this.SetFilterForAllNodes(this.TreeView.Nodes);
		}

		private void FillObjects(ArrayList groups, Plan plan, TreeNode parentNode)
		{
			foreach (object obj in groups)
			{
				int groupID = (int)obj;
				int num = 0;
				DrawObject drawObject = (plan != null) ? this.drawArea.FindObjectFromGroupID(plan, groupID) : this.drawArea.FindObjectFromGroupID(this.project, groupID);
				if (drawObject != null)
				{
					TreeNode treeNode = (parentNode != null) ? parentNode.Nodes.Add(drawObject.Name) : this.TreeView.Nodes.Add(drawObject.Name);
					int num2 = -1;
					string objectType;
					if ((objectType = drawObject.ObjectType) != null)
					{
						if (!(objectType == "Area"))
						{
							if (!(objectType == "Perimeter"))
							{
								if (!(objectType == "Counter"))
								{
									if (objectType == "Line")
									{
										num2 = 5;
									}
								}
								else
								{
									num2 = 4;
								}
							}
							else
							{
								num2 = 3;
							}
						}
						else
						{
							num2 = 2;
						}
					}
					if (num2 > -1)
					{
						treeNode.ImageIndex = num2;
						treeNode.SelectedImageIndex = num2;
					}
					treeNode.Tag = drawObject;
					treeNode.Checked = true;
					if (plan == null)
					{
						int num3 = 0;
						foreach (object obj2 in this.project.Plans.Collection)
						{
							Plan plan2 = (Plan)obj2;
							if (this.drawArea.FindObjectFromGroupID(plan2, groupID) != null)
							{
								num3++;
							}
						}
						if (num3 > 1)
						{
							foreach (object obj3 in this.project.Plans.Collection)
							{
								Plan plan3 = (Plan)obj3;
								if (this.drawArea.FindObjectFromGroupID(plan3, groupID) != null)
								{
									plan3.SortIndex = num++;
									TreeNode treeNode2 = treeNode.Nodes.Add(plan3.Name);
									treeNode2.ImageIndex = 0;
									treeNode2.SelectedImageIndex = 0;
									treeNode2.Tag = plan3;
									treeNode2.Checked = true;
								}
							}
						}
					}
				}
			}
		}

		private void FillItemsByPlanOrder()
		{
			int num = 0;
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				bool flag = false;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (drawObject.IsPartOfGroup() && !drawObject.IsDeduction())
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					plan.SortIndex = num++;
					TreeNode treeNode = this.TreeView.Nodes.Add(plan.Name);
					treeNode.ImageIndex = 0;
					treeNode.SelectedImageIndex = 0;
					treeNode.Tag = plan;
					treeNode.Checked = true;
					int num2 = 0;
					foreach (object obj4 in plan.Layers.Collection)
					{
						Layer layer2 = (Layer)obj4;
						ArrayList arrayList = new ArrayList();
						for (int i = layer2.DrawingObjects.Count - 1; i >= 0; i--)
						{
							DrawObject drawObject2 = layer2.DrawingObjects[i];
							if (drawObject2.IsPartOfGroup() && !drawObject2.IsDeduction() && !arrayList.Contains(drawObject2.GroupID))
							{
								arrayList.Add(drawObject2.GroupID);
							}
						}
						if (arrayList.Count > 0)
						{
							layer2.SortIndex = num2++;
							TreeNode treeNode2 = treeNode.Nodes.Add(layer2.Name);
							treeNode2.ImageIndex = 1;
							treeNode2.SelectedImageIndex = 1;
							treeNode2.Tag = layer2;
							treeNode2.Checked = true;
							this.FillObjects(arrayList, plan, treeNode2);
						}
						arrayList.Clear();
					}
				}
			}
		}

		private void FillItemsByObjectOrder()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					for (int i = layer.DrawingObjects.Count - 1; i >= 0; i--)
					{
						DrawObject drawObject = layer.DrawingObjects[i];
						if (drawObject.IsPartOfGroup() && !drawObject.IsDeduction() && !arrayList.Contains(drawObject.GroupID))
						{
							arrayList.Add(drawObject.GroupID);
						}
					}
				}
			}
			this.FillObjects(arrayList, null, null);
			arrayList.Clear();
			arrayList = null;
		}

		private void FillItems()
		{
			this.TreeView.TreeViewNodeSorter = null;
			this.TreeView.Nodes.Clear();
			if (this.report.Order == Report.ReportOrderEnum.ReportOrderByPlans)
			{
				this.FillItemsByPlanOrder();
			}
			else
			{
				this.FillItemsByObjectOrder();
			}
			this.TreeView.TreeViewNodeSorter = new ReportEditForm.NodeSorter();
			this.TreeView.Sort();
			this.TreeView.ExpandAll();
		}

		public void CleanUpFilters()
		{
			if (this.report == null || this.project == null)
			{
				return;
			}
			Report.ReportOrderEnum order = this.report.Order;
			this.SaveFilter();
			this.report.Order = ((this.report.Order == Report.ReportOrderEnum.ReportOrderByObjects) ? Report.ReportOrderEnum.ReportOrderByPlans : Report.ReportOrderEnum.ReportOrderByObjects);
			this.TreeView.BeginUpdate();
			this.FillItems();
			this.LoadFilter();
			this.SaveFilter();
			this.CommitChanges();
			this.TreeView.EndUpdate();
			if (this.TreeView.Nodes.Count > 0)
			{
				this.TreeView.SelectedNode = this.TreeView.Nodes[0];
				this.TreeView.Nodes[0].EnsureVisible();
			}
			Utilities.SetObjectFocus(this.TreeView);
			this.project.Report.Order = order;
		}

		private bool CheckIfFilterEnabled()
		{
			bool applyFilter = this.project.Report.ApplyFilter;
			this.groupBox.Visible = applyFilter;
			this.panFilterDisabled.Visible = !applyFilter;
			this.btOk.Enabled = applyFilter;
			return applyFilter;
		}

		private void btEnableFilter_Click(object sender, EventArgs e)
		{
			this.groupBox.Visible = true;
			this.panFilterDisabled.Visible = false;
			this.btOk.Enabled = true;
			this.LoadValues();
		}

		private void LoadValues()
		{
			this.isQuoteReport = (this.project.Report.SelectedReportType == ReportTypeEnum.QuoteReport);
			this.report.Order = (this.isQuoteReport ? Report.ReportOrderEnum.ReportOrderByPlans : this.project.Report.Order);
			this.report.OrderByPlansFilter = this.project.Report.OrderByPlansFilter;
			this.report.OrderByObjectsFilter = this.project.Report.OrderByObjectsFilter;
			this.report.ReportSortBy = this.project.Report.ReportSortBy;
			this.report.QuoteReportSortBy = this.project.Report.QuoteReportSortBy;
			this.project.Report.Dirty = false;
			this.opOrderBySections.Visible = this.isQuoteReport;
			this.opOrderByItemTypes.Visible = this.isQuoteReport;
			this.opOrderByList.Visible = this.isQuoteReport;
			this.opOrderByObjects.Visible = !this.isQuoteReport;
			this.opOrderByPlans.Visible = !this.isQuoteReport;
			this.opOrderByLayers.Visible = !this.isQuoteReport;
			if (this.isQuoteReport)
			{
				this.exitNow = true;
				this.opOrderBySections.Checked = (this.report.QuoteReportSortBy == Report.QuoteReportSortByEnum.QuoteReportSortBySections);
				this.opOrderByItemTypes.Checked = (this.report.QuoteReportSortBy == Report.QuoteReportSortByEnum.QuoteReportSortByTypes);
				this.opOrderByList.Checked = (this.report.QuoteReportSortBy == Report.QuoteReportSortByEnum.QuoteReportSortByList);
				this.exitNow = false;
			}
			else
			{
				this.exitNow = true;
				this.opOrderByObjects.Checked = (this.report.Order == Report.ReportOrderEnum.ReportOrderByObjects);
				this.opOrderByPlans.Checked = (this.report.Order == Report.ReportOrderEnum.ReportOrderByPlans && this.report.ReportSortBy == Report.ReportSortByEnum.ReportSortByPlans);
				this.opOrderByLayers.Checked = (this.report.Order == Report.ReportOrderEnum.ReportOrderByPlans && this.report.ReportSortBy == Report.ReportSortByEnum.ReportSortByLayers);
				this.exitNow = false;
			}
			this.TreeView.BeginUpdate();
			this.FillItems();
			this.LoadFilter();
			this.TreeView.EndUpdate();
			if (this.TreeView.Nodes.Count > 0)
			{
				this.TreeView.SelectedNode = this.TreeView.Nodes[0];
				this.TreeView.Nodes[0].EnsureVisible();
			}
			Utilities.SetObjectFocus(this.TreeView);
		}

		public void Initialize()
		{
			if (this.report != null)
			{
				this.report.Clear();
				this.report = null;
			}
			if (this.filter != null)
			{
				this.filter.Clear();
				this.filter = null;
			}
			this.report = new Report();
			this.filter = new Filter();
			if (!this.CheckIfFilterEnabled())
			{
				return;
			}
			this.LoadValues();
		}

		public ReportEditForm(Project project, DrawingArea drawArea)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.project = project;
			this.drawArea = drawArea;
			this.TreeView.ImageList = this.smallImages;
		}

		private void ToggleOrder()
		{
			this.SaveFilter();
			if (this.isQuoteReport)
			{
				this.report.Order = Report.ReportOrderEnum.ReportOrderByPlans;
			}
			else
			{
				this.report.Order = (this.opOrderByObjects.Checked ? Report.ReportOrderEnum.ReportOrderByObjects : Report.ReportOrderEnum.ReportOrderByPlans);
			}
			this.TreeView.BeginUpdate();
			this.FillItems();
			this.LoadFilter();
			this.TreeView.EndUpdate();
			if (this.TreeView.Nodes.Count > 0)
			{
				this.TreeView.SelectedNode = this.TreeView.Nodes[0];
				this.TreeView.Nodes[0].EnsureVisible();
			}
			Utilities.SetObjectFocus(this.TreeView);
		}

		private void triStateTreeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			Console.WriteLine("triStateTreeView_AfterCheck");
		}

		private void opOrderByPlans_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.exitNow && (this.opOrderByPlans.Checked || this.opOrderByLayers.Checked) && this.report.Order != Report.ReportOrderEnum.ReportOrderByPlans)
			{
				Utilities.EnableInterface(this, false);
				this.ToggleOrder();
				Utilities.EnableInterface(this, true);
			}
		}

		private void opOrderByObjects_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.exitNow && this.opOrderByObjects.Checked)
			{
				Utilities.EnableInterface(this, false);
				this.ToggleOrder();
				Utilities.EnableInterface(this, true);
			}
		}

		private void CommitChanges()
		{
			this.project.Report.OrderByPlansFilter = this.report.OrderByPlansFilter;
			this.project.Report.OrderByObjectsFilter = this.report.OrderByObjectsFilter;
			if (this.isQuoteReport)
			{
				if (this.opOrderBySections.Checked)
				{
					this.project.Report.QuoteReportSortBy = Report.QuoteReportSortByEnum.QuoteReportSortBySections;
				}
				if (this.opOrderByItemTypes.Checked)
				{
					this.project.Report.QuoteReportSortBy = Report.QuoteReportSortByEnum.QuoteReportSortByTypes;
				}
				if (this.opOrderByList.Checked)
				{
					this.project.Report.QuoteReportSortBy = Report.QuoteReportSortByEnum.QuoteReportSortByList;
				}
			}
			else
			{
				this.project.Report.Order = (this.opOrderByObjects.Checked ? Report.ReportOrderEnum.ReportOrderByObjects : Report.ReportOrderEnum.ReportOrderByPlans);
				if (this.opOrderByPlans.Checked || this.opOrderByLayers.Checked)
				{
					this.project.Report.ReportSortBy = (this.opOrderByPlans.Checked ? Report.ReportSortByEnum.ReportSortByPlans : Report.ReportSortByEnum.ReportSortByLayers);
				}
			}
			this.project.Report.Dirty = true;
		}

		private void btSelectAll_Click(object sender, EventArgs e)
		{
			Utilities.EnableInterface(this, false);
			this.SetFilterForAllNodes(this.TreeView.Nodes, true);
			Utilities.EnableInterface(this, true);
		}

		private void btSelectNone_Click(object sender, EventArgs e)
		{
			Utilities.EnableInterface(this, false);
			this.SetFilterForAllNodes(this.TreeView.Nodes, false);
			Utilities.EnableInterface(this, true);
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			Utilities.EnableInterface(this, false);
			this.project.Report.ApplyFilter = true;
			this.SaveFilter();
			this.CommitChanges();
			Utilities.EnableInterface(this, true);
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void ReportEditForm_Resize(object sender, EventArgs e)
		{
			if (!this.activated)
			{
				return;
			}
			int num = (base.Width - this.windowDefaultSize.Width) / 2;
			int num2 = base.Height - this.windowDefaultSize.Height;
			this.lblOrder.Location = new Point(this.lblOrderBasePoint.X + num, this.lblOrderBasePoint.Y + num2);
			this.opOrderByItemTypes.Location = new Point(this.opOrderByItemTypesBasePoint.X + num, this.opOrderByItemTypesBasePoint.Y + num2);
			this.opOrderByLayers.Location = new Point(this.opOrderByLayersBasePoint.X + num, this.opOrderByLayersBasePoint.Y + num2);
			this.opOrderByList.Location = new Point(this.opOrderByListBasePoint.X + num, this.opOrderByListBasePoint.Y + num2);
			this.opOrderByObjects.Location = new Point(this.opOrderByObjectsBasePoint.X + num, this.opOrderByObjectsBasePoint.Y + num2);
			this.opOrderByPlans.Location = new Point(this.opOrderByPlansBasePoint.X + num, this.opOrderByPlansBasePoint.Y + num2);
			this.opOrderBySections.Location = new Point(this.opOrderBySectionsBasePoint.X + num, this.opOrderBySectionsBasePoint.Y + num2);
			this.btSelectAll.Location = new Point(this.btSelectAllBasePoint.X + num, this.btSelectAllBasePoint.Y + num2);
			this.btSelectNone.Location = new Point(this.btSelectNoneBasePoint.X + num, this.btSelectNoneBasePoint.Y + num2);
			this.lblSeparator.Location = new Point(this.lblSeparatorBasePoint.X + num, this.lblSeparatorBasePoint.Y + num2);
			this.lblFilterDisabled.Location = new Point(this.lblFilterDisabledBasePont.X + num, this.lblFilterDisabledBasePont.Y + num2);
			this.btEnableFilters.Location = new Point(this.btEnableFiltersBasePont.X + num, this.btEnableFiltersBasePont.Y + num2);
		}

		private void ReportEditForm_Activated(object sender, EventArgs e)
		{
			if (this.activated)
			{
				return;
			}
			this.lblOrderBasePoint = new Point(this.lblOrder.Location.X, this.lblOrder.Location.Y);
			this.opOrderByItemTypesBasePoint = new Point(this.opOrderByItemTypes.Location.X, this.opOrderByItemTypes.Location.Y);
			this.opOrderByLayersBasePoint = new Point(this.opOrderByLayers.Location.X, this.opOrderByLayers.Location.Y);
			this.opOrderByListBasePoint = new Point(this.opOrderByList.Location.X, this.opOrderByList.Location.Y);
			this.opOrderByObjectsBasePoint = new Point(this.opOrderByObjects.Location.X, this.opOrderByObjects.Location.Y);
			this.opOrderByPlansBasePoint = new Point(this.opOrderByPlans.Location.X, this.opOrderByPlans.Location.Y);
			this.opOrderBySectionsBasePoint = new Point(this.opOrderBySections.Location.X, this.opOrderBySections.Location.Y);
			this.btSelectAllBasePoint = new Point(this.btSelectAll.Location.X, this.btSelectAll.Location.Y);
			this.btSelectNoneBasePoint = new Point(this.btSelectNone.Location.X, this.btSelectNone.Location.Y);
			this.lblSeparatorBasePoint = new Point(this.lblSeparator.Location.X, this.lblSeparator.Location.Y);
			this.lblFilterDisabledBasePont = new Point(this.lblFilterDisabled.Location.X, this.lblFilterDisabled.Location.Y);
			this.btEnableFiltersBasePont = new Point(this.btEnableFilters.Location.X, this.btEnableFilters.Location.Y);
			this.windowDefaultSize = new Size(base.Width, base.Height);
			this.activated = true;
		}

		private Project project;

		private DrawingArea drawArea;

		private Point lblOrderBasePoint;

		private Point opOrderByItemTypesBasePoint;

		private Point opOrderByLayersBasePoint;

		private Point opOrderByListBasePoint;

		private Point opOrderByObjectsBasePoint;

		private Point opOrderByPlansBasePoint;

		private Point opOrderBySectionsBasePoint;

		private Point btSelectAllBasePoint;

		private Point btSelectNoneBasePoint;

		private Point lblSeparatorBasePoint;

		private Point lblFilterDisabledBasePont;

		private Point btEnableFiltersBasePont;

		private Size windowDefaultSize;

		private Report report;

		private Filter filter;

		private bool isQuoteReport;

		private bool activated;

		private bool exitNow;

		private class NodeSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				int result;
				try
				{
					TreeNode node = x as TreeNode;
					TreeNode node2 = y as TreeNode;
					string text = ReportEditForm.NodeType(node);
					string a;
					if ((a = text) != null)
					{
						if (a == "Plan")
						{
							Plan plan = ReportEditForm.CastNodeToPlan(node);
							Plan plan2 = ReportEditForm.CastNodeToPlan(node2);
							return StringLogicalComparer.Compare(plan.SortIndex.ToString(), plan2.SortIndex.ToString());
						}
						if (a == "Layer")
						{
							Layer layer = ReportEditForm.CastNodeToLayer(node);
							Layer layer2 = ReportEditForm.CastNodeToLayer(node2);
							return StringLogicalComparer.Compare(layer.SortIndex.ToString(), layer2.SortIndex.ToString());
						}
					}
					DrawObject drawObject = ReportEditForm.CastNodeToObject(node);
					DrawObject drawObject2 = ReportEditForm.CastNodeToObject(node2);
					if (drawObject.ObjectType != drawObject2.ObjectType)
					{
						result = StringLogicalComparer.Compare(drawObject.ObjectSortOrder.ToString(), drawObject2.ObjectSortOrder.ToString());
					}
					else
					{
						result = StringLogicalComparer.Compare(drawObject.Name, drawObject2.Name);
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}
		}
	}
}
