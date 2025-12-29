using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar.Rendering;
using DevComponents.Editors;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using PdfSharp.Pdf;
using QuoterPlan.Properties;
using QuoterPlanControls;

namespace QuoterPlan
{
	public partial class MainForm : Office2007RibbonForm
	{
		public ImageCollection ImageCollection
		{
			get
			{
				return this.imageCollection;
			}
		}

		public HelpUtilities HelpUtilities
		{
			get
			{
				return this.helpUtilities;
			}
		}

		public COfficeInterface COInterface
		{
			get
			{
				return this.coInterface;
			}
		}

		private void LicenseBuy()
		{
			Utilities.OpenDocument(Utilities.GetBuyUrl());
		}

		private string GetTurboActivateXmlFile()
		{
			string currentValidUICultureShort;
			if ((currentValidUICultureShort = Utilities.GetCurrentValidUICultureShort()) != null)
			{
				if (currentValidUICultureShort == "fr")
				{
					return "Français.xml";
				}
				if (currentValidUICultureShort == "es")
				{
					return "Español.xml";
				}
			}
			return "";
		}

		private string GetTurboActivateKey()
		{
			string result;
			try
			{
				result = this.TurboActivate.GetPKey();
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private void LaunchTurboActivate()
		{
			Process process = new Process
			{
				StartInfo = 
				{
					FileName = Path.Combine(Utilities.GetApplicationFolder(), "TurboActivate.exe"),
					Arguments = this.GetTurboActivateXmlFile()
				},
				EnableRaisingEvents = true
			};
			process.Exited += this.p_Exited;
			process.Start();
		}

		private void p_Exited(object sender, EventArgs e)
		{
			((Process)sender).Exited -= this.p_Exited;
			base.Invoke(new MainForm.IsActivatedDelegate(this.CheckIfActivated));
		}

		private void CheckIfActivated()
		{
			if (this.TurboActivate.IsActivated())
			{
				this.isActivated = true;
				this.ToggleTrialMode(false);
				return;
			}
			if (this.trialDaysRemaining == 0U)
			{
				base.Close();
			}
		}

		private void ToggleTrialMode(bool trialMode)
		{
			this.lblTrialMessage.Visible = trialMode;
			this.btLicensing.Visible = trialMode;
			this.btLicenseDeactivate.Visible = !trialMode;
			if (trialMode)
			{
				this.trialDaysRemaining = 0U;
				try
				{
					this.TurboActivate.UseTrial(TA_Flags.TA_SYSTEM | TA_Flags.TA_VERIFIED_TRIAL, null);
					this.trialDaysRemaining = this.TurboActivate.TrialDaysRemaining(TA_Flags.TA_SYSTEM | TA_Flags.TA_VERIFIED_TRIAL);
				}
				catch (TurboActivateException ex)
				{
					this.trialDaysRemaining = 0U;
					if (ex.Message.IndexOf("The verified trial has expired.") == -1)
					{
						Utilities.DisplayError(Resources.Incapable_de_valider_la_période_d_essai, ex.Message);
					}
				}
				switch (this.trialDaysRemaining)
				{
				case 0U:
					this.lblTrialMessage.Text = Resources.Votre_période_d_essai_est_terminée;
					break;
				case 1U:
					this.lblTrialMessage.Text = Resources.Il_ne_reste_qu_un_jour_à_votre_période_d_essai;
					break;
				default:
					this.lblTrialMessage.Text = string.Format(Resources.Votre_période_d_essai_se_termine_dans_n_jours, this.trialDaysRemaining);
					break;
				}
			}
			Application.DoEvents();
			this.Refresh();
		}

		private void LicenseDeactivate()
		{
			bool flag = false;
			string désactiver_votre_clé_de_produit = Resources.Désactiver_votre_clé_de_produit;
			string cela_aura_pour_effet_de_désactiver_votre_clé = Resources.Cela_aura_pour_effet_de_désactiver_votre_clé;
			if (Utilities.DisplayWarningQuestionCustom(désactiver_votre_clé_de_produit, cela_aura_pour_effet_de_désactiver_votre_clé, Resources.deactivate_48x48, Resources.Désactiver) == DialogResult.No)
			{
				return;
			}
			Application.DoEvents();
			this.LockInterface(ref flag);
			try
			{
				this.TurboActivate.Deactivate(false);
				this.isActivated = false;
				this.ToggleTrialMode(true);
			}
			catch (TurboActivateException ex)
			{
				Utilities.DisplayError(Resources.Impossible_de_désactiver_votre_clé_de_produit, ex.Message);
			}
			this.UnlockInterface(ref flag);
		}

		private void ValidateActivation(bool launchTurboActivate)
		{
			if (!this.isActivated && this.trialDaysRemaining <= 5U && (launchTurboActivate || this.trialDaysRemaining == 0U))
			{
				this.LaunchTurboActivate();
			}
		}

		public bool ValidateMultiplateInstances()
		{
			int num = 1;
			try
			{
				num = Utilities.ConvertToInt(this.TurboActivate.GetFeatureValue("NumberOfInstances"));
			}
			catch
			{
				num = 1;
			}
			num = ((num == 0) ? 1 : num);
			int num2 = Program.singleInstance.InstancesCount();
			Console.WriteLine("NumberOfPermittedInstances=" + num);
			Console.WriteLine("NumberOfCurrentInstances=" + num2);
			bool flag = num2 <= num;
			if (!flag)
			{
				string message = string.Empty;
				string installLanguage;
				if ((installLanguage = Utilities.GetInstallLanguage()) != null)
				{
					if (installLanguage == "fr")
					{
						message = string.Format("Nombre maximum d'instances atteint : {0}", num);
						goto IL_CC;
					}
					if (installLanguage == "es")
					{
						message = string.Format("Maximum number of instances reached: {0}", num);
						goto IL_CC;
					}
				}
				message = string.Format("Maximum number of instances reached: {0}", num);
				IL_CC:
				Utilities.DisplayError(Utilities.ApplicationName, message);
			}
			return flag;
		}

		private void InitializeTurboActivate()
		{
			this.TurboActivate = new TurboActivate("2b7d027d541e2ff9d9bbc0.18338856", "");
		}

		private void InitializeLicensing()
		{
			try
			{
				IsGenuineResult isGenuineResult = this.TurboActivate.IsGenuine(30U, 14U, true, false);
				this.isActivated = (isGenuineResult == IsGenuineResult.Genuine || isGenuineResult == IsGenuineResult.GenuineFeaturesChanged || isGenuineResult == IsGenuineResult.InternetError);
			}
			catch (TurboActivateException ex)
			{
				Utilities.DisplayError(Resources.Incapable_de_valider_l_activation, ex.Message);
			}
			this.ToggleTrialMode(!this.isActivated);
		}

		public bool ZoomRestricted
		{
			get
			{
				return this.mainControl.ZoomRestricted;
			}
			set
			{
				this.mainControl.ZoomRestricted = value;
				bool flag = this.mainControl.Image != null && !value;
				this.zoomSlider.Enabled = flag;
				this.lblZoom.ForeColor = (flag ? this.lblStatus.ForeColor : Color.DimGray);
				this.qualitySlider.Enabled = flag;
				this.lblImageQuality.ForeColor = (flag ? this.lblStatus.ForeColor : Color.DimGray);
			}
		}

		public bool ImageEditing
		{
			get
			{
				return this.imageEditing;
			}
			set
			{
				this.imageEditing = value;
				this.startButton.Enabled = !value;
				this.ribbonControl.Enabled = !value;
				this.ribbonPanel.Enabled = !value;
				this.ribbonTabPlans.Enabled = !value;
				this.ribbonTabReport.Enabled = !value;
				this.cbObjects.Enabled = (this.guiEnabled && !value);
				this.tabProperties.Enabled = (this.guiEnabled && !value);
				this.barLayers.Enabled = (this.guiEnabled && !value);
				this.lstLayers.Enabled = (this.guiEnabled && !value);
				this.barGroups.Enabled = (this.guiEnabled && !value);
				this.treeObjects.Enabled = (this.guiEnabled && !value);
				this.lstRecentPlans.Enabled = (this.guiEnabled && !value);
				this.btSave.Enabled = !value;
			}
		}

		public bool PlansSelection
		{
			get
			{
				return this.plansSelection;
			}
			set
			{
				this.plansSelection = value;
				this.plansNavigator.MultiSelectionMode = value;
				this.startButton.Enabled = !value;
				this.ribbonControl.Enabled = !value;
				this.ribbonPanelPlans.Enabled = !value;
				this.ribbonTabStart.Enabled = !value;
				this.ribbonTabReport.Enabled = !value;
				this.btSave.Enabled = !value;
			}
		}

		public bool ReportEditing
		{
			get
			{
				return this.reportEditing;
			}
			set
			{
				this.reportEditing = value;
				this.startButton.Enabled = !value;
				this.ribbonControl.Enabled = !value;
				this.ribbonPanel.Enabled = !value;
				this.ribbonTabPlans.Enabled = !value;
				this.ribbonTabReport.Enabled = !value;
				this.ribbonPanelReport.Enabled = !value;
				this.btSave.Enabled = !value;
			}
		}

		public bool InterfaceIsLock
		{
			get
			{
				return this.bInterfaceIsLock;
			}
		}

		public bool CacheOrImageReady
		{
			get
			{
				return !this.mainControl.DrawingBoard.CanUseAdaptiveCache || this.mainControl.CacheInUsed;
			}
		}

		private void PreloadInit()
		{
			this.ValidateSettings();
			if (Settings.Default.UpdateSettings)
			{
				this.UpdateSettings();
			}
			this.InitializeTurboActivate();
			this.InitializeUICulture();
			this.InitializeComponent();
			this.InitializeVersion();
			this.InitializeFonts();
			this.InitializeTooltips();
			this.InitializeTheme();
			this.SelectCultureInMenu(Settings.Default.UICulture);
			this.InitializeDragAndDrop();
			this.InitializeHelp();
		}

		private void ValidatePreferences()
		{
			if (Settings.Default.CompanyName == "")
			{
				this.DisplayPreferencesForm(false);
			}
		}

		private void InitializeDragAndDrop()
		{
			this.AllowDrop = true;
			base.DragEnter += this.MainForm_DragEnter;
			base.DragDrop += this.MainForm_DragDrop;
		}

		private void LayoutManager()
		{
			this.dotNetBarManager.DefinitionName = "";
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					e.Effect = DragDropEffects.Copy;
				}
			}
			catch
			{
			}
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				this.OpenFileFromArgs((string[])e.Data.GetData(DataFormats.FileDrop), 0);
			}
			catch
			{
			}
		}

		public bool OpenFileFromArgs(string[] args, int fileNamePosition)
		{
			string text = string.Empty;
			if (args.Length > fileNamePosition)
			{
				try
				{
					text = ((Path.GetExtension(args[fileNamePosition]).ToLower() == ".qpl") ? args[fileNamePosition] : string.Empty);
				}
				catch
				{
					text = string.Empty;
				}
			}
			if (text != string.Empty)
			{
				this.FileOpen(text);
			}
			return text != string.Empty;
		}

		private void InitializeSession()
		{
			if (this.OpenFileFromArgs(Environment.GetCommandLineArgs(), 1))
			{
				return;
			}
			this.FileWelcome(true);
		}

		private void StartUp()
		{
			this.ValidatePreferences();
			this.InitializeLicensing();
			this.InitializeData();
			this.InitializeGui();
			this.FileClose(false);
			this.InitializeSession();
			this.InitializeDockingWindows();
			Utilities.SetObjectFocus(this.lstRecentProjects);
		}

		public MainForm()
		{
			this.PreloadInit();
			using (AboutForm aboutForm = new AboutForm(true, this.GetTurboActivateKey()))
			{
				aboutForm.Show();
				Application.DoEvents();
				Thread.Sleep(2500);
				this.StartUp();
			}
			base.Focus();
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (Settings.Default.AllowMultipleInstances && !this.ValidateMultiplateInstances())
			{
				base.Close();
				return;
			}
			this.ValidateActivation(true);
			if (this.forceApplicationExit)
			{
				base.Close();
				return;
			}
			Utilities.SetObjectFocus(this.lstRecentProjects);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && !this.SaveIfModified())
			{
				e.Cancel = true;
				return;
			}
			if (this.project != null)
			{
				this.DisableBackup();
				this.DeleteBackup();
				this.project.Clear();
			}
			Utilities.DisposeFontResources();
			this.SaveDatabase();
			this.SaveApplicationSettings();
		}

		private void SetDBIsReadOnly(bool value)
		{
			Settings.Default.DBIsReadOnly = value;
			this.ribbonPanelEstimating.Enabled = !value;
			this.ribbonEstimatingDatabase.Enabled = !value;
			this.ribbonEstimatingNew.Enabled = !value;
			this.ribbonEstimating.Enabled = !value;
			this.treeDBEstimatingItems.Enabled = !value;
			this.btSetDBReadOnly.Checked = value;
		}

		private bool ValidateFormula(DrawObjectGroup group, string formula)
		{
			if (formula == "")
			{
				string name = group.Name;
				string la_formule_ne_peut_être_vide = Resources.La_formule_ne_peut_être_vide;
				Utilities.DisplayError(name, la_formule_ne_peut_être_vide);
				return false;
			}
			string str = "";
			if (!FormulaUtilities.ValidateFields(formula, group, ref str))
			{
				string name2 = group.Name;
				string message = Resources.No_value_associated_with + " " + str;
				Utilities.DisplayError(name2, message);
				return false;
			}
			GroupStats groupStats = new GroupStats(group.ObjectType);
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
			return FormulaUtilities.Compute(formula, group.Presets, groupStats, UnitScale.UnitSystem.undefined, ref num);
		}

		private bool ValidateFormulas()
		{
			foreach (object obj in this.project.Groups.Collection)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				foreach (CEstimatingItem cestimatingItem in drawObjectGroup.COfficeProducts.Collection)
				{
					if (!this.ValidateFormula(drawObjectGroup, cestimatingItem.Formula))
					{
						return false;
					}
				}
			}
			return true;
		}

		private void InitializeEstimatingItemsControl()
		{
			TabItem tabItem = this.tabProperties.CreateTab(Resources.Items_d_estimation);
			this.estimatingItemsControl = new CEstimatingsItemsControl(this.project);
			this.estimatingItemsControl.Name = "EstimatingItems";
			this.estimatingItemsControl.Dock = DockStyle.Fill;
			tabItem.AttachedControl.Controls.Add(this.estimatingItemsControl);
			this.SetDBIsReadOnly(Settings.Default.DBIsReadOnly);
		}

		private void ValidateDatabase()
		{
			Utilities.ValidateDirectory(Utilities.GetDBFolder(""));
			string text = Path.Combine(Utilities.GetDBFolder(""), Utilities.GetDefaultDBName());
			if (Utilities.FileExists(text))
			{
				return;
			}
			string sourceFileName = Path.Combine(Utilities.GetInstallFolder(), Utilities.GetDefaultDBName());
			Utilities.FileCopy(sourceFileName, text);
		}

		private void OpenDatabase()
		{
			this.ValidateDatabase();
			this.dbManagement.Open(Path.Combine(Utilities.GetDBFolder(""), Utilities.GetDefaultDBName()));
			this.DatabaseWasModified(false);
		}

		private void SaveDatabase()
		{
			if (Settings.Default.DBIsReadOnly)
			{
				return;
			}
			string text = Path.Combine(Utilities.GetDBFolder(""), Utilities.GetDefaultDBName());
			this.BackupDatabase(text);
			this.dbManagement.Save(text);
			this.DatabaseWasModified(false);
		}

		private void BackupDatabase(string dbFileName)
		{
			if (!Utilities.FileExists(dbFileName))
			{
				return;
			}
			string localDBFolder = Utilities.GetLocalDBFolder();
			string dateString = Utilities.GetDateString(Utilities.FormatDateLong(DateTime.Now), Utilities.GetCurrentValidUICultureShort()).Replace(",", "").Replace(" ", "-");
			string backupDBFileName = Utilities.GetBackupDBFileName(dateString);
			Utilities.ValidateDirectory(localDBFolder);
			Utilities.FileCopy(dbFileName, backupDBFileName);
			this.CleanUpOldBackups(localDBFolder);
		}

		private void CleanUpOldBackups(string backupFolder)
		{
			int num = 0;
			DirectoryInfo directoryInfo = new DirectoryInfo(backupFolder);
			FileInfo[] array = (from p in directoryInfo.GetFiles("*.dat.bak")
			orderby p.CreationTime descending
			select p).ToArray<FileInfo>();
			foreach (FileInfo fileInfo in array)
			{
				num++;
				if (num > 10)
				{
					Utilities.FileDelete(fileInfo.FullName, true);
				}
			}
		}

		private bool ValidateImportSettings()
		{
			return Settings.Default.ImportDBPricesSkuPosition != 0 && Settings.Default.ImportDBPricesPricePosition != 0 && Settings.Default.ImportDBPricesSeparator != ' ' && !(Settings.Default.ImportDBPricesFileName == "") && Utilities.FileExists(Settings.Default.ImportDBPricesFileName);
		}

		private bool EditImportSettings()
		{
			using (ImportSettingsForm importSettingsForm = new ImportSettingsForm())
			{
				importSettingsForm.HelpUtilities = this.HelpUtilities;
				importSettingsForm.HelpContextString = "ImportSettingsForm";
				importSettingsForm.ShowDialog(this);
			}
			return this.ValidateImportSettings();
		}

		private void ImportPrices()
		{
			if (!this.ValidateImportSettings() && !this.EditImportSettings())
			{
				return;
			}
			string importDBPricesFileName = Settings.Default.ImportDBPricesFileName;
			Hashtable hashtable = new Hashtable();
			foreach (object obj in this.dbManagement.EstimatingItems.Collection)
			{
				DBEstimatingItem dbestimatingItem = (DBEstimatingItem)((DictionaryEntry)obj).Value;
				if (dbestimatingItem.BidCode != "" && !hashtable.ContainsKey(dbestimatingItem.BidCode))
				{
					hashtable.Add(dbestimatingItem.BidCode, dbestimatingItem);
				}
			}
			string text = Utilities.ReadToString(importDBPricesFileName);
			if (text != string.Empty)
			{
				string[] fields = Utilities.GetFields(text, new char[]
				{
					'\r',
					'\n'
				});
				foreach (string originalString in fields)
				{
					string[] fields2 = Utilities.GetFields(originalString, Settings.Default.ImportDBPricesSeparator);
					string text2 = Utilities.GetField(fields2, Settings.Default.ImportDBPricesSkuPosition - 1).ToString();
					if (text2 != "" && hashtable.ContainsKey(text2))
					{
						double num = Utilities.ConvertCurrency(Utilities.GetField(fields2, Settings.Default.ImportDBPricesPricePosition - 1).ToString(), 0.0);
						DBEstimatingItem dbestimatingItem2 = (DBEstimatingItem)hashtable[text2];
						if (dbestimatingItem2 != null)
						{
							dbestimatingItem2.PriceEach = num;
						}
						Console.WriteLine("itemPrice = " + num);
					}
				}
			}
			this.dbEstimatingItemsEditor.Refresh();
			this.DatabaseWasModified(true);
		}

		private void DatabaseWasModified(bool dbWasModified)
		{
			this.btEstimatingSaveDatabase.Enabled = dbWasModified;
		}

		private void InitializeDBEstimatingItemsEditor()
		{
			this.ribbonTabEstimatingItems.Visible = true;
			this.dbEstimatingItemsEditor = new DBEstimatingItemsEditor(this, this.dbManagement, this.treeDBEstimatingItems, this.imageCollection);
			this.dbEstimatingItemsEditor.OnSelected += this.dbEstimatingItemsEditor_OnSelected;
			this.dbEstimatingItemsEditor.OnDBItemCreated += this.dbEstimatingItemsEditor_OnDBItemCreated;
			this.dbEstimatingItemsEditor.OnDBItemModified += this.dbEstimatingItemsEditor_OnDBItemModified;
			this.dbEstimatingItemsEditor.OnDBItemDeleted += this.dbEstimatingItemsEditor_OnDBItemDeleted;
			this.dbEstimatingItemsEditor_OnSelected(this, new EventArgs());
		}

		private void InitializeTemplatesEditor()
		{
			this.ribbonTabEstimatingItems.Visible = true;
			this.templatesLibraryEditor = new TemplatesLibraryEditor(this, this.templatesSupport, this.treeTemplatesLibrary, this.imageCollection);
			this.templatesLibraryEditor.OnCreateArea += this.templatesLibraryEditor_OnCreateArea;
			this.templatesLibraryEditor.OnCreatePerimeter += this.templatesLibraryEditor_OnCreatePerimeter;
			this.templatesLibraryEditor.OnCreateLength += this.templatesLibraryEditor_OnCreateLength;
			this.templatesLibraryEditor.OnCreateCounter += this.templatesLibraryEditor_OnCreateCounter;
			this.templatesLibraryEditor.OnSelected += this.templatesLibraryEditor_OnSelected;
			this.templatesLibraryEditor.OnModify += this.templatesLibraryEditor_OnModify;
			this.templatesLibraryEditor.OnDuplicate += this.templatesLibraryEditor_OnDuplicate;
			this.templatesLibraryEditor_OnSelected(this, new EventArgs());
		}

		private void InitializeDBManagement()
		{
			this.dbManagement = new DBManagement();
			this.OpenDatabase();
		}

		public void SetModified()
		{
			this.project.Dirty = true;
			this.project.LastModified = Utilities.FormatDate(DateTime.Now);
			this.reportsControl.Dirty = true;
			this.drawArea.UpdateLegend();
			this.UpdateTileBar();
			this.UpdateStatusBar(MainForm.ApplicationStatusEnum.StatusModified);
			if (Settings.Default.EnableAutoBackup)
			{
				this.EnableBackup();
			}
		}

		public void UpdateObject(DrawObject drawObject)
		{
			Console.WriteLine("UpdateObject");
			if (drawObject != null)
			{
				this.objectEditor.ForceSelection(drawObject);
				this.objectsNavigator.Refresh(drawObject);
				this.estimatingEditor.Refresh(drawObject);
			}
			else
			{
				this.RefreshObjects();
			}
			this.EnableEditCommands(true);
		}

		public void RefreshObject(DrawObject drawObject)
		{
			if (this.objectEditor.SelectedObject != null && drawObject.HasSameGroupOrID(this.objectEditor.SelectedObject))
			{
				this.objectEditor.Refresh();
			}
			this.objectsNavigator.Refresh(drawObject);
			this.estimatingEditor.Refresh(drawObject);
		}

		public void RefreshObjects()
		{
			this.objectEditor.Refresh();
			this.objectsNavigator.RefreshAll();
			this.estimatingEditor.RefreshAll();
		}

		private bool GroupExistsInGUI(DrawObject drawObject)
		{
			for (int i = 0; i < this.cbObjects.Items.Count; i++)
			{
				Utilities.DisplayName displayName = (Utilities.DisplayName)this.cbObjects.Items[i];
				if (((DrawObject)displayName.Owner).ID == drawObject.ID || (((DrawObject)displayName.Owner).GroupID == drawObject.GroupID && drawObject.IsPartOfGroup()))
				{
					return true;
				}
			}
			return false;
		}

		public void AddObjectToGUI(int layerIndex, DrawObject drawObject, bool forceSelection)
		{
			if (drawObject == null)
			{
				return;
			}
			if (drawObject.DeductionParentID != -1)
			{
				drawObject = this.drawArea.DeductionParent;
				if (drawObject != null)
				{
					drawObject.Selected = true;
					this.drawArea.DeductionParentRelease();
					this.UpdateObject(drawObject);
					this.drawArea.Refresh();
				}
			}
			if (drawObject != null)
			{
				if (!this.GroupExistsInGUI(drawObject))
				{
					this.objectEditor.Add(drawObject, forceSelection);
					this.objectsNavigator.AddObject(layerIndex, drawObject, forceSelection);
					this.estimatingEditor.RefreshAll();
				}
				else if (forceSelection)
				{
					this.objectEditor.ForceSelection(drawObject);
				}
			}
			this.drawArea.FlagDeletedGroups();
			this.EnableEditCommands(true);
			this.SetModified();
		}

		public void AddLayerToGUI(Layer layer, int layerIndex)
		{
			this.layersNavigator.Add(layer, layerIndex, true);
			this.objectsNavigator.AddLayer(layer, layerIndex);
		}

		public void SelectLayerInGUI(int layerIndex)
		{
			this.exitNow = true;
			this.layersNavigator.ForceSelection(layerIndex);
			this.exitNow = false;
		}

		public void ReplaceObjectInGUI(DrawObject drawObject)
		{
			this.objectEditor.ReplaceObject(drawObject);
			this.objectsNavigator.ReplaceObject(drawObject);
			this.estimatingEditor.RefreshAll();
		}

		public void ReloadObjectsInGUI()
		{
			this.FillPlanObjects(this.drawArea.ActivePlan);
			this.drawArea.FlagDeletedGroups();
		}

		public void ReloadLayersInGUI(int layerIndex)
		{
			this.FillPlanLayers(this.drawArea.ActivePlan, layerIndex);
		}

		private void LoadResources()
		{
			this.btHelpAbout.Text = Utilities.À_propos_de_Quoter_Plan1;
			LabelItem labelItem = this.lblAreaGroups;
			labelItem.Text = labelItem.Text + "<br/>" + Resources.ou_ctrl_cliquez_pour_créer_un_nouveau_groupe;
			LabelItem labelItem2 = this.lblPerimeterGroups;
			labelItem2.Text = labelItem2.Text + "<br/>" + Resources.ou_ctrl_cliquez_pour_créer_un_nouveau_groupe;
			LabelItem labelItem3 = this.lblDistanceGroups;
			labelItem3.Text = labelItem3.Text + "<br/>" + Resources.ou_ctrl_cliquez_pour_créer_un_nouveau_groupe;
			LabelItem labelItem4 = this.lblCounterGroups;
			labelItem4.Text = labelItem4.Text + "<br/>" + Resources.ou_ctrl_cliquez_pour_créer_un_nouveau_groupe;
			base.Icon = Resources.active_takeoff_icon;
			this.btHelpYoutube.Visible = false;
			this.picWelcome.BackColor = Color.FromArgb(221, 229, 241);
			this.picWelcome.BorderStyle = BorderStyle.FixedSingle;
			this.picWelcome.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picWelcome.Left = 24;
			this.picWelcome.Top = 24;
			this.picWelcome.Width = 358;
			this.picWelcome.Height = 127;
			this.panelWelcomeMenu.Height = 484;
			this.lblLanguage.Visible = false;
			this.btLanguageEnglish.Visible = false;
			this.btLanguageFrench.Visible = false;
			this.btLanguageSpanish.Visible = false;
		}

		private void InitializeUICulture()
		{
			Settings.Default.UICulture = this.SetCulture(Settings.Default.UICulture, false);
			if (Thread.CurrentThread.CurrentUICulture.Name != Settings.Default.UICulture)
			{
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.UICulture);
			}
		}

		private void InitializeVersion()
		{
			this.ribbonTabExtensions.Visible = false;
		}

		private void InitializeFonts()
		{
			this.Font = Utilities.GetDefaultFont();
			this.barStatus.Font = Utilities.GetDefaultFont();
			this.btLicensing.PopupFont = Utilities.GetDefaultFont();
			this.ribbonBarZoom.Font = Utilities.GetDefaultFont();
			this.tabProperties.SelectedTabFont = Utilities.GetDefaultFont(FontStyle.Bold);
			this.superTabProperties.Font = Utilities.GetDefaultFont();
			this.superTabProperties.TabFont = Utilities.GetDefaultFont();
			this.superTabProperties.SelectedTabFont = Utilities.GetDefaultFont(FontStyle.Bold);
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.CaptionFont = Utilities.GetDefaultFont();
			this.ribbonControl.RibbonStripFont = Utilities.GetDefaultFont();
			this.barDisplayResults.Font = Utilities.GetDefaultFont();
			this.ribbonControl.Font = Utilities.GetDefaultFont();
			this.ribbonBarBrightnessContrast.Font = Utilities.GetDefaultFont();
			this.ribbonBarRotation.Font = Utilities.GetDefaultFont();
			this.ribbonBarPlansAction.Font = Utilities.GetDefaultFont();
			this.superTooltip.DefaultFont = Utilities.GetDefaultFont();
			this.contextMenuBar1.Font = Utilities.GetDefaultFont();
			this.lstLayers.Font = Utilities.GetDefaultFont();
			this.treeObjects.Font = Utilities.GetDefaultFont();
			this.lstRecentPlans.Font = Utilities.GetDefaultFont();
			this.treeEstimatingItems.Font = Utilities.GetDefaultFont();
			this.cbObjects.Font = Utilities.GetDefaultFont();
			this.cbPlans.Font = Utilities.GetDefaultFont();
			this.txtPlanName.Font = Utilities.GetDefaultFont();
			this.ribbonControl.Height = 172;
			if (this.ribbonControl.Font.Name == "Segoe UI")
			{
				this.panelBrightnessContrast.Height = 97;
				this.panelRotation.Height = 97;
				this.panelPlansAction.Height = 110;
				return;
			}
			this.panelBrightnessContrast.Height = 89;
			this.panelRotation.Height = 89;
			this.panelPlansAction.Height = 108;
		}

		private void SetTooltip(IComponent component, string headerText, string shortCut, string bodyText, bool showHelpFooter, int size = 230)
		{
			bool headerVisible = true;
			headerText += ((shortCut != string.Empty) ? (" " + shortCut) : string.Empty);
			string footerText = showHelpFooter ? Resources.Appuyer_sur_F1_pour_obtenir_de_l_aide : string.Empty;
			Image bodyImage = null;
			Image footerImage = showHelpFooter ? Resources.help_16x16 : null;
			Size customSize = showHelpFooter ? default(Size) : new Size(size, 0);
			this.superTooltip.SetSuperTooltip(component, new SuperTooltipInfo(headerText, footerText, bodyText, bodyImage, footerImage, eTooltipColor.Default, headerVisible, showHelpFooter, customSize));
		}

		private void InitializeTooltips()
		{
			this.SetTooltip(this.btEditPaste, Resources.Coller, "(Ctrl+V)", Resources.Coller1, false, 230);
			this.SetTooltip(this.btEditCut, Resources.Couper, "(Ctrl+X)", Resources.Couper1, false, 230);
			this.SetTooltip(this.btEditCopy, Resources.Copier, "(Ctrl+C)", Resources.Copier1, false, 230);
			this.SetTooltip(this.btEditDelete, Resources.Supprimer, "(Del)", Resources.Supprimer1, false, 230);
			this.SetTooltip(this.btEditUndo, Resources.Annuler, "(Ctrl+Z)", Resources.Annuler1, false, 230);
			this.SetTooltip(this.btEditRedo, Resources.Refaire, "(Ctrl+Y)", Resources.Refaire1, false, 230);
			this.SetTooltip(this.btUndo, Resources.Annuler, "(Ctrl+Z)", Resources.Annuler1, false, 230);
			this.SetTooltip(this.btRedo, Resources.Refaire, "(Ctrl+Y)", Resources.Refaire1, false, 230);
			this.SetTooltip(this.btEditSendData, Resources.EnvoyerDonnées, "", Utilities.EnvoyerDonnées1, true, 230);
			this.SetTooltip(this.btPrintPlan, Resources.Imprimer_Plan, "", Resources.Imprimer_Plan1, true, 230);
			this.SetTooltip(this.btExportPlanToPDF, Resources.Exporter_plan_PDF, "", Resources.Exporter_plan_PDF1, true, 230);
			this.SetTooltip(this.btScaleSet, Resources.Échelle, "", Resources.Échelle1, true, 230);
			this.SetTooltip(this.btToolSelection, Resources.Outil_pointeur, "", Resources.Outil_pointeur1, false, 230);
			this.SetTooltip(this.btToolPan, Resources.Outil_cadrage, "", Resources.Outil_cadrage1, false, 230);
			this.SetTooltip(this.btToolArea, Resources.Outil_surface, "", Resources.Outil_surface1, true, 230);
			this.SetTooltip(this.btToolPerimeter, Resources.Outil_périmètre, "", Resources.Outil_périmètre1, true, 230);
			this.SetTooltip(this.btToolRuler, Resources.Outil_distance, "", Resources.Outil_distance1, true, 230);
			this.SetTooltip(this.btToolCounter, Resources.Outil_compteur, "", Resources.Outil_compteur1, true, 230);
			this.SetTooltip(this.btToolAngle, Resources.Outil_angle, "", Resources.Outil_angle1, true, 230);
			this.SetTooltip(this.btMarkZone, Resources.Marquer_une_zone, "", Resources.Marquer_une_zone1, true, 230);
			this.SetTooltip(this.btInsertNote, Resources.Insérer_une_note, "", Resources.Insérer_une_note1, true, 230);
			this.SetTooltip(this.btZoomToSelection, Resources.Ajuster_à_la_sélection, "", Resources.Ajuster_à_la_sélection1, false, 230);
			this.SetTooltip(this.btZoomToWindow, Resources.Ajuster_à_la_fenêtre, "", Resources.Ajuster_à_la_fenêtre1, false, 230);
			this.SetTooltip(this.btZoomActualSize, Resources.Taille_normale, "", Resources.Taille_normale1, false, 230);
			this.SetTooltip(this.btZoomIn, Resources.Zoom_avant, "( + )", Resources.Zoom_avant1, false, 230);
			this.SetTooltip(this.btZoomOut, Resources.Zoom_arrière, "( - )", Resources.Zoom_arrière1, false, 230);
			this.SetTooltip(this.btBrowsePrevious, Resources.Élément_précédent_du_groupe, "(Ctrl+[)", Resources.Élément_précédent_du_groupe1, false, 230);
			this.SetTooltip(this.btBrowseNext, Resources.Élément_suivant_du_groupe, "(Ctrl+])", Resources.Élément_suivant_du_groupe1, false, 230);
			this.SetTooltip(this.btBrowseObjectTypePrevious, Resources.Élément_précédent_du_type, "( [ )", Resources.Élément_précédent_du_type1, false, 230);
			this.SetTooltip(this.btBrowseObjectTypeNext, Resources.Élément_suivant_du_type, "( ] )", Resources.Élément_suivant_du_type1, false, 230);
			this.SetTooltip(this.btBrightnessContrast, Resources.Luminosité_Contraste, "", Resources.Luminosité_Contraste1, false, 230);
			this.SetTooltip(this.btRotation, Resources.Retourner_Pivoter, "", Resources.Retourner_Pivoter1, false, 230);
			this.SetTooltip(this.btLayerAdd, Resources.Nouveau, "", Resources.Nouveau_calque1, false, 230);
			this.SetTooltip(this.btLayerRemove, Resources.Supprimer, "", Resources.Supprimer_calque1, false, 230);
			this.SetTooltip(this.btLayerRename, Resources.Renommer, "", Resources.Renommer_calque1, false, 230);
			this.SetTooltip(this.btLayerMoveUp, Resources.Déplacer_calque_vers_haut, "", Resources.Déplacer_calque_vers_haut1, false, 300);
			this.SetTooltip(this.btLayerMoveDown, Resources.Déplacer_calque_vers_bas, "", Resources.Déplacer_calque_vers_bas1, false, 300);
			this.SetTooltip(this.btLayerOpenList, Resources.Ouvrir_calques, "", Resources.Ouvrir_calques1, false, 300);
			this.SetTooltip(this.btLayerSaveList, Resources.Sauvegarder_calques_par_défaut, "", Resources.Sauvegarder_calques_par_défaut1, false, 300);
			this.SetTooltip(this.btLayerSaveListAs, Resources.Sauvergarder_calques_sous, "", Resources.Sauvergarder_calques_sous1, false, 300);
			this.SetTooltip(this.btLayersToggle, Resources.Rendre_tous_visible_invisible, "", Resources.Rendre_tous_calques_visible_invisible1, false, 300);
			this.SetTooltip(this.btGroupLocate, Resources.Localiser, "", Resources.Localiser1, false, 230);
			this.SetTooltip(this.btZoomToObject, Resources.Zoomer, "", Resources.Zoomer1, false, 230);
			this.SetTooltip(this.btGroupSelect, Resources.Zoomer_le_groupe, "", Resources.Zoomer_le_groupe1, false, 230);
			this.SetTooltip(this.btGroupRemove, Resources.Supprimer, "", Resources.Supprimer_groupe1, false, 230);
			this.SetTooltip(this.btGroupRename, Resources.Renommer, "", Resources.Renommer_groupe1, false, 230);
			this.SetTooltip(this.btRenamePlan, Resources.Éditer_nom_de_plan, "", Resources.Éditer_nom_de_plan1, false, 230);
			this.SetTooltip(this.btGroupsToggle, Resources.Rendre_tous_visible_invisible, "", Resources.Rendre_tous_groupes_visible_invisible1, false, 300);
			this.SetTooltip(this.btPlanInsertFromPDF, Resources.Insérer_des_plans_PDF, "", Resources.Insérer_des_plans_PDF1, true, 230);
			this.SetTooltip(this.btPlanInsertFromImage, Resources.Insérer_des_plans_images, "", Resources.Insérer_des_plans_images1, true, 230);
			this.SetTooltip(this.btPlanLoad, Resources.Charger_pour_l_édition, "", Resources.Charger_pour_l_édition1, false, 230);
			this.SetTooltip(this.btPlanProperties, Resources.Afficher_les_propriétés, "(Enter)", Resources.Afficher_les_propriétés1, false, 230);
			this.SetTooltip(this.btPlanRemove, Resources.Exclure_du_projet, "(Del)", Resources.Exclure_du_projet1, false, 230);
			this.SetTooltip(this.btPlansPrint, Resources.Imprimer_Plans, "", Resources.Imprimer_Plans1, true, 230);
			this.SetTooltip(this.btPlansExport, Resources.Exporter_plans_PDF, "", Resources.Exporter_plans_PDF1, true, 230);
			this.SetTooltip(this.btPlanRename, Resources.Renommer, "", Resources.Renommer_plan1, false, 230);
			this.SetTooltip(this.btReportFilter, Resources.Éditer_les_items_du_rapport, "", Resources.Éditer_les_items_du_rapport1, true, 230);
			this.SetTooltip(this.btReportPrint, Resources.Imprimer_le_rapport, "", Resources.Imprimer_le_rapport1, false, 230);
			this.SetTooltip(this.btReportPrintPreview, Resources.Aperçu_avant_impression, "", Resources.Aperçu_avant_impression1, false, 230);
			this.SetTooltip(this.btReportPrintSetup, Resources.Mise_en_page, "", Resources.Mise_en_page1, false, 230);
			this.SetTooltip(this.btReportExportToExcel, Resources.Exporter_vers_Excel, "", Resources.Exporter_vers_Excel1, true, 230);
			this.SetTooltip(this.btReportExportToXML, Resources.Exporter_vers_XML, "", Resources.Exporter_vers_XML1, false, 230);
			this.SetTooltip(this.btReportExportToHTML, Resources.Exporter_vers_HTML, "", Resources.Exporter_vers_HTML1, false, 230);
			this.SetTooltip(this.btReportExportToPDF, Resources.Exporter_vers_PDF, "", Resources.Exporter_vers_PDF1, false, 230);
			this.SetTooltip(this.switchOrtho, Resources.ModeOrtho, "", Resources.ModeOrtho1, true, 230);
			this.SetTooltip(this.btEstimatingMaterial, Resources.Nouvel_item_materiel, "(Ins)", Resources.Nouvel_item_materiel1, false, 230);
			this.SetTooltip(this.btEstimatingLabour, Resources.Nouvel_item_main_d_oeuvre, "(Shift+Ins)", Resources.Nouvel_item_main_d_oeuvre1, false, 230);
			this.SetTooltip(this.btEstimatingEquipment, Resources.Nouvel_item_equipement, "(Ctrl+Ins)", Resources.Nouvel_item_equipement1, false, 230);
			this.SetTooltip(this.btEstimatingSubcontract, Resources.Nouvel_item_sous_contracteur, "(Alt+Ins)", Resources.Nouvel_item_sous_contracteur1, false, 230);
			this.SetTooltip(this.btEstimatingModify, Resources.Editer_item_d_estimation, "(Enter)", Resources.Editer_item_d_estimation1, false, 230);
			this.SetTooltip(this.btEstimatingDuplicate, Resources.Dupliquer_item_estimation, "(Ctrl-D)", Resources.Dupliquer_item_estimation1, false, 230);
			this.SetTooltip(this.btEstimatingDelete, Resources.Supprimer_item_estimation, "(Del)", Resources.Supprimer_item_estimation1, false, 230);
			this.SetTooltip(this.btTemplateArea, Resources.Nouveau_modele_surface, "(Ins)", Resources.Nouveau_modele_surface1, false, 230);
			this.SetTooltip(this.btTemplatePerimeter, Resources.Nouveau_modele_perimetre, "(Shift+Ins)", Resources.Nouveau_modele_perimetre1, false, 230);
			this.SetTooltip(this.btTemplateLength, Resources.Nouveau_modele_distance, "(Ctrl+Ins)", Resources.Nouveau_modele_distance1, false, 230);
			this.SetTooltip(this.btTemplateCounter, Resources.Nouveau_modele_compteur, "(Alt+Ins)", Resources.Nouveau_modele_compteur1, false, 230);
			this.SetTooltip(this.btTemplateModify, Resources.Modifier_modele, "(Enter)", Resources.Modifier_modele1, false, 230);
			this.SetTooltip(this.btTemplateDuplicate, Resources.Dupliquer_modele, "(Ctrl-D)", Resources.Dupliquer_modele1, false, 230);
			this.SetTooltip(this.btTemplateDelete, Resources.Supprimer_modele, "(Del)", Resources.Supprimer_modele1, false, 230);
			this.SetTooltip(this.btEstimatingItemsUpdatePrices, Resources.Mettre_a_jour_les_prix, "", Resources.Mettre_a_jour_les_prix1, false, 230);
			this.superTooltip.BeforeTooltipDisplay += this.superTooltip_BeforeTooltipDisplay;
			this.superTooltip.TooltipClosed += this.superTooltip_TooltipClosed;
		}

		private void SelectThemeMenuItem()
		{
			switch (Settings.Default.ThemeStyle)
			{
			case 0:
				this.btStyleRetroBlue.Checked = true;
				return;
			case 1:
				this.btStyleRetroSilver.Checked = true;
				return;
			case 2:
				this.btStyleRetroBlack.Checked = true;
				return;
			case 3:
				this.btStyleRetroGlass.Checked = true;
				return;
			case 4:
				this.btStyleClassicSilver.Checked = true;
				return;
			case 5:
				this.btStyleClassicBlue.Checked = true;
				return;
			case 6:
				this.btStyleClassicBlack.Checked = true;
				return;
			case 7:
				this.btStyleModern.Checked = true;
				return;
			case 8:
				this.btStyleClassicExecutive.Checked = true;
				return;
			case 9:
				this.btStyleMetro.Checked = true;
				return;
			default:
				return;
			}
		}

		private void SetPlatformDefaultTheme()
		{
			string osinfo = Utilities.GetOSInfo(true);
			Console.WriteLine("Using default theme for Windows " + osinfo + ".");
			string a;
			if ((a = osinfo) != null)
			{
				if (a == "Vista" || a == "7")
				{
					Settings.Default.ThemeStyle = 4;
					goto IL_6F;
				}
				if (a == "8")
				{
					Settings.Default.ThemeStyle = 5;
					goto IL_6F;
				}
			}
			Settings.Default.ThemeStyle = 4;
			IL_6F:
			Settings.Default.ThemeColor = Color.Empty;
			this.SetThemeStyle((eStyle)Settings.Default.ThemeStyle);
		}

		private void InitializeTheme()
		{
			try
			{
				if (Settings.Default.ThemeStyle > -1)
				{
					this.SetThemeStyle((eStyle)Settings.Default.ThemeStyle);
					Settings.Default.ThemeColor = ((Settings.Default.ThemeColor == Color.Transparent) ? Color.Empty : Settings.Default.ThemeColor);
					if (Settings.Default.ThemeColor != Color.Empty)
					{
						this.SetThemeColor(Settings.Default.ThemeColor);
					}
				}
				else
				{
					this.SetPlatformDefaultTheme();
				}
			}
			catch
			{
				this.SetPlatformDefaultTheme();
			}
			this.SelectThemeMenuItem();
		}

		private void InitializePosition()
		{
			try
			{
				int num = Settings.Default.WindowPosition.X;
				int num2 = Settings.Default.WindowPosition.Y;
				num = ((num > Screen.PrimaryScreen.WorkingArea.Width / 2) ? 0 : Settings.Default.WindowPosition.X);
				num2 = ((num2 > Screen.PrimaryScreen.WorkingArea.Height / 2) ? 0 : Settings.Default.WindowPosition.Y);
				Settings.Default.WindowPosition = new Point(num, num2);
				base.Location = Settings.Default.WindowPosition;
				base.Size = Settings.Default.WindowSize;
				base.WindowState = Settings.Default.WindowState;
			}
			catch
			{
				if (Utilities.GetInstallLanguage() == "es")
				{
					base.Size = new Size(1520, 861);
				}
				else
				{
					base.Size = new Size(1494, 861);
				}
				base.CenterToScreen();
				base.WindowState = FormWindowState.Normal;
			}
		}

		private void InitializeClipboard()
		{
			this.drawArea.Clipboard.OnObjectPasted += this.Clipboard_ObjectPasted;
		}

		private void InitializeMenus()
		{
			this.bEditPopup.PopupClose += this.bEditPopup_PopupClose;
		}

		private void InitializeScrollSpeed()
		{
			this.sliderScrollSpeed.Value = Settings.Default.ScrollSpeed;
		}

		private void InitializePrintPlanSettings()
		{
			int printPlanType = Settings.Default.PrintPlanType;
			if (printPlanType == 1)
			{
				this.btPrintPlanWindow.Checked = true;
				return;
			}
			Settings.Default.PrintPlanType = 0;
			this.btPrintPlanFullSize.Checked = true;
		}

		private void InitializeExcelSettings()
		{
			switch (Settings.Default.ExportToExcelType)
			{
			case 0:
				this.btExportToExcelRawData.Checked = true;
				return;
			case 1:
				this.btExportToExcelFormattedData.Checked = true;
				return;
			case 2:
				this.btExportToExcelRawAndFormatted.Checked = true;
				return;
			default:
				return;
			}
		}

		private void InitializeStatusBar()
		{
			this.lblStatus.PaddingTop = 3;
			this.lblStatus.PaddingBottom = 3;
			this.lblStatusBarPadding.Stretch = true;
			this.switchOrtho.OffText = Resources.switch_off;
			this.switchOrtho.OnText = Resources.switch_on;
			this.EnableOrtho(Settings.Default.Ortho, true);
			this.zoomSlider.Minimum = 10;
			this.zoomSlider.Maximum = 300;
			this.qualitySlider.Minimum = 0;
			this.qualitySlider.Maximum = 1;
			this.ResetStatusBar();
		}

		private void InitializeColorScheme()
		{
			Color color = Color.FromArgb(252, 229, 126);
			Color black = Color.Black;
			Color color2 = Color.FromArgb(170, 200, 240);
			Color black2 = Color.Black;
			ElementStyle elementStyle = new ElementStyle(black, color);
			ElementStyle elementStyle2 = new ElementStyle(black2, color2);
			new ElementStyle(Color.FromKnownColor(KnownColor.ControlText));
			elementStyle.BorderColor = color;
			elementStyle.BorderColor2 = color;
			elementStyle2.BorderColor = color2;
			elementStyle2.BorderColor2 = color2;
			this.lstLayers.NodeStyleMouseOver = elementStyle;
			this.lstRecentPlans.NodeStyleMouseOver = elementStyle;
			this.lstRecentProjects.NodeStyleMouseOver = elementStyle;
			this.treeObjects.NodeStyleMouseOver = elementStyle;
			this.lstRecentPlans.NodeStyleSelected = this.lstLayers.NodeStyleSelected;
			this.lstRecentProjects.NodeStyleSelected = this.lstLayers.NodeStyleSelected;
			this.treeObjects.NodeStyleSelected = this.lstLayers.NodeStyleSelected;
			this.lstRecentPlans.NodeStyle = this.lstLayers.NodeStyle;
			this.lstRecentProjects.NodeStyle = this.lstLayers.NodeStyle;
			this.treeObjects.NodeStyle = this.lstLayers.NodeStyle;
		}

		private void InitializeAutoBackup()
		{
			this.EnableAutoBackup(Settings.Default.EnableAutoBackup);
		}

		private void InitializeDockingWindows()
		{
			this.dockingWindows.Add(new DockingWindow(Resources.Navigation, true, 335, 250, this.containerBarNavigation, this.dockContainerItemPreview, this.panelDockPreview));
			this.dockingWindows.Add(new DockingWindow(Resources.Propriétés, true, 335, 450, this.containerBarProperties, this.dockContainerItemProperties, this.panelDockProperties));
			this.dockingWindows.Add(new DockingWindow(Resources.Calques, true, 335, 250, this.containerBarLayers, this.dockContainerItemLayers, this.panelDockLayers));
			this.dockingWindows.Add(new DockingWindow(Resources.Groupes, true, 335, 450, this.containerBarGroups, this.dockContainerItemGroups, this.panelDockGroups));
			this.dockingWindows.Add(new DockingWindow(Resources.Plans_récents, true, 335, 300, this.containerBarRecentPlans, this.dockContainerItemRecentPlans, this.panelDockRecentPlans));
			this.dockingWindows.Add(new DockingWindow(Resources.Estimation, true, 500, 600, this.containerBarEstimating, this.dockContainerItemEstimating, this.panelDockEstimating));
			this.LoadLayout();
			foreach (DockingWindow dockingWindow in this.dockingWindows)
			{
				if (dockingWindow.ContainerBar.AutoHide)
				{
					dockingWindow.ContainerItem.Width = dockingWindow.Width;
				}
			}
			this.EnableDockingWindows(false, false);
			this.EnableDockingWindows(true, true);
		}

		private void InitializePanels()
		{
			this.panelWelcome.Dock = DockStyle.Fill;
			this.lblNoPlan.Dock = DockStyle.Fill;
			this.mainControl.Dock = DockStyle.Fill;
			this.flowPlans.Dock = DockStyle.Fill;
			this.webBrowser.Dock = DockStyle.Fill;
			this.reportsControl.Dock = DockStyle.Fill;
			this.panelBrightnessContrast.Dock = DockStyle.Bottom;
			this.panelRotation.Dock = DockStyle.Bottom;
			this.panelPlansAction.Dock = DockStyle.Bottom;
			this.panControl.Dock = DockStyle.Fill;
			this.cbObjects.Dock = DockStyle.Top;
			this.tabProperties.Dock = DockStyle.Top;
			this.barDisplayResults.Dock = DockStyle.Bottom;
			this.barGroups.Dock = DockStyle.Top;
			this.cbPlans.Dock = DockStyle.Top;
			this.txtPlanName.Dock = DockStyle.Top;
			this.treeObjects.Dock = DockStyle.Fill;
			this.treeDBEstimatingItems.Dock = DockStyle.Fill;
			this.treeTemplatesLibrary.Dock = DockStyle.Fill;
		}

		private void InitializeDrawingControls()
		{
			this.drawArea.Initialize(this, this.project, this.mainControl.DrawingBoard);
			this.drawArea.OnObjectsSelected += this.drawArea_ObjectsSelected;
			this.drawArea.OnDeductionsParentSet += this.drawArea_DeductionsParentSet;
			this.drawArea.OnDeductionsParentRelease += this.drawArea_DeductionsParentRelease;
			this.project.Initialize(this.drawArea, this.extensionsSupport, this.dbManagement);
		}

		private void InitializePlanPreview()
		{
			this.previewPanel = new ThumbnailPanel();
			this.previewPanel.DrawArea = this.drawArea;
			this.previewPanel.BackColor = Color.White;
			this.previewPanel.DisplayName = false;
			this.previewPanel.DisplayShadow = false;
			this.previewPanel.BringToFront();
			this.previewPanel.Visible = false;
			base.Controls.Add(this.previewPanel);
			base.Controls.SetChildIndex(this.previewPanel, 0);
		}

		private void InitializeObjectEditor()
		{
			this.objectEditor = new DrawObjectEditor(this.project, this.drawArea, this.extensionsSupport, this.cbObjects, this.gridObjectProperties, this.extensionsManager, this.tabProperties, this.superTabProperties, this.barDisplayResults, this.estimatingItemsControl, this.coControl, this.coInterface);
			this.objectEditor.OnObjectSelected += this.objectEditor_ObjectSelected;
			this.objectEditor.OnObjectChanged += this.objectEditor_ObjectChanged;
			this.objectEditor.OnDisplayCalculationsForAllPlans += this.objectEditor_OnDisplayCalculationsForAllPlans;
			this.objectEditor.OnPresetCreated += this.objectEditor_OnPresetCreated;
			this.objectEditor.OnPresetModified += this.objectEditor_OnPresetModified;
			this.objectEditor.OnPresetDeleted += this.objectEditor_OnPresetDeleted;
			this.objectEditor.OnProductCreated += this.objectEditor_OnProductCreated;
			this.objectEditor.OnProductModified += this.objectEditor_OnProductModified;
			this.objectEditor.OnProductDeleted += this.objectEditor_OnProductDeleted;
		}

		private void InitializeObjectsNavigator()
		{
			this.objectsNavigator = new DrawObjectsNavigator(this.project, this.drawArea, this.cbPlans, this.txtPlanName, this.treeObjects, this.previewPanel, this.containerBarGroups);
			this.objectsNavigator.OnNodeSelected += this.objectsNavigator_OnNodeSelected;
			this.objectsNavigator.OnLayerChanged += this.objectsNavigator_LayerChanged;
			this.objectsNavigator.OnObjectSelected += this.objectsNavigator_ObjectSelected;
			this.objectsNavigator.OnObjectChanged += this.objectsNavigator_ObjectChanged;
			this.objectsNavigator.OnPlanSelected += this.objectsNavigator_PlanSelected;
			this.objectsNavigator.OnPlanRename += this.objectsNavigator_OnPlanRename;
			this.objectsNavigator.OnGroupChangeLayer += this.objectsNavigator_OnGroupChangeLayer;
		}

		private void InitializeLayersNavigator()
		{
			this.layersNavigator = new LayersNavigator(this.project, this.drawArea, this.lstLayers);
			this.layersNavigator.OnLayerSelected += this.layersNavigator_LayerSelected;
			this.layersNavigator.OnLayerChanged += this.layersNavigator_LayerChanged;
		}

		private void InitializePlansNavigator()
		{
			this.plansNavigator = new PlansNavigator(this.project, this.drawArea, this.flowPlans, this.backgroundWorker);
			this.plansNavigator.OnPlanSelected += this.plansNavigator_OnPlanSelected;
			this.plansNavigator.OnPlanLoad += this.plansNavigator_OnPlanLoad;
			this.plansNavigator.OnPlanEdit += this.plansNavigator_OnPlanEdit;
			this.plansNavigator.OnPlanRemove += this.plansNavigator_OnPlanRemove;
			this.plansNavigator.OnPlanReordered += this.plansNavigator_OnPlanReordered;
			this.plansNavigator.OnPlanApplyAction += this.plansNavigator_OnPlanApplyAction;
		}

		private void InitializeRecentPlansNavigator()
		{
			this.recentPlansNavigator = new RecentPlansNavigator(this.project, this.drawArea, this.lstRecentPlans, this.previewPanel, this.containerBarRecentPlans);
			this.recentPlansNavigator.OnPlanRename += this.recentPlansNavigator_OnPlanRename;
			this.recentPlansNavigator.OnPlanLoad += this.plansNavigator_OnPlanLoad;
		}

		private void InitializeEstimatingEditor()
		{
			this.estimatingEditor = new EstimatingEditor(this.project, this.drawArea, this.treeEstimatingItems, this.mainControl, this.imageCollection);
			this.estimatingEditor.OnDisable += this.estimatingEditor_OnDisable;
			this.estimatingEditor.OnEnable += this.estimatingEditor_OnEnable;
			this.estimatingEditor.OnModified += this.estimatingEditor_OnModified;
		}

		private void InitializeReportModule()
		{
			this.reportsControl.InitializeData(this.project, this.drawArea, this.extensionsSupport, this.dbManagement);
			this.reportsControl.OnSelectReport += this.reportsControl_OnSelectReport;
			this.reportsControl.OnApplySettings += this.reportsControl_OnApplySettings;
			this.reportsControl.OnCancelSettings += this.reportsControl_OnCancelSettings;
		}

		private void LoadReportSettings()
		{
			this.exitNow = true;
			if (this.project.Report.SystemType == UnitScale.UnitSystem.undefined)
			{
				this.ValidateReportSystemType();
			}
			this.UpdateReportScaleSystemInGUI(false);
			this.UpdateReportScalePrecisionInGUI(false);
			this.exitNow = false;
			this.reportsControl.Dirty = true;
		}

		private void SynchPresets()
		{
			foreach (object obj in this.project.Groups.Collection)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				foreach (object obj2 in drawObjectGroup.Presets.Collection)
				{
					Preset preset = (Preset)obj2;
					preset.SynchWithTemplate(this.extensionsSupport);
				}
			}
		}

		private void LoadExtensions()
		{
			try
			{
				this.extensionsSupport.Clear();
				string[] files = Directory.GetFiles(Path.Combine(Utilities.GetInstallExtensionsFolder(), Settings.Default.UICulture), "*.xml", SearchOption.TopDirectoryOnly);
				foreach (string fileName in files)
				{
					this.extensionsSupport.LoadCategory(fileName);
				}
				this.SynchPresets();
			}
			catch (Exception ex)
			{
				string impossible_de_charger_les_extensions = Resources.Impossible_de_charger_les_extensions;
				string message = ex.Message;
				Utilities.DisplayError(impossible_de_charger_les_extensions, message);
			}
		}

		private void LoadTemplates()
		{
			try
			{
				this.templatesSupport.Clear();
				string[] files = Directory.GetFiles(Utilities.GetTemplatesFolder(), "*.xml", SearchOption.TopDirectoryOnly);
				foreach (string fileName in files)
				{
					this.templatesSupport.LoadTemplate(fileName, this.extensionsSupport);
				}
			}
			catch (Exception ex)
			{
				string impossible_de_charger_les_modèles = Resources.Impossible_de_charger_les_modèles;
				string message = ex.Message;
				Utilities.DisplayError(impossible_de_charger_les_modèles, message);
			}
		}

		private void InitializeGui()
		{
			this.LoadResources();
			this.InitializePosition();
			this.InitializeClipboard();
			this.InitializeMenus();
			this.InitializePanels();
			this.InitializeColorScheme();
			this.InitializeDrawingControls();
			this.InitializePlanPreview();
			this.InitializeObjectEditor();
			this.InitializeObjectsNavigator();
			this.InitializeLayersNavigator();
			this.InitializePlansNavigator();
			this.InitializeRecentPlansNavigator();
			this.InitializeEstimatingEditor();
			this.InitializeReportModule();
			this.InitializeDBEstimatingItemsEditor();
			this.InitializeTemplatesEditor();
			this.InitializeMruManager();
			this.InitializeScrollSpeed();
			this.InitializePrintPlanSettings();
			this.InitializeExcelSettings();
			this.InitializeStatusBar();
			this.InitializeAutoBackup();
			this.guiReady = true;
		}

		private void BackupSettings()
		{
			Utilities.ValidateDirectory(Utilities.GetLocalDBFolder());
			string text = "";
			foreach (object obj in Settings.Default.PropertyValues)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				try
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						settingsPropertyValue.Name,
						"|",
						settingsPropertyValue.PropertyValue.ToString().Replace(Environment.NewLine, "~"),
						"|",
						Environment.NewLine
					});
				}
				catch
				{
				}
			}
			Utilities.SaveStringToFile(Utilities.GetBackupSettingsFileName(), text);
		}

		private void RecoverSettings()
		{
			string text = Utilities.ReadToString(Utilities.GetBackupSettingsFileName());
			if (text != string.Empty)
			{
				string[] fields = Utilities.GetFields(text, new char[]
				{
					'\r',
					'\n'
				});
				foreach (string originalString in fields)
				{
					try
					{
						string[] fields2 = Utilities.GetFields(originalString, '|');
						if (fields2.GetUpperBound(0) >= 1)
						{
							string a = fields2.GetValue(0).ToString();
							object value = fields2.GetValue(1);
							if (a == "CompanyName")
							{
								Settings.Default.CompanyName = value.ToString();
							}
							if (a == "CompanyFullAddress")
							{
								Settings.Default.CompanyFullAddress = value.ToString().Replace("~", Environment.NewLine);
							}
							if (a == "CompanyRepresentative")
							{
								Settings.Default.CompanyRepresentative = value.ToString();
							}
							if (a == "DataFolder")
							{
								Settings.Default.DataFolder = value.ToString();
							}
							if (a == "ImagesFolder")
							{
								Settings.Default.ImagesFolder = value.ToString();
							}
							if (a == "PDFFolder")
							{
								Settings.Default.PDFFolder = value.ToString();
							}
							if (a == "COfficePath")
							{
								Settings.Default.COfficePath = value.ToString();
							}
							if (a == "Tax1Rate")
							{
								Settings.Default.Tax1Rate = Utilities.ConvertToDouble(value, -1);
							}
							if (a == "Tax2Rate")
							{
								Settings.Default.Tax2Rate = Utilities.ConvertToDouble(value, -1);
							}
							if (a == "TaxOnTax")
							{
								Settings.Default.TaxOnTax = Utilities.ConvertToBoolean(value, false);
							}
							if (a == "Tax1Label")
							{
								Settings.Default.Tax1Label = value.ToString();
							}
							if (a == "Tax2Label")
							{
								Settings.Default.Tax2Label = value.ToString();
							}
							if (a == "MruList")
							{
								Settings.Default.MruList = value.ToString();
							}
							if (a == "UICulture")
							{
								Settings.Default.UICulture = value.ToString();
							}
							if (a == "ImportManualDpi")
							{
								Settings.Default.ImportManualDpi = Utilities.ConvertToInt(value);
							}
							if (a == "ImportDpi")
							{
								Settings.Default.ImportDpi = Utilities.ConvertToInt(value);
							}
							if (a == "PrintPlanType")
							{
								Settings.Default.PrintPlanType = Utilities.ConvertToInt(value);
							}
							if (a == "AngleType")
							{
								Settings.Default.AngleType = Utilities.ConvertToInt(value);
							}
							if (a == "SlopeType")
							{
								Settings.Default.SlopeType = Utilities.ConvertToInt(value);
							}
							if (a == "ScrollSpeed")
							{
								Settings.Default.ScrollSpeed = Utilities.ConvertToInt(value);
							}
							if (a == "ThemeStyle")
							{
								Settings.Default.ThemeStyle = Utilities.ConvertToInt(value);
							}
							if (a == "DefaultSystemType")
							{
								Settings.Default.DefaultSystemType = Utilities.ConvertToInt(value);
							}
							if (a == "DefaultPrecision")
							{
								Settings.Default.DefaultPrecision = Utilities.ConvertToInt(value);
							}
							if (a == "ExportToExcelType")
							{
								Settings.Default.ExportToExcelType = Utilities.ConvertToInt(value);
							}
							if (a == "Ortho")
							{
								Settings.Default.Ortho = Utilities.ConvertToBoolean(value, false);
							}
							if (a == "ConvertPDFToColor")
							{
								Settings.Default.ConvertPDFToColor = Utilities.ConvertToBoolean(value, false);
							}
							if (a == "InstallLanguage")
							{
								Settings.Default.InstallLanguage = value.ToString();
							}
							if (a == "LastSaved")
							{
								Settings.Default.LastSaved = value.ToString();
							}
						}
					}
					catch (Exception exception)
					{
						Utilities.DisplaySystemError(exception);
					}
				}
				Settings.Default.UpdateSettings = false;
			}
		}

		private void ValidateSettings()
		{
			if (Settings.Default.LastSaved == "" && Utilities.FileExists(Utilities.GetBackupSettingsFileName()))
			{
				this.RecoverSettings();
			}
		}

		private void UpdateSettings()
		{
			try
			{
				Settings.Default.Upgrade();
				Settings.Default.UpdateSettings = false;
				Settings.Default.Save();
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void InitializeSettings()
		{
			string a = CultureInfo.CurrentCulture.Name.ToLower();
			Settings.Default.ImportDpi = ((Settings.Default.ImportDpi < 150) ? 150 : Settings.Default.ImportDpi);
			Settings.Default.ImportDpi = ((Settings.Default.ImportDpi > 300) ? 300 : Settings.Default.ImportDpi);
			Settings.Default.ImportManualDpi = ((Settings.Default.ImportManualDpi < 150) ? 150 : Settings.Default.ImportManualDpi);
			Settings.Default.ImportManualDpi = ((Settings.Default.ImportManualDpi > 300) ? 300 : Settings.Default.ImportManualDpi);
			Settings.Default.AngleType = ((Settings.Default.AngleType < 0 || Settings.Default.AngleType > 2) ? 0 : Settings.Default.AngleType);
			Settings.Default.SlopeType = ((Settings.Default.SlopeType < 0 || Settings.Default.SlopeType > 3) ? 1 : Settings.Default.SlopeType);
			Settings.Default.ScrollSpeed = ((Settings.Default.ScrollSpeed < 0 || Settings.Default.ScrollSpeed > 100) ? 0 : Settings.Default.ScrollSpeed);
			Settings.Default.Tax1Label = ((Settings.Default.Tax1Label != "") ? Settings.Default.Tax1Label : Resources.Taxe_1);
			Settings.Default.Tax2Label = ((Settings.Default.Tax2Label != "") ? Settings.Default.Tax2Label : Resources.Taxe_2);
			Settings.Default.DefaultSystemType = ((Settings.Default.DefaultSystemType < 0 || Settings.Default.DefaultSystemType > 1) ? ((a == "en-us" || a == "en-ca" || a == "fr-ca") ? 1 : 0) : Settings.Default.DefaultSystemType);
			Settings.Default.DefaultPrecision = ((Settings.Default.DefaultPrecision < 0 || Settings.Default.DefaultPrecision > 3) ? 2 : Settings.Default.DefaultPrecision);
			Settings.Default.BackupInterval = ((Settings.Default.BackupInterval < 0 || Settings.Default.BackupInterval > 30) ? 5 : Settings.Default.BackupInterval);
		}

		private void InitializeMruManager()
		{
			this.mruManager.Load(Utilities.GetProjectsFolder());
			this.mruManager.Populate(this.galleryRecentProjects, this.lstRecentProjects);
			this.mruManager.OnRecentProjectSelected += this.mruManager_OnRecentProjectSelected;
		}

		private void ValidateDataFolder()
		{
			if (Path.Combine(Settings.Default.DataFolder, Utilities.ApplicationName) == Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName))
			{
				Settings.Default.DataFolder = "";
			}
			if (Settings.Default.DataFolder != "")
			{
				if (Utilities.ValidateDirectory(Utilities.GetUserDataFolder()))
				{
					this.dataFolderToSave = Settings.Default.DataFolder;
					return;
				}
				string répertoire_de_données_invalide = Resources.Répertoire_de_données_invalide;
				string message = string.Concat(new string[]
				{
					Resources.Impossible_de_valider_le_répertoire_de_données_alternatif,
					"\n",
					Utilities.GetUserDataFolder(),
					"\n\n",
					Resources.Bascule_vers_le_répertoire_par_défaut
				});
				Utilities.DisplayError(répertoire_de_données_invalide, message);
				Settings.Default.DataFolder = "";
			}
			Utilities.ValidateDirectory(Utilities.GetUserDataFolder());
			this.dataFolderToSave = "";
		}

		private void ValidateDirectories()
		{
			this.ValidateDataFolder();
			Utilities.ValidateDirectory(Utilities.GetProjectsFolder());
			Utilities.ValidateDirectory(Utilities.GetDBFolder(""));
			Utilities.ValidateDirectory(Utilities.GetPlansFolder());
			Utilities.ValidateDirectory(Utilities.GetLayersFolder());
			Utilities.ValidateDirectory(Utilities.GetDefaultLayersFolder());
			Utilities.ValidateDirectory(Utilities.GetDefaultPDFFolder());
			Utilities.ValidateDirectory(Utilities.GetPDFExportFolder());
			Utilities.ValidateDirectory(Utilities.GetTemplatesFolder());
			Utilities.ValidateDirectory(Utilities.GetReportsFolder());
			Utilities.ValidateDirectory(Utilities.GetApplicationDataFolder());
			Utilities.ValidateDirectory(Utilities.GetThumbnailsFolder());
			Utilities.ValidateDirectory(Utilities.GetReportsFolder() + "\\css");
			if (!Utilities.DirectoryExists(Utilities.GetReportsFolder() + "\\images"))
			{
				Utilities.FilesCopy(Utilities.GetInstallReportsFolder() + "\\images", Utilities.GetReportsFolder() + "\\images", false, true);
			}
		}

		private void SetDefaultReportTheme()
		{
			string currentValidUICulture;
			if ((currentValidUICulture = Utilities.GetCurrentValidUICulture()) != null)
			{
				if (currentValidUICulture == "en-US")
				{
					Settings.Default.ReportTheme = "Blue Sky";
					return;
				}
				if (!(currentValidUICulture == "fr-FR"))
				{
					return;
				}
				Settings.Default.ReportTheme = "Bleu ciel";
			}
		}

		private void ValidateReportTheme()
		{
			if (Settings.Default.ReportTheme == string.Empty)
			{
				this.SetDefaultReportTheme();
				return;
			}
			if (!Utilities.FileExists(Path.Combine(Utilities.GetInstallReportsFolder(), string.Concat(new string[]
			{
				"css\\",
				Utilities.GetCurrentValidUICulture(),
				"\\",
				Settings.Default.ReportTheme,
				".css"
			}))))
			{
				this.SetDefaultReportTheme();
			}
		}

		private void InitializeCOInterface()
		{
			this.coInterface.IsReady = false;
			if (!this.coInterface.IsInstalled())
			{
				return;
			}
			this.forceApplicationExit = false;
			if (!this.coInterface.OpenDatabase(COfficeInterface.CODatabaseType.COResidentialDatabase, ref this.forceApplicationExit))
			{
				return;
			}
			if (this.coInterface.DatabaseExists(COfficeInterface.CODatabaseType.COCommercialDatabase))
			{
				this.coInterface.OpenDatabase(COfficeInterface.CODatabaseType.COCommercialDatabase, ref this.forceApplicationExit);
			}
			this.forceApplicationExit = false;
			this.coInterface.IsReady = true;
			this.InitializeCOfficeGui();
		}

		private void InitializeData()
		{
			this.InitializeSettings();
			this.ValidateDirectories();
			this.ValidateReportTheme();
			this.InitializeDBManagement();
			this.InitializeEstimatingItemsControl();
			this.LoadExtensions();
			this.LoadTemplates();
			this.InitializeCOInterface();
			this.InitializeBackupTimer();
		}

		private void SaveApplicationSettings()
		{
			try
			{
				this.mruManager.Save();
				Settings.Default.ScrollSpeed = this.sliderScrollSpeed.Value;
				Settings.Default.DataFolder = this.dataFolderToSave;
				if (base.WindowState != FormWindowState.Minimized)
				{
					Settings.Default.WindowState = base.WindowState;
				}
				if (Utilities.ConvertToInt(this.ribbonControl.SelectedRibbonTabItem.Tag.ToString()) == 0)
				{
					this.SaveLayout();
				}
				Settings.Default.LastSaved = Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort());
				Settings.Default.Save();
				this.BackupSettings();
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		public void LockInterface(ref bool bInterfaceWasLockedHere)
		{
			if (!this.bInterfaceIsLock)
			{
				this.bInterfaceIsLock = true;
				bInterfaceWasLockedHere = true;
				Utilities.EnableInterface(this, false);
			}
		}

		public void UnlockInterface(ref bool bInterfaceWasLockedHere)
		{
			if (bInterfaceWasLockedHere)
			{
				this.bInterfaceIsLock = false;
				bInterfaceWasLockedHere = false;
				Utilities.EnableInterface(this, true);
				Application.DoEvents();
			}
		}

		private void ResetReport()
		{
			this.exitNow = true;
			this.reportsControl.Clear();
			this.exitNow = false;
		}

		private void ResetToolsFilter()
		{
			this.txtAreaFilter.Text = "";
			this.txtPerimeterFilter.Text = "";
			this.txtDistanceFilter.Text = "";
			this.txtCounterFilter.Text = "";
		}

		private void ResetStatusBar()
		{
			this.zoomSlider.Value = 100;
			this.qualitySlider.Value = 1;
		}

		private void ResetLayout()
		{
		}

		private void ResetBrightnessContrastControls()
		{
			this.exitNow = true;
			this.sliderBrightness.Text = "0";
			this.sliderBrightness.Value = 0;
			this.sliderContrast.Text = "0";
			this.sliderContrast.Value = 0;
			this.mainControl.Brightness = 0;
			this.mainControl.Contrast = 0;
			this.mainControl.UseDynamicAdjustments = false;
			this.exitNow = false;
			this.panelBrightnessContrast.Visible = false;
			this.btBrightnessContrast.Checked = false;
			Utilities.SuspendDrawing(this);
			this.EnableDockingWindows(true, true);
			Utilities.ResumeDrawing(this);
			this.ZoomRestricted = false;
			this.ImageEditing = false;
			this.helpContextString = "";
		}

		private void ResetRotationControls()
		{
			this.mainControl.Brightness = 0;
			this.mainControl.Contrast = 0;
			this.mainControl.UseDynamicAdjustments = false;
			this.panelRotation.Visible = false;
			this.btRotation.Checked = false;
			Utilities.SuspendDrawing(this);
			this.EnableDockingWindows(true, true);
			Utilities.ResumeDrawing(this);
			this.ZoomRestricted = false;
			this.ImageEditing = false;
			this.helpContextString = "";
		}

		private void ResetPlansActionControls()
		{
			this.panelPlansAction.Visible = false;
			this.btPlansPrint.Checked = false;
			this.btPlansExport.Checked = false;
			this.PlansSelection = false;
			this.helpContextString = "";
			this.RestorePlan();
		}

		private void ResetEditReportSettings()
		{
			this.ReportEditing = false;
			this.helpContextString = "";
		}

		private void ResetHelp()
		{
			this.helpContextString = "";
		}

		public void UpdateStatusBar(string status)
		{
			if (status != string.Empty)
			{
				this.lblStatus.Text = status;
				return;
			}
			this.UpdateStatusBar(this.applicationStatus);
		}

		private void UpdateStatusBar(MainForm.ApplicationStatusEnum newStatus)
		{
			this.applicationStatus = newStatus;
			string text = string.Empty;
			switch (this.applicationStatus)
			{
			case MainForm.ApplicationStatusEnum.StatusReady:
				text = Resources.Prêt;
				break;
			case MainForm.ApplicationStatusEnum.StatusModified:
				text = Resources.Modifié;
				break;
			case MainForm.ApplicationStatusEnum.StatusSaved:
				text = Resources.Sauvegardé;
				break;
			}
			this.lblStatus.Text = text;
		}

		private void UpdateTileBar()
		{
			string text = Utilities.ApplicationName;
			if (this.project.Name != "")
			{
				text = text + " - " + this.project.DisplayName + ".qpl";
				if (this.drawArea.ActivePlan != null)
				{
					text = text + " (" + this.drawArea.ActivePlan.Name + ")";
				}
				text += (this.project.Dirty ? "*" : "");
			}
			this.Text = text;
		}

		public void EnableEditCommands(bool enable)
		{
			if (!enable)
			{
				this.btEditPaste.Enabled = false;
				this.btEditCut.Enabled = false;
				this.btEditCopy.Enabled = false;
				this.btEditDelete.Enabled = false;
				this.btEditUndo.Enabled = false;
				this.btEditRedo.Enabled = false;
				this.btUndo.Enabled = false;
				this.btRedo.Enabled = false;
				this.btEditSendData.Enabled = false;
				this.btEditSendData.SubItems.Clear();
				this.lblBrowseGroup.Enabled = false;
				this.btBrowsePrevious.Enabled = false;
				this.btBrowseNext.Enabled = false;
				this.lblBrowseObjectType.Enabled = false;
				this.btBrowseObjectTypePrevious.Enabled = false;
				this.btBrowseObjectTypeNext.Enabled = false;
				return;
			}
			bool enabled = this.CanBrowseObject(false);
			bool enabled2 = this.CanBrowseObject(true);
			bool flag = this.drawArea.SelectedObject != null && this.drawArea.SelectedObject.ObjectType != "Legend";
			bool flag2 = this.CanSendData();
			this.btEditCut.Enabled = (flag && !this.DeductionsEditing);
			this.btEditCopy.Enabled = (flag && !this.DeductionsEditing);
			this.btEditPaste.Enabled = (!this.Clipboard.IsEmpty && !this.DeductionsEditing);
			this.btEditDelete.Enabled = flag;
			this.btEditRedo.Enabled = this.drawArea.CanRedo;
			this.btEditUndo.Enabled = this.drawArea.CanUndo;
			this.btRedo.Enabled = this.drawArea.CanRedo;
			this.btUndo.Enabled = this.drawArea.CanUndo;
			this.btEditSendData.Enabled = flag2;
			if (flag2)
			{
				this.btEditSendData.SubItems.Add(new ButtonItem());
			}
			else
			{
				this.btEditSendData.SubItems.Clear();
			}
			this.lblBrowseGroup.Enabled = enabled;
			this.btBrowsePrevious.Enabled = enabled;
			this.btBrowseNext.Enabled = enabled;
			this.lblBrowseObjectType.Enabled = enabled2;
			this.btBrowseObjectTypePrevious.Enabled = enabled2;
			this.btBrowseObjectTypeNext.Enabled = enabled2;
		}

		private void EnablePlanCommands(bool enable)
		{
			this.btPlanLoad.Enabled = enable;
			this.btPlanProperties.Enabled = enable;
			this.btPlanRemove.Enabled = enable;
			this.btPlanDuplicate.Enabled = enable;
			this.btPlanExport.Enabled = enable;
			this.btPlansPrint.Enabled = (enable && this.project.Plans.Count > 0);
			this.btPlansExport.Enabled = (enable && this.project.Plans.Count > 0);
		}

		private void EnableReportCommands(bool enable)
		{
			this.btReportFilter.Enabled = enable;
			this.btReportSettings.Enabled = enable;
			this.btReportPrint.Enabled = enable;
			this.btReportPrintPreview.Enabled = enable;
			this.btReportPrintSetup.Enabled = enable;
			this.btReportExportToExcel.Enabled = enable;
			this.btReportExportToXML.Enabled = enable;
			this.btReportExportToHTML.Enabled = enable;
			this.btReportExportToPDF.Enabled = enable;
		}

		private void EnableGUI(bool enable)
		{
			if (!this.isActivated && this.trialDaysRemaining == 0U)
			{
				enable = false;
			}
			this.btProjectSaveAs.Visible = false;
			if (!this.ImageEditing)
			{
				this.btProjectSave.Enabled = enable;
				this.btProjectSaveAs.Enabled = enable;
				this.btProjectInfo.Enabled = enable;
				this.btProjectClose.Enabled = enable;
				this.btSave.Visible = enable;
				if (!enable)
				{
					this.EnableEditCommands(false);
					this.EnablePlanCommands(false);
					this.EnableReportCommands(false);
					this.btExportPlanToPDF.Enabled = false;
					this.btPrintPlan.Enabled = false;
					this.btScaleSet.Enabled = false;
					this.btToolSelection.Enabled = false;
					this.btToolPan.Enabled = false;
					this.btToolArea.Enabled = false;
					this.btToolPerimeter.Enabled = false;
					this.btToolCounter.Enabled = false;
					this.btToolAngle.Enabled = false;
					this.btToolRuler.Enabled = false;
					this.btMarkZone.Enabled = false;
					this.btInsertNote.Enabled = false;
					this.btInsertPicture.Enabled = false;
					this.btZoomIn.Enabled = false;
					this.btZoomOut.Enabled = false;
					this.btZoomActualSize.Enabled = false;
					this.btZoom50.Enabled = false;
					this.btZoomToWindow.Enabled = false;
					this.btZoomToSelection.Enabled = false;
					this.btBookmarks.Enabled = false;
					this.btBrightnessContrast.Enabled = false;
					this.btRotation.Enabled = false;
					this.btPlanInsertFromPDF.Enabled = false;
					this.btPlanInsertFromImage.Enabled = false;
					this.lblOrtho.ForeColor = Color.DimGray;
					this.switchOrtho.Enabled = false;
					this.zoomSlider.Enabled = false;
					this.lblZoom.ForeColor = Color.DimGray;
					this.qualitySlider.Enabled = false;
					this.lblImageQuality.ForeColor = Color.DimGray;
					this.objectEditor.Enabled = false;
					this.objectsNavigator.Enabled = false;
					this.layersNavigator.Enabled = false;
					this.recentPlansNavigator.Enabled = false;
					this.estimatingEditor.Enabled = false;
					this.barLayers.Enabled = false;
					this.barGroups.Enabled = false;
					this.barRecentPlans.Enabled = false;
					this.mainControl.Enabled = false;
					this.panControl.Enabled = false;
					this.plansNavigator.Enabled = false;
					this.btPlanInsertFromPDF.Enabled = false;
					this.btPlanInsertFromImage.Enabled = false;
					this.btPlansPrint.Enabled = false;
					this.btPlansExport.Enabled = false;
					this.btReportExportToCOffice.Enabled = false;
				}
				else
				{
					bool flag = this.mainControl.Image != null;
					bool flag2 = this.mainControl.Image != null && !this.mainControl.ZoomRestricted;
					bool flag3 = this.plansNavigator.SelectedPlan != null;
					this.EnableEditCommands(flag && this.currentTabIndex == MainForm.RibbonTabEnum.RibbonStart && !this.DeductionsEditing);
					this.EnablePlanCommands(flag3 && this.currentTabIndex == MainForm.RibbonTabEnum.RibbonPlans);
					this.EnableReportCommands(this.project.Plans.Count > 0 && this.currentTabIndex == MainForm.RibbonTabEnum.RibbonReport);
					this.btExportPlanToPDF.Enabled = flag;
					this.btPrintPlan.Enabled = flag;
					this.btScaleSet.Enabled = flag;
					this.btToolSelection.Enabled = flag;
					this.btToolPan.Enabled = flag;
					this.btToolArea.Enabled = flag;
					this.btToolPerimeter.Enabled = flag;
					this.btToolCounter.Enabled = flag;
					this.btToolAngle.Enabled = flag;
					this.btToolRuler.Enabled = flag;
					this.btMarkZone.Enabled = flag;
					this.btInsertNote.Enabled = flag;
					this.btZoomIn.Enabled = flag;
					this.btZoomOut.Enabled = flag;
					this.btZoomActualSize.Enabled = flag;
					this.btZoom50.Enabled = flag;
					this.btZoomToWindow.Enabled = flag;
					this.btZoomToSelection.Enabled = flag;
					this.btBrightnessContrast.Enabled = flag;
					this.btRotation.Enabled = flag;
					this.lblOrtho.ForeColor = (flag ? this.lblStatus.ForeColor : Color.DimGray);
					this.switchOrtho.Enabled = flag;
					this.zoomSlider.Enabled = flag2;
					this.lblZoom.ForeColor = (flag2 ? this.lblStatus.ForeColor : Color.DimGray);
					this.qualitySlider.Enabled = flag2;
					this.lblImageQuality.ForeColor = (flag2 ? this.lblStatus.ForeColor : Color.DimGray);
					this.objectEditor.Enabled = flag;
					this.objectsNavigator.Enabled = flag;
					this.layersNavigator.Enabled = flag;
					this.barLayers.Enabled = flag;
					this.barGroups.Enabled = flag;
					this.barRecentPlans.Enabled = flag;
					this.mainControl.Enabled = flag;
					this.panControl.Enabled = flag;
					this.recentPlansNavigator.Enabled = true;
					this.estimatingEditor.Enabled = true;
					this.plansNavigator.Enabled = true;
					this.btPlanInsertFromPDF.Enabled = true;
					this.btPlanInsertFromImage.Enabled = true;
					this.btPlansPrint.Enabled = true;
					this.btPlansExport.Enabled = true;
					this.btReportExportToCOffice.Enabled = true;
				}
			}
			this.guiEnabled = enable;
			this.SelectTabView();
		}

		private void SelectTabView()
		{
			Utilities.SuspendDrawing(this);
			this.btUndo.Visible = (this.currentTabIndex == MainForm.RibbonTabEnum.RibbonStart && this.guiEnabled && this.mainControl.Image != null);
			this.btRedo.Visible = (this.currentTabIndex == MainForm.RibbonTabEnum.RibbonStart && this.guiEnabled && this.mainControl.Image != null);
			switch (this.currentTabIndex)
			{
			case MainForm.RibbonTabEnum.RibbonStart:
				if (!this.panControl.Enabled)
				{
					this.panControl.Enabled = true;
				}
				this.lblNoPlan.Visible = (this.guiEnabled && this.project.Plans.Count == 0);
				this.mainControl.Visible = (this.guiEnabled && this.mainControl.Image != null);
				this.flowPlans.Visible = false;
				this.webBrowser.Visible = false;
				this.reportsControl.Visible = false;
				this.treeDBEstimatingItems.Visible = false;
				this.treeTemplatesLibrary.Visible = false;
				this.EnableDockingWindows(true, true);
				break;
			case MainForm.RibbonTabEnum.RibbonPlans:
				this.mainControl.Visible = false;
				this.webBrowser.Visible = false;
				this.reportsControl.Visible = false;
				this.lblNoPlan.Visible = (this.guiEnabled && this.project.Plans.Count == 0);
				this.flowPlans.Visible = (this.guiEnabled && this.project.Plans.Count > 0);
				this.panControl.Enabled = false;
				this.treeDBEstimatingItems.Visible = false;
				this.treeTemplatesLibrary.Visible = false;
				this.EnableDockingWindows(false, true);
				break;
			case MainForm.RibbonTabEnum.RibbonReport:
				this.mainControl.Visible = false;
				this.lblNoPlan.Visible = (this.guiEnabled && this.project.Plans.Count == 0);
				this.webBrowser.Visible = false;
				this.reportsControl.Visible = (this.guiEnabled && this.project.Plans.Count > 0);
				this.flowPlans.Visible = false;
				this.panControl.Enabled = false;
				this.treeDBEstimatingItems.Visible = false;
				this.treeTemplatesLibrary.Visible = false;
				this.EnableDockingWindows(false, true);
				break;
			case MainForm.RibbonTabEnum.RibbonTemplates:
				this.mainControl.Visible = false;
				this.reportsControl.Visible = false;
				this.webBrowser.Visible = false;
				this.flowPlans.Visible = false;
				this.panControl.Enabled = false;
				this.EnableDockingWindows(false, true);
				this.treeDBEstimatingItems.Visible = false;
				this.treeTemplatesLibrary.Visible = true;
				this.lblNoPlan.Visible = false;
				break;
			case MainForm.RibbonTabEnum.RibbonEstimating:
				this.mainControl.Visible = false;
				this.reportsControl.Visible = false;
				this.webBrowser.Visible = false;
				this.flowPlans.Visible = false;
				this.panControl.Enabled = false;
				this.EnableDockingWindows(false, true);
				this.treeDBEstimatingItems.Visible = true;
				this.treeTemplatesLibrary.Visible = false;
				this.lblNoPlan.Visible = false;
				break;
			}
			Utilities.ResumeDrawing(this);
			this.SelectStatusBar(this.currentTabIndex);
		}

		private void SelectStatusBar(MainForm.RibbonTabEnum tabindex)
		{
			this.lblOrtho.Visible = false;
			this.switchOrtho.Visible = false;
			this.lblStatusBarPadding2.Visible = false;
			this.lblImageQuality.Visible = false;
			this.qualitySlider.Visible = false;
			this.lblStatusPadding2.Visible = false;
			this.lblZoom.Visible = false;
			this.zoomSlider.Visible = false;
			switch (this.currentTabIndex)
			{
			case MainForm.RibbonTabEnum.RibbonStart:
				this.lblOrtho.Visible = true;
				this.switchOrtho.Visible = true;
				this.lblStatusBarPadding2.Visible = true;
				this.lblImageQuality.Visible = true;
				this.qualitySlider.Visible = true;
				this.lblStatusPadding2.Visible = true;
				this.lblZoom.Visible = true;
				this.zoomSlider.Visible = true;
				break;
			}
			this.barStatus.RecalcLayout();
		}

		private void SelectedRibbonTabChanged()
		{
			switch (this.currentTabIndex)
			{
			case MainForm.RibbonTabEnum.RibbonStart:
				if (!this.guiEnabled)
				{
					Utilities.SetObjectFocus(this.lstRecentProjects);
					return;
				}
				if (this.lblNoPlan.Visible)
				{
					Utilities.SetObjectFocus(this.lblNoPlan);
					return;
				}
				Utilities.SetObjectFocus(this.mainControl);
				return;
			case MainForm.RibbonTabEnum.RibbonPlans:
				if (!this.guiEnabled)
				{
					Utilities.SetObjectFocus(this.lstRecentProjects);
					return;
				}
				if (this.lblNoPlan.Visible)
				{
					Utilities.SetObjectFocus(this.lblNoPlan);
					return;
				}
				this.plansNavigator.ValidateThumbnails();
				return;
			case MainForm.RibbonTabEnum.RibbonReport:
				if (!this.guiEnabled)
				{
					Utilities.SetObjectFocus(this.lstRecentProjects);
					return;
				}
				if (this.lblNoPlan.Visible)
				{
					Utilities.SetObjectFocus(this.lblNoPlan);
					return;
				}
				if (this.reportsControl.Dirty)
				{
					base.Invalidate();
					Application.DoEvents();
					this.ReportRefresh();
				}
				Utilities.SetObjectFocus(this.reportsControl);
				return;
			case MainForm.RibbonTabEnum.RibbonTemplates:
				Utilities.SetObjectFocus(this.treeTemplatesLibrary);
				return;
			case MainForm.RibbonTabEnum.RibbonEstimating:
				Utilities.SetObjectFocus(this.treeDBEstimatingItems);
				break;
			case MainForm.RibbonTabEnum.RibbonExtensions:
				break;
			default:
				return;
			}
		}

		private void ForceTabSelection(MainForm.RibbonTabEnum tabindex)
		{
			this.exitNow = true;
			this.currentTabIndex = tabindex;
			switch (this.currentTabIndex)
			{
			case MainForm.RibbonTabEnum.RibbonStart:
				this.ribbonControl.SelectedRibbonTabItem = this.ribbonTabStart;
				break;
			case MainForm.RibbonTabEnum.RibbonPlans:
				this.ribbonControl.SelectedRibbonTabItem = this.ribbonTabPlans;
				break;
			case MainForm.RibbonTabEnum.RibbonReport:
				this.ribbonControl.SelectedRibbonTabItem = this.ribbonTabReport;
				break;
			case MainForm.RibbonTabEnum.RibbonTemplates:
				this.ribbonControl.SelectedRibbonTabItem = this.ribbonTabEstimatingItems;
				break;
			case MainForm.RibbonTabEnum.RibbonEstimating:
				this.ribbonControl.SelectedRibbonTabItem = this.ribbonTabTemplates;
				break;
			case MainForm.RibbonTabEnum.RibbonExtensions:
				this.ribbonControl.SelectedRibbonTabItem = this.ribbonTabExtensions;
				break;
			}
			this.EnableGUI(true);
			this.SelectedRibbonTabChanged();
			this.exitNow = false;
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			if (!this.guiReady)
			{
				return;
			}
			MainForm.RibbonTabEnum ribbonTabEnum = (MainForm.RibbonTabEnum)Utilities.ConvertToInt(this.ribbonControl.SelectedRibbonTabItem.Tag.ToString());
			if (ribbonTabEnum == this.currentTabIndex)
			{
				return;
			}
			this.currentTabIndex = ribbonTabEnum;
			this.FileWelcome(!this.guiEnabled && (this.currentTabIndex != MainForm.RibbonTabEnum.RibbonEstimating && this.currentTabIndex != MainForm.RibbonTabEnum.RibbonTemplates) && this.currentTabIndex != MainForm.RibbonTabEnum.RibbonExtensions);
			this.EnableGUI(this.guiEnabled);
			this.SelectedRibbonTabChanged();
		}

		private void panelDockProperties_Resize(object sender, EventArgs e)
		{
			this.tabProperties.Size = new Size(this.tabProperties.Width, this.panelDockProperties.Height - (this.cbObjects.Height + this.barDisplayResults.Height));
		}

		private void tabControlPanel1_Resize(object sender, EventArgs e)
		{
			this.superTabProperties.Location = new Point(0, 0);
			this.superTabProperties.Size = new Size(this.tabControlPanel1.Width, this.tabControlPanel1.Height);
		}

		private void panelWelcome_Resize(object sender, EventArgs e)
		{
			this.panelWelcomeMenu.Left = (this.panelWelcome.Width - this.panelWelcomeMenu.Width) / 2;
			this.panelWelcomeMenu.Top = (this.panelWelcome.Height - this.panelWelcomeMenu.Height) / 2;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			switch (this.currentTabIndex)
			{
			default:
				if (this.guiReady && base.WindowState == FormWindowState.Normal)
				{
					Settings.Default.WindowSize = base.Size;
					Settings.Default.WindowPosition = base.Location;
				}
				return;
			}
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			if (this.guiReady && base.WindowState == FormWindowState.Normal)
			{
				Settings.Default.WindowSize = base.Size;
				Settings.Default.WindowPosition = base.Location;
			}
		}

		private void FillPlanObjects(Plan plan)
		{
			int num = 0;
			this.cbObjects.BeginUpdate();
			this.treeObjects.BeginUpdate();
			this.treeEstimatingItems.BeginUpdate();
			this.objectEditor.Clear();
			this.objectsNavigator.Clear();
			this.estimatingEditor.Clear();
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				this.objectsNavigator.AddLayer(layer, num);
				for (int i = layer.DrawingObjects.Count - 1; i >= 0; i--)
				{
					DrawObject drawObject = layer.DrawingObjects[i];
					if (drawObject.DeductionParentID == -1 && !this.GroupExistsInGUI(drawObject))
					{
						this.objectEditor.Add(drawObject, false);
						this.objectsNavigator.AddObject(num, drawObject, false);
					}
				}
				num++;
			}
			this.objectsNavigator.ExpandAll();
			this.treeObjects.Refresh();
			this.cbObjects.EndUpdate();
			this.treeObjects.EndUpdate();
			this.treeObjects.RecalcLayout();
			this.estimatingEditor.RefreshAll();
			this.treeEstimatingItems.ExpandAll();
			this.treeEstimatingItems.EndUpdate();
		}

		private void FillPlanLayers(Plan plan, int selectedLayer)
		{
			int num = 0;
			this.layersNavigator.Clear();
			foreach (object obj in this.drawArea.ActivePlan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				this.layersNavigator.Add(layer, num++, num - 1 == selectedLayer);
			}
			this.lstLayers.RecalcLayout();
			this.lstLayers.RecalcLayout();
			this.lstLayers.Refresh();
		}

		private void BookmarkApply(Bookmark bookmark)
		{
			this.mainControl.Origin = new PointF((float)bookmark.Origin.X, (float)bookmark.Origin.Y);
			this.ZoomSet(bookmark.Zoom);
		}

		private bool ValidateRotation()
		{
			if (this.drawArea.DrawingBoard.Image == null)
			{
				return false;
			}
			if (this.drawArea.ActivePlan == null)
			{
				return false;
			}
			if (!this.drawArea.ActivePlan.Thumbnail.Dirty)
			{
				Console.WriteLine("!drawArea.ActivePlan.Thumbnail.Dirty");
				return false;
			}
			Console.WriteLine("Height = " + (float)this.drawArea.DrawingBoard.Image.Height);
			Console.WriteLine("Width = " + (float)this.drawArea.DrawingBoard.Image.Width);
			Console.WriteLine("Width * 1.10 = " + (float)this.drawArea.DrawingBoard.Image.Width * 1.1f);
			if ((float)this.drawArea.DrawingBoard.Image.Height <= (float)this.drawArea.DrawingBoard.Image.Width * 1.1f)
			{
				return false;
			}
			string ajuster_l_orientation_du_plan = Resources.Ajuster_l_orientation_du_plan;
			string text = Resources.Cliquez_Oui_pour_ajuster_l_orientation_du_plan;
			text += Resources.Cliquez_Non_pour_laisser_le_plan_tel_quel;
			if (Utilities.DisplayQuestion(ajuster_l_orientation_du_plan, text) == DialogResult.No)
			{
				return false;
			}
			this.ShowRotationControls();
			return true;
		}

		private void LoadPlan(Plan plan, bool refreshListOfPlans = true)
		{
			bool flag = plan == null || plan.Equals(this.drawArea.ActivePlan);
			if (flag)
			{
				this.UpdateTileBar();
				if (plan == null)
				{
					this.panControl.Clear();
				}
				this.ForceTabSelection(MainForm.RibbonTabEnum.RibbonStart);
				return;
			}
			this.drawArea.ActivePlan = plan;
			bool flag2 = false;
			string text = plan.FullFileName;
			if (plan.Brightness != 0 || plan.Contrast != 0)
			{
				flag2 = Utilities.FileExists(text + "~");
				text = (flag2 ? (text + "~") : text);
				if (flag2)
				{
					Console.WriteLine("Loading from cache...");
				}
			}
			Exception exception = null;
			if (!this.mainControl.OpenImageFile(text, plan.DefaultBookmark.Origin, plan.DefaultBookmark.Zoom, ref exception))
			{
				Utilities.DisplayFileOpenError(text, exception);
				this.drawArea.ActivePlan = null;
				this.UpdateTileBar();
				this.panControl.Clear();
				this.ForceTabSelection(MainForm.RibbonTabEnum.RibbonStart);
				return;
			}
			if ((plan.Brightness != 0 || plan.Contrast != 0) && !flag2)
			{
				this.mainControl.ApplyBrightnessAndContrast(plan.Brightness, plan.Contrast);
				plan.CreateCache((Bitmap)this.mainControl.Image);
			}
			this.panControl.Initialize(this.mainControl.DrawingBoard);
			this.drawArea.UnitScale.ReferenceDpiX = this.drawArea.DrawingBoard.OriginalDpiX;
			this.drawArea.UnitScale.ReferenceDpiY = this.drawArea.DrawingBoard.OriginalDpiY;
			if (this.currentTabIndex == MainForm.RibbonTabEnum.RibbonStart)
			{
				this.mainControl.Visible = true;
				this.mainControl.DrawingBoard.Refresh();
			}
			this.recentPlansNavigator.Add(plan, true);
			if (refreshListOfPlans)
			{
				this.objectsNavigator.RefreshListOfPlans(plan);
			}
			if (plan.Layers.Count == 0)
			{
				plan.CreateDefaultLayers();
			}
			this.drawArea.ValidateLegend(this.extensionsSupport);
			this.FillPlanLayers(plan, plan.DefaultBookmark.LayerIndex);
			this.FillPlanObjects(plan);
			this.UpdateSelectedObjects();
			this.UpdateTileBar();
			this.UpdateScaleSystemInGUI(false);
			this.UpdateScalePrecisionInGUI(false);
			this.RefreshObjects();
			this.ForceTabSelection(MainForm.RibbonTabEnum.RibbonStart);
			this.suspendScaleValidate = this.ValidateRotation();
			if (!this.suspendScaleValidate)
			{
				this.ScaleValidate();
			}
		}

		private void SuspendPlan()
		{
			if (this.drawArea.ActivePlan != null)
			{
				this.imageEditingBookmark = new Bookmark("", this.drawArea.ActiveLayerIndex, this.mainControl.Zoom, new Point((int)this.mainControl.Origin.X, (int)this.mainControl.Origin.Y));
				this.mainControl.Clear();
				this.mustRestorePlan = true;
				Console.WriteLine("SuspendPlan");
			}
		}

		private void RestorePlan()
		{
			if (this.mustRestorePlan)
			{
				if (this.drawArea.ActivePlan != null)
				{
					GC.Collect();
					if (this.ReloadImage(this.drawArea.ActivePlan.FullFileName, true))
					{
						this.RestoreBookmark();
					}
					else
					{
						this.UnloadPlan();
					}
					Application.DoEvents();
					Console.WriteLine("RestorePlan");
				}
				this.mustRestorePlan = false;
			}
		}

		private void UnloadPlan()
		{
			this.mainControl.Clear();
			this.panControl.Clear();
			this.ResetStatusBar();
			this.EnableGUI(true);
			this.drawArea.ActivePlan = null;
			this.SetModified();
		}

		private bool SaveIfModified()
		{
			if (!this.project.Dirty)
			{
				return true;
			}
			string sauvegarder_les_modifications = Resources.Sauvegarder_les_modifications;
			string vous_avez_fait_des_modifications_au_projet_courant = Resources.Vous_avez_fait_des_modifications_au_projet_courant;
			string sauvegarder = Resources.Sauvegarder;
			DialogResult dialogResult = Utilities.DisplayWarningQuestionYesNoCancelCustom(sauvegarder_les_modifications, vous_avez_fait_des_modifications_au_projet_courant, Resources.document_save_48x48, sauvegarder);
			DialogResult dialogResult2 = dialogResult;
			if (dialogResult2 == DialogResult.Cancel)
			{
				return false;
			}
			switch (dialogResult2)
			{
			case DialogResult.Yes:
				return this.FileSave(this.project.FullFileName);
			case DialogResult.No:
				return true;
			default:
				return false;
			}
		}

		private void FileNew()
		{
			this.backupTimer.Suspend = true;
			this.FileNewEx();
			this.backupTimer.Suspend = false;
		}

		private void FileNewEx()
		{
			bool flag = false;
			if (!this.isActivated && this.trialDaysRemaining == 0U)
			{
				return;
			}
			this.WaitForBackupCompletion();
			if (!this.SaveIfModified())
			{
				return;
			}
			this.FileClose(false);
			this.FileWelcome(false);
			if (!this.project.Create(this))
			{
				this.FileClose(false);
				this.FileWelcome(true);
				return;
			}
			this.LockInterface(ref flag);
			this.LoadReportSettings();
			this.SelectPointerTool();
			this.SetModified();
			this.ForceTabSelection(MainForm.RibbonTabEnum.RibbonPlans);
			this.UnlockInterface(ref flag);
		}

		private void FileOpen(string fileName)
		{
			this.backupTimer.Suspend = true;
			this.FileOpenEx(fileName);
			this.backupTimer.Suspend = false;
		}

		private void FileOpenEx(string fileName)
		{
			bool flag = false;
			if (!this.isActivated && this.trialDaysRemaining == 0U)
			{
				return;
			}
			this.WaitForBackupCompletion();
			if (!this.SaveIfModified())
			{
				return;
			}
			this.FileWelcome(false);
			string text = (fileName != string.Empty) ? fileName : Utilities.OpenFileDialog(Resources.Ouvrir_un_projet, Utilities.GetProjectsFolder(), Utilities.Projets_Quoter_Plan + " (*.qpl)|*.qpl");
			if (text == string.Empty)
			{
				this.FileWelcome(!this.guiEnabled);
				return;
			}
			this.FileClose(false);
			this.LockInterface(ref flag);
			this.panelWelcome.Visible = false;
			this.CheckForBackup(text);
			if (!this.project.Open(text))
			{
				this.mruManager.PurgeProject(text);
				this.mruManager.Populate(this.galleryRecentProjects, this.lstRecentProjects);
				this.FileClose(false);
				this.FileWelcome(true);
				this.UnlockInterface(ref flag);
				return;
			}
			this.recentPlansNavigator.LoadFromProject();
			this.LoadReportSettings();
			this.SynchPresets();
			Plan plan = this.project.Workspace.ActivePlan ?? ((this.project.Plans.Count > 0) ? this.project.Plans[0] : null);
			this.drawArea.ActivePlan = null;
			this.drawArea.ValidateLegends(this.extensionsSupport);
			this.LoadPlan(plan, true);
			this.SelectPointerTool();
			this.mruManager.Insert(text);
			this.mruManager.Populate(this.galleryRecentProjects, this.lstRecentProjects);
			this.objectsNavigator.RefreshListOfPlans(plan);
			this.UnlockInterface(ref flag);
		}

		private bool FileSave(string fileName)
		{
			this.backupTimer.Suspend = true;
			bool result = this.FileSaveEx(fileName);
			this.backupTimer.Suspend = false;
			return result;
		}

		private bool FileSaveEx(string fileName)
		{
			bool flag = false;
			bool flag2 = false;
			if (!this.isActivated && this.trialDaysRemaining == 0U)
			{
				return false;
			}
			this.WaitForBackupCompletion();
			string text;
			if (fileName != string.Empty)
			{
				text = fileName;
			}
			else
			{
				string text2;
				if (this.project.FileName == "")
				{
					text2 = this.project.Name + " (" + Utilities.GetDateString(this.project.CreationDate, Utilities.GetCurrentValidUICultureShort()).Replace(",", "").Replace(" ", "-") + ")";
				}
				else
				{
					text2 = this.project.FileName;
					int num = text2.ToLower().IndexOf("version");
					if (num > 0)
					{
						int num2 = 0;
						try
						{
							if (num < text2.Length)
							{
								num2 = Utilities.ConvertToInt(text2.Substring(num + 8, text2.Length - (num + 12)));
							}
						}
						catch
						{
						}
						num2++;
						text2 = string.Concat(new object[]
						{
							this.project.Name,
							" (",
							Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()).Replace(",", "").Replace(" ", "-"),
							") - Version ",
							(num2 < 2) ? 2 : num2
						});
					}
					else
					{
						text2 = this.project.Name + " (" + Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()).Replace(",", "").Replace(" ", "-") + ")";
						if ((text2 + ".qpl").ToLower() == this.project.FileName.ToLower())
						{
							text2 += " - Version 2";
						}
					}
				}
				string text3 = Path.Combine(this.project.CreationParentFolder, text2);
				text3 = Utilities.MakeUniqueFolderName(text3);
				if (!Utilities.ValidateDirectory(text3))
				{
					return false;
				}
				text = Path.Combine(text3, text2 + ".qpl");
				flag = true;
			}
			if (text == string.Empty)
			{
				return false;
			}
			this.LockInterface(ref flag2);
			Console.WriteLine("File Save IN");
			this.project.Workspace.ActivePlan = this.drawArea.ActivePlan;
			this.recentPlansNavigator.SaveToProject();
			this.reportsControl.ReportModule.CleanUpFilters();
			this.project.Save(text, false);
			this.UpdateTileBar();
			this.UpdateStatusBar(MainForm.ApplicationStatusEnum.StatusSaved);
			this.EnableGUI(true);
			if (flag)
			{
				this.mruManager.Insert(text);
				this.mruManager.Populate(this.galleryRecentProjects, this.lstRecentProjects);
			}
			this.DisableBackup();
			this.DeleteBackup();
			Console.WriteLine("File Save OUT");
			this.UnlockInterface(ref flag2);
			return true;
		}

		private void FileProjectInfo()
		{
			if (this.project.EditInfo(this))
			{
				if (this.currentTabIndex == MainForm.RibbonTabEnum.RibbonReport)
				{
					this.ReportRefresh();
				}
				this.SetModified();
			}
		}

		private void FileWelcome(bool bShow)
		{
			if (this.panelWelcome.Visible != bShow)
			{
				this.panelWelcome.Visible = bShow;
				Application.DoEvents();
				if (bShow)
				{
					Utilities.SetObjectFocus(this.lstRecentProjects);
				}
			}
		}

		private void FileClose(bool bSaveIfModified)
		{
			this.backupTimer.Suspend = true;
			this.FileCloseEx(bSaveIfModified);
			this.backupTimer.Suspend = false;
		}

		private void FileCloseEx(bool bSaveIfModified)
		{
			bool flag = false;
			this.WaitForBackupCompletion();
			if (bSaveIfModified && !this.SaveIfModified())
			{
				Utilities.SetObjectFocus(this);
				return;
			}
			this.DisableBackup();
			this.DeleteBackup();
			this.LockInterface(ref flag);
			this.mainControl.Visible = false;
			this.flowPlans.Visible = false;
			this.webBrowser.Visible = false;
			this.reportsControl.Visible = false;
			this.project.Clear();
			this.mainControl.Clear();
			this.panControl.Clear();
			this.drawArea.Clear();
			this.estimatingEditor.Clear();
			this.ResetReport();
			this.ResetToolsFilter();
			this.UpdateTileBar();
			this.ResetStatusBar();
			this.ResetLayout();
			this.UpdateStatusBar(MainForm.ApplicationStatusEnum.StatusReady);
			this.EnableGUI(false);
			this.drawArea.ActivePlan = null;
			this.mustRestorePlan = false;
			this.ResetHelp();
			this.ribbonControl.SelectFirstVisibleRibbonTab();
			this.UnlockInterface(ref flag);
			Utilities.SetObjectFocus(this.lstRecentProjects);
		}

		private void FileExit()
		{
			base.Close();
		}

		private void EnableAutoBackup(bool value)
		{
			Settings.Default.EnableAutoBackup = value;
			this.btEnableAutoBackup.Checked = value;
		}

		private void HelpAbout()
		{
			try
			{
				using (AboutForm aboutForm = new AboutForm(false, this.GetTurboActivateKey()))
				{
					aboutForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void HelpContent()
		{
			this.helpUtilities.ShowContent();
		}

		private void SelectPointerTool()
		{
			this.drawArea.SelectPointerTool();
		}

		private void SelectPanTool()
		{
			this.drawArea.SelectPanTool();
		}

		private int SelectToolPreset(DrawObject drawObject, Template template, bool editFromLibrary = false, bool duplicate = false)
		{
			UnitScale unitScale;
			if (editFromLibrary)
			{
				unitScale = new UnitScale(1f, UnitScale.DefaultUnitSystem(), UnitScale.DefaultUnitPrecision(), false);
			}
			else
			{
				unitScale = this.drawArea.UnitScale;
			}
			new Preset(unitScale.CurrentSystemType);
			string template2 = string.Empty;
			if (template != null)
			{
				template2 = template.ID;
			}
			DrawObjectGroup drawObjectGroup = new DrawObjectGroup(this.drawArea.GetNextGroupID(), drawObject.Name, drawObject.ObjectType, template2);
			if (template != null)
			{
				foreach (object obj in template.Presets.Collection)
				{
					Preset preset = (Preset)obj;
					Preset preset2 = preset.Clone(false);
					preset2.SynchWithTemplate(this.extensionsSupport);
					preset2.SetCustomRendering();
					drawObjectGroup.Presets.Add(preset2);
				}
				foreach (CEstimatingItem cestimatingItem in template.EstimatingItems.Collection)
				{
					CEstimatingItem cestimatingItem2 = new CEstimatingItem(cestimatingItem.ItemID, cestimatingItem.Description, cestimatingItem.Value, cestimatingItem.Unit, cestimatingItem.ItemType, cestimatingItem.UnitMeasure, cestimatingItem.CoverageValue, cestimatingItem.CoverageUnit, cestimatingItem.SectionID, cestimatingItem.SubSectionID, cestimatingItem.BidCode, cestimatingItem.Formula);
					cestimatingItem2.Tag = cestimatingItem2;
					drawObjectGroup.EstimatingItems.Add(cestimatingItem2);
				}
			}
			if (template != null)
			{
				foreach (CEstimatingItem cestimatingItem3 in template.COfficeProducts.Collection)
				{
					CEstimatingItem cestimatingItem4 = new CEstimatingItem(cestimatingItem3.ItemID, cestimatingItem3.Description, cestimatingItem3.Value, cestimatingItem3.Unit, cestimatingItem3.ItemType, cestimatingItem3.UnitMeasure, cestimatingItem3.CoverageValue, cestimatingItem3.CoverageUnit, cestimatingItem3.SectionID, cestimatingItem3.SubSectionID, cestimatingItem3.BidCode, cestimatingItem3.Formula);
					cestimatingItem4.Tag = cestimatingItem4;
					drawObjectGroup.COfficeProducts.Add(cestimatingItem4);
				}
			}
			try
			{
				using (ToolEditForm toolEditForm = new ToolEditForm(this, this.drawArea, drawObjectGroup, drawObject, template, this.extensionsSupport, this.templatesSupport, this.templatesLibraryEditor, unitScale, editFromLibrary, duplicate))
				{
					toolEditForm.HelpUtilities = this.HelpUtilities;
					toolEditForm.HelpContextString = "ToolEditForm" + drawObject.ObjectType;
					toolEditForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return -1;
			}
			if (!drawObject.Dirty)
			{
				return -1;
			}
			if (!editFromLibrary)
			{
				this.project.Groups.Add(drawObjectGroup);
			}
			return drawObjectGroup.ID;
		}

		private void SelectAreaTool(Template template, Color defaultColor, bool editFromLibrary = false, bool duplicate = false)
		{
			Color color = (defaultColor == Color.Empty) ? ((template == null) ? this.drawArea.GetNextColor("Area") : template.DrawObject.Color) : defaultColor;
			HatchStylePickerCombo.HatchStylePickerEnum pattern = (template == null) ? HatchStylePickerCombo.HatchStylePickerEnum.Solid : ((DrawPolyLine)template.DrawObject).Pattern;
			DrawPolyLine drawPolyLine = new DrawPolyLine();
			if (!editFromLibrary)
			{
				drawPolyLine.Name = ((template == null) ? this.drawArea.GetFreeObjectName(Resources.Surface) : this.drawArea.GetFreeTemplateObjectName(template.DrawObject.Name));
			}
			else
			{
				drawPolyLine.Name = ((template == null) ? this.templatesSupport.GetFreeTemplateObjectName("Area", Resources.Surface) : (duplicate ? this.templatesSupport.GetFreeTemplateObjectName("Area", template.DrawObject.Name) : template.DrawObject.Name));
			}
			drawPolyLine.Comment = ((template == null) ? "" : template.DrawObject.Comment);
			drawPolyLine.Color = color;
			drawPolyLine.FillColor = color;
			drawPolyLine.Filled = true;
			drawPolyLine.Pattern = pattern;
			if (template != null)
			{
				drawPolyLine.SetSlopeFactor(template.DrawObject.SlopeFactor);
			}
			int num = this.SelectToolPreset(drawPolyLine, template, editFromLibrary, duplicate);
			if (num == -1)
			{
				return;
			}
			if (editFromLibrary)
			{
				return;
			}
			this.UnselectAll();
			this.drawArea.SelectAreaTool(drawPolyLine, drawPolyLine.Name, num, drawPolyLine.Comment, drawPolyLine.Color, drawPolyLine.Pattern, this.drawArea.ActiveLayer.Opacity, drawPolyLine.SlopeFactor, true, true);
		}

		private void SelectPerimeterTool(Template template, Color defaultColor, bool editFromLibrary = false, bool duplicate = false)
		{
			Color color = (defaultColor == Color.Empty) ? ((template == null) ? this.drawArea.GetNextColor("Perimeter") : template.DrawObject.Color) : defaultColor;
			DrawPolyLine drawPolyLine = new DrawPolyLine();
			if (!editFromLibrary)
			{
				drawPolyLine.Name = ((template == null) ? this.drawArea.GetFreeObjectName(Resources.Périmètre) : this.drawArea.GetFreeTemplateObjectName(template.DrawObject.Name));
			}
			else
			{
				drawPolyLine.Name = ((template == null) ? this.templatesSupport.GetFreeTemplateObjectName("Perimeter", Resources.Périmètre) : (duplicate ? this.templatesSupport.GetFreeTemplateObjectName("Perimeter", template.DrawObject.Name) : template.DrawObject.Name));
			}
			drawPolyLine.Comment = ((template == null) ? "" : template.DrawObject.Comment);
			drawPolyLine.Color = color;
			drawPolyLine.FillColor = color;
			drawPolyLine.PenWidth = ((template == null) ? 6 : template.DrawObject.PenWidth);
			drawPolyLine.Filled = false;
			if (template != null)
			{
				drawPolyLine.SetSlopeFactor(template.DrawObject.SlopeFactor);
			}
			int num = this.SelectToolPreset(drawPolyLine, template, editFromLibrary, duplicate);
			if (num == -1)
			{
				return;
			}
			if (editFromLibrary)
			{
				return;
			}
			this.UnselectAll();
			this.drawArea.SelectPerimeterTool(drawPolyLine, drawPolyLine.Name, num, drawPolyLine.Comment, drawPolyLine.Color, this.drawArea.ActiveLayer.Opacity, drawPolyLine.PenWidth, drawPolyLine.SlopeFactor, true, true);
		}

		private void SelectCounterTool(Template template, Color defaultColor, bool editFromLibrary = false, bool duplicate = false)
		{
			Color color = (defaultColor == Color.Empty) ? ((template == null) ? this.drawArea.GetNextColor("Counter") : template.DrawObject.Color) : defaultColor;
			string text = "";
			DrawCounter drawCounter = new DrawCounter();
			if (!editFromLibrary)
			{
				drawCounter.Name = ((template == null) ? this.drawArea.GetFreeObjectName(Resources.Compteur, ref text) : this.drawArea.GetFreeTemplateObjectName(template.DrawObject.Name));
			}
			else
			{
				drawCounter.Name = ((template == null) ? this.templatesSupport.GetFreeTemplateObjectName("Counter", Resources.Compteur, ref text) : (duplicate ? this.templatesSupport.GetFreeTemplateObjectName("Counter", template.DrawObject.Name) : template.DrawObject.Name));
			}
			drawCounter.Comment = ((template == null) ? "" : template.DrawObject.Comment);
			drawCounter.Text = ((template == null) ? text : template.DrawObject.Text);
			drawCounter.Color = color;
			drawCounter.Shape = ((template == null) ? DrawCounter.CounterShapeTypeEnum.CounterShapeCircle : ((DrawCounter)template.DrawObject).Shape);
			drawCounter.DefaultSize = ((template == null) ? 80 : ((DrawCounter)template.DrawObject).DefaultSize);
			int num = this.SelectToolPreset(drawCounter, template, editFromLibrary, duplicate);
			if (num == -1)
			{
				return;
			}
			if (editFromLibrary)
			{
				return;
			}
			this.UnselectAll();
			this.drawArea.SelectCounterTool(drawCounter, drawCounter.Name, num, drawCounter.Text, drawCounter.DefaultSize, drawCounter.Shape, drawCounter.Comment, drawCounter.Color, this.drawArea.ActiveLayer.Opacity, true);
		}

		private void SelectDistanceTool(Template template, Color defaultColor, bool editFromLibrary = false, bool duplicate = false)
		{
			Color color = (defaultColor == Color.Empty) ? ((template == null) ? this.drawArea.GetNextColor("Line") : template.DrawObject.Color) : defaultColor;
			DrawLine drawLine = new DrawLine();
			if (!editFromLibrary)
			{
				drawLine.Name = ((template == null) ? this.drawArea.GetFreeObjectName(Resources.Distance) : this.drawArea.GetFreeTemplateObjectName(template.DrawObject.Name));
			}
			else
			{
				drawLine.Name = ((template == null) ? this.templatesSupport.GetFreeTemplateObjectName("Line", Resources.Distance) : (duplicate ? this.templatesSupport.GetFreeTemplateObjectName("Line", template.DrawObject.Name) : template.DrawObject.Name));
			}
			drawLine.Comment = ((template == null) ? "" : template.DrawObject.Comment);
			drawLine.Color = color;
			drawLine.PenWidth = ((template == null) ? 6 : template.DrawObject.PenWidth);
			if (template != null)
			{
				drawLine.SetSlopeFactor(template.DrawObject.SlopeFactor);
			}
			int num = this.SelectToolPreset(drawLine, template, editFromLibrary, duplicate);
			if (num == -1)
			{
				return;
			}
			if (editFromLibrary)
			{
				return;
			}
			this.UnselectAll();
			this.drawArea.SelectDistanceTool(drawLine, drawLine.Name, num, drawLine.Comment, drawLine.Color, this.drawArea.ActiveLayer.Opacity, drawLine.PenWidth, drawLine.SlopeFactor, true, true);
		}

		private void SelectAngleTool()
		{
			this.UnselectAll();
			this.drawArea.SelectAngleTool(this.drawArea.GetFreeObjectName(Resources.Angle), "", this.drawArea.GetAngleDefaultColor(), this.drawArea.ActiveLayer.Opacity, true);
		}

		private void SelectMarkerTool()
		{
			this.UnselectAll();
			this.drawArea.SelectMarkerTool(this.drawArea.GetFreeObjectName(Resources.Marqueur), "", this.drawArea.GetMarkerDefaultColor(), this.drawArea.ActiveLayer.Opacity, true);
		}

		private bool NoteEditForm(DrawNote drawNote, bool creationMode)
		{
			if (drawNote == null)
			{
				return false;
			}
			bool dirty = drawNote.Dirty;
			drawNote.Dirty = false;
			try
			{
				using (NoteEditForm noteEditForm = new NoteEditForm(drawNote, creationMode))
				{
					noteEditForm.HelpUtilities = this.HelpUtilities;
					noteEditForm.HelpContextString = "NoteEditForm";
					noteEditForm.ShowDialog(base.Owner);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return false;
			}
			bool dirty2 = drawNote.Dirty;
			drawNote.Dirty = dirty;
			return dirty2;
		}

		private void SelectNoteTool()
		{
			DrawNote drawNote = new DrawNote();
			if (!this.NoteEditForm(drawNote, true))
			{
				return;
			}
			this.UnselectAll();
			this.drawArea.SelectNoteTool(this.drawArea.GetFreeObjectName(Resources.Note), drawNote.Comment, this.drawArea.GetNoteDefaultColor(), this.drawArea.ActiveLayer.Opacity, true);
		}

		public void EnableScaleSubMenu()
		{
			this.btScaleSet.ShowSubItems = (this.drawArea.UnitScale.Scale != 0f);
		}

		public void UpdateScaleSystemInGUI(bool refreshGUI)
		{
			this.btScaleImperial.Checked = false;
			this.btScaleMetric.Checked = false;
			switch (this.drawArea.UnitScale.CurrentSystemType)
			{
			case UnitScale.UnitSystem.metric:
				this.btScaleMetric.Checked = true;
				break;
			case UnitScale.UnitSystem.imperial:
				this.btScaleImperial.Checked = true;
				break;
			}
			this.EnableScaleSubMenu();
			if (refreshGUI)
			{
				this.RefreshObjects();
			}
			this.Refresh();
		}

		private void UpdateScalePrecisionInGUI(bool refreshGUI)
		{
			this.btScalePrecision8.Checked = false;
			this.btScalePrecision16.Checked = false;
			this.btScalePrecision32.Checked = false;
			this.btScalePrecision64.Checked = false;
			switch (this.drawArea.UnitScale.Precision)
			{
			case UnitScale.UnitPrecision.precision8:
				this.btScalePrecision8.Checked = true;
				break;
			case UnitScale.UnitPrecision.precision16:
				this.btScalePrecision16.Checked = true;
				break;
			case UnitScale.UnitPrecision.precision32:
				this.btScalePrecision32.Checked = true;
				break;
			case UnitScale.UnitPrecision.precision64:
				this.btScalePrecision64.Checked = true;
				break;
			}
			if (refreshGUI)
			{
				this.RefreshObjects();
			}
			this.Refresh();
		}

		private void SetScaleCurrentSystemType(UnitScale.UnitSystem systemType)
		{
			this.drawArea.UnitScale.CurrentSystemType = systemType;
			this.UpdateScaleSystemInGUI(true);
			this.SetModified();
		}

		private void SetScalePrecision(UnitScale.UnitPrecision precision)
		{
			this.drawArea.UnitScale.Precision = precision;
			this.UpdateScalePrecisionInGUI(true);
			this.SetModified();
		}

		private void ScaleValidate()
		{
			this.drawArea.ScaleValidate();
		}

		private void CutSelectedObjects()
		{
			if (this.drawArea.SelectionCount == 0 || this.DeductionsEditing)
			{
				return;
			}
			if (this.drawArea.SelectionCount == 1 && this.drawArea.SelectedLegend != null)
			{
				return;
			}
			this.drawArea.UnselectLegend();
			this.Clipboard.Cut(this.drawArea);
			this.drawArea.Delete(false);
			this.SelectPointerTool();
			this.EnableEditCommands(true);
			this.SetModified();
			this.Refresh();
		}

		private void CopySelectedObjects()
		{
			if (this.drawArea.SelectionCount == 0 || this.DeductionsEditing)
			{
				return;
			}
			if (this.drawArea.SelectionCount == 1 && this.drawArea.SelectedLegend != null)
			{
				return;
			}
			this.drawArea.UnselectLegend();
			this.Clipboard.Copy(this.drawArea);
			this.EnableEditCommands(true);
			this.Refresh();
		}

		private void PasteSelectedObjects()
		{
			if (this.Clipboard.Objects.Count == 0 || this.DeductionsEditing)
			{
				return;
			}
			this.UnselectAll();
			this.Clipboard.Paste(this.drawArea, true);
			this.drawArea.AddCommandToHistory(new CommandPaste(this.drawArea, this.drawArea.ActiveLayerName));
			this.ReloadObjectsInGUI();
			this.SelectPointerTool();
			this.UpdateSelectedObjects();
			this.SetModified();
			this.Refresh();
		}

		private bool DeleteObjectsConfirmation()
		{
			int selectionCount = this.drawArea.SelectionCount;
			string title = Resources.Supprimer_l_objet_sélectionné;
			string message = Resources.L_objet_sélectionné_sera_supprimé_du_plan;
			if (selectionCount > 1)
			{
				title = Resources.Supprimer_les_objets_sélectionnés;
				if (selectionCount == this.drawArea.ObjectsCount())
				{
					message = Resources.Tous_les_objets_du_plan_seront_supprimés;
				}
				else if (selectionCount == this.drawArea.ObjectsCount(this.drawArea.ActiveLayerIndex))
				{
					message = Resources.Tous_les_objets_du_calque_seront_supprimés;
				}
				else
				{
					bool flag = false;
					int num = GroupUtilities.GroupCount(this.drawArea.ActiveLayer, this.drawArea.SelectedObject, true, "");
					if (selectionCount == num)
					{
						flag = GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, this.drawArea.SelectedObject);
					}
					if (selectionCount == num && flag)
					{
						message = ((num == GroupUtilities.GroupCount(this.project, this.drawArea.SelectedObject, null, true, "")) ? Resources.Tous_les_objets_du_groupe_seront_supprimés : Resources.Tous_les_objets_du_groupe_seront_supprimés_pour_ce_plan);
					}
					else
					{
						message = string.Format(Resources.n_objets_seront_supprimés_du_plan, selectionCount);
					}
				}
			}
			return Utilities.DisplayDeleteConfirmation(title, message) == DialogResult.Yes;
		}

		private void DeleteSelectedObjects(bool askConfirmation)
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return;
			}
			if (this.drawArea.SelectionCount == 1 && this.drawArea.SelectedLegend != null)
			{
				return;
			}
			this.drawArea.UnselectLegend();
			if (askConfirmation && !this.DeleteObjectsConfirmation())
			{
				return;
			}
			this.drawArea.Delete(this.drawArea.DeductionParent == null);
			this.ReloadObjectsInGUI();
			this.EnableEditCommands(true);
			this.SetModified();
			this.Refresh();
		}

		private void MoveObjectsZOrder(bool bringToFront)
		{
			if (this.drawArea.SelectionCount == 0 || this.DeductionsEditing)
			{
				return;
			}
			CommandChangeStateEx commandChangeStateEx = new CommandChangeStateEx(this.drawArea, this.drawArea.ActiveLayerName, !bringToFront, bringToFront);
			this.Clipboard.Cut(this.drawArea);
			this.drawArea.Delete(false);
			this.UnselectAll();
			this.Clipboard.Paste(this.drawArea, bringToFront);
			commandChangeStateEx.NewState(this.drawArea.ActiveLayerName);
			this.drawArea.AddCommandToHistory(commandChangeStateEx);
			this.ReloadObjectsInGUI();
			this.UpdateSelectedObjects();
			this.SetModified();
			this.Refresh();
		}

		private void BringObjectsToFront()
		{
			this.MoveObjectsZOrder(true);
		}

		private void SendObjectsToBack()
		{
			this.MoveObjectsZOrder(false);
		}

		private void DoUndoRedo(bool doUndo)
		{
			try
			{
				this.drawArea.UnselectAll();
				if (doUndo)
				{
					this.drawArea.Undo();
				}
				else
				{
					this.drawArea.Redo();
				}
				this.UpdateSelectedObjects();
				this.SetModified();
				this.Refresh();
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void Undo()
		{
			if (this.drawArea.CanUndo)
			{
				this.DoUndoRedo(true);
			}
		}

		private void Redo()
		{
			if (this.drawArea.CanRedo)
			{
				this.DoUndoRedo(false);
			}
		}

		private void SendDataToProcess(IntPtr handle, object value)
		{
			try
			{
				Utilities.CopyToClipboard(Utilities.ConvertToString(value, string.Empty), TextDataFormat.UnicodeText);
				Utilities.ForceWindowToForeground(handle);
				Thread.Sleep(350);
				SendKeys.Send("^v");
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void SendDataToExternalApplication(object value)
		{
			IntPtr nextWindow = Utilities.GetNextWindow();
			this.SendDataToProcess(nextWindow, value);
		}

		private void SendDataMenuAddItem(ButtonItem menuItem, int index, string caption, string formattedValue, object value)
		{
			ButtonItem buttonItem = new ButtonItem("bEditSendData" + index);
			buttonItem.ButtonStyle = eButtonStyle.Default;
			buttonItem.CanCustomize = false;
			buttonItem.Text = caption + " [" + formattedValue + "]";
			buttonItem.Tag = value;
			buttonItem.Click += this.btEditSendData_Click;
			menuItem.SubItems.Add(buttonItem);
		}

		private void BuildSendDataMenu(ButtonItem menuItem)
		{
			if (this.drawArea.ActivePlan == null)
			{
				return;
			}
			if (this.drawArea.SelectedObject == null)
			{
				return;
			}
			menuItem.SubItems.Clear();
			Plan activePlan = this.drawArea.ActivePlan;
			DrawObject selectedObject = this.drawArea.SelectedObject;
			UnitScale.UnitSystem currentSystemType = activePlan.UnitScale.CurrentSystemType;
			UnitScale.UnitPrecision precision = activePlan.UnitScale.Precision;
			UnitScale unitScale = new UnitScale(1f, currentSystemType, precision, false);
			GroupStats groupStats = GroupUtilities.ComputeGroupStats(activePlan, selectedObject, currentSystemType, true, "");
			LabelItem labelItem = new LabelItem("lblEditSendData", selectedObject.Name);
			labelItem.BackColor = this.lblSystemType.BackColor;
			labelItem.ForeColor = this.lblSystemType.ForeColor;
			labelItem.BorderSide = this.lblSystemType.BorderSide;
			labelItem.BorderType = this.lblSystemType.BorderType;
			labelItem.Height = this.lblSystemType.Height;
			labelItem.PaddingTop = this.lblSystemType.PaddingTop;
			labelItem.PaddingLeft = this.lblSystemType.PaddingLeft;
			labelItem.PaddingRight = this.lblSystemType.PaddingRight;
			labelItem.PaddingBottom = this.lblSystemType.PaddingBottom;
			labelItem.CanCustomize = false;
			menuItem.SubItems.Add(labelItem);
			string objectType;
			if ((objectType = selectedObject.ObjectType) != null)
			{
				if (objectType == "Line")
				{
					this.SendDataMenuAddItem(menuItem, 1, Resources.Longueur, unitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true), Math.Round(groupStats.Perimeter, (int)unitScale.Precision));
					return;
				}
				if (objectType == "Area")
				{
					this.SendDataMenuAddItem(menuItem, 1, Resources.Surface, unitScale.ToAreaStringFromUnitSystem(groupStats.Area, true), Math.Round(groupStats.Area, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 2, Resources.Déduction, unitScale.ToAreaStringFromUnitSystem(groupStats.DeductionArea, true), Math.Round(groupStats.DeductionArea, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 3, Resources.Surface_déductions, unitScale.ToAreaStringFromUnitSystem(groupStats.AreaMinusDeduction, true), Math.Round(groupStats.AreaMinusDeduction, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 4, Resources.Périmètre, unitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true), Math.Round(groupStats.Perimeter, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 5, Resources.Périmètre_déductions, unitScale.ToLengthStringFromUnitSystem(groupStats.PerimeterPlusDeduction, false, true, true), Math.Round(groupStats.PerimeterPlusDeduction, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 6, Resources.Nombre_de_déductions, this.drawArea.ToUnitString(groupStats.DeductionsCount), groupStats.DeductionsCount);
					return;
				}
				if (objectType == "Perimeter")
				{
					this.SendDataMenuAddItem(menuItem, 1, Resources.Longueur, unitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true), Math.Round(groupStats.Perimeter, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 2, Resources.Longueur_des_ouvertures, unitScale.ToLengthStringFromUnitSystem(groupStats.DeductionPerimeter, false, true, true), Math.Round(groupStats.DeductionPerimeter, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 3, Resources.Longueur_sans_ouvertures, unitScale.ToLengthStringFromUnitSystem(groupStats.NetLength, false, true, true), Math.Round(groupStats.NetLength, (int)unitScale.Precision));
					this.SendDataMenuAddItem(menuItem, 4, Resources.Nombre_d_ouvertures, this.drawArea.ToUnitString(groupStats.DeductionsCount), groupStats.DeductionsCount);
					this.SendDataMenuAddItem(menuItem, 5, Resources.Nombre_de_coins, this.drawArea.ToUnitString(groupStats.CornersCount), groupStats.CornersCount);
					this.SendDataMenuAddItem(menuItem, 6, Resources.Nombre_de_bouts, this.drawArea.ToUnitString(groupStats.EndsCount), groupStats.EndsCount);
					this.SendDataMenuAddItem(menuItem, 6, Resources.Nombre_de_segments, this.drawArea.ToUnitString(groupStats.SegmentsCount), groupStats.SegmentsCount);
					return;
				}
				if (!(objectType == "Counter"))
				{
					return;
				}
				this.SendDataMenuAddItem(menuItem, 1, Resources.Nombre_d_objets, this.drawArea.ToUnitString(groupStats.GroupCount), groupStats.GroupCount);
			}
		}

		private void CenterObject(Point objectCenter)
		{
			Rectangle rectangle = new Rectangle(0, 0, (int)((float)this.drawArea.ClientSize.Width / this.drawArea.ZoomFactor), (int)((float)this.drawArea.ClientSize.Height / this.drawArea.ZoomFactor));
			Point p = new Point(objectCenter.X - (int)((float)this.drawArea.ClientSize.Width / this.drawArea.ZoomFactor) / 2, objectCenter.Y - (int)((float)this.drawArea.ClientSize.Height / this.drawArea.ZoomFactor) / 2);
			if (p.X < 0)
			{
				p.X = 0;
			}
			if (p.X + rectangle.Width > this.drawArea.DrawingBoard.Image.Width)
			{
				p.X = this.drawArea.DrawingBoard.Image.Width - rectangle.Width;
			}
			if (p.Y < 0)
			{
				p.Y = 0;
			}
			if (p.Y + rectangle.Height > this.drawArea.DrawingBoard.Image.Height)
			{
				p.Y = this.drawArea.DrawingBoard.Image.Height - rectangle.Height;
			}
			this.drawArea.DrawingBoard.Origin = p;
		}

        private void ZoomObject(DrawObject drawObject, bool selectGroup)
        {
            if (drawObject == null)
                return;

            Layer objectLayer = this.drawArea.GetObjectLayer(drawObject);
            if (objectLayer == null)
                return;

            Point minPoint = Point.Empty;
            Point maxPoint = Point.Empty;
            Rectangle boundingRectangle;

            if (selectGroup)
            {
                foreach (object item in objectLayer.DrawingObjects.Collection)
                {
                    DrawObject candidate = (DrawObject)item;

                    if (!candidate.IsDeduction() &&
                        candidate.HasSameGroupOrID(drawObject) &&
                        candidate.Center != Point.Empty)
                    {
                        boundingRectangle = candidate.BoundingRectangle;

                        if (minPoint == Point.Empty)
                        {
                            minPoint.X = boundingRectangle.X;
                            minPoint.Y = boundingRectangle.Y;
                        }
                        else
                        {
                            if (boundingRectangle.X < minPoint.X)
                                minPoint.X = boundingRectangle.X;

                            if (boundingRectangle.Y < minPoint.Y)
                                minPoint.Y = boundingRectangle.Y;
                        }

                        int right = boundingRectangle.X + boundingRectangle.Width;
                        int bottom = boundingRectangle.Y + boundingRectangle.Height;

                        if (right > maxPoint.X)
                            maxPoint.X = right;

                        if (bottom > maxPoint.Y)
                            maxPoint.Y = bottom;
                    }
                }
            }
            else
            {
                boundingRectangle = drawObject.BoundingRectangle;

                minPoint.X = boundingRectangle.X;
                minPoint.Y = boundingRectangle.Y;

                maxPoint.X = boundingRectangle.X + boundingRectangle.Width;
                maxPoint.Y = boundingRectangle.Y + boundingRectangle.Height;
            }

            Rectangle selectionRectangle = new Rectangle(
                minPoint.X - (int)this.drawArea.DrawingBoard.Origin.X - 50,
                minPoint.Y - (int)this.drawArea.DrawingBoard.Origin.Y - 50,
                Math.Abs(maxPoint.X - minPoint.X) + 100,
                Math.Abs(maxPoint.Y - minPoint.Y) + 100);

            this.drawArea.DrawingBoard.ZoomSelection(selectionRectangle);

            Point selectionCenter = new Point(
                minPoint.X + Math.Abs(maxPoint.X - minPoint.X) / 2,
                minPoint.Y + Math.Abs(maxPoint.Y - minPoint.Y) / 2);

            this.CenterObject(selectionCenter);
        }

        private void EnsureObjectVisible(DrawObject drawObject, bool forceDisplay)
		{
			if (drawObject.IsDisplayed() && !forceDisplay)
			{
				return;
			}
			Rectangle boundingRectangle = drawObject.BoundingRectangle;
			Point point = new Point(boundingRectangle.X, boundingRectangle.Y);
			Point point2 = new Point(boundingRectangle.X + boundingRectangle.Width, boundingRectangle.Y + boundingRectangle.Height);
			Point objectCenter = new Point(point.X + Math.Abs(point2.X - point.X) / 2, point.Y + Math.Abs(point2.Y - point.Y) / 2);
			this.CenterObject(objectCenter);
		}

		private void SelectAll()
		{
			if (this.DeductionsEditing)
			{
				return;
			}
			this.drawArea.SelectAll();
			this.UpdateSelectedObjects();
		}

		private void UnselectAll()
		{
			if (this.DeductionsEditing)
			{
				return;
			}
			this.drawArea.UnselectAll();
			this.objectEditor.ForceSelection(null);
			this.EnableEditCommands(true);
		}

		private void SelectObjectType(string objectType)
		{
			if (GroupUtilities.ObjectsOfThisTypeCount(this.drawArea.ActiveLayer, objectType) == 0)
			{
				return;
			}
			this.drawArea.SelectObjectType(objectType);
			this.UpdateSelectedObjects();
		}

		private void SelectThisGroup(int groupID)
		{
			this.drawArea.SelectThisGroup(groupID);
			this.UpdateSelectedObjects();
		}

		private void PrintExportWarning(Graphics g, Image image)
		{
			try
			{
				using (StringFormat stringFormat = new StringFormat())
				{
					stringFormat.Alignment = StringAlignment.Center;
					stringFormat.LineAlignment = StringAlignment.Center;
					Font font = new Font("Tahoma", (float)image.Width / 300f, FontStyle.Bold);
					SizeF sizeF = g.MeasureString(Utilities.Pdf_export_warning, font);
					int num = sizeF.ToSize().Width + 6;
					int num2 = sizeF.ToSize().Height + 6;
					Rectangle rect = new Rectangle((image.Width - num) / 2, image.Height - num2 - 20, num, num2);
					g.DrawRectangle(new Pen(Color.Black), rect);
					g.FillRectangle(Brushes.Black, rect);
					g.DrawString(Utilities.Pdf_export_warning, font, Brushes.White, new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height), stringFormat);
					font.Dispose();
				}
			}
			catch
			{
			}
		}

		private Image PrintPlanImage(Bitmap sourceImage, Plan plan)
		{
			int num = (int)((float)sourceImage.Width * (72f / sourceImage.HorizontalResolution));
			int num2 = (int)((float)sourceImage.Height * (72f / sourceImage.VerticalResolution));
			int num3 = 3000;
			if (Math.Max(num, num2) < num3)
			{
				float num4 = (float)num3 / (float)Math.Max(num, num2);
				num = (int)((float)num * num4);
				num2 = (int)((float)num2 * num4);
			}
			Image image = null;
			try
			{
				int num5 = sourceImage.Width;
				int num6 = sourceImage.Height;
				if (num5 > num)
				{
					num6 = num6 * num / num5;
					num5 = num;
				}
				if (num6 > num2)
				{
					num5 = num5 * num2 / num6;
					num6 = num2;
				}
				double num7 = (double)num5 / (double)sourceImage.Width;
				double num8 = (double)num6 / (double)sourceImage.Height;
				double num9 = (num7 < num8) ? num7 : num8;
				image = new Bitmap((int)((double)sourceImage.Width * num9), (int)((float)sourceImage.Height * (float)num9), PixelFormat.Format24bppRgb);
				using (Graphics graphics = Graphics.FromImage(image))
				{
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					graphics.DrawImage(sourceImage, 0, 0, image.Width, image.Height);
					float num10 = (Utilities.GetScreenDpi() == 96) ? 12f : 15f;
					this.drawArea.DrawLayers(this, plan, graphics, image.Size, 0, 0, (float)num9, true, false, MainForm.ImageQualityEnum.QualityHigh, num10 / ((float)Utilities.GetScreenDpi() / sourceImage.HorizontalResolution));
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				if (image != null)
				{
					image.Dispose();
					image = null;
					GC.Collect();
				}
			}
			return image;
		}

		private Image LoadPlanImage(Plan plan)
		{
			Image result = null;
			Bitmap bitmap = null;
			Exception exception = null;
			if (this.mainControl.LoadImageFromStream(plan.FullFileName, ref bitmap, ref exception))
			{
				result = this.PrintPlanImage(bitmap, plan);
			}
			else
			{
				Utilities.DisplayFileOpenError(plan.FullFileName, exception);
			}
			if (bitmap != null)
			{
				bitmap.Dispose();
				GC.Collect();
			}
			return result;
		}

		private string ValidatePDFExportFileName(string exportFileName)
		{
			string text = Path.Combine(Utilities.GetPDFExportFolder(), this.project.DisplayName);
			Utilities.ValidateDirectory(text);
			return Path.Combine(text, Utilities.StripInvalidCharacters(Path.GetFileNameWithoutExtension(exportFileName), "") + ".pdf");
		}

		private void ExportCurrentPlanToPDF()
		{
			Image image = this.PrintPlanImage((Bitmap)this.mainControl.Image, this.drawArea.ActivePlan);
			if (image != null)
			{
				string destination = this.ValidatePDFExportFileName(this.drawArea.ActivePlan.Name);
				PdfDocument pdfDocument = new PdfDocument();
				PDFUtilities.SetPDFExportDocumentProperties(pdfDocument, this.project.Name, "", "", "", string.Format("{0} - {1}", Utilities.ApplicationName, Utilities.ApplicationWebsite));
				if (PDFUtilities.AddPageToPDFExportDocument(pdfDocument, image))
				{
					this.SavePDFDoc(pdfDocument, destination, true);
				}
				pdfDocument.Close();
				pdfDocument.Dispose();
				image.Dispose();
				GC.Collect();
			}
		}

		private string AddPlanToPDF(PdfDocument pdfDocument, Plan plan, string exportFileName)
		{
			string result = string.Empty;
			Image image = this.LoadPlanImage(plan);
			if (image != null)
			{
				result = this.ValidatePDFExportFileName(exportFileName);
				if (!PDFUtilities.AddPageToPDFExportDocument(pdfDocument, image))
				{
					result = string.Empty;
				}
				image.Dispose();
				GC.Collect();
			}
			return result;
		}

        private bool SavePDFDoc(PdfDocument pdfDoc, string destination, bool askToOpen)
        {
            bool savedSuccessfully = false;

            while (true)
            {
                try
                {
                    pdfDoc.Save(destination);
                    savedSuccessfully = true;
                    break;
                }
                catch (Exception ex)
                {
                    // Keep original behavior: if message mentions "process", offer Retry/Cancel.
                    if (ex.Message != null && ex.Message.IndexOf("process", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        string fileUsedByAnotherProcessTitle = Resources.Fichier_utilisé_par_un_autre_processus;
                        string closePdfFileMessage = Resources.Veuillez_fermer_votre_fichier_PDF;

                        DialogResult choice = Utilities.DisplayWarningQuestionRetryCancel(
                            fileUsedByAnotherProcessTitle,
                            closePdfFileMessage);

                        if (choice == DialogResult.Cancel)
                            break;

                        // Any non-cancel means retry (matches original != Cancel logic)
                        continue;
                    }

                    Utilities.DisplaySystemError(ex);
                    break;
                }
            }

            if (savedSuccessfully && askToOpen)
            {
                string successTitle = Resources.Fichier_créé_avec_succès;
                string infoMessage = Resources.Nom_du_fichier + "\n" + destination;

                DialogResult openChoice = Utilities.DisplayQuestionCustom(
                    successTitle,
                    infoMessage,
                    Resources.Ouvrir_le_fichier,
                    Resources.Ouvrir_le_répertoire);

                if (openChoice != DialogResult.Cancel)
                {
                    switch (openChoice)
                    {
                        case DialogResult.Yes:
                            Utilities.OpenDocument(destination);
                            break;

                        case DialogResult.No:
                            Utilities.OpenDocument(Path.GetDirectoryName(destination));
                            break;
                    }
                }
            }

            return savedSuccessfully;
        }

        private void PrintPlan(PrintPageEventArgs ev)
		{
			Image image = null;
			try
			{
				int num = 0;
				int num2 = 0;
				string text = string.Empty;
				if (Settings.Default.PrintPlanType == 1 && !this.PlansSelection)
				{
					text = this.drawArea.ActivePlan.Name;
					using (StringFormat stringFormat = new StringFormat())
					{
						stringFormat.Alignment = StringAlignment.Center;
						stringFormat.LineAlignment = StringAlignment.Center;
						SizeF sizeF = ev.Graphics.MeasureString(text, Utilities.GetDefaultFont(), Point.Empty, stringFormat);
						num = sizeF.ToSize().Width;
						num2 = sizeF.ToSize().Height;
					}
				}
				if (!this.PlansSelection)
				{
					Plan plan = this.drawArea.ActivePlan;
					if (Settings.Default.PrintPlanType == 1)
					{
						Rectangle targetBounds = new Rectangle(Point.Empty, this.mainControl.DrawingBoard.ClientSize);
						image = new Bitmap(targetBounds.Width, targetBounds.Height);
						this.mainControl.DrawingBoard.DrawToBitmap((Bitmap)image, targetBounds);
					}
					else
					{
						image = this.PrintPlanImage((Bitmap)this.mainControl.Image, plan);
					}
				}
				else
				{
					int num3 = Utilities.ConvertToInt((double)(this.selectedPlanIndex + 1) * (100.0 / (double)this.selectedPlans.Count));
					this.ribbonBarPlansAction.Text = num3 + " %";
					this.progressPlansAction.Value = num3;
					Application.DoEvents();
					string empty = string.Empty;
					Plan plan = (Plan)this.selectedPlans[this.selectedPlanIndex].Value;
					image = this.LoadPlanImage(plan);
					if (image == null)
					{
						ev.HasMorePages = (this.selectedPlanIndex + 1 < this.selectedPlans.Count);
						this.selectedPlanIndex++;
						return;
					}
				}
				int width = ev.MarginBounds.Width;
				int num4 = ev.MarginBounds.Height - (ev.MarginBounds.Top + num2 + 2);
				int width2;
				int height;
				if (Settings.Default.PrintPlanType == 1 && !this.PlansSelection)
				{
					width2 = this.mainControl.DrawingBoard.ClientSize.Width;
					height = this.mainControl.DrawingBoard.ClientSize.Height;
				}
				else
				{
					width2 = image.Width;
					height = image.Height;
				}
				double num5 = (double)width / (double)width2;
				double num6 = (double)num4 / (double)height;
				double num7 = (num5 < num6) ? num5 : num6;
				int height2 = Convert.ToInt32((double)height * num7);
				int width3 = Convert.ToInt32((double)width2 * num7);
				int x = Convert.ToInt32(((double)width - (double)width2 * num7) / 2.0);
				int num8 = Convert.ToInt32(((double)num4 - (double)height * num7) / 2.0);
				ev.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				ev.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				ev.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				ev.Graphics.Clear(Color.White);
				if (Settings.Default.PrintPlanType == 1 && !this.PlansSelection)
				{
					ev.Graphics.DrawString(text, Utilities.GetDefaultFont(), Brushes.Black, new Point(width / 2 - num / 2, ev.MarginBounds.Top));
				}
				ev.Graphics.DrawImage(image, x, num8 + ev.MarginBounds.Top + num2 + 2, width3, height2);
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			if (!this.PlansSelection)
			{
				ev.HasMorePages = false;
			}
			else
			{
				ev.HasMorePages = (this.selectedPlanIndex + 1 < this.selectedPlans.Count);
				this.selectedPlanIndex++;
			}
			if (image != null)
			{
				image.Dispose();
				GC.Collect();
			}
		}

		private void printDoc_PrintPage(object sender, PrintPageEventArgs ev)
		{
			this.PrintPlan(ev);
		}

		private void PrintPlanSetup()
		{
			try
			{
				PrintDialog printDialog = new PrintDialog();
				PrintDocument printDocument = new PrintDocument();
				printDocument.PrintPage += this.printDoc_PrintPage;
				printDocument.DocumentName = this.project.FileName;
				printDocument.OriginAtMargins = true;
				printDocument.DefaultPageSettings.Landscape = true;
				printDocument.DefaultPageSettings.Margins.Top = 10;
				printDocument.DefaultPageSettings.Margins.Left = 10;
				printDocument.DefaultPageSettings.Margins.Right = 10;
				printDocument.DefaultPageSettings.Margins.Bottom = 10;
				printDialog.Document = printDocument;
				printDialog.AllowSelection = false;
				printDialog.AllowSomePages = false;
				if (printDialog.ShowDialog() == DialogResult.OK)
				{
					printDocument.Print();
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void ZoomIn()
		{
			this.mainControl.ZoomIn();
		}

		private void ZoomOut()
		{
			this.mainControl.ZoomOut();
		}

		private void ZoomSet(int zoom)
		{
			this.mainControl.Zoom = zoom;
		}

		private void FitToScreen()
		{
			this.mainControl.FitToScreen();
		}

		private void ZoomToSelection()
		{
			this.drawArea.SelectZoomSelectionTool();
		}

		private void ZoomToSelectedObject()
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			this.ZoomObject(this.drawArea.SelectedObject, false);
		}

		private void ZoomToSelectedGroup()
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.SelectedObject;
			if (!GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, selectedObject))
			{
				return;
			}
			this.ZoomObject(selectedObject, true);
			this.ObjectSelect(selectedObject, true, false);
		}

		private void LayerAdd()
		{
			int num = this.drawArea.CreateNewLayer(this.drawArea.Layers.FindFreeLayerName(Resources.Nouveau_calque));
			Layer layer = this.drawArea.GetLayer(num);
			if (layer == null)
			{
				return;
			}
			this.AddLayerToGUI(layer, num);
			Utilities.SetObjectFocus(this.lstLayers);
			this.SetModified();
		}

		private void LayerEdit(int layerIndex)
		{
			this.layersNavigator.Edit(layerIndex);
		}

		private void LayerRemove(int layerIndex)
		{
			if (layerIndex > 0)
			{
				string title = Resources.Supprimer_ce_calque;
				string message = Resources.Ce_calque_sera_supprimé_du_plan;
				Layer layer = this.drawArea.GetLayer(layerIndex);
				if (layer != null && layer.DrawingObjects.Count > 0)
				{
					title = Resources.Supprimer_ce_calque_et_ses_objets_associés;
					message = string.Format(Resources.Si_vous_supprimez_ce_calque, layer.DrawingObjects.Count, (layer.DrawingObjects.Count > 1) ? Resources.objets : Resources.objet);
				}
				if (Utilities.DisplayDeleteConfirmation(title, message) == DialogResult.No)
				{
					return;
				}
				if (this.drawArea.RemoveLayer(layerIndex))
				{
					this.ReloadLayersInGUI(0);
					this.ReloadObjectsInGUI();
					Utilities.SetObjectFocus(this.lstLayers);
					this.SetModified();
					return;
				}
			}
			else
			{
				string action_invalide = Resources.Action_invalide;
				string il_est_interdit_de_supprimer_le_calque_par_défaut = Resources.Il_est_interdit_de_supprimer_le_calque_par_défaut;
				Utilities.DisplayError(action_invalide, il_est_interdit_de_supprimer_le_calque_par_défaut);
			}
		}

		private void LayerMoveUp(int layerIndex)
		{
			Layer layer = this.drawArea.GetLayer(layerIndex);
			if (layer != null && layerIndex > 0 && layerIndex < this.drawArea.Layers.Count - 1)
			{
				int num = this.drawArea.LayerMoveUp(layerIndex);
				if (num > -1)
				{
					this.layersNavigator.Refresh(num);
					this.objectsNavigator.Reload(this.drawArea.ActivePlan);
					Utilities.SetObjectFocus(this.lstLayers);
					this.SetModified();
				}
			}
		}

		private void LayerMoveDown(int layerIndex)
		{
			Layer layer = this.drawArea.GetLayer(layerIndex);
			if (layer != null && layerIndex > 1 && this.drawArea.Layers.Count > 2)
			{
				int num = this.drawArea.LayerMoveDown(layerIndex);
				if (num > -1)
				{
					this.layersNavigator.Refresh(num);
					this.objectsNavigator.Reload(this.drawArea.ActivePlan);
					Utilities.SetObjectFocus(this.lstLayers);
					this.SetModified();
				}
			}
		}

		private void LayerSaveList()
		{
			string sauvegarder_comme_liste_de_calques_par_défaut = Resources.Sauvegarder_comme_liste_de_calques_par_défaut;
			string tous_les_nouveaux_plans_utiliseront_cette_liste_de_calques = Resources.Tous_les_nouveaux_plans_utiliseront_cette_liste_de_calques;
			if (Utilities.DisplayWarningQuestionCustom(sauvegarder_comme_liste_de_calques_par_défaut, tous_les_nouveaux_plans_utiliseront_cette_liste_de_calques, Resources.document_save_48x48, Resources.Sauvegarder) == DialogResult.No)
			{
				return;
			}
			this.layersNavigator.SaveAsDefault();
		}

		private void LayerSaveListAs()
		{
			if (this.drawArea.ActivePlan == null)
			{
				return;
			}
			string text = Utilities.SaveFileDialog(Resources.Sauvegarder_la_liste_de_calques, Utilities.GetLayersFolder(), string.Empty, Resources.Fichiers_texte + " (*.txt)|*.txt");
			if (text != string.Empty)
			{
				this.layersNavigator.SaveAs(text);
			}
		}

		private void LayerOpenList()
		{
			if (this.drawArea.ActivePlan == null)
			{
				return;
			}
			if (this.drawArea.ObjectsCount() > 0)
			{
				string action_invalide = Resources.Action_invalide;
				string ce_plan_contient_des_objets = Resources.Ce_plan_contient_des_objets;
				Utilities.DisplayError(action_invalide, ce_plan_contient_des_objets);
				return;
			}
			string text = Utilities.OpenFileDialog(Resources.Ouvrir_une_liste_de_calques, Utilities.GetLayersFolder(), Resources.Fichiers_texte + " (*.txt)|*.txt");
			if (text != string.Empty)
			{
				this.layersNavigator.Load(text);
				this.drawArea.ClearHistory();
				this.drawArea.ActivePlan.DefaultBookmark.LayerIndex = 0;
				this.drawArea.ValidateLegend(this.extensionsSupport);
				this.FillPlanLayers(this.drawArea.ActivePlan, 0);
				this.FillPlanObjects(this.drawArea.ActivePlan);
			}
		}

		private void LayersMakeVisible()
		{
			if (this.drawArea.ActivePlan == null)
			{
				return;
			}
			this.layersNavigator.ToggleAllVisibility(true);
		}

		private void LayersMakeInvisible()
		{
			if (this.drawArea.ActivePlan == null)
			{
				return;
			}
			this.layersNavigator.ToggleAllVisibility(false);
		}

		private void GroupBrowsePrevious(bool byObjectType)
		{
			if (!this.CanBrowseObject(byObjectType))
			{
				return;
			}
			this.drawArea.SelectPreviousObject(byObjectType);
			this.UpdateObject(this.drawArea.SelectedObject);
			this.EnsureObjectVisible(this.drawArea.SelectedObject, true);
		}

		private void GroupBrowseNext(bool byObjectType)
		{
			if (!this.CanBrowseObject(byObjectType))
			{
				return;
			}
			this.drawArea.SelectNextObject(byObjectType);
			this.UpdateObject(this.drawArea.SelectedObject);
			this.EnsureObjectVisible(this.drawArea.SelectedObject, true);
		}

		private bool MoveSelected(int deltaX, int deltaY)
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return false;
			}
			this.drawArea.ActiveDrawingObjects.MoveSelected(deltaX, deltaY);
			this.Refresh();
			return true;
		}

		private Point PointInsert(Point contextPoint, bool refresh = true)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return Point.Empty;
			}
			DrawPolyLine selectedPolyLine = this.drawArea.SelectedPolyLine;
			if (selectedPolyLine == null)
			{
				return Point.Empty;
			}
			if (this.drawArea.SegmentHitTest(selectedPolyLine, contextPoint) != -2)
			{
				return Point.Empty;
			}
			Point point = selectedPolyLine.LocatePointOnSegment(this.drawArea.NativePoint(contextPoint));
			int nearestPointIndex = selectedPolyLine.GetNearestPointIndex(point);
			if (nearestPointIndex == -1)
			{
				return Point.Empty;
			}
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			}
			selectedPolyLine.InsertPoint(point, nearestPointIndex + 1);
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.DeductionSaveCommand();
			}
			else
			{
				this.drawArea.DeductionWasChanged = true;
			}
			if (refresh)
			{
				this.UpdateObject(selectedPolyLine);
				this.SetModified();
				this.Refresh();
			}
			return point;
		}

		private void PointRemove(Point contextPoint)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPolyLine = this.drawArea.SelectedPolyLine;
			if (selectedPolyLine == null)
			{
				return;
			}
			if ((selectedPolyLine.ObjectType == "Area" && selectedPolyLine.PointArray.Count <= 3) || (selectedPolyLine.ObjectType == "Perimeter" && selectedPolyLine.PointArray.Count <= 2))
			{
				return;
			}
			int num = this.drawArea.SegmentHitTest(selectedPolyLine, contextPoint);
			if (num < 1)
			{
				return;
			}
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			}
			Point handle = selectedPolyLine.GetHandle(num);
			selectedPolyLine.RemovePoint(handle);
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.DeductionSaveCommand();
			}
			else
			{
				this.drawArea.DeductionWasChanged = true;
			}
			this.UpdateObject(selectedPolyLine);
			this.SetModified();
			this.Refresh();
		}

		private void DropSetHeight(DrawPolyLine drawPolyLine, Point dropPoint)
		{
			if (drawPolyLine == null)
			{
				return;
			}
			double num = drawPolyLine.GetDropLength(dropPoint);
			if (this.drawArea.UnitScale.ScaleSystemType == UnitScale.UnitSystem.metric)
			{
				num = ((this.drawArea.UnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToInches(num) : UnitScale.FromMetersToMillimeters(num));
			}
			else
			{
				num = ((this.drawArea.UnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromFeetToInches(num) : UnitScale.FromFeetToMillimeters(num));
			}
			GenericValue genericValue = new GenericValue(num);
			try
			{
				using (DimensionForm dimensionForm = new DimensionForm(Resources.Longueur_de_la_montée_descente, this.drawArea.UnitScale, genericValue))
				{
					dimensionForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return;
			}
			if (genericValue.Dirty)
			{
				double dropLength = drawPolyLine.GetDropLength(dropPoint);
				double num2 = Utilities.ConvertToDouble(genericValue.Value, -1);
				num2 = ((this.drawArea.UnitScale.ScaleSystemType == UnitScale.UnitSystem.metric) ? UnitScale.FromMillimetersToMeters(num2) : UnitScale.FromInchesToFeet(num2));
				Console.WriteLine("oldHeight = " + dropLength);
				Console.WriteLine("newHeight = " + num2);
				if (num2 != dropLength)
				{
					drawPolyLine.SetDropLength(dropPoint, num2);
				}
			}
			this.UpdateObject(drawPolyLine);
			this.SetModified();
			this.Refresh();
		}

		private DrawLine DropInsert(Point contextPoint, bool refresh = true)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return null;
			}
			DrawPolyLine selectedPolyLine = this.drawArea.SelectedPolyLine;
			if (selectedPolyLine == null)
			{
				return null;
			}
			if (this.drawArea.SegmentHitTest(selectedPolyLine, contextPoint) != -2)
			{
				return null;
			}
			Point point = selectedPolyLine.LocatePointOnSegment(this.drawArea.NativePoint(contextPoint));
			int nearestPointIndex = selectedPolyLine.GetNearestPointIndex(point);
			if (nearestPointIndex == -1)
			{
				return null;
			}
			GenericValue genericValue = new GenericValue(0);
			try
			{
				using (DimensionForm dimensionForm = new DimensionForm(Resources.Longueur_de_la_montée_descente, this.drawArea.UnitScale, genericValue))
				{
					dimensionForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return null;
			}
			double num = 0.0;
			if (genericValue.Dirty)
			{
				num = Utilities.ConvertToDouble(genericValue.Value, -1);
				num = ((this.drawArea.UnitScale.ScaleSystemType == UnitScale.UnitSystem.metric) ? UnitScale.FromMillimetersToMeters(num) : UnitScale.FromInchesToFeet(num));
			}
			if (num == 0.0)
			{
				return null;
			}
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			}
			DrawLine result = selectedPolyLine.CreateDrop(point, num);
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.DeductionSaveCommand();
			}
			else
			{
				this.drawArea.DeductionWasChanged = true;
			}
			if (refresh)
			{
				this.UpdateObject(selectedPolyLine);
				this.SetModified();
				this.Refresh();
			}
			return result;
		}

		private void DropRemove(Point contextPoint)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPolyLine = this.drawArea.SelectedPolyLine;
			if (selectedPolyLine == null)
			{
				return;
			}
			Point point = this.drawArea.IsPartOfDrop(selectedPolyLine, contextPoint);
			if (point == Point.Empty)
			{
				return;
			}
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			}
			selectedPolyLine.DeleteDrop(point);
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.DeductionSaveCommand();
			}
			else
			{
				this.drawArea.DeductionWasChanged = true;
			}
			this.UpdateObject(selectedPolyLine);
			this.SetModified();
			this.Refresh();
		}

		private void DeductionCreate()
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawObject selectedArea = this.drawArea.SelectedArea;
			if (selectedArea == null)
			{
				return;
			}
			this.drawArea.DeductionCreate(selectedArea);
		}

		private void DeductionsEdit()
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawObject selectedArea = this.drawArea.SelectedArea;
			if (selectedArea == null)
			{
				return;
			}
			this.drawArea.DeductionsEdit(selectedArea);
		}

		private void DeductionDuplicate()
		{
			if (this.drawArea.SelectionCount != 1 && !this.DeductionsEditing)
			{
				return;
			}
			DrawPolyLine deductionParent = this.drawArea.DeductionParent;
			if (deductionParent == null)
			{
				return;
			}
			DrawPolyLine selectedArea = this.drawArea.SelectedArea;
			if (selectedArea == null)
			{
				return;
			}
			DrawPolyLine drawPolyLine = (DrawPolyLine)selectedArea.Clone();
			drawPolyLine.Move(10, 10);
			this.drawArea.InsertObject(drawPolyLine, this.drawArea.ActiveLayerIndex, true, true);
			deductionParent.CreateDeduction(drawPolyLine);
			this.drawArea.DeductionWasChanged = true;
			selectedArea.Selected = false;
			drawPolyLine.Selected = true;
			this.UpdateObject(deductionParent);
			this.SetModified();
			this.Refresh();
		}

		private void OpeningInsert(Point contextPoint)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPerimeter = this.drawArea.SelectedPerimeter;
			if (selectedPerimeter == null)
			{
				return;
			}
			if (this.drawArea.SegmentHitTest(selectedPerimeter, contextPoint) != -2)
			{
				return;
			}
			Point point = selectedPerimeter.LocatePointOnSegment(this.drawArea.NativePoint(contextPoint));
			this.drawArea.ToolSettings.StartPoint = new Point(point.X - (int)this.drawArea.DrawingBoard.Origin.X, point.Y - (int)this.drawArea.DrawingBoard.Origin.Y);
			this.drawArea.OpeningCreate(selectedPerimeter);
		}

		private void OpeningDuplicate(Point contextPoint)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPerimeter = this.drawArea.SelectedPerimeter;
			if (selectedPerimeter == null)
			{
				return;
			}
			if (this.drawArea.SegmentHitTest(selectedPerimeter, contextPoint) != -2)
			{
				return;
			}
			if (this.drawArea.Clipboard.SelectedOpening == null)
			{
				return;
			}
			for (int i = 0; i < selectedPerimeter.PointArray.Count - (selectedPerimeter.CloseFigure ? 0 : 1); i++)
			{
				Point[] array = new Point[2];
				if (selectedPerimeter.CloseFigure && i + 1 == selectedPerimeter.PointArray.Count)
				{
					array[0] = (Point)selectedPerimeter.PointArray[selectedPerimeter.PointArray.Count - 1];
					array[1] = (Point)selectedPerimeter.PointArray[0];
				}
				else
				{
					array[0] = (Point)selectedPerimeter.PointArray[i];
					array[1] = (Point)selectedPerimeter.PointArray[i + 1];
				}
				if (this.drawArea.ToolSettings.SelectedSegment.IsEqualTo(array[0], array[1]))
				{
					this.drawArea.ToolSettings.SelectedSegment.StartPoint = new Point(array[0].X, array[0].Y);
					this.drawArea.ToolSettings.SelectedSegment.EndPoint = new Point(array[1].X, array[1].Y);
					break;
				}
			}
			int num = Math.Abs(this.drawArea.ToolSettings.SelectedSegment.StartPoint.X - this.drawArea.ToolSettings.SelectedSegment.EndPoint.X);
			int num2 = Math.Abs(this.drawArea.ToolSettings.SelectedSegment.StartPoint.Y - this.drawArea.ToolSettings.SelectedSegment.EndPoint.Y);
			DrawPolyLine.WindingDirection windingDirection = (num > num2) ? ((this.drawArea.ToolSettings.SelectedSegment.StartPoint.X > this.drawArea.ToolSettings.SelectedSegment.EndPoint.X) ? DrawPolyLine.WindingDirection.CounterClockWise : DrawPolyLine.WindingDirection.Clockwise) : ((this.drawArea.ToolSettings.SelectedSegment.StartPoint.Y > this.drawArea.ToolSettings.SelectedSegment.EndPoint.Y) ? DrawPolyLine.WindingDirection.CounterClockWise : DrawPolyLine.WindingDirection.Clockwise);
			Console.WriteLine("direction = " + windingDirection);
			Point startPoint = selectedPerimeter.LocatePointOnSegment(this.drawArea.NativePoint(contextPoint));
			DrawLine.Angle((double)this.drawArea.ToolSettings.SelectedSegment.StartPoint.X, (double)this.drawArea.ToolSettings.SelectedSegment.StartPoint.Y, (double)this.drawArea.ToolSettings.SelectedSegment.EndPoint.X, (double)this.drawArea.ToolSettings.SelectedSegment.EndPoint.Y);
			double num3 = (double)this.drawArea.Clipboard.SelectedOpening.QueryLengthInPixels(this.drawArea.UnitScale);
			double num4 = (double)DrawLine.GetDistance(startPoint, (windingDirection == DrawPolyLine.WindingDirection.CounterClockWise) ? this.drawArea.ToolSettings.SelectedSegment.StartPoint : this.drawArea.ToolSettings.SelectedSegment.EndPoint);
			num3 = Math.Min(num3, num4 - 10.0);
			Segment segment = new Segment
			{
				StartPoint = new Point(startPoint.X, startPoint.Y)
			};
			foreach (Point endPoint in DrawLine.GetPointsOnLine(startPoint.X, startPoint.Y, (windingDirection == DrawPolyLine.WindingDirection.CounterClockWise) ? this.drawArea.ToolSettings.SelectedSegment.StartPoint.X : this.drawArea.ToolSettings.SelectedSegment.EndPoint.X, (windingDirection == DrawPolyLine.WindingDirection.CounterClockWise) ? this.drawArea.ToolSettings.SelectedSegment.StartPoint.Y : this.drawArea.ToolSettings.SelectedSegment.EndPoint.Y))
			{
				if ((double)DrawLine.GetDistance(startPoint, endPoint) >= num3)
				{
					segment.EndPoint = new Point(endPoint.X, endPoint.Y);
					break;
				}
			}
			DrawLine drawLine = new DrawLine(segment.StartPoint.X, segment.StartPoint.Y, segment.EndPoint.X, segment.EndPoint.Y, new PointF(0f, 0f), selectedPerimeter.Name, selectedPerimeter.GroupID, selectedPerimeter.Comment, true, Color.Black, selectedPerimeter.Opacity, selectedPerimeter.PenWidth);
			drawLine.Height = this.drawArea.Clipboard.SelectedOpening.QueryHeight(this.drawArea.UnitScale);
			drawLine.Visible = false;
			this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			selectedPerimeter.CreateOpening(drawLine, this.drawArea.ToolSettings.SelectedSegment, false, false);
			this.drawArea.DeductionSaveCommand();
			this.UpdateObject(selectedPerimeter);
			this.SetModified();
			this.Refresh();
		}

		private void OpeningCreateFromSegment()
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPerimeter = this.drawArea.SelectedPerimeter;
			if (selectedPerimeter == null)
			{
				return;
			}
			DrawLine drawLine = new DrawLine(this.drawArea.ToolSettings.SelectedSegment.StartPoint.X, this.drawArea.ToolSettings.SelectedSegment.StartPoint.Y, this.drawArea.ToolSettings.SelectedSegment.EndPoint.X, this.drawArea.ToolSettings.SelectedSegment.EndPoint.Y, new PointF(0f, 0f), selectedPerimeter.Name, selectedPerimeter.GroupID, selectedPerimeter.Comment, false, Color.Black, selectedPerimeter.Opacity, selectedPerimeter.PenWidth);
			drawLine.Visible = false;
			this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			selectedPerimeter.CreateOpening(drawLine, this.drawArea.ToolSettings.SelectedSegment, true, true);
			this.drawArea.DeductionSaveCommand();
			this.UpdateObject(selectedPerimeter);
			this.SetModified();
			this.Refresh();
		}

		private void OpeningDelete()
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPerimeter = this.drawArea.SelectedPerimeter;
			if (selectedPerimeter == null)
			{
				return;
			}
			this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			selectedPerimeter.DeleteOpening(this.drawArea.ToolSettings.SelectedSegment.StartPoint, this.drawArea.ToolSettings.SelectedSegment.EndPoint);
			this.drawArea.DeductionSaveCommand();
			this.UpdateObject(selectedPerimeter);
			this.SetModified();
			this.Refresh();
		}

		public void OpeningSetHeight(DrawPolyLine drawPolyLine, Segment segment)
		{
			if (drawPolyLine == null)
			{
				return;
			}
			double num = drawPolyLine.GetOpeningHeight(segment.StartPoint, segment.EndPoint);
			if (this.drawArea.UnitScale.ScaleSystemType == UnitScale.UnitSystem.metric)
			{
				num = ((this.drawArea.UnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToInches(num) : UnitScale.FromMetersToMillimeters(num));
			}
			else
			{
				num = ((this.drawArea.UnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromFeetToInches(num) : UnitScale.FromFeetToMillimeters(num));
			}
			GenericValue genericValue = new GenericValue(num);
			try
			{
				using (DimensionForm dimensionForm = new DimensionForm(Resources.Hauteur_de_l_ouverture_, this.drawArea.UnitScale, genericValue))
				{
					dimensionForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return;
			}
			if (genericValue.Dirty)
			{
				double openingHeight = drawPolyLine.GetOpeningHeight(segment.StartPoint, segment.EndPoint);
				double num2 = Utilities.ConvertToDouble(genericValue.Value, -1);
				num2 = ((this.drawArea.UnitScale.ScaleSystemType == UnitScale.UnitSystem.metric) ? UnitScale.FromMillimetersToMeters(num2) : UnitScale.FromInchesToFeet(num2));
				Console.WriteLine("oldHeight = " + openingHeight);
				Console.WriteLine("newHeight = " + num2);
				if (num2 != openingHeight)
				{
					drawPolyLine.SetOpeningHeight(segment.StartPoint, segment.EndPoint, num2);
				}
			}
			this.UpdateObject(drawPolyLine);
			this.SetModified();
			this.Refresh();
		}

		private void SetHeight(Point contextPoint)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPerimeter = this.drawArea.SelectedPerimeter;
			if (selectedPerimeter == null)
			{
				return;
			}
			Point point = this.drawArea.IsPartOfDrop(selectedPerimeter, contextPoint);
			if (point != Point.Empty)
			{
				this.DropSetHeight(selectedPerimeter, point);
				return;
			}
			int num = this.drawArea.SegmentHitTest(selectedPerimeter, contextPoint);
			if (num == -3)
			{
				this.OpeningSetHeight(selectedPerimeter, this.drawArea.ToolSettings.SelectedSegment);
			}
		}

		private void PerimeterCreateFromArea()
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < this.drawArea.ActiveDrawingObjects.SelectionCount; i++)
			{
				DrawObject selectedObject = this.drawArea.ActiveDrawingObjects.GetSelectedObject(i);
				if (selectedObject.ObjectType == "Area")
				{
					if (!arrayList.Contains(selectedObject.GroupID))
					{
						arrayList.Add(selectedObject.GroupID);
					}
					arrayList2.Add((DrawPolyLine)selectedObject);
				}
			}
			this.drawArea.ActiveDrawingObjects.UnselectAll();
			foreach (object obj in arrayList)
			{
				int num = (int)obj;
				DrawObject drawObject = this.drawArea.FindObjectFromGroupID(num);
				if (drawObject != null)
				{
					string freeObjectName = this.drawArea.GetFreeObjectName(Resources.Périmètre);
					int nextGroupID = this.drawArea.GetNextGroupID();
					Color nextColor = this.drawArea.GetNextColor(drawObject.ObjectType);
					bool flag = true;
					foreach (object obj2 in arrayList2)
					{
						DrawPolyLine drawPolyLine = (DrawPolyLine)obj2;
						if (drawPolyLine.GroupID == num)
						{
							DrawObject drawObject2 = drawPolyLine.Clone();
							drawObject2.Filled = false;
							drawObject2.Name = freeObjectName;
							drawObject2.GroupID = nextGroupID;
							drawObject2.Color = nextColor;
							drawObject2.FillColor = nextColor;
							drawObject2.PenWidth = 6;
							drawObject2.Selected = true;
							((DrawPolyLine)drawObject2).DeductionArray.Clear();
							((DrawPolyLine)drawObject2).DeductionParentID = -1;
							drawObject2.SlopeFactor.SetSlope(0);
							this.drawArea.InsertObject(drawObject2, this.drawArea.ActiveLayerIndex, true, false);
							this.drawArea.AddCommandToHistory(new CommandAdd(this.drawArea, this.drawArea.ActiveLayerName, drawObject2));
							this.objectsNavigator.Refresh(drawObject2);
							if (flag)
							{
								this.AddObjectToGUI(this.drawArea.ActiveLayerIndex, drawObject2, true);
								flag = false;
							}
						}
					}
				}
			}
			arrayList2.Clear();
			arrayList.Clear();
			this.UpdateSelectedObjects();
			this.SetModified();
			this.Refresh();
		}

		private void PerimeterUpdateFigure(bool closeFigure)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPerimeter = this.drawArea.SelectedPerimeter;
			if (selectedPerimeter == null)
			{
				return;
			}
			this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			if (!closeFigure && selectedPerimeter.SelectedSegment.IsValid())
			{
				int num = -1;
				for (int i = 0; i < selectedPerimeter.PointArray.Count; i++)
				{
					Point[] array = new Point[2];
					if (i == selectedPerimeter.PointArray.Count - 1)
					{
						array[0] = (Point)selectedPerimeter.PointArray[selectedPerimeter.PointArray.Count - 1];
						array[1] = (Point)selectedPerimeter.PointArray[0];
					}
					else
					{
						array[0] = (Point)selectedPerimeter.PointArray[i];
						array[1] = (Point)selectedPerimeter.PointArray[i + 1];
					}
					if (selectedPerimeter.SelectedSegment.IsEqualTo(array[0], array[1]))
					{
						num = ((i == selectedPerimeter.PointArray.Count - 1) ? 0 : (i + 1));
						break;
					}
				}
				if (num > -1)
				{
					ArrayList arrayList = new ArrayList();
					do
					{
						Point point = (Point)selectedPerimeter.PointArray[num];
						arrayList.Add(new Point(point.X, point.Y));
						num++;
						if (num == selectedPerimeter.PointArray.Count)
						{
							num = 0;
						}
					}
					while (arrayList.Count < selectedPerimeter.PointArray.Count);
					selectedPerimeter.PointArray.Clear();
					foreach (object obj in arrayList)
					{
						Point point2 = (Point)obj;
						selectedPerimeter.AddPoint(new Point(point2.X, point2.Y));
					}
					arrayList.Clear();
				}
			}
			selectedPerimeter.CloseFigure = closeFigure;
			selectedPerimeter.SelectedSegment.Reset();
			selectedPerimeter.ResetDrops();
			this.drawArea.DeductionSaveCommand();
			this.UpdateObject(selectedPerimeter);
			this.SetModified();
			this.Refresh();
		}

		private void SelectGroupLayer(int groupID)
		{
			int num = 0;
			foreach (object obj in this.drawArea.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj2;
					if (!drawObject.IsDeduction() && drawObject.GroupID == groupID)
					{
						this.layersNavigator.ForceSelection(num);
						return;
					}
				}
				num++;
			}
		}

		private void GroupAddObject(DrawObject groupObject)
		{
			if (groupObject == null)
			{
				return;
			}
			if (groupObject.IsPartOfGroup())
			{
				this.SelectGroupLayer(groupObject.GroupID);
			}
			string objectType;
			if ((objectType = groupObject.ObjectType) != null)
			{
				if (objectType == "Line")
				{
					this.drawArea.SelectDistanceTool((DrawLine)groupObject, groupObject.Name, groupObject.GroupID, groupObject.Comment, groupObject.Color, groupObject.Opacity, groupObject.PenWidth, ((DrawLine)groupObject).SlopeFactor, groupObject.ShowMeasure, true);
					return;
				}
				if (objectType == "Area")
				{
					this.drawArea.SelectAreaTool((DrawPolyLine)groupObject, groupObject.Name, groupObject.GroupID, groupObject.Comment, groupObject.Color, ((DrawPolyLine)groupObject).Pattern, groupObject.Opacity, ((DrawLine)groupObject).SlopeFactor, groupObject.ShowMeasure, true);
					return;
				}
				if (objectType == "Perimeter")
				{
					this.drawArea.SelectPerimeterTool((DrawPolyLine)groupObject, groupObject.Name, groupObject.GroupID, groupObject.Comment, groupObject.Color, groupObject.Opacity, groupObject.PenWidth, ((DrawLine)groupObject).SlopeFactor, groupObject.ShowMeasure, true);
					return;
				}
				if (!(objectType == "Counter"))
				{
					return;
				}
				this.drawArea.SelectCounterTool((DrawCounter)groupObject, groupObject.Name, groupObject.GroupID, groupObject.Text, ((DrawCounter)groupObject).DefaultSize, ((DrawCounter)groupObject).Shape, groupObject.Comment, groupObject.Color, groupObject.Opacity, true);
			}
		}

		private void GroupAddObject()
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.SelectedObject;
			if (!GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, selectedObject))
			{
				return;
			}
			this.GroupAddObject(selectedObject);
		}

        private void GroupMoveTo(DrawObject targetGroupObject)
        {
            if (targetGroupObject == null)
                return;

            int selectionCount = this.drawArea.SelectionCount;
            if (selectionCount == 0)
                return;

            if (selectionCount == 1 && this.drawArea.SelectedLegend != null)
                return;

            this.drawArea.UnselectLegend();

            DrawObject selectedObject = this.drawArea.SelectedObject;
            int sourceGroupLayerIndex = this.drawArea.FindGroupLayer(selectedObject.GroupID);
            int targetGroupLayerIndex = this.drawArea.FindGroupLayer(targetGroupObject.GroupID);

            CommandChangeStateEx changeStateCommand =
                new CommandChangeStateEx(this.drawArea, this.drawArea.ActiveLayerName, true, true);

            for (int selectionIndex = 0; selectionIndex < selectionCount; selectionIndex++)
            {
                DrawObject selectedItem = this.drawArea.GetSelectedObject(selectionIndex);

                if (selectedItem.ObjectType != targetGroupObject.ObjectType)
                    continue;

                selectedItem.Name = targetGroupObject.Name;
                selectedItem.GroupID = targetGroupObject.GroupID;
                selectedItem.Text = targetGroupObject.Text;
                selectedItem.Comment = targetGroupObject.Comment;
                selectedItem.Color = targetGroupObject.Color;
                selectedItem.FillColor = targetGroupObject.FillColor;
                selectedItem.PenWidth = targetGroupObject.PenWidth;
                selectedItem.ShowMeasure = targetGroupObject.ShowMeasure;
                selectedItem.Visible = targetGroupObject.Visible;
                selectedItem.SetSlopeFactor(targetGroupObject.SlopeFactor);
                selectedItem.Group = targetGroupObject.Group;

                if (selectedItem.ObjectType == "Area")
                {
                    ((DrawPolyLine)selectedItem).Pattern = ((DrawPolyLine)targetGroupObject).Pattern;
                }

                if (selectedItem.ObjectType == "Area" || selectedItem.ObjectType == "Perimeter")
                {
                    DrawPolyLine selectedPolyLine = (DrawPolyLine)selectedItem;

                    foreach (object deductionItem in selectedPolyLine.DeductionArray)
                    {
                        DrawObject deductionObject = (DrawObject)deductionItem;
                        deductionObject.GroupID = selectedItem.GroupID;
                    }

                    continue; // matches the original goto-to-end-of-loop behavior
                }

                if (selectedItem.ObjectType == "Counter")
                {
                    DrawCounter selectedCounter = (DrawCounter)selectedItem;
                    DrawCounter targetCounter = (DrawCounter)targetGroupObject;

                    selectedCounter.UpdateDefaultSize(targetCounter.DefaultSize);
                    selectedCounter.Shape = targetCounter.Shape;
                }
            }

            if (sourceGroupLayerIndex != targetGroupLayerIndex)
            {
                this.LayerMoveTo(targetGroupLayerIndex, changeStateCommand);
                return;
            }

            changeStateCommand.NewState(this.drawArea.ActiveLayerName);
            this.drawArea.AddCommandToHistory(changeStateCommand);
            this.ReloadObjectsInGUI();
            this.UpdateSelectedObjects();
            this.SetModified();
            this.Refresh();
        }

        private void GroupMoveToNew()
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.SelectedObject;
			if (!GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, selectedObject))
			{
				return;
			}
			int num = GroupUtilities.GroupCount(this.drawArea.ActiveLayer, selectedObject, true, "");
			if (num < 2 || this.drawArea.SelectionCount == num)
			{
				return;
			}
			CommandChangeStateEx commandChangeStateEx = new CommandChangeStateEx(this.drawArea, this.drawArea.ActiveLayerName, true, true);
			int groupID = selectedObject.GroupID;
			this.drawArea.MapSelectedObjectsToGroup(this.drawArea.ActiveDrawingObjects, selectedObject, this.drawArea.GetNextGroupID(), true);
			if (this.drawArea.ToolSettings.GroupID == groupID)
			{
				this.drawArea.ToolSettings.Name = selectedObject.Name;
				this.drawArea.ToolSettings.GroupID = selectedObject.GroupID;
				this.drawArea.ToolSettings.LineColor = selectedObject.Color;
				this.drawArea.ToolSettings.FillColor = selectedObject.Color;
				if (selectedObject.ObjectType == "Counter")
				{
					this.drawArea.ToolSettings.Text = selectedObject.Text;
				}
			}
			commandChangeStateEx.NewState(this.drawArea.ActiveLayerName);
			this.drawArea.AddCommandToHistory(commandChangeStateEx);
			this.ReloadObjectsInGUI();
			this.UpdateObject(selectedObject);
			this.SetModified();
			this.Refresh();
		}

		private void ObjectSelect(DrawObject drawObject, bool selectGroup, bool selectObjectThroughLayers = true)
		{
			if (drawObject == null)
			{
				return;
			}
			this.drawArea.UnselectAll();
			if (selectObjectThroughLayers)
			{
				this.SelectObjectThroughLayersFromID(drawObject.ID);
			}
			if (drawObject.IsPartOfGroup() && selectGroup)
			{
				this.drawArea.ActiveDrawingObjects.SelectGroup(drawObject.GroupID);
			}
			this.UpdateObject(drawObject);
			this.Refresh();
		}

		private void GroupSelect()
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.SelectedObject;
			if (selectedObject == null)
			{
				return;
			}
			if (!GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, selectedObject))
			{
				return;
			}
			this.ObjectSelect(selectedObject, true, false);
		}

		private DialogResult LayerMoveToSplitWarning()
		{
			string une_décision_est_requise = Resources.Une_décision_est_requise;
			string cette_action_aura_pour_conséquence_de_scinder_un_ou_plusieurs_groupes = Resources.Cette_action_aura_pour_conséquence_de_scinder_un_ou_plusieurs_groupes;
			return Utilities.DisplayWarningQuestion(une_décision_est_requise, cette_action_aura_pour_conséquence_de_scinder_un_ou_plusieurs_groupes);
		}

		private bool LayerMoveToSplitCheck()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.drawArea.SelectionCount; i++)
			{
				DrawObject selectedObject = this.drawArea.GetSelectedObject(i);
				if (selectedObject.IsPartOfGroup() && !selectedObject.IsDeduction() && !arrayList.Contains(selectedObject.GroupID))
				{
					arrayList.Add(selectedObject.GroupID);
				}
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				DrawObject drawObject = this.drawArea.FindObjectFromGroupID(this.drawArea.ActiveLayer, (int)arrayList[j]);
				int num = GroupUtilities.GroupCount(this.drawArea.ActiveLayer, drawObject, true, "");
				int num2 = GroupUtilities.GroupSelectedCount(this.drawArea, drawObject.GroupID);
				if (num != num2)
				{
					return true;
				}
			}
			arrayList.Clear();
			return false;
		}

		private void MoveAllGroupToLayer(DrawObject groupObject, int oldLayerIndex, int newLayerIndex)
		{
			this.drawArea.UnselectAll();
			this.SelectLayerInGUI(oldLayerIndex);
			if (groupObject.IsPartOfGroup())
			{
				this.drawArea.SelectThisGroup(groupObject.GroupID);
			}
			else
			{
				groupObject.Selected = true;
			}
			this.LayerMoveTo(newLayerIndex, new CommandChangeStateEx(this.drawArea, this.drawArea.ActiveLayerName, true, true));
		}

		private void LayerMoveTo(int layerIndex, CommandChangeStateEx commandChangeStateEx)
		{
			if (this.drawArea.SelectionCount == 0 || this.DeductionsEditing)
			{
				return;
			}
			if (this.LayerMoveToSplitCheck() && this.LayerMoveToSplitWarning() == DialogResult.No)
			{
				return;
			}
			this.Clipboard.Cut(this.drawArea);
			this.drawArea.Delete(false);
			this.SelectLayerInGUI(layerIndex);
			this.Clipboard.Paste(this.drawArea, true);
			commandChangeStateEx.NewState(this.drawArea.ActiveLayerName);
			this.drawArea.AddCommandToHistory(commandChangeStateEx);
			this.ReloadObjectsInGUI();
			this.SelectPointerTool();
			this.UpdateSelectedObjects();
			this.SetModified();
			this.Refresh();
		}

		private void AutoAdjust(DrawPolyLine.AutoAdjustType autoAdjustType)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawPolyLine selectedPolyLine = this.drawArea.SelectedPolyLine;
			if (selectedPolyLine == null)
			{
				return;
			}
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.ActionCommand = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			}
			selectedPolyLine.AutoAdjust((Bitmap)this.drawArea.DrawingBoard.Image, autoAdjustType);
			if (this.drawArea.DeductionParent == null)
			{
				this.drawArea.DeductionSaveCommand();
			}
			else
			{
				this.drawArea.DeductionWasChanged = true;
			}
			this.UpdateObject(selectedPolyLine);
			this.SetModified();
			this.Refresh();
		}

		private void AngleSetType(DrawAngle.AngleTypeEnum angleType)
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.SelectedObject;
			if (selectedObject == null)
			{
				return;
			}
			if (selectedObject.ObjectType != "Angle")
			{
				return;
			}
			DrawAngle drawAngle = (DrawAngle)selectedObject;
			drawAngle.AngleType = angleType;
			Settings.Default.AngleType = (int)angleType;
			this.UpdateObject(drawAngle);
			this.SetModified();
			this.Refresh();
		}

		private void AutoAdjustNote()
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.SelectedObject;
			if (selectedObject == null)
			{
				return;
			}
			if (selectedObject.ObjectType != "Note")
			{
				return;
			}
			DrawNote drawNote = (DrawNote)selectedObject;
			CommandChangeState commandChangeState = new CommandChangeState(this.drawArea, this.drawArea.ActiveLayerName);
			drawNote.RecalcLayout();
			commandChangeState.NewState();
			this.drawArea.AddCommandToHistory(commandChangeState);
			this.SetModified();
			this.Refresh();
		}

		private void NoteEdit()
		{
			if (this.drawArea.SelectionCount != 1)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.SelectedObject;
			if (selectedObject == null)
			{
				return;
			}
			if (selectedObject.ObjectType != "Note")
			{
				return;
			}
			if (!this.NoteEditForm((DrawNote)selectedObject, false))
			{
				return;
			}
			this.AutoAdjustNote();
		}

		private void ToggleMeasures()
		{
			if (this.drawArea.ActivePlan == null)
			{
				return;
			}
			if (this.drawArea.ObjectsCount() == 0)
			{
				return;
			}
			this.drawArea.ToggleMeasures();
			if (this.drawArea.SelectedObject != null)
			{
				this.UpdateObject(this.drawArea.SelectedObject);
			}
			this.SetModified();
			this.Refresh();
		}

		private bool ReloadImage(string fileName, bool reloadZoomedImages)
		{
			Exception exception = null;
			if (!this.mainControl.ReloadImage(fileName, reloadZoomedImages, ref exception))
			{
				Utilities.DisplaySystemError(exception);
				return false;
			}
			return true;
		}

		private void ReloadPlan(bool forceOriginalReload)
		{
			string text = this.drawArea.ActivePlan.FullFileName;
			if (!forceOriginalReload && (this.drawArea.ActivePlan.Brightness != 0 || this.drawArea.ActivePlan.Contrast != 0))
			{
				bool flag = Utilities.FileExists(text + "~");
				text = (flag ? (text + "~") : text);
				if (flag)
				{
					Console.WriteLine("Reloading from cache...");
				}
			}
			this.ReloadImage(text, false);
			this.mainControl.Refresh();
		}

		private void RestoreBookmark()
		{
			if (this.imageEditingBookmark == null)
			{
				return;
			}
			this.ZoomSet(this.imageEditingBookmark.Zoom);
			this.mainControl.Origin = new PointF((float)this.imageEditingBookmark.Origin.X, (float)this.imageEditingBookmark.Origin.Y);
			this.mainControl.Refresh();
			this.imageEditingBookmark = null;
		}

		private void ShowBrightnessContrastControls()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.SelectPointerTool();
			this.imageEditingBookmark = new Bookmark("", this.drawArea.ActiveLayerIndex, this.mainControl.Zoom, new Point((int)this.mainControl.Origin.X, (int)this.mainControl.Origin.Y));
			this.drawArea.ActivePlan.Layers.HideAll();
			if (this.drawArea.ActivePlan.Brightness != 0 || this.drawArea.ActivePlan.Contrast != 0)
			{
				this.ReloadImage(this.drawArea.ActivePlan.FullFileName, false);
			}
			this.exitNow = true;
			this.sliderBrightness.Value = this.drawArea.ActivePlan.Brightness;
			this.sliderBrightness.Text = this.sliderBrightness.Value.ToString();
			this.mainControl.Brightness = this.sliderBrightness.Value;
			this.sliderContrast.Value = this.drawArea.ActivePlan.Contrast;
			this.sliderContrast.Text = this.sliderContrast.Value.ToString();
			this.mainControl.Contrast = this.sliderContrast.Value;
			this.mainControl.UseDynamicAdjustments = true;
			this.exitNow = false;
			this.btBrightnessContrastApply.Enabled = false;
			this.btBrightnessContrastRestore.Enabled = (this.drawArea.ActivePlan.Brightness != 0 || this.drawArea.ActivePlan.Contrast != 0);
			Utilities.SuspendDrawing(this);
			this.EnableDockingWindows(false, true);
			Utilities.ResumeDrawing(this);
			this.panelBrightnessContrast.Visible = true;
			this.mainControl.ZoomLock(100);
			this.ZoomRestricted = true;
			this.ImageEditing = true;
			this.helpContextString = "BrightnessContrastControls";
			this.UnlockInterface(ref flag);
		}

		private void BrightnessContrastApply()
		{
			if (this.sliderBrightness.Value == 0 && this.sliderContrast.Value == 0)
			{
				this.BrightnessContrastRestore();
				return;
			}
			bool flag = false;
			this.LockInterface(ref flag);
			this.drawArea.ActivePlan.Brightness = this.sliderBrightness.Value;
			this.drawArea.ActivePlan.Contrast = this.sliderContrast.Value;
			this.mainControl.ApplyBrightnessAndContrast(this.sliderBrightness.Value, this.sliderContrast.Value);
			this.drawArea.ActivePlan.CreateCache((Bitmap)this.mainControl.Image);
			this.drawArea.ActivePlan.ReloadThumbnail();
			this.panControl.Reload();
			this.ResetBrightnessContrastControls();
			this.drawArea.ActivePlan.Layers.RestoreVisible();
			this.RestoreBookmark();
			this.SetModified();
			this.UnlockInterface(ref flag);
		}

		private void BrightnessContrastCancel()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.mainControl.ApplyBrightnessAndContrast(this.drawArea.ActivePlan.Brightness, this.drawArea.ActivePlan.Contrast);
			this.ResetBrightnessContrastControls();
			this.drawArea.ActivePlan.Layers.RestoreVisible();
			this.RestoreBookmark();
			this.UnlockInterface(ref flag);
		}

		private void BrightnessContrastRestore()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.drawArea.ActivePlan.Brightness = 0;
			this.drawArea.ActivePlan.Contrast = 0;
			this.mainControl.ReloadZoomedImages();
			this.drawArea.ActivePlan.ReloadThumbnail();
			this.drawArea.ActivePlan.DeleteCache();
			this.panControl.Reload();
			this.ResetBrightnessContrastControls();
			this.drawArea.ActivePlan.Layers.RestoreVisible();
			this.RestoreBookmark();
			this.SetModified();
			this.UnlockInterface(ref flag);
		}

        private bool DoRotateFlip(RotateFlipType rotateFlip)
        {
            bool succeeded = false;

            while (true)
            {
                try
                {
                    this.mainControl.Image.RotateFlip(rotateFlip);
                    succeeded = true;
                    break;
                }
                catch (Exception)
                {
                    string operationFailedTitle = Resources.Impossible_d_effectuer_l_opération_désirée;
                    string insufficientResourcesMessage = Resources.Ressources_insuffisantes_pour_compléter_l_opération;

                    DialogResult choice = Utilities.DisplayWarningQuestionRetryCancel(
                        operationFailedTitle,
                        insufficientResourcesMessage);

                    if (choice == DialogResult.Cancel)
                        break;

                    // any non-cancel => retry (same as original)
                }
            }

            return succeeded;
        }

        private void RotateFlip(RotateFlipType rotateFlip)
		{
			if (this.mainControl.Image == null)
			{
				return;
			}
			bool flag = false;
			this.LockInterface(ref flag);
			try
			{
				if (this.DoRotateFlip(rotateFlip))
				{
					this.mainControl.ReloadImage(true);
					this.mainControl.ZoomLock(this.mainControl.FitToScreenZoom);
					this.btRotationApply.Enabled = true;
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			this.UnlockInterface(ref flag);
		}

		private void FlipHorizontally()
		{
			this.RotateFlip(RotateFlipType.RotateNoneFlipX);
		}

		private void FlipVertically()
		{
			this.RotateFlip(RotateFlipType.Rotate180FlipX);
		}

		private void RotateLeft()
		{
			this.RotateFlip(RotateFlipType.Rotate270FlipNone);
		}

		private void RotateRight()
		{
			this.RotateFlip(RotateFlipType.Rotate90FlipNone);
		}

		private void ShowRotationControls()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.SelectPointerTool();
			this.imageEditingBookmark = new Bookmark("", this.drawArea.ActiveLayerIndex, this.mainControl.Zoom, new Point((int)this.mainControl.Origin.X, (int)this.mainControl.Origin.Y));
			this.drawArea.ActivePlan.Layers.HideAll();
			if (this.drawArea.ActivePlan.Brightness != 0 || this.drawArea.ActivePlan.Contrast != 0)
			{
				this.ReloadImage(this.drawArea.ActivePlan.FullFileName, false);
				this.mainControl.Refresh();
				this.panControl.Reload();
			}
			this.btRotationApply.Enabled = false;
			Utilities.SuspendDrawing(this);
			this.EnableDockingWindows(false, true);
			Utilities.ResumeDrawing(this);
			this.panelRotation.Visible = true;
			this.mainControl.ZoomLock(this.mainControl.FitToScreenZoom);
			this.ZoomRestricted = true;
			this.ImageEditing = true;
			Application.DoEvents();
			this.mainControl.ClearCache();
			this.helpContextString = "RotationControls";
			this.UnlockInterface(ref flag);
		}

		private void RotationApply()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.drawArea.ActivePlan.SaveFromViewer((Bitmap)this.mainControl.Image);
			if (this.drawArea.ActivePlan.Brightness != 0 || this.drawArea.ActivePlan.Contrast != 0)
			{
				this.mainControl.ApplyBrightnessAndContrast(this.drawArea.ActivePlan.Brightness, this.drawArea.ActivePlan.Contrast);
				this.drawArea.ActivePlan.CreateCache((Bitmap)this.mainControl.Image);
			}
			this.drawArea.ActivePlan.DeleteThumbnail();
			this.drawArea.ActivePlan.CreateThumbnail(false);
			this.drawArea.ActivePlan.GetImageDimension();
			this.panControl.Reload();
			this.ResetRotationControls();
			this.drawArea.ActivePlan.Layers.RestoreVisible();
			this.RestoreBookmark();
			this.SetModified();
			if (this.suspendScaleValidate)
			{
				this.ScaleValidate();
				this.suspendScaleValidate = false;
			}
			this.UnlockInterface(ref flag);
		}

		private void RotationCancel()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			if (this.btRotationApply.Enabled)
			{
				this.ReloadPlan(false);
				this.panControl.Reload();
			}
			this.ResetRotationControls();
			this.drawArea.ActivePlan.Layers.RestoreVisible();
			this.RestoreBookmark();
			if (this.suspendScaleValidate)
			{
				this.ScaleValidate();
				this.suspendScaleValidate = false;
			}
			this.UnlockInterface(ref flag);
		}

		private void PlanInsertFromImages()
		{
			if (this.project.FullFileName == string.Empty && !this.FileSave(string.Empty))
			{
				return;
			}
			this.plansNavigator.SetFocus(false);
			string[] array = Utilities.OpenMultiFilesDialog(Resources.Importer_des_fichiers_images, Utilities.GetImagesFolder(), Resources.Fichiers_images + " (*.png; *.tif; *.tiff; *.jpg; *.bmp)|*.png; *.tif; *.tiff; *.jpg; *.bmp|Tous les fichiers (*.*)|*.*");
			if (array == null)
			{
				this.plansNavigator.SetFocus(true);
				return;
			}
			bool flag = false;
			this.LockInterface(ref flag);
			this.lblNoPlan.Visible = false;
			this.flowPlans.Visible = true;
			string directoryName = Utilities.GetDirectoryName(array[0].ToString());
			if (directoryName != Utilities.GetPlansFolder())
			{
				Settings.Default.ImagesFolder = directoryName;
			}
			this.plansNavigator.ImportImageFiles(array);
			this.objectsNavigator.RefreshListOfPlans(this.drawArea.ActivePlan);
			this.SetModified();
			this.UnlockInterface(ref flag);
			this.plansNavigator.SetFocus(true);
		}

		private void PlanInsertFromPDFFiles()
		{
			if (this.project.FullFileName == string.Empty && !this.FileSave(string.Empty))
			{
				return;
			}
			this.plansNavigator.SetFocus(false);
			string[] array = Utilities.OpenMultiFilesDialog(Resources.Importer_des_fichiers_PDF, Utilities.GetPDFFolder(), Resources.Fichiers_PDF + " (*.pdf)|*.pdf|" + Resources.Tous_les_fichiers + " (*.*)|*.*");
			if (array == null)
			{
				this.plansNavigator.SetFocus(true);
				return;
			}
			bool flag = false;
			this.LockInterface(ref flag);
			this.lblNoPlan.Visible = false;
			this.flowPlans.Visible = true;
			string directoryName = Utilities.GetDirectoryName(array[0].ToString());
			if (directoryName != Utilities.GetPDFFolder())
			{
				Settings.Default.PDFFolder = directoryName;
			}
			this.SuspendPlan();
			this.plansNavigator.ImportPDFFiles(array);
			this.objectsNavigator.RefreshListOfPlans(this.drawArea.ActivePlan);
			this.RestorePlan();
			this.SetModified();
			this.UnlockInterface(ref flag);
			this.plansNavigator.SetFocus(true);
		}

		private void EnableManualDpi(bool enable)
		{
			this.sliderDpi.Enabled = enable;
			this.sliderDpi.TextColor = (enable ? Color.Black : Color.LightGray);
			this.lblDpi1.ForeColor = (enable ? Color.Black : Color.LightGray);
			this.lblDpi2.ForeColor = (enable ? Color.Black : Color.LightGray);
		}

		private void op172Dpi_Click(object sender, EventArgs e)
		{
			this.op300Dpi.Checked = false;
			this.opOtherDpi.Checked = false;
			this.EnableManualDpi(false);
			Settings.Default.ImportDpi = 172;
		}

		private void op300Dpi_Click(object sender, EventArgs e)
		{
			this.op172Dpi.Checked = false;
			this.opOtherDpi.Checked = false;
			this.EnableManualDpi(false);
			Settings.Default.ImportDpi = 300;
		}

		private void opOtherDpi_Click(object sender, EventArgs e)
		{
			this.op172Dpi.Checked = false;
			this.op300Dpi.Checked = false;
			this.EnableManualDpi(true);
			Settings.Default.ImportDpi = Settings.Default.ImportManualDpi;
		}

		private void sliderDpi_ValueChanged(object sender, EventArgs e)
		{
			this.sliderDpi.Text = this.sliderDpi.Value + " dpi";
			Settings.Default.ImportDpi = this.sliderDpi.Value;
			Settings.Default.ImportManualDpi = this.sliderDpi.Value;
		}

		private void opConvertToColor_Click(object sender, EventArgs e)
		{
			if (this.opConvertToColor.Checked)
			{
				string veuillez_confirmer_votre_choix = Resources.Veuillez_confirmer_votre_choix;
				string confirmer_importation_couleur = Resources.Confirmer_importation_couleur;
				if (Utilities.DisplayWarningQuestion(veuillez_confirmer_votre_choix, confirmer_importation_couleur) == DialogResult.No)
				{
					this.opConvertToColor.Checked = false;
				}
			}
			Settings.Default.ConvertPDFToColor = this.opConvertToColor.Checked;
		}

		private void BuildInsertFromPDFSettingsMenu()
		{
			int importDpi = Settings.Default.ImportDpi;
			bool convertPDFToColor = Settings.Default.ConvertPDFToColor;
			this.op172Dpi.Checked = false;
			this.op300Dpi.Checked = false;
			this.opOtherDpi.Checked = false;
			this.sliderDpi.Value = Settings.Default.ImportManualDpi;
			this.EnableManualDpi(false);
			int num = importDpi;
			if (num != 172)
			{
				if (num != 300)
				{
					this.opOtherDpi.Checked = true;
					this.EnableManualDpi(true);
				}
				else
				{
					this.op300Dpi.Checked = true;
				}
			}
			else
			{
				this.op172Dpi.Checked = true;
			}
			this.opConvertToColor.Checked = convertPDFToColor;
		}

		private void PlanLoad()
		{
			Plan selectedPlan = this.plansNavigator.SelectedPlan;
			if (selectedPlan == null)
			{
				return;
			}
			this.plansNavigator_OnPlanLoad(selectedPlan);
		}

		private void planPropertiesForm_OnPlanSelected(Plan plan)
		{
			this.plansNavigator.SelectedPlan = plan;
		}

		private void planPropertiesForm_OnSave(Plan plan)
		{
			this.plansNavigator.Refresh();
			this.recentPlansNavigator.Rename(plan, plan.Name);
			this.objectsNavigator.RefreshListOfPlans(this.drawArea.ActivePlan);
			this.reportsControl.ReportModule.RenamePlan(plan);
			this.SetModified();
		}

		private void PlanProperties()
		{
			Plan selectedPlan = this.plansNavigator.SelectedPlan;
			if (selectedPlan == null)
			{
				return;
			}
			int num = this.project.Plans.PlanIndex(selectedPlan);
			if (num == -1)
			{
				return;
			}
			this.plansNavigator.SetFocus(false);
			try
			{
				using (PlanPropertiesForm planPropertiesForm = new PlanPropertiesForm(num, this.project.Plans, this.drawArea))
				{
					planPropertiesForm.HelpUtilities = this.HelpUtilities;
					planPropertiesForm.HelpContextString = "PlanPropertiesForm";
					planPropertiesForm.OnPlanSelected += this.planPropertiesForm_OnPlanSelected;
					planPropertiesForm.OnSave += this.planPropertiesForm_OnSave;
					planPropertiesForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			this.plansNavigator.SetFocus(true);
		}

		private void RemovePlanSource(Plan plan)
		{
			plan.DeleteFile();
		}

		private void RemovePlanFromProject(Plan plan)
		{
			if (this.drawArea.ActivePlan != null && this.drawArea.ActivePlan.Equals(plan))
			{
				this.UnloadPlan();
			}
			this.recentPlansNavigator.Remove(plan);
			this.plansNavigator.Remove(plan);
			this.project.RemovePlan(plan);
			if (this.project.Plans.Count == 0)
			{
				this.lblNoPlan.Visible = true;
				this.flowPlans.Visible = false;
			}
			this.objectsNavigator.RefreshListOfPlans(null);
		}

		private void planRemoveForm_OnRemove(Plan plan)
		{
			this.RemovePlanFromProject(plan);
			this.SetModified();
		}

		private void planRemoveForm_OnRemoveWithData(Plan plan)
		{
			this.RemovePlanSource(plan);
			this.RemovePlanFromProject(plan);
			this.SetModified();
		}

		private void PlanRemove()
		{
			Plan selectedPlan = this.plansNavigator.SelectedPlan;
			if (selectedPlan == null)
			{
				return;
			}
			this.plansNavigator.SetFocus(false);
			try
			{
				using (PlanRemoveForm planRemoveForm = new PlanRemoveForm(selectedPlan, this.drawArea))
				{
					planRemoveForm.HelpUtilities = this.HelpUtilities;
					planRemoveForm.HelpContextString = "PlanRemoveForm";
					planRemoveForm.OnPlanSelected += this.planPropertiesForm_OnPlanSelected;
					planRemoveForm.OnRemove += this.planRemoveForm_OnRemove;
					planRemoveForm.OnRemoveWithData += this.planRemoveForm_OnRemoveWithData;
					planRemoveForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			this.plansNavigator.SetFocus(true);
		}

		private void DuplicatePlanInProject(Plan plan, int numberOfCopies, bool copyObjects)
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.plansNavigator.DuplicateFromProject(plan, numberOfCopies, copyObjects);
			this.objectsNavigator.RefreshListOfPlans(this.drawArea.ActivePlan);
			this.SetModified();
			this.UnlockInterface(ref flag);
		}

		private void planDuplicateForm_OnDuplicate(Plan plan, int numberOfCopies, bool copyObjects)
		{
			this.duplicatePlanNumberOfCopies = numberOfCopies;
			this.duplicatePlanCopyObjects = copyObjects;
		}

		private void PlanDuplicate()
		{
			Plan selectedPlan = this.plansNavigator.SelectedPlan;
			if (selectedPlan == null)
			{
				return;
			}
			this.plansNavigator.SetFocus(false);
			this.duplicatePlanNumberOfCopies = 0;
			this.duplicatePlanCopyObjects = false;
			try
			{
				using (PlanDuplicateForm planDuplicateForm = new PlanDuplicateForm(selectedPlan, this.drawArea))
				{
					planDuplicateForm.HelpUtilities = this.HelpUtilities;
					planDuplicateForm.HelpContextString = "PlanDuplicateForm";
					planDuplicateForm.OnDuplicate += this.planDuplicateForm_OnDuplicate;
					planDuplicateForm.ShowDialog(this);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			if (this.duplicatePlanNumberOfCopies > 0)
			{
				this.DuplicatePlanInProject(selectedPlan, this.duplicatePlanNumberOfCopies, this.duplicatePlanCopyObjects);
			}
			this.plansNavigator.SetFocus(true);
		}

		private void PlanApplyAction(PlansNavigator.PlansActionEnum action, Plan plan, int index, int count)
		{
			if (action == PlansNavigator.PlansActionEnum.PlansActionPrint)
			{
				this.selectedPlans.Add(new Variable(plan.FullFileName, plan));
				return;
			}
			int num = Utilities.ConvertToInt((double)index * (100.0 / (double)count));
			this.ribbonBarPlansAction.Text = num + " %";
			this.progressPlansAction.Value = num;
			Application.DoEvents();
			if (this.checkBoxExportSingleFile.Checked)
			{
				this.singlePDFFileName = this.AddPlanToPDF(this.singlePDFDoc, plan, this.project.DisplayName);
				return;
			}
			this.singlePDFDoc = new PdfDocument();
			PDFUtilities.SetPDFExportDocumentProperties(this.singlePDFDoc, this.project.Name, "", "", "", string.Format("{0} - {1}", Utilities.ApplicationName, Utilities.ApplicationWebsite));
			this.singlePDFFileName = this.AddPlanToPDF(this.singlePDFDoc, plan, plan.Name);
			if (this.singlePDFFileName != string.Empty && this.SavePDFDoc(this.singlePDFDoc, this.singlePDFFileName, false) && index == count)
			{
				Utilities.OpenDocument(Path.GetDirectoryName(this.singlePDFFileName));
			}
			this.singlePDFDoc.Close();
			this.singlePDFDoc.Dispose();
		}

		private void ShowPlansActionControls(PlansNavigator.PlansActionEnum action)
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.SelectPointerTool();
			this.plansAction = action;
			if (this.plansAction == PlansNavigator.PlansActionEnum.PlansActionPrint)
			{
				this.btPlansActionApply.Text = Resources.Confirmer_impression;
				this.btPlansActionApply.Image = Resources.print_apply_40x40;
				this.itemContainerExportType.Visible = false;
				this.helpContextString = "PlansActionPrint";
			}
			else
			{
				this.btPlansActionApply.Text = Resources.Confirmer_exportation;
				this.btPlansActionApply.Image = Resources.pdf_apply_40x40;
				this.itemContainerExportType.Visible = true;
				this.helpContextString = "PlansActionExport";
			}
			this.ribbonBarPlansAction.Text = Resources.Cliquer_pour_sélectionner_désélectionner_un_plan;
			this.progressPlansAction.Text = Resources.Traitement_en_cours;
			this.btPlansActionSelectAll.Visible = true;
			this.btPlansActionSelectNone.Visible = true;
			this.btPlansActionApply.Enabled = true;
			this.btPlansActionCancel.Enabled = true;
			this.progressPlansAction.Visible = false;
			this.ribbonBarPlansAction.RecalcLayout();
			this.panelPlansAction.Visible = true;
			this.SuspendPlan();
			this.PlansSelection = true;
			this.UnlockInterface(ref flag);
		}

		private void PlansActionApply()
		{
			bool flag = false;
			if (this.plansNavigator.SelectedCount() == 0)
			{
				string aucune_sélection = Resources.Aucune_sélection;
				string veuillez_sélectionner_au_moins_un_plan = Resources.Veuillez_sélectionner_au_moins_un_plan;
				Utilities.DisplayError(aucune_sélection, veuillez_sélectionner_au_moins_un_plan);
				return;
			}
			this.LockInterface(ref flag);
			this.btPlansActionApply.Enabled = false;
			this.btPlansActionCancel.Enabled = false;
			this.btPlansActionSelectAll.Visible = false;
			this.btPlansActionSelectNone.Visible = false;
			this.progressPlansAction.Value = 0;
			this.progressPlansAction.Visible = true;
			this.itemContainerExportType.Visible = false;
			this.ribbonBarPlansAction.RecalcLayout();
			Application.DoEvents();
			if (this.plansAction == PlansNavigator.PlansActionEnum.PlansActionPrint)
			{
				this.selectedPlanIndex = 0;
				this.selectedPlans.Clear();
			}
			else if (this.checkBoxExportSingleFile.Checked)
			{
				this.singlePDFDoc = new PdfDocument();
				PDFUtilities.SetPDFExportDocumentProperties(this.singlePDFDoc, this.project.Name, "", "", "", string.Format("{0} - {1}", Utilities.ApplicationName, Utilities.ApplicationWebsite));
			}
			this.plansNavigator.ActionApply(this.plansAction);
			if (this.plansAction == PlansNavigator.PlansActionEnum.PlansActionPrint)
			{
				this.PrintPlanSetup();
			}
			this.ResetPlansActionControls();
			if (this.plansAction == PlansNavigator.PlansActionEnum.PlansActionPrint)
			{
				this.selectedPlans.Clear();
			}
			else if (this.checkBoxExportSingleFile.Checked && this.singlePDFFileName != string.Empty)
			{
				this.SavePDFDoc(this.singlePDFDoc, this.singlePDFFileName, true);
				this.singlePDFDoc.Close();
				this.singlePDFDoc.Dispose();
			}
			this.UnlockInterface(ref flag);
		}

		private void PlansActionCancel()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.ResetPlansActionControls();
			this.UnlockInterface(ref flag);
		}

		private ReportEditForm FormReportEdit
		{
			get
			{
				if (this.frmReportEdit == null)
				{
					this.frmReportEdit = new ReportEditForm(this.project, this.drawArea);
					this.frmReportEdit.HelpUtilities = this.HelpUtilities;
					this.frmReportEdit.HelpContextString = "ReportEditForm";
				}
				return this.frmReportEdit;
			}
		}

		private void ReportEditFilter()
		{
			try
			{
				this.FormReportEdit.Initialize();
				this.FormReportEdit.ShowDialog(this);
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				return;
			}
			if (this.project.Report.Dirty)
			{
				this.SetModified();
				this.ReportRefresh();
			}
		}

		private void ReportEditSettings()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.ReportEditing = true;
			this.reportsControl.EditReportSettings();
			this.helpContextString = "EditSettingsControls";
			this.UnlockInterface(ref flag);
		}

		public void UpdateReportScaleSystemInGUI(bool refresh)
		{
			this.btReportScaleImperial.Checked = false;
			this.btReportScaleMetric.Checked = false;
			switch (this.project.Report.SystemType)
			{
			case UnitScale.UnitSystem.metric:
				this.btReportScaleMetric.Checked = true;
				break;
			case UnitScale.UnitSystem.imperial:
				this.btReportScaleImperial.Checked = true;
				break;
			}
			if (refresh)
			{
				this.ReportRefresh();
			}
		}

		private void UpdateReportScalePrecisionInGUI(bool refresh)
		{
			this.btReportScalePrecision8.Checked = false;
			this.btReportScalePrecision16.Checked = false;
			this.btReportScalePrecision32.Checked = false;
			this.btReportScalePrecision64.Checked = false;
			switch (this.project.Report.Precision)
			{
			case UnitScale.UnitPrecision.precision8:
				this.btReportScalePrecision8.Checked = true;
				break;
			case UnitScale.UnitPrecision.precision16:
				this.btReportScalePrecision16.Checked = true;
				break;
			case UnitScale.UnitPrecision.precision32:
				this.btReportScalePrecision32.Checked = true;
				break;
			case UnitScale.UnitPrecision.precision64:
				this.btReportScalePrecision64.Checked = true;
				break;
			}
			if (refresh)
			{
				this.ReportRefresh();
			}
		}

		private void SetReportScaleSystemType(UnitScale.UnitSystem systemType)
		{
			this.project.Report.SystemType = systemType;
			this.UpdateReportScaleSystemInGUI(true);
			this.SetModified();
		}

		private void SetReportScalePrecision(UnitScale.UnitPrecision precision)
		{
			this.project.Report.Precision = precision;
			this.UpdateReportScalePrecisionInGUI(true);
			this.SetModified();
		}

		private void ValidateReportSystemType()
		{
			int num = 0;
			int num2 = 0;
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				if (plan.UnitScale.ScaleSystemType == UnitScale.UnitSystem.metric)
				{
					num++;
				}
				else if (plan.UnitScale.ScaleSystemType == UnitScale.UnitSystem.imperial)
				{
					num2++;
				}
			}
			if (num == num2)
			{
				this.SetReportScaleSystemType(UnitScale.DefaultUnitSystem());
				return;
			}
			this.SetReportScaleSystemType((num > num2) ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial);
		}

		private void ReportRefresh()
		{
			this.reportsControl.UpdateReport();
		}

		private void ReportPrint()
		{
			this.reportsControl.Print();
		}

		private void ReportPrintPreview()
		{
		}

		private void ReportPrintSetup()
		{
		}

		private void ReportExportToExcel()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.reportsControl.ExportToExcel();
			this.UnlockInterface(ref flag);
		}

		private void ReportExportToCSV()
		{
		}

		private void ReportExportToXML()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.reportsControl.ExportToXML();
			this.UnlockInterface(ref flag);
		}

		private void ReportExportToHTML()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.reportsControl.ExportToHTML();
			this.UnlockInterface(ref flag);
		}

		private void ReportExportToPDF()
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.reportsControl.ExportToPDF();
			this.UnlockInterface(ref flag);
		}

		private void btProjectNew_Click(object sender, EventArgs e)
		{
			this.FileNew();
		}

		private void btProjectOpen_Click(object sender, EventArgs e)
		{
			this.FileOpen(string.Empty);
		}

		private void lstRecentProjects_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.mruManager.Open(this.lstRecentProjects);
			}
		}

		private void btProjectSave_Click(object sender, EventArgs e)
		{
			this.FileSave(this.project.FullFileName);
		}

		private void btProjectSaveAs_Click(object sender, EventArgs e)
		{
			this.FileSave(string.Empty);
		}

		private void btProjectInfo_Click(object sender, EventArgs e)
		{
			this.FileProjectInfo();
		}

		private void btProjectClose_Click(object sender, EventArgs e)
		{
			this.FileClose(true);
			this.FileWelcome(!this.guiEnabled);
		}

		private void btExit_Click(object sender, EventArgs e)
		{
			this.FileExit();
		}

		private void btHelpYoutube_Click(object sender, EventArgs e)
		{
			Utilities.OpenDocument(Utilities.GetYoutubeUrl());
		}

		private void btHelpContent_Click(object sender, EventArgs e)
		{
			this.HelpContent();
		}

		private void btHelpAbout_Click(object sender, EventArgs e)
		{
			this.HelpAbout();
		}

		private void btEditCut_Click(object sender, EventArgs e)
		{
			this.CutSelectedObjects();
		}

		private void btEditCopy_Click(object sender, EventArgs e)
		{
			this.CopySelectedObjects();
		}

		private void btEditPaste_Click(object sender, EventArgs e)
		{
			this.PasteSelectedObjects();
		}

		private void btEditDelete_Click(object sender, EventArgs e)
		{
			this.DeleteSelectedObjects(true);
		}

		private void btEditBringToFront_Click(object sender, EventArgs e)
		{
			this.BringObjectsToFront();
		}

		private void btEditSendToBack_Click(object sender, EventArgs e)
		{
			this.SendObjectsToBack();
		}

		private void btEditUndo_Click(object sender, EventArgs e)
		{
			this.Undo();
		}

		private void btEditRedo_Click(object sender, EventArgs e)
		{
			this.Redo();
		}

		private void btEditSelectAll_Click(object sender, EventArgs e)
		{
			this.SelectAll();
		}

		private void btUnselectAll_Click(object sender, EventArgs e)
		{
			this.UnselectAll();
		}

		private void btSelectObjectType_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			if (buttonItem.Tag.ToString() == "")
			{
				return;
			}
			this.SelectObjectType((string)buttonItem.Tag);
		}

		private void btSelectThisGroup_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			if (buttonItem.Tag.ToString() == "")
			{
				return;
			}
			this.SelectThisGroup((int)buttonItem.Tag);
		}

		private void btEditSendData_PopupOpen(object sender, PopupOpenEventArgs e)
		{
			this.BuildSendDataMenu(this.btEditSendData);
		}

		private void btEditSendData_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			if (buttonItem.Tag.ToString() == "")
			{
				return;
			}
			this.SendDataToExternalApplication(Utilities.ConvertToDouble(buttonItem.Tag, -1));
		}

		private void btExportPlanToPDF_Click(object sender, EventArgs e)
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.ExportCurrentPlanToPDF();
			this.UnlockInterface(ref flag);
		}

		private void btPrintPlanType_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			Settings.Default.PrintPlanType = Utilities.ConvertToInt(buttonItem.Tag);
		}

		private void btPrintPlan_Click(object sender, EventArgs e)
		{
			this.PrintPlanSetup();
		}

		private void btScaleSet_Click(object sender, EventArgs e)
		{
			if (this.drawArea.UnitScale.StandardScaleDisable && this.drawArea.UnitScale.Scale == 0f)
			{
				this.drawArea.ScaleSetManually();
			}
			else
			{
				this.drawArea.ScaleSet(false);
			}
			this.ValidateReportSystemType();
		}

		private void btScaleImperial_Click(object sender, EventArgs e)
		{
			this.SetScaleCurrentSystemType(UnitScale.UnitSystem.imperial);
		}

		private void btScaleMetric_Click(object sender, EventArgs e)
		{
			this.SetScaleCurrentSystemType(UnitScale.UnitSystem.metric);
		}

		private void btScalePrecision8_Click(object sender, EventArgs e)
		{
			this.SetScalePrecision(UnitScale.UnitPrecision.precision8);
		}

		private void btScalePrecision16_Click(object sender, EventArgs e)
		{
			this.SetScalePrecision(UnitScale.UnitPrecision.precision16);
		}

		private void btScalePrecision32_Click(object sender, EventArgs e)
		{
			this.SetScalePrecision(UnitScale.UnitPrecision.precision32);
		}

		private void btScalePrecision64_Click(object sender, EventArgs e)
		{
			this.SetScalePrecision(UnitScale.UnitPrecision.precision64);
		}

		private void btToolSelection_Click(object sender, EventArgs e)
		{
			this.SelectPointerTool();
		}

		private void btToolPan_Click(object sender, EventArgs e)
		{
			this.SelectPanTool();
		}

		private void btToolArea_Click(object sender, EventArgs e)
		{
			this.SelectAreaTool(null, Color.Empty, false, false);
		}

		private void btToolPerimeter_Click(object sender, EventArgs e)
		{
			this.SelectPerimeterTool(null, Color.Empty, false, false);
		}

		private void btToolCounter_Click(object sender, EventArgs e)
		{
			this.SelectCounterTool(null, Color.Empty, false, false);
		}

		private void btToolRuler_Click(object sender, EventArgs e)
		{
			this.SelectDistanceTool(null, Color.Empty, false, false);
		}

		private void btToolAngle_Click(object sender, EventArgs e)
		{
			this.SelectAngleTool();
		}

		private void btMarkZone_Click(object sender, EventArgs e)
		{
			this.SelectMarkerTool();
		}

		private void btInsertNote_Click(object sender, EventArgs e)
		{
			this.SelectNoteTool();
		}

		private void btToolArea_PopupOpen(object sender, PopupOpenEventArgs e)
		{
			this.BuildToolPresetMenu(this.btToolArea, "Area");
		}

		private void btToolPerimeter_PopupOpen(object sender, PopupOpenEventArgs e)
		{
			this.BuildToolPresetMenu(this.btToolPerimeter, "Perimeter");
		}

		private void btToolCounter_PopupOpen(object sender, PopupOpenEventArgs e)
		{
			this.BuildToolPresetMenu(this.btToolCounter, "Counter");
		}

		private void btToolRuler_PopupOpen(object sender, PopupOpenEventArgs e)
		{
			this.BuildToolPresetMenu(this.btToolRuler, "Line");
		}

		private void btZoomIn_Click(object sender, EventArgs e)
		{
			this.ZoomIn();
		}

		private void btZoomOut_Click(object sender, EventArgs e)
		{
			this.ZoomOut();
		}

		private void btZoomActualSize_Click(object sender, EventArgs e)
		{
			this.ZoomSet(100);
		}

		private void btZoom25_Click(object sender, EventArgs e)
		{
			this.ZoomSet(25);
		}

		private void btZoom50_Click(object sender, EventArgs e)
		{
			this.ZoomSet(50);
		}

		private void btZoom75_Click(object sender, EventArgs e)
		{
			this.ZoomSet(75);
		}

		private void btZoomToWindow_Click(object sender, EventArgs e)
		{
			this.FitToScreen();
		}

		private void btZoomToSelection_Click(object sender, EventArgs e)
		{
			this.ZoomToSelection();
		}

		private void btZoomToObject_Click(object sender, EventArgs e)
		{
			this.ZoomToSelectedObject();
		}

		private void btZoomToGroup_Click(object sender, EventArgs e)
		{
			this.ZoomToSelectedGroup();
		}

		private void btLayerAdd_Click(object sender, EventArgs e)
		{
			this.LayerAdd();
		}

		private void btLayerEdit_Click(object sender, EventArgs e)
		{
			int num = this.layersNavigator.SelectedLayerIndex();
			if (num > -1)
			{
				this.LayerEdit(num);
			}
		}

		private void btLayerRemove_Click(object sender, EventArgs e)
		{
			int num = this.layersNavigator.SelectedLayerIndex();
			if (num > -1)
			{
				this.LayerRemove(num);
			}
		}

		private void btLayerMoveTo_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			if (buttonItem.Tag.ToString() == "")
			{
				return;
			}
			this.drawArea.UnselectLegend();
			this.LayerMoveTo((int)buttonItem.Tag, new CommandChangeStateEx(this.drawArea, this.drawArea.ActiveLayerName, true, true));
		}

		private void btLayerMoveUp_Click(object sender, EventArgs e)
		{
			int num = this.layersNavigator.SelectedLayerIndex();
			if (num > -1)
			{
				this.LayerMoveUp(num);
			}
		}

		private void btLayerMoveDown_Click(object sender, EventArgs e)
		{
			int num = this.layersNavigator.SelectedLayerIndex();
			if (num > -1)
			{
				this.LayerMoveDown(num);
			}
		}

		private void btLayerSaveList_Click(object sender, EventArgs e)
		{
			this.LayerSaveList();
		}

		private void btLayerSaveListAs_Click(object sender, EventArgs e)
		{
			this.LayerSaveListAs();
		}

		private void btLayerOpenList_Click(object sender, EventArgs e)
		{
			this.LayerOpenList();
		}

		private void btLayersMakeVisible_Click(object sender, EventArgs e)
		{
			this.LayersMakeVisible();
		}

		private void btLayersMakeInvisible_Click(object sender, EventArgs e)
		{
			this.LayersMakeInvisible();
		}

		private void btGroupLocate_Click(object sender, EventArgs e)
		{
			DrawObject selectedObject = this.objectsNavigator.SelectedObject;
			if (selectedObject != null)
			{
				this.objectsNavigator.ToggleVisibility(selectedObject);
				this.EnsureObjectVisible(selectedObject, true);
				this.ObjectSelect(selectedObject, false, true);
			}
		}

		private void btGroupZoomToObject_Click(object sender, EventArgs e)
		{
			DrawObject selectedObject = this.objectsNavigator.SelectedObject;
			if (selectedObject != null)
			{
				this.objectsNavigator.ToggleVisibility(selectedObject);
				this.ZoomObject(selectedObject, false);
				this.ObjectSelect(selectedObject, false, true);
			}
		}

		private void btGroupSelect_Click(object sender, EventArgs e)
		{
			DrawObject selectedObject = this.objectsNavigator.SelectedObject;
			if (selectedObject != null)
			{
				this.objectsNavigator.ToggleVisibility(selectedObject);
				this.ZoomObject(selectedObject, true);
				this.ObjectSelect(selectedObject, true, true);
			}
		}

		private void btGroupRename_Click(object sender, EventArgs e)
		{
			DrawObject selectedObject = this.objectsNavigator.SelectedObject;
			if (selectedObject != null)
			{
				this.objectsNavigator.Edit(selectedObject);
			}
		}

		private void btGroupRemove_Click(object sender, EventArgs e)
		{
			DrawObject selectedObject = this.objectsNavigator.SelectedObject;
			if (selectedObject != null)
			{
				this.ObjectSelect(selectedObject, true, true);
				this.DeleteSelectedObjects(true);
			}
		}

		private void btRenamePlan_Click(object sender, EventArgs e)
		{
			if (this.drawArea.ActivePlan != null)
			{
				this.objectsNavigator.EditingPlanName = true;
			}
		}

		private void btGroupsMakeVisible_Click(object sender, EventArgs e)
		{
			this.objectsNavigator.ToggleAllVisibility(true);
		}

		private void btGroupsMakeInvisible_Click(object sender, EventArgs e)
		{
			this.objectsNavigator.ToggleAllVisibility(false);
		}

		private void btPlanRename_Click(object sender, EventArgs e)
		{
			this.recentPlansNavigator.Edit();
		}

		private void btBrowsePrevious_Click(object sender, EventArgs e)
		{
			this.GroupBrowsePrevious(false);
		}

		private void btBrowseNext_Click(object sender, EventArgs e)
		{
			this.GroupBrowseNext(false);
		}

		private void btBrowseObjectTypePrevious_Click(object sender, EventArgs e)
		{
			this.GroupBrowsePrevious(true);
		}

		private void btBrowseObjectTypeNext_Click(object sender, EventArgs e)
		{
			this.GroupBrowseNext(true);
		}

		private void btBrightnessContrast_Click(object sender, EventArgs e)
		{
			if (this.btBrightnessContrast.Checked)
			{
				return;
			}
			this.ShowBrightnessContrastControls();
		}

		private void btBrightnessContrastApply_Click(object sender, EventArgs e)
		{
			this.BrightnessContrastApply();
		}

		private void btBrightnessContrastCancel_Click(object sender, EventArgs e)
		{
			this.BrightnessContrastCancel();
		}

		private void btBrightnessContrastRestore_Click(object sender, EventArgs e)
		{
			this.BrightnessContrastRestore();
		}

		private void btFlipHorizontally_Click(object sender, EventArgs e)
		{
			this.FlipHorizontally();
		}

		private void btFlipVertically_Click(object sender, EventArgs e)
		{
			this.FlipVertically();
		}

		private void btRotateLeft_Click(object sender, EventArgs e)
		{
			this.RotateLeft();
		}

		private void btRotateRight_Click(object sender, EventArgs e)
		{
			this.RotateRight();
		}

		private void btRotation_Click(object sender, EventArgs e)
		{
			if (this.btRotation.Checked)
			{
				return;
			}
			this.ShowRotationControls();
		}

		private void btRotationApply_Click(object sender, EventArgs e)
		{
			this.RotationApply();
		}

		private void btRotationCancel_Click(object sender, EventArgs e)
		{
			this.RotationCancel();
		}

		private void btPlanInsertFromPDF_Click(object sender, EventArgs e)
		{
			this.PlanInsertFromPDFFiles();
		}

		private void btPlanInsertFromPDF_PopupOpen(object sender, PopupOpenEventArgs e)
		{
			this.BuildInsertFromPDFSettingsMenu();
		}

		private void btPlanInsertFromImage_Click(object sender, EventArgs e)
		{
			this.PlanInsertFromImages();
		}

		private void btPlanLoad_Click(object sender, EventArgs e)
		{
			this.PlanLoad();
		}

		private void btPlanProperties_Click(object sender, EventArgs e)
		{
			this.PlanProperties();
		}

		private void btPlanRemove_Click(object sender, EventArgs e)
		{
			this.PlanRemove();
		}

		private void btPlanDuplicate_Click(object sender, EventArgs e)
		{
			this.PlanDuplicate();
		}

		private void btPlanExport_Click(object sender, EventArgs e)
		{
		}

		private void btPlansPrint_Click(object sender, EventArgs e)
		{
			if (this.btPlansPrint.Checked)
			{
				return;
			}
			this.ShowPlansActionControls(PlansNavigator.PlansActionEnum.PlansActionPrint);
		}

		private void btPlansExport_Click(object sender, EventArgs e)
		{
			if (this.btPlansExport.Checked)
			{
				return;
			}
			this.ShowPlansActionControls(PlansNavigator.PlansActionEnum.PlansActionExport);
		}

		private void btPlansActionApply_Click(object sender, EventArgs e)
		{
			this.PlansActionApply();
		}

		private void btPlansActionCancel_Click(object sender, EventArgs e)
		{
			this.PlansActionCancel();
		}

		private void btPlansActionSelectAll_Click(object sender, EventArgs e)
		{
			this.plansNavigator.SelectAll(true);
		}

		private void btPlansActionSelectNone_Click(object sender, EventArgs e)
		{
			this.plansNavigator.SelectAll(false);
		}

		private void btReportEdit_Click(object sender, EventArgs e)
		{
			this.ReportEditFilter();
		}

		private void btReportEdit_PopupOpen(object sender, PopupOpenEventArgs e)
		{
		}

		private void btReportScaleImperial_Click(object sender, EventArgs e)
		{
			this.SetReportScaleSystemType(UnitScale.UnitSystem.imperial);
		}

		private void btReportScaleMetric_Click(object sender, EventArgs e)
		{
			this.SetReportScaleSystemType(UnitScale.UnitSystem.metric);
		}

		private void btReportScalePrecision8_Click(object sender, EventArgs e)
		{
			this.SetReportScalePrecision(UnitScale.UnitPrecision.precision8);
		}

		private void btReportScalePrecision16_Click(object sender, EventArgs e)
		{
			this.SetReportScalePrecision(UnitScale.UnitPrecision.precision16);
		}

		private void btReportScalePrecision32_Click(object sender, EventArgs e)
		{
			this.SetReportScalePrecision(UnitScale.UnitPrecision.precision32);
		}

		private void btReportScalePrecision64_Click(object sender, EventArgs e)
		{
			this.SetReportScalePrecision(UnitScale.UnitPrecision.precision64);
		}

		private void btReportSettings_Click(object sender, EventArgs e)
		{
			this.ReportEditSettings();
		}

		private void btReportPrint_Click(object sender, EventArgs e)
		{
			this.ReportPrint();
		}

		private void btReportPrintPreview_Click(object sender, EventArgs e)
		{
			this.ReportPrintPreview();
		}

		private void btReportPrintSetup_Click(object sender, EventArgs e)
		{
			this.ReportPrintSetup();
		}

		private void btExportToExcelType_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			Settings.Default.ExportToExcelType = Utilities.ConvertToInt(buttonItem.Tag);
		}

		private void btReportExportToExcel_Click(object sender, EventArgs e)
		{
			this.ReportExportToExcel();
		}

		private void btReportExportToCSV_Click(object sender, EventArgs e)
		{
			this.ReportExportToCSV();
		}

		private void btReportExportToXML_Click(object sender, EventArgs e)
		{
			this.ReportExportToXML();
		}

		private void btReportExportToHTML_Click(object sender, EventArgs e)
		{
			this.ReportExportToHTML();
		}

		private void btReportExportToPDF_Click(object sender, EventArgs e)
		{
			this.ReportExportToPDF();
		}

		private void btPointInsert_Click(object sender, EventArgs e)
		{
			this.PointInsert(this.contextMenuPoint, true);
		}

		private void btPointRemove_Click(object sender, EventArgs e)
		{
			this.PointRemove(this.contextMenuPoint);
		}

		private void btDropInsert_Click(object sender, EventArgs e)
		{
			this.DropInsert(this.contextMenuPoint, true);
		}

		private void btDropRemove_Click(object sender, EventArgs e)
		{
			this.DropRemove(this.contextMenuPoint);
		}

		private void btDeductionCreate_Click(object sender, EventArgs e)
		{
			this.DeductionCreate();
		}

		private void btDeductionsEdit_Click(object sender, EventArgs e)
		{
			this.DeductionsEdit();
		}

		private void btDeductionDuplicate_Click(object sender, EventArgs e)
		{
			this.DeductionDuplicate();
		}

		private void btPerimeterClose_Click(object sender, EventArgs e)
		{
			this.PerimeterUpdateFigure(true);
		}

		private void btPerimeterOpen_Click(object sender, EventArgs e)
		{
			this.PerimeterUpdateFigure(false);
		}

		private void btPerimeterCreateFromArea_Click(object sender, EventArgs e)
		{
			this.PerimeterCreateFromArea();
		}

		private void bOpeningHeight_Click(object sender, EventArgs e)
		{
			this.SetHeight(this.contextMenuPoint);
		}

		private void btOpeningCreateFromPosition_Click(object sender, EventArgs e)
		{
			this.OpeningInsert(this.contextMenuPoint);
		}

		private void btOpeningDuplicate_Click(object sender, EventArgs e)
		{
			this.OpeningDuplicate(this.contextMenuPoint);
		}

		private void btOpeningCreateFromSegment_Click(object sender, EventArgs e)
		{
			this.OpeningCreateFromSegment();
		}

		private void btOpeningDelete_Click(object sender, EventArgs e)
		{
			this.OpeningDelete();
		}

		private void btGroupAddObject_Click(object sender, EventArgs e)
		{
			this.GroupAddObject();
		}

		private void btGroupMoveTo_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			if (buttonItem.Tag.ToString() == "")
			{
				return;
			}
			this.GroupMoveTo((DrawObject)buttonItem.Tag);
		}

		private void btGroupMoveToNew_Click(object sender, EventArgs e)
		{
			this.GroupMoveToNew();
		}

		private void btEditSelectGroup_Click(object sender, EventArgs e)
		{
			this.GroupSelect();
		}

		private void btAutoAdjustToZone_Click(object sender, EventArgs e)
		{
			if (this.drawArea.SelectedPolyLine != null)
			{
				this.AutoAdjust(DrawPolyLine.AutoAdjustType.ToZone);
				return;
			}
			this.AutoAdjustNote();
		}

		private void btAngleDegreeType_Click(object sender, EventArgs e)
		{
			this.AngleSetType(DrawAngle.AngleTypeEnum.AngleDegree);
		}

		private void btAngleSlopeType_Click(object sender, EventArgs e)
		{
			this.AngleSetType(DrawAngle.AngleTypeEnum.AngleSlope);
		}

		private void btEditNote_Click(object sender, EventArgs e)
		{
			this.NoteEdit();
		}

		private void btToggleMeasures_Click(object sender, EventArgs e)
		{
			this.ToggleMeasures();
		}

		private void btLicensing_Click(object sender, EventArgs e)
		{
		}

		private void btLicenseActivate_Click(object sender, EventArgs e)
		{
			this.LaunchTurboActivate();
		}

		private void btLicenseDeactivate_Click(object sender, EventArgs e)
		{
			this.LicenseDeactivate();
		}

		private void btLicenseBuy_Click(object sender, EventArgs e)
		{
			this.LicenseBuy();
		}

		private void btEstimatingItemsExpandAll_Click(object sender, EventArgs e)
		{
			this.treeEstimatingItems.ExpandAll();
		}

		private void btEstimatingItemsCollapseAll_Click(object sender, EventArgs e)
		{
			this.treeEstimatingItems.CollapseAll();
		}

		private void btEstimatingItemsPrint_Click(object sender, EventArgs e)
		{
			this.treeEstimatingItems.ShowPrintPreview();
		}

		private void btEstimatingItemsUpdatePrices_Click(object sender, EventArgs e)
		{
			string mettre_a_jour_les_prix = Resources.Mettre_a_jour_les_prix;
			string voulez_vous_mettre_a_jour_votre_projet_courant_avec_les_prix_les_plus_recents_de_la_base_de_donnees = Resources.Voulez_vous_mettre_a_jour_votre_projet_courant_avec_les_prix_les_plus_recents_de_la_base_de_donnees;
			if (Utilities.DisplayQuestion(mettre_a_jour_les_prix, voulez_vous_mettre_a_jour_votre_projet_courant_avec_les_prix_les_plus_recents_de_la_base_de_donnees) == DialogResult.No)
			{
				return;
			}
			foreach (EstimatingItem estimatingItem in this.project.EstimatingItems.Collection)
			{
				if (estimatingItem.ItemID != -1)
				{
					DBEstimatingItem estimatingItem2 = this.project.DBManagement.GetEstimatingItem(estimatingItem.ItemID);
					if (estimatingItem2 != null)
					{
						this.project.EstimatingItems.SaveEstimatingItemCost(estimatingItem.GroupID, estimatingItem.ExtensionID, estimatingItem.ResultID, estimatingItem2.PriceEach, this.drawArea.ActivePlan.UnitScale.CurrentSystemType);
					}
				}
			}
			this.estimatingEditor.RefreshAll();
			this.SetModified();
		}

		private void btEstimatingSaveDatabase_Click(object sender, EventArgs e)
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.SaveDatabase();
			this.UnlockInterface(ref flag);
		}

		private void btEstimatingImportPrices_Click(object sender, EventArgs e)
		{
			bool flag = false;
			this.LockInterface(ref flag);
			this.ImportPrices();
			this.UnlockInterface(ref flag);
		}

		private void btEstimatingMaterial_Click(object sender, EventArgs e)
		{
			this.dbEstimatingItemsEditor.Add(DBEstimatingItem.EstimatingItemType.MaterialItem);
		}

		private void btEstimatingLabour_Click(object sender, EventArgs e)
		{
			this.dbEstimatingItemsEditor.Add(DBEstimatingItem.EstimatingItemType.LaborItem);
		}

		private void btEstimatingEquipment_Click(object sender, EventArgs e)
		{
			this.dbEstimatingItemsEditor.Add(DBEstimatingItem.EstimatingItemType.EquipmentItem);
		}

		private void btEstimatingSubcontract_Click(object sender, EventArgs e)
		{
			this.dbEstimatingItemsEditor.Add(DBEstimatingItem.EstimatingItemType.SubcontractItem);
		}

		private void btEstimatingModify_Click(object sender, EventArgs e)
		{
			this.dbEstimatingItemsEditor.Modify();
		}

		private void btEstimatingDuplicate_Click(object sender, EventArgs e)
		{
			this.dbEstimatingItemsEditor.Duplicate();
		}

		private void btEstimatingDelete_Click(object sender, EventArgs e)
		{
			this.dbEstimatingItemsEditor.Delete();
		}

		private void btTemplateArea_Click(object sender, EventArgs e)
		{
			Color empty = Color.Empty;
			this.SelectAreaTool(null, empty, true, false);
		}

		private void btTemplatePerimeter_Click(object sender, EventArgs e)
		{
			Color empty = Color.Empty;
			this.SelectPerimeterTool(null, empty, true, false);
		}

		private void btTemplateLength_Click(object sender, EventArgs e)
		{
			Color empty = Color.Empty;
			this.SelectDistanceTool(null, empty, true, false);
		}

		private void btTemplateCounter_Click(object sender, EventArgs e)
		{
			Color empty = Color.Empty;
			this.SelectCounterTool(null, empty, true, false);
		}

		private void btTemplateModify_Click(object sender, EventArgs e)
		{
			Template currentTemplate = this.templatesLibraryEditor.GetCurrentTemplate();
			if (currentTemplate != null)
			{
				this.CreateToolFromTemplate(currentTemplate, true, false);
			}
		}

		private void btTemplateDuplicate_Click(object sender, EventArgs e)
		{
			Template currentTemplate = this.templatesLibraryEditor.GetCurrentTemplate();
			if (currentTemplate != null)
			{
				this.CreateToolFromTemplate(currentTemplate, true, true);
			}
		}

		private void btTemplateDelete_Click(object sender, EventArgs e)
		{
			this.templatesLibraryEditor.Delete();
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				this.ShowHelp();
				e.Handled = true;
				return;
			}
			try
			{
				MainForm.RibbonTabEnum ribbonTabEnum = this.currentTabIndex;
				if (ribbonTabEnum == MainForm.RibbonTabEnum.RibbonStart && !this.CellEditing)
				{
					this.mainControl_KeyDown(this, e);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void mainControl_KeyDown(object sender, KeyEventArgs e)
		{
			this.drawArea.OnKeyDown(sender, e);
			if (e.Handled)
			{
				return;
			}
			if (this.ImageEditing)
			{
				return;
			}
			Keys keyCode = e.KeyCode;
			if (keyCode <= Keys.A)
			{
				if (keyCode != Keys.Delete)
				{
					if (keyCode == Keys.A)
					{
						if (Control.ModifierKeys == Keys.None)
						{
							if (this.drawArea.SelectedPolyLine != null)
							{
								this.AutoAdjust(DrawPolyLine.AutoAdjustType.ToZone);
							}
							else
							{
								this.AutoAdjustNote();
							}
							e.Handled = true;
						}
					}
				}
				else if (this.mainControl.Bounds.Contains(base.PointToClient(Cursor.Position)))
				{
					this.DeleteSelectedObjects(true);
					e.Handled = true;
				}
			}
			else if (keyCode != Keys.D)
			{
				switch (keyCode)
				{
				case Keys.X:
					if (Control.ModifierKeys == Keys.None)
					{
						this.PointRemove(this.drawArea.MouseLocation);
						e.Handled = true;
					}
					break;
				case Keys.Z:
					if (Control.ModifierKeys == Keys.None)
					{
						this.ZoomToSelectedObject();
						e.Handled = true;
					}
					break;
				}
			}
			else if (Control.ModifierKeys == Keys.None)
			{
				if (this.drawArea.SelectedArea != null)
				{
					this.DeductionDuplicate();
				}
				else if (this.drawArea.SelectedPerimeter != null)
				{
					this.OpeningDuplicate(this.drawArea.MouseLocation);
				}
				e.Handled = true;
			}
			if (e.Handled)
			{
				return;
			}
			if (this.drawArea.CursorRestricted)
			{
				return;
			}
			Keys keyCode2 = e.KeyCode;
			if (keyCode2 <= Keys.Z)
			{
				if (keyCode2 == Keys.Escape)
				{
					this.SelectPointerTool();
					return;
				}
				switch (keyCode2)
				{
				case Keys.Left:
					if (Control.ModifierKeys == Keys.Shift)
					{
						this.AutoAdjust(DrawPolyLine.AutoAdjustType.CompressLeftRight);
						e.Handled = true;
						return;
					}
					if (Control.ModifierKeys == Keys.Control)
					{
						e.Handled = this.MoveSelected(-1, 0);
						return;
					}
					break;
				case Keys.Up:
					if (Control.ModifierKeys == Keys.Shift)
					{
						this.AutoAdjust(DrawPolyLine.AutoAdjustType.ExpandTopBottom);
						e.Handled = true;
						return;
					}
					if (Control.ModifierKeys == Keys.Control)
					{
						e.Handled = this.MoveSelected(0, -1);
						return;
					}
					break;
				case Keys.Right:
					if (Control.ModifierKeys == Keys.Shift)
					{
						this.AutoAdjust(DrawPolyLine.AutoAdjustType.ExpandLeftRight);
						e.Handled = true;
						return;
					}
					if (Control.ModifierKeys == Keys.Control)
					{
						e.Handled = this.MoveSelected(1, 0);
						return;
					}
					break;
				case Keys.Down:
					if (Control.ModifierKeys == Keys.Shift)
					{
						this.AutoAdjust(DrawPolyLine.AutoAdjustType.CompressTopBottom);
						e.Handled = true;
					}
					if (Control.ModifierKeys == Keys.Control)
					{
						e.Handled = this.MoveSelected(0, 1);
						return;
					}
					break;
				default:
					switch (keyCode2)
					{
					case Keys.A:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.SelectAll();
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.SelectObjectType("Area");
							e.Handled = true;
							return;
						}
						break;
					case Keys.B:
					case Keys.F:
					case Keys.Q:
					case Keys.T:
					case Keys.U:
					case Keys.W:
						break;
					case Keys.C:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.CopySelectedObjects();
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.SelectObjectType("Counter");
							e.Handled = true;
							return;
						}
						break;
					case Keys.D:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.UnselectAll();
							e.Handled = true;
							return;
						}
						break;
					case Keys.E:
						if (Control.ModifierKeys == Keys.None)
						{
							if (this.drawArea.SelectedPolyLine != null)
							{
								this.DeductionsEdit();
							}
							else
							{
								this.NoteEdit();
							}
							e.Handled = true;
							return;
						}
						break;
					case Keys.G:
						if (Control.ModifierKeys == Keys.None)
						{
							this.ZoomToSelectedGroup();
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Control)
						{
							this.GroupSelect();
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.GroupMoveToNew();
							e.Handled = true;
							return;
						}
						break;
					case Keys.H:
						if (Control.ModifierKeys == Keys.None)
						{
							this.SetHeight(this.drawArea.MouseLocation);
							e.Handled = true;
							return;
						}
						break;
					case Keys.I:
						if (Control.ModifierKeys == Keys.None)
						{
							this.PointInsert(this.drawArea.MouseLocation, true);
							e.Handled = true;
							return;
						}
						break;
					case Keys.J:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.BringObjectsToFront();
							e.Handled = true;
							return;
						}
						break;
					case Keys.K:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.SendObjectsToBack();
							e.Handled = true;
							return;
						}
						break;
					case Keys.L:
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.SelectObjectType("Line");
							e.Handled = true;
							return;
						}
						break;
					case Keys.M:
						if (Control.ModifierKeys == Keys.None)
						{
							this.ToggleMeasures();
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.SelectObjectType("Rectangle");
							e.Handled = true;
							return;
						}
						break;
					case Keys.N:
						if (Control.ModifierKeys == Keys.None)
						{
							this.GroupAddObject();
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.SelectObjectType("Angle");
							e.Handled = true;
							return;
						}
						break;
					case Keys.O:
						if (Control.ModifierKeys == Keys.None)
						{
							this.OpeningInsert(this.drawArea.MouseLocation);
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.SelectObjectType("Note");
							e.Handled = true;
							return;
						}
						break;
					case Keys.P:
						if (Control.ModifierKeys == Keys.None)
						{
							this.PerimeterCreateFromArea();
							e.Handled = true;
							return;
						}
						if (Control.ModifierKeys == Keys.Shift)
						{
							this.SelectObjectType("Perimeter");
							e.Handled = true;
							return;
						}
						break;
					case Keys.R:
						if (Control.ModifierKeys == Keys.None)
						{
							this.DropInsert(this.drawArea.MouseLocation, true);
							e.Handled = true;
							return;
						}
						break;
					case Keys.S:
						if (Control.ModifierKeys == Keys.None)
						{
							this.DeductionCreate();
							e.Handled = true;
							return;
						}
						break;
					case Keys.V:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.PasteSelectedObjects();
							e.Handled = true;
							return;
						}
						break;
					case Keys.X:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.CutSelectedObjects();
							e.Handled = true;
							return;
						}
						break;
					case Keys.Y:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.Redo();
							e.Handled = true;
							return;
						}
						break;
					case Keys.Z:
						if (Control.ModifierKeys == Keys.Control)
						{
							this.Undo();
							e.Handled = true;
							return;
						}
						break;
					default:
						return;
					}
					break;
				}
			}
			else
			{
				switch (keyCode2)
				{
				case Keys.Add:
					goto IL_272;
				case Keys.Separator:
					return;
				case Keys.Subtract:
					break;
				default:
					switch (keyCode2)
					{
					case Keys.Oemplus:
						goto IL_272;
					case Keys.Oemcomma:
						return;
					case Keys.OemMinus:
						break;
					default:
						switch (keyCode2)
						{
						case Keys.OemOpenBrackets:
							if (Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.None)
							{
								this.GroupBrowsePrevious(Control.ModifierKeys == Keys.None);
								e.Handled = true;
								return;
							}
							return;
						case Keys.OemPipe:
							return;
						case Keys.OemCloseBrackets:
							if (Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.None)
							{
								this.GroupBrowseNext(Control.ModifierKeys == Keys.None);
								e.Handled = true;
								return;
							}
							return;
						default:
							return;
						}
						break;
					}
					break;
				}
				if (Control.ModifierKeys == Keys.None)
				{
					this.ZoomOut();
					e.Handled = true;
					return;
				}
				if (Control.ModifierKeys == Keys.Shift)
				{
					this.AutoAdjust(DrawPolyLine.AutoAdjustType.Compress);
					e.Handled = true;
					return;
				}
				return;
				IL_272:
				if (Control.ModifierKeys == Keys.None)
				{
					this.ZoomIn();
					e.Handled = true;
					return;
				}
				if (Control.ModifierKeys == Keys.Shift)
				{
					this.AutoAdjust(DrawPolyLine.AutoAdjustType.Expand);
					e.Handled = true;
					return;
				}
			}
		}

		private void BuildSelectObjectTypeContextMenu(ButtonItem menuItem)
		{
			menuItem.SubItems.Clear();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.drawArea.ActiveDrawingObjects.Collection)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (!drawObject.IsDeduction() && !arrayList.Contains(drawObject.ObjectType))
				{
					arrayList.Add(drawObject.ObjectType);
					string alternateShortCutText = string.Empty;
					string objectType;
					switch (objectType = drawObject.ObjectType)
					{
					case "Angle":
						alternateShortCutText = "Shift+N";
						break;
					case "Line":
						alternateShortCutText = "Shift+L";
						break;
					case "Area":
						alternateShortCutText = "Shift+A";
						break;
					case "Perimeter":
						alternateShortCutText = "Shift+P";
						break;
					case "Counter":
						alternateShortCutText = "Shift+C";
						break;
					case "Rectangle":
						alternateShortCutText = "Shift+M";
						break;
					case "Note":
						alternateShortCutText = "Shift+O";
						break;
					}
					ButtonItem buttonItem = new ButtonItem("bSelectObjectType" + drawObject.ObjectType, drawObject.ObjectTypeDisplayName);
					buttonItem.Tag = drawObject.ObjectType;
					buttonItem.AlternateShortCutText = alternateShortCutText;
					buttonItem.Click += this.btSelectObjectType_Click;
					menuItem.SubItems.Add(buttonItem);
				}
			}
			menuItem.SubItems.Sort();
			arrayList.Clear();
			arrayList = null;
		}

		private void BuildSelectThisGroupContextMenu(ButtonItem menuItem)
		{
			menuItem.SubItems.Clear();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.drawArea.ActiveDrawingObjects.Collection)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.IsPartOfGroup() && !drawObject.IsDeduction() && !arrayList.Contains(drawObject.GroupID))
				{
					arrayList.Add(drawObject.GroupID);
					ButtonItem buttonItem = new ButtonItem("bSelectThisGroup" + drawObject.GroupID, drawObject.Name);
					buttonItem.Tag = drawObject.GroupID;
					buttonItem.Click += this.btSelectThisGroup_Click;
					menuItem.SubItems.Add(buttonItem);
				}
			}
			menuItem.SubItems.Sort();
			arrayList.Clear();
			arrayList = null;
		}

		private void BuildLayerMoveToContextMenu(ButtonItem menuItem)
		{
			menuItem.SubItems.Clear();
			for (int i = 0; i < this.drawArea.ActivePlan.Layers.Count; i++)
			{
				if (i != this.drawArea.ActiveLayerIndex)
				{
					Layer layer = this.drawArea.ActivePlan.Layers[i];
					ButtonItem buttonItem = new ButtonItem("bMoveToLayer" + i, layer.Name);
					buttonItem.Tag = i;
					buttonItem.Click += this.btLayerMoveTo_Click;
					menuItem.SubItems.Add(buttonItem);
				}
			}
		}

		private void BuildGroupMoveToContextMenu(ButtonItem menuItem)
		{
			if (this.drawArea.ActiveDrawingObjects.SelectionCount == 0)
			{
				return;
			}
			DrawObject selectedObject = this.drawArea.ActiveDrawingObjects.GetSelectedObject(0);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (drawObject.IsPartOfGroup() && !drawObject.IsDeduction() && !arrayList.Contains(drawObject.GroupID))
						{
							arrayList.Add(drawObject.GroupID);
						}
					}
				}
			}
			menuItem.SubItems.Clear();
			for (int i = 0; i < arrayList.Count; i++)
			{
				int num = (int)arrayList[i];
				if (num != selectedObject.GroupID)
				{
					DrawObject drawObject2 = this.drawArea.FindObjectFromGroupID(this.project, num);
					if (drawObject2 != null && drawObject2.ObjectType == selectedObject.ObjectType)
					{
						ButtonItem buttonItem = new ButtonItem("bGroupMoveTo" + i, drawObject2.Name);
						buttonItem.Tag = drawObject2;
						buttonItem.Click += this.btGroupMoveTo_Click;
						menuItem.SubItems.Add(buttonItem);
					}
				}
			}
			menuItem.SubItems.Sort();
			arrayList.Clear();
			arrayList = null;
		}

		private void BuildEditContextMenu(MouseEventArgs e)
		{
			this.bCut.Visible = !this.DeductionsEditing;
			this.bCopy.Visible = !this.DeductionsEditing;
			this.bPaste.Visible = !this.DeductionsEditing;
			this.bBringToFront.Visible = !this.DeductionsEditing;
			this.bSendToBack.Visible = !this.DeductionsEditing;
			this.bDeductionDuplicate.Visible = this.DeductionsEditing;
			this.bDelete.Visible = true;
			this.bAutoAdjustToZone.Visible = false;
			this.bGroupAddObject.Visible = false;
			this.bDeductionCreate.Visible = false;
			this.bDeductionsEdit.Visible = false;
			this.bPointInsert.Visible = false;
			this.bPointRemove.Visible = false;
			this.bDropInsert.Visible = false;
			this.bDropRemove.Visible = false;
			this.bPerimeterClose.Visible = false;
			this.bPerimeterOpen.Visible = false;
			this.bPerimeterCreateFromArea.Visible = false;
			this.bZoomToObject.Visible = false;
			this.bZoomToGroup.Visible = false;
			this.bSelectGroup.Visible = false;
			this.bGroupMoveTo.Visible = false;
			this.bGroupMoveToNew.Visible = false;
			this.bLayerMoveTo.Visible = false;
			this.bSelectAll.Visible = true;
			this.bOpeningCreateFromSegment.Visible = false;
			this.bOpeningCreateFromPosition.Visible = false;
			this.bOpeningDuplicate.Visible = false;
			this.bOpeningDelete.Visible = false;
			this.bAngleDegreeType.Visible = false;
			this.bAngleSlopeType.Visible = false;
			this.bEditNote.Visible = false;
			this.bSetHeight.Visible = false;
			this.bToggleMeasures.Visible = !this.DeductionsEditing;
			DrawObject drawObject = null;
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num2 = GroupUtilities.GroupsCount(this.drawArea.ActiveLayer);
			int num3 = this.drawArea.ObjectsCount(this.drawArea.ActiveLayerIndex);
			int selectionCount = this.drawArea.ActiveDrawingObjects.SelectionCount;
			this.bSelectAll.Enabled = (selectionCount < num3);
			this.contextMenuPoint = this.drawArea.BackTrackMouse(new Point(e.X, e.Y));
			this.drawArea.ToolSettings.SelectedSegment.Reset();
			switch (selectionCount)
			{
			case 0:
				this.bCut.Visible = false;
				this.bCopy.Visible = false;
				this.bPaste.Visible = true;
				this.bPaste.Enabled = (!this.Clipboard.IsEmpty && !this.DeductionsEditing);
				this.bDelete.Visible = false;
				this.bBringToFront.Visible = false;
				this.bSendToBack.Visible = false;
				break;
			case 1:
			{
				drawObject = this.drawArea.ActiveDrawingObjects.GetSelectedObject(0);
				this.bCut.Enabled = (!this.DeductionsEditing && drawObject.ObjectType != "Legend");
				this.bCopy.Enabled = (!this.DeductionsEditing && drawObject.ObjectType != "Legend");
				this.bPaste.Enabled = (!this.Clipboard.IsEmpty && !this.DeductionsEditing);
				this.bDelete.Enabled = (drawObject.ObjectType != "Legend");
				this.bDeductionDuplicate.Enabled = this.DeductionsEditing;
				this.bBringToFront.Enabled = (!this.DeductionsEditing && drawObject.ObjectType != "Legend");
				this.bSendToBack.Enabled = (!this.DeductionsEditing && drawObject.ObjectType != "Legend");
				this.bLayerMoveTo.Visible = (drawObject.ObjectType != "Legend");
				flag = (drawObject.GroupID != -1 && !this.DeductionsEditing);
				int num4 = GroupUtilities.GroupCount(this.drawArea.ActiveLayer, drawObject, true, "");
				num = GroupUtilities.GroupSelectedCount(this.drawArea, this.drawArea.ActiveLayer);
				int num5 = GroupUtilities.GroupNumberOfSameType(this.project, drawObject);
				this.bGroupAddObject.Visible = flag;
				flag2 = (this.drawArea.ActivePlan.Layers.Count > 1);
				this.bGroupMoveTo.Visible = flag;
				flag3 = (num5 > 1);
				this.bGroupMoveToNew.Visible = flag;
				this.bGroupMoveToNew.Enabled = (flag && num4 > 1 && selectionCount < num4);
				this.bSelectGroup.Visible = flag;
				this.bSelectGroup.Enabled = (flag && num4 > 1);
				this.bZoomToObject.BeginGroup = true;
				this.bZoomToObject.Visible = true;
				this.bZoomToGroup.BeginGroup = false;
				this.bZoomToGroup.Visible = flag;
				this.bZoomToGroup.Enabled = flag;
				this.bCut.Visible = (drawObject.ObjectType != "Legend");
				this.bPaste.Visible = (drawObject.ObjectType != "Legend");
				this.bDelete.Visible = (drawObject.ObjectType != "Legend");
				this.bBringToFront.Visible = (drawObject.ObjectType != "Legend");
				this.bSendToBack.Visible = (drawObject.ObjectType != "Legend");
				if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
				{
					DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
					int count = drawPolyLine.PointArray.Count;
					int num6 = this.drawArea.SegmentHitTest(drawPolyLine, this.contextMenuPoint);
					bool flag4 = num6 > 0;
					bool flag5 = num6 == -3;
					bool flag6 = num6 == -2;
					bool flag7 = num6 == -4;
					if (flag4 && ((drawObject.ObjectType == "Area" && count > 3) || (drawObject.ObjectType == "Perimeter" && count > 2)))
					{
						this.bPointRemove.Visible = true;
					}
					if (flag7)
					{
						this.bSetHeight.Text = Resources.Drop_length_;
					}
					else
					{
						this.bSetHeight.Text = Resources.Modifier_la_hauteur;
					}
					this.bPointInsert.Visible = flag6;
					this.bDropInsert.Visible = flag6;
					this.bDropRemove.Visible = flag7;
					this.bSetHeight.Visible = flag7;
					this.bAutoAdjustToZone.Visible = (drawPolyLine.CloseFigure && count > 2);
					if (drawObject.ObjectType == "Area")
					{
						if (drawPolyLine.DeductionParentID == -1)
						{
							this.bDeductionCreate.Visible = true;
							this.bDeductionsEdit.Visible = (drawPolyLine.DeductionArray.Count > 0);
							this.bPerimeterCreateFromArea.Visible = true;
							this.bPerimeterCreateFromArea.Text = Resources.Créer_un_périmètre_à_partir_de_cette_surface;
						}
						else
						{
							this.bLayerMoveTo.Visible = false;
							this.bGroupMoveTo.Visible = false;
							this.bGroupMoveToNew.Visible = false;
							this.bGroupAddObject.Visible = false;
							this.bSelectGroup.Visible = false;
							this.bSelectAll.Visible = false;
						}
					}
					else if (flag6 || flag5)
					{
						this.bOpeningCreateFromSegment.Visible = (!flag5 && count > 2);
						this.bOpeningCreateFromPosition.Visible = !flag5;
						this.bOpeningDuplicate.Visible = (!flag5 && this.drawArea.Clipboard.SelectedOpening != null);
						this.bOpeningDelete.Visible = flag5;
						this.bPerimeterOpen.Visible = (drawPolyLine.CloseFigure && flag6 && count > 2);
						this.bPerimeterClose.Visible = (!drawPolyLine.CloseFigure && count > 2);
						this.bSetHeight.Visible = flag5;
					}
				}
				else if (drawObject.ObjectType == "Angle")
				{
					this.bAngleDegreeType.Visible = true;
					this.bAngleSlopeType.Visible = true;
					this.bAngleDegreeType.Checked = (((DrawAngle)drawObject).AngleType == DrawAngle.AngleTypeEnum.AngleDegree);
					this.bAngleSlopeType.Checked = (((DrawAngle)drawObject).AngleType == DrawAngle.AngleTypeEnum.AngleSlope);
				}
				else if (drawObject.ObjectType == "Note")
				{
					this.bEditNote.Visible = true;
					this.bAutoAdjustToZone.BeginGroup = false;
					this.bAutoAdjustToZone.Visible = true;
				}
				break;
			}
			default:
			{
				this.bCut.Enabled = !this.DeductionsEditing;
				this.bCopy.Enabled = !this.DeductionsEditing;
				this.bPaste.Enabled = (!this.Clipboard.IsEmpty && !this.DeductionsEditing);
				this.bDelete.Enabled = true;
				this.bDeductionDuplicate.Enabled = false;
				this.bBringToFront.Enabled = !this.DeductionsEditing;
				this.bSendToBack.Enabled = !this.DeductionsEditing;
				this.bLayerMoveTo.Visible = !this.DeductionsEditing;
				drawObject = this.drawArea.ActiveDrawingObjects.GetSelectedObject(0);
				int num4 = GroupUtilities.GroupCount(this.drawArea.ActiveLayer, drawObject, true, "");
				num = GroupUtilities.GroupSelectedCount(this.drawArea, this.drawArea.ActiveLayer);
				int num5 = GroupUtilities.GroupNumberOfSameType(this.project, drawObject);
				bool flag8 = GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, drawObject);
				flag = (num > 1 && flag8 && !this.DeductionsEditing);
				this.bGroupAddObject.Visible = flag;
				flag2 = (this.drawArea.ActivePlan.Layers.Count > 1);
				this.bGroupMoveTo.Visible = flag;
				flag3 = (flag && num5 > 1);
				this.bGroupMoveToNew.Visible = flag;
				this.bGroupMoveToNew.Enabled = (flag && num4 > 1 && selectionCount < num4);
				this.bSelectGroup.Visible = flag;
				this.bSelectGroup.Enabled = (flag && num4 > 1 && selectionCount < num4);
				this.bZoomToGroup.BeginGroup = (flag8 && !this.DeductionsEditing);
				this.bZoomToGroup.Visible = flag;
				this.bZoomToGroup.Enabled = (flag8 && !this.DeductionsEditing);
				this.bPerimeterCreateFromArea.Visible = (this.drawArea.FirstSelectedArea != null && !this.DeductionsEditing);
				if (drawObject.ObjectType == "Area" && flag8)
				{
					this.bPerimeterCreateFromArea.Text = Resources.Créer_des_périmètres_à_partir_de_ce_groupe;
				}
				else if (this.drawArea.FirstSelectedArea != null)
				{
					this.bPerimeterCreateFromArea.Text = Resources.Créer_des_périmètres_à_partir_des_surfaces_sélectionnées;
				}
				break;
			}
			}
			string objectType;
			if (drawObject != null && (objectType = drawObject.ObjectType) != null)
			{
				if (!(objectType == "Line"))
				{
					if (!(objectType == "Area"))
					{
						if (!(objectType == "Perimeter"))
						{
							if (objectType == "Counter")
							{
								this.bGroupAddObject.Text = Resources.Ajouter_un_nouveau_compteur_au_groupe;
								this.bGroupAddObject.Image = Resources.counter_small;
							}
						}
						else
						{
							this.bGroupAddObject.Text = Resources.Ajouter_un_nouveau_périmètre_au_groupe;
							this.bGroupAddObject.Image = Resources.perimeter_small;
						}
					}
					else
					{
						this.bGroupAddObject.Text = Resources.Ajouter_une_nouvelle_surface_au_groupe;
						this.bGroupAddObject.Image = Resources.area_small;
					}
				}
				else
				{
					this.bGroupAddObject.Text = Resources.Ajouter_une_nouvelle_distance_au_groupe;
					this.bGroupAddObject.Image = Resources.distance_small;
				}
			}
			this.bSelectAll.Visible = !this.DeductionsEditing;
			this.bUnselectAll.Enabled = (selectionCount > 0 && !this.DeductionsEditing);
			this.bUnselectAll.Visible = !this.DeductionsEditing;
			bool flag9 = num2 > 0 && !this.DeductionsEditing;
			bool flag10 = flag9 && num2 > num;
			this.bSelectThisGroup.Enabled = flag10;
			this.bSelectThisGroup.Visible = flag9;
			if (flag10)
			{
				this.BuildSelectThisGroupContextMenu(this.bSelectThisGroup);
			}
			bool flag11 = num3 > 0 && !this.DeductionsEditing;
			this.bSelectObjectType.Enabled = flag11;
			this.bSelectObjectType.Visible = flag11;
			if (flag11)
			{
				this.BuildSelectObjectTypeContextMenu(this.bSelectObjectType);
			}
			this.bLayerMoveTo.Enabled = flag2;
			if (flag2)
			{
				this.BuildLayerMoveToContextMenu(this.bLayerMoveTo);
			}
			this.bGroupMoveTo.Enabled = flag3;
			if (flag3)
			{
				this.BuildGroupMoveToContextMenu(this.bGroupMoveTo);
			}
			this.bSelectGroup.BeginGroup = flag;
			this.bSelectThisGroup.BeginGroup = (!flag && flag9);
			this.bSelectObjectType.BeginGroup = (!flag && !flag9);
			this.bSelectAll.BeginGroup = (num3 == 0);
		}

		private void bEditPopup_PopupClose(object sender, EventArgs e)
		{
			this.bEditPopup.Visible = false;
		}

		public void EditContextMenu(MouseEventArgs e)
		{
			if (this.drawArea.ActiveTool != DrawingArea.DrawToolType.Pointer && this.drawArea.ActiveTool != DrawingArea.DrawToolType.PolyLine && this.drawArea.ActiveTool != DrawingArea.DrawToolType.Deduction && this.drawArea.ActiveTool != DrawingArea.DrawToolType.Line && this.drawArea.ActiveTool != DrawingArea.DrawToolType.Counter && this.drawArea.ActiveTool != DrawingArea.DrawToolType.Angle && this.drawArea.ActiveTool != DrawingArea.DrawToolType.Rectangle)
			{
				return;
			}
			if (this.ImageEditing)
			{
				return;
			}
			if (this.bEditPopup.Visible)
			{
				return;
			}
			this.BuildEditContextMenu(e);
			this.bEditPopup.Visible = true;
			this.bEditPopup.PopupMenu(Control.MousePosition);
		}

		private void Clipboard_ObjectPasted(DrawObject drawObject)
		{
			if (!this.GroupExistsInGUI(drawObject))
			{
				this.objectEditor.Add(drawObject, false);
				this.objectsNavigator.AddObject(this.drawArea.ActiveLayerIndex, drawObject, false);
			}
		}

		private void mainControl_Paint(PaintEventArgs e, PointF origin)
		{
			this.drawArea.DrawLayers(this.mainControl, e.Graphics, this.mainControl.DrawingBoard.ClientSize, (int)this.mainControl.Origin.X, (int)this.mainControl.Origin.Y, (float)this.mainControl.ZoomFactor, true, true, this.ImageQuality, 12f);
			if (!this.drawArea.NetSelectionInProgress)
			{
				this.updatePan = true;
				this.panControl.PanRectangle = new Rectangle((int)(this.mainControl.Origin.X * this.panControl.ZoomFactor), (int)(this.mainControl.Origin.Y * this.panControl.ZoomFactor), (int)((float)this.mainControl.DrawingBoard.ClientSize.Width * this.panControl.ZoomFactor / (float)this.mainControl.ZoomFactor), (int)((float)this.mainControl.DrawingBoard.ClientSize.Height * this.panControl.ZoomFactor / (float)this.mainControl.ZoomFactor));
				this.panControl.Refresh();
			}
		}

		private void mainControl_Resize(object sender, EventArgs e)
		{
			if (base.WindowState != FormWindowState.Minimized)
			{
				this.updatePan = true;
				this.panControl.PanRectangle = new Rectangle((int)(this.mainControl.Origin.X * this.panControl.ZoomFactor), (int)(this.mainControl.Origin.Y * this.panControl.ZoomFactor), (int)((float)this.mainControl.DrawingBoard.ClientSize.Width * this.panControl.ZoomFactor / (float)this.mainControl.ZoomFactor), (int)((float)this.mainControl.DrawingBoard.ClientSize.Height * this.panControl.ZoomFactor / (float)this.mainControl.ZoomFactor));
				this.panControl.Refresh();
			}
		}

		private void mainControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (!this.ImageEditing)
			{
				this.drawArea.OnMouseDown(sender, e);
			}
		}

		private void mainControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.mainControl.Focused && !this.mainControl.DrawingBoard.Focused && !this.ImageEditing)
			{
				if (this.CellEditing)
				{
					return;
				}
				this.mainControl.Focus();
			}
			this.drawArea.OnMouseMove(sender, e);
			if (((this.drawArea.DrawingInProgress && !this.drawArea.PanningInProgress) || this.drawArea.PointerInProgress || this.drawArea.PanningFromPanTool) && !this.offbound)
			{
				this.offbound = (((e.X < 50 || e.X > this.mainControl.DrawingBoard.ClientSize.Width - 50) && this.mainControl.HScrollBarEnabled) || ((e.Y < 50 || e.Y > this.mainControl.DrawingBoard.ClientSize.Height - 50) && this.mainControl.VScrollBarEnabled));
				if (this.offbound)
				{
					this.pendingPaint = false;
					this.timer1.Enabled = true;
					this.timer1.Interval = 10.0;
				}
			}
		}

		private void mainControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (!this.ImageEditing)
			{
				this.offbound = false;
				this.drawArea.OnMouseUp(sender, e);
			}
		}

		private void mainControl_OnOriginChange(PointF origin)
		{
			if (this.drawArea.ActivePlan != null)
			{
				this.drawArea.ActivePlan.DefaultBookmark.Origin = new Point((int)origin.X, (int)origin.Y);
			}
		}

		private void mainControl_OnZoomChange(int zoom)
		{
			this.lblZoom.Text = zoom.ToString() + "%";
			this.lblZoom.Refresh();
			this.exitNow = true;
			this.zoomSlider.Value = zoom;
			this.zoomSlider.Refresh();
			if (this.drawArea.ActivePlan != null)
			{
				this.drawArea.ActivePlan.DefaultBookmark.Zoom = zoom;
			}
			this.exitNow = false;
			this.UpdateStatusBar(this.applicationStatus);
		}

		private void mainControl_OnCacheLoaded(int zoom)
		{
			this.UpdateStatusBar(this.applicationStatus);
		}

		private void mainControl_OnMouseWheel(int zoom)
		{
		}

		private void panControl_Panning(Point offset)
		{
			try
			{
				PointF origin = new PointF((float)offset.X / this.panControl.ZoomFactor, (float)offset.Y / this.panControl.ZoomFactor);
				origin.X = ((origin.X < 0f) ? 0f : origin.X);
				origin.X = ((origin.X + (float)this.panControl.PanRectangle.Width / this.panControl.ZoomFactor > (float)this.mainControl.Image.Width) ? ((float)this.mainControl.Image.Width - (float)this.panControl.PanRectangle.Width / this.panControl.ZoomFactor) : origin.X);
				origin.Y = ((origin.Y < 0f) ? 0f : origin.Y);
				origin.Y = ((origin.Y + (float)this.panControl.PanRectangle.Height / this.panControl.ZoomFactor > (float)this.mainControl.Image.Height) ? ((float)this.mainControl.Image.Height - (float)this.panControl.PanRectangle.Height / this.panControl.ZoomFactor) : origin.Y);
				this.mainControl.Origin = origin;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void panControl_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				if (this.panControl.Enabled)
				{
					if (this.updatePan)
					{
						this.updatePan = false;
						this.drawArea.DrawLayers(this.panControl, e.Graphics, this.panControl.ClientSize, -(int)(this.panControl.Offset.X / this.panControl.ZoomFactor), -(int)(this.panControl.Offset.Y / this.panControl.ZoomFactor), this.panControl.ZoomFactor, false, true, this.ImageQuality, 12f);
						this.panControl.DrawPanRectangle(e.Graphics);
					}
				}
			}
			catch
			{
			}
		}

		private void panControl_Resize(object sender, EventArgs e)
		{
			try
			{
				if (base.WindowState != FormWindowState.Minimized)
				{
					this.updatePan = true;
					this.panControl.PanRectangle = new Rectangle((int)(this.mainControl.Origin.X * this.panControl.ZoomFactor), (int)(this.mainControl.Origin.Y * this.panControl.ZoomFactor), (int)((float)this.mainControl.DrawingBoard.ClientSize.Width * this.panControl.ZoomFactor / (float)this.mainControl.ZoomFactor), (int)((float)this.mainControl.DrawingBoard.ClientSize.Height * this.panControl.ZoomFactor / (float)this.mainControl.ZoomFactor));
					this.panControl.Refresh();
				}
			}
			catch
			{
			}
		}

		private void panControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.mainControl.Focused && !this.mainControl.DrawingBoard.Focused && !this.CellEditing)
			{
				this.mainControl.Focus();
			}
		}

		private void objectEditor_ObjectSelected(DrawObject drawObject)
		{
			this.ObjectSelected(this.objectEditor, drawObject);
		}

		private void objectEditor_ObjectChanged(DrawObject drawObject, string propertyName, object propertyValue)
		{
			this.ObjectChanged(this.objectEditor, drawObject, propertyName, propertyValue);
		}

		private void UpdateEstimatingList()
		{
			this.estimatingEditor.RefreshAll();
			this.SetModified();
		}

		private void objectEditor_OnPresetCreated(Preset preset)
		{
			this.UpdateEstimatingList();
		}

		private void objectEditor_OnPresetModified(Preset preset)
		{
			this.UpdateEstimatingList();
		}

		private void objectEditor_OnPresetDeleted(Preset preset)
		{
			this.UpdateEstimatingList();
		}

		private void objectEditor_OnProductCreated(CEstimatingItem estimatingItem)
		{
			this.UpdateEstimatingList();
		}

		private void objectEditor_OnProductModified(CEstimatingItem estimatingItem)
		{
			this.UpdateEstimatingList();
		}

		private void objectEditor_OnProductDeleted(CEstimatingItem estimatingItem)
		{
			this.UpdateEstimatingList();
		}

		private void objectEditor_OnDisplayCalculationsForAllPlans(DrawObject drawObject)
		{
			this.objectsNavigator.RefreshAll();
			this.SetModified();
		}

		private void objectsNavigator_OnNodeSelected(object selectedObject)
		{
			if (selectedObject == null)
			{
				this.btGroupLocate.Enabled = false;
				this.btZoomToObject.Enabled = false;
				this.btGroupSelect.Enabled = false;
				this.btGroupRemove.Enabled = false;
				this.btGroupRename.Enabled = false;
			}
			else if (selectedObject.GetType().Name == "Layer")
			{
				this.btGroupLocate.Enabled = false;
				this.btZoomToObject.Enabled = false;
				this.btGroupSelect.Enabled = false;
				this.btGroupRemove.Enabled = false;
				this.btGroupRename.Enabled = false;
			}
			else
			{
				this.btGroupLocate.Enabled = true;
				this.btZoomToObject.Enabled = true;
				this.btGroupSelect.Enabled = true;
				try
				{
					this.btGroupRemove.Enabled = (((DrawObject)selectedObject).ObjectType != "Legend");
				}
				catch
				{
					this.btGroupRemove.Enabled = false;
				}
				try
				{
					this.btGroupRename.Enabled = (((DrawObject)selectedObject).ObjectType != "Legend");
				}
				catch
				{
					this.btGroupRename.Enabled = false;
				}
			}
			this.btGroupsToggle.Enabled = (this.treeObjects.Nodes.Count > 0);
		}

		private void objectsNavigator_LayerChanged(int layerIndex, string propertyName, object propertyValue)
		{
			this.LayerChanged(this.objectsNavigator, layerIndex, propertyName, propertyValue);
		}

		private void objectsNavigator_ObjectSelected(DrawObject drawObject)
		{
			this.ObjectSelected(this.objectsNavigator, drawObject);
		}

		private void objectsNavigator_ObjectChanged(DrawObject drawObject, string propertyName, object propertyValue)
		{
			this.ObjectChanged(this.objectsNavigator, drawObject, propertyName, propertyValue);
		}

		private void objectsNavigator_PlanSelected(Plan plan)
		{
			if (plan == null)
			{
				return;
			}
			bool flag = false;
			this.LockInterface(ref flag);
			this.LoadPlan(plan, false);
			this.UnlockInterface(ref flag);
		}

		private void objectsNavigator_OnPlanRename(Plan plan, string newName)
		{
			plan.PrevName = plan.Name;
			plan.Name = newName;
			this.plansNavigator.Refresh();
			this.objectsNavigator.RefreshListOfPlans(this.drawArea.ActivePlan);
			this.recentPlansNavigator.Rename(plan, newName);
			this.reportsControl.ReportModule.RenamePlan(plan);
			this.SetModified();
		}

		private void objectsNavigator_OnGroupChangeLayer(DrawObject groupObject, int oldLayerIndex, int newLayerIndex)
		{
			this.MoveAllGroupToLayer(groupObject, oldLayerIndex, newLayerIndex);
		}

		private void LayerChanged(object controler, int layerIndex, string propertyName, object propertyValue)
		{
			string name;
			if ((name = controler.GetType().Name) != null)
			{
				if (!(name == "LayersNavigator"))
				{
					if (name == "DrawObjectsNavigator")
					{
						if (propertyName != null && !(propertyName == "Name"))
						{
							if (!(propertyName == "Visible"))
							{
								if (!(propertyName == "Opacity"))
								{
								}
							}
							else
							{
								this.layersNavigator.ToggleVisibility(layerIndex, Utilities.ConvertToBoolean(propertyValue, true));
							}
						}
					}
				}
				else if (propertyName != null)
				{
					if (!(propertyName == "Name"))
					{
						if (!(propertyName == "Visible"))
						{
							if (propertyName == "Opacity")
							{
								this.drawArea.ToolSettings.Opacity = Utilities.ConvertToInt(propertyValue);
							}
						}
					}
					else
					{
						string empty = string.Empty;
						string newName = propertyValue.ToString();
						this.objectsNavigator.RenameLayer(layerIndex, newName, ref empty);
						this.drawArea.RenameLayerFromUndoManager(empty, newName);
						Plan activePlan = this.drawArea.ActivePlan;
						Layer layer = this.drawArea.GetLayer(layerIndex);
						if (activePlan != null && layer != null)
						{
							this.reportsControl.ReportModule.RenameLayer(activePlan, layer);
						}
					}
				}
			}
			this.SetModified();
			this.Refresh();
		}

		private void layersNavigator_LayerSelected(int layerIndex)
		{
			this.btLayerRename.Enabled = (layerIndex > -1);
			this.btLayerRemove.Enabled = (layerIndex != 0);
			if (this.drawArea.ActivePlan != null)
			{
				this.drawArea.ActivePlan.DefaultBookmark.LayerIndex = layerIndex;
			}
			if (this.drawArea.ActivePlan != null)
			{
				this.btLayerMoveUp.Enabled = (layerIndex > 0 && layerIndex < this.drawArea.ActivePlan.Layers.Count - 1);
				this.btLayerMoveDown.Enabled = (layerIndex > 1 && this.drawArea.ActivePlan.Layers.Count > 2);
				this.btLayersToggle.Enabled = (this.drawArea.ActivePlan.Layers.Count > 0);
			}
			else
			{
				this.btLayerMoveUp.Enabled = false;
				this.btLayerMoveDown.Enabled = false;
				this.btLayersToggle.Enabled = false;
			}
			if (this.exitNow)
			{
				return;
			}
			this.EnableEditCommands(true);
			this.SelectPointerTool();
			this.Refresh();
		}

		private void layersNavigator_LayerChanged(int layerIndex, string propertyName, object propertyValue)
		{
			this.LayerChanged(this.layersNavigator, layerIndex, propertyName, propertyValue);
		}

		private void recentPlansNavigator_OnPlanRename(Plan plan, string newName)
		{
			plan.PrevName = plan.Name;
			plan.Name = newName;
			this.plansNavigator.Refresh();
			this.objectsNavigator.RefreshListOfPlans(this.drawArea.ActivePlan);
			this.reportsControl.ReportModule.RenamePlan(plan);
			this.SetModified();
		}

		private void plansNavigator_OnPlanSelected(Plan plan)
		{
			this.EnablePlanCommands(plan != null);
		}

		private void plansNavigator_OnPlanLoad(Plan plan)
		{
			if (plan == null)
			{
				return;
			}
			bool flag = false;
			this.LockInterface(ref flag);
			this.LoadPlan(plan, true);
			this.UnlockInterface(ref flag);
		}

		private void plansNavigator_OnPlanEdit(Plan plan)
		{
			this.PlanProperties();
		}

		private void plansNavigator_OnPlanRemove(Plan plan)
		{
			this.PlanRemove();
		}

		private void plansNavigator_OnPlanReordered(Plan plan)
		{
			this.objectsNavigator.RefreshListOfPlans(this.drawArea.ActivePlan);
			this.SetModified();
		}

		private void plansNavigator_OnPlanApplyAction(PlansNavigator.PlansActionEnum action, Plan plan, int index, int count)
		{
			this.PlanApplyAction(action, plan, index, count);
		}

		private void mruManager_OnRecentProjectSelected(string fileName)
		{
			this.FileOpen(fileName);
		}

		private void estimatingEditor_OnDisable(object sender, EventArgs e)
		{
			this.btEstimatingItemsExpandAll.Enabled = false;
			this.btEstimatingItemsCollapseAll.Enabled = false;
			this.btEstimatingItemsUpdatePrices.Enabled = false;
		}

		private void estimatingEditor_OnEnable(object sender, EventArgs e)
		{
			this.btEstimatingItemsExpandAll.Enabled = true;
			this.btEstimatingItemsCollapseAll.Enabled = true;
			this.btEstimatingItemsUpdatePrices.Enabled = true;
		}

		private void estimatingEditor_OnModified(object sender, EventArgs e)
		{
			this.SetModified();
		}

		private void dbEstimatingItemsEditor_OnSelected(object sender, EventArgs e)
		{
			DBEstimatingItemsEditor.NodeItemType selectedNodeType = this.dbEstimatingItemsEditor.GetSelectedNodeType();
			this.btEstimatingModify.Enabled = (selectedNodeType >= DBEstimatingItemsEditor.NodeItemType.EstimatingItem);
			this.btEstimatingDuplicate.Enabled = (selectedNodeType >= DBEstimatingItemsEditor.NodeItemType.EstimatingItem);
			this.btEstimatingDelete.Enabled = (selectedNodeType == DBEstimatingItemsEditor.NodeItemType.EstimatingItem);
		}

		private void dbEstimatingItemsEditor_OnDBItemCreated(DBEstimatingItem estimatingItem)
		{
			this.dbManagement.AddBrowserItem(estimatingItem);
			this.DatabaseWasModified(true);
		}

		private void SynchTemplatesWithDB()
		{
			foreach (object obj in this.templatesSupport.Templates.Collection)
			{
				Template template = (Template)obj;
				foreach (CEstimatingItem cestimatingItem in template.EstimatingItems.Collection)
				{
					DBEstimatingItem estimatingItem = this.dbManagement.GetEstimatingItem(Utilities.ConvertToInt(cestimatingItem.ItemID));
					if (estimatingItem != null && (cestimatingItem.Description != estimatingItem.Description || cestimatingItem.Unit != estimatingItem.PurchaseUnit || cestimatingItem.CoverageValue != estimatingItem.CoverageValue || cestimatingItem.CoverageUnit != estimatingItem.CoverageUnit || cestimatingItem.SectionID != estimatingItem.SectionID || cestimatingItem.SubSectionID != estimatingItem.SubSectionID || cestimatingItem.BidCode != estimatingItem.BidCode || cestimatingItem.UnitMeasure != estimatingItem.UnitMeasure))
					{
						cestimatingItem.Description = estimatingItem.Description;
						cestimatingItem.Unit = estimatingItem.PurchaseUnit;
						cestimatingItem.CoverageValue = estimatingItem.CoverageValue;
						cestimatingItem.CoverageUnit = estimatingItem.CoverageUnit;
						cestimatingItem.SectionID = estimatingItem.SectionID;
						cestimatingItem.SubSectionID = estimatingItem.SubSectionID;
						cestimatingItem.BidCode = estimatingItem.BidCode;
						cestimatingItem.UnitMeasure = estimatingItem.UnitMeasure;
						this.templatesSupport.SaveTemplate(template, template.FullFileName);
					}
				}
			}
		}

		private void dbEstimatingItemsEditor_OnDBItemModified(DBEstimatingItem estimatingItem, double oldPrice)
		{
			this.dbManagement.UpdateBrowserItem(estimatingItem);
			this.DatabaseWasModified(true);
			this.SynchTemplatesWithDB();
			if (!this.guiEnabled)
			{
				return;
			}
			DrawObject selectedObject = this.objectEditor.SelectedObject;
			bool flag = estimatingItem.PriceEach != oldPrice;
			bool flag2 = false;
			foreach (object obj in this.project.Groups.Collection)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				foreach (CEstimatingItem cestimatingItem in drawObjectGroup.EstimatingItems.Collection)
				{
					if (cestimatingItem.ItemID == estimatingItem.ID.ToString())
					{
						cestimatingItem.Description = estimatingItem.Description;
						cestimatingItem.Unit = estimatingItem.PurchaseUnit;
						cestimatingItem.CoverageValue = estimatingItem.CoverageValue;
						cestimatingItem.CoverageUnit = estimatingItem.CoverageUnit;
						cestimatingItem.SectionID = estimatingItem.SectionID;
						cestimatingItem.SubSectionID = estimatingItem.SubSectionID;
						cestimatingItem.BidCode = estimatingItem.BidCode;
						cestimatingItem.UnitMeasure = estimatingItem.UnitMeasure;
						if (selectedObject != null && selectedObject.GroupID == drawObjectGroup.ID)
						{
							this.objectEditor.Refresh();
						}
						this.SetModified();
					}
				}
				foreach (EstimatingItem estimatingItem2 in this.project.EstimatingItems.Collection)
				{
					if (estimatingItem2.ItemID == estimatingItem.ID)
					{
						estimatingItem2.ResultName = estimatingItem.Description;
						estimatingItem2.ResultUnit = estimatingItem.PurchaseUnit;
						if (flag)
						{
							flag = false;
							string le_prix_de_l_item_a_ete_modifie = Resources.Le_prix_de_l_item_a_ete_modifie;
							string voulez_vous_mettre_a_jour_votre_projet_courant_avec_le_nouveau_prix_ = Resources.Voulez_vous_mettre_a_jour_votre_projet_courant_avec_le_nouveau_prix_;
							flag2 = (Utilities.DisplayQuestion(le_prix_de_l_item_a_ete_modifie, voulez_vous_mettre_a_jour_votre_projet_courant_avec_le_nouveau_prix_) == DialogResult.Yes);
						}
						if (flag2)
						{
							this.project.EstimatingItems.SaveEstimatingItemCost(drawObjectGroup.ID, estimatingItem2.ExtensionID, estimatingItem2.ResultID, estimatingItem.PriceEach, this.drawArea.ActivePlan.UnitScale.CurrentSystemType);
						}
					}
				}
				this.treeEstimatingItems.Refresh();
			}
		}

		private void dbEstimatingItemsEditor_OnDBItemDeleted(DBEstimatingItem estimatingItem)
		{
			DrawObject selectedObject = this.objectEditor.SelectedObject;
			if (selectedObject != null)
			{
				foreach (object obj in this.project.Groups.Collection)
				{
					DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
					foreach (CEstimatingItem cestimatingItem in drawObjectGroup.EstimatingItems.Collection)
					{
						if (cestimatingItem.ItemID == estimatingItem.ID.ToString() && selectedObject.GroupID == drawObjectGroup.ID)
						{
							this.SetModified();
							break;
						}
					}
				}
			}
			this.dbManagement.DeleteBrowserItem(estimatingItem);
			this.DatabaseWasModified(true);
			bool flag = this.guiEnabled;
		}

		private void templatesLibraryEditor_OnCreateArea(object sender, EventArgs e)
		{
			this.btTemplateArea_Click(this, e);
		}

		private void templatesLibraryEditor_OnCreatePerimeter(object sender, EventArgs e)
		{
			this.btTemplatePerimeter_Click(this, e);
		}

		private void templatesLibraryEditor_OnCreateLength(object sender, EventArgs e)
		{
			this.btTemplateLength_Click(this, e);
		}

		private void templatesLibraryEditor_OnCreateCounter(object sender, EventArgs e)
		{
			this.btTemplateCounter_Click(this, e);
		}

		private void templatesLibraryEditor_OnSelected(object sender, EventArgs e)
		{
			TemplatesLibraryEditor.NodeItemType selectedNodeType = this.templatesLibraryEditor.GetSelectedNodeType();
			this.btTemplateModify.Enabled = (selectedNodeType == TemplatesLibraryEditor.NodeItemType.Item);
			this.btTemplateDuplicate.Enabled = (selectedNodeType == TemplatesLibraryEditor.NodeItemType.Item);
			this.btTemplateDelete.Enabled = (selectedNodeType == TemplatesLibraryEditor.NodeItemType.Item);
		}

		private void templatesLibraryEditor_OnModify(object sender, EventArgs e)
		{
			this.btTemplateModify_Click(sender, e);
		}

		private void templatesLibraryEditor_OnDuplicate(object sender, EventArgs e)
		{
			this.btTemplateDuplicate_Click(this, e);
		}

		private void reportsControl_OnSelectReport(object sender, EventArgs e)
		{
			this.btReportExportToExcel.Visible = (this.project.Report.SelectedReportType != ReportTypeEnum.QuoteReport);
			this.btReportExportToCOffice.Visible = ((this.coInterface.IsReady && this.project.Report.SelectedReportType == ReportTypeEnum.TakeoffByGroupsReport) || this.project.Report.SelectedReportType == ReportTypeEnum.TakeoffByPlansReport);
			this.ribbonExportReport.RecalcLayout();
		}

		private void reportsControl_OnCancelSettings(object sender, EventArgs e)
		{
			this.ResetEditReportSettings();
		}

		private void reportsControl_OnApplySettings(object sender, EventArgs e)
		{
			this.ResetEditReportSettings();
			this.SetModified();
		}

		private void ObjectSelected(object controler, DrawObject drawObject)
		{
			if (drawObject != null)
			{
				this.EnsureObjectVisible(drawObject, controler.GetType().Name == "DrawObjectsNavigator");
				this.ObjectSelect(drawObject, false, true);
			}
		}

		private void ObjectChanged(object controler, DrawObject drawObject, string propertyName, object propertyValue)
		{
			if (drawObject.GroupID == this.drawArea.ToolSettings.GroupID)
			{
				this.UpdateToolSettingsProperty(drawObject, propertyName, propertyValue);
			}
			if (propertyName == "Name")
			{
				this.estimatingEditor.Rename(drawObject);
			}
			if (propertyName == "SlopeFactor")
			{
				this.estimatingEditor.Refresh(drawObject);
			}
			string name;
			if ((name = controler.GetType().Name) != null)
			{
				if (!(name == "DrawObjectEditor"))
				{
					if (name == "DrawObjectsNavigator")
					{
						if (this.objectEditor.SelectedObject != null && drawObject.HasSameGroupOrID(this.objectEditor.SelectedObject))
						{
							this.objectEditor.Refresh();
						}
					}
				}
				else if (propertyName == "Name" || propertyName == "SlopeFactor" || propertyName == "Visible")
				{
					this.objectsNavigator.Refresh(drawObject);
				}
			}
			this.SetModified();
			this.Refresh();
		}

		private void UpdateSelectedObjects()
		{
			DrawingObjects activeDrawingObjects = this.drawArea.ActiveDrawingObjects;
			if (activeDrawingObjects == null)
			{
				return;
			}
			int selectionCount = activeDrawingObjects.SelectionCount;
			switch (selectionCount)
			{
			case 0:
				this.objectEditor.ForceSelection(null);
				break;
			case 1:
			{
				DrawObject selectedObject = activeDrawingObjects.GetSelectedObject(0);
				this.objectEditor.ForceSelection(selectedObject);
				break;
			}
			default:
			{
				DrawObject selectedObject = activeDrawingObjects.GetSelectedObject(0);
				this.objectEditor.ForceSelection((GroupUtilities.GroupSelectedCount(this.drawArea, this.drawArea.ActiveLayer) == selectionCount) ? selectedObject : null);
				break;
			}
			}
			this.EnableEditCommands(!this.DeductionsEditing);
		}

		private void drawArea_ObjectsSelected()
		{
			this.UpdateSelectedObjects();
		}

		private void drawArea_DeductionsParentSet()
		{
			this.EnableEditCommands(false);
			this.Refresh();
		}

		private void drawArea_DeductionsParentRelease()
		{
			this.EnableEditCommands(true);
			this.Refresh();
		}

		private void zoomSlider_ValueChanged(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			if (this.drawArea.Zoom != this.zoomSlider.Value)
			{
				this.ZoomSet(this.zoomSlider.Value);
				Application.DoEvents();
			}
		}

		private void zoomSlider_MouseUp(object sender, MouseEventArgs e)
		{
		}

		private void UpdateImageQuality()
		{
			if (this.ImageQuality != (MainForm.ImageQualityEnum)this.qualitySlider.Value)
			{
				this.SetImageQuality((MainForm.ImageQualityEnum)this.qualitySlider.Value);
			}
		}

		private void qualitySlider_ValueChanged(object sender, EventArgs e)
		{
			this.UpdateImageQuality();
		}

		private void sliderBrightness_ValueChanged(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			Application.DoEvents();
			this.sliderBrightness.Text = this.sliderBrightness.Value.ToString();
			this.mainControl.Brightness = this.sliderBrightness.Value;
			this.btBrightnessContrastApply.Enabled = (this.sliderBrightness.Value != this.drawArea.ActivePlan.Brightness || this.sliderContrast.Value != this.drawArea.ActivePlan.Contrast);
		}

		private void sliderContrast_ValueChanged(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			Application.DoEvents();
			this.sliderContrast.Text = this.sliderContrast.Value.ToString();
			this.mainControl.Contrast = this.sliderContrast.Value;
			this.btBrightnessContrastApply.Enabled = (this.sliderBrightness.Value != this.drawArea.ActivePlan.Brightness || this.sliderContrast.Value != this.drawArea.ActivePlan.Contrast);
		}

		private void timer1_Elapsed(object sender, ElapsedEventArgs e)
		{
			this.timer1.Enabled = false;
			if (!this.pendingPaint && this.offbound)
			{
				do
				{
					this.pendingPaint = true;
					bool flag = false;
					Point location = this.mainControl.DrawingBoard.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
					Point p = new Point((int)this.mainControl.Origin.X, (int)this.mainControl.Origin.Y);
					if (this.mainControl.HScrollBarEnabled)
					{
						if (location.X + 5 >= this.mainControl.DrawingBoard.ClientSize.Width)
						{
							p.X += this.mainControl.ScrollX() * (this.drawArea.PanningInProgress ? -1 : 1);
							flag = true;
						}
						if (location.X - 5 <= 0)
						{
							p.X -= this.mainControl.ScrollX() * (this.drawArea.PanningInProgress ? -1 : 1);
							flag = true;
						}
						if (p.X < 0)
						{
							p.X = 0;
						}
						if ((double)p.X + (double)this.mainControl.DrawingBoard.Width / this.mainControl.ZoomFactor > (double)this.mainControl.Image.Width)
						{
							p.X = (int)((double)this.mainControl.Image.Width - (double)this.mainControl.DrawingBoard.Width / this.mainControl.ZoomFactor);
						}
					}
					if (this.mainControl.VScrollBarEnabled)
					{
						if (location.Y + 5 >= this.mainControl.DrawingBoard.ClientSize.Height)
						{
							p.Y += this.mainControl.ScrollY() * (this.drawArea.PanningInProgress ? -1 : 1);
							flag = true;
						}
						if (location.Y - 5 <= 0)
						{
							p.Y -= this.mainControl.ScrollY() * (this.drawArea.PanningInProgress ? -1 : 1);
							flag = true;
						}
						if (p.Y < 0)
						{
							p.Y = 0;
						}
						if ((double)p.Y + (double)this.mainControl.DrawingBoard.Height / this.mainControl.ZoomFactor > (double)this.mainControl.Image.Height)
						{
							p.Y = (int)((double)this.mainControl.Image.Height - (double)this.mainControl.DrawingBoard.Height / this.mainControl.ZoomFactor);
						}
					}
					if (flag)
					{
						if (this.drawArea.SuspendScrolling)
						{
							this.drawArea.SuspendScrolling = false;
							flag = false;
							this.pendingPaint = false;
							this.offbound = false;
						}
						else if (this.drawArea.DeductionParent != null)
						{
							PointF tempOffset = new PointF(this.mainControl.Origin.X, this.mainControl.Origin.Y);
							this.mainControl.DrawingBoard.TempOffset = p;
							if (!this.drawArea.DeductionParent.IsDisplayed())
							{
								this.mainControl.DrawingBoard.TempOffset = tempOffset;
								flag = false;
								this.pendingPaint = false;
								this.offbound = false;
							}
						}
						if (flag)
						{
							this.mainControl.Origin = p;
							this.drawArea.TrackMouse(location);
							this.offbound = true;
						}
					}
					else
					{
						this.pendingPaint = false;
						this.offbound = false;
					}
					if (this.drawArea.DrawingInProgress || this.drawArea.PointerInProgress || this.drawArea.PanningInProgress || this.drawArea.NetSelectionInProgress)
					{
						if (((this.drawArea.DrawingInProgress || this.drawArea.PointerInProgress) && this.mainControl.CacheInUsed) || this.drawArea.PanningInProgress || this.drawArea.NetSelectionInProgress)
						{
							Application.DoEvents();
						}
						if (this.sliderScrollSpeed.Value > 0)
						{
							Thread.Sleep(this.sliderScrollSpeed.Value);
						}
					}
				}
				while (this.offbound);
			}
		}

		private void timer2_Elapsed(object sender, ElapsedEventArgs e)
		{
		}

		private Clipboard Clipboard
		{
			get
			{
				return this.drawArea.Clipboard;
			}
		}

		private bool CellEditing
		{
			get
			{
				return !this.mainControl.Focused && !this.mainControl.DrawingBoard.Focused && (this.lstLayers.IsCellEditing || this.treeObjects.IsCellEditing || this.objectEditor.IsEditing || this.lstRecentPlans.IsCellEditing || this.objectsNavigator.EditingPlanName);
			}
		}

		private bool DeductionsEditing
		{
			get
			{
				return this.drawArea.DeductionParent != null;
			}
		}

		private void UpdateToolSettingsProperty(DrawObject drawObject, string propertyName, object propertyValue)
		{
			switch (propertyName)
			{
			case "Name":
				this.drawArea.ToolSettings.Name = propertyValue.ToString();
				return;
			case "Comment":
				this.drawArea.ToolSettings.Comment = propertyValue.ToString();
				return;
			case "Text":
				this.drawArea.ToolSettings.Text = propertyValue.ToString();
				this.drawArea.UpdateCursor(drawObject);
				return;
			case "Color":
				this.drawArea.ToolSettings.LineColor = (Color)propertyValue;
				this.drawArea.ToolSettings.FillColor = (Color)propertyValue;
				this.drawArea.UpdateCursor(drawObject);
				return;
			case "Pattern":
				this.drawArea.ToolSettings.Pattern = (HatchStylePickerCombo.HatchStylePickerEnum)propertyValue;
				this.drawArea.UpdateCursor(drawObject);
				return;
			case "Shape":
				this.drawArea.ToolSettings.Shape = (DrawCounter.CounterShapeTypeEnum)propertyValue;
				this.drawArea.UpdateCursor(drawObject);
				return;
			case "PenWidth":
				this.drawArea.ToolSettings.LineWidth = (int)propertyValue;
				return;
			case "CounterSize":
				this.drawArea.ToolSettings.CounterSize = (int)propertyValue;
				return;
			case "SlopeFactor":
				this.drawArea.ToolSettings.SlopeFactor.SetValues(drawObject.SlopeFactor);
				return;
			case "ShowMeasure":
				this.drawArea.ToolSettings.ShowMeasure = (bool)propertyValue;
				break;

				return;
			}
		}

		private void SelectObjectThroughLayersFromID(int objectID)
		{
			int num = 0;
			foreach (object obj in this.drawArea.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj2;
					if (drawObject.ID == objectID)
					{
						this.layersNavigator.ForceSelection(num);
						drawObject.Selected = true;
						return;
					}
				}
				num++;
			}
		}

		private void SetImageQuality(MainForm.ImageQualityEnum quality)
		{
			switch (quality)
			{
			case MainForm.ImageQualityEnum.QualityLow:
				this.lblImageQuality.Text = Resources.Qualité_Basse;
				this.mainControl.SetImageQuality(InterpolationModes.ZoomedImagesEnum.MoreThan50Percent, InterpolationMode.NearestNeighbor);
				this.mainControl.SetImageQuality(InterpolationModes.ZoomedImagesEnum.Between25and50Percents, InterpolationMode.NearestNeighbor);
				this.mainControl.SetImageQuality(InterpolationModes.ZoomedImagesEnum.Below25Percent, InterpolationMode.NearestNeighbor);
				break;
			case MainForm.ImageQualityEnum.QualityHigh:
				this.lblImageQuality.Text = Resources.Qualité_Haute;
				this.mainControl.SetImageQuality(InterpolationModes.ZoomedImagesEnum.MoreThan50Percent, InterpolationMode.HighQualityBicubic);
				this.mainControl.SetImageQuality(InterpolationModes.ZoomedImagesEnum.Between25and50Percents, InterpolationMode.HighQualityBicubic);
				this.mainControl.SetImageQuality(InterpolationModes.ZoomedImagesEnum.Below25Percent, InterpolationMode.HighQualityBicubic);
				break;
			}
			this.mainControl.ReloadAdaptiveZoomedImage();
			this.ImageQuality = quality;
		}

		private bool CanSendData()
		{
			if (this.drawArea.SelectionCount == 0)
			{
				return false;
			}
			if (this.drawArea.SelectionCount == 1)
			{
				return this.drawArea.SelectedObject.IsPartOfGroup();
			}
			return GroupUtilities.SelectedObjectsHaveSameGroupID(this.drawArea.ActiveLayer, this.drawArea.SelectedObject);
		}

		private bool CanBrowseObject(bool byObjectType)
		{
			bool result = false;
			DrawObject selectedObject = this.drawArea.SelectedObject;
			int selectionCount = this.drawArea.SelectionCount;
			if (selectionCount == 1 && !this.DeductionsEditing && selectedObject != null && !selectedObject.IsDeduction())
			{
				if (byObjectType)
				{
					result = (GroupUtilities.ObjectsOfThisTypeCount(this.drawArea.ActiveLayer, selectedObject.ObjectType) > 1);
				}
				else
				{
					result = (selectedObject.IsPartOfGroup() && GroupUtilities.GroupCount(this.drawArea.ActiveLayer, selectedObject, true, "") > 1);
				}
			}
			return result;
		}

		private void DrawAreaIcon(DrawObject drawObject, Graphics g)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddLine(new Point(13, 25), new Point(25, 25));
			graphicsPath.AddLine(new Point(25, 25), new Point(25, 12));
			graphicsPath.AddLine(new Point(25, 12), new Point(53, 12));
			graphicsPath.AddLine(new Point(53, 12), new Point(53, 25));
			graphicsPath.AddLine(new Point(53, 25), new Point(64, 25));
			graphicsPath.AddLine(new Point(64, 25), new Point(64, 36));
			graphicsPath.AddLine(new Point(64, 36), new Point(76, 36));
			graphicsPath.AddLine(new Point(76, 36), new Point(76, 56));
			graphicsPath.AddLine(new Point(76, 56), new Point(13, 56));
			graphicsPath.AddLine(new Point(13, 56), new Point(13, 25));
			graphicsPath.CloseFigure();
			DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
			if (drawPolyLine.Pattern == HatchStylePickerCombo.HatchStylePickerEnum.Solid)
			{
				g.FillPath(new SolidBrush(Color.FromArgb(170, drawPolyLine.FillColor)), graphicsPath);
			}
			else
			{
				g.FillPath(new HatchBrush((HatchStyle)drawPolyLine.Pattern, Color.FromArgb(170, drawPolyLine.FillColor), Color.FromArgb(170, Color.White)), graphicsPath);
			}
			g.DrawPath(new Pen(Color.FromArgb(200, drawObject.Color), 2.25f), graphicsPath);
			graphicsPath.Dispose();
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.EllipsisPath;
				stringFormat.LineAlignment = StringAlignment.Near;
				g.DrawString(drawObject.Name, new Font("Tahoma", Utilities.FontSizeInPoints(11f), FontStyle.Regular), Brushes.Black, new RectangleF(2f, 62f, 86f, 27f), stringFormat);
			}
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		private void DrawAreaIcon(DrawObject drawObject, ref Image image)
		{
			using (Graphics graphics = Graphics.FromImage(image))
			{
				this.DrawAreaIcon(drawObject, graphics);
			}
		}

		private void DrawPerimeterIcon(DrawObject drawObject, Graphics g)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			float num = (float)drawObject.PenWidth / 2f;
			num = ((num < 1f) ? 1f : num);
			GraphicsPath graphicsPath = new GraphicsPath();
			Pen pen = new Pen(Color.FromArgb(200, drawObject.Color), num);
			pen.StartCap = LineCap.Square;
			graphicsPath.AddLine(new Point(16, 7), new Point(16, 53));
			graphicsPath.AddLine(new Point(16, 53), new Point(74, 53));
			graphicsPath.AddLine(new Point(74, 53), new Point(74, 27));
			graphicsPath.AddLine(new Point(74, 27), new Point(48, 27));
			g.DrawPath(pen, graphicsPath);
			pen.Dispose();
			graphicsPath.Dispose();
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(12, 3), new Size(8, 8)));
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(12, 49), new Size(8, 8)));
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(44, 23), new Size(8, 8)));
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(70, 23), new Size(8, 8)));
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(70, 49), new Size(8, 8)));
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.EllipsisPath;
				stringFormat.LineAlignment = StringAlignment.Near;
				g.DrawString(drawObject.Name, new Font("Tahoma", Utilities.FontSizeInPoints(11f), FontStyle.Regular), Brushes.Black, new RectangleF(2f, 62f, 86f, 27f), stringFormat);
			}
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		private void DrawPerimeterIcon(DrawObject drawObject, ref Image image)
		{
			using (Graphics graphics = Graphics.FromImage(image))
			{
				this.DrawPerimeterIcon(drawObject, graphics);
			}
		}

		private void DrawCounterIcon(DrawObject drawObject, Graphics g)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = (float)((DrawCounter)drawObject).DefaultSize / 2f;
			int num2 = (int)num;
			float fontSizeInPixels = num * 0.25f;
			Point p = new Point(43, 37);
			DrawCounter.CounterShapeTypeEnum shape = ((DrawCounter)drawObject).Shape;
			Rectangle rect = new Rectangle(p.X - num2 / 2, p.Y - num2 / 2, num2, num2);
			switch (shape)
			{
			case DrawCounter.CounterShapeTypeEnum.CounterShapeSquare:
				graphicsPath.AddRectangle(rect);
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeDiamond:
				graphicsPath.AddPolygon(DrawCounter.GetDiamondPoints(rect));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangle:
				graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rect, DrawCounter.Direction.Up));
				p = new Point(p.X, p.Y + (int)((float)rect.Height / 6f));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTriangleReversed:
				graphicsPath.AddPolygon(DrawCounter.GetTrianglePoints(rect, DrawCounter.Direction.Down));
				p = new Point(p.X, p.Y - (int)((float)rect.Height / 6f));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapeze:
				graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rect, DrawCounter.Direction.Up));
				break;
			case DrawCounter.CounterShapeTypeEnum.CounterShapeTrapezeReversed:
				graphicsPath.AddPolygon(DrawCounter.GetTapezePoints(rect, DrawCounter.Direction.Down));
				break;
			default:
				graphicsPath.AddEllipse(rect);
				break;
			}
			g.FillPath(new SolidBrush(Color.FromArgb(170, drawObject.FillColor)), graphicsPath);
			g.DrawPath(new Pen(Color.FromArgb(200, Color.Black), 1.75f), graphicsPath);
			graphicsPath.Dispose();
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.Character;
				stringFormat.LineAlignment = StringAlignment.Center;
				g.DrawString(drawObject.Text, new Font("Tahoma", Utilities.FontSizeInPoints(fontSizeInPixels), FontStyle.Bold), Brushes.Black, p, stringFormat);
				stringFormat.Trimming = StringTrimming.EllipsisPath;
				stringFormat.LineAlignment = StringAlignment.Near;
				g.DrawString(drawObject.Name, new Font("Tahoma", Utilities.FontSizeInPoints(11f), FontStyle.Regular), Brushes.Black, new RectangleF(2f, 62f, 86f, 27f), stringFormat);
			}
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		private void DrawCounterIcon(DrawObject drawObject, ref Image image)
		{
			using (Graphics graphics = Graphics.FromImage(image))
			{
				this.DrawCounterIcon(drawObject, graphics);
			}
		}

		private void DrawDistanceIcon(DrawObject drawObject, Graphics g)
		{
			SmoothingMode smoothingMode = g.SmoothingMode;
			TextRenderingHint textRenderingHint = g.TextRenderingHint;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			float num = (float)drawObject.PenWidth / 2f;
			num = ((num < 1f) ? 1f : num);
			GraphicsPath graphicsPath = new GraphicsPath();
			Pen pen = new Pen(Color.FromArgb(200, drawObject.Color), num);
			graphicsPath.AddLine(new Point(16, 33), new Point(74, 33));
			g.DrawPath(pen, graphicsPath);
			pen.Dispose();
			graphicsPath.Dispose();
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(12, 29), new Size(8, 8)));
			g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(70, 29), new Size(8, 8)));
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.Trimming = StringTrimming.EllipsisPath;
				stringFormat.LineAlignment = StringAlignment.Near;
				g.DrawString(drawObject.Name, new Font("Tahoma", Utilities.FontSizeInPoints(11f), FontStyle.Regular), Brushes.Black, new RectangleF(2f, 62f, 86f, 27f), stringFormat);
			}
			g.TextRenderingHint = textRenderingHint;
			g.SmoothingMode = smoothingMode;
		}

		private void DrawDistanceIcon(DrawObject drawObject, ref Image image)
		{
			using (Graphics graphics = Graphics.FromImage(image))
			{
				this.DrawDistanceIcon(drawObject, graphics);
			}
		}

		public void DrawObjectTypeImage(DrawObject drawObject, Graphics g)
		{
			string objectType;
			if ((objectType = drawObject.ObjectType) != null)
			{
				if (objectType == "Area")
				{
					this.DrawAreaIcon(drawObject, g);
					return;
				}
				if (objectType == "Perimeter")
				{
					this.DrawPerimeterIcon(drawObject, g);
					return;
				}
				if (objectType == "Counter")
				{
					this.DrawCounterIcon(drawObject, g);
					return;
				}
				if (!(objectType == "Line"))
				{
					return;
				}
				this.DrawDistanceIcon(drawObject, g);
			}
		}

		private void DrawObjectTypeImage(DrawObject drawObject, ref Image image)
		{
			string objectType;
			if ((objectType = drawObject.ObjectType) != null)
			{
				if (objectType == "Area")
				{
					this.DrawAreaIcon(drawObject, ref image);
					return;
				}
				if (objectType == "Perimeter")
				{
					this.DrawPerimeterIcon(drawObject, ref image);
					return;
				}
				if (objectType == "Counter")
				{
					this.DrawCounterIcon(drawObject, ref image);
					return;
				}
				if (!(objectType == "Line"))
				{
					return;
				}
				this.DrawDistanceIcon(drawObject, ref image);
			}
		}

		private Image CreateObjectTypeImage(DrawObject groupObject)
		{
			Image result = new Bitmap(90, 90);
			this.DrawObjectTypeImage(groupObject, ref result);
			return result;
		}

		private void ToolPresetApplyFilter(GalleryContainer gallery, string filter)
		{
			foreach (object obj in gallery.SubItems)
			{
				ButtonItem buttonItem = (ButtonItem)obj;
				try
				{
					bool visible;
					if (filter == string.Empty)
					{
						visible = true;
					}
					else if (gallery.Name.ToString().IndexOf("Templates") > 0)
					{
						Template template = (Template)buttonItem.Tag;
						DrawObject drawObject = template.DrawObject;
						visible = Regex.IsMatch(drawObject.Name.ToLower(), filter.ToLower(), RegexOptions.Singleline);
					}
					else
					{
						DrawObject drawObject2 = (DrawObject)buttonItem.Tag;
						visible = Regex.IsMatch(drawObject2.Name.ToLower(), filter.ToLower(), RegexOptions.Singleline);
					}
					buttonItem.Visible = visible;
				}
				catch
				{
				}
			}
			gallery.Refresh();
		}

		private void ToolPresetClearSubItems(GalleryContainer gallery)
		{
			if (gallery.SubItems.Count > 0)
			{
				for (int i = gallery.SubItems.Count - 1; i >= 0; i--)
				{
					ButtonItem buttonItem = null;
					try
					{
						buttonItem = (ButtonItem)gallery.SubItems[i];
					}
					catch
					{
					}
					if (buttonItem != null && buttonItem.Image != null)
					{
						buttonItem.Image.Dispose();
					}
					gallery.SubItems.RemoveAt(i);
				}
			}
		}

		private void ToolPresetAddItems(GalleryContainer gallery, ArrayList objects, int minWidth)
		{
			foreach (object obj in objects)
			{
				DrawObject drawObject;
				if (gallery.Name.ToString().IndexOf("Templates") > 0)
				{
					drawObject = ((Template)obj).DrawObject;
				}
				else
				{
					drawObject = (DrawObject)obj;
				}
				if (drawObject != null)
				{
					ButtonItem buttonItem = (gallery.Name.ToString() == "galleryAreaTemplates") ? new ButtonItem() : new ButtonItem("bt" + drawObject.ObjectType + "Group" + drawObject.ID.ToString());
					buttonItem.ButtonStyle = eButtonStyle.Default;
					buttonItem.Image = this.CreateObjectTypeImage(drawObject);
					buttonItem.CanCustomize = false;
					buttonItem.Tag = obj;
					buttonItem.Click += ((gallery.Name.ToString().IndexOf("Templates") > 0) ? new EventHandler(this.selectToolFromTemplate_Click) : new EventHandler(this.groupAddTo_Click));
					gallery.SubItems.Add(buttonItem);
				}
			}
			int num = (int)Math.Ceiling((double)objects.Count / 4.0);
			num = ((num > 2) ? 2 : num);
			int width = 412;
			if (num == 1)
			{
				if (objects.Count < 3)
				{
					width = ((minWidth > 214) ? minWidth : 214);
				}
				else if (objects.Count == 3)
				{
					width = 313;
				}
			}
			int height = num * 96 + num;
			gallery.DefaultSize = new Size(width, height);
			gallery.RecalcSize();
		}

		private void ToolPresetEnableSubMenu(string objectType, ArrayList groups, ArrayList templates)
		{
			bool flag = groups.Count > 0;
			bool flag2 = templates.Count > 0;
			groups.Sort(new MainForm.GroupSorter());
			templates.Sort(new MainForm.TemplateSorter());
			if (objectType != null)
			{
				LabelItem labelItem;
				LabelItem labelItem2;
				TextBoxItem textBoxItem;
				ButtonItem buttonItem;
				ItemContainer itemContainer;
				LabelItem labelItem3;
				GalleryContainer galleryContainer;
				LabelItem labelItem4;
				GalleryContainer galleryContainer2;
				if (!(objectType == "Area"))
				{
					if (!(objectType == "Perimeter"))
					{
						if (!(objectType == "Counter"))
						{
							if (!(objectType == "Line"))
							{
								return;
							}
							labelItem = this.lblNoDistance;
							labelItem2 = this.lblDistanceFilter;
							textBoxItem = this.txtDistanceFilter;
							buttonItem = this.btDistanceFilterClear;
							itemContainer = this.itemContainerDistanceFilter;
							labelItem3 = this.lblDistanceGroups;
							galleryContainer = this.galleryDistanceGroups;
							labelItem4 = this.lblDistanceTemplates;
							galleryContainer2 = this.galleryDistanceTemplates;
						}
						else
						{
							labelItem = this.lblNoCounter;
							labelItem2 = this.lblCounterFilter;
							textBoxItem = this.txtCounterFilter;
							buttonItem = this.btCounterFilterClear;
							itemContainer = this.itemContainerCounterFilter;
							labelItem3 = this.lblCounterGroups;
							galleryContainer = this.galleryCounterGroups;
							labelItem4 = this.lblCounterTemplates;
							galleryContainer2 = this.galleryCounterTemplates;
						}
					}
					else
					{
						labelItem = this.lblNoPerimeter;
						labelItem2 = this.lblPerimeterFilter;
						textBoxItem = this.txtPerimeterFilter;
						buttonItem = this.btPerimeterFilterClear;
						itemContainer = this.itemContainerPerimeterFilter;
						labelItem3 = this.lblPerimeterGroups;
						galleryContainer = this.galleryPerimeterGroups;
						labelItem4 = this.lblPerimeterTemplates;
						galleryContainer2 = this.galleryPerimeterTemplates;
					}
				}
				else
				{
					labelItem = this.lblNoArea;
					labelItem2 = this.lblAreaFilter;
					textBoxItem = this.txtAreaFilter;
					buttonItem = this.btAreaFilterClear;
					itemContainer = this.itemContainerAreaFilter;
					labelItem3 = this.lblAreaGroups;
					galleryContainer = this.galleryAreaGroups;
					labelItem4 = this.lblAreaTemplates;
					galleryContainer2 = this.galleryAreaTemplates;
				}
				labelItem.Visible = (!flag && !flag2);
				labelItem2.Visible = (flag || flag2);
				textBoxItem.Visible = (flag || flag2);
				buttonItem.Visible = (flag || flag2);
				itemContainer.Visible = (flag || flag2);
				labelItem3.Visible = flag;
				galleryContainer.Visible = flag;
				labelItem4.Visible = flag2;
				galleryContainer2.Visible = flag2;
				this.ToolPresetClearSubItems(galleryContainer);
				this.ToolPresetClearSubItems(galleryContainer2);
				if (flag)
				{
					this.ToolPresetAddItems(galleryContainer, groups, Utilities.MeasureTextWidth(labelItem3.Text.Replace("<b>", "").Replace("</b>", "").Replace("<br/", "\n"), labelItem3.Font) + 50);
					this.ToolPresetApplyFilter(galleryContainer, textBoxItem.Text);
				}
				if (flag2)
				{
					this.ToolPresetAddItems(galleryContainer2, templates, Utilities.MeasureTextWidth(labelItem4.Text.Replace("<b>", "").Replace("</b>", "").Replace("<br/", "\n"), labelItem3.Font) + 50);
					this.ToolPresetApplyFilter(galleryContainer2, textBoxItem.Text);
				}
				int num = (galleryContainer.SubItems.Count > galleryContainer2.SubItems.Count) ? galleryContainer.SubItems.Count : galleryContainer2.SubItems.Count;
				textBoxItem.TextBoxWidth = ((num > 3) ? 361 : 262);
				return;
			}
		}

		private void BuildToolPresetMenu(ButtonItem menuItem, string objectType)
		{
			this.drawArea.FlagDeletedGroups();
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			GroupUtilities.GroupsCountByType(this.project, objectType, ref arrayList);
			this.templatesSupport.GetTemplatesNotInUse(this.project, objectType, ref arrayList2);
			this.ToolPresetEnableSubMenu(objectType, arrayList, arrayList2);
			arrayList2.Clear();
			arrayList2 = null;
			arrayList.Clear();
			arrayList = null;
		}

		private void UpdateFilter(string txtFilterName, string filterText)
		{
			if (txtFilterName != null)
			{
				GalleryContainer gallery;
				GalleryContainer gallery2;
				if (!(txtFilterName == "txtAreaFilter"))
				{
					if (!(txtFilterName == "txtPerimeterFilter"))
					{
						if (!(txtFilterName == "txtCounterFilter"))
						{
							if (!(txtFilterName == "txtDistanceFilter"))
							{
								return;
							}
							gallery = this.galleryDistanceGroups;
							gallery2 = this.galleryDistanceTemplates;
						}
						else
						{
							gallery = this.galleryCounterGroups;
							gallery2 = this.galleryCounterTemplates;
						}
					}
					else
					{
						gallery = this.galleryPerimeterGroups;
						gallery2 = this.galleryPerimeterTemplates;
					}
				}
				else
				{
					gallery = this.galleryAreaGroups;
					gallery2 = this.galleryAreaTemplates;
				}
				this.ToolPresetApplyFilter(gallery, filterText);
				this.ToolPresetApplyFilter(gallery2, filterText);
				return;
			}
		}

		private void btFilterClear_Click(object sender, EventArgs e)
		{
			string name;
			if ((name = ((ButtonItem)sender).Name) != null)
			{
				if (name == "btAreaFilterClear")
				{
					this.txtAreaFilter.Text = "";
					this.UpdateFilter("txtAreaFilter", this.txtAreaFilter.Text);
					return;
				}
				if (name == "btPerimeterFilterClear")
				{
					this.txtPerimeterFilter.Text = "";
					this.UpdateFilter("txtPerimeterFilter", this.txtPerimeterFilter.Text);
					return;
				}
				if (name == "btCounterFilterClear")
				{
					this.txtCounterFilter.Text = "";
					this.UpdateFilter("txtCounterFilter", this.txtCounterFilter.Text);
					return;
				}
				if (!(name == "btDistanceFilterClear"))
				{
					return;
				}
				this.txtDistanceFilter.Text = "";
				this.UpdateFilter("txtDistanceFilter", this.txtDistanceFilter.Text);
			}
		}

		private void txtFilter_MouseMove(object sender, MouseEventArgs e)
		{
			((TextBoxItem)sender).Focus();
			((TextBoxItem)sender).SelectionStart = ((TextBoxItem)sender).Text.Length;
		}

		private void txtFilter_KeyUp(object sender, KeyEventArgs e)
		{
			this.UpdateFilter(((TextBoxItem)sender).Name, ((TextBoxItem)sender).Text);
		}

		private void txtFilter_GotFocus(object sender, EventArgs e)
		{
			this.helpContextString = "txtGroupsFilter";
		}

		private void txtFilter_LostFocus(object sender, EventArgs e)
		{
			this.helpContextString = string.Empty;
		}

		private void groupAddTo_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			if (buttonItem.Tag.ToString() == "")
			{
				return;
			}
			try
			{
				DrawObject drawObject = (DrawObject)buttonItem.Tag;
				if ((Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					Template template = this.templatesSupport.CreateTemplate(drawObject, drawObject.Group.Presets, drawObject.Group);
					template.DrawObject.Color = this.drawArea.GetNextColor(template.DrawObject.ObjectType);
					template.DrawObject.FillColor = template.DrawObject.Color;
					template.DeletionForbidden = true;
					template.CreatedFromObject = true;
					this.CreateToolFromTemplate(template, false, false);
				}
				else
				{
					this.GroupAddObject(drawObject);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private DrawObject ValidateTemplatePresence(Template template)
		{
			bool flag = false;
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						string objectType;
						if ((objectType = template.DrawObject.ObjectType) != null)
						{
							if (!(objectType == "Line"))
							{
								if (!(objectType == "Area"))
								{
									if (!(objectType == "Perimeter"))
									{
										if (objectType == "Counter")
										{
											flag = (drawObject.ObjectType == template.DrawObject.ObjectType && drawObject.Name == template.DrawObject.Name && drawObject.Color == template.DrawObject.Color && drawObject.Text == template.DrawObject.Text);
										}
									}
									else
									{
										flag = (drawObject.ObjectType == template.DrawObject.ObjectType && drawObject.Name == template.DrawObject.Name && drawObject.PenWidth == template.DrawObject.PenWidth && drawObject.Color == template.DrawObject.Color);
									}
								}
								else
								{
									flag = (drawObject.ObjectType == template.DrawObject.ObjectType && drawObject.Name == template.DrawObject.Name && drawObject.Color == template.DrawObject.Color);
								}
							}
							else
							{
								flag = (drawObject.ObjectType == template.DrawObject.ObjectType && drawObject.Name == template.DrawObject.Name && drawObject.PenWidth == template.DrawObject.PenWidth && drawObject.Color == template.DrawObject.Color);
							}
						}
						if (flag)
						{
							return drawObject;
						}
					}
				}
			}
			return null;
		}

		private void CreateToolFromTemplate(Template template, bool editFromLibrary = false, bool bDuplicate = false)
		{
			try
			{
				Color defaultColor = Color.Empty;
				if (!editFromLibrary)
				{
					defaultColor = (this.drawArea.ColorAvailable(template.DrawObject.ObjectType, template.DrawObject.Color) ? Color.Empty : this.drawArea.GetNextColor(template.DrawObject.ObjectType));
				}
				string objectType;
				if ((objectType = template.DrawObject.ObjectType) != null)
				{
					if (!(objectType == "Line"))
					{
						if (!(objectType == "Area"))
						{
							if (!(objectType == "Perimeter"))
							{
								if (objectType == "Counter")
								{
									this.SelectCounterTool(template, defaultColor, editFromLibrary, bDuplicate);
								}
							}
							else
							{
								this.SelectPerimeterTool(template, defaultColor, editFromLibrary, bDuplicate);
							}
						}
						else
						{
							this.SelectAreaTool(template, defaultColor, editFromLibrary, bDuplicate);
						}
					}
					else
					{
						this.SelectDistanceTool(template, defaultColor, editFromLibrary, bDuplicate);
					}
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		private void selectToolFromTemplate_Click(object sender, EventArgs e)
		{
			ButtonItem buttonItem = (ButtonItem)sender;
			if (buttonItem.Tag.ToString() == "")
			{
				return;
			}
			try
			{
				Template template = (Template)buttonItem.Tag;
				this.CreateToolFromTemplate(template, false, false);
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			buttonItem.Parent.Visible = false;
		}

		public void EnableOrtho(bool enabled, bool updateGUI = false)
		{
			this.drawArea.OrthoEnabled = enabled;
			Settings.Default.Ortho = enabled;
			if (updateGUI)
			{
				this.switchOrtho.Value = enabled;
			}
		}

		private void switchOrtho_ValueChanged(object sender, EventArgs e)
		{
			this.EnableOrtho(this.switchOrtho.Value, false);
		}

		private void btSetThemeColor_PopupShowing(object sender, EventArgs e)
		{
			this.btSetThemeColor.SubItems[0].Text = Resources.Couleurs_étendues;
			this.btSetThemeColor.SubItems[7].Text = Resources.Couleurs_standard;
			this.btSetThemeColor.DisplayMoreColors = false;
		}

		private void SetPanelsColors(Office2007Renderer renderer)
		{
			if (StyleManager.Style != eStyle.Office2007VistaGlass)
			{
				Color titleText = renderer.ColorTable.RibbonBar.Default.TitleText;
			}
			else
			{
				Color black = Color.Black;
			}
			Office2007RibbonTabGroupColorTable office2007RibbonTabGroupColorTable = new Office2007RibbonTabGroupColorTable();
			office2007RibbonTabGroupColorTable.Background = new LinearGradientColorTable(Color.SteelBlue, Color.LightBlue);
			office2007RibbonTabGroupColorTable.BackgroundHighlight = new LinearGradientColorTable();
			office2007RibbonTabGroupColorTable.Text = Color.White;
			office2007RibbonTabGroupColorTable.Name = "MyGroupColorTable";
			Office2007ColorTable colorTable = ((Office2007Renderer)GlobalManager.Renderer).ColorTable;
			colorTable.RibbonTabGroupColors.Add(office2007RibbonTabGroupColorTable);
			this.ribbonTabItemDBManagement.CustomColorName = "MyGroupColorTable";
		}

		private void SetAlternateColors()
		{
			Office2007Renderer office2007Renderer = GlobalManager.Renderer as Office2007Renderer;
			if (office2007Renderer == null)
			{
				return;
			}
			Office2007Renderer office2007Renderer2 = new Office2007Renderer();
			office2007Renderer2.ColorTable.LegacyColors.BarBackground = this.lstLayers.BackColor;
			office2007Renderer2.ColorTable.LegacyColors.BarBackground2 = this.lstLayers.BackColor;
			office2007Renderer2.ColorTable.LegacyColors.BarDockedBorder = this.lstLayers.BackColor;
			this.barDisplayResults.RenderMode = eRenderMode.Custom;
			this.barDisplayResults.Renderer = office2007Renderer2;
			this.btDisplayResultsForAllPlans.ForeColor = Color.Black;
			this.btDisplayResultsForThisPlan.ForeColor = Color.Black;
			this.tabProperties.ColorScheme.TabPanelBorder = office2007Renderer.ColorTable.RibbonControl.OuterBorder.Start;
			this.tabProperties.ColorScheme.TabItemSelectedBorder = office2007Renderer.ColorTable.RibbonControl.OuterBorder.Start;
			if (StyleManager.Style != eStyle.Office2010Blue && StyleManager.Style != eStyle.Office2010Silver && StyleManager.Style != eStyle.Office2010Black && StyleManager.Style != eStyle.VisualStudio2010Blue)
			{
				this.SetPanelsColors(office2007Renderer);
				return;
			}
			Color.FromArgb(252, 229, 126);
			Color black = Color.Black;
			Color.FromArgb(170, 200, 240);
			Color black2 = Color.Black;
			Office2007ColorTable colorTable = office2007Renderer.ColorTable;
			switch (StyleManager.Style)
			{
			case eStyle.Office2010Silver:
				colorTable.Form.Active.CaptionText = Color.Black;
				colorTable.Form.Inactive.CaptionText = Color.DarkGray;
				colorTable.Form.Inactive.CaptionTopBackground = new LinearGradientColorTable(Color.FromArgb(229, 230, 232));
				colorTable.Form.Inactive.CaptionBottomBackground = new LinearGradientColorTable(Color.FromArgb(229, 230, 232));
				break;
			case eStyle.Office2010Blue:
				colorTable.Form.Active.CaptionText = Color.FromArgb(255, 62, 106, 170);
				colorTable.Form.Inactive.CaptionText = Color.DarkGray;
				break;
			case eStyle.Office2010Black:
				colorTable.Form.Active.CaptionText = Color.AntiqueWhite;
				colorTable.Form.Inactive.CaptionText = Color.DarkGray;
				colorTable.RibbonBar.Default.TitleText = Color.White;
				colorTable.RibbonBar.MouseOver.TitleText = Color.White;
				break;
			}
			RibbonPredefinedColorSchemes.ApplyOffice2007ColorTable(this);
			this.SetPanelsColors(office2007Renderer);
		}

		private void SetThemeResources()
		{
			this.btHelp.Image = ((StyleManager.Style == eStyle.Office2010Black || StyleManager.Style == eStyle.Office2007Black || StyleManager.Style == eStyle.Office2007VistaGlass || Utilities.GetOSInfo(true) == "8") ? Resources.help_16x16_alt : Resources.help_16x16);
		}

		private void SetTooltipsTheme()
		{
			foreach (object obj in this.superTooltip.SuperTooltipInfoTable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (StyleManager.Style == eStyle.Metro)
				{
					((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Office2003;
				}
				else
				{
					switch (StyleManager.Style)
					{
					case eStyle.Office2007Blue:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Gray;
						continue;
					case eStyle.Office2007Silver:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Office2003;
						continue;
					case eStyle.Office2007Black:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Gray;
						continue;
					case eStyle.Office2007VistaGlass:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Gray;
						continue;
					case eStyle.Office2010Silver:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Office2003;
						continue;
					case eStyle.Office2010Blue:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Office2003;
						continue;
					case eStyle.Office2010Black:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Gray;
						continue;
					case eStyle.VisualStudio2010Blue:
						((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Office2003;
						continue;
					}
					((SuperTooltipInfo)dictionaryEntry.Value).Color = eTooltipColor.Default;
				}
			}
		}

		private void SetThemeStyle(eStyle style)
		{
			Color colorTint = Color.Empty;
			if (style == eStyle.Metro)
			{
				StyleManager.MetroColorGeneratorParameters = MetroColorGeneratorParameters.DarkBlue;
				this.startButton.ColorTable = eButtonColor.Orange;
			}
			else
			{
				switch (style)
				{
				case eStyle.Office2007Black:
					if (Utilities.GetOSInfo(true) != "8")
					{
						colorTint = Color.FromArgb(64, 64, 64);
						goto IL_F0;
					}
					goto IL_F0;
				case eStyle.Office2010Silver:
					colorTint = Color.FromArgb(32, 32, 32);
					this.startButton.ColorTable = eButtonColor.Blue;
					goto IL_F0;
				case eStyle.Office2010Blue:
					this.startButton.ColorTable = eButtonColor.Blue;
					goto IL_F0;
				case eStyle.Office2010Black:
					if (Utilities.GetOSInfo(true) != "8")
					{
						colorTint = Color.FromArgb(48, 48, 48);
						this.startButton.ColorTable = eButtonColor.Magenta;
						goto IL_F0;
					}
					this.startButton.ColorTable = eButtonColor.Blue;
					goto IL_F0;
				case eStyle.VisualStudio2010Blue:
					this.startButton.ColorTable = eButtonColor.Magenta;
					goto IL_F0;
				}
				this.startButton.ColorTable = eButtonColor.Orange;
			}
			IL_F0:
			StyleManager.ChangeStyle(style, colorTint);
			this.SetAlternateColors();
			this.SetThemeResources();
			this.SetTooltipsTheme();
		}

		private void SetThemeColor(Color color)
		{
			if (StyleManager.Style == eStyle.Metro)
			{
				StyleManager.MetroColorGeneratorParameters = new MetroColorGeneratorParameters(Color.White, color);
			}
			else
			{
				StyleManager.ColorTint = color;
			}
			this.SetAlternateColors();
		}

		private void AppCommandTheme_Executed(object sender, EventArgs e)
		{
			ICommandSource commandSource = sender as ICommandSource;
			if (commandSource.CommandParameter is string)
			{
				eStyle themeStyle = (eStyle)Enum.Parse(typeof(eStyle), commandSource.CommandParameter.ToString());
				this.SetThemeStyle(themeStyle);
				Settings.Default.ThemeStyle = (int)themeStyle;
				Settings.Default.ThemeColor = Color.Empty;
				return;
			}
			if (commandSource.CommandParameter is Color)
			{
				Color themeColor = (Color)commandSource.CommandParameter;
				this.SetThemeColor(themeColor);
				Settings.Default.ThemeColor = themeColor;
			}
		}

		private void btSetThemeColor_ExpandChange(object sender, EventArgs e)
		{
			if (this.btSetThemeColor.Expanded)
			{
				this.m_ThemeColorSelected = false;
				this.m_ThemeBaseStyle = StyleManager.Style;
				return;
			}
			if (!this.m_ThemeColorSelected)
			{
				if (StyleManager.Style == eStyle.Metro)
				{
					StyleManager.MetroColorGeneratorParameters = MetroColorGeneratorParameters.Default;
				}
				else
				{
					StyleManager.ChangeStyle(this.m_ThemeBaseStyle, Color.Empty);
				}
				this.SetAlternateColors();
			}
		}

		private void btSetThemeColor_ColorPreview(object sender, ColorPreviewEventArgs e)
		{
			if (StyleManager.Style == eStyle.Metro)
			{
				Color color = e.Color;
				StyleManager.MetroColorGeneratorParameters = new MetroColorGeneratorParameters(Color.White, color);
			}
			else
			{
				StyleManager.ColorTint = e.Color;
			}
			this.SetAlternateColors();
		}

		private void btSetThemeColor_SelectedColorChanged(object sender, EventArgs e)
		{
			this.m_ThemeColorSelected = true;
			this.btSetThemeColor.CommandParameter = this.btSetThemeColor.SelectedColor;
		}

		private void SelectCultureInMenu(string culture)
		{
			if (culture != null)
			{
				if (culture == "en-US")
				{
					this.btLanguageEnglish.Checked = true;
					return;
				}
				if (culture == "fr-FR")
				{
					this.btLanguageFrench.Checked = true;
					return;
				}
				if (!(culture == "es"))
				{
					return;
				}
				this.btLanguageSpanish.Checked = true;
			}
		}

		private string SetCulture(string culture, bool displayMessage)
		{
			culture = ((culture == string.Empty) ? Utilities.GetInstallUICulture() : culture);
			string a;
			if ((a = culture) == null || (!(a == "en-US") && !(a == "fr-FR") && !(a == "es")))
			{
				culture = "en-US";
			}
			if (displayMessage)
			{
				string redémarrage_requis = Resources.Redémarrage_requis;
				string vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif = Utilities.Vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif;
				Utilities.DisplayMessage(redémarrage_requis, vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif);
			}
			return culture;
		}

		private void SelectCulture(string culture)
		{
			Settings.Default.UICulture = this.SetCulture(culture, true);
			this.SelectCultureInMenu(Settings.Default.UICulture);
			Settings.Default.Save();
		}

		private void btLanguageEnglish_Click(object sender, EventArgs e)
		{
			this.SelectCulture("en-US");
		}

		private void btLanguageFrench_Click(object sender, EventArgs e)
		{
			this.SelectCulture("fr-FR");
		}

		private void btLanguageSpanish_Click(object sender, EventArgs e)
		{
			this.SelectCulture("es");
		}

		private void btSelectDataFolder_Click(object sender, EventArgs e)
		{
			string dataFolder = Settings.Default.DataFolder;
			Settings.Default.DataFolder = this.dataFolderToSave;
			this.SaveDatabase();
			using (DataFolderForm dataFolderForm = new DataFolderForm())
			{
				dataFolderForm.HelpUtilities = this.HelpUtilities;
				dataFolderForm.HelpContextString = "DataFolderForm";
				dataFolderForm.ShowDialog(this);
			}
			this.dataFolderToSave = Settings.Default.DataFolder;
			Settings.Default.DataFolder = dataFolder;
		}

		private void DisplayPreferencesForm(bool enableCancel = true)
		{
			using (PreferencesForm preferencesForm = new PreferencesForm(enableCancel))
			{
				preferencesForm.HelpUtilities = this.HelpUtilities;
				preferencesForm.HelpContextString = "PreferencesForm";
				preferencesForm.ShowDialog(this);
			}
		}

		private void btPersonalPreferences_Click(object sender, EventArgs e)
		{
			this.DisplayPreferencesForm(true);
			if (this.guiEnabled && this.currentTabIndex == MainForm.RibbonTabEnum.RibbonReport)
			{
				this.ReportRefresh();
			}
		}

		private void btImportationPreferences_Click(object sender, EventArgs e)
		{
			this.EditImportSettings();
		}

		private void btSetDBReadOnly_Click(object sender, EventArgs e)
		{
			this.SetDBIsReadOnly(!Settings.Default.DBIsReadOnly);
		}

		private void btEnableAutoBackup_Click(object sender, EventArgs e)
		{
			this.EnableAutoBackup(!Settings.Default.EnableAutoBackup);
		}

		private void InitializeHelp()
		{
			this.helpUtilities = new HelpUtilities(this, Path.Combine(Utilities.GetInstallHelpFolder(), Utilities.GetCurrentValidUICulture() + "\\" + Utilities.ApplicationHelpFile));
			this.extensionsManager.HelpUtilities = this.HelpUtilities;
			this.BuildHelpContextTable();
		}

		private void BuildHelpContextTable()
		{
			this.helpUtilities.ContextTable.Add("Welcome", 2);
			this.helpUtilities.ContextTable.Add("startButton", 10);
			this.helpUtilities.ContextTable.Add("ribbonTabStart", 11);
			this.helpUtilities.ContextTable.Add("ribbonTabPlans", 12);
			this.helpUtilities.ContextTable.Add("ribbonTabReport", 61);
			this.helpUtilities.ContextTable.Add("ribbonTabEstimatingItems", 77);
			this.helpUtilities.ContextTable.Add("ribbonTabTemplates", 78);
			this.helpUtilities.ContextTable.Add("btEditPaste", 37);
			this.helpUtilities.ContextTable.Add("btEditCut", 37);
			this.helpUtilities.ContextTable.Add("btEditCopy", 37);
			this.helpUtilities.ContextTable.Add("btEditDelete", 37);
			this.helpUtilities.ContextTable.Add("btEditUndo", 37);
			this.helpUtilities.ContextTable.Add("btEditRedo", 37);
			this.helpUtilities.ContextTable.Add("btUndo", 37);
			this.helpUtilities.ContextTable.Add("btRedo", 37);
			this.helpUtilities.ContextTable.Add("btEditSendData", 73);
			this.helpUtilities.ContextTable.Add("btPrintPlan", 69);
			this.helpUtilities.ContextTable.Add("btScaleSet", 44);
			this.helpUtilities.ContextTable.Add("btToolSelection", 30);
			this.helpUtilities.ContextTable.Add("btToolPan", 31);
			this.helpUtilities.ContextTable.Add("btToolArea", 32);
			this.helpUtilities.ContextTable.Add("btToolPerimeter", 33);
			this.helpUtilities.ContextTable.Add("btToolRuler", 35);
			this.helpUtilities.ContextTable.Add("btToolCounter", 34);
			this.helpUtilities.ContextTable.Add("btToolAngle", 36);
			this.helpUtilities.ContextTable.Add("btMarkZone", 43);
			this.helpUtilities.ContextTable.Add("btInsertNote", 45);
			this.helpUtilities.ContextTable.Add("btZoomToSelection", 52);
			this.helpUtilities.ContextTable.Add("btZoomToWindow", 52);
			this.helpUtilities.ContextTable.Add("btZoomActualSize", 52);
			this.helpUtilities.ContextTable.Add("btZoomIn", 52);
			this.helpUtilities.ContextTable.Add("btZoomOut", 52);
			this.helpUtilities.ContextTable.Add("btBrowsePrevious", 53);
			this.helpUtilities.ContextTable.Add("btBrowseNext", 53);
			this.helpUtilities.ContextTable.Add("btBrowseObjectTypePrevious", 53);
			this.helpUtilities.ContextTable.Add("btBrowseObjectTypeNext", 53);
			this.helpUtilities.ContextTable.Add("btBrightnessContrast", 54);
			this.helpUtilities.ContextTable.Add("btRotation", 54);
			this.helpUtilities.ContextTable.Add("BrightnessContrastControls", 54);
			this.helpUtilities.ContextTable.Add("RotationControls", 54);
			this.helpUtilities.ContextTable.Add("btLayerAdd", 58);
			this.helpUtilities.ContextTable.Add("btLayerRemove", 58);
			this.helpUtilities.ContextTable.Add("btLayerRename", 58);
			this.helpUtilities.ContextTable.Add("btGroupLocate", 59);
			this.helpUtilities.ContextTable.Add("btZoomToObject", 59);
			this.helpUtilities.ContextTable.Add("btGroupSelect", 59);
			this.helpUtilities.ContextTable.Add("btGroupRemove", 59);
			this.helpUtilities.ContextTable.Add("btGroupRename", 59);
			this.helpUtilities.ContextTable.Add("btPlanInsertFromPDF", 27);
			this.helpUtilities.ContextTable.Add("btPlanInsertFromImage", 28);
			this.helpUtilities.ContextTable.Add("btPlanLoad", 12);
			this.helpUtilities.ContextTable.Add("btPlanProperties", 12);
			this.helpUtilities.ContextTable.Add("btPlanRemove", 12);
			this.helpUtilities.ContextTable.Add("btReportFilter", 61);
			this.helpUtilities.ContextTable.Add("btReportEdit", 61);
			this.helpUtilities.ContextTable.Add("btReportPrint", 61);
			this.helpUtilities.ContextTable.Add("btReportPrintPreview", 61);
			this.helpUtilities.ContextTable.Add("btReportPrintSetup", 61);
			this.helpUtilities.ContextTable.Add("btReportExportToExcel", 72);
			this.helpUtilities.ContextTable.Add("btReportExportToXML", 61);
			this.helpUtilities.ContextTable.Add("btReportExportToHTML", 61);
			this.helpUtilities.ContextTable.Add("btReportExportToPDF", 61);
			this.helpUtilities.ContextTable.Add("ToolEditFormArea", 32);
			this.helpUtilities.ContextTable.Add("ToolEditFormPerimeter", 33);
			this.helpUtilities.ContextTable.Add("ToolEditFormLine", 35);
			this.helpUtilities.ContextTable.Add("ToolEditFormCounter", 34);
			this.helpUtilities.ContextTable.Add("ScaleForm", 44);
			this.helpUtilities.ContextTable.Add("PDFSelectionForm", 27);
			this.helpUtilities.ContextTable.Add("PlanPropertiesForm", 12);
			this.helpUtilities.ContextTable.Add("PlanRemoveForm", 12);
			this.helpUtilities.ContextTable.Add("ProjectForm", 14);
			this.helpUtilities.ContextTable.Add("ReportEditForm", 61);
			this.helpUtilities.ContextTable.Add("PresetEditForm", 50);
			this.helpUtilities.ContextTable.Add("NoteEditForm", 45);
			this.helpUtilities.ContextTable.Add("DataFolderForm", 68);
			this.helpUtilities.ContextTable.Add("DBEstimatingForm", 77);
			this.helpUtilities.ContextTable.Add("PreferencesForm", 84);
			this.helpUtilities.ContextTable.Add("ImportSettingsForm", 86);
			this.helpUtilities.ContextTable.Add("switchOrtho", 71);
			this.helpUtilities.ContextTable.Add("btExportPlanToPDF", 80);
			this.helpUtilities.ContextTable.Add("btPlansPrint", 81);
			this.helpUtilities.ContextTable.Add("btPlansExport", 82);
			this.helpUtilities.ContextTable.Add("PlansActionPrint", 81);
			this.helpUtilities.ContextTable.Add("PlansActionExport", 82);
			this.helpUtilities.ContextTable.Add("txtGroupsFilter", 18);
			this.helpUtilities.ContextTable.Add("btEstimatingMaterial", 77);
			this.helpUtilities.ContextTable.Add("btEstimatingLabour", 77);
			this.helpUtilities.ContextTable.Add("btEstimatingEquipment", 77);
			this.helpUtilities.ContextTable.Add("btEstimatingSubcontract", 77);
			this.helpUtilities.ContextTable.Add("btEstimatingModify", 77);
			this.helpUtilities.ContextTable.Add("btEstimatingDuplicate", 77);
			this.helpUtilities.ContextTable.Add("btEstimatingDelete", 77);
			this.helpUtilities.ContextTable.Add("btTemplateArea", 78);
			this.helpUtilities.ContextTable.Add("btTemplatePerimeter", 78);
			this.helpUtilities.ContextTable.Add("btTemplateLength", 78);
			this.helpUtilities.ContextTable.Add("btTemplateCounter", 78);
			this.helpUtilities.ContextTable.Add("btTemplateModify", 78);
			this.helpUtilities.ContextTable.Add("btTemplateDuplicate", 78);
			this.helpUtilities.ContextTable.Add("btTemplateDelete", 78);
		}

		private void btHelp_Click(object sender, EventArgs e)
		{
			this.ShowHelp();
		}

		private void superTooltip_BeforeTooltipDisplay(object sender, SuperTooltipEventArgs e)
		{
			try
			{
				this.helpContextString = ((BaseItem)e.Source).Name;
			}
			catch
			{
			}
		}

		private void superTooltip_TooltipClosed(object sender, EventArgs e)
		{
			this.helpContextString = "";
		}

		private void startButton_PopupOpen(object sender, PopupOpenEventArgs e)
		{
			this.helpContextString = "startButton";
		}

		private void startButton_PopupClose(object sender, EventArgs e)
		{
			this.helpContextString = "";
		}

		private void webBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				this.ShowHelp();
			}
		}

		private string GetDefaultHelpContext()
		{
			if (this.panelWelcome.Visible)
			{
				return "Welcome";
			}
			return this.ribbonControl.SelectedRibbonTabItem.Name;
		}

		private void ShowHelp()
		{
			this.helpUtilities.ShowHelp((this.helpContextString != string.Empty) ? this.helpContextString : this.GetDefaultHelpContext());
		}

		private bool ValidateLayoutDefinition(string layoutDefinition)
		{
			string text = layoutDefinition.ToLower();
			return text.IndexOf("visible=\"false\"") == -1 || text == "";
		}

		private void SaveLayout()
		{
			if (!this.ValidateLayoutDefinition(this.dotNetBarManager.LayoutDefinition))
			{
				return;
			}
			this.dotNetBarManager.RightDockSite.BringToFront();
			this.dotNetBarManager.LeftDockSite.BringToFront();
			this.dotNetBarManager.BottomDockSite.BringToFront();
			this.dotNetBarManager.TopDockSite.BringToFront();
			this.panelWelcome.BringToFront();
			this.mainControl.BringToFront();
			Settings.Default.TakeoffLayout = this.dotNetBarManager.LayoutDefinition;
		}

		private void LoadLayout()
		{
			if (!this.ValidateLayoutDefinition(Settings.Default.TakeoffLayout))
			{
				Settings.Default.TakeoffLayout = Settings.Default.DefaultLayout;
			}
			try
			{
				this.dotNetBarManager.LayoutDefinition = Settings.Default.TakeoffLayout;
			}
			catch
			{
				Settings.Default.TakeoffLayout = Settings.Default.DefaultLayout;
				try
				{
					this.dotNetBarManager.LayoutDefinition = Settings.Default.TakeoffLayout;
				}
				catch
				{
				}
			}
			this.dotNetBarManager.RightDockSite.BringToFront();
			this.dotNetBarManager.LeftDockSite.BringToFront();
			this.dotNetBarManager.BottomDockSite.BringToFront();
			this.dotNetBarManager.TopDockSite.BringToFront();
			this.panelWelcome.BringToFront();
			this.mainControl.BringToFront();
		}

		private void EnableDockingWindows(bool visible, bool saveAndRestoreLayout = true)
		{
			if (this.dockingWindows.Count == 0)
			{
				return;
			}
			bool visible2 = this.dockingWindows[0].Visible;
			this.dotNetBarManager.SuspendLayout = true;
			if (visible2 != visible)
			{
				if (!visible && saveAndRestoreLayout)
				{
					this.SaveLayout();
				}
				foreach (DockingWindow dockingWindow in this.dockingWindows)
				{
					if (!visible)
					{
						dockingWindow.ContainerBar.AutoHide = false;
						dockingWindow.ContainerBar.Visible = false;
					}
					dockingWindow.Visible = visible;
				}
				if (visible && saveAndRestoreLayout)
				{
					this.LoadLayout();
				}
			}
			this.dotNetBarManager.SuspendLayout = false;
		}

		private void btResetDefaultPanelsLayout_Click(object sender, EventArgs e)
		{
			if (Utilities.DisplayQuestion(this.lblPanels.Text, Resources.Restaurer_les_volets_d_origine) == DialogResult.No)
			{
				return;
			}
			Settings.Default.TakeoffLayout = Settings.Default.DefaultLayout;
			if (Utilities.ConvertToInt(this.ribbonControl.SelectedRibbonTabItem.Tag.ToString()) == 0)
			{
				this.LoadLayout();
			}
		}

		private void InitializeCOfficeGui()
		{
			TabItem tabItem = this.tabProperties.CreateTab("Contractor's Office");
			this.coControl = new CEstimatingsItemsControl(null);
			this.coControl.Name = "coControl";
			this.coControl.Dock = DockStyle.Fill;
			tabItem.AttachedControl.Controls.Add(this.coControl);
			this.btReportExportToCOffice.Visible = true;
		}

		private void ReportExportToCOffice()
		{
			bool flag = false;
			if (this.project.JobNumber == "")
			{
				string title = "Contractor's Office";
				string vous_devez_fournir_un_numéro_de_projet = Resources.Vous_devez_fournir_un_numéro_de_projet;
				Utilities.DisplayError(title, vous_devez_fournir_un_numéro_de_projet);
				return;
			}
			if (!this.ValidateFormulas())
			{
				return;
			}
			this.LockInterface(ref flag);
			string sExportFilename = Path.Combine(this.COInterface.ProjectsPath(), this.project.JobNumber + ".csv");
			this.reportsControl.ExportToCOffice(sExportFilename);
			this.UnlockInterface(ref flag);
		}

		private void btReportExportToCOffice_Click(object sender, EventArgs e)
		{
			this.ReportExportToCOffice();
		}

		private void SaveBackup()
		{
			if (this.project.FullFileName == string.Empty)
			{
				return;
			}
			bool flag = false;
			this.LockInterface(ref flag);
			Console.WriteLine("SaveBackup");
			this.project.Save(this.project.FullFileName + ".bak", true);
			this.UnlockInterface(ref flag);
		}

		private void CheckForBackup(string projectFileName)
		{
			if (!Utilities.FileExists(projectFileName + ".bak"))
			{
				return;
			}
			string copie_de_sauvegarde_trouvée = Resources.Copie_de_sauvegarde_trouvée;
			string message = Utilities.ApplicationName + " " + Resources.a_trouvé_une_copie_de_sauvegarde_automatique;
			if (Utilities.DisplayQuestion(copie_de_sauvegarde_trouvée, message) == DialogResult.No)
			{
				if (Utilities.FileExists(projectFileName + ".last"))
				{
					Utilities.FileDelete(projectFileName + ".last", true);
				}
				Utilities.FileDelete(projectFileName + ".bak", true);
				return;
			}
			if (Utilities.FileExists(projectFileName + ".last"))
			{
				Utilities.FileDelete(projectFileName + ".last", true);
			}
			if (Utilities.FileExists(projectFileName))
			{
				Utilities.FileCopy(projectFileName, projectFileName + ".last");
				Utilities.FileDelete(projectFileName, true);
			}
			Utilities.FileCopy(projectFileName + ".bak", projectFileName);
			Utilities.FileDelete(projectFileName + ".bak", true);
		}

		private void DeleteBackup()
		{
			if (this.project.FullFileName == string.Empty)
			{
				return;
			}
			Console.WriteLine("DeleteBackup");
			if (Utilities.FileExists(this.project.FullFileName + ".bak"))
			{
				Utilities.FileDelete(this.project.FullFileName + ".bak", true);
			}
		}

		private void InitializeBackupTimer()
		{
			this.backupTimer.Interval = Settings.Default.BackupInterval;
			this.backupTimer.OnTimer += this.backupTimer_OnTimer;
		}

		private void EnableBackup()
		{
			Console.WriteLine("EnableBackup");
			this.backupTimer.Start();
		}

		private void DisableBackup()
		{
			Console.WriteLine("DisableBackup");
			this.backupTimer.Stop();
		}

		private void WaitForBackupCompletion()
		{
			Console.WriteLine("WaitForBackupCompletion");
			do
			{
				Application.DoEvents();
			}
			while (this.backupTimer.InProgress());
		}

		private void backupTimer_OnTimer(object sender, EventArgs e)
		{
			Console.WriteLine("backupTimer_OnTimer");
			this.SaveBackup();
			this.DisableBackup();
		}

		private bool bInterfaceIsLock;

		private bool guiReady;

		private bool offbound;

		private bool updatePan;

		private bool pendingPaint;

		private bool guiEnabled;

		private bool imageEditing;

		private bool reportEditing;

		private bool plansSelection;

		private bool suspendScaleValidate;

		private bool mustRestorePlan;

		private bool exitNow;

		private bool forceApplicationExit;

		private Point contextMenuPoint;

		private Bookmark imageEditingBookmark;

		private string dataFolderToSave = string.Empty;

		private Bitmap buttonImage = new Bitmap(64, 64);

		private MainForm.ApplicationStatusEnum applicationStatus;

		private MainForm.RibbonTabEnum currentTabIndex;

		public MainForm.ImageQualityEnum ImageQuality = MainForm.ImageQualityEnum.QualityHigh;

		private PlansNavigator.PlansActionEnum plansAction;

		private string singlePDFFileName;

		private PdfDocument singlePDFDoc;

		private int selectedPlanIndex;

		private Variables selectedPlans = new Variables();

		private Project project = new Project();

		private DrawingArea drawArea = new DrawingArea();

		private ExtensionsSupport extensionsSupport = new ExtensionsSupport();

		private TemplatesSupport templatesSupport = new TemplatesSupport();

		private MruManager mruManager = new MruManager();

		private ThumbnailPanel previewPanel;

		private BackupTimer backupTimer = new BackupTimer();

		private List<DockingWindow> dockingWindows = new List<DockingWindow>();

		private int duplicatePlanNumberOfCopies;

		private bool duplicatePlanCopyObjects;

		private string helpContextString;

		private HelpUtilities helpUtilities;

		private DBManagement dbManagement;

		private DrawObjectEditor objectEditor;

		private DrawObjectsNavigator objectsNavigator;

		private LayersNavigator layersNavigator;

		private PlansNavigator plansNavigator;

		private RecentPlansNavigator recentPlansNavigator;

		private EstimatingEditor estimatingEditor;

		private DBEstimatingItemsEditor dbEstimatingItemsEditor;

		private TemplatesLibraryEditor templatesLibraryEditor;

		private CEstimatingsItemsControl estimatingItemsControl;

		private CEstimatingsItemsControl coControl;

		private COfficeInterface coInterface = new COfficeInterface();

		private bool isActivated;

		private uint trialDaysRemaining;

		private TurboActivate TurboActivate;

		private ReportEditForm frmReportEdit;

		private bool m_ThemeColorSelected;

		private eStyle m_ThemeBaseStyle = eStyle.Office2010Silver;

		private enum ApplicationStatusEnum
		{
			StatusReady,
			StatusModified,
			StatusSaved
		}

		private enum RibbonTabEnum
		{
			RibbonStart,
			RibbonPlans,
			RibbonReport,
			RibbonTemplates,
			RibbonEstimating,
			RibbonExtensions
		}

		public enum ImageQualityEnum
		{
			QualityLow,
			QualityHigh
		}

		private enum DockingWindowEnum
		{
			PanWindow,
			PropertiesWindow,
			LayersWindow,
			GroupsWindow,
			RecentPlansWindow,
			EstimatingWindow
		}

		private delegate void IsActivatedDelegate();

		private class GroupSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				int result;
				try
				{
					DrawObject drawObject = x as DrawObject;
					DrawObject drawObject2 = y as DrawObject;
					if (drawObject.ObjectType != drawObject2.ObjectType)
					{
						result = StringLogicalComparer.Compare(drawObject.ObjectSortOrder.ToString(), drawObject2.ObjectSortOrder.ToString());
					}
					else
					{
						result = StringLogicalComparer.Compare(drawObject.Name, drawObject2.Name);
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}
		}

		private class TemplateSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				int result;
				try
				{
					Template template = x as Template;
					Template template2 = y as Template;
					if (template.DrawObject.ObjectType != template2.DrawObject.ObjectType)
					{
						result = StringLogicalComparer.Compare((template.DeletionForbidden ? "A" : "B") + template.DrawObject.ObjectSortOrder.ToString(), (template2.DeletionForbidden ? "A" : "B") + template2.DrawObject.ObjectSortOrder.ToString());
					}
					else
					{
						result = StringLogicalComparer.Compare((template.DeletionForbidden ? "A" : "B") + template.DrawObject.Name, (template2.DeletionForbidden ? "A" : "B") + template2.DrawObject.Name);
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}
		}
	}
}
