using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class XtraReportTakeoffByPlans : XtraReport
	{
		private void LoadResources()
		{
			this.lblHeader1.Text = Resources.Rapport_de_métré;
			this.lblHeader2.Text = ((this.project.Report.ReportSortBy == QuoterPlan.Report.ReportSortByEnum.ReportSortByPlans) ? Resources._par_plans_ : Resources._par_calques_);
			this.lbHeaderPageInfo.Format = Resources.Page_0_de_1_;
			this.lblProjectInfo.Text = Resources.Projet_;
			this.lblContactInfo.Text = Resources.Client_;
			this.lblComments.Text = Resources.Commentaires__;
			this.lblReportTitle.Text = ((this.project.Report.ReportSortBy == QuoterPlan.Report.ReportSortByEnum.ReportSortByPlans) ? Resources.RAPPORT_DE_MÉTRÉ_PAR_PLANS_ : Resources.RAPPORT_DE_MÉTRÉ__PAR_CALQUES_);
			this.lblFooter1.Text = Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan;
			this.lblFooter2.Text = Utilities.ApplicationWebsite;
			this.lbFooterlPageInfo.Format = Resources.Page_0_de_1_;
			this.lblThisPlan1.Text = Resources.CE_PLAN;
			this.lblThisPlan2.Text = Resources.CE_PLAN;
			this.lblAllPlans1.Text = Resources.TOUS_LES_PLANS;
			this.lblAllPlans2.Text = Resources.TOUS_LES_PLANS;
			this.lblThisLayer1.Text = Resources.CE_CALQUE;
			this.lblThisLayer2.Text = Resources.CE_CALQUE;
			this.lblAllLayers1.Text = Resources.TOUS_LES_CALQUES;
			this.lblAllLayers2.Text = Resources.TOUS_LES_CALQUES;
		}

		public XtraReportTakeoffByPlans(Project project)
		{
			this.project = project;
			this.InitializeComponent();
			this.LoadResources();
		}

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
			XRSummary xrsummary = new XRSummary();
			XRSummary xrsummary2 = new XRSummary();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XtraReportTakeoffByPlans));
			this.Detail = new DetailBand();
			this.TopMargin = new TopMarginBand();
			this.lblHeader2 = new XRLabel();
			this.lblHeader1 = new XRLabel();
			this.lbHeaderTitle = new XRLabel();
			this.lbHeaderPageInfo = new XRPageInfo();
			this.BottomMargin = new BottomMarginBand();
			this.lbFooterDate = new XRLabel();
			this.lblFooter2 = new XRLabel();
			this.lblFooter1 = new XRLabel();
			this.lbFooterlPageInfo = new XRPageInfo();
			this.ReportHeader = new ReportHeaderBand();
			this.SubBand1 = new SubBand();
			this.formattingRule4 = new FormattingRule();
			this.formattingRule6 = new FormattingRule();
			this.formattingRule7 = new FormattingRule();
			this.SubBand4 = new SubBand();
			this.formattingRule8 = new FormattingRule();
			this.SubBand5 = new SubBand();
			this.xrLabel20 = new XRLabel();
			this.DetailReport2 = new DetailReportBand();
			this.Detail3 = new DetailBand();
			this.DetailReport4 = new DetailReportBand();
			this.Detail5 = new DetailBand();
			this.xrLabel14 = new XRLabel();
			this.DetailReport8 = new DetailReportBand();
			this.Detail9 = new DetailBand();
			this.DetailReport11 = new DetailReportBand();
			this.Detail12 = new DetailBand();
			this.xrLabel9 = new XRLabel();
			this.xrLabel8 = new XRLabel();
			this.SubBand8 = new SubBand();
			this.xrLabel21 = new XRLabel();
			this.formattingRule9 = new FormattingRule();
			this.DetailReport12 = new DetailReportBand();
			this.Detail13 = new DetailBand();
			this.xrLabel2 = new XRLabel();
			this.lblThisLayer1 = new XRLabel();
			this.formattingRule12 = new FormattingRule();
			this.lblAllLayers1 = new XRLabel();
			this.lblThisPlan1 = new XRLabel();
			this.formattingRule13 = new FormattingRule();
			this.lblAllPlans1 = new XRLabel();
			this.DetailReport14 = new DetailReportBand();
			this.Detail15 = new DetailBand();
			this.xrLine1 = new XRLine();
			this.xrTable7 = new XRTable();
			this.xrTableRow6 = new XRTableRow();
			this.xrTableCell19 = new XRTableCell();
			this.xrTableCell5 = new XRTableCell();
			this.xrTableCell26 = new XRTableCell();
			this.formattingRule3 = new FormattingRule();
			this.formattingRule1 = new FormattingRule();
			this.DetailReport13 = new DetailReportBand();
			this.Detail14 = new DetailBand();
			this.xrLine2 = new XRLine();
			this.xrTable3 = new XRTable();
			this.xrTableRow5 = new XRTableRow();
			this.xrTableCell14 = new XRTableCell();
			this.xrTableCell15 = new XRTableCell();
			this.xrTableCell16 = new XRTableCell();
			this.xrTableCell17 = new XRTableCell();
			this.xrTableCell18 = new XRTableCell();
			this.GroupHeader1 = new GroupHeaderBand();
			this.lblThisLayer2 = new XRLabel();
			this.formattingRule11 = new FormattingRule();
			this.lblAllLayers2 = new XRLabel();
			this.lblThisPlan2 = new XRLabel();
			this.formattingRule10 = new FormattingRule();
			this.lblAllPlans2 = new XRLabel();
			this.DetailReport = new DetailReportBand();
			this.Detail1 = new DetailBand();
			this.xrLabel5 = new XRLabel();
			this.GroupHeader3 = new GroupHeaderBand();
			this.xrLabel7 = new XRLabel();
			this.GroupHeader4 = new GroupHeaderBand();
			this.xrLabel3 = new XRLabel();
			this.xrLabel6 = new XRLabel();
			this.GroupHeader5 = new GroupHeaderBand();
			this.xrLabel1 = new XRLabel();
			this.formattingRule2 = new FormattingRule();
			this.SubBand7 = new SubBand();
			this.lblReportTitle = new XRLabel();
			this.formattingRule5 = new FormattingRule();
			this.GroupHeader2 = new GroupHeaderBand();
			this.xrTable1 = new XRTable();
			this.xrTableRow1 = new XRTableRow();
			this.lblProjectInfo = new XRTableCell();
			this.xrTableRow7 = new XRTableRow();
			this.xrTableCell2 = new XRTableCell();
			this.xrTableRow9 = new XRTableRow();
			this.xrTableCell3 = new XRTableCell();
			this.xrTable2 = new XRTable();
			this.xrTableRow3 = new XRTableRow();
			this.lblContactInfo = new XRTableCell();
			this.xrTableRow2 = new XRTableRow();
			this.xrTableCell1 = new XRTableCell();
			this.xrTableRow4 = new XRTableRow();
			this.xrTableCell4 = new XRTableCell();
			this.xrTable6 = new XRTable();
			this.xrTableRow11 = new XRTableRow();
			this.lblComments = new XRTableCell();
			this.xrTableRow12 = new XRTableRow();
			this.xrTableCell12 = new XRTableCell();
			((ISupportInitialize)this.xrTable7).BeginInit();
			((ISupportInitialize)this.xrTable3).BeginInit();
			((ISupportInitialize)this.xrTable1).BeginInit();
			((ISupportInitialize)this.xrTable2).BeginInit();
			((ISupportInitialize)this.xrTable6).BeginInit();
			((ISupportInitialize)this).BeginInit();
			this.Detail.Expanded = false;
			this.Detail.HeightF = 8.333333f;
			this.Detail.Name = "Detail";
			this.Detail.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.Detail.TextAlignment = TextAlignment.TopLeft;
			this.TopMargin.Controls.AddRange(new XRControl[]
			{
				this.lblHeader2,
				this.lblHeader1,
				this.lbHeaderTitle,
				this.lbHeaderPageInfo
			});
			this.TopMargin.HeightF = 100f;
			this.TopMargin.Name = "TopMargin";
			this.TopMargin.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.TopMargin.TextAlignment = TextAlignment.TopLeft;
			this.lblHeader2.BorderColor = Color.Black;
			this.lblHeader2.Borders = BorderSide.None;
			this.lblHeader2.BorderWidth = 1f;
			this.lblHeader2.Font = new Font("Tahoma", 8.25f, FontStyle.Underline, GraphicsUnit.Point, 0);
			this.lblHeader2.LocationFloat = new PointFloat(527.7068f, 39.61459f);
			this.lblHeader2.Name = "lblHeader2";
			this.lblHeader2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblHeader2.SizeF = new SizeF(122.2933f, 17f);
			this.lblHeader2.StylePriority.UseBorderColor = false;
			this.lblHeader2.StylePriority.UseBorders = false;
			this.lblHeader2.StylePriority.UseBorderWidth = false;
			this.lblHeader2.StylePriority.UseFont = false;
			this.lblHeader2.StylePriority.UsePadding = false;
			this.lblHeader2.StylePriority.UseTextAlignment = false;
			this.lblHeader2.Text = "(par plans)";
			this.lblHeader2.TextAlignment = TextAlignment.MiddleRight;
			this.lblHeader1.BorderColor = Color.Black;
			this.lblHeader1.Borders = BorderSide.None;
			this.lblHeader1.BorderWidth = 1f;
			this.lblHeader1.Font = new Font("Tahoma", 8.25f, FontStyle.Underline, GraphicsUnit.Point, 0);
			this.lblHeader1.LocationFloat = new PointFloat(527.7068f, 22.61458f);
			this.lblHeader1.Name = "lblHeader1";
			this.lblHeader1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblHeader1.SizeF = new SizeF(122.2933f, 17f);
			this.lblHeader1.StylePriority.UseBorderColor = false;
			this.lblHeader1.StylePriority.UseBorders = false;
			this.lblHeader1.StylePriority.UseBorderWidth = false;
			this.lblHeader1.StylePriority.UseFont = false;
			this.lblHeader1.StylePriority.UsePadding = false;
			this.lblHeader1.StylePriority.UseTextAlignment = false;
			this.lblHeader1.Text = "Rapport de métré";
			this.lblHeader1.TextAlignment = TextAlignment.MiddleRight;
			this.lbHeaderTitle.BorderColor = Color.Black;
			this.lbHeaderTitle.Borders = BorderSide.None;
			this.lbHeaderTitle.BorderWidth = 1f;
			this.lbHeaderTitle.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Report.Title")
			});
			this.lbHeaderTitle.Font = new Font("Tahoma", 8.25f);
			this.lbHeaderTitle.LocationFloat = new PointFloat(0f, 37.5f);
			this.lbHeaderTitle.Name = "lbHeaderTitle";
			this.lbHeaderTitle.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lbHeaderTitle.SizeF = new SizeF(313.96f, 17f);
			this.lbHeaderTitle.StylePriority.UseBorderColor = false;
			this.lbHeaderTitle.StylePriority.UseBorders = false;
			this.lbHeaderTitle.StylePriority.UseBorderWidth = false;
			this.lbHeaderTitle.StylePriority.UseFont = false;
			this.lbHeaderTitle.StylePriority.UsePadding = false;
			this.lbHeaderTitle.StylePriority.UseTextAlignment = false;
			this.lbHeaderTitle.TextAlignment = TextAlignment.MiddleLeft;
			this.lbHeaderPageInfo.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbHeaderPageInfo.Format = "Page {0} de {1}";
			this.lbHeaderPageInfo.LocationFloat = new PointFloat(498.9999f, 60.38541f);
			this.lbHeaderPageInfo.Name = "lbHeaderPageInfo";
			this.lbHeaderPageInfo.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lbHeaderPageInfo.SizeF = new SizeF(151f, 17f);
			this.lbHeaderPageInfo.StylePriority.UseFont = false;
			this.lbHeaderPageInfo.StylePriority.UseTextAlignment = false;
			this.lbHeaderPageInfo.Text = "Total";
			this.lbHeaderPageInfo.TextAlignment = TextAlignment.MiddleRight;
			this.BottomMargin.Controls.AddRange(new XRControl[]
			{
				this.lbFooterDate,
				this.lblFooter2,
				this.lblFooter1,
				this.lbFooterlPageInfo
			});
			this.BottomMargin.HeightF = 100f;
			this.BottomMargin.Name = "BottomMargin";
			this.BottomMargin.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.BottomMargin.TextAlignment = TextAlignment.TopLeft;
			this.lbFooterDate.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Report.Date")
			});
			this.lbFooterDate.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbFooterDate.LocationFloat = new PointFloat(0.0001271566f, 41.50003f);
			this.lbFooterDate.Name = "lbFooterDate";
			this.lbFooterDate.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lbFooterDate.SizeF = new SizeF(186.46f, 17f);
			this.lbFooterDate.StylePriority.UseFont = false;
			this.lbFooterDate.StylePriority.UseTextAlignment = false;
			this.lbFooterDate.TextAlignment = TextAlignment.MiddleLeft;
			this.lblFooter2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblFooter2.LocationFloat = new PointFloat(197.855f, 49.99997f);
			this.lblFooter2.Name = "lblFooter2";
			this.lblFooter2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblFooter2.SizeF = new SizeF(257.29f, 15f);
			this.lblFooter2.StylePriority.UseFont = false;
			this.lblFooter2.StylePriority.UseTextAlignment = false;
			this.lblFooter2.Text = "www.quoterplan.com";
			this.lblFooter2.TextAlignment = TextAlignment.MiddleCenter;
			this.lblFooter1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblFooter1.LocationFloat = new PointFloat(197.855f, 35.00003f);
			this.lblFooter1.Name = "lblFooter1";
			this.lblFooter1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblFooter1.SizeF = new SizeF(257.29f, 15f);
			this.lblFooter1.StylePriority.UseFont = false;
			this.lblFooter1.StylePriority.UseTextAlignment = false;
			this.lblFooter1.Text = "Ce rapport a été généré grâce à Quoter Plan";
			this.lblFooter1.TextAlignment = TextAlignment.MiddleCenter;
			this.lbFooterlPageInfo.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbFooterlPageInfo.Format = "Page {0} de {1}";
			this.lbFooterlPageInfo.LocationFloat = new PointFloat(499.0001f, 41.5f);
			this.lbFooterlPageInfo.Name = "lbFooterlPageInfo";
			this.lbFooterlPageInfo.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lbFooterlPageInfo.SizeF = new SizeF(151f, 17f);
			this.lbFooterlPageInfo.StylePriority.UseFont = false;
			this.lbFooterlPageInfo.StylePriority.UseTextAlignment = false;
			this.lbFooterlPageInfo.Text = "Total";
			this.lbFooterlPageInfo.TextAlignment = TextAlignment.MiddleRight;
			this.ReportHeader.HeightF = 2.083333f;
			this.ReportHeader.Name = "ReportHeader";
			this.ReportHeader.SubBands.AddRange(new SubBand[]
			{
				this.SubBand1,
				this.SubBand4,
				this.SubBand5
			});
			this.SubBand1.Controls.AddRange(new XRControl[]
			{
				this.xrTable2,
				this.xrTable1
			});
			this.SubBand1.FormattingRules.Add(this.formattingRule4);
			this.SubBand1.HeightF = 75f;
			this.SubBand1.Name = "SubBand1";
			this.formattingRule4.Condition = "IsNullOrEmpty([Name])";
			this.formattingRule4.DataMember = "Project";
			this.formattingRule4.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule4.Name = "formattingRule4";
			this.formattingRule6.Condition = "IsNullOrEmpty([Description])";
			this.formattingRule6.DataMember = "Project";
			this.formattingRule6.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule6.Name = "formattingRule6";
			this.formattingRule7.Condition = "IsNullOrEmpty([ContactInfo])";
			this.formattingRule7.DataMember = "Project";
			this.formattingRule7.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule7.Name = "formattingRule7";
			this.SubBand4.Controls.AddRange(new XRControl[]
			{
				this.xrTable6
			});
			this.SubBand4.FormattingRules.Add(this.formattingRule8);
			this.SubBand4.HeightF = 60f;
			this.SubBand4.Name = "SubBand4";
			this.formattingRule8.Condition = "IsNullOrEmpty([Comment])";
			this.formattingRule8.DataMember = "Project";
			this.formattingRule8.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule8.Name = "formattingRule8";
			this.SubBand5.Controls.AddRange(new XRControl[]
			{
				this.xrLabel20
			});
			this.SubBand5.FormattingRules.Add(this.formattingRule4);
			this.SubBand5.HeightF = 35f;
			this.SubBand5.Name = "SubBand5";
			this.xrLabel20.BackColor = Color.Transparent;
			this.xrLabel20.BorderColor = Color.Black;
			this.xrLabel20.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel20.ForeColor = Color.Transparent;
			this.xrLabel20.LocationFloat = new PointFloat(200f, 12.5f);
			this.xrLabel20.Multiline = true;
			this.xrLabel20.Name = "xrLabel20";
			this.xrLabel20.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel20.SizeF = new SizeF(257.29f, 15f);
			this.xrLabel20.StylePriority.UseBackColor = false;
			this.xrLabel20.StylePriority.UseBorderColor = false;
			this.xrLabel20.StylePriority.UseFont = false;
			this.xrLabel20.StylePriority.UseForeColor = false;
			this.xrLabel20.StylePriority.UseTextAlignment = false;
			this.xrLabel20.Text = "* * *";
			this.xrLabel20.TextAlignment = TextAlignment.TopCenter;
			this.DetailReport2.Bands.AddRange(new Band[]
			{
				this.Detail3,
				this.DetailReport4,
				this.DetailReport8
			});
			this.DetailReport2.DataMember = "Report.Report_Plans";
			this.DetailReport2.Level = 0;
			this.DetailReport2.Name = "DetailReport2";
			this.Detail3.HeightF = 0f;
			this.Detail3.Name = "Detail3";
			this.Detail3.StylePriority.UseBackColor = false;
			this.Detail3.Visible = false;
			this.DetailReport4.Bands.AddRange(new Band[]
			{
				this.Detail5
			});
			this.DetailReport4.Expanded = false;
			this.DetailReport4.Level = 1;
			this.DetailReport4.Name = "DetailReport4";
			this.Detail5.Controls.AddRange(new XRControl[]
			{
				this.xrLabel14
			});
			this.Detail5.HeightF = 30f;
			this.Detail5.Name = "Detail5";
			this.xrLabel14.BackColor = Color.Transparent;
			this.xrLabel14.BorderColor = Color.Black;
			this.xrLabel14.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel14.ForeColor = Color.Transparent;
			this.xrLabel14.LocationFloat = new PointFloat(197.855f, 10f);
			this.xrLabel14.Multiline = true;
			this.xrLabel14.Name = "xrLabel14";
			this.xrLabel14.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel14.SizeF = new SizeF(257.29f, 15f);
			this.xrLabel14.StylePriority.UseBackColor = false;
			this.xrLabel14.StylePriority.UseBorderColor = false;
			this.xrLabel14.StylePriority.UseFont = false;
			this.xrLabel14.StylePriority.UseForeColor = false;
			this.xrLabel14.StylePriority.UseTextAlignment = false;
			this.xrLabel14.Text = "* * *";
			this.xrLabel14.TextAlignment = TextAlignment.TopCenter;
			this.DetailReport8.Bands.AddRange(new Band[]
			{
				this.Detail9,
				this.DetailReport11
			});
			this.DetailReport8.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer";
			this.DetailReport8.Level = 0;
			this.DetailReport8.Name = "DetailReport8";
			this.Detail9.HeightF = 0f;
			this.Detail9.Name = "Detail9";
			this.DetailReport11.Bands.AddRange(new Band[]
			{
				this.Detail12,
				this.DetailReport12,
				this.DetailReport,
				this.GroupHeader3,
				this.GroupHeader4,
				this.GroupHeader5
			});
			this.DetailReport11.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object";
			this.DetailReport11.Level = 0;
			this.DetailReport11.Name = "DetailReport11";
			this.Detail12.Controls.AddRange(new XRControl[]
			{
				this.xrLabel9,
				this.xrLabel8
			});
			this.Detail12.HeightF = 53f;
			this.Detail12.KeepTogetherWithDetailReports = true;
			this.Detail12.Name = "Detail12";
			this.Detail12.SubBands.AddRange(new SubBand[]
			{
				this.SubBand8
			});
			this.xrLabel9.BackColor = Color.Transparent;
			this.xrLabel9.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Name")
			});
			this.xrLabel9.Font = new Font("Tahoma", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel9.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel9.LocationFloat = new PointFloat(32f, 25f);
			this.xrLabel9.Name = "xrLabel9";
			this.xrLabel9.Padding = new PaddingInfo(6, 2, 1, 0, 100f);
			this.xrLabel9.SizeF = new SizeF(617.9999f, 28f);
			this.xrLabel9.StylePriority.UseFont = false;
			this.xrLabel9.StylePriority.UseForeColor = false;
			this.xrLabel9.StylePriority.UsePadding = false;
			this.xrLabel8.BorderColor = Color.FromArgb(32, 32, 32);
			this.xrLabel8.Borders = BorderSide.All;
			this.xrLabel8.BorderWidth = 2f;
			this.xrLabel8.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Color")
			});
			this.xrLabel8.LocationFloat = new PointFloat(10f, 27f);
			this.xrLabel8.Name = "xrLabel8";
			this.xrLabel8.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrLabel8.Scripts.OnBeforePrint = "xrLabel6_BeforePrint";
			this.xrLabel8.SizeF = new SizeF(21f, 21f);
			this.xrLabel8.StylePriority.UseBorderColor = false;
			this.xrLabel8.StylePriority.UseBorders = false;
			this.xrLabel8.StylePriority.UseBorderWidth = false;
			this.xrLabel8.StylePriority.UsePadding = false;
			this.SubBand8.Controls.AddRange(new XRControl[]
			{
				this.xrLabel21
			});
			this.SubBand8.FormattingRules.Add(this.formattingRule9);
			this.SubBand8.HeightF = 20f;
			this.SubBand8.KeepTogether = true;
			this.SubBand8.Name = "SubBand8";
			this.SubBand8.Visible = false;
			this.xrLabel21.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Comment")
			});
			this.xrLabel21.Font = new Font("Tahoma", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel21.ForeColor = Color.FromArgb(64, 64, 64);
			this.xrLabel21.LocationFloat = new PointFloat(31.99973f, 0f);
			this.xrLabel21.Multiline = true;
			this.xrLabel21.Name = "xrLabel21";
			this.xrLabel21.Padding = new PaddingInfo(6, 2, 2, 2, 100f);
			this.xrLabel21.SizeF = new SizeF(618f, 18f);
			this.xrLabel21.StylePriority.UseBorderColor = false;
			this.xrLabel21.StylePriority.UseFont = false;
			this.xrLabel21.StylePriority.UseForeColor = false;
			this.xrLabel21.StylePriority.UsePadding = false;
			this.xrLabel21.StylePriority.UseTextAlignment = false;
			this.xrLabel21.TextAlignment = TextAlignment.MiddleLeft;
			this.formattingRule9.Condition = "!IsNullOrEmpty([Comment])";
			this.formattingRule9.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object";
			this.formattingRule9.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule9.Name = "formattingRule9";
			this.DetailReport12.Bands.AddRange(new Band[]
			{
				this.Detail13,
				this.DetailReport14,
				this.DetailReport13
			});
			this.DetailReport12.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.DetailReport12.Level = 0;
			this.DetailReport12.Name = "DetailReport12";
			this.Detail13.Controls.AddRange(new XRControl[]
			{
				this.xrLabel2,
				this.lblThisLayer1,
				this.lblAllLayers1,
				this.lblThisPlan1,
				this.lblAllPlans1
			});
			this.Detail13.HeightF = 35f;
			this.Detail13.KeepTogether = true;
			this.Detail13.KeepTogetherWithDetailReports = true;
			this.Detail13.Name = "Detail13";
			this.xrLabel2.BackColor = Color.Transparent;
			this.xrLabel2.BorderColor = Color.Black;
			this.xrLabel2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Name")
			});
			this.xrLabel2.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrLabel2.ForeColor = Color.DarkRed;
			this.xrLabel2.LocationFloat = new PointFloat(0f, 8f);
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel2.SizeF = new SizeF(242.7101f, 20f);
			this.xrLabel2.StylePriority.UseBackColor = false;
			this.xrLabel2.StylePriority.UseBorderColor = false;
			this.xrLabel2.StylePriority.UseFont = false;
			this.xrLabel2.StylePriority.UseForeColor = false;
			this.xrLabel2.StylePriority.UsePadding = false;
			this.xrLabel2.StylePriority.UseTextAlignment = false;
			this.xrLabel2.TextAlignment = TextAlignment.MiddleLeft;
			this.lblThisLayer1.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblThisLayer1.ForeColor = Color.DarkRed;
			this.lblThisLayer1.FormattingRules.Add(this.formattingRule12);
			this.lblThisLayer1.LocationFloat = new PointFloat(258.3398f, 10.91665f);
			this.lblThisLayer1.Name = "lblThisLayer1";
			this.lblThisLayer1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblThisLayer1.SizeF = new SizeF(189.58f, 17.08336f);
			this.lblThisLayer1.StylePriority.UseFont = false;
			this.lblThisLayer1.StylePriority.UseForeColor = false;
			this.lblThisLayer1.StylePriority.UseTextAlignment = false;
			this.lblThisLayer1.Text = "CE CALQUE";
			this.lblThisLayer1.TextAlignment = TextAlignment.MiddleLeft;
			this.lblThisLayer1.Visible = false;
			this.formattingRule12.Condition = "IsNullOrEmpty([Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Fields])  And IsNullOrEmpty([Name])";
			this.formattingRule12.DataMember = "Plans.Plans_Plan";
			this.formattingRule12.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule12.Name = "formattingRule12";
			this.lblAllLayers1.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblAllLayers1.ForeColor = Color.DarkRed;
			this.lblAllLayers1.FormattingRules.Add(this.formattingRule12);
			this.lblAllLayers1.LocationFloat = new PointFloat(460.4197f, 10.91665f);
			this.lblAllLayers1.Name = "lblAllLayers1";
			this.lblAllLayers1.Padding = new PaddingInfo(2, 10, 0, 0, 100f);
			this.lblAllLayers1.SizeF = new SizeF(189.58f, 17.08336f);
			this.lblAllLayers1.StylePriority.UseFont = false;
			this.lblAllLayers1.StylePriority.UseForeColor = false;
			this.lblAllLayers1.StylePriority.UsePadding = false;
			this.lblAllLayers1.StylePriority.UseTextAlignment = false;
			this.lblAllLayers1.Text = "TOUS LES CALQUES";
			this.lblAllLayers1.TextAlignment = TextAlignment.MiddleLeft;
			this.lblAllLayers1.Visible = false;
			this.lblThisPlan1.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblThisPlan1.ForeColor = Color.DarkRed;
			this.lblThisPlan1.FormattingRules.Add(this.formattingRule13);
			this.lblThisPlan1.LocationFloat = new PointFloat(258.3398f, 10.91665f);
			this.lblThisPlan1.Name = "lblThisPlan1";
			this.lblThisPlan1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblThisPlan1.SizeF = new SizeF(189.58f, 17.08336f);
			this.lblThisPlan1.StylePriority.UseFont = false;
			this.lblThisPlan1.StylePriority.UseForeColor = false;
			this.lblThisPlan1.StylePriority.UseTextAlignment = false;
			this.lblThisPlan1.Text = "CE PLAN";
			this.lblThisPlan1.TextAlignment = TextAlignment.MiddleLeft;
			this.lblThisPlan1.Visible = false;
			this.formattingRule13.Condition = "IsNullOrEmpty([Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Fields])  And !IsNullOrEmpty([Name])";
			this.formattingRule13.DataMember = "Plans.Plans_Plan";
			this.formattingRule13.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule13.Name = "formattingRule13";
			this.lblAllPlans1.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblAllPlans1.ForeColor = Color.DarkRed;
			this.lblAllPlans1.FormattingRules.Add(this.formattingRule13);
			this.lblAllPlans1.LocationFloat = new PointFloat(460.4197f, 10.91665f);
			this.lblAllPlans1.Name = "lblAllPlans1";
			this.lblAllPlans1.Padding = new PaddingInfo(2, 10, 0, 0, 100f);
			this.lblAllPlans1.SizeF = new SizeF(189.58f, 17.08336f);
			this.lblAllPlans1.StylePriority.UseFont = false;
			this.lblAllPlans1.StylePriority.UseForeColor = false;
			this.lblAllPlans1.StylePriority.UsePadding = false;
			this.lblAllPlans1.StylePriority.UseTextAlignment = false;
			this.lblAllPlans1.Text = "TOUS LES PLANS";
			this.lblAllPlans1.TextAlignment = TextAlignment.MiddleLeft;
			this.lblAllPlans1.Visible = false;
			this.DetailReport14.Bands.AddRange(new Band[]
			{
				this.Detail15
			});
			this.DetailReport14.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Fields.Fields_Field";
			this.DetailReport14.FormattingRules.Add(this.formattingRule1);
			this.DetailReport14.Level = 0;
			this.DetailReport14.Name = "DetailReport14";
			this.DetailReport14.Visible = false;
			this.Detail15.Controls.AddRange(new XRControl[]
			{
				this.xrLine1,
				this.xrTable7
			});
			this.Detail15.FormattingRules.Add(this.formattingRule3);
			this.Detail15.FormattingRules.Add(this.formattingRule1);
			this.Detail15.HeightF = 23f;
			this.Detail15.Name = "Detail15";
			this.xrLine1.BorderColor = Color.Transparent;
			this.xrLine1.ForeColor = Color.DarkGray;
			this.xrLine1.LineStyle = DashStyle.Dot;
			this.xrLine1.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new SizeF(650f, 3f);
			this.xrLine1.StylePriority.UseBorderColor = false;
			this.xrLine1.StylePriority.UseForeColor = false;
			this.xrTable7.Borders = BorderSide.None;
			this.xrTable7.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable7.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable7.LocationFloat = new PointFloat(0.0003655752f, 0f);
			this.xrTable7.Name = "xrTable7";
			this.xrTable7.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow6
			});
			this.xrTable7.Scripts.OnBeforePrint = "xrTable7_BeforePrint";
			this.xrTable7.SizeF = new SizeF(649.9994f, 18f);
			this.xrTable7.StylePriority.UseBorders = false;
			this.xrTable7.StylePriority.UseFont = false;
			this.xrTable7.StylePriority.UseForeColor = false;
			this.xrTable7.StylePriority.UseTextAlignment = false;
			this.xrTable7.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow6.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell19,
				this.xrTableCell5,
				this.xrTableCell26
			});
			this.xrTableRow6.Name = "xrTableRow6";
			this.xrTableRow6.Weight = 1.0;
			this.xrTableCell19.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Fields.Fields_Field.Caption")
			});
			this.xrTableCell19.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell19.Name = "xrTableCell19";
			this.xrTableCell19.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell19.StylePriority.UseFont = false;
			this.xrTableCell19.StylePriority.UsePadding = false;
			this.xrTableCell19.Weight = 0.897600503794651;
			this.xrTableCell5.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell5.Name = "xrTableCell5";
			this.xrTableCell5.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell5.StylePriority.UseFont = false;
			this.xrTableCell5.StylePriority.UsePadding = false;
			this.xrTableCell5.Weight = 0.09885747331416397;
			this.xrTableCell26.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Fields.Fields_Field.Value")
			});
			this.xrTableCell26.Name = "xrTableCell26";
			this.xrTableCell26.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell26.StylePriority.UsePadding = false;
			this.xrTableCell26.StylePriority.UseTextAlignment = false;
			xrsummary.FormatString = "{0:0.00%}";
			xrsummary.Func = SummaryFunc.Avg;
			this.xrTableCell26.Summary = xrsummary;
			this.xrTableCell26.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell26.Weight = 1.5106958413566813;
			this.formattingRule3.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule3.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule3.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule3.Name = "formattingRule3";
			this.formattingRule1.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule1.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule1.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule1.Name = "formattingRule1";
			this.DetailReport13.Bands.AddRange(new Band[]
			{
				this.Detail14,
				this.GroupHeader1
			});
			this.DetailReport13.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result";
			this.DetailReport13.Level = 1;
			this.DetailReport13.Name = "DetailReport13";
			this.Detail14.Controls.AddRange(new XRControl[]
			{
				this.xrLine2,
				this.xrTable3
			});
			this.Detail14.HeightF = 23f;
			this.Detail14.Name = "Detail14";
			this.xrLine2.BorderColor = Color.Transparent;
			this.xrLine2.ForeColor = Color.DarkGray;
			this.xrLine2.LineStyle = DashStyle.Dot;
			this.xrLine2.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine2.Name = "xrLine2";
			this.xrLine2.SizeF = new SizeF(650f, 3f);
			this.xrLine2.StylePriority.UseBorderColor = false;
			this.xrLine2.StylePriority.UseForeColor = false;
			this.xrTable3.Borders = BorderSide.None;
			this.xrTable3.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable3.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable3.LocationFloat = new PointFloat(0.0007152557f, 1.000023f);
			this.xrTable3.Name = "xrTable3";
			this.xrTable3.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow5
			});
			this.xrTable3.Scripts.OnBeforePrint = "xrTable7_BeforePrint";
			this.xrTable3.SizeF = new SizeF(649.9994f, 18f);
			this.xrTable3.StylePriority.UseBorders = false;
			this.xrTable3.StylePriority.UseFont = false;
			this.xrTable3.StylePriority.UseForeColor = false;
			this.xrTable3.StylePriority.UseTextAlignment = false;
			this.xrTable3.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow5.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell14,
				this.xrTableCell15,
				this.xrTableCell16,
				this.xrTableCell17,
				this.xrTableCell18
			});
			this.xrTableRow5.Name = "xrTableRow5";
			this.xrTableRow5.Weight = 1.0;
			this.xrTableCell14.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.Caption")
			});
			this.xrTableCell14.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell14.Name = "xrTableCell14";
			this.xrTableCell14.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell14.StylePriority.UseFont = false;
			this.xrTableCell14.StylePriority.UsePadding = false;
			this.xrTableCell14.Weight = 0.9361713144796954;
			this.xrTableCell15.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell15.Name = "xrTableCell15";
			this.xrTableCell15.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell15.StylePriority.UseFont = false;
			this.xrTableCell15.StylePriority.UsePadding = false;
			this.xrTableCell15.Weight = 0.06028690110055632;
			this.xrTableCell16.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.Value")
			});
			this.xrTableCell16.Name = "xrTableCell16";
			this.xrTableCell16.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell16.StylePriority.UsePadding = false;
			this.xrTableCell16.StylePriority.UseTextAlignment = false;
			xrsummary2.FormatString = "{0:0.00%}";
			xrsummary2.Func = SummaryFunc.Avg;
			this.xrTableCell16.Summary = xrsummary2;
			this.xrTableCell16.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell16.Weight = 0.7312409903006138;
			this.xrTableCell17.Name = "xrTableCell17";
			this.xrTableCell17.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell17.StylePriority.UsePadding = false;
			this.xrTableCell17.StylePriority.UseTextAlignment = false;
			this.xrTableCell17.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell17.Weight = 0.048214766521536015;
			this.xrTableCell18.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.TotalValue")
			});
			this.xrTableCell18.Name = "xrTableCell18";
			this.xrTableCell18.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell18.StylePriority.UsePadding = false;
			this.xrTableCell18.StylePriority.UseTextAlignment = false;
			this.xrTableCell18.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell18.Weight = 0.7312399265355198;
			this.GroupHeader1.Controls.AddRange(new XRControl[]
			{
				this.lblThisLayer2,
				this.lblAllLayers2,
				this.lblThisPlan2,
				this.lblAllPlans2
			});
			this.GroupHeader1.HeightF = 27f;
			this.GroupHeader1.Name = "GroupHeader1";
			this.GroupHeader1.Visible = false;
			this.lblThisLayer2.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblThisLayer2.ForeColor = Color.DarkRed;
			this.lblThisLayer2.FormattingRules.Add(this.formattingRule11);
			this.lblThisLayer2.LocationFloat = new PointFloat(258.34f, 10f);
			this.lblThisLayer2.Name = "lblThisLayer2";
			this.lblThisLayer2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblThisLayer2.SizeF = new SizeF(189.58f, 17f);
			this.lblThisLayer2.StylePriority.UseFont = false;
			this.lblThisLayer2.StylePriority.UseForeColor = false;
			this.lblThisLayer2.StylePriority.UseTextAlignment = false;
			this.lblThisLayer2.Text = "CE CALQUE";
			this.lblThisLayer2.TextAlignment = TextAlignment.MiddleLeft;
			this.lblThisLayer2.Visible = false;
			this.formattingRule11.Condition = "IsNullOrEmpty([Name])";
			this.formattingRule11.DataMember = "Plans.Plans_Plan";
			this.formattingRule11.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule11.Name = "formattingRule11";
			this.lblAllLayers2.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblAllLayers2.ForeColor = Color.DarkRed;
			this.lblAllLayers2.FormattingRules.Add(this.formattingRule11);
			this.lblAllLayers2.LocationFloat = new PointFloat(460.42f, 10f);
			this.lblAllLayers2.Name = "lblAllLayers2";
			this.lblAllLayers2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblAllLayers2.SizeF = new SizeF(189.58f, 17f);
			this.lblAllLayers2.StylePriority.UseFont = false;
			this.lblAllLayers2.StylePriority.UseForeColor = false;
			this.lblAllLayers2.StylePriority.UseTextAlignment = false;
			this.lblAllLayers2.Text = "TOUS LES CALQUES";
			this.lblAllLayers2.TextAlignment = TextAlignment.MiddleLeft;
			this.lblAllLayers2.Visible = false;
			this.lblThisPlan2.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblThisPlan2.ForeColor = Color.DarkRed;
			this.lblThisPlan2.FormattingRules.Add(this.formattingRule10);
			this.lblThisPlan2.LocationFloat = new PointFloat(258.34f, 10f);
			this.lblThisPlan2.Name = "lblThisPlan2";
			this.lblThisPlan2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblThisPlan2.SizeF = new SizeF(189.58f, 17f);
			this.lblThisPlan2.StylePriority.UseFont = false;
			this.lblThisPlan2.StylePriority.UseForeColor = false;
			this.lblThisPlan2.StylePriority.UseTextAlignment = false;
			this.lblThisPlan2.Text = "CE PLAN";
			this.lblThisPlan2.TextAlignment = TextAlignment.MiddleLeft;
			this.lblThisPlan2.Visible = false;
			this.formattingRule10.Condition = "!IsNullOrEmpty([Name])";
			this.formattingRule10.DataMember = "Plans.Plans_Plan";
			this.formattingRule10.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule10.Name = "formattingRule10";
			this.lblAllPlans2.Font = new Font("Tahoma", 6f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblAllPlans2.ForeColor = Color.DarkRed;
			this.lblAllPlans2.FormattingRules.Add(this.formattingRule10);
			this.lblAllPlans2.LocationFloat = new PointFloat(460.42f, 10f);
			this.lblAllPlans2.Name = "lblAllPlans2";
			this.lblAllPlans2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblAllPlans2.SizeF = new SizeF(189.58f, 17f);
			this.lblAllPlans2.StylePriority.UseFont = false;
			this.lblAllPlans2.StylePriority.UseForeColor = false;
			this.lblAllPlans2.StylePriority.UseTextAlignment = false;
			this.lblAllPlans2.Text = "TOUS LES PLANS";
			this.lblAllPlans2.TextAlignment = TextAlignment.MiddleLeft;
			this.lblAllPlans2.Visible = false;
			this.DetailReport.Bands.AddRange(new Band[]
			{
				this.Detail1
			});
			this.DetailReport.Level = 1;
			this.DetailReport.Name = "DetailReport";
			this.DetailReport.Visible = false;
			this.Detail1.Controls.AddRange(new XRControl[]
			{
				this.xrLabel5
			});
			this.Detail1.HeightF = 30f;
			this.Detail1.Name = "Detail1";
			this.xrLabel5.BackColor = Color.Transparent;
			this.xrLabel5.BorderColor = Color.Black;
			this.xrLabel5.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel5.ForeColor = Color.Transparent;
			this.xrLabel5.LocationFloat = new PointFloat(197.85f, 10f);
			this.xrLabel5.Multiline = true;
			this.xrLabel5.Name = "xrLabel5";
			this.xrLabel5.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel5.SizeF = new SizeF(257.29f, 15f);
			this.xrLabel5.StylePriority.UseBackColor = false;
			this.xrLabel5.StylePriority.UseBorderColor = false;
			this.xrLabel5.StylePriority.UseFont = false;
			this.xrLabel5.StylePriority.UseForeColor = false;
			this.xrLabel5.StylePriority.UseTextAlignment = false;
			this.xrLabel5.Text = "* * *";
			this.xrLabel5.TextAlignment = TextAlignment.TopCenter;
			this.GroupHeader3.Controls.AddRange(new XRControl[]
			{
				this.xrLabel7
			});
			this.GroupHeader3.FormattingRules.Add(this.formattingRule11);
			this.GroupHeader3.GroupUnion = GroupUnion.WithFirstDetail;
			this.GroupHeader3.HeightF = 25f;
			this.GroupHeader3.Name = "GroupHeader3";
			this.GroupHeader3.Visible = false;
			this.xrLabel7.BackColor = Color.Transparent;
			this.xrLabel7.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Name")
			});
			this.xrLabel7.Font = new Font("Tahoma", 15f, FontStyle.Bold);
			this.xrLabel7.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel7.LocationFloat = new PointFloat(0.0007152557f, 0f);
			this.xrLabel7.Name = "xrLabel7";
			this.xrLabel7.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel7.SizeF = new SizeF(650f, 23f);
			this.xrLabel7.StylePriority.UseFont = false;
			this.xrLabel7.StylePriority.UseForeColor = false;
			this.xrLabel7.StylePriority.UsePadding = false;
			this.xrLabel7.StylePriority.UseTextAlignment = false;
			this.xrLabel7.TextAlignment = TextAlignment.MiddleCenter;
			this.GroupHeader4.Controls.AddRange(new XRControl[]
			{
				this.xrLabel3,
				this.xrLabel6
			});
			this.GroupHeader4.FormattingRules.Add(this.formattingRule10);
			this.GroupHeader4.GroupUnion = GroupUnion.WithFirstDetail;
			this.GroupHeader4.HeightF = 53f;
			this.GroupHeader4.KeepTogether = true;
			this.GroupHeader4.Level = 1;
			this.GroupHeader4.Name = "GroupHeader4";
			this.GroupHeader4.Visible = false;
			this.xrLabel3.BackColor = Color.Transparent;
			this.xrLabel3.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Name")
			});
			this.xrLabel3.Font = new Font("Tahoma", 12.5f, FontStyle.Bold | FontStyle.Underline);
			this.xrLabel3.ForeColor = Color.DarkRed;
			this.xrLabel3.LocationFloat = new PointFloat(0f, 27f);
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel3.SizeF = new SizeF(650f, 23f);
			this.xrLabel3.StylePriority.UseFont = false;
			this.xrLabel3.StylePriority.UseForeColor = false;
			this.xrLabel3.StylePriority.UsePadding = false;
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.TextAlignment = TextAlignment.MiddleCenter;
			this.xrLabel6.BackColor = Color.Transparent;
			this.xrLabel6.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Name")
			});
			this.xrLabel6.Font = new Font("Tahoma", 15f, FontStyle.Bold);
			this.xrLabel6.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel6.LocationFloat = new PointFloat(0f, 0f);
			this.xrLabel6.Name = "xrLabel6";
			this.xrLabel6.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel6.SizeF = new SizeF(650f, 23f);
			this.xrLabel6.StylePriority.UseFont = false;
			this.xrLabel6.StylePriority.UseForeColor = false;
			this.xrLabel6.StylePriority.UsePadding = false;
			this.xrLabel6.StylePriority.UseTextAlignment = false;
			this.xrLabel6.TextAlignment = TextAlignment.MiddleCenter;
			this.GroupHeader5.Controls.AddRange(new XRControl[]
			{
				this.xrLabel1
			});
			this.GroupHeader5.HeightF = 40f;
			this.GroupHeader5.Level = 2;
			this.GroupHeader5.Name = "GroupHeader5";
			this.xrLabel1.BackColor = Color.Transparent;
			this.xrLabel1.BorderColor = Color.Black;
			this.xrLabel1.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel1.ForeColor = Color.Transparent;
			this.xrLabel1.LocationFloat = new PointFloat(190.6303f, 0f);
			this.xrLabel1.Multiline = true;
			this.xrLabel1.Name = "xrLabel1";
			this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel1.SizeF = new SizeF(257.29f, 15f);
			this.xrLabel1.StylePriority.UseBackColor = false;
			this.xrLabel1.StylePriority.UseBorderColor = false;
			this.xrLabel1.StylePriority.UseFont = false;
			this.xrLabel1.StylePriority.UseForeColor = false;
			this.xrLabel1.StylePriority.UseTextAlignment = false;
			this.xrLabel1.Text = "* * *";
			this.xrLabel1.TextAlignment = TextAlignment.TopCenter;
			this.formattingRule2.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule2.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule2.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule2.Name = "formattingRule2";
			this.SubBand7.HeightF = 3.125f;
			this.SubBand7.Name = "SubBand7";
			this.lblReportTitle.BackColor = Color.Transparent;
			this.lblReportTitle.Font = new Font("Tahoma", 15.25f, FontStyle.Bold | FontStyle.Underline);
			this.lblReportTitle.ForeColor = Color.FromArgb(0, 64, 64);
			this.lblReportTitle.LocationFloat = new PointFloat(0f, 0f);
			this.lblReportTitle.Name = "lblReportTitle";
			this.lblReportTitle.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblReportTitle.SizeF = new SizeF(649.9998f, 23f);
			this.lblReportTitle.StylePriority.UseFont = false;
			this.lblReportTitle.StylePriority.UseForeColor = false;
			this.lblReportTitle.StylePriority.UsePadding = false;
			this.lblReportTitle.StylePriority.UseTextAlignment = false;
			this.lblReportTitle.Text = "RAPPORT DE MÉTRÉ (PAR PLANS)";
			this.lblReportTitle.TextAlignment = TextAlignment.MiddleCenter;
			this.formattingRule5.Condition = "IsNullOrEmpty([ContactName])";
			this.formattingRule5.DataMember = "Project";
			this.formattingRule5.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule5.Name = "formattingRule5";
			this.GroupHeader2.Controls.AddRange(new XRControl[]
			{
				this.lblReportTitle
			});
			this.GroupHeader2.HeightF = 30f;
			this.GroupHeader2.Name = "GroupHeader2";
			this.xrTable1.BorderColor = Color.DarkSlateGray;
			this.xrTable1.Borders = BorderSide.None;
			this.xrTable1.BorderWidth = 2f;
			this.xrTable1.LocationFloat = new PointFloat(0.001589457f, 0f);
			this.xrTable1.Name = "xrTable1";
			this.xrTable1.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow1,
				this.xrTableRow7,
				this.xrTableRow9
			});
			this.xrTable1.SizeF = new SizeF(313.9584f, 65f);
			this.xrTable1.StylePriority.UseBorderColor = false;
			this.xrTable1.StylePriority.UseBorders = false;
			this.xrTable1.StylePriority.UseBorderWidth = false;
			this.xrTable1.StylePriority.UsePadding = false;
			this.xrTableRow1.Cells.AddRange(new XRTableCell[]
			{
				this.lblProjectInfo
			});
			this.xrTableRow1.Name = "xrTableRow1";
			this.xrTableRow1.Weight = 0.736;
			this.lblProjectInfo.BackColor = Color.Transparent;
			this.lblProjectInfo.BorderColor = Color.White;
			this.lblProjectInfo.Borders = BorderSide.None;
			this.lblProjectInfo.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblProjectInfo.ForeColor = Color.Black;
			this.lblProjectInfo.Name = "lblProjectInfo";
			this.lblProjectInfo.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.lblProjectInfo.StylePriority.UseBackColor = false;
			this.lblProjectInfo.StylePriority.UseBorderColor = false;
			this.lblProjectInfo.StylePriority.UseFont = false;
			this.lblProjectInfo.StylePriority.UseForeColor = false;
			this.lblProjectInfo.StylePriority.UsePadding = false;
			this.lblProjectInfo.Text = "Nom du projet :";
			this.lblProjectInfo.TextAlignment = TextAlignment.MiddleLeft;
			this.lblProjectInfo.Weight = 1.0;
			this.xrTableRow7.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell2
			});
			this.xrTableRow7.Name = "xrTableRow7";
			this.xrTableRow7.Weight = 0.6719999999999999;
			this.xrTableCell2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.Name")
			});
			this.xrTableCell2.Font = new Font("Tahoma", 9.75f);
			this.xrTableCell2.Name = "xrTableCell2";
			this.xrTableCell2.Padding = new PaddingInfo(10, 0, 2, 0, 100f);
			this.xrTableCell2.StylePriority.UseFont = false;
			this.xrTableCell2.StylePriority.UsePadding = false;
			this.xrTableCell2.StylePriority.UseTextAlignment = false;
			this.xrTableCell2.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell2.Weight = 1.0;
			this.xrTableRow9.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell3
			});
			this.xrTableRow9.Name = "xrTableRow9";
			this.xrTableRow9.Weight = 0.672;
			this.xrTableCell3.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.Description")
			});
			this.xrTableCell3.Font = new Font("Tahoma", 9.75f);
			this.xrTableCell3.Multiline = true;
			this.xrTableCell3.Name = "xrTableCell3";
			this.xrTableCell3.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell3.StylePriority.UseFont = false;
			this.xrTableCell3.StylePriority.UsePadding = false;
			this.xrTableCell3.StylePriority.UseTextAlignment = false;
			this.xrTableCell3.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell3.Weight = 1.0;
			this.xrTable2.BorderColor = Color.DarkSlateGray;
			this.xrTable2.Borders = BorderSide.None;
			this.xrTable2.BorderWidth = 2f;
			this.xrTable2.LocationFloat = new PointFloat(339.0424f, 0f);
			this.xrTable2.Name = "xrTable2";
			this.xrTable2.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow3,
				this.xrTableRow2,
				this.xrTableRow4
			});
			this.xrTable2.SizeF = new SizeF(310.9584f, 65f);
			this.xrTable2.StylePriority.UseBorderColor = false;
			this.xrTable2.StylePriority.UseBorders = false;
			this.xrTable2.StylePriority.UseBorderWidth = false;
			this.xrTableRow3.Cells.AddRange(new XRTableCell[]
			{
				this.lblContactInfo
			});
			this.xrTableRow3.Name = "xrTableRow3";
			this.xrTableRow3.Weight = 0.736;
			this.lblContactInfo.BackColor = Color.Transparent;
			this.lblContactInfo.BorderColor = Color.White;
			this.lblContactInfo.Borders = BorderSide.None;
			this.lblContactInfo.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblContactInfo.ForeColor = Color.Black;
			this.lblContactInfo.Name = "lblContactInfo";
			this.lblContactInfo.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.lblContactInfo.StylePriority.UseBackColor = false;
			this.lblContactInfo.StylePriority.UseFont = false;
			this.lblContactInfo.StylePriority.UseForeColor = false;
			this.lblContactInfo.StylePriority.UsePadding = false;
			this.lblContactInfo.Text = "Nom du contact :";
			this.lblContactInfo.TextAlignment = TextAlignment.MiddleLeft;
			this.lblContactInfo.Weight = 1.0;
			this.xrTableRow2.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell1
			});
			this.xrTableRow2.Name = "xrTableRow2";
			this.xrTableRow2.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTableRow2.StylePriority.UsePadding = false;
			this.xrTableRow2.Weight = 0.6719999999999999;
			this.xrTableCell1.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.ContactName")
			});
			this.xrTableCell1.Font = new Font("Tahoma", 9.75f);
			this.xrTableCell1.Name = "xrTableCell1";
			this.xrTableCell1.Padding = new PaddingInfo(10, 0, 2, 0, 100f);
			this.xrTableCell1.StylePriority.UseFont = false;
			this.xrTableCell1.StylePriority.UsePadding = false;
			this.xrTableCell1.StylePriority.UseTextAlignment = false;
			this.xrTableCell1.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell1.Weight = 1.0;
			this.xrTableRow4.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell4
			});
			this.xrTableRow4.Name = "xrTableRow4";
			this.xrTableRow4.Weight = 0.672;
			this.xrTableCell4.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.ContactInfo")
			});
			this.xrTableCell4.Font = new Font("Tahoma", 9.75f);
			this.xrTableCell4.Multiline = true;
			this.xrTableCell4.Name = "xrTableCell4";
			this.xrTableCell4.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell4.StylePriority.UseBorderColor = false;
			this.xrTableCell4.StylePriority.UseFont = false;
			this.xrTableCell4.StylePriority.UsePadding = false;
			this.xrTableCell4.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell4.Weight = 1.0;
			this.xrTable6.BorderColor = Color.DarkSlateGray;
			this.xrTable6.Borders = BorderSide.None;
			this.xrTable6.BorderWidth = 2f;
			this.xrTable6.LocationFloat = new PointFloat(0f, 0f);
			this.xrTable6.Name = "xrTable6";
			this.xrTable6.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow11,
				this.xrTableRow12
			});
			this.xrTable6.SizeF = new SizeF(650f, 50f);
			this.xrTable6.StylePriority.UseBorderColor = false;
			this.xrTable6.StylePriority.UseBorders = false;
			this.xrTable6.StylePriority.UseBorderWidth = false;
			this.xrTableRow11.Cells.AddRange(new XRTableCell[]
			{
				this.lblComments
			});
			this.xrTableRow11.Name = "xrTableRow11";
			this.xrTableRow11.Weight = 0.736;
			this.lblComments.BackColor = Color.Transparent;
			this.lblComments.BorderColor = Color.White;
			this.lblComments.Borders = BorderSide.None;
			this.lblComments.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblComments.ForeColor = Color.Black;
			this.lblComments.Name = "lblComments";
			this.lblComments.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.lblComments.StylePriority.UseBackColor = false;
			this.lblComments.StylePriority.UseFont = false;
			this.lblComments.StylePriority.UseForeColor = false;
			this.lblComments.StylePriority.UsePadding = false;
			this.lblComments.Text = "Commentaires :";
			this.lblComments.TextAlignment = TextAlignment.MiddleLeft;
			this.lblComments.Weight = 1.0;
			this.xrTableRow12.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell12
			});
			this.xrTableRow12.Name = "xrTableRow12";
			this.xrTableRow12.Weight = 0.864;
			this.xrTableCell12.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.Comment")
			});
			this.xrTableCell12.Font = new Font("Tahoma", 9.75f);
			this.xrTableCell12.Multiline = true;
			this.xrTableCell12.Name = "xrTableCell12";
			this.xrTableCell12.Padding = new PaddingInfo(10, 0, 10, 10, 100f);
			this.xrTableCell12.StylePriority.UseBorderColor = false;
			this.xrTableCell12.StylePriority.UseFont = false;
			this.xrTableCell12.StylePriority.UsePadding = false;
			this.xrTableCell12.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell12.Weight = 1.0;
			base.Bands.AddRange(new Band[]
			{
				this.Detail,
				this.TopMargin,
				this.BottomMargin,
				this.ReportHeader,
				this.DetailReport2,
				this.GroupHeader2
			});
			base.DataMember = "Plans.Plans_Plan";
			base.FormattingRuleSheet.AddRange(new FormattingRule[]
			{
				this.formattingRule3,
				this.formattingRule1,
				this.formattingRule2,
				this.formattingRule4,
				this.formattingRule5,
				this.formattingRule6,
				this.formattingRule7,
				this.formattingRule8,
				this.formattingRule9,
				this.formattingRule10,
				this.formattingRule11,
				this.formattingRule12,
				this.formattingRule13
			});
			base.ScriptsSource = componentResourceManager.GetString("$this.ScriptsSource");
			base.Version = "14.2";
			this.XmlDataPath = "C:\\Users\\Patrick\\Documents\\Quoter Plan\\Mes rapports\\LE-PACIFIQUE-(11-Novembre-2014)-[Classé_par_plans].xml";
			((ISupportInitialize)this.xrTable7).EndInit();
			((ISupportInitialize)this.xrTable3).EndInit();
			((ISupportInitialize)this.xrTable1).EndInit();
			((ISupportInitialize)this.xrTable2).EndInit();
			((ISupportInitialize)this.xrTable6).EndInit();
			((ISupportInitialize)this).EndInit();
		}

		private Project project;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ReportHeaderBand ReportHeader;

		private XRPageInfo lbFooterlPageInfo;

		private DetailReportBand DetailReport2;

		private DetailBand Detail3;

		private FormattingRule formattingRule1;

		private FormattingRule formattingRule3;

		private FormattingRule formattingRule2;

		private XRLabel xrLabel7;

		private SubBand SubBand1;

		private FormattingRule formattingRule4;

		private XRLabel lblReportTitle;

		private FormattingRule formattingRule5;

		private FormattingRule formattingRule6;

		private FormattingRule formattingRule7;

		private FormattingRule formattingRule8;

		private GroupHeaderBand GroupHeader2;

		private XRLabel lblFooter2;

		private XRLabel lblFooter1;

		private XRLabel lbHeaderTitle;

		private DetailReportBand DetailReport4;

		private DetailBand Detail5;

		private XRLabel xrLabel14;

		private XRLabel lblHeader1;

		private XRPageInfo lbHeaderPageInfo;

		private XRLabel lblHeader2;

		private SubBand SubBand4;

		private SubBand SubBand5;

		private XRLabel xrLabel20;

		private XRLabel xrLabel21;

		private FormattingRule formattingRule9;

		private SubBand SubBand7;

		private DetailReportBand DetailReport8;

		private DetailBand Detail9;

		private DetailReportBand DetailReport11;

		private DetailBand Detail12;

		private DetailReportBand DetailReport12;

		private DetailBand Detail13;

		private XRLabel lblAllPlans1;

		private XRLabel lblThisPlan1;

		private XRLabel xrLabel2;

		private DetailReportBand DetailReport14;

		private DetailBand Detail15;

		private DetailReportBand DetailReport13;

		private DetailBand Detail14;

		private XRLabel lblAllPlans2;

		private XRLabel lblThisPlan2;

		private GroupHeaderBand GroupHeader1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel xrLabel5;

		private XRLine xrLine2;

		private XRLine xrLine1;

		private GroupHeaderBand GroupHeader3;

		private XRLabel xrLabel6;

		private XRLabel xrLabel8;

		private XRLabel xrLabel9;

		private XRLabel lbFooterDate;

		private XRTable xrTable3;

		private XRTableRow xrTableRow5;

		private XRTableCell xrTableCell14;

		private XRTableCell xrTableCell15;

		private XRTableCell xrTableCell16;

		private XRTableCell xrTableCell17;

		private XRTableCell xrTableCell18;

		private XRTable xrTable7;

		private XRTableRow xrTableRow6;

		private XRTableCell xrTableCell19;

		private XRTableCell xrTableCell5;

		private XRTableCell xrTableCell26;

		private GroupHeaderBand GroupHeader4;

		private GroupHeaderBand GroupHeader5;

		private XRLabel xrLabel1;

		private FormattingRule formattingRule10;

		private FormattingRule formattingRule11;

		private XRLabel xrLabel3;

		private SubBand SubBand8;

		private XRLabel lblThisLayer1;

		private XRLabel lblAllLayers1;

		private XRLabel lblThisLayer2;

		private XRLabel lblAllLayers2;

		private FormattingRule formattingRule12;

		private FormattingRule formattingRule13;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell lblProjectInfo;

		private XRTableRow xrTableRow7;

		private XRTableCell xrTableCell2;

		private XRTableRow xrTableRow9;

		private XRTableCell xrTableCell3;

		private XRTable xrTable2;

		private XRTableRow xrTableRow3;

		private XRTableCell lblContactInfo;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell1;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell4;

		private XRTable xrTable6;

		private XRTableRow xrTableRow11;

		private XRTableCell lblComments;

		private XRTableRow xrTableRow12;

		private XRTableCell xrTableCell12;
	}
}
