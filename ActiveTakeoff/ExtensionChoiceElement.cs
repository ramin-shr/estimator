using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ExtensionChoiceElement
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

		public string Caption
		{
			[CompilerGenerated]
			get
			{
				return this.<Caption>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Caption>k__BackingField = value;
			}
		}

		public ExtensionChoice Parent
		{
			[CompilerGenerated]
			get
			{
				return this.<Parent>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Parent>k__BackingField = value;
			}
		}

		public Variables Variables
		{
			[CompilerGenerated]
			get
			{
				return this.<Variables>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Variables>k__BackingField = value;
			}
		}

		public ExtensionChoiceElement(string name, string caption, ExtensionChoice parent)
		{
			this.Name = name;
			this.Caption = caption;
			this.Parent = parent;
			this.Variables = new Variables();
		}

		public void Clear()
		{
			this.Name = "";
			this.Caption = "";
			this.Parent = null;
			this.Variables.Clear();
		}

		public void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("Caption = " + this.Caption);
			this.Variables.Dump();
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private ExtensionChoice <Parent>k__BackingField;

		[CompilerGenerated]
		private Variables <Variables>k__BackingField;
	}
}
