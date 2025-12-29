using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class EstimatingItems
    {
        private List<EstimatingItem> estimatingItemsList;

        private Hashtable estimatingPrices;

        private Project project;

        public List<EstimatingItem> Collection
        {
            get
            {
                return this.estimatingItemsList;
            }
        }

        public int Count
        {
            get
            {
                return this.estimatingItemsList.Count;
            }
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

        public EstimatingItems(Project project)
        {
            this.estimatingItemsList = new List<EstimatingItem>();
            this.estimatingPrices = new Hashtable();
            this.project = project;
        }

        public void Add(EstimatingItem estimatingItem)
        {
            this.estimatingItemsList.Add(estimatingItem);
        }

        public void Clear()
        {
            this.estimatingItemsList.Clear();
        }

        public void ClearPrices()
        {
            this.estimatingPrices.Clear();
        }

        public void CloneEstimatingItemsPrices(DrawObjectGroup oldGroup, DrawObjectGroup newGroup)
        {
            Hashtable hashtables = new Hashtable();
            foreach (EstimatingItemPrice value in this.estimatingPrices.Values)
            {
                if (value.GroupID != oldGroup.ID)
                {
                    continue;
                }
                hashtables.Add(value.Key, value);
            }
            foreach (EstimatingItemPrice estimatingItemPrice in hashtables.Values)
            {
                bool extensionID = false;
                string d = "";
                extensionID = estimatingItemPrice.ExtensionID == "";
                if (!extensionID)
                {
                    if (!Utilities.IsNumber(estimatingItemPrice.ResultID))
                    {
                        Preset preset = oldGroup.Presets.FindById(estimatingItemPrice.ExtensionID);
                        if (preset != null)
                        {
                            Preset preset1 = newGroup.Presets.FindByDisplayName(preset.DisplayName, "");
                            if (preset1 != null)
                            {
                                d = preset1.ID;
                                extensionID = true;
                            }
                        }
                    }
                    else
                    {
                        CEstimatingItem str = newGroup.EstimatingItems.FindByInternalKey(estimatingItemPrice.ExtensionID);
                        if (str != null)
                        {
                            str.InternalKey = Guid.NewGuid().ToString();
                            d = str.InternalKey;
                            extensionID = true;
                        }
                    }
                }
                if (!extensionID)
                {
                    continue;
                }
                EstimatingItemPrice estimatingItemPrice1 = new EstimatingItemPrice(newGroup.ID, d, estimatingItemPrice.ResultID, estimatingItemPrice.CostEach, estimatingItemPrice.MarkupEach, estimatingItemPrice.SystemType);
                if (this.estimatingPrices.ContainsKey(estimatingItemPrice1.Key))
                {
                    continue;
                }
                this.estimatingPrices.Add(estimatingItemPrice1.Key, estimatingItemPrice1);
            }
        }

        public void Dump()
        {
            foreach (EstimatingItem estimatingItem in this.estimatingItemsList)
            {
                estimatingItem.Dump();
            }
        }

        private EstimatingItemPrice QueryEstimatingItemPrice(EstimatingItem groupItem)
        {
            EstimatingItemPrice estimatingItemPrice = null;
            string str = EstimatingItemPrice.GenerateKey(groupItem);
            if (!this.estimatingPrices.ContainsKey(str))
            {
                estimatingItemPrice = new EstimatingItemPrice(groupItem, UnitScale.UnitSystem.undefined);
                DBEstimatingItem estimatingItem = null;
                if (groupItem.ItemID != -1)
                {
                    estimatingItem = this.project.DBManagement.GetEstimatingItem(groupItem.ItemID);
                }
                if (estimatingItem != null)
                {
                    estimatingItemPrice.CostEach = estimatingItem.PriceEach;
                }
                this.estimatingPrices.Add(str, estimatingItemPrice);
            }
            else
            {
                estimatingItemPrice = (EstimatingItemPrice)this.estimatingPrices[str];
            }
            return estimatingItemPrice;
        }

        public EstimatingItemPrice QueryEstimatingItemPrice(int groupdID, string extensionID, string resultID)
        {
            EstimatingItem estimatingItem = new EstimatingItem(0, 0, groupdID, extensionID, resultID, "", "", 0, "", 0, 0, -1);
            return this.QueryEstimatingItemPrice(estimatingItem);
        }

        public UnitScale.UnitSystem QueryEstimatingItemSystemType(int groupdID, string extensionID, string resultID, UnitScale.UnitSystem defaultSystemType, int itemID = -1)
        {
            EstimatingItem estimatingItem = new EstimatingItem(0, 0, groupdID, extensionID, resultID, "", "", 0, "", 0, 0, itemID);
            EstimatingItemPrice estimatingItemPrice = this.QueryEstimatingItemPrice(estimatingItem);
            if (estimatingItemPrice == null)
            {
                return defaultSystemType;
            }
            if (estimatingItemPrice.CostEach == 0)
            {
                return defaultSystemType;
            }
            if (estimatingItemPrice.SystemType == UnitScale.UnitSystem.undefined)
            {
                return defaultSystemType;
            }
            return estimatingItemPrice.SystemType;
        }

        private void QueryObjectResults(DrawObject groupObject, List<EstimatingItem> results, ref int resultID, ref int resultParentID)
        {
            double num;
            double num1;
            double num2;
            double num3;
            double num4;
            double num5;
            double perimeter;
            double areaMinusDeduction;
            double netLength;
            UnitScale.UnitSystem scaleSystemType = groupObject.DrawArea.UnitScale.ScaleSystemType;
            UnitScale.UnitPrecision precision = groupObject.DrawArea.UnitScale.Precision;
            UnitScale unitScale = new UnitScale(1f, scaleSystemType, precision, false);
            UnitScale.UnitSystem unitSystem = this.QueryEstimatingItemSystemType(groupObject.GroupID, "", groupObject.ObjectType, groupObject.DrawArea.UnitScale.CurrentSystemType, -1);
            UnitScale unitScale1 = new UnitScale(1f, unitSystem, precision, false);
            GroupStats groupStat = GroupUtilities.ComputeGroupStats(this.project, groupObject, null, scaleSystemType, true, "");
            resultID++;
            resultParentID = resultID;
            EstimatingItem estimatingItem = null;
            string objectType = groupObject.ObjectType;
            string str = objectType;
            if (objectType != null)
            {
                if (str == "Line")
                {
                    if (scaleSystemType == unitSystem)
                    {
                        perimeter = groupStat.Perimeter;
                    }
                    else
                    {
                        perimeter = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(groupStat.Perimeter) : UnitScale.FromFeetToMeters(groupStat.Perimeter));
                    }
                    double num6 = perimeter;
                    estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale1.Round(num6), (unitSystem == UnitScale.UnitSystem.imperial ? Resources.pi : "m"), 0, 0, -1);
                }
                else if (str == "Area")
                {
                    if (scaleSystemType == unitSystem)
                    {
                        areaMinusDeduction = groupStat.AreaMinusDeduction;
                    }
                    else
                    {
                        areaMinusDeduction = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet(groupStat.AreaMinusDeduction) : UnitScale.FromSquareFeetToSquareMeters(groupStat.AreaMinusDeduction));
                    }
                    double num7 = areaMinusDeduction;
                    estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale1.Round(num7), (unitSystem == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²"), 0, 0, -1);
                }
                else if (str == "Perimeter")
                {
                    if (scaleSystemType == unitSystem)
                    {
                        netLength = groupStat.NetLength;
                    }
                    else
                    {
                        netLength = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(groupStat.NetLength) : UnitScale.FromFeetToMeters(groupStat.NetLength));
                    }
                    double num8 = netLength;
                    estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale1.Round(num8), (unitSystem == UnitScale.UnitSystem.imperial ? Resources.pi : "m"), 0, 0, -1);
                }
                else if (str == "Counter")
                {
                    estimatingItem = new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, unitScale1.Round((double)groupStat.GroupCount), Resources.unité_, 0, 0, -1);
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
                foreach (Preset collection in group.Presets.Collection)
                {
                    Variables variable = new Variables();
                    this.project.ExtensionsSupport.QueryPresetResults(collection, groupStat, unitScale);
                    foreach (PresetResult presetResult in collection.Results.Collection)
                    {
                        if (!presetResult.ConditionMet)
                        {
                            continue;
                        }
                        double result = presetResult.Result;
                        double num9 = Utilities.ConvertToDouble(result.ToString(), -1);
                        string empty = string.Empty;
                        string empty1 = string.Empty;
                        string currencySymbol = string.Empty;
                        unitSystem = (presetResult.ResultType != ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency ? this.QueryEstimatingItemSystemType(groupObject.GroupID, collection.ID, presetResult.Name, groupObject.DrawArea.UnitScale.CurrentSystemType, presetResult.ItemID) : groupObject.DrawArea.UnitScale.CurrentSystemType);
                        unitScale1 = new UnitScale(1f, unitSystem, precision, false);
                        DBEstimatingItem dBEstimatingItem = null;
                        if (presetResult.ItemID != -1)
                        {
                            dBEstimatingItem = this.project.DBManagement.GetEstimatingItem(presetResult.ItemID);
                        }
                        bool flag = false;
                        if (dBEstimatingItem != null)
                        {
                            flag = dBEstimatingItem.MatchResultType(presetResult.ResultType);
                        }
                        if (!flag)
                        {
                            switch (presetResult.ResultType)
                            {
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                                    {
                                        if (scaleSystemType == unitSystem)
                                        {
                                            num = num9;
                                        }
                                        else
                                        {
                                            num = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num9) : UnitScale.FromFeetToMeters(num9));
                                        }
                                        num9 = num;
                                        empty = unitScale.ToLengthStringFromUnitSystem(num9, false, true, true);
                                        empty1 = unitScale.ToLengthFromUnitSystem(num9).ToString();
                                        currencySymbol = (unitSystem == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                                    {
                                        if (scaleSystemType == unitSystem)
                                        {
                                            num1 = num9;
                                        }
                                        else
                                        {
                                            num1 = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet(num9) : UnitScale.FromSquareFeetToSquareMeters(num9));
                                        }
                                        num9 = num1;
                                        empty = unitScale.ToAreaStringFromUnitSystem(num9, true);
                                        empty1 = unitScale.ToAreaFromUnitSystem(num9).ToString();
                                        currencySymbol = (unitSystem == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²");
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                                    {
                                        if (scaleSystemType == unitSystem)
                                        {
                                            num2 = num9;
                                        }
                                        else
                                        {
                                            num2 = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromCubicMetersToCubicFeet(num9) : UnitScale.FromCubicFeetToCubicMeters(num9));
                                        }
                                        num9 = num2;
                                        empty = unitScale.ToCubicStringFromUnitSystem(num9, true);
                                        empty1 = unitScale.ToCubicFromUnitSystem(num9).ToString();
                                        currencySymbol = (unitSystem == UnitScale.UnitSystem.imperial ? Resources.pi_3 : "m³");
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
                                    {
                                        empty = groupObject.DrawArea.ToCurrency(num9);
                                        empty1 = num9.ToString();
                                        currencySymbol = Utilities.GetCurrencySymbol();
                                        break;
                                    }
                                default:
                                    {
                                        empty = unitScale.Round(num9).ToString();
                                        empty1 = empty.ToString();
                                        currencySymbol = presetResult.Unit.ToLower();
                                        break;
                                    }
                            }
                            empty = string.Concat(empty, (currencySymbol != string.Empty ? string.Concat(" ", currencySymbol) : ""));
                        }
                        else
                        {
                            UnitScale unitScale2 = new UnitScale(1f, (dBEstimatingItem.PurchaseUnit == "m" || dBEstimatingItem.PurchaseUnit == "m²" || dBEstimatingItem.PurchaseUnit == "m³" ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial), precision, false);
                            unitSystem = unitScale2.ScaleSystemType;
                            switch (presetResult.ResultType)
                            {
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                                    {
                                        if (scaleSystemType == unitSystem)
                                        {
                                            num3 = num9;
                                        }
                                        else
                                        {
                                            num3 = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num9) : UnitScale.FromFeetToMeters(num9));
                                        }
                                        num9 = num3;
                                        empty = unitScale2.ToLengthStringFromUnitSystem(num9, false, true, true);
                                        empty1 = unitScale2.ToLengthFromUnitSystem(num9).ToString();
                                        currencySymbol = dBEstimatingItem.PurchaseUnit;
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                                    {
                                        if (scaleSystemType == unitSystem)
                                        {
                                            num4 = num9;
                                        }
                                        else
                                        {
                                            num4 = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet(num9) : UnitScale.FromSquareFeetToSquareMeters(num9));
                                        }
                                        num9 = num4;
                                        empty = unitScale2.ToAreaStringFromUnitSystem(num9, true);
                                        empty1 = unitScale2.ToAreaFromUnitSystem(num9).ToString();
                                        currencySymbol = dBEstimatingItem.PurchaseUnit;
                                        break;
                                    }
                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                                    {
                                        if (scaleSystemType == unitSystem)
                                        {
                                            num5 = num9;
                                        }
                                        else
                                        {
                                            num5 = (unitSystem == UnitScale.UnitSystem.imperial ? UnitScale.FromCubicMetersToCubicFeet(num9) : UnitScale.FromCubicFeetToCubicMeters(num9));
                                        }
                                        num9 = num5;
                                        empty = unitScale2.ToCubicStringFromUnitSystem(num9, true);
                                        empty1 = unitScale2.ToCubicFromUnitSystem(num9).ToString();
                                        currencySymbol = dBEstimatingItem.PurchaseUnit;
                                        break;
                                    }
                                default:
                                    {
                                        empty = unitScale2.Round(num9).ToString();
                                        empty1 = empty.ToString();
                                        currencySymbol = dBEstimatingItem.PurchaseUnit;
                                        empty = string.Concat(empty, (currencySymbol != string.Empty ? string.Concat(" ", currencySymbol) : ""));
                                        break;
                                    }
                            }
                        }
                        resultID++;
                        if (!presetResult.IsEstimatingItem)
                        {
                            continue;
                        }
                        string str1 = (dBEstimatingItem != null ? dBEstimatingItem.Description : presetResult.Caption);
                        EstimatingItem estimatingItem1 = new EstimatingItem(resultID, resultParentID, groupObject.GroupID, collection.ID, presetResult.Name, groupObject.ObjectType, str1, unitScale.Round(Utilities.ConvertToDouble(empty1, -1)), currencySymbol, 0, 0, presetResult.ItemID);
                        this.SynchEstimatingItemPrice(estimatingItem1);
                        results.Add(estimatingItem1);
                    }
                }
                foreach (CEstimatingItem cEstimatingItem in group.EstimatingItems.Collection)
                {
                    if (cEstimatingItem.Formula == "")
                    {
                        continue;
                    }
                    DBEstimatingItem dBEstimatingItem1 = this.project.DBManagement.GetEstimatingItem(Utilities.ConvertToInt(cEstimatingItem.ItemID));
                    UnitScale.UnitSystem unitSystem1 = this.QueryEstimatingItemSystemType(groupObject.GroupID, cEstimatingItem.InternalKey, cEstimatingItem.ItemID, groupObject.DrawArea.UnitScale.CurrentSystemType, Utilities.ConvertToInt(cEstimatingItem.ItemID));
                    DBEstimatingItem.UnitMeasureType unitMeasureType = (dBEstimatingItem1 != null ? dBEstimatingItem1.UnitMeasure : cEstimatingItem.UnitMeasure);
                    double num10 = (dBEstimatingItem1 != null ? dBEstimatingItem1.CoverageRate : cEstimatingItem.CoverageRate);
                    if (unitMeasureType == DBEstimatingItem.UnitMeasureType.m || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_m || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_m || cEstimatingItem.Unit.ToUpper() == "M" || cEstimatingItem.Unit.ToUpper() == "M²" || cEstimatingItem.Unit.ToUpper() == "M³")
                    {
                        unitSystem1 = UnitScale.UnitSystem.metric;
                    }
                    else if (unitMeasureType == DBEstimatingItem.UnitMeasureType.lin_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_yd || cEstimatingItem.Unit.ToUpper() == Resources.pi.ToUpper() || cEstimatingItem.Unit.ToUpper() == Resources.pi_2.ToUpper() || cEstimatingItem.Unit.ToUpper() == Resources.pi_3.ToUpper() || cEstimatingItem.Unit.ToUpper() == Resources.v_3.ToUpper())
                    {
                        unitSystem1 = UnitScale.UnitSystem.imperial;
                    }
                    GroupStats groupStat1 = GroupUtilities.ComputeGroupStats(this.project, groupObject, null, unitSystem1, true, "");
                    foreach (Preset preset in groupObject.Group.Presets.Collection)
                    {
                        UnitScale unitScale3 = new UnitScale(1f, unitSystem1, precision, false);
                        this.project.ExtensionsSupport.QueryPresetResults(preset, groupStat1, unitScale3);
                    }
                    double num11 = 0;
                    if (!FormulaUtilities.Compute(cEstimatingItem.Formula, group.Presets, groupStat1, cEstimatingItem.ResultSystemType(unitSystem1), ref num11))
                    {
                        continue;
                    }
                    num11 = (num10 == 0 ? num11 : Math.Ceiling(num11 / num10));
                    resultID++;
                    EstimatingItem estimatingItem2 = new EstimatingItem(resultID, resultParentID, groupObject.GroupID, cEstimatingItem.InternalKey, cEstimatingItem.ItemID, groupObject.ObjectType, cEstimatingItem.Description, unitScale.Round(num11), cEstimatingItem.Unit, 0, 0, Utilities.ConvertToInt(cEstimatingItem.ItemID));
                    this.SynchEstimatingItemPrice(estimatingItem2);
                    results.Add(estimatingItem2);
                }
            }
        }

        private void QueryObjectsResults(List<EstimatingItem> results, List<Variable> groups)
        {
            int num = 0;
            int num1 = 0;
            for (int i = 0; i < groups.Count; i++)
            {
                this.QueryObjectResults((DrawObject)groups[i].Tag, results, ref num, ref num1);
            }
        }

        private void QueryObjectsResults(List<EstimatingItem> results)
        {
            List<Variable> variables = new List<Variable>();
            foreach (Plan collection in this.project.Plans.Collection)
            {
                foreach (Layer layer in collection.Layers.Collection)
                {
                    foreach (DrawObject drawObject in layer.DrawingObjects.Collection)
                    {
                        if (!drawObject.IsPartOfGroup() || drawObject.IsDeduction())
                        {
                            continue;
                        }
                        if (variables.Find((Variable x) => Utilities.ConvertToInt(x.Value) == drawObject.GroupID) != null)
                        {
                            continue;
                        }
                        variables.Add(new Variable(drawObject.Name, (object)drawObject.GroupID, drawObject));
                    }
                }
            }
            variables.Sort(new EstimatingItems.GroupSorter());
            this.QueryObjectsResults(results, variables);
            variables.Clear();
            variables = null;
        }

        public void Refresh(DrawObject groupObject)
        {
            int num = 0;
            int count = this.project.EstimatingItems.Count + 1;
            int num1 = count;
            List<EstimatingItem> estimatingItems = new List<EstimatingItem>();
            this.QueryObjectResults(groupObject, estimatingItems, ref count, ref num1);
            foreach (EstimatingItem estimatingItem in estimatingItems)
            {
                EstimatingItem name = this.Collection.Find((EstimatingItem x) => {
                    if (x.GroupID != estimatingItem.GroupID || !(x.ExtensionID == estimatingItem.ExtensionID))
                    {
                        return false;
                    }
                    return x.ResultName == estimatingItem.ResultName;
                });
                if (name != null)
                {
                    if (estimatingItem.ExtensionID == "")
                    {
                        name.ResultName = groupObject.Name;
                    }
                    if (estimatingItem.InternalUnit != Utilities.GetCurrencySymbol())
                    {
                        name.ResultValue = estimatingItem.ResultValue;
                    }
                    else
                    {
                        name.ResultValue = 1;
                        name.CostEach = estimatingItem.CostEach;
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

        public void Rename(DrawObject groupObject)
        {
            for (int i = 0; i < this.Count; i++)
            {
                EstimatingItem item = this[i];
                if (item.ParentID == 0 && item.GroupID == groupObject.GroupID)
                {
                    item.ResultName = groupObject.Name;
                }
            }
        }

        public void SaveEstimatingItemCost(int groupID, string extensionID, string resultID, double costEach, UnitScale.UnitSystem currentSystemType)
        {
            EstimatingItemPrice estimatingItemPrice = this.QueryEstimatingItemPrice(groupID, extensionID, resultID);
            estimatingItemPrice.CostEach = costEach;
            if (costEach == 0)
            {
                estimatingItemPrice.SystemType = UnitScale.UnitSystem.undefined;
                return;
            }
            estimatingItemPrice.SystemType = (estimatingItemPrice.SystemType == UnitScale.UnitSystem.undefined ? currentSystemType : estimatingItemPrice.SystemType);
        }

        public void SaveEstimatingItemMarkup(int groupID, string extensionID, string resultID, double markupEach)
        {
            this.QueryEstimatingItemPrice(groupID, extensionID, resultID).MarkupEach = markupEach;
        }

        private void SynchEstimatingItemPrice(EstimatingItem groupItem)
        {
            EstimatingItemPrice estimatingItemPrice = this.QueryEstimatingItemPrice(groupItem);
            if (groupItem.InternalUnit != Utilities.GetCurrencySymbol())
            {
                groupItem.CostEach = estimatingItemPrice.CostEach;
                groupItem.MarkupEach = estimatingItemPrice.MarkupEach;
                return;
            }
            groupItem.ResultValue = 1;
            groupItem.ResultUnit = Resources.chaque;
            groupItem.CostEach = groupItem.CostEach;
            groupItem.MarkupEach = estimatingItemPrice.MarkupEach;
        }

        [CompilerGenerated]
        // <>c__DisplayClass2
        private sealed class u003cu003ec__DisplayClass2
        {
            public DrawObject layerObject;

            public u003cu003ec__DisplayClass2()
            {
            }

            // <QueryObjectsResults>b__0
            public bool u003cQueryObjectsResultsu003eb__0(Variable x)
            {
                return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClass6
        private sealed class u003cu003ec__DisplayClass6
        {
            public EstimatingItem newResult;

            public u003cu003ec__DisplayClass6()
            {
            }

            // <Refresh>b__4
            public bool u003cRefreshu003eb__4(EstimatingItem x)
            {
                if (x.GroupID != this.newResult.GroupID || !(x.ExtensionID == this.newResult.ExtensionID))
                {
                    return false;
                }
                return x.ResultName == this.newResult.ResultName;
            }
        }

        private class GroupSorter : IComparer<Variable>
        {
            public GroupSorter()
            {
            }

            public int Compare(Variable x, Variable y)
            {
                int num;
                try
                {
                    DrawObject tag = x.Tag as DrawObject;
                    DrawObject drawObject = y.Tag as DrawObject;
                    if (tag.ObjectType == drawObject.ObjectType)
                    {
                        num = StringLogicalComparer.Compare(tag.Name, drawObject.Name);
                    }
                    else
                    {
                        string str = tag.ObjectSortOrder.ToString();
                        int objectSortOrder = drawObject.ObjectSortOrder;
                        num = StringLogicalComparer.Compare(str, objectSortOrder.ToString());
                    }
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    num = -1;
                }
                return num;
            }
        }
    }
}