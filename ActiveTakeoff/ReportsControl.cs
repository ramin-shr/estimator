using DevComponents.DotNetBar;
using DevExpress.DocumentView.Controls;
using DevExpress.LookAndFeel;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using QuoterPlan.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace QuoterPlan
{
    public class ReportsControl : UserControl
    {
        private bool exitNow;

        private bool[] dirty = new bool[4];

        private DataSet[] dataSet = new DataSet[6];

        private DataSet summariesDataSet;

        private XtraReport[] xtraReport = new XtraReport[6];

        private DocumentViewer[] documentViewer = new DocumentViewer[4];

        private Project project;

        private DrawingArea drawArea;

        private ExtensionsSupport extensionsSupport;

        private DBManagement dbManagement;

        private ReportModule reportModule;

        private IContainer components;

        private SuperTabControl superTabProperties;

        private SuperTabControlPanel superTabControlPanel4;

        private SuperTabItem superTabItemInvoice;

        private SuperTabControlPanel superTabControlPanel3;

        private SuperTabItem superTabItemQuote;

        private SuperTabControlPanel superTabControlPanel2;

        private SuperTabItem superTabItemEstimating;

        private SuperTabControlPanel superTabControlPanel1;

        private SuperTabItem superTabItemTakeoff;

        private DocumentViewer documentViewerTakeoff;

        private DocumentViewer documentViewerInvoice;

        private DocumentViewer documentViewerQuote;

        private DocumentViewer documentViewerEstimating;

        private PanelEx panelTakeoffSettings;

        private RibbonBar ribbonBarTakeoffSettings;

        private LabelItem lblBarTakeoffReportPadding1;

        private LabelItem lblBarTakeoffReportSeparator;

        private LabelItem lblBarTakeoffReportPadding3;

        private ItemContainer itemContainer1;

        private CheckBoxItem checkBoxTakeoffShowProjectInfo;

        private CheckBoxItem checkBoxTakeoffShowComments;

        private CheckBoxItem checkBoxTakeoffShowInvisibleObjects;

        private ButtonItem btTakeoffSettingsApply;

        private ButtonItem btTakeoffSettingsCancel;

        private LabelItem lblBarTakeoffReportPadding2;

        private LabelItem labelItem1;

        private ItemContainer itemContainer2;

        private LabelItem labelItem2;

        private LabelItem labelItem3;

        private LabelItem lblBarTakeoffReportPadding4;

        private LabelItem lblTakeoffEnableFilter;

        private SwitchButtonItem switchTakeoffEnableFilter;

        private PanelEx panelEstimatingSettings;

        private RibbonBar ribbonBarEstimatingSettings;

        private ItemContainer itemContainer3;

        private LabelItem labelItem4;

        private LabelItem labelItem5;

        private LabelItem lblEstimatingEnableFilter;

        private SwitchButtonItem switchEstimatingEnableFilter;

        private LabelItem labelItem7;

        private ItemContainer itemContainer4;

        private LabelItem labelItem8;

        private CheckBoxItem checkBoxEstimatingShowProjectInfo;

        private CheckBoxItem checkBoxEstimatingShowComments;

        private CheckBoxItem checkBoxEstimatingShowInvisibleObjects;

        private LabelItem labelItem9;

        private LabelItem labelItem10;

        private LabelItem labelItem11;

        private LabelItem labelItem12;

        private ButtonItem btEstimatingSettingsApply;

        private ButtonItem btEstimatingSettingsCancel;

        private LabelItem lblEstimatingConsolidateBy;

        private CheckBoxItem opEstimatingConsolidateByGroups;

        private CheckBoxItem opEstimatingConsolidateByItems;

        private LabelItem lblEstimatingSortBySeparator;

        private ItemContainer containerConsolidatedBy;

        private ItemContainer itemContainer7;

        private ButtonItem btEditEstimatingSections;

        private ItemContainer itemContainer8;

        private LabelItem labelItem14;

        private LabelItem labelItem13;

        private LabelItem labelItem15;

        private ButtonItem btEstimatingSettingsTaxes;

        private PanelEx panelQuoteSettings;

        private RibbonBar ribbonBarQuoteSettings;

        private ItemContainer itemContainer5;

        private LabelItem labelItem6;

        private LabelItem labelItem16;

        private LabelItem lblQuoteEnableFilter;

        private SwitchButtonItem switchQuoteEnableFilter;

        private LabelItem labelItem18;

        private ButtonItem btQuoteSettingsTaxes;

        private LabelItem labelItem19;

        private LabelItem labelItem23;

        private ItemContainer itemContainer11;

        private LabelItem labelItem24;

        private CheckBoxItem checkBoxQuoteShowInvisibleObjects;

        private LabelItem labelItem25;

        private LabelItem labelItem26;

        private LabelItem labelItem27;

        private LabelItem labelItem28;

        private ButtonItem btQuoteSettingsApply;

        private ButtonItem btQuoteSettingsCancel;

        public ReportTabEnum CurrentReportTab
        {
            get
            {
                return (ReportTabEnum)this.superTabProperties.SelectedTabIndex;
            }
        }

        public ReportTypeEnum CurrentReportType
        {
            get
            {
                switch (this.CurrentReportTab)
                {
                    case ReportTabEnum.EstimatingReportTab:
                        {
                            if (this.project.Report.Order != Report.ReportOrderEnum.ReportOrderByObjects)
                            {
                                return ReportTypeEnum.EstimatingByPlansReport;
                            }
                            return ReportTypeEnum.EstimatingByGroupsReport;
                        }
                    case ReportTabEnum.QuoteReportTab:
                        {
                            return ReportTypeEnum.QuoteReport;
                        }
                    case ReportTabEnum.InvoiceReportTab:
                        {
                            return ReportTypeEnum.InvoiceReport;
                        }
                }
                if (this.project.Report.Order != Report.ReportOrderEnum.ReportOrderByObjects)
                {
                    return ReportTypeEnum.TakeoffByPlansReport;
                }
                return ReportTypeEnum.TakeoffByGroupsReport;
            }
        }

        public bool Dirty
        {
            get
            {
                return this.dirty[(int)this.CurrentReportTab];
            }
            set
            {
                if (!value)
                {
                    this.dirty[(int)this.CurrentReportTab] = false;
                    return;
                }
                this.dirty[0] = true;
                this.dirty[1] = true;
                this.dirty[2] = true;
                this.dirty[3] = true;
            }
        }

        public ReportModule ReportModule
        {
            get
            {
                return this.reportModule;
            }
        }

        public ReportsControl()
        {
            this.InitializeComponent();
            this.LoadResources();
            this.InitializeFonts();
            this.documentViewer[0] = this.documentViewerTakeoff;
            this.documentViewer[1] = this.documentViewerEstimating;
            this.documentViewer[2] = this.documentViewerQuote;
            this.documentViewer[3] = this.documentViewerInvoice;
        }

        private void btEstimatingSettingsApply_Click(object sender, EventArgs e)
        {
            this.project.Report.EstimatingShowProjectInfo = this.checkBoxEstimatingShowProjectInfo.Checked;
            this.project.Report.EstimatingShowInvisibleObjects = this.checkBoxEstimatingShowInvisibleObjects.Checked;
            this.project.Report.EstimatingShowComments = this.checkBoxEstimatingShowComments.Checked;
            this.project.Report.EstimatingApplyFilter = this.switchEstimatingEnableFilter.Value;
            this.dirty[1] = true;
            this.UpdateReport();
            if (this.OnApplySettings != null)
            {
                this.OnApplySettings(this, new EventArgs());
            }
            this.panelEstimatingSettings.Visible = false;
            this.superTabControlPanel2_Resize(this, new EventArgs());
            this.Dirty = false;
        }

        private void btQuoteSettingsApply_Click(object sender, EventArgs e)
        {
            this.project.Report.QuoteShowInvisibleObjects = this.checkBoxQuoteShowInvisibleObjects.Checked;
            this.project.Report.QuoteApplyFilter = this.switchQuoteEnableFilter.Value;
            this.dirty[2] = true;
            this.UpdateReport();
            if (this.OnApplySettings != null)
            {
                this.OnApplySettings(this, new EventArgs());
            }
            this.panelQuoteSettings.Visible = false;
            this.superTabControlPanel3_Resize(this, new EventArgs());
            this.Dirty = false;
        }

        private void btSettingsCancel_Click(object sender, EventArgs e)
        {
            if (this.OnCancelSettings != null)
            {
                this.OnCancelSettings(this, new EventArgs());
            }
            this.panelTakeoffSettings.Visible = false;
            this.panelEstimatingSettings.Visible = false;
            this.panelQuoteSettings.Visible = false;
            this.superTabControlPanel1_Resize(this, new EventArgs());
            this.superTabControlPanel2_Resize(this, new EventArgs());
            this.superTabControlPanel3_Resize(this, new EventArgs());
        }

        private void btSettingsTaxes_Click(object sender, EventArgs e)
        {
            this.EditTaxesConfiguration();
        }

        private void btTakeoffSettingsApply_Click(object sender, EventArgs e)
        {
            this.project.Report.TakeoffShowProjectInfo = this.checkBoxTakeoffShowProjectInfo.Checked;
            this.project.Report.TakeoffShowInvisibleObjects = this.checkBoxTakeoffShowInvisibleObjects.Checked;
            this.project.Report.TakeoffShowComments = this.checkBoxTakeoffShowComments.Checked;
            this.project.Report.TakeoffApplyFilter = this.switchTakeoffEnableFilter.Value;
            this.dirty[0] = true;
            this.UpdateReport();
            if (this.OnApplySettings != null)
            {
                this.OnApplySettings(this, new EventArgs());
            }
            this.panelTakeoffSettings.Visible = false;
            this.superTabControlPanel1_Resize(this, new EventArgs());
            this.Dirty = false;
        }

        public void Clear()
        {
            this.Dirty = false;
            this.documentViewer[0].DocumentSource = null;
            this.documentViewer[1].DocumentSource = null;
            this.documentViewer[2].DocumentSource = null;
            this.documentViewer[3].DocumentSource = null;
        }

        private void CreateXMLReport(ref DataSet dataSet, bool processSummariesDataSet = false)
        {
            Report.ReportOrderEnum order = this.project.Report.Order;
            Report.ReportSortByEnum reportSortBy = this.project.Report.ReportSortBy;
            try
            {
                ReportTypeEnum currentReportType = this.CurrentReportType;
                this.project.Report.Order = (this.CurrentReportType == ReportTypeEnum.QuoteReport ? Report.ReportOrderEnum.ReportOrderByObjects : this.project.Report.Order);
                this.project.Report.ReportSortBy = (this.CurrentReportType == ReportTypeEnum.QuoteReport ? Report.ReportSortByEnum.ReportSortByLayers : this.project.Report.ReportSortBy);
                string xML = this.reportModule.ExportToXML((currentReportType == ReportTypeEnum.EstimatingByGroupsReport || currentReportType == ReportTypeEnum.EstimatingByPlansReport ? true : currentReportType == ReportTypeEnum.QuoteReport), true);
                if (currentReportType == ReportTypeEnum.QuoteReport)
                {
                    xML = (new QuoteReportGenerator()).Generate(new MemoryStream(Encoding.UTF8.GetBytes(xML ?? "")), this.project.Report.QuoteReportSortBy, this.project, this.dbManagement);
                    if (xML == "")
                    {
                        return;
                    }
                }
                MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xML ?? ""));
                dataSet = new DataSet();
                dataSet.ReadXml(memoryStream);
                if (processSummariesDataSet)
                {
                    if (this.summariesDataSet != null)
                    {
                        this.summariesDataSet.Dispose();
                        this.summariesDataSet = null;
                    }
                    memoryStream.Dispose();
                    memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(this.reportModule.ReportSummaries ?? ""));
                    this.summariesDataSet = new DataSet();
                    this.summariesDataSet.ReadXml(memoryStream);
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
            this.project.Report.Order = order;
            this.project.Report.ReportSortBy = reportSortBy;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoExportToExcel(string fileName)
        {
            bool flag;
            while (true)
            {
                try
                {
                    this.xtraReport[(int)this.CurrentReportType].ExportOptions.Xlsx.ExportMode = XlsxExportMode.SingleFile;
                    this.xtraReport[(int)this.CurrentReportType].ExportOptions.Xlsx.TextExportMode = TextExportMode.Text;
                    this.xtraReport[(int)this.CurrentReportType].ExportToXlsx(fileName);
                    flag = true;
                    break;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    if (exception.Message.IndexOf("process") == -1)
                    {
                        Utilities.DisplaySystemError(exception);
                        flag = false;
                        break;
                    }
                    else if (Utilities.DisplayWarningQuestionRetryCancel(Resources.Fichier_utilisé_par_un_autre_processus, Resources.Veuillez_fermer_votre_tableur_Excel) == DialogResult.Cancel)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

        private bool DoExportToPDF(string fileName)
        {
            bool flag;
            while (true)
            {
                try
                {
                    this.xtraReport[(int)this.CurrentReportType].ExportToPdf(fileName);
                    flag = true;
                    break;
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    if (exception.Message.IndexOf("process") == -1)
                    {
                        Utilities.DisplaySystemError(exception);
                        flag = false;
                        break;
                    }
                    else if (Utilities.DisplayWarningQuestionRetryCancel(Resources.Fichier_utilisé_par_un_autre_processus, Resources.Veuillez_fermer_votre_fichier_PDF) == DialogResult.Cancel)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

        public void EditReportSettings()
        {
            ReportTabEnum currentReportTab = this.CurrentReportTab;
            ReportTypeEnum currentReportType = this.CurrentReportType;
            switch (currentReportTab)
            {
                case ReportTabEnum.TakeoffReportTab:
                    {
                        this.exitNow = true;
                        this.checkBoxTakeoffShowProjectInfo.Checked = this.project.Report.TakeoffShowProjectInfo;
                        this.checkBoxTakeoffShowInvisibleObjects.Checked = this.project.Report.TakeoffShowInvisibleObjects;
                        this.checkBoxTakeoffShowComments.Checked = this.project.Report.TakeoffShowComments;
                        this.switchTakeoffEnableFilter.Value = this.project.Report.TakeoffApplyFilter;
                        this.panelTakeoffSettings.Visible = true;
                        this.superTabControlPanel1_Resize(this, new EventArgs());
                        this.exitNow = false;
                        return;
                    }
                case ReportTabEnum.EstimatingReportTab:
                    {
                        this.exitNow = true;
                        this.checkBoxEstimatingShowProjectInfo.Checked = this.project.Report.EstimatingShowProjectInfo;
                        this.checkBoxEstimatingShowInvisibleObjects.Checked = this.project.Report.EstimatingShowInvisibleObjects;
                        this.checkBoxEstimatingShowComments.Checked = this.project.Report.EstimatingShowComments;
                        this.switchEstimatingEnableFilter.Value = this.project.Report.EstimatingApplyFilter;
                        this.panelEstimatingSettings.Visible = true;
                        this.lblEstimatingSortBySeparator.Visible = currentReportType == ReportTypeEnum.EstimatingByPlansReport;
                        this.superTabControlPanel2_Resize(this, new EventArgs());
                        this.exitNow = false;
                        return;
                    }
                case ReportTabEnum.QuoteReportTab:
                    {
                        this.exitNow = true;
                        this.checkBoxQuoteShowInvisibleObjects.Checked = this.project.Report.QuoteShowInvisibleObjects;
                        this.switchQuoteEnableFilter.Value = this.project.Report.QuoteApplyFilter;
                        this.panelQuoteSettings.Visible = true;
                        this.superTabControlPanel3_Resize(this, new EventArgs());
                        this.exitNow = false;
                        return;
                    }
                case ReportTabEnum.InvoiceReportTab:
                    {
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        public void EditTaxesConfiguration()
        {
            try
            {
                using (TaxesConfiguration taxesConfiguration = new TaxesConfiguration())
                {
                    taxesConfiguration.ShowDialog(this);
                    this.UpdateReport();
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
        }

        public void ExportToCOffice(string sExportFilename)
        {
            this.reportModule.ExportToCOffice = true;
            this.reportModule.CEstimatingItems = new CEstimatingItems();
            this.reportModule.ExportToXML(false, true);
            if (this.ExportToCOfficeFile(sExportFilename, this.reportModule.CEstimatingItems))
            {
                Utilities.DisplayMessage(Resources.Fichier_créé_avec_succès, sExportFilename);
            }
            this.reportModule.CEstimatingItems = null;
            this.reportModule.ExportToCOffice = false;
        }

        private bool ExportToCOfficeFile(string sExportFilename, QuoterPlan.CEstimatingItems CEstimatingItems)
        {
            string str = string.Concat("Page,Digitizer Item,Item Number,Total,Units,Color,", Environment.NewLine);
            foreach (CEstimatingItem collection in CEstimatingItems.Collection)
            {
                Variable tag = (Variable)collection.Tag;
                str = string.Concat(str, "\"", tag.Name, "\",");
                object obj = str;
                object[] value = new object[] { obj, "\"", tag.Value, "\"," };
                str = string.Concat(value);
                str = string.Concat(str, collection.ItemID, ",");
                str = string.Concat(str, tag.Tag, ",");
                str = string.Concat(str, collection.Unit, ",");
                str = string.Concat(str, ",", Environment.NewLine);
            }
            return Utilities.SaveStringToFile(sExportFilename, str);
        }

        public void ExportToExcel()
        {
            string str = string.Concat(this.GetExportFileName(), ".xls");
            Settings.Default.ExportToExcelType = 0;
            this.reportModule.ExportToExcel(str, this.CurrentReportTab == ReportTabEnum.EstimatingReportTab);
        }

        public void ExportToHTML()
        {
            string str = string.Concat(this.GetExportFileName(), ".html");
            try
            {
                this.xtraReport[(int)this.CurrentReportType].ExportOptions.Html.ExportMode = HtmlExportMode.SingleFilePageByPage;
                this.xtraReport[(int)this.CurrentReportType].ExportToHtml(str);
                Utilities.OpenDocument(str);
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
        }

        public void ExportToPDF()
        {
            string str = string.Concat(this.GetExportFileName(), ".pdf");
            if (this.DoExportToPDF(str))
            {
                Utilities.OpenDocument(str);
            }
        }

        public void ExportToXML()
        {
            ReportTypeEnum currentReportType = this.CurrentReportType;
            this.reportModule.ExportToXML((currentReportType == ReportTypeEnum.EstimatingByGroupsReport ? true : currentReportType == ReportTypeEnum.EstimatingByPlansReport), false);
        }

        private string GetExportFileName()
        {
            string rapportDeMétréParGroupes;
            string rapportDEstimationParGroupes;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.project.FileName);
            switch (this.CurrentReportTab)
            {
                case ReportTabEnum.TakeoffReportTab:
                    {
                        string str = fileNameWithoutExtension;
                        if (this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByObjects)
                        {
                            rapportDeMétréParGroupes = Resources.Rapport_de_métré_par_groupes;
                        }
                        else
                        {
                            rapportDeMétréParGroupes = (this.project.Report.ReportSortBy == Report.ReportSortByEnum.ReportSortByPlans ? Resources.Rapport_de_métré_par_plans : Resources.Rapport_de_métré_par_calques);
                        }
                        fileNameWithoutExtension = string.Concat(str, "-", rapportDeMétréParGroupes);
                        break;
                    }
                case ReportTabEnum.EstimatingReportTab:
                    {
                        string str1 = fileNameWithoutExtension;
                        if (this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByObjects)
                        {
                            rapportDEstimationParGroupes = Resources.Rapport_d_estimation_par_groupes;
                        }
                        else
                        {
                            rapportDEstimationParGroupes = (this.project.Report.ReportSortBy == Report.ReportSortByEnum.ReportSortByPlans ? Resources.Rapport_d_estimation_par_plans : Resources.Rapport_d_estimation_par_calques);
                        }
                        fileNameWithoutExtension = string.Concat(str1, "-", rapportDEstimationParGroupes);
                        break;
                    }
                case ReportTabEnum.QuoteReportTab:
                    {
                        fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "-", Resources.Rapport_devis);
                        break;
                    }
                case ReportTabEnum.InvoiceReportTab:
                    {
                        fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "-", Resources.Rapport_facture);
                        break;
                    }
            }
            fileNameWithoutExtension = Path.Combine(Utilities.GetReportsFolder(), fileNameWithoutExtension.Replace(' ', '-'));
            return fileNameWithoutExtension;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ReportsControl));
            this.superTabProperties = new SuperTabControl();
            this.superTabControlPanel3 = new SuperTabControlPanel();
            this.panelQuoteSettings = new PanelEx();
            this.ribbonBarQuoteSettings = new RibbonBar();
            this.itemContainer5 = new ItemContainer();
            this.labelItem6 = new LabelItem();
            this.labelItem16 = new LabelItem();
            this.lblQuoteEnableFilter = new LabelItem();
            this.switchQuoteEnableFilter = new SwitchButtonItem();
            this.labelItem18 = new LabelItem();
            this.btQuoteSettingsTaxes = new ButtonItem();
            this.labelItem19 = new LabelItem();
            this.labelItem23 = new LabelItem();
            this.itemContainer11 = new ItemContainer();
            this.labelItem24 = new LabelItem();
            this.checkBoxQuoteShowInvisibleObjects = new CheckBoxItem();
            this.labelItem25 = new LabelItem();
            this.labelItem26 = new LabelItem();
            this.labelItem27 = new LabelItem();
            this.labelItem28 = new LabelItem();
            this.btQuoteSettingsApply = new ButtonItem();
            this.btQuoteSettingsCancel = new ButtonItem();
            this.documentViewerQuote = new DocumentViewer();
            this.superTabItemQuote = new SuperTabItem();
            this.superTabControlPanel2 = new SuperTabControlPanel();
            this.panelEstimatingSettings = new PanelEx();
            this.ribbonBarEstimatingSettings = new RibbonBar();
            this.itemContainer3 = new ItemContainer();
            this.labelItem4 = new LabelItem();
            this.labelItem5 = new LabelItem();
            this.lblEstimatingEnableFilter = new LabelItem();
            this.switchEstimatingEnableFilter = new SwitchButtonItem();
            this.labelItem15 = new LabelItem();
            this.btEstimatingSettingsTaxes = new ButtonItem();
            this.lblEstimatingSortBySeparator = new LabelItem();
            this.containerConsolidatedBy = new ItemContainer();
            this.itemContainer7 = new ItemContainer();
            this.lblEstimatingConsolidateBy = new LabelItem();
            this.opEstimatingConsolidateByGroups = new CheckBoxItem();
            this.opEstimatingConsolidateByItems = new CheckBoxItem();
            this.labelItem14 = new LabelItem();
            this.itemContainer8 = new ItemContainer();
            this.labelItem13 = new LabelItem();
            this.btEditEstimatingSections = new ButtonItem();
            this.labelItem7 = new LabelItem();
            this.itemContainer4 = new ItemContainer();
            this.labelItem8 = new LabelItem();
            this.checkBoxEstimatingShowProjectInfo = new CheckBoxItem();
            this.checkBoxEstimatingShowComments = new CheckBoxItem();
            this.checkBoxEstimatingShowInvisibleObjects = new CheckBoxItem();
            this.labelItem9 = new LabelItem();
            this.labelItem10 = new LabelItem();
            this.labelItem11 = new LabelItem();
            this.labelItem12 = new LabelItem();
            this.btEstimatingSettingsApply = new ButtonItem();
            this.btEstimatingSettingsCancel = new ButtonItem();
            this.documentViewerEstimating = new DocumentViewer();
            this.superTabItemEstimating = new SuperTabItem();
            this.superTabControlPanel1 = new SuperTabControlPanel();
            this.panelTakeoffSettings = new PanelEx();
            this.ribbonBarTakeoffSettings = new RibbonBar();
            this.itemContainer2 = new ItemContainer();
            this.labelItem2 = new LabelItem();
            this.labelItem3 = new LabelItem();
            this.lblTakeoffEnableFilter = new LabelItem();
            this.switchTakeoffEnableFilter = new SwitchButtonItem();
            this.lblBarTakeoffReportPadding4 = new LabelItem();
            this.itemContainer1 = new ItemContainer();
            this.lblBarTakeoffReportPadding2 = new LabelItem();
            this.checkBoxTakeoffShowProjectInfo = new CheckBoxItem();
            this.checkBoxTakeoffShowComments = new CheckBoxItem();
            this.checkBoxTakeoffShowInvisibleObjects = new CheckBoxItem();
            this.labelItem1 = new LabelItem();
            this.lblBarTakeoffReportPadding1 = new LabelItem();
            this.lblBarTakeoffReportSeparator = new LabelItem();
            this.lblBarTakeoffReportPadding3 = new LabelItem();
            this.btTakeoffSettingsApply = new ButtonItem();
            this.btTakeoffSettingsCancel = new ButtonItem();
            this.documentViewerTakeoff = new DocumentViewer();
            this.superTabItemTakeoff = new SuperTabItem();
            this.superTabControlPanel4 = new SuperTabControlPanel();
            this.documentViewerInvoice = new DocumentViewer();
            this.superTabItemInvoice = new SuperTabItem();
            ((ISupportInitialize)this.superTabProperties).BeginInit();
            this.superTabProperties.SuspendLayout();
            this.superTabControlPanel3.SuspendLayout();
            this.panelQuoteSettings.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.panelEstimatingSettings.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            this.panelTakeoffSettings.SuspendLayout();
            this.superTabControlPanel4.SuspendLayout();
            base.SuspendLayout();
            this.superTabProperties.BackColor = Color.White;
            this.superTabProperties.ControlBox.CloseBox.Name = Resources.Coller;
            this.superTabProperties.ControlBox.MenuBox.Name = Resources.Coller;
            this.superTabProperties.ControlBox.Name = Resources.Coller;
            SubItemsCollection subItems = this.superTabProperties.ControlBox.SubItems;
            BaseItem[] menuBox = new BaseItem[] { this.superTabProperties.ControlBox.MenuBox, this.superTabProperties.ControlBox.CloseBox };
            subItems.AddRange(menuBox);
            this.superTabProperties.Controls.Add(this.superTabControlPanel3);
            this.superTabProperties.Controls.Add(this.superTabControlPanel2);
            this.superTabProperties.Controls.Add(this.superTabControlPanel4);
            this.superTabProperties.Controls.Add(this.superTabControlPanel1);
            componentResourceManager.ApplyResources(this.superTabProperties, "superTabProperties");
            this.superTabProperties.FixedTabSize = new Size(120, 40);
            this.superTabProperties.ForeColor = Color.Black;
            this.superTabProperties.Name = "superTabProperties";
            this.superTabProperties.ReorderTabsEnabled = false;
            this.superTabProperties.SelectedTabIndex = 0;
            this.superTabProperties.TabAlignment = eTabStripAlignment.Left;
            this.superTabProperties.TabHorizontalSpacing = 0;
            SubItemsCollection tabs = this.superTabProperties.Tabs;
            BaseItem[] baseItemArray = new BaseItem[] { this.superTabItemTakeoff, this.superTabItemEstimating, this.superTabItemQuote, this.superTabItemInvoice };
            tabs.AddRange(baseItemArray);
            this.superTabProperties.TabStyle = eSuperTabStyle.Office2010BackstageBlue;
            this.superTabProperties.TabVerticalSpacing = 5;
            this.superTabProperties.SelectedTabChanged += new EventHandler<SuperTabStripSelectedTabChangedEventArgs>(this.superTabProperties_SelectedTabChanged);
            this.superTabControlPanel3.Controls.Add(this.panelQuoteSettings);
            this.superTabControlPanel3.Controls.Add(this.documentViewerQuote);
            componentResourceManager.ApplyResources(this.superTabControlPanel3, "superTabControlPanel3");
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.TabItem = this.superTabItemQuote;
            this.superTabControlPanel3.Resize += new EventHandler(this.superTabControlPanel3_Resize);
            this.panelQuoteSettings.CanvasColor = SystemColors.Control;
            this.panelQuoteSettings.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelQuoteSettings.Controls.Add(this.ribbonBarQuoteSettings);
            componentResourceManager.ApplyResources(this.panelQuoteSettings, "panelQuoteSettings");
            this.panelQuoteSettings.Name = "panelQuoteSettings";
            this.panelQuoteSettings.Style.Alignment = StringAlignment.Center;
            this.panelQuoteSettings.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.panelQuoteSettings.Style.Border = eBorderType.SingleLine;
            this.panelQuoteSettings.Style.BorderColor.ColorSchemePart = eColorSchemePart.ItemSeparator;
            this.panelQuoteSettings.Style.BorderSide = eBorderSide.Top;
            this.panelQuoteSettings.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.panelQuoteSettings.Style.GradientAngle = 90;
            this.ribbonBarQuoteSettings.AutoOverflowEnabled = true;
            this.ribbonBarQuoteSettings.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarQuoteSettings.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarQuoteSettings.ContainerControlProcessDialogKey = true;
            componentResourceManager.ApplyResources(this.ribbonBarQuoteSettings, "ribbonBarQuoteSettings");
            this.ribbonBarQuoteSettings.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            SubItemsCollection items = this.ribbonBarQuoteSettings.Items;
            BaseItem[] baseItemArray1 = new BaseItem[] { this.itemContainer5, this.labelItem18, this.btQuoteSettingsTaxes, this.labelItem19, this.labelItem23, this.itemContainer11, this.labelItem26, this.labelItem27, this.labelItem28, this.btQuoteSettingsApply, this.btQuoteSettingsCancel };
            items.AddRange(baseItemArray1);
            this.ribbonBarQuoteSettings.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarQuoteSettings.Name = "ribbonBarQuoteSettings";
            this.ribbonBarQuoteSettings.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarQuoteSettings.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarQuoteSettings.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.itemContainer5.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer5.ItemSpacing = 2;
            this.itemContainer5.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer5.Name = "itemContainer5";
            SubItemsCollection subItemsCollection = this.itemContainer5.SubItems;
            BaseItem[] baseItemArray2 = new BaseItem[] { this.labelItem6, this.labelItem16, this.lblQuoteEnableFilter, this.switchQuoteEnableFilter };
            subItemsCollection.AddRange(baseItemArray2);
            this.itemContainer5.TitleStyle.CornerType = eCornerType.Square;
            this.labelItem6.Name = "labelItem6";
            this.labelItem6.PaddingTop = -10;
            this.labelItem16.Name = "labelItem16";
            this.labelItem16.PaddingTop = -10;
            this.lblQuoteEnableFilter.Name = "lblQuoteEnableFilter";
            this.lblQuoteEnableFilter.PaddingBottom = 3;
            this.lblQuoteEnableFilter.PaddingRight = 3;
            componentResourceManager.ApplyResources(this.lblQuoteEnableFilter, "lblQuoteEnableFilter");
            this.switchQuoteEnableFilter.ButtonWidth = 100;
            this.switchQuoteEnableFilter.Name = "switchQuoteEnableFilter";
            this.switchQuoteEnableFilter.OffBackColor = Color.Linen;
            componentResourceManager.ApplyResources(this.switchQuoteEnableFilter, "switchQuoteEnableFilter");
            this.switchQuoteEnableFilter.OffTextColor = Color.FromArgb(64, 64, 64);
            this.switchQuoteEnableFilter.OnBackColor = Color.ForestGreen;
            this.switchQuoteEnableFilter.OnTextColor = Color.White;
            this.switchQuoteEnableFilter.SwitchWidth = 50;
            this.labelItem18.Name = "labelItem18";
            this.labelItem18.Width = 7;
            this.btQuoteSettingsTaxes.ButtonStyle = eButtonStyle.ImageAndText;
            this.btQuoteSettingsTaxes.Image = (Image)componentResourceManager.GetObject("btQuoteSettingsTaxes.Image");
            this.btQuoteSettingsTaxes.ImagePosition = eImagePosition.Top;
            this.btQuoteSettingsTaxes.Name = "btQuoteSettingsTaxes";
            componentResourceManager.ApplyResources(this.btQuoteSettingsTaxes, "btQuoteSettingsTaxes");
            this.btQuoteSettingsTaxes.Click += new EventHandler(this.btSettingsTaxes_Click);
            this.labelItem19.Name = "labelItem19";
            this.labelItem19.Visible = false;
            this.labelItem19.Width = 5;
            this.labelItem23.Name = "labelItem23";
            this.labelItem23.Visible = false;
            this.labelItem23.Width = 5;
            this.itemContainer11.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer11.ItemSpacing = 2;
            this.itemContainer11.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer11.Name = "itemContainer11";
            SubItemsCollection subItems1 = this.itemContainer11.SubItems;
            BaseItem[] baseItemArray3 = new BaseItem[] { this.labelItem24, this.checkBoxQuoteShowInvisibleObjects, this.labelItem25 };
            subItems1.AddRange(baseItemArray3);
            this.itemContainer11.TitleStyle.CornerType = eCornerType.Square;
            this.labelItem24.Name = "labelItem24";
            this.labelItem24.PaddingTop = -10;
            this.checkBoxQuoteShowInvisibleObjects.Name = "checkBoxQuoteShowInvisibleObjects";
            componentResourceManager.ApplyResources(this.checkBoxQuoteShowInvisibleObjects, "checkBoxQuoteShowInvisibleObjects");
            this.labelItem25.Name = "labelItem25";
            this.labelItem25.PaddingTop = -10;
            this.labelItem26.Name = "labelItem26";
            this.labelItem26.Width = 15;
            this.labelItem27.BeginGroup = true;
            this.labelItem27.Name = "labelItem27";
            this.labelItem27.Width = 5;
            this.labelItem28.Name = "labelItem28";
            this.labelItem28.Width = 5;
            this.btQuoteSettingsApply.ButtonStyle = eButtonStyle.ImageAndText;
            this.btQuoteSettingsApply.Image = (Image)componentResourceManager.GetObject("btQuoteSettingsApply.Image");
            this.btQuoteSettingsApply.ImagePosition = eImagePosition.Top;
            this.btQuoteSettingsApply.Name = "btQuoteSettingsApply";
            componentResourceManager.ApplyResources(this.btQuoteSettingsApply, "btQuoteSettingsApply");
            this.btQuoteSettingsApply.Click += new EventHandler(this.btQuoteSettingsApply_Click);
            this.btQuoteSettingsCancel.ButtonStyle = eButtonStyle.ImageAndText;
            this.btQuoteSettingsCancel.Image = (Image)componentResourceManager.GetObject("btQuoteSettingsCancel.Image");
            this.btQuoteSettingsCancel.ImagePosition = eImagePosition.Top;
            this.btQuoteSettingsCancel.Name = "btQuoteSettingsCancel";
            componentResourceManager.ApplyResources(this.btQuoteSettingsCancel, "btQuoteSettingsCancel");
            this.btQuoteSettingsCancel.Click += new EventHandler(this.btSettingsCancel_Click);
            componentResourceManager.ApplyResources(this.documentViewerQuote, "documentViewerQuote");
            this.documentViewerQuote.LookAndFeel.SkinName = "Seven Classic";
            this.documentViewerQuote.LookAndFeel.UseDefaultLookAndFeel = false;
            this.documentViewerQuote.Name = "documentViewerQuote";
            this.documentViewerQuote.ShowPageMargins = false;
            this.documentViewerQuote.Status = "The document does not contain any pages.";
            this.superTabItemQuote.AttachedControl = this.superTabControlPanel3;
            this.superTabItemQuote.GlobalItem = false;
            this.superTabItemQuote.Name = "superTabItemQuote";
            componentResourceManager.ApplyResources(this.superTabItemQuote, "superTabItemQuote");
            this.superTabItemQuote.TextAlignment = new eItemAlignment?(eItemAlignment.Center);
            this.superTabControlPanel2.Controls.Add(this.panelEstimatingSettings);
            this.superTabControlPanel2.Controls.Add(this.documentViewerEstimating);
            componentResourceManager.ApplyResources(this.superTabControlPanel2, "superTabControlPanel2");
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.TabItem = this.superTabItemEstimating;
            this.superTabControlPanel2.Resize += new EventHandler(this.superTabControlPanel2_Resize);
            this.panelEstimatingSettings.CanvasColor = SystemColors.Control;
            this.panelEstimatingSettings.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelEstimatingSettings.Controls.Add(this.ribbonBarEstimatingSettings);
            componentResourceManager.ApplyResources(this.panelEstimatingSettings, "panelEstimatingSettings");
            this.panelEstimatingSettings.Name = "panelEstimatingSettings";
            this.panelEstimatingSettings.Style.Alignment = StringAlignment.Center;
            this.panelEstimatingSettings.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.panelEstimatingSettings.Style.Border = eBorderType.SingleLine;
            this.panelEstimatingSettings.Style.BorderColor.ColorSchemePart = eColorSchemePart.ItemSeparator;
            this.panelEstimatingSettings.Style.BorderSide = eBorderSide.Top;
            this.panelEstimatingSettings.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.panelEstimatingSettings.Style.GradientAngle = 90;
            this.ribbonBarEstimatingSettings.AutoOverflowEnabled = true;
            this.ribbonBarEstimatingSettings.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarEstimatingSettings.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarEstimatingSettings.ContainerControlProcessDialogKey = true;
            componentResourceManager.ApplyResources(this.ribbonBarEstimatingSettings, "ribbonBarEstimatingSettings");
            this.ribbonBarEstimatingSettings.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            SubItemsCollection items1 = this.ribbonBarEstimatingSettings.Items;
            BaseItem[] baseItemArray4 = new BaseItem[] { this.itemContainer3, this.labelItem15, this.btEstimatingSettingsTaxes, this.lblEstimatingSortBySeparator, this.containerConsolidatedBy, this.labelItem7, this.itemContainer4, this.labelItem10, this.labelItem11, this.labelItem12, this.btEstimatingSettingsApply, this.btEstimatingSettingsCancel };
            items1.AddRange(baseItemArray4);
            this.ribbonBarEstimatingSettings.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarEstimatingSettings.Name = "ribbonBarEstimatingSettings";
            this.ribbonBarEstimatingSettings.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarEstimatingSettings.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarEstimatingSettings.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.itemContainer3.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer3.ItemSpacing = 2;
            this.itemContainer3.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer3.Name = "itemContainer3";
            SubItemsCollection subItemsCollection1 = this.itemContainer3.SubItems;
            BaseItem[] baseItemArray5 = new BaseItem[] { this.labelItem4, this.labelItem5, this.lblEstimatingEnableFilter, this.switchEstimatingEnableFilter };
            subItemsCollection1.AddRange(baseItemArray5);
            this.itemContainer3.TitleStyle.CornerType = eCornerType.Square;
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.PaddingTop = -10;
            this.labelItem5.Name = "labelItem5";
            this.labelItem5.PaddingTop = -10;
            this.lblEstimatingEnableFilter.Name = "lblEstimatingEnableFilter";
            this.lblEstimatingEnableFilter.PaddingBottom = 3;
            this.lblEstimatingEnableFilter.PaddingRight = 3;
            componentResourceManager.ApplyResources(this.lblEstimatingEnableFilter, "lblEstimatingEnableFilter");
            this.switchEstimatingEnableFilter.ButtonWidth = 100;
            this.switchEstimatingEnableFilter.Name = "switchEstimatingEnableFilter";
            this.switchEstimatingEnableFilter.OffBackColor = Color.Linen;
            componentResourceManager.ApplyResources(this.switchEstimatingEnableFilter, "switchEstimatingEnableFilter");
            this.switchEstimatingEnableFilter.OffTextColor = Color.FromArgb(64, 64, 64);
            this.switchEstimatingEnableFilter.OnBackColor = Color.ForestGreen;
            this.switchEstimatingEnableFilter.OnTextColor = Color.White;
            this.switchEstimatingEnableFilter.SwitchWidth = 50;
            this.labelItem15.Name = "labelItem15";
            this.labelItem15.Width = 7;
            this.btEstimatingSettingsTaxes.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingSettingsTaxes.Image = (Image)componentResourceManager.GetObject("btEstimatingSettingsTaxes.Image");
            this.btEstimatingSettingsTaxes.ImagePosition = eImagePosition.Top;
            this.btEstimatingSettingsTaxes.Name = "btEstimatingSettingsTaxes";
            componentResourceManager.ApplyResources(this.btEstimatingSettingsTaxes, "btEstimatingSettingsTaxes");
            this.btEstimatingSettingsTaxes.Click += new EventHandler(this.btSettingsTaxes_Click);
            this.lblEstimatingSortBySeparator.Name = "lblEstimatingSortBySeparator";
            this.lblEstimatingSortBySeparator.Visible = false;
            this.lblEstimatingSortBySeparator.Width = 5;
            this.containerConsolidatedBy.BackgroundStyle.CornerType = eCornerType.Square;
            this.containerConsolidatedBy.Name = "containerConsolidatedBy";
            SubItemsCollection subItems2 = this.containerConsolidatedBy.SubItems;
            BaseItem[] baseItemArray6 = new BaseItem[] { this.itemContainer7, this.itemContainer8 };
            subItems2.AddRange(baseItemArray6);
            this.containerConsolidatedBy.TitleStyle.CornerType = eCornerType.Square;
            this.itemContainer7.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer7.ItemSpacing = 0;
            this.itemContainer7.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer7.Name = "itemContainer7";
            SubItemsCollection subItemsCollection2 = this.itemContainer7.SubItems;
            BaseItem[] baseItemArray7 = new BaseItem[] { this.lblEstimatingConsolidateBy, this.opEstimatingConsolidateByGroups, this.opEstimatingConsolidateByItems, this.labelItem14 };
            subItemsCollection2.AddRange(baseItemArray7);
            this.itemContainer7.TitleStyle.CornerType = eCornerType.Square;
            this.lblEstimatingConsolidateBy.Name = "lblEstimatingConsolidateBy";
            this.lblEstimatingConsolidateBy.PaddingBottom = 5;
            this.lblEstimatingConsolidateBy.PaddingLeft = 4;
            this.lblEstimatingConsolidateBy.PaddingTop = 9;
            componentResourceManager.ApplyResources(this.lblEstimatingConsolidateBy, "lblEstimatingConsolidateBy");
            this.opEstimatingConsolidateByGroups.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.opEstimatingConsolidateByGroups.Name = "opEstimatingConsolidateByGroups";
            componentResourceManager.ApplyResources(this.opEstimatingConsolidateByGroups, "opEstimatingConsolidateByGroups");
            this.opEstimatingConsolidateByItems.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.opEstimatingConsolidateByItems.Name = "opEstimatingConsolidateByItems";
            componentResourceManager.ApplyResources(this.opEstimatingConsolidateByItems, "opEstimatingConsolidateByItems");
            this.labelItem14.Name = "labelItem14";
            this.labelItem14.PaddingTop = -10;
            this.itemContainer8.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer8.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer8.Name = "itemContainer8";
            SubItemsCollection subItems3 = this.itemContainer8.SubItems;
            BaseItem[] baseItemArray8 = new BaseItem[] { this.labelItem13, this.btEditEstimatingSections };
            subItems3.AddRange(baseItemArray8);
            this.itemContainer8.TitleStyle.CornerType = eCornerType.Square;
            this.labelItem13.Height = 16;
            this.labelItem13.Name = "labelItem13";
            this.labelItem13.PaddingBottom = 35;
            this.btEditEstimatingSections.Image = (Image)componentResourceManager.GetObject("btEditEstimatingSections.Image");
            this.btEditEstimatingSections.Name = "btEditEstimatingSections";
            componentResourceManager.ApplyResources(this.btEditEstimatingSections, "btEditEstimatingSections");
            this.labelItem7.Name = "labelItem7";
            this.labelItem7.Visible = false;
            this.labelItem7.Width = 5;
            this.itemContainer4.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer4.ItemSpacing = 2;
            this.itemContainer4.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer4.Name = "itemContainer4";
            SubItemsCollection subItemsCollection3 = this.itemContainer4.SubItems;
            BaseItem[] baseItemArray9 = new BaseItem[] { this.labelItem8, this.checkBoxEstimatingShowProjectInfo, this.checkBoxEstimatingShowComments, this.checkBoxEstimatingShowInvisibleObjects, this.labelItem9 };
            subItemsCollection3.AddRange(baseItemArray9);
            this.itemContainer4.TitleStyle.CornerType = eCornerType.Square;
            this.labelItem8.Name = "labelItem8";
            this.labelItem8.PaddingTop = -10;
            this.checkBoxEstimatingShowProjectInfo.Name = "checkBoxEstimatingShowProjectInfo";
            componentResourceManager.ApplyResources(this.checkBoxEstimatingShowProjectInfo, "checkBoxEstimatingShowProjectInfo");
            this.checkBoxEstimatingShowComments.Name = "checkBoxEstimatingShowComments";
            componentResourceManager.ApplyResources(this.checkBoxEstimatingShowComments, "checkBoxEstimatingShowComments");
            this.checkBoxEstimatingShowInvisibleObjects.Name = "checkBoxEstimatingShowInvisibleObjects";
            componentResourceManager.ApplyResources(this.checkBoxEstimatingShowInvisibleObjects, "checkBoxEstimatingShowInvisibleObjects");
            this.labelItem9.Name = "labelItem9";
            this.labelItem9.PaddingTop = -10;
            this.labelItem10.Name = "labelItem10";
            this.labelItem10.Width = 15;
            this.labelItem11.BeginGroup = true;
            this.labelItem11.Name = "labelItem11";
            this.labelItem11.Width = 5;
            this.labelItem12.Name = "labelItem12";
            this.labelItem12.Width = 5;
            this.btEstimatingSettingsApply.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingSettingsApply.Image = (Image)componentResourceManager.GetObject("btEstimatingSettingsApply.Image");
            this.btEstimatingSettingsApply.ImagePosition = eImagePosition.Top;
            this.btEstimatingSettingsApply.Name = "btEstimatingSettingsApply";
            componentResourceManager.ApplyResources(this.btEstimatingSettingsApply, "btEstimatingSettingsApply");
            this.btEstimatingSettingsApply.Click += new EventHandler(this.btEstimatingSettingsApply_Click);
            this.btEstimatingSettingsCancel.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingSettingsCancel.Image = (Image)componentResourceManager.GetObject("btEstimatingSettingsCancel.Image");
            this.btEstimatingSettingsCancel.ImagePosition = eImagePosition.Top;
            this.btEstimatingSettingsCancel.Name = "btEstimatingSettingsCancel";
            componentResourceManager.ApplyResources(this.btEstimatingSettingsCancel, "btEstimatingSettingsCancel");
            this.btEstimatingSettingsCancel.Click += new EventHandler(this.btSettingsCancel_Click);
            componentResourceManager.ApplyResources(this.documentViewerEstimating, "documentViewerEstimating");
            this.documentViewerEstimating.LookAndFeel.SkinName = "Seven Classic";
            this.documentViewerEstimating.LookAndFeel.UseDefaultLookAndFeel = false;
            this.documentViewerEstimating.Name = "documentViewerEstimating";
            this.documentViewerEstimating.ShowPageMargins = false;
            this.documentViewerEstimating.Status = "The document does not contain any pages.";
            this.superTabItemEstimating.AttachedControl = this.superTabControlPanel2;
            this.superTabItemEstimating.GlobalItem = false;
            this.superTabItemEstimating.Name = "superTabItemEstimating";
            componentResourceManager.ApplyResources(this.superTabItemEstimating, "superTabItemEstimating");
            this.superTabItemEstimating.TextAlignment = new eItemAlignment?(eItemAlignment.Center);
            this.superTabControlPanel1.Controls.Add(this.panelTakeoffSettings);
            this.superTabControlPanel1.Controls.Add(this.documentViewerTakeoff);
            componentResourceManager.ApplyResources(this.superTabControlPanel1, "superTabControlPanel1");
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.TabItem = this.superTabItemTakeoff;
            this.superTabControlPanel1.Resize += new EventHandler(this.superTabControlPanel1_Resize);
            this.panelTakeoffSettings.CanvasColor = SystemColors.Control;
            this.panelTakeoffSettings.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelTakeoffSettings.Controls.Add(this.ribbonBarTakeoffSettings);
            componentResourceManager.ApplyResources(this.panelTakeoffSettings, "panelTakeoffSettings");
            this.panelTakeoffSettings.Name = "panelTakeoffSettings";
            this.panelTakeoffSettings.Style.Alignment = StringAlignment.Center;
            this.panelTakeoffSettings.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.panelTakeoffSettings.Style.Border = eBorderType.SingleLine;
            this.panelTakeoffSettings.Style.BorderColor.ColorSchemePart = eColorSchemePart.ItemSeparator;
            this.panelTakeoffSettings.Style.BorderSide = eBorderSide.Top;
            this.panelTakeoffSettings.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.panelTakeoffSettings.Style.GradientAngle = 90;
            this.ribbonBarTakeoffSettings.AutoOverflowEnabled = true;
            this.ribbonBarTakeoffSettings.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarTakeoffSettings.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarTakeoffSettings.ContainerControlProcessDialogKey = true;
            componentResourceManager.ApplyResources(this.ribbonBarTakeoffSettings, "ribbonBarTakeoffSettings");
            this.ribbonBarTakeoffSettings.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            SubItemsCollection items2 = this.ribbonBarTakeoffSettings.Items;
            BaseItem[] baseItemArray10 = new BaseItem[] { this.itemContainer2, this.lblBarTakeoffReportPadding4, this.itemContainer1, this.lblBarTakeoffReportPadding1, this.lblBarTakeoffReportSeparator, this.lblBarTakeoffReportPadding3, this.btTakeoffSettingsApply, this.btTakeoffSettingsCancel };
            items2.AddRange(baseItemArray10);
            this.ribbonBarTakeoffSettings.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarTakeoffSettings.Name = "ribbonBarTakeoffSettings";
            this.ribbonBarTakeoffSettings.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarTakeoffSettings.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarTakeoffSettings.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.itemContainer2.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer2.ItemSpacing = 2;
            this.itemContainer2.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer2.Name = "itemContainer2";
            SubItemsCollection subItems4 = this.itemContainer2.SubItems;
            BaseItem[] baseItemArray11 = new BaseItem[] { this.labelItem2, this.labelItem3, this.lblTakeoffEnableFilter, this.switchTakeoffEnableFilter };
            subItems4.AddRange(baseItemArray11);
            this.itemContainer2.TitleStyle.CornerType = eCornerType.Square;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.PaddingTop = -10;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.PaddingTop = -10;
            this.lblTakeoffEnableFilter.Name = "lblTakeoffEnableFilter";
            this.lblTakeoffEnableFilter.PaddingBottom = 3;
            this.lblTakeoffEnableFilter.PaddingRight = 3;
            componentResourceManager.ApplyResources(this.lblTakeoffEnableFilter, "lblTakeoffEnableFilter");
            this.switchTakeoffEnableFilter.ButtonWidth = 100;
            this.switchTakeoffEnableFilter.Name = "switchTakeoffEnableFilter";
            this.switchTakeoffEnableFilter.OffBackColor = Color.Linen;
            componentResourceManager.ApplyResources(this.switchTakeoffEnableFilter, "switchTakeoffEnableFilter");
            this.switchTakeoffEnableFilter.OffTextColor = Color.FromArgb(64, 64, 64);
            this.switchTakeoffEnableFilter.OnBackColor = Color.ForestGreen;
            this.switchTakeoffEnableFilter.OnTextColor = Color.White;
            this.switchTakeoffEnableFilter.SwitchWidth = 50;
            this.lblBarTakeoffReportPadding4.Name = "lblBarTakeoffReportPadding4";
            this.lblBarTakeoffReportPadding4.Width = 10;
            this.itemContainer1.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer1.ItemSpacing = 2;
            this.itemContainer1.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer1.Name = "itemContainer1";
            SubItemsCollection subItemsCollection4 = this.itemContainer1.SubItems;
            BaseItem[] baseItemArray12 = new BaseItem[] { this.lblBarTakeoffReportPadding2, this.checkBoxTakeoffShowProjectInfo, this.checkBoxTakeoffShowComments, this.checkBoxTakeoffShowInvisibleObjects, this.labelItem1 };
            subItemsCollection4.AddRange(baseItemArray12);
            this.itemContainer1.TitleStyle.CornerType = eCornerType.Square;
            this.lblBarTakeoffReportPadding2.Name = "lblBarTakeoffReportPadding2";
            this.lblBarTakeoffReportPadding2.PaddingTop = -10;
            this.checkBoxTakeoffShowProjectInfo.Name = "checkBoxTakeoffShowProjectInfo";
            componentResourceManager.ApplyResources(this.checkBoxTakeoffShowProjectInfo, "checkBoxTakeoffShowProjectInfo");
            this.checkBoxTakeoffShowComments.Name = "checkBoxTakeoffShowComments";
            componentResourceManager.ApplyResources(this.checkBoxTakeoffShowComments, "checkBoxTakeoffShowComments");
            this.checkBoxTakeoffShowInvisibleObjects.Name = "checkBoxTakeoffShowInvisibleObjects";
            componentResourceManager.ApplyResources(this.checkBoxTakeoffShowInvisibleObjects, "checkBoxTakeoffShowInvisibleObjects");
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.PaddingTop = -10;
            this.lblBarTakeoffReportPadding1.Name = "lblBarTakeoffReportPadding1";
            this.lblBarTakeoffReportPadding1.Width = 15;
            this.lblBarTakeoffReportSeparator.BeginGroup = true;
            this.lblBarTakeoffReportSeparator.Name = "lblBarTakeoffReportSeparator";
            this.lblBarTakeoffReportSeparator.Width = 5;
            this.lblBarTakeoffReportPadding3.Name = "lblBarTakeoffReportPadding3";
            this.lblBarTakeoffReportPadding3.Width = 5;
            this.btTakeoffSettingsApply.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTakeoffSettingsApply.Image = (Image)componentResourceManager.GetObject("btTakeoffSettingsApply.Image");
            this.btTakeoffSettingsApply.ImagePosition = eImagePosition.Top;
            this.btTakeoffSettingsApply.Name = "btTakeoffSettingsApply";
            componentResourceManager.ApplyResources(this.btTakeoffSettingsApply, "btTakeoffSettingsApply");
            this.btTakeoffSettingsApply.Click += new EventHandler(this.btTakeoffSettingsApply_Click);
            this.btTakeoffSettingsCancel.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTakeoffSettingsCancel.Image = (Image)componentResourceManager.GetObject("btTakeoffSettingsCancel.Image");
            this.btTakeoffSettingsCancel.ImagePosition = eImagePosition.Top;
            this.btTakeoffSettingsCancel.Name = "btTakeoffSettingsCancel";
            componentResourceManager.ApplyResources(this.btTakeoffSettingsCancel, "btTakeoffSettingsCancel");
            this.btTakeoffSettingsCancel.Click += new EventHandler(this.btSettingsCancel_Click);
            componentResourceManager.ApplyResources(this.documentViewerTakeoff, "documentViewerTakeoff");
            this.documentViewerTakeoff.LookAndFeel.SkinName = "Seven Classic";
            this.documentViewerTakeoff.LookAndFeel.UseDefaultLookAndFeel = false;
            this.documentViewerTakeoff.Name = "documentViewerTakeoff";
            this.documentViewerTakeoff.ShowPageMargins = false;
            this.documentViewerTakeoff.Status = "The document does not contain any pages.";
            this.superTabItemTakeoff.AttachedControl = this.superTabControlPanel1;
            this.superTabItemTakeoff.GlobalItem = false;
            this.superTabItemTakeoff.Name = "superTabItemTakeoff";
            componentResourceManager.ApplyResources(this.superTabItemTakeoff, "superTabItemTakeoff");
            this.superTabItemTakeoff.TextAlignment = new eItemAlignment?(eItemAlignment.Center);
            this.superTabControlPanel4.Controls.Add(this.documentViewerInvoice);
            componentResourceManager.ApplyResources(this.superTabControlPanel4, "superTabControlPanel4");
            this.superTabControlPanel4.Name = "superTabControlPanel4";
            this.superTabControlPanel4.TabItem = this.superTabItemInvoice;
            componentResourceManager.ApplyResources(this.documentViewerInvoice, "documentViewerInvoice");
            this.documentViewerInvoice.LookAndFeel.SkinName = "Seven Classic";
            this.documentViewerInvoice.LookAndFeel.UseDefaultLookAndFeel = false;
            this.documentViewerInvoice.Name = "documentViewerInvoice";
            this.documentViewerInvoice.ShowPageMargins = false;
            this.documentViewerInvoice.Status = "The document does not contain any pages.";
            this.superTabItemInvoice.AttachedControl = this.superTabControlPanel4;
            this.superTabItemInvoice.GlobalItem = false;
            this.superTabItemInvoice.Name = "superTabItemInvoice";
            componentResourceManager.ApplyResources(this.superTabItemInvoice, "superTabItemInvoice");
            this.superTabItemInvoice.TextAlignment = new eItemAlignment?(eItemAlignment.Center);
            this.superTabItemInvoice.Visible = false;
            componentResourceManager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.superTabProperties);
            base.Name = "ReportsControl";
            ((ISupportInitialize)this.superTabProperties).EndInit();
            this.superTabProperties.ResumeLayout(false);
            this.superTabControlPanel3.ResumeLayout(false);
            this.panelQuoteSettings.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.panelEstimatingSettings.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.panelTakeoffSettings.ResumeLayout(false);
            this.superTabControlPanel4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void InitializeData(Project project, DrawingArea drawArea, ExtensionsSupport extensionsSupport, DBManagement dbManagement)
        {
            this.project = project;
            this.drawArea = drawArea;
            this.extensionsSupport = extensionsSupport;
            this.reportModule = new ReportModule(project, drawArea, extensionsSupport);
            this.dbManagement = dbManagement;
            this.containerConsolidatedBy.Visible = false;
        }

        private void InitializeFonts()
        {
            this.Font = Utilities.GetDefaultFont();
            this.superTabProperties.Font = Utilities.GetDefaultFont();
            this.superTabProperties.TabFont = Utilities.GetDefaultFont();
            this.superTabProperties.SelectedTabFont = Utilities.GetDefaultFont(FontStyle.Bold);
            this.ribbonBarTakeoffSettings.Font = Utilities.GetDefaultFont();
            if (this.ribbonBarTakeoffSettings.Font.Name == "Segoe UI")
            {
                this.ribbonBarTakeoffSettings.Height = 97;
                return;
            }
            this.ribbonBarTakeoffSettings.Height = 89;
        }

        private void LoadResources()
        {
            this.documentViewerTakeoff.Status = Resources.Ce_rapport_ne_contient_aucune_page;
            this.documentViewerEstimating.Status = Resources.Ce_rapport_ne_contient_aucune_page;
            this.documentViewerQuote.Status = Resources.Ce_rapport_ne_contient_aucune_page;
            this.documentViewerInvoice.Status = Resources.Ce_rapport_ne_contient_aucune_page;
        }

        public void Print()
        {
            ReportTabEnum currentReportTab = this.CurrentReportTab;
            ReportTypeEnum currentReportType = this.CurrentReportType;
            this.project.Report.SelectedReportType = currentReportType;
            Utilities.EnableInterface((Form)base.Parent, false);
            try
            {
                this.xtraReport[(int)currentReportType].PrintDialog();
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
            Utilities.EnableInterface((Form)base.Parent, true);
        }

        private void superTabControlPanel1_Resize(object sender, EventArgs e)
        {
            this.documentViewerTakeoff.Location = Point.Empty;
            this.documentViewerTakeoff.Size = new Size(this.superTabControlPanel1.Width, this.superTabControlPanel1.Height - (this.panelTakeoffSettings.Visible ? this.panelTakeoffSettings.Height : 0));
        }

        private void superTabControlPanel2_Resize(object sender, EventArgs e)
        {
            this.documentViewerEstimating.Location = Point.Empty;
            this.documentViewerEstimating.Size = new Size(this.superTabControlPanel2.Width, this.superTabControlPanel2.Height - (this.panelEstimatingSettings.Visible ? this.panelEstimatingSettings.Height : 0));
        }

        private void superTabControlPanel3_Resize(object sender, EventArgs e)
        {
            this.documentViewerQuote.Location = Point.Empty;
            this.documentViewerQuote.Size = new Size(this.superTabControlPanel3.Width, this.superTabControlPanel3.Height - (this.panelQuoteSettings.Visible ? this.panelQuoteSettings.Height : 0));
        }

        private void superTabProperties_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (this.exitNow)
            {
                return;
            }
            Timer timer = new Timer();
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Interval = 50;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            timer.Enabled = false;
            ReportTabEnum currentReportTab = this.CurrentReportTab;
            if (this.dirty[(int)currentReportTab])
            {
                this.UpdateReport();
            }
            this.project.Report.SelectedReportType = this.CurrentReportType;
            if (this.OnSelectReport != null)
            {
                this.OnSelectReport(this, new EventArgs());
            }
        }

        public void UpdateReport()
        {
            ReportTabEnum currentReportTab = this.CurrentReportTab;
            ReportTypeEnum currentReportType = this.CurrentReportType;
            this.project.Report.SelectedReportType = currentReportType;
            Utilities.EnableInterface((Form)base.Parent, false);
            if (this.dataSet[(int)currentReportTab] != null)
            {
                this.dataSet[(int)currentReportTab].Dispose();
                this.dataSet[(int)currentReportTab] = null;
            }
            if (this.xtraReport[(int)currentReportType] != null)
            {
                this.xtraReport[(int)currentReportType].DataSource = null;
                this.xtraReport[(int)currentReportType] = null;
            }
            try
            {
                switch (currentReportTab)
                {
                    case ReportTabEnum.TakeoffReportTab:
                        {
                            this.CreateXMLReport(ref this.dataSet[(int)currentReportTab], false);
                            if (currentReportType != ReportTypeEnum.TakeoffByGroupsReport)
                            {
                                this.xtraReport[(int)currentReportType] = new XtraReportTakeoffByPlans(this.project);
                                goto case ReportTabEnum.InvoiceReportTab;
                            }
                            else
                            {
                                this.xtraReport[(int)currentReportType] = new XtraReportTakeoffByGroups(this.project);
                                goto case ReportTabEnum.InvoiceReportTab;
                            }
                        }
                    case ReportTabEnum.EstimatingReportTab:
                        {
                            this.CreateXMLReport(ref this.dataSet[(int)currentReportTab], currentReportType == ReportTypeEnum.EstimatingByPlansReport);
                            if (currentReportType != ReportTypeEnum.EstimatingByGroupsReport)
                            {
                                this.xtraReport[(int)currentReportType] = new XtraReportEstimatingByPlans(this.project);
                                goto case ReportTabEnum.InvoiceReportTab;
                            }
                            else
                            {
                                this.xtraReport[(int)currentReportType] = new XtraReportEstimatingByGroups(this.project);
                                goto case ReportTabEnum.InvoiceReportTab;
                            }
                        }
                    case ReportTabEnum.QuoteReportTab:
                        {
                            this.CreateXMLReport(ref this.dataSet[(int)currentReportTab], false);
                            this.xtraReport[(int)currentReportType] = new XtraReportQuote(this.project);
                            goto case ReportTabEnum.InvoiceReportTab;
                        }
                    case ReportTabEnum.InvoiceReportTab:
                        {
                            this.xtraReport[(int)currentReportType].DataSource = this.dataSet[(int)currentReportTab];
                            this.xtraReport[(int)currentReportType].CreateDocument();
                            if (currentReportType == ReportTypeEnum.EstimatingByPlansReport)
                            {
                                XtraReportEstimatingTotalsByPlans xtraReportEstimatingTotalsByPlan = new XtraReportEstimatingTotalsByPlans(this.project)
                                {
                                    DataSource = this.summariesDataSet
                                };
                                xtraReportEstimatingTotalsByPlan.CreateDocument();
                                this.xtraReport[(int)currentReportType].Pages.AddRange(xtraReportEstimatingTotalsByPlan.Pages);
                                this.xtraReport[(int)currentReportType].PrintingSystem.ContinuousPageNumbering = true;
                            }
                            this.documentViewer[(int)currentReportTab].DocumentSource = this.xtraReport[(int)currentReportType];
                            this.documentViewer[(int)currentReportTab].SelectFirstPage();
                            this.dirty[(int)currentReportTab] = false;
                            break;
                        }
                    default:
                        {
                            goto case ReportTabEnum.InvoiceReportTab;
                        }
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
            Utilities.EnableInterface((Form)base.Parent, true);
        }

        public event EventHandler OnApplySettings;

        public event EventHandler OnCancelSettings;

        public event EventHandler OnSelectReport;
    }
}