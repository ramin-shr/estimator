using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class CEstimatingSection
    {
        public int ID
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }

        public int ParentID
        {
            get;
            private set;
        }

        public string SectionID
        {
            get;
            private set;
        }

        public object Tag
        {
            get;
            set;
        }

        public CEstimatingSection(int id, int parentID, string sectionID, string name)
        {
            this.ID = id;
            this.ParentID = parentID;
            this.SectionID = sectionID;
            this.Name = name;
        }

        public void Clear()
        {
            this.ID = 0;
            this.ParentID = 0;
            this.SectionID = "";
            this.Name = "";
            this.Tag = null;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("ParentID = ", this.ParentID));
            Console.WriteLine(string.Concat("SectionID = ", this.ParentID));
            Console.WriteLine(string.Concat("Name = ", this.Name));
        }
    }
}