using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraTreeList.Nodes;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class CEstimatingsItemsControl : BaseUserControl
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CEstimatingsItemsControl));
			this.barProducts = new Bar();
			this.btProductAdd = new ButtonItem();
			this.btProductRemove = new ButtonItem();
			this.btFormula = new ButtonItem();
			this.listItems = new TreeList();
			this.panelControl = new DevExpress.XtraEditors.PanelControl();
			this.txtCoverageRate = new TextBoxEx();
			this.txtFormula = new TextBoxEx();
			this.lblFormula = new LabelEx();
			this.lblCoverageRate = new LabelEx();
			((ISupportInitialize)this.barProducts).BeginInit();
			((ISupportInitialize)this.listItems).BeginInit();
			((ISupportInitialize)this.panelControl).BeginInit();
			this.panelControl.SuspendLayout();
			base.SuspendLayout();
			this.barProducts.CanAutoHide = false;
			this.barProducts.CanCustomize = false;
			this.barProducts.CanDockBottom = false;
			this.barProducts.CanDockLeft = false;
			this.barProducts.CanDockRight = false;
			this.barProducts.CanDockTab = false;
			this.barProducts.CanDockTop = false;
			this.barProducts.CanMove = false;
			this.barProducts.CanReorderTabs = false;
			this.barProducts.CanUndock = false;
			this.barProducts.ColorScheme.PredefinedColorScheme = ePredefinedColorScheme.Silver2003;
			this.barProducts.Dock = DockStyle.Top;
			this.barProducts.DockTabAlignment = eTabStripAlignment.Left;
			this.barProducts.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.barProducts.Items.AddRange(new BaseItem[]
			{
				this.btProductAdd,
				this.btProductRemove,
				this.btFormula
			});
			this.barProducts.Location = new Point(0, 0);
			this.barProducts.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.barProducts.Name = "barProducts";
			this.barProducts.RoundCorners = false;
			this.barProducts.SaveLayoutChanges = false;
			this.barProducts.Size = new Size(338, 25);
			this.barProducts.Stretch = true;
			this.barProducts.Style = eDotNetBarStyle.StyleManagerControlled;
			this.barProducts.TabIndex = 29;
			this.barProducts.TabStop = false;
			this.barProducts.Text = "barLayers";
			this.btProductAdd.Image = (Image)componentResourceManager.GetObject("btProductAdd.Image");
			this.btProductAdd.ImageFixedSize = new Size(16, 16);
			this.btProductAdd.Name = "btProductAdd";
			this.btProductAdd.Click += this.btProductAdd_Click;
			this.btProductRemove.Image = Resources.delete_16x16;
			this.btProductRemove.ImageFixedSize = new Size(16, 16);
			this.btProductRemove.Name = "btProductRemove";
			this.btProductRemove.Click += this.btProductRemove_Click;
			this.btFormula.BeginGroup = true;
			this.btFormula.Image = (Image)componentResourceManager.GetObject("btFormula.Image");
			this.btFormula.ImageFixedSize = new Size(16, 16);
			this.btFormula.Name = "btFormula";
			this.btFormula.Click += this.btProductLink_Click;
			this.listItems.Dock = DockStyle.Top;
			this.listItems.Font = new Font("Segoe UI", 9f);
			this.listItems.Location = new Point(0, 25);
			this.listItems.LookAndFeel.SkinName = "Office 2010 Silver";
			this.listItems.Name = "listItems";
			this.listItems.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
			this.listItems.OptionsView.ShowHorzLines = false;
			this.listItems.OptionsView.ShowIndicator = false;
			this.listItems.OptionsView.ShowVertLines = false;
			this.listItems.Size = new Size(338, 257);
			this.listItems.TabIndex = 0;
			this.panelControl.Controls.Add(this.txtCoverageRate);
			this.panelControl.Controls.Add(this.txtFormula);
			this.panelControl.Controls.Add(this.lblFormula);
			this.panelControl.Controls.Add(this.lblCoverageRate);
			this.panelControl.Dock = DockStyle.Bottom;
			this.panelControl.Location = new Point(0, 343);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new Size(338, 105);
			this.panelControl.TabIndex = 0;
			this.panelControl.Visible = false;
			this.txtCoverageRate.AssociatedLabel = this.txtCoverageRate;
			this.txtCoverageRate.Font = new Font("Segoe UI", 9f);
			this.txtCoverageRate.Location = new Point(9, 73);
			this.txtCoverageRate.MaxLength = 50;
			this.txtCoverageRate.Name = "txtCoverageRate";
			this.txtCoverageRate.ReadOnly = true;
			this.txtCoverageRate.Size = new Size(322, 23);
			this.txtCoverageRate.TabIndex = 3;
			this.txtCoverageRate.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			this.txtCoverageRate.Enter += this.txtValue_Enter;
			this.txtFormula.AssociatedLabel = this.lblFormula;
			this.txtFormula.Font = new Font("Segoe UI", 9f);
			this.txtFormula.Location = new Point(9, 25);
			this.txtFormula.MaxLength = 50;
			this.txtFormula.Name = "txtFormula";
			this.txtFormula.ReadOnly = true;
			this.txtFormula.Size = new Size(322, 23);
			this.txtFormula.TabIndex = 2;
			this.txtFormula.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			this.txtFormula.Enter += this.txtValue_Enter;
			this.lblFormula.AutoSize = true;
			this.lblFormula.Font = new Font("Segoe UI", 9f);
			this.lblFormula.ForeColor = Color.Black;
			this.lblFormula.ImeMode = ImeMode.NoControl;
			this.lblFormula.Location = new Point(6, 6);
			this.lblFormula.Name = "lblFormula";
			this.lblFormula.Size = new Size(54, 15);
			this.lblFormula.TabIndex = 105;
			this.lblFormula.Text = "Formula:";
			this.lblFormula.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			this.lblCoverageRate.AutoSize = true;
			this.lblCoverageRate.Font = new Font("Segoe UI", 9f);
			this.lblCoverageRate.ForeColor = Color.Black;
			this.lblCoverageRate.ImeMode = ImeMode.NoControl;
			this.lblCoverageRate.Location = new Point(6, 54);
			this.lblCoverageRate.Name = "lblCoverageRate";
			this.lblCoverageRate.Size = new Size(83, 15);
			this.lblCoverageRate.TabIndex = 104;
			this.lblCoverageRate.Text = "Coverage rate:";
			this.lblCoverageRate.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panelControl);
			base.Controls.Add(this.listItems);
			base.Controls.Add(this.barProducts);
			base.Name = "CEstimatingsItemsControl";
			base.Size = new Size(338, 448);
			base.Resize += this.CEstimatingsItemsControl_Resize;
			((ISupportInitialize)this.barProducts).EndInit();
			((ISupportInitialize)this.listItems).EndInit();
			((ISupportInitialize)this.panelControl).EndInit();
			this.panelControl.ResumeLayout(false);
			this.panelControl.PerformLayout();
			base.ResumeLayout(false);
		}

		public event EventHandler OnProductAdd
		{
			add
			{
				EventHandler eventHandler = this.OnProductAdd;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnProductAdd, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnProductAdd;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnProductAdd, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnProductRemove
		{
			add
			{
				EventHandler eventHandler = this.OnProductRemove;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnProductRemove, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnProductRemove;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnProductRemove, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public event EventHandler OnFormulaEdit
		{
			add
			{
				EventHandler eventHandler = this.OnFormulaEdit;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnFormulaEdit, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnFormulaEdit;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnFormulaEdit, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
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

		public CEstimatingItem SelectedProduct
		{
			get
			{
				if (this.listItems.Selection.Count > 0)
				{
					return this.CastListToProduct(this.listItems.Selection[0]);
				}
				return null;
			}
		}

		private double Opacity
		{
			get
			{
				return base.FindForm().Opacity;
			}
		}

		private void LoadResources()
		{
			this.lblCoverageRate.Text = Resources.Facteur_de_recouvrement_;
			this.lblFormula.Text = Resources.Formule_;
		}

		private void InitializeFonts()
		{
			this.Font = Utilities.GetDefaultFont();
			this.listItems.Font = Utilities.GetDefaultFont();
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

		public CEstimatingsItemsControl(Project project)
		{
			this.InitializeComponent();
			this.project = project;
			this.LoadResources();
			this.InitializeFonts();
		}

		private void InitializeList(CEstimatingItems products)
		{
			this.listItems.Enabled = true;
			this.listItems.DataSource = products.Collection;
			this.listItems.PopulateColumns();
			if (this.project != null)
			{
				this.listItems.Columns.Add();
				this.listItems.Columns.Add();
				this.listItems.Columns[0].Visible = false;
				this.listItems.Columns[1].Caption = Resources.Description;
				this.listItems.Columns[1].OptionsColumn.ReadOnly = true;
				this.listItems.Columns[1].OptionsColumn.AllowEdit = false;
				this.listItems.Columns[1].OptionsColumn.AllowSort = false;
				this.listItems.Columns[2].Visible = false;
				this.listItems.Columns[3].Visible = false;
				this.listItems.Columns[3].Caption = Resources.Unité;
				this.listItems.Columns[3].OptionsColumn.ReadOnly = true;
				this.listItems.Columns[3].OptionsColumn.AllowEdit = false;
				this.listItems.Columns[3].OptionsColumn.AllowSort = false;
				this.listItems.Columns[4].Visible = false;
				this.listItems.Columns[5].Visible = false;
				this.listItems.Columns[6].Visible = false;
				this.listItems.Columns[7].Visible = (this.groupObject != null);
				this.listItems.Columns[7].FieldName = "_Value";
				this.listItems.Columns[7].Caption = Resources.Valeur;
				this.listItems.Columns[7].OptionsColumn.ReadOnly = true;
				this.listItems.Columns[7].OptionsColumn.AllowEdit = false;
				this.listItems.Columns[7].OptionsColumn.AllowSort = false;
				this.listItems.Columns[7].UnboundType = UnboundColumnType.Decimal;
				this.listItems.Columns[8].Visible = true;
				this.listItems.Columns[8].FieldName = "_Unit";
				this.listItems.Columns[8].Caption = Resources.Unité;
				this.listItems.Columns[8].OptionsColumn.ReadOnly = true;
				this.listItems.Columns[8].OptionsColumn.AllowEdit = false;
				this.listItems.Columns[8].OptionsColumn.AllowSort = false;
				this.listItems.Columns[8].UnboundType = UnboundColumnType.String;
				this.listItems.Columns[9].Visible = false;
				this.listItems.Columns[10].Visible = false;
				this.listItems.Columns[11].Visible = false;
				this.listItems.Columns[12].Visible = false;
				this.listItems.Columns[13].Visible = false;
				this.listItems.Columns[14].Visible = false;
				this.listItems.Columns[15].Visible = false;
				this.listItems.Columns[16].Visible = false;
				this.listItems.Columns[17].Visible = false;
				this.listItems.Columns[18].Visible = false;
			}
			else
			{
				this.listItems.Columns[0].Visible = false;
				this.listItems.Columns[1].Caption = Resources.Description;
				this.listItems.Columns[1].OptionsColumn.ReadOnly = true;
				this.listItems.Columns[1].OptionsColumn.AllowEdit = false;
				this.listItems.Columns[1].OptionsColumn.AllowSort = false;
				this.listItems.Columns[2].Visible = false;
				this.listItems.Columns[3].Visible = true;
				this.listItems.Columns[3].Caption = Resources.Unité;
				this.listItems.Columns[3].OptionsColumn.ReadOnly = true;
				this.listItems.Columns[3].OptionsColumn.AllowEdit = false;
				this.listItems.Columns[3].OptionsColumn.AllowSort = false;
				this.listItems.Columns[4].Caption = Resources.Formule;
				this.listItems.Columns[4].OptionsColumn.ReadOnly = true;
				this.listItems.Columns[4].OptionsColumn.AllowEdit = false;
				this.listItems.Columns[4].OptionsColumn.AllowSort = false;
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
			}
			this.listItems.LookAndFeel.Style = LookAndFeelStyle.Skin;
			this.listItems.LookAndFeel.SkinName = "Office 2010 Silver";
			this.listItems.LookAndFeel.UseDefaultLookAndFeel = false;
			this.listItems.OptionsMenu.EnableColumnMenu = false;
			this.listItems.NodeCellStyle += this.list_NodeCellStyle;
			this.listItems.MouseMove += this.list_MouseMove;
			this.listItems.MouseLeave += this.list_MouseLeave;
			this.listItems.AfterFocusNode += this.list_AfterFocusNode;
			this.listItems.DoubleClick += this.listItems_DoubleClick;
			this.listItems.FocusedNodeChanged += this.listItems_FocusedNodeChanged;
			if (this.project != null)
			{
				this.listItems.CustomUnboundColumnData += this.listItems_CustomUnboundColumnData;
			}
			this.listItems.Columns[1].Width = 130;
			this.listItems.Columns[1].MinWidth = 50;
			this.listItems.Columns[3].Width = 10;
			this.listItems.Columns[3].MinWidth = 10;
			this.listItems.Columns[4].MinWidth = 120;
			if (this.project != null)
			{
				this.listItems.Columns[7].Width = 10;
				this.listItems.Columns[8].Width = 10;
				this.listItems.Columns[8].MinWidth = 10;
			}
			this.listItems.RefreshDataSource();
			if (this.listItems.Nodes.Count > 0)
			{
				this.listItems.Nodes[0].Selected = true;
			}
		}

		private void EnableMenu(bool enable = true)
		{
			if (this.listItems.Nodes.Count == 0)
			{
				this.ResizeLayout(false);
			}
			this.btProductAdd.Enabled = enable;
			this.btProductRemove.Enabled = (enable && this.listItems.Nodes.Count > 0);
			this.btFormula.Enabled = (enable && this.listItems.Nodes.Count > 0);
		}

		public void Disable()
		{
			this.listItems.Nodes.Clear();
			this.listItems.Enabled = false;
			this.EnableMenu(false);
		}

		public void UpdateGUI(CEstimatingItems products)
		{
			this.UpdateGUI(products, null);
		}

		public void UpdateGUI(CEstimatingItems products, DrawObject groupObject)
		{
			if (groupObject != null && groupObject.Group != null)
			{
				foreach (CEstimatingItem cestimatingItem in groupObject.Group.EstimatingItems.Collection)
				{
					cestimatingItem.Value = -1.0;
				}
			}
			this.groupObject = groupObject;
			this.InitializeList(products);
			this.EnableMenu(true);
		}

		public void RefreshList()
		{
			this.listItems.RefreshDataSource();
			this.EnableMenu(true);
		}

		public void SelectItem(CEstimatingItem product)
		{
			foreach (object obj in this.listItems.Nodes)
			{
				TreeListNode treeListNode = (TreeListNode)obj;
				CEstimatingItem cestimatingItem = this.CastListToProduct(treeListNode);
				if (cestimatingItem != null && cestimatingItem.Equals(product))
				{
					treeListNode.Selected = true;
					break;
				}
			}
		}

		private void btProductAdd_Click(object sender, EventArgs e)
		{
			if (this.OnProductAdd != null)
			{
				this.OnProductAdd(this, new EventArgs());
			}
		}

		private void btProductRemove_Click(object sender, EventArgs e)
		{
			if (this.OnProductRemove != null)
			{
				this.OnProductRemove(this, new EventArgs());
			}
		}

		private void btProductLink_Click(object sender, EventArgs e)
		{
			if (this.OnFormulaEdit != null)
			{
				this.OnFormulaEdit(this, new EventArgs());
			}
		}

		private void listItems_DoubleClick(object sender, EventArgs e)
		{
			TreeListHitInfo treeListHitInfo = this.listItems.CalcHitInfo(this.listItems.PointToClient(Control.MousePosition));
			if (treeListHitInfo.Node != null)
			{
				this.btProductLink_Click(sender, e);
			}
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

		private void list_AfterFocusNode(object sender, NodeEventArgs e)
		{
			this.EnableMenu(this.listItems.Nodes.Count > 0);
		}

		private void listItems_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			CEstimatingItem selectedProduct = this.SelectedProduct;
			if (selectedProduct != null && this.project != null)
			{
				DBEstimatingItem estimatingItem = this.project.DBManagement.GetEstimatingItem(Utilities.ConvertToInt(this.SelectedProduct.ItemID));
				this.ResizeLayout(true);
				this.txtFormula.Text = ((selectedProduct.Formula == "") ? Resources.Aucune_formule_associee : selectedProduct.Formula);
				string text;
				if (estimatingItem == null)
				{
					if (selectedProduct.CoverageRate == 0.0)
					{
						text = Resources.Aucun_facteur_de_recouvrement;
					}
					else
					{
						text = string.Concat(new string[]
						{
							selectedProduct.CoverageValue.ToString(),
							" ",
							selectedProduct.UnitMeasureCaption,
							" = ",
							selectedProduct.CoverageUnit.ToString(),
							" ",
							selectedProduct.Unit
						});
					}
				}
				else if (estimatingItem.CoverageRate == 0.0)
				{
					text = Resources.Aucun_facteur_de_recouvrement;
				}
				else
				{
					text = string.Concat(new string[]
					{
						estimatingItem.CoverageValue.ToString(),
						" ",
						estimatingItem.UnitMeasureCaption,
						" = ",
						estimatingItem.CoverageUnit.ToString(),
						" ",
						estimatingItem.PurchaseUnit
					});
				}
				this.txtCoverageRate.Text = text;
				return;
			}
			this.ResizeLayout(false);
		}

		private double ComputeEstimatingFormula(CEstimatingItem estimatingItem)
		{
			double num = 0.0;
			DBEstimatingItem estimatingItem2 = this.project.DBManagement.GetEstimatingItem(Utilities.ConvertToInt(estimatingItem.ItemID));
			if (this.groupObject != null)
			{
				Plan activePlan = this.groupObject.DrawArea.ActivePlan;
				UnitScale.UnitSystem unitSystem = this.project.EstimatingItems.QueryEstimatingItemSystemType(this.groupObject.GroupID, estimatingItem.InternalKey, estimatingItem.ItemID, this.groupObject.DrawArea.UnitScale.CurrentSystemType, Utilities.ConvertToInt(estimatingItem.ItemID));
				UnitScale.UnitPrecision precision = this.groupObject.DrawArea.UnitScale.Precision;
				UnitScale unitScale = new UnitScale(1f, unitSystem, precision, false);
				Presets presets = new Presets();
				foreach (object obj in this.groupObject.Group.Presets.Collection)
				{
					Preset preset = (Preset)obj;
					Preset preset2 = preset.Clone(true);
					presets.Add(preset2);
				}
				DBEstimatingItem.UnitMeasureType unitMeasureType = (estimatingItem2 != null) ? estimatingItem2.UnitMeasure : estimatingItem.UnitMeasure;
				double num2 = (estimatingItem2 != null) ? estimatingItem2.CoverageRate : estimatingItem.CoverageRate;
				if (unitMeasureType == DBEstimatingItem.UnitMeasureType.m || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_m || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_m || estimatingItem.Unit.ToUpper() == "M" || estimatingItem.Unit.ToUpper() == "M²" || estimatingItem.Unit.ToUpper() == "M³")
				{
					unitSystem = UnitScale.UnitSystem.metric;
				}
				else if (unitMeasureType == DBEstimatingItem.UnitMeasureType.lin_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_yd || estimatingItem.Unit.ToUpper() == Resources.pi.ToUpper() || estimatingItem.Unit.ToUpper() == Resources.pi_2.ToUpper() || estimatingItem.Unit.ToUpper() == Resources.pi_3.ToUpper() || estimatingItem.Unit.ToUpper() == Resources.v_3.ToUpper())
				{
					unitSystem = UnitScale.UnitSystem.imperial;
				}
				GroupStats objectStats = null;
				if (!this.project.DisplayResultsForAllPlans)
				{
					objectStats = GroupUtilities.ComputeGroupStats(activePlan, this.groupObject, unitSystem, true, "");
				}
				else
				{
					objectStats = GroupUtilities.ComputeGroupStats(this.project, this.groupObject, null, unitSystem, true, "");
				}
				foreach (object obj2 in this.groupObject.Group.Presets.Collection)
				{
					Preset preset3 = (Preset)obj2;
					UnitScale unitScale2 = new UnitScale(1f, unitSystem, precision, false);
					this.project.ExtensionsSupport.QueryPresetResults(preset3, objectStats, unitScale2);
				}
				if (FormulaUtilities.Compute(estimatingItem.Formula, this.groupObject.Group.Presets, objectStats, estimatingItem.ResultSystemType(unitSystem), ref num))
				{
					num = ((num2 == 0.0) ? num : Math.Ceiling(num / num2));
					num = unitScale.Round(num);
				}
				presets.Clear();
				presets = null;
			}
			return num;
		}

		private void listItems_CustomUnboundColumnData(object sender, TreeListCustomColumnDataEventArgs e)
		{
			if (e.Column.AbsoluteIndex == 7 || e.Column.AbsoluteIndex == 8)
			{
				CEstimatingItem cestimatingItem = this.CastListToProduct(e.Node);
				if (cestimatingItem != null)
				{
					if (e.Column.AbsoluteIndex == 7)
					{
						if (cestimatingItem.Formula == "")
						{
							e.Value = null;
							return;
						}
						if (cestimatingItem.Value == -1.0)
						{
							cestimatingItem.Value = this.ComputeEstimatingFormula(cestimatingItem);
						}
						e.Value = cestimatingItem.Value;
						return;
					}
					else
					{
						e.Value = cestimatingItem.Unit;
					}
				}
			}
		}

		private void ResizeLayout(bool enablePanelControl)
		{
			if (enablePanelControl)
			{
				this.listItems.Height = base.Height - (this.barProducts.Height + this.panelControl.Height);
				this.txtFormula.Width = base.Width - 10;
				this.txtCoverageRate.Width = base.Width - 10;
			}
			else
			{
				this.listItems.Height = base.Height - this.barProducts.Height;
			}
			this.panelControl.Visible = enablePanelControl;
		}

		private void CEstimatingsItemsControl_Resize(object sender, EventArgs e)
		{
			this.ResizeLayout(this.panelControl.Visible);
		}

		private void txtValue_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private IContainer components;

		private Bar barProducts;

		private ButtonItem btProductAdd;

		private ButtonItem btProductRemove;

		private ButtonItem btFormula;

		private TreeList listItems;

		private DevExpress.XtraEditors.PanelControl panelControl;

		private LabelEx lblCoverageRate;

		private LabelEx lblFormula;

		private TextBoxEx txtFormula;

		private TextBoxEx txtCoverageRate;

		private EventHandler OnProductAdd;

		private EventHandler OnProductRemove;

		private EventHandler OnFormulaEdit;

		private bool exitNow;

		private Project project;

		private DrawObject groupObject;

		private TreeListNode listTrackNode;
	}
}
