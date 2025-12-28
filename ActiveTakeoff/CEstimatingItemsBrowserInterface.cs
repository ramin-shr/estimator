using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;

namespace QuoterPlan
{
	public class CEstimatingItemsBrowserInterface
	{
		public event OnSelectDatabaseHandler OnSelectDatabase
		{
			add
			{
				OnSelectDatabaseHandler onSelectDatabaseHandler = this.OnSelectDatabase;
				OnSelectDatabaseHandler onSelectDatabaseHandler2;
				do
				{
					onSelectDatabaseHandler2 = onSelectDatabaseHandler;
					OnSelectDatabaseHandler value2 = (OnSelectDatabaseHandler)Delegate.Combine(onSelectDatabaseHandler2, value);
					onSelectDatabaseHandler = Interlocked.CompareExchange<OnSelectDatabaseHandler>(ref this.OnSelectDatabase, value2, onSelectDatabaseHandler2);
				}
				while (onSelectDatabaseHandler != onSelectDatabaseHandler2);
			}
			remove
			{
				OnSelectDatabaseHandler onSelectDatabaseHandler = this.OnSelectDatabase;
				OnSelectDatabaseHandler onSelectDatabaseHandler2;
				do
				{
					onSelectDatabaseHandler2 = onSelectDatabaseHandler;
					OnSelectDatabaseHandler value2 = (OnSelectDatabaseHandler)Delegate.Remove(onSelectDatabaseHandler2, value);
					onSelectDatabaseHandler = Interlocked.CompareExchange<OnSelectDatabaseHandler>(ref this.OnSelectDatabase, value2, onSelectDatabaseHandler2);
				}
				while (onSelectDatabaseHandler != onSelectDatabaseHandler2);
			}
		}

		public event OnLoadEstimatingSectionsHandler OnLoadEstimatingSections
		{
			add
			{
				OnLoadEstimatingSectionsHandler onLoadEstimatingSectionsHandler = this.OnLoadEstimatingSections;
				OnLoadEstimatingSectionsHandler onLoadEstimatingSectionsHandler2;
				do
				{
					onLoadEstimatingSectionsHandler2 = onLoadEstimatingSectionsHandler;
					OnLoadEstimatingSectionsHandler value2 = (OnLoadEstimatingSectionsHandler)Delegate.Combine(onLoadEstimatingSectionsHandler2, value);
					onLoadEstimatingSectionsHandler = Interlocked.CompareExchange<OnLoadEstimatingSectionsHandler>(ref this.OnLoadEstimatingSections, value2, onLoadEstimatingSectionsHandler2);
				}
				while (onLoadEstimatingSectionsHandler != onLoadEstimatingSectionsHandler2);
			}
			remove
			{
				OnLoadEstimatingSectionsHandler onLoadEstimatingSectionsHandler = this.OnLoadEstimatingSections;
				OnLoadEstimatingSectionsHandler onLoadEstimatingSectionsHandler2;
				do
				{
					onLoadEstimatingSectionsHandler2 = onLoadEstimatingSectionsHandler;
					OnLoadEstimatingSectionsHandler value2 = (OnLoadEstimatingSectionsHandler)Delegate.Remove(onLoadEstimatingSectionsHandler2, value);
					onLoadEstimatingSectionsHandler = Interlocked.CompareExchange<OnLoadEstimatingSectionsHandler>(ref this.OnLoadEstimatingSections, value2, onLoadEstimatingSectionsHandler2);
				}
				while (onLoadEstimatingSectionsHandler != onLoadEstimatingSectionsHandler2);
			}
		}

		public event OnLoadEstimatingItemsHandler OnLoadEstimatingItems
		{
			add
			{
				OnLoadEstimatingItemsHandler onLoadEstimatingItemsHandler = this.OnLoadEstimatingItems;
				OnLoadEstimatingItemsHandler onLoadEstimatingItemsHandler2;
				do
				{
					onLoadEstimatingItemsHandler2 = onLoadEstimatingItemsHandler;
					OnLoadEstimatingItemsHandler value2 = (OnLoadEstimatingItemsHandler)Delegate.Combine(onLoadEstimatingItemsHandler2, value);
					onLoadEstimatingItemsHandler = Interlocked.CompareExchange<OnLoadEstimatingItemsHandler>(ref this.OnLoadEstimatingItems, value2, onLoadEstimatingItemsHandler2);
				}
				while (onLoadEstimatingItemsHandler != onLoadEstimatingItemsHandler2);
			}
			remove
			{
				OnLoadEstimatingItemsHandler onLoadEstimatingItemsHandler = this.OnLoadEstimatingItems;
				OnLoadEstimatingItemsHandler onLoadEstimatingItemsHandler2;
				do
				{
					onLoadEstimatingItemsHandler2 = onLoadEstimatingItemsHandler;
					OnLoadEstimatingItemsHandler value2 = (OnLoadEstimatingItemsHandler)Delegate.Remove(onLoadEstimatingItemsHandler2, value);
					onLoadEstimatingItemsHandler = Interlocked.CompareExchange<OnLoadEstimatingItemsHandler>(ref this.OnLoadEstimatingItems, value2, onLoadEstimatingItemsHandler2);
				}
				while (onLoadEstimatingItemsHandler != onLoadEstimatingItemsHandler2);
			}
		}

