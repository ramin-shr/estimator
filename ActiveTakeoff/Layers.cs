using System;
using System.Collections;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class Layers
	{
		public bool Dirty
		{
			get
			{
				if (!this.dirty)
				{
					foreach (object obj in this.layerList)
					{
						Layer layer = (Layer)obj;
						if (layer.Dirty)
						{
							this.dirty = true;
							break;
						}
					}
				}
				return this.dirty;
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.layerList;
			}
		}

		public Layers()
		{
			this.layerList = new ArrayList();
		}

		public int ActiveLayerIndex
		{
			get
			{
				int num = 0;
				foreach (object obj in this.layerList)
				{
					Layer layer = (Layer)obj;
					if (layer.Active)
					{
						break;
					}
					num++;
				}
				return num;
			}
		}

		public bool Clear(bool createDefault)
		{
			bool flag = this.layerList.Count > 0;
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				layer.Clear();
			}
			this.layerList.Clear();
			if (createDefault)
			{
				this.CreateDefaultLayer();
			}
			if (flag)
			{
				this.dirty = false;
			}
			return flag;
		}

		public int Count
		{
			get
			{
				return this.layerList.Count;
			}
		}

		public Layer this[int index]
		{
			get
			{
				if (index < 0 || index >= this.layerList.Count)
				{
					return null;
				}
				return (Layer)this.layerList[index];
			}
		}

		public int Add(Layer layer)
		{
			return this.layerList.Add(layer);
		}

		public void Insert(Layer layer, int index)
		{
			this.layerList.Insert(index, layer);
		}

		public int CreateNewLayer(string name, int opacity = 150)
		{
			this.InactivateAllLayers();
			Layer layer = new Layer(name, true, true, opacity);
			return this.Add(layer);
		}

		public int CreateNewLayer(string name, int layerIndex, int opacity = 150)
		{
			this.InactivateAllLayers();
			Layer value = new Layer(name, true, true, opacity);
			int index = (layerIndex > this.Count) ? this.Count : layerIndex;
			this.layerList.Insert(index, value);
			return this.FindLayerIndex(name);
		}

		public int CreateDefaultLayer()
		{
			return this.CreateNewLayer(Resources.Calque_par_défaut, 150);
		}

		public void InactivateAllLayers()
		{
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				layer.Active = false;
				if (layer.DrawingObjects != null)
				{
					layer.DrawingObjects.UnselectAll();
				}
			}
		}

		public void MakeLayerInvisible(int p)
		{
			if (p > -1 && p < this.layerList.Count)
			{
				((Layer)this.layerList[p]).Visible = false;
			}
		}

		public void MakeLayerVisible(int p)
		{
			if (p > -1 && p < this.layerList.Count)
			{
				((Layer)this.layerList[p]).Visible = true;
			}
		}

		public void ResetActiveLayer()
		{
			try
			{
				if (((Layer)this.layerList[this.ActiveLayerIndex]).DrawingObjects != null)
				{
					((Layer)this.layerList[this.ActiveLayerIndex]).DrawingObjects.UnselectAll();
				}
			}
			catch
			{
			}
		}

		public void SetActiveLayer(int p)
		{
			if (this.ActiveLayerIndex == p)
			{
				return;
			}
			if (p > -1 && p < this.layerList.Count)
			{
				this.ResetActiveLayer();
				foreach (object obj in this.layerList)
				{
					Layer layer = (Layer)obj;
					layer.Active = false;
				}
				((Layer)this.layerList[p]).Active = true;
			}
		}

		public bool RemoveLayer(int p)
		{
			if (this.layerList.Count == 1)
			{
				string action_invalide = Resources.Action_invalide;
				string il_est_interdit_de_supprimer_le_calque_par_défaut = Resources.Il_est_interdit_de_supprimer_le_calque_par_défaut;
				Utilities.DisplayError(action_invalide, il_est_interdit_de_supprimer_le_calque_par_défaut);
				return false;
			}
			if (p > -1 && p < this.layerList.Count)
			{
				((Layer)this.layerList[p]).DrawingObjects.Clear();
				this.layerList.RemoveAt(p);
			}
			return true;
		}

		public int MoveUp(int p)
		{
			int result = -1;
			if (p > -1 && p < this.layerList.Count)
			{
				Layer value = (Layer)this.layerList[p];
				this.layerList.RemoveAt(p);
				this.layerList.Insert(p + 1, value);
				result = p + 1;
			}
			return result;
		}

		public int MoveDown(int p)
		{
			int result = -1;
			if (p > -1 && p < this.layerList.Count)
			{
				Layer value = (Layer)this.layerList[p];
				this.layerList.RemoveAt(p);
				this.layerList.Insert(p - 1, value);
				result = p - 1;
			}
			return result;
		}

		public bool NameExists(string name)
		{
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				if (layer.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		public Layer FindLayer(string name)
		{
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				if (layer.Name == name)
				{
					return layer;
				}
			}
			return null;
		}

		public int FindLayerIndex(string name)
		{
			int num = 0;
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				if (layer.Name == name)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		public void HideAll()
		{
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				layer.LastVisible = layer.Visible;
				layer.Visible = false;
			}
		}

		public void RestoreVisible()
		{
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				layer.Visible = layer.LastVisible;
			}
		}

		public string FindFreeLayerName(string prefix)
		{
			string text = "";
			return this.FindFreeLayerName(prefix, ref text);
		}

		public string FindFreeLayerName(string prefix, ref string suffix)
		{
			int num = 0;
			string arg = prefix + " ";
			do
			{
				num++;
			}
			while (this.FindLayer(arg + num) != null);
			suffix = num.ToString();
			return arg + num;
		}

		public Layers Duplicate()
		{
			Layers layers = new Layers();
			foreach (object obj in this.layerList)
			{
				Layer layer = (Layer)obj;
				layers.Add(new Layer(layer.Name, layer.Visible, layer.Active, layer.Opacity)
				{
					DrawingObjects = layer.DrawingObjects.Duplicate()
				});
			}
			return layers;
		}

		private bool dirty;

		private ArrayList layerList;
	}
}
