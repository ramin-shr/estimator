using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace QuoterPlan
{
    public class Project : BaseFileInfo
    {
        private DrawingArea drawArea;
        private string saveFileName = string.Empty;

        public string Comment { get; set; }
        public string ContactInfo { get; set; }
        public string ContactName { get; set; }
        public string CreationDate { get; set; }
        public string CreationParentFolder { get; set; }

        public DBManagement DBManagement { get; private set; }
        public string Description { get; set; }

        public string DisplayName
        {
            get
            {
                if (base.FileName != "")
                    return Path.GetFileNameWithoutExtension(base.FileName);

                return string.Concat(
                    base.Name,
                    " (",
                    Utilities.GetDateString(this.CreationDate, Utilities.GetCurrentValidUICultureShort())
                        .Replace(",", "")
                        .Replace(" ", "-"),
                    ")"
                );
            }
        }

        public bool DisplayResultsForAllPlans { get; set; }

        public EstimatingItems EstimatingItems { get; private set; }
        public ExtensionsSupport ExtensionsSupport { get; private set; }

        public int GroupCounter { get; set; }
        public DrawObjectGroups Groups { get; private set; }

        public string JobNumber { get; set; }
        public string LastModified { get; set; }

        public int ObjectCounter { get; set; }

        public Plans Plans { get; private set; }
        public Report Report { get; private set; }
        public Workspace Workspace { get; private set; }

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
                DrawObjectGroup group = this.Groups[i];
                DrawObject groupObject = this.drawArea.FindObjectFromGroupID(this, group.ID);
                if (groupObject != null)
                {
                    group.Name = groupObject.Name;
                    group.ObjectType = groupObject.ObjectType;
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
            bool wasDirty = base.Dirty;
            bool updated = this.ShowProjectForm(parentForm, false);
            base.Dirty = updated ? true : wasDirty;
            return updated;
        }

        private bool EstimatingPriceExists(EstimatingItemPrice price)
        {
            DrawObjectGroup group = this.Groups.FindFromGroupID(price.GroupID);
            if (group == null)
                return false;

            if (this.drawArea.FindObjectFromGroupID(this, price.GroupID) == null)
                return false;

            if (price.ExtensionID == "")
                return true;

            try
            {
                string[] parts = Utilities.GetFields(price.Key.ToString(), ';');
                string internalKey = parts[1];
                string secondPart = parts[2];

                if (!Utilities.IsNumber(secondPart))
                {
                    foreach (Preset preset in group.Presets.Collection)
                    {
                        if (preset.ID != internalKey)
                            continue;

                        foreach (PresetResult result in preset.Results.Collection)
                        {
                            if (result.Name == secondPart && result.ConditionMet)
                                return true;
                        }
                    }
                }
                else
                {
                    foreach (CEstimatingItem item in group.EstimatingItems.Collection)
                    {
                        if (item.InternalKey == internalKey && item.ItemID == secondPart)
                            return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public void FlagDeletedGroups()
        {
            for (int i = this.Groups.Count - 1; i >= 0; i--)
            {
                DrawObjectGroup group = this.Groups[i];
                DrawObject groupObject = this.drawArea.FindObjectFromGroupID(this, group.ID);
                group.Deleted = (groupObject == null);
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
                name = this.Plans.FindFreePlanName(name);

            Plan plan = new Plan(name, fileName, pinned, brightness, contrast);
            plan.GetImageDimension();
            this.Plans.Add(plan);
            return plan;
        }

        public bool Open(string fileName)
        {
            this.Clear();
            base.FullFileName = fileName;

            try
            {
                using (XmlTextReader reader = new XmlTextReader(Utilities.GetShortFileName(fileName)))
                {
                    this.ReadFromStream(reader);
                    reader.Close();
                }

                this.CleanUpGroups();
                this.SetDefaultValues();
                this.SetObjectsZorder();
                return true;
            }
            catch (Exception ex)
            {
                this.Clear();
                Utilities.DisplayFileOpenError(fileName, ex);
                return false;
            }
        }

        private void ReadFromStream(XmlTextReader reader)
        {
            int currentLayerOpacity = 150;
            int currentLayerIndex = 0;

            string activePlanName = "";
            string lastElementNameUpper = "";

            Utilities.NumberDecimalSeparator();

            ParserContext context = ParserContext.UndefinedContext;

            DrawObject currentTemplateObject = null;
            DrawObject pendingCommentObject = null;

            DrawPolyLine currentPolyLine = null;
            DrawPolyLine deductionParentPolyLine = null;

            DrawObjectGroup currentGroup = null;
            Preset currentPreset = null;

            Plan currentPlan = null;

            while (reader.Read())
            {
                XmlNodeType nodeType = reader.NodeType;

                if (nodeType == XmlNodeType.Element)
                {
                    DrawObject createdObjectToInsert = null;

                    lastElementNameUpper = reader.Name.ToUpperInvariant();
                    switch (lastElementNameUpper)
                    {
                        case "PROJECT":
                            context = ParserContext.ProjectContext;
                            base.Name = Utilities.GetStringAttribute(reader, "Name", "");
                            break;

                        case "WORKSPACE":
                            context = ParserContext.WorkpaceContext;
                            break;

                        case "ACTIVEPLAN":
                            activePlanName = Utilities.GetStringAttribute(reader, "Name", "");
                            break;

                        case "PLANS":
                            context = ParserContext.PlansContext;
                            break;

                        case "GROUP":
                            currentGroup = new DrawObjectGroup(
                                Utilities.GetIntegerAttribute(reader, "GroupID", -1),
                                Utilities.GetStringAttribute(reader, "TemplateID", "")
                            );
                            this.Groups.Add(currentGroup);
                            break;

                        case "PLAN":
                            if (context == ParserContext.WorkpaceContext)
                            {
                                this.Workspace.RecentPlans.Add(new Variable(Utilities.GetStringAttribute(reader, "Name", ""), null));
                            }
                            else if (context == ParserContext.PlansContext)
                            {
                                string planFileName = Utilities.GetStringAttribute(reader, "FileName", "");
                                if (!Path.IsPathRooted(planFileName))
                                {
                                    planFileName =
                                        (!Utilities.FileExists(Path.Combine(base.FolderName, planFileName))
                                            ? Path.Combine(Utilities.GetProjectPlansFolder(base.FolderName), planFileName)
                                            : Path.Combine(base.FolderName, planFileName));
                                }

                                currentPlan = this.InsertPlan(
                                    reader.GetAttribute("Name"),
                                    planFileName,
                                    Utilities.GetBoolAttribute(reader, "Pinned", false),
                                    Utilities.GetIntegerAttribute(reader, "Brightness", 0),
                                    Utilities.GetIntegerAttribute(reader, "Contrast", 0)
                                );

                                this.drawArea.ActivePlan = currentPlan;
                            }
                            break;

                        case "THUMBNAIL":
                            if (currentPlan != null)
                                currentPlan.Thumbnail.FileName = Utilities.GetStringAttribute(reader, "FileName", "");
                            break;

                        case "SCALE":
                            if (currentPlan != null)
                            {
                                float scaleValue = (float)Utilities.GetDoubleAttribute(reader, "Value", 0);
                                UnitScale.UnitSystem unitSystem = UnitScale.CastUnitSystem(Utilities.GetIntegerAttribute(reader, "Type", 2));
                                UnitScale.UnitPrecision precision = UnitScale.CastUnitPrecision(Utilities.GetIntegerAttribute(reader, "Precision", 0));
                                bool setManually = Utilities.GetBoolAttribute(reader, "SetManually", false);

                                currentPlan.UnitScale.SetScale(scaleValue, unitSystem, precision, setManually);

                                bool engineering = Utilities.GetBoolAttribute(reader, "Engineering", false);
                                currentPlan.UnitScale.Engineering = engineering && unitSystem == UnitScale.UnitSystem.imperial;
                            }
                            break;

                        case "BOOKMARK":
                            if (currentPlan != null && Utilities.GetStringAttribute(reader, "Name", "") == "Default")
                            {
                                currentPlan.DefaultBookmark.LayerIndex = Utilities.GetIntegerAttribute(reader, "LayerIndex", 0);
                                currentPlan.DefaultBookmark.Zoom = Utilities.GetIntegerAttribute(reader, "Zoom", 100);
                                currentPlan.DefaultBookmark.Origin = new Point(
                                    Utilities.GetIntegerAttribute(reader, "X", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y", 0)
                                );
                            }
                            break;

                        case "LAYER":
                            if (currentPlan != null)
                            {
                                currentLayerIndex = currentPlan.Layers.CreateNewLayer(Utilities.GetStringAttribute(reader, "Name", ""), 150);

                                currentLayerOpacity = Utilities.GetIntegerAttribute(reader, "Opacity", 150);
                                currentLayerOpacity = (currentLayerOpacity < 25 || currentLayerOpacity > 225) ? 150 : currentLayerOpacity;

                                currentPlan.Layers[currentLayerIndex].Opacity = currentLayerOpacity;
                                currentPlan.Layers[currentLayerIndex].Visible = Utilities.GetBoolAttribute(reader, "Visible", true);
                            }
                            break;

                        case "LEGEND":
                            {
                                DrawLegend legend = new DrawLegend(
                                    currentPlan,
                                    this.ExtensionsSupport,
                                    Utilities.GetIntegerAttribute(reader, "X", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y", 0),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 6),
                                    Utilities.GetIntegerAttribute(reader, "FontSize", DrawLegend.DefaultFontSize),
                                    Utilities.GetIntegerAttribute(reader, "MaxRows", DrawLegend.DefaultMaxRows)
                                )
                                {
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                createdObjectToInsert = legend;
                                pendingCommentObject = createdObjectToInsert;
                            }
                            break;

                        case "RECTANGLE":
                            {
                                DrawRectangle rect = new DrawRectangle(
                                    Utilities.GetIntegerAttribute(reader, "X", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y", 0),
                                    Utilities.GetIntegerAttribute(reader, "Width", 10),
                                    Utilities.GetIntegerAttribute(reader, "Height", 10),
                                    new PointF(0f, 0f),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    "",
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                    currentLayerOpacity,
                                    true,
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 3)
                                )
                                {
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                createdObjectToInsert = rect;
                                pendingCommentObject = createdObjectToInsert;
                            }
                            break;

                        case "ANGLE":
                            {
                                DrawAngle angle = new DrawAngle(
                                    Utilities.GetIntegerAttribute(reader, "X1", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y1", 0),
                                    Utilities.GetIntegerAttribute(reader, "X2", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y2", 0),
                                    Utilities.GetIntegerAttribute(reader, "X3", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y3", 0),
                                    new PointF(0f, 0f),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                    currentLayerOpacity,
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 3),
                                    (DrawAngle.AngleTypeEnum)Utilities.GetIntegerAttribute(reader, "AngleType", 0)
                                )
                                {
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                createdObjectToInsert = angle;
                                pendingCommentObject = createdObjectToInsert;
                            }
                            break;

                        case "NOTE":
                            {
                                DrawNote note = new DrawNote(
                                    Utilities.GetIntegerAttribute(reader, "X", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y", 0),
                                    Utilities.GetIntegerAttribute(reader, "Width", 10),
                                    Utilities.GetIntegerAttribute(reader, "Height", 10),
                                    Utilities.GetIntegerAttribute(reader, "AnchorX", 0),
                                    Utilities.GetIntegerAttribute(reader, "AnchorY", 0),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                    currentLayerOpacity,
                                    true,
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 3),
                                    Utilities.GetIntegerAttribute(reader, "FontSize", 32)
                                )
                                {
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                createdObjectToInsert = note;
                                pendingCommentObject = createdObjectToInsert;
                            }
                            break;

                        case "LINE":
                            {
                                DrawLine line = new DrawLine(
                                    0, 0, 0, 0,
                                    new PointF(0f, 0f),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    Utilities.GetIntegerAttribute(reader, "GroupID", -1),
                                    "",
                                    true,
                                    ColorTranslator.FromHtml(reader.GetAttribute("Color")),
                                    currentLayerOpacity,
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 4)
                                )
                                {
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                currentTemplateObject = line;
                                currentTemplateObject.SetSlopeFactor(
                                    Utilities.GetDoubleAttribute(reader, "Slope", 0),
                                    (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0),
                                    (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 0),
                                    (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0)
                                );
                            }
                            break;

                        case "AREA":
                            {
                                HatchStylePickerCombo.HatchStylePickerEnum pattern;
                                if (Utilities.GetStringAttribute(reader, "Pattern", string.Empty) == string.Empty)
                                    pattern = HatchStylePickerCombo.HatchStylePickerEnum.Solid;
                                else
                                    pattern = (HatchStylePickerCombo.HatchStylePickerEnum)Utilities.GetIntegerAttribute(reader, "Pattern", -1);

                                DrawPolyLine area = new DrawPolyLine(
                                    new PointF(0f, 0f),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    Utilities.GetIntegerAttribute(reader, "GroupID", -1),
                                    "",
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                    pattern,
                                    currentLayerOpacity,
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 4)
                                )
                                {
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                currentTemplateObject = area;
                                currentTemplateObject.SetSlopeFactor(
                                    Utilities.GetDoubleAttribute(reader, "Slope", 0),
                                    (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0),
                                    (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1),
                                    SlopeFactor.HipValleyEnum.hipValleyUnavailable
                                );
                            }
                            break;

                        case "PERIMETER":
                            {
                                DrawPolyLine perimeter = new DrawPolyLine(
                                    new PointF(0f, 0f),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    Utilities.GetIntegerAttribute(reader, "GroupID", -1),
                                    "",
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                    currentLayerOpacity,
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 4)
                                )
                                {
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                currentTemplateObject = perimeter;
                                currentTemplateObject.SetSlopeFactor(
                                    Utilities.GetDoubleAttribute(reader, "Slope", 0),
                                    (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0),
                                    (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1),
                                    (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0)
                                );
                            }
                            break;

                        case "POINT":
                            if (currentPolyLine != null)
                            {
                                currentPolyLine.AddPoint(new Point(
                                    Utilities.GetIntegerAttribute(reader, "X", 0),
                                    Utilities.GetIntegerAttribute(reader, "Y", 0)
                                ));
                            }
                            break;

                        case "DROP":
                            if (currentPolyLine != null)
                            {
                                currentPolyLine.CreateDrop(
                                    new Point(
                                        Utilities.GetIntegerAttribute(reader, "X", 0),
                                        Utilities.GetIntegerAttribute(reader, "Y", 0)
                                    ),
                                    Utilities.GetDoubleAttribute(reader, "Height", 0)
                                );
                            }
                            break;

                        case "CUSTOMRENDERING":
                            if (currentPolyLine != null)
                            {
                                string extensionId = Utilities.GetStringAttribute(reader, "ExtentionID", "");
                                int angleDeg = Utilities.GetIntegerAttribute(reader, "Angle", 0);
                                int offsetX = Utilities.GetIntegerAttribute(reader, "OffsetX", 0);
                                int offsetY = Utilities.GetIntegerAttribute(reader, "OffsetY", 0);
                                currentPolyLine.SetCustomRenderingProperties(extensionId, new CustomRenderingProperties(angleDeg, offsetX, offsetY));
                            }
                            break;

                        case "COUNTER":
                            {
                                DrawCounter counter = new DrawCounter(
                                    0, 0, 0, 0,
                                    Utilities.GetIntegerAttribute(reader, "DefaultSize", 80),
                                    (DrawCounter.CounterShapeTypeEnum)Utilities.GetIntegerAttribute(reader, "Shape", 0),
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    Utilities.GetIntegerAttribute(reader, "GroupID", -1),
                                    reader.GetAttribute("Text"),
                                    "",
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
                                    ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
                                    currentLayerOpacity,
                                    true,
                                    Utilities.GetIntegerAttribute(reader, "PenWidth", 2)
                                )
                                {
                                    ImageFileName = Utilities.GetStringAttribute(reader, "FileName", ""),
                                    ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
                                    Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
                                };

                                currentTemplateObject = counter;
                            }
                            break;

                        case "EXTENSION":
                            if (currentGroup != null)
                            {
                                string id = Utilities.GetStringAttribute(reader, "Id", "");
                                if (id == "")
                                    id = Guid.NewGuid().ToString();

                                string category = Utilities.GetStringAttribute(reader, "Category", "");
                                string extensionName = Utilities.GetStringAttribute(reader, "Name", "");
                                string caption = Utilities.GetStringAttribute(reader, "DisplayName", "");

                                if (caption == "")
                                {
                                    ExtensionCategory extensionCategory = null;
                                    Extension extension = this.ExtensionsSupport.FindExtension(ref extensionCategory, extensionName);
                                    if (extension != null)
                                        caption = extension.Caption;
                                }

                                if (caption == "")
                                    caption = extensionName;

                                caption = currentGroup.Presets.GetFreeDisplayName(caption, "");

                                currentPreset = new Preset(
                                    id,
                                    caption,
                                    category,
                                    extensionName,
                                    (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2)
                                );

                                currentPreset.SetCustomRendering();
                                currentGroup.Presets.Add(currentPreset);
                            }
                            break;

                        case "ESTIMATINGITEM":
                            if (context != ParserContext.EstimatingItems)
                            {
                                if (currentGroup != null)
                                {
                                    CEstimatingItem item = new CEstimatingItem
                                    {
                                        ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                    };

                                    if (item.ItemID != "")
                                    {
                                        item.Description = Utilities.GetStringAttribute(reader, "Description", "");
                                        item.Value = -1;
                                        item.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                        item.ItemType = (DBEstimatingItem.EstimatingItemType)Utilities.GetIntegerAttribute(reader, "ItemType", 0);
                                        item.UnitMeasure = (DBEstimatingItem.UnitMeasureType)Utilities.GetIntegerAttribute(reader, "UnitMeasure", 0);
                                        item.CoverageValue = Utilities.GetDoubleAttribute(reader, "CoverageValue", 0);
                                        item.CoverageUnit = (double)Utilities.GetIntegerAttribute(reader, "CoverageUnit", 0);
                                        item.SectionID = Utilities.GetIntegerAttribute(reader, "SectionID", 0);
                                        item.SubSectionID = Utilities.GetIntegerAttribute(reader, "SubSectionID", 0);
                                        item.BidCode = Utilities.GetStringAttribute(reader, "BidCode", "");
                                        item.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
                                        item.InternalKey = Utilities.GetStringAttribute(reader, "InternalKey", "");
                                        item.Tag = item;

                                        currentGroup.EstimatingItems.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                this.SetEstimatingItemPrice(reader);
                            }
                            break;

                        case "COFFICEPRODUCT":
                            if (currentGroup != null)
                            {
                                CEstimatingItem office = new CEstimatingItem
                                {
                                    ItemID = Utilities.GetStringAttribute(reader, "ItemID", "")
                                };

                                if (office.ItemID != "")
                                {
                                    office.Description = Utilities.GetStringAttribute(reader, "Description", "");
                                    office.Value = Utilities.GetDoubleAttribute(reader, "Cost", 0);
                                    office.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
                                    office.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
                                    office.Tag = office;

                                    currentGroup.COfficeProducts.Add(office);
                                }
                            }
                            break;

                        case "CHOICE":
                            if (currentPreset != null)
                            {
                                currentPreset.Choices.Add(new PresetChoice(
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    Utilities.GetStringAttribute(reader, "Element", "")
                                ));
                            }
                            break;

                        case "FIELD":
                            if (currentPreset != null)
                            {
                                currentPreset.Fields.Add(new PresetField(
                                    Utilities.GetStringAttribute(reader, "Name", ""),
                                    Utilities.GetStringAttribute(reader, "Value", "")
                                ));
                            }
                            break;

                        case "ELEMENT":
                            if (currentTemplateObject != null)
                            {
                                string objectType = currentTemplateObject.ObjectType;

                                if (objectType == "Line")
                                {
                                    DrawLine createdLine = new DrawLine(
                                        Utilities.GetIntegerAttribute(reader, "X1", 0),
                                        Utilities.GetIntegerAttribute(reader, "Y1", 0),
                                        Utilities.GetIntegerAttribute(reader, "X2", 0),
                                        Utilities.GetIntegerAttribute(reader, "Y2", 0),
                                        new PointF(0f, 0f),
                                        currentTemplateObject.Name,
                                        currentTemplateObject.GroupID,
                                        currentTemplateObject.Comment,
                                        true,
                                        currentTemplateObject.Color,
                                        currentLayerOpacity,
                                        currentTemplateObject.PenWidth
                                    )
                                    {
                                        Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                        ShowMeasure = currentTemplateObject.ShowMeasure,
                                        Visible = currentTemplateObject.Visible
                                    };

                                    createdObjectToInsert = createdLine;
                                    createdObjectToInsert.SetSlopeFactor(currentTemplateObject.SlopeFactor);
                                }
                                else if (objectType == "Area")
                                {
                                    DrawPolyLine createdArea = new DrawPolyLine(
                                        new PointF(0f, 0f),
                                        currentTemplateObject.Name,
                                        currentTemplateObject.GroupID,
                                        currentTemplateObject.Comment,
                                        currentTemplateObject.Color,
                                        currentTemplateObject.FillColor,
                                        ((DrawPolyLine)currentTemplateObject).Pattern,
                                        currentLayerOpacity,
                                        currentTemplateObject.PenWidth
                                    )
                                    {
                                        Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                        ShowMeasure = currentTemplateObject.ShowMeasure
                                    };

                                    createdObjectToInsert = createdArea;

                                    if (deductionParentPolyLine != null)
                                    {
                                        createdObjectToInsert.Color = Color.LightGray;
                                        createdObjectToInsert.FillColor = Color.LightGray;
                                        createdObjectToInsert.Visible = false;
                                    }
                                    else
                                    {
                                        createdObjectToInsert.SetSlopeFactor(currentTemplateObject.SlopeFactor);
                                        createdObjectToInsert.Visible = currentTemplateObject.Visible;
                                    }

                                    currentPolyLine = (DrawPolyLine)createdObjectToInsert;
                                }
                                else if (objectType == "Perimeter")
                                {
                                    if (deductionParentPolyLine != null)
                                    {
                                        DrawLine deductionLine = new DrawLine(
                                            Utilities.GetIntegerAttribute(reader, "X1", 0),
                                            Utilities.GetIntegerAttribute(reader, "Y1", 0),
                                            Utilities.GetIntegerAttribute(reader, "X2", 0),
                                            Utilities.GetIntegerAttribute(reader, "Y2", 0),
                                            new PointF(0f, 0f),
                                            currentTemplateObject.Name,
                                            currentTemplateObject.GroupID,
                                            currentTemplateObject.Comment,
                                            true,
                                            Color.Black,
                                            currentLayerOpacity,
                                            currentTemplateObject.PenWidth
                                        )
                                        {
                                            Height = Utilities.GetDoubleAttribute(reader, "Height", 0),
                                            ShowMeasure = currentTemplateObject.ShowMeasure,
                                            Visible = false
                                        };

                                        createdObjectToInsert = deductionLine;
                                    }
                                    else
                                    {
                                        DrawPolyLine createdPerimeter = new DrawPolyLine(
                                            new PointF(0f, 0f),
                                            currentTemplateObject.Name,
                                            currentTemplateObject.GroupID,
                                            currentTemplateObject.Comment,
                                            currentTemplateObject.Color,
                                            currentLayerOpacity,
                                            currentTemplateObject.PenWidth
                                        )
                                        {
                                            Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                            CloseFigure = Utilities.GetBoolAttribute(reader, "Closed", true),
                                            ShowMeasure = currentTemplateObject.ShowMeasure,
                                            Visible = currentTemplateObject.Visible
                                        };

                                        createdObjectToInsert = createdPerimeter;
                                        createdObjectToInsert.SetSlopeFactor(currentTemplateObject.SlopeFactor);
                                        currentPolyLine = (DrawPolyLine)createdObjectToInsert;
                                    }
                                }
                                else if (objectType == "Counter")
                                {
                                    DrawCounter createdCounter = new DrawCounter(
                                        Utilities.GetIntegerAttribute(reader, "X", 0),
                                        Utilities.GetIntegerAttribute(reader, "Y", 0),
                                        Utilities.GetIntegerAttribute(reader, "Width", 10),
                                        Utilities.GetIntegerAttribute(reader, "Height", 10),
                                        ((DrawCounter)currentTemplateObject).DefaultSize,
                                        ((DrawCounter)currentTemplateObject).Shape,
                                        currentTemplateObject.Name,
                                        currentTemplateObject.GroupID,
                                        currentTemplateObject.Text,
                                        currentTemplateObject.Comment,
                                        currentTemplateObject.Color,
                                        currentTemplateObject.FillColor,
                                        currentLayerOpacity,
                                        true,
                                        currentTemplateObject.PenWidth
                                    )
                                    {
                                        ImageFileName = ((DrawCounter)currentTemplateObject).ImageFileName,
                                        Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
                                        ShowMeasure = currentTemplateObject.ShowMeasure,
                                        Visible = currentTemplateObject.Visible
                                    };

                                    createdObjectToInsert = createdCounter;

                                    if (((DrawCounter)createdObjectToInsert).Shape == DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage)
                                        ((DrawCounter)createdObjectToInsert).LoadCustomImage();
                                }
                            }
                            break;

                        case "DEDUCTIONS":
                            deductionParentPolyLine = currentPolyLine;
                            break;

                        case "PRICES":
                        case "ESTIMATINGITEMS":
                            context = ParserContext.EstimatingItems;
                            break;

                        case "PRICE":
                            this.SetEstimatingItemPrice(reader);
                            break;

                        case "REPORTS":
                            context = ParserContext.ReportsContext;
                            break;

                        case "REPORT":
                            this.Report.Order = (Report.ReportOrderEnum)Utilities.GetIntegerAttribute(reader, "Order", 0);
                            this.Report.SystemType = (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2);
                            this.Report.Precision = (UnitScale.UnitPrecision)Utilities.GetIntegerAttribute(reader, "Precision", 0);
                            break;

                        case "PROPERTY":
                            if (context == ParserContext.ReportsContext)
                            {
                                string propertyName = Utilities.GetStringAttribute(reader, "Name", "");
                                switch (propertyName)
                                {
                                    case "ShowProjectInfo":
                                        this.Report.TakeoffShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "ShowComments":
                                        this.Report.TakeoffShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "ShowInvisibleObjects":
                                        this.Report.TakeoffShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "ApplyFilter":
                                        this.Report.TakeoffApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;

                                    case "EstimatingShowProjectInfo":
                                        this.Report.EstimatingShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "EstimatingShowComments":
                                        this.Report.EstimatingShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "EstimatingShowInvisibleObjects":
                                        this.Report.EstimatingShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "EstimatingApplyFilter":
                                        this.Report.EstimatingApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;

                                    case "QuoteShowProjectInfo":
                                        this.Report.QuoteShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "QuoteShowComments":
                                        this.Report.QuoteShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "QuoteShowInvisibleObjects":
                                        this.Report.QuoteShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;
                                    case "QuoteApplyFilter":
                                        this.Report.QuoteApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
                                        break;

                                    case "OrderByObjectsFilter":
                                        this.Report.OrderByObjectsFilter = Utilities.GetStringAttribute(reader, "Value", "");
                                        break;
                                    case "OrderByPlansFilter":
                                        this.Report.OrderByPlansFilter = Utilities.GetStringAttribute(reader, "Value", "");
                                        break;

                                    case "ReportSortBy":
                                        this.Report.ReportSortBy = (Report.ReportSortByEnum)Utilities.GetIntegerAttribute(reader, "Value", 0);
                                        break;

                                    case "QuoteReportSortBy":
                                        {
                                            Report.QuoteReportSortByEnum sortBy =
                                                (Report.QuoteReportSortByEnum)Utilities.GetIntegerAttribute(reader, "Value", 0);

                                            if (sortBy == Report.QuoteReportSortByEnum.QuoteReportSortBySections ||
                                                sortBy == Report.QuoteReportSortByEnum.QuoteReportSortByTypes ||
                                                sortBy == Report.QuoteReportSortByEnum.QuoteReportSortByList)
                                            {
                                                this.Report.QuoteReportSortBy = sortBy;
                                            }
                                            else
                                            {
                                                this.Report.QuoteReportSortBy = Report.QuoteReportSortByEnum.QuoteReportSortBySections;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }

                    if (createdObjectToInsert != null)
                    {
                        this.drawArea.InsertObject(createdObjectToInsert, currentLayerIndex, false, false);
                        if (deductionParentPolyLine != null)
                            deductionParentPolyLine.CreateDeduction(createdObjectToInsert);
                    }

                    continue;
                }

                if (nodeType == XmlNodeType.Text)
                {
                    switch (lastElementNameUpper)
                    {
                        case "COMMENT":
                            {
                                string text = reader.Value.Trim().Replace("`", "\r\n");

                                if (currentTemplateObject != null || pendingCommentObject != null)
                                {
                                    if (currentTemplateObject == null)
                                        pendingCommentObject.Comment = text;
                                    else
                                        currentTemplateObject.Comment = text;

                                    pendingCommentObject = null;
                                }
                                else
                                {
                                    if (context == ParserContext.ProjectContext)
                                        this.Comment = text;
                                    else if (context == ParserContext.PlansContext && currentPlan != null)
                                        currentPlan.Comment = text;
                                }
                            }
                            break;

                        case "DESCRIPTION":
                            this.Description = reader.Value.Trim().Replace("`", "\r\n");
                            break;

                        case "CONTACTNAME":
                            this.ContactName = reader.Value.Trim();
                            break;

                        case "CONTACTINFO":
                            this.ContactInfo = reader.Value.Trim().Replace("`", "\r\n");
                            break;

                        case "JOBNUMBER":
                            this.JobNumber = reader.Value.Trim();
                            break;

                        case "CREATIONDATE":
                            this.CreationDate = reader.Value.Trim();
                            break;

                        case "LASTMODIFIED":
                            this.LastModified = reader.Value.Trim();
                            break;

                        case "DISPLAYRESULTSFORALLPLANS":
                            this.DisplayResultsForAllPlans = Utilities.ConvertToBoolean(reader.Value, false);
                            break;
                    }

                    continue;
                }

                if (nodeType == XmlNodeType.EndElement)
                {
                    string endNameUpper = reader.Name.ToUpperInvariant();

                    if (endNameUpper == "DEDUCTIONS")
                        deductionParentPolyLine = null;

                    if (endNameUpper == "EXTENSION")
                        currentPreset = null;

                    if (endNameUpper != "COMMENT" &&
                        endNameUpper != "POINT" &&
                        endNameUpper != "CUSTOMRENDERING" &&
                        endNameUpper != "PROPERTY")
                    {
                        currentPolyLine = null;
                    }

                    if (endNameUpper != "COMMENT" &&
                        endNameUpper != "ELEMENT" &&
                        endNameUpper != "POINT" &&
                        endNameUpper != "DEDUCTIONS" &&
                        endNameUpper != "EXTENSION" &&
                        endNameUpper != "CHOICE" &&
                        endNameUpper != "FIELD" &&
                        endNameUpper != "CUSTOMRENDERING" &&
                        endNameUpper != "PROPERTY")
                    {
                        currentTemplateObject = null;
                    }

                    if (endNameUpper == "GROUP")
                        currentGroup = null;

                    continue;
                }
            }

            this.Workspace.ActivePlan = this.Plans.FindPlan(activePlanName);
        }

        public void RemovePlan(Plan plan)
        {
            plan.DeleteThumbnail();
            this.Plans.Remove(plan);
            this.FlagDeletedGroups();
        }

        public void ReorderPlans()
        {
            this.Plans.Collection.Sort(new PlanSorter());
        }

        public bool Save(string fileName, bool saveBackup = false)
        {
            try
            {
                if (!saveBackup)
                    this.saveFileName = fileName;

                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    this.SaveToStream(sw);
                    sw.Close();
                }

                if (!saveBackup)
                {
                    base.FullFileName = fileName;
                    base.Dirty = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Utilities.DisplayFileSaveError(fileName, ex);
                return false;
            }
        }

        private void SaveToStream(StreamWriter sw)
        {
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
                sw.WriteLine(string.Concat("\t\t<ActivePlan Name=\"", Utilities.EscapeString(this.Workspace.ActivePlan.Name), "\"/>"));

            if (this.Workspace.RecentPlans.Count > 0)
            {
                sw.WriteLine("\t\t<RecentPlans>");
                foreach (Variable recent in this.Workspace.RecentPlans.Collection)
                {
                    Plan plan = this.Plans.FindPlan(recent.Name);
                    if (plan != null)
                        sw.WriteLine(string.Concat("\t\t\t<Plan Name=\"", Utilities.EscapeString(plan.Name), "\"/>"));
                }
                sw.WriteLine("\t\t</RecentPlans>");
            }
            sw.WriteLine("\t</Workspace>");

            sw.WriteLine("\t<Plans>");

            foreach (DrawObjectGroup group in this.Groups.Collection)
            {
                if (group.Presets.Count <= 0 &&
                    !(group.TemplateID != "") &&
                    group.COfficeProducts.Collection.Count <= 0 &&
                    group.EstimatingItems.Count <= 0)
                {
                    continue;
                }

                sw.WriteLine(string.Concat(
                    "\t\t<Group GroupID=\"",
                    group.ID.ToString(),
                    "\"",
                    (group.TemplateID != "" ? string.Concat(" TemplateID=\"", group.TemplateID, "\"") : ""),
                    ">"
                ));

                foreach (Preset preset in group.Presets.Collection)
                {
                    string line = "\t\t\t<Extension ";
                    line = string.Concat(line, "Id=\"", preset.ID, "\" ");
                    line = string.Concat(line, "DisplayName=\"", Utilities.EscapeString(preset.DisplayName), "\" ");
                    line = string.Concat(line, "Name=\"", Utilities.EscapeString(preset.ExtensionName), "\" ");
                    line = string.Concat(line, "Category=\"", Utilities.EscapeString(preset.CategoryName), "\" ");
                    line = string.Concat(line, "ScaleType=\"", (int)preset.ScaleSystemType, "\"");

                    sw.WriteLine(string.Concat(line, ">"));

                    foreach (PresetChoice choice in preset.Choices.Collection)
                    {
                        sw.WriteLine(string.Concat(
                            "\t\t\t\t<Choice Name=\"",
                            Utilities.EscapeString(choice.ChoiceName),
                            "\" Element=\"",
                            Utilities.EscapeString(choice.ChoiceElementName),
                            "\"/>"
                        ));
                    }

                    foreach (PresetField field in preset.Fields.Collection)
                    {
                        sw.WriteLine(string.Concat(
                            "\t\t\t\t<Field Name=\"",
                            Utilities.EscapeString(field.Name),
                            "\" Value=\"",
                            field.Value,
                            "\"/>"
                        ));
                    }

                    sw.WriteLine("\t\t\t</Extension>");
                }

                foreach (CEstimatingItem item in group.EstimatingItems.Collection)
                {
                    string line = "\t\t\t<EstimatingItem ";
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
                    line = string.Concat(line, "Formula=\"", Utilities.EscapeString(item.Formula), "\" ");
                    line = string.Concat(line, "InternalKey=\"", Utilities.EscapeString(item.InternalKey), "\"");

                    sw.WriteLine(string.Concat(line, "/>"));
                }

                foreach (CEstimatingItem office in group.COfficeProducts.Collection)
                {
                    string line = "\t\t\t<COfficeProduct ";
                    line = string.Concat(line, "ItemID=\"", office.ItemID, "\" ");
                    line = string.Concat(line, "Description=\"", Utilities.EscapeString(office.Description), "\" ");
                    line = string.Concat(line, "Cost=\"", office.Value, "\" ");
                    line = string.Concat(line, "Unit=\"", Utilities.EscapeString(office.Unit), "\" ");
                    line = string.Concat(line, "Formula=\"", Utilities.EscapeString(office.Formula), "\"");

                    sw.WriteLine(string.Concat(line, "/>"));
                }

                sw.WriteLine("\t\t</Group>");
            }

            foreach (Plan plan in this.Plans.Collection)
            {
                string planFileName = Utilities.GetFileName(plan.FullFileName, false);

                if (Utilities.GetParentDirectory(plan.FullFileName) != base.FolderName)
                {
                    string planParent = Utilities.GetParentDirectory(plan.FolderName);
                    string plansFolder = Path.Combine(
                        Utilities.GetParentDirectory(Path.GetDirectoryName(this.saveFileName)),
                        Utilities.GetShortPlansFolder()
                    );

                    planFileName = (planParent.ToLower() == plansFolder.ToLower()
                        ? Utilities.MakeRelativePath(string.Concat(plansFolder, "\\"), plan.FullFileName)
                        : plan.FullFileName);
                }

                string planLine =
                    string.Concat("\t\t<Plan Name=\"", Utilities.EscapeString(plan.Name),
                        "\" FileName=\"", Utilities.EscapeString(planFileName), "\"",
                        (!plan.Pinned ? "" : string.Concat(" Pinned=\"", plan.Pinned, "\"")),
                        (plan.Brightness == 0 ? "" : string.Concat(" Brightness=\"", plan.Brightness, "\"")),
                        (plan.Contrast == 0 ? "" : string.Concat(" Contrast=\"", plan.Contrast, "\"")),
                        ">");

                sw.WriteLine(planLine);

                plan.Dirty = false;

                if (plan.Thumbnail.FileName != "")
                {
                    sw.WriteLine(string.Concat("\t\t\t<Thumbnail FileName=\"", Utilities.EscapeString(plan.Thumbnail.FileName), "\"/>"));
                    plan.Thumbnail.Dirty = false;
                }

                sw.WriteLine(string.Concat(
                    "\t\t\t<Scale Value=\"", plan.UnitScale.Scale,
                    "\" Type=\"", (int)plan.UnitScale.ScaleSystemType,
                    "\" Precision=\"", (int)plan.UnitScale.Precision,
                    "\" SetManually=\"", plan.UnitScale.SetManually,
                    "\" Engineering=\"", plan.UnitScale.Engineering,
                    "\"/>"
                ));

                sw.WriteLine("\t\t\t<Bookmarks>");
                sw.WriteLine(string.Concat(
                    "\t\t\t\t<Bookmark Name=\"", Utilities.EscapeString(plan.DefaultBookmark.Name),
                    "\" LayerIndex=\"", plan.DefaultBookmark.LayerIndex,
                    "\" Zoom=\"", plan.DefaultBookmark.Zoom,
                    "\" X=\"", plan.DefaultBookmark.Origin.X,
                    "\" Y=\"", plan.DefaultBookmark.Origin.Y,
                    "\"/>"
                ));
                sw.WriteLine("\t\t\t</Bookmarks>");

                sw.WriteLine(string.Concat("\t\t\t<Comment>", Utilities.EscapeString(plan.Comment.Replace("\n", "`").Replace("\r", "")), "</Comment>"));

                sw.WriteLine("\t\t\t<Layers>");

                for (int i = 0; i < plan.Layers.Count; i++)
                {
                    Layer layer = plan.Layers[i];
                    sw.WriteLine(string.Concat(
                        "\t\t\t\t<Layer Index=\"", i,
                        "\" Name=\"", Utilities.EscapeString(layer.Name),
                        "\" Opacity=\"", layer.Opacity,
                        "\" Visible=\"", layer.Visible,
                        "\" Active=\"", layer.Active,
                        "\">"
                    ));

                    ArrayList exportedGroupKeys = new ArrayList();

                    for (int j = layer.DrawingObjects.Count - 1; j >= 0; j--)
                    {
                        DrawObject obj = layer.DrawingObjects[j];

                        bool skipAsDuplicateGroupRoot = false;
                        string objectType = obj.ObjectType;

                        if (obj.IsPartOfGroup())
                        {
                            skipAsDuplicateGroupRoot = (obj.DeductionParentID != -1);
                            if (!skipAsDuplicateGroupRoot)
                                skipAsDuplicateGroupRoot = exportedGroupKeys.Contains(string.Concat(objectType, obj.GroupID));
                        }

                        if (skipAsDuplicateGroupRoot)
                            continue;

                        string attr = string.Concat("Name=\"", Utilities.EscapeString(obj.Name), "\" ");

                        if (objectType == "Rectangle")
                        {
                            DrawRectangle rect = (DrawRectangle)obj;
                            attr = string.Concat(attr, "X=\"", rect.Location.X, "\" ");
                            attr = string.Concat(attr, "Y=\"", rect.Location.Y, "\" ");
                            attr = string.Concat(attr, "Width=\"", rect.Size.Width, "\" ");
                            attr = string.Concat(attr, "Height=\"", rect.Size.Height, "\" ");
                        }

                        if (objectType == "Angle")
                        {
                            DrawAngle angle = (DrawAngle)obj;
                            if (angle.PointArray.Count >= 3)
                            {
                                Point p0 = (Point)angle.PointArray[0];
                                Point p1 = (Point)angle.PointArray[1];
                                Point p2 = (Point)angle.PointArray[2];

                                attr = string.Concat(attr, "X1=\"", p0.X, "\" ");
                                attr = string.Concat(attr, "Y1=\"", p0.Y, "\" ");
                                attr = string.Concat(attr, "X2=\"", p1.X, "\" ");
                                attr = string.Concat(attr, "Y2=\"", p1.Y, "\" ");
                                attr = string.Concat(attr, "X3=\"", p2.X, "\" ");
                                attr = string.Concat(attr, "Y3=\"", p2.Y, "\" ");
                            }
                            attr = string.Concat(attr, "AngleType=\"", (int)angle.AngleType, "\" ");
                        }

                        if (objectType == "Note")
                        {
                            DrawNote note = (DrawNote)obj;
                            attr = string.Concat(attr, "X=\"", note.Location.X, "\" ");
                            attr = string.Concat(attr, "Y=\"", note.Location.Y, "\" ");
                            attr = string.Concat(attr, "Width=\"", note.Size.Width, "\" ");
                            attr = string.Concat(attr, "Height=\"", note.Size.Height, "\" ");
                            attr = string.Concat(attr, "AnchorX=\"", note.StartPoint.X, "\" ");
                            attr = string.Concat(attr, "AnchorY=\"", note.StartPoint.Y, "\" ");
                            attr = string.Concat(attr, "FontSize=\"", note.FontSize, "\" ");
                        }

                        if (objectType == "Legend")
                        {
                            DrawLegend legend = (DrawLegend)obj;
                            attr = string.Concat(attr, "X=\"", legend.Location.X, "\" ");
                            attr = string.Concat(attr, "Y=\"", legend.Location.Y, "\" ");
                            attr = string.Concat(attr, "FontSize=\"", legend.FontSize, "\" ");
                            attr = string.Concat(attr, "MaxRows=\"", legend.MaxRows, "\" ");
                        }

                        if (obj.IsPartOfGroup())
                        {
                            attr = string.Concat(attr, "GroupID=\"", obj.GroupID, "\" ");
                            exportedGroupKeys.Add(string.Concat(objectType, obj.GroupID));
                        }

                        if (objectType == "Counter")
                        {
                            DrawCounter counter = (DrawCounter)obj;
                            attr = string.Concat(attr, "Shape=\"", (int)counter.Shape, "\" ");
                            attr = string.Concat(attr, (counter.ImageFileName == string.Empty ? string.Empty : string.Concat("FileName=\"", counter.ImageFileName, "\" ")));
                            attr = string.Concat(attr, "DefaultSize=\"", counter.DefaultSize, "\" ");
                            attr = string.Concat(attr, "Text=\"", Utilities.EscapeString(counter.Text), "\" ");
                        }

                        attr = string.Concat(attr, "Color=\"", obj.Color.ToArgb(), "\" ");
                        attr = string.Concat(attr, "PenWidth=\"", obj.PenWidth, "\" ");
                        attr = string.Concat(attr, "PenType=\"", obj.PenType, "\" ");

                        if (objectType == "Rectangle" || objectType == "Counter" || objectType == "Note" || objectType == "Legend" || objectType == "Area")
                            attr = string.Concat(attr, "FillColor=\"", obj.FillColor.ToArgb(), "\" ");

                        if (objectType == "Area")
                            attr = string.Concat(attr, "Pattern=\"", (int)((DrawPolyLine)obj).Pattern, "\" ");

                        if (objectType == "Area" || objectType == "Perimeter" || objectType == "Line")
                        {
                            DrawLine sloped = (DrawLine)obj;
                            if (sloped.SlopeFactor.InternalValue > 0)
                            {
                                attr = string.Concat(attr, "Slope=\"", sloped.SlopeFactor.InternalValue, "\" ");
                                attr = string.Concat(attr, "SlopeType=\"", (int)sloped.SlopeFactor.SlopeType, "\" ");
                                attr = string.Concat(attr, "SlopeApply=\"", (int)sloped.SlopeFactor.SlopeApplyType, "\" ");
                                attr = string.Concat(attr, "HipValley=\"", (int)sloped.SlopeFactor.HipValley, "\" ");
                            }
                        }

                        attr = string.Concat(attr, "ShowMeasure=\"", obj.ShowMeasure, "\" ");
                        attr = string.Concat(attr, "Visible=\"", obj.Visible, "\"");

                        string lineOpen = string.Concat("\t\t\t\t\t<", objectType, " ", attr);
                        lineOpen = (obj.GroupID > -1 || obj.Comment != string.Empty) ? string.Concat(lineOpen, ">") : string.Concat(lineOpen, "/>");
                        sw.WriteLine(lineOpen);

                        if (obj.Comment != string.Empty)
                            sw.WriteLine(string.Concat("\t\t\t\t\t\t<Comment>", Utilities.EscapeString(obj.Comment.Replace("\n", "`").Replace("\r", "")), "</Comment>"));

                        if (obj.GroupID > -1)
                        {
                            for (int k = layer.DrawingObjects.Count - 1; k >= 0; k--)
                            {
                                DrawObject groupMember = layer.DrawingObjects[k];
                                if (groupMember.GroupID != obj.GroupID)
                                    continue;

                                string memberType = groupMember.ObjectType;

                                if (memberType == "Line")
                                {
                                    DrawLine lineMember = (DrawLine)groupMember;
                                    if (lineMember.DeductionParentID == -1)
                                    {
                                        string el = string.Concat("\t\t\t\t\t\t<Element ", (lineMember.Label == string.Empty ? string.Empty : string.Concat("Label=\"", Utilities.EscapeString(lineMember.Label), "\" ")));
                                        el = string.Concat(el, "X1=\"", lineMember.StartPoint.X, "\" ");
                                        el = string.Concat(el, "Y1=\"", lineMember.StartPoint.Y, "\" ");
                                        el = string.Concat(el, "X2=\"", lineMember.EndPoint.X, "\" ");
                                        el = string.Concat(el, "Y2=\"", lineMember.EndPoint.Y, "\" ");
                                        sw.WriteLine(string.Concat(el, "/>"));
                                    }
                                }
                                else if (memberType == "Counter")
                                {
                                    DrawCounter counterMember = (DrawCounter)groupMember;
                                    string el = string.Concat("\t\t\t\t\t\t<Element ", (counterMember.Label == string.Empty ? string.Empty : string.Concat("Label=\"", Utilities.EscapeString(counterMember.Label), "\" ")));
                                    el = string.Concat(el, "X=\"", counterMember.Location.X, "\" ");
                                    el = string.Concat(el, "Y=\"", counterMember.Location.Y, "\" ");
                                    el = string.Concat(el, "Width=\"", counterMember.Size.Width, "\" ");
                                    el = string.Concat(el, "Height=\"", counterMember.Size.Height, "\"");
                                    sw.WriteLine(string.Concat(el, "/>"));
                                }
                                else if (memberType == "Area" || memberType == "Perimeter")
                                {
                                    DrawPolyLine poly = (DrawPolyLine)groupMember;
                                    if (poly.DeductionParentID != -1)
                                        continue;

                                    if (objectType != "Area")
                                        sw.WriteLine(string.Concat("\t\t\t\t\t\t<Element", (poly.Label == string.Empty ? string.Empty : string.Concat(" Label=\"", Utilities.EscapeString(poly.Label), "\"")), " Closed=\"", poly.CloseFigure, "\">"));
                                    else
                                        sw.WriteLine(string.Concat("\t\t\t\t\t\t<Element", (poly.Label == string.Empty ? string.Empty : string.Concat(" Label=\"", Utilities.EscapeString(poly.Label), "\"")), ">"));

                                    foreach (Point pt in poly.PointArray)
                                        sw.WriteLine(string.Concat("\t\t\t\t\t\t\t<Point X=\"", pt.X, "\" Y=\"", pt.Y, "\"/>"));

                                    foreach (DrawLine drop in poly.DropArray)
                                        sw.WriteLine(string.Concat("\t\t\t\t\t\t\t<Drop X=\"", drop.StartPoint.X, "\" Y=\"", drop.StartPoint.Y, "\" Height=\"", drop.Height, "\"/>"));

                                    if (poly.CustomRenderingArray.Count > 0)
                                    {
                                        foreach (KeyValuePair<string, CustomRenderingProperties> cr in poly.CustomRenderingArray)
                                        {
                                            CustomRenderingProperties v = cr.Value;
                                            string crLine = "\t\t\t\t\t\t\t<CustomRendering ";
                                            crLine = string.Concat(crLine, "ExtentionID=\"", cr.Key, "\" ");
                                            crLine = string.Concat(crLine, "Angle=\"", v.Angle, "\" ");
                                            crLine = string.Concat(crLine, "OffsetX=\"", v.OffsetX, "\" ");
                                            crLine = string.Concat(crLine, "OffsetY=\"", v.OffsetY, "\"");
                                            sw.WriteLine(string.Concat(crLine, "/>"));
                                        }
                                    }

                                    if (poly.DeductionArray.Count > 0)
                                    {
                                        sw.WriteLine("\t\t\t\t\t\t\t<Deductions>");

                                        if (objectType != "Area")
                                        {
                                            foreach (DrawLine d in poly.DeductionArray)
                                            {
                                                string el = string.Concat("\t\t\t\t\t\t\t\t\t<Element ", (d.Height == 0 ? string.Empty : string.Concat("Height=\"", d.Height, "\" ")));
                                                el = string.Concat(el, "X1=\"", d.StartPoint.X, "\" ");
                                                el = string.Concat(el, "Y1=\"", d.StartPoint.Y, "\" ");
                                                el = string.Concat(el, "X2=\"", d.EndPoint.X, "\" ");
                                                el = string.Concat(el, "Y2=\"", d.EndPoint.Y, "\" ");
                                                sw.WriteLine(string.Concat(el, "/>"));
                                            }
                                        }
                                        else
                                        {
                                            foreach (DrawPolyLine dPoly in poly.DeductionArray)
                                            {
                                                sw.WriteLine("\t\t\t\t\t\t\t\t<Element>");
                                                foreach (Point p in dPoly.PointArray)
                                                    sw.WriteLine(string.Concat("\t\t\t\t\t\t\t\t\t<Point X=\"", p.X, "\" Y=\"", p.Y, "\"/>"));
                                                sw.WriteLine("\t\t\t\t\t\t\t\t</Element>");
                                            }
                                        }

                                        sw.WriteLine("\t\t\t\t\t\t\t</Deductions>");
                                    }

                                    sw.WriteLine("\t\t\t\t\t\t</Element>");
                                }
                            }
                        }

                        if (obj.IsPartOfGroup() || obj.Comment != string.Empty)
                            sw.WriteLine(string.Concat("\t\t\t\t\t</", objectType, ">"));
                    }

                    sw.WriteLine("\t\t\t\t</Layer>");
                }

                sw.WriteLine("\t\t\t</Layers>");
                sw.WriteLine("\t\t</Plan>");
            }

            sw.WriteLine("\t</Plans>");

            sw.WriteLine("\t<Prices>");
            foreach (DictionaryEntry entry in this.EstimatingItems.EstimatingPrices)
            {
                EstimatingItemPrice price = (EstimatingItemPrice)entry.Value;
                if (!this.EstimatingPriceExists(price))
                    continue;

                sw.WriteLine(string.Concat(
                    "\t\t<Price Key=\"", Utilities.EscapeString(price.Key.ToString()),
                    "\" CostEach=\"", price.CostEach,
                    "\" MarkupEach=\"", price.MarkupEach,
                    "\" SystemType=\"", (int)price.SystemType,
                    "\"/>"
                ));
            }
            sw.WriteLine("\t</Prices>");

            sw.WriteLine("\t<Reports>");
            sw.WriteLine(string.Concat(
                "\t\t<Report Name=\"Default\" Order=\"", Utilities.ConvertToInt(this.Report.Order),
                "\" ScaleType=\"", Utilities.ConvertToInt(this.Report.SystemType),
                "\" Precision=\"", Utilities.ConvertToInt(this.Report.Precision),
                "\">"
            ));

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
            string key = Utilities.GetStringAttribute(reader, "Key", "");
            EstimatingItemPrice price = new EstimatingItemPrice(
                key,
                Utilities.GetDoubleAttribute(reader, "CostEach", 0),
                Utilities.GetDoubleAttribute(reader, "MarkupEach", 0),
                (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "SystemType", 0)
            );

            this.EstimatingItems.EstimatingPrices.Add(key, price);
        }

        private void SetObjectsZorder()
        {
            this.ObjectCounter = 0;

            foreach (Plan plan in this.Plans.Collection)
            {
                foreach (Layer layer in plan.Layers.Collection)
                    layer.DrawingObjects.Collection.Reverse();

                foreach (Layer layer in plan.Layers.Collection)
                {
                    for (int i = layer.DrawingObjects.Count - 1; i >= 0; i--)
                    {
                        DrawObject obj = layer.DrawingObjects[i];

                        this.ObjectCounter++;
                        obj.ID = this.ObjectCounter;

                        if (obj.ObjectType == "Area" || obj.ObjectType == "Perimeter")
                        {
                            foreach (DrawLine deduction in ((DrawPolyLine)obj).DeductionArray)
                                deduction.DeductionParentID = obj.ID;
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
                using (ProjectForm form = new ProjectForm(this, creationMode))
                {
                    form.HelpUtilities = parentForm.HelpUtilities;
                    form.HelpContextString = "ProjectForm";
                    form.ShowDialog(parentForm);
                }
            }
            catch (Exception ex)
            {
                Utilities.DisplaySystemError(ex);
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
            public int Compare(object x, object y)
            {
                try
                {
                    Plan a = x as Plan;
                    Plan b = y as Plan;
                    return a.ReorderIndex.CompareTo(b.ReorderIndex);
                }
                catch (Exception ex)
                {
                    Utilities.DisplaySystemError(ex);
                    return -1;
                }
            }
        }
    }
}
