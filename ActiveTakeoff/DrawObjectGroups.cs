using System;
using System.Collections;

namespace QuoterPlan
{
	public class DrawObjectGroups
	{
		public DrawObjectGroups()
		{
			this.groupList = new ArrayList();
		}

		public DrawObjectGroup this[int index]
		{
			get
			{
				if (index < 0 || index >= this.groupList.Count)
				{
					return null;
				}
				return (DrawObjectGroup)this.groupList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.groupList;
			}
		}

		public int Add(DrawObjectGroup group)
		{
			return this.groupList.Add(group);
		}

		public void RemoveAt(int index)
		{
			if (this[index] != null)
			{
				this[index].Clear();
				this.groupList.RemoveAt(index);
			}
		}

		public int Count
		{
			get
			{
				return this.groupList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.groupList)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				drawObjectGroup.Clear();
			}
			this.groupList.Clear();
		}

		public bool TemplateInUse(string templateID)
		{
			foreach (object obj in this.groupList)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				if (!drawObjectGroup.Deleted && drawObjectGroup.TemplateID == templateID)
				{
					return true;
				}
			}
			return false;
		}

		public DrawObjectGroup FindFromGroupID(int groupID)
		{
			foreach (object obj in this.groupList)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				if (drawObjectGroup.ID == groupID)
				{
					return drawObjectGroup;
				}
			}
			return null;
		}

		public Preset FindPresetFromID(string presetID)
		{
			Preset preset = null;
			foreach (object obj in this.groupList)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				preset = drawObjectGroup.Presets.FindById(presetID);
				if (preset != null)
				{
					break;
				}
			}
			return preset;
		}

		public void Dump()
		{
			foreach (object obj in this.groupList)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj;
				drawObjectGroup.Dump();
			}
		}

		private ArrayList groupList;
	}
}
