using System;
using System.Collections.Generic;

namespace QuoterPlan
{
	public class CommandPaste : Command
	{
		public override void RenameLayer(string oldName, string newName)
		{
			this.layerName = ((this.layerName == oldName) ? newName : this.layerName);
		}

		public CommandPaste(DrawingArea drawArea, string layerName)
		{
			this.drawArea = drawArea;
			this.layerName = layerName;
			Layer layer = drawArea.FindLayerByName(layerName);
			if (layer != null)
			{
				this.cloneList = new List<DrawObject>();
				using (IEnumerator<DrawObject> enumerator = layer.DrawingObjects.Selection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DrawObject drawObject = enumerator.Current;
						if (!drawObject.IsDeduction())
						{
							DrawObject item = drawObject.Clone();
							this.cloneList.Add(item);
						}
						Console.WriteLine("Paste " + drawObject.ID);
					}
					return;
				}
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Undo()
		{
			Layer layer = this.drawArea.FindLayerByName(this.layerName);
			if (layer != null)
			{
				int count = layer.DrawingObjects.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					bool flag = false;
					DrawObject drawObject = layer.DrawingObjects[i];
					foreach (DrawObject drawObject2 in this.cloneList)
					{
						if (drawObject.ID == drawObject2.ID)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						layer.DrawingObjects.RemoveAt(i);
						Console.WriteLine("Paste::Undo " + drawObject.ID);
					}
				}
				this.drawArea.Owner.ReloadObjectsInGUI();
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Redo()
		{
			int num = -1;
			Layer layer = this.drawArea.FindLayerByName(this.layerName, ref num);
			if (layer != null)
			{
				int count = this.cloneList.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					DrawObject drawObject = this.cloneList[i].Clone();
					this.drawArea.SetObjectPropertiesFromGroup(drawObject);
					drawObject.Selected = (num == this.drawArea.ActiveLayerIndex);
					layer.DrawingObjects.Insert(drawObject, 0);
					Console.WriteLine("Paste::Redo " + drawObject.ID);
				}
				this.drawArea.Owner.ReloadObjectsInGUI();
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		private string layerName;

		private DrawingArea drawArea;

		private List<DrawObject> cloneList;
	}
}
