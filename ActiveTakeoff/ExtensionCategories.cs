using System;
using System.Collections;

namespace QuoterPlan
{
	public class ExtensionCategories
	{
		public ExtensionCategories()
		{
			this.extensionCategoryList = new ArrayList();
		}

		public ExtensionCategory this[int index]
		{
			get
			{
				if (index < 0 || index >= this.extensionCategoryList.Count)
				{
					return null;
				}
				return (ExtensionCategory)this.extensionCategoryList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.extensionCategoryList;
			}
		}

		public int Add(ExtensionCategory extensionCategory)
		{
			return this.extensionCategoryList.Add(extensionCategory);
		}

		public int Count
		{
			get
			{
				return this.extensionCategoryList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.extensionCategoryList)
			{
				ExtensionCategory extensionCategory = (ExtensionCategory)obj;
				extensionCategory.Clear();
			}
			this.extensionCategoryList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.extensionCategoryList)
			{
				ExtensionCategory extensionCategory = (ExtensionCategory)obj;
				extensionCategory.Dump();
			}
		}

		private ArrayList extensionCategoryList;
	}
}
