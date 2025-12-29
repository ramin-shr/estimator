using DevExpress.Utils;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace QuoterPlan
{
    public class DBManagement
    {
        public TreeViewNodes TreeItems;

        public DBEstimatingItems EstimatingItems;

        public DBEstimatingSections CSICodesA;

        public DBEstimatingSections CSICodesB;

        private CEstimatingItemsBrowserInterface browserInterface;

        private TreeViewNodes eEstimatingSections;

        private CEstimatingItems browserProducts = new CEstimatingItems();

        public CEstimatingItemsBrowserInterface BrowserInterface
        {
            get
            {
                return this.browserInterface;
            }
        }

        public DBManagement()
        {
            this.TreeItems = new TreeViewNodes();
            this.EstimatingItems = new DBEstimatingItems();
            this.CSICodesA = new DBEstimatingSections();
            this.CSICodesB = new DBEstimatingSections();
        }

        public void AddBrowserItem(DBEstimatingItem dbItem)
        {
            this.eEstimatingSections = null;
        }

        public int AddEstimatingItem(DBEstimatingItem estimatingItem)
        {
            int num = this.InsertTreeNode(estimatingItem);
            if (num > -1)
            {
                this.EstimatingItems.Add(estimatingItem);
            }
            return num;
        }

        private void AddSection(string[] fields, DBEstimatingSections sections)
        {
            int intField = this.GetIntField(fields, 0, -1);
            if (intField > -1)
            {
                int num = this.GetIntField(fields, 2, -1);
                string stringField = this.GetStringField(fields, 1, "");
                sections.Add(new DBEstimatingSection(intField, num, stringField));
            }
        }

        private void browserInterface_OnGetDefaultFormula(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
        {
            this.MatchDefaultTakeoffResult(drawObject, stats, product, coUnit);
        }

        private void browserInterface_OnLoadEstimatingItems(CEstimatingSection section, ref CEstimatingItems products)
        {
            Utilities.EnableInterface(this.browserInterface.Browser, false);
            this.browserProducts.Clear();
            foreach (DictionaryEntry collection in this.EstimatingItems.Collection)
            {
                DBEstimatingItem value = (DBEstimatingItem)collection.Value;
                if ((section.ParentID != 0 || value.SectionID != Utilities.ConvertToInt(section.SectionID)) && (section.ParentID <= 0 || value.SubSectionID != Utilities.ConvertToInt(section.SectionID)))
                {
                    continue;
                }
                string str = value.ID.ToString();
                string description = value.Description;
                double priceEach = value.PriceEach;
                string purchaseUnit = value.PurchaseUnit;
                CEstimatingItem cEstimatingItem = new CEstimatingItem(
                    str,
                    description,
                    priceEach,
                    purchaseUnit,
                    value.ItemType,
                    value.UnitMeasure,
                    value.CoverageValue,
                    value.CoverageUnit,
                    value.SectionID,
                    value.SubSectionID,
                    value.BidCode,
                    ""
                );

                cEstimatingItem.Tag = cEstimatingItem;

                this.browserProducts.Add(cEstimatingItem);
            }
            products = this.browserProducts;
            Utilities.EnableInterface(this.browserInterface.Browser, true);
        }

        private void browserInterface_OnLoadEstimatingSections(ref TreeViewNodes sections)
        {
            if (this.eEstimatingSections == null)
            {
                this.eEstimatingSections = new TreeViewNodes();
                foreach (TreeViewNode collection in this.TreeItems.Collection)
                {
                    if (collection.Tag.GetType() != typeof(int))
                    {
                        continue;
                    }
                    string str = "";
                    if (collection.ParentID != 0)
                    {
                        DBEstimatingSection item = this.CSICodesB[Utilities.ConvertToInt(collection.Tag)];
                        if (item == null)
                        {
                            continue;
                        }
                        int num = Utilities.ConvertToInt(collection.Tag);
                        str = string.Concat(num.ToString("D5"), " - ", item.Name);
                        CEstimatingSection cEstimatingSection = new CEstimatingSection(
                            collection.ID,
                            collection.ParentID,
                            collection.Tag.ToString(),
                            str
                        );

                        cEstimatingSection.Tag = cEstimatingSection;

                        this.eEstimatingSections.Add(collection.ID, collection.ParentID, cEstimatingSection);
                    }
                    else
                    {
                        DBEstimatingSection dBEstimatingSection = this.CSICodesA[Utilities.ConvertToInt(collection.Tag)];
                        if (dBEstimatingSection == null)
                        {
                            continue;
                        }
                        int num1 = Utilities.ConvertToInt(collection.Tag);
                        str = string.Concat(num1.ToString("D5"), " - ", dBEstimatingSection.Name);
                        CEstimatingSection cEstimatingSection1 = new CEstimatingSection(
                            collection.ID,
                            0,
                            collection.Tag.ToString(),
                            str
                        );

                        cEstimatingSection1.Tag = cEstimatingSection1;

                        this.eEstimatingSections.Add(collection.ID, 0, cEstimatingSection1);
                    }
                }
            }
            sections = this.eEstimatingSections;
        }

        private void browserInterface_OnSelectDatabase(string databaseName, ref bool cancel)
        {
        }

        private string ConvertUnitToString(string coUnit)
        {
            coUnit = coUnit.ToUpper();
            if (coUnit == Resources.Material_Chacun.ToUpper())
            {
                return "EA";
            }
            if (coUnit == Resources.pi.ToUpper() || coUnit == "M")
            {
                return "LF";
            }
            if (coUnit == Resources.pi_2.ToUpper() || coUnit == "M²")
            {
                return "SF";
            }
            if (coUnit == Resources.pi_3.ToUpper())
            {
                return "CF";
            }
            if (!(coUnit == Resources.v_3.ToUpper()) && !(coUnit == "M³"))
            {
                return coUnit;
            }
            return "CY";
        }

        public void DeleteBrowserItem(DBEstimatingItem dbItem)
        {
            this.eEstimatingSections = null;
        }

        private bool GetBoolField(string[] fields, int index, bool defaultValue)
        {
            bool flag;
            try
            {
                flag = Utilities.ConvertToBoolean(fields[index], defaultValue);
            }
            catch
            {
                flag = defaultValue;
            }
            return flag;
        }

        private double GetDoubleField(string[] fields, int index, double defaultValue)
        {
            double num;
            try
            {
                num = Utilities.ConvertToDouble(fields[index], -1);
            }
            catch
            {
                num = defaultValue;
            }
            return num;
        }

        public DBEstimatingItem GetEstimatingItem(int itemID)
        {
            DBEstimatingItem item;
            try
            {
                item = this.EstimatingItems[itemID];
            }
            catch
            {
                item = null;
            }
            return item;
        }

        private int GetIntField(string[] fields, int index, int defaultValue)
        {
            int num;
            try
            {
                num = Utilities.ConvertToInt(fields[index]);
            }
            catch
            {
                num = defaultValue;
            }
            return num;
        }

        private string GetStringField(string[] fields, int index, string defaultValue)
        {
            string str;
            try
            {
                str = fields[index];
            }
            catch
            {
                str = defaultValue;
            }
            return str;
        }

        public void InitializeBrowserInterface(ImageCollection imageCollection)
        {
            this.browserInterface = new CEstimatingItemsBrowserInterface(imageCollection, false);
            this.browserInterface.OnSelectDatabase += new OnSelectDatabaseHandler(this.browserInterface_OnSelectDatabase);
            this.browserInterface.OnLoadEstimatingSections += new OnLoadEstimatingSectionsHandler(this.browserInterface_OnLoadEstimatingSections);
            this.browserInterface.OnLoadEstimatingItems += new OnLoadEstimatingItemsHandler(this.browserInterface_OnLoadEstimatingItems);
            this.browserInterface.OnGetDefaultFormula += new OnGetDefaultFormulaHandler(this.browserInterface_OnGetDefaultFormula);
        }

        public int InsertTreeNode(DBEstimatingItem estimatingItem)
        {
            int num = -1;
            int num1 = this.ValideTreeNodeParent(estimatingItem);
            if (num1 <= -1)
            {
                Console.WriteLine(estimatingItem.Description);
            }
            else
            {
                num = this.TreeItems.Add(num1, estimatingItem);
            }
            return num;
        }

        public void MatchDefaultTakeoffResult(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
        {
            string str = "";
            if ((product.UnitMeasure == DBEstimatingItem.UnitMeasureType.m || product.UnitMeasure == DBEstimatingItem.UnitMeasureType.lin_ft) && drawObject.ObjectType == "Perimeter")
            {
                str = string.Concat("[", Resources.Longueur_nette, "]");
            }
            if ((product.UnitMeasure == DBEstimatingItem.UnitMeasureType.m || product.UnitMeasure == DBEstimatingItem.UnitMeasureType.lin_ft) && drawObject.ObjectType == "Line")
            {
                str = string.Concat("[", Resources.Longueur, "]");
            }
            else if ((product.UnitMeasure == DBEstimatingItem.UnitMeasureType.sq_m || product.UnitMeasure == DBEstimatingItem.UnitMeasureType.sq_ft) && drawObject.ObjectType == "Area")
            {
                str = string.Concat("[", Resources.Surface_déductions, "]");
            }
            if (str == "")
            {
                coUnit = this.ConvertUnitToString(coUnit.Trim());
                string str1 = coUnit;
                string str2 = str1;
                if (str1 != null && !(str2 == "CF") && !(str2 == "CY"))
                {
                    if (str2 == "EA")
                    {
                        if (drawObject.ObjectType == "Counter")
                        {
                            str = string.Concat("[", Resources.Nombre_d_objets, "]");
                        }
                    }
                    else if (str2 != "LF")
                    {
                        if (str2 == "SF")
                        {
                            if (drawObject.ObjectType == "Area")
                            {
                                str = string.Concat("[", Resources.Surface_déductions, "]");
                            }
                        }
                    }
                    else if (drawObject.ObjectType == "Perimeter")
                    {
                        str = string.Concat("[", Resources.Longueur_nette, "]");
                    }
                    else if (drawObject.ObjectType == "Line")
                    {
                        str = string.Concat("[", Resources.Longueur, "]");
                    }
                }
            }
            if (str != "")
            {
                product.Formula = str;
            }
        }

        private void MatchDefaultTakeoffResultInExtension(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
        {
            IEnumerator enumerator = drawObject.Group.Presets.Collection.GetEnumerator();
            try
            {
                do
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    Preset current = (Preset)enumerator.Current;
                    drawObject.DrawArea.Project.ExtensionsSupport.QueryPresetResults(current, stats, drawObject.DrawArea.ActivePlan.UnitScale);
                    coUnit = coUnit.Trim().ToUpper();
                    string str = coUnit;
                    string str1 = str;
                    if (str == null || str1 == "CF" || str1 == "CY")
                    {
                        continue;
                    }
                }
                while (product.Formula == "");
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        public bool Open(string fileName)
        {
            bool flag;
            DBManagement.ParsingContext parsingContext = DBManagement.ParsingContext.Products;
            try
            {
                foreach (string str in File.ReadLines(fileName))
                {
                    if (!str.StartsWith("##"))
                    {
                        string[] strArrays = str.Split(new char[] { ';' });
                        switch (parsingContext)
                        {
                            case DBManagement.ParsingContext.NextAvailableIndex:
                                {
                                    this.EstimatingItems.NextAvailableIndex = this.GetIntField(strArrays, 0, 0);
                                    continue;
                                }
                            case DBManagement.ParsingContext.Products:
                                {
                                    int intField = this.GetIntField(strArrays, 0, -1);
                                    if (intField <= -1)
                                    {
                                        continue;
                                    }
                                    int num = this.GetIntField(strArrays, 1, 0);
                                    string stringField = this.GetStringField(strArrays, 2, "");
                                    string stringField1 = this.GetStringField(strArrays, 3, "");
                                    if (stringField1 == "fle")
                                    {
                                        stringField1 = "sh";
                                    }
                                    int intField1 = this.GetIntField(strArrays, 4, 0);
                                    double doubleField = this.GetDoubleField(strArrays, 5, 0);
                                    double doubleField1 = this.GetDoubleField(strArrays, 6, 1);
                                    double num1 = this.GetDoubleField(strArrays, 7, 0);
                                    int intField2 = this.GetIntField(strArrays, 8, -1);
                                    int num2 = this.GetIntField(strArrays, 9, -1);
                                    string str1 = this.GetStringField(strArrays, 10, "");
                                    bool boolField = this.GetBoolField(strArrays, 11, false);
                                    DBEstimatingItem dBEstimatingItem = new DBEstimatingItem(intField, (DBEstimatingItem.EstimatingItemType)num, stringField, stringField1, (DBEstimatingItem.UnitMeasureType)intField1, doubleField, doubleField1, num1, intField2, num2, str1, boolField);
                                    this.AddEstimatingItem(dBEstimatingItem);
                                    continue;
                                }
                            case DBManagement.ParsingContext.CSICodesA:
                                {
                                    this.AddSection(strArrays, this.CSICodesA);
                                    continue;
                                }
                            case DBManagement.ParsingContext.CSICodesB:
                                {
                                    this.AddSection(strArrays, this.CSICodesB);
                                    continue;
                                }
                            default:
                                {
                                    continue;
                                }
                        }
                    }
                    else
                    {
                        string str2 = str;
                        string str3 = str2;
                        if (str2 == null)
                        {
                            continue;
                        }
                        if (str3 == "##NextAvailableIndex")
                        {
                            parsingContext = DBManagement.ParsingContext.NextAvailableIndex;
                        }
                        else if (str3 == "##EstimatingItems")
                        {
                            parsingContext = DBManagement.ParsingContext.Products;
                        }
                        else if (str3 == "##CSICodesA")
                        {
                            parsingContext = DBManagement.ParsingContext.CSICodesA;
                        }
                        else if (str3 == "##CSICodesB")
                        {
                            parsingContext = DBManagement.ParsingContext.CSICodesB;
                        }
                    }
                }
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplayFileOpenError(fileName, exception);
                flag = false;
            }
            return flag;
        }

        public bool Save(string fileName)
        {
            bool flag;
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        this.SaveToStream(streamWriter);
                    }
                    fileStream.Close();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplayFileSaveError(fileName, exception);
                flag = false;
            }
            return flag;
        }

        private void SaveToStream(StreamWriter sw)
        {
            string[] strArrays = this.SortHashTable(this.EstimatingItems.Collection, true);
            sw.WriteLine("##NextAvailableIndex");
            sw.WriteLine(this.EstimatingItems.NextAvailableIndex.ToString());
            sw.WriteLine("##EstimatingItems");
            for (int i = 0; i < this.EstimatingItems.Count; i++)
            {
                DBEstimatingItem item = this.EstimatingItems[Utilities.ConvertToInt(strArrays[i])];
                if (item != null)
                {
                    string str = item.ID.ToString();
                    string str1 = item.ItemType.ToString();
                    string description = item.Description;
                    string str2 = item.purchaseUnit;
                    string str3 = item.UnitMeasure.ToString();
                    string str4 = item.CoverageValue.ToString();
                    string str5 = item.CoverageUnit.ToString();
                    string str6 = item.PriceEach.ToString();
                    string str7 = item.SectionID.ToString();
                    string str8 = item.SubSectionID.ToString();
                    string bidCode = item.BidCode;
                    bool flag = Utilities.ConvertToBoolean(item.IsSystemItem, false);
                    string str9 = flag.ToString();
                    string[] strArrays1 = new string[] { str, ";", str1, ";", description, ";", str2, ";", str3, ";", str4, ";", str5, ";", str6, ";", str7, ";", str8, ";", bidCode, ";", str9, ";" };
                    sw.WriteLine(string.Concat(strArrays1));
                }
            }
            string[] strArrays2 = this.SortHashTable(this.CSICodesA.Collection, true);
            sw.WriteLine("##CSICodesA");
            for (int j = 0; j < this.CSICodesA.Count; j++)
            {
                DBEstimatingSection dBEstimatingSection = this.CSICodesA[Utilities.ConvertToInt(strArrays2[j])];
                if (dBEstimatingSection != null)
                {
                    string str10 = dBEstimatingSection.ID.ToString();
                    string name = dBEstimatingSection.Name;
                    string str11 = dBEstimatingSection.ParentID.ToString();
                    string[] strArrays3 = new string[] { str10, ";", name, ";", str11, ";" };
                    sw.WriteLine(string.Concat(strArrays3));
                }
            }
            string[] strArrays4 = this.SortHashTable(this.CSICodesB.Collection, true);
            sw.WriteLine("##CSICodesB");
            for (int k = 0; k < this.CSICodesB.Count; k++)
            {
                DBEstimatingSection item1 = this.CSICodesB[Utilities.ConvertToInt(strArrays4[k])];
                if (item1 != null)
                {
                    string str12 = item1.ID.ToString();
                    string name1 = item1.Name;
                    string str13 = item1.ParentID.ToString();
                    string[] strArrays5 = new string[] { str12, ";", name1, ";", str13, ";" };
                    sw.WriteLine(string.Concat(strArrays5));
                }
            }
        }

        public string[] SortHashTable(Hashtable hashtable, bool numericComparer = true)
        {
            string[] str = new string[hashtable.Count];
            int num = 0;
            foreach (DictionaryEntry dictionaryEntry in hashtable)
            {
                str[num] = dictionaryEntry.Key.ToString();
                num++;
            }
            if (!numericComparer)
            {
                Array.Sort<string>(str);
            }
            else
            {
                Array.Sort(str, new NumericComparer());
            }
            return str;
        }

        public void UpdateBrowserItem(DBEstimatingItem dbItem)
        {
            this.eEstimatingSections = null;
        }

        private int ValideTreeNodeParent(DBEstimatingItem estimatingItem)
        {
            int d = -1;
            int num = -1;
            List<TreeViewNode>.Enumerator enumerator = this.TreeItems.Collection.GetEnumerator();
            try
            {
                do
                {
                Label0:
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    TreeViewNode current = enumerator.Current;
                    if (current.Tag.GetType().Name == "Int32")
                    {
                        int tag = (int)current.Tag;
                        if (current.ParentID != 0)
                        {
                            if (tag != estimatingItem.SubSectionID)
                            {
                                continue;
                            }
                            num = current.ID;
                        }
                        else
                        {
                            if (tag != estimatingItem.SectionID)
                            {
                                continue;
                            }
                            d = current.ID;
                        }
                    }
                    else
                    {
                        goto Label0;
                    }
                }
                while (d <= -1 || num <= -1);
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            if (d == -1)
            {
                d = this.TreeItems.Add(0, estimatingItem.SectionID);
            }
            if (num == -1 && d != -1)
            {
                num = this.TreeItems.Add(d, estimatingItem.SubSectionID);
            }
            return num;
        }

        private enum ParsingContext
        {
            NextAvailableIndex,
            Products,
            CSICodesA,
            CSICodesB
        }
    }
}