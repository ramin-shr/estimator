using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class DockingWindow
    {
        public string Caption
        {
            get;
            set;
        }

        public Bar ContainerBar
        {
            get;
            set;
        }

        public Control ContainerControl
        {
            get;
            set;
        }

        public DockContainerItem ContainerItem
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public DockingWindow(string caption, bool visible, int width, int height, Bar containerBar, DockContainerItem containerItem, Control containerControl)
        {
            this.Caption = caption;
            this.Visible = visible;
            this.Width = width;
            this.Height = height;
            this.ContainerBar = containerBar;
            this.ContainerItem = containerItem;
            this.ContainerControl = containerControl;
            this.ContainerItem.DefaultFloatingSize = new Size(width, height);
        }
    }
}