using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class Extension
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

		public string ObjectType
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ObjectType>k__BackingField = value;
			}
		}

		public bool Hidden
		{
			[CompilerGenerated]
			get
			{
				return this.<Hidden>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Hidden>k__BackingField = value;
			}
		}

		public Variables[] Variables
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

		public ExtensionChoices Choices
		{
			[CompilerGenerated]
			get
			{
				return this.<Choices>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Choices>k__BackingField = value;
			}
		}

		public ExtensionFields Fields
		{
			[CompilerGenerated]
			get
			{
				return this.<Fields>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Fields>k__BackingField = value;
			}
		}

		public ExtensionResults Results
		{
			[CompilerGenerated]
			get
			{
				return this.<Results>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Results>k__BackingField = value;
			}
		}

		public Extension(string name, string caption, string objectType, bool hidden)
		{
			this.Name = name;
			this.Caption = caption;
			this.ObjectType = objectType;
			this.Hidden = hidden;
			this.Variables = new Variables[2];
			this.Variables[0] = new Variables();
			this.Variables[1] = new Variables();
			this.Choices = new ExtensionChoices();
			this.Fields = new ExtensionFields();
			this.Results = new ExtensionResults();
		}

		public void Clear()
		{
			this.Name = "";
			this.Caption = "";
			this.ObjectType = "";
			this.Hidden = false;
			this.Variables[0].Clear();
			this.Variables[1].Clear();
			this.Choices.Clear();
			this.Fields.Clear();
			this.Results.Clear();
		}

		public void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("Caption = " + this.Caption);
			Console.WriteLine("ObjectType = " + this.ObjectType);
			Console.WriteLine("Hidden = " + this.Hidden);
			this.Variables[0].Dump();
			this.Variables[1].Dump();
			this.Choices.Dump();
			this.Fields.Dump();
			this.Results.Dump();
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private string <ObjectType>k__BackingField;

		[CompilerGenerated]
		private bool <Hidden>k__BackingField;

		[CompilerGenerated]
		private Variables[] <Variables>k__BackingField;

		[CompilerGenerated]
		private ExtensionChoices <Choices>k__BackingField;

		[CompilerGenerated]
		private ExtensionFields <Fields>k__BackingField;

		[CompilerGenerated]
		private ExtensionResults <Results>k__BackingField;
	}
}
