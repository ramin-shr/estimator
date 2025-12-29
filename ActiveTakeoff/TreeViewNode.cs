using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class TreeViewNode
    {
        public int ID
        {
            get;
            set;
        }

        public int ParentID
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public TreeViewNode(int id, int parentID, object tag)
        {
            this.ID = id;
            this.ParentID = parentID;
            this.Tag = tag;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("ParentID = ", this.ParentID));
            Console.WriteLine(string.Concat("Tag = ", this.Tag));
        }
    }
}