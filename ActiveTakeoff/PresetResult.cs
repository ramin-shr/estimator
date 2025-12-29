using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class PresetResult : ExtensionResult
    {
        public bool ConditionMet
        {
            get;
            set;
        }

        public double Result
        {
            get;
            set;
        }

        public PresetResult(string name, string caption, string unit, string condition, string formula, ExtensionResult.ExtensionResultTypeEnum resultType, bool showInLegend = false, bool isEstimatingItem = true, int itemID = -1, int sectionID = -1, int subSectionID = -1)
        {
            base.Name = name;
            base.Caption = caption;
            base.Unit = unit;
            base.Condition = condition;
            base.Formula = formula;
            this.Result = 0;
            base.ResultType = resultType;
            base.ShowInLegend = showInLegend;
            base.IsEstimatingItem = isEstimatingItem;
            base.ItemID = itemID;
            base.SectionID = sectionID;
            base.SubSectionID = subSectionID;
            this.ConditionMet = false;
        }

        public new void Clear()
        {
            base.Clear();
            this.Result = 0;
            this.ConditionMet = false;
        }

        public new void Dump()
        {
            base.Dump();
            Console.WriteLine(string.Concat("Result = ", this.Result));
        }
    }
}