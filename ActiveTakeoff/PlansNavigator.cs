using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Foxit.PDF.Rasterizer;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class PlansNavigator
	{
		public event OnPlanSelectedHandler OnPlanSelected
		{
			add
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanSelected;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Combine(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanSelected, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
			remove
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanSelected;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Remove(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanSelected, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
		}

		public event OnPlanSelectedHandler OnPlanLoad
		{
			add
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanLoad;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Combine(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanLoad, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
			remove
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanLoad;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Remove(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanLoad, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
		}

		public event OnPlanSelectedHandler OnPlanEdit
		{
			add
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanEdit;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Combine(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanEdit, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
			remove
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanEdit;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Remove(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanEdit, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
		}

		public event OnPlanSelectedHandler OnPlanRemove
		{
			add
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanRemove;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Combine(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanRemove, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
			remove
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanRemove;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Remove(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanRemove, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
		}

		public event OnPlanSelectedHandler OnPlanReordered
		{
			add
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanReordered;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Combine(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanReordered, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
			remove
			{
				OnPlanSelectedHandler onPlanSelectedHandler = this.OnPlanReordered;
				OnPlanSelectedHandler onPlanSelectedHandler2;
				do
				{
					onPlanSelectedHandler2 = onPlanSelectedHandler;
					OnPlanSelectedHandler value2 = (OnPlanSelectedHandler)Delegate.Remove(onPlanSelectedHandler2, value);
					onPlanSelectedHandler = Interlocked.CompareExchange<OnPlanSelectedHandler>(ref this.OnPlanReordered, value2, onPlanSelectedHandler2);
				}
				while (onPlanSelectedHandler != onPlanSelectedHandler2);
			}
		}

		public event OnPlanActionHandler OnPlanApplyAction
		{
			add
			{
				OnPlanActionHandler onPlanActionHandler = this.OnPlanApplyAction;
				OnPlanActionHandler onPlanActionHandler2;
				do
				{
					onPlanActionHandler2 = onPlanActionHandler;
					OnPlanActionHandler value2 = (OnPlanActionHandler)Delegate.Combine(onPlanActionHandler2, value);
					onPlanActionHandler = Interlocked.CompareExchange<OnPlanActionHandler>(ref this.OnPlanApplyAction, value2, onPlanActionHandler2);
				}
				while (onPlanActionHandler != onPlanActionHandler2);
			}
			remove
			{
				OnPlanActionHandler onPlanActionHandler = this.OnPlanApplyAction;
				OnPlanActionHandler onPlanActionHandler2;
				do
				{
					onPlanActionHandler2 = onPlanActionHandler;
					OnPlanActionHandler value2 = (OnPlanActionHandler)Delegate.Remove(onPlanActionHandler2, value);
					onPlanActionHandler = Interlocked.CompareExchange<OnPlanActionHandler>(ref this.OnPlanApplyAction, value2, onPlanActionHandler2);
				}
				while (onPlanActionHandler != onPlanActionHandler2);
			}
		}

		private void LoadResources()
		{
		}

		private void FlowLayoutPanelInitialize()
		{
			this.flowLayoutPanel.Margin = new Padding(0, 0, 0, 0);
			this.flowLayoutPanel.Resize += this.flowLayoutPanel_Resize;
			this.flowLayoutPanel.PreviewKeyDown += this.flowLayoutPanel_PreviewKeyDown;
			this.flowLayoutPanel.DragEnter += this.flowLayoutPanel_DragEnter;
			this.flowLayoutPanel.DragOver += this.flowLayoutPanel_DragOver;
			this.flowLayoutPanel.DragDrop += this.flowLayoutPanel_DragDrop;
		}

		private void BackgroundWorkerInitialize()
		{
			this.backgroundWorker.WorkerReportsProgress = false;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
		}

		public PlansNavigator(Project project, DrawingArea drawArea, FlowLayoutPanel flowLayoutPanel, BackgroundWorker backgroundWorker)
		{
			this.project = project;
			this.drawArea = drawArea;
			this.flowLayoutPanel = flowLayoutPanel;
			this.backgroundWorker = backgroundWorker;
			this.enabled = true;
			this.multiSelectionMode = false;
			this.thumbnailsList = new Variables();
			this.FlowLayoutPanelInitialize();
			this.BackgroundWorkerInitialize();
			this.LoadResources();
		}

		private Plan CastPanelToPlan(object panel)
		{
			Plan result;
			try
			{
				result = ((ThumbnailPanel)panel).Plan;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private bool PlanExists(Plan plan)
		{
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				Plan plan2 = this.CastPanelToPlan((ThumbnailPanel)this.flowLayoutPanel.Controls[i]);
				if (plan2 != null && plan.Equals(plan2))
				{
					return true;
				}
			}
			return false;
		}

		private void RecalcPanelsTabIndex()
		{
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				this.flowLayoutPanel.Controls[i].Name = "panelThumbnail" + (i + 1).ToString();
				this.flowLayoutPanel.Controls[i].TabIndex = i;
			}
		}

		private void InitMultiSelectMode(bool enable)
		{
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				ThumbnailPanel thumbnailPanel = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
				if (thumbnailPanel != null)
				{
					thumbnailPanel.Selected = enable;
					thumbnailPanel.AllowDrag = !enable;
					thumbnailPanel.EnableDoubleClick(!enable);
				}
			}
			this.flowLayoutPanel.Refresh();
		}

		private bool FocusPanel(Plan plan)
		{
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				Plan plan2 = this.CastPanelToPlan((ThumbnailPanel)this.flowLayoutPanel.Controls[i]);
				if (plan2 != null && plan.Equals(plan2))
				{
					Utilities.SetObjectFocus(this.flowLayoutPanel.Controls[i]);
					return true;
				}
			}
			return false;
		}

		private ThumbnailPanel FindPanel(Plan plan)
		{
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				Plan plan2 = this.CastPanelToPlan((ThumbnailPanel)this.flowLayoutPanel.Controls[i]);
				if (plan2 != null && plan.Equals(plan2))
				{
					return (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
				}
			}
			return null;
		}

		public int Count
		{
			get
			{
				return this.flowLayoutPanel.Controls.Count;
			}
		}

		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				this.flowLayoutPanel.Enabled = this.enabled;
				if (!this.enabled)
				{
					this.Clear();
				}
			}
		}

		public void SelectAll(bool select)
		{
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				ThumbnailPanel thumbnailPanel = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
				if (thumbnailPanel != null)
				{
					thumbnailPanel.Selected = select;
				}
			}
			this.flowLayoutPanel.Refresh();
		}

		public bool MultiSelectionMode
		{
			get
			{
				return this.multiSelectionMode;
			}
			set
			{
				this.multiSelectionMode = value;
				this.InitMultiSelectMode(this.multiSelectionMode);
			}
		}

		public Plan SelectedPlan
		{
			get
			{
				return this.selectedPlan;
			}
			set
			{
				this.selectedPlan = value;
				this.FocusPanel(value);
				this.flowLayoutPanel.Invalidate();
			}
		}

		public void Clear()
		{
			for (int i = this.flowLayoutPanel.Controls.Count - 1; i >= 0; i--)
			{
				Control control = this.flowLayoutPanel.Controls[i];
				this.flowLayoutPanel.Controls.RemoveAt(i);
				control.Dispose();
			}
			this.flowLayoutPanel.Controls.Clear();
			this.selectedPlan = null;
		}

		public void ImportImageFiles(string[] files)
		{
			try
			{
				NumericComparer comparer = new NumericComparer();
				Array.Sort(files, comparer);
			}
			catch
			{
			}
			this.thumbnailsList.Clear();
			foreach (string name in files)
			{
				this.thumbnailsList.Add(new Variable(name, 0, PlansNavigator.ThumbnailsCreationEnum.CreateFromImages));
			}
			if (this.thumbnailsList.Count > 0)
			{
				this.CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum.CreateFromImages);
				this.RecalcPanelsTabIndex();
			}
			this.EnsureSelected();
		}

		private void SelectPDFPages(string fileName, Variables pages)
		{
			try
			{
				using (PDFSelectionForm pdfselectionForm = new PDFSelectionForm(fileName, pages))
				{
					pdfselectionForm.HelpUtilities = this.drawArea.Owner.HelpUtilities;
					pdfselectionForm.HelpContextString = "PDFSelectionForm";
					pdfselectionForm.ShowDialog(this.drawArea.Owner);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		public void ImportPDFFiles(string[] files)
		{
			try
			{
				NumericComparer comparer = new NumericComparer();
				Array.Sort(files, comparer);
			}
			catch
			{
			}
			this.thumbnailsList.Clear();
			foreach (string text in files)
			{
				int num = PDFUtilities.PagesCount(text);
				Variables variables = new Variables();
				for (int j = 0; j < num; j++)
				{
					variables.Add(new Variable(j.ToString(), true));
				}
				if (num > 1)
				{
					this.SelectPDFPages(text, variables);
				}
				for (int k = 0; k < variables.Count; k++)
				{
					if (Utilities.ConvertToBoolean(variables[k].Value, true))
					{
						this.thumbnailsList.Add(new Variable(text, k, PlansNavigator.ThumbnailsCreationEnum.CreateFromPDFFiles));
					}
				}
			}
			if (this.thumbnailsList.Count > 0)
			{
				this.CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum.CreateFromPDFFiles);
				this.RecalcPanelsTabIndex();
			}
			this.EnsureSelected();
		}

		public void DuplicateFromProject(Plan plan, int numberOfCopies, bool copyObjects)
		{
			this.thumbnailsList.Clear();
			for (int i = 0; i < numberOfCopies; i++)
			{
				this.thumbnailsList.Add(new Variable(copyObjects.ToString(), plan, PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject));
			}
			if (this.thumbnailsList.Count > 0)
			{
				this.CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject);
				this.RecalcPanelsTabIndex();
			}
			this.EnsureSelected();
		}

		public void ValidateThumbnails()
		{
			this.thumbnailsList.Clear();
			for (int i = 0; i < this.project.Plans.Count; i++)
			{
				if (!this.PlanExists(this.project.Plans[i]))
				{
					this.thumbnailsList.Add(new Variable(i.ToString(), i, PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans));
				}
			}
			if (this.thumbnailsList.Count > 0)
			{
				this.CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans);
				this.RecalcPanelsTabIndex();
			}
			if (this.drawArea.ActivePlan != null)
			{
				this.selectedPlan = this.drawArea.ActivePlan;
			}
			this.EnsureSelected();
		}

		public void CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum thumbnailsCreationContext)
		{
			try
			{
				string description = "";
				switch (thumbnailsCreationContext)
				{
				case PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans:
					description = Resources.Chargement_de_la_miniature;
					break;
				case PlansNavigator.ThumbnailsCreationEnum.CreateFromImages:
				case PlansNavigator.ThumbnailsCreationEnum.CreateFromPDFFiles:
					description = Resources.Importation_de;
					break;
				case PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject:
					description = Resources.Duplication_en_cours;
					break;
				}
				this.frmProgress = new ProgressForm(description, "", thumbnailsCreationContext != PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans);
				this.backgroundWorker.RunWorkerAsync();
				this.frmProgress.ShowDialog();
				this.frmProgress = null;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
		}

		public void Remove(Plan plan)
		{
			ThumbnailPanel thumbnailPanel = this.FindPanel(plan);
			if (thumbnailPanel != null)
			{
				this.flowLayoutPanel.Controls.Remove(thumbnailPanel);
				this.EnsureSelected();
			}
		}

		public void Refresh()
		{
			this.flowLayoutPanel.Refresh();
		}

		public void SetFocus(bool value)
		{
			if (!value)
			{
				this.flowLayoutPanel.Focus();
				return;
			}
			this.SelectedPlan = this.selectedPlan;
		}

		public int SelectedCount()
		{
			int num = 0;
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				ThumbnailPanel thumbnailPanel = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
				if (thumbnailPanel != null && thumbnailPanel.Selected)
				{
					num++;
				}
			}
			return num;
		}

		public void ActionApply(PlansNavigator.PlansActionEnum plansAction)
		{
			int num = 0;
			int num2 = this.SelectedCount();
			if (num2 > 0)
			{
				for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
				{
					ThumbnailPanel thumbnailPanel = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
					if (thumbnailPanel != null && thumbnailPanel.Selected)
					{
						num++;
						Plan plan = this.CastPanelToPlan(thumbnailPanel);
						if (plan != null && this.OnPlanApplyAction != null)
						{
							this.OnPlanApplyAction(plansAction, plan, num, num2);
						}
					}
				}
			}
		}

		private void CancelThread(BackgroundWorker worker)
		{
			try
			{
				worker.CancelAsync();
			}
			catch
			{
			}
		}

		private void UpdateProgressDescriptions(BackgroundWorker worker, string description1, string description2)
		{
			if (this.frmProgress != null)
			{
				if (this.frmProgress.IsHandleCreated)
				{
					this.frmProgress.progressBar.Invoke(new MethodInvoker(delegate()
					{
						this.frmProgress.lblDescription.Text = description1;
						this.frmProgress.lblDescription2.Text = description2;
					}));
				}
				Application.DoEvents();
				return;
			}
			this.CancelThread(worker);
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			string text = "";
			PdfRasterizer pdfRasterizer = null;
			Application.DoEvents();
			Thread.Sleep(10);
			int i;
			for (i = 0; i < this.thumbnailsList.Count; i++)
			{
				try
				{
					Plan plan = null;
					switch ((PlansNavigator.ThumbnailsCreationEnum)this.thumbnailsList[i].Tag)
					{
					case PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans:
						plan = this.project.Plans[Utilities.ConvertToInt(this.thumbnailsList[i].Name)];
						if (plan != null)
						{
							this.UpdateProgressDescriptions(worker, Resources.Chargement_de_la_miniature, plan.FileName);
						}
						break;
					case PlansNavigator.ThumbnailsCreationEnum.CreateFromImages:
					{
						string name = this.thumbnailsList[i].Name;
						string text2 = Utilities.StripInvalidCharacters(Path.GetFileNameWithoutExtension(name), "");
						string directoryName = Utilities.GetDirectoryName(name);
						string fileName = Utilities.GetFileName(name, false);
						this.UpdateProgressDescriptions(worker, Resources.Importation_de, fileName);
						string folderName = this.project.FolderName;
						Utilities.ValidateDirectory(folderName);
						string text3;
						if (directoryName == folderName)
						{
							text3 = name;
						}
						else
						{
							text3 = PDFUtilities.GetPageUniqueFileName(Path.Combine(folderName, fileName));
							Utilities.FileCopy(name, text3);
						}
						plan = this.project.InsertPlan(text2, text3, false, 0, 0);
						plan.Dirty = true;
						break;
					}
					case PlansNavigator.ThumbnailsCreationEnum.CreateFromPDFFiles:
					{
						string name = this.thumbnailsList[i].Name;
						this.UpdateProgressDescriptions(worker, Resources.Importation_de, Utilities.GetFileName(name, false));
						if (this.frmProgress != null)
						{
							if (name != text)
							{
								if (pdfRasterizer != null)
								{
									pdfRasterizer = null;
									GC.Collect();
								}
								pdfRasterizer = PDFUtilities.OpenPDFFile(name);
								text = name;
							}
							if (this.frmProgress != null && pdfRasterizer != null)
							{
								int importDpi = Settings.Default.ImportDpi;
								bool convertToGrayscale = !Settings.Default.ConvertPDFToColor;
								int num = Utilities.ConvertToInt(this.thumbnailsList[i].Value);
								string text2 = Utilities.StripInvalidCharacters(Path.GetFileNameWithoutExtension(name) + ((pdfRasterizer.Pages.Count > 1) ? (" - " + (num + 1).ToString()) : ""), "");
								string path = text2 + ".png";
								string folderName = this.project.FolderName;
								Utilities.ValidateDirectory(folderName);
								double maxSize = Utilities.ComputeAvailableMemoryForImage();
								string text3 = PDFUtilities.GetPageUniqueFileName(Path.Combine(folderName, path));
								double width;
								double height;
								if (PDFUtilities.GetPageDimension(text, num, out width, out height) && PDFUtilities.ConvertPDFPageToImage(pdfRasterizer, width, height, num, text3, maxSize, importDpi, convertToGrayscale) && this.frmProgress != null)
								{
									plan = this.project.InsertPlan(text2, text3, false, 0, 0);
									plan.Dirty = true;
								}
							}
						}
						break;
					}
					case PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject:
					{
						Plan plan2 = (Plan)this.thumbnailsList[i].Value;
						bool flag = Utilities.ConvertToBoolean(this.thumbnailsList[i].Name, false);
						if (plan2 != null)
						{
							string text2 = this.project.Plans.FindFreePlanName(plan2.Name);
							this.UpdateProgressDescriptions(worker, Resources.Duplication_en_cours, text2);
							string text3 = PDFUtilities.GetPageUniqueFileName(Path.Combine(plan2.FolderName, plan2.FileName));
							Utilities.FileCopy(plan2.FullFileName, text3);
							plan = this.project.InsertPlan(text2, text3, false, plan2.Brightness, plan2.Contrast);
							plan.UnitScale = plan2.UnitScale.Duplicate();
							if (flag)
							{
								plan.Layers = plan2.Layers.Duplicate();
							}
							foreach (object obj in plan.Layers.Collection)
							{
								Layer layer = (Layer)obj;
								foreach (object obj2 in layer.DrawingObjects.Collection)
								{
									DrawObject drawObject = (DrawObject)obj2;
									if (drawObject.GetType().Name == "DrawLegend")
									{
										((DrawLegend)drawObject).Plan = plan;
									}
								}
							}
							plan.Dirty = true;
						}
						break;
					}
					}
					if (plan != null)
					{
						if (!plan.Thumbnail.IsValid())
						{
							plan.CreateThumbnail(false);
						}
						this.flowLayoutPanel.Invoke(new MethodInvoker(delegate()
						{
							ThumbnailPanel thumbnailPanel = new ThumbnailPanel(this.flowLayoutPanel);
							thumbnailPanel.DrawArea = this.drawArea;
							thumbnailPanel.ThumbnailMarging = new Padding(10, 3, 0, 0);
							thumbnailPanel.ThumbnailPadding = new Padding(15, 38, 0, 0);
							thumbnailPanel.ThumbnailSize = new Size(256, 192);
							thumbnailPanel.RecalcLayout();
							thumbnailPanel.Plan = plan;
							this.flowLayoutPanel.Controls.Add(thumbnailPanel);
							thumbnailPanel.Paint += this.panel_Paint;
							thumbnailPanel.PreviewKeyDown += this.flowLayoutPanel_PreviewKeyDown;
							thumbnailPanel.Enter += this.panel_Enter;
							thumbnailPanel.Click += this.panel_Click;
							thumbnailPanel.DoubleClick += this.panel_DoubleClick;
							thumbnailPanel.AllowDrag = true;
							int num2 = this.flowLayoutPanel.Width / thumbnailPanel.Width;
							int count = this.flowLayoutPanel.Controls.Count;
							if (count <= num2)
							{
								this.flowLayoutPanel.Padding = new Padding((this.flowLayoutPanel.Width - thumbnailPanel.Width * count) / 2, 12, 0, 0);
							}
						}));
					}
					if (this.frmProgress == null)
					{
						this.CancelThread(worker);
						break;
					}
					if (this.frmProgress.IsHandleCreated)
					{
						this.frmProgress.progressBar.Invoke(new MethodInvoker(delegate()
						{
							int num2 = Utilities.ConvertToInt((double)(i + 1) * (100.0 / (double)this.thumbnailsList.Count));
							this.frmProgress.progressBar.Text = num2 + " %";
							this.frmProgress.progressBar.Value = num2;
							Console.WriteLine(num2 + " %");
						}));
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
				}
				Thread.Sleep(100);
			}
			if (this.frmProgress != null && this.frmProgress.IsHandleCreated)
			{
				this.frmProgress.progressBar.Invoke(new MethodInvoker(delegate()
				{
					this.frmProgress.progressBar.Text = "100 %";
					this.frmProgress.progressBar.Value = 100;
				}));
			}
			Thread.Sleep(200);
			if (pdfRasterizer != null)
			{
				pdfRasterizer = null;
				GC.Collect();
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.frmProgress != null)
			{
				this.frmProgress.Hide();
				this.frmProgress = null;
			}
			if (e.Error != null)
			{
				Utilities.DisplaySystemError(e.Error);
			}
		}

		private void panel_Click(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			if (this.drawArea.Owner.PlansSelection)
			{
				try
				{
					this.exitNow = true;
					ThumbnailPanel thumbnailPanel = (ThumbnailPanel)sender;
					bool selected = thumbnailPanel.Selected;
					thumbnailPanel.Selected = !selected;
					thumbnailPanel.Invalidate();
					this.exitNow = false;
				}
				catch
				{
				}
			}
		}

		private void panel_DoubleClick(object sender, EventArgs e)
		{
			if (!this.drawArea.Owner.PlansSelection)
			{
				this.selectedPlan = this.CastPanelToPlan((ThumbnailPanel)sender);
				if (this.selectedPlan == null)
				{
					return;
				}
				if (this.OnPlanLoad != null)
				{
					this.OnPlanLoad(this.selectedPlan);
				}
			}
		}

		private void panel_Enter(object sender, EventArgs e)
		{
			if (!this.drawArea.Owner.PlansSelection)
			{
				this.selectedPlan = this.CastPanelToPlan((ThumbnailPanel)sender);
				this.flowLayoutPanel.Invalidate();
				if (this.OnPlanSelected != null)
				{
					this.OnPlanSelected(this.selectedPlan);
				}
			}
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			if (!this.drawArea.Owner.PlansSelection)
			{
				try
				{
					ThumbnailPanel thumbnailPanel = (ThumbnailPanel)sender;
					thumbnailPanel.Selected = thumbnailPanel.Plan.Equals(this.selectedPlan);
				}
				catch
				{
				}
			}
		}

		private void ScrollUp(ScrollableControl control, bool isLargeChange)
		{
			try
			{
				int num;
				if (isLargeChange)
				{
					num = control.VerticalScroll.LargeChange;
				}
				else
				{
					num = control.VerticalScroll.SmallChange * 3;
				}
				int value = control.VerticalScroll.Value;
				if (value - num > control.VerticalScroll.Minimum)
				{
					control.VerticalScroll.Value -= num;
				}
				else
				{
					control.VerticalScroll.Value = control.VerticalScroll.Minimum;
				}
				control.PerformLayout();
			}
			catch
			{
			}
		}

		private void ScrollDown(ScrollableControl control, bool isLargeChange)
		{
			try
			{
				int num;
				if (isLargeChange)
				{
					num = control.VerticalScroll.LargeChange;
				}
				else
				{
					num = control.VerticalScroll.SmallChange * 3;
				}
				int value = control.VerticalScroll.Value;
				if (value + num < control.VerticalScroll.Maximum)
				{
					control.VerticalScroll.Value += num;
				}
				else
				{
					control.VerticalScroll.Value = control.VerticalScroll.Maximum;
				}
				control.PerformLayout();
			}
			catch
			{
			}
		}

		private void EnsureSelected()
		{
			if (this.selectedPlan != null && !this.FocusPanel(this.selectedPlan))
			{
				this.selectedPlan = null;
			}
			if (this.selectedPlan == null && this.flowLayoutPanel.Controls.Count > 0)
			{
				Utilities.SetObjectFocus(this.flowLayoutPanel.Controls[0]);
				this.selectedPlan = this.CastPanelToPlan(this.flowLayoutPanel.Controls[0]);
			}
			this.flowLayoutPanel.Refresh();
			if (this.OnPlanSelected != null)
			{
				this.OnPlanSelected(this.selectedPlan);
			}
		}

		private void SelectFirstElement()
		{
			if (this.flowLayoutPanel.Controls.Count > 0)
			{
				Utilities.SetObjectFocus(this.flowLayoutPanel.Controls[0]);
			}
		}

		private void SelectLastElement()
		{
			if (this.flowLayoutPanel.Controls.Count > 0)
			{
				Utilities.SetObjectFocus(this.flowLayoutPanel.Controls[this.flowLayoutPanel.Controls.Count - 1]);
			}
		}

		private void flowLayoutPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Prior:
						this.ScrollUp(this.flowLayoutPanel, true);
						e.IsInputKey = true;
						break;
					case Keys.Next:
						this.ScrollDown(this.flowLayoutPanel, true);
						e.IsInputKey = true;
						break;
					case Keys.End:
						this.SelectLastElement();
						e.IsInputKey = true;
						break;
					case Keys.Home:
						this.SelectFirstElement();
						e.IsInputKey = true;
						break;
					default:
						if (keyCode == Keys.Delete)
						{
							if (!this.drawArea.Owner.PlansSelection && this.SelectedPlan != null && this.OnPlanRemove != null)
							{
								this.OnPlanRemove(this.SelectedPlan);
							}
						}
						break;
					}
				}
				else if (!this.drawArea.Owner.PlansSelection && this.SelectedPlan != null)
				{
					if (Control.ModifierKeys == Keys.Control)
					{
						if (this.OnPlanLoad != null)
						{
							this.OnPlanLoad(this.SelectedPlan);
						}
					}
					else if (this.OnPlanEdit != null)
					{
						this.OnPlanEdit(this.SelectedPlan);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void ReorderPlans()
		{
			for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
			{
				ThumbnailPanel thumbnailPanel = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
				if (thumbnailPanel != null)
				{
					Plan plan = this.CastPanelToPlan(thumbnailPanel);
					if (plan != null)
					{
						plan.ReorderIndex = i;
					}
				}
			}
			this.project.ReorderPlans();
		}

		private void flowLayoutPanel_DragDrop(object sender, DragEventArgs e)
		{
			if (!this.wasReordered)
			{
				return;
			}
			this.ReorderPlans();
			if (this.OnPlanReordered != null)
			{
				this.OnPlanReordered(this.SelectedPlan);
			}
			this.wasReordered = false;
			this.dragPanel = null;
		}

		private void flowLayoutPanel_DragOver(object sender, DragEventArgs e)
		{
			try
			{
				Point pt = this.flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
				Control childAtPoint = this.flowLayoutPanel.GetChildAtPoint(pt);
				int childIndex = this.flowLayoutPanel.Controls.GetChildIndex(childAtPoint, false);
				if (childIndex != -1 && childIndex != this.dragIndex)
				{
					Console.WriteLine("index = " + childIndex);
					this.flowLayoutPanel.Controls.SetChildIndex(this.dragPanel, childIndex);
					this.flowLayoutPanel.Invalidate();
					this.dragIndex = childIndex;
					this.wasReordered = true;
				}
			}
			catch
			{
			}
		}

		private void flowLayoutPanel_DragEnter(object sender, DragEventArgs e)
		{
			this.dragIndex = -1;
			this.dragPanel = (ThumbnailPanel)e.Data.GetData(typeof(ThumbnailPanel));
			Point pt = this.flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
			Control childAtPoint = this.flowLayoutPanel.GetChildAtPoint(pt);
			this.dragOriginalIndex = this.flowLayoutPanel.Controls.GetChildIndex(childAtPoint, false);
			this.wasReordered = false;
			e.Effect = DragDropEffects.Move;
		}

		private void flowLayoutPanel_Resize(object sender, EventArgs e)
		{
			if (this.exitNow)
			{
				return;
			}
			this.exitNow = true;
			Size size = new Size(274, 230);
			int num = this.flowLayoutPanel.ClientSize.Width / size.Width;
			int count = this.flowLayoutPanel.Controls.Count;
			this.flowLayoutPanel.Padding = ((count <= num) ? new Padding((this.flowLayoutPanel.Width - size.Width * count) / 2, 12, 0, 0) : new Padding((this.flowLayoutPanel.Width - size.Width * num) / 2, 12, 0, 0));
			this.flowLayoutPanel.PerformLayout();
			this.exitNow = false;
		}

		[CompilerGenerated]
		private void <backgroundWorker_DoWork>b__6()
		{
			this.frmProgress.progressBar.Text = "100 %";
			this.frmProgress.progressBar.Value = 100;
		}

		public const int ThumbnailWidth = 256;

		public const int ThumbnailHeight = 192;

		private OnPlanSelectedHandler OnPlanSelected;

		private OnPlanSelectedHandler OnPlanLoad;

		private OnPlanSelectedHandler OnPlanEdit;

		private OnPlanSelectedHandler OnPlanRemove;

		private OnPlanSelectedHandler OnPlanReordered;

		private OnPlanActionHandler OnPlanApplyAction;

		private bool enabled;

		private bool multiSelectionMode;

		private bool exitNow;

		private Project project;

		private DrawingArea drawArea;

		private BackgroundWorker backgroundWorker;

		private FlowLayoutPanel flowLayoutPanel;

		private int dragIndex = -1;

		private int dragOriginalIndex = -1;

		private ThumbnailPanel dragPanel;

		private bool wasReordered;

		private Plan selectedPlan;

		private ProgressForm frmProgress;

		private Variables thumbnailsList;

		public enum ThumbnailsCreationEnum
		{
			CreateFromPlans,
			CreateFromImages,
			CreateFromPDFFiles,
			DuplicateFromProject
		}

		public enum PlansActionEnum
		{
			PlansActionPrint,
			PlansActionExport
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2
		{
			public <>c__DisplayClass2()
			{
			}

			public void <UpdateProgressDescriptions>b__0()
			{
				this.<>4__this.frmProgress.lblDescription.Text = this.description1;
				this.<>4__this.frmProgress.lblDescription2.Text = this.description2;
			}

			public PlansNavigator <>4__this;

			public string description1;

			public string description2;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass9
		{
			public <>c__DisplayClass9()
			{
			}

			public void <backgroundWorker_DoWork>b__5()
			{
				int num = Utilities.ConvertToInt((double)(this.i + 1) * (100.0 / (double)this.<>4__this.thumbnailsList.Count));
				this.<>4__this.frmProgress.progressBar.Text = num + " %";
				this.<>4__this.frmProgress.progressBar.Value = num;
				Console.WriteLine(num + " %");
			}

			public int i;

			public PlansNavigator <>4__this;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClassc
		{
			public <>c__DisplayClassc()
			{
			}

			public void <backgroundWorker_DoWork>b__4()
			{
				ThumbnailPanel thumbnailPanel = new ThumbnailPanel(this.CS$<>8__localsa.<>4__this.flowLayoutPanel);
				thumbnailPanel.DrawArea = this.CS$<>8__localsa.<>4__this.drawArea;
				thumbnailPanel.ThumbnailMarging = new Padding(10, 3, 0, 0);
				thumbnailPanel.ThumbnailPadding = new Padding(15, 38, 0, 0);
				thumbnailPanel.ThumbnailSize = new Size(256, 192);
				thumbnailPanel.RecalcLayout();
				thumbnailPanel.Plan = this.plan;
				this.CS$<>8__localsa.<>4__this.flowLayoutPanel.Controls.Add(thumbnailPanel);
				thumbnailPanel.Paint += this.CS$<>8__localsa.<>4__this.panel_Paint;
				thumbnailPanel.PreviewKeyDown += this.CS$<>8__localsa.<>4__this.flowLayoutPanel_PreviewKeyDown;
				thumbnailPanel.Enter += this.CS$<>8__localsa.<>4__this.panel_Enter;
				thumbnailPanel.Click += this.CS$<>8__localsa.<>4__this.panel_Click;
				thumbnailPanel.DoubleClick += this.CS$<>8__localsa.<>4__this.panel_DoubleClick;
				thumbnailPanel.AllowDrag = true;
				int num = this.CS$<>8__localsa.<>4__this.flowLayoutPanel.Width / thumbnailPanel.Width;
				int count = this.CS$<>8__localsa.<>4__this.flowLayoutPanel.Controls.Count;
				if (count <= num)
				{
					this.CS$<>8__localsa.<>4__this.flowLayoutPanel.Padding = new Padding((this.CS$<>8__localsa.<>4__this.flowLayoutPanel.Width - thumbnailPanel.Width * count) / 2, 12, 0, 0);
				}
			}

			public PlansNavigator.<>c__DisplayClass9 CS$<>8__localsa;

			public Plan plan;
		}
	}
}
