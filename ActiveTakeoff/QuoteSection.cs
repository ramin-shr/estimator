using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class QuoteSection
	{
		public int ID
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

		public double Total
		{
			[CompilerGenerated]
			get
			{
				return this.<Total>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Total>k__BackingField = value;
			}
		}

		public QuoteItems QuoteItems
		{
			[CompilerGenerated]
			get
			{
				return this.<QuoteItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QuoteItems>k__BackingField = value;
			}
		}

		public QuoteSection(int id, string description)
		{
			this.ID = id;
			this.Description = description;
			this.QuoteItems = new QuoteItems();
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("Description = " + this.Description);
			this.QuoteItems.Dump();
		}

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private string <Description>k__BackingField;

		[CompilerGenerated]
		private double <Total>k__BackingField;

		[CompilerGenerated]
		private QuoteItems <QuoteItems>k__BackingField;
	}
}
