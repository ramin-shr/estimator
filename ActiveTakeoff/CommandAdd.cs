using System;

namespace QuoterPlan
{
	public class CommandAdd : Command
	{
		public override void RenameLayer(string oldName, string newName)
		{
			this.layerName = ((this.layerName == oldName) ? newName : this.layerName);
		}

		public CommandAdd(DrawingArea drawArea, string layerName, DrawObject drawObject)
		{
			this.drawArea = drawArea;
			this.layerName = layerName;
			this.drawObject = drawObject.Clone();
			Console.WriteLine("Add " + this.drawObject.ID);
		}

		public override void Undo()
		{
			Layer layer = this.drawArea.FindLayerByName(this.layerName);
			if (layer != null)
			{
				layer.DrawingObjects.Delete(this.drawObject);
				this.drawArea.Owner.ReloadObjectsInGUI();
				Console.WriteLine("Add::Undo " + this.drawObject.ID);
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
				DrawObject drawObject = this.drawObject.Clone();
				this.drawArea.SetObjectPropertiesFromGroup(drawObject);
				drawObject.Selected = (num == this.drawArea.ActiveLayerIndex);
				layer.DrawingObjects.Insert(drawObject, 0);
				this.drawArea.Owner.ReloadObjectsInGUI();
				Console.WriteLine("Add::Redo " + drawObject.ID);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		private string layerName;

		private DrawObject drawObject;

		private DrawingArea drawArea;
	}
}
