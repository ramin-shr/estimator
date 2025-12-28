using System;
using System.Collections;
using System.IO;
using System.Text;
using DevExpress.Utils;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class DBManagement
	{
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

		private string GetStringField(string[] fields, int index, string defaultValue)
		{
			string result;
			try
			{
				result = fields[index];
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		private int GetIntField(string[] fields, int index, int defaultValue)
		{
			int result;
			try
			{
				result = Utilities.ConvertToInt(fields[index]);
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		private double GetDoubleField(string[] fields, int index, double defaultValue)
		{
			double result;
			try
			{
				result = Utilities.ConvertToDouble(fields[index], -1);
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		private bool GetBoolField(string[] fields, int index, bool defaultValue)
		{
			bool result;
			try
			{
				result = Utilities.ConvertToBoolean(fields[index], defaultValue);
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		public DBEstimatingItem GetEstimatingItem(int itemID)
		{
			DBEstimatingItem result;
			try
			{
				result = this.EstimatingItems[itemID];
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private void AddSection(string[] fields, DBEstimatingSections sections)
		{
			int intField = this.GetIntField(fields, 0, -1);
			if (intField > -1)
			{
				int intField2 = this.GetIntField(fields, 2, -1);
				string stringField = this.GetStringField(fields, 1, "");
				DBEstimatingSection section = new DBEstimatingSection(intField, intField2, stringField);
				sections.Add(section);
			}
		}

		private int ValideTreeNodeParent(DBEstimatingItem estimatingItem)
		{
			int num = -1;
			int num2 = -1;
			foreach (TreeViewNode treeViewNode in this.TreeItems.Collection)
			{
				if (treeViewNode.Tag.GetType().Name == "Int32")
				{
					int num3 = (int)treeViewNode.Tag;
					if (treeViewNode.ParentID == 0)
					{
						if (num3 == estimatingItem.SectionID)
						{
							num = treeViewNode.ID;
						}
					}
					else if (num3 == estimatingItem.SubSectionID)
					{
						num2 = treeViewNode.ID;
					}
					if (num > -1 && num2 > -1)
					{
						break;
					}
				}
			}
			if (num == -1)
			{
				num = this.TreeItems.Add(0, estimatingItem.SectionID);
			}
			if (num2 == -1 && num != -1)
			{
				num2 = this.TreeItems.Add(num, estimatingItem.SubSectionID);
			}
			return num2;
		}

		public int InsertTreeNode(DBEstimatingItem estimatingItem)
		{
			int result = -1;
			int num = this.ValideTreeNodeParent(estimatingItem);
			if (num > -1)
			{
				result = this.TreeItems.Add(num, estimatingItem);
			}
			else
			{
				Console.WriteLine(estimatingItem.Description);
			}
			return result;
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

		public bool Open(string fileName)
		{
			DBManagement.ParsingContext parsingContext = DBManagement.ParsingContext.Products;
			bool result;
			try
			{
				foreach (string text in File.ReadLines(fileName))
				{
					if (text.StartsWith("##"))
					{
						string a;
						if ((a = text) != null)
						{
							if (!(a == "##NextAvailableIndex"))
							{
								if (!(a == "##EstimatingItems"))
								{
									if (!(a == "##CSICodesA"))
									{
										if (a == "##CSICodesB")
										{
											parsingContext = DBManagement.ParsingContext.CSICodesB;
										}
									}
									else
									{
										parsingContext = DBManagement.ParsingContext.CSICodesA;
									}
								}
								else
								{
									parsingContext = DBManagement.ParsingContext.Products;
								}
							}
							else
							{
								parsingContext = DBManagement.ParsingContext.NextAvailableIndex;
							}
						}
					}
					else
					{
						string[] fields = text.Split(new char[]
						{
							';'
						});
						switch (parsingContext)
						{
						case DBManagement.ParsingContext.NextAvailableIndex:
							this.EstimatingItems.NextAvailableIndex = this.GetIntField(fields, 0, 0);
							break;
						case DBManagement.ParsingContext.Products:
						{
							int intField = this.GetIntField(fields, 0, -1);
							if (intField > -1)
							{
								int intField2 = this.GetIntField(fields, 1, 0);
								string stringField = this.GetStringField(fields, 2, "");
								string text2 = this.GetStringField(fields, 3, "");
								if (text2 == "fle")
								{
									text2 = "sh";
								}
								int intField3 = this.GetIntField(fields, 4, 0);
								double doubleField = this.GetDoubleField(fields, 5, 0.0);
								double doubleField2 = this.GetDoubleField(fields, 6, 1.0);
								double doubleField3 = this.GetDoubleField(fields, 7, 0.0);
								int intField4 = this.GetIntField(fields, 8, -1);
								int intField5 = this.GetIntField(fields, 9, -1);
								string stringField2 = this.GetStringField(fields, 10, "");
								bool boolField = this.GetBoolField(fields, 11, false);
								DBEstimatingItem estimatingItem = new DBEstimatingItem(intField, (DBEstimatingItem.EstimatingItemType)intField2, stringField, text2, (DBEstimatingItem.UnitMeasureType)intField3, doubleField, doubleField2, doubleField3, intField4, intField5, stringField2, boolField);
								this.AddEstimatingItem(estimatingItem);
							}
							break;
						}
						case DBManagement.ParsingContext.CSICodesA:
							this.AddSection(fields, this.CSICodesA);
							break;
						case DBManagement.ParsingContext.CSICodesB:
							this.AddSection(fields, this.CSICodesB);
							break;
						}
					}
				}
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileOpenError(fileName, exception);
				result = false;
			}
			return result;
		}

		public bool Save(string fileName)
		{
			bool result;
			try
			{
				using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
					{
						this.SaveToStream(streamWriter);
					}
					fileStream.Close();
					result = true;
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileSaveError(fileName, exception);
				result = false;
			}
			return result;
		}

		public string[] SortHashTable(Hashtable hashtable, bool numericComparer = true)
		{
			string[] array = new string[hashtable.Count];
			int num = 0;
			foreach (object obj in hashtable)
			{
				array[num] = ((DictionaryEntry)obj).Key.ToString();
				num++;
			}
			if (numericComparer)
			{
				Array.Sort(array, new NumericComparer());
			}
			else
			{
				Array.Sort<string>(array);
			}
			return array;
		}

		private void SaveToStream(StreamWriter sw)
		{
			string[] array = this.SortHashTable(this.EstimatingItems.Collection, true);
			sw.WriteLine("##NextAvailableIndex");
			sw.WriteLine(this.EstimatingItems.NextAvailableIndex.ToString());
			sw.WriteLine("##EstimatingItems");
			for (int i = 0; i < this.EstimatingItems.Count; i++)
			{
				DBEstimatingItem dbestimatingItem = this.EstimatingItems[Utilities.ConvertToInt(array[i])];
				if (dbestimatingItem != null)
				{
					string text = dbestimatingItem.ID.ToString();
					string text2 = ((int)dbestimatingItem.ItemType).ToString();
					string description = dbestimatingItem.Description;
					string purchaseUnit = dbestimatingItem.purchaseUnit;
					string text3 = ((int)dbestimatingItem.UnitMeasure).ToString();
					string text4 = dbestimatingItem.CoverageValue.ToString();
					string text5 = dbestimatingItem.CoverageUnit.ToString();
					string text6 = dbestimatingItem.PriceEach.ToString();
					string text7 = dbestimatingItem.SectionID.ToString();
					string text8 = dbestimatingItem.SubSectionID.ToString();
					string bidCode = dbestimatingItem.BidCode;
					string text9 = Utilities.ConvertToBoolean(dbestimatingItem.IsSystemItem, false).ToString();
					sw.WriteLine(string.Concat(new string[]
					{
						text,
						";",
						text2,
						";",
						description,
						";",
						purchaseUnit,
						";",
						text3,
						";",
						text4,
						";",
						text5,
						";",
						text6,
						";",
						text7,
						";",
						text8,
						";",
						bidCode,
						";",
						text9,
						";"
					}));
				}
			}
			string[] array2 = this.SortHashTable(this.CSICodesA.Collection, true);
			sw.WriteLine("##CSICodesA");
			for (int j = 0; j < this.CSICodesA.Count; j++)
			{
				DBEstimatingSection dbestimatingSection = this.CSICodesA[Utilities.ConvertToInt(array2[j])];
				if (dbestimatingSection != null)
				{
					string text10 = dbestimatingSection.ID.ToString();
					string name = dbestimatingSection.Name;
					string text11 = dbestimatingSection.ParentID.ToString();
					sw.WriteLine(string.Concat(new string[]
					{
						text10,
						";",
						name,
						";",
						text11,
						";"
					}));
				}
			}
			string[] array3 = this.SortHashTable(this.CSICodesB.Collection, true);
			sw.WriteLine("##CSICodesB");
			for (int k = 0; k < this.CSICodesB.Count; k++)
			{
				DBEstimatingSection dbestimatingSection2 = this.CSICodesB[Utilities.ConvertToInt(array3[k])];
				if (dbestimatingSection2 != null)
				{
					string text12 = dbestimatingSection2.ID.ToString();
					string name2 = dbestimatingSection2.Name;
					string text13 = dbestimatingSection2.ParentID.ToString();
					sw.WriteLine(string.Concat(new string[]
					{
						text12,
						";",
						name2,
						";",
						text13,
						";"
					}));
				}
			}
		}

		public void InitializeBrowserInterface(ImageCollection imageCollection)
		{
			this.browserInterface = new CEstimatingItemsBrowserInterface(imageCollection, false);
			this.browserInterface.OnSelectDatabase += this.browserInterface_OnSelectDatabase;
			this.browserInterface.OnLoadEstimatingSections += this.browserInterface_OnLoadEstimatingSections;
			this.browserInterface.OnLoadEstimatingItems += this.browserInterface_OnLoadEstimatingItems;
			this.browserInterface.OnGetDefaultFormula += this.browserInterface_OnGetDefaultFormula;
		}

		private void browserInterface_OnSelectDatabase(string databaseName, ref bool cancel)
		{
		}

		public void AddBrowserItem(DBEstimatingItem dbItem)
		{
			this.eEstimatingSections = null;
		}

		public void UpdateBrowserItem(DBEstimatingItem dbItem)
		{
			this.eEstimatingSections = null;
		}

		public void DeleteBrowserItem(DBEstimatingItem dbItem)
		{
			this.eEstimatingSections = null;
		}

		private void browserInterface_OnLoadEstimatingSections(ref TreeViewNodes sections)
		{
			if (this.eEstimatingSections == null)
			{
				this.eEstimatingSections = new TreeViewNodes();
				foreach (TreeViewNode treeViewNode in this.TreeItems.Collection)
				{
					if (treeViewNode.Tag.GetType() == typeof(int))
					{
						if (treeViewNode.ParentID == 0)
						{
							DBEstimatingSection dbestimatingSection = this.CSICodesA[Utilities.ConvertToInt(treeViewNode.Tag)];
							if (dbestimatingSection != null)
							{
								string name = Utilities.ConvertToInt(treeViewNode.Tag).ToString("D5") + " - " + dbestimatingSection.Name;
								CEstimatingSection cestimatingSection = new CEstimatingSection(treeViewNode.ID, 0, treeViewNode.Tag.ToString(), name);
								cestimatingSection.Tag = cestimatingSection;
								this.eEstimatingSections.Add(treeViewNode.ID, 0, cestimatingSection);
							}
						}
						else
						{
							DBEstimatingSection dbestimatingSection2 = this.CSICodesB[Utilities.ConvertToInt(treeViewNode.Tag)];
							if (dbestimatingSection2 != null)
							{
								string name = Utilities.ConvertToInt(treeViewNode.Tag).ToString("D5") + " - " + dbestimatingSection2.Name;
								CEstimatingSection cestimatingSection2 = new CEstimatingSection(treeViewNode.ID, treeViewNode.ParentID, treeViewNode.Tag.ToString(), name);
								cestimatingSection2.Tag = cestimatingSection2;
								this.eEstimatingSections.Add(treeViewNode.ID, treeViewNode.ParentID, cestimatingSection2);
							}
						}
					}
				}
			}
			sections = this.eEstimatingSections;
		}

		private void browserInterface_OnLoadEstimatingItems(CEstimatingSection section, ref CEstimatingItems products)
		{
			Utilities.EnableInterface(this.browserInterface.Browser, false);
			this.browserProducts.Clear();
			foreach (object obj in this.EstimatingItems.Collection)
			{
				DBEstimatingItem dbestimatingItem = (DBEstimatingItem)((DictionaryEntry)obj).Value;
				if ((section.ParentID == 0 && dbestimatingItem.SectionID == Utilities.ConvertToInt(section.SectionID)) || (section.ParentID > 0 && dbestimatingItem.SubSectionID == Utilities.ConvertToInt(section.SectionID)))
				{
					string itemID = dbestimatingItem.ID.ToString();
					string description = dbestimatingItem.Description;
					double priceEach = dbestimatingItem.PriceEach;
					string purchaseUnit = dbestimatingItem.PurchaseUnit;
					CEstimatingItem cestimatingItem = new CEstimatingItem(itemID, description, priceEach, purchaseUnit, dbestimatingItem.ItemType, dbestimatingItem.UnitMeasure, dbestimatingItem.CoverageValue, dbestimatingItem.CoverageUnit, dbestimatingItem.SectionID, dbestimatingItem.SubSectionID, dbestimatingItem.BidCode, "");
					cestimatingItem.Tag = cestimatingItem;
					this.browserProducts.Add(cestimatingItem);
				}
			}
			products = this.browserProducts;
			Utilities.EnableInterface(this.browserInterface.Browser, true);
		}

		private void browserInterface_OnGetDefaultFormula(DrawObject drawObject, GroupStats stats, CEstimatingItem product, string coUnit)
		{
			this.MatchDefaultTakeoffResult(drawObject, stats, product, coUnit);
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
			if (coUnit == Resources.v_3.ToUpper() || coUnit == "M³")
			{
				return "CY";
			}
			return coUnit;
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
			string text = "";
			if ((product.UnitMeasure == DBEstimatingItem.UnitMeasureType.m || product.UnitMeasure == DBEstimatingItem.UnitMeasureType.lin_ft) && drawObject.ObjectType == "Perimeter")
			{
				text = "[" + Resources.Longueur_nette + "]";
			}
			if ((product.UnitMeasure == DBEstimatingItem.UnitMeasureType.m || product.UnitMeasure == DBEstimatingItem.UnitMeasureType.lin_ft) && drawObject.ObjectType == "Line")
			{
				text = "[" + Resources.Longueur + "]";
			}
			else if ((product.UnitMeasure == DBEstimatingItem.UnitMeasureType.sq_m || product.UnitMeasure == DBEstimatingItem.UnitMeasureType.sq_ft) && drawObject.ObjectType == "Area")
			{
				text = "[" + Resources.Surface_déductions + "]";
			}
			if (text == "")
			{
				coUnit = this.ConvertUnitToString(coUnit.Trim());
				string a;
				if ((a = coUnit) != null && !(a == "CF") && !(a == "CY"))
				{
					if (!(a == "EA"))
					{
						if (!(a == "LF"))
						{
							if (a == "SF")
							{
								if (drawObject.ObjectType == "Area")
								{
									text = "[" + Resources.Surface_déductions + "]";
								}
							}
						}
						else if (drawObject.ObjectType == "Perimeter")
						{
							text = "[" + Resources.Longueur_nette + "]";
						}
						else if (drawObject.ObjectType == "Line")
						{
							text = "[" + Resources.Longueur + "]";
						}
					}
					else if (drawObject.ObjectType == "Counter")
					{
						text = "[" + Resources.Nombre_d_objets + "]";
					}
				}
			}
			if (text != "")
			{
				product.Formula = text;
			}
		}

		public TreeViewNodes TreeItems;

		public DBEstimatingItems EstimatingItems;

		public DBEstimatingSections CSICodesA;

		public DBEstimatingSections CSICodesB;

		private CEstimatingItemsBrowserInterface browserInterface;

		private TreeViewNodes eEstimatingSections;

		private CEstimatingItems browserProducts = new CEstimatingItems();

		private enum ParsingContext
		{
			NextAvailableIndex,
			Products,
			CSICodesA,
			CSICodesB
		}
	}
}
