using Microsoft.Win32;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;

namespace QuoterPlan
{
    public class ReportModule
    {
        private bool enabled;

        private Project project;

        private DrawingArea drawArea;

        private WebBrowser webBrowser;

        private ExtensionsSupport extensionSupport;

        private Filter filter;

        private object registerHeaderOriginalValue = "";

        private object registerFooterOriginalValue = "";

        private object registerPrintBackgroundOriginalValue = "";

        private bool exportToCOffice;

        private CEstimatingItems estimatingItems;

        public CEstimatingItems CEstimatingItems
        {
            get
            {
                return this.estimatingItems;
            }
            set
            {
                this.estimatingItems = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
                if (!this.enabled)
                {
                    this.Clear();
                }
            }
        }

        public bool ExportEstimatingItems
        {
            get;
            set;
        }

        public bool ExportReportToMemory
        {
            get;
            set;
        }

        public bool ExportToCOffice
        {
            get
            {
                return this.exportToCOffice;
            }
            set
            {
                this.exportToCOffice = value;
            }
        }

        public string ReportSummaries
        {
            get;
            set;
        }

        public bool SortByLayers
        {
            get;
            set;
        }

        public ReportModule(Project project, DrawingArea drawArea, ExtensionsSupport extensionSupport)
        {
            this.project = project;
            this.drawArea = drawArea;
            this.extensionSupport = extensionSupport;
            this.filter = new Filter();
            this.enabled = true;
            this.InitializeWebBrowser();
            this.LoadResources();
        }

        private string BuildHTML(Report.ReportOrderEnum order, bool useAbsolutePath)
        {
            string str = this.BuildXML(order, false, false);
            return this.MakeHTML(str, this.BuildXSL(order), useAbsolutePath);
        }

        private bool BuildXLS(Report.ReportOrderEnum order, Report.ReportSortByEnum sortBy, bool exportEstimating, string fileName)
        {
            string str = this.BuildXML(order, false, true);
            return (new ReportModule.ExportToXLS(order, sortBy, exportEstimating)).Export(str, fileName);
        }

        private string BuildXML(Report.ReportOrderEnum order, bool includeHiddenValues = false, bool exportForExcel = false)
        {
            string str = string.Concat("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", Environment.NewLine);
            string str1 = str;
            string[] strArrays = new string[] { str1, "<Report Title=\"", Utilities.EscapeString(this.project.Name), "\" Date=\"", Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()), "\">", Environment.NewLine };
            str = string.Concat(strArrays);
            if (this.project.Report.ShowProjectInfo)
            {
                str = string.Concat(str, this.BuildXMLProjectInfo());
            }
            switch (order)
            {
                case Report.ReportOrderEnum.ReportOrderByObjects:
                    {
                        str = string.Concat(str, this.BuildXMLOrderByObjects(includeHiddenValues, exportForExcel, null));
                        break;
                    }
                case Report.ReportOrderEnum.ReportOrderByPlans:
                    {
                        if (this.project.Report.ReportSortBy != Report.ReportSortByEnum.ReportSortByPlans)
                        {
                            str = string.Concat(str, this.BuildXMLOrderByLayers(includeHiddenValues, exportForExcel));
                            break;
                        }
                        else
                        {
                            str = string.Concat(str, this.BuildXMLOrderByPlans(includeHiddenValues, exportForExcel));
                            break;
                        }
                    }
            }
            str = string.Concat(str, "</Report>");
            return str;
        }

