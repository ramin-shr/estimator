using System;
using System.Collections;

namespace QuoterPlan
{
	public class ExtensionResults
	{
		public ExtensionResults()
		{
			this.extensionResultList = new ArrayList();
		}

		public ExtensionResult this[int index]
		{
			get
			{
				if (index < 0 || index >= this.extensionResultList.Count)
				{
					return null;
				}
				return (ExtensionResult)this.extensionResultList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.extensionResultList;
			}
		}

		public int Add(ExtensionResult extensionResult)
		{
			return this.extensionResultList.Add(extensionResult);
		}

		public int Count
		{
			get
			{
				return this.extensionResultList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.extensionResultList)
			{
				ExtensionResult extensionResult = (ExtensionResult)obj;
				extensionResult.Clear();
			}
			this.extensionResultList.Clear();
		}

		public ExtensionResult Find(string name)
		{
			foreach (object obj in this.extensionResultList)
			{
				ExtensionResult extensionResult = (ExtensionResult)obj;
				if (extensionResult.Name == name)
				{
					return extensionResult;
				}
			}
			return null;
		}

		public string GetIndexedName(string name)
		{
			string text = "";
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				if (char.IsDigit(name[i]))
				{
					c = Convert.ToChar(65 + Utilities.ConvertToInt(Convert.ToString(name[i])));
				}
				text += c;
			}
			name = text;
			int num = 1;
			string text2;
			for (;;)
			{
				text2 = name + num.ToString();
				if (this.Find(text2) == null)
				{
					break;
				}
				num++;
			}
			return text2;
		}

		public void Dump()
		{
			foreach (object obj in this.extensionResultList)
			{
				ExtensionResult extensionResult = (ExtensionResult)obj;
				extensionResult.Dump();
			}
		}

		private ArrayList extensionResultList;
	}
}
