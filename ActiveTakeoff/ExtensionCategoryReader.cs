using System;
using System.Xml;

namespace QuoterPlan
{
	public class ExtensionCategoryReader
	{
		public ExtensionCategoryReader()
		{
		}

		public ExtensionCategory Open(string fileName)
		{
			ExtensionCategory result;
			try
			{
				this.parserCategory = new ExtensionCategory();
				using (XmlTextReader xmlTextReader = new XmlTextReader(fileName))
				{
					this.ReadFromStream(xmlTextReader);
					xmlTextReader.Close();
					result = this.parserCategory;
				}
			}
			catch (Exception exception)
			{
				this.parserCategory.Clear();
				this.parserCategory = null;
				Utilities.DisplayFileOpenError(fileName, exception);
				result = null;
			}
			return result;
		}

		private ExtensionField.ExtensionFieldTypeEnum GetParserFieldType(string fieldType)
		{
			if (fieldType != null)
			{
				if (fieldType == "DIMENSION")
				{
					return ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension;
				}
				if (fieldType == "DOUBLE")
				{
					return ExtensionField.ExtensionFieldTypeEnum.FieldTypeDouble;
				}
				if (fieldType == "INTEGER")
				{
					return ExtensionField.ExtensionFieldTypeEnum.FieldTypeInteger;
				}
				if (fieldType == "CURRENCY")
				{
					return ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency;
				}
			}
			return ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension;
		}

