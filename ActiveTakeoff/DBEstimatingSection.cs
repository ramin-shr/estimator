using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class DBEstimatingSection
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

        public DBEstimatingSection(int id, int parentID, string name)
        {
            this.ID = id;
            this.ParentID = parentID;
            this.Name = name;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("ParentID = ", this.ParentID));
            Console.WriteLine(string.Concat("Name = ", this.Name));
        }
    }
}