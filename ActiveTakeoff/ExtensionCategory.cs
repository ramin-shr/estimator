using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ExtensionCategory
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

		public string Caption
		{
			[CompilerGenerated]
			get
			{
				return this.<Caption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Caption>k__BackingField = value;
			}
		}

		public int Division
		{
			[CompilerGenerated]
			get
			{
				return this.<Division>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Division>k__BackingField = value;
			}
		}

		public Extensions Templates
		{
			[CompilerGenerated]
			get
			{
				return this.<Templates>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Templates>k__BackingField = value;
			}
		}

		public ExtensionCategory()
		{
			this.Templates = new Extensions();
		}

		public ExtensionCategory(string name, string caption, int division)
		{
			this.Name = name;
			this.Caption = caption;
			this.Division = division;
			this.Templates = new Extensions();
		}

		public void Clear()
		{
			this.Name = "";
			this.Caption = "";
			this.Division = 0;
			this.Templates.Clear();
		}

		public void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("Caption = " + this.Caption);
			Console.WriteLine("Division = " + this.Division);
			this.Templates.Dump();
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private int <Division>k__BackingField;

		[CompilerGenerated]
		private Extensions <Templates>k__BackingField;
	}
}
