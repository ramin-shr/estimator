using System;
using System.Collections;

namespace QuoterPlan
{
	public class PresetChoices
	{
		public PresetChoices()
		{
			this.presetChoiceList = new ArrayList();
		}

		public PresetChoice this[int index]
		{
			get
			{
				if (index < 0 || index >= this.presetChoiceList.Count)
				{
					return null;
				}
				return (PresetChoice)this.presetChoiceList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.presetChoiceList;
			}
		}

		public int Add(PresetChoice presetChoice)
		{
			return this.presetChoiceList.Add(presetChoice);
		}

		public PresetChoice Find(string name)
		{
			foreach (object obj in this.presetChoiceList)
			{
				PresetChoice presetChoice = (PresetChoice)obj;
				if (presetChoice.ChoiceName == name)
				{
					return presetChoice;
				}
			}
			return null;
		}

		public int Count
		{
			get
			{
				return this.presetChoiceList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.presetChoiceList)
			{
				PresetChoice presetChoice = (PresetChoice)obj;
				presetChoice.Clear();
			}
			this.presetChoiceList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.presetChoiceList)
			{
				PresetChoice presetChoice = (PresetChoice)obj;
				presetChoice.Dump();
			}
		}

		private ArrayList presetChoiceList;
	}
}
