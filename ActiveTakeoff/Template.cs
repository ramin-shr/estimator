using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace QuoterPlan
{
	public class Template : BaseFileInfo
	{
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
						streamWriter.Close();
						base.FullFileName = fileName;
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

		private void SaveToStream(StreamWriter sw)
		{
			sw.WriteLine("<?xml version=\"1.0\"?>");
			sw.WriteLine("<Template" + (this.DeletionForbidden ? " DeletionForbidden=\"True\"" : string.Empty) + ">");
			string objectType = this.DrawObject.ObjectType;
			string text = "Name=\"" + Utilities.EscapeString(this.DrawObject.Name) + "\" ";
			if (objectType == "Counter")
			{
				DrawCounter drawCounter = (DrawCounter)this.DrawObject;
				text = text + "Text=\"" + Utilities.EscapeString(drawCounter.Text) + "\" ";
			}
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"Color=\"",
				this.DrawObject.Color.ToArgb(),
				"\" "
			});
			if (objectType == "Perimeter" || objectType == "Line")
			{
				object obj2 = text;
				text = string.Concat(new object[]
				{
					obj2,
					"PenWidth=\"",
					this.DrawObject.PenWidth,
					"\" "
				});
			}
			if (objectType == "Counter" || objectType == "Area")
			{
				object obj3 = text;
				text = string.Concat(new object[]
				{
					obj3,
					"FillColor=\"",
					this.DrawObject.FillColor.ToArgb(),
					"\" "
				});
			}
			if (objectType == "Area")
			{
				object obj4 = text;
				text = string.Concat(new object[]
				{
					obj4,
					"Pattern=\"",
					(int)((DrawPolyLine)this.DrawObject).Pattern,
					"\" "
				});
			}
			if (objectType == "Counter")
			{
				object obj5 = text;
				text = string.Concat(new object[]
				{
					obj5,
					"Shape=\"",
					(int)((DrawCounter)this.DrawObject).Shape,
					"\" "
				});
				object obj6 = text;
				text = string.Concat(new object[]
				{
					obj6,
					"DefaultSize=\"",
					((DrawCounter)this.DrawObject).DefaultSize,
					"\" "
				});
			}
			if (objectType == "Area" || objectType == "Perimeter" || objectType == "Line")
			{
				DrawLine drawLine = (DrawLine)this.DrawObject;
				if (drawLine.SlopeFactor.InternalValue > 0.0)
				{
					object obj7 = text;
					text = string.Concat(new object[]
					{
						obj7,
						"Slope=\"",
						drawLine.SlopeFactor.InternalValue,
						"\" "
					});
					object obj8 = text;
					text = string.Concat(new object[]
					{
						obj8,
						"SlopeType=\"",
						(int)drawLine.SlopeFactor.SlopeType,
						"\" "
					});
					object obj9 = text;
					text = string.Concat(new object[]
					{
						obj9,
						"SlopeApply=\"",
						(int)drawLine.SlopeFactor.SlopeApplyType,
						"\" "
					});
					object obj10 = text;
					text = string.Concat(new object[]
					{
						obj10,
						"HipValley=\"",
						(int)drawLine.SlopeFactor.HipValley,
						"\" "
					});
				}
			}
			sw.WriteLine(string.Concat(new string[]
			{
				"\t<",
				objectType,
				" ",
				text,
				"/>"
			}));
			if (this.DrawObject.Comment != string.Empty)
			{
				sw.WriteLine("\t<Comment>" + Utilities.EscapeString(this.DrawObject.Comment.Replace("\n", "`").Replace("\r", "")) + "</Comment>");
			}
			foreach (object obj11 in this.Presets.Collection)
			{
				Preset preset = (Preset)obj11;
				string text2 = "\t<Extension ";
				text2 = text2 + "DisplayName=\"" + Utilities.EscapeString(preset.DisplayName) + "\" ";
				text2 = text2 + "Name=\"" + Utilities.EscapeString(preset.ExtensionName) + "\" ";
				text2 = text2 + "Category=\"" + Utilities.EscapeString(preset.CategoryName) + "\" ";
				object obj12 = text2;
				text2 = string.Concat(new object[]
				{
					obj12,
					"ScaleType=\"",
					(int)preset.ScaleSystemType,
					"\""
				});
				text2 += ">";
				sw.WriteLine(text2);
				foreach (object obj13 in preset.Choices.Collection)
				{
					PresetChoice presetChoice = (PresetChoice)obj13;
					sw.WriteLine(string.Concat(new string[]
					{
						"\t\t<Choice Name=\"",
						Utilities.EscapeString(presetChoice.ChoiceName),
						"\" Element=\"",
						Utilities.EscapeString(presetChoice.ChoiceElementName),
						"\"/>"
					}));
				}
				foreach (object obj14 in preset.Fields.Collection)
				{
					PresetField presetField = (PresetField)obj14;
					sw.WriteLine(string.Concat(new object[]
					{
						"\t\t<Field Name=\"",
						Utilities.EscapeString(presetField.Name),
						"\" Value=\"",
						presetField.Value,
						"\"/>"
					}));
				}
				sw.WriteLine("\t</Extension>");
			}
			foreach (CEstimatingItem cestimatingItem in this.EstimatingItems.Collection)
			{
				string text3 = "\t<CEstimatingItem ";
				text3 = text3 + "ItemID=\"" + cestimatingItem.ItemID + "\" ";
				text3 = text3 + "Description=\"" + Utilities.EscapeString(cestimatingItem.Description) + "\" ";
				text3 = text3 + "Unit=\"" + Utilities.EscapeString(cestimatingItem.Unit) + "\" ";
				object obj15 = text3;
				text3 = string.Concat(new object[]
				{
					obj15,
					"ItemType=\"",
					(int)cestimatingItem.ItemType,
					"\" "
				});
				object obj16 = text3;
				text3 = string.Concat(new object[]
				{
					obj16,
					"UnitMeasure=\"",
					(int)cestimatingItem.UnitMeasure,
					"\" "
				});
				object obj17 = text3;
				text3 = string.Concat(new object[]
				{
					obj17,
					"CoverageValue=\"",
					cestimatingItem.CoverageValue,
					"\" "
				});
				object obj18 = text3;
				text3 = string.Concat(new object[]
				{
					obj18,
					"CoverageUnit=\"",
					cestimatingItem.CoverageUnit,
					"\" "
				});
				object obj19 = text3;
				text3 = string.Concat(new object[]
				{
					obj19,
					"SectionID=\"",
					cestimatingItem.SectionID,
					"\" "
				});
				object obj20 = text3;
				text3 = string.Concat(new object[]
				{
					obj20,
					"SubSectionID=\"",
					cestimatingItem.SubSectionID,
					"\" "
				});
				text3 = text3 + "BidCode=\"" + Utilities.EscapeString(cestimatingItem.BidCode) + "\" ";
				text3 = text3 + "Formula=\"" + Utilities.EscapeString(cestimatingItem.Formula) + "\"";
				text3 += "/>";
				sw.WriteLine(text3);
			}
			foreach (CEstimatingItem cestimatingItem2 in this.COfficeProducts.Collection)
			{
				string text4 = "\t<COfficeProduct ";
				text4 = text4 + "ItemID=\"" + cestimatingItem2.ItemID + "\" ";
				text4 = text4 + "Description=\"" + Utilities.EscapeString(cestimatingItem2.Description) + "\" ";
				object obj21 = text4;
				text4 = string.Concat(new object[]
				{
					obj21,
					"Cost=\"",
					cestimatingItem2.Value,
					"\" "
				});
				text4 = text4 + "Unit=\"" + Utilities.EscapeString(cestimatingItem2.Unit) + "\" ";
				text4 = text4 + "Formula=\"" + Utilities.EscapeString(cestimatingItem2.Formula) + "\"";
				text4 += "/>";
				sw.WriteLine(text4);
			}
			sw.WriteLine("</Template>");
		}

		public bool Open(string fileName, ExtensionsSupport extensionSupport)
		{
			this.Clear();
			base.FullFileName = fileName;
			bool result;
			try
			{
				using (XmlTextReader xmlTextReader = new XmlTextReader(fileName))
				{
					this.ReadFromStream(xmlTextReader, extensionSupport);
					xmlTextReader.Close();
					result = (this.DrawObject != null);
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileOpenError(fileName, exception);
				result = false;
			}
			return result;
		}

		private void ReadFromStream(XmlTextReader reader, ExtensionsSupport extensionSupport)
		{
			string text = "";
			Utilities.NumberDecimalSeparator();
			Preset preset = null;
			while (reader.Read())
			{
				XmlNodeType nodeType = reader.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				{
					text = reader.Name.ToUpper();
					string key;
					switch (key = text)
					{
					case "TEMPLATE":
						this.SystemTemplate = Utilities.GetBoolAttribute(reader, "SystemTemplate", false);
						this.DeletionForbidden = Utilities.GetBoolAttribute(reader, "DeletionForbidden", false);
						break;
					case "AREA":
					{
						string stringAttribute = Utilities.GetStringAttribute(reader, "Pattern", string.Empty);
						HatchStylePickerCombo.HatchStylePickerEnum pattern = (HatchStylePickerCombo.HatchStylePickerEnum)((stringAttribute == string.Empty) ? -1 : Utilities.GetIntegerAttribute(reader, "Pattern", -1));
						DrawPolyLine drawPolyLine = new DrawPolyLine
						{
							Name = Utilities.GetStringAttribute(reader, "Name", ""),
							Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
							FillColor = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
							Pattern = pattern,
							Filled = true
						};
						drawPolyLine.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0.0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), SlopeFactor.HipValleyEnum.hipValleyUnavailable);
						this.DrawObject = drawPolyLine;
						break;
					}
					case "PERIMETER":
					{
						DrawPolyLine drawPolyLine2 = new DrawPolyLine
						{
							Name = Utilities.GetStringAttribute(reader, "Name", ""),
							Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
							PenWidth = Utilities.GetIntegerAttribute(reader, "PenWidth", 4),
							Filled = false
						};
						drawPolyLine2.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0.0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
						this.DrawObject = drawPolyLine2;
						break;
					}
					case "COUNTER":
					{
						DrawCounter drawObject = new DrawCounter
						{
							Name = Utilities.GetStringAttribute(reader, "Name", ""),
							Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
							FillColor = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")),
							Shape = (DrawCounter.CounterShapeTypeEnum)Utilities.GetIntegerAttribute(reader, "Shape", 0),
							DefaultSize = Utilities.GetIntegerAttribute(reader, "DefaultSize", 80),
							Text = Utilities.GetStringAttribute(reader, "Text", "")
						};
						this.DrawObject = drawObject;
						break;
					}
					case "LINE":
					{
						DrawLine drawLine = new DrawLine
						{
							Name = Utilities.GetStringAttribute(reader, "Name", ""),
							Color = ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")),
							PenWidth = Utilities.GetIntegerAttribute(reader, "PenWidth", 4)
						};
						drawLine.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0.0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
						this.DrawObject = drawLine;
						break;
					}
					case "EXTENSION":
					{
						UnitScale.UnitSystem scaleSystemType;
						if (this.SystemTemplate)
						{
							scaleSystemType = UnitScale.DefaultUnitSystem();
						}
						else
						{
							scaleSystemType = (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2);
						}
						string stringAttribute2 = Utilities.GetStringAttribute(reader, "Name", "");
						string text2 = Utilities.GetStringAttribute(reader, "DisplayName", "");
						if (text2 == "")
						{
							ExtensionCategory extensionCategory = null;
							Extension extension = extensionSupport.FindExtension(ref extensionCategory, stringAttribute2);
							if (extension != null)
							{
								text2 = extension.Caption;
							}
						}
						if (text2 == "")
						{
							text2 = stringAttribute2;
						}
						text2 = this.Presets.GetFreeDisplayName(text2, "");
						preset = new Preset(Guid.NewGuid().ToString(), text2, Utilities.GetStringAttribute(reader, "Category", ""), stringAttribute2, scaleSystemType);
						this.Presets.Add(preset);
						break;
					}
					case "CESTIMATINGITEM":
					{
						CEstimatingItem cestimatingItem = new CEstimatingItem();
						cestimatingItem.ItemID = Utilities.GetStringAttribute(reader, "ItemID", "");
						if (cestimatingItem.ItemID != "")
						{
							cestimatingItem.Description = Utilities.GetStringAttribute(reader, "Description", "");
							cestimatingItem.Value = -1.0;
							cestimatingItem.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
							cestimatingItem.ItemType = (DBEstimatingItem.EstimatingItemType)Utilities.GetIntegerAttribute(reader, "ItemType", 0);
							cestimatingItem.UnitMeasure = (DBEstimatingItem.UnitMeasureType)Utilities.GetIntegerAttribute(reader, "UnitMeasure", 0);
							cestimatingItem.CoverageValue = Utilities.GetDoubleAttribute(reader, "CoverageValue", 0.0);
							cestimatingItem.CoverageUnit = (double)Utilities.GetIntegerAttribute(reader, "CoverageUnit", 0);
							cestimatingItem.SectionID = Utilities.GetIntegerAttribute(reader, "SectionID", 0);
							cestimatingItem.SubSectionID = Utilities.GetIntegerAttribute(reader, "SubSectionID", 0);
							cestimatingItem.BidCode = Utilities.GetStringAttribute(reader, "BidCode", "");
							cestimatingItem.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
							cestimatingItem.Tag = cestimatingItem;
							this.EstimatingItems.Add(cestimatingItem);
						}
						break;
					}
					case "COFFICEPRODUCT":
					{
						CEstimatingItem cestimatingItem2 = new CEstimatingItem();
						cestimatingItem2.ItemID = Utilities.GetStringAttribute(reader, "ItemID", "");
						if (cestimatingItem2.ItemID != "")
						{
							cestimatingItem2.Description = Utilities.GetStringAttribute(reader, "Description", "");
							cestimatingItem2.Value = Utilities.GetDoubleAttribute(reader, "Cost", 0.0);
							cestimatingItem2.Unit = Utilities.GetStringAttribute(reader, "Unit", "");
							cestimatingItem2.Formula = Utilities.GetStringAttribute(reader, "Formula", "");
							cestimatingItem2.Tag = cestimatingItem2;
							this.COfficeProducts.Add(cestimatingItem2);
						}
						break;
					}
					case "CHOICE":
						if (preset != null)
						{
							string stringAttribute3;
							if (this.SystemTemplate)
							{
								if (UnitScale.DefaultUnitSystem() == UnitScale.UnitSystem.imperial)
								{
									stringAttribute3 = Utilities.GetStringAttribute(reader, "imperialElement", "");
								}
								else
								{
									stringAttribute3 = Utilities.GetStringAttribute(reader, "metricElement", "");
								}
							}
							else
							{
								stringAttribute3 = Utilities.GetStringAttribute(reader, "Element", "");
							}
							preset.Choices.Add(new PresetChoice(Utilities.GetStringAttribute(reader, "Name", ""), stringAttribute3));
						}
						break;
					case "FIELD":
						if (preset != null)
						{
							object stringAttribute4;
							if (this.SystemTemplate)
							{
								if (UnitScale.DefaultUnitSystem() == UnitScale.UnitSystem.imperial)
								{
									stringAttribute4 = Utilities.GetStringAttribute(reader, "imperialValue", "");
								}
								else
								{
									stringAttribute4 = Utilities.GetStringAttribute(reader, "metricValue", "");
								}
							}
							else
							{
								stringAttribute4 = Utilities.GetStringAttribute(reader, "Value", "");
							}
							preset.Fields.Add(new PresetField(Utilities.GetStringAttribute(reader, "Name", ""), stringAttribute4));
						}
						break;
					}
					break;
				}
				case XmlNodeType.Attribute:
					break;
				case XmlNodeType.Text:
				{
					string a;
					if ((a = text) != null && a == "COMMENT")
					{
						this.DrawObject.Comment = reader.Value.Trim().Replace("`", "\r\n");
					}
					break;
				}
				default:
					if (nodeType == XmlNodeType.EndElement)
					{
						string a2;
						if ((a2 = reader.Name.ToUpper()) != null && a2 == "EXTENSION")
						{
							preset = null;
						}
					}
					break;
				}
			}
		}

		public string ID
		{
			get
			{
				return Path.GetFileNameWithoutExtension(base.FileName);
			}
		}

		public bool SystemTemplate
		{
			[CompilerGenerated]
			get
			{
				return this.<SystemTemplate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SystemTemplate>k__BackingField = value;
			}
		}

		public bool DeletionForbidden
		{
			[CompilerGenerated]
			get
			{
				return this.<DeletionForbidden>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DeletionForbidden>k__BackingField = value;
			}
		}

		public bool CreatedFromObject
		{
			[CompilerGenerated]
			get
			{
				return this.<CreatedFromObject>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CreatedFromObject>k__BackingField = value;
			}
		}

		public DrawObject DrawObject
		{
			[CompilerGenerated]
			get
			{
				return this.<DrawObject>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DrawObject>k__BackingField = value;
			}
		}

		public Presets Presets
		{
			[CompilerGenerated]
			get
			{
				return this.<Presets>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Presets>k__BackingField = value;
			}
		}

		public CEstimatingItems EstimatingItems
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatingItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EstimatingItems>k__BackingField = value;
			}
		}

		public CEstimatingItems COfficeProducts
		{
			[CompilerGenerated]
			get
			{
				return this.<COfficeProducts>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<COfficeProducts>k__BackingField = value;
			}
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
				Console.WriteLine("Name = " + this.DrawObject.Name);
				Console.WriteLine("Color = " + this.DrawObject.Color);
				Console.WriteLine("PenType = " + this.DrawObject.PenType);
			}
			this.Presets.Dump();
			this.EstimatingItems.Dump();
			this.COfficeProducts.Dump();
		}

		[CompilerGenerated]
		private bool <SystemTemplate>k__BackingField;

		[CompilerGenerated]
		private bool <DeletionForbidden>k__BackingField;

		[CompilerGenerated]
		private bool <CreatedFromObject>k__BackingField;

		[CompilerGenerated]
		private DrawObject <DrawObject>k__BackingField;

		[CompilerGenerated]
		private Presets <Presets>k__BackingField;

		[CompilerGenerated]
		private CEstimatingItems <EstimatingItems>k__BackingField;

		[CompilerGenerated]
		private CEstimatingItems <COfficeProducts>k__BackingField;
	}
}
