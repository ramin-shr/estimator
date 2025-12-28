using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class CEstimatingSection
	{
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
			[CompilerGenerated]
			private set
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
			private set
			{
				this.<ParentID>k__BackingField = value;
			}
		}

		public string SectionID
		{
			[CompilerGenerated]
			get
			{
				return this.<SectionID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SectionID>k__BackingField = value;
			}
		}

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
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

		public CEstimatingSection(int id, int parentID, string sectionID, string name)
		{
			this.ID = id;
			this.ParentID = parentID;
			this.SectionID = sectionID;
			this.Name = name;
		}

		public void Clear()
		{
			this.ID = 0;
			this.ParentID = 0;
			this.SectionID = "";
			this.Name = "";
			this.Tag = null;
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("ParentID = " + this.ParentID);
			Console.WriteLine("SectionID = " + this.ParentID);
			Console.WriteLine("Name = " + this.Name);
		}

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private int <ParentID>k__BackingField;

		[CompilerGenerated]
		private string <SectionID>k__BackingField;

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private object <Tag>k__BackingField;
	}
}
