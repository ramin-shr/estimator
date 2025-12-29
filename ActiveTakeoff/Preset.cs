using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Preset
    {
        public string CategoryCaption
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            private set;
        }

        public PresetChoices Choices
        {
            get;
            private set;
        }

        public CustomRendering CustomRendering
        {
            get;
            private set;
        }

        public bool Dirty
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string ExtensionCaption
        {
            get;
            set;
        }

        public string ExtensionName
        {
            get;
            private set;
        }

        public PresetFields Fields
        {
            get;
            private set;
        }

        public string ID
        {
            get;
            set;
        }

        public PresetResults Results
        {
            get;
            private set;
        }

        public UnitScale.UnitSystem ScaleSystemType
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public Variables[] Variables
        {
            get;
            private set;
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

        public string ChoiceElement(string choiceName)
        {
            string choiceElementName;
            IEnumerator enumerator = this.Choices.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    PresetChoice current = (PresetChoice)enumerator.Current;
                    if (current.ChoiceName != choiceName)
                    {
                        continue;
                    }
                    choiceElementName = current.ChoiceElementName;
                    return choiceElementName;
                }
                return "";
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return choiceElementName;
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
            foreach (PresetChoice collection in this.Choices.Collection)
            {
                PresetChoice presetChoice = new PresetChoice(collection.ChoiceName, collection.ChoiceElementName);
                if (fullCloning)
                {
                    presetChoice.ChoiceCaption = collection.ChoiceCaption;
                    presetChoice.ChoiceElementCaption = collection.ChoiceElementCaption;
                    foreach (Variable variable in collection.Variables.Collection)
                    {
                        Variable variable1 = new Variable(variable.Name, variable.Value, variable.Tag);
                        presetChoice.Variables.Add(variable1);
                    }
                }
                preset.Choices.Add(presetChoice);
            }
            foreach (PresetField presetField in this.Fields.Collection)
            {
                PresetField caption = new PresetField(presetField.Name, presetField.Value);
                if (fullCloning)
                {
                    caption.Caption = presetField.Caption;
                    caption.FieldType = presetField.FieldType;
                }
                preset.Fields.Add(caption);
            }
            if (fullCloning)
            {
                preset.CategoryCaption = this.CategoryCaption;
                preset.ExtensionCaption = this.ExtensionCaption;
                for (int i = 0; i < 2; i++)
                {
                    foreach (Variable collection1 in this.Variables[i].Collection)
                    {
                        preset.Variables[i].Add(new Variable(collection1.Name, collection1.Value, collection1.Tag));
                    }
                }
                foreach (PresetResult presetResult in this.Results.Collection)
                {
                    preset.Results.Add(new PresetResult(presetResult.Name, presetResult.Caption, presetResult.Unit, presetResult.Condition, presetResult.Formula, presetResult.ResultType, presetResult.ShowInLegend, presetResult.IsEstimatingItem, presetResult.ItemID, presetResult.SectionID, presetResult.SubSectionID));
                }
            }
            preset.CustomRendering = this.CustomRendering;
            return preset;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("DisplayName = ", this.DisplayName));
            Console.WriteLine(string.Concat("Category = ", this.CategoryName));
            Console.WriteLine(string.Concat("Template = ", this.ExtensionName));
            Console.WriteLine(string.Concat("CategoryCaption = ", this.CategoryCaption));
            Console.WriteLine(string.Concat("TemplateCaption = ", this.ExtensionCaption));
            this.Choices.Dump();
            this.Fields.Dump();
            this.Results.Dump();
        }

        public string FieldValue(string fieldName)
        {
            string str;
            IEnumerator enumerator = this.Fields.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    PresetField current = (PresetField)enumerator.Current;
                    if (current.Name != fieldName)
                    {
                        continue;
                    }
                    str = current.Value.ToString();
                    return str;
                }
                return "0";
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return str;
        }

        public object FieldValue(string fieldName, object defaultValue)
        {
            object value;
            IEnumerator enumerator = this.Fields.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    PresetField current = (PresetField)enumerator.Current;
                    if (current.Name != fieldName)
                    {
                        continue;
                    }
                    value = current.Value;
                    return value;
                }
                return defaultValue;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return value;
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

        public void Reset(string displayName, string categoryName, string extensionName)
        {
            this.Clear();
            this.DisplayName = displayName;
            this.CategoryName = categoryName;
            this.ExtensionName = extensionName;
        }

        public void SetCustomRendering()
        {
            CustomRendering customRenderingGenericTile = null;
            string upper = this.ExtensionName.ToUpper();
            string str = upper;
            if (upper != null && (str == "GENERICTILE" || str == "CERAMICTILE"))
            {
                Console.WriteLine(string.Concat("Setting CustomRenderingGenericTiles for ID : ", this.ID));
                customRenderingGenericTile = new CustomRenderingGenericTiles(this);
            }
            this.CustomRendering = customRenderingGenericTile;
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
                foreach (Variable collection in extension.Variables[i].Collection)
                {
                    this.Variables[i].Add(new Variable(collection.Name, collection.Value));
                }
            }
            foreach (ExtensionChoice extensionChoice in extension.Choices.Collection)
            {
                if (!extensionSupport.QueryPresetCondition(this, extensionChoice.Condition))
                {
                    PresetChoice presetChoice = this.Choices.Find(extensionChoice.Name);
                    if (presetChoice == null)
                    {
                        continue;
                    }
                    this.Choices.Collection.Remove(presetChoice);
                }
                else
                {
                    PresetChoice item = this.Choices.Find(extensionChoice.Name);
                    ExtensionChoiceElement defaultElement = null;
                    if (item == null)
                    {
                        defaultElement = extensionChoice.GetDefaultElement();
                        if (defaultElement != null)
                        {
                            item = this.Choices[this.Choices.Add(new PresetChoice(extensionChoice.Name, defaultElement.Name))];
                        }
                    }
                    if (item == null)
                    {
                        continue;
                    }
                    item.ChoiceCaption = extensionChoice.Caption;
                    item.Variables.Clear();
                    if (defaultElement == null)
                    {
                        defaultElement = extensionChoice.FindElement(item.ChoiceElementName);
                    }
                    if (defaultElement == null)
                    {
                        continue;
                    }
                    item.ChoiceElementCaption = defaultElement.Caption;
                    foreach (Variable variable in defaultElement.Variables.Collection)
                    {
                        item.Variables.Add(new Variable(variable.Name, variable.Value));
                    }
                }
            }
            PresetFields presetField = new PresetFields();
            foreach (PresetField collection1 in this.Fields.Collection)
            {
                presetField.Add(new PresetField(collection1.Name, collection1.Value));
            }
            this.Fields.Clear();
            foreach (ExtensionField extensionField in extension.Fields.Collection)
            {
                if (!extensionSupport.QueryPresetCondition(this, extensionField.Condition))
                {
                    continue;
                }
                PresetField caption = presetField.Find(extensionField.Name);
                caption = (caption == null ? this.Fields[this.Fields.Add(new PresetField(extensionField.Name, (object)0))] : this.Fields[this.Fields.Add(new PresetField(caption.Name, caption.Value))]);
                if (caption == null)
                {
                    continue;
                }
                caption.Caption = extensionField.Caption;
                caption.FieldType = extensionField.FieldType;
            }
            this.Results.Clear();
            foreach (ExtensionResult extensionResult in extension.Results.Collection)
            {
                this.Results.Add(new PresetResult(extensionResult.Name, extensionResult.Caption, extensionResult.Unit, extensionResult.Condition, extensionResult.Formula, extensionResult.ResultType, extensionResult.ShowInLegend, extensionResult.IsEstimatingItem, extensionResult.ItemID, extensionResult.SectionID, extensionResult.SubSectionID));
            }
        }
    }
}