using System;
using System.Collections.Generic;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class FormulaResults
	{
		public FormulaResults()
		{
			this.formulaResultsList = new List<FormulaResult>();
		}

		public FormulaResult this[int index]
		{
			get
			{
				if (index < 0 || index >= this.formulaResultsList.Count)
				{
					return null;
				}
				return this.formulaResultsList[index];
			}
		}

		public List<FormulaResult> Collection
		{
			get
			{
				return this.formulaResultsList;
			}
		}

		public void Add(FormulaResult formulaResult)
		{
			this.formulaResultsList.Add(formulaResult);
		}

		public int Count
		{
			get
			{
				return this.formulaResultsList.Count;
			}
		}

		public void Clear()
		{
			this.formulaResultsList.Clear();
		}

		private int InsertValue(int parentID, Preset preset, string name, string caption, string unit)
		{
			int num = this.formulaResultsList.Count + 1;
			FormulaResult formulaResult = new FormulaResult(num, parentID, preset, name, caption, unit);
			formulaResult.Tag = formulaResult;
			this.formulaResultsList.Add(formulaResult);
			return num;
		}

		private void QueryValues(DrawObject drawObject, GroupStats stats, UnitScale.UnitSystem currentSystemType, UnitScale unitScale, Presets presets, ExtensionsSupport extensionsSupport)
		{
			int parentID = this.InsertValue(-1, null, "", Resources.Résultats, "");
			string objectType;
			if ((objectType = drawObject.ObjectType) != null)
			{
				if (!(objectType == "Area"))
				{
					if (!(objectType == "Perimeter"))
					{
						if (!(objectType == "Counter"))
						{
							if (objectType == "Line")
							{
								this.InsertValue(parentID, null, "Length", Resources.Longueur, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
								this.InsertValue(parentID, null, "GroupsCount", Resources.Nombre_d_objets, Resources.Unité);
							}
						}
						else
						{
							this.InsertValue(parentID, null, "GroupsCount", Resources.Nombre_d_objets, Resources.Unité);
						}
					}
					else
					{
						this.InsertValue(parentID, null, "Length", Resources.Longueur, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
						this.InsertValue(parentID, null, "Openings", Resources.Longueur_des_ouvertures, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
						this.InsertValue(parentID, null, "DropsLength", Resources.Drop_length, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
						this.InsertValue(parentID, null, "NetLength", Resources.Longueur_nette, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
						this.InsertValue(parentID, null, "DropsCount", Resources.Drops_count, Resources.Unité);
						this.InsertValue(parentID, null, "OpeningsCount", Resources.Nombre_d_ouvertures, Resources.Unité);
						this.InsertValue(parentID, null, "CornersCount", Resources.Nombre_de_coins, Resources.Unité);
						this.InsertValue(parentID, null, "EndsCount", Resources.Nombre_de_bouts, Resources.Unité);
						this.InsertValue(parentID, null, "SegmentsCount", Resources.Nombre_de_segments, Resources.Unité);
						this.InsertValue(parentID, null, "GroupsCount", Resources.Nombre_d_objets, Resources.Unité);
					}
				}
				else
				{
					this.InsertValue(parentID, null, "Area", Resources.Surface, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²");
					this.InsertValue(parentID, null, "Deduction", Resources.Déduction, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²");
					this.InsertValue(parentID, null, "AreaMinusDeduction", Resources.Surface_déductions, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²");
					this.InsertValue(parentID, null, "Perimeter", Resources.Périmètre, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
					this.InsertValue(parentID, null, "DeductionsPerimeter", Resources.Périmètre_des_déductions, (currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
					this.InsertValue(parentID, null, "DeductionsCount", Resources.Nombre_de_déductions, Resources.Unité);
					this.InsertValue(parentID, null, "GroupsCount", Resources.Nombre_d_objets, Resources.Unité);
				}
			}
			foreach (object obj in presets.Collection)
			{
				Preset preset = (Preset)obj;
				extensionsSupport.QueryPresetResults(preset, stats, unitScale);
				parentID = this.InsertValue(-1, preset, "", preset.DisplayName, "");
				foreach (object obj2 in preset.Results.Collection)
				{
					PresetResult presetResult = (PresetResult)obj2;
					if (presetResult.ConditionMet)
					{
						string unit;
						switch (presetResult.ResultType)
						{
						case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
							unit = ((currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
							break;
						case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
							unit = ((currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²");
							break;
						case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
							unit = ((currentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi_3 : "m³");
							break;
						case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
							unit = Resources.chaque;
							break;
						default:
							unit = presetResult.Unit.ToLower();
							break;
						}
						this.InsertValue(parentID, preset, presetResult.Name, presetResult.Caption, unit);
					}
				}
			}
		}

		public void Refresh(DrawObject drawObject, GroupStats stats, UnitScale.UnitSystem currentSystemType, UnitScale unitScale, Presets presets, ExtensionsSupport extensionsSupport)
		{
			this.Clear();
			this.QueryValues(drawObject, stats, currentSystemType, unitScale, presets, extensionsSupport);
		}

		public void Dump()
		{
			foreach (FormulaResult formulaResult in this.formulaResultsList)
			{
				formulaResult.Dump();
			}
		}

		private List<FormulaResult> formulaResultsList;
	}
}
