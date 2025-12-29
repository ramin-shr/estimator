using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace QuoterPlan
{
    public class Template : BaseFileInfo
    {
        public CEstimatingItems COfficeProducts { get; set; }

        public bool CreatedFromObject { get; set; }

        public bool DeletionForbidden { get; set; }

        public DrawObject DrawObject { get; set; }

        public CEstimatingItems EstimatingItems { get; set; }

        public string ID => Path.GetFileNameWithoutExtension(base.FileName);

        public Presets Presets { get; private set; }

        public bool SystemTemplate { get; set; }

        public Template()
        {
            this.Presets = new Presets();
            this.EstimatingItems = new CEstimatingItems();
            this.COfficeProducts = new CEstimatingItems();
            this.SystemTemplate = false;
        }

        public override void Clear()
        {
            base.Clear();
            this.DrawObject = null;
            this.Presets.Clear();
            this.EstimatingItems.Clear();
            this.COfficeProducts = new CEstimatingItems();
            this.SystemTemplate = false;
        }

        public override void Dump()
        {
            base.Dump();

            if (this.DrawObject != null)
            {
                Console.WriteLine("Name = " + this.DrawObject.Name);
                Console.WriteLine("Color = " + this.DrawObject.Color);
                Console.WriteLine("PenType = " + this.DrawObject.PenType);
            }

            this.Presets.Dump();
            this.EstimatingItems.Dump();
            this.COfficeProducts.Dump();
        }

        public bool Open(string fileName, ExtensionsSupport extensionSupport)
        {
            bool ok;

            this.Clear();
            base.FullFileName = fileName;

            try
            {
                using (XmlTextReader reader = new XmlTextReader(fileName))
                {
                    this.ReadFromStream(reader, extensionSupport);
                    ok = (this.DrawObject != null);
                }
            }
            catch (Exception ex)
            {
                Utilities.DisplayFileOpenError(fileName, ex);
                ok = false;
            }

            return ok;
        }

        private void ReadFromStream(XmlTextReader reader, ExtensionsSupport extensionSupport)
        {
            Utilities.NumberDecimalSeparator();

            Preset currentPreset = null;
            string currentElementName = string.Empty;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        {
                            currentElementName = reader.Name.ToUpperInvariant();

                            switch (currentElementName)
                            {
                                case "TEMPLATE":
                                    {
                                        this.SystemTemplate = Utilities.GetBoolAttribute(reader, "SystemTemplate", false);
                                        this.DeletionForbidden = Utilities.GetBoolAttribute(reader, "DeletionForbidden", false);
                                        break;
                                    }

                                case "AREA":
                                    {
                                        HatchStylePickerCombo.HatchStylePickerEnum pattern;
                                        string patternString = Utilities.GetStringAttribute(reader, "Pattern", string.Empty);
                                        if (patternString == string.Empty)
                                        {
                                            pattern = HatchStylePickerCombo.HatchStylePickerEnum.Solid;
                                        }
                                        else
                                        {
                                            pattern = (HatchStylePickerCombo.HatchStylePickerEnum)Utilities.GetIntegerAttribute(reader, "Pattern", -1);
                                        }

                                        DrawPolyLine area = new DrawPolyLine
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            FillColor = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                            Pattern = pattern,
                                            Filled = true
                                        };

                                        area.SetSlopeFactor(
                                            Utilities.GetDoubleAttribute(reader, "Slope", 0),
                                            (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0),
                                            (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1),
                                            SlopeFactor.HipValleyEnum.hipValleyUnavailable);

                                        this.DrawObject = area;
                                        break;
                                    }

                                case "PERIMETER":
                                    {
                                        DrawPolyLine perimeter = new DrawPolyLine
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            PenWidth = Utilities.GetIntegerAttribute(reader, "PenWidth", 4),
                                            Filled = false
                                        };

                                        perimeter.SetSlopeFactor(
                                            Utilities.GetDoubleAttribute(reader, "Slope", 0),
                                            (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0),
                                            (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1),
                                            (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));

                                        this.DrawObject = perimeter;
                                        break;
                                    }

                                case "COUNTER":
                                    {
                                        DrawCounter counter = new DrawCounter
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            FillColor = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                            Shape = (DrawCounter.CounterShapeTypeEnum)Utilities.GetIntegerAttribute(reader, "Shape", 0),
                                            DefaultSize = Utilities.GetIntegerAttribute(reader, "DefaultSize", 80),
                                            Text = Utilities.GetStringAttribute(reader, "Text", "")
                                        };

                                        this.DrawObject = counter;
                                        break;
                                    }

                                case "LINE":
                                    {
                                        DrawLine line = new DrawLine
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            PenWidth = Utilities.GetIntegerAttribute(reader, "PenWidth", 4)
                                        };

                                        line.SetSlopeFactor(
                                            Utilities.GetDoubleAttribute(reader, "Slope", 0),
                                            (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0),
                                            (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1),
                                            (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));

                                        this.DrawObject = line;
                                        break;
                                    }

                                case "EXTENSION":
                                    {
                                        UnitScale.UnitSystem scaleSystem;
                                        if (!this.SystemTemplate)
                                        {
                                            scaleSystem = (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2);
                                        }
                                        else
                                        {
                                            scaleSystem = UnitScale.DefaultUnitSystem();
                                        }

                                        string extensionName = Utilities.GetStringAttribute(reader, "Name", "");
                                        string displayName = Utilities.GetStringAttribute(reader, "DisplayName", "");

                                        if (displayName == "")
                                        {
                                            ExtensionCategory category = null;
                                            Extension extension = extensionSupport.FindExtension(ref category, extensionName);
                                            if (extension != null)
                                                displayName = extension.Caption;
                                        }

                                        if (displayName == "")
                                            displayName = extensionName;

                                        displayName = this.Presets.GetFreeDisplayName(displayName, "");

                                        currentPreset = new Preset(
                                            Guid.NewGuid().ToString(),
                                            displayName,
                                            Utilities.GetStringAttribute(reader, "Category", ""),
                                            extensionName,
                                            scaleSystem);

                                        this.Presets.Add(currentPreset);
                                        break;
                                    }

                                case "CESTIMATINGITEM":
                                    {
                                        CEstimatingItem estimatingItem = new CEstimatingItem
                                        {
                                            ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                        };

                                        if (estimatingItem.ItemID == "")
                                            break;

                                        estimatingItem.Description = Utilities.GetStringAttribute(reader, "Description", "");
                                        estimatingItem.Value = -1;
                                        estimatingItem.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                        estimatingItem.ItemType = (DBEstimatingItem.EstimatingItemType)Utilities.GetIntegerAttribute(reader, "ItemType", 0);
                                        estimatingItem.UnitMeasure = (DBEstimatingItem.UnitMeasureType)Utilities.GetIntegerAttribute(reader, "UnitMeasure", 0);
                                        estimatingItem.CoverageValue = Utilities.GetDoubleAttribute(reader, "CoverageValue", 0);
                                        estimatingItem.CoverageUnit = (double)Utilities.GetIntegerAttribute(reader, "CoverageUnit", 0);
                                        estimatingItem.SectionID = Utilities.GetIntegerAttribute(reader, "SectionID", 0);
                                        estimatingItem.SubSectionID = Utilities.GetIntegerAttribute(reader, "SubSectionID", 0);
                                        estimatingItem.BidCode = Utilities.GetStringAttribute(reader, "BidCode", "");
                                        estimatingItem.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
                                        estimatingItem.Tag = estimatingItem;

                                        this.EstimatingItems.Add(estimatingItem);
                                        break;
                                    }

                                case "COFFICEPRODUCT":
                                    {
                                        CEstimatingItem officeProduct = new CEstimatingItem
                                        {
                                            ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                        };

                                        if (officeProduct.ItemID == "")
                                            break;

                                        officeProduct.Description = Utilities.GetStringAttribute(reader, "Description", "");
                                        officeProduct.Value = Utilities.GetDoubleAttribute(reader, "Cost", 0);
                                        officeProduct.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                        officeProduct.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
                                        officeProduct.Tag = officeProduct;

                                        this.COfficeProducts.Add(officeProduct);
                                        break;
                                    }

                                case "CHOICE":
                                    {
                                        if (currentPreset == null)
                                            break;

                                        string elementValue;
                                        if (!this.SystemTemplate)
                                        {
                                            elementValue = Utilities.GetStringAttribute(reader, "Element", "");
                                        }
                                        else
                                        {
                                            elementValue = (UnitScale.DefaultUnitSystem() != UnitScale.UnitSystem.imperial)
                                                ? Utilities.GetStringAttribute(reader, "metricElement", "")
                                                : Utilities.GetStringAttribute(reader, "imperialElement", "");
                                        }

                                        currentPreset.Choices.Add(new PresetChoice(
                                            Utilities.GetStringAttribute(reader, "Name", ""),
                                            elementValue));

                                        break;
                                    }

                                case "FIELD":
                                    {
                                        if (currentPreset == null)
                                            break;

                                        string fieldValue;
                                        if (!this.SystemTemplate)
                                        {
                                            fieldValue = Utilities.GetStringAttribute(reader, "Value", "");
                                        }
                                        else
                                        {
                                            fieldValue = (UnitScale.DefaultUnitSystem() != UnitScale.UnitSystem.imperial)
                                                ? Utilities.GetStringAttribute(reader, "metricValue", "")
                                                : Utilities.GetStringAttribute(reader, "imperialValue", "");
                                        }

                                        currentPreset.Fields.Add(new PresetField(
                                            Utilities.GetStringAttribute(reader, "Name", ""),
                                            fieldValue));

                                        break;
                                    }
                            }

                            break;
                        }

                    case XmlNodeType.Text:
                        {
                            if (currentElementName == "COMMENT")
                            {
                                this.DrawObject.Comment = reader.Value.Trim().Replace("`", "\r\n");
                            }
                            break;
                        }

                    case XmlNodeType.EndElement:
                        {
                            string endName = reader.Name.ToUpperInvariant();
                            if (endName == "EXTENSION")
                                currentPreset = null;
                            break;
                        }
                }
            }
        }

        public bool Save(string fileName)
        {
            bool ok;

            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    this.SaveToStream(writer);
                    base.FullFileName = fileName;
                }

                ok = true;
            }
            catch (Exception ex)
            {
                Utilities.DisplayFileSaveError(fileName, ex);
                ok = false;
            }

            return ok;
        }

        private void SaveToStream(StreamWriter sw)
        {
            sw.WriteLine("<?xml version=\"1.0\"?>");
            sw.WriteLine(string.Concat("<Template", (this.DeletionForbidden ? " DeletionForbidden=\"True\"" : string.Empty), ">"));

            string objectType = this.DrawObject.ObjectType;
            string attributes = string.Concat("Name=\"", Utilities.EscapeString(this.DrawObject.Name), "\" ");

            if (objectType == "Counter")
            {
                DrawCounter counter = (DrawCounter)this.DrawObject;
                attributes = string.Concat(attributes, "Text=\"", Utilities.EscapeString(counter.Text), "\" ");
            }

            attributes = string.Concat(attributes, "Color=\"", this.DrawObject.Color.ToArgb(), "\" ");

            if (objectType == "Perimeter" || objectType == "Line")
            {
                attributes = string.Concat(attributes, "PenWidth=\"", this.DrawObject.PenWidth, "\" ");
            }

            if (objectType == "Counter" || objectType == "Area")
            {
                attributes = string.Concat(attributes, "FillColor=\"", this.DrawObject.FillColor.ToArgb(), "\" ");
            }

            if (objectType == "Area")
            {
                attributes = string.Concat(attributes, "Pattern=\"", (int)((DrawPolyLine)this.DrawObject).Pattern, "\" ");
            }

            if (objectType == "Counter")
            {
                attributes = string.Concat(attributes, "Shape=\"", (int)((DrawCounter)this.DrawObject).Shape, "\" ");
                attributes = string.Concat(attributes, "DefaultSize=\"", ((DrawCounter)this.DrawObject).DefaultSize, "\" ");
            }

            if (objectType == "Area" || objectType == "Perimeter" || objectType == "Line")
            {
                DrawLine line = (DrawLine)this.DrawObject;
                if (line.SlopeFactor.InternalValue > 0)
                {
                    attributes = string.Concat(attributes, "Slope=\"", line.SlopeFactor.InternalValue, "\" ");
                    attributes = string.Concat(attributes, "SlopeType=\"", (int)line.SlopeFactor.SlopeType, "\" ");
                    attributes = string.Concat(attributes, "SlopeApply=\"", (int)line.SlopeFactor.SlopeApplyType, "\" ");
                    attributes = string.Concat(attributes, "HipValley=\"", (int)line.SlopeFactor.HipValley, "\" ");
                }
            }

            sw.WriteLine(string.Concat("\t<", objectType, " ", attributes, "/>"));

            if (this.DrawObject.Comment != string.Empty)
            {
                sw.WriteLine(string.Concat("\t<Comment>", Utilities.EscapeString(this.DrawObject.Comment.Replace("\n", "`").Replace("\r", "")), "</Comment>"));
            }

            foreach (Preset preset in this.Presets.Collection)
            {
                string extensionLine = "\t<Extension ";
                extensionLine = string.Concat(extensionLine, "DisplayName=\"", Utilities.EscapeString(preset.DisplayName), "\" ");
                extensionLine = string.Concat(extensionLine, "Name=\"", Utilities.EscapeString(preset.ExtensionName), "\" ");
                extensionLine = string.Concat(extensionLine, "Category=\"", Utilities.EscapeString(preset.CategoryName), "\" ");
                extensionLine = string.Concat(extensionLine, "ScaleType=\"", (int)preset.ScaleSystemType, "\"");
                sw.WriteLine(string.Concat(extensionLine, ">"));

                foreach (PresetChoice choice in preset.Choices.Collection)
                {
                    sw.WriteLine(string.Concat("\t\t<Choice Name=\"", Utilities.EscapeString(choice.ChoiceName), "\" Element=\"", Utilities.EscapeString(choice.ChoiceElementName), "\"/>"));
                }

                foreach (PresetField field in preset.Fields.Collection)
                {
                    sw.WriteLine(string.Concat("\t\t<Field Name=\"", Utilities.EscapeString(field.Name), "\" Value=\"", field.Value, "\"/>"));
                }

                sw.WriteLine("\t</Extension>");
            }

            foreach (CEstimatingItem item in this.EstimatingItems.Collection)
            {
                string line = "\t<CEstimatingItem ";
                line = string.Concat(line, "ItemID=\"", item.ItemID, "\" ");
                line = string.Concat(line, "Description=\"", Utilities.EscapeString(item.Description), "\" ");
                line = string.Concat(line, "Unit=\"", Utilities.EscapeString(item.Unit), "\" ");
                line = string.Concat(line, "ItemType=\"", (int)item.ItemType, "\" ");
                line = string.Concat(line, "UnitMeasure=\"", (int)item.UnitMeasure, "\" ");
                line = string.Concat(line, "CoverageValue=\"", item.CoverageValue, "\" ");
                line = string.Concat(line, "CoverageUnit=\"", item.CoverageUnit, "\" ");
                line = string.Concat(line, "SectionID=\"", item.SectionID, "\" ");
                line = string.Concat(line, "SubSectionID=\"", item.SubSectionID, "\" ");
                line = string.Concat(line, "BidCode=\"", Utilities.EscapeString(item.BidCode), "\" ");
                line = string.Concat(line, "Formula=\"", Utilities.EscapeString(item.Formula), "\"");
                sw.WriteLine(string.Concat(line, "/>"));
            }

            foreach (CEstimatingItem item in this.COfficeProducts.Collection)
            {
                string line = "\t<COfficeProduct ";
                line = string.Concat(line, "ItemID=\"", item.ItemID, "\" ");
                line = string.Concat(line, "Description=\"", Utilities.EscapeString(item.Description), "\" ");
                line = string.Concat(line, "Cost=\"", item.Value, "\" ");
                line = string.Concat(line, "Unit=\"", Utilities.EscapeString(item.Unit), "\" ");
                line = string.Concat(line, "Formula=\"", Utilities.EscapeString(item.Formula), "\"");
                sw.WriteLine(string.Concat(line, "/>"));
            }

            sw.WriteLine("</Template>");
        }
    }
}
