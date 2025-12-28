using System;
using System.Collections;
using System.Text.RegularExpressions;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public static class FormulaUtilities
	{
		private static void ExtractBaseResults(GroupStats groupStats, ref Hashtable h)
		{
			string objectType;
			if ((objectType = groupStats.ObjectType) != null)
			{
				if (objectType == "Area")
				{
					h.Add("area", groupStats.Area.ToString());
					h.Add("substraction", groupStats.DeductionArea.ToString());
					h.Add("netarea", groupStats.AreaMinusDeduction.ToString());
					h.Add("perimeter", groupStats.Perimeter.ToString());
					h.Add("substractionsperimeter", groupStats.DeductionPerimeter.ToString());
					h.Add("substractionscount", groupStats.DeductionsCount.ToString());
					h.Add("objectscount", groupStats.GroupCount.ToString());
					return;
				}
				if (objectType == "Perimeter")
				{
					h.Add("length", groupStats.Perimeter.ToString());
					h.Add("openings", groupStats.DeductionPerimeter.ToString());
					h.Add("dropslength", groupStats.DropLength.ToString());
					h.Add("netlength", groupStats.NetLength.ToString());
					h.Add("dropscount", groupStats.DropsCount.ToString());
					h.Add("openingscount", groupStats.DeductionsCount.ToString());
					h.Add("cornerscount", groupStats.CornersCount.ToString());
					h.Add("endscount", groupStats.EndsCount.ToString());
					h.Add("segmentscount", groupStats.SegmentsCount.ToString());
					h.Add("objectscount", groupStats.GroupCount.ToString());
					return;
				}
				if (objectType == "Counter")
				{
					h.Add("objectscount", groupStats.GroupCount.ToString());
					return;
				}
				if (!(objectType == "Line"))
				{
					return;
				}
				h.Add("length", groupStats.Perimeter.ToString());
				h.Add("objectscount", groupStats.GroupCount.ToString());
			}
		}

		private static void ExtractPresetsResults(Presets presets, ref Hashtable h)
		{
			int num = 0;
			foreach (object obj in presets.Collection)
			{
				Preset preset = (Preset)obj;
				num++;
				foreach (object obj2 in preset.Results.Collection)
				{
					PresetResult presetResult = (PresetResult)obj2;
					if (presetResult.ConditionMet)
					{
						h.Add((presetResult.Name + num.ToString()).ToLower(), presetResult.Result.ToString());
					}
				}
			}
		}

		private static PresetResult FindResultByCaption(Preset preset, string resultCaption)
		{
			foreach (object obj in preset.Results.Collection)
			{
				PresetResult presetResult = (PresetResult)obj;
				if (presetResult.Caption == resultCaption && presetResult.ConditionMet)
				{
					return presetResult;
				}
			}
			return null;
		}

		public static bool ValidateFields(string formula, DrawObjectGroup group, ref string invalidField)
		{
			invalidField = "";
			string text = "\\[(.*?)\\]";
			MatchCollection matchCollection = Regex.Matches(formula, text);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				bool flag = false;
				string text2 = "";
				string text3 = match.Groups[1].ToString();
				string[] fields = Utilities.GetFields(text3, '.', StringSplitOptions.None);
				if (fields.Length > 1)
				{
					string displayName = fields.GetValue(0).ToString();
					for (int i = 1; i < fields.Length; i++)
					{
						text2 = text2 + fields.GetValue(i).ToString() + ".";
					}
					text2 = text2.Substring(0, text2.Length - 1);
					Preset preset = group.Presets.FindByDisplayName(displayName, "");
					if (preset != null)
					{
						flag = (FormulaUtilities.FindResultByCaption(preset, text2) != null);
					}
				}
				else
				{
					text2 = fields.GetValue(0).ToString();
					string objectType;
					if ((objectType = group.ObjectType) != null)
					{
						if (!(objectType == "Area"))
						{
							if (!(objectType == "Perimeter"))
							{
								if (!(objectType == "Counter"))
								{
									if (objectType == "Line")
									{
										flag = (text2 == Resources.Longueur || text2 == Resources.Nombre_d_objets);
									}
								}
								else
								{
									flag = (text2 == Resources.Nombre_d_objets);
								}
							}
							else
							{
								flag = (text2 == Resources.Longueur || text2 == Resources.Longueur_des_ouvertures || text2 == Resources.Drop_length || text2 == Resources.Longueur_nette || text2 == Resources.Drops_count || text2 == Resources.Nombre_d_ouvertures || text2 == Resources.Nombre_de_coins || text2 == Resources.Nombre_de_bouts || text2 == Resources.Nombre_de_segments || text2 == Resources.Nombre_d_objets);
							}
						}
						else
						{
							flag = (text2 == Resources.Surface || text2 == Resources.Déduction || text2 == Resources.Surface_déductions || text2 == Resources.Périmètre || text2 == Resources.Périmètre_des_déductions || text2 == Resources.Nombre_de_déductions || text2 == Resources.Nombre_d_objets);
						}
					}
				}
				if (!flag)
				{
					invalidField = "[" + text3 + "]";
					return false;
				}
			}
			return true;
		}

		public static void RenameExtension(DrawObjectGroup group, string oldDisplayName, string newDisplayName)
		{
			foreach (CEstimatingItem cestimatingItem in group.COfficeProducts.Collection)
			{
				string text = "\\[(.*?)\\]";
				string text2 = cestimatingItem.Formula;
				MatchCollection matchCollection = Regex.Matches(text2, text);
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					string text3 = "";
					string text4 = match.Groups[1].ToString();
					string[] fields = Utilities.GetFields(text4, '.', StringSplitOptions.None);
					if (fields.Length > 1)
					{
						string a = fields.GetValue(0).ToString();
						for (int i = 1; i < fields.Length; i++)
						{
							text3 = text3 + fields.GetValue(i).ToString() + ".";
						}
						text3 = text3.Substring(0, text3.Length - 1);
						if (a == oldDisplayName)
						{
							text2 = text2.Replace("[" + text4 + "]", string.Concat(new string[]
							{
								"[",
								newDisplayName,
								".",
								text3,
								"]"
							}));
						}
					}
				}
				cestimatingItem.Formula = text2;
			}
		}

		private static string FromFieldCaptionToName(string presetCaption, string fieldCaption, Presets presets)
		{
			string text = "";
			if (presetCaption == "")
			{
				text = fieldCaption;
				if (fieldCaption == Resources.Surface)
				{
					text = "area";
				}
				else if (fieldCaption == Resources.Déduction)
				{
					text = "substraction";
				}
				else if (fieldCaption == Resources.Surface_déductions)
				{
					text = "netarea";
				}
				else if (fieldCaption == Resources.Périmètre)
				{
					text = "perimeter";
				}
				else if (fieldCaption == Resources.Périmètre_des_déductions)
				{
					text = "substractionsperimeter";
				}
				else if (fieldCaption == Resources.Nombre_de_déductions)
				{
					text = "substractionscount";
				}
				else if (fieldCaption == Resources.Longueur)
				{
					text = "length";
				}
				else if (fieldCaption == Resources.Longueur_nette)
				{
					text = "netlength";
				}
				else if (fieldCaption == Resources.Longueur_des_ouvertures)
				{
					text = "openings";
				}
				else if (fieldCaption == Resources.Nombre_d_ouvertures)
				{
					text = "openingscount";
				}
				else if (fieldCaption == Resources.Drop_length)
				{
					text = "dropslength";
				}
				else if (fieldCaption == Resources.Drops_count)
				{
					text = "dropscount";
				}
				else if (fieldCaption == Resources.Nombre_de_coins)
				{
					text = "cornerscount";
				}
				else if (fieldCaption == Resources.Nombre_de_bouts)
				{
					text = "endscount";
				}
				else if (fieldCaption == Resources.Nombre_de_segments)
				{
					text = "segmentscount";
				}
				else if (fieldCaption == Resources.Nombre_d_objets)
				{
					text = "objectscount";
				}
			}
			else
			{
				text = presetCaption + "." + fieldCaption;
				int num = 0;
				foreach (object obj in presets.Collection)
				{
					Preset preset = (Preset)obj;
					num++;
					if (preset.DisplayName == presetCaption)
					{
						PresetResult presetResult = FormulaUtilities.FindResultByCaption(preset, fieldCaption);
						if (presetResult != null)
						{
							text = (presetResult.ConditionMet ? (presetResult.Name + num.ToString()).ToLower() : text);
						}
					}
				}
			}
			return text;
		}

		private static string ConvertDimensionsToValue(string formula, UnitScale.UnitSystem resultUnitSystem, string pattern)
		{
			MatchCollection matchCollection = Regex.Matches(formula, pattern);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string text = match.Groups[1].ToString();
				UnitScale.UnitSystem unitSystem = UnitScale.UnitSystem.undefined;
				double value = UnitScale.ConvertDimensionToValue(text, resultUnitSystem, ref unitSystem);
				if (unitSystem != resultUnitSystem)
				{
					switch (unitSystem)
					{
					case UnitScale.UnitSystem.metric:
						value = UnitScale.FromMetersToFeet(value);
						break;
					case UnitScale.UnitSystem.imperial:
						value = UnitScale.FromFeetToMeters(value);
						break;
					}
				}
				formula = formula.Replace(text, value.ToString());
			}
			return formula;
		}

		private static string ConvertDimensionsToValue(string formula, UnitScale.UnitSystem resultUnitSystem)
		{
			if (resultUnitSystem == UnitScale.UnitSystem.undefined)
			{
				return formula;
			}
			formula = FormulaUtilities.ConvertDimensionsToValue(formula, resultUnitSystem, "([0-9]*'[0-9]*\\\")");
			formula = FormulaUtilities.ConvertDimensionsToValue(formula, resultUnitSystem, "([0-9]*(\\.|,)?[0-9]+(cm|mm))");
			formula = FormulaUtilities.ConvertDimensionsToValue(formula, resultUnitSystem, "([0-9]*(\\.|,)?[0-9]+(m|'|\\\"))");
			return formula;
		}

		private static string SetForEvaluation(string formula, Presets presets)
		{
			string text = "\\[(.*?)\\]";
			MatchCollection matchCollection = Regex.Matches(formula, text);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string presetCaption = "";
				string text2 = "";
				string text3 = match.Groups[1].ToString();
				string[] fields = Utilities.GetFields(text3, '.', StringSplitOptions.None);
				if (fields.Length > 1)
				{
					presetCaption = fields.GetValue(0).ToString();
					for (int i = 1; i < fields.Length; i++)
					{
						text2 = text2 + fields.GetValue(i).ToString() + ".";
					}
					text2 = text2.Substring(0, text2.Length - 1);
				}
				else
				{
					text2 = fields.GetValue(0).ToString();
				}
				string text4 = FormulaUtilities.FromFieldCaptionToName(presetCaption, text2, presets);
				if (text3 != text4)
				{
					formula = formula.Replace("[" + text3 + "]", text4);
				}
			}
			string newValue = Utilities.NumberDecimalSeparator();
			formula = formula.Replace(",", newValue).Replace(".", newValue);
			return formula;
		}

		private static bool Evaluate(string expression, string displayExpression, Hashtable h, ref double result)
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
				Utilities.DisplayLogError(Resources.Incapable_d_évaluer_l_expression_suivante, displayExpression + Environment.NewLine + Environment.NewLine + ex.Message);
				result2 = false;
			}
			return result2;
		}

		public static bool Compute(string formula, Presets presets, GroupStats objectStats, UnitScale.UnitSystem resultUnitSystem, ref double result)
		{
			result = 0.0;
			Hashtable h = new Hashtable();
			FormulaUtilities.ExtractBaseResults(objectStats, ref h);
			FormulaUtilities.ExtractPresetsResults(presets, ref h);
			string displayExpression = formula;
			formula = FormulaUtilities.ConvertDimensionsToValue(formula, resultUnitSystem);
			formula = FormulaUtilities.SetForEvaluation(formula, presets);
			return FormulaUtilities.Evaluate(formula, displayExpression, h, ref result);
		}
	}
}
