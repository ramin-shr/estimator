using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Extension
    {
        public string Caption
        {
            get;
            private set;
        }

        public ExtensionChoices Choices
        {
            get;
            private set;
        }

        public ExtensionFields Fields
        {
            get;
            private set;
        }

        public bool Hidden
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string ObjectType
        {
            get;
            private set;
        }

        public ExtensionResults Results
        {
            get;
            private set;
        }

        public Variables[] Variables
        {
            get;
            private set;
        }

        public Extension(string name, string caption, string objectType, bool hidden)
        {
            this.Name = name;
            this.Caption = caption;
            this.ObjectType = objectType;
            this.Hidden = hidden;
            this.Variables = new Variables[] { new Variables(), new Variables() };
            this.Choices = new ExtensionChoices();
            this.Fields = new ExtensionFields();
            this.Results = new ExtensionResults();
        }

        public void Clear()
        {
            this.Name = "";
            this.Caption = "";
            this.ObjectType = "";
            this.Hidden = false;
            this.Variables[0].Clear();
            this.Variables[1].Clear();
            this.Choices.Clear();
            this.Fields.Clear();
            this.Results.Clear();
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("Caption = ", this.Caption));
            Console.WriteLine(string.Concat("ObjectType = ", this.ObjectType));
            Console.WriteLine(string.Concat("Hidden = ", this.Hidden));
            this.Variables[0].Dump();
            this.Variables[1].Dump();
            this.Choices.Dump();
            this.Fields.Dump();
            this.Results.Dump();
        }
    }
}