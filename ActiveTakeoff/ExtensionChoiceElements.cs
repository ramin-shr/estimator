using System;
using System.Collections;

namespace QuoterPlan
{
	public class ExtensionChoiceElements
	{
		public ExtensionChoiceElements()
		{
			this.extensionChoiceElementList = new ArrayList();
		}

		public ExtensionChoiceElement this[int index]
		{
			get
			{
				if (index < 0 || index >= this.extensionChoiceElementList.Count)
				{
					return null;
				}
				return (ExtensionChoiceElement)this.extensionChoiceElementList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.extensionChoiceElementList;
			}
		}

		public int Add(ExtensionChoiceElement templateChoiceElement)
		{
			return this.extensionChoiceElementList.Add(templateChoiceElement);
		}

		public int Count
		{
			get
			{
				return this.extensionChoiceElementList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.extensionChoiceElementList)
			{
				ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)obj;
				extensionChoiceElement.Clear();
			}
			this.extensionChoiceElementList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.extensionChoiceElementList)
			{
				ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)obj;
				extensionChoiceElement.Dump();
			}
		}

		private ArrayList extensionChoiceElementList;
	}
}
