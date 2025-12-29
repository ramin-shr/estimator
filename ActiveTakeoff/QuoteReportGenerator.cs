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
            try
            {
                this.sortBy = sortBy;
                this.project = projet;
                this.dbManagement = dbManagement;

                using (XmlTextReader reader = new XmlTextReader(memStream))
                {
                    this.ReadFromStream(reader);
                    reader.Close();

                    this.quoteSections.Dump();
                    return this.CreateXML();
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                return "";
            }
        }

        private string CreateXML()
        {
            string reportDate = Utilities.GetDateString(Utilities.FormatDate(DateTime.Now), Utilities.GetCurrentValidUICultureShort());

            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + Environment.NewLine;
            xml = xml + "<Report Title=\"" + Utilities.EscapeString(this.project.Name) + "\" Date=\"" + reportDate + "\">" + Environment.NewLine;

            string companyName = Settings.Default.CompanyName;
            string companyFullAddress = Settings.Default.CompanyFullAddress;
            string quoteNumber = this.project.JobNumber;
            string companyRepresentative = Settings.Default.CompanyRepresentative;

            xml = xml + "\t<Company>" + Environment.NewLine;
            xml = xml + "\t\t<CompanyName>" + Utilities.EscapeString(companyName) + "</CompanyName>" + Environment.NewLine;

            string[] addressLines = Utilities.GetFields(companyFullAddress, new char[] { '\r', '\n' });
            if (addressLines.GetUpperBound(0) >= 0)
            {
                xml += "\t\t<CompanyInfo>";
                foreach (string line in addressLines)
                {
                    xml = xml + Utilities.EscapeString(line) + "&#xD;";
                }
                xml = xml + "</CompanyInfo>" + Environment.NewLine;
            }

            xml = xml + "\t</Company>" + Environment.NewLine;

            xml = xml + "\t<Quote>" + Environment.NewLine;
            xml = xml + "\t\t<QuoteNumber>" + quoteNumber + "</QuoteNumber>" + Environment.NewLine;
            xml = xml + "\t\t<QuoteDate>" + reportDate + "</QuoteDate>" + Environment.NewLine;
            xml = xml + "\t\t<QuoteRepresentative>" + Utilities.EscapeString(companyRepresentative) + "</QuoteRepresentative>" + Environment.NewLine;
            xml = xml + "\t</Quote>" + Environment.NewLine;

            xml = xml + "\t<Project>" + Environment.NewLine;

            string projectInfoRaw = this.project.Name + '\r' + this.project.Description;
            string[] projectInfoLines = Utilities.GetFields(projectInfoRaw, new char[] { '\r', '\n' });
            if (projectInfoLines.GetUpperBound(0) >= 0)
            {
                xml += "\t\t<ProjectInfo>";
                foreach (string line in projectInfoLines)
                {
                    xml = xml + Utilities.EscapeString(line) + "&#xD;";
                }
                xml = xml + "</ProjectInfo>" + Environment.NewLine;
            }

            string contactInfoRaw = this.project.ContactName + '\r' + this.project.ContactInfo;
            string[] contactInfoLines = Utilities.GetFields(contactInfoRaw, new char[] { '\r', '\n' });
            if (contactInfoLines.GetUpperBound(0) >= 0)
            {
                xml += "\t\t<ContactInfo>";
                foreach (string line in contactInfoLines)
                {
                    xml = xml + Utilities.EscapeString(line) + "&#xD;";
                }
                xml = xml + "</ContactInfo>" + Environment.NewLine;
            }

            string[] commentLines = Utilities.GetFields(this.project.Comment, new char[] { '\r', '\n' });
            if (commentLines.GetUpperBound(0) >= 0)
            {
                xml += "\t\t<Comment>";
                foreach (string line in commentLines)
                {
                    xml = xml + Utilities.EscapeString(line) + "&#xD;";
                }
                xml = xml + "</Comment>" + Environment.NewLine;
            }

            xml = xml + "\t</Project>" + Environment.NewLine;

            string[] sectionKeys = this.dbManagement.SortHashTable(this.quoteSections.Collection, true);

            double totalBeforeTaxes = 0.0;

            xml = xml + "\t<Sections>" + Environment.NewLine;

            for (int sectionIndex = 0; sectionIndex < this.quoteSections.Count; sectionIndex++)
            {
                QuoteSection section = this.quoteSections[Utilities.ConvertToInt(sectionKeys[sectionIndex])];
                if (section == null || section.QuoteItems.Count <= 0)
                    continue;

                xml = xml + "\t\t<Section Description=\"" + Utilities.EscapeString(section.Description) + "\" ";
                xml = xml + "Total=\"" + string.Format("{0:C}", section.Total) + "\" ";
                xml = xml + "RawTotal=\"" + section.Total + "\"";
                xml = xml + ">" + Environment.NewLine;

                totalBeforeTaxes += section.Total;

                string[] itemKeys = this.dbManagement.SortHashTable(section.QuoteItems.Collection, false);

                xml = xml + "\t\t\t<Items>" + Environment.NewLine;

                for (int itemIndex = 0; itemIndex < section.QuoteItems.Count; itemIndex++)
                {
                    QuoteItem item = section.QuoteItems[itemKeys[itemIndex]];
                    if (item == null)
                        continue;

                    xml += "\t\t\t\t<Item ";
                    xml = xml + "Description=\"" + Utilities.EscapeString(item.Description) + "\" ";
                    xml = xml + "Quantity=\"" + item.Quantity + "\" ";
                    xml = xml + "Unit=\"" + Utilities.EscapeString(item.Unit) + "\" ";
                    xml = xml + "PriceEach=\"" + string.Format("{0:C}", item.PriceEach) + "\" ";
                    xml = xml + "Total=\"" + string.Format("{0:C}", item.Total) + "\" ";
                    xml = xml + "RawPriceEach=\"" + item.PriceEach + "\" ";
                    xml = xml + "RawTotal=\"" + item.Total + "\"";
                    xml = xml + "/>" + Environment.NewLine;
                }

                xml = xml + "\t\t\t</Items>" + Environment.NewLine;
                xml = xml + "\t\t</Section>" + Environment.NewLine;
            }

            xml = xml + "\t</Sections>" + Environment.NewLine;

            xml = xml + "\t<Totals>" + Environment.NewLine;

            for (int sectionIndex = 0; sectionIndex < this.quoteSections.Count; sectionIndex++)
            {
                QuoteSection section = this.quoteSections[Utilities.ConvertToInt(sectionKeys[sectionIndex])];
                if (section == null || section.QuoteItems.Count <= 0)
                    continue;

                xml = xml + "\t\t<SubTotal Description=\"" + Utilities.EscapeString(section.Description) + "\" ";
                xml = xml + "Total=\"" + string.Format("{0:C}", section.Total) + "\" ";
                xml = xml + "RawTotal=\"" + section.Total + "\"";
                xml = xml + "/>" + Environment.NewLine;
            }

            bool taxOnTax = Settings.Default.TaxOnTax;
            string tax1Label = Settings.Default.Tax1Label;
            string tax2Label = Settings.Default.Tax2Label;
            double tax1Rate = Settings.Default.Tax1Rate;
            double tax2Rate = Settings.Default.Tax2Rate;

            string tax1Caption = tax1Label + " (" + string.Format("{0:0.#####%}", tax1Rate) + ") :";
            string tax2Caption = tax2Label + " (" + string.Format("{0:0.#####%}", tax2Rate) + ") :";

            double tax1Total = totalBeforeTaxes * tax1Rate;
            tax1Total = Math.Round(tax1Total, 2);

            double tax2Total = taxOnTax ? ((tax1Total + totalBeforeTaxes) * tax2Rate) : (totalBeforeTaxes * tax2Rate);
            tax2Total = Math.Round(tax2Total, 2);

            double totalAfterTaxes = totalBeforeTaxes + tax1Total + tax2Total;
            totalAfterTaxes = Math.Round(totalAfterTaxes, 2);

            xml = xml
                + "\t\t<GrandTotal Total=\"" + string.Format("{0:C}", totalBeforeTaxes)
                + "\" RawTotal=\"" + totalBeforeTaxes
                + "\" Taxe1Rate=\"" + string.Format("{0:0.00%}", tax1Rate)
                + "\" RawTaxe1Rate=\"" + tax1Rate
                + "\" Taxe2Rate=\"" + string.Format("{0:0.00%}", tax2Rate)
                + "\" RawTaxe2Rate=\"" + tax2Rate
                + "\" Taxe1Caption=\"" + tax1Caption
                + "\" Taxe2Caption=\"" + tax2Caption
                + "\" Taxe1Total=\"" + string.Format("{0:C}", tax1Total)
                + "\" RawTaxe1Total=\"" + tax1Total
                + "\" Taxe2Total=\"" + string.Format("{0:C}", tax2Total)
                + "\" RawTaxe2Total=\"" + tax2Total
                + "\" TotalAfterTaxes=\"" + string.Format("{0:C}", totalAfterTaxes)
                + "\" RawTotalAfterTaxes=\"" + totalAfterTaxes
                + "\"/>" + Environment.NewLine;

            xml = xml + "\t</Totals>" + Environment.NewLine;
            return xml + "</Report>" + Environment.NewLine;
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
                            string elementNameUpper = reader.Name.ToUpper();

                            if (elementNameUpper == "EXTENSION")
                            {
                                this.baseExtension = Utilities.GetBoolAttribute(reader, "BaseExtension", false);
                                break;
                            }

                            if (elementNameUpper == "OBJECT")
                            {
                                if (!Utilities.GetBoolAttribute(reader, "IsLabel", false))
                                    this.groupName = Utilities.GetStringAttribute(reader, "Name", string.Empty);
                                else
                                    this.groupName = "";

                                break;
                            }

                            if (elementNameUpper == "RESULT")
                            {
                                if (this.groupName == "")
                                    break;

                                int itemId = Utilities.GetIntegerAttribute(reader, "ItemID", 0);
                                string caption = Utilities.GetStringAttribute(reader, "Caption", "");
                                if (this.baseExtension)
                                {
                                    caption = this.groupName + " - " + caption;
                                }

                                string unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                double quantity = Utilities.GetDoubleAttribute(reader, "Quantity", 0.0);
                                double rawPriceEach = Utilities.GetDoubleAttribute(reader, "RawPriceEach", 0.0);
                                int sectionId = Utilities.GetIntegerAttribute(reader, "SectionID", 0);
                                DBEstimatingItem.EstimatingItemType itemType =
                                    (DBEstimatingItem.EstimatingItemType)Utilities.GetIntegerAttribute(reader, "ItemType", 0);

                                bool hasItemOrSection = (itemId > 0 || sectionId > 0);

                                int groupKey = 0;
                                string groupDescription = "";

                                switch (this.sortBy)
                                {
                                    case Report.QuoteReportSortByEnum.QuoteReportSortBySections:
                                        if (sectionId == 0)
                                        {
                                            groupKey = 999;
                                            groupDescription = Resources.Non_classé;
                                        }
                                        else
                                        {
                                            DBEstimatingSection estimatingSection = this.dbManagement.CSICodesA[sectionId];
                                            if (estimatingSection == null)
                                            {
                                                groupKey = 999;
                                                groupDescription = Resources.Non_classé;
                                            }
                                            else
                                            {
                                                groupKey = estimatingSection.ID;
                                                groupDescription = estimatingSection.ID.ToString() + ". " + estimatingSection.Name;
                                            }
                                        }
                                        break;

                                    case Report.QuoteReportSortByEnum.QuoteReportSortByTypes:
                                        if (sectionId == 0)
                                        {
                                            groupKey = 999;
                                            groupDescription = Resources.Non_classé;
                                        }
                                        else
                                        {
                                            groupKey = (int)(itemType + 1);
                                            groupDescription = DBEstimatingItem.GetItemTypeCaption(itemType);
                                        }
                                        break;

                                    case Report.QuoteReportSortByEnum.QuoteReportSortByList:
                                        groupDescription = Resources.Tous_les_items;
                                        break;
                                }

                                QuoteSection quoteSection = this.quoteSections[groupKey];
                                if (quoteSection == null)
                                {
                                    quoteSection = new QuoteSection(groupKey, groupDescription);
                                    this.quoteSections.Add(quoteSection);
                                }

                                if (quoteSection != null && rawPriceEach > 0.0 && quantity > 0.0)
                                {
                                    string groupPrefix = (this.baseExtension && !hasItemOrSection) ? this.groupName : "";

                                    string itemKey = caption + ";" + unit + ";" + rawPriceEach.ToString() + ";" + groupPrefix + ";";

                                    QuoteItem quoteItem = quoteSection.QuoteItems[itemKey];
                                    if (quoteItem == null)
                                    {
                                        quoteItem = new QuoteItem(itemKey, quantity, rawPriceEach, caption, unit, quoteSection);
                                        quoteSection.QuoteItems.Add(quoteItem);
                                    }
                                    else
                                    {
                                        quoteItem.Quantity += quantity;
                                    }

                                    quoteSection.Total += quantity * rawPriceEach;
                                }

                                break;
                            }

                            break;
                        }

                    case XmlNodeType.Attribute:
                    case XmlNodeType.Text:
                        break;

                    default:
                        if (nodeType == XmlNodeType.EndElement)
                        {
                            string endElementUpper = reader.Name.ToUpper();
                            bool isExtensionEnd = (endElementUpper == "EXTENSION"); // keep “no effect” behaviour, but valid C#
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
