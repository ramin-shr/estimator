using System;
using System.Collections;

namespace QuoterPlan
{
	public class ExtensionFields
	{
		public ExtensionFields()
		{
			this.extensionFieldList = new ArrayList();
		}

		public ExtensionField this[int index]
		{
			get
			{
				if (index < 0 || index >= this.extensionFieldList.Count)
				{
					return null;
				}
				return (ExtensionField)this.extensionFieldList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.extensionFieldList;
			}
		}

		public int Add(ExtensionField extensionField)
		{
			return this.extensionFieldList.Add(extensionField);
		}

		public int Count
		{
			get
			{
				return this.extensionFieldList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.extensionFieldList)
			{
				ExtensionField extensionField = (ExtensionField)obj;
				extensionField.Clear();
			}
			this.extensionFieldList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.extensionFieldList)
			{
				ExtensionField extensionField = (ExtensionField)obj;
				extensionField.Dump();
			}
		}

		private ArrayList extensionFieldList;
	}
}
