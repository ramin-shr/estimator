using System;
using System.Collections.Generic;

namespace QuoterPlan
{
	public class CommandChangeState : Command
	{
		public override void RenameLayer(string oldName, string newName)
		{
			this.layerName = ((this.layerName == oldName) ? newName : this.layerName);
		}

		public CommandChangeState(DrawingArea drawArea, string layerName)
		{
			this.drawArea = drawArea;
			this.layerName = layerName;
			Layer layer = drawArea.FindLayerByName(layerName);
			if (layer != null)
			{
				this.FillList(layer.DrawingObjects, ref this.listBefore);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public void NewState()
		{
			Layer layer = this.drawArea.FindLayerByName(this.layerName);
			if (layer != null)
			{
				this.FillList(layer.DrawingObjects, ref this.listAfter);
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Undo()
		{
			Layer layer = this.drawArea.FindLayerByName(this.layerName);
			if (layer != null)
			{
				this.ReplaceObjects(layer.DrawingObjects, this.listBefore, true);
				Console.WriteLine("CommandChangeState::Undo");
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		public override void Redo()
		{
			Layer layer = this.drawArea.FindLayerByName(this.layerName);
			if (layer != null)
			{
				this.ReplaceObjects(layer.DrawingObjects, this.listAfter, true);
				Console.WriteLine("CommandChangeState::Redo");
				return;
			}
			Console.WriteLine("Layer is NULL");
		}

		private void ReplaceObjects(DrawingObjects graphicsList, List<DrawObject> list, bool select)
		{
			foreach (DrawObject drawObject in list)
			{
				int index = -1;
				DrawObject drawObject2 = null;
				for (int i = 0; i < graphicsList.Count; i++)
				{
					if (drawObject.ID == graphicsList[i].ID)
					{
						index = i;
						drawObject2 = drawObject;
						break;
					}
				}
				if (drawObject2 != null)
				{
					DrawObject drawObject3 = drawObject2.Clone();
					this.drawArea.SetObjectPropertiesFromGroup(drawObject3);
					drawObject3.Selected = (select || drawObject3.Selected);
					graphicsList.Replace(index, drawObject3);
					this.drawArea.Owner.ReplaceObjectInGUI(drawObject3);
					this.drawArea.Owner.RefreshObject(drawObject3);
					Console.WriteLine("CommandChangeState::ReplaceObject " + drawObject3.ID);
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

		private List<DrawObject> listBefore;

		private List<DrawObject> listAfter;

		private string layerName;

		private DrawingArea drawArea;
	}
}