        private string BuildXMLEstimatingItemResults(DrawObject groupObject, GroupStats groupStats, UnitScale reportUnitScale, string extensionID, string resultID, string resultName, string estimatingCaption, double quantity, PresetResult presetResult, ref double costTotalSubTotal, ref double priceTotalSubTotal, string defaultUnit = "", double defaultCostEach = 0)
        {
            bool deductionsCount = false;
            EstimatingItemPrice estimatingItemPrice = null;
            string str = defaultUnit;
            string str1 = "";
            quantity = reportUnitScale.Round(quantity);
            if (presetResult != null || !(resultName != ""))
            {
                if (presetResult == null)
                {
                    str1 = string.Concat("\" ItemID=\"", resultID);
                }
                else if (presetResult.ItemID != -1)
                {
                    str1 = string.Concat("\" ItemID=\"", presetResult.ItemID);
                }
                deductionsCount = true;
            }
            else
            {
                string objectType = groupObject.ObjectType;
                string str2 = objectType;
                if (objectType != null)
                {
                    if (str2 == "Line")
                    {
                        str = (reportUnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                        deductionsCount = true;
                    }
                    else if (str2 == "Counter")
                    {
                        str = Resources.unité_;
                        deductionsCount = true;
                    }
                    else if (str2 == "Area")
                    {
                        str = (reportUnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²");
                        string str3 = resultName;
                        string str4 = str3;
                        if (str3 != null)
                        {
                            if (str4 == "Area")
                            {
                                deductionsCount = groupStats.DeductionsCount == 0;
                            }
                            else if (str4 == "AreaMinusDeduction")
                            {
                                deductionsCount = groupStats.DeductionsCount > 0;
                            }
                        }
                    }
                    else if (str2 == "Perimeter")
                    {
                        str = (reportUnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                        string str5 = resultName;
                        string str6 = str5;
                        if (str5 != null)
                        {
                            if (str6 == "Length")
                            {
                                deductionsCount = (groupStats.DeductionPerimeter != 0 ? false : groupStats.DropLength == 0);
                            }
                            else if (str6 == "PerimeterMinusOpening")
                            {
                                deductionsCount = (groupStats.DeductionPerimeter <= 0 ? false : groupStats.DropLength == 0);
                            }
                            else if (str6 == "NetLength")
                            {
                                deductionsCount = groupStats.DropLength > 0;
                            }
                        }
                    }
                }
            }
            double num = 0;
            double costEach = defaultCostEach;
            double num1 = 0;
            double num2 = 0;
            double num3 = 0;
            if (deductionsCount)
            {
                estimatingItemPrice = this.project.EstimatingItems.QueryEstimatingItemPrice(groupObject.GroupID, extensionID, resultID);
                if (estimatingItemPrice != null)
                {
                    if (costEach == 0)
                    {
                        costEach = estimatingItemPrice.CostEach;
                    }
                    num = Math.Round(costEach * (1 + estimatingItemPrice.MarkupEach / 100), 2);
                    num1 = Math.Round(quantity * costEach, 2);
                    num2 = Math.Round(quantity * num, 2);
                    num3 = (num > 0 ? (num - costEach) / num : 0);
                    costTotalSubTotal += num1;
                    priceTotalSubTotal += num2;
                }
            }
            if (estimatingItemPrice == null)
            {
                return "";
            }
            object[] objArray = new object[] { "\" EstimatingItem=\"", true, (estimatingCaption != "" ? string.Concat("\" EstimatingCaption=\"", Utilities.EscapeString(estimatingCaption)) : ""), str1, "\" Quantity=\"", quantity, "\" EstimatingUnit=\"", str, "\" CostEach=\"", string.Format("{0:C}", costEach), "\" RawCostEach=\"", costEach, "\" MarkupEach=\"", string.Format("{0:C}", estimatingItemPrice.MarkupEach), "\" RawMarkupEach=\"", estimatingItemPrice.MarkupEach, "\" CostTotal=\"", string.Format("{0:C}", num1), "\" RawCostTotal=\"", num1, "\" PriceEach=\"", string.Format("{0:C}", num), "\" RawPriceEach=\"", num, "\" PriceTotal=\"", string.Format("{0:C}", num2), "\" RawPriceTotal=\"", num2, "\" Margin=\"", string.Format("{0:0.00%}", num3), "\" RawMargin=\"", num3 };
            return string.Concat(objArray);
        }

        private string BuildXMLLayersSummaries(List<List<ReportSummary>> reportAllSummaries)
        {
            string str = "";
            double costSubTotal = 0;
            double priceSubTotal = 0;
            double num = 0;
            if (reportAllSummaries.Count > 0)
            {
                List<ReportSummary> reportSummaries = new List<ReportSummary>();
                str = string.Concat(str, "\t<PlansSummaries>", Environment.NewLine);
                str = string.Concat(str, "\t\t<PlanSummaries Name=\"\">", Environment.NewLine);
                foreach (List<ReportSummary> reportAllSummary in reportAllSummaries)
                {
                    if (reportAllSummary.Count <= 0)
                    {
                        continue;
                    }
                    string str1 = str;
                    string[] strArrays = new string[] { str1, "\t\t\t<LayerSummaries Name=\"", Utilities.EscapeString(reportAllSummary[0].LayerName), "\">", Environment.NewLine };
                    str = string.Concat(strArrays);
                    str = string.Concat(str, this.BuildXMLObjectSummaries(reportAllSummary, "\t\t", true));
                    foreach (ReportSummary reportSummary in reportAllSummary)
                    {
                        if (reportSummary.Caption.IndexOf('*') != -1)
                        {
                            continue;
                        }
                        ReportSummary reportSummary1 = reportSummaries.Find((ReportSummary x) => x.LayerName == reportSummary.LayerName);
                        if (reportSummary1 != null)
                        {
                            ReportSummary costSubTotal1 = reportSummary1;
                            costSubTotal1.CostSubTotal = costSubTotal1.CostSubTotal + reportSummary.CostSubTotal;
                            ReportSummary priceSubTotal1 = reportSummary1;
                            priceSubTotal1.PriceSubTotal = priceSubTotal1.PriceSubTotal + reportSummary.PriceSubTotal;
                        }
                        else
                        {
                            reportSummaries.Add(new ReportSummary(reportSummary.LayerName, reportSummary.CostSubTotal, reportSummary.PriceSubTotal, null, null, -1, reportSummary.LayerName));
                        }
                        costSubTotal += reportSummary.CostSubTotal;
                        priceSubTotal += reportSummary.PriceSubTotal;
                    }
                    str = string.Concat(str, "\t\t\t</LayerSummaries>", Environment.NewLine);
                }
                num = (priceSubTotal > 0 ? (priceSubTotal - costSubTotal) / priceSubTotal : 0);
                object obj = str;
                object[] objArray = new object[] { obj, "\t\t\t<Total CostTotal=\"", string.Format("{0:C}", costSubTotal), "\" RawCostTotal=\"", costSubTotal, "\" PriceTotal=\"", string.Format("{0:C}", priceSubTotal), "\" RawPriceTotal=\"", priceSubTotal, "\" MarginTotal=\"", string.Format("{0:0.00%}", num), "\" RawMarginTotal=\"", num, "\"/>", Environment.NewLine };
                str = string.Concat(objArray);
                costSubTotal = 0;
                priceSubTotal = 0;
                str = string.Concat(str, "\t\t</PlanSummaries>", Environment.NewLine);
                str = string.Concat(str, "\t</PlansSummaries>", Environment.NewLine);
                reportSummaries.Sort(new ReportModule.SummarySorter());
                str = string.Concat(str, this.BuildXMLSummaries(reportSummaries, ""));
                reportSummaries.Clear();
                reportSummaries = null;
            }
            return str;
        }

        private string BuildXMLLayerSummaries(List<ReportSummary> reportTotalSummaries, List<ReportSummary> reportSummaries, ref double costTotalTotal, ref double priceTotalTotal)
        {
            string str = string.Concat("\t\t\t<LayerSummaries Name=\"", Utilities.EscapeString(reportSummaries[0].LayerName), "\">", Environment.NewLine);
            str = string.Concat(str, this.BuildXMLObjectSummaries(reportSummaries, "\t\t", true));
            foreach (ReportSummary reportSummary in reportSummaries)
            {
                if (reportSummary.Caption.IndexOf('*') != -1)
                {
                    continue;
                }
                ReportSummary reportSummary1 = reportTotalSummaries.Find((ReportSummary x) => x.GroupID == reportSummary.GroupID);
                if (reportSummary1 != null)
                {
                    ReportSummary costSubTotal = reportSummary1;
                    costSubTotal.CostSubTotal = costSubTotal.CostSubTotal + reportSummary.CostSubTotal;
                    ReportSummary priceSubTotal = reportSummary1;
                    priceSubTotal.PriceSubTotal = priceSubTotal.PriceSubTotal + reportSummary.PriceSubTotal;
                }
                else
                {
                    reportTotalSummaries.Add(new ReportSummary(reportSummary.Caption, reportSummary.CostSubTotal, reportSummary.PriceSubTotal, reportSummary.Plan, reportSummary.GroupObject, reportSummary.GroupID, reportSummary.LayerName));
                }
                costTotalTotal += reportSummary.CostSubTotal;
                priceTotalTotal += reportSummary.PriceSubTotal;
            }
            str = string.Concat(str, "\t\t\t</LayerSummaries>", Environment.NewLine);
            return str;
        }

        private string BuildXMLObject(DrawObject groupObject, Plan plan, string layerName, Variables plans, List<ReportSummary> reportSummaries, ref string indent, ref int resultID, ref int resultParentID, bool includeHiddenValues = false, bool exportForExcel = false, List<EstimatingItem> results = null)
        {
            int num;
            object obj;
            object[] objectType;
            object[] objArray;
            string[] str;
            object obj1;
            object obj2;
            double num1;
            object obj3;
            object obj4;
            double num2;
            double num3;
            double num4;
            double num5;
            double num6;
            double num7;
            object obj5;
            object obj6;
            double num8;
            object obj7;
            object obj8;
            object obj9;
            object obj10;
            object obj11;
            object obj12;
            object obj13;
            double num9;
            object obj14;
            object obj15;
            object obj16;
            object obj17;
            object obj18;
            object obj19;
            object obj20;
            object obj21;
            object obj22;
            object obj23;
            object obj24;
            object obj25;
            string str1;
            string str2 = "";
            bool flag = (plan != null ? false : layerName != "");
            double num10 = 0;
            double num11 = 0;
            if (plan == null)
            {
                indent = (this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByPlans ? "\t\t\t\t" : "\t");
            }
            else
            {
                indent = (true ? "\t\t\t\t" : "\t\t\t");
            }
            if (groupObject != null && (groupObject.Visible || this.project.Report.ShowInvisibleObjects))
            {
                string empty = string.Empty;
                if (plans != null)
                {
                    foreach (Variable collection in plans.Collection)
                    {
                        if (collection.Name != groupObject.GroupID.ToString())
                        {
                            continue;
                        }
                        empty = string.Concat(empty, (empty == "" ? "" : ", "), Utilities.EscapeString(collection.Value.ToString()));
                    }
                }
                List<string> strs = null;
                if (exportForExcel)
                {
                    strs = new List<string>()
                    {
                        string.Empty
                    };
                }
                else
                {
                    strs = (plan == null ? GroupUtilities.GetObjectLabels(this.project, groupObject, this.filter) : GroupUtilities.GetObjectLabels(plan, groupObject));
                }
                for (int i = 0; i < strs.Count; i++)
                {
                    if (!false)
                    {
                        if (results == null)
                        {
                            obj = str2;
                            objectType = new object[] { obj, indent, "\t<Object Name=\"", null, null, null, null, null, null, null, null, null, null };
                            object[] objArray1 = objectType;
                            string name = groupObject.Name;
                            if (i > 0)
                            {
                                str1 = string.Concat(" - (", Utilities.EscapeString(strs[i]), ")");
                            }
                            else
                            {
                                str1 = (strs.Count == 1 ? string.Empty : string.Empty);
                            }
                            objArray1[3] = Utilities.EscapeString(string.Concat(name, str1));
                            objectType[4] = (empty == "" ? "" : string.Concat("\" Plans=\"", empty));
                            objectType[5] = "\" Type=\"";
                            objectType[6] = groupObject.ObjectType;
                            objectType[7] = "\" Color=\"";
                            objectType[8] = groupObject.Color.ToArgb();
                            objectType[9] = "\" IsLabel=\"";
                            objectType[10] = i > 0;
                            objectType[11] = "\">";
                            objectType[12] = Environment.NewLine;
                            str2 = string.Concat(objectType);
                        }
                        if (this.project.Report.ShowComments && groupObject.Comment != string.Empty)
                        {
                            string comment = groupObject.Comment;
                            char[] chrArray = new char[] { '\r', '\n' };
                            string[] fields = Utilities.GetFields(comment, chrArray);
                            if (fields.GetUpperBound(0) >= 0)
                            {
                                str2 = string.Concat(str2, "\t\t\t<Comment>");
                                string[] strArrays = fields;
                                for (int j = 0; j < (int)strArrays.Length; j++)
                                {
                                    string str3 = strArrays[j];
                                    str2 = string.Concat(str2, Utilities.EscapeString(str3), "&#xD;");
                                }
                                str2 = string.Concat(str2, "</Comment>", Environment.NewLine);
                            }
                        }
                        if (results == null)
                        {
                            str2 = string.Concat(str2, indent, "\t\t<Extensions>", Environment.NewLine);
                        }
                        if (results == null)
                        {
                            obj = str2;
                            objectType = new object[] { obj, indent, "\t\t\t<Extension Name=\"", Resources.Résultats, "\" BaseExtension=\"", true, "\">", Environment.NewLine };
                            str2 = string.Concat(objectType);
                            str2 = string.Concat(str2, indent, "\t\t\t\t<Results>", Environment.NewLine);
                        }
                        num10 = 0;
                        num11 = 0;
                        UnitScale.UnitSystem systemType = this.project.Report.SystemType;
                        UnitScale.UnitPrecision precision = this.project.Report.Precision;
                        UnitScale unitScale = new UnitScale(1f, systemType, precision, false);
                        UnitScale.UnitSystem scaleSystemType = this.project.EstimatingItems.QueryEstimatingItemSystemType(groupObject.GroupID, "", groupObject.ObjectType, systemType, -1);
                        UnitScale unitScale1 = new UnitScale(1f, scaleSystemType, precision, false);
                        GroupStats groupStat = null;
                        if (plan != null)
                        {
                            groupStat = GroupUtilities.ComputeGroupStats(plan, groupObject, systemType, strs.Count == 1, (strs.Count > 1 ? strs[i] : string.Empty));
                        }
                        else if (layerName != "")
                        {
                            groupStat = GroupUtilities.ComputeGroupStats(this.project, layerName, groupObject, this.filter, systemType, strs.Count == 1, (strs.Count > 1 ? strs[i] : string.Empty));
                        }
                        GroupStats groupStat1 = GroupUtilities.ComputeGroupStats(this.project, groupObject, this.filter, systemType, strs.Count == 1, (strs.Count > 1 ? strs[i] : string.Empty));
                        bool flag1 = (groupStat1.DeductionsCount > 0 ? true : exportForExcel);
                        bool flag2 = (groupStat1.DropLength > 0 ? true : exportForExcel);
                        bool flag3 = (groupStat1.DeductionPerimeter > 0 ? true : exportForExcel);
                        resultID++;
                        resultParentID = resultID;
                        string objectType1 = groupObject.ObjectType;
                        string str4 = objectType1;
                        if (objectType1 != null)
                        {
                            if (str4 == "Line")
                            {
                                if (systemType == scaleSystemType)
                                {
                                    num7 = (groupStat != null ? groupStat.Perimeter : groupStat1.Perimeter);
                                }
                                else
                                {
                                    num7 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet((groupStat != null ? groupStat.Perimeter : groupStat1.Perimeter)) : UnitScale.FromFeetToMeters((groupStat != null ? groupStat.Perimeter : groupStat1.Perimeter)));
                                }
                                double num12 = num7;
                                if (results != null)
                                {
                                    results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, num12, (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m"), 0, 0, -1));
                                }
                                else if (this.ExportEstimatingItems)
                                {
                                    obj = str2;
                                    objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Length\" Caption=\"", Resources.Longueur, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                    objectType[5] = (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                    object[] objArray2 = objectType;
                                    if (groupStat != null || flag)
                                    {
                                        objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale1.ToLengthStringFromUnitSystem(groupStat.Perimeter, false, true, true)), "\" RawValue=\"", groupStat.Perimeter };
                                        obj5 = string.Concat(objArray);
                                    }
                                    else
                                    {
                                        obj5 = "";
                                    }
                                    objArray2[6] = obj5;
                                    objectType[7] = "\" TotalValue=\"";
                                    objectType[8] = Utilities.EscapeString(unitScale1.ToLengthStringFromUnitSystem(groupStat1.Perimeter, false, true, true));
                                    objectType[9] = "\" TotalRawValue=\"";
                                    objectType[10] = groupStat1.Perimeter;
                                    objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale1, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStat != null ? num12 : num12), null, ref num10, ref num11, "", 0);
                                    objectType[12] = "\"/>";
                                    objectType[13] = Environment.NewLine;
                                    str2 = string.Concat(objectType);
                                }
                                else
                                {
                                    obj = str2;
                                    objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Length\" Caption=\"", Resources.Longueur, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                    objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                    object[] objArray3 = objectType;
                                    if (groupStat != null || flag)
                                    {
                                        objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.Perimeter, false, true, true)), "\" RawValue=\"", groupStat.Perimeter };
                                        obj6 = string.Concat(objArray);
                                    }
                                    else
                                    {
                                        obj6 = "";
                                    }
                                    objArray3[6] = obj6;
                                    objectType[7] = "\" TotalValue=\"";
                                    objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.Perimeter, false, true, true));
                                    objectType[9] = "\" TotalRawValue=\"";
                                    objectType[10] = groupStat1.Perimeter;
                                    objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale1, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStat != null ? num12 : num12), null, ref num10, ref num11, "", 0);
                                    objectType[12] = "\"/>";
                                    objectType[13] = Environment.NewLine;
                                    str2 = string.Concat(objectType);
                                }
                            }
                            else if (str4 == "Area")
                            {
                                if (systemType == scaleSystemType)
                                {
                                    num8 = (groupStat != null ? groupStat.AreaMinusDeduction : groupStat1.AreaMinusDeduction);
                                }
                                else
                                {
                                    num8 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet((groupStat != null ? groupStat.AreaMinusDeduction : groupStat1.AreaMinusDeduction)) : UnitScale.FromSquareFeetToSquareMeters((groupStat != null ? groupStat.AreaMinusDeduction : groupStat1.AreaMinusDeduction)));
                                }
                                double num13 = num8;
                                if (results != null)
                                {
                                    results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, num13, (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²"), 0, 0, -1));
                                }
                                else
                                {
                                    if (this.ExportEstimatingItems)
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Area\" Caption=\"", Resources.Surface, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                        objectType[5] = (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²");
                                        object[] objArray4 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale1.ToAreaStringFromUnitSystem(groupStat.Area, true)), "\" RawValue=\"", groupStat.Area };
                                            obj7 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj7 = "";
                                        }
                                        objArray4[6] = obj7;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale1.ToAreaStringFromUnitSystem(groupStat1.Area, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.Area;
                                        objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale1, "", groupObject.ObjectType, "Area", Resources.Surface, (groupStat != null ? num13 : num13), null, ref num10, ref num11, "", 0);
                                        objectType[12] = "\"/>";
                                        objectType[13] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    else
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Area\" Caption=\"", Resources.Surface, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²");
                                        object[] objArray5 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStat.Area, true)), "\" RawValue=\"", groupStat.Area };
                                            obj13 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj13 = "";
                                        }
                                        objArray5[6] = obj13;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStat1.Area, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.Area;
                                        objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale1, "", groupObject.ObjectType, "Area", Resources.Surface, (groupStat != null ? num13 : num13), null, ref num10, ref num11, "", 0);
                                        objectType[12] = "\"/>";
                                        objectType[13] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    if (flag1)
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Substractions\" Caption=\"", Resources.Déduction, "\" Unit=\"", null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²");
                                        object[] objArray6 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStat.DeductionArea, true)), "\" RawValue=\"", groupStat.DeductionArea };
                                            obj11 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj11 = "";
                                        }
                                        objArray6[6] = obj11;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStat1.DeductionArea, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.DeductionArea;
                                        objectType[11] = "\"/>";
                                        objectType[12] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"AreaMinusSubstractions\" Caption=\"", Resources.Surface_déductions, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²");
                                        object[] objArray7 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStat.AreaMinusDeduction, true)), "\" RawValue=\"", groupStat.AreaMinusDeduction };
                                            obj12 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj12 = "";
                                        }
                                        objArray7[6] = obj12;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStat1.AreaMinusDeduction, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.AreaMinusDeduction;
                                        objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, "", groupObject.ObjectType, "AreaMinusDeduction", Resources.Surface, (groupStat != null ? groupStat.AreaMinusDeduction : groupStat1.AreaMinusDeduction), null, ref num10, ref num11, "", 0);
                                        objectType[12] = "\"/>";
                                        objectType[13] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    obj = str2;
                                    objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Perimeter\" Caption=\"", Resources.Périmètre, "\" Unit=\"", null, null, null, null, null, null, null, null };
                                    objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                    object[] objArray8 = objectType;
                                    if (groupStat != null)
                                    {
                                        objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.Perimeter, false, true, true)), "\" RawValue=\"", groupStat.Perimeter };
                                        obj8 = string.Concat(objArray);
                                    }
                                    else
                                    {
                                        obj8 = "";
                                    }
                                    objArray8[6] = obj8;
                                    objectType[7] = "\" TotalValue=\"";
                                    objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.Perimeter, false, true, true));
                                    objectType[9] = "\" TotalRawValue=\"";
                                    objectType[10] = groupStat1.Perimeter;
                                    objectType[11] = "\"/>";
                                    objectType[12] = Environment.NewLine;
                                    str2 = string.Concat(objectType);
                                    if (flag1)
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"PerimeterPlusSubstractions\" Caption=\"", Resources.Périmètre_déductions, "\" Unit=\"", null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        object[] objArray9 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.PerimeterPlusDeduction, false, true, true)), "\" RawValue=\"", groupStat.PerimeterPlusDeduction };
                                            obj9 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj9 = "";
                                        }
                                        objArray9[6] = obj9;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.PerimeterPlusDeduction, false, true, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.PerimeterPlusDeduction;
                                        objectType[11] = "\"/>";
                                        objectType[12] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"SubstractionsCount\" Caption=\"", Resources.Nombre_de_déductions, "\" Unit=\"", Resources.unité_, null, null, null, null, null, null, null };
                                        object[] objArray10 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(this.drawArea.ToUnitString(groupStat.DeductionsCount)), "\" RawValue=\"", groupStat.DeductionsCount };
                                            obj10 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj10 = "";
                                        }
                                        objArray10[6] = obj10;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(this.drawArea.ToUnitString(groupStat1.DeductionsCount));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.DeductionsCount;
                                        objectType[11] = "\"/>";
                                        objectType[12] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                }
                            }
                            else if (str4 == "Perimeter")
                            {
                                if (systemType == scaleSystemType)
                                {
                                    num9 = (groupStat != null ? groupStat.Perimeter : groupStat1.Perimeter);
                                }
                                else
                                {
                                    num9 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet((groupStat != null ? groupStat.Perimeter : groupStat1.Perimeter)) : UnitScale.FromFeetToMeters((groupStat != null ? groupStat.Perimeter : groupStat1.Perimeter)));
                                }
                                double num14 = num9;
                                if (results != null)
                                {
                                    results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, num14, (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m"), 0, 0, -1));
                                }
                                else
                                {
                                    if (this.ExportEstimatingItems)
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Length\" Caption=\"", Resources.Longueur, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                        objectType[5] = (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        object[] objArray11 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale1.ToLengthStringFromUnitSystem(groupStat.Perimeter, false, true, true)), "\" RawValue=\"", groupStat.Perimeter };
                                            obj14 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj14 = "";
                                        }
                                        objArray11[6] = obj14;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale1.ToLengthStringFromUnitSystem(groupStat1.Perimeter, false, true, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.Perimeter;
                                        objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale1, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStat != null ? num14 : num14), null, ref num10, ref num11, "", 0);
                                        objectType[12] = "\"/>";
                                        objectType[13] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    else
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Length\" Caption=\"", Resources.Longueur, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        object[] objArray12 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.Perimeter, false, true, true)), "\" RawValue=\"", groupStat.Perimeter };
                                            obj24 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj24 = "";
                                        }
                                        objArray12[6] = obj24;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.Perimeter, false, true, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.Perimeter;
                                        objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale1, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStat != null ? num14 : num14), null, ref num10, ref num11, "", 0);
                                        objectType[12] = "\"/>";
                                        objectType[13] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    if (flag3)
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Openings\" Caption=\"", Resources.Longueur_des_ouvertures, "\" Unit=\"", null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        object[] objArray13 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.DeductionPerimeter, false, true, true)), "\" RawValue=\"", groupStat.DeductionPerimeter };
                                            obj22 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj22 = "";
                                        }
                                        objArray13[6] = obj22;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.DeductionPerimeter, false, true, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.DeductionPerimeter;
                                        objectType[11] = "\"/>";
                                        objectType[12] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"PerimeterMinusOpenings\" Caption=\"", Resources.Longueur_sans_ouvertures, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        object[] objArray14 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.PerimeterMinusOpening, false, true, true)), "\" RawValue=\"", groupStat.PerimeterMinusOpening };
                                            obj23 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj23 = "";
                                        }
                                        objArray14[6] = obj23;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.PerimeterMinusOpening, false, true, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.PerimeterMinusOpening;
                                        objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, "", groupObject.ObjectType, "PerimeterMinusOpening", Resources.Longueur, (groupStat != null ? groupStat.PerimeterMinusOpening : groupStat1.PerimeterMinusOpening), null, ref num10, ref num11, "", 0);
                                        objectType[12] = "\"/>";
                                        objectType[13] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    if (flag2)
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"DropLength\" Caption=\"", Resources.Drop_length, "\" Unit=\"", null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        object[] objArray15 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.DropLength, false, true, true)), "\" RawValue=\"", groupStat.DropLength };
                                            obj19 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj19 = "";
                                        }
                                        objArray15[6] = obj19;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.DropLength, false, true, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.DropLength;
                                        objectType[11] = "\"/>";
                                        objectType[12] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"NetLength\" Caption=\"", Resources.Longueur_nette, "\" Unit=\"", null, null, null, null, null, null, null, null, null };
                                        objectType[5] = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        object[] objArray16 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat.NetLength, false, true, true)), "\" RawValue=\"", groupStat.NetLength };
                                            obj20 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj20 = "";
                                        }
                                        objArray16[6] = obj20;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStat1.NetLength, false, true, true));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.NetLength;
                                        objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, "", groupObject.ObjectType, "NetLength", Resources.Longueur, (groupStat != null ? groupStat.NetLength : groupStat1.NetLength), null, ref num10, ref num11, "", 0);
                                        objectType[12] = "\"/>";
                                        objectType[13] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"DropsCount\" Caption=\"", Resources.Drops_count, "\" Unit=\"", Resources.unité_, null, null, null, null, null, null, null };
                                        object[] objArray17 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(this.drawArea.ToUnitString(groupStat.DropsCount)), "\" RawValue=\"", groupStat.DropsCount };
                                            obj21 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj21 = "";
                                        }
                                        objArray17[6] = obj21;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(this.drawArea.ToUnitString(groupStat1.DropsCount));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.DropsCount;
                                        objectType[11] = "\"/>";
                                        objectType[12] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    if (flag1)
                                    {
                                        obj = str2;
                                        objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"OpeningsCount\" Caption=\"", Resources.Nombre_d_ouvertures, "\" Unit=\"", Resources.unité_, null, null, null, null, null, null, null };
                                        object[] objArray18 = objectType;
                                        if (groupStat != null)
                                        {
                                            objArray = new object[] { "\" Value=\"", Utilities.EscapeString(this.drawArea.ToUnitString(groupStat.DeductionsCount)), "\" RawValue=\"", groupStat.DeductionsCount };
                                            obj18 = string.Concat(objArray);
                                        }
                                        else
                                        {
                                            obj18 = "";
                                        }
                                        objArray18[6] = obj18;
                                        objectType[7] = "\" TotalValue=\"";
                                        objectType[8] = Utilities.EscapeString(this.drawArea.ToUnitString(groupStat1.DeductionsCount));
                                        objectType[9] = "\" TotalRawValue=\"";
                                        objectType[10] = groupStat1.DeductionsCount;
                                        objectType[11] = "\"/>";
                                        objectType[12] = Environment.NewLine;
                                        str2 = string.Concat(objectType);
                                    }
                                    obj = str2;
                                    objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"CornersCount\" Caption=\"", Resources.Nombre_de_coins, "\" Unit=\"", Resources.unité_, null, null, null, null, null, null, null };
                                    object[] objArray19 = objectType;
                                    if (groupStat != null)
                                    {
                                        objArray = new object[] { "\" Value=\"", Utilities.EscapeString(this.drawArea.ToUnitString(groupStat.CornersCount)), "\" RawValue=\"", groupStat.CornersCount };
                                        obj15 = string.Concat(objArray);
                                    }
                                    else
                                    {
                                        obj15 = "";
                                    }
                                    objArray19[6] = obj15;
                                    objectType[7] = "\" TotalValue=\"";
                                    objectType[8] = Utilities.EscapeString(this.drawArea.ToUnitString(groupStat1.CornersCount));
                                    objectType[9] = "\" TotalRawValue=\"";
                                    objectType[10] = groupStat1.CornersCount;
                                    objectType[11] = "\"/>";
                                    objectType[12] = Environment.NewLine;
                                    str2 = string.Concat(objectType);
                                    obj = str2;
                                    objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"EndsCount\" Caption=\"", Resources.Nombre_de_bouts, "\" Unit=\"", Resources.unité_, null, null, null, null, null, null, null };
                                    object[] objArray20 = objectType;
                                    if (groupStat != null)
                                    {
                                        objArray = new object[] { "\" Value=\"", Utilities.EscapeString(this.drawArea.ToUnitString(groupStat.EndsCount)), "\" RawValue=\"", groupStat.EndsCount };
                                        obj16 = string.Concat(objArray);
                                    }
                                    else
                                    {
                                        obj16 = "";
                                    }
                                    objArray20[6] = obj16;
                                    objectType[7] = "\" TotalValue=\"";
                                    objectType[8] = Utilities.EscapeString(this.drawArea.ToUnitString(groupStat1.EndsCount));
                                    objectType[9] = "\" TotalRawValue=\"";
                                    objectType[10] = groupStat1.EndsCount;
                                    objectType[11] = "\"/>";
                                    objectType[12] = Environment.NewLine;
                                    str2 = string.Concat(objectType);
                                    obj = str2;
                                    objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"SegmentsCount\" Caption=\"", Resources.Nombre_de_segments, "\" Unit=\"", Resources.unité_, null, null, null, null, null, null, null };
                                    object[] objArray21 = objectType;
                                    if (groupStat != null)
                                    {
                                        objArray = new object[] { "\" Value=\"", Utilities.EscapeString(this.drawArea.ToUnitString(groupStat.SegmentsCount)), "\" RawValue=\"", groupStat.SegmentsCount };
                                        obj17 = string.Concat(objArray);
                                    }
                                    else
                                    {
                                        obj17 = "";
                                    }
                                    objArray21[6] = obj17;
                                    objectType[7] = "\" TotalValue=\"";
                                    objectType[8] = Utilities.EscapeString(this.drawArea.ToUnitString(groupStat1.SegmentsCount));
                                    objectType[9] = "\" TotalRawValue=\"";
                                    objectType[10] = groupStat1.SegmentsCount;
                                    objectType[11] = "\"/>";
                                    objectType[12] = Environment.NewLine;
                                    str2 = string.Concat(objectType);
                                }
                            }
                            else if (str4 == "Counter")
                            {
                                if (results != null)
                                {
                                    results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, (double)groupStat1.GroupCount, Resources.unité_, 0, 0, -1));
                                }
                                else
                                {
                                    obj = str2;
                                    objectType = new object[] { obj, indent, "\t\t\t\t\t<Result Name=\"Count\" Caption=\"", Resources.Nombre_d_objets, "\" Unit=\"", Resources.unité_, null, null, null, null, null, null, null, null };
                                    object[] objArray22 = objectType;
                                    if (groupStat != null)
                                    {
                                        objArray = new object[] { "\" Value=\"", Utilities.EscapeString(this.drawArea.ToUnitString(groupStat.GroupCount)), "\" RawValue=\"", groupStat.GroupCount };
                                        obj25 = string.Concat(objArray);
                                    }
                                    else
                                    {
                                        obj25 = "";
                                    }
                                    objArray22[6] = obj25;
                                    objectType[7] = "\" TotalValue=\"";
                                    objectType[8] = Utilities.EscapeString(this.drawArea.ToUnitString(groupStat1.GroupCount));
                                    objectType[9] = "\" TotalRawValue=\"";
                                    objectType[10] = groupStat1.GroupCount;
                                    objectType[11] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, "", groupObject.ObjectType, "Count", Resources.Nombre_d_objets, (double)((groupStat != null ? groupStat.GroupCount : groupStat1.GroupCount)), null, ref num10, ref num11, "", 0);
                                    objectType[12] = "\"/>";
                                    objectType[13] = Environment.NewLine;
                                    str2 = string.Concat(objectType);
                                }
                            }
                        }
                        if (results == null)
                        {
                            str2 = string.Concat(str2, indent, "\t\t\t\t</Results>", Environment.NewLine);
                            str2 = string.Concat(str2, indent, "\t\t\t</Extension>", Environment.NewLine);
                        }
                        DrawObjectGroup group = groupObject.Group;
                        Presets preset = new Presets();
                        if (group != null)
                        {
                            foreach (Preset collection1 in group.Presets.Collection)
                            {
                                preset.Add(collection1.Clone(true));
                            }
                            foreach (Preset preset1 in group.Presets.Collection)
                            {
                                if (results == null)
                                {
                                    str4 = str2;
                                    str = new string[] { str4, indent, "\t\t\t<Extension Name=\"", Utilities.EscapeString(preset1.DisplayName), "\">", Environment.NewLine };
                                    str2 = string.Concat(str);
                                }
                                if (i == 0)
                                {
                                    string str5 = "";
                                    int num15 = 0;
                                    foreach (PresetChoice presetChoice in preset1.Choices.Collection)
                                    {
                                        ExtensionChoice extensionChoice = this.extensionSupport.FindChoice(preset1.CategoryName, preset1.ExtensionName, presetChoice.ChoiceName);
                                        if (extensionChoice != null && (!extensionChoice.Hidden || includeHiddenValues))
                                        {
                                            ExtensionChoiceElement extensionChoiceElement = extensionChoice.FindElement(presetChoice.ChoiceElementName);
                                            if (extensionChoiceElement != null && results == null)
                                            {
                                                str4 = str5;
                                                str = new string[] { str4, indent, "\t\t\t\t\t<Field Name=\"Choice", null, null, null, null, null, null, null, null, null };
                                                num = num15 + 1;
                                                str[3] = num.ToString();
                                                str[4] = "\" Caption=\"";
                                                str[5] = presetChoice.ChoiceCaption;
                                                str[6] = "\" Value=\"";
                                                str[7] = Utilities.EscapeString(extensionChoiceElement.Caption);
                                                str[8] = "\" RawValue=\"";
                                                str[9] = Utilities.EscapeString(extensionChoiceElement.Caption);
                                                str[10] = "\"/>";
                                                str[11] = Environment.NewLine;
                                                str5 = string.Concat(str);
                                            }
                                        }
                                        num15++;
                                    }
                                    num15 = 0;
                                    foreach (PresetField presetField in preset1.Fields.Collection)
                                    {
                                        double num16 = Utilities.ConvertToDouble(presetField.Value.ToString(), -1);
                                        string lengthStringFromUnitSystem = num16.ToString();
                                        string str6 = num16.ToString();
                                        string currencySymbol = string.Empty;
                                        ExtensionField.ExtensionFieldTypeEnum fieldType = presetField.FieldType;
                                        if (fieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension)
                                        {
                                            if (preset1.ScaleSystemType != unitScale.ScaleSystemType)
                                            {
                                                num16 = (unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num16) : UnitScale.FromFeetToMeters(num16));
                                            }
                                            lengthStringFromUnitSystem = unitScale.ToLengthStringFromUnitSystem(num16, false, false, true);
                                            str6 = unitScale.ToLengthFromUnitSystem(num16).ToString();
                                            currencySymbol = (systemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                        }
                                        else if (fieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency)
                                        {
                                            lengthStringFromUnitSystem = this.drawArea.ToCurrency(num16);
                                            str6 = num16.ToString();
                                            currencySymbol = Utilities.GetCurrencySymbol();
                                        }
                                        if (results == null)
                                        {
                                            str4 = str5;
                                            str = new string[14];
                                            str[0] = str4;
                                            str[1] = indent;
                                            str[2] = "\t\t\t\t\t<Field Name=\"Field";
                                            num = num15 + 1;
                                            str[3] = num.ToString();
                                            str[4] = "\" Caption=\"";
                                            str[5] = Utilities.EscapeString(presetField.Caption);
                                            str[6] = "\" Unit=\"";
                                            str[7] = currencySymbol;
                                            str[8] = "\" Value=\"";
                                            str[9] = Utilities.EscapeString(lengthStringFromUnitSystem);
                                            str[10] = "\" RawValue=\"";
                                            str[11] = Utilities.EscapeString(str6);
                                            str[12] = "\"/>";
                                            str[13] = Environment.NewLine;
                                            str5 = string.Concat(str);
                                        }
                                        num15++;
                                    }
                                    if (results == null && str5.Trim() != "")
                                    {
                                        str2 = string.Concat(str2, indent, "\t\t\t\t<Fields>", Environment.NewLine);
                                        str2 = string.Concat(str2, str5);
                                        str2 = string.Concat(str2, indent, "\t\t\t\t</Fields>", Environment.NewLine);
                                    }
                                }
                                if (results == null)
                                {
                                    str2 = string.Concat(str2, indent, "\t\t\t\t<Results>", Environment.NewLine);
                                }
                                Variables variable = new Variables();
                                for (int k = (plan != null || layerName != "" ? 2 : 1); k > 0; k--)
                                {
                                    Preset preset2 = (k == 2 ? preset.FindById(preset1.ID) : preset1);
                                    preset2 = (preset2 == null ? preset1 : preset2);
                                    this.extensionSupport.QueryPresetResults(preset2, (k == 2 ? groupStat : groupStat1), unitScale);
                                    foreach (PresetResult presetResult in preset2.Results.Collection)
                                    {
                                        if (!presetResult.ConditionMet)
                                        {
                                            continue;
                                        }
                                        scaleSystemType = this.project.EstimatingItems.QueryEstimatingItemSystemType(groupObject.GroupID, preset1.ID, presetResult.Name, systemType, -1);
                                        unitScale1 = new UnitScale(1f, scaleSystemType, precision, false);
                                        double result = presetResult.Result;
                                        double num17 = Utilities.ConvertToDouble(result.ToString(), -1);
                                        string areaStringFromUnitSystem = string.Empty;
                                        string empty1 = string.Empty;
                                        string lower = string.Empty;
                                        double num18 = 0;
                                        DBEstimatingItem estimatingItem = null;
                                        if (presetResult.ItemID != -1)
                                        {
                                            estimatingItem = this.project.DBManagement.GetEstimatingItem(presetResult.ItemID);
                                        }
                                        bool flag4 = false;
                                        if (estimatingItem != null)
                                        {
                                            flag4 = estimatingItem.MatchResultType(presetResult.ResultType);
                                        }
                                        if (!flag4)
                                        {
                                            switch (presetResult.ResultType)
                                            {
                                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                                                    {
                                                        if (systemType == scaleSystemType)
                                                        {
                                                            num1 = num17;
                                                        }
                                                        else
                                                        {
                                                            num1 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num17) : UnitScale.FromFeetToMeters(num17));
                                                        }
                                                        num17 = num1;
                                                        areaStringFromUnitSystem = unitScale1.ToLengthStringFromUnitSystem(num17, false, true, true);
                                                        empty1 = unitScale1.ToLengthFromUnitSystem(num17).ToString();
                                                        lower = (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi : "m");
                                                        break;
                                                    }
                                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                                                    {
                                                        if (systemType == scaleSystemType)
                                                        {
                                                            num2 = num17;
                                                        }
                                                        else
                                                        {
                                                            num2 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet(num17) : UnitScale.FromSquareFeetToSquareMeters(num17));
                                                        }
                                                        num17 = num2;
                                                        areaStringFromUnitSystem = unitScale1.ToAreaStringFromUnitSystem(num17, true);
                                                        empty1 = unitScale1.ToAreaFromUnitSystem(num17).ToString();
                                                        lower = (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi_2 : "m²");
                                                        break;
                                                    }
                                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                                                    {
                                                        if (systemType == scaleSystemType)
                                                        {
                                                            num3 = num17;
                                                        }
                                                        else
                                                        {
                                                            num3 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromCubicMetersToCubicFeet(num17) : UnitScale.FromCubicFeetToCubicMeters(num17));
                                                        }
                                                        num17 = num3;
                                                        areaStringFromUnitSystem = unitScale1.ToCubicStringFromUnitSystem(num17, true);
                                                        empty1 = unitScale1.ToCubicFromUnitSystem(num17).ToString();
                                                        lower = (scaleSystemType == UnitScale.UnitSystem.imperial ? Resources.pi_3 : "m³");
                                                        break;
                                                    }
                                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
                                                    {
                                                        areaStringFromUnitSystem = this.drawArea.ToCurrency(num17);
                                                        num18 = Utilities.ConvertToDouble(num17, -1);
                                                        empty1 = "1";
                                                        lower = Resources.chaque;
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        areaStringFromUnitSystem = unitScale1.Round(num17).ToString();
                                                        empty1 = areaStringFromUnitSystem.ToString();
                                                        lower = presetResult.Unit.ToLower();
                                                        areaStringFromUnitSystem = string.Concat(areaStringFromUnitSystem, (lower != string.Empty ? string.Concat(" ", lower) : ""));
                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            UnitScale unitScale2 = new UnitScale(1f, (estimatingItem.PurchaseUnit == "m" || estimatingItem.PurchaseUnit == "m²" || estimatingItem.PurchaseUnit == "m³" ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial), precision, false);
                                            scaleSystemType = unitScale2.ScaleSystemType;
                                            switch (presetResult.ResultType)
                                            {
                                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                                                    {
                                                        if (systemType == scaleSystemType)
                                                        {
                                                            num4 = num17;
                                                        }
                                                        else
                                                        {
                                                            num4 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(num17) : UnitScale.FromFeetToMeters(num17));
                                                        }
                                                        num17 = num4;
                                                        areaStringFromUnitSystem = unitScale2.ToLengthStringFromUnitSystem(num17, false, true, true);
                                                        empty1 = unitScale2.ToLengthFromUnitSystem(num17).ToString();
                                                        lower = estimatingItem.PurchaseUnit;
                                                        break;
                                                    }
                                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                                                    {
                                                        if (systemType == scaleSystemType)
                                                        {
                                                            num5 = num17;
                                                        }
                                                        else
                                                        {
                                                            num5 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromSquareMetersToSquareFeet(num17) : UnitScale.FromSquareFeetToSquareMeters(num17));
                                                        }
                                                        num17 = num5;
                                                        areaStringFromUnitSystem = unitScale2.ToAreaStringFromUnitSystem(num17, true);
                                                        empty1 = unitScale2.ToAreaFromUnitSystem(num17).ToString();
                                                        lower = estimatingItem.PurchaseUnit;
                                                        break;
                                                    }
                                                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                                                    {
                                                        if (systemType == scaleSystemType)
                                                        {
                                                            num6 = num17;
                                                        }
                                                        else
                                                        {
                                                            num6 = (scaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromCubicMetersToCubicFeet(num17) : UnitScale.FromCubicFeetToCubicMeters(num17));
                                                        }
                                                        num17 = num6;
                                                        areaStringFromUnitSystem = unitScale2.ToCubicStringFromUnitSystem(num17, true);
                                                        empty1 = unitScale2.ToCubicFromUnitSystem(num17).ToString();
                                                        lower = estimatingItem.PurchaseUnit;
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        areaStringFromUnitSystem = unitScale2.Round(num17).ToString();
                                                        empty1 = areaStringFromUnitSystem.ToString();
                                                        lower = estimatingItem.PurchaseUnit;
                                                        areaStringFromUnitSystem = string.Concat(areaStringFromUnitSystem, (lower != string.Empty ? string.Concat(" ", lower) : ""));
                                                        break;
                                                    }
                                            }
                                        }
                                        if (!presetResult.IsEstimatingItem && this.ExportEstimatingItems)
                                        {
                                            continue;
                                        }
                                        string str7 = (estimatingItem != null ? estimatingItem.Description : presetResult.Caption);
                                        int num19 = (estimatingItem != null ? estimatingItem.SectionID : presetResult.SectionID);
                                        int num20 = (estimatingItem != null ? estimatingItem.SubSectionID : presetResult.SubSectionID);
                                        string str8 = (estimatingItem != null ? estimatingItem.BidCode : "");
                                        DBEstimatingItem.EstimatingItemType estimatingItemType = (estimatingItem != null ? estimatingItem.ItemType : DBEstimatingItem.EstimatingItemType.MaterialItem);
                                        if (results != null)
                                        {
                                            resultID++;
                                            if (!presetResult.IsEstimatingItem)
                                            {
                                                continue;
                                            }
                                            results.Add(new EstimatingItem(resultID, resultParentID, groupObject.GroupID, preset1.ID, presetResult.Name, groupObject.ObjectType, str7, Utilities.ConvertToDouble(empty1, -1), (lower != string.Empty ? lower : presetResult.Unit), 0, 0, -1));
                                        }
                                        else
                                        {
                                            Variable variable1 = variable.Find(presetResult.Name);
                                            if (variable1 == null)
                                            {
                                                string name1 = presetResult.Name;
                                                str = new string[] { indent, "\t\t\t\t\t<Result Name=\"", presetResult.Name, "\" Caption=\"", Utilities.EscapeString(str7), "\" Unit=\"", lower, "\"" };
                                                string str9 = string.Concat(str);
                                                if (k == 2)
                                                {
                                                    str = new string[] { " Value=\"", Utilities.EscapeString(areaStringFromUnitSystem), "\" RawValue=\"", empty1, null, null };
                                                    str[4] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, preset1.ID, presetResult.Name, presetResult.Name, str7, Utilities.ConvertToDouble(empty1, -1), presetResult, ref num10, ref num11, lower, num18);
                                                    str[5] = "\"";
                                                    obj3 = string.Concat(str);
                                                }
                                                else
                                                {
                                                    objectType = new object[] { " TotalValue=\"", Utilities.EscapeString(areaStringFromUnitSystem), "\" TotalRawValue=\"", empty1, null, null, null, null, null, null, null, null, null, null };
                                                    objectType[4] = (groupStat == null ? this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, preset1.ID, presetResult.Name, presetResult.Name, str7, Utilities.ConvertToDouble(empty1, -1), presetResult, ref num10, ref num11, lower, num18) : "");
                                                    objectType[5] = "\" SectionID=\"";
                                                    objectType[6] = num19;
                                                    objectType[7] = "\" SubSectionID=\"";
                                                    objectType[8] = num20;
                                                    objectType[9] = "\" BidCode=\"";
                                                    objectType[10] = Utilities.EscapeString(str8);
                                                    objectType[11] = "\" ItemType=\"";
                                                    objectType[12] = (int)estimatingItemType;
                                                    objectType[13] = "\"";
                                                    obj3 = string.Concat(objectType);
                                                }
                                                variable1 = new Variable(name1, str9, obj3);
                                                variable.Add(variable1);
                                            }
                                            else
                                            {
                                                Variable variable2 = variable1;
                                                object tag = variable2.Tag;
                                                if (k == 2)
                                                {
                                                    str = new string[] { " Value=\"", Utilities.EscapeString(areaStringFromUnitSystem), "\" RawValue=\"", empty1, null, null };
                                                    str[4] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, preset1.ID, presetResult.Name, presetResult.Name, str7, Utilities.ConvertToDouble(empty1, -1), presetResult, ref num10, ref num11, lower, num18);
                                                    str[5] = "\"";
                                                    obj4 = string.Concat(str);
                                                }
                                                else
                                                {
                                                    objectType = new object[] { " TotalValue=\"", Utilities.EscapeString(areaStringFromUnitSystem), "\" TotalRawValue=\"", empty1, null, null, null, null, null, null, null, null, null, null };
                                                    objectType[4] = (groupStat == null ? this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, preset1.ID, presetResult.Name, presetResult.Name, str7, Utilities.ConvertToDouble(empty1, -1), presetResult, ref num10, ref num11, lower, num18) : "");
                                                    objectType[5] = "\" SectionID=\"";
                                                    objectType[6] = num19;
                                                    objectType[7] = "\" SubSectionID=\"";
                                                    objectType[8] = num20;
                                                    objectType[9] = "\" BidCode=\"";
                                                    objectType[10] = Utilities.EscapeString(str8);
                                                    objectType[11] = "\" ItemType=\"";
                                                    objectType[12] = (int)estimatingItemType;
                                                    objectType[13] = "\"";
                                                    obj4 = string.Concat(objectType);
                                                }
                                                variable2.Tag = string.Concat(tag, obj4);
                                            }
                                        }
                                    }
                                }
                                foreach (Variable collection2 in variable.Collection)
                                {
                                    if (results != null)
                                    {
                                        continue;
                                    }
                                    str4 = str2;
                                    str = new string[] { str4, collection2.Value.ToString(), collection2.Tag.ToString(), "/>", Environment.NewLine };
                                    str2 = string.Concat(str);
                                }
                                variable.Clear();
                                variable = null;
                                if (results != null)
                                {
                                    continue;
                                }
                                str2 = string.Concat(str2, indent, "\t\t\t\t</Results>", Environment.NewLine);
                                str2 = string.Concat(str2, indent, "\t\t\t</Extension>", Environment.NewLine);
                            }
                        }
                        if (results == null)
                        {
                            if (group.EstimatingItems.Count > 0)
                            {
                                str4 = str2;
                                str = new string[] { str4, indent, "\t\t\t<Extension Name=\"", Utilities.EscapeString(Resources.Items_d_estimation), "\">", Environment.NewLine };
                                str2 = string.Concat(str);
                                str2 = string.Concat(str2, indent, "\t\t\t\t<Results>", Environment.NewLine);
                                Variables variable3 = new Variables();
                                for (int l = (plan != null || layerName != "" ? 2 : 1); l > 0; l--)
                                {
                                    foreach (CEstimatingItem cEstimatingItem in group.EstimatingItems.Collection)
                                    {
                                        if (cEstimatingItem.Formula == "")
                                        {
                                            continue;
                                        }
                                        DBEstimatingItem dBEstimatingItem = this.project.DBManagement.GetEstimatingItem(Utilities.ConvertToInt(cEstimatingItem.ItemID));
                                        UnitScale.UnitSystem unitSystem = this.project.EstimatingItems.QueryEstimatingItemSystemType(groupObject.GroupID, cEstimatingItem.InternalKey, cEstimatingItem.ItemID, systemType, Utilities.ConvertToInt(cEstimatingItem.ItemID));
                                        DBEstimatingItem.UnitMeasureType unitMeasureType = (dBEstimatingItem != null ? dBEstimatingItem.UnitMeasure : cEstimatingItem.UnitMeasure);
                                        double num21 = (dBEstimatingItem != null ? dBEstimatingItem.CoverageRate : cEstimatingItem.CoverageRate);
                                        if (unitMeasureType == DBEstimatingItem.UnitMeasureType.m || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_m || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_m || cEstimatingItem.Unit.ToUpper() == "M" || cEstimatingItem.Unit.ToUpper() == "M²" || cEstimatingItem.Unit.ToUpper() == "M³")
                                        {
                                            unitSystem = UnitScale.UnitSystem.metric;
                                        }
                                        else if (unitMeasureType == DBEstimatingItem.UnitMeasureType.lin_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_yd || cEstimatingItem.Unit.ToUpper() == Resources.pi.ToUpper() || cEstimatingItem.Unit.ToUpper() == Resources.pi_2.ToUpper() || cEstimatingItem.Unit.ToUpper() == Resources.pi_3.ToUpper() || cEstimatingItem.Unit.ToUpper() == Resources.v_3.ToUpper())
                                        {
                                            unitSystem = UnitScale.UnitSystem.imperial;
                                        }
                                        GroupStats groupStat2 = null;
                                        if (plan != null)
                                        {
                                            groupStat2 = GroupUtilities.ComputeGroupStats(plan, groupObject, unitSystem, strs.Count == 1, (strs.Count > 1 ? strs[i] : string.Empty));
                                        }
                                        else if (layerName != "")
                                        {
                                            groupStat2 = GroupUtilities.ComputeGroupStats(this.project, layerName, groupObject, this.filter, unitSystem, strs.Count == 1, (strs.Count > 1 ? strs[i] : string.Empty));
                                        }
                                        GroupStats groupStat3 = GroupUtilities.ComputeGroupStats(this.project, groupObject, this.filter, unitSystem, strs.Count == 1, (strs.Count > 1 ? strs[i] : string.Empty));
                                        foreach (Preset collection3 in groupObject.Group.Presets.Collection)
                                        {
                                            UnitScale unitScale3 = new UnitScale(1f, unitSystem, precision, false);
                                            this.project.ExtensionsSupport.QueryPresetResults(collection3, groupStat3, unitScale3);
                                        }
                                        double num22 = 0;
                                        if (!FormulaUtilities.Compute(cEstimatingItem.Formula, (l == 2 ? preset : groupObject.Group.Presets), (l == 2 ? groupStat2 : groupStat3), cEstimatingItem.ResultSystemType(unitSystem), ref num22))
                                        {
                                            continue;
                                        }
                                        num22 = (num21 == 0 ? num22 : Math.Ceiling(num22 / num21));
                                        string str10 = unitScale.Round(num22).ToString();
                                        string str11 = str10.ToString();
                                        string lower1 = cEstimatingItem.Unit.ToLower();
                                        double num23 = 0;
                                        int sectionID = cEstimatingItem.SectionID;
                                        int subSectionID = cEstimatingItem.SubSectionID;
                                        string bidCode = cEstimatingItem.BidCode;
                                        DBEstimatingItem.EstimatingItemType itemType = cEstimatingItem.ItemType;
                                        str10 = string.Concat(str10, " ", lower1);
                                        Variable variable4 = variable3.Find(cEstimatingItem.InternalKey);
                                        if (variable4 == null)
                                        {
                                            string internalKey = cEstimatingItem.InternalKey;
                                            str = new string[] { indent, "\t\t\t\t\t<Result Name=\"", cEstimatingItem.InternalKey, "\" Caption=\"", Utilities.EscapeString(cEstimatingItem.Description), "\" Unit=\"", lower1, "\"" };
                                            string str12 = string.Concat(str);
                                            if (l == 2)
                                            {
                                                str = new string[] { " Value=\"", Utilities.EscapeString(str10), "\" RawValue=\"", str11, null, null };
                                                str[4] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, cEstimatingItem.InternalKey, cEstimatingItem.ItemID, "", cEstimatingItem.Description, Utilities.ConvertToDouble(str11, -1), null, ref num10, ref num11, lower1, num23);
                                                str[5] = "\"";
                                                obj1 = string.Concat(str);
                                            }
                                            else
                                            {
                                                objectType = new object[] { " TotalValue=\"", Utilities.EscapeString(str10), "\" TotalRawValue=\"", str11, null, null, null, null, null, null, null, null, null, null };
                                                objectType[4] = (groupStat == null ? this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, cEstimatingItem.InternalKey, cEstimatingItem.ItemID, "", cEstimatingItem.Description, Utilities.ConvertToDouble(str11, -1), null, ref num10, ref num11, lower1, num23) : "");
                                                objectType[5] = "\" SectionID=\"";
                                                objectType[6] = sectionID;
                                                objectType[7] = "\" SubSectionID=\"";
                                                objectType[8] = subSectionID;
                                                objectType[9] = "\" BidCode=\"";
                                                objectType[10] = Utilities.EscapeString(bidCode);
                                                objectType[11] = "\" ItemType=\"";
                                                objectType[12] = (int)itemType;
                                                objectType[13] = "\"";
                                                obj1 = string.Concat(objectType);
                                            }
                                            variable4 = new Variable(internalKey, str12, obj1);
                                            variable3.Add(variable4);
                                        }
                                        else
                                        {
                                            Variable variable5 = variable4;
                                            object tag1 = variable5.Tag;
                                            if (l == 2)
                                            {
                                                str = new string[] { " Value=\"", Utilities.EscapeString(str10), "\" RawValue=\"", str11, null, null };
                                                str[4] = this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, cEstimatingItem.InternalKey, cEstimatingItem.ItemID, "", Utilities.EscapeString(cEstimatingItem.Description), Utilities.ConvertToDouble(str11, -1), null, ref num10, ref num11, lower1, num23);
                                                str[5] = "\"";
                                                obj2 = string.Concat(str);
                                            }
                                            else
                                            {
                                                objectType = new object[] { " TotalValue=\"", Utilities.EscapeString(str10), "\" TotalRawValue=\"", str11, null, null, null, null, null, null, null, null, null, null };
                                                objectType[4] = (groupStat == null ? this.BuildXMLEstimatingItemResults(groupObject, (groupStat == null ? groupStat1 : groupStat), unitScale, cEstimatingItem.InternalKey, cEstimatingItem.ItemID, "", cEstimatingItem.Description, Utilities.ConvertToDouble(str11, -1), null, ref num10, ref num11, lower1, num23) : "");
                                                objectType[5] = "\" SectionID=\"";
                                                objectType[6] = sectionID;
                                                objectType[7] = "\" SubSectionID=\"";
                                                objectType[8] = subSectionID;
                                                objectType[9] = "\" BidCode=\"";
                                                objectType[10] = Utilities.EscapeString(bidCode);
                                                objectType[11] = "\" ItemType=\"";
                                                objectType[12] = (int)itemType;
                                                objectType[13] = "\"";
                                                obj2 = string.Concat(objectType);
                                            }
                                            variable5.Tag = string.Concat(tag1, obj2);
                                        }
                                    }
                                }
                                foreach (Variable collection4 in variable3.Collection)
                                {
                                    if (results != null)
                                    {
                                        continue;
                                    }
                                    str4 = str2;
                                    str = new string[] { str4, collection4.Value.ToString(), collection4.Tag.ToString(), "/>", Environment.NewLine };
                                    str2 = string.Concat(str);
                                }
                                variable3.Clear();
                                variable3 = null;
                                str2 = string.Concat(str2, indent, "\t\t\t\t</Results>", Environment.NewLine);
                                str2 = string.Concat(str2, indent, "\t\t\t</Extension>", Environment.NewLine);
                            }
                            str2 = string.Concat(str2, indent, "\t\t</Extensions>", Environment.NewLine);
                            if (this.ExportToCOffice && i == 0)
                            {
                                string str13 = "";
                                if (plan != null || flag)
                                {
                                    str13 = (!flag ? plan.Name : layerName);
                                }
                                else
                                {
                                    char tOUSLESPLANS = Resources.TOUS_LES_PLANS[0];
                                    str13 = string.Concat(tOUSLESPLANS.ToString(), Resources.TOUS_LES_PLANS.Substring(1, Resources.TOUS_LES_PLANS.Length - 1).ToLower());
                                }
                                str2 = string.Concat(str2, indent, "\t\t<COffice>", Environment.NewLine);
                                if (groupObject.Group != null)
                                {
                                    foreach (CEstimatingItem cEstimatingItem1 in groupObject.Group.COfficeProducts.Collection)
                                    {
                                        double num24 = 0;
                                        FormulaUtilities.Compute(cEstimatingItem1.Formula, groupObject.Group.Presets, (plan != null || flag ? groupStat : groupStat1), cEstimatingItem1.ResultSystemType(UnitScale.DefaultUnitSystem()), ref num24);
                                        str2 = string.Concat(str2, indent, "\t\t\t<CEstimatingItem ");
                                        str2 = string.Concat(str2, "Page=\"", str13, "\" ");
                                        str2 = string.Concat(str2, "DigitizerItem=\"", groupObject.Name, "\" ");
                                        str2 = string.Concat(str2, "ItemID=\"", cEstimatingItem1.ItemID, "\" ");
                                        str2 = string.Concat(str2, "Description=\"", Utilities.EscapeString(cEstimatingItem1.Description), "\" ");
                                        obj = str2;
                                        objectType = new object[] { obj, "Cost=\"", cEstimatingItem1.Value, "\" " };
                                        str2 = string.Concat(objectType);
                                        str2 = string.Concat(str2, "Unit=\"", Utilities.EscapeString(cEstimatingItem1.Unit), "\" ");
                                        str2 = string.Concat(str2, "Formula=\"", Utilities.EscapeString(cEstimatingItem1.Formula), "\" ");
                                        obj = str2;
                                        objectType = new object[] { obj, "Result=\"", num24, "\"" };
                                        str2 = string.Concat(objectType);
                                        str2 = string.Concat(str2, "/>", Environment.NewLine);
                                        if (this.CEstimatingItems == null)
                                        {
                                            continue;
                                        }
                                        CEstimatingItem cEstimatingItem2 = new CEstimatingItem(cEstimatingItem1.ItemID.Replace(',', '.'), cEstimatingItem1.Description, 0, cEstimatingItem1.Unit, cEstimatingItem1.ItemType, cEstimatingItem1.UnitMeasure, cEstimatingItem1.CoverageValue, cEstimatingItem1.CoverageUnit, cEstimatingItem1.SectionID, cEstimatingItem1.SubSectionID, cEstimatingItem1.BidCode, "")
                                        {
                                            Tag = new Variable(str13, groupObject.Name, num24.ToString().Replace(',', '.'))
                                        };
                                        this.CEstimatingItems.Add(cEstimatingItem2);
                                    }
                                }
                                str2 = string.Concat(str2, indent, "\t\t</COffice>", Environment.NewLine);
                            }
                            if (reportSummaries != null)
                            {
                                double num25 = (num11 > 0 ? (num11 - num10) / num11 : 0);
                                obj = str2;
                                objectType = new object[] { obj, indent, "\t\t<Summary CostTotalSubTotal=\"", string.Format("{0:C}", num10), "\" RawCostTotalSubTotal=\"", num10, "\" PriceTotalSubTotal=\"", string.Format("{0:C}", num11), "\" RawPriceTotalSubTotal=\"", num11, "\" MarginSubTotal=\"", string.Format("{0:0.00%}", num25), "\" RawMarginSubTotal=\"", num25, "\"/>", Environment.NewLine };
                                str2 = string.Concat(objectType);
                                reportSummaries.Add(new ReportSummary(string.Concat(groupObject.Name.ToString(), (i == 0 ? "" : "*")), num10, num11, plan, groupObject, groupObject.GroupID, layerName));
                            }
                            str2 = string.Concat(str2, indent, "\t</Object>", Environment.NewLine);
                        }
                        preset.Clear();
                        preset = null;
                    }
                }
            }
            return str2;
        }

        private string BuildXMLObjects(List<Variable> groups, List<ReportSummary> reportSummaries, Plan plan, string layerName, Variables plans, bool includeHiddenValues = false, bool exportForExcel = false, List<EstimatingItem> results = null)
        {
            int num = 0;
            int num1 = 0;
            string str = "";
            string str1 = "";
            for (int i = 0; i < groups.Count; i++)
            {
                str = string.Concat(str, this.BuildXMLObject((DrawObject)groups[i].Tag, plan, layerName, plans, reportSummaries, ref str1, ref num, ref num1, includeHiddenValues, exportForExcel, results));
            }
            return str;
        }

        private string BuildXMLObjectSummaries(List<ReportSummary> reportSummaries, string indent, bool isPlanSummaries = false)
        {
            bool taxOnTax = Settings.Default.TaxOnTax;
            string tax1Label = Settings.Default.Tax1Label;
            string tax2Label = Settings.Default.Tax2Label;
            double tax1Rate = Settings.Default.Tax1Rate;
            double tax2Rate = Settings.Default.Tax2Rate;
            double costSubTotal = 0;
            double priceSubTotal = 0;
            string str = "";
            foreach (ReportSummary reportSummary in reportSummaries)
            {
                if (reportSummary.Caption.IndexOf('*') != -1)
                {
                    continue;
                }
                costSubTotal += reportSummary.CostSubTotal;
                priceSubTotal += reportSummary.PriceSubTotal;
            }
            if (!isPlanSummaries)
            {
                str = string.Concat(str, indent, "\t<Summaries>", Environment.NewLine);
            }
            foreach (ReportSummary reportSummary1 in reportSummaries)
            {
                if (reportSummary1.Caption.IndexOf('*') != -1)
                {
                    continue;
                }
                double num = reportSummary1.CostSubTotal;
                double priceSubTotal1 = reportSummary1.PriceSubTotal;
                double num1 = (priceSubTotal1 > 0 ? (priceSubTotal1 - num) / priceSubTotal1 : 0);
                double num2 = (priceSubTotal > 0 ? priceSubTotal1 / priceSubTotal : 0);
                string str1 = string.Concat(num2.ToString(), ";");
                DrawObject groupObject = reportSummary1.GroupObject;
                if (groupObject != null)
                {
                    Color color = groupObject.Color;
                    str1 = string.Concat(str1, color.ToArgb(), ";");
                }
                object obj = str;
                object[] objArray = new object[] { obj, indent, "\t\t<", (!isPlanSummaries ? "Summary" : "PlanSummary"), " Name=\"", Utilities.EscapeString(reportSummary1.Caption), "\" CostTotalSubTotal=\"", string.Format("{0:C}", num), "\" RawCostTotalSubTotal=\"", num, "\" PriceTotalSubTotal=\"", string.Format("{0:C}", priceSubTotal1), "\" RawPriceTotalSubTotal=\"", priceSubTotal1, "\" MarginSubTotal=\"", string.Format("{0:0.00%}", num1), "\" RawMarginSubTotal=\"", num1, "\" TotalBreakdown=\"", string.Format("{0:0.00%}", num2), "\" RawTotalBreakdown=\"", num2, "\" TotalBreakdownTag=\"", str1, "\"/>", Environment.NewLine };
                str = string.Concat(objArray);
            }
            string str2 = string.Concat(tax1Label, " (", string.Format("{0:0.#####%}", tax1Rate), ") :");
            string str3 = string.Concat(tax2Label, " (", string.Format("{0:0.#####%}", tax2Rate), ") :");
            double num3 = priceSubTotal * tax1Rate;
            num3 = Math.Round(num3, 2);
            double num4 = (taxOnTax ? (num3 + priceSubTotal) * tax2Rate : priceSubTotal * tax2Rate);
            num4 = Math.Round(num4, 2);
            double num5 = priceSubTotal + num3 + num4;
            num5 = Math.Round(num5, 2);
            double num6 = (priceSubTotal > 0 ? (priceSubTotal - costSubTotal) / priceSubTotal : 0);
            object obj1 = str;
            object[] objArray1 = new object[] { obj1, indent, "\t\t<", (!isPlanSummaries ? "Total" : "PlanTotal"), " CostTotal=\"", string.Format("{0:C}", costSubTotal), "\" RawCostTotal=\"", costSubTotal, "\" PriceTotal=\"", string.Format("{0:C}", priceSubTotal), "\" RawPriceTotal=\"", priceSubTotal, "\" MarginTotal=\"", string.Format("{0:0.00%}", num6), "\" RawMarginTotal=\"", num6, "\" Taxe1Rate=\"", string.Format("{0:0.00%}", tax1Rate), "\" RawTaxe1Rate=\"", tax1Rate, "\" Taxe2Rate=\"", string.Format("{0:0.00%}", tax2Rate), "\" RawTaxe2Rate=\"", tax2Rate, "\" Taxe1Caption=\"", str2, "\" Taxe2Caption=\"", str3, "\" Taxe1Total=\"", string.Format("{0:C}", num3), "\" RawTaxe1Total=\"", num3, "\" Taxe2Total=\"", string.Format("{0:C}", num4), "\" RawTaxe2Total=\"", num4, "\" TotalAfterTaxes=\"", string.Format("{0:C}", num5), "\" RawTotalAfterTaxes=\"", num5, "\"/>", Environment.NewLine };
            str = string.Concat(objArray1);
            if (!isPlanSummaries)
            {
                str = string.Concat(str, indent, "\t</Summaries>", Environment.NewLine);
            }
            return str;
        }

        private string BuildXMLOrderByLayers(bool includeHiddenValues = false, bool exportForExcel = false)
        {
            string str = "";
            string str1 = "";
            List<Variable> variables = new List<Variable>();
            List<List<ReportSummary>> lists = new List<List<ReportSummary>>();
            foreach (Plan collection in this.project.Plans.Collection)
            {
                if (this.filter.QueryFilter(collection.Name, "", -1, false))
                {
                    continue;
                }
                foreach (Layer layer in collection.Layers.Collection)
                {
                    if (this.filter.QueryFilter(collection.Name, layer.Name, -1, false))
                    {
                        continue;
                    }
                    foreach (DrawObject drawObject in layer.DrawingObjects.Collection)
                    {
                        if (!drawObject.IsPartOfGroup() || drawObject.IsDeduction() || this.filter.QueryFilter(collection.Name, layer.Name, drawObject.GroupID, false))
                        {
                            continue;
                        }
                        Variable variable = variables.Find((Variable x) => x.Name == layer.Name);
                        if (variable == null)
                        {
                            List<Variable> variables1 = new List<Variable>();
                            variable = new Variable(layer.Name, variables1);
                            variables.Add(variable);
                        }
                        if (variable == null)
                        {
                            continue;
                        }
                        List<Variable> value = (List<Variable>)variable.Value;
                        if (value.Find((Variable x) => Utilities.ConvertToInt(x.Value) == drawObject.GroupID) != null)
                        {
                            continue;
                        }
                        value.Add(new Variable(drawObject.Name, (object)drawObject.GroupID, drawObject));
                    }
                }
            }
            str = string.Concat(str, "\t<Plans>", Environment.NewLine);
            variables.Sort(new ReportModule.LayerSorter());
            foreach (Variable variable1 in variables)
            {
                List<Variable> value1 = (List<Variable>)variable1.Value;
                value1.Sort(new ReportModule.GroupSorter());
                List<ReportSummary> reportSummaries = new List<ReportSummary>();
                string str2 = this.BuildXMLObjects(value1, reportSummaries, null, variable1.Name, null, includeHiddenValues, exportForExcel, null);
                if (str2.Trim() == "")
                {
                    reportSummaries.Clear();
                    reportSummaries = null;
                }
                else
                {
                    string str3 = str1;
                    string[] strArrays = new string[] { str3, "\t\t\t\t<Layer Name=\"", Utilities.EscapeString(variable1.Name), "\">", Environment.NewLine };
                    str1 = string.Concat(strArrays);
                    str1 = string.Concat(str1, str2);
                    str1 = string.Concat(str1, "\t\t\t\t</Layer>", Environment.NewLine);
                    lists.Add(reportSummaries);
                }
            }
            if (str1.Trim() != "")
            {
                str = string.Concat(str, "\t\t<Plan Name=\"\">", Environment.NewLine);
                str = string.Concat(str, "\t\t\t<Objects>", Environment.NewLine);
                str = string.Concat(str, str1);
                str = string.Concat(str, "\t\t\t</Objects>", Environment.NewLine);
                str = string.Concat(str, "\t\t</Plan>", Environment.NewLine);
            }
            str = string.Concat(str, "\t</Plans>", Environment.NewLine);
            this.ReportSummaries = this.BuildXMLLayersSummaries(lists);
            str = string.Concat(str, this.ReportSummaries);
            foreach (List<ReportSummary> list in lists)
            {
                list.Clear();
            }
            lists.Clear();
            lists = null;
            return str;
        }

        private string BuildXMLOrderByObjects(bool includeHiddenValues = false, bool exportForExcel = false, List<EstimatingItem> results = null)
        {
            string str = "";
            List<Variable> variables = new List<Variable>();
            List<ReportSummary> reportSummaries = new List<ReportSummary>();
            Variables variable = new Variables();
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
                        bool flag = false;
                        if (this.project.Report.SelectedReportType != ReportTypeEnum.QuoteReport)
                        {
                            flag = this.filter.QueryFilter("", "", drawObject.GroupID, false);
                            if (!flag)
                            {
                                flag = this.filter.QueryFilter(collection.Name, drawObject.GroupID);
                            }
                        }
                        else
                        {
                            flag = this.filter.QueryFilter(collection.Name, "", -1, false);
                            if (!flag)
                            {
                                flag = this.filter.QueryFilter(collection.Name, layer.Name, -1, false);
                            }
                            if (!flag)
                            {
                                flag = this.filter.QueryFilter(collection.Name, layer.Name, drawObject.GroupID, false);
                            }
                        }
                        if (flag)
                        {
                            continue;
                        }
                        if (variables.Find((Variable x) => Utilities.ConvertToInt(x.Value) == drawObject.GroupID) == null)
                        {
                            variables.Add(new Variable(drawObject.Name, (object)drawObject.GroupID, drawObject));
                        }
                        bool flag1 = false;
                        foreach (Variable collection1 in variable.Collection)
                        {
                            if (!(collection1.Name == drawObject.GroupID.ToString()) || !(collection1.Value.ToString() == collection.Name))
                            {
                                continue;
                            }
                            flag1 = true;
                            break;
                        }
                        if (flag1)
                        {
                            continue;
                        }
                        int groupID = drawObject.GroupID;
                        variable.Add(new Variable(groupID.ToString(), collection.Name));
                    }
                }
            }
            str = string.Concat("\t<Objects>", Environment.NewLine);
            variables.Sort(new ReportModule.GroupSorter());
            str = string.Concat(str, this.BuildXMLObjects(variables, reportSummaries, null, "", variable, includeHiddenValues, exportForExcel, results));
            str = string.Concat(str, "\t</Objects>", Environment.NewLine);
            this.ReportSummaries = this.BuildXMLObjectSummaries(reportSummaries, "", false);
            str = string.Concat(str, this.ReportSummaries);
            variables.Clear();
            variables = null;
            reportSummaries.Clear();
            reportSummaries = null;
            variable.Clear();
            variable = null;
            if (results == null)
            {
                return str;
            }
            return string.Empty;
        }

        private string BuildXMLOrderByPlans(bool includeHiddenValues = false, bool exportForExcel = false)
        {
            string str = "";
            List<List<ReportSummary>> lists = new List<List<ReportSummary>>();
            str = string.Concat(str, "\t<Plans>", Environment.NewLine);
            foreach (Plan collection in this.project.Plans.Collection)
            {
                List<Variable> variables = new List<Variable>();
                string str1 = "";
                if (!this.filter.QueryFilter(collection.Name, "", -1, false))
                {
                    foreach (Layer layer in collection.Layers.Collection)
                    {
                        if (!this.filter.QueryFilter(collection.Name, layer.Name, -1, false))
                        {
                            foreach (DrawObject drawObject in layer.DrawingObjects.Collection)
                            {
                                if (!drawObject.IsPartOfGroup() || drawObject.IsDeduction() || this.filter.QueryFilter(collection.Name, layer.Name, drawObject.GroupID, false))
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
                        variables.Sort(new ReportModule.GroupSorter());
                        List<ReportSummary> reportSummaries = new List<ReportSummary>();
                        string str2 = this.BuildXMLObjects(variables, reportSummaries, collection, layer.Name, null, includeHiddenValues, exportForExcel, null);
                        if (str2.Trim() == "")
                        {
                            reportSummaries.Clear();
                            reportSummaries = null;
                        }
                        else
                        {
                            bool flag = true;
                            if (flag)
                            {
                                string str3 = str1;
                                string[] strArrays = new string[] { str3, "\t\t\t\t<Layer Name=\"", Utilities.EscapeString(layer.Name), "\">", Environment.NewLine };
                                str1 = string.Concat(strArrays);
                            }
                            str1 = string.Concat(str1, str2);
                            if (flag)
                            {
                                str1 = string.Concat(str1, "\t\t\t\t</Layer>", Environment.NewLine);
                            }
                            lists.Add(reportSummaries);
                        }
                        variables.Clear();
                    }
                    if (str1.Trim() != "")
                    {
                        string str4 = str;
                        string[] strArrays1 = new string[] { str4, "\t\t<Plan Name=\"", Utilities.EscapeString(collection.Name), "\">", Environment.NewLine };
                        str = string.Concat(strArrays1);
                        str = string.Concat(str, "\t\t\t<Objects>", Environment.NewLine);
                        str = string.Concat(str, str1);
                        str = string.Concat(str, "\t\t\t</Objects>", Environment.NewLine);
                        str = string.Concat(str, "\t\t</Plan>", Environment.NewLine);
                    }
                }
                variables = null;
            }
            str = string.Concat(str, "\t</Plans>", Environment.NewLine);
            this.ReportSummaries = this.BuildXMLPlansSummaries(lists);
            str = string.Concat(str, this.ReportSummaries);
            foreach (List<ReportSummary> list in lists)
            {
                list.Clear();
            }
            lists.Clear();
            lists = null;
            return str;
        }

        private string BuildXMLPlansSummaries(List<List<ReportSummary>> reportAllSummaries)
        {
            string str = "";
            double num = 0;
            double num1 = 0;
            double num2 = 0;
            Plan plan = null;
            if (reportAllSummaries.Count > 0)
            {
                bool flag = false;
                string name = "";
                List<ReportSummary> reportSummaries = new List<ReportSummary>();
                List<ReportSummary> reportSummaries1 = new List<ReportSummary>();
                str = string.Concat(str, "\t<PlansSummaries>", Environment.NewLine);
                foreach (List<ReportSummary> reportAllSummary in reportAllSummaries)
                {
                    if (reportAllSummary.Count <= 0)
                    {
                        continue;
                    }
                    if (name != reportAllSummary[0].Plan.Name)
                    {
                        if (flag)
                        {
                            num2 = (num1 - num) / num1;
                            object obj = str;
                            object[] objArray = new object[] { obj, "\t\t\t<Total CostTotal=\"", string.Format("{0:C}", num), "\" RawCostTotal=\"", num, "\" PriceTotal=\"", string.Format("{0:C}", num1), "\" RawPriceTotal=\"", num1, "\" MarginTotal=\"", string.Format("{0:0.00%}", num2), "\" RawMarginTotal=\"", num2, "\"/>", Environment.NewLine };
                            str = string.Concat(objArray);
                            reportSummaries.Add(new ReportSummary(plan.Name, num, num1, plan, null, -1, ""));
                            num = 0;
                            num1 = 0;
                            str = string.Concat(str, "\t\t</PlanSummaries>", Environment.NewLine);
                        }
                        string str1 = str;
                        string[] strArrays = new string[] { str1, "\t\t<PlanSummaries Name=\"", Utilities.EscapeString(reportAllSummary[0].Plan.Name), "\">", Environment.NewLine };
                        str = string.Concat(strArrays);
                        plan = reportAllSummary[0].Plan;
                        flag = true;
                    }
                    str = string.Concat(str, this.BuildXMLLayerSummaries(reportSummaries1, reportAllSummary, ref num, ref num1));
                    if (name == reportAllSummary[0].Plan.Name)
                    {
                        continue;
                    }
                    name = reportAllSummary[0].Plan.Name;
                }
                if (flag)
                {
                    num2 = (num1 > 0 ? (num1 - num) / num1 : 0);
                    object obj1 = str;
                    object[] objArray1 = new object[] { obj1, "\t\t\t<Total CostTotal=\"", string.Format("{0:C}", num), "\" RawCostTotal=\"", num, "\" PriceTotal=\"", string.Format("{0:C}", num1), "\" RawPriceTotal=\"", num1, "\" MarginTotal=\"", string.Format("{0:0.00%}", num2), "\" RawMarginTotal=\"", num2, "\"/>", Environment.NewLine };
                    str = string.Concat(objArray1);
                    reportSummaries.Add(new ReportSummary(plan.Name, num, num1, plan, null, -1, ""));
                    num = 0;
                    num1 = 0;
                    str = string.Concat(str, "\t\t</PlanSummaries>", Environment.NewLine);
                    flag = false;
                }
                str = string.Concat(str, "\t</PlansSummaries>", Environment.NewLine);
                reportSummaries.Sort(new ReportModule.SummarySorter());
                str = string.Concat(str, this.BuildXMLSummaries(reportSummaries, ""));
                reportSummaries.Clear();
                reportSummaries = null;
                reportSummaries1.Clear();
                reportSummaries1 = null;
            }
            return str;
        }

        private string BuildXMLProjectInfo()
        {
            string str = "";
            string str1 = str;
            string[] strArrays = new string[] { str1, "\t<Project Name=\"", Utilities.EscapeString(this.project.Name), "\">", Environment.NewLine };
            str = string.Concat(strArrays);
            string str2 = str;
            string[] strArrays1 = new string[] { str2, "\t\t<ContactName>", Utilities.EscapeString(this.project.ContactName), "</ContactName>", Environment.NewLine };
            str = string.Concat(strArrays1);
            string description = this.project.Description;
            char[] chrArray = new char[] { '\r', '\n' };
            string[] fields = Utilities.GetFields(description, chrArray);
            if (fields.GetUpperBound(0) >= 0)
            {
                str = string.Concat(str, "\t\t<Description>");
                string[] strArrays2 = fields;
                for (int i = 0; i < (int)strArrays2.Length; i++)
                {
                    string str3 = strArrays2[i];
                    str = string.Concat(str, Utilities.EscapeString(str3), "&#xD;");
                }
                str = string.Concat(str, "</Description>", Environment.NewLine);
            }
            string contactInfo = this.project.ContactInfo;
            char[] chrArray1 = new char[] { '\r', '\n' };
            fields = Utilities.GetFields(contactInfo, chrArray1);
            if (fields.GetUpperBound(0) >= 0)
            {
                str = string.Concat(str, "\t\t<ContactInfo>");
                string[] strArrays3 = fields;
                for (int j = 0; j < (int)strArrays3.Length; j++)
                {
                    string str4 = strArrays3[j];
                    str = string.Concat(str, Utilities.EscapeString(str4), "&#xD;");
                }
                str = string.Concat(str, "</ContactInfo>", Environment.NewLine);
            }
            string comment = this.project.Comment;
            char[] chrArray2 = new char[] { '\r', '\n' };
            fields = Utilities.GetFields(comment, chrArray2);
            if (fields.GetUpperBound(0) >= 0)
            {
                str = string.Concat(str, "\t\t<Comment>");
                string[] strArrays4 = fields;
                for (int k = 0; k < (int)strArrays4.Length; k++)
                {
                    string str5 = strArrays4[k];
                    str = string.Concat(str, Utilities.EscapeString(str5), "&#xD;");
                }
                str = string.Concat(str, "</Comment>", Environment.NewLine);
            }
            str = string.Concat(str, "\t</Project>", Environment.NewLine);
            return str;
        }

        private string BuildXMLSummaries(List<ReportSummary> reportSummaries, string indent)
        {
            bool taxOnTax = Settings.Default.TaxOnTax;
            string tax1Label = Settings.Default.Tax1Label;
            string tax2Label = Settings.Default.Tax2Label;
            double tax1Rate = Settings.Default.Tax1Rate;
            double tax2Rate = Settings.Default.Tax2Rate;
            double costSubTotal = 0;
            double priceSubTotal = 0;
            string str = "";
            foreach (ReportSummary reportSummary in reportSummaries)
            {
                costSubTotal += reportSummary.CostSubTotal;
                priceSubTotal += reportSummary.PriceSubTotal;
            }
            str = string.Concat(str, indent, "\t<Summaries>", Environment.NewLine);
            int num = 11;
            foreach (ReportSummary reportSummary1 in reportSummaries)
            {
                double costSubTotal1 = reportSummary1.CostSubTotal;
                double priceSubTotal1 = reportSummary1.PriceSubTotal;
                double num1 = (priceSubTotal1 > 0 ? (priceSubTotal1 - costSubTotal1) / priceSubTotal1 : 0);
                double num2 = (priceSubTotal > 0 ? priceSubTotal1 / priceSubTotal : 0);
                string str1 = string.Concat(num2.ToString(), ";");
                int num3 = num + 1;
                num = num3;
                Color basicColor = Utilities.GetBasicColor(num3);
                str1 = string.Concat(str1, basicColor.ToArgb(), ";");
                if (num == 15)
                {
                    num = 0;
                }
                object obj = str;
                object[] objArray = new object[] { obj, indent, "\t\t<Summary Name=\"", Utilities.EscapeString(reportSummary1.Caption), "\" CostTotalSubTotal=\"", string.Format("{0:C}", costSubTotal1), "\" RawCostTotalSubTotal=\"", costSubTotal1, "\" PriceTotalSubTotal=\"", string.Format("{0:C}", priceSubTotal1), "\" RawPriceTotalSubTotal=\"", priceSubTotal1, "\" MarginSubTotal=\"", string.Format("{0:0.00%}", num1), "\" RawMarginSubTotal=\"", num1, "\" TotalBreakdown=\"", string.Format("{0:0.00%}", num2), "\" RawTotalBreakdown=\"", num2, "\" TotalBreakdownTag=\"", str1, "\"/>", Environment.NewLine };
                str = string.Concat(objArray);
            }
            string str2 = string.Concat(tax1Label, " (", string.Format("{0:0.#####%}", tax1Rate), ") :");
            string str3 = string.Concat(tax2Label, " (", string.Format("{0:0.#####%}", tax2Rate), ") :");
            double num4 = priceSubTotal * tax1Rate;
            num4 = Math.Round(num4, 2);
            double num5 = (taxOnTax ? (num4 + priceSubTotal) * tax2Rate : priceSubTotal * tax2Rate);
            num5 = Math.Round(num5, 2);
            double num6 = priceSubTotal + num4 + num5;
            num6 = Math.Round(num6, 2);
            double num7 = (priceSubTotal > 0 ? (priceSubTotal - costSubTotal) / priceSubTotal : 0);
            object obj1 = str;
            object[] objArray1 = new object[] { obj1, indent, "\t\t<Total CostTotal=\"", string.Format("{0:C}", costSubTotal), "\" RawCostTotal=\"", costSubTotal, "\" PriceTotal=\"", string.Format("{0:C}", priceSubTotal), "\" RawPriceTotal=\"", priceSubTotal, "\" MarginTotal=\"", string.Format("{0:0.00%}", num7), "\" RawMarginTotal=\"", num7, "\" Taxe1Rate=\"", string.Format("{0:0.00%}", tax1Rate), "\" RawTaxe1Rate=\"", tax1Rate, "\" Taxe2Rate=\"", string.Format("{0:0.00%}", tax2Rate), "\" RawTaxe2Rate=\"", tax2Rate, "\" Taxe1Caption=\"", str2, "\" Taxe2Caption=\"", str3, "\" Taxe1Total=\"", string.Format("{0:C}", num4), "\" RawTaxe1Total=\"", num4, "\" Taxe2Total=\"", string.Format("{0:C}", num5), "\" RawTaxe2Total=\"", num5, "\" TotalAfterTaxes=\"", string.Format("{0:C}", num6), "\" RawTotalAfterTaxes=\"", num6, "\"/>", Environment.NewLine };
            str = string.Concat(objArray1);
            str = string.Concat(str, indent, "\t</Summaries>", Environment.NewLine);
            return str;
        }

        private string BuildXSL(Report.ReportOrderEnum order)
        {
            string str = "";
            switch (order)
            {
                case Report.ReportOrderEnum.ReportOrderByObjects:
                    {
                        str = this.BuildXSLOrderByObjects();
                        break;
                    }
                case Report.ReportOrderEnum.ReportOrderByPlans:
                    {
                        str = this.BuildXSLOrderByPlans();
                        break;
                    }
            }
            return str;
        }

        private string BuildXSLOrderByObjects()
        {
            string str = Utilities.ReadToString(Path.Combine(Utilities.GetInstallReportsFolder(), string.Concat(Utilities.GetCurrentValidUICulture(), "\\sort_by_objects.xsl")));
            return str;
        }

        private string BuildXSLOrderByPlans()
        {
            string str = Utilities.ReadToString(Path.Combine(Utilities.GetInstallReportsFolder(), string.Concat(Utilities.GetCurrentValidUICulture(), "\\sort_by_plans.xsl")));
            return str;
        }

        public void CleanUpFilters()
        {
            Console.WriteLine("ReportModule.CleanUpFilters");
            using (ReportEditForm reportEditForm = new ReportEditForm(this.project, this.drawArea))
            {
                reportEditForm.CleanUpFilters();
            }
        }

        public void Clear()
        {
            this.LoadHtmlDocument(this.FormatHtmlString(""));
        }

        public void ExportObjectToList(DrawObject groupObject, List<EstimatingItem> results)
        {
            int num = 0;
            int num1 = 0;
            string str = "";
            this.BuildXMLObject(groupObject, null, "", null, null, ref str, ref num, ref num1, true, false, results);
        }

        public void ExportToExcel(string fileName, bool exportEstimating)
        {
            this.LoadFilter();
            string str = fileName;
            if (this.BuildXLS(this.project.Report.Order, this.project.Report.ReportSortBy, exportEstimating, str))
            {
                Utilities.OpenDocument(str);
            }
        }

        public void ExportToList(List<EstimatingItem> results)
        {
            this.BuildXMLOrderByObjects(true, false, results);
        }

        public string ExportToXML(bool exportEstimatingItems, bool exportToMemory = false)
        {
            this.LoadFilter();
            this.ExportEstimatingItems = exportEstimatingItems;
            this.ExportReportToMemory = exportToMemory;
            this.ReportSummaries = "";
            string str = this.BuildXML(this.project.Report.Order, false, false);
            if (!exportToMemory)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.project.FileName);
                fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "-", (this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByObjects ? Resources.par_objets : Resources.par_plans), ".xml");
                fileNameWithoutExtension = Path.Combine(Utilities.GetReportsFolder(), fileNameWithoutExtension.Replace(' ', '-'));
                Utilities.SaveStringToFile(fileNameWithoutExtension, str);
                Utilities.OpenDocument(fileNameWithoutExtension);
                return string.Empty;
            }
            string str1 = string.Concat("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", Environment.NewLine);
            string[] strArrays = new string[] { str1, "<Report Title=\"", Utilities.EscapeString(this.project.Name), "\" Date=\"", Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()), "\">", Environment.NewLine };
            string str2 = string.Concat(strArrays);
            str2 = string.Concat(str2, this.ReportSummaries);
            str2 = string.Concat(str2, "</Report>", Environment.NewLine);
            this.ReportSummaries = str2;
            return str;
        }

        private string FormatHtmlString(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<head>");
            stringBuilder.AppendLine("<title>Title</title>");
            stringBuilder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"application/xhtml+xml; charset=utf-8\"/>");
            stringBuilder.AppendLine("</head>");
            stringBuilder.AppendLine("<body>");
            stringBuilder.AppendLine(text);
            stringBuilder.AppendLine("</body>");
            stringBuilder.AppendLine("</html>");
            return stringBuilder.ToString();
        }

        public void IERestoreRegistry()
        {
            try
            {
                bool flag = true;
                string str = "header";
                string str1 = "footer";
                string str2 = "Print_Background";
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", flag);
                registryKey.SetValue(str, this.registerHeaderOriginalValue);
                registryKey.SetValue(str1, this.registerFooterOriginalValue);
                registryKey.SetValue(str2, this.registerPrintBackgroundOriginalValue);
                registryKey.Close();
            }
            catch
            {
            }
        }

        private void IESetupHeaderFooter(string dateString)
        {
            try
            {
                bool flag = true;
                string str = "header";
                string str1 = "footer";
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", flag);
                registryKey.SetValue(str, Resources.Page_x_de_n);
                registryKey.SetValue(str1, string.Concat(Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan, dateString));
                registryKey.Close();
            }
            catch
            {
            }
        }

        private void IESetupRegistry()
        {
            try
            {
                bool flag = true;
                string str = "header";
                string str1 = "footer";
                string str2 = "Print_Background";
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", flag);
                this.registerHeaderOriginalValue = registryKey.GetValue(str, "");
                registryKey.SetValue(str, "");
                this.registerFooterOriginalValue = registryKey.GetValue(str1, "");
                registryKey.SetValue(str1, "");
                this.registerPrintBackgroundOriginalValue = registryKey.GetValue(str2, "");
                registryKey.SetValue(str2, "yes");
                registryKey.Close();
            }
            catch
            {
            }
        }

        private void InitializeWebBrowser()
        {
        }

        private void LoadFilter()
        {
            if (!this.project.Report.ApplyFilter)
            {
                this.filter.Clear();
                return;
            }
            if (this.project.Report.SelectedReportType == ReportTypeEnum.QuoteReport)
            {
                this.filter.LoadFromString(this.project.Report.OrderByPlansFilter, Report.ReportOrderEnum.ReportOrderByPlans);
                return;
            }
            this.filter.LoadFromString((this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByPlans ? this.project.Report.OrderByPlansFilter : this.project.Report.OrderByObjectsFilter), this.project.Report.Order);
        }

        private void LoadHtmlDocument(string htmlDocument)
        {
            if (this.webBrowser.Document == null)
            {
                this.webBrowser.Navigate("about:blank");
            }
            else
            {
                this.webBrowser.Document.OpenNew(true);
            }
            while (this.webBrowser.Document == null && this.webBrowser.Document.Body == null)
            {
                Application.DoEvents();
            }
            this.webBrowser.DocumentText = htmlDocument;
        }

        private void LoadResources()
        {
        }

        private string MakeHTML(string xml, string xsl, bool useAbsolutePath)
        {
            string str;
            try
            {
                this.IESetupHeaderFooter(Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()));
                StringReader stringReader = new StringReader(xml);
                XPathDocument xPathDocument = new XPathDocument(stringReader);
                StringReader stringReader1 = new StringReader(xsl);
                XmlTextReader xmlTextReader = new XmlTextReader(stringReader1);
                XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
                xslCompiledTransform.Load(xmlTextReader, null, null);
                XsltArgumentList xsltArgumentList = new XsltArgumentList();
                xsltArgumentList.AddParam("title", "", this.project.Name);
                xsltArgumentList.AddParam("app_path", "", Utilities.GetInstallFolder());
                string installReportsFolder = Utilities.GetInstallReportsFolder();
                string[] currentValidUICulture = new string[] { "css\\", Utilities.GetCurrentValidUICulture(), "\\", Settings.Default.ReportTheme, ".css" };
                string str1 = Path.Combine(installReportsFolder, string.Concat(currentValidUICulture));
                string str2 = Path.Combine(Utilities.GetReportsFolder(), "css\\style.css");
                if (!useAbsolutePath)
                {
                    Utilities.FileCopy(str1, str2);
                }
                xsltArgumentList.AddParam("css_path", "", (useAbsolutePath ? str1 : "css\\style.css"));
                xsltArgumentList.AddParam("images_path", "", (useAbsolutePath ? Path.Combine(Utilities.GetInstallReportsFolder(), "images") : "images"));
                ReportModule.UTF8StringWriter uTF8StringWriter = new ReportModule.UTF8StringWriter();
                xslCompiledTransform.Transform(xPathDocument, xsltArgumentList, uTF8StringWriter);
                string str3 = uTF8StringWriter.ToString();
                stringReader.Close();
                stringReader1.Close();
                xmlTextReader.Close();
                uTF8StringWriter.Close();
                str = str3;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                str = "";
            }
            return str;
        }

        private string RenameInFilter(string filter, string objectType, string oldName, string newName, string planName = "")
        {
            string str = Filter.Rename(filter, objectType, oldName, newName, planName);
            Console.WriteLine(string.Concat("Old filter = ", filter));
            Console.WriteLine(string.Concat("New filter = ", str));
            return str;
        }

        public void RenameLayer(Plan plan, Layer layer)
        {
            if (layer.PrevName == layer.Name)
            {
                return;
            }
            Console.WriteLine("OrderByPlansFilter");
            this.project.Report.OrderByPlansFilter = this.RenameInFilter(this.project.Report.OrderByPlansFilter, "#LAYER", layer.PrevName, layer.Name, plan.Name);
        }

        public void RenamePlan(Plan plan)
        {
            if (plan.PrevName == plan.Name)
            {
                return;
            }
            Console.WriteLine("OrderByPlansFilter");
            this.project.Report.OrderByPlansFilter = this.RenameInFilter(this.project.Report.OrderByPlansFilter, "#PLAN", plan.PrevName, plan.Name, "");
            Console.WriteLine("OrderByObjectsFilter");
            this.project.Report.OrderByObjectsFilter = this.RenameInFilter(this.project.Report.OrderByObjectsFilter, "#PLAN", plan.PrevName, plan.Name, "");
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (this.webBrowser.Document != null)
            {
                bool flag = this.enabled;
            }
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            HtmlDocument document = this.webBrowser.Document;
        }

        [CompilerGenerated]
        // <>c__DisplayClass12
        private sealed class u003cu003ec__DisplayClass12
        {
            // CS$<>8__locals10
            public ReportModule.u003cu003ec__DisplayClassf CSu0024u003cu003e8__locals10;

            public DrawObject layerObject;

            public u003cu003ec__DisplayClass12()
            {
            }

            // <BuildXMLOrderByLayers>b__d
            public bool u003cBuildXMLOrderByLayersu003eb__d(Variable x)
            {
                return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClass16
        private sealed class u003cu003ec__DisplayClass16
        {
            public DrawObject layerObject;

            public u003cu003ec__DisplayClass16()
            {
            }

            // <BuildXMLOrderByPlans>b__14
            public bool u003cBuildXMLOrderByPlansu003eb__14(Variable x)
            {
                return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClass2
        private sealed class u003cu003ec__DisplayClass2
        {
            public ReportSummary reportSummary;

            public u003cu003ec__DisplayClass2()
            {
            }

            // <BuildXMLLayerSummaries>b__0
            public bool u003cBuildXMLLayerSummariesu003eb__0(ReportSummary x)
            {
                return x.GroupID == this.reportSummary.GroupID;
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClass6
        private sealed class u003cu003ec__DisplayClass6
        {
            public ReportSummary reportSummary;

            public u003cu003ec__DisplayClass6()
            {
            }

            // <BuildXMLLayersSummaries>b__4
            public bool u003cBuildXMLLayersSummariesu003eb__4(ReportSummary x)
            {
                return x.LayerName == this.reportSummary.LayerName;
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClassa
        private sealed class u003cu003ec__DisplayClassa
        {
            public DrawObject layerObject;

            public u003cu003ec__DisplayClassa()
            {
            }

            // <BuildXMLOrderByObjects>b__8
            public bool u003cBuildXMLOrderByObjectsu003eb__8(Variable x)
            {
                return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClassf
        private sealed class u003cu003ec__DisplayClassf
        {
            public Layer layer;

            public u003cu003ec__DisplayClassf()
            {
            }

            // <BuildXMLOrderByLayers>b__c
            public bool u003cBuildXMLOrderByLayersu003eb__c(Variable x)
            {
                return x.Name == this.layer.Name;
            }
        }

        private class ExportToXLS
        {
            private HSSFWorkbook workbook;

            private ISheet formattedSheet;

            private ISheet rawSheet;

            private ISheet formattedSheetShort;

            private ISheet rawSheetShort;

            private ISheet formattedSheetConsolidated;

            private ISheet rawSheetConsolidated;

            private ISheet sheet;

            private ISheet estimatingSheet;

            private ICellStyle integerCellStyle;

            private ICellStyle decimalCellStyle;

            private ICellStyle resultCellStyle;

            private ICellStyle planNameCellStyle;

            private ICellStyle layerNameCellStyle;

            private ICellStyle objectNameCellStyle;

            private ICellStyle extensionNameCellStyle;

            private ICellStyle columnHeaderCellStyle;

            private ICellStyle commentCellStyle;

            private ICellStyle defaultCellStyle;

            private ICellStyle estimatingItemCellStyle;

            private ICellStyle estimatingItemResultCellStyle;

            private ICellStyle estimatingItemPriceCellStyle;

            private ICellStyle estimatingEditableQuantityCellStyle;

            private ICellStyle estimatingEditablePriceCellStyle;

            private ICellStyle estimatingItemTotalCellStyle;

            private ICellStyle estimatingItemMarginCellStyle;

            private ICellStyle estimatingSubTotalCaptionCellStyle;

            private Report.ReportOrderEnum order;

            private Report.ReportSortByEnum sortBy;

            private bool exportEstimating;

            private IRow parserObjectRow;

            private IRow parserResultRowHeader;

            private IRow parserResultRowData;

            private int parserExtensionCount;

            private int parserResultColumnIndex;

            private int parserEstimatingResultCount;

            private int parserEstimatingRowStartIndex;

            private int parserEstimatingRowEndIndex;

            private string parserPlanName = string.Empty;

            private string parserLayerName = string.Empty;

            private string parserObjectName = string.Empty;

            private string parserObjectType = string.Empty;

            private bool parserObjectTypeHasChanged;

            private int parserObjectTypeCount;

            private string parserExtensionName = string.Empty;

            private Preset parserPreset;

            private Presets parserPresets = new Presets();

            private string reportTotalCaption = string.Empty;

            private List<ReportSummary> reportTotalSummaries = new List<ReportSummary>();

            private List<ReportSummary> reportTotalTotalSummaries = new List<ReportSummary>();

            public ExportToXLS(Report.ReportOrderEnum order, Report.ReportSortByEnum sortBy, bool exportEstimating)
            {
                this.order = order;
                this.sortBy = sortBy;
                this.exportEstimating = exportEstimating;
            }

            private IRow AppendRow(short rowHeight = -1)
            {
                IRow row = this.sheet.CreateRow((this.sheet.PhysicalNumberOfRows == 0 ? 0 : this.sheet.LastRowNum + 1));
                if (rowHeight != -1)
                {
                    row.HeightInPoints = (float)rowHeight;
                }
                return row;
            }

            private void CreateWorkbook()
            {
                this.workbook = new HSSFWorkbook();
                string currencySymbol = NumberFormatInfo.CurrentInfo.CurrencySymbol;
                currencySymbol = string.Concat(currencySymbol, "#,##0.00_);(", currencySymbol, "#,##0.00)");
                DocumentSummaryInformation documentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation();
                this.workbook.DocumentSummaryInformation = documentSummaryInformation;
                SummaryInformation summaryInformation = PropertySetFactory.CreateSummaryInformation();
                this.workbook.SummaryInformation = summaryInformation;
                IDataFormat dataFormat = this.workbook.CreateDataFormat();
                this.integerCellStyle = this.workbook.CreateCellStyle();
                this.integerCellStyle.Alignment = HorizontalAlignment.RIGHT;
                this.integerCellStyle.WrapText = false;
                this.integerCellStyle.DataFormat = dataFormat.GetFormat("0");
                this.integerCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
                IDataFormat dataFormat1 = this.workbook.CreateDataFormat();
                this.decimalCellStyle = this.workbook.CreateCellStyle();
                this.decimalCellStyle.Alignment = HorizontalAlignment.RIGHT;
                this.decimalCellStyle.WrapText = false;
                this.decimalCellStyle.DataFormat = dataFormat1.GetFormat("0.00");
                this.decimalCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
                this.resultCellStyle = this.workbook.CreateCellStyle();
                this.resultCellStyle.Alignment = HorizontalAlignment.RIGHT;
                this.resultCellStyle.WrapText = false;
                this.resultCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
                this.planNameCellStyle = this.workbook.CreateCellStyle();
                this.planNameCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.planNameCellStyle.WrapText = false;
                this.planNameCellStyle.SetFont(this.GetFont(0x2bc, 0, 13, "Calibri", false, false, 0, 0));
                this.layerNameCellStyle = this.workbook.CreateCellStyle();
                this.layerNameCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.layerNameCellStyle.WrapText = false;
                this.layerNameCellStyle.SetFont(this.GetFont(0x2bc, 0, 12, "Calibri", false, false, 0, 0));
                this.objectNameCellStyle = this.workbook.CreateCellStyle();
                this.objectNameCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.objectNameCellStyle.WrapText = false;
                this.objectNameCellStyle.SetFont(this.GetFont(0x2bc, 0, 11, "Calibri", false, false, 0, 0));
                this.extensionNameCellStyle = this.workbook.CreateCellStyle();
                this.extensionNameCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.extensionNameCellStyle.WrapText = false;
                this.extensionNameCellStyle.SetFont(this.GetFont(0x2bc, 0, 11, "Calibri", true, false, 0, 1));
                this.columnHeaderCellStyle = this.workbook.CreateCellStyle();
                this.columnHeaderCellStyle.Alignment = HorizontalAlignment.RIGHT;
                this.columnHeaderCellStyle.WrapText = true;
                this.columnHeaderCellStyle.SetFont(this.GetFont(0x2bc, 0, 11, "Calibri", true, false, 0, 0));
                this.commentCellStyle = this.workbook.CreateCellStyle();
                this.commentCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.commentCellStyle.WrapText = false;
                this.commentCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
                this.defaultCellStyle = this.workbook.CreateCellStyle();
                this.defaultCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.defaultCellStyle.WrapText = false;
                this.defaultCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
                this.estimatingItemCellStyle = this.workbook.CreateCellStyle();
                this.estimatingItemCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.estimatingItemCellStyle.WrapText = false;
                this.estimatingItemCellStyle.SetFont(this.GetFont(0x2bc, 0, 10, "Calibri", false, false, 0, 0));
                this.estimatingItemResultCellStyle = this.workbook.CreateCellStyle();
                this.estimatingItemResultCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.estimatingItemResultCellStyle.WrapText = false;
                this.estimatingItemResultCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
                this.estimatingItemPriceCellStyle = this.workbook.CreateCellStyle();
                this.estimatingItemPriceCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.estimatingItemPriceCellStyle.WrapText = false;
                this.estimatingItemPriceCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
                this.estimatingItemPriceCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat(currencySymbol);
                this.estimatingEditableQuantityCellStyle = this.workbook.CreateCellStyle();
                this.estimatingEditableQuantityCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.estimatingEditableQuantityCellStyle.WrapText = false;
                this.estimatingEditableQuantityCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
                this.estimatingEditableQuantityCellStyle.FillForegroundColor = HSSFColor.YELLOW.index;
                this.estimatingEditableQuantityCellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                this.estimatingEditableQuantityCellStyle.IsLocked = false;
                this.estimatingEditablePriceCellStyle = this.workbook.CreateCellStyle();
                this.estimatingEditablePriceCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.estimatingEditablePriceCellStyle.WrapText = false;
                this.estimatingEditablePriceCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
                this.estimatingEditablePriceCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat(currencySymbol);
                this.estimatingEditablePriceCellStyle.FillForegroundColor = HSSFColor.YELLOW.index;
                this.estimatingEditablePriceCellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                this.estimatingEditablePriceCellStyle.IsLocked = false;
                this.estimatingItemTotalCellStyle = this.workbook.CreateCellStyle();
                this.estimatingItemTotalCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.estimatingItemTotalCellStyle.WrapText = false;
                this.estimatingItemTotalCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
                this.estimatingItemTotalCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat(currencySymbol);
                this.estimatingItemMarginCellStyle = this.workbook.CreateCellStyle();
                this.estimatingItemMarginCellStyle.Alignment = HorizontalAlignment.LEFT;
                this.estimatingItemMarginCellStyle.WrapText = false;
                this.estimatingItemMarginCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
                this.estimatingItemMarginCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat("0.00%");
                this.estimatingSubTotalCaptionCellStyle = this.workbook.CreateCellStyle();
                this.estimatingSubTotalCaptionCellStyle.Alignment = HorizontalAlignment.RIGHT;
                this.estimatingSubTotalCaptionCellStyle.WrapText = false;
                this.estimatingSubTotalCaptionCellStyle.SetFont(this.GetFont(0x2bc, 0, 10, "Calibri", false, false, 0, 0));
                if (this.exportEstimating)
                {
                    this.estimatingSheet = this.workbook.CreateSheet(Resources.Rapport_d_estimation);
                    this.estimatingSheet.DefaultColumnWidth = 14;
                    this.estimatingSheet.SetColumnWidth(0, 0x19b9);
                    this.estimatingSheet.SetColumnWidth(2, 0x8b9);
                    return;
                }
                if (Settings.Default.ExportToExcelType != 1)
                {
                    this.rawSheetConsolidated = this.workbook.CreateSheet(Resources.Données_brutes_consolidées);
                    this.rawSheetConsolidated.DefaultColumnWidth = 14;
                    this.rawSheetConsolidated.SetColumnWidth(0, 0x14b9);
                }
                if (Settings.Default.ExportToExcelType != 0)
                {
                    this.formattedSheetConsolidated = this.workbook.CreateSheet(Resources.Données_formatées_consolidées);
                    this.formattedSheetConsolidated.DefaultColumnWidth = 15;
                    this.formattedSheetConsolidated.SetColumnWidth(0, 0x14b9);
                }
                if (Settings.Default.ExportToExcelType != 1)
                {
                    this.rawSheet = this.workbook.CreateSheet(Resources.Données_brutes);
                    this.rawSheet.DefaultColumnWidth = 14;
                }
                if (Settings.Default.ExportToExcelType != 0)
                {
                    this.formattedSheet = this.workbook.CreateSheet(Resources.Données_formatées);
                    this.formattedSheet.DefaultColumnWidth = 15;
                }
                if (Settings.Default.ExportToExcelType != 1)
                {
                    this.rawSheetShort = this.workbook.CreateSheet(Resources.Données_brutes_résumé);
                    this.rawSheetShort.DefaultColumnWidth = 14;
                    this.rawSheetShort.SetColumnWidth(0, 0x19b9);
                    this.rawSheetShort.SetColumnWidth(2, 0x5b9);
                }
                if (Settings.Default.ExportToExcelType != 0)
                {
                    this.formattedSheetShort = this.workbook.CreateSheet(Resources.Données_formatées_résumé);
                    this.formattedSheetShort.DefaultColumnWidth = 15;
                    this.formattedSheetShort.SetColumnWidth(0, 0x19b9);
                }
            }

            public bool Export(string xml, string fileName)
            {
                bool flag;
                try
                {
                    this.CreateWorkbook();
                    this.GenerateXLSFromXML(xml);
                    this.SaveWorkbook(fileName);
                    flag = true;
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    flag = false;
                }
                return flag;
            }

            private void GenerateSheet(ISheet sheetToGenerate, XmlNode root)
            {
                this.parserObjectRow = null;
                this.parserResultRowHeader = null;
                this.parserResultRowData = null;
                this.parserExtensionCount = 0;
                this.parserResultColumnIndex = 0;
                this.parserPlanName = string.Empty;
                this.parserLayerName = string.Empty;
                this.parserObjectName = string.Empty;
                this.parserObjectType = string.Empty;
                this.parserObjectTypeHasChanged = false;
                this.parserObjectTypeCount = 0;
                this.parserExtensionName = string.Empty;
                this.parserPreset = null;
                this.parserPresets.Clear();
                this.reportTotalSummaries.Clear();
                this.reportTotalTotalSummaries.Clear();
                this.sheet = sheetToGenerate;
                this.ParseTree(root);
                if (this.sheet == this.estimatingSheet)
                {
                    if (this.order == Report.ReportOrderEnum.ReportOrderByPlans)
                    {
                        this.OutputReportSubTotal((this.sortBy == Report.ReportSortByEnum.ReportSortByPlans ? this.parserPlanName : this.parserLayerName));
                    }
                    this.OutputReportTotals();
                }
                else
                {
                    this.OutputConsolidatedExtensions();
                }
                this.InsertSignature();
            }

            private bool GenerateXLSFromXML(string xml)
            {
                bool flag;
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xml);
                    if (!this.exportEstimating)
                    {
                        if (Settings.Default.ExportToExcelType != 1)
                        {
                            this.GenerateSheet(this.rawSheet, xmlDocument.DocumentElement);
                        }
                        if (Settings.Default.ExportToExcelType != 0)
                        {
                            this.GenerateSheet(this.formattedSheet, xmlDocument.DocumentElement);
                        }
                        if (Settings.Default.ExportToExcelType != 1)
                        {
                            this.GenerateSheet(this.rawSheetShort, xmlDocument.DocumentElement);
                        }
                        if (Settings.Default.ExportToExcelType != 0)
                        {
                            this.GenerateSheet(this.formattedSheetShort, xmlDocument.DocumentElement);
                        }
                        if (Settings.Default.ExportToExcelType != 1)
                        {
                            this.GenerateSheet(this.rawSheetConsolidated, xmlDocument.DocumentElement);
                        }
                        if (Settings.Default.ExportToExcelType != 0)
                        {
                            this.GenerateSheet(this.formattedSheetConsolidated, xmlDocument.DocumentElement);
                        }
                    }
                    else
                    {
                        this.GenerateSheet(this.estimatingSheet, xmlDocument.DocumentElement);
                    }
                    flag = true;
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    flag = false;
                }
                return flag;
            }

            private double GetDoubleAttribute(XmlNode node, string attributeName)
            {
                double num;
                try
                {
                    string value = node.Attributes.GetNamedItem(attributeName).Value;
                    string str = Utilities.NumberDecimalSeparator();
                    value = value.Replace(",", str).Replace(".", str);
                    num = (double)((double)decimal.Parse(value));
                }
                catch (Exception exception)
                {
                    num = 0;
                }
                return num;
            }

            private IFont GetFont(short boldWeight, short color, short fontHeightInPoints, string name, bool italic, bool strikeout, short typeOffset, byte underline)
            {
                short num = (short)(fontHeightInPoints * 20);
                IFont font = this.workbook.FindFont(boldWeight, color, num, name, italic, strikeout, typeOffset, underline);
                if (font != null)
                {
                    return font;
                }
                font = this.workbook.CreateFont();
                font.Boldweight = boldWeight;
                font.Color = color;
                font.FontHeight = num;
                font.FontName = name;
                font.IsItalic = italic;
                font.IsStrikeout = strikeout;
                font.TypeOffset = typeOffset;
                font.Underline = underline;
                return font;
            }

            private string GetStringAttribute(XmlNode node, string attributeName)
            {
                string value;
                try
                {
                    value = node.Attributes.GetNamedItem(attributeName).Value;
                }
                catch
                {
                    value = "";
                }
                return value;
            }

            private ICell InsertCell(object value, IRow row, int cellIndex, CellType cellType, ICellStyle cellStyle)
            {
                ICell cell = row.CreateCell(cellIndex, cellType);
                cell.CellStyle = cellStyle;
                if (cellType != CellType.NUMERIC)
                {
                    cell.SetCellValue(value.ToString());
                }
                else
                {
                    cell.SetCellValue(Utilities.ConvertToDouble(value, -1));
                }
                return cell;
            }

            private void InsertSignature()
            {
                IRow row = null;
                row = this.AppendRow(-1);
                row = this.AppendRow(-1);
                row = this.AppendRow(-1);
                row = this.AppendRow(-1);
                this.InsertCell(Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan, row, 0, CellType.STRING, this.defaultCellStyle);
            }

            private void OutputConsolidatedExtensions()
            {
                IRow row = null;
                string empty = string.Empty;
                if (this.parserPresets.Count > 0)
                {
                    this.parserPresets.Collection.Sort(new ReportModule.ExportToXLS.PresetSorter());
                    foreach (Preset collection in this.parserPresets.Collection)
                    {
                        if (empty != string.Concat(collection.ExtensionName, collection.Tag))
                        {
                            row = this.AppendRow(26);
                            this.InsertCell(Utilities.GetFields(collection.ExtensionName, ';').GetValue(0).ToString().Trim(), row, 0, CellType.STRING, this.extensionNameCellStyle);
                        }
                        if (empty != string.Concat(collection.ExtensionName, collection.Tag))
                        {
                            this.parserResultRowHeader = this.AppendRow(-1);
                        }
                        this.parserResultRowData = this.AppendRow(16);
                        this.InsertCell(collection.CategoryName, this.parserResultRowData, 0, CellType.STRING, this.objectNameCellStyle);
                        this.parserResultColumnIndex = 1;
                        foreach (PresetResult presetResult in collection.Results.Collection)
                        {
                            if (empty != string.Concat(collection.ExtensionName, collection.Tag))
                            {
                                this.InsertCell(presetResult.Caption, this.parserResultRowHeader, this.parserResultColumnIndex, CellType.STRING, this.columnHeaderCellStyle);
                            }
                            bool flag = this.sheet.Equals(this.formattedSheetConsolidated);
                            string unit = presetResult.Unit;
                            if (flag)
                            {
                                this.InsertCell(unit, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
                            }
                            else if (Utilities.IsNumber(unit.ToString()))
                            {
                                this.InsertCell(unit, this.parserResultRowData, this.parserResultColumnIndex, CellType.NUMERIC, (Utilities.IsInteger(unit) ? this.integerCellStyle : this.decimalCellStyle));
                            }
                            else
                            {
                                this.InsertCell(unit, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
                            }
                            this.parserResultColumnIndex++;
                        }
                        empty = string.Concat(collection.ExtensionName, collection.Tag);
                    }
                    this.parserPresets.Clear();
                }
            }

            private void OutputReportSubTotal(string caption)
            {
                if (this.reportTotalSummaries.Count == 0)
                {
                    return;
                }
                IRow row = this.AppendRow(10);
                row = this.AppendRow(14);
                string str = "";
                string str1 = "";
                foreach (ReportSummary reportTotalSummary in this.reportTotalSummaries)
                {
                    str = string.Concat(str, (str == "" ? "" : " + "), string.Format("E{0}", reportTotalSummary.CostSubTotal + 1));
                    str1 = string.Concat(str1, (str1 == "" ? "" : " + "), string.Format("G{0}", reportTotalSummary.CostSubTotal + 1));
                }
                this.InsertCell(Resources.Sous_totaux_, row, 3, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
                ICell cell = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                cell.SetCellFormula(str);
                ICell cell1 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                cell1.SetCellFormula(str1);
                ICell cell2 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
                cell2.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell.RowIndex + 1));
                this.reportTotalTotalSummaries.Add(new ReportSummary(caption, (double)row.RowNum, 0, null, null, -1, ""));
            }

            private void OutputReportTotals()
            {
                if (this.reportTotalTotalSummaries.Count == 0)
                {
                    return;
                }
                IRow row = this.AppendRow(20);
                row = this.AppendRow(28);
                this.InsertCell(Resources.SOMMAIRE_DES_TOTAUX, row, 0, CellType.STRING, this.planNameCellStyle);
                row = this.AppendRow(14);
                this.InsertCell("", row, 0, CellType.STRING, this.estimatingItemCellStyle);
                this.InsertCell(Resources.Coûtant_total, row, 4, CellType.STRING, this.estimatingItemCellStyle);
                this.InsertCell(Resources.Prix_total, row, 6, CellType.STRING, this.estimatingItemCellStyle);
                this.InsertCell(Resources.Marge, row, 7, CellType.STRING, this.estimatingItemCellStyle);
                this.parserEstimatingRowStartIndex = row.RowNum + 1;
                this.parserEstimatingRowEndIndex = this.parserEstimatingRowStartIndex;
                this.parserEstimatingRowStartIndex++;
                foreach (ReportSummary reportTotalTotalSummary in this.reportTotalTotalSummaries)
                {
                    row = this.AppendRow(14);
                    this.InsertCell(reportTotalTotalSummary.Caption, row, 0, CellType.STRING, this.estimatingItemCellStyle);
                    ICell cell = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                    cell.SetCellFormula(string.Format("E{0}", reportTotalTotalSummary.CostSubTotal + 1));
                    ICell cell1 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                    cell1.SetCellFormula(string.Format("G{0}", reportTotalTotalSummary.CostSubTotal + 1));
                    ICell cell2 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
                    cell2.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell.RowIndex + 1));
                    this.parserEstimatingRowEndIndex++;
                }
                row = this.AppendRow(14);
                ICell cell3 = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                cell3.SetCellFormula(string.Format("SUM(E{0}:E{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
                ICell cell4 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                cell4.SetCellFormula(string.Format("SUM(G{0}:G{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
                ICell cell5 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
                cell5.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell3.RowIndex + 1));
                bool taxOnTax = Settings.Default.TaxOnTax;
                string tax1Label = Settings.Default.Tax1Label;
                string tax2Label = Settings.Default.Tax2Label;
                double tax1Rate = Settings.Default.Tax1Rate;
                double tax2Rate = Settings.Default.Tax2Rate;
                string str = string.Concat(tax1Label, " (", string.Format("{0:0.#####%}", tax1Rate), ") :");
                string str1 = string.Concat(tax2Label, " (", string.Format("{0:0.#####%}", tax2Rate), ") :");
                row = this.AppendRow(20);
                row = this.AppendRow(14);
                this.InsertCell(str, row, 5, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
                ICell cell6 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                cell6.SetCellFormula(string.Format(string.Concat("G{0}*", tax1Rate), cell3.RowIndex + 1));
                row = this.AppendRow(14);
                this.InsertCell(str1, row, 5, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
                ICell cell7 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                if (!taxOnTax)
                {
                    cell7.SetCellFormula(string.Format(string.Concat("G{0}*", tax2Rate), cell3.RowIndex + 1));
                }
                else
                {
                    cell7.SetCellFormula(string.Format(string.Concat("(G{0}+G{1})*", tax2Rate), cell3.RowIndex + 1, cell6.RowIndex + 1));
                }
                row = this.AppendRow(10);
                row = this.AppendRow(14);
                this.InsertCell(Resources.GRAND_TOTAL_, row, 5, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
                ICell cell8 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                cell8.SetCellFormula(string.Format("G{0}+G{1}+G{2}", cell3.RowIndex + 1, cell6.RowIndex + 1, cell7.RowIndex + 1));
            }

            private void ParseNode(XmlNode node)
            {
                string str;
                string str1;
                string str2;
                string str3;
                IRow row = null;
                if (this.sheet.Equals(this.estimatingSheet))
                {
                    string upper = node.Name.ToUpper();
                    string str4 = upper;
                    if (upper != null)
                    {
                        if (str4 == "PLAN")
                        {
                            if (node.HasChildNodes && node.FirstChild.HasChildNodes)
                            {
                                if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByPlans)
                                {
                                    this.OutputReportSubTotal(this.parserPlanName);
                                    this.reportTotalSummaries.Clear();
                                    if (this.parserPlanName != string.Empty)
                                    {
                                        row = this.AppendRow(10);
                                    }
                                }
                                this.parserObjectType = string.Empty;
                                this.parserObjectTypeCount = 0;
                                this.parserPlanName = this.GetStringAttribute(node, "Name");
                                if (this.parserPlanName != string.Empty)
                                {
                                    row = this.AppendRow(28);
                                    this.InsertCell(this.parserPlanName, row, 0, CellType.STRING, this.planNameCellStyle);
                                    if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByPlans)
                                    {
                                        this.reportTotalCaption = this.parserPlanName;
                                        return;
                                    }
                                }
                            }
                        }
                        else if (str4 == "LAYER")
                        {
                            if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByLayers)
                            {
                                this.OutputReportSubTotal(this.parserLayerName);
                                this.reportTotalSummaries.Clear();
                                if (this.parserLayerName != string.Empty)
                                {
                                    row = this.AppendRow(10);
                                }
                            }
                            this.parserObjectType = string.Empty;
                            this.parserObjectTypeCount = 0;
                            this.parserLayerName = this.GetStringAttribute(node, "Name");
                            row = this.AppendRow(28);
                            this.InsertCell(this.parserLayerName, row, 0, CellType.STRING, this.planNameCellStyle);
                            if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByLayers)
                            {
                                this.reportTotalCaption = this.parserLayerName;
                                return;
                            }
                        }
                        else if (str4 == "OBJECT")
                        {
                            this.parserExtensionCount = 0;
                            this.parserEstimatingResultCount = 0;
                            this.parserObjectName = this.GetStringAttribute(node, "Name");
                            string stringAttribute = this.GetStringAttribute(node, "Type");
                            this.parserObjectTypeHasChanged = stringAttribute != this.parserObjectType;
                            this.parserObjectType = stringAttribute;
                            this.parserObjectRow = this.AppendRow(26);
                            this.InsertCell(this.parserObjectName, this.parserObjectRow, 0, CellType.STRING, this.objectNameCellStyle);
                            if (this.order == Report.ReportOrderEnum.ReportOrderByObjects)
                            {
                                this.reportTotalCaption = this.parserObjectName;
                                return;
                            }
                        }
                        else
                        {
                            if (str4 != "COMMENT")
                            {
                                if (str4 != "RESULT")
                                {
                                    if (str4 != "SUMMARY")
                                    {
                                        return;
                                    }
                                    if (this.parserEstimatingRowStartIndex > 0)
                                    {
                                        row = this.AppendRow(14);
                                        ICell cell = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                                        cell.SetCellFormula(string.Format("SUM(E{0}:E{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
                                        ICell cell1 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                                        cell1.SetCellFormula(string.Format("SUM(G{0}:G{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
                                        ICell cell2 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
                                        cell2.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell.RowIndex + 1));
                                        if (this.order != Report.ReportOrderEnum.ReportOrderByObjects)
                                        {
                                            this.reportTotalSummaries.Add(new ReportSummary("", (double)row.RowNum, 0, null, null, -1, ""));
                                        }
                                        else
                                        {
                                            this.reportTotalTotalSummaries.Add(new ReportSummary(this.reportTotalCaption, (double)row.RowNum, 0, null, null, -1, ""));
                                        }
                                        this.parserEstimatingRowStartIndex = 0;
                                    }
                                    this.parserExtensionCount++;
                                    return;
                                }
                                if (this.parserEstimatingResultCount == 0)
                                {
                                    row = this.AppendRow(14);
                                    this.parserEstimatingRowStartIndex = row.RowNum + 1;
                                    this.parserEstimatingRowEndIndex = this.parserEstimatingRowStartIndex;
                                    this.InsertCell("", row, 0, CellType.STRING, this.estimatingItemCellStyle);
                                    this.InsertCell(Resources.Quantité, row, 1, CellType.STRING, this.estimatingItemCellStyle);
                                    this.InsertCell(Resources.Unité, row, 2, CellType.STRING, this.estimatingItemCellStyle);
                                    this.InsertCell(Resources.Coûtant, row, 3, CellType.STRING, this.estimatingItemCellStyle);
                                    this.InsertCell(Resources.Coûtant_total, row, 4, CellType.STRING, this.estimatingItemCellStyle);
                                    this.InsertCell(Resources.Prix, row, 5, CellType.STRING, this.estimatingItemCellStyle);
                                    this.InsertCell(Resources.Prix_total, row, 6, CellType.STRING, this.estimatingItemCellStyle);
                                    this.InsertCell(Resources.Marge, row, 7, CellType.STRING, this.estimatingItemCellStyle);
                                }
                                string stringAttribute1 = this.GetStringAttribute(node, "EstimatingCaption");
                                if (stringAttribute1 != string.Empty)
                                {
                                    row = this.AppendRow(14);
                                    this.InsertCell(stringAttribute1, row, 0, CellType.STRING, this.estimatingItemCellStyle);
                                    ICell cell3 = this.InsertCell(this.GetStringAttribute(node, "Quantity"), row, 1, CellType.NUMERIC, this.estimatingEditableQuantityCellStyle);
                                    this.InsertCell(this.GetStringAttribute(node, "EstimatingUnit"), row, 2, CellType.STRING, this.estimatingItemResultCellStyle);
                                    this.InsertCell(this.GetDoubleAttribute(node, "RawCostEach"), row, 3, CellType.NUMERIC, this.estimatingEditablePriceCellStyle);
                                    ICell cell4 = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                                    cell4.SetCellFormula(string.Format("B{0}*D{0}", cell3.RowIndex + 1));
                                    ICell cell5 = this.InsertCell(this.GetDoubleAttribute(node, "RawPriceEach"), row, 5, CellType.NUMERIC, this.estimatingEditablePriceCellStyle);
                                    ICell cell6 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
                                    cell6.SetCellFormula(string.Format("B{0}*F{0}", cell3.RowIndex + 1));
                                    ICell cell7 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
                                    cell7.SetCellFormula(string.Format("(F{0}-D{0})/D{0}", cell5.RowIndex + 1));
                                    this.parserEstimatingRowEndIndex++;
                                }
                                this.parserEstimatingResultCount++;
                                return;
                            }
                            if (node.ParentNode.Name.ToUpper() != "PROJECT")
                            {
                                string str5 = this.GetStringAttribute(node, "Value").Trim().Replace("`", "\r\n");
                                if (str5 != string.Empty)
                                {
                                    row = this.AppendRow(-1);
                                    this.InsertCell(str5, row, 0, CellType.STRING, this.commentCellStyle);
                                    return;
                                }
                            }
                        }
                    }
                }
                else if ((this.sheet.Equals(this.formattedSheetShort) ? true : this.sheet.Equals(this.rawSheetShort)))
                {
                    string upper1 = node.Name.ToUpper();
                    string str6 = upper1;
                    if (upper1 != null)
                    {
                        if (str6 != "PLAN")
                        {
                            if (str6 == "LAYER")
                            {
                                if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByLayers && this.parserLayerName != "")
                                {
                                    row = this.AppendRow(10);
                                }
                                this.parserObjectType = string.Empty;
                                this.parserObjectTypeCount = 0;
                                this.parserLayerName = this.GetStringAttribute(node, "Name");
                                row = this.AppendRow(28);
                                this.InsertCell(this.parserLayerName, row, 0, CellType.STRING, this.planNameCellStyle);
                                return;
                            }
                            if (str6 == "OBJECT")
                            {
                                this.parserExtensionCount = 0;
                                this.parserObjectName = this.GetStringAttribute(node, "Name");
                                this.parserObjectType = this.GetStringAttribute(node, "Type");
                                this.parserObjectRow = this.AppendRow(-1);
                                this.InsertCell(this.parserObjectName, this.parserObjectRow, 0, CellType.STRING, this.objectNameCellStyle);
                                return;
                            }
                            if (str6 == "EXTENSION")
                            {
                                this.parserExtensionCount++;
                                return;
                            }
                            if (str6 != "RESULT")
                            {
                                return;
                            }
                            if (this.parserExtensionCount == 1)
                            {
                                bool flag = false;
                                string stringAttribute2 = this.GetStringAttribute(node, "Name");
                                string upper2 = this.parserObjectType.ToUpper();
                                string str7 = upper2;
                                if (upper2 != null)
                                {
                                    if (str7 == "AREA")
                                    {
                                        flag = stringAttribute2 == "AreaMinusSubstractions";
                                    }
                                    else if (str7 == "PERIMETER")
                                    {
                                        flag = stringAttribute2 == "PerimeterMinusOpenings";
                                    }
                                    else if (str7 == "LINE")
                                    {
                                        flag = stringAttribute2 == "Length";
                                    }
                                    else if (str7 == "COUNTER")
                                    {
                                        flag = stringAttribute2 == "Count";
                                    }
                                }
                                if (flag)
                                {
                                    bool flag1 = this.sheet.Equals(this.formattedSheetShort);
                                    if (node.Name.ToUpper() == "FIELD")
                                    {
                                        str = "Value";
                                    }
                                    else
                                    {
                                        str = (this.order == Report.ReportOrderEnum.ReportOrderByObjects ? "TotalValue" : "Value");
                                    }
                                    string str8 = str;
                                    if (flag1)
                                    {
                                        str1 = str8;
                                    }
                                    else
                                    {
                                        str1 = (str8 == "Value" ? "RawValue" : "TotalRawValue");
                                    }
                                    str8 = str1;
                                    string stringAttribute3 = this.GetStringAttribute(node, str8);
                                    if (flag1)
                                    {
                                        this.InsertCell(stringAttribute3, this.parserObjectRow, 1, CellType.STRING, this.resultCellStyle);
                                    }
                                    else
                                    {
                                        this.InsertCell(stringAttribute3, this.parserObjectRow, 1, CellType.NUMERIC, (Utilities.IsInteger(stringAttribute3.ToString()) ? this.integerCellStyle : this.decimalCellStyle));
                                    }
                                    if (!flag1)
                                    {
                                        string stringAttribute4 = this.GetStringAttribute(node, "Unit");
                                        stringAttribute4 = (stringAttribute4 == Resources.unité_ ? "" : stringAttribute4);
                                        this.InsertCell(stringAttribute4, this.parserObjectRow, 2, CellType.STRING, this.columnHeaderCellStyle);
                                    }
                                }
                            }
                        }
                        else if (node.HasChildNodes && node.FirstChild.HasChildNodes)
                        {
                            if (this.parserPlanName != "")
                            {
                                row = this.AppendRow(10);
                            }
                            this.parserPlanName = this.GetStringAttribute(node, "Name");
                            if (this.parserPlanName != "")
                            {
                                row = this.AppendRow(28);
                                this.InsertCell(this.parserPlanName, row, 0, CellType.STRING, this.planNameCellStyle);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    bool flag2 = (this.sheet.Equals(this.formattedSheetConsolidated) ? true : this.sheet.Equals(this.rawSheetConsolidated));
                    string upper3 = node.Name.ToUpper();
                    string str9 = upper3;
                    if (upper3 != null)
                    {
                        switch (str9)
                        {
                            case "PLAN":
                                {
                                    if (flag2 && this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByPlans)
                                    {
                                        this.OutputConsolidatedExtensions();
                                    }
                                    if (!node.HasChildNodes || !node.FirstChild.HasChildNodes)
                                    {
                                        break;
                                    }
                                    if (this.parserPlanName != "")
                                    {
                                        row = this.AppendRow(10);
                                    }
                                    this.parserObjectType = string.Empty;
                                    this.parserObjectTypeCount = 0;
                                    this.parserPlanName = this.GetStringAttribute(node, "Name");
                                    if (this.parserPlanName == "")
                                    {
                                        break;
                                    }
                                    row = this.AppendRow(28);
                                    this.InsertCell(this.parserPlanName, row, 0, CellType.STRING, this.planNameCellStyle);
                                    return;
                                }
                            case "LAYER":
                                {
                                    if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByLayers)
                                    {
                                        this.OutputConsolidatedExtensions();
                                        if (this.parserLayerName != "")
                                        {
                                            row = this.AppendRow(10);
                                        }
                                    }
                                    this.parserObjectType = string.Empty;
                                    this.parserObjectTypeCount = 0;
                                    this.parserLayerName = this.GetStringAttribute(node, "Name");
                                    row = this.AppendRow(28);
                                    this.InsertCell(this.parserLayerName, row, 0, CellType.STRING, this.planNameCellStyle);
                                    return;
                                }
                            case "OBJECTS":
                                {
                                    if (!flag2 || this.order != Report.ReportOrderEnum.ReportOrderByObjects)
                                    {
                                        break;
                                    }
                                    this.OutputConsolidatedExtensions();
                                    return;
                                }
                            case "OBJECT":
                                {
                                    this.parserExtensionCount = 0;
                                    this.parserObjectName = this.GetStringAttribute(node, "Name");
                                    string stringAttribute5 = this.GetStringAttribute(node, "Type");
                                    this.parserObjectTypeHasChanged = stringAttribute5 != this.parserObjectType;
                                    this.parserObjectType = stringAttribute5;
                                    if (flag2)
                                    {
                                        break;
                                    }
                                    this.parserObjectRow = this.AppendRow(26);
                                    this.InsertCell(this.parserObjectName, this.parserObjectRow, 0, CellType.STRING, this.objectNameCellStyle);
                                    return;
                                }
                            case "COMMENT":
                                {
                                    if (flag2 || !(node.ParentNode.Name.ToUpper() != "PROJECT"))
                                    {
                                        break;
                                    }
                                    string str10 = this.GetStringAttribute(node, "Value").Trim().Replace("`", "\r\n");
                                    if (str10 == string.Empty)
                                    {
                                        break;
                                    }
                                    row = this.AppendRow(-1);
                                    this.InsertCell(str10, row, 0, CellType.STRING, this.commentCellStyle);
                                    return;
                                }
                            case "EXTENSION":
                                {
                                    this.parserExtensionCount++;
                                    if (flag2)
                                    {
                                        if (this.parserExtensionCount <= 1)
                                        {
                                            break;
                                        }
                                        int num = 0;
                                        string upper4 = this.parserObjectType.ToUpper();
                                        string str11 = upper4;
                                        if (upper4 != null)
                                        {
                                            if (str11 == "AREA")
                                            {
                                                num = 1;
                                            }
                                            else if (str11 == "PERIMETER")
                                            {
                                                num = 2;
                                            }
                                            else if (str11 == "LINE")
                                            {
                                                num = 3;
                                            }
                                            else if (str11 == "COUNTER")
                                            {
                                                num = 4;
                                            }
                                        }
                                        Guid guid = Guid.NewGuid();
                                        this.parserPreset = new Preset(guid.ToString(), this.GetStringAttribute(node, "Name"), this.parserObjectName, string.Concat(this.GetStringAttribute(node, "Name"), ";", num.ToString()), UnitScale.UnitSystem.undefined);
                                        this.parserPresets.Add(this.parserPreset);
                                        return;
                                    }
                                    else
                                    {
                                        if (this.parserExtensionCount <= 1)
                                        {
                                            break;
                                        }
                                        row = this.AppendRow(20);
                                        this.InsertCell(this.GetStringAttribute(node, "Name"), row, 0, CellType.STRING, this.extensionNameCellStyle);
                                        return;
                                    }
                                }
                            case "FIELDS":
                            case "RESULTS":
                                {
                                    if (!flag2)
                                    {
                                        this.parserResultColumnIndex = 0;
                                        this.parserResultRowHeader = this.AppendRow(-1);
                                        this.parserResultRowData = this.AppendRow((node.Name.ToUpper() == "FIELDS" ? -1 : 16));
                                        return;
                                    }
                                    if (this.parserExtensionCount != 1)
                                    {
                                        break;
                                    }
                                    this.parserResultColumnIndex = 1;
                                    if (this.parserObjectTypeHasChanged)
                                    {
                                        if (this.order == Report.ReportOrderEnum.ReportOrderByObjects)
                                        {
                                            if (this.sheet.PhysicalNumberOfRows > 0)
                                            {
                                                this.AppendRow(-1);
                                            }
                                        }
                                        else if (this.parserObjectTypeCount > 0)
                                        {
                                            this.AppendRow(-1);
                                        }
                                        this.parserObjectTypeCount++;
                                        this.parserResultRowHeader = this.AppendRow(-1);
                                    }
                                    this.parserResultRowData = this.AppendRow((node.Name.ToUpper() == "FIELDS" ? -1 : 16));
                                    this.InsertCell(this.parserObjectName, this.parserResultRowData, 0, CellType.STRING, this.objectNameCellStyle);
                                    return;
                                }
                            case "FIELD":
                            case "RESULT":
                                {
                                    if (!flag2 && this.parserResultColumnIndex == 7)
                                    {
                                        this.parserResultColumnIndex = 0;
                                        this.parserResultRowHeader = this.AppendRow(-1);
                                        this.parserResultRowData = this.AppendRow((node.Name.ToUpper() == "FIELD" ? -1 : 16));
                                    }
                                    bool flag3 = (this.sheet.Equals(this.formattedSheet) ? true : this.sheet.Equals(this.formattedSheetConsolidated));
                                    string stringAttribute6 = this.GetStringAttribute(node, "Caption");
                                    if (node.Name.ToUpper() == "FIELD")
                                    {
                                        str2 = "Value";
                                    }
                                    else
                                    {
                                        str2 = (this.order == Report.ReportOrderEnum.ReportOrderByObjects ? "TotalValue" : "Value");
                                    }
                                    string str12 = str2;
                                    if (flag3)
                                    {
                                        str3 = str12;
                                    }
                                    else
                                    {
                                        str3 = (str12 == "Value" ? "RawValue" : "TotalRawValue");
                                    }
                                    str12 = str3;
                                    if (!flag3)
                                    {
                                        string stringAttribute7 = this.GetStringAttribute(node, "Unit");
                                        stringAttribute7 = (stringAttribute7 == Resources.unité_ ? "" : stringAttribute7);
                                        if (stringAttribute7 != string.Empty)
                                        {
                                            stringAttribute6 = string.Concat(stringAttribute6, " (", stringAttribute7, ")");
                                        }
                                    }
                                    if (!flag2 || this.parserExtensionCount == 1)
                                    {
                                        if (!flag2 || this.parserObjectTypeHasChanged)
                                        {
                                            this.InsertCell(stringAttribute6, this.parserResultRowHeader, this.parserResultColumnIndex, CellType.STRING, this.columnHeaderCellStyle);
                                        }
                                        string stringAttribute8 = this.GetStringAttribute(node, str12);
                                        if (flag3)
                                        {
                                            this.InsertCell(stringAttribute8, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
                                        }
                                        else if (Utilities.IsNumber(stringAttribute8.ToString()))
                                        {
                                            this.InsertCell(stringAttribute8, this.parserResultRowData, this.parserResultColumnIndex, CellType.NUMERIC, (Utilities.IsInteger(stringAttribute8.ToString()) ? this.integerCellStyle : this.decimalCellStyle));
                                        }
                                        else
                                        {
                                            this.InsertCell(stringAttribute8, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
                                        }
                                    }
                                    else if (flag2)
                                    {
                                        PresetResult presetResult = new PresetResult(this.GetStringAttribute(node, "Name"), stringAttribute6, this.GetStringAttribute(node, str12), "", "", ExtensionResult.ExtensionResultTypeEnum.ResultTypeUnit, false, true, -1, -1, -1);
                                        this.parserPreset.Results.Add(presetResult);
                                        if (!flag3)
                                        {
                                            Preset preset = this.parserPreset;
                                            preset.Tag = string.Concat(preset.Tag, stringAttribute6, ";");
                                        }
                                    }
                                    this.parserResultColumnIndex++;
                                    return;
                                }
                            default:
                                {
                                    return;
                                }
                        }
                    }
                }
            }

            private void ParseTree(XmlNode node)
            {
                if (node != null)
                {
                    this.ParseNode(node);
                }
                if (node.HasChildNodes)
                {
                    node = node.FirstChild;
                    while (node != null)
                    {
                        this.ParseTree(node);
                        node = node.NextSibling;
                    }
                }
            }

            private void SaveWorkbook(string fileName)
            {
                while (true)
                {
                    try
                    {
                        using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            this.workbook.SetActiveSheet(0);
                            this.workbook.Write(fileStream);
                            fileStream.Close();
                        }
                        break;
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        if (exception.Message.IndexOf("process") == -1)
                        {
                            Utilities.DisplaySystemError(exception);
                            break;
                        }
                        else if (Utilities.DisplayWarningQuestionRetryCancel(Resources.Fichier_utilisé_par_un_autre_processus, Resources.Veuillez_fermer_votre_tableur_Excel) == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                }
            }

            private class PresetSorter : IComparer
            {
                public PresetSorter()
                {
                }

                public int Compare(object x, object y)
                {
                    int num;
                    try
                    {
                        Preset preset = x as Preset;
                        Preset preset1 = y as Preset;
                        num = StringLogicalComparer.Compare(string.Concat(preset.ExtensionName, (string)preset.Tag, preset.CategoryName), string.Concat(preset1.ExtensionName, (string)preset1.Tag, preset1.CategoryName));
                    }
                    catch (Exception exception)
                    {
                        num = -1;
                    }
                    return num;
                }
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

        private class GroupSummarySorter : IComparer<ReportSummary>
        {
            public GroupSummarySorter()
            {
            }

            public int Compare(ReportSummary x, ReportSummary y)
            {
                int num;
                try
                {
                    DrawObject groupObject = x.GroupObject;
                    DrawObject drawObject = y.GroupObject;
                    if (groupObject.ObjectType == drawObject.ObjectType)
                    {
                        num = StringLogicalComparer.Compare(groupObject.Name, drawObject.Name);
                    }
                    else
                    {
                        string str = groupObject.ObjectSortOrder.ToString();
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

        private class LayerSorter : IComparer<Variable>
        {
            public LayerSorter()
            {
            }

            public int Compare(Variable x, Variable y)
            {
                int num;
                try
                {
                    num = StringLogicalComparer.Compare(x.Name, y.Name);
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    num = -1;
                }
                return num;
            }
        }

        private class SummarySorter : IComparer<ReportSummary>
        {
            public SummarySorter()
            {
            }

            public int Compare(ReportSummary x, ReportSummary y)
            {
                int num;
                try
                {
                    num = StringLogicalComparer.Compare(x.Caption, y.Caption);
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    num = -1;
                }
                return num;
            }
        }

        private class UTF8StringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }

            public UTF8StringWriter()
            {
            }

            public UTF8StringWriter(IFormatProvider formatProvider) : base(formatProvider)
            {
            }

            public UTF8StringWriter(StringBuilder sb) : base(sb)
            {
            }

            public UTF8StringWriter(StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider)
            {
            }
        }
    }
}