		private void ExtractVariables(string variablesString, Variables variables)
		{
			string[] array = variablesString.Split(new string[]
			{
				";",
				" ",
				"\t"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				string[] array3 = text.Split(new string[]
				{
					"=",
					" ",
					"\t"
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array3.GetLength(0) == 2)
				{
					Variable variable = new Variable(array3.GetValue(0).ToString(), array3.GetValue(1).ToString());
					variables.Add(variable);
				}
			}
		}

		private void ReadFromStream(XmlTextReader reader)
		{
			string newValue = Utilities.NumberDecimalSeparator();
			while (reader.Read())
			{
				XmlNodeType nodeType = reader.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				{
					string key;
					switch (key = reader.Name.ToUpper())
					{
					case "CATEGORY":
						this.parserCategory.Name = Utilities.GetStringAttribute(reader, "name", "");
						this.parserCategory.Caption = Utilities.GetStringAttribute(reader, "caption", "");
						this.parserCategory.Division = Utilities.GetIntegerAttribute(reader, "division", 0);
						break;
					case "AREA":
						this.parserObjectType = "Area";
						break;
					case "PERIMETER":
						this.parserObjectType = "Perimeter";
						break;
					case "COUNTER":
						this.parserObjectType = "Counter";
						break;
					case "DISTANCE":
						this.parserObjectType = "Line";
						break;
					case "EXTENSION":
						this.parserExtension = new Extension(Utilities.GetStringAttribute(reader, "name", ""), Utilities.GetStringAttribute(reader, "caption", ""), this.parserObjectType, Utilities.GetBoolAttribute(reader, "hidden", false));
						this.parserCategory.Templates.Add(this.parserExtension);
						break;
					case "CONST":
					{
						UnitScale.UnitSystem unitSystem = (Utilities.GetStringAttribute(reader, "system", "").ToLower() == "metric") ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial;
						this.ExtractVariables(Utilities.GetStringAttribute(reader, "variables", ""), this.parserExtension.Variables[(int)unitSystem]);
						break;
					}
					case "CHOICE":
						this.parserChoice = new ExtensionChoice(Utilities.GetStringAttribute(reader, "name", ""), Utilities.GetStringAttribute(reader, "caption", ""), Utilities.GetStringAttribute(reader, "default", ""), Utilities.GetStringAttribute(reader, "defaultImperial", ""), Utilities.GetStringAttribute(reader, "defaultMetric", ""), ((this.parserCondition != null) ? Utilities.ReplaceWords(this.parserCondition, "AND,&&;OR,||;", true) : "").Replace(",", newValue).Replace(".", newValue), Utilities.GetBoolAttribute(reader, "hidden", false));
						this.parserExtension.Choices.Add(this.parserChoice);
						break;
					case "ELEMENT":
						this.parserChoiceElement = new ExtensionChoiceElement(Utilities.GetStringAttribute(reader, "name", ""), Utilities.GetStringAttribute(reader, "caption", ""), this.parserChoice);
						this.ExtractVariables(Utilities.GetStringAttribute(reader, "variables", ""), this.parserChoiceElement.Variables);
						this.parserChoice.Elements.Add(this.parserChoiceElement);
						break;
					case "DIMENSION":
					case "DOUBLE":
					case "INTEGER":
					case "CURRENCY":
						this.parserField = new ExtensionField(Utilities.GetStringAttribute(reader, "name", ""), Utilities.GetStringAttribute(reader, "caption", ""), ((this.parserCondition != null) ? Utilities.ReplaceWords(this.parserCondition, "AND,&&;OR,||;", true) : "").Replace(",", newValue).Replace(".", newValue), this.GetParserFieldType(reader.Name.ToUpper()));
						this.parserExtension.Fields.Add(this.parserField);
						break;
					case "CONDITION":
						this.parserCondition = Utilities.GetStringAttribute(reader, "value", "");
						break;
					case "RESULT":
					{
						bool isEstimatingItem = true;
						string attribute = reader.GetAttribute("estimatingItem");
						if (attribute != null)
						{
							isEstimatingItem = Utilities.ConvertToBoolean(attribute, true);
						}
						int num2 = Utilities.GetIntegerAttribute(reader, "itemID", -1);
						num2 = ((num2 <= 0) ? -1 : num2);
						int integerAttribute = Utilities.GetIntegerAttribute(reader, "sectionID", -1);
						int integerAttribute2 = Utilities.GetIntegerAttribute(reader, "subSectionID", -1);
						this.parserResult = new ExtensionResult(this.parserExtension.Results.GetIndexedName(Utilities.GetStringAttribute(reader, "name", "")), Utilities.GetStringAttribute(reader, "caption", ""), Utilities.GetStringAttribute(reader, "unit", ""), num2, integerAttribute, integerAttribute2, Utilities.ReplaceWords(Utilities.GetStringAttribute(reader, "formula", ""), "AND,&&;OR,||;", true).Replace(",", newValue).Replace(".", newValue), ((this.parserCondition != null) ? Utilities.ReplaceWords(this.parserCondition, "AND,&&;OR,||;", true) : "").Replace(",", newValue).Replace(".", newValue), (ExtensionResult.ExtensionResultTypeEnum)Utilities.GetIntegerAttribute(reader, "type", 0), Utilities.GetBoolAttribute(reader, "showInLegend", false), isEstimatingItem);
						this.parserExtension.Results.Add(this.parserResult);
						break;
					}
					}
					break;
				}
				case XmlNodeType.Attribute:
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType == XmlNodeType.EndElement)
					{
						string key2;
						switch (key2 = reader.Name.ToUpper())
						{
						case "AREA":
						case "PERIMETER":
						case "COUNTER":
						case "DISTANCE":
							this.parserObjectType = "";
							break;
						case "EXTENSION":
							this.parserExtension = null;
							break;
						case "CHOICE":
							this.parserChoice = null;
							break;
						case "ELEMENT":
							this.parserChoiceElement = null;
							break;
						case "DIMENSION":
						case "DOUBLE":
						case "INTEGER":
						case "CURRENCY":
							this.parserField = null;
							break;
						case "CONDITION":
							this.parserCondition = "";
							break;
						case "RESULT":
							this.parserResult = null;
							break;
						}
					}
					break;
				}
			}
		}

		private string parserObjectType;

		private string parserCondition;

		private ExtensionCategory parserCategory;

		private Extension parserExtension;

		private ExtensionChoice parserChoice;

		private ExtensionChoiceElement parserChoiceElement;

		private ExtensionField parserField;

		private ExtensionResult parserResult;
	}
}
