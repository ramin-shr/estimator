using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class DBEstimatingSection
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

		public DBEstimatingSection(int id, int parentID, string name)
		{
			this.ID = id;
			this.ParentID = parentID;
			this.Name = name;
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("ParentID = " + this.ParentID);
			Console.WriteLine("Name = " + this.Name);
		}

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private int <ParentID>k__BackingField;

		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
