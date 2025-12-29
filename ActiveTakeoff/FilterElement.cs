using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class FilterElement
    {
        public int GroupID
        {
            get;
            private set;
        }

        public string LayerName
        {
            get;
            set;
        }

        public string PlanName
        {
            get;
            set;
        }

        public FilterElement(string planName, string layerName, int groupID)
        {
            this.PlanName = planName;
            this.LayerName = layerName;
            this.GroupID = groupID;
        }
    }
}