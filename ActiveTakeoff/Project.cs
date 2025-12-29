using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace QuoterPlan
{
    public class Project : BaseFileInfo
    {
        private DrawingArea drawArea;

        private string saveFileName = string.Empty;

        public string Comment
        {
            get;
            set;
        }

        public string ContactInfo
        {
            get;
            set;
        }

        public string ContactName
        {
            get;
            set;
        }

        public string CreationDate
        {
            get;
            set;
        }

        public string CreationParentFolder
        {
            get;
            set;
        }

        public DBManagement DBManagement
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            set;
        }

        public string DisplayName
        {
            get
            {
                if (base.FileName != "")
                {
                    return Path.GetFileNameWithoutExtension(base.FileName);
                }
                return string.Concat(base.Name, " (", Utilities.GetDateString(this.CreationDate, Utilities.GetCurrentValidUICultureShort()).Replace(",", "").Replace(" ", "-"), ")");
            }
        }

        public bool DisplayResultsForAllPlans
        {
            get;
            set;
        }

        public EstimatingItems EstimatingItems
        {
            get;
            private set;
        }

        public ExtensionsSupport ExtensionsSupport
        {
            get;
            private set;
        }

        public int GroupCounter
        {
            get;
            set;
        }

        public DrawObjectGroups Groups
        {
            get;
            private set;
        }

        public string JobNumber
        {
            get;
            set;
        }

        public string LastModified
        {
            get;
            set;
        }

        public int ObjectCounter
        {
            get;
            set;
        }

        public Plans Plans
        {
            get;
            private set;
        }

        public Report Report
        {
            get;
            private set;
        }

        public Workspace Workspace
        {
            get;
            private set;
        }

        public Project()
        {
            this.Plans = new Plans();
            this.Groups = new DrawObjectGroups();
            this.Workspace = new Workspace();
            this.Report = new Report();
            this.EstimatingItems = new EstimatingItems(this);
            this.Clear();
        }

        private void CleanUpGroups()
        {
            for (int i = this.Groups.Count - 1; i >= 0; i--)
            {
                DrawObjectGroup item = this.Groups[i];
                DrawObject drawObject = this.drawArea.FindObjectFromGroupID(this, item.ID);
                if (drawObject != null)
                {
                    item.Name = drawObject.Name;
                    item.ObjectType = drawObject.ObjectType;
                }
                else
                {
                    this.Groups.RemoveAt(i);
                }
            }
        }

        public override void Clear()
        {
            base.Clear();
            this.Description = "";
            this.ContactName = "";
            this.ContactInfo = "";
            this.Comment = "";
            this.JobNumber = "";
            this.CreationDate = "";
            this.LastModified = "";
            this.CreationParentFolder = "";
            this.ObjectCounter = 0;
            this.GroupCounter = 0;
            this.DisplayResultsForAllPlans = false;
            this.Plans.Clear();
            this.Groups.Clear();
            this.Workspace.Clear();
            this.Report.Clear();
            this.EstimatingItems.Clear();
            this.EstimatingItems.ClearPrices();
        }

        public bool Create(MainForm parentForm)
        {
            this.Clear();
            return this.ShowProjectForm(parentForm, true);
        }

        public bool EditInfo(MainForm parentForm)
        {
            bool dirty = base.Dirty;
            bool flag = this.ShowProjectForm(parentForm, false);
            base.Dirty = (flag ? true : dirty);
            return flag;
        }

        private bool EstimatingPriceExists(EstimatingItemPrice estimatingItemPrice)
        {
            bool flag;
            DrawObjectGroup drawObjectGroup = this.Groups.FindFromGroupID(estimatingItemPrice.GroupID);
            if (drawObjectGroup == null)
            {
                return false;
            }
            if (this.drawArea.FindObjectFromGroupID(this, estimatingItemPrice.GroupID) == null)
            {
                return false;
            }
            if (estimatingItemPrice.ExtensionID == "")
            {
                return true;
            }
            try
            {
                string[] fields = Utilities.GetFields(estimatingItemPrice.Key.ToString(), ';');
                string str = fields[1];
                string str1 = fields[2];
                if (!Utilities.IsNumber(str1))
                {
                    foreach (Preset collection in drawObjectGroup.Presets.Collection)
                    {
                        if (collection.ID != str)
                        {
                            continue;
                        }
                        foreach (PresetResult presetResult in collection.Results.Collection)
                        {
                            if (!(presetResult.Name == str1) || !presetResult.ConditionMet)
                            {
                                continue;
                            }
                            flag = true;
                            return flag;
                        }
                    }
                }
                else
                {
                    foreach (CEstimatingItem cEstimatingItem in drawObjectGroup.EstimatingItems.Collection)
                    {
                        if (!(cEstimatingItem.InternalKey == str) || !(cEstimatingItem.ItemID == str1))
                        {
                            continue;
                        }
                        flag = true;
                        return flag;
                    }
                }
                flag = false;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public void FlagDeletedGroups()
        {
            for (int i = this.Groups.Count - 1; i >= 0; i--)
            {
                DrawObjectGroup item = this.Groups[i];
                DrawObject drawObject = this.drawArea.FindObjectFromGroupID(this, item.ID);
                item.Deleted = drawObject == null;
            }
        }

        public void Initialize(DrawingArea drawArea, ExtensionsSupport extensionsSupport, DBManagement dbManagement)
        {
            this.drawArea = drawArea;
            this.ExtensionsSupport = extensionsSupport;
            this.DBManagement = dbManagement;
        }

        public Plan InsertPlan(string name, string fileName, bool pinned, int brightness, int contrast)
        {
            if (this.Plans.FindPlan(name) != null)
            {
                name = this.Plans.FindFreePlanName(name);
            }
            Plan plan = new Plan(name, fileName, pinned, brightness, contrast);
            plan.GetImageDimension();
            this.Plans.Add(plan);
            return plan;
        }

        public bool Open(string fileName)
        {
            bool flag;
            this.Clear();
            base.FullFileName = fileName;
            try
            {
                using (XmlTextReader xmlTextReader = new XmlTextReader(Utilities.GetShortFileName(fileName)))
                {
                    this.ReadFromStream(xmlTextReader);
                    xmlTextReader.Close();
                    this.CleanUpGroups();
                    this.SetDefaultValues();
                    this.SetObjectsZorder();
                    flag = true;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.Clear();
                Utilities.DisplayFileOpenError(fileName, exception);
                flag = false;
            }
            return flag;
        }

        private void ReadFromStream(XmlTextReader reader)
        {
            int num;
            int num1;
            HatchStylePickerCombo.HatchStylePickerEnum integerAttribute;
            int integerAttribute1 = 150;
            int boolAttribute = 0;
            string stringAttribute = "";
            string upper = "";
            Utilities.NumberDecimalSeparator();
            Project.ParserContext parserContext = Project.ParserContext.UndefinedContext;
            DrawObject drawObject = null;
            DrawObject drawObject1 = null;
            DrawPolyLine drawPolyLine = null;
            DrawPolyLine drawPolyLine1 = null;
            DrawObjectGroup drawObjectGroup = null;
            Preset preset = null;
            Plan point = null;
        Label2:
            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;
                switch (nodeType)
                {
                    case XmlNodeType.Element:
                        {
                            DrawObject lightGray = null;
                            upper = reader.Name.ToUpper();
                            string str = upper;
                            string str1 = str;
                            if (str != null)
                            {
                                switch (str1)
                                {
                                    case "PROJECT":
                                        {
                                            parserContext = Project.ParserContext.ProjectContext;
                                            base.Name = Utilities.GetStringAttribute(reader, "Name", "");
                                            break;
                                        }
                                    case "WORKSPACE":
                                        {
                                            parserContext = Project.ParserContext.WorkpaceContext;
                                            break;
                                        }
                                    case "ACTIVEPLAN":
                                        {
                                            stringAttribute = Utilities.GetStringAttribute(reader, "Name", "");
                                            break;
                                        }
                                    case "PLANS":
                                        {
                                            parserContext = Project.ParserContext.PlansContext;
                                            break;
                                        }
                                    case "GROUP":
                                        {
                                            drawObjectGroup = new DrawObjectGroup(Utilities.GetIntegerAttribute(reader, "GroupID", -1), Utilities.GetStringAttribute(reader, "TemplateID", ""));
                                            this.Groups.Add(drawObjectGroup);
                                            break;
                                        }
                                    case "PLAN":
                                        {
                                            switch (parserContext)
                                            {
                                                case Project.ParserContext.WorkpaceContext:
                                                    {
                                                        this.Workspace.RecentPlans.Add(new Variable(Utilities.GetStringAttribute(reader, "Name", ""), null));
                                                        break;
                                                    }
                                                case Project.ParserContext.PlansContext:
                                                    {
                                                        string stringAttribute1 = Utilities.GetStringAttribute(reader, "FileName", "");
                                                        if (!Path.IsPathRooted(stringAttribute1))
                                                        {
                                                            stringAttribute1 = (!Utilities.FileExists(Path.Combine(base.FolderName, stringAttribute1)) ? Path.Combine(Utilities.GetProjectPlansFolder(base.FolderName), stringAttribute1) : Path.Combine(base.FolderName, stringAttribute1));
                                                        }
                                                        point = this.InsertPlan(reader.GetAttribute("Name"), stringAttribute1, Utilities.GetBoolAttribute(reader, "Pinned", false), Utilities.GetIntegerAttribute(reader, "Brightness", 0), Utilities.GetIntegerAttribute(reader, "Contrast", 0));
                                                        this.drawArea.ActivePlan = point;
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case "THUMBNAIL":
                                        {
                                            if (point == null)
                                            {
                                                break;
                                            }
                                            point.Thumbnail.FileName = Utilities.GetStringAttribute(reader, "FileName", "");
                                            break;
                                        }
                                    case "SCALE":
                                        {
                                            if (point == null)
                                            {
                                                break;
                                            }
                                            float doubleAttribute = (float)Utilities.GetDoubleAttribute(reader, "Value", 0);
                                            UnitScale.UnitSystem unitSystem = UnitScale.CastUnitSystem(Utilities.GetIntegerAttribute(reader, "Type", 2));
                                            UnitScale.UnitPrecision unitPrecision = UnitScale.CastUnitPrecision(Utilities.GetIntegerAttribute(reader, "Precision", 0));
                                            bool flag = Utilities.GetBoolAttribute(reader, "SetManually", false);
                                            point.UnitScale.SetScale(doubleAttribute, unitSystem, unitPrecision, flag);
                                            bool boolAttribute1 = Utilities.GetBoolAttribute(reader, "Engineering", false);
                                            point.UnitScale.Engineering = (!boolAttribute1 ? false : unitSystem == UnitScale.UnitSystem.imperial);
                                            break;
                                        }
                                    case "BOOKMARK":
                                        {
                                            if (point == null || !(Utilities.GetStringAttribute(reader, "Name", "") == "Default"))
                                            {
                                                break;
                                            }
                                            point.DefaultBookmark.LayerIndex = Utilities.GetIntegerAttribute(reader, "LayerIndex", 0);
                                            point.DefaultBookmark.Zoom = Utilities.GetIntegerAttribute(reader, "Zoom", 100);
                                            point.DefaultBookmark.Origin = new Point(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0));
                                            break;
                                        }
                                    case "LAYER":
                                        {
                                            if (point == null)
                                            {
                                                break;
                                            }
                                            boolAttribute = point.Layers.CreateNewLayer(Utilities.GetStringAttribute(reader, "Name", ""), 150);
                                            integerAttribute1 = Utilities.GetIntegerAttribute(reader, "Opacity", 150);
                                            integerAttribute1 = (integerAttribute1 < 25 || integerAttribute1 > 225 ? 150 : integerAttribute1);
                                            point.Layers[boolAttribute].Opacity = integerAttribute1;
                                            point.Layers[boolAttribute].Visible = Utilities.GetBoolAttribute(reader, "Visible", true);
                                            break;
                                        }
                                    case "LEGEND":
                                        {
                                            DrawLegend drawLegend = new DrawLegend(point, this.ExtensionsSupport, Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetStringAttribute(reader, "Name", ""), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), Utilities.GetIntegerAttribute(reader, "PenWidth", 6), Utilities.GetIntegerAttribute(reader, "FontSize", DrawLegend.DefaultFontSize), Utilities.GetIntegerAttribute(reader, "MaxRows", DrawLegend.DefaultMaxRows))
                                            {
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            lightGray = drawLegend;
                                            drawObject1 = lightGray;
                                            break;
                                        }
                                    case "RECTANGLE":
                                        {
                                            DrawRectangle drawRectangle = new DrawRectangle(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetIntegerAttribute(reader, "Width", 10), Utilities.GetIntegerAttribute(reader, "Height", 10), new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), integerAttribute1, true, Utilities.GetIntegerAttribute(reader, "PenWidth", 3))
                                            {
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            lightGray = drawRectangle;
                                            drawObject1 = lightGray;
                                            break;
                                        }
                                    case "ANGLE":
                                        {
                                            DrawAngle drawAngle = new DrawAngle(Utilities.GetIntegerAttribute(reader, "X1", 0), Utilities.GetIntegerAttribute(reader, "Y1", 0), Utilities.GetIntegerAttribute(reader, "X2", 0), Utilities.GetIntegerAttribute(reader, "Y2", 0), Utilities.GetIntegerAttribute(reader, "X3", 0), Utilities.GetIntegerAttribute(reader, "Y3", 0), new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), integerAttribute1, Utilities.GetIntegerAttribute(reader, "PenWidth", 3), (DrawAngle.AngleTypeEnum)Utilities.GetIntegerAttribute(reader, "AngleType", 0))
                                            {
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            lightGray = drawAngle;
                                            drawObject1 = lightGray;
                                            break;
                                        }
                                    case "NOTE":
                                        {
                                            DrawNote drawNote = new DrawNote(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetIntegerAttribute(reader, "Width", 10), Utilities.GetIntegerAttribute(reader, "Height", 10), Utilities.GetIntegerAttribute(reader, "AnchorX", 0), Utilities.GetIntegerAttribute(reader, "AnchorY", 0), Utilities.GetStringAttribute(reader, "Name", ""), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), integerAttribute1, true, Utilities.GetIntegerAttribute(reader, "PenWidth", 3), Utilities.GetIntegerAttribute(reader, "FontSize", 32))
                                            {
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            lightGray = drawNote;
                                            drawObject1 = lightGray;
                                            break;
                                        }
                                    case "LINE":
                                        {
                                            DrawLine drawLine = new DrawLine(0, 0, 0, 0, new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), "", true, ColorTranslator.FromHtml(reader.GetAttribute("Color")), integerAttribute1, Utilities.GetIntegerAttribute(reader, "PenWidth", 4))
                                            {
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            drawObject = drawLine;
                                            drawObject.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 0), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
                                            break;
                                        }
                                    case "AREA":
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
                                            DrawPolyLine drawPolyLine2 = new DrawPolyLine(new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), hatchStylePickerEnum, integerAttribute1, Utilities.GetIntegerAttribute(reader, "PenWidth", 4))
                                            {
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            drawObject = drawPolyLine2;
                                            drawObject.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), SlopeFactor.HipValleyEnum.hipValleyUnavailable);
                                            break;
                                        }
                                    case "PERIMETER":
                                        {
                                            DrawPolyLine drawPolyLine3 = new DrawPolyLine(new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), integerAttribute1, Utilities.GetIntegerAttribute(reader, "PenWidth", 4))
                                            {
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            drawObject = drawPolyLine3;
                                            drawObject.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
                                            break;
                                        }
                                    case "POINT":
                                        {
                                            if (drawPolyLine == null)
                                            {
                                                break;
                                            }
                                            drawPolyLine.AddPoint(new Point(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0)));
                                            break;
                                        }
                                    case "DROP":
                                        {
                                            if (drawPolyLine == null)
                                            {
                                                break;
                                            }
                                            drawPolyLine.CreateDrop(new Point(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0)), Utilities.GetDoubleAttribute(reader, "Height", 0));
                                            break;
                                        }
                                    case "CUSTOMRENDERING":
                                        {
                                            if (drawPolyLine == null)
                                            {
                                                break;
                                            }
                                            string stringAttribute2 = Utilities.GetStringAttribute(reader, "ExtentionID", "");
                                            int integerAttribute2 = Utilities.GetIntegerAttribute(reader, "Angle", 0);
                                            int num2 = Utilities.GetIntegerAttribute(reader, "OffsetX", 0);
                                            int integerAttribute3 = Utilities.GetIntegerAttribute(reader, "OffsetY", 0);
                                            drawPolyLine.SetCustomRenderingProperties(stringAttribute2, new CustomRenderingProperties(integerAttribute2, num2, integerAttribute3));
                                            break;
                                        }
                                    case "COUNTER":
                                        {
                                            DrawCounter drawCounter = new DrawCounter(0, 0, 0, 0, Utilities.GetIntegerAttribute(reader, "DefaultSize", 80), (DrawCounter.CounterShapeTypeEnum)Utilities.GetIntegerAttribute(reader, "Shape", 0), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), reader.GetAttribute("Text"), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), integerAttribute1, true, Utilities.GetIntegerAttribute(reader, "PenWidth", 2))
                                            {
                                                ImageFileName = Utilities.GetStringAttribute(reader, "FileName", ""),
                                                ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                                Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                            };
                                            drawObject = drawCounter;
                                            break;
                                        }
                                    case "EXTENSION":
                                        {
                                            if (drawObjectGroup == null)
                                            {
                                                break;
                                            }
                                            string str2 = Utilities.GetStringAttribute(reader, "Id", "");
                                            str2 = (str2 == "" ? Guid.NewGuid().ToString() : str2);
                                            string stringAttribute3 = Utilities.GetStringAttribute(reader, "Category", "");
                                            string str3 = Utilities.GetStringAttribute(reader, "Name", "");
                                            string caption = Utilities.GetStringAttribute(reader, "DisplayName", "");
                                            if (caption == "")
                                            {
                                                ExtensionCategory extensionCategory = null;
                                                Extension extension = this.ExtensionsSupport.FindExtension(ref extensionCategory, str3);
                                                if (extension != null)
                                                {
                                                    caption = extension.Caption;
                                                }
                                            }
                                            if (caption == "")
                                            {
                                                caption = str3;
                                            }
                                            caption = drawObjectGroup.Presets.GetFreeDisplayName(caption, "");
                                            preset = new Preset(str2, caption, stringAttribute3, str3, (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2));
                                            preset.SetCustomRendering();
                                            drawObjectGroup.Presets.Add(preset);
                                            break;
                                        }
                                    case "ESTIMATINGITEM":
                                        {
                                            if (parserContext != Project.ParserContext.EstimatingItems)
                                            {
                                                if (drawObjectGroup == null)
                                                {
                                                    break;
                                                }
                                                CEstimatingItem cEstimatingItem = new CEstimatingItem()
                                                {
                                                    ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                                };
                                                if (cEstimatingItem.ItemID == "")
                                                {
                                                    break;
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
                                                cEstimatingItem.InternalKey = Utilities.GetStringAttribute(reader, "InternalKey", "");
                                                cEstimatingItem.Tag = cEstimatingItem;
                                                drawObjectGroup.EstimatingItems.Add(cEstimatingItem);
                                                break;
                                            }
                                            else
                                            {
                                                this.SetEstimatingItemPrice(reader);
                                                break;
                                            }
                                        }
                                    case "COFFICEPRODUCT":
                                        {
                                            if (drawObjectGroup == null)
                                            {
                                                break;
                                            }
                                            CEstimatingItem cEstimatingItem1 = new CEstimatingItem()
                                            {
                                                ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                            };
                                            if (cEstimatingItem1.ItemID == "")
                                            {
                                                break;
                                            }
                                            cEstimatingItem1.Description = Utilities.GetStringAttribute(reader, "Description", "");
                                            cEstimatingItem1.Value = Utilities.GetDoubleAttribute(reader, "Cost", 0);
                                            cEstimatingItem1.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                            cEstimatingItem1.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
                                            cEstimatingItem1.Tag = cEstimatingItem1;
                                            drawObjectGroup.COfficeProducts.Add(cEstimatingItem1);
                                            break;
                                        }
                                    case "CHOICE":
                                        {
                                            if (preset == null)
                                            {
                                                break;
                                            }
                                            preset.Choices.Add(new PresetChoice(Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetStringAttribute(reader, "Element", "")));
                                            break;
                                        }
                                    case "FIELD":
                                        {
                                            if (preset == null)
                                            {
                                                break;
                                            }
                                            preset.Fields.Add(new PresetField(Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetStringAttribute(reader, "Value", "")));
                                            break;
                                        }
                                    case "ELEMENT":
                                        {
                                            if (drawObject == null)
                                            {
                                                break;
                                            }
                                            string objectType = drawObject.ObjectType;
                                            string str4 = objectType;
                                            if (objectType == null)
                                            {
                                                break;
                                            }
                                            if (str4 == "Line")
                                            {
                                                DrawLine drawLine1 = new DrawLine(Utilities.GetIntegerAttribute(reader, "X1", 0), Utilities.GetIntegerAttribute(reader, "Y1", 0), Utilities.GetIntegerAttribute(reader, "X2", 0), Utilities.GetIntegerAttribute(reader, "Y2", 0), new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, true, drawObject.Color, integerAttribute1, drawObject.PenWidth)
                                                {
                                                    Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                                    ShowMeasure = drawObject.ShowMeasure,
                                                    Visible = drawObject.Visible
                                                };
                                                lightGray = drawLine1;
                                                lightGray.SetSlopeFactor(drawObject.SlopeFactor);
                                                break;
                                            }
                                            else if (str4 == "Area")
                                            {
                                                DrawPolyLine drawPolyLine4 = new DrawPolyLine(new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, drawObject.Color, drawObject.FillColor, ((DrawPolyLine)drawObject).Pattern, integerAttribute1, drawObject.PenWidth)
                                                {
                                                    Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                                    ShowMeasure = drawObject.ShowMeasure
                                                };
                                                lightGray = drawPolyLine4;
                                                if (drawPolyLine1 != null)
                                                {
                                                    lightGray.Color = Color.LightGray;
                                                    lightGray.FillColor = Color.LightGray;
                                                    lightGray.Visible = false;
                                                }
                                                else
                                                {
                                                    lightGray.SetSlopeFactor(drawObject.SlopeFactor);
                                                    lightGray.Visible = drawObject.Visible;
                                                }
                                                drawPolyLine = (DrawPolyLine)lightGray;
                                                break;
                                            }
                                            else if (str4 != "Perimeter")
                                            {
                                                if (str4 == "Counter")
                                                {
                                                    DrawCounter drawCounter1 = new DrawCounter(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetIntegerAttribute(reader, "Width", 10), Utilities.GetIntegerAttribute(reader, "Height", 10), ((DrawCounter)drawObject).DefaultSize, ((DrawCounter)drawObject).Shape, drawObject.Name, drawObject.GroupID, drawObject.Text, drawObject.Comment, drawObject.Color, drawObject.FillColor, integerAttribute1, true, drawObject.PenWidth)
                                                    {
                                                        ImageFileName = ((DrawCounter)drawObject).ImageFileName,
                                                        Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                                        ShowMeasure = drawObject.ShowMeasure,
                                                        Visible = drawObject.Visible
                                                    };
                                                    lightGray = drawCounter1;
                                                    if (((DrawCounter)lightGray).Shape != DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage)
                                                    {
                                                        break;
                                                    }
                                                    ((DrawCounter)lightGray).LoadCustomImage();
                                                    break;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            else if (drawPolyLine1 != null)
                                            {
                                                DrawLine drawLine2 = new DrawLine(Utilities.GetIntegerAttribute(reader, "X1", 0), Utilities.GetIntegerAttribute(reader, "Y1", 0), Utilities.GetIntegerAttribute(reader, "X2", 0), Utilities.GetIntegerAttribute(reader, "Y2", 0), new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, true, Color.Black, integerAttribute1, drawObject.PenWidth)
                                                {
                                                    Height = Utilities.GetDoubleAttribute(reader, "Height", 0),
                                                    ShowMeasure = drawObject.ShowMeasure,
                                                    Visible = false
                                                };
                                                lightGray = drawLine2;
                                                break;
                                            }
                                            else
                                            {
                                                DrawPolyLine drawPolyLine5 = new DrawPolyLine(new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, drawObject.Color, integerAttribute1, drawObject.PenWidth)
                                                {
                                                    Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                                    CloseFigure = Utilities.GetBoolAttribute(reader, "Closed", true),
                                                    ShowMeasure = drawObject.ShowMeasure,
                                                    Visible = drawObject.Visible
                                                };
                                                lightGray = drawPolyLine5;
                                                lightGray.SetSlopeFactor(drawObject.SlopeFactor);
                                                drawPolyLine = (DrawPolyLine)lightGray;
                                                break;
                                            }
                                        }
                                    case "DEDUCTIONS":
                                        {
                                            drawPolyLine1 = drawPolyLine;
                                            break;
                                        }
                                    case "PRICES":
                                    case "ESTIMATINGITEMS":
                                        {
                                            parserContext = Project.ParserContext.EstimatingItems;
                                            break;
                                        }
                                    case "PRICE":
                                        {
                                            this.SetEstimatingItemPrice(reader);
                                            break;
                                        }
                                    case "REPORTS":
                                        {
                                            parserContext = Project.ParserContext.ReportsContext;
                                            break;
                                        }
                                    case "REPORT":
                                        {
                                            this.Report.Order = (Report.ReportOrderEnum)Utilities.GetIntegerAttribute(reader, "Order", 0);
                                            this.Report.SystemType = (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2);
                                            this.Report.Precision = (UnitScale.UnitPrecision)Utilities.GetIntegerAttribute(reader, "Precision", 0);
                                            break;
                                        }
                                    case "PROPERTY":
                                        {
                                            string stringAttribute4 = Utilities.GetStringAttribute(reader, "Name", "");
                                            if (parserContext != Project.ParserContext.ReportsContext)
                                            {
                                                break;
                                            }
                                            string str5 = stringAttribute4;
                                            string str6 = str5;
                                            if (str5 == null)
                                            {
                                                break;
                                            }
                                            if (u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x600117du002d2 == null)
                                            {
                                                u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x600117du002d2 = new Dictionary<string, int>(16)
                                        {
                                            { "ShowProjectInfo", 0 },
                                            { "ShowComments", 1 },
                                            { "ShowInvisibleObjects", 2 },
                                            { "ApplyFilter", 3 },
                                            { "EstimatingShowProjectInfo", 4 },
                                            { "EstimatingShowComments", 5 },
                                            { "EstimatingShowInvisibleObjects", 6 },
                                            { "EstimatingApplyFilter", 7 },
                                            { "QuoteShowProjectInfo", 8 },
                                            { "QuoteShowComments", 9 },
                                            { "QuoteShowInvisibleObjects", 10 },
                                            { "QuoteApplyFilter", 11 },
                                            { "OrderByObjectsFilter", 12 },
                                            { "OrderByPlansFilter", 13 },
                                            { "ReportSortBy", 14 },
                                            { "QuoteReportSortBy", 15 }
                                        };
                                            }
                                            if (!u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x600117du002d2.TryGetValue(str6, out num))
                                            {
                                                break;
                                            }
                                            switch (num)
                                            {
                                                case 0:
                                                    {
                                                        this.Report.TakeoffShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 1:
                                                    {
                                                        this.Report.TakeoffShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        this.Report.TakeoffShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        this.Report.TakeoffApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        this.Report.EstimatingShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 5:
                                                    {
                                                        this.Report.EstimatingShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 6:
                                                    {
                                                        this.Report.EstimatingShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 7:
                                                    {
                                                        this.Report.EstimatingApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 8:
                                                    {
                                                        this.Report.QuoteShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 9:
                                                    {
                                                        this.Report.QuoteShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 10:
                                                    {
                                                        this.Report.QuoteShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 11:
                                                    {
                                                        this.Report.QuoteApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
                                                        break;
                                                    }
                                                case 12:
                                                    {
                                                        this.Report.OrderByObjectsFilter = Utilities.GetStringAttribute(reader, "Value", "");
                                                        break;
                                                    }
                                                case 13:
                                                    {
                                                        this.Report.OrderByPlansFilter = Utilities.GetStringAttribute(reader, "Value", "");
                                                        break;
                                                    }
                                                case 14:
                                                    {
                                                        this.Report.ReportSortBy = (Report.ReportSortByEnum)Utilities.GetIntegerAttribute(reader, "Value", 0);
                                                        break;
                                                    }
                                                case 15:
                                                    {
                                                        this.Report.QuoteReportSortBy = (Report.QuoteReportSortByEnum)Utilities.GetIntegerAttribute(reader, "Value", 0);
                                                        if (this.Report.QuoteReportSortBy == Report.QuoteReportSortByEnum.QuoteReportSortBySections || this.Report.QuoteReportSortBy == Report.QuoteReportSortByEnum.QuoteReportSortByTypes || this.Report.QuoteReportSortBy == Report.QuoteReportSortByEnum.QuoteReportSortByList)
                                                        {
                                                            goto Label1;
                                                        }
                                                        this.Report.QuoteReportSortBy = Report.QuoteReportSortByEnum.QuoteReportSortBySections;
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                }
                            }
                        Label1:
                            if (lightGray == null)
                            {
                                continue;
                            }
                            this.drawArea.InsertObject(lightGray, boolAttribute, false, false);
                            if (drawPolyLine1 == null)
                            {
                                continue;
                            }
                            drawPolyLine1.CreateDeduction(lightGray);
                            continue;
                        }
                    case XmlNodeType.Attribute:
                        {
                            continue;
                        }
                    case XmlNodeType.Text:
                        {
                            string str7 = upper;
                            string str8 = str7;
                            if (str7 == null)
                            {
                                continue;
                            }
                            if (u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x600117du002d3 == null)
                            {
                                u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x600117du002d3 = new Dictionary<string, int>(8)
                            {
                                { "COMMENT", 0 },
                                { "DESCRIPTION", 1 },
                                { "CONTACTNAME", 2 },
                                { "CONTACTINFO", 3 },
                                { "JOBNUMBER", 4 },
                                { "CREATIONDATE", 5 },
                                { "LASTMODIFIED", 6 },
                                { "DISPLAYRESULTSFORALLPLANS", 7 }
                            };
                            }
                            if (!u003cPrivateImplementationDetailsu003eu007bE0438CCFu002d7425u002d4F3Bu002dBA1Bu002d66DC2567D6E9u007d.u0024u0024method0x600117du002d3.TryGetValue(str8, out num1))
                            {
                                continue;
                            }
                            switch (num1)
                            {
                                case 0:
                                    {
                                        string str9 = reader.Value.Trim().Replace("`", "\r\n");
                                        if (drawObject != null || drawObject1 != null)
                                        {
                                            if (drawObject == null)
                                            {
                                                drawObject1.Comment = str9;
                                            }
                                            else
                                            {
                                                drawObject.Comment = str9;
                                            }
                                            drawObject1 = null;
                                            continue;
                                        }
                                        else
                                        {
                                            switch (parserContext)
                                            {
                                                case Project.ParserContext.ProjectContext:
                                                    {
                                                        this.Comment = str9;
                                                        continue;
                                                    }
                                                case Project.ParserContext.PlansContext:
                                                    {
                                                        point.Comment = str9;
                                                        continue;
                                                    }
                                                default:
                                                    {
                                                        continue;
                                                    }
                                            }
                                        }
                                        break;
                                    }
                                case 1:
                                    {
                                        this.Description = reader.Value.Trim().Replace("`", "\r\n");
                                        continue;
                                    }
                                case 2:
                                    {
                                        this.ContactName = reader.Value.Trim();
                                        continue;
                                    }
                                case 3:
                                    {
                                        this.ContactInfo = reader.Value.Trim().Replace("`", "\r\n");
                                        continue;
                                    }
                                case 4:
                                    {
                                        this.JobNumber = reader.Value.Trim();
                                        continue;
                                    }
                                case 5:
                                    {
                                        this.CreationDate = reader.Value.Trim();
                                        continue;
                                    }
                                case 6:
                                    {
                                        this.LastModified = reader.Value.Trim();
                                        continue;
                                    }
                                case 7:
                                    {
                                        this.DisplayResultsForAllPlans = Utilities.ConvertToBoolean(reader.Value, false);
                                        continue;
                                    }
                                default:
                                    {
                                        continue;
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            if (nodeType == XmlNodeType.EndElement)
                            {
                                break;
                            }
                            else
                            {
                                goto Label2;
                            }
                        }
                }
                string upper1 = reader.Name.ToUpper();
                if (upper1 == "DEDUCTIONS")
                {
                    drawPolyLine1 = null;
                }
                if (upper1 == "EXTENSION")
                {
                    preset = null;
                }
                if (upper1 != "COMMENT" && upper1 != "POINT" && upper1 != "CUSTOMRENDERING" && upper1 != "PROPERTY")
                {
                    drawPolyLine = null;
                }
                if (upper1 != "COMMENT" && upper1 != "ELEMENT" && upper1 != "POINT" && upper1 != "DEDUCTIONS" && upper1 != "EXTENSION" && upper1 != "CHOICE" && upper1 != "FIELD" && upper1 != "CUSTOMRENDERING" && upper1 != "PROPERTY")
                {
                    drawObject = null;
                }
                if (upper1 != "GROUP")
                {
                    continue;
                }
                drawObjectGroup = null;
            }
            this.Workspace.ActivePlan = this.Plans.FindPlan(stringAttribute);
        }

        public void RemovePlan(Plan plan)
        {
            plan.DeleteThumbnail();
            this.Plans.Remove(plan);
            this.FlagDeletedGroups();
        }

        public void ReorderPlans()
        {
            this.Plans.Collection.Sort(new Project.PlanSorter());
        }

        public bool Save(string fileName, bool saveBackup = false)
        {
            bool flag;
            try
            {
                if (!saveBackup)
                {
                    this.saveFileName = fileName;
                }
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        this.SaveToStream(streamWriter);
                        streamWriter.Close();
                    }
                    fileStream.Close();
                    if (!saveBackup)
                    {
                        base.FullFileName = fileName;
                        base.Dirty = false;
                    }
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
            string[] str;
            object obj;
            object[] scaleSystemType;
            Size size;
            sw.WriteLine("<?xml version=\"1.0\"?>");
            sw.WriteLine("<QuoterPlanSession>");
            sw.WriteLine(string.Concat("\t<Project Name=\"", Utilities.EscapeString(base.Name), "\">"));
            sw.WriteLine(string.Concat("\t\t<Description>", Utilities.EscapeString(this.Description.Replace("\n", "`").Replace("\r", "")), "</Description>"));
            sw.WriteLine(string.Concat("\t\t<ContactName>", Utilities.EscapeString(this.ContactName), "</ContactName>"));
            sw.WriteLine(string.Concat("\t\t<ContactInfo>", Utilities.EscapeString(this.ContactInfo.Replace("\n", "`").Replace("\r", "")), "</ContactInfo>"));
            sw.WriteLine(string.Concat("\t\t<JobNumber>", Utilities.EscapeString(this.JobNumber), "</JobNumber>"));
            sw.WriteLine(string.Concat("\t\t<Comment>", Utilities.EscapeString(this.Comment.Replace("\n", "`").Replace("\r", "")), "</Comment>"));
            sw.WriteLine(string.Concat("\t\t<CreationDate>", this.CreationDate, "</CreationDate>"));
            sw.WriteLine(string.Concat("\t\t<LastModified>", this.LastModified, "</LastModified>"));
            sw.WriteLine(string.Concat("\t\t<DisplayResultsForAllPlans>", this.DisplayResultsForAllPlans, "</DisplayResultsForAllPlans>"));
            sw.WriteLine("\t</Project>");
            sw.WriteLine("\t<Workspace>");
            if (this.Workspace.ActivePlan != null)
            {
                sw.WriteLine(string.Concat("\t\t<ActivePlan Name=\"", Utilities.EscapeString(this.Workspace.ActivePlan.Name), "\"/>"));
            }
            if (this.Workspace.RecentPlans.Count > 0)
            {
                sw.WriteLine("\t\t<RecentPlans>");
                foreach (Variable collection in this.Workspace.RecentPlans.Collection)
                {
                    Plan plan = this.Plans.FindPlan(collection.Name);
                    if (plan == null)
                    {
                        continue;
                    }
                    sw.WriteLine(string.Concat("\t\t\t<Plan Name=\"", Utilities.EscapeString(plan.Name), "\"/>"));
                }
                sw.WriteLine("\t\t</RecentPlans>");
            }
            sw.WriteLine("\t</Workspace>");
            sw.WriteLine("\t<Plans>");
            foreach (DrawObjectGroup drawObjectGroup in this.Groups.Collection)
            {
                if (drawObjectGroup.Presets.Count <= 0 && !(drawObjectGroup.TemplateID != "") && drawObjectGroup.COfficeProducts.Collection.Count <= 0 && drawObjectGroup.EstimatingItems.Count <= 0)
                {
                    continue;
                }
                StreamWriter streamWriter = sw;
                str = new string[] { "\t\t<Group GroupID=\"", null, null, null, null };
                str[1] = drawObjectGroup.ID.ToString();
                str[2] = "\"";
                str[3] = (drawObjectGroup.TemplateID != "" ? string.Concat(" TemplateID=\"", drawObjectGroup.TemplateID, "\"") : "");
                str[4] = ">";
                streamWriter.WriteLine(string.Concat(str));
                foreach (Preset preset in drawObjectGroup.Presets.Collection)
                {
                    string str1 = "\t\t\t<Extension ";
                    str1 = string.Concat(str1, "Id=\"", preset.ID, "\" ");
                    str1 = string.Concat(str1, "DisplayName=\"", Utilities.EscapeString(preset.DisplayName), "\" ");
                    str1 = string.Concat(str1, "Name=\"", Utilities.EscapeString(preset.ExtensionName), "\" ");
                    str1 = string.Concat(str1, "Category=\"", Utilities.EscapeString(preset.CategoryName), "\" ");
                    obj = str1;
                    scaleSystemType = new object[] { obj, "ScaleType=\"", (int)preset.ScaleSystemType, "\"" };
                    str1 = string.Concat(scaleSystemType);
                    sw.WriteLine(string.Concat(str1, ">"));
                    foreach (PresetChoice presetChoice in preset.Choices.Collection)
                    {
                        string[] strArrays = new string[] { "\t\t\t\t<Choice Name=\"", Utilities.EscapeString(presetChoice.ChoiceName), "\" Element=\"", Utilities.EscapeString(presetChoice.ChoiceElementName), "\"/>" };
                        sw.WriteLine(string.Concat(strArrays));
                    }
                    foreach (PresetField presetField in preset.Fields.Collection)
                    {
                        object[] objArray = new object[] { "\t\t\t\t<Field Name=\"", Utilities.EscapeString(presetField.Name), "\" Value=\"", presetField.Value, "\"/>" };
                        sw.WriteLine(string.Concat(objArray));
                    }
                    sw.WriteLine("\t\t\t</Extension>");
                }
                foreach (CEstimatingItem cEstimatingItem in drawObjectGroup.EstimatingItems.Collection)
                {
                    string str2 = "\t\t\t<EstimatingItem ";
                    str2 = string.Concat(str2, "ItemID=\"", cEstimatingItem.ItemID, "\" ");
                    str2 = string.Concat(str2, "Description=\"", Utilities.EscapeString(cEstimatingItem.Description), "\" ");
                    str2 = string.Concat(str2, "Unit=\"", Utilities.EscapeString(cEstimatingItem.Unit), "\" ");
                    obj = str2;
                    scaleSystemType = new object[] { obj, "ItemType=\"", (int)cEstimatingItem.ItemType, "\" " };
                    obj = string.Concat(scaleSystemType);
                    scaleSystemType = new object[] { obj, "UnitMeasure=\"", (int)cEstimatingItem.UnitMeasure, "\" " };
                    obj = string.Concat(scaleSystemType);
                    scaleSystemType = new object[] { obj, "CoverageValue=\"", cEstimatingItem.CoverageValue, "\" " };
                    obj = string.Concat(scaleSystemType);
                    scaleSystemType = new object[] { obj, "CoverageUnit=\"", cEstimatingItem.CoverageUnit, "\" " };
                    obj = string.Concat(scaleSystemType);
                    scaleSystemType = new object[] { obj, "SectionID=\"", cEstimatingItem.SectionID, "\" " };
                    obj = string.Concat(scaleSystemType);
                    scaleSystemType = new object[] { obj, "SubSectionID=\"", cEstimatingItem.SubSectionID, "\" " };
                    str2 = string.Concat(scaleSystemType);
                    str2 = string.Concat(str2, "BidCode=\"", Utilities.EscapeString(cEstimatingItem.BidCode), "\" ");
                    str2 = string.Concat(str2, "Formula=\"", Utilities.EscapeString(cEstimatingItem.Formula), "\" ");
                    str2 = string.Concat(str2, "InternalKey=\"", Utilities.EscapeString(cEstimatingItem.InternalKey), "\"");
                    sw.WriteLine(string.Concat(str2, "/>"));
                }
                foreach (CEstimatingItem collection1 in drawObjectGroup.COfficeProducts.Collection)
                {
                    string str3 = "\t\t\t<COfficeProduct ";
                    str3 = string.Concat(str3, "ItemID=\"", collection1.ItemID, "\" ");
                    str3 = string.Concat(str3, "Description=\"", Utilities.EscapeString(collection1.Description), "\" ");
                    obj = str3;
                    scaleSystemType = new object[] { obj, "Cost=\"", collection1.Value, "\" " };
                    str3 = string.Concat(scaleSystemType);
                    str3 = string.Concat(str3, "Unit=\"", Utilities.EscapeString(collection1.Unit), "\" ");
                    str3 = string.Concat(str3, "Formula=\"", Utilities.EscapeString(collection1.Formula), "\"");
                    sw.WriteLine(string.Concat(str3, "/>"));
                }
                sw.WriteLine("\t\t</Group>");
            }
            foreach (Plan plan1 in this.Plans.Collection)
            {
                string fileName = Utilities.GetFileName(plan1.FullFileName, false);
                if (Utilities.GetParentDirectory(plan1.FullFileName) != base.FolderName)
                {
                    string parentDirectory = Utilities.GetParentDirectory(plan1.FolderName);
                    string str4 = Path.Combine(Utilities.GetParentDirectory(Path.GetDirectoryName(this.saveFileName)), Utilities.GetShortPlansFolder());
                    fileName = (parentDirectory.ToLower() == str4.ToLower() ? Utilities.MakeRelativePath(string.Concat(str4, "\\"), plan1.FullFileName) : plan1.FullFileName);
                }
                StreamWriter streamWriter1 = sw;
                str = new string[] { "\t\t<Plan Name=\"", Utilities.EscapeString(plan1.Name), "\" FileName=\"", Utilities.EscapeString(fileName), "\"", null, null, null, null };
                str[5] = (!plan1.Pinned ? "" : string.Concat(" Pinned=\"", plan1.Pinned, "\""));
                str[6] = (plan1.Brightness == 0 ? "" : string.Concat(" Brightness=\"", plan1.Brightness, "\""));
                str[7] = (plan1.Contrast == 0 ? "" : string.Concat(" Contrast=\"", plan1.Contrast, "\""));
                str[8] = ">";
                streamWriter1.WriteLine(string.Concat(str));
                plan1.Dirty = false;
                if (plan1.Thumbnail.FileName != "")
                {
                    sw.WriteLine(string.Concat("\t\t\t<Thumbnail FileName=\"", Utilities.EscapeString(plan1.Thumbnail.FileName), "\"/>"));
                    plan1.Thumbnail.Dirty = false;
                }
                scaleSystemType = new object[] { "\t\t\t<Scale Value=\"", plan1.UnitScale.Scale, "\" Type=\"", (int)plan1.UnitScale.ScaleSystemType, "\" Precision=\"", (int)plan1.UnitScale.Precision, "\" SetManually=\"", plan1.UnitScale.SetManually, "\" Engineering=\"", plan1.UnitScale.Engineering, "\"/>" };
                sw.WriteLine(string.Concat(scaleSystemType));
                sw.WriteLine("\t\t\t<Bookmarks>");
                scaleSystemType = new object[] { "\t\t\t\t<Bookmark Name=\"", Utilities.EscapeString(plan1.DefaultBookmark.Name), "\" LayerIndex=\"", plan1.DefaultBookmark.LayerIndex, "\" Zoom=\"", plan1.DefaultBookmark.Zoom, "\" X=\"", null, null, null, null };
                Point origin = plan1.DefaultBookmark.Origin;
                scaleSystemType[7] = origin.X;
                scaleSystemType[8] = "\" Y=\"";
                origin = plan1.DefaultBookmark.Origin;
                scaleSystemType[9] = origin.Y;
                scaleSystemType[10] = "\"/>";
                sw.WriteLine(string.Concat(scaleSystemType));
                sw.WriteLine("\t\t\t</Bookmarks>");
                sw.WriteLine(string.Concat("\t\t\t<Comment>", Utilities.EscapeString(plan1.Comment.Replace("\n", "`").Replace("\r", "")), "</Comment>"));
                sw.WriteLine("\t\t\t<Layers>");
                for (int i = 0; i < plan1.Layers.Count; i++)
                {
                    Layer item = plan1.Layers[i];
                    scaleSystemType = new object[] { "\t\t\t\t<Layer Index=\"", i, "\" Name=\"", Utilities.EscapeString(item.Name), "\" Opacity=\"", item.Opacity, "\" Visible=\"", item.Visible, "\" Active=\"", item.Active, "\">" };
                    sw.WriteLine(string.Concat(scaleSystemType));
                    ArrayList arrayLists = new ArrayList();
                    for (int j = item.DrawingObjects.Count - 1; j >= 0; j--)
                    {
                        bool deductionParentID = false;
                        string empty = string.Empty;
                        DrawObject drawObject = item.DrawingObjects[j];
                        string objectType = drawObject.ObjectType;
                        string str5 = string.Concat("Name=\"", Utilities.EscapeString(drawObject.Name), "\" ");
                        if (drawObject.IsPartOfGroup())
                        {
                            deductionParentID = drawObject.DeductionParentID != -1;
                            if (!deductionParentID)
                            {
                                deductionParentID = arrayLists.Contains(string.Concat(objectType, drawObject.GroupID));
                            }
                        }
                        if (!deductionParentID)
                        {
                            if (objectType == "Rectangle")
                            {
                                DrawRectangle drawRectangle = (DrawRectangle)drawObject;
                                obj = str5;
                                scaleSystemType = new object[] { obj, "X=\"", null, null };
                                origin = drawRectangle.Location;
                                scaleSystemType[2] = origin.X;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Y=\"", null, null };
                                origin = drawRectangle.Location;
                                scaleSystemType[2] = origin.Y;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Width=\"", null, null };
                                size = drawRectangle.Size;
                                scaleSystemType[2] = size.Width;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Height=\"", null, null };
                                size = drawRectangle.Size;
                                scaleSystemType[2] = size.Height;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                            }
                            if (objectType == "Angle")
                            {
                                DrawAngle drawAngle = (DrawAngle)drawObject;
                                if (drawAngle.PointArray.Count >= 3)
                                {
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "X1=\"", null, null };
                                    origin = (Point)drawAngle.PointArray[0];
                                    scaleSystemType[2] = origin.X;
                                    scaleSystemType[3] = "\" ";
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "Y1=\"", null, null };
                                    origin = (Point)drawAngle.PointArray[0];
                                    scaleSystemType[2] = origin.Y;
                                    scaleSystemType[3] = "\" ";
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "X2=\"", null, null };
                                    origin = (Point)drawAngle.PointArray[1];
                                    scaleSystemType[2] = origin.X;
                                    scaleSystemType[3] = "\" ";
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "Y2=\"", null, null };
                                    origin = (Point)drawAngle.PointArray[1];
                                    scaleSystemType[2] = origin.Y;
                                    scaleSystemType[3] = "\" ";
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "X3=\"", null, null };
                                    origin = (Point)drawAngle.PointArray[2];
                                    scaleSystemType[2] = origin.X;
                                    scaleSystemType[3] = "\" ";
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "Y3=\"", null, null };
                                    origin = (Point)drawAngle.PointArray[2];
                                    scaleSystemType[2] = origin.Y;
                                    scaleSystemType[3] = "\" ";
                                    str5 = string.Concat(scaleSystemType);
                                }
                                obj = str5;
                                scaleSystemType = new object[] { obj, "AngleType=\"", (int)drawAngle.AngleType, "\" " };
                                str5 = string.Concat(scaleSystemType);
                            }
                            if (objectType == "Note")
                            {
                                DrawNote drawNote = (DrawNote)drawObject;
                                obj = str5;
                                scaleSystemType = new object[] { obj, "X=\"", null, null };
                                origin = drawNote.Location;
                                scaleSystemType[2] = origin.X;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Y=\"", null, null };
                                origin = drawNote.Location;
                                scaleSystemType[2] = origin.Y;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Width=\"", null, null };
                                size = drawNote.Size;
                                scaleSystemType[2] = size.Width;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Height=\"", null, null };
                                size = drawNote.Size;
                                scaleSystemType[2] = size.Height;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "AnchorX=\"", null, null };
                                origin = drawNote.StartPoint;
                                scaleSystemType[2] = origin.X;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "AnchorY=\"", null, null };
                                origin = drawNote.StartPoint;
                                scaleSystemType[2] = origin.Y;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "FontSize=\"", drawNote.FontSize, "\" " };
                                str5 = string.Concat(scaleSystemType);
                            }
                            if (objectType == "Legend")
                            {
                                DrawLegend drawLegend = (DrawLegend)drawObject;
                                obj = str5;
                                scaleSystemType = new object[] { obj, "X=\"", null, null };
                                origin = drawLegend.Location;
                                scaleSystemType[2] = origin.X;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Y=\"", null, null };
                                origin = drawLegend.Location;
                                scaleSystemType[2] = origin.Y;
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "FontSize=\"", drawLegend.FontSize, "\" " };
                                str5 = string.Concat(scaleSystemType);
                                obj = str5;
                                scaleSystemType = new object[] { obj, "MaxRows=\"", drawLegend.MaxRows, "\" " };
                                str5 = string.Concat(scaleSystemType);
                            }
                            if (drawObject.IsPartOfGroup())
                            {
                                obj = str5;
                                scaleSystemType = new object[] { obj, "GroupID=\"", drawObject.GroupID, "\" " };
                                str5 = string.Concat(scaleSystemType);
                                arrayLists.Add(string.Concat(objectType, drawObject.GroupID));
                            }
                            if (objectType == "Counter")
                            {
                                DrawCounter drawCounter = (DrawCounter)drawObject;
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Shape=\"", (int)drawCounter.Shape, "\" " };
                                str5 = string.Concat(scaleSystemType);
                                str5 = string.Concat(str5, (drawCounter.ImageFileName == string.Empty ? string.Empty : string.Concat("FileName=\"", drawCounter.ImageFileName, "\" ")));
                                obj = str5;
                                scaleSystemType = new object[] { obj, "DefaultSize=\"", drawCounter.DefaultSize, "\" " };
                                str5 = string.Concat(scaleSystemType);
                                str5 = string.Concat(str5, "Text=\"", Utilities.EscapeString(drawCounter.Text), "\" ");
                            }
                            obj = str5;
                            scaleSystemType = new object[] { obj, "Color=\"", null, null };
                            Color color = drawObject.Color;
                            scaleSystemType[2] = color.ToArgb();
                            scaleSystemType[3] = "\" ";
                            str5 = string.Concat(scaleSystemType);
                            obj = str5;
                            scaleSystemType = new object[] { obj, "PenWidth=\"", drawObject.PenWidth, "\" " };
                            str5 = string.Concat(scaleSystemType);
                            obj = str5;
                            scaleSystemType = new object[] { obj, "PenType=\"", drawObject.PenType, "\" " };
                            str5 = string.Concat(scaleSystemType);
                            if (objectType == "Rectangle" || objectType == "Counter" || objectType == "Note" || objectType == "Legend" || objectType == "Area")
                            {
                                obj = str5;
                                scaleSystemType = new object[] { obj, "FillColor=\"", null, null };
                                color = drawObject.FillColor;
                                scaleSystemType[2] = color.ToArgb();
                                scaleSystemType[3] = "\" ";
                                str5 = string.Concat(scaleSystemType);
                            }
                            if (objectType == "Area")
                            {
                                obj = str5;
                                scaleSystemType = new object[] { obj, "Pattern=\"", (int)((DrawPolyLine)drawObject).Pattern, "\" " };
                                str5 = string.Concat(scaleSystemType);
                            }
                            if (objectType == "Area" || objectType == "Perimeter" || objectType == "Line")
                            {
                                DrawLine drawLine = (DrawLine)drawObject;
                                if (drawLine.SlopeFactor.InternalValue > 0)
                                {
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "Slope=\"", drawLine.SlopeFactor.InternalValue, "\" " };
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "SlopeType=\"", (int)drawLine.SlopeFactor.SlopeType, "\" " };
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "SlopeApply=\"", (int)drawLine.SlopeFactor.SlopeApplyType, "\" " };
                                    str5 = string.Concat(scaleSystemType);
                                    obj = str5;
                                    scaleSystemType = new object[] { obj, "HipValley=\"", (int)drawLine.SlopeFactor.HipValley, "\" " };
                                    str5 = string.Concat(scaleSystemType);
                                }
                            }
                            obj = str5;
                            scaleSystemType = new object[] { obj, "ShowMeasure=\"", drawObject.ShowMeasure, "\" " };
                            str5 = string.Concat(scaleSystemType);
                            obj = str5;
                            scaleSystemType = new object[] { obj, "Visible=\"", drawObject.Visible, "\"" };
                            str5 = string.Concat(scaleSystemType);
                            empty = string.Concat("\t\t\t\t\t<", objectType, " ", str5);
                            empty = (drawObject.GroupID > -1 || drawObject.Comment != string.Empty ? string.Concat(empty, ">") : string.Concat(empty, "/>"));
                            sw.WriteLine(empty);
                            if (drawObject.Comment != string.Empty)
                            {
                                sw.WriteLine(string.Concat("\t\t\t\t\t\t<Comment>", Utilities.EscapeString(drawObject.Comment.Replace("\n", "`").Replace("\r", "")), "</Comment>"));
                            }
                            if (drawObject.GroupID > -1)
                            {
                                for (int k = item.DrawingObjects.Count - 1; k >= 0; k--)
                                {
                                    DrawObject item1 = item.DrawingObjects[k];
                                    if (item1.GroupID == drawObject.GroupID)
                                    {
                                        string objectType1 = item1.ObjectType;
                                        string str6 = objectType1;
                                        if (objectType1 != null)
                                        {
                                            if (str6 == "Line")
                                            {
                                                DrawLine drawLine1 = (DrawLine)item1;
                                                if (drawLine1.DeductionParentID == -1)
                                                {
                                                    obj = string.Concat("\t\t\t\t\t\t<Element ", (drawLine1.Label == string.Empty ? string.Empty : string.Concat("Label=\"", Utilities.EscapeString(drawLine1.Label), "\" ")));
                                                    scaleSystemType = new object[] { obj, "X1=\"", null, null };
                                                    origin = drawLine1.StartPoint;
                                                    scaleSystemType[2] = origin.X;
                                                    scaleSystemType[3] = "\" ";
                                                    obj = string.Concat(scaleSystemType);
                                                    scaleSystemType = new object[] { obj, "Y1=\"", null, null };
                                                    origin = drawLine1.StartPoint;
                                                    scaleSystemType[2] = origin.Y;
                                                    scaleSystemType[3] = "\" ";
                                                    obj = string.Concat(scaleSystemType);
                                                    scaleSystemType = new object[] { obj, "X2=\"", null, null };
                                                    origin = drawLine1.EndPoint;
                                                    scaleSystemType[2] = origin.X;
                                                    scaleSystemType[3] = "\" ";
                                                    obj = string.Concat(scaleSystemType);
                                                    scaleSystemType = new object[] { obj, "Y2=\"", null, null };
                                                    origin = drawLine1.EndPoint;
                                                    scaleSystemType[2] = origin.Y;
                                                    scaleSystemType[3] = "\" ";
                                                    string str7 = string.Concat(scaleSystemType);
                                                    sw.WriteLine(string.Concat(str7, "/>"));
                                                }
                                            }
                                            else if (str6 == "Counter")
                                            {
                                                DrawCounter drawCounter1 = (DrawCounter)item1;
                                                obj = string.Concat("\t\t\t\t\t\t<Element ", (drawCounter1.Label == string.Empty ? string.Empty : string.Concat("Label=\"", Utilities.EscapeString(drawCounter1.Label), "\" ")));
                                                scaleSystemType = new object[] { obj, "X=\"", null, null };
                                                origin = drawCounter1.Location;
                                                scaleSystemType[2] = origin.X;
                                                scaleSystemType[3] = "\" ";
                                                obj = string.Concat(scaleSystemType);
                                                scaleSystemType = new object[] { obj, "Y=\"", null, null };
                                                origin = drawCounter1.Location;
                                                scaleSystemType[2] = origin.Y;
                                                scaleSystemType[3] = "\" ";
                                                obj = string.Concat(scaleSystemType);
                                                scaleSystemType = new object[] { obj, "Width=\"", null, null };
                                                size = drawCounter1.Size;
                                                scaleSystemType[2] = size.Width;
                                                scaleSystemType[3] = "\" ";
                                                obj = string.Concat(scaleSystemType);
                                                scaleSystemType = new object[] { obj, "Height=\"", null, null };
                                                size = drawCounter1.Size;
                                                scaleSystemType[2] = size.Height;
                                                scaleSystemType[3] = "\"";
                                                string str8 = string.Concat(scaleSystemType);
                                                sw.WriteLine(string.Concat(str8, "/>"));
                                            }
                                            else if (str6 == "Area" || str6 == "Perimeter")
                                            {
                                                DrawPolyLine drawPolyLine = (DrawPolyLine)item1;
                                                if (drawPolyLine.DeductionParentID == -1)
                                                {
                                                    if (objectType != "Area")
                                                    {
                                                        StreamWriter streamWriter2 = sw;
                                                        scaleSystemType = new object[] { "\t\t\t\t\t\t<Element", null, null, null, null };
                                                        scaleSystemType[1] = (drawPolyLine.Label == string.Empty ? string.Empty : string.Concat(" Label=\"", Utilities.EscapeString(drawPolyLine.Label), "\""));
                                                        scaleSystemType[2] = " Closed=\"";
                                                        scaleSystemType[3] = drawPolyLine.CloseFigure;
                                                        scaleSystemType[4] = "\">";
                                                        streamWriter2.WriteLine(string.Concat(scaleSystemType));
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine(string.Concat("\t\t\t\t\t\t<Element", (drawPolyLine.Label == string.Empty ? string.Empty : string.Concat(" Label=\"", Utilities.EscapeString(drawPolyLine.Label), "\"")), ">"));
                                                    }
                                                    foreach (Point pointArray in drawPolyLine.PointArray)
                                                    {
                                                        scaleSystemType = new object[] { "\t\t\t\t\t\t\t<Point X=\"", pointArray.X, "\" Y=\"", pointArray.Y, "\"/>" };
                                                        sw.WriteLine(string.Concat(scaleSystemType));
                                                    }
                                                    foreach (DrawLine dropArray in drawPolyLine.DropArray)
                                                    {
                                                        scaleSystemType = new object[] { "\t\t\t\t\t\t\t<Drop X=\"", null, null, null, null, null, null };
                                                        origin = dropArray.StartPoint;
                                                        scaleSystemType[1] = origin.X;
                                                        scaleSystemType[2] = "\" Y=\"";
                                                        origin = dropArray.StartPoint;
                                                        scaleSystemType[3] = origin.Y;
                                                        scaleSystemType[4] = "\" Height=\"";
                                                        scaleSystemType[5] = dropArray.Height;
                                                        scaleSystemType[6] = "\"/>";
                                                        sw.WriteLine(string.Concat(scaleSystemType));
                                                    }
                                                    if (drawPolyLine.CustomRenderingArray.Count > 0)
                                                    {
                                                        foreach (KeyValuePair<string, CustomRenderingProperties> customRenderingArray in drawPolyLine.CustomRenderingArray)
                                                        {
                                                            CustomRenderingProperties value = customRenderingArray.Value;
                                                            string str9 = "\t\t\t\t\t\t\t<CustomRendering ";
                                                            str9 = string.Concat(str9, "ExtentionID=\"", customRenderingArray.Key, "\" ");
                                                            obj = str9;
                                                            scaleSystemType = new object[] { obj, "Angle=\"", value.Angle, "\" " };
                                                            obj = string.Concat(scaleSystemType);
                                                            scaleSystemType = new object[] { obj, "OffsetX=\"", value.OffsetX, "\" " };
                                                            obj = string.Concat(scaleSystemType);
                                                            scaleSystemType = new object[] { obj, "OffsetY=\"", value.OffsetY, "\"" };
                                                            str9 = string.Concat(scaleSystemType);
                                                            sw.WriteLine(string.Concat(str9, "/>"));
                                                        }
                                                    }
                                                    if (drawPolyLine.DeductionArray.Count > 0)
                                                    {
                                                        sw.WriteLine("\t\t\t\t\t\t\t<Deductions>");
                                                        if (objectType != "Area")
                                                        {
                                                            foreach (DrawLine deductionArray in drawPolyLine.DeductionArray)
                                                            {
                                                                obj = string.Concat("\t\t\t\t\t\t\t\t\t<Element ", (deductionArray.Height == 0 ? string.Empty : string.Concat("Height=\"", deductionArray.Height, "\" ")));
                                                                scaleSystemType = new object[] { obj, "X1=\"", null, null };
                                                                origin = deductionArray.StartPoint;
                                                                scaleSystemType[2] = origin.X;
                                                                scaleSystemType[3] = "\" ";
                                                                obj = string.Concat(scaleSystemType);
                                                                scaleSystemType = new object[] { obj, "Y1=\"", null, null };
                                                                origin = deductionArray.StartPoint;
                                                                scaleSystemType[2] = origin.Y;
                                                                scaleSystemType[3] = "\" ";
                                                                obj = string.Concat(scaleSystemType);
                                                                scaleSystemType = new object[] { obj, "X2=\"", null, null };
                                                                origin = deductionArray.EndPoint;
                                                                scaleSystemType[2] = origin.X;
                                                                scaleSystemType[3] = "\" ";
                                                                obj = string.Concat(scaleSystemType);
                                                                scaleSystemType = new object[] { obj, "Y2=\"", null, null };
                                                                origin = deductionArray.EndPoint;
                                                                scaleSystemType[2] = origin.Y;
                                                                scaleSystemType[3] = "\" ";
                                                                string str10 = string.Concat(scaleSystemType);
                                                                sw.WriteLine(string.Concat(str10, "/>"));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            foreach (DrawPolyLine deductionArray1 in drawPolyLine.DeductionArray)
                                                            {
                                                                sw.WriteLine("\t\t\t\t\t\t\t\t<Element>");
                                                                foreach (Point point in deductionArray1.PointArray)
                                                                {
                                                                    scaleSystemType = new object[] { "\t\t\t\t\t\t\t\t\t<Point X=\"", point.X, "\" Y=\"", point.Y, "\"/>" };
                                                                    sw.WriteLine(string.Concat(scaleSystemType));
                                                                }
                                                                sw.WriteLine("\t\t\t\t\t\t\t\t</Element>");
                                                            }
                                                        }
                                                        sw.WriteLine("\t\t\t\t\t\t\t</Deductions>");
                                                    }
                                                    sw.WriteLine("\t\t\t\t\t\t</Element>");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (drawObject.IsPartOfGroup() || drawObject.Comment != string.Empty)
                            {
                                sw.WriteLine(string.Concat("\t\t\t\t\t</", objectType, ">"));
                            }
                        }
                    }
                    sw.WriteLine("\t\t\t\t</Layer>");
                }
                sw.WriteLine("\t\t\t</Layers>");
                sw.WriteLine("\t\t</Plan>");
            }
            sw.WriteLine("\t</Plans>");
            sw.WriteLine("\t<Prices>");
            foreach (DictionaryEntry estimatingPrice in this.EstimatingItems.EstimatingPrices)
            {
                EstimatingItemPrice estimatingItemPrice = (EstimatingItemPrice)estimatingPrice.Value;
                if (!this.EstimatingPriceExists(estimatingItemPrice))
                {
                    continue;
                }
                scaleSystemType = new object[] { "\t\t<Price Key=\"", Utilities.EscapeString(estimatingItemPrice.Key.ToString()), "\" CostEach=\"", estimatingItemPrice.CostEach, "\" MarkupEach=\"", estimatingItemPrice.MarkupEach, "\" SystemType=\"", (int)estimatingItemPrice.SystemType, "\"/>" };
                sw.WriteLine(string.Concat(scaleSystemType));
            }
            sw.WriteLine("\t</Prices>");
            sw.WriteLine("\t<Reports>");
            scaleSystemType = new object[] { "\t\t<Report Name=\"Default\" Order=\"", Utilities.ConvertToInt(this.Report.Order), "\" ScaleType=\"", Utilities.ConvertToInt(this.Report.SystemType), "\" Precision=\"", Utilities.ConvertToInt(this.Report.Precision), "\">" };
            sw.WriteLine(string.Concat(scaleSystemType));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"ShowProjectInfo\" Value=\"", this.Report.TakeoffShowProjectInfo, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"ShowComments\" Value=\"", this.Report.TakeoffShowComments, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"ShowInvisibleObjects\" Value=\"", this.Report.TakeoffShowInvisibleObjects, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"ApplyFilter\" Value=\"", this.Report.TakeoffApplyFilter, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"EstimatingShowProjectInfo\" Value=\"", this.Report.EstimatingShowProjectInfo, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"EstimatingShowComments\" Value=\"", this.Report.EstimatingShowComments, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"EstimatingShowInvisibleObjects\" Value=\"", this.Report.EstimatingShowInvisibleObjects, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"EstimatingApplyFilter\" Value=\"", this.Report.EstimatingApplyFilter, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"QuoteShowProjectInfo\" Value=\"", this.Report.QuoteShowProjectInfo, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"QuoteShowComments\" Value=\"", this.Report.QuoteShowComments, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"QuoteShowInvisibleObjects\" Value=\"", this.Report.QuoteShowInvisibleObjects, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"QuoteApplyFilter\" Value=\"", this.Report.QuoteApplyFilter, "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"OrderByObjectsFilter\" Value=\"", Utilities.EscapeString(this.Report.OrderByObjectsFilter), "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"OrderByPlansFilter\" Value=\"", Utilities.EscapeString(this.Report.OrderByPlansFilter), "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"ReportSortBy\" Value=\"", Utilities.ConvertToInt(this.Report.ReportSortBy), "\"/>"));
            sw.WriteLine(string.Concat("\t\t\t<Property Name=\"QuoteReportSortBy\" Value=\"", Utilities.ConvertToInt(this.Report.QuoteReportSortBy), "\"/>"));
            sw.WriteLine("\t\t</Report>");
            sw.WriteLine("\t</Reports>");
            sw.WriteLine("</QuoterPlanSession>");
        }

        private void SetDefaultValues()
        {
            base.Name = (base.Name == "" ? base.FileName.Substring(0, base.FileName.Length - 4) : base.Name);
            this.CreationDate = (this.CreationDate == "" ? Utilities.FormatDate(DateTime.Now) : this.CreationDate);
            this.LastModified = (this.LastModified == "" ? Utilities.FormatDate(DateTime.Now) : this.LastModified);
        }

        private void SetEstimatingItemPrice(XmlTextReader reader)
        {
            string stringAttribute = Utilities.GetStringAttribute(reader, "Key", "");
            EstimatingItemPrice estimatingItemPrice = new EstimatingItemPrice(stringAttribute, Utilities.GetDoubleAttribute(reader, "CostEach", 0), Utilities.GetDoubleAttribute(reader, "MarkupEach", 0), (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "SystemType", 0));
            this.EstimatingItems.EstimatingPrices.Add(stringAttribute, estimatingItemPrice);
        }

        private void SetObjectsZorder()
        {
            this.ObjectCounter = 0;
            foreach (Plan collection in this.Plans.Collection)
            {
                foreach (Layer layer in collection.Layers.Collection)
                {
                    layer.DrawingObjects.Collection.Reverse();
                }
                foreach (Layer collection1 in collection.Layers.Collection)
                {
                    for (int i = collection1.DrawingObjects.Count - 1; i >= 0; i--)
                    {
                        DrawObject item = collection1.DrawingObjects[i];
                        Project project = this;
                        int objectCounter = project.ObjectCounter + 1;
                        int num = objectCounter;
                        project.ObjectCounter = objectCounter;
                        item.ID = num;
                        if (item.ObjectType == "Area" || item.ObjectType == "Perimeter")
                        {
                            foreach (DrawLine deductionArray in ((DrawPolyLine)item).DeductionArray)
                            {
                                deductionArray.DeductionParentID = item.ID;
                            }
                        }
                    }
                }
            }
            this.GroupCounter = GroupUtilities.GetFreeGroupID(this) - 1;
        }

        private bool ShowProjectForm(MainForm parentForm, bool creationMode)
        {
            base.Dirty = false;
            try
            {
                using (ProjectForm projectForm = new ProjectForm(this, creationMode))
                {
                    projectForm.HelpUtilities = parentForm.HelpUtilities;
                    projectForm.HelpContextString = "ProjectForm";
                    projectForm.ShowDialog(parentForm);
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
            return base.Dirty;
        }

        private enum ParserContext
        {
            UndefinedContext,
            ProjectContext,
            WorkpaceContext,
            PlansContext,
            EstimatingItems,
            ReportsContext
        }

        private class PlanSorter : IComparer
        {
            public PlanSorter()
            {
            }

            public int Compare(object x, object y)
            {
                int num;
                try
                {
                    Plan plan = x as Plan;
                    num = plan.ReorderIndex.CompareTo((y as Plan).ReorderIndex);
                }
                catch (Exception exception)
                {
                    Utilities.DisplaySystemError(exception);
                    num = -1;
                }
                return num;
            }
        }
    }
}