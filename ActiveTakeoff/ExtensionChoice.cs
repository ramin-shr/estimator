using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ExtensionChoice
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

		public string DefaultChoiceImperial
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultChoiceImperial>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DefaultChoiceImperial>k__BackingField = value;
			}
		}

		public string DefaultChoiceMetric
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultChoiceMetric>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DefaultChoiceMetric>k__BackingField = value;
			}
		}

		public string Condition
		{
			[CompilerGenerated]
			get
			{
				return this.<Condition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Condition>k__BackingField = value;
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
			set
			{
				this.<Hidden>k__BackingField = value;
			}
		}

		public ExtensionChoiceElements Elements
		{
			[CompilerGenerated]
			get
			{
				return this.<Elements>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Elements>k__BackingField = value;
			}
		}

		public string DefaultChoice
		{
			get
			{
				string text = (UnitScale.DefaultUnitSystem() == UnitScale.UnitSystem.imperial) ? this.DefaultChoiceImperial : this.DefaultChoiceMetric;
				return (text == "") ? this.defaultChoice : text;
			}
			private set
			{
				this.defaultChoice = value;
			}
		}

		public ExtensionChoice(string name, string caption, string defaultChoice, string defaultChoiceImperial, string defaultChoiceMetric, string condition, bool hidden)
		{
			this.Name = name;
			this.Caption = caption;
			this.DefaultChoice = defaultChoice;
			this.DefaultChoiceImperial = defaultChoiceImperial;
			this.DefaultChoiceMetric = defaultChoiceMetric;
			this.Hidden = hidden;
			this.Condition = condition;
			this.Elements = new ExtensionChoiceElements();
		}

		public void Clear()
		{
			this.Name = "";
			this.Caption = "";
			this.DefaultChoice = "";
			this.DefaultChoiceImperial = "";
			this.DefaultChoiceMetric = "";
			this.Condition = "";
			this.Hidden = false;
			this.Elements.Clear();
		}

		public ExtensionChoiceElement GetDefaultElement()
		{
			ExtensionChoiceElement result;
			try
			{
				if (this.DefaultChoice == "")
				{
					result = this.Elements[0];
				}
				else
				{
					foreach (object obj in this.Elements.Collection)
					{
						ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)obj;
						if (extensionChoiceElement.Name == this.DefaultChoice)
						{
							return extensionChoiceElement;
						}
					}
					result = this.Elements[0];
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public ExtensionChoiceElement FindElement(string name)
		{
			foreach (object obj in this.Elements.Collection)
			{
				ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)obj;
				if (extensionChoiceElement.Name == name)
				{
					return extensionChoiceElement;
				}
			}
			return null;
		}

		public void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("Caption = " + this.Caption);
			Console.WriteLine("DefaultChoice= " + this.DefaultChoice);
			Console.WriteLine("DefaultChoiceImperial= " + this.DefaultChoiceImperial);
			Console.WriteLine("DefaultChoiceMetric= " + this.DefaultChoiceMetric);
			Console.WriteLine("Condition= " + this.Condition);
			Console.WriteLine("Hidden= " + this.Hidden);
			this.Elements.Dump();
		}

		private string defaultChoice;

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private string <DefaultChoiceImperial>k__BackingField;

		[CompilerGenerated]
		private string <DefaultChoiceMetric>k__BackingField;

		[CompilerGenerated]
		private string <Condition>k__BackingField;

		[CompilerGenerated]
		private bool <Hidden>k__BackingField;

		[CompilerGenerated]
		private ExtensionChoiceElements <Elements>k__BackingField;
	}
}
