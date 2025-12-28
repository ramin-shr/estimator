using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class BaseFileInfo : BaseInfo
	{
		public string FullFileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FullFileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FullFileName>k__BackingField = value;
			}
		}

		public string FileName
		{
			get
			{
				return Utilities.GetFileName(this.FullFileName, false);
			}
		}

		public string FolderName
		{
			get
			{
				return Utilities.GetDirectoryName(this.FullFileName);
			}
		}

		public BaseFileInfo()
		{
			this.FullFileName = string.Empty;
		}

		public override void Clear()
		{
			base.Clear();
			this.FullFileName = string.Empty;
		}

		public override void Dump()
		{
			base.Dump();
			Console.WriteLine("FullFileName = " + this.FullFileName);
		}

		[CompilerGenerated]
		private string <FullFileName>k__BackingField;
	}
}
