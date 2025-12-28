using System;
using System.Collections;

namespace QuoterPlan
{
	public class Extensions
	{
		public Extensions()
		{
			this.extensionList = new ArrayList();
		}

		public Extension this[int index]
		{
			get
			{
				if (index < 0 || index >= this.extensionList.Count)
				{
					return null;
				}
				return (Extension)this.extensionList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.extensionList;
			}
		}

		public int Add(Extension extension)
		{
			return this.extensionList.Add(extension);
		}

		public int Count
		{
			get
			{
				return this.extensionList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.extensionList)
			{
				Extension extension = (Extension)obj;
				extension.Clear();
			}
			this.extensionList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.extensionList)
			{
				Extension extension = (Extension)obj;
				extension.Dump();
			}
		}

		private ArrayList extensionList;
	}
}
