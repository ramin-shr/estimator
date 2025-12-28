using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class EEExchangeData
	{
		public string ItemType
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemType>k__BackingField = value;
			}
		}

		public string ItemID
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemID>k__BackingField = value;
			}
		}

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

		public string Key
		{
			[CompilerGenerated]
			get
			{
				return this.<Key>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Key>k__BackingField = value;
			}
		}

		public string PersonalKey
		{
			[CompilerGenerated]
			get
			{
				return this.<PersonalKey>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PersonalKey>k__BackingField = value;
			}
		}

		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		public bool IsValid()
		{
			return this.ItemType != null && this.ItemType != "";
		}

		public void Clear()
		{
			this.ItemType = "";
			this.ItemID = "";
			this.Name = "";
			this.Key = "";
			this.PersonalKey = "";
			this.Description = "";
		}

		public void Clone(EEExchangeData source)
		{
			if (source != null)
			{
				this.ItemType = source.ItemType;
				this.ItemID = source.ItemID;
				this.Name = source.Name;
				this.Key = source.Key;
				this.PersonalKey = source.PersonalKey;
				this.Description = source.Description;
				return;
			}
			this.Clear();
		}

		public EEExchangeData()
		{
		}

		[CompilerGenerated]
		private string <ItemType>k__BackingField;

		[CompilerGenerated]
		private string <ItemID>k__BackingField;

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Key>k__BackingField;

		[CompilerGenerated]
		private string <PersonalKey>k__BackingField;

		[CompilerGenerated]
		private string <Description>k__BackingField;
	}
}
