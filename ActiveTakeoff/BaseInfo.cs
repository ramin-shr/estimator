using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class BaseInfo
	{
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

		public string PrevName
		{
			[CompilerGenerated]
			get
			{
				return this.<PrevName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrevName>k__BackingField = value;
			}
		}

		public bool Visible
		{
			[CompilerGenerated]
			get
			{
				return this.<Visible>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Visible>k__BackingField = value;
			}
		}

		public bool LastVisible
		{
			[CompilerGenerated]
			get
			{
				return this.<LastVisible>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LastVisible>k__BackingField = value;
			}
		}

		public bool Active
		{
			[CompilerGenerated]
			get
			{
				return this.<Active>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Active>k__BackingField = value;
			}
		}

		public bool Dirty
		{
			[CompilerGenerated]
			get
			{
				return this.<Dirty>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Dirty>k__BackingField = value;
			}
		}

		public BaseInfo()
		{
			this.Initialize();
		}

		private void Initialize()
		{
			this.Name = string.Empty;
			this.PrevName = string.Empty;
			this.Visible = false;
			this.LastVisible = false;
			this.Active = false;
			this.Dirty = false;
		}

		public virtual void Clear()
		{
			this.Initialize();
		}

		public virtual void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("PrevName = " + this.PrevName);
			Console.WriteLine("Visible = " + this.Visible);
			Console.WriteLine("LastVisible = " + this.LastVisible);
			Console.WriteLine("Active = " + this.Active);
			Console.WriteLine("Dirty = " + this.Dirty);
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <PrevName>k__BackingField;

		[CompilerGenerated]
		private bool <Visible>k__BackingField;

		[CompilerGenerated]
		private bool <LastVisible>k__BackingField;

		[CompilerGenerated]
		private bool <Active>k__BackingField;

		[CompilerGenerated]
		private bool <Dirty>k__BackingField;
	}
}
