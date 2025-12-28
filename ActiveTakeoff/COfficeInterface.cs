using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.Utils;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class COfficeInterface
	{
		public CEstimatingItemsBrowserInterface BrowserInterface
		{
			get
			{
				return this.browserInterface;
			}
		}

		public bool IsReady
		{
			[CompilerGenerated]
			get
			{
				return this.<IsReady>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsReady>k__BackingField = value;
			}
		}

		public COfficeInterface()
		{
		}

		public string InstalledPath()
		{
			string result;
			try
			{
				result = ((Settings.Default.COfficePath == "") ? "C:\\PrioSoft" : Settings.Default.COfficePath);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		public string SystemDataPath()
		{
			string result;
			try
			{
				result = Path.Combine(this.InstalledPath(), "SystemData");
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		public string DatabasePath(COfficeInterface.CODatabaseType dbType)
		{
			string result;
			try
			{
				switch (dbType)
				{
				case COfficeInterface.CODatabaseType.COResidentialDatabase:
					result = Path.Combine(this.SystemDataPath(), "aestmat.DBF");
					break;
				case COfficeInterface.CODatabaseType.COCommercialDatabase:
					result = Path.Combine(this.SystemDataPath(), "meansdata.dbf");
					break;
				default:
					result = string.Empty;
					break;
				}
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		public string ProjectsPath()
		{
			string result;
			try
			{
				result = Path.Combine(this.InstalledPath(), "Projects");
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		public bool IsInstalled()
		{
			string text = this.InstalledPath();
			return !(text == string.Empty) && Utilities.FileExists(Path.Combine(text, "one.exe"));
		}

		public bool IsRunning(ref IntPtr handle)
		{
			return Utilities.FindWindow("one9c000000", "The Contractor's Office", ref handle);
		}

		public bool Start(ref IntPtr handle)
		{
			string text = this.InstalledPath();
			return !(text == string.Empty) && Utilities.StartProcess(Path.Combine(text, "one.exe"), ref handle, 5000);
		}

		public bool EnsureRunning(bool setFocus)
		{
			IntPtr zero = IntPtr.Zero;
			bool result;
			try
			{
				bool flag = this.IsRunning(ref zero);
				if (!flag)
				{
					if (this.IsInstalled())
					{
						flag = this.Start(ref zero);
					}
					else
					{
						Utilities.DisplayError(Resources.Impossible_d_effectuer_l_opération_désirée, Resources.Expert_Estimateur_ne_semble_pas_installé_sur_cet_ordinateur);
					}
				}
				if (flag && setFocus)
				{
					Utilities.ForceWindowToForeground(zero);
				}
				result = flag;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private DataTable GetDataTableDBF(string strFileName, ref bool exitNow)
		{
			DataTable result;
			try
			{
				string connectionString = "Provider=VFPOLEDB.1;Data Source=C:\\PrioSoft\\SystemData\\";
				OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
				string cmdText = "SELECT CODE, TXT, ABOVE, DESCRIPT, COST, MEASURE FROM [" + Path.GetFileName(strFileName) + "] ORDER BY CODE";
				OleDbCommand selectCommand = new OleDbCommand(cmdText, oleDbConnection);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				DataSet dataSet = new DataSet();
				oleDbDataAdapter.Fill(dataSet);
				DataTable dataTable = new DataTable();
				dataTable = dataSet.Tables[0];
				oleDbConnection.Close();
				result = dataTable;
			}
			catch (Exception ex)
			{
				Utilities.DisplaySystemError(ex);
				if (ex.Message.IndexOf("VFPOLEDB.1") > 0)
				{
					string pilote_Fox_Pro_Non_Disponible = Resources.Pilote_Fox_Pro_Non_Disponible;
					string ce_pilote_est_nécécessaire_pour_l_intégration_avec_Contractor_s_Office_Installer_le_pilote_maintenant = Resources.Ce_pilote_est_nécécessaire_pour_l_intégration_avec_Contractor_s_Office_Installer_le_pilote_maintenant;
					if (Utilities.DisplayWarningQuestion(pilote_Fox_Pro_Non_Disponible, ce_pilote_est_nécécessaire_pour_l_intégration_avec_Contractor_s_Office_Installer_le_pilote_maintenant) == DialogResult.Yes)
					{
						Utilities.OpenDocument(Utilities.GetFoxProUrl());
						exitNow = true;
					}
				}
				result = null;
			}
			return result;
		}

		private void QuerySectionsFromResidentialDatabase(DataTable table, TreeViewNodes sections)
		{
			Hashtable hashtable = new Hashtable();
			int num = -1;
			foreach (object obj in table.Rows)
			{
				DataRow row = (DataRow)obj;
				string a = row.Field("TXT").Trim();
				if (a == "i")
				{
					string text = string.Format("{0:F6}", row.Field("CODE"));
					try
					{
						string value = text.Replace(Utilities.NumberDecimalSeparator(), "");
						int num2 = Utilities.ConvertToInt(value);
						if (num2 > 0 && num2 <= 37999999)
						{
							string name = row.Field("ABOVE").Trim();
							string a2 = text.Substring(text.IndexOf(Utilities.NumberDecimalSeparator()) + 1, 6);
							if (a2 == "000000")
							{
								string key = "0;" + text + ";";
								if (!hashtable.ContainsKey(key))
								{
									CEstimatingSection cestimatingSection = new CEstimatingSection(num2, 0, text, name);
									cestimatingSection.Tag = cestimatingSection;
									sections.Add(num2, 0, cestimatingSection);
									num = num2;
									hashtable.Add(key, cestimatingSection);
								}
							}
							else if (num > -1)
							{
								string key = num.ToString() + ";" + text + ";";
								if (!hashtable.ContainsKey(key))
								{
									CEstimatingSection cestimatingSection2 = new CEstimatingSection(num2, num, text, name);
									cestimatingSection2.Tag = cestimatingSection2;
									sections.Add(num2, num, cestimatingSection2);
									hashtable.Add(key, cestimatingSection2);
								}
							}
						}
						else
						{
							num = -1;
						}
					}
					catch
					{
					}
				}
			}
		}

		private void QuerySectionsFromCommercialDatabase(DataTable table, TreeViewNodes sections)
		{
			Hashtable hashtable = new Hashtable();
			int num = -1;
			foreach (object obj in table.Rows)
			{
				DataRow row = (DataRow)obj;
				string a = row.Field("TXT").Trim();
				if (a == "i")
				{
					string text = row.Field("CODE");
					try
					{
						string text2 = text.Substring(0, 2);
						string value = text.Substring(0, 8);
						int num2 = Utilities.ConvertToInt(value);
						if (num2 > 0 && num2 <= 37999999)
						{
							string name = row.Field("ABOVE").Trim();
							string a2 = text.Substring(2, 8);
							if (a2 == "00000000")
							{
								string key = "0;" + text + ";";
								if (!hashtable.ContainsKey(key))
								{
									CEstimatingSection cestimatingSection = new CEstimatingSection(num2, 0, text, name);
									cestimatingSection.Tag = cestimatingSection;
									sections.Add(num2, 0, cestimatingSection);
									num = num2;
									hashtable.Add(key, cestimatingSection);
								}
							}
							else if (num > -1)
							{
								string key = num.ToString() + ";" + text + ";";
								if (!hashtable.ContainsKey(key))
								{
									CEstimatingSection cestimatingSection2 = new CEstimatingSection(num2, num, text, name);
									cestimatingSection2.Tag = cestimatingSection2;
									sections.Add(num2, num, cestimatingSection2);
									hashtable.Add(key, cestimatingSection2);
								}
							}
						}
						else
						{
							num = -1;
						}
					}
					catch
					{
					}
				}
			}
		}

		private void QuerySections(DataTable table, COfficeInterface.CODatabaseType dbType, TreeViewNodes sections)
		{
			switch (dbType)
			{
			case COfficeInterface.CODatabaseType.COResidentialDatabase:
				this.QuerySectionsFromResidentialDatabase(table, sections);
				return;
			case COfficeInterface.CODatabaseType.COCommercialDatabase:
				this.QuerySectionsFromCommercialDatabase(table, sections);
				return;
			default:
				return;
			}
		}

		public void RefreshSections(DataTable table, COfficeInterface.CODatabaseType dbType, TreeViewNodes sections)
		{
			sections.Clear();
			this.QuerySections(table, dbType, sections);
		}

		private void LoadSections(COfficeInterface.CODatabaseType dbType)
		{
			this.COSections[(int)dbType] = new TreeViewNodes();
			this.RefreshSections(this.CODataTable[(int)dbType], dbType, this.COSections[(int)dbType]);
		}

		public bool DatabaseExists(COfficeInterface.CODatabaseType dbType)
		{
			return Utilities.FileExists(this.DatabasePath(dbType));
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

		public void InitializeBrowserInterface(ImageCollection imageCollection, bool enableCOCommercialDatabase)
		{
			this.browserInterface = new CEstimatingItemsBrowserInterface(imageCollection, enableCOCommercialDatabase);
			this.browserInterface.OnSelectDatabase += this.browserInterface_OnSelectDatabase;
			this.browserInterface.OnLoadEstimatingSections += this.browserInterface_OnLoadEstimatingSections;
			this.browserInterface.OnLoadEstimatingItems += this.browserInterface_OnLoadEstimatingItems;
			this.browserInterface.OnGetDefaultFormula += this.browserInterface_OnGetDefaultFormula;
		}

		public void browserInterface_OnSelectDatabase(string databaseName, ref bool cancel)
		{
			COfficeInterface.CODatabaseType codatabaseType = (databaseName == "COResidentialDatabase") ? COfficeInterface.CODatabaseType.COResidentialDatabase : COfficeInterface.CODatabaseType.COCommercialDatabase;
			if (this.browserSelectedDatabase != codatabaseType)
			{
				this.browserSelectedDatabase = codatabaseType;
				return;
			}
			cancel = true;
		}

		private void browserInterface_OnLoadEstimatingSections(ref TreeViewNodes sections)
		{
			sections = this.COSections[(int)this.browserSelectedDatabase];
		}

		private void QueryProductsFromResidentialDatabase(CEstimatingSection section, DataTable table, CEstimatingItems products)
		{
			string a = "";
			foreach (object obj in table.Rows)
			{
				DataRow row = (DataRow)obj;
				string a2 = row.Field("TXT").Trim();
				if (a2 == "")
				{
					string a3 = row.Field("ABOVE").Trim();
					if (a3 == section.Name)
					{
						string itemID = string.Format("{0:F6}", row.Field("CODE"));
						string description = row.Field("DESCRIPT").Trim();
						decimal value = row.Field("COST");
						string unit = row.Field("MEASURE").Trim();
						CEstimatingItem cestimatingItem = new CEstimatingItem(itemID, description, (double)value, unit, DBEstimatingItem.EstimatingItemType.MaterialItem, DBEstimatingItem.UnitMeasureType.none, 0.0, 1.0, 0, 0, "", "");
						cestimatingItem.Tag = cestimatingItem;
						products.Add(cestimatingItem);
					}
					else if (a2 == "'" && a != "")
					{
						break;
					}
				}
			}
		}

		private void QueryProductsFromCommercialDatabase(CEstimatingSection section, DataTable table, CEstimatingItems products)
		{
			string a = "";
			foreach (object obj in table.Rows)
			{
				DataRow row = (DataRow)obj;
				string a2 = row.Field("TXT").Trim();
				if (a2 == "")
				{
					string a3 = row.Field("ABOVE").Trim();
					if (a3 == section.Name)
					{
						string itemID = row.Field("CODE");
						string description = row.Field("DESCRIPT").Trim();
						decimal value = row.Field("COST");
						string unit = row.Field("MEASURE").Trim();
						CEstimatingItem cestimatingItem = new CEstimatingItem(itemID, description, (double)value, unit, DBEstimatingItem.EstimatingItemType.MaterialItem, DBEstimatingItem.UnitMeasureType.none, 0.0, 1.0, 0, 0, "", "");
						cestimatingItem.Tag = cestimatingItem;
						products.Add(cestimatingItem);
					}
					else if (a2 == "'" && a != "")
					{
						break;
					}
				}
			}
		}

		private void QueryProducts(CEstimatingSection section, DataTable table, COfficeInterface.CODatabaseType dbType, CEstimatingItems products)
		{
			switch (dbType)
			{
			case COfficeInterface.CODatabaseType.COResidentialDatabase:
				this.QueryProductsFromResidentialDatabase(section, table, products);
				return;
			case COfficeInterface.CODatabaseType.COCommercialDatabase:
				this.QueryProductsFromCommercialDatabase(section, table, products);
				return;
			default:
				return;
			}
		}

		public void RefreshProducts(CEstimatingSection section, DataTable table, COfficeInterface.CODatabaseType dbType, CEstimatingItems products)
		{
			products.Clear();
			this.QueryProducts(section, table, dbType, products);
		}

		private void browserInterface_OnLoadEstimatingItems(CEstimatingSection section, ref CEstimatingItems products)
		{
			this.RefreshProducts(section, this.CODataTable[(int)this.browserSelectedDatabase], this.browserSelectedDatabase, this.browserProducts);
			products = this.browserProducts;
		}

		private void browserInterface_OnGetDefaultFormula(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
		{
			this.MatchDefaultTakeoffResult(drawObject, stats, product, coUnit);
		}

		private void MatchDefaultTakeoffResultInExtension(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
		{
			foreach (object obj in drawObject.Group.Presets.Collection)
			{
				Preset preset = (Preset)obj;
				drawObject.DrawArea.Project.ExtensionsSupport.QueryPresetResults(preset, stats, drawObject.DrawArea.ActivePlan.UnitScale);
				coUnit = coUnit.Trim().ToUpper();
				string a;
				if ((a = coUnit) != null && !(a == "CF") && !(a == "CY"))
				{
					a == "SF";
				}
				if (product.Formula != "")
				{
					break;
				}
			}
		}

		public void MatchDefaultTakeoffResult(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
		{
			coUnit = coUnit.Trim().ToUpper();
			string key;
			switch (key = coUnit)
			{
			case "CF":
			case "CY":
			case "SQ":
			case "SY":
				break;
			case "EA":
			case "PAIR":
			case "EACH":
			case "STEP":
			case "SET":
				if (drawObject.ObjectType == "Counter")
				{
					product.Formula = "[" + Resources.Nombre_d_objets + "]";
					return;
				}
				break;
			case "LF":
				if (drawObject.ObjectType == "Perimeter")
				{
					product.Formula = "[" + Resources.Longueur_nette + "]";
					return;
				}
				if (drawObject.ObjectType == "Line")
				{
					product.Formula = "[" + Resources.Longueur + "]";
					return;
				}
				break;
			case "SF":
				if (drawObject.ObjectType == "Area")
				{
					product.Formula = "[" + Resources.Surface_déductions + "]";
				}
				break;

				return;
			}
		}

		private const string COBinaryName = "one.exe";

		private const string COSystemDataFolder = "SystemData";

		private const string COResidentialDatabaseName = "aestmat.DBF";

		private const string COCommercialDatabaseName = "meansdata.dbf";

		public DataTable[] CODataTable = new DataTable[2];

		public TreeViewNodes[] COSections = new TreeViewNodes[2];

		private CEstimatingItemsBrowserInterface browserInterface;

		private CEstimatingItems browserProducts = new CEstimatingItems();

		private COfficeInterface.CODatabaseType browserSelectedDatabase;

		[CompilerGenerated]
		private bool <IsReady>k__BackingField;

		public enum CODatabaseType
		{
			COResidentialDatabase,
			COCommercialDatabase
		}
	}
}
