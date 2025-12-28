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
	public class XtraReportTakeoffByGroups : XtraReport
	{
		private void LoadResources()
		{
			this.lblHeader1.Text = Resources.Rapport_de_métré;
			this.lblHeader2.Text = Resources._par_groupes_;
			this.lbHeaderPageInfo.Format = Resources.Page_0_de_1_;
			this.lblProjectInfo.Text = Resources.Projet_;
			this.lblContactInfo.Text = Resources.Client_;
			this.lblComments.Text = Resources.Commentaires__;
			this.lblReportTitle.Text = Resources.RAPPORT_DE_MÉTRÉ_PAR_GROUPES_;
			this.lblFooter1.Text = Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan;
			this.lblFooter2.Text = Utilities.ApplicationWebsite;
			this.lbFooterlPageInfo.Format = Resources.Page_0_de_1_;
		}

		public XtraReportTakeoffByGroups(Project project)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XtraReportTakeoffByGroups));
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
			this.xrTable2 = new XRTable();
			this.xrTableRow3 = new XRTableRow();
			this.lblContactInfo = new XRTableCell();
			this.xrTableRow4 = new XRTableRow();
			this.xrTableCell4 = new XRTableCell();
			this.xrTable1 = new XRTable();
			this.xrTableRow1 = new XRTableRow();
			this.lblProjectInfo = new XRTableCell();
			this.xrTableRow9 = new XRTableRow();
			this.xrTableCell3 = new XRTableCell();
			this.formattingRule4 = new FormattingRule();
			this.SubBand4 = new SubBand();
			this.xrTable6 = new XRTable();
			this.xrTableRow11 = new XRTableRow();
			this.lblComments = new XRTableCell();
			this.xrTableRow12 = new XRTableRow();
			this.xrTableCell12 = new XRTableCell();
			this.formattingRule8 = new FormattingRule();
			this.SubBand5 = new SubBand();
			this.xrLabel3 = new XRLabel();
			this.formattingRule6 = new FormattingRule();
			this.formattingRule7 = new FormattingRule();
			this.DetailReport2 = new DetailReportBand();
			this.Detail3 = new DetailBand();
			this.xrLabel7 = new XRLabel();
			this.xrLabel9 = new XRLabel();
			this.xrLabel8 = new XRLabel();
			this.SubBand7 = new SubBand();
			this.xrLabel4 = new XRLabel();
			this.formattingRule9 = new FormattingRule();
			this.DetailReport = new DetailReportBand();
			this.Detail1 = new DetailBand();
			this.xrTable3 = new XRTable();
			this.xrTableRow5 = new XRTableRow();
			this.xrTableCell5 = new XRTableCell();
			this.DetailReport1 = new DetailReportBand();
			this.Detail2 = new DetailBand();
			this.xrLine2 = new XRLine();
			this.xrTable7 = new XRTable();
			this.xrTableRow6 = new XRTableRow();
			this.xrTableCell19 = new XRTableCell();
			this.xrTableCell26 = new XRTableCell();
			this.formattingRule1 = new FormattingRule();
			this.DetailReport3 = new DetailReportBand();
			this.Detail4 = new DetailBand();
			this.xrLine1 = new XRLine();
			this.xrTable8 = new XRTable();
			this.xrTableRow13 = new XRTableRow();
			this.xrTableCell6 = new XRTableCell();
			this.xrTableCell13 = new XRTableCell();
			this.DetailReport4 = new DetailReportBand();
			this.Detail5 = new DetailBand();
			this.xrLabel14 = new XRLabel();
			this.formattingRule3 = new FormattingRule();
			this.formattingRule2 = new FormattingRule();
			this.lblReportTitle = new XRLabel();
			this.formattingRule5 = new FormattingRule();
			this.GroupHeader2 = new GroupHeaderBand();
			this.xrTableRow2 = new XRTableRow();
			this.xrTableCell1 = new XRTableCell();
			this.xrTableRow7 = new XRTableRow();
			this.xrTableCell2 = new XRTableCell();
			((ISupportInitialize)this.xrTable2).BeginInit();
			((ISupportInitialize)this.xrTable1).BeginInit();
			((ISupportInitialize)this.xrTable6).BeginInit();
			((ISupportInitialize)this.xrTable3).BeginInit();
			((ISupportInitialize)this.xrTable7).BeginInit();
			((ISupportInitialize)this.xrTable8).BeginInit();
			((ISupportInitialize)this).BeginInit();
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
			this.lblHeader2.Text = "(par groupes)";
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
			this.lbHeaderPageInfo.LocationFloat = new PointFloat(498.9998f, 60.38541f);
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
			this.lbFooterDate.LocationFloat = new PointFloat(0.0001430511f, 41.50003f);
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
				this.xrTable1,
				this.xrTable2
			});
			this.SubBand1.FormattingRules.Add(this.formattingRule4);
			this.SubBand1.HeightF = 75f;
			this.SubBand1.Name = "SubBand1";
			this.xrTable2.BorderColor = Color.DarkSlateGray;
			this.xrTable2.Borders = BorderSide.None;
			this.xrTable2.BorderWidth = 2f;
			this.xrTable2.LocationFloat = new PointFloat(339.0417f, 0f);
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
			this.formattingRule4.Condition = "IsNullOrEmpty([Name])";
			this.formattingRule4.DataMember = "Project";
			this.formattingRule4.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule4.Name = "formattingRule4";
			this.SubBand4.Controls.AddRange(new XRControl[]
			{
				this.xrTable6
			});
			this.SubBand4.FormattingRules.Add(this.formattingRule8);
			this.SubBand4.HeightF = 60f;
			this.SubBand4.Name = "SubBand4";
			this.xrTable6.BorderColor = Color.DarkSlateGray;
			this.xrTable6.Borders = BorderSide.None;
			this.xrTable6.BorderWidth = 2f;
			this.xrTable6.LocationFloat = new PointFloat(0.0001430511f, 0f);
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
			this.formattingRule8.Condition = "IsNullOrEmpty([Comment])";
			this.formattingRule8.DataMember = "Project";
			this.formattingRule8.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule8.Name = "formattingRule8";
			this.SubBand5.Controls.AddRange(new XRControl[]
			{
				this.xrLabel3
			});
			this.SubBand5.FormattingRules.Add(this.formattingRule4);
			this.SubBand5.HeightF = 35f;
			this.SubBand5.Name = "SubBand5";
			this.xrLabel3.BackColor = Color.Transparent;
			this.xrLabel3.BorderColor = Color.Black;
			this.xrLabel3.Font = new Font("Tahoma", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.xrLabel3.ForeColor = Color.Transparent;
			this.xrLabel3.LocationFloat = new PointFloat(200f, 12.5f);
			this.xrLabel3.Multiline = true;
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
			this.xrLabel3.SizeF = new SizeF(257.29f, 15f);
			this.xrLabel3.StylePriority.UseBackColor = false;
			this.xrLabel3.StylePriority.UseBorderColor = false;
			this.xrLabel3.StylePriority.UseFont = false;
			this.xrLabel3.StylePriority.UseForeColor = false;
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.Text = "* * *";
			this.xrLabel3.TextAlignment = TextAlignment.TopCenter;
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
			this.DetailReport2.DataMember = "Objects.Objects_Object";
			this.DetailReport2.Level = 0;
			this.DetailReport2.Name = "DetailReport2";
			this.Detail3.Controls.AddRange(new XRControl[]
			{
				this.xrLabel7,
				this.xrLabel9,
				this.xrLabel8
			});
			this.Detail3.HeightF = 70f;
			this.Detail3.KeepTogetherWithDetailReports = true;
			this.Detail3.Name = "Detail3";
			this.Detail3.StylePriority.UseBackColor = false;
			this.Detail3.SubBands.AddRange(new SubBand[]
			{
				this.SubBand7
			});
			this.xrLabel7.BackColor = Color.Transparent;
			this.xrLabel7.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Name")
			});
			this.xrLabel7.Font = new Font("Tahoma", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel7.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel7.LocationFloat = new PointFloat(32f, 25f);
			this.xrLabel7.Name = "xrLabel7";
			this.xrLabel7.Padding = new PaddingInfo(6, 2, 1, 0, 100f);
			this.xrLabel7.SizeF = new SizeF(617.9999f, 23f);
			this.xrLabel7.StylePriority.UseFont = false;
			this.xrLabel7.StylePriority.UseForeColor = false;
			this.xrLabel7.StylePriority.UsePadding = false;
			this.xrLabel9.BorderColor = Color.FromArgb(32, 32, 32);
			this.xrLabel9.Borders = BorderSide.All;
			this.xrLabel9.BorderWidth = 2f;
			this.xrLabel9.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Color")
			});
			this.xrLabel9.LocationFloat = new PointFloat(10f, 27f);
			this.xrLabel9.Name = "xrLabel9";
			this.xrLabel9.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrLabel9.Scripts.OnBeforePrint = "xrLabel6_BeforePrint";
			this.xrLabel9.SizeF = new SizeF(21f, 21f);
			this.xrLabel9.StylePriority.UseBorderColor = false;
			this.xrLabel9.StylePriority.UseBorders = false;
			this.xrLabel9.StylePriority.UseBorderWidth = false;
			this.xrLabel9.StylePriority.UsePadding = false;
			this.xrLabel8.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Plans")
			});
			this.xrLabel8.Font = new Font("Tahoma", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel8.ForeColor = Color.FromArgb(0, 64, 64);
			this.xrLabel8.LocationFloat = new PointFloat(32f, 50f);
			this.xrLabel8.Multiline = true;
			this.xrLabel8.Name = "xrLabel8";
			this.xrLabel8.Padding = new PaddingInfo(6, 2, 2, 2, 100f);
			this.xrLabel8.SizeF = new SizeF(618f, 17.08334f);
			this.xrLabel8.StylePriority.UseBorderColor = false;
			this.xrLabel8.StylePriority.UseFont = false;
			this.xrLabel8.StylePriority.UseForeColor = false;
			this.xrLabel8.StylePriority.UsePadding = false;
			this.xrLabel8.StylePriority.UseTextAlignment = false;
			this.xrLabel8.TextAlignment = TextAlignment.MiddleLeft;
			this.SubBand7.Controls.AddRange(new XRControl[]
			{
				this.xrLabel4
			});
			this.SubBand7.FormattingRules.Add(this.formattingRule9);
			this.SubBand7.HeightF = 18f;
			this.SubBand7.Name = "SubBand7";
			this.SubBand7.Visible = false;
			this.xrLabel4.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Comment")
			});
			this.xrLabel4.Font = new Font("Tahoma", 6.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.xrLabel4.ForeColor = Color.FromArgb(64, 64, 64);
			this.xrLabel4.LocationFloat = new PointFloat(32f, 0f);
			this.xrLabel4.Multiline = true;
			this.xrLabel4.Name = "xrLabel4";
			this.xrLabel4.Padding = new PaddingInfo(6, 2, 2, 2, 100f);
			this.xrLabel4.SizeF = new SizeF(618f, 18f);
			this.xrLabel4.StylePriority.UseBorderColor = false;
			this.xrLabel4.StylePriority.UseFont = false;
			this.xrLabel4.StylePriority.UseForeColor = false;
			this.xrLabel4.StylePriority.UsePadding = false;
			this.xrLabel4.StylePriority.UseTextAlignment = false;
			this.xrLabel4.TextAlignment = TextAlignment.MiddleLeft;
			this.formattingRule9.Condition = "!IsNullOrEmpty([Comment])";
			this.formattingRule9.DataMember = "Objects.Objects_Object";
			this.formattingRule9.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule9.Name = "formattingRule9";
			this.DetailReport.Bands.AddRange(new Band[]
			{
				this.Detail1,
				this.DetailReport1,
				this.DetailReport3
			});
			this.DetailReport.DataMember = "Objects.Objects_Object.Object_Extensions.Extensions_Extension";
			this.DetailReport.Level = 0;
			this.DetailReport.Name = "DetailReport";
			this.Detail1.Controls.AddRange(new XRControl[]
			{
				this.xrTable3
			});
			this.Detail1.HeightF = 35f;
			this.Detail1.Name = "Detail1";
			this.xrTable3.Font = new Font("Tahoma", 9f, FontStyle.Bold | FontStyle.Underline);
			this.xrTable3.ForeColor = Color.DarkRed;
			this.xrTable3.LocationFloat = new PointFloat(0f, 8f);
			this.xrTable3.Name = "xrTable3";
			this.xrTable3.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
			this.xrTable3.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow5
			});
			this.xrTable3.SizeF = new SizeF(650f, 20f);
			this.xrTable3.StylePriority.UseFont = false;
			this.xrTable3.StylePriority.UseForeColor = false;
			this.xrTable3.StylePriority.UsePadding = false;
			this.xrTableRow5.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell5
			});
			this.xrTableRow5.Name = "xrTableRow5";
			this.xrTableRow5.Weight = 1.0;
			this.xrTableCell5.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Object_Extensions.Extensions_Extension.Name")
			});
			this.xrTableCell5.Name = "xrTableCell5";
			this.xrTableCell5.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell5.StylePriority.UsePadding = false;
			this.xrTableCell5.StylePriority.UseTextAlignment = false;
			this.xrTableCell5.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell5.Weight = 1.7528671039761468;
			this.DetailReport1.Bands.AddRange(new Band[]
			{
				this.Detail2
			});
			this.DetailReport1.DataMember = "Objects.Objects_Object.Object_Extensions.Extensions_Extension.Extension_Fields.Fields_Field";
			this.DetailReport1.FormattingRules.Add(this.formattingRule1);
			this.DetailReport1.Level = 0;
			this.DetailReport1.Name = "DetailReport1";
			this.DetailReport1.Visible = false;
			this.Detail2.Controls.AddRange(new XRControl[]
			{
				this.xrLine2,
				this.xrTable7
			});
			this.Detail2.HeightF = 23f;
			this.Detail2.Name = "Detail2";
			this.Detail2.StylePriority.UseBackColor = false;
			this.xrLine2.BorderColor = Color.Transparent;
			this.xrLine2.ForeColor = Color.DarkGray;
			this.xrLine2.LineStyle = DashStyle.Dot;
			this.xrLine2.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine2.Name = "xrLine2";
			this.xrLine2.SizeF = new SizeF(650f, 3f);
			this.xrLine2.StylePriority.UseBorderColor = false;
			this.xrLine2.StylePriority.UseForeColor = false;
			this.xrTable7.Borders = BorderSide.None;
			this.xrTable7.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable7.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable7.LocationFloat = new PointFloat(0.000333786f, 0f);
			this.xrTable7.Name = "xrTable7";
			this.xrTable7.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow6
			});
			this.xrTable7.Scripts.OnBeforePrint = "xrTable7_BeforePrint";
			this.xrTable7.SizeF = new SizeF(649.9998f, 18f);
			this.xrTable7.StylePriority.UseBorders = false;
			this.xrTable7.StylePriority.UseFont = false;
			this.xrTable7.StylePriority.UseForeColor = false;
			this.xrTable7.StylePriority.UseTextAlignment = false;
			this.xrTable7.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow6.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell19,
				this.xrTableCell26
			});
			this.xrTableRow6.Name = "xrTableRow6";
			this.xrTableRow6.Weight = 1.0;
			this.xrTableCell19.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Object_Extensions.Extensions_Extension.Extension_Fields.Fields_Field.Caption")
			});
			this.xrTableCell19.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell19.Name = "xrTableCell19";
			this.xrTableCell19.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell19.StylePriority.UseFont = false;
			this.xrTableCell19.StylePriority.UsePadding = false;
			this.xrTableCell19.Weight = 1.2109889018130584;
			this.xrTableCell26.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Object_Extensions.Extensions_Extension.Extension_Fields.Fields_Field.Value")
			});
			this.xrTableCell26.Name = "xrTableCell26";
			this.xrTableCell26.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell26.StylePriority.UsePadding = false;
			this.xrTableCell26.StylePriority.UseTextAlignment = false;
			xrsummary.FormatString = "{0:0.00%}";
			xrsummary.Func = SummaryFunc.Avg;
			this.xrTableCell26.Summary = xrsummary;
			this.xrTableCell26.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell26.Weight = 1.2961665864743686;
			this.formattingRule1.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule1.DataMember = "Objects.Objects_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule1.Formatting.Visible = DefaultBoolean.True;
			this.formattingRule1.Name = "formattingRule1";
			this.DetailReport3.Bands.AddRange(new Band[]
			{
				this.Detail4
			});
			this.DetailReport3.DataMember = "Objects.Objects_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result";
			this.DetailReport3.Level = 1;
			this.DetailReport3.Name = "DetailReport3";
			this.Detail4.Borders = BorderSide.All;
			this.Detail4.Controls.AddRange(new XRControl[]
			{
				this.xrLine1,
				this.xrTable8
			});
			this.Detail4.HeightF = 22f;
			this.Detail4.Name = "Detail4";
			this.Detail4.StylePriority.UseBackColor = false;
			this.Detail4.StylePriority.UseBorders = false;
			this.xrLine1.BorderColor = Color.Transparent;
			this.xrLine1.ForeColor = Color.DarkGray;
			this.xrLine1.LineStyle = DashStyle.Dot;
			this.xrLine1.LocationFloat = new PointFloat(0f, 19f);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new SizeF(650f, 3f);
			this.xrLine1.StylePriority.UseBorderColor = false;
			this.xrLine1.StylePriority.UseForeColor = false;
			this.xrTable8.Borders = BorderSide.None;
			this.xrTable8.Font = new Font("Courier New", 8.25f, FontStyle.Bold);
			this.xrTable8.ForeColor = Color.FromArgb(32, 32, 32);
			this.xrTable8.LocationFloat = new PointFloat(0.000333786f, 0f);
			this.xrTable8.Name = "xrTable8";
			this.xrTable8.Rows.AddRange(new XRTableRow[]
			{
				this.xrTableRow13
			});
			this.xrTable8.Scripts.OnBeforePrint = "xrTable7_BeforePrint";
			this.xrTable8.SizeF = new SizeF(649.9998f, 18f);
			this.xrTable8.StylePriority.UseBorders = false;
			this.xrTable8.StylePriority.UseFont = false;
			this.xrTable8.StylePriority.UseForeColor = false;
			this.xrTable8.StylePriority.UseTextAlignment = false;
			this.xrTable8.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableRow13.Cells.AddRange(new XRTableCell[]
			{
				this.xrTableCell6,
				this.xrTableCell13
			});
			this.xrTableRow13.Name = "xrTableRow13";
			this.xrTableRow13.Weight = 1.0;
			this.xrTableCell6.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.Caption")
			});
			this.xrTableCell6.Font = new Font("Tahoma", 8.5f, FontStyle.Bold);
			this.xrTableCell6.Name = "xrTableCell6";
			this.xrTableCell6.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.xrTableCell6.StylePriority.UseFont = false;
			this.xrTableCell6.StylePriority.UsePadding = false;
			this.xrTableCell6.Weight = 1.2109889018130584;
			this.xrTableCell13.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Objects.Objects_Object.Object_Extensions.Extensions_Extension.Extension_Results.Results_Result.TotalValue")
			});
			this.xrTableCell13.Name = "xrTableCell13";
			this.xrTableCell13.Padding = new PaddingInfo(1, 9, 1, 0, 100f);
			this.xrTableCell13.StylePriority.UsePadding = false;
			this.xrTableCell13.StylePriority.UseTextAlignment = false;
			xrsummary2.FormatString = "{0:0.00%}";
			xrsummary2.Func = SummaryFunc.Avg;
			this.xrTableCell13.Summary = xrsummary2;
			this.xrTableCell13.TextAlignment = TextAlignment.MiddleLeft;
			this.xrTableCell13.Weight = 1.2961665864743686;
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
			this.Detail5.Visible = false;
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
			this.formattingRule3.Condition = "IsNullOrEmpty([Extension_Fields.Fields_Id])";
			this.formattingRule3.DataMember = "Objects.Objects_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule3.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule3.Name = "formattingRule3";
			this.formattingRule2.Condition = "!IsNullOrEmpty([Extension_Fields])";
			this.formattingRule2.DataMember = "Plans.Plans_Plan.Plan_Objects.Objects_Object.Object_Extensions.Extensions_Extension";
			this.formattingRule2.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule2.Name = "formattingRule2";
			this.lblReportTitle.BackColor = Color.Transparent;
			this.lblReportTitle.Font = new Font("Tahoma", 15.25f, FontStyle.Bold | FontStyle.Underline);
			this.lblReportTitle.ForeColor = Color.FromArgb(0, 64, 64);
			this.lblReportTitle.LocationFloat = new PointFloat(0f, 0f);
			this.lblReportTitle.Name = "lblReportTitle";
			this.lblReportTitle.Padding = new PaddingInfo(10, 2, 0, 0, 100f);
			this.lblReportTitle.SizeF = new SizeF(650.0001f, 23f);
			this.lblReportTitle.StylePriority.UseFont = false;
			this.lblReportTitle.StylePriority.UseForeColor = false;
			this.lblReportTitle.StylePriority.UsePadding = false;
			this.lblReportTitle.StylePriority.UseTextAlignment = false;
			this.lblReportTitle.Text = "RAPPORT DE MÉTRÉ (PAR GROUPES)";
			this.lblReportTitle.TextAlignment = TextAlignment.MiddleCenter;
			this.formattingRule5.Condition = "IsNullOrEmpty([ContactName])";
			this.formattingRule5.DataMember = "Project";
			this.formattingRule5.Formatting.Visible = DefaultBoolean.False;
			this.formattingRule5.Name = "formattingRule5";
			this.GroupHeader2.Controls.AddRange(new XRControl[]
			{
				this.lblReportTitle
			});
			this.GroupHeader2.HeightF = 50f;
			this.GroupHeader2.Name = "GroupHeader2";
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
				this.formattingRule9
			});
			base.ScriptsSource = componentResourceManager.GetString("$this.ScriptsSource");
			base.Version = "14.2";
			this.XmlDataPath = "C:\\Users\\Patrick\\Documents\\Quoter Plan\\Mes rapports\\LE-PACIFIQUE-(11-Novembre-2014)-[Classé_par_objets].xml";
			((ISupportInitialize)this.xrTable2).EndInit();
			((ISupportInitialize)this.xrTable1).EndInit();
			((ISupportInitialize)this.xrTable6).EndInit();
			((ISupportInitialize)this.xrTable3).EndInit();
			((ISupportInitialize)this.xrTable7).EndInit();
			((ISupportInitialize)this.xrTable8).EndInit();
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

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private DetailReportBand DetailReport3;

		private DetailBand Detail4;

		private FormattingRule formattingRule1;

		private FormattingRule formattingRule3;

		private FormattingRule formattingRule2;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private SubBand SubBand1;

		private XRTable xrTable2;

		private XRTableRow xrTableRow3;

		private XRTableCell lblContactInfo;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell4;

		private XRTable xrTable6;

		private XRTableRow xrTableRow11;

		private XRTableCell lblComments;

		private XRTableRow xrTableRow12;

		private XRTableCell xrTableCell12;

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

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell lblProjectInfo;

		private SubBand SubBand4;

		private SubBand SubBand5;

		private XRLabel xrLabel3;

		private FormattingRule formattingRule9;

		private XRLine xrLine2;

		private XRLine xrLine1;

		private XRLabel xrLabel9;

		private XRLabel lbFooterDate;

		private XRTable xrTable3;

		private XRTableRow xrTableRow5;

		private XRTableCell xrTableCell5;

		private XRTable xrTable7;

		private XRTableRow xrTableRow6;

		private XRTableCell xrTableCell19;

		private XRTableCell xrTableCell26;

		private XRTable xrTable8;

		private XRTableRow xrTableRow13;

		private XRTableCell xrTableCell6;

		private XRTableCell xrTableCell13;

		private XRLabel xrLabel4;

		private SubBand SubBand7;

		private XRTableRow xrTableRow9;

		private XRTableCell xrTableCell3;

		private XRTableRow xrTableRow7;

		private XRTableCell xrTableCell2;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell1;
	}
}
