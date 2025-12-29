using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Variable
    {
        public string Name
        {
            get;
            private set;
        }

        public object Tag
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        public Variable(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public Variable(string name, object value, object tag)
        {
            this.Name = name;
            this.Value = value;
            this.Tag = tag;
        }

        public void Clear()
        {
            this.Name = "";
            this.Value = null;
            this.Tag = null;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            if (this.Value != null)
            {
                Console.WriteLine(string.Concat("Value = ", this.Value));
            }
        }
    }
}