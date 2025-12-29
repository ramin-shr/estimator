using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class StatusArgs : EventArgs
    {
        public TA_TrialStatus Status
        {
            get;
            set;
        }

        public StatusArgs()
        {
        }
    }
}