using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class CustomRenderingProperties
    {
        public int Angle
        {
            get;
            set;
        }

        public int OffsetX
        {
            get;
            set;
        }

        public int OffsetY
        {
            get;
            set;
        }

        public Dictionary<string, CustomRenderingResult> ResultsArray
        {
            get;
            set;
        }

        public CustomRenderingProperties()
        {
            this.Angle = 0;
            this.OffsetX = 0;
            this.OffsetY = 0;
            this.ResultsArray = new Dictionary<string, CustomRenderingResult>();
        }

        public CustomRenderingProperties(int angle, int offsetX, int offsetY)
        {
            this.Angle = angle;
            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
            this.ResultsArray = new Dictionary<string, CustomRenderingResult>();
        }

        public CustomRenderingProperties Clone()
        {
            CustomRenderingProperties customRenderingProperty = new CustomRenderingProperties(this.Angle, this.OffsetX, this.OffsetY);
            foreach (KeyValuePair<string, CustomRenderingResult> resultsArray in customRenderingProperty.ResultsArray)
            {
                customRenderingProperty.ResultsArray.Add(resultsArray.Key, resultsArray.Value.Clone());
            }
            return customRenderingProperty;
        }
    }
}