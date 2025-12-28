using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;

namespace QuoterPlan
{
	public class Clipboard
	{
		public DrawingObjects Objects
		{
			get
			{
				return this.clipboardObjects;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.clipboardObjects.Count == 0;
			}
		}

		public Clipboard.Opening SelectedOpening
		{
			[CompilerGenerated]
			get
			{
				return this.<SelectedOpening>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SelectedOpening>k__BackingField = value;
			}
		}

		public event OnObjectPastedHandler OnObjectPasted
		{
			add
			{
				OnObjectPastedHandler onObjectPastedHandler = this.OnObjectPasted;
				OnObjectPastedHandler onObjectPastedHandler2;
				do
				{
					onObjectPastedHandler2 = onObjectPastedHandler;
					OnObjectPastedHandler value2 = (OnObjectPastedHandler)Delegate.Combine(onObjectPastedHandler2, value);
					onObjectPastedHandler = Interlocked.CompareExchange<OnObjectPastedHandler>(ref this.OnObjectPasted, value2, onObjectPastedHandler2);
				}
				while (onObjectPastedHandler != onObjectPastedHandler2);
			}
			remove
			{
				OnObjectPastedHandler onObjectPastedHandler = this.OnObjectPasted;
				OnObjectPastedHandler onObjectPastedHandler2;
				do
				{
					onObjectPastedHandler2 = onObjectPastedHandler;
					OnObjectPastedHandler value2 = (OnObjectPastedHandler)Delegate.Remove(onObjectPastedHandler2, value);
					onObjectPastedHandler = Interlocked.CompareExchange<OnObjectPastedHandler>(ref this.OnObjectPasted, value2, onObjectPastedHandler2);
				}
				while (onObjectPastedHandler != onObjectPastedHandler2);
			}
		}

		public void Clear()
		{
			this.wasCut = false;
			this.sourceDpiX = 0f;
			this.sourceDpiY = 0f;
			this.pastedDpiX = 0f;
			this.pastedDpiY = 0f;
			this.pastedObjects.Clear();
			this.clipboardObjects.Clear();
		}

		private void ShiftObject(DrawObject drawObject)
		{
			drawObject.Move(5, 5);
		}

		private void CopySelectedDrawingObjects(DrawingArea drawArea, DrawingObjects source, DrawingObjects destination, int layerIndex, bool reverse, bool bringToFront = true)
		{
			int num = reverse ? (source.SelectionCount - 1) : 0;
			while (reverse ? (num >= 0) : (num < source.SelectionCount))
			{
				DrawObject selectedObject = source.GetSelectedObject(num);
				if (selectedObject != null)
				{
					if (selectedObject.ObjectType != "Legend")
					{
						DrawObject drawObject = selectedObject.Clone();
						drawObject.Selected = true;
						if (layerIndex > -1)
						{
							Layer layer = drawArea.GetLayer(layerIndex);
							if (layer != null)
							{
								drawObject.Opacity = layer.Opacity;
							}
							if (drawObject.IsPartOfGroup())
							{
								drawObject.GroupID = (drawObject.GroupID + 2) * -1;
							}
							if (!this.wasCut)
							{
								drawArea.InsertObject(drawObject, layerIndex, bringToFront, false);
							}
							else
							{
								drawArea.InsertObject(drawObject, selectedObject.ID, layerIndex, bringToFront, false);
							}
							if (!(drawObject.ObjectType == "Area") && !(drawObject.ObjectType == "Perimeter"))
							{
								goto IL_18A;
							}
							DrawPolyLine drawPolyLine = (DrawPolyLine)drawObject;
							using (IEnumerator enumerator = drawPolyLine.DeductionArray.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									DrawObject drawObject2 = (DrawObject)obj;
									drawObject2.GroupID = drawObject.GroupID;
									drawObject2.Opacity = drawObject.Opacity;
									drawObject2.DeductionParentID = drawObject.ID;
									drawObject2.SetSlopeFactor(drawObject.SlopeFactor);
								}
								goto IL_18A;
							}
						}
						destination.Insert(drawObject, 0);
						this.pastedDpiX = drawArea.UnitScale.ReferenceDpiX;
						this.pastedDpiY = drawArea.UnitScale.ReferenceDpiY;
						this.pastedObjects.Add(selectedObject);
					}
				}
				else
				{
					Console.WriteLine("drawObject is null");
				}
				IL_18A:
				num += (reverse ? -1 : 1);
			}
		}

