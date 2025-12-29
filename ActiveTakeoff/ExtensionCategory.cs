using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ExtensionCategory
    {
        public string Caption
        {
            get;
            set;
        }

        public int Division
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Extensions Templates
        {
            get;
            private set;
        }

        public ExtensionCategory()
        {
            this.Templates = new Extensions();
        }

        public ExtensionCategory(string name, string caption, int division)
        {
            this.Name = name;
            this.Caption = caption;
            this.Division = division;
            this.Templates = new Extensions();
        }

        public void Clear()
        {
            this.Name = "";
            this.Caption = "";
            this.Division = 0;
            this.Templates.Clear();
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("Caption = ", this.Caption));
            Console.WriteLine(string.Concat("Division = ", this.Division));
            this.Templates.Dump();
        }
    }
}