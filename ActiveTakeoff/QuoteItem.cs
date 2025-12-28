using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class QuoteItem
	{
		public string ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ID>k__BackingField = value;
			}
		}

		public double Quantity
		{
			[CompilerGenerated]
			get
			{
				return this.<Quantity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Quantity>k__BackingField = value;
			}
		}

		public double PriceEach
		{
			[CompilerGenerated]
			get
			{
				return this.<PriceEach>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PriceEach>k__BackingField = value;
			}
		}

		public double Total
		{
			get
			{
				return this.Quantity * this.PriceEach;
			}
		}

		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		public string Unit
		{
			[CompilerGenerated]
			get
			{
				return this.<Unit>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Unit>k__BackingField = value;
			}
		}

		public QuoteSection QuoteSection
		{
			[CompilerGenerated]
			get
			{
				return this.<QuoteSection>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QuoteSection>k__BackingField = value;
			}
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
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("Quantity = " + this.Quantity);
			Console.WriteLine("PriceEach = " + this.PriceEach);
			Console.WriteLine("Total = " + this.Total);
			Console.WriteLine("Description = " + this.Description);
		}

		[CompilerGenerated]
		private string <ID>k__BackingField;

		[CompilerGenerated]
		private double <Quantity>k__BackingField;

		[CompilerGenerated]
		private double <PriceEach>k__BackingField;

		[CompilerGenerated]
		private string <Description>k__BackingField;

		[CompilerGenerated]
		private string <Unit>k__BackingField;

		[CompilerGenerated]
		private QuoteSection <QuoteSection>k__BackingField;
	}
}
