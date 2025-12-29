using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class PresetField : ExtensionField
    {
        public object Value
        {
            get;
            set;
        }

        public PresetField(string name, object value)
        {
            base.Name = name;
            this.Value = value;
        }

        public new void Clear()
        {
            base.Clear();
            this.Value = 0;
        }

        public new void Dump()
        {
            base.Dump();
            Console.WriteLine(string.Concat("Value = ", this.Value));
        }
    }
}