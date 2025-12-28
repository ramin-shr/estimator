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
	public class XtraReportEstimatingByPlans : XtraReport
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
			XRSummary xrsummary = new XRSummary();
			XRSummary xrsummary2 = new XRSummary();
			XRSummary xrsummary3 = new XRSummary();
			XRSummary xrsummary4 = new XRSummary();
			XRSummary xrsummary5 = new XRSummary();
			XRSummary xrsummary6 = new XRSummary();
			XRSummary xrsummary7 = new XRSummary();
			XRSummary xrsummary8 = new XRSummary();
			XRSummary xrsummary9 = new XRSummary();
			XRSummary xrsummary10 = new XRSummary();
			XRSummary xrsummary11 = new XRSummary();
			XRSummary xrsummary12 = new XRSummary();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XtraReportEstimatingByPlans));
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
			this.xrLabel24 = new XRLabel();
			this.xrLabel6 = new XRLabel();
			this.SubBand11 = new SubBand();
			this.xrLabel21 = new XRLabel();
			this.formattingRule9 = new FormattingRule();
			this.formattingRule16 = new FormattingRule();
			this.DetailReport12 = new DetailReportBand();
			this.Detail13 = new DetailBand();
			this.DetailReport13 = new DetailReportBand();
			this.Detail14 = new DetailBand();
			this.xrTable7 = new XRTable();
			this.xrTableRow6 = new XRTableRow();
			this.xrTableCell19 = new XRTableCell();
			this.xrTableCell20 = new XRTableCell();
			this.xrTableCell21 = new XRTableCell();
			this.xrTableCell22 = new XRTableCell();
			this.xrTableCell23 = new XRTableCell();
			this.xrTableCell24 = new XRTableCell();
			this.xrTableCell25 = new XRTableCell();
			this.xrTableCell26 = new XRTableCell();
			this.formattingRule12 = new FormattingRule();
			this.xrLine2 = new XRLine();
			this.GroupHeader7 = new GroupHeaderBand();
			this.xrLabel8 = new XRLabel();
			this.formattingRule20 = new FormattingRule();
			this.GroupHeader4 = new GroupHeaderBand();
			this.xrTable3 = new XRTable();
			this.xrTableRow5 = new XRTableRow();
			this.lblItem = new XRTableCell();
			this.lblQuantity = new XRTableCell();
			this.lblUnit = new XRTableCell();
			this.lblCost = new XRTableCell();
			this.lblCostTotal = new XRTableCell();
			this.lblPrice = new XRTableCell();
			this.lblPriceTotal = new XRTableCell();
			this.lblMargin = new XRTableCell();
			this.GroupFooter1 = new GroupFooterBand();
			this.xrTable8 = new XRTable();
			this.xrTableRow13 = new XRTableRow();
			this.xrTableCell27 = new XRTableCell();
			this.xrTableCell28 = new XRTableCell();
			this.xrTableCell29 = new XRTableCell();
			this.xrTableCell30 = new XRTableCell();
			this.xrTableCell31 = new XRTableCell();
			this.xrTableCell32 = new XRTableCell();
			this.xrTableCell33 = new XRTableCell();
			this.xrTableCell34 = new XRTableCell();
			this.formattingRule13 = new FormattingRule();
			this.DetailReport = new DetailReportBand();
			this.Detail1 = new DetailBand();
			this.xrLabel5 = new XRLabel();
			this.GroupHeader1 = new GroupHeaderBand();
			this.xrLabel2 = new XRLabel();
			this.formattingRule18 = new FormattingRule();
			this.GroupHeader5 = new GroupHeaderBand();
			this.xrLabel3 = new XRLabel();
			this.xrLabel7 = new XRLabel();
			this.formattingRule17 = new FormattingRule();
			this.GroupHeader6 = new GroupHeaderBand();
			this.xrLabel1 = new XRLabel();
			this.DetailReport1 = new DetailReportBand();
			this.Detail2 = new DetailBand();
			this.xrLine1 = new XRLine();
			this.xrTable10 = new XRTable();
			this.xrTableRow15 = new XRTableRow();
			this.xrTableCell36 = new XRTableCell();
			this.xrTableCell37 = new XRTableCell();
			this.xrTableCell38 = new XRTableCell();
			this.xrLabel4 = new XRLabel();
			this.xrTableCell40 = new XRTableCell();
			this.xrTableCell43 = new XRTableCell();
			this.xrTableCell44 = new XRTableCell();
			this.xrTableCell45 = new XRTableCell();
			this.xrTableCell46 = new XRTableCell();
			this.formattingRule14 = new FormattingRule();
			this.GroupHeader3 = new GroupHeaderBand();
			this.xrTable9 = new XRTable();
			this.xrTableRow14 = new XRTableRow();
			this.lblSummaryGroup = new XRTableCell();
			this.xrTableCell39 = new XRTableCell();
			this.lblSummaryBreakdown = new XRTableCell();
			this.xrTableCell42 = new XRTableCell();
			this.lblSummaryCostTotal = new XRTableCell();
			this.xrTableCell48 = new XRTableCell();
			this.lblSummaryPriceTotal = new XRTableCell();
			this.lblSummaryMargin = new XRTableCell();
			this.lblSummaryTotals = new XRLabel();
			this.GroupFooter2 = new GroupFooterBand();
			this.xrTable11 = new XRTable();
			this.xrTableRow16 = new XRTableRow();
			this.xrTableCell51 = new XRTableCell();
			this.xrTableCell52 = new XRTableCell();
			this.xrTableCell53 = new XRTableCell();
			this.lblSummarySubTotals = new XRTableCell();
			this.xrTableCell55 = new XRTableCell();
			this.xrTableCell56 = new XRTableCell();
			this.xrTableCell57 = new XRTableCell();
			this.xrTableCell58 = new XRTableCell();
			this.formattingRule15 = new FormattingRule();
			this.SubBand6 = new SubBand();
			this.xrTable12 = new XRTable();
			this.xrTableRow17 = new XRTableRow();
			this.xrTableCell59 = new XRTableCell();
			this.xrTableCell60 = new XRTableCell();
			this.xrTableCell61 = new XRTableCell();
			this.xrTableCell62 = new XRTableCell();
			this.xrTableCell63 = new XRTableCell();
			this.lblSummaryTax1 = new XRTableCell();
			this.xrTableCell65 = new XRTableCell();
			this.xrTableCell66 = new XRTableCell();
			this.SubBand8 = new SubBand();
			this.xrTable13 = new XRTable();
			this.xrTableRow18 = new XRTableRow();
			this.xrTableCell67 = new XRTableCell();
			this.xrTableCell68 = new XRTableCell();
			this.xrTableCell69 = new XRTableCell();
			this.xrTableCell70 = new XRTableCell();
			this.xrTableCell71 = new XRTableCell();
			this.lblSummaryTax2 = new XRTableCell();
			this.xrTableCell73 = new XRTableCell();
			this.xrTableCell74 = new XRTableCell();
			this.SubBand9 = new SubBand();
			this.xrTable14 = new XRTable();
			this.xrTableRow19 = new XRTableRow();
			this.xrTableCell75 = new XRTableCell();
			this.xrTableCell76 = new XRTableCell();
			this.xrTableCell77 = new XRTableCell();
			this.xrTableCell78 = new XRTableCell();
			this.xrTableCell79 = new XRTableCell();
			this.lblSummaryGrandTotal = new XRTableCell();
			this.xrTableCell81 = new XRTableCell();
			this.xrTableCell82 = new XRTableCell();
			this.formattingRule2 = new FormattingRule();
			this.formattingRule1 = new FormattingRule();
			this.formattingRule3 = new FormattingRule();
			this.SubBand7 = new SubBand();
			this.lblReportTitle = new XRLabel();
			this.formattingRule5 = new FormattingRule();
			this.GroupHeader2 = new GroupHeaderBand();
			this.formattingRule10 = new FormattingRule();
			this.formattingRule11 = new FormattingRule();
			this.formattingRule19 = new FormattingRule();
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
			((ISupportInitialize)this.xrTable8).BeginInit();
			((ISupportInitialize)this.xrTable10).BeginInit();
			((ISupportInitialize)this.xrTable9).BeginInit();
			((ISupportInitialize)this.xrTable11).BeginInit();
			((ISupportInitialize)this.xrTable12).BeginInit();
			((ISupportInitialize)this.xrTable13).BeginInit();
			((ISupportInitialize)this.xrTable14).BeginInit();
			((ISupportInitialize)this.xrTable1).BeginInit();
			((ISupportInitialize)this.xrTable2).BeginInit();
			((ISupportInitialize)this.xrTable6).BeginInit();
			((ISupportInitialize)this).BeginInit();
			this.Detail.Expanded = false;
			this.Detail.HeightF = 0f;
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
			this.lblHeader2.LocationFloat = new PointFloat(777.7067f, 37.5f);
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
			this.lblHeader1.LocationFloat = new PointFloat(777.7067f, 20.49999f);
			this.lblHeader1.Name = "lblHeader1";
			this.lblHeader1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblHeader1.SizeF = new SizeF(122.2933f, 17f);
			this.lblHeader1.StylePriority.UseBorderColor = false;
			this.lblHeader1.StylePriority.UseBorders = false;
			this.lblHeader1.StylePriority.UseBorderWidth = false;
			this.lblHeader1.StylePriority.UseFont = false;
			this.lblHeader1.StylePriority.UsePadding = false;
			this.lblHeader1.StylePriority.UseTextAlignment = false;
			this.lblHeader1.Text = "Rapport d'estimation";
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
			this.lbHeaderPageInfo.LocationFloat = new PointFloat(748.9998f, 58.27084f);
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
			this.lbFooterDate.LocationFloat = new PointFloat(0f, 37.5f);
			this.lbFooterDate.Name = "lbFooterDate";
			this.lbFooterDate.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lbFooterDate.SizeF = new SizeF(186.46f, 17f);
			this.lbFooterDate.StylePriority.UseFont = false;
			this.lbFooterDate.StylePriority.UseTextAlignment = false;
			this.lbFooterDate.TextAlignment = TextAlignment.MiddleLeft;
			this.lblFooter2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblFooter2.LocationFloat = new PointFloat(321.355f, 49.99997f);
			this.lblFooter2.Name = "lblFooter2";
			this.lblFooter2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblFooter2.SizeF = new SizeF(257.29f, 15f);
			this.lblFooter2.StylePriority.UseFont = false;
			this.lblFooter2.StylePriority.UseTextAlignment = false;
			this.lblFooter2.Text = "www.quoterplan.com";
			this.lblFooter2.TextAlignment = TextAlignment.MiddleCenter;
			this.lblFooter1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblFooter1.LocationFloat = new PointFloat(321.355f, 35.00003f);
			this.lblFooter1.Name = "lblFooter1";
			this.lblFooter1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblFooter1.SizeF = new SizeF(257.29f, 15f);
			this.lblFooter1.StylePriority.UseFont = false;
			this.lblFooter1.StylePriority.UseTextAlignment = false;
			this.lblFooter1.Text = "Ce rapport a été généré grace à Quoter Plan";
			this.lblFooter1.TextAlignment = TextAlignment.MiddleCenter;
			this.lbFooterlPageInfo.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbFooterlPageInfo.Format = "Page {0} de {1}";
			this.lbFooterlPageInfo.LocationFloat = new PointFloat(748.9998f, 37.5f);
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
			this.xrLabel20.LocationFloat = new PointFloat(325f, 12.5f);
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
				this.DetailReport8,
				this.DetailReport1
			});
			this.DetailReport2.DataMember = "ReportPlans.ReportPlans_Plans";
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
			this.DetailReport4.Level = 1;
			this.DetailReport4.Name = "DetailReport4";
			this.DetailReport4.Visible = false;
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
			this.xrLabel14.LocationFloat = new PointFloat(325f, 0f);
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
				this.GroupHeader1,
				this.GroupHeader5,
				this.GroupHeader6
			});
			this.DetailReport11.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object";
			this.DetailReport11.Level = 0;
			this.DetailReport11.Name = "DetailReport11";
			this.Detail12.Controls.AddRange(new XRControl[]
			{
				this.xrLabel24,
				this.xrLabel6
			});
			this.Detail12.HeightF = 53f;
			this.Detail12.KeepTogetherWithDetailReports = true;
			this.Detail12.Name = "Detail12";
			this.Detail12.SubBands.AddRange(new SubBand[]
			{
				this.SubBand11
			});
			this.xrLabel24.BackColor = Color.Transparent;
			this.xrLabel24.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Name")
			});
			this.xrLabel24.Font = new Font("Tahoma", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel24.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel24.LocationFloat = new PointFloat(32f, 25f);
			this.xrLabel24.Name = "xrLabel24";
			this.xrLabel24.Padding = new PaddingInfo(6, 2, 1, 0, 100f);
			this.xrLabel24.SizeF = new SizeF(868f, 23f);
			this.xrLabel24.StylePriority.UseFont = false;
			this.xrLabel24.StylePriority.UseForeColor = false;
			this.xrLabel24.StylePriority.UsePadding = false;
			this.xrLabel6.BorderColor = Color.FromArgb(32, 32, 32);
			this.xrLabel6.Borders = BorderSide.All;
			this.xrLabel6.BorderWidth = 2f;
			this.xrLabel6.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Color")
			});
			this.xrLabel6.LocationFloat = new PointFloat(10f, 27f);
			this.xrLabel6.Name = "xrLabel6";
			this.xrLabel6.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrLabel6.Scripts.OnBeforePrint = "xrLabel6_BeforePrint";
			this.xrLabel6.SizeF = new SizeF(21f, 21f);
			this.xrLabel6.StylePriority.UseBorderColor = false;
			this.xrLabel6.StylePriority.UseBorders = false;
			this.xrLabel6.StylePriority.UseBorderWidth = false;
			this.xrLabel6.StylePriority.UsePadding = false;
			this.SubBand11.Controls.AddRange(new XRControl[]
			{
				this.xrLabel21
			});
			this.SubBand11.FormattingRules.Add(this.formattingRule16);
			this.SubBand11.HeightF = 20f;
			this.SubBand11.KeepTogether = true;
			this.SubBand11.Name = "SubBand11";
			this.SubBand11.Visible = false;
			this.xrLabel21.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Comment")
			});
			this.xrLabel21.Font = new Font("Tahoma", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel21.ForeColor = Color.FromArgb(64, 64, 64);
			this.xrLabel21.FormattingRules.Add(this.formattingRule9);
			this.xrLabel21.LocationFloat = new PointFloat(31.99979f, 0f);
			this.xrLabel21.Multiline = true;
			this.xrLabel21.Name = "xrLabel21";
			this.xrLabel21.Padding = new PaddingInfo(6, 2, 2, 2, 100f);
			this.xrLabel21.SizeF = new SizeF(868f, 18f);
			this.xrLabel21.StylePriority.UseBorderColor = false;
			this.xrLabel21.StylePriority.UseFont = false;
			this.xrLabel21.StylePriority.UseForeColor = false;
			this.xrLabel21.StylePriority.UsePadding = false;
			this.xrLabel21.StylePriority.UseTextAlignment = false;
			this.xrLabel21.TextAlignment = TextAlignment.MiddleLeft;
			this.formattingRule9.Condition = "[EstimatingItem] == true";
			this.formattingRule9.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result";
			this.formattingRule9.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule9.Name = "formattingRule9";
			this.formattingRule16.Condition = "!IsNullOrEmpty([Comment])";
			this.formattingRule16.DataMember = "Objects.Objects_Layer.Layer_Object";
			this.formattingRule16.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule16.Name = "formattingRule16";
			this.DetailReport12.Bands.AddRange(new Band[]
			{
				this.Detail13,
				this.DetailReport13,
				this.GroupHeader4,
				this.GroupFooter1
			});
			this.DetailReport12.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.DetailReport12.Level = 0;
			this.DetailReport12.Name = "DetailReport12";
			this.Detail13.Expanded = false;
			this.Detail13.HeightF = 0f;
			this.Detail13.Name = "Detail13";
			this.DetailReport13.Bands.AddRange(new Band[]
			{
				this.Detail14,
				this.GroupHeader7
			});
			this.DetailReport13.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result";
			this.DetailReport13.Level = 0;
			this.DetailReport13.Name = "DetailReport13";
			this.Detail14.Controls.AddRange(new XRControl[]
			{
				this.xrTable7,
				this.xrLine2
			});
			this.Detail14.FormattingRules.Add(this.formattingRule9);
			this.Detail14.HeightF = 23f;
			this.Detail14.Name = "Detail14";
			this.Detail14.Visible = false;
			this.xrTable7.Borders = BorderSide.None;
			this.xrTable7.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable7.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable7.LocationFloat = new PointFloat(0f, 0f);
			this.xrTable7.Name = "xrTable7";
			this.xrTable7.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow6
			});
			this.xrTable7.Scripts.OnBeforePrint = "xrTable7_BeforePrint";
			this.xrTable7.SizeF = new SizeF(900f, 18f);
			this.xrTable7.StylePriority.UseBorders = false;
			this.xrTable7.StylePriority.UseFont = false;
			this.xrTable7.StylePriority.UseForeColor = false;
			this.xrTable7.StylePriority.UseTextAlignment = false;
			this.xrTable7.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow6.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell19,
				this.xrTableCell20,
				this.xrTableCell21,
				this.xrTableCell22,
				this.xrTableCell23,
				this.xrTableCell24,
				this.xrTableCell25,
				this.xrTableCell26
			});
			this.xrTableRow6.Name = "xrTableRow6";
			this.xrTableRow6.Weight = 1.0;
			this.xrTableCell19.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.EstimatingCaption")
			});
			this.xrTableCell19.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell19.Name = "xrTableCell19";
			this.xrTableCell19.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell19.StylePriority.UseFont = false;
			this.xrTableCell19.StylePriority.UsePadding = false;
			this.xrTableCell19.Weight = 1.7528238329211578;
			this.xrTableCell20.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.Quantity")
			});
			this.xrTableCell20.Name = "xrTableCell20";
			this.xrTableCell20.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell20.StylePriority.UsePadding = false;
			this.xrTableCell20.StylePriority.UseTextAlignment = false;
			this.xrTableCell20.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell20.Weight = 0.8859793702230667;
			this.xrTableCell21.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.EstimatingUnit")
			});
			this.xrTableCell21.Name = "xrTableCell21";
			this.xrTableCell21.Padding = new PaddingInfo(6, 3, 1, 0, 100f);
			this.xrTableCell21.StylePriority.UsePadding = false;
			this.xrTableCell21.Weight = 0.5315625034745683;
			this.xrTableCell22.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.CostEach")
			});
			this.xrTableCell22.Name = "xrTableCell22";
			this.xrTableCell22.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell22.StylePriority.UsePadding = false;
			this.xrTableCell22.StylePriority.UseTextAlignment = false;
			this.xrTableCell22.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell22.Weight = 0.9302324657345029;
			this.xrTableCell23.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.CostTotal")
			});
			this.xrTableCell23.Name = "xrTableCell23";
			this.xrTableCell23.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell23.StylePriority.UsePadding = false;
			this.xrTableCell23.StylePriority.UseTextAlignment = false;
			this.xrTableCell23.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell23.Weight = 1.1074196474734324;
			this.xrTableCell24.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.PriceEach")
			});
			this.xrTableCell24.Name = "xrTableCell24";
			this.xrTableCell24.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell24.StylePriority.UsePadding = false;
			this.xrTableCell24.StylePriority.UseTextAlignment = false;
			this.xrTableCell24.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell24.Weight = 0.9302303366012893;
			this.xrTableCell25.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.PriceTotal")
			});
			this.xrTableCell25.Name = "xrTableCell25";
			this.xrTableCell25.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell25.StylePriority.UsePadding = false;
			this.xrTableCell25.StylePriority.UseTextAlignment = false;
			this.xrTableCell25.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell25.Weight = 1.1074196474734321;
			this.xrTableCell26.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.Margin")
			});
			this.xrTableCell26.FormattingRules.Add(this.formattingRule12);
			this.xrTableCell26.Name = "xrTableCell26";
			this.xrTableCell26.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell26.StylePriority.UsePadding = false;
			this.xrTableCell26.StylePriority.UseTextAlignment = false;
			xrsummary.FormatString = "{0:0.00%}";
			xrsummary.Func = SummaryFunc.Avg;
			this.xrTableCell26.Summary = xrsummary;
			this.xrTableCell26.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell26.Visible = false;
			this.xrTableCell26.Weight = 0.7543316553662692;
			this.formattingRule12.Condition = "[RawMargin] > 0";
			this.formattingRule12.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result";
			this.formattingRule12.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule12.Name = "formattingRule12";
			this.xrLine2.BorderColor = Color.Transparent;
			this.xrLine2.ForeColor = Color.DarkGray;
			this.xrLine2.LineStyle = DashStyle.Dot;
			this.xrLine2.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine2.Name = "xrLine2";
			this.xrLine2.SizeF = new SizeF(900f, 3f);
			this.xrLine2.StylePriority.UseBorderColor = false;
			this.xrLine2.StylePriority.UseForeColor = false;
			this.GroupHeader7.Controls.AddRange(new XRControl[]
			{
				this.xrLabel8
			});
			this.GroupHeader7.HeightF = 18f;
			this.GroupHeader7.Name = "GroupHeader7";
			this.xrLabel8.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Name")
			});
			this.xrLabel8.Font = new Font("Tahoma", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel8.ForeColor = Color.DarkRed;
			this.xrLabel8.FormattingRules.Add(this.formattingRule20);
			this.xrLabel8.LocationFloat = new PointFloat(0.0001271566f, 0f);
			this.xrLabel8.Multiline = true;
			this.xrLabel8.Name = "xrLabel8";
			this.xrLabel8.Padding = new PaddingInfo(10, 2, 4, 2, 100f);
			this.xrLabel8.SizeF = new SizeF(900.0001f, 18f);
			this.xrLabel8.StylePriority.UseBorderColor = false;
			this.xrLabel8.StylePriority.UseFont = false;
			this.xrLabel8.StylePriority.UseForeColor = false;
			this.xrLabel8.StylePriority.UsePadding = false;
			this.xrLabel8.StylePriority.UseTextAlignment = false;
			this.xrLabel8.TextAlignment = TextAlignment.MiddleLeft;
			this.formattingRule20.Condition = "[BaseExtension] ==True Or IsNullOrEmpty([Extension_Results]) Or IsNullOrEmpty([Extension_Results.Results_Result])";
			this.formattingRule20.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule20.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule20.Name = "formattingRule20";
			this.GroupHeader4.Controls.AddRange(new XRControl[]
			{
				this.xrTable3
			});
			this.GroupHeader4.GroupUnion = GroupUnion.WithFirstDetail;
			this.GroupHeader4.HeightF = 35f;
			this.GroupHeader4.Name = "GroupHeader4";
			this.xrTable3.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline);
			this.xrTable3.ForeColor = Color.DarkRed;
			this.xrTable3.LocationFloat = new PointFloat(0f, 8f);
			this.xrTable3.Name = "xrTable3";
			this.xrTable3.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable3.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow5
			});
			this.xrTable3.SizeF = new SizeF(900f, 22.92f);
			this.xrTable3.StylePriority.UseFont = false;
			this.xrTable3.StylePriority.UseForeColor = false;
			this.xrTable3.StylePriority.UsePadding = false;
			this.xrTableRow5.Cells.AddRange(new XRTableCell[]
			{
				this.lblItem,
				this.lblQuantity,
				this.lblUnit,
				this.lblCost,
				this.lblCostTotal,
				this.lblPrice,
				this.lblPriceTotal,
				this.lblMargin
			});
			this.xrTableRow5.Name = "xrTableRow5";
			this.xrTableRow5.Weight = 1.0;
			this.lblItem.Name = "lblItem";
			this.lblItem.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblItem.StylePriority.UsePadding = false;
			this.lblItem.StylePriority.UseTextAlignment = false;
			this.lblItem.Text = "Item";
			this.lblItem.TextAlignment = TextAlignment.MiddleLeft;
			this.lblItem.Weight = 1.7528671039761468;
			this.lblQuantity.Name = "lblQuantity";
			this.lblQuantity.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblQuantity.StylePriority.UsePadding = false;
			this.lblQuantity.StylePriority.UseTextAlignment = false;
			this.lblQuantity.Text = "Quantité";
			this.lblQuantity.TextAlignment = TextAlignment.MiddleRight;
			this.lblQuantity.Weight = 0.8859358332191036;
			this.lblUnit.Name = "lblUnit";
			this.lblUnit.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.lblUnit.StylePriority.UsePadding = false;
			this.lblUnit.StylePriority.UseTextAlignment = false;
			this.lblUnit.Text = "Unité";
			this.lblUnit.TextAlignment = TextAlignment.MiddleLeft;
			this.lblUnit.Weight = 0.5315614955897898;
			this.lblCost.Name = "lblCost";
			this.lblCost.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblCost.StylePriority.UsePadding = false;
			this.lblCost.StylePriority.UseTextAlignment = false;
			this.lblCost.Text = "Coûtant";
			this.lblCost.TextAlignment = TextAlignment.MiddleRight;
			this.lblCost.Weight = 0.9302325889412165;
			this.lblCostTotal.Name = "lblCostTotal";
			this.lblCostTotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblCostTotal.StylePriority.UsePadding = false;
			this.lblCostTotal.StylePriority.UseTextAlignment = false;
			this.lblCostTotal.Text = "Coûtant total";
			this.lblCostTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblCostTotal.Weight = 1.1074197766683267;
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblPrice.StylePriority.UsePadding = false;
			this.lblPrice.StylePriority.UseTextAlignment = false;
			this.lblPrice.Text = "Prix";
			this.lblPrice.TextAlignment = TextAlignment.MiddleRight;
			this.lblPrice.Weight = 0.93023261674881;
			this.lblPriceTotal.Name = "lblPriceTotal";
			this.lblPriceTotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblPriceTotal.StylePriority.UsePadding = false;
			this.lblPriceTotal.StylePriority.UseTextAlignment = false;
			this.lblPriceTotal.Text = "Prix total";
			this.lblPriceTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblPriceTotal.Weight = 1.1074197766683258;
			this.lblMargin.Name = "lblMargin";
			this.lblMargin.Padding = new PaddingInfo(1, 10, 0, 0, 100f);
			this.lblMargin.StylePriority.UsePadding = false;
			this.lblMargin.StylePriority.UseTextAlignment = false;
			this.lblMargin.Text = "Marge";
			this.lblMargin.TextAlignment = TextAlignment.MiddleRight;
			this.lblMargin.Weight = 0.7543302674559997;
			this.GroupFooter1.Controls.AddRange(new XRControl[]
			{
				this.xrTable8
			});
			this.GroupFooter1.GroupUnion = GroupFooterUnion.WithLastDetail;
			this.GroupFooter1.HeightF = 30f;
			this.GroupFooter1.Name = "GroupFooter1";
			this.xrTable8.Borders = BorderSide.None;
			this.xrTable8.BorderWidth = 3f;
			this.xrTable8.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable8.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable8.LocationFloat = new PointFloat(0f, 5f);
			this.xrTable8.Name = "xrTable8";
			this.xrTable8.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow13
			});
			this.xrTable8.SizeF = new SizeF(900f, 18f);
			this.xrTable8.StylePriority.UseBorders = false;
			this.xrTable8.StylePriority.UseBorderWidth = false;
			this.xrTable8.StylePriority.UseFont = false;
			this.xrTable8.StylePriority.UseForeColor = false;
			this.xrTable8.StylePriority.UseTextAlignment = false;
			this.xrTable8.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow13.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell27,
				this.xrTableCell28,
				this.xrTableCell29,
				this.xrTableCell30,
				this.xrTableCell31,
				this.xrTableCell32,
				this.xrTableCell33,
				this.xrTableCell34
			});
			this.xrTableRow13.Name = "xrTableRow13";
			this.xrTableRow13.Weight = 1.0;
			this.xrTableCell27.Name = "xrTableCell27";
			this.xrTableCell27.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell27.StylePriority.UsePadding = false;
			this.xrTableCell27.Weight = 1.752823830580536;
			this.xrTableCell28.Name = "xrTableCell28";
			this.xrTableCell28.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell28.StylePriority.UsePadding = false;
			this.xrTableCell28.StylePriority.UseTextAlignment = false;
			this.xrTableCell28.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell28.Weight = 0.8859357060943043;
			this.xrTableCell29.Name = "xrTableCell29";
			this.xrTableCell29.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell29.StylePriority.UsePadding = false;
			this.xrTableCell29.Weight = 0.5315614220100788;
			this.xrTableCell30.Name = "xrTableCell30";
			this.xrTableCell30.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell30.StylePriority.UsePadding = false;
			this.xrTableCell30.StylePriority.UseTextAlignment = false;
			this.xrTableCell30.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell30.Weight = 0.9302324668585246;
			this.xrTableCell31.Borders = BorderSide.None;
			this.xrTableCell31.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Summary.CostTotalSubTotal")
			});
			this.xrTableCell31.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell31.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell31.Name = "xrTableCell31";
			this.xrTableCell31.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell31.StylePriority.UseBorders = false;
			this.xrTableCell31.StylePriority.UseFont = false;
			this.xrTableCell31.StylePriority.UseForeColor = false;
			this.xrTableCell31.StylePriority.UsePadding = false;
			this.xrTableCell31.StylePriority.UseTextAlignment = false;
			this.xrTableCell31.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell31.Weight = 1.107419581005925;
			this.xrTableCell32.Name = "xrTableCell32";
			this.xrTableCell32.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell32.StylePriority.UsePadding = false;
			this.xrTableCell32.StylePriority.UseTextAlignment = false;
			this.xrTableCell32.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell32.Weight = 0.9302324993991369;
			this.xrTableCell33.Borders = BorderSide.None;
			this.xrTableCell33.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Summary.PriceTotalSubTotal")
			});
			this.xrTableCell33.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell33.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell33.Name = "xrTableCell33";
			this.xrTableCell33.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell33.StylePriority.UseBorders = false;
			this.xrTableCell33.StylePriority.UseFont = false;
			this.xrTableCell33.StylePriority.UseForeColor = false;
			this.xrTableCell33.StylePriority.UsePadding = false;
			this.xrTableCell33.StylePriority.UseTextAlignment = false;
			xrsummary2.FormatString = "{0:c}";
			this.xrTableCell33.Summary = xrsummary2;
			this.xrTableCell33.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell33.Weight = 1.1074195798819029;
			this.xrTableCell34.Borders = BorderSide.None;
			this.xrTableCell34.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Summary.MarginSubTotal")
			});
			this.xrTableCell34.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell34.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell34.FormattingRules.Add(this.formattingRule13);
			this.xrTableCell34.Name = "xrTableCell34";
			this.xrTableCell34.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell34.StylePriority.UseBorders = false;
			this.xrTableCell34.StylePriority.UseFont = false;
			this.xrTableCell34.StylePriority.UseForeColor = false;
			this.xrTableCell34.StylePriority.UsePadding = false;
			this.xrTableCell34.StylePriority.UseTextAlignment = false;
			xrsummary3.Func = SummaryFunc.Custom;
			this.xrTableCell34.Summary = xrsummary3;
			this.xrTableCell34.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell34.Visible = false;
			this.xrTableCell34.Weight = 0.7543739676644248;
			this.formattingRule13.Condition = "[RawMarginSubTotal] > 0";
			this.formattingRule13.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Summary";
			this.formattingRule13.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule13.Name = "formattingRule13";
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
			this.Detail1.HeightF = 2f;
			this.Detail1.Name = "Detail1";
			this.xrLabel5.BackColor = Color.Transparent;
			this.xrLabel5.BorderColor = Color.Black;
			this.xrLabel5.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel5.ForeColor = Color.Transparent;
			this.xrLabel5.LocationFloat = new PointFloat(325f, 0f);
			this.xrLabel5.Multiline = true;
			this.xrLabel5.Name = "xrLabel5";
			this.xrLabel5.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel5.SizeF = new SizeF(257.29f, 2f);
			this.xrLabel5.StylePriority.UseBackColor = false;
			this.xrLabel5.StylePriority.UseBorderColor = false;
			this.xrLabel5.StylePriority.UseFont = false;
			this.xrLabel5.StylePriority.UseForeColor = false;
			this.xrLabel5.StylePriority.UseTextAlignment = false;
			this.xrLabel5.Text = "* * *";
			this.xrLabel5.TextAlignment = TextAlignment.TopCenter;
			this.GroupHeader1.Controls.AddRange(new XRControl[]
			{
				this.xrLabel2
			});
			this.GroupHeader1.FormattingRules.Add(this.formattingRule18);
			this.GroupHeader1.GroupUnion = GroupUnion.WithFirstDetail;
			this.GroupHeader1.HeightF = 25f;
			this.GroupHeader1.Name = "GroupHeader1";
			this.GroupHeader1.Visible = false;
			this.xrLabel2.BackColor = Color.Transparent;
			this.xrLabel2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Name")
			});
			this.xrLabel2.Font = new Font("Tahoma", 15f, FontStyle.Bold);
			this.xrLabel2.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel2.LocationFloat = new PointFloat(0f, 0f);
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel2.SizeF = new SizeF(899.9999f, 23f);
			this.xrLabel2.StylePriority.UseFont = false;
			this.xrLabel2.StylePriority.UseForeColor = false;
			this.xrLabel2.StylePriority.UsePadding = false;
			this.xrLabel2.StylePriority.UseTextAlignment = false;
			this.xrLabel2.TextAlignment = TextAlignment.MiddleCenter;
			this.formattingRule18.Condition = "IsNullOrEmpty([Name])";
			this.formattingRule18.DataMember = "Plans.Plans_Plan";
			this.formattingRule18.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule18.Name = "formattingRule18";
			this.GroupHeader5.Controls.AddRange(new XRControl[]
			{
				this.xrLabel3,
				this.xrLabel7
			});
			this.GroupHeader5.FormattingRules.Add(this.formattingRule17);
			this.GroupHeader5.GroupUnion = GroupUnion.WithFirstDetail;
			this.GroupHeader5.HeightF = 53f;
			this.GroupHeader5.KeepTogether = true;
			this.GroupHeader5.Level = 1;
			this.GroupHeader5.Name = "GroupHeader5";
			this.GroupHeader5.Visible = false;
			this.xrLabel3.BackColor = Color.Transparent;
			this.xrLabel3.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Name")
			});
			this.xrLabel3.Font = new Font("Tahoma", 12.5f, FontStyle.Bold | FontStyle.Underline);
			this.xrLabel3.ForeColor = Color.DarkRed;
			this.xrLabel3.LocationFloat = new PointFloat(0f, 27.00005f);
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel3.SizeF = new SizeF(899.9999f, 23f);
			this.xrLabel3.StylePriority.UseFont = false;
			this.xrLabel3.StylePriority.UseForeColor = false;
			this.xrLabel3.StylePriority.UsePadding = false;
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.TextAlignment = TextAlignment.MiddleCenter;
			this.xrLabel7.BackColor = Color.Transparent;
			this.xrLabel7.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Plans.Plans_Plan.Name")
			});
			this.xrLabel7.Font = new Font("Tahoma", 15f, FontStyle.Bold);
			this.xrLabel7.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel7.LocationFloat = new PointFloat(0f, 0f);
			this.xrLabel7.Name = "xrLabel7";
			this.xrLabel7.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel7.SizeF = new SizeF(899.9999f, 23f);
			this.xrLabel7.StylePriority.UseFont = false;
			this.xrLabel7.StylePriority.UseForeColor = false;
			this.xrLabel7.StylePriority.UsePadding = false;
			this.xrLabel7.StylePriority.UseTextAlignment = false;
			this.xrLabel7.TextAlignment = TextAlignment.MiddleCenter;
			this.formattingRule17.Condition = "!IsNullOrEmpty([Name])";
			this.formattingRule17.DataMember = "Plans.Plans_Plan";
			this.formattingRule17.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule17.Name = "formattingRule17";
			this.GroupHeader6.Controls.AddRange(new XRControl[]
			{
				this.xrLabel1
			});
			this.GroupHeader6.HeightF = 40f;
			this.GroupHeader6.Level = 2;
			this.GroupHeader6.Name = "GroupHeader6";
			this.xrLabel1.BackColor = Color.Transparent;
			this.xrLabel1.BorderColor = Color.Black;
			this.xrLabel1.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel1.ForeColor = Color.Transparent;
			this.xrLabel1.LocationFloat = new PointFloat(321.355f, 0f);
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
			this.DetailReport1.Bands.AddRange(new Band[]
			{
				this.Detail2,
				this.GroupHeader3,
				this.GroupFooter2
			});
			this.DetailReport1.DataMember = "Summaries.Summaries_Summary";
			this.DetailReport1.Level = 2;
			this.DetailReport1.Name = "DetailReport1";
			this.DetailReport1.Visible = false;
			this.Detail2.Controls.AddRange(new XRControl[]
			{
				this.xrLine1,
				this.xrTable10
			});
			this.Detail2.HeightF = 23f;
			this.Detail2.Name = "Detail2";
			this.xrLine1.BorderColor = Color.Transparent;
			this.xrLine1.ForeColor = Color.DarkGray;
			this.xrLine1.LineStyle = DashStyle.Dot;
			this.xrLine1.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new SizeF(900f, 3f);
			this.xrLine1.StylePriority.UseBorderColor = false;
			this.xrLine1.StylePriority.UseForeColor = false;
			this.xrTable10.Borders = BorderSide.None;
			this.xrTable10.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable10.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable10.LocationFloat = new PointFloat(0f, 0f);
			this.xrTable10.Name = "xrTable10";
			this.xrTable10.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow15
			});
			this.xrTable10.Scripts.OnBeforePrint = "xrTable7_BeforePrint";
			this.xrTable10.SizeF = new SizeF(900f, 18f);
			this.xrTable10.StylePriority.UseBorders = false;
			this.xrTable10.StylePriority.UseFont = false;
			this.xrTable10.StylePriority.UseForeColor = false;
			this.xrTable10.StylePriority.UseTextAlignment = false;
			this.xrTable10.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow15.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell36,
				this.xrTableCell37,
				this.xrTableCell38,
				this.xrTableCell40,
				this.xrTableCell43,
				this.xrTableCell44,
				this.xrTableCell45,
				this.xrTableCell46
			});
			this.xrTableRow15.Name = "xrTableRow15";
			this.xrTableRow15.Weight = 1.0;
			this.xrTableCell36.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.Name")
			});
			this.xrTableCell36.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell36.Name = "xrTableCell36";
			this.xrTableCell36.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell36.StylePriority.UseFont = false;
			this.xrTableCell36.StylePriority.UseForeColor = false;
			this.xrTableCell36.StylePriority.UsePadding = false;
			this.xrTableCell36.Weight = 1.6355553711476463;
			this.xrTableCell37.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.TotalBreakdown")
			});
			this.xrTableCell37.Name = "xrTableCell37";
			this.xrTableCell37.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell37.Scripts.OnBeforePrint = "xrTableCell37_BeforePrint";
			this.xrTableCell37.StylePriority.UseBackColor = false;
			this.xrTableCell37.StylePriority.UsePadding = false;
			this.xrTableCell37.StylePriority.UseTextAlignment = false;
			this.xrTableCell37.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell37.Weight = 0.6062221343233484;
			this.xrTableCell38.Controls.AddRange(new XRControl[]
			{
				this.xrLabel4
			});
			this.xrTableCell38.Name = "xrTableCell38";
			this.xrTableCell38.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell38.StylePriority.UsePadding = false;
			this.xrTableCell38.Weight = 1.767081793174668;
			this.xrLabel4.BackColor = Color.Blue;
			this.xrLabel4.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.TotalBreakdownTag")
			});
			this.xrLabel4.ForeColor = Color.Blue;
			this.xrLabel4.LocationFloat = new PointFloat(4.000015f, 0.9999593f);
			this.xrLabel4.Name = "xrLabel4";
			this.xrLabel4.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel4.Scripts.OnBeforePrint = "xrLabel4_BeforePrint";
			this.xrLabel4.SizeF = new SizeF(194f, 16f);
			this.xrLabel4.StylePriority.UseBackColor = false;
			this.xrLabel4.StylePriority.UseForeColor = false;
			this.xrLabel4.WordWrap = false;
			this.xrTableCell40.Name = "xrTableCell40";
			this.xrTableCell40.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell40.StylePriority.UsePadding = false;
			this.xrTableCell40.StylePriority.UseTextAlignment = false;
			this.xrTableCell40.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell40.Weight = 0.0916941268977809;
			this.xrTableCell43.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.CostTotalSubTotal")
			});
			this.xrTableCell43.Name = "xrTableCell43";
			this.xrTableCell43.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell43.StylePriority.UseFont = false;
			this.xrTableCell43.StylePriority.UsePadding = false;
			this.xrTableCell43.StylePriority.UseTextAlignment = false;
			this.xrTableCell43.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell43.Weight = 1.1074195810059249;
			this.xrTableCell44.Name = "xrTableCell44";
			this.xrTableCell44.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell44.StylePriority.UsePadding = false;
			this.xrTableCell44.StylePriority.UseTextAlignment = false;
			this.xrTableCell44.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell44.Weight = 0.9302324993991505;
			this.xrTableCell45.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.PriceTotalSubTotal")
			});
			this.xrTableCell45.Name = "xrTableCell45";
			this.xrTableCell45.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell45.StylePriority.UseFont = false;
			this.xrTableCell45.StylePriority.UsePadding = false;
			this.xrTableCell45.StylePriority.UseTextAlignment = false;
			this.xrTableCell45.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell45.Weight = 1.1074195798819029;
			this.xrTableCell46.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.MarginSubTotal")
			});
			this.xrTableCell46.FormattingRules.Add(this.formattingRule14);
			this.xrTableCell46.Name = "xrTableCell46";
			this.xrTableCell46.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell46.StylePriority.UseFont = false;
			this.xrTableCell46.StylePriority.UsePadding = false;
			this.xrTableCell46.StylePriority.UseTextAlignment = false;
			xrsummary4.FormatString = "{0:0.00%}";
			xrsummary4.Func = SummaryFunc.Avg;
			this.xrTableCell46.Summary = xrsummary4;
			this.xrTableCell46.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell46.Visible = false;
			this.xrTableCell46.Weight = 0.7543739676644249;
			this.formattingRule14.Condition = "[RawMarginSubTotal] > 0";
			this.formattingRule14.DataMember = "Summaries.Summaries_Summary";
			this.formattingRule14.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule14.Name = "formattingRule14";
			this.GroupHeader3.Controls.AddRange(new XRControl[]
			{
				this.xrTable9,
				this.lblSummaryTotals
			});
			this.GroupHeader3.HeightF = 75f;
			this.GroupHeader3.KeepTogether = true;
			this.GroupHeader3.Name = "GroupHeader3";
			this.GroupHeader3.PageBreak = PageBreak.BeforeBand;
			this.GroupHeader3.RepeatEveryPage = true;
			this.xrTable9.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline);
			this.xrTable9.ForeColor = Color.DarkRed;
			this.xrTable9.LocationFloat = new PointFloat(0f, 50.5f);
			this.xrTable9.Name = "xrTable9";
			this.xrTable9.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable9.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow14
			});
			this.xrTable9.SizeF = new SizeF(900f, 22.92f);
			this.xrTable9.StylePriority.UseFont = false;
			this.xrTable9.StylePriority.UseForeColor = false;
			this.xrTable9.StylePriority.UsePadding = false;
			this.xrTableRow14.Cells.AddRange(new XRTableCell[]
			{
				this.lblSummaryGroup,
				this.xrTableCell39,
				this.lblSummaryBreakdown,
				this.xrTableCell42,
				this.lblSummaryCostTotal,
				this.xrTableCell48,
				this.lblSummaryPriceTotal,
				this.lblSummaryMargin
			});
			this.xrTableRow14.Name = "xrTableRow14";
			this.xrTableRow14.Weight = 1.0;
			this.lblSummaryGroup.Name = "lblSummaryGroup";
			this.lblSummaryGroup.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblSummaryGroup.StylePriority.UsePadding = false;
			this.lblSummaryGroup.StylePriority.UseTextAlignment = false;
			this.lblSummaryGroup.Text = "Groupe";
			this.lblSummaryGroup.TextAlignment = TextAlignment.MiddleLeft;
			this.lblSummaryGroup.Weight = 1.7528241134162492;
			this.xrTableCell39.Name = "xrTableCell39";
			this.xrTableCell39.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell39.StylePriority.UsePadding = false;
			this.xrTableCell39.StylePriority.UseTextAlignment = false;
			this.xrTableCell39.TextAlignment = TextAlignment.MiddleCenter;
			this.xrTableCell39.Weight = 0.47028489133506407;
			this.lblSummaryBreakdown.Name = "lblSummaryBreakdown";
			this.lblSummaryBreakdown.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.lblSummaryBreakdown.StylePriority.UsePadding = false;
			this.lblSummaryBreakdown.StylePriority.UseTextAlignment = false;
			this.lblSummaryBreakdown.Text = "Répartition";
			this.lblSummaryBreakdown.TextAlignment = TextAlignment.MiddleCenter;
			this.lblSummaryBreakdown.Weight = 1.7857512794421047;
			this.xrTableCell42.Name = "xrTableCell42";
			this.xrTableCell42.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell42.StylePriority.UsePadding = false;
			this.xrTableCell42.StylePriority.UseTextAlignment = false;
			this.xrTableCell42.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell42.Weight = 0.09169374809696523;
			this.lblSummaryCostTotal.Name = "lblSummaryCostTotal";
			this.lblSummaryCostTotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblSummaryCostTotal.StylePriority.UsePadding = false;
			this.lblSummaryCostTotal.StylePriority.UseTextAlignment = false;
			this.lblSummaryCostTotal.Text = "Coûtant total";
			this.lblSummaryCostTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryCostTotal.Weight = 1.1074197777923485;
			this.xrTableCell48.Name = "xrTableCell48";
			this.xrTableCell48.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell48.StylePriority.UsePadding = false;
			this.xrTableCell48.StylePriority.UseTextAlignment = false;
			this.xrTableCell48.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell48.Weight = 0.9302326166176373;
			this.lblSummaryPriceTotal.Name = "lblSummaryPriceTotal";
			this.lblSummaryPriceTotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblSummaryPriceTotal.StylePriority.UsePadding = false;
			this.lblSummaryPriceTotal.StylePriority.UseTextAlignment = false;
			this.lblSummaryPriceTotal.Text = "Prix total";
			this.lblSummaryPriceTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryPriceTotal.Weight = 1.107419776668326;
			this.lblSummaryMargin.Name = "lblSummaryMargin";
			this.lblSummaryMargin.Padding = new PaddingInfo(1, 10, 0, 0, 100f);
			this.lblSummaryMargin.StylePriority.UsePadding = false;
			this.lblSummaryMargin.StylePriority.UseTextAlignment = false;
			this.lblSummaryMargin.Text = "Marge";
			this.lblSummaryMargin.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryMargin.Weight = 0.7543739315906786;
			this.lblSummaryTotals.BackColor = Color.Transparent;
			this.lblSummaryTotals.Font = new Font("Tahoma", 15.25f, FontStyle.Bold | FontStyle.Underline);
			this.lblSummaryTotals.ForeColor = Color.FromArgb(0, 64, 64);
			this.lblSummaryTotals.LocationFloat = new PointFloat(0f, 0f);
			this.lblSummaryTotals.Name = "lblSummaryTotals";
			this.lblSummaryTotals.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblSummaryTotals.SizeF = new SizeF(900f, 23f);
			this.lblSummaryTotals.StylePriority.UseFont = false;
			this.lblSummaryTotals.StylePriority.UseForeColor = false;
			this.lblSummaryTotals.StylePriority.UsePadding = false;
			this.lblSummaryTotals.StylePriority.UseTextAlignment = false;
			this.lblSummaryTotals.Text = "SOMMAIRE DES TOTAUX";
			this.lblSummaryTotals.TextAlignment = TextAlignment.TopCenter;
			this.GroupFooter2.Controls.AddRange(new XRControl[]
			{
				this.xrTable11
			});
			this.GroupFooter2.HeightF = 30f;
			this.GroupFooter2.Name = "GroupFooter2";
			this.GroupFooter2.SubBands.AddRange(new SubBand[]
			{
				this.SubBand6,
				this.SubBand8,
				this.SubBand9
			});
			this.xrTable11.Borders = BorderSide.None;
			this.xrTable11.BorderWidth = 3f;
			this.xrTable11.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable11.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable11.LocationFloat = new PointFloat(0f, 0f);
			this.xrTable11.Name = "xrTable11";
			this.xrTable11.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow16
			});
			this.xrTable11.SizeF = new SizeF(900f, 18f);
			this.xrTable11.StylePriority.UseBorders = false;
			this.xrTable11.StylePriority.UseBorderWidth = false;
			this.xrTable11.StylePriority.UseFont = false;
			this.xrTable11.StylePriority.UseForeColor = false;
			this.xrTable11.StylePriority.UseTextAlignment = false;
			this.xrTable11.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow16.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell51,
				this.xrTableCell52,
				this.xrTableCell53,
				this.lblSummarySubTotals,
				this.xrTableCell55,
				this.xrTableCell56,
				this.xrTableCell57,
				this.xrTableCell58
			});
			this.xrTableRow16.Name = "xrTableRow16";
			this.xrTableRow16.Weight = 1.0;
			this.xrTableCell51.Name = "xrTableCell51";
			this.xrTableCell51.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell51.StylePriority.UsePadding = false;
			this.xrTableCell51.Weight = 1.752823830580536;
			this.xrTableCell52.Name = "xrTableCell52";
			this.xrTableCell52.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell52.StylePriority.UsePadding = false;
			this.xrTableCell52.StylePriority.UseTextAlignment = false;
			this.xrTableCell52.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell52.Weight = 0.8859357060943043;
			this.xrTableCell53.Name = "xrTableCell53";
			this.xrTableCell53.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell53.StylePriority.UsePadding = false;
			this.xrTableCell53.Weight = 0.5315614220100788;
			this.lblSummarySubTotals.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummarySubTotals.Name = "lblSummarySubTotals";
			this.lblSummarySubTotals.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummarySubTotals.StylePriority.UseFont = false;
			this.lblSummarySubTotals.StylePriority.UsePadding = false;
			this.lblSummarySubTotals.StylePriority.UseTextAlignment = false;
			this.lblSummarySubTotals.Text = "Sous-totaux :";
			this.lblSummarySubTotals.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummarySubTotals.Weight = 0.9302324668585246;
			this.xrTableCell55.Borders = BorderSide.None;
			this.xrTableCell55.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.CostTotal")
			});
			this.xrTableCell55.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell55.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell55.Name = "xrTableCell55";
			this.xrTableCell55.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell55.StylePriority.UseBorders = false;
			this.xrTableCell55.StylePriority.UseFont = false;
			this.xrTableCell55.StylePriority.UseForeColor = false;
			this.xrTableCell55.StylePriority.UsePadding = false;
			this.xrTableCell55.StylePriority.UseTextAlignment = false;
			this.xrTableCell55.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell55.Weight = 1.107419581005925;
			this.xrTableCell56.Name = "xrTableCell56";
			this.xrTableCell56.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell56.StylePriority.UsePadding = false;
			this.xrTableCell56.StylePriority.UseTextAlignment = false;
			this.xrTableCell56.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell56.Weight = 0.9302324993991369;
			this.xrTableCell57.Borders = BorderSide.None;
			this.xrTableCell57.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.PriceTotal")
			});
			this.xrTableCell57.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell57.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell57.Name = "xrTableCell57";
			this.xrTableCell57.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell57.StylePriority.UseBorders = false;
			this.xrTableCell57.StylePriority.UseFont = false;
			this.xrTableCell57.StylePriority.UseForeColor = false;
			this.xrTableCell57.StylePriority.UsePadding = false;
			this.xrTableCell57.StylePriority.UseTextAlignment = false;
			xrsummary5.FormatString = "{0:c}";
			this.xrTableCell57.Summary = xrsummary5;
			this.xrTableCell57.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell57.Weight = 1.1074195798819029;
			this.xrTableCell58.Borders = BorderSide.None;
			this.xrTableCell58.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.MarginTotal")
			});
			this.xrTableCell58.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell58.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell58.FormattingRules.Add(this.formattingRule15);
			this.xrTableCell58.Name = "xrTableCell58";
			this.xrTableCell58.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell58.StylePriority.UseBorders = false;
			this.xrTableCell58.StylePriority.UseFont = false;
			this.xrTableCell58.StylePriority.UseForeColor = false;
			this.xrTableCell58.StylePriority.UsePadding = false;
			this.xrTableCell58.StylePriority.UseTextAlignment = false;
			xrsummary6.Func = SummaryFunc.Custom;
			this.xrTableCell58.Summary = xrsummary6;
			this.xrTableCell58.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell58.Visible = false;
			this.xrTableCell58.Weight = 0.7543739676644248;
			this.formattingRule15.Condition = "[MarginTotal] > 0";
			this.formattingRule15.DataMember = "Summaries.Summaries_Total";
			this.formattingRule15.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule15.Name = "formattingRule15";
			this.SubBand6.Controls.AddRange(new XRControl[]
			{
				this.xrTable12
			});
			this.SubBand6.HeightF = 23f;
			this.SubBand6.Name = "SubBand6";
			this.xrTable12.Borders = BorderSide.None;
			this.xrTable12.BorderWidth = 3f;
			this.xrTable12.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable12.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable12.LocationFloat = new PointFloat(0f, 1f);
			this.xrTable12.Name = "xrTable12";
			this.xrTable12.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow17
			});
			this.xrTable12.SizeF = new SizeF(900f, 20f);
			this.xrTable12.StylePriority.UseBorders = false;
			this.xrTable12.StylePriority.UseBorderWidth = false;
			this.xrTable12.StylePriority.UseFont = false;
			this.xrTable12.StylePriority.UseForeColor = false;
			this.xrTable12.StylePriority.UseTextAlignment = false;
			this.xrTable12.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow17.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell59,
				this.xrTableCell60,
				this.xrTableCell61,
				this.xrTableCell62,
				this.xrTableCell63,
				this.lblSummaryTax1,
				this.xrTableCell65,
				this.xrTableCell66
			});
			this.xrTableRow17.Name = "xrTableRow17";
			this.xrTableRow17.Weight = 1.0;
			this.xrTableCell59.Name = "xrTableCell59";
			this.xrTableCell59.Padding = new PaddingInfo(10, 2, 3, 3, 100f);
			this.xrTableCell59.StylePriority.UsePadding = false;
			this.xrTableCell59.Weight = 1.752823830580536;
			this.xrTableCell60.Name = "xrTableCell60";
			this.xrTableCell60.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell60.StylePriority.UsePadding = false;
			this.xrTableCell60.StylePriority.UseTextAlignment = false;
			this.xrTableCell60.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell60.Weight = 0.8859357060943043;
			this.xrTableCell61.Name = "xrTableCell61";
			this.xrTableCell61.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell61.StylePriority.UsePadding = false;
			this.xrTableCell61.Weight = 0.5315614220100788;
			this.xrTableCell62.Name = "xrTableCell62";
			this.xrTableCell62.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell62.StylePriority.UsePadding = false;
			this.xrTableCell62.StylePriority.UseTextAlignment = false;
			this.xrTableCell62.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell62.Weight = 0.9302324668585246;
			this.xrTableCell63.Borders = BorderSide.None;
			this.xrTableCell63.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell63.ForeColor = Color.DarkRed;
			this.xrTableCell63.Name = "xrTableCell63";
			this.xrTableCell63.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell63.StylePriority.UseBorders = false;
			this.xrTableCell63.StylePriority.UseFont = false;
			this.xrTableCell63.StylePriority.UseForeColor = false;
			this.xrTableCell63.StylePriority.UsePadding = false;
			this.xrTableCell63.StylePriority.UseTextAlignment = false;
			this.xrTableCell63.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell63.Weight = 0.08859417476498987;
			this.lblSummaryTax1.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Total.Taxe1Caption")
			});
			this.lblSummaryTax1.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummaryTax1.Name = "lblSummaryTax1";
			this.lblSummaryTax1.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummaryTax1.StylePriority.UseFont = false;
			this.lblSummaryTax1.StylePriority.UsePadding = false;
			this.lblSummaryTax1.StylePriority.UseTextAlignment = false;
			this.lblSummaryTax1.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryTax1.Weight = 1.949057905640072;
			this.xrTableCell65.Borders = BorderSide.None;
			this.xrTableCell65.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe1Total")
			});
			this.xrTableCell65.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell65.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell65.Name = "xrTableCell65";
			this.xrTableCell65.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell65.StylePriority.UseBorders = false;
			this.xrTableCell65.StylePriority.UseFont = false;
			this.xrTableCell65.StylePriority.UseForeColor = false;
			this.xrTableCell65.StylePriority.UsePadding = false;
			this.xrTableCell65.StylePriority.UseTextAlignment = false;
			xrsummary7.FormatString = "{0:c}";
			this.xrTableCell65.Summary = xrsummary7;
			this.xrTableCell65.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell65.Weight = 1.1074195798819029;
			this.xrTableCell66.Borders = BorderSide.None;
			this.xrTableCell66.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell66.ForeColor = Color.DarkRed;
			this.xrTableCell66.Name = "xrTableCell66";
			this.xrTableCell66.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell66.StylePriority.UseBorders = false;
			this.xrTableCell66.StylePriority.UseFont = false;
			this.xrTableCell66.StylePriority.UseForeColor = false;
			this.xrTableCell66.StylePriority.UsePadding = false;
			this.xrTableCell66.StylePriority.UseTextAlignment = false;
			xrsummary8.Func = SummaryFunc.Custom;
			this.xrTableCell66.Summary = xrsummary8;
			this.xrTableCell66.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell66.Visible = false;
			this.xrTableCell66.Weight = 0.7543739676644248;
			this.SubBand8.Controls.AddRange(new XRControl[]
			{
				this.xrTable13
			});
			this.SubBand8.HeightF = 23f;
			this.SubBand8.Name = "SubBand8";
			this.xrTable13.Borders = BorderSide.None;
			this.xrTable13.BorderWidth = 3f;
			this.xrTable13.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable13.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable13.LocationFloat = new PointFloat(0f, 1f);
			this.xrTable13.Name = "xrTable13";
			this.xrTable13.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow18
			});
			this.xrTable13.SizeF = new SizeF(900f, 20f);
			this.xrTable13.StylePriority.UseBorders = false;
			this.xrTable13.StylePriority.UseBorderWidth = false;
			this.xrTable13.StylePriority.UseFont = false;
			this.xrTable13.StylePriority.UseForeColor = false;
			this.xrTable13.StylePriority.UseTextAlignment = false;
			this.xrTable13.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow18.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell67,
				this.xrTableCell68,
				this.xrTableCell69,
				this.xrTableCell70,
				this.xrTableCell71,
				this.lblSummaryTax2,
				this.xrTableCell73,
				this.xrTableCell74
			});
			this.xrTableRow18.Name = "xrTableRow18";
			this.xrTableRow18.Weight = 1.0;
			this.xrTableCell67.Name = "xrTableCell67";
			this.xrTableCell67.Padding = new PaddingInfo(10, 2, 3, 3, 100f);
			this.xrTableCell67.StylePriority.UsePadding = false;
			this.xrTableCell67.Weight = 1.752823830580536;
			this.xrTableCell68.Name = "xrTableCell68";
			this.xrTableCell68.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell68.StylePriority.UsePadding = false;
			this.xrTableCell68.StylePriority.UseTextAlignment = false;
			this.xrTableCell68.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell68.Weight = 0.8859357060943043;
			this.xrTableCell69.Name = "xrTableCell69";
			this.xrTableCell69.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell69.StylePriority.UsePadding = false;
			this.xrTableCell69.Weight = 0.5315614220100788;
			this.xrTableCell70.Name = "xrTableCell70";
			this.xrTableCell70.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell70.StylePriority.UsePadding = false;
			this.xrTableCell70.StylePriority.UseTextAlignment = false;
			this.xrTableCell70.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell70.Weight = 0.9302324668585246;
			this.xrTableCell71.Borders = BorderSide.None;
			this.xrTableCell71.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell71.ForeColor = Color.DarkRed;
			this.xrTableCell71.Name = "xrTableCell71";
			this.xrTableCell71.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell71.StylePriority.UseBorders = false;
			this.xrTableCell71.StylePriority.UseFont = false;
			this.xrTableCell71.StylePriority.UseForeColor = false;
			this.xrTableCell71.StylePriority.UsePadding = false;
			this.xrTableCell71.StylePriority.UseTextAlignment = false;
			this.xrTableCell71.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell71.Weight = 0.07973393942674822;
			this.lblSummaryTax2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Total.Taxe2Caption")
			});
			this.lblSummaryTax2.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummaryTax2.Name = "lblSummaryTax2";
			this.lblSummaryTax2.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummaryTax2.StylePriority.UseFont = false;
			this.lblSummaryTax2.StylePriority.UsePadding = false;
			this.lblSummaryTax2.StylePriority.UseTextAlignment = false;
			this.lblSummaryTax2.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryTax2.Weight = 1.9579181409783137;
			this.xrTableCell73.Borders = BorderSide.None;
			this.xrTableCell73.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe2Total")
			});
			this.xrTableCell73.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell73.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell73.Name = "xrTableCell73";
			this.xrTableCell73.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell73.StylePriority.UseBorders = false;
			this.xrTableCell73.StylePriority.UseFont = false;
			this.xrTableCell73.StylePriority.UseForeColor = false;
			this.xrTableCell73.StylePriority.UsePadding = false;
			this.xrTableCell73.StylePriority.UseTextAlignment = false;
			xrsummary9.FormatString = "{0:c}";
			this.xrTableCell73.Summary = xrsummary9;
			this.xrTableCell73.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell73.Weight = 1.1074195798819029;
			this.xrTableCell74.Borders = BorderSide.None;
			this.xrTableCell74.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell74.ForeColor = Color.DarkRed;
			this.xrTableCell74.Name = "xrTableCell74";
			this.xrTableCell74.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell74.StylePriority.UseBorders = false;
			this.xrTableCell74.StylePriority.UseFont = false;
			this.xrTableCell74.StylePriority.UseForeColor = false;
			this.xrTableCell74.StylePriority.UsePadding = false;
			this.xrTableCell74.StylePriority.UseTextAlignment = false;
			xrsummary10.Func = SummaryFunc.Custom;
			this.xrTableCell74.Summary = xrsummary10;
			this.xrTableCell74.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell74.Visible = false;
			this.xrTableCell74.Weight = 0.7543739676644248;
			this.SubBand9.Controls.AddRange(new XRControl[]
			{
				this.xrTable14
			});
			this.SubBand9.HeightF = 23f;
			this.SubBand9.Name = "SubBand9";
			this.xrTable14.Borders = BorderSide.None;
			this.xrTable14.BorderWidth = 3f;
			this.xrTable14.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable14.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable14.LocationFloat = new PointFloat(0f, 1f);
			this.xrTable14.Name = "xrTable14";
			this.xrTable14.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow19
			});
			this.xrTable14.SizeF = new SizeF(900f, 20f);
			this.xrTable14.StylePriority.UseBorders = false;
			this.xrTable14.StylePriority.UseBorderWidth = false;
			this.xrTable14.StylePriority.UseFont = false;
			this.xrTable14.StylePriority.UseForeColor = false;
			this.xrTable14.StylePriority.UseTextAlignment = false;
			this.xrTable14.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow19.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell75,
				this.xrTableCell76,
				this.xrTableCell77,
				this.xrTableCell78,
				this.xrTableCell79,
				this.lblSummaryGrandTotal,
				this.xrTableCell81,
				this.xrTableCell82
			});
			this.xrTableRow19.Name = "xrTableRow19";
			this.xrTableRow19.Weight = 1.0;
			this.xrTableCell75.Name = "xrTableCell75";
			this.xrTableCell75.Padding = new PaddingInfo(10, 2, 3, 3, 100f);
			this.xrTableCell75.StylePriority.UsePadding = false;
			this.xrTableCell75.Weight = 1.752823830580536;
			this.xrTableCell76.Name = "xrTableCell76";
			this.xrTableCell76.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell76.StylePriority.UsePadding = false;
			this.xrTableCell76.StylePriority.UseTextAlignment = false;
			this.xrTableCell76.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell76.Weight = 0.8859357060943043;
			this.xrTableCell77.Name = "xrTableCell77";
			this.xrTableCell77.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell77.StylePriority.UsePadding = false;
			this.xrTableCell77.Weight = 0.5315614220100788;
			this.xrTableCell78.Name = "xrTableCell78";
			this.xrTableCell78.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell78.StylePriority.UsePadding = false;
			this.xrTableCell78.StylePriority.UseTextAlignment = false;
			this.xrTableCell78.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell78.Weight = 0.9302324668585246;
			this.xrTableCell79.Borders = BorderSide.None;
			this.xrTableCell79.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell79.ForeColor = Color.DarkRed;
			this.xrTableCell79.Name = "xrTableCell79";
			this.xrTableCell79.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell79.StylePriority.UseBorders = false;
			this.xrTableCell79.StylePriority.UseFont = false;
			this.xrTableCell79.StylePriority.UseForeColor = false;
			this.xrTableCell79.StylePriority.UsePadding = false;
			this.xrTableCell79.StylePriority.UseTextAlignment = false;
			this.xrTableCell79.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell79.Weight = 0.07973420979285684;
			this.lblSummaryGrandTotal.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.lblSummaryGrandTotal.Name = "lblSummaryGrandTotal";
			this.lblSummaryGrandTotal.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummaryGrandTotal.StylePriority.UseFont = false;
			this.lblSummaryGrandTotal.StylePriority.UsePadding = false;
			this.lblSummaryGrandTotal.StylePriority.UseTextAlignment = false;
			this.lblSummaryGrandTotal.Text = "GRAND TOTAL :";
			this.lblSummaryGrandTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryGrandTotal.Weight = 1.9579178706122051;
			this.xrTableCell81.Borders = BorderSide.None;
			this.xrTableCell81.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.TotalAfterTaxes", "{0:#.00}")
			});
			this.xrTableCell81.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell81.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell81.Name = "xrTableCell81";
			this.xrTableCell81.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell81.StylePriority.UseBorders = false;
			this.xrTableCell81.StylePriority.UseFont = false;
			this.xrTableCell81.StylePriority.UseForeColor = false;
			this.xrTableCell81.StylePriority.UsePadding = false;
			this.xrTableCell81.StylePriority.UseTextAlignment = false;
			xrsummary11.FormatString = "{0:c}";
			this.xrTableCell81.Summary = xrsummary11;
			this.xrTableCell81.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell81.Weight = 1.1074195798819029;
			this.xrTableCell82.Borders = BorderSide.None;
			this.xrTableCell82.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell82.ForeColor = Color.DarkRed;
			this.xrTableCell82.Name = "xrTableCell82";
			this.xrTableCell82.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell82.StylePriority.UseBorders = false;
			this.xrTableCell82.StylePriority.UseFont = false;
			this.xrTableCell82.StylePriority.UseForeColor = false;
			this.xrTableCell82.StylePriority.UsePadding = false;
			this.xrTableCell82.StylePriority.UseTextAlignment = false;
			xrsummary12.Func = SummaryFunc.Custom;
			this.xrTableCell82.Summary = xrsummary12;
			this.xrTableCell82.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell82.Visible = false;
			this.xrTableCell82.Weight = 0.7543739676644248;
			this.formattingRule2.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule2.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule2.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule2.Name = "formattingRule2";
			this.formattingRule1.Condition = "[BaseExtension] ==True Or IsNullOrEmpty([Extension_Results]) Or IsNullOrEmpty([Extension_Results.Results_Result])";
			this.formattingRule1.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule1.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule1.Name = "formattingRule1";
			this.formattingRule3.Condition = "IsNullOrEmpty([Extension_Fields.Extension_Id])";
			this.formattingRule3.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule3.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule3.Name = "formattingRule3";
			this.SubBand7.HeightF = 3.125f;
			this.SubBand7.Name = "SubBand7";
			this.lblReportTitle.BackColor = Color.Transparent;
			this.lblReportTitle.Font = new Font("Tahoma", 15.25f, FontStyle.Bold | FontStyle.Underline);
			this.lblReportTitle.ForeColor = Color.FromArgb(0, 64, 64);
			this.lblReportTitle.LocationFloat = new PointFloat(0.0001271566f, 0f);
			this.lblReportTitle.Name = "lblReportTitle";
			this.lblReportTitle.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblReportTitle.SizeF = new SizeF(899.9997f, 23f);
			this.lblReportTitle.StylePriority.UseFont = false;
			this.lblReportTitle.StylePriority.UseForeColor = false;
			this.lblReportTitle.StylePriority.UsePadding = false;
			this.lblReportTitle.StylePriority.UseTextAlignment = false;
			this.lblReportTitle.Text = "RAPPORT D'ESTIMATION (PAR PLANS)";
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
			this.formattingRule10.Condition = "[BaseExtension] == true";
			this.formattingRule10.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule10.Name = "formattingRule10";
			this.formattingRule11.Condition = "IsNullOrEmpty([BaseExtension])";
			this.formattingRule11.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule11.Name = "formattingRule11";
			this.formattingRule19.Condition = "[BaseExtension] ==True Or IsNullOrEmpty([Extension_Results]) Or IsNullOrEmpty([Extension_Results.Results_Result])";
			this.formattingRule19.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions";
			this.formattingRule19.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule19.Name = "formattingRule19";
			this.xrTable1.BorderColor = Color.DarkSlateGray;
			this.xrTable1.Borders = BorderSide.None;
			this.xrTable1.BorderWidth = 2f;
			this.xrTable1.LocationFloat = new PointFloat(125.0001f, 0f);
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
			this.xrTable2.LocationFloat = new PointFloat(465f, 0f);
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
			this.xrTable6.LocationFloat = new PointFloat(125f, 0f);
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
				this.formattingRule13,
				this.formattingRule14,
				this.formattingRule15,
				this.formattingRule16,
				this.formattingRule17,
				this.formattingRule18,
				this.formattingRule19,
				this.formattingRule20
			});
			base.Landscape = true;
			base.PageHeight = 850;
			base.PageWidth = 1100;
			base.ScriptsSource = componentResourceManager.GetString("$this.ScriptsSource");
			base.Version = "14.2";
			this.XmlDataPath = "C:\\Users\\Patrick\\Documents\\Quoter Plan\\Mes rapports\\LE-PACIFIQUE-(11-Novembre-2014)-[Classé_par_plans].xml";
			((ISupportInitialize)this.xrTable7).EndInit();
			((ISupportInitialize)this.xrTable3).EndInit();
			((ISupportInitialize)this.xrTable8).EndInit();
			((ISupportInitialize)this.xrTable10).EndInit();
			((ISupportInitialize)this.xrTable9).EndInit();
			((ISupportInitialize)this.xrTable11).EndInit();
			((ISupportInitialize)this.xrTable12).EndInit();
			((ISupportInitialize)this.xrTable13).EndInit();
			((ISupportInitialize)this.xrTable14).EndInit();
			((ISupportInitialize)this.xrTable1).EndInit();
			((ISupportInitialize)this.xrTable2).EndInit();
			((ISupportInitialize)this.xrTable6).EndInit();
			((ISupportInitialize)this).EndInit();
		}

		private void LoadResources()
		{
			this.lblHeader1.Text = Resources.Rapport_d_estimation;
			this.lblHeader2.Text = ((this.project.Report.ReportSortBy == QuoterPlan.Report.ReportSortByEnum.ReportSortByPlans) ? Resources._par_plans_ : Resources._par_calques_);
			this.lbHeaderPageInfo.Format = Resources.Page_0_de_1_;
			this.lblProjectInfo.Text = Resources.Projet_;
			this.lblContactInfo.Text = Resources.Client_;
			this.lblComments.Text = Resources.Commentaires__;
			this.lblReportTitle.Text = ((this.project.Report.ReportSortBy == QuoterPlan.Report.ReportSortByEnum.ReportSortByPlans) ? Resources.RAPPORT_D_ESTIMATION__PAR_PLANS_ : Resources.RAPPORT_D_ESTIMATION__PAR_CALQUES_);
			this.lblFooter1.Text = Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan;
			this.lblFooter2.Text = Utilities.ApplicationWebsite;
			this.lbFooterlPageInfo.Format = Resources.Page_0_de_1_;
			this.lblItem.Text = Resources.Item;
			this.lblQuantity.Text = Resources.Quantité;
			this.lblUnit.Text = Resources.Unité;
			this.lblCost.Text = Resources.Coûtant;
			this.lblCostTotal.Text = Resources.Coûtant_total;
			this.lblPrice.Text = Resources.Prix;
			this.lblPriceTotal.Text = Resources.Prix_total;
			this.lblMargin.Text = Resources.Marge;
		}

		public XtraReportEstimatingByPlans(Project project)
		{
			this.project = project;
			this.InitializeComponent();
			this.LoadResources();
		}

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

		private FormattingRule formattingRule9;

		private SubBand SubBand7;

		private DetailReportBand DetailReport8;

		private DetailBand Detail9;

		private DetailReportBand DetailReport11;

		private DetailBand Detail12;

		private XRLabel xrLabel24;

		private DetailReportBand DetailReport12;

		private DetailBand Detail13;

		private DetailReportBand DetailReport13;

		private DetailBand Detail14;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel xrLabel5;

		private XRLine xrLine2;

		private XRLabel xrLabel6;

		private XRLabel lbFooterDate;

		private GroupHeaderBand GroupHeader4;

		private XRTable xrTable3;

		private XRTableRow xrTableRow5;

		private XRTableCell lblItem;

		private XRTableCell lblQuantity;

		private XRTableCell lblUnit;

		private XRTableCell lblCost;

		private XRTableCell lblCostTotal;

		private XRTableCell lblPrice;

		private XRTableCell lblPriceTotal;

		private XRTableCell lblMargin;

		private XRTable xrTable7;

		private XRTableRow xrTableRow6;

		private XRTableCell xrTableCell19;

		private XRTableCell xrTableCell20;

		private XRTableCell xrTableCell21;

		private XRTableCell xrTableCell22;

		private XRTableCell xrTableCell23;

		private XRTableCell xrTableCell24;

		private XRTableCell xrTableCell25;

		private XRTableCell xrTableCell26;

		private GroupFooterBand GroupFooter1;

		private XRTable xrTable8;

		private XRTableRow xrTableRow13;

		private XRTableCell xrTableCell27;

		private XRTableCell xrTableCell28;

		private XRTableCell xrTableCell29;

		private XRTableCell xrTableCell30;

		private XRTableCell xrTableCell31;

		private XRTableCell xrTableCell32;

		private XRTableCell xrTableCell33;

		private XRTableCell xrTableCell34;

		private FormattingRule formattingRule10;

		private FormattingRule formattingRule11;

		private FormattingRule formattingRule12;

		private FormattingRule formattingRule13;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel7;

		private SubBand SubBand6;

		private SubBand SubBand8;

		private SubBand SubBand9;

		private XRTable xrTable12;

		private XRTableRow xrTableRow17;

		private XRTableCell xrTableCell59;

		private XRTableCell xrTableCell60;

		private XRTableCell xrTableCell61;

		private XRTableCell xrTableCell62;

		private XRTableCell xrTableCell63;

		private XRTableCell lblSummaryTax1;

		private XRTableCell xrTableCell65;

		private XRTableCell xrTableCell66;

		private XRTable xrTable13;

		private XRTableRow xrTableRow18;

		private XRTableCell xrTableCell67;

		private XRTableCell xrTableCell68;

		private XRTableCell xrTableCell69;

		private XRTableCell xrTableCell70;

		private XRTableCell xrTableCell71;

		private XRTableCell lblSummaryTax2;

		private XRTableCell xrTableCell73;

		private XRTableCell xrTableCell74;

		private XRTable xrTable14;

		private XRTableRow xrTableRow19;

		private XRTableCell xrTableCell75;

		private XRTableCell xrTableCell76;

		private XRTableCell xrTableCell77;

		private XRTableCell xrTableCell78;

		private XRTableCell xrTableCell79;

		private XRTableCell lblSummaryGrandTotal;

		private XRTableCell xrTableCell81;

		private XRTableCell xrTableCell82;

		private FormattingRule formattingRule14;

		private FormattingRule formattingRule15;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRLine xrLine1;

		private XRTable xrTable10;

		private XRTableRow xrTableRow15;

		private XRTableCell xrTableCell36;

		private XRTableCell xrTableCell37;

		private XRTableCell xrTableCell38;

		private XRLabel xrLabel4;

		private XRTableCell xrTableCell40;

		private XRTableCell xrTableCell43;

		private XRTableCell xrTableCell44;

		private XRTableCell xrTableCell45;

		private XRTableCell xrTableCell46;

		private GroupHeaderBand GroupHeader3;

		private XRTable xrTable9;

		private XRTableRow xrTableRow14;

		private XRTableCell lblSummaryGroup;

		private XRTableCell xrTableCell39;

		private XRTableCell lblSummaryBreakdown;

		private XRTableCell xrTableCell42;

		private XRTableCell lblSummaryCostTotal;

		private XRTableCell xrTableCell48;

		private XRTableCell lblSummaryPriceTotal;

		private XRTableCell lblSummaryMargin;

		private XRLabel lblSummaryTotals;

		private GroupFooterBand GroupFooter2;

		private XRTable xrTable11;

		private XRTableRow xrTableRow16;

		private XRTableCell xrTableCell51;

		private XRTableCell xrTableCell52;

		private XRTableCell xrTableCell53;

		private XRTableCell lblSummarySubTotals;

		private XRTableCell xrTableCell55;

		private XRTableCell xrTableCell56;

		private XRTableCell xrTableCell57;

		private XRTableCell xrTableCell58;

		private XRLabel xrLabel21;

		private FormattingRule formattingRule16;

		private FormattingRule formattingRule17;

		private GroupHeaderBand GroupHeader5;

		private GroupHeaderBand GroupHeader6;

		private XRLabel xrLabel1;

		private XRLabel xrLabel3;

		private FormattingRule formattingRule18;

		private SubBand SubBand11;

		private GroupHeaderBand GroupHeader7;

		private XRLabel xrLabel8;

		private FormattingRule formattingRule19;

		private FormattingRule formattingRule20;

		private XRTable xrTable2;

		private XRTableRow xrTableRow3;

		private XRTableCell lblContactInfo;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell1;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell4;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell lblProjectInfo;

		private XRTableRow xrTableRow7;

		private XRTableCell xrTableCell2;

		private XRTableRow xrTableRow9;

		private XRTableCell xrTableCell3;

		private XRTable xrTable6;

		private XRTableRow xrTableRow11;

		private XRTableCell lblComments;

		private XRTableRow xrTableRow12;

		private XRTableCell xrTableCell12;

		private Project project;
	}
}
