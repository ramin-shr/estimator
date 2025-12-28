using System;
using System.Collections;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class ExtensionsSupport
	{
		public ExtensionCategories Categories
		{
			get
			{
				return this.categories;
			}
		}

		public ExtensionsSupport()
		{
			this.categories = new ExtensionCategories();
			this.CreateDefaultCategories();
		}

		public void Clear()
		{
			this.categories.Clear();
			this.CreateDefaultCategories();
		}

		private void CreateDefaultCategories()
		{
			ExtensionCategory category = this.CreateCategory("Generic", Resources.Générique, 0);
			this.CreateExtension(category, "GenericArea", Resources.Surface_générique, "Area");
			this.CreateExtension(category, "GenericPerimeter", Resources.Périmètre_générique, "Perimeter");
			this.CreateExtension(category, "GenericCounter", Resources.Compteur_générique, "Counter");
			this.CreateExtension(category, "GenericDistance", Resources.Distance_générique, "Line");
		}

		public void LoadCategory(string fileName)
		{
			ExtensionCategoryReader extensionCategoryReader = new ExtensionCategoryReader();
			ExtensionCategory extensionCategory = extensionCategoryReader.Open(fileName);
			if (extensionCategory != null)
			{
				ExtensionCategory extensionCategory2 = this.FindCategory(extensionCategory.Name);
				if (extensionCategory2 == null)
				{
					this.categories.Add(extensionCategory);
					return;
				}
				foreach (object obj in extensionCategory.Templates.Collection)
				{
					Extension extension = (Extension)obj;
					extensionCategory2.Templates.Add(extension);
				}
			}
		}

		public Extension CreateExtension(ExtensionCategory category, string name, string caption, string objectType)
		{
			Extension extension = new Extension(name, caption, objectType, false);
			category.Templates.Add(extension);
			return extension;
		}

		public ExtensionCategory CreateCategory(string name, string caption, int division)
		{
			ExtensionCategory extensionCategory = new ExtensionCategory(name, caption, division);
			this.categories.Add(extensionCategory);
			return extensionCategory;
		}

		public ExtensionCategory FindCategory(string name)
		{
			foreach (object obj in this.categories.Collection)
			{
				ExtensionCategory extensionCategory = (ExtensionCategory)obj;
				if (extensionCategory.Name == name)
				{
					return extensionCategory;
				}
			}
			return null;
		}

		public Extension FindExtension(ref ExtensionCategory category, string name)
		{
			foreach (object obj in this.categories.Collection)
			{
				ExtensionCategory extensionCategory = (ExtensionCategory)obj;
				foreach (object obj2 in extensionCategory.Templates.Collection)
				{
					Extension extension = (Extension)obj2;
					if (extension.Name == name)
					{
						category = extensionCategory;
						return extension;
					}
				}
			}
			return null;
		}

		public Extension FindTemplate(string categoryName, string templateName)
		{
			ExtensionCategory extensionCategory = null;
			return this.FindExtension(ref extensionCategory, templateName);
		}

		public ExtensionChoice FindChoice(string categoryName, string templateName, string choiceName)
		{
			ExtensionCategory extensionCategory = null;
			Extension extension = this.FindExtension(ref extensionCategory, templateName);
			if (extension == null)
			{
				return null;
			}
			foreach (object obj in extension.Choices.Collection)
			{
				ExtensionChoice extensionChoice = (ExtensionChoice)obj;
				if (extensionChoice.Name == choiceName)
				{
					return extensionChoice;
				}
			}
			return null;
		}

		private void ExtractGlobalVariables(Preset preset, ref Hashtable h, UnitScale unitScale)
		{
			foreach (object obj in preset.Variables[(int)unitScale.ScaleSystemType].Collection)
			{
				Variable variable = (Variable)obj;
				h.Add(variable.Name.ToLower(), Utilities.ConvertToDouble(variable.Value, -1).ToString());
			}
		}

		private void ExtractChoicesVariables(Preset preset, ref Hashtable h)
		{
			foreach (object obj in preset.Choices.Collection)
			{
				PresetChoice presetChoice = (PresetChoice)obj;
				foreach (object obj2 in presetChoice.Variables.Collection)
				{
					Variable variable = (Variable)obj2;
					string text = variable.Value.ToString();
					if (text[0] == '$')
					{
						string key = text.Substring(1, text.Length - 1);
						if (h[key] != null)
						{
							text = h[key].ToString();
						}
					}
					try
					{
						h.Add(variable.Name.ToLower(), Utilities.ConvertToDouble(text, -1).ToString());
					}
					catch (Exception)
					{
					}
				}
			}
		}

		private void ExtractFieldsVariables(Preset preset, ref Hashtable h, UnitScale unitScale)
		{
			foreach (object obj in preset.Fields.Collection)
			{
				PresetField presetField = (PresetField)obj;
				double value = Utilities.ConvertToDouble(presetField.Value.ToString(), -1);
				ExtensionField.ExtensionFieldTypeEnum fieldType = presetField.FieldType;
				if (fieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension && preset.ScaleSystemType != unitScale.ScaleSystemType)
				{
					value = ((unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(value) : UnitScale.FromFeetToMeters(value));
				}
				h.Add(presetField.Name.ToLower(), value.ToString());
			}
		}

		private void ExtractPresetVariables(Preset preset, ref Hashtable h, UnitScale unitScale)
		{
			this.ExtractGlobalVariables(preset, ref h, unitScale);
			this.ExtractChoicesVariables(preset, ref h);
			this.ExtractFieldsVariables(preset, ref h, unitScale);
		}

		private void ExtractObjectVariables(GroupStats groupStats, ref Hashtable h)
		{
			string objectType;
			if ((objectType = groupStats.ObjectType) != null)
			{
				if (objectType == "Line")
				{
					h.Add("length", groupStats.Perimeter.ToString());
					return;
				}
				if (objectType == "Area")
				{
					h.Add("area", groupStats.Area.ToString());
					h.Add("perimeter", groupStats.Perimeter.ToString());
					h.Add("netarea", groupStats.AreaMinusDeduction.ToString());
					h.Add("deduction", groupStats.DeductionArea.ToString());
					h.Add("deductionscount", groupStats.DeductionsCount.ToString());
					h.Add("totalperimeter", groupStats.PerimeterPlusDeduction.ToString());
					return;
				}
				if (objectType == "Perimeter")
				{
					h.Add("perimeter", groupStats.PerimeterPlusDrop.ToString());
					h.Add("openings", groupStats.DeductionPerimeter.ToString());
					h.Add("deduction", groupStats.DeductionArea.ToString());
					h.Add("netperimeter", groupStats.NetLength.ToString());
					h.Add("openingscount", groupStats.DeductionsCount.ToString());
					h.Add("cornerscount", groupStats.CornersCount.ToString());
					h.Add("endscount", groupStats.EndsCount.ToString());
					h.Add("segmentscount", groupStats.SegmentsCount.ToString());
					return;
				}
				if (!(objectType == "Counter"))
				{
					return;
				}
				h.Add("count", groupStats.GroupCount.ToString());
			}
		}

		private bool ConditionIsValid(string condition, Hashtable h)
		{
			double num = 0.0;
			return condition == "" || (this.Evaluate(condition, h, ref num) && Utilities.ConvertToBoolean(num, false));
		}

		private bool Evaluate(string expression, Hashtable h, ref double result)
		{
			MathParser mathParser = new MathParser();
			bool result2;
			try
			{
				result = mathParser.Parse(expression, h);
				result2 = true;
			}
			catch (MathParserException ex)
			{
				Utilities.DisplayLogError(Resources.Incapable_d_évaluer_l_expression_suivante, expression + Environment.NewLine + Environment.NewLine + ex.Message);
				result2 = false;
			}
			return result2;
		}

		public bool QueryPresetCondition(Preset preset, string condition)
		{
			if (condition != string.Empty)
			{
				Hashtable h = new Hashtable();
				this.ExtractChoicesVariables(preset, ref h);
				return this.ConditionIsValid(condition, h);
			}
			return true;
		}

		private double EvaluateRenderingResult(string extensionID, string resultName, GroupStats objectStats, UnitScale unitScale)
		{
			double result = 0.0;
			CustomRenderingResult customRenderingResult = objectStats.RenderingResult(extensionID, resultName);
			if (customRenderingResult != null)
			{
				result = customRenderingResult.Result;
			}
			return result;
		}

		public void QueryPresetResults(Preset preset, GroupStats objectStats, UnitScale unitScale)
		{
			Hashtable h = new Hashtable();
			this.ExtractObjectVariables(objectStats, ref h);
			this.ExtractPresetVariables(preset, ref h, unitScale);
			foreach (object obj in preset.Results.Collection)
			{
				PresetResult presetResult = (PresetResult)obj;
				presetResult.Result = 0.0;
				presetResult.ConditionMet = this.ConditionIsValid(presetResult.Condition, h);
				if (presetResult.ConditionMet)
				{
					double result = 0.0;
					if (presetResult.Formula != string.Empty)
					{
						if (this.Evaluate(presetResult.Formula, h, ref result))
						{
							presetResult.Result = result;
						}
					}
					else
					{
						presetResult.Result = this.EvaluateRenderingResult(preset.ID, presetResult.Name, objectStats, unitScale);
						presetResult.ConditionMet = !double.IsInfinity(presetResult.Result);
					}
				}
			}
		}

		public void Dump()
		{
			this.categories.Dump();
		}

		private ExtensionCategories categories;
	}
}
