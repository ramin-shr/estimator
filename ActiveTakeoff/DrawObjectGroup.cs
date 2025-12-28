using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class DrawObjectGroup
	{
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.<ID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ID>k__BackingField = value;
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

		public string ObjectType
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ObjectType>k__BackingField = value;
			}
		}

		public Color Color
		{
			[CompilerGenerated]
			get
			{
				return this.<Color>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Color>k__BackingField = value;
			}
		}

		public string TemplateID
		{
			[CompilerGenerated]
			get
			{
				return this.<TemplateID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TemplateID>k__BackingField = value;
			}
		}

		public string BasicInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<BasicInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BasicInfo>k__BackingField = value;
			}
		}

		public bool Deleted
		{
			[CompilerGenerated]
			get
			{
				return this.<Deleted>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Deleted>k__BackingField = value;
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

		public Presets Presets
		{
			[CompilerGenerated]
			get
			{
				return this.<Presets>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Presets>k__BackingField = value;
			}
		}

		public Preset SelectedPreset
		{
			[CompilerGenerated]
			get
			{
				return this.<SelectedPreset>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SelectedPreset>k__BackingField = value;
			}
		}

		public Preset SelectedRenderingPreset
		{
			[CompilerGenerated]
			get
			{
				return this.<SelectedRenderingPreset>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SelectedRenderingPreset>k__BackingField = value;
			}
		}

		public CEstimatingItems EstimatingItems
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatingItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EstimatingItems>k__BackingField = value;
			}
		}

		public CEstimatingItems COfficeProducts
		{
			[CompilerGenerated]
			get
			{
				return this.<COfficeProducts>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<COfficeProducts>k__BackingField = value;
			}
		}

		public DrawObjectGroup(int id, string template)
		{
			this.ID = id;
			this.Name = "";
			this.ObjectType = "";
			this.TemplateID = template;
			this.BasicInfo = "";
			this.Deleted = false;
			this.Presets = new Presets();
			this.SelectedPreset = null;
			this.SelectedRenderingPreset = null;
			this.EstimatingItems = new CEstimatingItems();
			this.COfficeProducts = new CEstimatingItems();
		}

		public DrawObjectGroup(int id, string name, string objectType, string template)
		{
			this.ID = id;
			this.Name = name;
			this.ObjectType = objectType;
			this.TemplateID = template;
			this.BasicInfo = "";
			this.Deleted = false;
			this.Presets = new Presets();
			this.SelectedPreset = null;
			this.SelectedRenderingPreset = null;
			this.EstimatingItems = new CEstimatingItems();
			this.COfficeProducts = new CEstimatingItems();
		}

		public DrawObjectGroup(int id, string name, string objectType, Color color, string basicInfo, object tag)
		{
			this.ID = id;
			this.Name = name;
			this.ObjectType = objectType;
			this.Color = color;
			this.TemplateID = "";
			this.BasicInfo = basicInfo;
			this.Tag = tag;
			this.Deleted = false;
			this.Presets = new Presets();
			this.SelectedPreset = null;
			this.SelectedRenderingPreset = null;
			this.EstimatingItems = new CEstimatingItems();
			this.COfficeProducts = new CEstimatingItems();
		}

		public void Clear()
		{
			this.ID = -1;
			this.Name = "";
			this.ObjectType = "";
			this.TemplateID = "";
			this.BasicInfo = "";
			this.Tag = null;
			this.Presets.Clear();
			this.SelectedPreset = null;
			this.SelectedRenderingPreset = null;
			this.EstimatingItems = new CEstimatingItems();
			this.COfficeProducts = new CEstimatingItems();
		}

		public void Dump()
		{
			Console.WriteLine("ID = " + this.ID);
			Console.WriteLine("Name = " + this.Name);
			Console.WriteLine("ObjectType = " + this.ObjectType);
			Console.WriteLine("Color = " + this.Color);
			Console.WriteLine("Template = " + this.TemplateID);
			Console.WriteLine("BasicInfo = " + this.BasicInfo);
			Console.WriteLine("Deleted = " + this.Deleted);
			this.Presets.Dump();
		}

		[CompilerGenerated]
		private int <ID>k__BackingField;

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private string <ObjectType>k__BackingField;

		[CompilerGenerated]
		private Color <Color>k__BackingField;

		[CompilerGenerated]
		private string <TemplateID>k__BackingField;

		[CompilerGenerated]
		private string <BasicInfo>k__BackingField;

		[CompilerGenerated]
		private bool <Deleted>k__BackingField;

		[CompilerGenerated]
		private object <Tag>k__BackingField;

		[CompilerGenerated]
		private Presets <Presets>k__BackingField;

		[CompilerGenerated]
		private Preset <SelectedPreset>k__BackingField;

		[CompilerGenerated]
		private Preset <SelectedRenderingPreset>k__BackingField;

		[CompilerGenerated]
		private CEstimatingItems <EstimatingItems>k__BackingField;

		[CompilerGenerated]
		private CEstimatingItems <COfficeProducts>k__BackingField;
	}
}
