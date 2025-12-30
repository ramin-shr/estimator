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
            this.components = new Container();

            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");
            this.ribbonControl = new RibbonControl();
            this.ribbonPanelEstimating = new RibbonPanel();
            this.ribbonEstimating = new RibbonBar();
            this.btEstimatingModify = new ButtonItem();
            this.btEstimatingDuplicate = new ButtonItem();
            this.btEstimatingDelete = new ButtonItem();
            this.ribbonEstimatingNew = new RibbonBar();
            this.btEstimatingMaterial = new ButtonItem();
            this.btEstimatingLabour = new ButtonItem();
            this.btEstimatingEquipment = new ButtonItem();
            this.btEstimatingSubcontract = new ButtonItem();
            this.ribbonEstimatingDatabase = new RibbonBar();
            this.btEstimatingTradesPackages = new ButtonItem();
            this.btEstimatingSaveDatabase = new ButtonItem();
            this.btEstimatingCompactDatabase = new ButtonItem();
            this.btEstimatingImportPrices = new ButtonItem();
            this.ribbonPanelReport = new RibbonPanel();
            this.ribbonExportReport = new RibbonBar();
            this.btReportExportToExcel = new ButtonItem();
            this.lblExportToExcelOptions = new LabelItem();
            this.buttonItem1 = new ButtonItem();
            this.btExportToExcelRawData = new ButtonItem();
            this.btExportToExcelFormattedData = new ButtonItem();
            this.btExportToExcelRawAndFormatted = new ButtonItem();
            this.btReportExportToCSV = new ButtonItem();
            this.btReportExportToXML = new ButtonItem();
            this.btReportExportToHTML = new ButtonItem();
            this.btReportExportToPDF = new ButtonItem();
            this.btReportExportToEE = new ButtonItem();
            this.btReportExportToCOffice = new ButtonItem();
            this.ribbonPrintReport = new RibbonBar();
            this.btReportPrint = new ButtonItem();
            this.btReportPrintPreview = new ButtonItem();
            this.btReportPrintSetup = new ButtonItem();
            this.ribbonReportOrder = new RibbonBar();
            this.btReportFilter = new ButtonItem();
            this.lblReportSystemType = new LabelItem();
            this.btReportScaleImperial = new ButtonItem();
            this.btReportScaleMetric = new ButtonItem();
            this.lblReportPrecision = new LabelItem();
            this.btReportScalePrecision64 = new ButtonItem();
            this.btReportScalePrecision32 = new ButtonItem();
            this.btReportScalePrecision16 = new ButtonItem();
            this.btReportScalePrecision8 = new ButtonItem();
            this.lblReportTheme = new LabelItem();
            this.btReportSettings = new ButtonItem();
            this.ribbonPanel = new RibbonPanel();
            this.ribbonBarPrintExport = new RibbonBar();
            this.btPrintPlan = new ButtonItem();
            this.lblPrintPlanOptions = new LabelItem();
            this.buttonItem2 = new ButtonItem();
            this.btPrintPlanFullSize = new ButtonItem();
            this.btPrintPlanWindow = new ButtonItem();
            this.btExportPlanToPDF = new ButtonItem();
            this.ribbonBarImage = new RibbonBar();
            this.btBrightnessContrast = new ButtonItem();
            this.btRotation = new ButtonItem();
            this.ribbonBarBrowse = new RibbonBar();
            this.itemContainerBrowse1 = new ItemContainer();
            this.lblBrowseGroup = new LabelItem();
            this.itemContainerBrowse3 = new ItemContainer();
            this.btBrowsePrevious = new ButtonItem();
            this.btBrowseNext = new ButtonItem();
            this.itemContainerBrowse2 = new ItemContainer();
            this.lblBrowseObjectType = new LabelItem();
            this.itemContainerBrowse4 = new ItemContainer();
            this.btBrowseObjectTypePrevious = new ButtonItem();
            this.btBrowseObjectTypeNext = new ButtonItem();
            this.ribbonBarZoom = new RibbonBar();
            this.btZoomToSelection = new ButtonItem();
            this.btZoomToWindow = new ButtonItem();
            this.btZoomActualSize = new ButtonItem();
            this.btZoom75 = new ButtonItem();
            this.btZoom50 = new ButtonItem();
            this.btZoom25 = new ButtonItem();
            this.btZoomIn = new ButtonItem();
            this.btZoomOut = new ButtonItem();
            this.btBookmarks = new ButtonItem();
            this.btZoomTo75 = new ButtonItem();
            this.btZoomTo50 = new ButtonItem();
            this.btZoomTo25 = new ButtonItem();
            this.btZoomTo150 = new ButtonItem();
            this.btZoomTo200 = new ButtonItem();
            this.buttonItem61 = new ButtonItem();
            this.btModifyBookmarks = new ButtonItem();
            this.ribbonBarAnnotations = new RibbonBar();
            this.btMarkZone = new ButtonItem();
            this.btInsertNote = new ButtonItem();
            this.btInsertPicture = new ButtonItem();
            this.ribbonBarTools = new RibbonBar();
            this.btToolSelection = new ButtonItem();
            this.btToolPan = new ButtonItem();
            this.btToolArea = new ButtonItem();
            this.lblNoArea = new LabelItem();
            this.lblAreaFilter = new LabelItem();
            this.itemContainerAreaFilter = new ItemContainer();
            this.txtAreaFilter = new TextBoxItem();
            this.btAreaFilterClear = new ButtonItem();
            this.lblAreaFilterPadding = new LabelItem();
            this.lblAreaGroups = new LabelItem();
            this.galleryAreaGroups = new GalleryContainer();
            this.lblAreaTemplates = new LabelItem();
            this.galleryAreaTemplates = new GalleryContainer();
            this.btToolPerimeter = new ButtonItem();
            this.lblNoPerimeter = new LabelItem();
            this.lblPerimeterFilter = new LabelItem();
            this.itemContainerPerimeterFilter = new ItemContainer();
            this.txtPerimeterFilter = new TextBoxItem();
            this.btPerimeterFilterClear = new ButtonItem();
            this.lblPerimeterGroups = new LabelItem();
            this.galleryPerimeterGroups = new GalleryContainer();
            this.lblPerimeterTemplates = new LabelItem();
            this.galleryPerimeterTemplates = new GalleryContainer();
            this.btToolRuler = new ButtonItem();
            this.lblNoDistance = new LabelItem();
            this.lblDistanceFilter = new LabelItem();
            this.itemContainerDistanceFilter = new ItemContainer();
            this.txtDistanceFilter = new TextBoxItem();
            this.btDistanceFilterClear = new ButtonItem();
            this.lblDistanceGroups = new LabelItem();
            this.galleryDistanceGroups = new GalleryContainer();
            this.lblDistanceTemplates = new LabelItem();
            this.galleryDistanceTemplates = new GalleryContainer();
            this.btToolCounter = new ButtonItem();
            this.lblNoCounter = new LabelItem();
            this.lblCounterFilter = new LabelItem();
            this.itemContainerCounterFilter = new ItemContainer();
            this.txtCounterFilter = new TextBoxItem();
            this.btCounterFilterClear = new ButtonItem();
            this.lblCounterGroups = new LabelItem();
            this.galleryCounterGroups = new GalleryContainer();
            this.lblCounterTemplates = new LabelItem();
            this.galleryCounterTemplates = new GalleryContainer();
            this.btToolAngle = new ButtonItem();
            this.ribbonBarScale = new RibbonBar();
            this.btScaleSet = new ButtonItem();
            this.lblSystemType = new LabelItem();
            this.btScaleImperial = new ButtonItem();
            this.btScaleMetric = new ButtonItem();
            this.lblPrecision = new LabelItem();
            this.btScalePrecision64 = new ButtonItem();
            this.btScalePrecision32 = new ButtonItem();
            this.btScalePrecision16 = new ButtonItem();
            this.btScalePrecision8 = new ButtonItem();
            this.ribbonBarEdit = new RibbonBar();
            this.btEditPaste = new ButtonItem();
            this.itemContainerEdit = new ItemContainer();
            this.btEditCut = new ButtonItem();
            this.btEditCopy = new ButtonItem();
            this.btEditDelete = new ButtonItem();
            this.itemContainerUndoRedo = new ItemContainer();
            this.btEditUndo = new ButtonItem();
            this.btEditRedo = new ButtonItem();
            this.btEditSendData = new ButtonItem();
            this.ribbonBarLayout = new RibbonBar();
            this.itemContainerLayouts = new ItemContainer();
            this.opTakeoffLayout = new CheckBoxItem();
            this.opEstimatingLayout = new CheckBoxItem();
            this.ribbonPanelTemplates = new RibbonPanel();
            this.ribbonTemplate = new RibbonBar();
            this.btTemplateModify = new ButtonItem();
            this.btTemplateDuplicate = new ButtonItem();
            this.btTemplateDelete = new ButtonItem();
            this.ribbonTemplateCreate = new RibbonBar();
            this.btTemplateArea = new ButtonItem();
            this.btTemplatePerimeter = new ButtonItem();
            this.btTemplateLength = new ButtonItem();
            this.btTemplateCounter = new ButtonItem();
            this.ribbonTemplateDatabase = new RibbonBar();
            this.btTemplateTradesPackages = new ButtonItem();
            this.btTemplateCompactDatabase = new ButtonItem();
            this.ribbonPanelPlans = new RibbonPanel();
            this.ribbonBarMultiPlans = new RibbonBar();
            this.btPlansPrint = new ButtonItem();
            this.btPlansExport = new ButtonItem();
            this.ribbonBarPlans = new RibbonBar();
            this.btPlanLoad = new ButtonItem();
            this.btPlanProperties = new ButtonItem();
            this.btPlanRemove = new ButtonItem();
            this.btPlanExport = new ButtonItem();
            this.btPlanDuplicate = new ButtonItem();
            this.ribbonBarPlansInsert = new RibbonBar();
            this.btPlanInsertFromPDF = new ButtonItem();
            this.iblImportDPI = new LabelItem();
            this.labelItem2 = new LabelItem();
            this.op172Dpi = new CheckBoxItem();
            this.op300Dpi = new CheckBoxItem();
            this.opOtherDpi = new CheckBoxItem();
            this.sliderDpi = new SliderItem();
            this.itemContainerDpi = new ItemContainer();
            this.lblDpi1 = new LabelItem();
            this.labelDpiPadding1 = new LabelItem();
            this.lblDpi2 = new LabelItem();
            this.iblImportColorManagement = new LabelItem();
            this.labelItem4 = new LabelItem();
            this.opConvertToColor = new CheckBoxItem();
            this.btPlanInsertFromImage = new ButtonItem();
            this.ribbonPanelExtensions = new RibbonPanel();
            this.ribbonExtension = new RibbonBar();
            this.btExtensionModify = new ButtonItem();
            this.btExtensionDuplicate = new ButtonItem();
            this.btExtensionDelete = new ButtonItem();
            this.ribbonExtensionCreate = new RibbonBar();
            this.btExtensionArea = new ButtonItem();
            this.btExtensionPerimeter = new ButtonItem();
            this.btExtensionRuler = new ButtonItem();
            this.btExtensionCounter = new ButtonItem();
            this.ribbonExtensionDatabase = new RibbonBar();
            this.btExtensionTradesPackages = new ButtonItem();
            this.btExtensionCompactDatabase = new ButtonItem();
            this.contextMenuBar1 = new ContextMenuBar();
            this.bEditPopup = new ButtonItem();
            this.bAutoAdjustToZone = new ButtonItem();
            this.bEditNote = new ButtonItem();
            this.bPointInsert = new ButtonItem();
            this.bPointRemove = new ButtonItem();
            this.bSetHeight = new ButtonItem();
            this.bGroupAddObject = new ButtonItem();
            this.bDeductionCreate = new ButtonItem();
            this.bDeductionsEdit = new ButtonItem();
            this.bPerimeterCreateFromArea = new ButtonItem();
            this.bOpeningCreateFromPosition = new ButtonItem();
            this.bOpeningDuplicate = new ButtonItem();
            this.bOpeningCreateFromSegment = new ButtonItem();
            this.bOpeningDelete = new ButtonItem();
            this.bDropInsert = new ButtonItem();
            this.bDropRemove = new ButtonItem();
            this.bPerimeterOpen = new ButtonItem();
            this.bPerimeterClose = new ButtonItem();
            this.bAngleDegreeType = new ButtonItem();
            this.bAngleSlopeType = new ButtonItem();
            this.bDeductionDuplicate = new ButtonItem();
            this.bCut = new ButtonItem();
            this.bCopy = new ButtonItem();
            this.bPaste = new ButtonItem();
            this.bDelete = new ButtonItem();
            this.bToggleMeasures = new ButtonItem();
            this.bZoomToObject = new ButtonItem();
            this.bZoomToGroup = new ButtonItem();
            this.bBringToFront = new ButtonItem();
            this.bSendToBack = new ButtonItem();
            this.bSelectGroup = new ButtonItem();
            this.bSelectThisGroup = new ButtonItem();
            this.bSelectThisGroup1 = new ButtonItem();
            this.bSelectObjectType = new ButtonItem();
            this.bSelectObjectType1 = new ButtonItem();
            this.bSelectAll = new ButtonItem();
            this.bUnselectAll = new ButtonItem();
            this.bLayerMoveTo = new ButtonItem();
            this.bLayerMoveTo1 = new ButtonItem();
            this.bGroupMoveTo = new ButtonItem();
            this.bGroupMoveTo1 = new ButtonItem();
            this.bGroupMoveToNew = new ButtonItem();
            this.ribbonTabStart = new RibbonTabItem();
            this.ribbonTabPlans = new RibbonTabItem();
            this.ribbonTabReport = new RibbonTabItem();
            this.ribbonTabEstimatingItems = new RibbonTabItem();
            this.ribbonTabItemDBManagement = new RibbonTabItemGroup();
            this.ribbonTabTemplates = new RibbonTabItem();
            this.ribbonTabExtensions = new RibbonTabItem();
            this.lblTrialMessage = new ButtonItem();
            this.btLicensing = new ButtonItem();
            this.btLicenseBuy = new ButtonItem();
            this.btLicenseActivate = new ButtonItem();
            this.btSettings = new ButtonItem();
            this.lblLanguage = new LabelItem();
            this.btLanguageEnglish = new ButtonItem();
            this.btLanguageFrench = new ButtonItem();
            this.btLanguageSpanish = new ButtonItem();
            this.lblScrollSpeed = new LabelItem();
            this.sliderScrollSpeed = new SliderItem();
            this.containerScroll = new ItemContainer();
            this.lblScrollFast = new LabelItem();
            this.lblScrollPadding = new LabelItem();
            this.lblScrollSlow = new LabelItem();
            this.lblDataFolder = new LabelItem();
            this.lblPersonalPreferences = new LabelItem();
            this.buttonItem3 = new ButtonItem();
            this.btSelectDataFolder = new ButtonItem();
            this.btPersonalPreferences = new ButtonItem();
            this.btImportationPreferences = new ButtonItem();
            this.btEnableAutoBackup = new ButtonItem();
            this.btSetDBReadOnly = new ButtonItem();
            this.lblTheme = new LabelItem();
            this.btStyleMetro = new ButtonItem();
            this.AppCommandTheme = new DevComponents.DotNetBar.Command(this.components);
            this.btStyleClassicBlue = new ButtonItem();
            this.btStyleClassicSilver = new ButtonItem();
            this.btStyleClassicBlack = new ButtonItem();
            this.btStyleClassicExecutive = new ButtonItem();
            this.btStyleRetroBlue = new ButtonItem();
            this.btStyleRetroSilver = new ButtonItem();
            this.btStyleRetroBlack = new ButtonItem();
            this.btStyleRetroGlass = new ButtonItem();
            this.btStyleModern = new ButtonItem();
            this.btSetThemeColor = new ColorPickerDropDown();
            this.lblPanels = new LabelItem();
            this.btResetDefaultPanelsLayout = new ButtonItem();
            this.btHelp = new ButtonItem();
            this.startButton = new Office2007StartButton();
            this.itemContainerFileMenu = new ItemContainer();
            this.itemContainerFileMenu2 = new ItemContainer();
            this.btProjectNew = new ButtonItem();
            this.btProjectOpen = new ButtonItem();
            this.btProjectSave = new ButtonItem();
            this.btProjectSaveAs = new ButtonItem();
            this.btProjectInfo = new ButtonItem();
            this.btProjectClose = new ButtonItem();
            this.btHelpContent = new ButtonItem();
            this.btHelpYoutube = new ButtonItem();
            this.btHelpAbout = new ButtonItem();
            this.btLicenseDeactivate = new ButtonItem();
            this.btExit = new ButtonItem();
            this.galleryRecentProjects = new GalleryContainer();
            this.lblFileRecentProjects = new LabelItem();
            this.btSave = new ButtonItem();
            this.btUndo = new ButtonItem();
            this.btRedo = new ButtonItem();
            this.galleryGroup1 = new GalleryGroup();
            this.barStatus = new Bar();
            this.lblStatus = new LabelItem();
            this.lblStatusBarPadding = new LabelItem();
            this.lblOrtho = new LabelItem();
            this.switchOrtho = new SwitchButtonItem();
            this.lblStatusBarPadding2 = new LabelItem();
            this.lblImageQuality = new LabelItem();
            this.qualitySlider = new SliderItem();
            this.lblStatusBarPadding3 = new LabelItem();
            this.lblZoom = new LabelItem();
            this.lblStatusPadding2 = new LabelItem();
            this.zoomSlider = new SliderItem();
            this.lstLayers = new AdvTree();
            this.columnLayerVisible = new ColumnHeader();
            this.columnLayerName = new ColumnHeader();
            this.columnLayerOpacity = new ColumnHeader();
            this.nodeConnector2 = new NodeConnector();
            this.elementStyle1 = new ElementStyle();
            this.elementStyle8 = new ElementStyle();
            this.elementStyle2 = new ElementStyle();
            this.barLayers = new Bar();
            this.btLayerAdd = new ButtonItem();
            this.btLayerRemove = new ButtonItem();
            this.btLayerRename = new ButtonItem();
            this.btLayerMoveUp = new ButtonItem();
            this.btLayerMoveDown = new ButtonItem();
            this.btLayerSaveList = new ButtonItem();
            this.btLayerSaveListAs = new ButtonItem();
            this.btLayerOpenList = new ButtonItem();
            this.btLayersToggle = new ButtonItem();
            this.btLayersMakeVisible = new ButtonItem();
            this.btLayersMakeInvisible = new ButtonItem();
            this.barDisplayResults = new Bar();
            this.lblDisplayResults = new LabelItem();
            this.btDisplayResultsForThisPlan = new ButtonItem();
            this.lblDisplayResultsPadding = new LabelItem();
            this.btDisplayResultsForAllPlans = new ButtonItem();
            this.tabProperties = new TabControl();
            this.tabControlPanel1 = new TabControlPanel();
            this.superTabProperties = new SuperTabControl();
            this.superTabControlPanel1 = new SuperTabControlPanel();
            this.gridObjectProperties = new AdvPropertyGrid();
            this.superTabItem1 = new SuperTabItem();
            this.tabItem1 = new TabItem(this.components);
            this.tabControlPanel2 = new TabControlPanel();
            this.extensionsManager = new ExtensionsManager();
            this.tabItem2 = new TabItem(this.components);
            this.cbObjects = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panControl = new PanControl();
            this.lstRecentPlans = new AdvTree();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.nodeConnector3 = new NodeConnector();
            this.elementStyle3 = new ElementStyle();
            this.elementStyle4 = new ElementStyle();
            this.barRecentPlans = new Bar();
            this.btPlanRename = new ButtonItem();
            this.treeObjects = new AdvTree();
            this.columnObjectIcon = new ColumnHeader();
            this.columnObjectName = new ColumnHeader();
            this.columnObjectInfo = new ColumnHeader();
            this.columnObjectColor = new ColumnHeader();
            this.columnObjectVisible = new ColumnHeader();
            this.columnObjectPadding = new ColumnHeader();
            this.nodeConnector1 = new NodeConnector();
            this.elementStyle7 = new ElementStyle();
            this.panelPlanName = new Panel();
            this.txtPlanName = new TextBoxEx();
            this.cbPlans = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.barGroups = new Bar();
            this.btGroupLocate = new ButtonItem();
            this.btZoomToObject = new ButtonItem();
            this.btGroupSelect = new ButtonItem();
            this.btGroupRemove = new ButtonItem();
            this.btGroupRename = new ButtonItem();
            this.btRenamePlan = new ButtonItem();
            this.btGroupsToggle = new ButtonItem();
            this.btGroupsMakeVisible = new ButtonItem();
            this.btGroupsMakeInvisible = new ButtonItem();
            this.checkBoxItem1 = new CheckBoxItem();
            this.sliderItem1 = new SliderItem();
            this.checkBoxItem2 = new CheckBoxItem();
            this.sliderItem2 = new SliderItem();
            this.timer1 = new Timer();
            this.itemContainer5 = new ItemContainer();
            this.galleryGroupArea = new GalleryGroup();
            this.galleryGroupPerimeter = new GalleryGroup();
            this.galleryGroupCounter = new GalleryGroup();
            this.tabItem3 = new TabItem(this.components);
            this.backgroundWorker = new BackgroundWorker();
            this.flowPlans = new FlowLayoutPanel();
            this.sliderItem4 = new SliderItem();
            this.itemContainerBrightness = new ItemContainer();
            this.lblBrightness = new LabelItem();
            this.sliderBrightness = new SliderItem();
            this.lblBrightnessContrastPadding1 = new LabelItem();
            this.itemContainerContrast = new ItemContainer();
            this.lblContrast = new LabelItem();
            this.sliderContrast = new SliderItem();
            this.ribbonBarBrightnessContrast = new RibbonBar();
            this.lblBrightnessContrastPadding2 = new LabelItem();
            this.lblBrightnessContrastSeparator = new LabelItem();
            this.lblBrightnessContrastPadding3 = new LabelItem();
            this.btBrightnessContrastApply = new ButtonItem();
            this.btBrightnessContrastCancel = new ButtonItem();
            this.btBrightnessContrastRestore = new ButtonItem();
            this.panelBrightnessContrast = new PanelEx();
            this.panelRotation = new PanelEx();
            this.ribbonBarRotation = new RibbonBar();
            this.btFlipHorizontally = new ButtonItem();
            this.btFlipVertically = new ButtonItem();
            this.btRotateLeft = new ButtonItem();
            this.btRotateRight = new ButtonItem();
            this.lblBarRotationPadding1 = new LabelItem();
            this.lblBarRotationSeparator = new LabelItem();
            this.lblBarRotationPadding2 = new LabelItem();
            this.btRotationApply = new ButtonItem();
            this.btRotationCancel = new ButtonItem();
            this.webBrowser = new WebBrowser();
            this.comboItem1 = new ComboItem();
            this.comboItem2 = new ComboItem();
            this.comboItem3 = new ComboItem();
            this.comboItem4 = new ComboItem();
            this.comboItem5 = new ComboItem();
            this.comboItem6 = new ComboItem();
            this.labelItem1 = new LabelItem();
            this.panelWelcome = new Panel();
            this.panelWelcomeMenu = new PanelEx();
            this.lblRecentProjects = new LabelEx();
            this.btNew = new ButtonX();
            this.lstRecentProjects = new AdvTree();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.nodeConnector4 = new NodeConnector();
            this.elementStyle5 = new ElementStyle();
            this.elementStyle6 = new ElementStyle();
            this.btOpen = new ButtonX();
            this.picWelcome = new PictureBox();
            this.superTooltip = new SuperTooltip();
            this.panelPlansAction = new PanelEx();
            this.ribbonBarPlansAction = new RibbonBar();
            this.progressPlansAction = new CircularProgressItem();
            this.itemContainerExportType = new ItemContainer();
            this.lblBarPlansActionPadding3 = new LabelItem();
            this.checkBoxExportSingleFile = new CheckBoxItem();
            this.checkBoxExportMultiFiles = new CheckBoxItem();
            this.lblBarPlansActionPadding4 = new LabelItem();
            this.btPlansActionSelectAll = new ButtonItem();
            this.btPlansActionSelectNone = new ButtonItem();
            this.lblBarPlansActionPadding1 = new LabelItem();
            this.lblPlansActionSeparator = new LabelItem();
            this.lblBarPlansActionPadding2 = new LabelItem();
            this.btPlansActionApply = new ButtonItem();
            this.btPlansActionCancel = new ButtonItem();
            this.mainControl = new MainControl();
            this.dotNetBarManager = new DotNetBarManager(this.components);
            this.dockSite4 = new DockSite();
            this.dockSite1 = new DockSite();
            this.containerBarLayers = new Bar();
            this.panelDockLayers = new PanelDockContainer();
            this.dockContainerItemLayers = new DockContainerItem();
            this.containerBarNavigation = new Bar();
            this.panelDockPreview = new PanelDockContainer();
            this.dockContainerItemPreview = new DockContainerItem();
            this.containerBarProperties = new Bar();
            this.panelDockProperties = new PanelDockContainer();
            this.dockContainerItemProperties = new DockContainerItem();
            this.dockSite2 = new DockSite();
            this.containerBarGroups = new Bar();
            this.panelDockGroups = new PanelDockContainer();
            this.dockContainerItemGroups = new DockContainerItem();
            this.containerBarRecentPlans = new Bar();
            this.panelDockRecentPlans = new PanelDockContainer();
            this.dockContainerItemRecentPlans = new DockContainerItem();
            this.containerBarEstimating = new Bar();
            this.panelDockEstimating = new PanelDockContainer();
            this.treeEstimatingItems = new TreeList();
            this.barEstimatingItems = new Bar();
            this.btEstimatingItemsExpandAll = new ButtonItem();
            this.btEstimatingItemsCollapseAll = new ButtonItem();
            this.btEstimatingItemsUpdatePrices = new ButtonItem();
            this.btEstimatingItemsPrint = new ButtonItem();
            this.dockContainerItemEstimating = new DockContainerItem();
            this.dockSite8 = new DockSite();
            this.dockSite5 = new DockSite();
            this.dockSite6 = new DockSite();
            this.dockSite7 = new DockSite();
            this.dockSite3 = new DockSite();
            this.lblNoPlan = new LabelEx();
            this.imageCollection = new ImageCollection(this.components);
            this.reportsControl = new ReportsControl();
            this.dockContainerEstimating = new DockContainerItem();
            this.treeDBEstimatingItems = new TreeList();
            this.treeTemplatesLibrary = new TreeList();
            this.ribbonControl.SuspendLayout();
            this.ribbonPanelEstimating.SuspendLayout();
            this.ribbonPanelReport.SuspendLayout();
            this.ribbonPanel.SuspendLayout();
            this.ribbonPanelTemplates.SuspendLayout();
            this.ribbonPanelPlans.SuspendLayout();
            this.ribbonPanelExtensions.SuspendLayout();
            ((ISupportInitialize)this.contextMenuBar1).BeginInit();
            ((ISupportInitialize)this.barStatus).BeginInit();
            ((ISupportInitialize)this.lstLayers).BeginInit();
            ((ISupportInitialize)this.barLayers).BeginInit();
            ((ISupportInitialize)this.barDisplayResults).BeginInit();
            ((ISupportInitialize)this.tabProperties).BeginInit();
            this.tabProperties.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((ISupportInitialize)this.superTabProperties).BeginInit();
            this.superTabProperties.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            ((ISupportInitialize)this.gridObjectProperties).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            ((ISupportInitialize)this.lstRecentPlans).BeginInit();
            ((ISupportInitialize)this.barRecentPlans).BeginInit();
            ((ISupportInitialize)this.treeObjects).BeginInit();
            this.panelPlanName.SuspendLayout();
            ((ISupportInitialize)this.barGroups).BeginInit();
            ((ISupportInitialize)this.timer1).BeginInit();
            this.panelBrightnessContrast.SuspendLayout();
            this.panelRotation.SuspendLayout();
            this.panelWelcome.SuspendLayout();
            this.panelWelcomeMenu.SuspendLayout();
            ((ISupportInitialize)this.lstRecentProjects).BeginInit();
            ((ISupportInitialize)this.picWelcome).BeginInit();
            this.panelPlansAction.SuspendLayout();
            this.dockSite1.SuspendLayout();
            ((ISupportInitialize)this.containerBarLayers).BeginInit();
            this.containerBarLayers.SuspendLayout();
            this.panelDockLayers.SuspendLayout();
            ((ISupportInitialize)this.containerBarNavigation).BeginInit();
            this.containerBarNavigation.SuspendLayout();
            this.panelDockPreview.SuspendLayout();
            ((ISupportInitialize)this.containerBarProperties).BeginInit();
            this.containerBarProperties.SuspendLayout();
            this.panelDockProperties.SuspendLayout();
            this.dockSite2.SuspendLayout();
            ((ISupportInitialize)this.containerBarGroups).BeginInit();
            this.containerBarGroups.SuspendLayout();
            this.panelDockGroups.SuspendLayout();
            ((ISupportInitialize)this.containerBarRecentPlans).BeginInit();
            this.containerBarRecentPlans.SuspendLayout();
            this.panelDockRecentPlans.SuspendLayout();
            ((ISupportInitialize)this.containerBarEstimating).BeginInit();
            this.containerBarEstimating.SuspendLayout();
            this.panelDockEstimating.SuspendLayout();
            ((ISupportInitialize)this.treeEstimatingItems).BeginInit();
            ((ISupportInitialize)this.barEstimatingItems).BeginInit();
            ((ISupportInitialize)this.imageCollection).BeginInit();
            ((ISupportInitialize)this.treeDBEstimatingItems).BeginInit();
            ((ISupportInitialize)this.treeTemplatesLibrary).BeginInit();
            base.SuspendLayout();
            this.ribbonControl.AutoExpand = false;
            this.ribbonControl.AutoKeyboardExpand = false;
            this.ribbonControl.BackColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.ribbonControl.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonControl.CanCustomize = false;
            this.ribbonControl.CaptionVisible = true;
            this.ribbonControl.Controls.Add(this.ribbonPanelEstimating);
            this.ribbonControl.Controls.Add(this.ribbonPanelReport);
            this.ribbonControl.Controls.Add(this.ribbonPanel);
            this.ribbonControl.Controls.Add(this.ribbonPanelTemplates);
            this.ribbonControl.Controls.Add(this.ribbonPanelPlans);
            this.ribbonControl.Controls.Add(this.ribbonPanelExtensions);
            resources.ApplyResources(this.ribbonControl, "ribbonControl");
            this.ribbonControl.ForeColor = Color.Black;
            this.ribbonControl.GlobalContextMenuBar = this.contextMenuBar1;
            this.ribbonControl.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
                this.ribbonTabStart,
                this.ribbonTabPlans,
                this.ribbonTabReport,
                this.ribbonTabEstimatingItems,
                this.ribbonTabTemplates,
                this.ribbonTabExtensions,
                this.lblTrialMessage,
                this.btLicensing,
                this.btSettings,
                this.btHelp
            });
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.QuickToolbarItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
                this.startButton,
                this.btSave,
                this.btUndo,
                this.btRedo
            });
            this.ribbonControl.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl.SystemText.MaximizeRibbonText = resources.GetString("ribbonControl.SystemText.MaximizeRibbonText");
            this.ribbonControl.SystemText.MinimizeRibbonText = resources.GetString("ribbonControl.SystemText.MinimizeRibbonText");
            this.ribbonControl.SystemText.QatAddItemText = resources.GetString("ribbonControl.SystemText.QatAddItemText");
            this.ribbonControl.SystemText.QatCustomizeMenuLabel = resources.GetString("ribbonControl.SystemText.QatCustomizeMenuLabel");
            this.ribbonControl.SystemText.QatCustomizeText = resources.GetString("ribbonControl.SystemText.QatCustomizeText");
            this.ribbonControl.SystemText.QatDialogAddButton = resources.GetString("ribbonControl.SystemText.QatDialogAddButton");
            this.ribbonControl.SystemText.QatDialogCancelButton = resources.GetString("ribbonControl.SystemText.QatDialogCancelButton");
            this.ribbonControl.SystemText.QatDialogCaption = resources.GetString("ribbonControl.SystemText.QatDialogCaption");
            this.ribbonControl.SystemText.QatDialogCategoriesLabel = resources.GetString("ribbonControl.SystemText.QatDialogCategoriesLabel");
            this.ribbonControl.SystemText.QatDialogOkButton = resources.GetString("ribbonControl.SystemText.QatDialogOkButton");
            this.ribbonControl.SystemText.QatDialogPlacementCheckbox = resources.GetString("ribbonControl.SystemText.QatDialogPlacementCheckbox");
            this.ribbonControl.SystemText.QatDialogRemoveButton = resources.GetString("ribbonControl.SystemText.QatDialogRemoveButton");
            this.ribbonControl.SystemText.QatPlaceAboveRibbonText = resources.GetString("ribbonControl.SystemText.QatPlaceAboveRibbonText");
            this.ribbonControl.SystemText.QatPlaceBelowRibbonText = resources.GetString("ribbonControl.SystemText.QatPlaceBelowRibbonText");
            this.ribbonControl.SystemText.QatRemoveItemText = resources.GetString("ribbonControl.SystemText.QatRemoveItemText");
            this.ribbonControl.TabGroupHeight = 14;
            this.ribbonControl.TabGroups.AddRange(new RibbonTabItemGroup[] { this.ribbonTabItemDBManagement });
            this.ribbonControl.TabGroupsVisible = true;
            this.ribbonControl.UseCustomizeDialog = false;
            this.ribbonControl.SelectedRibbonTabChanged += new EventHandler(this.ribbonControl_SelectedRibbonTabChanged);
            this.ribbonPanelEstimating.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimating);
            this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimatingNew);
            this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimatingDatabase);
            resources.ApplyResources(this.ribbonPanelEstimating, "ribbonPanelEstimating");
            this.ribbonPanelEstimating.Name = "ribbonPanelEstimating";
            this.ribbonPanelEstimating.Style.CornerType = eCornerType.Square;
            this.ribbonPanelEstimating.StyleMouseDown.CornerType = eCornerType.Square;
            this.ribbonPanelEstimating.StyleMouseOver.CornerType = eCornerType.Square;
            this.ribbonEstimating.AutoOverflowEnabled = false;
            this.ribbonEstimating.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonEstimating.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonEstimating.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonEstimating, "ribbonEstimating");
            this.ribbonEstimating.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
                this.btEstimatingModify,
                this.btEstimatingDuplicate,
                this.btEstimatingDelete
            });
            this.ribbonEstimating.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonEstimating.Name = "ribbonEstimating";
            this.ribbonEstimating.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonEstimating.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonEstimating.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btEstimatingModify.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingModify.CanCustomize = false;
            this.btEstimatingModify.Image = Resources.properties;
            this.btEstimatingModify.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingModify.ImagePosition = eImagePosition.Top;
            this.btEstimatingModify.Name = "btEstimatingModify";
            resources.ApplyResources(this.btEstimatingModify, "btEstimatingModify");
            this.btEstimatingModify.Click += new EventHandler(this.btEstimatingModify_Click);
            this.btEstimatingDuplicate.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingDuplicate.CanCustomize = false;
            this.btEstimatingDuplicate.Image = (Image)resources.GetObject("btEstimatingDuplicate.Image");
            this.btEstimatingDuplicate.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingDuplicate.ImagePosition = eImagePosition.Top;
            this.btEstimatingDuplicate.Name = "btEstimatingDuplicate";
            resources.ApplyResources(this.btEstimatingDuplicate, "btEstimatingDuplicate");
            this.btEstimatingDuplicate.Click += new EventHandler(this.btEstimatingDuplicate_Click);
            this.btEstimatingDelete.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingDelete.CanCustomize = false;
            this.btEstimatingDelete.Image = (Image)resources.GetObject("btEstimatingDelete.Image");
            this.btEstimatingDelete.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingDelete.ImagePosition = eImagePosition.Top;
            this.btEstimatingDelete.Name = "btEstimatingDelete";
            resources.ApplyResources(this.btEstimatingDelete, "btEstimatingDelete");
            this.btEstimatingDelete.Click += new EventHandler(this.btEstimatingDelete_Click);
            this.ribbonEstimatingNew.AutoOverflowEnabled = false;
            this.ribbonEstimatingNew.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonEstimatingNew.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonEstimatingNew.CanCustomize = false;
            this.ribbonEstimatingNew.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonEstimatingNew, "ribbonEstimatingNew");
            this.ribbonEstimatingNew.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
                this.btEstimatingMaterial,
                this.btEstimatingLabour,
                this.btEstimatingEquipment,
                this.btEstimatingSubcontract
            });
            this.ribbonEstimatingNew.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonEstimatingNew.Name = "ribbonEstimatingNew";
            this.ribbonEstimatingNew.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonEstimatingNew.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonEstimatingNew.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btEstimatingMaterial.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingMaterial.CanCustomize = false;
            this.btEstimatingMaterial.Image = (Image)resources.GetObject("btEstimatingMaterial.Image");
            this.btEstimatingMaterial.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingMaterial.ImagePosition = eImagePosition.Top;
            this.btEstimatingMaterial.Name = "btEstimatingMaterial";
            resources.ApplyResources(this.btEstimatingMaterial, "btEstimatingMaterial");
            this.btEstimatingMaterial.Click += new EventHandler(this.btEstimatingMaterial_Click);
            this.btEstimatingLabour.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingLabour.CanCustomize = false;
            this.btEstimatingLabour.Image = (Image)resources.GetObject("btEstimatingLabour.Image");
            this.btEstimatingLabour.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingLabour.ImagePosition = eImagePosition.Top;
            this.btEstimatingLabour.Name = "btEstimatingLabour";
            resources.ApplyResources(this.btEstimatingLabour, "btEstimatingLabour");
            this.btEstimatingLabour.Click += new EventHandler(this.btEstimatingLabour_Click);
            this.btEstimatingEquipment.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingEquipment.CanCustomize = false;
            this.btEstimatingEquipment.Image = (Image)resources.GetObject("btEstimatingEquipment.Image");
            this.btEstimatingEquipment.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingEquipment.ImagePosition = eImagePosition.Top;
            this.btEstimatingEquipment.Name = "btEstimatingEquipment";
            resources.ApplyResources(this.btEstimatingEquipment, "btEstimatingEquipment");
            this.btEstimatingEquipment.Click += new EventHandler(this.btEstimatingEquipment_Click);
            this.btEstimatingSubcontract.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingSubcontract.CanCustomize = false;
            this.btEstimatingSubcontract.Image = (Image)resources.GetObject("btEstimatingSubcontract.Image");
            this.btEstimatingSubcontract.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingSubcontract.ImagePosition = eImagePosition.Top;
            this.btEstimatingSubcontract.Name = "btEstimatingSubcontract";
            resources.ApplyResources(this.btEstimatingSubcontract, "btEstimatingSubcontract");
            this.btEstimatingSubcontract.Click += new EventHandler(this.btEstimatingSubcontract_Click);
            this.ribbonEstimatingDatabase.AutoOverflowEnabled = false;
            this.ribbonEstimatingDatabase.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonEstimatingDatabase.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonEstimatingDatabase.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonEstimatingDatabase, "ribbonEstimatingDatabase");
            this.ribbonEstimatingDatabase.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btEstimatingTradesPackages,
    this.btEstimatingSaveDatabase,
    this.btEstimatingCompactDatabase,
    this.btEstimatingImportPrices
            });
            this.ribbonEstimatingDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonEstimatingDatabase.Name = "ribbonEstimatingDatabase";
            this.ribbonEstimatingDatabase.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonEstimatingDatabase.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonEstimatingDatabase.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btEstimatingTradesPackages.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingTradesPackages.Image = (Image)resources.GetObject("btEstimatingTradesPackages.Image");
            this.btEstimatingTradesPackages.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingTradesPackages.ImagePaddingHorizontal = 10;
            this.btEstimatingTradesPackages.ImagePaddingVertical = 5;
            this.btEstimatingTradesPackages.ImagePosition = eImagePosition.Top;
            this.btEstimatingTradesPackages.Name = "btEstimatingTradesPackages";
            resources.ApplyResources(this.btEstimatingTradesPackages, "btEstimatingTradesPackages");
            this.btEstimatingTradesPackages.Visible = false;
            this.btEstimatingSaveDatabase.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingSaveDatabase.Image = (Image)resources.GetObject("btEstimatingSaveDatabase.Image");
            this.btEstimatingSaveDatabase.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingSaveDatabase.ImagePaddingVertical = 5;
            this.btEstimatingSaveDatabase.ImagePosition = eImagePosition.Top;
            this.btEstimatingSaveDatabase.Name = "btEstimatingSaveDatabase";
            resources.ApplyResources(this.btEstimatingSaveDatabase, "btEstimatingSaveDatabase");
            this.btEstimatingSaveDatabase.Click += new EventHandler(this.btEstimatingSaveDatabase_Click);
            this.btEstimatingCompactDatabase.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingCompactDatabase.Image = (Image)resources.GetObject("btEstimatingCompactDatabase.Image");
            this.btEstimatingCompactDatabase.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingCompactDatabase.ImagePaddingVertical = 5;
            this.btEstimatingCompactDatabase.ImagePosition = eImagePosition.Top;
            this.btEstimatingCompactDatabase.Name = "btEstimatingCompactDatabase";
            resources.ApplyResources(this.btEstimatingCompactDatabase, "btEstimatingCompactDatabase");
            this.btEstimatingCompactDatabase.Visible = false;
            this.btEstimatingImportPrices.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEstimatingImportPrices.Image = (Image)resources.GetObject("btEstimatingImportPrices.Image");
            this.btEstimatingImportPrices.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEstimatingImportPrices.ImagePaddingVertical = 5;
            this.btEstimatingImportPrices.ImagePosition = eImagePosition.Top;
            this.btEstimatingImportPrices.Name = "btEstimatingImportPrices";
            resources.ApplyResources(this.btEstimatingImportPrices, "btEstimatingImportPrices");
            this.btEstimatingImportPrices.Click += new EventHandler(this.btEstimatingImportPrices_Click);
            this.ribbonPanelReport.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelReport.Controls.Add(this.ribbonExportReport);
            this.ribbonPanelReport.Controls.Add(this.ribbonPrintReport);
            this.ribbonPanelReport.Controls.Add(this.ribbonReportOrder);
            resources.ApplyResources(this.ribbonPanelReport, "ribbonPanelReport");
            this.ribbonPanelReport.Name = "ribbonPanelReport";
            this.ribbonPanelReport.Style.CornerType = eCornerType.Square;
            this.ribbonPanelReport.StyleMouseDown.CornerType = eCornerType.Square;
            this.ribbonPanelReport.StyleMouseOver.CornerType = eCornerType.Square;
            this.ribbonExportReport.AutoOverflowEnabled = false;
            this.ribbonExportReport.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonExportReport.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonExportReport.CanCustomize = false;
            this.ribbonExportReport.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonExportReport, "ribbonExportReport");
            this.ribbonExportReport.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btReportExportToExcel,
    this.btReportExportToCSV,
    this.btReportExportToXML,
    this.btReportExportToHTML,
    this.btReportExportToPDF,
    this.btReportExportToEE,
    this.btReportExportToCOffice
            });

            this.ribbonExportReport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExportReport.Name = "ribbonExportReport";
            this.ribbonExportReport.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExportReport.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonExportReport.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btReportExportToExcel.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportExportToExcel.CanCustomize = false;
            this.btReportExportToExcel.Image = (Image)resources.GetObject("btReportExportToExcel.Image");
            this.btReportExportToExcel.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportExportToExcel.ImagePosition = eImagePosition.Top;
            this.btReportExportToExcel.Name = "btReportExportToExcel";
            this.btReportExportToExcel.ShowSubItems = false;
            this.btReportExportToExcel.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblExportToExcelOptions,
    this.btExportToExcelRawData,
    this.btExportToExcelFormattedData,
    this.btExportToExcelRawAndFormatted
            });

            resources.ApplyResources(this.btReportExportToExcel, "btReportExportToExcel");
            this.btReportExportToExcel.Click += new EventHandler(this.btReportExportToExcel_Click);
            this.lblExportToExcelOptions.BackColor = Color.FromArgb(221, 231, 238);
            this.lblExportToExcelOptions.BorderSide = eBorderSide.None;
            this.lblExportToExcelOptions.CanCustomize = false;
            this.lblExportToExcelOptions.Name = "lblExportToExcelOptions";
            this.lblExportToExcelOptions.PaddingBottom = 3;
            this.lblExportToExcelOptions.PaddingLeft = 10;
            this.lblExportToExcelOptions.PaddingTop = 3;
            this.lblExportToExcelOptions.SingleLineColor = Color.FromArgb(197, 197, 197);
            this.lblExportToExcelOptions.SubItems.AddRange(new BaseItem[] { this.buttonItem1 });
            resources.ApplyResources(this.lblExportToExcelOptions, "lblExportToExcelOptions");
            this.lblExportToExcelOptions.Visible = false;
            this.buttonItem1.CanCustomize = false;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = Resources.Impérial;
            this.btExportToExcelRawData.AutoCheckOnClick = true;
            this.btExportToExcelRawData.CanCustomize = false;
            this.btExportToExcelRawData.Name = "btExportToExcelRawData";
            this.btExportToExcelRawData.OptionGroup = "ExportToExcelType";
            resources.ApplyResources(this.btExportToExcelRawData, "btExportToExcelRawData");
            this.btExportToExcelRawData.Visible = false;
            this.btExportToExcelRawData.Click += new EventHandler(this.btExportToExcelType_Click);
            this.btExportToExcelFormattedData.AutoCheckOnClick = true;
            this.btExportToExcelFormattedData.CanCustomize = false;
            this.btExportToExcelFormattedData.Name = "btExportToExcelFormattedData";
            this.btExportToExcelFormattedData.OptionGroup = "ExportToExcelType";
            resources.ApplyResources(this.btExportToExcelFormattedData, "btExportToExcelFormattedData");
            this.btExportToExcelFormattedData.Visible = false;
            this.btExportToExcelFormattedData.Click += new EventHandler(this.btExportToExcelType_Click);
            this.btExportToExcelRawAndFormatted.AutoCheckOnClick = true;
            this.btExportToExcelRawAndFormatted.CanCustomize = false;
            this.btExportToExcelRawAndFormatted.Name = "btExportToExcelRawAndFormatted";
            this.btExportToExcelRawAndFormatted.OptionGroup = "ExportToExcelType";
            resources.ApplyResources(this.btExportToExcelRawAndFormatted, "btExportToExcelRawAndFormatted");
            this.btExportToExcelRawAndFormatted.Visible = false;
            this.btExportToExcelRawAndFormatted.Click += new EventHandler(this.btExportToExcelType_Click);
            this.btReportExportToCSV.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportExportToCSV.Image = (Image)resources.GetObject("btReportExportToCSV.Image");
            this.btReportExportToCSV.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportExportToCSV.ImagePosition = eImagePosition.Top;
            this.btReportExportToCSV.Name = "btReportExportToCSV";
            resources.ApplyResources(this.btReportExportToCSV, "btReportExportToCSV");
            this.btReportExportToCSV.Visible = false;
            this.btReportExportToCSV.Click += new EventHandler(this.btReportExportToCSV_Click);
            this.btReportExportToXML.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportExportToXML.Image = (Image)resources.GetObject("btReportExportToXML.Image");
            this.btReportExportToXML.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportExportToXML.ImagePosition = eImagePosition.Top;
            this.btReportExportToXML.Name = "btReportExportToXML";
            resources.ApplyResources(this.btReportExportToXML, "btReportExportToXML");
            this.btReportExportToXML.Click += new EventHandler(this.btReportExportToXML_Click);
            this.btReportExportToHTML.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportExportToHTML.Image = (Image)resources.GetObject("btReportExportToHTML.Image");
            this.btReportExportToHTML.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportExportToHTML.ImagePosition = eImagePosition.Top;
            this.btReportExportToHTML.Name = "btReportExportToHTML";
            resources.ApplyResources(this.btReportExportToHTML, "btReportExportToHTML");
            this.btReportExportToHTML.Click += new EventHandler(this.btReportExportToHTML_Click);
            this.btReportExportToPDF.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportExportToPDF.Image = Resources.file_pdf_40x40;
            this.btReportExportToPDF.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportExportToPDF.ImagePaddingVertical = 5;
            this.btReportExportToPDF.ImagePosition = eImagePosition.Top;
            this.btReportExportToPDF.Name = "btReportExportToPDF";
            resources.ApplyResources(this.btReportExportToPDF, "btReportExportToPDF");
            this.btReportExportToPDF.Click += new EventHandler(this.btReportExportToPDF_Click);
            this.btReportExportToEE.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportExportToEE.Image = (Image)resources.GetObject("btReportExportToEE.Image");
            this.btReportExportToEE.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportExportToEE.ImagePosition = eImagePosition.Top;
            this.btReportExportToEE.Name = "btReportExportToEE";
            resources.ApplyResources(this.btReportExportToEE, "btReportExportToEE");
            this.btReportExportToEE.Visible = false;
            this.btReportExportToCOffice.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportExportToCOffice.Image = (Image)resources.GetObject("btReportExportToCOffice.Image");
            this.btReportExportToCOffice.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportExportToCOffice.ImagePosition = eImagePosition.Top;
            this.btReportExportToCOffice.Name = "btReportExportToCOffice";
            resources.ApplyResources(this.btReportExportToCOffice, "btReportExportToCOffice");
            this.btReportExportToCOffice.Visible = false;
            this.btReportExportToCOffice.Click += new EventHandler(this.btReportExportToCOffice_Click);
            this.ribbonPrintReport.AutoOverflowEnabled = false;
            this.ribbonPrintReport.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonPrintReport.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonPrintReport.CanCustomize = false;
            this.ribbonPrintReport.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonPrintReport, "ribbonPrintReport");
            this.ribbonPrintReport.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btReportPrint,
    this.btReportPrintPreview,
    this.btReportPrintSetup
            });

            this.ribbonPrintReport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonPrintReport.Name = "ribbonPrintReport";
            this.ribbonPrintReport.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPrintReport.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonPrintReport.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btReportPrint.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportPrint.Image = (Image)resources.GetObject("btReportPrint.Image");
            this.btReportPrint.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportPrint.ImagePosition = eImagePosition.Top;
            this.btReportPrint.Name = "btReportPrint";
            resources.ApplyResources(this.btReportPrint, "btReportPrint");
            this.btReportPrint.Click += new EventHandler(this.btReportPrint_Click);
            this.btReportPrintPreview.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportPrintPreview.Image = (Image)resources.GetObject("btReportPrintPreview.Image");
            this.btReportPrintPreview.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportPrintPreview.ImagePosition = eImagePosition.Top;
            this.btReportPrintPreview.Name = "btReportPrintPreview";
            resources.ApplyResources(this.btReportPrintPreview, "btReportPrintPreview");
            this.btReportPrintPreview.Visible = false;
            this.btReportPrintPreview.Click += new EventHandler(this.btReportPrintPreview_Click);
            this.btReportPrintSetup.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportPrintSetup.Image = (Image)resources.GetObject("btReportPrintSetup.Image");
            this.btReportPrintSetup.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportPrintSetup.ImagePosition = eImagePosition.Top;
            this.btReportPrintSetup.Name = "btReportPrintSetup";
            resources.ApplyResources(this.btReportPrintSetup, "btReportPrintSetup");
            this.btReportPrintSetup.Visible = false;
            this.btReportPrintSetup.Click += new EventHandler(this.btReportPrintSetup_Click);
            this.ribbonReportOrder.AutoOverflowEnabled = false;
            this.ribbonReportOrder.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonReportOrder.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonReportOrder.CanCustomize = false;
            this.ribbonReportOrder.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonReportOrder, "ribbonReportOrder");
            this.ribbonReportOrder.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btReportFilter,
    this.btReportSettings
            });

            this.ribbonReportOrder.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonReportOrder.Name = "ribbonReportOrder";
            this.ribbonReportOrder.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonReportOrder.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonReportOrder.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btReportFilter.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportFilter.CanCustomize = false;
            this.btReportFilter.Image = (Image)resources.GetObject("btReportFilter.Image");
            this.btReportFilter.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportFilter.ImagePosition = eImagePosition.Top;
            this.btReportFilter.Name = "btReportFilter";
            this.btReportFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblReportSystemType,
    this.btReportScaleImperial,
    this.btReportScaleMetric,
    this.lblReportPrecision,
    this.btReportScalePrecision64,
    this.btReportScalePrecision32,
    this.btReportScalePrecision16,
    this.btReportScalePrecision8,
    this.lblReportTheme
            });

            resources.ApplyResources(this.btReportFilter, "btReportFilter");
            this.btReportFilter.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.btReportEdit_PopupOpen);
            this.btReportFilter.Click += new EventHandler(this.btReportEdit_Click);
            this.lblReportSystemType.BackColor = Color.FromArgb(221, 231, 238);
            this.lblReportSystemType.BorderSide = eBorderSide.None;
            this.lblReportSystemType.CanCustomize = false;
            this.lblReportSystemType.Name = "lblReportSystemType";
            this.lblReportSystemType.PaddingBottom = 3;
            this.lblReportSystemType.PaddingLeft = 10;
            this.lblReportSystemType.PaddingTop = 3;
            this.lblReportSystemType.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblReportSystemType, "lblReportSystemType");
            this.btReportScaleImperial.CanCustomize = false;
            this.btReportScaleImperial.Name = "btReportScaleImperial";
            this.btReportScaleImperial.Text = Resources.Impérial;
            this.btReportScaleImperial.Click += new EventHandler(this.btReportScaleImperial_Click);
            this.btReportScaleMetric.CanCustomize = false;
            this.btReportScaleMetric.Name = "btReportScaleMetric";
            this.btReportScaleMetric.Text = Resources.Métrique;
            this.btReportScaleMetric.Click += new EventHandler(this.btReportScaleMetric_Click);
            this.lblReportPrecision.BackColor = Color.FromArgb(221, 231, 238);
            this.lblReportPrecision.BorderSide = eBorderSide.None;
            this.lblReportPrecision.CanCustomize = false;
            this.lblReportPrecision.Name = "lblReportPrecision";
            this.lblReportPrecision.PaddingBottom = 3;
            this.lblReportPrecision.PaddingLeft = 10;
            this.lblReportPrecision.PaddingTop = 3;
            this.lblReportPrecision.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblReportPrecision, "lblReportPrecision");
            this.btReportScalePrecision64.CanCustomize = false;
            this.btReportScalePrecision64.Name = "btReportScalePrecision64";
            resources.ApplyResources(this.btReportScalePrecision64, "btReportScalePrecision64");
            this.btReportScalePrecision64.Click += new EventHandler(this.btReportScalePrecision64_Click);
            this.btReportScalePrecision32.CanCustomize = false;
            this.btReportScalePrecision32.Name = "btReportScalePrecision32";
            resources.ApplyResources(this.btReportScalePrecision32, "btReportScalePrecision32");
            this.btReportScalePrecision32.Click += new EventHandler(this.btReportScalePrecision32_Click);
            this.btReportScalePrecision16.CanCustomize = false;
            this.btReportScalePrecision16.Name = "btReportScalePrecision16";
            resources.ApplyResources(this.btReportScalePrecision16, "btReportScalePrecision16");
            this.btReportScalePrecision16.Click += new EventHandler(this.btReportScalePrecision16_Click);
            this.btReportScalePrecision8.CanCustomize = false;
            this.btReportScalePrecision8.Name = "btReportScalePrecision8";
            resources.ApplyResources(this.btReportScalePrecision8, "btReportScalePrecision8");
            this.btReportScalePrecision8.Click += new EventHandler(this.btReportScalePrecision8_Click);
            this.lblReportTheme.BackColor = Color.FromArgb(221, 231, 238);
            this.lblReportTheme.BorderSide = eBorderSide.None;
            this.lblReportTheme.CanCustomize = false;
            this.lblReportTheme.Name = "lblReportTheme";
            this.lblReportTheme.PaddingBottom = 3;
            this.lblReportTheme.PaddingLeft = 10;
            this.lblReportTheme.PaddingTop = 3;
            this.lblReportTheme.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblReportTheme, "lblReportTheme");
            this.lblReportTheme.Visible = false;
            this.btReportSettings.ButtonStyle = eButtonStyle.ImageAndText;
            this.btReportSettings.Image = (Image)resources.GetObject("btReportSettings.Image");
            this.btReportSettings.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btReportSettings.ImagePosition = eImagePosition.Top;
            this.btReportSettings.Name = "btReportSettings";
            resources.ApplyResources(this.btReportSettings, "btReportSettings");
            this.btReportSettings.Click += new EventHandler(this.btReportSettings_Click);
            this.ribbonPanel.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel.Controls.Add(this.ribbonBarPrintExport);
            this.ribbonPanel.Controls.Add(this.ribbonBarImage);
            this.ribbonPanel.Controls.Add(this.ribbonBarBrowse);
            this.ribbonPanel.Controls.Add(this.ribbonBarZoom);
            this.ribbonPanel.Controls.Add(this.ribbonBarAnnotations);
            this.ribbonPanel.Controls.Add(this.ribbonBarTools);
            this.ribbonPanel.Controls.Add(this.ribbonBarScale);
            this.ribbonPanel.Controls.Add(this.ribbonBarEdit);
            this.ribbonPanel.Controls.Add(this.ribbonBarLayout);
            resources.ApplyResources(this.ribbonPanel, "ribbonPanel");
            this.ribbonPanel.Name = "ribbonPanel";
            this.ribbonPanel.Style.CornerType = eCornerType.Square;
            this.ribbonPanel.StyleMouseDown.CornerType = eCornerType.Square;
            this.ribbonPanel.StyleMouseOver.CornerType = eCornerType.Square;
            this.ribbonBarPrintExport.AutoOverflowEnabled = true;
            this.ribbonBarPrintExport.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarPrintExport.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarPrintExport.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarPrintExport, "ribbonBarPrintExport");
            this.ribbonBarPrintExport.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btPrintPlan,
    this.btExportPlanToPDF
            });

            this.ribbonBarPrintExport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPrintExport.Name = "ribbonBarPrintExport";
            this.ribbonBarPrintExport.OverflowButtonImage = Resources.print;
            this.ribbonBarPrintExport.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPrintExport.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarPrintExport.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btPrintPlan.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPrintPlan.Image = Resources.printer_32x36;
            this.btPrintPlan.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPrintPlan.ImagePaddingHorizontal = 10;
            this.btPrintPlan.ImagePaddingVertical = 5;
            this.btPrintPlan.ImagePosition = eImagePosition.Top;
            this.btPrintPlan.Name = "btPrintPlan";
            this.btPrintPlan.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblPrintPlanOptions,
    this.btPrintPlanFullSize,
    this.btPrintPlanWindow
            });

            resources.ApplyResources(this.btPrintPlan, "btPrintPlan");
            this.btPrintPlan.Click += new EventHandler(this.btPrintPlan_Click);
            this.lblPrintPlanOptions.BackColor = Color.FromArgb(221, 231, 238);
            this.lblPrintPlanOptions.BorderSide = eBorderSide.None;
            this.lblPrintPlanOptions.CanCustomize = false;
            this.lblPrintPlanOptions.Name = "lblPrintPlanOptions";
            this.lblPrintPlanOptions.PaddingBottom = 3;
            this.lblPrintPlanOptions.PaddingLeft = 10;
            this.lblPrintPlanOptions.PaddingTop = 3;
            this.lblPrintPlanOptions.SingleLineColor = Color.FromArgb(197, 197, 197);
            this.lblPrintPlanOptions.SubItems.AddRange(new BaseItem[] { this.buttonItem2 });
            resources.ApplyResources(this.lblPrintPlanOptions, "lblPrintPlanOptions");
            this.buttonItem2.CanCustomize = false;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = Resources.Impérial;
            this.btPrintPlanFullSize.AutoCheckOnClick = true;
            this.btPrintPlanFullSize.CanCustomize = false;
            this.btPrintPlanFullSize.Name = "btPrintPlanFullSize";
            this.btPrintPlanFullSize.OptionGroup = "PrintPlanOptions";
            resources.ApplyResources(this.btPrintPlanFullSize, "btPrintPlanFullSize");
            this.btPrintPlanFullSize.Click += new EventHandler(this.btPrintPlanType_Click);
            this.btPrintPlanWindow.AutoCheckOnClick = true;
            this.btPrintPlanWindow.CanCustomize = false;
            this.btPrintPlanWindow.Name = "btPrintPlanWindow";
            this.btPrintPlanWindow.OptionGroup = "PrintPlanOptions";
            resources.ApplyResources(this.btPrintPlanWindow, "btPrintPlanWindow");
            this.btPrintPlanWindow.Click += new EventHandler(this.btPrintPlanType_Click);
            this.btExportPlanToPDF.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExportPlanToPDF.Image = Resources.file_pdf_40x40;
            this.btExportPlanToPDF.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExportPlanToPDF.ImagePaddingVertical = 5;
            this.btExportPlanToPDF.ImagePosition = eImagePosition.Top;
            this.btExportPlanToPDF.Name = "btExportPlanToPDF";
            resources.ApplyResources(this.btExportPlanToPDF, "btExportPlanToPDF");
            this.btExportPlanToPDF.Click += new EventHandler(this.btExportPlanToPDF_Click);
            this.ribbonBarImage.AutoOverflowEnabled = true;
            this.ribbonBarImage.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarImage.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarImage.CanCustomize = false;
            this.ribbonBarImage.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarImage, "ribbonBarImage");
            this.ribbonBarImage.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btBrightnessContrast,
    this.btRotation
            });

            this.ribbonBarImage.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarImage.Name = "ribbonBarImage";
            this.ribbonBarImage.OverflowButtonImage = (Image)resources.GetObject("ribbonBarImage.OverflowButtonImage");
            this.ribbonBarImage.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarImage.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarImage.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btBrightnessContrast.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrightnessContrast.Image = (Image)resources.GetObject("btBrightnessContrast.Image");
            this.btBrightnessContrast.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btBrightnessContrast.ImagePosition = eImagePosition.Top;
            this.btBrightnessContrast.Name = "btBrightnessContrast";
            resources.ApplyResources(this.btBrightnessContrast, "btBrightnessContrast");
            this.btBrightnessContrast.Click += new EventHandler(this.btBrightnessContrast_Click);
            this.btRotation.ButtonStyle = eButtonStyle.ImageAndText;
            this.btRotation.Image = (Image)resources.GetObject("btRotation.Image");
            this.btRotation.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btRotation.ImagePosition = eImagePosition.Top;
            this.btRotation.Name = "btRotation";
            resources.ApplyResources(this.btRotation, "btRotation");
            this.btRotation.Click += new EventHandler(this.btRotation_Click);
            this.ribbonBarBrowse.AutoOverflowEnabled = true;
            this.ribbonBarBrowse.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarBrowse.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarBrowse.CanCustomize = false;
            this.ribbonBarBrowse.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarBrowse, "ribbonBarBrowse");
            this.ribbonBarBrowse.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.itemContainerBrowse1,
    this.itemContainerBrowse2
            });

            this.ribbonBarBrowse.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarBrowse.Name = "ribbonBarBrowse";
            this.ribbonBarBrowse.OverflowButtonImage = (Image)resources.GetObject("ribbonBarBrowse.OverflowButtonImage");
            this.ribbonBarBrowse.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarBrowse.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarBrowse.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.itemContainerBrowse1.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerBrowse1.FixedSize = new Size(88, 0);
            this.itemContainerBrowse1.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse1.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerBrowse1.MultiLine = true;
            this.itemContainerBrowse1.Name = "itemContainerBrowse1";
            this.itemContainerBrowse1.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblBrowseGroup,
    this.itemContainerBrowse3
            });

            this.itemContainerBrowse1.TitleStyle.CornerType = eCornerType.Square;
            this.lblBrowseGroup.Name = "lblBrowseGroup";
            this.lblBrowseGroup.PaddingTop = 3;
            this.lblBrowseGroup.Stretch = true;
            resources.ApplyResources(this.lblBrowseGroup, "lblBrowseGroup");
            this.lblBrowseGroup.TextAlignment = StringAlignment.Center;
            this.lblBrowseGroup.Width = 88;
            this.lblBrowseGroup.WordWrap = true;
            this.itemContainerBrowse3.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerBrowse3.FixedSize = new Size(88, 0);
            this.itemContainerBrowse3.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse3.Name = "itemContainerBrowse3";
            this.itemContainerBrowse3.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btBrowsePrevious,
    this.btBrowseNext
            });

            this.itemContainerBrowse3.TitleStyle.CornerType = eCornerType.Square;
            this.btBrowsePrevious.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrowsePrevious.Image = (Image)resources.GetObject("btBrowsePrevious.Image");
            this.btBrowsePrevious.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btBrowsePrevious.ImagePosition = eImagePosition.Bottom;
            this.btBrowsePrevious.Name = "btBrowsePrevious";
            this.btBrowsePrevious.Click += new EventHandler(this.btBrowsePrevious_Click);
            this.btBrowseNext.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrowseNext.Image = (Image)resources.GetObject("btBrowseNext.Image");
            this.btBrowseNext.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btBrowseNext.ImagePosition = eImagePosition.Bottom;
            this.btBrowseNext.Name = "btBrowseNext";
            this.btBrowseNext.Click += new EventHandler(this.btBrowseNext_Click);
            this.itemContainerBrowse2.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerBrowse2.FixedSize = new Size(88, 0);
            this.itemContainerBrowse2.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse2.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerBrowse2.MultiLine = true;
            this.itemContainerBrowse2.Name = "itemContainerBrowse2";
            this.itemContainerBrowse2.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblBrowseObjectType,
    this.itemContainerBrowse4
            });

            this.itemContainerBrowse2.TitleStyle.CornerType = eCornerType.Square;
            this.lblBrowseObjectType.Name = "lblBrowseObjectType";
            this.lblBrowseObjectType.PaddingTop = 3;
            this.lblBrowseObjectType.Stretch = true;
            resources.ApplyResources(this.lblBrowseObjectType, "lblBrowseObjectType");
            this.lblBrowseObjectType.TextAlignment = StringAlignment.Center;
            this.lblBrowseObjectType.Width = 88;
            this.lblBrowseObjectType.WordWrap = true;
            this.itemContainerBrowse4.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerBrowse4.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.itemContainerBrowse4.MinimumSize = new Size(88, 0);
            this.itemContainerBrowse4.Name = "itemContainerBrowse4";
            this.itemContainerBrowse4.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btBrowseObjectTypePrevious,
    this.btBrowseObjectTypeNext
            });

            this.itemContainerBrowse4.TitleStyle.CornerType = eCornerType.Square;
            this.btBrowseObjectTypePrevious.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrowseObjectTypePrevious.Image = (Image)resources.GetObject("btBrowseObjectTypePrevious.Image");
            this.btBrowseObjectTypePrevious.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btBrowseObjectTypePrevious.ImagePosition = eImagePosition.Bottom;
            this.btBrowseObjectTypePrevious.Name = "btBrowseObjectTypePrevious";
            this.btBrowseObjectTypePrevious.Click += new EventHandler(this.btBrowseObjectTypePrevious_Click);
            this.btBrowseObjectTypeNext.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrowseObjectTypeNext.Image = (Image)resources.GetObject("btBrowseObjectTypeNext.Image");
            this.btBrowseObjectTypeNext.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btBrowseObjectTypeNext.ImagePosition = eImagePosition.Bottom;
            this.btBrowseObjectTypeNext.Name = "btBrowseObjectTypeNext";
            this.btBrowseObjectTypeNext.Click += new EventHandler(this.btBrowseObjectTypeNext_Click);
            this.ribbonBarZoom.AutoOverflowEnabled = false;
            this.ribbonBarZoom.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarZoom.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarZoom.CanCustomize = false;
            this.ribbonBarZoom.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarZoom, "ribbonBarZoom");
            this.ribbonBarZoom.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btZoomToSelection,
    this.btZoomToWindow,
    this.btZoomActualSize,
    this.btZoomIn,
    this.btZoomOut,
    this.btBookmarks
            });

            this.ribbonBarZoom.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarZoom.Name = "ribbonBarZoom";
            this.ribbonBarZoom.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarZoom.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarZoom.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btZoomToSelection.BeginGroup = true;
            this.btZoomToSelection.ButtonStyle = eButtonStyle.ImageAndText;
            this.btZoomToSelection.Image = (Image)resources.GetObject("btZoomToSelection.Image");
            this.btZoomToSelection.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btZoomToSelection.ImagePosition = eImagePosition.Top;
            this.btZoomToSelection.Name = "btZoomToSelection";
            resources.ApplyResources(this.btZoomToSelection, "btZoomToSelection");
            this.btZoomToSelection.Click += new EventHandler(this.btZoomToSelection_Click);
            this.btZoomToWindow.ButtonStyle = eButtonStyle.ImageAndText;
            this.btZoomToWindow.Image = (Image)resources.GetObject("btZoomToWindow.Image");
            this.btZoomToWindow.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btZoomToWindow.ImagePosition = eImagePosition.Top;
            this.btZoomToWindow.Name = "btZoomToWindow";
            resources.ApplyResources(this.btZoomToWindow, "btZoomToWindow");
            this.btZoomToWindow.Click += new EventHandler(this.btZoomToWindow_Click);
            this.btZoomActualSize.ButtonStyle = eButtonStyle.ImageAndText;
            this.btZoomActualSize.CanCustomize = false;
            this.btZoomActualSize.Image = (Image)resources.GetObject("btZoomActualSize.Image");
            this.btZoomActualSize.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btZoomActualSize.ImagePosition = eImagePosition.Top;
            this.btZoomActualSize.Name = "btZoomActualSize";
            this.btZoomActualSize.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btZoom75,
    this.btZoom50,
    this.btZoom25
            });

            resources.ApplyResources(this.btZoomActualSize, "btZoomActualSize");
            this.btZoomActualSize.Click += new EventHandler(this.btZoomActualSize_Click);
            this.btZoom75.CanCustomize = false;
            this.btZoom75.Image = (Image)resources.GetObject("btZoom75.Image");
            this.btZoom75.Name = "btZoom75";
            resources.ApplyResources(this.btZoom75, "btZoom75");
            this.btZoom75.Click += new EventHandler(this.btZoom75_Click);
            this.btZoom50.ButtonStyle = eButtonStyle.ImageAndText;
            this.btZoom50.CanCustomize = false;
            this.btZoom50.Image = (Image)resources.GetObject("btZoom50.Image");
            this.btZoom50.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btZoom50.ImagePaddingHorizontal = 12;
            this.btZoom50.ImagePosition = eImagePosition.Top;
            this.btZoom50.Name = "btZoom50";
            resources.ApplyResources(this.btZoom50, "btZoom50");
            this.btZoom50.Click += new EventHandler(this.btZoom50_Click);
            this.btZoom25.CanCustomize = false;
            this.btZoom25.Image = (Image)resources.GetObject("btZoom25.Image");
            this.btZoom25.Name = "btZoom25";
            resources.ApplyResources(this.btZoom25, "btZoom25");
            this.btZoom25.Click += new EventHandler(this.btZoom25_Click);
            this.btZoomIn.ButtonStyle = eButtonStyle.ImageAndText;
            this.btZoomIn.Image = (Image)resources.GetObject("btZoomIn.Image");
            this.btZoomIn.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btZoomIn.ImagePosition = eImagePosition.Top;
            this.btZoomIn.Name = "btZoomIn";
            resources.ApplyResources(this.btZoomIn, "btZoomIn");
            this.btZoomIn.Click += new EventHandler(this.btZoomIn_Click);
            this.btZoomOut.ButtonStyle = eButtonStyle.ImageAndText;
            this.btZoomOut.Image = (Image)resources.GetObject("btZoomOut.Image");
            this.btZoomOut.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btZoomOut.ImagePosition = eImagePosition.Top;
            this.btZoomOut.Name = "btZoomOut";
            resources.ApplyResources(this.btZoomOut, "btZoomOut");
            this.btZoomOut.Click += new EventHandler(this.btZoomOut_Click);
            this.btBookmarks.BeginGroup = true;
            this.btBookmarks.CanCustomize = false;
            this.btBookmarks.Image = (Image)resources.GetObject("btBookmarks.Image");
            this.btBookmarks.ImageFixedSize = new Size(32, 32);
            this.btBookmarks.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btBookmarks.ImagePosition = eImagePosition.Top;
            this.btBookmarks.Name = "btBookmarks";
            this.btBookmarks.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btZoomTo75,
    this.btZoomTo50,
    this.btZoomTo25,
    this.btZoomTo150,
    this.btZoomTo200,
    this.buttonItem61,
    this.btModifyBookmarks
            });

            resources.ApplyResources(this.btBookmarks, "btBookmarks");
            this.btBookmarks.Visible = false;
            this.btZoomTo75.Name = "btZoomTo75";
            resources.ApplyResources(this.btZoomTo75, "btZoomTo75");
            this.btZoomTo50.Name = "btZoomTo50";
            resources.ApplyResources(this.btZoomTo50, "btZoomTo50");
            this.btZoomTo25.Name = "btZoomTo25";
            resources.ApplyResources(this.btZoomTo25, "btZoomTo25");
            this.btZoomTo150.Name = "btZoomTo150";
            resources.ApplyResources(this.btZoomTo150, "btZoomTo150");
            this.btZoomTo200.Name = "btZoomTo200";
            resources.ApplyResources(this.btZoomTo200, "btZoomTo200");
            this.buttonItem61.Name = "buttonItem61";
            resources.ApplyResources(this.buttonItem61, "buttonItem61");
            this.btModifyBookmarks.BeginGroup = true;
            this.btModifyBookmarks.Name = "btModifyBookmarks";
            resources.ApplyResources(this.btModifyBookmarks, "btModifyBookmarks");
            this.ribbonBarAnnotations.AutoOverflowEnabled = false;
            this.ribbonBarAnnotations.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarAnnotations.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarAnnotations.CanCustomize = false;
            this.ribbonBarAnnotations.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarAnnotations, "ribbonBarAnnotations");
            this.ribbonBarAnnotations.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btMarkZone,
    this.btInsertNote,
    this.btInsertPicture
            });

            this.ribbonBarAnnotations.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarAnnotations.Name = "ribbonBarAnnotations";
            this.ribbonBarAnnotations.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarAnnotations.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarAnnotations.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btMarkZone.ButtonStyle = eButtonStyle.ImageAndText;
            this.btMarkZone.CanCustomize = false;
            this.btMarkZone.Image = (Image)resources.GetObject("btMarkZone.Image");
            this.btMarkZone.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btMarkZone.ImagePosition = eImagePosition.Top;
            this.btMarkZone.Name = "btMarkZone";
            resources.ApplyResources(this.btMarkZone, "btMarkZone");
            this.btMarkZone.Click += new EventHandler(this.btMarkZone_Click);
            this.btInsertNote.ButtonStyle = eButtonStyle.ImageAndText;
            this.btInsertNote.CanCustomize = false;
            this.btInsertNote.Image = (Image)resources.GetObject("btInsertNote.Image");
            this.btInsertNote.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btInsertNote.ImagePosition = eImagePosition.Top;
            this.btInsertNote.Name = "btInsertNote";
            resources.ApplyResources(this.btInsertNote, "btInsertNote");
            this.btInsertNote.Click += new EventHandler(this.btInsertNote_Click);
            this.btInsertPicture.ButtonStyle = eButtonStyle.ImageAndText;
            this.btInsertPicture.CanCustomize = false;
            this.btInsertPicture.Image = (Image)resources.GetObject("btInsertPicture.Image");
            this.btInsertPicture.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btInsertPicture.ImagePosition = eImagePosition.Top;
            this.btInsertPicture.Name = "btInsertPicture";
            resources.ApplyResources(this.btInsertPicture, "btInsertPicture");
            this.btInsertPicture.Visible = false;
            this.ribbonBarTools.AutoOverflowEnabled = false;
            this.ribbonBarTools.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarTools.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarTools.CanCustomize = false;
            this.ribbonBarTools.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarTools, "ribbonBarTools");
            this.ribbonBarTools.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btToolSelection,
    this.btToolPan,
    this.btToolArea,
    this.btToolPerimeter,
    this.btToolRuler,
    this.btToolCounter,
    this.btToolAngle
            });

            this.ribbonBarTools.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarTools.Name = "ribbonBarTools";
            this.ribbonBarTools.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarTools.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarTools.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btToolSelection.ButtonStyle = eButtonStyle.ImageAndText;
            this.btToolSelection.CanCustomize = false;
            this.btToolSelection.Image = (Image)resources.GetObject("btToolSelection.Image");
            this.btToolSelection.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btToolSelection.ImagePosition = eImagePosition.Top;
            this.btToolSelection.Name = "btToolSelection";
            resources.ApplyResources(this.btToolSelection, "btToolSelection");
            this.btToolSelection.Click += new EventHandler(this.btToolSelection_Click);
            this.btToolPan.ButtonStyle = eButtonStyle.ImageAndText;
            this.btToolPan.CanCustomize = false;
            this.btToolPan.Image = (Image)resources.GetObject("btToolPan.Image");
            this.btToolPan.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btToolPan.ImagePosition = eImagePosition.Top;
            this.btToolPan.Name = "btToolPan";
            resources.ApplyResources(this.btToolPan, "btToolPan");
            this.btToolPan.Click += new EventHandler(this.btToolPan_Click);
            this.btToolArea.ButtonStyle = eButtonStyle.ImageAndText;
            this.btToolArea.CanCustomize = false;
            this.btToolArea.Image = (Image)resources.GetObject("btToolArea.Image");
            this.btToolArea.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btToolArea.ImagePosition = eImagePosition.Top;
            this.btToolArea.Name = "btToolArea";
            this.btToolArea.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblNoArea,
    this.lblAreaFilter,
    this.itemContainerAreaFilter,
    this.lblAreaGroups,
    this.galleryAreaGroups,
    this.lblAreaTemplates,
    this.galleryAreaTemplates
            });

            resources.ApplyResources(this.btToolArea, "btToolArea");
            this.btToolArea.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.btToolArea_PopupOpen);
            this.btToolArea.Click += new EventHandler(this.btToolArea_Click);
            this.lblNoArea.BackColor = Color.FromArgb(221, 231, 238);
            this.lblNoArea.BorderSide = eBorderSide.None;
            this.lblNoArea.CanCustomize = false;
            this.lblNoArea.Name = "lblNoArea";
            this.lblNoArea.PaddingBottom = 3;
            this.lblNoArea.PaddingLeft = 10;
            this.lblNoArea.PaddingTop = 3;
            this.lblNoArea.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblNoArea, "lblNoArea");
            this.lblAreaFilter.BackColor = Color.FromArgb(221, 231, 238);
            this.lblAreaFilter.BorderSide = eBorderSide.None;
            this.lblAreaFilter.CanCustomize = false;
            this.lblAreaFilter.Name = "lblAreaFilter";
            this.lblAreaFilter.PaddingBottom = 3;
            this.lblAreaFilter.PaddingLeft = 10;
            this.lblAreaFilter.PaddingTop = 3;
            this.lblAreaFilter.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblAreaFilter, "lblAreaFilter");
            this.itemContainerAreaFilter.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerAreaFilter.CanCustomize = false;
            this.itemContainerAreaFilter.ItemSpacing = 0;
            this.itemContainerAreaFilter.Name = "itemContainerAreaFilter";
            this.itemContainerAreaFilter.ResizeItemsToFit = false;
            this.itemContainerAreaFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.txtAreaFilter,
    this.btAreaFilterClear,
    this.lblAreaFilterPadding
            });

            this.itemContainerAreaFilter.TitleStyle.CornerType = eCornerType.Square;
            this.txtAreaFilter.CanCustomize = false;
            this.txtAreaFilter.ItemAlignment = eItemAlignment.Center;
            this.txtAreaFilter.Name = "txtAreaFilter";
            this.txtAreaFilter.Stretch = true;
            this.txtAreaFilter.WatermarkColor = SystemColors.GrayText;
            this.txtAreaFilter.KeyUp += new KeyEventHandler(this.txtFilter_KeyUp);
            this.txtAreaFilter.LostFocus += new EventHandler(this.txtFilter_LostFocus);
            this.txtAreaFilter.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.txtAreaFilter.MouseMove += new MouseEventHandler(this.txtFilter_MouseMove);
            this.btAreaFilterClear.AutoCollapseOnClick = false;
            this.btAreaFilterClear.Image = Resources.delete_16x16;
            this.btAreaFilterClear.ImagePaddingHorizontal = 4;
            this.btAreaFilterClear.ImagePaddingVertical = 4;
            this.btAreaFilterClear.Name = "btAreaFilterClear";
            this.btAreaFilterClear.Click += new EventHandler(this.btFilterClear_Click);
            this.btAreaFilterClear.LostFocus += new EventHandler(this.txtFilter_LostFocus);
            this.btAreaFilterClear.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.lblAreaFilterPadding.Name = "lblAreaFilterPadding";
            this.lblAreaFilterPadding.Width = 4;
            this.lblAreaGroups.BackColor = Color.FromArgb(221, 231, 238);
            this.lblAreaGroups.BorderSide = eBorderSide.None;
            this.lblAreaGroups.CanCustomize = false;
            this.lblAreaGroups.Name = "lblAreaGroups";
            this.lblAreaGroups.PaddingBottom = 3;
            this.lblAreaGroups.PaddingLeft = 10;
            this.lblAreaGroups.PaddingTop = 3;
            this.lblAreaGroups.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblAreaGroups, "lblAreaGroups");
            this.galleryAreaGroups.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryAreaGroups.CanCustomize = false;
            this.galleryAreaGroups.DefaultSize = new Size(200, 20);
            this.galleryAreaGroups.EnableGalleryPopup = false;
            this.galleryAreaGroups.MinimumSize = new Size(200, 20);
            this.galleryAreaGroups.Name = "galleryAreaGroups";
            resources.ApplyResources(this.galleryAreaGroups, "galleryAreaGroups");
            this.galleryAreaGroups.TitleStyle.CornerType = eCornerType.Square;
            this.lblAreaTemplates.BackColor = Color.FromArgb(221, 231, 238);
            this.lblAreaTemplates.BorderSide = eBorderSide.None;
            this.lblAreaTemplates.CanCustomize = false;
            this.lblAreaTemplates.Name = "lblAreaTemplates";
            this.lblAreaTemplates.PaddingBottom = 3;
            this.lblAreaTemplates.PaddingLeft = 10;
            this.lblAreaTemplates.PaddingTop = 3;
            this.lblAreaTemplates.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblAreaTemplates, "lblAreaTemplates");
            this.galleryAreaTemplates.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryAreaTemplates.CanCustomize = false;
            this.galleryAreaTemplates.DefaultSize = new Size(200, 20);
            this.galleryAreaTemplates.EnableGalleryPopup = false;
            this.galleryAreaTemplates.MinimumSize = new Size(200, 20);
            this.galleryAreaTemplates.Name = "galleryAreaTemplates";
            resources.ApplyResources(this.galleryAreaTemplates, "galleryAreaTemplates");
            this.galleryAreaTemplates.TitleStyle.CornerType = eCornerType.Square;
            this.btToolPerimeter.ButtonStyle = eButtonStyle.ImageAndText;
            this.btToolPerimeter.CanCustomize = false;
            this.btToolPerimeter.Image = (Image)resources.GetObject("btToolPerimeter.Image");
            this.btToolPerimeter.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btToolPerimeter.ImagePosition = eImagePosition.Top;
            this.btToolPerimeter.Name = "btToolPerimeter";
            this.btToolPerimeter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblNoPerimeter,
    this.lblPerimeterFilter,
    this.itemContainerPerimeterFilter,
    this.lblPerimeterGroups,
    this.galleryPerimeterGroups,
    this.lblPerimeterTemplates,
    this.galleryPerimeterTemplates
            });

            resources.ApplyResources(this.btToolPerimeter, "btToolPerimeter");
            this.btToolPerimeter.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.btToolPerimeter_PopupOpen);
            this.btToolPerimeter.Click += new EventHandler(this.btToolPerimeter_Click);
            this.lblNoPerimeter.BackColor = Color.FromArgb(221, 231, 238);
            this.lblNoPerimeter.BorderSide = eBorderSide.None;
            this.lblNoPerimeter.CanCustomize = false;
            this.lblNoPerimeter.Name = "lblNoPerimeter";
            this.lblNoPerimeter.PaddingBottom = 3;
            this.lblNoPerimeter.PaddingLeft = 10;
            this.lblNoPerimeter.PaddingTop = 3;
            this.lblNoPerimeter.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblNoPerimeter, "lblNoPerimeter");
            this.lblPerimeterFilter.BackColor = Color.FromArgb(221, 231, 238);
            this.lblPerimeterFilter.BorderSide = eBorderSide.None;
            this.lblPerimeterFilter.CanCustomize = false;
            this.lblPerimeterFilter.Name = "lblPerimeterFilter";
            this.lblPerimeterFilter.PaddingBottom = 3;
            this.lblPerimeterFilter.PaddingLeft = 10;
            this.lblPerimeterFilter.PaddingTop = 3;
            this.lblPerimeterFilter.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblPerimeterFilter, "lblPerimeterFilter");
            this.itemContainerPerimeterFilter.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerPerimeterFilter.CanCustomize = false;
            this.itemContainerPerimeterFilter.ItemSpacing = 0;
            this.itemContainerPerimeterFilter.Name = "itemContainerPerimeterFilter";
            this.itemContainerPerimeterFilter.ResizeItemsToFit = false;
            this.itemContainerPerimeterFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.txtPerimeterFilter,
    this.btPerimeterFilterClear
            });

            this.itemContainerPerimeterFilter.TitleStyle.CornerType = eCornerType.Square;
            this.txtPerimeterFilter.CanCustomize = false;
            this.txtPerimeterFilter.Name = "txtPerimeterFilter";
            this.txtPerimeterFilter.Stretch = true;
            this.txtPerimeterFilter.WatermarkColor = SystemColors.GrayText;
            this.txtPerimeterFilter.KeyUp += new KeyEventHandler(this.txtFilter_KeyUp);
            this.txtPerimeterFilter.LostFocus += new EventHandler(this.txtFilter_LostFocus);
            this.txtPerimeterFilter.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.txtPerimeterFilter.MouseMove += new MouseEventHandler(this.txtFilter_MouseMove);
            this.btPerimeterFilterClear.AutoCollapseOnClick = false;
            this.btPerimeterFilterClear.Image = Resources.delete_16x16;
            this.btPerimeterFilterClear.ImagePaddingHorizontal = 4;
            this.btPerimeterFilterClear.ImagePaddingVertical = 4;
            this.btPerimeterFilterClear.Name = "btPerimeterFilterClear";
            this.btPerimeterFilterClear.Click += new EventHandler(this.btFilterClear_Click);
            this.btPerimeterFilterClear.LostFocus += new EventHandler(this.txtFilter_LostFocus);
            this.btPerimeterFilterClear.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.lblPerimeterGroups.BackColor = Color.FromArgb(221, 231, 238);
            this.lblPerimeterGroups.BorderSide = eBorderSide.None;
            this.lblPerimeterGroups.CanCustomize = false;
            this.lblPerimeterGroups.Name = "lblPerimeterGroups";
            this.lblPerimeterGroups.PaddingBottom = 3;
            this.lblPerimeterGroups.PaddingLeft = 10;
            this.lblPerimeterGroups.PaddingTop = 3;
            this.lblPerimeterGroups.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblPerimeterGroups, "lblPerimeterGroups");
            this.galleryPerimeterGroups.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryPerimeterGroups.CanCustomize = false;
            this.galleryPerimeterGroups.DefaultSize = new Size(200, 20);
            this.galleryPerimeterGroups.EnableGalleryPopup = false;
            this.galleryPerimeterGroups.MinimumSize = new Size(200, 20);
            this.galleryPerimeterGroups.Name = "galleryPerimeterGroups";
            this.galleryPerimeterGroups.ScrollAnimation = false;
            resources.ApplyResources(this.galleryPerimeterGroups, "galleryPerimeterGroups");
            this.galleryPerimeterGroups.TitleStyle.CornerType = eCornerType.Square;
            this.lblPerimeterTemplates.BackColor = Color.FromArgb(221, 231, 238);
            this.lblPerimeterTemplates.BorderSide = eBorderSide.None;
            this.lblPerimeterTemplates.CanCustomize = false;
            this.lblPerimeterTemplates.Name = "lblPerimeterTemplates";
            this.lblPerimeterTemplates.PaddingBottom = 3;
            this.lblPerimeterTemplates.PaddingLeft = 10;
            this.lblPerimeterTemplates.PaddingTop = 3;
            this.lblPerimeterTemplates.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblPerimeterTemplates, "lblPerimeterTemplates");
            this.galleryPerimeterTemplates.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryPerimeterTemplates.CanCustomize = false;
            this.galleryPerimeterTemplates.DefaultSize = new Size(200, 20);
            this.galleryPerimeterTemplates.EnableGalleryPopup = false;
            this.galleryPerimeterTemplates.MinimumSize = new Size(200, 20);
            this.galleryPerimeterTemplates.Name = "galleryPerimeterTemplates";
            resources.ApplyResources(this.galleryPerimeterTemplates, "galleryPerimeterTemplates");
            this.galleryPerimeterTemplates.TitleStyle.CornerType = eCornerType.Square;
            this.btToolRuler.ButtonStyle = eButtonStyle.ImageAndText;
            this.btToolRuler.CanCustomize = false;
            this.btToolRuler.Image = (Image)resources.GetObject("btToolRuler.Image");
            this.btToolRuler.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btToolRuler.ImagePosition = eImagePosition.Top;
            this.btToolRuler.Name = "btToolRuler";
            this.btToolRuler.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblNoDistance,
    this.lblDistanceFilter,
    this.itemContainerDistanceFilter,
    this.lblDistanceGroups,
    this.galleryDistanceGroups,
    this.lblDistanceTemplates,
    this.galleryDistanceTemplates
            });

            resources.ApplyResources(this.btToolRuler, "btToolRuler");
            this.btToolRuler.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.btToolRuler_PopupOpen);
            this.btToolRuler.Click += new EventHandler(this.btToolRuler_Click);
            this.lblNoDistance.BackColor = Color.FromArgb(221, 231, 238);
            this.lblNoDistance.BorderSide = eBorderSide.None;
            this.lblNoDistance.CanCustomize = false;
            this.lblNoDistance.Name = "lblNoDistance";
            this.lblNoDistance.PaddingBottom = 3;
            this.lblNoDistance.PaddingLeft = 10;
            this.lblNoDistance.PaddingTop = 3;
            this.lblNoDistance.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblNoDistance, "lblNoDistance");
            this.lblDistanceFilter.BackColor = Color.FromArgb(221, 231, 238);
            this.lblDistanceFilter.BorderSide = eBorderSide.None;
            this.lblDistanceFilter.CanCustomize = false;
            this.lblDistanceFilter.Name = "lblDistanceFilter";
            this.lblDistanceFilter.PaddingBottom = 3;
            this.lblDistanceFilter.PaddingLeft = 10;
            this.lblDistanceFilter.PaddingTop = 3;
            this.lblDistanceFilter.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblDistanceFilter, "lblDistanceFilter");
            this.itemContainerDistanceFilter.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerDistanceFilter.CanCustomize = false;
            this.itemContainerDistanceFilter.ItemSpacing = 0;
            this.itemContainerDistanceFilter.Name = "itemContainerDistanceFilter";
            this.itemContainerDistanceFilter.ResizeItemsToFit = false;
            this.itemContainerDistanceFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.txtDistanceFilter,
    this.btDistanceFilterClear
            });

            this.itemContainerDistanceFilter.TitleStyle.CornerType = eCornerType.Square;
            this.txtDistanceFilter.CanCustomize = false;
            this.txtDistanceFilter.Name = "txtDistanceFilter";
            this.txtDistanceFilter.Stretch = true;
            this.txtDistanceFilter.WatermarkColor = SystemColors.GrayText;
            this.txtDistanceFilter.KeyUp += new KeyEventHandler(this.txtFilter_KeyUp);
            this.txtDistanceFilter.LostFocus += new EventHandler(this.txtFilter_LostFocus);
            this.txtDistanceFilter.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.txtDistanceFilter.MouseMove += new MouseEventHandler(this.txtFilter_MouseMove);
            this.btDistanceFilterClear.AutoCollapseOnClick = false;
            this.btDistanceFilterClear.Image = Resources.delete_16x16;
            this.btDistanceFilterClear.ImagePaddingHorizontal = 4;
            this.btDistanceFilterClear.ImagePaddingVertical = 4;
            this.btDistanceFilterClear.Name = "btDistanceFilterClear";
            this.btDistanceFilterClear.Click += new EventHandler(this.btFilterClear_Click);
            this.btDistanceFilterClear.LostFocus += new EventHandler(this.txtFilter_LostFocus);
            this.btDistanceFilterClear.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.lblDistanceGroups.BackColor = Color.FromArgb(221, 231, 238);
            this.lblDistanceGroups.BorderSide = eBorderSide.None;
            this.lblDistanceGroups.CanCustomize = false;
            this.lblDistanceGroups.Name = "lblDistanceGroups";
            this.lblDistanceGroups.PaddingBottom = 3;
            this.lblDistanceGroups.PaddingLeft = 10;
            this.lblDistanceGroups.PaddingTop = 3;
            this.lblDistanceGroups.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblDistanceGroups, "lblDistanceGroups");
            this.galleryDistanceGroups.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryDistanceGroups.CanCustomize = false;
            this.galleryDistanceGroups.DefaultSize = new Size(200, 20);
            this.galleryDistanceGroups.EnableGalleryPopup = false;
            this.galleryDistanceGroups.MinimumSize = new Size(200, 20);
            this.galleryDistanceGroups.Name = "galleryDistanceGroups";
            this.galleryDistanceGroups.ScrollAnimation = false;
            resources.ApplyResources(this.galleryDistanceGroups, "galleryDistanceGroups");
            this.galleryDistanceGroups.TitleStyle.CornerType = eCornerType.Square;
            this.lblDistanceTemplates.BackColor = Color.FromArgb(221, 231, 238);
            this.lblDistanceTemplates.BorderSide = eBorderSide.None;
            this.lblDistanceTemplates.CanCustomize = false;
            this.lblDistanceTemplates.Name = "lblDistanceTemplates";
            this.lblDistanceTemplates.PaddingBottom = 3;
            this.lblDistanceTemplates.PaddingLeft = 10;
            this.lblDistanceTemplates.PaddingTop = 3;
            this.lblDistanceTemplates.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblDistanceTemplates, "lblDistanceTemplates");
            this.galleryDistanceTemplates.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryDistanceTemplates.CanCustomize = false;
            this.galleryDistanceTemplates.DefaultSize = new Size(200, 20);
            this.galleryDistanceTemplates.EnableGalleryPopup = false;
            this.galleryDistanceTemplates.MinimumSize = new Size(200, 20);
            this.galleryDistanceTemplates.Name = "galleryDistanceTemplates";
            this.galleryDistanceTemplates.ScrollAnimation = false;
            resources.ApplyResources(this.galleryDistanceTemplates, "galleryDistanceTemplates");
            this.galleryDistanceTemplates.TitleStyle.CornerType = eCornerType.Square;
            this.btToolCounter.ButtonStyle = eButtonStyle.ImageAndText;
            this.btToolCounter.CanCustomize = false;
            this.btToolCounter.Image = (Image)resources.GetObject("btToolCounter.Image");
            this.btToolCounter.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btToolCounter.ImagePosition = eImagePosition.Top;
            this.btToolCounter.Name = "btToolCounter";
            this.btToolCounter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblNoCounter,
    this.lblCounterFilter,
    this.itemContainerCounterFilter,
    this.lblCounterGroups,
    this.galleryCounterGroups,
    this.lblCounterTemplates,
    this.galleryCounterTemplates
            });

            resources.ApplyResources(this.btToolCounter, "btToolCounter");
            this.btToolCounter.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.btToolCounter_PopupOpen);
            this.btToolCounter.Click += new EventHandler(this.btToolCounter_Click);
            this.lblNoCounter.BackColor = Color.FromArgb(221, 231, 238);
            this.lblNoCounter.BorderSide = eBorderSide.None;
            this.lblNoCounter.CanCustomize = false;
            this.lblNoCounter.Name = "lblNoCounter";
            this.lblNoCounter.PaddingBottom = 3;
            this.lblNoCounter.PaddingLeft = 10;
            this.lblNoCounter.PaddingTop = 3;
            this.lblNoCounter.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblNoCounter, "lblNoCounter");
            this.lblCounterFilter.BackColor = Color.FromArgb(221, 231, 238);
            this.lblCounterFilter.BorderSide = eBorderSide.None;
            this.lblCounterFilter.CanCustomize = false;
            this.lblCounterFilter.Name = "lblCounterFilter";
            this.lblCounterFilter.PaddingBottom = 3;
            this.lblCounterFilter.PaddingLeft = 10;
            this.lblCounterFilter.PaddingTop = 3;
            this.lblCounterFilter.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblCounterFilter, "lblCounterFilter");
            this.itemContainerCounterFilter.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerCounterFilter.CanCustomize = false;
            this.itemContainerCounterFilter.ItemSpacing = 0;
            this.itemContainerCounterFilter.Name = "itemContainerCounterFilter";
            this.itemContainerCounterFilter.ResizeItemsToFit = false;
            this.itemContainerCounterFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.txtCounterFilter,
    this.btCounterFilterClear
            });

            this.itemContainerCounterFilter.TitleStyle.CornerType = eCornerType.Square;
            this.txtCounterFilter.CanCustomize = false;
            this.txtCounterFilter.Name = "txtCounterFilter";
            this.txtCounterFilter.Stretch = true;
            this.txtCounterFilter.WatermarkColor = SystemColors.GrayText;
            this.txtCounterFilter.KeyUp += new KeyEventHandler(this.txtFilter_KeyUp);
            this.txtCounterFilter.LostFocus += new EventHandler(this.txtFilter_GotFocus);
            this.txtCounterFilter.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.txtCounterFilter.MouseMove += new MouseEventHandler(this.txtFilter_MouseMove);
            this.btCounterFilterClear.AutoCollapseOnClick = false;
            this.btCounterFilterClear.Image = Resources.delete_16x16;
            this.btCounterFilterClear.ImagePaddingHorizontal = 4;
            this.btCounterFilterClear.ImagePaddingVertical = 4;
            this.btCounterFilterClear.Name = "btCounterFilterClear";
            this.btCounterFilterClear.Click += new EventHandler(this.btFilterClear_Click);
            this.btCounterFilterClear.LostFocus += new EventHandler(this.txtFilter_GotFocus);
            this.btCounterFilterClear.GotFocus += new EventHandler(this.txtFilter_GotFocus);
            this.lblCounterGroups.BackColor = Color.FromArgb(221, 231, 238);
            this.lblCounterGroups.BorderSide = eBorderSide.None;
            this.lblCounterGroups.CanCustomize = false;
            this.lblCounterGroups.Name = "lblCounterGroups";
            this.lblCounterGroups.PaddingBottom = 3;
            this.lblCounterGroups.PaddingLeft = 10;
            this.lblCounterGroups.PaddingTop = 3;
            this.lblCounterGroups.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblCounterGroups, "lblCounterGroups");
            this.galleryCounterGroups.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryCounterGroups.CanCustomize = false;
            this.galleryCounterGroups.DefaultSize = new Size(200, 20);
            this.galleryCounterGroups.EnableGalleryPopup = false;
            this.galleryCounterGroups.MinimumSize = new Size(200, 20);
            this.galleryCounterGroups.Name = "galleryCounterGroups";
            this.galleryCounterGroups.ScrollAnimation = false;
            resources.ApplyResources(this.galleryCounterGroups, "galleryCounterGroups");
            this.galleryCounterGroups.TitleStyle.CornerType = eCornerType.Square;
            this.lblCounterTemplates.BackColor = Color.FromArgb(221, 231, 238);
            this.lblCounterTemplates.BorderSide = eBorderSide.None;
            this.lblCounterTemplates.CanCustomize = false;
            this.lblCounterTemplates.Name = "lblCounterTemplates";
            this.lblCounterTemplates.PaddingBottom = 3;
            this.lblCounterTemplates.PaddingLeft = 10;
            this.lblCounterTemplates.PaddingTop = 3;
            this.lblCounterTemplates.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblCounterTemplates, "lblCounterTemplates");
            this.galleryCounterTemplates.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryCounterTemplates.CanCustomize = false;
            this.galleryCounterTemplates.DefaultSize = new Size(200, 20);
            this.galleryCounterTemplates.EnableGalleryPopup = false;
            this.galleryCounterTemplates.MinimumSize = new Size(200, 20);
            this.galleryCounterTemplates.Name = "galleryCounterTemplates";
            this.galleryCounterTemplates.ScrollAnimation = false;
            resources.ApplyResources(this.galleryCounterTemplates, "galleryCounterTemplates");
            this.galleryCounterTemplates.TitleStyle.CornerType = eCornerType.Square;
            this.btToolAngle.ButtonStyle = eButtonStyle.ImageAndText;
            this.btToolAngle.CanCustomize = false;
            this.btToolAngle.Image = (Image)resources.GetObject("btToolAngle.Image");
            this.btToolAngle.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btToolAngle.ImagePosition = eImagePosition.Top;
            this.btToolAngle.Name = "btToolAngle";
            this.btToolAngle.Text = Resources.Angle;
            this.btToolAngle.Click += new EventHandler(this.btToolAngle_Click);
            this.ribbonBarScale.AutoOverflowEnabled = false;
            this.ribbonBarScale.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarScale.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarScale.CanCustomize = false;
            this.ribbonBarScale.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarScale, "ribbonBarScale");
            this.ribbonBarScale.Items.AddRange(new BaseItem[] { this.btScaleSet });
            this.ribbonBarScale.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarScale.Name = "ribbonBarScale";
            this.ribbonBarScale.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarScale.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarScale.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btScaleSet.ButtonStyle = eButtonStyle.ImageAndText;
            this.btScaleSet.CanCustomize = false;
            this.btScaleSet.Image = (Image)resources.GetObject("btScaleSet.Image");
            this.btScaleSet.ImageFixedSize = new Size(48, 48);
            this.btScaleSet.ImagePaddingVertical = 3;
            this.btScaleSet.ImagePosition = eImagePosition.Top;
            this.btScaleSet.Name = "btScaleSet";
            this.btScaleSet.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblSystemType,
    this.btScaleImperial,
    this.btScaleMetric,
    this.lblPrecision,
    this.btScalePrecision64,
    this.btScalePrecision32,
    this.btScalePrecision16,
    this.btScalePrecision8
            });

            this.btScaleSet.Click += new EventHandler(this.btScaleSet_Click);
            this.lblSystemType.BackColor = Color.FromArgb(221, 231, 238);
            this.lblSystemType.BorderSide = eBorderSide.None;
            this.lblSystemType.CanCustomize = false;
            this.lblSystemType.Name = "lblSystemType";
            this.lblSystemType.PaddingBottom = 3;
            this.lblSystemType.PaddingLeft = 10;
            this.lblSystemType.PaddingTop = 3;
            this.lblSystemType.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblSystemType, "lblSystemType");
            this.btScaleImperial.CanCustomize = false;
            this.btScaleImperial.Name = "btScaleImperial";
            this.btScaleImperial.Text = Resources.Impérial;
            this.btScaleImperial.Click += new EventHandler(this.btScaleImperial_Click);
            this.btScaleMetric.CanCustomize = false;
            this.btScaleMetric.Name = "btScaleMetric";
            this.btScaleMetric.Text = Resources.Métrique;
            this.btScaleMetric.Click += new EventHandler(this.btScaleMetric_Click);
            this.lblPrecision.BackColor = Color.FromArgb(221, 231, 238);
            this.lblPrecision.BorderSide = eBorderSide.None;
            this.lblPrecision.CanCustomize = false;
            this.lblPrecision.Name = "lblPrecision";
            this.lblPrecision.PaddingBottom = 4;
            this.lblPrecision.PaddingLeft = 10;
            this.lblPrecision.PaddingTop = 4;
            this.lblPrecision.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblPrecision, "lblPrecision");
            this.btScalePrecision64.CanCustomize = false;
            this.btScalePrecision64.Name = "btScalePrecision64";
            resources.ApplyResources(this.btScalePrecision64, "btScalePrecision64");
            this.btScalePrecision64.Click += new EventHandler(this.btScalePrecision64_Click);
            this.btScalePrecision32.CanCustomize = false;
            this.btScalePrecision32.Name = "btScalePrecision32";
            resources.ApplyResources(this.btScalePrecision32, "btScalePrecision32");
            this.btScalePrecision32.Click += new EventHandler(this.btScalePrecision32_Click);
            this.btScalePrecision16.CanCustomize = false;
            this.btScalePrecision16.Name = "btScalePrecision16";
            resources.ApplyResources(this.btScalePrecision16, "btScalePrecision16");
            this.btScalePrecision16.Click += new EventHandler(this.btScalePrecision16_Click);
            this.btScalePrecision8.CanCustomize = false;
            this.btScalePrecision8.Name = "btScalePrecision8";
            resources.ApplyResources(this.btScalePrecision8, "btScalePrecision8");
            this.btScalePrecision8.Click += new EventHandler(this.btScalePrecision8_Click);
            this.ribbonBarEdit.AutoOverflowEnabled = false;
            this.ribbonBarEdit.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarEdit.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarEdit.CanCustomize = false;
            this.ribbonBarEdit.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarEdit, "ribbonBarEdit");
            this.ribbonBarEdit.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btEditPaste,
    this.itemContainerEdit,
    this.itemContainerUndoRedo,
    this.btEditSendData
            });

            this.ribbonBarEdit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarEdit.Name = "ribbonBarEdit";
            this.ribbonBarEdit.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarEdit.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarEdit.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btEditPaste.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEditPaste.Image = (Image)resources.GetObject("btEditPaste.Image");
            this.btEditPaste.ImagePosition = eImagePosition.Top;
            this.btEditPaste.ItemAlignment = eItemAlignment.Center;
            this.btEditPaste.Name = "btEditPaste";
            resources.ApplyResources(this.btEditPaste, "btEditPaste");
            this.btEditPaste.Click += new EventHandler(this.btEditPaste_Click);
            this.itemContainerEdit.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerEdit.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.itemContainerEdit.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerEdit.Name = "itemContainerEdit";
            this.itemContainerEdit.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btEditCut,
    this.btEditCopy,
    this.btEditDelete
            });

            this.itemContainerEdit.TitleStyle.CornerType = eCornerType.Square;
            this.itemContainerEdit.VerticalItemAlignment = eVerticalItemsAlignment.Middle;
            this.btEditCut.Image = (Image)resources.GetObject("btEditCut.Image");
            this.btEditCut.ImageFixedSize = new Size(22, 22);
            this.btEditCut.ImagePaddingVertical = 1;
            this.btEditCut.Name = "btEditCut";
            resources.ApplyResources(this.btEditCut, "btEditCut");
            this.btEditCut.Click += new EventHandler(this.btEditCut_Click);
            this.btEditCopy.Image = (Image)resources.GetObject("btEditCopy.Image");
            this.btEditCopy.ImagePaddingVertical = 1;
            this.btEditCopy.Name = "btEditCopy";
            resources.ApplyResources(this.btEditCopy, "btEditCopy");
            this.btEditCopy.Click += new EventHandler(this.btEditCopy_Click);
            this.btEditDelete.Image = (Image)resources.GetObject("btEditDelete.Image");
            this.btEditDelete.ImagePaddingVertical = 1;
            this.btEditDelete.Name = "btEditDelete";
            resources.ApplyResources(this.btEditDelete, "btEditDelete");
            this.btEditDelete.Click += new EventHandler(this.btEditDelete_Click);
            this.itemContainerUndoRedo.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerUndoRedo.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.itemContainerUndoRedo.ItemSpacing = 3;
            this.itemContainerUndoRedo.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerUndoRedo.Name = "itemContainerUndoRedo";
            this.itemContainerUndoRedo.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btEditUndo,
    this.btEditRedo
            });

            this.itemContainerUndoRedo.TitleStyle.CornerType = eCornerType.Square;
            this.itemContainerUndoRedo.VerticalItemAlignment = eVerticalItemsAlignment.Middle;
            this.itemContainerUndoRedo.Visible = false;
            this.btEditUndo.Image = (Image)resources.GetObject("btEditUndo.Image");
            this.btEditUndo.ImagePaddingVertical = 2;
            this.btEditUndo.Name = "btEditUndo";
            resources.ApplyResources(this.btEditUndo, "btEditUndo");
            this.btEditUndo.Click += new EventHandler(this.btEditUndo_Click);
            this.btEditRedo.Image = (Image)resources.GetObject("btEditRedo.Image");
            this.btEditRedo.ImagePaddingVertical = 2;
            this.btEditRedo.Name = "btEditRedo";
            resources.ApplyResources(this.btEditRedo, "btEditRedo");
            this.btEditRedo.Click += new EventHandler(this.btEditRedo_Click);
            this.btEditSendData.AutoExpandOnClick = true;
            this.btEditSendData.ButtonStyle = eButtonStyle.ImageAndText;
            this.btEditSendData.Image = Resources.right_arrow_blue;
            this.btEditSendData.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btEditSendData.ImagePosition = eImagePosition.Top;
            this.btEditSendData.Name = "btEditSendData";
            resources.ApplyResources(this.btEditSendData, "btEditSendData");
            this.btEditSendData.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.btEditSendData_PopupOpen);
            this.btEditSendData.Click += new EventHandler(this.btEditSendData_Click);
            this.ribbonBarLayout.AutoOverflowEnabled = false;
            this.ribbonBarLayout.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarLayout.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarLayout.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarLayout, "ribbonBarLayout");
            this.ribbonBarLayout.Items.AddRange(new BaseItem[] { this.itemContainerLayouts });
            this.ribbonBarLayout.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarLayout.Name = "ribbonBarLayout";
            this.ribbonBarLayout.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarLayout.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarLayout.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.itemContainerLayouts.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerLayouts.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerLayouts.Name = "itemContainerLayouts";
            this.itemContainerLayouts.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.opTakeoffLayout,
    this.opEstimatingLayout
            });

            this.itemContainerLayouts.TitleStyle.CornerType = eCornerType.Square;
            this.itemContainerLayouts.VerticalItemAlignment = eVerticalItemsAlignment.Middle;
            this.opTakeoffLayout.CheckBoxPosition = eCheckBoxPosition.Right;
            this.opTakeoffLayout.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.opTakeoffLayout.Checked = true;
            this.opTakeoffLayout.CheckState = CheckState.Checked;
            this.opTakeoffLayout.Name = "opTakeoffLayout";
            resources.ApplyResources(this.opTakeoffLayout, "opTakeoffLayout");
            this.opEstimatingLayout.CheckBoxPosition = eCheckBoxPosition.Right;
            this.opEstimatingLayout.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.opEstimatingLayout.Name = "opEstimatingLayout";
            resources.ApplyResources(this.opEstimatingLayout, "opEstimatingLayout");
            this.ribbonPanelTemplates.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplate);
            this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplateCreate);
            this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplateDatabase);
            resources.ApplyResources(this.ribbonPanelTemplates, "ribbonPanelTemplates");
            this.ribbonPanelTemplates.Name = "ribbonPanelTemplates";
            this.ribbonPanelTemplates.Style.CornerType = eCornerType.Square;
            this.ribbonPanelTemplates.StyleMouseDown.CornerType = eCornerType.Square;
            this.ribbonPanelTemplates.StyleMouseOver.CornerType = eCornerType.Square;
            this.ribbonTemplate.AutoOverflowEnabled = false;
            this.ribbonTemplate.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonTemplate.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonTemplate.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonTemplate, "ribbonTemplate");
            this.ribbonTemplate.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btTemplateModify,
    this.btTemplateDuplicate,
    this.btTemplateDelete
            });

            this.ribbonTemplate.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonTemplate.Name = "ribbonTemplate";
            this.ribbonTemplate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonTemplate.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonTemplate.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btTemplateModify.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateModify.CanCustomize = false;
            this.btTemplateModify.Image = Resources.properties;
            this.btTemplateModify.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateModify.ImagePosition = eImagePosition.Top;
            this.btTemplateModify.Name = "btTemplateModify";
            resources.ApplyResources(this.btTemplateModify, "btTemplateModify");
            this.btTemplateModify.Click += new EventHandler(this.btTemplateModify_Click);
            this.btTemplateDuplicate.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateDuplicate.CanCustomize = false;
            this.btTemplateDuplicate.Image = (Image)resources.GetObject("btTemplateDuplicate.Image");
            this.btTemplateDuplicate.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateDuplicate.ImagePosition = eImagePosition.Top;
            this.btTemplateDuplicate.Name = "btTemplateDuplicate";
            resources.ApplyResources(this.btTemplateDuplicate, "btTemplateDuplicate");
            this.btTemplateDuplicate.Click += new EventHandler(this.btTemplateDuplicate_Click);
            this.btTemplateDelete.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateDelete.CanCustomize = false;
            this.btTemplateDelete.Image = (Image)resources.GetObject("btTemplateDelete.Image");
            this.btTemplateDelete.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateDelete.ImagePosition = eImagePosition.Top;
            this.btTemplateDelete.Name = "btTemplateDelete";
            resources.ApplyResources(this.btTemplateDelete, "btTemplateDelete");
            this.btTemplateDelete.Click += new EventHandler(this.btTemplateDelete_Click);
            this.ribbonTemplateCreate.AutoOverflowEnabled = false;
            this.ribbonTemplateCreate.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonTemplateCreate.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonTemplateCreate.CanCustomize = false;
            this.ribbonTemplateCreate.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonTemplateCreate, "ribbonTemplateCreate");
            this.ribbonTemplateCreate.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btTemplateArea,
    this.btTemplatePerimeter,
    this.btTemplateLength,
    this.btTemplateCounter
            });

            this.ribbonTemplateCreate.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonTemplateCreate.Name = "ribbonTemplateCreate";
            this.ribbonTemplateCreate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonTemplateCreate.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonTemplateCreate.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btTemplateArea.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateArea.CanCustomize = false;
            this.btTemplateArea.Image = (Image)resources.GetObject("btTemplateArea.Image");
            this.btTemplateArea.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateArea.ImagePosition = eImagePosition.Top;
            this.btTemplateArea.Name = "btTemplateArea";
            resources.ApplyResources(this.btTemplateArea, "btTemplateArea");
            this.btTemplateArea.Click += new EventHandler(this.btTemplateArea_Click);
            this.btTemplatePerimeter.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplatePerimeter.CanCustomize = false;
            this.btTemplatePerimeter.Image = (Image)resources.GetObject("btTemplatePerimeter.Image");
            this.btTemplatePerimeter.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplatePerimeter.ImagePosition = eImagePosition.Top;
            this.btTemplatePerimeter.Name = "btTemplatePerimeter";
            resources.ApplyResources(this.btTemplatePerimeter, "btTemplatePerimeter");
            this.btTemplatePerimeter.Click += new EventHandler(this.btTemplatePerimeter_Click);
            this.btTemplateLength.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateLength.CanCustomize = false;
            this.btTemplateLength.Image = (Image)resources.GetObject("btTemplateLength.Image");
            this.btTemplateLength.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateLength.ImagePosition = eImagePosition.Top;
            this.btTemplateLength.Name = "btTemplateLength";
            resources.ApplyResources(this.btTemplateLength, "btTemplateLength");
            this.btTemplateLength.Click += new EventHandler(this.btTemplateLength_Click);
            this.btTemplateCounter.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateCounter.CanCustomize = false;
            this.btTemplateCounter.Image = (Image)resources.GetObject("btTemplateCounter.Image");
            this.btTemplateCounter.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateCounter.ImagePosition = eImagePosition.Top;
            this.btTemplateCounter.Name = "btTemplateCounter";
            resources.ApplyResources(this.btTemplateCounter, "btTemplateCounter");
            this.btTemplateCounter.Click += new EventHandler(this.btTemplateCounter_Click);
            this.ribbonTemplateDatabase.AutoOverflowEnabled = false;
            this.ribbonTemplateDatabase.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonTemplateDatabase.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonTemplateDatabase.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonTemplateDatabase, "ribbonTemplateDatabase");
            this.ribbonTemplateDatabase.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btTemplateTradesPackages,
    this.btTemplateCompactDatabase
            });

            this.ribbonTemplateDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonTemplateDatabase.Name = "ribbonTemplateDatabase";
            this.ribbonTemplateDatabase.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonTemplateDatabase.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonTemplateDatabase.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btTemplateTradesPackages.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateTradesPackages.Image = (Image)resources.GetObject("btTemplateTradesPackages.Image");
            this.btTemplateTradesPackages.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateTradesPackages.ImagePaddingHorizontal = 10;
            this.btTemplateTradesPackages.ImagePaddingVertical = 5;
            this.btTemplateTradesPackages.ImagePosition = eImagePosition.Top;
            this.btTemplateTradesPackages.Name = "btTemplateTradesPackages";
            resources.ApplyResources(this.btTemplateTradesPackages, "btTemplateTradesPackages");
            this.btTemplateCompactDatabase.ButtonStyle = eButtonStyle.ImageAndText;
            this.btTemplateCompactDatabase.Image = (Image)resources.GetObject("btTemplateCompactDatabase.Image");
            this.btTemplateCompactDatabase.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btTemplateCompactDatabase.ImagePaddingVertical = 5;
            this.btTemplateCompactDatabase.ImagePosition = eImagePosition.Top;
            this.btTemplateCompactDatabase.Name = "btTemplateCompactDatabase";
            resources.ApplyResources(this.btTemplateCompactDatabase, "btTemplateCompactDatabase");
            this.ribbonPanelPlans.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelPlans.Controls.Add(this.ribbonBarMultiPlans);
            this.ribbonPanelPlans.Controls.Add(this.ribbonBarPlans);
            this.ribbonPanelPlans.Controls.Add(this.ribbonBarPlansInsert);
            resources.ApplyResources(this.ribbonPanelPlans, "ribbonPanelPlans");
            this.ribbonPanelPlans.Name = "ribbonPanelPlans";
            this.ribbonPanelPlans.Style.CornerType = eCornerType.Square;
            this.ribbonPanelPlans.StyleMouseDown.CornerType = eCornerType.Square;
            this.ribbonPanelPlans.StyleMouseOver.CornerType = eCornerType.Square;
            this.ribbonBarMultiPlans.AutoOverflowEnabled = false;
            this.ribbonBarMultiPlans.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarMultiPlans.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarMultiPlans.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarMultiPlans, "ribbonBarMultiPlans");
            this.ribbonBarMultiPlans.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btPlansPrint,
    this.btPlansExport
            });

            this.ribbonBarMultiPlans.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarMultiPlans.Name = "ribbonBarMultiPlans";
            this.ribbonBarMultiPlans.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarMultiPlans.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarMultiPlans.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btPlansPrint.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlansPrint.Image = Resources.printer_32x36;
            this.btPlansPrint.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlansPrint.ImagePaddingHorizontal = 10;
            this.btPlansPrint.ImagePaddingVertical = 5;
            this.btPlansPrint.ImagePosition = eImagePosition.Top;
            this.btPlansPrint.Name = "btPlansPrint";
            resources.ApplyResources(this.btPlansPrint, "btPlansPrint");
            this.btPlansPrint.Click += new EventHandler(this.btPlansPrint_Click);
            this.btPlansExport.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlansExport.Image = Resources.file_pdf_40x40;
            this.btPlansExport.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlansExport.ImagePaddingVertical = 5;
            this.btPlansExport.ImagePosition = eImagePosition.Top;
            this.btPlansExport.Name = "btPlansExport";
            resources.ApplyResources(this.btPlansExport, "btPlansExport");
            this.btPlansExport.Click += new EventHandler(this.btPlansExport_Click);
            this.ribbonBarPlans.AutoOverflowEnabled = false;
            this.ribbonBarPlans.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlans.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlans.CanCustomize = false;
            this.ribbonBarPlans.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarPlans, "ribbonBarPlans");
            this.ribbonBarPlans.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btPlanLoad,
    this.btPlanProperties,
    this.btPlanRemove,
    this.btPlanExport,
    this.btPlanDuplicate
            });

            this.ribbonBarPlans.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPlans.Name = "ribbonBarPlans";
            this.ribbonBarPlans.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPlans.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlans.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btPlanLoad.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlanLoad.Image = (Image)resources.GetObject("btPlanLoad.Image");
            this.btPlanLoad.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlanLoad.ImagePosition = eImagePosition.Top;
            this.btPlanLoad.Name = "btPlanLoad";
            resources.ApplyResources(this.btPlanLoad, "btPlanLoad");
            this.btPlanLoad.Click += new EventHandler(this.btPlanLoad_Click);
            this.btPlanProperties.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlanProperties.Image = (Image)resources.GetObject("btPlanProperties.Image");
            this.btPlanProperties.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlanProperties.ImagePosition = eImagePosition.Top;
            this.btPlanProperties.Name = "btPlanProperties";
            resources.ApplyResources(this.btPlanProperties, "btPlanProperties");
            this.btPlanProperties.Click += new EventHandler(this.btPlanProperties_Click);
            this.btPlanRemove.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlanRemove.Image = (Image)resources.GetObject("btPlanRemove.Image");
            this.btPlanRemove.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlanRemove.ImagePosition = eImagePosition.Top;
            this.btPlanRemove.Name = "btPlanRemove";
            resources.ApplyResources(this.btPlanRemove, "btPlanRemove");
            this.btPlanRemove.Click += new EventHandler(this.btPlanRemove_Click);
            this.btPlanExport.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlanExport.Image = (Image)resources.GetObject("btPlanExport.Image");
            this.btPlanExport.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlanExport.ImagePosition = eImagePosition.Top;
            this.btPlanExport.Name = "btPlanExport";
            resources.ApplyResources(this.btPlanExport, "btPlanExport");
            this.btPlanExport.Visible = false;
            this.btPlanExport.Click += new EventHandler(this.btPlanExport_Click);
            this.btPlanDuplicate.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlanDuplicate.Image = (Image)resources.GetObject("btPlanDuplicate.Image");
            this.btPlanDuplicate.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlanDuplicate.ImagePosition = eImagePosition.Top;
            this.btPlanDuplicate.Name = "btPlanDuplicate";
            resources.ApplyResources(this.btPlanDuplicate, "btPlanDuplicate");
            this.btPlanDuplicate.Click += new EventHandler(this.btPlanDuplicate_Click);
            this.ribbonBarPlansInsert.AutoOverflowEnabled = false;
            this.ribbonBarPlansInsert.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlansInsert.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlansInsert.CanCustomize = false;
            this.ribbonBarPlansInsert.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarPlansInsert, "ribbonBarPlansInsert");
            this.ribbonBarPlansInsert.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btPlanInsertFromPDF,
    this.btPlanInsertFromImage
            });

            this.ribbonBarPlansInsert.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPlansInsert.Name = "ribbonBarPlansInsert";
            this.ribbonBarPlansInsert.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPlansInsert.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlansInsert.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btPlanInsertFromPDF.AutoCollapseOnClick = false;
            this.btPlanInsertFromPDF.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlanInsertFromPDF.CanCustomize = false;
            this.btPlanInsertFromPDF.Image = (Image)resources.GetObject("btPlanInsertFromPDF.Image");
            this.btPlanInsertFromPDF.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlanInsertFromPDF.ImagePosition = eImagePosition.Top;
            this.btPlanInsertFromPDF.Name = "btPlanInsertFromPDF";
            this.btPlanInsertFromPDF.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.iblImportDPI,
    this.op172Dpi,
    this.op300Dpi,
    this.opOtherDpi,
    this.sliderDpi,
    this.itemContainerDpi,
    this.iblImportColorManagement,
    this.opConvertToColor
            });

            resources.ApplyResources(this.btPlanInsertFromPDF, "btPlanInsertFromPDF");
            this.btPlanInsertFromPDF.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.btPlanInsertFromPDF_PopupOpen);
            this.btPlanInsertFromPDF.Click += new EventHandler(this.btPlanInsertFromPDF_Click);
            this.iblImportDPI.BackColor = Color.FromArgb(221, 231, 238);
            this.iblImportDPI.BorderSide = eBorderSide.None;
            this.iblImportDPI.CanCustomize = false;
            this.iblImportDPI.Name = "iblImportDPI";
            this.iblImportDPI.PaddingBottom = 3;
            this.iblImportDPI.PaddingLeft = 10;
            this.iblImportDPI.PaddingTop = 3;
            this.iblImportDPI.SingleLineColor = Color.FromArgb(197, 197, 197);
            this.iblImportDPI.SubItems.AddRange(new BaseItem[] { this.labelItem2 });
            resources.ApplyResources(this.iblImportDPI, "iblImportDPI");
            this.labelItem2.BackColor = Color.FromArgb(221, 231, 238);
            this.labelItem2.BorderSide = eBorderSide.None;
            this.labelItem2.CanCustomize = false;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.PaddingBottom = 3;
            this.labelItem2.PaddingLeft = 10;
            this.labelItem2.PaddingTop = 3;
            this.labelItem2.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.labelItem2, "labelItem2");
            this.op172Dpi.CanCustomize = false;
            this.op172Dpi.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.op172Dpi.Name = "op172Dpi";
            resources.ApplyResources(this.op172Dpi, "op172Dpi");
            this.op172Dpi.Click += new EventHandler(this.op172Dpi_Click);
            this.op300Dpi.CanCustomize = false;
            this.op300Dpi.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.op300Dpi.Name = "op300Dpi";
            resources.ApplyResources(this.op300Dpi, "op300Dpi");
            this.op300Dpi.Click += new EventHandler(this.op300Dpi_Click);
            this.opOtherDpi.AutoCollapseOnClick = false;
            this.opOtherDpi.CanCustomize = false;
            this.opOtherDpi.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.opOtherDpi.Name = "opOtherDpi";
            resources.ApplyResources(this.opOtherDpi, "opOtherDpi");
            this.opOtherDpi.Click += new EventHandler(this.opOtherDpi_Click);
            this.sliderDpi.AutoCollapseOnClick = false;
            this.sliderDpi.CanCustomize = false;
            this.sliderDpi.LabelPosition = eSliderLabelPosition.Top;
            this.sliderDpi.LabelWidth = 150;
            this.sliderDpi.Maximum = 0x12c;
            this.sliderDpi.Minimum = 150;
            this.sliderDpi.Name = "sliderDpi";
            resources.ApplyResources(this.sliderDpi, "sliderDpi");
            this.sliderDpi.TextColor = Color.Black;
            this.sliderDpi.TrackMarker = false;
            this.sliderDpi.Value = 150;
            this.sliderDpi.Width = 150;
            this.sliderDpi.ValueChanged += new EventHandler(this.sliderDpi_ValueChanged);
            this.itemContainerDpi.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerDpi.FixedSize = new Size(0, 32);
            this.itemContainerDpi.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.itemContainerDpi.Name = "itemContainerDpi";
            this.itemContainerDpi.ResizeItemsToFit = false;
            this.itemContainerDpi.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblDpi1,
    this.labelDpiPadding1,
    this.lblDpi2
            });

            this.itemContainerDpi.TitleStyle.CornerType = eCornerType.Square;
            this.lblDpi1.CanCustomize = false;
            this.lblDpi1.Name = "lblDpi1";
            resources.ApplyResources(this.lblDpi1, "lblDpi1");
            this.lblDpi1.TextAlignment = StringAlignment.Center;
            this.lblDpi1.WordWrap = true;
            this.labelDpiPadding1.CanCustomize = false;
            this.labelDpiPadding1.Name = "labelDpiPadding1";
            this.labelDpiPadding1.Stretch = true;
            this.labelDpiPadding1.Width = 120;
            this.lblDpi2.CanCustomize = false;
            this.lblDpi2.Name = "lblDpi2";
            resources.ApplyResources(this.lblDpi2, "lblDpi2");
            this.lblDpi2.TextAlignment = StringAlignment.Center;
            this.lblDpi2.WordWrap = true;
            this.iblImportColorManagement.BackColor = Color.FromArgb(221, 231, 238);
            this.iblImportColorManagement.BorderSide = eBorderSide.None;
            this.iblImportColorManagement.CanCustomize = false;
            this.iblImportColorManagement.Name = "iblImportColorManagement";
            this.iblImportColorManagement.PaddingBottom = 3;
            this.iblImportColorManagement.PaddingLeft = 10;
            this.iblImportColorManagement.PaddingTop = 3;
            this.iblImportColorManagement.SingleLineColor = Color.FromArgb(197, 197, 197);
            this.iblImportColorManagement.SubItems.AddRange(new BaseItem[] { this.labelItem4 });
            resources.ApplyResources(this.iblImportColorManagement, "iblImportColorManagement");
            this.labelItem4.BackColor = Color.FromArgb(221, 231, 238);
            this.labelItem4.BorderSide = eBorderSide.None;
            this.labelItem4.CanCustomize = false;
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.PaddingBottom = 3;
            this.labelItem4.PaddingLeft = 10;
            this.labelItem4.PaddingTop = 3;
            this.labelItem4.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.labelItem4, "labelItem4");
            this.opConvertToColor.CanCustomize = false;
            resources.ApplyResources(this.opConvertToColor, "opConvertToColor");
            this.opConvertToColor.Name = "opConvertToColor";
            this.opConvertToColor.Click += new EventHandler(this.opConvertToColor_Click);
            this.btPlanInsertFromImage.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlanInsertFromImage.Image = (Image)resources.GetObject("btPlanInsertFromImage.Image");
            this.btPlanInsertFromImage.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlanInsertFromImage.ImagePosition = eImagePosition.Top;
            this.btPlanInsertFromImage.Name = "btPlanInsertFromImage";
            resources.ApplyResources(this.btPlanInsertFromImage, "btPlanInsertFromImage");
            this.btPlanInsertFromImage.Click += new EventHandler(this.btPlanInsertFromImage_Click);
            this.ribbonPanelExtensions.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanelExtensions.Controls.Add(this.ribbonExtension);
            this.ribbonPanelExtensions.Controls.Add(this.ribbonExtensionCreate);
            this.ribbonPanelExtensions.Controls.Add(this.ribbonExtensionDatabase);
            resources.ApplyResources(this.ribbonPanelExtensions, "ribbonPanelExtensions");
            this.ribbonPanelExtensions.Name = "ribbonPanelExtensions";
            this.ribbonPanelExtensions.Style.CornerType = eCornerType.Square;
            this.ribbonPanelExtensions.StyleMouseDown.CornerType = eCornerType.Square;
            this.ribbonPanelExtensions.StyleMouseOver.CornerType = eCornerType.Square;
            this.ribbonExtension.AutoOverflowEnabled = false;
            this.ribbonExtension.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonExtension.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonExtension.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonExtension, "ribbonExtension");
            this.ribbonExtension.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btExtensionModify,
    this.btExtensionDuplicate,
    this.btExtensionDelete
            });

            this.ribbonExtension.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExtension.Name = "ribbonExtension";
            this.ribbonExtension.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExtension.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonExtension.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btExtensionModify.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionModify.CanCustomize = false;
            this.btExtensionModify.Image = Resources.properties;
            this.btExtensionModify.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionModify.ImagePosition = eImagePosition.Top;
            this.btExtensionModify.Name = "btExtensionModify";
            resources.ApplyResources(this.btExtensionModify, "btExtensionModify");
            this.btExtensionDuplicate.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionDuplicate.CanCustomize = false;
            this.btExtensionDuplicate.Image = (Image)resources.GetObject("btExtensionDuplicate.Image");
            this.btExtensionDuplicate.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionDuplicate.ImagePosition = eImagePosition.Top;
            this.btExtensionDuplicate.Name = "btExtensionDuplicate";
            resources.ApplyResources(this.btExtensionDuplicate, "btExtensionDuplicate");
            this.btExtensionDelete.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionDelete.CanCustomize = false;
            this.btExtensionDelete.Image = (Image)resources.GetObject("btExtensionDelete.Image");
            this.btExtensionDelete.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionDelete.ImagePosition = eImagePosition.Top;
            this.btExtensionDelete.Name = "btExtensionDelete";
            resources.ApplyResources(this.btExtensionDelete, "btExtensionDelete");
            this.ribbonExtensionCreate.AutoOverflowEnabled = false;
            this.ribbonExtensionCreate.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonExtensionCreate.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonExtensionCreate.CanCustomize = false;
            this.ribbonExtensionCreate.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonExtensionCreate, "ribbonExtensionCreate");
            this.ribbonExtensionCreate.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btExtensionArea,
    this.btExtensionPerimeter,
    this.btExtensionRuler,
    this.btExtensionCounter
            });

            this.ribbonExtensionCreate.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExtensionCreate.Name = "ribbonExtensionCreate";
            this.ribbonExtensionCreate.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExtensionCreate.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonExtensionCreate.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btExtensionArea.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionArea.CanCustomize = false;
            this.btExtensionArea.Image = (Image)resources.GetObject("btExtensionArea.Image");
            this.btExtensionArea.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionArea.ImagePosition = eImagePosition.Top;
            this.btExtensionArea.Name = "btExtensionArea";
            resources.ApplyResources(this.btExtensionArea, "btExtensionArea");
            this.btExtensionPerimeter.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionPerimeter.CanCustomize = false;
            this.btExtensionPerimeter.Image = (Image)resources.GetObject("btExtensionPerimeter.Image");
            this.btExtensionPerimeter.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionPerimeter.ImagePosition = eImagePosition.Top;
            this.btExtensionPerimeter.Name = "btExtensionPerimeter";
            resources.ApplyResources(this.btExtensionPerimeter, "btExtensionPerimeter");
            this.btExtensionRuler.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionRuler.CanCustomize = false;
            this.btExtensionRuler.Image = (Image)resources.GetObject("btExtensionRuler.Image");
            this.btExtensionRuler.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionRuler.ImagePosition = eImagePosition.Top;
            this.btExtensionRuler.Name = "btExtensionRuler";
            resources.ApplyResources(this.btExtensionRuler, "btExtensionRuler");
            this.btExtensionCounter.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionCounter.CanCustomize = false;
            this.btExtensionCounter.Image = (Image)resources.GetObject("btExtensionCounter.Image");
            this.btExtensionCounter.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionCounter.ImagePosition = eImagePosition.Top;
            this.btExtensionCounter.Name = "btExtensionCounter";
            resources.ApplyResources(this.btExtensionCounter, "btExtensionCounter");
            this.ribbonExtensionDatabase.AutoOverflowEnabled = false;
            this.ribbonExtensionDatabase.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonExtensionDatabase.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonExtensionDatabase.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonExtensionDatabase, "ribbonExtensionDatabase");
            this.ribbonExtensionDatabase.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btExtensionTradesPackages,
    this.btExtensionCompactDatabase
            });

            this.ribbonExtensionDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonExtensionDatabase.Name = "ribbonExtensionDatabase";
            this.ribbonExtensionDatabase.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonExtensionDatabase.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonExtensionDatabase.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btExtensionTradesPackages.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionTradesPackages.Image = (Image)resources.GetObject("btExtensionTradesPackages.Image");
            this.btExtensionTradesPackages.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionTradesPackages.ImagePaddingHorizontal = 10;
            this.btExtensionTradesPackages.ImagePaddingVertical = 5;
            this.btExtensionTradesPackages.ImagePosition = eImagePosition.Top;
            this.btExtensionTradesPackages.Name = "btExtensionTradesPackages";
            resources.ApplyResources(this.btExtensionTradesPackages, "btExtensionTradesPackages");
            this.btExtensionCompactDatabase.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExtensionCompactDatabase.Image = (Image)resources.GetObject("btExtensionCompactDatabase.Image");
            this.btExtensionCompactDatabase.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btExtensionCompactDatabase.ImagePaddingVertical = 5;
            this.btExtensionCompactDatabase.ImagePosition = eImagePosition.Top;
            this.btExtensionCompactDatabase.Name = "btExtensionCompactDatabase";
            resources.ApplyResources(this.btExtensionCompactDatabase, "btExtensionCompactDatabase");
            resources.ApplyResources(this.contextMenuBar1, "contextMenuBar1");
            this.contextMenuBar1.Items.AddRange(new BaseItem[] { this.bEditPopup });
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = eDotNetBarStyle.StyleManagerControlled;
            this.contextMenuBar1.TabStop = false;
            this.bEditPopup.AutoExpandOnClick = true;
            this.bEditPopup.GlobalName = "bEditPopup";
            this.bEditPopup.Name = "bEditPopup";
            this.bEditPopup.PopupAnimation = ePopupAnimation.SystemDefault;
            this.bEditPopup.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
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
    this.bGroupMoveToNew
            });

            resources.ApplyResources(this.bEditPopup, "bEditPopup");
            this.bEditPopup.Visible = false;
            resources.ApplyResources(this.bAutoAdjustToZone, "bAutoAdjustToZone");
            this.bAutoAdjustToZone.BeginGroup = true;
            this.bAutoAdjustToZone.Name = "bAutoAdjustToZone";
            this.bAutoAdjustToZone.Click += new EventHandler(this.btAutoAdjustToZone_Click);
            resources.ApplyResources(this.bEditNote, "bEditNote");
            this.bEditNote.Image = Resources.note_small;
            this.bEditNote.Name = "bEditNote";
            this.bEditNote.Click += new EventHandler(this.btEditNote_Click);
            resources.ApplyResources(this.bPointInsert, "bPointInsert");
            this.bPointInsert.Name = "bPointInsert";
            this.bPointInsert.Click += new EventHandler(this.btPointInsert_Click);
            resources.ApplyResources(this.bPointRemove, "bPointRemove");
            this.bPointRemove.Name = "bPointRemove";
            this.bPointRemove.Click += new EventHandler(this.btPointRemove_Click);
            resources.ApplyResources(this.bSetHeight, "bSetHeight");
            this.bSetHeight.Name = "bSetHeight";
            this.bSetHeight.Click += new EventHandler(this.bOpeningHeight_Click);
            resources.ApplyResources(this.bGroupAddObject, "bGroupAddObject");
            this.bGroupAddObject.BeginGroup = true;
            this.bGroupAddObject.Name = "bGroupAddObject";
            this.bGroupAddObject.Click += new EventHandler(this.btGroupAddObject_Click);
            resources.ApplyResources(this.bDeductionCreate, "bDeductionCreate");
            this.bDeductionCreate.Image = Resources.deduction_small;
            this.bDeductionCreate.Name = "bDeductionCreate";
            this.bDeductionCreate.Click += new EventHandler(this.btDeductionCreate_Click);
            resources.ApplyResources(this.bDeductionsEdit, "bDeductionsEdit");
            this.bDeductionsEdit.Name = "bDeductionsEdit";
            this.bDeductionsEdit.Click += new EventHandler(this.btDeductionsEdit_Click);
            resources.ApplyResources(this.bPerimeterCreateFromArea, "bPerimeterCreateFromArea");
            this.bPerimeterCreateFromArea.Image = Resources.perimeter_small;
            this.bPerimeterCreateFromArea.Name = "bPerimeterCreateFromArea";
            this.bPerimeterCreateFromArea.Shortcuts.Add(eShortcut.CtrlP);
            this.bPerimeterCreateFromArea.Click += new EventHandler(this.btPerimeterCreateFromArea_Click);
            resources.ApplyResources(this.bOpeningCreateFromPosition, "bOpeningCreateFromPosition");
            this.bOpeningCreateFromPosition.Name = "bOpeningCreateFromPosition";
            this.bOpeningCreateFromPosition.Click += new EventHandler(this.btOpeningCreateFromPosition_Click);
            resources.ApplyResources(this.bOpeningDuplicate, "bOpeningDuplicate");
            this.bOpeningDuplicate.Name = "bOpeningDuplicate";
            this.bOpeningDuplicate.Click += new EventHandler(this.btOpeningDuplicate_Click);
            this.bOpeningCreateFromSegment.Name = "bOpeningCreateFromSegment";
            resources.ApplyResources(this.bOpeningCreateFromSegment, "bOpeningCreateFromSegment");
            this.bOpeningCreateFromSegment.Click += new EventHandler(this.btOpeningCreateFromSegment_Click);
            this.bOpeningDelete.Name = "bOpeningDelete";
            resources.ApplyResources(this.bOpeningDelete, "bOpeningDelete");
            this.bOpeningDelete.Click += new EventHandler(this.btOpeningDelete_Click);
            resources.ApplyResources(this.bDropInsert, "bDropInsert");
            this.bDropInsert.Name = "bDropInsert";
            this.bDropInsert.Click += new EventHandler(this.btDropInsert_Click);
            this.bDropRemove.Name = "bDropRemove";
            resources.ApplyResources(this.bDropRemove, "bDropRemove");
            this.bDropRemove.Click += new EventHandler(this.btDropRemove_Click);
            this.bPerimeterOpen.Name = "bPerimeterOpen";
            resources.ApplyResources(this.bPerimeterOpen, "bPerimeterOpen");
            this.bPerimeterOpen.Click += new EventHandler(this.btPerimeterOpen_Click);
            this.bPerimeterClose.Name = "bPerimeterClose";
            resources.ApplyResources(this.bPerimeterClose, "bPerimeterClose");
            this.bPerimeterClose.Click += new EventHandler(this.btPerimeterClose_Click);
            this.bAngleDegreeType.Name = "bAngleDegreeType";
            resources.ApplyResources(this.bAngleDegreeType, "bAngleDegreeType");
            this.bAngleDegreeType.Click += new EventHandler(this.btAngleDegreeType_Click);
            this.bAngleSlopeType.Name = "bAngleSlopeType";
            resources.ApplyResources(this.bAngleSlopeType, "bAngleSlopeType");
            this.bAngleSlopeType.Click += new EventHandler(this.btAngleSlopeType_Click);
            resources.ApplyResources(this.bDeductionDuplicate, "bDeductionDuplicate");
            this.bDeductionDuplicate.BeginGroup = true;
            this.bDeductionDuplicate.Name = "bDeductionDuplicate";
            this.bDeductionDuplicate.Click += new EventHandler(this.btDeductionDuplicate_Click);
            resources.ApplyResources(this.bCut, "bCut");
            this.bCut.BeginGroup = true;
            this.bCut.GlobalName = "bCut";
            this.bCut.Image = Resources.cut_16x16;
            this.bCut.ImageIndex = 5;
            this.bCut.Name = "bCut";
            this.bCut.PopupAnimation = ePopupAnimation.SystemDefault;
            this.bCut.Click += new EventHandler(this.btEditCut_Click);
            resources.ApplyResources(this.bCopy, "bCopy");
            this.bCopy.GlobalName = "bCopy";
            this.bCopy.Image = Resources.copy_16x16;
            this.bCopy.ImageIndex = 4;
            this.bCopy.Name = "bCopy";
            this.bCopy.PopupAnimation = ePopupAnimation.SystemDefault;
            this.bCopy.Click += new EventHandler(this.btEditCopy_Click);
            resources.ApplyResources(this.bPaste, "bPaste");
            this.bPaste.GlobalName = "bPaste";
            this.bPaste.Image = Resources.paste_16x16;
            this.bPaste.ImageIndex = 12;
            this.bPaste.Name = "bPaste";
            this.bPaste.PopupAnimation = ePopupAnimation.SystemDefault;
            this.bPaste.Click += new EventHandler(this.btEditPaste_Click);
            resources.ApplyResources(this.bDelete, "bDelete");
            this.bDelete.Image = Resources.delete_16x16;
            this.bDelete.Name = "bDelete";
            this.bDelete.Click += new EventHandler(this.btEditDelete_Click);
            resources.ApplyResources(this.bToggleMeasures, "bToggleMeasures");
            this.bToggleMeasures.BeginGroup = true;
            this.bToggleMeasures.Name = "bToggleMeasures";
            this.bToggleMeasures.Click += new EventHandler(this.btToggleMeasures_Click);
            resources.ApplyResources(this.bZoomToObject, "bZoomToObject");
            this.bZoomToObject.BeginGroup = true;
            this.bZoomToObject.Image = Resources.zoom_16x16;
            this.bZoomToObject.Name = "bZoomToObject";
            this.bZoomToObject.Click += new EventHandler(this.btZoomToObject_Click);
            resources.ApplyResources(this.bZoomToGroup, "bZoomToGroup");
            this.bZoomToGroup.Image = Resources.selection_16x16_alt;
            this.bZoomToGroup.Name = "bZoomToGroup";
            this.bZoomToGroup.Click += new EventHandler(this.btZoomToGroup_Click);
            resources.ApplyResources(this.bBringToFront, "bBringToFront");
            this.bBringToFront.BeginGroup = true;
            this.bBringToFront.Image = Resources.bring_to_front_16x16;
            this.bBringToFront.Name = "bBringToFront";
            this.bBringToFront.Click += new EventHandler(this.btEditBringToFront_Click);
            resources.ApplyResources(this.bSendToBack, "bSendToBack");
            this.bSendToBack.Image = Resources.send_to_back_16x16;
            this.bSendToBack.Name = "bSendToBack";
            this.bSendToBack.Click += new EventHandler(this.btEditSendToBack_Click);
            resources.ApplyResources(this.bSelectGroup, "bSelectGroup");
            this.bSelectGroup.BeginGroup = true;
            this.bSelectGroup.Name = "bSelectGroup";
            this.bSelectGroup.Click += new EventHandler(this.btEditSelectGroup_Click);
            this.bSelectThisGroup.Name = "bSelectThisGroup";
            this.bSelectThisGroup.SubItems.AddRange(new BaseItem[] { this.bSelectThisGroup1 });
            resources.ApplyResources(this.bSelectThisGroup, "bSelectThisGroup");
            this.bSelectThisGroup1.Name = "bSelectThisGroup1";
            resources.ApplyResources(this.bSelectThisGroup1, "bSelectThisGroup1");
            this.bSelectObjectType.Name = "bSelectObjectType";
            this.bSelectObjectType.SubItems.AddRange(new BaseItem[] { this.bSelectObjectType1 });
            resources.ApplyResources(this.bSelectObjectType, "bSelectObjectType");
            this.bSelectObjectType1.Name = "bSelectObjectType1";
            resources.ApplyResources(this.bSelectObjectType1, "bSelectObjectType1");
            resources.ApplyResources(this.bSelectAll, "bSelectAll");
            this.bSelectAll.GlobalName = "bSelectAll";
            this.bSelectAll.Name = "bSelectAll";
            this.bSelectAll.PopupAnimation = ePopupAnimation.SystemDefault;
            this.bSelectAll.Click += new EventHandler(this.btEditSelectAll_Click);
            resources.ApplyResources(this.bUnselectAll, "bUnselectAll");
            this.bUnselectAll.Name = "bUnselectAll";
            this.bUnselectAll.Click += new EventHandler(this.btUnselectAll_Click);
            this.bLayerMoveTo.BeginGroup = true;
            this.bLayerMoveTo.Name = "bLayerMoveTo";
            this.bLayerMoveTo.SubItems.AddRange(new BaseItem[] { this.bLayerMoveTo1 });
            resources.ApplyResources(this.bLayerMoveTo, "bLayerMoveTo");
            this.bLayerMoveTo.Click += new EventHandler(this.btLayerMoveTo_Click);
            this.bLayerMoveTo1.Name = "bLayerMoveTo1";
            resources.ApplyResources(this.bLayerMoveTo1, "bLayerMoveTo1");
            this.bGroupMoveTo.Name = "bGroupMoveTo";
            this.bGroupMoveTo.SubItems.AddRange(new BaseItem[] { this.bGroupMoveTo1 });
            resources.ApplyResources(this.bGroupMoveTo, "bGroupMoveTo");
            this.bGroupMoveTo.Click += new EventHandler(this.btGroupMoveTo_Click);
            this.bGroupMoveTo1.Name = "bGroupMoveTo1";
            resources.ApplyResources(this.bGroupMoveTo1, "bGroupMoveTo1");
            resources.ApplyResources(this.bGroupMoveToNew, "bGroupMoveToNew");
            this.bGroupMoveToNew.Name = "bGroupMoveToNew";
            this.bGroupMoveToNew.Click += new EventHandler(this.btGroupMoveToNew_Click);
            this.ribbonTabStart.Name = "ribbonTabStart";
            this.ribbonTabStart.Panel = this.ribbonPanel;
            resources.ApplyResources(this.ribbonTabStart, "ribbonTabStart");
            this.ribbonTabPlans.Name = "ribbonTabPlans";
            this.ribbonTabPlans.Panel = this.ribbonPanelPlans;
            resources.ApplyResources(this.ribbonTabPlans, "ribbonTabPlans");
            this.ribbonTabReport.Name = "ribbonTabReport";
            this.ribbonTabReport.Panel = this.ribbonPanelReport;
            resources.ApplyResources(this.ribbonTabReport, "ribbonTabReport");
            this.ribbonTabEstimatingItems.Checked = true;
            this.ribbonTabEstimatingItems.Group = this.ribbonTabItemDBManagement;
            this.ribbonTabEstimatingItems.Name = "ribbonTabEstimatingItems";
            this.ribbonTabEstimatingItems.Panel = this.ribbonPanelEstimating;
            resources.ApplyResources(this.ribbonTabEstimatingItems, "ribbonTabEstimatingItems");
            resources.ApplyResources(this.ribbonTabItemDBManagement, "ribbonTabItemDBManagement");
            this.ribbonTabItemDBManagement.Name = "ribbonTabItemDBManagement";
            this.ribbonTabItemDBManagement.Style.BackColor = Color.FromArgb(174, 109, 148);
            this.ribbonTabItemDBManagement.Style.BackColor2 = Color.FromArgb(144, 72, 123);
            this.ribbonTabItemDBManagement.Style.BackColorGradientAngle = 90;
            this.ribbonTabItemDBManagement.Style.BorderBottom = eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderBottomWidth = 1;
            this.ribbonTabItemDBManagement.Style.BorderColor = Color.FromArgb(154, 58, 59);
            this.ribbonTabItemDBManagement.Style.BorderLeft = eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderLeftWidth = 1;
            this.ribbonTabItemDBManagement.Style.BorderRight = eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderRightWidth = 1;
            this.ribbonTabItemDBManagement.Style.BorderTop = eStyleBorderType.Solid;
            this.ribbonTabItemDBManagement.Style.BorderTopWidth = 1;
            this.ribbonTabItemDBManagement.Style.CornerType = eCornerType.Square;
            this.ribbonTabItemDBManagement.Style.TextAlignment = eStyleTextAlignment.Center;
            this.ribbonTabItemDBManagement.Style.TextColor = Color.White;
            this.ribbonTabItemDBManagement.Style.TextLineAlignment = eStyleTextAlignment.Near;
            this.ribbonTabItemDBManagement.Style.TextShadowColor = Color.Black;
            this.ribbonTabItemDBManagement.Style.TextShadowOffset = new Point(1, 1);
            this.ribbonTabTemplates.Group = this.ribbonTabItemDBManagement;
            this.ribbonTabTemplates.Name = "ribbonTabTemplates";
            this.ribbonTabTemplates.Panel = this.ribbonPanelTemplates;
            resources.ApplyResources(this.ribbonTabTemplates, "ribbonTabTemplates");
            this.ribbonTabExtensions.Group = this.ribbonTabItemDBManagement;
            this.ribbonTabExtensions.Name = "ribbonTabExtensions";
            this.ribbonTabExtensions.Panel = this.ribbonPanelExtensions;
            resources.ApplyResources(this.ribbonTabExtensions, "ribbonTabExtensions");
            this.ribbonTabExtensions.Visible = false;
            this.lblTrialMessage.ButtonStyle = eButtonStyle.TextOnlyAlways;
            this.lblTrialMessage.ItemAlignment = eItemAlignment.Far;
            this.lblTrialMessage.Name = "lblTrialMessage";
            this.lblTrialMessage.Click += new EventHandler(this.btLicenseActivate_Click);
            this.btLicensing.AutoExpandOnClick = true;
            this.btLicensing.ButtonStyle = eButtonStyle.ImageAndText;
            this.btLicensing.Image = Resources.activate_16x16;
            this.btLicensing.ItemAlignment = eItemAlignment.Far;
            this.btLicensing.Name = "btLicensing";
            this.btLicensing.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btLicenseBuy,
    this.btLicenseActivate
            });

            resources.ApplyResources(this.btLicensing, "btLicensing");
            this.btLicensing.Click += new EventHandler(this.btLicensing_Click);
            this.btLicenseBuy.Name = "btLicenseBuy";
            resources.ApplyResources(this.btLicenseBuy, "btLicenseBuy");
            this.btLicenseBuy.Click += new EventHandler(this.btLicenseBuy_Click);
            this.btLicenseActivate.Name = "btLicenseActivate";
            resources.ApplyResources(this.btLicenseActivate, "btLicenseActivate");
            this.btLicenseActivate.Click += new EventHandler(this.btLicenseActivate_Click);
            this.btSettings.AutoExpandOnClick = true;
            this.btSettings.Image = Resources.settings_16x16;
            this.btSettings.ItemAlignment = eItemAlignment.Far;
            this.btSettings.Name = "btSettings";
            this.btSettings.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
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
    this.btResetDefaultPanelsLayout
            });

            this.lblLanguage.BackColor = Color.FromArgb(221, 231, 238);
            this.lblLanguage.BorderSide = eBorderSide.None;
            this.lblLanguage.CanCustomize = false;
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.PaddingBottom = 3;
            this.lblLanguage.PaddingLeft = 10;
            this.lblLanguage.PaddingTop = 3;
            this.lblLanguage.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblLanguage, "lblLanguage");
            this.btLanguageEnglish.Name = "btLanguageEnglish";
            this.btLanguageEnglish.OptionGroup = "Language";
            resources.ApplyResources(this.btLanguageEnglish, "btLanguageEnglish");
            this.btLanguageEnglish.Click += new EventHandler(this.btLanguageEnglish_Click);
            this.btLanguageFrench.Name = "btLanguageFrench";
            this.btLanguageFrench.OptionGroup = "Language";
            resources.ApplyResources(this.btLanguageFrench, "btLanguageFrench");
            this.btLanguageFrench.Click += new EventHandler(this.btLanguageFrench_Click);
            this.btLanguageSpanish.Name = "btLanguageSpanish";
            this.btLanguageSpanish.OptionGroup = "Language";
            resources.ApplyResources(this.btLanguageSpanish, "btLanguageSpanish");
            this.btLanguageSpanish.Click += new EventHandler(this.btLanguageSpanish_Click);
            this.lblScrollSpeed.BackColor = Color.FromArgb(221, 231, 238);
            this.lblScrollSpeed.BorderSide = eBorderSide.None;
            this.lblScrollSpeed.CanCustomize = false;
            this.lblScrollSpeed.Name = "lblScrollSpeed";
            this.lblScrollSpeed.PaddingBottom = 3;
            this.lblScrollSpeed.PaddingLeft = 10;
            this.lblScrollSpeed.PaddingTop = 3;
            this.lblScrollSpeed.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblScrollSpeed, "lblScrollSpeed");
            this.sliderScrollSpeed.AutoCollapseOnClick = false;
            this.sliderScrollSpeed.CanCustomize = false;
            this.sliderScrollSpeed.LabelVisible = false;
            this.sliderScrollSpeed.LabelWidth = 50;
            this.sliderScrollSpeed.Name = "sliderScrollSpeed";
            this.sliderScrollSpeed.Step = 5;
            this.sliderScrollSpeed.TextColor = Color.Black;
            this.sliderScrollSpeed.TrackMarker = false;
            this.sliderScrollSpeed.Value = 0;
            this.sliderScrollSpeed.Width = 150;
            this.containerScroll.BackgroundStyle.CornerType = eCornerType.Square;
            this.containerScroll.FixedSize = new Size(0, 16);
            this.containerScroll.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.containerScroll.Name = "containerScroll";
            this.containerScroll.ResizeItemsToFit = false;
            this.containerScroll.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblScrollFast,
    this.lblScrollPadding,
    this.lblScrollSlow
            });

            this.containerScroll.TitleStyle.CornerType = eCornerType.Square;
            this.lblScrollFast.CanCustomize = false;
            this.lblScrollFast.Name = "lblScrollFast";
            resources.ApplyResources(this.lblScrollFast, "lblScrollFast");
            this.lblScrollFast.TextAlignment = StringAlignment.Center;
            this.lblScrollFast.WordWrap = true;
            this.lblScrollPadding.CanCustomize = false;
            this.lblScrollPadding.Name = "lblScrollPadding";
            this.lblScrollPadding.Stretch = true;
            this.lblScrollPadding.Width = 100;
            this.lblScrollSlow.CanCustomize = false;
            this.lblScrollSlow.Name = "lblScrollSlow";
            resources.ApplyResources(this.lblScrollSlow, "lblScrollSlow");
            this.lblScrollSlow.TextAlignment = StringAlignment.Center;
            this.lblScrollSlow.WordWrap = true;
            this.lblDataFolder.BackColor = Color.FromArgb(221, 231, 238);
            this.lblDataFolder.BorderSide = eBorderSide.None;
            this.lblDataFolder.CanCustomize = false;
            this.lblDataFolder.Name = "lblDataFolder";
            this.lblDataFolder.PaddingBottom = 3;
            this.lblDataFolder.PaddingLeft = 10;
            this.lblDataFolder.PaddingTop = 3;
            this.lblDataFolder.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblDataFolder, "lblDataFolder");
            this.lblDataFolder.Visible = false;
            this.lblPersonalPreferences.BackColor = Color.FromArgb(221, 231, 238);
            this.lblPersonalPreferences.BorderSide = eBorderSide.None;
            this.lblPersonalPreferences.CanCustomize = false;
            this.lblPersonalPreferences.Name = "lblPersonalPreferences";
            this.lblPersonalPreferences.PaddingBottom = 3;
            this.lblPersonalPreferences.PaddingLeft = 10;
            this.lblPersonalPreferences.PaddingTop = 3;
            this.lblPersonalPreferences.SingleLineColor = Color.FromArgb(197, 197, 197);
            this.lblPersonalPreferences.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.buttonItem3
            });

            resources.ApplyResources(this.lblPersonalPreferences, "lblPersonalPreferences");
            this.buttonItem3.Name = "buttonItem3";
            resources.ApplyResources(this.buttonItem3, "buttonItem3");
            this.btSelectDataFolder.Name = "btSelectDataFolder";
            resources.ApplyResources(this.btSelectDataFolder, "btSelectDataFolder");
            this.btSelectDataFolder.Click += new EventHandler(this.btSelectDataFolder_Click);
            this.btPersonalPreferences.Name = "btPersonalPreferences";
            resources.ApplyResources(this.btPersonalPreferences, "btPersonalPreferences");
            this.btPersonalPreferences.Click += new EventHandler(this.btPersonalPreferences_Click);
            this.btImportationPreferences.Name = "btImportationPreferences";
            resources.ApplyResources(this.btImportationPreferences, "btImportationPreferences");
            this.btImportationPreferences.Click += new EventHandler(this.btImportationPreferences_Click);
            this.btEnableAutoBackup.BeginGroup = true;
            this.btEnableAutoBackup.Name = "btEnableAutoBackup";
            resources.ApplyResources(this.btEnableAutoBackup, "btEnableAutoBackup");
            this.btEnableAutoBackup.Click += new EventHandler(this.btEnableAutoBackup_Click);
            this.btSetDBReadOnly.Name = "btSetDBReadOnly";
            resources.ApplyResources(this.btSetDBReadOnly, "btSetDBReadOnly");
            this.btSetDBReadOnly.Click += new EventHandler(this.btSetDBReadOnly_Click);
            this.lblTheme.BackColor = Color.FromArgb(221, 231, 238);
            this.lblTheme.BorderSide = eBorderSide.None;
            this.lblTheme.CanCustomize = false;
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.PaddingBottom = 3;
            this.lblTheme.PaddingLeft = 10;
            this.lblTheme.PaddingTop = 3;
            this.lblTheme.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblTheme, "lblTheme");
            this.btStyleMetro.CanCustomize = false;
            this.btStyleMetro.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleMetro, "btStyleMetro");
            this.btStyleMetro.Name = "btStyleMetro";
            this.btStyleMetro.OptionGroup = "style";
            this.AppCommandTheme.Name = "AppCommandTheme";
            this.AppCommandTheme.Executed += new EventHandler(this.AppCommandTheme_Executed);
            this.btStyleClassicBlue.BeginGroup = true;
            this.btStyleClassicBlue.CanCustomize = false;
            this.btStyleClassicBlue.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleClassicBlue, "btStyleClassicBlue");
            this.btStyleClassicBlue.Name = "btStyleClassicBlue";
            this.btStyleClassicBlue.OptionGroup = "style";
            this.btStyleClassicSilver.CanCustomize = false;
            this.btStyleClassicSilver.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleClassicSilver, "btStyleClassicSilver");
            this.btStyleClassicSilver.Name = "btStyleClassicSilver";
            this.btStyleClassicSilver.OptionGroup = "style";
            this.btStyleClassicBlack.CanCustomize = false;
            this.btStyleClassicBlack.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleClassicBlack, "btStyleClassicBlack");
            this.btStyleClassicBlack.Name = "btStyleClassicBlack";
            this.btStyleClassicBlack.OptionGroup = "style";
            this.btStyleClassicExecutive.CanCustomize = false;
            this.btStyleClassicExecutive.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleClassicExecutive, "btStyleClassicExecutive");
            this.btStyleClassicExecutive.Name = "btStyleClassicExecutive";
            this.btStyleClassicExecutive.OptionGroup = "style";
            this.btStyleRetroBlue.BeginGroup = true;
            this.btStyleRetroBlue.CanCustomize = false;
            this.btStyleRetroBlue.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleRetroBlue, "btStyleRetroBlue");
            this.btStyleRetroBlue.Name = "btStyleRetroBlue";
            this.btStyleRetroBlue.OptionGroup = "style";
            this.btStyleRetroSilver.CanCustomize = false;
            this.btStyleRetroSilver.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleRetroSilver, "btStyleRetroSilver");
            this.btStyleRetroSilver.Name = "btStyleRetroSilver";
            this.btStyleRetroSilver.OptionGroup = "style";
            this.btStyleRetroBlack.CanCustomize = false;
            this.btStyleRetroBlack.Checked = true;
            this.btStyleRetroBlack.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleRetroBlack, "btStyleRetroBlack");
            this.btStyleRetroBlack.Name = "btStyleRetroBlack";
            this.btStyleRetroBlack.OptionGroup = "style";
            this.btStyleRetroGlass.CanCustomize = false;
            this.btStyleRetroGlass.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleRetroGlass, "btStyleRetroGlass");
            this.btStyleRetroGlass.Name = "btStyleRetroGlass";
            this.btStyleRetroGlass.OptionGroup = "style";
            this.btStyleModern.BeginGroup = true;
            this.btStyleModern.CanCustomize = false;
            this.btStyleModern.Command = this.AppCommandTheme;
            resources.ApplyResources(this.btStyleModern, "btStyleModern");
            this.btStyleModern.Name = "btStyleModern";
            this.btStyleModern.OptionGroup = "style";
            this.btStyleModern.Visible = false;
            this.btSetThemeColor.BeginGroup = true;
            this.btSetThemeColor.CanCustomize = false;
            this.btSetThemeColor.Command = this.AppCommandTheme;
            this.btSetThemeColor.DisplayMoreColors = false;
            this.btSetThemeColor.Name = "btSetThemeColor";
            resources.ApplyResources(this.btSetThemeColor, "btSetThemeColor");
            this.btSetThemeColor.SelectedColorChanged += new EventHandler(this.btSetThemeColor_SelectedColorChanged);
            this.btSetThemeColor.ColorPreview += new ColorPreviewEventHandler(this.btSetThemeColor_ColorPreview);
            this.btSetThemeColor.PopupShowing += new EventHandler(this.btSetThemeColor_PopupShowing);
            this.btSetThemeColor.ExpandChange += new EventHandler(this.btSetThemeColor_ExpandChange);
            this.lblPanels.BackColor = Color.FromArgb(221, 231, 238);
            this.lblPanels.BorderSide = eBorderSide.None;
            this.lblPanels.CanCustomize = false;
            this.lblPanels.Name = "lblPanels";
            this.lblPanels.PaddingBottom = 3;
            this.lblPanels.PaddingLeft = 10;
            this.lblPanels.PaddingTop = 3;
            this.lblPanels.SingleLineColor = Color.FromArgb(197, 197, 197);
            resources.ApplyResources(this.lblPanels, "lblPanels");
            this.btResetDefaultPanelsLayout.Name = "btResetDefaultPanelsLayout";
            resources.ApplyResources(this.btResetDefaultPanelsLayout, "btResetDefaultPanelsLayout");
            this.btResetDefaultPanelsLayout.Click += new EventHandler(this.btResetDefaultPanelsLayout_Click);
            this.btHelp.Image = (Image)resources.GetObject("btHelp.Image");
            this.btHelp.Name = "btHelp";
            resources.ApplyResources(this.btHelp, "btHelp");
            this.btHelp.Click += new EventHandler(this.btHelp_Click);
            this.startButton.AutoExpandOnClick = true;
            this.startButton.CanCustomize = false;
            this.startButton.HotTrackingStyle = eHotTrackingStyle.Image;
            this.startButton.Image = Resources.ruler;
            this.startButton.ImagePaddingHorizontal = 2;
            this.startButton.ImagePaddingVertical = 2;
            this.startButton.Name = "startButton";
            this.startButton.ShowSubItems = false;
            this.startButton.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.itemContainerFileMenu
            });

            resources.ApplyResources(this.startButton, "startButton");
            this.startButton.PopupOpen += new DotNetBarManager.PopupOpenEventHandler(this.startButton_PopupOpen);
            this.startButton.PopupClose += new EventHandler(this.startButton_PopupClose);
            this.itemContainerFileMenu.BackgroundStyle.Class = "RibbonFileMenuTwoColumnContainer";
            this.itemContainerFileMenu.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerFileMenu.CanCustomize = false;
            this.itemContainerFileMenu.ItemSpacing = 0;
            this.itemContainerFileMenu.Name = "itemContainerFileMenu";
            this.itemContainerFileMenu.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.itemContainerFileMenu2,
    this.galleryRecentProjects
            });

            this.itemContainerFileMenu.TitleStyle.CornerType = eCornerType.Square;
            this.itemContainerFileMenu2.BackgroundStyle.Class = "RibbonFileMenuColumnOneContainer";
            this.itemContainerFileMenu2.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerFileMenu2.CanCustomize = false;
            this.itemContainerFileMenu2.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerFileMenu2.MinimumSize = new Size(200, 0);
            this.itemContainerFileMenu2.Name = "itemContainerFileMenu2";
            this.itemContainerFileMenu2.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
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
    this.btExit
            });

            this.itemContainerFileMenu2.TitleStyle.CornerType = eCornerType.Square;
            this.btProjectNew.ButtonStyle = eButtonStyle.ImageAndText;
            this.btProjectNew.CanCustomize = false;
            this.btProjectNew.Image = (Image)resources.GetObject("btProjectNew.Image");
            this.btProjectNew.ImagePaddingVertical = 5;
            this.btProjectNew.Name = "btProjectNew";
            this.btProjectNew.Shortcuts.Add(eShortcut.CtrlN);
            this.btProjectNew.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btProjectNew, "btProjectNew");
            this.btProjectNew.Click += new EventHandler(this.btProjectNew_Click);
            this.btProjectOpen.ButtonStyle = eButtonStyle.ImageAndText;
            this.btProjectOpen.CanCustomize = false;
            this.btProjectOpen.Image = (Image)resources.GetObject("btProjectOpen.Image");
            this.btProjectOpen.ImagePaddingVertical = 5;
            this.btProjectOpen.Name = "btProjectOpen";
            this.btProjectOpen.Shortcuts.Add(eShortcut.CtrlO);
            this.btProjectOpen.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btProjectOpen, "btProjectOpen");
            this.btProjectOpen.Click += new EventHandler(this.btProjectOpen_Click);
            this.btProjectSave.ButtonStyle = eButtonStyle.ImageAndText;
            this.btProjectSave.CanCustomize = false;
            this.btProjectSave.Image = (Image)resources.GetObject("btProjectSave.Image");
            this.btProjectSave.ImagePaddingVertical = 5;
            this.btProjectSave.Name = "btProjectSave";
            this.btProjectSave.Shortcuts.Add(eShortcut.CtrlS);
            this.btProjectSave.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btProjectSave, "btProjectSave");
            this.btProjectSave.Click += new EventHandler(this.btProjectSave_Click);
            this.btProjectSaveAs.ButtonStyle = eButtonStyle.ImageAndText;
            this.btProjectSaveAs.CanCustomize = false;
            this.btProjectSaveAs.Image = (Image)resources.GetObject("btProjectSaveAs.Image");
            this.btProjectSaveAs.ImagePaddingVertical = 5;
            this.btProjectSaveAs.Name = "btProjectSaveAs";
            this.btProjectSaveAs.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btProjectSaveAs, "btProjectSaveAs");
            this.btProjectSaveAs.Click += new EventHandler(this.btProjectSaveAs_Click);
            this.btProjectInfo.BeginGroup = true;
            this.btProjectInfo.ButtonStyle = eButtonStyle.ImageAndText;
            this.btProjectInfo.CanCustomize = false;
            this.btProjectInfo.Image = Resources.properties;
            this.btProjectInfo.Name = "btProjectInfo";
            resources.ApplyResources(this.btProjectInfo, "btProjectInfo");
            this.btProjectInfo.Click += new EventHandler(this.btProjectInfo_Click);
            this.btProjectClose.BeginGroup = true;
            this.btProjectClose.ButtonStyle = eButtonStyle.ImageAndText;
            this.btProjectClose.CanCustomize = false;
            this.btProjectClose.Image = (Image)resources.GetObject("btProjectClose.Image");
            this.btProjectClose.ImagePaddingVertical = 5;
            this.btProjectClose.Name = "btProjectClose";
            this.btProjectClose.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btProjectClose, "btProjectClose");
            this.btProjectClose.Click += new EventHandler(this.btProjectClose_Click);
            this.btHelpContent.BeginGroup = true;
            this.btHelpContent.ButtonStyle = eButtonStyle.ImageAndText;
            this.btHelpContent.CanCustomize = false;
            this.btHelpContent.Image = (Image)resources.GetObject("btHelpContent.Image");
            this.btHelpContent.ImagePaddingVertical = 5;
            this.btHelpContent.Name = "btHelpContent";
            this.btHelpContent.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btHelpContent, "btHelpContent");
            this.btHelpContent.Click += new EventHandler(this.btHelpContent_Click);
            this.btHelpYoutube.ButtonStyle = eButtonStyle.ImageAndText;
            this.btHelpYoutube.CanCustomize = false;
            this.btHelpYoutube.Image = (Image)resources.GetObject("btHelpYoutube.Image");
            this.btHelpYoutube.ImagePaddingVertical = 5;
            this.btHelpYoutube.Name = "btHelpYoutube";
            this.btHelpYoutube.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btHelpYoutube, "btHelpYoutube");
            this.btHelpYoutube.Click += new EventHandler(this.btHelpYoutube_Click);
            this.btHelpAbout.ButtonStyle = eButtonStyle.ImageAndText;
            this.btHelpAbout.CanCustomize = false;
            this.btHelpAbout.Image = (Image)resources.GetObject("btHelpAbout.Image");
            this.btHelpAbout.ImagePaddingVertical = 5;
            this.btHelpAbout.Name = "btHelpAbout";
            this.btHelpAbout.SubItemsExpandWidth = 24;
            resources.ApplyResources(this.btHelpAbout, "btHelpAbout");
            this.btHelpAbout.Click += new EventHandler(this.btHelpAbout_Click);
            this.btLicenseDeactivate.BeginGroup = true;
            this.btLicenseDeactivate.ButtonStyle = eButtonStyle.ImageAndText;
            this.btLicenseDeactivate.CanCustomize = false;
            this.btLicenseDeactivate.Image = Resources.deactivate;
            this.btLicenseDeactivate.ImagePaddingVertical = 5;
            this.btLicenseDeactivate.Name = "btLicenseDeactivate";
            resources.ApplyResources(this.btLicenseDeactivate, "btLicenseDeactivate");
            this.btLicenseDeactivate.Click += new EventHandler(this.btLicenseDeactivate_Click);
            this.btExit.BeginGroup = true;
            this.btExit.ButtonStyle = eButtonStyle.ImageAndText;
            this.btExit.CanCustomize = false;
            this.btExit.Image = Resources.exit;
            this.btExit.Name = "btExit";
            this.btExit.Shortcuts.Add(eShortcut.AltF4);
            resources.ApplyResources(this.btExit, "btExit");
            this.btExit.Click += new EventHandler(this.btExit_Click);
            this.galleryRecentProjects.BackgroundStyle.Class = "RibbonFileMenuColumnTwoContainer";
            this.galleryRecentProjects.BackgroundStyle.CornerType = eCornerType.Square;
            this.galleryRecentProjects.CanCustomize = false;
            this.galleryRecentProjects.DefaultSize = new Size(0x166, 240);
            this.galleryRecentProjects.EnableGalleryPopup = false;
            this.galleryRecentProjects.FixedSize = new Size(0x166, 240);
            this.galleryRecentProjects.LayoutOrientation = eOrientation.Vertical;
            this.galleryRecentProjects.MinimumSize = new Size(0x166, 240);
            this.galleryRecentProjects.MultiLine = false;
            this.galleryRecentProjects.Name = "galleryRecentProjects";
            this.galleryRecentProjects.PopupUsesStandardScrollbars = false;
            this.galleryRecentProjects.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblFileRecentProjects
            });

            this.galleryRecentProjects.TitleStyle.CornerType = eCornerType.Square;
            this.lblFileRecentProjects.BorderSide = eBorderSide.Bottom;
            this.lblFileRecentProjects.BorderType = eBorderType.Etched;
            this.lblFileRecentProjects.CanCustomize = false;
            this.lblFileRecentProjects.ForeColor = SystemColors.ControlText;
            this.lblFileRecentProjects.Name = "lblFileRecentProjects";
            this.lblFileRecentProjects.PaddingBottom = 2;
            this.lblFileRecentProjects.PaddingTop = 2;
            this.lblFileRecentProjects.Stretch = true;
            resources.ApplyResources(this.lblFileRecentProjects, "lblFileRecentProjects");
            this.btSave.Image = Resources.document_save_16x16;
            this.btSave.Name = "btSave";
            this.btSave.Click += new EventHandler(this.btProjectSave_Click);
            this.btUndo.BeginGroup = true;
            this.btUndo.Image = Resources.undo_icon__16x16;
            this.btUndo.Name = "btUndo";
            this.btUndo.Click += new EventHandler(this.btEditUndo_Click);
            this.btRedo.Image = Resources.redo_icon_16x16;
            this.btRedo.Name = "btRedo";
            this.btRedo.Click += new EventHandler(this.btEditRedo_Click);
            this.galleryGroup1.Name = "galleryGroup1";
            resources.ApplyResources(this.galleryGroup1, "galleryGroup1");
            resources.ApplyResources(this.barStatus, "barStatus");
            this.barStatus.AccessibleRole = AccessibleRole.StatusBar;
            this.barStatus.AntiAlias = true;
            this.barStatus.BarType = eBarType.StatusBar;
            this.barStatus.GrabHandleStyle = eGrabHandleStyle.ResizeHandle;
            this.barStatus.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblStatus,
    this.lblStatusBarPadding,
    this.lblOrtho,
    this.switchOrtho,
    this.lblStatusBarPadding2,
    this.lblImageQuality,
    this.qualitySlider,
    this.lblStatusBarPadding3,
    this.lblZoom,
    this.zoomSlider
            });

            this.barStatus.Name = "barStatus";
            this.barStatus.PaddingBottom = 3;
            this.barStatus.PaddingTop = 2;
            this.barStatus.Stretch = true;
            this.barStatus.Style = eDotNetBarStyle.StyleManagerControlled;
            this.barStatus.TabStop = false;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.PaddingBottom = 3;
            this.lblStatus.PaddingTop = 3;
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.TextAlignment = StringAlignment.Center;
            this.lblStatusBarPadding.Name = "lblStatusBarPadding";
            this.lblStatusBarPadding.Width = 3;
            this.lblOrtho.Name = "lblOrtho";
            this.lblOrtho.PaddingBottom = 1;
            this.lblOrtho.PaddingRight = 3;
            resources.ApplyResources(this.lblOrtho, "lblOrtho");
            this.lblOrtho.TextAlignment = StringAlignment.Far;
            this.switchOrtho.ButtonWidth = 100;
            this.switchOrtho.Name = "switchOrtho";
            this.switchOrtho.OffBackColor = Color.Linen;
            this.switchOrtho.OffTextColor = Color.FromArgb(64, 64, 64);
            this.switchOrtho.OnBackColor = Color.ForestGreen;
            this.switchOrtho.OnTextColor = Color.White;
            this.switchOrtho.SwitchWidth = 50;
            this.switchOrtho.ValueChanged += new EventHandler(this.switchOrtho_ValueChanged);
            this.lblStatusBarPadding2.Name = "lblStatusBarPadding2";
            this.lblStatusBarPadding2.Width = 2;
            this.lblImageQuality.Name = "lblImageQuality";
            this.lblImageQuality.PaddingBottom = 1;
            this.lblImageQuality.Text = Resources.Qualité_Haute;
            this.lblImageQuality.TextAlignment = StringAlignment.Center;
            this.lblImageQuality.Width = 90;
            this.qualitySlider.LabelVisible = false;
            this.qualitySlider.Maximum = 1;
            this.qualitySlider.Name = "qualitySlider";
            resources.ApplyResources(this.qualitySlider, "qualitySlider");
            this.qualitySlider.TrackMarker = false;
            this.qualitySlider.Value = 1;
            this.qualitySlider.Width = 60;
            this.qualitySlider.ValueChanged += new EventHandler(this.qualitySlider_ValueChanged);
            this.lblStatusBarPadding3.Name = "lblStatusBarPadding3";
            this.lblStatusBarPadding3.Width = 3;
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.PaddingBottom = 1;
            this.lblZoom.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblStatusPadding2
            });

            resources.ApplyResources(this.lblZoom, "lblZoom");
            this.lblZoom.TextAlignment = StringAlignment.Center;
            this.lblZoom.Width = 35;
            this.lblStatusPadding2.Name = "lblStatusPadding2";
            this.lblStatusPadding2.Width = 5;
            this.zoomSlider.LabelVisible = false;
            this.zoomSlider.Maximum = 0x12c;
            this.zoomSlider.Minimum = 10;
            this.zoomSlider.Name = "zoomSlider";
            resources.ApplyResources(this.zoomSlider, "zoomSlider");
            this.zoomSlider.TrackMarker = false;
            this.zoomSlider.Value = 0;
            this.zoomSlider.Width = 200;
            this.zoomSlider.ValueChanged += new EventHandler(this.zoomSlider_ValueChanged);
            this.zoomSlider.MouseUp += new MouseEventHandler(this.zoomSlider_MouseUp);
            this.lstLayers.AllowDrop = true;
            this.lstLayers.AllowUserToResizeColumns = false;
            this.lstLayers.BackColor = SystemColors.Window;
            this.lstLayers.BackgroundStyle.BorderColor = Color.Transparent;
            this.lstLayers.BackgroundStyle.BorderColor2 = Color.Transparent;
            this.lstLayers.BackgroundStyle.Class = "TreeBorderKey";
            this.lstLayers.BackgroundStyle.CornerType = eCornerType.Square;
            this.lstLayers.CellEdit = true;
            this.lstLayers.Columns.Add(this.columnLayerVisible);
            this.lstLayers.Columns.Add(this.columnLayerName);
            this.lstLayers.Columns.Add(this.columnLayerOpacity);
            this.lstLayers.ColumnsVisible = false;
            resources.ApplyResources(this.lstLayers, "lstLayers");
            this.lstLayers.DragDropEnabled = false;
            this.lstLayers.DragDropNodeCopyEnabled = false;
            this.lstLayers.ExpandBorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.lstLayers.ExpandWidth = 0;
            this.lstLayers.GridColumnLineResizeEnabled = true;
            this.lstLayers.GridColumnLines = false;
            this.lstLayers.HotTracking = true;
            this.lstLayers.HScrollBarVisible = false;
            this.lstLayers.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.NodeHorizontalSpacing = 0;
            this.lstLayers.NodesConnector = this.nodeConnector2;
            this.lstLayers.NodeSpacing = 1;
            this.lstLayers.NodeStyle = this.elementStyle1;
            this.lstLayers.NodeStyleMouseOver = this.elementStyle8;
            this.lstLayers.NodeStyleSelected = this.elementStyle2;
            this.lstLayers.PathSeparator = ";";
            this.lstLayers.Styles.Add(this.elementStyle1);
            this.lstLayers.Styles.Add(this.elementStyle2);
            this.lstLayers.Styles.Add(this.elementStyle8);
            this.columnLayerVisible.Name = "columnLayerVisible";
            resources.ApplyResources(this.columnLayerVisible, "columnLayerVisible");
            this.columnLayerVisible.Width.AutoSize = true;
            this.columnLayerName.MaxInputLength = 50;
            this.columnLayerName.Name = "columnLayerName";
            this.columnLayerName.StretchToFill = true;
            resources.ApplyResources(this.columnLayerName, "columnLayerName");
            this.columnLayerOpacity.Name = "columnLayerOpacity";
            resources.ApplyResources(this.columnLayerOpacity, "columnLayerOpacity");
            this.columnLayerOpacity.Width.Absolute = 90;
            this.nodeConnector2.LineColor = SystemColors.ControlText;
            this.elementStyle1.BorderTopWidth = 1;
            this.elementStyle1.CornerType = eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = SystemColors.ControlText;
            this.elementStyle8.BackColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.elementStyle8.BackColor2 = Color.FromArgb(210, 224, 252);
            this.elementStyle8.BackColorGradientAngle = 90;
            this.elementStyle8.BorderBottom = eStyleBorderType.Solid;
            this.elementStyle8.BorderBottomWidth = 1;
            this.elementStyle8.BorderColor = Color.DarkGray;
            this.elementStyle8.BorderLeft = eStyleBorderType.Solid;
            this.elementStyle8.BorderLeftWidth = 1;
            this.elementStyle8.BorderRight = eStyleBorderType.Solid;
            this.elementStyle8.BorderRightWidth = 1;
            this.elementStyle8.BorderTop = eStyleBorderType.Solid;
            this.elementStyle8.BorderTopWidth = 1;
            this.elementStyle8.CornerDiameter = 4;
            this.elementStyle8.CornerType = eCornerType.Rounded;
            this.elementStyle8.Description = "BlueLight";
            this.elementStyle8.Name = "elementStyle8";
            this.elementStyle8.PaddingBottom = 1;
            this.elementStyle8.PaddingLeft = 1;
            this.elementStyle8.PaddingRight = 1;
            this.elementStyle8.PaddingTop = 1;
            this.elementStyle8.TextColor = Color.FromArgb(69, 84, 115);
            this.elementStyle2.BackColor2SchemePart = eColorSchemePart.ItemCheckedBackground2;
            this.elementStyle2.BackColorGradientAngle = 90;
            this.elementStyle2.BackColorSchemePart = eColorSchemePart.ItemHotBackground;
            this.elementStyle2.BorderBottomWidth = 1;
            this.elementStyle2.BorderColor = Color.DarkGray;
            this.elementStyle2.BorderLeftWidth = 1;
            this.elementStyle2.BorderRightWidth = 1;
            this.elementStyle2.BorderTopWidth = 1;
            this.elementStyle2.CornerType = eCornerType.Square;
            this.elementStyle2.Description = "Yellow";
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.PaddingBottom = 1;
            this.elementStyle2.PaddingLeft = 1;
            this.elementStyle2.PaddingRight = 1;
            this.elementStyle2.PaddingTop = 1;
            this.elementStyle2.TextColor = Color.Black;
            this.barLayers.CanAutoHide = false;
            this.barLayers.CanCustomize = false;
            this.barLayers.CanDockBottom = false;
            this.barLayers.CanDockLeft = false;
            this.barLayers.CanDockRight = false;
            this.barLayers.CanDockTab = false;
            this.barLayers.CanDockTop = false;
            this.barLayers.CanMove = false;
            this.barLayers.CanReorderTabs = false;
            this.barLayers.CanUndock = false;
            this.barLayers.ColorScheme.PredefinedColorScheme = ePredefinedColorScheme.Silver2003;
            resources.ApplyResources(this.barLayers, "barLayers");
            this.barLayers.DockTabAlignment = eTabStripAlignment.Left;
            this.barLayers.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btLayerAdd,
    this.btLayerRemove,
    this.btLayerRename,
    this.btLayerMoveUp,
    this.btLayerMoveDown,
    this.btLayerSaveList,
    this.btLayerSaveListAs,
    this.btLayerOpenList,
    this.btLayersToggle
            });

            this.barLayers.Name = "barLayers";
            this.barLayers.RoundCorners = false;
            this.barLayers.SaveLayoutChanges = false;
            this.barLayers.Stretch = true;
            this.barLayers.Style = eDotNetBarStyle.StyleManagerControlled;
            this.barLayers.TabStop = false;
            this.btLayerAdd.Image = (Image)resources.GetObject("btLayerAdd.Image");
            this.btLayerAdd.ImageFixedSize = new Size(16, 16);
            this.btLayerAdd.Name = "btLayerAdd";
            this.btLayerAdd.Click += new EventHandler(this.btLayerAdd_Click);
            this.btLayerRemove.Image = Resources.delete_16x16;
            this.btLayerRemove.ImageFixedSize = new Size(16, 16);
            this.btLayerRemove.Name = "btLayerRemove";
            this.btLayerRemove.Click += new EventHandler(this.btLayerRemove_Click);
            this.btLayerRename.Image = (Image)resources.GetObject("btLayerRename.Image");
            this.btLayerRename.ImageFixedSize = new Size(16, 16);
            this.btLayerRename.Name = "btLayerRename";
            this.btLayerRename.Click += new EventHandler(this.btLayerEdit_Click);
            this.btLayerMoveUp.BeginGroup = true;
            this.btLayerMoveUp.Image = Resources.move_up_16x16;
            this.btLayerMoveUp.ImageFixedSize = new Size(16, 16);
            this.btLayerMoveUp.Name = "btLayerMoveUp";
            this.btLayerMoveUp.Click += new EventHandler(this.btLayerMoveUp_Click);
            this.btLayerMoveDown.Image = Resources.move_down_16x16;
            this.btLayerMoveDown.ImageFixedSize = new Size(16, 16);
            this.btLayerMoveDown.Name = "btLayerMoveDown";
            this.btLayerMoveDown.Click += new EventHandler(this.btLayerMoveDown_Click);
            this.btLayerSaveList.BeginGroup = true;
            this.btLayerSaveList.Image = Resources.document_save_16x16;
            this.btLayerSaveList.ImageFixedSize = new Size(16, 16);
            this.btLayerSaveList.Name = "btLayerSaveList";
            this.btLayerSaveList.Click += new EventHandler(this.btLayerSaveList_Click);
            this.btLayerSaveListAs.Image = Resources.document_save_as_16x16;
            this.btLayerSaveListAs.ImageFixedSize = new Size(16, 16);
            this.btLayerSaveListAs.Name = "btLayerSaveListAs";
            this.btLayerSaveListAs.Click += new EventHandler(this.btLayerSaveListAs_Click);
            this.btLayerOpenList.BeginGroup = true;
            this.btLayerOpenList.Image = Resources.document_open_16x16;
            this.btLayerOpenList.ImageFixedSize = new Size(16, 16);
            this.btLayerOpenList.Name = "btLayerOpenList";
            this.btLayerOpenList.Click += new EventHandler(this.btLayerOpenList_Click);
            this.btLayersToggle.AutoExpandOnClick = true;
            this.btLayersToggle.BeginGroup = true;
            this.btLayersToggle.Image = Resources.checked_list_16x16;
            this.btLayersToggle.ImageFixedSize = new Size(16, 16);
            this.btLayersToggle.ImagePaddingHorizontal = 4;
            this.btLayersToggle.ImagePaddingVertical = 2;
            this.btLayersToggle.ImagePosition = eImagePosition.Top;
            this.btLayersToggle.Name = "btLayersToggle";
            this.btLayersToggle.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btLayersMakeVisible,
    this.btLayersMakeInvisible
            });

            this.btLayersMakeVisible.Name = "btLayersMakeVisible";
            resources.ApplyResources(this.btLayersMakeVisible, "btLayersMakeVisible");
            this.btLayersMakeVisible.Click += new EventHandler(this.btLayersMakeVisible_Click);
            this.btLayersMakeInvisible.Name = "btLayersMakeInvisible";
            resources.ApplyResources(this.btLayersMakeInvisible, "btLayersMakeInvisible");
            this.btLayersMakeInvisible.Click += new EventHandler(this.btLayersMakeInvisible_Click);
            this.barDisplayResults.CanAutoHide = false;
            this.barDisplayResults.CanCustomize = false;
            this.barDisplayResults.CanDockLeft = false;
            this.barDisplayResults.CanDockRight = false;
            this.barDisplayResults.CanDockTab = false;
            this.barDisplayResults.CanDockTop = false;
            this.barDisplayResults.CanMove = false;
            this.barDisplayResults.CanReorderTabs = false;
            this.barDisplayResults.CanUndock = false;
            resources.ApplyResources(this.barDisplayResults, "barDisplayResults");
            this.barDisplayResults.DockTabAlignment = eTabStripAlignment.Left;
            this.barDisplayResults.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblDisplayResults,
    this.btDisplayResultsForThisPlan,
    this.lblDisplayResultsPadding,
    this.btDisplayResultsForAllPlans
            });

            this.barDisplayResults.Name = "barDisplayResults";
            this.barDisplayResults.PaddingBottom = 4;
            this.barDisplayResults.PaddingLeft = 0;
            this.barDisplayResults.PaddingTop = 0;
            this.barDisplayResults.RoundCorners = false;
            this.barDisplayResults.SaveLayoutChanges = false;
            this.barDisplayResults.Stretch = true;
            this.barDisplayResults.Style = eDotNetBarStyle.StyleManagerControlled;
            this.barDisplayResults.TabStop = false;
            this.lblDisplayResults.ForeColor = Color.Black;
            this.lblDisplayResults.ItemAlignment = eItemAlignment.Far;
            this.lblDisplayResults.Name = "lblDisplayResults";
            this.lblDisplayResults.PaddingRight = 3;
            resources.ApplyResources(this.lblDisplayResults, "lblDisplayResults");
            this.btDisplayResultsForThisPlan.AutoCheckOnClick = true;
            this.btDisplayResultsForThisPlan.CanCustomize = false;
            this.btDisplayResultsForThisPlan.Checked = true;
            this.btDisplayResultsForThisPlan.ItemAlignment = eItemAlignment.Far;
            this.btDisplayResultsForThisPlan.Name = "btDisplayResultsForThisPlan";
            this.btDisplayResultsForThisPlan.OptionGroup = "DisplayResults";
            resources.ApplyResources(this.btDisplayResultsForThisPlan, "btDisplayResultsForThisPlan");
            this.lblDisplayResultsPadding.ItemAlignment = eItemAlignment.Far;
            this.lblDisplayResultsPadding.Name = "lblDisplayResultsPadding";
            this.lblDisplayResultsPadding.Width = 1;
            this.btDisplayResultsForAllPlans.AutoCheckOnClick = true;
            this.btDisplayResultsForAllPlans.CanCustomize = false;
            this.btDisplayResultsForAllPlans.ItemAlignment = eItemAlignment.Far;
            this.btDisplayResultsForAllPlans.Name = "btDisplayResultsForAllPlans";
            this.btDisplayResultsForAllPlans.OptionGroup = "DisplayResults";
            resources.ApplyResources(this.btDisplayResultsForAllPlans, "btDisplayResultsForAllPlans");
            this.tabProperties.BackColor = Color.FromArgb(113, 113, 113);
            this.tabProperties.CanReorderTabs = false;
            this.tabProperties.ColorScheme.TabBorder = Color.Transparent;
            BackgroundColorBlendCollection tabItemBackgroundColorBlend = this.tabProperties.ColorScheme.TabItemBackgroundColorBlend;
            BackgroundColorBlend[] backgroundColorBlend = new BackgroundColorBlend[] { new BackgroundColorBlend(Color.FromArgb(215, 230, 249), 0f), new BackgroundColorBlend(Color.FromArgb(199, 220, 248), 0.45f), new BackgroundColorBlend(Color.FromArgb(179, 208, 245), 0.45f), new BackgroundColorBlend(Color.FromArgb(215, 229, 247), 1f) };
            tabItemBackgroundColorBlend.AddRange(backgroundColorBlend);
            BackgroundColorBlendCollection tabItemHotBackgroundColorBlend = this.tabProperties.ColorScheme.TabItemHotBackgroundColorBlend;
            backgroundColorBlend = new BackgroundColorBlend[] { new BackgroundColorBlend(Color.FromArgb(0xff, 253, 235), 0f), new BackgroundColorBlend(Color.FromArgb(0xff, 236, 168), 0.45f), new BackgroundColorBlend(Color.FromArgb(0xff, 218, 89), 0.45f), new BackgroundColorBlend(Color.FromArgb(0xff, 230, 141), 1f) };
            tabItemHotBackgroundColorBlend.AddRange(backgroundColorBlend);
            BackgroundColorBlendCollection tabItemSelectedBackgroundColorBlend = this.tabProperties.ColorScheme.TabItemSelectedBackgroundColorBlend;
            backgroundColorBlend = new BackgroundColorBlend[] { new BackgroundColorBlend(Color.White, 0f), new BackgroundColorBlend(Color.FromArgb(253, 253, 254), 0.45f), new BackgroundColorBlend(Color.FromArgb(253, 253, 254), 0.45f), new BackgroundColorBlend(Color.FromArgb(253, 253, 254), 1f) };
            tabItemSelectedBackgroundColorBlend.AddRange(backgroundColorBlend);
            this.tabProperties.Controls.Add(this.tabControlPanel1);
            this.tabProperties.Controls.Add(this.tabControlPanel2);
            resources.ApplyResources(this.tabProperties, "tabProperties");
            this.tabProperties.ForeColor = Color.Black;
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabProperties.SelectedTabIndex = 0;
            this.tabProperties.Style = eTabStripStyle.VS2005Document;
            this.tabProperties.TabAlignment = eTabStripAlignment.Bottom;
            this.tabProperties.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabProperties.Tabs.Add(this.tabItem1);
            this.tabProperties.Tabs.Add(this.tabItem2);
            this.tabControlPanel1.CanvasColor = SystemColors.Control;
            this.tabControlPanel1.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.tabControlPanel1.Controls.Add(this.superTabProperties);
            resources.ApplyResources(this.tabControlPanel1, "tabControlPanel1");
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Style.BorderSide = eBorderSide.None;
            this.tabControlPanel1.Style.BorderWidth = 0;
            this.tabControlPanel1.Style.GradientAngle = -90;
            this.tabControlPanel1.StyleMouseOver.BorderWidth = 0;
            this.tabControlPanel1.TabItem = this.tabItem1;
            this.tabControlPanel1.UseCustomStyle = true;
            this.tabControlPanel1.Resize += new EventHandler(this.tabControlPanel1_Resize);
            this.superTabProperties.BackColor = Color.White;
            this.superTabProperties.ControlBox.CloseBox.Name = Resources.Coller;
            this.superTabProperties.ControlBox.MenuBox.Name = Resources.Coller;
            this.superTabProperties.ControlBox.Name = Resources.Coller;
            this.superTabProperties.ControlBox.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.superTabProperties.ControlBox.MenuBox,
    this.superTabProperties.ControlBox.CloseBox
            });

            this.superTabProperties.Controls.Add(this.superTabControlPanel1);
            resources.ApplyResources(this.superTabProperties, "superTabProperties");
            this.superTabProperties.ForeColor = Color.Black;
            this.superTabProperties.Name = "superTabProperties";
            this.superTabProperties.ReorderTabsEnabled = false;
            this.superTabProperties.SelectedTabIndex = 0;
            this.superTabProperties.TabAlignment = eTabStripAlignment.Bottom;
            this.superTabProperties.TabHorizontalSpacing = 3;
            this.superTabProperties.Tabs.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.superTabItem1
            });

            this.superTabProperties.TabStyle = eSuperTabStyle.Office2010BackstageBlue;
            this.superTabProperties.TabsVisible = false;
            this.superTabProperties.TabVerticalSpacing = 5;
            this.superTabControlPanel1.Controls.Add(this.gridObjectProperties);
            resources.ApplyResources(this.superTabControlPanel1, "superTabControlPanel1");
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            resources.ApplyResources(this.gridObjectProperties, "gridObjectProperties");
            this.gridObjectProperties.GridLinesColor = Color.WhiteSmoke;
            this.gridObjectProperties.Name = "gridObjectProperties";
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            resources.ApplyResources(this.superTabItem1, "superTabItem1");
            //this.superTabItem1.TextAlignment = new eItemAlignment?(eItemAlignment.Center);
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = Resources.Propriétés;
            this.tabControlPanel2.CanvasColor = SystemColors.Control;
            this.tabControlPanel2.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.tabControlPanel2.Controls.Add(this.extensionsManager);
            resources.ApplyResources(this.tabControlPanel2, "tabControlPanel2");
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Style.BackColor2.Color = Color.FromArgb(251, 250, 247);
            this.tabControlPanel2.Style.BorderSide = eBorderSide.Left | eBorderSide.Right | eBorderSide.Top;
            this.tabControlPanel2.Style.GradientAngle = -90;
            this.tabControlPanel2.TabItem = this.tabItem2;
            this.tabControlPanel2.UseCustomStyle = true;
            this.extensionsManager.BackColor = SystemColors.Window;
            resources.ApplyResources(this.extensionsManager, "extensionsManager");
            this.extensionsManager.HelpUtilities = null;
            this.extensionsManager.Name = "extensionsManager";
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            resources.ApplyResources(this.tabItem2, "tabItem2");
            this.cbObjects.DisableInternalDrawing = true;
            resources.ApplyResources(this.cbObjects, "cbObjects");
            this.cbObjects.DrawMode = DrawMode.OwnerDrawFixed;
            this.cbObjects.DropDownHeight = 220;
            this.cbObjects.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbObjects.FormattingEnabled = true;
            this.cbObjects.Name = "cbObjects";
            this.cbObjects.Style = eDotNetBarStyle.StyleManagerControlled;
            this.cbObjects.WatermarkEnabled = false;
            this.panControl.BackColor = SystemColors.Window;
            resources.ApplyResources(this.panControl, "panControl");
            this.panControl.Name = "panControl";
            this.panControl.PanFromScrolling = false;
            this.panControl.PanRectangle = new Rectangle(0, 0, 1, 1);
            this.panControl.Panning += new PanningEventHandler(this.panControl_Panning);
            this.panControl.Paint += new System.Windows.Forms.PaintEventHandler(this.panControl_Paint);
            this.panControl.MouseMove += new MouseEventHandler(this.panControl_MouseMove);
            this.panControl.Resize += new EventHandler(this.panControl_Resize);
            this.lstRecentPlans.AllowDrop = true;
            this.lstRecentPlans.AllowUserToResizeColumns = false;
            this.lstRecentPlans.BackColor = SystemColors.Window;
            this.lstRecentPlans.BackgroundStyle.BorderColor = Color.Transparent;
            this.lstRecentPlans.BackgroundStyle.BorderColor2 = Color.Transparent;
            this.lstRecentPlans.BackgroundStyle.Class = "TreeBorderKey";
            this.lstRecentPlans.BackgroundStyle.CornerType = eCornerType.Square;
            this.lstRecentPlans.BackgroundStyle.PaddingLeft = 1;
            this.lstRecentPlans.BackgroundStyle.PaddingRight = 10;
            this.lstRecentPlans.CellEdit = true;
            this.lstRecentPlans.Columns.Add(this.columnHeader1);
            this.lstRecentPlans.Columns.Add(this.columnHeader2);
            this.lstRecentPlans.Columns.Add(this.columnHeader5);
            this.lstRecentPlans.ColumnsVisible = false;
            resources.ApplyResources(this.lstRecentPlans, "lstRecentPlans");
            this.lstRecentPlans.DragDropEnabled = false;
            this.lstRecentPlans.DragDropNodeCopyEnabled = false;
            this.lstRecentPlans.ExpandBorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.lstRecentPlans.ExpandWidth = 0;
            this.lstRecentPlans.GridColumnLineResizeEnabled = true;
            this.lstRecentPlans.GridColumnLines = false;
            this.lstRecentPlans.HotTracking = true;
            this.lstRecentPlans.HScrollBarVisible = false;
            this.lstRecentPlans.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.lstRecentPlans.Name = "lstRecentPlans";
            this.lstRecentPlans.NodeHorizontalSpacing = 0;
            this.lstRecentPlans.NodesConnector = this.nodeConnector3;
            this.lstRecentPlans.PathSeparator = ";";
            this.lstRecentPlans.Styles.Add(this.elementStyle3);
            this.lstRecentPlans.Styles.Add(this.elementStyle4);
            this.columnHeader1.Editable = false;
            this.columnHeader1.Name = "columnHeader1";
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            this.columnHeader1.Width.Absolute = 22;
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.StretchToFill = true;
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            this.columnHeader2.Width.AutoSize = true;
            this.columnHeader5.Name = "columnHeader5";
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            this.columnHeader5.Width.Absolute = 3;
            this.nodeConnector3.LineColor = SystemColors.ControlText;
            this.elementStyle3.BackColor2SchemePart = eColorSchemePart.ItemHotBackground2;
            this.elementStyle3.BackColorGradientAngle = 90;
            this.elementStyle3.BackColorSchemePart = eColorSchemePart.ItemHotBackground;
            this.elementStyle3.BorderBottomWidth = 1;
            this.elementStyle3.BorderColorSchemePart = eColorSchemePart.BarCaptionText;
            this.elementStyle3.BorderLeftWidth = 1;
            this.elementStyle3.BorderRightWidth = 1;
            this.elementStyle3.BorderTopWidth = 1;
            this.elementStyle3.CornerDiameter = 4;
            this.elementStyle3.CornerType = eCornerType.Square;
            this.elementStyle3.Description = "Blue";
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.PaddingBottom = 4;
            this.elementStyle3.PaddingLeft = 4;
            this.elementStyle3.PaddingRight = 4;
            this.elementStyle3.PaddingTop = 4;
            this.elementStyle3.TextColor = Color.Black;
            this.elementStyle4.BackColorGradientAngle = 90;
            this.elementStyle4.BorderBottomWidth = 1;
            this.elementStyle4.BorderColor = Color.DarkGray;
            this.elementStyle4.BorderLeftWidth = 1;
            this.elementStyle4.BorderRightWidth = 1;
            this.elementStyle4.BorderTopWidth = 1;
            this.elementStyle4.CornerDiameter = 4;
            this.elementStyle4.CornerType = eCornerType.Square;
            this.elementStyle4.Description = "Blue";
            this.elementStyle4.Name = "elementStyle4";
            this.elementStyle4.PaddingBottom = 1;
            this.elementStyle4.PaddingLeft = 1;
            this.elementStyle4.PaddingRight = 1;
            this.elementStyle4.PaddingTop = 1;
            this.elementStyle4.TextColor = Color.Black;
            this.barRecentPlans.CanAutoHide = false;
            this.barRecentPlans.CanCustomize = false;
            this.barRecentPlans.CanDockBottom = false;
            this.barRecentPlans.CanDockLeft = false;
            this.barRecentPlans.CanDockRight = false;
            this.barRecentPlans.CanDockTab = false;
            this.barRecentPlans.CanDockTop = false;
            this.barRecentPlans.CanMove = false;
            this.barRecentPlans.CanReorderTabs = false;
            this.barRecentPlans.CanUndock = false;
            this.barRecentPlans.ColorScheme.PredefinedColorScheme = ePredefinedColorScheme.Silver2003;
            resources.ApplyResources(this.barRecentPlans, "barRecentPlans");
            this.barRecentPlans.DockTabAlignment = eTabStripAlignment.Left;
            this.barRecentPlans.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btPlanRename
            });

            this.barRecentPlans.Name = "barRecentPlans";
            this.barRecentPlans.RoundCorners = false;
            this.barRecentPlans.SaveLayoutChanges = false;
            this.barRecentPlans.Stretch = true;
            this.barRecentPlans.Style = eDotNetBarStyle.StyleManagerControlled;
            this.barRecentPlans.TabStop = false;
            this.btPlanRename.Image = (Image)resources.GetObject("btPlanRename.Image");
            this.btPlanRename.ImageFixedSize = new Size(16, 16);
            this.btPlanRename.Name = "btPlanRename";
            this.btPlanRename.Click += new EventHandler(this.btPlanRename_Click);
            this.treeObjects.AllowDrop = true;
            this.treeObjects.AllowUserToResizeColumns = false;
            this.treeObjects.BackColor = SystemColors.Window;
            this.treeObjects.BackgroundStyle.BorderColor = Color.Transparent;
            this.treeObjects.BackgroundStyle.BorderColor2 = Color.Transparent;
            this.treeObjects.BackgroundStyle.Class = "TreeBorderKey";
            this.treeObjects.BackgroundStyle.CornerType = eCornerType.Square;
            this.treeObjects.CellEdit = true;
            this.treeObjects.Columns.Add(this.columnObjectIcon);
            this.treeObjects.Columns.Add(this.columnObjectName);
            this.treeObjects.Columns.Add(this.columnObjectInfo);
            this.treeObjects.Columns.Add(this.columnObjectColor);
            this.treeObjects.Columns.Add(this.columnObjectVisible);
            this.treeObjects.Columns.Add(this.columnObjectPadding);
            this.treeObjects.ColumnsVisible = false;
            resources.ApplyResources(this.treeObjects, "treeObjects");
            this.treeObjects.DragDropNodeCopyEnabled = false;
            this.treeObjects.DropAsChildOffset = 0;
            this.treeObjects.ExpandWidth = 12;
            this.treeObjects.ForeColor = Color.Black;
            this.treeObjects.GridColumnLineResizeEnabled = true;
            this.treeObjects.GridColumnLines = false;
            this.treeObjects.GridRowLines = true;
            this.treeObjects.HotTracking = true;
            this.treeObjects.HScrollBarVisible = false;
            this.treeObjects.Indent = 8;
            this.treeObjects.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeObjects.Name = "treeObjects";
            this.treeObjects.NodeHorizontalSpacing = 3;
            this.treeObjects.NodesConnector = this.nodeConnector1;
            this.treeObjects.NodeSpacing = 1;
            this.treeObjects.PathSeparator = ";";
            this.treeObjects.Styles.Add(this.elementStyle7);
            this.columnObjectIcon.Name = "columnObjectIcon";
            this.columnObjectIcon.SortingEnabled = false;
            resources.ApplyResources(this.columnObjectIcon, "columnObjectIcon");
            this.columnObjectIcon.Width.Absolute = 44;
            this.columnObjectName.MaxInputLength = 50;
            this.columnObjectName.Name = "columnObjectName";
            this.columnObjectName.SortingEnabled = false;
            resources.ApplyResources(this.columnObjectName, "columnObjectName");
            this.columnObjectName.Width.Relative = 46;
            this.columnObjectInfo.Name = "columnObjectInfo";
            this.columnObjectInfo.SortingEnabled = false;
            this.columnObjectInfo.StretchToFill = true;
            resources.ApplyResources(this.columnObjectInfo, "columnObjectInfo");
            this.columnObjectInfo.Width.Relative = 31;
            this.columnObjectColor.Name = "columnObjectColor";
            this.columnObjectColor.SortingEnabled = false;
            resources.ApplyResources(this.columnObjectColor, "columnObjectColor");
            this.columnObjectColor.Visible = false;
            this.columnObjectColor.Width.AutoSize = true;
            this.columnObjectVisible.Name = "columnObjectVisible";
            this.columnObjectVisible.SortingEnabled = false;
            resources.ApplyResources(this.columnObjectVisible, "columnObjectVisible");
            this.columnObjectVisible.Width.AutoSize = true;
            this.columnObjectPadding.Name = "columnObjectPadding";
            this.columnObjectPadding.SortingEnabled = false;
            resources.ApplyResources(this.columnObjectPadding, "columnObjectPadding");
            this.columnObjectPadding.Width.Absolute = 1;
            this.nodeConnector1.LineColor = SystemColors.ControlText;
            this.elementStyle7.BackColor = Color.FromArgb(221, 230, 247);
            this.elementStyle7.BackColor2 = Color.FromArgb(138, 168, 228);
            this.elementStyle7.BackColorGradientAngle = 90;
            this.elementStyle7.BorderBottom = eStyleBorderType.Solid;
            this.elementStyle7.BorderBottomWidth = 1;
            this.elementStyle7.BorderColor = Color.DarkGray;
            this.elementStyle7.BorderLeft = eStyleBorderType.Solid;
            this.elementStyle7.BorderLeftWidth = 1;
            this.elementStyle7.BorderRight = eStyleBorderType.Solid;
            this.elementStyle7.BorderRightWidth = 1;
            this.elementStyle7.BorderTop = eStyleBorderType.Solid;
            this.elementStyle7.BorderTopWidth = 1;
            this.elementStyle7.CornerDiameter = 4;
            this.elementStyle7.CornerType = eCornerType.Square;
            this.elementStyle7.Description = "Blue";
            this.elementStyle7.Name = "elementStyle7";
            this.elementStyle7.PaddingBottom = 1;
            this.elementStyle7.PaddingLeft = 1;
            this.elementStyle7.PaddingRight = 1;
            this.elementStyle7.PaddingTop = 1;
            this.elementStyle7.TextColor = Color.Black;
            this.panelPlanName.Controls.Add(this.txtPlanName);
            this.panelPlanName.Controls.Add(this.cbPlans);
            resources.ApplyResources(this.panelPlanName, "panelPlanName");
            this.panelPlanName.Name = "panelPlanName";
            this.txtPlanName.AssociatedLabel = null;
            resources.ApplyResources(this.txtPlanName, "txtPlanName");
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            this.cbPlans.DisableInternalDrawing = true;
            resources.ApplyResources(this.cbPlans, "cbPlans");
            this.cbPlans.DrawMode = DrawMode.OwnerDrawFixed;
            this.cbPlans.DropDownHeight = 220;
            this.cbPlans.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbPlans.FormattingEnabled = true;
            this.cbPlans.Name = "cbPlans";
            this.cbPlans.Style = eDotNetBarStyle.StyleManagerControlled;
            this.cbPlans.WatermarkEnabled = false;
            this.barGroups.CanAutoHide = false;
            this.barGroups.CanCustomize = false;
            this.barGroups.CanDockBottom = false;
            this.barGroups.CanDockLeft = false;
            this.barGroups.CanDockRight = false;
            this.barGroups.CanDockTab = false;
            this.barGroups.CanDockTop = false;
            this.barGroups.CanMove = false;
            this.barGroups.CanReorderTabs = false;
            this.barGroups.CanUndock = false;
            this.barGroups.ColorScheme.PredefinedColorScheme = ePredefinedColorScheme.Silver2003;
            resources.ApplyResources(this.barGroups, "barGroups");
            this.barGroups.DockTabAlignment = eTabStripAlignment.Left;
            this.barGroups.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btGroupLocate,
    this.btZoomToObject,
    this.btGroupSelect,
    this.btGroupRemove,
    this.btGroupRename,
    this.btRenamePlan,
    this.btGroupsToggle
            });

            this.barGroups.Name = "barGroups";
            this.barGroups.RoundCorners = false;
            this.barGroups.SaveLayoutChanges = false;
            this.barGroups.Stretch = true;
            this.barGroups.Style = eDotNetBarStyle.StyleManagerControlled;
            this.barGroups.TabStop = false;
            this.btGroupLocate.BeginGroup = true;
            this.btGroupLocate.Image = Resources.locate_16x16;
            this.btGroupLocate.ImageFixedSize = new Size(16, 16);
            this.btGroupLocate.Name = "btGroupLocate";
            this.btGroupLocate.Click += new EventHandler(this.btGroupLocate_Click);
            this.btZoomToObject.Image = Resources.zoom_16x16;
            this.btZoomToObject.ImageFixedSize = new Size(16, 16);
            this.btZoomToObject.Name = "btZoomToObject";
            this.btZoomToObject.Click += new EventHandler(this.btGroupZoomToObject_Click);
            this.btGroupSelect.Image = Resources.selection_16x16_alt;
            this.btGroupSelect.ImageFixedSize = new Size(16, 16);
            this.btGroupSelect.Name = "btGroupSelect";
            this.btGroupSelect.Click += new EventHandler(this.btGroupSelect_Click);
            this.btGroupRemove.Image = Resources.delete_16x16;
            this.btGroupRemove.ImageFixedSize = new Size(16, 16);
            this.btGroupRemove.Name = "btGroupRemove";
            this.btGroupRemove.Click += new EventHandler(this.btGroupRemove_Click);
            this.btGroupRename.Image = (Image)resources.GetObject("btGroupRename.Image");
            this.btGroupRename.ImageFixedSize = new Size(16, 16);
            this.btGroupRename.Name = "btGroupRename";
            this.btGroupRename.Click += new EventHandler(this.btGroupRename_Click);
            this.btRenamePlan.BeginGroup = true;
            this.btRenamePlan.Image = Resources.textfield_rename_16x16;
            this.btRenamePlan.ImageFixedSize = new Size(16, 16);
            this.btRenamePlan.Name = "btRenamePlan";
            this.btRenamePlan.Click += new EventHandler(this.btRenamePlan_Click);
            this.btGroupsToggle.AutoExpandOnClick = true;
            this.btGroupsToggle.BeginGroup = true;
            this.btGroupsToggle.Image = Resources.checked_list_16x16;
            this.btGroupsToggle.ImageFixedSize = new Size(16, 16);
            this.btGroupsToggle.ImagePaddingHorizontal = 4;
            this.btGroupsToggle.ImagePaddingVertical = 2;
            this.btGroupsToggle.ImagePosition = eImagePosition.Top;
            this.btGroupsToggle.Name = "btGroupsToggle";
            this.btGroupsToggle.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btGroupsMakeVisible,
    this.btGroupsMakeInvisible
            });

            this.btGroupsMakeVisible.Name = "btGroupsMakeVisible";
            resources.ApplyResources(this.btGroupsMakeVisible, "btGroupsMakeVisible");
            this.btGroupsMakeVisible.Click += new EventHandler(this.btGroupsMakeVisible_Click);
            this.btGroupsMakeInvisible.Name = "btGroupsMakeInvisible";
            resources.ApplyResources(this.btGroupsMakeInvisible, "btGroupsMakeInvisible");
            this.btGroupsMakeInvisible.Click += new EventHandler(this.btGroupsMakeInvisible_Click);
            this.checkBoxItem1.Name = "checkBoxItem1";
            this.checkBoxItem1.TextVisible = false;
            this.sliderItem1.EnableMarkup = false;
            this.sliderItem1.LabelVisible = false;
            this.sliderItem1.Name = "sliderItem1";
            this.sliderItem1.TrackMarker = false;
            this.sliderItem1.Value = 0;
            this.checkBoxItem2.Name = "checkBoxItem2";
            resources.ApplyResources(this.checkBoxItem2, "checkBoxItem2");
            this.checkBoxItem2.TextVisible = false;
            this.sliderItem2.LabelVisible = false;
            this.sliderItem2.Name = "sliderItem2";
            resources.ApplyResources(this.sliderItem2, "sliderItem2");
            this.sliderItem2.TrackMarker = false;
            this.sliderItem2.Value = 0;
            this.timer1.Interval = 10;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new ElapsedEventHandler(this.timer1_Elapsed);
            this.itemContainer5.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainer5.LayoutOrientation = eOrientation.Vertical;
            this.itemContainer5.Name = "itemContainer5";
            this.itemContainer5.TitleStyle.CornerType = eCornerType.Square;
            this.galleryGroupArea.Name = "galleryGroupArea";
            this.galleryGroupArea.Text = Resources.Surface;
            this.galleryGroupPerimeter.Name = "galleryGroupPerimeter";
            this.galleryGroupPerimeter.Text = Resources.Périmètre;
            this.galleryGroupCounter.Name = "galleryGroupCounter";
            this.galleryGroupCounter.Text = Resources.Compteur;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = Resources.Coller;
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.flowPlans.AllowDrop = true;
            resources.ApplyResources(this.flowPlans, "flowPlans");
            this.flowPlans.BackColor = SystemColors.Window;
            this.flowPlans.Name = "flowPlans";
            this.sliderItem4.LabelVisible = false;
            this.sliderItem4.Maximum = 0x12c;
            this.sliderItem4.Minimum = 10;
            this.sliderItem4.Name = "sliderItem4";
            resources.ApplyResources(this.sliderItem4, "sliderItem4");
            this.sliderItem4.TrackMarker = false;
            this.sliderItem4.Value = 0;
            this.sliderItem4.Width = 200;
            this.itemContainerBrightness.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerBrightness.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerBrightness.Name = "itemContainerBrightness";
            this.itemContainerBrightness.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblBrightness,
    this.sliderBrightness
            });

            this.itemContainerBrightness.TitleStyle.CornerType = eCornerType.Square;
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.PaddingTop = 12;
            resources.ApplyResources(this.lblBrightness, "lblBrightness");
            this.lblBrightness.TextAlignment = StringAlignment.Center;
            this.sliderBrightness.LabelPosition = eSliderLabelPosition.Bottom;
            this.sliderBrightness.Maximum = 0xff;
            this.sliderBrightness.Minimum = -255;
            this.sliderBrightness.Name = "sliderBrightness";
            resources.ApplyResources(this.sliderBrightness, "sliderBrightness");
            this.sliderBrightness.TextColor = Color.Black;
            this.sliderBrightness.Value = 0;
            this.sliderBrightness.ValueChanged += new EventHandler(this.sliderBrightness_ValueChanged);
            this.lblBrightnessContrastPadding1.Name = "lblBrightnessContrastPadding1";
            this.lblBrightnessContrastPadding1.Width = 5;
            this.itemContainerContrast.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerContrast.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerContrast.Name = "itemContainerContrast";
            this.itemContainerContrast.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblContrast,
    this.sliderContrast
            });

            this.itemContainerContrast.TitleStyle.CornerType = eCornerType.Square;
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.PaddingTop = 12;
            resources.ApplyResources(this.lblContrast, "lblContrast");
            this.lblContrast.TextAlignment = StringAlignment.Center;
            this.sliderContrast.LabelPosition = eSliderLabelPosition.Bottom;
            this.sliderContrast.Minimum = -100;
            this.sliderContrast.Name = "sliderContrast";
            resources.ApplyResources(this.sliderContrast, "sliderContrast");
            this.sliderContrast.TextColor = Color.Black;
            this.sliderContrast.Value = 0;
            this.sliderContrast.ValueChanged += new EventHandler(this.sliderContrast_ValueChanged);
            this.ribbonBarBrightnessContrast.AutoOverflowEnabled = true;
            this.ribbonBarBrightnessContrast.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarBrightnessContrast.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarBrightnessContrast.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarBrightnessContrast, "ribbonBarBrightnessContrast");
            this.ribbonBarBrightnessContrast.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.ribbonBarBrightnessContrast.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.itemContainerBrightness,
    this.lblBrightnessContrastPadding1,
    this.itemContainerContrast,
    this.lblBrightnessContrastPadding2,
    this.lblBrightnessContrastSeparator,
    this.lblBrightnessContrastPadding3,
    this.btBrightnessContrastApply,
    this.btBrightnessContrastCancel,
    this.btBrightnessContrastRestore
            });

            this.ribbonBarBrightnessContrast.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarBrightnessContrast.Name = "ribbonBarBrightnessContrast";
            this.ribbonBarBrightnessContrast.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarBrightnessContrast.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarBrightnessContrast.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.lblBrightnessContrastPadding2.Name = "lblBrightnessContrastPadding2";
            this.lblBrightnessContrastPadding2.Width = 15;
            this.lblBrightnessContrastSeparator.BeginGroup = true;
            this.lblBrightnessContrastSeparator.Name = "lblBrightnessContrastSeparator";
            this.lblBrightnessContrastSeparator.Width = 5;
            this.lblBrightnessContrastPadding3.Name = "lblBrightnessContrastPadding3";
            this.lblBrightnessContrastPadding3.Width = 5;
            this.btBrightnessContrastApply.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrightnessContrastApply.Image = (Image)resources.GetObject("btBrightnessContrastApply.Image");
            this.btBrightnessContrastApply.ImagePosition = eImagePosition.Top;
            this.btBrightnessContrastApply.Name = "btBrightnessContrastApply";
            resources.ApplyResources(this.btBrightnessContrastApply, "btBrightnessContrastApply");
            this.btBrightnessContrastApply.Click += new EventHandler(this.btBrightnessContrastApply_Click);
            this.btBrightnessContrastCancel.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrightnessContrastCancel.Image = (Image)resources.GetObject("btBrightnessContrastCancel.Image");
            this.btBrightnessContrastCancel.ImagePosition = eImagePosition.Top;
            this.btBrightnessContrastCancel.Name = "btBrightnessContrastCancel";
            resources.ApplyResources(this.btBrightnessContrastCancel, "btBrightnessContrastCancel");
            this.btBrightnessContrastCancel.Click += new EventHandler(this.btBrightnessContrastCancel_Click);
            this.btBrightnessContrastRestore.ButtonStyle = eButtonStyle.ImageAndText;
            this.btBrightnessContrastRestore.Image = (Image)resources.GetObject("btBrightnessContrastRestore.Image");
            this.btBrightnessContrastRestore.ImagePosition = eImagePosition.Top;
            this.btBrightnessContrastRestore.Name = "btBrightnessContrastRestore";
            this.btBrightnessContrastRestore.Stretch = true;
            resources.ApplyResources(this.btBrightnessContrastRestore, "btBrightnessContrastRestore");
            this.btBrightnessContrastRestore.Click += new EventHandler(this.btBrightnessContrastRestore_Click);
            this.panelBrightnessContrast.CanvasColor = SystemColors.Control;
            this.panelBrightnessContrast.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelBrightnessContrast.Controls.Add(this.ribbonBarBrightnessContrast);
            resources.ApplyResources(this.panelBrightnessContrast, "panelBrightnessContrast");
            this.panelBrightnessContrast.Name = "panelBrightnessContrast";
            this.panelBrightnessContrast.Style.Alignment = StringAlignment.Center;
            this.panelBrightnessContrast.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.panelBrightnessContrast.Style.Border = eBorderType.SingleLine;
            this.panelBrightnessContrast.Style.BorderColor.ColorSchemePart = eColorSchemePart.ItemSeparator;
            this.panelBrightnessContrast.Style.BorderSide = eBorderSide.Top;
            this.panelBrightnessContrast.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.panelBrightnessContrast.Style.GradientAngle = 90;
            this.panelRotation.CanvasColor = SystemColors.Control;
            this.panelRotation.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelRotation.Controls.Add(this.ribbonBarRotation);
            resources.ApplyResources(this.panelRotation, "panelRotation");
            this.panelRotation.Name = "panelRotation";
            this.panelRotation.Style.Alignment = StringAlignment.Center;
            this.panelRotation.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.panelRotation.Style.Border = eBorderType.SingleLine;
            this.panelRotation.Style.BorderColor.ColorSchemePart = eColorSchemePart.ItemSeparator;
            this.panelRotation.Style.BorderSide = eBorderSide.Top;
            this.panelRotation.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.panelRotation.Style.GradientAngle = 90;
            this.ribbonBarRotation.AutoOverflowEnabled = true;
            this.ribbonBarRotation.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarRotation.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarRotation.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarRotation, "ribbonBarRotation");
            this.ribbonBarRotation.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.ribbonBarRotation.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btFlipHorizontally,
    this.btFlipVertically,
    this.btRotateLeft,
    this.btRotateRight,
    this.lblBarRotationPadding1,
    this.lblBarRotationSeparator,
    this.lblBarRotationPadding2,
    this.btRotationApply,
    this.btRotationCancel
            });

            this.ribbonBarRotation.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarRotation.Name = "ribbonBarRotation";
            this.ribbonBarRotation.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarRotation.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarRotation.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.btFlipHorizontally.ButtonStyle = eButtonStyle.ImageAndText;
            this.btFlipHorizontally.Image = (Image)resources.GetObject("btFlipHorizontally.Image");
            this.btFlipHorizontally.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btFlipHorizontally.ImagePosition = eImagePosition.Top;
            this.btFlipHorizontally.Name = "btFlipHorizontally";
            resources.ApplyResources(this.btFlipHorizontally, "btFlipHorizontally");
            this.btFlipHorizontally.Click += new EventHandler(this.btFlipHorizontally_Click);
            this.btFlipVertically.ButtonStyle = eButtonStyle.ImageAndText;
            this.btFlipVertically.Image = (Image)resources.GetObject("btFlipVertically.Image");
            this.btFlipVertically.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btFlipVertically.ImagePosition = eImagePosition.Top;
            this.btFlipVertically.Name = "btFlipVertically";
            resources.ApplyResources(this.btFlipVertically, "btFlipVertically");
            this.btFlipVertically.Click += new EventHandler(this.btFlipVertically_Click);
            this.btRotateLeft.ButtonStyle = eButtonStyle.ImageAndText;
            this.btRotateLeft.Image = (Image)resources.GetObject("btRotateLeft.Image");
            this.btRotateLeft.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btRotateLeft.ImagePosition = eImagePosition.Top;
            this.btRotateLeft.Name = "btRotateLeft";
            resources.ApplyResources(this.btRotateLeft, "btRotateLeft");
            this.btRotateLeft.Click += new EventHandler(this.btRotateLeft_Click);
            this.btRotateRight.ButtonStyle = eButtonStyle.ImageAndText;
            this.btRotateRight.Image = (Image)resources.GetObject("btRotateRight.Image");
            this.btRotateRight.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btRotateRight.ImagePosition = eImagePosition.Top;
            this.btRotateRight.Name = "btRotateRight";
            resources.ApplyResources(this.btRotateRight, "btRotateRight");
            this.btRotateRight.Click += new EventHandler(this.btRotateRight_Click);
            this.lblBarRotationPadding1.Name = "lblBarRotationPadding1";
            this.lblBarRotationPadding1.Width = 10;
            this.lblBarRotationSeparator.BeginGroup = true;
            this.lblBarRotationSeparator.Name = "lblBarRotationSeparator";
            this.lblBarRotationSeparator.Width = 5;
            this.lblBarRotationPadding2.Name = "lblBarRotationPadding2";
            this.lblBarRotationPadding2.Width = 5;
            this.btRotationApply.ButtonStyle = eButtonStyle.ImageAndText;
            this.btRotationApply.Image = (Image)resources.GetObject("btRotationApply.Image");
            this.btRotationApply.ImagePosition = eImagePosition.Top;
            this.btRotationApply.Name = "btRotationApply";
            resources.ApplyResources(this.btRotationApply, "btRotationApply");
            this.btRotationApply.Click += new EventHandler(this.btRotationApply_Click);
            this.btRotationCancel.ButtonStyle = eButtonStyle.ImageAndText;
            this.btRotationCancel.Image = (Image)resources.GetObject("btRotationCancel.Image");
            this.btRotationCancel.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btRotationCancel.ImagePosition = eImagePosition.Top;
            this.btRotationCancel.Name = "btRotationCancel";
            resources.ApplyResources(this.btRotationCancel, "btRotationCancel");
            this.btRotationCancel.Click += new EventHandler(this.btRotationCancel_Click);
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            this.webBrowser.PreviewKeyDown += new PreviewKeyDownEventHandler(this.webBrowser_PreviewKeyDown);
            resources.ApplyResources(this.comboItem1, "comboItem1");
            resources.ApplyResources(this.comboItem2, "comboItem2");
            resources.ApplyResources(this.comboItem3, "comboItem3");
            resources.ApplyResources(this.comboItem4, "comboItem4");
            resources.ApplyResources(this.comboItem5, "comboItem5");
            resources.ApplyResources(this.comboItem6, "comboItem6");
            this.labelItem1.Name = "labelItem1";
            resources.ApplyResources(this.labelItem1, "labelItem1");
            this.panelWelcome.BackColor = Color.Transparent;
            this.panelWelcome.Controls.Add(this.panelWelcomeMenu);
            resources.ApplyResources(this.panelWelcome, "panelWelcome");
            this.panelWelcome.Name = "panelWelcome";
            this.panelWelcome.Resize += new EventHandler(this.panelWelcome_Resize);
            this.panelWelcomeMenu.CanvasColor = SystemColors.Control;
            this.panelWelcomeMenu.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelWelcomeMenu.Controls.Add(this.lblRecentProjects);
            this.panelWelcomeMenu.Controls.Add(this.btNew);
            this.panelWelcomeMenu.Controls.Add(this.lstRecentProjects);
            this.panelWelcomeMenu.Controls.Add(this.btOpen);
            this.panelWelcomeMenu.Controls.Add(this.picWelcome);
            resources.ApplyResources(this.panelWelcomeMenu, "panelWelcomeMenu");
            this.panelWelcomeMenu.Name = "panelWelcomeMenu";
            this.panelWelcomeMenu.Style.Alignment = StringAlignment.Center;
            this.panelWelcomeMenu.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.panelWelcomeMenu.Style.BackColor2.ColorSchemePart = eColorSchemePart.PanelBackground2;
            this.panelWelcomeMenu.Style.Border = eBorderType.SingleLine;
            this.panelWelcomeMenu.Style.BorderColor.ColorSchemePart = eColorSchemePart.PanelBorder;
            this.panelWelcomeMenu.Style.BorderWidth = 10;
            this.panelWelcomeMenu.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.panelWelcomeMenu.Style.GradientAngle = 90;
            resources.ApplyResources(this.lblRecentProjects, "lblRecentProjects");
            this.lblRecentProjects.Name = "lblRecentProjects";
            this.lblRecentProjects.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            this.btNew.AccessibleRole = AccessibleRole.PushButton;
            resources.ApplyResources(this.btNew, "btNew");
            this.btNew.AntiAlias = true;
            this.btNew.ColorTable = eButtonColor.OrangeWithBackground;
            this.btNew.FocusCuesEnabled = false;
            this.btNew.Image = Resources.document_new;
            this.btNew.ImagePosition = eImagePosition.Top;
            this.btNew.Name = "btNew";
            this.btNew.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btNew.Click += new EventHandler(this.btProjectNew_Click);
            this.lstRecentProjects.AllowDrop = true;
            this.lstRecentProjects.AllowUserToResizeColumns = false;
            resources.ApplyResources(this.lstRecentProjects, "lstRecentProjects");
            this.lstRecentProjects.BackColor = SystemColors.Window;
            this.lstRecentProjects.BackgroundStyle.CornerDiameter = 0;
            this.lstRecentProjects.BackgroundStyle.CornerType = eCornerType.Square;
            this.lstRecentProjects.BackgroundStyle.PaddingLeft = 1;
            this.lstRecentProjects.BackgroundStyle.PaddingRight = 2;
            this.lstRecentProjects.Columns.Add(this.columnHeader3);
            this.lstRecentProjects.Columns.Add(this.columnHeader4);
            this.lstRecentProjects.ColumnsVisible = false;
            this.lstRecentProjects.DragDropEnabled = false;
            this.lstRecentProjects.DragDropNodeCopyEnabled = false;
            this.lstRecentProjects.DropAsChildOffset = 0;
            this.lstRecentProjects.ExpandBorderColorSchemePart = eColorSchemePart.PanelBorder;
            this.lstRecentProjects.ExpandButtonSize = new Size(0, 0);
            this.lstRecentProjects.ExpandWidth = 0;
            this.lstRecentProjects.GridColumnLines = false;
            this.lstRecentProjects.HotTracking = true;
            this.lstRecentProjects.HScrollBarVisible = false;
            this.lstRecentProjects.Indent = 0;
            this.lstRecentProjects.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.lstRecentProjects.Name = "lstRecentProjects";
            this.lstRecentProjects.NodeHorizontalSpacing = 0;
            this.lstRecentProjects.NodesConnector = this.nodeConnector4;
            this.lstRecentProjects.NodeSpacing = 0;
            this.lstRecentProjects.PathSeparator = ";";
            this.lstRecentProjects.Styles.Add(this.elementStyle5);
            this.lstRecentProjects.Styles.Add(this.elementStyle6);
            this.lstRecentProjects.KeyDown += new KeyEventHandler(this.lstRecentProjects_KeyDown);
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.StretchToFill = true;
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            this.columnHeader3.Width.AutoSize = true;
            this.columnHeader4.Name = "columnHeader4";
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            this.columnHeader4.Width.Absolute = 150;
            this.nodeConnector4.LineColor = SystemColors.ControlText;
            this.elementStyle5.CornerType = eCornerType.Square;
            this.elementStyle5.Name = "elementStyle5";
            this.elementStyle5.TextColor = SystemColors.ControlText;
            this.elementStyle6.BackColor2SchemePart = eColorSchemePart.ItemCheckedBackground2;
            this.elementStyle6.BackColorGradientAngle = 90;
            this.elementStyle6.BackColorSchemePart = eColorSchemePart.ItemHotBackground;
            this.elementStyle6.BorderColor = Color.DarkGray;
            this.elementStyle6.BorderLeftWidth = 1;
            this.elementStyle6.BorderRightWidth = 1;
            this.elementStyle6.BorderTopWidth = 1;
            this.elementStyle6.CornerDiameter = 4;
            this.elementStyle6.CornerType = eCornerType.Square;
            this.elementStyle6.Description = "Yellow";
            this.elementStyle6.Name = "elementStyle6";
            this.elementStyle6.PaddingBottom = 1;
            this.elementStyle6.PaddingLeft = 1;
            this.elementStyle6.PaddingRight = 1;
            this.elementStyle6.PaddingTop = 1;
            this.elementStyle6.TextColor = Color.Black;
            this.btOpen.AccessibleRole = AccessibleRole.PushButton;
            resources.ApplyResources(this.btOpen, "btOpen");
            this.btOpen.AntiAlias = true;
            this.btOpen.ColorTable = eButtonColor.OrangeWithBackground;
            this.btOpen.FocusCuesEnabled = false;
            this.btOpen.Image = Resources.document_open;
            this.btOpen.ImagePosition = eImagePosition.Top;
            this.btOpen.Name = "btOpen";
            this.btOpen.Style = eDotNetBarStyle.StyleManagerControlled;
            this.btOpen.Click += new EventHandler(this.btProjectOpen_Click);
            this.picWelcome.BackColor = Color.White;
            this.picWelcome.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(this.picWelcome, "picWelcome");
            this.picWelcome.Name = "picWelcome";
            this.picWelcome.TabStop = false;
            this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superTooltip.MinimumTooltipSize = new Size(150, 50);
            this.panelPlansAction.CanvasColor = SystemColors.Control;
            this.panelPlansAction.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelPlansAction.Controls.Add(this.ribbonBarPlansAction);
            resources.ApplyResources(this.panelPlansAction, "panelPlansAction");
            this.panelPlansAction.Name = "panelPlansAction";
            this.panelPlansAction.Style.Alignment = StringAlignment.Center;
            this.panelPlansAction.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
            this.panelPlansAction.Style.Border = eBorderType.SingleLine;
            this.panelPlansAction.Style.BorderColor.ColorSchemePart = eColorSchemePart.ItemSeparator;
            this.panelPlansAction.Style.BorderSide = eBorderSide.Top;
            this.panelPlansAction.Style.ForeColor.ColorSchemePart = eColorSchemePart.PanelText;
            this.panelPlansAction.Style.GradientAngle = 90;
            this.ribbonBarPlansAction.AutoOverflowEnabled = true;
            this.ribbonBarPlansAction.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlansAction.BackgroundStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlansAction.ContainerControlProcessDialogKey = true;
            resources.ApplyResources(this.ribbonBarPlansAction, "ribbonBarPlansAction");
            this.ribbonBarPlansAction.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
            this.ribbonBarPlansAction.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.progressPlansAction,
    this.itemContainerExportType,
    this.lblBarPlansActionPadding4,
    this.btPlansActionSelectAll,
    this.btPlansActionSelectNone,
    this.lblBarPlansActionPadding1,
    this.lblPlansActionSeparator,
    this.lblBarPlansActionPadding2,
    this.btPlansActionApply,
    this.btPlansActionCancel
            });

            this.ribbonBarPlansAction.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBarPlansAction.Name = "ribbonBarPlansAction";
            this.ribbonBarPlansAction.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBarPlansAction.TitleStyle.CornerType = eCornerType.Square;
            this.ribbonBarPlansAction.TitleStyleMouseOver.CornerType = eCornerType.Square;
            this.progressPlansAction.Name = "progressPlansAction";
            this.progressPlansAction.ProgressBarType = eCircularProgressType.Donut;
            resources.ApplyResources(this.progressPlansAction, "progressPlansAction");
            this.itemContainerExportType.BackgroundStyle.CornerType = eCornerType.Square;
            this.itemContainerExportType.LayoutOrientation = eOrientation.Vertical;
            this.itemContainerExportType.Name = "itemContainerExportType";
            this.itemContainerExportType.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.lblBarPlansActionPadding3,
    this.checkBoxExportSingleFile,
    this.checkBoxExportMultiFiles
            });

            this.itemContainerExportType.TitleStyle.CornerType = eCornerType.Square;
            this.lblBarPlansActionPadding3.Name = "lblBarPlansActionPadding3";
            resources.ApplyResources(this.checkBoxExportSingleFile, "checkBoxExportSingleFile");
            this.checkBoxExportSingleFile.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.checkBoxExportSingleFile.Checked = true;
            this.checkBoxExportSingleFile.CheckState = CheckState.Checked;
            this.checkBoxExportSingleFile.Name = "checkBoxExportSingleFile";
            resources.ApplyResources(this.checkBoxExportMultiFiles, "checkBoxExportMultiFiles");
            this.checkBoxExportMultiFiles.CheckBoxStyle = eCheckBoxStyle.RadioButton;
            this.checkBoxExportMultiFiles.Name = "checkBoxExportMultiFiles";
            this.lblBarPlansActionPadding4.Name = "lblBarPlansActionPadding4";
            this.lblBarPlansActionPadding4.Width = 10;
            this.btPlansActionSelectAll.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlansActionSelectAll.Image = Resources.select_all_32x32;
            this.btPlansActionSelectAll.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlansActionSelectAll.Name = "btPlansActionSelectAll";
            resources.ApplyResources(this.btPlansActionSelectAll, "btPlansActionSelectAll");
            this.btPlansActionSelectAll.Click += new EventHandler(this.btPlansActionSelectAll_Click);
            this.btPlansActionSelectNone.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlansActionSelectNone.Image = (Image)resources.GetObject("btPlansActionSelectNone.Image");
            this.btPlansActionSelectNone.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlansActionSelectNone.Name = "btPlansActionSelectNone";
            resources.ApplyResources(this.btPlansActionSelectNone, "btPlansActionSelectNone");
            this.btPlansActionSelectNone.Click += new EventHandler(this.btPlansActionSelectNone_Click);
            this.lblBarPlansActionPadding1.Name = "lblBarPlansActionPadding1";
            this.lblBarPlansActionPadding1.Width = 10;
            this.lblPlansActionSeparator.BeginGroup = true;
            this.lblPlansActionSeparator.Name = "lblPlansActionSeparator";
            this.lblPlansActionSeparator.Width = 5;
            this.lblBarPlansActionPadding2.Name = "lblBarPlansActionPadding2";
            this.lblBarPlansActionPadding2.Width = 5;
            this.btPlansActionApply.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlansActionApply.Image = Resources.print_apply_40x40;
            this.btPlansActionApply.ImagePaddingHorizontal = 10;
            this.btPlansActionApply.ImagePaddingVertical = 5;
            this.btPlansActionApply.ImagePosition = eImagePosition.Top;
            this.btPlansActionApply.Name = "btPlansActionApply";
            resources.ApplyResources(this.btPlansActionApply, "btPlansActionApply");
            this.btPlansActionApply.Click += new EventHandler(this.btPlansActionApply_Click);
            this.btPlansActionCancel.ButtonStyle = eButtonStyle.ImageAndText;
            this.btPlansActionCancel.Image = (Image)resources.GetObject("btPlansActionCancel.Image");
            this.btPlansActionCancel.ImageListSizeSelection = eButtonImageListSelection.Default;
            this.btPlansActionCancel.ImagePaddingHorizontal = 10;
            this.btPlansActionCancel.ImagePaddingVertical = 5;
            this.btPlansActionCancel.ImagePosition = eImagePosition.Top;
            this.btPlansActionCancel.Name = "btPlansActionCancel";
            resources.ApplyResources(this.btPlansActionCancel, "btPlansActionCancel");
            this.btPlansActionCancel.Click += new EventHandler(this.btPlansActionCancel_Click);
            this.mainControl.BackColor = SystemColors.Control;
            this.mainControl.Brightness = 0;
            this.mainControl.Contrast = 0;
            resources.ApplyResources(this.mainControl, "mainControl");
            this.mainControl.Name = "mainControl";
            //this.mainControl.Origin = (PointF)resources.GetObject("mainControl.Origin");
            this.mainControl.Origin = new PointF(474, 418);
            this.mainControl.ScrollbarsVisible = true;
            this.mainControl.UseDynamicAdjustments = false;
            this.mainControl.Zoom = 100;
            this.mainControl.ZoomRestricted = false;
            this.mainControl.MouseDown += new MouseEventHandler(this.mainControl_MouseDown);
            this.mainControl.MouseMove += new MouseEventHandler(this.mainControl_MouseMove);
            this.mainControl.MouseUp += new MouseEventHandler(this.mainControl_MouseUp);
            this.mainControl.OnPaint += new QuoterPlanControls.PaintEventHandler(this.mainControl_Paint);
            this.mainControl.OnZoomChange += new ZoomChangeEventHandler(this.mainControl_OnZoomChange);
            this.mainControl.OnCacheLoaded += new ZoomChangeEventHandler(this.mainControl_OnCacheLoaded);
            this.mainControl.OnMouseWheel += new ZoomChangeEventHandler(this.mainControl_OnMouseWheel);
            this.mainControl.OnOriginChange += new OriginChangeEventHandler(this.mainControl_OnOriginChange);
            this.mainControl.Resize += new EventHandler(this.mainControl_Resize);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.F1);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlC);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlA);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlV);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlX);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlZ);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.CtrlY);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.Del);
            this.dotNetBarManager.AutoDispatchShortcuts.Add(eShortcut.Ins);
            this.dotNetBarManager.BottomDockSite = this.dockSite4;
            this.dotNetBarManager.EnableFullSizeDock = false;
            this.dotNetBarManager.LeftDockSite = this.dockSite1;
            this.dotNetBarManager.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.dotNetBarManager.ParentForm = this;
            this.dotNetBarManager.RightDockSite = this.dockSite2;
            this.dotNetBarManager.Style = eDotNetBarStyle.StyleManagerControlled;
            this.dotNetBarManager.ToolbarBottomDockSite = this.dockSite8;
            this.dotNetBarManager.ToolbarLeftDockSite = this.dockSite5;
            this.dotNetBarManager.ToolbarRightDockSite = this.dockSite6;
            this.dotNetBarManager.ToolbarTopDockSite = this.dockSite7;
            this.dotNetBarManager.TopDockSite = this.dockSite3;
            this.dotNetBarManager.UseGlobalColorScheme = true;
            this.dockSite4.AccessibleRole = AccessibleRole.Window;
            resources.ApplyResources(this.dockSite4, "dockSite4");
            this.dockSite4.DocumentDockContainer = new DocumentDockContainer();
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.TabStop = false;
            this.dockSite1.AccessibleRole = AccessibleRole.Window;
            this.dockSite1.Controls.Add(this.containerBarLayers);
            this.dockSite1.Controls.Add(this.containerBarNavigation);
            this.dockSite1.Controls.Add(this.containerBarProperties);
            resources.ApplyResources(this.dockSite1, "dockSite1");
            DockSite documentDockContainer = this.dockSite1;
            DocumentBaseContainer[] documentBarContainer = new DocumentBaseContainer[] { new DocumentBarContainer(this.containerBarNavigation, 0x11d, 180), new DocumentBarContainer(this.containerBarProperties, 0x11d, 180), new DocumentBarContainer(this.containerBarLayers, 0x11d, 183) };
            documentDockContainer.DocumentDockContainer = new DocumentDockContainer(documentBarContainer, eOrientation.Vertical);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.TabStop = false;
            resources.ApplyResources(this.containerBarLayers, "containerBarLayers");
            this.containerBarLayers.AccessibleRole = AccessibleRole.ToolBar;
            this.containerBarLayers.AutoSyncBarCaption = true;
            this.containerBarLayers.CanCustomize = false;
            this.containerBarLayers.CanDockBottom = false;
            this.containerBarLayers.CanDockTab = false;
            this.containerBarLayers.CanDockTop = false;
            this.containerBarLayers.CloseSingleTab = true;
            this.containerBarLayers.Controls.Add(this.panelDockLayers);
            this.containerBarLayers.GrabHandleStyle = eGrabHandleStyle.Caption;
            this.containerBarLayers.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.dockContainerItemLayers
            });

            this.containerBarLayers.LayoutType = eLayoutType.DockContainer;
            this.containerBarLayers.Name = "containerBarLayers";
            this.containerBarLayers.Stretch = true;
            this.containerBarLayers.Style = eDotNetBarStyle.StyleManagerControlled;
            this.containerBarLayers.TabStop = false;
            this.panelDockLayers.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelDockLayers.Controls.Add(this.lstLayers);
            this.panelDockLayers.Controls.Add(this.barLayers);
            resources.ApplyResources(this.panelDockLayers, "panelDockLayers");
            this.panelDockLayers.Name = "panelDockLayers";
            this.panelDockLayers.Style.Alignment = StringAlignment.Center;
            this.panelDockLayers.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            this.panelDockLayers.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder;
            this.panelDockLayers.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            this.panelDockLayers.Style.GradientAngle = 90;
            this.dockContainerItemLayers.Control = this.panelDockLayers;
            this.dockContainerItemLayers.Name = "dockContainerItemLayers";
            resources.ApplyResources(this.dockContainerItemLayers, "dockContainerItemLayers");
            resources.ApplyResources(this.containerBarNavigation, "containerBarNavigation");
            this.containerBarNavigation.AccessibleRole = AccessibleRole.ToolBar;
            this.containerBarNavigation.AutoSyncBarCaption = true;
            this.containerBarNavigation.CanCustomize = false;
            this.containerBarNavigation.CanDockBottom = false;
            this.containerBarNavigation.CanDockTab = false;
            this.containerBarNavigation.CanDockTop = false;
            this.containerBarNavigation.CloseSingleTab = true;
            this.containerBarNavigation.Controls.Add(this.panelDockPreview);
            this.containerBarNavigation.GrabHandleStyle = eGrabHandleStyle.Caption;
            this.containerBarNavigation.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.dockContainerItemPreview
            });

            this.containerBarNavigation.LayoutType = eLayoutType.DockContainer;
            this.containerBarNavigation.Name = "containerBarNavigation";
            this.containerBarNavigation.Stretch = true;
            this.containerBarNavigation.Style = eDotNetBarStyle.StyleManagerControlled;
            this.containerBarNavigation.TabStop = false;
            this.panelDockPreview.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelDockPreview.Controls.Add(this.panControl);
            resources.ApplyResources(this.panelDockPreview, "panelDockPreview");
            this.panelDockPreview.Name = "panelDockPreview";
            this.panelDockPreview.Style.Alignment = StringAlignment.Center;
            this.panelDockPreview.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            this.panelDockPreview.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder;
            this.panelDockPreview.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            this.panelDockPreview.Style.GradientAngle = 90;
            this.dockContainerItemPreview.Control = this.panelDockPreview;
            this.dockContainerItemPreview.Name = "dockContainerItemPreview";
            resources.ApplyResources(this.dockContainerItemPreview, "dockContainerItemPreview");
            resources.ApplyResources(this.containerBarProperties, "containerBarProperties");
            this.containerBarProperties.AccessibleRole = AccessibleRole.ToolBar;
            this.containerBarProperties.AutoSyncBarCaption = true;
            this.containerBarProperties.CanCustomize = false;
            this.containerBarProperties.CanDockBottom = false;
            this.containerBarProperties.CanDockTab = false;
            this.containerBarProperties.CanDockTop = false;
            this.containerBarProperties.CloseSingleTab = true;
            this.containerBarProperties.Controls.Add(this.panelDockProperties);
            this.containerBarProperties.GrabHandleStyle = eGrabHandleStyle.Caption;
            this.containerBarProperties.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.dockContainerItemProperties
            });

            this.containerBarProperties.LayoutType = eLayoutType.DockContainer;
            this.containerBarProperties.Name = "containerBarProperties";
            this.containerBarProperties.Stretch = true;
            this.containerBarProperties.Style = eDotNetBarStyle.StyleManagerControlled;
            this.containerBarProperties.TabStop = false;
            this.panelDockProperties.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelDockProperties.Controls.Add(this.barDisplayResults);
            this.panelDockProperties.Controls.Add(this.tabProperties);
            this.panelDockProperties.Controls.Add(this.cbObjects);
            resources.ApplyResources(this.panelDockProperties, "panelDockProperties");
            this.panelDockProperties.Name = "panelDockProperties";
            this.panelDockProperties.Style.Alignment = StringAlignment.Center;
            this.panelDockProperties.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            this.panelDockProperties.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder;
            this.panelDockProperties.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            this.panelDockProperties.Style.GradientAngle = 90;
            this.panelDockProperties.Resize += new EventHandler(this.panelDockProperties_Resize);
            this.dockContainerItemProperties.Control = this.panelDockProperties;
            this.dockContainerItemProperties.Name = "dockContainerItemProperties";
            resources.ApplyResources(this.dockContainerItemProperties, "dockContainerItemProperties");
            this.dockSite2.AccessibleRole = AccessibleRole.Window;
            this.dockSite2.Controls.Add(this.containerBarGroups);
            this.dockSite2.Controls.Add(this.containerBarRecentPlans);
            this.dockSite2.Controls.Add(this.containerBarEstimating);
            resources.ApplyResources(this.dockSite2, "dockSite2");
            DockSite dockSite = this.dockSite2;
            documentBarContainer = new DocumentBaseContainer[2];
            DocumentBaseContainer[] documentBaseContainerArray = new DocumentBaseContainer[] { new DocumentBarContainer(this.containerBarGroups, 147, 0x123), null };
            DocumentBaseContainer[] documentBarContainer1 = new DocumentBaseContainer[] { new DocumentBarContainer(this.containerBarRecentPlans, 179, 0x109) };
            documentBaseContainerArray[1] = new DocumentDockContainer(documentBarContainer1, eOrientation.Horizontal);
            documentBarContainer[0] = new DocumentDockContainer(documentBaseContainerArray, eOrientation.Vertical);
            documentBarContainer[1] = new DocumentBarContainer(this.containerBarEstimating, 180, 0x225);
            dockSite.DocumentDockContainer = new DocumentDockContainer(documentBarContainer, eOrientation.Horizontal);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.TabStop = false;
            resources.ApplyResources(this.containerBarGroups, "containerBarGroups");
            this.containerBarGroups.AccessibleRole = AccessibleRole.ToolBar;
            this.containerBarGroups.AutoSyncBarCaption = true;
            this.containerBarGroups.CanCustomize = false;
            this.containerBarGroups.CanDockBottom = false;
            this.containerBarGroups.CanDockTab = false;
            this.containerBarGroups.CanDockTop = false;
            this.containerBarGroups.CloseSingleTab = true;
            this.containerBarGroups.Controls.Add(this.panelDockGroups);
            this.containerBarGroups.GrabHandleStyle = eGrabHandleStyle.Caption;
            this.containerBarGroups.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.dockContainerItemGroups
            });

            this.containerBarGroups.LayoutType = eLayoutType.DockContainer;
            this.containerBarGroups.Name = "containerBarGroups";
            this.containerBarGroups.Stretch = true;
            this.containerBarGroups.Style = eDotNetBarStyle.StyleManagerControlled;
            this.containerBarGroups.TabStop = false;
            this.panelDockGroups.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelDockGroups.Controls.Add(this.treeObjects);
            this.panelDockGroups.Controls.Add(this.panelPlanName);
            this.panelDockGroups.Controls.Add(this.barGroups);
            resources.ApplyResources(this.panelDockGroups, "panelDockGroups");
            this.panelDockGroups.Name = "panelDockGroups";
            this.panelDockGroups.Style.Alignment = StringAlignment.Center;
            this.panelDockGroups.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            this.panelDockGroups.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder;
            this.panelDockGroups.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            this.panelDockGroups.Style.GradientAngle = 90;
            this.dockContainerItemGroups.Control = this.panelDockGroups;
            this.dockContainerItemGroups.Name = "dockContainerItemGroups";
            resources.ApplyResources(this.dockContainerItemGroups, "dockContainerItemGroups");
            resources.ApplyResources(this.containerBarRecentPlans, "containerBarRecentPlans");
            this.containerBarRecentPlans.AccessibleRole = AccessibleRole.ToolBar;
            this.containerBarRecentPlans.AutoSyncBarCaption = true;
            this.containerBarRecentPlans.CanCustomize = false;
            this.containerBarRecentPlans.CanDockBottom = false;
            this.containerBarRecentPlans.CanDockTab = false;
            this.containerBarRecentPlans.CanDockTop = false;
            this.containerBarRecentPlans.CloseSingleTab = true;
            this.containerBarRecentPlans.Controls.Add(this.panelDockRecentPlans);
            this.containerBarRecentPlans.GrabHandleStyle = eGrabHandleStyle.Caption;
            this.containerBarRecentPlans.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.dockContainerItemRecentPlans
            });

            this.containerBarRecentPlans.LayoutType = eLayoutType.DockContainer;
            this.containerBarRecentPlans.Name = "containerBarRecentPlans";
            this.containerBarRecentPlans.Stretch = true;
            this.containerBarRecentPlans.Style = eDotNetBarStyle.StyleManagerControlled;
            this.containerBarRecentPlans.TabStop = false;
            this.panelDockRecentPlans.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelDockRecentPlans.Controls.Add(this.lstRecentPlans);
            this.panelDockRecentPlans.Controls.Add(this.barRecentPlans);
            resources.ApplyResources(this.panelDockRecentPlans, "panelDockRecentPlans");
            this.panelDockRecentPlans.Name = "panelDockRecentPlans";
            this.panelDockRecentPlans.Style.Alignment = StringAlignment.Center;
            this.panelDockRecentPlans.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            this.panelDockRecentPlans.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder;
            this.panelDockRecentPlans.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            this.panelDockRecentPlans.Style.GradientAngle = 90;
            this.dockContainerItemRecentPlans.Control = this.panelDockRecentPlans;
            this.dockContainerItemRecentPlans.Name = "dockContainerItemRecentPlans";
            resources.ApplyResources(this.dockContainerItemRecentPlans, "dockContainerItemRecentPlans");
            resources.ApplyResources(this.containerBarEstimating, "containerBarEstimating");
            this.containerBarEstimating.AccessibleRole = AccessibleRole.ToolBar;
            this.containerBarEstimating.AutoSyncBarCaption = true;
            this.containerBarEstimating.CanCustomize = false;
            this.containerBarEstimating.CanDockBottom = false;
            this.containerBarEstimating.CanDockTab = false;
            this.containerBarEstimating.CanDockTop = false;
            this.containerBarEstimating.CloseSingleTab = true;
            this.containerBarEstimating.Controls.Add(this.panelDockEstimating);
            this.containerBarEstimating.GrabHandleStyle = eGrabHandleStyle.Caption;
            this.containerBarEstimating.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.dockContainerItemEstimating
            });

            this.containerBarEstimating.LayoutType = eLayoutType.DockContainer;
            this.containerBarEstimating.Name = "containerBarEstimating";
            this.containerBarEstimating.Stretch = true;
            this.containerBarEstimating.Style = eDotNetBarStyle.StyleManagerControlled;
            this.containerBarEstimating.TabStop = false;
            this.panelDockEstimating.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            this.panelDockEstimating.Controls.Add(this.treeEstimatingItems);
            this.panelDockEstimating.Controls.Add(this.barEstimatingItems);
            resources.ApplyResources(this.panelDockEstimating, "panelDockEstimating");
            this.panelDockEstimating.Name = "panelDockEstimating";
            this.panelDockEstimating.Style.Alignment = StringAlignment.Center;
            this.panelDockEstimating.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            this.panelDockEstimating.Style.BorderColor.ColorSchemePart = eColorSchemePart.BarDockedBorder;
            this.panelDockEstimating.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            this.panelDockEstimating.Style.GradientAngle = 90;
            resources.ApplyResources(this.treeEstimatingItems, "treeEstimatingItems");
            this.treeEstimatingItems.LookAndFeel.SkinName = "Office 2010 Silver";
            this.treeEstimatingItems.Name = "treeEstimatingItems";
            this.treeEstimatingItems.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            this.treeEstimatingItems.OptionsView.ShowHorzLines = false;
            this.treeEstimatingItems.OptionsView.ShowIndicator = false;
            this.treeEstimatingItems.OptionsView.ShowVertLines = false;
            this.barEstimatingItems.CanAutoHide = false;
            this.barEstimatingItems.CanCustomize = false;
            this.barEstimatingItems.CanDockBottom = false;
            this.barEstimatingItems.CanDockLeft = false;
            this.barEstimatingItems.CanDockRight = false;
            this.barEstimatingItems.CanDockTab = false;
            this.barEstimatingItems.CanDockTop = false;
            this.barEstimatingItems.CanMove = false;
            this.barEstimatingItems.CanReorderTabs = false;
            this.barEstimatingItems.CanUndock = false;
            this.barEstimatingItems.ColorScheme.PredefinedColorScheme = ePredefinedColorScheme.Silver2003;
            resources.ApplyResources(this.barEstimatingItems, "barEstimatingItems");
            this.barEstimatingItems.DockTabAlignment = eTabStripAlignment.Left;
            this.barEstimatingItems.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
            {
    this.btEstimatingItemsExpandAll,
    this.btEstimatingItemsCollapseAll,
    this.btEstimatingItemsUpdatePrices,
    this.btEstimatingItemsPrint
            });

            this.barEstimatingItems.Name = "barEstimatingItems";
            this.barEstimatingItems.RoundCorners = false;
            this.barEstimatingItems.SaveLayoutChanges = false;
            this.barEstimatingItems.Stretch = true;
            this.barEstimatingItems.Style = eDotNetBarStyle.StyleManagerControlled;
            this.barEstimatingItems.TabStop = false;
            this.btEstimatingItemsExpandAll.Image = (Image)resources.GetObject("btEstimatingItemsExpandAll.Image");
            this.btEstimatingItemsExpandAll.Name = "btEstimatingItemsExpandAll";
            this.btEstimatingItemsExpandAll.Click += new EventHandler(this.btEstimatingItemsExpandAll_Click);
            this.btEstimatingItemsCollapseAll.Image = (Image)resources.GetObject("btEstimatingItemsCollapseAll.Image");
            this.btEstimatingItemsCollapseAll.ImageFixedSize = new Size(16, 16);
            this.btEstimatingItemsCollapseAll.Name = "btEstimatingItemsCollapseAll";
            this.btEstimatingItemsCollapseAll.Click += new EventHandler(this.btEstimatingItemsCollapseAll_Click);
            this.btEstimatingItemsUpdatePrices.BeginGroup = true;
            this.btEstimatingItemsUpdatePrices.Image = Resources.if_price_tag_usd_172529;
            this.btEstimatingItemsUpdatePrices.ImageFixedSize = new Size(16, 16);
            this.btEstimatingItemsUpdatePrices.Name = "btEstimatingItemsUpdatePrices";
            this.btEstimatingItemsUpdatePrices.Click += new EventHandler(this.btEstimatingItemsUpdatePrices_Click);
            this.btEstimatingItemsPrint.Image = (Image)resources.GetObject("btEstimatingItemsPrint.Image");
            this.btEstimatingItemsPrint.ImageFixedSize = new Size(16, 16);
            this.btEstimatingItemsPrint.Name = "btEstimatingItemsPrint";
            this.btEstimatingItemsPrint.Visible = false;
            this.btEstimatingItemsPrint.Click += new EventHandler(this.btEstimatingItemsPrint_Click);
            this.dockContainerItemEstimating.Control = this.panelDockEstimating;
            this.dockContainerItemEstimating.Name = "dockContainerItemEstimating";
            resources.ApplyResources(this.dockContainerItemEstimating, "dockContainerItemEstimating");
            this.dockSite8.AccessibleRole = AccessibleRole.Window;
            resources.ApplyResources(this.dockSite8, "dockSite8");
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.TabStop = false;
            this.dockSite5.AccessibleRole = AccessibleRole.Window;
            resources.ApplyResources(this.dockSite5, "dockSite5");
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.TabStop = false;
            this.dockSite6.AccessibleRole = AccessibleRole.Window;
            resources.ApplyResources(this.dockSite6, "dockSite6");
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.TabStop = false;
            this.dockSite7.AccessibleRole = AccessibleRole.Window;
            resources.ApplyResources(this.dockSite7, "dockSite7");
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.TabStop = false;
            this.dockSite3.AccessibleRole = AccessibleRole.Window;
            resources.ApplyResources(this.dockSite3, "dockSite3");
            this.dockSite3.DocumentDockContainer = new DocumentDockContainer();
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.TabStop = false;
            this.lblNoPlan.BackColor = SystemColors.Control;
            resources.ApplyResources(this.lblNoPlan, "lblNoPlan");
            this.lblNoPlan.Name = "lblNoPlan";
            this.lblNoPlan.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            this.imageCollection.ImageStream = (ImageCollectionStreamer)resources.GetObject("imageCollection.ImageStream");
            //this.imageCollection.Images.SetKeyName(0, "PrintDialog_16x16.png");
            //this.imageCollection.Images.SetKeyName(1, "PrintViaPDF_16x16.png");
            //this.imageCollection.Images.SetKeyName(2, "material_16x16.png");
            //this.imageCollection.Images.SetKeyName(3, "labor_16x16.png");
            //this.imageCollection.Images.SetKeyName(4, "equipment_16x16.png");
            //this.imageCollection.Images.SetKeyName(5, "subcontract_16x16.png");
            //this.imageCollection.Images.SetKeyName(6, "locked_16x16.png");
            //this.imageCollection.Images.SetKeyName(7, "Folder_16x16.png");
            //this.imageCollection.Images.SetKeyName(8, "FolderOpen_16x16_72.png");
            this.reportsControl.BackColor = SystemColors.Control;
            this.reportsControl.Dirty = false;
            resources.ApplyResources(this.reportsControl, "reportsControl");
            this.reportsControl.Name = "reportsControl";
            this.dockContainerEstimating.Name = "dockContainerEstimating";
            resources.ApplyResources(this.dockContainerEstimating, "dockContainerEstimating");
            resources.ApplyResources(this.treeDBEstimatingItems, "treeDBEstimatingItems");
            this.treeDBEstimatingItems.LookAndFeel.SkinName = "Office 2010 Silver";
            this.treeDBEstimatingItems.Name = "treeDBEstimatingItems";
            this.treeDBEstimatingItems.OptionsBehavior.EnableFiltering = true;
            this.treeDBEstimatingItems.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            this.treeDBEstimatingItems.OptionsView.ShowAutoFilterRow = true;
            this.treeDBEstimatingItems.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            this.treeDBEstimatingItems.OptionsView.ShowHorzLines = false;
            this.treeDBEstimatingItems.OptionsView.ShowVertLines = false;
            resources.ApplyResources(this.treeTemplatesLibrary, "treeTemplatesLibrary");
            this.treeTemplatesLibrary.LookAndFeel.SkinName = "Office 2010 Silver";
            this.treeTemplatesLibrary.Name = "treeTemplatesLibrary";
            this.treeTemplatesLibrary.OptionsBehavior.EnableFiltering = true;
            this.treeTemplatesLibrary.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            this.treeTemplatesLibrary.OptionsView.ShowAutoFilterRow = true;
            this.treeTemplatesLibrary.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            this.treeTemplatesLibrary.OptionsView.ShowHorzLines = false;
            this.treeTemplatesLibrary.OptionsView.ShowVertLines = false;
            base.AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            base.Controls.Add(this.treeTemplatesLibrary);
            base.Controls.Add(this.mainControl);
            base.Controls.Add(this.treeDBEstimatingItems);
            base.Controls.Add(this.lblNoPlan);
            base.Controls.Add(this.panelWelcome);
            base.Controls.Add(this.dockSite2);
            base.Controls.Add(this.dockSite1);
            base.Controls.Add(this.webBrowser);
            base.Controls.Add(this.reportsControl);
            base.Controls.Add(this.flowPlans);
            base.Controls.Add(this.panelPlansAction);
            base.Controls.Add(this.panelBrightnessContrast);
            base.Controls.Add(this.panelRotation);
            base.Controls.Add(this.contextMenuBar1);
            base.Controls.Add(this.dockSite3);
            base.Controls.Add(this.dockSite4);
            base.Controls.Add(this.dockSite5);
            base.Controls.Add(this.dockSite6);
            base.Controls.Add(this.dockSite7);
            base.Controls.Add(this.dockSite8);
            base.Controls.Add(this.barStatus);
            base.Controls.Add(this.ribbonControl);
            base.KeyPreview = true;
            base.Name = "MainForm";
            base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
            base.Shown += new EventHandler(this.MainForm_Shown);
            base.LocationChanged += new EventHandler(this.MainForm_LocationChanged);
            base.KeyDown += new KeyEventHandler(this.frmMain_KeyDown);
            base.Resize += new EventHandler(this.MainForm_Resize);
            this.ribbonControl.ResumeLayout(false);
            this.ribbonControl.PerformLayout();
            this.ribbonPanelEstimating.ResumeLayout(false);
            this.ribbonPanelReport.ResumeLayout(false);
            this.ribbonPanel.ResumeLayout(false);
            this.ribbonPanelTemplates.ResumeLayout(false);
            this.ribbonPanelPlans.ResumeLayout(false);
            this.ribbonPanelExtensions.ResumeLayout(false);
            ((ISupportInitialize)this.contextMenuBar1).EndInit();
            ((ISupportInitialize)this.barStatus).EndInit();
            ((ISupportInitialize)this.lstLayers).EndInit();
            ((ISupportInitialize)this.barLayers).EndInit();
            ((ISupportInitialize)this.barDisplayResults).EndInit();
            ((ISupportInitialize)this.tabProperties).EndInit();
            this.tabProperties.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((ISupportInitialize)this.superTabProperties).EndInit();
            this.superTabProperties.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            ((ISupportInitialize)this.gridObjectProperties).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            ((ISupportInitialize)this.lstRecentPlans).EndInit();
            ((ISupportInitialize)this.barRecentPlans).EndInit();
            ((ISupportInitialize)this.treeObjects).EndInit();
            this.panelPlanName.ResumeLayout(false);
            this.panelPlanName.PerformLayout();
            ((ISupportInitialize)this.barGroups).EndInit();
            ((ISupportInitialize)this.timer1).EndInit();
            this.panelBrightnessContrast.ResumeLayout(false);
            this.panelRotation.ResumeLayout(false);
            this.panelWelcome.ResumeLayout(false);
            this.panelWelcomeMenu.ResumeLayout(false);
            this.panelWelcomeMenu.PerformLayout();
            ((ISupportInitialize)this.lstRecentProjects).EndInit();
            ((ISupportInitialize)this.picWelcome).EndInit();
            this.panelPlansAction.ResumeLayout(false);
            this.dockSite1.ResumeLayout(false);
            ((ISupportInitialize)this.containerBarLayers).EndInit();
            this.containerBarLayers.ResumeLayout(false);
            this.panelDockLayers.ResumeLayout(false);
            ((ISupportInitialize)this.containerBarNavigation).EndInit();
            this.containerBarNavigation.ResumeLayout(false);
            this.panelDockPreview.ResumeLayout(false);
            ((ISupportInitialize)this.containerBarProperties).EndInit();
            this.containerBarProperties.ResumeLayout(false);
            this.panelDockProperties.ResumeLayout(false);
            this.dockSite2.ResumeLayout(false);
            ((ISupportInitialize)this.containerBarGroups).EndInit();
            this.containerBarGroups.ResumeLayout(false);
            this.panelDockGroups.ResumeLayout(false);
            ((ISupportInitialize)this.containerBarRecentPlans).EndInit();
            this.containerBarRecentPlans.ResumeLayout(false);
            this.panelDockRecentPlans.ResumeLayout(false);
            ((ISupportInitialize)this.containerBarEstimating).EndInit();
            this.containerBarEstimating.ResumeLayout(false);
            this.panelDockEstimating.ResumeLayout(false);
            ((ISupportInitialize)this.treeEstimatingItems).EndInit();
            ((ISupportInitialize)this.barEstimatingItems).EndInit();
            ((ISupportInitialize)this.imageCollection).EndInit();
            ((ISupportInitialize)this.treeDBEstimatingItems).EndInit();
            ((ISupportInitialize)this.treeTemplatesLibrary).EndInit();
            base.ResumeLayout(false);
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
