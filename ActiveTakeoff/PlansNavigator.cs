using DevComponents.DotNetBar.Controls;
using Foxit.PDF.Rasterizer;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace QuoterPlan
{
    public class PlansNavigator
    {
        public const int ThumbnailWidth = 0x100;

        public const int ThumbnailHeight = 192;

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

        [CompilerGenerated]
        // <backgroundWorker_DoWork>b__6
        private void u003cbackgroundWorker_DoWorku003eb__6()
        {
            this.frmProgress.progressBar.Text = "100 %";
            this.frmProgress.progressBar.Value = 100;
        }

        public void ActionApply(PlansNavigator.PlansActionEnum plansAction)
        {
            int num = 0;
            int num1 = this.SelectedCount();
            if (num1 > 0)
            {
                for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
                {
                    ThumbnailPanel item = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
                    if (item != null && item.Selected)
                    {
                        num++;
                        Plan plan = this.CastPanelToPlan(item);
                        if (plan != null && this.OnPlanApplyAction != null)
                        {
                            this.OnPlanApplyAction(plansAction, plan, num, num1);
                        }
                    }
                }
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            double num1;
            double num2;
            string str;
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            string str1 = "";
            PdfRasterizer pdfRasterizer = null;
            Application.DoEvents();
            Thread.Sleep(10);
            for (int i = 0; i < this.thumbnailsList.Count; i++)
            {
                try
                {
                    Plan item = null;
                    string str2 = "";
                    string name = "";
                    string directoryName = "";
                    string fileName = "";
                    string folderName = "";
                    string pageUniqueFileName = "";
                    switch ((PlansNavigator.ThumbnailsCreationEnum)this.thumbnailsList[i].Tag)
                    {
                        case PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans:
                            {
                                item = this.project.Plans[Utilities.ConvertToInt(this.thumbnailsList[i].Name)];
                                if (item == null)
                                {
                                    break;
                                }
                                this.UpdateProgressDescriptions(backgroundWorker, Resources.Chargement_de_la_miniature, item.FileName);
                                break;
                            }
                        case PlansNavigator.ThumbnailsCreationEnum.CreateFromImages:
                            {
                                name = this.thumbnailsList[i].Name;
                                str2 = Utilities.StripInvalidCharacters(Path.GetFileNameWithoutExtension(name), "");
                                directoryName = Utilities.GetDirectoryName(name);
                                fileName = Utilities.GetFileName(name, false);
                                this.UpdateProgressDescriptions(backgroundWorker, Resources.Importation_de, fileName);
                                folderName = this.project.FolderName;
                                Utilities.ValidateDirectory(folderName);
                                if (directoryName != folderName)
                                {
                                    pageUniqueFileName = PDFUtilities.GetPageUniqueFileName(Path.Combine(folderName, fileName));
                                    Utilities.FileCopy(name, pageUniqueFileName);
                                }
                                else
                                {
                                    pageUniqueFileName = name;
                                }
                                item = this.project.InsertPlan(str2, pageUniqueFileName, false, 0, 0);
                                item.Dirty = true;
                                break;
                            }
                        case PlansNavigator.ThumbnailsCreationEnum.CreateFromPDFFiles:
                            {
                                name = this.thumbnailsList[i].Name;
                                this.UpdateProgressDescriptions(backgroundWorker, Resources.Importation_de, Utilities.GetFileName(name, false));
                                if (this.frmProgress == null)
                                {
                                    break;
                                }
                                if (name != str1)
                                {
                                    if (pdfRasterizer != null)
                                    {
                                        pdfRasterizer = null;
                                        GC.Collect();
                                    }
                                    pdfRasterizer = PDFUtilities.OpenPDFFile(name);
                                    str1 = name;
                                }
                                if (this.frmProgress == null || pdfRasterizer == null)
                                {
                                    break;
                                }
                                int importDpi = Settings.Default.ImportDpi;
                                bool convertPDFToColor = !Settings.Default.ConvertPDFToColor;
                                int num3 = Utilities.ConvertToInt(this.thumbnailsList[i].Value);
                                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(name);
                                if (pdfRasterizer.Pages.Count > 1)
                                {
                                    int num4 = num3 + 1;
                                    str = string.Concat(" - ", num4.ToString());
                                }
                                else
                                {
                                    str = "";
                                }
                                str2 = Utilities.StripInvalidCharacters(string.Concat(fileNameWithoutExtension, str), "");
                                string str3 = string.Concat(str2, ".png");
                                folderName = this.project.FolderName;
                                Utilities.ValidateDirectory(folderName);
                                double num5 = Utilities.ComputeAvailableMemoryForImage();
                                pageUniqueFileName = PDFUtilities.GetPageUniqueFileName(Path.Combine(folderName, str3));
                                if (!PDFUtilities.GetPageDimension(str1, num3, out num1, out num2) || !PDFUtilities.ConvertPDFPageToImage(pdfRasterizer, num1, num2, num3, pageUniqueFileName, num5, importDpi, convertPDFToColor) || this.frmProgress == null)
                                {
                                    break;
                                }
                                item = this.project.InsertPlan(str2, pageUniqueFileName, false, 0, 0);
                                item.Dirty = true;
                                break;
                            }
                        case PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject:
                            {
                                Plan value = (Plan)this.thumbnailsList[i].Value;
                                bool flag = Utilities.ConvertToBoolean(this.thumbnailsList[i].Name, false);
                                if (value == null)
                                {
                                    break;
                                }
                                str2 = this.project.Plans.FindFreePlanName(value.Name);
                                this.UpdateProgressDescriptions(backgroundWorker, Resources.Duplication_en_cours, str2);
                                pageUniqueFileName = PDFUtilities.GetPageUniqueFileName(Path.Combine(value.FolderName, value.FileName));
                                Utilities.FileCopy(value.FullFileName, pageUniqueFileName);
                                item = this.project.InsertPlan(str2, pageUniqueFileName, false, value.Brightness, value.Contrast);
                                item.UnitScale = value.UnitScale.Duplicate();
                                if (flag)
                                {
                                    item.Layers = value.Layers.Duplicate();
                                }
                                foreach (Layer collection in item.Layers.Collection)
                                {
                                    foreach (DrawObject drawObject in collection.DrawingObjects.Collection)
                                    {
                                        if (drawObject.GetType().Name != "DrawLegend")
                                        {
                                            continue;
                                        }
                                        ((DrawLegend)drawObject).Plan = item;
                                    }
                                }
                                item.Dirty = true;
                                break;
                            }
                    }
                    if (item != null)
                    {
                        if (!item.Thumbnail.IsValid())
                        {
                            item.CreateThumbnail(false);
                        }
                        this.flowLayoutPanel.Invoke(new MethodInvoker(() => {
                            ThumbnailPanel thumbnailPanel = new ThumbnailPanel(this.flowLayoutPanel)
                            {
                                DrawArea = this.drawArea,
                                ThumbnailMarging = new Padding(10, 3, 0, 0),
                                ThumbnailPadding = new Padding(15, 38, 0, 0),
                                ThumbnailSize = new Size(0x100, 192)
                            };
                            thumbnailPanel.RecalcLayout();
                            thumbnailPanel.Plan = item;
                            this.flowLayoutPanel.Controls.Add(thumbnailPanel);
                            thumbnailPanel.Paint += new PaintEventHandler(this.panel_Paint);
                            thumbnailPanel.PreviewKeyDown += new PreviewKeyDownEventHandler(this.flowLayoutPanel_PreviewKeyDown);
                            thumbnailPanel.Enter += new EventHandler(this.panel_Enter);
                            thumbnailPanel.Click += new EventHandler(this.panel_Click);
                            thumbnailPanel.DoubleClick += new EventHandler(this.panel_DoubleClick);
                            thumbnailPanel.AllowDrag = true;
                            int width = this.flowLayoutPanel.Width / thumbnailPanel.Width;
                            int count = this.flowLayoutPanel.Controls.Count;
                            if (count <= width)
                            {
                                this.flowLayoutPanel.Padding = new Padding((this.flowLayoutPanel.Width - thumbnailPanel.Width * count) / 2, 12, 0, 0);
                            }
                        }));
                    }
                    if (this.frmProgress == null)
                    {
                        this.CancelThread(backgroundWorker);
                        break;
                    }
                    else if (this.frmProgress.IsHandleCreated)
                    {
                        this.frmProgress.progressBar.Invoke(new MethodInvoker(() => {
                            int num = Utilities.ConvertToInt((double)(i + 1) * (100 / (double)this.thumbnailsList.Count));
                            this.frmProgress.progressBar.Text = string.Concat(num, " %");
                            this.frmProgress.progressBar.Value = num;
                            Console.WriteLine(string.Concat(num, " %"));
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
                this.frmProgress.progressBar.Invoke(new MethodInvoker(() => {
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

        private void BackgroundWorkerInitialize()
        {
            this.backgroundWorker.WorkerReportsProgress = false;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
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

        private Plan CastPanelToPlan(object panel)
        {
            Plan plan;
            try
            {
                plan = ((ThumbnailPanel)panel).Plan;
            }
            catch
            {
                plan = null;
            }
            return plan;
        }

        public void Clear()
        {
            for (int i = this.flowLayoutPanel.Controls.Count - 1; i >= 0; i--)
            {
                Control item = this.flowLayoutPanel.Controls[i];
                this.flowLayoutPanel.Controls.RemoveAt(i);
                item.Dispose();
            }
            this.flowLayoutPanel.Controls.Clear();
            this.selectedPlan = null;
        }

        public void CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum thumbnailsCreationContext)
        {
            try
            {
                string chargementDeLaMiniature = "";
                switch (thumbnailsCreationContext)
                {
                    case PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans:
                        {
                            chargementDeLaMiniature = Resources.Chargement_de_la_miniature;
                            break;
                        }
                    case PlansNavigator.ThumbnailsCreationEnum.CreateFromImages:
                    case PlansNavigator.ThumbnailsCreationEnum.CreateFromPDFFiles:
                        {
                            chargementDeLaMiniature = Resources.Importation_de;
                            break;
                        }
                    case PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject:
                        {
                            chargementDeLaMiniature = Resources.Duplication_en_cours;
                            break;
                        }
                }
                this.frmProgress = new ProgressForm(chargementDeLaMiniature, "", thumbnailsCreationContext != PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans);
                this.backgroundWorker.RunWorkerAsync();
                this.frmProgress.ShowDialog();
                this.frmProgress = null;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
        }

        public void DuplicateFromProject(Plan plan, int numberOfCopies, bool copyObjects)
        {
            this.thumbnailsList.Clear();
            for (int i = 0; i < numberOfCopies; i++)
            {
                this.thumbnailsList.Add(new Variable(copyObjects.ToString(), plan, (object)PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject));
            }
            if (this.thumbnailsList.Count > 0)
            {
                this.CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum.DuplicateFromProject);
                this.RecalcPanelsTabIndex();
            }
            this.EnsureSelected();
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

        private ThumbnailPanel FindPanel(Plan plan)
        {
            for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
            {
                Plan plan1 = this.CastPanelToPlan((ThumbnailPanel)this.flowLayoutPanel.Controls[i]);
                if (plan1 != null && plan.Equals(plan1))
                {
                    return (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
                }
            }
            return null;
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

        private void flowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            this.dragIndex = -1;
            this.dragPanel = (ThumbnailPanel)e.Data.GetData(typeof(ThumbnailPanel));
            Point client = this.flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
            Control childAtPoint = this.flowLayoutPanel.GetChildAtPoint(client);
            this.dragOriginalIndex = this.flowLayoutPanel.Controls.GetChildIndex(childAtPoint, false);
            this.wasReordered = false;
            e.Effect = DragDropEffects.Move;
        }

        private void flowLayoutPanel_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                Point client = this.flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
                Control childAtPoint = this.flowLayoutPanel.GetChildAtPoint(client);
                int childIndex = this.flowLayoutPanel.Controls.GetChildIndex(childAtPoint, false);
                if (childIndex != -1 && childIndex != this.dragIndex)
                {
                    Console.WriteLine(string.Concat("index = ", childIndex));
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
                            {
                                this.ScrollUp(this.flowLayoutPanel, true);
                                e.IsInputKey = true;
                                break;
                            }
                        case Keys.Next:
                            {
                                this.ScrollDown(this.flowLayoutPanel, true);
                                e.IsInputKey = true;
                                break;
                            }
                        case Keys.End:
                            {
                                this.SelectLastElement();
                                e.IsInputKey = true;
                                break;
                            }
                        case Keys.Home:
                            {
                                this.SelectFirstElement();
                                e.IsInputKey = true;
                                break;
                            }
                        default:
                            {
                                if (keyCode == Keys.Delete)
                                {
                                    if (this.drawArea.Owner.PlansSelection || this.SelectedPlan == null || this.OnPlanRemove == null)
                                    {
                                        break;
                                    }
                                    this.OnPlanRemove(this.SelectedPlan);
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
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
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            if (this.exitNow)
            {
                return;
            }
            this.exitNow = true;
            Size size = new Size(0x112, 230);
            int width = this.flowLayoutPanel.ClientSize.Width / size.Width;
            int count = this.flowLayoutPanel.Controls.Count;
            this.flowLayoutPanel.Padding = (count <= width ? new Padding((this.flowLayoutPanel.Width - size.Width * count) / 2, 12, 0, 0) : new Padding((this.flowLayoutPanel.Width - size.Width * width) / 2, 12, 0, 0));
            this.flowLayoutPanel.PerformLayout();
            this.exitNow = false;
        }

        private void FlowLayoutPanelInitialize()
        {
            this.flowLayoutPanel.Margin = new Padding(0, 0, 0, 0);
            this.flowLayoutPanel.Resize += new EventHandler(this.flowLayoutPanel_Resize);
            this.flowLayoutPanel.PreviewKeyDown += new PreviewKeyDownEventHandler(this.flowLayoutPanel_PreviewKeyDown);
            this.flowLayoutPanel.DragEnter += new DragEventHandler(this.flowLayoutPanel_DragEnter);
            this.flowLayoutPanel.DragOver += new DragEventHandler(this.flowLayoutPanel_DragOver);
            this.flowLayoutPanel.DragDrop += new DragEventHandler(this.flowLayoutPanel_DragDrop);
        }

        private bool FocusPanel(Plan plan)
        {
            for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
            {
                Plan plan1 = this.CastPanelToPlan((ThumbnailPanel)this.flowLayoutPanel.Controls[i]);
                if (plan1 != null && plan.Equals(plan1))
                {
                    Utilities.SetObjectFocus(this.flowLayoutPanel.Controls[i]);
                    return true;
                }
            }
            return false;
        }

        public void ImportImageFiles(string[] files)
        {
            try
            {
                Array.Sort(files, new NumericComparer());
            }
            catch
            {
            }
            this.thumbnailsList.Clear();
            string[] strArrays = files;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str = strArrays[i];
                this.thumbnailsList.Add(new Variable(str, (object)0, (object)PlansNavigator.ThumbnailsCreationEnum.CreateFromImages));
            }
            if (this.thumbnailsList.Count > 0)
            {
                this.CreateThumbnails(PlansNavigator.ThumbnailsCreationEnum.CreateFromImages);
                this.RecalcPanelsTabIndex();
            }
            this.EnsureSelected();
        }

        public void ImportPDFFiles(string[] files)
        {
            try
            {
                Array.Sort(files, new NumericComparer());
            }
            catch
            {
            }
            this.thumbnailsList.Clear();
            string[] strArrays = files;
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str = strArrays[i];
                int num = PDFUtilities.PagesCount(str);
                Variables variable = new Variables();
                for (int j = 0; j < num; j++)
                {
                    variable.Add(new Variable(j.ToString(), true));
                }
                if (num > 1)
                {
                    this.SelectPDFPages(str, variable);
                }
                for (int k = 0; k < variable.Count; k++)
                {
                    if (Utilities.ConvertToBoolean(variable[k].Value, true))
                    {
                        this.thumbnailsList.Add(new Variable(str, (object)k, (object)PlansNavigator.ThumbnailsCreationEnum.CreateFromPDFFiles));
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

        private void InitMultiSelectMode(bool enable)
        {
            for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
            {
                ThumbnailPanel item = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
                if (item != null)
                {
                    item.Selected = enable;
                    item.AllowDrag = !enable;
                    item.EnableDoubleClick(!enable);
                }
            }
            this.flowLayoutPanel.Refresh();
        }

        private void LoadResources()
        {
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
                    ThumbnailPanel selected = (ThumbnailPanel)sender;
                    selected.Selected = !selected.Selected;
                    selected.Invalidate();
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

        private bool PlanExists(Plan plan)
        {
            for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
            {
                Plan plan1 = this.CastPanelToPlan((ThumbnailPanel)this.flowLayoutPanel.Controls[i]);
                if (plan1 != null && plan.Equals(plan1))
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
                int num = i + 1;
                this.flowLayoutPanel.Controls[i].Name = string.Concat("panelThumbnail", num.ToString());
                this.flowLayoutPanel.Controls[i].TabIndex = i;
            }
        }

        public void Refresh()
        {
            this.flowLayoutPanel.Refresh();
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

        private void ReorderPlans()
        {
            for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
            {
                ThumbnailPanel item = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
                if (item != null)
                {
                    Plan plan = this.CastPanelToPlan(item);
                    if (plan != null)
                    {
                        plan.ReorderIndex = i;
                    }
                }
            }
            this.project.ReorderPlans();
        }

        private void ScrollDown(ScrollableControl control, bool isLargeChange)
        {
            int num;
            try
            {
                num = (!isLargeChange ? control.VerticalScroll.SmallChange * 3 : control.VerticalScroll.LargeChange);
                if (control.VerticalScroll.Value + num >= control.VerticalScroll.Maximum)
                {
                    control.VerticalScroll.Value = control.VerticalScroll.Maximum;
                }
                else
                {
                    VScrollProperties verticalScroll = control.VerticalScroll;
                    verticalScroll.Value = verticalScroll.Value + num;
                }
                control.PerformLayout();
            }
            catch
            {
            }
        }

        private void ScrollUp(ScrollableControl control, bool isLargeChange)
        {
            int num;
            try
            {
                num = (!isLargeChange ? control.VerticalScroll.SmallChange * 3 : control.VerticalScroll.LargeChange);
                if (control.VerticalScroll.Value - num <= control.VerticalScroll.Minimum)
                {
                    control.VerticalScroll.Value = control.VerticalScroll.Minimum;
                }
                else
                {
                    VScrollProperties verticalScroll = control.VerticalScroll;
                    verticalScroll.Value = verticalScroll.Value - num;
                }
                control.PerformLayout();
            }
            catch
            {
            }
        }

        public void SelectAll(bool select)
        {
            for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
            {
                ThumbnailPanel item = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
                if (item != null)
                {
                    item.Selected = select;
                }
            }
            this.flowLayoutPanel.Refresh();
        }

        public int SelectedCount()
        {
            int num = 0;
            for (int i = 0; i < this.flowLayoutPanel.Controls.Count; i++)
            {
                ThumbnailPanel item = (ThumbnailPanel)this.flowLayoutPanel.Controls[i];
                if (item != null && item.Selected)
                {
                    num++;
                }
            }
            return num;
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

        private void SelectPDFPages(string fileName, Variables pages)
        {
            try
            {
                using (PDFSelectionForm pDFSelectionForm = new PDFSelectionForm(fileName, pages))
                {
                    pDFSelectionForm.HelpUtilities = this.drawArea.Owner.HelpUtilities;
                    pDFSelectionForm.HelpContextString = "PDFSelectionForm";
                    pDFSelectionForm.ShowDialog(this.drawArea.Owner);
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
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

        private void UpdateProgressDescriptions(BackgroundWorker worker, string description1, string description2)
        {
            if (this.frmProgress == null)
            {
                this.CancelThread(worker);
                return;
            }
            if (this.frmProgress.IsHandleCreated)
            {
                this.frmProgress.progressBar.Invoke(new MethodInvoker(() => {
                    this.frmProgress.lblDescription.Text = description1;
                    this.frmProgress.lblDescription2.Text = description2;
                }));
            }
            Application.DoEvents();
        }

        public void ValidateThumbnails()
        {
            this.thumbnailsList.Clear();
            for (int i = 0; i < this.project.Plans.Count; i++)
            {
                if (!this.PlanExists(this.project.Plans[i]))
                {
                    this.thumbnailsList.Add(new Variable(i.ToString(), (object)i, (object)PlansNavigator.ThumbnailsCreationEnum.CreateFromPlans));
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

        public event OnPlanActionHandler OnPlanApplyAction;

        public event OnPlanSelectedHandler OnPlanEdit;

        public event OnPlanSelectedHandler OnPlanLoad;

        public event OnPlanSelectedHandler OnPlanRemove;

        public event OnPlanSelectedHandler OnPlanReordered;

        public event OnPlanSelectedHandler OnPlanSelected;

        [CompilerGenerated]
        // <>c__DisplayClass2
        private sealed class u003cu003ec__DisplayClass2
        {
            // <>4__this
            public PlansNavigator u003cu003e4__this;

            public string description1;

            public string description2;

            public u003cu003ec__DisplayClass2()
            {
            }

            // <UpdateProgressDescriptions>b__0
            public void u003cUpdateProgressDescriptionsu003eb__0()
            {
                this.u003cu003e4__this.frmProgress.lblDescription.Text = this.description1;
                this.u003cu003e4__this.frmProgress.lblDescription2.Text = this.description2;
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClass9
        private sealed class u003cu003ec__DisplayClass9
        {
            public int i;

            // <>4__this
            public PlansNavigator u003cu003e4__this;

            public u003cu003ec__DisplayClass9()
            {
            }

            // <backgroundWorker_DoWork>b__5
            public void u003cbackgroundWorker_DoWorku003eb__5()
            {
                int num = Utilities.ConvertToInt((double)(this.i + 1) * (100 / (double)this.u003cu003e4__this.thumbnailsList.Count));
                this.u003cu003e4__this.frmProgress.progressBar.Text = string.Concat(num, " %");
                this.u003cu003e4__this.frmProgress.progressBar.Value = num;
                Console.WriteLine(string.Concat(num, " %"));
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClassc
        private sealed class u003cu003ec__DisplayClassc
        {
            // CS$<>8__localsa
            public PlansNavigator.u003cu003ec__DisplayClass9 CSu0024u003cu003e8__localsa;

            public Plan plan;

            public u003cu003ec__DisplayClassc()
            {
            }

            // <backgroundWorker_DoWork>b__4
            public void u003cbackgroundWorker_DoWorku003eb__4()
            {
                ThumbnailPanel thumbnailPanel = new ThumbnailPanel(this.CSu0024u003cu003e8__localsa.u003cu003e4__this.flowLayoutPanel)
                {
                    DrawArea = this.CSu0024u003cu003e8__localsa.u003cu003e4__this.drawArea,
                    ThumbnailMarging = new Padding(10, 3, 0, 0),
                    ThumbnailPadding = new Padding(15, 38, 0, 0),
                    ThumbnailSize = new Size(0x100, 192)
                };
                thumbnailPanel.RecalcLayout();
                thumbnailPanel.Plan = this.plan;
                this.CSu0024u003cu003e8__localsa.u003cu003e4__this.flowLayoutPanel.Controls.Add(thumbnailPanel);
                thumbnailPanel.Paint += new PaintEventHandler(this.CSu0024u003cu003e8__localsa.u003cu003e4__this.panel_Paint);
                thumbnailPanel.PreviewKeyDown += new PreviewKeyDownEventHandler(this.CSu0024u003cu003e8__localsa.u003cu003e4__this.flowLayoutPanel_PreviewKeyDown);
                thumbnailPanel.Enter += new EventHandler(this.CSu0024u003cu003e8__localsa.u003cu003e4__this.panel_Enter);
                thumbnailPanel.Click += new EventHandler(this.CSu0024u003cu003e8__localsa.u003cu003e4__this.panel_Click);
                thumbnailPanel.DoubleClick += new EventHandler(this.CSu0024u003cu003e8__localsa.u003cu003e4__this.panel_DoubleClick);
                thumbnailPanel.AllowDrag = true;
                int width = this.CSu0024u003cu003e8__localsa.u003cu003e4__this.flowLayoutPanel.Width / thumbnailPanel.Width;
                int count = this.CSu0024u003cu003e8__localsa.u003cu003e4__this.flowLayoutPanel.Controls.Count;
                if (count <= width)
                {
                    this.CSu0024u003cu003e8__localsa.u003cu003e4__this.flowLayoutPanel.Padding = new Padding((this.CSu0024u003cu003e8__localsa.u003cu003e4__this.flowLayoutPanel.Width - thumbnailPanel.Width * count) / 2, 12, 0, 0);
                }
            }
        }

        public enum PlansActionEnum
        {
            PlansActionPrint,
            PlansActionExport
        }

        public enum ThumbnailsCreationEnum
        {
            CreateFromPlans,
            CreateFromImages,
            CreateFromPDFFiles,
            DuplicateFromProject
        }
    }
}