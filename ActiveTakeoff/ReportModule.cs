using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Microsoft.Win32;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class ReportModule
	{
		public bool ExportReportToMemory
		{
			[CompilerGenerated]
			get
			{
				return this.<ExportReportToMemory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExportReportToMemory>k__BackingField = value;
			}
		}

		public bool SortByLayers
		{
			[CompilerGenerated]
			get
			{
				return this.<SortByLayers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SortByLayers>k__BackingField = value;
			}
		}

		public bool ExportEstimatingItems
		{
			[CompilerGenerated]
			get
			{
				return this.<ExportEstimatingItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExportEstimatingItems>k__BackingField = value;
			}
		}

		public string ReportSummaries
		{
			[CompilerGenerated]
			get
			{
				return this.<ReportSummaries>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReportSummaries>k__BackingField = value;
			}
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

		private void LoadResources()
		{
		}

		private void InitializeWebBrowser()
		{
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

		private void IESetupHeaderFooter(string dateString)
		{
			try
			{
				bool writable = true;
				string name = "header";
				string name2 = "footer";
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", writable);
				registryKey.SetValue(name, Resources.Page_x_de_n);
				registryKey.SetValue(name2, Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan + dateString);
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
				bool writable = true;
				string name = "header";
				string name2 = "footer";
				string name3 = "Print_Background";
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", writable);
				this.registerHeaderOriginalValue = registryKey.GetValue(name, "");
				registryKey.SetValue(name, "");
				this.registerFooterOriginalValue = registryKey.GetValue(name2, "");
				registryKey.SetValue(name2, "");
				this.registerPrintBackgroundOriginalValue = registryKey.GetValue(name3, "");
				registryKey.SetValue(name3, "yes");
				registryKey.Close();
			}
			catch
			{
			}
		}

		public void IERestoreRegistry()
		{
			try
			{
				bool writable = true;
				string name = "header";
				string name2 = "footer";
				string name3 = "Print_Background";
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", writable);
				registryKey.SetValue(name, this.registerHeaderOriginalValue);
				registryKey.SetValue(name2, this.registerFooterOriginalValue);
				registryKey.SetValue(name3, this.registerPrintBackgroundOriginalValue);
				registryKey.Close();
			}
			catch
			{
			}
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

		private void LoadHtmlDocument(string htmlDocument)
		{
			if (this.webBrowser.Document != null)
			{
				this.webBrowser.Document.OpenNew(true);
			}
			else
			{
				this.webBrowser.Navigate("about:blank");
			}
			while (this.webBrowser.Document == null && this.webBrowser.Document.Body == null)
			{
				Application.DoEvents();
			}
			this.webBrowser.DocumentText = htmlDocument;
		}

		private string BuildXMLProjectInfo()
		{
			string text = "";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"\t<Project Name=\"",
				Utilities.EscapeString(this.project.Name),
				"\">",
				Environment.NewLine
			});
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\t\t<ContactName>",
				Utilities.EscapeString(this.project.ContactName),
				"</ContactName>",
				Environment.NewLine
			});
			string[] fields = Utilities.GetFields(this.project.Description, new char[]
			{
				'\r',
				'\n'
			});
			if (fields.GetUpperBound(0) >= 0)
			{
				text += "\t\t<Description>";
				foreach (string s in fields)
				{
					text = text + Utilities.EscapeString(s) + "&#xD;";
				}
				text = text + "</Description>" + Environment.NewLine;
			}
			fields = Utilities.GetFields(this.project.ContactInfo, new char[]
			{
				'\r',
				'\n'
			});
			if (fields.GetUpperBound(0) >= 0)
			{
				text += "\t\t<ContactInfo>";
				foreach (string s2 in fields)
				{
					text = text + Utilities.EscapeString(s2) + "&#xD;";
				}
				text = text + "</ContactInfo>" + Environment.NewLine;
			}
			fields = Utilities.GetFields(this.project.Comment, new char[]
			{
				'\r',
				'\n'
			});
			if (fields.GetUpperBound(0) >= 0)
			{
				text += "\t\t<Comment>";
				foreach (string s3 in fields)
				{
					text = text + Utilities.EscapeString(s3) + "&#xD;";
				}
				text = text + "</Comment>" + Environment.NewLine;
			}
			return text + "\t</Project>" + Environment.NewLine;
		}

		private string BuildXMLEstimatingItemResults(DrawObject groupObject, GroupStats groupStats, UnitScale reportUnitScale, string extensionID, string resultID, string resultName, string estimatingCaption, double quantity, PresetResult presetResult, ref double costTotalSubTotal, ref double priceTotalSubTotal, string defaultUnit = "", double defaultCostEach = 0.0)
		{
			bool flag = false;
			EstimatingItemPrice estimatingItemPrice = null;
			string text = defaultUnit;
			string text2 = "";
			quantity = reportUnitScale.Round(quantity);
			if (presetResult == null && resultName != "")
			{
				string objectType;
				if ((objectType = groupObject.ObjectType) != null)
				{
					if (!(objectType == "Line"))
					{
						if (!(objectType == "Counter"))
						{
							if (!(objectType == "Area"))
							{
								if (objectType == "Perimeter")
								{
									text = ((reportUnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
									if (resultName != null)
									{
										if (!(resultName == "Length"))
										{
											if (!(resultName == "PerimeterMinusOpening"))
											{
												if (resultName == "NetLength")
												{
													flag = (groupStats.DropLength > 0.0);
												}
											}
											else
											{
												flag = (groupStats.DeductionPerimeter > 0.0 && groupStats.DropLength == 0.0);
											}
										}
										else
										{
											flag = (groupStats.DeductionPerimeter == 0.0 && groupStats.DropLength == 0.0);
										}
									}
								}
							}
							else
							{
								text = ((reportUnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²");
								if (resultName != null)
								{
									if (!(resultName == "Area"))
									{
										if (resultName == "AreaMinusDeduction")
										{
											flag = (groupStats.DeductionsCount > 0);
										}
									}
									else
									{
										flag = (groupStats.DeductionsCount == 0);
									}
								}
							}
						}
						else
						{
							text = Resources.unité_;
							flag = true;
						}
					}
					else
					{
						text = ((reportUnitScale.CurrentSystemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
						flag = true;
					}
				}
			}
			else
			{
				if (presetResult != null)
				{
					if (presetResult.ItemID != -1)
					{
						text2 = "\" ItemID=\"" + presetResult.ItemID;
					}
				}
				else
				{
					text2 = "\" ItemID=\"" + resultID;
				}
				flag = true;
			}
			double num = 0.0;
			double num2 = defaultCostEach;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			if (flag)
			{
				estimatingItemPrice = this.project.EstimatingItems.QueryEstimatingItemPrice(groupObject.GroupID, extensionID, resultID);
				if (estimatingItemPrice != null)
				{
					if (num2 == 0.0)
					{
						num2 = estimatingItemPrice.CostEach;
					}
					num = Math.Round(num2 * (1.0 + estimatingItemPrice.MarkupEach / 100.0), 2);
					num3 = Math.Round(quantity * num2, 2);
					num4 = Math.Round(quantity * num, 2);
					num5 = ((num > 0.0) ? ((num - num2) / num) : 0.0);
					costTotalSubTotal += num3;
					priceTotalSubTotal += num4;
				}
			}
			if (estimatingItemPrice == null)
			{
				return "";
			}
			return string.Concat(new object[]
			{
				"\" EstimatingItem=\"",
				true,
				(estimatingCaption != "") ? ("\" EstimatingCaption=\"" + Utilities.EscapeString(estimatingCaption)) : "",
				text2,
				"\" Quantity=\"",
				quantity,
				"\" EstimatingUnit=\"",
				text,
				"\" CostEach=\"",
				string.Format("{0:C}", num2),
				"\" RawCostEach=\"",
				num2,
				"\" MarkupEach=\"",
				string.Format("{0:C}", estimatingItemPrice.MarkupEach),
				"\" RawMarkupEach=\"",
				estimatingItemPrice.MarkupEach,
				"\" CostTotal=\"",
				string.Format("{0:C}", num3),
				"\" RawCostTotal=\"",
				num3,
				"\" PriceEach=\"",
				string.Format("{0:C}", num),
				"\" RawPriceEach=\"",
				num,
				"\" PriceTotal=\"",
				string.Format("{0:C}", num4),
				"\" RawPriceTotal=\"",
				num4,
				"\" Margin=\"",
				string.Format("{0:0.00%}", num5),
				"\" RawMargin=\"",
				num5
			});
		}

		private string BuildXMLObject(DrawObject groupObject, Plan plan, string layerName, Variables plans, List<ReportSummary> reportSummaries, ref string indent, ref int resultID, ref int resultParentID, bool includeHiddenValues = false, bool exportForExcel = false, List<EstimatingItem> results = null)
		{
			string text = "";
			bool flag = plan == null && layerName != "";
			double num = 0.0;
			double num2 = 0.0;
			if (plan != null)
			{
				indent = (true ? "\t\t\t\t" : "\t\t\t");
			}
			else
			{
				indent = ((this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByPlans) ? "\t\t\t\t" : "\t");
			}
			if (groupObject != null && (groupObject.Visible || this.project.Report.ShowInvisibleObjects))
			{
				string text2 = string.Empty;
				if (plans != null)
				{
					foreach (object obj in plans.Collection)
					{
						Variable variable = (Variable)obj;
						if (variable.Name == groupObject.GroupID.ToString())
						{
							text2 = text2 + ((text2 == "") ? "" : ", ") + Utilities.EscapeString(variable.Value.ToString());
						}
					}
				}
				List<string> list = null;
				if (!exportForExcel)
				{
					if (plan != null)
					{
						list = GroupUtilities.GetObjectLabels(plan, groupObject);
					}
					else
					{
						list = GroupUtilities.GetObjectLabels(this.project, groupObject, this.filter);
					}
				}
				else
				{
					list = new List<string>();
					list.Add(string.Empty);
				}
				for (int i = 0; i < list.Count; i++)
				{
					if (!false)
					{
						if (results == null)
						{
							object obj2 = text;
							text = string.Concat(new object[]
							{
								obj2,
								indent,
								"\t<Object Name=\"",
								Utilities.EscapeString(groupObject.Name + ((i > 0) ? (" - (" + Utilities.EscapeString(list[i]) + ")") : ((list.Count == 1) ? string.Empty : string.Empty))),
								(text2 == "") ? "" : ("\" Plans=\"" + text2),
								"\" Type=\"",
								groupObject.ObjectType,
								"\" Color=\"",
								groupObject.Color.ToArgb(),
								"\" IsLabel=\"",
								i > 0,
								"\">",
								Environment.NewLine
							});
						}
						if (this.project.Report.ShowComments && groupObject.Comment != string.Empty)
						{
							string[] fields = Utilities.GetFields(groupObject.Comment, new char[]
							{
								'\r',
								'\n'
							});
							if (fields.GetUpperBound(0) >= 0)
							{
								text += "\t\t\t<Comment>";
								foreach (string s in fields)
								{
									text = text + Utilities.EscapeString(s) + "&#xD;";
								}
								text = text + "</Comment>" + Environment.NewLine;
							}
						}
						if (results == null)
						{
							text = text + indent + "\t\t<Extensions>" + Environment.NewLine;
						}
						if (results == null)
						{
							object obj2 = text;
							text = string.Concat(new object[]
							{
								obj2,
								indent,
								"\t\t\t<Extension Name=\"",
								Resources.Résultats,
								"\" BaseExtension=\"",
								true,
								"\">",
								Environment.NewLine
							});
							text = text + indent + "\t\t\t\t<Results>" + Environment.NewLine;
						}
						num = 0.0;
						num2 = 0.0;
						UnitScale.UnitSystem systemType = this.project.Report.SystemType;
						UnitScale.UnitPrecision precision = this.project.Report.Precision;
						UnitScale unitScale = new UnitScale(1f, systemType, precision, false);
						UnitScale.UnitSystem unitSystem = this.project.EstimatingItems.QueryEstimatingItemSystemType(groupObject.GroupID, "", groupObject.ObjectType, systemType, -1);
						UnitScale unitScale2 = new UnitScale(1f, unitSystem, precision, false);
						GroupStats groupStats = null;
						if (plan != null)
						{
							groupStats = GroupUtilities.ComputeGroupStats(plan, groupObject, systemType, list.Count == 1, (list.Count > 1) ? list[i] : string.Empty);
						}
						else if (layerName != "")
						{
							groupStats = GroupUtilities.ComputeGroupStats(this.project, layerName, groupObject, this.filter, systemType, list.Count == 1, (list.Count > 1) ? list[i] : string.Empty);
						}
						GroupStats groupStats2 = GroupUtilities.ComputeGroupStats(this.project, groupObject, this.filter, systemType, list.Count == 1, (list.Count > 1) ? list[i] : string.Empty);
						bool flag2 = groupStats2.DeductionsCount > 0 || exportForExcel;
						bool flag3 = groupStats2.DropLength > 0.0 || exportForExcel;
						bool flag4 = groupStats2.DeductionPerimeter > 0.0 || exportForExcel;
						resultID++;
						resultParentID = resultID;
						string text3;
						if ((text3 = groupObject.ObjectType) != null)
						{
							if (!(text3 == "Line"))
							{
								if (!(text3 == "Area"))
								{
									if (!(text3 == "Perimeter"))
									{
										if (text3 == "Counter")
										{
											if (results == null)
											{
												object obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"Count\" Caption=\"",
													Resources.Nombre_d_objets,
													"\" Unit=\"",
													Resources.unité_,
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(this.drawArea.ToUnitString(groupStats.GroupCount)),
														"\" RawValue=\"",
														groupStats.GroupCount
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(this.drawArea.ToUnitString(groupStats2.GroupCount)),
													"\" TotalRawValue=\"",
													groupStats2.GroupCount,
													this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, "", groupObject.ObjectType, "Count", Resources.Nombre_d_objets, (double)((groupStats != null) ? groupStats.GroupCount : groupStats2.GroupCount), null, ref num, ref num2, "", 0.0),
													"\"/>",
													Environment.NewLine
												});
											}
											else
											{
												results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, (double)groupStats2.GroupCount, Resources.unité_, 0.0, 0.0, -1));
											}
										}
									}
									else
									{
										double num3 = (systemType == unitSystem) ? ((groupStats != null) ? groupStats.Perimeter : groupStats2.Perimeter) : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet((groupStats != null) ? groupStats.Perimeter : groupStats2.Perimeter) : UnitScale.FromFeetToMeters((groupStats != null) ? groupStats.Perimeter : groupStats2.Perimeter));
										if (results == null)
										{
											object obj2;
											if (!this.ExportEstimatingItems)
											{
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"Length\" Caption=\"",
													Resources.Longueur,
													"\" Unit=\"",
													(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true)),
														"\" RawValue=\"",
														groupStats.Perimeter
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.Perimeter, false, true, true)),
													"\" TotalRawValue=\"",
													groupStats2.Perimeter,
													this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale2, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStats != null) ? num3 : num3, null, ref num, ref num2, "", 0.0),
													"\"/>",
													Environment.NewLine
												});
											}
											else
											{
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"Length\" Caption=\"",
													Resources.Longueur,
													"\" Unit=\"",
													(unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(unitScale2.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true)),
														"\" RawValue=\"",
														groupStats.Perimeter
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(unitScale2.ToLengthStringFromUnitSystem(groupStats2.Perimeter, false, true, true)),
													"\" TotalRawValue=\"",
													groupStats2.Perimeter,
													this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale2, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStats != null) ? num3 : num3, null, ref num, ref num2, "", 0.0),
													"\"/>",
													Environment.NewLine
												});
											}
											if (flag4)
											{
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"Openings\" Caption=\"",
													Resources.Longueur_des_ouvertures,
													"\" Unit=\"",
													(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.DeductionPerimeter, false, true, true)),
														"\" RawValue=\"",
														groupStats.DeductionPerimeter
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.DeductionPerimeter, false, true, true)),
													"\" TotalRawValue=\"",
													groupStats2.DeductionPerimeter,
													"\"/>",
													Environment.NewLine
												});
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"PerimeterMinusOpenings\" Caption=\"",
													Resources.Longueur_sans_ouvertures,
													"\" Unit=\"",
													(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.PerimeterMinusOpening, false, true, true)),
														"\" RawValue=\"",
														groupStats.PerimeterMinusOpening
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.PerimeterMinusOpening, false, true, true)),
													"\" TotalRawValue=\"",
													groupStats2.PerimeterMinusOpening,
													this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, "", groupObject.ObjectType, "PerimeterMinusOpening", Resources.Longueur, (groupStats != null) ? groupStats.PerimeterMinusOpening : groupStats2.PerimeterMinusOpening, null, ref num, ref num2, "", 0.0),
													"\"/>",
													Environment.NewLine
												});
											}
											if (flag3)
											{
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"DropLength\" Caption=\"",
													Resources.Drop_length,
													"\" Unit=\"",
													(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.DropLength, false, true, true)),
														"\" RawValue=\"",
														groupStats.DropLength
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.DropLength, false, true, true)),
													"\" TotalRawValue=\"",
													groupStats2.DropLength,
													"\"/>",
													Environment.NewLine
												});
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"NetLength\" Caption=\"",
													Resources.Longueur_nette,
													"\" Unit=\"",
													(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.NetLength, false, true, true)),
														"\" RawValue=\"",
														groupStats.NetLength
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.NetLength, false, true, true)),
													"\" TotalRawValue=\"",
													groupStats2.NetLength,
													this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, "", groupObject.ObjectType, "NetLength", Resources.Longueur, (groupStats != null) ? groupStats.NetLength : groupStats2.NetLength, null, ref num, ref num2, "", 0.0),
													"\"/>",
													Environment.NewLine
												});
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"DropsCount\" Caption=\"",
													Resources.Drops_count,
													"\" Unit=\"",
													Resources.unité_,
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(this.drawArea.ToUnitString(groupStats.DropsCount)),
														"\" RawValue=\"",
														groupStats.DropsCount
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(this.drawArea.ToUnitString(groupStats2.DropsCount)),
													"\" TotalRawValue=\"",
													groupStats2.DropsCount,
													"\"/>",
													Environment.NewLine
												});
											}
											if (flag2)
											{
												obj2 = text;
												text = string.Concat(new object[]
												{
													obj2,
													indent,
													"\t\t\t\t\t<Result Name=\"OpeningsCount\" Caption=\"",
													Resources.Nombre_d_ouvertures,
													"\" Unit=\"",
													Resources.unité_,
													(groupStats != null) ? string.Concat(new object[]
													{
														"\" Value=\"",
														Utilities.EscapeString(this.drawArea.ToUnitString(groupStats.DeductionsCount)),
														"\" RawValue=\"",
														groupStats.DeductionsCount
													}) : "",
													"\" TotalValue=\"",
													Utilities.EscapeString(this.drawArea.ToUnitString(groupStats2.DeductionsCount)),
													"\" TotalRawValue=\"",
													groupStats2.DeductionsCount,
													"\"/>",
													Environment.NewLine
												});
											}
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"CornersCount\" Caption=\"",
												Resources.Nombre_de_coins,
												"\" Unit=\"",
												Resources.unité_,
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(this.drawArea.ToUnitString(groupStats.CornersCount)),
													"\" RawValue=\"",
													groupStats.CornersCount
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(this.drawArea.ToUnitString(groupStats2.CornersCount)),
												"\" TotalRawValue=\"",
												groupStats2.CornersCount,
												"\"/>",
												Environment.NewLine
											});
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"EndsCount\" Caption=\"",
												Resources.Nombre_de_bouts,
												"\" Unit=\"",
												Resources.unité_,
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(this.drawArea.ToUnitString(groupStats.EndsCount)),
													"\" RawValue=\"",
													groupStats.EndsCount
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(this.drawArea.ToUnitString(groupStats2.EndsCount)),
												"\" TotalRawValue=\"",
												groupStats2.EndsCount,
												"\"/>",
												Environment.NewLine
											});
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"SegmentsCount\" Caption=\"",
												Resources.Nombre_de_segments,
												"\" Unit=\"",
												Resources.unité_,
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(this.drawArea.ToUnitString(groupStats.SegmentsCount)),
													"\" RawValue=\"",
													groupStats.SegmentsCount
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(this.drawArea.ToUnitString(groupStats2.SegmentsCount)),
												"\" TotalRawValue=\"",
												groupStats2.SegmentsCount,
												"\"/>",
												Environment.NewLine
											});
										}
										else
										{
											results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, num3, (unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m", 0.0, 0.0, -1));
										}
									}
								}
								else
								{
									double num4 = (systemType == unitSystem) ? ((groupStats != null) ? groupStats.AreaMinusDeduction : groupStats2.AreaMinusDeduction) : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet((groupStats != null) ? groupStats.AreaMinusDeduction : groupStats2.AreaMinusDeduction) : UnitScale.FromSquareFeetToSquareMeters((groupStats != null) ? groupStats.AreaMinusDeduction : groupStats2.AreaMinusDeduction));
									if (results == null)
									{
										object obj2;
										if (!this.ExportEstimatingItems)
										{
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"Area\" Caption=\"",
												Resources.Surface,
												"\" Unit=\"",
												(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²",
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStats.Area, true)),
													"\" RawValue=\"",
													groupStats.Area
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStats2.Area, true)),
												"\" TotalRawValue=\"",
												groupStats2.Area,
												this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale2, "", groupObject.ObjectType, "Area", Resources.Surface, (groupStats != null) ? num4 : num4, null, ref num, ref num2, "", 0.0),
												"\"/>",
												Environment.NewLine
											});
										}
										else
										{
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"Area\" Caption=\"",
												Resources.Surface,
												"\" Unit=\"",
												(unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²",
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(unitScale2.ToAreaStringFromUnitSystem(groupStats.Area, true)),
													"\" RawValue=\"",
													groupStats.Area
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(unitScale2.ToAreaStringFromUnitSystem(groupStats2.Area, true)),
												"\" TotalRawValue=\"",
												groupStats2.Area,
												this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale2, "", groupObject.ObjectType, "Area", Resources.Surface, (groupStats != null) ? num4 : num4, null, ref num, ref num2, "", 0.0),
												"\"/>",
												Environment.NewLine
											});
										}
										if (flag2)
										{
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"Substractions\" Caption=\"",
												Resources.Déduction,
												"\" Unit=\"",
												(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²",
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStats.DeductionArea, true)),
													"\" RawValue=\"",
													groupStats.DeductionArea
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStats2.DeductionArea, true)),
												"\" TotalRawValue=\"",
												groupStats2.DeductionArea,
												"\"/>",
												Environment.NewLine
											});
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"AreaMinusSubstractions\" Caption=\"",
												Resources.Surface_déductions,
												"\" Unit=\"",
												(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²",
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStats.AreaMinusDeduction, true)),
													"\" RawValue=\"",
													groupStats.AreaMinusDeduction
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(unitScale.ToAreaStringFromUnitSystem(groupStats2.AreaMinusDeduction, true)),
												"\" TotalRawValue=\"",
												groupStats2.AreaMinusDeduction,
												this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, "", groupObject.ObjectType, "AreaMinusDeduction", Resources.Surface, (groupStats != null) ? groupStats.AreaMinusDeduction : groupStats2.AreaMinusDeduction, null, ref num, ref num2, "", 0.0),
												"\"/>",
												Environment.NewLine
											});
										}
										obj2 = text;
										text = string.Concat(new object[]
										{
											obj2,
											indent,
											"\t\t\t\t\t<Result Name=\"Perimeter\" Caption=\"",
											Resources.Périmètre,
											"\" Unit=\"",
											(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
											(groupStats != null) ? string.Concat(new object[]
											{
												"\" Value=\"",
												Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true)),
												"\" RawValue=\"",
												groupStats.Perimeter
											}) : "",
											"\" TotalValue=\"",
											Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.Perimeter, false, true, true)),
											"\" TotalRawValue=\"",
											groupStats2.Perimeter,
											"\"/>",
											Environment.NewLine
										});
										if (flag2)
										{
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"PerimeterPlusSubstractions\" Caption=\"",
												Resources.Périmètre_déductions,
												"\" Unit=\"",
												(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.PerimeterPlusDeduction, false, true, true)),
													"\" RawValue=\"",
													groupStats.PerimeterPlusDeduction
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.PerimeterPlusDeduction, false, true, true)),
												"\" TotalRawValue=\"",
												groupStats2.PerimeterPlusDeduction,
												"\"/>",
												Environment.NewLine
											});
											obj2 = text;
											text = string.Concat(new object[]
											{
												obj2,
												indent,
												"\t\t\t\t\t<Result Name=\"SubstractionsCount\" Caption=\"",
												Resources.Nombre_de_déductions,
												"\" Unit=\"",
												Resources.unité_,
												(groupStats != null) ? string.Concat(new object[]
												{
													"\" Value=\"",
													Utilities.EscapeString(this.drawArea.ToUnitString(groupStats.DeductionsCount)),
													"\" RawValue=\"",
													groupStats.DeductionsCount
												}) : "",
												"\" TotalValue=\"",
												Utilities.EscapeString(this.drawArea.ToUnitString(groupStats2.DeductionsCount)),
												"\" TotalRawValue=\"",
												groupStats2.DeductionsCount,
												"\"/>",
												Environment.NewLine
											});
										}
									}
									else
									{
										results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, num4, (unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²", 0.0, 0.0, -1));
									}
								}
							}
							else
							{
								double num5 = (systemType == unitSystem) ? ((groupStats != null) ? groupStats.Perimeter : groupStats2.Perimeter) : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet((groupStats != null) ? groupStats.Perimeter : groupStats2.Perimeter) : UnitScale.FromFeetToMeters((groupStats != null) ? groupStats.Perimeter : groupStats2.Perimeter));
								if (results == null)
								{
									if (!this.ExportEstimatingItems)
									{
										object obj2 = text;
										text = string.Concat(new object[]
										{
											obj2,
											indent,
											"\t\t\t\t\t<Result Name=\"Length\" Caption=\"",
											Resources.Longueur,
											"\" Unit=\"",
											(systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
											(groupStats != null || flag) ? string.Concat(new object[]
											{
												"\" Value=\"",
												Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true)),
												"\" RawValue=\"",
												groupStats.Perimeter
											}) : "",
											"\" TotalValue=\"",
											Utilities.EscapeString(unitScale.ToLengthStringFromUnitSystem(groupStats2.Perimeter, false, true, true)),
											"\" TotalRawValue=\"",
											groupStats2.Perimeter,
											this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale2, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStats != null) ? num5 : num5, null, ref num, ref num2, "", 0.0),
											"\"/>",
											Environment.NewLine
										});
									}
									else
									{
										object obj2 = text;
										text = string.Concat(new object[]
										{
											obj2,
											indent,
											"\t\t\t\t\t<Result Name=\"Length\" Caption=\"",
											Resources.Longueur,
											"\" Unit=\"",
											(unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m",
											(groupStats != null || flag) ? string.Concat(new object[]
											{
												"\" Value=\"",
												Utilities.EscapeString(unitScale2.ToLengthStringFromUnitSystem(groupStats.Perimeter, false, true, true)),
												"\" RawValue=\"",
												groupStats.Perimeter
											}) : "",
											"\" TotalValue=\"",
											Utilities.EscapeString(unitScale2.ToLengthStringFromUnitSystem(groupStats2.Perimeter, false, true, true)),
											"\" TotalRawValue=\"",
											groupStats2.Perimeter,
											this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale2, "", groupObject.ObjectType, "Length", Resources.Longueur, (groupStats != null) ? num5 : num5, null, ref num, ref num2, "", 0.0),
											"\"/>",
											Environment.NewLine
										});
									}
								}
								else
								{
									results.Add(new EstimatingItem(resultID, 0, groupObject.GroupID, "", groupObject.ObjectType, groupObject.ObjectType, groupObject.Name, num5, (unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m", 0.0, 0.0, -1));
								}
							}
						}
						if (results == null)
						{
							text = text + indent + "\t\t\t\t</Results>" + Environment.NewLine;
							text = text + indent + "\t\t\t</Extension>" + Environment.NewLine;
						}
						DrawObjectGroup group = groupObject.Group;
						Presets presets = new Presets();
						if (group != null)
						{
							foreach (object obj3 in group.Presets.Collection)
							{
								Preset preset = (Preset)obj3;
								Preset preset2 = preset.Clone(true);
								presets.Add(preset2);
							}
							foreach (object obj4 in group.Presets.Collection)
							{
								Preset preset3 = (Preset)obj4;
								if (results == null)
								{
									text3 = text;
									text = string.Concat(new string[]
									{
										text3,
										indent,
										"\t\t\t<Extension Name=\"",
										Utilities.EscapeString(preset3.DisplayName),
										"\">",
										Environment.NewLine
									});
								}
								if (i == 0)
								{
									string text4 = "";
									int num6 = 0;
									foreach (object obj5 in preset3.Choices.Collection)
									{
										PresetChoice presetChoice = (PresetChoice)obj5;
										ExtensionChoice extensionChoice = this.extensionSupport.FindChoice(preset3.CategoryName, preset3.ExtensionName, presetChoice.ChoiceName);
										if (extensionChoice != null && (!extensionChoice.Hidden || includeHiddenValues))
										{
											ExtensionChoiceElement extensionChoiceElement = extensionChoice.FindElement(presetChoice.ChoiceElementName);
											if (extensionChoiceElement != null && results == null)
											{
												text3 = text4;
												text4 = string.Concat(new string[]
												{
													text3,
													indent,
													"\t\t\t\t\t<Field Name=\"Choice",
													(num6 + 1).ToString(),
													"\" Caption=\"",
													presetChoice.ChoiceCaption,
													"\" Value=\"",
													Utilities.EscapeString(extensionChoiceElement.Caption),
													"\" RawValue=\"",
													Utilities.EscapeString(extensionChoiceElement.Caption),
													"\"/>",
													Environment.NewLine
												});
											}
										}
										num6++;
									}
									num6 = 0;
									foreach (object obj6 in preset3.Fields.Collection)
									{
										PresetField presetField = (PresetField)obj6;
										double value = Utilities.ConvertToDouble(presetField.Value.ToString(), -1);
										string s2 = value.ToString();
										string s3 = value.ToString();
										string text5 = string.Empty;
										ExtensionField.ExtensionFieldTypeEnum fieldType = presetField.FieldType;
										if (fieldType != ExtensionField.ExtensionFieldTypeEnum.FieldTypeDimension)
										{
											if (fieldType == ExtensionField.ExtensionFieldTypeEnum.FieldTypeCurrency)
											{
												s2 = this.drawArea.ToCurrency(value);
												s3 = value.ToString();
												text5 = Utilities.GetCurrencySymbol();
											}
										}
										else
										{
											if (preset3.ScaleSystemType != unitScale.ScaleSystemType)
											{
												value = ((unitScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(value) : UnitScale.FromFeetToMeters(value));
											}
											s2 = unitScale.ToLengthStringFromUnitSystem(value, false, false, true);
											s3 = unitScale.ToLengthFromUnitSystem(value).ToString();
											text5 = ((systemType == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
										}
										if (results == null)
										{
											text3 = text4;
											text4 = string.Concat(new string[]
											{
												text3,
												indent,
												"\t\t\t\t\t<Field Name=\"Field",
												(num6 + 1).ToString(),
												"\" Caption=\"",
												Utilities.EscapeString(presetField.Caption),
												"\" Unit=\"",
												text5,
												"\" Value=\"",
												Utilities.EscapeString(s2),
												"\" RawValue=\"",
												Utilities.EscapeString(s3),
												"\"/>",
												Environment.NewLine
											});
										}
										num6++;
									}
									if (results == null && text4.Trim() != "")
									{
										text = text + indent + "\t\t\t\t<Fields>" + Environment.NewLine;
										text += text4;
										text = text + indent + "\t\t\t\t</Fields>" + Environment.NewLine;
									}
								}
								if (results == null)
								{
									text = text + indent + "\t\t\t\t<Results>" + Environment.NewLine;
								}
								Variables variables = new Variables();
								for (int k = (plan != null || layerName != "") ? 2 : 1; k > 0; k--)
								{
									Preset preset4 = (k == 2) ? presets.FindById(preset3.ID) : preset3;
									preset4 = ((preset4 == null) ? preset3 : preset4);
									this.extensionSupport.QueryPresetResults(preset4, (k == 2) ? groupStats : groupStats2, unitScale);
									foreach (object obj7 in preset4.Results.Collection)
									{
										PresetResult presetResult = (PresetResult)obj7;
										if (presetResult.ConditionMet)
										{
											unitSystem = this.project.EstimatingItems.QueryEstimatingItemSystemType(groupObject.GroupID, preset3.ID, presetResult.Name, systemType, -1);
											unitScale2 = new UnitScale(1f, unitSystem, precision, false);
											double num7 = Utilities.ConvertToDouble(presetResult.Result.ToString(), -1);
											string text6 = string.Empty;
											string text7 = string.Empty;
											string text8 = string.Empty;
											double defaultCostEach = 0.0;
											DBEstimatingItem dbestimatingItem = null;
											if (presetResult.ItemID != -1)
											{
												dbestimatingItem = this.project.DBManagement.GetEstimatingItem(presetResult.ItemID);
											}
											bool flag5 = false;
											if (dbestimatingItem != null)
											{
												flag5 = dbestimatingItem.MatchResultType(presetResult.ResultType);
											}
											if (flag5)
											{
												UnitScale unitScale3 = new UnitScale(1f, (dbestimatingItem.PurchaseUnit == "m" || dbestimatingItem.PurchaseUnit == "m²" || dbestimatingItem.PurchaseUnit == "m³") ? UnitScale.UnitSystem.metric : UnitScale.UnitSystem.imperial, precision, false);
												unitSystem = unitScale3.ScaleSystemType;
												switch (presetResult.ResultType)
												{
												case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
													num7 = ((systemType == unitSystem) ? num7 : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num7) : UnitScale.FromFeetToMeters(num7)));
													text6 = unitScale3.ToLengthStringFromUnitSystem(num7, false, true, true);
													text7 = unitScale3.ToLengthFromUnitSystem(num7).ToString();
													text8 = dbestimatingItem.PurchaseUnit;
													break;
												case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
													num7 = ((systemType == unitSystem) ? num7 : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(num7) : UnitScale.FromSquareFeetToSquareMeters(num7)));
													text6 = unitScale3.ToAreaStringFromUnitSystem(num7, true);
													text7 = unitScale3.ToAreaFromUnitSystem(num7).ToString();
													text8 = dbestimatingItem.PurchaseUnit;
													break;
												case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
													num7 = ((systemType == unitSystem) ? num7 : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromCubicMetersToCubicFeet(num7) : UnitScale.FromCubicFeetToCubicMeters(num7)));
													text6 = unitScale3.ToCubicStringFromUnitSystem(num7, true);
													text7 = unitScale3.ToCubicFromUnitSystem(num7).ToString();
													text8 = dbestimatingItem.PurchaseUnit;
													break;
												default:
													text6 = unitScale3.Round(num7).ToString();
													text7 = text6.ToString();
													text8 = dbestimatingItem.PurchaseUnit;
													text6 += ((text8 != string.Empty) ? (" " + text8) : "");
													break;
												}
											}
											else
											{
												switch (presetResult.ResultType)
												{
												case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
													num7 = ((systemType == unitSystem) ? num7 : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num7) : UnitScale.FromFeetToMeters(num7)));
													text6 = unitScale2.ToLengthStringFromUnitSystem(num7, false, true, true);
													text7 = unitScale2.ToLengthFromUnitSystem(num7).ToString();
													text8 = ((unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi : "m");
													break;
												case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
													num7 = ((systemType == unitSystem) ? num7 : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromSquareMetersToSquareFeet(num7) : UnitScale.FromSquareFeetToSquareMeters(num7)));
													text6 = unitScale2.ToAreaStringFromUnitSystem(num7, true);
													text7 = unitScale2.ToAreaFromUnitSystem(num7).ToString();
													text8 = ((unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi_2 : "m²");
													break;
												case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
													num7 = ((systemType == unitSystem) ? num7 : ((unitSystem == UnitScale.UnitSystem.imperial) ? UnitScale.FromCubicMetersToCubicFeet(num7) : UnitScale.FromCubicFeetToCubicMeters(num7)));
													text6 = unitScale2.ToCubicStringFromUnitSystem(num7, true);
													text7 = unitScale2.ToCubicFromUnitSystem(num7).ToString();
													text8 = ((unitSystem == UnitScale.UnitSystem.imperial) ? Resources.pi_3 : "m³");
													break;
												case ExtensionResult.ExtensionResultTypeEnum.ResultTypeCurrency:
													text6 = this.drawArea.ToCurrency(num7);
													defaultCostEach = Utilities.ConvertToDouble(num7, -1);
													text7 = "1";
													text8 = Resources.chaque;
													break;
												default:
													text6 = unitScale2.Round(num7).ToString();
													text7 = text6.ToString();
													text8 = presetResult.Unit.ToLower();
													text6 += ((text8 != string.Empty) ? (" " + text8) : "");
													break;
												}
											}
											if (presetResult.IsEstimatingItem || !this.ExportEstimatingItems)
											{
												string text9 = (dbestimatingItem != null) ? dbestimatingItem.Description : presetResult.Caption;
												int num8 = (dbestimatingItem != null) ? dbestimatingItem.SectionID : presetResult.SectionID;
												int num9 = (dbestimatingItem != null) ? dbestimatingItem.SubSectionID : presetResult.SubSectionID;
												string s4 = (dbestimatingItem != null) ? dbestimatingItem.BidCode : "";
												DBEstimatingItem.EstimatingItemType estimatingItemType = (dbestimatingItem != null) ? dbestimatingItem.ItemType : DBEstimatingItem.EstimatingItemType.MaterialItem;
												if (results == null)
												{
													Variable variable2 = variables.Find(presetResult.Name);
													if (variable2 != null)
													{
														Variable variable3 = variable2;
														variable3.Tag += ((k == 2) ? string.Concat(new string[]
														{
															" Value=\"",
															Utilities.EscapeString(text6),
															"\" RawValue=\"",
															text7,
															this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, preset3.ID, presetResult.Name, presetResult.Name, text9, Utilities.ConvertToDouble(text7, -1), presetResult, ref num, ref num2, text8, defaultCostEach),
															"\""
														}) : string.Concat(new object[]
														{
															" TotalValue=\"",
															Utilities.EscapeString(text6),
															"\" TotalRawValue=\"",
															text7,
															(groupStats == null) ? this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, preset3.ID, presetResult.Name, presetResult.Name, text9, Utilities.ConvertToDouble(text7, -1), presetResult, ref num, ref num2, text8, defaultCostEach) : "",
															"\" SectionID=\"",
															num8,
															"\" SubSectionID=\"",
															num9,
															"\" BidCode=\"",
															Utilities.EscapeString(s4),
															"\" ItemType=\"",
															(int)estimatingItemType,
															"\""
														}));
													}
													else
													{
														variable2 = new Variable(presetResult.Name, string.Concat(new string[]
														{
															indent,
															"\t\t\t\t\t<Result Name=\"",
															presetResult.Name,
															"\" Caption=\"",
															Utilities.EscapeString(text9),
															"\" Unit=\"",
															text8,
															"\""
														}), (k == 2) ? string.Concat(new string[]
														{
															" Value=\"",
															Utilities.EscapeString(text6),
															"\" RawValue=\"",
															text7,
															this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, preset3.ID, presetResult.Name, presetResult.Name, text9, Utilities.ConvertToDouble(text7, -1), presetResult, ref num, ref num2, text8, defaultCostEach),
															"\""
														}) : string.Concat(new object[]
														{
															" TotalValue=\"",
															Utilities.EscapeString(text6),
															"\" TotalRawValue=\"",
															text7,
															(groupStats == null) ? this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, preset3.ID, presetResult.Name, presetResult.Name, text9, Utilities.ConvertToDouble(text7, -1), presetResult, ref num, ref num2, text8, defaultCostEach) : "",
															"\" SectionID=\"",
															num8,
															"\" SubSectionID=\"",
															num9,
															"\" BidCode=\"",
															Utilities.EscapeString(s4),
															"\" ItemType=\"",
															(int)estimatingItemType,
															"\""
														}));
														variables.Add(variable2);
													}
												}
												else
												{
													resultID++;
													if (presetResult.IsEstimatingItem)
													{
														results.Add(new EstimatingItem(resultID, resultParentID, groupObject.GroupID, preset3.ID, presetResult.Name, groupObject.ObjectType, text9, Utilities.ConvertToDouble(text7, -1), (text8 != string.Empty) ? text8 : presetResult.Unit, 0.0, 0.0, -1));
													}
												}
											}
										}
									}
								}
								foreach (object obj8 in variables.Collection)
								{
									Variable variable4 = (Variable)obj8;
									if (results == null)
									{
										text3 = text;
										text = string.Concat(new string[]
										{
											text3,
											variable4.Value.ToString(),
											variable4.Tag.ToString(),
											"/>",
											Environment.NewLine
										});
									}
								}
								variables.Clear();
								variables = null;
								if (results == null)
								{
									text = text + indent + "\t\t\t\t</Results>" + Environment.NewLine;
									text = text + indent + "\t\t\t</Extension>" + Environment.NewLine;
								}
							}
						}
						if (results == null)
						{
							if (group.EstimatingItems.Count > 0)
							{
								text3 = text;
								text = string.Concat(new string[]
								{
									text3,
									indent,
									"\t\t\t<Extension Name=\"",
									Utilities.EscapeString(Resources.Items_d_estimation),
									"\">",
									Environment.NewLine
								});
								text = text + indent + "\t\t\t\t<Results>" + Environment.NewLine;
								Variables variables2 = new Variables();
								for (int l = (plan != null || layerName != "") ? 2 : 1; l > 0; l--)
								{
									foreach (CEstimatingItem cestimatingItem in group.EstimatingItems.Collection)
									{
										if (cestimatingItem.Formula != "")
										{
											DBEstimatingItem estimatingItem = this.project.DBManagement.GetEstimatingItem(Utilities.ConvertToInt(cestimatingItem.ItemID));
											UnitScale.UnitSystem unitSystem2 = this.project.EstimatingItems.QueryEstimatingItemSystemType(groupObject.GroupID, cestimatingItem.InternalKey, cestimatingItem.ItemID, systemType, Utilities.ConvertToInt(cestimatingItem.ItemID));
											DBEstimatingItem.UnitMeasureType unitMeasureType = (estimatingItem != null) ? estimatingItem.UnitMeasure : cestimatingItem.UnitMeasure;
											double num10 = (estimatingItem != null) ? estimatingItem.CoverageRate : cestimatingItem.CoverageRate;
											if (unitMeasureType == DBEstimatingItem.UnitMeasureType.m || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_m || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_m || cestimatingItem.Unit.ToUpper() == "M" || cestimatingItem.Unit.ToUpper() == "M²" || cestimatingItem.Unit.ToUpper() == "M³")
											{
												unitSystem2 = UnitScale.UnitSystem.metric;
											}
											else if (unitMeasureType == DBEstimatingItem.UnitMeasureType.lin_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.sq_ft || unitMeasureType == DBEstimatingItem.UnitMeasureType.cu_yd || cestimatingItem.Unit.ToUpper() == Resources.pi.ToUpper() || cestimatingItem.Unit.ToUpper() == Resources.pi_2.ToUpper() || cestimatingItem.Unit.ToUpper() == Resources.pi_3.ToUpper() || cestimatingItem.Unit.ToUpper() == Resources.v_3.ToUpper())
											{
												unitSystem2 = UnitScale.UnitSystem.imperial;
											}
											GroupStats groupStats3 = null;
											if (plan != null)
											{
												groupStats3 = GroupUtilities.ComputeGroupStats(plan, groupObject, unitSystem2, list.Count == 1, (list.Count > 1) ? list[i] : string.Empty);
											}
											else if (layerName != "")
											{
												groupStats3 = GroupUtilities.ComputeGroupStats(this.project, layerName, groupObject, this.filter, unitSystem2, list.Count == 1, (list.Count > 1) ? list[i] : string.Empty);
											}
											GroupStats groupStats4 = GroupUtilities.ComputeGroupStats(this.project, groupObject, this.filter, unitSystem2, list.Count == 1, (list.Count > 1) ? list[i] : string.Empty);
											foreach (object obj9 in groupObject.Group.Presets.Collection)
											{
												Preset preset5 = (Preset)obj9;
												UnitScale unitScale4 = new UnitScale(1f, unitSystem2, precision, false);
												this.project.ExtensionsSupport.QueryPresetResults(preset5, groupStats4, unitScale4);
											}
											double num11 = 0.0;
											if (FormulaUtilities.Compute(cestimatingItem.Formula, (l == 2) ? presets : groupObject.Group.Presets, (l == 2) ? groupStats3 : groupStats4, cestimatingItem.ResultSystemType(unitSystem2), ref num11))
											{
												num11 = ((num10 == 0.0) ? num11 : Math.Ceiling(num11 / num10));
												string text10 = unitScale.Round(num11).ToString();
												string text11 = text10.ToString();
												string text12 = cestimatingItem.Unit.ToLower();
												double defaultCostEach2 = 0.0;
												int sectionID = cestimatingItem.SectionID;
												int subSectionID = cestimatingItem.SubSectionID;
												string bidCode = cestimatingItem.BidCode;
												DBEstimatingItem.EstimatingItemType itemType = cestimatingItem.ItemType;
												text10 = text10 + " " + text12;
												Variable variable5 = variables2.Find(cestimatingItem.InternalKey);
												if (variable5 != null)
												{
													Variable variable6 = variable5;
													variable6.Tag += ((l == 2) ? string.Concat(new string[]
													{
														" Value=\"",
														Utilities.EscapeString(text10),
														"\" RawValue=\"",
														text11,
														this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, cestimatingItem.InternalKey, cestimatingItem.ItemID, "", Utilities.EscapeString(cestimatingItem.Description), Utilities.ConvertToDouble(text11, -1), null, ref num, ref num2, text12, defaultCostEach2),
														"\""
													}) : string.Concat(new object[]
													{
														" TotalValue=\"",
														Utilities.EscapeString(text10),
														"\" TotalRawValue=\"",
														text11,
														(groupStats == null) ? this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, cestimatingItem.InternalKey, cestimatingItem.ItemID, "", cestimatingItem.Description, Utilities.ConvertToDouble(text11, -1), null, ref num, ref num2, text12, defaultCostEach2) : "",
														"\" SectionID=\"",
														sectionID,
														"\" SubSectionID=\"",
														subSectionID,
														"\" BidCode=\"",
														Utilities.EscapeString(bidCode),
														"\" ItemType=\"",
														(int)itemType,
														"\""
													}));
												}
												else
												{
													variable5 = new Variable(cestimatingItem.InternalKey, string.Concat(new string[]
													{
														indent,
														"\t\t\t\t\t<Result Name=\"",
														cestimatingItem.InternalKey,
														"\" Caption=\"",
														Utilities.EscapeString(cestimatingItem.Description),
														"\" Unit=\"",
														text12,
														"\""
													}), (l == 2) ? string.Concat(new string[]
													{
														" Value=\"",
														Utilities.EscapeString(text10),
														"\" RawValue=\"",
														text11,
														this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, cestimatingItem.InternalKey, cestimatingItem.ItemID, "", cestimatingItem.Description, Utilities.ConvertToDouble(text11, -1), null, ref num, ref num2, text12, defaultCostEach2),
														"\""
													}) : string.Concat(new object[]
													{
														" TotalValue=\"",
														Utilities.EscapeString(text10),
														"\" TotalRawValue=\"",
														text11,
														(groupStats == null) ? this.BuildXMLEstimatingItemResults(groupObject, (groupStats == null) ? groupStats2 : groupStats, unitScale, cestimatingItem.InternalKey, cestimatingItem.ItemID, "", cestimatingItem.Description, Utilities.ConvertToDouble(text11, -1), null, ref num, ref num2, text12, defaultCostEach2) : "",
														"\" SectionID=\"",
														sectionID,
														"\" SubSectionID=\"",
														subSectionID,
														"\" BidCode=\"",
														Utilities.EscapeString(bidCode),
														"\" ItemType=\"",
														(int)itemType,
														"\""
													}));
													variables2.Add(variable5);
												}
											}
										}
									}
								}
								foreach (object obj10 in variables2.Collection)
								{
									Variable variable7 = (Variable)obj10;
									if (results == null)
									{
										text3 = text;
										text = string.Concat(new string[]
										{
											text3,
											variable7.Value.ToString(),
											variable7.Tag.ToString(),
											"/>",
											Environment.NewLine
										});
									}
								}
								variables2.Clear();
								variables2 = null;
								text = text + indent + "\t\t\t\t</Results>" + Environment.NewLine;
								text = text + indent + "\t\t\t</Extension>" + Environment.NewLine;
							}
							text = text + indent + "\t\t</Extensions>" + Environment.NewLine;
							if (this.ExportToCOffice && i == 0)
							{
								string text13;
								if (plan == null && !flag)
								{
									text13 = Resources.TOUS_LES_PLANS[0].ToString() + Resources.TOUS_LES_PLANS.Substring(1, Resources.TOUS_LES_PLANS.Length - 1).ToLower();
								}
								else if (flag)
								{
									text13 = layerName;
								}
								else
								{
									text13 = plan.Name;
								}
								text = text + indent + "\t\t<COffice>" + Environment.NewLine;
								if (groupObject.Group != null)
								{
									foreach (CEstimatingItem cestimatingItem2 in groupObject.Group.COfficeProducts.Collection)
									{
										double num12 = 0.0;
										FormulaUtilities.Compute(cestimatingItem2.Formula, groupObject.Group.Presets, (plan == null && !flag) ? groupStats2 : groupStats, cestimatingItem2.ResultSystemType(UnitScale.DefaultUnitSystem()), ref num12);
										text = text + indent + "\t\t\t<CEstimatingItem ";
										text = text + "Page=\"" + text13 + "\" ";
										text = text + "DigitizerItem=\"" + groupObject.Name + "\" ";
										text = text + "ItemID=\"" + cestimatingItem2.ItemID + "\" ";
										text = text + "Description=\"" + Utilities.EscapeString(cestimatingItem2.Description) + "\" ";
										object obj2 = text;
										text = string.Concat(new object[]
										{
											obj2,
											"Cost=\"",
											cestimatingItem2.Value,
											"\" "
										});
										text = text + "Unit=\"" + Utilities.EscapeString(cestimatingItem2.Unit) + "\" ";
										text = text + "Formula=\"" + Utilities.EscapeString(cestimatingItem2.Formula) + "\" ";
										obj2 = text;
										text = string.Concat(new object[]
										{
											obj2,
											"Result=\"",
											num12,
											"\""
										});
										text = text + "/>" + Environment.NewLine;
										if (this.CEstimatingItems != null)
										{
											CEstimatingItem cestimatingItem3 = new CEstimatingItem(cestimatingItem2.ItemID.Replace(',', '.'), cestimatingItem2.Description, 0.0, cestimatingItem2.Unit, cestimatingItem2.ItemType, cestimatingItem2.UnitMeasure, cestimatingItem2.CoverageValue, cestimatingItem2.CoverageUnit, cestimatingItem2.SectionID, cestimatingItem2.SubSectionID, cestimatingItem2.BidCode, "");
											cestimatingItem3.Tag = new Variable(text13, groupObject.Name, num12.ToString().Replace(',', '.'));
											this.CEstimatingItems.Add(cestimatingItem3);
										}
									}
								}
								text = text + indent + "\t\t</COffice>" + Environment.NewLine;
							}
							if (reportSummaries != null)
							{
								double num13 = (num2 > 0.0) ? ((num2 - num) / num2) : 0.0;
								object obj2 = text;
								text = string.Concat(new object[]
								{
									obj2,
									indent,
									"\t\t<Summary CostTotalSubTotal=\"",
									string.Format("{0:C}", num),
									"\" RawCostTotalSubTotal=\"",
									num,
									"\" PriceTotalSubTotal=\"",
									string.Format("{0:C}", num2),
									"\" RawPriceTotalSubTotal=\"",
									num2,
									"\" MarginSubTotal=\"",
									string.Format("{0:0.00%}", num13),
									"\" RawMarginSubTotal=\"",
									num13,
									"\"/>",
									Environment.NewLine
								});
								reportSummaries.Add(new ReportSummary(groupObject.Name.ToString() + ((i == 0) ? "" : "*"), num, num2, plan, groupObject, groupObject.GroupID, layerName));
							}
							text = text + indent + "\t</Object>" + Environment.NewLine;
						}
						presets.Clear();
						presets = null;
					}
				}
			}
			return text;
		}

		private string BuildXMLObjectSummaries(List<ReportSummary> reportSummaries, string indent, bool isPlanSummaries = false)
		{
			bool taxOnTax = Settings.Default.TaxOnTax;
			string tax1Label = Settings.Default.Tax1Label;
			string tax2Label = Settings.Default.Tax2Label;
			double tax1Rate = Settings.Default.Tax1Rate;
			double tax2Rate = Settings.Default.Tax2Rate;
			double num = 0.0;
			double num2 = 0.0;
			string text = "";
			foreach (ReportSummary reportSummary in reportSummaries)
			{
				if (reportSummary.Caption.IndexOf('*') == -1)
				{
					num += reportSummary.CostSubTotal;
					num2 += reportSummary.PriceSubTotal;
				}
			}
			if (!isPlanSummaries)
			{
				text = text + indent + "\t<Summaries>" + Environment.NewLine;
			}
			foreach (ReportSummary reportSummary2 in reportSummaries)
			{
				if (reportSummary2.Caption.IndexOf('*') == -1)
				{
					double costSubTotal = reportSummary2.CostSubTotal;
					double priceSubTotal = reportSummary2.PriceSubTotal;
					double num3 = (priceSubTotal > 0.0) ? ((priceSubTotal - costSubTotal) / priceSubTotal) : 0.0;
					double num4 = (num2 > 0.0) ? (priceSubTotal / num2) : 0.0;
					string text2 = num4.ToString() + ";";
					DrawObject groupObject = reportSummary2.GroupObject;
					if (groupObject != null)
					{
						text2 = text2 + groupObject.Color.ToArgb() + ";";
					}
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						indent,
						"\t\t<",
						(!isPlanSummaries) ? "Summary" : "PlanSummary",
						" Name=\"",
						Utilities.EscapeString(reportSummary2.Caption),
						"\" CostTotalSubTotal=\"",
						string.Format("{0:C}", costSubTotal),
						"\" RawCostTotalSubTotal=\"",
						costSubTotal,
						"\" PriceTotalSubTotal=\"",
						string.Format("{0:C}", priceSubTotal),
						"\" RawPriceTotalSubTotal=\"",
						priceSubTotal,
						"\" MarginSubTotal=\"",
						string.Format("{0:0.00%}", num3),
						"\" RawMarginSubTotal=\"",
						num3,
						"\" TotalBreakdown=\"",
						string.Format("{0:0.00%}", num4),
						"\" RawTotalBreakdown=\"",
						num4,
						"\" TotalBreakdownTag=\"",
						text2,
						"\"/>",
						Environment.NewLine
					});
				}
			}
			string text3 = tax1Label + " (" + string.Format("{0:0.#####%}", tax1Rate) + ") :";
			string text4 = tax2Label + " (" + string.Format("{0:0.#####%}", tax2Rate) + ") :";
			double num5 = num2 * tax1Rate;
			num5 = Math.Round(num5, 2);
			double num6 = taxOnTax ? ((num5 + num2) * tax2Rate) : (num2 * tax2Rate);
			num6 = Math.Round(num6, 2);
			double num7 = num2 + num5 + num6;
			num7 = Math.Round(num7, 2);
			double num8 = (num2 > 0.0) ? ((num2 - num) / num2) : 0.0;
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				indent,
				"\t\t<",
				(!isPlanSummaries) ? "Total" : "PlanTotal",
				" CostTotal=\"",
				string.Format("{0:C}", num),
				"\" RawCostTotal=\"",
				num,
				"\" PriceTotal=\"",
				string.Format("{0:C}", num2),
				"\" RawPriceTotal=\"",
				num2,
				"\" MarginTotal=\"",
				string.Format("{0:0.00%}", num8),
				"\" RawMarginTotal=\"",
				num8,
				"\" Taxe1Rate=\"",
				string.Format("{0:0.00%}", tax1Rate),
				"\" RawTaxe1Rate=\"",
				tax1Rate,
				"\" Taxe2Rate=\"",
				string.Format("{0:0.00%}", tax2Rate),
				"\" RawTaxe2Rate=\"",
				tax2Rate,
				"\" Taxe1Caption=\"",
				text3,
				"\" Taxe2Caption=\"",
				text4,
				"\" Taxe1Total=\"",
				string.Format("{0:C}", num5),
				"\" RawTaxe1Total=\"",
				num5,
				"\" Taxe2Total=\"",
				string.Format("{0:C}", num6),
				"\" RawTaxe2Total=\"",
				num6,
				"\" TotalAfterTaxes=\"",
				string.Format("{0:C}", num7),
				"\" RawTotalAfterTaxes=\"",
				num7,
				"\"/>",
				Environment.NewLine
			});
			if (!isPlanSummaries)
			{
				text = text + indent + "\t</Summaries>" + Environment.NewLine;
			}
			return text;
		}

		private string BuildXMLLayerSummaries(List<ReportSummary> reportTotalSummaries, List<ReportSummary> reportSummaries, ref double costTotalTotal, ref double priceTotalTotal)
		{
			string text = "\t\t\t<LayerSummaries Name=\"" + Utilities.EscapeString(reportSummaries[0].LayerName) + "\">" + Environment.NewLine;
			text += this.BuildXMLObjectSummaries(reportSummaries, "\t\t", true);
			ReportSummary reportSummary;
			foreach (ReportSummary reportSummary3 in reportSummaries)
			{
				reportSummary = reportSummary3;
				if (reportSummary.Caption.IndexOf('*') == -1)
				{
					ReportSummary reportSummary2 = reportTotalSummaries.Find((ReportSummary x) => x.GroupID == reportSummary.GroupID);
					if (reportSummary2 == null)
					{
						reportTotalSummaries.Add(new ReportSummary(reportSummary.Caption, reportSummary.CostSubTotal, reportSummary.PriceSubTotal, reportSummary.Plan, reportSummary.GroupObject, reportSummary.GroupID, reportSummary.LayerName));
					}
					else
					{
						reportSummary2.CostSubTotal += reportSummary.CostSubTotal;
						reportSummary2.PriceSubTotal += reportSummary.PriceSubTotal;
					}
					costTotalTotal += reportSummary.CostSubTotal;
					priceTotalTotal += reportSummary.PriceSubTotal;
				}
			}
			text = text + "\t\t\t</LayerSummaries>" + Environment.NewLine;
			return text;
		}

		private string BuildXMLLayersSummaries(List<List<ReportSummary>> reportAllSummaries)
		{
			string text = "";
			double num = 0.0;
			double num2 = 0.0;
			if (reportAllSummaries.Count > 0)
			{
				List<ReportSummary> list = new List<ReportSummary>();
				text = text + "\t<PlansSummaries>" + Environment.NewLine;
				text = text + "\t\t<PlanSummaries Name=\"\">" + Environment.NewLine;
				foreach (List<ReportSummary> list2 in reportAllSummaries)
				{
					if (list2.Count > 0)
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							"\t\t\t<LayerSummaries Name=\"",
							Utilities.EscapeString(list2[0].LayerName),
							"\">",
							Environment.NewLine
						});
						text += this.BuildXMLObjectSummaries(list2, "\t\t", true);
						ReportSummary reportSummary;
						foreach (ReportSummary reportSummary3 in list2)
						{
							reportSummary = reportSummary3;
							if (reportSummary.Caption.IndexOf('*') == -1)
							{
								ReportSummary reportSummary2 = list.Find((ReportSummary x) => x.LayerName == reportSummary.LayerName);
								if (reportSummary2 == null)
								{
									list.Add(new ReportSummary(reportSummary.LayerName, reportSummary.CostSubTotal, reportSummary.PriceSubTotal, null, null, -1, reportSummary.LayerName));
								}
								else
								{
									reportSummary2.CostSubTotal += reportSummary.CostSubTotal;
									reportSummary2.PriceSubTotal += reportSummary.PriceSubTotal;
								}
								num += reportSummary.CostSubTotal;
								num2 += reportSummary.PriceSubTotal;
							}
						}
						text = text + "\t\t\t</LayerSummaries>" + Environment.NewLine;
					}
				}
				double num3 = (num2 > 0.0) ? ((num2 - num) / num2) : 0.0;
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"\t\t\t<Total CostTotal=\"",
					string.Format("{0:C}", num),
					"\" RawCostTotal=\"",
					num,
					"\" PriceTotal=\"",
					string.Format("{0:C}", num2),
					"\" RawPriceTotal=\"",
					num2,
					"\" MarginTotal=\"",
					string.Format("{0:0.00%}", num3),
					"\" RawMarginTotal=\"",
					num3,
					"\"/>",
					Environment.NewLine
				});
				num = 0.0;
				num2 = 0.0;
				text = text + "\t\t</PlanSummaries>" + Environment.NewLine;
				text = text + "\t</PlansSummaries>" + Environment.NewLine;
				list.Sort(new ReportModule.SummarySorter());
				text += this.BuildXMLSummaries(list, "");
				list.Clear();
				list = null;
			}
			return text;
		}

		private string BuildXMLPlansSummaries(List<List<ReportSummary>> reportAllSummaries)
		{
			string text = "";
			double num = 0.0;
			double num2 = 0.0;
			Plan plan = null;
			if (reportAllSummaries.Count > 0)
			{
				bool flag = false;
				string a = "";
				List<ReportSummary> list = new List<ReportSummary>();
				List<ReportSummary> list2 = new List<ReportSummary>();
				text = text + "\t<PlansSummaries>" + Environment.NewLine;
				foreach (List<ReportSummary> list3 in reportAllSummaries)
				{
					if (list3.Count > 0)
					{
						if (a != list3[0].Plan.Name)
						{
							if (flag)
							{
								double num3 = (num2 - num) / num2;
								object obj = text;
								text = string.Concat(new object[]
								{
									obj,
									"\t\t\t<Total CostTotal=\"",
									string.Format("{0:C}", num),
									"\" RawCostTotal=\"",
									num,
									"\" PriceTotal=\"",
									string.Format("{0:C}", num2),
									"\" RawPriceTotal=\"",
									num2,
									"\" MarginTotal=\"",
									string.Format("{0:0.00%}", num3),
									"\" RawMarginTotal=\"",
									num3,
									"\"/>",
									Environment.NewLine
								});
								list.Add(new ReportSummary(plan.Name, num, num2, plan, null, -1, ""));
								num = 0.0;
								num2 = 0.0;
								text = text + "\t\t</PlanSummaries>" + Environment.NewLine;
							}
							string text2 = text;
							text = string.Concat(new string[]
							{
								text2,
								"\t\t<PlanSummaries Name=\"",
								Utilities.EscapeString(list3[0].Plan.Name),
								"\">",
								Environment.NewLine
							});
							plan = list3[0].Plan;
							flag = true;
						}
						text += this.BuildXMLLayerSummaries(list2, list3, ref num, ref num2);
						if (a != list3[0].Plan.Name)
						{
							a = list3[0].Plan.Name;
						}
					}
				}
				if (flag)
				{
					double num3 = (num2 > 0.0) ? ((num2 - num) / num2) : 0.0;
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						"\t\t\t<Total CostTotal=\"",
						string.Format("{0:C}", num),
						"\" RawCostTotal=\"",
						num,
						"\" PriceTotal=\"",
						string.Format("{0:C}", num2),
						"\" RawPriceTotal=\"",
						num2,
						"\" MarginTotal=\"",
						string.Format("{0:0.00%}", num3),
						"\" RawMarginTotal=\"",
						num3,
						"\"/>",
						Environment.NewLine
					});
					list.Add(new ReportSummary(plan.Name, num, num2, plan, null, -1, ""));
					num = 0.0;
					num2 = 0.0;
					text = text + "\t\t</PlanSummaries>" + Environment.NewLine;
					flag = false;
				}
				text = text + "\t</PlansSummaries>" + Environment.NewLine;
				list.Sort(new ReportModule.SummarySorter());
				text += this.BuildXMLSummaries(list, "");
				list.Clear();
				list = null;
				list2.Clear();
				list2 = null;
			}
			return text;
		}

		private string BuildXMLSummaries(List<ReportSummary> reportSummaries, string indent)
		{
			bool taxOnTax = Settings.Default.TaxOnTax;
			string tax1Label = Settings.Default.Tax1Label;
			string tax2Label = Settings.Default.Tax2Label;
			double tax1Rate = Settings.Default.Tax1Rate;
			double tax2Rate = Settings.Default.Tax2Rate;
			double num = 0.0;
			double num2 = 0.0;
			string text = "";
			foreach (ReportSummary reportSummary in reportSummaries)
			{
				num += reportSummary.CostSubTotal;
				num2 += reportSummary.PriceSubTotal;
			}
			text = text + indent + "\t<Summaries>" + Environment.NewLine;
			int num3 = 11;
			foreach (ReportSummary reportSummary2 in reportSummaries)
			{
				double costSubTotal = reportSummary2.CostSubTotal;
				double priceSubTotal = reportSummary2.PriceSubTotal;
				double num4 = (priceSubTotal > 0.0) ? ((priceSubTotal - costSubTotal) / priceSubTotal) : 0.0;
				double num5 = (num2 > 0.0) ? (priceSubTotal / num2) : 0.0;
				string text2 = num5.ToString() + ";";
				text2 = text2 + Utilities.GetBasicColor(++num3).ToArgb() + ";";
				if (num3 == 15)
				{
					num3 = 0;
				}
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					indent,
					"\t\t<Summary Name=\"",
					Utilities.EscapeString(reportSummary2.Caption),
					"\" CostTotalSubTotal=\"",
					string.Format("{0:C}", costSubTotal),
					"\" RawCostTotalSubTotal=\"",
					costSubTotal,
					"\" PriceTotalSubTotal=\"",
					string.Format("{0:C}", priceSubTotal),
					"\" RawPriceTotalSubTotal=\"",
					priceSubTotal,
					"\" MarginSubTotal=\"",
					string.Format("{0:0.00%}", num4),
					"\" RawMarginSubTotal=\"",
					num4,
					"\" TotalBreakdown=\"",
					string.Format("{0:0.00%}", num5),
					"\" RawTotalBreakdown=\"",
					num5,
					"\" TotalBreakdownTag=\"",
					text2,
					"\"/>",
					Environment.NewLine
				});
			}
			string text3 = tax1Label + " (" + string.Format("{0:0.#####%}", tax1Rate) + ") :";
			string text4 = tax2Label + " (" + string.Format("{0:0.#####%}", tax2Rate) + ") :";
			double num6 = num2 * tax1Rate;
			num6 = Math.Round(num6, 2);
			double num7 = taxOnTax ? ((num6 + num2) * tax2Rate) : (num2 * tax2Rate);
			num7 = Math.Round(num7, 2);
			double num8 = num2 + num6 + num7;
			num8 = Math.Round(num8, 2);
			double num9 = (num2 > 0.0) ? ((num2 - num) / num2) : 0.0;
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				indent,
				"\t\t<Total CostTotal=\"",
				string.Format("{0:C}", num),
				"\" RawCostTotal=\"",
				num,
				"\" PriceTotal=\"",
				string.Format("{0:C}", num2),
				"\" RawPriceTotal=\"",
				num2,
				"\" MarginTotal=\"",
				string.Format("{0:0.00%}", num9),
				"\" RawMarginTotal=\"",
				num9,
				"\" Taxe1Rate=\"",
				string.Format("{0:0.00%}", tax1Rate),
				"\" RawTaxe1Rate=\"",
				tax1Rate,
				"\" Taxe2Rate=\"",
				string.Format("{0:0.00%}", tax2Rate),
				"\" RawTaxe2Rate=\"",
				tax2Rate,
				"\" Taxe1Caption=\"",
				text3,
				"\" Taxe2Caption=\"",
				text4,
				"\" Taxe1Total=\"",
				string.Format("{0:C}", num6),
				"\" RawTaxe1Total=\"",
				num6,
				"\" Taxe2Total=\"",
				string.Format("{0:C}", num7),
				"\" RawTaxe2Total=\"",
				num7,
				"\" TotalAfterTaxes=\"",
				string.Format("{0:C}", num8),
				"\" RawTotalAfterTaxes=\"",
				num8,
				"\"/>",
				Environment.NewLine
			});
			text = text + indent + "\t</Summaries>" + Environment.NewLine;
			return text;
		}

		private string BuildXMLObjects(List<Variable> groups, List<ReportSummary> reportSummaries, Plan plan, string layerName, Variables plans, bool includeHiddenValues = false, bool exportForExcel = false, List<EstimatingItem> results = null)
		{
			int num = 0;
			int num2 = 0;
			string text = "";
			string text2 = "";
			for (int i = 0; i < groups.Count; i++)
			{
				text += this.BuildXMLObject((DrawObject)groups[i].Tag, plan, layerName, plans, reportSummaries, ref text2, ref num, ref num2, includeHiddenValues, exportForExcel, results);
			}
			return text;
		}

		private string BuildXMLOrderByObjects(bool includeHiddenValues = false, bool exportForExcel = false, List<EstimatingItem> results = null)
		{
			List<Variable> list = new List<Variable>();
			List<ReportSummary> list2 = new List<ReportSummary>();
			Variables variables = new Variables();
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
							bool flag;
							if (this.project.Report.SelectedReportType == ReportTypeEnum.QuoteReport)
							{
								flag = this.filter.QueryFilter(plan.Name, "", -1, false);
								if (!flag)
								{
									flag = this.filter.QueryFilter(plan.Name, layer.Name, -1, false);
								}
								if (!flag)
								{
									flag = this.filter.QueryFilter(plan.Name, layer.Name, layerObject.GroupID, false);
								}
							}
							else
							{
								flag = this.filter.QueryFilter("", "", layerObject.GroupID, false);
								if (!flag)
								{
									flag = this.filter.QueryFilter(plan.Name, layerObject.GroupID);
								}
							}
							if (!flag)
							{
								if (list.Find((Variable x) => Utilities.ConvertToInt(x.Value) == layerObject.GroupID) == null)
								{
									list.Add(new Variable(layerObject.Name, layerObject.GroupID, layerObject));
								}
								bool flag2 = false;
								foreach (object obj4 in variables.Collection)
								{
									Variable variable = (Variable)obj4;
									if (variable.Name == layerObject.GroupID.ToString() && variable.Value.ToString() == plan.Name)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									variables.Add(new Variable(layerObject.GroupID.ToString(), plan.Name));
								}
							}
						}
					}
				}
			}
			string text = "\t<Objects>" + Environment.NewLine;
			list.Sort(new ReportModule.GroupSorter());
			text += this.BuildXMLObjects(list, list2, null, "", variables, includeHiddenValues, exportForExcel, results);
			text = text + "\t</Objects>" + Environment.NewLine;
			this.ReportSummaries = this.BuildXMLObjectSummaries(list2, "", false);
			text += this.ReportSummaries;
			list.Clear();
			list = null;
			list2.Clear();
			list2 = null;
			variables.Clear();
			variables = null;
			if (results == null)
			{
				return text;
			}
			return string.Empty;
		}

		private string BuildXMLOrderByLayers(bool includeHiddenValues = false, bool exportForExcel = false)
		{
			string text = "";
			string text2 = "";
			List<Variable> list = new List<Variable>();
			List<List<ReportSummary>> list2 = new List<List<ReportSummary>>();
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				if (!this.filter.QueryFilter(plan.Name, "", -1, false))
				{
					Layer layer;
					foreach (object obj2 in plan.Layers.Collection)
					{
						layer = (Layer)obj2;
						if (!this.filter.QueryFilter(plan.Name, layer.Name, -1, false))
						{
							DrawObject layerObject;
							foreach (object obj3 in layer.DrawingObjects.Collection)
							{
								layerObject = (DrawObject)obj3;
								if (layerObject.IsPartOfGroup() && !layerObject.IsDeduction() && !this.filter.QueryFilter(plan.Name, layer.Name, layerObject.GroupID, false))
								{
									Variable variable = list.Find((Variable x) => x.Name == layer.Name);
									if (variable == null)
									{
										List<Variable> value = new List<Variable>();
										variable = new Variable(layer.Name, value);
										list.Add(variable);
									}
									if (variable != null)
									{
										List<Variable> list3 = (List<Variable>)variable.Value;
										if (list3.Find((Variable x) => Utilities.ConvertToInt(x.Value) == layerObject.GroupID) == null)
										{
											list3.Add(new Variable(layerObject.Name, layerObject.GroupID, layerObject));
										}
									}
								}
							}
						}
					}
				}
			}
			text = text + "\t<Plans>" + Environment.NewLine;
			list.Sort(new ReportModule.LayerSorter());
			foreach (Variable variable2 in list)
			{
				List<Variable> list4 = (List<Variable>)variable2.Value;
				list4.Sort(new ReportModule.GroupSorter());
				List<ReportSummary> list5 = new List<ReportSummary>();
				string text3 = this.BuildXMLObjects(list4, list5, null, variable2.Name, null, includeHiddenValues, exportForExcel, null);
				if (text3.Trim() != "")
				{
					string text4 = text2;
					text2 = string.Concat(new string[]
					{
						text4,
						"\t\t\t\t<Layer Name=\"",
						Utilities.EscapeString(variable2.Name),
						"\">",
						Environment.NewLine
					});
					text2 += text3;
					text2 = text2 + "\t\t\t\t</Layer>" + Environment.NewLine;
					list2.Add(list5);
				}
				else
				{
					list5.Clear();
				}
			}
			if (text2.Trim() != "")
			{
				text = text + "\t\t<Plan Name=\"\">" + Environment.NewLine;
				text = text + "\t\t\t<Objects>" + Environment.NewLine;
				text += text2;
				text = text + "\t\t\t</Objects>" + Environment.NewLine;
				text = text + "\t\t</Plan>" + Environment.NewLine;
			}
			text = text + "\t</Plans>" + Environment.NewLine;
			this.ReportSummaries = this.BuildXMLLayersSummaries(list2);
			text += this.ReportSummaries;
			foreach (List<ReportSummary> list6 in list2)
			{
				list6.Clear();
			}
			list2.Clear();
			list2 = null;
			return text;
		}

		private string BuildXMLOrderByPlans(bool includeHiddenValues = false, bool exportForExcel = false)
		{
			string text = "";
			List<List<ReportSummary>> list = new List<List<ReportSummary>>();
			text = text + "\t<Plans>" + Environment.NewLine;
			foreach (object obj in this.project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				List<Variable> list2 = new List<Variable>();
				string text2 = "";
				if (!this.filter.QueryFilter(plan.Name, "", -1, false))
				{
					foreach (object obj2 in plan.Layers.Collection)
					{
						Layer layer = (Layer)obj2;
						if (!this.filter.QueryFilter(plan.Name, layer.Name, -1, false))
						{
							DrawObject layerObject;
							foreach (object obj3 in layer.DrawingObjects.Collection)
							{
								layerObject = (DrawObject)obj3;
								if (layerObject.IsPartOfGroup() && !layerObject.IsDeduction() && !this.filter.QueryFilter(plan.Name, layer.Name, layerObject.GroupID, false))
								{
									if (list2.Find((Variable x) => Utilities.ConvertToInt(x.Value) == layerObject.GroupID) == null)
									{
										list2.Add(new Variable(layerObject.Name, layerObject.GroupID, layerObject));
									}
								}
							}
						}
						list2.Sort(new ReportModule.GroupSorter());
						List<ReportSummary> list3 = new List<ReportSummary>();
						string text3 = this.BuildXMLObjects(list2, list3, plan, layer.Name, null, includeHiddenValues, exportForExcel, null);
						if (text3.Trim() != "")
						{
							bool flag = true;
							if (flag)
							{
								string text4 = text2;
								text2 = string.Concat(new string[]
								{
									text4,
									"\t\t\t\t<Layer Name=\"",
									Utilities.EscapeString(layer.Name),
									"\">",
									Environment.NewLine
								});
							}
							text2 += text3;
							if (flag)
							{
								text2 = text2 + "\t\t\t\t</Layer>" + Environment.NewLine;
							}
							list.Add(list3);
						}
						else
						{
							list3.Clear();
						}
						list2.Clear();
					}
					if (text2.Trim() != "")
					{
						string text5 = text;
						text = string.Concat(new string[]
						{
							text5,
							"\t\t<Plan Name=\"",
							Utilities.EscapeString(plan.Name),
							"\">",
							Environment.NewLine
						});
						text = text + "\t\t\t<Objects>" + Environment.NewLine;
						text += text2;
						text = text + "\t\t\t</Objects>" + Environment.NewLine;
						text = text + "\t\t</Plan>" + Environment.NewLine;
					}
				}
				list2 = null;
			}
			text = text + "\t</Plans>" + Environment.NewLine;
			this.ReportSummaries = this.BuildXMLPlansSummaries(list);
			text += this.ReportSummaries;
			foreach (List<ReportSummary> list4 in list)
			{
				list4.Clear();
			}
			list.Clear();
			list = null;
			return text;
		}

		private string BuildXML(Report.ReportOrderEnum order, bool includeHiddenValues = false, bool exportForExcel = false)
		{
			string text = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<Report Title=\"",
				Utilities.EscapeString(this.project.Name),
				"\" Date=\"",
				Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()),
				"\">",
				Environment.NewLine
			});
			if (this.project.Report.ShowProjectInfo)
			{
				text += this.BuildXMLProjectInfo();
			}
			switch (order)
			{
			case Report.ReportOrderEnum.ReportOrderByObjects:
				text += this.BuildXMLOrderByObjects(includeHiddenValues, exportForExcel, null);
				break;
			case Report.ReportOrderEnum.ReportOrderByPlans:
				if (this.project.Report.ReportSortBy == Report.ReportSortByEnum.ReportSortByPlans)
				{
					text += this.BuildXMLOrderByPlans(includeHiddenValues, exportForExcel);
				}
				else
				{
					text += this.BuildXMLOrderByLayers(includeHiddenValues, exportForExcel);
				}
				break;
			}
			return text + "</Report>";
		}

		private string BuildXSLOrderByObjects()
		{
			return Utilities.ReadToString(Path.Combine(Utilities.GetInstallReportsFolder(), Utilities.GetCurrentValidUICulture() + "\\sort_by_objects.xsl"));
		}

		private string BuildXSLOrderByPlans()
		{
			return Utilities.ReadToString(Path.Combine(Utilities.GetInstallReportsFolder(), Utilities.GetCurrentValidUICulture() + "\\sort_by_plans.xsl"));
		}

		private string BuildXSL(Report.ReportOrderEnum order)
		{
			string result = "";
			switch (order)
			{
			case Report.ReportOrderEnum.ReportOrderByObjects:
				result = this.BuildXSLOrderByObjects();
				break;
			case Report.ReportOrderEnum.ReportOrderByPlans:
				result = this.BuildXSLOrderByPlans();
				break;
			}
			return result;
		}

		private string MakeHTML(string xml, string xsl, bool useAbsolutePath)
		{
			string result;
			try
			{
				this.IESetupHeaderFooter(Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()));
				StringReader stringReader = new StringReader(xml);
				XPathDocument input = new XPathDocument(stringReader);
				StringReader stringReader2 = new StringReader(xsl);
				XmlTextReader xmlTextReader = new XmlTextReader(stringReader2);
				XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
				xslCompiledTransform.Load(xmlTextReader, null, null);
				XsltArgumentList xsltArgumentList = new XsltArgumentList();
				xsltArgumentList.AddParam("title", "", this.project.Name);
				xsltArgumentList.AddParam("app_path", "", Utilities.GetInstallFolder());
				string text = Path.Combine(Utilities.GetInstallReportsFolder(), string.Concat(new string[]
				{
					"css\\",
					Utilities.GetCurrentValidUICulture(),
					"\\",
					Settings.Default.ReportTheme,
					".css"
				}));
				string destinationFileName = Path.Combine(Utilities.GetReportsFolder(), "css\\style.css");
				if (!useAbsolutePath)
				{
					Utilities.FileCopy(text, destinationFileName);
				}
				xsltArgumentList.AddParam("css_path", "", useAbsolutePath ? text : "css\\style.css");
				xsltArgumentList.AddParam("images_path", "", useAbsolutePath ? Path.Combine(Utilities.GetInstallReportsFolder(), "images") : "images");
				ReportModule.UTF8StringWriter utf8StringWriter = new ReportModule.UTF8StringWriter();
				xslCompiledTransform.Transform(input, xsltArgumentList, utf8StringWriter);
				string text2 = utf8StringWriter.ToString();
				stringReader.Close();
				stringReader2.Close();
				xmlTextReader.Close();
				utf8StringWriter.Close();
				result = text2;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = "";
			}
			return result;
		}

		private string BuildHTML(Report.ReportOrderEnum order, bool useAbsolutePath)
		{
			string xml = this.BuildXML(order, false, false);
			string xsl = this.BuildXSL(order);
			return this.MakeHTML(xml, xsl, useAbsolutePath);
		}

		private bool BuildXLS(Report.ReportOrderEnum order, Report.ReportSortByEnum sortBy, bool exportEstimating, string fileName)
		{
			string xml = this.BuildXML(order, false, true);
			ReportModule.ExportToXLS exportToXLS = new ReportModule.ExportToXLS(order, sortBy, exportEstimating);
			return exportToXLS.Export(xml, fileName);
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

		public void Clear()
		{
			string htmlDocument = this.FormatHtmlString("");
			this.LoadHtmlDocument(htmlDocument);
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
			this.filter.LoadFromString((this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByPlans) ? this.project.Report.OrderByPlansFilter : this.project.Report.OrderByObjectsFilter, this.project.Report.Order);
		}

		public string ExportToXML(bool exportEstimatingItems, bool exportToMemory = false)
		{
			this.LoadFilter();
			this.ExportEstimatingItems = exportEstimatingItems;
			this.ExportReportToMemory = exportToMemory;
			this.ReportSummaries = "";
			string text = this.BuildXML(this.project.Report.Order, false, false);
			if (exportToMemory)
			{
				string text2 = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
				string text3 = text2;
				text2 = string.Concat(new string[]
				{
					text3,
					"<Report Title=\"",
					Utilities.EscapeString(this.project.Name),
					"\" Date=\"",
					Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort()),
					"\">",
					Environment.NewLine
				});
				text2 += this.ReportSummaries;
				text2 = text2 + "</Report>" + Environment.NewLine;
				this.ReportSummaries = text2;
				return text;
			}
			string text4 = Path.GetFileNameWithoutExtension(this.project.FileName);
			text4 = text4 + "-" + ((this.project.Report.Order == Report.ReportOrderEnum.ReportOrderByObjects) ? Resources.par_objets : Resources.par_plans) + ".xml";
			text4 = Path.Combine(Utilities.GetReportsFolder(), text4.Replace(' ', '-'));
			Utilities.SaveStringToFile(text4, text);
			Utilities.OpenDocument(text4);
			return string.Empty;
		}

		public void ExportToExcel(string fileName, bool exportEstimating)
		{
			this.LoadFilter();
			if (this.BuildXLS(this.project.Report.Order, this.project.Report.ReportSortBy, exportEstimating, fileName))
			{
				Utilities.OpenDocument(fileName);
			}
		}

		public void ExportObjectToList(DrawObject groupObject, List<EstimatingItem> results)
		{
			int num = 0;
			int num2 = 0;
			string text = "";
			this.BuildXMLObject(groupObject, null, "", null, null, ref text, ref num, ref num2, true, false, results);
		}

		public void ExportToList(List<EstimatingItem> results)
		{
			this.BuildXMLOrderByObjects(true, false, results);
		}

		private string RenameInFilter(string filter, string objectType, string oldName, string newName, string planName = "")
		{
			string text = Filter.Rename(filter, objectType, oldName, newName, planName);
			Console.WriteLine("Old filter = " + filter);
			Console.WriteLine("New filter = " + text);
			return text;
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

		public void RenameLayer(Plan plan, Layer layer)
		{
			if (layer.PrevName == layer.Name)
			{
				return;
			}
			Console.WriteLine("OrderByPlansFilter");
			this.project.Report.OrderByPlansFilter = this.RenameInFilter(this.project.Report.OrderByPlansFilter, "#LAYER", layer.PrevName, layer.Name, plan.Name);
		}

		public void CleanUpFilters()
		{
			Console.WriteLine("ReportModule.CleanUpFilters");
			using (ReportEditForm reportEditForm = new ReportEditForm(this.project, this.drawArea))
			{
				reportEditForm.CleanUpFilters();
			}
		}

		private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			HtmlDocument document = this.webBrowser.Document;
		}

		private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (this.webBrowser.Document != null)
			{
				bool flag = this.enabled;
			}
		}

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

		[CompilerGenerated]
		private bool <ExportReportToMemory>k__BackingField;

		[CompilerGenerated]
		private bool <SortByLayers>k__BackingField;

		[CompilerGenerated]
		private bool <ExportEstimatingItems>k__BackingField;

		[CompilerGenerated]
		private string <ReportSummaries>k__BackingField;

		private class GroupSummarySorter : IComparer<ReportSummary>
		{
			public int Compare(ReportSummary x, ReportSummary y)
			{
				int result;
				try
				{
					DrawObject groupObject = x.GroupObject;
					DrawObject groupObject2 = y.GroupObject;
					if (groupObject.ObjectType != groupObject2.ObjectType)
					{
						result = StringLogicalComparer.Compare(groupObject.ObjectSortOrder.ToString(), groupObject2.ObjectSortOrder.ToString());
					}
					else
					{
						result = StringLogicalComparer.Compare(groupObject.Name, groupObject2.Name);
					}
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}

			public GroupSummarySorter()
			{
			}
		}

		private class SummarySorter : IComparer<ReportSummary>
		{
			public int Compare(ReportSummary x, ReportSummary y)
			{
				int result;
				try
				{
					string caption = x.Caption;
					string caption2 = y.Caption;
					result = StringLogicalComparer.Compare(caption, caption2);
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}

			public SummarySorter()
			{
			}
		}

		private class LayerSorter : IComparer<Variable>
		{
			public int Compare(Variable x, Variable y)
			{
				int result;
				try
				{
					string name = x.Name;
					string name2 = y.Name;
					result = StringLogicalComparer.Compare(name, name2);
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}

			public LayerSorter()
			{
			}
		}

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

		private class UTF8StringWriter : StringWriter
		{
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

			public override Encoding Encoding
			{
				get
				{
					return Encoding.UTF8;
				}
			}
		}

		private class ExportToXLS
		{
			public ExportToXLS(Report.ReportOrderEnum order, Report.ReportSortByEnum sortBy, bool exportEstimating)
			{
				this.order = order;
				this.sortBy = sortBy;
				this.exportEstimating = exportEstimating;
			}

			private string GetStringAttribute(XmlNode node, string attributeName)
			{
				string result;
				try
				{
					result = node.Attributes.GetNamedItem(attributeName).Value;
				}
				catch
				{
					result = "";
				}
				return result;
			}

			private double GetDoubleAttribute(XmlNode node, string attributeName)
			{
				double result;
				try
				{
					string text = node.Attributes.GetNamedItem(attributeName).Value;
					string newValue = Utilities.NumberDecimalSeparator();
					text = text.Replace(",", newValue).Replace(".", newValue);
					decimal value = decimal.Parse(text);
					result = (double)value;
				}
				catch (Exception)
				{
					result = 0.0;
				}
				return result;
			}

			private IRow AppendRow(short rowHeight = -1)
			{
				IRow row = this.sheet.CreateRow((this.sheet.PhysicalNumberOfRows == 0) ? 0 : (this.sheet.LastRowNum + 1));
				if (rowHeight != -1)
				{
					row.HeightInPoints = (float)rowHeight;
				}
				return row;
			}

			private IFont GetFont(short boldWeight, short color, short fontHeightInPoints, string name, bool italic, bool strikeout, short typeOffset, byte underline)
			{
				short fontHeight = fontHeightInPoints * 20;
				IFont font = this.workbook.FindFont(boldWeight, color, fontHeight, name, italic, strikeout, typeOffset, underline);
				if (font != null)
				{
					return font;
				}
				font = this.workbook.CreateFont();
				font.Boldweight = boldWeight;
				font.Color = color;
				font.FontHeight = fontHeight;
				font.FontName = name;
				font.IsItalic = italic;
				font.IsStrikeout = strikeout;
				font.TypeOffset = typeOffset;
				font.Underline = underline;
				return font;
			}

			private ICell InsertCell(object value, IRow row, int cellIndex, CellType cellType, ICellStyle cellStyle)
			{
				ICell cell = row.CreateCell(cellIndex, cellType);
				cell.CellStyle = cellStyle;
				if (cellType == CellType.NUMERIC)
				{
					cell.SetCellValue(Utilities.ConvertToDouble(value, -1));
				}
				else
				{
					cell.SetCellValue(value.ToString());
				}
				return cell;
			}

			private void OutputConsolidatedExtensions()
			{
				string a = string.Empty;
				if (this.parserPresets.Count > 0)
				{
					this.parserPresets.Collection.Sort(new ReportModule.ExportToXLS.PresetSorter());
					foreach (object obj in this.parserPresets.Collection)
					{
						Preset preset = (Preset)obj;
						if (a != preset.ExtensionName + preset.Tag)
						{
							IRow row = this.AppendRow(26);
							this.InsertCell(Utilities.GetFields(preset.ExtensionName, ';').GetValue(0).ToString().Trim(), row, 0, CellType.STRING, this.extensionNameCellStyle);
						}
						if (a != preset.ExtensionName + preset.Tag)
						{
							this.parserResultRowHeader = this.AppendRow(-1);
						}
						this.parserResultRowData = this.AppendRow(16);
						this.InsertCell(preset.CategoryName, this.parserResultRowData, 0, CellType.STRING, this.objectNameCellStyle);
						this.parserResultColumnIndex = 1;
						foreach (object obj2 in preset.Results.Collection)
						{
							PresetResult presetResult = (PresetResult)obj2;
							if (a != preset.ExtensionName + preset.Tag)
							{
								this.InsertCell(presetResult.Caption, this.parserResultRowHeader, this.parserResultColumnIndex, CellType.STRING, this.columnHeaderCellStyle);
							}
							bool flag = this.sheet.Equals(this.formattedSheetConsolidated);
							string unit = presetResult.Unit;
							if (!flag)
							{
								if (!Utilities.IsNumber(unit.ToString()))
								{
									this.InsertCell(unit, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
								}
								else
								{
									this.InsertCell(unit, this.parserResultRowData, this.parserResultColumnIndex, CellType.NUMERIC, Utilities.IsInteger(unit) ? this.integerCellStyle : this.decimalCellStyle);
								}
							}
							else
							{
								this.InsertCell(unit, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
							}
							this.parserResultColumnIndex++;
						}
						a = preset.ExtensionName + preset.Tag;
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
				string text = "";
				string text2 = "";
				foreach (ReportSummary reportSummary in this.reportTotalSummaries)
				{
					text = text + ((text == "") ? "" : " + ") + string.Format("E{0}", reportSummary.CostSubTotal + 1.0);
					text2 = text2 + ((text2 == "") ? "" : " + ") + string.Format("G{0}", reportSummary.CostSubTotal + 1.0);
				}
				this.InsertCell(Resources.Sous_totaux_, row, 3, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
				ICell cell = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
				cell.SetCellFormula(text);
				ICell cell2 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
				cell2.SetCellFormula(text2);
				ICell cell3 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
				cell3.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell.RowIndex + 1));
				this.reportTotalTotalSummaries.Add(new ReportSummary(caption, (double)row.RowNum, 0.0, null, null, -1, ""));
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
				foreach (ReportSummary reportSummary in this.reportTotalTotalSummaries)
				{
					row = this.AppendRow(14);
					this.InsertCell(reportSummary.Caption, row, 0, CellType.STRING, this.estimatingItemCellStyle);
					ICell cell = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
					cell.SetCellFormula(string.Format("E{0}", reportSummary.CostSubTotal + 1.0));
					ICell cell2 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
					cell2.SetCellFormula(string.Format("G{0}", reportSummary.CostSubTotal + 1.0));
					ICell cell3 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
					cell3.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell.RowIndex + 1));
					this.parserEstimatingRowEndIndex++;
				}
				row = this.AppendRow(14);
				ICell cell4 = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
				cell4.SetCellFormula(string.Format("SUM(E{0}:E{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
				ICell cell5 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
				cell5.SetCellFormula(string.Format("SUM(G{0}:G{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
				ICell cell6 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
				cell6.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell4.RowIndex + 1));
				bool taxOnTax = Settings.Default.TaxOnTax;
				string tax1Label = Settings.Default.Tax1Label;
				string tax2Label = Settings.Default.Tax2Label;
				double tax1Rate = Settings.Default.Tax1Rate;
				double tax2Rate = Settings.Default.Tax2Rate;
				string value = tax1Label + " (" + string.Format("{0:0.#####%}", tax1Rate) + ") :";
				string value2 = tax2Label + " (" + string.Format("{0:0.#####%}", tax2Rate) + ") :";
				row = this.AppendRow(20);
				row = this.AppendRow(14);
				this.InsertCell(value, row, 5, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
				ICell cell7 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
				cell7.SetCellFormula(string.Format("G{0}*" + tax1Rate, cell4.RowIndex + 1));
				row = this.AppendRow(14);
				this.InsertCell(value2, row, 5, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
				ICell cell8 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
				if (taxOnTax)
				{
					cell8.SetCellFormula(string.Format("(G{0}+G{1})*" + tax2Rate, cell4.RowIndex + 1, cell7.RowIndex + 1));
				}
				else
				{
					cell8.SetCellFormula(string.Format("G{0}*" + tax2Rate, cell4.RowIndex + 1));
				}
				row = this.AppendRow(10);
				row = this.AppendRow(14);
				this.InsertCell(Resources.GRAND_TOTAL_, row, 5, CellType.STRING, this.estimatingSubTotalCaptionCellStyle);
				ICell cell9 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
				cell9.SetCellFormula(string.Format("G{0}+G{1}+G{2}", cell4.RowIndex + 1, cell7.RowIndex + 1, cell8.RowIndex + 1));
			}

			private void ParseNode(XmlNode node)
			{
				string a3;
				if (this.sheet.Equals(this.estimatingSheet))
				{
					string a;
					if ((a = node.Name.ToUpper()) != null)
					{
						if (!(a == "PLAN"))
						{
							if (!(a == "LAYER"))
							{
								if (!(a == "OBJECT"))
								{
									if (!(a == "COMMENT"))
									{
										if (a == "RESULT")
										{
											if (this.parserEstimatingResultCount == 0)
											{
												IRow row = this.AppendRow(14);
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
											string stringAttribute = this.GetStringAttribute(node, "EstimatingCaption");
											if (stringAttribute != string.Empty)
											{
												IRow row = this.AppendRow(14);
												this.InsertCell(stringAttribute, row, 0, CellType.STRING, this.estimatingItemCellStyle);
												ICell cell = this.InsertCell(this.GetStringAttribute(node, "Quantity"), row, 1, CellType.NUMERIC, this.estimatingEditableQuantityCellStyle);
												this.InsertCell(this.GetStringAttribute(node, "EstimatingUnit"), row, 2, CellType.STRING, this.estimatingItemResultCellStyle);
												this.InsertCell(this.GetDoubleAttribute(node, "RawCostEach"), row, 3, CellType.NUMERIC, this.estimatingEditablePriceCellStyle);
												ICell cell2 = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
												cell2.SetCellFormula(string.Format("B{0}*D{0}", cell.RowIndex + 1));
												ICell cell3 = this.InsertCell(this.GetDoubleAttribute(node, "RawPriceEach"), row, 5, CellType.NUMERIC, this.estimatingEditablePriceCellStyle);
												ICell cell4 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
												cell4.SetCellFormula(string.Format("B{0}*F{0}", cell.RowIndex + 1));
												ICell cell5 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
												cell5.SetCellFormula(string.Format("(F{0}-D{0})/D{0}", cell3.RowIndex + 1));
												this.parserEstimatingRowEndIndex++;
											}
											this.parserEstimatingResultCount++;
											return;
										}
										if (!(a == "SUMMARY"))
										{
											return;
										}
										if (this.parserEstimatingRowStartIndex > 0)
										{
											IRow row = this.AppendRow(14);
											ICell cell6 = this.InsertCell(0, row, 4, CellType.FORMULA, this.estimatingItemPriceCellStyle);
											cell6.SetCellFormula(string.Format("SUM(E{0}:E{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
											ICell cell7 = this.InsertCell(0, row, 6, CellType.FORMULA, this.estimatingItemPriceCellStyle);
											cell7.SetCellFormula(string.Format("SUM(G{0}:G{1})", this.parserEstimatingRowStartIndex, this.parserEstimatingRowEndIndex));
											ICell cell8 = this.InsertCell(0, row, 7, CellType.FORMULA, this.estimatingItemMarginCellStyle);
											cell8.SetCellFormula(string.Format("(G{0}-E{0})/E{0}", cell6.RowIndex + 1));
											if (this.order == Report.ReportOrderEnum.ReportOrderByObjects)
											{
												this.reportTotalTotalSummaries.Add(new ReportSummary(this.reportTotalCaption, (double)row.RowNum, 0.0, null, null, -1, ""));
											}
											else
											{
												this.reportTotalSummaries.Add(new ReportSummary("", (double)row.RowNum, 0.0, null, null, -1, ""));
											}
											this.parserEstimatingRowStartIndex = 0;
										}
										this.parserExtensionCount++;
										return;
									}
									else if (node.ParentNode.Name.ToUpper() != "PROJECT")
									{
										string text = this.GetStringAttribute(node, "Value").Trim().Replace("`", "\r\n");
										if (text != string.Empty)
										{
											IRow row = this.AppendRow(-1);
											this.InsertCell(text, row, 0, CellType.STRING, this.commentCellStyle);
											return;
										}
									}
								}
								else
								{
									this.parserExtensionCount = 0;
									this.parserEstimatingResultCount = 0;
									this.parserObjectName = this.GetStringAttribute(node, "Name");
									string stringAttribute2 = this.GetStringAttribute(node, "Type");
									this.parserObjectTypeHasChanged = (stringAttribute2 != this.parserObjectType);
									this.parserObjectType = stringAttribute2;
									this.parserObjectRow = this.AppendRow(26);
									this.InsertCell(this.parserObjectName, this.parserObjectRow, 0, CellType.STRING, this.objectNameCellStyle);
									if (this.order == Report.ReportOrderEnum.ReportOrderByObjects)
									{
										this.reportTotalCaption = this.parserObjectName;
										return;
									}
								}
							}
							else
							{
								IRow row;
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
						}
						else if (node.HasChildNodes && node.FirstChild.HasChildNodes)
						{
							if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByPlans)
							{
								this.OutputReportSubTotal(this.parserPlanName);
								this.reportTotalSummaries.Clear();
								if (this.parserPlanName != string.Empty)
								{
									IRow row = this.AppendRow(10);
								}
							}
							this.parserObjectType = string.Empty;
							this.parserObjectTypeCount = 0;
							this.parserPlanName = this.GetStringAttribute(node, "Name");
							if (this.parserPlanName != string.Empty)
							{
								IRow row = this.AppendRow(28);
								this.InsertCell(this.parserPlanName, row, 0, CellType.STRING, this.planNameCellStyle);
								if (this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByPlans)
								{
									this.reportTotalCaption = this.parserPlanName;
									return;
								}
							}
						}
					}
				}
				else if (!this.sheet.Equals(this.formattedSheetShort) && !this.sheet.Equals(this.rawSheetShort))
				{
					bool flag = this.sheet.Equals(this.formattedSheetConsolidated) || this.sheet.Equals(this.rawSheetConsolidated);
					string key;
					switch (key = node.Name.ToUpper())
					{
					case "PLAN":
						if (flag && this.order == Report.ReportOrderEnum.ReportOrderByPlans && this.sortBy == Report.ReportSortByEnum.ReportSortByPlans)
						{
							this.OutputConsolidatedExtensions();
						}
						if (node.HasChildNodes && node.FirstChild.HasChildNodes)
						{
							if (this.parserPlanName != "")
							{
								IRow row = this.AppendRow(10);
							}
							this.parserObjectType = string.Empty;
							this.parserObjectTypeCount = 0;
							this.parserPlanName = this.GetStringAttribute(node, "Name");
							if (this.parserPlanName != "")
							{
								IRow row = this.AppendRow(28);
								this.InsertCell(this.parserPlanName, row, 0, CellType.STRING, this.planNameCellStyle);
								return;
							}
						}
						break;
					case "LAYER":
					{
						IRow row;
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
						if (flag && this.order == Report.ReportOrderEnum.ReportOrderByObjects)
						{
							this.OutputConsolidatedExtensions();
							return;
						}
						break;
					case "OBJECT":
					{
						this.parserExtensionCount = 0;
						this.parserObjectName = this.GetStringAttribute(node, "Name");
						string stringAttribute3 = this.GetStringAttribute(node, "Type");
						this.parserObjectTypeHasChanged = (stringAttribute3 != this.parserObjectType);
						this.parserObjectType = stringAttribute3;
						if (!flag)
						{
							this.parserObjectRow = this.AppendRow(26);
							this.InsertCell(this.parserObjectName, this.parserObjectRow, 0, CellType.STRING, this.objectNameCellStyle);
							return;
						}
						break;
					}
					case "COMMENT":
						if (!flag && node.ParentNode.Name.ToUpper() != "PROJECT")
						{
							string text2 = this.GetStringAttribute(node, "Value").Trim().Replace("`", "\r\n");
							if (text2 != string.Empty)
							{
								IRow row = this.AppendRow(-1);
								this.InsertCell(text2, row, 0, CellType.STRING, this.commentCellStyle);
								return;
							}
						}
						break;
					case "EXTENSION":
						this.parserExtensionCount++;
						if (!flag)
						{
							if (this.parserExtensionCount > 1)
							{
								IRow row = this.AppendRow(20);
								this.InsertCell(this.GetStringAttribute(node, "Name"), row, 0, CellType.STRING, this.extensionNameCellStyle);
								return;
							}
						}
						else if (this.parserExtensionCount > 1)
						{
							int num2 = 0;
							string a2;
							if ((a2 = this.parserObjectType.ToUpper()) != null)
							{
								if (!(a2 == "AREA"))
								{
									if (!(a2 == "PERIMETER"))
									{
										if (!(a2 == "LINE"))
										{
											if (a2 == "COUNTER")
											{
												num2 = 4;
											}
										}
										else
										{
											num2 = 3;
										}
									}
									else
									{
										num2 = 2;
									}
								}
								else
								{
									num2 = 1;
								}
							}
							this.parserPreset = new Preset(Guid.NewGuid().ToString(), this.GetStringAttribute(node, "Name"), this.parserObjectName, this.GetStringAttribute(node, "Name") + ";" + num2.ToString(), UnitScale.UnitSystem.undefined);
							this.parserPresets.Add(this.parserPreset);
							return;
						}
						break;
					case "FIELDS":
					case "RESULTS":
						if (!flag)
						{
							this.parserResultColumnIndex = 0;
							this.parserResultRowHeader = this.AppendRow(-1);
							this.parserResultRowData = this.AppendRow((node.Name.ToUpper() == "FIELDS") ? -1 : 16);
							return;
						}
						if (this.parserExtensionCount == 1)
						{
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
							this.parserResultRowData = this.AppendRow((node.Name.ToUpper() == "FIELDS") ? -1 : 16);
							this.InsertCell(this.parserObjectName, this.parserResultRowData, 0, CellType.STRING, this.objectNameCellStyle);
							return;
						}
						break;
					case "FIELD":
					case "RESULT":
					{
						if (!flag && this.parserResultColumnIndex == 7)
						{
							this.parserResultColumnIndex = 0;
							this.parserResultRowHeader = this.AppendRow(-1);
							this.parserResultRowData = this.AppendRow((node.Name.ToUpper() == "FIELD") ? -1 : 16);
						}
						bool flag2 = this.sheet.Equals(this.formattedSheet) || this.sheet.Equals(this.formattedSheetConsolidated);
						string text3 = this.GetStringAttribute(node, "Caption");
						string text4 = (node.Name.ToUpper() == "FIELD") ? "Value" : ((this.order == Report.ReportOrderEnum.ReportOrderByObjects) ? "TotalValue" : "Value");
						text4 = (flag2 ? text4 : ((text4 == "Value") ? "RawValue" : "TotalRawValue"));
						if (!flag2)
						{
							string text5 = this.GetStringAttribute(node, "Unit");
							text5 = ((text5 == Resources.unité_) ? "" : text5);
							if (text5 != string.Empty)
							{
								text3 = text3 + " (" + text5 + ")";
							}
						}
						if (!flag || this.parserExtensionCount == 1)
						{
							if (!flag || this.parserObjectTypeHasChanged)
							{
								this.InsertCell(text3, this.parserResultRowHeader, this.parserResultColumnIndex, CellType.STRING, this.columnHeaderCellStyle);
							}
							string stringAttribute4 = this.GetStringAttribute(node, text4);
							if (!flag2)
							{
								if (!Utilities.IsNumber(stringAttribute4.ToString()))
								{
									this.InsertCell(stringAttribute4, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
								}
								else
								{
									this.InsertCell(stringAttribute4, this.parserResultRowData, this.parserResultColumnIndex, CellType.NUMERIC, Utilities.IsInteger(stringAttribute4.ToString()) ? this.integerCellStyle : this.decimalCellStyle);
								}
							}
							else
							{
								this.InsertCell(stringAttribute4, this.parserResultRowData, this.parserResultColumnIndex, CellType.STRING, this.resultCellStyle);
							}
						}
						else if (flag)
						{
							PresetResult presetResult = new PresetResult(this.GetStringAttribute(node, "Name"), text3, this.GetStringAttribute(node, text4), "", "", ExtensionResult.ExtensionResultTypeEnum.ResultTypeUnit, false, true, -1, -1, -1);
							this.parserPreset.Results.Add(presetResult);
							if (!flag2)
							{
								Preset preset = this.parserPreset;
								preset.Tag = preset.Tag + text3 + ";";
							}
						}
						this.parserResultColumnIndex++;
						return;
					}

						return;
					}
				}
				else if ((a3 = node.Name.ToUpper()) != null)
				{
					if (!(a3 == "PLAN"))
					{
						if (a3 == "LAYER")
						{
							IRow row;
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
						if (a3 == "OBJECT")
						{
							this.parserExtensionCount = 0;
							this.parserObjectName = this.GetStringAttribute(node, "Name");
							this.parserObjectType = this.GetStringAttribute(node, "Type");
							this.parserObjectRow = this.AppendRow(-1);
							this.InsertCell(this.parserObjectName, this.parserObjectRow, 0, CellType.STRING, this.objectNameCellStyle);
							return;
						}
						if (a3 == "EXTENSION")
						{
							this.parserExtensionCount++;
							return;
						}
						if (!(a3 == "RESULT"))
						{
							return;
						}
						if (this.parserExtensionCount == 1)
						{
							bool flag3 = false;
							string stringAttribute5 = this.GetStringAttribute(node, "Name");
							string a4;
							if ((a4 = this.parserObjectType.ToUpper()) != null)
							{
								if (!(a4 == "AREA"))
								{
									if (!(a4 == "PERIMETER"))
									{
										if (!(a4 == "LINE"))
										{
											if (a4 == "COUNTER")
											{
												flag3 = (stringAttribute5 == "Count");
											}
										}
										else
										{
											flag3 = (stringAttribute5 == "Length");
										}
									}
									else
									{
										flag3 = (stringAttribute5 == "PerimeterMinusOpenings");
									}
								}
								else
								{
									flag3 = (stringAttribute5 == "AreaMinusSubstractions");
								}
							}
							if (flag3)
							{
								bool flag4 = this.sheet.Equals(this.formattedSheetShort);
								string text6 = (node.Name.ToUpper() == "FIELD") ? "Value" : ((this.order == Report.ReportOrderEnum.ReportOrderByObjects) ? "TotalValue" : "Value");
								text6 = (flag4 ? text6 : ((text6 == "Value") ? "RawValue" : "TotalRawValue"));
								string stringAttribute6 = this.GetStringAttribute(node, text6);
								if (!flag4)
								{
									this.InsertCell(stringAttribute6, this.parserObjectRow, 1, CellType.NUMERIC, Utilities.IsInteger(stringAttribute6.ToString()) ? this.integerCellStyle : this.decimalCellStyle);
								}
								else
								{
									this.InsertCell(stringAttribute6, this.parserObjectRow, 1, CellType.STRING, this.resultCellStyle);
								}
								if (!flag4)
								{
									string text7 = this.GetStringAttribute(node, "Unit");
									text7 = ((text7 == Resources.unité_) ? "" : text7);
									this.InsertCell(text7, this.parserObjectRow, 2, CellType.STRING, this.columnHeaderCellStyle);
								}
							}
						}
					}
					else if (node.HasChildNodes && node.FirstChild.HasChildNodes)
					{
						if (this.parserPlanName != "")
						{
							IRow row = this.AppendRow(10);
						}
						this.parserPlanName = this.GetStringAttribute(node, "Name");
						if (this.parserPlanName != "")
						{
							IRow row = this.AppendRow(28);
							this.InsertCell(this.parserPlanName, row, 0, CellType.STRING, this.planNameCellStyle);
							return;
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
					for (node = node.FirstChild; node != null; node = node.NextSibling)
					{
						this.ParseTree(node);
					}
				}
			}

			private void InsertSignature()
			{
				IRow row = this.AppendRow(-1);
				row = this.AppendRow(-1);
				row = this.AppendRow(-1);
				row = this.AppendRow(-1);
				this.InsertCell(Utilities.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan, row, 0, CellType.STRING, this.defaultCellStyle);
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
				if (this.sheet != this.estimatingSheet)
				{
					this.OutputConsolidatedExtensions();
				}
				else
				{
					if (this.order == Report.ReportOrderEnum.ReportOrderByPlans)
					{
						this.OutputReportSubTotal((this.sortBy == Report.ReportSortByEnum.ReportSortByPlans) ? this.parserPlanName : this.parserLayerName);
					}
					this.OutputReportTotals();
				}
				this.InsertSignature();
			}

			private bool GenerateXLSFromXML(string xml)
			{
				bool result;
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(xml);
					if (this.exportEstimating)
					{
						this.GenerateSheet(this.estimatingSheet, xmlDocument.DocumentElement);
					}
					else
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
					result = true;
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = false;
				}
				return result;
			}

			private void CreateWorkbook()
			{
				this.workbook = new HSSFWorkbook();
				string text = NumberFormatInfo.CurrentInfo.CurrencySymbol;
				text = text + "#,##0.00_);(" + text + "#,##0.00)";
				DocumentSummaryInformation documentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation();
				this.workbook.DocumentSummaryInformation = documentSummaryInformation;
				SummaryInformation summaryInformation = PropertySetFactory.CreateSummaryInformation();
				this.workbook.SummaryInformation = summaryInformation;
				IDataFormat dataFormat = this.workbook.CreateDataFormat();
				this.integerCellStyle = this.workbook.CreateCellStyle();
				this.integerCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT;
				this.integerCellStyle.WrapText = false;
				this.integerCellStyle.DataFormat = dataFormat.GetFormat("0");
				this.integerCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
				IDataFormat dataFormat2 = this.workbook.CreateDataFormat();
				this.decimalCellStyle = this.workbook.CreateCellStyle();
				this.decimalCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT;
				this.decimalCellStyle.WrapText = false;
				this.decimalCellStyle.DataFormat = dataFormat2.GetFormat("0.00");
				this.decimalCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
				this.resultCellStyle = this.workbook.CreateCellStyle();
				this.resultCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT;
				this.resultCellStyle.WrapText = false;
				this.resultCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
				this.planNameCellStyle = this.workbook.CreateCellStyle();
				this.planNameCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.planNameCellStyle.WrapText = false;
				this.planNameCellStyle.SetFont(this.GetFont(700, 0, 13, "Calibri", false, false, 0, 0));
				this.layerNameCellStyle = this.workbook.CreateCellStyle();
				this.layerNameCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.layerNameCellStyle.WrapText = false;
				this.layerNameCellStyle.SetFont(this.GetFont(700, 0, 12, "Calibri", false, false, 0, 0));
				this.objectNameCellStyle = this.workbook.CreateCellStyle();
				this.objectNameCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.objectNameCellStyle.WrapText = false;
				this.objectNameCellStyle.SetFont(this.GetFont(700, 0, 11, "Calibri", false, false, 0, 0));
				this.extensionNameCellStyle = this.workbook.CreateCellStyle();
				this.extensionNameCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.extensionNameCellStyle.WrapText = false;
				this.extensionNameCellStyle.SetFont(this.GetFont(700, 0, 11, "Calibri", true, false, 0, 1));
				this.columnHeaderCellStyle = this.workbook.CreateCellStyle();
				this.columnHeaderCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT;
				this.columnHeaderCellStyle.WrapText = true;
				this.columnHeaderCellStyle.SetFont(this.GetFont(700, 0, 11, "Calibri", true, false, 0, 0));
				this.commentCellStyle = this.workbook.CreateCellStyle();
				this.commentCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.commentCellStyle.WrapText = false;
				this.commentCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
				this.defaultCellStyle = this.workbook.CreateCellStyle();
				this.defaultCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.defaultCellStyle.WrapText = false;
				this.defaultCellStyle.SetFont(this.GetFont(0, 0, 11, "Calibri", false, false, 0, 0));
				this.estimatingItemCellStyle = this.workbook.CreateCellStyle();
				this.estimatingItemCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.estimatingItemCellStyle.WrapText = false;
				this.estimatingItemCellStyle.SetFont(this.GetFont(700, 0, 10, "Calibri", false, false, 0, 0));
				this.estimatingItemResultCellStyle = this.workbook.CreateCellStyle();
				this.estimatingItemResultCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.estimatingItemResultCellStyle.WrapText = false;
				this.estimatingItemResultCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
				this.estimatingItemPriceCellStyle = this.workbook.CreateCellStyle();
				this.estimatingItemPriceCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.estimatingItemPriceCellStyle.WrapText = false;
				this.estimatingItemPriceCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
				this.estimatingItemPriceCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat(text);
				this.estimatingEditableQuantityCellStyle = this.workbook.CreateCellStyle();
				this.estimatingEditableQuantityCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.estimatingEditableQuantityCellStyle.WrapText = false;
				this.estimatingEditableQuantityCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
				this.estimatingEditableQuantityCellStyle.FillForegroundColor = HSSFColor.YELLOW.index;
				this.estimatingEditableQuantityCellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
				this.estimatingEditableQuantityCellStyle.IsLocked = false;
				this.estimatingEditablePriceCellStyle = this.workbook.CreateCellStyle();
				this.estimatingEditablePriceCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.estimatingEditablePriceCellStyle.WrapText = false;
				this.estimatingEditablePriceCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
				this.estimatingEditablePriceCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat(text);
				this.estimatingEditablePriceCellStyle.FillForegroundColor = HSSFColor.YELLOW.index;
				this.estimatingEditablePriceCellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
				this.estimatingEditablePriceCellStyle.IsLocked = false;
				this.estimatingItemTotalCellStyle = this.workbook.CreateCellStyle();
				this.estimatingItemTotalCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.estimatingItemTotalCellStyle.WrapText = false;
				this.estimatingItemTotalCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
				this.estimatingItemTotalCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat(text);
				this.estimatingItemMarginCellStyle = this.workbook.CreateCellStyle();
				this.estimatingItemMarginCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
				this.estimatingItemMarginCellStyle.WrapText = false;
				this.estimatingItemMarginCellStyle.SetFont(this.GetFont(0, 0, 10, "Calibri", false, false, 0, 0));
				this.estimatingItemMarginCellStyle.DataFormat = this.workbook.CreateDataFormat().GetFormat("0.00%");
				this.estimatingSubTotalCaptionCellStyle = this.workbook.CreateCellStyle();
				this.estimatingSubTotalCaptionCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT;
				this.estimatingSubTotalCaptionCellStyle.WrapText = false;
				this.estimatingSubTotalCaptionCellStyle.SetFont(this.GetFont(700, 0, 10, "Calibri", false, false, 0, 0));
				if (this.exportEstimating)
				{
					this.estimatingSheet = this.workbook.CreateSheet(Resources.Rapport_d_estimation);
					this.estimatingSheet.DefaultColumnWidth = 14;
					this.estimatingSheet.SetColumnWidth(0, 6585);
					this.estimatingSheet.SetColumnWidth(2, 2233);
					return;
				}
				if (Settings.Default.ExportToExcelType != 1)
				{
					this.rawSheetConsolidated = this.workbook.CreateSheet(Resources.Données_brutes_consolidées);
					this.rawSheetConsolidated.DefaultColumnWidth = 14;
					this.rawSheetConsolidated.SetColumnWidth(0, 5305);
				}
				if (Settings.Default.ExportToExcelType != 0)
				{
					this.formattedSheetConsolidated = this.workbook.CreateSheet(Resources.Données_formatées_consolidées);
					this.formattedSheetConsolidated.DefaultColumnWidth = 15;
					this.formattedSheetConsolidated.SetColumnWidth(0, 5305);
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
					this.rawSheetShort.SetColumnWidth(0, 6585);
					this.rawSheetShort.SetColumnWidth(2, 1465);
				}
				if (Settings.Default.ExportToExcelType != 0)
				{
					this.formattedSheetShort = this.workbook.CreateSheet(Resources.Données_formatées_résumé);
					this.formattedSheetShort.DefaultColumnWidth = 15;
					this.formattedSheetShort.SetColumnWidth(0, 6585);
				}
			}

			private void SaveWorkbook(string fileName)
			{
				try
				{
					IL_00:
					using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
					{
						this.workbook.SetActiveSheet(0);
						this.workbook.Write(fileStream);
						fileStream.Close();
					}
				}
				catch (Exception ex)
				{
					if (ex.Message.IndexOf("process") != -1)
					{
						string fichier_utilisé_par_un_autre_processus = Resources.Fichier_utilisé_par_un_autre_processus;
						string veuillez_fermer_votre_tableur_Excel = Resources.Veuillez_fermer_votre_tableur_Excel;
						if (Utilities.DisplayWarningQuestionRetryCancel(fichier_utilisé_par_un_autre_processus, veuillez_fermer_votre_tableur_Excel) != DialogResult.Cancel)
						{
							goto IL_00;
						}
					}
					else
					{
						Utilities.DisplaySystemError(ex);
					}
				}
			}

			public bool Export(string xml, string fileName)
			{
				bool result;
				try
				{
					this.CreateWorkbook();
					this.GenerateXLSFromXML(xml);
					this.SaveWorkbook(fileName);
					result = true;
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = false;
				}
				return result;
			}

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

			private class PresetSorter : IComparer
			{
				public int Compare(object x, object y)
				{
					int result;
					try
					{
						Preset preset = x as Preset;
						Preset preset2 = y as Preset;
						result = StringLogicalComparer.Compare(preset.ExtensionName + (string)preset.Tag + preset.CategoryName, preset2.ExtensionName + (string)preset2.Tag + preset2.CategoryName);
					}
					catch (Exception)
					{
						result = -1;
					}
					return result;
				}

				public PresetSorter()
				{
				}
			}
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2
		{
			public <>c__DisplayClass2()
			{
			}

			public bool <BuildXMLLayerSummaries>b__0(ReportSummary x)
			{
				return x.GroupID == this.reportSummary.GroupID;
			}

			public ReportSummary reportSummary;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass6
		{
			public <>c__DisplayClass6()
			{
			}

			public bool <BuildXMLLayersSummaries>b__4(ReportSummary x)
			{
				return x.LayerName == this.reportSummary.LayerName;
			}

			public ReportSummary reportSummary;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClassa
		{
			public <>c__DisplayClassa()
			{
			}

			public bool <BuildXMLOrderByObjects>b__8(Variable x)
			{
				return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
			}

			public DrawObject layerObject;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClassf
		{
			public <>c__DisplayClassf()
			{
			}

			public bool <BuildXMLOrderByLayers>b__c(Variable x)
			{
				return x.Name == this.layer.Name;
			}

			public Layer layer;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass12
		{
			public <>c__DisplayClass12()
			{
			}

			public bool <BuildXMLOrderByLayers>b__d(Variable x)
			{
				return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
			}

			public ReportModule.<>c__DisplayClassf CS$<>8__locals10;

			public DrawObject layerObject;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass16
		{
			public <>c__DisplayClass16()
			{
			}

			public bool <BuildXMLOrderByPlans>b__14(Variable x)
			{
				return Utilities.ConvertToInt(x.Value) == this.layerObject.GroupID;
			}

			public DrawObject layerObject;
		}
	}
}
