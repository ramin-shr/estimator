using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Report
    {
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

        public bool Dirty
        {
            get;
            set;
        }

        public bool EstimatingApplyFilter
        {
            get;
            set;
        }

        public bool EstimatingShowComments
        {
            get;
            set;
        }

        public bool EstimatingShowInvisibleObjects
        {
            get;
            set;
        }

        public bool EstimatingShowProjectInfo
        {
            get;
            set;
        }

        public Report.ReportOrderEnum Order
        {
            get;
            set;
        }

        public string OrderByObjectsFilter
        {
            get;
            set;
        }

        public string OrderByPlansFilter
        {
            get;
            set;
        }

        public UnitScale.UnitPrecision Precision
        {
            get;
            set;
        }

        public bool QuoteApplyFilter
        {
            get;
            set;
        }

        public Report.QuoteReportSortByEnum QuoteReportSortBy
        {
            get;
            set;
        }

        public bool QuoteShowComments
        {
            get;
            set;
        }

        public bool QuoteShowInvisibleObjects
        {
            get;
            set;
        }

        public bool QuoteShowProjectInfo
        {
            get;
            set;
        }

        public Report.ReportSortByEnum ReportSortBy
        {
            get;
            set;
        }

        public ReportTypeEnum SelectedReportType
        {
            get;
            set;
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

        public UnitScale.UnitSystem SystemType
        {
            get;
            set;
        }

        public bool TakeoffApplyFilter
        {
            get;
            set;
        }

        public bool TakeoffShowComments
        {
            get;
            set;
        }

        public bool TakeoffShowInvisibleObjects
        {
            get;
            set;
        }

        public bool TakeoffShowProjectInfo
        {
            get;
            set;
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

        public enum QuoteReportSortByEnum
        {
            QuoteReportSortBySections,
            QuoteReportSortByTypes,
            QuoteReportSortByList
        }

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
    }
}