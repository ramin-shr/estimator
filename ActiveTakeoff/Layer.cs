using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class Layer : BaseInfo
    {
        public DrawingObjects DrawingObjects
        {
            get;
            set;
        }

        public int Opacity
        {
            get;
            set;
        }

        public int SortIndex
        {
            get;
            set;
        }

        public Layer(string name, bool visible, bool active, int opacity)
        {
            base.Name = name;
            base.Visible = visible;
            base.Active = active;
            this.Opacity = opacity;
            this.DrawingObjects = new DrawingObjects();
        }

        public override void Clear()
        {
            base.Clear();
            if (this.DrawingObjects != null)
            {
                this.DrawingObjects.Clear();
            }
        }

        public Layer Clone()
        {
            return new Layer(base.Name, base.Visible, base.Active, this.Opacity);
        }
    }
}