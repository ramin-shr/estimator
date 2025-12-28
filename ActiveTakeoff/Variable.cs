using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class Variable
	{
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
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

		public Variable(string name, object value)
		{
			this.Name = name;
			this.Value = value;
		}

		public Variable(string name, object value, object tag)
		{
			this.Name = name;
			this.Value = value;
			this.Tag = tag;
		}

		public void Clear()
		{
			this.Name = "";
			this.Value = null;
			this.Tag = null;
		}

		public void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			if (this.Value != null)
			{
				Console.WriteLine("Value = " + this.Value);
			}
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private object <Value>k__BackingField;

		[CompilerGenerated]
		private object <Tag>k__BackingField;
	}
}
