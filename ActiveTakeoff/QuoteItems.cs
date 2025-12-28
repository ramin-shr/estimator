using System;
using System.Collections;

namespace QuoterPlan
{
	public class QuoteItems
	{
		public QuoteItems()
		{
			this.quoteItemsList = new Hashtable();
		}

		public QuoteItem this[string index]
		{
			get
			{
				QuoteItem result;
				try
				{
					result = (QuoteItem)this.quoteItemsList[index];
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public Hashtable Collection
		{
			get
			{
				return this.quoteItemsList;
			}
		}

		public void Add(QuoteItem quoteItem)
		{
			this.quoteItemsList.Add(quoteItem.ID, quoteItem);
		}

		public void Delete(QuoteItem quoteItem)
		{
			this.quoteItemsList.Remove(quoteItem);
		}

		public int Count
		{
			get
			{
				return this.quoteItemsList.Count;
			}
		}

		public void Clear()
		{
			this.quoteItemsList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.quoteItemsList.Values)
			{
				QuoteItem quoteItem = (QuoteItem)obj;
				quoteItem.Dump();
			}
		}

		private Hashtable quoteItemsList;
	}
}
