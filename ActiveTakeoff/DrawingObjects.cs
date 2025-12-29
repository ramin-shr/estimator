using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DrawingObjects
    {
        private bool dirty;
        private readonly ArrayList objectList;

        public ArrayList Collection => this.objectList;

        public int Count => this.objectList.Count;

        public bool Dirty
        {
            get
            {
                if (!this.dirty)
                {
                    foreach (DrawObject drawObject in this.objectList)
                    {
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
                foreach (DrawObject drawObject in this.objectList)
                {
                    drawObject.Dirty = false;
                }
                this.dirty = false;
            }
        }

        public DrawObject this[int index]
        {
            get
            {
                if (index < 0 || index >= this.objectList.Count)
                    return null;

                return (DrawObject)this.objectList[index];
            }
        }

        public IEnumerable<DrawObject> Selection
        {
            get
            {
                foreach (DrawObject drawObject in this.objectList)
                {
                    if (drawObject.Selected)
                        yield return drawObject;
                }
            }
        }

        public int SelectionCount
        {
            get
            {
                int selectedCount = 0;
                foreach (DrawObject drawObject in this.objectList)
                {
                    if (drawObject.Selected)
                        selectedCount++;
                }
                return selectedCount;
            }
        }

        public DrawingObjects()
        {
            this.objectList = new ArrayList();
        }

        public void Add(DrawObject obj)
        {
            this.objectList.Add(obj);
        }

        private void ApplyProperties()
        {
        }

        public bool AutoAdjust(Bitmap image, DrawPolyLine.AutoAdjustType autoAdjustType)
        {
            bool anyAdjusted = false;

            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.Selected && drawObject.AutoAdjust(image, autoAdjustType))
                    anyAdjusted = true;
            }

            this.dirty = this.dirty || anyAdjusted;
            return anyAdjusted;
        }

        public bool Clear()
        {
            bool hadItems = this.objectList.Count > 0;

            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
                {
                    ((DrawPolyLine)drawObject).DeductionArray.Clear();
                }
                drawObject.ID = -1;
            }

            this.objectList.Clear();

            if (hadItems)
                this.dirty = false;

            return hadItems;
        }

        private void DeductionCleanUp(DrawObject drawObject)
        {
            if (drawObject.ObjectType != "Area" &&
                drawObject.ObjectType != "Perimeter" &&
                drawObject.ObjectType != "Line")
            {
                return;
            }

            if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
            {
                DrawPolyLine polyLine = (DrawPolyLine)drawObject;

                if (polyLine.DeductionParentID == -1)
                {
                    for (int i = polyLine.DeductionArray.Count - 1; i >= 0; i--)
                    {
                        ((DrawObject)polyLine.DeductionArray[i]).ID = -1;
                        polyLine.DeductionArray.RemoveAt(i);
                    }
                    return;
                }

                DrawPolyLine parent = (DrawPolyLine)this.GetObjectByID(polyLine.DeductionParentID);
                if (parent != null)
                {
                    parent.DeleteDeduction(polyLine);
                    return;
                }
            }
            else
            {
                DrawLine line = (DrawLine)drawObject;

                if (line.DeductionParentID != -1)
                {
                    DrawPolyLine parent = (DrawPolyLine)this.GetObjectByID(line.DeductionParentID);
                    if (parent != null)
                        parent.DeleteDeduction(line);
                }
            }
        }

        public bool Delete(DrawObject drawObject)
        {
            bool deletedAny = false;

            this.DeductionCleanUp(drawObject);

            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];

                bool shouldDelete = (item.ID == drawObject.ID) || (item.ID == -1);

                if (!shouldDelete && item.IsDeduction())
                {
                    shouldDelete = this.GetObjectByID(item.DeductionParentID) == null;
                }

                if (shouldDelete)
                {
                    item.ID = -1;
                    this.objectList.RemoveAt(i);
                    deletedAny = true;
                }
            }

            if (deletedAny)
                this.dirty = true;

            return deletedAny;
        }

        public bool DeleteSelection()
        {
            bool deletedAny = false;

            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];

                if (item.Selected && (item.ObjectType == "Area" || item.ObjectType == "Perimeter"))
                {
                    this.DeductionCleanUp(item);
                    deletedAny = true;
                }
            }

            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];

                if (item.ObjectType == "Legend")
                    continue;

                bool shouldDelete = item.Selected || item.ID == -1;

                if (!shouldDelete && item.IsDeduction())
                {
                    shouldDelete = this.GetObjectByID(item.DeductionParentID) == null;
                }

                if (shouldDelete)
                {
                    item.ID = -1;
                    this.objectList.RemoveAt(i);
                    deletedAny = true;
                }
            }

            if (deletedAny)
                this.dirty = true;

            return deletedAny;
        }

        public void Draw(
            object sender,
            Graphics g,
            int offsetX,
            int offsetY,
            bool layerVisible,
            bool printToScreen,
            MainForm.ImageQualityEnum imageQuality)
        {
            bool isMainControl = sender != null && sender.GetType().Name == "MainControl";

            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];

                if (!(layerVisible || item.ObjectType == "Legend"))
                    continue;

                if (!item.Visible)
                    continue;

                bool shouldDraw = true;

                if (isMainControl)
                    shouldDraw = item.IntersectsWith(Rectangle.Round(g.ClipBounds), offsetX, offsetY);

                if (!shouldDraw)
                    continue;

                bool suppressContent = !isMainControl &&
                                       (item.ObjectType == "Counter" || item.ObjectType == "Legend") &&
                                       printToScreen;

                if (suppressContent)
                    item.SuspendContentDrawing = true;

                item.Draw(g, offsetX, offsetY, printToScreen, imageQuality);

                if (item.Selected && printToScreen && isMainControl)
                    item.DrawTracker(g, offsetX, offsetY);

                if (suppressContent)
                    item.SuspendContentDrawing = false;
            }
        }

        public void DrawText(
            object sender,
            Graphics g,
            int offsetX,
            int offsetY,
            bool printToScreen,
            MainForm.ImageQualityEnum imageQuality,
            float defaultFontSize = 12f)
        {
            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];

                if (item.TextDirty || !printToScreen)
                    item.DrawText(g, offsetX, offsetY, printToScreen, imageQuality, defaultFontSize);
            }
        }

        public DrawingObjects Duplicate()
        {
            DrawingObjects copy = new DrawingObjects();

            foreach (DrawObject drawObject in this.objectList)
            {
                copy.Add(drawObject.Clone());
            }

            return copy;
        }

        public DrawObject GetObjectByID(int objectID)
        {
            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.ID == objectID)
                    return drawObject;
            }

            return null;
        }

        public DrawObject GetSelectedObject(int index)
        {
            int selectedIndex = -1;

            foreach (DrawObject drawObject in this.objectList)
            {
                if (!drawObject.Selected)
                    continue;

                selectedIndex++;

                if (selectedIndex == index)
                    return drawObject;
            }

            return null;
        }

        public void Insert(DrawObject obj, int index)
        {
            this.objectList.Insert(index, obj);
        }

        public void MoveSelected(int deltaX, int deltaY)
        {
            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.Selected)
                    drawObject.Move(deltaX, deltaY);
            }
        }

        public bool MoveSelectionToBack()
        {
            ArrayList selectedItems = new ArrayList();

            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                if (((DrawObject)this.objectList[i]).Selected)
                {
                    selectedItems.Add(this.objectList[i]);
                    this.objectList.RemoveAt(i);
                }
            }

            int movedCount = selectedItems.Count;

            for (int i = movedCount - 1; i >= 0; i--)
            {
                this.objectList.Add(selectedItems[i]);
            }

            if (movedCount > 0)
                this.dirty = true;

            return movedCount > 0;
        }

        public bool MoveSelectionToFront()
        {
            ArrayList selectedItems = new ArrayList();

            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                if (((DrawObject)this.objectList[i]).Selected)
                {
                    selectedItems.Add(this.objectList[i]);
                    this.objectList.RemoveAt(i);
                }
            }

            int movedCount = selectedItems.Count;

            for (int i = 0; i < movedCount; i++)
            {
                this.objectList.Insert(0, selectedItems[i]);
            }

            if (movedCount > 0)
                this.dirty = true;

            return movedCount > 0;
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < this.objectList.Count)
                this.objectList.RemoveAt(index);
        }

        public void Replace(int index, DrawObject obj)
        {
            if (index >= 0 && index < this.objectList.Count)
            {
                this.objectList.RemoveAt(index);
                this.objectList.Insert(index, obj);
            }
        }

        public void SelectAll()
        {
            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.Visible)
                    drawObject.Selected = true;
            }
        }

        public void SelectGroup(int groupID)
        {
            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.GroupID == groupID && drawObject.Visible)
                    drawObject.Selected = true;
            }
        }

        public void SelectInRectangle(Rectangle rectangle, int offsetX, int offsetY)
        {
            this.UnselectAll();

            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.Visible && drawObject.IntersectsWith(rectangle, offsetX, offsetY))
                    drawObject.Selected = true;
            }
        }

        public bool ShowPropertiesDialog(IWin32Window parent)
        {
            return true;
        }

        public void UnselectAll()
        {
            foreach (DrawObject drawObject in this.objectList)
            {
                drawObject.Selected = false;
            }
        }
    }
}
