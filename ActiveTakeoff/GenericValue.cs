using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class GenericValue
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

		public GenericValue(object value)
		{
			this.Value = value;
			this.Dirty = false;
		}

		[CompilerGenerated]
		private object <Value>k__BackingField;

		[CompilerGenerated]
		private bool <Dirty>k__BackingField;
	}
}
