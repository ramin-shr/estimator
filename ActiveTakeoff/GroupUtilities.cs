using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace QuoterPlan
{
	public static class GroupUtilities
	{
		public static int GetFreeGroupID(Project project)
		{
			int num = 0;
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (drawObject.GroupID > num)
						{
							num = drawObject.GroupID;
						}
					}
				}
			}
			return num + 1;
		}

		public static List<string> GetObjectLabels(Plan plan, DrawObject drawObject)
		{
			string objectType = drawObject.ObjectType;
			List<string> list = new List<string>();
			if (objectType != "Line" && objectType != "Area" && objectType != "Perimeter" && objectType != "Counter")
			{
				return list;
			}
			list.Add(string.Empty);
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject2 = (DrawObject)obj2;
					if (drawObject2.GroupID == drawObject.GroupID && drawObject2.DeductionParentID == -1 && !list.Contains(drawObject2.Label))
					{
						list.Add(drawObject2.Label);
					}
				}
			}
			list.Sort();
			return list;
		}

		public static List<string> GetObjectLabels(Project project, DrawObject drawObject, Filter filter)
		{
			string objectType = drawObject.ObjectType;
			List<string> list = new List<string>();
			if (objectType != "Line" && objectType != "Area" && objectType != "Perimeter" && objectType != "Counter")
			{
				return list;
			}
			list.Add(string.Empty);
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				bool flag = false;
				if (filter != null)
				{
					flag = filter.QueryFilter(plan.Name, "", -1, false);
					if (!flag)
					{
						flag = filter.QueryFilter(plan.Name, "", drawObject.GroupID, true);
					}
				}
				if (!flag)
				{
					foreach (object obj2 in plan.Layers.Collection)
					{
						Layer layer = (Layer)obj2;
						foreach (object obj3 in layer.DrawingObjects.Collection)
						{
							DrawObject drawObject2 = (DrawObject)obj3;
							if (drawObject2.GroupID == drawObject.GroupID && drawObject2.DeductionParentID == -1 && !list.Contains(drawObject2.Label))
							{
								list.Add(drawObject2.Label);
							}
						}
					}
				}
			}
			list.Sort();
			return list;
		}

		public static bool LabelExistsInLayer(Project project, DrawObject drawObject, string layerName, string label)
		{
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					if (layer.Name == layerName)
					{
						foreach (object obj3 in layer.DrawingObjects.Collection)
						{
							DrawObject drawObject2 = (DrawObject)obj3;
							if (drawObject2.GroupID == drawObject.GroupID && drawObject2.DeductionParentID == -1 && drawObject2.Label == label)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		private static GroupStats ComputeGroupStats(Plan plan, Layer layer, DrawObject drawObject, UnitScale.UnitSystem systemType, bool allLabels = true, string objectLabel = "")
		{
			string objectType = drawObject.ObjectType;
			GroupStats groupStats = new GroupStats(objectType);
			if (objectType != "Line" && objectType != "Area" && objectType != "Perimeter" && objectType != "Counter")
			{
				return groupStats;
			}
			foreach (object obj in layer.DrawingObjects.Collection)
			{
				DrawObject drawObject2 = (DrawObject)obj;
				string a;
				if (drawObject2.GroupID == drawObject.GroupID && drawObject2.DeductionParentID == -1 && (objectLabel == string.Empty || drawObject2.Label == objectLabel) && (a = objectType) != null)
				{
					if (!(a == "Counter"))
					{
						if (!(a == "Line"))
						{
							if (!(a == "Area"))
							{
								if (a == "Perimeter")
								{
									groupStats += ((DrawPolyLine)drawObject2).Stats;
									foreach (object obj2 in ((DrawPolyLine)drawObject2).DeductionArray)
									{
										DrawObject drawObject3 = (DrawObject)obj2;
										if (((DrawLine)drawObject3).Height > 0.0)
										{
											double num = (double)((DrawLine)drawObject3).Distance2D(true);
											num = plan.UnitScale.ToLength((int)num);
											num = ((systemType == plan.UnitScale.ScaleSystemType) ? num : ((systemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num) : UnitScale.FromFeetToMeters(num)));
											double num2 = ((DrawLine)drawObject3).Height;
											num2 = ((systemType == plan.UnitScale.ScaleSystemType) ? num2 : ((systemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num2) : UnitScale.FromFeetToMeters(num2)));
											groupStats.DeductionArea += num * num2;
										}
									}
									foreach (object obj3 in ((DrawPolyLine)drawObject2).DropArray)
									{
										DrawLine drawLine = (DrawLine)obj3;
										if (drawLine.Height > 0.0)
										{
											double num3 = drawLine.Height;
											num3 = ((systemType == plan.UnitScale.ScaleSystemType) ? num3 : ((systemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num3) : UnitScale.FromFeetToMeters(num3)));
											groupStats.DropLength += num3;
										}
									}
									groupStats.DropsCount += ((DrawPolyLine)drawObject2).DropArray.Count;
								}
							}
							else
							{
								if (((DrawPolyLine)drawObject2).Group.Presets.HasCustomRendering())
								{
									((DrawPolyLine)drawObject2).ComputeCustomRendering(plan);
								}
								groupStats += ((DrawPolyLine)drawObject2).Stats;
							}
						}
						else
						{
							groupStats.GroupCount++;
							groupStats.Perimeter += (double)((DrawLine)drawObject2).Distance2D(true);
						}
					}
					else
					{
						groupStats.GroupCount++;
					}
				}
			}
			return groupStats;
		}

		public static GroupStats ComputeGroupStats(Plan plan, DrawObject drawObject, UnitScale.UnitSystem systemType, bool allLabels = true, string objectLabel = "")
		{
			string objectType = drawObject.ObjectType;
			GroupStats groupStats = new GroupStats(objectType);
			if (objectType != "Line" && objectType != "Area" && objectType != "Perimeter" && objectType != "Counter")
			{
				return groupStats;
			}
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				groupStats += GroupUtilities.ComputeGroupStats(plan, layer, drawObject, systemType, allLabels, objectLabel);
			}
			double deductionArea = groupStats.DeductionArea;
			groupStats.DeductionArea = ((objectType == "Perimeter") ? 0.0 : groupStats.DeductionArea);
			plan.UnitScale.ConvertStatsToUnitSystem(groupStats, systemType);
			groupStats.DeductionArea = ((objectType == "Perimeter") ? deductionArea : groupStats.DeductionArea);
			return groupStats;
		}

		public static GroupStats ComputeGroupStats(Project project, string layerName, DrawObject drawObject, Filter filter, UnitScale.UnitSystem systemType, bool allLabels = true, string objectLabel = "")
		{
			string objectType = drawObject.ObjectType;
			GroupStats groupStats = new GroupStats(objectType);
			if (objectType != "Line" && objectType != "Area" && objectType != "Perimeter" && objectType != "Counter")
			{
				return groupStats;
			}
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					if (layer.Name == layerName)
					{
						bool flag = false;
						if (filter != null)
						{
							flag = filter.QueryFilter(plan.Name, "", -1, false);
							if (!flag)
							{
								flag = filter.QueryFilter(plan.Name, "", drawObject.GroupID, true);
							}
						}
						if (!flag)
						{
							GroupStats groupStats2 = GroupUtilities.ComputeGroupStats(plan, layer, drawObject, systemType, allLabels, objectLabel);
							double deductionArea = groupStats2.DeductionArea;
							groupStats2.DeductionArea = ((objectType == "Perimeter") ? 0.0 : groupStats2.DeductionArea);
							plan.UnitScale.ConvertStatsToUnitSystem(groupStats2, systemType);
							groupStats2.DeductionArea = ((objectType == "Perimeter") ? deductionArea : groupStats2.DeductionArea);
							groupStats += groupStats2;
						}
					}
				}
			}
			return groupStats;
		}

		public static GroupStats ComputeGroupStats(Project project, DrawObject drawObject, Filter filter, UnitScale.UnitSystem systemType, bool allLabels = true, string objectLabel = "")
		{
			string objectType = drawObject.ObjectType;
			GroupStats groupStats = new GroupStats(objectType);
			if (objectType != "Line" && objectType != "Area" && objectType != "Perimeter" && objectType != "Counter")
			{
				return groupStats;
			}
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				bool flag = false;
				if (filter != null)
				{
					flag = filter.QueryFilter(plan.Name, "", -1, false);
					if (!flag)
					{
						flag = filter.QueryFilter(plan.Name, "", drawObject.GroupID, true);
					}
				}
				if (!flag)
				{
					GroupStats drawObjectStats = GroupUtilities.ComputeGroupStats(plan, drawObject, systemType, allLabels, objectLabel);
					groupStats += drawObjectStats;
				}
			}
			return groupStats;
		}

        public static void UpdateGroupProperty(Project project, DrawingArea drawArea, DrawObject drawObject, string propertyName, object value)
        {
            string[] excludedGroupProperties = new string[]
            {
        "Label",
        "RenderingAngle",
        "RenderingOffsetX",
        "RenderingOffsetY"
            };

            bool isExcludedProperty = Array.IndexOf<string>(excludedGroupProperties, propertyName) != -1;
            bool shouldUpdateWholeGroup = (drawObject.GroupID > -1) && !isExcludedProperty;

            if (shouldUpdateWholeGroup)
            {
                foreach (object planItem in project.Plans.Collection)
                {
                    Plan plan = (Plan)planItem;

                    foreach (object layerItem in plan.Layers.Collection)
                    {
                        Layer layer = (Layer)layerItem;

                        foreach (object drawingObjectItem in layer.DrawingObjects.Collection)
                        {
                            DrawObject groupMember = (DrawObject)drawingObjectItem;

                            if (groupMember.GroupID == drawObject.GroupID)
                            {
                                GroupUtilities.UpdateProperty(project, groupMember, propertyName, value);
                            }
                        }
                    }
                }

                return;
            }

            if (isExcludedProperty && drawArea.ActiveDrawingObjects != null)
            {
                int selectedCount = drawArea.ActiveDrawingObjects.SelectionCount;

                for (int selectionIndex = 0; selectionIndex < selectedCount; selectionIndex++)
                {
                    DrawObject selectedObject = drawArea.ActiveDrawingObjects.GetSelectedObject(selectionIndex);
                    GroupUtilities.UpdateProperty(project, selectedObject, propertyName, value);
                }

                return;
            }

            GroupUtilities.UpdateProperty(project, drawObject, propertyName, value);
        }

        private static void UpdateProperty(Project project, DrawObject drawObject, string propertyName, object value)
		{
			if (drawObject.DeductionParentID != -1)
			{
				return;
			}
			switch (propertyName)
			{
			case "Name":
				drawObject.Name = value.ToString();
				return;
			case "Label":
				drawObject.Label = value.ToString();
				return;
			case "Comment":
				drawObject.Comment = value.ToString();
				if (drawObject.ObjectType == "Note")
				{
					((DrawNote)drawObject).RecalcLayout();
					return;
				}
				break;
			case "Text":
				drawObject.Text = value.ToString();
				return;
			case "Color":
				drawObject.Color = (Color)value;
				drawObject.FillColor = (Color)value;
				return;
			case "Pattern":
				((DrawPolyLine)drawObject).Pattern = (HatchStylePickerCombo.HatchStylePickerEnum)value;
				return;
			case "PenWidth":
				drawObject.PenWidth = (int)value;
				if (drawObject.ObjectType == "Note")
				{
					((DrawNote)drawObject).RecalcLayout();
					return;
				}
				break;
			case "FontSize":
				if (drawObject.ObjectType == "Note")
				{
					((DrawNote)drawObject).UpdateFontSize((int)value);
					return;
				}
				if (drawObject.ObjectType == "Legend")
				{
					((DrawLegend)drawObject).UpdateFontSize((int)value);
					return;
				}
				break;
			case "MaxRows":
				if (drawObject.ObjectType == "Legend")
				{
					((DrawLegend)drawObject).UpdateMaxRows((int)value);
					return;
				}
				break;
			case "Shape":
				((DrawCounter)drawObject).Shape = (DrawCounter.CounterShapeTypeEnum)value;
				return;
			case "CounterSize":
				((DrawCounter)drawObject).UpdateDefaultSize((int)value);
				return;
			case "SlopeFactor":
				drawObject.SetSlopeFactor((SlopeFactor)value);
				return;
			case "ShowMeasure":
				drawObject.ShowMeasure = (bool)value;
				return;
			case "Visible":
				drawObject.Visible = (bool)value;
				return;
			case "RenderingAngle":
				((DrawPolyLine)drawObject).SetCustomRenderingAngle(drawObject.Group.SelectedPreset.ID, (int)value);
				return;
			case "RenderingOffsetX":
				((DrawPolyLine)drawObject).SetCustomRenderingOffsetX(drawObject.Group.SelectedPreset.ID, (int)value);
				return;
			case "RenderingOffsetY":
				((DrawPolyLine)drawObject).SetCustomRenderingOffsetY(drawObject.Group.SelectedPreset.ID, (int)value);
				break;

				return;
			}
		}

		public static int GroupSelectedCount(DrawingArea drawArea, Layer layer)
		{
			int result;
			try
			{
				int num = 0;
				DrawObject selectedObject = layer.DrawingObjects.GetSelectedObject(0);
				if (selectedObject.IsPartOfGroup() && !selectedObject.IsDeduction())
				{
					num++;
					for (int i = 1; i < layer.DrawingObjects.SelectionCount; i++)
					{
						if (drawArea.ActiveDrawingObjects.GetSelectedObject(i).GroupID != selectedObject.GroupID || drawArea.ActiveDrawingObjects.GetSelectedObject(i).IsDeduction())
						{
							num = 0;
							break;
						}
						num++;
					}
				}
				result = num;
			}
			catch
			{
				Console.WriteLine("GroupSelectedCount_Err");
				result = 0;
			}
			return result;
		}

		public static bool SelectedObjectsHaveSameGroupID(Layer layer, DrawObject drawObject)
		{
			if (layer == null || drawObject == null)
			{
				return false;
			}
			if (!drawObject.IsPartOfGroup())
			{
				return false;
			}
			for (int i = 1; i < layer.DrawingObjects.SelectionCount; i++)
			{
				if (layer.DrawingObjects.GetSelectedObject(i).GroupID != drawObject.GroupID)
				{
					return false;
				}
			}
			return true;
		}

		public static bool SelectedObjectsHaveSameLabel(Layer layer, DrawObject drawObject)
		{
			if (layer == null || drawObject == null)
			{
				return false;
			}
			for (int i = 1; i < layer.DrawingObjects.SelectionCount; i++)
			{
				if (layer.DrawingObjects.GetSelectedObject(i).Label != drawObject.Label)
				{
					return false;
				}
			}
			return true;
		}

		public static int GroupNumberOfSameType(Project project, DrawObject drawObject)
		{
			if (drawObject == null)
			{
				return 0;
			}
			if (!drawObject.IsPartOfGroup())
			{
				return 0;
			}
			int num = 0;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject2 = (DrawObject)obj3;
						if (drawObject2.ObjectType == drawObject.ObjectType && !arrayList.Contains(drawObject2.GroupID))
						{
							arrayList.Add(drawObject2.GroupID);
							num++;
						}
					}
				}
			}
			arrayList.Clear();
			arrayList = null;
			return num;
		}

		public static int GroupNumberOfSameType(Plan plan, DrawObject drawObject)
		{
			if (drawObject == null)
			{
				return 0;
			}
			if (!drawObject.IsPartOfGroup())
			{
				return 0;
			}
			int num = 0;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject2 = (DrawObject)obj2;
					if (drawObject2.ObjectType == drawObject.ObjectType && !arrayList.Contains(drawObject2.GroupID))
					{
						arrayList.Add(drawObject2.GroupID);
						num++;
					}
				}
			}
			arrayList.Clear();
			arrayList = null;
			return num;
		}

		public static int ObjectsOfThisTypeCount(Layer layer, string objectType)
		{
			if (layer == null)
			{
				return 0;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in layer.DrawingObjects.Collection)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (!drawObject.IsDeduction() && drawObject.ObjectType == objectType && !arrayList.Contains(drawObject.ID + "_" + drawObject.ObjectType))
				{
					arrayList.Add(drawObject.ID + "_" + drawObject.ObjectType);
				}
			}
			int count = arrayList.Count;
			arrayList.Clear();
			arrayList = null;
			return count;
		}

		public static int ObjectTypeCount(Layer layer)
		{
			if (layer == null)
			{
				return 0;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in layer.DrawingObjects.Collection)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (!drawObject.IsDeduction() && !arrayList.Contains(drawObject.ObjectType))
				{
					arrayList.Add(drawObject.ObjectType);
				}
			}
			int count = arrayList.Count;
			arrayList.Clear();
			arrayList = null;
			return count;
		}

		public static void GroupsCountByType(Project project, string objectType, ref ArrayList groups)
		{
			if (project == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (drawObject.IsPartOfGroup() && !drawObject.IsDeduction() && drawObject.ObjectType == objectType && !arrayList.Contains(drawObject.GroupID))
						{
							arrayList.Add(drawObject.GroupID);
							groups.Add(drawObject);
						}
					}
				}
			}
			arrayList.Clear();
			arrayList = null;
		}

		public static int GroupsCountByType(Project project, string objectType)
		{
			if (project == null)
			{
				return 0;
			}
			ArrayList arrayList = new ArrayList();
			GroupUtilities.GroupsCountByType(project, objectType, ref arrayList);
			int count = arrayList.Count;
			arrayList.Clear();
			arrayList = null;
			return count;
		}

		public static int GroupsCount(Layer layer)
		{
			if (layer == null)
			{
				return 0;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in layer.DrawingObjects.Collection)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.IsPartOfGroup() && !drawObject.IsDeduction() && !arrayList.Contains(drawObject.GroupID))
				{
					arrayList.Add(drawObject.GroupID);
				}
			}
			int count = arrayList.Count;
			arrayList.Clear();
			arrayList = null;
			return count;
		}

		public static int GroupSelectedCount(DrawingArea drawArea, int groupID)
		{
			if (groupID == -1)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < drawArea.SelectionCount; i++)
			{
				if (drawArea.GetSelectedObject(i).GroupID == groupID)
				{
					num++;
				}
			}
			return num;
		}

		public static int GroupCount(Layer layer, int groupID, bool allLabels = true, string objectLabel = "")
		{
			if (groupID == -1)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in layer.DrawingObjects.Collection)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.GroupID == groupID && !drawObject.IsDeduction() && (allLabels || drawObject.Label == objectLabel))
				{
					num++;
				}
			}
			return num;
		}

		public static int GroupCount(Layer layer, DrawObject drawObject, bool allLabels = true, string objectLabel = "")
		{
			if (drawObject == null)
			{
				return 0;
			}
			if (drawObject.GroupID == -1)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in layer.DrawingObjects.Collection)
			{
				DrawObject drawObject2 = (DrawObject)obj;
				if (drawObject2.HasSameGroupOrID(drawObject) && !drawObject2.IsDeduction() && (allLabels || drawObject2.Label == objectLabel))
				{
					num++;
				}
			}
			return num;
		}

		public static int GroupCount(Plan plan, DrawObject drawObject, bool allLabels = true, string objectLabel = "")
		{
			if (drawObject == null)
			{
				return 0;
			}
			if (drawObject.GroupID == -1)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				num += GroupUtilities.GroupCount(layer, drawObject, allLabels, objectLabel);
			}
			return num;
		}

		public static int GroupCount(Project project, DrawObject drawObject, Filter filter = null, bool allLabels = true, string objectLabel = "")
		{
			if (drawObject == null)
			{
				return 0;
			}
			if (drawObject.GroupID == -1)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				bool flag = false;
				if (filter != null)
				{
					flag = filter.QueryFilter(plan.Name, "", drawObject.GroupID, false);
				}
				if (!flag)
				{
					num += GroupUtilities.GroupCount(plan, drawObject, allLabels, objectLabel);
				}
			}
			return num;
		}

		public static bool GroupExists(Project project, int groupID)
		{
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				if (GroupUtilities.GroupExists(plan, groupID))
				{
					return true;
				}
			}
			return false;
		}

		public static bool GroupExists(Plan plan, int groupID)
		{
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj2;
					if (drawObject.GroupID == groupID)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool GroupExists(DrawingObjects drawingObjects, int groupID)
		{
			foreach (object obj in drawingObjects.Collection)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.GroupID == groupID)
				{
					return true;
				}
			}
			return false;
		}

		public static int ObjectsCountForOtherLayers(Plan plan, int layerIndex)
		{
			int num = 0;
			for (int i = 0; i < plan.Layers.Count; i++)
			{
				if (i != layerIndex)
				{
					Layer layer = plan.Layers[i];
					foreach (object obj in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj;
						num += layer.DrawingObjects.Count;
					}
				}
			}
			return num;
		}

		public static int ObjectsCount(Plan plan)
		{
			int num = 0;
			foreach (object obj in plan.Layers.Collection)
			{
				Layer layer = (Layer)obj;
				foreach (object obj2 in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj2;
					num += layer.DrawingObjects.Count;
				}
			}
			return num;
		}

		public static void CleanUpCustomRendering(Project project, int groupID, string extensionID)
		{
			foreach (object obj in project.Plans.Collection)
			{
				Plan plan = (Plan)obj;
				foreach (object obj2 in plan.Layers.Collection)
				{
					Layer layer = (Layer)obj2;
					foreach (object obj3 in layer.DrawingObjects.Collection)
					{
						DrawObject drawObject = (DrawObject)obj3;
						if (drawObject.ObjectType == "Area")
						{
							DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
							if (drawPolyLine.GroupID == groupID && drawPolyLine.CustomRenderingArray.ContainsKey(extensionID))
							{
								drawPolyLine.CustomRenderingArray.Remove(extensionID);
							}
						}
					}
				}
			}
		}
	}
}
