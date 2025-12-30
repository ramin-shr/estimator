namespace QuoterPlan
{
	public partial class MainForm : global::DevComponents.DotNetBar.Office2007RibbonForm
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));

            this.SuspendLayout();

            // If you have a BaseForm.resx, keep this. If not, delete this line.
            resources.ApplyResources(this, "$this");
            global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.MainForm));
			this.ribbonControl = new global::DevComponents.DotNetBar.RibbonControl();
			this.ribbonPanelEstimating = new global::DevComponents.DotNetBar.RibbonPanel();
			this.ribbonEstimating = new global::DevComponents.DotNetBar.RibbonBar();
			this.btEstimatingModify = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingDuplicate = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingDelete = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonEstimatingNew = new global::DevComponents.DotNetBar.RibbonBar();
			this.btEstimatingMaterial = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingLabour = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingEquipment = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingSubcontract = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonEstimatingDatabase = new global::DevComponents.DotNetBar.RibbonBar();
			this.btEstimatingTradesPackages = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingSaveDatabase = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingCompactDatabase = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingImportPrices = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonPanelReport = new global::DevComponents.DotNetBar.RibbonPanel();
			this.ribbonExportReport = new global::DevComponents.DotNetBar.RibbonBar();
			this.btReportExportToExcel = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblExportToExcelOptions = new global::DevComponents.DotNetBar.LabelItem();
			this.buttonItem1 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExportToExcelRawData = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExportToExcelFormattedData = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExportToExcelRawAndFormatted = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportExportToCSV = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportExportToXML = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportExportToHTML = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportExportToPDF = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportExportToEE = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportExportToCOffice = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonPrintReport = new global::DevComponents.DotNetBar.RibbonBar();
			this.btReportPrint = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportPrintPreview = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportPrintSetup = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonReportOrder = new global::DevComponents.DotNetBar.RibbonBar();
			this.btReportFilter = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblReportSystemType = new global::DevComponents.DotNetBar.LabelItem();
			this.btReportScaleImperial = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportScaleMetric = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblReportPrecision = new global::DevComponents.DotNetBar.LabelItem();
			this.btReportScalePrecision64 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportScalePrecision32 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportScalePrecision16 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btReportScalePrecision8 = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblReportTheme = new global::DevComponents.DotNetBar.LabelItem();
			this.btReportSettings = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonPanel = new global::DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarPrintExport = new global::DevComponents.DotNetBar.RibbonBar();
			this.btPrintPlan = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblPrintPlanOptions = new global::DevComponents.DotNetBar.LabelItem();
			this.buttonItem2 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPrintPlanFullSize = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPrintPlanWindow = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExportPlanToPDF = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarImage = new global::DevComponents.DotNetBar.RibbonBar();
			this.btBrightnessContrast = new global::DevComponents.DotNetBar.ButtonItem();
			this.btRotation = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarBrowse = new global::DevComponents.DotNetBar.RibbonBar();
			this.itemContainerBrowse1 = new global::DevComponents.DotNetBar.ItemContainer();
			this.lblBrowseGroup = new global::DevComponents.DotNetBar.LabelItem();
			this.itemContainerBrowse3 = new global::DevComponents.DotNetBar.ItemContainer();
			this.btBrowsePrevious = new global::DevComponents.DotNetBar.ButtonItem();
			this.btBrowseNext = new global::DevComponents.DotNetBar.ButtonItem();
			this.itemContainerBrowse2 = new global::DevComponents.DotNetBar.ItemContainer();
			this.lblBrowseObjectType = new global::DevComponents.DotNetBar.LabelItem();
			this.itemContainerBrowse4 = new global::DevComponents.DotNetBar.ItemContainer();
			this.btBrowseObjectTypePrevious = new global::DevComponents.DotNetBar.ButtonItem();
			this.btBrowseObjectTypeNext = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarZoom = new global::DevComponents.DotNetBar.RibbonBar();
			this.btZoomToSelection = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomToWindow = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomActualSize = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoom75 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoom50 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoom25 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomIn = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomOut = new global::DevComponents.DotNetBar.ButtonItem();
			this.btBookmarks = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomTo75 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomTo50 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomTo25 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomTo150 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomTo200 = new global::DevComponents.DotNetBar.ButtonItem();
			this.buttonItem61 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btModifyBookmarks = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarAnnotations = new global::DevComponents.DotNetBar.RibbonBar();
			this.btMarkZone = new global::DevComponents.DotNetBar.ButtonItem();
			this.btInsertNote = new global::DevComponents.DotNetBar.ButtonItem();
			this.btInsertPicture = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarTools = new global::DevComponents.DotNetBar.RibbonBar();
			this.btToolSelection = new global::DevComponents.DotNetBar.ButtonItem();
			this.btToolPan = new global::DevComponents.DotNetBar.ButtonItem();
			this.btToolArea = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblNoArea = new global::DevComponents.DotNetBar.LabelItem();
			this.lblAreaFilter = new global::DevComponents.DotNetBar.LabelItem();
			this.itemContainerAreaFilter = new global::DevComponents.DotNetBar.ItemContainer();
			this.txtAreaFilter = new global::DevComponents.DotNetBar.TextBoxItem();
			this.btAreaFilterClear = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblAreaFilterPadding = new global::DevComponents.DotNetBar.LabelItem();
			this.lblAreaGroups = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryAreaGroups = new global::DevComponents.DotNetBar.GalleryContainer();
			this.lblAreaTemplates = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryAreaTemplates = new global::DevComponents.DotNetBar.GalleryContainer();
			this.btToolPerimeter = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblNoPerimeter = new global::DevComponents.DotNetBar.LabelItem();
			this.lblPerimeterFilter = new global::DevComponents.DotNetBar.LabelItem();
			this.itemContainerPerimeterFilter = new global::DevComponents.DotNetBar.ItemContainer();
			this.txtPerimeterFilter = new global::DevComponents.DotNetBar.TextBoxItem();
			this.btPerimeterFilterClear = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblPerimeterGroups = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryPerimeterGroups = new global::DevComponents.DotNetBar.GalleryContainer();
			this.lblPerimeterTemplates = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryPerimeterTemplates = new global::DevComponents.DotNetBar.GalleryContainer();
			this.btToolRuler = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblNoDistance = new global::DevComponents.DotNetBar.LabelItem();
			this.lblDistanceFilter = new global::DevComponents.DotNetBar.LabelItem();
			this.itemContainerDistanceFilter = new global::DevComponents.DotNetBar.ItemContainer();
			this.txtDistanceFilter = new global::DevComponents.DotNetBar.TextBoxItem();
			this.btDistanceFilterClear = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblDistanceGroups = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryDistanceGroups = new global::DevComponents.DotNetBar.GalleryContainer();
			this.lblDistanceTemplates = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryDistanceTemplates = new global::DevComponents.DotNetBar.GalleryContainer();
			this.btToolCounter = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblNoCounter = new global::DevComponents.DotNetBar.LabelItem();
			this.lblCounterFilter = new global::DevComponents.DotNetBar.LabelItem();
			this.itemContainerCounterFilter = new global::DevComponents.DotNetBar.ItemContainer();
			this.txtCounterFilter = new global::DevComponents.DotNetBar.TextBoxItem();
			this.btCounterFilterClear = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblCounterGroups = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryCounterGroups = new global::DevComponents.DotNetBar.GalleryContainer();
			this.lblCounterTemplates = new global::DevComponents.DotNetBar.LabelItem();
			this.galleryCounterTemplates = new global::DevComponents.DotNetBar.GalleryContainer();
			this.btToolAngle = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarScale = new global::DevComponents.DotNetBar.RibbonBar();
			this.btScaleSet = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblSystemType = new global::DevComponents.DotNetBar.LabelItem();
			this.btScaleImperial = new global::DevComponents.DotNetBar.ButtonItem();
			this.btScaleMetric = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblPrecision = new global::DevComponents.DotNetBar.LabelItem();
			this.btScalePrecision64 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btScalePrecision32 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btScalePrecision16 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btScalePrecision8 = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarEdit = new global::DevComponents.DotNetBar.RibbonBar();
			this.btEditPaste = new global::DevComponents.DotNetBar.ButtonItem();
			this.itemContainerEdit = new global::DevComponents.DotNetBar.ItemContainer();
			this.btEditCut = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEditCopy = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEditDelete = new global::DevComponents.DotNetBar.ButtonItem();
			this.itemContainerUndoRedo = new global::DevComponents.DotNetBar.ItemContainer();
			this.btEditUndo = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEditRedo = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEditSendData = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarLayout = new global::DevComponents.DotNetBar.RibbonBar();
			this.itemContainerLayouts = new global::DevComponents.DotNetBar.ItemContainer();
			this.opTakeoffLayout = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.opEstimatingLayout = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.ribbonPanelTemplates = new global::DevComponents.DotNetBar.RibbonPanel();
			this.ribbonTemplate = new global::DevComponents.DotNetBar.RibbonBar();
			this.btTemplateModify = new global::DevComponents.DotNetBar.ButtonItem();
			this.btTemplateDuplicate = new global::DevComponents.DotNetBar.ButtonItem();
			this.btTemplateDelete = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonTemplateCreate = new global::DevComponents.DotNetBar.RibbonBar();
			this.btTemplateArea = new global::DevComponents.DotNetBar.ButtonItem();
			this.btTemplatePerimeter = new global::DevComponents.DotNetBar.ButtonItem();
			this.btTemplateLength = new global::DevComponents.DotNetBar.ButtonItem();
			this.btTemplateCounter = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonTemplateDatabase = new global::DevComponents.DotNetBar.RibbonBar();
			this.btTemplateTradesPackages = new global::DevComponents.DotNetBar.ButtonItem();
			this.btTemplateCompactDatabase = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonPanelPlans = new global::DevComponents.DotNetBar.RibbonPanel();
			this.ribbonBarMultiPlans = new global::DevComponents.DotNetBar.RibbonBar();
			this.btPlansPrint = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPlansExport = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarPlans = new global::DevComponents.DotNetBar.RibbonBar();
			this.btPlanLoad = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPlanProperties = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPlanRemove = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPlanExport = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPlanDuplicate = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonBarPlansInsert = new global::DevComponents.DotNetBar.RibbonBar();
			this.btPlanInsertFromPDF = new global::DevComponents.DotNetBar.ButtonItem();
			this.iblImportDPI = new global::DevComponents.DotNetBar.LabelItem();
			this.labelItem2 = new global::DevComponents.DotNetBar.LabelItem();
			this.op172Dpi = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.op300Dpi = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.opOtherDpi = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.sliderDpi = new global::DevComponents.DotNetBar.SliderItem();
			this.itemContainerDpi = new global::DevComponents.DotNetBar.ItemContainer();
			this.lblDpi1 = new global::DevComponents.DotNetBar.LabelItem();
			this.labelDpiPadding1 = new global::DevComponents.DotNetBar.LabelItem();
			this.lblDpi2 = new global::DevComponents.DotNetBar.LabelItem();
			this.iblImportColorManagement = new global::DevComponents.DotNetBar.LabelItem();
			this.labelItem4 = new global::DevComponents.DotNetBar.LabelItem();
			this.opConvertToColor = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.btPlanInsertFromImage = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonPanelExtensions = new global::DevComponents.DotNetBar.RibbonPanel();
			this.ribbonExtension = new global::DevComponents.DotNetBar.RibbonBar();
			this.btExtensionModify = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExtensionDuplicate = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExtensionDelete = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonExtensionCreate = new global::DevComponents.DotNetBar.RibbonBar();
			this.btExtensionArea = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExtensionPerimeter = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExtensionRuler = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExtensionCounter = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonExtensionDatabase = new global::DevComponents.DotNetBar.RibbonBar();
			this.btExtensionTradesPackages = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExtensionCompactDatabase = new global::DevComponents.DotNetBar.ButtonItem();
			this.contextMenuBar1 = new global::DevComponents.DotNetBar.ContextMenuBar();
			this.bEditPopup = new global::DevComponents.DotNetBar.ButtonItem();
			this.bAutoAdjustToZone = new global::DevComponents.DotNetBar.ButtonItem();
			this.bEditNote = new global::DevComponents.DotNetBar.ButtonItem();
			this.bPointInsert = new global::DevComponents.DotNetBar.ButtonItem();
			this.bPointRemove = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSetHeight = new global::DevComponents.DotNetBar.ButtonItem();
			this.bGroupAddObject = new global::DevComponents.DotNetBar.ButtonItem();
			this.bDeductionCreate = new global::DevComponents.DotNetBar.ButtonItem();
			this.bDeductionsEdit = new global::DevComponents.DotNetBar.ButtonItem();
			this.bPerimeterCreateFromArea = new global::DevComponents.DotNetBar.ButtonItem();
			this.bOpeningCreateFromPosition = new global::DevComponents.DotNetBar.ButtonItem();
			this.bOpeningDuplicate = new global::DevComponents.DotNetBar.ButtonItem();
			this.bOpeningCreateFromSegment = new global::DevComponents.DotNetBar.ButtonItem();
			this.bOpeningDelete = new global::DevComponents.DotNetBar.ButtonItem();
			this.bDropInsert = new global::DevComponents.DotNetBar.ButtonItem();
			this.bDropRemove = new global::DevComponents.DotNetBar.ButtonItem();
			this.bPerimeterOpen = new global::DevComponents.DotNetBar.ButtonItem();
			this.bPerimeterClose = new global::DevComponents.DotNetBar.ButtonItem();
			this.bAngleDegreeType = new global::DevComponents.DotNetBar.ButtonItem();
			this.bAngleSlopeType = new global::DevComponents.DotNetBar.ButtonItem();
			this.bDeductionDuplicate = new global::DevComponents.DotNetBar.ButtonItem();
			this.bCut = new global::DevComponents.DotNetBar.ButtonItem();
			this.bCopy = new global::DevComponents.DotNetBar.ButtonItem();
			this.bPaste = new global::DevComponents.DotNetBar.ButtonItem();
			this.bDelete = new global::DevComponents.DotNetBar.ButtonItem();
			this.bToggleMeasures = new global::DevComponents.DotNetBar.ButtonItem();
			this.bZoomToObject = new global::DevComponents.DotNetBar.ButtonItem();
			this.bZoomToGroup = new global::DevComponents.DotNetBar.ButtonItem();
			this.bBringToFront = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSendToBack = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSelectGroup = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSelectThisGroup = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSelectThisGroup1 = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSelectObjectType = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSelectObjectType1 = new global::DevComponents.DotNetBar.ButtonItem();
			this.bSelectAll = new global::DevComponents.DotNetBar.ButtonItem();
			this.bUnselectAll = new global::DevComponents.DotNetBar.ButtonItem();
			this.bLayerMoveTo = new global::DevComponents.DotNetBar.ButtonItem();
			this.bLayerMoveTo1 = new global::DevComponents.DotNetBar.ButtonItem();
			this.bGroupMoveTo = new global::DevComponents.DotNetBar.ButtonItem();
			this.bGroupMoveTo1 = new global::DevComponents.DotNetBar.ButtonItem();
			this.bGroupMoveToNew = new global::DevComponents.DotNetBar.ButtonItem();
			this.ribbonTabStart = new global::DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabPlans = new global::DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabReport = new global::DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabEstimatingItems = new global::DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabItemDBManagement = new global::DevComponents.DotNetBar.RibbonTabItemGroup();
			this.ribbonTabTemplates = new global::DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabExtensions = new global::DevComponents.DotNetBar.RibbonTabItem();
			this.lblTrialMessage = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLicensing = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLicenseBuy = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLicenseActivate = new global::DevComponents.DotNetBar.ButtonItem();
			this.btSettings = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblLanguage = new global::DevComponents.DotNetBar.LabelItem();
			this.btLanguageEnglish = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLanguageFrench = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLanguageSpanish = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblScrollSpeed = new global::DevComponents.DotNetBar.LabelItem();
			this.sliderScrollSpeed = new global::DevComponents.DotNetBar.SliderItem();
			this.containerScroll = new global::DevComponents.DotNetBar.ItemContainer();
			this.lblScrollFast = new global::DevComponents.DotNetBar.LabelItem();
			this.lblScrollPadding = new global::DevComponents.DotNetBar.LabelItem();
			this.lblScrollSlow = new global::DevComponents.DotNetBar.LabelItem();
			this.lblDataFolder = new global::DevComponents.DotNetBar.LabelItem();
			this.lblPersonalPreferences = new global::DevComponents.DotNetBar.LabelItem();
			this.buttonItem3 = new global::DevComponents.DotNetBar.ButtonItem();
			this.btSelectDataFolder = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPersonalPreferences = new global::DevComponents.DotNetBar.ButtonItem();
			this.btImportationPreferences = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEnableAutoBackup = new global::DevComponents.DotNetBar.ButtonItem();
			this.btSetDBReadOnly = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblTheme = new global::DevComponents.DotNetBar.LabelItem();
			this.btStyleMetro = new global::DevComponents.DotNetBar.ButtonItem();
			this.AppCommandTheme = new global::DevComponents.DotNetBar.Command(this.components);
			this.btStyleClassicBlue = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleClassicSilver = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleClassicBlack = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleClassicExecutive = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleRetroBlue = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleRetroSilver = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleRetroBlack = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleRetroGlass = new global::DevComponents.DotNetBar.ButtonItem();
			this.btStyleModern = new global::DevComponents.DotNetBar.ButtonItem();
			this.btSetThemeColor = new global::DevComponents.DotNetBar.ColorPickerDropDown();
			this.lblPanels = new global::DevComponents.DotNetBar.LabelItem();
			this.btResetDefaultPanelsLayout = new global::DevComponents.DotNetBar.ButtonItem();
			this.btHelp = new global::DevComponents.DotNetBar.ButtonItem();
			this.startButton = new global::DevComponents.DotNetBar.Office2007StartButton();
			this.itemContainerFileMenu = new global::DevComponents.DotNetBar.ItemContainer();
			this.itemContainerFileMenu2 = new global::DevComponents.DotNetBar.ItemContainer();
			this.btProjectNew = new global::DevComponents.DotNetBar.ButtonItem();
			this.btProjectOpen = new global::DevComponents.DotNetBar.ButtonItem();
			this.btProjectSave = new global::DevComponents.DotNetBar.ButtonItem();
			this.btProjectSaveAs = new global::DevComponents.DotNetBar.ButtonItem();
			this.btProjectInfo = new global::DevComponents.DotNetBar.ButtonItem();
			this.btProjectClose = new global::DevComponents.DotNetBar.ButtonItem();
			this.btHelpContent = new global::DevComponents.DotNetBar.ButtonItem();
			this.btHelpYoutube = new global::DevComponents.DotNetBar.ButtonItem();
			this.btHelpAbout = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLicenseDeactivate = new global::DevComponents.DotNetBar.ButtonItem();
			this.btExit = new global::DevComponents.DotNetBar.ButtonItem();
			this.galleryRecentProjects = new global::DevComponents.DotNetBar.GalleryContainer();
			this.lblFileRecentProjects = new global::DevComponents.DotNetBar.LabelItem();
			this.btSave = new global::DevComponents.DotNetBar.ButtonItem();
			this.btUndo = new global::DevComponents.DotNetBar.ButtonItem();
			this.btRedo = new global::DevComponents.DotNetBar.ButtonItem();
			this.galleryGroup1 = new global::DevComponents.DotNetBar.GalleryGroup();
			this.barStatus = new global::DevComponents.DotNetBar.Bar();
			this.lblStatus = new global::DevComponents.DotNetBar.LabelItem();
			this.lblStatusBarPadding = new global::DevComponents.DotNetBar.LabelItem();
			this.lblOrtho = new global::DevComponents.DotNetBar.LabelItem();
			this.switchOrtho = new global::DevComponents.DotNetBar.SwitchButtonItem();
			this.lblStatusBarPadding2 = new global::DevComponents.DotNetBar.LabelItem();
			this.lblImageQuality = new global::DevComponents.DotNetBar.LabelItem();
			this.qualitySlider = new global::DevComponents.DotNetBar.SliderItem();
			this.lblStatusBarPadding3 = new global::DevComponents.DotNetBar.LabelItem();
			this.lblZoom = new global::DevComponents.DotNetBar.LabelItem();
			this.lblStatusPadding2 = new global::DevComponents.DotNetBar.LabelItem();
			this.zoomSlider = new global::DevComponents.DotNetBar.SliderItem();
			this.lstLayers = new global::DevComponents.AdvTree.AdvTree();
			this.columnLayerVisible = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnLayerName = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnLayerOpacity = new global::DevComponents.AdvTree.ColumnHeader();
			this.nodeConnector2 = new global::DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new global::DevComponents.DotNetBar.ElementStyle();
			this.elementStyle8 = new global::DevComponents.DotNetBar.ElementStyle();
			this.elementStyle2 = new global::DevComponents.DotNetBar.ElementStyle();
			this.barLayers = new global::DevComponents.DotNetBar.Bar();
			this.btLayerAdd = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayerRemove = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayerRename = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayerMoveUp = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayerMoveDown = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayerSaveList = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayerSaveListAs = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayerOpenList = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayersToggle = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayersMakeVisible = new global::DevComponents.DotNetBar.ButtonItem();
			this.btLayersMakeInvisible = new global::DevComponents.DotNetBar.ButtonItem();
			this.barDisplayResults = new global::DevComponents.DotNetBar.Bar();
			this.lblDisplayResults = new global::DevComponents.DotNetBar.LabelItem();
			this.btDisplayResultsForThisPlan = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblDisplayResultsPadding = new global::DevComponents.DotNetBar.LabelItem();
			this.btDisplayResultsForAllPlans = new global::DevComponents.DotNetBar.ButtonItem();
			this.tabProperties = new global::DevComponents.DotNetBar.TabControl();
			this.tabControlPanel1 = new global::DevComponents.DotNetBar.TabControlPanel();
			this.superTabProperties = new global::DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel1 = new global::DevComponents.DotNetBar.SuperTabControlPanel();
			this.gridObjectProperties = new global::DevComponents.DotNetBar.AdvPropertyGrid();
			this.superTabItem1 = new global::DevComponents.DotNetBar.SuperTabItem();
			this.tabItem1 = new global::DevComponents.DotNetBar.TabItem(this.components);
			this.tabControlPanel2 = new global::DevComponents.DotNetBar.TabControlPanel();
			this.extensionsManager = new global::QuoterPlan.ExtensionsManager();
			this.tabItem2 = new global::DevComponents.DotNetBar.TabItem(this.components);
			this.cbObjects = new global::DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.panControl = new global::QuoterPlanControls.PanControl();
			this.lstRecentPlans = new global::DevComponents.AdvTree.AdvTree();
			this.columnHeader1 = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnHeader2 = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnHeader5 = new global::DevComponents.AdvTree.ColumnHeader();
			this.nodeConnector3 = new global::DevComponents.AdvTree.NodeConnector();
			this.elementStyle3 = new global::DevComponents.DotNetBar.ElementStyle();
			this.elementStyle4 = new global::DevComponents.DotNetBar.ElementStyle();
			this.barRecentPlans = new global::DevComponents.DotNetBar.Bar();
			this.btPlanRename = new global::DevComponents.DotNetBar.ButtonItem();
			this.treeObjects = new global::DevComponents.AdvTree.AdvTree();
			this.columnObjectIcon = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnObjectName = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnObjectInfo = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnObjectColor = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnObjectVisible = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnObjectPadding = new global::DevComponents.AdvTree.ColumnHeader();
			this.nodeConnector1 = new global::DevComponents.AdvTree.NodeConnector();
			this.elementStyle7 = new global::DevComponents.DotNetBar.ElementStyle();
			this.panelPlanName = new global::System.Windows.Forms.Panel();
			this.txtPlanName = new global::QuoterPlan.TextBoxEx();
			this.cbPlans = new global::DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.barGroups = new global::DevComponents.DotNetBar.Bar();
			this.btGroupLocate = new global::DevComponents.DotNetBar.ButtonItem();
			this.btZoomToObject = new global::DevComponents.DotNetBar.ButtonItem();
			this.btGroupSelect = new global::DevComponents.DotNetBar.ButtonItem();
			this.btGroupRemove = new global::DevComponents.DotNetBar.ButtonItem();
			this.btGroupRename = new global::DevComponents.DotNetBar.ButtonItem();
			this.btRenamePlan = new global::DevComponents.DotNetBar.ButtonItem();
			this.btGroupsToggle = new global::DevComponents.DotNetBar.ButtonItem();
			this.btGroupsMakeVisible = new global::DevComponents.DotNetBar.ButtonItem();
			this.btGroupsMakeInvisible = new global::DevComponents.DotNetBar.ButtonItem();
			this.checkBoxItem1 = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.sliderItem1 = new global::DevComponents.DotNetBar.SliderItem();
			this.checkBoxItem2 = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.sliderItem2 = new global::DevComponents.DotNetBar.SliderItem();
			this.timer1 = new global::System.Timers.Timer();
			this.itemContainer5 = new global::DevComponents.DotNetBar.ItemContainer();
			this.galleryGroupArea = new global::DevComponents.DotNetBar.GalleryGroup();
			this.galleryGroupPerimeter = new global::DevComponents.DotNetBar.GalleryGroup();
			this.galleryGroupCounter = new global::DevComponents.DotNetBar.GalleryGroup();
			this.tabItem3 = new global::DevComponents.DotNetBar.TabItem(this.components);
			this.backgroundWorker = new global::System.ComponentModel.BackgroundWorker();
			this.flowPlans = new global::System.Windows.Forms.FlowLayoutPanel();
			this.sliderItem4 = new global::DevComponents.DotNetBar.SliderItem();
			this.itemContainerBrightness = new global::DevComponents.DotNetBar.ItemContainer();
			this.lblBrightness = new global::DevComponents.DotNetBar.LabelItem();
			this.sliderBrightness = new global::DevComponents.DotNetBar.SliderItem();
			this.lblBrightnessContrastPadding1 = new global::DevComponents.DotNetBar.LabelItem();
			this.itemContainerContrast = new global::DevComponents.DotNetBar.ItemContainer();
			this.lblContrast = new global::DevComponents.DotNetBar.LabelItem();
			this.sliderContrast = new global::DevComponents.DotNetBar.SliderItem();
			this.ribbonBarBrightnessContrast = new global::DevComponents.DotNetBar.RibbonBar();
			this.lblBrightnessContrastPadding2 = new global::DevComponents.DotNetBar.LabelItem();
			this.lblBrightnessContrastSeparator = new global::DevComponents.DotNetBar.LabelItem();
			this.lblBrightnessContrastPadding3 = new global::DevComponents.DotNetBar.LabelItem();
			this.btBrightnessContrastApply = new global::DevComponents.DotNetBar.ButtonItem();
			this.btBrightnessContrastCancel = new global::DevComponents.DotNetBar.ButtonItem();
			this.btBrightnessContrastRestore = new global::DevComponents.DotNetBar.ButtonItem();
			this.panelBrightnessContrast = new global::DevComponents.DotNetBar.PanelEx();
			this.panelRotation = new global::DevComponents.DotNetBar.PanelEx();
			this.ribbonBarRotation = new global::DevComponents.DotNetBar.RibbonBar();
			this.btFlipHorizontally = new global::DevComponents.DotNetBar.ButtonItem();
			this.btFlipVertically = new global::DevComponents.DotNetBar.ButtonItem();
			this.btRotateLeft = new global::DevComponents.DotNetBar.ButtonItem();
			this.btRotateRight = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblBarRotationPadding1 = new global::DevComponents.DotNetBar.LabelItem();
			this.lblBarRotationSeparator = new global::DevComponents.DotNetBar.LabelItem();
			this.lblBarRotationPadding2 = new global::DevComponents.DotNetBar.LabelItem();
			this.btRotationApply = new global::DevComponents.DotNetBar.ButtonItem();
			this.btRotationCancel = new global::DevComponents.DotNetBar.ButtonItem();
			this.webBrowser = new global::System.Windows.Forms.WebBrowser();
			this.comboItem1 = new global::DevComponents.Editors.ComboItem();
			this.comboItem2 = new global::DevComponents.Editors.ComboItem();
			this.comboItem3 = new global::DevComponents.Editors.ComboItem();
			this.comboItem4 = new global::DevComponents.Editors.ComboItem();
			this.comboItem5 = new global::DevComponents.Editors.ComboItem();
			this.comboItem6 = new global::DevComponents.Editors.ComboItem();
			this.labelItem1 = new global::DevComponents.DotNetBar.LabelItem();
			this.panelWelcome = new global::System.Windows.Forms.Panel();
			this.panelWelcomeMenu = new global::DevComponents.DotNetBar.PanelEx();
			this.lblRecentProjects = new global::QuoterPlan.LabelEx();
			this.btNew = new global::DevComponents.DotNetBar.ButtonX();
			this.lstRecentProjects = new global::DevComponents.AdvTree.AdvTree();
			this.columnHeader3 = new global::DevComponents.AdvTree.ColumnHeader();
			this.columnHeader4 = new global::DevComponents.AdvTree.ColumnHeader();
			this.nodeConnector4 = new global::DevComponents.AdvTree.NodeConnector();
			this.elementStyle5 = new global::DevComponents.DotNetBar.ElementStyle();
			this.elementStyle6 = new global::DevComponents.DotNetBar.ElementStyle();
			this.btOpen = new global::DevComponents.DotNetBar.ButtonX();
			this.picWelcome = new global::System.Windows.Forms.PictureBox();
			this.superTooltip = new global::DevComponents.DotNetBar.SuperTooltip();
			this.panelPlansAction = new global::DevComponents.DotNetBar.PanelEx();
			this.ribbonBarPlansAction = new global::DevComponents.DotNetBar.RibbonBar();
			this.progressPlansAction = new global::DevComponents.DotNetBar.CircularProgressItem();
			this.itemContainerExportType = new global::DevComponents.DotNetBar.ItemContainer();
			this.lblBarPlansActionPadding3 = new global::DevComponents.DotNetBar.LabelItem();
			this.checkBoxExportSingleFile = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.checkBoxExportMultiFiles = new global::DevComponents.DotNetBar.CheckBoxItem();
			this.lblBarPlansActionPadding4 = new global::DevComponents.DotNetBar.LabelItem();
			this.btPlansActionSelectAll = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPlansActionSelectNone = new global::DevComponents.DotNetBar.ButtonItem();
			this.lblBarPlansActionPadding1 = new global::DevComponents.DotNetBar.LabelItem();
			this.lblPlansActionSeparator = new global::DevComponents.DotNetBar.LabelItem();
			this.lblBarPlansActionPadding2 = new global::DevComponents.DotNetBar.LabelItem();
			this.btPlansActionApply = new global::DevComponents.DotNetBar.ButtonItem();
			this.btPlansActionCancel = new global::DevComponents.DotNetBar.ButtonItem();
			this.mainControl = new global::QuoterPlanControls.MainControl();
			this.dotNetBarManager = new global::DevComponents.DotNetBar.DotNetBarManager(this.components);
			this.dockSite4 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite1 = new global::DevComponents.DotNetBar.DockSite();
			this.containerBarLayers = new global::DevComponents.DotNetBar.Bar();
			this.panelDockLayers = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItemLayers = new global::DevComponents.DotNetBar.DockContainerItem();
			this.containerBarNavigation = new global::DevComponents.DotNetBar.Bar();
			this.panelDockPreview = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItemPreview = new global::DevComponents.DotNetBar.DockContainerItem();
			this.containerBarProperties = new global::DevComponents.DotNetBar.Bar();
			this.panelDockProperties = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItemProperties = new global::DevComponents.DotNetBar.DockContainerItem();
			this.dockSite2 = new global::DevComponents.DotNetBar.DockSite();
			this.containerBarGroups = new global::DevComponents.DotNetBar.Bar();
			this.panelDockGroups = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItemGroups = new global::DevComponents.DotNetBar.DockContainerItem();
			this.containerBarRecentPlans = new global::DevComponents.DotNetBar.Bar();
			this.panelDockRecentPlans = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItemRecentPlans = new global::DevComponents.DotNetBar.DockContainerItem();
			this.containerBarEstimating = new global::DevComponents.DotNetBar.Bar();
			this.panelDockEstimating = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.treeEstimatingItems = new global::DevExpress.XtraTreeList.TreeList();
			this.barEstimatingItems = new global::DevComponents.DotNetBar.Bar();
			this.btEstimatingItemsExpandAll = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingItemsCollapseAll = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingItemsUpdatePrices = new global::DevComponents.DotNetBar.ButtonItem();
			this.btEstimatingItemsPrint = new global::DevComponents.DotNetBar.ButtonItem();
			this.dockContainerItemEstimating = new global::DevComponents.DotNetBar.DockContainerItem();
			this.dockSite8 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite5 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite6 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite7 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite3 = new global::DevComponents.DotNetBar.DockSite();
			this.lblNoPlan = new global::QuoterPlan.LabelEx();
			this.imageCollection = new global::DevExpress.Utils.ImageCollection(this.components);
			this.reportsControl = new global::QuoterPlan.ReportsControl();
			this.dockContainerEstimating = new global::DevComponents.DotNetBar.DockContainerItem();
			this.treeDBEstimatingItems = new global::DevExpress.XtraTreeList.TreeList();
			this.treeTemplatesLibrary = new global::DevExpress.XtraTreeList.TreeList();
			this.ribbonControl.SuspendLayout();
			this.ribbonPanelEstimating.SuspendLayout();
			this.ribbonPanelReport.SuspendLayout();
			this.ribbonPanel.SuspendLayout();
			this.ribbonPanelTemplates.SuspendLayout();
			this.ribbonPanelPlans.SuspendLayout();
			this.ribbonPanelExtensions.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.contextMenuBar1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.barStatus).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.lstLayers).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.barLayers).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.barDisplayResults).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.tabProperties).BeginInit();
			this.tabProperties.SuspendLayout();
			this.tabControlPanel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.superTabProperties).BeginInit();
			this.superTabProperties.SuspendLayout();
			this.superTabControlPanel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.gridObjectProperties).BeginInit();
			this.tabControlPanel2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.lstRecentPlans).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.barRecentPlans).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeObjects).BeginInit();
			this.panelPlanName.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.barGroups).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.timer1).BeginInit();
			this.panelBrightnessContrast.SuspendLayout();
			this.panelRotation.SuspendLayout();
			this.panelWelcome.SuspendLayout();
			this.panelWelcomeMenu.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.lstRecentProjects).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.picWelcome).BeginInit();
			this.panelPlansAction.SuspendLayout();
			this.dockSite1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.containerBarLayers).BeginInit();
			this.containerBarLayers.SuspendLayout();
			this.panelDockLayers.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.containerBarNavigation).BeginInit();
			this.containerBarNavigation.SuspendLayout();
			this.panelDockPreview.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.containerBarProperties).BeginInit();
			this.containerBarProperties.SuspendLayout();
			this.panelDockProperties.SuspendLayout();
			this.dockSite2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.containerBarGroups).BeginInit();
			this.containerBarGroups.SuspendLayout();
			this.panelDockGroups.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.containerBarRecentPlans).BeginInit();
			this.containerBarRecentPlans.SuspendLayout();
			this.panelDockRecentPlans.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.containerBarEstimating).BeginInit();
			this.containerBarEstimating.SuspendLayout();
			this.panelDockEstimating.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.treeEstimatingItems).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.barEstimatingItems).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.imageCollection).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeDBEstimatingItems).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeTemplatesLibrary).BeginInit();
			base.SuspendLayout();
			this.ribbonControl.AutoExpand = false;
			this.ribbonControl.AutoKeyboardExpand = false;
			this.ribbonControl.BackColor = global::System.Drawing.Color.FromArgb(255, 255, 255);
			this.ribbonControl.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.CanCustomize = false;
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Controls.Add(this.ribbonPanelEstimating);
			this.ribbonControl.Controls.Add(this.ribbonPanelReport);
			this.ribbonControl.Controls.Add(this.ribbonPanel);
			this.ribbonControl.Controls.Add(this.ribbonPanelTemplates);
			this.ribbonControl.Controls.Add(this.ribbonPanelPlans);
			this.ribbonControl.Controls.Add(this.ribbonPanelExtensions);
			resources.ApplyResources(this.ribbonControl, "ribbonControl");
			this.ribbonControl.ForeColor = global::System.Drawing.Color.Black;
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
			this.ribbonControl.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
			this.ribbonControl.TabGroups.AddRange(new global::DevComponents.DotNetBar.RibbonTabItemGroup[]
			{
				this.ribbonTabItemDBManagement
			});
			this.ribbonControl.TabGroupsVisible = true;
			this.ribbonControl.UseCustomizeDialog = false;
			this.ribbonControl.SelectedRibbonTabChanged += new global::System.EventHandler(this.ribbonControl_SelectedRibbonTabChanged);
			this.ribbonPanelEstimating.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimating);
			this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimatingNew);
			this.ribbonPanelEstimating.Controls.Add(this.ribbonEstimatingDatabase);
			resources.ApplyResources(this.ribbonPanelEstimating, "ribbonPanelEstimating");
			this.ribbonPanelEstimating.Name = "ribbonPanelEstimating";
			this.ribbonPanelEstimating.Style.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelEstimating.StyleMouseDown.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelEstimating.StyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonEstimating.AutoOverflowEnabled = false;
			this.ribbonEstimating.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonEstimating.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonEstimating.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonEstimating.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonEstimating.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btEstimatingModify.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingModify.CanCustomize = false;
			this.btEstimatingModify.Image = global::QuoterPlan.Properties.Resources.properties;
			this.btEstimatingModify.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingModify.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingModify.Name = "btEstimatingModify";
			resources.ApplyResources(this.btEstimatingModify, "btEstimatingModify");
			this.btEstimatingModify.Click += new global::System.EventHandler(this.btEstimatingModify_Click);
			this.btEstimatingDuplicate.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingDuplicate.CanCustomize = false;
			this.btEstimatingDuplicate.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingDuplicate.Image");
			this.btEstimatingDuplicate.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingDuplicate.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingDuplicate.Name = "btEstimatingDuplicate";
			resources.ApplyResources(this.btEstimatingDuplicate, "btEstimatingDuplicate");
			this.btEstimatingDuplicate.Click += new global::System.EventHandler(this.btEstimatingDuplicate_Click);
			this.btEstimatingDelete.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingDelete.CanCustomize = false;
			this.btEstimatingDelete.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingDelete.Image");
			this.btEstimatingDelete.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingDelete.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingDelete.Name = "btEstimatingDelete";
			resources.ApplyResources(this.btEstimatingDelete, "btEstimatingDelete");
			this.btEstimatingDelete.Click += new global::System.EventHandler(this.btEstimatingDelete_Click);
			this.ribbonEstimatingNew.AutoOverflowEnabled = false;
			this.ribbonEstimatingNew.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonEstimatingNew.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonEstimatingNew.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonEstimatingNew.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonEstimatingNew.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btEstimatingMaterial.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingMaterial.CanCustomize = false;
			this.btEstimatingMaterial.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingMaterial.Image");
			this.btEstimatingMaterial.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingMaterial.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingMaterial.Name = "btEstimatingMaterial";
			resources.ApplyResources(this.btEstimatingMaterial, "btEstimatingMaterial");
			this.btEstimatingMaterial.Click += new global::System.EventHandler(this.btEstimatingMaterial_Click);
			this.btEstimatingLabour.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingLabour.CanCustomize = false;
			this.btEstimatingLabour.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingLabour.Image");
			this.btEstimatingLabour.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingLabour.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingLabour.Name = "btEstimatingLabour";
			resources.ApplyResources(this.btEstimatingLabour, "btEstimatingLabour");
			this.btEstimatingLabour.Click += new global::System.EventHandler(this.btEstimatingLabour_Click);
			this.btEstimatingEquipment.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingEquipment.CanCustomize = false;
			this.btEstimatingEquipment.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingEquipment.Image");
			this.btEstimatingEquipment.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingEquipment.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingEquipment.Name = "btEstimatingEquipment";
			resources.ApplyResources(this.btEstimatingEquipment, "btEstimatingEquipment");
			this.btEstimatingEquipment.Click += new global::System.EventHandler(this.btEstimatingEquipment_Click);
			this.btEstimatingSubcontract.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingSubcontract.CanCustomize = false;
			this.btEstimatingSubcontract.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingSubcontract.Image");
			this.btEstimatingSubcontract.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingSubcontract.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingSubcontract.Name = "btEstimatingSubcontract";
			resources.ApplyResources(this.btEstimatingSubcontract, "btEstimatingSubcontract");
			this.btEstimatingSubcontract.Click += new global::System.EventHandler(this.btEstimatingSubcontract_Click);
			this.ribbonEstimatingDatabase.AutoOverflowEnabled = false;
			this.ribbonEstimatingDatabase.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonEstimatingDatabase.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonEstimatingDatabase.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonEstimatingDatabase.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonEstimatingDatabase.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btEstimatingTradesPackages.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingTradesPackages.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingTradesPackages.Image");
			this.btEstimatingTradesPackages.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingTradesPackages.ImagePaddingHorizontal = 10;
			this.btEstimatingTradesPackages.ImagePaddingVertical = 5;
			this.btEstimatingTradesPackages.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingTradesPackages.Name = "btEstimatingTradesPackages";
			resources.ApplyResources(this.btEstimatingTradesPackages, "btEstimatingTradesPackages");
			this.btEstimatingTradesPackages.Visible = false;
			this.btEstimatingSaveDatabase.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingSaveDatabase.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingSaveDatabase.Image");
			this.btEstimatingSaveDatabase.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingSaveDatabase.ImagePaddingVertical = 5;
			this.btEstimatingSaveDatabase.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingSaveDatabase.Name = "btEstimatingSaveDatabase";
			resources.ApplyResources(this.btEstimatingSaveDatabase, "btEstimatingSaveDatabase");
			this.btEstimatingSaveDatabase.Click += new global::System.EventHandler(this.btEstimatingSaveDatabase_Click);
			this.btEstimatingCompactDatabase.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingCompactDatabase.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingCompactDatabase.Image");
			this.btEstimatingCompactDatabase.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingCompactDatabase.ImagePaddingVertical = 5;
			this.btEstimatingCompactDatabase.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingCompactDatabase.Name = "btEstimatingCompactDatabase";
			resources.ApplyResources(this.btEstimatingCompactDatabase, "btEstimatingCompactDatabase");
			this.btEstimatingCompactDatabase.Visible = false;
			this.btEstimatingImportPrices.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEstimatingImportPrices.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingImportPrices.Image");
			this.btEstimatingImportPrices.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEstimatingImportPrices.ImagePaddingVertical = 5;
			this.btEstimatingImportPrices.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEstimatingImportPrices.Name = "btEstimatingImportPrices";
			resources.ApplyResources(this.btEstimatingImportPrices, "btEstimatingImportPrices");
			this.btEstimatingImportPrices.Click += new global::System.EventHandler(this.btEstimatingImportPrices_Click);
			this.ribbonPanelReport.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelReport.Controls.Add(this.ribbonExportReport);
			this.ribbonPanelReport.Controls.Add(this.ribbonPrintReport);
			this.ribbonPanelReport.Controls.Add(this.ribbonReportOrder);
			resources.ApplyResources(this.ribbonPanelReport, "ribbonPanelReport");
			this.ribbonPanelReport.Name = "ribbonPanelReport";
			this.ribbonPanelReport.Style.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelReport.StyleMouseDown.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelReport.StyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExportReport.AutoOverflowEnabled = false;
			this.ribbonExportReport.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExportReport.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonExportReport.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonExportReport.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExportReport.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btReportExportToExcel.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportExportToExcel.CanCustomize = false;
			this.btReportExportToExcel.Image = (global::System.Drawing.Image)resources.GetObject("btReportExportToExcel.Image");
			this.btReportExportToExcel.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportExportToExcel.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btReportExportToExcel.Click += new global::System.EventHandler(this.btReportExportToExcel_Click);
			this.lblExportToExcelOptions.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblExportToExcelOptions.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblExportToExcelOptions.CanCustomize = false;
			this.lblExportToExcelOptions.Name = "lblExportToExcelOptions";
			this.lblExportToExcelOptions.PaddingBottom = 3;
			this.lblExportToExcelOptions.PaddingLeft = 10;
			this.lblExportToExcelOptions.PaddingTop = 3;
			this.lblExportToExcelOptions.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			this.lblExportToExcelOptions.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.buttonItem1
			});
			resources.ApplyResources(this.lblExportToExcelOptions, "lblExportToExcelOptions");
			this.lblExportToExcelOptions.Visible = false;
			this.buttonItem1.CanCustomize = false;
			this.buttonItem1.Name = "buttonItem1";
			this.buttonItem1.Text = global::QuoterPlan.Properties.Resources.Impérial;
			this.btExportToExcelRawData.AutoCheckOnClick = true;
			this.btExportToExcelRawData.CanCustomize = false;
			this.btExportToExcelRawData.Name = "btExportToExcelRawData";
			this.btExportToExcelRawData.OptionGroup = "ExportToExcelType";
			resources.ApplyResources(this.btExportToExcelRawData, "btExportToExcelRawData");
			this.btExportToExcelRawData.Visible = false;
			this.btExportToExcelRawData.Click += new global::System.EventHandler(this.btExportToExcelType_Click);
			this.btExportToExcelFormattedData.AutoCheckOnClick = true;
			this.btExportToExcelFormattedData.CanCustomize = false;
			this.btExportToExcelFormattedData.Name = "btExportToExcelFormattedData";
			this.btExportToExcelFormattedData.OptionGroup = "ExportToExcelType";
			resources.ApplyResources(this.btExportToExcelFormattedData, "btExportToExcelFormattedData");
			this.btExportToExcelFormattedData.Visible = false;
			this.btExportToExcelFormattedData.Click += new global::System.EventHandler(this.btExportToExcelType_Click);
			this.btExportToExcelRawAndFormatted.AutoCheckOnClick = true;
			this.btExportToExcelRawAndFormatted.CanCustomize = false;
			this.btExportToExcelRawAndFormatted.Name = "btExportToExcelRawAndFormatted";
			this.btExportToExcelRawAndFormatted.OptionGroup = "ExportToExcelType";
			resources.ApplyResources(this.btExportToExcelRawAndFormatted, "btExportToExcelRawAndFormatted");
			this.btExportToExcelRawAndFormatted.Visible = false;
			this.btExportToExcelRawAndFormatted.Click += new global::System.EventHandler(this.btExportToExcelType_Click);
			this.btReportExportToCSV.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportExportToCSV.Image = (global::System.Drawing.Image)resources.GetObject("btReportExportToCSV.Image");
			this.btReportExportToCSV.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportExportToCSV.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportExportToCSV.Name = "btReportExportToCSV";
			resources.ApplyResources(this.btReportExportToCSV, "btReportExportToCSV");
			this.btReportExportToCSV.Visible = false;
			this.btReportExportToCSV.Click += new global::System.EventHandler(this.btReportExportToCSV_Click);
			this.btReportExportToXML.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportExportToXML.Image = (global::System.Drawing.Image)resources.GetObject("btReportExportToXML.Image");
			this.btReportExportToXML.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportExportToXML.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportExportToXML.Name = "btReportExportToXML";
			resources.ApplyResources(this.btReportExportToXML, "btReportExportToXML");
			this.btReportExportToXML.Click += new global::System.EventHandler(this.btReportExportToXML_Click);
			this.btReportExportToHTML.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportExportToHTML.Image = (global::System.Drawing.Image)resources.GetObject("btReportExportToHTML.Image");
			this.btReportExportToHTML.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportExportToHTML.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportExportToHTML.Name = "btReportExportToHTML";
			resources.ApplyResources(this.btReportExportToHTML, "btReportExportToHTML");
			this.btReportExportToHTML.Click += new global::System.EventHandler(this.btReportExportToHTML_Click);
			this.btReportExportToPDF.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportExportToPDF.Image = global::QuoterPlan.Properties.Resources.file_pdf_40x40;
			this.btReportExportToPDF.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportExportToPDF.ImagePaddingVertical = 5;
			this.btReportExportToPDF.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportExportToPDF.Name = "btReportExportToPDF";
			resources.ApplyResources(this.btReportExportToPDF, "btReportExportToPDF");
			this.btReportExportToPDF.Click += new global::System.EventHandler(this.btReportExportToPDF_Click);
			this.btReportExportToEE.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportExportToEE.Image = (global::System.Drawing.Image)resources.GetObject("btReportExportToEE.Image");
			this.btReportExportToEE.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportExportToEE.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportExportToEE.Name = "btReportExportToEE";
			resources.ApplyResources(this.btReportExportToEE, "btReportExportToEE");
			this.btReportExportToEE.Visible = false;
			this.btReportExportToCOffice.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportExportToCOffice.Image = (global::System.Drawing.Image)resources.GetObject("btReportExportToCOffice.Image");
			this.btReportExportToCOffice.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportExportToCOffice.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportExportToCOffice.Name = "btReportExportToCOffice";
			resources.ApplyResources(this.btReportExportToCOffice, "btReportExportToCOffice");
			this.btReportExportToCOffice.Visible = false;
			this.btReportExportToCOffice.Click += new global::System.EventHandler(this.btReportExportToCOffice_Click);
			this.ribbonPrintReport.AutoOverflowEnabled = false;
			this.ribbonPrintReport.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPrintReport.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonPrintReport.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPrintReport.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPrintReport.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btReportPrint.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportPrint.Image = (global::System.Drawing.Image)resources.GetObject("btReportPrint.Image");
			this.btReportPrint.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportPrint.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportPrint.Name = "btReportPrint";
			resources.ApplyResources(this.btReportPrint, "btReportPrint");
			this.btReportPrint.Click += new global::System.EventHandler(this.btReportPrint_Click);
			this.btReportPrintPreview.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportPrintPreview.Image = (global::System.Drawing.Image)resources.GetObject("btReportPrintPreview.Image");
			this.btReportPrintPreview.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportPrintPreview.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportPrintPreview.Name = "btReportPrintPreview";
			resources.ApplyResources(this.btReportPrintPreview, "btReportPrintPreview");
			this.btReportPrintPreview.Visible = false;
			this.btReportPrintPreview.Click += new global::System.EventHandler(this.btReportPrintPreview_Click);
			this.btReportPrintSetup.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportPrintSetup.Image = (global::System.Drawing.Image)resources.GetObject("btReportPrintSetup.Image");
			this.btReportPrintSetup.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportPrintSetup.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportPrintSetup.Name = "btReportPrintSetup";
			resources.ApplyResources(this.btReportPrintSetup, "btReportPrintSetup");
			this.btReportPrintSetup.Visible = false;
			this.btReportPrintSetup.Click += new global::System.EventHandler(this.btReportPrintSetup_Click);
			this.ribbonReportOrder.AutoOverflowEnabled = false;
			this.ribbonReportOrder.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonReportOrder.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonReportOrder.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonReportOrder.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonReportOrder.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btReportFilter.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportFilter.CanCustomize = false;
			this.btReportFilter.Image = (global::System.Drawing.Image)resources.GetObject("btReportFilter.Image");
			this.btReportFilter.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportFilter.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btReportFilter.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btReportEdit_PopupOpen);
			this.btReportFilter.Click += new global::System.EventHandler(this.btReportEdit_Click);
			this.lblReportSystemType.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblReportSystemType.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblReportSystemType.CanCustomize = false;
			this.lblReportSystemType.Name = "lblReportSystemType";
			this.lblReportSystemType.PaddingBottom = 3;
			this.lblReportSystemType.PaddingLeft = 10;
			this.lblReportSystemType.PaddingTop = 3;
			this.lblReportSystemType.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblReportSystemType, "lblReportSystemType");
			this.btReportScaleImperial.CanCustomize = false;
			this.btReportScaleImperial.Name = "btReportScaleImperial";
			this.btReportScaleImperial.Text = global::QuoterPlan.Properties.Resources.Impérial;
			this.btReportScaleImperial.Click += new global::System.EventHandler(this.btReportScaleImperial_Click);
			this.btReportScaleMetric.CanCustomize = false;
			this.btReportScaleMetric.Name = "btReportScaleMetric";
			this.btReportScaleMetric.Text = global::QuoterPlan.Properties.Resources.Métrique;
			this.btReportScaleMetric.Click += new global::System.EventHandler(this.btReportScaleMetric_Click);
			this.lblReportPrecision.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblReportPrecision.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblReportPrecision.CanCustomize = false;
			this.lblReportPrecision.Name = "lblReportPrecision";
			this.lblReportPrecision.PaddingBottom = 3;
			this.lblReportPrecision.PaddingLeft = 10;
			this.lblReportPrecision.PaddingTop = 3;
			this.lblReportPrecision.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblReportPrecision, "lblReportPrecision");
			this.btReportScalePrecision64.CanCustomize = false;
			this.btReportScalePrecision64.Name = "btReportScalePrecision64";
			resources.ApplyResources(this.btReportScalePrecision64, "btReportScalePrecision64");
			this.btReportScalePrecision64.Click += new global::System.EventHandler(this.btReportScalePrecision64_Click);
			this.btReportScalePrecision32.CanCustomize = false;
			this.btReportScalePrecision32.Name = "btReportScalePrecision32";
			resources.ApplyResources(this.btReportScalePrecision32, "btReportScalePrecision32");
			this.btReportScalePrecision32.Click += new global::System.EventHandler(this.btReportScalePrecision32_Click);
			this.btReportScalePrecision16.CanCustomize = false;
			this.btReportScalePrecision16.Name = "btReportScalePrecision16";
			resources.ApplyResources(this.btReportScalePrecision16, "btReportScalePrecision16");
			this.btReportScalePrecision16.Click += new global::System.EventHandler(this.btReportScalePrecision16_Click);
			this.btReportScalePrecision8.CanCustomize = false;
			this.btReportScalePrecision8.Name = "btReportScalePrecision8";
			resources.ApplyResources(this.btReportScalePrecision8, "btReportScalePrecision8");
			this.btReportScalePrecision8.Click += new global::System.EventHandler(this.btReportScalePrecision8_Click);
			this.lblReportTheme.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblReportTheme.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblReportTheme.CanCustomize = false;
			this.lblReportTheme.Name = "lblReportTheme";
			this.lblReportTheme.PaddingBottom = 3;
			this.lblReportTheme.PaddingLeft = 10;
			this.lblReportTheme.PaddingTop = 3;
			this.lblReportTheme.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblReportTheme, "lblReportTheme");
			this.lblReportTheme.Visible = false;
			this.btReportSettings.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btReportSettings.Image = (global::System.Drawing.Image)resources.GetObject("btReportSettings.Image");
			this.btReportSettings.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btReportSettings.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btReportSettings.Name = "btReportSettings";
			resources.ApplyResources(this.btReportSettings, "btReportSettings");
			this.btReportSettings.Click += new global::System.EventHandler(this.btReportSettings_Click);
			this.ribbonPanel.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
			this.ribbonPanel.Style.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanel.StyleMouseDown.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanel.StyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPrintExport.AutoOverflowEnabled = true;
			this.ribbonBarPrintExport.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPrintExport.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPrintExport.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonBarPrintExport, "ribbonBarPrintExport");
			this.ribbonBarPrintExport.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btPrintPlan,
				this.btExportPlanToPDF
			});
			this.ribbonBarPrintExport.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarPrintExport.Name = "ribbonBarPrintExport";
			this.ribbonBarPrintExport.OverflowButtonImage = global::QuoterPlan.Properties.Resources.print;
			this.ribbonBarPrintExport.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarPrintExport.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPrintExport.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btPrintPlan.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPrintPlan.Image = global::QuoterPlan.Properties.Resources.printer_32x36;
			this.btPrintPlan.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPrintPlan.ImagePaddingHorizontal = 10;
			this.btPrintPlan.ImagePaddingVertical = 5;
			this.btPrintPlan.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPrintPlan.Name = "btPrintPlan";
			this.btPrintPlan.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblPrintPlanOptions,
				this.btPrintPlanFullSize,
				this.btPrintPlanWindow
			});
			resources.ApplyResources(this.btPrintPlan, "btPrintPlan");
			this.btPrintPlan.Click += new global::System.EventHandler(this.btPrintPlan_Click);
			this.lblPrintPlanOptions.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblPrintPlanOptions.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblPrintPlanOptions.CanCustomize = false;
			this.lblPrintPlanOptions.Name = "lblPrintPlanOptions";
			this.lblPrintPlanOptions.PaddingBottom = 3;
			this.lblPrintPlanOptions.PaddingLeft = 10;
			this.lblPrintPlanOptions.PaddingTop = 3;
			this.lblPrintPlanOptions.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			this.lblPrintPlanOptions.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.buttonItem2
			});
			resources.ApplyResources(this.lblPrintPlanOptions, "lblPrintPlanOptions");
			this.buttonItem2.CanCustomize = false;
			this.buttonItem2.Name = "buttonItem2";
			this.buttonItem2.Text = global::QuoterPlan.Properties.Resources.Impérial;
			this.btPrintPlanFullSize.AutoCheckOnClick = true;
			this.btPrintPlanFullSize.CanCustomize = false;
			this.btPrintPlanFullSize.Name = "btPrintPlanFullSize";
			this.btPrintPlanFullSize.OptionGroup = "PrintPlanOptions";
			resources.ApplyResources(this.btPrintPlanFullSize, "btPrintPlanFullSize");
			this.btPrintPlanFullSize.Click += new global::System.EventHandler(this.btPrintPlanType_Click);
			this.btPrintPlanWindow.AutoCheckOnClick = true;
			this.btPrintPlanWindow.CanCustomize = false;
			this.btPrintPlanWindow.Name = "btPrintPlanWindow";
			this.btPrintPlanWindow.OptionGroup = "PrintPlanOptions";
			resources.ApplyResources(this.btPrintPlanWindow, "btPrintPlanWindow");
			this.btPrintPlanWindow.Click += new global::System.EventHandler(this.btPrintPlanType_Click);
			this.btExportPlanToPDF.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExportPlanToPDF.Image = global::QuoterPlan.Properties.Resources.file_pdf_40x40;
			this.btExportPlanToPDF.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExportPlanToPDF.ImagePaddingVertical = 5;
			this.btExportPlanToPDF.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExportPlanToPDF.Name = "btExportPlanToPDF";
			resources.ApplyResources(this.btExportPlanToPDF, "btExportPlanToPDF");
			this.btExportPlanToPDF.Click += new global::System.EventHandler(this.btExportPlanToPDF_Click);
			this.ribbonBarImage.AutoOverflowEnabled = true;
			this.ribbonBarImage.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarImage.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarImage.OverflowButtonImage = (global::System.Drawing.Image)resources.GetObject("ribbonBarImage.OverflowButtonImage");
			this.ribbonBarImage.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarImage.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarImage.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btBrightnessContrast.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrightnessContrast.Image = (global::System.Drawing.Image)resources.GetObject("btBrightnessContrast.Image");
			this.btBrightnessContrast.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btBrightnessContrast.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btBrightnessContrast.Name = "btBrightnessContrast";
			resources.ApplyResources(this.btBrightnessContrast, "btBrightnessContrast");
			this.btBrightnessContrast.Click += new global::System.EventHandler(this.btBrightnessContrast_Click);
			this.btRotation.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btRotation.Image = (global::System.Drawing.Image)resources.GetObject("btRotation.Image");
			this.btRotation.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btRotation.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btRotation.Name = "btRotation";
			resources.ApplyResources(this.btRotation, "btRotation");
			this.btRotation.Click += new global::System.EventHandler(this.btRotation_Click);
			this.ribbonBarBrowse.AutoOverflowEnabled = true;
			this.ribbonBarBrowse.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBrowse.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarBrowse.OverflowButtonImage = (global::System.Drawing.Image)resources.GetObject("ribbonBarBrowse.OverflowButtonImage");
			this.ribbonBarBrowse.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarBrowse.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBrowse.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerBrowse1.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerBrowse1.FixedSize = new global::System.Drawing.Size(88, 0);
			this.itemContainerBrowse1.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainerBrowse1.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerBrowse1.MultiLine = true;
			this.itemContainerBrowse1.Name = "itemContainerBrowse1";
			this.itemContainerBrowse1.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblBrowseGroup,
				this.itemContainerBrowse3
			});
			this.itemContainerBrowse1.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblBrowseGroup.Name = "lblBrowseGroup";
			this.lblBrowseGroup.PaddingTop = 3;
			this.lblBrowseGroup.Stretch = true;
			resources.ApplyResources(this.lblBrowseGroup, "lblBrowseGroup");
			this.lblBrowseGroup.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblBrowseGroup.Width = 88;
			this.lblBrowseGroup.WordWrap = true;
			this.itemContainerBrowse3.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerBrowse3.FixedSize = new global::System.Drawing.Size(88, 0);
			this.itemContainerBrowse3.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainerBrowse3.Name = "itemContainerBrowse3";
			this.itemContainerBrowse3.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btBrowsePrevious,
				this.btBrowseNext
			});
			this.itemContainerBrowse3.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btBrowsePrevious.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrowsePrevious.Image = (global::System.Drawing.Image)resources.GetObject("btBrowsePrevious.Image");
			this.btBrowsePrevious.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btBrowsePrevious.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Bottom;
			this.btBrowsePrevious.Name = "btBrowsePrevious";
			this.btBrowsePrevious.Click += new global::System.EventHandler(this.btBrowsePrevious_Click);
			this.btBrowseNext.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrowseNext.Image = (global::System.Drawing.Image)resources.GetObject("btBrowseNext.Image");
			this.btBrowseNext.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btBrowseNext.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Bottom;
			this.btBrowseNext.Name = "btBrowseNext";
			this.btBrowseNext.Click += new global::System.EventHandler(this.btBrowseNext_Click);
			this.itemContainerBrowse2.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerBrowse2.FixedSize = new global::System.Drawing.Size(88, 0);
			this.itemContainerBrowse2.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainerBrowse2.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerBrowse2.MultiLine = true;
			this.itemContainerBrowse2.Name = "itemContainerBrowse2";
			this.itemContainerBrowse2.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblBrowseObjectType,
				this.itemContainerBrowse4
			});
			this.itemContainerBrowse2.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblBrowseObjectType.Name = "lblBrowseObjectType";
			this.lblBrowseObjectType.PaddingTop = 3;
			this.lblBrowseObjectType.Stretch = true;
			resources.ApplyResources(this.lblBrowseObjectType, "lblBrowseObjectType");
			this.lblBrowseObjectType.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblBrowseObjectType.Width = 88;
			this.lblBrowseObjectType.WordWrap = true;
			this.itemContainerBrowse4.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerBrowse4.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainerBrowse4.MinimumSize = new global::System.Drawing.Size(88, 0);
			this.itemContainerBrowse4.Name = "itemContainerBrowse4";
			this.itemContainerBrowse4.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btBrowseObjectTypePrevious,
				this.btBrowseObjectTypeNext
			});
			this.itemContainerBrowse4.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btBrowseObjectTypePrevious.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrowseObjectTypePrevious.Image = (global::System.Drawing.Image)resources.GetObject("btBrowseObjectTypePrevious.Image");
			this.btBrowseObjectTypePrevious.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btBrowseObjectTypePrevious.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Bottom;
			this.btBrowseObjectTypePrevious.Name = "btBrowseObjectTypePrevious";
			this.btBrowseObjectTypePrevious.Click += new global::System.EventHandler(this.btBrowseObjectTypePrevious_Click);
			this.btBrowseObjectTypeNext.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrowseObjectTypeNext.Image = (global::System.Drawing.Image)resources.GetObject("btBrowseObjectTypeNext.Image");
			this.btBrowseObjectTypeNext.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btBrowseObjectTypeNext.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Bottom;
			this.btBrowseObjectTypeNext.Name = "btBrowseObjectTypeNext";
			this.btBrowseObjectTypeNext.Click += new global::System.EventHandler(this.btBrowseObjectTypeNext_Click);
			this.ribbonBarZoom.AutoOverflowEnabled = false;
			this.ribbonBarZoom.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarZoom.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarZoom.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarZoom.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarZoom.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btZoomToSelection.BeginGroup = true;
			this.btZoomToSelection.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btZoomToSelection.Image = (global::System.Drawing.Image)resources.GetObject("btZoomToSelection.Image");
			this.btZoomToSelection.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btZoomToSelection.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btZoomToSelection.Name = "btZoomToSelection";
			resources.ApplyResources(this.btZoomToSelection, "btZoomToSelection");
			this.btZoomToSelection.Click += new global::System.EventHandler(this.btZoomToSelection_Click);
			this.btZoomToWindow.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btZoomToWindow.Image = (global::System.Drawing.Image)resources.GetObject("btZoomToWindow.Image");
			this.btZoomToWindow.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btZoomToWindow.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btZoomToWindow.Name = "btZoomToWindow";
			resources.ApplyResources(this.btZoomToWindow, "btZoomToWindow");
			this.btZoomToWindow.Click += new global::System.EventHandler(this.btZoomToWindow_Click);
			this.btZoomActualSize.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btZoomActualSize.CanCustomize = false;
			this.btZoomActualSize.Image = (global::System.Drawing.Image)resources.GetObject("btZoomActualSize.Image");
			this.btZoomActualSize.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btZoomActualSize.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btZoomActualSize.Name = "btZoomActualSize";
			this.btZoomActualSize.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btZoom75,
				this.btZoom50,
				this.btZoom25
			});
			resources.ApplyResources(this.btZoomActualSize, "btZoomActualSize");
			this.btZoomActualSize.Click += new global::System.EventHandler(this.btZoomActualSize_Click);
			this.btZoom75.CanCustomize = false;
			this.btZoom75.Image = (global::System.Drawing.Image)resources.GetObject("btZoom75.Image");
			this.btZoom75.Name = "btZoom75";
			resources.ApplyResources(this.btZoom75, "btZoom75");
			this.btZoom75.Click += new global::System.EventHandler(this.btZoom75_Click);
			this.btZoom50.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btZoom50.CanCustomize = false;
			this.btZoom50.Image = (global::System.Drawing.Image)resources.GetObject("btZoom50.Image");
			this.btZoom50.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btZoom50.ImagePaddingHorizontal = 12;
			this.btZoom50.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btZoom50.Name = "btZoom50";
			resources.ApplyResources(this.btZoom50, "btZoom50");
			this.btZoom50.Click += new global::System.EventHandler(this.btZoom50_Click);
			this.btZoom25.CanCustomize = false;
			this.btZoom25.Image = (global::System.Drawing.Image)resources.GetObject("btZoom25.Image");
			this.btZoom25.Name = "btZoom25";
			resources.ApplyResources(this.btZoom25, "btZoom25");
			this.btZoom25.Click += new global::System.EventHandler(this.btZoom25_Click);
			this.btZoomIn.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btZoomIn.Image = (global::System.Drawing.Image)resources.GetObject("btZoomIn.Image");
			this.btZoomIn.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btZoomIn.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btZoomIn.Name = "btZoomIn";
			resources.ApplyResources(this.btZoomIn, "btZoomIn");
			this.btZoomIn.Click += new global::System.EventHandler(this.btZoomIn_Click);
			this.btZoomOut.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btZoomOut.Image = (global::System.Drawing.Image)resources.GetObject("btZoomOut.Image");
			this.btZoomOut.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btZoomOut.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btZoomOut.Name = "btZoomOut";
			resources.ApplyResources(this.btZoomOut, "btZoomOut");
			this.btZoomOut.Click += new global::System.EventHandler(this.btZoomOut_Click);
			this.btBookmarks.BeginGroup = true;
			this.btBookmarks.CanCustomize = false;
			this.btBookmarks.Image = (global::System.Drawing.Image)resources.GetObject("btBookmarks.Image");
			this.btBookmarks.ImageFixedSize = new global::System.Drawing.Size(32, 32);
			this.btBookmarks.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btBookmarks.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.ribbonBarAnnotations.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarAnnotations.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarAnnotations.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarAnnotations.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarAnnotations.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btMarkZone.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btMarkZone.CanCustomize = false;
			this.btMarkZone.Image = (global::System.Drawing.Image)resources.GetObject("btMarkZone.Image");
			this.btMarkZone.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btMarkZone.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btMarkZone.Name = "btMarkZone";
			resources.ApplyResources(this.btMarkZone, "btMarkZone");
			this.btMarkZone.Click += new global::System.EventHandler(this.btMarkZone_Click);
			this.btInsertNote.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btInsertNote.CanCustomize = false;
			this.btInsertNote.Image = (global::System.Drawing.Image)resources.GetObject("btInsertNote.Image");
			this.btInsertNote.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btInsertNote.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btInsertNote.Name = "btInsertNote";
			resources.ApplyResources(this.btInsertNote, "btInsertNote");
			this.btInsertNote.Click += new global::System.EventHandler(this.btInsertNote_Click);
			this.btInsertPicture.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btInsertPicture.CanCustomize = false;
			this.btInsertPicture.Image = (global::System.Drawing.Image)resources.GetObject("btInsertPicture.Image");
			this.btInsertPicture.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btInsertPicture.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btInsertPicture.Name = "btInsertPicture";
			resources.ApplyResources(this.btInsertPicture, "btInsertPicture");
			this.btInsertPicture.Visible = false;
			this.ribbonBarTools.AutoOverflowEnabled = false;
			this.ribbonBarTools.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarTools.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarTools.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarTools.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarTools.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btToolSelection.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btToolSelection.CanCustomize = false;
			this.btToolSelection.Image = (global::System.Drawing.Image)resources.GetObject("btToolSelection.Image");
			this.btToolSelection.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btToolSelection.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btToolSelection.Name = "btToolSelection";
			resources.ApplyResources(this.btToolSelection, "btToolSelection");
			this.btToolSelection.Click += new global::System.EventHandler(this.btToolSelection_Click);
			this.btToolPan.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btToolPan.CanCustomize = false;
			this.btToolPan.Image = (global::System.Drawing.Image)resources.GetObject("btToolPan.Image");
			this.btToolPan.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btToolPan.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btToolPan.Name = "btToolPan";
			resources.ApplyResources(this.btToolPan, "btToolPan");
			this.btToolPan.Click += new global::System.EventHandler(this.btToolPan_Click);
			this.btToolArea.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btToolArea.CanCustomize = false;
			this.btToolArea.Image = (global::System.Drawing.Image)resources.GetObject("btToolArea.Image");
			this.btToolArea.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btToolArea.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btToolArea.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolArea_PopupOpen);
			this.btToolArea.Click += new global::System.EventHandler(this.btToolArea_Click);
			this.lblNoArea.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblNoArea.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblNoArea.CanCustomize = false;
			this.lblNoArea.Name = "lblNoArea";
			this.lblNoArea.PaddingBottom = 3;
			this.lblNoArea.PaddingLeft = 10;
			this.lblNoArea.PaddingTop = 3;
			this.lblNoArea.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblNoArea, "lblNoArea");
			this.lblAreaFilter.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblAreaFilter.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblAreaFilter.CanCustomize = false;
			this.lblAreaFilter.Name = "lblAreaFilter";
			this.lblAreaFilter.PaddingBottom = 3;
			this.lblAreaFilter.PaddingLeft = 10;
			this.lblAreaFilter.PaddingTop = 3;
			this.lblAreaFilter.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblAreaFilter, "lblAreaFilter");
			this.itemContainerAreaFilter.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.itemContainerAreaFilter.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.txtAreaFilter.CanCustomize = false;
			this.txtAreaFilter.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Center;
			this.txtAreaFilter.Name = "txtAreaFilter";
			this.txtAreaFilter.Stretch = true;
			this.txtAreaFilter.WatermarkColor = global::System.Drawing.SystemColors.GrayText;
			this.txtAreaFilter.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
			this.txtAreaFilter.LostFocus += new global::System.EventHandler(this.txtFilter_LostFocus);
			this.txtAreaFilter.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.txtAreaFilter.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
			this.btAreaFilterClear.AutoCollapseOnClick = false;
			this.btAreaFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			this.btAreaFilterClear.ImagePaddingHorizontal = 4;
			this.btAreaFilterClear.ImagePaddingVertical = 4;
			this.btAreaFilterClear.Name = "btAreaFilterClear";
			this.btAreaFilterClear.Click += new global::System.EventHandler(this.btFilterClear_Click);
			this.btAreaFilterClear.LostFocus += new global::System.EventHandler(this.txtFilter_LostFocus);
			this.btAreaFilterClear.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.lblAreaFilterPadding.Name = "lblAreaFilterPadding";
			this.lblAreaFilterPadding.Width = 4;
			this.lblAreaGroups.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblAreaGroups.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblAreaGroups.CanCustomize = false;
			this.lblAreaGroups.Name = "lblAreaGroups";
			this.lblAreaGroups.PaddingBottom = 3;
			this.lblAreaGroups.PaddingLeft = 10;
			this.lblAreaGroups.PaddingTop = 3;
			this.lblAreaGroups.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblAreaGroups, "lblAreaGroups");
			this.galleryAreaGroups.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryAreaGroups.CanCustomize = false;
			this.galleryAreaGroups.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryAreaGroups.EnableGalleryPopup = false;
			this.galleryAreaGroups.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryAreaGroups.Name = "galleryAreaGroups";
			resources.ApplyResources(this.galleryAreaGroups, "galleryAreaGroups");
			this.galleryAreaGroups.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblAreaTemplates.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblAreaTemplates.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblAreaTemplates.CanCustomize = false;
			this.lblAreaTemplates.Name = "lblAreaTemplates";
			this.lblAreaTemplates.PaddingBottom = 3;
			this.lblAreaTemplates.PaddingLeft = 10;
			this.lblAreaTemplates.PaddingTop = 3;
			this.lblAreaTemplates.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblAreaTemplates, "lblAreaTemplates");
			this.galleryAreaTemplates.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryAreaTemplates.CanCustomize = false;
			this.galleryAreaTemplates.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryAreaTemplates.EnableGalleryPopup = false;
			this.galleryAreaTemplates.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryAreaTemplates.Name = "galleryAreaTemplates";
			resources.ApplyResources(this.galleryAreaTemplates, "galleryAreaTemplates");
			this.galleryAreaTemplates.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btToolPerimeter.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btToolPerimeter.CanCustomize = false;
			this.btToolPerimeter.Image = (global::System.Drawing.Image)resources.GetObject("btToolPerimeter.Image");
			this.btToolPerimeter.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btToolPerimeter.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btToolPerimeter.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolPerimeter_PopupOpen);
			this.btToolPerimeter.Click += new global::System.EventHandler(this.btToolPerimeter_Click);
			this.lblNoPerimeter.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblNoPerimeter.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblNoPerimeter.CanCustomize = false;
			this.lblNoPerimeter.Name = "lblNoPerimeter";
			this.lblNoPerimeter.PaddingBottom = 3;
			this.lblNoPerimeter.PaddingLeft = 10;
			this.lblNoPerimeter.PaddingTop = 3;
			this.lblNoPerimeter.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblNoPerimeter, "lblNoPerimeter");
			this.lblPerimeterFilter.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblPerimeterFilter.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblPerimeterFilter.CanCustomize = false;
			this.lblPerimeterFilter.Name = "lblPerimeterFilter";
			this.lblPerimeterFilter.PaddingBottom = 3;
			this.lblPerimeterFilter.PaddingLeft = 10;
			this.lblPerimeterFilter.PaddingTop = 3;
			this.lblPerimeterFilter.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblPerimeterFilter, "lblPerimeterFilter");
			this.itemContainerPerimeterFilter.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerPerimeterFilter.CanCustomize = false;
			this.itemContainerPerimeterFilter.ItemSpacing = 0;
			this.itemContainerPerimeterFilter.Name = "itemContainerPerimeterFilter";
			this.itemContainerPerimeterFilter.ResizeItemsToFit = false;
			this.itemContainerPerimeterFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.txtPerimeterFilter,
				this.btPerimeterFilterClear
			});
			this.itemContainerPerimeterFilter.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.txtPerimeterFilter.CanCustomize = false;
			this.txtPerimeterFilter.Name = "txtPerimeterFilter";
			this.txtPerimeterFilter.Stretch = true;
			this.txtPerimeterFilter.WatermarkColor = global::System.Drawing.SystemColors.GrayText;
			this.txtPerimeterFilter.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
			this.txtPerimeterFilter.LostFocus += new global::System.EventHandler(this.txtFilter_LostFocus);
			this.txtPerimeterFilter.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.txtPerimeterFilter.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
			this.btPerimeterFilterClear.AutoCollapseOnClick = false;
			this.btPerimeterFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			this.btPerimeterFilterClear.ImagePaddingHorizontal = 4;
			this.btPerimeterFilterClear.ImagePaddingVertical = 4;
			this.btPerimeterFilterClear.Name = "btPerimeterFilterClear";
			this.btPerimeterFilterClear.Click += new global::System.EventHandler(this.btFilterClear_Click);
			this.btPerimeterFilterClear.LostFocus += new global::System.EventHandler(this.txtFilter_LostFocus);
			this.btPerimeterFilterClear.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.lblPerimeterGroups.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblPerimeterGroups.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblPerimeterGroups.CanCustomize = false;
			this.lblPerimeterGroups.Name = "lblPerimeterGroups";
			this.lblPerimeterGroups.PaddingBottom = 3;
			this.lblPerimeterGroups.PaddingLeft = 10;
			this.lblPerimeterGroups.PaddingTop = 3;
			this.lblPerimeterGroups.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblPerimeterGroups, "lblPerimeterGroups");
			this.galleryPerimeterGroups.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryPerimeterGroups.CanCustomize = false;
			this.galleryPerimeterGroups.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryPerimeterGroups.EnableGalleryPopup = false;
			this.galleryPerimeterGroups.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryPerimeterGroups.Name = "galleryPerimeterGroups";
			this.galleryPerimeterGroups.ScrollAnimation = false;
			resources.ApplyResources(this.galleryPerimeterGroups, "galleryPerimeterGroups");
			this.galleryPerimeterGroups.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblPerimeterTemplates.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblPerimeterTemplates.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblPerimeterTemplates.CanCustomize = false;
			this.lblPerimeterTemplates.Name = "lblPerimeterTemplates";
			this.lblPerimeterTemplates.PaddingBottom = 3;
			this.lblPerimeterTemplates.PaddingLeft = 10;
			this.lblPerimeterTemplates.PaddingTop = 3;
			this.lblPerimeterTemplates.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblPerimeterTemplates, "lblPerimeterTemplates");
			this.galleryPerimeterTemplates.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryPerimeterTemplates.CanCustomize = false;
			this.galleryPerimeterTemplates.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryPerimeterTemplates.EnableGalleryPopup = false;
			this.galleryPerimeterTemplates.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryPerimeterTemplates.Name = "galleryPerimeterTemplates";
			resources.ApplyResources(this.galleryPerimeterTemplates, "galleryPerimeterTemplates");
			this.galleryPerimeterTemplates.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btToolRuler.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btToolRuler.CanCustomize = false;
			this.btToolRuler.Image = (global::System.Drawing.Image)resources.GetObject("btToolRuler.Image");
			this.btToolRuler.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btToolRuler.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btToolRuler.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolRuler_PopupOpen);
			this.btToolRuler.Click += new global::System.EventHandler(this.btToolRuler_Click);
			this.lblNoDistance.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblNoDistance.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblNoDistance.CanCustomize = false;
			this.lblNoDistance.Name = "lblNoDistance";
			this.lblNoDistance.PaddingBottom = 3;
			this.lblNoDistance.PaddingLeft = 10;
			this.lblNoDistance.PaddingTop = 3;
			this.lblNoDistance.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblNoDistance, "lblNoDistance");
			this.lblDistanceFilter.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblDistanceFilter.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblDistanceFilter.CanCustomize = false;
			this.lblDistanceFilter.Name = "lblDistanceFilter";
			this.lblDistanceFilter.PaddingBottom = 3;
			this.lblDistanceFilter.PaddingLeft = 10;
			this.lblDistanceFilter.PaddingTop = 3;
			this.lblDistanceFilter.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblDistanceFilter, "lblDistanceFilter");
			this.itemContainerDistanceFilter.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerDistanceFilter.CanCustomize = false;
			this.itemContainerDistanceFilter.ItemSpacing = 0;
			this.itemContainerDistanceFilter.Name = "itemContainerDistanceFilter";
			this.itemContainerDistanceFilter.ResizeItemsToFit = false;
			this.itemContainerDistanceFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.txtDistanceFilter,
				this.btDistanceFilterClear
			});
			this.itemContainerDistanceFilter.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.txtDistanceFilter.CanCustomize = false;
			this.txtDistanceFilter.Name = "txtDistanceFilter";
			this.txtDistanceFilter.Stretch = true;
			this.txtDistanceFilter.WatermarkColor = global::System.Drawing.SystemColors.GrayText;
			this.txtDistanceFilter.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
			this.txtDistanceFilter.LostFocus += new global::System.EventHandler(this.txtFilter_LostFocus);
			this.txtDistanceFilter.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.txtDistanceFilter.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
			this.btDistanceFilterClear.AutoCollapseOnClick = false;
			this.btDistanceFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			this.btDistanceFilterClear.ImagePaddingHorizontal = 4;
			this.btDistanceFilterClear.ImagePaddingVertical = 4;
			this.btDistanceFilterClear.Name = "btDistanceFilterClear";
			this.btDistanceFilterClear.Click += new global::System.EventHandler(this.btFilterClear_Click);
			this.btDistanceFilterClear.LostFocus += new global::System.EventHandler(this.txtFilter_LostFocus);
			this.btDistanceFilterClear.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.lblDistanceGroups.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblDistanceGroups.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblDistanceGroups.CanCustomize = false;
			this.lblDistanceGroups.Name = "lblDistanceGroups";
			this.lblDistanceGroups.PaddingBottom = 3;
			this.lblDistanceGroups.PaddingLeft = 10;
			this.lblDistanceGroups.PaddingTop = 3;
			this.lblDistanceGroups.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblDistanceGroups, "lblDistanceGroups");
			this.galleryDistanceGroups.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryDistanceGroups.CanCustomize = false;
			this.galleryDistanceGroups.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryDistanceGroups.EnableGalleryPopup = false;
			this.galleryDistanceGroups.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryDistanceGroups.Name = "galleryDistanceGroups";
			this.galleryDistanceGroups.ScrollAnimation = false;
			resources.ApplyResources(this.galleryDistanceGroups, "galleryDistanceGroups");
			this.galleryDistanceGroups.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblDistanceTemplates.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblDistanceTemplates.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblDistanceTemplates.CanCustomize = false;
			this.lblDistanceTemplates.Name = "lblDistanceTemplates";
			this.lblDistanceTemplates.PaddingBottom = 3;
			this.lblDistanceTemplates.PaddingLeft = 10;
			this.lblDistanceTemplates.PaddingTop = 3;
			this.lblDistanceTemplates.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblDistanceTemplates, "lblDistanceTemplates");
			this.galleryDistanceTemplates.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryDistanceTemplates.CanCustomize = false;
			this.galleryDistanceTemplates.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryDistanceTemplates.EnableGalleryPopup = false;
			this.galleryDistanceTemplates.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryDistanceTemplates.Name = "galleryDistanceTemplates";
			this.galleryDistanceTemplates.ScrollAnimation = false;
			resources.ApplyResources(this.galleryDistanceTemplates, "galleryDistanceTemplates");
			this.galleryDistanceTemplates.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btToolCounter.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btToolCounter.CanCustomize = false;
			this.btToolCounter.Image = (global::System.Drawing.Image)resources.GetObject("btToolCounter.Image");
			this.btToolCounter.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btToolCounter.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btToolCounter.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btToolCounter_PopupOpen);
			this.btToolCounter.Click += new global::System.EventHandler(this.btToolCounter_Click);
			this.lblNoCounter.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblNoCounter.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblNoCounter.CanCustomize = false;
			this.lblNoCounter.Name = "lblNoCounter";
			this.lblNoCounter.PaddingBottom = 3;
			this.lblNoCounter.PaddingLeft = 10;
			this.lblNoCounter.PaddingTop = 3;
			this.lblNoCounter.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblNoCounter, "lblNoCounter");
			this.lblCounterFilter.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblCounterFilter.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblCounterFilter.CanCustomize = false;
			this.lblCounterFilter.Name = "lblCounterFilter";
			this.lblCounterFilter.PaddingBottom = 3;
			this.lblCounterFilter.PaddingLeft = 10;
			this.lblCounterFilter.PaddingTop = 3;
			this.lblCounterFilter.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblCounterFilter, "lblCounterFilter");
			this.itemContainerCounterFilter.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerCounterFilter.CanCustomize = false;
			this.itemContainerCounterFilter.ItemSpacing = 0;
			this.itemContainerCounterFilter.Name = "itemContainerCounterFilter";
			this.itemContainerCounterFilter.ResizeItemsToFit = false;
			this.itemContainerCounterFilter.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.txtCounterFilter,
				this.btCounterFilterClear
			});
			this.itemContainerCounterFilter.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.txtCounterFilter.CanCustomize = false;
			this.txtCounterFilter.Name = "txtCounterFilter";
			this.txtCounterFilter.Stretch = true;
			this.txtCounterFilter.WatermarkColor = global::System.Drawing.SystemColors.GrayText;
			this.txtCounterFilter.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
			this.txtCounterFilter.LostFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.txtCounterFilter.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.txtCounterFilter.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.txtFilter_MouseMove);
			this.btCounterFilterClear.AutoCollapseOnClick = false;
			this.btCounterFilterClear.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			this.btCounterFilterClear.ImagePaddingHorizontal = 4;
			this.btCounterFilterClear.ImagePaddingVertical = 4;
			this.btCounterFilterClear.Name = "btCounterFilterClear";
			this.btCounterFilterClear.Click += new global::System.EventHandler(this.btFilterClear_Click);
			this.btCounterFilterClear.LostFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.btCounterFilterClear.GotFocus += new global::System.EventHandler(this.txtFilter_GotFocus);
			this.lblCounterGroups.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblCounterGroups.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblCounterGroups.CanCustomize = false;
			this.lblCounterGroups.Name = "lblCounterGroups";
			this.lblCounterGroups.PaddingBottom = 3;
			this.lblCounterGroups.PaddingLeft = 10;
			this.lblCounterGroups.PaddingTop = 3;
			this.lblCounterGroups.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblCounterGroups, "lblCounterGroups");
			this.galleryCounterGroups.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryCounterGroups.CanCustomize = false;
			this.galleryCounterGroups.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryCounterGroups.EnableGalleryPopup = false;
			this.galleryCounterGroups.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryCounterGroups.Name = "galleryCounterGroups";
			this.galleryCounterGroups.ScrollAnimation = false;
			resources.ApplyResources(this.galleryCounterGroups, "galleryCounterGroups");
			this.galleryCounterGroups.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblCounterTemplates.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblCounterTemplates.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblCounterTemplates.CanCustomize = false;
			this.lblCounterTemplates.Name = "lblCounterTemplates";
			this.lblCounterTemplates.PaddingBottom = 3;
			this.lblCounterTemplates.PaddingLeft = 10;
			this.lblCounterTemplates.PaddingTop = 3;
			this.lblCounterTemplates.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblCounterTemplates, "lblCounterTemplates");
			this.galleryCounterTemplates.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryCounterTemplates.CanCustomize = false;
			this.galleryCounterTemplates.DefaultSize = new global::System.Drawing.Size(200, 20);
			this.galleryCounterTemplates.EnableGalleryPopup = false;
			this.galleryCounterTemplates.MinimumSize = new global::System.Drawing.Size(200, 20);
			this.galleryCounterTemplates.Name = "galleryCounterTemplates";
			this.galleryCounterTemplates.ScrollAnimation = false;
			resources.ApplyResources(this.galleryCounterTemplates, "galleryCounterTemplates");
			this.galleryCounterTemplates.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btToolAngle.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btToolAngle.CanCustomize = false;
			this.btToolAngle.Image = (global::System.Drawing.Image)resources.GetObject("btToolAngle.Image");
			this.btToolAngle.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btToolAngle.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btToolAngle.Name = "btToolAngle";
			this.btToolAngle.Text = global::QuoterPlan.Properties.Resources.Angle;
			this.btToolAngle.Click += new global::System.EventHandler(this.btToolAngle_Click);
			this.ribbonBarScale.AutoOverflowEnabled = false;
			this.ribbonBarScale.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarScale.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarScale.CanCustomize = false;
			this.ribbonBarScale.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonBarScale, "ribbonBarScale");
			this.ribbonBarScale.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btScaleSet
			});
			this.ribbonBarScale.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarScale.Name = "ribbonBarScale";
			this.ribbonBarScale.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarScale.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarScale.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btScaleSet.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btScaleSet.CanCustomize = false;
			this.btScaleSet.Image = (global::System.Drawing.Image)resources.GetObject("btScaleSet.Image");
			this.btScaleSet.ImageFixedSize = new global::System.Drawing.Size(48, 48);
			this.btScaleSet.ImagePaddingVertical = 3;
			this.btScaleSet.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btScaleSet.Click += new global::System.EventHandler(this.btScaleSet_Click);
			this.lblSystemType.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblSystemType.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblSystemType.CanCustomize = false;
			this.lblSystemType.Name = "lblSystemType";
			this.lblSystemType.PaddingBottom = 3;
			this.lblSystemType.PaddingLeft = 10;
			this.lblSystemType.PaddingTop = 3;
			this.lblSystemType.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblSystemType, "lblSystemType");
			this.btScaleImperial.CanCustomize = false;
			this.btScaleImperial.Name = "btScaleImperial";
			this.btScaleImperial.Text = global::QuoterPlan.Properties.Resources.Impérial;
			this.btScaleImperial.Click += new global::System.EventHandler(this.btScaleImperial_Click);
			this.btScaleMetric.CanCustomize = false;
			this.btScaleMetric.Name = "btScaleMetric";
			this.btScaleMetric.Text = global::QuoterPlan.Properties.Resources.Métrique;
			this.btScaleMetric.Click += new global::System.EventHandler(this.btScaleMetric_Click);
			this.lblPrecision.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblPrecision.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblPrecision.CanCustomize = false;
			this.lblPrecision.Name = "lblPrecision";
			this.lblPrecision.PaddingBottom = 4;
			this.lblPrecision.PaddingLeft = 10;
			this.lblPrecision.PaddingTop = 4;
			this.lblPrecision.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblPrecision, "lblPrecision");
			this.btScalePrecision64.CanCustomize = false;
			this.btScalePrecision64.Name = "btScalePrecision64";
			resources.ApplyResources(this.btScalePrecision64, "btScalePrecision64");
			this.btScalePrecision64.Click += new global::System.EventHandler(this.btScalePrecision64_Click);
			this.btScalePrecision32.CanCustomize = false;
			this.btScalePrecision32.Name = "btScalePrecision32";
			resources.ApplyResources(this.btScalePrecision32, "btScalePrecision32");
			this.btScalePrecision32.Click += new global::System.EventHandler(this.btScalePrecision32_Click);
			this.btScalePrecision16.CanCustomize = false;
			this.btScalePrecision16.Name = "btScalePrecision16";
			resources.ApplyResources(this.btScalePrecision16, "btScalePrecision16");
			this.btScalePrecision16.Click += new global::System.EventHandler(this.btScalePrecision16_Click);
			this.btScalePrecision8.CanCustomize = false;
			this.btScalePrecision8.Name = "btScalePrecision8";
			resources.ApplyResources(this.btScalePrecision8, "btScalePrecision8");
			this.btScalePrecision8.Click += new global::System.EventHandler(this.btScalePrecision8_Click);
			this.ribbonBarEdit.AutoOverflowEnabled = false;
			this.ribbonBarEdit.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarEdit.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarEdit.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarEdit.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarEdit.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btEditPaste.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEditPaste.Image = (global::System.Drawing.Image)resources.GetObject("btEditPaste.Image");
			this.btEditPaste.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEditPaste.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Center;
			this.btEditPaste.Name = "btEditPaste";
			resources.ApplyResources(this.btEditPaste, "btEditPaste");
			this.btEditPaste.Click += new global::System.EventHandler(this.btEditPaste_Click);
			this.itemContainerEdit.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerEdit.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainerEdit.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerEdit.Name = "itemContainerEdit";
			this.itemContainerEdit.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btEditCut,
				this.btEditCopy,
				this.btEditDelete
			});
			this.itemContainerEdit.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerEdit.VerticalItemAlignment = global::DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			this.btEditCut.Image = (global::System.Drawing.Image)resources.GetObject("btEditCut.Image");
			this.btEditCut.ImageFixedSize = new global::System.Drawing.Size(22, 22);
			this.btEditCut.ImagePaddingVertical = 1;
			this.btEditCut.Name = "btEditCut";
			resources.ApplyResources(this.btEditCut, "btEditCut");
			this.btEditCut.Click += new global::System.EventHandler(this.btEditCut_Click);
			this.btEditCopy.Image = (global::System.Drawing.Image)resources.GetObject("btEditCopy.Image");
			this.btEditCopy.ImagePaddingVertical = 1;
			this.btEditCopy.Name = "btEditCopy";
			resources.ApplyResources(this.btEditCopy, "btEditCopy");
			this.btEditCopy.Click += new global::System.EventHandler(this.btEditCopy_Click);
			this.btEditDelete.Image = (global::System.Drawing.Image)resources.GetObject("btEditDelete.Image");
			this.btEditDelete.ImagePaddingVertical = 1;
			this.btEditDelete.Name = "btEditDelete";
			resources.ApplyResources(this.btEditDelete, "btEditDelete");
			this.btEditDelete.Click += new global::System.EventHandler(this.btEditDelete_Click);
			this.itemContainerUndoRedo.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerUndoRedo.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainerUndoRedo.ItemSpacing = 3;
			this.itemContainerUndoRedo.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerUndoRedo.Name = "itemContainerUndoRedo";
			this.itemContainerUndoRedo.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btEditUndo,
				this.btEditRedo
			});
			this.itemContainerUndoRedo.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerUndoRedo.VerticalItemAlignment = global::DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			this.itemContainerUndoRedo.Visible = false;
			this.btEditUndo.Image = (global::System.Drawing.Image)resources.GetObject("btEditUndo.Image");
			this.btEditUndo.ImagePaddingVertical = 2;
			this.btEditUndo.Name = "btEditUndo";
			resources.ApplyResources(this.btEditUndo, "btEditUndo");
			this.btEditUndo.Click += new global::System.EventHandler(this.btEditUndo_Click);
			this.btEditRedo.Image = (global::System.Drawing.Image)resources.GetObject("btEditRedo.Image");
			this.btEditRedo.ImagePaddingVertical = 2;
			this.btEditRedo.Name = "btEditRedo";
			resources.ApplyResources(this.btEditRedo, "btEditRedo");
			this.btEditRedo.Click += new global::System.EventHandler(this.btEditRedo_Click);
			this.btEditSendData.AutoExpandOnClick = true;
			this.btEditSendData.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btEditSendData.Image = global::QuoterPlan.Properties.Resources.right_arrow_blue;
			this.btEditSendData.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btEditSendData.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btEditSendData.Name = "btEditSendData";
			resources.ApplyResources(this.btEditSendData, "btEditSendData");
			this.btEditSendData.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btEditSendData_PopupOpen);
			this.btEditSendData.Click += new global::System.EventHandler(this.btEditSendData_Click);
			this.ribbonBarLayout.AutoOverflowEnabled = false;
			this.ribbonBarLayout.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarLayout.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarLayout.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonBarLayout, "ribbonBarLayout");
			this.ribbonBarLayout.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.itemContainerLayouts
			});
			this.ribbonBarLayout.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarLayout.Name = "ribbonBarLayout";
			this.ribbonBarLayout.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarLayout.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarLayout.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerLayouts.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerLayouts.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerLayouts.Name = "itemContainerLayouts";
			this.itemContainerLayouts.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.opTakeoffLayout,
				this.opEstimatingLayout
			});
			this.itemContainerLayouts.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerLayouts.VerticalItemAlignment = global::DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
			this.opTakeoffLayout.CheckBoxPosition = global::DevComponents.DotNetBar.eCheckBoxPosition.Right;
			this.opTakeoffLayout.CheckBoxStyle = global::DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.opTakeoffLayout.Checked = true;
			this.opTakeoffLayout.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.opTakeoffLayout.Name = "opTakeoffLayout";
			resources.ApplyResources(this.opTakeoffLayout, "opTakeoffLayout");
			this.opEstimatingLayout.CheckBoxPosition = global::DevComponents.DotNetBar.eCheckBoxPosition.Right;
			this.opEstimatingLayout.CheckBoxStyle = global::DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.opEstimatingLayout.Name = "opEstimatingLayout";
			resources.ApplyResources(this.opEstimatingLayout, "opEstimatingLayout");
			this.ribbonPanelTemplates.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplate);
			this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplateCreate);
			this.ribbonPanelTemplates.Controls.Add(this.ribbonTemplateDatabase);
			resources.ApplyResources(this.ribbonPanelTemplates, "ribbonPanelTemplates");
			this.ribbonPanelTemplates.Name = "ribbonPanelTemplates";
			this.ribbonPanelTemplates.Style.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelTemplates.StyleMouseDown.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelTemplates.StyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplate.AutoOverflowEnabled = false;
			this.ribbonTemplate.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplate.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonTemplate.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonTemplate.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplate.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btTemplateModify.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateModify.CanCustomize = false;
			this.btTemplateModify.Image = global::QuoterPlan.Properties.Resources.properties;
			this.btTemplateModify.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateModify.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateModify.Name = "btTemplateModify";
			resources.ApplyResources(this.btTemplateModify, "btTemplateModify");
			this.btTemplateModify.Click += new global::System.EventHandler(this.btTemplateModify_Click);
			this.btTemplateDuplicate.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateDuplicate.CanCustomize = false;
			this.btTemplateDuplicate.Image = (global::System.Drawing.Image)resources.GetObject("btTemplateDuplicate.Image");
			this.btTemplateDuplicate.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateDuplicate.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateDuplicate.Name = "btTemplateDuplicate";
			resources.ApplyResources(this.btTemplateDuplicate, "btTemplateDuplicate");
			this.btTemplateDuplicate.Click += new global::System.EventHandler(this.btTemplateDuplicate_Click);
			this.btTemplateDelete.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateDelete.CanCustomize = false;
			this.btTemplateDelete.Image = (global::System.Drawing.Image)resources.GetObject("btTemplateDelete.Image");
			this.btTemplateDelete.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateDelete.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateDelete.Name = "btTemplateDelete";
			resources.ApplyResources(this.btTemplateDelete, "btTemplateDelete");
			this.btTemplateDelete.Click += new global::System.EventHandler(this.btTemplateDelete_Click);
			this.ribbonTemplateCreate.AutoOverflowEnabled = false;
			this.ribbonTemplateCreate.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplateCreate.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonTemplateCreate.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonTemplateCreate.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplateCreate.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btTemplateArea.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateArea.CanCustomize = false;
			this.btTemplateArea.Image = (global::System.Drawing.Image)resources.GetObject("btTemplateArea.Image");
			this.btTemplateArea.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateArea.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateArea.Name = "btTemplateArea";
			resources.ApplyResources(this.btTemplateArea, "btTemplateArea");
			this.btTemplateArea.Click += new global::System.EventHandler(this.btTemplateArea_Click);
			this.btTemplatePerimeter.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplatePerimeter.CanCustomize = false;
			this.btTemplatePerimeter.Image = (global::System.Drawing.Image)resources.GetObject("btTemplatePerimeter.Image");
			this.btTemplatePerimeter.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplatePerimeter.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplatePerimeter.Name = "btTemplatePerimeter";
			resources.ApplyResources(this.btTemplatePerimeter, "btTemplatePerimeter");
			this.btTemplatePerimeter.Click += new global::System.EventHandler(this.btTemplatePerimeter_Click);
			this.btTemplateLength.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateLength.CanCustomize = false;
			this.btTemplateLength.Image = (global::System.Drawing.Image)resources.GetObject("btTemplateLength.Image");
			this.btTemplateLength.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateLength.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateLength.Name = "btTemplateLength";
			resources.ApplyResources(this.btTemplateLength, "btTemplateLength");
			this.btTemplateLength.Click += new global::System.EventHandler(this.btTemplateLength_Click);
			this.btTemplateCounter.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateCounter.CanCustomize = false;
			this.btTemplateCounter.Image = (global::System.Drawing.Image)resources.GetObject("btTemplateCounter.Image");
			this.btTemplateCounter.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateCounter.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateCounter.Name = "btTemplateCounter";
			resources.ApplyResources(this.btTemplateCounter, "btTemplateCounter");
			this.btTemplateCounter.Click += new global::System.EventHandler(this.btTemplateCounter_Click);
			this.ribbonTemplateDatabase.AutoOverflowEnabled = false;
			this.ribbonTemplateDatabase.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplateDatabase.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplateDatabase.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonTemplateDatabase, "ribbonTemplateDatabase");
			this.ribbonTemplateDatabase.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btTemplateTradesPackages,
				this.btTemplateCompactDatabase
			});
			this.ribbonTemplateDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonTemplateDatabase.Name = "ribbonTemplateDatabase";
			this.ribbonTemplateDatabase.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonTemplateDatabase.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTemplateDatabase.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btTemplateTradesPackages.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateTradesPackages.Image = (global::System.Drawing.Image)resources.GetObject("btTemplateTradesPackages.Image");
			this.btTemplateTradesPackages.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateTradesPackages.ImagePaddingHorizontal = 10;
			this.btTemplateTradesPackages.ImagePaddingVertical = 5;
			this.btTemplateTradesPackages.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateTradesPackages.Name = "btTemplateTradesPackages";
			resources.ApplyResources(this.btTemplateTradesPackages, "btTemplateTradesPackages");
			this.btTemplateCompactDatabase.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btTemplateCompactDatabase.Image = (global::System.Drawing.Image)resources.GetObject("btTemplateCompactDatabase.Image");
			this.btTemplateCompactDatabase.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btTemplateCompactDatabase.ImagePaddingVertical = 5;
			this.btTemplateCompactDatabase.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btTemplateCompactDatabase.Name = "btTemplateCompactDatabase";
			resources.ApplyResources(this.btTemplateCompactDatabase, "btTemplateCompactDatabase");
			this.ribbonPanelPlans.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelPlans.Controls.Add(this.ribbonBarMultiPlans);
			this.ribbonPanelPlans.Controls.Add(this.ribbonBarPlans);
			this.ribbonPanelPlans.Controls.Add(this.ribbonBarPlansInsert);
			resources.ApplyResources(this.ribbonPanelPlans, "ribbonPanelPlans");
			this.ribbonPanelPlans.Name = "ribbonPanelPlans";
			this.ribbonPanelPlans.Style.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelPlans.StyleMouseDown.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelPlans.StyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarMultiPlans.AutoOverflowEnabled = false;
			this.ribbonBarMultiPlans.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarMultiPlans.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarMultiPlans.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonBarMultiPlans, "ribbonBarMultiPlans");
			this.ribbonBarMultiPlans.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btPlansPrint,
				this.btPlansExport
			});
			this.ribbonBarMultiPlans.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBarMultiPlans.Name = "ribbonBarMultiPlans";
			this.ribbonBarMultiPlans.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarMultiPlans.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarMultiPlans.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btPlansPrint.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlansPrint.Image = global::QuoterPlan.Properties.Resources.printer_32x36;
			this.btPlansPrint.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlansPrint.ImagePaddingHorizontal = 10;
			this.btPlansPrint.ImagePaddingVertical = 5;
			this.btPlansPrint.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlansPrint.Name = "btPlansPrint";
			resources.ApplyResources(this.btPlansPrint, "btPlansPrint");
			this.btPlansPrint.Click += new global::System.EventHandler(this.btPlansPrint_Click);
			this.btPlansExport.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlansExport.Image = global::QuoterPlan.Properties.Resources.file_pdf_40x40;
			this.btPlansExport.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlansExport.ImagePaddingVertical = 5;
			this.btPlansExport.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlansExport.Name = "btPlansExport";
			resources.ApplyResources(this.btPlansExport, "btPlansExport");
			this.btPlansExport.Click += new global::System.EventHandler(this.btPlansExport_Click);
			this.ribbonBarPlans.AutoOverflowEnabled = false;
			this.ribbonBarPlans.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPlans.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarPlans.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarPlans.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPlans.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btPlanLoad.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlanLoad.Image = (global::System.Drawing.Image)resources.GetObject("btPlanLoad.Image");
			this.btPlanLoad.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlanLoad.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlanLoad.Name = "btPlanLoad";
			resources.ApplyResources(this.btPlanLoad, "btPlanLoad");
			this.btPlanLoad.Click += new global::System.EventHandler(this.btPlanLoad_Click);
			this.btPlanProperties.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlanProperties.Image = (global::System.Drawing.Image)resources.GetObject("btPlanProperties.Image");
			this.btPlanProperties.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlanProperties.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlanProperties.Name = "btPlanProperties";
			resources.ApplyResources(this.btPlanProperties, "btPlanProperties");
			this.btPlanProperties.Click += new global::System.EventHandler(this.btPlanProperties_Click);
			this.btPlanRemove.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlanRemove.Image = (global::System.Drawing.Image)resources.GetObject("btPlanRemove.Image");
			this.btPlanRemove.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlanRemove.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlanRemove.Name = "btPlanRemove";
			resources.ApplyResources(this.btPlanRemove, "btPlanRemove");
			this.btPlanRemove.Click += new global::System.EventHandler(this.btPlanRemove_Click);
			this.btPlanExport.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlanExport.Image = (global::System.Drawing.Image)resources.GetObject("btPlanExport.Image");
			this.btPlanExport.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlanExport.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlanExport.Name = "btPlanExport";
			resources.ApplyResources(this.btPlanExport, "btPlanExport");
			this.btPlanExport.Visible = false;
			this.btPlanExport.Click += new global::System.EventHandler(this.btPlanExport_Click);
			this.btPlanDuplicate.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlanDuplicate.Image = (global::System.Drawing.Image)resources.GetObject("btPlanDuplicate.Image");
			this.btPlanDuplicate.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlanDuplicate.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlanDuplicate.Name = "btPlanDuplicate";
			resources.ApplyResources(this.btPlanDuplicate, "btPlanDuplicate");
			this.btPlanDuplicate.Click += new global::System.EventHandler(this.btPlanDuplicate_Click);
			this.ribbonBarPlansInsert.AutoOverflowEnabled = false;
			this.ribbonBarPlansInsert.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPlansInsert.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonBarPlansInsert.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarPlansInsert.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPlansInsert.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btPlanInsertFromPDF.AutoCollapseOnClick = false;
			this.btPlanInsertFromPDF.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlanInsertFromPDF.CanCustomize = false;
			this.btPlanInsertFromPDF.Image = (global::System.Drawing.Image)resources.GetObject("btPlanInsertFromPDF.Image");
			this.btPlanInsertFromPDF.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlanInsertFromPDF.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
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
			this.btPlanInsertFromPDF.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btPlanInsertFromPDF_PopupOpen);
			this.btPlanInsertFromPDF.Click += new global::System.EventHandler(this.btPlanInsertFromPDF_Click);
			this.iblImportDPI.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.iblImportDPI.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.iblImportDPI.CanCustomize = false;
			this.iblImportDPI.Name = "iblImportDPI";
			this.iblImportDPI.PaddingBottom = 3;
			this.iblImportDPI.PaddingLeft = 10;
			this.iblImportDPI.PaddingTop = 3;
			this.iblImportDPI.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			this.iblImportDPI.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.labelItem2
			});
			resources.ApplyResources(this.iblImportDPI, "iblImportDPI");
			this.labelItem2.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.labelItem2.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.labelItem2.CanCustomize = false;
			this.labelItem2.Name = "labelItem2";
			this.labelItem2.PaddingBottom = 3;
			this.labelItem2.PaddingLeft = 10;
			this.labelItem2.PaddingTop = 3;
			this.labelItem2.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.labelItem2, "labelItem2");
			this.op172Dpi.CanCustomize = false;
			this.op172Dpi.CheckBoxStyle = global::DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.op172Dpi.Name = "op172Dpi";
			resources.ApplyResources(this.op172Dpi, "op172Dpi");
			this.op172Dpi.Click += new global::System.EventHandler(this.op172Dpi_Click);
			this.op300Dpi.CanCustomize = false;
			this.op300Dpi.CheckBoxStyle = global::DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.op300Dpi.Name = "op300Dpi";
			resources.ApplyResources(this.op300Dpi, "op300Dpi");
			this.op300Dpi.Click += new global::System.EventHandler(this.op300Dpi_Click);
			this.opOtherDpi.AutoCollapseOnClick = false;
			this.opOtherDpi.CanCustomize = false;
			this.opOtherDpi.CheckBoxStyle = global::DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.opOtherDpi.Name = "opOtherDpi";
			resources.ApplyResources(this.opOtherDpi, "opOtherDpi");
			this.opOtherDpi.Click += new global::System.EventHandler(this.opOtherDpi_Click);
			this.sliderDpi.AutoCollapseOnClick = false;
			this.sliderDpi.CanCustomize = false;
			this.sliderDpi.LabelPosition = global::DevComponents.DotNetBar.eSliderLabelPosition.Top;
			this.sliderDpi.LabelWidth = 150;
			this.sliderDpi.Maximum = 300;
			this.sliderDpi.Minimum = 150;
			this.sliderDpi.Name = "sliderDpi";
			resources.ApplyResources(this.sliderDpi, "sliderDpi");
			this.sliderDpi.TextColor = global::System.Drawing.Color.Black;
			this.sliderDpi.TrackMarker = false;
			this.sliderDpi.Value = 150;
			this.sliderDpi.Width = 150;
			this.sliderDpi.ValueChanged += new global::System.EventHandler(this.sliderDpi_ValueChanged);
			this.itemContainerDpi.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerDpi.FixedSize = new global::System.Drawing.Size(0, 32);
			this.itemContainerDpi.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.itemContainerDpi.Name = "itemContainerDpi";
			this.itemContainerDpi.ResizeItemsToFit = false;
			this.itemContainerDpi.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblDpi1,
				this.labelDpiPadding1,
				this.lblDpi2
			});
			this.itemContainerDpi.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblDpi1.CanCustomize = false;
			this.lblDpi1.Name = "lblDpi1";
			resources.ApplyResources(this.lblDpi1, "lblDpi1");
			this.lblDpi1.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblDpi1.WordWrap = true;
			this.labelDpiPadding1.CanCustomize = false;
			this.labelDpiPadding1.Name = "labelDpiPadding1";
			this.labelDpiPadding1.Stretch = true;
			this.labelDpiPadding1.Width = 120;
			this.lblDpi2.CanCustomize = false;
			this.lblDpi2.Name = "lblDpi2";
			resources.ApplyResources(this.lblDpi2, "lblDpi2");
			this.lblDpi2.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblDpi2.WordWrap = true;
			this.iblImportColorManagement.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.iblImportColorManagement.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.iblImportColorManagement.CanCustomize = false;
			this.iblImportColorManagement.Name = "iblImportColorManagement";
			this.iblImportColorManagement.PaddingBottom = 3;
			this.iblImportColorManagement.PaddingLeft = 10;
			this.iblImportColorManagement.PaddingTop = 3;
			this.iblImportColorManagement.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			this.iblImportColorManagement.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.labelItem4
			});
			resources.ApplyResources(this.iblImportColorManagement, "iblImportColorManagement");
			this.labelItem4.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.labelItem4.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.labelItem4.CanCustomize = false;
			this.labelItem4.Name = "labelItem4";
			this.labelItem4.PaddingBottom = 3;
			this.labelItem4.PaddingLeft = 10;
			this.labelItem4.PaddingTop = 3;
			this.labelItem4.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.labelItem4, "labelItem4");
			this.opConvertToColor.CanCustomize = false;
			resources.ApplyResources(this.opConvertToColor, "opConvertToColor");
			this.opConvertToColor.Name = "opConvertToColor";
			this.opConvertToColor.Click += new global::System.EventHandler(this.opConvertToColor_Click);
			this.btPlanInsertFromImage.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlanInsertFromImage.Image = (global::System.Drawing.Image)resources.GetObject("btPlanInsertFromImage.Image");
			this.btPlanInsertFromImage.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlanInsertFromImage.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlanInsertFromImage.Name = "btPlanInsertFromImage";
			resources.ApplyResources(this.btPlanInsertFromImage, "btPlanInsertFromImage");
			this.btPlanInsertFromImage.Click += new global::System.EventHandler(this.btPlanInsertFromImage_Click);
			this.ribbonPanelExtensions.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonPanelExtensions.Controls.Add(this.ribbonExtension);
			this.ribbonPanelExtensions.Controls.Add(this.ribbonExtensionCreate);
			this.ribbonPanelExtensions.Controls.Add(this.ribbonExtensionDatabase);
			resources.ApplyResources(this.ribbonPanelExtensions, "ribbonPanelExtensions");
			this.ribbonPanelExtensions.Name = "ribbonPanelExtensions";
			this.ribbonPanelExtensions.Style.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelExtensions.StyleMouseDown.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonPanelExtensions.StyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtension.AutoOverflowEnabled = false;
			this.ribbonExtension.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtension.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonExtension.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonExtension.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtension.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btExtensionModify.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionModify.CanCustomize = false;
			this.btExtensionModify.Image = global::QuoterPlan.Properties.Resources.properties;
			this.btExtensionModify.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionModify.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionModify.Name = "btExtensionModify";
			resources.ApplyResources(this.btExtensionModify, "btExtensionModify");
			this.btExtensionDuplicate.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionDuplicate.CanCustomize = false;
			this.btExtensionDuplicate.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionDuplicate.Image");
			this.btExtensionDuplicate.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionDuplicate.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionDuplicate.Name = "btExtensionDuplicate";
			resources.ApplyResources(this.btExtensionDuplicate, "btExtensionDuplicate");
			this.btExtensionDelete.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionDelete.CanCustomize = false;
			this.btExtensionDelete.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionDelete.Image");
			this.btExtensionDelete.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionDelete.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionDelete.Name = "btExtensionDelete";
			resources.ApplyResources(this.btExtensionDelete, "btExtensionDelete");
			this.ribbonExtensionCreate.AutoOverflowEnabled = false;
			this.ribbonExtensionCreate.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtensionCreate.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.ribbonExtensionCreate.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonExtensionCreate.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtensionCreate.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btExtensionArea.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionArea.CanCustomize = false;
			this.btExtensionArea.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionArea.Image");
			this.btExtensionArea.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionArea.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionArea.Name = "btExtensionArea";
			resources.ApplyResources(this.btExtensionArea, "btExtensionArea");
			this.btExtensionPerimeter.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionPerimeter.CanCustomize = false;
			this.btExtensionPerimeter.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionPerimeter.Image");
			this.btExtensionPerimeter.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionPerimeter.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionPerimeter.Name = "btExtensionPerimeter";
			resources.ApplyResources(this.btExtensionPerimeter, "btExtensionPerimeter");
			this.btExtensionRuler.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionRuler.CanCustomize = false;
			this.btExtensionRuler.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionRuler.Image");
			this.btExtensionRuler.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionRuler.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionRuler.Name = "btExtensionRuler";
			resources.ApplyResources(this.btExtensionRuler, "btExtensionRuler");
			this.btExtensionCounter.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionCounter.CanCustomize = false;
			this.btExtensionCounter.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionCounter.Image");
			this.btExtensionCounter.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionCounter.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionCounter.Name = "btExtensionCounter";
			resources.ApplyResources(this.btExtensionCounter, "btExtensionCounter");
			this.ribbonExtensionDatabase.AutoOverflowEnabled = false;
			this.ribbonExtensionDatabase.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtensionDatabase.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtensionDatabase.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonExtensionDatabase, "ribbonExtensionDatabase");
			this.ribbonExtensionDatabase.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btExtensionTradesPackages,
				this.btExtensionCompactDatabase
			});
			this.ribbonExtensionDatabase.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonExtensionDatabase.Name = "ribbonExtensionDatabase";
			this.ribbonExtensionDatabase.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonExtensionDatabase.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonExtensionDatabase.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btExtensionTradesPackages.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionTradesPackages.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionTradesPackages.Image");
			this.btExtensionTradesPackages.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionTradesPackages.ImagePaddingHorizontal = 10;
			this.btExtensionTradesPackages.ImagePaddingVertical = 5;
			this.btExtensionTradesPackages.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionTradesPackages.Name = "btExtensionTradesPackages";
			resources.ApplyResources(this.btExtensionTradesPackages, "btExtensionTradesPackages");
			this.btExtensionCompactDatabase.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExtensionCompactDatabase.Image = (global::System.Drawing.Image)resources.GetObject("btExtensionCompactDatabase.Image");
			this.btExtensionCompactDatabase.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btExtensionCompactDatabase.ImagePaddingVertical = 5;
			this.btExtensionCompactDatabase.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btExtensionCompactDatabase.Name = "btExtensionCompactDatabase";
			resources.ApplyResources(this.btExtensionCompactDatabase, "btExtensionCompactDatabase");
			resources.ApplyResources(this.contextMenuBar1, "contextMenuBar1");
			this.contextMenuBar1.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.bEditPopup
			});
			this.contextMenuBar1.Name = "contextMenuBar1";
			this.contextMenuBar1.Stretch = true;
			this.contextMenuBar1.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.contextMenuBar1.TabStop = false;
			this.bEditPopup.AutoExpandOnClick = true;
			this.bEditPopup.GlobalName = "bEditPopup";
			this.bEditPopup.Name = "bEditPopup";
			this.bEditPopup.PopupAnimation = global::DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
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
			this.bAutoAdjustToZone.Click += new global::System.EventHandler(this.btAutoAdjustToZone_Click);
			resources.ApplyResources(this.bEditNote, "bEditNote");
			this.bEditNote.Image = global::QuoterPlan.Properties.Resources.note_small;
			this.bEditNote.Name = "bEditNote";
			this.bEditNote.Click += new global::System.EventHandler(this.btEditNote_Click);
			resources.ApplyResources(this.bPointInsert, "bPointInsert");
			this.bPointInsert.Name = "bPointInsert";
			this.bPointInsert.Click += new global::System.EventHandler(this.btPointInsert_Click);
			resources.ApplyResources(this.bPointRemove, "bPointRemove");
			this.bPointRemove.Name = "bPointRemove";
			this.bPointRemove.Click += new global::System.EventHandler(this.btPointRemove_Click);
			resources.ApplyResources(this.bSetHeight, "bSetHeight");
			this.bSetHeight.Name = "bSetHeight";
			this.bSetHeight.Click += new global::System.EventHandler(this.bOpeningHeight_Click);
			resources.ApplyResources(this.bGroupAddObject, "bGroupAddObject");
			this.bGroupAddObject.BeginGroup = true;
			this.bGroupAddObject.Name = "bGroupAddObject";
			this.bGroupAddObject.Click += new global::System.EventHandler(this.btGroupAddObject_Click);
			resources.ApplyResources(this.bDeductionCreate, "bDeductionCreate");
			this.bDeductionCreate.Image = global::QuoterPlan.Properties.Resources.deduction_small;
			this.bDeductionCreate.Name = "bDeductionCreate";
			this.bDeductionCreate.Click += new global::System.EventHandler(this.btDeductionCreate_Click);
			resources.ApplyResources(this.bDeductionsEdit, "bDeductionsEdit");
			this.bDeductionsEdit.Name = "bDeductionsEdit";
			this.bDeductionsEdit.Click += new global::System.EventHandler(this.btDeductionsEdit_Click);
			resources.ApplyResources(this.bPerimeterCreateFromArea, "bPerimeterCreateFromArea");
			this.bPerimeterCreateFromArea.Image = global::QuoterPlan.Properties.Resources.perimeter_small;
			this.bPerimeterCreateFromArea.Name = "bPerimeterCreateFromArea";
			this.bPerimeterCreateFromArea.Shortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlP);
			this.bPerimeterCreateFromArea.Click += new global::System.EventHandler(this.btPerimeterCreateFromArea_Click);
			resources.ApplyResources(this.bOpeningCreateFromPosition, "bOpeningCreateFromPosition");
			this.bOpeningCreateFromPosition.Name = "bOpeningCreateFromPosition";
			this.bOpeningCreateFromPosition.Click += new global::System.EventHandler(this.btOpeningCreateFromPosition_Click);
			resources.ApplyResources(this.bOpeningDuplicate, "bOpeningDuplicate");
			this.bOpeningDuplicate.Name = "bOpeningDuplicate";
			this.bOpeningDuplicate.Click += new global::System.EventHandler(this.btOpeningDuplicate_Click);
			this.bOpeningCreateFromSegment.Name = "bOpeningCreateFromSegment";
			resources.ApplyResources(this.bOpeningCreateFromSegment, "bOpeningCreateFromSegment");
			this.bOpeningCreateFromSegment.Click += new global::System.EventHandler(this.btOpeningCreateFromSegment_Click);
			this.bOpeningDelete.Name = "bOpeningDelete";
			resources.ApplyResources(this.bOpeningDelete, "bOpeningDelete");
			this.bOpeningDelete.Click += new global::System.EventHandler(this.btOpeningDelete_Click);
			resources.ApplyResources(this.bDropInsert, "bDropInsert");
			this.bDropInsert.Name = "bDropInsert";
			this.bDropInsert.Click += new global::System.EventHandler(this.btDropInsert_Click);
			this.bDropRemove.Name = "bDropRemove";
			resources.ApplyResources(this.bDropRemove, "bDropRemove");
			this.bDropRemove.Click += new global::System.EventHandler(this.btDropRemove_Click);
			this.bPerimeterOpen.Name = "bPerimeterOpen";
			resources.ApplyResources(this.bPerimeterOpen, "bPerimeterOpen");
			this.bPerimeterOpen.Click += new global::System.EventHandler(this.btPerimeterOpen_Click);
			this.bPerimeterClose.Name = "bPerimeterClose";
			resources.ApplyResources(this.bPerimeterClose, "bPerimeterClose");
			this.bPerimeterClose.Click += new global::System.EventHandler(this.btPerimeterClose_Click);
			this.bAngleDegreeType.Name = "bAngleDegreeType";
			resources.ApplyResources(this.bAngleDegreeType, "bAngleDegreeType");
			this.bAngleDegreeType.Click += new global::System.EventHandler(this.btAngleDegreeType_Click);
			this.bAngleSlopeType.Name = "bAngleSlopeType";
			resources.ApplyResources(this.bAngleSlopeType, "bAngleSlopeType");
			this.bAngleSlopeType.Click += new global::System.EventHandler(this.btAngleSlopeType_Click);
			resources.ApplyResources(this.bDeductionDuplicate, "bDeductionDuplicate");
			this.bDeductionDuplicate.BeginGroup = true;
			this.bDeductionDuplicate.Name = "bDeductionDuplicate";
			this.bDeductionDuplicate.Click += new global::System.EventHandler(this.btDeductionDuplicate_Click);
			resources.ApplyResources(this.bCut, "bCut");
			this.bCut.BeginGroup = true;
			this.bCut.GlobalName = "bCut";
			this.bCut.Image = global::QuoterPlan.Properties.Resources.cut_16x16;
			this.bCut.ImageIndex = 5;
			this.bCut.Name = "bCut";
			this.bCut.PopupAnimation = global::DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
			this.bCut.Click += new global::System.EventHandler(this.btEditCut_Click);
			resources.ApplyResources(this.bCopy, "bCopy");
			this.bCopy.GlobalName = "bCopy";
			this.bCopy.Image = global::QuoterPlan.Properties.Resources.copy_16x16;
			this.bCopy.ImageIndex = 4;
			this.bCopy.Name = "bCopy";
			this.bCopy.PopupAnimation = global::DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
			this.bCopy.Click += new global::System.EventHandler(this.btEditCopy_Click);
			resources.ApplyResources(this.bPaste, "bPaste");
			this.bPaste.GlobalName = "bPaste";
			this.bPaste.Image = global::QuoterPlan.Properties.Resources.paste_16x16;
			this.bPaste.ImageIndex = 12;
			this.bPaste.Name = "bPaste";
			this.bPaste.PopupAnimation = global::DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
			this.bPaste.Click += new global::System.EventHandler(this.btEditPaste_Click);
			resources.ApplyResources(this.bDelete, "bDelete");
			this.bDelete.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			this.bDelete.Name = "bDelete";
			this.bDelete.Click += new global::System.EventHandler(this.btEditDelete_Click);
			resources.ApplyResources(this.bToggleMeasures, "bToggleMeasures");
			this.bToggleMeasures.BeginGroup = true;
			this.bToggleMeasures.Name = "bToggleMeasures";
			this.bToggleMeasures.Click += new global::System.EventHandler(this.btToggleMeasures_Click);
			resources.ApplyResources(this.bZoomToObject, "bZoomToObject");
			this.bZoomToObject.BeginGroup = true;
			this.bZoomToObject.Image = global::QuoterPlan.Properties.Resources.zoom_16x16;
			this.bZoomToObject.Name = "bZoomToObject";
			this.bZoomToObject.Click += new global::System.EventHandler(this.btZoomToObject_Click);
			resources.ApplyResources(this.bZoomToGroup, "bZoomToGroup");
			this.bZoomToGroup.Image = global::QuoterPlan.Properties.Resources.selection_16x16_alt;
			this.bZoomToGroup.Name = "bZoomToGroup";
			this.bZoomToGroup.Click += new global::System.EventHandler(this.btZoomToGroup_Click);
			resources.ApplyResources(this.bBringToFront, "bBringToFront");
			this.bBringToFront.BeginGroup = true;
			this.bBringToFront.Image = global::QuoterPlan.Properties.Resources.bring_to_front_16x16;
			this.bBringToFront.Name = "bBringToFront";
			this.bBringToFront.Click += new global::System.EventHandler(this.btEditBringToFront_Click);
			resources.ApplyResources(this.bSendToBack, "bSendToBack");
			this.bSendToBack.Image = global::QuoterPlan.Properties.Resources.send_to_back_16x16;
			this.bSendToBack.Name = "bSendToBack";
			this.bSendToBack.Click += new global::System.EventHandler(this.btEditSendToBack_Click);
			resources.ApplyResources(this.bSelectGroup, "bSelectGroup");
			this.bSelectGroup.BeginGroup = true;
			this.bSelectGroup.Name = "bSelectGroup";
			this.bSelectGroup.Click += new global::System.EventHandler(this.btEditSelectGroup_Click);
			this.bSelectThisGroup.Name = "bSelectThisGroup";
			this.bSelectThisGroup.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.bSelectThisGroup1
			});
			resources.ApplyResources(this.bSelectThisGroup, "bSelectThisGroup");
			this.bSelectThisGroup1.Name = "bSelectThisGroup1";
			resources.ApplyResources(this.bSelectThisGroup1, "bSelectThisGroup1");
			this.bSelectObjectType.Name = "bSelectObjectType";
			this.bSelectObjectType.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.bSelectObjectType1
			});
			resources.ApplyResources(this.bSelectObjectType, "bSelectObjectType");
			this.bSelectObjectType1.Name = "bSelectObjectType1";
			resources.ApplyResources(this.bSelectObjectType1, "bSelectObjectType1");
			resources.ApplyResources(this.bSelectAll, "bSelectAll");
			this.bSelectAll.GlobalName = "bSelectAll";
			this.bSelectAll.Name = "bSelectAll";
			this.bSelectAll.PopupAnimation = global::DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
			this.bSelectAll.Click += new global::System.EventHandler(this.btEditSelectAll_Click);
			resources.ApplyResources(this.bUnselectAll, "bUnselectAll");
			this.bUnselectAll.Name = "bUnselectAll";
			this.bUnselectAll.Click += new global::System.EventHandler(this.btUnselectAll_Click);
			this.bLayerMoveTo.BeginGroup = true;
			this.bLayerMoveTo.Name = "bLayerMoveTo";
			this.bLayerMoveTo.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.bLayerMoveTo1
			});
			resources.ApplyResources(this.bLayerMoveTo, "bLayerMoveTo");
			this.bLayerMoveTo.Click += new global::System.EventHandler(this.btLayerMoveTo_Click);
			this.bLayerMoveTo1.Name = "bLayerMoveTo1";
			resources.ApplyResources(this.bLayerMoveTo1, "bLayerMoveTo1");
			this.bGroupMoveTo.Name = "bGroupMoveTo";
			this.bGroupMoveTo.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.bGroupMoveTo1
			});
			resources.ApplyResources(this.bGroupMoveTo, "bGroupMoveTo");
			this.bGroupMoveTo.Click += new global::System.EventHandler(this.btGroupMoveTo_Click);
			this.bGroupMoveTo1.Name = "bGroupMoveTo1";
			resources.ApplyResources(this.bGroupMoveTo1, "bGroupMoveTo1");
			resources.ApplyResources(this.bGroupMoveToNew, "bGroupMoveToNew");
			this.bGroupMoveToNew.Name = "bGroupMoveToNew";
			this.bGroupMoveToNew.Click += new global::System.EventHandler(this.btGroupMoveToNew_Click);
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
			this.ribbonTabItemDBManagement.Style.BackColor = global::System.Drawing.Color.FromArgb(174, 109, 148);
			this.ribbonTabItemDBManagement.Style.BackColor2 = global::System.Drawing.Color.FromArgb(144, 72, 123);
			this.ribbonTabItemDBManagement.Style.BackColorGradientAngle = 90;
			this.ribbonTabItemDBManagement.Style.BorderBottom = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.ribbonTabItemDBManagement.Style.BorderBottomWidth = 1;
			this.ribbonTabItemDBManagement.Style.BorderColor = global::System.Drawing.Color.FromArgb(154, 58, 59);
			this.ribbonTabItemDBManagement.Style.BorderLeft = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.ribbonTabItemDBManagement.Style.BorderLeftWidth = 1;
			this.ribbonTabItemDBManagement.Style.BorderRight = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.ribbonTabItemDBManagement.Style.BorderRightWidth = 1;
			this.ribbonTabItemDBManagement.Style.BorderTop = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.ribbonTabItemDBManagement.Style.BorderTopWidth = 1;
			this.ribbonTabItemDBManagement.Style.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonTabItemDBManagement.Style.TextAlignment = global::DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.ribbonTabItemDBManagement.Style.TextColor = global::System.Drawing.Color.White;
			this.ribbonTabItemDBManagement.Style.TextLineAlignment = global::DevComponents.DotNetBar.eStyleTextAlignment.Near;
			this.ribbonTabItemDBManagement.Style.TextShadowColor = global::System.Drawing.Color.Black;
			this.ribbonTabItemDBManagement.Style.TextShadowOffset = new global::System.Drawing.Point(1, 1);
			this.ribbonTabTemplates.Group = this.ribbonTabItemDBManagement;
			this.ribbonTabTemplates.Name = "ribbonTabTemplates";
			this.ribbonTabTemplates.Panel = this.ribbonPanelTemplates;
			resources.ApplyResources(this.ribbonTabTemplates, "ribbonTabTemplates");
			this.ribbonTabExtensions.Group = this.ribbonTabItemDBManagement;
			this.ribbonTabExtensions.Name = "ribbonTabExtensions";
			this.ribbonTabExtensions.Panel = this.ribbonPanelExtensions;
			resources.ApplyResources(this.ribbonTabExtensions, "ribbonTabExtensions");
			this.ribbonTabExtensions.Visible = false;
			this.lblTrialMessage.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
			this.lblTrialMessage.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Far;
			this.lblTrialMessage.Name = "lblTrialMessage";
			this.lblTrialMessage.Click += new global::System.EventHandler(this.btLicenseActivate_Click);
			this.btLicensing.AutoExpandOnClick = true;
			this.btLicensing.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btLicensing.Image = global::QuoterPlan.Properties.Resources.activate_16x16;
			this.btLicensing.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Far;
			this.btLicensing.Name = "btLicensing";
			this.btLicensing.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btLicenseBuy,
				this.btLicenseActivate
			});
			resources.ApplyResources(this.btLicensing, "btLicensing");
			this.btLicensing.Click += new global::System.EventHandler(this.btLicensing_Click);
			this.btLicenseBuy.Name = "btLicenseBuy";
			resources.ApplyResources(this.btLicenseBuy, "btLicenseBuy");
			this.btLicenseBuy.Click += new global::System.EventHandler(this.btLicenseBuy_Click);
			this.btLicenseActivate.Name = "btLicenseActivate";
			resources.ApplyResources(this.btLicenseActivate, "btLicenseActivate");
			this.btLicenseActivate.Click += new global::System.EventHandler(this.btLicenseActivate_Click);
			this.btSettings.AutoExpandOnClick = true;
			this.btSettings.Image = global::QuoterPlan.Properties.Resources.settings_16x16;
			this.btSettings.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Far;
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
			this.lblLanguage.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblLanguage.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblLanguage.CanCustomize = false;
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.PaddingBottom = 3;
			this.lblLanguage.PaddingLeft = 10;
			this.lblLanguage.PaddingTop = 3;
			this.lblLanguage.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblLanguage, "lblLanguage");
			this.btLanguageEnglish.Name = "btLanguageEnglish";
			this.btLanguageEnglish.OptionGroup = "Language";
			resources.ApplyResources(this.btLanguageEnglish, "btLanguageEnglish");
			this.btLanguageEnglish.Click += new global::System.EventHandler(this.btLanguageEnglish_Click);
			this.btLanguageFrench.Name = "btLanguageFrench";
			this.btLanguageFrench.OptionGroup = "Language";
			resources.ApplyResources(this.btLanguageFrench, "btLanguageFrench");
			this.btLanguageFrench.Click += new global::System.EventHandler(this.btLanguageFrench_Click);
			this.btLanguageSpanish.Name = "btLanguageSpanish";
			this.btLanguageSpanish.OptionGroup = "Language";
			resources.ApplyResources(this.btLanguageSpanish, "btLanguageSpanish");
			this.btLanguageSpanish.Click += new global::System.EventHandler(this.btLanguageSpanish_Click);
			this.lblScrollSpeed.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblScrollSpeed.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblScrollSpeed.CanCustomize = false;
			this.lblScrollSpeed.Name = "lblScrollSpeed";
			this.lblScrollSpeed.PaddingBottom = 3;
			this.lblScrollSpeed.PaddingLeft = 10;
			this.lblScrollSpeed.PaddingTop = 3;
			this.lblScrollSpeed.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblScrollSpeed, "lblScrollSpeed");
			this.sliderScrollSpeed.AutoCollapseOnClick = false;
			this.sliderScrollSpeed.CanCustomize = false;
			this.sliderScrollSpeed.LabelVisible = false;
			this.sliderScrollSpeed.LabelWidth = 50;
			this.sliderScrollSpeed.Name = "sliderScrollSpeed";
			this.sliderScrollSpeed.Step = 5;
			this.sliderScrollSpeed.TextColor = global::System.Drawing.Color.Black;
			this.sliderScrollSpeed.TrackMarker = false;
			this.sliderScrollSpeed.Value = 0;
			this.sliderScrollSpeed.Width = 150;
			this.containerScroll.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.containerScroll.FixedSize = new global::System.Drawing.Size(0, 16);
			this.containerScroll.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
			this.containerScroll.Name = "containerScroll";
			this.containerScroll.ResizeItemsToFit = false;
			this.containerScroll.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblScrollFast,
				this.lblScrollPadding,
				this.lblScrollSlow
			});
			this.containerScroll.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblScrollFast.CanCustomize = false;
			this.lblScrollFast.Name = "lblScrollFast";
			resources.ApplyResources(this.lblScrollFast, "lblScrollFast");
			this.lblScrollFast.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblScrollFast.WordWrap = true;
			this.lblScrollPadding.CanCustomize = false;
			this.lblScrollPadding.Name = "lblScrollPadding";
			this.lblScrollPadding.Stretch = true;
			this.lblScrollPadding.Width = 100;
			this.lblScrollSlow.CanCustomize = false;
			this.lblScrollSlow.Name = "lblScrollSlow";
			resources.ApplyResources(this.lblScrollSlow, "lblScrollSlow");
			this.lblScrollSlow.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblScrollSlow.WordWrap = true;
			this.lblDataFolder.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblDataFolder.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblDataFolder.CanCustomize = false;
			this.lblDataFolder.Name = "lblDataFolder";
			this.lblDataFolder.PaddingBottom = 3;
			this.lblDataFolder.PaddingLeft = 10;
			this.lblDataFolder.PaddingTop = 3;
			this.lblDataFolder.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblDataFolder, "lblDataFolder");
			this.lblDataFolder.Visible = false;
			this.lblPersonalPreferences.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblPersonalPreferences.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblPersonalPreferences.CanCustomize = false;
			this.lblPersonalPreferences.Name = "lblPersonalPreferences";
			this.lblPersonalPreferences.PaddingBottom = 3;
			this.lblPersonalPreferences.PaddingLeft = 10;
			this.lblPersonalPreferences.PaddingTop = 3;
			this.lblPersonalPreferences.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			this.lblPersonalPreferences.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.buttonItem3
			});
			resources.ApplyResources(this.lblPersonalPreferences, "lblPersonalPreferences");
			this.buttonItem3.Name = "buttonItem3";
			resources.ApplyResources(this.buttonItem3, "buttonItem3");
			this.btSelectDataFolder.Name = "btSelectDataFolder";
			resources.ApplyResources(this.btSelectDataFolder, "btSelectDataFolder");
			this.btSelectDataFolder.Click += new global::System.EventHandler(this.btSelectDataFolder_Click);
			this.btPersonalPreferences.Name = "btPersonalPreferences";
			resources.ApplyResources(this.btPersonalPreferences, "btPersonalPreferences");
			this.btPersonalPreferences.Click += new global::System.EventHandler(this.btPersonalPreferences_Click);
			this.btImportationPreferences.Name = "btImportationPreferences";
			resources.ApplyResources(this.btImportationPreferences, "btImportationPreferences");
			this.btImportationPreferences.Click += new global::System.EventHandler(this.btImportationPreferences_Click);
			this.btEnableAutoBackup.BeginGroup = true;
			this.btEnableAutoBackup.Name = "btEnableAutoBackup";
			resources.ApplyResources(this.btEnableAutoBackup, "btEnableAutoBackup");
			this.btEnableAutoBackup.Click += new global::System.EventHandler(this.btEnableAutoBackup_Click);
			this.btSetDBReadOnly.Name = "btSetDBReadOnly";
			resources.ApplyResources(this.btSetDBReadOnly, "btSetDBReadOnly");
			this.btSetDBReadOnly.Click += new global::System.EventHandler(this.btSetDBReadOnly_Click);
			this.lblTheme.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblTheme.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblTheme.CanCustomize = false;
			this.lblTheme.Name = "lblTheme";
			this.lblTheme.PaddingBottom = 3;
			this.lblTheme.PaddingLeft = 10;
			this.lblTheme.PaddingTop = 3;
			this.lblTheme.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblTheme, "lblTheme");
			this.btStyleMetro.CanCustomize = false;
			this.btStyleMetro.Command = this.AppCommandTheme;
			resources.ApplyResources(this.btStyleMetro, "btStyleMetro");
			this.btStyleMetro.Name = "btStyleMetro";
			this.btStyleMetro.OptionGroup = "style";
			this.AppCommandTheme.Name = "AppCommandTheme";
			this.AppCommandTheme.Executed += new global::System.EventHandler(this.AppCommandTheme_Executed);
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
			this.btSetThemeColor.SelectedColorChanged += new global::System.EventHandler(this.btSetThemeColor_SelectedColorChanged);
			this.btSetThemeColor.ColorPreview += new global::DevComponents.DotNetBar.ColorPreviewEventHandler(this.btSetThemeColor_ColorPreview);
			this.btSetThemeColor.PopupShowing += new global::System.EventHandler(this.btSetThemeColor_PopupShowing);
			this.btSetThemeColor.ExpandChange += new global::System.EventHandler(this.btSetThemeColor_ExpandChange);
			this.lblPanels.BackColor = global::System.Drawing.Color.FromArgb(221, 231, 238);
			this.lblPanels.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.lblPanels.CanCustomize = false;
			this.lblPanels.Name = "lblPanels";
			this.lblPanels.PaddingBottom = 3;
			this.lblPanels.PaddingLeft = 10;
			this.lblPanels.PaddingTop = 3;
			this.lblPanels.SingleLineColor = global::System.Drawing.Color.FromArgb(197, 197, 197);
			resources.ApplyResources(this.lblPanels, "lblPanels");
			this.btResetDefaultPanelsLayout.Name = "btResetDefaultPanelsLayout";
			resources.ApplyResources(this.btResetDefaultPanelsLayout, "btResetDefaultPanelsLayout");
			this.btResetDefaultPanelsLayout.Click += new global::System.EventHandler(this.btResetDefaultPanelsLayout_Click);
			this.btHelp.Image = (global::System.Drawing.Image)resources.GetObject("btHelp.Image");
			this.btHelp.Name = "btHelp";
			resources.ApplyResources(this.btHelp, "btHelp");
			this.btHelp.Click += new global::System.EventHandler(this.btHelp_Click);
			this.startButton.AutoExpandOnClick = true;
			this.startButton.CanCustomize = false;
			this.startButton.HotTrackingStyle = global::DevComponents.DotNetBar.eHotTrackingStyle.Image;
			this.startButton.Image = global::QuoterPlan.Properties.Resources.ruler;
			this.startButton.ImagePaddingHorizontal = 2;
			this.startButton.ImagePaddingVertical = 2;
			this.startButton.Name = "startButton";
			this.startButton.ShowSubItems = false;
			this.startButton.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.itemContainerFileMenu
			});
			resources.ApplyResources(this.startButton, "startButton");
			this.startButton.PopupOpen += new global::DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.startButton_PopupOpen);
			this.startButton.PopupClose += new global::System.EventHandler(this.startButton_PopupClose);
			this.itemContainerFileMenu.BackgroundStyle.Class = "RibbonFileMenuTwoColumnContainer";
			this.itemContainerFileMenu.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerFileMenu.CanCustomize = false;
			this.itemContainerFileMenu.ItemSpacing = 0;
			this.itemContainerFileMenu.Name = "itemContainerFileMenu";
			this.itemContainerFileMenu.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.itemContainerFileMenu2,
				this.galleryRecentProjects
			});
			this.itemContainerFileMenu.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerFileMenu2.BackgroundStyle.Class = "RibbonFileMenuColumnOneContainer";
			this.itemContainerFileMenu2.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerFileMenu2.CanCustomize = false;
			this.itemContainerFileMenu2.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerFileMenu2.MinimumSize = new global::System.Drawing.Size(200, 0);
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
			this.itemContainerFileMenu2.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btProjectNew.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btProjectNew.CanCustomize = false;
			this.btProjectNew.Image = (global::System.Drawing.Image)resources.GetObject("btProjectNew.Image");
			this.btProjectNew.ImagePaddingVertical = 5;
			this.btProjectNew.Name = "btProjectNew";
			this.btProjectNew.Shortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlN);
			this.btProjectNew.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btProjectNew, "btProjectNew");
			this.btProjectNew.Click += new global::System.EventHandler(this.btProjectNew_Click);
			this.btProjectOpen.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btProjectOpen.CanCustomize = false;
			this.btProjectOpen.Image = (global::System.Drawing.Image)resources.GetObject("btProjectOpen.Image");
			this.btProjectOpen.ImagePaddingVertical = 5;
			this.btProjectOpen.Name = "btProjectOpen";
			this.btProjectOpen.Shortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlO);
			this.btProjectOpen.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btProjectOpen, "btProjectOpen");
			this.btProjectOpen.Click += new global::System.EventHandler(this.btProjectOpen_Click);
			this.btProjectSave.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btProjectSave.CanCustomize = false;
			this.btProjectSave.Image = (global::System.Drawing.Image)resources.GetObject("btProjectSave.Image");
			this.btProjectSave.ImagePaddingVertical = 5;
			this.btProjectSave.Name = "btProjectSave";
			this.btProjectSave.Shortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlS);
			this.btProjectSave.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btProjectSave, "btProjectSave");
			this.btProjectSave.Click += new global::System.EventHandler(this.btProjectSave_Click);
			this.btProjectSaveAs.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btProjectSaveAs.CanCustomize = false;
			this.btProjectSaveAs.Image = (global::System.Drawing.Image)resources.GetObject("btProjectSaveAs.Image");
			this.btProjectSaveAs.ImagePaddingVertical = 5;
			this.btProjectSaveAs.Name = "btProjectSaveAs";
			this.btProjectSaveAs.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btProjectSaveAs, "btProjectSaveAs");
			this.btProjectSaveAs.Click += new global::System.EventHandler(this.btProjectSaveAs_Click);
			this.btProjectInfo.BeginGroup = true;
			this.btProjectInfo.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btProjectInfo.CanCustomize = false;
			this.btProjectInfo.Image = global::QuoterPlan.Properties.Resources.properties;
			this.btProjectInfo.Name = "btProjectInfo";
			resources.ApplyResources(this.btProjectInfo, "btProjectInfo");
			this.btProjectInfo.Click += new global::System.EventHandler(this.btProjectInfo_Click);
			this.btProjectClose.BeginGroup = true;
			this.btProjectClose.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btProjectClose.CanCustomize = false;
			this.btProjectClose.Image = (global::System.Drawing.Image)resources.GetObject("btProjectClose.Image");
			this.btProjectClose.ImagePaddingVertical = 5;
			this.btProjectClose.Name = "btProjectClose";
			this.btProjectClose.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btProjectClose, "btProjectClose");
			this.btProjectClose.Click += new global::System.EventHandler(this.btProjectClose_Click);
			this.btHelpContent.BeginGroup = true;
			this.btHelpContent.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btHelpContent.CanCustomize = false;
			this.btHelpContent.Image = (global::System.Drawing.Image)resources.GetObject("btHelpContent.Image");
			this.btHelpContent.ImagePaddingVertical = 5;
			this.btHelpContent.Name = "btHelpContent";
			this.btHelpContent.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btHelpContent, "btHelpContent");
			this.btHelpContent.Click += new global::System.EventHandler(this.btHelpContent_Click);
			this.btHelpYoutube.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btHelpYoutube.CanCustomize = false;
			this.btHelpYoutube.Image = (global::System.Drawing.Image)resources.GetObject("btHelpYoutube.Image");
			this.btHelpYoutube.ImagePaddingVertical = 5;
			this.btHelpYoutube.Name = "btHelpYoutube";
			this.btHelpYoutube.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btHelpYoutube, "btHelpYoutube");
			this.btHelpYoutube.Click += new global::System.EventHandler(this.btHelpYoutube_Click);
			this.btHelpAbout.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btHelpAbout.CanCustomize = false;
			this.btHelpAbout.Image = (global::System.Drawing.Image)resources.GetObject("btHelpAbout.Image");
			this.btHelpAbout.ImagePaddingVertical = 5;
			this.btHelpAbout.Name = "btHelpAbout";
			this.btHelpAbout.SubItemsExpandWidth = 24;
			resources.ApplyResources(this.btHelpAbout, "btHelpAbout");
			this.btHelpAbout.Click += new global::System.EventHandler(this.btHelpAbout_Click);
			this.btLicenseDeactivate.BeginGroup = true;
			this.btLicenseDeactivate.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btLicenseDeactivate.CanCustomize = false;
			this.btLicenseDeactivate.Image = global::QuoterPlan.Properties.Resources.deactivate;
			this.btLicenseDeactivate.ImagePaddingVertical = 5;
			this.btLicenseDeactivate.Name = "btLicenseDeactivate";
			resources.ApplyResources(this.btLicenseDeactivate, "btLicenseDeactivate");
			this.btLicenseDeactivate.Click += new global::System.EventHandler(this.btLicenseDeactivate_Click);
			this.btExit.BeginGroup = true;
			this.btExit.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btExit.CanCustomize = false;
			this.btExit.Image = global::QuoterPlan.Properties.Resources.exit;
			this.btExit.Name = "btExit";
			this.btExit.Shortcuts.Add(global::DevComponents.DotNetBar.eShortcut.AltF4);
			resources.ApplyResources(this.btExit, "btExit");
			this.btExit.Click += new global::System.EventHandler(this.btExit_Click);
			this.galleryRecentProjects.BackgroundStyle.Class = "RibbonFileMenuColumnTwoContainer";
			this.galleryRecentProjects.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryRecentProjects.CanCustomize = false;
			this.galleryRecentProjects.DefaultSize = new global::System.Drawing.Size(358, 240);
			this.galleryRecentProjects.EnableGalleryPopup = false;
			this.galleryRecentProjects.FixedSize = new global::System.Drawing.Size(358, 240);
			this.galleryRecentProjects.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.galleryRecentProjects.MinimumSize = new global::System.Drawing.Size(358, 240);
			this.galleryRecentProjects.MultiLine = false;
			this.galleryRecentProjects.Name = "galleryRecentProjects";
			this.galleryRecentProjects.PopupUsesStandardScrollbars = false;
			this.galleryRecentProjects.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblFileRecentProjects
			});
			this.galleryRecentProjects.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblFileRecentProjects.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Bottom;
			this.lblFileRecentProjects.BorderType = global::DevComponents.DotNetBar.eBorderType.Etched;
			this.lblFileRecentProjects.CanCustomize = false;
			this.lblFileRecentProjects.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.lblFileRecentProjects.Name = "lblFileRecentProjects";
			this.lblFileRecentProjects.PaddingBottom = 2;
			this.lblFileRecentProjects.PaddingTop = 2;
			this.lblFileRecentProjects.Stretch = true;
			resources.ApplyResources(this.lblFileRecentProjects, "lblFileRecentProjects");
			this.btSave.Image = global::QuoterPlan.Properties.Resources.document_save_16x16;
			this.btSave.Name = "btSave";
			this.btSave.Click += new global::System.EventHandler(this.btProjectSave_Click);
			this.btUndo.BeginGroup = true;
			this.btUndo.Image = global::QuoterPlan.Properties.Resources.undo_icon__16x16;
			this.btUndo.Name = "btUndo";
			this.btUndo.Click += new global::System.EventHandler(this.btEditUndo_Click);
			this.btRedo.Image = global::QuoterPlan.Properties.Resources.redo_icon_16x16;
			this.btRedo.Name = "btRedo";
			this.btRedo.Click += new global::System.EventHandler(this.btEditRedo_Click);
			this.galleryGroup1.Name = "galleryGroup1";
			resources.ApplyResources(this.galleryGroup1, "galleryGroup1");
			resources.ApplyResources(this.barStatus, "barStatus");
			this.barStatus.AccessibleRole = global::System.Windows.Forms.AccessibleRole.StatusBar;
			this.barStatus.AntiAlias = true;
			this.barStatus.BarType = global::DevComponents.DotNetBar.eBarType.StatusBar;
			this.barStatus.GrabHandleStyle = global::DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle;
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
			this.barStatus.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barStatus.TabStop = false;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.PaddingBottom = 3;
			this.lblStatus.PaddingTop = 3;
			resources.ApplyResources(this.lblStatus, "lblStatus");
			this.lblStatus.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblStatusBarPadding.Name = "lblStatusBarPadding";
			this.lblStatusBarPadding.Width = 3;
			this.lblOrtho.Name = "lblOrtho";
			this.lblOrtho.PaddingBottom = 1;
			this.lblOrtho.PaddingRight = 3;
			resources.ApplyResources(this.lblOrtho, "lblOrtho");
			this.lblOrtho.TextAlignment = global::System.Drawing.StringAlignment.Far;
			this.switchOrtho.ButtonWidth = 100;
			this.switchOrtho.Name = "switchOrtho";
			this.switchOrtho.OffBackColor = global::System.Drawing.Color.Linen;
			this.switchOrtho.OffTextColor = global::System.Drawing.Color.FromArgb(64, 64, 64);
			this.switchOrtho.OnBackColor = global::System.Drawing.Color.ForestGreen;
			this.switchOrtho.OnTextColor = global::System.Drawing.Color.White;
			this.switchOrtho.SwitchWidth = 50;
			this.switchOrtho.ValueChanged += new global::System.EventHandler(this.switchOrtho_ValueChanged);
			this.lblStatusBarPadding2.Name = "lblStatusBarPadding2";
			this.lblStatusBarPadding2.Width = 2;
			this.lblImageQuality.Name = "lblImageQuality";
			this.lblImageQuality.PaddingBottom = 1;
			this.lblImageQuality.Text = global::QuoterPlan.Properties.Resources.Qualité_Haute;
			this.lblImageQuality.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblImageQuality.Width = 90;
			this.qualitySlider.LabelVisible = false;
			this.qualitySlider.Maximum = 1;
			this.qualitySlider.Name = "qualitySlider";
			resources.ApplyResources(this.qualitySlider, "qualitySlider");
			this.qualitySlider.TrackMarker = false;
			this.qualitySlider.Value = 1;
			this.qualitySlider.Width = 60;
			this.qualitySlider.ValueChanged += new global::System.EventHandler(this.qualitySlider_ValueChanged);
			this.lblStatusBarPadding3.Name = "lblStatusBarPadding3";
			this.lblStatusBarPadding3.Width = 3;
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.PaddingBottom = 1;
			this.lblZoom.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblStatusPadding2
			});
			resources.ApplyResources(this.lblZoom, "lblZoom");
			this.lblZoom.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.lblZoom.Width = 35;
			this.lblStatusPadding2.Name = "lblStatusPadding2";
			this.lblStatusPadding2.Width = 5;
			this.zoomSlider.LabelVisible = false;
			this.zoomSlider.Maximum = 300;
			this.zoomSlider.Minimum = 10;
			this.zoomSlider.Name = "zoomSlider";
			resources.ApplyResources(this.zoomSlider, "zoomSlider");
			this.zoomSlider.TrackMarker = false;
			this.zoomSlider.Value = 0;
			this.zoomSlider.Width = 200;
			this.zoomSlider.ValueChanged += new global::System.EventHandler(this.zoomSlider_ValueChanged);
			this.zoomSlider.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.zoomSlider_MouseUp);
			this.lstLayers.AllowDrop = true;
			this.lstLayers.AllowUserToResizeColumns = false;
			this.lstLayers.BackColor = global::System.Drawing.SystemColors.Window;
			this.lstLayers.BackgroundStyle.BorderColor = global::System.Drawing.Color.Transparent;
			this.lstLayers.BackgroundStyle.BorderColor2 = global::System.Drawing.Color.Transparent;
			this.lstLayers.BackgroundStyle.Class = "TreeBorderKey";
			this.lstLayers.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lstLayers.CellEdit = true;
			this.lstLayers.Columns.Add(this.columnLayerVisible);
			this.lstLayers.Columns.Add(this.columnLayerName);
			this.lstLayers.Columns.Add(this.columnLayerOpacity);
			this.lstLayers.ColumnsVisible = false;
			resources.ApplyResources(this.lstLayers, "lstLayers");
			this.lstLayers.DragDropEnabled = false;
			this.lstLayers.DragDropNodeCopyEnabled = false;
			this.lstLayers.ExpandBorderColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
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
			this.nodeConnector2.LineColor = global::System.Drawing.SystemColors.ControlText;
			this.elementStyle1.BorderTopWidth = 1;
			this.elementStyle1.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = global::System.Drawing.SystemColors.ControlText;
			this.elementStyle8.BackColor = global::System.Drawing.Color.FromArgb(255, 255, 255);
			this.elementStyle8.BackColor2 = global::System.Drawing.Color.FromArgb(210, 224, 252);
			this.elementStyle8.BackColorGradientAngle = 90;
			this.elementStyle8.BorderBottom = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle8.BorderBottomWidth = 1;
			this.elementStyle8.BorderColor = global::System.Drawing.Color.DarkGray;
			this.elementStyle8.BorderLeft = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle8.BorderLeftWidth = 1;
			this.elementStyle8.BorderRight = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle8.BorderRightWidth = 1;
			this.elementStyle8.BorderTop = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle8.BorderTopWidth = 1;
			this.elementStyle8.CornerDiameter = 4;
			this.elementStyle8.CornerType = global::DevComponents.DotNetBar.eCornerType.Rounded;
			this.elementStyle8.Description = "BlueLight";
			this.elementStyle8.Name = "elementStyle8";
			this.elementStyle8.PaddingBottom = 1;
			this.elementStyle8.PaddingLeft = 1;
			this.elementStyle8.PaddingRight = 1;
			this.elementStyle8.PaddingTop = 1;
			this.elementStyle8.TextColor = global::System.Drawing.Color.FromArgb(69, 84, 115);
			this.elementStyle2.BackColor2SchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
			this.elementStyle2.BackColorGradientAngle = 90;
			this.elementStyle2.BackColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
			this.elementStyle2.BorderBottomWidth = 1;
			this.elementStyle2.BorderColor = global::System.Drawing.Color.DarkGray;
			this.elementStyle2.BorderLeftWidth = 1;
			this.elementStyle2.BorderRightWidth = 1;
			this.elementStyle2.BorderTopWidth = 1;
			this.elementStyle2.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle2.Description = "Yellow";
			this.elementStyle2.Name = "elementStyle2";
			this.elementStyle2.PaddingBottom = 1;
			this.elementStyle2.PaddingLeft = 1;
			this.elementStyle2.PaddingRight = 1;
			this.elementStyle2.PaddingTop = 1;
			this.elementStyle2.TextColor = global::System.Drawing.Color.Black;
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
			this.barLayers.ColorScheme.PredefinedColorScheme = global::DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
			resources.ApplyResources(this.barLayers, "barLayers");
			this.barLayers.DockTabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Left;
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
			this.barLayers.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barLayers.TabStop = false;
			this.btLayerAdd.Image = (global::System.Drawing.Image)resources.GetObject("btLayerAdd.Image");
			this.btLayerAdd.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerAdd.Name = "btLayerAdd";
			this.btLayerAdd.Click += new global::System.EventHandler(this.btLayerAdd_Click);
			this.btLayerRemove.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			this.btLayerRemove.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerRemove.Name = "btLayerRemove";
			this.btLayerRemove.Click += new global::System.EventHandler(this.btLayerRemove_Click);
			this.btLayerRename.Image = (global::System.Drawing.Image)resources.GetObject("btLayerRename.Image");
			this.btLayerRename.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerRename.Name = "btLayerRename";
			this.btLayerRename.Click += new global::System.EventHandler(this.btLayerEdit_Click);
			this.btLayerMoveUp.BeginGroup = true;
			this.btLayerMoveUp.Image = global::QuoterPlan.Properties.Resources.move_up_16x16;
			this.btLayerMoveUp.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerMoveUp.Name = "btLayerMoveUp";
			this.btLayerMoveUp.Click += new global::System.EventHandler(this.btLayerMoveUp_Click);
			this.btLayerMoveDown.Image = global::QuoterPlan.Properties.Resources.move_down_16x16;
			this.btLayerMoveDown.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerMoveDown.Name = "btLayerMoveDown";
			this.btLayerMoveDown.Click += new global::System.EventHandler(this.btLayerMoveDown_Click);
			this.btLayerSaveList.BeginGroup = true;
			this.btLayerSaveList.Image = global::QuoterPlan.Properties.Resources.document_save_16x16;
			this.btLayerSaveList.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerSaveList.Name = "btLayerSaveList";
			this.btLayerSaveList.Click += new global::System.EventHandler(this.btLayerSaveList_Click);
			this.btLayerSaveListAs.Image = global::QuoterPlan.Properties.Resources.document_save_as_16x16;
			this.btLayerSaveListAs.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerSaveListAs.Name = "btLayerSaveListAs";
			this.btLayerSaveListAs.Click += new global::System.EventHandler(this.btLayerSaveListAs_Click);
			this.btLayerOpenList.BeginGroup = true;
			this.btLayerOpenList.Image = global::QuoterPlan.Properties.Resources.document_open_16x16;
			this.btLayerOpenList.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayerOpenList.Name = "btLayerOpenList";
			this.btLayerOpenList.Click += new global::System.EventHandler(this.btLayerOpenList_Click);
			this.btLayersToggle.AutoExpandOnClick = true;
			this.btLayersToggle.BeginGroup = true;
			this.btLayersToggle.Image = global::QuoterPlan.Properties.Resources.checked_list_16x16;
			this.btLayersToggle.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btLayersToggle.ImagePaddingHorizontal = 4;
			this.btLayersToggle.ImagePaddingVertical = 2;
			this.btLayersToggle.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btLayersToggle.Name = "btLayersToggle";
			this.btLayersToggle.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btLayersMakeVisible,
				this.btLayersMakeInvisible
			});
			this.btLayersMakeVisible.Name = "btLayersMakeVisible";
			resources.ApplyResources(this.btLayersMakeVisible, "btLayersMakeVisible");
			this.btLayersMakeVisible.Click += new global::System.EventHandler(this.btLayersMakeVisible_Click);
			this.btLayersMakeInvisible.Name = "btLayersMakeInvisible";
			resources.ApplyResources(this.btLayersMakeInvisible, "btLayersMakeInvisible");
			this.btLayersMakeInvisible.Click += new global::System.EventHandler(this.btLayersMakeInvisible_Click);
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
			this.barDisplayResults.DockTabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Left;
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
			this.barDisplayResults.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barDisplayResults.TabStop = false;
			this.lblDisplayResults.ForeColor = global::System.Drawing.Color.Black;
			this.lblDisplayResults.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Far;
			this.lblDisplayResults.Name = "lblDisplayResults";
			this.lblDisplayResults.PaddingRight = 3;
			resources.ApplyResources(this.lblDisplayResults, "lblDisplayResults");
			this.btDisplayResultsForThisPlan.AutoCheckOnClick = true;
			this.btDisplayResultsForThisPlan.CanCustomize = false;
			this.btDisplayResultsForThisPlan.Checked = true;
			this.btDisplayResultsForThisPlan.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Far;
			this.btDisplayResultsForThisPlan.Name = "btDisplayResultsForThisPlan";
			this.btDisplayResultsForThisPlan.OptionGroup = "DisplayResults";
			resources.ApplyResources(this.btDisplayResultsForThisPlan, "btDisplayResultsForThisPlan");
			this.lblDisplayResultsPadding.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Far;
			this.lblDisplayResultsPadding.Name = "lblDisplayResultsPadding";
			this.lblDisplayResultsPadding.Width = 1;
			this.btDisplayResultsForAllPlans.AutoCheckOnClick = true;
			this.btDisplayResultsForAllPlans.CanCustomize = false;
			this.btDisplayResultsForAllPlans.ItemAlignment = global::DevComponents.DotNetBar.eItemAlignment.Far;
			this.btDisplayResultsForAllPlans.Name = "btDisplayResultsForAllPlans";
			this.btDisplayResultsForAllPlans.OptionGroup = "DisplayResults";
			resources.ApplyResources(this.btDisplayResultsForAllPlans, "btDisplayResultsForAllPlans");
			this.tabProperties.BackColor = global::System.Drawing.Color.FromArgb(113, 113, 113);
			this.tabProperties.CanReorderTabs = false;
			this.tabProperties.ColorScheme.TabBorder = global::System.Drawing.Color.Transparent;
			this.tabProperties.ColorScheme.TabItemBackgroundColorBlend.AddRange(new global::DevComponents.DotNetBar.BackgroundColorBlend[]
			{
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(215, 230, 249), 0f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(199, 220, 248), 0.45f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(179, 208, 245), 0.45f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(215, 229, 247), 1f)
			});
			this.tabProperties.ColorScheme.TabItemHotBackgroundColorBlend.AddRange(new global::DevComponents.DotNetBar.BackgroundColorBlend[]
			{
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(255, 253, 235), 0f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(255, 236, 168), 0.45f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(255, 218, 89), 0.45f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(255, 230, 141), 1f)
			});
			this.tabProperties.ColorScheme.TabItemSelectedBackgroundColorBlend.AddRange(new global::DevComponents.DotNetBar.BackgroundColorBlend[]
			{
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.White, 0f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(253, 253, 254), 0.45f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(253, 253, 254), 0.45f),
				new global::DevComponents.DotNetBar.BackgroundColorBlend(global::System.Drawing.Color.FromArgb(253, 253, 254), 1f)
			});
			this.tabProperties.Controls.Add(this.tabControlPanel1);
			this.tabProperties.Controls.Add(this.tabControlPanel2);
			resources.ApplyResources(this.tabProperties, "tabProperties");
			this.tabProperties.ForeColor = global::System.Drawing.Color.Black;
			this.tabProperties.Name = "tabProperties";
			this.tabProperties.SelectedTabFont = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold);
			this.tabProperties.SelectedTabIndex = 0;
			this.tabProperties.Style = global::DevComponents.DotNetBar.eTabStripStyle.VS2005Document;
			this.tabProperties.TabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Bottom;
			this.tabProperties.TabLayoutType = global::DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
			this.tabProperties.Tabs.Add(this.tabItem1);
			this.tabProperties.Tabs.Add(this.tabItem2);
			this.tabControlPanel1.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.tabControlPanel1.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.tabControlPanel1.Controls.Add(this.superTabProperties);
			resources.ApplyResources(this.tabControlPanel1, "tabControlPanel1");
			this.tabControlPanel1.Name = "tabControlPanel1";
			this.tabControlPanel1.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.None;
			this.tabControlPanel1.Style.BorderWidth = 0;
			this.tabControlPanel1.Style.GradientAngle = -90;
			this.tabControlPanel1.StyleMouseOver.BorderWidth = 0;
			this.tabControlPanel1.TabItem = this.tabItem1;
			this.tabControlPanel1.UseCustomStyle = true;
			this.tabControlPanel1.Resize += new global::System.EventHandler(this.tabControlPanel1_Resize);
			this.superTabProperties.BackColor = global::System.Drawing.Color.White;
			this.superTabProperties.ControlBox.CloseBox.Name = global::QuoterPlan.Properties.Resources.Coller;
			this.superTabProperties.ControlBox.MenuBox.Name = global::QuoterPlan.Properties.Resources.Coller;
			this.superTabProperties.ControlBox.Name = global::QuoterPlan.Properties.Resources.Coller;
			this.superTabProperties.ControlBox.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.superTabProperties.ControlBox.MenuBox,
				this.superTabProperties.ControlBox.CloseBox
			});
			this.superTabProperties.Controls.Add(this.superTabControlPanel1);
			resources.ApplyResources(this.superTabProperties, "superTabProperties");
			this.superTabProperties.ForeColor = global::System.Drawing.Color.Black;
			this.superTabProperties.Name = "superTabProperties";
			this.superTabProperties.ReorderTabsEnabled = false;
			this.superTabProperties.SelectedTabIndex = 0;
			this.superTabProperties.TabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Bottom;
			this.superTabProperties.TabHorizontalSpacing = 3;
			this.superTabProperties.Tabs.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.superTabItem1
			});
			this.superTabProperties.TabStyle = global::DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
			this.superTabProperties.TabsVisible = false;
			this.superTabProperties.TabVerticalSpacing = 5;
			this.superTabControlPanel1.Controls.Add(this.gridObjectProperties);
			resources.ApplyResources(this.superTabControlPanel1, "superTabControlPanel1");
			this.superTabControlPanel1.Name = "superTabControlPanel1";
			this.superTabControlPanel1.TabItem = this.superTabItem1;
			resources.ApplyResources(this.gridObjectProperties, "gridObjectProperties");
			this.gridObjectProperties.GridLinesColor = global::System.Drawing.Color.WhiteSmoke;
			this.gridObjectProperties.Name = "gridObjectProperties";
			this.superTabItem1.AttachedControl = this.superTabControlPanel1;
			this.superTabItem1.GlobalItem = false;
			this.superTabItem1.Name = "superTabItem1";
			resources.ApplyResources(this.superTabItem1, "superTabItem1");
			this.superTabItem1.TextAlignment = new global::DevComponents.DotNetBar.eItemAlignment?(global::DevComponents.DotNetBar.eItemAlignment.Center);
			this.tabItem1.AttachedControl = this.tabControlPanel1;
			this.tabItem1.Name = "tabItem1";
			this.tabItem1.Text = global::QuoterPlan.Properties.Resources.Propriétés;
			this.tabControlPanel2.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.tabControlPanel2.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.tabControlPanel2.Controls.Add(this.extensionsManager);
			resources.ApplyResources(this.tabControlPanel2, "tabControlPanel2");
			this.tabControlPanel2.Name = "tabControlPanel2";
			this.tabControlPanel2.Style.BackColor2.Color = global::System.Drawing.Color.FromArgb(251, 250, 247);
			this.tabControlPanel2.Style.BorderSide = (global::DevComponents.DotNetBar.eBorderSide.Left | global::DevComponents.DotNetBar.eBorderSide.Right | global::DevComponents.DotNetBar.eBorderSide.Top);
			this.tabControlPanel2.Style.GradientAngle = -90;
			this.tabControlPanel2.TabItem = this.tabItem2;
			this.tabControlPanel2.UseCustomStyle = true;
			this.extensionsManager.BackColor = global::System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.extensionsManager, "extensionsManager");
			this.extensionsManager.HelpUtilities = null;
			this.extensionsManager.Name = "extensionsManager";
			this.tabItem2.AttachedControl = this.tabControlPanel2;
			this.tabItem2.Name = "tabItem2";
			resources.ApplyResources(this.tabItem2, "tabItem2");
			this.cbObjects.DisableInternalDrawing = true;
			resources.ApplyResources(this.cbObjects, "cbObjects");
			this.cbObjects.DrawMode = global::System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbObjects.DropDownHeight = 220;
			this.cbObjects.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbObjects.FormattingEnabled = true;
			this.cbObjects.Name = "cbObjects";
			this.cbObjects.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbObjects.WatermarkEnabled = false;
			this.panControl.BackColor = global::System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.panControl, "panControl");
			this.panControl.Name = "panControl";
			this.panControl.PanFromScrolling = false;
			this.panControl.PanRectangle = new global::System.Drawing.Rectangle(0, 0, 1, 1);
			this.panControl.Panning += new global::QuoterPlanControls.PanningEventHandler(this.panControl_Panning);
			this.panControl.Paint += new global::System.Windows.Forms.PaintEventHandler(this.panControl_Paint);
			this.panControl.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.panControl_MouseMove);
			this.panControl.Resize += new global::System.EventHandler(this.panControl_Resize);
			this.lstRecentPlans.AllowDrop = true;
			this.lstRecentPlans.AllowUserToResizeColumns = false;
			this.lstRecentPlans.BackColor = global::System.Drawing.SystemColors.Window;
			this.lstRecentPlans.BackgroundStyle.BorderColor = global::System.Drawing.Color.Transparent;
			this.lstRecentPlans.BackgroundStyle.BorderColor2 = global::System.Drawing.Color.Transparent;
			this.lstRecentPlans.BackgroundStyle.Class = "TreeBorderKey";
			this.lstRecentPlans.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.lstRecentPlans.ExpandBorderColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
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
			this.nodeConnector3.LineColor = global::System.Drawing.SystemColors.ControlText;
			this.elementStyle3.BackColor2SchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2;
			this.elementStyle3.BackColorGradientAngle = 90;
			this.elementStyle3.BackColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
			this.elementStyle3.BorderBottomWidth = 1;
			this.elementStyle3.BorderColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarCaptionText;
			this.elementStyle3.BorderLeftWidth = 1;
			this.elementStyle3.BorderRightWidth = 1;
			this.elementStyle3.BorderTopWidth = 1;
			this.elementStyle3.CornerDiameter = 4;
			this.elementStyle3.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle3.Description = "Blue";
			this.elementStyle3.Name = "elementStyle3";
			this.elementStyle3.PaddingBottom = 4;
			this.elementStyle3.PaddingLeft = 4;
			this.elementStyle3.PaddingRight = 4;
			this.elementStyle3.PaddingTop = 4;
			this.elementStyle3.TextColor = global::System.Drawing.Color.Black;
			this.elementStyle4.BackColorGradientAngle = 90;
			this.elementStyle4.BorderBottomWidth = 1;
			this.elementStyle4.BorderColor = global::System.Drawing.Color.DarkGray;
			this.elementStyle4.BorderLeftWidth = 1;
			this.elementStyle4.BorderRightWidth = 1;
			this.elementStyle4.BorderTopWidth = 1;
			this.elementStyle4.CornerDiameter = 4;
			this.elementStyle4.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle4.Description = "Blue";
			this.elementStyle4.Name = "elementStyle4";
			this.elementStyle4.PaddingBottom = 1;
			this.elementStyle4.PaddingLeft = 1;
			this.elementStyle4.PaddingRight = 1;
			this.elementStyle4.PaddingTop = 1;
			this.elementStyle4.TextColor = global::System.Drawing.Color.Black;
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
			this.barRecentPlans.ColorScheme.PredefinedColorScheme = global::DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
			resources.ApplyResources(this.barRecentPlans, "barRecentPlans");
			this.barRecentPlans.DockTabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Left;
			this.barRecentPlans.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btPlanRename
			});
			this.barRecentPlans.Name = "barRecentPlans";
			this.barRecentPlans.RoundCorners = false;
			this.barRecentPlans.SaveLayoutChanges = false;
			this.barRecentPlans.Stretch = true;
			this.barRecentPlans.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barRecentPlans.TabStop = false;
			this.btPlanRename.Image = (global::System.Drawing.Image)resources.GetObject("btPlanRename.Image");
			this.btPlanRename.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btPlanRename.Name = "btPlanRename";
			this.btPlanRename.Click += new global::System.EventHandler(this.btPlanRename_Click);
			this.treeObjects.AllowDrop = true;
			this.treeObjects.AllowUserToResizeColumns = false;
			this.treeObjects.BackColor = global::System.Drawing.SystemColors.Window;
			this.treeObjects.BackgroundStyle.BorderColor = global::System.Drawing.Color.Transparent;
			this.treeObjects.BackgroundStyle.BorderColor2 = global::System.Drawing.Color.Transparent;
			this.treeObjects.BackgroundStyle.Class = "TreeBorderKey";
			this.treeObjects.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
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
			this.treeObjects.ForeColor = global::System.Drawing.Color.Black;
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
			this.nodeConnector1.LineColor = global::System.Drawing.SystemColors.ControlText;
			this.elementStyle7.BackColor = global::System.Drawing.Color.FromArgb(221, 230, 247);
			this.elementStyle7.BackColor2 = global::System.Drawing.Color.FromArgb(138, 168, 228);
			this.elementStyle7.BackColorGradientAngle = 90;
			this.elementStyle7.BorderBottom = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle7.BorderBottomWidth = 1;
			this.elementStyle7.BorderColor = global::System.Drawing.Color.DarkGray;
			this.elementStyle7.BorderLeft = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle7.BorderLeftWidth = 1;
			this.elementStyle7.BorderRight = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle7.BorderRightWidth = 1;
			this.elementStyle7.BorderTop = global::DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle7.BorderTopWidth = 1;
			this.elementStyle7.CornerDiameter = 4;
			this.elementStyle7.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle7.Description = "Blue";
			this.elementStyle7.Name = "elementStyle7";
			this.elementStyle7.PaddingBottom = 1;
			this.elementStyle7.PaddingLeft = 1;
			this.elementStyle7.PaddingRight = 1;
			this.elementStyle7.PaddingTop = 1;
			this.elementStyle7.TextColor = global::System.Drawing.Color.Black;
			this.panelPlanName.Controls.Add(this.txtPlanName);
			this.panelPlanName.Controls.Add(this.cbPlans);
			resources.ApplyResources(this.panelPlanName, "panelPlanName");
			this.panelPlanName.Name = "panelPlanName";
			this.txtPlanName.AssociatedLabel = null;
			resources.ApplyResources(this.txtPlanName, "txtPlanName");
			this.txtPlanName.Name = "txtPlanName";
			this.txtPlanName.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			this.cbPlans.DisableInternalDrawing = true;
			resources.ApplyResources(this.cbPlans, "cbPlans");
			this.cbPlans.DrawMode = global::System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbPlans.DropDownHeight = 220;
			this.cbPlans.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPlans.FormattingEnabled = true;
			this.cbPlans.Name = "cbPlans";
			this.cbPlans.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
			this.barGroups.ColorScheme.PredefinedColorScheme = global::DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
			resources.ApplyResources(this.barGroups, "barGroups");
			this.barGroups.DockTabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Left;
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
			this.barGroups.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barGroups.TabStop = false;
			this.btGroupLocate.BeginGroup = true;
			this.btGroupLocate.Image = global::QuoterPlan.Properties.Resources.locate_16x16;
			this.btGroupLocate.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btGroupLocate.Name = "btGroupLocate";
			this.btGroupLocate.Click += new global::System.EventHandler(this.btGroupLocate_Click);
			this.btZoomToObject.Image = global::QuoterPlan.Properties.Resources.zoom_16x16;
			this.btZoomToObject.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btZoomToObject.Name = "btZoomToObject";
			this.btZoomToObject.Click += new global::System.EventHandler(this.btGroupZoomToObject_Click);
			this.btGroupSelect.Image = global::QuoterPlan.Properties.Resources.selection_16x16_alt;
			this.btGroupSelect.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btGroupSelect.Name = "btGroupSelect";
			this.btGroupSelect.Click += new global::System.EventHandler(this.btGroupSelect_Click);
			this.btGroupRemove.Image = global::QuoterPlan.Properties.Resources.delete_16x16;
			this.btGroupRemove.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btGroupRemove.Name = "btGroupRemove";
			this.btGroupRemove.Click += new global::System.EventHandler(this.btGroupRemove_Click);
			this.btGroupRename.Image = (global::System.Drawing.Image)resources.GetObject("btGroupRename.Image");
			this.btGroupRename.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btGroupRename.Name = "btGroupRename";
			this.btGroupRename.Click += new global::System.EventHandler(this.btGroupRename_Click);
			this.btRenamePlan.BeginGroup = true;
			this.btRenamePlan.Image = global::QuoterPlan.Properties.Resources.textfield_rename_16x16;
			this.btRenamePlan.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btRenamePlan.Name = "btRenamePlan";
			this.btRenamePlan.Click += new global::System.EventHandler(this.btRenamePlan_Click);
			this.btGroupsToggle.AutoExpandOnClick = true;
			this.btGroupsToggle.BeginGroup = true;
			this.btGroupsToggle.Image = global::QuoterPlan.Properties.Resources.checked_list_16x16;
			this.btGroupsToggle.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btGroupsToggle.ImagePaddingHorizontal = 4;
			this.btGroupsToggle.ImagePaddingVertical = 2;
			this.btGroupsToggle.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btGroupsToggle.Name = "btGroupsToggle";
			this.btGroupsToggle.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btGroupsMakeVisible,
				this.btGroupsMakeInvisible
			});
			this.btGroupsMakeVisible.Name = "btGroupsMakeVisible";
			resources.ApplyResources(this.btGroupsMakeVisible, "btGroupsMakeVisible");
			this.btGroupsMakeVisible.Click += new global::System.EventHandler(this.btGroupsMakeVisible_Click);
			this.btGroupsMakeInvisible.Name = "btGroupsMakeInvisible";
			resources.ApplyResources(this.btGroupsMakeInvisible, "btGroupsMakeInvisible");
			this.btGroupsMakeInvisible.Click += new global::System.EventHandler(this.btGroupsMakeInvisible_Click);
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
			this.timer1.Interval = 10.0;
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new global::System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			this.itemContainer5.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer5.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer5.Name = "itemContainer5";
			this.itemContainer5.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.galleryGroupArea.Name = "galleryGroupArea";
			this.galleryGroupArea.Text = global::QuoterPlan.Properties.Resources.Surface;
			this.galleryGroupPerimeter.Name = "galleryGroupPerimeter";
			this.galleryGroupPerimeter.Text = global::QuoterPlan.Properties.Resources.Périmètre;
			this.galleryGroupCounter.Name = "galleryGroupCounter";
			this.galleryGroupCounter.Text = global::QuoterPlan.Properties.Resources.Compteur;
			this.tabItem3.Name = "tabItem3";
			this.tabItem3.Text = global::QuoterPlan.Properties.Resources.Coller;
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.flowPlans.AllowDrop = true;
			resources.ApplyResources(this.flowPlans, "flowPlans");
			this.flowPlans.BackColor = global::System.Drawing.SystemColors.Window;
			this.flowPlans.Name = "flowPlans";
			this.sliderItem4.LabelVisible = false;
			this.sliderItem4.Maximum = 300;
			this.sliderItem4.Minimum = 10;
			this.sliderItem4.Name = "sliderItem4";
			resources.ApplyResources(this.sliderItem4, "sliderItem4");
			this.sliderItem4.TrackMarker = false;
			this.sliderItem4.Value = 0;
			this.sliderItem4.Width = 200;
			this.itemContainerBrightness.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerBrightness.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerBrightness.Name = "itemContainerBrightness";
			this.itemContainerBrightness.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblBrightness,
				this.sliderBrightness
			});
			this.itemContainerBrightness.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblBrightness.Name = "lblBrightness";
			this.lblBrightness.PaddingTop = 12;
			resources.ApplyResources(this.lblBrightness, "lblBrightness");
			this.lblBrightness.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.sliderBrightness.LabelPosition = global::DevComponents.DotNetBar.eSliderLabelPosition.Bottom;
			this.sliderBrightness.Maximum = 255;
			this.sliderBrightness.Minimum = -255;
			this.sliderBrightness.Name = "sliderBrightness";
			resources.ApplyResources(this.sliderBrightness, "sliderBrightness");
			this.sliderBrightness.TextColor = global::System.Drawing.Color.Black;
			this.sliderBrightness.Value = 0;
			this.sliderBrightness.ValueChanged += new global::System.EventHandler(this.sliderBrightness_ValueChanged);
			this.lblBrightnessContrastPadding1.Name = "lblBrightnessContrastPadding1";
			this.lblBrightnessContrastPadding1.Width = 5;
			this.itemContainerContrast.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerContrast.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerContrast.Name = "itemContainerContrast";
			this.itemContainerContrast.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblContrast,
				this.sliderContrast
			});
			this.itemContainerContrast.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblContrast.Name = "lblContrast";
			this.lblContrast.PaddingTop = 12;
			resources.ApplyResources(this.lblContrast, "lblContrast");
			this.lblContrast.TextAlignment = global::System.Drawing.StringAlignment.Center;
			this.sliderContrast.LabelPosition = global::DevComponents.DotNetBar.eSliderLabelPosition.Bottom;
			this.sliderContrast.Minimum = -100;
			this.sliderContrast.Name = "sliderContrast";
			resources.ApplyResources(this.sliderContrast, "sliderContrast");
			this.sliderContrast.TextColor = global::System.Drawing.Color.Black;
			this.sliderContrast.Value = 0;
			this.sliderContrast.ValueChanged += new global::System.EventHandler(this.sliderContrast_ValueChanged);
			this.ribbonBarBrightnessContrast.AutoOverflowEnabled = true;
			this.ribbonBarBrightnessContrast.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBrightnessContrast.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBrightnessContrast.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonBarBrightnessContrast, "ribbonBarBrightnessContrast");
			this.ribbonBarBrightnessContrast.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
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
			this.ribbonBarBrightnessContrast.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarBrightnessContrast.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarBrightnessContrast.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblBrightnessContrastPadding2.Name = "lblBrightnessContrastPadding2";
			this.lblBrightnessContrastPadding2.Width = 15;
			this.lblBrightnessContrastSeparator.BeginGroup = true;
			this.lblBrightnessContrastSeparator.Name = "lblBrightnessContrastSeparator";
			this.lblBrightnessContrastSeparator.Width = 5;
			this.lblBrightnessContrastPadding3.Name = "lblBrightnessContrastPadding3";
			this.lblBrightnessContrastPadding3.Width = 5;
			this.btBrightnessContrastApply.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrightnessContrastApply.Image = (global::System.Drawing.Image)resources.GetObject("btBrightnessContrastApply.Image");
			this.btBrightnessContrastApply.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btBrightnessContrastApply.Name = "btBrightnessContrastApply";
			resources.ApplyResources(this.btBrightnessContrastApply, "btBrightnessContrastApply");
			this.btBrightnessContrastApply.Click += new global::System.EventHandler(this.btBrightnessContrastApply_Click);
			this.btBrightnessContrastCancel.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrightnessContrastCancel.Image = (global::System.Drawing.Image)resources.GetObject("btBrightnessContrastCancel.Image");
			this.btBrightnessContrastCancel.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btBrightnessContrastCancel.Name = "btBrightnessContrastCancel";
			resources.ApplyResources(this.btBrightnessContrastCancel, "btBrightnessContrastCancel");
			this.btBrightnessContrastCancel.Click += new global::System.EventHandler(this.btBrightnessContrastCancel_Click);
			this.btBrightnessContrastRestore.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btBrightnessContrastRestore.Image = (global::System.Drawing.Image)resources.GetObject("btBrightnessContrastRestore.Image");
			this.btBrightnessContrastRestore.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btBrightnessContrastRestore.Name = "btBrightnessContrastRestore";
			this.btBrightnessContrastRestore.Stretch = true;
			resources.ApplyResources(this.btBrightnessContrastRestore, "btBrightnessContrastRestore");
			this.btBrightnessContrastRestore.Click += new global::System.EventHandler(this.btBrightnessContrastRestore_Click);
			this.panelBrightnessContrast.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelBrightnessContrast.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelBrightnessContrast.Controls.Add(this.ribbonBarBrightnessContrast);
			resources.ApplyResources(this.panelBrightnessContrast, "panelBrightnessContrast");
			this.panelBrightnessContrast.Name = "panelBrightnessContrast";
			this.panelBrightnessContrast.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelBrightnessContrast.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelBrightnessContrast.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelBrightnessContrast.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemSeparator;
			this.panelBrightnessContrast.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Top;
			this.panelBrightnessContrast.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelBrightnessContrast.Style.GradientAngle = 90;
			this.panelRotation.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelRotation.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelRotation.Controls.Add(this.ribbonBarRotation);
			resources.ApplyResources(this.panelRotation, "panelRotation");
			this.panelRotation.Name = "panelRotation";
			this.panelRotation.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelRotation.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelRotation.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelRotation.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemSeparator;
			this.panelRotation.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Top;
			this.panelRotation.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelRotation.Style.GradientAngle = 90;
			this.ribbonBarRotation.AutoOverflowEnabled = true;
			this.ribbonBarRotation.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarRotation.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarRotation.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonBarRotation, "ribbonBarRotation");
			this.ribbonBarRotation.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
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
			this.ribbonBarRotation.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarRotation.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarRotation.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.btFlipHorizontally.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btFlipHorizontally.Image = (global::System.Drawing.Image)resources.GetObject("btFlipHorizontally.Image");
			this.btFlipHorizontally.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btFlipHorizontally.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btFlipHorizontally.Name = "btFlipHorizontally";
			resources.ApplyResources(this.btFlipHorizontally, "btFlipHorizontally");
			this.btFlipHorizontally.Click += new global::System.EventHandler(this.btFlipHorizontally_Click);
			this.btFlipVertically.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btFlipVertically.Image = (global::System.Drawing.Image)resources.GetObject("btFlipVertically.Image");
			this.btFlipVertically.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btFlipVertically.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btFlipVertically.Name = "btFlipVertically";
			resources.ApplyResources(this.btFlipVertically, "btFlipVertically");
			this.btFlipVertically.Click += new global::System.EventHandler(this.btFlipVertically_Click);
			this.btRotateLeft.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btRotateLeft.Image = (global::System.Drawing.Image)resources.GetObject("btRotateLeft.Image");
			this.btRotateLeft.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btRotateLeft.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btRotateLeft.Name = "btRotateLeft";
			resources.ApplyResources(this.btRotateLeft, "btRotateLeft");
			this.btRotateLeft.Click += new global::System.EventHandler(this.btRotateLeft_Click);
			this.btRotateRight.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btRotateRight.Image = (global::System.Drawing.Image)resources.GetObject("btRotateRight.Image");
			this.btRotateRight.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btRotateRight.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btRotateRight.Name = "btRotateRight";
			resources.ApplyResources(this.btRotateRight, "btRotateRight");
			this.btRotateRight.Click += new global::System.EventHandler(this.btRotateRight_Click);
			this.lblBarRotationPadding1.Name = "lblBarRotationPadding1";
			this.lblBarRotationPadding1.Width = 10;
			this.lblBarRotationSeparator.BeginGroup = true;
			this.lblBarRotationSeparator.Name = "lblBarRotationSeparator";
			this.lblBarRotationSeparator.Width = 5;
			this.lblBarRotationPadding2.Name = "lblBarRotationPadding2";
			this.lblBarRotationPadding2.Width = 5;
			this.btRotationApply.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btRotationApply.Image = (global::System.Drawing.Image)resources.GetObject("btRotationApply.Image");
			this.btRotationApply.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btRotationApply.Name = "btRotationApply";
			resources.ApplyResources(this.btRotationApply, "btRotationApply");
			this.btRotationApply.Click += new global::System.EventHandler(this.btRotationApply_Click);
			this.btRotationCancel.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btRotationCancel.Image = (global::System.Drawing.Image)resources.GetObject("btRotationCancel.Image");
			this.btRotationCancel.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btRotationCancel.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btRotationCancel.Name = "btRotationCancel";
			resources.ApplyResources(this.btRotationCancel, "btRotationCancel");
			this.btRotationCancel.Click += new global::System.EventHandler(this.btRotationCancel_Click);
			this.webBrowser.AllowWebBrowserDrop = false;
			this.webBrowser.IsWebBrowserContextMenuEnabled = false;
			resources.ApplyResources(this.webBrowser, "webBrowser");
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.WebBrowserShortcutsEnabled = false;
			this.webBrowser.PreviewKeyDown += new global::System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowser_PreviewKeyDown);
			resources.ApplyResources(this.comboItem1, "comboItem1");
			resources.ApplyResources(this.comboItem2, "comboItem2");
			resources.ApplyResources(this.comboItem3, "comboItem3");
			resources.ApplyResources(this.comboItem4, "comboItem4");
			resources.ApplyResources(this.comboItem5, "comboItem5");
			resources.ApplyResources(this.comboItem6, "comboItem6");
			this.labelItem1.Name = "labelItem1";
			resources.ApplyResources(this.labelItem1, "labelItem1");
			this.panelWelcome.BackColor = global::System.Drawing.Color.Transparent;
			this.panelWelcome.Controls.Add(this.panelWelcomeMenu);
			resources.ApplyResources(this.panelWelcome, "panelWelcome");
			this.panelWelcome.Name = "panelWelcome";
			this.panelWelcome.Resize += new global::System.EventHandler(this.panelWelcome_Resize);
			this.panelWelcomeMenu.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelWelcomeMenu.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelWelcomeMenu.Controls.Add(this.lblRecentProjects);
			this.panelWelcomeMenu.Controls.Add(this.btNew);
			this.panelWelcomeMenu.Controls.Add(this.lstRecentProjects);
			this.panelWelcomeMenu.Controls.Add(this.btOpen);
			this.panelWelcomeMenu.Controls.Add(this.picWelcome);
			resources.ApplyResources(this.panelWelcomeMenu, "panelWelcomeMenu");
			this.panelWelcomeMenu.Name = "panelWelcomeMenu";
			this.panelWelcomeMenu.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelWelcomeMenu.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelWelcomeMenu.Style.BackColor2.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelWelcomeMenu.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelWelcomeMenu.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelWelcomeMenu.Style.BorderWidth = 10;
			this.panelWelcomeMenu.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelWelcomeMenu.Style.GradientAngle = 90;
			resources.ApplyResources(this.lblRecentProjects, "lblRecentProjects");
			this.lblRecentProjects.Name = "lblRecentProjects";
			this.lblRecentProjects.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btNew.AccessibleRole = global::System.Windows.Forms.AccessibleRole.PushButton;
			resources.ApplyResources(this.btNew, "btNew");
			this.btNew.AntiAlias = true;
			this.btNew.ColorTable = global::DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btNew.FocusCuesEnabled = false;
			this.btNew.Image = global::QuoterPlan.Properties.Resources.document_new;
			this.btNew.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btNew.Name = "btNew";
			this.btNew.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btNew.Click += new global::System.EventHandler(this.btProjectNew_Click);
			this.lstRecentProjects.AllowDrop = true;
			this.lstRecentProjects.AllowUserToResizeColumns = false;
			resources.ApplyResources(this.lstRecentProjects, "lstRecentProjects");
			this.lstRecentProjects.BackColor = global::System.Drawing.SystemColors.Window;
			this.lstRecentProjects.BackgroundStyle.CornerDiameter = 0;
			this.lstRecentProjects.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lstRecentProjects.BackgroundStyle.PaddingLeft = 1;
			this.lstRecentProjects.BackgroundStyle.PaddingRight = 2;
			this.lstRecentProjects.Columns.Add(this.columnHeader3);
			this.lstRecentProjects.Columns.Add(this.columnHeader4);
			this.lstRecentProjects.ColumnsVisible = false;
			this.lstRecentProjects.DragDropEnabled = false;
			this.lstRecentProjects.DragDropNodeCopyEnabled = false;
			this.lstRecentProjects.DropAsChildOffset = 0;
			this.lstRecentProjects.ExpandBorderColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.lstRecentProjects.ExpandButtonSize = new global::System.Drawing.Size(0, 0);
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
			this.lstRecentProjects.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.lstRecentProjects_KeyDown);
			this.columnHeader3.Name = "columnHeader3";
			this.columnHeader3.StretchToFill = true;
			resources.ApplyResources(this.columnHeader3, "columnHeader3");
			this.columnHeader3.Width.AutoSize = true;
			this.columnHeader4.Name = "columnHeader4";
			resources.ApplyResources(this.columnHeader4, "columnHeader4");
			this.columnHeader4.Width.Absolute = 150;
			this.nodeConnector4.LineColor = global::System.Drawing.SystemColors.ControlText;
			this.elementStyle5.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle5.Name = "elementStyle5";
			this.elementStyle5.TextColor = global::System.Drawing.SystemColors.ControlText;
			this.elementStyle6.BackColor2SchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground2;
			this.elementStyle6.BackColorGradientAngle = 90;
			this.elementStyle6.BackColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground;
			this.elementStyle6.BorderColor = global::System.Drawing.Color.DarkGray;
			this.elementStyle6.BorderLeftWidth = 1;
			this.elementStyle6.BorderRightWidth = 1;
			this.elementStyle6.BorderTopWidth = 1;
			this.elementStyle6.CornerDiameter = 4;
			this.elementStyle6.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle6.Description = "Yellow";
			this.elementStyle6.Name = "elementStyle6";
			this.elementStyle6.PaddingBottom = 1;
			this.elementStyle6.PaddingLeft = 1;
			this.elementStyle6.PaddingRight = 1;
			this.elementStyle6.PaddingTop = 1;
			this.elementStyle6.TextColor = global::System.Drawing.Color.Black;
			this.btOpen.AccessibleRole = global::System.Windows.Forms.AccessibleRole.PushButton;
			resources.ApplyResources(this.btOpen, "btOpen");
			this.btOpen.AntiAlias = true;
			this.btOpen.ColorTable = global::DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btOpen.FocusCuesEnabled = false;
			this.btOpen.Image = global::QuoterPlan.Properties.Resources.document_open;
			this.btOpen.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btOpen.Name = "btOpen";
			this.btOpen.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btOpen.Click += new global::System.EventHandler(this.btProjectOpen_Click);
			this.picWelcome.BackColor = global::System.Drawing.Color.White;
			this.picWelcome.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.picWelcome, "picWelcome");
			this.picWelcome.Name = "picWelcome";
			this.picWelcome.TabStop = false;
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.superTooltip.MinimumTooltipSize = new global::System.Drawing.Size(150, 50);
			this.panelPlansAction.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelPlansAction.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelPlansAction.Controls.Add(this.ribbonBarPlansAction);
			resources.ApplyResources(this.panelPlansAction, "panelPlansAction");
			this.panelPlansAction.Name = "panelPlansAction";
			this.panelPlansAction.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelPlansAction.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelPlansAction.Style.Border = global::DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelPlansAction.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemSeparator;
			this.panelPlansAction.Style.BorderSide = global::DevComponents.DotNetBar.eBorderSide.Top;
			this.panelPlansAction.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelPlansAction.Style.GradientAngle = 90;
			this.ribbonBarPlansAction.AutoOverflowEnabled = true;
			this.ribbonBarPlansAction.BackgroundMouseOverStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPlansAction.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPlansAction.ContainerControlProcessDialogKey = true;
			resources.ApplyResources(this.ribbonBarPlansAction, "ribbonBarPlansAction");
			this.ribbonBarPlansAction.HorizontalItemAlignment = global::DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
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
			this.ribbonBarPlansAction.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonBarPlansAction.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonBarPlansAction.TitleStyleMouseOver.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.progressPlansAction.Name = "progressPlansAction";
			this.progressPlansAction.ProgressBarType = global::DevComponents.DotNetBar.eCircularProgressType.Donut;
			resources.ApplyResources(this.progressPlansAction, "progressPlansAction");
			this.itemContainerExportType.BackgroundStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainerExportType.LayoutOrientation = global::DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainerExportType.Name = "itemContainerExportType";
			this.itemContainerExportType.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.lblBarPlansActionPadding3,
				this.checkBoxExportSingleFile,
				this.checkBoxExportMultiFiles
			});
			this.itemContainerExportType.TitleStyle.CornerType = global::DevComponents.DotNetBar.eCornerType.Square;
			this.lblBarPlansActionPadding3.Name = "lblBarPlansActionPadding3";
			resources.ApplyResources(this.checkBoxExportSingleFile, "checkBoxExportSingleFile");
			this.checkBoxExportSingleFile.CheckBoxStyle = global::DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.checkBoxExportSingleFile.Checked = true;
			this.checkBoxExportSingleFile.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxExportSingleFile.Name = "checkBoxExportSingleFile";
			resources.ApplyResources(this.checkBoxExportMultiFiles, "checkBoxExportMultiFiles");
			this.checkBoxExportMultiFiles.CheckBoxStyle = global::DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.checkBoxExportMultiFiles.Name = "checkBoxExportMultiFiles";
			this.lblBarPlansActionPadding4.Name = "lblBarPlansActionPadding4";
			this.lblBarPlansActionPadding4.Width = 10;
			this.btPlansActionSelectAll.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlansActionSelectAll.Image = global::QuoterPlan.Properties.Resources.select_all_32x32;
			this.btPlansActionSelectAll.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlansActionSelectAll.Name = "btPlansActionSelectAll";
			resources.ApplyResources(this.btPlansActionSelectAll, "btPlansActionSelectAll");
			this.btPlansActionSelectAll.Click += new global::System.EventHandler(this.btPlansActionSelectAll_Click);
			this.btPlansActionSelectNone.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlansActionSelectNone.Image = (global::System.Drawing.Image)resources.GetObject("btPlansActionSelectNone.Image");
			this.btPlansActionSelectNone.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlansActionSelectNone.Name = "btPlansActionSelectNone";
			resources.ApplyResources(this.btPlansActionSelectNone, "btPlansActionSelectNone");
			this.btPlansActionSelectNone.Click += new global::System.EventHandler(this.btPlansActionSelectNone_Click);
			this.lblBarPlansActionPadding1.Name = "lblBarPlansActionPadding1";
			this.lblBarPlansActionPadding1.Width = 10;
			this.lblPlansActionSeparator.BeginGroup = true;
			this.lblPlansActionSeparator.Name = "lblPlansActionSeparator";
			this.lblPlansActionSeparator.Width = 5;
			this.lblBarPlansActionPadding2.Name = "lblBarPlansActionPadding2";
			this.lblBarPlansActionPadding2.Width = 5;
			this.btPlansActionApply.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlansActionApply.Image = global::QuoterPlan.Properties.Resources.print_apply_40x40;
			this.btPlansActionApply.ImagePaddingHorizontal = 10;
			this.btPlansActionApply.ImagePaddingVertical = 5;
			this.btPlansActionApply.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlansActionApply.Name = "btPlansActionApply";
			resources.ApplyResources(this.btPlansActionApply, "btPlansActionApply");
			this.btPlansActionApply.Click += new global::System.EventHandler(this.btPlansActionApply_Click);
			this.btPlansActionCancel.ButtonStyle = global::DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btPlansActionCancel.Image = (global::System.Drawing.Image)resources.GetObject("btPlansActionCancel.Image");
			this.btPlansActionCancel.ImageListSizeSelection = global::DevComponents.DotNetBar.eButtonImageListSelection.Default;
			this.btPlansActionCancel.ImagePaddingHorizontal = 10;
			this.btPlansActionCancel.ImagePaddingVertical = 5;
			this.btPlansActionCancel.ImagePosition = global::DevComponents.DotNetBar.eImagePosition.Top;
			this.btPlansActionCancel.Name = "btPlansActionCancel";
			resources.ApplyResources(this.btPlansActionCancel, "btPlansActionCancel");
			this.btPlansActionCancel.Click += new global::System.EventHandler(this.btPlansActionCancel_Click);
			this.mainControl.BackColor = global::System.Drawing.SystemColors.Control;
			this.mainControl.Brightness = 0;
			this.mainControl.Contrast = 0;
			resources.ApplyResources(this.mainControl, "mainControl");
			this.mainControl.Name = "mainControl";
			this.mainControl.Origin = (global::System.Drawing.PointF)resources.GetObject("mainControl.Origin");
			this.mainControl.ScrollbarsVisible = true;
			this.mainControl.UseDynamicAdjustments = false;
			this.mainControl.Zoom = 100;
			this.mainControl.ZoomRestricted = false;
			this.mainControl.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.mainControl_MouseDown);
			this.mainControl.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.mainControl_MouseMove);
			this.mainControl.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.mainControl_MouseUp);
			this.mainControl.OnPaint += new global::QuoterPlanControls.PaintEventHandler(this.mainControl_Paint);
			this.mainControl.OnZoomChange += new global::QuoterPlanControls.ZoomChangeEventHandler(this.mainControl_OnZoomChange);
			this.mainControl.OnCacheLoaded += new global::QuoterPlanControls.ZoomChangeEventHandler(this.mainControl_OnCacheLoaded);
			this.mainControl.OnMouseWheel += new global::QuoterPlanControls.ZoomChangeEventHandler(this.mainControl_OnMouseWheel);
			this.mainControl.OnOriginChange += new global::QuoterPlanControls.OriginChangeEventHandler(this.mainControl_OnOriginChange);
			this.mainControl.Resize += new global::System.EventHandler(this.mainControl_Resize);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.F1);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlC);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlA);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlV);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlX);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlZ);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.CtrlY);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.Del);
			this.dotNetBarManager.AutoDispatchShortcuts.Add(global::DevComponents.DotNetBar.eShortcut.Ins);
			this.dotNetBarManager.BottomDockSite = this.dockSite4;
			this.dotNetBarManager.EnableFullSizeDock = false;
			this.dotNetBarManager.LeftDockSite = this.dockSite1;
			this.dotNetBarManager.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.dotNetBarManager.ParentForm = this;
			this.dotNetBarManager.RightDockSite = this.dockSite2;
			this.dotNetBarManager.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dotNetBarManager.ToolbarBottomDockSite = this.dockSite8;
			this.dotNetBarManager.ToolbarLeftDockSite = this.dockSite5;
			this.dotNetBarManager.ToolbarRightDockSite = this.dockSite6;
			this.dotNetBarManager.ToolbarTopDockSite = this.dockSite7;
			this.dotNetBarManager.TopDockSite = this.dockSite3;
			this.dotNetBarManager.UseGlobalColorScheme = true;
			this.dockSite4.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			resources.ApplyResources(this.dockSite4, "dockSite4");
			this.dockSite4.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer();
			this.dockSite4.Name = "dockSite4";
			this.dockSite4.TabStop = false;
			this.dockSite1.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite1.Controls.Add(this.containerBarLayers);
			this.dockSite1.Controls.Add(this.containerBarNavigation);
			this.dockSite1.Controls.Add(this.containerBarProperties);
			resources.ApplyResources(this.dockSite1, "dockSite1");
			this.dockSite1.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer(new global::DevComponents.DotNetBar.DocumentBaseContainer[]
			{
				new global::DevComponents.DotNetBar.DocumentBarContainer(this.containerBarNavigation, 285, 180),
				new global::DevComponents.DotNetBar.DocumentBarContainer(this.containerBarProperties, 285, 180),
				new global::DevComponents.DotNetBar.DocumentBarContainer(this.containerBarLayers, 285, 183)
			}, global::DevComponents.DotNetBar.eOrientation.Vertical);
			this.dockSite1.Name = "dockSite1";
			this.dockSite1.TabStop = false;
			resources.ApplyResources(this.containerBarLayers, "containerBarLayers");
			this.containerBarLayers.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.containerBarLayers.AutoSyncBarCaption = true;
			this.containerBarLayers.CanCustomize = false;
			this.containerBarLayers.CanDockBottom = false;
			this.containerBarLayers.CanDockTab = false;
			this.containerBarLayers.CanDockTop = false;
			this.containerBarLayers.CloseSingleTab = true;
			this.containerBarLayers.Controls.Add(this.panelDockLayers);
			this.containerBarLayers.GrabHandleStyle = global::DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.containerBarLayers.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItemLayers
			});
			this.containerBarLayers.LayoutType = global::DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.containerBarLayers.Name = "containerBarLayers";
			this.containerBarLayers.Stretch = true;
			this.containerBarLayers.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.containerBarLayers.TabStop = false;
			this.panelDockLayers.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelDockLayers.Controls.Add(this.lstLayers);
			this.panelDockLayers.Controls.Add(this.barLayers);
			resources.ApplyResources(this.panelDockLayers, "panelDockLayers");
			this.panelDockLayers.Name = "panelDockLayers";
			this.panelDockLayers.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockLayers.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelDockLayers.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelDockLayers.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelDockLayers.Style.GradientAngle = 90;
			this.dockContainerItemLayers.Control = this.panelDockLayers;
			this.dockContainerItemLayers.Name = "dockContainerItemLayers";
			resources.ApplyResources(this.dockContainerItemLayers, "dockContainerItemLayers");
			resources.ApplyResources(this.containerBarNavigation, "containerBarNavigation");
			this.containerBarNavigation.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.containerBarNavigation.AutoSyncBarCaption = true;
			this.containerBarNavigation.CanCustomize = false;
			this.containerBarNavigation.CanDockBottom = false;
			this.containerBarNavigation.CanDockTab = false;
			this.containerBarNavigation.CanDockTop = false;
			this.containerBarNavigation.CloseSingleTab = true;
			this.containerBarNavigation.Controls.Add(this.panelDockPreview);
			this.containerBarNavigation.GrabHandleStyle = global::DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.containerBarNavigation.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItemPreview
			});
			this.containerBarNavigation.LayoutType = global::DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.containerBarNavigation.Name = "containerBarNavigation";
			this.containerBarNavigation.Stretch = true;
			this.containerBarNavigation.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.containerBarNavigation.TabStop = false;
			this.panelDockPreview.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelDockPreview.Controls.Add(this.panControl);
			resources.ApplyResources(this.panelDockPreview, "panelDockPreview");
			this.panelDockPreview.Name = "panelDockPreview";
			this.panelDockPreview.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockPreview.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelDockPreview.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelDockPreview.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelDockPreview.Style.GradientAngle = 90;
			this.dockContainerItemPreview.Control = this.panelDockPreview;
			this.dockContainerItemPreview.Name = "dockContainerItemPreview";
			resources.ApplyResources(this.dockContainerItemPreview, "dockContainerItemPreview");
			resources.ApplyResources(this.containerBarProperties, "containerBarProperties");
			this.containerBarProperties.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.containerBarProperties.AutoSyncBarCaption = true;
			this.containerBarProperties.CanCustomize = false;
			this.containerBarProperties.CanDockBottom = false;
			this.containerBarProperties.CanDockTab = false;
			this.containerBarProperties.CanDockTop = false;
			this.containerBarProperties.CloseSingleTab = true;
			this.containerBarProperties.Controls.Add(this.panelDockProperties);
			this.containerBarProperties.GrabHandleStyle = global::DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.containerBarProperties.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItemProperties
			});
			this.containerBarProperties.LayoutType = global::DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.containerBarProperties.Name = "containerBarProperties";
			this.containerBarProperties.Stretch = true;
			this.containerBarProperties.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.containerBarProperties.TabStop = false;
			this.panelDockProperties.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelDockProperties.Controls.Add(this.barDisplayResults);
			this.panelDockProperties.Controls.Add(this.tabProperties);
			this.panelDockProperties.Controls.Add(this.cbObjects);
			resources.ApplyResources(this.panelDockProperties, "panelDockProperties");
			this.panelDockProperties.Name = "panelDockProperties";
			this.panelDockProperties.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockProperties.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelDockProperties.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelDockProperties.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelDockProperties.Style.GradientAngle = 90;
			this.panelDockProperties.Resize += new global::System.EventHandler(this.panelDockProperties_Resize);
			this.dockContainerItemProperties.Control = this.panelDockProperties;
			this.dockContainerItemProperties.Name = "dockContainerItemProperties";
			resources.ApplyResources(this.dockContainerItemProperties, "dockContainerItemProperties");
			this.dockSite2.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite2.Controls.Add(this.containerBarGroups);
			this.dockSite2.Controls.Add(this.containerBarRecentPlans);
			this.dockSite2.Controls.Add(this.containerBarEstimating);
			resources.ApplyResources(this.dockSite2, "dockSite2");
			this.dockSite2.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer(new global::DevComponents.DotNetBar.DocumentBaseContainer[]
			{
				new global::DevComponents.DotNetBar.DocumentDockContainer(new global::DevComponents.DotNetBar.DocumentBaseContainer[]
				{
					new global::DevComponents.DotNetBar.DocumentBarContainer(this.containerBarGroups, 147, 291),
					new global::DevComponents.DotNetBar.DocumentDockContainer(new global::DevComponents.DotNetBar.DocumentBaseContainer[]
					{
						new global::DevComponents.DotNetBar.DocumentBarContainer(this.containerBarRecentPlans, 179, 265)
					}, global::DevComponents.DotNetBar.eOrientation.Horizontal)
				}, global::DevComponents.DotNetBar.eOrientation.Vertical),
				new global::DevComponents.DotNetBar.DocumentBarContainer(this.containerBarEstimating, 180, 549)
			}, global::DevComponents.DotNetBar.eOrientation.Horizontal);
			this.dockSite2.Name = "dockSite2";
			this.dockSite2.TabStop = false;
			resources.ApplyResources(this.containerBarGroups, "containerBarGroups");
			this.containerBarGroups.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.containerBarGroups.AutoSyncBarCaption = true;
			this.containerBarGroups.CanCustomize = false;
			this.containerBarGroups.CanDockBottom = false;
			this.containerBarGroups.CanDockTab = false;
			this.containerBarGroups.CanDockTop = false;
			this.containerBarGroups.CloseSingleTab = true;
			this.containerBarGroups.Controls.Add(this.panelDockGroups);
			this.containerBarGroups.GrabHandleStyle = global::DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.containerBarGroups.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItemGroups
			});
			this.containerBarGroups.LayoutType = global::DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.containerBarGroups.Name = "containerBarGroups";
			this.containerBarGroups.Stretch = true;
			this.containerBarGroups.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.containerBarGroups.TabStop = false;
			this.panelDockGroups.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelDockGroups.Controls.Add(this.treeObjects);
			this.panelDockGroups.Controls.Add(this.panelPlanName);
			this.panelDockGroups.Controls.Add(this.barGroups);
			resources.ApplyResources(this.panelDockGroups, "panelDockGroups");
			this.panelDockGroups.Name = "panelDockGroups";
			this.panelDockGroups.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockGroups.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelDockGroups.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelDockGroups.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelDockGroups.Style.GradientAngle = 90;
			this.dockContainerItemGroups.Control = this.panelDockGroups;
			this.dockContainerItemGroups.Name = "dockContainerItemGroups";
			resources.ApplyResources(this.dockContainerItemGroups, "dockContainerItemGroups");
			resources.ApplyResources(this.containerBarRecentPlans, "containerBarRecentPlans");
			this.containerBarRecentPlans.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.containerBarRecentPlans.AutoSyncBarCaption = true;
			this.containerBarRecentPlans.CanCustomize = false;
			this.containerBarRecentPlans.CanDockBottom = false;
			this.containerBarRecentPlans.CanDockTab = false;
			this.containerBarRecentPlans.CanDockTop = false;
			this.containerBarRecentPlans.CloseSingleTab = true;
			this.containerBarRecentPlans.Controls.Add(this.panelDockRecentPlans);
			this.containerBarRecentPlans.GrabHandleStyle = global::DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.containerBarRecentPlans.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItemRecentPlans
			});
			this.containerBarRecentPlans.LayoutType = global::DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.containerBarRecentPlans.Name = "containerBarRecentPlans";
			this.containerBarRecentPlans.Stretch = true;
			this.containerBarRecentPlans.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.containerBarRecentPlans.TabStop = false;
			this.panelDockRecentPlans.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelDockRecentPlans.Controls.Add(this.lstRecentPlans);
			this.panelDockRecentPlans.Controls.Add(this.barRecentPlans);
			resources.ApplyResources(this.panelDockRecentPlans, "panelDockRecentPlans");
			this.panelDockRecentPlans.Name = "panelDockRecentPlans";
			this.panelDockRecentPlans.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockRecentPlans.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelDockRecentPlans.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelDockRecentPlans.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelDockRecentPlans.Style.GradientAngle = 90;
			this.dockContainerItemRecentPlans.Control = this.panelDockRecentPlans;
			this.dockContainerItemRecentPlans.Name = "dockContainerItemRecentPlans";
			resources.ApplyResources(this.dockContainerItemRecentPlans, "dockContainerItemRecentPlans");
			resources.ApplyResources(this.containerBarEstimating, "containerBarEstimating");
			this.containerBarEstimating.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.containerBarEstimating.AutoSyncBarCaption = true;
			this.containerBarEstimating.CanCustomize = false;
			this.containerBarEstimating.CanDockBottom = false;
			this.containerBarEstimating.CanDockTab = false;
			this.containerBarEstimating.CanDockTop = false;
			this.containerBarEstimating.CloseSingleTab = true;
			this.containerBarEstimating.Controls.Add(this.panelDockEstimating);
			this.containerBarEstimating.GrabHandleStyle = global::DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.containerBarEstimating.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItemEstimating
			});
			this.containerBarEstimating.LayoutType = global::DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.containerBarEstimating.Name = "containerBarEstimating";
			this.containerBarEstimating.Stretch = true;
			this.containerBarEstimating.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.containerBarEstimating.TabStop = false;
			this.panelDockEstimating.ColorSchemeStyle = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelDockEstimating.Controls.Add(this.treeEstimatingItems);
			this.panelDockEstimating.Controls.Add(this.barEstimatingItems);
			resources.ApplyResources(this.panelDockEstimating, "panelDockEstimating");
			this.panelDockEstimating.Name = "panelDockEstimating";
			this.panelDockEstimating.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockEstimating.Style.BackColor1.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelDockEstimating.Style.BorderColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelDockEstimating.Style.ForeColor.ColorSchemePart = global::DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelDockEstimating.Style.GradientAngle = 90;
			resources.ApplyResources(this.treeEstimatingItems, "treeEstimatingItems");
			this.treeEstimatingItems.LookAndFeel.SkinName = "Office 2010 Silver";
			this.treeEstimatingItems.Name = "treeEstimatingItems";
			this.treeEstimatingItems.OptionsView.FocusRectStyle = global::DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
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
			this.barEstimatingItems.ColorScheme.PredefinedColorScheme = global::DevComponents.DotNetBar.ePredefinedColorScheme.Silver2003;
			resources.ApplyResources(this.barEstimatingItems, "barEstimatingItems");
			this.barEstimatingItems.DockTabAlignment = global::DevComponents.DotNetBar.eTabStripAlignment.Left;
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
			this.barEstimatingItems.Style = global::DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barEstimatingItems.TabStop = false;
			this.btEstimatingItemsExpandAll.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingItemsExpandAll.Image");
			this.btEstimatingItemsExpandAll.Name = "btEstimatingItemsExpandAll";
			this.btEstimatingItemsExpandAll.Click += new global::System.EventHandler(this.btEstimatingItemsExpandAll_Click);
			this.btEstimatingItemsCollapseAll.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingItemsCollapseAll.Image");
			this.btEstimatingItemsCollapseAll.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btEstimatingItemsCollapseAll.Name = "btEstimatingItemsCollapseAll";
			this.btEstimatingItemsCollapseAll.Click += new global::System.EventHandler(this.btEstimatingItemsCollapseAll_Click);
			this.btEstimatingItemsUpdatePrices.BeginGroup = true;
			this.btEstimatingItemsUpdatePrices.Image = global::QuoterPlan.Properties.Resources.if_price_tag_usd_172529;
			this.btEstimatingItemsUpdatePrices.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btEstimatingItemsUpdatePrices.Name = "btEstimatingItemsUpdatePrices";
			this.btEstimatingItemsUpdatePrices.Click += new global::System.EventHandler(this.btEstimatingItemsUpdatePrices_Click);
			this.btEstimatingItemsPrint.Image = (global::System.Drawing.Image)resources.GetObject("btEstimatingItemsPrint.Image");
			this.btEstimatingItemsPrint.ImageFixedSize = new global::System.Drawing.Size(16, 16);
			this.btEstimatingItemsPrint.Name = "btEstimatingItemsPrint";
			this.btEstimatingItemsPrint.Visible = false;
			this.btEstimatingItemsPrint.Click += new global::System.EventHandler(this.btEstimatingItemsPrint_Click);
			this.dockContainerItemEstimating.Control = this.panelDockEstimating;
			this.dockContainerItemEstimating.Name = "dockContainerItemEstimating";
			resources.ApplyResources(this.dockContainerItemEstimating, "dockContainerItemEstimating");
			this.dockSite8.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			resources.ApplyResources(this.dockSite8, "dockSite8");
			this.dockSite8.Name = "dockSite8";
			this.dockSite8.TabStop = false;
			this.dockSite5.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			resources.ApplyResources(this.dockSite5, "dockSite5");
			this.dockSite5.Name = "dockSite5";
			this.dockSite5.TabStop = false;
			this.dockSite6.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			resources.ApplyResources(this.dockSite6, "dockSite6");
			this.dockSite6.Name = "dockSite6";
			this.dockSite6.TabStop = false;
			this.dockSite7.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			resources.ApplyResources(this.dockSite7, "dockSite7");
			this.dockSite7.Name = "dockSite7";
			this.dockSite7.TabStop = false;
			this.dockSite3.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			resources.ApplyResources(this.dockSite3, "dockSite3");
			this.dockSite3.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer();
			this.dockSite3.Name = "dockSite3";
			this.dockSite3.TabStop = false;
			this.lblNoPlan.BackColor = global::System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.lblNoPlan, "lblNoPlan");
			this.lblNoPlan.Name = "lblNoPlan";
			this.lblNoPlan.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.imageCollection.ImageStream = (global::DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection.ImageStream");
			this.imageCollection.Images.SetKeyName(0, "PrintDialog_16x16.png");
			this.imageCollection.Images.SetKeyName(1, "PrintViaPDF_16x16.png");
			this.imageCollection.Images.SetKeyName(2, "material_16x16.png");
			this.imageCollection.Images.SetKeyName(3, "labor_16x16.png");
			this.imageCollection.Images.SetKeyName(4, "equipment_16x16.png");
			this.imageCollection.Images.SetKeyName(5, "subcontract_16x16.png");
			this.imageCollection.Images.SetKeyName(6, "locked_16x16.png");
			this.imageCollection.Images.SetKeyName(7, "Folder_16x16.png");
			this.imageCollection.Images.SetKeyName(8, "FolderOpen_16x16_72.png");
			this.reportsControl.BackColor = global::System.Drawing.SystemColors.Control;
			this.reportsControl.Dirty = false;
			resources.ApplyResources(this.reportsControl, "reportsControl");
			this.reportsControl.Name = "reportsControl";
			this.dockContainerEstimating.Name = "dockContainerEstimating";
			resources.ApplyResources(this.dockContainerEstimating, "dockContainerEstimating");
			resources.ApplyResources(this.treeDBEstimatingItems, "treeDBEstimatingItems");
			this.treeDBEstimatingItems.LookAndFeel.SkinName = "Office 2010 Silver";
			this.treeDBEstimatingItems.Name = "treeDBEstimatingItems";
			this.treeDBEstimatingItems.OptionsBehavior.EnableFiltering = true;
			this.treeDBEstimatingItems.OptionsView.FocusRectStyle = global::DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
			this.treeDBEstimatingItems.OptionsView.ShowAutoFilterRow = true;
			this.treeDBEstimatingItems.OptionsView.ShowFilterPanelMode = global::DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
			this.treeDBEstimatingItems.OptionsView.ShowHorzLines = false;
			this.treeDBEstimatingItems.OptionsView.ShowVertLines = false;
			resources.ApplyResources(this.treeTemplatesLibrary, "treeTemplatesLibrary");
			this.treeTemplatesLibrary.LookAndFeel.SkinName = "Office 2010 Silver";
			this.treeTemplatesLibrary.Name = "treeTemplatesLibrary";
			this.treeTemplatesLibrary.OptionsBehavior.EnableFiltering = true;
			this.treeTemplatesLibrary.OptionsView.FocusRectStyle = global::DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
			this.treeTemplatesLibrary.OptionsView.ShowAutoFilterRow = true;
			this.treeTemplatesLibrary.OptionsView.ShowFilterPanelMode = global::DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
			this.treeTemplatesLibrary.OptionsView.ShowHorzLines = false;
			this.treeTemplatesLibrary.OptionsView.ShowVertLines = false;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
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
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			base.Shown += new global::System.EventHandler(this.MainForm_Shown);
			base.LocationChanged += new global::System.EventHandler(this.MainForm_LocationChanged);
			base.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			base.Resize += new global::System.EventHandler(this.MainForm_Resize);
			this.ribbonControl.ResumeLayout(false);
			this.ribbonControl.PerformLayout();
			this.ribbonPanelEstimating.ResumeLayout(false);
			this.ribbonPanelReport.ResumeLayout(false);
			this.ribbonPanel.ResumeLayout(false);
			this.ribbonPanelTemplates.ResumeLayout(false);
			this.ribbonPanelPlans.ResumeLayout(false);
			this.ribbonPanelExtensions.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.contextMenuBar1).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.barStatus).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.lstLayers).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.barLayers).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.barDisplayResults).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.tabProperties).EndInit();
			this.tabProperties.ResumeLayout(false);
			this.tabControlPanel1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.superTabProperties).EndInit();
			this.superTabProperties.ResumeLayout(false);
			this.superTabControlPanel1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.gridObjectProperties).EndInit();
			this.tabControlPanel2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.lstRecentPlans).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.barRecentPlans).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeObjects).EndInit();
			this.panelPlanName.ResumeLayout(false);
			this.panelPlanName.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.barGroups).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.timer1).EndInit();
			this.panelBrightnessContrast.ResumeLayout(false);
			this.panelRotation.ResumeLayout(false);
			this.panelWelcome.ResumeLayout(false);
			this.panelWelcomeMenu.ResumeLayout(false);
			this.panelWelcomeMenu.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.lstRecentProjects).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.picWelcome).EndInit();
			this.panelPlansAction.ResumeLayout(false);
			this.dockSite1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.containerBarLayers).EndInit();
			this.containerBarLayers.ResumeLayout(false);
			this.panelDockLayers.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.containerBarNavigation).EndInit();
			this.containerBarNavigation.ResumeLayout(false);
			this.panelDockPreview.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.containerBarProperties).EndInit();
			this.containerBarProperties.ResumeLayout(false);
			this.panelDockProperties.ResumeLayout(false);
			this.dockSite2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.containerBarGroups).EndInit();
			this.containerBarGroups.ResumeLayout(false);
			this.panelDockGroups.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.containerBarRecentPlans).EndInit();
			this.containerBarRecentPlans.ResumeLayout(false);
			this.panelDockRecentPlans.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.containerBarEstimating).EndInit();
			this.containerBarEstimating.ResumeLayout(false);
			this.panelDockEstimating.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.treeEstimatingItems).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.barEstimatingItems).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.imageCollection).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeDBEstimatingItems).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeTemplatesLibrary).EndInit();
			base.ResumeLayout(false);
		}

		private global::DevComponents.DotNetBar.RibbonControl ribbonControl;

		private global::DevComponents.DotNetBar.RibbonPanel ribbonPanel;

		private global::DevComponents.DotNetBar.RibbonTabItem ribbonTabStart;

		private global::DevComponents.DotNetBar.Bar barStatus;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarEdit;

		private global::DevComponents.DotNetBar.ButtonItem btEditPaste;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerEdit;

		private global::DevComponents.DotNetBar.ButtonItem btEditCut;

		private global::DevComponents.DotNetBar.ButtonItem btEditCopy;

		private global::DevComponents.DotNetBar.ButtonItem btEditDelete;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerUndoRedo;

		private global::DevComponents.DotNetBar.ButtonItem btEditUndo;

		private global::DevComponents.DotNetBar.ButtonItem btEditRedo;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarScale;

		private global::DevComponents.DotNetBar.ButtonItem btScaleSet;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarAnnotations;

		private global::DevComponents.DotNetBar.ButtonItem btMarkZone;

		private global::DevComponents.DotNetBar.ButtonItem btInsertNote;

		private global::DevComponents.DotNetBar.ButtonItem btInsertPicture;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarTools;

		private global::DevComponents.DotNetBar.ButtonItem btToolSelection;

		private global::DevComponents.DotNetBar.ButtonItem btToolPan;

		private global::DevComponents.DotNetBar.ButtonItem btToolArea;

		private global::DevComponents.DotNetBar.ButtonItem btToolPerimeter;

		private global::DevComponents.DotNetBar.ButtonItem btToolCounter;

		private global::DevComponents.DotNetBar.ButtonItem btToolAngle;

		private global::DevComponents.DotNetBar.ButtonItem btToolRuler;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarZoom;

		private global::DevComponents.DotNetBar.ButtonItem btZoomIn;

		private global::DevComponents.DotNetBar.ButtonItem btZoomOut;

		private global::DevComponents.DotNetBar.ButtonItem btZoomActualSize;

		private global::DevComponents.DotNetBar.ButtonItem btZoomToWindow;

		private global::DevComponents.DotNetBar.ButtonItem btZoomToSelection;

		private global::DevComponents.DotNetBar.ButtonItem btBookmarks;

		private global::DevComponents.DotNetBar.ButtonItem btZoomTo75;

		private global::DevComponents.DotNetBar.ButtonItem btZoomTo50;

		private global::DevComponents.DotNetBar.ButtonItem btZoomTo25;

		private global::DevComponents.DotNetBar.ButtonItem btZoomTo150;

		private global::DevComponents.DotNetBar.ButtonItem btZoomTo200;

		private global::DevComponents.DotNetBar.ButtonItem btStyleRetroGlass;

		private global::DevComponents.DotNetBar.ButtonItem buttonItem61;

		private global::DevComponents.DotNetBar.ButtonItem btModifyBookmarks;

		private global::QuoterPlanControls.PanControl panControl;

		private global::DevComponents.DotNetBar.Office2007StartButton startButton;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerFileMenu;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerFileMenu2;

		private global::DevComponents.DotNetBar.ButtonItem btProjectNew;

		private global::DevComponents.DotNetBar.ButtonItem btProjectOpen;

		private global::DevComponents.DotNetBar.ButtonItem btProjectSave;

		private global::DevComponents.DotNetBar.ButtonItem btProjectClose;

		private global::DevComponents.DotNetBar.GalleryContainer galleryRecentProjects;

		private global::DevComponents.DotNetBar.LabelItem lblFileRecentProjects;

		private global::DevComponents.DotNetBar.RibbonPanel ribbonPanelPlans;

		private global::DevComponents.DotNetBar.RibbonTabItem ribbonTabPlans;

		private global::DevComponents.DotNetBar.ButtonItem btProjectSaveAs;

		private global::DevComponents.DotNetBar.ButtonItem btHelpContent;

		private global::DevComponents.DotNetBar.ButtonItem btHelpYoutube;

		private global::DevComponents.DotNetBar.ButtonItem btHelpAbout;

		private global::DevComponents.DotNetBar.CheckBoxItem checkBoxItem1;

		private global::DevComponents.DotNetBar.SliderItem sliderItem1;

		private global::DevComponents.DotNetBar.SliderItem sliderItem2;

		private global::DevComponents.DotNetBar.CheckBoxItem checkBoxItem2;

		private global::DevComponents.AdvTree.AdvTree lstLayers;

		private global::DevComponents.AdvTree.ColumnHeader columnLayerVisible;

		private global::DevComponents.AdvTree.NodeConnector nodeConnector2;

		private global::DevComponents.AdvTree.ColumnHeader columnLayerName;

		private global::DevComponents.AdvTree.ColumnHeader columnLayerOpacity;

		private global::DevComponents.AdvTree.AdvTree treeObjects;

		private global::System.Timers.Timer timer1;

		private global::DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;

		private global::DevComponents.DotNetBar.ButtonItem bEditPopup;

		private global::DevComponents.DotNetBar.ButtonItem bCut;

		private global::DevComponents.DotNetBar.ButtonItem bCopy;

		private global::DevComponents.DotNetBar.ButtonItem bPaste;

		private global::DevComponents.DotNetBar.ButtonItem bSelectAll;

		private global::DevComponents.DotNetBar.ButtonItem bDelete;

		private global::DevComponents.DotNetBar.ButtonItem bSelectGroup;

		private global::DevComponents.DotNetBar.ButtonItem bDeductionCreate;

		private global::DevComponents.DotNetBar.ButtonItem bDeductionsEdit;

		private global::DevComponents.DotNetBar.ButtonItem bPointInsert;

		private global::DevComponents.DotNetBar.ButtonItem bGroupAddObject;

		private global::DevComponents.DotNetBar.ButtonItem bLayerMoveTo;

		private global::DevComponents.DotNetBar.ButtonItem bGroupMoveTo;

		private global::DevComponents.DotNetBar.ButtonItem bGroupMoveToNew;

		private global::DevComponents.DotNetBar.ButtonItem bPerimeterClose;

		private global::DevComponents.DotNetBar.ButtonItem bPerimeterOpen;

		private global::DevComponents.DotNetBar.ButtonItem bPerimeterCreateFromArea;

		private global::DevComponents.DotNetBar.ButtonItem bOpeningCreateFromSegment;

		private global::DevComponents.DotNetBar.ButtonItem bOpeningDelete;

		private global::DevComponents.DotNetBar.ButtonItem bOpeningCreateFromPosition;

		private global::DevComponents.DotNetBar.ButtonItem bAutoAdjustToZone;

		private global::DevComponents.DotNetBar.ButtonItem btScaleImperial;

		private global::DevComponents.DotNetBar.ButtonItem btScaleMetric;

		private global::DevComponents.DotNetBar.ButtonItem btScalePrecision64;

		private global::DevComponents.DotNetBar.ButtonItem btScalePrecision32;

		private global::DevComponents.DotNetBar.ButtonItem btScalePrecision16;

		private global::DevComponents.DotNetBar.ButtonItem btScalePrecision8;

		private global::DevComponents.DotNetBar.LabelItem lblPrecision;

		private global::DevComponents.DotNetBar.LabelItem lblSystemType;

		private global::DevComponents.DotNetBar.Controls.ComboBoxEx cbObjects;

		private global::DevComponents.AdvTree.ColumnHeader columnObjectIcon;

		private global::DevComponents.AdvTree.ColumnHeader columnObjectName;

		private global::DevComponents.AdvTree.ColumnHeader columnObjectInfo;

		private global::DevComponents.AdvTree.ColumnHeader columnObjectColor;

		private global::DevComponents.AdvTree.ColumnHeader columnObjectVisible;

		private global::DevComponents.AdvTree.ColumnHeader columnObjectPadding;

		private global::DevComponents.AdvTree.NodeConnector nodeConnector1;

		private global::DevComponents.DotNetBar.LabelItem lblStatusBarPadding;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle1;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle2;

		private global::DevComponents.DotNetBar.ButtonItem bSelectThisGroup;

		private global::DevComponents.DotNetBar.ButtonItem bSelectObjectType;

		private global::DevComponents.DotNetBar.ButtonItem bSelectThisGroup1;

		private global::DevComponents.DotNetBar.ButtonItem bSelectObjectType1;

		private global::DevComponents.DotNetBar.ButtonItem bUnselectAll;

		private global::DevComponents.DotNetBar.ButtonItem bLayerMoveTo1;

		private global::DevComponents.DotNetBar.ButtonItem bGroupMoveTo1;

		private global::DevComponents.DotNetBar.ItemContainer itemContainer5;

		private global::DevComponents.DotNetBar.LabelItem lblNoPerimeter;

		private global::DevComponents.DotNetBar.LabelItem lblNoCounter;

		private global::DevComponents.DotNetBar.LabelItem lblNoDistance;

		private global::DevComponents.DotNetBar.GalleryGroup galleryGroup1;

		private global::DevComponents.DotNetBar.GalleryGroup galleryGroupArea;

		private global::DevComponents.DotNetBar.GalleryGroup galleryGroupPerimeter;

		private global::DevComponents.DotNetBar.GalleryGroup galleryGroupCounter;

		private global::DevComponents.DotNetBar.LabelItem lblNoArea;

		private global::DevComponents.DotNetBar.LabelItem lblAreaGroups;

		private global::DevComponents.DotNetBar.GalleryContainer galleryAreaGroups;

		private global::DevComponents.DotNetBar.LabelItem lblAreaTemplates;

		private global::DevComponents.DotNetBar.GalleryContainer galleryAreaTemplates;

		private global::DevComponents.DotNetBar.GalleryContainer galleryPerimeterGroups;

		private global::DevComponents.DotNetBar.LabelItem lblPerimeterGroups;

		private global::DevComponents.DotNetBar.LabelItem lblPerimeterTemplates;

		private global::DevComponents.DotNetBar.GalleryContainer galleryPerimeterTemplates;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarBrowse;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerBrowse1;

		private global::DevComponents.DotNetBar.LabelItem lblBrowseGroup;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerBrowse3;

		private global::DevComponents.DotNetBar.ButtonItem btBrowsePrevious;

		private global::DevComponents.DotNetBar.ButtonItem btBrowseNext;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerBrowse2;

		private global::DevComponents.DotNetBar.LabelItem lblBrowseObjectType;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerBrowse4;

		private global::DevComponents.DotNetBar.ButtonItem btBrowseObjectTypePrevious;

		private global::DevComponents.DotNetBar.ButtonItem btBrowseObjectTypeNext;

		private global::DevComponents.DotNetBar.LabelItem lblCounterGroups;

		private global::DevComponents.DotNetBar.GalleryContainer galleryCounterGroups;

		private global::DevComponents.DotNetBar.LabelItem lblCounterTemplates;

		private global::DevComponents.DotNetBar.GalleryContainer galleryCounterTemplates;

		private global::DevComponents.DotNetBar.LabelItem lblDistanceGroups;

		private global::DevComponents.DotNetBar.GalleryContainer galleryDistanceGroups;

		private global::DevComponents.DotNetBar.LabelItem lblDistanceTemplates;

		private global::DevComponents.DotNetBar.GalleryContainer galleryDistanceTemplates;

		private global::DevComponents.DotNetBar.TabControl tabProperties;

		private global::DevComponents.DotNetBar.TabControlPanel tabControlPanel1;

		private global::DevComponents.DotNetBar.TabItem tabItem1;

		private global::DevComponents.DotNetBar.TabItem tabItem3;

		private global::DevComponents.DotNetBar.TabControlPanel tabControlPanel2;

		private global::DevComponents.DotNetBar.TabItem tabItem2;

		private global::QuoterPlanControls.MainControl mainControl;

		private global::QuoterPlan.ExtensionsManager extensionsManager;

		private global::DevComponents.DotNetBar.SuperTabControl superTabProperties;

		private global::DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;

		private global::DevComponents.DotNetBar.SuperTabItem superTabItem1;

		private global::DevComponents.DotNetBar.AdvPropertyGrid gridObjectProperties;

		private global::System.ComponentModel.BackgroundWorker backgroundWorker;

		private global::System.Windows.Forms.FlowLayoutPanel flowPlans;

		private global::DevComponents.AdvTree.AdvTree lstRecentPlans;

		private global::DevComponents.AdvTree.ColumnHeader columnHeader1;

		private global::DevComponents.AdvTree.ColumnHeader columnHeader2;

		private global::DevComponents.AdvTree.NodeConnector nodeConnector3;

		private global::DevComponents.DotNetBar.SliderItem sliderItem4;

		private global::DevComponents.DotNetBar.LabelItem lblImageQuality;

		private global::DevComponents.DotNetBar.SliderItem qualitySlider;

		private global::DevComponents.DotNetBar.LabelItem lblZoom;

		private global::DevComponents.DotNetBar.SliderItem zoomSlider;

		private global::DevComponents.DotNetBar.LabelItem lblStatus;

		private global::DevComponents.DotNetBar.LabelItem lblStatusPadding2;

		private global::DevComponents.DotNetBar.LabelItem lblStatusBarPadding3;

		private global::DevComponents.DotNetBar.ButtonItem btZoom50;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarPlans;

		private global::DevComponents.DotNetBar.ButtonItem btPlanRemove;

		private global::DevComponents.DotNetBar.ButtonItem btPlanExport;

		private global::DevComponents.DotNetBar.ButtonItem btPlanProperties;

		private global::QuoterPlan.LabelEx lblNoPlan;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarBrightnessContrast;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerBrightness;

		private global::DevComponents.DotNetBar.LabelItem lblBrightness;

		private global::DevComponents.DotNetBar.SliderItem sliderBrightness;

		private global::DevComponents.DotNetBar.LabelItem lblBrightnessContrastPadding1;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerContrast;

		private global::DevComponents.DotNetBar.LabelItem lblContrast;

		private global::DevComponents.DotNetBar.SliderItem sliderContrast;

		private global::DevComponents.DotNetBar.PanelEx panelBrightnessContrast;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarImage;

		private global::DevComponents.DotNetBar.ButtonItem btBrightnessContrast;

		private global::DevComponents.DotNetBar.ButtonItem btRotation;

		private global::DevComponents.DotNetBar.PanelEx panelRotation;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarRotation;

		private global::DevComponents.DotNetBar.ButtonItem btFlipHorizontally;

		private global::DevComponents.DotNetBar.ButtonItem btFlipVertically;

		private global::DevComponents.DotNetBar.ButtonItem btRotateLeft;

		private global::DevComponents.DotNetBar.ButtonItem btRotateRight;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarPlansInsert;

		private global::DevComponents.DotNetBar.ButtonItem btPlanInsertFromPDF;

		private global::DevComponents.DotNetBar.LabelItem iblImportDPI;

		private global::DevComponents.DotNetBar.CheckBoxItem op172Dpi;

		private global::DevComponents.DotNetBar.CheckBoxItem op300Dpi;

		private global::DevComponents.DotNetBar.CheckBoxItem opOtherDpi;

		private global::DevComponents.DotNetBar.SliderItem sliderDpi;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerDpi;

		private global::DevComponents.DotNetBar.LabelItem lblDpi1;

		private global::DevComponents.DotNetBar.LabelItem labelDpiPadding1;

		private global::DevComponents.DotNetBar.LabelItem lblDpi2;

		private global::DevComponents.DotNetBar.ButtonItem btPlanInsertFromImage;

		private global::DevComponents.DotNetBar.ButtonItem btPlanLoad;

		private global::System.Windows.Forms.WebBrowser webBrowser;

		private global::DevComponents.DotNetBar.RibbonPanel ribbonPanelReport;

		private global::DevComponents.DotNetBar.RibbonTabItem ribbonTabReport;

		private global::DevComponents.Editors.ComboItem comboItem1;

		private global::DevComponents.Editors.ComboItem comboItem2;

		private global::DevComponents.Editors.ComboItem comboItem3;

		private global::DevComponents.Editors.ComboItem comboItem4;

		private global::DevComponents.Editors.ComboItem comboItem5;

		private global::DevComponents.Editors.ComboItem comboItem6;

		private global::DevComponents.DotNetBar.LabelItem labelItem1;

		private global::DevComponents.DotNetBar.RibbonBar ribbonReportOrder;

		private global::DevComponents.DotNetBar.ButtonItem btReportFilter;

		private global::DevComponents.DotNetBar.RibbonBar ribbonExportReport;

		private global::DevComponents.DotNetBar.RibbonBar ribbonPrintReport;

		private global::DevComponents.DotNetBar.ButtonItem btReportPrint;

		private global::DevComponents.DotNetBar.ButtonItem btReportPrintPreview;

		private global::DevComponents.DotNetBar.ButtonItem btReportPrintSetup;

		private global::DevComponents.DotNetBar.ButtonItem btReportExportToXML;

		private global::DevComponents.DotNetBar.ButtonItem btReportExportToHTML;

		private global::DevComponents.DotNetBar.ButtonItem btReportExportToExcel;

		private global::DevComponents.DotNetBar.ButtonItem btReportExportToCSV;

		private global::DevComponents.DotNetBar.LabelItem lblReportSystemType;

		private global::DevComponents.DotNetBar.ButtonItem btReportScaleImperial;

		private global::DevComponents.DotNetBar.ButtonItem btReportScaleMetric;

		private global::DevComponents.DotNetBar.LabelItem lblReportPrecision;

		private global::DevComponents.DotNetBar.ButtonItem btReportScalePrecision64;

		private global::DevComponents.DotNetBar.ButtonItem btReportScalePrecision32;

		private global::DevComponents.DotNetBar.ButtonItem btReportScalePrecision16;

		private global::DevComponents.DotNetBar.ButtonItem btReportScalePrecision8;

		private global::DevComponents.DotNetBar.ButtonItem btZoom75;

		private global::DevComponents.DotNetBar.ButtonItem btZoom25;

		private global::DevComponents.DotNetBar.ButtonItem bAngleDegreeType;

		private global::DevComponents.DotNetBar.ButtonItem bAngleSlopeType;

		private global::DevComponents.DotNetBar.ButtonItem btLicensing;

		private global::DevComponents.DotNetBar.ButtonItem btLicenseDeactivate;

		private global::DevComponents.DotNetBar.ButtonItem btSave;

		private global::DevComponents.DotNetBar.ButtonItem btLicenseBuy;

		private global::DevComponents.DotNetBar.ButtonItem btLicenseActivate;

		private global::DevComponents.DotNetBar.ButtonItem btProjectInfo;

		private global::DevComponents.DotNetBar.ButtonItem btExit;

		private global::DevComponents.DotNetBar.ButtonItem btStyleMetro;

		private global::DevComponents.DotNetBar.ButtonItem btStyleClassicBlue;

		private global::DevComponents.DotNetBar.ButtonItem btStyleClassicSilver;

		private global::DevComponents.DotNetBar.ButtonItem btStyleClassicBlack;

		private global::DevComponents.DotNetBar.ButtonItem btStyleClassicExecutive;

		private global::DevComponents.DotNetBar.ButtonItem btStyleModern;

		private global::DevComponents.DotNetBar.ButtonItem btStyleRetroBlue;

		private global::DevComponents.DotNetBar.ButtonItem btStyleRetroBlack;

		private global::DevComponents.DotNetBar.ButtonItem btStyleRetroSilver;

		private global::DevComponents.DotNetBar.ColorPickerDropDown btSetThemeColor;

		private global::DevComponents.DotNetBar.Command AppCommandTheme;

		private global::System.Windows.Forms.Panel panelWelcome;

		private global::DevComponents.DotNetBar.PanelEx panelWelcomeMenu;

		private global::System.Windows.Forms.PictureBox picWelcome;

		private global::QuoterPlan.LabelEx lblRecentProjects;

		private global::DevComponents.AdvTree.AdvTree lstRecentProjects;

		private global::DevComponents.AdvTree.ColumnHeader columnHeader3;

		private global::DevComponents.AdvTree.ColumnHeader columnHeader4;

		private global::DevComponents.AdvTree.NodeConnector nodeConnector4;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle6;

		private global::DevComponents.AdvTree.ColumnHeader columnHeader5;

		private global::DevComponents.DotNetBar.ButtonItem btSettings;

		private global::DevComponents.DotNetBar.ButtonItem btLanguageEnglish;

		private global::DevComponents.DotNetBar.ButtonItem btLanguageFrench;

		private global::DevComponents.DotNetBar.ButtonItem btBrightnessContrastRestore;

		private global::DevComponents.DotNetBar.ButtonItem btRotationApply;

		private global::DevComponents.DotNetBar.ButtonItem btRotationCancel;

		private global::DevComponents.DotNetBar.ButtonItem btBrightnessContrastApply;

		private global::DevComponents.DotNetBar.ButtonItem btBrightnessContrastCancel;

		private global::DevComponents.DotNetBar.LabelItem lblBrightnessContrastPadding2;

		private global::DevComponents.DotNetBar.LabelItem lblBarRotationPadding1;

		private global::DevComponents.DotNetBar.LabelItem lblBarRotationSeparator;

		private global::DevComponents.DotNetBar.LabelItem lblBarRotationPadding2;

		private global::DevComponents.DotNetBar.LabelItem lblBrightnessContrastSeparator;

		private global::DevComponents.DotNetBar.LabelItem lblBrightnessContrastPadding3;

		private global::DevComponents.DotNetBar.SuperTooltip superTooltip;

		private global::DevComponents.DotNetBar.ButtonItem btHelp;

		private global::DevComponents.DotNetBar.LabelItem lblLanguage;

		private global::DevComponents.DotNetBar.LabelItem lblTheme;

		private global::DevComponents.DotNetBar.Bar barDisplayResults;

		private global::DevComponents.DotNetBar.LabelItem lblDisplayResults;

		private global::DevComponents.DotNetBar.ButtonItem btDisplayResultsForThisPlan;

		private global::DevComponents.DotNetBar.LabelItem lblDisplayResultsPadding;

		private global::DevComponents.DotNetBar.ButtonItem btDisplayResultsForAllPlans;

		private global::DevComponents.DotNetBar.Bar barLayers;

		private global::DevComponents.DotNetBar.ButtonItem btLayerAdd;

		private global::DevComponents.DotNetBar.ButtonItem btLayerRemove;

		private global::DevComponents.DotNetBar.ButtonItem btLayerRename;

		private global::DevComponents.DotNetBar.Bar barGroups;

		private global::DevComponents.DotNetBar.ButtonItem btGroupLocate;

		private global::DevComponents.DotNetBar.ButtonItem btGroupRemove;

		private global::DevComponents.DotNetBar.ButtonItem btGroupRename;

		private global::DevComponents.DotNetBar.ButtonItem btGroupSelect;

		private global::DevComponents.DotNetBar.ButtonItem bZoomToObject;

		private global::DevComponents.DotNetBar.ButtonItem bZoomToGroup;

		private global::DevComponents.DotNetBar.ButtonItem btZoomToObject;

		private global::DevComponents.DotNetBar.ButtonX btNew;

		private global::DevComponents.DotNetBar.ButtonX btOpen;

		private global::DevComponents.DotNetBar.LabelItem lblScrollSpeed;

		private global::DevComponents.DotNetBar.SliderItem sliderScrollSpeed;

		private global::DevComponents.DotNetBar.ItemContainer containerScroll;

		private global::DevComponents.DotNetBar.LabelItem lblScrollFast;

		private global::DevComponents.DotNetBar.LabelItem lblScrollPadding;

		private global::DevComponents.DotNetBar.LabelItem lblScrollSlow;

		private global::DevComponents.DotNetBar.ButtonItem bBringToFront;

		private global::DevComponents.DotNetBar.ButtonItem bSendToBack;

		private global::DevComponents.DotNetBar.LabelItem lblReportTheme;

		private global::DevComponents.DotNetBar.ButtonItem bEditNote;

		private global::DevComponents.DotNetBar.LabelItem lblDataFolder;

		private global::DevComponents.DotNetBar.ButtonItem btSelectDataFolder;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarPrintExport;

		private global::DevComponents.DotNetBar.ButtonItem btPrintPlan;

		private global::DevComponents.DotNetBar.LabelItem lblExportToExcelOptions;

		private global::DevComponents.DotNetBar.ButtonItem buttonItem1;

		private global::DevComponents.DotNetBar.ButtonItem btExportToExcelRawAndFormatted;

		private global::DevComponents.DotNetBar.ButtonItem btExportToExcelRawData;

		private global::DevComponents.DotNetBar.ButtonItem btExportToExcelFormattedData;

		private global::DevComponents.DotNetBar.ButtonItem btEditSendData;

		private global::DevComponents.DotNetBar.LabelItem lblStatusBarPadding2;

		private global::DevComponents.DotNetBar.SwitchButtonItem switchOrtho;

		private global::DevComponents.DotNetBar.LabelItem lblOrtho;

		private global::DevComponents.DotNetBar.ButtonItem btLayerOpenList;

		private global::DevComponents.DotNetBar.ButtonItem btLayerSaveList;

		private global::DevComponents.DotNetBar.ButtonItem btLayerSaveListAs;

		private global::DevComponents.DotNetBar.LabelItem lblPrintPlanOptions;

		private global::DevComponents.DotNetBar.ButtonItem buttonItem2;

		private global::DevComponents.DotNetBar.ButtonItem btPrintPlanFullSize;

		private global::DevComponents.DotNetBar.ButtonItem btPrintPlanWindow;

		private global::DevComponents.DotNetBar.ButtonItem btExportPlanToPDF;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarMultiPlans;

		private global::DevComponents.DotNetBar.ButtonItem btPlansExport;

		private global::DevComponents.DotNetBar.ButtonItem btPlansPrint;

		private global::DevComponents.DotNetBar.PanelEx panelPlansAction;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarPlansAction;

		private global::DevComponents.DotNetBar.ButtonItem btPlansActionSelectAll;

		private global::DevComponents.DotNetBar.ButtonItem btPlansActionSelectNone;

		private global::DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding4;

		private global::DevComponents.DotNetBar.LabelItem lblPlansActionSeparator;

		private global::DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding2;

		private global::DevComponents.DotNetBar.ButtonItem btPlansActionApply;

		private global::DevComponents.DotNetBar.ButtonItem btPlansActionCancel;

		private global::DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding1;

		private global::DevComponents.DotNetBar.CircularProgressItem progressPlansAction;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerExportType;

		private global::DevComponents.DotNetBar.LabelItem lblBarPlansActionPadding3;

		private global::DevComponents.DotNetBar.CheckBoxItem checkBoxExportSingleFile;

		private global::DevComponents.DotNetBar.CheckBoxItem checkBoxExportMultiFiles;

		private global::DevComponents.DotNetBar.CheckBoxItem opConvertToColor;

		private global::DevComponents.DotNetBar.LabelItem labelItem2;

		private global::DevComponents.DotNetBar.LabelItem iblImportColorManagement;

		private global::DevComponents.DotNetBar.LabelItem labelItem4;

		private global::DevComponents.DotNetBar.ButtonItem bSetHeight;

		private global::DevComponents.DotNetBar.ButtonItem bDeductionDuplicate;

		private global::DevComponents.DotNetBar.ButtonItem btUndo;

		private global::DevComponents.DotNetBar.ButtonItem btRedo;

		private global::DevComponents.DotNetBar.Bar barRecentPlans;

		private global::DevComponents.DotNetBar.ButtonItem btPlanRename;

		private global::DevComponents.DotNetBar.ButtonItem btRenamePlan;

		private global::System.Windows.Forms.Panel panelPlanName;

		private global::DevComponents.DotNetBar.Controls.ComboBoxEx cbPlans;

		private global::QuoterPlan.TextBoxEx txtPlanName;

		private global::DevComponents.DotNetBar.ButtonItem bOpeningDuplicate;

		private global::DevComponents.DotNetBar.ButtonItem btReportExportToEE;

		private global::DevComponents.DotNetBar.LabelItem lblAreaFilter;

		private global::DevComponents.DotNetBar.LabelItem lblPerimeterFilter;

		private global::DevComponents.DotNetBar.LabelItem lblDistanceFilter;

		private global::DevComponents.DotNetBar.TextBoxItem txtDistanceFilter;

		private global::DevComponents.DotNetBar.LabelItem lblCounterFilter;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerAreaFilter;

		private global::DevComponents.DotNetBar.ButtonItem btAreaFilterClear;

		private global::DevComponents.DotNetBar.TextBoxItem txtAreaFilter;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerPerimeterFilter;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerDistanceFilter;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerCounterFilter;

		private global::DevComponents.DotNetBar.ButtonItem btPerimeterFilterClear;

		private global::DevComponents.DotNetBar.ButtonItem btDistanceFilterClear;

		private global::DevComponents.DotNetBar.ButtonItem btCounterFilterClear;

		private global::DevComponents.DotNetBar.TextBoxItem txtCounterFilter;

		private global::DevComponents.DotNetBar.LabelItem lblAreaFilterPadding;

		private global::DevComponents.DotNetBar.TextBoxItem txtPerimeterFilter;

		private global::DevComponents.DotNetBar.ButtonItem btLayerMoveUp;

		private global::DevComponents.DotNetBar.ButtonItem btLayerMoveDown;

		private global::DevComponents.DotNetBar.ButtonItem bDropInsert;

		private global::DevComponents.DotNetBar.ButtonItem bPointRemove;

		private global::DevComponents.DotNetBar.ButtonItem bDropRemove;

		private global::DevComponents.DotNetBar.DockSite dockSite2;

		private global::DevComponents.DotNetBar.DockSite dockSite1;

		private global::DevComponents.DotNetBar.DockSite dockSite3;

		private global::DevComponents.DotNetBar.DockSite dockSite4;

		private global::DevComponents.DotNetBar.DockSite dockSite5;

		private global::DevComponents.DotNetBar.DockSite dockSite6;

		private global::DevComponents.DotNetBar.DockSite dockSite7;

		private global::DevComponents.DotNetBar.DockSite dockSite8;

		private global::DevComponents.DotNetBar.DotNetBarManager dotNetBarManager;

		private global::DevComponents.DotNetBar.RibbonBar ribbonBarLayout;

		private global::DevComponents.DotNetBar.ItemContainer itemContainerLayouts;

		private global::DevComponents.DotNetBar.CheckBoxItem opTakeoffLayout;

		private global::DevComponents.DotNetBar.CheckBoxItem opEstimatingLayout;

		private global::DevExpress.XtraTreeList.TreeList treeEstimatingItems;

		private global::DevComponents.DotNetBar.Bar barEstimatingItems;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingItemsPrint;

		private global::DevExpress.Utils.ImageCollection imageCollection;

		private global::QuoterPlan.ReportsControl reportsControl;

		private global::DevComponents.DotNetBar.ButtonItem btReportSettings;

		private global::DevComponents.DotNetBar.RibbonPanel ribbonPanelEstimating;

		private global::DevComponents.DotNetBar.RibbonTabItem ribbonTabEstimatingItems;

		private global::DevComponents.DotNetBar.RibbonPanel ribbonPanelTemplates;

		private global::DevComponents.DotNetBar.RibbonTabItemGroup ribbonTabItemDBManagement;

		private global::DevComponents.DotNetBar.RibbonTabItem ribbonTabTemplates;

		private global::DevComponents.DotNetBar.RibbonPanel ribbonPanelExtensions;

		private global::DevComponents.DotNetBar.RibbonTabItem ribbonTabExtensions;

		private global::DevComponents.DotNetBar.ButtonItem lblTrialMessage;

		private global::DevComponents.DotNetBar.RibbonBar ribbonTemplateCreate;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateArea;

		private global::DevComponents.DotNetBar.ButtonItem btTemplatePerimeter;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateLength;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateCounter;

		private global::DevComponents.DotNetBar.RibbonBar ribbonTemplate;

		private global::DevComponents.DotNetBar.RibbonBar ribbonTemplateDatabase;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateTradesPackages;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateCompactDatabase;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateModify;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateDelete;

		private global::DevComponents.DotNetBar.ButtonItem btTemplateDuplicate;

		private global::DevComponents.DotNetBar.RibbonBar ribbonExtension;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionModify;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionDuplicate;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionDelete;

		private global::DevComponents.DotNetBar.RibbonBar ribbonExtensionCreate;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionArea;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionPerimeter;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionRuler;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionCounter;

		private global::DevComponents.DotNetBar.RibbonBar ribbonExtensionDatabase;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionTradesPackages;

		private global::DevComponents.DotNetBar.ButtonItem btExtensionCompactDatabase;

		private global::DevComponents.DotNetBar.RibbonBar ribbonEstimatingDatabase;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingTradesPackages;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingCompactDatabase;

		private global::DevComponents.DotNetBar.RibbonBar ribbonEstimating;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingModify;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingDuplicate;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingDelete;

		private global::DevComponents.DotNetBar.RibbonBar ribbonEstimatingNew;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingMaterial;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingLabour;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingEquipment;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingSubcontract;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle7;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle8;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle5;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle3;

		private global::DevComponents.DotNetBar.ElementStyle elementStyle4;

		private global::DevComponents.DotNetBar.PanelDockContainer panelDockEstimating;

		private global::DevComponents.DotNetBar.PanelDockContainer panelDockGroups;

		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItemGroups;

		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItemEstimating;

		private global::DevComponents.DotNetBar.Bar containerBarRecentPlans;

		private global::DevComponents.DotNetBar.PanelDockContainer panelDockRecentPlans;

		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItemRecentPlans;

		private global::DevComponents.DotNetBar.Bar containerBarLayers;

		private global::DevComponents.DotNetBar.PanelDockContainer panelDockLayers;

		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItemLayers;

		private global::DevComponents.DotNetBar.Bar containerBarNavigation;

		private global::DevComponents.DotNetBar.PanelDockContainer panelDockPreview;

		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItemPreview;

		private global::DevComponents.DotNetBar.PanelDockContainer panelDockProperties;

		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItemProperties;

		private global::DevComponents.DotNetBar.DockContainerItem dockContainerEstimating;

		private global::DevComponents.DotNetBar.Bar containerBarGroups;

		private global::DevComponents.DotNetBar.Bar containerBarProperties;

		private global::DevComponents.DotNetBar.Bar containerBarEstimating;

		private global::DevExpress.XtraTreeList.TreeList treeDBEstimatingItems;

		private global::DevComponents.DotNetBar.ButtonItem btReportExportToPDF;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingItemsExpandAll;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingItemsCollapseAll;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingSaveDatabase;

		private global::DevComponents.DotNetBar.LabelItem lblPanels;

		private global::DevComponents.DotNetBar.ButtonItem btResetDefaultPanelsLayout;

		private global::DevComponents.DotNetBar.ButtonItem btPlanDuplicate;

		private global::DevComponents.DotNetBar.ButtonItem btLanguageSpanish;

		private global::DevComponents.DotNetBar.ButtonItem btReportExportToCOffice;

		private global::DevComponents.DotNetBar.ButtonItem bToggleMeasures;

		private global::DevComponents.DotNetBar.LabelItem lblPersonalPreferences;

		private global::DevComponents.DotNetBar.ButtonItem btPersonalPreferences;

		private global::DevExpress.XtraTreeList.TreeList treeTemplatesLibrary;

		private global::DevComponents.DotNetBar.ButtonItem btLayersToggle;

		private global::DevComponents.DotNetBar.ButtonItem btGroupsToggle;

		private global::DevComponents.DotNetBar.ButtonItem btLayersMakeVisible;

		private global::DevComponents.DotNetBar.ButtonItem btLayersMakeInvisible;

		private global::DevComponents.DotNetBar.ButtonItem btGroupsMakeVisible;

		private global::DevComponents.DotNetBar.ButtonItem btGroupsMakeInvisible;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingItemsUpdatePrices;

		private global::DevComponents.DotNetBar.ButtonItem btEstimatingImportPrices;

		private global::DevComponents.DotNetBar.ButtonItem btImportationPreferences;

		private global::DevComponents.DotNetBar.ButtonItem buttonItem3;

		private global::DevComponents.DotNetBar.ButtonItem btSetDBReadOnly;

		private global::DevComponents.DotNetBar.ButtonItem btEnableAutoBackup;
	}
}
