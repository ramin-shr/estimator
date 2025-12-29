using DevExpress.Utils;
using System;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class CEstimatingItemsBrowserInterface
    {
        private CEstimatingItemsBrowserForm frmCOfficeDataBrowser;

        public CEstimatingItemsBrowserForm Browser
        {
            get
            {
                return this.frmCOfficeDataBrowser;
            }
        }

        public CEstimatingItemsBrowserInterface(ImageCollection imageCollection, bool enableCOCommercialDatabase)
        {
            this.frmCOfficeDataBrowser = new CEstimatingItemsBrowserForm(imageCollection);
            this.frmCOfficeDataBrowser.OnSelectDatabase += new OnSelectDatabaseHandler(this.frmCOfficeDataBrowser_OnSelectDatabase);
            this.frmCOfficeDataBrowser.OnLoadEstimatingSections += new OnLoadEstimatingSectionsHandler(this.frmCOfficeDataBrowser_OnLoadEstimatingSections);
            this.frmCOfficeDataBrowser.OnLoadEstimatingItems += new OnLoadEstimatingItemsHandler(this.frmCOfficeDataBrowser_OnLoadEstimatingItems);
            this.frmCOfficeDataBrowser.EnableCOCommercialDatabase(enableCOCommercialDatabase);
        }

        public void frmCOfficeDataBrowser_OnLoadEstimatingItems(CEstimatingSection section, ref CEstimatingItems products)
        {
            if (this.OnLoadEstimatingItems != null)
            {
                this.OnLoadEstimatingItems(section, ref products);
            }
        }

        public void frmCOfficeDataBrowser_OnLoadEstimatingSections(ref TreeViewNodes sections)
        {
            if (this.OnLoadEstimatingSections != null)
            {
                this.OnLoadEstimatingSections(ref sections);
            }
        }

        public void frmCOfficeDataBrowser_OnSelectDatabase(string databaseName, ref bool cancel)
        {
            if (this.OnSelectDatabase != null)
            {
                this.OnSelectDatabase(databaseName, ref cancel);
            }
        }

        public void GetDefaultFormula(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
        {
            if (this.OnGetDefaultFormula != null)
            {
                this.OnGetDefaultFormula(drawObject, stats, product, coUnit);
            }
        }

        public void ShowBrowser(IWin32Window parentForm)
        {
            this.frmCOfficeDataBrowser.RefreshSections(false);
            this.frmCOfficeDataBrowser.ShowDialog(parentForm);
        }

        public event OnGetDefaultFormulaHandler OnGetDefaultFormula;

        public event OnLoadEstimatingItemsHandler OnLoadEstimatingItems;

        public event OnLoadEstimatingSectionsHandler OnLoadEstimatingSections;

        public event OnSelectDatabaseHandler OnSelectDatabase;
    }
}