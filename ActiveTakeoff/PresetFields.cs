using System;
using System.Collections;

namespace QuoterPlan
{
	public class PresetFields
	{
		public PresetFields()
		{
			this.presetFieldList = new ArrayList();
		}

		public PresetField this[int index]
		{
			get
			{
				if (index < 0 || index >= this.presetFieldList.Count)
				{
					return null;
				}
				return (PresetField)this.presetFieldList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.presetFieldList;
			}
		}

		public int Add(PresetField presetField)
		{
			return this.presetFieldList.Add(presetField);
		}

		public PresetField Find(string name)
		{
			foreach (object obj in this.presetFieldList)
			{
				PresetField presetField = (PresetField)obj;
				if (presetField.Name == name)
				{
					return presetField;
				}
			}
			return null;
		}

		public int Count
		{
			get
			{
				return this.presetFieldList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.presetFieldList)
			{
				PresetField presetField = (PresetField)obj;
				presetField.Clear();
			}
			this.presetFieldList.Clear();
		}

		public void Dump()
		{
			foreach (object obj in this.presetFieldList)
			{
				PresetField presetField = (PresetField)obj;
				presetField.Dump();
			}
		}

		private ArrayList presetFieldList;
	}
}
