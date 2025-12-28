using System;
using System.Collections;

namespace QuoterPlan
{
	public class QuoteSections
	{
		public QuoteSections()
		{
			this.quoteSectionsList = new Hashtable();
		}

		public QuoteSection this[int index]
		{
			get
			{
				QuoteSection result;
				try
				{
					result = (QuoteSection)this.quoteSectionsList[index];
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
				return this.quoteSectionsList;
			}
		}

		public void Add(QuoteSection section)
		{
			this.quoteSectionsList.Add(section.ID, section);
		}

		public void Delete(QuoteSection section)
		{
			this.quoteSectionsList.Remove(section);
		}

		public int Count
		{
			get
			{
				return this.quoteSectionsList.Count;
			}
		}

		private string[] SortHashTable(Hashtable hashtable)
		{
			string[] array = new string[hashtable.Count];
			int num = 0;
			foreach (object obj in hashtable)
			{
				array[num] = ((DictionaryEntry)obj).Key.ToString();
				num++;
			}
			Array.Sort(array, new NumericComparer());
			return array;
		}

		public void Clear()
		{
			this.quoteSectionsList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.quoteSectionsList.Values)
			{
				QuoteSection quoteSection = (QuoteSection)obj;
				quoteSection.Dump();
			}
		}

		private Hashtable quoteSectionsList;
	}
}
