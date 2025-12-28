using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class XtraReportQuote : XtraReport
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
			this.Detail = new DetailBand();
			this.TopMargin = new TopMarginBand();
			this.lbHeaderTitle = new XRLabel();
			this.lblHeader1 = new XRLabel();
			this.lbHeaderPageInfo = new XRPageInfo();
			this.BottomMargin = new BottomMarginBand();
			this.lblFooter2 = new XRLabel();
			this.lblFooter1 = new XRLabel();
			this.lbFooterlPageInfo = new XRPageInfo();
			this.lbFooterDate = new XRLabel();
			this.ReportHeader = new ReportHeaderBand();
			this.SubBand1 = new SubBand();
			this.xrTable2 = new XRTable();
			this.xrTableRow3 = new XRTableRow();
			this.lblQuote = new XRTableCell();
			this.xrTableRow4 = new XRTableRow();
			this.lblQuoteNumber = new XRTableCell();
			this.xrTableCell19 = new XRTableCell();
			this.xrTableRow19 = new XRTableRow();
			this.lblQuoteDate = new XRTableCell();
			this.xrTableCell32 = new XRTableCell();
			this.xrTableRow18 = new XRTableRow();
			this.lblQuoteContact = new XRTableCell();
			this.xrTableCell28 = new XRTableCell();
			this.xrTable4 = new XRTable();
			this.xrTableRow7 = new XRTableRow();
			this.lblCompany = new XRTableCell();
			this.xrTableRow8 = new XRTableRow();
			this.xrTableCell8 = new XRTableCell();
			this.SubBand2 = new SubBand();
			this.xrTable1 = new XRTable();
			this.xrTableRow1 = new XRTableRow();
			this.lblProjectInfo = new XRTableCell();
			this.xrTableRow2 = new XRTableRow();
			this.xrTableCell4 = new XRTableCell();
			this.xrTable5 = new XRTable();
			this.xrTableRow9 = new XRTableRow();
			this.lblContactInfo = new XRTableCell();
			this.xrTableRow10 = new XRTableRow();
			this.xrTableCell10 = new XRTableCell();
			this.xrTable15 = new XRTable();
			this.xrTableRow23 = new XRTableRow();
			this.lblQuoteTotalDescription = new XRTableCell();
			this.lblQuoteTotal = new XRTableCell();
			this.formattingRule8 = new FormattingRule();
			this.xrTable6 = new XRTable();
			this.xrTableRow11 = new XRTableRow();
			this.lblComments = new XRTableCell();
			this.xrTableRow12 = new XRTableRow();
			this.xrTableCell12 = new XRTableCell();
			this.formattingRule4 = new FormattingRule();
			this.formattingRule6 = new FormattingRule();
			this.formattingRule7 = new FormattingRule();
			this.DetailReport2 = new DetailReportBand();
			this.Detail3 = new DetailBand();
			this.xrTable9 = new XRTable();
			this.xrTableRow14 = new XRTableRow();
			this.lblItem = new XRTableCell();
			this.lblQuantity = new XRTableCell();
			this.lblUnit = new XRTableCell();
			this.lblPrice = new XRTableCell();
			this.lblPriceTotal = new XRTableCell();
			this.DetailReport = new DetailReportBand();
			this.Detail1 = new DetailBand();
			this.xrLine1 = new XRLine();
			this.xrTable3 = new XRTable();
			this.xrTableRow5 = new XRTableRow();
			this.xrTableCell1 = new XRTableCell();
			this.xrTableCell3 = new XRTableCell();
			this.xrTableCell5 = new XRTableCell();
			this.xrTableCell6 = new XRTableCell();
			this.xrTableCell7 = new XRTableCell();
			this.GroupFooter1 = new GroupFooterBand();
			this.xrTable7 = new XRTable();
			this.xrTableRow6 = new XRTableRow();
			this.xrTableCell9 = new XRTableCell();
			this.xrTableCell11 = new XRTableCell();
			this.xrTableCell13 = new XRTableCell();
			this.xrTableCell14 = new XRTableCell();
			this.xrTableCell15 = new XRTableCell();
			this.DetailReport4 = new DetailReportBand();
			this.Detail5 = new DetailBand();
			this.formattingRule9 = new FormattingRule();
			this.formattingRule1 = new FormattingRule();
			this.formattingRule3 = new FormattingRule();
			this.formattingRule2 = new FormattingRule();
			this.formattingRule5 = new FormattingRule();
			this.xrTable8 = new XRTable();
			this.xrTableRow13 = new XRTableRow();
			this.xrTableCell16 = new XRTableCell();
			this.xrTableCell17 = new XRTableCell();
			this.lblSummaryTax1 = new XRTableCell();
			this.xrTableCell20 = new XRTableCell();
			this.xrTable10 = new XRTable();
			this.xrTableRow15 = new XRTableRow();
			this.xrTableCell21 = new XRTableCell();
			this.xrTableCell22 = new XRTableCell();
			this.lblSummaryTax2 = new XRTableCell();
			this.xrTableCell25 = new XRTableCell();
			this.xrTable11 = new XRTable();
			this.xrTableRow16 = new XRTableRow();
			this.xrTableCell26 = new XRTableCell();
			this.xrTableCell27 = new XRTableCell();
			this.lblSummaryTotalAmontDue = new XRTableCell();
			this.xrTableCell30 = new XRTableCell();
			this.xrLine2 = new XRLine();
			this.DetailReport1 = new DetailReportBand();
			this.Detail2 = new DetailBand();
			this.xrTable12 = new XRTable();
			this.xrTableRow17 = new XRTableRow();
			this.xrTableCell18 = new XRTableCell();
			this.xrTableCell23 = new XRTableCell();
			this.lblSummarySubtotal = new XRTableCell();
			this.xrTableCell31 = new XRTableCell();
			this.xrLine3 = new XRLine();
			this.GroupFooter2 = new GroupFooterBand();
			this.GroupFooter3 = new GroupFooterBand();
			this.xrLabel1 = new XRLabel();
			this.DetailReport3 = new DetailReportBand();
			this.Detail4 = new DetailBand();
			this.DetailReport6 = new DetailReportBand();
			this.Detail7 = new DetailBand();
			this.xrTable13 = new XRTable();
			this.xrTableRow20 = new XRTableRow();
			this.xrTableCell2 = new XRTableCell();
			this.xrTableCell34 = new XRTableCell();
			this.xrLine4 = new XRLine();
			this.DetailReport5 = new DetailReportBand();
			this.Detail6 = new DetailBand();
			this.formattingRule10 = new FormattingRule();
			this.SubBand3 = new SubBand();
			this.SubBand4 = new SubBand();
			this.SubBand5 = new SubBand();
			this.formattingRule11 = new FormattingRule();
			this.formattingRule12 = new FormattingRule();
			((ISupportInitialize)this.xrTable2).BeginInit();
			((ISupportInitialize)this.xrTable4).BeginInit();
			((ISupportInitialize)this.xrTable1).BeginInit();
			((ISupportInitialize)this.xrTable5).BeginInit();
			((ISupportInitialize)this.xrTable15).BeginInit();
			((ISupportInitialize)this.xrTable6).BeginInit();
			((ISupportInitialize)this.xrTable9).BeginInit();
			((ISupportInitialize)this.xrTable3).BeginInit();
			((ISupportInitialize)this.xrTable7).BeginInit();
			((ISupportInitialize)this.xrTable8).BeginInit();
			((ISupportInitialize)this.xrTable10).BeginInit();
			((ISupportInitialize)this.xrTable11).BeginInit();
			((ISupportInitialize)this.xrTable12).BeginInit();
			((ISupportInitialize)this.xrTable13).BeginInit();
			((ISupportInitialize)this).BeginInit();
			this.Detail.HeightF = 0f;
			this.Detail.Name = "Detail";
			this.Detail.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.Detail.TextAlignment = TextAlignment.TopLeft;
			this.TopMargin.Controls.AddRange(new XRControl[]
			{
				this.lbHeaderTitle,
				this.lblHeader1,
				this.lbHeaderPageInfo
			});
			this.TopMargin.HeightF = 100f;
			this.TopMargin.Name = "TopMargin";
			this.TopMargin.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.TopMargin.TextAlignment = TextAlignment.TopLeft;
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
			this.lbHeaderTitle.Visible = false;
			this.lblHeader1.BorderColor = Color.Black;
			this.lblHeader1.Borders = BorderSide.None;
			this.lblHeader1.BorderWidth = 1f;
			this.lblHeader1.Font = new Font("Tahoma", 8.25f, FontStyle.Underline, GraphicsUnit.Point, 0);
			this.lblHeader1.LocationFloat = new PointFloat(527.7065f, 37.5f);
			this.lblHeader1.Name = "lblHeader1";
			this.lblHeader1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblHeader1.SizeF = new SizeF(122.2933f, 17f);
			this.lblHeader1.StylePriority.UseBorderColor = false;
			this.lblHeader1.StylePriority.UseBorders = false;
			this.lblHeader1.StylePriority.UseBorderWidth = false;
			this.lblHeader1.StylePriority.UseFont = false;
			this.lblHeader1.StylePriority.UsePadding = false;
			this.lblHeader1.StylePriority.UseTextAlignment = false;
			this.lblHeader1.Text = "Devis";
			this.lblHeader1.TextAlignment = TextAlignment.MiddleRight;
			this.lblHeader1.Visible = false;
			this.lbHeaderPageInfo.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbHeaderPageInfo.Format = "Page {0} de {1}";
			this.lbHeaderPageInfo.LocationFloat = new PointFloat(498.9998f, 60.38541f);
			this.lbHeaderPageInfo.Name = "lbHeaderPageInfo";
			this.lbHeaderPageInfo.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lbHeaderPageInfo.SizeF = new SizeF(151f, 17f);
			this.lbHeaderPageInfo.StylePriority.UseFont = false;
			this.lbHeaderPageInfo.StylePriority.UseTextAlignment = false;
			this.lbHeaderPageInfo.Text = "Total";
			this.lbHeaderPageInfo.TextAlignment = TextAlignment.MiddleRight;
			this.lbHeaderPageInfo.Visible = false;
			this.BottomMargin.Controls.AddRange(new XRControl[]
			{
				this.lblFooter2,
				this.lblFooter1,
				this.lbFooterlPageInfo,
				this.lbFooterDate
			});
			this.BottomMargin.HeightF = 100f;
			this.BottomMargin.Name = "BottomMargin";
			this.BottomMargin.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.BottomMargin.TextAlignment = TextAlignment.TopLeft;
			this.lblFooter2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblFooter2.LocationFloat = new PointFloat(197.855f, 49.99997f);
			this.lblFooter2.Name = "lblFooter2";
			this.lblFooter2.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblFooter2.SizeF = new SizeF(257.29f, 15f);
			this.lblFooter2.StylePriority.UseFont = false;
			this.lblFooter2.StylePriority.UseTextAlignment = false;
			this.lblFooter2.Text = "www.quoterplan.com";
			this.lblFooter2.TextAlignment = TextAlignment.MiddleCenter;
			this.lblFooter2.Visible = false;
			this.lblFooter1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblFooter1.LocationFloat = new PointFloat(197.855f, 35.00003f);
			this.lblFooter1.Name = "lblFooter1";
			this.lblFooter1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lblFooter1.SizeF = new SizeF(257.29f, 15f);
			this.lblFooter1.StylePriority.UseFont = false;
			this.lblFooter1.StylePriority.UseTextAlignment = false;
			this.lblFooter1.Text = "Ce rapport a été généré grâce à Quoter Plan";
			this.lblFooter1.TextAlignment = TextAlignment.MiddleCenter;
			this.lblFooter1.Visible = false;
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
			this.lbFooterDate.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Report.Date")
			});
			this.lbFooterDate.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbFooterDate.LocationFloat = new PointFloat(0.0001271566f, 41.5f);
			this.lbFooterDate.Name = "lbFooterDate";
			this.lbFooterDate.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.lbFooterDate.SizeF = new SizeF(186.46f, 17f);
			this.lbFooterDate.StylePriority.UseFont = false;
			this.lbFooterDate.StylePriority.UseTextAlignment = false;
			this.lbFooterDate.TextAlignment = TextAlignment.MiddleLeft;
			this.ReportHeader.HeightF = 2.083333f;
			this.ReportHeader.Name = "ReportHeader";
			this.ReportHeader.SubBands.AddRange(new SubBand[]
			{
				this.SubBand1,
				this.SubBand2
			});
			this.SubBand1.Controls.AddRange(new XRControl[]
			{
				this.xrTable2,
				this.xrTable4
			});
			this.SubBand1.HeightF = 161.125f;
			this.SubBand1.Name = "SubBand1";
			this.xrTable2.BorderColor = Color.SteelBlue;
			this.xrTable2.Borders = BorderSide.None;
			this.xrTable2.BorderWidth = 2f;
			this.xrTable2.LocationFloat = new PointFloat(349.5001f, 0f);
			this.xrTable2.Name = "xrTable2";
			this.xrTable2.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow3,
				this.xrTableRow4,
				this.xrTableRow19,
				this.xrTableRow18
			});
			this.xrTable2.SizeF = new SizeF(301.4998f, 129.0682f);
			this.xrTable2.StylePriority.UseBorderColor = false;
			this.xrTable2.StylePriority.UseBorders = false;
			this.xrTable2.StylePriority.UseBorderWidth = false;
			this.xrTableRow3.Cells.AddRange(new XRTableCell[]
			{
				this.lblQuote
			});
			this.xrTableRow3.Name = "xrTableRow3";
			this.xrTableRow3.Weight = 0.4056783960521852;
			this.lblQuote.BackColor = Color.Transparent;
			this.lblQuote.BorderColor = Color.White;
			this.lblQuote.Borders = BorderSide.None;
			this.lblQuote.Font = new Font("Tahoma", 32f, FontStyle.Bold);
			this.lblQuote.ForeColor = Color.SteelBlue;
			this.lblQuote.Name = "lblQuote";
			this.lblQuote.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.lblQuote.StylePriority.UseBackColor = false;
			this.lblQuote.StylePriority.UseBorderColor = false;
			this.lblQuote.StylePriority.UseFont = false;
			this.lblQuote.StylePriority.UseForeColor = false;
			this.lblQuote.StylePriority.UsePadding = false;
			this.lblQuote.StylePriority.UseTextAlignment = false;
			this.lblQuote.Text = "DEVIS";
			this.lblQuote.TextAlignment = TextAlignment.TopRight;
			this.lblQuote.Weight = 1.0;
			this.xrTableRow4.Cells.AddRange(new XRTableCell[]
			{
				this.lblQuoteNumber,
				this.xrTableCell19
			});
			this.xrTableRow4.Name = "xrTableRow4";
			this.xrTableRow4.Weight = 0.14151231919976612;
			this.lblQuoteNumber.Font = new Font("Tahoma", 9.75f, FontStyle.Bold);
			this.lblQuoteNumber.Name = "lblQuoteNumber";
			this.lblQuoteNumber.Padding = new PaddingInfo(10, 5, 0, 0, 100f);
			this.lblQuoteNumber.StylePriority.UseFont = false;
			this.lblQuoteNumber.StylePriority.UsePadding = false;
			this.lblQuoteNumber.StylePriority.UseTextAlignment = false;
			this.lblQuoteNumber.Text = "Projet #";
			this.lblQuoteNumber.TextAlignment = TextAlignment.MiddleRight;
			this.lblQuoteNumber.Weight = 0.5016625842565635;
			this.xrTableCell19.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Quote.QuoteNumber")
			});
			this.xrTableCell19.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTableCell19.Multiline = true;
			this.xrTableCell19.Name = "xrTableCell19";
			this.xrTableCell19.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell19.StylePriority.UseBorderColor = false;
			this.xrTableCell19.StylePriority.UseFont = false;
			this.xrTableCell19.StylePriority.UsePadding = false;
			this.xrTableCell19.StylePriority.UseTextAlignment = false;
			this.xrTableCell19.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell19.Weight = 0.44068833965930587;
			this.xrTableRow19.Cells.AddRange(new XRTableCell[]
			{
				this.lblQuoteDate,
				this.xrTableCell32
			});
			this.xrTableRow19.Name = "xrTableRow19";
			this.xrTableRow19.Weight = 0.14151232229200997;
			this.lblQuoteDate.Font = new Font("Tahoma", 9.75f, FontStyle.Bold);
			this.lblQuoteDate.Name = "lblQuoteDate";
			this.lblQuoteDate.Padding = new PaddingInfo(10, 5, 0, 0, 100f);
			this.lblQuoteDate.StylePriority.UseFont = false;
			this.lblQuoteDate.StylePriority.UsePadding = false;
			this.lblQuoteDate.StylePriority.UseTextAlignment = false;
			this.lblQuoteDate.Text = "Date :";
			this.lblQuoteDate.TextAlignment = TextAlignment.MiddleRight;
			this.lblQuoteDate.Weight = 0.5016625764530398;
			this.xrTableCell32.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Quote.QuoteDate")
			});
			this.xrTableCell32.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTableCell32.Name = "xrTableCell32";
			this.xrTableCell32.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell32.StylePriority.UseFont = false;
			this.xrTableCell32.StylePriority.UsePadding = false;
			this.xrTableCell32.StylePriority.UseTextAlignment = false;
			this.xrTableCell32.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell32.Weight = 0.44068834746282953;
			this.xrTableRow18.Cells.AddRange(new XRTableCell[]
			{
				this.lblQuoteContact,
				this.xrTableCell28
			});
			this.xrTableRow18.Name = "xrTableRow18";
			this.xrTableRow18.Weight = 0.14151229419055783;
			this.lblQuoteContact.Font = new Font("Tahoma", 9.75f, FontStyle.Bold);
			this.lblQuoteContact.Name = "lblQuoteContact";
			this.lblQuoteContact.Padding = new PaddingInfo(10, 5, 0, 0, 100f);
			this.lblQuoteContact.StylePriority.UseFont = false;
			this.lblQuoteContact.StylePriority.UsePadding = false;
			this.lblQuoteContact.StylePriority.UseTextAlignment = false;
			this.lblQuoteContact.Text = "Représentant :";
			this.lblQuoteContact.TextAlignment = TextAlignment.MiddleRight;
			this.lblQuoteContact.Weight = 0.5016624847028457;
			this.xrTableCell28.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Quote.QuoteRepresentative")
			});
			this.xrTableCell28.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTableCell28.Name = "xrTableCell28";
			this.xrTableCell28.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell28.StylePriority.UseFont = false;
			this.xrTableCell28.StylePriority.UsePadding = false;
			this.xrTableCell28.StylePriority.UseTextAlignment = false;
			this.xrTableCell28.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell28.Weight = 0.4406884392130236;
			this.xrTable4.BorderColor = Color.SteelBlue;
			this.xrTable4.Borders = BorderSide.None;
			this.xrTable4.BorderWidth = 2f;
			this.xrTable4.LocationFloat = new PointFloat(0f, 0f);
			this.xrTable4.Name = "xrTable4";
			this.xrTable4.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow7,
				this.xrTableRow8
			});
			this.xrTable4.SizeF = new SizeF(300.5f, 50f);
			this.xrTable4.StylePriority.UseBorderColor = false;
			this.xrTable4.StylePriority.UseBorders = false;
			this.xrTable4.StylePriority.UseBorderWidth = false;
			this.xrTableRow7.Cells.AddRange(new XRTableCell[]
			{
				this.lblCompany
			});
			this.xrTableRow7.Name = "xrTableRow7";
			this.xrTableRow7.Weight = 0.736;
			this.lblCompany.BackColor = Color.Transparent;
			this.lblCompany.BorderColor = Color.White;
			this.lblCompany.Borders = BorderSide.All;
			this.lblCompany.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Company.CompanyName")
			});
			this.lblCompany.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblCompany.ForeColor = Color.Black;
			this.lblCompany.Name = "lblCompany";
			this.lblCompany.Padding = new PaddingInfo(8, 0, 0, 0, 100f);
			this.lblCompany.StylePriority.UseBackColor = false;
			this.lblCompany.StylePriority.UseBorderColor = false;
			this.lblCompany.StylePriority.UseBorders = false;
			this.lblCompany.StylePriority.UseFont = false;
			this.lblCompany.StylePriority.UseForeColor = false;
			this.lblCompany.StylePriority.UsePadding = false;
			this.lblCompany.TextAlignment = TextAlignment.MiddleLeft;
			this.lblCompany.Weight = 1.0;
			this.xrTableRow8.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell8
			});
			this.xrTableRow8.Name = "xrTableRow8";
			this.xrTableRow8.Weight = 0.8640000000000001;
			this.xrTableCell8.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Company.CompanyInfo")
			});
			this.xrTableCell8.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTableCell8.Multiline = true;
			this.xrTableCell8.Name = "xrTableCell8";
			this.xrTableCell8.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell8.StylePriority.UseBorderColor = false;
			this.xrTableCell8.StylePriority.UseBorders = false;
			this.xrTableCell8.StylePriority.UseBorderWidth = false;
			this.xrTableCell8.StylePriority.UseFont = false;
			this.xrTableCell8.StylePriority.UsePadding = false;
			this.xrTableCell8.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell8.Weight = 1.0;
			this.SubBand2.Controls.AddRange(new XRControl[]
			{
				this.xrTable1,
				this.xrTable5
			});
			this.SubBand2.HeightF = 90f;
			this.SubBand2.Name = "SubBand2";
			this.xrTable1.BorderColor = Color.SteelBlue;
			this.xrTable1.Borders = BorderSide.None;
			this.xrTable1.BorderWidth = 2f;
			this.xrTable1.LocationFloat = new PointFloat(1.004028f, 0f);
			this.xrTable1.Name = "xrTable1";
			this.xrTable1.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow1,
				this.xrTableRow2
			});
			this.xrTable1.SizeF = new SizeF(300.5f, 50f);
			this.xrTable1.StylePriority.UseBorderColor = false;
			this.xrTable1.StylePriority.UseBorders = false;
			this.xrTable1.StylePriority.UseBorderWidth = false;
			this.xrTableRow1.Cells.AddRange(new XRTableCell[]
			{
				this.lblProjectInfo
			});
			this.xrTableRow1.Name = "xrTableRow1";
			this.xrTableRow1.Weight = 0.7360000000000001;
			this.lblProjectInfo.BackColor = Color.Empty;
			this.lblProjectInfo.BorderColor = Color.White;
			this.lblProjectInfo.Borders = BorderSide.None;
			this.lblProjectInfo.Font = new Font("Tahoma", 9.75f, FontStyle.Bold);
			this.lblProjectInfo.ForeColor = Color.Black;
			this.lblProjectInfo.Name = "lblProjectInfo";
			this.lblProjectInfo.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.lblProjectInfo.StylePriority.UseBackColor = false;
			this.lblProjectInfo.StylePriority.UseFont = false;
			this.lblProjectInfo.StylePriority.UseForeColor = false;
			this.lblProjectInfo.StylePriority.UsePadding = false;
			this.lblProjectInfo.Text = "Project";
			this.lblProjectInfo.TextAlignment = TextAlignment.MiddleLeft;
			this.lblProjectInfo.Weight = 1.0;
			this.xrTableRow2.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell4
			});
			this.xrTableRow2.Name = "xrTableRow2";
			this.xrTableRow2.Weight = 0.8639999999999998;
			this.xrTableCell4.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.ProjectInfo")
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
			this.xrTable5.BorderColor = Color.SteelBlue;
			this.xrTable5.Borders = BorderSide.None;
			this.xrTable5.BorderWidth = 2f;
			this.xrTable5.LocationFloat = new PointFloat(350.0001f, 0f);
			this.xrTable5.Name = "xrTable5";
			this.xrTable5.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow9,
				this.xrTableRow10
			});
			this.xrTable5.SizeF = new SizeF(300f, 50f);
			this.xrTable5.StylePriority.UseBorderColor = false;
			this.xrTable5.StylePriority.UseBorders = false;
			this.xrTable5.StylePriority.UseBorderWidth = false;
			this.xrTableRow9.Cells.AddRange(new XRTableCell[]
			{
				this.lblContactInfo
			});
			this.xrTableRow9.Name = "xrTableRow9";
			this.xrTableRow9.Weight = 0.7360000000000001;
			this.lblContactInfo.BackColor = Color.Empty;
			this.lblContactInfo.BorderColor = Color.White;
			this.lblContactInfo.Borders = BorderSide.None;
			this.lblContactInfo.Font = new Font("Tahoma", 9.75f, FontStyle.Bold);
			this.lblContactInfo.ForeColor = Color.Black;
			this.lblContactInfo.Name = "lblContactInfo";
			this.lblContactInfo.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.lblContactInfo.StylePriority.UseBackColor = false;
			this.lblContactInfo.StylePriority.UseFont = false;
			this.lblContactInfo.StylePriority.UseForeColor = false;
			this.lblContactInfo.StylePriority.UsePadding = false;
			this.lblContactInfo.Text = "Client";
			this.lblContactInfo.TextAlignment = TextAlignment.MiddleLeft;
			this.lblContactInfo.Weight = 1.0;
			this.xrTableRow10.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell10
			});
			this.xrTableRow10.Name = "xrTableRow10";
			this.xrTableRow10.Weight = 0.8639999999999998;
			this.xrTableCell10.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.ContactInfo")
			});
			this.xrTableCell10.Font = new Font("Tahoma", 9.75f);
			this.xrTableCell10.Multiline = true;
			this.xrTableCell10.Name = "xrTableCell10";
			this.xrTableCell10.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell10.StylePriority.UseBorderColor = false;
			this.xrTableCell10.StylePriority.UseFont = false;
			this.xrTableCell10.StylePriority.UsePadding = false;
			this.xrTableCell10.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell10.Weight = 1.0;
			this.xrTable15.BackColor = Color.SteelBlue;
			this.xrTable15.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.xrTable15.ForeColor = Color.White;
			this.xrTable15.LocationFloat = new PointFloat(0f, 8f);
			this.xrTable15.Name = "xrTable15";
			this.xrTable15.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable15.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow23
			});
			this.xrTable15.SizeF = new SizeF(649.996f, 25f);
			this.xrTable15.StylePriority.UseBackColor = false;
			this.xrTable15.StylePriority.UseFont = false;
			this.xrTable15.StylePriority.UseForeColor = false;
			this.xrTable15.StylePriority.UsePadding = false;
			this.xrTableRow23.Cells.AddRange(new XRTableCell[]
			{
				this.lblQuoteTotalDescription,
				this.lblQuoteTotal
			});
			this.xrTableRow23.Name = "xrTableRow23";
			this.xrTableRow23.Weight = 1.0869565217391304;
			this.lblQuoteTotalDescription.Name = "lblQuoteTotalDescription";
			this.lblQuoteTotalDescription.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblQuoteTotalDescription.StylePriority.UsePadding = false;
			this.lblQuoteTotalDescription.StylePriority.UseTextAlignment = false;
			this.lblQuoteTotalDescription.Text = "Description";
			this.lblQuoteTotalDescription.TextAlignment = TextAlignment.MiddleLeft;
			this.lblQuoteTotalDescription.Weight = 4.43351133631049;
			this.lblQuoteTotal.Name = "lblQuoteTotal";
			this.lblQuoteTotal.Padding = new PaddingInfo(3, 10, 0, 0, 100f);
			this.lblQuoteTotal.StylePriority.UsePadding = false;
			this.lblQuoteTotal.StylePriority.UseTextAlignment = false;
			this.lblQuoteTotal.Text = "Total";
			this.lblQuoteTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblQuoteTotal.Weight = 1.353213820794502;
			this.formattingRule8.Condition = "IsNullOrEmpty([Comment])";
			this.formattingRule8.DataMember = "Project";
			this.formattingRule8.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule8.Name = "formattingRule8";
			this.xrTable6.BorderColor = Color.SteelBlue;
			this.xrTable6.Borders = BorderSide.None;
			this.xrTable6.BorderWidth = 2f;
			this.xrTable6.LocationFloat = new PointFloat(1.004028f, 0f);
			this.xrTable6.Name = "xrTable6";
			this.xrTable6.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow11,
				this.xrTableRow12
			});
			this.xrTable6.SizeF = new SizeF(649.996f, 50f);
			this.xrTable6.StylePriority.UseBorderColor = false;
			this.xrTable6.StylePriority.UseBorders = false;
			this.xrTable6.StylePriority.UseBorderWidth = false;
			this.xrTableRow11.Cells.AddRange(new XRTableCell[]
			{
				this.lblComments
			});
			this.xrTableRow11.Name = "xrTableRow11";
			this.xrTableRow11.Weight = 0.7360000000000001;
			this.lblComments.BackColor = Color.Empty;
			this.lblComments.BorderColor = Color.White;
			this.lblComments.Borders = BorderSide.None;
			this.lblComments.Font = new Font("Tahoma", 9.75f, FontStyle.Bold);
			this.lblComments.ForeColor = Color.Black;
			this.lblComments.Name = "lblComments";
			this.lblComments.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.lblComments.StylePriority.UseBackColor = false;
			this.lblComments.StylePriority.UseFont = false;
			this.lblComments.StylePriority.UseForeColor = false;
			this.lblComments.StylePriority.UsePadding = false;
			this.lblComments.Text = "Commentaires";
			this.lblComments.TextAlignment = TextAlignment.MiddleLeft;
			this.lblComments.Weight = 1.0;
			this.xrTableRow12.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell12
			});
			this.xrTableRow12.Name = "xrTableRow12";
			this.xrTableRow12.Weight = 0.8639999999999998;
			this.xrTableCell12.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Project.Comment")
			});
			this.xrTableCell12.Font = new Font("Tahoma", 9.75f);
			this.xrTableCell12.Multiline = true;
			this.xrTableCell12.Name = "xrTableCell12";
			this.xrTableCell12.Padding = new PaddingInfo(10, 0, 0, 0, 100f);
			this.xrTableCell12.StylePriority.UseBorderColor = false;
			this.xrTableCell12.StylePriority.UseFont = false;
			this.xrTableCell12.StylePriority.UsePadding = false;
			this.xrTableCell12.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell12.Weight = 1.0;
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
			this.DetailReport2.Bands.AddRange(new Band[]
			{
				this.Detail3,
				this.DetailReport,
				this.DetailReport4
			});
			this.DetailReport2.DataMember = "Sections.Sections_Section";
			this.DetailReport2.Level = 2;
			this.DetailReport2.Name = "DetailReport2";
			this.Detail3.Controls.AddRange(new XRControl[]
			{
				this.xrTable9
			});
			this.Detail3.HeightF = 38f;
			this.Detail3.Name = "Detail3";
			this.Detail3.StylePriority.UseBackColor = false;
			this.xrTable9.BackColor = Color.SteelBlue;
			this.xrTable9.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.xrTable9.ForeColor = Color.White;
			this.xrTable9.LocationFloat = new PointFloat(0f, 8f);
			this.xrTable9.Name = "xrTable9";
			this.xrTable9.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable9.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow14
			});
			this.xrTable9.SizeF = new SizeF(650f, 25f);
			this.xrTable9.StylePriority.UseBackColor = false;
			this.xrTable9.StylePriority.UseFont = false;
			this.xrTable9.StylePriority.UseForeColor = false;
			this.xrTable9.StylePriority.UsePadding = false;
			this.xrTableRow14.Cells.AddRange(new XRTableCell[]
			{
				this.lblItem,
				this.lblQuantity,
				this.lblUnit,
				this.lblPrice,
				this.lblPriceTotal
			});
			this.xrTableRow14.Name = "xrTableRow14";
			this.xrTableRow14.Weight = 1.0869565217391304;
			this.lblItem.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Sections.Sections_Section.Description")
			});
			this.lblItem.Name = "lblItem";
			this.lblItem.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblItem.StylePriority.UsePadding = false;
			this.lblItem.StylePriority.UseTextAlignment = false;
			this.lblItem.TextAlignment = TextAlignment.MiddleLeft;
			this.lblItem.Weight = 2.4888891925681467;
			this.lblQuantity.Name = "lblQuantity";
			this.lblQuantity.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblQuantity.StylePriority.UsePadding = false;
			this.lblQuantity.StylePriority.UseTextAlignment = false;
			this.lblQuantity.Text = "Quantité";
			this.lblQuantity.TextAlignment = TextAlignment.MiddleRight;
			this.lblQuantity.Weight = 0.8000001375251499;
			this.lblUnit.Name = "lblUnit";
			this.lblUnit.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.lblUnit.StylePriority.UsePadding = false;
			this.lblUnit.StylePriority.UseTextAlignment = false;
			this.lblUnit.Text = "Unité";
			this.lblUnit.TextAlignment = TextAlignment.MiddleLeft;
			this.lblUnit.Weight = 0.4888889762725724;
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblPrice.StylePriority.UsePadding = false;
			this.lblPrice.StylePriority.UseTextAlignment = false;
			this.lblPrice.Text = "Prix";
			this.lblPrice.TextAlignment = TextAlignment.MiddleRight;
			this.lblPrice.Weight = 0.9244445620674466;
			this.lblPriceTotal.Name = "lblPriceTotal";
			this.lblPriceTotal.Padding = new PaddingInfo(3, 10, 0, 0, 100f);
			this.lblPriceTotal.StylePriority.UsePadding = false;
			this.lblPriceTotal.StylePriority.UseTextAlignment = false;
			this.lblPriceTotal.Text = "Prix total";
			this.lblPriceTotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblPriceTotal.Weight = 1.0755557621185552;
			this.DetailReport.Bands.AddRange(new Band[]
			{
				this.Detail1,
				this.GroupFooter1
			});
			this.DetailReport.DataMember = "Sections.Sections_Section.Section_Items.Items_Item";
			this.DetailReport.Level = 0;
			this.DetailReport.Name = "DetailReport";
			this.Detail1.Controls.AddRange(new XRControl[]
			{
				this.xrLine1,
				this.xrTable3
			});
			this.Detail1.HeightF = 23f;
			this.Detail1.KeepTogether = true;
			this.Detail1.Name = "Detail1";
			this.xrLine1.BorderColor = Color.Transparent;
			this.xrLine1.ForeColor = Color.DarkGray;
			this.xrLine1.LineStyle = DashStyle.Dot;
			this.xrLine1.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new SizeF(650f, 3f);
			this.xrLine1.StylePriority.UseBorderColor = false;
			this.xrLine1.StylePriority.UseForeColor = false;
			this.xrTable3.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTable3.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable3.LocationFloat = new PointFloat(0f, 0f);
			this.xrTable3.Name = "xrTable3";
			this.xrTable3.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable3.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow5
			});
			this.xrTable3.SizeF = new SizeF(650f, 18f);
			this.xrTable3.StylePriority.UseFont = false;
			this.xrTable3.StylePriority.UseForeColor = false;
			this.xrTable3.StylePriority.UsePadding = false;
			this.xrTableRow5.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell1,
				this.xrTableCell3,
				this.xrTableCell5,
				this.xrTableCell6,
				this.xrTableCell7
			});
			this.xrTableRow5.Name = "xrTableRow5";
			this.xrTableRow5.Weight = 1.0;
			this.xrTableCell1.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Sections.Sections_Section.Section_Items.Items_Item.Description")
			});
			this.xrTableCell1.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell1.Name = "xrTableCell1";
			this.xrTableCell1.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell1.StylePriority.UseFont = false;
			this.xrTableCell1.StylePriority.UsePadding = false;
			this.xrTableCell1.StylePriority.UseTextAlignment = false;
			this.xrTableCell1.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell1.Weight = 2.4888891925681467;
			this.xrTableCell3.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Sections.Sections_Section.Section_Items.Items_Item.Quantity")
			});
			this.xrTableCell3.Name = "xrTableCell3";
			this.xrTableCell3.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell3.StylePriority.UsePadding = false;
			this.xrTableCell3.StylePriority.UseTextAlignment = false;
			this.xrTableCell3.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell3.Weight = 0.80000013752515;
			this.xrTableCell5.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Sections.Sections_Section.Section_Items.Items_Item.Unit")
			});
			this.xrTableCell5.Name = "xrTableCell5";
			this.xrTableCell5.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell5.StylePriority.UsePadding = false;
			this.xrTableCell5.StylePriority.UseTextAlignment = false;
			this.xrTableCell5.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell5.Weight = 0.4888889762725725;
			this.xrTableCell6.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Sections.Sections_Section.Section_Items.Items_Item.PriceEach")
			});
			this.xrTableCell6.Name = "xrTableCell6";
			this.xrTableCell6.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell6.StylePriority.UsePadding = false;
			this.xrTableCell6.StylePriority.UseTextAlignment = false;
			this.xrTableCell6.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell6.Weight = 0.9244445620674465;
			this.xrTableCell7.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Sections.Sections_Section.Section_Items.Items_Item.Total")
			});
			this.xrTableCell7.Name = "xrTableCell7";
			this.xrTableCell7.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell7.StylePriority.UsePadding = false;
			this.xrTableCell7.StylePriority.UseTextAlignment = false;
			this.xrTableCell7.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell7.Weight = 1.0755557621185552;
			this.GroupFooter1.Controls.AddRange(new XRControl[]
			{
				this.xrTable7
			});
			this.GroupFooter1.GroupUnion = GroupFooterUnion.WithLastDetail;
			this.GroupFooter1.HeightF = 30f;
			this.GroupFooter1.Name = "GroupFooter1";
			this.xrTable7.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTable7.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable7.LocationFloat = new PointFloat(0f, 5f);
			this.xrTable7.Name = "xrTable7";
			this.xrTable7.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable7.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow6
			});
			this.xrTable7.SizeF = new SizeF(649.9951f, 18f);
			this.xrTable7.StylePriority.UseFont = false;
			this.xrTable7.StylePriority.UseForeColor = false;
			this.xrTable7.StylePriority.UsePadding = false;
			this.xrTableRow6.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell9,
				this.xrTableCell11,
				this.xrTableCell13,
				this.xrTableCell14,
				this.xrTableCell15
			});
			this.xrTableRow6.Name = "xrTableRow6";
			this.xrTableRow6.Weight = 1.0;
			this.xrTableCell9.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell9.Name = "xrTableCell9";
			this.xrTableCell9.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell9.StylePriority.UseFont = false;
			this.xrTableCell9.StylePriority.UsePadding = false;
			this.xrTableCell9.StylePriority.UseTextAlignment = false;
			this.xrTableCell9.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell9.Weight = 2.4888891925681467;
			this.xrTableCell11.Name = "xrTableCell11";
			this.xrTableCell11.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell11.StylePriority.UsePadding = false;
			this.xrTableCell11.StylePriority.UseTextAlignment = false;
			this.xrTableCell11.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell11.Weight = 0.80000013752515;
			this.xrTableCell13.Name = "xrTableCell13";
			this.xrTableCell13.Padding = new PaddingInfo(6, 3, 0, 0, 100f);
			this.xrTableCell13.StylePriority.UsePadding = false;
			this.xrTableCell13.StylePriority.UseTextAlignment = false;
			this.xrTableCell13.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell13.Weight = 0.4888884337377703;
			this.xrTableCell14.Name = "xrTableCell14";
			this.xrTableCell14.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell14.StylePriority.UsePadding = false;
			this.xrTableCell14.StylePriority.UseTextAlignment = false;
			this.xrTableCell14.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell14.Weight = 0.9244445620674465;
			this.xrTableCell15.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Sections.Sections_Section.Total")
			});
			this.xrTableCell15.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell15.ForeColor = Color.SteelBlue;
			this.xrTableCell15.Name = "xrTableCell15";
			this.xrTableCell15.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell15.StylePriority.UseFont = false;
			this.xrTableCell15.StylePriority.UseForeColor = false;
			this.xrTableCell15.StylePriority.UsePadding = false;
			this.xrTableCell15.StylePriority.UseTextAlignment = false;
			this.xrTableCell15.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell15.Weight = 1.0755129018691736;
			this.DetailReport4.Bands.AddRange(new Band[]
			{
				this.Detail5
			});
			this.DetailReport4.Expanded = false;
			this.DetailReport4.Level = 1;
			this.DetailReport4.Name = "DetailReport4";
			this.Detail5.HeightF = 36.24999f;
			this.Detail5.Name = "Detail5";
			this.Detail5.Visible = false;
			this.formattingRule9.Condition = "!IsNullOrEmpty([Comment])";
			this.formattingRule9.DataMember = "Objects.Objects_Object";
			this.formattingRule9.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule9.Name = "formattingRule9";
			this.formattingRule1.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule1.DataMember = "Objects.Objects_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule1.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule1.Name = "formattingRule1";
			this.formattingRule3.Condition = "IsNullOrEmpty([Extension_Fields.Fields_Id])";
			this.formattingRule3.DataMember = "Objects.Objects_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule3.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule3.Name = "formattingRule3";
			this.formattingRule2.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule2.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule2.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule2.Name = "formattingRule2";
			this.formattingRule5.Condition = "IsNullOrEmpty([ContactName])";
			this.formattingRule5.DataMember = "Project";
			this.formattingRule5.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule5.Name = "formattingRule5";
			this.xrTable8.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTable8.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable8.LocationFloat = new PointFloat(1.004855f, 0f);
			this.xrTable8.Name = "xrTable8";
			this.xrTable8.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable8.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow13
			});
			this.xrTable8.SizeF = new SizeF(649.9952f, 18f);
			this.xrTable8.StylePriority.UseFont = false;
			this.xrTable8.StylePriority.UseForeColor = false;
			this.xrTable8.StylePriority.UsePadding = false;
			this.xrTableRow13.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell16,
				this.xrTableCell17,
				this.lblSummaryTax1,
				this.xrTableCell20
			});
			this.xrTableRow13.Name = "xrTableRow13";
			this.xrTableRow13.Weight = 1.0;
			this.xrTableCell16.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell16.Name = "xrTableCell16";
			this.xrTableCell16.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell16.StylePriority.UseFont = false;
			this.xrTableCell16.StylePriority.UsePadding = false;
			this.xrTableCell16.StylePriority.UseTextAlignment = false;
			this.xrTableCell16.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell16.Weight = 2.648164118420158;
			this.xrTableCell17.Name = "xrTableCell17";
			this.xrTableCell17.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell17.StylePriority.UsePadding = false;
			this.xrTableCell17.StylePriority.UseTextAlignment = false;
			this.xrTableCell17.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell17.Weight = 0.3055373816360779;
			this.lblSummaryTax1.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "GrandTotal.Taxe1Caption")
			});
			this.lblSummaryTax1.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummaryTax1.Name = "lblSummaryTax1";
			this.lblSummaryTax1.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblSummaryTax1.StylePriority.UseFont = false;
			this.lblSummaryTax1.StylePriority.UsePadding = false;
			this.lblSummaryTax1.StylePriority.UseTextAlignment = false;
			this.lblSummaryTax1.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryTax1.Weight = 1.2748070905247797;
			this.xrTableCell20.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "GrandTotal.Taxe1Total")
			});
			this.xrTableCell20.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell20.ForeColor = Color.SteelBlue;
			this.xrTableCell20.Name = "xrTableCell20";
			this.xrTableCell20.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell20.StylePriority.UseFont = false;
			this.xrTableCell20.StylePriority.UseForeColor = false;
			this.xrTableCell20.StylePriority.UsePadding = false;
			this.xrTableCell20.StylePriority.UseTextAlignment = false;
			this.xrTableCell20.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell20.Weight = 1.0870519389455473;
			this.xrTable10.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTable10.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable10.LocationFloat = new PointFloat(1.004855f, 0f);
			this.xrTable10.Name = "xrTable10";
			this.xrTable10.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable10.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow15
			});
			this.xrTable10.SizeF = new SizeF(649.9958f, 18f);
			this.xrTable10.StylePriority.UseFont = false;
			this.xrTable10.StylePriority.UseForeColor = false;
			this.xrTable10.StylePriority.UsePadding = false;
			this.xrTableRow15.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell21,
				this.xrTableCell22,
				this.lblSummaryTax2,
				this.xrTableCell25
			});
			this.xrTableRow15.Name = "xrTableRow15";
			this.xrTableRow15.Weight = 1.0;
			this.xrTableCell21.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell21.Name = "xrTableCell21";
			this.xrTableCell21.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell21.StylePriority.UseFont = false;
			this.xrTableCell21.StylePriority.UsePadding = false;
			this.xrTableCell21.StylePriority.UseTextAlignment = false;
			this.xrTableCell21.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell21.Weight = 2.648164118420158;
			this.xrTableCell22.Name = "xrTableCell22";
			this.xrTableCell22.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell22.StylePriority.UsePadding = false;
			this.xrTableCell22.StylePriority.UseTextAlignment = false;
			this.xrTableCell22.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell22.Weight = 0.3055371248708958;
			this.lblSummaryTax2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "GrandTotal.Taxe2Caption")
			});
			this.lblSummaryTax2.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummaryTax2.Name = "lblSummaryTax2";
			this.lblSummaryTax2.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblSummaryTax2.StylePriority.UseFont = false;
			this.lblSummaryTax2.StylePriority.UsePadding = false;
			this.lblSummaryTax2.StylePriority.UseTextAlignment = false;
			this.lblSummaryTax2.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryTax2.Weight = 1.274807347289962;
			this.xrTableCell25.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "GrandTotal.Taxe2Total")
			});
			this.xrTableCell25.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell25.ForeColor = Color.SteelBlue;
			this.xrTableCell25.Name = "xrTableCell25";
			this.xrTableCell25.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell25.StylePriority.UseFont = false;
			this.xrTableCell25.StylePriority.UseForeColor = false;
			this.xrTableCell25.StylePriority.UsePadding = false;
			this.xrTableCell25.StylePriority.UseTextAlignment = false;
			this.xrTableCell25.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell25.Weight = 1.087016999683304;
			this.xrTable11.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTable11.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable11.LocationFloat = new PointFloat(1.005554f, 17.00001f);
			this.xrTable11.Name = "xrTable11";
			this.xrTable11.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable11.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow16
			});
			this.xrTable11.SizeF = new SizeF(649.9951f, 18f);
			this.xrTable11.StylePriority.UseFont = false;
			this.xrTable11.StylePriority.UseForeColor = false;
			this.xrTable11.StylePriority.UsePadding = false;
			this.xrTableRow16.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell26,
				this.xrTableCell27,
				this.lblSummaryTotalAmontDue,
				this.xrTableCell30
			});
			this.xrTableRow16.Name = "xrTableRow16";
			this.xrTableRow16.Weight = 1.0;
			this.xrTableCell26.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell26.Name = "xrTableCell26";
			this.xrTableCell26.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell26.StylePriority.UseFont = false;
			this.xrTableCell26.StylePriority.UsePadding = false;
			this.xrTableCell26.StylePriority.UseTextAlignment = false;
			this.xrTableCell26.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell26.Weight = 2.648164118420158;
			this.xrTableCell27.Name = "xrTableCell27";
			this.xrTableCell27.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell27.StylePriority.UsePadding = false;
			this.xrTableCell27.StylePriority.UseTextAlignment = false;
			this.xrTableCell27.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell27.Weight = 0.30553836790307654;
			this.lblSummaryTotalAmontDue.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummaryTotalAmontDue.Name = "lblSummaryTotalAmontDue";
			this.lblSummaryTotalAmontDue.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblSummaryTotalAmontDue.StylePriority.UseFont = false;
			this.lblSummaryTotalAmontDue.StylePriority.UsePadding = false;
			this.lblSummaryTotalAmontDue.StylePriority.UseTextAlignment = false;
			this.lblSummaryTotalAmontDue.Text = "Total Amount Due:";
			this.lblSummaryTotalAmontDue.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummaryTotalAmontDue.Weight = 1.2748061042577812;
			this.xrTableCell30.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "GrandTotal.TotalAfterTaxes")
			});
			this.xrTableCell30.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell30.ForeColor = Color.SteelBlue;
			this.xrTableCell30.Name = "xrTableCell30";
			this.xrTableCell30.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell30.StylePriority.UseFont = false;
			this.xrTableCell30.StylePriority.UseForeColor = false;
			this.xrTableCell30.StylePriority.UsePadding = false;
			this.xrTableCell30.StylePriority.UseTextAlignment = false;
			this.xrTableCell30.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell30.Weight = 1.0870150031262864;
			this.xrLine2.BorderColor = Color.Transparent;
			this.xrLine2.ForeColor = Color.Gray;
			this.xrLine2.LocationFloat = new PointFloat(0f, 0f);
			this.xrLine2.Name = "xrLine2";
			this.xrLine2.SizeF = new SizeF(650f, 3f);
			this.xrLine2.StylePriority.UseBorderColor = false;
			this.xrLine2.StylePriority.UseForeColor = false;
			this.xrLine2.Visible = false;
			this.DetailReport1.Bands.AddRange(new Band[]
			{
				this.Detail2,
				this.GroupFooter2,
				this.GroupFooter3
			});
			this.DetailReport1.Level = 1;
			this.DetailReport1.Name = "DetailReport1";
			this.Detail2.Controls.AddRange(new XRControl[]
			{
				this.xrTable12,
				this.xrLine2
			});
			this.Detail2.HeightF = 37.5f;
			this.Detail2.KeepTogether = true;
			this.Detail2.Name = "Detail2";
			this.Detail2.SubBands.AddRange(new SubBand[]
			{
				this.SubBand3,
				this.SubBand4,
				this.SubBand5
			});
			this.xrTable12.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTable12.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable12.LocationFloat = new PointFloat(0f, 14.87503f);
			this.xrTable12.Name = "xrTable12";
			this.xrTable12.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable12.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow17
			});
			this.xrTable12.SizeF = new SizeF(649.9957f, 18f);
			this.xrTable12.StylePriority.UseFont = false;
			this.xrTable12.StylePriority.UseForeColor = false;
			this.xrTable12.StylePriority.UsePadding = false;
			this.xrTableRow17.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell18,
				this.xrTableCell23,
				this.lblSummarySubtotal,
				this.xrTableCell31
			});
			this.xrTableRow17.Name = "xrTableRow17";
			this.xrTableRow17.Weight = 1.0;
			this.xrTableCell18.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell18.Name = "xrTableCell18";
			this.xrTableCell18.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell18.StylePriority.UseFont = false;
			this.xrTableCell18.StylePriority.UsePadding = false;
			this.xrTableCell18.StylePriority.UseTextAlignment = false;
			this.xrTableCell18.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell18.Weight = 2.648164118420158;
			this.xrTableCell23.Name = "xrTableCell23";
			this.xrTableCell23.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell23.StylePriority.UsePadding = false;
			this.xrTableCell23.StylePriority.UseTextAlignment = false;
			this.xrTableCell23.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell23.Weight = 0.3055371248708958;
			this.lblSummarySubtotal.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.lblSummarySubtotal.Name = "lblSummarySubtotal";
			this.lblSummarySubtotal.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.lblSummarySubtotal.StylePriority.UseFont = false;
			this.lblSummarySubtotal.StylePriority.UsePadding = false;
			this.lblSummarySubtotal.StylePriority.UseTextAlignment = false;
			this.lblSummarySubtotal.Text = "Subtotal:";
			this.lblSummarySubtotal.TextAlignment = TextAlignment.MiddleRight;
			this.lblSummarySubtotal.Weight = 1.274807347289962;
			this.xrTableCell31.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "GrandTotal.Total")
			});
			this.xrTableCell31.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Underline);
			this.xrTableCell31.ForeColor = Color.SteelBlue;
			this.xrTableCell31.Name = "xrTableCell31";
			this.xrTableCell31.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell31.StylePriority.UseFont = false;
			this.xrTableCell31.StylePriority.UseForeColor = false;
			this.xrTableCell31.StylePriority.UsePadding = false;
			this.xrTableCell31.StylePriority.UseTextAlignment = false;
			this.xrTableCell31.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell31.Weight = 1.0870519389455473;
			this.xrLine3.BorderColor = Color.Transparent;
			this.xrLine3.ForeColor = Color.Gray;
			this.xrLine3.LineWidth = 2;
			this.xrLine3.LocationFloat = new PointFloat(361.1836f, 0f);
			this.xrLine3.Name = "xrLine3";
			this.xrLine3.SizeF = new SizeF(288.8051f, 16.45838f);
			this.xrLine3.StylePriority.UseBorderColor = false;
			this.xrLine3.StylePriority.UseForeColor = false;
			this.GroupFooter2.Controls.AddRange(new XRControl[]
			{
				this.xrTable6
			});
			this.GroupFooter2.HeightF = 62.5f;
			this.GroupFooter2.KeepTogether = true;
			this.GroupFooter2.Name = "GroupFooter2";
			this.GroupFooter3.Controls.AddRange(new XRControl[]
			{
				this.xrLabel1
			});
			this.GroupFooter3.HeightF = 47.91667f;
			this.GroupFooter3.Level = 1;
			this.GroupFooter3.Name = "GroupFooter3";
			this.GroupFooter3.PageBreak = PageBreak.AfterBand;
			this.xrLabel1.BorderColor = Color.Black;
			this.xrLabel1.Borders = BorderSide.None;
			this.xrLabel1.BorderWidth = 1f;
			this.xrLabel1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel1.LocationFloat = new PointFloat(225f, 12.5f);
			this.xrLabel1.Name = "xrLabel1";
			this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel1.SizeF = new SizeF(125f, 12.5f);
			this.xrLabel1.StylePriority.UseBorderColor = false;
			this.xrLabel1.StylePriority.UseBorders = false;
			this.xrLabel1.StylePriority.UseBorderWidth = false;
			this.xrLabel1.StylePriority.UseFont = false;
			this.xrLabel1.StylePriority.UsePadding = false;
			this.xrLabel1.StylePriority.UseTextAlignment = false;
			this.xrLabel1.TextAlignment = TextAlignment.MiddleRight;
			this.DetailReport3.Bands.AddRange(new Band[]
			{
				this.Detail4,
				this.DetailReport6
			});
			this.DetailReport3.Level = 0;
			this.DetailReport3.Name = "DetailReport3";
			this.Detail4.Controls.AddRange(new XRControl[]
			{
				this.xrTable15
			});
			this.Detail4.HeightF = 38f;
			this.Detail4.Name = "Detail4";
			this.DetailReport6.Bands.AddRange(new Band[]
			{
				this.Detail7
			});
			this.DetailReport6.DataMember = "Totals.Totals_SubTotal";
			this.DetailReport6.Level = 0;
			this.DetailReport6.Name = "DetailReport6";
			this.Detail7.Controls.AddRange(new XRControl[]
			{
				this.xrTable13,
				this.xrLine4
			});
			this.Detail7.HeightF = 23f;
			this.Detail7.KeepTogether = true;
			this.Detail7.Name = "Detail7";
			this.xrTable13.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrTable13.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable13.LocationFloat = new PointFloat(0f, 0f);
			this.xrTable13.Name = "xrTable13";
			this.xrTable13.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable13.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow20
			});
			this.xrTable13.SizeF = new SizeF(649.9951f, 18f);
			this.xrTable13.StylePriority.UseFont = false;
			this.xrTable13.StylePriority.UseForeColor = false;
			this.xrTable13.StylePriority.UsePadding = false;
			this.xrTableRow20.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell2,
				this.xrTableCell34
			});
			this.xrTableRow20.Name = "xrTableRow20";
			this.xrTableRow20.Weight = 1.0;
			this.xrTableCell2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Totals.Totals_SubTotal.Description")
			});
			this.xrTableCell2.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell2.Name = "xrTableCell2";
			this.xrTableCell2.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell2.StylePriority.UseFont = false;
			this.xrTableCell2.StylePriority.UsePadding = false;
			this.xrTableCell2.StylePriority.UseTextAlignment = false;
			this.xrTableCell2.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell2.Weight = 4.426629263261771;
			this.xrTableCell34.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Totals.Totals_SubTotal.Total")
			});
			this.xrTableCell34.Font = new Font("Courier New", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrTableCell34.Name = "xrTableCell34";
			this.xrTableCell34.Padding = new PaddingInfo(3, 3, 0, 0, 100f);
			this.xrTableCell34.StylePriority.UseFont = false;
			this.xrTableCell34.StylePriority.UsePadding = false;
			this.xrTableCell34.StylePriority.UseTextAlignment = false;
			this.xrTableCell34.TextAlignment = TextAlignment.MiddleRight;
			this.xrTableCell34.Weight = 1.3511058785174284;
			this.xrLine4.BorderColor = Color.Transparent;
			this.xrLine4.ForeColor = Color.DarkGray;
			this.xrLine4.LineStyle = DashStyle.Dot;
			this.xrLine4.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine4.Name = "xrLine4";
			this.xrLine4.SizeF = new SizeF(650f, 3f);
			this.xrLine4.StylePriority.UseBorderColor = false;
			this.xrLine4.StylePriority.UseForeColor = false;
			this.DetailReport5.Bands.AddRange(new Band[]
			{
				this.Detail6
			});
			this.DetailReport5.Expanded = false;
			this.DetailReport5.Level = 3;
			this.DetailReport5.Name = "DetailReport5";
			this.Detail6.HeightF = 100f;
			this.Detail6.Name = "Detail6";
			this.formattingRule10.Condition = "[DataSource.CurrentRowIndex] == [DataSource.RowCount]";
			this.formattingRule10.DataMember = "Totals.Totals_SubTotal";
			this.formattingRule10.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule10.Name = "formattingRule10";
			this.SubBand3.Controls.AddRange(new XRControl[]
			{
				this.xrTable8
			});
			this.SubBand3.FormattingRules.Add(this.formattingRule11);
			this.SubBand3.HeightF = 20f;
			this.SubBand3.Name = "SubBand3";
			this.SubBand3.Visible = false;
			this.SubBand4.Controls.AddRange(new XRControl[]
			{
				this.xrTable10
			});
			this.SubBand4.FormattingRules.Add(this.formattingRule12);
			this.SubBand4.HeightF = 20f;
			this.SubBand4.Name = "SubBand4";
			this.SubBand4.Visible = false;
			this.SubBand5.Controls.AddRange(new XRControl[]
			{
				this.xrTable11,
				this.xrLine3
			});
			this.SubBand5.HeightF = 45f;
			this.SubBand5.Name = "SubBand5";
			this.formattingRule11.Condition = "[RawTaxe1Total] > 0";
			this.formattingRule11.DataMember = "GrandTotal";
			this.formattingRule11.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule11.Name = "formattingRule11";
			this.formattingRule12.Condition = "[RawTaxe2Total] > 0";
			this.formattingRule12.DataMember = "GrandTotal";
			this.formattingRule12.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule12.Name = "formattingRule12";
			base.Bands.AddRange(new Band[]
			{
				this.Detail,
				this.TopMargin,
				this.BottomMargin,
				this.ReportHeader,
				this.DetailReport2,
				this.DetailReport1,
				this.DetailReport3,
				this.DetailReport5
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
				this.formattingRule12
			});
			base.Margins = new Margins(100, 99, 100, 100);
			base.Version = "14.2";
			this.XmlDataPath = "C:\\Users\\Patrick\\Documents\\Quoter Plan\\Mes rapports\\QuoteReport.xml";
			((ISupportInitialize)this.xrTable2).EndInit();
			((ISupportInitialize)this.xrTable4).EndInit();
			((ISupportInitialize)this.xrTable1).EndInit();
			((ISupportInitialize)this.xrTable5).EndInit();
			((ISupportInitialize)this.xrTable15).EndInit();
			((ISupportInitialize)this.xrTable6).EndInit();
			((ISupportInitialize)this.xrTable9).EndInit();
			((ISupportInitialize)this.xrTable3).EndInit();
			((ISupportInitialize)this.xrTable7).EndInit();
			((ISupportInitialize)this.xrTable8).EndInit();
			((ISupportInitialize)this.xrTable10).EndInit();
			((ISupportInitialize)this.xrTable11).EndInit();
			((ISupportInitialize)this.xrTable12).EndInit();
			((ISupportInitialize)this.xrTable13).EndInit();
			((ISupportInitialize)this).EndInit();
		}

		private void LoadResources()
		{
			this.lblHeader1.Text = Resources.Devis;
			this.lbHeaderPageInfo.Format = Resources.Page_0_de_1_;
			this.lblProjectInfo.Text = Resources.Projet;
			this.lblContactInfo.Text = Resources.Client;
			this.lblQuote.Text = Resources.Devis;
			this.lblQuoteNumber.Text = Resources.Quote_number;
			this.lblQuoteDate.Text = Resources.Quote_date;
			this.lblQuoteContact.Text = Resources.Quote_representative;
			this.lblComments.Text = Resources.Commentaires;
			this.lblSummarySubtotal.Text = Resources.Sous_total_;
			this.lblSummaryTotalAmontDue.Text = Resources.Somme_due_;
			this.lblFooter1.Text = Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan;
			this.lblFooter2.Text = Utilities.ApplicationWebsite;
			this.lbFooterlPageInfo.Format = Resources.Page_0_de_1_;
			this.lblQuantity.Text = Resources.Quantité;
			this.lblUnit.Text = Resources.Unité;
			this.lblPrice.Text = Resources.Prix;
			this.lblPriceTotal.Text = Resources.Prix_total;
		}

		public XtraReportQuote(Project project)
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

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private FormattingRule formattingRule1;

		private FormattingRule formattingRule3;

		private FormattingRule formattingRule2;

		private SubBand SubBand1;

		private FormattingRule formattingRule4;

		private FormattingRule formattingRule5;

		private FormattingRule formattingRule6;

		private FormattingRule formattingRule7;

		private FormattingRule formattingRule8;

		private XRLabel lblFooter2;

		private XRLabel lblFooter1;

		private DetailReportBand DetailReport4;

		private DetailBand Detail5;

		private XRLabel lblHeader1;

		private XRPageInfo lbHeaderPageInfo;

		private FormattingRule formattingRule9;

		private XRTable xrTable9;

		private XRTableRow xrTableRow14;

		private XRTableCell lblItem;

		private XRTableCell lblQuantity;

		private XRTableCell lblUnit;

		private XRTableCell lblPrice;

		private XRTableCell lblPriceTotal;

		private XRLabel lbHeaderTitle;

		private XRTable xrTable3;

		private XRTableRow xrTableRow5;

		private XRTableCell xrTableCell1;

		private XRTableCell xrTableCell3;

		private XRTableCell xrTableCell5;

		private XRTableCell xrTableCell6;

		private XRTableCell xrTableCell7;

		private XRLabel lbFooterDate;

		private XRLine xrLine1;

		private GroupFooterBand GroupFooter1;

		private XRTable xrTable7;

		private XRTableRow xrTableRow6;

		private XRTableCell xrTableCell9;

		private XRTableCell xrTableCell11;

		private XRTableCell xrTableCell13;

		private XRTableCell xrTableCell14;

		private XRTableCell xrTableCell15;

		private XRTable xrTable8;

		private XRTableRow xrTableRow13;

		private XRTableCell xrTableCell16;

		private XRTableCell xrTableCell17;

		private XRTableCell lblSummaryTax1;

		private XRTableCell xrTableCell20;

		private XRTable xrTable11;

		private XRTableRow xrTableRow16;

		private XRTableCell xrTableCell26;

		private XRTableCell xrTableCell27;

		private XRTableCell lblSummaryTotalAmontDue;

		private XRTableCell xrTableCell30;

		private XRTable xrTable10;

		private XRTableRow xrTableRow15;

		private XRTableCell xrTableCell21;

		private XRTableCell xrTableCell22;

		private XRTableCell lblSummaryTax2;

		private XRTableCell xrTableCell25;

		private XRLine xrLine2;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRLine xrLine3;

		private XRTable xrTable12;

		private XRTableRow xrTableRow17;

		private XRTableCell xrTableCell18;

		private XRTableCell xrTableCell23;

		private XRTableCell lblSummarySubtotal;

		private XRTableCell xrTableCell31;

		private SubBand SubBand2;

		private XRTable xrTable4;

		private XRTableRow xrTableRow7;

		private XRTableCell lblCompany;

		private XRTableRow xrTableRow8;

		private XRTableCell xrTableCell8;

		private XRTable xrTable5;

		private XRTableRow xrTableRow9;

		private XRTableCell lblContactInfo;

		private XRTableRow xrTableRow10;

		private XRTableCell xrTableCell10;

		private XRTable xrTable6;

		private XRTableRow xrTableRow11;

		private XRTableCell lblComments;

		private XRTableRow xrTableRow12;

		private XRTableCell xrTableCell12;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell lblProjectInfo;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell4;

		private XRTable xrTable2;

		private XRTableRow xrTableRow3;

		private XRTableCell lblQuote;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell19;

		private XRTableCell lblQuoteNumber;

		private XRTableRow xrTableRow19;

		private XRTableCell lblQuoteDate;

		private XRTableCell xrTableCell32;

		private XRTableRow xrTableRow18;

		private XRTableCell lblQuoteContact;

		private XRTableCell xrTableCell28;

		private XRTable xrTable15;

		private XRTableRow xrTableRow23;

		private XRTableCell lblQuoteTotalDescription;

		private XRTableCell lblQuoteTotal;

		private DetailReportBand DetailReport3;

		private DetailBand Detail4;

		private DetailReportBand DetailReport5;

		private DetailBand Detail6;

		private GroupFooterBand GroupFooter2;

		private GroupFooterBand GroupFooter3;

		private XRLabel xrLabel1;

		private DetailReportBand DetailReport6;

		private DetailBand Detail7;

		private XRTable xrTable13;

		private XRTableRow xrTableRow20;

		private XRTableCell xrTableCell2;

		private XRTableCell xrTableCell34;

		private XRLine xrLine4;

		private FormattingRule formattingRule10;

		private SubBand SubBand3;

		private SubBand SubBand4;

		private SubBand SubBand5;

		private FormattingRule formattingRule11;

		private FormattingRule formattingRule12;

		private Project project;
	}
}
