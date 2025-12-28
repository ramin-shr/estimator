using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class PresetChoice
	{
		public string ChoiceName
		{
			[CompilerGenerated]
			get
			{
				return this.<ChoiceName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ChoiceName>k__BackingField = value;
			}
		}

		public string ChoiceCaption
		{
			[CompilerGenerated]
			get
			{
				return this.<ChoiceCaption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChoiceCaption>k__BackingField = value;
			}
		}

		public string ChoiceElementName
		{
			[CompilerGenerated]
			get
			{
				return this.<ChoiceElementName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChoiceElementName>k__BackingField = value;
			}
		}

		public string ChoiceElementCaption
		{
			[CompilerGenerated]
			get
			{
				return this.<ChoiceElementCaption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChoiceElementCaption>k__BackingField = value;
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

		public PresetChoice(string choiceName, string choiceElementName)
		{
			this.ChoiceName = choiceName;
			this.ChoiceElementName = choiceElementName;
			this.Variables = new Variables();
		}

		public void Clear()
		{
			this.ChoiceName = "";
			this.ChoiceCaption = "";
			this.ChoiceElementName = "";
			this.ChoiceElementCaption = "";
			this.Variables.Clear();
		}

		public void Dump()
		{
			Console.WriteLine("ChoiceName = " + this.ChoiceName);
			Console.WriteLine("ChoiceCaption = " + this.ChoiceCaption);
			Console.WriteLine("ChoiceElementName = " + this.ChoiceElementName);
			Console.WriteLine("ChoiceElementCaption = " + this.ChoiceElementCaption);
			this.Variables.Dump();
		}

		[CompilerGenerated]
		private string <ChoiceName>k__BackingField;

		[CompilerGenerated]
		private string <ChoiceCaption>k__BackingField;

		[CompilerGenerated]
		private string <ChoiceElementName>k__BackingField;

		[CompilerGenerated]
		private string <ChoiceElementCaption>k__BackingField;

		[CompilerGenerated]
		private Variables <Variables>k__BackingField;
	}
}
