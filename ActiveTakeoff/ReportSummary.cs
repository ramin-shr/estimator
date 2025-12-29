using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ReportSummary
    {
        public string Caption
        {
            get;
            set;
        }

        public double CostSubTotal
        {
            get;
            set;
        }

        public int GroupID
        {
            get;
            private set;
        }

        public DrawObject GroupObject
        {
            get;
            private set;
        }

        public string LayerName
        {
            get;
            private set;
        }

        public Plan Plan
        {
            get;
            private set;
        }

        public double PriceSubTotal
        {
            get;
            set;
        }

        public ReportSummary(string caption, double costSubTotal, double priceSubTotal, Plan plan, DrawObject groupObject, int groupID, string layerName)
        {
            this.Caption = caption;
            this.CostSubTotal = costSubTotal;
            this.PriceSubTotal = priceSubTotal;
            this.Plan = plan;
            this.GroupObject = groupObject;
            this.GroupID = groupID;
            this.LayerName = layerName;
        }
    }
}