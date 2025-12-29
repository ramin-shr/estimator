using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class FormulaResult
    {
        public string Caption
        {
            get;
            private set;
        }

        public int ID
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public int ParentID
        {
            get;
            private set;
        }

        public Preset Preset
        {
            get;
            private set;
        }

        public object Tag
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            private set;
        }

        public FormulaResult(int id, int parentID, Preset preset, string name, string caption, string unit)
        {
            this.ID = id;
            this.ParentID = parentID;
            this.Preset = preset;
            this.Name = name;
            this.Caption = caption;
            this.Unit = unit;
            this.Tag = null;
        }

        public void Clear()
        {
            this.ID = 0;
            this.ParentID = 0;
            this.Preset = null;
            this.Name = "";
            this.Caption = "";
            this.Unit = "";
            this.Tag = null;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("ParentID = ", this.ParentID));
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("Caption = ", this.Caption));
            Console.WriteLine(string.Concat("Unit = ", this.Unit));
        }

        public string FormulaString()
        {
            if (this.Preset == null)
            {
                return string.Concat("[", this.Caption, "]");
            }
            string[] displayName = new string[] { "[", this.Preset.DisplayName, ".", this.Caption, "]" };
            return string.Concat(displayName);
        }
    }
}