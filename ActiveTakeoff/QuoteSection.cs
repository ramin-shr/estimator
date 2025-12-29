using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class QuoteSection
    {
        public string Description
        {
            get;
            set;
        }

        public int ID
        {
            get;
            private set;
        }

        public QuoteItems QuoteItems
        {
            get;
            set;
        }

        public double Total
        {
            get;
            set;
        }

        public QuoteSection(int id, string description)
        {
            this.ID = id;
            this.Description = description;
            this.QuoteItems = new QuoteItems();
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("Description = ", this.Description));
            this.QuoteItems.Dump();
        }
    }
}