		public void Cut(DrawingArea drawArea)
		{
			this.Clear();
			this.CopySelectedDrawingObjects(drawArea, drawArea.ActiveDrawingObjects, this.clipboardObjects, -1, false, true);
			this.sourceDpiX = drawArea.UnitScale.ReferenceDpiX;
			this.sourceDpiY = drawArea.UnitScale.ReferenceDpiY;
			this.wasCut = true;
		}

		public void Copy(DrawingArea drawArea)
		{
			this.Clear();
			this.CopySelectedDrawingObjects(drawArea, drawArea.ActiveDrawingObjects, this.clipboardObjects, -1, false, true);
			this.sourceDpiX = drawArea.UnitScale.ReferenceDpiX;
			this.sourceDpiY = drawArea.UnitScale.ReferenceDpiY;
		}

		public void Paste(DrawingArea drawArea, bool bringToFront)
		{
			this.CopySelectedDrawingObjects(drawArea, this.clipboardObjects, drawArea.ActiveDrawingObjects, drawArea.ActiveLayerIndex, !bringToFront, bringToFront);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < drawArea.SelectionCount; i++)
			{
				DrawObject selectedObject = drawArea.GetSelectedObject(i);
				if (selectedObject != null)
				{
					if (selectedObject.GroupID < -1 && !arrayList.Contains(selectedObject.GroupID))
					{
						arrayList.Add(selectedObject.GroupID);
					}
					if (this.sourceDpiX != 0f && this.sourceDpiY != 0f && (this.sourceDpiX != drawArea.UnitScale.ReferenceDpiX || this.sourceDpiY != drawArea.UnitScale.ReferenceDpiY))
					{
						float scaleX = drawArea.UnitScale.ReferenceDpiX / this.sourceDpiX;
						float scaleY = drawArea.UnitScale.ReferenceDpiY / this.sourceDpiY;
						selectedObject.Scale(scaleX, scaleY);
					}
				}
			}
			for (int j = arrayList.Count - 1; j >= 0; j--)
			{
				DrawObject drawObject = drawArea.FindObjectFromGroupID((int)arrayList[j]);
				if (drawObject != null)
				{
					int num = drawArea.FindGroupLayer((drawObject.GroupID + 2) * -1);
					bool flag = num != drawArea.ActiveLayerIndex && num != -1;
					int groupID;
					if (flag)
					{
						groupID = drawArea.GetNextGroupID();
					}
					else
					{
						groupID = (drawObject.GroupID + 2) * -1;
					}
					drawArea.MapSelectedObjectsToGroup(drawArea.ActiveDrawingObjects, drawObject, groupID, flag);
				}
			}
			for (int k = drawArea.SelectionCount - 1; k >= 0; k--)
			{
				DrawObject selectedObject2 = drawArea.GetSelectedObject(k);
				if (selectedObject2 != null)
				{
					if (this.wasCut)
					{
						selectedObject2.Name = "_" + selectedObject2.Name;
					}
					else
					{
						if (!selectedObject2.IsPartOfGroup())
						{
							string text = "";
							selectedObject2.Name = drawArea.GetFreeObjectName(selectedObject2, ref text);
							selectedObject2.Text = text;
						}
						if (this.pastedObjects.Count > 0)
						{
							DrawObject drawObject2 = null;
							try
							{
								drawObject2 = (DrawObject)this.pastedObjects[k];
							}
							catch
							{
							}
							if (drawObject2 != null)
							{
								if (this.pastedDpiX == drawArea.UnitScale.ReferenceDpiX && this.pastedDpiY == drawArea.UnitScale.ReferenceDpiY)
								{
									DrawObject drawObject3 = drawObject2.Clone();
									if (this.pastedDpiX != 0f && this.pastedDpiY != 0f && (this.pastedDpiX != drawArea.UnitScale.ReferenceDpiX || this.pastedDpiY != drawArea.UnitScale.ReferenceDpiY))
									{
										float scaleX2 = drawArea.UnitScale.ReferenceDpiX / this.pastedDpiX;
										float scaleY2 = drawArea.UnitScale.ReferenceDpiY / this.pastedDpiY;
										drawObject3.Scale(scaleX2, scaleY2);
									}
									Point center = drawObject3.Center;
									Point center2 = selectedObject2.Center;
									int deltaX = center.X - center2.X + ((center.X + 5 > drawArea.DrawingBoard.Image.Width) ? 0 : 5);
									int deltaY = center.Y - center2.Y + ((center.Y + 5 > drawArea.DrawingBoard.Image.Height) ? 0 : 5);
									selectedObject2.Move(deltaX, deltaY);
								}
							}
							else
							{
								Console.WriteLine("pastedObject is null");
							}
						}
						else
						{
							Console.WriteLine("pastedObjects is empty");
						}
					}
				}
			}
			this.pastedObjects.Clear();
			for (int l = 0; l < drawArea.SelectionCount; l++)
			{
				DrawObject selectedObject3 = drawArea.GetSelectedObject(l);
				if (selectedObject3 != null)
				{
					this.pastedObjects.Add(selectedObject3);
				}
			}
			for (int m = drawArea.SelectionCount - 1; m >= 0; m--)
			{
				DrawObject selectedObject4 = drawArea.GetSelectedObject(m);
				if (selectedObject4 != null)
				{
					if (this.wasCut)
					{
						selectedObject4.Name = Utilities.Substring(selectedObject4.Name, 1, selectedObject4.Name.Length - 1);
					}
					this.pastedDpiX = drawArea.UnitScale.ReferenceDpiX;
					this.pastedDpiY = drawArea.UnitScale.ReferenceDpiY;
					if (this.OnObjectPasted != null)
					{
						this.OnObjectPasted(selectedObject4);
					}
				}
			}
			arrayList.Clear();
			arrayList = null;
			this.wasCut = false;
		}

		public Clipboard()
		{
		}

		private const int ShiftDeltaX = 5;

		private const int ShiftDeltaY = 5;

		private readonly DrawingObjects clipboardObjects = new DrawingObjects();

		private readonly ArrayList pastedObjects = new ArrayList();

		private float pastedDpiX;

		private float pastedDpiY;

		private float sourceDpiX;

		private float sourceDpiY;

		private bool wasCut;

		private OnObjectPastedHandler OnObjectPasted;

		[CompilerGenerated]
		private Clipboard.Opening <SelectedOpening>k__BackingField;

		public class Opening
		{
			public Opening(DrawLine drawLine, UnitScale scale)
			{
				this.drawLine = drawLine;
				this.scale = scale;
			}

			public int QueryLengthInPixels(UnitScale currentScale)
			{
				DrawLine drawLine = (DrawLine)this.drawLine.Clone();
				int num;
				if (this.scale.ScaleSystemType == currentScale.ScaleSystemType)
				{
					if (this.scale.ReferenceDpiX != 0f && this.scale.ReferenceDpiY != 0f && (this.scale.ReferenceDpiX != currentScale.ReferenceDpiX || this.scale.ReferenceDpiY != currentScale.ReferenceDpiY))
					{
						float scaleX = currentScale.ReferenceDpiX / this.scale.ReferenceDpiX;
						float scaleY = currentScale.ReferenceDpiY / this.scale.ReferenceDpiY;
						drawLine.Scale(scaleX, scaleY);
					}
					num = drawLine.Distance2D(false);
					if (this.scale.Scale != currentScale.Scale)
					{
						if (this.scale.ScaleSystemType == UnitScale.UnitSystem.metric)
						{
							num = (int)(1f / currentScale.Scale * (float)num / (1f / this.scale.Scale));
						}
						else
						{
							num = (int)(currentScale.Scale * (float)num / this.scale.Scale);
						}
					}
				}
				else
				{
					double num2 = this.scale.ToLength(drawLine.Distance2D(false));
					double value = (currentScale.ScaleSystemType == UnitScale.UnitSystem.imperial) ? UnitScale.FromMetersToFeet(num2) : num2;
					num = (int)currentScale.ToPixelsFromFeet(value);
				}
				return num;
			}

			public double QueryHeight(UnitScale currentScale)
			{
				if (this.scale.ScaleSystemType == currentScale.ScaleSystemType)
				{
					return this.drawLine.Height;
				}
				if (currentScale.ScaleSystemType != UnitScale.UnitSystem.imperial)
				{
					return UnitScale.FromFeetToMeters(this.drawLine.Height);
				}
				return UnitScale.FromMetersToFeet(this.drawLine.Height);
			}

			private DrawLine drawLine;

			private UnitScale scale;
		}
	}
}
