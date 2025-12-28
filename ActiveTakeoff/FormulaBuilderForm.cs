using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using FastColoredTextBoxNS;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class FormulaBuilderForm : BaseForm
	{
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
					if (this.treeResults.ActiveEditor != null)
					{
						this.treeResults.PostEditor();
					}
					this.treeResults.RefreshNode(node);
					this.treeResults.RefreshNode(this.treeTrackNode);
				}
			}
		}

		private void LoadResources(string sAssociatedProduct)
		{
			this.Text = Resources.Constructeur_de_formule;
			this.btOk.Text = Resources.OK;
			this.btCancel.Text = Resources.Cancel;
			this.labelEx1.Text = Resources.Formule_associée_avec;
			this.labelEx2.Text = sAssociatedProduct;
			this.labelEx3.Text = Resources.Double_cliquez_pour_insérer_dans_la_formule;
		}

		private void InitializeFonts()
		{
			this.labelEx1.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.labelEx2.Font = Utilities.GetDefaultFont(FontStyle.Bold);
		}

		private void InitializeTreeView()
		{
			this.titleFont = Utilities.GetDefaultFont(11f, FontStyle.Bold);
			this.defaultFont = Utilities.GetDefaultFont(11f, FontStyle.Regular);
			this.treeListViewState = new TreeListViewState(this.treeResults);
			this.treeResults.DataSource = this.formulaResults.Collection;
			this.treeResults.PopulateColumns();
			this.treeResults.Columns[0].Visible = false;
			this.treeResults.Columns[1].Visible = false;
			this.treeResults.Columns[2].Caption = Resources.Description;
			this.treeResults.Columns[2].OptionsColumn.ReadOnly = true;
			this.treeResults.Columns[2].OptionsColumn.AllowEdit = false;
			this.treeResults.Columns[2].OptionsColumn.AllowSort = false;
			this.treeResults.Columns[3].Caption = Resources.Unité;
			this.treeResults.Columns[3].OptionsColumn.ReadOnly = true;
			this.treeResults.Columns[3].OptionsColumn.AllowEdit = false;
			this.treeResults.Columns[3].OptionsColumn.AllowSort = false;
			this.treeResults.Columns[4].Visible = false;
			this.treeResults.LookAndFeel.Style = LookAndFeelStyle.Skin;
			this.treeResults.LookAndFeel.SkinName = "Office 2010 Silver";
			this.treeResults.LookAndFeel.UseDefaultLookAndFeel = false;
			this.treeResults.OptionsView.ShowColumns = false;
			this.treeResults.NodeCellStyle += this.tree_NodeCellStyle;
			this.treeResults.MouseMove += this.tree_MouseMove;
			this.treeResults.MouseLeave += this.tree_MouseLeave;
			this.treeResults.FocusedNodeChanged += this.treeSections_FocusedNodeChanged;
			this.treeResults.DoubleClick += this.treeResults_DoubleClick;
			this.treeResults.BestFitColumns();
			this.treeResults.ExpandAll();
		}

		public FormulaBuilderForm(CEstimatingItem selectedProduct, FormulaResults formulaResults, DrawObject drawObject, ImageCollection imageCollection)
		{
			this.InitializeComponent();
			this.InitializeFonts();
			this.LoadResources(selectedProduct.Description + " (" + selectedProduct.Unit + ")");
			this.selectedProduct = selectedProduct;
			this.formulaResults = formulaResults;
			this.drawObject = drawObject;
			this.imageCollection = imageCollection;
			this.popupMenu = new AutocompleteMenu(this.fctb);
			this.fctb.Text = selectedProduct.Formula;
			this.fctb.SelectionStart = this.fctb.TextLength + 1;
			this.InitializeTreeView();
		}

		private void InsertText(string sText, int offset = 0)
		{
			int selectionStart = this.fctb.SelectionStart;
			if (sText.Length > 0)
			{
				this.fctb.Text = this.fctb.Text.Substring(0, selectionStart) + this.fctb.Text.Substring(selectionStart + this.fctb.SelectionLength, this.fctb.Text.Length - (selectionStart + this.fctb.SelectedText.Length));
			}
			this.fctb.Text = this.fctb.Text.Insert(selectionStart, sText);
			this.fctb.Focus();
			this.fctb.SelectionStart = selectionStart + (sText.Length + offset);
		}

		private FormulaResult CastNodeToResult(TreeListNode node)
		{
			FormulaResult result;
			try
			{
				if (node.Level == 1)
				{
					result = (FormulaResult)node.GetValue(4);
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

		private void btPlus_Click(object sender, EventArgs e)
		{
			this.InsertText("+", 0);
		}

		private void btMinus_Click(object sender, EventArgs e)
		{
			this.InsertText("-", 0);
		}

		private void btMultiply_Click(object sender, EventArgs e)
		{
			this.InsertText("*", 0);
		}

		private void btDivide_Click(object sender, EventArgs e)
		{
			this.InsertText("/", 0);
		}

		private void btPower_Click(object sender, EventArgs e)
		{
			this.InsertText("^", 0);
		}

		private void btCeil_Click(object sender, EventArgs e)
		{
			this.InsertText("$ceil()", -1);
		}

		private void btFloor_Click(object sender, EventArgs e)
		{
			this.InsertText("$floor()", -1);
		}

		private void btRound_Click(object sender, EventArgs e)
		{
			this.InsertText("$round()", -1);
		}

		private void btSqrt_Click(object sender, EventArgs e)
		{
			this.InsertText("$sqrt()", -1);
		}

		private bool ValidateFormula(string formula)
		{
			if (formula == "")
			{
				string constructeur_de_formule = Resources.Constructeur_de_formule;
				string la_formule_ne_peut_être_vide = Resources.La_formule_ne_peut_être_vide;
				Utilities.DisplayError(constructeur_de_formule, la_formule_ne_peut_être_vide);
				return false;
			}
			string str = "";
			if (!FormulaUtilities.ValidateFields(formula, this.drawObject.Group, ref str))
			{
				string constructeur_de_formule2 = Resources.Constructeur_de_formule;
				string message = Resources.No_value_associated_with + " " + str;
				Utilities.DisplayError(constructeur_de_formule2, message);
				return false;
			}
			GroupStats groupStats = new GroupStats(this.drawObject.ObjectType);
			groupStats.GroupCount = 1;
			groupStats.Area = 1.0;
			groupStats.DeductionArea = 1.0;
			groupStats.DeductionsCount = 1;
			groupStats.Perimeter = 1.0;
			groupStats.DropLength = 1.0;
			groupStats.DeductionPerimeter = 1.0;
			groupStats.DropsCount = 1;
			groupStats.EndsCount = 1;
			groupStats.SegmentsCount = 1;
			groupStats.CornersCount = 1;
			double num = 0.0;
			return FormulaUtilities.Compute(formula, this.drawObject.Group.Presets, groupStats, this.selectedProduct.ResultSystemType(UnitScale.DefaultUnitSystem()), ref num);
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			this.fctb.Text = this.fctb.Text.Trim();
			if (!this.ValidateFormula(this.fctb.Text))
			{
				Utilities.SetObjectFocus(this.fctb);
				return;
			}
			this.selectedProduct.Formula = this.fctb.Text;
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
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
		}

		private void treeSections_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
		}

		private void treeResults_DoubleClick(object sender, EventArgs e)
		{
			TreeListHitInfo treeListHitInfo = this.treeResults.CalcHitInfo(this.treeResults.PointToClient(Control.MousePosition));
			if (treeListHitInfo.Node != null)
			{
				FormulaResult formulaResult = this.CastNodeToResult(treeListHitInfo.Node);
				if (formulaResult != null)
				{
					this.InsertText(formulaResult.FormulaString(), 0);
				}
			}
		}

		private void SetSyntaxHighlight(TextChangedEventArgs e)
		{
			e.ChangedRange.ClearStyle(new Style[]
			{
				this.FieldStyle,
				this.OperatorStyle,
				this.ParenthesisStyle,
				this.ParenthesisStyle,
				this.FunctionsStyle,
				this.NumbersStyle
			});
			e.ChangedRange.SetStyle(this.FieldStyle, "\\[(.*?)\\]");
			e.ChangedRange.SetStyle(this.OperatorStyle, "[\\+\\-\\*/\\^]");
			e.ChangedRange.SetStyle(this.ParenthesisStyle, "[\\(\\)]");
			e.ChangedRange.SetStyle(this.FunctionsStyle, "\\$sqrt|\\$round|\\$ceil|\\$floor");
			e.ChangedRange.SetStyle(this.NumbersStyle, "([0-9]*(\\.|,)?[0-9])");
			e.ChangedRange.SetStyle(this.DimensionStyle, "([0-9]*(\\.|,)?[0-9]+(m|cm|mm|'|\\\"))");
		}

		private void fctb_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.SetSyntaxHighlight(e);
		}

		private void FillWithResults(List<string> keywords)
		{
			foreach (FormulaResult formulaResult in this.formulaResults.Collection)
			{
				if (formulaResult.ParentID != -1)
				{
					keywords.Add(formulaResult.FormulaString());
				}
			}
		}

		private void FillWithFunctions(List<string> keywords)
		{
			keywords.Add("$ceil(");
			keywords.Add("$floor(");
			keywords.Add("$round(");
			keywords.Add("$sqrt(");
		}

		private void fctb_KeyPressing(object sender, KeyPressEventArgs e)
		{
			List<string> list = new List<string>();
			if (e.KeyChar == '[')
			{
				this.FillWithResults(list);
				this.popupMenu.MinFragmentLength = 2;
				this.popupMenu.Items.MaximumSize = new Size(350, 300);
				this.popupMenu.Items.Width = 350;
			}
			else if (e.KeyChar == '$')
			{
				this.FillWithFunctions(list);
				this.popupMenu.MinFragmentLength = 2;
				this.popupMenu.Items.MaximumSize = new Size(150, 300);
				this.popupMenu.Items.Width = 150;
			}
			else if (e.KeyChar == '.' || e.KeyChar == ',')
			{
				e.Handled = true;
				int selectionStart = this.fctb.SelectionStart;
				this.fctb.Text = this.fctb.Text.Insert(selectionStart, Utilities.NumberDecimalSeparator());
				this.fctb.SelectionStart = selectionStart + 1;
			}
			if (list.Count > 0)
			{
				this.popupMenu.Items.SetAutocompleteItems(list);
				this.popupMenu.Show(true);
				e.Handled = true;
			}
		}

		private bool exitNow;

		private Font titleFont;

		private Font defaultFont;

		private CEstimatingItem selectedProduct;

		private FormulaResults formulaResults;

		private DrawObject drawObject;

		private ImageCollection imageCollection;

		private TreeListViewState treeListViewState;

		private TreeListNode treeTrackNode;

		private TextStyle FieldStyle = new TextStyle(Brushes.SteelBlue, null, FontStyle.Bold);

		private TextStyle OperatorStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);

		private TextStyle ParenthesisStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);

		private TextStyle FunctionsStyle = new TextStyle(Brushes.SaddleBrown, null, FontStyle.Bold | FontStyle.Underline);

		private TextStyle NumbersStyle = new TextStyle(Brushes.DarkRed, null, FontStyle.Bold);

		private TextStyle DimensionStyle = new TextStyle(Brushes.DarkGreen, null, FontStyle.Bold);

		private AutocompleteMenu popupMenu;
	}
}