		public event OnGetDefaultFormulaHandler OnGetDefaultFormula
		{
			add
			{
				OnGetDefaultFormulaHandler onGetDefaultFormulaHandler = this.OnGetDefaultFormula;
				OnGetDefaultFormulaHandler onGetDefaultFormulaHandler2;
				do
				{
					onGetDefaultFormulaHandler2 = onGetDefaultFormulaHandler;
					OnGetDefaultFormulaHandler value2 = (OnGetDefaultFormulaHandler)Delegate.Combine(onGetDefaultFormulaHandler2, value);
					onGetDefaultFormulaHandler = Interlocked.CompareExchange<OnGetDefaultFormulaHandler>(ref this.OnGetDefaultFormula, value2, onGetDefaultFormulaHandler2);
				}
				while (onGetDefaultFormulaHandler != onGetDefaultFormulaHandler2);
			}
			remove
			{
				OnGetDefaultFormulaHandler onGetDefaultFormulaHandler = this.OnGetDefaultFormula;
				OnGetDefaultFormulaHandler onGetDefaultFormulaHandler2;
				do
				{
					onGetDefaultFormulaHandler2 = onGetDefaultFormulaHandler;
					OnGetDefaultFormulaHandler value2 = (OnGetDefaultFormulaHandler)Delegate.Remove(onGetDefaultFormulaHandler2, value);
					onGetDefaultFormulaHandler = Interlocked.CompareExchange<OnGetDefaultFormulaHandler>(ref this.OnGetDefaultFormula, value2, onGetDefaultFormulaHandler2);
				}
				while (onGetDefaultFormulaHandler != onGetDefaultFormulaHandler2);
			}
		}

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
			this.frmCOfficeDataBrowser.OnSelectDatabase += this.frmCOfficeDataBrowser_OnSelectDatabase;
			this.frmCOfficeDataBrowser.OnLoadEstimatingSections += this.frmCOfficeDataBrowser_OnLoadEstimatingSections;
			this.frmCOfficeDataBrowser.OnLoadEstimatingItems += this.frmCOfficeDataBrowser_OnLoadEstimatingItems;
			this.frmCOfficeDataBrowser.EnableCOCommercialDatabase(enableCOCommercialDatabase);
		}

		public void ShowBrowser(IWin32Window parentForm)
		{
			this.frmCOfficeDataBrowser.RefreshSections(false);
			this.frmCOfficeDataBrowser.ShowDialog(parentForm);
		}

		public void GetDefaultFormula(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
		{
			if (this.OnGetDefaultFormula != null)
			{
				this.OnGetDefaultFormula(drawObject, stats, product, coUnit);
			}
		}

		public void frmCOfficeDataBrowser_OnSelectDatabase(string databaseName, ref bool cancel)
		{
			if (this.OnSelectDatabase != null)
			{
				this.OnSelectDatabase(databaseName, ref cancel);
			}
		}

		public void frmCOfficeDataBrowser_OnLoadEstimatingSections(ref TreeViewNodes sections)
		{
			if (this.OnLoadEstimatingSections != null)
			{
				this.OnLoadEstimatingSections(ref sections);
			}
		}

		public void frmCOfficeDataBrowser_OnLoadEstimatingItems(CEstimatingSection section, ref CEstimatingItems products)
		{
			if (this.OnLoadEstimatingItems != null)
			{
				this.OnLoadEstimatingItems(section, ref products);
			}
		}

		private OnSelectDatabaseHandler OnSelectDatabase;

		private OnLoadEstimatingSectionsHandler OnLoadEstimatingSections;

		private OnLoadEstimatingItemsHandler OnLoadEstimatingItems;

		private OnGetDefaultFormulaHandler OnGetDefaultFormula;

		private CEstimatingItemsBrowserForm frmCOfficeDataBrowser;
	}
}
