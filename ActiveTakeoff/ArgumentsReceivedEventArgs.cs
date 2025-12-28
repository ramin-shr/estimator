using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ArgumentsReceivedEventArgs : EventArgs
	{
		public string[] Args
		{
			[CompilerGenerated]
			get
			{
				return this.<Args>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Args>k__BackingField = value;
			}
		}

		public ArgumentsReceivedEventArgs()
		{
		}

		[CompilerGenerated]
		private string[] <Args>k__BackingField;
	}
}
