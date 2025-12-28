using System;
using System.Collections;
using System.Runtime.CompilerServices;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class DBEstimatingItems
	{
		public int NextAvailableIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<NextAvailableIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NextAvailableIndex>k__BackingField = value;
			}
		}

		public DBEstimatingItems()
		{
			this.estimatingItemsList = new Hashtable();
			this.NextAvailableIndex = 0;
		}

		public DBEstimatingItem this[int index]
		{
			get
			{
				DBEstimatingItem result;
				try
				{
					result = (DBEstimatingItem)this.estimatingItemsList[index];
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
				return this.estimatingItemsList;
			}
		}

		public void Add(DBEstimatingItem estimatingItem)
		{
			this.estimatingItemsList.Add(estimatingItem.ID, estimatingItem);
		}

		public void Delete(DBEstimatingItem estimatingItem)
		{
			this.estimatingItemsList.Remove(estimatingItem.ID);
		}

		public int Count
		{
			get
			{
				return this.estimatingItemsList.Count;
			}
		}

		public string GetNameFromID(int itemID)
		{
			string result;
			try
			{
				result = this[itemID].Description;
			}
			catch
			{
				result = Resources.Item_inconnu;
			}
			return result;
		}

		public int GetNextAvailableIndex()
		{
			int num = this.NextAvailableIndex;
			foreach (object obj in this.estimatingItemsList.Values)
			{
				DBEstimatingItem dbestimatingItem = (DBEstimatingItem)obj;
				if (dbestimatingItem.ID >= num)
				{
					num = dbestimatingItem.ID + 1;
				}
			}
			return num;
		}

		public void Clear()
		{
			this.estimatingItemsList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.estimatingItemsList.Values)
			{
				DBEstimatingItem dbestimatingItem = (DBEstimatingItem)obj;
				dbestimatingItem.Dump();
			}
		}

		private Hashtable estimatingItemsList;

		[CompilerGenerated]
		private int <NextAvailableIndex>k__BackingField;
	}
}
