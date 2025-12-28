using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class PresetField : ExtensionField
	{
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

		public PresetField(string name, object value)
		{
			base.Name = name;
			this.Value = value;
		}

		public new void Clear()
		{
			base.Clear();
			this.Value = 0;
		}

		public new void Dump()
		{
			base.Dump();
			Console.WriteLine("Value = " + this.Value);
		}

		[CompilerGenerated]
		private object <Value>k__BackingField;
	}
}
