using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class ExtensionField
	{
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

		public string Caption
		{
			[CompilerGenerated]
			get
			{
				return this.<Caption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Caption>k__BackingField = value;
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

		public ExtensionField.ExtensionFieldTypeEnum FieldType
		{
			[CompilerGenerated]
			get
			{
				return this.<FieldType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FieldType>k__BackingField = value;
			}
		}

		public ExtensionField()
		{
		}

		public ExtensionField(string name, string caption, string condition, ExtensionField.ExtensionFieldTypeEnum fieldType)
		{
			this.Name = name;
			this.Caption = caption;
			this.Condition = condition;
			this.FieldType = fieldType;
		}

		public void Clear()
		{
			this.Name = "";
			this.Caption = "";
			this.Condition = "";
		}

		public void Dump()
		{
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("Caption = " + this.Caption);
			Console.WriteLine("Condition = " + this.Condition);
			Console.WriteLine("FieldType = " + this.FieldType);
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private string <Condition>k__BackingField;

		[CompilerGenerated]
		private ExtensionField.ExtensionFieldTypeEnum <FieldType>k__BackingField;

		public enum ExtensionFieldTypeEnum
		{
			FieldTypeDimension,
			FieldTypeInteger,
			FieldTypeDouble,
			FieldTypeCurrency,
			FieldTypeEnumCount
		}
	}
}
