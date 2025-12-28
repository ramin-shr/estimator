using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class EstimatingItems
	{
		public EstimatingItems(Project project)
		{
			this.estimatingItemsList = new List<EstimatingItem>();
			this.estimatingPrices = new Hashtable();
			this.project = project;
		}

		public Hashtable EstimatingPrices
		{
			get
			{
				return this.estimatingPrices;
			}
		}

		public EstimatingItem this[int index]
		{
			get
			{
				if (index < 0 || index >= this.estimatingItemsList.Count)
				{
					return null;
				}
				return this.estimatingItemsList[index];
			}
		}

		public List<EstimatingItem> Collection
		{
			get
			{
				return this.estimatingItemsList;
			}
		}

		public void Add(EstimatingItem estimatingItem)
		{
			this.estimatingItemsList.Add(estimatingItem);
		}

		public int Count
		{
			get
			{
				return this.estimatingItemsList.Count;
			}
		}

		public void Clear()
		{
			this.estimatingItemsList.Clear();
		}

		public void ClearPrices()
		{
			this.estimatingPrices.Clear();
		}

		private EstimatingItemPrice QueryEstimatingItemPrice(EstimatingItem groupItem)
		{
			string key = EstimatingItemPrice.GenerateKey(groupItem);
			EstimatingItemPrice estimatingItemPrice;
			if (this.estimatingPrices.ContainsKey(key))
			{
				estimatingItemPrice = (EstimatingItemPrice)this.estimatingPrices[key];
			}
			else
			{
				estimatingItemPrice = new EstimatingItemPrice(groupItem, UnitScale.UnitSystem.undefined);
				DBEstimatingItem dbestimatingItem = null;
				if (groupItem.ItemID != -1)
				{
					dbestimatingItem = this.project.DBManagement.GetEstimatingItem(groupItem.ItemID);
				}
				if (dbestimatingItem != null)
				{
					estimatingItemPrice.CostEach = dbestimatingItem.PriceEach;
				}
				this.estimatingPrices.Add(key, estimatingItemPrice);
			}
			return estimatingItemPrice;
		}

		private void SynchEstimatingItemPrice(EstimatingItem groupItem)
		{
			EstimatingItemPrice estimatingItemPrice = this.QueryEstimatingItemPrice(groupItem);
			if (groupItem.InternalUnit == Utilities.GetCurrencySymbol())
			{
				groupItem.ResultValue = 1.0;
				groupItem.ResultUnit = Resources.chaque;
				groupItem.CostEach = groupItem.CostEach;
				groupItem.MarkupEach = estimatingItemPrice.MarkupEach;
				return;
			}
			groupItem.CostEach = estimatingItemPrice.CostEach;
			groupItem.MarkupEach = estimatingItemPrice.MarkupEach;
		}

		private void QueryObjectResults(DrawObject groupObject, List<EstimatingItem> results, ref int resultID, ref int resultParentID)
		{
			UnitScale.UnitSystem scaleSystemType = groupObject.DrawArea.UnitScale.ScaleSystemType;
			UnitScale.UnitPrecision precision = groupObject.DrawArea.UnitScale.Precision;
			UnitScale unitScale = new UnitScale(1f, scaleSystemType, precision, false);
			UnitScale.UnitSystem unitSystem = this.QueryEstimatingItemSystemType(groupObject.GroupID, "", groupObject.ObjectType, groupObject.DrawArea.UnitScale.CurrentSystemType, -1);
			UnitScale unitScale2 = new UnitScale(1f, unitSystem, precision, false);
			GroupStats groupStats = GroupUtilities.ComputeGroupStats(this.project, groupObject, null, scaleSystemType, true, "");
			resultID++;
			resultParentID = resultID;
			EstimatingItem estimatingItem = null;
			string objectType;
			if ((objectType = groupObject.ObjectType) != null)
			{
				if (!(objectType == "Line"))
				{
					if (!(objectType == "Area"))
					{
						if (!(objectType == "Perimeter"))
						{
							if (objectType == "Counter")
							{
								estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale2.Round((double)groupStats.GroupCount), Resources.unité_, 0.0, 0.0, -1);
							}
						}
						else
						{
							double value = (scaleSystemType == unitSystem) ? groupStats.NetLength : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(groupStats.NetLength) : UnitScale.FromFeetToMeters(groupStats.NetLength));
							estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale2.Round(value), (unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m", 0.0, 0.0, -1);
						}
					}
					else
					{
						double value2 = (scaleSystemType == unitSystem) ? groupStats.AreaMinusDeduction : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(groupStats.AreaMinusDeduction) : UnitScale.FromSquareFeetToSquareMeters(groupStats.AreaMinusDeduction));
						estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale2.Round(value2), (unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²", 0.0, 0.0, -1);
					}
				}
				else
				{
					double value3 = (scaleSystemType == unitSystem) ? groupStats.Perimeter : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(groupStats.Perimeter) : UnitScale.FromFeetToMeters(groupStats.Perimeter));
					estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale2.Round(value3), (unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m", 0.0, 0.0, -1);
				}
			}
			if (estimatingItem != null)
			{
				this.SynchEstimatingItemPrice(estimatingItem);
				results.Add(estimatingItem);
			}
			DrawObjectGroup group = groupObject.Group;
			if (group != null)
			{
				foreach (object obj in group.Presets.Collection)
				{
					Preset preset = (Preset)obj;
					new Variables();
					this.project.ExtensionsSupport.QueryPresetResults(preset, groupStats, unitScale);
					foreach (object obj2 in preset.Results.Collection)
					{
						PresetResult presetResult = (PresetResult)obj2;
						if (presetResult.ConditionMet)
						{
							double num = Utilities.ConvertToDouble(presetResult.Result.ToString(), -1);
							string text = string.Empty;
							string value4 = string.Empty;
							string text2 = string.Empty;
							if (presetResult.ResultType == ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency)
							{
								unitSystem = groupObject.DrawArea.UnitScale.CurrentSystemType;
							}
							else
							{
								unitSystem = this.QueryEstimatingItemSystemType(groupObject.GroupID, preset.ID, presetResult.Name, groupObject.DrawArea.UnitScale.CurrentSystemType, presetResult.ItemID);
							}
							unitScale2 = new UnitScale(1f, unitSystem, precision, false);
							DBEstimatingItem dbestimatingItem = null;
							if (presetResult.ItemID != -1)
							{
								dbestimatingItem = this.project.DBManagement.GetEstimatingItem(presetResult.ItemID);
							}
							bool flag = false;
							if (dbestimatingItem != null)
							{
								flag = dbestimatingItem.MatchResultType(presetResult.ResultType);
							}
							if (flag)
							{
								UnitScale unitScale3 = new UnitScale(1f, (dbestimatingItem.PurchaseUnit == "m" || dbestimatingItem.PurchaseUnit == "m²" || dbestimatingItem.PurchaseUnit == "m³") ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial, precision, false);
								unitSystem = unitScale3.ScaleSystemType;
								switch (presetResult.ResultType)
								{
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
									num = ((scaleSystemType == unitSystem) ? num : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num) : UnitScale.FromFeetToMeters(num)));
									text = unitScale3.ToLengthStringFromUnitSystem(num, false, true, true);
									value4 = unitScale3.ToLengthFromUnitSystem(num).ToString();
									text2 = dbestimatingItem.PurchaseUnit;
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
									num = ((scaleSystemType == unitSystem) ? num : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(num) : UnitScale.FromSquareFeetToSquareMeters(num)));
									text = unitScale3.ToAreaStringFromUnitSystem(num, true);
									value4 = unitScale3.ToAreaFromUnitSystem(num).ToString();
									text2 = dbestimatingItem.PurchaseUnit;
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
									num = ((scaleSystemType == unitSystem) ? num : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromCubicMetersToCubicFeet(num) : UnitScale.FromCubicFeetToCubicMeters(num)));
									text = unitScale3.ToCubicStringFromUnitSystem(num, true);
									value4 = unitScale3.ToCubicFromUnitSystem(num).ToString();
									text2 = dbestimatingItem.PurchaseUnit;
									break;
								default:
									text = unitScale3.Round(num).ToString();
									value4 = text.ToString();
									text2 = dbestimatingItem.PurchaseUnit;
									text += ((text2 != string.Empty) ? (" " + text2) : "");
									break;
								}
							}
							else
							{
								switch (presetResult.ResultType)
								{
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
									num = ((scaleSystemType == unitSystem) ? num : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num) : UnitScale.FromFeetToMeters(num)));
									text = unitScale.ToLengthStringFromUnitSystem(num, false, true, true);
									value4 = unitScale.ToLengthFromUnitSystem(num).ToString();
									text2 = ((unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
									num = ((scaleSystemType == unitSystem) ? num : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(num) : UnitScale.FromSquareFeetToSquareMeters(num)));
									text = unitScale.ToAreaStringFromUnitSystem(num, true);
									value4 = unitScale.ToAreaFromUnitSystem(num).ToString();
									text2 = ((unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²");
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
									num = ((scaleSystemType == unitSystem) ? num : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromCubicMetersToCubicFeet(num) : UnitScale.FromCubicFeetToCubicMeters(num)));
									text = unitScale.ToCubicStringFromUnitSystem(num, true);
									value4 = unitScale.ToCubicFromUnitSystem(num).ToString();
									text2 = ((unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi_3 : "m³");
									break;
								case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
									text = groupObject.DrawArea.ToCurrency(num);
									value4 = num.ToString();
									text2 = Utilities.GetCurrencySymbol();
									break;
								default:
									text = unitScale.Round(num).ToString();
									value4 = text.ToString();
									text2 = presetResult.Unit.ToLower();
									break;
								}
								text += ((text2 != string.Empty) ? (" " + text2) : "");
							}
							resultID++;
							if (presetResult.IsEstimatingItem)
							{
								string resultName = (dbestimatingItem != null) ? dbestimatingItem.Description : presetResult.Caption;
								EstimatingItem estimatingItem2 = new EstimatingItem(resultID, resultParentID, groupObject.GroupID, preset.ID, presetResult.Name, groupObject.ObjectType, resultName, unitScale.Round(Utilities.ConvertToDouble(value4, -1)), text2, 0.0, 0.0, presetResult.ItemID);
								this.SynchEstimatingItemPrice(estimatingItem2);
								results.Add(estimatingItem2);
							}
						}
					}
				}
				foreach (CEstimatingItem cestimatingItem in group.EstimatingItems.Collection)
				{
					if (cestimatingItem.Formula != "")
					{
						DBEstimatingItem estimatingItem3 = this.project.DBManagement.GetEstimatingItem(Utilities.ConvertToInt(cestimatingItem.ItemID));
						UnitScale.UnitSystem unitSystem2 = this.QueryEstimatingItemSystemType(groupObject.GroupID, cestimatingItem.InternalKey, cestimatingItem.ItemID, groupObject.DrawArea.UnitScale.CurrentSystemType, Utilities.ConvertToInt(cestimatingItem.ItemID));
						DBEstimatingItem.UnitMeasureType unitMeasureType = (estimatingItem3 != null) ? estimatingItem3.UnitMeasure : cestimatingItem.UnitMeasure;
						double num2 = (estimatingItem3 != null) ? estimatingItem3.CoverageRate : cestimatingItem.CoverageRate;
						if (unitMeasureType == DBEstimatingItem.UnitMeasureType.m || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_m || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_m || cestimatingItem.Unit.ToUpper() == "M" || cestimatingItem.Unit.ToUpper() == "M²" || cestimatingItem.Unit.ToUpper() == "M³")
						{
							unitSystem2 = UnitScale.UnitSystem.metric;
						}
						else if (unitMeasureType == DBEstimatingItem.UnitMeasureType.lin_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_yd || cestimatingItem.Unit.ToUpper() == Resources.pi.ToUpper() || cestimatingItem.Unit.ToUpper() == Resources.pi_2.ToUpper() || cestimatingItem.Unit.ToUpper() == Resources.pi_3.ToUpper() || cestimatingItem.Unit.ToUpper() == Resources.v_3.ToUpper())
						{
							unitSystem2 = UnitScale.UnitSystem.imperial;
						}
						GroupStats objectStats = GroupUtilities.ComputeGroupStats(this.project, groupObject, null, unitSystem2, true, "");
						foreach (object obj3 in groupObject.Group.Presets.Collection)
						{
							Preset preset2 = (Preset)obj3;
							UnitScale unitScale4 = new UnitScale(1f, unitSystem2, precision, false);
							this.project.ExtensionsSupport.QueryPresetResults(preset2, objectStats, unitScale4);
						}
						double num3 = 0.0;
						if (FormulaUtilities.Compute(cestimatingItem.Formula, group.Presets, objectStats, cestimatingItem.ResultSystemType(unitSystem2), ref num3))
						{
							num3 = ((num2 == 0.0) ? num3 : Math.Ceiling(num3 / num2));
							resultID++;
							EstimatingItem estimatingItem4 = new EstimatingItem(resultID, resultParentID, groupObject.GroupID, cestimatingItem.InternalKey, cestimatingItem.ItemID, groupObject.ObjectType, cestimatingItem.Description, unitScale.Round(num3), cestimatingItem.Unit, 0.0, 0.0, Utilities.ConvertToInt(cestimatingItem.ItemID));
							this.SynchEstimatingItemPrice(estimatingItem4);
							results.Add(estimatingItem4);
						}
					}
				}
			}
		}

		private void QueryObjectsResults(List<EstimatingItem> results, List<Variable> groups)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < groups.Count; i++)
			{
				this.QueryObjectResults((DrawObject)groups[i].Tag, results, ref num, ref num2);
			}
		}

		private void QueryObjectsResults(List<EstimatingItem> results)
		{
			List<Variable> list = new List<Variable>();
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					DrawObject layerObject;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						layerObject = (DrawObject)obj3;
						if (layerObject.IsPartOfGroup() && !layerObject.IsDeduction())
						{
							if (list.Find((Variable x) => Utilities.ConvertToInt(x.Value) == layerObject.GroupID) == null)
							{
								list.Add(new Variable(layerObject.Name, layerObject.GroupID, layerObject));
							}
						}
					}
				}
			}
			list.Sort(new EstimatingItems.GroupSorter());
			this.QueryObjectsResults(results, list);
			list.Clear();
			list = null;
		}

		public void CloneEstimatingItemsPrices(DrawObjectGroup oldGroup, DrawObjectGroup newGroup)
		{
			Hashtable hashtable = new Hashtable();
			foreach (object obj in this.estimatingPrices.Values)
			{
				EstimatingItemPrice estimatingItemPrice = (EstimatingItemPrice)obj;
				if (estimatingItemPrice.GroupID == oldGroup.ID)
				{
					hashtable.Add(estimatingItemPrice.Key, estimatingItemPrice);
				}
			}
			foreach (object obj2 in hashtable.Values)
			{
				EstimatingItemPrice estimatingItemPrice2 = (EstimatingItemPrice)obj2;
				string extensionID = "";
				bool flag = estimatingItemPrice2.ExtensionID == "";
				if (!flag)
				{
					if (Utilities.IsNumber(estimatingItemPrice2.ResultID))
					{
						CEstimatingItem cestimatingItem = newGroup.EstimatingItems.FindByInternalKey(estimatingItemPrice2.ExtensionID);
						if (cestimatingItem != null)
						{
							cestimatingItem.InternalKey = Guid.NewGuid().ToString();
							extensionID = cestimatingItem.InternalKey;
							flag = true;
						}
					}
					else
					{
						Preset preset = oldGroup.Presets.FindById(estimatingItemPrice2.ExtensionID);
						if (preset != null)
						{
							Preset preset2 = newGroup.Presets.FindByDisplayName(preset.DisplayName, "");
							if (preset2 != null)
							{
								extensionID = preset2.ID;
								flag = true;
							}
						}
					}
				}
				if (flag)
				{
					EstimatingItemPrice estimatingItemPrice3 = new EstimatingItemPrice(newGroup.ID, extensionID, estimatingItemPrice2.ResultID, estimatingItemPrice2.CostEach, estimatingItemPrice2.MarkupEach, estimatingItemPrice2.SystemType);
					if (!this.estimatingPrices.ContainsKey(estimatingItemPrice3.Key))
					{
						this.estimatingPrices.Add(estimatingItemPrice3.Key, estimatingItemPrice3);
					}
				}
			}
		}

		public UnitScale.UnitSystem QueryEstimatingItemSystemType(int groupdID, string extensionID, string resultID, UnitScale.UnitSystem defaultSystemType, int itemID = -1)
		{
			EstimatingItem groupItem = new EstimatingItem(0, 0, groupdID, extensionID, resultID, "", "", 0.0, "", 0.0, 0.0, itemID);
			EstimatingItemPrice estimatingItemPrice = this.QueryEstimatingItemPrice(groupItem);
			if (estimatingItemPrice == null)
			{
				return defaultSystemType;
			}
			if (estimatingItemPrice.CostEach == 0.0)
			{
				return defaultSystemType;
			}
			if (estimatingItemPrice.SystemType != UnitScale.UnitSystem.undefined)
			{
				return estimatingItemPrice.SystemType;
			}
			return defaultSystemType;
		}

		public EstimatingItemPrice QueryEstimatingItemPrice(int groupdID, string extensionID, string resultID)
		{
			EstimatingItem groupItem = new EstimatingItem(0, 0, groupdID, extensionID, resultID, "", "", 0.0, "", 0.0, 0.0, -1);
			return this.QueryEstimatingItemPrice(groupItem);
		}

		public void SaveEstimatingItemCost(int groupID, string extensionID, string resultID, double costEach, UnitScale.UnitSystem currentSystemType)
		{
			EstimatingItemPrice estimatingItemPrice = this.QueryEstimatingItemPrice(groupID, extensionID, resultID);
			estimatingItemPrice.CostEach = costEach;
			if (costEach == 0.0)
			{
				estimatingItemPrice.SystemType = UnitScale.UnitSystem.undefined;
				return;
			}
			estimatingItemPrice.SystemType = ((estimatingItemPrice.SystemType == UnitScale.UnitSystem.undefined) ? currentSystemType : estimatingItemPrice.SystemType);
		}

		public void SaveEstimatingItemMarkup(int groupID, string extensionID, string resultID, double markupEach)
		{
			EstimatingItemPrice estimatingItemPrice = this.QueryEstimatingItemPrice(groupID, extensionID, resultID);
			estimatingItemPrice.MarkupEach = markupEach;
		}

		public void Rename(DrawObject groupObject)
		{
			for (int i = 0; i < this.Count; i++)
			{
				EstimatingItem estimatingItem = this[i];
				if (estimatingItem.ParentID == 0 && estimatingItem.GroupID == groupObject.GroupID)
				{
					estimatingItem.ResultName = groupObject.Name;
				}
			}
		}

		public void Refresh(DrawObject groupObject)
		{
			int num = 0;
			int num2 = this.project.EstimatingItems.Count + 1;
			int num3 = num2;
			List<EstimatingItem> list = new List<EstimatingItem>();
			this.QueryObjectResults(groupObject, list, ref num2, ref num3);
			EstimatingItem newResult;
			foreach (EstimatingItem newResult2 in list)
			{
				newResult = newResult2;
				EstimatingItem estimatingItem = this.Collection.Find((EstimatingItem x) => x.GroupID == newResult.GroupID && x.ExtensionID == newResult.ExtensionID && x.ResultName == newResult.ResultName);
				if (estimatingItem != null)
				{
					if (newResult.ExtensionID == "")
					{
						estimatingItem.ResultName = groupObject.Name;
					}
					if (newResult.InternalUnit == Utilities.GetCurrencySymbol())
					{
						estimatingItem.ResultValue = 1.0;
						estimatingItem.CostEach = newResult.CostEach;
					}
					else
					{
						estimatingItem.ResultValue = newResult.ResultValue;
					}
				}
				num++;
			}
		}

		public void RefreshAll()
		{
			this.Clear();
			this.QueryObjectsResults(this.Collection);
		}

		public void Dump()
		{
			foreach (EstimatingItem estimatingItem in this.estimatingItemsList)
			{
				estimatingItem.Dump();
			}
		}

		private List<EstimatingItem> estimatingItemsList;

		private Hashtable estimatingPrices;

		private Project project;

		private class GroupSorter : IComparer<Variable>
		{
			public int Compare(Variable x, Variable y)
			{
				int result;
				try
				{
					DrawObject drawObject = x.Tag as DrawObject;
					DrawObject drawObject2 = y.Tag as DrawObject;
					if (drawObject.ObjectType != drawObject2.ObjectType)
					{
						result = StringLogicalComparer.Compare(drawObject.ObjectSortOrder.ToString(), drawObject2.ObjectSortOrder.ToString());
					}
					else
					{
						result = StringLogicalComparer.Compare(drawObject.Name, drawObject2.Name);
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}

			public GroupSorter()
			{
			}
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2
		{
			public <>c__DisplayClass2()
			{
			}

			public bool <QueryObjectsResults>b__0(Variable x)
			{
				return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
			}

			public DrawObject layerObject;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass6
		{
			public <>c__DisplayClass6()
			{
			}

			public bool <Refresh>b__4(EstimatingItem x)
			{
				return x.GroupID == this.newResult.GroupID && x.ExtensionID == this.newResult.ExtensionID && x.ResultName == this.newResult.ResultName;
			}

			public EstimatingItem newResult;
		}
	}
}
