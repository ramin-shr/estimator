using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ExtensionChoiceElement
    {
        public string Caption
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public ExtensionChoice Parent
        {
            get;
            private set;
        }

        public Variables Variables
        {
            get;
            private set;
        }

        public ExtensionChoiceElement(string name, string caption, ExtensionChoice parent)
        {
            this.Name = name;
            this.Caption = caption;
            this.Parent = parent;
            this.Variables = new Variables();
        }

        public void Clear()
        {
            this.Name = "";
            this.Caption = "";
            this.Parent = null;
            this.Variables.Clear();
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("Caption = ", this.Caption));
            this.Variables.Dump();
        }
    }
}