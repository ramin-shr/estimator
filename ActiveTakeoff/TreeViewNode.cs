using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class TreeViewNode
	{
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ID>k__BackingField = value;
			}
		}

		public int ParentID
		{
			[CompilerGenerated]
			get
			{
				return this.<ParentID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ParentID>k__BackingField = value;
			}
		}

		public object Tag
		{
			[CompilerGenerated]
			get
			{
				return this.<Tag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tag>k__BackingField = value;
			}
		}

		public TreeViewNode(int id, int parentID, object tag)
		{
			this.ID = id;
			this.ParentID = parentID;
			this.Tag = tag;
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("ParentID = " + this.ParentID);
			Console.WriteLine("Tag = " + this.Tag);
		}

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private int <ParentID>k__BackingField;

		[CompilerGenerated]
		private object <Tag>k__BackingField;
	}
}
