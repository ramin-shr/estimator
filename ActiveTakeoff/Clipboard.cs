using QuoterPlanControls;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;

namespace QuoterPlan
{
    public class Clipboard
    {
        private const int ShiftDeltaX = 5;

        private const int ShiftDeltaY = 5;

        private readonly DrawingObjects clipboardObjects = new DrawingObjects();

        private readonly ArrayList pastedObjects = new ArrayList();

        private float pastedDpiX;

        private float pastedDpiY;

        private float sourceDpiX;

        private float sourceDpiY;

        private bool wasCut;

        public bool IsEmpty
        {
            get
            {
                return this.clipboardObjects.Count == 0;
            }
        }

        public DrawingObjects Objects
        {
            get
            {
                return this.clipboardObjects;
            }
        }

        public Clipboard.Opening SelectedOpening
        {
            get;
            set;
        }

        public Clipboard()
        {
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

        public void Copy(DrawingArea drawArea)
        {
            this.Clear();
            this.CopySelectedDrawingObjects(drawArea, drawArea.ActiveDrawingObjects, this.clipboardObjects, -1, false, true);
            this.sourceDpiX = drawArea.UnitScale.ReferenceDpiX;
            this.sourceDpiY = drawArea.UnitScale.ReferenceDpiY;
        }

        private void CopySelectedDrawingObjects(DrawingArea drawArea, DrawingObjects source, DrawingObjects destination, int layerIndex, bool reverse, bool bringToFront = true)
        {
            int num = (reverse ? source.SelectionCount - 1 : 0);
            while (true)
            {
                if ((reverse ? num < 0 : num >= source.SelectionCount))
                {
                    break;
                }
                DrawObject selectedObject = source.GetSelectedObject(num);
                if (selectedObject == null)
                {
                    Console.WriteLine("drawObject is null");
                }
                else if (selectedObject.ObjectType != "Legend")
                {
                    DrawObject opacity = selectedObject.Clone();
                    opacity.Selected = true;
                    if (layerIndex <= -1)
                    {
                        destination.Insert(opacity, 0);
                        this.pastedDpiX = drawArea.UnitScale.ReferenceDpiX;
                        this.pastedDpiY = drawArea.UnitScale.ReferenceDpiY;
                        this.pastedObjects.Add(selectedObject);
                    }
                    else
                    {
                        Layer layer = drawArea.GetLayer(layerIndex);
                        if (layer != null)
                        {
                            opacity.Opacity = layer.Opacity;
                        }
                        if (opacity.IsPartOfGroup())
                        {
                            opacity.GroupID = (opacity.GroupID + 2) * -1;
                        }
                        if (this.wasCut)
                        {
                            drawArea.InsertObject(opacity, selectedObject.ID, layerIndex, bringToFront, false);
                        }
                        else
                        {
                            drawArea.InsertObject(opacity, layerIndex, bringToFront, false);
                        }
                        if (opacity.ObjectType == "Area" || opacity.ObjectType == "Perimeter")
                        {
                            foreach (DrawObject deductionArray in ((DrawPolyLine)opacity).DeductionArray)
                            {
                                deductionArray.GroupID = opacity.GroupID;
                                deductionArray.Opacity = opacity.Opacity;
                                deductionArray.DeductionParentID = opacity.ID;
                                deductionArray.SetSlopeFactor(opacity.SlopeFactor);
                            }
                        }
                    }
                }
                num = num + (reverse ? -1 : 1);
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

        public void Paste(DrawingArea drawArea, bool bringToFront)
        {
            int num;
            this.CopySelectedDrawingObjects(drawArea, this.clipboardObjects, drawArea.ActiveDrawingObjects, drawArea.ActiveLayerIndex, !bringToFront, bringToFront);
            ArrayList arrayLists = new ArrayList();
            for (int i = 0; i < drawArea.SelectionCount; i++)
            {
                DrawObject selectedObject = drawArea.GetSelectedObject(i);
                if (selectedObject != null)
                {
                    if (selectedObject.GroupID < -1 && !arrayLists.Contains(selectedObject.GroupID))
                    {
                        arrayLists.Add(selectedObject.GroupID);
                    }
                    if (this.sourceDpiX != 0f && this.sourceDpiY != 0f && (this.sourceDpiX != drawArea.UnitScale.ReferenceDpiX || this.sourceDpiY != drawArea.UnitScale.ReferenceDpiY))
                    {
                        float referenceDpiX = drawArea.UnitScale.ReferenceDpiX / this.sourceDpiX;
                        float referenceDpiY = drawArea.UnitScale.ReferenceDpiY / this.sourceDpiY;
                        selectedObject.Scale(referenceDpiX, referenceDpiY);
                    }
                }
            }
            for (int j = arrayLists.Count - 1; j >= 0; j--)
            {
                DrawObject drawObject = drawArea.FindObjectFromGroupID((int)arrayLists[j]);
                if (drawObject != null)
                {
                    int num1 = drawArea.FindGroupLayer((drawObject.GroupID + 2) * -1);
                    bool flag = (num1 == drawArea.ActiveLayerIndex ? false : num1 != -1);
                    num = (!flag ? (drawObject.GroupID + 2) * -1 : drawArea.GetNextGroupID());
                    drawArea.MapSelectedObjectsToGroup(drawArea.ActiveDrawingObjects, drawObject, num, flag);
                }
            }
            for (int k = drawArea.SelectionCount - 1; k >= 0; k--)
            {
                DrawObject freeObjectName = drawArea.GetSelectedObject(k);
                if (freeObjectName != null)
                {
                    if (!this.wasCut)
                    {
                        if (!freeObjectName.IsPartOfGroup())
                        {
                            string str = "";
                            freeObjectName.Name = drawArea.GetFreeObjectName(freeObjectName, ref str);
                            freeObjectName.Text = str;
                        }
                        if (this.pastedObjects.Count <= 0)
                        {
                            Console.WriteLine("pastedObjects is empty");
                        }
                        else
                        {
                            DrawObject item = null;
                            try
                            {
                                item = (DrawObject)this.pastedObjects[k];
                            }
                            catch
                            {
                            }
                            if (item == null)
                            {
                                Console.WriteLine("pastedObject is null");
                            }
                            else
                            {
                                if ((this.pastedDpiX != drawArea.UnitScale.ReferenceDpiX ? false : this.pastedDpiY == drawArea.UnitScale.ReferenceDpiY))
                                {
                                    DrawObject drawObject1 = item.Clone();
                                    if (this.pastedDpiX != 0f && this.pastedDpiY != 0f && (this.pastedDpiX != drawArea.UnitScale.ReferenceDpiX || this.pastedDpiY != drawArea.UnitScale.ReferenceDpiY))
                                    {
                                        float single = drawArea.UnitScale.ReferenceDpiX / this.pastedDpiX;
                                        float referenceDpiY1 = drawArea.UnitScale.ReferenceDpiY / this.pastedDpiY;
                                        drawObject1.Scale(single, referenceDpiY1);
                                    }
                                    Point center = drawObject1.Center;
                                    Point point = freeObjectName.Center;
                                    int x = center.X - point.X + (center.X + 5 > drawArea.DrawingBoard.Image.Width ? 0 : 5);
                                    freeObjectName.Move(x, center.Y - point.Y + (center.Y + 5 > drawArea.DrawingBoard.Image.Height ? 0 : 5));
                                }
                            }
                        }
                    }
                    else
                    {
                        freeObjectName.Name = string.Concat("_", freeObjectName.Name);
                    }
                }
            }
            this.pastedObjects.Clear();
            for (int l = 0; l < drawArea.SelectionCount; l++)
            {
                DrawObject selectedObject1 = drawArea.GetSelectedObject(l);
                if (selectedObject1 != null)
                {
                    this.pastedObjects.Add(selectedObject1);
                }
            }
            for (int m = drawArea.SelectionCount - 1; m >= 0; m--)
            {
                DrawObject selectedObject2 = drawArea.GetSelectedObject(m);
                if (selectedObject2 != null)
                {
                    if (this.wasCut)
                    {
                        selectedObject2.Name = Utilities.Substring(selectedObject2.Name, 1, selectedObject2.Name.Length - 1);
                    }
                    this.pastedDpiX = drawArea.UnitScale.ReferenceDpiX;
                    this.pastedDpiY = drawArea.UnitScale.ReferenceDpiY;
                    if (this.OnObjectPasted != null)
                    {
                        this.OnObjectPasted(selectedObject2);
                    }
                }
            }
            arrayLists.Clear();
            arrayLists = null;
            this.wasCut = false;
        }

        private void ShiftObject(DrawObject drawObject)
        {
            drawObject.Move(5, 5);
        }

        public event OnObjectPastedHandler OnObjectPasted;

        public class Opening
        {
            private DrawLine drawLine;

            private UnitScale scale;

            public Opening(DrawLine drawLine, UnitScale scale)
            {
                this.drawLine = drawLine;
                this.scale = scale;
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

            public int QueryLengthInPixels(UnitScale currentScale)
            {
                int pixelsFromFeet = 0;
                DrawLine drawLine = (DrawLine)this.drawLine.Clone();
                if (this.scale.ScaleSystemType != currentScale.ScaleSystemType)
                {
                    double length = this.scale.ToLength(drawLine.Distance2D(false));
                    pixelsFromFeet = (int)currentScale.ToPixelsFromFeet((currentScale.ScaleSystemType == UnitScale.UnitSystem.imperial ? UnitScale.FromMetersToFeet(length) : length));
                }
                else
                {
                    if (this.scale.ReferenceDpiX != 0f && this.scale.ReferenceDpiY != 0f && (this.scale.ReferenceDpiX != currentScale.ReferenceDpiX || this.scale.ReferenceDpiY != currentScale.ReferenceDpiY))
                    {
                        float referenceDpiX = currentScale.ReferenceDpiX / this.scale.ReferenceDpiX;
                        float referenceDpiY = currentScale.ReferenceDpiY / this.scale.ReferenceDpiY;
                        drawLine.Scale(referenceDpiX, referenceDpiY);
                    }
                    pixelsFromFeet = drawLine.Distance2D(false);
                    if (this.scale.Scale != currentScale.Scale)
                    {
                        pixelsFromFeet = (this.scale.ScaleSystemType != UnitScale.UnitSystem.metric ? (int)(currentScale.Scale * (float)pixelsFromFeet / this.scale.Scale) : (int)(1f / currentScale.Scale * (float)pixelsFromFeet / (1f / this.scale.Scale)));
                    }
                }
                return pixelsFromFeet;
            }
        }
    }
}