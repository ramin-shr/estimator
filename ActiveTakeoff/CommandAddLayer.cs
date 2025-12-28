using System;

namespace QuoterPlan
{
	public class CommandAddLayer : Command
	{
		public override void RenameLayer(string oldName, string newName)
		{
			this.layerName = ((this.layerName == oldName) ? newName : this.layerName);
		}

		public CommandAddLayer(DrawingArea drawArea, string layerName)
		{
			this.drawArea = drawArea;
			this.layerName = layerName;
			Console.WriteLine("Add layer " + this.layerName);
		}

		public override void Undo()
		{
			int p = -1;
			this.drawArea.FindLayerByName(this.layerName, ref p);
			if (this.drawArea.Layers.RemoveLayer(p))
			{
				this.drawArea.Owner.ReloadLayersInGUI(0);
				this.drawArea.Owner.ReloadObjectsInGUI();
				Console.WriteLine("AddLayer::Undo " + this.layerName);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Redo()
		{
			int layerIndex = this.drawArea.Layers.CreateNewLayer(this.layerName, 150);
			this.drawArea.Owner.ReloadLayersInGUI(layerIndex);
			this.drawArea.Owner.ReloadObjectsInGUI();
			Console.WriteLine("AddLayer::Redo " + this.layerName);
		}

		private string layerName;

		private DrawingArea drawArea;
	}
}
