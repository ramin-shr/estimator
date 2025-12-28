using System;
using System.Collections.Generic;

namespace QuoterPlan
{
	internal class CommandChangeStateEx : Command
	{
		public override void RenameLayer(string oldName, string newName)
		{
			this.sourceLayerName = ((this.sourceLayerName == oldName) ? newName : this.sourceLayerName);
			this.destLayerName = ((this.destLayerName == oldName) ? newName : this.destLayerName);
		}

		public CommandChangeStateEx(DrawingArea drawArea, string layerName, bool bringToFrontUndo = true, bool bringToFrontRedo = true)
		{
			this.drawArea = drawArea;
			this.sourceLayerName = layerName;
			this.bringToFrontUndo = bringToFrontUndo;
			this.bringToFrontRedo = bringToFrontRedo;
			Layer layer = drawArea.FindLayerByName(layerName);
			if (layer != null)
			{
				this.FillList(layer.DrawingObjects, ref this.sourceList);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public void NewState(string layerName)
		{
			this.destLayerName = layerName;
			Layer layer = this.drawArea.FindLayerByName(layerName);
			if (layer != null)
			{
				this.FillList(layer.DrawingObjects, ref this.destList);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Undo()
		{
			int destLayerIndex = -1;
			Layer layer = this.drawArea.FindLayerByName(this.sourceLayerName, ref destLayerIndex);
			Layer layer2 = this.drawArea.FindLayerByName(this.destLayerName);
			if (layer != null && layer2 != null)
			{
				this.UpdateObjects(layer2.DrawingObjects, this.destList, layer.DrawingObjects, this.sourceList, destLayerIndex, this.bringToFrontUndo);
				Console.WriteLine("CommandChangeLayer::Undo");
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Redo()
		{
			Layer layer = this.drawArea.FindLayerByName(this.sourceLayerName);
			int destLayerIndex = -1;
			Layer layer2 = this.drawArea.FindLayerByName(this.destLayerName, ref destLayerIndex);
			if (layer != null && layer2 != null)
			{
				this.UpdateObjects(layer.DrawingObjects, this.sourceList, layer2.DrawingObjects, this.destList, destLayerIndex, this.bringToFrontRedo);
				Console.WriteLine("CommandChangeLayer::Redo");
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		private void UpdateObjects(DrawingObjects sourceGraphicsList, List<DrawObject> sourceList, DrawingObjects destGraphicsList, List<DrawObject> destList, int destLayerIndex, bool bringToFront)
		{
			sourceGraphicsList.UnselectAll();
			this.RemoveObjects(sourceGraphicsList, sourceList);
			this.drawArea.Owner.SelectLayerInGUI(destLayerIndex);
			this.InsertObjects(destGraphicsList, destList, destLayerIndex, bringToFront);
			this.drawArea.Owner.ReloadObjectsInGUI();
		}

		private void InsertObjects(DrawingObjects graphicsList, List<DrawObject> list, int layerIndex, bool bringToFront)
		{
			int count = list.Count;
			bool flag = !bringToFront;
			int num = flag ? (count - 1) : 0;
			while (flag ? (num >= 0) : (num < count))
			{
				DrawObject drawObject = list[num].Clone();
				this.drawArea.SetObjectPropertiesFromGroup(drawObject);
				drawObject.Selected = (layerIndex == this.drawArea.ActiveLayerIndex);
				if (bringToFront)
				{
					graphicsList.Insert(drawObject, 0);
				}
				else
				{
					graphicsList.Add(drawObject);
				}
				Console.WriteLine("CommandChangeLayer::InsertObjects " + drawObject.ID);
				num += (flag ? -1 : 1);
			}
		}

		private void RemoveObjects(DrawingObjects graphicsList, List<DrawObject> list)
		{
			int count = graphicsList.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				bool flag = false;
				DrawObject drawObject = graphicsList[i];
				foreach (DrawObject drawObject2 in list)
				{
					if (drawObject.ID == drawObject2.ID)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					graphicsList.RemoveAt(i);
					Console.WriteLine("CommandChangeLayer::RemoveObjects " + drawObject.ID);
				}
			}
		}

		private void FillList(DrawingObjects graphicsList, ref List<DrawObject> listToFill)
		{
			listToFill = new List<DrawObject>();
			foreach (DrawObject drawObject in graphicsList.Selection)
			{
				if (!drawObject.IsDeduction())
				{
					DrawObject item = drawObject.Clone();
					listToFill.Add(item);
				}
			}
		}

		private string sourceLayerName;

		private string destLayerName;

		private List<DrawObject> sourceList;

		private List<DrawObject> destList;

		public bool bringToFrontUndo;

		public bool bringToFrontRedo;

		private DrawingArea drawArea;
	}
}
