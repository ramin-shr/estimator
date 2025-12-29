using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Bookmark
    {
        public int LayerIndex
        {
            get;
            set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Point Origin
        {
            get;
            set;
        }

        public int Zoom
        {
            get;
            set;
        }

        public Bookmark(string name, int layerIndex, int zoom, Point origin)
        {
            this.Name = name;
            this.LayerIndex = layerIndex;
            this.Zoom = zoom;
            this.Origin = origin;
        }
    }
}