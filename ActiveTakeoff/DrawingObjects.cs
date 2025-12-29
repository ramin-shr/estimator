using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DrawingObjects
    {
        private bool dirty;

        private ArrayList objectList;

        public ArrayList Collection
        {
            get
            {
                return this.objectList;
            }
        }

        public int Count
        {
            get
            {
                return this.objectList.Count;
            }
        }

        public bool Dirty
        {
            get
            {
                if (!this.dirty)
                {
                    foreach (DrawObject drawObject in this.objectList)
                    {
                        if (!drawObject.Dirty)
                        {
                            continue;
                        }
                        this.dirty = true;
                        break;
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
                {
                    return null;
                }
                return (DrawObject)this.objectList[index];
            }
        }

        public IEnumerable<DrawObject> Selection
        {
            get
            {
                foreach (DrawObject drawObject in this.objectList)
                {
                    if (!drawObject.Selected)
                    {
                        continue;
                    }
                    yield return drawObject;
                }
            }
        }

        public int SelectionCount
        {
            get
            {
                int num = 0;
                foreach (DrawObject drawObject in this.objectList)
                {
                    if (!drawObject.Selected)
                    {
                        continue;
                    }
                    num++;
                }
                return num;
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
            bool flag = false;
            foreach (DrawObject drawObject in this.objectList)
            {
                if (!drawObject.Selected || !drawObject.AutoAdjust(image, autoAdjustType))
                {
                    continue;
                }
                flag = true;
            }
            this.dirty = (this.dirty ? this.dirty : flag);
            return flag;
        }

        public bool Clear()
        {
            bool count = this.objectList.Count > 0;
            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.ObjectType == "Area" || drawObject.ObjectType == "Perimeter")
                {
                    ((DrawPolyLine)drawObject).DeductionArray.Clear();
                }
                drawObject.ID = -1;
            }
            this.objectList.Clear();
            if (count)
            {
                this.dirty = false;
            }
            return count;
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
                DrawPolyLine objectByID = (DrawPolyLine)this.GetObjectByID(drawPolyLine.DeductionParentID);
                if (objectByID != null)
                {
                    objectByID.DeleteDeduction(drawPolyLine);
                    return;
                }
            }
            else
            {
                DrawLine drawLine = (DrawLine)drawObject;
                if (drawLine.DeductionParentID != -1)
                {
                    DrawPolyLine objectByID1 = (DrawPolyLine)this.GetObjectByID(drawLine.DeductionParentID);
                    if (objectByID1 != null)
                    {
                        objectByID1.DeleteDeduction(drawLine);
                    }
                }
            }
        }

        public bool Delete(DrawObject drawObject)
        {
            bool flag = false;
            this.DeductionCleanUp(drawObject);
            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];
                bool objectByID = (item.ID == drawObject.ID ? true : item.ID == -1);
                if (!objectByID && item.IsDeduction())
                {
                    objectByID = this.GetObjectByID(item.DeductionParentID) == null;
                }
                if (objectByID)
                {
                    item.ID = -1;
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

        public bool DeleteSelection()
        {
            bool flag = false;
            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];
                if (item.Selected && (item.ObjectType == "Area" || item.ObjectType == "Perimeter"))
                {
                    this.DeductionCleanUp(item);
                    flag = true;
                }
            }
            for (int j = this.objectList.Count - 1; j >= 0; j--)
            {
                DrawObject drawObject = (DrawObject)this.objectList[j];
                if (drawObject.ObjectType != "Legend")
                {
                    bool objectByID = (drawObject.Selected ? true : drawObject.ID == -1);
                    if (!objectByID && drawObject.IsDeduction())
                    {
                        objectByID = this.GetObjectByID(drawObject.DeductionParentID) == null;
                    }
                    if (objectByID)
                    {
                        drawObject.ID = -1;
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

        public void Draw(object sender, Graphics g, int offsetX, int offsetY, bool layerVisible, bool printToScreen, MainForm.ImageQualityEnum imageQuality)
        {
            int num = 0;
            bool name = sender.GetType().Name == "MainControl";
            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];
                if ((layerVisible || item.ObjectType == "Legend") && item.Visible)
                {
                    bool flag = true;
                    if (name)
                    {
                        flag = item.IntersectsWith(Rectangle.Round(g.ClipBounds), offsetX, offsetY);
                    }
                    if (flag)
                    {
                        if (!name && (item.ObjectType == "Counter" || item.ObjectType == "Legend") && printToScreen)
                        {
                            item.SuspendContentDrawing = true;
                        }
                        item.Draw(g, offsetX, offsetY, printToScreen, imageQuality);
                        if (item.Selected && printToScreen && name)
                        {
                            item.DrawTracker(g, offsetX, offsetY);
                        }
                        num++;
                        if (!name && (item.ObjectType == "Counter" || item.ObjectType == "Legend") && printToScreen)
                        {
                            item.SuspendContentDrawing = false;
                        }
                    }
                }
            }
        }

        public void DrawText(object sender, Graphics g, int offsetX, int offsetY, bool printToScreen, MainForm.ImageQualityEnum imageQuality, float defaultFontSize = 12f)
        {
            for (int i = this.objectList.Count - 1; i >= 0; i--)
            {
                DrawObject item = (DrawObject)this.objectList[i];
                if (item.TextDirty || !printToScreen)
                {
                    item.DrawText(g, offsetX, offsetY, printToScreen, imageQuality, defaultFontSize);
                }
            }
        }

        public DrawingObjects Duplicate()
        {
            DrawingObjects drawingObject = new DrawingObjects();
            foreach (DrawObject drawObject in this.objectList)
            {
                drawingObject.Add(drawObject.Clone());
            }
            return drawingObject;
        }

        public DrawObject GetObjectByID(int objectID)
        {
            DrawObject drawObject;
            IEnumerator enumerator = this.objectList.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject current = (DrawObject)enumerator.Current;
                    if (current.ID != objectID)
                    {
                        continue;
                    }
                    drawObject = current;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public DrawObject GetSelectedObject(int index)
        {
            DrawObject drawObject;
            int num = -1;
            IEnumerator enumerator = this.objectList.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    DrawObject current = (DrawObject)enumerator.Current;
                    if (!current.Selected)
                    {
                        continue;
                    }
                    num++;
                    if (num != index)
                    {
                        continue;
                    }
                    drawObject = current;
                    return drawObject;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return drawObject;
        }

        public void Insert(DrawObject obj, int index)
        {
            this.objectList.Insert(index, obj);
        }

        public void MoveSelected(int deltaX, int deltaY)
        {
            foreach (DrawObject drawObject in this.objectList)
            {
                if (!drawObject.Selected)
                {
                    continue;
                }
                drawObject.Move(deltaX, deltaY);
            }
        }

        public bool MoveSelectionToBack()
        {
            int i;
            ArrayList arrayLists = new ArrayList();
            int count = this.objectList.Count;
            for (i = count - 1; i >= 0; i--)
            {
                if (((DrawObject)this.objectList[i]).Selected)
                {
                    arrayLists.Add(this.objectList[i]);
                    this.objectList.RemoveAt(i);
                }
            }
            count = arrayLists.Count;
            for (i = count - 1; i >= 0; i--)
            {
                this.objectList.Add(arrayLists[i]);
            }
            if (count > 0)
            {
                this.dirty = true;
            }
            return count > 0;
        }

        public bool MoveSelectionToFront()
        {
            int i;
            ArrayList arrayLists = new ArrayList();
            int count = this.objectList.Count;
            for (i = count - 1; i >= 0; i--)
            {
                if (((DrawObject)this.objectList[i]).Selected)
                {
                    arrayLists.Add(this.objectList[i]);
                    this.objectList.RemoveAt(i);
                }
            }
            count = arrayLists.Count;
            for (i = 0; i < count; i++)
            {
                this.objectList.Insert(0, arrayLists[i]);
            }
            if (count > 0)
            {
                this.dirty = true;
            }
            return count > 0;
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < this.objectList.Count)
            {
                this.objectList.RemoveAt(index);
            }
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
                if (!drawObject.Visible)
                {
                    continue;
                }
                drawObject.Selected = true;
            }
        }

        public void SelectGroup(int groupID)
        {
            foreach (DrawObject drawObject in this.objectList)
            {
                if (drawObject.GroupID != groupID || !drawObject.Visible)
                {
                    continue;
                }
                drawObject.Selected = true;
            }
        }

        public void SelectInRectangle(Rectangle rectangle, int offsetX, int offsetY)
        {
            this.UnselectAll();
            foreach (DrawObject drawObject in this.objectList)
            {
                if (!drawObject.Visible || !drawObject.IntersectsWith(rectangle, offsetX, offsetY))
                {
                    continue;
                }
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

        [CompilerGenerated]
        // <get_Selection>d__0
        private sealed class u003cget_Selectionu003ed__0 : IEnumerable<DrawObject>, IEnumerable, IEnumerator<DrawObject>, IEnumerator, IDisposable
        {
            // <>2__current
            private DrawObject u003cu003e2__current;

            // <>1__state
            private int u003cu003e1__state;

            // <>l__initialThreadId
            private int u003cu003el__initialThreadId;

            // <>4__this
            public DrawingObjects u003cu003e4__this;

            // <o>5__1
            public DrawObject u003cou003e5__1;

            // <>7__wrap2
            public IEnumerator u003cu003e7__wrap2;

            // <>7__wrap3
            public IDisposable u003cu003e7__wrap3;

            DrawObject System.Collections.Generic.IEnumerator<QuoterPlan.DrawObject>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.u003cu003e2__current;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.u003cu003e2__current;
                }
            }

            [DebuggerHidden]
            public u003cget_Selectionu003ed__0(int u003cu003e1__state)
            {
                this.u003cu003e1__state = u003cu003e1__state;
                this.u003cu003el__initialThreadId = Thread.CurrentThread.ManagedThreadId;
            }

            // <>m__Finally4
            private void u003cu003em__Finally4()
            {
                this.u003cu003e1__state = -1;
                this.u003cu003e7__wrap3 = this.u003cu003e7__wrap2 as IDisposable;
                if (this.u003cu003e7__wrap3 != null)
                {
                    this.u003cu003e7__wrap3.Dispose();
                }
            }

            bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.u003cu003e1__state)
                    {
                        case 0:
                            {
                                this.u003cu003e1__state = -1;
                                this.u003cu003e7__wrap2 = this.u003cu003e4__this.objectList.GetEnumerator();
                                this.u003cu003e1__state = 1;
                                break;
                            }
                        case 2:
                            {
                                this.u003cu003e1__state = 1;
                                break;
                            }
                        default:
                            {
                                goto Label0;
                            }
                    }
                    while (this.u003cu003e7__wrap2.MoveNext())
                    {
                        this.u003cou003e5__1 = (DrawObject)this.u003cu003e7__wrap2.Current;
                        if (!this.u003cou003e5__1.Selected)
                        {
                            continue;
                        }
                        this.u003cu003e2__current = this.u003cou003e5__1;
                        this.u003cu003e1__state = 2;
                        flag = true;
                        return flag;
                    }
                    this.u003cu003em__Finally4();
                Label0:
                    flag = false;
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<DrawObject> System.Collections.Generic.IEnumerable<QuoterPlan.DrawObject>.GetEnumerator()
            {
                DrawingObjects.u003cget_Selectionu003ed__0 variable;
                if (Thread.CurrentThread.ManagedThreadId != this.u003cu003el__initialThreadId || this.u003cu003e1__state != -2)
                {
                    variable = new DrawingObjects.u003cget_Selectionu003ed__0(0)
                    {
                        u003cu003e4__this = this.u003cu003e4__this
                    };
                }
                else
                {
                    this.u003cu003e1__state = 0;
                    variable = this;
                }
                return variable;
            }

            [DebuggerHidden]
            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.System.Collections.Generic.IEnumerable<QuoterPlan.DrawObject>.GetEnumerator();
            }

            [DebuggerHidden]
            void System.Collections.IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void System.IDisposable.Dispose()
            {
                switch (this.u003cu003e1__state)
                {
                    case 1:
                    case 2:
                        {
                            try
                            {
                            }
                            finally
                            {
                                this.u003cu003em__Finally4();
                            }
                            return;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }
    }
}