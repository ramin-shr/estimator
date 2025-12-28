using System;
using System.Collections.Generic;

namespace QuoterPlan
{
	public class CEstimatingItems
	{
		public CEstimatingItems()
		{
			this.coProductsList = new List<CEstimatingItem>();
		}

		public CEstimatingItem this[int index]
		{
			get
			{
				if (index < 0 || index >= this.coProductsList.Count)
				{
					return null;
				}
				return this.coProductsList[index];
			}
		}

		public List<CEstimatingItem> Collection
		{
			get
			{
				return this.coProductsList;
			}
		}

		public void Add(CEstimatingItem product)
		{
			this.coProductsList.Add(product);
		}

		public void Remove(CEstimatingItem product)
		{
			this.coProductsList.Remove(product);
		}

		public int Count
		{
			get
			{
				return this.coProductsList.Count;
			}
		}

		public void Clear()
		{
			this.coProductsList.Clear();
		}

		public CEstimatingItem FindByInternalKey(string internalKey)
		{
			foreach (CEstimatingItem cestimatingItem in this.coProductsList)
			{
				if (cestimatingItem.InternalKey == internalKey)
				{
					return cestimatingItem;
				}
			}
			return null;
		}

		public CEstimatingItem FindByFormula(string itemID, string formula)
		{
			foreach (CEstimatingItem cestimatingItem in this.coProductsList)
			{
				if (cestimatingItem.ItemID == itemID && cestimatingItem.Formula == formula)
				{
					return cestimatingItem;
				}
			}
			return null;
		}

		public virtual void Refresh()
		{
		}

		public void Dump()
		{
			foreach (CEstimatingItem cestimatingItem in this.coProductsList)
			{
				cestimatingItem.Dump();
			}
		}

		private List<CEstimatingItem> coProductsList;
	}
}
