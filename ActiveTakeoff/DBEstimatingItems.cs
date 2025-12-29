using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class DBEstimatingItems
    {
        private Hashtable estimatingItemsList;

        public Hashtable Collection
        {
            get
            {
                return this.estimatingItemsList;
            }
        }

        public int Count
        {
            get
            {
                return this.estimatingItemsList.Count;
            }
        }

        public DBEstimatingItem this[int index]
        {
            get
            {
                DBEstimatingItem item;
                try
                {
                    item = (DBEstimatingItem)this.estimatingItemsList[index];
                }
                catch
                {
                    item = null;
                }
                return item;
            }
        }

        public int NextAvailableIndex
        {
            get;
            set;
        }

        public DBEstimatingItems()
        {
            this.estimatingItemsList = new Hashtable();
            this.NextAvailableIndex = 0;
        }

        public void Add(DBEstimatingItem estimatingItem)
        {
            this.estimatingItemsList.Add(estimatingItem.ID, estimatingItem);
        }

        public void Clear()
        {
            this.estimatingItemsList.Clear();
        }

        public void Delete(DBEstimatingItem estimatingItem)
        {
            this.estimatingItemsList.Remove(estimatingItem.ID);
        }

        public void Dump()
        {
            foreach (DBEstimatingItem value in this.estimatingItemsList.Values)
            {
                value.Dump();
            }
        }

        public string GetNameFromID(int itemID)
        {
            string description;
            try
            {
                description = this[itemID].Description;
            }
            catch
            {
                description = Resources.Item_inconnu;
            }
            return description;
        }

        public int GetNextAvailableIndex()
        {
            int nextAvailableIndex = this.NextAvailableIndex;
            foreach (DBEstimatingItem value in this.estimatingItemsList.Values)
            {
                if (value.ID < nextAvailableIndex)
                {
                    continue;
                }
                nextAvailableIndex = value.ID + 1;
            }
            return nextAvailableIndex;
        }
    }
}