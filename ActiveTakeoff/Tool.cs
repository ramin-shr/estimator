using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    internal abstract class Tool
    {
        public DrawingArea DrawArea
        {
            protected get;
            set;
        }

        protected Tool()
        {
        }

        public virtual void InitializeTool(Cursor cursor)
        {
        }

        public virtual void LoadCursor(Cursor cursor)
        {
        }

        public virtual void OnKeyDown(KeyEventArgs e)
        {
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
        }

        public virtual void ReleaseTool()
        {
        }

        public virtual void TrackMouse(Point location)
        {
        }
    }
}