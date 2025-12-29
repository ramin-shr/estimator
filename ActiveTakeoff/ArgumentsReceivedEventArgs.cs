using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ArgumentsReceivedEventArgs : EventArgs
    {
        public string[] Args
        {
            get;
            set;
        }

        public ArgumentsReceivedEventArgs()
        {
        }
    }
}