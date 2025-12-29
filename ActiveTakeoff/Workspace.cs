using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Workspace
    {
        public Plan ActivePlan
        {
            get;
            set;
        }

        public Variables RecentPlans
        {
            get;
            private set;
        }

        public Workspace()
        {
            this.ActivePlan = null;
            this.RecentPlans = new Variables();
        }

        public void Clear()
        {
            this.ActivePlan = null;
            this.RecentPlans.Clear();
        }
    }
}