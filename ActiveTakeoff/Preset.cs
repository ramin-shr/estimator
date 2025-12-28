using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class Preset
	{
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

		public string ID
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

		public string DisplayName
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayName>k__BackingField = value;
			}
		}

		public string CategoryName
		{
			[CompilerGenerated]
			get
			{
				return this.<CategoryName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CategoryName>k__BackingField = value;
			}
		}

		public string CategoryCaption
		{
			[CompilerGenerated]
			get
			{
				return this.<CategoryCaption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CategoryCaption>k__BackingField = value;
			}
		}

		public string ExtensionName
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ExtensionName>k__BackingField = value;
			}
		}

		public string ExtensionCaption
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionCaption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExtensionCaption>k__BackingField = value;
			}
		}

		public Variables[] Variables
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

		public PresetChoices Choices
		{
			[CompilerGenerated]
			get
			{
				return this.<Choices>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Choices>k__BackingField = value;
			}
		}

		public PresetFields Fields
		{
			[CompilerGenerated]
			get
			{
				return this.<Fields>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Fields>k__BackingField = value;
			}
		}

		public PresetResults Results
		{
			[CompilerGenerated]
			get
			{
				return this.<Results>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Results>k__BackingField = value;
			}
		}

		public UnitScale.UnitSystem ScaleSystemType
		{
			[CompilerGenerated]
			get
			{
				return this.<ScaleSystemType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ScaleSystemType>k__BackingField = value;
			}
		}

		public CustomRendering CustomRendering
		{
			[CompilerGenerated]
			get
			{
				return this.<CustomRendering>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CustomRendering>k__BackingField = value;
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

		private void Initialize()
		{
			this.Dirty = false;
			this.ID = "";
			this.DisplayName = "";
			this.CategoryCaption = "";
			this.ExtensionCaption = "";
			this.Variables[0] = new Variables();
			this.Variables[1] = new Variables();
			this.Choices = new PresetChoices();
			this.Fields = new PresetFields();
			this.Results = new PresetResults();
			this.ScaleSystemType = UnitScale.UnitSystem.undefined;
		}

		public Preset(UnitScale.UnitSystem scaleSystemType)
		{
			this.Variables = new Variables[2];
			this.Initialize();
			this.ID = Guid.NewGuid().ToString();
			this.DisplayName = "";
			this.CategoryName = "";
			this.ExtensionName = "";
			this.ScaleSystemType = scaleSystemType;
		}

		public Preset(string id, string displayName, string categoryName, string extensionName, UnitScale.UnitSystem scaleSystemType)
		{
			this.Variables = new Variables[2];
			this.Initialize();
			this.ID = id;
			this.DisplayName = displayName;
			this.CategoryName = categoryName;
			this.ExtensionName = extensionName;
			this.ScaleSystemType = scaleSystemType;
		}

		public void Reset(string displayName, string categoryName, string extensionName)
		{
			this.Clear();
			this.DisplayName = displayName;
			this.CategoryName = categoryName;
			this.ExtensionName = extensionName;
		}

		public void Clear()
		{
			this.Dirty = false;
			this.DisplayName = "";
			this.CategoryName = "";
			this.ExtensionName = "";
			this.CategoryCaption = "";
			this.ExtensionCaption = "";
			this.Variables[0].Clear();
			this.Variables[1].Clear();
			this.Choices.Clear();
			this.Fields.Clear();
			this.Results.Clear();
		}

		public Preset Clone(bool fullCloning = false)
		{
			Preset preset = new Preset(this.ID, this.DisplayName, this.CategoryName, this.ExtensionName, this.ScaleSystemType);
			foreach (object obj in this.Choices.Collection)
			{
				PresetChoice presetChoice = (PresetChoice)obj;
				PresetChoice presetChoice2 = new PresetChoice(presetChoice.ChoiceName, presetChoice.ChoiceElementName);
				if (fullCloning)
				{
					presetChoice2.ChoiceCaption = presetChoice.ChoiceCaption;
					presetChoice2.ChoiceElementCaption = presetChoice.ChoiceElementCaption;
					foreach (object obj2 in presetChoice.Variables.Collection)
					{
						Variable variable = (Variable)obj2;
						Variable variable2 = new Variable(variable.Name, variable.Value, variable.Tag);
						presetChoice2.Variables.Add(variable2);
					}
				}
				preset.Choices.Add(presetChoice2);
			}
			foreach (object obj3 in this.Fields.Collection)
			{
				PresetField presetField = (PresetField)obj3;
				PresetField presetField2 = new PresetField(presetField.Name, presetField.Value);
				if (fullCloning)
				{
					presetField2.Caption = presetField.Caption;
					presetField2.FieldType = presetField.FieldType;
				}
				preset.Fields.Add(presetField2);
			}
			if (fullCloning)
			{
				preset.CategoryCaption = this.CategoryCaption;
				preset.ExtensionCaption = this.ExtensionCaption;
				for (int i = 0; i < 2; i++)
				{
					foreach (object obj4 in this.Variables[i].Collection)
					{
						Variable variable3 = (Variable)obj4;
						preset.Variables[i].Add(new Variable(variable3.Name, variable3.Value, variable3.Tag));
					}
				}
				foreach (object obj5 in this.Results.Collection)
				{
					PresetResult presetResult = (PresetResult)obj5;
					preset.Results.Add(new PresetResult(presetResult.Name, presetResult.Caption, presetResult.Unit, presetResult.Condition, presetResult.Formula, presetResult.ResultType, presetResult.ShowInLegend, presetResult.IsEstimatingItem, presetResult.ItemID, presetResult.SectionID, presetResult.SubSectionID));
				}
			}
			preset.CustomRendering = this.CustomRendering;
			return preset;
		}

		public void SetCustomRendering()
		{
			CustomRendering customRendering = null;
			string a;
			if ((a = this.ExtensionName.ToUpper()) != null && (a == "GENERICTILE" || a == "CERAMICTILE"))
			{
				Console.WriteLine("Setting CustomRenderingGenericTiles for ID : " + this.ID);
				customRendering = new CustomRenderingGenericTiles(this);
			}
			this.CustomRendering = customRendering;
		}

		public string ChoiceElement(string choiceName)
		{
			foreach (object obj in this.Choices.Collection)
			{
				PresetChoice presetChoice = (PresetChoice)obj;
				if (presetChoice.ChoiceName == choiceName)
				{
					return presetChoice.ChoiceElementName;
				}
			}
			return "";
		}

		public string FieldValue(string fieldName)
		{
			foreach (object obj in this.Fields.Collection)
			{
				PresetField presetField = (PresetField)obj;
				if (presetField.Name == fieldName)
				{
					return presetField.Value.ToString();
				}
			}
			return "0";
		}

		public object FieldValue(string fieldName, object defaultValue)
		{
			foreach (object obj in this.Fields.Collection)
			{
				PresetField presetField = (PresetField)obj;
				if (presetField.Name == fieldName)
				{
					return presetField.Value;
				}
			}
			return defaultValue;
		}

		public void SynchWithTemplate(ExtensionsSupport extensionSupport)
		{
			ExtensionCategory extensionCategory = null;
			Extension extension = extensionSupport.FindExtension(ref extensionCategory, this.ExtensionName);
			if (extension == null)
			{
				return;
			}
			this.CategoryCaption = extensionCategory.Caption;
			this.ExtensionCaption = extension.Caption;
			for (int i = 0; i < 2; i++)
			{
				this.Variables[i].Clear();
				foreach (object obj in extension.Variables[i].Collection)
				{
					Variable variable = (Variable)obj;
					this.Variables[i].Add(new Variable(variable.Name, variable.Value));
				}
			}
			foreach (object obj2 in extension.Choices.Collection)
			{
				ExtensionChoice extensionChoice = (ExtensionChoice)obj2;
				if (extensionSupport.QueryPresetCondition(this, extensionChoice.Condition))
				{
					PresetChoice presetChoice = this.Choices.Find(extensionChoice.Name);
					ExtensionChoiceElement extensionChoiceElement = null;
					if (presetChoice == null)
					{
						extensionChoiceElement = extensionChoice.GetDefaultElement();
						if (extensionChoiceElement != null)
						{
							presetChoice = this.Choices[this.Choices.Add(new PresetChoice(extensionChoice.Name, extensionChoiceElement.Name))];
						}
					}
					if (presetChoice == null)
					{
						continue;
					}
					presetChoice.ChoiceCaption = extensionChoice.Caption;
					presetChoice.Variables.Clear();
					if (extensionChoiceElement == null)
					{
						extensionChoiceElement = extensionChoice.FindElement(presetChoice.ChoiceElementName);
					}
					if (extensionChoiceElement == null)
					{
						continue;
					}
					presetChoice.ChoiceElementCaption = extensionChoiceElement.Caption;
					using (IEnumerator enumerator3 = extensionChoiceElement.Variables.Collection.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							object obj3 = enumerator3.Current;
							Variable variable2 = (Variable)obj3;
							presetChoice.Variables.Add(new Variable(variable2.Name, variable2.Value));
						}
						continue;
					}
				}
				PresetChoice presetChoice2 = this.Choices.Find(extensionChoice.Name);
				if (presetChoice2 != null)
				{
					this.Choices.Collection.Remove(presetChoice2);
				}
			}
			PresetFields presetFields = new PresetFields();
			foreach (object obj4 in this.Fields.Collection)
			{
				PresetField presetField = (PresetField)obj4;
				presetFields.Add(new PresetField(presetField.Name, presetField.Value));
			}
			this.Fields.Clear();
			foreach (object obj5 in extension.Fields.Collection)
			{
				ExtensionField extensionField = (ExtensionField)obj5;
				if (extensionSupport.QueryPresetCondition(this, extensionField.Condition))
				{
					PresetField presetField2 = presetFields.Find(extensionField.Name);
					if (presetField2 != null)
					{
						presetField2 = this.Fields[this.Fields.Add(new PresetField(presetField2.Name, presetField2.Value))];
					}
					else
					{
						presetField2 = this.Fields[this.Fields.Add(new PresetField(extensionField.Name, 0))];
					}
					if (presetField2 != null)
					{
						presetField2.Caption = extensionField.Caption;
						presetField2.FieldType = extensionField.FieldType;
					}
				}
			}
			this.Results.Clear();
			foreach (object obj6 in extension.Results.Collection)
			{
				ExtensionResult extensionResult = (ExtensionResult)obj6;
				this.Results.Add(new PresetResult(extensionResult.Name, extensionResult.Caption, extensionResult.Unit, extensionResult.Condition, extensionResult.Formula, extensionResult.ResultType, extensionResult.ShowInLegend, extensionResult.IsEstimatingItem, extensionResult.ItemID, extensionResult.SectionID, extensionResult.SubSectionID));
			}
		}

		public void Dump()
		{
			Console.WriteLine("DisplayName = " + this.DisplayName);
			Console.WriteLine("Category = " + this.CategoryName);
			Console.WriteLine("Template = " + this.ExtensionName);
			Console.WriteLine("CategoryCaption = " + this.CategoryCaption);
			Console.WriteLine("TemplateCaption = " + this.ExtensionCaption);
			this.Choices.Dump();
			this.Fields.Dump();
			this.Results.Dump();
		}

		[CompilerGenerated]
		private bool <Dirty>k__BackingField;

		[CompilerGenerated]
		private string <ID>k__BackingField;

		[CompilerGenerated]
		private string <DisplayName>k__BackingField;

		[CompilerGenerated]
		private string <CategoryName>k__BackingField;

		[CompilerGenerated]
		private string <CategoryCaption>k__BackingField;

		[CompilerGenerated]
		private string <ExtensionName>k__BackingField;

		[CompilerGenerated]
		private string <ExtensionCaption>k__BackingField;

		[CompilerGenerated]
		private Variables[] <Variables>k__BackingField;

		[CompilerGenerated]
		private PresetChoices <Choices>k__BackingField;

		[CompilerGenerated]
		private PresetFields <Fields>k__BackingField;

		[CompilerGenerated]
		private PresetResults <Results>k__BackingField;

		[CompilerGenerated]
		private UnitScale.UnitSystem <ScaleSystemType>k__BackingField;

		[CompilerGenerated]
		private CustomRendering <CustomRendering>k__BackingField;

		[CompilerGenerated]
		private object <Tag>k__BackingField;
	}
}
