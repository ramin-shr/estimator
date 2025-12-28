using System;
using System.IO;
using System.Xml;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class QuoteReportGenerator
	{
		public string Generate(MemoryStream memStream, Report.QuoteReportSortByEnum sortBy, Project projet, DBManagement dbManagement)
		{
			string result;
			try
			{
				this.sortBy = sortBy;
				this.project = projet;
				this.dbManagement = dbManagement;
				using (XmlTextReader xmlTextReader = new XmlTextReader(memStream))
				{
					this.ReadFromStream(xmlTextReader);
					xmlTextReader.Close();
					this.quoteSections.Dump();
					result = this.CreateXML();
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = "";
			}
			return result;
		}

		private string CreateXML()
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
			string companyName = Settings.Default.CompanyName;
			string companyFullAddress = Settings.Default.CompanyFullAddress;
			string jobNumber = this.project.JobNumber;
			string dateString = Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort());
			string companyRepresentative = Settings.Default.CompanyRepresentative;
			text = text + "\t<Company>" + Environment.NewLine;
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\t\t<CompanyName>",
				Utilities.EscapeString(companyName),
				"</CompanyName>",
				Environment.NewLine
			});
			string[] fields = Utilities.GetFields(companyFullAddress, new char[]
			{
				'\r',
				'\n'
			});
			if (fields.GetUpperBound(0) >= 0)
			{
				text += "\t\t<CompanyInfo>";
				foreach (string s in fields)
				{
					text = text + Utilities.EscapeString(s) + "&#xD;";
				}
				text = text + "</CompanyInfo>" + Environment.NewLine;
			}
			text = text + "\t</Company>" + Environment.NewLine;
			text = text + "\t<Quote>" + Environment.NewLine;
			string text4 = text;
			text = string.Concat(new string[]
			{
				text4,
				"\t\t<QuoteNumber>",
				jobNumber,
				"</QuoteNumber>",
				Environment.NewLine
			});
			string text5 = text;
			text = string.Concat(new string[]
			{
				text5,
				"\t\t<QuoteDate>",
				dateString,
				"</QuoteDate>",
				Environment.NewLine
			});
			string text6 = text;
			text = string.Concat(new string[]
			{
				text6,
				"\t\t<QuoteRepresentative>",
				Utilities.EscapeString(companyRepresentative),
				"</QuoteRepresentative>",
				Environment.NewLine
			});
			text = text + "\t</Quote>" + Environment.NewLine;
			text = text + "\t<Project>" + Environment.NewLine;
			string originalString = this.project.Name + '\r' + this.project.Description;
			fields = Utilities.GetFields(originalString, new char[]
			{
				'\r',
				'\n'
			});
			if (fields.GetUpperBound(0) >= 0)
			{
				text += "\t\t<ProjectInfo>";
				foreach (string s2 in fields)
				{
					text = text + Utilities.EscapeString(s2) + "&#xD;";
				}
				text = text + "</ProjectInfo>" + Environment.NewLine;
			}
			string originalString2 = this.project.ContactName + '\r' + this.project.ContactInfo;
			fields = Utilities.GetFields(originalString2, new char[]
			{
				'\r',
				'\n'
			});
			if (fields.GetUpperBound(0) >= 0)
			{
				text += "\t\t<ContactInfo>";
				foreach (string s3 in fields)
				{
					text = text + Utilities.EscapeString(s3) + "&#xD;";
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
				foreach (string s4 in fields)
				{
					text = text + Utilities.EscapeString(s4) + "&#xD;";
				}
				text = text + "</Comment>" + Environment.NewLine;
			}
			text = text + "\t</Project>" + Environment.NewLine;
			string[] array5 = this.dbManagement.SortHashTable(this.quoteSections.Collection, true);
			double num = 0.0;
			text = text + "\t<Sections>" + Environment.NewLine;
			object obj;
			for (int m = 0; m < this.quoteSections.Count; m++)
			{
				QuoteSection quoteSection = this.quoteSections[Utilities.ConvertToInt(array5[m])];
				if (quoteSection != null && quoteSection.QuoteItems.Count > 0)
				{
					text = text + "\t\t<Section Description=\"" + Utilities.EscapeString(quoteSection.Description) + "\" ";
					text = text + "Total=\"" + string.Format("{0:C}", quoteSection.Total) + "\" ";
					obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"RawTotal=\"",
						quoteSection.Total,
						"\""
					});
					text = text + ">" + Environment.NewLine;
					num += quoteSection.Total;
					string[] array6 = this.dbManagement.SortHashTable(quoteSection.QuoteItems.Collection, false);
					text = text + "\t\t\t<Items>" + Environment.NewLine;
					for (int n = 0; n < quoteSection.QuoteItems.Count; n++)
					{
						QuoteItem quoteItem = quoteSection.QuoteItems[array6[n]];
						if (quoteItem != null)
						{
							text += "\t\t\t\t<Item ";
							text = text + "Description=\"" + Utilities.EscapeString(quoteItem.Description) + "\" ";
							object obj2 = text;
							text = string.Concat(new object[]
							{
								obj2,
								"Quantity=\"",
								quoteItem.Quantity,
								"\" "
							});
							text = text + "Unit=\"" + Utilities.EscapeString(quoteItem.Unit) + "\" ";
							text = text + "PriceEach=\"" + string.Format("{0:C}", quoteItem.PriceEach) + "\" ";
							text = text + "Total=\"" + string.Format("{0:C}", quoteItem.Total) + "\" ";
							object obj3 = text;
							text = string.Concat(new object[]
							{
								obj3,
								"RawPriceEach=\"",
								quoteItem.PriceEach,
								"\" "
							});
							object obj4 = text;
							text = string.Concat(new object[]
							{
								obj4,
								"RawTotal=\"",
								quoteItem.Total,
								"\""
							});
							text = text + "/>" + Environment.NewLine;
						}
					}
					text = text + "\t\t\t</Items>" + Environment.NewLine;
					text = text + "\t\t</Section>" + Environment.NewLine;
				}
			}
			text = text + "\t</Sections>" + Environment.NewLine;
			text = text + "\t<Totals>" + Environment.NewLine;
			for (int num2 = 0; num2 < this.quoteSections.Count; num2++)
			{
				QuoteSection quoteSection2 = this.quoteSections[Utilities.ConvertToInt(array5[num2])];
				if (quoteSection2 != null && quoteSection2.QuoteItems.Count > 0)
				{
					text = text + "\t\t<SubTotal Description=\"" + Utilities.EscapeString(quoteSection2.Description) + "\" ";
					text = text + "Total=\"" + string.Format("{0:C}", quoteSection2.Total) + "\" ";
					obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"RawTotal=\"",
						quoteSection2.Total,
						"\""
					});
					text = text + "/>" + Environment.NewLine;
				}
			}
			bool taxOnTax = Settings.Default.TaxOnTax;
			string tax1Label = Settings.Default.Tax1Label;
			string tax2Label = Settings.Default.Tax2Label;
			double tax1Rate = Settings.Default.Tax1Rate;
			double tax2Rate = Settings.Default.Tax2Rate;
			string text7 = tax1Label + " (" + string.Format("{0:0.#####%}", tax1Rate) + ") :";
			string text8 = tax2Label + " (" + string.Format("{0:0.#####%}", tax2Rate) + ") :";
			double num3 = num * tax1Rate;
			num3 = Math.Round(num3, 2);
			double num4 = taxOnTax ? ((num3 + num) * tax2Rate) : (num * tax2Rate);
			num4 = Math.Round(num4, 2);
			double num5 = num + num3 + num4;
			num5 = Math.Round(num5, 2);
			obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"\t\t<GrandTotal Total=\"",
				string.Format("{0:C}", num),
				"\" RawTotal=\"",
				num,
				"\" Taxe1Rate=\"",
				string.Format("{0:0.00%}", tax1Rate),
				"\" RawTaxe1Rate=\"",
				tax1Rate,
				"\" Taxe2Rate=\"",
				string.Format("{0:0.00%}", tax2Rate),
				"\" RawTaxe2Rate=\"",
				tax2Rate,
				"\" Taxe1Caption=\"",
				text7,
				"\" Taxe2Caption=\"",
				text8,
				"\" Taxe1Total=\"",
				string.Format("{0:C}", num3),
				"\" RawTaxe1Total=\"",
				num3,
				"\" Taxe2Total=\"",
				string.Format("{0:C}", num4),
				"\" RawTaxe2Total=\"",
				num4,
				"\" TotalAfterTaxes=\"",
				string.Format("{0:C}", num5),
				"\" RawTotalAfterTaxes=\"",
				num5,
				"\"/>",
				Environment.NewLine
			});
			text = text + "\t</Totals>" + Environment.NewLine;
			return text + "</Report>" + Environment.NewLine;
		}

		private void ReadFromStream(XmlTextReader reader)
		{
			while (reader.Read())
			{
				XmlNodeType nodeType = reader.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				{
					string a;
					if ((a = reader.Name.ToUpper()) != null)
					{
						if (!(a == "EXTENSION"))
						{
							if (!(a == "OBJECT"))
							{
								if (a == "RESULT")
								{
									if (this.groupName != "")
									{
										int integerAttribute = Utilities.GetIntegerAttribute(reader, "ItemID", 0);
										string text = Utilities.GetStringAttribute(reader, "Caption", "");
										if (this.baseExtension)
										{
											text = this.groupName + " - " + text;
										}
										string stringAttribute = Utilities.GetStringAttribute(reader, "Unit", "");
										double doubleAttribute = Utilities.GetDoubleAttribute(reader, "Quantity", 0.0);
										double doubleAttribute2 = Utilities.GetDoubleAttribute(reader, "RawPriceEach", 0.0);
										int integerAttribute2 = Utilities.GetIntegerAttribute(reader, "SectionID", 0);
										DBEstimatingItem.EstimatingItemType integerAttribute3 = (DBEstimatingItem.EstimatingItemType)Utilities.GetIntegerAttribute(reader, "ItemType", 0);
										bool flag = integerAttribute > 0 || integerAttribute2 > 0;
										int num = 0;
										string description = "";
										switch (this.sortBy)
										{
										case Report.QuoteReportSortByEnum.QuoteReportSortBySections:
											if (integerAttribute2 == 0)
											{
												num = 999;
												description = Resources.Non_classé;
											}
											else
											{
												DBEstimatingSection dbestimatingSection = this.dbManagement.CSICodesA[integerAttribute2];
												if (dbestimatingSection == null)
												{
													num = 999;
													description = Resources.Non_classé;
												}
												else
												{
													num = dbestimatingSection.ID;
													description = dbestimatingSection.ID.ToString() + ". " + dbestimatingSection.Name;
												}
											}
											break;
										case Report.QuoteReportSortByEnum.QuoteReportSortByTypes:
											if (integerAttribute2 == 0)
											{
												num = 999;
												description = Resources.Non_classé;
											}
											else
											{
												num = (int)(integerAttribute3 + 1);
												description = DBEstimatingItem.GetItemTypeCaption(integerAttribute3);
											}
											break;
										case Report.QuoteReportSortByEnum.QuoteReportSortByList:
											description = Resources.Tous_les_items;
											break;
										}
										QuoteSection quoteSection = this.quoteSections[num];
										if (quoteSection == null)
										{
											quoteSection = new QuoteSection(num, description);
											this.quoteSections.Add(quoteSection);
										}
										if (quoteSection != null && doubleAttribute2 > 0.0 && doubleAttribute > 0.0)
										{
											string text2 = string.Concat(new string[]
											{
												text,
												";",
												stringAttribute,
												";",
												doubleAttribute2.ToString(),
												";",
												(this.baseExtension && !flag) ? this.groupName : "",
												";"
											});
											QuoteItem quoteItem = quoteSection.QuoteItems[text2];
											if (quoteItem == null)
											{
												quoteItem = new QuoteItem(text2, doubleAttribute, doubleAttribute2, text, stringAttribute, quoteSection);
												quoteSection.QuoteItems.Add(quoteItem);
											}
											else
											{
												quoteItem.Quantity += doubleAttribute;
											}
											quoteSection.Total += doubleAttribute * doubleAttribute2;
										}
									}
								}
							}
							else if (!Utilities.GetBoolAttribute(reader, "IsLabel", false))
							{
								this.groupName = Utilities.GetStringAttribute(reader, "Name", string.Empty);
							}
							else
							{
								this.groupName = "";
							}
						}
						else
						{
							this.baseExtension = Utilities.GetBoolAttribute(reader, "BaseExtension", false);
						}
					}
					break;
				}
				case XmlNodeType.Attribute:
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType == XmlNodeType.EndElement)
					{
						string a2;
						if ((a2 = reader.Name.ToUpper()) != null)
						{
							a2 == "EXTENSION";
						}
					}
					break;
				}
			}
		}

		public QuoteReportGenerator()
		{
		}

		private string groupName = "";

		private bool baseExtension;

		private QuoteSections quoteSections = new QuoteSections();

		private Report.QuoteReportSortByEnum sortBy;

		private Project project;

		private DBManagement dbManagement;
	}
}
