using System;
using System.Collections.Generic;

namespace QuoterPlan
{
	public class CommandDeleteLayer : Command
	{
		public override void RenameLayer(string oldName, string newName)
		{
			this.layerName = ((this.layerName == oldName) ? newName : this.layerName);
		}

		public CommandDeleteLayer(DrawingArea drawArea, string layerName)
		{
			this.drawArea = drawArea;
			this.layerName = layerName;
			this.layerIndex = -1;
			Layer layer = drawArea.FindLayerByName(layerName, ref this.layerIndex);
			if (layer != null)
			{
				this.cloneList = new List<DrawObject>();
				foreach (object obj in layer.DrawingObjects.Collection)
				{
					DrawObject drawObject = (DrawObject)obj;
					if (!drawObject.IsDeduction())
					{
						DrawObject drawObject2 = drawObject.Clone();
						this.cloneList.Add(drawObject2);
						Console.WriteLine("Delete object " + drawObject2.ID);
					}
				}
				Console.WriteLine("Delete layer " + layerName);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Undo()
		{
			this.drawArea.Layers.CreateNewLayer(this.layerName, this.layerIndex);
			Layer layer = this.drawArea.FindLayerByName(this.layerName, ref this.layerIndex);
			if (layer != null)
			{
				int count = this.cloneList.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					DrawObject drawObject = this.cloneList[i].Clone();
					this.drawArea.SetObjectPropertiesFromGroup(drawObject);
					drawObject.Selected = (this.layerIndex == this.drawArea.ActiveLayerIndex);
					layer.DrawingObjects.Insert(drawObject, 0);
					Console.WriteLine("Delete object::Undo " + drawObject.ID);
				}
				this.drawArea.Owner.ReloadLayersInGUI(this.layerIndex);
				this.drawArea.Owner.ReloadObjectsInGUI();
				Console.WriteLine("Delete layer::Undo " + this.layerName);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Redo()
		{
			int p = -1;
			Layer layer = this.drawArea.FindLayerByName(this.layerName, ref p);
			if (layer != null)
			{
				if (this.drawArea.Layers.RemoveLayer(p))
				{
					this.drawArea.Owner.ReloadLayersInGUI(0);
					this.drawArea.Owner.ReloadObjectsInGUI();
					Console.WriteLine("Delete layer::Redo " + this.layerName);
					return;
				}
			}
			else
			{
				Console.WriteLine("Layer is NULL");
			}
		}

		private string layerName;

		private int layerIndex;

		private DrawingArea drawArea;

		private List<DrawObject> cloneList;
	}
}
