using System;
using System.Collections;

namespace QuoterPlan
{
	public class Presets
	{
		public Presets()
		{
			this.presetList = new ArrayList();
		}

		public Preset this[int index]
		{
			get
			{
				if (index < 0 || index >= this.presetList.Count)
				{
					return null;
				}
				return (Preset)this.presetList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.presetList;
			}
		}

		public int Add(Preset preset)
		{
			return this.presetList.Add(preset);
		}

		public void Remove(Preset preset)
		{
			this.presetList.Remove(preset);
		}

		public int Count
		{
			get
			{
				return this.presetList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.presetList)
			{
				Preset preset = (Preset)obj;
				preset.Clear();
			}
			this.presetList.Clear();
		}

		public Preset Find(string extensionName)
		{
			foreach (object obj in this.presetList)
			{
				Preset preset = (Preset)obj;
				if (preset.ExtensionName == extensionName)
				{
					return preset;
				}
			}
			return null;
		}

		public Preset FindById(string id)
		{
			foreach (object obj in this.presetList)
			{
				Preset preset = (Preset)obj;
				if (preset.ID == id)
				{
					return preset;
				}
			}
			return null;
		}

		public Preset FindByDisplayName(string displayName, string extensionID = "")
		{
			foreach (object obj in this.presetList)
			{
				Preset preset = (Preset)obj;
				if (extensionID == "")
				{
					if (preset.DisplayName == displayName)
					{
						return preset;
					}
				}
				else if (preset.DisplayName == displayName && preset.ID != extensionID)
				{
					return preset;
				}
			}
			return null;
		}

		public string GetFreeDisplayName(string displayName, string extensionID = "")
		{
			displayName = Utilities.StripInvalidCharacters(displayName, "[].");
			if (this.FindByDisplayName(displayName, extensionID) == null)
			{
				return displayName;
			}
			int num = 0;
			displayName = Utilities.StripIndexFromString(displayName) + " ";
			do
			{
				num++;
			}
			while (this.FindByDisplayName(displayName + num, "") != null);
			return displayName + num;
		}

		public bool HasCustomRendering()
		{
			foreach (object obj in this.presetList)
			{
				Preset preset = (Preset)obj;
				if (preset.CustomRendering != null)
				{
					return true;
				}
			}
			return false;
		}

		public void Dump()
		{
			foreach (object obj in this.presetList)
			{
				Preset preset = (Preset)obj;
				preset.Dump();
			}
		}

		private ArrayList presetList;
	}
}
