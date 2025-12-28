using System;
using System.Collections;

namespace QuoterPlan
{
	public class PresetResults
	{
		public PresetResults()
		{
			this.presetResultList = new ArrayList();
		}

		public PresetResult this[int index]
		{
			get
			{
				if (index < 0 || index >= this.presetResultList.Count)
				{
					return null;
				}
				return (PresetResult)this.presetResultList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.presetResultList;
			}
		}

		public int Add(PresetResult presetResult)
		{
			return this.presetResultList.Add(presetResult);
		}

		public PresetResult Find(string name)
		{
			foreach (object obj in this.presetResultList)
			{
				PresetResult presetResult = (PresetResult)obj;
				if (presetResult.Name == name)
				{
					return presetResult;
				}
			}
			return null;
		}

		public PresetResult FindByCaption(string caption)
		{
			foreach (object obj in this.presetResultList)
			{
				PresetResult presetResult = (PresetResult)obj;
				if (presetResult.Caption == caption)
				{
					return presetResult;
				}
			}
			return null;
		}

		public string GetIndexedName(string name)
		{
			int num = 1;
			string text;
			for (;;)
			{
				text = name + (num + 1).ToString();
				if (this.Find(text) == null)
				{
					break;
				}
				num++;
			}
			return text;
		}

		public int Count
		{
			get
			{
				return this.presetResultList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.presetResultList)
			{
				PresetResult presetResult = (PresetResult)obj;
				presetResult.Clear();
			}
			this.presetResultList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.presetResultList)
			{
				PresetResult presetResult = (PresetResult)obj;
				presetResult.Dump();
			}
		}

		private ArrayList presetResultList;
	}
}
