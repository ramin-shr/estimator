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
	public class XtraReportEstimatingTotalsByPlans : XtraReport
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
			XRSummary xrsummary13 = new XRSummary();
			XRSummary xrsummary14 = new XRSummary();
			XRSummary xrsummary15 = new XRSummary();
			XRSummary xrsummary16 = new XRSummary();
			XRSummary xrsummary17 = new XRSummary();
			XRSummary xrsummary18 = new XRSummary();
			XRSummary xrsummary19 = new XRSummary();
			XRSummary xrsummary20 = new XRSummary();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XtraReportEstimatingTotalsByPlans));
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
			this.SubBand1 = new SubBand();
			this.xrTable2 = new XRTable();
			this.xrTableRow3 = new XRTableRow();
			this.xrTableCell3 = new XRTableCell();
			this.xrTableRow4 = new XRTableRow();
			this.xrTableCell4 = new XRTableCell();
			this.xrTable1 = new XRTable();
			this.xrTableRow1 = new XRTableRow();
			this.xrTableCell1 = new XRTableCell();
			this.xrTableRow2 = new XRTableRow();
			this.xrTableCell2 = new XRTableCell();
			this.formattingRule4 = new FormattingRule();
			this.SubBand2 = new SubBand();
			this.xrTable4 = new XRTable();
			this.xrTableRow7 = new XRTableRow();
			this.xrTableCell7 = new XRTableCell();
			this.xrTableRow8 = new XRTableRow();
			this.xrTableCell8 = new XRTableCell();
			this.formattingRule6 = new FormattingRule();
			this.SubBand3 = new SubBand();
			this.xrTable5 = new XRTable();
			this.xrTableRow9 = new XRTableRow();
			this.xrTableCell9 = new XRTableCell();
			this.xrTableRow10 = new XRTableRow();
			this.xrTableCell10 = new XRTableCell();
			this.formattingRule7 = new FormattingRule();
			this.SubBand4 = new SubBand();
			this.xrTable6 = new XRTable();
			this.xrTableRow11 = new XRTableRow();
			this.xrTableCell11 = new XRTableCell();
			this.xrTableRow12 = new XRTableRow();
			this.xrTableCell12 = new XRTableCell();
			this.formattingRule8 = new FormattingRule();
			this.SubBand5 = new SubBand();
			this.xrLabel20 = new XRLabel();
			this.DetailReport2 = new DetailReportBand();
			this.Detail3 = new DetailBand();
			this.lblSummaryByPlans = new XRLabel();
			this.DetailReport3 = new DetailReportBand();
			this.Detail4 = new DetailBand();
			this.DetailReport5 = new DetailReportBand();
			this.Detail6 = new DetailBand();
			this.DetailReport6 = new DetailReportBand();
			this.Detail7 = new DetailBand();
			this.xrLine3 = new XRLine();
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
			this.formattingRule16 = new FormattingRule();
			this.GroupHeader2 = new GroupHeaderBand();
			this.xrTable9 = new XRTable();
			this.xrTableRow14 = new XRTableRow();
			this.lblPlanSummaryGroup = new XRTableCell();
			this.xrTableCell39 = new XRTableCell();
			this.lblPlanSummaryBreakdown = new XRTableCell();
			this.xrTableCell42 = new XRTableCell();
			this.lblPlanSummaryCostTotal = new XRTableCell();
			this.xrTableCell48 = new XRTableCell();
			this.lblPlanSummaryPriceTotal = new XRTableCell();
			this.lblPlanSummaryMargin = new XRTableCell();
			this.xrLabel5 = new XRLabel();
			this.ReportFooter = new ReportFooterBand();
			this.xrTable11 = new XRTable();
			this.xrTableRow16 = new XRTableRow();
			this.xrTableCell51 = new XRTableCell();
			this.xrTableCell52 = new XRTableCell();
			this.xrTableCell53 = new XRTableCell();
			this.lblPlanSummarySubTotals = new XRTableCell();
			this.xrTableCell55 = new XRTableCell();
			this.xrTableCell56 = new XRTableCell();
			this.xrTableCell57 = new XRTableCell();
			this.xrTableCell58 = new XRTableCell();
			this.formattingRule15 = new FormattingRule();
			this.DetailReport1 = new DetailReportBand();
			this.Detail2 = new DetailBand();
			this.xrTable18 = new XRTable();
			this.xrTableRow23 = new XRTableRow();
			this.xrTableCell107 = new XRTableCell();
			this.xrTableCell108 = new XRTableCell();
			this.xrTableCell109 = new XRTableCell();
			this.lblPlanSummaryTotals = new XRTableCell();
			this.xrTableCell111 = new XRTableCell();
			this.xrTableCell112 = new XRTableCell();
			this.xrTableCell113 = new XRTableCell();
			this.xrTableCell114 = new XRTableCell();
			this.formattingRule17 = new FormattingRule();
			this.xrLabel14 = new XRLabel();
			this.formattingRule14 = new FormattingRule();
			this.SubBand6 = new SubBand();
			this.xrTable12 = new XRTable();
			this.xrTableRow17 = new XRTableRow();
			this.xrTableCell59 = new XRTableCell();
			this.xrTableCell60 = new XRTableCell();
			this.xrTableCell61 = new XRTableCell();
			this.xrTableCell62 = new XRTableCell();
			this.xrTableCell63 = new XRTableCell();
			this.xrTableCell64 = new XRTableCell();
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
			this.xrTableCell72 = new XRTableCell();
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
			this.xrTableCell80 = new XRTableCell();
			this.xrTableCell81 = new XRTableCell();
			this.xrTableCell82 = new XRTableCell();
			this.formattingRule12 = new FormattingRule();
			this.formattingRule9 = new FormattingRule();
			this.formattingRule13 = new FormattingRule();
			this.formattingRule2 = new FormattingRule();
			this.formattingRule1 = new FormattingRule();
			this.formattingRule3 = new FormattingRule();
			this.SubBand7 = new SubBand();
			this.formattingRule5 = new FormattingRule();
			this.formattingRule10 = new FormattingRule();
			this.formattingRule11 = new FormattingRule();
			this.DetailReport = new DetailReportBand();
			this.Detail1 = new DetailBand();
			this.xrLine2 = new XRLine();
			this.xrTable7 = new XRTable();
			this.xrTableRow6 = new XRTableRow();
			this.xrTableCell19 = new XRTableCell();
			this.xrTableCell20 = new XRTableCell();
			this.xrTableCell21 = new XRTableCell();
			this.xrLabel2 = new XRLabel();
			this.xrTableCell22 = new XRTableCell();
			this.xrTableCell23 = new XRTableCell();
			this.xrTableCell24 = new XRTableCell();
			this.xrTableCell25 = new XRTableCell();
			this.xrTableCell26 = new XRTableCell();
			this.GroupHeader1 = new GroupHeaderBand();
			this.lblSummaryTotals = new XRLabel();
			this.xrTable3 = new XRTable();
			this.xrTableRow5 = new XRTableRow();
			this.lblSummaryItem = new XRTableCell();
			this.xrTableCell6 = new XRTableCell();
			this.lblSummaryBreakdown = new XRTableCell();
			this.xrTableCell14 = new XRTableCell();
			this.lblSummaryCostTotal = new XRTableCell();
			this.xrTableCell16 = new XRTableCell();
			this.lblSummaryPriceTotal = new XRTableCell();
			this.lblSummaryMargin = new XRTableCell();
			this.GroupFooter1 = new GroupFooterBand();
			this.xrTable8 = new XRTable();
			this.xrTableRow13 = new XRTableRow();
			this.xrTableCell27 = new XRTableCell();
			this.xrTableCell28 = new XRTableCell();
			this.xrTableCell29 = new XRTableCell();
			this.lblSummarySubTotals = new XRTableCell();
			this.xrTableCell31 = new XRTableCell();
			this.xrTableCell32 = new XRTableCell();
			this.xrTableCell33 = new XRTableCell();
			this.xrTableCell34 = new XRTableCell();
			this.formattingRule18 = new FormattingRule();
			this.SubBand10 = new SubBand();
			this.xrTable15 = new XRTable();
			this.xrTableRow20 = new XRTableRow();
			this.xrTableCell83 = new XRTableCell();
			this.xrTableCell84 = new XRTableCell();
			this.xrTableCell85 = new XRTableCell();
			this.xrTableCell86 = new XRTableCell();
			this.xrTableCell87 = new XRTableCell();
			this.lblSummaryTax1 = new XRTableCell();
			this.xrTableCell89 = new XRTableCell();
			this.xrTableCell90 = new XRTableCell();
			this.SubBand11 = new SubBand();
			this.xrTable16 = new XRTable();
			this.xrTableRow21 = new XRTableRow();
			this.xrTableCell91 = new XRTableCell();
			this.xrTableCell92 = new XRTableCell();
			this.xrTableCell93 = new XRTableCell();
			this.xrTableCell94 = new XRTableCell();
			this.xrTableCell95 = new XRTableCell();
			this.lblSummaryTax2 = new XRTableCell();
			this.xrTableCell97 = new XRTableCell();
			this.xrTableCell98 = new XRTableCell();
			this.SubBand12 = new SubBand();
			this.xrTable17 = new XRTable();
			this.xrTableRow22 = new XRTableRow();
			this.xrTableCell99 = new XRTableCell();
			this.xrTableCell100 = new XRTableCell();
			this.xrTableCell101 = new XRTableCell();
			this.xrTableCell102 = new XRTableCell();
			this.xrTableCell103 = new XRTableCell();
			this.lblSummaryGrandTotal = new XRTableCell();
			this.xrTableCell105 = new XRTableCell();
			this.xrTableCell106 = new XRTableCell();
			this.formattingRule19 = new FormattingRule();
			this.formattingRule20 = new FormattingRule();
			((ISupportInitialize)this.xrTable2).BeginInit();
			((ISupportInitialize)this.xrTable1).BeginInit();
			((ISupportInitialize)this.xrTable4).BeginInit();
			((ISupportInitialize)this.xrTable5).BeginInit();
			((ISupportInitialize)this.xrTable6).BeginInit();
			((ISupportInitialize)this.xrTable10).BeginInit();
			((ISupportInitialize)this.xrTable9).BeginInit();
			((ISupportInitialize)this.xrTable11).BeginInit();
			((ISupportInitialize)this.xrTable18).BeginInit();
			((ISupportInitialize)this.xrTable12).BeginInit();
			((ISupportInitialize)this.xrTable13).BeginInit();
			((ISupportInitialize)this.xrTable14).BeginInit();
			((ISupportInitialize)this.xrTable7).BeginInit();
			((ISupportInitialize)this.xrTable3).BeginInit();
			((ISupportInitialize)this.xrTable8).BeginInit();
			((ISupportInitialize)this.xrTable15).BeginInit();
			((ISupportInitialize)this.xrTable16).BeginInit();
			((ISupportInitialize)this.xrTable17).BeginInit();
			((ISupportInitialize)this).BeginInit();
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
			this.SubBand1.Controls.AddRange(new XRControl[]
			{
				this.xrTable2,
				this.xrTable1
			});
			this.SubBand1.FormattingRules.Add(this.formattingRule4);
			this.SubBand1.HeightF = 75f;
			this.SubBand1.Name = "SubBand1";
			this.xrTable2.BorderColor = Color.DarkSlateGray;
			this.xrTable2.Borders = BorderSide.All;
			this.xrTable2.BorderWidth = 2f;
			this.xrTable2.LocationFloat = new PointFloat(462.5416f, 0f);
			this.xrTable2.Name = "xrTable2";
			this.xrTable2.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow3,
				this.xrTableRow4
			});
			this.xrTable2.SizeF = new SizeF(313.9584f, 62.5f);
			this.xrTable2.StylePriority.UseBorderColor = false;
			this.xrTable2.StylePriority.UseBorderWidth = false;
			this.xrTableRow3.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell3
			});
			this.xrTableRow3.Name = "xrTableRow3";
			this.xrTableRow3.Weight = 1.0;
			this.xrTableCell3.BackColor = Color.DarkSlateGray;
			this.xrTableCell3.BorderColor = Color.White;
			this.xrTableCell3.Borders = BorderSide.None;
			this.xrTableCell3.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell3.ForeColor = Color.White;
			this.xrTableCell3.Name = "xrTableCell3";
			this.xrTableCell3.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell3.StylePriority.UseBackColor = false;
			this.xrTableCell3.StylePriority.UseFont = false;
			this.xrTableCell3.StylePriority.UseForeColor = false;
			this.xrTableCell3.StylePriority.UsePadding = false;
			this.xrTableCell3.Text = "Nom du contact :";
			this.xrTableCell3.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell3.Weight = 1.0;
			this.xrTableRow4.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell4
			});
			this.xrTableRow4.Name = "xrTableRow4";
			this.xrTableRow4.Weight = 1.0;
			this.xrTableCell4.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.ContactName")
			});
			this.xrTableCell4.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell4.Name = "xrTableCell4";
			this.xrTableCell4.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell4.StylePriority.UseBorderColor = false;
			this.xrTableCell4.StylePriority.UseFont = false;
			this.xrTableCell4.StylePriority.UsePadding = false;
			this.xrTableCell4.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell4.Weight = 1.0;
			this.xrTable1.BorderColor = Color.DarkSlateGray;
			this.xrTable1.Borders = BorderSide.All;
			this.xrTable1.BorderWidth = 2f;
			this.xrTable1.LocationFloat = new PointFloat(123.5f, 0f);
			this.xrTable1.Name = "xrTable1";
			this.xrTable1.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow1,
				this.xrTableRow2
			});
			this.xrTable1.SizeF = new SizeF(313.9584f, 62.5f);
			this.xrTable1.StylePriority.UseBorderColor = false;
			this.xrTable1.StylePriority.UseBorderWidth = false;
			this.xrTable1.StylePriority.UsePadding = false;
			this.xrTableRow1.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell1
			});
			this.xrTableRow1.Name = "xrTableRow1";
			this.xrTableRow1.Weight = 1.0;
			this.xrTableCell1.BackColor = Color.DarkSlateGray;
			this.xrTableCell1.BorderColor = Color.White;
			this.xrTableCell1.Borders = BorderSide.None;
			this.xrTableCell1.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell1.ForeColor = Color.White;
			this.xrTableCell1.Name = "xrTableCell1";
			this.xrTableCell1.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell1.StylePriority.UseBackColor = false;
			this.xrTableCell1.StylePriority.UseBorderColor = false;
			this.xrTableCell1.StylePriority.UseFont = false;
			this.xrTableCell1.StylePriority.UseForeColor = false;
			this.xrTableCell1.StylePriority.UsePadding = false;
			this.xrTableCell1.Text = "Nom du projet :";
			this.xrTableCell1.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell1.Weight = 1.0;
			this.xrTableRow2.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell2
			});
			this.xrTableRow2.Name = "xrTableRow2";
			this.xrTableRow2.Weight = 1.0;
			this.xrTableCell2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.Name")
			});
			this.xrTableCell2.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell2.Name = "xrTableCell2";
			this.xrTableCell2.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell2.StylePriority.UseBorderColor = false;
			this.xrTableCell2.StylePriority.UseFont = false;
			this.xrTableCell2.StylePriority.UsePadding = false;
			this.xrTableCell2.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell2.Weight = 1.0;
			this.formattingRule4.Condition = "IsNullOrEmpty([Name])";
			this.formattingRule4.DataMember = "Project";
			this.formattingRule4.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule4.Name = "formattingRule4";
			this.SubBand2.Controls.AddRange(new XRControl[]
			{
				this.xrTable4
			});
			this.SubBand2.FormattingRules.Add(this.formattingRule6);
			this.SubBand2.HeightF = 75f;
			this.SubBand2.Name = "SubBand2";
			this.xrTable4.BorderColor = Color.DarkSlateGray;
			this.xrTable4.Borders = BorderSide.All;
			this.xrTable4.BorderWidth = 2f;
			this.xrTable4.LocationFloat = new PointFloat(125f, 0f);
			this.xrTable4.Name = "xrTable4";
			this.xrTable4.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow7,
				this.xrTableRow8
			});
			this.xrTable4.SizeF = new SizeF(652.9999f, 62.5f);
			this.xrTable4.StylePriority.UseBorderColor = false;
			this.xrTable4.StylePriority.UseBorderWidth = false;
			this.xrTableRow7.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell7
			});
			this.xrTableRow7.Name = "xrTableRow7";
			this.xrTableRow7.Weight = 1.0;
			this.xrTableCell7.BackColor = Color.DarkSlateGray;
			this.xrTableCell7.BorderColor = Color.White;
			this.xrTableCell7.Borders = BorderSide.None;
			this.xrTableCell7.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell7.ForeColor = Color.White;
			this.xrTableCell7.Name = "xrTableCell7";
			this.xrTableCell7.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell7.StylePriority.UseBackColor = false;
			this.xrTableCell7.StylePriority.UseFont = false;
			this.xrTableCell7.StylePriority.UseForeColor = false;
			this.xrTableCell7.StylePriority.UsePadding = false;
			this.xrTableCell7.Text = "Description du projet :";
			this.xrTableCell7.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell7.Weight = 1.0;
			this.xrTableRow8.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell8
			});
			this.xrTableRow8.Name = "xrTableRow8";
			this.xrTableRow8.Weight = 1.0;
			this.xrTableCell8.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.Description")
			});
			this.xrTableCell8.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell8.Multiline = true;
			this.xrTableCell8.Name = "xrTableCell8";
			this.xrTableCell8.Padding = new PaddingInfo(10, 0, 10, 10, 100f);
			this.xrTableCell8.StylePriority.UseBorderColor = false;
			this.xrTableCell8.StylePriority.UseFont = false;
			this.xrTableCell8.StylePriority.UsePadding = false;
			this.xrTableCell8.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell8.Weight = 1.0;
			this.formattingRule6.Condition = "IsNullOrEmpty([Description])";
			this.formattingRule6.DataMember = "Project";
			this.formattingRule6.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule6.Name = "formattingRule6";
			this.SubBand3.Controls.AddRange(new XRControl[]
			{
				this.xrTable5
			});
			this.SubBand3.FormattingRules.Add(this.formattingRule7);
			this.SubBand3.HeightF = 75f;
			this.SubBand3.Name = "SubBand3";
			this.xrTable5.BorderColor = Color.DarkSlateGray;
			this.xrTable5.Borders = BorderSide.All;
			this.xrTable5.BorderWidth = 2f;
			this.xrTable5.LocationFloat = new PointFloat(125f, 0f);
			this.xrTable5.Name = "xrTable5";
			this.xrTable5.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow9,
				this.xrTableRow10
			});
			this.xrTable5.SizeF = new SizeF(652.9999f, 62.5f);
			this.xrTable5.StylePriority.UseBorderColor = false;
			this.xrTable5.StylePriority.UseBorderWidth = false;
			this.xrTableRow9.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell9
			});
			this.xrTableRow9.Name = "xrTableRow9";
			this.xrTableRow9.Weight = 1.0;
			this.xrTableCell9.BackColor = Color.DarkSlateGray;
			this.xrTableCell9.BorderColor = Color.White;
			this.xrTableCell9.Borders = BorderSide.None;
			this.xrTableCell9.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell9.ForeColor = Color.White;
			this.xrTableCell9.Name = "xrTableCell9";
			this.xrTableCell9.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell9.StylePriority.UseBackColor = false;
			this.xrTableCell9.StylePriority.UseFont = false;
			this.xrTableCell9.StylePriority.UseForeColor = false;
			this.xrTableCell9.StylePriority.UsePadding = false;
			this.xrTableCell9.Text = "Information du contact :";
			this.xrTableCell9.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell9.Weight = 1.0;
			this.xrTableRow10.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell10
			});
			this.xrTableRow10.Name = "xrTableRow10";
			this.xrTableRow10.Weight = 1.0;
			this.xrTableCell10.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.ContactInfo")
			});
			this.xrTableCell10.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell10.Multiline = true;
			this.xrTableCell10.Name = "xrTableCell10";
			this.xrTableCell10.Padding = new PaddingInfo(10, 0, 10, 10, 100f);
			this.xrTableCell10.StylePriority.UseBorderColor = false;
			this.xrTableCell10.StylePriority.UseFont = false;
			this.xrTableCell10.StylePriority.UsePadding = false;
			this.xrTableCell10.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell10.Weight = 1.0;
			this.formattingRule7.Condition = "IsNullOrEmpty([ContactInfo])";
			this.formattingRule7.DataMember = "Project";
			this.formattingRule7.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule7.Name = "formattingRule7";
			this.SubBand4.Controls.AddRange(new XRControl[]
			{
				this.xrTable6
			});
			this.SubBand4.FormattingRules.Add(this.formattingRule8);
			this.SubBand4.HeightF = 75f;
			this.SubBand4.Name = "SubBand4";
			this.xrTable6.BorderColor = Color.DarkSlateGray;
			this.xrTable6.Borders = BorderSide.All;
			this.xrTable6.BorderWidth = 2f;
			this.xrTable6.LocationFloat = new PointFloat(125f, 0f);
			this.xrTable6.Name = "xrTable6";
			this.xrTable6.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow11,
				this.xrTableRow12
			});
			this.xrTable6.SizeF = new SizeF(652.9999f, 62.5f);
			this.xrTable6.StylePriority.UseBorderColor = false;
			this.xrTable6.StylePriority.UseBorderWidth = false;
			this.xrTableRow11.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell11
			});
			this.xrTableRow11.Name = "xrTableRow11";
			this.xrTableRow11.Weight = 1.0;
			this.xrTableCell11.BackColor = Color.DarkSlateGray;
			this.xrTableCell11.BorderColor = Color.White;
			this.xrTableCell11.Borders = BorderSide.None;
			this.xrTableCell11.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell11.ForeColor = Color.White;
			this.xrTableCell11.Name = "xrTableCell11";
			this.xrTableCell11.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell11.StylePriority.UseBackColor = false;
			this.xrTableCell11.StylePriority.UseFont = false;
			this.xrTableCell11.StylePriority.UseForeColor = false;
			this.xrTableCell11.StylePriority.UsePadding = false;
			this.xrTableCell11.Text = "Commentaires :";
			this.xrTableCell11.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell11.Weight = 1.0;
			this.xrTableRow12.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell12
			});
			this.xrTableRow12.Name = "xrTableRow12";
			this.xrTableRow12.Weight = 1.0;
			this.xrTableCell12.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.Comment")
			});
			this.xrTableCell12.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell12.Multiline = true;
			this.xrTableCell12.Name = "xrTableCell12";
			this.xrTableCell12.Padding = new PaddingInfo(10, 0, 10, 10, 100f);
			this.xrTableCell12.StylePriority.UseBorderColor = false;
			this.xrTableCell12.StylePriority.UseFont = false;
			this.xrTableCell12.StylePriority.UsePadding = false;
			this.xrTableCell12.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell12.Weight = 1.0;
			this.formattingRule8.Condition = "IsNullOrEmpty([Comment])";
			this.formattingRule8.DataMember = "Project";
			this.formattingRule8.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule8.Name = "formattingRule8";
			this.SubBand5.Controls.AddRange(new XRControl[]
			{
				this.xrLabel20
			});
			this.SubBand5.FormattingRules.Add(this.formattingRule4);
			this.SubBand5.HeightF = 38f;
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
				this.DetailReport3,
				this.DetailReport5,
				this.DetailReport1
			});
			this.DetailReport2.DataMember = "PlansSummaries.PlansSummaries_PlanSummaries";
			this.DetailReport2.Level = 0;
			this.DetailReport2.Name = "DetailReport2";
			this.Detail3.Controls.AddRange(new XRControl[]
			{
				this.lblSummaryByPlans
			});
			this.Detail3.HeightF = 35f;
			this.Detail3.Name = "Detail3";
			this.Detail3.StylePriority.UseBackColor = false;
			this.lblSummaryByPlans.BackColor = Color.Transparent;
			this.lblSummaryByPlans.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.Name")
			});
			this.lblSummaryByPlans.Font = new Font("Tahoma", 15.25f, FontStyle.Bold | FontStyle.Underline);
			this.lblSummaryByPlans.ForeColor = Color.FromArgb(0, 64, 64);
			this.lblSummaryByPlans.LocationFloat = new PointFloat(0f, 0f);
			this.lblSummaryByPlans.Name = "lblSummaryByPlans";
			this.lblSummaryByPlans.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblSummaryByPlans.Scripts.OnAfterPrint = "lblSummaryByPlans_AfterPrint";
			this.lblSummaryByPlans.Scripts.OnBeforePrint = "xrLabel9_BeforePrint";
			this.lblSummaryByPlans.SizeF = new SizeF(900f, 23f);
			this.lblSummaryByPlans.StylePriority.UseFont = false;
			this.lblSummaryByPlans.StylePriority.UseForeColor = false;
			this.lblSummaryByPlans.StylePriority.UsePadding = false;
			this.lblSummaryByPlans.StylePriority.UseTextAlignment = false;
			this.lblSummaryByPlans.TextAlignment = TextAlignment.TopCenter;
			this.DetailReport3.Bands.AddRange(new Band[]
			{
				this.Detail4
			});
			this.DetailReport3.Level = 0;
			this.DetailReport3.Name = "DetailReport3";
			this.Detail4.HeightF = 0f;
			this.Detail4.Name = "Detail4";
			this.DetailReport5.Bands.AddRange(new Band[]
			{
				this.Detail6,
				this.DetailReport6
			});
			this.DetailReport5.DataMember = "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries";
			this.DetailReport5.Level = 1;
			this.DetailReport5.Name = "DetailReport5";
			this.Detail6.HeightF = 0f;
			this.Detail6.KeepTogether = true;
			this.Detail6.KeepTogetherWithDetailReports = true;
			this.Detail6.Name = "Detail6";
			this.DetailReport6.Bands.AddRange(new Band[]
			{
				this.Detail7,
				this.GroupHeader2,
				this.ReportFooter
			});
			this.DetailReport6.DataMember = "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary";
			this.DetailReport6.Level = 0;
			this.DetailReport6.Name = "DetailReport6";
			this.Detail7.Controls.AddRange(new XRControl[]
			{
				this.xrLine3,
				this.xrTable10
			});
			this.Detail7.HeightF = 23f;
			this.Detail7.Name = "Detail7";
			this.xrLine3.BorderColor = Color.Transparent;
			this.xrLine3.ForeColor = Color.DarkGray;
			this.xrLine3.LineStyle = DashStyle.Dot;
			this.xrLine3.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine3.Name = "xrLine3";
			this.xrLine3.SizeF = new SizeF(900f, 3f);
			this.xrLine3.StylePriority.UseBorderColor = false;
			this.xrLine3.StylePriority.UseForeColor = false;
			this.xrTable10.Borders = BorderSide.None;
			this.xrTable10.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable10.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable10.LocationFloat = new PointFloat(1.589457E-05f, 0f);
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
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary.Name")
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
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary.TotalBreakdown")
			});
			this.xrTableCell37.Name = "xrTableCell37";
			this.xrTableCell37.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell37.Scripts.OnBeforePrint = "xrTableCell37_BeforePrint";
			this.xrTableCell37.StylePriority.UseBackColor = false;
			this.xrTableCell37.StylePriority.UsePadding = false;
			this.xrTableCell37.StylePriority.UseTextAlignment = false;
			this.xrTableCell37.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell37.Weight = 0.6062221343233483;
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
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary.TotalBreakdownTag")
			});
			this.xrLabel4.ForeColor = Color.Blue;
			this.xrLabel4.LocationFloat = new PointFloat(4.00003f, 0.9999593f);
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
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary.CostTotalSubTotal")
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
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary.PriceTotalSubTotal")
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
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary.MarginSubTotal")
			});
			this.xrTableCell46.FormattingRules.Add(this.formattingRule20);
			this.xrTableCell46.Name = "xrTableCell46";
			this.xrTableCell46.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell46.StylePriority.UseFont = false;
			this.xrTableCell46.StylePriority.UsePadding = false;
			this.xrTableCell46.StylePriority.UseTextAlignment = false;
			xrsummary.FormatString = "{0:0.00%}";
			xrsummary.Func = SummaryFunc.Avg;
			this.xrTableCell46.Summary = xrsummary;
			this.xrTableCell46.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell46.Visible = false;
			this.xrTableCell46.Weight = 0.7543739676644249;
			this.formattingRule16.Condition = "[RawTaxe1Total] > 0";
			this.formattingRule16.DataMember = "Summaries.Summaries_Total";
			this.formattingRule16.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule16.Name = "formattingRule16";
			this.GroupHeader2.Controls.AddRange(new XRControl[]
			{
				this.xrTable9,
				this.xrLabel5
			});
			this.GroupHeader2.GroupUnion = GroupUnion.WithFirstDetail;
			this.GroupHeader2.HeightF = 48f;
			this.GroupHeader2.KeepTogether = true;
			this.GroupHeader2.Name = "GroupHeader2";
			this.GroupHeader2.RepeatEveryPage = true;
			this.xrTable9.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline);
			this.xrTable9.ForeColor = Color.DarkRed;
			this.xrTable9.LocationFloat = new PointFloat(0f, 20f);
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
				this.lblPlanSummaryGroup,
				this.xrTableCell39,
				this.lblPlanSummaryBreakdown,
				this.xrTableCell42,
				this.lblPlanSummaryCostTotal,
				this.xrTableCell48,
				this.lblPlanSummaryPriceTotal,
				this.lblPlanSummaryMargin
			});
			this.xrTableRow14.Name = "xrTableRow14";
			this.xrTableRow14.Weight = 1.0;
			this.lblPlanSummaryGroup.Name = "lblPlanSummaryGroup";
			this.lblPlanSummaryGroup.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblPlanSummaryGroup.StylePriority.UsePadding = false;
			this.lblPlanSummaryGroup.StylePriority.UseTextAlignment = false;
			this.lblPlanSummaryGroup.Text = "Groupe";
			this.lblPlanSummaryGroup.TextAlignment = TextAlignment.MiddleLeft;
			this.lblPlanSummaryGroup.Weight = 1.7528241134162492;
			this.xrTableCell39.Name = "xrTableCell39";
			this.xrTableCell39.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell39.StylePriority.UsePadding = false;
			this.xrTableCell39.StylePriority.UseTextAlignment = false;
			this.xrTableCell39.TextAlignment = TextAlignment.MiddleCenter;
			this.xrTableCell39.Weight = 0.47028489133506407;
			this.lblPlanSummaryBreakdown.Name = "lblPlanSummaryBreakdown";
			this.lblPlanSummaryBreakdown.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.lblPlanSummaryBreakdown.StylePriority.UsePadding = false;
			this.lblPlanSummaryBreakdown.StylePriority.UseTextAlignment = false;
			this.lblPlanSummaryBreakdown.Text = "Répartition";
			this.lblPlanSummaryBreakdown.TextAlignment = TextAlignment.MiddleCenter;
			this.lblPlanSummaryBreakdown.Weight = 1.7857512794421047;
			this.xrTableCell42.Name = "xrTableCell42";
			this.xrTableCell42.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell42.StylePriority.UsePadding = false;
			this.xrTableCell42.StylePriority.UseTextAlignment = false;
			this.xrTableCell42.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell42.Weight = 0.09169374809696523;
			this.lblPlanSummaryCostTotal.Name = "lblPlanSummaryCostTotal";
			this.lblPlanSummaryCostTotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblPlanSummaryCostTotal.StylePriority.UsePadding = false;
			this.lblPlanSummaryCostTotal.StylePriority.UseTextAlignment = false;
			this.lblPlanSummaryCostTotal.Text = "Coûtant total";
			this.lblPlanSummaryCostTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblPlanSummaryCostTotal.Weight = 1.1074197777923485;
			this.xrTableCell48.Name = "xrTableCell48";
			this.xrTableCell48.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell48.StylePriority.UsePadding = false;
			this.xrTableCell48.StylePriority.UseTextAlignment = false;
			this.xrTableCell48.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell48.Weight = 0.9302326166176373;
			this.lblPlanSummaryPriceTotal.Name = "lblPlanSummaryPriceTotal";
			this.lblPlanSummaryPriceTotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblPlanSummaryPriceTotal.StylePriority.UsePadding = false;
			this.lblPlanSummaryPriceTotal.StylePriority.UseTextAlignment = false;
			this.lblPlanSummaryPriceTotal.Text = "Prix total";
			this.lblPlanSummaryPriceTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblPlanSummaryPriceTotal.Weight = 1.107419776668326;
			this.lblPlanSummaryMargin.Name = "lblPlanSummaryMargin";
			this.lblPlanSummaryMargin.Padding = new PaddingInfo(1, 10, 0, 0, 100f);
			this.lblPlanSummaryMargin.StylePriority.UsePadding = false;
			this.lblPlanSummaryMargin.StylePriority.UseTextAlignment = false;
			this.lblPlanSummaryMargin.Text = "Marge";
			this.lblPlanSummaryMargin.TextAlignment = TextAlignment.MiddleRight;
			this.lblPlanSummaryMargin.Weight = 0.7543739315906786;
			this.xrLabel5.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.Name")
			});
			this.xrLabel5.Font = new Font("Tahoma", 10f, FontStyle.Bold | FontStyle.Underline);
			this.xrLabel5.ForeColor = Color.DarkRed;
			this.xrLabel5.LocationFloat = new PointFloat(0f, 0f);
			this.xrLabel5.Name = "xrLabel5";
			this.xrLabel5.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrLabel5.SizeF = new SizeF(900f, 19.16666f);
			this.xrLabel5.StylePriority.UseFont = false;
			this.xrLabel5.StylePriority.UseForeColor = false;
			this.xrLabel5.StylePriority.UsePadding = false;
			this.ReportFooter.Controls.AddRange(new XRControl[]
			{
				this.xrTable11
			});
			this.ReportFooter.HeightF = 32f;
			this.ReportFooter.Name = "ReportFooter";
			this.xrTable11.Borders = BorderSide.None;
			this.xrTable11.BorderWidth = 3f;
			this.xrTable11.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable11.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable11.LocationFloat = new PointFloat(0f, 5f);
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
				this.lblPlanSummarySubTotals,
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
			this.lblPlanSummarySubTotals.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblPlanSummarySubTotals.Name = "lblPlanSummarySubTotals";
			this.lblPlanSummarySubTotals.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblPlanSummarySubTotals.StylePriority.UseFont = false;
			this.lblPlanSummarySubTotals.StylePriority.UsePadding = false;
			this.lblPlanSummarySubTotals.StylePriority.UseTextAlignment = false;
			this.lblPlanSummarySubTotals.Text = "Sous-totaux :";
			this.lblPlanSummarySubTotals.TextAlignment = TextAlignment.MiddleRight;
			this.lblPlanSummarySubTotals.Weight = 0.9302324668585246;
			this.xrTableCell55.Borders = BorderSide.None;
			this.xrTableCell55.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanTotal.CostTotal")
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
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanTotal.PriceTotal")
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
			xrsummary2.FormatString = "{0:c}";
			this.xrTableCell57.Summary = xrsummary2;
			this.xrTableCell57.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell57.Weight = 1.1074195798819029;
			this.xrTableCell58.Borders = BorderSide.None;
			this.xrTableCell58.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanTotal.MarginTotal")
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
			xrsummary3.Func = SummaryFunc.Custom;
			this.xrTableCell58.Summary = xrsummary3;
			this.xrTableCell58.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell58.Visible = false;
			this.xrTableCell58.Weight = 0.7543739676644248;
			this.formattingRule15.Condition = "[RawMarginTotal] > 0";
			this.formattingRule15.DataMember = "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanTotal";
			this.formattingRule15.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule15.Name = "formattingRule15";
			this.DetailReport1.Bands.AddRange(new Band[]
			{
				this.Detail2
			});
			this.DetailReport1.DataMember = "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_Total";
			this.DetailReport1.Level = 2;
			this.DetailReport1.Name = "DetailReport1";
			this.Detail2.Controls.AddRange(new XRControl[]
			{
				this.xrTable18,
				this.xrLabel14
			});
			this.Detail2.HeightF = 50f;
			this.Detail2.Name = "Detail2";
			this.xrTable18.Borders = BorderSide.None;
			this.xrTable18.BorderWidth = 3f;
			this.xrTable18.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable18.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable18.LocationFloat = new PointFloat(0f, 5f);
			this.xrTable18.Name = "xrTable18";
			this.xrTable18.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow23
			});
			this.xrTable18.SizeF = new SizeF(900f, 18f);
			this.xrTable18.StylePriority.UseBorders = false;
			this.xrTable18.StylePriority.UseBorderWidth = false;
			this.xrTable18.StylePriority.UseFont = false;
			this.xrTable18.StylePriority.UseForeColor = false;
			this.xrTable18.StylePriority.UseTextAlignment = false;
			this.xrTable18.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow23.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell107,
				this.xrTableCell108,
				this.xrTableCell109,
				this.lblPlanSummaryTotals,
				this.xrTableCell111,
				this.xrTableCell112,
				this.xrTableCell113,
				this.xrTableCell114
			});
			this.xrTableRow23.Name = "xrTableRow23";
			this.xrTableRow23.Weight = 1.0;
			this.xrTableCell107.Name = "xrTableCell107";
			this.xrTableCell107.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell107.StylePriority.UsePadding = false;
			this.xrTableCell107.Weight = 0.013564731887289773;
			this.xrTableCell108.Name = "xrTableCell108";
			this.xrTableCell108.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell108.StylePriority.UsePadding = false;
			this.xrTableCell108.StylePriority.UseTextAlignment = false;
			this.xrTableCell108.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell108.Weight = 0.01851851774038501;
			this.xrTableCell109.Name = "xrTableCell109";
			this.xrTableCell109.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell109.StylePriority.UsePadding = false;
			this.xrTableCell109.Weight = 0.12445435850446303;
			this.lblPlanSummaryTotals.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.lblPlanSummaryTotals.Name = "lblPlanSummaryTotals";
			this.lblPlanSummaryTotals.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblPlanSummaryTotals.Scripts.OnBeforePrint = "xrTableCell110_BeforePrint";
			this.lblPlanSummaryTotals.StylePriority.UseFont = false;
			this.lblPlanSummaryTotals.StylePriority.UsePadding = false;
			this.lblPlanSummaryTotals.StylePriority.UseTextAlignment = false;
			this.lblPlanSummaryTotals.TextAlignment = TextAlignment.MiddleRight;
			this.lblPlanSummaryTotals.Visible = false;
			this.lblPlanSummaryTotals.Weight = 3.9440158174113056;
			this.xrTableCell111.Borders = BorderSide.None;
			this.xrTableCell111.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_Total.CostTotal")
			});
			this.xrTableCell111.Font = new Font("Tahoma", 8.5f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell111.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell111.Name = "xrTableCell111";
			this.xrTableCell111.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell111.StylePriority.UseBorders = false;
			this.xrTableCell111.StylePriority.UseFont = false;
			this.xrTableCell111.StylePriority.UseForeColor = false;
			this.xrTableCell111.StylePriority.UsePadding = false;
			this.xrTableCell111.StylePriority.UseTextAlignment = false;
			this.xrTableCell111.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell111.Weight = 1.107419581005925;
			this.xrTableCell112.Name = "xrTableCell112";
			this.xrTableCell112.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell112.StylePriority.UsePadding = false;
			this.xrTableCell112.StylePriority.UseTextAlignment = false;
			this.xrTableCell112.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell112.Weight = 0.9302324993991369;
			this.xrTableCell113.Borders = BorderSide.None;
			this.xrTableCell113.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_Total.PriceTotal")
			});
			this.xrTableCell113.Font = new Font("Tahoma", 8.5f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell113.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell113.Name = "xrTableCell113";
			this.xrTableCell113.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell113.StylePriority.UseBorders = false;
			this.xrTableCell113.StylePriority.UseFont = false;
			this.xrTableCell113.StylePriority.UseForeColor = false;
			this.xrTableCell113.StylePriority.UsePadding = false;
			this.xrTableCell113.StylePriority.UseTextAlignment = false;
			xrsummary4.FormatString = "{0:c}";
			this.xrTableCell113.Summary = xrsummary4;
			this.xrTableCell113.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell113.Weight = 1.1074195798819029;
			this.xrTableCell114.Borders = BorderSide.None;
			this.xrTableCell114.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_Total.MarginTotal")
			});
			this.xrTableCell114.Font = new Font("Tahoma", 8.5f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell114.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell114.FormattingRules.Add(this.formattingRule17);
			this.xrTableCell114.Name = "xrTableCell114";
			this.xrTableCell114.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell114.StylePriority.UseBorders = false;
			this.xrTableCell114.StylePriority.UseFont = false;
			this.xrTableCell114.StylePriority.UseForeColor = false;
			this.xrTableCell114.StylePriority.UsePadding = false;
			this.xrTableCell114.StylePriority.UseTextAlignment = false;
			xrsummary5.Func = SummaryFunc.Custom;
			this.xrTableCell114.Summary = xrsummary5;
			this.xrTableCell114.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell114.Visible = false;
			this.xrTableCell114.Weight = 0.7543739676644248;
			this.formattingRule17.Condition = "[RawTaxe2Total] > 0";
			this.formattingRule17.DataMember = "Summaries.Summaries_Total";
			this.formattingRule17.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule17.Name = "formattingRule17";
			this.xrLabel14.BackColor = Color.Transparent;
			this.xrLabel14.BorderColor = Color.Black;
			this.xrLabel14.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel14.ForeColor = Color.Transparent;
			this.xrLabel14.LocationFloat = new PointFloat(328.607f, 34.99997f);
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
			this.formattingRule14.Condition = "[RawMarginSubTotal] > 0";
			this.formattingRule14.DataMember = "Summaries.Summaries_Summary";
			this.formattingRule14.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule14.Name = "formattingRule14";
			this.SubBand6.Controls.AddRange(new XRControl[]
			{
				this.xrTable12
			});
			this.SubBand6.HeightF = 21f;
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
				this.xrTableCell64,
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
			this.xrTableCell64.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Total.Taxe1Caption")
			});
			this.xrTableCell64.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell64.Name = "xrTableCell64";
			this.xrTableCell64.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell64.StylePriority.UseFont = false;
			this.xrTableCell64.StylePriority.UsePadding = false;
			this.xrTableCell64.StylePriority.UseTextAlignment = false;
			this.xrTableCell64.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell64.Weight = 1.949057905640072;
			this.xrTableCell65.Borders = BorderSide.None;
			this.xrTableCell65.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe1Total")
			});
			this.xrTableCell65.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell65.ForeColor = Color.DarkRed;
			this.xrTableCell65.Name = "xrTableCell65";
			this.xrTableCell65.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell65.StylePriority.UseBorders = false;
			this.xrTableCell65.StylePriority.UseFont = false;
			this.xrTableCell65.StylePriority.UseForeColor = false;
			this.xrTableCell65.StylePriority.UsePadding = false;
			this.xrTableCell65.StylePriority.UseTextAlignment = false;
			xrsummary6.FormatString = "{0:c}";
			this.xrTableCell65.Summary = xrsummary6;
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
			xrsummary7.Func = SummaryFunc.Custom;
			this.xrTableCell66.Summary = xrsummary7;
			this.xrTableCell66.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell66.Visible = false;
			this.xrTableCell66.Weight = 0.7543739676644248;
			this.SubBand8.Controls.AddRange(new XRControl[]
			{
				this.xrTable13
			});
			this.SubBand8.HeightF = 21f;
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
				this.xrTableCell72,
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
			this.xrTableCell72.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Total.Taxe2Caption")
			});
			this.xrTableCell72.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell72.Name = "xrTableCell72";
			this.xrTableCell72.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell72.StylePriority.UseFont = false;
			this.xrTableCell72.StylePriority.UsePadding = false;
			this.xrTableCell72.StylePriority.UseTextAlignment = false;
			this.xrTableCell72.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell72.Weight = 1.9579181409783137;
			this.xrTableCell73.Borders = BorderSide.None;
			this.xrTableCell73.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe2Total")
			});
			this.xrTableCell73.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell73.ForeColor = Color.DarkRed;
			this.xrTableCell73.Name = "xrTableCell73";
			this.xrTableCell73.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell73.StylePriority.UseBorders = false;
			this.xrTableCell73.StylePriority.UseFont = false;
			this.xrTableCell73.StylePriority.UseForeColor = false;
			this.xrTableCell73.StylePriority.UsePadding = false;
			this.xrTableCell73.StylePriority.UseTextAlignment = false;
			xrsummary8.FormatString = "{0:c}";
			this.xrTableCell73.Summary = xrsummary8;
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
			xrsummary9.Func = SummaryFunc.Custom;
			this.xrTableCell74.Summary = xrsummary9;
			this.xrTableCell74.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell74.Visible = false;
			this.xrTableCell74.Weight = 0.7543739676644248;
			this.SubBand9.Controls.AddRange(new XRControl[]
			{
				this.xrTable14
			});
			this.SubBand9.HeightF = 21f;
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
				this.xrTableCell80,
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
			this.xrTableCell80.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.xrTableCell80.Name = "xrTableCell80";
			this.xrTableCell80.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell80.StylePriority.UseFont = false;
			this.xrTableCell80.StylePriority.UsePadding = false;
			this.xrTableCell80.StylePriority.UseTextAlignment = false;
			this.xrTableCell80.Text = "GRAND TOTAL :";
			this.xrTableCell80.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell80.Weight = 1.9579178706122051;
			this.xrTableCell81.Borders = BorderSide.None;
			this.xrTableCell81.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.TotalAfterTaxes")
			});
			this.xrTableCell81.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell81.ForeColor = Color.DarkRed;
			this.xrTableCell81.Name = "xrTableCell81";
			this.xrTableCell81.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell81.StylePriority.UseBorders = false;
			this.xrTableCell81.StylePriority.UseFont = false;
			this.xrTableCell81.StylePriority.UseForeColor = false;
			this.xrTableCell81.StylePriority.UsePadding = false;
			this.xrTableCell81.StylePriority.UseTextAlignment = false;
			xrsummary10.FormatString = "{0:c}";
			this.xrTableCell81.Summary = xrsummary10;
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
			xrsummary11.Func = SummaryFunc.Custom;
			this.xrTableCell82.Summary = xrsummary11;
			this.xrTableCell82.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell82.Visible = false;
			this.xrTableCell82.Weight = 0.7543739676644248;
			this.formattingRule12.Condition = "[RawMargin] > 0";
			this.formattingRule12.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result";
			this.formattingRule12.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule12.Name = "formattingRule12";
			this.formattingRule9.Condition = "[EstimatingItem] == true";
			this.formattingRule9.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result";
			this.formattingRule9.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule9.Name = "formattingRule9";
			this.formattingRule13.Condition = "[RawMarginSubTotal] > 0";
			this.formattingRule13.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Summary";
			this.formattingRule13.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule13.Name = "formattingRule13";
			this.formattingRule2.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule2.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule2.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule2.Name = "formattingRule2";
			this.formattingRule1.Condition = "[BaseExtension] ==True Or IsNullOrEmpty([Extension_Fields]) Or IsNullOrEmpty([Extension_Fields.Fields_Field])";
			this.formattingRule1.DataMember = "Summaries.Summaries_Total";
			this.formattingRule1.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule1.Name = "formattingRule1";
			this.formattingRule3.Condition = "IsNullOrEmpty([Extension_Fields.Extension_Id])";
			this.formattingRule3.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule3.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule3.Name = "formattingRule3";
			this.SubBand7.HeightF = 3.125f;
			this.SubBand7.Name = "SubBand7";
			this.formattingRule5.Condition = "IsNullOrEmpty([ContactName])";
			this.formattingRule5.DataMember = "Project";
			this.formattingRule5.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule5.Name = "formattingRule5";
			this.formattingRule10.Condition = "[BaseExtension] == true";
			this.formattingRule10.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule10.Name = "formattingRule10";
			this.formattingRule11.Condition = "IsNullOrEmpty([BaseExtension])";
			this.formattingRule11.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Layer.Layer_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule11.Name = "formattingRule11";
			this.DetailReport.Bands.AddRange(new Band[]
			{
				this.Detail1,
				this.GroupHeader1,
				this.GroupFooter1
			});
			this.DetailReport.DataMember = "Summaries.Summaries_Summary";
			this.DetailReport.Level = 1;
			this.DetailReport.Name = "DetailReport";
			this.Detail1.Controls.AddRange(new XRControl[]
			{
				this.xrLine2,
				this.xrTable7
			});
			this.Detail1.HeightF = 23f;
			this.Detail1.Name = "Detail1";
			this.xrLine2.BorderColor = Color.Transparent;
			this.xrLine2.ForeColor = Color.DarkGray;
			this.xrLine2.LineStyle = DashStyle.Dot;
			this.xrLine2.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine2.Name = "xrLine2";
			this.xrLine2.SizeF = new SizeF(900f, 3f);
			this.xrLine2.StylePriority.UseBorderColor = false;
			this.xrLine2.StylePriority.UseForeColor = false;
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
				new XRBinding("Text", null, "Summaries.Summaries_Summary.Name")
			});
			this.xrTableCell19.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell19.Name = "xrTableCell19";
			this.xrTableCell19.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell19.StylePriority.UseFont = false;
			this.xrTableCell19.StylePriority.UseForeColor = false;
			this.xrTableCell19.StylePriority.UsePadding = false;
			this.xrTableCell19.Weight = 1.6355553711476463;
			this.xrTableCell20.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.TotalBreakdown")
			});
			this.xrTableCell20.Name = "xrTableCell20";
			this.xrTableCell20.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell20.Scripts.OnBeforePrint = "xrTableCell37_BeforePrint";
			this.xrTableCell20.StylePriority.UseBackColor = false;
			this.xrTableCell20.StylePriority.UsePadding = false;
			this.xrTableCell20.StylePriority.UseTextAlignment = false;
			this.xrTableCell20.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell20.Weight = 0.6062221343233484;
			this.xrTableCell21.Controls.AddRange(new XRControl[]
			{
				this.xrLabel2
			});
			this.xrTableCell21.Name = "xrTableCell21";
			this.xrTableCell21.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell21.StylePriority.UsePadding = false;
			this.xrTableCell21.Weight = 1.767081793174668;
			this.xrLabel2.BackColor = Color.Blue;
			this.xrLabel2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.TotalBreakdownTag")
			});
			this.xrLabel2.ForeColor = Color.Blue;
			this.xrLabel2.LocationFloat = new PointFloat(4.000015f, 0.9999593f);
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel2.Scripts.OnBeforePrint = "xrLabel4_BeforePrint";
			this.xrLabel2.SizeF = new SizeF(194f, 16f);
			this.xrLabel2.StylePriority.UseBackColor = false;
			this.xrLabel2.StylePriority.UseForeColor = false;
			this.xrLabel2.WordWrap = false;
			this.xrTableCell22.Name = "xrTableCell22";
			this.xrTableCell22.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell22.StylePriority.UsePadding = false;
			this.xrTableCell22.StylePriority.UseTextAlignment = false;
			this.xrTableCell22.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell22.Weight = 0.0916941268977809;
			this.xrTableCell23.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.CostTotalSubTotal")
			});
			this.xrTableCell23.Name = "xrTableCell23";
			this.xrTableCell23.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell23.StylePriority.UseFont = false;
			this.xrTableCell23.StylePriority.UsePadding = false;
			this.xrTableCell23.StylePriority.UseTextAlignment = false;
			this.xrTableCell23.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell23.Weight = 1.1074195810059249;
			this.xrTableCell24.Name = "xrTableCell24";
			this.xrTableCell24.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell24.StylePriority.UsePadding = false;
			this.xrTableCell24.StylePriority.UseTextAlignment = false;
			this.xrTableCell24.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell24.Weight = 0.9302324993991505;
			this.xrTableCell25.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.PriceTotalSubTotal")
			});
			this.xrTableCell25.Name = "xrTableCell25";
			this.xrTableCell25.Padding = new PaddingInfo(3, 2, 1, 0, 100f);
			this.xrTableCell25.StylePriority.UseFont = false;
			this.xrTableCell25.StylePriority.UsePadding = false;
			this.xrTableCell25.StylePriority.UseTextAlignment = false;
			this.xrTableCell25.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell25.Weight = 1.1074195798819029;
			this.xrTableCell26.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Summary.MarginSubTotal")
			});
			this.xrTableCell26.FormattingRules.Add(this.formattingRule14);
			this.xrTableCell26.Name = "xrTableCell26";
			this.xrTableCell26.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell26.StylePriority.UseFont = false;
			this.xrTableCell26.StylePriority.UsePadding = false;
			this.xrTableCell26.StylePriority.UseTextAlignment = false;
			xrsummary12.FormatString = "{0:0.00%}";
			xrsummary12.Func = SummaryFunc.Avg;
			this.xrTableCell26.Summary = xrsummary12;
			this.xrTableCell26.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell26.Visible = false;
			this.xrTableCell26.Weight = 0.7543739676644249;
			this.GroupHeader1.Controls.AddRange(new XRControl[]
			{
				this.lblSummaryTotals,
				this.xrTable3
			});
			this.GroupHeader1.GroupUnion = GroupUnion.WithFirstDetail;
			this.GroupHeader1.HeightF = 75f;
			this.GroupHeader1.KeepTogether = true;
			this.GroupHeader1.Name = "GroupHeader1";
			this.GroupHeader1.PageBreak = PageBreak.BeforeBand;
			this.GroupHeader1.RepeatEveryPage = true;
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
			this.xrTable3.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline);
			this.xrTable3.ForeColor = Color.DarkRed;
			this.xrTable3.LocationFloat = new PointFloat(0f, 50.49998f);
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
				this.lblSummaryItem,
				this.xrTableCell6,
				this.lblSummaryBreakdown,
				this.xrTableCell14,
				this.lblSummaryCostTotal,
				this.xrTableCell16,
				this.lblSummaryPriceTotal,
				this.lblSummaryMargin
			});
			this.xrTableRow5.Name = "xrTableRow5";
			this.xrTableRow5.Weight = 1.0;
			this.lblSummaryItem.Name = "lblSummaryItem";
			this.lblSummaryItem.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblSummaryItem.StylePriority.UsePadding = false;
			this.lblSummaryItem.StylePriority.UseTextAlignment = false;
			this.lblSummaryItem.Text = "Groupe";
			this.lblSummaryItem.TextAlignment = TextAlignment.MiddleLeft;
			this.lblSummaryItem.Weight = 1.7528241134162492;
			this.xrTableCell6.Name = "xrTableCell6";
			this.xrTableCell6.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell6.StylePriority.UsePadding = false;
			this.xrTableCell6.StylePriority.UseTextAlignment = false;
			this.xrTableCell6.TextAlignment = TextAlignment.MiddleCenter;
			this.xrTableCell6.Weight = 0.47028489133506407;
			this.lblSummaryBreakdown.Name = "lblSummaryBreakdown";
			this.lblSummaryBreakdown.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.lblSummaryBreakdown.StylePriority.UsePadding = false;
			this.lblSummaryBreakdown.StylePriority.UseTextAlignment = false;
			this.lblSummaryBreakdown.Text = "Répartition";
			this.lblSummaryBreakdown.TextAlignment = TextAlignment.MiddleCenter;
			this.lblSummaryBreakdown.Weight = 1.7857512794421047;
			this.xrTableCell14.Name = "xrTableCell14";
			this.xrTableCell14.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell14.StylePriority.UsePadding = false;
			this.xrTableCell14.StylePriority.UseTextAlignment = false;
			this.xrTableCell14.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell14.Weight = 0.09169374809696523;
			this.lblSummaryCostTotal.Name = "lblSummaryCostTotal";
			this.lblSummaryCostTotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblSummaryCostTotal.StylePriority.UsePadding = false;
			this.lblSummaryCostTotal.StylePriority.UseTextAlignment = false;
			this.lblSummaryCostTotal.Text = "Coûtant total";
			this.lblSummaryCostTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryCostTotal.Weight = 1.1074197777923485;
			this.xrTableCell16.Name = "xrTableCell16";
			this.xrTableCell16.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell16.StylePriority.UsePadding = false;
			this.xrTableCell16.StylePriority.UseTextAlignment = false;
			this.xrTableCell16.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell16.Weight = 0.9302326166176373;
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
			this.GroupFooter1.Controls.AddRange(new XRControl[]
			{
				this.xrTable8
			});
			this.GroupFooter1.HeightF = 30f;
			this.GroupFooter1.Name = "GroupFooter1";
			this.GroupFooter1.SubBands.AddRange(new SubBand[]
			{
				this.SubBand10,
				this.SubBand11,
				this.SubBand12
			});
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
				this.lblSummarySubTotals,
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
			this.lblSummarySubTotals.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummarySubTotals.Name = "lblSummarySubTotals";
			this.lblSummarySubTotals.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummarySubTotals.StylePriority.UseFont = false;
			this.lblSummarySubTotals.StylePriority.UsePadding = false;
			this.lblSummarySubTotals.StylePriority.UseTextAlignment = false;
			this.lblSummarySubTotals.Text = "Sous-totaux :";
			this.lblSummarySubTotals.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummarySubTotals.Weight = 0.9302324668585246;
			this.xrTableCell31.Borders = BorderSide.None;
			this.xrTableCell31.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.CostTotal")
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
				new XRBinding("Text", null, "Summaries.Summaries_Total.PriceTotal")
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
			xrsummary13.FormatString = "{0:c}";
			this.xrTableCell33.Summary = xrsummary13;
			this.xrTableCell33.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell33.Weight = 1.1074195798819029;
			this.xrTableCell34.Borders = BorderSide.None;
			this.xrTableCell34.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.MarginTotal")
			});
			this.xrTableCell34.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell34.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell34.FormattingRules.Add(this.formattingRule18);
			this.xrTableCell34.Name = "xrTableCell34";
			this.xrTableCell34.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell34.StylePriority.UseBorders = false;
			this.xrTableCell34.StylePriority.UseFont = false;
			this.xrTableCell34.StylePriority.UseForeColor = false;
			this.xrTableCell34.StylePriority.UsePadding = false;
			this.xrTableCell34.StylePriority.UseTextAlignment = false;
			xrsummary14.Func = SummaryFunc.Custom;
			this.xrTableCell34.Summary = xrsummary14;
			this.xrTableCell34.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell34.Visible = false;
			this.xrTableCell34.Weight = 0.7543739676644248;
			this.formattingRule18.Condition = "[RawMarginTotal] > 0";
			this.formattingRule18.DataMember = "Summaries.Summaries_Total";
			this.formattingRule18.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule18.Name = "formattingRule18";
			this.SubBand10.Controls.AddRange(new XRControl[]
			{
				this.xrTable15
			});
			this.SubBand10.FormattingRules.Add(this.formattingRule16);
			this.SubBand10.HeightF = 21f;
			this.SubBand10.Name = "SubBand10";
			this.SubBand10.Visible = false;
			this.xrTable15.Borders = BorderSide.None;
			this.xrTable15.BorderWidth = 3f;
			this.xrTable15.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable15.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable15.LocationFloat = new PointFloat(0f, 1f);
			this.xrTable15.Name = "xrTable15";
			this.xrTable15.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow20
			});
			this.xrTable15.SizeF = new SizeF(900f, 20f);
			this.xrTable15.StylePriority.UseBorders = false;
			this.xrTable15.StylePriority.UseBorderWidth = false;
			this.xrTable15.StylePriority.UseFont = false;
			this.xrTable15.StylePriority.UseForeColor = false;
			this.xrTable15.StylePriority.UseTextAlignment = false;
			this.xrTable15.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow20.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell83,
				this.xrTableCell84,
				this.xrTableCell85,
				this.xrTableCell86,
				this.xrTableCell87,
				this.lblSummaryTax1,
				this.xrTableCell89,
				this.xrTableCell90
			});
			this.xrTableRow20.Name = "xrTableRow20";
			this.xrTableRow20.Weight = 1.0;
			this.xrTableCell83.Name = "xrTableCell83";
			this.xrTableCell83.Padding = new PaddingInfo(10, 2, 3, 3, 100f);
			this.xrTableCell83.StylePriority.UsePadding = false;
			this.xrTableCell83.Weight = 1.752823830580536;
			this.xrTableCell84.Name = "xrTableCell84";
			this.xrTableCell84.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell84.StylePriority.UsePadding = false;
			this.xrTableCell84.StylePriority.UseTextAlignment = false;
			this.xrTableCell84.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell84.Weight = 0.8859357060943043;
			this.xrTableCell85.Name = "xrTableCell85";
			this.xrTableCell85.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell85.StylePriority.UsePadding = false;
			this.xrTableCell85.Weight = 0.5315614220100788;
			this.xrTableCell86.Name = "xrTableCell86";
			this.xrTableCell86.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell86.StylePriority.UsePadding = false;
			this.xrTableCell86.StylePriority.UseTextAlignment = false;
			this.xrTableCell86.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell86.Weight = 0.9302324668585246;
			this.xrTableCell87.Borders = BorderSide.None;
			this.xrTableCell87.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell87.ForeColor = Color.DarkRed;
			this.xrTableCell87.Name = "xrTableCell87";
			this.xrTableCell87.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell87.StylePriority.UseBorders = false;
			this.xrTableCell87.StylePriority.UseFont = false;
			this.xrTableCell87.StylePriority.UseForeColor = false;
			this.xrTableCell87.StylePriority.UsePadding = false;
			this.xrTableCell87.StylePriority.UseTextAlignment = false;
			this.xrTableCell87.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell87.Weight = 0.08859417476498987;
			this.lblSummaryTax1.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe1Caption")
			});
			this.lblSummaryTax1.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummaryTax1.Name = "lblSummaryTax1";
			this.lblSummaryTax1.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummaryTax1.StylePriority.UseFont = false;
			this.lblSummaryTax1.StylePriority.UsePadding = false;
			this.lblSummaryTax1.StylePriority.UseTextAlignment = false;
			this.lblSummaryTax1.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryTax1.Weight = 1.949057905640072;
			this.xrTableCell89.Borders = BorderSide.None;
			this.xrTableCell89.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe1Total")
			});
			this.xrTableCell89.Font = new Font("Tahoma", 8.5f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell89.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell89.Name = "xrTableCell89";
			this.xrTableCell89.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell89.StylePriority.UseBorders = false;
			this.xrTableCell89.StylePriority.UseFont = false;
			this.xrTableCell89.StylePriority.UseForeColor = false;
			this.xrTableCell89.StylePriority.UsePadding = false;
			this.xrTableCell89.StylePriority.UseTextAlignment = false;
			xrsummary15.FormatString = "{0:c}";
			this.xrTableCell89.Summary = xrsummary15;
			this.xrTableCell89.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell89.Weight = 1.1074195798819029;
			this.xrTableCell90.Borders = BorderSide.None;
			this.xrTableCell90.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell90.ForeColor = Color.DarkRed;
			this.xrTableCell90.Name = "xrTableCell90";
			this.xrTableCell90.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell90.StylePriority.UseBorders = false;
			this.xrTableCell90.StylePriority.UseFont = false;
			this.xrTableCell90.StylePriority.UseForeColor = false;
			this.xrTableCell90.StylePriority.UsePadding = false;
			this.xrTableCell90.StylePriority.UseTextAlignment = false;
			xrsummary16.Func = SummaryFunc.Custom;
			this.xrTableCell90.Summary = xrsummary16;
			this.xrTableCell90.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell90.Visible = false;
			this.xrTableCell90.Weight = 0.7543739676644248;
			this.SubBand11.Controls.AddRange(new XRControl[]
			{
				this.xrTable16
			});
			this.SubBand11.FormattingRules.Add(this.formattingRule17);
			this.SubBand11.HeightF = 21f;
			this.SubBand11.Name = "SubBand11";
			this.SubBand11.Visible = false;
			this.xrTable16.Borders = BorderSide.None;
			this.xrTable16.BorderWidth = 3f;
			this.xrTable16.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable16.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable16.LocationFloat = new PointFloat(0f, 1f);
			this.xrTable16.Name = "xrTable16";
			this.xrTable16.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow21
			});
			this.xrTable16.SizeF = new SizeF(900f, 20f);
			this.xrTable16.StylePriority.UseBorders = false;
			this.xrTable16.StylePriority.UseBorderWidth = false;
			this.xrTable16.StylePriority.UseFont = false;
			this.xrTable16.StylePriority.UseForeColor = false;
			this.xrTable16.StylePriority.UseTextAlignment = false;
			this.xrTable16.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow21.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell91,
				this.xrTableCell92,
				this.xrTableCell93,
				this.xrTableCell94,
				this.xrTableCell95,
				this.lblSummaryTax2,
				this.xrTableCell97,
				this.xrTableCell98
			});
			this.xrTableRow21.Name = "xrTableRow21";
			this.xrTableRow21.Weight = 1.0;
			this.xrTableCell91.Name = "xrTableCell91";
			this.xrTableCell91.Padding = new PaddingInfo(10, 2, 3, 3, 100f);
			this.xrTableCell91.StylePriority.UsePadding = false;
			this.xrTableCell91.Weight = 1.752823830580536;
			this.xrTableCell92.Name = "xrTableCell92";
			this.xrTableCell92.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell92.StylePriority.UsePadding = false;
			this.xrTableCell92.StylePriority.UseTextAlignment = false;
			this.xrTableCell92.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell92.Weight = 0.8859357060943043;
			this.xrTableCell93.Name = "xrTableCell93";
			this.xrTableCell93.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell93.StylePriority.UsePadding = false;
			this.xrTableCell93.Weight = 0.5315614220100788;
			this.xrTableCell94.Name = "xrTableCell94";
			this.xrTableCell94.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell94.StylePriority.UsePadding = false;
			this.xrTableCell94.StylePriority.UseTextAlignment = false;
			this.xrTableCell94.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell94.Weight = 0.9302324668585246;
			this.xrTableCell95.Borders = BorderSide.None;
			this.xrTableCell95.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell95.ForeColor = Color.DarkRed;
			this.xrTableCell95.Name = "xrTableCell95";
			this.xrTableCell95.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell95.StylePriority.UseBorders = false;
			this.xrTableCell95.StylePriority.UseFont = false;
			this.xrTableCell95.StylePriority.UseForeColor = false;
			this.xrTableCell95.StylePriority.UsePadding = false;
			this.xrTableCell95.StylePriority.UseTextAlignment = false;
			this.xrTableCell95.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell95.Weight = 0.07973393942674822;
			this.lblSummaryTax2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe2Caption")
			});
			this.lblSummaryTax2.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummaryTax2.Name = "lblSummaryTax2";
			this.lblSummaryTax2.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummaryTax2.StylePriority.UseFont = false;
			this.lblSummaryTax2.StylePriority.UsePadding = false;
			this.lblSummaryTax2.StylePriority.UseTextAlignment = false;
			this.lblSummaryTax2.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryTax2.Weight = 1.9579181409783137;
			this.xrTableCell97.Borders = BorderSide.None;
			this.xrTableCell97.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.Taxe2Total")
			});
			this.xrTableCell97.Font = new Font("Tahoma", 8.5f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell97.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell97.Name = "xrTableCell97";
			this.xrTableCell97.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell97.StylePriority.UseBorders = false;
			this.xrTableCell97.StylePriority.UseFont = false;
			this.xrTableCell97.StylePriority.UseForeColor = false;
			this.xrTableCell97.StylePriority.UsePadding = false;
			this.xrTableCell97.StylePriority.UseTextAlignment = false;
			xrsummary17.FormatString = "{0:c}";
			this.xrTableCell97.Summary = xrsummary17;
			this.xrTableCell97.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell97.Weight = 1.1074195798819029;
			this.xrTableCell98.Borders = BorderSide.None;
			this.xrTableCell98.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell98.ForeColor = Color.DarkRed;
			this.xrTableCell98.Name = "xrTableCell98";
			this.xrTableCell98.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell98.StylePriority.UseBorders = false;
			this.xrTableCell98.StylePriority.UseFont = false;
			this.xrTableCell98.StylePriority.UseForeColor = false;
			this.xrTableCell98.StylePriority.UsePadding = false;
			this.xrTableCell98.StylePriority.UseTextAlignment = false;
			xrsummary18.Func = SummaryFunc.Custom;
			this.xrTableCell98.Summary = xrsummary18;
			this.xrTableCell98.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell98.Visible = false;
			this.xrTableCell98.Weight = 0.7543739676644248;
			this.SubBand12.Controls.AddRange(new XRControl[]
			{
				this.xrTable17
			});
			this.SubBand12.HeightF = 21f;
			this.SubBand12.Name = "SubBand12";
			this.xrTable17.Borders = BorderSide.None;
			this.xrTable17.BorderWidth = 3f;
			this.xrTable17.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable17.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable17.LocationFloat = new PointFloat(0f, 1f);
			this.xrTable17.Name = "xrTable17";
			this.xrTable17.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow22
			});
			this.xrTable17.SizeF = new SizeF(900f, 20f);
			this.xrTable17.StylePriority.UseBorders = false;
			this.xrTable17.StylePriority.UseBorderWidth = false;
			this.xrTable17.StylePriority.UseFont = false;
			this.xrTable17.StylePriority.UseForeColor = false;
			this.xrTable17.StylePriority.UseTextAlignment = false;
			this.xrTable17.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow22.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell99,
				this.xrTableCell100,
				this.xrTableCell101,
				this.xrTableCell102,
				this.xrTableCell103,
				this.lblSummaryGrandTotal,
				this.xrTableCell105,
				this.xrTableCell106
			});
			this.xrTableRow22.Name = "xrTableRow22";
			this.xrTableRow22.Weight = 1.0;
			this.xrTableCell99.Name = "xrTableCell99";
			this.xrTableCell99.Padding = new PaddingInfo(10, 2, 3, 3, 100f);
			this.xrTableCell99.StylePriority.UsePadding = false;
			this.xrTableCell99.Weight = 1.752823830580536;
			this.xrTableCell100.Name = "xrTableCell100";
			this.xrTableCell100.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell100.StylePriority.UsePadding = false;
			this.xrTableCell100.StylePriority.UseTextAlignment = false;
			this.xrTableCell100.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell100.Weight = 0.8859357060943043;
			this.xrTableCell101.Name = "xrTableCell101";
			this.xrTableCell101.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell101.StylePriority.UsePadding = false;
			this.xrTableCell101.Weight = 0.5315614220100788;
			this.xrTableCell102.Name = "xrTableCell102";
			this.xrTableCell102.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell102.StylePriority.UsePadding = false;
			this.xrTableCell102.StylePriority.UseTextAlignment = false;
			this.xrTableCell102.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell102.Weight = 0.9302324668585246;
			this.xrTableCell103.Borders = BorderSide.None;
			this.xrTableCell103.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell103.ForeColor = Color.DarkRed;
			this.xrTableCell103.Name = "xrTableCell103";
			this.xrTableCell103.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell103.StylePriority.UseBorders = false;
			this.xrTableCell103.StylePriority.UseFont = false;
			this.xrTableCell103.StylePriority.UseForeColor = false;
			this.xrTableCell103.StylePriority.UsePadding = false;
			this.xrTableCell103.StylePriority.UseTextAlignment = false;
			this.xrTableCell103.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell103.Weight = 0.07973420979285684;
			this.lblSummaryGrandTotal.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.lblSummaryGrandTotal.Name = "lblSummaryGrandTotal";
			this.lblSummaryGrandTotal.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.lblSummaryGrandTotal.StylePriority.UseFont = false;
			this.lblSummaryGrandTotal.StylePriority.UsePadding = false;
			this.lblSummaryGrandTotal.StylePriority.UseTextAlignment = false;
			this.lblSummaryGrandTotal.Text = "GRAND TOTAL :";
			this.lblSummaryGrandTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryGrandTotal.Weight = 1.9579178706122051;
			this.xrTableCell105.Borders = BorderSide.None;
			this.xrTableCell105.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Summaries.Summaries_Total.TotalAfterTaxes", "{0:#.00}")
			});
			this.xrTableCell105.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell105.ForeColor = Color.FromArgb(0, 96, 96);
			this.xrTableCell105.Name = "xrTableCell105";
			this.xrTableCell105.Padding = new PaddingInfo(3, 2, 0, 0, 100f);
			this.xrTableCell105.StylePriority.UseBorders = false;
			this.xrTableCell105.StylePriority.UseFont = false;
			this.xrTableCell105.StylePriority.UseForeColor = false;
			this.xrTableCell105.StylePriority.UsePadding = false;
			this.xrTableCell105.StylePriority.UseTextAlignment = false;
			xrsummary19.FormatString = "{0:c}";
			this.xrTableCell105.Summary = xrsummary19;
			this.xrTableCell105.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell105.Weight = 1.1074195798819029;
			this.xrTableCell106.Borders = BorderSide.None;
			this.xrTableCell106.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			this.xrTableCell106.ForeColor = Color.DarkRed;
			this.xrTableCell106.Name = "xrTableCell106";
			this.xrTableCell106.Padding = new PaddingInfo(1, 9, 0, 0, 100f);
			this.xrTableCell106.StylePriority.UseBorders = false;
			this.xrTableCell106.StylePriority.UseFont = false;
			this.xrTableCell106.StylePriority.UseForeColor = false;
			this.xrTableCell106.StylePriority.UsePadding = false;
			this.xrTableCell106.StylePriority.UseTextAlignment = false;
			xrsummary20.Func = SummaryFunc.Custom;
			this.xrTableCell106.Summary = xrsummary20;
			this.xrTableCell106.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell106.Visible = false;
			this.xrTableCell106.Weight = 0.7543739676644248;
			this.formattingRule19.Condition = "!IsNullOrEmpty([Name])";
			this.formattingRule19.DataMember = "PlanSummaries";
			this.formattingRule19.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule19.Name = "formattingRule19";
			this.formattingRule20.Condition = "[RawMarginSubTotal] > 0";
			this.formattingRule20.DataMember = "PlansSummaries.PlansSummaries_PlanSummaries.PlanSummaries_LayerSummaries.LayerSummaries_PlanSummary";
			this.formattingRule20.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule20.Name = "formattingRule20";
			base.Bands.AddRange(new Band[]
			{
				this.Detail,
				this.TopMargin,
				this.BottomMargin,
				this.DetailReport2,
				this.DetailReport
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
			this.XmlDataPath = "C:\\Users\\Patrick\\Documents\\Quoter Plan\\Mes rapports\\LE-PACIFIQUE-(11-Novembre-2014)-[Sommaires_par_plans].xml";
			((ISupportInitialize)this.xrTable2).EndInit();
			((ISupportInitialize)this.xrTable1).EndInit();
			((ISupportInitialize)this.xrTable4).EndInit();
			((ISupportInitialize)this.xrTable5).EndInit();
			((ISupportInitialize)this.xrTable6).EndInit();
			((ISupportInitialize)this.xrTable10).EndInit();
			((ISupportInitialize)this.xrTable9).EndInit();
			((ISupportInitialize)this.xrTable11).EndInit();
			((ISupportInitialize)this.xrTable18).EndInit();
			((ISupportInitialize)this.xrTable12).EndInit();
			((ISupportInitialize)this.xrTable13).EndInit();
			((ISupportInitialize)this.xrTable14).EndInit();
			((ISupportInitialize)this.xrTable7).EndInit();
			((ISupportInitialize)this.xrTable3).EndInit();
			((ISupportInitialize)this.xrTable8).EndInit();
			((ISupportInitialize)this.xrTable15).EndInit();
			((ISupportInitialize)this.xrTable16).EndInit();
			((ISupportInitialize)this.xrTable17).EndInit();
			((ISupportInitialize)this).EndInit();
		}

		private void LoadResources()
		{
			this.lblHeader1.Text = Resources.Rapport_d_estimation;
			this.lblHeader2.Text = ((this.project.Report.ReportSortBy == QuoterPlan.Report.ReportSortByEnum.ReportSortByPlans) ? Resources._par_plans_ : Resources._par_calques_);
			this.lbHeaderPageInfo.Format = Resources.Page_0_de_1_;
			this.lblFooter1.Text = Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan;
			this.lblFooter2.Text = Utilities.ApplicationWebsite;
			this.lbFooterlPageInfo.Format = Resources.Page_0_de_1_;
			this.lblPlanSummaryGroup.Text = Resources.Groupe;
			this.lblPlanSummaryBreakdown.Text = Resources.Répartition;
			this.lblPlanSummaryCostTotal.Text = Resources.Coûtant_total;
			this.lblPlanSummaryPriceTotal.Text = Resources.Prix_total;
			this.lblPlanSummaryMargin.Text = Resources.Marge;
			this.lblPlanSummarySubTotals.Text = Resources.Sous_totaux_;
			this.lblSummaryItem.Text = ((this.project.Report.ReportSortBy == QuoterPlan.Report.ReportSortByEnum.ReportSortByPlans) ? Resources.Plan : Resources.Calque);
			this.lblSummaryBreakdown.Text = Resources.Répartition;
			this.lblSummaryCostTotal.Text = Resources.Coûtant_total;
			this.lblSummaryPriceTotal.Text = Resources.Prix_total;
			this.lblSummaryMargin.Text = Resources.Marge;
			this.lblSummarySubTotals.Text = Resources.Sous_totaux_;
			this.lblSummaryGrandTotal.Text = Resources.GRAND_TOTAL_;
			if (this.project.Report.ReportSortBy == QuoterPlan.Report.ReportSortByEnum.ReportSortByPlans)
			{
				this.lblSummaryByPlans.Tag = " - " + Resources.SOMMAIRE;
			}
			else
			{
				this.lblSummaryByPlans.DataBindings.Clear();
				this.lblSummaryByPlans.Text = Resources.SOMMAIRE_PAR_CALQUES;
				this.lblSummaryByPlans.Tag = "";
			}
			this.lblSummaryTotals.Text = Resources.SOMMAIRE_DES_TOTAUX;
		}

		public XtraReportEstimatingTotalsByPlans(Project project)
		{
			this.project = project;
			this.InitializeComponent();
			this.LoadResources();
		}

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private XRPageInfo lbFooterlPageInfo;

		private DetailReportBand DetailReport2;

		private DetailBand Detail3;

		private FormattingRule formattingRule1;

		private FormattingRule formattingRule3;

		private FormattingRule formattingRule2;

		private SubBand SubBand1;

		private XRTable xrTable4;

		private XRTableRow xrTableRow7;

		private XRTableCell xrTableCell7;

		private XRTableRow xrTableRow8;

		private XRTableCell xrTableCell8;

		private XRTable xrTable2;

		private XRTableRow xrTableRow3;

		private XRTableCell xrTableCell3;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell4;

		private XRTable xrTable6;

		private XRTableRow xrTableRow11;

		private XRTableCell xrTableCell11;

		private XRTableRow xrTableRow12;

		private XRTableCell xrTableCell12;

		private XRTable xrTable5;

		private XRTableRow xrTableRow9;

		private XRTableCell xrTableCell9;

		private XRTableRow xrTableRow10;

		private XRTableCell xrTableCell10;

		private FormattingRule formattingRule4;

		private FormattingRule formattingRule5;

		private FormattingRule formattingRule6;

		private FormattingRule formattingRule7;

		private FormattingRule formattingRule8;

		private XRLabel lblFooter2;

		private XRLabel lblFooter1;

		private XRLabel lbHeaderTitle;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell xrTableCell1;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell2;

		private XRLabel lblHeader1;

		private XRPageInfo lbHeaderPageInfo;

		private XRLabel lblHeader2;

		private SubBand SubBand2;

		private SubBand SubBand3;

		private SubBand SubBand4;

		private SubBand SubBand5;

		private XRLabel xrLabel20;

		private FormattingRule formattingRule9;

		private SubBand SubBand7;

		private XRLabel lbFooterDate;

		private FormattingRule formattingRule10;

		private FormattingRule formattingRule11;

		private FormattingRule formattingRule12;

		private FormattingRule formattingRule13;

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

		private XRTableCell xrTableCell64;

		private XRTableCell xrTableCell65;

		private XRTableCell xrTableCell66;

		private XRTable xrTable13;

		private XRTableRow xrTableRow18;

		private XRTableCell xrTableCell67;

		private XRTableCell xrTableCell68;

		private XRTableCell xrTableCell69;

		private XRTableCell xrTableCell70;

		private XRTableCell xrTableCell71;

		private XRTableCell xrTableCell72;

		private XRTableCell xrTableCell73;

		private XRTableCell xrTableCell74;

		private XRTable xrTable14;

		private XRTableRow xrTableRow19;

		private XRTableCell xrTableCell75;

		private XRTableCell xrTableCell76;

		private XRTableCell xrTableCell77;

		private XRTableCell xrTableCell78;

		private XRTableCell xrTableCell79;

		private XRTableCell xrTableCell80;

		private XRTableCell xrTableCell81;

		private XRTableCell xrTableCell82;

		private FormattingRule formattingRule14;

		private FormattingRule formattingRule15;

		private DetailReportBand DetailReport3;

		private DetailBand Detail4;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLine xrLine2;

		private XRTable xrTable7;

		private XRTableRow xrTableRow6;

		private XRTableCell xrTableCell19;

		private XRTableCell xrTableCell20;

		private XRTableCell xrTableCell21;

		private XRLabel xrLabel2;

		private XRTableCell xrTableCell22;

		private XRTableCell xrTableCell23;

		private XRTableCell xrTableCell24;

		private XRTableCell xrTableCell25;

		private XRTableCell xrTableCell26;

		private GroupHeaderBand GroupHeader1;

		private XRTable xrTable3;

		private XRTableRow xrTableRow5;

		private XRTableCell lblSummaryItem;

		private XRTableCell xrTableCell6;

		private XRTableCell lblSummaryBreakdown;

		private XRTableCell xrTableCell14;

		private XRTableCell lblSummaryCostTotal;

		private XRTableCell xrTableCell16;

		private XRTableCell lblSummaryPriceTotal;

		private XRTableCell lblSummaryMargin;

		private GroupFooterBand GroupFooter1;

		private XRTable xrTable8;

		private XRTableRow xrTableRow13;

		private XRTableCell xrTableCell27;

		private XRTableCell xrTableCell28;

		private XRTableCell xrTableCell29;

		private XRTableCell lblSummarySubTotals;

		private XRTableCell xrTableCell31;

		private XRTableCell xrTableCell32;

		private XRTableCell xrTableCell33;

		private XRTableCell xrTableCell34;

		private SubBand SubBand10;

		private XRTable xrTable15;

		private XRTableRow xrTableRow20;

		private XRTableCell xrTableCell83;

		private XRTableCell xrTableCell84;

		private XRTableCell xrTableCell85;

		private XRTableCell xrTableCell86;

		private XRTableCell xrTableCell87;

		private XRTableCell lblSummaryTax1;

		private XRTableCell xrTableCell89;

		private XRTableCell xrTableCell90;

		private SubBand SubBand11;

		private XRTable xrTable16;

		private XRTableRow xrTableRow21;

		private XRTableCell xrTableCell91;

		private XRTableCell xrTableCell92;

		private XRTableCell xrTableCell93;

		private XRTableCell xrTableCell94;

		private XRTableCell xrTableCell95;

		private XRTableCell lblSummaryTax2;

		private XRTableCell xrTableCell97;

		private XRTableCell xrTableCell98;

		private SubBand SubBand12;

		private XRTable xrTable17;

		private XRTableRow xrTableRow22;

		private XRTableCell xrTableCell99;

		private XRTableCell xrTableCell100;

		private XRTableCell xrTableCell101;

		private XRTableCell xrTableCell102;

		private XRTableCell xrTableCell103;

		private XRTableCell lblSummaryGrandTotal;

		private XRTableCell xrTableCell105;

		private XRTableCell xrTableCell106;

		private XRLabel lblSummaryByPlans;

		private XRTable xrTable9;

		private XRTableRow xrTableRow14;

		private XRTableCell lblPlanSummaryGroup;

		private XRTableCell xrTableCell39;

		private XRTableCell lblPlanSummaryBreakdown;

		private XRTableCell xrTableCell42;

		private XRTableCell lblPlanSummaryCostTotal;

		private XRTableCell xrTableCell48;

		private XRTableCell lblPlanSummaryPriceTotal;

		private XRTableCell lblPlanSummaryMargin;

		private DetailReportBand DetailReport5;

		private DetailBand Detail6;

		private XRLabel xrLabel5;

		private DetailReportBand DetailReport6;

		private DetailBand Detail7;

		private XRLine xrLine3;

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

		private XRLabel lblSummaryTotals;

		private GroupHeaderBand GroupHeader2;

		private ReportFooterBand ReportFooter;

		private XRTable xrTable11;

		private XRTableRow xrTableRow16;

		private XRTableCell xrTableCell51;

		private XRTableCell xrTableCell52;

		private XRTableCell xrTableCell53;

		private XRTableCell lblPlanSummarySubTotals;

		private XRTableCell xrTableCell55;

		private XRTableCell xrTableCell56;

		private XRTableCell xrTableCell57;

		private XRTableCell xrTableCell58;

		private XRLabel xrLabel14;

		private FormattingRule formattingRule16;

		private FormattingRule formattingRule17;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRTable xrTable18;

		private XRTableRow xrTableRow23;

		private XRTableCell xrTableCell107;

		private XRTableCell xrTableCell108;

		private XRTableCell xrTableCell109;

		private XRTableCell xrTableCell111;

		private XRTableCell xrTableCell112;

		private XRTableCell xrTableCell113;

		private XRTableCell xrTableCell114;

		private FormattingRule formattingRule18;

		private FormattingRule formattingRule19;

		private XRTableCell lblPlanSummaryTotals;

		private FormattingRule formattingRule20;

		private Project project;
	}
}
