using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class BaseInfo
    {
        public bool Active
        {
            get;
            set;
        }

        public bool Dirty
        {
            get;
            set;
        }

        public bool LastVisible
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string PrevName
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public BaseInfo()
        {
            this.Initialize();
        }

        public virtual void Clear()
        {
            this.Initialize();
        }

        public virtual void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("PrevName = ", this.PrevName));
            Console.WriteLine(string.Concat("Visible = ", this.Visible));
            Console.WriteLine(string.Concat("LastVisible = ", this.LastVisible));
            Console.WriteLine(string.Concat("Active = ", this.Active));
            Console.WriteLine(string.Concat("Dirty = ", this.Dirty));
        }

        private void Initialize()
        {
            this.Name = string.Empty;
            this.PrevName = string.Empty;
            this.Visible = false;
            this.LastVisible = false;
            this.Active = false;
            this.Dirty = false;
        }
    }
}