using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class QuoteItem
    {
        public string Description
        {
            get;
            set;
        }

        public string ID
        {
            get;
            private set;
        }

        public double PriceEach
        {
            get;
            set;
        }

        public double Quantity
        {
            get;
            set;
        }

        public QuoteSection QuoteSection
        {
            get;
            set;
        }

        public double Total
        {
            get
            {
                return this.Quantity * this.PriceEach;
            }
        }

        public string Unit
        {
            get;
            set;
        }

        public QuoteItem(string id, double quantity, double priceEach, string description, string unit, QuoteSection quoteSection)
        {
            this.ID = id;
            this.Quantity = quantity;
            this.PriceEach = priceEach;
            this.Description = description;
            this.Unit = unit;
            this.QuoteSection = quoteSection;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("Quantity = ", this.Quantity));
            Console.WriteLine(string.Concat("PriceEach = ", this.PriceEach));
            Console.WriteLine(string.Concat("Total = ", this.Total));
            Console.WriteLine(string.Concat("Description = ", this.Description));
        }
    }
}