using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class Report
	{
		public bool TakeoffShowProjectInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<TakeoffShowProjectInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TakeoffShowProjectInfo>k__BackingField = value;
			}
		}

		public bool TakeoffShowComments
		{
			[CompilerGenerated]
			get
			{
				return this.<TakeoffShowComments>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TakeoffShowComments>k__BackingField = value;
			}
		}

		public bool TakeoffShowInvisibleObjects
		{
			[CompilerGenerated]
			get
			{
				return this.<TakeoffShowInvisibleObjects>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TakeoffShowInvisibleObjects>k__BackingField = value;
			}
		}

		public bool TakeoffApplyFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<TakeoffApplyFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TakeoffApplyFilter>k__BackingField = value;
			}
		}

		public bool EstimatingShowProjectInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatingShowProjectInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EstimatingShowProjectInfo>k__BackingField = value;
			}
		}

		public bool EstimatingShowComments
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatingShowComments>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EstimatingShowComments>k__BackingField = value;
			}
		}

		public bool EstimatingShowInvisibleObjects
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatingShowInvisibleObjects>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EstimatingShowInvisibleObjects>k__BackingField = value;
			}
		}

		public bool EstimatingApplyFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatingApplyFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EstimatingApplyFilter>k__BackingField = value;
			}
		}

		public bool QuoteShowProjectInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<QuoteShowProjectInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QuoteShowProjectInfo>k__BackingField = value;
			}
		}

		public bool QuoteShowComments
		{
			[CompilerGenerated]
			get
			{
				return this.<QuoteShowComments>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QuoteShowComments>k__BackingField = value;
			}
		}

		public bool QuoteShowInvisibleObjects
		{
			[CompilerGenerated]
			get
			{
				return this.<QuoteShowInvisibleObjects>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QuoteShowInvisibleObjects>k__BackingField = value;
			}
		}

		public bool QuoteApplyFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<QuoteApplyFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QuoteApplyFilter>k__BackingField = value;
			}
		}

		public string OrderByObjectsFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<OrderByObjectsFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OrderByObjectsFilter>k__BackingField = value;
			}
		}

		public string OrderByPlansFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<OrderByPlansFilter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OrderByPlansFilter>k__BackingField = value;
			}
		}

		public Report.ReportOrderEnum Order
		{
			[CompilerGenerated]
			get
			{
				return this.<Order>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Order>k__BackingField = value;
			}
		}

		public Report.ReportSortByEnum ReportSortBy
		{
			[CompilerGenerated]
			get
			{
				return this.<ReportSortBy>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReportSortBy>k__BackingField = value;
			}
		}

		public Report.QuoteReportSortByEnum QuoteReportSortBy
		{
			[CompilerGenerated]
			get
			{
				return this.<QuoteReportSortBy>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QuoteReportSortBy>k__BackingField = value;
			}
		}

		public UnitScale.UnitSystem SystemType
		{
			[CompilerGenerated]
			get
			{
				return this.<SystemType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SystemType>k__BackingField = value;
			}
		}

		public UnitScale.UnitPrecision Precision
		{
			[CompilerGenerated]
			get
			{
				return this.<Precision>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Precision>k__BackingField = value;
			}
		}

		public ReportTypeEnum SelectedReportType
		{
			[CompilerGenerated]
			get
			{
				return this.<SelectedReportType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SelectedReportType>k__BackingField = value;
			}
		}

		public bool Dirty
		{
			[CompilerGenerated]
			get
			{
				return this.<Dirty>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Dirty>k__BackingField = value;
			}
		}

		public bool ShowProjectInfo
		{
			get
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					return this.EstimatingShowProjectInfo;
				}
				if (this.SelectedReportType != ReportTypeEnum.QuoteReport)
				{
					return this.TakeoffShowProjectInfo;
				}
				return this.QuoteShowProjectInfo;
			}
			set
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					this.EstimatingShowProjectInfo = value;
					return;
				}
				if (this.SelectedReportType == ReportTypeEnum.QuoteReport)
				{
					this.QuoteShowProjectInfo = value;
					return;
				}
				this.TakeoffShowProjectInfo = value;
			}
		}

		public bool ShowComments
		{
			get
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					return this.EstimatingShowComments;
				}
				if (this.SelectedReportType != ReportTypeEnum.QuoteReport)
				{
					return this.TakeoffShowComments;
				}
				return this.QuoteShowComments;
			}
			set
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					this.EstimatingShowComments = value;
					return;
				}
				if (this.SelectedReportType == ReportTypeEnum.QuoteReport)
				{
					this.QuoteShowComments = value;
					return;
				}
				this.TakeoffShowComments = value;
			}
		}

		public bool ShowInvisibleObjects
		{
			get
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					return this.EstimatingShowInvisibleObjects;
				}
				if (this.SelectedReportType != ReportTypeEnum.QuoteReport)
				{
					return this.TakeoffShowInvisibleObjects;
				}
				return this.QuoteShowInvisibleObjects;
			}
			set
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					this.EstimatingShowInvisibleObjects = value;
					return;
				}
				if (this.SelectedReportType == ReportTypeEnum.QuoteReport)
				{
					this.QuoteShowInvisibleObjects = value;
					return;
				}
				this.TakeoffShowInvisibleObjects = value;
			}
		}

		public bool ApplyFilter
		{
			get
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					return this.EstimatingApplyFilter;
				}
				if (this.SelectedReportType != ReportTypeEnum.QuoteReport)
				{
					return this.TakeoffApplyFilter;
				}
				return this.QuoteApplyFilter;
			}
			set
			{
				if (this.SelectedReportType == ReportTypeEnum.EstimatingByGroupsReport || this.SelectedReportType == ReportTypeEnum.EstimatingByPlansReport)
				{
					this.EstimatingApplyFilter = value;
					return;
				}
				if (this.SelectedReportType == ReportTypeEnum.QuoteReport)
				{
					this.QuoteApplyFilter = value;
					return;
				}
				this.TakeoffApplyFilter = value;
			}
		}

		public Report()
		{
			this.Clear();
		}

		public void Clear()
		{
			this.TakeoffShowProjectInfo = true;
			this.TakeoffShowComments = true;
			this.TakeoffShowInvisibleObjects = true;
			this.TakeoffApplyFilter = true;
			this.EstimatingShowProjectInfo = true;
			this.EstimatingShowComments = true;
			this.EstimatingShowInvisibleObjects = true;
			this.EstimatingApplyFilter = true;
			this.QuoteShowProjectInfo = true;
			this.QuoteShowComments = true;
			this.QuoteShowInvisibleObjects = true;
			this.QuoteApplyFilter = true;
			this.OrderByObjectsFilter = "";
			this.OrderByPlansFilter = "";
			this.Order = Report.ReportOrderEnum.ReportOrderByPlans;
			this.ReportSortBy = Report.ReportSortByEnum.ReportSortByPlans;
			this.QuoteReportSortBy = Report.QuoteReportSortByEnum.QuoteReportSortBySections;
			this.SystemType = UnitScale.UnitSystem.undefined;
			this.Precision = UnitScale.DefaultUnitPrecision();
			this.SelectedReportType = ReportTypeEnum.TakeoffByGroupsReport;
			this.Dirty = false;
		}

		[CompilerGenerated]
		private bool <TakeoffShowProjectInfo>k__BackingField;

		[CompilerGenerated]
		private bool <TakeoffShowComments>k__BackingField;

		[CompilerGenerated]
		private bool <TakeoffShowInvisibleObjects>k__BackingField;

		[CompilerGenerated]
		private bool <TakeoffApplyFilter>k__BackingField;

		[CompilerGenerated]
		private bool <EstimatingShowProjectInfo>k__BackingField;

		[CompilerGenerated]
		private bool <EstimatingShowComments>k__BackingField;

		[CompilerGenerated]
		private bool <EstimatingShowInvisibleObjects>k__BackingField;

		[CompilerGenerated]
		private bool <EstimatingApplyFilter>k__BackingField;

		[CompilerGenerated]
		private bool <QuoteShowProjectInfo>k__BackingField;

		[CompilerGenerated]
		private bool <QuoteShowComments>k__BackingField;

		[CompilerGenerated]
		private bool <QuoteShowInvisibleObjects>k__BackingField;

		[CompilerGenerated]
		private bool <QuoteApplyFilter>k__BackingField;

		[CompilerGenerated]
		private string <OrderByObjectsFilter>k__BackingField;

		[CompilerGenerated]
		private string <OrderByPlansFilter>k__BackingField;

		[CompilerGenerated]
		private Report.ReportOrderEnum <Order>k__BackingField;

		[CompilerGenerated]
		private Report.ReportSortByEnum <ReportSortBy>k__BackingField;

		[CompilerGenerated]
		private Report.QuoteReportSortByEnum <QuoteReportSortBy>k__BackingField;

		[CompilerGenerated]
		private UnitScale.UnitSystem <SystemType>k__BackingField;

		[CompilerGenerated]
		private UnitScale.UnitPrecision <Precision>k__BackingField;

		[CompilerGenerated]
		private ReportTypeEnum <SelectedReportType>k__BackingField;

		[CompilerGenerated]
		private bool <Dirty>k__BackingField;

		public enum ReportOrderEnum
		{
			ReportOrderByObjects,
			ReportOrderByPlans
		}

		public enum ReportSortByEnum
		{
			ReportSortByPlans,
			ReportSortByLayers
		}

		public enum QuoteReportSortByEnum
		{
			QuoteReportSortBySections,
			QuoteReportSortByTypes,
			QuoteReportSortByList
		}
	}
}
