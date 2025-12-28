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
	public class Project : BaseFileInfo
	{
		public bool Save(string fileName, bool saveBackup = false)
		{
			bool result;
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

		private bool EstimatingPriceExists(EstimatingItemPrice estimatingItemPrice)
		{
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
			bool result;
			try
			{
				string[] fields = Utilities.GetFields(estimatingItemPrice.Key.ToString(), ';');
				string b = fields[1];
				string text = fields[2];
				bool flag = Utilities.IsNumber(text);
				if (flag)
				{
					using (List<CEstimatingItem>.Enumerator enumerator = drawObjectGroup.EstimatingItems.Collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CEstimatingItem cestimatingItem = enumerator.Current;
							if (cestimatingItem.InternalKey == b && cestimatingItem.ItemID == text)
							{
								return true;
							}
						}
						goto IL_187;
					}
				}
				foreach (object obj in drawObjectGroup.Presets.Collection)
				{
					Preset preset = (Preset)obj;
					if (preset.ID == b)
					{
						foreach (object obj2 in preset.Results.Collection)
						{
							PresetResult presetResult = (PresetResult)obj2;
							if (presetResult.Name == text && presetResult.ConditionMet)
							{
								return true;
							}
						}
					}
				}
				IL_187:
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void SaveToStream(StreamWriter sw)
		{
			sw.WriteLine("<?xml version=\"1.0\"?>");
			sw.WriteLine("<QuoterPlanSession>");
			sw.WriteLine("\t<Project Name=\"" + Utilities.EscapeString(base.Name) + "\">");
			sw.WriteLine("\t\t<Description>" + Utilities.EscapeString(this.Description.Replace("\n", "`").Replace("\r", "")) + "</Description>");
			sw.WriteLine("\t\t<ContactName>" + Utilities.EscapeString(this.ContactName) + "</ContactName>");
			sw.WriteLine("\t\t<ContactInfo>" + Utilities.EscapeString(this.ContactInfo.Replace("\n", "`").Replace("\r", "")) + "</ContactInfo>");
			sw.WriteLine("\t\t<JobNumber>" + Utilities.EscapeString(this.JobNumber) + "</JobNumber>");
			sw.WriteLine("\t\t<Comment>" + Utilities.EscapeString(this.Comment.Replace("\n", "`").Replace("\r", "")) + "</Comment>");
			sw.WriteLine("\t\t<CreationDate>" + this.CreationDate + "</CreationDate>");
			sw.WriteLine("\t\t<LastModified>" + this.LastModified + "</LastModified>");
			sw.WriteLine("\t\t<DisplayResultsForAllPlans>" + this.DisplayResultsForAllPlans + "</DisplayResultsForAllPlans>");
			sw.WriteLine("\t</Project>");
			sw.WriteLine("\t<Workspace>");
			if (this.Workspace.ActivePlan != null)
			{
				sw.WriteLine("\t\t<ActivePlan Name=\"" + Utilities.EscapeString(this.Workspace.ActivePlan.Name) + "\"/>");
			}
			if (this.Workspace.RecentPlans.Count > 0)
			{
				sw.WriteLine("\t\t<RecentPlans>");
				foreach (object obj in this.Workspace.RecentPlans.Collection)
				{
					Variable variable = (Variable)obj;
					Plan plan = this.Plans.FindPlan(variable.Name);
					if (plan != null)
					{
						sw.WriteLine("\t\t\t<Plan Name=\"" + Utilities.EscapeString(plan.Name) + "\"/>");
					}
				}
				sw.WriteLine("\t\t</RecentPlans>");
			}
			sw.WriteLine("\t</Workspace>");
			sw.WriteLine("\t<Plans>");
			foreach (object obj2 in this.Groups.Collection)
			{
				DrawObjectGroup drawObjectGroup = (DrawObjectGroup)obj2;
				if (drawObjectGroup.Presets.Count > 0 || drawObjectGroup.TemplateID != "" || drawObjectGroup.COfficeProducts.Collection.Count > 0 || drawObjectGroup.EstimatingItems.Count > 0)
				{
					sw.WriteLine(string.Concat(new string[]
					{
						"\t\t<Group GroupID=\"",
						drawObjectGroup.ID.ToString(),
						"\"",
						(drawObjectGroup.TemplateID != "") ? (" TemplateID=\"" + drawObjectGroup.TemplateID + "\"") : "",
						">"
					}));
					foreach (object obj3 in drawObjectGroup.Presets.Collection)
					{
						Preset preset = (Preset)obj3;
						string text = "\t\t\t<Extension ";
						text = text + "Id=\"" + preset.ID + "\" ";
						text = text + "DisplayName=\"" + Utilities.EscapeString(preset.DisplayName) + "\" ";
						text = text + "Name=\"" + Utilities.EscapeString(preset.ExtensionName) + "\" ";
						text = text + "Category=\"" + Utilities.EscapeString(preset.CategoryName) + "\" ";
						object obj4 = text;
						text = string.Concat(new object[]
						{
							obj4,
							"ScaleType=\"",
							(int)preset.ScaleSystemType,
							"\""
						});
						text += ">";
						sw.WriteLine(text);
						foreach (object obj5 in preset.Choices.Collection)
						{
							PresetChoice presetChoice = (PresetChoice)obj5;
							sw.WriteLine(string.Concat(new string[]
							{
								"\t\t\t\t<Choice Name=\"",
								Utilities.EscapeString(presetChoice.ChoiceName),
								"\" Element=\"",
								Utilities.EscapeString(presetChoice.ChoiceElementName),
								"\"/>"
							}));
						}
						foreach (object obj6 in preset.Fields.Collection)
						{
							PresetField presetField = (PresetField)obj6;
							sw.WriteLine(string.Concat(new object[]
							{
								"\t\t\t\t<Field Name=\"",
								Utilities.EscapeString(presetField.Name),
								"\" Value=\"",
								presetField.Value,
								"\"/>"
							}));
						}
						sw.WriteLine("\t\t\t</Extension>");
					}
					foreach (CEstimatingItem cestimatingItem in drawObjectGroup.EstimatingItems.Collection)
					{
						string text2 = "\t\t\t<EstimatingItem ";
						text2 = text2 + "ItemID=\"" + cestimatingItem.ItemID + "\" ";
						text2 = text2 + "Description=\"" + Utilities.EscapeString(cestimatingItem.Description) + "\" ";
						text2 = text2 + "Unit=\"" + Utilities.EscapeString(cestimatingItem.Unit) + "\" ";
						object obj4 = text2;
						text2 = string.Concat(new object[]
						{
							obj4,
							"ItemType=\"",
							(int)cestimatingItem.ItemType,
							"\" "
						});
						obj4 = text2;
						text2 = string.Concat(new object[]
						{
							obj4,
							"UnitMeasure=\"",
							(int)cestimatingItem.UnitMeasure,
							"\" "
						});
						obj4 = text2;
						text2 = string.Concat(new object[]
						{
							obj4,
							"CoverageValue=\"",
							cestimatingItem.CoverageValue,
							"\" "
						});
						obj4 = text2;
						text2 = string.Concat(new object[]
						{
							obj4,
							"CoverageUnit=\"",
							cestimatingItem.CoverageUnit,
							"\" "
						});
						obj4 = text2;
						text2 = string.Concat(new object[]
						{
							obj4,
							"SectionID=\"",
							cestimatingItem.SectionID,
							"\" "
						});
						obj4 = text2;
						text2 = string.Concat(new object[]
						{
							obj4,
							"SubSectionID=\"",
							cestimatingItem.SubSectionID,
							"\" "
						});
						text2 = text2 + "BidCode=\"" + Utilities.EscapeString(cestimatingItem.BidCode) + "\" ";
						text2 = text2 + "Formula=\"" + Utilities.EscapeString(cestimatingItem.Formula) + "\" ";
						text2 = text2 + "InternalKey=\"" + Utilities.EscapeString(cestimatingItem.InternalKey) + "\"";
						text2 += "/>";
						sw.WriteLine(text2);
					}
					foreach (CEstimatingItem cestimatingItem2 in drawObjectGroup.COfficeProducts.Collection)
					{
						string text3 = "\t\t\t<COfficeProduct ";
						text3 = text3 + "ItemID=\"" + cestimatingItem2.ItemID + "\" ";
						text3 = text3 + "Description=\"" + Utilities.EscapeString(cestimatingItem2.Description) + "\" ";
						object obj4 = text3;
						text3 = string.Concat(new object[]
						{
							obj4,
							"Cost=\"",
							cestimatingItem2.Value,
							"\" "
						});
						text3 = text3 + "Unit=\"" + Utilities.EscapeString(cestimatingItem2.Unit) + "\" ";
						text3 = text3 + "Formula=\"" + Utilities.EscapeString(cestimatingItem2.Formula) + "\"";
						text3 += "/>";
						sw.WriteLine(text3);
					}
					sw.WriteLine("\t\t</Group>");
				}
			}
			foreach (object obj7 in this.Plans.Collection)
			{
				Plan plan2 = (Plan)obj7;
				string s = Utilities.GetFileName(plan2.FullFileName, false);
				if (Utilities.GetParentDirectory(plan2.FullFileName) != base.FolderName)
				{
					string parentDirectory = Utilities.GetParentDirectory(plan2.FolderName);
					string text4 = Path.Combine(Utilities.GetParentDirectory(Path.GetDirectoryName(this.saveFileName)), Utilities.GetShortPlansFolder());
					s = ((parentDirectory.ToLower() == text4.ToLower()) ? Utilities.MakeRelativePath(text4 + "\\", plan2.FullFileName) : plan2.FullFileName);
				}
				sw.WriteLine(string.Concat(new string[]
				{
					"\t\t<Plan Name=\"",
					Utilities.EscapeString(plan2.Name),
					"\" FileName=\"",
					Utilities.EscapeString(s),
					"\"",
					(!plan2.Pinned) ? "" : (" Pinned=\"" + plan2.Pinned + "\""),
					(plan2.Brightness == 0) ? "" : (" Brightness=\"" + plan2.Brightness + "\""),
					(plan2.Contrast == 0) ? "" : (" Contrast=\"" + plan2.Contrast + "\""),
					">"
				}));
				plan2.Dirty = false;
				if (plan2.Thumbnail.FileName != "")
				{
					sw.WriteLine("\t\t\t<Thumbnail FileName=\"" + Utilities.EscapeString(plan2.Thumbnail.FileName) + "\"/>");
					plan2.Thumbnail.Dirty = false;
				}
				sw.WriteLine(string.Concat(new object[]
				{
					"\t\t\t<Scale Value=\"",
					plan2.UnitScale.Scale,
					"\" Type=\"",
					(int)plan2.UnitScale.ScaleSystemType,
					"\" Precision=\"",
					(int)plan2.UnitScale.Precision,
					"\" SetManually=\"",
					plan2.UnitScale.SetManually,
					"\" Engineering=\"",
					plan2.UnitScale.Engineering,
					"\"/>"
				}));
				sw.WriteLine("\t\t\t<Bookmarks>");
				sw.WriteLine(string.Concat(new object[]
				{
					"\t\t\t\t<Bookmark Name=\"",
					Utilities.EscapeString(plan2.DefaultBookmark.Name),
					"\" LayerIndex=\"",
					plan2.DefaultBookmark.LayerIndex,
					"\" Zoom=\"",
					plan2.DefaultBookmark.Zoom,
					"\" X=\"",
					plan2.DefaultBookmark.Origin.X,
					"\" Y=\"",
					plan2.DefaultBookmark.Origin.Y,
					"\"/>"
				}));
				sw.WriteLine("\t\t\t</Bookmarks>");
				sw.WriteLine("\t\t\t<Comment>" + Utilities.EscapeString(plan2.Comment.Replace("\n", "`").Replace("\r", "")) + "</Comment>");
				sw.WriteLine("\t\t\t<Layers>");
				for (int i = 0; i < plan2.Layers.Count; i++)
				{
					Layer layer = plan2.Layers[i];
					sw.WriteLine(string.Concat(new object[]
					{
						"\t\t\t\t<Layer Index=\"",
						i,
						"\" Name=\"",
						Utilities.EscapeString(layer.Name),
						"\" Opacity=\"",
						layer.Opacity,
						"\" Visible=\"",
						layer.Visible,
						"\" Active=\"",
						layer.Active,
						"\">"
					}));
					ArrayList arrayList = new ArrayList();
					for (int j = layer.DrawingObjects.Count - 1; j >= 0; j--)
					{
						bool flag = false;
						string text5 = string.Empty;
						DrawObject drawObject = layer.DrawingObjects[j];
						string objectType = drawObject.ObjectType;
						string text6 = "Name=\"" + Utilities.EscapeString(drawObject.Name) + "\" ";
						if (drawObject.IsPartOfGroup())
						{
							flag = (drawObject.DeductionParentID != -1);
							if (!flag)
							{
								flag = arrayList.Contains(objectType + drawObject.GroupID);
							}
						}
						if (!flag)
						{
							object obj4;
							if (objectType == "Rectangle")
							{
								DrawRectangle drawRectangle = (DrawRectangle)drawObject;
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"X=\"",
									drawRectangle.Location.X,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Y=\"",
									drawRectangle.Location.Y,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Width=\"",
									drawRectangle.Size.Width,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Height=\"",
									drawRectangle.Size.Height,
									"\" "
								});
							}
							if (objectType == "Angle")
							{
								DrawAngle drawAngle = (DrawAngle)drawObject;
								if (drawAngle.PointArray.Count >= 3)
								{
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"X1=\"",
										((Point)drawAngle.PointArray[0]).X,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"Y1=\"",
										((Point)drawAngle.PointArray[0]).Y,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"X2=\"",
										((Point)drawAngle.PointArray[1]).X,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"Y2=\"",
										((Point)drawAngle.PointArray[1]).Y,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"X3=\"",
										((Point)drawAngle.PointArray[2]).X,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"Y3=\"",
										((Point)drawAngle.PointArray[2]).Y,
										"\" "
									});
								}
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"AngleType=\"",
									(int)drawAngle.AngleType,
									"\" "
								});
							}
							if (objectType == "Note")
							{
								DrawNote drawNote = (DrawNote)drawObject;
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"X=\"",
									drawNote.Location.X,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Y=\"",
									drawNote.Location.Y,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Width=\"",
									drawNote.Size.Width,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Height=\"",
									drawNote.Size.Height,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"AnchorX=\"",
									drawNote.StartPoint.X,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"AnchorY=\"",
									drawNote.StartPoint.Y,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"FontSize=\"",
									drawNote.FontSize,
									"\" "
								});
							}
							if (objectType == "Legend")
							{
								DrawLegend drawLegend = (DrawLegend)drawObject;
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"X=\"",
									drawLegend.Location.X,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Y=\"",
									drawLegend.Location.Y,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"FontSize=\"",
									drawLegend.FontSize,
									"\" "
								});
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"MaxRows=\"",
									drawLegend.MaxRows,
									"\" "
								});
							}
							if (drawObject.IsPartOfGroup())
							{
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"GroupID=\"",
									drawObject.GroupID,
									"\" "
								});
								arrayList.Add(objectType + drawObject.GroupID);
							}
							if (objectType == "Counter")
							{
								DrawCounter drawCounter = (DrawCounter)drawObject;
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Shape=\"",
									(int)drawCounter.Shape,
									"\" "
								});
								text6 += ((drawCounter.ImageFileName == string.Empty) ? string.Empty : ("FileName=\"" + drawCounter.ImageFileName + "\" "));
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"DefaultSize=\"",
									drawCounter.DefaultSize,
									"\" "
								});
								text6 = text6 + "Text=\"" + Utilities.EscapeString(drawCounter.Text) + "\" ";
							}
							obj4 = text6;
							text6 = string.Concat(new object[]
							{
								obj4,
								"Color=\"",
								drawObject.Color.ToArgb(),
								"\" "
							});
							obj4 = text6;
							text6 = string.Concat(new object[]
							{
								obj4,
								"PenWidth=\"",
								drawObject.PenWidth,
								"\" "
							});
							obj4 = text6;
							text6 = string.Concat(new object[]
							{
								obj4,
								"PenType=\"",
								drawObject.PenType,
								"\" "
							});
							if (objectType == "Rectangle" || objectType == "Counter" || objectType == "Note" || objectType == "Legend" || objectType == "Area")
							{
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"FillColor=\"",
									drawObject.FillColor.ToArgb(),
									"\" "
								});
							}
							if (objectType == "Area")
							{
								obj4 = text6;
								text6 = string.Concat(new object[]
								{
									obj4,
									"Pattern=\"",
									(int)((DrawPolyLine)drawObject).Pattern,
									"\" "
								});
							}
							if (objectType == "Area" || objectType == "Perimeter" || objectType == "Line")
							{
								DrawLine drawLine = (DrawLine)drawObject;
								if (drawLine.SlopeFactor.InternalValue > 0.0)
								{
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"Slope=\"",
										drawLine.SlopeFactor.InternalValue,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"SlopeType=\"",
										(int)drawLine.SlopeFactor.SlopeType,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"SlopeApply=\"",
										(int)drawLine.SlopeFactor.SlopeApplyType,
										"\" "
									});
									obj4 = text6;
									text6 = string.Concat(new object[]
									{
										obj4,
										"HipValley=\"",
										(int)drawLine.SlopeFactor.HipValley,
										"\" "
									});
								}
							}
							obj4 = text6;
							text6 = string.Concat(new object[]
							{
								obj4,
								"ShowMeasure=\"",
								drawObject.ShowMeasure,
								"\" "
							});
							obj4 = text6;
							text6 = string.Concat(new object[]
							{
								obj4,
								"Visible=\"",
								drawObject.Visible,
								"\""
							});
							text5 = "\t\t\t\t\t<" + objectType + " " + text6;
							if (drawObject.GroupID > -1 || drawObject.Comment != string.Empty)
							{
								text5 += ">";
							}
							else
							{
								text5 += "/>";
							}
							sw.WriteLine(text5);
							if (drawObject.Comment != string.Empty)
							{
								sw.WriteLine("\t\t\t\t\t\t<Comment>" + Utilities.EscapeString(drawObject.Comment.Replace("\n", "`").Replace("\r", "")) + "</Comment>");
							}
							if (drawObject.GroupID > -1)
							{
								for (int k = layer.DrawingObjects.Count - 1; k >= 0; k--)
								{
									DrawObject drawObject2 = layer.DrawingObjects[k];
									string objectType2;
									if (drawObject2.GroupID == drawObject.GroupID && (objectType2 = drawObject2.ObjectType) != null)
									{
										if (!(objectType2 == "Line"))
										{
											if (!(objectType2 == "Counter"))
											{
												if (objectType2 == "Area" || objectType2 == "Perimeter")
												{
													DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject2;
													if (drawPolyLine.DeductionParentID == -1)
													{
														if (objectType == "Area")
														{
															sw.WriteLine("\t\t\t\t\t\t<Element" + ((drawPolyLine.Label == string.Empty) ? string.Empty : (" Label=\"" + Utilities.EscapeString(drawPolyLine.Label) + "\"")) + ">");
														}
														else
														{
															sw.WriteLine(string.Concat(new object[]
															{
																"\t\t\t\t\t\t<Element",
																(drawPolyLine.Label == string.Empty) ? string.Empty : (" Label=\"" + Utilities.EscapeString(drawPolyLine.Label) + "\""),
																" Closed=\"",
																drawPolyLine.CloseFigure,
																"\">"
															}));
														}
														foreach (object obj8 in drawPolyLine.PointArray)
														{
															Point point = (Point)obj8;
															sw.WriteLine(string.Concat(new object[]
															{
																"\t\t\t\t\t\t\t<Point X=\"",
																point.X,
																"\" Y=\"",
																point.Y,
																"\"/>"
															}));
														}
														foreach (object obj9 in drawPolyLine.DropArray)
														{
															DrawLine drawLine2 = (DrawLine)obj9;
															sw.WriteLine(string.Concat(new object[]
															{
																"\t\t\t\t\t\t\t<Drop X=\"",
																drawLine2.StartPoint.X,
																"\" Y=\"",
																drawLine2.StartPoint.Y,
																"\" Height=\"",
																drawLine2.Height,
																"\"/>"
															}));
														}
														if (drawPolyLine.CustomRenderingArray.Count > 0)
														{
															foreach (KeyValuePair<string, CustomRenderingProperties> keyValuePair in drawPolyLine.CustomRenderingArray)
															{
																CustomRenderingProperties value = keyValuePair.Value;
																string text7 = "\t\t\t\t\t\t\t<CustomRendering ";
																text7 = text7 + "ExtentionID=\"" + keyValuePair.Key + "\" ";
																obj4 = text7;
																text7 = string.Concat(new object[]
																{
																	obj4,
																	"Angle=\"",
																	value.Angle,
																	"\" "
																});
																obj4 = text7;
																text7 = string.Concat(new object[]
																{
																	obj4,
																	"OffsetX=\"",
																	value.OffsetX,
																	"\" "
																});
																obj4 = text7;
																text7 = string.Concat(new object[]
																{
																	obj4,
																	"OffsetY=\"",
																	value.OffsetY,
																	"\""
																});
																text7 += "/>";
																sw.WriteLine(text7);
															}
														}
														if (drawPolyLine.DeductionArray.Count > 0)
														{
															sw.WriteLine("\t\t\t\t\t\t\t<Deductions>");
															if (objectType == "Area")
															{
																using (IEnumerator enumerator2 = drawPolyLine.DeductionArray.GetEnumerator())
																{
																	while (enumerator2.MoveNext())
																	{
																		object obj10 = enumerator2.Current;
																		DrawPolyLine drawPolyLine2 = (DrawPolyLine)obj10;
																		sw.WriteLine("\t\t\t\t\t\t\t\t<Element>");
																		foreach (object obj11 in drawPolyLine2.PointArray)
																		{
																			Point point2 = (Point)obj11;
																			sw.WriteLine(string.Concat(new object[]
																			{
																				"\t\t\t\t\t\t\t\t\t<Point X=\"",
																				point2.X,
																				"\" Y=\"",
																				point2.Y,
																				"\"/>"
																			}));
																		}
																		sw.WriteLine("\t\t\t\t\t\t\t\t</Element>");
																	}
																	goto IL_2461;
																}
																goto IL_22AB;
															}
															goto IL_22AB;
															IL_2461:
															sw.WriteLine("\t\t\t\t\t\t\t</Deductions>");
															goto IL_246C;
															IL_22AB:
															foreach (object obj12 in drawPolyLine.DeductionArray)
															{
																DrawLine drawLine3 = (DrawLine)obj12;
																string text8 = "\t\t\t\t\t\t\t\t\t<Element ";
																text8 += ((drawLine3.Height == 0.0) ? string.Empty : ("Height=\"" + drawLine3.Height + "\" "));
																obj4 = text8;
																text8 = string.Concat(new object[]
																{
																	obj4,
																	"X1=\"",
																	drawLine3.StartPoint.X,
																	"\" "
																});
																obj4 = text8;
																text8 = string.Concat(new object[]
																{
																	obj4,
																	"Y1=\"",
																	drawLine3.StartPoint.Y,
																	"\" "
																});
																obj4 = text8;
																text8 = string.Concat(new object[]
																{
																	obj4,
																	"X2=\"",
																	drawLine3.EndPoint.X,
																	"\" "
																});
																obj4 = text8;
																text8 = string.Concat(new object[]
																{
																	obj4,
																	"Y2=\"",
																	drawLine3.EndPoint.Y,
																	"\" "
																});
																text8 += "/>";
																sw.WriteLine(text8);
															}
															goto IL_2461;
														}
														IL_246C:
														sw.WriteLine("\t\t\t\t\t\t</Element>");
													}
												}
											}
											else
											{
												DrawCounter drawCounter2 = (DrawCounter)drawObject2;
												string text9 = "\t\t\t\t\t\t<Element ";
												text9 += ((drawCounter2.Label == string.Empty) ? string.Empty : ("Label=\"" + Utilities.EscapeString(drawCounter2.Label) + "\" "));
												obj4 = text9;
												text9 = string.Concat(new object[]
												{
													obj4,
													"X=\"",
													drawCounter2.Location.X,
													"\" "
												});
												obj4 = text9;
												text9 = string.Concat(new object[]
												{
													obj4,
													"Y=\"",
													drawCounter2.Location.Y,
													"\" "
												});
												obj4 = text9;
												text9 = string.Concat(new object[]
												{
													obj4,
													"Width=\"",
													drawCounter2.Size.Width,
													"\" "
												});
												obj4 = text9;
												text9 = string.Concat(new object[]
												{
													obj4,
													"Height=\"",
													drawCounter2.Size.Height,
													"\""
												});
												text9 += "/>";
												sw.WriteLine(text9);
											}
										}
										else
										{
											DrawLine drawLine4 = (DrawLine)drawObject2;
											if (drawLine4.DeductionParentID == -1)
											{
												string text10 = "\t\t\t\t\t\t<Element ";
												text10 += ((drawLine4.Label == string.Empty) ? string.Empty : ("Label=\"" + Utilities.EscapeString(drawLine4.Label) + "\" "));
												obj4 = text10;
												text10 = string.Concat(new object[]
												{
													obj4,
													"X1=\"",
													drawLine4.StartPoint.X,
													"\" "
												});
												obj4 = text10;
												text10 = string.Concat(new object[]
												{
													obj4,
													"Y1=\"",
													drawLine4.StartPoint.Y,
													"\" "
												});
												obj4 = text10;
												text10 = string.Concat(new object[]
												{
													obj4,
													"X2=\"",
													drawLine4.EndPoint.X,
													"\" "
												});
												obj4 = text10;
												text10 = string.Concat(new object[]
												{
													obj4,
													"Y2=\"",
													drawLine4.EndPoint.Y,
													"\" "
												});
												text10 += "/>";
												sw.WriteLine(text10);
											}
										}
									}
								}
							}
							if (drawObject.IsPartOfGroup() || drawObject.Comment != string.Empty)
							{
								sw.WriteLine("\t\t\t\t\t</" + objectType + ">");
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
			foreach (object obj13 in this.EstimatingItems.EstimatingPrices)
			{
				EstimatingItemPrice estimatingItemPrice = (EstimatingItemPrice)((DictionaryEntry)obj13).Value;
				if (this.EstimatingPriceExists(estimatingItemPrice))
				{
					sw.WriteLine(string.Concat(new object[]
					{
						"\t\t<Price Key=\"",
						Utilities.EscapeString(estimatingItemPrice.Key.ToString()),
						"\" CostEach=\"",
						estimatingItemPrice.CostEach,
						"\" MarkupEach=\"",
						estimatingItemPrice.MarkupEach,
						"\" SystemType=\"",
						(int)estimatingItemPrice.SystemType,
						"\"/>"
					}));
				}
			}
			sw.WriteLine("\t</Prices>");
			sw.WriteLine("\t<Reports>");
			sw.WriteLine(string.Concat(new object[]
			{
				"\t\t<Report Name=\"Default\" Order=\"",
				Utilities.ConvertToInt(this.Report.Order),
				"\" ScaleType=\"",
				Utilities.ConvertToInt(this.Report.SystemType),
				"\" Precision=\"",
				Utilities.ConvertToInt(this.Report.Precision),
				"\">"
			}));
			sw.WriteLine("\t\t\t<Property Name=\"ShowProjectInfo\" Value=\"" + this.Report.TakeoffShowProjectInfo + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"ShowComments\" Value=\"" + this.Report.TakeoffShowComments + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"ShowInvisibleObjects\" Value=\"" + this.Report.TakeoffShowInvisibleObjects + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"ApplyFilter\" Value=\"" + this.Report.TakeoffApplyFilter + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"EstimatingShowProjectInfo\" Value=\"" + this.Report.EstimatingShowProjectInfo + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"EstimatingShowComments\" Value=\"" + this.Report.EstimatingShowComments + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"EstimatingShowInvisibleObjects\" Value=\"" + this.Report.EstimatingShowInvisibleObjects + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"EstimatingApplyFilter\" Value=\"" + this.Report.EstimatingApplyFilter + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"QuoteShowProjectInfo\" Value=\"" + this.Report.QuoteShowProjectInfo + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"QuoteShowComments\" Value=\"" + this.Report.QuoteShowComments + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"QuoteShowInvisibleObjects\" Value=\"" + this.Report.QuoteShowInvisibleObjects + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"QuoteApplyFilter\" Value=\"" + this.Report.QuoteApplyFilter + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"OrderByObjectsFilter\" Value=\"" + Utilities.EscapeString(this.Report.OrderByObjectsFilter) + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"OrderByPlansFilter\" Value=\"" + Utilities.EscapeString(this.Report.OrderByPlansFilter) + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"ReportSortBy\" Value=\"" + Utilities.ConvertToInt(this.Report.ReportSortBy) + "\"/>");
			sw.WriteLine("\t\t\t<Property Name=\"QuoteReportSortBy\" Value=\"" + Utilities.ConvertToInt(this.Report.QuoteReportSortBy) + "\"/>");
			sw.WriteLine("\t\t</Report>");
			sw.WriteLine("\t</Reports>");
			sw.WriteLine("</QuoterPlanSession>");
		}

		public bool Open(string fileName)
		{
			this.Clear();
			base.FullFileName = fileName;
			bool result;
			try
			{
				string shortFileName = Utilities.GetShortFileName(fileName);
				using (XmlTextReader xmlTextReader = new XmlTextReader(shortFileName))
				{
					this.ReadFromStream(xmlTextReader);
					xmlTextReader.Close();
					this.CleanUpGroups();
					this.SetDefaultValues();
					this.SetObjectsZorder();
					result = true;
				}
			}
			catch (Exception exception)
			{
				this.Clear();
				Utilities.DisplayFileOpenError(fileName, exception);
				result = false;
			}
			return result;
		}

		private void SetEstimatingItemPrice(XmlTextReader reader)
		{
			string stringAttribute = Utilities.GetStringAttribute(reader, "Key", "");
			EstimatingItemPrice value = new EstimatingItemPrice(stringAttribute, Utilities.GetDoubleAttribute(reader, "CostEach", 0.0), Utilities.GetDoubleAttribute(reader, "MarkupEach", 0.0), (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "SystemType", 0));
			this.EstimatingItems.EstimatingPrices.Add(stringAttribute, value);
		}

		private void ReadFromStream(XmlTextReader reader)
		{
			int num = 150;
			int num2 = 0;
			string name = "";
			string text = "";
			Utilities.NumberDecimalSeparator();
			Project.ParserContext parserContext = Project.ParserContext.UndefinedContext;
			DrawObject drawObject = null;
			DrawObject drawObject2 = null;
			DrawPolyLine drawPolyLine = null;
			DrawPolyLine drawPolyLine2 = null;
			DrawObjectGroup drawObjectGroup = null;
			Preset preset = null;
			Plan plan = null;
			while (reader.Read())
			{
				XmlNodeType nodeType = reader.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				{
					DrawObject drawObject3 = null;
					text = reader.Name.ToUpper();
					string key;
					switch (key = text)
					{
					case "PROJECT":
						parserContext = Project.ParserContext.ProjectContext;
						base.Name = Utilities.GetStringAttribute(reader, "Name", "");
						break;
					case "WORKSPACE":
						parserContext = Project.ParserContext.WorkpaceContext;
						break;
					case "ACTIVEPLAN":
						name = Utilities.GetStringAttribute(reader, "Name", "");
						break;
					case "PLANS":
						parserContext = Project.ParserContext.PlansContext;
						break;
					case "GROUP":
						drawObjectGroup = new DrawObjectGroup(Utilities.GetIntegerAttribute(reader, "GroupID", -1), Utilities.GetStringAttribute(reader, "TemplateID", ""));
						this.Groups.Add(drawObjectGroup);
						break;
					case "PLAN":
						switch (parserContext)
						{
						case Project.ParserContext.WorkpaceContext:
							this.Workspace.RecentPlans.Add(new Variable(Utilities.GetStringAttribute(reader, "Name", ""), null));
							break;
						case Project.ParserContext.PlansContext:
						{
							string text2 = Utilities.GetStringAttribute(reader, "FileName", "");
							if (!Path.IsPathRooted(text2))
							{
								if (Utilities.FileExists(Path.Combine(base.FolderName, text2)))
								{
									text2 = Path.Combine(base.FolderName, text2);
								}
								else
								{
									text2 = Path.Combine(Utilities.GetProjectPlansFolder(base.FolderName), text2);
								}
							}
							plan = this.InsertPlan(reader.GetAttribute("Name"), text2, Utilities.GetBoolAttribute(reader, "Pinned", false), Utilities.GetIntegerAttribute(reader, "Brightness", 0), Utilities.GetIntegerAttribute(reader, "Contrast", 0));
							this.drawArea.ActivePlan = plan;
							break;
						}
						}
						break;
					case "THUMBNAIL":
						if (plan != null)
						{
							plan.Thumbnail.FileName = Utilities.GetStringAttribute(reader, "FileName", "");
						}
						break;
					case "SCALE":
						if (plan != null)
						{
							float scale = (float)Utilities.GetDoubleAttribute(reader, "Value", 0.0);
							UnitScale.UnitSystem unitSystem = UnitScale.CastUnitSystem(Utilities.GetIntegerAttribute(reader, "Type", 2));
							UnitScale.UnitPrecision precision = UnitScale.CastUnitPrecision(Utilities.GetIntegerAttribute(reader, "Precision", 0));
							bool boolAttribute = Utilities.GetBoolAttribute(reader, "SetManually", false);
							plan.UnitScale.SetScale(scale, unitSystem, precision, boolAttribute);
							bool boolAttribute2 = Utilities.GetBoolAttribute(reader, "Engineering", false);
							plan.UnitScale.Engineering = (boolAttribute2 && unitSystem == UnitScale.UnitSystem.imperial);
						}
						break;
					case "BOOKMARK":
						if (plan != null)
						{
							string stringAttribute = Utilities.GetStringAttribute(reader, "Name", "");
							if (stringAttribute == "Default")
							{
								plan.DefaultBookmark.LayerIndex = Utilities.GetIntegerAttribute(reader, "LayerIndex", 0);
								plan.DefaultBookmark.Zoom = Utilities.GetIntegerAttribute(reader, "Zoom", 100);
								plan.DefaultBookmark.Origin = new Point(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0));
							}
						}
						break;
					case "LAYER":
						if (plan != null)
						{
							num2 = plan.Layers.CreateNewLayer(Utilities.GetStringAttribute(reader, "Name", ""), 150);
							num = Utilities.GetIntegerAttribute(reader, "Opacity", 150);
							num = ((num < 25 || num > 225) ? 150 : num);
							plan.Layers[num2].Opacity = num;
							plan.Layers[num2].Visible = Utilities.GetBoolAttribute(reader, "Visible", true);
						}
						break;
					case "LEGEND":
						drawObject3 = new DrawLegend(plan, this.ExtensionsSupport, Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetStringAttribute(reader, "Name", ""), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), Utilities.GetIntegerAttribute(reader, "PenWidth", 6), Utilities.GetIntegerAttribute(reader, "FontSize", DrawLegend.DefaultFontSize), Utilities.GetIntegerAttribute(reader, "MaxRows", DrawLegend.DefaultMaxRows))
						{
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						drawObject2 = drawObject3;
						break;
					case "RECTANGLE":
						drawObject3 = new DrawRectangle(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetIntegerAttribute(reader, "Width", 10), Utilities.GetIntegerAttribute(reader, "Height", 10), new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), num, true, Utilities.GetIntegerAttribute(reader, "PenWidth", 3))
						{
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						drawObject2 = drawObject3;
						break;
					case "ANGLE":
						drawObject3 = new DrawAngle(Utilities.GetIntegerAttribute(reader, "X1", 0), Utilities.GetIntegerAttribute(reader, "Y1", 0), Utilities.GetIntegerAttribute(reader, "X2", 0), Utilities.GetIntegerAttribute(reader, "Y2", 0), Utilities.GetIntegerAttribute(reader, "X3", 0), Utilities.GetIntegerAttribute(reader, "Y3", 0), new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), num, Utilities.GetIntegerAttribute(reader, "PenWidth", 3), (DrawAngle.AngleTypeEnum)Utilities.GetIntegerAttribute(reader, "AngleType", 0))
						{
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						drawObject2 = drawObject3;
						break;
					case "NOTE":
						drawObject3 = new DrawNote(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetIntegerAttribute(reader, "Width", 10), Utilities.GetIntegerAttribute(reader, "Height", 10), Utilities.GetIntegerAttribute(reader, "AnchorX", 0), Utilities.GetIntegerAttribute(reader, "AnchorY", 0), Utilities.GetStringAttribute(reader, "Name", ""), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), num, true, Utilities.GetIntegerAttribute(reader, "PenWidth", 3), Utilities.GetIntegerAttribute(reader, "FontSize", 32))
						{
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						drawObject2 = drawObject3;
						break;
					case "LINE":
						drawObject = new DrawLine(0, 0, 0, 0, new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), "", true, ColorTranslator.FromHtml(reader.GetAttribute("Color")), num, Utilities.GetIntegerAttribute(reader, "PenWidth", 4))
						{
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						drawObject.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0.0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 0), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
						break;
					case "AREA":
					{
						string stringAttribute2 = Utilities.GetStringAttribute(reader, "Pattern", string.Empty);
						HatchStylePickerCombo.HatchStylePickerEnum pattern = (HatchStylePickerCombo.HatchStylePickerEnum)((stringAttribute2 == string.Empty) ? -1 : Utilities.GetIntegerAttribute(reader, "Pattern", -1));
						drawObject = new DrawPolyLine(new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), pattern, num, Utilities.GetIntegerAttribute(reader, "PenWidth", 4))
						{
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						drawObject.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0.0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), SlopeFactor.HipValleyEnum.hipValleyUnavailable);
						break;
					}
					case "PERIMETER":
						drawObject = new DrawPolyLine(new PointF(0f, 0f), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), num, Utilities.GetIntegerAttribute(reader, "PenWidth", 4))
						{
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						drawObject.SetSlopeFactor(Utilities.GetDoubleAttribute(reader, "Slope", 0.0), (SlopeFactor.SlopeTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeType", 0), (SlopeFactor.SlopeApplyTypeEnum)Utilities.GetIntegerAttribute(reader, "SlopeApply", 1), (SlopeFactor.HipValleyEnum)Utilities.GetIntegerAttribute(reader, "HipValley", 0));
						break;
					case "POINT":
						if (drawPolyLine != null)
						{
							drawPolyLine.AddPoint(new Point(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0)));
						}
						break;
					case "DROP":
						if (drawPolyLine != null)
						{
							drawPolyLine.CreateDrop(new Point(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0)), Utilities.GetDoubleAttribute(reader, "Height", 0.0));
						}
						break;
					case "CUSTOMRENDERING":
						if (drawPolyLine != null)
						{
							string stringAttribute3 = Utilities.GetStringAttribute(reader, "ExtentionID", "");
							int integerAttribute = Utilities.GetIntegerAttribute(reader, "Angle", 0);
							int integerAttribute2 = Utilities.GetIntegerAttribute(reader, "OffsetX", 0);
							int integerAttribute3 = Utilities.GetIntegerAttribute(reader, "OffsetY", 0);
							drawPolyLine.SetCustomRenderingProperties(stringAttribute3, new CustomRenderingProperties(integerAttribute, integerAttribute2, integerAttribute3));
						}
						break;
					case "COUNTER":
						drawObject = new DrawCounter(0, 0, 0, 0, Utilities.GetIntegerAttribute(reader, "DefaultSize", 80), (DrawCounter.CounterShapeTypeEnum)Utilities.GetIntegerAttribute(reader, "Shape", 0), Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetIntegerAttribute(reader, "GroupID", -1), reader.GetAttribute("Text"), "", ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "Color", "0")), ColorTranslator.FromHtml(Utilities.GetStringAttribute(reader, "FillColor", "0")), num, true, Utilities.GetIntegerAttribute(reader, "PenWidth", 2))
						{
							ImageFileName = Utilities.GetStringAttribute(reader, "FileName", ""),
							ShowMeasure = Utilities.GetBoolAttribute(reader, "ShowMeasure", true),
							Visible = Utilities.GetBoolAttribute(reader, "Visible", true)
						};
						break;
					case "EXTENSION":
						if (drawObjectGroup != null)
						{
							string text3 = Utilities.GetStringAttribute(reader, "Id", "");
							text3 = ((text3 == "") ? Guid.NewGuid().ToString() : text3);
							string stringAttribute4 = Utilities.GetStringAttribute(reader, "Category", "");
							string stringAttribute5 = Utilities.GetStringAttribute(reader, "Name", "");
							string text4 = Utilities.GetStringAttribute(reader, "DisplayName", "");
							if (text4 == "")
							{
								ExtensionCategory extensionCategory = null;
								Extension extension = this.ExtensionsSupport.FindExtension(ref extensionCategory, stringAttribute5);
								if (extension != null)
								{
									text4 = extension.Caption;
								}
							}
							if (text4 == "")
							{
								text4 = stringAttribute5;
							}
							text4 = drawObjectGroup.Presets.GetFreeDisplayName(text4, "");
							preset = new Preset(text3, text4, stringAttribute4, stringAttribute5, (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2));
							preset.SetCustomRendering();
							drawObjectGroup.Presets.Add(preset);
						}
						break;
					case "ESTIMATINGITEM":
						if (parserContext == Project.ParserContext.EstimatingItems)
						{
							this.SetEstimatingItemPrice(reader);
						}
						else if (drawObjectGroup != null)
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
								cestimatingItem.InternalKey = Utilities.GetStringAttribute(reader, "InternalKey", "");
								cestimatingItem.Tag = cestimatingItem;
								drawObjectGroup.EstimatingItems.Add(cestimatingItem);
							}
						}
						break;
					case "COFFICEPRODUCT":
						if (drawObjectGroup != null)
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
								drawObjectGroup.COfficeProducts.Add(cestimatingItem2);
							}
						}
						break;
					case "CHOICE":
						if (preset != null)
						{
							preset.Choices.Add(new PresetChoice(Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetStringAttribute(reader, "Element", "")));
						}
						break;
					case "FIELD":
						if (preset != null)
						{
							preset.Fields.Add(new PresetField(Utilities.GetStringAttribute(reader, "Name", ""), Utilities.GetStringAttribute(reader, "Value", "")));
						}
						break;
					case "ELEMENT":
					{
						string objectType;
						if (drawObject != null && (objectType = drawObject.ObjectType) != null)
						{
							if (!(objectType == "Line"))
							{
								if (!(objectType == "Area"))
								{
									if (!(objectType == "Perimeter"))
									{
										if (objectType == "Counter")
										{
											drawObject3 = new DrawCounter(Utilities.GetIntegerAttribute(reader, "X", 0), Utilities.GetIntegerAttribute(reader, "Y", 0), Utilities.GetIntegerAttribute(reader, "Width", 10), Utilities.GetIntegerAttribute(reader, "Height", 10), ((DrawCounter)drawObject).DefaultSize, ((DrawCounter)drawObject).Shape, drawObject.Name, drawObject.GroupID, drawObject.Text, drawObject.Comment, drawObject.Color, drawObject.FillColor, num, true, drawObject.PenWidth)
											{
												ImageFileName = ((DrawCounter)drawObject).ImageFileName,
												Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
												ShowMeasure = drawObject.ShowMeasure,
												Visible = drawObject.Visible
											};
											if (((DrawCounter)drawObject3).Shape == DrawCounter.CounterShapeTypeEnum.CounterShapeCustomImage)
											{
												((DrawCounter)drawObject3).LoadCustomImage();
											}
										}
									}
									else if (drawPolyLine2 == null)
									{
										drawObject3 = new DrawPolyLine(new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, drawObject.Color, num, drawObject.PenWidth)
										{
											Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
											CloseFigure = Utilities.GetBoolAttribute(reader, "Closed", true),
											ShowMeasure = drawObject.ShowMeasure,
											Visible = drawObject.Visible
										};
										drawObject3.SetSlopeFactor(drawObject.SlopeFactor);
										drawPolyLine = (DrawPolyLine)drawObject3;
									}
									else
									{
										drawObject3 = new DrawLine(Utilities.GetIntegerAttribute(reader, "X1", 0), Utilities.GetIntegerAttribute(reader, "Y1", 0), Utilities.GetIntegerAttribute(reader, "X2", 0), Utilities.GetIntegerAttribute(reader, "Y2", 0), new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, true, Color.Black, num, drawObject.PenWidth)
										{
											Height = Utilities.GetDoubleAttribute(reader, "Height", 0.0),
											ShowMeasure = drawObject.ShowMeasure,
											Visible = false
										};
									}
								}
								else
								{
									drawObject3 = new DrawPolyLine(new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, drawObject.Color, drawObject.FillColor, ((DrawPolyLine)drawObject).Pattern, num, drawObject.PenWidth)
									{
										Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
										ShowMeasure = drawObject.ShowMeasure
									};
									if (drawPolyLine2 == null)
									{
										drawObject3.SetSlopeFactor(drawObject.SlopeFactor);
										drawObject3.Visible = drawObject.Visible;
									}
									else
									{
										drawObject3.Color = Color.LightGray;
										drawObject3.FillColor = Color.LightGray;
										drawObject3.Visible = false;
									}
									drawPolyLine = (DrawPolyLine)drawObject3;
								}
							}
							else
							{
								drawObject3 = new DrawLine(Utilities.GetIntegerAttribute(reader, "X1", 0), Utilities.GetIntegerAttribute(reader, "Y1", 0), Utilities.GetIntegerAttribute(reader, "X2", 0), Utilities.GetIntegerAttribute(reader, "Y2", 0), new PointF(0f, 0f), drawObject.Name, drawObject.GroupID, drawObject.Comment, true, drawObject.Color, num, drawObject.PenWidth)
								{
									Label = Utilities.GetStringAttribute(reader, "Label", string.Empty),
									ShowMeasure = drawObject.ShowMeasure,
									Visible = drawObject.Visible
								};
								drawObject3.SetSlopeFactor(drawObject.SlopeFactor);
							}
						}
						break;
					}
					case "DEDUCTIONS":
						drawPolyLine2 = drawPolyLine;
						break;
					case "PRICES":
					case "ESTIMATINGITEMS":
						parserContext = Project.ParserContext.EstimatingItems;
						break;
					case "PRICE":
						this.SetEstimatingItemPrice(reader);
						break;
					case "REPORTS":
						parserContext = Project.ParserContext.ReportsContext;
						break;
					case "REPORT":
						this.Report.Order = (Report.ReportOrderEnum)Utilities.GetIntegerAttribute(reader, "Order", 0);
						this.Report.SystemType = (UnitScale.UnitSystem)Utilities.GetIntegerAttribute(reader, "ScaleType", 2);
						this.Report.Precision = (UnitScale.UnitPrecision)Utilities.GetIntegerAttribute(reader, "Precision", 0);
						break;
					case "PROPERTY":
					{
						string stringAttribute6 = Utilities.GetStringAttribute(reader, "Name", "");
						Project.ParserContext parserContext2 = parserContext;
						string key2;
						if (parserContext2 == Project.ParserContext.ReportsContext && (key2 = stringAttribute6) != null)
						{
							if (<PrivateImplementationDetails>{E0438CCF-7425-4F3B-BA1B-66DC2567D6E9}.$$method0x600117d-2 == null)
							{
								<PrivateImplementationDetails>{E0438CCF-7425-4F3B-BA1B-66DC2567D6E9}.$$method0x600117d-2 = new Dictionary<string, int>(16)
								{
									{
										"ShowProjectInfo",
										0
									},
									{
										"ShowComments",
										1
									},
									{
										"ShowInvisibleObjects",
										2
									},
									{
										"ApplyFilter",
										3
									},
									{
										"EstimatingShowProjectInfo",
										4
									},
									{
										"EstimatingShowComments",
										5
									},
									{
										"EstimatingShowInvisibleObjects",
										6
									},
									{
										"EstimatingApplyFilter",
										7
									},
									{
										"QuoteShowProjectInfo",
										8
									},
									{
										"QuoteShowComments",
										9
									},
									{
										"QuoteShowInvisibleObjects",
										10
									},
									{
										"QuoteApplyFilter",
										11
									},
									{
										"OrderByObjectsFilter",
										12
									},
									{
										"OrderByPlansFilter",
										13
									},
									{
										"ReportSortBy",
										14
									},
									{
										"QuoteReportSortBy",
										15
									}
								};
							}
							int num4;
							if (<PrivateImplementationDetails>{E0438CCF-7425-4F3B-BA1B-66DC2567D6E9}.$$method0x600117d-2.TryGetValue(key2, out num4))
							{
								switch (num4)
								{
								case 0:
									this.Report.TakeoffShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 1:
									this.Report.TakeoffShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 2:
									this.Report.TakeoffShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 3:
									this.Report.TakeoffApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 4:
									this.Report.EstimatingShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 5:
									this.Report.EstimatingShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 6:
									this.Report.EstimatingShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 7:
									this.Report.EstimatingApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 8:
									this.Report.QuoteShowProjectInfo = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 9:
									this.Report.QuoteShowComments = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 10:
									this.Report.QuoteShowInvisibleObjects = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 11:
									this.Report.QuoteApplyFilter = Utilities.GetBoolAttribute(reader, "Value", true);
									break;
								case 12:
									this.Report.OrderByObjectsFilter = Utilities.GetStringAttribute(reader, "Value", "");
									break;
								case 13:
									this.Report.OrderByPlansFilter = Utilities.GetStringAttribute(reader, "Value", "");
									break;
								case 14:
									this.Report.ReportSortBy = (Report.ReportSortByEnum)Utilities.GetIntegerAttribute(reader, "Value", 0);
									break;
								case 15:
									this.Report.QuoteReportSortBy = (Report.QuoteReportSortByEnum)Utilities.GetIntegerAttribute(reader, "Value", 0);
									if (this.Report.QuoteReportSortBy != Report.QuoteReportSortByEnum.QuoteReportSortBySections && this.Report.QuoteReportSortBy != Report.QuoteReportSortByEnum.QuoteReportSortByTypes && this.Report.QuoteReportSortBy != Report.QuoteReportSortByEnum.QuoteReportSortByList)
									{
										this.Report.QuoteReportSortBy = Report.QuoteReportSortByEnum.QuoteReportSortBySections;
									}
									break;
								}
							}
						}
						break;
					}
					}
					if (drawObject3 != null)
					{
						this.drawArea.InsertObject(drawObject3, num2, false, false);
						if (drawPolyLine2 != null)
						{
							drawPolyLine2.CreateDeduction(drawObject3);
						}
					}
					break;
				}
				case XmlNodeType.Attribute:
					break;
				case XmlNodeType.Text:
				{
					string key3;
					switch (key3 = text)
					{
					case "COMMENT":
					{
						string comment = reader.Value.Trim().Replace("`", "\r\n");
						if (drawObject != null || drawObject2 != null)
						{
							if (drawObject != null)
							{
								drawObject.Comment = comment;
							}
							else
							{
								drawObject2.Comment = comment;
							}
							drawObject2 = null;
						}
						else
						{
							switch (parserContext)
							{
							case Project.ParserContext.ProjectContext:
								this.Comment = comment;
								break;
							case Project.ParserContext.PlansContext:
								plan.Comment = comment;
								break;
							}
						}
						break;
					}
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
					break;
				}
				default:
					if (nodeType == XmlNodeType.EndElement)
					{
						string a = reader.Name.ToUpper();
						if (a == "DEDUCTIONS")
						{
							drawPolyLine2 = null;
						}
						if (a == "EXTENSION")
						{
							preset = null;
						}
						if (a != "COMMENT" && a != "POINT" && a != "CUSTOMRENDERING" && a != "PROPERTY")
						{
							drawPolyLine = null;
						}
						if (a != "COMMENT" && a != "ELEMENT" && a != "POINT" && a != "DEDUCTIONS" && a != "EXTENSION" && a != "CHOICE" && a != "FIELD" && a != "CUSTOMRENDERING" && a != "PROPERTY")
						{
							drawObject = null;
						}
						if (a == "GROUP")
						{
							drawObjectGroup = null;
						}
					}
					break;
				}
			}
			this.Workspace.ActivePlan = this.Plans.FindPlan(name);
		}

		public string DisplayName
		{
			get
			{
				if (base.FileName != "")
				{
					return Path.GetFileNameWithoutExtension(base.FileName);
				}
				return base.Name + " (" + Utilities.GetDateString(this.CreationDate, Utilities.GetCurrentValidUICultureShort()).Replace(",", "").Replace(" ", "-") + ")";
			}
		}

		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		public string ContactName
		{
			[CompilerGenerated]
			get
			{
				return this.<ContactName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContactName>k__BackingField = value;
			}
		}

		public string ContactInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<ContactInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContactInfo>k__BackingField = value;
			}
		}

		public string JobNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<JobNumber>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<JobNumber>k__BackingField = value;
			}
		}

		public string Comment
		{
			[CompilerGenerated]
			get
			{
				return this.<Comment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Comment>k__BackingField = value;
			}
		}

		public string CreationDate
		{
			[CompilerGenerated]
			get
			{
				return this.<CreationDate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CreationDate>k__BackingField = value;
			}
		}

		public string LastModified
		{
			[CompilerGenerated]
			get
			{
				return this.<LastModified>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LastModified>k__BackingField = value;
			}
		}

		public bool DisplayResultsForAllPlans
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayResultsForAllPlans>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayResultsForAllPlans>k__BackingField = value;
			}
		}

		public int ObjectCounter
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectCounter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ObjectCounter>k__BackingField = value;
			}
		}

		public int GroupCounter
		{
			[CompilerGenerated]
			get
			{
				return this.<GroupCounter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GroupCounter>k__BackingField = value;
			}
		}

		public string CreationParentFolder
		{
			[CompilerGenerated]
			get
			{
				return this.<CreationParentFolder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CreationParentFolder>k__BackingField = value;
			}
		}

		public Plans Plans
		{
			[CompilerGenerated]
			get
			{
				return this.<Plans>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Plans>k__BackingField = value;
			}
		}

		public DrawObjectGroups Groups
		{
			[CompilerGenerated]
			get
			{
				return this.<Groups>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Groups>k__BackingField = value;
			}
		}

		public Workspace Workspace
		{
			[CompilerGenerated]
			get
			{
				return this.<Workspace>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Workspace>k__BackingField = value;
			}
		}

		public Report Report
		{
			[CompilerGenerated]
			get
			{
				return this.<Report>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Report>k__BackingField = value;
			}
		}

		public ExtensionsSupport ExtensionsSupport
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtensionsSupport>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ExtensionsSupport>k__BackingField = value;
			}
		}

		public EstimatingItems EstimatingItems
		{
			[CompilerGenerated]
			get
			{
				return this.<EstimatingItems>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EstimatingItems>k__BackingField = value;
			}
		}

		public DBManagement DBManagement
		{
			[CompilerGenerated]
			get
			{
				return this.<DBManagement>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DBManagement>k__BackingField = value;
			}
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

		private void CleanUpGroups()
		{
			for (int i = this.Groups.Count - 1; i >= 0; i--)
			{
				DrawObjectGroup drawObjectGroup = this.Groups[i];
				DrawObject drawObject = this.drawArea.FindObjectFromGroupID(this, drawObjectGroup.ID);
				if (drawObject == null)
				{
					this.Groups.RemoveAt(i);
				}
				else
				{
					drawObjectGroup.Name = drawObject.Name;
					drawObjectGroup.ObjectType = drawObject.ObjectType;
				}
			}
		}

		private void SetDefaultValues()
		{
			base.Name = ((base.Name == "") ? base.FileName.Substring(0, base.FileName.Length - 4) : base.Name);
			this.CreationDate = ((this.CreationDate == "") ? Utilities.FormatDate(DateTime.Now) : this.CreationDate);
			this.LastModified = ((this.LastModified == "") ? Utilities.FormatDate(DateTime.Now) : this.LastModified);
		}

		private void SetObjectsZorder()
		{
			this.ObjectCounter = 0;
			foreach (object obj in this.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					layer.DrawingObjects.Collection.Reverse();
				}
				foreach (object obj3 in plan.Layers.Collection)
				{
					Layer layer2 = (Layer)obj3;
					for (int i = layer2.DrawingObjects.Count - 1; i >= 0; i--)
					{
						DrawObject drawObject = layer2.DrawingObjects[i];
						drawObject.ID = ++this.ObjectCounter;
						if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
						{
							DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
							foreach (object obj4 in drawPolyLine.DeductionArray)
							{
								DrawLine drawLine = (DrawLine)obj4;
								drawLine.DeductionParentID = drawObject.ID;
							}
						}
					}
				}
			}
			this.GroupCounter = GroupUtilities.GetFreeGroupID(this) - 1;
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

		public void Initialize(DrawingArea drawArea, ExtensionsSupport extensionsSupport, DBManagement dbManagement)
		{
			this.drawArea = drawArea;
			this.ExtensionsSupport = extensionsSupport;
			this.DBManagement = dbManagement;
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
			base.Dirty = (flag || dirty);
			return flag;
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

		public void FlagDeletedGroups()
		{
			for (int i = this.Groups.Count - 1; i >= 0; i--)
			{
				DrawObjectGroup drawObjectGroup = this.Groups[i];
				DrawObject drawObject = this.drawArea.FindObjectFromGroupID(this, drawObjectGroup.ID);
				drawObjectGroup.Deleted = (drawObject == null);
			}
		}

		private DrawingArea drawArea;

		private string saveFileName = string.Empty;

		[CompilerGenerated]
		private string <Description>k__BackingField;

		[CompilerGenerated]
		private string <ContactName>k__BackingField;

		[CompilerGenerated]
		private string <ContactInfo>k__BackingField;

		[CompilerGenerated]
		private string <JobNumber>k__BackingField;

		[CompilerGenerated]
		private string <Comment>k__BackingField;

		[CompilerGenerated]
		private string <CreationDate>k__BackingField;

		[CompilerGenerated]
		private string <LastModified>k__BackingField;

		[CompilerGenerated]
		private bool <DisplayResultsForAllPlans>k__BackingField;

		[CompilerGenerated]
		private int <ObjectCounter>k__BackingField;

		[CompilerGenerated]
		private int <GroupCounter>k__BackingField;

		[CompilerGenerated]
		private string <CreationParentFolder>k__BackingField;

		[CompilerGenerated]
		private Plans <Plans>k__BackingField;

		[CompilerGenerated]
		private DrawObjectGroups <Groups>k__BackingField;

		[CompilerGenerated]
		private Workspace <Workspace>k__BackingField;

		[CompilerGenerated]
		private Report <Report>k__BackingField;

		[CompilerGenerated]
		private ExtensionsSupport <ExtensionsSupport>k__BackingField;

		[CompilerGenerated]
		private EstimatingItems <EstimatingItems>k__BackingField;

		[CompilerGenerated]
		private DBManagement <DBManagement>k__BackingField;

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
				int result;
				try
				{
					Plan plan = x as Plan;
					Plan plan2 = y as Plan;
					result = plan.ReorderIndex.CompareTo(plan2.ReorderIndex);
				}
				catch (Exception exception)
				{
					Utilities.DisplaySystemError(exception);
					result = -1;
				}
				return result;
			}

			public PlanSorter()
			{
			}
		}
	}
}
