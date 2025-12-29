using DevExpress.Utils;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class COfficeInterface
    {
        private const string COBinaryName = "one.exe";

        private const string COSystemDataFolder = "SystemData";

        private const string COResidentialDatabaseName = "aestmat.DBF";

        private const string COCommercialDatabaseName = "meansdata.dbf";

        public DataTable[] CODataTable = new DataTable[2];

        public TreeViewNodes[] COSections = new TreeViewNodes[2];

        private CEstimatingItemsBrowserInterface browserInterface;

        private CEstimatingItems browserProducts = new CEstimatingItems();

        private COfficeInterface.CODatabaseType browserSelectedDatabase;

        public CEstimatingItemsBrowserInterface BrowserInterface
        {
            get
            {
                return this.browserInterface;
            }
        }

        public bool IsReady
        {
            get;
            set;
        }

        public COfficeInterface()
        {
        }

        private void browserInterface_OnGetDefaultFormula(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
        {
            this.MatchDefaultTakeoffResult(drawObject, stats, product, coUnit);
        }

        private void browserInterface_OnLoadEstimatingItems(CEstimatingSection section, ref CEstimatingItems products)
        {
            this.RefreshProducts(section, this.CODataTable[(int)this.browserSelectedDatabase], this.browserSelectedDatabase, this.browserProducts);
            products = this.browserProducts;
        }

        private void browserInterface_OnLoadEstimatingSections(ref TreeViewNodes sections)
        {
            sections = this.COSections[(int)this.browserSelectedDatabase];
        }

        public void browserInterface_OnSelectDatabase(string databaseName, ref bool cancel)
        {
            COfficeInterface.CODatabaseType cODatabaseType = (databaseName == "COResidentialDatabase" ? COfficeInterface.CODatabaseType.COResidentialDatabase : COfficeInterface.CODatabaseType.COCommercialDatabase);
            if (this.browserSelectedDatabase != cODatabaseType)
            {
                this.browserSelectedDatabase = cODatabaseType;
                return;
            }
            cancel = true;
        }

        public bool DatabaseExists(COfficeInterface.CODatabaseType dbType)
        {
            return Utilities.FileExists(this.DatabasePath(dbType));
        }

        public string DatabasePath(COfficeInterface.CODatabaseType dbType)
        {
            string empty;
            try
            {
                switch (dbType)
                {
                    case COfficeInterface.CODatabaseType.COResidentialDatabase:
                        {
                            empty = Path.Combine(this.SystemDataPath(), "aestmat.DBF");
                            return empty;
                        }
                    case COfficeInterface.CODatabaseType.COCommercialDatabase:
                        {
                            empty = Path.Combine(this.SystemDataPath(), "meansdata.dbf");
                            return empty;
                        }
                }
                empty = string.Empty;
            }
            catch
            {
                empty = string.Empty;
            }
            return empty;
        }

        public bool EnsureRunning(bool setFocus)
        {
            bool flag;
            IntPtr zero = IntPtr.Zero;
            try
            {
                bool flag1 = this.IsRunning(ref zero);
                if (!flag1)
                {
                    if (!this.IsInstalled())
                    {
                        Utilities.DisplayError(Resources.Impossible_d_effectuer_l_opération_désirée, Resources.Expert_Estimateur_ne_semble_pas_installé_sur_cet_ordinateur);
                    }
                    else
                    {
                        flag1 = this.Start(ref zero);
                    }
                }
                if (flag1 && setFocus)
                {
                    Utilities.ForceWindowToForeground(zero);
                }
                flag = flag1;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private DataTable GetDataTableDBF(string strFileName, ref bool exitNow)
        {
            DataTable dataTable;
            try
            {
                OleDbConnection oleDbConnection = new OleDbConnection("Provider=VFPOLEDB.1;Data Source=C:\\PrioSoft\\SystemData\\");
                string str = string.Concat("SELECT CODE, TXT, ABOVE, DESCRIPT, COST, MEASURE FROM [", Path.GetFileName(strFileName), "] ORDER BY CODE");
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand(str, oleDbConnection));
                DataSet dataSet = new DataSet();
                oleDbDataAdapter.Fill(dataSet);
                DataTable item = new DataTable();
                item = dataSet.Tables[0];
                oleDbConnection.Close();
                dataTable = item;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Utilities.DisplaySystemError(exception);
                if (exception.Message.IndexOf("VFPOLEDB.1") > 0 && Utilities.DisplayWarningQuestion(Resources.Pilote_Fox_Pro_Non_Disponible, Resources.Ce_pilote_est_nécécessaire_pour_l_intégration_avec_Contractor_s_Office_Installer_le_pilote_maintenant) == DialogResult.Yes)
                {
                    Utilities.OpenDocument(Utilities.GetFoxProUrl());
                    exitNow = true;
                }
                dataTable = null;
            }
            return dataTable;
        }

        public void InitializeBrowserInterface(ImageCollection imageCollection, bool enableCOCommercialDatabase)
        {
            this.browserInterface = new CEstimatingItemsBrowserInterface(imageCollection, enableCOCommercialDatabase);
            this.browserInterface.OnSelectDatabase += new OnSelectDatabaseHandler(this.browserInterface_OnSelectDatabase);
            this.browserInterface.OnLoadEstimatingSections += new OnLoadEstimatingSectionsHandler(this.browserInterface_OnLoadEstimatingSections);
            this.browserInterface.OnLoadEstimatingItems += new OnLoadEstimatingItemsHandler(this.browserInterface_OnLoadEstimatingItems);
            this.browserInterface.OnGetDefaultFormula += new OnGetDefaultFormulaHandler(this.browserInterface_OnGetDefaultFormula);
        }

        public string InstalledPath()
        {
            string empty;
            try
            {
                empty = (Settings.Default.COfficePath == "" ? "C:\\PrioSoft" : Settings.Default.COfficePath);
            }
            catch
            {
                empty = string.Empty;
            }
            return empty;
        }

        public bool IsInstalled()
        {
            string str = this.InstalledPath();
            if (str == string.Empty)
            {
                return false;
            }
            return Utilities.FileExists(Path.Combine(str, "one.exe"));
        }

        public bool IsRunning(ref IntPtr handle)
        {
            return Utilities.FindWindow("one9c000000", "The Contractor's Office", ref handle);
        }

        private void LoadSections(COfficeInterface.CODatabaseType dbType)
        {
            this.COSections[(int)dbType] = new TreeViewNodes();
            this.RefreshSections(this.CODataTable[(int)dbType], dbType, this.COSections[(int)dbType]);
        }

        public void MatchDefaultTakeoffResult(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
        {
            coUnit = coUnit.Trim().ToUpper();
            string str = coUnit;
            string str1 = str;
            if (str != null)
            {
                switch (str1)
                {
                    case "CF":
                    case "CY":
                    case "SQ":
                    case "SY":
                        {
                            break;
                        }
                    case "EA":
                    case "PAIR":
                    case "EACH":
                    case "STEP":
                    case "SET":
                        {
                            if (drawObject.ObjectType != "Counter")
                            {
                                break;
                            }
                            product.Formula = string.Concat("[", Resources.Nombre_d_objets, "]");
                            return;
                        }
                    case "LF":
                        {
                            if (drawObject.ObjectType == "Perimeter")
                            {
                                product.Formula = string.Concat("[", Resources.Longueur_nette, "]");
                                return;
                            }
                            if (drawObject.ObjectType != "Line")
                            {
                                break;
                            }
                            product.Formula = string.Concat("[", Resources.Longueur, "]");
                            return;
                        }
                    case "SF":
                        {
                            if (drawObject.ObjectType != "Area")
                            {
                                break;
                            }
                            product.Formula = string.Concat("[", Resources.Surface_déductions, "]");
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
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

        public bool OpenDatabase(COfficeInterface.CODatabaseType dbType, ref bool exitNow)
        {
            DataTable dataTableDBF = this.GetDataTableDBF(this.DatabasePath(dbType), ref exitNow);
            if (dataTableDBF != null)
            {
                this.CODataTable[(int)dbType] = dataTableDBF;
                this.LoadSections(dbType);
            }
            return dataTableDBF != null;
        }

        public string ProjectsPath()
        {
            string empty;
            try
            {
                empty = Path.Combine(this.InstalledPath(), "Projects");
            }
            catch
            {
                empty = string.Empty;
            }
            return empty;
        }

        private void QueryProducts(CEstimatingSection section, DataTable table, COfficeInterface.CODatabaseType dbType, CEstimatingItems products)
        {
            switch (dbType)
            {
                case COfficeInterface.CODatabaseType.COResidentialDatabase:
                    {
                        this.QueryProductsFromResidentialDatabase(section, table, products);
                        return;
                    }
                case COfficeInterface.CODatabaseType.COCommercialDatabase:
                    {
                        this.QueryProductsFromCommercialDatabase(section, table, products);
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void QueryProductsFromCommercialDatabase(CEstimatingSection section, DataTable table, CEstimatingItems products)
        {
            string str = "";
            foreach (DataRow row in table.Rows)
            {
                string str1 = row.Field<string>("TXT").Trim();
                if (str1 != "")
                {
                    continue;
                }
                if (row.Field<string>("ABOVE").Trim() != section.Name)
                {
                    if (!(str1 == "'") || !(str != ""))
                    {
                        continue;
                    }
                    return;
                }
                else
                {
                    string str2 = row.Field<string>("CODE");
                    string str3 = row.Field<string>("DESCRIPT").Trim();
                    decimal num = row.Field<decimal>("COST");
                    string str4 = row.Field<string>("MEASURE").Trim();
                    CEstimatingItem cEstimatingItem = new CEstimatingItem(
                        str2,
                        str3,
                        (double)num,
                        str4,
                        DBEstimatingItem.EstimatingItemType.MaterialItem,
                        DBEstimatingItem.UnitMeasureType.none,
                        0,
                        1,
                        0,
                        0,
                        "",
                        ""
                    );

                    cEstimatingItem.Tag = cEstimatingItem;

                    products.Add(cEstimatingItem);
                }
            }
        }

        private void QueryProductsFromResidentialDatabase(CEstimatingSection section, DataTable table, CEstimatingItems products)
        {
            string str = "";
            foreach (DataRow row in table.Rows)
            {
                string str1 = row.Field<string>("TXT").Trim();
                if (str1 != "")
                {
                    continue;
                }
                if (row.Field<string>("ABOVE").Trim() != section.Name)
                {
                    if (!(str1 == "'") || !(str != ""))
                    {
                        continue;
                    }
                    return;
                }
                else
                {
                    string str2 = string.Format("{0:F6}", row.Field<decimal>("CODE"));
                    string str3 = row.Field<string>("DESCRIPT").Trim();
                    decimal num = row.Field<decimal>("COST");
                    string str4 = row.Field<string>("MEASURE").Trim();

                    CEstimatingItem cEstimatingItem = new CEstimatingItem(
                        str2,
                        str3,
                        (double)num,
                        str4,
                        DBEstimatingItem.EstimatingItemType.MaterialItem,
                        DBEstimatingItem.UnitMeasureType.none,
                        0,
                        1,
                        0,
                        0,
                        "",
                        ""
                    );

                    cEstimatingItem.Tag = cEstimatingItem;

                    products.Add(cEstimatingItem);
                }
            }
        }

        private void QuerySections(DataTable table, COfficeInterface.CODatabaseType dbType, TreeViewNodes sections)
        {
            switch (dbType)
            {
                case COfficeInterface.CODatabaseType.COResidentialDatabase:
                    {
                        this.QuerySectionsFromResidentialDatabase(table, sections);
                        return;
                    }
                case COfficeInterface.CODatabaseType.COCommercialDatabase:
                    {
                        this.QuerySectionsFromCommercialDatabase(table, sections);
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void QuerySectionsFromCommercialDatabase(DataTable table, TreeViewNodes sections)
        {
            Hashtable hashtables = new Hashtable();
            int num = -1;
            foreach (DataRow row in table.Rows)
            {
                if (row.Field<string>("TXT").Trim() != "i")
                {
                    continue;
                }
                string str = row.Field<string>("CODE");
                try
                {
                    string str1 = "";
                    str.Substring(0, 2);
                    int num1 = Utilities.ConvertToInt(str.Substring(0, 8));
                    if (num1 <= 0 || num1 > 0x243d57f)
                    {
                        num = -1;
                    }
                    else
                    {
                        string str2 = row.Field<string>("ABOVE").Trim();
                        if (str.Substring(2, 8) == "00000000")
                        {
                            str1 = string.Concat("0;", str, ";");
                            if (!hashtables.ContainsKey(str1))
                            {
                                CEstimatingSection cEstimatingSection = new CEstimatingSection(num1, 0, str, str2);
                                cEstimatingSection.Tag = cEstimatingSection;

                                sections.Add(num1, 0, cEstimatingSection);
                                num = num1;
                                hashtables.Add(str1, cEstimatingSection);
                            }
                        }
                        else if (num > -1)
                        {
                            str1 = string.Concat(num.ToString(), ";", str, ";");
                            if (!hashtables.ContainsKey(str1))
                            {
                                CEstimatingSection cEstimatingSection1 = new CEstimatingSection(num1, num, str, str2);
                                cEstimatingSection1.Tag = cEstimatingSection1;
                                sections.Add(num1, num, cEstimatingSection1);
                                hashtables.Add(str1, cEstimatingSection1);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void QuerySectionsFromResidentialDatabase(DataTable table, TreeViewNodes sections)
        {
            Hashtable hashtables = new Hashtable();
            int num = -1;
            foreach (DataRow row in table.Rows)
            {
                if (row.Field<string>("TXT").Trim() != "i")
                {
                    continue;
                }
                string str = string.Format("{0:F6}", row.Field<decimal>("CODE"));
                try
                {
                    string str1 = "";
                    string str2 = str.Replace(Utilities.NumberDecimalSeparator(), "");
                    int num1 = Utilities.ConvertToInt(str2);
                    if (num1 <= 0 || num1 > 0x243d57f)
                    {
                        num = -1;
                    }
                    else
                    {
                        string str3 = row.Field<string>("ABOVE").Trim();
                        if (str.Substring(str.IndexOf(Utilities.NumberDecimalSeparator()) + 1, 6) == "000000")
                        {
                            str1 = string.Concat("0;", str, ";");
                            if (!hashtables.ContainsKey(str1))
                            {
                                CEstimatingSection cEstimatingSection = new CEstimatingSection(num1, 0, str, str3);
                                cEstimatingSection.Tag = cEstimatingSection;

                                sections.Add(num1, 0, cEstimatingSection);
                                num = num1;
                                hashtables.Add(str1, cEstimatingSection);
                            }
                        }
                        else if (num > -1)
                        {
                            str1 = string.Concat(num.ToString(), ";", str, ";");
                            if (!hashtables.ContainsKey(str1))
                            {
                                CEstimatingSection cEstimatingSection1 = new CEstimatingSection(num1, num, str, str3);
                                cEstimatingSection1.Tag = cEstimatingSection1;

                                sections.Add(num1, num, cEstimatingSection1);
                                hashtables.Add(str1, cEstimatingSection1);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public void RefreshProducts(CEstimatingSection section, DataTable table, COfficeInterface.CODatabaseType dbType, CEstimatingItems products)
        {
            products.Clear();
            this.QueryProducts(section, table, dbType, products);
        }

        public void RefreshSections(DataTable table, COfficeInterface.CODatabaseType dbType, TreeViewNodes sections)
        {
            sections.Clear();
            this.QuerySections(table, dbType, sections);
        }

        public bool Start(ref IntPtr handle)
        {
            string str = this.InstalledPath();
            if (str == string.Empty)
            {
                return false;
            }
            return Utilities.StartProcess(Path.Combine(str, "one.exe"), ref handle, 0x1388);
        }

        public string SystemDataPath()
        {
            string empty;
            try
            {
                empty = Path.Combine(this.InstalledPath(), "SystemData");
            }
            catch
            {
                empty = string.Empty;
            }
            return empty;
        }

        public enum CODatabaseType
        {
            COResidentialDatabase,
            COCommercialDatabase
        }
    }
}