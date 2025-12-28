using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class FormulaResult
	{
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ID>k__BackingField = value;
			}
		}

		public int ParentID
		{
			[CompilerGenerated]
			get
			{
				return this.<ParentID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ParentID>k__BackingField = value;
			}
		}

		public Preset Preset
		{
			[CompilerGenerated]
			get
			{
				return this.<Preset>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Preset>k__BackingField = value;
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

		public string Unit
		{
			[CompilerGenerated]
			get
			{
				return this.<Unit>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Unit>k__BackingField = value;
			}
		}

		public object Tag
		{
			[CompilerGenerated]
			get
			{
				return this.<Tag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tag>k__BackingField = value;
			}
		}

		public FormulaResult(int id, int parentID, Preset preset, string name, string caption, string unit)
		{
			this.ID = id;
			this.ParentID = parentID;
			this.Preset = preset;
			this.Name = name;
			this.Caption = caption;
			this.Unit = unit;
			this.Tag = null;
		}

		public void Clear()
		{
			this.ID = 0;
			this.ParentID = 0;
			this.Preset = null;
			this.Name = "";
			this.Caption = "";
			this.Unit = "";
			this.Tag = null;
		}

		public string FormulaString()
		{
			if (this.Preset == null)
			{
				return "[" + this.Caption + "]";
			}
			return string.Concat(new string[]
			{
				"[",
				this.Preset.DisplayName,
				".",
				this.Caption,
				"]"
			});
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("ParentID = " + this.ParentID);
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("Caption = " + this.Caption);
			Console.WriteLine("Unit = " + this.Unit);
		}

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private int <ParentID>k__BackingField;

		[CompilerGenerated]
		private Preset <Preset>k__BackingField;

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <Caption>k__BackingField;

		[CompilerGenerated]
		private string <Unit>k__BackingField;

		[CompilerGenerated]
		private object <Tag>k__BackingField;
	}
}
