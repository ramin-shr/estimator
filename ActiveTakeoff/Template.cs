using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace QuoterPlan
{
    public class Template : BaseFileInfo
    {
        public CEstimatingItems COfficeProducts
        {
            get;
            set;
        }

        public bool CreatedFromObject
        {
            get;
            set;
        }

        public bool DeletionForbidden
        {
            get;
            set;
        }

        public DrawObject DrawObject
        {
            get;
            set;
        }

        public CEstimatingItems EstimatingItems
        {
            get;
            set;
        }

        public string ID
        {
            get
            {
                return Path.GetFileNameWithoutExtension(base.FileName);
            }
        }

        public Presets Presets
        {
            get;
            private set;
        }

        public bool SystemTemplate
        {
            get;
            set;
        }

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
                Console.WriteLine(string.Concat("Name = ", this.DrawObject.Name));
                Console.WriteLine(string.Concat("Color = ", this.DrawObject.Color));
                Console.WriteLine(string.Concat("PenType = ", this.DrawObject.PenType));
            }
            this.Presets.Dump();
            this.EstimatingItems.Dump();
            this.COfficeProducts.Dump();
        }

        public bool Open(string fileName, ExtensionsSupport extensionSupport)
        {
            bool drawObject;
            this.Clear();
            base.FullFileName = fileName;
            try
            {
                using (XmlTextReader xmlTextReader = new XmlTextReader(fileName))
                {
                    this.ReadFromStream(xmlTextReader, extensionSupport);
                    xmlTextReader.Close();
                    drawObject = this.DrawObject != null;
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplayFileOpenError(fileName, exception);
                drawObject = false;
            }
            return drawObject;
        }

        private void ReadFromStream(XmlTextReader reader, ExtensionsSupport extensionSupport)
        {
            string stringAttribute;
            object obj;
            int num;
            HatchStylePickerCombo.HatchStylePickerEnum integerAttribute;
            string upper = "";
            Utilities.NumberDecimalSeparator();
            Preset preset = null;
        Label1:
            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                switch (nodeType)
                {
                    case XmlNodeType.Element:
                        {
                            upper = reader.Name.ToUpper();
                            string str = upper;
                            string str1 = str;
                            if (str == null)
                            {
                                continue;
                            }
                            if (u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x6000492u002d1 == null)
                            {
                                u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x6000492u002d1 = new Dictionary<string, int>(10)
                            {
                                { "TEMPLATE", 0 },
                                { "AREA", 1 },
                                { "PERIMETER", 2 },
                                { "COUNTER", 3 },
                                { "LINE", 4 },
                                { "EXTENSION", 5 },
                                { "CESTIMATINGITEM", 6 },
                                { "COFFICEPRODUCT", 7 },
                                { "CHOICE", 8 },
                                { "FIELD", 9 }
                            };
                            }
                            if (!u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x6000492u002d1.TryGetValue(str1, out num))
                            {
                                continue;
                            }
                            switch (num)
                            {
                                case 0:
                                    {
                                        this.SystemTemplate = Utilities.GetBoolAttribute(reader, "SystemTemplate", false);
                                        this.DeletionForbidden = Utilities.GetBoolAttribute(reader, "DeletionForbidden", false);
                                        continue;
                                    }
                                case 1:
                                    {
                                        if (Utilities.GetStringAttribute(reader, "Pattern", string.Empty) == string.Empty)
                                        {
                                            integerAttribute = HatchStylePickerCombo.HatchStylePickerEnum.Solid;
                                        }
                                        else
                                        {
                                            integerAttribute = (HatchStylePickerCombo.HatchStylePickerEnum)Utilities.GetIntegerAttribute(reader, "Pattern", -1);
                                        }
                                        HatchStylePickerCombo.HatchStylePickerEnum hatchStylePickerEnum = integerAttribute;
                                        DrawPolyLine drawPolyLine = new DrawPolyLine()
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            FillColor = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                            Pattern = hatchStylePickerEnum,
                                            Filled = true
                                        };
                                        DrawPolyLine drawPolyLine1 = drawPolyLine;
                                        drawPolyLine1.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), SlopeFactor.HipValleyEnum.hipValleyUnavailable);
                                        this.DrawObject = drawPolyLine1;
                                        continue;
                                    }
                                case 2:
                                    {
                                        DrawPolyLine drawPolyLine2 = new DrawPolyLine()
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            PenWidth = Utilities.GetIntegerAttribute(reader, "PenWidth", 4),
                                            Filled = false
                                        };
                                        DrawPolyLine drawPolyLine3 = drawPolyLine2;
                                        drawPolyLine3.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
                                        this.DrawObject = drawPolyLine3;
                                        continue;
                                    }
                                case 3:
                                    {
                                        DrawCounter drawCounter = new DrawCounter()
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            FillColor = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                            Shape = (DrawCounter.CounterShapeTypeEnum)Utilities.GetIntegerAttribute(reader, "Shape", 0),
                                            DefaultSize = Utilities.GetIntegerAttribute(reader, "DefaultSize", 80),
                                            Text = Utilities.GetStringAttribute(reader, "Text", "")
                                        };
                                        this.DrawObject = drawCounter;
                                        continue;
                                    }
                                case 4:
                                    {
                                        DrawLine drawLine = new DrawLine()
                                        {
                                            Name = Utilities.GetStringAttribute(reader, "Name", ""),
                                            Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                            PenWidth = Utilities.GetIntegerAttribute(reader, "PenWidth", 4)
                                        };
                                        DrawLine drawLine1 = drawLine;
                                        drawLine1.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
                                        this.DrawObject = drawLine1;
                                        continue;
                                    }
                                case 5:
                                    {
                                        UnitScale.UnitSystem unitSystem = UnitScale.UnitSystem.undefined;
                                        if (!this.SystemTemplate)
                                        {
                                            unitSystem = (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2);
                                        }
                                        else
                                        {
                                            unitSystem = UnitScale.DefaultUnitSystem();
                                        }
                                        string stringAttribute1 = Utilities.GetStringAttribute(reader, "Name", "");
                                        string caption = Utilities.GetStringAttribute(reader, "DisplayName", "");
                                        if (caption == "")
                                        {
                                            ExtensionCategory extensionCategory = null;
                                            Extension extension = extensionSupport.FindExtension(ref extensionCategory, stringAttribute1);
                                            if (extension != null)
                                            {
                                                caption = extension.Caption;
                                            }
                                        }
                                        if (caption == "")
                                        {
                                            caption = stringAttribute1;
                                        }
                                        caption = this.Presets.GetFreeDisplayName(caption, "");
                                        Guid guid = Guid.NewGuid();
                                        preset = new Preset(guid.ToString(), caption, Utilities.GetStringAttribute(reader, "Category", ""), stringAttribute1, unitSystem);
                                        this.Presets.Add(preset);
                                        continue;
                                    }
                                case 6:
                                    {
                                        CEstimatingItem cEstimatingItem = new CEstimatingItem()
                                        {
                                            ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                        };
                                        if (cEstimatingItem.ItemID == "")
                                        {
                                            continue;
                                        }
                                        cEstimatingItem.Description = Utilities.GetStringAttribute(reader, "Description", "");
                                        cEstimatingItem.Value = -1;
                                        cEstimatingItem.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                        cEstimatingItem.ItemType = (DBEstimatingItem.EstimatingItemType)Utilities.GetIntegerAttribute(reader, "ItemType", 0);
                                        cEstimatingItem.UnitMeasure = (DBEstimatingItem.UnitMeasureType)Utilities.GetIntegerAttribute(reader, "UnitMeasure", 0);
                                        cEstimatingItem.CoverageValue = Utilities.GetDoubleAttribute(reader, "CoverageValue", 0);
                                        cEstimatingItem.CoverageUnit = (double)Utilities.GetIntegerAttribute(reader, "CoverageUnit", 0);
                                        cEstimatingItem.SectionID = Utilities.GetIntegerAttribute(reader, "SectionID", 0);
                                        cEstimatingItem.SubSectionID = Utilities.GetIntegerAttribute(reader, "SubSectionID", 0);
                                        cEstimatingItem.BidCode = Utilities.GetStringAttribute(reader, "BidCode", "");
                                        cEstimatingItem.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
                                        cEstimatingItem.Tag = cEstimatingItem;
                                        this.EstimatingItems.Add(cEstimatingItem);
                                        continue;
                                    }
                                case 7:
                                    {
                                        CEstimatingItem doubleAttribute = new CEstimatingItem()
                                        {
                                            ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                        };
                                        if (doubleAttribute.ItemID == "")
                                        {
                                            continue;
                                        }
                                        doubleAttribute.Description = Utilities.GetStringAttribute(reader, "Description", "");
                                        doubleAttribute.Value = Utilities.GetDoubleAttribute(reader, "Cost", 0);
                                        doubleAttribute.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                        doubleAttribute.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
                                        doubleAttribute.Tag = doubleAttribute;
                                        this.COfficeProducts.Add(doubleAttribute);
                                        continue;
                                    }
                                case 8:
                                    {
                                        if (preset == null)
                                        {
                                            continue;
                                        }
                                        if (!this.SystemTemplate)
                                        {
                                            stringAttribute = Utilities.GetStringAttribute(reader, "Element", "");
                                        }
                                        else
                                        {
                                            stringAttribute = (UnitScale.DefaultUnitSystem() != UnitScale.UnitSystem.imperial ? Utilities.GetStringAttribute(reader, "metricElement", "") : Utilities.GetStringAttribute(reader, "imperialElement", ""));
                                        }
                                        preset.Choices.Add(new PresetChoice(Utilities.GetStringAttribute(reader, "Name", ""), stringAttribute));
                                        continue;
                                    }
                                case 9:
                                    {
                                        if (preset == null)
                                        {
                                            continue;
                                        }
                                        if (!this.SystemTemplate)
                                        {
                                            obj = Utilities.GetStringAttribute(reader, "Value", "");
                                        }
                                        else
                                        {
                                            obj = (UnitScale.DefaultUnitSystem() != UnitScale.UnitSystem.imperial ? Utilities.GetStringAttribute(reader, "metricValue", "") : Utilities.GetStringAttribute(reader, "imperialValue", ""));
                                        }
                                        preset.Fields.Add(new PresetField(Utilities.GetStringAttribute(reader, "Name", ""), obj));
                                        continue;
                                    }
                                default:
                                    {
                                        continue;
                                    }
                            }
                            break;
                        }
                    case XmlNodeType.Attribute:
                        {
                            continue;
                        }
                    case XmlNodeType.Text:
                        {
                            string str2 = upper;
                            if (str2 == null || !(str2 == "COMMENT"))
                            {
                                continue;
                            }
                            this.DrawObject.Comment = reader.Value.Trim().Replace("`", "\r\n");
                            continue;
                        }
                    default:
                        {
                            if (nodeType == XmlNodeType.EndElement)
                            {
                                break;
                            }
                            else
                            {
                                goto Label1;
                            }
                        }
                }
                string upper1 = reader.Name.ToUpper();
                if (upper1 == null || !(upper1 == "EXTENSION"))
                {
                    continue;
                }
                preset = null;
            }
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
                        streamWriter.Close();
                        base.FullFileName = fileName;
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
            sw.WriteLine("<?xml version=\"1.0\"?>");
            sw.WriteLine(string.Concat("<Template", (this.DeletionForbidden ? " DeletionForbidden=\"True\"" : string.Empty), ">"));
            string objectType = this.DrawObject.ObjectType;
            string str = string.Concat("Name=\"", Utilities.EscapeString(this.DrawObject.Name), "\" ");
            if (objectType == "Counter")
            {
                DrawCounter drawObject = (DrawCounter)this.DrawObject;
                str = string.Concat(str, "Text=\"", Utilities.EscapeString(drawObject.Text), "\" ");
            }
            object[] argb = new object[] { str, "Color=\"", null, null };
            Color color = this.DrawObject.Color;
            argb[2] = color.ToArgb();
            argb[3] = "\" ";
            str = string.Concat(argb);
            if (objectType == "Perimeter" || objectType == "Line")
            {
                object obj = str;
                object[] penWidth = new object[] { obj, "PenWidth=\"", this.DrawObject.PenWidth, "\" " };
                str = string.Concat(penWidth);
            }
            if (objectType == "Counter" || objectType == "Area")
            {
                object[] objArray = new object[] { str, "FillColor=\"", null, null };
                Color fillColor = this.DrawObject.FillColor;
                objArray[2] = fillColor.ToArgb();
                objArray[3] = "\" ";
                str = string.Concat(objArray);
            }
            if (objectType == "Area")
            {
                object obj1 = str;
                object[] pattern = new object[] { obj1, "Pattern=\"", (int)((DrawPolyLine)this.DrawObject).Pattern, "\" " };
                str = string.Concat(pattern);
            }
            if (objectType == "Counter")
            {
                object obj2 = str;
                object[] shape = new object[] { obj2, "Shape=\"", (int)((DrawCounter)this.DrawObject).Shape, "\" " };
                str = string.Concat(shape);
                object obj3 = str;
                object[] defaultSize = new object[] { obj3, "DefaultSize=\"", ((DrawCounter)this.DrawObject).DefaultSize, "\" " };
                str = string.Concat(defaultSize);
            }
            if (objectType == "Area" || objectType == "Perimeter" || objectType == "Line")
            {
                DrawLine drawLine = (DrawLine)this.DrawObject;
                if (drawLine.SlopeFactor.InternalValue > 0)
                {
                    object obj4 = str;
                    object[] internalValue = new object[] { obj4, "Slope=\"", drawLine.SlopeFactor.InternalValue, "\" " };
                    str = string.Concat(internalValue);
                    object obj5 = str;
                    object[] slopeType = new object[] { obj5, "SlopeType=\"", (int)drawLine.SlopeFactor.SlopeType, "\" " };
                    str = string.Concat(slopeType);
                    object obj6 = str;
                    object[] slopeApplyType = new object[] { obj6, "SlopeApply=\"", (int)drawLine.SlopeFactor.SlopeApplyType, "\" " };
                    str = string.Concat(slopeApplyType);
                    object obj7 = str;
                    object[] hipValley = new object[] { obj7, "HipValley=\"", (int)drawLine.SlopeFactor.HipValley, "\" " };
                    str = string.Concat(hipValley);
                }
            }
            string[] strArrays = new string[] { "\t<", objectType, " ", str, "/>" };
            sw.WriteLine(string.Concat(strArrays));
            if (this.DrawObject.Comment != string.Empty)
            {
                sw.WriteLine(string.Concat("\t<Comment>", Utilities.EscapeString(this.DrawObject.Comment.Replace("\n", "`").Replace("\r", "")), "</Comment>"));
            }
            foreach (Preset collection in this.Presets.Collection)
            {
                string str1 = "\t<Extension ";
                str1 = string.Concat(str1, "DisplayName=\"", Utilities.EscapeString(collection.DisplayName), "\" ");
                str1 = string.Concat(str1, "Name=\"", Utilities.EscapeString(collection.ExtensionName), "\" ");
                str1 = string.Concat(str1, "Category=\"", Utilities.EscapeString(collection.CategoryName), "\" ");
                object obj8 = str1;
                object[] scaleSystemType = new object[] { obj8, "ScaleType=\"", (int)collection.ScaleSystemType, "\"" };
                str1 = string.Concat(scaleSystemType);
                sw.WriteLine(string.Concat(str1, ">"));
                foreach (PresetChoice presetChoice in collection.Choices.Collection)
                {
                    string[] strArrays1 = new string[] { "\t\t<Choice Name=\"", Utilities.EscapeString(presetChoice.ChoiceName), "\" Element=\"", Utilities.EscapeString(presetChoice.ChoiceElementName), "\"/>" };
                    sw.WriteLine(string.Concat(strArrays1));
                }
                foreach (PresetField presetField in collection.Fields.Collection)
                {
                    object[] objArray1 = new object[] { "\t\t<Field Name=\"", Utilities.EscapeString(presetField.Name), "\" Value=\"", presetField.Value, "\"/>" };
                    sw.WriteLine(string.Concat(objArray1));
                }
                sw.WriteLine("\t</Extension>");
            }
            foreach (CEstimatingItem cEstimatingItem in this.EstimatingItems.Collection)
            {
                string str2 = "\t<CEstimatingItem ";
                str2 = string.Concat(str2, "ItemID=\"", cEstimatingItem.ItemID, "\" ");
                str2 = string.Concat(str2, "Description=\"", Utilities.EscapeString(cEstimatingItem.Description), "\" ");
                str2 = string.Concat(str2, "Unit=\"", Utilities.EscapeString(cEstimatingItem.Unit), "\" ");
                object obj9 = str2;
                object[] itemType = new object[] { obj9, "ItemType=\"", (int)cEstimatingItem.ItemType, "\" " };
                object obj10 = string.Concat(itemType);
                object[] unitMeasure = new object[] { obj10, "UnitMeasure=\"", (int)cEstimatingItem.UnitMeasure, "\" " };
                object obj11 = string.Concat(unitMeasure);
                object[] coverageValue = new object[] { obj11, "CoverageValue=\"", cEstimatingItem.CoverageValue, "\" " };
                object obj12 = string.Concat(coverageValue);
                object[] coverageUnit = new object[] { obj12, "CoverageUnit=\"", cEstimatingItem.CoverageUnit, "\" " };
                object obj13 = string.Concat(coverageUnit);
                object[] sectionID = new object[] { obj13, "SectionID=\"", cEstimatingItem.SectionID, "\" " };
                object obj14 = string.Concat(sectionID);
                object[] subSectionID = new object[] { obj14, "SubSectionID=\"", cEstimatingItem.SubSectionID, "\" " };
                str2 = string.Concat(subSectionID);
                str2 = string.Concat(str2, "BidCode=\"", Utilities.EscapeString(cEstimatingItem.BidCode), "\" ");
                str2 = string.Concat(str2, "Formula=\"", Utilities.EscapeString(cEstimatingItem.Formula), "\"");
                sw.WriteLine(string.Concat(str2, "/>"));
            }
            foreach (CEstimatingItem collection1 in this.COfficeProducts.Collection)
            {
                string str3 = "\t<COfficeProduct ";
                str3 = string.Concat(str3, "ItemID=\"", collection1.ItemID, "\" ");
                str3 = string.Concat(str3, "Description=\"", Utilities.EscapeString(collection1.Description), "\" ");
                object obj15 = str3;
                object[] value = new object[] { obj15, "Cost=\"", collection1.Value, "\" " };
                str3 = string.Concat(value);
                str3 = string.Concat(str3, "Unit=\"", Utilities.EscapeString(collection1.Unit), "\" ");
                str3 = string.Concat(str3, "Formula=\"", Utilities.EscapeString(collection1.Formula), "\"");
                sw.WriteLine(string.Concat(str3, "/>"));
            }
            sw.WriteLine("</Template>");
        }
    }
}