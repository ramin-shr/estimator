using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar.Rendering;
using DevComponents.Editors;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using QuoterPlan.Properties;
using QuoterPlanControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using ColumnHeader = DevComponents.AdvTree.ColumnHeader;
using eItemAlignment = DevComponents.DotNetBar.eItemAlignment;
using TabControl = DevComponents.DotNetBar.TabControl;
using Timer = System.Timers.Timer;
using ComboBoxEx = DevComponents.DotNetBar.Controls.ComboBoxEx;

namespace QuoterPlan
{
	public partial class MainForm : DevComponents.DotNetBar.Office2007RibbonForm
	{
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
            this.ribbonPanelEstimating = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonEstimating = new DevComponents.DotNetBar.RibbonBar();
            this.btEstimatingModify = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingDuplicate = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingDelete = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonEstimatingNew = new DevComponents.DotNetBar.RibbonBar();
            this.btEstimatingMaterial = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingLabour = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingEquipment = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingSubcontract = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonEstimatingDatabase = new DevComponents.DotNetBar.RibbonBar();
            this.btEstimatingTradesPackages = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingSaveDatabase = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingCompactDatabase = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingImportPrices = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonPanelReport = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonExportReport = new DevComponents.DotNetBar.RibbonBar();
            this.btReportExportToExcel = new DevComponents.DotNetBar.ButtonItem();
            this.lblExportToExcelOptions = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btExportToExcelRawData = new DevComponents.DotNetBar.ButtonItem();
            this.btExportToExcelFormattedData = new DevComponents.DotNetBar.ButtonItem();
            this.btExportToExcelRawAndFormatted = new DevComponents.DotNetBar.ButtonItem();
            this.btReportExportToCSV = new DevComponents.DotNetBar.ButtonItem();
            this.btReportExportToXML = new DevComponents.DotNetBar.ButtonItem();
            this.btReportExportToHTML = new DevComponents.DotNetBar.ButtonItem();
            this.btReportExportToPDF = new DevComponents.DotNetBar.ButtonItem();
            this.btReportExportToEE = new DevComponents.DotNetBar.ButtonItem();
            this.btReportExportToCOffice = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonPrintReport = new DevComponents.DotNetBar.RibbonBar();
            this.btReportPrint = new DevComponents.DotNetBar.ButtonItem();
            this.btReportPrintPreview = new DevComponents.DotNetBar.ButtonItem();
            this.btReportPrintSetup = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonReportOrder = new DevComponents.DotNetBar.RibbonBar();
            this.btReportFilter = new DevComponents.DotNetBar.ButtonItem();
            this.lblReportSystemType = new DevComponents.DotNetBar.LabelItem();
            this.btReportScaleImperial = new DevComponents.DotNetBar.ButtonItem();
            this.btReportScaleMetric = new DevComponents.DotNetBar.ButtonItem();
            this.lblReportPrecision = new DevComponents.DotNetBar.LabelItem();
            this.btReportScalePrecision64 = new DevComponents.DotNetBar.ButtonItem();
            this.btReportScalePrecision32 = new DevComponents.DotNetBar.ButtonItem();
            this.btReportScalePrecision16 = new DevComponents.DotNetBar.ButtonItem();
            this.btReportScalePrecision8 = new DevComponents.DotNetBar.ButtonItem();
            this.lblReportTheme = new DevComponents.DotNetBar.LabelItem();
            this.btReportSettings = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonPanel = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonBarPrintExport = new DevComponents.DotNetBar.RibbonBar();
            this.btPrintPlan = new DevComponents.DotNetBar.ButtonItem();
            this.lblPrintPlanOptions = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.btPrintPlanFullSize = new DevComponents.DotNetBar.ButtonItem();
            this.btPrintPlanWindow = new DevComponents.DotNetBar.ButtonItem();
            this.btExportPlanToPDF = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarImage = new DevComponents.DotNetBar.RibbonBar();
            this.btBrightnessContrast = new DevComponents.DotNetBar.ButtonItem();
            this.btRotation = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarBrowse = new DevComponents.DotNetBar.RibbonBar();
            this.itemContainerBrowse1 = new DevComponents.DotNetBar.ItemContainer();
            this.lblBrowseGroup = new DevComponents.DotNetBar.LabelItem();
            this.itemContainerBrowse3 = new DevComponents.DotNetBar.ItemContainer();
            this.btBrowsePrevious = new DevComponents.DotNetBar.ButtonItem();
            this.btBrowseNext = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainerBrowse2 = new DevComponents.DotNetBar.ItemContainer();
            this.lblBrowseObjectType = new DevComponents.DotNetBar.LabelItem();
            this.itemContainerBrowse4 = new DevComponents.DotNetBar.ItemContainer();
            this.btBrowseObjectTypePrevious = new DevComponents.DotNetBar.ButtonItem();
            this.btBrowseObjectTypeNext = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarZoom = new DevComponents.DotNetBar.RibbonBar();
            this.btZoomToSelection = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomToWindow = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomActualSize = new DevComponents.DotNetBar.ButtonItem();
            this.btZoom75 = new DevComponents.DotNetBar.ButtonItem();
            this.btZoom50 = new DevComponents.DotNetBar.ButtonItem();
            this.btZoom25 = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomIn = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomOut = new DevComponents.DotNetBar.ButtonItem();
            this.btBookmarks = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomTo75 = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomTo50 = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomTo25 = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomTo150 = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomTo200 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem61 = new DevComponents.DotNetBar.ButtonItem();
            this.btModifyBookmarks = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarAnnotations = new DevComponents.DotNetBar.RibbonBar();
            this.btMarkZone = new DevComponents.DotNetBar.ButtonItem();
            this.btInsertNote = new DevComponents.DotNetBar.ButtonItem();
            this.btInsertPicture = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarTools = new DevComponents.DotNetBar.RibbonBar();
            this.btToolSelection = new DevComponents.DotNetBar.ButtonItem();
            this.btToolPan = new DevComponents.DotNetBar.ButtonItem();
            this.btToolArea = new DevComponents.DotNetBar.ButtonItem();
            this.lblNoArea = new DevComponents.DotNetBar.LabelItem();
            this.lblAreaFilter = new DevComponents.DotNetBar.LabelItem();
            this.itemContainerAreaFilter = new DevComponents.DotNetBar.ItemContainer();
            this.txtAreaFilter = new DevComponents.DotNetBar.TextBoxItem();
            this.btAreaFilterClear = new DevComponents.DotNetBar.ButtonItem();
            this.lblAreaFilterPadding = new DevComponents.DotNetBar.LabelItem();
            this.lblAreaGroups = new DevComponents.DotNetBar.LabelItem();
            this.galleryAreaGroups = new DevComponents.DotNetBar.GalleryContainer();
            this.lblAreaTemplates = new DevComponents.DotNetBar.LabelItem();
            this.galleryAreaTemplates = new DevComponents.DotNetBar.GalleryContainer();
            this.btToolPerimeter = new DevComponents.DotNetBar.ButtonItem();
            this.lblNoPerimeter = new DevComponents.DotNetBar.LabelItem();
            this.lblPerimeterFilter = new DevComponents.DotNetBar.LabelItem();
            this.itemContainerPerimeterFilter = new DevComponents.DotNetBar.ItemContainer();
            this.txtPerimeterFilter = new DevComponents.DotNetBar.TextBoxItem();
            this.btPerimeterFilterClear = new DevComponents.DotNetBar.ButtonItem();
            this.lblPerimeterGroups = new DevComponents.DotNetBar.LabelItem();
            this.galleryPerimeterGroups = new DevComponents.DotNetBar.GalleryContainer();
            this.lblPerimeterTemplates = new DevComponents.DotNetBar.LabelItem();
            this.galleryPerimeterTemplates = new DevComponents.DotNetBar.GalleryContainer();
            this.btToolRuler = new DevComponents.DotNetBar.ButtonItem();
            this.lblNoDistance = new DevComponents.DotNetBar.LabelItem();
            this.lblDistanceFilter = new DevComponents.DotNetBar.LabelItem();
            this.itemContainerDistanceFilter = new DevComponents.DotNetBar.ItemContainer();
            this.txtDistanceFilter = new DevComponents.DotNetBar.TextBoxItem();
            this.btDistanceFilterClear = new DevComponents.DotNetBar.ButtonItem();
            this.lblDistanceGroups = new DevComponents.DotNetBar.LabelItem();
            this.galleryDistanceGroups = new DevComponents.DotNetBar.GalleryContainer();
            this.lblDistanceTemplates = new DevComponents.DotNetBar.LabelItem();
            this.galleryDistanceTemplates = new DevComponents.DotNetBar.GalleryContainer();
            this.btToolCounter = new DevComponents.DotNetBar.ButtonItem();
            this.lblNoCounter = new DevComponents.DotNetBar.LabelItem();
            this.lblCounterFilter = new DevComponents.DotNetBar.LabelItem();
            this.itemContainerCounterFilter = new DevComponents.DotNetBar.ItemContainer();
            this.txtCounterFilter = new DevComponents.DotNetBar.TextBoxItem();
            this.btCounterFilterClear = new DevComponents.DotNetBar.ButtonItem();
            this.lblCounterGroups = new DevComponents.DotNetBar.LabelItem();
            this.galleryCounterGroups = new DevComponents.DotNetBar.GalleryContainer();
            this.lblCounterTemplates = new DevComponents.DotNetBar.LabelItem();
            this.galleryCounterTemplates = new DevComponents.DotNetBar.GalleryContainer();
            this.btToolAngle = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarScale = new DevComponents.DotNetBar.RibbonBar();
            this.btScaleSet = new DevComponents.DotNetBar.ButtonItem();
            this.lblSystemType = new DevComponents.DotNetBar.LabelItem();
            this.btScaleImperial = new DevComponents.DotNetBar.ButtonItem();
            this.btScaleMetric = new DevComponents.DotNetBar.ButtonItem();
            this.lblPrecision = new DevComponents.DotNetBar.LabelItem();
            this.btScalePrecision64 = new DevComponents.DotNetBar.ButtonItem();
            this.btScalePrecision32 = new DevComponents.DotNetBar.ButtonItem();
            this.btScalePrecision16 = new DevComponents.DotNetBar.ButtonItem();
            this.btScalePrecision8 = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarEdit = new DevComponents.DotNetBar.RibbonBar();
            this.btEditPaste = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainerEdit = new DevComponents.DotNetBar.ItemContainer();
            this.btEditCut = new DevComponents.DotNetBar.ButtonItem();
            this.btEditCopy = new DevComponents.DotNetBar.ButtonItem();
            this.btEditDelete = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainerUndoRedo = new DevComponents.DotNetBar.ItemContainer();
            this.btEditUndo = new DevComponents.DotNetBar.ButtonItem();
            this.btEditRedo = new DevComponents.DotNetBar.ButtonItem();
            this.btEditSendData = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarLayout = new DevComponents.DotNetBar.RibbonBar();
            this.itemContainerLayouts = new DevComponents.DotNetBar.ItemContainer();
            this.opTakeoffLayout = new DevComponents.DotNetBar.CheckBoxItem();
            this.opEstimatingLayout = new DevComponents.DotNetBar.CheckBoxItem();
            this.ribbonPanelTemplates = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonTemplate = new DevComponents.DotNetBar.RibbonBar();
            this.btTemplateModify = new DevComponents.DotNetBar.ButtonItem();
            this.btTemplateDuplicate = new DevComponents.DotNetBar.ButtonItem();
            this.btTemplateDelete = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTemplateCreate = new DevComponents.DotNetBar.RibbonBar();
            this.btTemplateArea = new DevComponents.DotNetBar.ButtonItem();
            this.btTemplatePerimeter = new DevComponents.DotNetBar.ButtonItem();
            this.btTemplateLength = new DevComponents.DotNetBar.ButtonItem();
            this.btTemplateCounter = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTemplateDatabase = new DevComponents.DotNetBar.RibbonBar();
            this.btTemplateTradesPackages = new DevComponents.DotNetBar.ButtonItem();
            this.btTemplateCompactDatabase = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonPanelPlans = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonBarMultiPlans = new DevComponents.DotNetBar.RibbonBar();
            this.btPlansPrint = new DevComponents.DotNetBar.ButtonItem();
            this.btPlansExport = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarPlans = new DevComponents.DotNetBar.RibbonBar();
            this.btPlanLoad = new DevComponents.DotNetBar.ButtonItem();
            this.btPlanProperties = new DevComponents.DotNetBar.ButtonItem();
            this.btPlanRemove = new DevComponents.DotNetBar.ButtonItem();
            this.btPlanExport = new DevComponents.DotNetBar.ButtonItem();
            this.btPlanDuplicate = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBarPlansInsert = new DevComponents.DotNetBar.RibbonBar();
            this.btPlanInsertFromPDF = new DevComponents.DotNetBar.ButtonItem();
            this.iblImportDPI = new DevComponents.DotNetBar.LabelItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.op172Dpi = new DevComponents.DotNetBar.CheckBoxItem();
            this.op300Dpi = new DevComponents.DotNetBar.CheckBoxItem();
            this.opOtherDpi = new DevComponents.DotNetBar.CheckBoxItem();
            this.sliderDpi = new DevComponents.DotNetBar.SliderItem();
            this.itemContainerDpi = new DevComponents.DotNetBar.ItemContainer();
            this.lblDpi1 = new DevComponents.DotNetBar.LabelItem();
            this.labelDpiPadding1 = new DevComponents.DotNetBar.LabelItem();
            this.lblDpi2 = new DevComponents.DotNetBar.LabelItem();
            this.iblImportColorManagement = new DevComponents.DotNetBar.LabelItem();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this.opConvertToColor = new DevComponents.DotNetBar.CheckBoxItem();
            this.btPlanInsertFromImage = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonPanelExtensions = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonExtension = new DevComponents.DotNetBar.RibbonBar();
            this.btExtensionModify = new DevComponents.DotNetBar.ButtonItem();
            this.btExtensionDuplicate = new DevComponents.DotNetBar.ButtonItem();
            this.btExtensionDelete = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonExtensionCreate = new DevComponents.DotNetBar.RibbonBar();
            this.btExtensionArea = new DevComponents.DotNetBar.ButtonItem();
            this.btExtensionPerimeter = new DevComponents.DotNetBar.ButtonItem();
            this.btExtensionRuler = new DevComponents.DotNetBar.ButtonItem();
            this.btExtensionCounter = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonExtensionDatabase = new DevComponents.DotNetBar.RibbonBar();
            this.btExtensionTradesPackages = new DevComponents.DotNetBar.ButtonItem();
            this.btExtensionCompactDatabase = new DevComponents.DotNetBar.ButtonItem();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.bEditPopup = new DevComponents.DotNetBar.ButtonItem();
            this.bAutoAdjustToZone = new DevComponents.DotNetBar.ButtonItem();
            this.bEditNote = new DevComponents.DotNetBar.ButtonItem();
            this.bPointInsert = new DevComponents.DotNetBar.ButtonItem();
            this.bPointRemove = new DevComponents.DotNetBar.ButtonItem();
            this.bSetHeight = new DevComponents.DotNetBar.ButtonItem();
            this.bGroupAddObject = new DevComponents.DotNetBar.ButtonItem();
            this.bDeductionCreate = new DevComponents.DotNetBar.ButtonItem();
            this.bDeductionsEdit = new DevComponents.DotNetBar.ButtonItem();
            this.bPerimeterCreateFromArea = new DevComponents.DotNetBar.ButtonItem();
            this.bOpeningCreateFromPosition = new DevComponents.DotNetBar.ButtonItem();
            this.bOpeningDuplicate = new DevComponents.DotNetBar.ButtonItem();
            this.bOpeningCreateFromSegment = new DevComponents.DotNetBar.ButtonItem();
            this.bOpeningDelete = new DevComponents.DotNetBar.ButtonItem();
            this.bDropInsert = new DevComponents.DotNetBar.ButtonItem();
            this.bDropRemove = new DevComponents.DotNetBar.ButtonItem();
            this.bPerimeterOpen = new DevComponents.DotNetBar.ButtonItem();
            this.bPerimeterClose = new DevComponents.DotNetBar.ButtonItem();
            this.bAngleDegreeType = new DevComponents.DotNetBar.ButtonItem();
            this.bAngleSlopeType = new DevComponents.DotNetBar.ButtonItem();
            this.bDeductionDuplicate = new DevComponents.DotNetBar.ButtonItem();
            this.bCut = new DevComponents.DotNetBar.ButtonItem();
            this.bCopy = new DevComponents.DotNetBar.ButtonItem();
            this.bPaste = new DevComponents.DotNetBar.ButtonItem();
            this.bDelete = new DevComponents.DotNetBar.ButtonItem();
            this.bToggleMeasures = new DevComponents.DotNetBar.ButtonItem();
            this.bZoomToObject = new DevComponents.DotNetBar.ButtonItem();
            this.bZoomToGroup = new DevComponents.DotNetBar.ButtonItem();
            this.bBringToFront = new DevComponents.DotNetBar.ButtonItem();
            this.bSendToBack = new DevComponents.DotNetBar.ButtonItem();
            this.bSelectGroup = new DevComponents.DotNetBar.ButtonItem();
            this.bSelectThisGroup = new DevComponents.DotNetBar.ButtonItem();
            this.bSelectThisGroup1 = new DevComponents.DotNetBar.ButtonItem();
            this.bSelectObjectType = new DevComponents.DotNetBar.ButtonItem();
            this.bSelectObjectType1 = new DevComponents.DotNetBar.ButtonItem();
            this.bSelectAll = new DevComponents.DotNetBar.ButtonItem();
            this.bUnselectAll = new DevComponents.DotNetBar.ButtonItem();
            this.bLayerMoveTo = new DevComponents.DotNetBar.ButtonItem();
            this.bLayerMoveTo1 = new DevComponents.DotNetBar.ButtonItem();
            this.bGroupMoveTo = new DevComponents.DotNetBar.ButtonItem();
            this.bGroupMoveTo1 = new DevComponents.DotNetBar.ButtonItem();
            this.bGroupMoveToNew = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTabStart = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabPlans = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabReport = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabEstimatingItems = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabItemDBManagement = new DevComponents.DotNetBar.RibbonTabItemGroup();
            this.ribbonTabTemplates = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabExtensions = new DevComponents.DotNetBar.RibbonTabItem();
            this.lblTrialMessage = new DevComponents.DotNetBar.ButtonItem();
            this.btLicensing = new DevComponents.DotNetBar.ButtonItem();
            this.btLicenseBuy = new DevComponents.DotNetBar.ButtonItem();
            this.btLicenseActivate = new DevComponents.DotNetBar.ButtonItem();
            this.btSettings = new DevComponents.DotNetBar.ButtonItem();
            this.lblLanguage = new DevComponents.DotNetBar.LabelItem();
            this.btLanguageEnglish = new DevComponents.DotNetBar.ButtonItem();
            this.btLanguageFrench = new DevComponents.DotNetBar.ButtonItem();
            this.btLanguageSpanish = new DevComponents.DotNetBar.ButtonItem();
            this.lblScrollSpeed = new DevComponents.DotNetBar.LabelItem();
            this.sliderScrollSpeed = new DevComponents.DotNetBar.SliderItem();
            this.containerScroll = new DevComponents.DotNetBar.ItemContainer();
            this.lblScrollFast = new DevComponents.DotNetBar.LabelItem();
            this.lblScrollPadding = new DevComponents.DotNetBar.LabelItem();
            this.lblScrollSlow = new DevComponents.DotNetBar.LabelItem();
            this.lblDataFolder = new DevComponents.DotNetBar.LabelItem();
            this.lblPersonalPreferences = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.btSelectDataFolder = new DevComponents.DotNetBar.ButtonItem();
            this.btPersonalPreferences = new DevComponents.DotNetBar.ButtonItem();
            this.btImportationPreferences = new DevComponents.DotNetBar.ButtonItem();
            this.btEnableAutoBackup = new DevComponents.DotNetBar.ButtonItem();
            this.btSetDBReadOnly = new DevComponents.DotNetBar.ButtonItem();
            this.lblTheme = new DevComponents.DotNetBar.LabelItem();
            this.btStyleMetro = new DevComponents.DotNetBar.ButtonItem();
            this.AppCommandTheme = new DevComponents.DotNetBar.Command(this.components);
            this.btStyleClassicBlue = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleClassicSilver = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleClassicBlack = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleClassicExecutive = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleRetroBlue = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleRetroSilver = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleRetroBlack = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleRetroGlass = new DevComponents.DotNetBar.ButtonItem();
            this.btStyleModern = new DevComponents.DotNetBar.ButtonItem();
            this.btSetThemeColor = new DevComponents.DotNetBar.ColorPickerDropDown();
            this.lblPanels = new DevComponents.DotNetBar.LabelItem();
            this.btResetDefaultPanelsLayout = new DevComponents.DotNetBar.ButtonItem();
            this.btHelp = new DevComponents.DotNetBar.ButtonItem();
            this.startButton = new DevComponents.DotNetBar.Office2007StartButton();
            this.itemContainerFileMenu = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainerFileMenu2 = new DevComponents.DotNetBar.ItemContainer();
            this.btProjectNew = new DevComponents.DotNetBar.ButtonItem();
            this.btProjectOpen = new DevComponents.DotNetBar.ButtonItem();
            this.btProjectSave = new DevComponents.DotNetBar.ButtonItem();
            this.btProjectSaveAs = new DevComponents.DotNetBar.ButtonItem();
            this.btProjectInfo = new DevComponents.DotNetBar.ButtonItem();
            this.btProjectClose = new DevComponents.DotNetBar.ButtonItem();
            this.btHelpContent = new DevComponents.DotNetBar.ButtonItem();
            this.btHelpYoutube = new DevComponents.DotNetBar.ButtonItem();
            this.btHelpAbout = new DevComponents.DotNetBar.ButtonItem();
            this.btLicenseDeactivate = new DevComponents.DotNetBar.ButtonItem();
            this.btExit = new DevComponents.DotNetBar.ButtonItem();
            this.galleryRecentProjects = new DevComponents.DotNetBar.GalleryContainer();
            this.lblFileRecentProjects = new DevComponents.DotNetBar.LabelItem();
            this.btSave = new DevComponents.DotNetBar.ButtonItem();
            this.btUndo = new DevComponents.DotNetBar.ButtonItem();
            this.btRedo = new DevComponents.DotNetBar.ButtonItem();
            this.galleryGroup1 = new DevComponents.DotNetBar.GalleryGroup();
            this.barStatus = new DevComponents.DotNetBar.Bar();
            this.lblStatus = new DevComponents.DotNetBar.LabelItem();
            this.lblStatusBarPadding = new DevComponents.DotNetBar.LabelItem();
            this.lblOrtho = new DevComponents.DotNetBar.LabelItem();
            this.switchOrtho = new DevComponents.DotNetBar.SwitchButtonItem();
            this.lblStatusBarPadding2 = new DevComponents.DotNetBar.LabelItem();
            this.lblImageQuality = new DevComponents.DotNetBar.LabelItem();
            this.qualitySlider = new DevComponents.DotNetBar.SliderItem();
            this.lblStatusBarPadding3 = new DevComponents.DotNetBar.LabelItem();
            this.lblZoom = new DevComponents.DotNetBar.LabelItem();
            this.lblStatusPadding2 = new DevComponents.DotNetBar.LabelItem();
            this.zoomSlider = new DevComponents.DotNetBar.SliderItem();
            this.lstLayers = new DevComponents.AdvTree.AdvTree();
            this.columnLayerVisible = new DevComponents.AdvTree.ColumnHeader();
            this.columnLayerName = new DevComponents.AdvTree.ColumnHeader();
            this.columnLayerOpacity = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyle8 = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.barLayers = new DevComponents.DotNetBar.Bar();
            this.btLayerAdd = new DevComponents.DotNetBar.ButtonItem();
            this.btLayerRemove = new DevComponents.DotNetBar.ButtonItem();
            this.btLayerRename = new DevComponents.DotNetBar.ButtonItem();
            this.btLayerMoveUp = new DevComponents.DotNetBar.ButtonItem();
            this.btLayerMoveDown = new DevComponents.DotNetBar.ButtonItem();
            this.btLayerSaveList = new DevComponents.DotNetBar.ButtonItem();
            this.btLayerSaveListAs = new DevComponents.DotNetBar.ButtonItem();
            this.btLayerOpenList = new DevComponents.DotNetBar.ButtonItem();
            this.btLayersToggle = new DevComponents.DotNetBar.ButtonItem();
            this.btLayersMakeVisible = new DevComponents.DotNetBar.ButtonItem();
            this.btLayersMakeInvisible = new DevComponents.DotNetBar.ButtonItem();
            this.barDisplayResults = new DevComponents.DotNetBar.Bar();
            this.lblDisplayResults = new DevComponents.DotNetBar.LabelItem();
            this.btDisplayResultsForThisPlan = new DevComponents.DotNetBar.ButtonItem();
            this.lblDisplayResultsPadding = new DevComponents.DotNetBar.LabelItem();
            this.btDisplayResultsForAllPlans = new DevComponents.DotNetBar.ButtonItem();
            this.tabProperties = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.superTabProperties = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.gridObjectProperties = new DevComponents.DotNetBar.AdvPropertyGrid();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.extensionsManager = new QuoterPlan.ExtensionsManager();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.cbObjects = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panControl = new QuoterPlanControls.PanControl();
            this.lstRecentPlans = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader5 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyle4 = new DevComponents.DotNetBar.ElementStyle();
            this.barRecentPlans = new DevComponents.DotNetBar.Bar();
            this.btPlanRename = new DevComponents.DotNetBar.ButtonItem();
            this.treeObjects = new DevComponents.AdvTree.AdvTree();
            this.columnObjectIcon = new DevComponents.AdvTree.ColumnHeader();
            this.columnObjectName = new DevComponents.AdvTree.ColumnHeader();
            this.columnObjectInfo = new DevComponents.AdvTree.ColumnHeader();
            this.columnObjectColor = new DevComponents.AdvTree.ColumnHeader();
            this.columnObjectVisible = new DevComponents.AdvTree.ColumnHeader();
            this.columnObjectPadding = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle7 = new DevComponents.DotNetBar.ElementStyle();
            this.panelPlanName = new System.Windows.Forms.Panel();
            this.txtPlanName = new QuoterPlan.TextBoxEx();
            this.cbPlans = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.barGroups = new DevComponents.DotNetBar.Bar();
            this.btGroupLocate = new DevComponents.DotNetBar.ButtonItem();
            this.btZoomToObject = new DevComponents.DotNetBar.ButtonItem();
            this.btGroupSelect = new DevComponents.DotNetBar.ButtonItem();
            this.btGroupRemove = new DevComponents.DotNetBar.ButtonItem();
            this.btGroupRename = new DevComponents.DotNetBar.ButtonItem();
            this.btRenamePlan = new DevComponents.DotNetBar.ButtonItem();
            this.btGroupsToggle = new DevComponents.DotNetBar.ButtonItem();
            this.btGroupsMakeVisible = new DevComponents.DotNetBar.ButtonItem();
            this.btGroupsMakeInvisible = new DevComponents.DotNetBar.ButtonItem();
            this.checkBoxItem1 = new DevComponents.DotNetBar.CheckBoxItem();
            this.sliderItem1 = new DevComponents.DotNetBar.SliderItem();
            this.checkBoxItem2 = new DevComponents.DotNetBar.CheckBoxItem();
            this.sliderItem2 = new DevComponents.DotNetBar.SliderItem();
            this.timer1 = new System.Timers.Timer();
            this.itemContainer5 = new DevComponents.DotNetBar.ItemContainer();
            this.galleryGroupArea = new DevComponents.DotNetBar.GalleryGroup();
            this.galleryGroupPerimeter = new DevComponents.DotNetBar.GalleryGroup();
            this.galleryGroupCounter = new DevComponents.DotNetBar.GalleryGroup();
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.flowPlans = new System.Windows.Forms.FlowLayoutPanel();
            this.sliderItem4 = new DevComponents.DotNetBar.SliderItem();
            this.itemContainerBrightness = new DevComponents.DotNetBar.ItemContainer();
            this.lblBrightness = new DevComponents.DotNetBar.LabelItem();
            this.sliderBrightness = new DevComponents.DotNetBar.SliderItem();
            this.lblBrightnessContrastPadding1 = new DevComponents.DotNetBar.LabelItem();
            this.itemContainerContrast = new DevComponents.DotNetBar.ItemContainer();
            this.lblContrast = new DevComponents.DotNetBar.LabelItem();
            this.sliderContrast = new DevComponents.DotNetBar.SliderItem();
            this.ribbonBarBrightnessContrast = new DevComponents.DotNetBar.RibbonBar();
            this.lblBrightnessContrastPadding2 = new DevComponents.DotNetBar.LabelItem();
            this.lblBrightnessContrastSeparator = new DevComponents.DotNetBar.LabelItem();
            this.lblBrightnessContrastPadding3 = new DevComponents.DotNetBar.LabelItem();
            this.btBrightnessContrastApply = new DevComponents.DotNetBar.ButtonItem();
            this.btBrightnessContrastCancel = new DevComponents.DotNetBar.ButtonItem();
            this.btBrightnessContrastRestore = new DevComponents.DotNetBar.ButtonItem();
            this.panelBrightnessContrast = new DevComponents.DotNetBar.PanelEx();
            this.panelRotation = new DevComponents.DotNetBar.PanelEx();
            this.ribbonBarRotation = new DevComponents.DotNetBar.RibbonBar();
            this.btFlipHorizontally = new DevComponents.DotNetBar.ButtonItem();
            this.btFlipVertically = new DevComponents.DotNetBar.ButtonItem();
            this.btRotateLeft = new DevComponents.DotNetBar.ButtonItem();
            this.btRotateRight = new DevComponents.DotNetBar.ButtonItem();
            this.lblBarRotationPadding1 = new DevComponents.DotNetBar.LabelItem();
            this.lblBarRotationSeparator = new DevComponents.DotNetBar.LabelItem();
            this.lblBarRotationPadding2 = new DevComponents.DotNetBar.LabelItem();
            this.btRotationApply = new DevComponents.DotNetBar.ButtonItem();
            this.btRotationCancel = new DevComponents.DotNetBar.ButtonItem();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.panelWelcome = new System.Windows.Forms.Panel();
            this.panelWelcomeMenu = new DevComponents.DotNetBar.PanelEx();
            this.lblRecentProjects = new QuoterPlan.LabelEx();
            this.btNew = new DevComponents.DotNetBar.ButtonX();
            this.lstRecentProjects = new DevComponents.AdvTree.AdvTree();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader4 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector4 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle5 = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyle6 = new DevComponents.DotNetBar.ElementStyle();
            this.btOpen = new DevComponents.DotNetBar.ButtonX();
            this.picWelcome = new System.Windows.Forms.PictureBox();
            this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
            this.panelPlansAction = new DevComponents.DotNetBar.PanelEx();
            this.ribbonBarPlansAction = new DevComponents.DotNetBar.RibbonBar();
            this.progressPlansAction = new DevComponents.DotNetBar.CircularProgressItem();
            this.itemContainerExportType = new DevComponents.DotNetBar.ItemContainer();
            this.lblBarPlansActionPadding3 = new DevComponents.DotNetBar.LabelItem();
            this.checkBoxExportSingleFile = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxExportMultiFiles = new DevComponents.DotNetBar.CheckBoxItem();
            this.lblBarPlansActionPadding4 = new DevComponents.DotNetBar.LabelItem();
            this.btPlansActionSelectAll = new DevComponents.DotNetBar.ButtonItem();
            this.btPlansActionSelectNone = new DevComponents.DotNetBar.ButtonItem();
            this.lblBarPlansActionPadding1 = new DevComponents.DotNetBar.LabelItem();
            this.lblPlansActionSeparator = new DevComponents.DotNetBar.LabelItem();
            this.lblBarPlansActionPadding2 = new DevComponents.DotNetBar.LabelItem();
            this.btPlansActionApply = new DevComponents.DotNetBar.ButtonItem();
            this.btPlansActionCancel = new DevComponents.DotNetBar.ButtonItem();
            this.mainControl = new QuoterPlanControls.MainControl();
            this.dotNetBarManager = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.containerBarLayers = new DevComponents.DotNetBar.Bar();
            this.panelDockLayers = new DevComponents.DotNetBar.PanelDockContainer();
            this.dockContainerItemLayers = new DevComponents.DotNetBar.DockContainerItem();
            this.containerBarNavigation = new DevComponents.DotNetBar.Bar();
            this.panelDockPreview = new DevComponents.DotNetBar.PanelDockContainer();
            this.dockContainerItemPreview = new DevComponents.DotNetBar.DockContainerItem();
            this.containerBarProperties = new DevComponents.DotNetBar.Bar();
            this.panelDockProperties = new DevComponents.DotNetBar.PanelDockContainer();
            this.dockContainerItemProperties = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.containerBarGroups = new DevComponents.DotNetBar.Bar();
            this.panelDockGroups = new DevComponents.DotNetBar.PanelDockContainer();
            this.dockContainerItemGroups = new DevComponents.DotNetBar.DockContainerItem();
            this.containerBarRecentPlans = new DevComponents.DotNetBar.Bar();
            this.panelDockRecentPlans = new DevComponents.DotNetBar.PanelDockContainer();
            this.dockContainerItemRecentPlans = new DevComponents.DotNetBar.DockContainerItem();
            this.containerBarEstimating = new DevComponents.DotNetBar.Bar();
            this.panelDockEstimating = new DevComponents.DotNetBar.PanelDockContainer();
            this.treeEstimatingItems = new DevExpress.XtraTreeList.TreeList();
            this.barEstimatingItems = new DevComponents.DotNetBar.Bar();
            this.btEstimatingItemsExpandAll = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingItemsCollapseAll = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingItemsUpdatePrices = new DevComponents.DotNetBar.ButtonItem();
            this.btEstimatingItemsPrint = new DevComponents.DotNetBar.ButtonItem();
            this.dockContainerItemEstimating = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.lblNoPlan = new QuoterPlan.LabelEx();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.reportsControl = new QuoterPlan.ReportsControl();
            this.dockContainerEstimating = new DevComponents.DotNetBar.DockContainerItem();
            this.treeDBEstimatingItems = new DevExpress.XtraTreeList.TreeList();
            this.treeTemplatesLibrary = new DevExpress.XtraTreeList.TreeList();
            this.ribbonControl.SuspendLayout();
            this.ribbonPanelEstimating.SuspendLayout();
            this.ribbonPanelReport.SuspendLayout();
            this.ribbonPanel.SuspendLayout();
            this.ribbonPanelTemplates.SuspendLayout();
            this.ribbonPanelPlans.SuspendLayout();
            this.ribbonPanelExtensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDisplayResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabProperties)).BeginInit();
            this.tabProperties.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.superTabProperties)).BeginInit();
            this.superTabProperties.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridObjectProperties)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstRecentPlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barRecentPlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeObjects)).BeginInit();
            this.panelPlanName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.panelBrightnessContrast.SuspendLayout();
            this.panelRotation.SuspendLayout();
            this.panelWelcome.SuspendLayout();
            this.panelWelcomeMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstRecentProjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWelcome)).BeginInit();
            this.panelPlansAction.SuspendLayout();
            this.dockSite1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerBarLayers)).BeginInit();
            this.containerBarLayers.SuspendLayout();
            this.panelDockLayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerBarNavigation)).BeginInit();
            this.containerBarNavigation.SuspendLayout();
            this.panelDockPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerBarProperties)).BeginInit();
            this.containerBarProperties.SuspendLayout();
            this.panelDockProperties.SuspendLayout();
            this.dockSite2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerBarGroups)).BeginInit();
            this.containerBarGroups.SuspendLayout();
            this.panelDockGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerBarRecentPlans)).BeginInit();
            this.containerBarRecentPlans.SuspendLayout();
            this.panelDockRecentPlans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerBarEstimating)).BeginInit();
            this.containerBarEstimating.SuspendLayout();
            this.panelDockEstimating.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeEstimatingItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEstimatingItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDBEstimatingItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeTemplatesLibrary)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.AutoExpand = false;
            this.ribbonControl.AutoKeyboardExpand = false;
            this.ribbonControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonControl.CanCustomize = false;
            this.ribbonControl.CaptionVisible = true;
            this.ribbonControl.Controls.Add(this.ribbonPanelEstimating);
            this.ribbonControl.Controls.Add(this.ribbonPanelReport);
            this.ribbonControl.Controls.Add(this.ribbonPanel);
            this.ribbonControl.Controls.Add(this.ribbonPanelTemplates);
            this.ribbonControl.Controls.Add(this.ribbonPanelPlans);
            this.ribbonControl.Controls.Add(this.ribbonPanelExtensions);
            this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonControl.ForeColor = System.Drawing.Color.Black;
            this.ribbonControl.GlobalContextMenuBar = this.contextMenuBar1;
            this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabStart,
            this.ribbonTabPlans,
            this.ribbonTabReport,
            this.ribbonTabEstimatingItems,
            this.ribbonTabTemplates,
            this.ribbonTabExtensions,
            this.lblTrialMessage,
            this.btLicensing,
            this.btSettings,
            this.btHelp});
            this.ribbonControl.Location = new System.Drawing.Point(5, 1);
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.ribbonControl.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.startButton,
            this.btSave,
            this.btUndo,
            this.btRedo});
            this.ribbonControl.Size = new System.Drawing.Size(1729, 170);
            this.ribbonControl.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.ribbonControl.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.ribbonControl.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.ribbonControl.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.ribbonControl.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.ribbonControl.SystemText.QatDialogAddButton = "&Add >>";
            this.ribbonControl.SystemText.QatDialogCancelButton = "Cancel";
            this.ribbonControl.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.ribbonControl.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.ribbonControl.SystemText.QatDialogOkButton = "OK";
            this.ribbonControl.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl.SystemText.QatDialogRemoveButton = "&Remove";
            this.ribbonControl.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.ribbonControl.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.ribbonControl.TabGroupHeight = 14;
            this.ribbonControl.TabGroups.AddRange(new DevComponents.DotNetBar.RibbonTabItemGroup[] {
            this.ribbonTabItemDBManagement});
            this.ribbonControl.TabGroupsVisible = true;
            this.ribbonControl.TabIndex = 0;
            this.ribbonControl.Text = "Quoter Plan";
            this.ribbonControl.UseCustomizeDialog = false;
            this.ribbonControl.SelectedRibbonTabChanged += new System.EventHandler(this.ribbonControl_SelectedRibbonTabChanged);
            // 
            // ribbonPanelEstimating
            // 
            this.ribbonPanelEstimating.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimating);
            this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimatingNew);
            this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimatingDatabase);
            this.ribbonPanelEstimating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanelEstimating.Location = new System.Drawing.Point(0, 57);
            this.ribbonPanelEstimating.Name = "ribbonPanelEstimating";
            this.ribbonPanelEstimating.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanelEstimating.Size = new System.Drawing.Size(1729, 110);
            // 
            // 
            // 
            this.ribbonPanelEstimating.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelEstimating.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelEstimating.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanelEstimating.TabIndex = 4;
            this.ribbonPanelEstimating.Visible = true;
            // 
            // ribbonEstimating
            // 
            this.ribbonEstimating.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonEstimating.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonEstimating.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonEstimating.ContainerControlProcessDialogKey = true;
            this.ribbonEstimating.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonEstimating.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btEstimatingModify,
            this.btEstimatingDuplicate,
            this.btEstimatingDelete});
            this.ribbonEstimating.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonEstimating.Location = new System.Drawing.Point(484, 0);
            this.ribbonEstimating.Name = "ribbonEstimating";
            this.ribbonEstimating.Size = new System.Drawing.Size(146, 107);
            this.ribbonEstimating.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonEstimating.TabIndex = 9;
            this.ribbonEstimating.Text = "Selected Item";
            // 
            // 
            // 
            this.ribbonEstimating.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonEstimating.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btEstimatingModify
            // 
            this.btEstimatingModify.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingModify.CanCustomize = false;
            this.btEstimatingModify.Image = global::QuoterPlan.Properties.Resources.properties;
            this.btEstimatingModify.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingModify.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingModify.Name = "btEstimatingModify";
            this.btEstimatingModify.Text = "Modify";
            this.btEstimatingModify.Click += new System.EventHandler(this.btEstimatingModify_Click);
            // 
            // btEstimatingDuplicate
            // 
            this.btEstimatingDuplicate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingDuplicate.CanCustomize = false;
            this.btEstimatingDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingDuplicate.Image")));
            this.btEstimatingDuplicate.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingDuplicate.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingDuplicate.Name = "btEstimatingDuplicate";
            this.btEstimatingDuplicate.Text = "Duplicate";
            this.btEstimatingDuplicate.Click += new System.EventHandler(this.btEstimatingDuplicate_Click);
            // 
            // btEstimatingDelete
            // 
            this.btEstimatingDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingDelete.CanCustomize = false;
            this.btEstimatingDelete.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingDelete.Image")));
            this.btEstimatingDelete.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingDelete.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingDelete.Name = "btEstimatingDelete";
            this.btEstimatingDelete.Text = "Delete";
            this.btEstimatingDelete.Click += new System.EventHandler(this.btEstimatingDelete_Click);
            // 
            // ribbonEstimatingNew
            // 
            this.ribbonEstimatingNew.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonEstimatingNew.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonEstimatingNew.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonEstimatingNew.CanCustomize = false;
            this.ribbonEstimatingNew.ContainerControlProcessDialogKey = true;
            this.ribbonEstimatingNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonEstimatingNew.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btEstimatingMaterial,
            this.btEstimatingLabour,
            this.btEstimatingEquipment,
            this.btEstimatingSubcontract});
            this.ribbonEstimatingNew.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonEstimatingNew.Location = new System.Drawing.Point(256, 0);
            this.ribbonEstimatingNew.Name = "ribbonEstimatingNew";
            this.ribbonEstimatingNew.Size = new System.Drawing.Size(228, 107);
            this.ribbonEstimatingNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonEstimatingNew.TabIndex = 8;
            this.ribbonEstimatingNew.Text = "New Estimating Item";
            // 
            // 
            // 
            this.ribbonEstimatingNew.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonEstimatingNew.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btEstimatingMaterial
            // 
            this.btEstimatingMaterial.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingMaterial.CanCustomize = false;
            this.btEstimatingMaterial.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingMaterial.Image")));
            this.btEstimatingMaterial.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingMaterial.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingMaterial.Name = "btEstimatingMaterial";
            this.btEstimatingMaterial.Text = "Material\r\n";
            this.btEstimatingMaterial.Click += new System.EventHandler(this.btEstimatingMaterial_Click);
            // 
            // btEstimatingLabour
            // 
            this.btEstimatingLabour.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingLabour.CanCustomize = false;
            this.btEstimatingLabour.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingLabour.Image")));
            this.btEstimatingLabour.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingLabour.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingLabour.Name = "btEstimatingLabour";
            this.btEstimatingLabour.Text = "Labor\r\n";
            this.btEstimatingLabour.Click += new System.EventHandler(this.btEstimatingLabour_Click);
            // 
            // btEstimatingEquipment
            // 
            this.btEstimatingEquipment.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingEquipment.CanCustomize = false;
            this.btEstimatingEquipment.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingEquipment.Image")));
            this.btEstimatingEquipment.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingEquipment.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingEquipment.Name = "btEstimatingEquipment";
            this.btEstimatingEquipment.Text = "Equipment\r\n";
            this.btEstimatingEquipment.Click += new System.EventHandler(this.btEstimatingEquipment_Click);
            // 
            // btEstimatingSubcontract
            // 
            this.btEstimatingSubcontract.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingSubcontract.CanCustomize = false;
            this.btEstimatingSubcontract.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingSubcontract.Image")));
            this.btEstimatingSubcontract.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingSubcontract.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingSubcontract.Name = "btEstimatingSubcontract";
            this.btEstimatingSubcontract.Text = "Subcontract\r\n";
            this.btEstimatingSubcontract.Click += new System.EventHandler(this.btEstimatingSubcontract_Click);
            // 
            // ribbonEstimatingDatabase
            // 
            this.ribbonEstimatingDatabase.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonEstimatingDatabase.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonEstimatingDatabase.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonEstimatingDatabase.ContainerControlProcessDialogKey = true;
            this.ribbonEstimatingDatabase.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonEstimatingDatabase.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btEstimatingTradesPackages,
            this.btEstimatingSaveDatabase,
            this.btEstimatingCompactDatabase,
            this.btEstimatingImportPrices});
            this.ribbonEstimatingDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonEstimatingDatabase.Location = new System.Drawing.Point(3, 0);
            this.ribbonEstimatingDatabase.Name = "ribbonEstimatingDatabase";
            this.ribbonEstimatingDatabase.Size = new System.Drawing.Size(253, 107);
            this.ribbonEstimatingDatabase.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonEstimatingDatabase.TabIndex = 7;
            this.ribbonEstimatingDatabase.Text = "Database";
            // 
            // 
            // 
            this.ribbonEstimatingDatabase.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonEstimatingDatabase.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btEstimatingTradesPackages
            // 
            this.btEstimatingTradesPackages.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingTradesPackages.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingTradesPackages.Image")));
            this.btEstimatingTradesPackages.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingTradesPackages.ImagePaddingHorizontal = 10;
            this.btEstimatingTradesPackages.ImagePaddingVertical = 5;
            this.btEstimatingTradesPackages.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingTradesPackages.Name = "btEstimatingTradesPackages";
            this.btEstimatingTradesPackages.Text = "Trade Packages";
            this.btEstimatingTradesPackages.Visible = false;
            // 
            // btEstimatingSaveDatabase
            // 
            this.btEstimatingSaveDatabase.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingSaveDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingSaveDatabase.Image")));
            this.btEstimatingSaveDatabase.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingSaveDatabase.ImagePaddingVertical = 5;
            this.btEstimatingSaveDatabase.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingSaveDatabase.Name = "btEstimatingSaveDatabase";
            this.btEstimatingSaveDatabase.Text = "Save Database";
            this.btEstimatingSaveDatabase.Click += new System.EventHandler(this.btEstimatingSaveDatabase_Click);
            // 
            // btEstimatingCompactDatabase
            // 
            this.btEstimatingCompactDatabase.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingCompactDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingCompactDatabase.Image")));
            this.btEstimatingCompactDatabase.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingCompactDatabase.ImagePaddingVertical = 5;
            this.btEstimatingCompactDatabase.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingCompactDatabase.Name = "btEstimatingCompactDatabase";
            this.btEstimatingCompactDatabase.Text = "Compact Database";
            this.btEstimatingCompactDatabase.Visible = false;
            // 
            // btEstimatingImportPrices
            // 
            this.btEstimatingImportPrices.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEstimatingImportPrices.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingImportPrices.Image")));
            this.btEstimatingImportPrices.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEstimatingImportPrices.ImagePaddingVertical = 5;
            this.btEstimatingImportPrices.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEstimatingImportPrices.Name = "btEstimatingImportPrices";
            this.btEstimatingImportPrices.Text = "Import Prices";
            this.btEstimatingImportPrices.Click += new System.EventHandler(this.btEstimatingImportPrices_Click);
            // 
            // ribbonPanelReport
            // 
            this.ribbonPanelReport.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelReport.Controls.Add(this.ribbonExportReport);
            this.ribbonPanelReport.Controls.Add(this.ribbonPrintReport);
            this.ribbonPanelReport.Controls.Add(this.ribbonReportOrder);
            this.ribbonPanelReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanelReport.Location = new System.Drawing.Point(0, 57);
            this.ribbonPanelReport.Name = "ribbonPanelReport";
            this.ribbonPanelReport.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanelReport.Size = new System.Drawing.Size(1729, 110);
            // 
            // 
            // 
            this.ribbonPanelReport.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelReport.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelReport.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanelReport.TabIndex = 3;
            this.ribbonPanelReport.Visible = false;
            // 
            // ribbonExportReport
            // 
            this.ribbonExportReport.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonExportReport.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExportReport.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonExportReport.CanCustomize = false;
            this.ribbonExportReport.ContainerControlProcessDialogKey = true;
            this.ribbonExportReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonExportReport.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btReportExportToExcel,
            this.btReportExportToCSV,
            this.btReportExportToXML,
            this.btReportExportToHTML,
            this.btReportExportToPDF,
            this.btReportExportToEE,
            this.btReportExportToCOffice});
            this.ribbonExportReport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExportReport.Location = new System.Drawing.Point(247, 0);
            this.ribbonExportReport.Name = "ribbonExportReport";
            this.ribbonExportReport.Size = new System.Drawing.Size(436, 107);
            this.ribbonExportReport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExportReport.TabIndex = 27;
            this.ribbonExportReport.Text = "Export";
            // 
            // 
            // 
            this.ribbonExportReport.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExportReport.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btReportExportToExcel
            // 
            this.btReportExportToExcel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportExportToExcel.CanCustomize = false;
            this.btReportExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btReportExportToExcel.Image")));
            this.btReportExportToExcel.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportExportToExcel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportExportToExcel.Name = "btReportExportToExcel";
            this.btReportExportToExcel.ShowSubItems = false;
            this.btReportExportToExcel.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblExportToExcelOptions,
            this.btExportToExcelRawData,
            this.btExportToExcelFormattedData,
            this.btExportToExcelRawAndFormatted});
            this.btReportExportToExcel.Text = "Export to Excel";
            this.btReportExportToExcel.Click += new System.EventHandler(this.btReportExportToExcel_Click);
            // 
            // lblExportToExcelOptions
            // 
            this.lblExportToExcelOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblExportToExcelOptions.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblExportToExcelOptions.CanCustomize = false;
            this.lblExportToExcelOptions.Name = "lblExportToExcelOptions";
            this.lblExportToExcelOptions.PaddingBottom = 3;
            this.lblExportToExcelOptions.PaddingLeft = 10;
            this.lblExportToExcelOptions.PaddingTop = 3;
            this.lblExportToExcelOptions.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblExportToExcelOptions.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.lblExportToExcelOptions.Text = "Options";
            this.lblExportToExcelOptions.Visible = false;
            // 
            // buttonItem1
            // 
            this.buttonItem1.CanCustomize = false;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = global::QuoterPlan.Properties.Resources.Impérial;
            // 
            // btExportToExcelRawData
            // 
            this.btExportToExcelRawData.AutoCheckOnClick = true;
            this.btExportToExcelRawData.CanCustomize = false;
            this.btExportToExcelRawData.Name = "btExportToExcelRawData";
            this.btExportToExcelRawData.OptionGroup = "ExportToExcelType";
            this.btExportToExcelRawData.Tag = "0";
            this.btExportToExcelRawData.Text = "Export Raw Data";
            this.btExportToExcelRawData.Visible = false;
            this.btExportToExcelRawData.Click += new System.EventHandler(this.btExportToExcelType_Click);
            // 
            // btExportToExcelFormattedData
            // 
            this.btExportToExcelFormattedData.AutoCheckOnClick = true;
            this.btExportToExcelFormattedData.CanCustomize = false;
            this.btExportToExcelFormattedData.Name = "btExportToExcelFormattedData";
            this.btExportToExcelFormattedData.OptionGroup = "ExportToExcelType";
            this.btExportToExcelFormattedData.Tag = "1";
            this.btExportToExcelFormattedData.Text = "Export Formatted Data";
            this.btExportToExcelFormattedData.Visible = false;
            this.btExportToExcelFormattedData.Click += new System.EventHandler(this.btExportToExcelType_Click);
            // 
            // btExportToExcelRawAndFormatted
            // 
            this.btExportToExcelRawAndFormatted.AutoCheckOnClick = true;
            this.btExportToExcelRawAndFormatted.CanCustomize = false;
            this.btExportToExcelRawAndFormatted.Name = "btExportToExcelRawAndFormatted";
            this.btExportToExcelRawAndFormatted.OptionGroup = "ExportToExcelType";
            this.btExportToExcelRawAndFormatted.Tag = "2";
            this.btExportToExcelRawAndFormatted.Text = "Export Both";
            this.btExportToExcelRawAndFormatted.Visible = false;
            this.btExportToExcelRawAndFormatted.Click += new System.EventHandler(this.btExportToExcelType_Click);
            // 
            // btReportExportToCSV
            // 
            this.btReportExportToCSV.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportExportToCSV.Image = ((System.Drawing.Image)(resources.GetObject("btReportExportToCSV.Image")));
            this.btReportExportToCSV.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportExportToCSV.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportExportToCSV.Name = "btReportExportToCSV";
            this.btReportExportToCSV.Text = "Export to CSV File";
            this.btReportExportToCSV.Visible = false;
            this.btReportExportToCSV.Click += new System.EventHandler(this.btReportExportToCSV_Click);
            // 
            // btReportExportToXML
            // 
            this.btReportExportToXML.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportExportToXML.Image = ((System.Drawing.Image)(resources.GetObject("btReportExportToXML.Image")));
            this.btReportExportToXML.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportExportToXML.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportExportToXML.Name = "btReportExportToXML";
            this.btReportExportToXML.Text = "Export to XML";
            this.btReportExportToXML.Click += new System.EventHandler(this.btReportExportToXML_Click);
            // 
            // btReportExportToHTML
            // 
            this.btReportExportToHTML.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportExportToHTML.Image = ((System.Drawing.Image)(resources.GetObject("btReportExportToHTML.Image")));
            this.btReportExportToHTML.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportExportToHTML.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportExportToHTML.Name = "btReportExportToHTML";
            this.btReportExportToHTML.Text = "Export to HTML";
            this.btReportExportToHTML.Click += new System.EventHandler(this.btReportExportToHTML_Click);
            // 
            // btReportExportToPDF
            // 
            this.btReportExportToPDF.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportExportToPDF.Image = global::QuoterPlan.Properties.Resources.file_pdf_40x40;
            this.btReportExportToPDF.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportExportToPDF.ImagePaddingVertical = 5;
            this.btReportExportToPDF.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportExportToPDF.Name = "btReportExportToPDF";
            this.btReportExportToPDF.Text = "Export to PDF";
            this.btReportExportToPDF.Click += new System.EventHandler(this.btReportExportToPDF_Click);
            // 
            // btReportExportToEE
            // 
            this.btReportExportToEE.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportExportToEE.Image = ((System.Drawing.Image)(resources.GetObject("btReportExportToEE.Image")));
            this.btReportExportToEE.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportExportToEE.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportExportToEE.Name = "btReportExportToEE";
            this.btReportExportToEE.Text = "Export to Expert Estimator";
            this.btReportExportToEE.Visible = false;
            // 
            // btReportExportToCOffice
            // 
            this.btReportExportToCOffice.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportExportToCOffice.Image = ((System.Drawing.Image)(resources.GetObject("btReportExportToCOffice.Image")));
            this.btReportExportToCOffice.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportExportToCOffice.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportExportToCOffice.Name = "btReportExportToCOffice";
            this.btReportExportToCOffice.Text = "Contractor\'s\r\nOffice";
            this.btReportExportToCOffice.Visible = false;
            this.btReportExportToCOffice.Click += new System.EventHandler(this.btReportExportToCOffice_Click);
            // 
            // ribbonPrintReport
            // 
            this.ribbonPrintReport.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonPrintReport.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPrintReport.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPrintReport.CanCustomize = false;
            this.ribbonPrintReport.ContainerControlProcessDialogKey = true;
            this.ribbonPrintReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonPrintReport.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btReportPrint,
            this.btReportPrintPreview,
            this.btReportPrintSetup});
            this.ribbonPrintReport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonPrintReport.Location = new System.Drawing.Point(106, 0);
            this.ribbonPrintReport.Name = "ribbonPrintReport";
            this.ribbonPrintReport.Size = new System.Drawing.Size(141, 107);
            this.ribbonPrintReport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPrintReport.TabIndex = 26;
            this.ribbonPrintReport.Text = "Print";
            // 
            // 
            // 
            this.ribbonPrintReport.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPrintReport.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btReportPrint
            // 
            this.btReportPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportPrint.Image = ((System.Drawing.Image)(resources.GetObject("btReportPrint.Image")));
            this.btReportPrint.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportPrint.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportPrint.Name = "btReportPrint";
            this.btReportPrint.Text = "Print Report";
            this.btReportPrint.Click += new System.EventHandler(this.btReportPrint_Click);
            // 
            // btReportPrintPreview
            // 
            this.btReportPrintPreview.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("btReportPrintPreview.Image")));
            this.btReportPrintPreview.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportPrintPreview.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportPrintPreview.Name = "btReportPrintPreview";
            this.btReportPrintPreview.Text = "Print Preview";
            this.btReportPrintPreview.Visible = false;
            this.btReportPrintPreview.Click += new System.EventHandler(this.btReportPrintPreview_Click);
            // 
            // btReportPrintSetup
            // 
            this.btReportPrintSetup.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportPrintSetup.Image = ((System.Drawing.Image)(resources.GetObject("btReportPrintSetup.Image")));
            this.btReportPrintSetup.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportPrintSetup.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportPrintSetup.Name = "btReportPrintSetup";
            this.btReportPrintSetup.Text = "Page Setup";
            this.btReportPrintSetup.Visible = false;
            this.btReportPrintSetup.Click += new System.EventHandler(this.btReportPrintSetup_Click);
            // 
            // ribbonReportOrder
            // 
            this.ribbonReportOrder.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonReportOrder.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonReportOrder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonReportOrder.CanCustomize = false;
            this.ribbonReportOrder.ContainerControlProcessDialogKey = true;
            this.ribbonReportOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonReportOrder.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btReportFilter,
            this.btReportSettings});
            this.ribbonReportOrder.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonReportOrder.Location = new System.Drawing.Point(3, 0);
            this.ribbonReportOrder.Name = "ribbonReportOrder";
            this.ribbonReportOrder.Size = new System.Drawing.Size(103, 107);
            this.ribbonReportOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonReportOrder.TabIndex = 24;
            this.ribbonReportOrder.Text = "Options";
            // 
            // 
            // 
            this.ribbonReportOrder.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonReportOrder.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btReportFilter
            // 
            this.btReportFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportFilter.CanCustomize = false;
            this.btReportFilter.Image = ((System.Drawing.Image)(resources.GetObject("btReportFilter.Image")));
            this.btReportFilter.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportFilter.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportFilter.Name = "btReportFilter";
            this.btReportFilter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblReportSystemType,
            this.btReportScaleImperial,
            this.btReportScaleMetric,
            this.lblReportPrecision,
            this.btReportScalePrecision64,
            this.btReportScalePrecision32,
            this.btReportScalePrecision16,
            this.btReportScalePrecision8,
            this.lblReportTheme});
            this.btReportFilter.Text = "Filter Groups";
            this.btReportFilter.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btReportEdit_PopupOpen);
            this.btReportFilter.Click += new System.EventHandler(this.btReportEdit_Click);
            // 
            // lblReportSystemType
            // 
            this.lblReportSystemType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblReportSystemType.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblReportSystemType.CanCustomize = false;
            this.lblReportSystemType.Name = "lblReportSystemType";
            this.lblReportSystemType.PaddingBottom = 3;
            this.lblReportSystemType.PaddingLeft = 10;
            this.lblReportSystemType.PaddingTop = 3;
            this.lblReportSystemType.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblReportSystemType.Text = "Display";
            // 
            // btReportScaleImperial
            // 
            this.btReportScaleImperial.CanCustomize = false;
            this.btReportScaleImperial.Name = "btReportScaleImperial";
            this.btReportScaleImperial.Text = global::QuoterPlan.Properties.Resources.Impérial;
            this.btReportScaleImperial.Click += new System.EventHandler(this.btReportScaleImperial_Click);
            // 
            // btReportScaleMetric
            // 
            this.btReportScaleMetric.CanCustomize = false;
            this.btReportScaleMetric.Name = "btReportScaleMetric";
            this.btReportScaleMetric.Text = global::QuoterPlan.Properties.Resources.Métrique;
            this.btReportScaleMetric.Click += new System.EventHandler(this.btReportScaleMetric_Click);
            // 
            // lblReportPrecision
            // 
            this.lblReportPrecision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblReportPrecision.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblReportPrecision.CanCustomize = false;
            this.lblReportPrecision.Name = "lblReportPrecision";
            this.lblReportPrecision.PaddingBottom = 3;
            this.lblReportPrecision.PaddingLeft = 10;
            this.lblReportPrecision.PaddingTop = 3;
            this.lblReportPrecision.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblReportPrecision.Text = "Accuracy";
            // 
            // btReportScalePrecision64
            // 
            this.btReportScalePrecision64.CanCustomize = false;
            this.btReportScalePrecision64.Name = "btReportScalePrecision64";
            this.btReportScalePrecision64.Text = "1/64 | 0.001";
            this.btReportScalePrecision64.Click += new System.EventHandler(this.btReportScalePrecision64_Click);
            // 
            // btReportScalePrecision32
            // 
            this.btReportScalePrecision32.CanCustomize = false;
            this.btReportScalePrecision32.Name = "btReportScalePrecision32";
            this.btReportScalePrecision32.Text = "1/32 | 0.01";
            this.btReportScalePrecision32.Click += new System.EventHandler(this.btReportScalePrecision32_Click);
            // 
            // btReportScalePrecision16
            // 
            this.btReportScalePrecision16.CanCustomize = false;
            this.btReportScalePrecision16.Name = "btReportScalePrecision16";
            this.btReportScalePrecision16.Text = "1/16 | 0.1";
            this.btReportScalePrecision16.Click += new System.EventHandler(this.btReportScalePrecision16_Click);
            // 
            // btReportScalePrecision8
            // 
            this.btReportScalePrecision8.CanCustomize = false;
            this.btReportScalePrecision8.Name = "btReportScalePrecision8";
            this.btReportScalePrecision8.Text = "1/8 | 1";
            this.btReportScalePrecision8.Click += new System.EventHandler(this.btReportScalePrecision8_Click);
            // 
            // lblReportTheme
            // 
            this.lblReportTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblReportTheme.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblReportTheme.CanCustomize = false;
            this.lblReportTheme.Name = "lblReportTheme";
            this.lblReportTheme.PaddingBottom = 3;
            this.lblReportTheme.PaddingLeft = 10;
            this.lblReportTheme.PaddingTop = 3;
            this.lblReportTheme.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblReportTheme.Text = "Theme";
            this.lblReportTheme.Visible = false;
            // 
            // btReportSettings
            // 
            this.btReportSettings.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReportSettings.Image = ((System.Drawing.Image)(resources.GetObject("btReportSettings.Image")));
            this.btReportSettings.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btReportSettings.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btReportSettings.Name = "btReportSettings";
            this.btReportSettings.Text = "Modify Settings";
            this.btReportSettings.Click += new System.EventHandler(this.btReportSettings_Click);
            // 
            // ribbonPanel
            // 
            this.ribbonPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel.Controls.Add(this.ribbonBarPrintExport);
            this.ribbonPanel.Controls.Add(this.ribbonBarImage);
            this.ribbonPanel.Controls.Add(this.ribbonBarBrowse);
            this.ribbonPanel.Controls.Add(this.ribbonBarZoom);
            this.ribbonPanel.Controls.Add(this.ribbonBarAnnotations);
            this.ribbonPanel.Controls.Add(this.ribbonBarTools);
            this.ribbonPanel.Controls.Add(this.ribbonBarScale);
            this.ribbonPanel.Controls.Add(this.ribbonBarEdit);
            this.ribbonPanel.Controls.Add(this.ribbonBarLayout);
            this.ribbonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel.Location = new System.Drawing.Point(0, 57);
            this.ribbonPanel.Name = "ribbonPanel";
            this.ribbonPanel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel.Size = new System.Drawing.Size(1729, 110);
            // 
            // 
            // 
            this.ribbonPanel.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel.TabIndex = 1;
            this.ribbonPanel.Visible = false;
            // 
            // ribbonBarPrintExport
            // 
            this.ribbonBarPrintExport.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBarPrintExport.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPrintExport.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarPrintExport.ContainerControlProcessDialogKey = true;
            this.ribbonBarPrintExport.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarPrintExport.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btPrintPlan,
            this.btExportPlanToPDF});
            this.ribbonBarPrintExport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPrintExport.Location = new System.Drawing.Point(1307, 0);
            this.ribbonBarPrintExport.Name = "ribbonBarPrintExport";
            this.ribbonBarPrintExport.OverflowButtonImage = global::QuoterPlan.Properties.Resources.print;
            this.ribbonBarPrintExport.Size = new System.Drawing.Size(104, 107);
            this.ribbonBarPrintExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPrintExport.TabIndex = 22;
            this.ribbonBarPrintExport.Text = "Print / Export";
            // 
            // 
            // 
            this.ribbonBarPrintExport.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPrintExport.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btPrintPlan
            // 
            this.btPrintPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPrintPlan.Image = global::QuoterPlan.Properties.Resources.printer_32x36;
            this.btPrintPlan.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPrintPlan.ImagePaddingHorizontal = 10;
            this.btPrintPlan.ImagePaddingVertical = 5;
            this.btPrintPlan.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPrintPlan.Name = "btPrintPlan";
            this.btPrintPlan.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblPrintPlanOptions,
            this.btPrintPlanFullSize,
            this.btPrintPlanWindow});
            this.btPrintPlan.Text = "Print";
            this.btPrintPlan.Click += new System.EventHandler(this.btPrintPlan_Click);
            // 
            // lblPrintPlanOptions
            // 
            this.lblPrintPlanOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblPrintPlanOptions.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblPrintPlanOptions.CanCustomize = false;
            this.lblPrintPlanOptions.Name = "lblPrintPlanOptions";
            this.lblPrintPlanOptions.PaddingBottom = 3;
            this.lblPrintPlanOptions.PaddingLeft = 10;
            this.lblPrintPlanOptions.PaddingTop = 3;
            this.lblPrintPlanOptions.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPrintPlanOptions.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2});
            this.lblPrintPlanOptions.Text = "Options";
            // 
            // buttonItem2
            // 
            this.buttonItem2.CanCustomize = false;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = global::QuoterPlan.Properties.Resources.Impérial;
            // 
            // btPrintPlanFullSize
            // 
            this.btPrintPlanFullSize.AutoCheckOnClick = true;
            this.btPrintPlanFullSize.CanCustomize = false;
            this.btPrintPlanFullSize.Name = "btPrintPlanFullSize";
            this.btPrintPlanFullSize.OptionGroup = "PrintPlanOptions";
            this.btPrintPlanFullSize.Tag = "0";
            this.btPrintPlanFullSize.Text = "Print Full Size";
            this.btPrintPlanFullSize.Click += new System.EventHandler(this.btPrintPlanType_Click);
            // 
            // btPrintPlanWindow
            // 
            this.btPrintPlanWindow.AutoCheckOnClick = true;
            this.btPrintPlanWindow.CanCustomize = false;
            this.btPrintPlanWindow.Name = "btPrintPlanWindow";
            this.btPrintPlanWindow.OptionGroup = "PrintPlanOptions";
            this.btPrintPlanWindow.Tag = "1";
            this.btPrintPlanWindow.Text = "Print Window Only";
            this.btPrintPlanWindow.Click += new System.EventHandler(this.btPrintPlanType_Click);
            // 
            // btExportPlanToPDF
            // 
            this.btExportPlanToPDF.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExportPlanToPDF.Image = global::QuoterPlan.Properties.Resources.file_pdf_40x40;
            this.btExportPlanToPDF.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExportPlanToPDF.ImagePaddingVertical = 5;
            this.btExportPlanToPDF.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExportPlanToPDF.Name = "btExportPlanToPDF";
            this.btExportPlanToPDF.Text = "Export to PDF";
            this.btExportPlanToPDF.Click += new System.EventHandler(this.btExportPlanToPDF_Click);
            // 
            // ribbonBarImage
            // 
            this.ribbonBarImage.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBarImage.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarImage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarImage.CanCustomize = false;
            this.ribbonBarImage.ContainerControlProcessDialogKey = true;
            this.ribbonBarImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarImage.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btBrightnessContrast,
            this.btRotation});
            this.ribbonBarImage.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarImage.Location = new System.Drawing.Point(1189, 0);
            this.ribbonBarImage.Name = "ribbonBarImage";
            this.ribbonBarImage.OverflowButtonImage = ((System.Drawing.Image)(resources.GetObject("ribbonBarImage.OverflowButtonImage")));
            this.ribbonBarImage.Size = new System.Drawing.Size(118, 107);
            this.ribbonBarImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarImage.TabIndex = 21;
            this.ribbonBarImage.Text = "Adjustements";
            // 
            // 
            // 
            this.ribbonBarImage.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarImage.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btBrightnessContrast
            // 
            this.btBrightnessContrast.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrightnessContrast.Image = ((System.Drawing.Image)(resources.GetObject("btBrightnessContrast.Image")));
            this.btBrightnessContrast.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btBrightnessContrast.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btBrightnessContrast.Name = "btBrightnessContrast";
            this.btBrightnessContrast.Text = "Brightness / Contrast";
            this.btBrightnessContrast.Click += new System.EventHandler(this.btBrightnessContrast_Click);
            // 
            // btRotation
            // 
            this.btRotation.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRotation.Image = ((System.Drawing.Image)(resources.GetObject("btRotation.Image")));
            this.btRotation.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btRotation.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btRotation.Name = "btRotation";
            this.btRotation.Text = "Flip / Rotate";
            this.btRotation.Click += new System.EventHandler(this.btRotation_Click);
            // 
            // ribbonBarBrowse
            // 
            this.ribbonBarBrowse.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBarBrowse.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarBrowse.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarBrowse.CanCustomize = false;
            this.ribbonBarBrowse.ContainerControlProcessDialogKey = true;
            this.ribbonBarBrowse.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarBrowse.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerBrowse1,
            this.itemContainerBrowse2});
            this.ribbonBarBrowse.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarBrowse.Location = new System.Drawing.Point(1003, 0);
            this.ribbonBarBrowse.Name = "ribbonBarBrowse";
            this.ribbonBarBrowse.OverflowButtonImage = ((System.Drawing.Image)(resources.GetObject("ribbonBarBrowse.OverflowButtonImage")));
            this.ribbonBarBrowse.Size = new System.Drawing.Size(186, 107);
            this.ribbonBarBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarBrowse.TabIndex = 18;
            this.ribbonBarBrowse.Text = "Browse";
            // 
            // 
            // 
            this.ribbonBarBrowse.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarBrowse.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // itemContainerBrowse1
            // 
            // 
            // 
            // 
            this.itemContainerBrowse1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerBrowse1.FixedSize = new System.Drawing.Size(88, 0);
            this.itemContainerBrowse1.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerBrowse1.MultiLine = true;
            this.itemContainerBrowse1.Name = "itemContainerBrowse1";
            this.itemContainerBrowse1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblBrowseGroup,
            this.itemContainerBrowse3});
            // 
            // 
            // 
            this.itemContainerBrowse1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblBrowseGroup
            // 
            this.lblBrowseGroup.Name = "lblBrowseGroup";
            this.lblBrowseGroup.PaddingTop = 3;
            this.lblBrowseGroup.Stretch = true;
            this.lblBrowseGroup.Text = "Through\r\nSelected Group";
            this.lblBrowseGroup.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblBrowseGroup.Width = 88;
            this.lblBrowseGroup.WordWrap = true;
            // 
            // itemContainerBrowse3
            // 
            // 
            // 
            // 
            this.itemContainerBrowse3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerBrowse3.FixedSize = new System.Drawing.Size(88, 0);
            this.itemContainerBrowse3.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse3.Name = "itemContainerBrowse3";
            this.itemContainerBrowse3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btBrowsePrevious,
            this.btBrowseNext});
            // 
            // 
            // 
            this.itemContainerBrowse3.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btBrowsePrevious
            // 
            this.btBrowsePrevious.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrowsePrevious.Image = ((System.Drawing.Image)(resources.GetObject("btBrowsePrevious.Image")));
            this.btBrowsePrevious.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btBrowsePrevious.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.btBrowsePrevious.Name = "btBrowsePrevious";
            this.btBrowsePrevious.Click += new System.EventHandler(this.btBrowsePrevious_Click);
            // 
            // btBrowseNext
            // 
            this.btBrowseNext.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrowseNext.Image = ((System.Drawing.Image)(resources.GetObject("btBrowseNext.Image")));
            this.btBrowseNext.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btBrowseNext.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.btBrowseNext.Name = "btBrowseNext";
            this.btBrowseNext.Click += new System.EventHandler(this.btBrowseNext_Click);
            // 
            // itemContainerBrowse2
            // 
            // 
            // 
            // 
            this.itemContainerBrowse2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerBrowse2.FixedSize = new System.Drawing.Size(88, 0);
            this.itemContainerBrowse2.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerBrowse2.MultiLine = true;
            this.itemContainerBrowse2.Name = "itemContainerBrowse2";
            this.itemContainerBrowse2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblBrowseObjectType,
            this.itemContainerBrowse4});
            // 
            // 
            // 
            this.itemContainerBrowse2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblBrowseObjectType
            // 
            this.lblBrowseObjectType.Name = "lblBrowseObjectType";
            this.lblBrowseObjectType.PaddingTop = 3;
            this.lblBrowseObjectType.Stretch = true;
            this.lblBrowseObjectType.Text = "Through\r\nSelected Type";
            this.lblBrowseObjectType.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblBrowseObjectType.Width = 88;
            this.lblBrowseObjectType.WordWrap = true;
            // 
            // itemContainerBrowse4
            // 
            // 
            // 
            // 
            this.itemContainerBrowse4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerBrowse4.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse4.MinimumSize = new System.Drawing.Size(88, 0);
            this.itemContainerBrowse4.Name = "itemContainerBrowse4";
            this.itemContainerBrowse4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btBrowseObjectTypePrevious,
            this.btBrowseObjectTypeNext});
            // 
            // 
            // 
            this.itemContainerBrowse4.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btBrowseObjectTypePrevious
            // 
            this.btBrowseObjectTypePrevious.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrowseObjectTypePrevious.Image = ((System.Drawing.Image)(resources.GetObject("btBrowseObjectTypePrevious.Image")));
            this.btBrowseObjectTypePrevious.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btBrowseObjectTypePrevious.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.btBrowseObjectTypePrevious.Name = "btBrowseObjectTypePrevious";
            this.btBrowseObjectTypePrevious.Click += new System.EventHandler(this.btBrowseObjectTypePrevious_Click);
            // 
            // btBrowseObjectTypeNext
            // 
            this.btBrowseObjectTypeNext.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrowseObjectTypeNext.Image = ((System.Drawing.Image)(resources.GetObject("btBrowseObjectTypeNext.Image")));
            this.btBrowseObjectTypeNext.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btBrowseObjectTypeNext.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.btBrowseObjectTypeNext.Name = "btBrowseObjectTypeNext";
            this.btBrowseObjectTypeNext.Click += new System.EventHandler(this.btBrowseObjectTypeNext_Click);
            // 
            // ribbonBarZoom
            // 
            this.ribbonBarZoom.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarZoom.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarZoom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarZoom.CanCustomize = false;
            this.ribbonBarZoom.ContainerControlProcessDialogKey = true;
            this.ribbonBarZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarZoom.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btZoomToSelection,
            this.btZoomToWindow,
            this.btZoomActualSize,
            this.btZoomIn,
            this.btZoomOut,
            this.btBookmarks});
            this.ribbonBarZoom.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarZoom.Location = new System.Drawing.Point(698, 0);
            this.ribbonBarZoom.Name = "ribbonBarZoom";
            this.ribbonBarZoom.Size = new System.Drawing.Size(305, 107);
            this.ribbonBarZoom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarZoom.TabIndex = 16;
            this.ribbonBarZoom.Text = "Zoom";
            // 
            // 
            // 
            this.ribbonBarZoom.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarZoom.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btZoomToSelection
            // 
            this.btZoomToSelection.BeginGroup = true;
            this.btZoomToSelection.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btZoomToSelection.Image = ((System.Drawing.Image)(resources.GetObject("btZoomToSelection.Image")));
            this.btZoomToSelection.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btZoomToSelection.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btZoomToSelection.Name = "btZoomToSelection";
            this.btZoomToSelection.Text = "Zoom to Selection";
            this.btZoomToSelection.Click += new System.EventHandler(this.btZoomToSelection_Click);
            // 
            // btZoomToWindow
            // 
            this.btZoomToWindow.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btZoomToWindow.Image = ((System.Drawing.Image)(resources.GetObject("btZoomToWindow.Image")));
            this.btZoomToWindow.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btZoomToWindow.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btZoomToWindow.Name = "btZoomToWindow";
            this.btZoomToWindow.Text = "Zoom to Window";
            this.btZoomToWindow.Click += new System.EventHandler(this.btZoomToWindow_Click);
            // 
            // btZoomActualSize
            // 
            this.btZoomActualSize.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btZoomActualSize.CanCustomize = false;
            this.btZoomActualSize.Image = ((System.Drawing.Image)(resources.GetObject("btZoomActualSize.Image")));
            this.btZoomActualSize.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btZoomActualSize.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btZoomActualSize.Name = "btZoomActualSize";
            this.btZoomActualSize.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btZoom75,
            this.btZoom50,
            this.btZoom25});
            this.btZoomActualSize.Text = "Normal Size";
            this.btZoomActualSize.Click += new System.EventHandler(this.btZoomActualSize_Click);
            // 
            // btZoom75
            // 
            this.btZoom75.CanCustomize = false;
            this.btZoom75.Image = ((System.Drawing.Image)(resources.GetObject("btZoom75.Image")));
            this.btZoom75.Name = "btZoom75";
            this.btZoom75.Text = "Zoom to 75%";
            this.btZoom75.Click += new System.EventHandler(this.btZoom75_Click);
            // 
            // btZoom50
            // 
            this.btZoom50.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btZoom50.CanCustomize = false;
            this.btZoom50.Image = ((System.Drawing.Image)(resources.GetObject("btZoom50.Image")));
            this.btZoom50.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btZoom50.ImagePaddingHorizontal = 12;
            this.btZoom50.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btZoom50.Name = "btZoom50";
            this.btZoom50.Text = "Zoom to 50%";
            this.btZoom50.Click += new System.EventHandler(this.btZoom50_Click);
            // 
            // btZoom25
            // 
            this.btZoom25.CanCustomize = false;
            this.btZoom25.Image = ((System.Drawing.Image)(resources.GetObject("btZoom25.Image")));
            this.btZoom25.Name = "btZoom25";
            this.btZoom25.Text = "Zoom to 25%";
            this.btZoom25.Click += new System.EventHandler(this.btZoom25_Click);
            // 
            // btZoomIn
            // 
            this.btZoomIn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btZoomIn.Image")));
            this.btZoomIn.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btZoomIn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btZoomIn.Name = "btZoomIn";
            this.btZoomIn.Text = "Zoom In";
            this.btZoomIn.Click += new System.EventHandler(this.btZoomIn_Click);
            // 
            // btZoomOut
            // 
            this.btZoomOut.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btZoomOut.Image")));
            this.btZoomOut.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btZoomOut.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btZoomOut.Name = "btZoomOut";
            this.btZoomOut.Text = "Zoom Out";
            this.btZoomOut.Click += new System.EventHandler(this.btZoomOut_Click);
            // 
            // btBookmarks
            // 
            this.btBookmarks.BeginGroup = true;
            this.btBookmarks.CanCustomize = false;
            this.btBookmarks.Image = ((System.Drawing.Image)(resources.GetObject("btBookmarks.Image")));
            this.btBookmarks.ImageFixedSize = new System.Drawing.Size(32, 32);
            this.btBookmarks.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btBookmarks.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btBookmarks.Name = "btBookmarks";
            this.btBookmarks.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btZoomTo75,
            this.btZoomTo50,
            this.btZoomTo25,
            this.btZoomTo150,
            this.btZoomTo200,
            this.buttonItem61,
            this.btModifyBookmarks});
            this.btBookmarks.Text = "Bookmarks";
            this.btBookmarks.Visible = false;
            // 
            // btZoomTo75
            // 
            this.btZoomTo75.Name = "btZoomTo75";
            this.btZoomTo75.Text = "Zoom to 75%";
            // 
            // btZoomTo50
            // 
            this.btZoomTo50.Name = "btZoomTo50";
            this.btZoomTo50.Text = "Zoom to 50 %";
            // 
            // btZoomTo25
            // 
            this.btZoomTo25.Name = "btZoomTo25";
            this.btZoomTo25.Text = "Zoom to 25 %";
            // 
            // btZoomTo150
            // 
            this.btZoomTo150.Name = "btZoomTo150";
            this.btZoomTo150.Text = "Zoom to 150 %";
            // 
            // btZoomTo200
            // 
            this.btZoomTo200.Name = "btZoomTo200";
            this.btZoomTo200.Text = "Zoom to 200 %";
            // 
            // buttonItem61
            // 
            this.buttonItem61.Name = "buttonItem61";
            this.buttonItem61.Text = "Rooms";
            // 
            // btModifyBookmarks
            // 
            this.btModifyBookmarks.BeginGroup = true;
            this.btModifyBookmarks.Name = "btModifyBookmarks";
            this.btModifyBookmarks.Text = "Modify bookmarks...";
            // 
            // ribbonBarAnnotations
            // 
            this.ribbonBarAnnotations.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarAnnotations.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarAnnotations.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarAnnotations.CanCustomize = false;
            this.ribbonBarAnnotations.ContainerControlProcessDialogKey = true;
            this.ribbonBarAnnotations.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarAnnotations.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btMarkZone,
            this.btInsertNote,
            this.btInsertPicture});
            this.ribbonBarAnnotations.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarAnnotations.Location = new System.Drawing.Point(553, 0);
            this.ribbonBarAnnotations.Name = "ribbonBarAnnotations";
            this.ribbonBarAnnotations.Size = new System.Drawing.Size(145, 107);
            this.ribbonBarAnnotations.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarAnnotations.TabIndex = 14;
            this.ribbonBarAnnotations.Text = "Annotations";
            // 
            // 
            // 
            this.ribbonBarAnnotations.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarAnnotations.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btMarkZone
            // 
            this.btMarkZone.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btMarkZone.CanCustomize = false;
            this.btMarkZone.Image = ((System.Drawing.Image)(resources.GetObject("btMarkZone.Image")));
            this.btMarkZone.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btMarkZone.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btMarkZone.Name = "btMarkZone";
            this.btMarkZone.Text = "Mark a Zone";
            this.btMarkZone.Click += new System.EventHandler(this.btMarkZone_Click);
            // 
            // btInsertNote
            // 
            this.btInsertNote.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btInsertNote.CanCustomize = false;
            this.btInsertNote.Image = ((System.Drawing.Image)(resources.GetObject("btInsertNote.Image")));
            this.btInsertNote.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btInsertNote.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btInsertNote.Name = "btInsertNote";
            this.btInsertNote.Text = "Insert a Note";
            this.btInsertNote.Click += new System.EventHandler(this.btInsertNote_Click);
            // 
            // btInsertPicture
            // 
            this.btInsertPicture.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btInsertPicture.CanCustomize = false;
            this.btInsertPicture.Image = ((System.Drawing.Image)(resources.GetObject("btInsertPicture.Image")));
            this.btInsertPicture.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btInsertPicture.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btInsertPicture.Name = "btInsertPicture";
            this.btInsertPicture.Text = "Insert an Image";
            this.btInsertPicture.Visible = false;
            // 
            // ribbonBarTools
            // 
            this.ribbonBarTools.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarTools.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarTools.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarTools.CanCustomize = false;
            this.ribbonBarTools.ContainerControlProcessDialogKey = true;
            this.ribbonBarTools.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarTools.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btToolSelection,
            this.btToolPan,
            this.btToolArea,
            this.btToolPerimeter,
            this.btToolRuler,
            this.btToolCounter,
            this.btToolAngle});
            this.ribbonBarTools.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarTools.Location = new System.Drawing.Point(230, 0);
            this.ribbonBarTools.Name = "ribbonBarTools";
            this.ribbonBarTools.Size = new System.Drawing.Size(323, 107);
            this.ribbonBarTools.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarTools.TabIndex = 13;
            this.ribbonBarTools.Text = "Tools";
            // 
            // 
            // 
            this.ribbonBarTools.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarTools.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btToolSelection
            // 
            this.btToolSelection.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btToolSelection.CanCustomize = false;
            this.btToolSelection.Image = ((System.Drawing.Image)(resources.GetObject("btToolSelection.Image")));
            this.btToolSelection.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btToolSelection.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btToolSelection.Name = "btToolSelection";
            this.btToolSelection.Text = "Cursor";
            this.btToolSelection.Click += new System.EventHandler(this.btToolSelection_Click);
            // 
            // btToolPan
            // 
            this.btToolPan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btToolPan.CanCustomize = false;
            this.btToolPan.Image = ((System.Drawing.Image)(resources.GetObject("btToolPan.Image")));
            this.btToolPan.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btToolPan.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btToolPan.Name = "btToolPan";
            this.btToolPan.Text = "Pan";
            this.btToolPan.Click += new System.EventHandler(this.btToolPan_Click);
            // 
            // btToolArea
            // 
            this.btToolArea.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btToolArea.CanCustomize = false;
            this.btToolArea.Image = ((System.Drawing.Image)(resources.GetObject("btToolArea.Image")));
            this.btToolArea.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btToolArea.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btToolArea.Name = "btToolArea";
            this.btToolArea.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblNoArea,
            this.lblAreaFilter,
            this.itemContainerAreaFilter,
            this.lblAreaGroups,
            this.galleryAreaGroups,
            this.lblAreaTemplates,
            this.galleryAreaTemplates});
            this.btToolArea.Text = "Area";
            this.btToolArea.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolArea_PopupOpen);
            this.btToolArea.Click += new System.EventHandler(this.btToolArea_Click);
            // 
            // lblNoArea
            // 
            this.lblNoArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblNoArea.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblNoArea.CanCustomize = false;
            this.lblNoArea.Name = "lblNoArea";
            this.lblNoArea.PaddingBottom = 3;
            this.lblNoArea.PaddingLeft = 10;
            this.lblNoArea.PaddingTop = 3;
            this.lblNoArea.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblNoArea.Text = "No Area Available.";
            // 
            // lblAreaFilter
            // 
            this.lblAreaFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblAreaFilter.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblAreaFilter.CanCustomize = false;
            this.lblAreaFilter.Name = "lblAreaFilter";
            this.lblAreaFilter.PaddingBottom = 3;
            this.lblAreaFilter.PaddingLeft = 10;
            this.lblAreaFilter.PaddingTop = 3;
            this.lblAreaFilter.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblAreaFilter.Text = "Filter:";
            // 
            // itemContainerAreaFilter
            // 
            // 
            // 
            // 
            this.itemContainerAreaFilter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerAreaFilter.CanCustomize = false;
            this.itemContainerAreaFilter.ItemSpacing = 0;
            this.itemContainerAreaFilter.Name = "itemContainerAreaFilter";
            this.itemContainerAreaFilter.ResizeItemsToFit = false;
            this.itemContainerAreaFilter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.txtAreaFilter,
            this.btAreaFilterClear,
            this.lblAreaFilterPadding});
            // 
            // 
            // 
            this.itemContainerAreaFilter.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // txtAreaFilter
            // 
            this.txtAreaFilter.CanCustomize = false;
            this.txtAreaFilter.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.txtAreaFilter.Name = "txtAreaFilter";
            this.txtAreaFilter.Stretch = true;
            this.txtAreaFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            this.txtAreaFilter.LostFocus += new System.EventHandler(this.txtFilter_LostFocus);
            this.txtAreaFilter.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            this.txtAreaFilter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
            // 
            // btAreaFilterClear
            // 
            this.btAreaFilterClear.AutoCollapseOnClick = false;
            this.btAreaFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
            this.btAreaFilterClear.ImagePaddingHorizontal = 4;
            this.btAreaFilterClear.ImagePaddingVertical = 4;
            this.btAreaFilterClear.Name = "btAreaFilterClear";
            this.btAreaFilterClear.Click += new System.EventHandler(this.btFilterClear_Click);
            this.btAreaFilterClear.LostFocus += new System.EventHandler(this.txtFilter_LostFocus);
            this.btAreaFilterClear.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            // 
            // lblAreaFilterPadding
            // 
            this.lblAreaFilterPadding.Name = "lblAreaFilterPadding";
            this.lblAreaFilterPadding.Width = 4;
            // 
            // lblAreaGroups
            // 
            this.lblAreaGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblAreaGroups.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblAreaGroups.CanCustomize = false;
            this.lblAreaGroups.Name = "lblAreaGroups";
            this.lblAreaGroups.PaddingBottom = 3;
            this.lblAreaGroups.PaddingLeft = 10;
            this.lblAreaGroups.PaddingTop = 3;
            this.lblAreaGroups.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblAreaGroups.Text = "Add to an <b>Existing Group</b>:";
            // 
            // galleryAreaGroups
            // 
            // 
            // 
            // 
            this.galleryAreaGroups.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryAreaGroups.CanCustomize = false;
            this.galleryAreaGroups.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryAreaGroups.EnableGalleryPopup = false;
            this.galleryAreaGroups.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryAreaGroups.Name = "galleryAreaGroups";
            this.galleryAreaGroups.Text = "galleryContainer4";
            // 
            // 
            // 
            this.galleryAreaGroups.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblAreaTemplates
            // 
            this.lblAreaTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblAreaTemplates.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblAreaTemplates.CanCustomize = false;
            this.lblAreaTemplates.Name = "lblAreaTemplates";
            this.lblAreaTemplates.PaddingBottom = 3;
            this.lblAreaTemplates.PaddingLeft = 10;
            this.lblAreaTemplates.PaddingTop = 3;
            this.lblAreaTemplates.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblAreaTemplates.Text = "Create a <b>New Area</b> from a <b>Template</b>:";
            // 
            // galleryAreaTemplates
            // 
            // 
            // 
            // 
            this.galleryAreaTemplates.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryAreaTemplates.CanCustomize = false;
            this.galleryAreaTemplates.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryAreaTemplates.EnableGalleryPopup = false;
            this.galleryAreaTemplates.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryAreaTemplates.Name = "galleryAreaTemplates";
            this.galleryAreaTemplates.Text = "galleryContainer4";
            // 
            // 
            // 
            this.galleryAreaTemplates.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btToolPerimeter
            // 
            this.btToolPerimeter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btToolPerimeter.CanCustomize = false;
            this.btToolPerimeter.Image = ((System.Drawing.Image)(resources.GetObject("btToolPerimeter.Image")));
            this.btToolPerimeter.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btToolPerimeter.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btToolPerimeter.Name = "btToolPerimeter";
            this.btToolPerimeter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblNoPerimeter,
            this.lblPerimeterFilter,
            this.itemContainerPerimeterFilter,
            this.lblPerimeterGroups,
            this.galleryPerimeterGroups,
            this.lblPerimeterTemplates,
            this.galleryPerimeterTemplates});
            this.btToolPerimeter.Text = "Perimeter";
            this.btToolPerimeter.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolPerimeter_PopupOpen);
            this.btToolPerimeter.Click += new System.EventHandler(this.btToolPerimeter_Click);
            // 
            // lblNoPerimeter
            // 
            this.lblNoPerimeter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblNoPerimeter.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblNoPerimeter.CanCustomize = false;
            this.lblNoPerimeter.Name = "lblNoPerimeter";
            this.lblNoPerimeter.PaddingBottom = 3;
            this.lblNoPerimeter.PaddingLeft = 10;
            this.lblNoPerimeter.PaddingTop = 3;
            this.lblNoPerimeter.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblNoPerimeter.Text = "No Perimeter Available.";
            // 
            // lblPerimeterFilter
            // 
            this.lblPerimeterFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblPerimeterFilter.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblPerimeterFilter.CanCustomize = false;
            this.lblPerimeterFilter.Name = "lblPerimeterFilter";
            this.lblPerimeterFilter.PaddingBottom = 3;
            this.lblPerimeterFilter.PaddingLeft = 10;
            this.lblPerimeterFilter.PaddingTop = 3;
            this.lblPerimeterFilter.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPerimeterFilter.Text = "Filter:";
            // 
            // itemContainerPerimeterFilter
            // 
            // 
            // 
            // 
            this.itemContainerPerimeterFilter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerPerimeterFilter.CanCustomize = false;
            this.itemContainerPerimeterFilter.ItemSpacing = 0;
            this.itemContainerPerimeterFilter.Name = "itemContainerPerimeterFilter";
            this.itemContainerPerimeterFilter.ResizeItemsToFit = false;
            this.itemContainerPerimeterFilter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.txtPerimeterFilter,
            this.btPerimeterFilterClear});
            // 
            // 
            // 
            this.itemContainerPerimeterFilter.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // txtPerimeterFilter
            // 
            this.txtPerimeterFilter.CanCustomize = false;
            this.txtPerimeterFilter.Name = "txtPerimeterFilter";
            this.txtPerimeterFilter.Stretch = true;
            this.txtPerimeterFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            this.txtPerimeterFilter.LostFocus += new System.EventHandler(this.txtFilter_LostFocus);
            this.txtPerimeterFilter.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            this.txtPerimeterFilter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
            // 
            // btPerimeterFilterClear
            // 
            this.btPerimeterFilterClear.AutoCollapseOnClick = false;
            this.btPerimeterFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
            this.btPerimeterFilterClear.ImagePaddingHorizontal = 4;
            this.btPerimeterFilterClear.ImagePaddingVertical = 4;
            this.btPerimeterFilterClear.Name = "btPerimeterFilterClear";
            this.btPerimeterFilterClear.Click += new System.EventHandler(this.btFilterClear_Click);
            this.btPerimeterFilterClear.LostFocus += new System.EventHandler(this.txtFilter_LostFocus);
            this.btPerimeterFilterClear.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            // 
            // lblPerimeterGroups
            // 
            this.lblPerimeterGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblPerimeterGroups.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblPerimeterGroups.CanCustomize = false;
            this.lblPerimeterGroups.Name = "lblPerimeterGroups";
            this.lblPerimeterGroups.PaddingBottom = 3;
            this.lblPerimeterGroups.PaddingLeft = 10;
            this.lblPerimeterGroups.PaddingTop = 3;
            this.lblPerimeterGroups.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPerimeterGroups.Text = "Add to an <b>Existing Group</b>:";
            // 
            // galleryPerimeterGroups
            // 
            // 
            // 
            // 
            this.galleryPerimeterGroups.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryPerimeterGroups.CanCustomize = false;
            this.galleryPerimeterGroups.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryPerimeterGroups.EnableGalleryPopup = false;
            this.galleryPerimeterGroups.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryPerimeterGroups.Name = "galleryPerimeterGroups";
            this.galleryPerimeterGroups.ScrollAnimation = false;
            this.galleryPerimeterGroups.Text = "galleryPerimeterGroups";
            // 
            // 
            // 
            this.galleryPerimeterGroups.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblPerimeterTemplates
            // 
            this.lblPerimeterTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblPerimeterTemplates.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblPerimeterTemplates.CanCustomize = false;
            this.lblPerimeterTemplates.Name = "lblPerimeterTemplates";
            this.lblPerimeterTemplates.PaddingBottom = 3;
            this.lblPerimeterTemplates.PaddingLeft = 10;
            this.lblPerimeterTemplates.PaddingTop = 3;
            this.lblPerimeterTemplates.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPerimeterTemplates.Text = "Create a <b>New Perimeter</b> from a <b>Template</b>:";
            // 
            // galleryPerimeterTemplates
            // 
            // 
            // 
            // 
            this.galleryPerimeterTemplates.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryPerimeterTemplates.CanCustomize = false;
            this.galleryPerimeterTemplates.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryPerimeterTemplates.EnableGalleryPopup = false;
            this.galleryPerimeterTemplates.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryPerimeterTemplates.Name = "galleryPerimeterTemplates";
            this.galleryPerimeterTemplates.Text = "galleryPerimeterTemplates";
            // 
            // 
            // 
            this.galleryPerimeterTemplates.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btToolRuler
            // 
            this.btToolRuler.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btToolRuler.CanCustomize = false;
            this.btToolRuler.Image = ((System.Drawing.Image)(resources.GetObject("btToolRuler.Image")));
            this.btToolRuler.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btToolRuler.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btToolRuler.Name = "btToolRuler";
            this.btToolRuler.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblNoDistance,
            this.lblDistanceFilter,
            this.itemContainerDistanceFilter,
            this.lblDistanceGroups,
            this.galleryDistanceGroups,
            this.lblDistanceTemplates,
            this.galleryDistanceTemplates});
            this.btToolRuler.Text = "Length";
            this.btToolRuler.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolRuler_PopupOpen);
            this.btToolRuler.Click += new System.EventHandler(this.btToolRuler_Click);
            // 
            // lblNoDistance
            // 
            this.lblNoDistance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblNoDistance.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblNoDistance.CanCustomize = false;
            this.lblNoDistance.Name = "lblNoDistance";
            this.lblNoDistance.PaddingBottom = 3;
            this.lblNoDistance.PaddingLeft = 10;
            this.lblNoDistance.PaddingTop = 3;
            this.lblNoDistance.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblNoDistance.Text = "No Length Available.";
            // 
            // lblDistanceFilter
            // 
            this.lblDistanceFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblDistanceFilter.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblDistanceFilter.CanCustomize = false;
            this.lblDistanceFilter.Name = "lblDistanceFilter";
            this.lblDistanceFilter.PaddingBottom = 3;
            this.lblDistanceFilter.PaddingLeft = 10;
            this.lblDistanceFilter.PaddingTop = 3;
            this.lblDistanceFilter.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblDistanceFilter.Text = "Filter:";
            // 
            // itemContainerDistanceFilter
            // 
            // 
            // 
            // 
            this.itemContainerDistanceFilter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerDistanceFilter.CanCustomize = false;
            this.itemContainerDistanceFilter.ItemSpacing = 0;
            this.itemContainerDistanceFilter.Name = "itemContainerDistanceFilter";
            this.itemContainerDistanceFilter.ResizeItemsToFit = false;
            this.itemContainerDistanceFilter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.txtDistanceFilter,
            this.btDistanceFilterClear});
            // 
            // 
            // 
            this.itemContainerDistanceFilter.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // txtDistanceFilter
            // 
            this.txtDistanceFilter.CanCustomize = false;
            this.txtDistanceFilter.Name = "txtDistanceFilter";
            this.txtDistanceFilter.Stretch = true;
            this.txtDistanceFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            this.txtDistanceFilter.LostFocus += new System.EventHandler(this.txtFilter_LostFocus);
            this.txtDistanceFilter.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            this.txtDistanceFilter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
            // 
            // btDistanceFilterClear
            // 
            this.btDistanceFilterClear.AutoCollapseOnClick = false;
            this.btDistanceFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
            this.btDistanceFilterClear.ImagePaddingHorizontal = 4;
            this.btDistanceFilterClear.ImagePaddingVertical = 4;
            this.btDistanceFilterClear.Name = "btDistanceFilterClear";
            this.btDistanceFilterClear.Click += new System.EventHandler(this.btFilterClear_Click);
            this.btDistanceFilterClear.LostFocus += new System.EventHandler(this.txtFilter_LostFocus);
            this.btDistanceFilterClear.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            // 
            // lblDistanceGroups
            // 
            this.lblDistanceGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblDistanceGroups.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblDistanceGroups.CanCustomize = false;
            this.lblDistanceGroups.Name = "lblDistanceGroups";
            this.lblDistanceGroups.PaddingBottom = 3;
            this.lblDistanceGroups.PaddingLeft = 10;
            this.lblDistanceGroups.PaddingTop = 3;
            this.lblDistanceGroups.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblDistanceGroups.Text = "Add to an <b>Existing Group</b>:";
            // 
            // galleryDistanceGroups
            // 
            // 
            // 
            // 
            this.galleryDistanceGroups.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryDistanceGroups.CanCustomize = false;
            this.galleryDistanceGroups.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryDistanceGroups.EnableGalleryPopup = false;
            this.galleryDistanceGroups.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryDistanceGroups.Name = "galleryDistanceGroups";
            this.galleryDistanceGroups.ScrollAnimation = false;
            this.galleryDistanceGroups.Text = "galleryPerimeterGroups";
            // 
            // 
            // 
            this.galleryDistanceGroups.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblDistanceTemplates
            // 
            this.lblDistanceTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblDistanceTemplates.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblDistanceTemplates.CanCustomize = false;
            this.lblDistanceTemplates.Name = "lblDistanceTemplates";
            this.lblDistanceTemplates.PaddingBottom = 3;
            this.lblDistanceTemplates.PaddingLeft = 10;
            this.lblDistanceTemplates.PaddingTop = 3;
            this.lblDistanceTemplates.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblDistanceTemplates.Text = "Create a <b>New Length</b> from a <b>Template</b>:";
            // 
            // galleryDistanceTemplates
            // 
            // 
            // 
            // 
            this.galleryDistanceTemplates.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryDistanceTemplates.CanCustomize = false;
            this.galleryDistanceTemplates.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryDistanceTemplates.EnableGalleryPopup = false;
            this.galleryDistanceTemplates.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryDistanceTemplates.Name = "galleryDistanceTemplates";
            this.galleryDistanceTemplates.ScrollAnimation = false;
            this.galleryDistanceTemplates.Text = "galleryPerimeterGroups";
            // 
            // 
            // 
            this.galleryDistanceTemplates.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btToolCounter
            // 
            this.btToolCounter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btToolCounter.CanCustomize = false;
            this.btToolCounter.Image = ((System.Drawing.Image)(resources.GetObject("btToolCounter.Image")));
            this.btToolCounter.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btToolCounter.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btToolCounter.Name = "btToolCounter";
            this.btToolCounter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblNoCounter,
            this.lblCounterFilter,
            this.itemContainerCounterFilter,
            this.lblCounterGroups,
            this.galleryCounterGroups,
            this.lblCounterTemplates,
            this.galleryCounterTemplates});
            this.btToolCounter.Text = "Counter";
            this.btToolCounter.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolCounter_PopupOpen);
            this.btToolCounter.Click += new System.EventHandler(this.btToolCounter_Click);
            // 
            // lblNoCounter
            // 
            this.lblNoCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblNoCounter.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblNoCounter.CanCustomize = false;
            this.lblNoCounter.Name = "lblNoCounter";
            this.lblNoCounter.PaddingBottom = 3;
            this.lblNoCounter.PaddingLeft = 10;
            this.lblNoCounter.PaddingTop = 3;
            this.lblNoCounter.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblNoCounter.Text = "No Counter Available.";
            // 
            // lblCounterFilter
            // 
            this.lblCounterFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblCounterFilter.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblCounterFilter.CanCustomize = false;
            this.lblCounterFilter.Name = "lblCounterFilter";
            this.lblCounterFilter.PaddingBottom = 3;
            this.lblCounterFilter.PaddingLeft = 10;
            this.lblCounterFilter.PaddingTop = 3;
            this.lblCounterFilter.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblCounterFilter.Text = "Filter:";
            // 
            // itemContainerCounterFilter
            // 
            // 
            // 
            // 
            this.itemContainerCounterFilter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerCounterFilter.CanCustomize = false;
            this.itemContainerCounterFilter.ItemSpacing = 0;
            this.itemContainerCounterFilter.Name = "itemContainerCounterFilter";
            this.itemContainerCounterFilter.ResizeItemsToFit = false;
            this.itemContainerCounterFilter.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.txtCounterFilter,
            this.btCounterFilterClear});
            // 
            // 
            // 
            this.itemContainerCounterFilter.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // txtCounterFilter
            // 
            this.txtCounterFilter.CanCustomize = false;
            this.txtCounterFilter.Name = "txtCounterFilter";
            this.txtCounterFilter.Stretch = true;
            this.txtCounterFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            this.txtCounterFilter.LostFocus += new System.EventHandler(this.txtFilter_GotFocus);
            this.txtCounterFilter.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            this.txtCounterFilter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
            // 
            // btCounterFilterClear
            // 
            this.btCounterFilterClear.AutoCollapseOnClick = false;
            this.btCounterFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
            this.btCounterFilterClear.ImagePaddingHorizontal = 4;
            this.btCounterFilterClear.ImagePaddingVertical = 4;
            this.btCounterFilterClear.Name = "btCounterFilterClear";
            this.btCounterFilterClear.Click += new System.EventHandler(this.btFilterClear_Click);
            this.btCounterFilterClear.LostFocus += new System.EventHandler(this.txtFilter_GotFocus);
            this.btCounterFilterClear.GotFocus += new System.EventHandler(this.txtFilter_GotFocus);
            // 
            // lblCounterGroups
            // 
            this.lblCounterGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblCounterGroups.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblCounterGroups.CanCustomize = false;
            this.lblCounterGroups.Name = "lblCounterGroups";
            this.lblCounterGroups.PaddingBottom = 3;
            this.lblCounterGroups.PaddingLeft = 10;
            this.lblCounterGroups.PaddingTop = 3;
            this.lblCounterGroups.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblCounterGroups.Text = "Add to an <b>Existing Group</b>:";
            // 
            // galleryCounterGroups
            // 
            // 
            // 
            // 
            this.galleryCounterGroups.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryCounterGroups.CanCustomize = false;
            this.galleryCounterGroups.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryCounterGroups.EnableGalleryPopup = false;
            this.galleryCounterGroups.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryCounterGroups.Name = "galleryCounterGroups";
            this.galleryCounterGroups.ScrollAnimation = false;
            this.galleryCounterGroups.Text = "galleryPerimeterGroups";
            // 
            // 
            // 
            this.galleryCounterGroups.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblCounterTemplates
            // 
            this.lblCounterTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblCounterTemplates.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblCounterTemplates.CanCustomize = false;
            this.lblCounterTemplates.Name = "lblCounterTemplates";
            this.lblCounterTemplates.PaddingBottom = 3;
            this.lblCounterTemplates.PaddingLeft = 10;
            this.lblCounterTemplates.PaddingTop = 3;
            this.lblCounterTemplates.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblCounterTemplates.Text = "Create a <b>New Counter</b> from a <b>Template</b>:";
            // 
            // galleryCounterTemplates
            // 
            // 
            // 
            // 
            this.galleryCounterTemplates.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryCounterTemplates.CanCustomize = false;
            this.galleryCounterTemplates.DefaultSize = new System.Drawing.Size(200, 20);
            this.galleryCounterTemplates.EnableGalleryPopup = false;
            this.galleryCounterTemplates.MinimumSize = new System.Drawing.Size(200, 20);
            this.galleryCounterTemplates.Name = "galleryCounterTemplates";
            this.galleryCounterTemplates.ScrollAnimation = false;
            this.galleryCounterTemplates.Text = "galleryPerimeterGroups";
            // 
            // 
            // 
            this.galleryCounterTemplates.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btToolAngle
            // 
            this.btToolAngle.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btToolAngle.CanCustomize = false;
            this.btToolAngle.Image = ((System.Drawing.Image)(resources.GetObject("btToolAngle.Image")));
            this.btToolAngle.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btToolAngle.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btToolAngle.Name = "btToolAngle";
            this.btToolAngle.Text = global::QuoterPlan.Properties.Resources.Angle;
            this.btToolAngle.Click += new System.EventHandler(this.btToolAngle_Click);
            // 
            // ribbonBarScale
            // 
            this.ribbonBarScale.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarScale.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarScale.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarScale.CanCustomize = false;
            this.ribbonBarScale.ContainerControlProcessDialogKey = true;
            this.ribbonBarScale.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarScale.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btScaleSet});
            this.ribbonBarScale.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarScale.Location = new System.Drawing.Point(165, 0);
            this.ribbonBarScale.Name = "ribbonBarScale";
            this.ribbonBarScale.Size = new System.Drawing.Size(65, 107);
            this.ribbonBarScale.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarScale.TabIndex = 11;
            this.ribbonBarScale.Text = "Scale";
            // 
            // 
            // 
            this.ribbonBarScale.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarScale.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btScaleSet
            // 
            this.btScaleSet.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btScaleSet.CanCustomize = false;
            this.btScaleSet.Image = ((System.Drawing.Image)(resources.GetObject("btScaleSet.Image")));
            this.btScaleSet.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.btScaleSet.ImagePaddingVertical = 3;
            this.btScaleSet.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btScaleSet.Name = "btScaleSet";
            this.btScaleSet.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblSystemType,
            this.btScaleImperial,
            this.btScaleMetric,
            this.lblPrecision,
            this.btScalePrecision64,
            this.btScalePrecision32,
            this.btScalePrecision16,
            this.btScalePrecision8});
            this.btScaleSet.Click += new System.EventHandler(this.btScaleSet_Click);
            // 
            // lblSystemType
            // 
            this.lblSystemType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblSystemType.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblSystemType.CanCustomize = false;
            this.lblSystemType.Name = "lblSystemType";
            this.lblSystemType.PaddingBottom = 3;
            this.lblSystemType.PaddingLeft = 10;
            this.lblSystemType.PaddingTop = 3;
            this.lblSystemType.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblSystemType.Text = "Display";
            // 
            // btScaleImperial
            // 
            this.btScaleImperial.CanCustomize = false;
            this.btScaleImperial.Name = "btScaleImperial";
            this.btScaleImperial.Text = global::QuoterPlan.Properties.Resources.Impérial;
            this.btScaleImperial.Click += new System.EventHandler(this.btScaleImperial_Click);
            // 
            // btScaleMetric
            // 
            this.btScaleMetric.CanCustomize = false;
            this.btScaleMetric.Name = "btScaleMetric";
            this.btScaleMetric.Text = global::QuoterPlan.Properties.Resources.Métrique;
            this.btScaleMetric.Click += new System.EventHandler(this.btScaleMetric_Click);
            // 
            // lblPrecision
            // 
            this.lblPrecision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblPrecision.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblPrecision.CanCustomize = false;
            this.lblPrecision.Name = "lblPrecision";
            this.lblPrecision.PaddingBottom = 4;
            this.lblPrecision.PaddingLeft = 10;
            this.lblPrecision.PaddingTop = 4;
            this.lblPrecision.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPrecision.Text = "Accuracy";
            // 
            // btScalePrecision64
            // 
            this.btScalePrecision64.CanCustomize = false;
            this.btScalePrecision64.Name = "btScalePrecision64";
            this.btScalePrecision64.Text = "1/64 | 0.001";
            this.btScalePrecision64.Click += new System.EventHandler(this.btScalePrecision64_Click);
            // 
            // btScalePrecision32
            // 
            this.btScalePrecision32.CanCustomize = false;
            this.btScalePrecision32.Name = "btScalePrecision32";
            this.btScalePrecision32.Text = "1/32 | 0.01";
            this.btScalePrecision32.Click += new System.EventHandler(this.btScalePrecision32_Click);
            // 
            // btScalePrecision16
            // 
            this.btScalePrecision16.CanCustomize = false;
            this.btScalePrecision16.Name = "btScalePrecision16";
            this.btScalePrecision16.Text = "1/16 | 0.1";
            this.btScalePrecision16.Click += new System.EventHandler(this.btScalePrecision16_Click);
            // 
            // btScalePrecision8
            // 
            this.btScalePrecision8.CanCustomize = false;
            this.btScalePrecision8.Name = "btScalePrecision8";
            this.btScalePrecision8.Text = "1/8 | 1";
            this.btScalePrecision8.Click += new System.EventHandler(this.btScalePrecision8_Click);
            // 
            // ribbonBarEdit
            // 
            this.ribbonBarEdit.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarEdit.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarEdit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarEdit.CanCustomize = false;
            this.ribbonBarEdit.ContainerControlProcessDialogKey = true;
            this.ribbonBarEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarEdit.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btEditPaste,
            this.itemContainerEdit,
            this.itemContainerUndoRedo,
            this.btEditSendData});
            this.ribbonBarEdit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarEdit.Location = new System.Drawing.Point(3, 0);
            this.ribbonBarEdit.Name = "ribbonBarEdit";
            this.ribbonBarEdit.Size = new System.Drawing.Size(162, 107);
            this.ribbonBarEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarEdit.TabIndex = 1;
            this.ribbonBarEdit.Text = "Edit";
            // 
            // 
            // 
            this.ribbonBarEdit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarEdit.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btEditPaste
            // 
            this.btEditPaste.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("btEditPaste.Image")));
            this.btEditPaste.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEditPaste.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.btEditPaste.Name = "btEditPaste";
            this.btEditPaste.Text = "Paste";
            this.btEditPaste.Click += new System.EventHandler(this.btEditPaste_Click);
            // 
            // itemContainerEdit
            // 
            // 
            // 
            // 
            this.itemContainerEdit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerEdit.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainerEdit.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerEdit.Name = "itemContainerEdit";
            this.itemContainerEdit.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btEditCut,
            this.btEditCopy,
            this.btEditDelete});
            // 
            // 
            // 
            this.itemContainerEdit.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerEdit.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // btEditCut
            // 
            this.btEditCut.Image = ((System.Drawing.Image)(resources.GetObject("btEditCut.Image")));
            this.btEditCut.ImageFixedSize = new System.Drawing.Size(22, 22);
            this.btEditCut.ImagePaddingVertical = 1;
            this.btEditCut.Name = "btEditCut";
            this.btEditCut.Text = "buttonItem29";
            this.btEditCut.Click += new System.EventHandler(this.btEditCut_Click);
            // 
            // btEditCopy
            // 
            this.btEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("btEditCopy.Image")));
            this.btEditCopy.ImagePaddingVertical = 1;
            this.btEditCopy.Name = "btEditCopy";
            this.btEditCopy.Text = "buttonItem29";
            this.btEditCopy.Click += new System.EventHandler(this.btEditCopy_Click);
            // 
            // btEditDelete
            // 
            this.btEditDelete.Image = ((System.Drawing.Image)(resources.GetObject("btEditDelete.Image")));
            this.btEditDelete.ImagePaddingVertical = 1;
            this.btEditDelete.Name = "btEditDelete";
            this.btEditDelete.Text = "buttonItem29";
            this.btEditDelete.Click += new System.EventHandler(this.btEditDelete_Click);
            // 
            // itemContainerUndoRedo
            // 
            // 
            // 
            // 
            this.itemContainerUndoRedo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerUndoRedo.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainerUndoRedo.ItemSpacing = 3;
            this.itemContainerUndoRedo.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerUndoRedo.Name = "itemContainerUndoRedo";
            this.itemContainerUndoRedo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btEditUndo,
            this.btEditRedo});
            // 
            // 
            // 
            this.itemContainerUndoRedo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerUndoRedo.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            this.itemContainerUndoRedo.Visible = false;
            // 
            // btEditUndo
            // 
            this.btEditUndo.Image = ((System.Drawing.Image)(resources.GetObject("btEditUndo.Image")));
            this.btEditUndo.ImagePaddingVertical = 2;
            this.btEditUndo.Name = "btEditUndo";
            this.btEditUndo.Text = "Undo";
            this.btEditUndo.Click += new System.EventHandler(this.btEditUndo_Click);
            // 
            // btEditRedo
            // 
            this.btEditRedo.Image = ((System.Drawing.Image)(resources.GetObject("btEditRedo.Image")));
            this.btEditRedo.ImagePaddingVertical = 2;
            this.btEditRedo.Name = "btEditRedo";
            this.btEditRedo.Text = "Redo";
            this.btEditRedo.Click += new System.EventHandler(this.btEditRedo_Click);
            // 
            // btEditSendData
            // 
            this.btEditSendData.AutoExpandOnClick = true;
            this.btEditSendData.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEditSendData.Image = global::QuoterPlan.Properties.Resources.right_arrow_blue;
            this.btEditSendData.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btEditSendData.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btEditSendData.Name = "btEditSendData";
            this.btEditSendData.Text = "Send Data";
            this.btEditSendData.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btEditSendData_PopupOpen);
            this.btEditSendData.Click += new System.EventHandler(this.btEditSendData_Click);
            // 
            // ribbonBarLayout
            // 
            this.ribbonBarLayout.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarLayout.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarLayout.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarLayout.ContainerControlProcessDialogKey = true;
            this.ribbonBarLayout.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarLayout.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerLayouts});
            this.ribbonBarLayout.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarLayout.Location = new System.Drawing.Point(3, 0);
            this.ribbonBarLayout.Name = "ribbonBarLayout";
            this.ribbonBarLayout.Size = new System.Drawing.Size(81, 107);
            this.ribbonBarLayout.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarLayout.TabIndex = 23;
            this.ribbonBarLayout.Text = "Layout";
            // 
            // 
            // 
            this.ribbonBarLayout.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarLayout.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarLayout.Visible = false;
            // 
            // itemContainerLayouts
            // 
            // 
            // 
            // 
            this.itemContainerLayouts.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerLayouts.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerLayouts.Name = "itemContainerLayouts";
            this.itemContainerLayouts.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.opTakeoffLayout,
            this.opEstimatingLayout});
            // 
            // 
            // 
            this.itemContainerLayouts.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerLayouts.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // opTakeoffLayout
            // 
            this.opTakeoffLayout.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.opTakeoffLayout.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.opTakeoffLayout.Checked = true;
            this.opTakeoffLayout.CheckState = System.Windows.Forms.CheckState.Checked;
            this.opTakeoffLayout.Name = "opTakeoffLayout";
            this.opTakeoffLayout.Text = "Takeoff";
            // 
            // opEstimatingLayout
            // 
            this.opEstimatingLayout.CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right;
            this.opEstimatingLayout.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.opEstimatingLayout.Name = "opEstimatingLayout";
            this.opEstimatingLayout.Text = "Estimating";
            // 
            // ribbonPanelTemplates
            // 
            this.ribbonPanelTemplates.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplate);
            this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplateCreate);
            this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplateDatabase);
            this.ribbonPanelTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanelTemplates.Location = new System.Drawing.Point(0, 57);
            this.ribbonPanelTemplates.Name = "ribbonPanelTemplates";
            this.ribbonPanelTemplates.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanelTemplates.Size = new System.Drawing.Size(1729, 110);
            // 
            // 
            // 
            this.ribbonPanelTemplates.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelTemplates.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelTemplates.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanelTemplates.TabIndex = 5;
            this.ribbonPanelTemplates.Visible = false;
            // 
            // ribbonTemplate
            // 
            this.ribbonTemplate.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonTemplate.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonTemplate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonTemplate.ContainerControlProcessDialogKey = true;
            this.ribbonTemplate.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonTemplate.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btTemplateModify,
            this.btTemplateDuplicate,
            this.btTemplateDelete});
            this.ribbonTemplate.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonTemplate.Location = new System.Drawing.Point(233, 0);
            this.ribbonTemplate.Name = "ribbonTemplate";
            this.ribbonTemplate.Size = new System.Drawing.Size(146, 107);
            this.ribbonTemplate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonTemplate.TabIndex = 5;
            this.ribbonTemplate.Text = "Selected Template";
            // 
            // 
            // 
            this.ribbonTemplate.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonTemplate.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btTemplateModify
            // 
            this.btTemplateModify.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateModify.CanCustomize = false;
            this.btTemplateModify.Image = global::QuoterPlan.Properties.Resources.properties;
            this.btTemplateModify.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateModify.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateModify.Name = "btTemplateModify";
            this.btTemplateModify.Text = "Modify";
            this.btTemplateModify.Click += new System.EventHandler(this.btTemplateModify_Click);
            // 
            // btTemplateDuplicate
            // 
            this.btTemplateDuplicate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateDuplicate.CanCustomize = false;
            this.btTemplateDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("btTemplateDuplicate.Image")));
            this.btTemplateDuplicate.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateDuplicate.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateDuplicate.Name = "btTemplateDuplicate";
            this.btTemplateDuplicate.Text = "Duplicate";
            this.btTemplateDuplicate.Click += new System.EventHandler(this.btTemplateDuplicate_Click);
            // 
            // btTemplateDelete
            // 
            this.btTemplateDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateDelete.CanCustomize = false;
            this.btTemplateDelete.Image = ((System.Drawing.Image)(resources.GetObject("btTemplateDelete.Image")));
            this.btTemplateDelete.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateDelete.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateDelete.Name = "btTemplateDelete";
            this.btTemplateDelete.Text = "Delete";
            this.btTemplateDelete.Click += new System.EventHandler(this.btTemplateDelete_Click);
            // 
            // ribbonTemplateCreate
            // 
            this.ribbonTemplateCreate.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonTemplateCreate.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonTemplateCreate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonTemplateCreate.CanCustomize = false;
            this.ribbonTemplateCreate.ContainerControlProcessDialogKey = true;
            this.ribbonTemplateCreate.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonTemplateCreate.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btTemplateArea,
            this.btTemplatePerimeter,
            this.btTemplateLength,
            this.btTemplateCounter});
            this.ribbonTemplateCreate.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonTemplateCreate.Location = new System.Drawing.Point(3, 0);
            this.ribbonTemplateCreate.Name = "ribbonTemplateCreate";
            this.ribbonTemplateCreate.Size = new System.Drawing.Size(230, 107);
            this.ribbonTemplateCreate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonTemplateCreate.TabIndex = 3;
            this.ribbonTemplateCreate.Text = "Create Template";
            // 
            // 
            // 
            this.ribbonTemplateCreate.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonTemplateCreate.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btTemplateArea
            // 
            this.btTemplateArea.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateArea.CanCustomize = false;
            this.btTemplateArea.Image = ((System.Drawing.Image)(resources.GetObject("btTemplateArea.Image")));
            this.btTemplateArea.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateArea.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateArea.Name = "btTemplateArea";
            this.btTemplateArea.Text = "Area Template";
            this.btTemplateArea.Click += new System.EventHandler(this.btTemplateArea_Click);
            // 
            // btTemplatePerimeter
            // 
            this.btTemplatePerimeter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplatePerimeter.CanCustomize = false;
            this.btTemplatePerimeter.Image = ((System.Drawing.Image)(resources.GetObject("btTemplatePerimeter.Image")));
            this.btTemplatePerimeter.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplatePerimeter.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplatePerimeter.Name = "btTemplatePerimeter";
            this.btTemplatePerimeter.Text = "Perimeter Template";
            this.btTemplatePerimeter.Click += new System.EventHandler(this.btTemplatePerimeter_Click);
            // 
            // btTemplateLength
            // 
            this.btTemplateLength.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateLength.CanCustomize = false;
            this.btTemplateLength.Image = ((System.Drawing.Image)(resources.GetObject("btTemplateLength.Image")));
            this.btTemplateLength.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateLength.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateLength.Name = "btTemplateLength";
            this.btTemplateLength.Text = "Length Template";
            this.btTemplateLength.Click += new System.EventHandler(this.btTemplateLength_Click);
            // 
            // btTemplateCounter
            // 
            this.btTemplateCounter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateCounter.CanCustomize = false;
            this.btTemplateCounter.Image = ((System.Drawing.Image)(resources.GetObject("btTemplateCounter.Image")));
            this.btTemplateCounter.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateCounter.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateCounter.Name = "btTemplateCounter";
            this.btTemplateCounter.Text = "Counter Template";
            this.btTemplateCounter.Click += new System.EventHandler(this.btTemplateCounter_Click);
            // 
            // ribbonTemplateDatabase
            // 
            this.ribbonTemplateDatabase.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonTemplateDatabase.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonTemplateDatabase.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonTemplateDatabase.ContainerControlProcessDialogKey = true;
            this.ribbonTemplateDatabase.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonTemplateDatabase.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btTemplateTradesPackages,
            this.btTemplateCompactDatabase});
            this.ribbonTemplateDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonTemplateDatabase.Location = new System.Drawing.Point(3, 0);
            this.ribbonTemplateDatabase.Name = "ribbonTemplateDatabase";
            this.ribbonTemplateDatabase.Size = new System.Drawing.Size(125, 107);
            this.ribbonTemplateDatabase.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonTemplateDatabase.TabIndex = 6;
            this.ribbonTemplateDatabase.Text = "Database";
            // 
            // 
            // 
            this.ribbonTemplateDatabase.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonTemplateDatabase.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonTemplateDatabase.Visible = false;
            // 
            // btTemplateTradesPackages
            // 
            this.btTemplateTradesPackages.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateTradesPackages.Image = ((System.Drawing.Image)(resources.GetObject("btTemplateTradesPackages.Image")));
            this.btTemplateTradesPackages.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateTradesPackages.ImagePaddingHorizontal = 10;
            this.btTemplateTradesPackages.ImagePaddingVertical = 5;
            this.btTemplateTradesPackages.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateTradesPackages.Name = "btTemplateTradesPackages";
            this.btTemplateTradesPackages.Text = "Trade Packages";
            // 
            // btTemplateCompactDatabase
            // 
            this.btTemplateCompactDatabase.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btTemplateCompactDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btTemplateCompactDatabase.Image")));
            this.btTemplateCompactDatabase.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btTemplateCompactDatabase.ImagePaddingVertical = 5;
            this.btTemplateCompactDatabase.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btTemplateCompactDatabase.Name = "btTemplateCompactDatabase";
            this.btTemplateCompactDatabase.Text = "Compact Database";
            // 
            // ribbonPanelPlans
            // 
            this.ribbonPanelPlans.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelPlans.Controls.Add(this.ribbonBarMultiPlans);
            this.ribbonPanelPlans.Controls.Add(this.ribbonBarPlans);
            this.ribbonPanelPlans.Controls.Add(this.ribbonBarPlansInsert);
            this.ribbonPanelPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanelPlans.Location = new System.Drawing.Point(0, 57);
            this.ribbonPanelPlans.Name = "ribbonPanelPlans";
            this.ribbonPanelPlans.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanelPlans.Size = new System.Drawing.Size(1682, 110);
            // 
            // 
            // 
            this.ribbonPanelPlans.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelPlans.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelPlans.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanelPlans.TabIndex = 2;
            this.ribbonPanelPlans.Visible = false;
            // 
            // ribbonBarMultiPlans
            // 
            this.ribbonBarMultiPlans.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarMultiPlans.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarMultiPlans.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarMultiPlans.ContainerControlProcessDialogKey = true;
            this.ribbonBarMultiPlans.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarMultiPlans.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btPlansPrint,
            this.btPlansExport});
            this.ribbonBarMultiPlans.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarMultiPlans.Location = new System.Drawing.Point(489, 0);
            this.ribbonBarMultiPlans.Name = "ribbonBarMultiPlans";
            this.ribbonBarMultiPlans.Size = new System.Drawing.Size(104, 107);
            this.ribbonBarMultiPlans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarMultiPlans.TabIndex = 4;
            this.ribbonBarMultiPlans.Text = "Print / Export";
            // 
            // 
            // 
            this.ribbonBarMultiPlans.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarMultiPlans.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btPlansPrint
            // 
            this.btPlansPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlansPrint.Image = global::QuoterPlan.Properties.Resources.printer_32x36;
            this.btPlansPrint.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlansPrint.ImagePaddingHorizontal = 10;
            this.btPlansPrint.ImagePaddingVertical = 5;
            this.btPlansPrint.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlansPrint.Name = "btPlansPrint";
            this.btPlansPrint.Text = "Print";
            this.btPlansPrint.Click += new System.EventHandler(this.btPlansPrint_Click);
            // 
            // btPlansExport
            // 
            this.btPlansExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlansExport.Image = global::QuoterPlan.Properties.Resources.file_pdf_40x40;
            this.btPlansExport.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlansExport.ImagePaddingVertical = 5;
            this.btPlansExport.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlansExport.Name = "btPlansExport";
            this.btPlansExport.Text = "Export to PDF";
            this.btPlansExport.Click += new System.EventHandler(this.btPlansExport_Click);
            // 
            // ribbonBarPlans
            // 
            this.ribbonBarPlans.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarPlans.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPlans.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarPlans.CanCustomize = false;
            this.ribbonBarPlans.ContainerControlProcessDialogKey = true;
            this.ribbonBarPlans.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarPlans.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btPlanLoad,
            this.btPlanProperties,
            this.btPlanRemove,
            this.btPlanExport,
            this.btPlanDuplicate});
            this.ribbonBarPlans.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPlans.Location = new System.Drawing.Point(140, 0);
            this.ribbonBarPlans.Name = "ribbonBarPlans";
            this.ribbonBarPlans.Size = new System.Drawing.Size(349, 107);
            this.ribbonBarPlans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPlans.TabIndex = 2;
            this.ribbonBarPlans.Text = "Selected Plan";
            // 
            // 
            // 
            this.ribbonBarPlans.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPlans.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btPlanLoad
            // 
            this.btPlanLoad.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanLoad.Image = ((System.Drawing.Image)(resources.GetObject("btPlanLoad.Image")));
            this.btPlanLoad.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlanLoad.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlanLoad.Name = "btPlanLoad";
            this.btPlanLoad.Text = "Load for Editing";
            this.btPlanLoad.Click += new System.EventHandler(this.btPlanLoad_Click);
            // 
            // btPlanProperties
            // 
            this.btPlanProperties.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanProperties.Image = ((System.Drawing.Image)(resources.GetObject("btPlanProperties.Image")));
            this.btPlanProperties.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlanProperties.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlanProperties.Name = "btPlanProperties";
            this.btPlanProperties.Text = "Display Properties";
            this.btPlanProperties.Click += new System.EventHandler(this.btPlanProperties_Click);
            // 
            // btPlanRemove
            // 
            this.btPlanRemove.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanRemove.Image = ((System.Drawing.Image)(resources.GetObject("btPlanRemove.Image")));
            this.btPlanRemove.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlanRemove.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlanRemove.Name = "btPlanRemove";
            this.btPlanRemove.Text = "Exclude from Project";
            this.btPlanRemove.Click += new System.EventHandler(this.btPlanRemove_Click);
            // 
            // btPlanExport
            // 
            this.btPlanExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanExport.Image = ((System.Drawing.Image)(resources.GetObject("btPlanExport.Image")));
            this.btPlanExport.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlanExport.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlanExport.Name = "btPlanExport";
            this.btPlanExport.Text = "Export Image";
            this.btPlanExport.Visible = false;
            this.btPlanExport.Click += new System.EventHandler(this.btPlanExport_Click);
            // 
            // btPlanDuplicate
            // 
            this.btPlanDuplicate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("btPlanDuplicate.Image")));
            this.btPlanDuplicate.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlanDuplicate.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlanDuplicate.Name = "btPlanDuplicate";
            this.btPlanDuplicate.Text = "Duplicate Plan";
            this.btPlanDuplicate.Click += new System.EventHandler(this.btPlanDuplicate_Click);
            // 
            // ribbonBarPlansInsert
            // 
            this.ribbonBarPlansInsert.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBarPlansInsert.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPlansInsert.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarPlansInsert.CanCustomize = false;
            this.ribbonBarPlansInsert.ContainerControlProcessDialogKey = true;
            this.ribbonBarPlansInsert.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBarPlansInsert.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btPlanInsertFromPDF,
            this.btPlanInsertFromImage});
            this.ribbonBarPlansInsert.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPlansInsert.Location = new System.Drawing.Point(3, 0);
            this.ribbonBarPlansInsert.Name = "ribbonBarPlansInsert";
            this.ribbonBarPlansInsert.Size = new System.Drawing.Size(137, 107);
            this.ribbonBarPlansInsert.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPlansInsert.TabIndex = 3;
            this.ribbonBarPlansInsert.Text = "Insert Plans";
            // 
            // 
            // 
            this.ribbonBarPlansInsert.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPlansInsert.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btPlanInsertFromPDF
            // 
            this.btPlanInsertFromPDF.AutoCollapseOnClick = false;
            this.btPlanInsertFromPDF.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanInsertFromPDF.CanCustomize = false;
            this.btPlanInsertFromPDF.Image = ((System.Drawing.Image)(resources.GetObject("btPlanInsertFromPDF.Image")));
            this.btPlanInsertFromPDF.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlanInsertFromPDF.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlanInsertFromPDF.Name = "btPlanInsertFromPDF";
            this.btPlanInsertFromPDF.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.iblImportDPI,
            this.op172Dpi,
            this.op300Dpi,
            this.opOtherDpi,
            this.sliderDpi,
            this.itemContainerDpi,
            this.iblImportColorManagement,
            this.opConvertToColor});
            this.btPlanInsertFromPDF.Text = "From PDF Files";
            this.btPlanInsertFromPDF.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btPlanInsertFromPDF_PopupOpen);
            this.btPlanInsertFromPDF.Click += new System.EventHandler(this.btPlanInsertFromPDF_Click);
            // 
            // iblImportDPI
            // 
            this.iblImportDPI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.iblImportDPI.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.iblImportDPI.CanCustomize = false;
            this.iblImportDPI.Name = "iblImportDPI";
            this.iblImportDPI.PaddingBottom = 3;
            this.iblImportDPI.PaddingLeft = 10;
            this.iblImportDPI.PaddingTop = 3;
            this.iblImportDPI.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.iblImportDPI.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem2});
            this.iblImportDPI.Text = "DPI Target:";
            // 
            // labelItem2
            // 
            this.labelItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.labelItem2.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelItem2.CanCustomize = false;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.PaddingBottom = 3;
            this.labelItem2.PaddingLeft = 10;
            this.labelItem2.PaddingTop = 3;
            this.labelItem2.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.labelItem2.Text = "DPI Target:";
            // 
            // op172Dpi
            // 
            this.op172Dpi.CanCustomize = false;
            this.op172Dpi.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.op172Dpi.Name = "op172Dpi";
            this.op172Dpi.Text = " 172 dpi";
            this.op172Dpi.Click += new System.EventHandler(this.op172Dpi_Click);
            // 
            // op300Dpi
            // 
            this.op300Dpi.CanCustomize = false;
            this.op300Dpi.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.op300Dpi.Name = "op300Dpi";
            this.op300Dpi.Text = " 300 dpi";
            this.op300Dpi.Click += new System.EventHandler(this.op300Dpi_Click);
            // 
            // opOtherDpi
            // 
            this.opOtherDpi.AutoCollapseOnClick = false;
            this.opOtherDpi.CanCustomize = false;
            this.opOtherDpi.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.opOtherDpi.Name = "opOtherDpi";
            this.opOtherDpi.Text = " Other:";
            this.opOtherDpi.Click += new System.EventHandler(this.opOtherDpi_Click);
            // 
            // sliderDpi
            // 
            this.sliderDpi.AutoCollapseOnClick = false;
            this.sliderDpi.CanCustomize = false;
            this.sliderDpi.LabelPosition = DevComponents.DotNetBar.eSliderLabelPosition.Top;
            this.sliderDpi.LabelWidth = 150;
            this.sliderDpi.Maximum = 300;
            this.sliderDpi.Minimum = 150;
            this.sliderDpi.Name = "sliderDpi";
            this.sliderDpi.Text = "150 dpi";
            this.sliderDpi.TextColor = System.Drawing.Color.Black;
            this.sliderDpi.TrackMarker = false;
            this.sliderDpi.Value = 150;
            this.sliderDpi.Width = 150;
            this.sliderDpi.ValueChanged += new System.EventHandler(this.sliderDpi_ValueChanged);
            // 
            // itemContainerDpi
            // 
            // 
            // 
            // 
            this.itemContainerDpi.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerDpi.FixedSize = new System.Drawing.Size(0, 32);
            this.itemContainerDpi.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.itemContainerDpi.Name = "itemContainerDpi";
            this.itemContainerDpi.ResizeItemsToFit = false;
            this.itemContainerDpi.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblDpi1,
            this.labelDpiPadding1,
            this.lblDpi2});
            // 
            // 
            // 
            this.itemContainerDpi.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblDpi1
            // 
            this.lblDpi1.CanCustomize = false;
            this.lblDpi1.Name = "lblDpi1";
            this.lblDpi1.Text = "Faster";
            this.lblDpi1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblDpi1.WordWrap = true;
            // 
            // labelDpiPadding1
            // 
            this.labelDpiPadding1.CanCustomize = false;
            this.labelDpiPadding1.Name = "labelDpiPadding1";
            this.labelDpiPadding1.Stretch = true;
            this.labelDpiPadding1.Width = 120;
            // 
            // lblDpi2
            // 
            this.lblDpi2.CanCustomize = false;
            this.lblDpi2.Name = "lblDpi2";
            this.lblDpi2.Text = "More\r\naccurate";
            this.lblDpi2.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblDpi2.WordWrap = true;
            // 
            // iblImportColorManagement
            // 
            this.iblImportColorManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.iblImportColorManagement.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.iblImportColorManagement.CanCustomize = false;
            this.iblImportColorManagement.Name = "iblImportColorManagement";
            this.iblImportColorManagement.PaddingBottom = 3;
            this.iblImportColorManagement.PaddingLeft = 10;
            this.iblImportColorManagement.PaddingTop = 3;
            this.iblImportColorManagement.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.iblImportColorManagement.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem4});
            this.iblImportColorManagement.Text = "Color Management:";
            // 
            // labelItem4
            // 
            this.labelItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.labelItem4.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.labelItem4.CanCustomize = false;
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.PaddingBottom = 3;
            this.labelItem4.PaddingLeft = 10;
            this.labelItem4.PaddingTop = 3;
            this.labelItem4.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.labelItem4.Text = "DPI Target:";
            // 
            // opConvertToColor
            // 
            this.opConvertToColor.CanCustomize = false;
            this.opConvertToColor.Category = "ConvertColorType";
            this.opConvertToColor.Name = "opConvertToColor";
            this.opConvertToColor.Text = "Enable Full Color Conversion";
            this.opConvertToColor.Click += new System.EventHandler(this.opConvertToColor_Click);
            // 
            // btPlanInsertFromImage
            // 
            this.btPlanInsertFromImage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanInsertFromImage.Image = ((System.Drawing.Image)(resources.GetObject("btPlanInsertFromImage.Image")));
            this.btPlanInsertFromImage.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlanInsertFromImage.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlanInsertFromImage.Name = "btPlanInsertFromImage";
            this.btPlanInsertFromImage.Text = "From Image Files";
            this.btPlanInsertFromImage.Click += new System.EventHandler(this.btPlanInsertFromImage_Click);
            // 
            // ribbonPanelExtensions
            // 
            this.ribbonPanelExtensions.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelExtensions.Controls.Add(this.ribbonExtension);
            this.ribbonPanelExtensions.Controls.Add(this.ribbonExtensionCreate);
            this.ribbonPanelExtensions.Controls.Add(this.ribbonExtensionDatabase);
            this.ribbonPanelExtensions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanelExtensions.Location = new System.Drawing.Point(0, 57);
            this.ribbonPanelExtensions.Name = "ribbonPanelExtensions";
            this.ribbonPanelExtensions.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanelExtensions.Size = new System.Drawing.Size(1693, 110);
            // 
            // 
            // 
            this.ribbonPanelExtensions.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelExtensions.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanelExtensions.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanelExtensions.TabIndex = 6;
            this.ribbonPanelExtensions.Visible = false;
            // 
            // ribbonExtension
            // 
            this.ribbonExtension.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonExtension.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExtension.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonExtension.ContainerControlProcessDialogKey = true;
            this.ribbonExtension.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonExtension.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btExtensionModify,
            this.btExtensionDuplicate,
            this.btExtensionDelete});
            this.ribbonExtension.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExtension.Location = new System.Drawing.Point(368, 0);
            this.ribbonExtension.Name = "ribbonExtension";
            this.ribbonExtension.Size = new System.Drawing.Size(146, 107);
            this.ribbonExtension.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExtension.TabIndex = 8;
            this.ribbonExtension.Text = "Selected Extension";
            // 
            // 
            // 
            this.ribbonExtension.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExtension.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btExtensionModify
            // 
            this.btExtensionModify.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionModify.CanCustomize = false;
            this.btExtensionModify.Image = global::QuoterPlan.Properties.Resources.properties;
            this.btExtensionModify.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionModify.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionModify.Name = "btExtensionModify";
            this.btExtensionModify.Text = "Modify";
            // 
            // btExtensionDuplicate
            // 
            this.btExtensionDuplicate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionDuplicate.CanCustomize = false;
            this.btExtensionDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionDuplicate.Image")));
            this.btExtensionDuplicate.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionDuplicate.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionDuplicate.Name = "btExtensionDuplicate";
            this.btExtensionDuplicate.Text = "Duplicate";
            // 
            // btExtensionDelete
            // 
            this.btExtensionDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionDelete.CanCustomize = false;
            this.btExtensionDelete.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionDelete.Image")));
            this.btExtensionDelete.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionDelete.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionDelete.Name = "btExtensionDelete";
            this.btExtensionDelete.Text = "Delete";
            // 
            // ribbonExtensionCreate
            // 
            this.ribbonExtensionCreate.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonExtensionCreate.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExtensionCreate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonExtensionCreate.CanCustomize = false;
            this.ribbonExtensionCreate.ContainerControlProcessDialogKey = true;
            this.ribbonExtensionCreate.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonExtensionCreate.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btExtensionArea,
            this.btExtensionPerimeter,
            this.btExtensionRuler,
            this.btExtensionCounter});
            this.ribbonExtensionCreate.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExtensionCreate.Location = new System.Drawing.Point(128, 0);
            this.ribbonExtensionCreate.Name = "ribbonExtensionCreate";
            this.ribbonExtensionCreate.Size = new System.Drawing.Size(240, 107);
            this.ribbonExtensionCreate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExtensionCreate.TabIndex = 7;
            this.ribbonExtensionCreate.Text = "Create Extension";
            // 
            // 
            // 
            this.ribbonExtensionCreate.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExtensionCreate.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btExtensionArea
            // 
            this.btExtensionArea.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionArea.CanCustomize = false;
            this.btExtensionArea.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionArea.Image")));
            this.btExtensionArea.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionArea.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionArea.Name = "btExtensionArea";
            this.btExtensionArea.Text = "Area Extension";
            // 
            // btExtensionPerimeter
            // 
            this.btExtensionPerimeter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionPerimeter.CanCustomize = false;
            this.btExtensionPerimeter.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionPerimeter.Image")));
            this.btExtensionPerimeter.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionPerimeter.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionPerimeter.Name = "btExtensionPerimeter";
            this.btExtensionPerimeter.Text = "Perimeter Extension";
            // 
            // btExtensionRuler
            // 
            this.btExtensionRuler.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionRuler.CanCustomize = false;
            this.btExtensionRuler.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionRuler.Image")));
            this.btExtensionRuler.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionRuler.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionRuler.Name = "btExtensionRuler";
            this.btExtensionRuler.Text = "Length Extension";
            // 
            // btExtensionCounter
            // 
            this.btExtensionCounter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionCounter.CanCustomize = false;
            this.btExtensionCounter.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionCounter.Image")));
            this.btExtensionCounter.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionCounter.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionCounter.Name = "btExtensionCounter";
            this.btExtensionCounter.Text = "Counter Extension";
            // 
            // ribbonExtensionDatabase
            // 
            this.ribbonExtensionDatabase.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonExtensionDatabase.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExtensionDatabase.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonExtensionDatabase.ContainerControlProcessDialogKey = true;
            this.ribbonExtensionDatabase.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonExtensionDatabase.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btExtensionTradesPackages,
            this.btExtensionCompactDatabase});
            this.ribbonExtensionDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExtensionDatabase.Location = new System.Drawing.Point(3, 0);
            this.ribbonExtensionDatabase.Name = "ribbonExtensionDatabase";
            this.ribbonExtensionDatabase.Size = new System.Drawing.Size(125, 107);
            this.ribbonExtensionDatabase.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExtensionDatabase.TabIndex = 9;
            this.ribbonExtensionDatabase.Text = "Database";
            // 
            // 
            // 
            this.ribbonExtensionDatabase.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonExtensionDatabase.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btExtensionTradesPackages
            // 
            this.btExtensionTradesPackages.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionTradesPackages.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionTradesPackages.Image")));
            this.btExtensionTradesPackages.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionTradesPackages.ImagePaddingHorizontal = 10;
            this.btExtensionTradesPackages.ImagePaddingVertical = 5;
            this.btExtensionTradesPackages.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionTradesPackages.Name = "btExtensionTradesPackages";
            this.btExtensionTradesPackages.Text = "Trade Packages";
            // 
            // btExtensionCompactDatabase
            // 
            this.btExtensionCompactDatabase.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExtensionCompactDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btExtensionCompactDatabase.Image")));
            this.btExtensionCompactDatabase.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btExtensionCompactDatabase.ImagePaddingVertical = 5;
            this.btExtensionCompactDatabase.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btExtensionCompactDatabase.Name = "btExtensionCompactDatabase";
            this.btExtensionCompactDatabase.Text = "Compact Database";
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bEditPopup});
            this.contextMenuBar1.Location = new System.Drawing.Point(1048, 174);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(150, 25);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.contextMenuBar1.TabIndex = 24;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.WrapItemsDock = true;
            // 
            // bEditPopup
            // 
            this.bEditPopup.AutoExpandOnClick = true;
            this.bEditPopup.GlobalName = "bEditPopup";
            this.bEditPopup.Name = "bEditPopup";
            this.bEditPopup.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
            this.bEditPopup.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bAutoAdjustToZone,
            this.bEditNote,
            this.bPointInsert,
            this.bPointRemove,
            this.bSetHeight,
            this.bGroupAddObject,
            this.bDeductionCreate,
            this.bDeductionsEdit,
            this.bPerimeterCreateFromArea,
            this.bOpeningCreateFromPosition,
            this.bOpeningDuplicate,
            this.bOpeningCreateFromSegment,
            this.bOpeningDelete,
            this.bDropInsert,
            this.bDropRemove,
            this.bPerimeterOpen,
            this.bPerimeterClose,
            this.bAngleDegreeType,
            this.bAngleSlopeType,
            this.bDeductionDuplicate,
            this.bCut,
            this.bCopy,
            this.bPaste,
            this.bDelete,
            this.bToggleMeasures,
            this.bZoomToObject,
            this.bZoomToGroup,
            this.bBringToFront,
            this.bSendToBack,
            this.bSelectGroup,
            this.bSelectThisGroup,
            this.bSelectObjectType,
            this.bSelectAll,
            this.bUnselectAll,
            this.bLayerMoveTo,
            this.bGroupMoveTo,
            this.bGroupMoveToNew});
            this.bEditPopup.Text = "bEditPopup";
            this.bEditPopup.Visible = false;
            // 
            // bAutoAdjustToZone
            // 
            this.bAutoAdjustToZone.AlternateShortCutText = "A";
            this.bAutoAdjustToZone.BeginGroup = true;
            this.bAutoAdjustToZone.Name = "bAutoAdjustToZone";
            this.bAutoAdjustToZone.Text = "Automatic Adjustment";
            this.bAutoAdjustToZone.Click += new System.EventHandler(this.btAutoAdjustToZone_Click);
            // 
            // bEditNote
            // 
            this.bEditNote.AlternateShortCutText = "E";
            this.bEditNote.Image = global::QuoterPlan.Properties.Resources.note_small;
            this.bEditNote.Name = "bEditNote";
            this.bEditNote.Text = "Edit Note";
            this.bEditNote.Click += new System.EventHandler(this.btEditNote_Click);
            // 
            // bPointInsert
            // 
            this.bPointInsert.AlternateShortCutText = "I";
            this.bPointInsert.Name = "bPointInsert";
            this.bPointInsert.Text = "Insert a Point";
            this.bPointInsert.Click += new System.EventHandler(this.btPointInsert_Click);
            // 
            // bPointRemove
            // 
            this.bPointRemove.AlternateShortCutText = "X";
            this.bPointRemove.Name = "bPointRemove";
            this.bPointRemove.Text = "Delete this Point";
            this.bPointRemove.Click += new System.EventHandler(this.btPointRemove_Click);
            // 
            // bSetHeight
            // 
            this.bSetHeight.AlternateShortCutText = "H";
            this.bSetHeight.Name = "bSetHeight";
            this.bSetHeight.Text = "Modify Height";
            this.bSetHeight.Click += new System.EventHandler(this.bOpeningHeight_Click);
            // 
            // bGroupAddObject
            // 
            this.bGroupAddObject.AlternateShortCutText = "N";
            this.bGroupAddObject.BeginGroup = true;
            this.bGroupAddObject.Name = "bGroupAddObject";
            this.bGroupAddObject.Text = "Add an Object to Group";
            this.bGroupAddObject.Click += new System.EventHandler(this.btGroupAddObject_Click);
            // 
            // bDeductionCreate
            // 
            this.bDeductionCreate.AlternateShortCutText = "S";
            this.bDeductionCreate.Image = global::QuoterPlan.Properties.Resources.deduction_small;
            this.bDeductionCreate.Name = "bDeductionCreate";
            this.bDeductionCreate.Text = "Create a New Subtraction";
            this.bDeductionCreate.Click += new System.EventHandler(this.btDeductionCreate_Click);
            // 
            // bDeductionsEdit
            // 
            this.bDeductionsEdit.AlternateShortCutText = "E";
            this.bDeductionsEdit.Name = "bDeductionsEdit";
            this.bDeductionsEdit.Text = "Edit Subtractions";
            this.bDeductionsEdit.Click += new System.EventHandler(this.btDeductionsEdit_Click);
            // 
            // bPerimeterCreateFromArea
            // 
            this.bPerimeterCreateFromArea.AlternateShortCutText = "P";
            this.bPerimeterCreateFromArea.Image = global::QuoterPlan.Properties.Resources.perimeter_small;
            this.bPerimeterCreateFromArea.Name = "bPerimeterCreateFromArea";
            this.bPerimeterCreateFromArea.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
            this.bPerimeterCreateFromArea.Text = "Create a Perimeter from this Area";
            this.bPerimeterCreateFromArea.Click += new System.EventHandler(this.btPerimeterCreateFromArea_Click);
            // 
            // bOpeningCreateFromPosition
            // 
            this.bOpeningCreateFromPosition.AlternateShortCutText = "O";
            this.bOpeningCreateFromPosition.Name = "bOpeningCreateFromPosition";
            this.bOpeningCreateFromPosition.Text = "Create a New Opening";
            this.bOpeningCreateFromPosition.Click += new System.EventHandler(this.btOpeningCreateFromPosition_Click);
            // 
            // bOpeningDuplicate
            // 
            this.bOpeningDuplicate.AlternateShortCutText = "D";
            this.bOpeningDuplicate.Name = "bOpeningDuplicate";
            this.bOpeningDuplicate.Text = "Duplicate Last Opening";
            this.bOpeningDuplicate.Click += new System.EventHandler(this.btOpeningDuplicate_Click);
            // 
            // bOpeningCreateFromSegment
            // 
            this.bOpeningCreateFromSegment.Name = "bOpeningCreateFromSegment";
            this.bOpeningCreateFromSegment.Text = "Convert Segment into Opening";
            this.bOpeningCreateFromSegment.Click += new System.EventHandler(this.btOpeningCreateFromSegment_Click);
            // 
            // bOpeningDelete
            // 
            this.bOpeningDelete.Name = "bOpeningDelete";
            this.bOpeningDelete.Text = "Convert Opening into Segment";
            this.bOpeningDelete.Click += new System.EventHandler(this.btOpeningDelete_Click);
            // 
            // bDropInsert
            // 
            this.bDropInsert.AlternateShortCutText = "R";
            this.bDropInsert.Name = "bDropInsert";
            this.bDropInsert.Text = "Insert Rise/Drop";
            this.bDropInsert.Click += new System.EventHandler(this.btDropInsert_Click);
            // 
            // bDropRemove
            // 
            this.bDropRemove.Name = "bDropRemove";
            this.bDropRemove.Text = "Delete Rise/Drop";
            this.bDropRemove.Click += new System.EventHandler(this.btDropRemove_Click);
            // 
            // bPerimeterOpen
            // 
            this.bPerimeterOpen.Name = "bPerimeterOpen";
            this.bPerimeterOpen.Text = "Open Perimeter";
            this.bPerimeterOpen.Click += new System.EventHandler(this.btPerimeterOpen_Click);
            // 
            // bPerimeterClose
            // 
            this.bPerimeterClose.Name = "bPerimeterClose";
            this.bPerimeterClose.Text = "Close Perimeter";
            this.bPerimeterClose.Click += new System.EventHandler(this.btPerimeterClose_Click);
            // 
            // bAngleDegreeType
            // 
            this.bAngleDegreeType.Name = "bAngleDegreeType";
            this.bAngleDegreeType.Text = "Display in Degrees";
            this.bAngleDegreeType.Click += new System.EventHandler(this.btAngleDegreeType_Click);
            // 
            // bAngleSlopeType
            // 
            this.bAngleSlopeType.Name = "bAngleSlopeType";
            this.bAngleSlopeType.Text = "Display in Slope Factor";
            this.bAngleSlopeType.Click += new System.EventHandler(this.btAngleSlopeType_Click);
            // 
            // bDeductionDuplicate
            // 
            this.bDeductionDuplicate.AlternateShortCutText = "D";
            this.bDeductionDuplicate.BeginGroup = true;
            this.bDeductionDuplicate.Name = "bDeductionDuplicate";
            this.bDeductionDuplicate.Text = "Duplicate Substraction";
            this.bDeductionDuplicate.Click += new System.EventHandler(this.btDeductionDuplicate_Click);
            // 
            // bCut
            // 
            this.bCut.AlternateShortCutText = "Ctrl+X";
            this.bCut.BeginGroup = true;
            this.bCut.GlobalName = "bCut";
            this.bCut.Image = global::QuoterPlan.Properties.Resources.cut_16x16;
            this.bCut.ImageIndex = 5;
            this.bCut.Name = "bCut";
            this.bCut.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
            this.bCut.Text = "Cut";
            this.bCut.Click += new System.EventHandler(this.btEditCut_Click);
            // 
            // bCopy
            // 
            this.bCopy.AlternateShortCutText = "Ctrl+C";
            this.bCopy.GlobalName = "bCopy";
            this.bCopy.Image = global::QuoterPlan.Properties.Resources.copy_16x16;
            this.bCopy.ImageIndex = 4;
            this.bCopy.Name = "bCopy";
            this.bCopy.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
            this.bCopy.Text = "Copy";
            this.bCopy.Click += new System.EventHandler(this.btEditCopy_Click);
            // 
            // bPaste
            // 
            this.bPaste.AlternateShortCutText = "Ctrl+V";
            this.bPaste.GlobalName = "bPaste";
            this.bPaste.Image = global::QuoterPlan.Properties.Resources.paste_16x16;
            this.bPaste.ImageIndex = 12;
            this.bPaste.Name = "bPaste";
            this.bPaste.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
            this.bPaste.Text = "Paste";
            this.bPaste.Click += new System.EventHandler(this.btEditPaste_Click);
            // 
            // bDelete
            // 
            this.bDelete.AlternateShortCutText = "Del";
            this.bDelete.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
            this.bDelete.Name = "bDelete";
            this.bDelete.Text = "Delete";
            this.bDelete.Click += new System.EventHandler(this.btEditDelete_Click);
            // 
            // bToggleMeasures
            // 
            this.bToggleMeasures.AlternateShortCutText = "M";
            this.bToggleMeasures.BeginGroup = true;
            this.bToggleMeasures.Name = "bToggleMeasures";
            this.bToggleMeasures.Text = "Hide / Restore Measures";
            this.bToggleMeasures.Click += new System.EventHandler(this.btToggleMeasures_Click);
            // 
            // bZoomToObject
            // 
            this.bZoomToObject.AlternateShortCutText = "Z";
            this.bZoomToObject.BeginGroup = true;
            this.bZoomToObject.Image = global::QuoterPlan.Properties.Resources.zoom_16x16;
            this.bZoomToObject.Name = "bZoomToObject";
            this.bZoomToObject.Text = "Zoom to Object";
            this.bZoomToObject.Click += new System.EventHandler(this.btZoomToObject_Click);
            // 
            // bZoomToGroup
            // 
            this.bZoomToGroup.AlternateShortCutText = "G";
            this.bZoomToGroup.Image = global::QuoterPlan.Properties.Resources.selection_16x16_alt;
            this.bZoomToGroup.Name = "bZoomToGroup";
            this.bZoomToGroup.Text = "Zoom to Group";
            this.bZoomToGroup.Click += new System.EventHandler(this.btZoomToGroup_Click);
            // 
            // bBringToFront
            // 
            this.bBringToFront.AlternateShortCutText = "Ctrl+J";
            this.bBringToFront.BeginGroup = true;
            this.bBringToFront.Image = global::QuoterPlan.Properties.Resources.bring_to_front_16x16;
            this.bBringToFront.Name = "bBringToFront";
            this.bBringToFront.Text = "Bring to Front";
            this.bBringToFront.Click += new System.EventHandler(this.btEditBringToFront_Click);
            // 
            // bSendToBack
            // 
            this.bSendToBack.AlternateShortCutText = "Ctrl+K";
            this.bSendToBack.Image = global::QuoterPlan.Properties.Resources.send_to_back_16x16;
            this.bSendToBack.Name = "bSendToBack";
            this.bSendToBack.Text = "Send to Back";
            this.bSendToBack.Click += new System.EventHandler(this.btEditSendToBack_Click);
            // 
            // bSelectGroup
            // 
            this.bSelectGroup.AlternateShortCutText = "Ctrl+G";
            this.bSelectGroup.BeginGroup = true;
            this.bSelectGroup.Name = "bSelectGroup";
            this.bSelectGroup.Text = "Select Group";
            this.bSelectGroup.Click += new System.EventHandler(this.btEditSelectGroup_Click);
            // 
            // bSelectThisGroup
            // 
            this.bSelectThisGroup.Name = "bSelectThisGroup";
            this.bSelectThisGroup.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bSelectThisGroup1});
            this.bSelectThisGroup.Text = "Select this Group";
            // 
            // bSelectThisGroup1
            // 
            this.bSelectThisGroup1.Name = "bSelectThisGroup1";
            this.bSelectThisGroup1.Text = "buttonItem1";
            // 
            // bSelectObjectType
            // 
            this.bSelectObjectType.Name = "bSelectObjectType";
            this.bSelectObjectType.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bSelectObjectType1});
            this.bSelectObjectType.Text = "Select this Object Type";
            // 
            // bSelectObjectType1
            // 
            this.bSelectObjectType1.Name = "bSelectObjectType1";
            this.bSelectObjectType1.Text = "buttonItem1";
            // 
            // bSelectAll
            // 
            this.bSelectAll.AlternateShortCutText = "Ctrl+A";
            this.bSelectAll.GlobalName = "bSelectAll";
            this.bSelectAll.Name = "bSelectAll";
            this.bSelectAll.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
            this.bSelectAll.Text = "Select All Objects for this Layer";
            this.bSelectAll.Click += new System.EventHandler(this.btEditSelectAll_Click);
            // 
            // bUnselectAll
            // 
            this.bUnselectAll.AlternateShortCutText = "Ctrl+D";
            this.bUnselectAll.Name = "bUnselectAll";
            this.bUnselectAll.Text = "Select None";
            this.bUnselectAll.Click += new System.EventHandler(this.btUnselectAll_Click);
            // 
            // bLayerMoveTo
            // 
            this.bLayerMoveTo.BeginGroup = true;
            this.bLayerMoveTo.Name = "bLayerMoveTo";
            this.bLayerMoveTo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bLayerMoveTo1});
            this.bLayerMoveTo.Text = "Move to this Layer";
            this.bLayerMoveTo.Click += new System.EventHandler(this.btLayerMoveTo_Click);
            // 
            // bLayerMoveTo1
            // 
            this.bLayerMoveTo1.Name = "bLayerMoveTo1";
            this.bLayerMoveTo1.Text = "buttonItem1";
            // 
            // bGroupMoveTo
            // 
            this.bGroupMoveTo.Name = "bGroupMoveTo";
            this.bGroupMoveTo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bGroupMoveTo1});
            this.bGroupMoveTo.Text = "Move to this Group";
            this.bGroupMoveTo.Click += new System.EventHandler(this.btGroupMoveTo_Click);
            // 
            // bGroupMoveTo1
            // 
            this.bGroupMoveTo1.Name = "bGroupMoveTo1";
            this.bGroupMoveTo1.Text = "buttonItem1";
            // 
            // bGroupMoveToNew
            // 
            this.bGroupMoveToNew.AlternateShortCutText = "Shift+G";
            this.bGroupMoveToNew.Name = "bGroupMoveToNew";
            this.bGroupMoveToNew.Text = "Move to a New Group";
            this.bGroupMoveToNew.Click += new System.EventHandler(this.btGroupMoveToNew_Click);
            // 
            // ribbonTabStart
            // 
            this.ribbonTabStart.Name = "ribbonTabStart";
            this.ribbonTabStart.Panel = this.ribbonPanel;
            this.ribbonTabStart.Tag = "0";
            this.ribbonTabStart.Text = "&Home";
            // 
            // ribbonTabPlans
            // 
            this.ribbonTabPlans.Name = "ribbonTabPlans";
            this.ribbonTabPlans.Panel = this.ribbonPanelPlans;
            this.ribbonTabPlans.Tag = "1";
            this.ribbonTabPlans.Text = "&Plans";
            // 
            // ribbonTabReport
            // 
            this.ribbonTabReport.Name = "ribbonTabReport";
            this.ribbonTabReport.Panel = this.ribbonPanelReport;
            this.ribbonTabReport.Tag = "2";
            this.ribbonTabReport.Text = "&Reports";
            // 
            // ribbonTabEstimatingItems
            // 
            this.ribbonTabEstimatingItems.Checked = true;
            this.ribbonTabEstimatingItems.Group = this.ribbonTabItemDBManagement;
            this.ribbonTabEstimatingItems.Name = "ribbonTabEstimatingItems";
            this.ribbonTabEstimatingItems.Panel = this.ribbonPanelEstimating;
            this.ribbonTabEstimatingItems.Tag = "4";
            this.ribbonTabEstimatingItems.Text = "Estimating Items";
            // 
            // ribbonTabItemDBManagement
            // 
            this.ribbonTabItemDBManagement.GroupTitle = "Database Management";
            this.ribbonTabItemDBManagement.Name = "ribbonTabItemDBManagement";
            // 
            // 
            // 
            this.ribbonTabItemDBManagement.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(109)))), ((int)(((byte)(148)))));
            this.ribbonTabItemDBManagement.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(72)))), ((int)(((byte)(123)))));
            this.ribbonTabItemDBManagement.Style.BackColorGradientAngle = 90;
            this.ribbonTabItemDBManagement.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderBottomWidth = 1;
            this.ribbonTabItemDBManagement.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(58)))), ((int)(((byte)(59)))));
            this.ribbonTabItemDBManagement.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderLeftWidth = 1;
            this.ribbonTabItemDBManagement.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderRightWidth = 1;
            this.ribbonTabItemDBManagement.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderTopWidth = 1;
            this.ribbonTabItemDBManagement.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonTabItemDBManagement.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ribbonTabItemDBManagement.Style.TextColor = System.Drawing.Color.White;
            this.ribbonTabItemDBManagement.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.ribbonTabItemDBManagement.Style.TextShadowColor = System.Drawing.Color.Black;
            this.ribbonTabItemDBManagement.Style.TextShadowOffset = new System.Drawing.Point(1, 1);
            // 
            // ribbonTabTemplates
            // 
            this.ribbonTabTemplates.Group = this.ribbonTabItemDBManagement;
            this.ribbonTabTemplates.Name = "ribbonTabTemplates";
            this.ribbonTabTemplates.Panel = this.ribbonPanelTemplates;
            this.ribbonTabTemplates.Tag = "3";
            this.ribbonTabTemplates.Text = "Templates Library";
            // 
            // ribbonTabExtensions
            // 
            this.ribbonTabExtensions.Group = this.ribbonTabItemDBManagement;
            this.ribbonTabExtensions.Name = "ribbonTabExtensions";
            this.ribbonTabExtensions.Panel = this.ribbonPanelExtensions;
            this.ribbonTabExtensions.Tag = "5";
            this.ribbonTabExtensions.Text = "Extensions";
            this.ribbonTabExtensions.Visible = false;
            // 
            // lblTrialMessage
            // 
            this.lblTrialMessage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.lblTrialMessage.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.lblTrialMessage.Name = "lblTrialMessage";
            this.lblTrialMessage.Click += new System.EventHandler(this.btLicenseActivate_Click);
            // 
            // btLicensing
            // 
            this.btLicensing.AutoExpandOnClick = true;
            this.btLicensing.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btLicensing.Image = global::QuoterPlan.Properties.Resources.activate_16x16;
            this.btLicensing.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btLicensing.Name = "btLicensing";
            this.btLicensing.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btLicenseBuy,
            this.btLicenseActivate});
            this.btLicensing.Text = "Buy / Activate";
            this.btLicensing.Click += new System.EventHandler(this.btLicensing_Click);
            // 
            // btLicenseBuy
            // 
            this.btLicenseBuy.Name = "btLicenseBuy";
            this.btLicenseBuy.Text = "I want to buy a product key";
            this.btLicenseBuy.Click += new System.EventHandler(this.btLicenseBuy_Click);
            // 
            // btLicenseActivate
            // 
            this.btLicenseActivate.Name = "btLicenseActivate";
            this.btLicenseActivate.Text = "I already have a product key and I want to activate it";
            this.btLicenseActivate.Click += new System.EventHandler(this.btLicenseActivate_Click);
            // 
            // btSettings
            // 
            this.btSettings.AutoExpandOnClick = true;
            this.btSettings.Image = global::QuoterPlan.Properties.Resources.settings_16x16;
            this.btSettings.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btSettings.Name = "btSettings";
            this.btSettings.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblLanguage,
            this.btLanguageEnglish,
            this.btLanguageFrench,
            this.btLanguageSpanish,
            this.lblScrollSpeed,
            this.sliderScrollSpeed,
            this.containerScroll,
            this.lblDataFolder,
            this.lblPersonalPreferences,
            this.btSelectDataFolder,
            this.btPersonalPreferences,
            this.btImportationPreferences,
            this.btEnableAutoBackup,
            this.btSetDBReadOnly,
            this.lblTheme,
            this.btStyleMetro,
            this.btStyleClassicBlue,
            this.btStyleClassicSilver,
            this.btStyleClassicBlack,
            this.btStyleClassicExecutive,
            this.btStyleRetroBlue,
            this.btStyleRetroSilver,
            this.btStyleRetroBlack,
            this.btStyleRetroGlass,
            this.btStyleModern,
            this.btSetThemeColor,
            this.lblPanels,
            this.btResetDefaultPanelsLayout});
            // 
            // lblLanguage
            // 
            this.lblLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblLanguage.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblLanguage.CanCustomize = false;
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.PaddingBottom = 3;
            this.lblLanguage.PaddingLeft = 10;
            this.lblLanguage.PaddingTop = 3;
            this.lblLanguage.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblLanguage.Text = "Language";
            // 
            // btLanguageEnglish
            // 
            this.btLanguageEnglish.Name = "btLanguageEnglish";
            this.btLanguageEnglish.OptionGroup = "Language";
            this.btLanguageEnglish.Text = "English";
            this.btLanguageEnglish.Click += new System.EventHandler(this.btLanguageEnglish_Click);
            // 
            // btLanguageFrench
            // 
            this.btLanguageFrench.Name = "btLanguageFrench";
            this.btLanguageFrench.OptionGroup = "Language";
            this.btLanguageFrench.Text = "French";
            this.btLanguageFrench.Click += new System.EventHandler(this.btLanguageFrench_Click);
            // 
            // btLanguageSpanish
            // 
            this.btLanguageSpanish.Name = "btLanguageSpanish";
            this.btLanguageSpanish.OptionGroup = "Language";
            this.btLanguageSpanish.Text = "Spanish";
            this.btLanguageSpanish.Click += new System.EventHandler(this.btLanguageSpanish_Click);
            // 
            // lblScrollSpeed
            // 
            this.lblScrollSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblScrollSpeed.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblScrollSpeed.CanCustomize = false;
            this.lblScrollSpeed.Name = "lblScrollSpeed";
            this.lblScrollSpeed.PaddingBottom = 3;
            this.lblScrollSpeed.PaddingLeft = 10;
            this.lblScrollSpeed.PaddingTop = 3;
            this.lblScrollSpeed.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblScrollSpeed.Text = "Scroll Speed";
            // 
            // sliderScrollSpeed
            // 
            this.sliderScrollSpeed.AutoCollapseOnClick = false;
            this.sliderScrollSpeed.CanCustomize = false;
            this.sliderScrollSpeed.LabelVisible = false;
            this.sliderScrollSpeed.LabelWidth = 50;
            this.sliderScrollSpeed.Name = "sliderScrollSpeed";
            this.sliderScrollSpeed.Step = 5;
            this.sliderScrollSpeed.TextColor = System.Drawing.Color.Black;
            this.sliderScrollSpeed.TrackMarker = false;
            this.sliderScrollSpeed.Value = 0;
            this.sliderScrollSpeed.Width = 150;
            // 
            // containerScroll
            // 
            // 
            // 
            // 
            this.containerScroll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.containerScroll.FixedSize = new System.Drawing.Size(0, 16);
            this.containerScroll.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.containerScroll.Name = "containerScroll";
            this.containerScroll.ResizeItemsToFit = false;
            this.containerScroll.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblScrollFast,
            this.lblScrollPadding,
            this.lblScrollSlow});
            // 
            // 
            // 
            this.containerScroll.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblScrollFast
            // 
            this.lblScrollFast.CanCustomize = false;
            this.lblScrollFast.Name = "lblScrollFast";
            this.lblScrollFast.Text = "Faster";
            this.lblScrollFast.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblScrollFast.WordWrap = true;
            // 
            // lblScrollPadding
            // 
            this.lblScrollPadding.CanCustomize = false;
            this.lblScrollPadding.Name = "lblScrollPadding";
            this.lblScrollPadding.Stretch = true;
            this.lblScrollPadding.Width = 100;
            // 
            // lblScrollSlow
            // 
            this.lblScrollSlow.CanCustomize = false;
            this.lblScrollSlow.Name = "lblScrollSlow";
            this.lblScrollSlow.Text = "Slower";
            this.lblScrollSlow.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblScrollSlow.WordWrap = true;
            // 
            // lblDataFolder
            // 
            this.lblDataFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblDataFolder.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblDataFolder.CanCustomize = false;
            this.lblDataFolder.Name = "lblDataFolder";
            this.lblDataFolder.PaddingBottom = 3;
            this.lblDataFolder.PaddingLeft = 10;
            this.lblDataFolder.PaddingTop = 3;
            this.lblDataFolder.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblDataFolder.Text = "Data Folder";
            this.lblDataFolder.Visible = false;
            // 
            // lblPersonalPreferences
            // 
            this.lblPersonalPreferences.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblPersonalPreferences.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblPersonalPreferences.CanCustomize = false;
            this.lblPersonalPreferences.Name = "lblPersonalPreferences";
            this.lblPersonalPreferences.PaddingBottom = 3;
            this.lblPersonalPreferences.PaddingLeft = 10;
            this.lblPersonalPreferences.PaddingTop = 3;
            this.lblPersonalPreferences.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPersonalPreferences.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem3});
            this.lblPersonalPreferences.Text = "Preferences";
            // 
            // buttonItem3
            // 
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "Importation Preferences...";
            // 
            // btSelectDataFolder
            // 
            this.btSelectDataFolder.Name = "btSelectDataFolder";
            this.btSelectDataFolder.Text = "Change Data Folder...";
            this.btSelectDataFolder.Click += new System.EventHandler(this.btSelectDataFolder_Click);
            // 
            // btPersonalPreferences
            // 
            this.btPersonalPreferences.Name = "btPersonalPreferences";
            this.btPersonalPreferences.Text = "Edit Personal Preferences...";
            this.btPersonalPreferences.Click += new System.EventHandler(this.btPersonalPreferences_Click);
            // 
            // btImportationPreferences
            // 
            this.btImportationPreferences.Name = "btImportationPreferences";
            this.btImportationPreferences.Text = "Import Settings...";
            this.btImportationPreferences.Click += new System.EventHandler(this.btImportationPreferences_Click);
            // 
            // btEnableAutoBackup
            // 
            this.btEnableAutoBackup.BeginGroup = true;
            this.btEnableAutoBackup.Name = "btEnableAutoBackup";
            this.btEnableAutoBackup.Text = "Enable Auto Backup";
            this.btEnableAutoBackup.Click += new System.EventHandler(this.btEnableAutoBackup_Click);
            // 
            // btSetDBReadOnly
            // 
            this.btSetDBReadOnly.Name = "btSetDBReadOnly";
            this.btSetDBReadOnly.Text = "Lock Database";
            this.btSetDBReadOnly.Click += new System.EventHandler(this.btSetDBReadOnly_Click);
            // 
            // lblTheme
            // 
            this.lblTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblTheme.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblTheme.CanCustomize = false;
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.PaddingBottom = 3;
            this.lblTheme.PaddingLeft = 10;
            this.lblTheme.PaddingTop = 3;
            this.lblTheme.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblTheme.Text = "Theme";
            // 
            // btStyleMetro
            // 
            this.btStyleMetro.CanCustomize = false;
            this.btStyleMetro.Command = this.AppCommandTheme;
            this.btStyleMetro.CommandParameter = "Metro";
            this.btStyleMetro.Name = "btStyleMetro";
            this.btStyleMetro.OptionGroup = "style";
            this.btStyleMetro.Text = "Modern";
            // 
            // AppCommandTheme
            // 
            this.AppCommandTheme.Name = "AppCommandTheme";
            this.AppCommandTheme.Executed += new System.EventHandler(this.AppCommandTheme_Executed);
            // 
            // btStyleClassicBlue
            // 
            this.btStyleClassicBlue.BeginGroup = true;
            this.btStyleClassicBlue.CanCustomize = false;
            this.btStyleClassicBlue.Command = this.AppCommandTheme;
            this.btStyleClassicBlue.CommandParameter = "Office2010Blue";
            this.btStyleClassicBlue.Name = "btStyleClassicBlue";
            this.btStyleClassicBlue.OptionGroup = "style";
            this.btStyleClassicBlue.Text = "Classic - Blue";
            // 
            // btStyleClassicSilver
            // 
            this.btStyleClassicSilver.CanCustomize = false;
            this.btStyleClassicSilver.Command = this.AppCommandTheme;
            this.btStyleClassicSilver.CommandParameter = "Office2010Silver";
            this.btStyleClassicSilver.Name = "btStyleClassicSilver";
            this.btStyleClassicSilver.OptionGroup = "style";
            this.btStyleClassicSilver.Text = "Classic - Silver";
            // 
            // btStyleClassicBlack
            // 
            this.btStyleClassicBlack.CanCustomize = false;
            this.btStyleClassicBlack.Command = this.AppCommandTheme;
            this.btStyleClassicBlack.CommandParameter = "Office2010Black";
            this.btStyleClassicBlack.Name = "btStyleClassicBlack";
            this.btStyleClassicBlack.OptionGroup = "style";
            this.btStyleClassicBlack.Text = "Classic - Black";
            // 
            // btStyleClassicExecutive
            // 
            this.btStyleClassicExecutive.CanCustomize = false;
            this.btStyleClassicExecutive.Command = this.AppCommandTheme;
            this.btStyleClassicExecutive.CommandParameter = "VisualStudio2010Blue";
            this.btStyleClassicExecutive.Name = "btStyleClassicExecutive";
            this.btStyleClassicExecutive.OptionGroup = "style";
            this.btStyleClassicExecutive.Text = "Classic - Executive";
            // 
            // btStyleRetroBlue
            // 
            this.btStyleRetroBlue.BeginGroup = true;
            this.btStyleRetroBlue.CanCustomize = false;
            this.btStyleRetroBlue.Command = this.AppCommandTheme;
            this.btStyleRetroBlue.CommandParameter = "Office2007Blue";
            this.btStyleRetroBlue.Name = "btStyleRetroBlue";
            this.btStyleRetroBlue.OptionGroup = "style";
            this.btStyleRetroBlue.Text = "Retro - Blue";
            // 
            // btStyleRetroSilver
            // 
            this.btStyleRetroSilver.CanCustomize = false;
            this.btStyleRetroSilver.Command = this.AppCommandTheme;
            this.btStyleRetroSilver.CommandParameter = "Office2007Silver";
            this.btStyleRetroSilver.Name = "btStyleRetroSilver";
            this.btStyleRetroSilver.OptionGroup = "style";
            this.btStyleRetroSilver.Text = "Retro - Silver";
            // 
            // btStyleRetroBlack
            // 
            this.btStyleRetroBlack.CanCustomize = false;
            this.btStyleRetroBlack.Checked = true;
            this.btStyleRetroBlack.Command = this.AppCommandTheme;
            this.btStyleRetroBlack.CommandParameter = "Office2007Black";
            this.btStyleRetroBlack.Name = "btStyleRetroBlack";
            this.btStyleRetroBlack.OptionGroup = "style";
            this.btStyleRetroBlack.Text = "Retro - Black";
            // 
            // btStyleRetroGlass
            // 
            this.btStyleRetroGlass.CanCustomize = false;
            this.btStyleRetroGlass.Command = this.AppCommandTheme;
            this.btStyleRetroGlass.CommandParameter = "Office2007VistaGlass";
            this.btStyleRetroGlass.Name = "btStyleRetroGlass";
            this.btStyleRetroGlass.OptionGroup = "style";
            this.btStyleRetroGlass.Text = "Retro - Glass";
            // 
            // btStyleModern
            // 
            this.btStyleModern.BeginGroup = true;
            this.btStyleModern.CanCustomize = false;
            this.btStyleModern.Command = this.AppCommandTheme;
            this.btStyleModern.CommandParameter = "Windows7Blue";
            this.btStyleModern.Name = "btStyleModern";
            this.btStyleModern.OptionGroup = "style";
            this.btStyleModern.Text = "Windows 7";
            this.btStyleModern.Visible = false;
            // 
            // btSetThemeColor
            // 
            this.btSetThemeColor.BeginGroup = true;
            this.btSetThemeColor.CanCustomize = false;
            this.btSetThemeColor.Command = this.AppCommandTheme;
            this.btSetThemeColor.DisplayMoreColors = false;
            this.btSetThemeColor.Name = "btSetThemeColor";
            this.btSetThemeColor.Text = "Chromatic Variation";
            this.btSetThemeColor.SelectedColorChanged += new System.EventHandler(this.btSetThemeColor_SelectedColorChanged);
            this.btSetThemeColor.ColorPreview += new DevComponents.DotNetBar.ColorPreviewEventHandler(this.btSetThemeColor_ColorPreview);
            this.btSetThemeColor.PopupShowing += new System.EventHandler(this.btSetThemeColor_PopupShowing);
            this.btSetThemeColor.ExpandChange += new System.EventHandler(this.btSetThemeColor_ExpandChange);
            // 
            // lblPanels
            // 
            this.lblPanels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
            this.lblPanels.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.lblPanels.CanCustomize = false;
            this.lblPanels.Name = "lblPanels";
            this.lblPanels.PaddingBottom = 3;
            this.lblPanels.PaddingLeft = 10;
            this.lblPanels.PaddingTop = 3;
            this.lblPanels.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.lblPanels.Text = "Panels";
            // 
            // btResetDefaultPanelsLayout
            // 
            this.btResetDefaultPanelsLayout.Name = "btResetDefaultPanelsLayout";
            this.btResetDefaultPanelsLayout.Text = "Restore Default Layout";
            this.btResetDefaultPanelsLayout.Click += new System.EventHandler(this.btResetDefaultPanelsLayout_Click);
            // 
            // btHelp
            // 
            this.btHelp.Image = ((System.Drawing.Image)(resources.GetObject("btHelp.Image")));
            this.btHelp.Name = "btHelp";
            this.btHelp.Text = "btHelp";
            this.btHelp.Click += new System.EventHandler(this.btHelp_Click);
            // 
            // startButton
            // 
            this.startButton.AutoExpandOnClick = true;
            this.startButton.CanCustomize = false;
            this.startButton.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.startButton.Image = global::QuoterPlan.Properties.Resources.ruler;
            this.startButton.ImagePaddingHorizontal = 2;
            this.startButton.ImagePaddingVertical = 2;
            this.startButton.Name = "startButton";
            this.startButton.ShowSubItems = false;
            this.startButton.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerFileMenu});
            this.startButton.Text = "&File";
            this.startButton.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.startButton_PopupOpen);
            this.startButton.PopupClose += new System.EventHandler(this.startButton_PopupClose);
            // 
            // itemContainerFileMenu
            // 
            // 
            // 
            // 
            this.itemContainerFileMenu.BackgroundStyle.Class = "RibbonFileMenuTwoColumnContainer";
            this.itemContainerFileMenu.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerFileMenu.CanCustomize = false;
            this.itemContainerFileMenu.ItemSpacing = 0;
            this.itemContainerFileMenu.Name = "itemContainerFileMenu";
            this.itemContainerFileMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerFileMenu2,
            this.galleryRecentProjects});
            // 
            // 
            // 
            this.itemContainerFileMenu.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // itemContainerFileMenu2
            // 
            // 
            // 
            // 
            this.itemContainerFileMenu2.BackgroundStyle.Class = "RibbonFileMenuColumnOneContainer";
            this.itemContainerFileMenu2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerFileMenu2.CanCustomize = false;
            this.itemContainerFileMenu2.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerFileMenu2.MinimumSize = new System.Drawing.Size(200, 0);
            this.itemContainerFileMenu2.Name = "itemContainerFileMenu2";
            this.itemContainerFileMenu2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btProjectNew,
            this.btProjectOpen,
            this.btProjectSave,
            this.btProjectSaveAs,
            this.btProjectInfo,
            this.btProjectClose,
            this.btHelpContent,
            this.btHelpYoutube,
            this.btHelpAbout,
            this.btLicenseDeactivate,
            this.btExit});
            // 
            // 
            // 
            this.itemContainerFileMenu2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btProjectNew
            // 
            this.btProjectNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btProjectNew.CanCustomize = false;
            this.btProjectNew.Image = ((System.Drawing.Image)(resources.GetObject("btProjectNew.Image")));
            this.btProjectNew.ImagePaddingVertical = 5;
            this.btProjectNew.Name = "btProjectNew";
            this.btProjectNew.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN);
            this.btProjectNew.SubItemsExpandWidth = 24;
            this.btProjectNew.Text = "&New";
            this.btProjectNew.Click += new System.EventHandler(this.btProjectNew_Click);
            // 
            // btProjectOpen
            // 
            this.btProjectOpen.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btProjectOpen.CanCustomize = false;
            this.btProjectOpen.Image = ((System.Drawing.Image)(resources.GetObject("btProjectOpen.Image")));
            this.btProjectOpen.ImagePaddingVertical = 5;
            this.btProjectOpen.Name = "btProjectOpen";
            this.btProjectOpen.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlO);
            this.btProjectOpen.SubItemsExpandWidth = 24;
            this.btProjectOpen.Text = "&Open...";
            this.btProjectOpen.Click += new System.EventHandler(this.btProjectOpen_Click);
            // 
            // btProjectSave
            // 
            this.btProjectSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btProjectSave.CanCustomize = false;
            this.btProjectSave.Image = ((System.Drawing.Image)(resources.GetObject("btProjectSave.Image")));
            this.btProjectSave.ImagePaddingVertical = 5;
            this.btProjectSave.Name = "btProjectSave";
            this.btProjectSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.btProjectSave.SubItemsExpandWidth = 24;
            this.btProjectSave.Text = "&Save";
            this.btProjectSave.Click += new System.EventHandler(this.btProjectSave_Click);
            // 
            // btProjectSaveAs
            // 
            this.btProjectSaveAs.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btProjectSaveAs.CanCustomize = false;
            this.btProjectSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btProjectSaveAs.Image")));
            this.btProjectSaveAs.ImagePaddingVertical = 5;
            this.btProjectSaveAs.Name = "btProjectSaveAs";
            this.btProjectSaveAs.SubItemsExpandWidth = 24;
            this.btProjectSaveAs.Text = "Save &As...";
            this.btProjectSaveAs.Click += new System.EventHandler(this.btProjectSaveAs_Click);
            // 
            // btProjectInfo
            // 
            this.btProjectInfo.BeginGroup = true;
            this.btProjectInfo.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btProjectInfo.CanCustomize = false;
            this.btProjectInfo.Image = global::QuoterPlan.Properties.Resources.properties;
            this.btProjectInfo.Name = "btProjectInfo";
            this.btProjectInfo.Text = "&Properties";
            this.btProjectInfo.Click += new System.EventHandler(this.btProjectInfo_Click);
            // 
            // btProjectClose
            // 
            this.btProjectClose.BeginGroup = true;
            this.btProjectClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btProjectClose.CanCustomize = false;
            this.btProjectClose.Image = ((System.Drawing.Image)(resources.GetObject("btProjectClose.Image")));
            this.btProjectClose.ImagePaddingVertical = 5;
            this.btProjectClose.Name = "btProjectClose";
            this.btProjectClose.SubItemsExpandWidth = 24;
            this.btProjectClose.Text = "&Close";
            this.btProjectClose.Click += new System.EventHandler(this.btProjectClose_Click);
            // 
            // btHelpContent
            // 
            this.btHelpContent.BeginGroup = true;
            this.btHelpContent.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btHelpContent.CanCustomize = false;
            this.btHelpContent.Image = ((System.Drawing.Image)(resources.GetObject("btHelpContent.Image")));
            this.btHelpContent.ImagePaddingVertical = 5;
            this.btHelpContent.Name = "btHelpContent";
            this.btHelpContent.SubItemsExpandWidth = 24;
            this.btHelpContent.Text = "&Help";
            this.btHelpContent.Click += new System.EventHandler(this.btHelpContent_Click);
            // 
            // btHelpYoutube
            // 
            this.btHelpYoutube.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btHelpYoutube.CanCustomize = false;
            this.btHelpYoutube.Image = ((System.Drawing.Image)(resources.GetObject("btHelpYoutube.Image")));
            this.btHelpYoutube.ImagePaddingVertical = 5;
            this.btHelpYoutube.Name = "btHelpYoutube";
            this.btHelpYoutube.SubItemsExpandWidth = 24;
            this.btHelpYoutube.Text = "Online &Tutorial";
            this.btHelpYoutube.Click += new System.EventHandler(this.btHelpYoutube_Click);
            // 
            // btHelpAbout
            // 
            this.btHelpAbout.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btHelpAbout.CanCustomize = false;
            this.btHelpAbout.Image = ((System.Drawing.Image)(resources.GetObject("btHelpAbout.Image")));
            this.btHelpAbout.ImagePaddingVertical = 5;
            this.btHelpAbout.Name = "btHelpAbout";
            this.btHelpAbout.SubItemsExpandWidth = 24;
            this.btHelpAbout.Text = "&About Quoter Plan";
            this.btHelpAbout.Click += new System.EventHandler(this.btHelpAbout_Click);
            // 
            // btLicenseDeactivate
            // 
            this.btLicenseDeactivate.BeginGroup = true;
            this.btLicenseDeactivate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btLicenseDeactivate.CanCustomize = false;
            this.btLicenseDeactivate.Image = global::QuoterPlan.Properties.Resources.deactivate;
            this.btLicenseDeactivate.ImagePaddingVertical = 5;
            this.btLicenseDeactivate.Name = "btLicenseDeactivate";
            this.btLicenseDeactivate.Text = "&Deactivate / Transfer Product Key...";
            this.btLicenseDeactivate.Click += new System.EventHandler(this.btLicenseDeactivate_Click);
            // 
            // btExit
            // 
            this.btExit.BeginGroup = true;
            this.btExit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btExit.CanCustomize = false;
            this.btExit.Image = global::QuoterPlan.Properties.Resources.exit;
            this.btExit.Name = "btExit";
            this.btExit.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.AltF4);
            this.btExit.Text = "&Quit";
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // galleryRecentProjects
            // 
            // 
            // 
            // 
            this.galleryRecentProjects.BackgroundStyle.Class = "RibbonFileMenuColumnTwoContainer";
            this.galleryRecentProjects.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galleryRecentProjects.CanCustomize = false;
            this.galleryRecentProjects.DefaultSize = new System.Drawing.Size(358, 240);
            this.galleryRecentProjects.EnableGalleryPopup = false;
            this.galleryRecentProjects.FixedSize = new System.Drawing.Size(358, 240);
            this.galleryRecentProjects.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.galleryRecentProjects.MinimumSize = new System.Drawing.Size(358, 240);
            this.galleryRecentProjects.MultiLine = false;
            this.galleryRecentProjects.Name = "galleryRecentProjects";
            this.galleryRecentProjects.PopupUsesStandardScrollbars = false;
            this.galleryRecentProjects.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblFileRecentProjects});
            // 
            // 
            // 
            this.galleryRecentProjects.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblFileRecentProjects
            // 
            this.lblFileRecentProjects.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.lblFileRecentProjects.BorderType = DevComponents.DotNetBar.eBorderType.Etched;
            this.lblFileRecentProjects.CanCustomize = false;
            this.lblFileRecentProjects.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFileRecentProjects.Name = "lblFileRecentProjects";
            this.lblFileRecentProjects.PaddingBottom = 2;
            this.lblFileRecentProjects.PaddingTop = 2;
            this.lblFileRecentProjects.Stretch = true;
            this.lblFileRecentProjects.Text = "Recent Projects";
            // 
            // btSave
            // 
            this.btSave.Image = global::QuoterPlan.Properties.Resources.document_save_16x16;
            this.btSave.Name = "btSave";
            this.btSave.Click += new System.EventHandler(this.btProjectSave_Click);
            // 
            // btUndo
            // 
            this.btUndo.BeginGroup = true;
            this.btUndo.Image = global::QuoterPlan.Properties.Resources.undo_icon__16x16;
            this.btUndo.Name = "btUndo";
            this.btUndo.Click += new System.EventHandler(this.btEditUndo_Click);
            // 
            // btRedo
            // 
            this.btRedo.Image = global::QuoterPlan.Properties.Resources.redo_icon_16x16;
            this.btRedo.Name = "btRedo";
            this.btRedo.Click += new System.EventHandler(this.btEditRedo_Click);
            // 
            // galleryGroup1
            // 
            this.galleryGroup1.Name = "galleryGroup1";
            this.galleryGroup1.Text = "Available areas for project.";
            // 
            // barStatus
            // 
            this.barStatus.AccessibleDescription = "DotNetBar Bar (barStatus)";
            this.barStatus.AccessibleName = "DotNetBar Bar";
            this.barStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
            this.barStatus.AntiAlias = true;
            this.barStatus.BarType = DevComponents.DotNetBar.eBarType.StatusBar;
            this.barStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.barStatus.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle;
            this.barStatus.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblStatus,
            this.lblStatusBarPadding,
            this.lblOrtho,
            this.switchOrtho,
            this.lblStatusBarPadding2,
            this.lblImageQuality,
            this.qualitySlider,
            this.lblStatusBarPadding3,
            this.lblZoom,
            this.zoomSlider});
            this.barStatus.Location = new System.Drawing.Point(5, 717);
            this.barStatus.Name = "barStatus";
            this.barStatus.PaddingBottom = 3;
            this.barStatus.PaddingTop = 2;
            this.barStatus.Size = new System.Drawing.Size(1729, 29);
            this.barStatus.Stretch = true;
            this.barStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barStatus.TabIndex = 1;
            this.barStatus.TabStop = false;
            this.barStatus.Text = "bar1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.PaddingBottom = 3;
            this.lblStatus.PaddingTop = 3;
            this.lblStatus.Text = "lblStatus";
            this.lblStatus.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // lblStatusBarPadding
            // 
            this.lblStatusBarPadding.Name = "lblStatusBarPadding";
            this.lblStatusBarPadding.Width = 3;
            // 
            // lblOrtho
            // 
            this.lblOrtho.Name = "lblOrtho";
            this.lblOrtho.PaddingBottom = 1;
            this.lblOrtho.PaddingRight = 3;
            this.lblOrtho.Text = "Ortho";
            this.lblOrtho.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // switchOrtho
            // 
            this.switchOrtho.ButtonWidth = 100;
            this.switchOrtho.Name = "switchOrtho";
            this.switchOrtho.OffBackColor = System.Drawing.Color.Linen;
            this.switchOrtho.OffTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchOrtho.OnBackColor = System.Drawing.Color.ForestGreen;
            this.switchOrtho.OnTextColor = System.Drawing.Color.White;
            this.switchOrtho.SwitchWidth = 50;
            this.switchOrtho.ValueChanged += new System.EventHandler(this.switchOrtho_ValueChanged);
            // 
            // lblStatusBarPadding2
            // 
            this.lblStatusBarPadding2.Name = "lblStatusBarPadding2";
            this.lblStatusBarPadding2.Width = 2;
            // 
            // lblImageQuality
            // 
            this.lblImageQuality.Name = "lblImageQuality";
            this.lblImageQuality.PaddingBottom = 1;
            this.lblImageQuality.Text = global::QuoterPlan.Properties.Resources.Qualité_Haute;
            this.lblImageQuality.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblImageQuality.Width = 90;
            // 
            // qualitySlider
            // 
            this.qualitySlider.LabelVisible = false;
            this.qualitySlider.Maximum = 1;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Text = "sliderItem1";
            this.qualitySlider.TrackMarker = false;
            this.qualitySlider.Value = 1;
            this.qualitySlider.Width = 60;
            this.qualitySlider.ValueChanged += new System.EventHandler(this.qualitySlider_ValueChanged);
            // 
            // lblStatusBarPadding3
            // 
            this.lblStatusBarPadding3.Name = "lblStatusBarPadding3";
            this.lblStatusBarPadding3.Width = 3;
            // 
            // lblZoom
            // 
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.PaddingBottom = 1;
            this.lblZoom.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblStatusPadding2});
            this.lblZoom.Text = "100%";
            this.lblZoom.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblZoom.Width = 35;
            // 
            // lblStatusPadding2
            // 
            this.lblStatusPadding2.Name = "lblStatusPadding2";
            this.lblStatusPadding2.Width = 5;
            // 
            // zoomSlider
            // 
            this.zoomSlider.LabelVisible = false;
            this.zoomSlider.Maximum = 300;
            this.zoomSlider.Minimum = 10;
            this.zoomSlider.Name = "zoomSlider";
            this.zoomSlider.Text = "sliderItem1";
            this.zoomSlider.TrackMarker = false;
            this.zoomSlider.Value = 0;
            this.zoomSlider.Width = 200;
            this.zoomSlider.ValueChanged += new System.EventHandler(this.zoomSlider_ValueChanged);
            this.zoomSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomSlider_MouseUp);
            // 
            // lstLayers
            // 
            this.lstLayers.AllowDrop = true;
            this.lstLayers.AllowUserToResizeColumns = false;
            this.lstLayers.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.lstLayers.BackgroundStyle.BorderColor = System.Drawing.Color.Transparent;
            this.lstLayers.BackgroundStyle.BorderColor2 = System.Drawing.Color.Transparent;
            this.lstLayers.BackgroundStyle.Class = "TreeBorderKey";
            this.lstLayers.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstLayers.CellEdit = true;
            this.lstLayers.Columns.Add(this.columnLayerVisible);
            this.lstLayers.Columns.Add(this.columnLayerName);
            this.lstLayers.Columns.Add(this.columnLayerOpacity);
            this.lstLayers.ColumnsVisible = false;
            this.lstLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLayers.DragDropEnabled = false;
            this.lstLayers.DragDropNodeCopyEnabled = false;
            this.lstLayers.ExpandBorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.lstLayers.ExpandWidth = 0;
            this.lstLayers.GridColumnLineResizeEnabled = true;
            this.lstLayers.GridColumnLines = false;
            this.lstLayers.HotTracking = true;
            this.lstLayers.HScrollBarVisible = false;
            this.lstLayers.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.lstLayers.Location = new System.Drawing.Point(0, 25);
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.NodeHorizontalSpacing = 0;
            this.lstLayers.NodesConnector = this.nodeConnector2;
            this.lstLayers.NodeSpacing = 1;
            this.lstLayers.NodeStyle = this.elementStyle1;
            this.lstLayers.NodeStyleMouseOver = this.elementStyle8;
            this.lstLayers.NodeStyleSelected = this.elementStyle2;
            this.lstLayers.PathSeparator = ";";
            this.lstLayers.Size = new System.Drawing.Size(279, 137);
            this.lstLayers.Styles.Add(this.elementStyle1);
            this.lstLayers.Styles.Add(this.elementStyle2);
            this.lstLayers.Styles.Add(this.elementStyle8);
            this.lstLayers.TabIndex = 28;
            this.lstLayers.Text = "advTree1";
            // 
            // columnLayerVisible
            // 
            this.columnLayerVisible.Name = "columnLayerVisible";
            this.columnLayerVisible.Text = "Column";
            this.columnLayerVisible.Width.AutoSize = true;
            // 
            // columnLayerName
            // 
            this.columnLayerName.MaxInputLength = 50;
            this.columnLayerName.Name = "columnLayerName";
            this.columnLayerName.StretchToFill = true;
            this.columnLayerName.Text = "Column";
            // 
            // columnLayerOpacity
            // 
            this.columnLayerOpacity.Name = "columnLayerOpacity";
            this.columnLayerOpacity.Text = "Column";
            this.columnLayerOpacity.Width.Absolute = 90;
            // 
            // nodeConnector2
            // 
            this.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.BorderTopWidth = 1;
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle8
            // 
            this.elementStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.elementStyle8.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(224)))), ((int)(((byte)(252)))));
            this.elementStyle8.BackColorGradientAngle = 90;
            this.elementStyle8.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle8.BorderBottomWidth = 1;
            this.elementStyle8.BorderColor = System.Drawing.Color.DarkGray;
            this.elementStyle8.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle8.BorderLeftWidth = 1;
            this.elementStyle8.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle8.BorderRightWidth = 1;
            this.elementStyle8.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle8.BorderTopWidth = 1;
            this.elementStyle8.CornerDiameter = 4;
            this.elementStyle8.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.elementStyle8.Description = "BlueLight";
            this.elementStyle8.Name = "elementStyle8";
            this.elementStyle8.PaddingBottom = 1;
            this.elementStyle8.PaddingLeft = 1;
            this.elementStyle8.PaddingRight = 1;
            this.elementStyle8.PaddingTop = 1;
            this.elementStyle8.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(84)))), ((int)(((byte)(115)))));
            // 
            // elementStyle2
            // 
            this.elementStyle2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
            this.elementStyle2.BackColorGradientAngle = 90;
            this.elementStyle2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
            this.elementStyle2.BorderBottomWidth = 1;
            this.elementStyle2.BorderColor = System.Drawing.Color.DarkGray;
            this.elementStyle2.BorderLeftWidth = 1;
            this.elementStyle2.BorderRightWidth = 1;
            this.elementStyle2.BorderTopWidth = 1;
            this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle2.Description = "Yellow";
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.PaddingBottom = 1;
            this.elementStyle2.PaddingLeft = 1;
            this.elementStyle2.PaddingRight = 1;
            this.elementStyle2.PaddingTop = 1;
            this.elementStyle2.TextColor = System.Drawing.Color.Black;
            // 
            // barLayers
            // 
            this.barLayers.CanAutoHide = false;
            this.barLayers.CanDockBottom = false;
            this.barLayers.CanDockLeft = false;
            this.barLayers.CanDockRight = false;
            this.barLayers.CanDockTab = false;
            this.barLayers.CanDockTop = false;
            this.barLayers.CanMove = false;
            this.barLayers.CanReorderTabs = false;
            this.barLayers.CanUndock = false;
            this.barLayers.ColorScheme.PredefinedColorScheme = DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
            this.barLayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.barLayers.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.barLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.barLayers.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btLayerAdd,
            this.btLayerRemove,
            this.btLayerRename,
            this.btLayerMoveUp,
            this.btLayerMoveDown,
            this.btLayerSaveList,
            this.btLayerSaveListAs,
            this.btLayerOpenList,
            this.btLayersToggle});
            this.barLayers.Location = new System.Drawing.Point(0, 0);
            this.barLayers.Margin = new System.Windows.Forms.Padding(5);
            this.barLayers.Name = "barLayers";
            this.barLayers.RoundCorners = false;
            this.barLayers.SaveLayoutChanges = false;
            this.barLayers.Size = new System.Drawing.Size(279, 25);
            this.barLayers.Stretch = true;
            this.barLayers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barLayers.TabIndex = 28;
            this.barLayers.TabStop = false;
            this.barLayers.Text = "barLayers";
            // 
            // btLayerAdd
            // 
            this.btLayerAdd.Image = ((System.Drawing.Image)(resources.GetObject("btLayerAdd.Image")));
            this.btLayerAdd.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerAdd.Name = "btLayerAdd";
            this.btLayerAdd.Click += new System.EventHandler(this.btLayerAdd_Click);
            // 
            // btLayerRemove
            // 
            this.btLayerRemove.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
            this.btLayerRemove.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerRemove.Name = "btLayerRemove";
            this.btLayerRemove.Click += new System.EventHandler(this.btLayerRemove_Click);
            // 
            // btLayerRename
            // 
            this.btLayerRename.Image = ((System.Drawing.Image)(resources.GetObject("btLayerRename.Image")));
            this.btLayerRename.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerRename.Name = "btLayerRename";
            this.btLayerRename.Click += new System.EventHandler(this.btLayerEdit_Click);
            // 
            // btLayerMoveUp
            // 
            this.btLayerMoveUp.BeginGroup = true;
            this.btLayerMoveUp.Image = global::QuoterPlan.Properties.Resources.move_up_16x16;
            this.btLayerMoveUp.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerMoveUp.Name = "btLayerMoveUp";
            this.btLayerMoveUp.Click += new System.EventHandler(this.btLayerMoveUp_Click);
            // 
            // btLayerMoveDown
            // 
            this.btLayerMoveDown.Image = global::QuoterPlan.Properties.Resources.move_down_16x16;
            this.btLayerMoveDown.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerMoveDown.Name = "btLayerMoveDown";
            this.btLayerMoveDown.Click += new System.EventHandler(this.btLayerMoveDown_Click);
            // 
            // btLayerSaveList
            // 
            this.btLayerSaveList.BeginGroup = true;
            this.btLayerSaveList.Image = global::QuoterPlan.Properties.Resources.document_save_16x16;
            this.btLayerSaveList.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerSaveList.Name = "btLayerSaveList";
            this.btLayerSaveList.Click += new System.EventHandler(this.btLayerSaveList_Click);
            // 
            // btLayerSaveListAs
            // 
            this.btLayerSaveListAs.Image = global::QuoterPlan.Properties.Resources.document_save_as_16x16;
            this.btLayerSaveListAs.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerSaveListAs.Name = "btLayerSaveListAs";
            this.btLayerSaveListAs.Click += new System.EventHandler(this.btLayerSaveListAs_Click);
            // 
            // btLayerOpenList
            // 
            this.btLayerOpenList.BeginGroup = true;
            this.btLayerOpenList.Image = global::QuoterPlan.Properties.Resources.document_open_16x16;
            this.btLayerOpenList.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayerOpenList.Name = "btLayerOpenList";
            this.btLayerOpenList.Click += new System.EventHandler(this.btLayerOpenList_Click);
            // 
            // btLayersToggle
            // 
            this.btLayersToggle.AutoExpandOnClick = true;
            this.btLayersToggle.BeginGroup = true;
            this.btLayersToggle.Image = global::QuoterPlan.Properties.Resources.checked_list_16x16;
            this.btLayersToggle.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btLayersToggle.ImagePaddingHorizontal = 4;
            this.btLayersToggle.ImagePaddingVertical = 2;
            this.btLayersToggle.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btLayersToggle.Name = "btLayersToggle";
            this.btLayersToggle.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btLayersMakeVisible,
            this.btLayersMakeInvisible});
            // 
            // btLayersMakeVisible
            // 
            this.btLayersMakeVisible.Name = "btLayersMakeVisible";
            this.btLayersMakeVisible.Text = "Make All Visible";
            this.btLayersMakeVisible.Click += new System.EventHandler(this.btLayersMakeVisible_Click);
            // 
            // btLayersMakeInvisible
            // 
            this.btLayersMakeInvisible.Name = "btLayersMakeInvisible";
            this.btLayersMakeInvisible.Text = "Make All Invisible";
            this.btLayersMakeInvisible.Click += new System.EventHandler(this.btLayersMakeInvisible_Click);
            // 
            // barDisplayResults
            // 
            this.barDisplayResults.CanAutoHide = false;
            this.barDisplayResults.CanDockLeft = false;
            this.barDisplayResults.CanDockRight = false;
            this.barDisplayResults.CanDockTab = false;
            this.barDisplayResults.CanDockTop = false;
            this.barDisplayResults.CanMove = false;
            this.barDisplayResults.CanReorderTabs = false;
            this.barDisplayResults.CanUndock = false;
            this.barDisplayResults.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDisplayResults.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.barDisplayResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.barDisplayResults.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblDisplayResults,
            this.btDisplayResultsForThisPlan,
            this.lblDisplayResultsPadding,
            this.btDisplayResultsForAllPlans});
            this.barDisplayResults.Location = new System.Drawing.Point(0, 123);
            this.barDisplayResults.Margin = new System.Windows.Forms.Padding(5);
            this.barDisplayResults.Name = "barDisplayResults";
            this.barDisplayResults.PaddingBottom = 4;
            this.barDisplayResults.PaddingLeft = 0;
            this.barDisplayResults.PaddingTop = 0;
            this.barDisplayResults.RoundCorners = false;
            this.barDisplayResults.SaveLayoutChanges = false;
            this.barDisplayResults.Size = new System.Drawing.Size(279, 27);
            this.barDisplayResults.Stretch = true;
            this.barDisplayResults.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barDisplayResults.TabIndex = 29;
            this.barDisplayResults.TabStop = false;
            this.barDisplayResults.Text = "barDisplayResults";
            // 
            // lblDisplayResults
            // 
            this.lblDisplayResults.ForeColor = System.Drawing.Color.Black;
            this.lblDisplayResults.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.lblDisplayResults.Name = "lblDisplayResults";
            this.lblDisplayResults.PaddingRight = 3;
            this.lblDisplayResults.Text = "Display results:";
            // 
            // btDisplayResultsForThisPlan
            // 
            this.btDisplayResultsForThisPlan.AutoCheckOnClick = true;
            this.btDisplayResultsForThisPlan.CanCustomize = false;
            this.btDisplayResultsForThisPlan.Checked = true;
            this.btDisplayResultsForThisPlan.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btDisplayResultsForThisPlan.Name = "btDisplayResultsForThisPlan";
            this.btDisplayResultsForThisPlan.OptionGroup = "DisplayResults";
            this.btDisplayResultsForThisPlan.Text = "For this Plan";
            // 
            // lblDisplayResultsPadding
            // 
            this.lblDisplayResultsPadding.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.lblDisplayResultsPadding.Name = "lblDisplayResultsPadding";
            this.lblDisplayResultsPadding.Width = 1;
            // 
            // btDisplayResultsForAllPlans
            // 
            this.btDisplayResultsForAllPlans.AutoCheckOnClick = true;
            this.btDisplayResultsForAllPlans.CanCustomize = false;
            this.btDisplayResultsForAllPlans.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btDisplayResultsForAllPlans.Name = "btDisplayResultsForAllPlans";
            this.btDisplayResultsForAllPlans.OptionGroup = "DisplayResults";
            this.btDisplayResultsForAllPlans.Text = "For All Plans";
            // 
            // tabProperties
            // 
            this.tabProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.tabProperties.CanReorderTabs = false;
            this.tabProperties.ColorScheme.TabBorder = System.Drawing.Color.Transparent;
            this.tabProperties.ColorScheme.TabItemBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(230)))), ((int)(((byte)(249))))), 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(220)))), ((int)(((byte)(248))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(208)))), ((int)(((byte)(245))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(229)))), ((int)(((byte)(247))))), 1F)});
            this.tabProperties.ColorScheme.TabItemHotBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(235))))), 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(236)))), ((int)(((byte)(168))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(89))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(141))))), 1F)});
            this.tabProperties.ColorScheme.TabItemSelectedBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.White, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254))))), 1F)});
            this.tabProperties.Controls.Add(this.tabControlPanel1);
            this.tabProperties.Controls.Add(this.tabControlPanel2);
            this.tabProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabProperties.ForeColor = System.Drawing.Color.Black;
            this.tabProperties.Location = new System.Drawing.Point(0, 26);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabProperties.SelectedTabIndex = 0;
            this.tabProperties.Size = new System.Drawing.Size(279, 188);
            this.tabProperties.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005Document;
            this.tabProperties.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Bottom;
            this.tabProperties.TabIndex = 27;
            this.tabProperties.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabProperties.Tabs.Add(this.tabItem1);
            this.tabProperties.Tabs.Add(this.tabItem2);
            this.tabProperties.Text = "tabProperties";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.tabControlPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tabControlPanel1.Controls.Add(this.superTabProperties);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(279, 162);
            this.tabControlPanel1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.tabControlPanel1.Style.BorderWidth = 0;
            this.tabControlPanel1.Style.GradientAngle = -90;
            this.tabControlPanel1.StyleMouseOver.BorderWidth = 0;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            this.tabControlPanel1.UseCustomStyle = true;
            this.tabControlPanel1.Resize += new System.EventHandler(this.tabControlPanel1_Resize);
            // 
            // superTabProperties
            // 
            this.superTabProperties.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabProperties.ControlBox.CloseBox.Name = global::QuoterPlan.Properties.Resources.Coller;
            // 
            // 
            // 
            this.superTabProperties.ControlBox.MenuBox.Name = global::QuoterPlan.Properties.Resources.Coller;
            this.superTabProperties.ControlBox.Name = global::QuoterPlan.Properties.Resources.Coller;
            this.superTabProperties.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabProperties.ControlBox.MenuBox,
            this.superTabProperties.ControlBox.CloseBox});
            this.superTabProperties.Controls.Add(this.superTabControlPanel1);
            this.superTabProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabProperties.ForeColor = System.Drawing.Color.Black;
            this.superTabProperties.Location = new System.Drawing.Point(1, 1);
            this.superTabProperties.Margin = new System.Windows.Forms.Padding(0);
            this.superTabProperties.Name = "superTabProperties";
            this.superTabProperties.ReorderTabsEnabled = false;
            this.superTabProperties.SelectedTabIndex = 0;
            this.superTabProperties.Size = new System.Drawing.Size(277, 160);
            this.superTabProperties.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Bottom;
            this.superTabProperties.TabHorizontalSpacing = 3;
            this.superTabProperties.TabIndex = 38;
            this.superTabProperties.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
            this.superTabProperties.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
            this.superTabProperties.TabsVisible = false;
            this.superTabProperties.TabVerticalSpacing = 5;
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.gridObjectProperties);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(277, 160);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // gridObjectProperties
            // 
            this.gridObjectProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridObjectProperties.GridLinesColor = System.Drawing.Color.WhiteSmoke;
            this.gridObjectProperties.Location = new System.Drawing.Point(0, 0);
            this.gridObjectProperties.Margin = new System.Windows.Forms.Padding(0);
            this.gridObjectProperties.Name = "gridObjectProperties";
            this.gridObjectProperties.Size = new System.Drawing.Size(277, 160);
            this.gridObjectProperties.TabIndex = 26;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "Group";
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = global::QuoterPlan.Properties.Resources.Propriétés;
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.tabControlPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tabControlPanel2.Controls.Add(this.extensionsManager);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 0);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(279, 162);
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(250)))), ((int)(((byte)(247)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Top)));
            this.tabControlPanel2.Style.GradientAngle = -90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItem2;
            this.tabControlPanel2.UseCustomStyle = true;
            // 
            // extensionsManager
            // 
            this.extensionsManager.BackColor = System.Drawing.SystemColors.Window;
            this.extensionsManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extensionsManager.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.extensionsManager.HelpUtilities = null;
            this.extensionsManager.Location = new System.Drawing.Point(1, 1);
            this.extensionsManager.Margin = new System.Windows.Forms.Padding(4);
            this.extensionsManager.Name = "extensionsManager";
            this.extensionsManager.Size = new System.Drawing.Size(277, 160);
            this.extensionsManager.TabIndex = 0;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "Extensions";
            // 
            // cbObjects
            // 
            this.cbObjects.DisableInternalDrawing = true;
            this.cbObjects.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbObjects.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbObjects.DropDownHeight = 220;
            this.cbObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbObjects.FormattingEnabled = true;
            this.cbObjects.IntegralHeight = false;
            this.cbObjects.ItemHeight = 20;
            this.cbObjects.Location = new System.Drawing.Point(0, 0);
            this.cbObjects.Margin = new System.Windows.Forms.Padding(0);
            this.cbObjects.Name = "cbObjects";
            this.cbObjects.Size = new System.Drawing.Size(279, 26);
            this.cbObjects.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbObjects.TabIndex = 23;
            this.cbObjects.WatermarkEnabled = false;
            // 
            // panControl
            // 
            this.panControl.BackColor = System.Drawing.SystemColors.Window;
            this.panControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panControl.Location = new System.Drawing.Point(0, 0);
            this.panControl.Margin = new System.Windows.Forms.Padding(4);
            this.panControl.Name = "panControl";
            this.panControl.PanFromScrolling = false;
            this.panControl.PanRectangle = new System.Drawing.Rectangle(0, 0, 1, 1);
            this.panControl.Size = new System.Drawing.Size(279, 150);
            this.panControl.TabIndex = 20;
            this.panControl.Panning += new QuoterPlanControls.PanningEventHandler(this.panControl_Panning);
            this.panControl.Paint += new System.Windows.Forms.PaintEventHandler(this.panControl_Paint);
            this.panControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panControl_MouseMove);
            this.panControl.Resize += new System.EventHandler(this.panControl_Resize);
            // 
            // lstRecentPlans
            // 
            this.lstRecentPlans.AllowDrop = true;
            this.lstRecentPlans.AllowUserToResizeColumns = false;
            this.lstRecentPlans.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.lstRecentPlans.BackgroundStyle.BorderColor = System.Drawing.Color.Transparent;
            this.lstRecentPlans.BackgroundStyle.BorderColor2 = System.Drawing.Color.Transparent;
            this.lstRecentPlans.BackgroundStyle.Class = "TreeBorderKey";
            this.lstRecentPlans.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstRecentPlans.BackgroundStyle.PaddingLeft = 1;
            this.lstRecentPlans.BackgroundStyle.PaddingRight = 10;
            this.lstRecentPlans.CellEdit = true;
            this.lstRecentPlans.Columns.Add(this.columnHeader1);
            this.lstRecentPlans.Columns.Add(this.columnHeader2);
            this.lstRecentPlans.Columns.Add(this.columnHeader5);
            this.lstRecentPlans.ColumnsVisible = false;
            this.lstRecentPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRecentPlans.DragDropEnabled = false;
            this.lstRecentPlans.DragDropNodeCopyEnabled = false;
            this.lstRecentPlans.ExpandBorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.lstRecentPlans.ExpandWidth = 0;
            this.lstRecentPlans.GridColumnLineResizeEnabled = true;
            this.lstRecentPlans.GridColumnLines = false;
            this.lstRecentPlans.HotTracking = true;
            this.lstRecentPlans.HScrollBarVisible = false;
            this.lstRecentPlans.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.lstRecentPlans.Location = new System.Drawing.Point(0, 25);
            this.lstRecentPlans.Name = "lstRecentPlans";
            this.lstRecentPlans.NodeHorizontalSpacing = 0;
            this.lstRecentPlans.NodesConnector = this.nodeConnector3;
            this.lstRecentPlans.PathSeparator = ";";
            this.lstRecentPlans.Size = new System.Drawing.Size(173, 212);
            this.lstRecentPlans.Styles.Add(this.elementStyle3);
            this.lstRecentPlans.Styles.Add(this.elementStyle4);
            this.lstRecentPlans.TabIndex = 32;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Editable = false;
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Column";
            this.columnHeader1.Width.Absolute = 22;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.StretchToFill = true;
            this.columnHeader2.Text = "Column";
            this.columnHeader2.Width.AutoSize = true;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Name = "columnHeader5";
            this.columnHeader5.Text = "Column";
            this.columnHeader5.Width.Absolute = 3;
            // 
            // nodeConnector3
            // 
            this.nodeConnector3.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle3
            // 
            this.elementStyle3.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2;
            this.elementStyle3.BackColorGradientAngle = 90;
            this.elementStyle3.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
            this.elementStyle3.BorderBottomWidth = 1;
            this.elementStyle3.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionText;
            this.elementStyle3.BorderLeftWidth = 1;
            this.elementStyle3.BorderRightWidth = 1;
            this.elementStyle3.BorderTopWidth = 1;
            this.elementStyle3.CornerDiameter = 4;
            this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle3.Description = "Blue";
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.PaddingBottom = 4;
            this.elementStyle3.PaddingLeft = 4;
            this.elementStyle3.PaddingRight = 4;
            this.elementStyle3.PaddingTop = 4;
            this.elementStyle3.TextColor = System.Drawing.Color.Black;
            // 
            // elementStyle4
            // 
            this.elementStyle4.BackColorGradientAngle = 90;
            this.elementStyle4.BorderBottomWidth = 1;
            this.elementStyle4.BorderColor = System.Drawing.Color.DarkGray;
            this.elementStyle4.BorderLeftWidth = 1;
            this.elementStyle4.BorderRightWidth = 1;
            this.elementStyle4.BorderTopWidth = 1;
            this.elementStyle4.CornerDiameter = 4;
            this.elementStyle4.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle4.Description = "Blue";
            this.elementStyle4.Name = "elementStyle4";
            this.elementStyle4.PaddingBottom = 1;
            this.elementStyle4.PaddingLeft = 1;
            this.elementStyle4.PaddingRight = 1;
            this.elementStyle4.PaddingTop = 1;
            this.elementStyle4.TextColor = System.Drawing.Color.Black;
            // 
            // barRecentPlans
            // 
            this.barRecentPlans.CanAutoHide = false;
            this.barRecentPlans.CanDockBottom = false;
            this.barRecentPlans.CanDockLeft = false;
            this.barRecentPlans.CanDockRight = false;
            this.barRecentPlans.CanDockTab = false;
            this.barRecentPlans.CanDockTop = false;
            this.barRecentPlans.CanMove = false;
            this.barRecentPlans.CanReorderTabs = false;
            this.barRecentPlans.CanUndock = false;
            this.barRecentPlans.ColorScheme.PredefinedColorScheme = DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
            this.barRecentPlans.Dock = System.Windows.Forms.DockStyle.Top;
            this.barRecentPlans.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.barRecentPlans.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.barRecentPlans.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btPlanRename});
            this.barRecentPlans.Location = new System.Drawing.Point(0, 0);
            this.barRecentPlans.Margin = new System.Windows.Forms.Padding(5);
            this.barRecentPlans.Name = "barRecentPlans";
            this.barRecentPlans.RoundCorners = false;
            this.barRecentPlans.SaveLayoutChanges = false;
            this.barRecentPlans.Size = new System.Drawing.Size(173, 25);
            this.barRecentPlans.Stretch = true;
            this.barRecentPlans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barRecentPlans.TabIndex = 38;
            this.barRecentPlans.TabStop = false;
            this.barRecentPlans.Text = "bar1";
            // 
            // btPlanRename
            // 
            this.btPlanRename.Image = ((System.Drawing.Image)(resources.GetObject("btPlanRename.Image")));
            this.btPlanRename.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btPlanRename.Name = "btPlanRename";
            this.btPlanRename.Click += new System.EventHandler(this.btPlanRename_Click);
            // 
            // treeObjects
            // 
            this.treeObjects.AllowDrop = true;
            this.treeObjects.AllowUserToResizeColumns = false;
            this.treeObjects.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeObjects.BackgroundStyle.BorderColor = System.Drawing.Color.Transparent;
            this.treeObjects.BackgroundStyle.BorderColor2 = System.Drawing.Color.Transparent;
            this.treeObjects.BackgroundStyle.Class = "TreeBorderKey";
            this.treeObjects.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeObjects.CellEdit = true;
            this.treeObjects.Columns.Add(this.columnObjectIcon);
            this.treeObjects.Columns.Add(this.columnObjectName);
            this.treeObjects.Columns.Add(this.columnObjectInfo);
            this.treeObjects.Columns.Add(this.columnObjectColor);
            this.treeObjects.Columns.Add(this.columnObjectVisible);
            this.treeObjects.Columns.Add(this.columnObjectPadding);
            this.treeObjects.ColumnsVisible = false;
            this.treeObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeObjects.DragDropNodeCopyEnabled = false;
            this.treeObjects.DropAsChildOffset = 0;
            this.treeObjects.ExpandWidth = 12;
            this.treeObjects.ForeColor = System.Drawing.Color.Black;
            this.treeObjects.GridColumnLineResizeEnabled = true;
            this.treeObjects.GridColumnLines = false;
            this.treeObjects.GridRowLines = true;
            this.treeObjects.HotTracking = true;
            this.treeObjects.HScrollBarVisible = false;
            this.treeObjects.Indent = 8;
            this.treeObjects.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeObjects.Location = new System.Drawing.Point(0, 51);
            this.treeObjects.Name = "treeObjects";
            this.treeObjects.NodeHorizontalSpacing = 3;
            this.treeObjects.NodesConnector = this.nodeConnector1;
            this.treeObjects.NodeSpacing = 1;
            this.treeObjects.PathSeparator = ";";
            this.treeObjects.Size = new System.Drawing.Size(173, 203);
            this.treeObjects.Styles.Add(this.elementStyle7);
            this.treeObjects.TabIndex = 1;
            this.treeObjects.Text = "advTree1";
            // 
            // columnObjectIcon
            // 
            this.columnObjectIcon.Name = "columnObjectIcon";
            this.columnObjectIcon.SortingEnabled = false;
            this.columnObjectIcon.Text = "Column";
            this.columnObjectIcon.Width.Absolute = 44;
            // 
            // columnObjectName
            // 
            this.columnObjectName.MaxInputLength = 50;
            this.columnObjectName.Name = "columnObjectName";
            this.columnObjectName.SortingEnabled = false;
            this.columnObjectName.Text = "Column";
            this.columnObjectName.Width.Relative = 46;
            // 
            // columnObjectInfo
            // 
            this.columnObjectInfo.Name = "columnObjectInfo";
            this.columnObjectInfo.SortingEnabled = false;
            this.columnObjectInfo.StretchToFill = true;
            this.columnObjectInfo.Text = "Column";
            this.columnObjectInfo.Width.Relative = 31;
            // 
            // columnObjectColor
            // 
            this.columnObjectColor.Name = "columnObjectColor";
            this.columnObjectColor.SortingEnabled = false;
            this.columnObjectColor.Text = "Column";
            this.columnObjectColor.Visible = false;
            this.columnObjectColor.Width.AutoSize = true;
            // 
            // columnObjectVisible
            // 
            this.columnObjectVisible.Name = "columnObjectVisible";
            this.columnObjectVisible.SortingEnabled = false;
            this.columnObjectVisible.Text = "Column";
            this.columnObjectVisible.Width.AutoSize = true;
            // 
            // columnObjectPadding
            // 
            this.columnObjectPadding.Name = "columnObjectPadding";
            this.columnObjectPadding.SortingEnabled = false;
            this.columnObjectPadding.Text = "Column";
            this.columnObjectPadding.Width.Absolute = 1;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle7
            // 
            this.elementStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.elementStyle7.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
            this.elementStyle7.BackColorGradientAngle = 90;
            this.elementStyle7.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle7.BorderBottomWidth = 1;
            this.elementStyle7.BorderColor = System.Drawing.Color.DarkGray;
            this.elementStyle7.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle7.BorderLeftWidth = 1;
            this.elementStyle7.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle7.BorderRightWidth = 1;
            this.elementStyle7.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle7.BorderTopWidth = 1;
            this.elementStyle7.CornerDiameter = 4;
            this.elementStyle7.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle7.Description = "Blue";
            this.elementStyle7.Name = "elementStyle7";
            this.elementStyle7.PaddingBottom = 1;
            this.elementStyle7.PaddingLeft = 1;
            this.elementStyle7.PaddingRight = 1;
            this.elementStyle7.PaddingTop = 1;
            this.elementStyle7.TextColor = System.Drawing.Color.Black;
            // 
            // panelPlanName
            // 
            this.panelPlanName.Controls.Add(this.txtPlanName);
            this.panelPlanName.Controls.Add(this.cbPlans);
            this.panelPlanName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPlanName.Location = new System.Drawing.Point(0, 25);
            this.panelPlanName.Name = "panelPlanName";
            this.panelPlanName.Size = new System.Drawing.Size(173, 26);
            this.panelPlanName.TabIndex = 40;
            // 
            // txtPlanName
            // 
            this.txtPlanName.AssociatedLabel = null;
            this.txtPlanName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlanName.Location = new System.Drawing.Point(0, 0);
            this.txtPlanName.MaxLength = 0;
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.Size = new System.Drawing.Size(173, 20);
            this.txtPlanName.TabIndex = 36;
            this.txtPlanName.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.txtPlanName.Visible = false;
            // 
            // cbPlans
            // 
            this.cbPlans.DisableInternalDrawing = true;
            this.cbPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPlans.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPlans.DropDownHeight = 220;
            this.cbPlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPlans.FormattingEnabled = true;
            this.cbPlans.IntegralHeight = false;
            this.cbPlans.ItemHeight = 20;
            this.cbPlans.Location = new System.Drawing.Point(0, 0);
            this.cbPlans.Margin = new System.Windows.Forms.Padding(0);
            this.cbPlans.Name = "cbPlans";
            this.cbPlans.Size = new System.Drawing.Size(173, 26);
            this.cbPlans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPlans.TabIndex = 35;
            this.cbPlans.WatermarkEnabled = false;
            // 
            // barGroups
            // 
            this.barGroups.CanAutoHide = false;
            this.barGroups.CanDockBottom = false;
            this.barGroups.CanDockLeft = false;
            this.barGroups.CanDockRight = false;
            this.barGroups.CanDockTab = false;
            this.barGroups.CanDockTop = false;
            this.barGroups.CanMove = false;
            this.barGroups.CanReorderTabs = false;
            this.barGroups.CanUndock = false;
            this.barGroups.ColorScheme.PredefinedColorScheme = DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
            this.barGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this.barGroups.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.barGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.barGroups.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btGroupLocate,
            this.btZoomToObject,
            this.btGroupSelect,
            this.btGroupRemove,
            this.btGroupRename,
            this.btRenamePlan,
            this.btGroupsToggle});
            this.barGroups.Location = new System.Drawing.Point(0, 0);
            this.barGroups.Margin = new System.Windows.Forms.Padding(5);
            this.barGroups.Name = "barGroups";
            this.barGroups.RoundCorners = false;
            this.barGroups.SaveLayoutChanges = false;
            this.barGroups.Size = new System.Drawing.Size(173, 25);
            this.barGroups.Stretch = true;
            this.barGroups.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barGroups.TabIndex = 37;
            this.barGroups.TabStop = false;
            this.barGroups.Text = "bar1";
            // 
            // btGroupLocate
            // 
            this.btGroupLocate.BeginGroup = true;
            this.btGroupLocate.Image = global::QuoterPlan.Properties.Resources.locate_16x16;
            this.btGroupLocate.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btGroupLocate.Name = "btGroupLocate";
            this.btGroupLocate.Click += new System.EventHandler(this.btGroupLocate_Click);
            // 
            // btZoomToObject
            // 
            this.btZoomToObject.Image = global::QuoterPlan.Properties.Resources.zoom_16x16;
            this.btZoomToObject.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btZoomToObject.Name = "btZoomToObject";
            this.btZoomToObject.Click += new System.EventHandler(this.btGroupZoomToObject_Click);
            // 
            // btGroupSelect
            // 
            this.btGroupSelect.Image = global::QuoterPlan.Properties.Resources.selection_16x16_alt;
            this.btGroupSelect.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btGroupSelect.Name = "btGroupSelect";
            this.btGroupSelect.Click += new System.EventHandler(this.btGroupSelect_Click);
            // 
            // btGroupRemove
            // 
            this.btGroupRemove.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
            this.btGroupRemove.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btGroupRemove.Name = "btGroupRemove";
            this.btGroupRemove.Click += new System.EventHandler(this.btGroupRemove_Click);
            // 
            // btGroupRename
            // 
            this.btGroupRename.Image = ((System.Drawing.Image)(resources.GetObject("btGroupRename.Image")));
            this.btGroupRename.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btGroupRename.Name = "btGroupRename";
            this.btGroupRename.Click += new System.EventHandler(this.btGroupRename_Click);
            // 
            // btRenamePlan
            // 
            this.btRenamePlan.BeginGroup = true;
            this.btRenamePlan.Image = global::QuoterPlan.Properties.Resources.textfield_rename_16x16;
            this.btRenamePlan.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btRenamePlan.Name = "btRenamePlan";
            this.btRenamePlan.Click += new System.EventHandler(this.btRenamePlan_Click);
            // 
            // btGroupsToggle
            // 
            this.btGroupsToggle.AutoExpandOnClick = true;
            this.btGroupsToggle.BeginGroup = true;
            this.btGroupsToggle.Image = global::QuoterPlan.Properties.Resources.checked_list_16x16;
            this.btGroupsToggle.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btGroupsToggle.ImagePaddingHorizontal = 4;
            this.btGroupsToggle.ImagePaddingVertical = 2;
            this.btGroupsToggle.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btGroupsToggle.Name = "btGroupsToggle";
            this.btGroupsToggle.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btGroupsMakeVisible,
            this.btGroupsMakeInvisible});
            // 
            // btGroupsMakeVisible
            // 
            this.btGroupsMakeVisible.Name = "btGroupsMakeVisible";
            this.btGroupsMakeVisible.Text = "Make All Visible";
            this.btGroupsMakeVisible.Click += new System.EventHandler(this.btGroupsMakeVisible_Click);
            // 
            // btGroupsMakeInvisible
            // 
            this.btGroupsMakeInvisible.Name = "btGroupsMakeInvisible";
            this.btGroupsMakeInvisible.Text = "Make All Invisible";
            this.btGroupsMakeInvisible.Click += new System.EventHandler(this.btGroupsMakeInvisible_Click);
            // 
            // checkBoxItem1
            // 
            this.checkBoxItem1.Name = "checkBoxItem1";
            this.checkBoxItem1.TextVisible = false;
            // 
            // sliderItem1
            // 
            this.sliderItem1.EnableMarkup = false;
            this.sliderItem1.LabelVisible = false;
            this.sliderItem1.Name = "sliderItem1";
            this.sliderItem1.TrackMarker = false;
            this.sliderItem1.Value = 0;
            // 
            // checkBoxItem2
            // 
            this.checkBoxItem2.Name = "checkBoxItem2";
            this.checkBoxItem2.Text = "checkBoxItem2";
            this.checkBoxItem2.TextVisible = false;
            // 
            // sliderItem2
            // 
            this.sliderItem2.LabelVisible = false;
            this.sliderItem2.Name = "sliderItem2";
            this.sliderItem2.Text = "sliderItem2";
            this.sliderItem2.TrackMarker = false;
            this.sliderItem2.Value = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 10D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // itemContainer5
            // 
            // 
            // 
            // 
            this.itemContainer5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer5.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer5.Name = "itemContainer5";
            // 
            // 
            // 
            this.itemContainer5.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // galleryGroupArea
            // 
            this.galleryGroupArea.Name = "galleryGroupArea";
            this.galleryGroupArea.Text = global::QuoterPlan.Properties.Resources.Surface;
            // 
            // galleryGroupPerimeter
            // 
            this.galleryGroupPerimeter.Name = "galleryGroupPerimeter";
            this.galleryGroupPerimeter.Text = global::QuoterPlan.Properties.Resources.Périmètre;
            // 
            // galleryGroupCounter
            // 
            this.galleryGroupCounter.Name = "galleryGroupCounter";
            this.galleryGroupCounter.Text = global::QuoterPlan.Properties.Resources.Compteur;
            // 
            // tabItem3
            // 
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = global::QuoterPlan.Properties.Resources.Coller;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            // 
            // flowPlans
            // 
            this.flowPlans.AllowDrop = true;
            this.flowPlans.AutoScroll = true;
            this.flowPlans.BackColor = System.Drawing.SystemColors.Window;
            this.flowPlans.Location = new System.Drawing.Point(793, 286);
            this.flowPlans.Name = "flowPlans";
            this.flowPlans.Size = new System.Drawing.Size(250, 107);
            this.flowPlans.TabIndex = 29;
            this.flowPlans.Visible = false;
            // 
            // sliderItem4
            // 
            this.sliderItem4.LabelVisible = false;
            this.sliderItem4.Maximum = 300;
            this.sliderItem4.Minimum = 10;
            this.sliderItem4.Name = "sliderItem4";
            this.sliderItem4.Text = "sliderItem1";
            this.sliderItem4.TrackMarker = false;
            this.sliderItem4.Value = 0;
            this.sliderItem4.Width = 200;
            // 
            // itemContainerBrightness
            // 
            // 
            // 
            // 
            this.itemContainerBrightness.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerBrightness.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerBrightness.Name = "itemContainerBrightness";
            this.itemContainerBrightness.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblBrightness,
            this.sliderBrightness});
            // 
            // 
            // 
            this.itemContainerBrightness.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblBrightness
            // 
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.PaddingTop = 12;
            this.lblBrightness.Text = "Brightness";
            this.lblBrightness.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // sliderBrightness
            // 
            this.sliderBrightness.LabelPosition = DevComponents.DotNetBar.eSliderLabelPosition.Bottom;
            this.sliderBrightness.Maximum = 255;
            this.sliderBrightness.Minimum = -255;
            this.sliderBrightness.Name = "sliderBrightness";
            this.sliderBrightness.Text = "0";
            this.sliderBrightness.TextColor = System.Drawing.Color.Black;
            this.sliderBrightness.Value = 0;
            this.sliderBrightness.ValueChanged += new System.EventHandler(this.sliderBrightness_ValueChanged);
            // 
            // lblBrightnessContrastPadding1
            // 
            this.lblBrightnessContrastPadding1.Name = "lblBrightnessContrastPadding1";
            this.lblBrightnessContrastPadding1.Width = 5;
            // 
            // itemContainerContrast
            // 
            // 
            // 
            // 
            this.itemContainerContrast.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerContrast.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerContrast.Name = "itemContainerContrast";
            this.itemContainerContrast.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblContrast,
            this.sliderContrast});
            // 
            // 
            // 
            this.itemContainerContrast.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblContrast
            // 
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.PaddingTop = 12;
            this.lblContrast.Text = "Contrast";
            this.lblContrast.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // sliderContrast
            // 
            this.sliderContrast.LabelPosition = DevComponents.DotNetBar.eSliderLabelPosition.Bottom;
            this.sliderContrast.Minimum = -100;
            this.sliderContrast.Name = "sliderContrast";
            this.sliderContrast.Text = "0";
            this.sliderContrast.TextColor = System.Drawing.Color.Black;
            this.sliderContrast.Value = 0;
            this.sliderContrast.ValueChanged += new System.EventHandler(this.sliderContrast_ValueChanged);
            // 
            // ribbonBarBrightnessContrast
            // 
            this.ribbonBarBrightnessContrast.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBarBrightnessContrast.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarBrightnessContrast.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarBrightnessContrast.ContainerControlProcessDialogKey = true;
            this.ribbonBarBrightnessContrast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonBarBrightnessContrast.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.ribbonBarBrightnessContrast.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainerBrightness,
            this.lblBrightnessContrastPadding1,
            this.itemContainerContrast,
            this.lblBrightnessContrastPadding2,
            this.lblBrightnessContrastSeparator,
            this.lblBrightnessContrastPadding3,
            this.btBrightnessContrastApply,
            this.btBrightnessContrastCancel,
            this.btBrightnessContrastRestore});
            this.ribbonBarBrightnessContrast.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarBrightnessContrast.Location = new System.Drawing.Point(1, 1);
            this.ribbonBarBrightnessContrast.Name = "ribbonBarBrightnessContrast";
            this.ribbonBarBrightnessContrast.Size = new System.Drawing.Size(681, 95);
            this.ribbonBarBrightnessContrast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarBrightnessContrast.TabIndex = 33;
            this.ribbonBarBrightnessContrast.Text = "Brightness/Contrast";
            // 
            // 
            // 
            this.ribbonBarBrightnessContrast.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarBrightnessContrast.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblBrightnessContrastPadding2
            // 
            this.lblBrightnessContrastPadding2.Name = "lblBrightnessContrastPadding2";
            this.lblBrightnessContrastPadding2.Width = 15;
            // 
            // lblBrightnessContrastSeparator
            // 
            this.lblBrightnessContrastSeparator.BeginGroup = true;
            this.lblBrightnessContrastSeparator.Name = "lblBrightnessContrastSeparator";
            this.lblBrightnessContrastSeparator.Width = 5;
            // 
            // lblBrightnessContrastPadding3
            // 
            this.lblBrightnessContrastPadding3.Name = "lblBrightnessContrastPadding3";
            this.lblBrightnessContrastPadding3.Width = 5;
            // 
            // btBrightnessContrastApply
            // 
            this.btBrightnessContrastApply.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrightnessContrastApply.Image = ((System.Drawing.Image)(resources.GetObject("btBrightnessContrastApply.Image")));
            this.btBrightnessContrastApply.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btBrightnessContrastApply.Name = "btBrightnessContrastApply";
            this.btBrightnessContrastApply.Text = "Apply Changes";
            this.btBrightnessContrastApply.Click += new System.EventHandler(this.btBrightnessContrastApply_Click);
            // 
            // btBrightnessContrastCancel
            // 
            this.btBrightnessContrastCancel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrightnessContrastCancel.Image = ((System.Drawing.Image)(resources.GetObject("btBrightnessContrastCancel.Image")));
            this.btBrightnessContrastCancel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btBrightnessContrastCancel.Name = "btBrightnessContrastCancel";
            this.btBrightnessContrastCancel.Text = "Cancel Changes";
            this.btBrightnessContrastCancel.Click += new System.EventHandler(this.btBrightnessContrastCancel_Click);
            // 
            // btBrightnessContrastRestore
            // 
            this.btBrightnessContrastRestore.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btBrightnessContrastRestore.Image = ((System.Drawing.Image)(resources.GetObject("btBrightnessContrastRestore.Image")));
            this.btBrightnessContrastRestore.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btBrightnessContrastRestore.Name = "btBrightnessContrastRestore";
            this.btBrightnessContrastRestore.Stretch = true;
            this.btBrightnessContrastRestore.Text = "Original Image";
            this.btBrightnessContrastRestore.Click += new System.EventHandler(this.btBrightnessContrastRestore_Click);
            // 
            // panelBrightnessContrast
            // 
            this.panelBrightnessContrast.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelBrightnessContrast.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelBrightnessContrast.Controls.Add(this.ribbonBarBrightnessContrast);
            this.panelBrightnessContrast.Location = new System.Drawing.Point(393, 648);
            this.panelBrightnessContrast.Name = "panelBrightnessContrast";
            this.panelBrightnessContrast.Padding = new System.Windows.Forms.Padding(1);
            this.panelBrightnessContrast.Size = new System.Drawing.Size(683, 97);
            this.panelBrightnessContrast.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelBrightnessContrast.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelBrightnessContrast.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelBrightnessContrast.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemSeparator;
            this.panelBrightnessContrast.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelBrightnessContrast.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelBrightnessContrast.Style.GradientAngle = 90;
            this.panelBrightnessContrast.TabIndex = 34;
            this.panelBrightnessContrast.Visible = false;
            // 
            // panelRotation
            // 
            this.panelRotation.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelRotation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelRotation.Controls.Add(this.ribbonBarRotation);
            this.panelRotation.Location = new System.Drawing.Point(370, 610);
            this.panelRotation.Name = "panelRotation";
            this.panelRotation.Padding = new System.Windows.Forms.Padding(1);
            this.panelRotation.Size = new System.Drawing.Size(683, 97);
            this.panelRotation.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelRotation.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRotation.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelRotation.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemSeparator;
            this.panelRotation.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelRotation.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelRotation.Style.GradientAngle = 90;
            this.panelRotation.TabIndex = 35;
            this.panelRotation.Visible = false;
            // 
            // ribbonBarRotation
            // 
            this.ribbonBarRotation.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBarRotation.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarRotation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarRotation.ContainerControlProcessDialogKey = true;
            this.ribbonBarRotation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonBarRotation.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.ribbonBarRotation.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btFlipHorizontally,
            this.btFlipVertically,
            this.btRotateLeft,
            this.btRotateRight,
            this.lblBarRotationPadding1,
            this.lblBarRotationSeparator,
            this.lblBarRotationPadding2,
            this.btRotationApply,
            this.btRotationCancel});
            this.ribbonBarRotation.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarRotation.Location = new System.Drawing.Point(1, 1);
            this.ribbonBarRotation.Name = "ribbonBarRotation";
            this.ribbonBarRotation.Size = new System.Drawing.Size(681, 95);
            this.ribbonBarRotation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarRotation.TabIndex = 33;
            this.ribbonBarRotation.Text = "Flip/Rotate";
            // 
            // 
            // 
            this.ribbonBarRotation.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarRotation.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // btFlipHorizontally
            // 
            this.btFlipHorizontally.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btFlipHorizontally.Image = ((System.Drawing.Image)(resources.GetObject("btFlipHorizontally.Image")));
            this.btFlipHorizontally.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btFlipHorizontally.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btFlipHorizontally.Name = "btFlipHorizontally";
            this.btFlipHorizontally.Text = "Flip Horizontally";
            this.btFlipHorizontally.Click += new System.EventHandler(this.btFlipHorizontally_Click);
            // 
            // btFlipVertically
            // 
            this.btFlipVertically.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btFlipVertically.Image = ((System.Drawing.Image)(resources.GetObject("btFlipVertically.Image")));
            this.btFlipVertically.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btFlipVertically.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btFlipVertically.Name = "btFlipVertically";
            this.btFlipVertically.Text = "Flip Vertically";
            this.btFlipVertically.Click += new System.EventHandler(this.btFlipVertically_Click);
            // 
            // btRotateLeft
            // 
            this.btRotateLeft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRotateLeft.Image = ((System.Drawing.Image)(resources.GetObject("btRotateLeft.Image")));
            this.btRotateLeft.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btRotateLeft.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btRotateLeft.Name = "btRotateLeft";
            this.btRotateLeft.Text = "Rotate to Left";
            this.btRotateLeft.Click += new System.EventHandler(this.btRotateLeft_Click);
            // 
            // btRotateRight
            // 
            this.btRotateRight.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRotateRight.Image = ((System.Drawing.Image)(resources.GetObject("btRotateRight.Image")));
            this.btRotateRight.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btRotateRight.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btRotateRight.Name = "btRotateRight";
            this.btRotateRight.Text = "Rotate to Right";
            this.btRotateRight.Click += new System.EventHandler(this.btRotateRight_Click);
            // 
            // lblBarRotationPadding1
            // 
            this.lblBarRotationPadding1.Name = "lblBarRotationPadding1";
            this.lblBarRotationPadding1.Width = 10;
            // 
            // lblBarRotationSeparator
            // 
            this.lblBarRotationSeparator.BeginGroup = true;
            this.lblBarRotationSeparator.Name = "lblBarRotationSeparator";
            this.lblBarRotationSeparator.Width = 5;
            // 
            // lblBarRotationPadding2
            // 
            this.lblBarRotationPadding2.Name = "lblBarRotationPadding2";
            this.lblBarRotationPadding2.Width = 5;
            // 
            // btRotationApply
            // 
            this.btRotationApply.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRotationApply.Image = ((System.Drawing.Image)(resources.GetObject("btRotationApply.Image")));
            this.btRotationApply.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btRotationApply.Name = "btRotationApply";
            this.btRotationApply.Text = "Apply Changes";
            this.btRotationApply.Click += new System.EventHandler(this.btRotationApply_Click);
            // 
            // btRotationCancel
            // 
            this.btRotationCancel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRotationCancel.Image = ((System.Drawing.Image)(resources.GetObject("btRotationCancel.Image")));
            this.btRotationCancel.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btRotationCancel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btRotationCancel.Name = "btRotationCancel";
            this.btRotationCancel.Text = "Cancel Changes";
            this.btRotationCancel.Click += new System.EventHandler(this.btRotationCancel_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(1096, 394);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(253, 80);
            this.webBrowser.TabIndex = 36;
            this.webBrowser.Visible = false;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            this.webBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowser_PreviewKeyDown);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "comboItem1";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "comboItem2";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "comboItem3";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "comboItem4";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "comboItem5";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "comboItem6";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "Elements to include in report:";
            // 
            // panelWelcome
            // 
            this.panelWelcome.BackColor = System.Drawing.Color.Transparent;
            this.panelWelcome.Controls.Add(this.panelWelcomeMenu);
            this.panelWelcome.Location = new System.Drawing.Point(615, 182);
            this.panelWelcome.Name = "panelWelcome";
            this.panelWelcome.Size = new System.Drawing.Size(463, 480);
            this.panelWelcome.TabIndex = 0;
            this.panelWelcome.Visible = false;
            this.panelWelcome.Resize += new System.EventHandler(this.panelWelcome_Resize);
            // 
            // panelWelcomeMenu
            // 
            this.panelWelcomeMenu.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelWelcomeMenu.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelWelcomeMenu.Controls.Add(this.lblRecentProjects);
            this.panelWelcomeMenu.Controls.Add(this.btNew);
            this.panelWelcomeMenu.Controls.Add(this.lstRecentProjects);
            this.panelWelcomeMenu.Controls.Add(this.btOpen);
            this.panelWelcomeMenu.Controls.Add(this.picWelcome);
            this.panelWelcomeMenu.Location = new System.Drawing.Point(30, 21);
            this.panelWelcomeMenu.Name = "panelWelcomeMenu";
            this.panelWelcomeMenu.Size = new System.Drawing.Size(408, 462);
            this.panelWelcomeMenu.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelWelcomeMenu.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelWelcomeMenu.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelWelcomeMenu.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelWelcomeMenu.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelWelcomeMenu.Style.BorderWidth = 10;
            this.panelWelcomeMenu.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelWelcomeMenu.Style.GradientAngle = 90;
            this.panelWelcomeMenu.TabIndex = 0;
            // 
            // lblRecentProjects
            // 
            this.lblRecentProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecentProjects.AutoSize = true;
            this.lblRecentProjects.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRecentProjects.Location = new System.Drawing.Point(24, 208);
            this.lblRecentProjects.Name = "lblRecentProjects";
            this.lblRecentProjects.Size = new System.Drawing.Size(119, 13);
            this.lblRecentProjects.TabIndex = 0;
            this.lblRecentProjects.Text = "Open a Recent Project:";
            this.lblRecentProjects.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // btNew
            // 
            this.btNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btNew.AntiAlias = true;
            this.btNew.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btNew.FocusCuesEnabled = false;
            this.btNew.Image = global::QuoterPlan.Properties.Resources.document_new;
            this.btNew.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btNew.Location = new System.Drawing.Point(24, 138);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(174, 64);
            this.btNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btNew.TabIndex = 2;
            this.btNew.Text = "Create a New Project";
            this.btNew.Click += new System.EventHandler(this.btProjectNew_Click);
            // 
            // lstRecentProjects
            // 
            this.lstRecentProjects.AllowDrop = true;
            this.lstRecentProjects.AllowUserToResizeColumns = false;
            this.lstRecentProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRecentProjects.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.lstRecentProjects.BackgroundStyle.CornerDiameter = 0;
            this.lstRecentProjects.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstRecentProjects.BackgroundStyle.PaddingLeft = 1;
            this.lstRecentProjects.BackgroundStyle.PaddingRight = 2;
            this.lstRecentProjects.Columns.Add(this.columnHeader3);
            this.lstRecentProjects.Columns.Add(this.columnHeader4);
            this.lstRecentProjects.ColumnsVisible = false;
            this.lstRecentProjects.DragDropEnabled = false;
            this.lstRecentProjects.DragDropNodeCopyEnabled = false;
            this.lstRecentProjects.DropAsChildOffset = 0;
            this.lstRecentProjects.ExpandBorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.lstRecentProjects.ExpandButtonSize = new System.Drawing.Size(0, 0);
            this.lstRecentProjects.ExpandWidth = 0;
            this.lstRecentProjects.GridColumnLines = false;
            this.lstRecentProjects.HotTracking = true;
            this.lstRecentProjects.HScrollBarVisible = false;
            this.lstRecentProjects.Indent = 0;
            this.lstRecentProjects.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.lstRecentProjects.Location = new System.Drawing.Point(24, 227);
            this.lstRecentProjects.Name = "lstRecentProjects";
            this.lstRecentProjects.NodeHorizontalSpacing = 0;
            this.lstRecentProjects.NodesConnector = this.nodeConnector4;
            this.lstRecentProjects.NodeSpacing = 0;
            this.lstRecentProjects.PathSeparator = ";";
            this.lstRecentProjects.Size = new System.Drawing.Size(360, 210);
            this.lstRecentProjects.Styles.Add(this.elementStyle5);
            this.lstRecentProjects.Styles.Add(this.elementStyle6);
            this.lstRecentProjects.TabIndex = 1;
            this.lstRecentProjects.Text = "lstRecentProjects";
            this.lstRecentProjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstRecentProjects_KeyDown);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.StretchToFill = true;
            this.columnHeader3.Text = "Column";
            this.columnHeader3.Width.AutoSize = true;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Name = "columnHeader4";
            this.columnHeader4.Text = "Column";
            this.columnHeader4.Width.Absolute = 150;
            // 
            // nodeConnector4
            // 
            this.nodeConnector4.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle5
            // 
            this.elementStyle5.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle5.Name = "elementStyle5";
            this.elementStyle5.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle6
            // 
            this.elementStyle6.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
            this.elementStyle6.BackColorGradientAngle = 90;
            this.elementStyle6.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
            this.elementStyle6.BorderColor = System.Drawing.Color.DarkGray;
            this.elementStyle6.BorderLeftWidth = 1;
            this.elementStyle6.BorderRightWidth = 1;
            this.elementStyle6.BorderTopWidth = 1;
            this.elementStyle6.CornerDiameter = 4;
            this.elementStyle6.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle6.Description = "Yellow";
            this.elementStyle6.Name = "elementStyle6";
            this.elementStyle6.PaddingBottom = 1;
            this.elementStyle6.PaddingLeft = 1;
            this.elementStyle6.PaddingRight = 1;
            this.elementStyle6.PaddingTop = 1;
            this.elementStyle6.TextColor = System.Drawing.Color.Black;
            // 
            // btOpen
            // 
            this.btOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpen.AntiAlias = true;
            this.btOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btOpen.FocusCuesEnabled = false;
            this.btOpen.Image = global::QuoterPlan.Properties.Resources.document_open;
            this.btOpen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btOpen.Location = new System.Drawing.Point(209, 138);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(174, 64);
            this.btOpen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btOpen.TabIndex = 3;
            this.btOpen.Text = "Open an Existing Project...";
            this.btOpen.Click += new System.EventHandler(this.btProjectOpen_Click);
            // 
            // picWelcome
            // 
            this.picWelcome.BackColor = System.Drawing.Color.White;
            this.picWelcome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWelcome.Image = ((System.Drawing.Image)(resources.GetObject("picWelcome.Image")));
            this.picWelcome.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picWelcome.Location = new System.Drawing.Point(23, 27);
            this.picWelcome.Name = "picWelcome";
            this.picWelcome.Size = new System.Drawing.Size(482, 172);
            this.picWelcome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWelcome.TabIndex = 38;
            this.picWelcome.TabStop = false;
            // 
            // superTooltip
            // 
            this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superTooltip.MinimumTooltipSize = new System.Drawing.Size(150, 50);
            // 
            // panelPlansAction
            // 
            this.panelPlansAction.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelPlansAction.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelPlansAction.Controls.Add(this.ribbonBarPlansAction);
            this.panelPlansAction.Location = new System.Drawing.Point(574, 685);
            this.panelPlansAction.Name = "panelPlansAction";
            this.panelPlansAction.Padding = new System.Windows.Forms.Padding(1);
            this.panelPlansAction.Size = new System.Drawing.Size(683, 97);
            this.panelPlansAction.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelPlansAction.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelPlansAction.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelPlansAction.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemSeparator;
            this.panelPlansAction.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelPlansAction.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelPlansAction.Style.GradientAngle = 90;
            this.panelPlansAction.TabIndex = 40;
            this.panelPlansAction.Visible = false;
            // 
            // ribbonBarPlansAction
            // 
            this.ribbonBarPlansAction.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBarPlansAction.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPlansAction.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBarPlansAction.ContainerControlProcessDialogKey = true;
            this.ribbonBarPlansAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonBarPlansAction.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            this.ribbonBarPlansAction.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.progressPlansAction,
            this.itemContainerExportType,
            this.lblBarPlansActionPadding4,
            this.btPlansActionSelectAll,
            this.btPlansActionSelectNone,
            this.lblBarPlansActionPadding1,
            this.lblPlansActionSeparator,
            this.lblBarPlansActionPadding2,
            this.btPlansActionApply,
            this.btPlansActionCancel});
            this.ribbonBarPlansAction.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPlansAction.Location = new System.Drawing.Point(1, 1);
            this.ribbonBarPlansAction.Name = "ribbonBarPlansAction";
            this.ribbonBarPlansAction.Size = new System.Drawing.Size(681, 95);
            this.ribbonBarPlansAction.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPlansAction.TabIndex = 33;
            this.ribbonBarPlansAction.Text = "Action";
            // 
            // 
            // 
            this.ribbonBarPlansAction.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBarPlansAction.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // progressPlansAction
            // 
            this.progressPlansAction.Name = "progressPlansAction";
            this.progressPlansAction.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Donut;
            this.progressPlansAction.Text = "Processing...";
            // 
            // itemContainerExportType
            // 
            // 
            // 
            // 
            this.itemContainerExportType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainerExportType.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainerExportType.Name = "itemContainerExportType";
            this.itemContainerExportType.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblBarPlansActionPadding3,
            this.checkBoxExportSingleFile,
            this.checkBoxExportMultiFiles});
            // 
            // 
            // 
            this.itemContainerExportType.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // lblBarPlansActionPadding3
            // 
            this.lblBarPlansActionPadding3.Name = "lblBarPlansActionPadding3";
            // 
            // checkBoxExportSingleFile
            // 
            this.checkBoxExportSingleFile.Category = "ExportType";
            this.checkBoxExportSingleFile.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxExportSingleFile.Checked = true;
            this.checkBoxExportSingleFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExportSingleFile.Name = "checkBoxExportSingleFile";
            this.checkBoxExportSingleFile.Text = "Export to single file";
            // 
            // checkBoxExportMultiFiles
            // 
            this.checkBoxExportMultiFiles.Category = "ExportType";
            this.checkBoxExportMultiFiles.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxExportMultiFiles.Name = "checkBoxExportMultiFiles";
            this.checkBoxExportMultiFiles.Text = "Export to multiple files";
            // 
            // lblBarPlansActionPadding4
            // 
            this.lblBarPlansActionPadding4.Name = "lblBarPlansActionPadding4";
            this.lblBarPlansActionPadding4.Width = 10;
            // 
            // btPlansActionSelectAll
            // 
            this.btPlansActionSelectAll.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlansActionSelectAll.Image = global::QuoterPlan.Properties.Resources.select_all_32x32;
            this.btPlansActionSelectAll.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlansActionSelectAll.Name = "btPlansActionSelectAll";
            this.btPlansActionSelectAll.Text = "Select All";
            this.btPlansActionSelectAll.Click += new System.EventHandler(this.btPlansActionSelectAll_Click);
            // 
            // btPlansActionSelectNone
            // 
            this.btPlansActionSelectNone.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlansActionSelectNone.Image = ((System.Drawing.Image)(resources.GetObject("btPlansActionSelectNone.Image")));
            this.btPlansActionSelectNone.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlansActionSelectNone.Name = "btPlansActionSelectNone";
            this.btPlansActionSelectNone.Text = "Select None";
            this.btPlansActionSelectNone.Click += new System.EventHandler(this.btPlansActionSelectNone_Click);
            // 
            // lblBarPlansActionPadding1
            // 
            this.lblBarPlansActionPadding1.Name = "lblBarPlansActionPadding1";
            this.lblBarPlansActionPadding1.Width = 10;
            // 
            // lblPlansActionSeparator
            // 
            this.lblPlansActionSeparator.BeginGroup = true;
            this.lblPlansActionSeparator.Name = "lblPlansActionSeparator";
            this.lblPlansActionSeparator.Width = 5;
            // 
            // lblBarPlansActionPadding2
            // 
            this.lblBarPlansActionPadding2.Name = "lblBarPlansActionPadding2";
            this.lblBarPlansActionPadding2.Width = 5;
            // 
            // btPlansActionApply
            // 
            this.btPlansActionApply.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlansActionApply.Image = global::QuoterPlan.Properties.Resources.print_apply_40x40;
            this.btPlansActionApply.ImagePaddingHorizontal = 10;
            this.btPlansActionApply.ImagePaddingVertical = 5;
            this.btPlansActionApply.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlansActionApply.Name = "btPlansActionApply";
            this.btPlansActionApply.Text = "Apply Action";
            this.btPlansActionApply.Click += new System.EventHandler(this.btPlansActionApply_Click);
            // 
            // btPlansActionCancel
            // 
            this.btPlansActionCancel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlansActionCancel.Image = ((System.Drawing.Image)(resources.GetObject("btPlansActionCancel.Image")));
            this.btPlansActionCancel.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
            this.btPlansActionCancel.ImagePaddingHorizontal = 10;
            this.btPlansActionCancel.ImagePaddingVertical = 5;
            this.btPlansActionCancel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btPlansActionCancel.Name = "btPlansActionCancel";
            this.btPlansActionCancel.Text = "Cancel";
            this.btPlansActionCancel.Click += new System.EventHandler(this.btPlansActionCancel_Click);
            // 
            // mainControl
            // 
            this.mainControl.BackColor = System.Drawing.SystemColors.Control;
            this.mainControl.Brightness = 0;
            this.mainControl.Contrast = 0;
            this.mainControl.Location = new System.Drawing.Point(1099, 308);
            this.mainControl.Margin = new System.Windows.Forms.Padding(0);
            this.mainControl.Name = "mainControl";
            this.mainControl.Origin = ((System.Drawing.PointF)(resources.GetObject("mainControl.Origin")));
            this.mainControl.ScrollbarsVisible = true;
            this.mainControl.Size = new System.Drawing.Size(252, 75);
            this.mainControl.TabIndex = 28;
            this.mainControl.UseDynamicAdjustments = false;
            this.mainControl.Visible = false;
            this.mainControl.Zoom = 100;
            this.mainControl.ZoomRestricted = false;
            this.mainControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainControl_MouseDown);
            this.mainControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainControl_MouseMove);
            this.mainControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainControl_MouseUp);
            this.mainControl.OnCacheLoaded += new QuoterPlanControls.ZoomChangeEventHandler(this.mainControl_OnCacheLoaded);
            this.mainControl.OnMouseWheel += new QuoterPlanControls.ZoomChangeEventHandler(this.mainControl_OnMouseWheel);
            this.mainControl.OnOriginChange += new QuoterPlanControls.OriginChangeEventHandler(this.mainControl_OnOriginChange);
            this.mainControl.OnPaint += new QuoterPlanControls.PaintEventHandler(this.mainControl_Paint);
            this.mainControl.OnZoomChange += new QuoterPlanControls.ZoomChangeEventHandler(this.mainControl_OnZoomChange);
            this.mainControl.Resize += new System.EventHandler(this.mainControl_Resize);
            // 
            // dotNetBarManager
            // 
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManager.BottomDockSite = this.dockSite4;
            this.dotNetBarManager.EnableFullSizeDock = false;
            this.dotNetBarManager.LeftDockSite = this.dockSite1;
            this.dotNetBarManager.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.dotNetBarManager.ParentForm = this;
            this.dotNetBarManager.RightDockSite = this.dockSite2;
            this.dotNetBarManager.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dotNetBarManager.ToolbarBottomDockSite = this.dockSite8;
            this.dotNetBarManager.ToolbarLeftDockSite = this.dockSite5;
            this.dotNetBarManager.ToolbarRightDockSite = this.dockSite6;
            this.dotNetBarManager.ToolbarTopDockSite = this.dockSite7;
            this.dotNetBarManager.TopDockSite = this.dockSite3;
            this.dotNetBarManager.UseGlobalColorScheme = true;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite4.Location = new System.Drawing.Point(5, 717);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(1729, 0);
            this.dockSite4.TabIndex = 44;
            this.dockSite4.TabStop = false;
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Controls.Add(this.containerBarLayers);
            this.dockSite1.Controls.Add(this.containerBarNavigation);
            this.dockSite1.Controls.Add(this.containerBarProperties);
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.containerBarNavigation, 285, 176))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.containerBarProperties, 285, 176))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.containerBarLayers, 285, 188)))}, DevComponents.DotNetBar.eOrientation.Vertical);
            this.dockSite1.Location = new System.Drawing.Point(5, 171);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(288, 546);
            this.dockSite1.TabIndex = 41;
            this.dockSite1.TabStop = false;
            // 
            // containerBarLayers
            // 
            this.containerBarLayers.AccessibleDescription = "DotNetBar Bar (containerBarLayers)";
            this.containerBarLayers.AccessibleName = "DotNetBar Bar";
            this.containerBarLayers.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.containerBarLayers.AutoSyncBarCaption = true;
            this.containerBarLayers.CanDockBottom = false;
            this.containerBarLayers.CanDockTab = false;
            this.containerBarLayers.CanDockTop = false;
            this.containerBarLayers.CloseSingleTab = true;
            this.containerBarLayers.Controls.Add(this.panelDockLayers);
            this.containerBarLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerBarLayers.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.containerBarLayers.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItemLayers});
            this.containerBarLayers.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.containerBarLayers.Location = new System.Drawing.Point(0, 358);
            this.containerBarLayers.Name = "containerBarLayers";
            this.containerBarLayers.Size = new System.Drawing.Size(285, 188);
            this.containerBarLayers.Stretch = true;
            this.containerBarLayers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.containerBarLayers.TabIndex = 2;
            this.containerBarLayers.TabStop = false;
            this.containerBarLayers.Text = "Layers";
            // 
            // panelDockLayers
            // 
            this.panelDockLayers.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockLayers.Controls.Add(this.lstLayers);
            this.panelDockLayers.Controls.Add(this.barLayers);
            this.panelDockLayers.Location = new System.Drawing.Point(3, 23);
            this.panelDockLayers.Name = "panelDockLayers";
            this.panelDockLayers.Size = new System.Drawing.Size(279, 162);
            this.panelDockLayers.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockLayers.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockLayers.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockLayers.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockLayers.Style.GradientAngle = 90;
            this.panelDockLayers.TabIndex = 0;
            this.panelDockLayers.Visible = true;
            // 
            // dockContainerItemLayers
            // 
            this.dockContainerItemLayers.Control = this.panelDockLayers;
            this.dockContainerItemLayers.Name = "dockContainerItemLayers";
            this.dockContainerItemLayers.Text = "Layers";
            // 
            // containerBarNavigation
            // 
            this.containerBarNavigation.AccessibleDescription = "DotNetBar Bar (containerBarNavigation)";
            this.containerBarNavigation.AccessibleName = "DotNetBar Bar";
            this.containerBarNavigation.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.containerBarNavigation.AutoSyncBarCaption = true;
            this.containerBarNavigation.CanDockBottom = false;
            this.containerBarNavigation.CanDockTab = false;
            this.containerBarNavigation.CanDockTop = false;
            this.containerBarNavigation.CloseSingleTab = true;
            this.containerBarNavigation.Controls.Add(this.panelDockPreview);
            this.containerBarNavigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerBarNavigation.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.containerBarNavigation.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItemPreview});
            this.containerBarNavigation.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.containerBarNavigation.Location = new System.Drawing.Point(0, 0);
            this.containerBarNavigation.Name = "containerBarNavigation";
            this.containerBarNavigation.Size = new System.Drawing.Size(285, 176);
            this.containerBarNavigation.Stretch = true;
            this.containerBarNavigation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.containerBarNavigation.TabIndex = 0;
            this.containerBarNavigation.TabStop = false;
            this.containerBarNavigation.Text = "Navigation";
            // 
            // panelDockPreview
            // 
            this.panelDockPreview.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockPreview.Controls.Add(this.panControl);
            this.panelDockPreview.Location = new System.Drawing.Point(3, 23);
            this.panelDockPreview.Name = "panelDockPreview";
            this.panelDockPreview.Size = new System.Drawing.Size(279, 150);
            this.panelDockPreview.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockPreview.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockPreview.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockPreview.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockPreview.Style.GradientAngle = 90;
            this.panelDockPreview.TabIndex = 0;
            this.panelDockPreview.Visible = true;
            // 
            // dockContainerItemPreview
            // 
            this.dockContainerItemPreview.Control = this.panelDockPreview;
            this.dockContainerItemPreview.Name = "dockContainerItemPreview";
            this.dockContainerItemPreview.Text = "Navigation";
            // 
            // containerBarProperties
            // 
            this.containerBarProperties.AccessibleDescription = "DotNetBar Bar (containerBarProperties)";
            this.containerBarProperties.AccessibleName = "DotNetBar Bar";
            this.containerBarProperties.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.containerBarProperties.AutoSyncBarCaption = true;
            this.containerBarProperties.CanDockBottom = false;
            this.containerBarProperties.CanDockTab = false;
            this.containerBarProperties.CanDockTop = false;
            this.containerBarProperties.CloseSingleTab = true;
            this.containerBarProperties.Controls.Add(this.panelDockProperties);
            this.containerBarProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerBarProperties.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.containerBarProperties.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItemProperties});
            this.containerBarProperties.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.containerBarProperties.Location = new System.Drawing.Point(0, 179);
            this.containerBarProperties.Name = "containerBarProperties";
            this.containerBarProperties.Size = new System.Drawing.Size(285, 176);
            this.containerBarProperties.Stretch = true;
            this.containerBarProperties.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.containerBarProperties.TabIndex = 3;
            this.containerBarProperties.TabStop = false;
            this.containerBarProperties.Text = "Properties";
            // 
            // panelDockProperties
            // 
            this.panelDockProperties.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockProperties.Controls.Add(this.barDisplayResults);
            this.panelDockProperties.Controls.Add(this.tabProperties);
            this.panelDockProperties.Controls.Add(this.cbObjects);
            this.panelDockProperties.Location = new System.Drawing.Point(3, 23);
            this.panelDockProperties.Name = "panelDockProperties";
            this.panelDockProperties.Size = new System.Drawing.Size(279, 150);
            this.panelDockProperties.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockProperties.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockProperties.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockProperties.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockProperties.Style.GradientAngle = 90;
            this.panelDockProperties.TabIndex = 0;
            this.panelDockProperties.Visible = true;
            this.panelDockProperties.Resize += new System.EventHandler(this.panelDockProperties_Resize);
            // 
            // dockContainerItemProperties
            // 
            this.dockContainerItemProperties.Control = this.panelDockProperties;
            this.dockContainerItemProperties.Name = "dockContainerItemProperties";
            this.dockContainerItemProperties.Text = "Properties";
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Controls.Add(this.containerBarGroups);
            this.dockSite2.Controls.Add(this.containerBarRecentPlans);
            this.dockSite2.Controls.Add(this.containerBarEstimating);
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
                        ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.containerBarGroups, 147, 291))),
                        ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
                                    ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.containerBarRecentPlans, 179, 263)))}, DevComponents.DotNetBar.eOrientation.Horizontal)))}, DevComponents.DotNetBar.eOrientation.Vertical))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.containerBarEstimating, 180, 546)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.dockSite2.Location = new System.Drawing.Point(1369, 171);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(365, 546);
            this.dockSite2.TabIndex = 42;
            this.dockSite2.TabStop = false;
            // 
            // containerBarGroups
            // 
            this.containerBarGroups.AccessibleDescription = "DotNetBar Bar (containerBarGroups)";
            this.containerBarGroups.AccessibleName = "DotNetBar Bar";
            this.containerBarGroups.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.containerBarGroups.AutoSyncBarCaption = true;
            this.containerBarGroups.CanDockBottom = false;
            this.containerBarGroups.CanDockTab = false;
            this.containerBarGroups.CanDockTop = false;
            this.containerBarGroups.CloseSingleTab = true;
            this.containerBarGroups.Controls.Add(this.panelDockGroups);
            this.containerBarGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerBarGroups.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.containerBarGroups.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItemGroups});
            this.containerBarGroups.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.containerBarGroups.Location = new System.Drawing.Point(3, 0);
            this.containerBarGroups.Name = "containerBarGroups";
            this.containerBarGroups.Size = new System.Drawing.Size(179, 280);
            this.containerBarGroups.Stretch = true;
            this.containerBarGroups.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.containerBarGroups.TabIndex = 3;
            this.containerBarGroups.TabStop = false;
            this.containerBarGroups.Text = "Groups";
            // 
            // panelDockGroups
            // 
            this.panelDockGroups.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockGroups.Controls.Add(this.treeObjects);
            this.panelDockGroups.Controls.Add(this.panelPlanName);
            this.panelDockGroups.Controls.Add(this.barGroups);
            this.panelDockGroups.Location = new System.Drawing.Point(3, 23);
            this.panelDockGroups.Name = "panelDockGroups";
            this.panelDockGroups.Size = new System.Drawing.Size(173, 254);
            this.panelDockGroups.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockGroups.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockGroups.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockGroups.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockGroups.Style.GradientAngle = 90;
            this.panelDockGroups.TabIndex = 0;
            this.panelDockGroups.Visible = true;
            // 
            // dockContainerItemGroups
            // 
            this.dockContainerItemGroups.Control = this.panelDockGroups;
            this.dockContainerItemGroups.Name = "dockContainerItemGroups";
            this.dockContainerItemGroups.Text = "Groups";
            // 
            // containerBarRecentPlans
            // 
            this.containerBarRecentPlans.AccessibleDescription = "DotNetBar Bar (containerBarRecentPlans)";
            this.containerBarRecentPlans.AccessibleName = "DotNetBar Bar";
            this.containerBarRecentPlans.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.containerBarRecentPlans.AutoSyncBarCaption = true;
            this.containerBarRecentPlans.CanDockBottom = false;
            this.containerBarRecentPlans.CanDockTab = false;
            this.containerBarRecentPlans.CanDockTop = false;
            this.containerBarRecentPlans.CloseSingleTab = true;
            this.containerBarRecentPlans.Controls.Add(this.panelDockRecentPlans);
            this.containerBarRecentPlans.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerBarRecentPlans.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.containerBarRecentPlans.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItemRecentPlans});
            this.containerBarRecentPlans.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.containerBarRecentPlans.Location = new System.Drawing.Point(3, 283);
            this.containerBarRecentPlans.Name = "containerBarRecentPlans";
            this.containerBarRecentPlans.Size = new System.Drawing.Size(179, 263);
            this.containerBarRecentPlans.Stretch = true;
            this.containerBarRecentPlans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.containerBarRecentPlans.TabIndex = 2;
            this.containerBarRecentPlans.TabStop = false;
            this.containerBarRecentPlans.Text = "Recent plans";
            // 
            // panelDockRecentPlans
            // 
            this.panelDockRecentPlans.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockRecentPlans.Controls.Add(this.lstRecentPlans);
            this.panelDockRecentPlans.Controls.Add(this.barRecentPlans);
            this.panelDockRecentPlans.Location = new System.Drawing.Point(3, 23);
            this.panelDockRecentPlans.Name = "panelDockRecentPlans";
            this.panelDockRecentPlans.Size = new System.Drawing.Size(173, 237);
            this.panelDockRecentPlans.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockRecentPlans.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockRecentPlans.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockRecentPlans.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockRecentPlans.Style.GradientAngle = 90;
            this.panelDockRecentPlans.TabIndex = 2;
            this.panelDockRecentPlans.Visible = true;
            // 
            // dockContainerItemRecentPlans
            // 
            this.dockContainerItemRecentPlans.Control = this.panelDockRecentPlans;
            this.dockContainerItemRecentPlans.Name = "dockContainerItemRecentPlans";
            this.dockContainerItemRecentPlans.Text = "Recent plans";
            // 
            // containerBarEstimating
            // 
            this.containerBarEstimating.AccessibleDescription = "DotNetBar Bar (containerBarEstimating)";
            this.containerBarEstimating.AccessibleName = "DotNetBar Bar";
            this.containerBarEstimating.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.containerBarEstimating.AutoSyncBarCaption = true;
            this.containerBarEstimating.CanDockBottom = false;
            this.containerBarEstimating.CanDockTab = false;
            this.containerBarEstimating.CanDockTop = false;
            this.containerBarEstimating.CloseSingleTab = true;
            this.containerBarEstimating.Controls.Add(this.panelDockEstimating);
            this.containerBarEstimating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerBarEstimating.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.containerBarEstimating.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItemEstimating});
            this.containerBarEstimating.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.containerBarEstimating.Location = new System.Drawing.Point(185, 0);
            this.containerBarEstimating.Name = "containerBarEstimating";
            this.containerBarEstimating.Size = new System.Drawing.Size(180, 546);
            this.containerBarEstimating.Stretch = true;
            this.containerBarEstimating.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.containerBarEstimating.TabIndex = 4;
            this.containerBarEstimating.TabStop = false;
            this.containerBarEstimating.Text = "Estimating";
            // 
            // panelDockEstimating
            // 
            this.panelDockEstimating.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockEstimating.Controls.Add(this.treeEstimatingItems);
            this.panelDockEstimating.Controls.Add(this.barEstimatingItems);
            this.panelDockEstimating.Location = new System.Drawing.Point(3, 23);
            this.panelDockEstimating.Name = "panelDockEstimating";
            this.panelDockEstimating.Size = new System.Drawing.Size(174, 520);
            this.panelDockEstimating.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockEstimating.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockEstimating.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockEstimating.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockEstimating.Style.GradientAngle = 90;
            this.panelDockEstimating.TabIndex = 0;
            this.panelDockEstimating.Visible = true;
            // 
            // treeEstimatingItems
            // 
            this.treeEstimatingItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeEstimatingItems.Location = new System.Drawing.Point(0, 25);
            this.treeEstimatingItems.LookAndFeel.SkinName = "Office 2010 Silver";
            this.treeEstimatingItems.Name = "treeEstimatingItems";
            this.treeEstimatingItems.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
            this.treeEstimatingItems.OptionsView.ShowHorzLines = false;
            this.treeEstimatingItems.OptionsView.ShowIndicator = false;
            this.treeEstimatingItems.OptionsView.ShowVertLines = false;
            this.treeEstimatingItems.Size = new System.Drawing.Size(174, 495);
            this.treeEstimatingItems.TabIndex = 40;
            // 
            // barEstimatingItems
            // 
            this.barEstimatingItems.CanAutoHide = false;
            this.barEstimatingItems.CanDockBottom = false;
            this.barEstimatingItems.CanDockLeft = false;
            this.barEstimatingItems.CanDockRight = false;
            this.barEstimatingItems.CanDockTab = false;
            this.barEstimatingItems.CanDockTop = false;
            this.barEstimatingItems.CanMove = false;
            this.barEstimatingItems.CanReorderTabs = false;
            this.barEstimatingItems.CanUndock = false;
            this.barEstimatingItems.ColorScheme.PredefinedColorScheme = DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
            this.barEstimatingItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.barEstimatingItems.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.barEstimatingItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.barEstimatingItems.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btEstimatingItemsExpandAll,
            this.btEstimatingItemsCollapseAll,
            this.btEstimatingItemsUpdatePrices,
            this.btEstimatingItemsPrint});
            this.barEstimatingItems.Location = new System.Drawing.Point(0, 0);
            this.barEstimatingItems.Margin = new System.Windows.Forms.Padding(5);
            this.barEstimatingItems.Name = "barEstimatingItems";
            this.barEstimatingItems.RoundCorners = false;
            this.barEstimatingItems.SaveLayoutChanges = false;
            this.barEstimatingItems.Size = new System.Drawing.Size(174, 25);
            this.barEstimatingItems.Stretch = true;
            this.barEstimatingItems.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barEstimatingItems.TabIndex = 39;
            this.barEstimatingItems.TabStop = false;
            this.barEstimatingItems.Text = "bar1";
            // 
            // btEstimatingItemsExpandAll
            // 
            this.btEstimatingItemsExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingItemsExpandAll.Image")));
            this.btEstimatingItemsExpandAll.Name = "btEstimatingItemsExpandAll";
            this.btEstimatingItemsExpandAll.Click += new System.EventHandler(this.btEstimatingItemsExpandAll_Click);
            // 
            // btEstimatingItemsCollapseAll
            // 
            this.btEstimatingItemsCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingItemsCollapseAll.Image")));
            this.btEstimatingItemsCollapseAll.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btEstimatingItemsCollapseAll.Name = "btEstimatingItemsCollapseAll";
            this.btEstimatingItemsCollapseAll.Click += new System.EventHandler(this.btEstimatingItemsCollapseAll_Click);
            // 
            // btEstimatingItemsUpdatePrices
            // 
            this.btEstimatingItemsUpdatePrices.BeginGroup = true;
            this.btEstimatingItemsUpdatePrices.Image = global::QuoterPlan.Properties.Resources.if_price_tag_usd_172529;
            this.btEstimatingItemsUpdatePrices.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btEstimatingItemsUpdatePrices.Name = "btEstimatingItemsUpdatePrices";
            this.btEstimatingItemsUpdatePrices.Click += new System.EventHandler(this.btEstimatingItemsUpdatePrices_Click);
            // 
            // btEstimatingItemsPrint
            // 
            this.btEstimatingItemsPrint.Image = ((System.Drawing.Image)(resources.GetObject("btEstimatingItemsPrint.Image")));
            this.btEstimatingItemsPrint.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.btEstimatingItemsPrint.Name = "btEstimatingItemsPrint";
            this.btEstimatingItemsPrint.Visible = false;
            this.btEstimatingItemsPrint.Click += new System.EventHandler(this.btEstimatingItemsPrint_Click);
            // 
            // dockContainerItemEstimating
            // 
            this.dockContainerItemEstimating.Control = this.panelDockEstimating;
            this.dockContainerItemEstimating.Name = "dockContainerItemEstimating";
            this.dockContainerItemEstimating.Text = "Estimating";
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(5, 717);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(1729, 0);
            this.dockSite8.TabIndex = 48;
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(5, 171);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 546);
            this.dockSite5.TabIndex = 45;
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(1734, 171);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 546);
            this.dockSite6.TabIndex = 46;
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(5, 171);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(1729, 0);
            this.dockSite7.TabIndex = 47;
            this.dockSite7.TabStop = false;
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Location = new System.Drawing.Point(5, 171);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(1729, 0);
            this.dockSite3.TabIndex = 43;
            this.dockSite3.TabStop = false;
            // 
            // lblNoPlan
            // 
            this.lblNoPlan.BackColor = System.Drawing.SystemColors.Control;
            this.lblNoPlan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNoPlan.Location = new System.Drawing.Point(1099, 219);
            this.lblNoPlan.Name = "lblNoPlan";
            this.lblNoPlan.Size = new System.Drawing.Size(252, 80);
            this.lblNoPlan.TabIndex = 30;
            this.lblNoPlan.Text = "No plans are associated to this project.\r\n\r\nTo associate one or many plans, use t" +
    "he \'Insert Plans\' menu from the \'Plans\' tab.";
            this.lblNoPlan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoPlan.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.lblNoPlan.Visible = false;
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            // 
            // reportsControl
            // 
            this.reportsControl.BackColor = System.Drawing.SystemColors.Control;
            this.reportsControl.Dirty = false;
            this.reportsControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.reportsControl.Location = new System.Drawing.Point(285, 173);
            this.reportsControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.reportsControl.Name = "reportsControl";
            this.reportsControl.Size = new System.Drawing.Size(633, 392);
            this.reportsControl.TabIndex = 0;
            // 
            // dockContainerEstimating
            // 
            this.dockContainerEstimating.Name = "dockContainerEstimating";
            this.dockContainerEstimating.Text = "dockContainerItem5";
            // 
            // treeDBEstimatingItems
            // 
            this.treeDBEstimatingItems.Location = new System.Drawing.Point(1096, 481);
            this.treeDBEstimatingItems.LookAndFeel.SkinName = "Office 2010 Silver";
            this.treeDBEstimatingItems.Name = "treeDBEstimatingItems";
            this.treeDBEstimatingItems.OptionsBehavior.EnableFiltering = true;
            this.treeDBEstimatingItems.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
            this.treeDBEstimatingItems.OptionsView.ShowAutoFilterRow = true;
            this.treeDBEstimatingItems.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            this.treeDBEstimatingItems.OptionsView.ShowHorzLines = false;
            this.treeDBEstimatingItems.OptionsView.ShowVertLines = false;
            this.treeDBEstimatingItems.Size = new System.Drawing.Size(253, 80);
            this.treeDBEstimatingItems.TabIndex = 49;
            this.treeDBEstimatingItems.Visible = false;
            // 
            // treeTemplatesLibrary
            // 
            this.treeTemplatesLibrary.Location = new System.Drawing.Point(1096, 567);
            this.treeTemplatesLibrary.LookAndFeel.SkinName = "Office 2010 Silver";
            this.treeTemplatesLibrary.Name = "treeTemplatesLibrary";
            this.treeTemplatesLibrary.OptionsBehavior.EnableFiltering = true;
            this.treeTemplatesLibrary.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
            this.treeTemplatesLibrary.OptionsView.ShowAutoFilterRow = true;
            this.treeTemplatesLibrary.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            this.treeTemplatesLibrary.OptionsView.ShowHorzLines = false;
            this.treeTemplatesLibrary.OptionsView.ShowVertLines = false;
            this.treeTemplatesLibrary.Size = new System.Drawing.Size(253, 80);
            this.treeTemplatesLibrary.TabIndex = 50;
            this.treeTemplatesLibrary.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1739, 748);
            this.Controls.Add(this.treeTemplatesLibrary);
            this.Controls.Add(this.mainControl);
            this.Controls.Add(this.treeDBEstimatingItems);
            this.Controls.Add(this.lblNoPlan);
            this.Controls.Add(this.panelWelcome);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.reportsControl);
            this.Controls.Add(this.flowPlans);
            this.Controls.Add(this.panelPlansAction);
            this.Controls.Add(this.panelBrightnessContrast);
            this.Controls.Add(this.panelRotation);
            this.Controls.Add(this.contextMenuBar1);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.Controls.Add(this.barStatus);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(638, 477);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Quoter Plan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ribbonControl.ResumeLayout(false);
            this.ribbonControl.PerformLayout();
            this.ribbonPanelEstimating.ResumeLayout(false);
            this.ribbonPanelReport.ResumeLayout(false);
            this.ribbonPanel.ResumeLayout(false);
            this.ribbonPanelTemplates.ResumeLayout(false);
            this.ribbonPanelPlans.ResumeLayout(false);
            this.ribbonPanelExtensions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDisplayResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabProperties)).EndInit();
            this.tabProperties.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.superTabProperties)).EndInit();
            this.superTabProperties.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridObjectProperties)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstRecentPlans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barRecentPlans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeObjects)).EndInit();
            this.panelPlanName.ResumeLayout(false);
            this.panelPlanName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.panelBrightnessContrast.ResumeLayout(false);
            this.panelRotation.ResumeLayout(false);
            this.panelWelcome.ResumeLayout(false);
            this.panelWelcomeMenu.ResumeLayout(false);
            this.panelWelcomeMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstRecentProjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWelcome)).EndInit();
            this.panelPlansAction.ResumeLayout(false);
            this.dockSite1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerBarLayers)).EndInit();
            this.containerBarLayers.ResumeLayout(false);
            this.panelDockLayers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerBarNavigation)).EndInit();
            this.containerBarNavigation.ResumeLayout(false);
            this.panelDockPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerBarProperties)).EndInit();
            this.containerBarProperties.ResumeLayout(false);
            this.panelDockProperties.ResumeLayout(false);
            this.dockSite2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerBarGroups)).EndInit();
            this.containerBarGroups.ResumeLayout(false);
            this.panelDockGroups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerBarRecentPlans)).EndInit();
            this.containerBarRecentPlans.ResumeLayout(false);
            this.panelDockRecentPlans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerBarEstimating)).EndInit();
            this.containerBarEstimating.ResumeLayout(false);
            this.panelDockEstimating.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeEstimatingItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEstimatingItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDBEstimatingItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeTemplatesLibrary)).EndInit();
            this.ResumeLayout(false);

        }

        private DevComponents.DotNetBar.RibbonControl ribbonControl;

		private DevComponents.DotNetBar.RibbonPanel ribbonPanel;

		private DevComponents.DotNetBar.RibbonTabItem ribbonTabStart;

		private DevComponents.DotNetBar.Bar barStatus;

		private DevComponents.DotNetBar.RibbonBar ribbonBarEdit;

		private DevComponents.DotNetBar.ButtonItem btEditPaste;

		private DevComponents.DotNetBar.ItemContainer itemContainerEdit;

		private DevComponents.DotNetBar.ButtonItem btEditCut;

		private DevComponents.DotNetBar.ButtonItem btEditCopy;

		private DevComponents.DotNetBar.ButtonItem btEditDelete;

		private DevComponents.DotNetBar.ItemContainer itemContainerUndoRedo;

		private DevComponents.DotNetBar.ButtonItem btEditUndo;

		private DevComponents.DotNetBar.ButtonItem btEditRedo;

		private DevComponents.DotNetBar.RibbonBar ribbonBarScale;

		private DevComponents.DotNetBar.ButtonItem btScaleSet;

		private DevComponents.DotNetBar.RibbonBar ribbonBarAnnotations;

		private DevComponents.DotNetBar.ButtonItem btMarkZone;

		private DevComponents.DotNetBar.ButtonItem btInsertNote;

		private DevComponents.DotNetBar.ButtonItem btInsertPicture;

		private DevComponents.DotNetBar.RibbonBar ribbonBarTools;

		private DevComponents.DotNetBar.ButtonItem btToolSelection;

		private DevComponents.DotNetBar.ButtonItem btToolPan;

		private DevComponents.DotNetBar.ButtonItem btToolArea;

		private DevComponents.DotNetBar.ButtonItem btToolPerimeter;

		private DevComponents.DotNetBar.ButtonItem btToolCounter;

		private DevComponents.DotNetBar.ButtonItem btToolAngle;

		private DevComponents.DotNetBar.ButtonItem btToolRuler;

		private DevComponents.DotNetBar.RibbonBar ribbonBarZoom;

		private DevComponents.DotNetBar.ButtonItem btZoomIn;

		private DevComponents.DotNetBar.ButtonItem btZoomOut;

		private DevComponents.DotNetBar.ButtonItem btZoomActualSize;

		private DevComponents.DotNetBar.ButtonItem btZoomToWindow;

		private DevComponents.DotNetBar.ButtonItem btZoomToSelection;

		private DevComponents.DotNetBar.ButtonItem btBookmarks;

		private DevComponents.DotNetBar.ButtonItem btZoomTo75;

		private DevComponents.DotNetBar.ButtonItem btZoomTo50;

		private DevComponents.DotNetBar.ButtonItem btZoomTo25;

		private DevComponents.DotNetBar.ButtonItem btZoomTo150;

		private DevComponents.DotNetBar.ButtonItem btZoomTo200;

		private DevComponents.DotNetBar.ButtonItem btStyleRetroGlass;

		private DevComponents.DotNetBar.ButtonItem buttonItem61;

		private DevComponents.DotNetBar.ButtonItem btModifyBookmarks;

		private QuoterPlanControls.PanControl panControl;

		private DevComponents.DotNetBar.Office2007StartButton startButton;

		private DevComponents.DotNetBar.ItemContainer itemContainerFileMenu;

		private DevComponents.DotNetBar.ItemContainer itemContainerFileMenu2;

		private DevComponents.DotNetBar.ButtonItem btProjectNew;

		private DevComponents.DotNetBar.ButtonItem btProjectOpen;

		private DevComponents.DotNetBar.ButtonItem btProjectSave;

		private DevComponents.DotNetBar.ButtonItem btProjectClose;

		private DevComponents.DotNetBar.GalleryContainer galleryRecentProjects;

		private DevComponents.DotNetBar.LabelItem lblFileRecentProjects;

		private DevComponents.DotNetBar.RibbonPanel ribbonPanelPlans;

		private DevComponents.DotNetBar.RibbonTabItem ribbonTabPlans;

		private DevComponents.DotNetBar.ButtonItem btProjectSaveAs;

		private DevComponents.DotNetBar.ButtonItem btHelpContent;

		private DevComponents.DotNetBar.ButtonItem btHelpYoutube;

		private DevComponents.DotNetBar.ButtonItem btHelpAbout;

		private DevComponents.DotNetBar.CheckBoxItem checkBoxItem1;

		private DevComponents.DotNetBar.SliderItem sliderItem1;

		private DevComponents.DotNetBar.SliderItem sliderItem2;

		private DevComponents.DotNetBar.CheckBoxItem checkBoxItem2;

		private DevComponents.AdvTree.AdvTree lstLayers;

		private DevComponents.AdvTree.ColumnHeader columnLayerVisible;

		private DevComponents.AdvTree.NodeConnector nodeConnector2;

		private DevComponents.AdvTree.ColumnHeader columnLayerName;

		private DevComponents.AdvTree.ColumnHeader columnLayerOpacity;

		private DevComponents.AdvTree.AdvTree treeObjects;

		private System.Timers.Timer timer1;

		private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;

		private DevComponents.DotNetBar.ButtonItem bEditPopup;

		private DevComponents.DotNetBar.ButtonItem bCut;

		private DevComponents.DotNetBar.ButtonItem bCopy;

		private DevComponents.DotNetBar.ButtonItem bPaste;

		private DevComponents.DotNetBar.ButtonItem bSelectAll;

		private DevComponents.DotNetBar.ButtonItem bDelete;

		private DevComponents.DotNetBar.ButtonItem bSelectGroup;

		private DevComponents.DotNetBar.ButtonItem bDeductionCreate;

		private DevComponents.DotNetBar.ButtonItem bDeductionsEdit;

		private DevComponents.DotNetBar.ButtonItem bPointInsert;

		private DevComponents.DotNetBar.ButtonItem bGroupAddObject;

		private DevComponents.DotNetBar.ButtonItem bLayerMoveTo;

		private DevComponents.DotNetBar.ButtonItem bGroupMoveTo;

		private DevComponents.DotNetBar.ButtonItem bGroupMoveToNew;

		private DevComponents.DotNetBar.ButtonItem bPerimeterClose;

		private DevComponents.DotNetBar.ButtonItem bPerimeterOpen;

		private DevComponents.DotNetBar.ButtonItem bPerimeterCreateFromArea;

		private DevComponents.DotNetBar.ButtonItem bOpeningCreateFromSegment;

		private DevComponents.DotNetBar.ButtonItem bOpeningDelete;

		private DevComponents.DotNetBar.ButtonItem bOpeningCreateFromPosition;

		private DevComponents.DotNetBar.ButtonItem bAutoAdjustToZone;

		private DevComponents.DotNetBar.ButtonItem btScaleImperial;

		private DevComponents.DotNetBar.ButtonItem btScaleMetric;

		private DevComponents.DotNetBar.ButtonItem btScalePrecision64;

		private DevComponents.DotNetBar.ButtonItem btScalePrecision32;

		private DevComponents.DotNetBar.ButtonItem btScalePrecision16;

		private DevComponents.DotNetBar.ButtonItem btScalePrecision8;

		private DevComponents.DotNetBar.LabelItem lblPrecision;

		private DevComponents.DotNetBar.LabelItem lblSystemType;

		private DevComponents.DotNetBar.Controls.ComboBoxEx cbObjects;

		private DevComponents.AdvTree.ColumnHeader columnObjectIcon;

		private DevComponents.AdvTree.ColumnHeader columnObjectName;

		private DevComponents.AdvTree.ColumnHeader columnObjectInfo;

		private DevComponents.AdvTree.ColumnHeader columnObjectColor;

		private DevComponents.AdvTree.ColumnHeader columnObjectVisible;

		private DevComponents.AdvTree.ColumnHeader columnObjectPadding;

		private DevComponents.AdvTree.NodeConnector nodeConnector1;

		private DevComponents.DotNetBar.LabelItem lblStatusBarPadding;

		private DevComponents.DotNetBar.ElementStyle elementStyle1;

		private DevComponents.DotNetBar.ElementStyle elementStyle2;

		private DevComponents.DotNetBar.ButtonItem bSelectThisGroup;

		private DevComponents.DotNetBar.ButtonItem bSelectObjectType;

		private DevComponents.DotNetBar.ButtonItem bSelectThisGroup1;

		private DevComponents.DotNetBar.ButtonItem bSelectObjectType1;

		private DevComponents.DotNetBar.ButtonItem bUnselectAll;

		private DevComponents.DotNetBar.ButtonItem bLayerMoveTo1;

		private DevComponents.DotNetBar.ButtonItem bGroupMoveTo1;

		private DevComponents.DotNetBar.ItemContainer itemContainer5;

		private DevComponents.DotNetBar.LabelItem lblNoPerimeter;

		private DevComponents.DotNetBar.LabelItem lblNoCounter;

		private DevComponents.DotNetBar.LabelItem lblNoDistance;

		private DevComponents.DotNetBar.GalleryGroup galleryGroup1;

		private DevComponents.DotNetBar.GalleryGroup galleryGroupArea;

		private DevComponents.DotNetBar.GalleryGroup galleryGroupPerimeter;

		private DevComponents.DotNetBar.GalleryGroup galleryGroupCounter;

		private DevComponents.DotNetBar.LabelItem lblNoArea;

		private DevComponents.DotNetBar.LabelItem lblAreaGroups;

		private DevComponents.DotNetBar.GalleryContainer galleryAreaGroups;

		private DevComponents.DotNetBar.LabelItem lblAreaTemplates;

		private DevComponents.DotNetBar.GalleryContainer galleryAreaTemplates;

		private DevComponents.DotNetBar.GalleryContainer galleryPerimeterGroups;

		private DevComponents.DotNetBar.LabelItem lblPerimeterGroups;

		private DevComponents.DotNetBar.LabelItem lblPerimeterTemplates;

		private DevComponents.DotNetBar.GalleryContainer galleryPerimeterTemplates;

		private DevComponents.DotNetBar.RibbonBar ribbonBarBrowse;

		private DevComponents.DotNetBar.ItemContainer itemContainerBrowse1;

		private DevComponents.DotNetBar.LabelItem lblBrowseGroup;

		private DevComponents.DotNetBar.ItemContainer itemContainerBrowse3;

		private DevComponents.DotNetBar.ButtonItem btBrowsePrevious;

		private DevComponents.DotNetBar.ButtonItem btBrowseNext;

		private DevComponents.DotNetBar.ItemContainer itemContainerBrowse2;

		private DevComponents.DotNetBar.LabelItem lblBrowseObjectType;

		private DevComponents.DotNetBar.ItemContainer itemContainerBrowse4;

		private DevComponents.DotNetBar.ButtonItem btBrowseObjectTypePrevious;

		private DevComponents.DotNetBar.ButtonItem btBrowseObjectTypeNext;

		private DevComponents.DotNetBar.LabelItem lblCounterGroups;

		private DevComponents.DotNetBar.GalleryContainer galleryCounterGroups;

		private DevComponents.DotNetBar.LabelItem lblCounterTemplates;

		private DevComponents.DotNetBar.GalleryContainer galleryCounterTemplates;

		private DevComponents.DotNetBar.LabelItem lblDistanceGroups;

		private DevComponents.DotNetBar.GalleryContainer galleryDistanceGroups;

		private DevComponents.DotNetBar.LabelItem lblDistanceTemplates;

		private DevComponents.DotNetBar.GalleryContainer galleryDistanceTemplates;

		private DevComponents.DotNetBar.TabControl tabProperties;

		private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;

		private DevComponents.DotNetBar.TabItem tabItem1;

		private DevComponents.DotNetBar.TabItem tabItem3;

		private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;

		private DevComponents.DotNetBar.TabItem tabItem2;

		private QuoterPlanControls.MainControl mainControl;

		private QuoterPlan.ExtensionsManager extensionsManager;

		private DevComponents.DotNetBar.SuperTabControl superTabProperties;

		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;

		private DevComponents.DotNetBar.SuperTabItem superTabItem1;

		private DevComponents.DotNetBar.AdvPropertyGrid gridObjectProperties;

		private System.ComponentModel.BackgroundWorker backgroundWorker;

		private System.Windows.Forms.FlowLayoutPanel flowPlans;

		private DevComponents.AdvTree.AdvTree lstRecentPlans;

		private DevComponents.AdvTree.ColumnHeader columnHeader1;

		private DevComponents.AdvTree.ColumnHeader columnHeader2;

		private DevComponents.AdvTree.NodeConnector nodeConnector3;

		private DevComponents.DotNetBar.SliderItem sliderItem4;

		private DevComponents.DotNetBar.LabelItem lblImageQuality;

		private DevComponents.DotNetBar.SliderItem qualitySlider;

		private DevComponents.DotNetBar.LabelItem lblZoom;

		private DevComponents.DotNetBar.SliderItem zoomSlider;

		private DevComponents.DotNetBar.LabelItem lblStatus;

		private DevComponents.DotNetBar.LabelItem lblStatusPadding2;

		private DevComponents.DotNetBar.LabelItem lblStatusBarPadding3;

		private DevComponents.DotNetBar.ButtonItem btZoom50;

		private DevComponents.DotNetBar.RibbonBar ribbonBarPlans;

		private DevComponents.DotNetBar.ButtonItem btPlanRemove;

		private DevComponents.DotNetBar.ButtonItem btPlanExport;

		private DevComponents.DotNetBar.ButtonItem btPlanProperties;

		private QuoterPlan.LabelEx lblNoPlan;

		private DevComponents.DotNetBar.RibbonBar ribbonBarBrightnessContrast;

		private DevComponents.DotNetBar.ItemContainer itemContainerBrightness;

		private DevComponents.DotNetBar.LabelItem lblBrightness;

		private DevComponents.DotNetBar.SliderItem sliderBrightness;

		private DevComponents.DotNetBar.LabelItem lblBrightnessContrastPadding1;

		private DevComponents.DotNetBar.ItemContainer itemContainerContrast;

		private DevComponents.DotNetBar.LabelItem lblContrast;

		private DevComponents.DotNetBar.SliderItem sliderContrast;

		private DevComponents.DotNetBar.PanelEx panelBrightnessContrast;

		private DevComponents.DotNetBar.RibbonBar ribbonBarImage;

		private DevComponents.DotNetBar.ButtonItem btBrightnessContrast;

		private DevComponents.DotNetBar.ButtonItem btRotation;

		private DevComponents.DotNetBar.PanelEx panelRotation;

		private DevComponents.DotNetBar.RibbonBar ribbonBarRotation;

		private DevComponents.DotNetBar.ButtonItem btFlipHorizontally;

		private DevComponents.DotNetBar.ButtonItem btFlipVertically;

		private DevComponents.DotNetBar.ButtonItem btRotateLeft;

		private DevComponents.DotNetBar.ButtonItem btRotateRight;

		private DevComponents.DotNetBar.RibbonBar ribbonBarPlansInsert;

		private DevComponents.DotNetBar.ButtonItem btPlanInsertFromPDF;

		private DevComponents.DotNetBar.LabelItem iblImportDPI;

		private DevComponents.DotNetBar.CheckBoxItem op172Dpi;

		private DevComponents.DotNetBar.CheckBoxItem op300Dpi;

		private DevComponents.DotNetBar.CheckBoxItem opOtherDpi;

		private DevComponents.DotNetBar.SliderItem sliderDpi;

		private DevComponents.DotNetBar.ItemContainer itemContainerDpi;

		private DevComponents.DotNetBar.LabelItem lblDpi1;

		private DevComponents.DotNetBar.LabelItem labelDpiPadding1;

		private DevComponents.DotNetBar.LabelItem lblDpi2;

		private DevComponents.DotNetBar.ButtonItem btPlanInsertFromImage;

		private DevComponents.DotNetBar.ButtonItem btPlanLoad;

		private System.Windows.Forms.WebBrowser webBrowser;

		private DevComponents.DotNetBar.RibbonPanel ribbonPanelReport;

		private DevComponents.DotNetBar.RibbonTabItem ribbonTabReport;

		private DevComponents.Editors.ComboItem comboItem1;

		private DevComponents.Editors.ComboItem comboItem2;

		private DevComponents.Editors.ComboItem comboItem3;

		private DevComponents.Editors.ComboItem comboItem4;

		private DevComponents.Editors.ComboItem comboItem5;

		private DevComponents.Editors.ComboItem comboItem6;

		private DevComponents.DotNetBar.LabelItem labelItem1;

		private DevComponents.DotNetBar.RibbonBar ribbonReportOrder;

		private DevComponents.DotNetBar.ButtonItem btReportFilter;

		private DevComponents.DotNetBar.RibbonBar ribbonExportReport;

		private DevComponents.DotNetBar.RibbonBar ribbonPrintReport;

		private DevComponents.DotNetBar.ButtonItem btReportPrint;

		private DevComponents.DotNetBar.ButtonItem btReportPrintPreview;

		private DevComponents.DotNetBar.ButtonItem btReportPrintSetup;

		private DevComponents.DotNetBar.ButtonItem btReportExportToXML;

		private DevComponents.DotNetBar.ButtonItem btReportExportToHTML;

		private DevComponents.DotNetBar.ButtonItem btReportExportToExcel;

		private DevComponents.DotNetBar.ButtonItem btReportExportToCSV;

		private DevComponents.DotNetBar.LabelItem lblReportSystemType;

		private DevComponents.DotNetBar.ButtonItem btReportScaleImperial;

		private DevComponents.DotNetBar.ButtonItem btReportScaleMetric;

		private DevComponents.DotNetBar.LabelItem lblReportPrecision;

		private DevComponents.DotNetBar.ButtonItem btReportScalePrecision64;

		private DevComponents.DotNetBar.ButtonItem btReportScalePrecision32;

		private DevComponents.DotNetBar.ButtonItem btReportScalePrecision16;

		private DevComponents.DotNetBar.ButtonItem btReportScalePrecision8;

		private DevComponents.DotNetBar.ButtonItem btZoom75;

		private DevComponents.DotNetBar.ButtonItem btZoom25;

		private DevComponents.DotNetBar.ButtonItem bAngleDegreeType;

		private DevComponents.DotNetBar.ButtonItem bAngleSlopeType;

		private DevComponents.DotNetBar.ButtonItem btLicensing;

		private DevComponents.DotNetBar.ButtonItem btLicenseDeactivate;

		private DevComponents.DotNetBar.ButtonItem btSave;

		private DevComponents.DotNetBar.ButtonItem btLicenseBuy;

		private DevComponents.DotNetBar.ButtonItem btLicenseActivate;

		private DevComponents.DotNetBar.ButtonItem btProjectInfo;

		private DevComponents.DotNetBar.ButtonItem btExit;

		private DevComponents.DotNetBar.ButtonItem btStyleMetro;

		private DevComponents.DotNetBar.ButtonItem btStyleClassicBlue;

		private DevComponents.DotNetBar.ButtonItem btStyleClassicSilver;

		private DevComponents.DotNetBar.ButtonItem btStyleClassicBlack;

		private DevComponents.DotNetBar.ButtonItem btStyleClassicExecutive;

		private DevComponents.DotNetBar.ButtonItem btStyleModern;

		private DevComponents.DotNetBar.ButtonItem btStyleRetroBlue;

		private DevComponents.DotNetBar.ButtonItem btStyleRetroBlack;

		private DevComponents.DotNetBar.ButtonItem btStyleRetroSilver;

		private DevComponents.DotNetBar.ColorPickerDropDown btSetThemeColor;

		private DevComponents.DotNetBar.Command AppCommandTheme;

		private System.Windows.Forms.Panel panelWelcome;

		private DevComponents.DotNetBar.PanelEx panelWelcomeMenu;

		private System.Windows.Forms.PictureBox picWelcome;

		private QuoterPlan.LabelEx lblRecentProjects;

		private DevComponents.AdvTree.AdvTree lstRecentProjects;

		private DevComponents.AdvTree.ColumnHeader columnHeader3;

		private DevComponents.AdvTree.ColumnHeader columnHeader4;

		private DevComponents.AdvTree.NodeConnector nodeConnector4;

		private DevComponents.DotNetBar.ElementStyle elementStyle6;

		private DevComponents.AdvTree.ColumnHeader columnHeader5;

		private DevComponents.DotNetBar.ButtonItem btSettings;

		private DevComponents.DotNetBar.ButtonItem btLanguageEnglish;

		private DevComponents.DotNetBar.ButtonItem btLanguageFrench;

		private DevComponents.DotNetBar.ButtonItem btBrightnessContrastRestore;

		private DevComponents.DotNetBar.ButtonItem btRotationApply;

		private DevComponents.DotNetBar.ButtonItem btRotationCancel;

		private DevComponents.DotNetBar.ButtonItem btBrightnessContrastApply;

		private DevComponents.DotNetBar.ButtonItem btBrightnessContrastCancel;

		private DevComponents.DotNetBar.LabelItem lblBrightnessContrastPadding2;

		private DevComponents.DotNetBar.LabelItem lblBarRotationPadding1;

		private DevComponents.DotNetBar.LabelItem lblBarRotationSeparator;

		private DevComponents.DotNetBar.LabelItem lblBarRotationPadding2;

		private DevComponents.DotNetBar.LabelItem lblBrightnessContrastSeparator;

		private DevComponents.DotNetBar.LabelItem lblBrightnessContrastPadding3;

		private DevComponents.DotNetBar.SuperTooltip superTooltip;

		private DevComponents.DotNetBar.ButtonItem btHelp;

		private DevComponents.DotNetBar.LabelItem lblLanguage;

		private DevComponents.DotNetBar.LabelItem lblTheme;

		private DevComponents.DotNetBar.Bar barDisplayResults;

		private DevComponents.DotNetBar.LabelItem lblDisplayResults;

		private DevComponents.DotNetBar.ButtonItem btDisplayResultsForThisPlan;

		private DevComponents.DotNetBar.LabelItem lblDisplayResultsPadding;

		private DevComponents.DotNetBar.ButtonItem btDisplayResultsForAllPlans;

		private DevComponents.DotNetBar.Bar barLayers;

		private DevComponents.DotNetBar.ButtonItem btLayerAdd;

		private DevComponents.DotNetBar.ButtonItem btLayerRemove;

		private DevComponents.DotNetBar.ButtonItem btLayerRename;

		private DevComponents.DotNetBar.Bar barGroups;

		private DevComponents.DotNetBar.ButtonItem btGroupLocate;

		private DevComponents.DotNetBar.ButtonItem btGroupRemove;

		private DevComponents.DotNetBar.ButtonItem btGroupRename;

		private DevComponents.DotNetBar.ButtonItem btGroupSelect;

		private DevComponents.DotNetBar.ButtonItem bZoomToObject;

		private DevComponents.DotNetBar.ButtonItem bZoomToGroup;

		private DevComponents.DotNetBar.ButtonItem btZoomToObject;

		private DevComponents.DotNetBar.ButtonX btNew;

		private DevComponents.DotNetBar.ButtonX btOpen;

		private DevComponents.DotNetBar.LabelItem lblScrollSpeed;

		private DevComponents.DotNetBar.SliderItem sliderScrollSpeed;

		private DevComponents.DotNetBar.ItemContainer containerScroll;

		private DevComponents.DotNetBar.LabelItem lblScrollFast;

		private DevComponents.DotNetBar.LabelItem lblScrollPadding;

		private DevComponents.DotNetBar.LabelItem lblScrollSlow;

		private DevComponents.DotNetBar.ButtonItem bBringToFront;

		private DevComponents.DotNetBar.ButtonItem bSendToBack;

		private DevComponents.DotNetBar.LabelItem lblReportTheme;

		private DevComponents.DotNetBar.ButtonItem bEditNote;

		private DevComponents.DotNetBar.LabelItem lblDataFolder;

		private DevComponents.DotNetBar.ButtonItem btSelectDataFolder;

		private DevComponents.DotNetBar.RibbonBar ribbonBarPrintExport;

		private DevComponents.DotNetBar.ButtonItem btPrintPlan;

		private DevComponents.DotNetBar.LabelItem lblExportToExcelOptions;

		private DevComponents.DotNetBar.ButtonItem buttonItem1;

		private DevComponents.DotNetBar.ButtonItem btExportToExcelRawAndFormatted;

		private DevComponents.DotNetBar.ButtonItem btExportToExcelRawData;

		private DevComponents.DotNetBar.ButtonItem btExportToExcelFormattedData;

		private DevComponents.DotNetBar.ButtonItem btEditSendData;

		private DevComponents.DotNetBar.LabelItem lblStatusBarPadding2;

		private DevComponents.DotNetBar.SwitchButtonItem switchOrtho;

		private DevComponents.DotNetBar.LabelItem lblOrtho;

		private DevComponents.DotNetBar.ButtonItem btLayerOpenList;

		private DevComponents.DotNetBar.ButtonItem btLayerSaveList;

		private DevComponents.DotNetBar.ButtonItem btLayerSaveListAs;

		private DevComponents.DotNetBar.LabelItem lblPrintPlanOptions;

		private DevComponents.DotNetBar.ButtonItem buttonItem2;

		private DevComponents.DotNetBar.ButtonItem btPrintPlanFullSize;

		private DevComponents.DotNetBar.ButtonItem btPrintPlanWindow;

		private DevComponents.DotNetBar.ButtonItem btExportPlanToPDF;

		private DevComponents.DotNetBar.RibbonBar ribbonBarMultiPlans;

		private DevComponents.DotNetBar.ButtonItem btPlansExport;

		private DevComponents.DotNetBar.ButtonItem btPlansPrint;

		private DevComponents.DotNetBar.PanelEx panelPlansAction;

		private DevComponents.DotNetBar.RibbonBar ribbonBarPlansAction;

		private DevComponents.DotNetBar.ButtonItem btPlansActionSelectAll;

		private DevComponents.DotNetBar.ButtonItem btPlansActionSelectNone;

		private DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding4;

		private DevComponents.DotNetBar.LabelItem lblPlansActionSeparator;

		private DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding2;

		private DevComponents.DotNetBar.ButtonItem btPlansActionApply;

		private DevComponents.DotNetBar.ButtonItem btPlansActionCancel;

		private DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding1;

		private DevComponents.DotNetBar.CircularProgressItem progressPlansAction;

		private DevComponents.DotNetBar.ItemContainer itemContainerExportType;

		private DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding3;

		private DevComponents.DotNetBar.CheckBoxItem checkBoxExportSingleFile;

		private DevComponents.DotNetBar.CheckBoxItem checkBoxExportMultiFiles;

		private DevComponents.DotNetBar.CheckBoxItem opConvertToColor;

		private DevComponents.DotNetBar.LabelItem labelItem2;

		private DevComponents.DotNetBar.LabelItem iblImportColorManagement;

		private DevComponents.DotNetBar.LabelItem labelItem4;

		private DevComponents.DotNetBar.ButtonItem bSetHeight;

		private DevComponents.DotNetBar.ButtonItem bDeductionDuplicate;

		private DevComponents.DotNetBar.ButtonItem btUndo;

		private DevComponents.DotNetBar.ButtonItem btRedo;

		private DevComponents.DotNetBar.Bar barRecentPlans;

		private DevComponents.DotNetBar.ButtonItem btPlanRename;

		private DevComponents.DotNetBar.ButtonItem btRenamePlan;

		private System.Windows.Forms.Panel panelPlanName;

		private DevComponents.DotNetBar.Controls.ComboBoxEx cbPlans;

		private QuoterPlan.TextBoxEx txtPlanName;

		private DevComponents.DotNetBar.ButtonItem bOpeningDuplicate;

		private DevComponents.DotNetBar.ButtonItem btReportExportToEE;

		private DevComponents.DotNetBar.LabelItem lblAreaFilter;

		private DevComponents.DotNetBar.LabelItem lblPerimeterFilter;

		private DevComponents.DotNetBar.LabelItem lblDistanceFilter;

		private DevComponents.DotNetBar.TextBoxItem txtDistanceFilter;

		private DevComponents.DotNetBar.LabelItem lblCounterFilter;

		private DevComponents.DotNetBar.ItemContainer itemContainerAreaFilter;

		private DevComponents.DotNetBar.ButtonItem btAreaFilterClear;

		private DevComponents.DotNetBar.TextBoxItem txtAreaFilter;

		private DevComponents.DotNetBar.ItemContainer itemContainerPerimeterFilter;

		private DevComponents.DotNetBar.ItemContainer itemContainerDistanceFilter;

		private DevComponents.DotNetBar.ItemContainer itemContainerCounterFilter;

		private DevComponents.DotNetBar.ButtonItem btPerimeterFilterClear;

		private DevComponents.DotNetBar.ButtonItem btDistanceFilterClear;

		private DevComponents.DotNetBar.ButtonItem btCounterFilterClear;

		private DevComponents.DotNetBar.TextBoxItem txtCounterFilter;

		private DevComponents.DotNetBar.LabelItem lblAreaFilterPadding;

		private DevComponents.DotNetBar.TextBoxItem txtPerimeterFilter;

		private DevComponents.DotNetBar.ButtonItem btLayerMoveUp;

		private DevComponents.DotNetBar.ButtonItem btLayerMoveDown;

		private DevComponents.DotNetBar.ButtonItem bDropInsert;

		private DevComponents.DotNetBar.ButtonItem bPointRemove;

		private DevComponents.DotNetBar.ButtonItem bDropRemove;

		private DevComponents.DotNetBar.DockSite dockSite2;

		private DevComponents.DotNetBar.DockSite dockSite1;

		private DevComponents.DotNetBar.DockSite dockSite3;

		private DevComponents.DotNetBar.DockSite dockSite4;

		private DevComponents.DotNetBar.DockSite dockSite5;

		private DevComponents.DotNetBar.DockSite dockSite6;

		private DevComponents.DotNetBar.DockSite dockSite7;

		private DevComponents.DotNetBar.DockSite dockSite8;

		private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager;

		private DevComponents.DotNetBar.RibbonBar ribbonBarLayout;

		private DevComponents.DotNetBar.ItemContainer itemContainerLayouts;

		private DevComponents.DotNetBar.CheckBoxItem opTakeoffLayout;

		private DevComponents.DotNetBar.CheckBoxItem opEstimatingLayout;

		private DevExpress.XtraTreeList.TreeList treeEstimatingItems;

		private DevComponents.DotNetBar.Bar barEstimatingItems;

		private DevComponents.DotNetBar.ButtonItem btEstimatingItemsPrint;

		private DevExpress.Utils.ImageCollection imageCollection;

		private QuoterPlan.ReportsControl reportsControl;

		private DevComponents.DotNetBar.ButtonItem btReportSettings;

		private DevComponents.DotNetBar.RibbonPanel ribbonPanelEstimating;

		private DevComponents.DotNetBar.RibbonTabItem ribbonTabEstimatingItems;

		private DevComponents.DotNetBar.RibbonPanel ribbonPanelTemplates;

		private DevComponents.DotNetBar.RibbonTabItemGroup ribbonTabItemDBManagement;

		private DevComponents.DotNetBar.RibbonTabItem ribbonTabTemplates;

		private DevComponents.DotNetBar.RibbonPanel ribbonPanelExtensions;

		private DevComponents.DotNetBar.RibbonTabItem ribbonTabExtensions;

		private DevComponents.DotNetBar.ButtonItem lblTrialMessage;

		private DevComponents.DotNetBar.RibbonBar ribbonTemplateCreate;

		private DevComponents.DotNetBar.ButtonItem btTemplateArea;

		private DevComponents.DotNetBar.ButtonItem btTemplatePerimeter;

		private DevComponents.DotNetBar.ButtonItem btTemplateLength;

		private DevComponents.DotNetBar.ButtonItem btTemplateCounter;

		private DevComponents.DotNetBar.RibbonBar ribbonTemplate;

		private DevComponents.DotNetBar.RibbonBar ribbonTemplateDatabase;

		private DevComponents.DotNetBar.ButtonItem btTemplateTradesPackages;

		private DevComponents.DotNetBar.ButtonItem btTemplateCompactDatabase;

		private DevComponents.DotNetBar.ButtonItem btTemplateModify;

		private DevComponents.DotNetBar.ButtonItem btTemplateDelete;

		private DevComponents.DotNetBar.ButtonItem btTemplateDuplicate;

		private DevComponents.DotNetBar.RibbonBar ribbonExtension;

		private DevComponents.DotNetBar.ButtonItem btExtensionModify;

		private DevComponents.DotNetBar.ButtonItem btExtensionDuplicate;

		private DevComponents.DotNetBar.ButtonItem btExtensionDelete;

		private DevComponents.DotNetBar.RibbonBar ribbonExtensionCreate;

		private DevComponents.DotNetBar.ButtonItem btExtensionArea;

		private DevComponents.DotNetBar.ButtonItem btExtensionPerimeter;

		private DevComponents.DotNetBar.ButtonItem btExtensionRuler;

		private DevComponents.DotNetBar.ButtonItem btExtensionCounter;

		private DevComponents.DotNetBar.RibbonBar ribbonExtensionDatabase;

		private DevComponents.DotNetBar.ButtonItem btExtensionTradesPackages;

		private DevComponents.DotNetBar.ButtonItem btExtensionCompactDatabase;

		private DevComponents.DotNetBar.RibbonBar ribbonEstimatingDatabase;

		private DevComponents.DotNetBar.ButtonItem btEstimatingTradesPackages;

		private DevComponents.DotNetBar.ButtonItem btEstimatingCompactDatabase;

		private DevComponents.DotNetBar.RibbonBar ribbonEstimating;

		private DevComponents.DotNetBar.ButtonItem btEstimatingModify;

		private DevComponents.DotNetBar.ButtonItem btEstimatingDuplicate;

		private DevComponents.DotNetBar.ButtonItem btEstimatingDelete;

		private DevComponents.DotNetBar.RibbonBar ribbonEstimatingNew;

		private DevComponents.DotNetBar.ButtonItem btEstimatingMaterial;

		private DevComponents.DotNetBar.ButtonItem btEstimatingLabour;

		private DevComponents.DotNetBar.ButtonItem btEstimatingEquipment;

		private DevComponents.DotNetBar.ButtonItem btEstimatingSubcontract;

		private DevComponents.DotNetBar.ElementStyle elementStyle7;

		private DevComponents.DotNetBar.ElementStyle elementStyle8;

		private DevComponents.DotNetBar.ElementStyle elementStyle5;

		private DevComponents.DotNetBar.ElementStyle elementStyle3;

		private DevComponents.DotNetBar.ElementStyle elementStyle4;

		private DevComponents.DotNetBar.PanelDockContainer panelDockEstimating;

		private DevComponents.DotNetBar.PanelDockContainer panelDockGroups;

		private DevComponents.DotNetBar.DockContainerItem dockContainerItemGroups;

		private DevComponents.DotNetBar.DockContainerItem dockContainerItemEstimating;

		private DevComponents.DotNetBar.Bar containerBarRecentPlans;

		private DevComponents.DotNetBar.PanelDockContainer panelDockRecentPlans;

		private DevComponents.DotNetBar.DockContainerItem dockContainerItemRecentPlans;

		private DevComponents.DotNetBar.Bar containerBarLayers;

		private DevComponents.DotNetBar.PanelDockContainer panelDockLayers;

		private DevComponents.DotNetBar.DockContainerItem dockContainerItemLayers;

		private DevComponents.DotNetBar.Bar containerBarNavigation;

		private DevComponents.DotNetBar.PanelDockContainer panelDockPreview;

		private DevComponents.DotNetBar.DockContainerItem dockContainerItemPreview;

		private DevComponents.DotNetBar.PanelDockContainer panelDockProperties;

		private DevComponents.DotNetBar.DockContainerItem dockContainerItemProperties;

		private DevComponents.DotNetBar.DockContainerItem dockContainerEstimating;

		private DevComponents.DotNetBar.Bar containerBarGroups;

		private DevComponents.DotNetBar.Bar containerBarProperties;

		private DevComponents.DotNetBar.Bar containerBarEstimating;

		private DevExpress.XtraTreeList.TreeList treeDBEstimatingItems;

		private DevComponents.DotNetBar.ButtonItem btReportExportToPDF;

		private DevComponents.DotNetBar.ButtonItem btEstimatingItemsExpandAll;

		private DevComponents.DotNetBar.ButtonItem btEstimatingItemsCollapseAll;

		private DevComponents.DotNetBar.ButtonItem btEstimatingSaveDatabase;

		private DevComponents.DotNetBar.LabelItem lblPanels;

		private DevComponents.DotNetBar.ButtonItem btResetDefaultPanelsLayout;

		private DevComponents.DotNetBar.ButtonItem btPlanDuplicate;

		private DevComponents.DotNetBar.ButtonItem btLanguageSpanish;

		private DevComponents.DotNetBar.ButtonItem btReportExportToCOffice;

		private DevComponents.DotNetBar.ButtonItem bToggleMeasures;

		private DevComponents.DotNetBar.LabelItem lblPersonalPreferences;

		private DevComponents.DotNetBar.ButtonItem btPersonalPreferences;

		private DevExpress.XtraTreeList.TreeList treeTemplatesLibrary;

		private DevComponents.DotNetBar.ButtonItem btLayersToggle;

		private DevComponents.DotNetBar.ButtonItem btGroupsToggle;

		private DevComponents.DotNetBar.ButtonItem btLayersMakeVisible;

		private DevComponents.DotNetBar.ButtonItem btLayersMakeInvisible;

		private DevComponents.DotNetBar.ButtonItem btGroupsMakeVisible;

		private DevComponents.DotNetBar.ButtonItem btGroupsMakeInvisible;

		private DevComponents.DotNetBar.ButtonItem btEstimatingItemsUpdatePrices;

		private DevComponents.DotNetBar.ButtonItem btEstimatingImportPrices;

		private DevComponents.DotNetBar.ButtonItem btImportationPreferences;

		private DevComponents.DotNetBar.ButtonItem buttonItem3;

		private DevComponents.DotNetBar.ButtonItem btSetDBReadOnly;

		private DevComponents.DotNetBar.ButtonItem btEnableAutoBackup;
	}
}
