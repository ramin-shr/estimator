using System;
using System.Collections;

namespace QuoterPlan
{
	public class Templates
	{
		public Templates()
		{
			this.extensionList = new ArrayList();
		}

		public Template this[int index]
		{
			get
			{
				if (index < 0 || index >= this.extensionList.Count)
				{
					return null;
				}
				return (Template)this.extensionList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.extensionList;
			}
		}

		public int Add(Template template)
		{
			return this.extensionList.Add(template);
		}

		public void RemoveAt(int index)
		{
			if (this[index] != null)
			{
				this[index].Clear();
				this.extensionList.RemoveAt(index);
			}
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
				Template template = (Template)obj;
				template.Clear();
			}
			this.extensionList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.extensionList)
			{
				Template template = (Template)obj;
				template.Dump();
			}
		}

		private ArrayList extensionList;
	}
}
