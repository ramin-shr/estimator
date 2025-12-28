using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class DrawingObjects
	{
		public bool Dirty
		{
			get
			{
				if (!this.dirty)
				{
					foreach (object obj in this.objectList)
					{
						DrawObject drawObject = (DrawObject)obj;
						if (drawObject.Dirty)
						{
							this.dirty = true;
							break;
						}
					}
				}
				return this.dirty;
			}
			set
			{
				foreach (object obj in this.objectList)
				{
					DrawObject drawObject = (DrawObject)obj;
					drawObject.Dirty = false;
				}
				this.dirty = false;
			}
		}

		public IEnumerable<DrawObject> Selection
		{
			get
			{
				foreach (object obj in this.objectList)
				{
					DrawObject o = (DrawObject)obj;
					if (o.Selected)
					{
						yield return o;
					}
				}
				yield break;
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.objectList;
			}
		}

		public DrawingObjects()
		{
			this.objectList = new ArrayList();
		}

		public void Draw(object sender, Graphics g, int offsetX, int offsetY, bool layerVisible, bool printToScreen, MainForm.ImageQualityEnum imageQuality)
		{
			int num = 0;
			bool flag = sender.GetType().Name == "MainControl";
			for (int i = this.objectList.Count - 1; i >= 0; i--)
			{
				DrawObject drawObject = (DrawObject)this.objectList[i];
				if ((layerVisible || drawObject.ObjectType == "Legend") && drawObject.Visible)
				{
					bool flag2 = true;
					if (flag)
					{
						flag2 = drawObject.IntersectsWith(Rectangle.Round(g.ClipBounds), offsetX, offsetY);
					}
					if (flag2)
					{
						if (!flag && (drawObject.ObjectType == "Counter" || drawObject.ObjectType == "Legend") && printToScreen)
						{
							drawObject.SuspendContentDrawing = true;
						}
						drawObject.Draw(g, offsetX, offsetY, printToScreen, imageQuality);
						if (drawObject.Selected && printToScreen && flag)
						{
							drawObject.DrawTracker(g, offsetX, offsetY);
						}
						num++;
						if (!flag && (drawObject.ObjectType == "Counter" || drawObject.ObjectType == "Legend") && printToScreen)
						{
							drawObject.SuspendContentDrawing = false;
						}
					}
				}
			}
		}

		public void DrawText(object sender, Graphics g, int offsetX, int offsetY, bool printToScreen, MainForm.ImageQualityEnum imageQuality, float defaultFontSize = 12f)
		{
			for (int i = this.objectList.Count - 1; i >= 0; i--)
			{
				DrawObject drawObject = (DrawObject)this.objectList[i];
				if (drawObject.TextDirty || !printToScreen)
				{
					drawObject.DrawText(g, offsetX, offsetY, printToScreen, imageQuality, defaultFontSize);
				}
			}
		}

		public bool Clear()
		{
			bool flag = this.objectList.Count > 0;
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
				{
					DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
					drawPolyLine.DeductionArray.Clear();
				}
				drawObject.ID = -1;
			}
			this.objectList.Clear();
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
				return this.objectList.Count;
			}
		}

		public DrawObject this[int index]
		{
			get
			{
				if (index < 0 || index >= this.objectList.Count)
				{
					return null;
				}
				return (DrawObject)this.objectList[index];
			}
		}

		public int SelectionCount
		{
			get
			{
				int num = 0;
				foreach (object obj in this.objectList)
				{
					DrawObject drawObject = (DrawObject)obj;
					if (drawObject.Selected)
					{
						num++;
					}
				}
				return num;
			}
		}

		public DrawObject GetSelectedObject(int index)
		{
			int num = -1;
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.Selected)
				{
					num++;
					if (num == index)
					{
						return drawObject;
					}
				}
			}
			return null;
		}

		public void Insert(DrawObject obj, int index)
		{
			this.objectList.Insert(index, obj);
		}

		public void Add(DrawObject obj)
		{
			this.objectList.Add(obj);
		}

		public DrawObject GetObjectByID(int objectID)
		{
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.ID == objectID)
				{
					return drawObject;
				}
			}
			return null;
		}

		public void SelectInRectangle(Rectangle rectangle, int offsetX, int offsetY)
		{
			this.UnselectAll();
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.Visible && drawObject.IntersectsWith(rectangle, offsetX, offsetY))
				{
					drawObject.Selected = true;
				}
			}
		}

		public void MoveSelected(int deltaX, int deltaY)
		{
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.Selected)
				{
					drawObject.Move(deltaX, deltaY);
				}
			}
		}

		public void SelectGroup(int groupID)
		{
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.GroupID == groupID && drawObject.Visible)
				{
					drawObject.Selected = true;
				}
			}
		}

		public void UnselectAll()
		{
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				drawObject.Selected = false;
			}
		}

		public void SelectAll()
		{
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.Visible)
				{
					drawObject.Selected = true;
				}
			}
		}

		private void DeductionCleanUp(DrawObject drawObject)
		{
			if (drawObject.ObjectType != "Area" && drawObject.ObjectType != "Perimeter" && drawObject.ObjectType != "Line")
			{
				return;
			}
			if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
			{
				DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
				if (drawPolyLine.DeductionParentID == -1)
				{
					for (int i = drawPolyLine.DeductionArray.Count - 1; i >= 0; i--)
					{
						((DrawObject)drawPolyLine.DeductionArray[i]).ID = -1;
						drawPolyLine.DeductionArray.RemoveAt(i);
					}
					return;
				}
				DrawPolyLine drawPolyLine2 = (DrawPolyLine)this.GetObjectByID(drawPolyLine.DeductionParentID);
				if (drawPolyLine2 != null)
				{
					drawPolyLine2.DeleteDeduction(drawPolyLine);
					return;
				}
			}
			else
			{
				DrawLine drawLine = (DrawLine)drawObject;
				if (drawLine.DeductionParentID != -1)
				{
					DrawPolyLine drawPolyLine3 = (DrawPolyLine)this.GetObjectByID(drawLine.DeductionParentID);
					if (drawPolyLine3 != null)
					{
						drawPolyLine3.DeleteDeduction(drawLine);
					}
				}
			}
		}

		public bool Delete(DrawObject drawObject)
		{
			bool flag = false;
			this.DeductionCleanUp(drawObject);
			int count = this.objectList.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				DrawObject drawObject2 = (DrawObject)this.objectList[i];
				bool flag2 = drawObject2.ID == drawObject.ID || drawObject2.ID == -1;
				if (!flag2 && drawObject2.IsDeduction())
				{
					flag2 = (this.GetObjectByID(drawObject2.DeductionParentID) == null);
				}
				if (flag2)
				{
					drawObject2.ID = -1;
					this.objectList.RemoveAt(i);
					flag = true;
				}
			}
			if (flag)
			{
				this.dirty = true;
			}
			return flag;
		}

		public bool AutoAdjust(Bitmap image, DrawPolyLine.AutoAdjustType autoAdjustType)
		{
			bool flag = false;
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				if (drawObject.Selected && drawObject.AutoAdjust(image, autoAdjustType))
				{
					flag = true;
				}
			}
			this.dirty = (this.dirty ? this.dirty : flag);
			return flag;
		}

		public bool DeleteSelection()
		{
			bool flag = false;
			int count = this.objectList.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				DrawObject drawObject = (DrawObject)this.objectList[i];
				if (drawObject.Selected && (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter"))
				{
					this.DeductionCleanUp(drawObject);
					flag = true;
				}
			}
			count = this.objectList.Count;
			for (int j = count - 1; j >= 0; j--)
			{
				DrawObject drawObject2 = (DrawObject)this.objectList[j];
				if (drawObject2.ObjectType != "Legend")
				{
					bool flag2 = drawObject2.Selected || drawObject2.ID == -1;
					if (!flag2 && drawObject2.IsDeduction())
					{
						flag2 = (this.GetObjectByID(drawObject2.DeductionParentID) == null);
					}
					if (flag2)
					{
						drawObject2.ID = -1;
						this.objectList.RemoveAt(j);
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.dirty = true;
			}
			return flag;
		}

		public void Replace(int index, DrawObject obj)
		{
			if (index >= 0 && index < this.objectList.Count)
			{
				this.objectList.RemoveAt(index);
				this.objectList.Insert(index, obj);
			}
		}

		public void RemoveAt(int index)
		{
			if (index >= 0 && index < this.objectList.Count)
			{
				this.objectList.RemoveAt(index);
			}
		}

		public bool MoveSelectionToFront()
		{
			ArrayList arrayList = new ArrayList();
			int count = this.objectList.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (((DrawObject)this.objectList[i]).Selected)
				{
					arrayList.Add(this.objectList[i]);
					this.objectList.RemoveAt(i);
				}
			}
			count = arrayList.Count;
			for (int i = 0; i < count; i++)
			{
				this.objectList.Insert(0, arrayList[i]);
			}
			if (count > 0)
			{
				this.dirty = true;
			}
			return count > 0;
		}

		public bool MoveSelectionToBack()
		{
			ArrayList arrayList = new ArrayList();
			int count = this.objectList.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (((DrawObject)this.objectList[i]).Selected)
				{
					arrayList.Add(this.objectList[i]);
					this.objectList.RemoveAt(i);
				}
			}
			count = arrayList.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				this.objectList.Add(arrayList[i]);
			}
			if (count > 0)
			{
				this.dirty = true;
			}
			return count > 0;
		}

		private void ApplyProperties()
		{
		}

		public bool ShowPropertiesDialog(IWin32Window parent)
		{
			return true;
		}

		public DrawingObjects Duplicate()
		{
			DrawingObjects drawingObjects = new DrawingObjects();
			foreach (object obj in this.objectList)
			{
				DrawObject drawObject = (DrawObject)obj;
				DrawObject obj2 = drawObject.Clone();
				drawingObjects.Add(obj2);
			}
			return drawingObjects;
		}

		private bool dirty;

		private ArrayList objectList;

		[CompilerGenerated]
		private sealed class <get_Selection>d__0 : IEnumerable<DrawObject>, IEnumerable, IEnumerator<DrawObject>, IEnumerator, IDisposable
		{
			[DebuggerHidden]
			IEnumerator<DrawObject> IEnumerable<DrawObject>.GetEnumerator()
			{
				DrawingObjects.<get_Selection>d__0 <get_Selection>d__;
				if (Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId && this.<>1__state == -2)
				{
					this.<>1__state = 0;
					<get_Selection>d__ = this;
				}
				else
				{
					<get_Selection>d__ = new DrawingObjects.<get_Selection>d__0(0);
					<get_Selection>d__.<>4__this = this;
				}
				return <get_Selection>d__;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<QuoterPlan.DrawObject>.GetEnumerator();
			}

			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = this.objectList.GetEnumerator();
						this.<>1__state = 1;
						break;
					case 1:
						goto IL_95;
					case 2:
						this.<>1__state = 1;
						break;
					default:
						goto IL_95;
					}
					while (enumerator.MoveNext())
					{
						o = (DrawObject)enumerator.Current;
						if (o.Selected)
						{
							this.<>2__current = o;
							this.<>1__state = 2;
							return true;
						}
					}
					this.<>m__Finally4();
					IL_95:
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			DrawObject IEnumerator<DrawObject>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case 1:
				case 2:
					try
					{
					}
					finally
					{
						this.<>m__Finally4();
					}
					return;
				default:
					return;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			[DebuggerHidden]
			public <get_Selection>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			private void <>m__Finally4()
			{
				this.<>1__state = -1;
				disposable = (enumerator as IDisposable);
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			private DrawObject <>2__current;

			private int <>1__state;

			private int <>l__initialThreadId;

			public DrawingObjects <>4__this;

			public DrawObject <o>5__1;

			public IEnumerator <>7__wrap2;

			public IDisposable <>7__wrap3;
		}
	}
}
