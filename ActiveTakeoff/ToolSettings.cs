using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ToolSettings
    {
        public bool CloseFigure
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public int CounterSize
        {
            get;
            set;
        }

        public bool DrawFilled
        {
            get;
            set;
        }

        public Color FillColor
        {
            get;
            set;
        }

        public int GroupID
        {
            get;
            set;
        }

        public bool IsDeduction
        {
            get;
            set;
        }

        public Color LineColor
        {
            get;
            set;
        }

        public int LineWidth
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Opacity
        {
            get;
            set;
        }

        public HatchStylePickerCombo.HatchStylePickerEnum Pattern
        {
            get;
            set;
        }

        public Segment SelectedSegment
        {
            get;
            private set;
        }

        public DrawCounter.CounterShapeTypeEnum Shape
        {
            get;
            set;
        }

        public bool ShowMeasure
        {
            get;
            set;
        }

        public SlopeFactor SlopeFactor
        {
            get;
            private set;
        }

        public Point StartPoint
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public ToolSettings()
        {
            this.SlopeFactor = new SlopeFactor(SlopeFactor.HipValleyEnum.hipValleyUnavailable);
            this.SelectedSegment = new Segment();
            this.Visible = true;
            this.ShowMeasure = true;
            this.DrawFilled = false;
            this.CloseFigure = false;
            this.LineWidth = -1;
            this.CounterSize = 80;
            this.Shape = DrawCounter.CounterShapeTypeEnum.CounterShapeCircle;
            this.Opacity = 150;
            this.FillColor = Color.White;
            this.LineColor = Color.Black;
            this.Text = string.Empty;
            this.Comment = string.Empty;
            this.Name = string.Empty;
            this.GroupID = -1;
            this.IsDeduction = false;
        }
    }
}