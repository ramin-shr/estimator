using System;
using System.Collections;

namespace QuoterPlan
{
	public class DBEstimatingSections
	{
		public DBEstimatingSections()
		{
			this.sectionsList = new Hashtable();
		}

		public DBEstimatingSection this[int index]
		{
			get
			{
				DBEstimatingSection result;
				try
				{
					result = (DBEstimatingSection)this.sectionsList[index];
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
				return this.sectionsList;
			}
		}

		public void Add(DBEstimatingSection section)
		{
			this.sectionsList.Add(section.ID, section);
		}

		public int Count
		{
			get
			{
				return this.sectionsList.Count;
			}
		}

		public void Clear()
		{
			this.sectionsList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.sectionsList)
			{
				DBEstimatingSection dbestimatingSection = (DBEstimatingSection)obj;
				dbestimatingSection.Dump();
			}
		}

		private Hashtable sectionsList;
	}
}
