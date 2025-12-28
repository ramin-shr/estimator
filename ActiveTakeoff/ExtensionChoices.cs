using System;
using System.Collections;

namespace QuoterPlan
{
	public class ExtensionChoices
	{
		public ExtensionChoices()
		{
			this.extensionChoiceList = new ArrayList();
		}

		public ExtensionChoice this[int index]
		{
			get
			{
				if (index < 0 || index >= this.extensionChoiceList.Count)
				{
					return null;
				}
				return (ExtensionChoice)this.extensionChoiceList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.extensionChoiceList;
			}
		}

		public int Add(ExtensionChoice templateChoice)
		{
			return this.extensionChoiceList.Add(templateChoice);
		}

		public int Count
		{
			get
			{
				return this.extensionChoiceList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.extensionChoiceList)
			{
				ExtensionChoice extensionChoice = (ExtensionChoice)obj;
				extensionChoice.Clear();
			}
			this.extensionChoiceList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.extensionChoiceList)
			{
				ExtensionChoice extensionChoice = (ExtensionChoice)obj;
				extensionChoice.Dump();
			}
		}

		private ArrayList extensionChoiceList;
	}
}
