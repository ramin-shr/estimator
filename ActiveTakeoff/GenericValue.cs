using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class GenericValue
    {
        public bool Dirty
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        public GenericValue(object value)
        {
            this.Value = value;
            this.Dirty = false;
        }
    }
}